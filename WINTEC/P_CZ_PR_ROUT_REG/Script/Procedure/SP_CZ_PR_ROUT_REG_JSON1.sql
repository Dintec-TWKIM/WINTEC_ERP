USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_ROUT_REG_JSON1]    Script Date: 2023-02-09 오후 2:38:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_ROUT_REG_JSON1]          
(
	@P_JSON			NVARCHAR(MAX),
	@P_ID_USER		NVARCHAR(10)	
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_ERRMSG NVARCHAR(255)   -- ERROR 메시지

BEGIN TRAN SP_CZ_PR_ROUT_REG_JSON1
BEGIN TRY

SELECT * INTO #JSON_U
FROM OPENJSON(@P_JSON)
WITH 
(
	CD_COMPANY      NVARCHAR(7),
	CD_ITEM			NVARCHAR(20),
	DC_RMK1			NVARCHAR(200),
	JSON_FLAG		NVARCHAR(1)
) JS
WHERE JS.JSON_FLAG = 'U'

UPDATE PI
SET PI.DC_RMK1 = JS.DC_RMK1,
	PI.ID_UPDATE = @P_ID_USER,
	PI.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM MA_PITEM PI
JOIN #JSON_U JS 
ON JS.CD_COMPANY = PI.CD_COMPANY AND JS.CD_ITEM = PI.CD_ITEM

DROP TABLE #JSON_U

COMMIT TRAN SP_CZ_PR_ROUT_REG_JSON1

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_PR_ROUT_REG_JSON1
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN
ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK TRAN SP_CZ_PR_ROUT_REG_JSON1

