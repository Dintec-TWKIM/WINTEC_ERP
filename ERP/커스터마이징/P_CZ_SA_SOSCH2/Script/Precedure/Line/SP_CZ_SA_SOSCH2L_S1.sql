USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SOSCH2L_S1]    Script Date: 2018-07-23 오후 1:04:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
--EXEC SP_CZ_SA_SOSCH2L_S 'TEST', '1', '1', '1', '1', '1', '1', '1', '1'            
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOSCH2L_S1] 
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_KEY				NVARCHAR(20),
	@P_KEY2				NVARCHAR(20),
	@P_TP_BUSI			NVARCHAR(3) = '',      
	@P_STA_SO			NVARCHAR(3) = '',    
	@P_MULTI_GRP_MFG	NVARCHAR(4000) = '',
	@P_NO_PO_PARTNER	NVARCHAR(50) = '',
	@P_CD_SUPPLIER		NVARCHAR(4000) = '',
	@P_YN_AUTO_SUBMIT	NVARCHAR(1) = 'N',
	@P_YN_EX_CHARGE		NVARCHAR(1) = 'N',
	@P_YN_EX_FREE		NVARCHAR(1) = 'N'
)
AS  

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT @P_KEY AS NO_PO,
	   @P_KEY2 AS NO_SO,
	   SL.SEQ_SO,
	   QL.NO_DSP,
	   SL.CD_ITEM,
	   MI.NM_ITEM,
	   QL.NM_SUBJECT,
	   QL.CD_ITEM_PARTNER,
	   QL.NM_ITEM_PARTNER,
	   MI.STND_ITEM,
	   MI.UNIT_SO,
	   PH.CD_PARTNER AS CD_SUPPLIER,
	   MP.LN_PARTNER AS LN_SUPPLIER,
	   CONVERT(NUMERIC, QI.DT_LIMIT) AS DT_LIMIT,
	   (CASE WHEN SL.LT IN (997, 998, 999) THEN '' ELSE SL.DT_DUEDATE END) AS DT_DUEDATE,
	   SL.DT_REQGI,
 	   ISNULL(CASE WHEN ST.RET = 'Y' THEN -SL.QT_SO ELSE SL.QT_SO END, 0) AS QT_SO,
 	   ISNULL(SB.QT_STOCK, 0) AS QT_STOCK,
 	   ISNULL(SB.QT_BOOK, 0) AS QT_BOOK,
	   (ISNULL(SL.QT_PO, 0) + ISNULL(SB.QT_STOCK, 0)) AS QT_PO,
	   (CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM, 0) 
	   							  ELSE (ISNULL(QI.QT_REQ, 0) + ISNULL(SB.QT_BOOK, 0)) END) AS QT_REQ,
	   (CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM_RETURN, 0) 
	   							  ELSE ISNULL(QI1.QT_REQ_RETURN, 0) END) AS QT_REQ_RETURN,
 	   (CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM, 0) 
	   							  ELSE (ISNULL(QI.QT_IO, 0) + ISNULL(SB.QT_BOOK, 0)) END) AS QT_IN,
 	   (CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM_RETURN, 0) 
	   							  ELSE ISNULL(QI1.QT_IO_RETURN, 0) END) AS QT_IN_RETURN,
 	   ISNULL(GL.QT_GIR, 0) AS QT_GIR,
 	   ISNULL(GL1.QT_GIR, 0) AS QT_GIR_RETURN,
 	   ISNULL(PK.QT_GIR, 0) AS QT_GIR_PACK,
 	   ISNULL(PK.QT_PACK, 0) AS QT_PACK,
	   (((CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM, 0) 
	   							    ELSE (ISNULL(QI.QT_IO, 0) + ISNULL(SB.QT_BOOK, 0)) END) + 
	     (CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM_RETURN, 0) 
	   							    ELSE ISNULL(QI1.QT_IO_RETURN, 0) END)) - ISNULL(GL.QT_GIR, 0)) AS QT_READY,
 	   ISNULL(GL.QT_IO, 0) AS QT_OUT,
 	   ISNULL(GL1.QT_IO, 0) AS QT_OUT_RETURN,
	   (ISNULL(CASE WHEN ST.RET = 'Y' THEN -SL.QT_SO ELSE SL.QT_SO END, 0) - ISNULL(GL.QT_IO, 0)) AS QT_OUT_REMAIN,
 	   ISNULL(QO.QT_IV, 0) AS QT_IV,
 	   ISNULL(QO.QT_IV_RETURN, 0) AS QT_IV_RETURN,
	   (ISNULL(GL.QT_IO, 0) - ISNULL(QO.QT_IV, 0)) AS QT_IV_REMAIN,
	   MC.NM_SYSDEF AS NM_STA_SO,
	   PG.LN_PARTNER AS LN_GI_PARTNER,
	   --NEOE.FN_CZ_GET_STOCK_QTY (SL.CD_COMPANY, CONVERT(CHAR(8), GETDATE(), 112), SL.CD_PLANT, SL.CD_ITEM) AS QT_INV,
	   ML.NM_SL,
	   SL.NO_PO_PARTNER,
	   SL.NO_POLINE_PARTNER,
	   QI.CD_LOCATION,
	   ISNULL(CASE WHEN ST.RET = 'Y' THEN -SL.UM_EX_S ELSE SL.UM_EX_S END, 0) AS UM_EX,
	   ISNULL(CASE WHEN ST.RET = 'Y' THEN -SL.UM_SO ELSE SL.UM_SO END, 0) AS UM,
 	   ISNULL(CASE WHEN ST.RET = 'Y' THEN -SL.AM_SO ELSE SL.AM_SO END, 0) AS AM_EX,
	   ISNULL(CASE WHEN ST.RET = 'Y' THEN -SL.AM_WONAMT ELSE SL.AM_WONAMT END, 0) AS AM,
	   ((CASE WHEN MI.WEIGHT IS NOT NULL THEN MI.WEIGHT
			  WHEN HS.WEIGHT_LOG IS NOT NULL THEN HS.WEIGHT_LOG
			  WHEN HS.WEIGHT_HGS IS NOT NULL THEN HS.WEIGHT_HGS		
			  ELSE 0 END) * ISNULL(SL.QT_SO, 0)) AS WEIGHT
