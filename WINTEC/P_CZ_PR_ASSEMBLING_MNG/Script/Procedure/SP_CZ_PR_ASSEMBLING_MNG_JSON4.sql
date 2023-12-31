SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_ASSEMBLING_MNG_JSON4]          
(
	@P_JSON			NVARCHAR(MAX),
	@P_ID_USER		NVARCHAR(10)	
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_ERRMSG NVARCHAR(255)   -- ERROR �޽���

BEGIN TRAN SP_CZ_PR_ASSEMBLING_MNG_JSON4
BEGIN TRY

SELECT * INTO #JSON_I
FROM OPENJSON(@P_JSON)
WITH
(
	CD_COMPANY      NVARCHAR(7),
	NO_WO           NVARCHAR(20),
	NO_LINE         NUMERIC(5, 0),
	SEQ_WO          NUMERIC(5, 0),
	NO_SO           NVARCHAR(20),
	TXT_SHIM     NVARCHAR(500),
	JSON_FLAG		NVARCHAR(1)
) JS
WHERE JS.JSON_FLAG = 'I'

INSERT INTO CZ_PR_WO_INSP
(
	CD_COMPANY,  
	NO_WO, 
	NO_LINE,     
	SEQ_WO,    
	NO_INSP,     
	NO_SO,
	TXT_SHIM, 
	DTS_INSERT,  
	ID_INSERT
)
SELECT CD_COMPANY,  
	   NO_WO, 
	   NO_LINE,     
	   SEQ_WO,    
	   996 AS NO_INSP,     
	   NO_SO,
	   TXT_SHIM, 
	   @P_ID_USER,
	   NEOE.SF_SYSDATE(GETDATE())
FROM #JSON_I

SELECT * INTO #JSON_U
FROM OPENJSON(@P_JSON)
WITH
(
	CD_COMPANY      NVARCHAR(7),
	NO_WO           NVARCHAR(20),
	NO_LINE         NUMERIC(5, 0),
	SEQ_WO          NUMERIC(5, 0),
	TXT_SHIM        NVARCHAR(500),
	JSON_FLAG		NVARCHAR(1)
) JS
WHERE JS.JSON_FLAG = 'U'

UPDATE WI
SET WI.TXT_SHIM = JS.TXT_SHIM,
	WI.ID_UPDATE = @P_ID_USER,
	WI.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_PR_WO_INSP WI
JOIN #JSON_U JS 
ON JS.CD_COMPANY = WI.CD_COMPANY AND JS.NO_WO = WI.NO_WO AND JS.NO_LINE = WI.NO_LINE AND JS.SEQ_WO = WI.SEQ_WO AND 996 = WI.NO_INSP

DROP TABLE #JSON_I
DROP TABLE #JSON_U

COMMIT TRAN SP_CZ_PR_ASSEMBLING_MNG_JSON4

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_PR_ASSEMBLING_MNG_JSON4
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN
ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK TRAN SP_CZ_PR_ASSEMBLING_MNG_JSON4

GO
