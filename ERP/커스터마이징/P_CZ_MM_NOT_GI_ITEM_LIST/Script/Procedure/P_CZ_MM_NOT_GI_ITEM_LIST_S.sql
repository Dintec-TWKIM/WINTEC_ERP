USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[P_CZ_MM_NOT_GI_ITEM_LIST_S]    Script Date: 2017-12-14 오후 2:27:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[P_CZ_MM_NOT_GI_ITEM_LIST_S]
(
	@P_DT_FROM		NVARCHAR(8),
	@P_DT_TO		NVARCHAR(8),
	@P_YN_IMAGE		NVARCHAR(1)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @SUBJ TABLE
(
	  CD_PARTNER NVARCHAR(50)
	, SUBJ1		 NVARCHAR(50)
	, SUBJ2		 NVARCHAR(50)
	, SUBJ3		 NVARCHAR(50)
	, SUBJ4		 NVARCHAR(50)
)
INSERT INTO @SUBJ
SELECT
	  CD_PARTNER
	, NEOE.SPLIT(NEOE.FN_CZ_DECODE_ASCII(SUBJ), ' ', 1)	AS SUBJ1
	, NEOE.SPLIT(NEOE.FN_CZ_DECODE_ASCII(SUBJ), ' ', 2)	AS SUBJ2
	, NEOE.SPLIT(NEOE.FN_CZ_DECODE_ASCII(SUBJ), ' ', 3)	AS SUBJ3
	, NEOE.SPLIT(NEOE.FN_CZ_DECODE_ASCII(SUBJ), ' ', 4)	AS SUBJ4
FROM
(
	SELECT
		  CD_PARTNER
		, SPLIT.A.value('.', 'NVARCHAR(100)')	AS SUBJ
	FROM
	(
		SELECT
			  CD_FLAG1	AS CD_PARTNER
			, CONVERT(XML, '<N>' + REPLACE(NEOE.FN_CZ_ENCODE_ASCII(CD_FLAG4), ',', '</N><N>') + '</N>')	AS X
		FROM CZ_MA_CODEDTL
		WHERE 1 = 1
			AND CD_COMPANY = 'K100'
			AND CD_FIELD = 'CZ_MM00002'
			AND YN_USE = 'Y'
			AND ISNULL(CD_FLAG4, '') <> ''
	) AS A CROSS APPLY X.nodes ('/N[.]') AS SPLIT(A)
) AS A

SELECT QL.NO_DSP,
	   IH.CD_COMPANY,
	   MC.NM_COMPANY,
	   IH.NO_IO,
	   IL.NO_IOLINE,
	   IH.DT_IO,
	   MP.LN_PARTNER,
	   PL.NO_PO,
	   PL.CD_ITEM,
	   QL.CD_ITEM_PARTNER,
	   QL.NM_ITEM_PARTNER,
	   IL.QT_IO,
	   LC.CD_LOCATION,
	   (CASE WHEN EXISTS (SELECT 1 
						  FROM @SUBJ A
						  WHERE A.CD_PARTNER = IH.CD_PARTNER 
						  AND ('   ' + QL.NM_SUBJECT + '   ' + QL.CD_ITEM_PARTNER + '   ' + QL.NM_ITEM_PARTNER + '   ') LIKE '%' + A.SUBJ1 + '%'
						  AND ('   ' + QL.NM_SUBJECT + '   ' + QL.CD_ITEM_PARTNER + '   ' + QL.NM_ITEM_PARTNER + '   ') LIKE '%' + A.SUBJ2 + '%'
						  AND ('   ' + QL.NM_SUBJECT + '   ' + QL.CD_ITEM_PARTNER + '   ' + QL.NM_ITEM_PARTNER + '   ') LIKE '%' + A.SUBJ3 + '%'
						  AND ('   ' + QL.NM_SUBJECT + '   ' + QL.CD_ITEM_PARTNER + '   ' + QL.NM_ITEM_PARTNER + '   ') LIKE '%' + A.SUBJ4 + '%') THEN 'Y' ELSE 'N' END) AS YN_WARNING,
	   (CASE WHEN EXISTS (SELECT 1 
						  FROM @SUBJ A
						  WHERE A.CD_PARTNER = IH.CD_PARTNER 
						  AND ('   ' + QL.NM_SUBJECT + '   ' + QL.CD_ITEM_PARTNER + '   ' + QL.NM_ITEM_PARTNER + '   ') LIKE '%' + A.SUBJ1 + '%'
						  AND ('   ' + QL.NM_SUBJECT + '   ' + QL.CD_ITEM_PARTNER + '   ' + QL.NM_ITEM_PARTNER + '   ') LIKE '%' + A.SUBJ2 + '%'
						  AND ('   ' + QL.NM_SUBJECT + '   ' + QL.CD_ITEM_PARTNER + '   ' + QL.NM_ITEM_PARTNER + '   ') LIKE '%' + A.SUBJ3 + '%'
						  AND ('   ' + QL.NM_SUBJECT + '   ' + QL.CD_ITEM_PARTNER + '   ' + QL.NM_ITEM_PARTNER + '   ') LIKE '%' + A.SUBJ4 + '%') THEN MC1.CD_FLAG5 ELSE NULL END) AS DC_RMK_WORK,
	   IL.CD_USERDEF1 AS DC_RMK_SPO,
	   IL.CD_USERDEF2 AS DC_RMK_ALBA
FROM MM_QTIOH IH
JOIN MM_QTIO IL ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IO = IH.NO_IO
JOIN PU_POL PL ON PL.CD_COMPANY = IL.CD_COMPANY AND PL.NO_PO = IL.NO_PSO_MGMT AND PL.NO_LINE = IL.NO_PSOLINE_MGMT
JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = PL.CD_COMPANY AND QL.NO_FILE = PL.NO_SO AND QL.NO_LINE = PL.NO_LINE
JOIN CZ_MA_CODEDTL MC1 ON MC1.CD_COMPANY = 'K100' AND MC1.CD_FIELD = 'CZ_MM00002' AND MC1.CD_FLAG1 = IH.CD_PARTNER AND MC1.YN_USE = 'Y'
LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = IH.CD_COMPANY
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = IH.CD_COMPANY AND MP.CD_PARTNER = IH.CD_PARTNER
LEFT JOIN MM_QTIO_LOCATION LC ON LC.CD_COMPANY = IL.CD_COMPANY AND LC.NO_IO = IL.NO_IO AND LC.NO_IOLINE = IL.NO_IOLINE
WHERE IH.CD_COMPANY IN ('K100', 'K200')
AND IH.DT_IO BETWEEN @P_DT_FROM AND @P_DT_TO
AND IL.FG_PS = '1'
AND (ISNULL(MC1.CD_FLAG2, '') = '' OR IL.CD_ITEM LIKE MC1.CD_FLAG2 + '%')
AND (ISNULL(MC1.CD_FLAG3, '') = '' OR NOT EXISTS (SELECT 1 
												  FROM CZ_MA_PITEM_FILE MF
												  WHERE MF.CD_COMPANY = IL.CD_COMPANY
												  AND MF.CD_PLANT = IL.CD_PLANT
												  AND MF.CD_ITEM = IL.CD_ITEM
												  AND ((MF.IMAGE1 LIKE '%.jpg' OR MF.IMAGE1 LIKE '%.png') OR 
													   (MF.IMAGE2 LIKE '%.jpg' OR MF.IMAGE2 LIKE '%.png') OR 
													   (MF.IMAGE3 LIKE '%.jpg' OR MF.IMAGE3 LIKE '%.png') OR 
													   (MF.IMAGE4 LIKE '%.jpg' OR MF.IMAGE4 LIKE '%.png') OR 
													   (MF.IMAGE5 LIKE '%.jpg' OR MF.IMAGE5 LIKE '%.png') OR 
													   (MF.IMAGE6 LIKE '%.jpg' OR MF.IMAGE6 LIKE '%.png') OR 
													   (MF.IMAGE7 LIKE '%.jpg' OR MF.IMAGE7 LIKE '%.png'))))
AND (ISNULL(@P_YN_IMAGE, 'N') = 'N' OR NOT EXISTS (SELECT 1 
												   FROM CZ_PU_POL_FILE PF
												   WHERE PF.CD_COMPANY = PL.CD_COMPANY
												   AND PF.NO_PO = PL.NO_PO
												   AND PF.NO_LINE = PL.NO_LINE))
AND EXISTS (SELECT 1 
			FROM SA_SOL SL
			WHERE SL.CD_COMPANY = PL.CD_COMPANY
			AND SL.NO_SO = PL.NO_SO
			AND SL.SEQ_SO = PL.NO_SOLINE
			AND SL.QT_SO > ISNULL(SL.QT_GI, 0))
ORDER BY IH.CD_COMPANY, PL.NO_PO

GO