FROM SA_SOH SH
JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN SA_TPSO ST ON ST.CD_COMPANY = SH.CD_COMPANY AND ST.TP_SO = SH.TP_SO
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_PLANT = SL.CD_PLANT AND MI.CD_ITEM = SL.CD_ITEM
LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
LEFT JOIN CZ_MA_KCODE_HGS HS ON HS.CD_COMPANY = QL.CD_COMPANY AND HS.KCODE = QL.NO_DRAWING AND HS.KCODE != ''
LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE, PH.NO_PO, PH.CD_PARTNER
		   FROM PU_POH PH
		   JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
		   GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE, PH.NO_PO, PH.CD_PARTNER
		   UNION ALL
		   SELECT CD_COMPANY, NO_FILE, NO_LINE, 'STOCK', 'STOCK'
		   FROM CZ_SA_STOCK_BOOK) PH 
ON PH.CD_COMPANY = SL.CD_COMPANY AND PH.NO_SO = SL.NO_SO AND PH.NO_SOLINE = SL.SEQ_SO
LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO,
				  SUM(GL.QT_GIR) AS QT_GIR,
				  SUM(OL.QT_IO) AS QT_IO
		   FROM SA_GIRL GL
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
				  MAX(PL.DT_LIMIT) AS DT_LIMIT,
		   		  SUM(RL.QT_REQ) AS QT_REQ,
   		          SUM(IL.QT_IO) AS QT_IO,
				  MAX(LC.CD_LOCATION) AS CD_LOCATION
           FROM PU_POL PL
           LEFT JOIN PU_RCVL RL ON RL.CD_COMPANY = PL.CD_COMPANY AND RL.NO_PO = PL.NO_PO AND RL.NO_POLINE = PL.NO_LINE
           LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = RL.CD_COMPANY AND IL.NO_ISURCV = RL.NO_RCV AND IL.NO_ISURCVLINE = RL.NO_LINE
		   LEFT JOIN MM_QTIO_LOCATION LC ON LC.CD_COMPANY = IL.CD_COMPANY AND LC.NO_IO = IL.NO_IO AND LC.NO_IOLINE = IL.NO_IOLINE
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
				  SUM(CASE WHEN OH.YN_RETURN <> 'Y' AND OL.FG_PS = '1' AND OL.FG_IO = '002' THEN OL.QT_IO  ELSE 0 END) AS QT_BOM,
				  SUM(CASE WHEN OH.YN_RETURN = 'Y' AND OL.FG_PS = '1' AND OL.FG_IO = '002' THEN -OL.QT_IO ELSE 0 END) AS QT_BOM_RETURN,
				  SUM(CASE WHEN IL.YN_RETURN <> 'Y' THEN IL.QT_CLS ELSE 0 END) AS QT_IV,
				  SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.QT_CLS ELSE 0 END) AS QT_IV_RETURN
		   FROM MM_QTIOH OH
		   LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
		   LEFT JOIN SA_IVL IL ON IL.CD_COMPANY = OL.CD_COMPANY AND IL.NO_IO = OL.NO_IO AND IL.NO_IOLINE = OL.NO_IOLINE AND IL.CD_ITEM = OL.CD_ITEM
		   GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) QO 
