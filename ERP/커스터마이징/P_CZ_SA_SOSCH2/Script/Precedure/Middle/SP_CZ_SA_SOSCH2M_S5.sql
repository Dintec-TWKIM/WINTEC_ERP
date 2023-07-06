USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SOSCH2M_S5]    Script Date: 2018-07-23 오후 1:06:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--EXEC SP_CZ_SA_SOSCH2M_S 'TEST', '20150610', '1', '20150601', '20150615', '1', '1', '1', '002', '1', '2', '1', '1', '1', '1', '1', '1', '1', '002', '002', '002', '1'
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOSCH2M_S5] 
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_KEY				NVARCHAR(20),
	@P_NO_SO_PRE		NVARCHAR(5) = '',
	@P_NO_SO			NVARCHAR(20) = '',
	@P_DT_SO_FROM		NCHAR(8),
	@P_DT_SO_TO			NCHAR(8),
	@P_MULTI_SALEGRP	NVARCHAR(4000) = '',
	@P_TP_BUSI			NVARCHAR(3) = '',
	@P_STA_SO			NVARCHAR(3) = '', 
	@P_TP_SO			NVARCHAR(1000) = '',
	@P_CD_PARTNER	    NVARCHAR(4000) = '',
	@P_CD_PARTNER_GRP	NVARCHAR(4000) = '',
	@P_NO_EMP_MULTI		NVARCHAR(1000) = '',
	@P_MULTI_GRP_MFG	NVARCHAR(4000) = '',
	@P_NO_PO_PARTNER	NVARCHAR(50) = '',
	@P_CD_SUPPLIER		NVARCHAR(4000) = '',
	@P_NO_IMO			NVARCHAR(4000) = '',
	@P_PO_STATE			NVARCHAR(3), -- 001 -> 전체, 002 -> 미발주, 003 -> 발주
	@P_IN_STATE			NVARCHAR(3), -- 001 -> 전체, 002 -> 미입고, 003 -> 입고
	@P_GIR_STATE		NVARCHAR(3), -- 001 -> 전체, 002 -> 미의뢰, 003 -> 의뢰   
	@P_OUT_STATE		NVARCHAR(3), -- 001 -> 전체, 002 -> 미출고, 003 -> 출고   
	@P_IV_STATE			NVARCHAR(3), -- 001 -> 전체, 002 -> 미매출, 003 -> 매출
	@P_CLOSE_STATE		NVARCHAR(3), -- 001 -> 전체, 002 -> 미마감, 003 -> 마감 
	@P_ID_LOG			NVARCHAR(15) = '',
	@P_YN_AUTO_SUBMIT	NVARCHAR(1) = 'N',
	@P_YN_EX_CHARGE		NVARCHAR(1) = 'N',
	@P_YN_EX_FREE		NVARCHAR(1) = 'N'
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

