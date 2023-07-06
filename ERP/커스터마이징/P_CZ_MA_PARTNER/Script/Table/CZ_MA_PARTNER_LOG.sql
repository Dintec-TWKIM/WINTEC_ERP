USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MA_PARTNER_LOG]    Script Date: 2022-09-23 오전 11:41:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MA_PARTNER_LOG](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[DTS_UPDATE] [nvarchar](14) NOT NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[LN_PARTNER] [nvarchar](50) NULL,
	[CD_EXCH1] [nvarchar](3) NULL,
	[TP_SO] [nvarchar](4) NULL,
	[TP_VAT] [nvarchar](3) NULL,
	[FG_BILL1] [nvarchar](3) NULL,
	[FG_BILL2] [nvarchar](3) NULL,
	[TP_COND_PRICE] [nvarchar](3) NULL,
	[CD_SALEGRP] [nvarchar](7) NULL,
	[TXT_USERDEF2] [nvarchar](3) NULL,
	[TXT_USERDEF3] [nvarchar](3) NULL,
	[CD_EXCH2] [nvarchar](3) NULL,
	[AM_EXCH] [numeric](5, 4) NULL,
	[CD_TPPO] [nvarchar](7) NULL,
	[FG_PAYMENT] [nvarchar](3) NULL,
	[FG_TAX] [nvarchar](3) NULL,
	[TP_UM_TAX] [nvarchar](3) NULL,
	[TP_INQ_PO] [nvarchar](3) NULL,
	[TP_PO] [nvarchar](3) NULL,
	[TP_INQ] [nvarchar](3) NULL,
	[TP_QTN] [nvarchar](3) NULL,
	[TP_DELIVERY_CONDITION] [nvarchar](3) NULL,
	[TP_TERMS_PAYMENT] [nvarchar](3) NULL,
	[RT_COMMISSION] [numeric](5, 2) NULL,
	[DC_COMMISSION] [nvarchar](500) NULL,
	[RT_SALES_PROFIT] [numeric](5, 2) NULL,
	[RT_SALES_DC] [numeric](5, 2) NULL,
	[RT_PURCHASE_DC] [numeric](5, 2) NULL,
	[DC_PURCHASE_MAIL] [nvarchar](255) NULL,
	[DC_CEO_E_MAIL] [nvarchar](100) NULL,
	[CD_PARTNER_RELATION] [nvarchar](20) NULL,
	[DC_PHOTO] [nvarchar](50) NULL,
	[YN_CERT] [nvarchar](1) NULL,
	[CD_ORIGIN] [nvarchar](20) NULL,
	[TP_PRINT] [nvarchar](5) NULL,
	[TP_ATTACH_EMAIL] [nvarchar](4) NULL,
	[YN_USE_GIR] [nvarchar](1) NULL,
	[YN_GR_LABEL] [nvarchar](1) NULL,
	[NO_VAT] [nvarchar](50) NULL,
	[TP_PRICE_SENS] [nvarchar](4) NULL,
	[SHIPSERV_TNID] [nvarchar](10) NULL,
	[YN_HOLD] [nvarchar](1) NULL,
	[TP_INV] [nvarchar](3) NULL,
	[YN_COLOR] [nvarchar](1) NULL,
	[TP_PAY] [nvarchar](3) NULL,
	[TP_DELIVERY] [nvarchar](3) NULL,
 CONSTRAINT [PK_CZ_MA_PARTNER_LOG] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PARTNER] ASC,
	[DTS_UPDATE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


