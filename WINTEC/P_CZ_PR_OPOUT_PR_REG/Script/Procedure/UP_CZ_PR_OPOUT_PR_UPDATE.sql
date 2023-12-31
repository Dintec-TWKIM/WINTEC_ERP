USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_CZ_PR_OPOUT_PR_UPDATE]    Script Date: 2022-10-19 오후 5:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[UP_CZ_PR_OPOUT_PR_UPDATE]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_PLANT     NVARCHAR(7),
	@P_NO_PR        NVARCHAR(20),
	@P_NO_LINE      NUMERIC(5,0),
	@P_DT_PR		NVARCHAR(8),
	@P_NO_EMP		NVARCHAR(10),
	@P_NO_WO        NVARCHAR(20),
	@P_CD_OP        NVARCHAR(4),
	@P_CD_WC        NVARCHAR(8),
	@P_CD_WCOP      NVARCHAR(4),
	@P_CD_ITEM      NVARCHAR(20),
	@P_QT_PR        NUMERIC(17,4),
	@P_DT_DUE		NVARCHAR(8),
	@P_CD_PARTNER	NVARCHAR(20),
	@P_DC_RMK       NVARCHAR(MAX),
	@P_ID_UPDATE    NVARCHAR(15)
)
AS

DECLARE 
    @ERRMSG         NVARCHAR(255), 
    @P_DTS_UPDATE   NVARCHAR(14)

SET @P_DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())

UPDATE	CZ_PR_OPOUT_PR
SET		DT_PR	   = @P_DT_PR,
		NO_EMP	   = @P_NO_EMP,
		NO_WO      = @P_NO_WO, 
		CD_OP      = @P_CD_OP, 
		CD_WC      = @P_CD_WC, 
		CD_WCOP    = @P_CD_WCOP, 
		CD_ITEM    = @P_CD_ITEM, 
		QT_PR      = @P_QT_PR, 
		DT_DUE	   = @P_DT_DUE,
		CD_PARTNER = @P_CD_PARTNER,
		DC_RMK     = @P_DC_RMK, 
		ID_UPDATE  = @P_ID_UPDATE, 
		DTS_UPDATE = @P_DTS_UPDATE
WHERE	CD_COMPANY = @P_CD_COMPANY
AND		CD_PLANT   = @P_CD_PLANT
AND		NO_PR      = @P_NO_PR
AND		NO_LINE    = @P_NO_LINE

IF (@@ERROR <> 0 ) BEGIN SELECT @ERRMSG = '[UP_CZ_PR_OPOUT_PRL_UPDATE]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

RETURN
ERROR: RAISERROR (@ERRMSG, 18, 1)
