USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_BANK_SEND_BAND_S]    Script Date: 2016-12-27 오후 1:01:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_FI_BANK_SEND_BAND_S]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_NO_DOCU		NVARCHAR(20),
	@P_NO_DOLINE	NUMERIC(5)
)
AS
/*******************************************
**  SYSTEM          : 회계관리
**  SUB SYSTEM      : 은행연동-거래내역관리(반제)
**  PAGE            : P_FI_BANK_SEND_BAN
**  DESC            : 하단 그리드 조회
**  RETURN VALUES   : 사용업체 (엔위즈, 엔투비)
**
**  작    성    자  :
**  작    성    일  : 
**
**  수    정    자  : 
**  수  정  내  용  :
*********************************************
** CHANGE HISTORY
*********************************************
*********************************************/
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT SH.NO_DOCU,
	   SH.NO_DOLINE AS LINE_NO,
	   SH.TRANS_DATE,
	   SH.TRANS_SEQ,
	   SD.SEQ,
	   SH.SETTLE_YN,
	   SH.SETTLE_DATE,
	   SH.BANK_CODE,
	   MC2.NM_SYSDEF AS NM_BANK,
	   SH.ACCT_NO,
	   SD.TRANS_BANK_CODE,
	   MC.NM_SYSDEF AS TRANS_BANK_NAME,
	   SD.TRANS_ACCT_NO,
	   SD.TRANS_NAME,
	   SD.CD_EXCH,
	   MC1.NM_SYSDEF AS NM_EXCH,
	   SD.TRANS_AMT_EX,
	   SD.TRANS_AMT,
	   SH.CLIENT_NOTE,
	   SH.TRANS_NOTE,
	   SD.TP_CHARGE,
	   MC3.NM_SYSDEF AS NM_TP_CHARGE,
	   SD.TP_SEND_BY,
	   MC4.NM_SYSDEF AS NM_TP_SEND_BY,
	   PD.NM_BANK_EN,
	   PD.CD_BANK_NATION,
	   PD.NM_BANK_NATION,
	   PD.NO_SORT,
	   PD.CD_DEPOSIT_NATION,
	   PD.NM_DEPOSIT_NATION,
	   PD.NO_SWIFT,
	   PD.DC_DEPOSIT_TEL,
	   PD.DC_DEPOSIT_ADDRESS,
	   PD.NO_BANK_BIC,
	   PD.DC_RMK
FROM BANK_SENDH SH
LEFT JOIN BANK_SENDD SD ON SD.C_CODE = SH.C_CODE AND SD.TRANS_DATE = SH.TRANS_DATE AND SD.TRANS_SEQ = SH.TRANS_SEQ AND SD.SEQ = SH.SEQ
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = SD.C_CODE AND MC.CD_FIELD = 'FI_T000013' AND MC.CD_SYSDEF = SD.TRANS_BANK_CODE
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = SD.C_CODE AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = SD.CD_EXCH
LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = SH.C_CODE AND MC2.CD_FIELD = 'FI_T000013' AND MC2.CD_SYSDEF = SH.BANK_CODE
LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = SD.C_CODE AND MC3.CD_FIELD = 'CZ_FI00002' AND MC3.CD_SYSDEF = SD.TP_CHARGE
LEFT JOIN MA_CODEDTL MC4 ON MC4.CD_COMPANY = SD.C_CODE AND MC4.CD_FIELD = 'CZ_FI00003' AND MC4.CD_SYSDEF = SD.TP_SEND_BY
LEFT JOIN (SELECT PD.CD_COMPANY,
				  PD.CD_PARTNER,
				  PD2.NO_DEPOSIT,
				  PD2.NM_BANK AS NM_BANK_EN,
				  PD2.CD_BANK_NATION,
				  MC1.NM_SYSDEF AS NM_BANK_NATION,
				  PD2.NO_SORT,
				  PD2.CD_DEPOSIT_NATION,
				  MC2.NM_SYSDEF AS NM_DEPOSIT_NATION,
				  PD2.NO_SWIFT,
				  PD2.DC_DEPOSIT_TEL,
				  PD2.DC_DEPOSIT_ADDRESS,
				  PD2.NO_BANK_BIC,
				  PD2.DC_RMK
		   FROM	(SELECT CD_COMPANY, CD_PARTNER,
				 	    MAX(NO_DEPOSIT) AS NO_DEPOSIT 
				 FROM MA_PARTNER_DEPOSIT
				 WHERE USE_YN = 'Y'
				 GROUP BY CD_COMPANY, CD_PARTNER) PD
		   LEFT JOIN MA_PARTNER_DEPOSIT PD1 ON PD1.CD_COMPANY = PD.CD_COMPANY AND PD1.CD_PARTNER = PD.CD_PARTNER AND PD1.NO_DEPOSIT = PD.NO_DEPOSIT
		   LEFT JOIN CZ_MA_PARTNER_DEPOSIT PD2 ON PD2.CD_COMPANY = PD1.CD_COMPANY AND PD2.CD_PARTNER = PD1.CD_PARTNER AND PD2.NO_DEPOSIT = PD1.NO_DEPOSIT 
		   LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PD1.CD_COMPANY AND MC.CD_FIELD = 'FI_T000013' AND MC.CD_SYSDEF = PD1.CD_BANK
		   LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = PD2.CD_COMPANY AND MC1.CD_FIELD = 'CZ_MA00016' AND MC1.CD_SYSDEF = PD2.CD_BANK_NATION
		   LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = PD2.CD_COMPANY AND MC2.CD_FIELD = 'CZ_MA00016' AND MC2.CD_SYSDEF = PD2.CD_DEPOSIT_NATION) PD 
ON PD.CD_COMPANY = SD.C_CODE AND PD.CD_PARTNER = SD.CUST_CODE AND PD.NO_DEPOSIT = SD.TRANS_ACCT_NO
WHERE SH.C_CODE = @P_CD_COMPANY
AND SH.NO_DOCU = @P_NO_DOCU
AND SH.NO_DOLINE = @P_NO_DOLINE
ORDER BY SH.TRANS_DATE, SH.TRANS_SEQ, SD.SEQ

GO

