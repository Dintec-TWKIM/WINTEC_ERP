USE [NEOE]
GO

/****** Object:  Trigger [NEOE].[TU_CZ_MA_PARTNER]    Script Date: 2017-12-01 오후 1:55:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [NEOE].[TU_CZ_MA_PARTNER] ON [NEOE].[CZ_MA_PARTNER]
FOR UPDATE
AS

DECLARE @V_ID_UPDATE	NVARCHAR(15),
		@V_DTS_UPDATE	NVARCHAR(14)

SELECT @V_ID_UPDATE = ID_UPDATE,
	   @V_DTS_UPDATE = DTS_UPDATE
FROM INSERTED

INSERT INTO CZ_MA_PARTNER_LOG
(
	CD_COMPANY,
	CD_PARTNER,
	ID_UPDATE, 
	DTS_UPDATE,
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
	AM_EXCH, 
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
	DC_CEO_E_MAIL, 
	CD_PARTNER_RELATION, 
	DC_PHOTO,
	YN_CERT,
	CD_ORIGIN,
	TP_PRINT,
	TP_ATTACH_EMAIL,
	YN_USE_GIR,
	YN_GR_LABEL,
	NO_VAT,
	TP_PRICE_SENS,
	SHIPSERV_TNID,
	YN_HOLD,
	TP_INV,
	YN_COLOR,
	TP_PAY,
	TP_DELIVERY
)
SELECT CD_COMPANY,
	   CD_PARTNER,
	   @V_ID_UPDATE, 
	   @V_DTS_UPDATE,
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
	   AM_EXCH, 
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
	   DC_CEO_E_MAIL, 
	   CD_PARTNER_RELATION, 
	   DC_PHOTO,
	   YN_CERT,
	   CD_ORIGIN,
	   TP_PRINT,
	   TP_ATTACH_EMAIL,
	   YN_USE_GIR,
	   YN_GR_LABEL,
	   NO_VAT,
	   TP_PRICE_SENS,
	   SHIPSERV_TNID,
	   YN_HOLD,
	   TP_INV,
	   YN_COLOR,
	   TP_PAY,
	   TP_DELIVERY
FROM DELETED
GO

ALTER TABLE [NEOE].[CZ_MA_PARTNER] ENABLE TRIGGER [TU_CZ_MA_PARTNER]
GO

