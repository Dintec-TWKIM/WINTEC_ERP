USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_FI_IMP_PMTH]    Script Date: 2017-11-07 오후 7:35:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_FI_IMP_PMTH](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_IMPORT] [nvarchar](14) NOT NULL,
	[NO_BL] [nvarchar](20) NULL,
	[CD_PAYMENT] [nvarchar](20) NULL,
	[DT_IMPORT] [nvarchar](8) NULL,
	[DT_LIMIT] [nvarchar](8) NULL,
	[DT_RETURN] [nvarchar](8) NULL,
	[DT_APPLICATION] [nvarchar](8) NULL,
	[NO_EMP] [nvarchar](10) NULL,
	[AM_VAT_BASE] [numeric](17, 4) NULL,
	[AM_VAT] [numeric](17, 4) NULL,
	[DC_RMK] [nvarchar](100) NULL,
	[DC_FREIGHT] [nvarchar](100) NULL,
	[NO_PAYMENT] [nvarchar](20) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_FI_IMP_PMTH1] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_IMPORT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


