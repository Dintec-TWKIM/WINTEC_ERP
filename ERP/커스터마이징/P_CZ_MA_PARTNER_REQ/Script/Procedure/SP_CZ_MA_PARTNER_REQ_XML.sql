USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_PARTNER_REQ_XML]    Script Date: 2016-08-19 오후 5:25:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_MA_PARTNER_REQ_XML] 
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10), 
    @DOC			INT = NULL
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML 

-- ================================================== DELETE
DELETE A 
FROM CZ_MA_PARTNER_REQ A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY NVARCHAR(7),
			  NO_REQ	 NVARCHAR(20)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_REQ = B.NO_REQ

DELETE A 
FROM FI_GWDOCU A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY NVARCHAR(7),
			  NO_REQ	 NVARCHAR(20)) B 
ON A.CD_COMPANY = 'K100'
AND A.CD_PC = '010000'
AND A.NO_DOCU = B.CD_COMPANY + '-' + B.NO_REQ
-- ================================================== INSERT
INSERT INTO CZ_MA_PARTNER_REQ 
(
	CD_COMPANY,
	NO_REQ,
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
	TP_PTR,
	CD_EMP_PARTNER,
	NO_TEL,
	NO_HPEMP_PARTNER,
	E_MAIL,
	NO_FAX,
	CD_DEPOSIT,
	NO_DEPOSIT,
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
	NM_TEXT,
	TP_INV,
	YN_JEONJA,
	NM_ORIGIN,
	NM_ORIGIN_RPT,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   NO_REQ,
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
	   TP_PTR,
	   CD_EMP_PARTNER,
	   NO_TEL,
	   NO_HPEMP_PARTNER,
	   E_MAIL,
	   NO_FAX,
	   CD_DEPOSIT,
	   NO_DEPOSIT,
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
	   NM_TEXT,
	   TP_INV,
	   YN_JEONJA,
	   NM_ORIGIN,
	   NM_ORIGIN_RPT,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY				NVARCHAR(7),
				NO_REQ					NVARCHAR(20),
				LN_PARTNER				NVARCHAR(50),
				NO_COMPANY				NVARCHAR(10),
				NO_RES					NVARCHAR(20),
				FG_PARTNER				NVARCHAR(3),
				CLS_PARTNER				NVARCHAR(3),
				NM_CEO					NVARCHAR(40),
				CD_AREA					NVARCHAR(3),
				CD_NATION				NVARCHAR(20),
				NO_POST1				NVARCHAR(15),
				DC_ADS1_H				NVARCHAR(400),
				DC_ADS1_D				NVARCHAR(400),
				CD_PARTNER_GRP			NVARCHAR(10),
				CD_PARTNER_GRP_2		NVARCHAR(10),
				NO_TEL1					NVARCHAR(20),
				NO_FAX1					NVARCHAR(20),
				TP_JOB					NVARCHAR(50),
				CLS_JOB					NVARCHAR(50),
				TP_PTR					NVARCHAR(3),
				CD_EMP_PARTNER			NVARCHAR(14),
				NO_TEL					NVARCHAR(20),
				NO_HPEMP_PARTNER		NVARCHAR(15),
				E_MAIL					NVARCHAR(50),
				NO_FAX					NVARCHAR(20),
				CD_DEPOSIT				NVARCHAR(3),
				NO_DEPOSIT				NVARCHAR(40),
				CD_BANK					NVARCHAR(3),
				NM_BANK					NVARCHAR(200),
				CD_BANK_NATION			NVARCHAR(3),
				NO_SORT					NVARCHAR(20),
				CD_DEPOSIT_NATION		NVARCHAR(3),
				NO_SWIFT				NVARCHAR(20),
				NM_DEPOSIT				NVARCHAR(100),
				DC_DEPOSIT_TEL			NVARCHAR(50),
				DC_DEPOSIT_ADDRESS		NVARCHAR(500),
				NO_BANK_BIC				NVARCHAR(20),
				DC_RMK					NVARCHAR(50),
				CD_TPPO					NVARCHAR(7),
				CD_EXCH2				NVARCHAR(3),
				FG_PAYMENT				NVARCHAR(3),
				TP_COND_PRICE			NVARCHAR(3),
				FG_TAX					NVARCHAR(3),
				RT_PURCHASE_DC			NUMERIC(5, 2),
				TP_SO					NVARCHAR(4),
				CD_EXCH1				NVARCHAR(3),
				TP_INQ_SEND				NVARCHAR(3),
				TP_PO_SEND				NVARCHAR(3),
				TP_INQ_RCV				NVARCHAR(3),
				TP_QTN_SEND				NVARCHAR(3),
				TP_TERMS_PAYMENT		NVARCHAR(3),
				TP_DELIVERY_CONDITION	NVARCHAR(3),
				RT_SALES_PROFIT			NUMERIC(5, 2),
				RT_SALES_DC				NUMERIC(5, 2),
				RT_COMMISSION			NUMERIC(5, 2),
				DC_COMMISSION			NVARCHAR(500),
				TP_VAT					NVARCHAR(3),
				NM_TEXT					NVARCHAR(500),
				TP_INV					NVARCHAR(3),
				YN_JEONJA			    NVARCHAR(1),
				NM_ORIGIN			    NVARCHAR(200),
				NM_ORIGIN_RPT			NVARCHAR(50)) 
