USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_CZ_PR_OPOUT_REG_UPDATE]    Script Date: 2022-08-01 오후 2:15:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[UP_CZ_PR_OPOUT_REG_UPDATE]
(
    @P_CD_COMPANY   NVARCHAR(7),
	@P_CD_PLANT     NVARCHAR(7),
    @P_NO_WO        NVARCHAR(20),    
	@P_CD_OP		NVARCHAR(4),
	@P_QT_PR		NUMERIC(17,4),
	@P_ID_UPDATE	NVARCHAR(15),
	@P_DC_RMK		NVARCHAR(MAX)
)
AS

DECLARE 
    @ERRMSG         NVARCHAR(255), 
    @P_DTS_UPDATE   VARCHAR(14)

SET @P_DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())

UPDATE	CZ_PR_OPOUT_PR
SET		QT_PR	   = @P_QT_PR,
		DC_RMK     = @P_DC_RMK, 
		ID_UPDATE  = @P_ID_UPDATE, 
		DTS_UPDATE = @P_DTS_UPDATE
WHERE	CD_COMPANY = @P_CD_COMPANY
AND		CD_PLANT   = @P_CD_PLANT
AND		NO_WO      = @P_NO_WO
AND		CD_OP	   = @P_CD_OP

IF (@@ERROR <> 0 ) BEGIN SELECT @ERRMSG = '[UP_CZ_PR_OPOUT_REG_UPDATE]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

RETURN
ERROR: RAISERROR (@ERRMSG, 18, 1)
GO

