USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_BANK_SEND_EX_I]    Script Date: 2018-02-06 오후 3:41:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_FI_BANK_SEND_EX_I]
(  
	@P_C_CODE		VARCHAR(40),  
	@P_TRANS_DATE	VARCHAR(40),  
	@P_TRANS_SEQ	VARCHAR(40)
)  
AS 

INSERT INTO IBK_FCC_AP_ERP_REMIT
(
	SITE_NO, -- 사업자번호
	REG_DATE, -- 등록일자
	REG_TIME, -- 등록시간
	REG_SEQ, -- 등록일련번호
	BULK_REMT_NM, -- 일괄송금명
	BRTP_CSNO, -- 고객번호
	DRAC_NO, -- 외화출금계좌번호
	EBNK_FAMT1700, -- 외화송금액
	REMT_CURCD, -- 송금통화코드
	TRS_CLS2, -- 송금종류
	REQR_ENG_NM35, -- 송금의뢰인영문성명
	REQR_RNNO, -- 송금의뢰인사업자번호
	REQR_ENG_ADR1, -- 송금의뢰인주소1
	REQR_ENG_ADR2, -- 송금의뢰인주소2
	REQR_TELNO, -- 송금의뢰인연락전화
	RCVR_ENG_NM35, -- 수취인영문성명
	RCVR_ACNO, -- 수취인계좌번호
	REMT_NACD, -- 상대국코드
	PYBK_BIC, -- 수취(지급)은행코드
	PYBK_ENG_NM1, -- 수취(지급)은행은행명및주소1

	GEDR_ACNO, -- 수수료인출원화계좌번호
	FEE_BERR_CLCD, -- 해외은행송금수수료부담구분코드
	DRAW_CLS, --  송금수수료출금계좌지정구분
	--REMT_RSCD1, -- 송금사유코드
	--REMT_REF_CTT, -- 송금사유내용
	EBR_YN, -- e-Branch 여부
	PYBK_CQNO, -- 지급은행고유번호
	--RCVR_RNNO, -- 유학생실명번호
	--EBNK_FAMT1701, -- 송금사유금액1
	--EBNK_RSCD2, -- 송금사유코드2
	--EBNK_FAMT1702, -- 송금사유금액2
	--EBNK_RSCD3, -- 송금사유코드3
	--EBNK_FAMT1703, -- 송금사유금액3
	--BRCD, -- 수입서류발송부점코드
	--PRC_CNCD, -- 가격조건코드
	--HSCD, -- HS코드
	--IMP_USAG_CD, -- 수입용도구분코드
	--IMNO, -- 수입신고번호
	--EMAL_ADR40, -- 이메일주소
	CQNO_CLCD, -- 지급은행고유구분
	OSFE_DSCD, -- 해외수수료구분값
	
	REQR_ENG_NM, -- 송금의뢰인영문성명
	RCVR_ENG_NM, -- 수취인영문성명
	RCVR_ADR, -- 수취인주소
	--TAG_CTT, -- 수취인앞지시사항
	PYBK_ENG_NM, -- 수취(지급)은행명및주소
	--RLBK1, -- 송금경유은행1
	--RLBK2, -- 송금경유은행2
	--RLBK3, -- 송금경유은행3
	REQR_ENG_ADR, -- 송금의뢰인주소

	EB_STS_CD, -- E-BRANCH 처리상태
	INS_USER_ID, -- 등록자 ID
	INS_DATETIME -- 등록일시
)
SELECT MB.NO_BIZAREA AS SITE_NO, -- 사업자번호
	   BD.TRANS_DATE AS REG_DATE, -- 등록일자
	   '155500' AS REG_TIME, -- 등록시간
	   BD.TRANS_SEQ AS REG_SEQ, -- 등록일련번호
	   (BD.TRANS_DATE + '-' + BD.TRANS_SEQ) AS BULK_REMT_NM, -- 일괄송금명
	   '' AS BRTP_CSNO, -- 고객번호
	   BD.ACCT_NO AS DRAC_NO, -- 외화출금계좌번호
	   BD.TRANS_AMT_EX AS EBNK_FAMT1700, -- 외화송금액
	   MC.NM_SYSDEF AS REMT_CURCD, -- 송금통화코드
	   (CASE BD.TP_SEND_BY WHEN '001' THEN '01' ELSE '03' END)AS TRS_CLS2, -- 송금종류
	   '' AS REQR_ENG_NM35, -- 송금의뢰인영문성명
	   MB.NO_BIZAREA AS REQR_RNNO, -- 송금의뢰인사업자번호
	   '' AS REQR_ENG_ADR1, -- 송금의뢰인주소1
	   '' AS REQR_ENG_ADR2, -- 송금의뢰인주소2
	   '821098098088' AS REQR_TELNO, -- 송금의뢰인연락전화
	   '' AS RCVR_ENG_NM35, -- 수취인영문성명
	   BD.TRANS_ACCT_NO AS RCVR_ACNO, -- 수취인계좌번호
	   PD1.CD_BANK_NATION AS REMT_NACD, -- 상대국코드
	   PD1.NO_SWIFT AS PYBK_BIC, -- 수취(지급)은행코드
	   '' AS PYBK_ENG_NM1, -- 수취(지급)은행은행명및주소1
	   NULL AS GEDR_ACNO, -- 수수료인출원화계좌번호
	   MC2.CD_FLAG1 AS FEE_BERR_CLCD, -- 해외은행송금수수료부담구분코드
	   (CASE WHEN MC2.CD_FLAG1 = 'O' THEN '1' ELSE NULL END) AS DRAW_CLS, --  송금수수료출금계좌지정구분
	   --NULL AS REMT_RSCD1, -- 송금사유코드
	   --NULL AS REMT_REF_CTT, -- 송금사유내용
	   NULL AS EBR_YN, -- e-Branch 여부
	   PD1.NO_SORT AS PYBK_CQNO, -- 지급은행고유번호
	   --NULL AS RCVR_RNNO, -- 유학생실명번호
	   --NULL AS EBNK_FAMT1701, -- 송금사유금액1
	   --NULL AS EBNK_RSCD2, -- 송금사유코드2
	   --NULL AS EBNK_FAMT1702, -- 송금사유금액2
	   --NULL AS EBNK_RSCD3, -- 송금사유코드3
	   --NULL AS EBNK_FAMT1703, -- 송금사유금액3
	   --NULL AS BRCD, -- 수입서류발송부점코드
	   --NULL AS PRC_CNCD, -- 가격조건코드
	   --NULL AS HSCD, -- HS코드
	   --NULL AS IMP_USAG_CD, -- 수입용도구분코드
	   --NULL AS IMNO, -- 수입신고번호
	   --NULL AS EMAL_ADR40, -- 이메일주소
	   (CASE PD1.CD_BANK_NATION WHEN 'US' THEN '1'
								WHEN 'GB' THEN '2'
								WHEN 'IE' THEN '2'
								WHEN 'DE' THEN '3'
								WHEN 'AU' THEN '6'
								WHEN 'NZ' THEN '6'
								WHEN 'CA' THEN '4'
								ELSE '0' END) AS CQNO_CLCD, -- 지급은행고유구분
	   (CASE WHEN MC2.CD_FLAG1 = 'O' THEN 'N' ELSE NULL END) AS OSFE_DSCD, -- 해외수수료구분값
	   
	   MB.EN_BIZAREA AS REQR_ENG_NM, -- 송금의뢰인영문성명
	   LEFT(BD.TRANS_NAME, 35) AS RCVR_ENG_NM, -- 수취인영문성명
	   (CASE WHEN LEN(BD.TRANS_NAME) > 35 THEN SUBSTRING(BD.TRANS_NAME, 35, (LEN(BD.TRANS_NAME) - 35)) ELSE '' END) + PD1.DC_DEPOSIT_ADDRESS AS RCVR_ADR, -- 수취인주소
	   --NULL AS TAG_CTT, -- 수취인앞지시사항
	   PD1.NM_BANK AS PYBK_ENG_NM, -- 수취(지급)은행명및주소
	   --NULL AS RLBK1, -- 송금경유은행1
	   --NULL AS RLBK2, -- 송금경유은행2
	   --NULL AS RLBK3, -- 송금경유은행3
	   MB.DC_ADS_ENG AS REQR_ENG_ADR, -- 송금의뢰인주소

	   '0' AS EB_STS_CD, -- E-BRANCH 처리상태
	   BD.INSERT_ID AS INS_USER_ID, -- 등록자 ID
	   (BD.INSERT_DATE + BD.INSERT_TIME) AS INS_DATETIME -- 등록일시
