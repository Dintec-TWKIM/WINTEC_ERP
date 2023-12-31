USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_TAX_EXPORTH_S]    Script Date: 2015-08-11 오전 9:59:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_FI_TAX_EXPORTH_S] 
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_DT_TAX_FROM			NVARCHAR(8),
	@P_DT_TAX_TO			NVARCHAR(8),
	@P_DT_LOADING_FROM		NVARCHAR(8),
	@P_DT_LOADING_TO		NVARCHAR(8),
	@P_AM_DIFF				INT,
	@P_YN_CHECK				NVARCHAR(1),
	@P_YN_IMPORT			NVARCHAR(1),
	@P_NO_TAX				NVARCHAR(20)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   TE.NO_TAX,
	   TE.DT_TAX,
	   TE.NO_REF,
	   TE.NO_IMPORT,
	   TE.DT_LOADING,
	   TE.CD_EXCH,
	   TE.RT_EXCH,
	   FT.AM_EX_IO,
	   FT.AM_EX_IV,
	   TE.AM_TAX_EX,
	   TE.AM_PAY,
	   (ISNULL(FT.AM_EX_IO, 0) - ISNULL(TE.AM_PAY, 0)) AS AM_DIFF,
	   TE.AM_TAX,
	   REPLACE(TE.DC_TAX, CHAR(10), CHAR(13) + CHAR(10)) AS DC_TAX,
	   TE.YN_CHECK,
	   TE.DC_IMPORT,
	   TE.DC_RMK,
	   (CASE WHEN FT.NO_TO IS NOT NULL THEN 'Y' ELSE 'N' END) AS YN_TAX,
	   TE.TP_GUBUN
FROM (SELECT TE.CD_COMPANY,
			 TE.NO_TAX,
			 MAX(TE.DT_TAX) AS DT_TAX,
			 MAX(TE.NO_REF) AS NO_REF,
			 MAX(TE.NO_IMPORT) AS NO_IMPORT,
			 MAX(TE.DC_TAX) AS DC_TAX,
			 MAX(TE.DT_LOADING) AS DT_LOADING,
			 MAX(TE.CD_EXCH) AS CD_EXCH,
			 MAX(TE.RT_EXCH) AS RT_EXCH,
			 MAX(TE.AM_TAX_EX) AS AM_TAX_EX,
			 MAX(TE.AM_TAX) AS AM_TAX,
			 MAX(TE.AM_PAY) AS AM_PAY,
			 MAX(TE.YN_CHECK) AS YN_CHECK,
			 MAX(TE.DC_IMPORT) AS DC_IMPORT,
			 MAX(TE.DC_RMK) AS DC_RMK,
			 MAX(TE.TP_GUBUN) AS TP_GUBUN
	  FROM CZ_FI_TAX_EXPORT TE
	  GROUP BY TE.CD_COMPANY, TE.NO_TAX) TE
LEFT JOIN (SELECT FT.CD_COMPANY, FT.NO_TO,
				  SUM(IL.AM_EX_IV) AS AM_EX_IV,
				  SUM(IL.AM_EX_IO) AS AM_EX_IO
		   FROM CZ_FI_TAX FT
		   LEFT JOIN (SELECT IL.CD_COMPANY, IL.NO_IV, IL.NO_IO, IL.NO_SO,
					  	     SUM(CASE WHEN ISNULL(IL.YN_RETURN, 'N') = 'Y' THEN -IL.AM_EX_CLS ELSE IL.AM_EX_CLS END) AS AM_EX_IV,
							 SUM(CASE WHEN ISNULL(IL.YN_RETURN, 'N') = 'Y' THEN -OL.AM_EX ELSE OL.AM_EX END) AS AM_EX_IO
					  FROM SA_IVL IL
					  JOIN MM_QTIO OL ON OL.CD_COMPANY = IL.CD_COMPANY AND OL.NO_IO = IL.NO_IO AND OL.NO_IOLINE = IL.NO_IOLINE AND IL.QT_CLS > 0
					  GROUP BY IL.CD_COMPANY, IL.NO_IV, IL.NO_IO, IL.NO_SO) IL
		   ON IL.CD_COMPANY = FT.CD_COMPANY AND IL.NO_IV = FT.NO_IV AND IL.NO_IO = FT.NO_IO AND IL.NO_SO = FT.NO_SO
		   GROUP BY FT.CD_COMPANY, FT.NO_TO) FT
ON FT.CD_COMPANY = TE.CD_COMPANY AND FT.NO_TO = TE.NO_TAX
WHERE TE.CD_COMPANY = @P_CD_COMPANY
AND (ISNULL(@P_NO_TAX, '') = '' OR TE.NO_TAX = REPLACE(@P_NO_TAX, '-', ''))
AND ((ISNULL(@P_DT_TAX_FROM, '') = '' AND ISNULL(@P_DT_TAX_TO, '') = '') OR (TE.DT_TAX BETWEEN @P_DT_TAX_FROM AND @P_DT_TAX_TO))
AND ((ISNULL(@P_DT_LOADING_FROM, '') = '' AND ISNULL(@P_DT_LOADING_TO, '') = '') OR (TE.DT_LOADING BETWEEN @P_DT_LOADING_FROM AND @P_DT_LOADING_TO))
AND (ISNULL(@P_YN_CHECK, 'N') = 'N' OR ISNULL(TE.YN_CHECK, 'N') = 'N')
AND (ISNULL(@P_YN_IMPORT, 'N') = 'N' OR ISNULL(TE.DC_IMPORT, '') <> '' OR ISNULL(TE.NO_IMPORT, '') <> '')
AND ABS(ISNULL(FT.AM_EX_IO, 0) - ISNULL(TE.AM_PAY, 0)) > @P_AM_DIFF

GO