USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_PARTNER_I]    Script Date: 2023-04-04 오후 5:41:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MA_PARTNER_I]
( 
	@P_CD_PARTNER 				NVARCHAR(20), 
	@P_CD_COMPANY   			NVARCHAR(7), 
	@P_LN_PARTNER    			NVARCHAR(50), 
	@P_DT_OPEN					NCHAR(8), 
	@P_AM_CAP         			NUMERIC(17,4), 
	@P_MANU_EMP      			NUMERIC(7,0), 
	@P_MANA_EMP       			NUMERIC(7,0), 
	@P_SD_PARTNER				NCHAR(8), 
	@P_LT_TRANS       			NUMERIC(3,0), 
	@P_SN_PARTNER   			NVARCHAR(30), 
	@P_EN_PARTNER    			NVARCHAR(50), 
	@P_TP_PARTNER    			NVARCHAR(3), 
	@P_FG_PARTNER    			NVARCHAR(3), 
	@P_NM_CEO         			NVARCHAR(40), 
	@P_NO_COMPANY  				NVARCHAR(20), 
	@P_NO_RES         			NVARCHAR(200), 
	@P_TP_JOB         			NVARCHAR(50), 
	@P_CLS_JOB        			NVARCHAR(50), 
	@P_URL_PARTNER  			NVARCHAR(40), 
	@P_CLS_PARTNER  			NVARCHAR(3), 
	@P_CD_NATION      			NVARCHAR(20), 
	@P_STA_CREDIT     			NVARCHAR(1), 
	@P_E_MAIL         			NVARCHAR(80), 
	@P_NO_TEL         			NVARCHAR(20), 
	@P_NO_FAX         			NVARCHAR(20), 
	@P_NO_POST1       			NVARCHAR(15), 
	@P_DC_ADS1_H      			NVARCHAR(500), 
	@P_DC_ADS1_D      			NVARCHAR(500), 
	@P_NO_TEL1        			NVARCHAR(20), 
	@P_NO_FAX1        			NVARCHAR(20), 
	@P_NO_POST2       			NVARCHAR(15), 
	@P_DC_ADS2_H      			NVARCHAR(500), 
	@P_DC_ADS2_D      			NVARCHAR(500), 
	@P_NO_TEL2        			NVARCHAR(20), 
	@P_NO_FAX2        			NVARCHAR(20), 
	@P_NO_POST3       			NVARCHAR(15), 
	@P_DC_ADS3_H      			NVARCHAR(500), 
	@P_DC_ADS3_D      			NVARCHAR(500), 
	@P_NO_TEL3        			NVARCHAR(20), 
	@P_NO_FAX3        			NVARCHAR(20), 
	@P_NM_PTR         			NVARCHAR(15), 
	@P_USE_YN         			NCHAR(1), 
	@P_FG_CREDIT      			NCHAR(1), 
	@P_TP_TAX         			NVARCHAR(3), 
	@P_NO_MERCHANT				NVARCHAR(20), 
	@P_NO_DEPOSIT				NVARCHAR(40), 
	@P_CD_BANK        			NVARCHAR(20), 
	@P_ID_COMPANY				NVARCHAR(20), 
	@P_PW_COMPANY				NVARCHAR(20),
	@P_CD_PARTNER_GRP			NVARCHAR(20),	
	@P_CD_EMP_SALE				NVARCHAR(14),	
	@P_CD_EMP_PARTNER			NVARCHAR(14),	
	@P_NO_HPEMP_PARTNER 		NVARCHAR(15),	
	@P_CD_AREA					NVARCHAR(3),	
	@P_DT_RCP_PREARRANGED		NUMERIC(3,0),	
	@P_NM_DEPOSIT				NVARCHAR(100),	
	@P_CD_PARTNER_GRP_2 		NVARCHAR(20),	
	@P_FG_BILL					NVARCHAR(30),	
	@P_FG_PAYBILL				NVARCHAR(3),	
	@P_DT_PAY_PREARRANGED		NUMERIC(5, 0),	
	@P_CD_CON					NVARCHAR(3),    
	@P_ID_INSERT      			NVARCHAR(15),
	@P_DT_CLOSE					NVARCHAR(8) = NULL,
	@P_YN_BIZTAX				NVARCHAR(1),
	@P_NO_BIZTAX				NVARCHAR(4),
	@P_YN_JEONJA				NVARCHAR(1),
	@P_TP_DEFER					NVARCHAR(1),
	@P_TP_RCP_DD				NVARCHAR(2),
	@P_DT_RCP_DD				NVARCHAR(2),
	@P_TP_PAY_DD				NVARCHAR(2),
	@P_DT_PAY_DD				NVARCHAR(2),
	@P_FG_CORP					NVARCHAR(1)	= NULL,
	@P_NM_CURE_AGENCY			NVARCHAR(100),
	@P_NO_CUER_AGENCY			NVARCHAR(100),
	@P_NM_TEXT					NVARCHAR(500),	-- kyila 추가 기타정보 비고 
	@P_TP_NATION				NVARCHAR(3),
	@P_NM_DCDEPOSIT				NVARCHAR(100),
	@P_FG_CORPCODE				NVARCHAR(10),
	@P_DT_RCP_PRETOLERANCE		NUMERIC(3,0),
	@P_NO_COR					NVARCHAR(13) = NULL,
	@P_CD_EXCH1					NVARCHAR(3),
	@P_TP_SO					NVARCHAR(4),
	@P_TP_VAT					NVARCHAR(3),
	@P_FG_BILL1					NVARCHAR(3),
	@P_FG_BILL2					NVARCHAR(3),
	@P_TP_COND_PRICE			NVARCHAR(3),
	@P_CD_SALEGRP				NVARCHAR(7),
	@P_TXT_USERDEF2				NVARCHAR(3),
	@P_TXT_USERDEF3				NVARCHAR(3),
	@P_CD_EXCH2					NVARCHAR(3),
	@P_CD_TPPO					NVARCHAR(7),
	@P_FG_PAYMENT				NVARCHAR(3),
	@P_FG_TAX					NVARCHAR(3),
	@P_TP_UM_TAX				NVARCHAR(3),
	@P_TP_INQ_PO				NVARCHAR(3),
	@P_TP_PO					NVARCHAR(3),
	@P_TP_INQ					NVARCHAR(3),
	@P_TP_QTN					NVARCHAR(3),
	@P_TP_DELIVERY_CONDITION	NVARCHAR(3),
	@P_TP_TERMS_PAYMENT			NVARCHAR(3),
	@P_RT_COMMISSION			NUMERIC(5, 2),
	@P_DC_COMMISSION			NVARCHAR(500),
	@P_RT_SALES_PROFIT			NUMERIC(5, 2),
	@P_RT_SALES_DC				NUMERIC(5, 2),
	@P_RT_PURCHASE_DC			NUMERIC(5, 2),
	@P_DC_PURCHASE_MAIL			NVARCHAR(255),
	@P_YN_CERT					NVARCHAR(1),
	@P_TP_PRINT					NVARCHAR(5),
	@P_TP_ATTACH_EMAIL			NVARCHAR(4),
	@P_CD_ORIGIN				NVARCHAR(20),
	@P_YN_USE_GIR				NVARCHAR(1),
	@P_YN_GR_LABEL				NVARCHAR(1),
	@P_DC_CEO_E_MAIL			NVARCHAR(100)	= NULL,
	@P_CD_PARTNER_RELATION		NVARCHAR(20)	= NULL,
	@P_NO_VAT					NVARCHAR(50)	= NULL,
	@P_TP_PRICE_SENS			NVARCHAR(4),
	@P_SHIPSERV_TNID			NVARCHAR(10),
	@P_YN_HOLD					NVARCHAR(1),
	@P_TP_INV					NVARCHAR(3),
	@P_YN_COLOR					NVARCHAR(1),
	@P_TP_PAY					NVARCHAR(3),
	@P_TP_DELIVERY				NVARCHAR(3),
	@P_TP_PACKING				NVARCHAR(4)
) AS 
-- ================================================
-- AUTHOR      : 이대성
-- CREATE DATE : 2010.03.24
--				 
-- MODULE      : 시스템관리
-- SYSTEM      : 거래처정보
-- SUBSYSTEM   : 
-- PAGE        : 거래처정보관리
-- PROJECT     : P_MA_PARTNER/P_MA_PARTNER_YTN
-- DESCRIPTION : 
-- ================================================ 
-- CHANGE HISTORY
-- v1.0 : 2010.11.16 이대성 수정 - 신규 컬럼 추가
-- v1.1 : 2011.01.20 이대성 수정 - 부가정보 탭에 연결법인 구분 추가
-- v1.2 : 2011.09.15 김병훈 수정 - 기타정보 탭의 비고 컬럼 추가 
-- v1.3 : 2011.11.07 김병훈 수정 - 솔리테크 POP 연동 방식 변경 -> URL 
-- ================================================
DECLARE @V_URL_PARTNER	NVARCHAR(6)
		
