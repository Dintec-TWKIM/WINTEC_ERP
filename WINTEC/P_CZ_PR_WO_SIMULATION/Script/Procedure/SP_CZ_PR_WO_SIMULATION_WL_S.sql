USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WO_SIMULATION_WL_S]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_SIMULATION_WL_S]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_SO			NVARCHAR(20),
	@P_CD_ITEM			NVARCHAR(20),
	@P_DT_FROM			NVARCHAR(8),
	@P_DT_TO			NVARCHAR(8),
	@P_YN_GIR			NVARCHAR(1),
	@P_YN_GI			NVARCHAR(1),
	@P_YN_CLOSE			NVARCHAR(1),
	@P_YN_WO		    NVARCHAR(1),
	@P_YN_PR			NVARCHAR(1),
	@P_YN_SU_PR			NVARCHAR(1),
	@P_YN_STOCK			NVARCHAR(1),
	@P_TP_PROC			NVARCHAR(1),
	@P_NO_OPPATH		NVARCHAR(3)
)  
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

CREATE TABLE #BOM
(
	CD_COMPANY	NVARCHAR(7), 
	CD_PLANT	NVARCHAR(7), 
	CD_ITEM_T	NVARCHAR(20), 
	CD_ITEM		NVARCHAR(20), 
	CD_MATL		NVARCHAR(20), 
	LEVEL		INT, 
	PATH		NVARCHAR(100), 
	QT			NUMERIC(25, 10)
)

CREATE NONCLUSTERED INDEX BOM ON #BOM (CD_COMPANY, CD_PLANT, CD_ITEM_T);

WITH BOM 
(
	CD_COMPANY, 
	CD_PLANT, 
	CD_ITEM_T, 
	CD_ITEM, 
	CD_MATL, 
	LEVEL, 
	PATH, 
	QT
) 
AS
(
	SELECT PB.CD_COMPANY,
		   PB.CD_PLANT,
		   PB.CD_ITEM AS CD_ITEM_T,
		   PB.CD_ITEM, 
		   PB.CD_MATL, 
		   LEVEL = 1, 
		   CONVERT(VARCHAR(1000), PB.CD_MATL),
		   PB1.QT_ITEM_NET
    FROM PR_ROUT_ASN PB
	JOIN PR_BOM PB1 ON PB1.CD_COMPANY = PB.CD_COMPANY AND PB1.CD_PLANT = PB.CD_PLANT AND PB1.CD_ITEM = PB.CD_ITEM AND PB1.CD_MATL = PB.CD_MATL AND PB1.DT_END >= CONVERT(CHAR(8), GETDATE(), 112)
	WHERE PB.CD_COMPANY = @P_CD_COMPANY
	AND PB.NO_OPPATH = @P_NO_OPPATH
	AND EXISTS (SELECT 1 
				FROM MA_PITEM MI 
				WHERE MI.CD_COMPANY = PB.CD_COMPANY 
				AND MI.CD_PLANT = PB.CD_PLANT 
				AND MI.CD_ITEM = PB.CD_MATL
				AND MI.TP_PROC IN ('M', 'P', 'S')
				AND ISNULL(MI.YN_USE, 'N') = 'Y')
	AND EXISTS (SELECT 1 
		        FROM PR_ROUT_L RL
		        WHERE RL.CD_COMPANY = PB.CD_COMPANY
		        AND RL.CD_PLANT = PB.CD_PLANT
		        AND RL.CD_ITEM = PB.CD_ITEM
		        AND RL.NO_OPPATH = PB.NO_OPPATH
		        AND RL.TP_OPPATH = PB.TP_OPPATH
		        AND RL.CD_OP = PB.CD_OP
			    AND RL.YN_USE = 'Y')
	UNION ALL
	SELECT BM.CD_COMPANY,
		   BM.CD_PLANT,
		   BM.CD_ITEM_T,
		   PB.CD_ITEM, 
		   PB.CD_MATL, 
		   LEVEL + 1, 
		   CONVERT(VARCHAR(1000), PATH + ' -> ' + PB.CD_MATL),
		   PB1.QT_ITEM_NET
	FROM BOM BM
	JOIN PR_ROUT_ASN PB ON PB.CD_ITEM = BM.CD_MATL AND PB.NO_OPPATH = @P_NO_OPPATH
	JOIN PR_BOM PB1 ON PB1.CD_COMPANY = PB.CD_COMPANY AND PB1.CD_PLANT = PB.CD_PLANT AND PB1.CD_ITEM = PB.CD_ITEM AND PB1.CD_MATL = PB.CD_MATL AND PB1.DT_END >= CONVERT(CHAR(8), GETDATE(), 112)
	WHERE EXISTS (SELECT 1 
				  FROM MA_PITEM MI 
				  WHERE MI.CD_COMPANY = PB.CD_COMPANY 
				  AND MI.CD_PLANT = PB.CD_PLANT 
				  AND MI.CD_ITEM = PB.CD_MATL
				  AND MI.TP_PROC IN ('M', 'P', 'S')
				  AND ISNULL(MI.YN_USE, 'N') = 'Y')
	AND EXISTS (SELECT 1 
		        FROM PR_ROUT_L RL
		        WHERE RL.CD_COMPANY = PB.CD_COMPANY
		        AND RL.CD_PLANT = PB.CD_PLANT
		        AND RL.CD_ITEM = PB.CD_ITEM
		        AND RL.NO_OPPATH = PB.NO_OPPATH
		        AND RL.TP_OPPATH = PB.TP_OPPATH
		        AND RL.CD_OP = PB.CD_OP
			    AND RL.YN_USE = 'Y')
)
INSERT INTO #BOM
SELECT CD_COMPANY, 
	   CD_PLANT, 
	   CD_ITEM_T, 
	   CD_ITEM, 
	   CD_MATL, 
	   LEVEL, 
	   PATH, 
	   QT 
