USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_CZ_PR_OPOUT_REG_ID_DELETE]    Script Date: 2022-09-01 오후 1:18:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[UP_CZ_PR_OPOUT_REG_ID_DELETE]
(
    @P_CD_COMPANY   NVARCHAR(7),
    @P_NO_WO        NVARCHAR(20),    
	@P_NO_WO_LINE	NUMERIC(5,0),
	@P_SEQ_WO		NUMERIC(5,0)
)
AS

DECLARE 
    @ERRMSG         NVARCHAR(255)

DELETE CZ_PR_WO_INSP
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_WO = @P_NO_WO
AND NO_LINE = @P_NO_WO_LINE
AND SEQ_WO = @P_SEQ_WO
AND NO_INSP = 994
AND NO_OPOUT_PO IS NULL

IF (@@ERROR <> 0 ) BEGIN SELECT @ERRMSG = '[UP_CZ_PR_OPOUT_REG_ID_DELETE]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

RETURN
ERROR: RAISERROR (@ERRMSG, 18, 1)
GO