SET NOCOUNT ON 

SET @V_URL_PARTNER = REPLACE( STR(	(	SELECT	MAX(URL_PARTNER) + 1
										FROM	MA_PARTNER
										WHERE	LEN(URL_PARTNER) = 6
										AND		ISNUMERIC(URL_PARTNER) = 1
										AND		CD_COMPANY = @P_CD_COMPANY ), 6, 0)
							, ' ', '0')
							
IF @V_URL_PARTNER IS NULL 
BEGIN 
	SET @V_URL_PARTNER = '000001'
END

IF EXISTS (SELECT * FROM MA_PARTNER WHERE CD_COMPANY = @P_CD_COMPANY AND CD_PARTNER = @P_CD_PARTNER) 
BEGIN 
	--DECLARE @P_ERRNO  	INT 
	DECLARE @P_ERRMSG  	VARCHAR(255) 
 
	SELECT @P_ERRMSG = '거래처코드' + @P_CD_PARTNER + '이(가) 중복되었습니다.' 
	--RAISERROR @P_ERRNO @P_ERRMSG 
	-- MSSQL버전 업으로 에러메시지 변경
	RAISERROR (@P_ERRMSG, 18, 1)
	RETURN 
END


INSERT INTO  MA_PARTNER 
( 
	CD_PARTNER,
	CD_COMPANY,
	LN_PARTNER,
	DT_OPEN,
	AM_CAP,
	MANU_EMP,
	MANA_EMP,
	SD_PARTNER,
	LT_TRANS,  
	SN_PARTNER,
	EN_PARTNER,
	TP_PARTNER,
	FG_PARTNER,
	NM_CEO,
	NO_COMPANY,
	NO_RES,
	TP_JOB,
	CLS_JOB,  
	URL_PARTNER,
	CLS_PARTNER,
	CD_NATION,
	STA_CREDIT,
	E_MAIL,
	NO_TEL,
	NO_FAX,
	NO_POST1,
	DC_ADS1_H,  
	DC_ADS1_D,
	NO_TEL1,
	NO_FAX1,
	NO_POST2,
	DC_ADS2_H,
	DC_ADS2_D,
	NO_TEL2,
	NO_FAX2,
	NO_POST3,  
	DC_ADS3_H,
	DC_ADS3_D,
	NO_TEL3,
	NO_FAX3,
	NM_PTR,
	USE_YN,
	FG_CREDIT,
	TP_TAX,
	NO_MERCHANT,  
	NO_DEPOSIT,
	CD_BANK,
	ID_COMPANY,
	PW_COMPANY,
	CD_PARTNER_GRP,
	CD_EMP_SALE,
	CD_EMP_PARTNER,
	NO_HPEMP_PARTNER,
	CD_AREA,
	DT_RCP_PREARRANGED,
	NM_DEPOSIT,
	CD_PARTNER_GRP_2,							
	FG_BILL,
	FG_PAYBILL,
	DT_PAY_PREARRANGED,
	CD_CON,
	DT_CLOSE,
	ID_INSERT,
	DTS_INSERT,
	YN_BIZTAX,
	NO_BIZTAX,
	YN_JEONJA,
	TP_DEFER,
	TP_RCP_DD,
	DT_RCP_DD,
	TP_PAY_DD,
	DT_PAY_DD,
	FG_CORP,
	NM_CURE_AGENCY,
	NO_CUER_AGENCY, 
	NM_TEXT,
	TP_NATION,
	NM_DCDEPOSIT,
	FG_CORPCODE,
	DT_RCP_PRETOLERANCE,
	NO_COR
) 
VALUES 
( 
	@P_CD_PARTNER,
	@P_CD_COMPANY,
	@P_LN_PARTNER,
	@P_DT_OPEN,
	@P_AM_CAP,
	@P_MANU_EMP,
	@P_MANA_EMP,
	@P_SD_PARTNER,
	@P_LT_TRANS,  
	@P_SN_PARTNER,
	@P_EN_PARTNER,
	@P_TP_PARTNER,
	@P_FG_PARTNER,
	@P_NM_CEO,
	@P_NO_COMPANY,
	@P_NO_RES,
	@P_TP_JOB,
	@P_CLS_JOB,  
	@P_URL_PARTNER,
	@P_CLS_PARTNER,
	@P_CD_NATION,
	@P_STA_CREDIT,
	@P_E_MAIL,
	@P_NO_TEL,
	@P_NO_FAX,
	@P_NO_POST1,
	@P_DC_ADS1_H,  
	@P_DC_ADS1_D,
	@P_NO_TEL1,
	@P_NO_FAX1,
	@P_NO_POST2,
	@P_DC_ADS2_H,
	@P_DC_ADS2_D,
	@P_NO_TEL2,
	@P_NO_FAX2,
	@P_NO_POST3,  
	@P_DC_ADS3_H,
	@P_DC_ADS3_D,
	@P_NO_TEL3,
	@P_NO_FAX3,
	@P_NM_PTR,
	@P_USE_YN,
	@P_FG_CREDIT,
	@P_TP_TAX,
	@P_NO_MERCHANT,  
	@P_NO_DEPOSIT,
	@P_CD_BANK,
	@P_ID_COMPANY,
	@P_PW_COMPANY, 
	@P_CD_PARTNER_GRP,
	@P_CD_EMP_SALE,
	@P_CD_EMP_PARTNER,	--2007.11.15 주홍열 
	@P_NO_HPEMP_PARTNER,
	@P_CD_AREA,						--2007.11.15 주홍열 
	@P_DT_RCP_PREARRANGED,
	@P_NM_DEPOSIT,					--2007.12.07 제갈경미 
	@P_CD_PARTNER_GRP_2,									--2008.03.26 허성철 
	@P_FG_BILL,												--2009.02.17 이대성		
	@P_FG_PAYBILL,			--2009.12.01 박태호
	@P_DT_PAY_PREARRANGED,	--2009.12.01 박태호
	@P_CD_CON,              --2010.01.04 정남진
	@P_DT_CLOSE,			--2010.03.24 이대성:폐업일자
	@P_ID_INSERT,
	NEOE.SF_SYSDATE(GETDATE()),
	@P_YN_BIZTAX,			-- 2010.11.16 이대성 추가 : 사업자단위과세 여부
	@P_NO_BIZTAX,			-- 2010.11.16 이대성 추가 : 종사업장번호
	@P_YN_JEONJA,			-- 2010.11.16 이대성 추가 : 전자세금계산서발행여부
	@P_TP_DEFER,			-- 2010.11.16 이대성 추가 : 지급보류여부
	@P_TP_RCP_DD,			-- 2010.11.16 이대성 추가 : 자금수금예정월
	@P_DT_RCP_DD,			-- 2010.11.16 이대성 추가 : 자금수금예정일
	@P_TP_PAY_DD,			-- 2010.11.16 이대성 추가 : 자금지급에정월
	@P_DT_PAY_DD,			-- 2010.11.16 이대성 추가 : 자금지급예정일
	@P_FG_CORP,				-- 2011.01.20 이대성 추가 : 연결법인구분
	@P_NM_CURE_AGENCY,
	@P_NO_CUER_AGENCY,
	@P_NM_TEXT,				-- 2011.09.15 김병훈 추가 : 기타정보 탭의 비고 컬럼  
	@P_TP_NATION,
	@P_NM_DCDEPOSIT,
	@P_FG_CORPCODE,
	@P_DT_RCP_PRETOLERANCE,
	@P_NO_COR
)