WITH SA_SOH1 AS
(
	SELECT SH.CD_COMPANY, SH.NO_SO,
		   ST.RET
	FROM SA_SOH SH
	LEFT JOIN SA_TPSO ST ON ST.CD_COMPANY = SH.CD_COMPANY AND ST.TP_SO = SH.TP_SO
	WHERE SH.CD_COMPANY = @P_CD_COMPANY
	AND SH.CD_PARTNER = @P_KEY
	AND (@P_NO_SO <> '' OR SH.DT_SO BETWEEN @P_DT_SO_FROM AND @P_DT_SO_TO) 
	AND (@P_NO_SO = '' OR SH.NO_SO LIKE @P_NO_SO + '%')
	AND (@P_NO_SO_PRE = '' OR SH.NO_SO LIKE @P_NO_SO_PRE + '%')
	AND (@P_TP_SO = '' OR SH.TP_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_TP_SO)))
	AND (@P_CD_PARTNER_GRP = '' OR SH.CD_PARTNER_GRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP)))
	AND (@P_NO_EMP_MULTI = '' OR SH.NO_EMP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_NO_EMP_MULTI)))
	AND (@P_NO_IMO = '' OR SH.NO_IMO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_NO_IMO)))
	AND (@P_STA_SO = '' OR SH.STA_SO = @P_STA_SO)
	AND (@P_MULTI_SALEGRP = '' OR SH.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEGRP)))
	AND (@P_NO_PO_PARTNER = '' OR SH.NO_PO_PARTNER LIKE '%' + @P_NO_PO_PARTNER + '%')
	AND (@P_ID_LOG = '' OR EXISTS (SELECT 1 
								   FROM CZ_MA_WORKFLOWH WH
								   WHERE WH.CD_COMPANY = SH.CD_COMPANY
								   AND WH.NO_KEY = SH.NO_SO
								   AND WH.TP_STEP = '08'
								   AND WH.ID_LOG = @P_ID_LOG))
	AND (@P_CLOSE_STATE <> '002' OR SH.YN_CLOSE = 'N' OR SH.YN_CLOSE IS NULL)
	AND (@P_CLOSE_STATE <> '003' OR SH.YN_CLOSE = 'Y' AND EXISTS (SELECT 1 
																  FROM FI_GWDOCU GW
																  WHERE GW.CD_COMPANY = 'K100' 
																  AND GW.CD_PC = '010000'
																  AND GW.NO_DOCU = SH.NO_DOCU
																  AND (GW.ST_STAT IS NULL OR GW.ST_STAT IN ('0', '-1', '2', '3'))))
	AND (@P_CLOSE_STATE <> '004' OR SH.YN_CLOSE = 'Y' AND EXISTS (SELECT 1 
																  FROM FI_GWDOCU GW
																  WHERE GW.CD_COMPANY = 'K100' 
																  AND GW.CD_PC = '010000' 
																  AND GW.NO_DOCU = SH.NO_DOCU
																  AND GW.ST_STAT = '1'))
),
SA_SOL1 AS
(
	SELECT SH.CD_COMPANY,
	   	   SH.NO_SO,
	   	   MAX(QL.NM_SUBJECT) AS NM_SUBJECT,
	   	   MAX(GL.YN_AUTO_SUBMIT) AS YN_AUTO_SUBMIT,
	   	   MAX(QI.DT_PO) AS DT_PO,
	   	   MAX(QI.DT_IO) AS DT_IN,
	   	   MAX(QO.DT_IO) AS DT_OUT,
	   	   MAX(QO.DT_PROCESS) AS DT_IV,
	   	   MAX(QI.DT_LIMIT) AS DT_LIMIT,
	   	   MAX((CASE WHEN SL.LT IN (997, 998, 999) THEN '' ELSE SL.DT_DUEDATE END)) AS DT_DUEDATE,
	   	   MAX(QI.YN_PACK) AS YN_PACK,
	   	   SUM(CASE WHEN SH.RET = 'Y' THEN -SL.QT_SO ELSE SL.QT_SO END) AS QT_SO,
	   	   SUM(SB.QT_STOCK) AS QT_STOCK,
	   	   SUM(ISNULL(SL.QT_PO, 0) + ISNULL(SB.QT_STOCK, 0)) AS QT_PO,
	   	   SUM(SB.QT_BOOK) AS QT_BOOK,
		   SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM, 0) 
	   	   								 ELSE (ISNULL(QI.QT_REQ, 0) + ISNULL(SB.QT_BOOK, 0)) END) AS QT_REQ,
		   SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM_RETURN, 0) 
	   	   								 ELSE ISNULL(QI1.QT_REQ_RETURN, 0) END) AS QT_REQ_RETURN,
	   	   SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM, 0) 
		   								 ELSE (ISNULL(QI.QT_IO, 0) + ISNULL(SB.QT_BOOK, 0)) END) AS QT_IN,
	   	   SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM_RETURN, 0) 
		   								 ELSE ISNULL(QI1.QT_IO_RETURN, 0) END) AS QT_IN_RETURN,
	   	   SUM(GL.QT_GIR) AS QT_GIR,
	   	   SUM(GL1.QT_GIR) AS QT_GIR_RETURN,
	   	   SUM(PK.QT_GIR) AS QT_GIR_PACK,
	   	   SUM(PK.QT_PACK) AS QT_PACK,
	   	   SUM(GL.QT_IO) AS QT_OUT,
	   	   SUM(GL1.QT_IO) AS QT_OUT_RETURN,
	   	   SUM(QO.QT_IV) AS QT_IV,
	   	   SUM(QO.QT_IV_RETURN) AS QT_IV_RETURN,
	   	   SUM(CASE WHEN SH.RET = 'Y' THEN -SL.AM_SO ELSE SL.AM_SO END) AS AM_EX_SO,
		   SUM(CASE WHEN SH.RET = 'Y' THEN -SL.AM_WONAMT ELSE SL.AM_WONAMT END) AS AM_SO
	  FROM SA_SOH1 SH
	  JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	  LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
	  LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
	  LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO,
	  					MAX(WD.YN_AUTO_SUBMIT) AS YN_AUTO_SUBMIT, 
	  					SUM(GL.QT_GIR) AS QT_GIR,
	  					SUM(OL.QT_IO) AS QT_IO
	  			 FROM SA_GIRL GL
	  			 LEFT JOIN CZ_SA_GIRH_WORK_DETAIL WD ON WD.CD_COMPANY = GL.CD_COMPANY AND WD.NO_GIR = GL.NO_GIR
	  			 LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
	  			 GROUP BY GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO) GL 
	  ON GL.CD_COMPANY = SL.CD_COMPANY AND GL.NO_SO = SL.NO_SO AND GL.SEQ_SO = SL.SEQ_SO
	  LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_SO_MGMT, GL.NO_SOLINE_MGMT,
	  					SUM(-GL.QT_GIR) AS QT_GIR,
	  					SUM(-OL.QT_IO) AS QT_IO 
	  			 FROM SA_GIRL GL
	  			 LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
	  			 GROUP BY GL.CD_COMPANY, GL.NO_SO_MGMT, GL.NO_SOLINE_MGMT) GL1
	  ON GL1.CD_COMPANY = SL.CD_COMPANY AND GL1.NO_SO_MGMT = SL.NO_SO AND GL1.NO_SOLINE_MGMT = SL.SEQ_SO
	  LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.SEQ_SO,
	  		   			SUM(PL.QT_GIR) AS QT_GIR,
	  		   			SUM(PK.QT_PACK) AS QT_PACK 
	  		     FROM CZ_SA_GIRH_PACK PH
	  		     LEFT JOIN CZ_SA_GIRL_PACK PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR
	  		     LEFT JOIN (SELECT CD_COMPANY, NO_GIR, SEQ_GIR,
				 				   SUM(QT_PACK) AS QT_PACK 
				 		    FROM CZ_SA_PACKL
				 			GROUP BY CD_COMPANY, NO_GIR, SEQ_GIR) PK 
				 ON PK.CD_COMPANY = PL.CD_COMPANY AND PK.NO_GIR = PL.NO_GIR AND PK.SEQ_GIR = PL.SEQ_GIR 
	  		     GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.SEQ_SO) PK
	  ON PK.CD_COMPANY = SL.CD_COMPANY AND PK.NO_SO = SL.NO_SO AND PK.SEQ_SO = SL.SEQ_SO
	  LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
						MAX(PH.DT_PO) AS DT_PO,
						MAX(PL.DT_LIMIT) AS DT_LIMIT,
						MAX(IH.DT_IO) AS DT_IO,
						MAX(CASE WHEN IH.DC_RMK IS NOT NULL THEN 'Y' ELSE 'N' END) AS YN_PACK,
	  		   			SUM(RL.QT_REQ) AS QT_REQ,
		   		        SUM(IL.QT_IO) AS QT_IO
		         FROM PU_POH PH
	             LEFT JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
		         LEFT JOIN PU_RCVL RL ON RL.CD_COMPANY = PL.CD_COMPANY AND RL.NO_PO = PL.NO_PO AND RL.NO_POLINE = PL.NO_LINE
		         LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = RL.CD_COMPANY AND IL.NO_ISURCV = RL.NO_RCV AND IL.NO_ISURCVLINE = RL.NO_LINE
		         LEFT JOIN MM_QTIOH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO
	  		     GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) QI 
	  ON QI.CD_COMPANY = SL.CD_COMPANY AND QI.NO_SO = SL.NO_SO AND QI.NO_SOLINE = SL.SEQ_SO
	  LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
				        SUM(-RL.QT_REQ) AS QT_REQ_RETURN,
				        SUM(-IL.QT_IO) AS QT_IO_RETURN
		         FROM PU_POH PH
	             LEFT JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
		         LEFT JOIN PU_RCVL RL ON RL.CD_COMPANY = PL.CD_COMPANY AND RL.NO_PO_MGMT = PL.NO_PO AND RL.NO_POLINE_MGMT = PL.NO_LINE
		         LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = RL.CD_COMPANY AND IL.NO_ISURCV = RL.NO_RCV AND IL.NO_ISURCVLINE = RL.NO_LINE
	  		     GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) QI1 
	  ON QI1.CD_COMPANY = SL.CD_COMPANY AND QI1.NO_SO = SL.NO_SO AND QI1.NO_SOLINE = SL.SEQ_SO
	  LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
	  		   			MAX(CASE WHEN OL.CD_QTIOTP IN ('200','240','250','210','241','251') THEN OH.DT_IO ELSE NULL END) AS DT_IO,
						MAX(IH.DT_PROCESS) AS DT_PROCESS,
	  					SUM(CASE WHEN OH.YN_RETURN <> 'Y' AND OL.FG_PS = '1' AND OL.FG_IO = '002' THEN OL.QT_IO  ELSE 0 END) AS QT_BOM,
	  					SUM(CASE WHEN OH.YN_RETURN = 'Y' AND OL.FG_PS = '1' AND OL.FG_IO = '002' THEN -OL.QT_IO ELSE 0 END) AS QT_BOM_RETURN,
						SUM(CASE WHEN IL.YN_RETURN <> 'Y' THEN IL.QT_CLS ELSE 0 END) AS QT_IV,
	  				    SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.QT_CLS ELSE 0 END) AS QT_IV_RETURN
	  			 FROM MM_QTIOH OH
	  			 LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
				 LEFT JOIN SA_IVL IL ON IL.CD_COMPANY = OL.CD_COMPANY AND IL.NO_IO = OL.NO_IO AND IL.NO_IOLINE = OL.NO_IOLINE AND IL.CD_ITEM = OL.CD_ITEM
				 LEFT JOIN SA_IVH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IV = IL.NO_IV
	  			 GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) QO 
	  ON QO.CD_COMPANY = SL.CD_COMPANY AND QO.NO_PSO_MGMT = SL.NO_SO AND QO.NO_PSOLINE_MGMT = SL.SEQ_SO
	  WHERE (@P_TP_BUSI = '' OR SL.TP_BUSI = @P_TP_BUSI)
	  AND (@P_YN_EX_CHARGE = 'N' OR SL.CD_ITEM NOT LIKE 'SD%')
	  AND (@P_MULTI_GRP_MFG = '' OR EXISTS (SELECT 1 
										    FROM MA_PITEM MI 
										    WHERE MI.CD_COMPANY = SL.CD_COMPANY 
										    AND MI.CD_PLANT = SL.CD_PLANT 
										    AND MI.CD_ITEM = SL.CD_ITEM
										    AND MI.GRP_MFG IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_GRP_MFG))))
	  AND (@P_YN_AUTO_SUBMIT = 'N' OR GL.YN_AUTO_SUBMIT = 'Y')
	  AND (@P_YN_EX_FREE = 'N' OR (@P_YN_EX_FREE = 'Y' AND EXISTS (SELECT 1 
														           FROM MM_EJTP ET 
														           WHERE ET.CD_COMPANY = SL.CD_COMPANY 
														           AND ET.CD_QTIOTP = SL.TP_GI
														           AND ET.YN_AM = 'Y')))
	  AND (@P_CD_SUPPLIER = '' OR EXISTS (SELECT 1 
									      FROM PU_POL PL
									      LEFT JOIN PU_POH PH ON PH.CD_COMPANY = PL.CD_COMPANY AND PL.NO_PO = PH.NO_PO
									      WHERE PL.CD_COMPANY = SL.CD_COMPANY
									      AND PL.NO_SO = SL.NO_SO
									      AND PL.NO_SOLINE = SL.SEQ_SO
									      AND PH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SUPPLIER))) OR (SB.CD_COMPANY IS NOT NULL AND EXISTS (SELECT 1 
																																						         FROM GETTABLEFROMSPLIT(@P_CD_SUPPLIER)
																																						         WHERE CD_STR = 'STOCK')))
	  GROUP BY SH.CD_COMPANY, SH.NO_SO
)
SELECT @P_KEY AS CD_PARTNER,
	   SH.NO_SO,
	   SH.NO_PO_PARTNER,
	   SH.DT_SO,
	   MP.LN_PARTNER,
	   MH.NO_HULL,
	   MH.NM_VESSEL,
	   SH.NO_EMP,
	   ME.NM_KOR,
	   SH.NO_PROJECT,
	   PJ.NM_PROJECT,
	   ST.NM_SO,
	   SH.DC_RMK_TEXT2,
	   SH.DC_RMK1,
	   ISNULL(RH.AM_RCP_A, 0) AS AM_RCP_A,
	   SL.DT_PO,
	   (SELECT MAX(DT_EXPECT)
		FROM CZ_SA_DEFERRED_DELIVERY
		WHERE CD_COMPANY = SL.CD_COMPANY
		AND NO_SO = SL.NO_SO
		AND TP_TYPE = '2') AS DT_EXPECT_IN,
	   SL.DT_IN,
	   SL.DT_OUT,
	   SL.DT_IV,
	   RH.DT_RCP,
	   SL.DT_LIMIT,
	   SL.DT_DUEDATE,
	   (SELECT SUM(IH.WEIGHT)
		FROM MM_QTIOH IH
		WHERE IH.CD_COMPANY = SL.CD_COMPANY 
		AND (IH.YN_RETURN = 'N' OR IH.YN_RETURN IS NULL)
		AND IH.WEIGHT > 0
		AND EXISTS(SELECT 1 
				   FROM PU_POL PL
				   JOIN MM_QTIO IL ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
				   WHERE IL.CD_COMPANY = IH.CD_COMPANY
				   AND IL.NO_IO = IH.NO_IO
				   AND PL.NO_SO = SL.NO_SO)) AS WEIGHT,
	   MC.CD_CC,
	   MC.NM_CC,
	   CD3.NM_SYSDEF AS NM_EXCH,
	   SH.RT_EXCH,
	   ISNULL(SL.AM_EX_SO, 0) AS AM_EX_SO,
	   ISNULL(SL.AM_SO, 0) AS AM_SO,
	   (CASE WHEN ISNULL(CD4.CD_FLAG1, '') <> '' THEN 'Y' ELSE 'N' END) AS YN_PROFORMA,
	   CD.NM_SYSDEF AS NM_PARTNER_GRP,
	   CD1.NM_SYSDEF AS NM_NATION,
	   SL.YN_PACK,
	   ISNULL(SH.YN_CLOSE, 'N') AS YN_CLOSE,
	   GW.ST_STAT,
	   (CD2.NM_SYSDEF + ' ' + SH.PORT_LOADING) AS NM_TP_TRANSPORT,
	   SL.NM_SUBJECT,
	   SL.YN_AUTO_SUBMIT,
	   ISNULL(SL.QT_SO, 0) AS QT_SO,
	   ISNULL(SL.QT_STOCK, 0) AS QT_STOCK,
	   ISNULL(SL.QT_PO, 0) AS QT_PO,
	   ISNULL(SL.QT_BOOK, 0) AS QT_BOOK,
	   ISNULL(SL.QT_REQ, 0) AS QT_REQ,
	   ISNULL(SL.QT_REQ_RETURN, 0) AS QT_REQ_RETURN,
	   ISNULL(SL.QT_IN, 0) AS QT_IN,
	   ISNULL(SL.QT_IN_RETURN, 0) AS QT_IN_RETURN,
	   ISNULL(SL.QT_GIR, 0) AS QT_GIR,
	   ISNULL(SL.QT_GIR_RETURN, 0) AS QT_GIR_RETURN,
	   ISNULL(SL.QT_GIR_PACK, 0) AS QT_GIR_PACK,
	   ISNULL(SL.QT_PACK, 0) AS QT_PACK,
	   ((ISNULL(SL.QT_IN, 0) + ISNULL(SL.QT_IN_RETURN, 0)) - ISNULL(SL.QT_GIR, 0)) AS QT_READY,
	   ISNULL(SL.QT_OUT, 0) AS QT_OUT,
	   ISNULL(SL.QT_OUT_RETURN, 0) AS QT_OUT_RETURN,
	   (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_OUT, 0)) AS QT_GI_REMAIN,
	   ISNULL(SL.QT_IV, 0) AS QT_IV,
	   ISNULL(SL.QT_IV_RETURN, 0) AS QT_IV_RETURN,
	   (ISNULL(SL.QT_OUT, 0) - ISNULL(SL.QT_IV, 0)) AS QT_IV_REMAIN,
	   SH.DC_RMK_TEXT