-- ================================================== UPDATE    
UPDATE A 
   SET A.LN_PARTNER = B.LN_PARTNER,
	   A.NO_COMPANY = B.NO_COMPANY,
	   A.NO_RES = B.NO_RES,
	   A.FG_PARTNER = B.FG_PARTNER,
	   A.CLS_PARTNER = B.CLS_PARTNER, 
	   A.NM_CEO = B.NM_CEO,
	   A.CD_AREA = B.CD_AREA,
	   A.CD_NATION = B.CD_NATION,
	   A.NO_POST1 = B.NO_POST1,
	   A.DC_ADS1_H = B.DC_ADS1_H,
	   A.DC_ADS1_D = B.DC_ADS1_D,
	   A.CD_PARTNER_GRP = B.CD_PARTNER_GRP,
	   A.CD_PARTNER_GRP_2 = B.CD_PARTNER_GRP_2,
	   A.NO_TEL1 = B.NO_TEL1,
	   A.NO_FAX1 = B.NO_FAX1,
	   A.TP_JOB = B.TP_JOB,
	   A.CLS_JOB = B.CLS_JOB,
	   A.TP_PTR = B.TP_PTR,
	   A.CD_EMP_PARTNER = B.CD_EMP_PARTNER,
	   A.NO_TEL = B.NO_TEL,
	   A.NO_HPEMP_PARTNER = B.NO_HPEMP_PARTNER,
	   A.E_MAIL = B.E_MAIL,
	   A.NO_FAX = B.NO_FAX,
	   A.CD_DEPOSIT = B.CD_DEPOSIT,
	   A.NO_DEPOSIT = B.NO_DEPOSIT,
	   A.CD_BANK = B.CD_BANK,
	   A.NM_BANK = B.NM_BANK,
	   A.CD_BANK_NATION = B.CD_BANK_NATION,
	   A.NO_SORT = B.NO_SORT,
	   A.CD_DEPOSIT_NATION = B.CD_DEPOSIT_NATION,
	   A.NO_SWIFT = B.NO_SWIFT,
	   A.NM_DEPOSIT = B.NM_DEPOSIT,
	   A.DC_DEPOSIT_TEL = B.DC_DEPOSIT_TEL,
	   A.DC_DEPOSIT_ADDRESS = B.DC_DEPOSIT_ADDRESS,
	   A.NO_BANK_BIC = B.NO_BANK_BIC,
	   A.DC_RMK = B.DC_RMK,
	   A.CD_TPPO = B.CD_TPPO,
	   A.CD_EXCH2 = B.CD_EXCH2,
	   A.FG_PAYMENT = B.FG_PAYMENT,
	   A.TP_COND_PRICE = B.TP_COND_PRICE,
	   A.FG_TAX = B.FG_TAX,
	   A.RT_PURCHASE_DC = B.RT_PURCHASE_DC,
	   A.TP_SO = B.TP_SO,
	   A.CD_EXCH1 = B.CD_EXCH1,
	   A.TP_INQ_SEND = B.TP_INQ_SEND,
	   A.TP_PO_SEND = B.TP_PO_SEND,
	   A.TP_INQ_RCV = B.TP_INQ_RCV,
	   A.TP_QTN_SEND = B.TP_QTN_SEND,
	   A.TP_TERMS_PAYMENT = B.TP_TERMS_PAYMENT,
	   A.TP_DELIVERY_CONDITION = B.TP_DELIVERY_CONDITION,
	   A.RT_SALES_PROFIT = B.RT_SALES_PROFIT,
	   A.RT_SALES_DC = B.RT_SALES_DC,
	   A.RT_COMMISSION = B.RT_COMMISSION,
	   A.DC_COMMISSION = B.DC_COMMISSION,
	   A.TP_VAT = B.TP_VAT,
	   A.NM_TEXT = B.NM_TEXT,
	   A.TP_INV = B.TP_INV,
	   A.YN_JEONJA = B.YN_JEONJA,
	   A.NM_ORIGIN = B.NM_ORIGIN,
	   A.NM_ORIGIN_RPT = B.NM_ORIGIN_RPT,
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_MA_PARTNER_REQ A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY				NVARCHAR(7),
				NO_REQ					NVARCHAR(20),
				LN_PARTNER				NVARCHAR(50),
				NO_COMPANY				NVARCHAR(10),
				NO_RES					NVARCHAR(20),
				FG_PARTNER				NVARCHAR(3),
				CLS_PARTNER				NVARCHAR(3),
				NM_CEO					NVARCHAR(40),
				CD_AREA					NVARCHAR(3),
				CD_NATION				NVARCHAR(20),
				NO_POST1				NVARCHAR(15),
				DC_ADS1_H				NVARCHAR(400),
				DC_ADS1_D				NVARCHAR(400),
				CD_PARTNER_GRP			NVARCHAR(10),
				CD_PARTNER_GRP_2		NVARCHAR(10),
				NO_TEL1					NVARCHAR(20),
				NO_FAX1					NVARCHAR(20),
				TP_JOB					NVARCHAR(50),
				CLS_JOB					NVARCHAR(50),
				TP_PTR					NVARCHAR(3),
				CD_EMP_PARTNER			NVARCHAR(14),
				NO_TEL					NVARCHAR(20),
				NO_HPEMP_PARTNER		NVARCHAR(15),
				E_MAIL					NVARCHAR(50),
				NO_FAX					NVARCHAR(20),
				CD_DEPOSIT				NVARCHAR(3),
				NO_DEPOSIT				NVARCHAR(40),
				CD_BANK					NVARCHAR(20),
				NM_BANK					NVARCHAR(200),
				CD_BANK_NATION			NVARCHAR(3),
				NO_SORT					NVARCHAR(20),
				CD_DEPOSIT_NATION		NVARCHAR(3),
				NO_SWIFT			    NVARCHAR(20),
				NM_DEPOSIT				NVARCHAR(100),
				DC_DEPOSIT_TEL			NVARCHAR(50),
				DC_DEPOSIT_ADDRESS		NVARCHAR(500),
				NO_BANK_BIC				NVARCHAR(20),
				DC_RMK					NVARCHAR(50),
				CD_TPPO					NVARCHAR(7),
				CD_EXCH2				NVARCHAR(3),
				FG_PAYMENT				NVARCHAR(3),
				TP_COND_PRICE			NVARCHAR(3),
				FG_TAX					NVARCHAR(3),
				RT_PURCHASE_DC			NUMERIC(5, 2),
				TP_SO					NVARCHAR(4),
				CD_EXCH1				NVARCHAR(3),
				TP_INQ_SEND				NVARCHAR(3),
				TP_PO_SEND				NVARCHAR(3),
				TP_INQ_RCV				NVARCHAR(3),
				TP_QTN_SEND				NVARCHAR(3),
				TP_TERMS_PAYMENT		NVARCHAR(3),
				TP_DELIVERY_CONDITION	NVARCHAR(3),
				RT_SALES_PROFIT			NUMERIC(5, 2),
				RT_SALES_DC				NUMERIC(5, 2),
				RT_COMMISSION			NUMERIC(5, 2),
				DC_COMMISSION			NVARCHAR(500),
				TP_VAT					NVARCHAR(3),
				NM_TEXT					NVARCHAR(500),
				TP_INV					NVARCHAR(3),
				YN_JEONJA			    NVARCHAR(1),
				NM_ORIGIN			    NVARCHAR(200),
				NM_ORIGIN_RPT			NVARCHAR(50)) B 
  ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_REQ = B.NO_REQ 

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO