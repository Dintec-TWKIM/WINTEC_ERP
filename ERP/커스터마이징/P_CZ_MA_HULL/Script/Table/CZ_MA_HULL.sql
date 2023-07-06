USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MA_HULL]    Script Date: 2020-10-08 오후 5:33:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MA_HULL](
	[NO_IMO] [nvarchar](10) NOT NULL,
	[NO_HULL] [nvarchar](20) NOT NULL,
	[CALL_SIGN] [nvarchar](20) NULL,
	[NM_VESSEL] [nvarchar](50) NULL,
	[NM_CERT] [nvarchar](50) NULL,
	[NM_SHIP_OWNER] [nvarchar](80) NULL,
	[DT_SHIP_DLV] [nvarchar](6) NULL,
	[TP_SHIP_YARD] [nvarchar](5) NULL,
	[TP_SHIP] [nvarchar](3) NULL,
	[YN_NEWSHIP] [nvarchar](1) NULL,
	[CD_PARTNER] [nvarchar](20) NULL,
	[PURIFIER] [nvarchar](30) NULL,
	[BOILER] [nvarchar](30) NULL,
	[INCINERATOR] [nvarchar](30) NULL,
	[PUMP] [nvarchar](30) NULL,
	[REF_SYS] [nvarchar](30) NULL,
	[ER_MACH] [nvarchar](30) NULL,
	[DECK_MACH] [nvarchar](30) NULL,
	[PRE_MACH] [nvarchar](30) NULL,
	[DC_RMK] [nvarchar](500) NULL,
	[ME_SPEC] [nvarchar](20) NULL,
	[GE1_SPEC] [nvarchar](20) NULL,
	[GE2_SPEC] [nvarchar](20) NULL,
	[TC_SPEC] [nvarchar](20) NULL,
	[TC1_SPEC] [nvarchar](20) NULL,
	[TC2_SPEC] [nvarchar](20) NULL,
	[PUMP_SPEC] [nvarchar](20) NULL,
	[TP_ME_MAKER] [nvarchar](2) NULL,
	[TP_GE1_MAKER] [nvarchar](2) NULL,
	[TP_GE2_MAKER] [nvarchar](2) NULL,
	[TP_TC_MAKER] [nvarchar](2) NULL,
	[TP_TC1_MAKER] [nvarchar](2) NULL,
	[TP_TC2_MAKER] [nvarchar](2) NULL,
	[ME_CAPY] [numeric](10, 0) NULL,
	[GE1_CAPY] [numeric](10, 0) NULL,
	[GE2_CAPY] [numeric](10, 0) NULL,
	[ME_SERIAL] [nvarchar](20) NULL,
	[GE1_SERIAL] [nvarchar](20) NULL,
	[GE2_SERIAL] [nvarchar](20) NULL,
	[PUMP_SERIAL] [nvarchar](20) NULL,
	[PROPELLER_SERIAL] [nvarchar](20) NULL,
	[STERNPOST_SERIAL] [nvarchar](20) NULL,
	[INVOICE_COMPANY] [nvarchar](max) NULL,
	[INVOICE_ADDRESS] [nvarchar](max) NULL,
	[INVOICE_TEL] [nvarchar](max) NULL,
	[INVOICE_EMAIL] [nvarchar](max) NULL,
	[INVOICE_RMK] [nvarchar](max) NULL,
	[TP_VAT] [nvarchar](3) NULL,
	[CD_NATION] [nvarchar](3) NULL,
	[NO_ORIGIN_BUYER] [nvarchar](20) NULL,
	[NO_HULL_HHI] [nvarchar](50) NULL,
	[NM_VESSEL_HHI] [nvarchar](100) NULL,
	[NO_HULL_HHI_2ND] [nvarchar](50) NULL,
	[QT_DEAD_WEIGHT] [numeric](18, 0) NULL,
	[YN_DOM] [nvarchar](1) NULL,
	[YN_SCRUBBER] [nvarchar](1) NULL,
	[DC_BWTS] [nvarchar](100) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[DC_SHIPBUILDER] [nvarchar](100) NULL,
	[CD_SHIPBUILDER] [nvarchar](10) NULL,
	[NO_HULL_HGS] [nvarchar](20) NULL,
 CONSTRAINT [PK_CZ_MA_HULL] PRIMARY KEY CLUSTERED 
(
	[NO_IMO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


