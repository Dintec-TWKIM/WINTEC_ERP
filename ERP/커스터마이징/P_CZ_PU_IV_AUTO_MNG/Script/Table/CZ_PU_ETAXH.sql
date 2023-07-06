USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PU_ETAXH]    Script Date: 2021-07-23 오후 3:18:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PU_ETAXH](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_ETAX] [nvarchar](30) NOT NULL,
	[NO_COMPANY] [nvarchar](10) NULL,
	[NO_COMPANY_REF] [nvarchar](10) NULL,
	[DT_SEND] [nvarchar](10) NULL,
	[AM] [numeric](17, 4) NULL,
	[VAT] [numeric](17, 4) NULL,
	[DC_REASON] [nvarchar](500) NULL,
	[DC_RMK1] [nvarchar](500) NULL,
	[DC_RMK2] [nvarchar](500) NULL,
	[PLATFORM] [nvarchar](50) NULL,
	[NO_FILE] [nvarchar](4000) NULL,
	[NO_IO] [nvarchar](500) NULL,
	[NO_IV] [nvarchar](20) NULL,
	[NO_DOCU] [nvarchar](20) NULL,
	[YN_READ] [nvarchar](1) NULL,
	[DC_LOG] [nvarchar](500) NULL,
	[NO_FILE_SEND] [nvarchar](4000) NULL,
	[YN_CONFIRM] [nvarchar](1) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_PU_ETAXH] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_ETAX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


