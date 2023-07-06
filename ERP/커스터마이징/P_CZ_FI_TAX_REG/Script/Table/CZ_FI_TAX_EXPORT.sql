USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_FI_TAX_EXPORT]    Script Date: 2022-04-05 오후 4:54:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_FI_TAX_EXPORT](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_TAX] [nvarchar](20) NOT NULL,
	[NO_BL] [nvarchar](20) NOT NULL,
	[DT_TAX] [nvarchar](8) NULL,
	[NO_REF] [nvarchar](200) NULL,
	[NO_IMPORT] [nvarchar](100) NULL,
	[DT_LOADING] [nvarchar](8) NULL,
	[CD_EXCH] [nvarchar](3) NULL,
	[RT_EXCH] [numeric](15, 4) NULL,
	[AM_TAX_EX] [numeric](19, 4) NULL,
	[AM_TAX] [numeric](19, 4) NULL,
	[TP_GUBUN] [nvarchar](20) NULL,
	[AM_PAY] [numeric](19, 4) NULL,
	[YN_CHECK] [nvarchar](1) NULL,
	[YN_PARSING] [nvarchar](1) NULL,
	[DC_IMPORT] [nvarchar](100) NULL,
	[DC_TAX] [nvarchar](1000) NULL,
	[DC_RMK] [nvarchar](500) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_FI_TAX_EXPORT] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_TAX] ASC,
	[NO_BL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


