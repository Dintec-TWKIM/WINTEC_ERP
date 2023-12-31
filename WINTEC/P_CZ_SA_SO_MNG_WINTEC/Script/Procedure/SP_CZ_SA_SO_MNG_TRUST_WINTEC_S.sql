USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SO_MNG_TRUST_WINTEC_S]    Script Date: 2023-06-15 오후 7:55:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_SO_MNG_TRUST_WINTEC_S]        
(  
    @P_CD_COMPANY           NVARCHAR(7),
    @P_NO_SO                NVARCHAR(20),
    @P_SEQ_SO               NUMERIC(5, 0),
	@P_CD_ITEM				NVARCHAR(20)
)        
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_COLUMN_PV NVARCHAR(MAX)
DECLARE @V_COLUMN_PV1 NVARCHAR(MAX)
DECLARE @V_COLUMN NVARCHAR(MAX)
DECLARE @V_COLUMN1 NVARCHAR(MAX)
DECLARE @V_QUERY NVARCHAR(MAX)

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

CREATE NONCLUSTERED INDEX BOM ON #BOM (CD_COMPANY, CD_PLANT, CD_ITEM_T)

;WITH BOM 
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
	JOIN MA_PITEM MI ON MI.CD_COMPANY = PB.CD_COMPANY AND MI.CD_PLANT = PB.CD_PLANT AND MI.CD_ITEM = PB.CD_MATL AND MI.TP_PROC = 'M' AND ISNULL(MI.YN_USE, 'N') = 'Y'
	WHERE PB.CD_COMPANY = @P_CD_COMPANY
	AND PB.NO_OPPATH = '100'
	AND PB.CD_ITEM = @P_CD_ITEM
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
	JOIN PR_ROUT_ASN PB ON PB.CD_ITEM = BM.CD_MATL AND PB.NO_OPPATH = '100'
	JOIN PR_BOM PB1 ON PB1.CD_COMPANY = PB.CD_COMPANY AND PB1.CD_PLANT = PB.CD_PLANT AND PB1.CD_ITEM = PB.CD_ITEM AND PB1.CD_MATL = PB.CD_MATL AND PB1.DT_END >= CONVERT(CHAR(8), GETDATE(), 112)
	JOIN MA_PITEM MI ON MI.CD_COMPANY = PB.CD_COMPANY AND MI.CD_PLANT = PB.CD_PLANT AND MI.CD_ITEM = PB.CD_MATL AND MI.TP_PROC = 'M' AND ISNULL(MI.YN_USE, 'N') = 'Y'
	WHERE EXISTS (SELECT 1 
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
UNION ALL
SELECT PB.CD_COMPANY,
	   PB.CD_PLANT,
	   PB.CD_ITEM AS CD_ITEM_T,
	   PB.CD_ITEM, 
	   PB.CD_ITEM, 
	   LEVEL = 0, 
	   CONVERT(VARCHAR(1000), PB.CD_ITEM),
	   1
FROM PR_ROUT_ASN PB
JOIN MA_PITEM MI1 ON MI1.CD_COMPANY = PB.CD_COMPANY AND MI1.CD_PLANT = PB.CD_PLANT AND MI1.CD_ITEM = PB.CD_ITEM
WHERE PB.CD_COMPANY = @P_CD_COMPANY
AND PB.NO_OPPATH = '100'
AND PB.CD_ITEM = @P_CD_ITEM
AND EXISTS (SELECT 1 
		    FROM PR_ROUT_L RL
		    WHERE RL.CD_COMPANY = PB.CD_COMPANY
		    AND RL.CD_PLANT = PB.CD_PLANT
		    AND RL.CD_ITEM = PB.CD_ITEM
		    AND RL.NO_OPPATH = PB.NO_OPPATH
		    AND RL.TP_OPPATH = PB.TP_OPPATH
		    AND RL.CD_OP = PB.CD_OP
			AND RL.YN_USE = 'Y')
GROUP BY PB.CD_COMPANY, PB.CD_PLANT, PB.CD_ITEM

;WITH A AS
(
	SELECT '[' + MI.CD_ITEM + ']' AS CD_ITEM,
		   '[' + MI.CD_ITEM + '-1]' AS CD_ITEM1,
		   'MAX(PV1.[' + MI.CD_ITEM + ']) AS [' + MI.CD_ITEM + ']' AS CD_ITEM_MAX,
		   'MAX(PV1.[' + MI.CD_ITEM + '-1]) AS [' + MI.CD_ITEM + '-1]' AS CD_ITEM1_MAX
	FROM SA_SOL SL
	LEFT JOIN #BOM BM ON BM.CD_COMPANY = SL.CD_COMPANY AND BM.CD_ITEM_T = SL.CD_ITEM
	LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = BM.CD_COMPANY AND MI.CD_PLANT = BM.CD_PLANT AND MI.CD_ITEM = BM.CD_MATL
	WHERE SL.CD_COMPANY = @P_CD_COMPANY
	AND SL.NO_SO = @P_NO_SO
	AND SL.SEQ_SO = @P_SEQ_SO
	AND MI.GRP_ITEM IN ('2VP01', '2VP02', '2VP05', '2VP06', 'GP01', '4VP06', '4PP06', 'GP04')
)
SELECT @V_COLUMN_PV = STRING_AGG(A.CD_ITEM_MAX, ','),
	   @V_COLUMN_PV1 = STRING_AGG(A.CD_ITEM1_MAX, ','),
	   @V_COLUMN = STRING_AGG(A.CD_ITEM, ','),
	   @V_COLUMN1 = STRING_AGG(A.CD_ITEM1, ',')
FROM A

SET @V_QUERY = ';WITH A AS
(
	SELECT SL.NO_SO,
		   SL.SEQ_SO,
		   IX.IDX,
		   MI.CD_ITEM,
		   (MI.CD_ITEM + ''-1'') AS CD_ITEM1,
		   ST.NO_TRUST,
		   ST.NO_ID
	FROM SA_SOL SL
	LEFT JOIN #BOM BM ON BM.CD_COMPANY = SL.CD_COMPANY AND BM.CD_ITEM_T = SL.CD_ITEM
	LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = BM.CD_COMPANY AND MI.CD_PLANT = BM.CD_PLANT AND MI.CD_ITEM = BM.CD_MATL
	LEFT JOIN (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 0)) AS IDX FROM sys.all_objects) IX
	ON IX.IDX <= SL.QT_SO
	LEFT JOIN CZ_SA_SOL_TRUST_WINTEC ST ON ST.CD_COMPANY = SL.CD_COMPANY AND ST.NO_SO = SL.NO_SO AND ST.IDX = IX.IDX AND ST.CD_ITEM = MI.CD_ITEM
	WHERE SL.CD_COMPANY = ''' + @P_CD_COMPANY + '''
    AND SL.NO_SO = ''' + @P_NO_SO + '''
	AND SL.SEQ_SO = ' + CONVERT(NVARCHAR, @P_SEQ_SO) + '
	AND MI.GRP_ITEM IN (''2VP01'', ''2VP02'', ''2VP05'', ''2VP06'', ''GP01'', ''4VP06'', ''4PP06'', ''GP04'')
)
SELECT PV1.NO_SO,
	   PV1.SEQ_SO,
	   PV1.IDX,'
	   + @V_COLUMN_PV + ','
	   + @V_COLUMN_PV1 +
