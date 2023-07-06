USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WO_SA_SOL_CHANGE_S]    Script Date: 2021-03-02 오전 10:51:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_SA_SOL_CHANGE_S]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_OPPATH		NVARCHAR(3),
	@P_NO_SO			NVARCHAR(20),
	@P_CD_ITEM			NVARCHAR(20),
	@P_NO_WO			NVARCHAR(20),
	@P_DT_FROM			NVARCHAR(8),
	@P_DT_TO			NVARCHAR(8),
	@P_YN_EXPECT_GIR	NVARCHAR(1),
	@P_YN_EXPECT_GI		NVARCHAR(1),
	@P_YN_EXPECT_CLOSE  NVARCHAR(1)
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
FROM BOM
WHERE CD_MATL = @P_CD_ITEM;

SELECT SL.NO_SO,
	   SL.SEQ_SO,
	   SL.DT_DUEDATE,
	   SL.CD_ITEM,
	   MI.NM_ITEM,
	   SL.QT_SO,
	   SL.QT_GIR,
	   SL.QT_GI,
	   MP.QT_APPLY,
	   ST.QT_STOCK
FROM SA_SOH SH
LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_PLANT = SL.CD_PLANT AND MI.CD_ITEM = SL.CD_ITEM
LEFT JOIN (SELECT CD_COMPANY, NO_SO, NO_LINE, CD_ITEM,
				  SUM(QT_APPLY) AS QT_APPLY
		   FROM CZ_PR_SA_SOL_PR_WO_MAPPING
		   GROUP BY CD_COMPANY, NO_SO, NO_LINE, CD_ITEM) MP
ON MP.CD_COMPANY = SL.CD_COMPANY AND MP.NO_SO = SL.NO_SO AND MP.NO_LINE = SL.SEQ_SO AND MP.CD_ITEM = SL.CD_ITEM
LEFT JOIN (SELECT QH.CD_COMPANY, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM,
		   	      SUM(QL.QT_GI) AS QT_STOCK
		   FROM MM_GIREQH QH
		   JOIN MM_GIREQL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_GIREQ = QH.NO_GIREQ
		   WHERE QL.CD_SL = 'SL_STND'
		   GROUP BY QH.CD_COMPANY, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM) ST
ON ST.CD_COMPANY = SL.CD_COMPANY AND ST.NO_SO = SL.NO_SO AND ST.SEQ_SO = SL.SEQ_SO AND ST.CD_ITEM = SL.CD_ITEM
WHERE SL.CD_COMPANY = @P_CD_COMPANY
AND SL.DT_DUEDATE BETWEEN @P_DT_FROM AND @P_DT_TO
AND (ISNULL(@P_NO_SO, '') = '' OR SL.NO_SO = @P_NO_SO)
AND (ISNULL(@P_YN_EXPECT_GIR, 'N') = 'N' OR ISNULL(SL.QT_SO, 0) > ISNULL(SL.QT_GIR, 0))
AND (ISNULL(@P_YN_EXPECT_GI, 'N') = 'N' OR ISNULL(SL.QT_SO, 0) > ISNULL(SL.QT_GI, 0))
AND (ISNULL(@P_YN_EXPECT_CLOSE, 'N') = 'N' OR SH.STA_SO <> 'C')
AND MI.TP_PROC = 'M'
AND (ISNULL(@P_CD_ITEM, '') = '' OR SL.CD_ITEM = @P_CD_ITEM OR EXISTS (SELECT 1 
																	   FROM #BOM BM
																	   WHERE BM.CD_COMPANY = SL.CD_COMPANY
																	   AND BM.CD_ITEM_T = SL.CD_ITEM))
AND (ISNULL(@P_NO_WO, '') = '' OR EXISTS (SELECT 1 
										  FROM CZ_PR_SA_SOL_PR_WO_MAPPING SM
										  WHERE SM.CD_COMPANY = SL.CD_COMPANY
										  AND SM.NO_SO = SL.NO_SO
										  AND SM.NO_LINE = SL.SEQ_SO
										  AND SM.NO_WO = @P_NO_WO))
ORDER BY SL.DT_DUEDATE ASC, SL.NO_SO ASC

GO