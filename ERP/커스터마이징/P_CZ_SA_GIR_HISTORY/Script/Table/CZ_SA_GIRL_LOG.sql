USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_GIRL_LOG]    Script Date: 2017-04-12 오전 10:34:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_GIRL_LOG](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[TP_GIR] [nvarchar](4) NOT NULL,
	[NO_GIR] [nvarchar](20) NOT NULL,
	[NO_HST] [numeric](5, 0) NOT NULL,
	[SEQ_GIR] [numeric](5, 0) NOT NULL,
	[GI_PARTNER] [nvarchar](20) NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[TP_ITEM] [nvarchar](3) NULL,
	[UNIT] [nvarchar](3) NULL,
	[DT_DUEDATE] [nvarchar](8) NULL,
	[DT_REQGI] [nvarchar](8) NULL,
	[YN_INSPECT] [nchar](1) NULL,
	[QT_GIR] [numeric](17, 4) NULL,
	[UM] [numeric](15, 4) NULL,
	[AM_GIR] [numeric](17, 4) NULL,
	[CD_EXCH] [nvarchar](3) NULL,
	[RT_EXCH] [numeric](15, 4) NULL,
	[AM_GIRAMT] [numeric](17, 4) NULL,
	[NO_PROJECT] [nvarchar](20) NULL,
	[CD_SALEGRP] [nvarchar](7) NULL,
	[NO_EMP] [nvarchar](20) NULL,
	[NO_SO] [nvarchar](20) NULL,
	[SEQ_SO] [numeric](5, 0) NULL,
	[QT_GI] [numeric](17, 4) NULL,
	[TP_GI] [nvarchar](3) NULL,
	[TP_IV] [nvarchar](3) NULL,
	[TP_BUSI] [nvarchar](3) NULL,
	[GIR] [nchar](1) NULL,
	[IV] [nchar](1) NULL,
	[RET] [nchar](1) NULL,
	[TP_VAT] [nvarchar](3) NULL,
	[RT_VAT] [numeric](5, 2) NULL,
	[QT_GIR_IM] [numeric](17, 4) NULL,
	[QT_GI_IM] [numeric](17, 4) NULL,
	[FG_TAXP] [nchar](3) NULL,
	[AM_VAT] [numeric](17, 4) NULL,
	[CD_SL] [nvarchar](7) NULL,
	[NO_IO_MGMT] [nvarchar](20) NULL,
	[NO_IOLINE_MGMT] [numeric](5, 0) NULL,
	[NO_SO_MGMT] [nvarchar](20) NULL,
	[NO_SOLINE_MGMT] [numeric](5, 0) NULL,
	[AM_EXGI] [numeric](17, 4) NULL,
	[AM_GI] [numeric](17, 4) NULL,
	[FG_LC_OPEN] [nvarchar](3) NULL,
	[DC_RMK] [nvarchar](500) NULL,
	[NO_INV] [nvarchar](20) NULL,
	[TP_UM_TAX] [nvarchar](3) NULL,
	[UMVAT_GIR] [numeric](15, 4) NULL,
	[ID_MEMO] [nvarchar](36) NULL,
	[QT_GIR_STOCK] [numeric](17, 4) NULL,
	[YN_GI_STOCK] [nvarchar](1) NULL,
	[YN_ADD_STOCK] [nvarchar](1) NULL,
	[ID_GI_STOCK] [nvarchar](15) NULL,
	[DTS_GI_STOCK] [nvarchar](14) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_DELETE] [nvarchar](14) NULL,
	[ID_DELETE] [nvarchar](15) NULL,
 CONSTRAINT [PK_CZ_SA_GIRL_LOG1] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_GIR] ASC,
	[NO_HST] ASC,
	[TP_GIR] ASC,
	[SEQ_GIR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