ON QO.CD_COMPANY = SL.CD_COMPANY AND QO.NO_PSO_MGMT = SL.NO_SO AND QO.NO_PSOLINE_MGMT = SL.SEQ_SO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
LEFT JOIN MA_PARTNER PG ON PG.CD_COMPANY = SL.CD_COMPANY AND PG.CD_PARTNER = SL.GI_PARTNER
LEFT JOIN MA_PLANT PT ON PT.CD_COMPANY = SL.CD_COMPANY AND PT.CD_PLANT = SL.CD_PLANT
LEFT JOIN MA_SL ML ON ML.CD_COMPANY = SL.CD_COMPANY AND ML.CD_PLANT = SL.CD_PLANT AND ML.CD_BIZAREA = PT.CD_BIZAREA AND ML.CD_SL = SL.CD_SL
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = SL.CD_COMPANY AND MC.CD_FIELD = 'SA_B000016' AND MC.CD_SYSDEF = SL.STA_SO
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND (PH.NO_PO = @P_KEY OR (@P_KEY = '' AND PH.NO_PO IS NULL))
AND SH.NO_SO = @P_KEY2
AND (@P_TP_BUSI = '' OR SL.TP_BUSI = @P_TP_BUSI)
AND (@P_YN_EX_CHARGE = 'N' OR SL.CD_ITEM NOT LIKE 'SD%')
AND (@P_MULTI_GRP_MFG = '' OR MI.GRP_MFG IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_GRP_MFG)))
AND (@P_YN_AUTO_SUBMIT = 'N' OR EXISTS (SELECT 1 
										FROM CZ_SA_GIRH_WORK_DETAIL WD
										JOIN SA_GIRL GL ON GL.CD_COMPANY = WD.CD_COMPANY AND GL.NO_GIR = WD.NO_GIR
										WHERE WD.YN_AUTO_SUBMIT = 'Y'
										AND GL.CD_COMPANY = SL.CD_COMPANY
										AND GL.NO_SO = SL.NO_SO
										AND GL.SEQ_SO = SL.SEQ_SO))
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
ORDER BY QL.NO_DSP

GO

