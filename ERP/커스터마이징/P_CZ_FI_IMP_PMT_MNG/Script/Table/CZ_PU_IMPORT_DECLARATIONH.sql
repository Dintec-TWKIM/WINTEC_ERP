SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [NEOE].[CZ_PU_IMPORT_DECLARATIONH](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_IMPORT] [nvarchar](30) NOT NULL,
	[DT_IMPORT] [nvarchar](8) NULL,
	[CD_OFFICE] [nvarchar](10) NULL,
	[DT_ARRIVAL] [nvarchar](8) NULL,
	[NO_BL] [nvarchar](30) NULL,
	[NO_BL_MASTER] [nvarchar](30) NULL,
	[NO_CARGO] [nvarchar](30) NULL,
	[DECLARANT] [nvarchar](100) NULL,
	[IMPORTER] [nvarchar](100) NULL,
	[TAXPAYER] [nvarchar](100) NULL,
	[FORWARDER] [nvarchar](100) NULL,
	[TRADER] [nvarchar](100) NULL,
	[GROSS_WEIGHT] [numeric](10, 4) NULL,
	[ARRIVAL_PORT] [nvarchar](50) NULL,
	[EXPORT_COUNTRY] [nvarchar](50) NULL,
	[ITEM_NAME] [nvarchar](100) NULL,
	[CURRENCY] [nvarchar](3) NULL,
	[EXCHANGE_RATE] [numeric](10, 4) NULL,
	[TAXABLE_VAT] [numeric](17, 4) NULL,
	[TAX_CUSTOMS] [numeric](17, 4) NULL,
	[TAX_CONSUMPTION] [numeric](17, 4) NULL,
	[TAX_ENERGY] [numeric](17, 4) NULL,
	[TAX_LIQUOR] [numeric](17, 4) NULL,
	[TAX_EDUCATION] [numeric](17, 4) NULL,
	[TAX_RURAL] [numeric](17, 4) NULL,
	[TAX_VAT] [numeric](17, 4) NULL,
	[TAX_LATE_REPORT] [numeric](17, 4) NULL,
	[TAX_NO_REPORT] [numeric](17, 4) NULL,
	[YN_DONE] [nvarchar](1) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_PU_IMPORT_CERTH] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_IMPORT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
