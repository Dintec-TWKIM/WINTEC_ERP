USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_PARTNER_REQ_CONFIRM]    Script Date: 2016-08-22 오후 8:23:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_MA_PARTNER_REQ_CONFIRM]
(
	@P_CD_COMPANY 	NVARCHAR(7),
	@P_NO_REQ		NVARCHAR(20),
	@P_TP_CONFIRM	NVARCHAR(1),
	@P_ID_INSERT	NVARCHAR(15),
	@P_CD_PARTNER	NVARCHAR(20) OUTPUT
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_ERRMSG		VARCHAR(255),   -- ERROR 메시지
		@V_RANGE_FROM	NVARCHAR(5),
		@V_RANGE_TO		NVARCHAR(5)

IF @P_CD_COMPANY = 'K100'
BEGIN
	SET @V_RANGE_FROM = '18000'
	SET @V_RANGE_TO = '19000'
END
ELSE IF @P_CD_COMPANY = 'K200'
BEGIN
	SET @V_RANGE_FROM = '15000'
	SET @V_RANGE_TO = '16000'
END
ELSE IF @P_CD_COMPANY = 'S100'
BEGIN
	SET @V_RANGE_FROM = '13000'
	SET @V_RANGE_TO = '14000'
END
ELSE IF @P_CD_COMPANY = 'W100'
BEGIN
	SET @V_RANGE_FROM = '20000'
	SET @V_RANGE_TO = '21000'
END
ELSE
BEGIN
	SET @V_RANGE_FROM = '18000'
	SET @V_RANGE_TO = '19000'
END

IF EXISTS (SELECT 1 
		   FROM CZ_MA_PARTNER_REQ
		   WHERE CD_COMPANY = @P_CD_COMPANY
		   AND NO_REQ = @P_NO_REQ
		   AND CD_PARTNER IS NOT NULL)
BEGIN
	IF @P_TP_CONFIRM = 'C'
		SELECT @V_ERRMSG = '이미 승인된 건 입니다.'
	ELSE
		SELECT @V_ERRMSG = '이미 반려된 건 입니다.'
	
	GOTO ERROR
END

BEGIN TRAN SP_CZ_MA_PARTNER_REQ_CONFIRM
BEGIN TRY

IF @P_TP_CONFIRM = 'C'
BEGIN
	--SELECT DISTINCT TOP 1 @P_CD_PARTNER = (CD_PARTNER + 1)
	--FROM MA_PARTNER
	--WHERE CD_COMPANY <> 'TEST' 
	--AND ISNUMERIC(CD_PARTNER) = 1
	--AND CD_PARTNER BETWEEN @V_RANGE_FROM AND @V_RANGE_TO
	--AND (CD_PARTNER + 1) NOT IN (SELECT CD_PARTNER 
	--							 FROM MA_PARTNER
	--							 WHERE CD_COMPANY <> 'TEST' 
	--							 AND ISNUMERIC(CD_PARTNER) = 1
	--							 AND CD_PARTNER BETWEEN @V_RANGE_FROM AND @V_RANGE_TO)
	--ORDER BY (CD_PARTNER + 1)
	
	SELECT @P_CD_PARTNER = (ISNULL(MAX(CD_PARTNER), @V_RANGE_FROM) + 1) 
	FROM MA_PARTNER
	WHERE CD_COMPANY <> 'TEST'
	AND CD_PARTNER BETWEEN @V_RANGE_FROM AND @V_RANGE_TO
	
	INSERT INTO MA_PARTNER
	(
		CD_COMPANY,
		CD_PARTNER,
		LN_PARTNER,
		NO_COMPANY,
		NO_RES,
		FG_PARTNER,
		CLS_PARTNER,
		NM_CEO,
		CD_AREA,
		CD_NATION,
		NO_POST1,
		DC_ADS1_H,
		DC_ADS1_D,
		CD_PARTNER_GRP,
		CD_PARTNER_GRP_2,
		NO_TEL1,
		NO_FAX1,
		TP_JOB,
		CLS_JOB,
		CD_EMP_PARTNER,
		NO_TEL,
		NO_HPEMP_PARTNER,
		E_MAIL,
		NO_FAX,
		CD_BANK,
		NO_DEPOSIT,
		NM_DEPOSIT,
		NM_TEXT,
		USE_YN,
		DT_RCP_PREARRANGED,
		SN_PARTNER,
		TP_PARTNER,
		YN_JEONJA,
		DT_PAY_PREARRANGED,
		ID_INSERT,
		DTS_INSERT
	)
	SELECT MP.CD_COMPANY,
		   @P_CD_PARTNER,
		   MP.LN_PARTNER,
		   MP.NO_COMPANY,
		   MP.NO_RES,
		   MP.FG_PARTNER,
		   MP.CLS_PARTNER,
		   MP.NM_CEO,
		   MP.CD_AREA,
		   MP.CD_NATION,
		   MP.NO_POST1,
		   MP.DC_ADS1_H,
		   MP.DC_ADS1_D,
		   MP.CD_PARTNER_GRP,
		   MP.CD_PARTNER_GRP_2,
		   MP.NO_TEL1,
		   MP.NO_FAX1,
		   MP.TP_JOB,
		   MP.CLS_JOB,
		   MP.CD_EMP_PARTNER,
		   MP.NO_TEL,
		   MP.NO_HPEMP_PARTNER,
		   MP.E_MAIL,
		   MP.NO_FAX,
		   MP.CD_BANK,
		   MP.NO_DEPOSIT,
		   MP.NM_DEPOSIT,
		   MP.NM_TEXT,
		   'Y',
		   30,
		   MP.LN_PARTNER,
		   'MIX',
		   ISNULL(MP.YN_JEONJA, 'Y') AS YN_JEONJA,
		   MC.CD_FLAG1,
		   @P_ID_INSERT,
		   NEOE.SF_SYSDATE(GETDATE()) 
	FROM CZ_MA_PARTNER_REQ MP
	LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = MP.CD_COMPANY AND MC.CD_FIELD = 'PU_C000014' AND MC.CD_SYSDEF = MP.FG_PAYMENT
	WHERE MP.CD_COMPANY = @P_CD_COMPANY
	AND MP.NO_REQ = @P_NO_REQ
	
	INSERT INTO CZ_MA_PARTNER
	(
		CD_COMPANY,
		CD_PARTNER,
		CD_TPPO,
		CD_EXCH2,
		FG_PAYMENT,
		TP_COND_PRICE,
		FG_TAX,
		RT_PURCHASE_DC,
		TP_SO,
		CD_EXCH1,
		TP_INQ_PO,
		TP_PO,
		TP_INQ,
		TP_QTN,
		TP_TERMS_PAYMENT,
		TP_DELIVERY_CONDITION,
		RT_SALES_PROFIT,
		RT_SALES_DC,
		RT_COMMISSION,
		DC_COMMISSION,
		TP_VAT,
		YN_USE_GIR,
		YN_GR_LABEL,
		TP_PRICE_SENS,
		YN_HOLD,
		YN_COLOR,
		TP_INV,
		ID_INSERT,
		DTS_INSERT
	)
	SELECT CD_COMPANY,
		   @P_CD_PARTNER,
		   CD_TPPO,
		   CD_EXCH2,
		   FG_PAYMENT,
		   TP_COND_PRICE,
		   FG_TAX,
		   RT_PURCHASE_DC,
		   TP_SO,
		   CD_EXCH1,
		   TP_INQ_SEND,
		   TP_PO_SEND,
		   TP_INQ_RCV,
		   TP_QTN_SEND,
		   TP_TERMS_PAYMENT,
		   TP_DELIVERY_CONDITION,
		   RT_SALES_PROFIT,
		   RT_SALES_DC,
		   RT_COMMISSION,
		   DC_COMMISSION,
		   TP_VAT,
		   'Y',
		   (CASE WHEN CD_AREA = '200' THEN 'N' ELSE 'Y' END) AS YN_GR_LABEL,
		   '002' AS TP_PRICE_SENS,
		   'N' AS YN_HOLD,
		   'N' AS YN_COLOR,
		   TP_INV,
		   @P_ID_INSERT,
		   NEOE.SF_SYSDATE(GETDATE()) 
	FROM CZ_MA_PARTNER_REQ
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND NO_REQ = @P_NO_REQ

	IF NOT EXISTS (SELECT 1 
			       FROM CZ_SRM_PARTNER
			       WHERE CD_COMPANY = @P_CD_COMPANY
			       AND CD_PARTNER = @P_CD_PARTNER)
	BEGIN
		INSERT INTO CZ_SRM_PARTNER
		(
			CD_COMPANY,
			CD_PARTNER,
			PASSWORD,
			ID_INSERT,
			DTS_INSERT
		)
		VALUES
		(
			@P_CD_COMPANY,
			@P_CD_PARTNER,
			'1234',
			@P_ID_INSERT,
			NEOE.SF_SYSDATE(GETDATE()) 
		)	
	END
	
	IF EXISTS (SELECT 1
			   FROM CZ_MA_PARTNER_REQ
			   WHERE CD_COMPANY = @P_CD_COMPANY
			   AND NO_REQ = @P_NO_REQ
			   AND CD_EMP_PARTNER IS NOT NULL)
	BEGIN
		INSERT INTO FI_PARTNERPTR
		(
			CD_COMPANY,
			CD_PARTNER,
			SEQ,
			USE_YN,
			TP_PTR,
			NM_PTR,
			NM_EMAIL,
			NO_HP,
			NO_TEL,
			NO_FAX,
			ID_INSERT,
			DTS_INSERT
		)
		SELECT CD_COMPANY,
			   @P_CD_PARTNER,
			   1,
			   'Y',
			   TP_PTR,
			   CD_EMP_PARTNER,
			   E_MAIL,
			   NO_HPEMP_PARTNER,
			   NO_TEL,
			   NO_FAX,
			   @P_ID_INSERT,
			   NEOE.SF_SYSDATE(GETDATE())
		FROM CZ_MA_PARTNER_REQ
		WHERE CD_COMPANY = @P_CD_COMPANY
		AND NO_REQ = @P_NO_REQ
	END

	IF EXISTS (SELECT 1
			   FROM CZ_MA_PARTNER_REQ
			   WHERE CD_COMPANY = @P_CD_COMPANY
			   AND NO_REQ = @P_NO_REQ
			   AND ISNULL(NO_DEPOSIT, '') <> '')
	BEGIN
		INSERT INTO CZ_MA_PARTNER_DEPOSIT
		(
			CD_COMPANY,
			CD_PARTNER,
			NO_DEPOSIT,
			YN_USE,
			CD_DEPOSIT,
			CD_BANK,
			NM_BANK,
			CD_BANK_NATION,
			NO_SORT,
			CD_DEPOSIT_NATION,
			NO_SWIFT,
			NM_DEPOSIT,
			DC_DEPOSIT_TEL,
			DC_DEPOSIT_ADDRESS,
			NO_BANK_BIC,
			DC_RMK,
			ID_INSERT,
			DTS_INSERT
		)
		SELECT CD_COMPANY,
			   @P_CD_PARTNER,
			   NO_DEPOSIT,
			   'Y',
			   CD_DEPOSIT,
			   CD_BANK,
			   NM_BANK,
			   CD_BANK_NATION,
			   NO_SORT,
			   CD_DEPOSIT_NATION,
			   NO_SWIFT,
			   NM_DEPOSIT,
			   DC_DEPOSIT_TEL,
			   DC_DEPOSIT_ADDRESS,
			   NO_BANK_BIC,
			   DC_RMK,
			   @P_ID_INSERT,
			   NEOE.SF_SYSDATE(GETDATE())
		FROM CZ_MA_PARTNER_REQ
		WHERE CD_COMPANY = @P_CD_COMPANY
	    AND NO_REQ = @P_NO_REQ

		UPDATE PD
		SET PD.CD_DEPOSIT = PD1.CD_DEPOSIT,
			PD.DC_RMK = PD1.DC_RMK 
		FROM MA_PARTNER_DEPOSIT PD
		LEFT JOIN CZ_MA_PARTNER_DEPOSIT PD1 ON PD1.CD_COMPANY = PD.CD_COMPANY AND PD1.CD_PARTNER = PD.CD_PARTNER
		WHERE PD.CD_COMPANY = @P_CD_COMPANY
		AND PD.CD_PARTNER = @P_CD_PARTNER
	END
	
	IF EXISTS (SELECT 1
			   FROM CZ_MA_PARTNER_REQ
			   WHERE CD_COMPANY = @P_CD_COMPANY
			   AND NO_REQ = @P_NO_REQ
			   AND ISNULL(NM_ORIGIN, '') <> '')
	BEGIN
		INSERT INTO CZ_MA_PARTNER_ORIGIN 
		(
			CD_COMPANY,
			CD_PARTNER,
			CD_ORIGIN,
			NM_ORIGIN,
			NM_ORIGIN_RPT,
			ID_INSERT,
			DTS_INSERT
		)
		SELECT CD_COMPANY,
			   @P_CD_PARTNER,
			   '1',
			   NM_ORIGIN,
			   NM_ORIGIN_RPT,
			   @P_ID_INSERT,
			   NEOE.SF_SYSDATE(GETDATE())
		FROM CZ_MA_PARTNER_REQ
		WHERE CD_COMPANY = @P_CD_COMPANY
	    AND NO_REQ = @P_NO_REQ
	END

	UPDATE CZ_MA_PARTNER_REQ
	SET CD_PARTNER = @P_CD_PARTNER
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND NO_REQ = @P_NO_REQ	
END
ELSE
BEGIN
	UPDATE CZ_MA_PARTNER_REQ
	SET CD_PARTNER = 'REJECTED'
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND NO_REQ = @P_NO_REQ	
END

COMMIT TRAN SP_CZ_MA_PARTNER_REQ_CONFIRM

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_MA_PARTNER_REQ_CONFIRM
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN

ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK TRAN SP_CZ_MA_PARTNER_REQ_CONFIRM

GO

