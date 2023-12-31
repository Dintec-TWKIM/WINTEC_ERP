USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CLAIMH_S]    Script Date: 2015-04-14 오전 8:31:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_SA_CLAIMH_I]
(
	@P_CD_COMPANY				NVARCHAR(7),
	@P_NO_CLAIM					NVARCHAR(8), 
    @P_NO_SO					NVARCHAR(20), 
    @P_CD_STATUS				NVARCHAR(1), 
    @P_NO_IMO					NVARCHAR(10), 
    @P_CD_PARTNER				NVARCHAR(20), 
	@P_NO_SALES_EMP				NVARCHAR(10), 
    @P_TP_CLAIM					NVARCHAR(3), 
    @P_TP_CAUSE					NVARCHAR(3), 
    @P_TP_ITEM					NVARCHAR(3), 
    @P_AM_ITEM_RCV				NUMERIC(14, 2), 
    @P_AM_ADD_RCV				NUMERIC(14, 2),
	@P_AM_ITEM_PRO				NUMERIC(14, 2), 
    @P_AM_ADD_PRO				NUMERIC(14, 2),
	@P_AM_ITEM_CLS				NUMERIC(14, 2), 
    @P_AM_ADD_CLS				NUMERIC(14, 2),
    @P_DC_PROGRESS				NVARCHAR(MAX), 
    @P_DC_RESULT				NVARCHAR(MAX), 
    @P_DC_PREVENTION			NVARCHAR(MAX), 
    @P_DC_CLOSING				NVARCHAR(MAX),
	@P_NO_EMP					NVARCHAR(10),
	@P_DT_INPUT					NVARCHAR(8),
	@P_DT_EXPECTED_CLOSING_PRO	NVARCHAR(8),
	@P_DT_EXPECTED_CLOSING		NVARCHAR(8),
	@P_DT_CLOSING				NVARCHAR(8),
	@P_CD_SUPPLIER_REWARD		NVARCHAR(3),
	@P_DC_SUPPLIER_REWARD		NVARCHAR(MAX),
	@P_DC_CREDIT_NOTE			NVARCHAR(MAX),
	@P_CD_CREDIT_EXCH			NVARCHAR(4),
	@P_AM_CREDIT				NUMERIC(14, 2), 
	@P_TP_REASON				NVARCHAR(4),
	@P_TP_RETURN				NVARCHAR(4),
	@P_TP_REQUEST				NVARCHAR(4),
	@P_DC_RECEIVE				NVARCHAR(MAX),
	@P_IMAGE1					NVARCHAR(100),
	@P_IMAGE2					NVARCHAR(100),
	@P_IMAGE3					NVARCHAR(100),
	@P_IMAGE4					NVARCHAR(100),
	@P_IMAGE5					NVARCHAR(100),
	@P_IMAGE6					NVARCHAR(100),
	@P_DC_IMAGE1				NVARCHAR(100),
	@P_DC_IMAGE2				NVARCHAR(100),
	@P_DC_IMAGE3				NVARCHAR(100),
	@P_DC_IMAGE4				NVARCHAR(100),
	@P_DC_IMAGE5				NVARCHAR(100),
	@P_DC_IMAGE6				NVARCHAR(100),
	@P_QT_PACK_WEIGHT			NUMERIC(14, 2),
	@P_DC_PACK_SIZE				NVARCHAR(100),
	@P_ID_INSERT				NVARCHAR(15)
) 
AS

IF ISNULL(@P_NO_CLAIM, '') = '' OR LEN(@P_NO_CLAIM) < 8
BEGIN
	DECLARE @V_DC_MSG NVARCHAR(500)
	SET @V_DC_MSG = '[SP_CZ_SA_CLAIMH_I] 클레임번호가 잘못 되었습니다.' + ' [클레임번호 : ' + @P_NO_CLAIM + ']'

	RAISERROR(@V_DC_MSG, 16, 1)
	RETURN
END

INSERT INTO CZ_SA_CLAIMH 
(
	CD_COMPANY, 
	NO_CLAIM, 
	NO_SO, 
	CD_STATUS, 
	NO_IMO, 
	CD_PARTNER, 
	NO_SALES_EMP,
	TP_CLAIM, 
	TP_CAUSE, 
	TP_ITEM, 
	AM_ITEM_RCV, 
	AM_ADD_RCV, 
	AM_ITEM_PRO, 
	AM_ADD_PRO, 
	AM_ITEM_CLS, 
	AM_ADD_CLS,  
	DC_PROGRESS, 
	DC_RESULT, 
	DC_PREVENTION, 
	DC_CLOSING, 
	NO_EMP,
	DT_INPUT, 
	DT_EXPECTED_CLOSING_PRO,
	DT_EXPECTED_CLOSING,
	DT_CLOSING,
	CD_SUPPLIER_REWARD,
	DC_SUPPLIER_REWARD,
	DC_CREDIT_NOTE,
	CD_CREDIT_EXCH,
	AM_CREDIT,
	TP_REASON,
	TP_RETURN,
	TP_REQUEST,
	DC_RECEIVE,
	IMAGE1,
	IMAGE2,
	IMAGE3,
	IMAGE4,
	IMAGE5,
	IMAGE6,
	DC_IMAGE1,
	DC_IMAGE2,
	DC_IMAGE3,
	DC_IMAGE4,
	DC_IMAGE5,
	DC_IMAGE6,
	QT_PACK_WEIGHT,
	DC_PACK_SIZE,
	ID_INSERT, 
	DTS_INSERT
)
VALUES
(
	@P_CD_COMPANY, 
	@P_NO_CLAIM, 
	@P_NO_SO, 
	@P_CD_STATUS, 
	@P_NO_IMO, 
	@P_CD_PARTNER, 
	@P_NO_SALES_EMP,
	@P_TP_CLAIM, 
	@P_TP_CAUSE, 
	@P_TP_ITEM, 
	@P_AM_ITEM_RCV, 
	@P_AM_ADD_RCV, 
	@P_AM_ITEM_PRO, 
	@P_AM_ADD_PRO, 
	@P_AM_ITEM_CLS, 
	@P_AM_ADD_CLS, 
	@P_DC_PROGRESS, 
	@P_DC_RESULT, 
	@P_DC_PREVENTION, 
	@P_DC_CLOSING,
	@P_NO_EMP,
	@P_DT_INPUT, 
	@P_DT_EXPECTED_CLOSING_PRO,
	@P_DT_EXPECTED_CLOSING,
	@P_DT_CLOSING,
	@P_CD_SUPPLIER_REWARD,
	@P_DC_SUPPLIER_REWARD,
	@P_DC_CREDIT_NOTE,
	@P_CD_CREDIT_EXCH,
	@P_AM_CREDIT,
	@P_TP_REASON,
	@P_TP_RETURN,
	@P_TP_REQUEST,
	@P_DC_RECEIVE,
	@P_IMAGE1,
	@P_IMAGE2,
	@P_IMAGE3,
	@P_IMAGE4,
	@P_IMAGE5,
	@P_IMAGE6,
	@P_DC_IMAGE1,
	@P_DC_IMAGE2,
	@P_DC_IMAGE3,
	@P_DC_IMAGE4,
	@P_DC_IMAGE5,
	@P_DC_IMAGE6,
	@P_QT_PACK_WEIGHT,
	@P_DC_PACK_SIZE,
	@P_ID_INSERT, 
	NEOE.SF_SYSDATE(GETDATE())
)

GO