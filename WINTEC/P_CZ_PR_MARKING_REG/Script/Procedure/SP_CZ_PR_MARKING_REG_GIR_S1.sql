USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MARKING_REG_GIR_S1]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_MARKING_REG_GIR_S1]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_GIR			NVARCHAR(20),
	@P_SEQ_GIR			NUMERIC(5, 0),
	@P_NO_SEQ			NUMERIC(5, 0),
	@P_CD_MATL			NVARCHAR(20)
)
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

CREATE TABLE #BOM
(
	CD_COMPANY	NVARCHAR(7), 
	CD_PLANT	NVARCHAR(7), 
	CD_ITEM_T	NVARCHAR(20),
	NO_SEQ		NUMERIC(5, 0),
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
	NO_SEQ,
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
		   PB.NO_SEQ,
		   PB.CD_ITEM, 
		   PB.CD_MATL, 
		   LEVEL = 1, 
		   CONVERT(VARCHAR(1000), PB.CD_MATL),
		   PB1.QT_ITEM_NET
    FROM PR_ROUT_ASN PB
	JOIN PR_BOM PB1 ON PB1.CD_COMPANY = PB.CD_COMPANY AND PB1.CD_PLANT = PB.CD_PLANT AND PB1.CD_ITEM = PB.CD_ITEM AND PB1.CD_MATL = PB.CD_MATL AND PB1.DT_END >= CONVERT(CHAR(8), GETDATE(), 112)
	WHERE PB.CD_COMPANY = @P_CD_COMPANY
	AND PB.NO_OPPATH = '100'
	AND EXISTS (SELECT 1 
				FROM MA_PITEM MI 
				WHERE MI.CD_COMPANY = PB.CD_COMPANY 
				AND MI.CD_PLANT = PB.CD_PLANT 
				AND MI.CD_ITEM = PB.CD_MATL
				AND MI.TP_PROC = 'M'
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
		   PB.NO_SEQ,
		   PB.CD_ITEM, 
		   PB.CD_MATL, 
		   LEVEL + 1, 
		   CONVERT(VARCHAR(1000), PATH + ' -> ' + PB.CD_MATL),
		   PB1.QT_ITEM_NET
	FROM BOM BM
	JOIN PR_ROUT_ASN PB ON PB.CD_ITEM = BM.CD_MATL AND PB.NO_OPPATH = '100'
	JOIN PR_BOM PB1 ON PB1.CD_COMPANY = PB.CD_COMPANY AND PB1.CD_PLANT = PB.CD_PLANT AND PB1.CD_ITEM = PB.CD_ITEM AND PB1.CD_MATL = PB.CD_MATL AND PB1.DT_END >= CONVERT(CHAR(8), GETDATE(), 112)
	WHERE EXISTS (SELECT 1 
				  FROM MA_PITEM MI 
				  WHERE MI.CD_COMPANY = PB.CD_COMPANY 
				  AND MI.CD_PLANT = PB.CD_PLANT 
				  AND MI.CD_ITEM = PB.CD_MATL
				  AND MI.TP_PROC = 'M'
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
	   NO_SEQ,
	   CD_ITEM, 
	   CD_MATL, 
	   LEVEL, 
	   PATH, 
	   QT 
FROM BOM;

SELECT GH.NO_GIR,
	   (ISNULL(GM.NO_LINE, 0) + 1) AS NO_LINE,
	   BM.CD_MATL,
	   MI.NM_ITEM,
	   MI.NO_DESIGN,
	   QI.NO_CERT AS DC_TEXT
FROM SA_GIRH GH
JOIN SA_GIRL GL ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
JOIN #BOM BM ON BM.CD_COMPANY = GH.CD_COMPANY AND BM.CD_PLANT = GH.CD_PLANT AND BM.CD_ITEM_T = GL.CD_ITEM
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = BM.CD_COMPANY AND MI.CD_PLANT = BM.CD_PLANT AND MI.CD_ITEM = BM.CD_MATL
JOIN CZ_PR_MARKING_SETTING MS ON MS.CD_COMPANY = GH.CD_COMPANY AND MS.CD_PARTNER = GH.CD_PARTNER AND MS.CD_ITEM = MI.CD_ITEM
LEFT JOIN (SELECT CD_COMPANY, NO_GIR, SEQ_GIR, NO_SEQ, CD_MATL,
				  MAX(NO_LINE) AS NO_LINE
		   FROM CZ_SA_GIRL_MARKING
		   GROUP BY CD_COMPANY, NO_GIR, SEQ_GIR, NO_SEQ, CD_MATL) GM
ON GM.CD_COMPANY = GL.CD_COMPANY AND GM.NO_GIR = GL.NO_GIR AND GM.SEQ_GIR = GL.SEQ_GIR AND GM.NO_SEQ = BM.NO_SEQ AND GM.CD_MATL = BM.CD_MATL
LEFT JOIN (SELECT QI.CD_COMPANY, QI.NO_SO,
				  MAX(QI.NO_CERT) AS NO_CERT
		   FROM CZ_QU_INSP_SCHEDULE QI
		   GROUP BY QI.CD_COMPANY, QI.NO_SO) QI 
ON QI.CD_COMPANY = GL.CD_COMPANY AND QI.NO_SO = GL.NO_SO
WHERE GH.CD_COMPANY = @P_CD_COMPANY
AND GH.NO_GIR = @P_NO_GIR
AND GL.SEQ_GIR = @P_SEQ_GIR
AND BM.NO_SEQ = @P_NO_SEQ
AND BM.CD_MATL = @P_CD_MATL
AND GL.QT_GIR >= (ISNULL(GM.NO_LINE, 0) + 1)
AND (ISNULL(MS.YN_CERT, 'N') = 'N' OR ISNULL(QI.NO_CERT, '') <> '')

GO