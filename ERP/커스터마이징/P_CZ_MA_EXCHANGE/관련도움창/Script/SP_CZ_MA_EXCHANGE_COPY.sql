USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_EXCHANGE_COPY]    Script Date: 2015-12-18 오후 5:14:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MA_EXCHANGE_COPY]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_NO_EMP		NVARCHAR(15),
	@P_NO_SEQ		NUMERIC(2, 0),
	@P_YM			NVARCHAR(8),
	@P_YM_COPY_F	NVARCHAR(8),
	@P_YM_COPY_T	NVARCHAR(8)
) AS
-- ================================================
-- AUTHOR      : 이대성
-- CREATE DATE : 2010.06.15
--               
-- MODULE      : 시스템관리
-- SYSTEM      : 시스템기준정보
-- SUBSYSTEM   : 
-- PAGE        : 환율관리정보 복사
-- PROJECT     : P_MA_EXCHANGE_SUB
-- DESCRIPTION : 기준년월에 있는 환율정보 복사
-- ================================================ 
-- CHANGE HISTORY
-- v1.0 : 2010.06.15 프로시저 신규 생성
-- v1.1 : 2010.11.29 이대성 수정 - UI에서 처리하던 로직 프로시저로 뺌.
-- ================================================
SET NOCOUNT ON

DECLARE	@V_DIFF		INT,
		@V_CNT		INT,
		@V_YYMMDD	NVARCHAR(8)

SET @V_DIFF = DATEDIFF(DAY, @P_YM_COPY_F, @P_YM_COPY_T)
SET @V_CNT = 0

WHILE (@V_CNT <= @V_DIFF)
BEGIN
	SET @V_YYMMDD = CONVERT(NVARCHAR(8), DATEADD(DAY, @V_CNT, @P_YM_COPY_F), 112)
	
	DELETE FROM MA_EXCHANGE
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND	YYMMDD = @V_YYMMDD
	AND	NO_SEQ = @P_NO_SEQ

	INSERT INTO MA_EXCHANGE 
	(
		YYMMDD,
		CURR_SOUR,
		CURR_DEST, 
		CD_COMPANY, 
		RATE_BASE, 
		RATE_SALE, 
		RATE_BUY,
		ID_INSERT, 
		DTS_INSERT, 
		NO_SEQ, 
		QUOTATION_TIME
	)
	SELECT @V_YYMMDD AS YYMMDD, 
		   CURR_SOUR, 
		   CURR_DEST, 
		   CD_COMPANY, 
		   RATE_BASE, 
		   RATE_SALE, 
		   RATE_BUY, 
		   @P_NO_EMP AS ID_INSERT, 
		   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT, 
		   NO_SEQ, 
		   QUOTATION_TIME
	FROM MA_EXCHANGE
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND	YYMMDD = @P_YM
	AND	NO_SEQ = @P_NO_SEQ

	DELETE FROM CZ_MA_EXCHANGE
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND YYMMDD = @V_YYMMDD
	AND NO_SEQ = @P_NO_SEQ

	INSERT INTO CZ_MA_EXCHANGE
	(
		CD_COMPANY,
		YYMMDD, 
		NO_SEQ, 
		CURR_SOUR, 
		CURR_DEST,
		RATE_PURCHASE, 
		RATE_SALES, 
		ID_INSERT, 
		DTS_INSERT
	)
	SELECT CD_COMPANY,
		   @V_YYMMDD AS YYMMDD,
		   NO_SEQ,
		   CURR_SOUR, 
		   CURR_DEST,
		   RATE_PURCHASE, 
		   RATE_SALES, 
		   @P_NO_EMP AS ID_INSERT, 
		   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
	FROM CZ_MA_EXCHANGE
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND	YYMMDD = @P_YM
	AND	NO_SEQ = @P_NO_SEQ

	SET @V_CNT = @V_CNT + 1
END

SET NOCOUNT OFF
GO

