USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_BANK_SEND_PRINT_LOG_I]    Script Date: 2017-01-19 오전 11:18:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_FI_BANK_SEND_PRINT_LOG_I]
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_TRANS_DATE			NVARCHAR(8),
	@P_TRANS_SEQ			NVARCHAR(16),
	@P_SEQ					NVARCHAR(16),
	@P_CUST_CODE			NVARCHAR(20),
	@P_TRANS_BANK_CODE		NVARCHAR(10),
	@P_TRANS_ACCT_NO		NVARCHAR(40),
	@P_TRANS_NAME			NVARCHAR(100),
	@P_CD_EXCH				NVARCHAR(3),
	@P_TRANS_AMT_EX			NUMERIC(19, 4),
	@P_TRANS_AMT			NUMERIC(19, 4),
    @P_CLIENT_NOTE			NVARCHAR(100),
	@P_TRANS_NOTE			NVARCHAR(100),
	@P_NM_BANK_EN			NVARCHAR(200),
	@P_CD_BANK_NATION		NVARCHAR(3),
	@P_NO_SORT				NVARCHAR(20),
	@P_NO_SWIFT				NVARCHAR(20),
	@P_DC_DEPOSIT_TEL		NVARCHAR(50),
	@P_CD_DEPOSIT_NATION	NVARCHAR(3),
	@P_DC_DEPOSIT_ADDRESS	NVARCHAR(200),
	@P_NO_BANK_BIC			NVARCHAR(20),
	@P_TP_CHARGE			NVARCHAR(3),
	@P_TP_SEND_BY			NVARCHAR(3),
	@P_DC_RELATION			NVARCHAR(50),
    @P_ID_PRINT				NVARCHAR(15)
)
AS

DECLARE @V_NO_HST NUMERIC(5, 0)

SELECT @V_NO_HST = (ISNULL(MAX(NO_HST), 0) + 1)
FROM CZ_FI_BANK_SEND_PRINT_LOG
WHERE CD_COMPANY = @P_CD_COMPANY
AND TRANS_DATE = @P_TRANS_DATE
AND TRANS_SEQ = @P_TRANS_SEQ
AND SEQ = @P_SEQ
GROUP BY CD_COMPANY, TRANS_DATE, TRANS_SEQ, SEQ

IF ISNULL(@V_NO_HST, 0) = 0
	SET @V_NO_HST = 1

INSERT INTO CZ_FI_BANK_SEND_PRINT_LOG
(
	CD_COMPANY,
	TRANS_DATE,
	TRANS_SEQ,
	SEQ,
	NO_HST,
	CUST_CODE,
	TRANS_BANK_CODE,
	TRANS_ACCT_NO,
	TRANS_NAME,
	CD_EXCH,
	TRANS_AMT_EX,
	TRANS_AMT, 
    CLIENT_NOTE,
	TRANS_NOTE,
	NM_BANK_EN,
	CD_BANK_NATION,
	NO_SORT,
	NO_SWIFT,
	DC_DEPOSIT_TEL,
	CD_DEPOSIT_NATION,
	DC_DEPOSIT_ADDRESS,
	NO_BANK_BIC,
	TP_CHARGE,
	TP_SEND_BY,
	DC_RELATION,
	ID_PRINT,
	DTS_PRINT
)
VALUES
(
	@P_CD_COMPANY,
	@P_TRANS_DATE,
	@P_TRANS_SEQ,
	@P_SEQ,
	@V_NO_HST,
	@P_CUST_CODE,
	@P_TRANS_BANK_CODE,
	@P_TRANS_ACCT_NO,
	@P_TRANS_NAME,
	@P_CD_EXCH,
	@P_TRANS_AMT_EX,
	@P_TRANS_AMT, 
    @P_CLIENT_NOTE,
	@P_TRANS_NOTE,
	@P_NM_BANK_EN,
	@P_CD_BANK_NATION,
	@P_NO_SORT,
	@P_NO_SWIFT,
	@P_DC_DEPOSIT_TEL,
	@P_CD_DEPOSIT_NATION,
	@P_DC_DEPOSIT_ADDRESS,
	@P_NO_BANK_BIC,
	@P_TP_CHARGE,
	@P_TP_SEND_BY,
	@P_DC_RELATION,
	@P_ID_PRINT,
	NEOE.SF_SYSDATE(GETDATE())
)

UPDATE BANK_SENDH
SET SETTLE_YN = 'Y',
	SETTLE_DATE = CONVERT(CHAR(8), GETDATE(), 112)
WHERE C_CODE = @P_CD_COMPANY
AND TRANS_DATE = @P_TRANS_DATE
AND TRANS_SEQ = @P_TRANS_SEQ
AND SEQ = @P_SEQ

GO

