USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SOSCH2H_S2]    Script Date: 2018-07-23 오후 1:03:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOSCH2H_S2] 
(
	@P_CD_COMPANY		NVARCHAR(7),
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
	@P_YN_EX_FREE		NVARCHAR(1) = 'N',
	@P_YN_EX_READY		NVARCHAR(1) = 'N',
	@P_YN_EX_FORWARD	NVARCHAR(1) = 'N'
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
	AND (@P_NO_SO <> '' OR SH.DT_SO BETWEEN @P_DT_SO_FROM AND @P_DT_SO_TO) 
	AND (@P_NO_SO = '' OR SH.NO_SO LIKE @P_NO_SO + '%')
	AND (@P_NO_SO_PRE = '' OR SH.NO_SO LIKE @P_NO_SO_PRE + '%')
	AND (@P_TP_SO = '' OR SH.TP_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_TP_SO)))
	AND (@P_CD_PARTNER = '' OR SH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
	AND (@P_CD_PARTNER_GRP = '' OR SH.CD_PARTNER_GRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP)))
	AND (@P_NO_EMP_MULTI = '' OR SH.NO_EMP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_NO_EMP_MULTI)))
	AND (@P_NO_IMO = '' OR SH.NO_IMO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_NO_IMO)))
	AND (@P_STA_SO = '' OR SH.STA_SO = @P_STA_SO)
	AND (@P_MULTI_SALEGRP = '' OR SH.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEGRP)))
	AND (@P_NO_PO_PARTNER = '' OR SH.NO_PO_PARTNER LIKE '%' + @P_NO_PO_PARTNER + '%')
	AND (@P_YN_EX_FORWARD = 'N' OR ISNULL(SH.CD_GIR_MAIN, '') <> 'WM001')
	AND (@P_YN_EX_READY = 'N' OR NOT EXISTS (SELECT 1 
										     FROM CZ_SA_AUTO_MAIL_PARTNER AM
										     WHERE AM.CD_COMPANY = SH.CD_COMPANY
										     AND AM.CD_PARTNER = SH.CD_PARTNER
										     AND AM.TP_PARTNER = '002'
										     AND AM.YN_READY_INFO = 'Y'))
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
		   PH.DT_IO,
	   	   SUM(CASE WHEN SH.RET = 'Y' THEN -SL.AM_WONAMT ELSE SL.AM_WONAMT END) AS AM_SO,
		   SUM(CASE WHEN SH.RET = 'Y' THEN -SL.QT_SO ELSE SL.QT_SO END) AS QT_SO,
	   	   SUM(ISNULL(SL.QT_PO, 0) + ISNULL(SB.QT_STOCK, 0)) AS QT_PO,
	   	   SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM, 0) 
		   								 ELSE (ISNULL(QI.QT_IO, 0) + ISNULL(SB.QT_BOOK, 0)) END) AS QT_IN,
	   	   SUM(GL.QT_GIR) AS QT_GIR,
	   	   SUM(GL.QT_IO) AS QT_OUT,
	   	   SUM(QO.QT_IV) AS QT_IV
    FROM SA_SOH1 SH
    JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
    LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
    LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
	LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE, OH.DT_IO
    		   FROM PU_POL PL
    		   LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = PL.CD_COMPANY AND OL.NO_PSO_MGMT = PL.NO_PO AND OL.NO_PSOLINE_MGMT = PL.NO_LINE
    		   LEFT JOIN MM_QTIOH OH ON OH.CD_COMPANY = OL.CD_COMPANY AND OH.NO_IO = OL.NO_IO
			   WHERE (OH.YN_RETURN IS NULL OR OH.YN_RETURN = 'N')
    		   GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE, OH.DT_IO) PH 
    ON PH.CD_COMPANY = SL.CD_COMPANY AND PH.NO_SO = SL.NO_SO AND PH.NO_SOLINE = SL.SEQ_SO
    LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO,
    	 			  SUM(GL.QT_GIR) AS QT_GIR,
    	 			  SUM(OL.QT_IO) AS QT_IO
    		   FROM SA_GIRL GL
    		   LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
    		   GROUP BY GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO) GL 
    ON GL.CD_COMPANY = SL.CD_COMPANY AND GL.NO_SO = SL.NO_SO AND GL.SEQ_SO = SL.SEQ_SO
    LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
    		   		  SUM(OL.QT_IO) AS QT_IO
    		   FROM PU_POL PL
    		   LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = PL.CD_COMPANY AND OL.NO_PSO_MGMT = PL.NO_PO AND OL.NO_PSOLINE_MGMT = PL.NO_LINE
    		   LEFT JOIN MM_QTIOH OH ON OH.CD_COMPANY = OL.CD_COMPANY AND OH.NO_IO = OL.NO_IO
			   WHERE (OH.YN_RETURN IS NULL OR OH.YN_RETURN = 'N')
    		   GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) QI 
    ON QI.CD_COMPANY = SL.CD_COMPANY AND QI.NO_SO = SL.NO_SO AND QI.NO_SOLINE = SL.SEQ_SO
    LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
	  		   		  SUM(CASE WHEN OL.FG_PS = '1' AND OL.FG_IO = '002' THEN OL.QT_IO ELSE 0 END) AS QT_BOM,
			   		  SUM(IL.QT_CLS) AS QT_IV
	  		   FROM MM_QTIOH OH
	  		   LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
			   LEFT JOIN SA_IVL IL ON IL.CD_COMPANY = OL.CD_COMPANY AND IL.NO_IO = OL.NO_IO AND IL.NO_IOLINE = OL.NO_IOLINE AND IL.CD_ITEM = OL.CD_ITEM
			   WHERE (OH.YN_RETURN IS NULL OR OH.YN_RETURN = 'N')
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
    GROUP BY SH.CD_COMPANY, SH.NO_SO, PH.DT_IO
)
SELECT SL.DT_IO AS DT_IN,
	   ISNULL(COUNT(DISTINCT SL.NO_SO), 0) AS CNT_SO,
	   ISNULL(SUM(SL.AM_SO), 0) AS AM_SO,
	   ISNULL(SUM(SL.QT_SO), 0) AS QT_SO
FROM SA_SOL1 SL
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
GROUP BY SL.DT_IO
ORDER BY SL.DT_IO DESC

GO

