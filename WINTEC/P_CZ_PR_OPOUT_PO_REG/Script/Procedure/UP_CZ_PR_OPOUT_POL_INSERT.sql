USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_CZ_PR_OPOUT_POL_INSERT]    Script Date: 2022-12-09 오후 4:50:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [NEOE].[UP_CZ_PR_OPOUT_POL_INSERT]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_PLANT     NVARCHAR(7),
	@P_NO_PO        NVARCHAR(20),
	@P_NO_LINE      NUMERIC(5,0),
	@P_NO_WO        NVARCHAR(20),
	@P_CD_OP        NVARCHAR(4),
	@P_CD_WC        NVARCHAR(7),
	@P_CD_WCOP      NVARCHAR(7),
	@P_CD_ITEM      NVARCHAR(50),
	@P_DT_DUE       NVARCHAR(8),
	@P_QT_PO        NUMERIC(17,4),
	@P_QT_RCV       NUMERIC(17,4),
	@P_QT_CLS       NUMERIC(17,4),
	@P_UM_EX        NUMERIC(19,6),
	@P_AM_EX        NUMERIC(19,6),
	@P_UM           NUMERIC(19,6),
	@P_AM           NUMERIC(17,4),
	@P_AM_VAT       NUMERIC(17,4),
	@P_DC_RMK       NVARCHAR(100),
	@P_ID_INSERT    NVARCHAR(15),
	@P_UM_MATL      NUMERIC(19,6),
	@P_UM_SOUL      NUMERIC(19,6),
	@P_OLD_QT_PO	NUMERIC(17,4) = NULL, --변환전 수량
	@P_UNIT_CH		NVARCHAR(3)   = NULL, --변환단위
	@P_QT_CHCOEF	NUMERIC(17,6) = NULL, --변환계수
	@P_NO_WORK		NVARCHAR(20)  = NULL,  --실적번호
	@P_NO_PR		NVARCHAR(20)
)
AS

DECLARE 
	@ERRMSG         NVARCHAR(255), 
    @P_DTS_INSERT   VARCHAR(14)

SET @P_DTS_INSERT = NEOE.SF_SYSDATE(GETDATE())

INSERT INTO PR_OPOUT_POL
(
CD_COMPANY,				CD_PLANT,			NO_PO,				NO_LINE,			NO_WO,				CD_OP, 
CD_WC,					CD_WCOP,			CD_ITEM,			DT_DUE,				QT_PO,				QT_RCV, 
QT_CLS,					UM_EX,				AM_EX,				UM,					AM,					AM_VAT, 
DC_RMK,					ID_INSERT,			DTS_INSERT,			UM_MATL,			UM_SOUL,			OLD_QT_PO,
UNIT_CH,				QT_CHCOEF,			NO_WORK,			NO_PR
)
VALUES
(
@P_CD_COMPANY,			@P_CD_PLANT,		@P_NO_PO,			@P_NO_LINE,			@P_NO_WO,			@P_CD_OP, 
@P_CD_WC,				@P_CD_WCOP,			@P_CD_ITEM,			@P_DT_DUE,			@P_QT_PO,			@P_QT_RCV, 
@P_QT_CLS,				@P_UM_EX,			@P_AM_EX,			@P_UM,				@P_AM,				@P_AM_VAT, 
@P_DC_RMK,				@P_ID_INSERT,		@P_DTS_INSERT,		@P_UM_MATL,			@P_UM_SOUL,			@P_OLD_QT_PO,
@P_UNIT_CH,				@P_QT_CHCOEF,		@P_NO_WORK,			@P_NO_PR
)

IF (@@ERROR <> 0 ) BEGIN SELECT @ERRMSG = '[UP_PR_OPOUT_POL_INSERT]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

RETURN
ERROR: RAISERROR(@ERRMSG, 18, 1)