IF NOT EXISTS (SELECT 1 
			   FROM CZ_MA_PARTNER 
			   WHERE CD_COMPANY = @P_CD_COMPANY 
			   AND CD_PARTNER = @P_CD_PARTNER)
BEGIN
	INSERT INTO CZ_MA_PARTNER
	(
		CD_COMPANY,
		CD_PARTNER,
		LN_PARTNER,
		CD_EXCH1,
		TP_SO,
		TP_VAT,
		FG_BILL1,
		FG_BILL2,
		TP_COND_PRICE,
		CD_SALEGRP,
		TXT_USERDEF2,
		TXT_USERDEF3,
		CD_EXCH2,
		CD_TPPO,
		FG_PAYMENT,
		FG_TAX,
		TP_UM_TAX,
		TP_INQ_PO,
		TP_PO,
		TP_INQ,
		TP_QTN,
		TP_DELIVERY_CONDITION,
		TP_TERMS_PAYMENT,
		RT_COMMISSION,
		DC_COMMISSION,
		RT_SALES_PROFIT,
		RT_SALES_DC,
		RT_PURCHASE_DC,
		DC_PURCHASE_MAIL,
		YN_CERT,
		TP_PRINT,
		TP_ATTACH_EMAIL,
		CD_ORIGIN,
		YN_USE_GIR,
		YN_GR_LABEL,
		ID_INSERT,
		DTS_INSERT,
		DC_CEO_E_MAIL,
		CD_PARTNER_RELATION,
		NO_VAT,
		TP_PRICE_SENS,
		SHIPSERV_TNID,
		YN_HOLD,
		TP_INV,
		YN_COLOR,
		TP_PAY,
		TP_DELIVERY,
		TP_PACKING
	)
	VALUES 
	( 
		@P_CD_COMPANY,
		@P_CD_PARTNER,
		@P_LN_PARTNER,
		@P_CD_EXCH1,
		@P_TP_SO,
		@P_TP_VAT,
		@P_FG_BILL1,
		@P_FG_BILL2,
		@P_TP_COND_PRICE,
		@P_CD_SALEGRP,
		@P_TXT_USERDEF2,
		@P_TXT_USERDEF3,
		@P_CD_EXCH2,
		@P_CD_TPPO,
		@P_FG_PAYMENT,
		@P_FG_TAX,
		@P_TP_UM_TAX,
		@P_TP_INQ_PO,
		@P_TP_PO,
		@P_TP_INQ,
		@P_TP_QTN,
		@P_TP_DELIVERY_CONDITION,
		@P_TP_TERMS_PAYMENT,
		@P_RT_COMMISSION,
		@P_DC_COMMISSION,
		@P_RT_SALES_PROFIT,
		@P_RT_SALES_DC,
		@P_RT_PURCHASE_DC,
		@P_DC_PURCHASE_MAIL,
		@P_YN_CERT,
		@P_TP_PRINT,
		@P_TP_ATTACH_EMAIL,
		@P_CD_ORIGIN,
		@P_YN_USE_GIR,
		@P_YN_GR_LABEL,
		@P_ID_INSERT,
		NEOE.SF_SYSDATE(GETDATE()),
		@P_DC_CEO_E_MAIL,
		@P_CD_PARTNER_RELATION,
		@P_NO_VAT,
		@P_TP_PRICE_SENS,
		@P_SHIPSERV_TNID,
		@P_YN_HOLD,
		@P_TP_INV,
		@P_YN_COLOR,
		@P_TP_PAY,
		@P_TP_DELIVERY,
		@P_TP_PACKING
	)
