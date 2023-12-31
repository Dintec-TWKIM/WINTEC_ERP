USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_PARTNERD_S]    Script Date: 2016-07-04 오후 4:16:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MA_PARTNER_S]
(
	@P_CD_COMPANY 	  NVARCHAR(7),
	@P_SEARCH_KEY	  NVARCHAR(50),	-- 거래처코드 또는 거래처명 검색
	@P_FG_PARTNER	  NVARCHAR(3),	-- 거래처구분
	@P_CLS_PARTNER	  NVARCHAR(3),	-- 거래처분류
	@P_USE_YN		  NCHAR(1),		-- 사용유무
	@P_NM_TEXT		  NVARCHAR(500),
	@P_CD_PARTNER_GRP NVARCHAR(4),
	@P_FG_PAYMENT	  NVARCHAR(4)
) 
AS
-- ================================================
-- AUTHOR      : 
-- CREATE DATE : 
--				 
-- MODULE      : 시스템관리
-- SYSTEM      : 거래처정보
-- SUBSYSTEM   : 
-- PAGE        : 거래처정보관리
-- PROJECT     : P_MA_PARTNER/P_MA_PARTNER_YTN
-- DESCRIPTION : 
-- ================================================ 
-- CHANGE HISTORY
-- v1.0 : 2010.03.24 이대성 수정
-- v1.1 : 2010.10.18 이대성 수정 - YTN 관련 최종수정일/최종수정자 컬럼 추가
-- v1.2 : 2010.11.16 이대성 수정 - 신규 컬럼 추가
-- v1.3 : 2011.01.20 이대성 수정 - 부가정보 탭에 연결법인 구분 추가
-- 2015.01.07 : D20141215008 : 기타정보 지역구분 그리드에 디스플레이되게 수정.
-- ================================================
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT MP.CD_PARTNER,
	   MP.LN_PARTNER,
	   MP.NO_COMPANY,
	   MP.NM_CEO,
	   MP.NO_RES,
	   MP.USE_YN,
	   MP.NM_TEXT,
	   MP.NO_POST1,
	   MP.DC_ADS1_H,
	   MP.DC_ADS1_D,
	   MP.SN_PARTNER,
	   MP.NO_TEL,
	   MP.NO_FAX,
	   MP.NO_COR,
	   ME.NM_KOR AS NM_EMP_SALE,
	   ISNULL(MP.CD_AREA, '') AS CD_AREA,
	   MP.CD_PARTNER AS OLD_CD_PARTNER,
	   (CASE WHEN MP.DT_OPEN = '00000000' THEN '' ELSE MP.DT_OPEN END) AS DT_OPEN, 
	   MP.AM_CAP,
	   MP.MANU_EMP,
	   MP.MANA_EMP, 
	   (CASE WHEN MP.SD_PARTNER = '00000000' THEN '' ELSE MP.SD_PARTNER END) AS SD_PARTNER, 
	   MP.LT_TRANS,
	   MP.EN_PARTNER,
	   MP.TP_PARTNER,
	   MP.FG_PARTNER,
	   MP.NO_COMPANY AS OLD_NO_COMPANY,
	   MP.TP_JOB,
	   MP.CLS_JOB,
	   MP.URL_PARTNER,
	   MP.CLS_PARTNER,
	   MC6.NM_SYSDEF AS NM_CLS_PARTNER,
	   MP.NM_PTR,
	   MP.CD_NATION,
	   MC7.NM_SYSDEF AS NM_NATION,
	   MP.STA_CREDIT,
	   MP.E_MAIL,
	   MP.NO_TEL1,
	   MP.NO_FAX1,
	   MP.NO_POST2,
	   MP.DC_ADS2_H,
	   MP.DC_ADS2_D, 
	   MP.NO_TEL2,
	   MP.NO_FAX2,
	   MP.NO_POST3,
	   MP.DC_ADS3_H,
	   MP.DC_ADS3_D, 
	   MP.NO_TEL3,
	   MP.NO_FAX3,
	   MP.FG_CREDIT,
	   MP.TP_TAX,
	   MP.NO_MERCHANT,
	   PD.CD_DEPOSIT,
	   MP.NO_DEPOSIT,
	   MP.CD_BANK,
	   PD.NM_BANK,
	   PD.CD_BANK_NATION,
	   PD.NO_SORT,
	   PD.CD_DEPOSIT_NATION,
	   PD.NO_SWIFT,
	   MP.NM_DEPOSIT,
	   PD.DC_DEPOSIT_TEL,
	   PD.DC_DEPOSIT_ADDRESS,
	   PD.NO_BANK_BIC,
	   PD.DC_RMK,	
	   CP.NM_COMPANY, 
	   MP.ID_COMPANY,
	   MP.PW_COMPANY, -- 2007.11.14 주홍열 작업
	   ISNULL(MP.CD_PARTNER_GRP, '') AS CD_PARTNER_GRP, 
	   ISNULL(MP.CD_EMP_SALE, '') AS CD_EMP_SALE,
	   ISNULL(MP.CD_EMP_PARTNER, '') AS CD_EMP_PARTNER, 
	   ISNULL(MP.NO_HPEMP_PARTNER, '') AS NO_HPEMP_PARTNER,
	   MP.DT_RCP_PREARRANGED,		-- 2007.12.07 제갈경미
	   MP.CD_PARTNER_GRP_2,			-- 2008.03.26 허성철
	   MP.FG_BILL,					-- 2009.02.17 이대성
	   MP.FG_PAYBILL,				-- 2009.12.01 박태호
	   MP.DT_PAY_PREARRANGED,		-- 2009.12.01 박태호
	   ISNULL(MP.CD_CON, '001') CD_CON,	-- 2010.01.04 정남진(거래처 휴.폐업추가)
	   MC1.NM_SYSDEF AS NM_CON,
	   MP.DT_CLOSE,					-- 2010.03.24 이대성:폐업일자
	   MU.NM_USER AS NM_UPDATE,
	   ISNULL(MP.YN_BIZTAX, 'N') AS YN_BIZTAX,	-- 2010.11.16 이대성 추가 : 사업자단위과세 여부
	   MP.NO_BIZTAX,						-- 2010.11.16 이대성 추가 : 종사업장번호
	   MP.YN_JEONJA,	-- 2010.11.16 이대성 추가 : 전자세금계산서발행여부
	   ISNULL(MP.TP_DEFER, '0') AS TP_DEFER,	-- 2010.11.16 이대성 추가 : 지급보류여부
	   ISNULL(MP.TP_RCP_DD, '0') AS TP_RCP_DD,	-- 2010.11.16 이대성 추가 : 자금수금예정월
	   MP.DT_RCP_DD,						-- 2010.11.16 이대성 추가 : 자금수금예정일
	   ISNULL(MP.TP_PAY_DD, '0') AS TP_PAY_DD,	-- 2010.11.16 이대성 추가 : 자금지급에정월
	   MP.DT_PAY_DD,						-- 2010.11.16 이대성 추가 : 자금지급예정일
	   ISNULL(MP.FG_CORP, '0') AS FG_CORP,		-- 2011.01.20 이대성 추가 : 연결법인구분
	   MP.NM_CURE_AGENCY, 
	   MP.NO_CUER_AGENCY, -- 요양기관명,요양기관번호    
	   MP.TP_NATION,						-- 내외국인
	   MP.NM_DCDEPOSIT,  
	   MP.FG_CORPCODE,
	   MP.DT_RCP_PRETOLERANCE,
	   -- 업체전용 컬럼
	   MP1.CD_EXCH1,
	   MP1.TP_SO,
	   TS.NM_SO,
	   MP1.TP_VAT,
	   MP1.FG_BILL1,
	   MP1.FG_BILL2,
	   MP1.TP_COND_PRICE,
	   MP1.CD_SALEGRP,
	   SG.NM_SALEGRP,
	   MP1.TXT_USERDEF2,
	   MC3.NM_SYSDEF AS NM_USERDEF2,
	   MP1.TXT_USERDEF3,
	   MC4.NM_SYSDEF AS NM_USERDEF3,
	   MP1.CD_EXCH2,
	   MP1.CD_TPPO,
	   TP.NM_TPPO,
	   MP1.FG_PAYMENT,
	   MP1.FG_TAX,
	   MP1.TP_UM_TAX,
	   MP1.TP_INQ_PO,
	   MP1.TP_PO,
	   MP1.TP_INQ,
	   MP1.TP_QTN,
	   MP1.TP_DELIVERY_CONDITION,
	   MP1.TP_TERMS_PAYMENT,
	   MP1.RT_COMMISSION,
	   MP1.DC_COMMISSION,
	   MP1.RT_SALES_PROFIT,
	   MP1.RT_SALES_DC,
	   MP1.RT_PURCHASE_DC,
	   MP1.DC_PURCHASE_MAIL,
	   MP1.DC_CEO_E_MAIL,
	   MP1.CD_PARTNER_RELATION,
	   MP2.LN_PARTNER AS NM_PARTNER_RELATION,
	   GW.ST_STAT,
	   MC5.NM_SYSDEF AS NM_GW_STATUS,
	   MP1.YN_CERT,
	   MP1.TP_PRINT,
	   MP1.TP_ATTACH_EMAIL,
	   MP1.CD_ORIGIN,
	   MP1.YN_USE_GIR,
	   MP1.YN_GR_LABEL,
	   MP1.NO_VAT,
	   MP1.TP_PRICE_SENS,
	   MP1.SHIPSERV_TNID,
	   ISNULL(MP1.YN_HOLD, 'N') AS YN_HOLD,
	   MP1.TP_INV,
	   MP1.YN_COLOR,
	   MP1.TP_PAY,
	   MP1.TP_DELIVERY,
	   MP1.TP_PACKING
	   