FROM SA_SOL1 SL
JOIN SA_SOH SH ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO
LEFT JOIN (SELECT CD_COMPANY, NO_PROJECT,
				  MAX(DT_RCP) AS DT_RCP,
				  SUM(AM_RCP_A) AS AM_RCP_A
	       FROM SA_RCPH 
		   GROUP BY CD_COMPANY, NO_PROJECT) RH
ON RH.CD_COMPANY = SH.CD_COMPANY AND RH.NO_PROJECT = SH.NO_PROJECT
LEFT JOIN FI_GWDOCU GW ON GW.CD_COMPANY = 'K100' AND GW.CD_PC = '010000' AND GW.NO_DOCU = SH.NO_DOCU
LEFT JOIN SA_TPSO ST ON ST.CD_COMPANY = SH.CD_COMPANY AND ST.TP_SO = SH.TP_SO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
LEFT JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = SH.CD_COMPANY AND PJ.NO_PROJECT = SH.NO_PROJECT
LEFT JOIN MA_SALEGRP MS ON MS.CD_COMPANY = SH.CD_COMPANY AND MS.CD_SALEGRP = SH.CD_SALEGRP
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = MS.CD_COMPANY AND MC.CD_CC = MS.CD_CC
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = SH.CD_COMPANY AND CD.CD_FIELD = 'MA_B000065' AND CD.CD_SYSDEF = SH.CD_PARTNER_GRP
LEFT JOIN MA_CODEDTL CD1 ON CD1.CD_COMPANY = MP.CD_COMPANY AND CD1.CD_FIELD = 'MA_B000020' AND CD1.CD_SYSDEF = MP.CD_NATION
LEFT JOIN MA_CODEDTL CD2 ON CD2.CD_COMPANY = SH.CD_COMPANY AND CD2.CD_FIELD = 'TR_IM00003' AND CD2.CD_SYSDEF = SH.TP_TRANS
LEFT JOIN MA_CODEDTL CD3 ON CD3.CD_COMPANY = SH.CD_COMPANY AND CD3.CD_FIELD = 'MA_B000005' AND CD3.CD_SYSDEF = SH.CD_EXCH
LEFT JOIN MA_CODEDTL CD4 ON CD4.CD_COMPANY = SH.CD_COMPANY AND CD4.CD_FIELD = 'CZ_SA00013' AND CD4.CD_SYSDEF = SH.COND_PAY
WHERE 1=1
AND (@P_PO_STATE <> '002' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_PO, 0)) > 0)
AND (@P_PO_STATE <> '003' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_PO, 0)) = 0)
AND (@P_IN_STATE <> '002' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_IN, 0)) > 0)
AND (@P_IN_STATE <> '003' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_IN, 0)) = 0)
AND (@P_GIR_STATE <> '002' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_GIR, 0)) > 0)
AND (@P_GIR_STATE <> '003' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_GIR, 0)) = 0)
AND (@P_OUT_STATE <> '002' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_OUT, 0)) > 0)
AND (@P_OUT_STATE <> '003' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_OUT, 0)) = 0)
AND (@P_IV_STATE <> '002' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_IV, 0)) > 0)
AND (@P_IV_STATE <> '003' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_IV, 0)) = 0)

GO