FROM BANK_SENDD BD
LEFT JOIN MA_BIZAREA MB ON MB.CD_COMPANY = BD.C_CODE
LEFT JOIN (SELECT CD_COMPANY, CD_PARTNER,
				  MAX(NO_DEPOSIT) AS NO_DEPOSIT 
		   FROM MA_PARTNER_DEPOSIT
		   WHERE USE_YN = 'Y'
		   GROUP BY CD_COMPANY, CD_PARTNER) PD
ON PD.CD_COMPANY = BD.C_CODE AND PD.CD_PARTNER = BD.CUST_CODE AND PD.NO_DEPOSIT = BD.TRANS_ACCT_NO
LEFT JOIN CZ_MA_PARTNER_DEPOSIT PD1 ON PD1.CD_COMPANY = PD.CD_COMPANY AND PD1.CD_PARTNER = PD.CD_PARTNER AND PD1.NO_DEPOSIT = PD.NO_DEPOSIT 
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = BD.C_CODE AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = BD.CD_EXCH
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = PD1.CD_COMPANY AND MC1.CD_FIELD = 'CZ_MA00016' AND MC1.CD_SYSDEF = PD1.CD_BANK_NATION
LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = BD.C_CODE AND MC2.CD_FIELD = 'CZ_FI00002' AND MC2.CD_SYSDEF = BD.TP_CHARGE
WHERE BD.C_CODE = @P_C_CODE
AND BD.TRANS_DATE = @P_TRANS_DATE
AND BD.TRANS_SEQ = @P_TRANS_SEQ

GO