FROM BOM;

WITH A AS
(
	SELECT SH.CD_COMPANY, -- 해당 수주의 모품목
		   SH.NO_SO,
		   SL.SEQ_SO,
		   SL.CD_ITEM,
		   SL.CD_PLANT,
		   SL.QT_SO,
		   SL.QT_GIR,
		   SL.QT_GI,
		   WS.QT_APPLY,
		   PL.QT_PR,
		   SR.QT_SU_PR,
		   ST.QT_STOCK,
		   SL.CD_ITEM AS CD_MATL,
	       0 AS LEVEL,
		   1 AS QT_BOM,
		   SL.QT_SO AS QT_NEED,
		   WS.NO_WO,
		   WS.DT_REL,
		   WS.ST_WO,
		   WS.FG_AUTO,
		   PL.NO_PR,
		   PL.ST_PRST,
		   SR.NO_SU_PR,
		   SR.ST_PROC,
		   ST.NO_GIREQ
	FROM SA_SOH SH
	JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	LEFT JOIN (SELECT WS.CD_COMPANY, WS.NO_SO, WS.NO_LINE, WS.CD_ITEM,
					  SUM(WS.QT_APPLY) AS QT_APPLY,
					  MAX(WO.NO_WO) AS NO_WO,
					  MAX(WO.DT_REL) AS DT_REL, 
				      MAX(WO.ST_WO) AS ST_WO,
				      MAX(TW.FG_AUTO) AS FG_AUTO
			   FROM CZ_PR_SA_SOL_PR_WO_MAPPING WS
			   JOIN PR_WO WO ON WO.CD_COMPANY = WS.CD_COMPANY AND WO.NO_WO = WS.NO_WO
			   LEFT JOIN PR_TPWO TW ON TW.CD_COMPANY = WO.CD_COMPANY AND TW.TP_WO = WO.TP_ROUT
			   GROUP BY WS.CD_COMPANY, WS.NO_SO, WS.NO_LINE, WS.CD_ITEM) WS
	ON WS.CD_COMPANY = SL.CD_COMPANY AND WS.NO_SO = SL.NO_SO AND WS.NO_LINE = SL.SEQ_SO AND WS.CD_ITEM = SL.CD_ITEM
	LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.CD_ITEM,
					  SUM(PL.QT_PR) AS QT_PR,
				      MAX(PL.NO_PR) AS NO_PR,
					  MAX(PL1.ST_PRST) AS ST_PRST
			   FROM CZ_PR_SA_SOL_PU_PRL_MAPPING PL
			   JOIN PU_PRL PL1 ON PL1.CD_COMPANY = PL.CD_COMPANY AND PL1.NO_PR = PL.NO_PR AND PL1.NO_PRLINE = PL.NO_PRLINE
			   GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.CD_ITEM) PL 
	ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_LINE = SL.SEQ_SO AND PL.CD_ITEM = SL.CD_ITEM
	LEFT JOIN (SELECT SR.CD_COMPANY, SR.NO_SO, SR.NO_LINE, SR.CD_ITEM,
		   			  SUM(SR.QT_PR) AS QT_SU_PR,
				      MAX(SR.NO_PR) AS NO_SU_PR,
					  MAX(SR1.ST_PROC) AS ST_PROC
			   FROM CZ_PR_SA_SOL_SU_PRL_MAPPING SR
			   JOIN SU_PRL SR1 ON SR1.CD_COMPANY = SR.CD_COMPANY AND SR1.NO_PR = SR.NO_PR AND SR1.NO_LINE = SR.NO_PRLINE
			   GROUP BY SR.CD_COMPANY, SR.NO_SO, SR.NO_LINE, SR.CD_ITEM) SR 
	ON SR.CD_COMPANY = SL.CD_COMPANY AND SR.NO_SO = SL.NO_SO AND SR.NO_LINE = SL.SEQ_SO AND SR.CD_ITEM = SL.CD_ITEM
	LEFT JOIN (SELECT QH.CD_COMPANY, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM, 
    		   	      SUM(QL.QT_GI) AS QT_STOCK,
					  MAX(QH.NO_GIREQ) AS NO_GIREQ
    		   FROM MM_GIREQH QH
    		   JOIN MM_GIREQL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_GIREQ = QH.NO_GIREQ
    		   WHERE QL.CD_SL = 'SL_STND'
    		   GROUP BY QH.CD_COMPANY, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM) ST
    ON ST.CD_COMPANY = SL.CD_COMPANY AND ST.NO_SO = SL.NO_SO AND ST.SEQ_SO = SL.SEQ_SO AND ST.CD_ITEM = SL.CD_ITEM
	WHERE SH.CD_COMPANY = @P_CD_COMPANY
	AND SH.NO_SO = @P_NO_SO
	AND SL.CD_ITEM = @P_CD_ITEM
	AND SL.DT_DUEDATE BETWEEN @P_DT_FROM AND @P_DT_TO
	AND (@P_YN_GIR = 'N' OR SL.QT_SO > ISNULL(SL.QT_GIR, 0))
	AND (@P_YN_GI = 'N' OR SL.QT_SO > ISNULL(SL.QT_GI, 0))
	AND (@P_YN_CLOSE = 'N' OR ISNULL(SH.STA_SO, '') <> 'C')
	AND (@P_YN_STOCK = 'N' OR SL.QT_SO > ISNULL(ST.QT_STOCK, 0))
	AND (@P_YN_WO = 'N' OR (ISNULL(SL.QT_SO, 0) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(WS.QT_APPLY, 0))
	AND (@P_YN_PR = 'N' OR (ISNULL(SL.QT_SO, 0) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(PL.QT_PR, 0))
	AND (@P_YN_SU_PR = 'N' OR (ISNULL(SL.QT_SO, 0) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(SR.QT_SU_PR, 0))
	UNION ALL
	SELECT SH.CD_COMPANY, -- 해당 수주의 자품목
		   SH.NO_SO,
		   SL.SEQ_SO,
		   SL.CD_ITEM,
		   SL.CD_PLANT,
		   SL.QT_SO,
		   SL.QT_GIR,
		   SL.QT_GI,
		   WS.QT_APPLY,
		   PL.QT_PR,
		   SR.QT_SU_PR,
		   ST.QT_STOCK,
		   BM.CD_MATL,
	       BM.LEVEL,
		   BM.QT AS QT_BOM,
		   (BM.QT * SL.QT_SO) AS QT_NEED,
		   WS.NO_WO,
		   WS.DT_REL,
		   WS.ST_WO,
		   WS.FG_AUTO,
		   PL.NO_PR,
		   PL.ST_PRST,
		   SR.NO_SU_PR,
		   SR.ST_PROC,
		   ST.NO_GIREQ
	FROM SA_SOH SH
	JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	JOIN #BOM BM ON BM.CD_COMPANY = SL.CD_COMPANY AND BM.CD_PLANT = SL.CD_PLANT AND BM.CD_ITEM_T = SL.CD_ITEM
	LEFT JOIN (SELECT WS.CD_COMPANY, WS.NO_SO, WS.NO_LINE, WS.CD_ITEM,
					  SUM(WS.QT_APPLY) AS QT_APPLY,
					  MAX(WO.NO_WO) AS NO_WO,
					  MAX(WO.DT_REL) AS DT_REL,
				      MAX(WO.ST_WO) AS ST_WO,
				      MAX(TW.FG_AUTO) AS FG_AUTO
			   FROM CZ_PR_SA_SOL_PR_WO_MAPPING WS
			   JOIN PR_WO WO ON WO.CD_COMPANY = WS.CD_COMPANY AND WO.NO_WO = WS.NO_WO
			   LEFT JOIN PR_TPWO TW ON TW.CD_COMPANY = WO.CD_COMPANY AND TW.TP_WO = WO.TP_ROUT
			   GROUP BY WS.CD_COMPANY, WS.NO_SO, WS.NO_LINE, WS.CD_ITEM) WS
	ON WS.CD_COMPANY = SL.CD_COMPANY AND WS.NO_SO = SL.NO_SO AND WS.NO_LINE = SL.SEQ_SO AND WS.CD_ITEM = BM.CD_MATL
	LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.CD_ITEM,
					  SUM(PL.QT_PR) AS QT_PR,
				      MAX(PL.NO_PR) AS NO_PR,
					  MAX(PL1.ST_PRST) AS ST_PRST
			   FROM CZ_PR_SA_SOL_PU_PRL_MAPPING PL
			   JOIN PU_PRL PL1 ON PL1.CD_COMPANY = PL.CD_COMPANY AND PL1.NO_PR = PL.NO_PR AND PL1.NO_PRLINE = PL.NO_PRLINE
			   GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.CD_ITEM) PL 
	ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_LINE = SL.SEQ_SO AND PL.CD_ITEM = BM.CD_MATL
	LEFT JOIN (SELECT SR.CD_COMPANY, SR.NO_SO, SR.NO_LINE, SR.CD_ITEM,
		   			  SUM(SR.QT_PR) AS QT_SU_PR,
				      MAX(SR.NO_PR) AS NO_SU_PR,
					  MAX(SR1.ST_PROC) AS ST_PROC
			   FROM CZ_PR_SA_SOL_SU_PRL_MAPPING SR
			   JOIN SU_PRL SR1 ON SR1.CD_COMPANY = SR.CD_COMPANY AND SR1.NO_PR = SR.NO_PR AND SR1.NO_LINE = SR.NO_PRLINE
			   GROUP BY SR.CD_COMPANY, SR.NO_SO, SR.NO_LINE, SR.CD_ITEM) SR 
	ON SR.CD_COMPANY = SL.CD_COMPANY AND SR.NO_SO = SL.NO_SO AND SR.NO_LINE = SL.SEQ_SO AND SR.CD_ITEM = BM.CD_MATL
	LEFT JOIN (SELECT QH.CD_COMPANY, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM, 
    		   	      SUM(QL.QT_GI) AS QT_STOCK,
					  MAX(QH.NO_GIREQ) AS NO_GIREQ
    		   FROM MM_GIREQH QH
    		   JOIN MM_GIREQL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_GIREQ = QH.NO_GIREQ
    		   WHERE QL.CD_SL = 'SL_STND'
    		   GROUP BY QH.CD_COMPANY, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM) ST
    ON ST.CD_COMPANY = SL.CD_COMPANY AND ST.NO_SO = SL.NO_SO AND ST.SEQ_SO = SL.SEQ_SO AND ST.CD_ITEM = BM.CD_MATL
	WHERE SH.CD_COMPANY = @P_CD_COMPANY
	AND SH.NO_SO = @P_NO_SO
	AND SL.CD_ITEM = @P_CD_ITEM
	AND SL.DT_DUEDATE BETWEEN @P_DT_FROM AND @P_DT_TO
	AND (@P_YN_GIR = 'N' OR SL.QT_SO > ISNULL(SL.QT_GIR, 0))
	AND (@P_YN_GI = 'N' OR SL.QT_SO > ISNULL(SL.QT_GI, 0))
	AND (@P_YN_CLOSE = 'N' OR ISNULL(SH.STA_SO, '') <> 'C')
	AND (@P_YN_STOCK = 'N' OR (BM.QT * SL.QT_SO) > ISNULL(ST.QT_STOCK, 0))
	AND (@P_YN_WO = 'N' OR ((BM.QT * SL.QT_SO) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(WS.QT_APPLY, 0))
	AND (@P_YN_PR = 'N' OR ((BM.QT * SL.QT_SO) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(PL.QT_PR, 0))
	AND (@P_YN_SU_PR = 'N' OR ((BM.QT * SL.QT_SO) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(SR.QT_SU_PR, 0))
),
B AS
(
	SELECT SH.CD_COMPANY, -- 타 수주의 모품목
		   A.NO_SO,
		   A.SEQ_SO,
		   A.CD_ITEM,
		   A.CD_PLANT,
		   SL.QT_SO,
		   SL.QT_GIR,
		   SL.QT_GI,
		   WS.QT_APPLY,
		   PL.QT_PR,
		   SR.QT_SU_PR,
		   ST.QT_STOCK,
		   SL.CD_ITEM AS CD_MATL,
	       A.LEVEL,
		   1 AS QT_BOM,
		   SL.QT_SO AS QT_NEED,
		   NULL AS NO_WO,
		   NULL AS DT_REL,
		   NULL AS ST_WO,
		   NULL AS FG_AUTO,
		   NULL AS NO_PR,
		   NULL AS ST_PRST,
		   NULL AS NO_SU_PR,
		   NULL AS ST_PROC,
		   NULL AS NO_GIREQ
	FROM SA_SOH SH
	JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	LEFT JOIN (SELECT WS.CD_COMPANY, WS.NO_SO, WS.NO_LINE, WS.CD_ITEM,
					  SUM(WS.QT_APPLY) AS QT_APPLY
			   FROM CZ_PR_SA_SOL_PR_WO_MAPPING WS
			   JOIN PR_WO WO ON WO.CD_COMPANY = WS.CD_COMPANY AND WO.NO_WO = WS.NO_WO
			   GROUP BY WS.CD_COMPANY, WS.NO_SO, WS.NO_LINE, WS.CD_ITEM) WS
	ON WS.CD_COMPANY = SL.CD_COMPANY AND WS.NO_SO = SL.NO_SO AND WS.NO_LINE = SL.SEQ_SO AND WS.CD_ITEM = SL.CD_ITEM
	LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.CD_ITEM,
					  SUM(PL.QT_PR) AS QT_PR
			   FROM CZ_PR_SA_SOL_PU_PRL_MAPPING PL
			   JOIN PU_PRL PL1 ON PL1.CD_COMPANY = PL.CD_COMPANY AND PL1.NO_PR = PL.NO_PR AND PL1.NO_PRLINE = PL.NO_PRLINE
			   GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.CD_ITEM) PL 
	ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_LINE = SL.SEQ_SO AND PL.CD_ITEM = SL.CD_ITEM
	LEFT JOIN (SELECT SR.CD_COMPANY, SR.NO_SO, SR.NO_LINE, SR.CD_ITEM,
		   			  SUM(SR.QT_PR) AS QT_SU_PR
			   FROM CZ_PR_SA_SOL_SU_PRL_MAPPING SR
			   JOIN SU_PRL SR1 ON SR1.CD_COMPANY = SR.CD_COMPANY AND SR1.NO_PR = SR.NO_PR AND SR1.NO_LINE = SR.NO_PRLINE
			   GROUP BY SR.CD_COMPANY, SR.NO_SO, SR.NO_LINE, SR.CD_ITEM) SR 
	ON SR.CD_COMPANY = SL.CD_COMPANY AND SR.NO_SO = SL.NO_SO AND SR.NO_LINE = SL.SEQ_SO AND SR.CD_ITEM = SL.CD_ITEM
	LEFT JOIN (SELECT QH.CD_COMPANY, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM, 
    		   	      SUM(QL.QT_GI) AS QT_STOCK
    		   FROM MM_GIREQH QH
    		   JOIN MM_GIREQL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_GIREQ = QH.NO_GIREQ
    		   WHERE QL.CD_SL = 'SL_STND'
    		   GROUP BY QH.CD_COMPANY, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM) ST
    ON ST.CD_COMPANY = SL.CD_COMPANY AND ST.NO_SO = SL.NO_SO AND ST.SEQ_SO = SL.SEQ_SO AND ST.CD_ITEM = SL.CD_ITEM
	JOIN A ON A.CD_COMPANY = SL.CD_COMPANY AND A.CD_MATL = SL.CD_ITEM
	WHERE SH.CD_COMPANY = @P_CD_COMPANY
	AND SH.NO_SO <> @P_NO_SO
	AND SL.DT_DUEDATE BETWEEN @P_DT_FROM AND @P_DT_TO
	AND (@P_YN_GIR = 'N' OR SL.QT_SO > ISNULL(SL.QT_GIR, 0))
	AND (@P_YN_GI = 'N' OR SL.QT_SO > ISNULL(SL.QT_GI, 0))
	AND (@P_YN_CLOSE = 'N' OR ISNULL(SH.STA_SO, '') <> 'C')
	AND (@P_YN_STOCK = 'N' OR SL.QT_SO > ISNULL(ST.QT_STOCK, 0))
	AND (@P_YN_WO = 'N' OR (ISNULL(SL.QT_SO, 0) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(WS.QT_APPLY, 0))
	AND (@P_YN_PR = 'N' OR (ISNULL(SL.QT_SO, 0) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(PL.QT_PR, 0))
	AND (@P_YN_SU_PR = 'N' OR (ISNULL(SL.QT_SO, 0) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(SR.QT_SU_PR, 0))
	UNION ALL
	SELECT SH.CD_COMPANY, -- 타 수주의 자품목
		   A.NO_SO,
		   0 AS SEQ_SO,
		   A.CD_ITEM,
		   A.CD_PLANT,
		   SL.QT_SO,
		   SL.QT_GIR,
		   SL.QT_GI,
		   WS.QT_APPLY,
		   PL.QT_PR,
		   SR.QT_SU_PR,
		   ST.QT_STOCK,
		   BM.CD_MATL,
	       A.LEVEL,
		   BM.QT AS QT_BOM,
		   (BM.QT * SL.QT_SO) AS QT_NEED,
		   NULL AS NO_WO,
		   NULL AS DT_REL,
		   NULL AS ST_WO,
		   NULL AS FG_AUTO,
		   NULL AS NO_PR,
		   NULL AS ST_PRST,
		   NULL AS NO_SU_PR,
		   NULL AS ST_PROC,
		   NULL AS NO_GIREQ
	FROM SA_SOH SH
	JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	JOIN #BOM BM ON BM.CD_COMPANY = SL.CD_COMPANY AND BM.CD_PLANT = SL.CD_PLANT AND BM.CD_ITEM_T = SL.CD_ITEM
	LEFT JOIN (SELECT WS.CD_COMPANY, WS.NO_SO, WS.NO_LINE, WS.CD_ITEM,
					  SUM(WS.QT_APPLY) AS QT_APPLY
			   FROM CZ_PR_SA_SOL_PR_WO_MAPPING WS
			   JOIN PR_WO WO ON WO.CD_COMPANY = WS.CD_COMPANY AND WO.NO_WO = WS.NO_WO
			   GROUP BY WS.CD_COMPANY, WS.NO_SO, WS.NO_LINE, WS.CD_ITEM) WS
	ON WS.CD_COMPANY = SL.CD_COMPANY AND WS.NO_SO = SL.NO_SO AND WS.NO_LINE = SL.SEQ_SO AND WS.CD_ITEM = BM.CD_MATL
	LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.CD_ITEM,
					  SUM(PL.QT_PR) AS QT_PR
			   FROM CZ_PR_SA_SOL_PU_PRL_MAPPING PL
			   JOIN PU_PRL PL1 ON PL1.CD_COMPANY = PL.CD_COMPANY AND PL1.NO_PR = PL.NO_PR AND PL1.NO_PRLINE = PL.NO_PRLINE
			   GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.CD_ITEM) PL 
	ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_LINE = SL.SEQ_SO AND PL.CD_ITEM = BM.CD_MATL
	LEFT JOIN (SELECT SR.CD_COMPANY, SR.NO_SO, SR.NO_LINE, SR.CD_ITEM,
		   			  SUM(SR.QT_PR) AS QT_SU_PR
			   FROM CZ_PR_SA_SOL_SU_PRL_MAPPING SR
			   JOIN SU_PRL SR1 ON SR1.CD_COMPANY = SR.CD_COMPANY AND SR1.NO_PR = SR.NO_PR AND SR1.NO_LINE = SR.NO_PRLINE
			   GROUP BY SR.CD_COMPANY, SR.NO_SO, SR.NO_LINE, SR.CD_ITEM) SR 
	ON SR.CD_COMPANY = SL.CD_COMPANY AND SR.NO_SO = SL.NO_SO AND SR.NO_LINE = SL.SEQ_SO AND SR.CD_ITEM = BM.CD_MATL
	LEFT JOIN (SELECT QH.CD_COMPANY, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM, 
    		   	      SUM(QL.QT_GI) AS QT_STOCK
    		   FROM MM_GIREQH QH
    		   JOIN MM_GIREQL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_GIREQ = QH.NO_GIREQ
    		   WHERE QL.CD_SL = 'SL_STND'
    		   GROUP BY QH.CD_COMPANY, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM) ST
    ON ST.CD_COMPANY = SL.CD_COMPANY AND ST.NO_SO = SL.NO_SO AND ST.SEQ_SO = SL.SEQ_SO AND ST.CD_ITEM = BM.CD_MATL
	JOIN A ON A.CD_COMPANY = BM.CD_COMPANY AND A.CD_MATL = BM.CD_MATL
	WHERE SH.CD_COMPANY = @P_CD_COMPANY
	AND SH.NO_SO <> @P_NO_SO
	AND SL.DT_DUEDATE BETWEEN @P_DT_FROM AND @P_DT_TO
	AND (@P_YN_GIR = 'N' OR SL.QT_SO > ISNULL(SL.QT_GIR, 0))
	AND (@P_YN_GI = 'N' OR SL.QT_SO > ISNULL(SL.QT_GI, 0))
	AND (@P_YN_CLOSE = 'N' OR ISNULL(SH.STA_SO, '') <> 'C')
	AND (@P_YN_STOCK = 'N' OR (BM.QT * SL.QT_SO) > ISNULL(ST.QT_STOCK, 0))
	AND (@P_YN_WO = 'N' OR ((BM.QT * SL.QT_SO) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(WS.QT_APPLY, 0))
	AND (@P_YN_PR = 'N' OR ((BM.QT * SL.QT_SO) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(PL.QT_PR, 0))
	AND (@P_YN_SU_PR = 'N' OR ((BM.QT * SL.QT_SO) - (CASE WHEN @P_YN_STOCK = 'Y' THEN ISNULL(ST.QT_STOCK, 0) ELSE 0 END)) > ISNULL(SR.QT_SU_PR, 0))
),
C AS
(
	SELECT * FROM A
	UNION ALL
	SELECT * FROM B
),
D AS
(
	SELECT C.CD_COMPANY,
		   C.NO_SO,
		   MIN(CASE WHEN C.SEQ_SO > 0 THEN C.SEQ_SO ELSE NULL END) AS SEQ_SO,
		   C.CD_ITEM,
		   C.CD_PLANT,
		   C.CD_MATL,
		   C.LEVEL,
		   SUM(C.QT_SO) AS QT_SO,
		   SUM(C.QT_APPLY) AS QT_APPLY,
		   SUM(C.QT_PR) AS QT_PR,
		   SUM(C.QT_SU_PR) AS QT_SU_PR,
		   SUM(C.QT_STOCK) AS QT_STOCK,
		   SUM(C.QT_GIR) AS QT_GIR,
		   SUM(C.QT_GI) AS QT_GI,
	       MAX(C.QT_BOM) AS QT_BOM,
		   SUM(C.QT_NEED) AS QT_NEED,
		   MAX(C.NO_WO) AS NO_WO,
		   MAX(C.DT_REL) AS DT_REL,
		   MAX(C.ST_WO) AS ST_WO,
		   MAX(C.FG_AUTO) AS FG_AUTO,
		   MAX(C.NO_PR) AS NO_PR,
		   MAX(C.ST_PRST) AS ST_PRST,
		   MAX(C.NO_SU_PR) AS NO_SU_PR,
		   MAX(C.ST_PROC) AS ST_PROC,
		   MAX(C.NO_GIREQ) AS NO_GIREQ
	FROM C
	GROUP BY C.CD_COMPANY, C.NO_SO, C.CD_ITEM, C.CD_PLANT, C.CD_MATL, C.LEVEL
)
SELECT 'N' AS S,
	   D.NO_SO,
	   D.SEQ_SO,
	   D.CD_ITEM,
	   D.CD_MATL,
	   MI.NM_ITEM,
	   MI.TP_PROC,
	   MI.NO_DESIGN,
	   MI.CD_SL,
	   MC.NM_SYSDEF AS NM_TP_PROC,
	   D.LEVEL,
	   D.QT_SO,
	   ISNULL(D.QT_APPLY, 0) AS QT_APPLY,
	   ISNULL(D.QT_PR, 0) AS QT_PR,
	   ISNULL(D.QT_SU_PR, 0) AS QT_SU_PR,
	   ISNULL(D.QT_STOCK, 0) AS QT_STOCK,
	   ISNULL(D.QT_GIR, 0) AS QT_GIR,
	   ISNULL(D.QT_GI, 0) AS QT_GI,
	   D.QT_BOM,
	   D.QT_NEED,
	   ISNULL(WM.QT_REMAIN, 0) AS QT_REMAIN,
	   ISNULL(INV.QT_INV, 0) AS QT_INV,
	   D.CD_PLANT,
	   D.NO_WO,
	   D.DT_REL,
	   D.ST_WO,
	   D.FG_AUTO,
	   D.NO_PR,
	   D.ST_PRST,
	   D.NO_SU_PR,
	   D.ST_PROC,
	   D.NO_GIREQ,
	   MI.WEIGHT,
	   MI.STND_ITEM,
	   WR.NM_OP,
	   0 AS QT_ADD,
	   (CASE WHEN EXISTS (SELECT 1 
						  FROM PR_ROUT_L RL
						  JOIN PR_ROUT_ASN RA ON RA.CD_COMPANY = RL.CD_COMPANY AND RA.CD_PLANT = RL.CD_PLANT AND RA.CD_ITEM = RL.CD_ITEM AND RA.NO_OPPATH = RL.NO_OPPATH AND RA.TP_OPPATH = RL.TP_OPPATH AND RA.CD_OP = RL.CD_OP
						  WHERE RL.CD_COMPANY = D.CD_COMPANY
						  AND RL.CD_PLANT = D.CD_PLANT
						  AND RL.NO_OPPATH = @P_NO_OPPATH
						  AND RL.CD_ITEM = D.CD_MATL
						  AND RL.YN_USE = 'Y') THEN 'Y' ELSE 'N' END) AS YN_ASN,
	   (CASE WHEN EXISTS (SELECT 1 
						  FROM CZ_PR_WO_REQ_D WD
						  WHERE WD.CD_COMPANY = D.CD_COMPANY
						  AND WD.NO_WO = D.NO_WO) THEN 'Y' ELSE 'N' END) AS YN_INPUT,
	   (CASE WHEN MI.TP_PROC = 'M' THEN MI.UNIT_MO 
			 WHEN MI.TP_PROC = 'P' THEN MI.UNIT_PO
			 WHEN MI.TP_PROC = 'S' THEN MI.UNIT_SU
			 ELSE MI.UNIT_SO END) AS NM_UNIT,
	   (CASE WHEN MI.TP_PROC = 'M' THEN ISNULL(NULLIF(MI.UNIT_MO_FACT, 0), 1) 
			 WHEN MI.TP_PROC = 'P' THEN ISNULL(NULLIF(MI.UNIT_PO_FACT, 0), 1) 
			 WHEN MI.TP_PROC = 'S' THEN ISNULL(NULLIF(MI.UNIT_SU_FACT, 0), 1) 
			 ELSE NULLIF(MI.UNIT_SO_FACT, 0) END) AS UNIT_FACT
FROM D
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = D.CD_COMPANY AND MI.CD_PLANT = D.CD_PLANT AND MI.CD_ITEM = D.CD_MATL
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = MI.CD_COMPANY AND MC.CD_FIELD = 'MA_B000009' AND MC.CD_SYSDEF = MI.TP_PROC
LEFT JOIN (SELECT WO.CD_COMPANY, WO.CD_ITEM,
		   	      SUM((ISNULL(WO.QT_ITEM, 0) - ISNULL(WR.QT_REJECT, 0)) - ISNULL(WM.QT_APPLY, 0)) AS QT_REMAIN
		   FROM PR_WO WO
		   LEFT JOIN (SELECT CD_COMPANY, NO_WO,
		   				     SUM(QT_APPLY) AS QT_APPLY
		   		      FROM CZ_PR_SA_SOL_PR_WO_MAPPING
		   		      GROUP BY CD_COMPANY, NO_WO) WM
		   ON WM.CD_COMPANY = WO.CD_COMPANY AND WM.NO_WO = WO.NO_WO
		   LEFT JOIN (SELECT WR.CD_COMPANY, WR.NO_WO,
							 SUM(WR.QT_REJECT) AS QT_REJECT
					  FROM PR_WO_ROUT WR
					  GROUP BY WR.CD_COMPANY, WR.NO_WO) WR
		   ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
		   WHERE WO.CD_COMPANY = @P_CD_COMPANY
		   AND ISNULL(WO.QT_ITEM, 0) > ISNULL(WM.QT_APPLY, 0)
		   GROUP BY WO.CD_COMPANY, WO.CD_ITEM) WM
ON WM.CD_COMPANY = D.CD_COMPANY AND WM.CD_ITEM = D.CD_MATL
LEFT JOIN (SELECT WR.CD_COMPANY,
		   		  WR.NO_WO,
		   	      WR.NM_OP
		   FROM (SELECT WR.CD_COMPANY, WR.NO_WO,
		   	  	        OP.NM_OP,
		   	  	        ROW_NUMBER() OVER (PARTITION BY WR.CD_COMPANY, WR.NO_WO ORDER BY WR.QT_WIP DESC) AS IDX
		   	     FROM PR_WO_ROUT WR
		   	     LEFT JOIN PR_WCOP OP ON OP.CD_COMPANY = WR.CD_COMPANY AND OP.CD_PLANT = WR.CD_PLANT AND OP.CD_WC = WR.CD_WC AND OP.CD_WCOP = WR.CD_WCOP) WR
		   WHERE WR.IDX = 1) WR
ON WR.CD_COMPANY = D.CD_COMPANY AND WR.NO_WO = D.NO_WO
LEFT JOIN (SELECT MY.CD_COMPANY, MY.CD_ITEM, 
		   	      (SUM(MY.QT_GOOD_OPEN + MY.QT_REJECT_OPEN + MY.QT_INSP_OPEN + MY.QT_TRANS_OPEN) 
		          + SUM(MY.QT_GOOD_GR + MY.QT_REJECT_GR + MY.QT_INSP_GR + MY.QT_TRANS_GR) 
		          - SUM(MY.QT_GOOD_GI + MY.QT_REJECT_GI + MY.QT_INSP_GI + MY.QT_TRANS_GI)) AS QT_INV
		   FROM MM_PINVN MY
		   WHERE MY.P_YR = SUBSTRING(CONVERT(NVARCHAR(8), GETDATE(), 112), 1, 4)
		   AND MY.CD_SL = 'SL_STND'
		   GROUP BY MY.CD_COMPANY, MY.CD_ITEM) INV
ON INV.CD_COMPANY = D.CD_COMPANY AND INV.CD_ITEM = D.CD_MATL
WHERE (ISNULL(@P_TP_PROC, '') = '' OR MI.TP_PROC = @P_TP_PROC)
ORDER BY D.LEVEL
OPTION(RECOMPILE)

GO