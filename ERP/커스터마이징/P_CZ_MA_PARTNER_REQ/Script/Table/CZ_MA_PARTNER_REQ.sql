USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MA_PARTNER_REQ]    Script Date: 2023-04-24 오후 1:45:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MA_PARTNER_REQ](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_REQ] [nvarchar](20) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NULL,
	[LN_PARTNER] [nvarchar](50) NULL,
	[NO_COMPANY] [nvarchar](10) NULL,
	[NO_RES] [nvarchar](20) NULL,
	[FG_PARTNER] [nvarchar](3) NULL,
	[CLS_PARTNER] [nvarchar](3) NULL,
	[NM_CEO] [nvarchar](40) NULL,
	[CD_AREA] [nvarchar](3) NULL,
	[CD_NATION] [nvarchar](20) NULL,
	[NO_POST1] [nvarchar](15) NULL,
	[DC_ADS1_H] [nvarchar](400) NULL,
	[DC_ADS1_D] [nvarchar](400) NULL,
	[CD_PARTNER_GRP] [nvarchar](10) NULL,
	[CD_PARTNER_GRP_2] [nvarchar](10) NULL,
	[NO_TEL1] [nvarchar](20) NULL,
	[NO_FAX1] [nvarchar](20) NULL,
	[TP_JOB] [nvarchar](50) NULL,
	[CLS_JOB] [nvarchar](50) NULL,
	[TP_PTR] [nvarchar](3) NULL,
	[CD_EMP_PARTNER] [nvarchar](14) NULL,
	[NO_TEL] [nvarchar](20) NULL,
	[NO_HPEMP_PARTNER] [nvarchar](15) NULL,
	[E_MAIL] [nvarchar](80) NULL,
	[NO_FAX] [nvarchar](20) NULL,
	[CD_DEPOSIT] [nvarchar](3) NULL,
	[NO_DEPOSIT] [nvarchar](40) NULL,
	[CD_BANK] [nvarchar](20) NULL,
	[NM_BANK] [nvarchar](200) NULL,
	[CD_BANK_NATION] [nvarchar](3) NULL,
	[NO_SORT] [nvarchar](20) NULL,
	[NO_SWIFT] [nvarchar](20) NULL,
	[NM_DEPOSIT] [nvarchar](100) NULL,
	[DC_DEPOSIT_TEL] [nvarchar](50) NULL,
	[CD_DEPOSIT_NATION] [nvarchar](3) NULL,
	[DC_DEPOSIT_ADDRESS] [nvarchar](500) NULL,
	[NO_BANK_BIC] [nvarchar](20) NULL,
	[DC_RMK] [nvarchar](50) NULL,
	[CD_TPPO] [nvarchar](7) NULL,
	[CD_EXCH2] [nvarchar](3) NULL,
	[FG_PAYMENT] [nvarchar](3) NULL,
	[TP_COND_PRICE] [nvarchar](3) NULL,
	[FG_TAX] [nvarchar](3) NULL,
	[RT_PURCHASE_DC] [numeric](5, 2) NULL,
	[TP_SO] [nvarchar](4) NULL,
	[CD_EXCH1] [nvarchar](3) NULL,
	[TP_INQ_SEND] [nvarchar](3) NULL,
	[TP_PO_SEND] [nvarchar](3) NULL,
	[TP_INQ_RCV] [nvarchar](3) NULL,
	[TP_QTN_SEND] [nvarchar](3) NULL,
	[TP_TERMS_PAYMENT] [nvarchar](3) NULL,
	[TP_DELIVERY_CONDITION] [nvarchar](3) NULL,
	[RT_SALES_PROFIT] [numeric](5, 2) NULL,
	[RT_SALES_DC] [numeric](5, 2) NULL,
	[RT_COMMISSION] [numeric](5, 2) NULL,
	[DC_COMMISSION] [nvarchar](500) NULL,
	[TP_VAT] [nvarchar](3) NULL,
	[NM_TEXT] [nvarchar](500) NULL,
	[TP_INV] [nvarchar](3) NULL,
	[YN_JEONJA] [nvarchar](1) NULL,
	[NM_ORIGIN] [nvarchar](200) NULL,
	[NM_ORIGIN_RPT] [nvarchar](50) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_MA_PARTNER_REQ] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_REQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


