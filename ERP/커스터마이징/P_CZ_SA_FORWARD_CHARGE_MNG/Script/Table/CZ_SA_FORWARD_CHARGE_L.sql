USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_FORWARD_CHARGE_L]    Script Date: 2022-05-13 오후 2:21:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_FORWARD_CHARGE_L](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[TP_DELIVERY] [nvarchar](4) NOT NULL,
	[DT_MONTH] [nvarchar](6) NOT NULL,
	[SEQ] [int] NOT NULL,
	[NO_BL] [nvarchar](20) NOT NULL,
	[NO_REF] [nvarchar](300) NOT NULL,
	[NO_IO] [nvarchar](20) NULL,
	[DT_FORWARD] [nvarchar](8) NULL,
	[AM_CHARGE] [numeric](18, 4) NULL,
	[AM_ETC] [numeric](18, 4) NULL,
	[AM_VAT_WEEK] [numeric](18, 4) NULL,
	[AM_WEEK] [numeric](18, 4) NULL,
	[AM_CHARGE_MONTH] [numeric](18, 4) NULL,
	[AM_ETC_MONTH] [numeric](18, 4) NULL,
	[AM_VAT_MONTH] [numeric](18, 4) NULL,
	[AM_VAT_TARGET] [numeric](18, 4) NULL,
	[AM_WFG] [numeric](18, 4) NULL,
	[AM_MONTH] [numeric](18, 4) NULL,
	[AM_TAX_MONTH] [numeric](18, 4) NULL,
	[AM_FULL] [numeric](18, 4) NULL,
	[RT_DC] [numeric](18, 4) NULL,
	[RT_DC_IV] [numeric](18, 4) NULL,
	[AM_IV] [numeric](18, 4) NULL,
	[AM_TAX] [numeric](18, 4) NULL,
	[WEIGHT] [numeric](18, 2) NULL,
	[ZONE] [int] NULL,
	[RT_FSC] [numeric](18, 4) NULL,
	[AM_ZONE] [numeric](18, 4) NULL,
	[DT_TAX_MONTH] [nvarchar](6) NULL,
	[SEQ_TAX_MONTH] [int] NULL,
	[NM_NOTE] [nvarchar](100) NULL,
	[NO_MDOCU] [nvarchar](20) NULL,
	[DC_RMK] [nvarchar](500) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_FORWARD_CHARGE] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PARTNER] ASC,
	[TP_DELIVERY] ASC,
	[DT_MONTH] ASC,
	[SEQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


