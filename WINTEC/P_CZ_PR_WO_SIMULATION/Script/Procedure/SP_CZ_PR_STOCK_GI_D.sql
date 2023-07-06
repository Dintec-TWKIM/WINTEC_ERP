USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_STOCK_GI_D]    Script Date: 2021-03-12 오후 3:39:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_STOCK_GI_D]      
(
	@P_CD_COMPANY   NVARCHAR(7),
	@P_NO_GIREQ		NVARCHAR(20)
)      
AS

BEGIN TRAN SP_CZ_PR_STOCK_GI_D
BEGIN TRY

DECLARE @V_ERRMSG		NVARCHAR(255)

SELECT OL.CD_COMPANY,
	   OL.NO_IO,
	   OL.NO_IOLINE INTO #MM_QTIO 
FROM MM_QTIO OL
WHERE OL.CD_COMPANY = @P_CD_COMPANY
AND EXISTS (SELECT 1 
		    FROM MM_GIREQL QL
			WHERE QL.CD_COMPANY = OL.CD_COMPANY
			AND QL.NO_GIREQ = OL.NO_ISURCV
			AND QL.NO_LINE = OL.NO_ISURCVLINE
			AND QL.NO_GIREQ = @P_NO_GIREQ
			AND QL.CD_SL = 'SL_STND')

SELECT QL.CD_COMPANY, 
	   QL.NO_GIREQ,
	   QL.NO_LINE INTO #MM_GIREQL
FROM MM_GIREQL QL
WHERE QL.CD_COMPANY = @P_CD_COMPANY
AND QL.NO_GIREQ = @P_NO_GIREQ
AND QL.CD_SL = 'SL_STND'

DELETE OL
FROM MM_QTIO OL
WHERE EXISTS (SELECT 1 
			  FROM #MM_QTIO OL1 
			  WHERE OL1.CD_COMPANY = OL.CD_COMPANY 
			  AND OL1.NO_IO = OL.NO_IO 
			  AND OL1.NO_IOLINE = OL.NO_IOLINE)

DELETE QL
FROM MM_GIREQL QL
WHERE EXISTS (SELECT 1 
			  FROM #MM_GIREQL QL1 
			  WHERE QL1.CD_COMPANY = QL.CD_COMPANY 
			  AND QL1.NO_GIREQ = QL.NO_GIREQ 
			  AND QL1.NO_LINE = QL.NO_LINE)

DELETE OH
FROM MM_QTIOH OH
WHERE EXISTS (SELECT 1 
			  FROM #MM_QTIO OL
			  WHERE OL.CD_COMPANY = OH.CD_COMPANY
			  AND OL.NO_IO = OH.NO_IO)
AND NOT EXISTS (SELECT 1 
				FROM MM_QTIO OL
				WHERE OL.CD_COMPANY = OH.CD_COMPANY
			    AND OL.NO_IO = OH.NO_IO)

DELETE QH 
FROM MM_GIREQH QH
WHERE EXISTS (SELECT 1 
			  FROM #MM_GIREQL QL
			  WHERE QL.CD_COMPANY = QH.CD_COMPANY
			  AND QL.NO_GIREQ = QH.NO_GIREQ)
AND NOT EXISTS (SELECT 1 
				FROM MM_GIREQL QL
			    WHERE QL.CD_COMPANY = QH.CD_COMPANY
			    AND QL.NO_GIREQ = QH.NO_GIREQ)

COMMIT TRAN SP_CZ_PR_STOCK_GI_D

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_PR_STOCK_GI_D
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN

ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK TRAN SP_CZ_PR_STOCK_GI_D

GO