FROM MA_PARTNER MP
LEFT JOIN CZ_MA_PARTNER MP1 ON MP1.CD_COMPANY = MP.CD_COMPANY AND MP1.CD_PARTNER = MP.CD_PARTNER
LEFT JOIN CZ_MA_PARTNER_DEPOSIT PD ON PD.CD_COMPANY = MP.CD_COMPANY AND PD.CD_PARTNER = MP.CD_PARTNER AND PD.NO_DEPOSIT = MP.NO_DEPOSIT
LEFT JOIN MA_PARTNER MP2 ON MP2.CD_COMPANY = MP1.CD_COMPANY AND MP2.CD_PARTNER = MP1.CD_PARTNER_RELATION
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = MP.CD_COMPANY AND ME.NO_EMP = MP.CD_EMP_SALE
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = MP.CD_COMPANY AND MU.ID_USER = (CASE WHEN ISNULL(MP.ID_UPDATE, '') = '' THEN MP.ID_INSERT ELSE MP.ID_UPDATE END)
LEFT JOIN MA_COMPANY CP ON CP.CD_COMPANY = MP.CD_COMPANY
LEFT JOIN SA_TPSO TS ON TS.CD_COMPANY = MP1.CD_COMPANY AND TS.TP_SO = MP1.TP_SO
LEFT JOIN PU_TPPO TP ON TP.CD_COMPANY = MP1.CD_COMPANY AND TP.CD_TPPO = MP1.CD_TPPO
LEFT JOIN FI_GWDOCU GW ON GW.CD_COMPANY = 'K100' AND GW.CD_PC = '010000' AND GW.NO_DOCU = MP.CD_COMPANY + '-PTNR-' + MP.CD_PARTNER
LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = MP1.CD_COMPANY AND SG.CD_SALEGRP = MP1.CD_SALEGRP
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = MP.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000073' AND MC1.CD_SYSDEF = ISNULL(MP.CD_CON, '001')
LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MP1.CD_COMPANY AND MC3.CD_FIELD = 'MA_DIN0002' AND MC3.CD_SYSDEF = MP1.TXT_USERDEF2
LEFT JOIN MA_CODEDTL MC4 ON MC4.CD_COMPANY = MP1.CD_COMPANY AND MC4.CD_FIELD = 'MA_DIN0003' AND MC4.CD_SYSDEF = MP1.TXT_USERDEF3
LEFT JOIN MA_CODEDTL MC5 ON MC5.CD_COMPANY = MP.CD_COMPANY AND MC5.CD_FIELD = 'PU_C000065' AND MC5.CD_SYSDEF = GW.ST_STAT
LEFT JOIN MA_CODEDTL MC6 ON MC6.CD_COMPANY = MP.CD_COMPANY AND MC6.CD_FIELD = 'MA_B000003' AND MC6.CD_SYSDEF = MP.CLS_PARTNER
LEFT JOIN MA_CODEDTL MC7 ON MC7.CD_COMPANY = MP.CD_COMPANY AND MC7.CD_FIELD = 'MA_B000020' AND MC7.CD_SYSDEF = MP.CD_NATION
WHERE MP.CD_COMPANY = @P_CD_COMPANY
AND	(ISNULL(@P_FG_PARTNER, '') = '' OR MP.FG_PARTNER = @P_FG_PARTNER)
AND	(ISNULL(@P_CLS_PARTNER, '') = '' OR MP.CLS_PARTNER = @P_CLS_PARTNER)
AND	(ISNULL(@P_CD_PARTNER_GRP, '') = '' OR MP.CD_PARTNER_GRP = @P_CD_PARTNER_GRP)
AND (ISNULL(@P_FG_PAYMENT, '') = '' OR MP1.FG_PAYMENT = @P_FG_PAYMENT)
AND	(ISNULL(@P_USE_YN, '') = '' OR MP.USE_YN = @P_USE_YN)
AND	(ISNULL(@P_NM_TEXT, '') = '' OR MP.NM_TEXT LIKE '%' + @P_NM_TEXT + '%')
AND	(ISNULL(@P_SEARCH_KEY, '') = '' OR (MP.CD_PARTNER LIKE '%' + @P_SEARCH_KEY + '%'
									OR MP.LN_PARTNER LIKE '%' + @P_SEARCH_KEY + '%'
									OR MP.NM_TEXT LIKE '%' + @P_SEARCH_KEY + '%'))
ORDER BY MP.CD_PARTNER ASC

GO

