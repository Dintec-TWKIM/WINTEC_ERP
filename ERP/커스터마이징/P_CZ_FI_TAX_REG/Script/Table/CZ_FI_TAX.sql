USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_FI_TAX]    Script Date: 2016-03-23 오후 8:47:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_FI_TAX](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_IV] [nvarchar](20) NOT NULL,
	[NO_IO] [nvarchar](20) NOT NULL,
	[NO_SO] [nvarchar](20) NOT NULL,
	[NO_TO] [nvarchar](20) NULL,
	[DT_TO] [nvarchar](8) NULL,
	[DT_LOADING] [nvarchar](8) NULL,
	[CD_EXCH] [nvarchar](3) NULL,
	[RT_EXCH] [numeric](15, 4) NULL,
	[AM_EX] [numeric](19, 4) NULL,
	[AM] [numeric](19, 4) NULL,
	[AM_TAX_EX] [numeric](19, 4) NULL,
	[AM_TAX] [numeric](19, 4) NULL,
	[DT_SHIPPING] [nvarchar](8) NULL,
	[DT_VAT] [nvarchar](8) NULL,
	[TP_EXPORT] [nvarchar](3) NULL,
	[DC_RMK] [nvarchar](550) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_FI_TAX_1] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_IV] ASC,
	[NO_IO] ASC,
	[NO_SO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


