USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_TAX_EXPORT_AUTO]    Script Date: 2016-06-28 오후 1:28:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_FI_TAX_EXPORT_AUTO] 
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_NO_TAX		NVARCHAR(20),
	@P_ID_USER		NVARCHAR(10)
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

CREATE TABLE #CZ_FI_TAX
(
	CD_COMPANY  NVARCHAR(7),
	NO_IV       NVARCHAR(20),
	NO_IO       NVARCHAR(20),
	NO_SO       NVARCHAR(20),
	NO_TO       NVARCHAR(20),
	DT_TO       NVARCHAR(8),
	DT_SHIPPING NVARCHAR(8),
	CD_EXCH     NVARCHAR(3),
	RT_EXCH     NUMERIC(15, 4),
	AM_EX       NUMERIC(19, 4),
	AM          NUMERIC(19, 4),
	ID_INSERT   NVARCHAR(15),
	DTS_INSERT  NVARCHAR(14)
)

INSERT INTO #CZ_FI_TAX
SELECT TE.CD_COMPANY,
	   IL.NO_IV,
	   IL.NO_IO,
	   IL.NO_SO,
	   TE.NO_TAX,
	   TE.DT_TAX,
	   TE.DT_SHIPPING,
	   TE.CD_EXCH,
	   TE.RT_EXCH,
	   TE.AM_TAX_EX,
	   TE.AM_TAX,
	   @P_ID_USER AS ID_INSERT,
	   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
FROM (SELECT TE.CD_COMPANY,
			 TE.NO_TAX,
			 TE.DT_TAX,
			 MAX(TE.DT_LOADING) AS DT_SHIPPING,
			 MAX(TE.CD_EXCH) AS CD_EXCH,
			 MAX(TE.RT_EXCH) AS RT_EXCH,
			 MAX(TE.AM_TAX_EX) AS AM_TAX_EX,
			 MAX(TE.AM_TAX) AS AM_TAX
	  FROM CZ_FI_TAX_EXPORT TE
	  GROUP BY TE.CD_COMPANY, TE.NO_TAX, TE.DT_TAX) TE
JOIN CZ_FI_TAX_EXPORT_D TD ON TD.CD_COMPANY = TE.CD_COMPANY AND TD.NO_TAX = TE.NO_TAX
JOIN (SELECT IL.CD_COMPANY, IL.NO_IV, IL.NO_IO, IL.NO_SO 
	  FROM SA_IVL IL
	  GROUP BY IL.CD_COMPANY, IL.NO_IV, IL.NO_IO, IL.NO_SO) IL
ON IL.CD_COMPANY = TD.CD_COMPANY AND IL.NO_IO = TD.NO_IO
WHERE TE.CD_COMPANY = @P_CD_COMPANY
AND TE.NO_TAX = @P_NO_TAX

INSERT INTO CZ_FI_TAX
(
	CD_COMPANY,
	NO_IV,
	NO_IO,
	NO_SO,
	NO_TO,
	DT_TO,
	DT_SHIPPING,
	CD_EXCH,
	RT_EXCH,
	AM_EX,
	AM,
	ID_INSERT,
	DTS_INSERT
)
SELECT A.CD_COMPANY,
	   A.NO_IV,
	   A.NO_IO,
	   A.NO_SO,
	   A.NO_TO,
	   A.DT_TO,
	   A.DT_SHIPPING,
	   A.CD_EXCH,
	   A.RT_EXCH,
	   A.AM_EX,
	   A.AM,
	   A.ID_INSERT,
	   A.DTS_INSERT 
FROM #CZ_FI_TAX A
WHERE NOT EXISTS (SELECT 1 
				  FROM CZ_FI_TAX FT
				  WHERE FT.CD_COMPANY = A.CD_COMPANY
				  AND FT.NO_IV = A.NO_IV
				  AND FT.NO_IO = A.NO_IO
				  AND FT.NO_SO = A.NO_SO)

UPDATE FT
SET FT.NO_TO = A.NO_TO,
	FT.DT_TO = A.DT_TO,
	FT.DT_SHIPPING = A.DT_SHIPPING,
	FT.CD_EXCH = A.CD_EXCH,
	FT.RT_EXCH = A.RT_EXCH,
	FT.AM_EX = A.AM_EX,
	FT.AM = A.AM,
	FT.ID_UPDATE = A.ID_INSERT,
	FT.DTS_UPDATE = A.DTS_INSERT
FROM CZ_FI_TAX FT
JOIN #CZ_FI_TAX A ON A.CD_COMPANY = FT.CD_COMPANY AND A.NO_IV = FT.NO_IV AND A.NO_IO = FT.NO_IO AND A.NO_SO = FT.NO_SO
WHERE ISNULL(FT.NO_TO, '') = ''

DROP TABLE #CZ_FI_TAX

GO