'FROM A
PIVOT (MAX(A.NO_TRUST) FOR A.CD_ITEM IN (' + @V_COLUMN + ')) AS PV
PIVOT (MAX(PV.NO_ID) FOR PV.CD_ITEM1 IN (' + @V_COLUMN1 + ')) AS PV1
GROUP BY PV1.NO_SO, PV1.SEQ_SO, PV1.IDX
ORDER BY PV1.IDX ASC'

PRINT @V_QUERY

EXEC SP_EXECUTESQL @V_QUERY

SELECT * INTO #SORT 
FROM 
(VALUES
('2VP05', '1'),
('2VP02', '2'),
('2VP06', '3'),
('2VP01', '4'),
('GP01', '5'),
('4VP06', '6'),
('4PP06', '7'),
('GP04', '8')) AS A (GRP_ITEM, SORT)

SELECT MI.CD_ITEM,
	   MI.NM_ITEM,
	   MI.GRP_ITEM
FROM SA_SOL SL
LEFT JOIN #BOM BM ON BM.CD_COMPANY = SL.CD_COMPANY AND BM.CD_ITEM_T = SL.CD_ITEM
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = BM.CD_COMPANY AND MI.CD_PLANT = BM.CD_PLANT AND MI.CD_ITEM = BM.CD_MATL
LEFT JOIN #SORT ST ON ST.GRP_ITEM = MI.GRP_ITEM
WHERE SL.CD_COMPANY = @P_CD_COMPANY
AND SL.NO_SO = @P_NO_SO
AND SL.SEQ_SO = @P_SEQ_SO
AND MI.GRP_ITEM IN ('2VP01', '2VP02', '2VP05', '2VP06', 'GP01', '4VP06', '4PP06', 'GP04')
ORDER BY ST.SORT ASC

DROP TABLE #BOM
DROP TABLE #SORT

