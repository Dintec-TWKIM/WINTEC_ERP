USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_HULL_ENGINE_ITEM_CHECK]    Script Date: 2020-03-06 오후 1:38:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MA_HULL_ENGINE_ITEM_CHECK] 
(
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10)
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_DOC INT

EXEC SP_XML_PREPAREDOCUMENT @V_DOC OUTPUT, @P_XML 

SELECT * INTO #XML
FROM OPENXML (@V_DOC, '/XML/ROW', 2) WITH 
(
    NO_IMO			NVARCHAR(10), 
	NO_ENGINE		INT,
	NO_PLATE		NVARCHAR(100),
	YN_DELETE		NVARCHAR(1)
)

EXEC SP_XML_REMOVEDOCUMENT @V_DOC

SELECT EI.NO_IMO,
	   EI.NO_ENGINE,
	   EI.NO_PLATE
FROM CZ_MA_HULL_ENGINE_ITEM EI
WHERE EXISTS (SELECT 1 
			  FROM #XML A
			  WHERE A.NO_IMO = EI.NO_IMO
			  AND A.NO_ENGINE = EI.NO_ENGINE
			  AND A.NO_PLATE = EI.NO_PLATE
			  AND A.YN_DELETE <> 'Y')

DROP TABLE #XML

