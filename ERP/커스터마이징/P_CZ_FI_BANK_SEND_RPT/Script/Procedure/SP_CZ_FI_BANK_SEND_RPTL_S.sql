USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_BANK_SEND_RPTL_S]    Script Date: 2016-12-28 오후 3:04:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_FI_BANK_SEND_RPTL_S]
(  
	@P_C_CODE		NVARCHAR(10), --회사  
	@P_TRANS_DATE	NVARCHAR(8),  --파일작성일자FROM  
	@P_TRANS_SEQ	NVARCHAR(16)  --파일작성순번
)  
AS  
/*******************************************  
**  SYSTEM  : 회계관리  
**  SUB SYSTEM : 은행연동 - 거래내역관리  
**  PAGE   : 이체내역관리  
**  DESC   : 이체내역현황 조회(DETAIL)  
**  
**  RETURN VALUES  
**  
**  작    성    자  : 허성철  
**  작    성    일  : 2007.01.16  
*********************************************  
** CHANGE HISTORY  
*********************************************
** 2014.09.02 윤나라 회계단위 조회조건 추가  
*********************************************/  
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   SH.TRANS_DATE,
	   SH.TRANS_SEQ,
	   SH.SEQ,
	   SH.CUST_CODE,
	   SH.CUST_NAME,
	   SH.TRANS_BANK_CODE,
	   MC.NM_SYSDEF AS TRANS_BANK_NAME,
	   SH.TRANS_ACCT_NO,
	   SH.TRANS_NAME,
	   SH.CD_EXCH,
	   MC1.NM_SYSDEF AS NM_EXCH,
	   SH.TRANS_AMT_EX,
	   SH.TRANS_AMT,
	   SH.CLIENT_NOTE,
	   SH.TRANS_NOTE,
	   SH.SETTLE_YN,
	   SH.SETTLE_DATE,
	   SH.NO_LIMITE,
	   SH.TP_CHARGE,
	   MC2.NM_SYSDEF AS NM_TP_CHARGE,
	   SH.TP_SEND_BY,
	   MC3.NM_SYSDEF AS NM_TP_SEND_BY,
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
	   LG.NO_HST,
	   LG1.ID_PRINT,
	   MU.NM_USER AS NM_PRINT,
	   LG1.DTS_PRINT,
	   PD.DC_RMK,
	   SH.DC_RELATION
FROM BANK_SENDH SH
LEFT JOIN (SELECT CD_COMPANY, TRANS_DATE, TRANS_SEQ, SEQ,
				  MAX(NO_HST) AS NO_HST
		   FROM CZ_FI_BANK_SEND_PRINT_LOG
		   GROUP BY CD_COMPANY, TRANS_DATE, TRANS_SEQ, SEQ) LG 
ON LG.CD_COMPANY = SH.C_CODE AND LG.TRANS_DATE = SH.TRANS_DATE AND LG.TRANS_SEQ = SH.TRANS_SEQ AND LG.SEQ = SH.SEQ
LEFT JOIN CZ_FI_BANK_SEND_PRINT_LOG LG1 ON LG1.CD_COMPANY = LG.CD_COMPANY AND LG1.TRANS_DATE = LG.TRANS_DATE AND LG1.TRANS_SEQ = LG.TRANS_SEQ AND LG1.SEQ = LG.SEQ AND LG1.NO_HST = LG.NO_HST
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = LG1.CD_COMPANY AND MU.ID_USER = LG1.ID_PRINT
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = SH.C_CODE AND MC.CD_FIELD = 'FI_T000013' AND MC.CD_SYSDEF = SH.TRANS_BANK_CODE
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = SH.C_CODE AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = SH.CD_EXCH
LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = SH.C_CODE AND MC2.CD_FIELD = 'CZ_FI00002' AND MC2.CD_SYSDEF = SH.TP_CHARGE
LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = SH.C_CODE AND MC3.CD_FIELD = 'CZ_FI00003' AND MC3.CD_SYSDEF = SH.TP_SEND_BY
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
ON PD.CD_COMPANY = SH.C_CODE AND PD.CD_PARTNER = SH.CUST_CODE AND PD.NO_DEPOSIT = SH.TRANS_ACCT_NO
WHERE SH.C_CODE = @P_C_CODE
AND SH.TRANS_DATE = @P_TRANS_DATE
AND SH.TRANS_SEQ = @P_TRANS_SEQ
ORDER BY SH.SEQ

GO