END
ELSE
BEGIN
	UPDATE CZ_MA_PARTNER
	SET	LN_PARTNER = @P_LN_PARTNER,
		CD_EXCH1 = @P_CD_EXCH1,
		TP_SO = @P_TP_SO,
		TP_VAT = @P_TP_VAT,
		FG_BILL1 = @P_FG_BILL1,
		FG_BILL2 = @P_FG_BILL2,
		TP_COND_PRICE = @P_TP_COND_PRICE,
		CD_SALEGRP = @P_CD_SALEGRP,
		TXT_USERDEF2 = @P_TXT_USERDEF2,
		TXT_USERDEF3 = @P_TXT_USERDEF3,
		CD_EXCH2 = @P_CD_EXCH2,
		CD_TPPO = @P_CD_TPPO,
		FG_PAYMENT = @P_FG_PAYMENT,
		FG_TAX = @P_FG_TAX,
		TP_UM_TAX = @P_TP_UM_TAX,
		TP_INQ_PO = @P_TP_INQ_PO,
		TP_PO = @P_TP_PO,
		TP_INQ = @P_TP_INQ,
		TP_QTN = @P_TP_QTN,
		TP_DELIVERY_CONDITION = @P_TP_DELIVERY_CONDITION,
		TP_TERMS_PAYMENT = @P_TP_TERMS_PAYMENT,
		RT_COMMISSION = @P_RT_COMMISSION,
		DC_COMMISSION = @P_DC_COMMISSION,
		RT_SALES_PROFIT = @P_RT_SALES_PROFIT,
		RT_SALES_DC = @P_RT_SALES_DC,
		RT_PURCHASE_DC = @P_RT_PURCHASE_DC,
		DC_PURCHASE_MAIL = @P_DC_PURCHASE_MAIL,
		YN_CERT = @P_YN_CERT,
		TP_PRINT = @P_TP_PRINT,
		TP_ATTACH_EMAIL = @P_TP_ATTACH_EMAIL,
		CD_ORIGIN = @P_CD_ORIGIN,
		YN_USE_GIR = @P_YN_USE_GIR,
		YN_GR_LABEL = @P_YN_GR_LABEL,
		ID_UPDATE = @P_ID_INSERT,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE()),
		DC_CEO_E_MAIL = @P_DC_CEO_E_MAIL,
		CD_PARTNER_RELATION = @P_CD_PARTNER_RELATION,
		NO_VAT = @P_NO_VAT,
		TP_PRICE_SENS = @P_TP_PRICE_SENS,
		SHIPSERV_TNID = @P_SHIPSERV_TNID,
		YN_HOLD = @P_YN_HOLD,
		TP_INV = @P_TP_INV,
		YN_COLOR = @P_YN_COLOR,
		TP_PAY = @P_TP_PAY,
		TP_DELIVERY = @P_TP_DELIVERY,
		TP_PACKING = @P_TP_PACKING
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND CD_PARTNER = @P_CD_PARTNER
END

-- 현대 실사용 갈매기 처리
--IF @P_CD_COMPANY = 'K100' AND 
--	 @P_TP_PARTNER = 'ETC' AND 
--	 NOT EXISTS (SELECT 1 
--				 FROM CZ_MA_PARTNER_GULL
--				 WHERE CD_PARTNER = @P_CD_PARTNER)
--BEGIN
--	INSERT INTO CZ_MA_PARTNER_GULL
--	(
--		CD_PARTNER,
--		ID_INSERT,
--		DTS_INSERT
--	)
--	VALUES
--	(
--		@P_CD_PARTNER,
--		@P_ID_INSERT,
--		NEOE.SF_SYSDATE(GETDATE())
--	) 
--END

--담당자추가
--EXEC UP_FI_Z_DINTEC_PARTNERPTR_I  @P_CD_COMPANY, @P_CD_PARTNER, @P_CD_EMP_PARTNER, @P_E_MAIL, @P_NO_HPEMP_PARTNER, @P_NO_TEL, @P_NO_FAX, @P_ID_INSERT

SET NOCOUNT OFF
