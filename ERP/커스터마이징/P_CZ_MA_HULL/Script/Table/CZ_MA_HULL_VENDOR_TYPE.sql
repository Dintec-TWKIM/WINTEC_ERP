USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MA_HULL_VENDOR_TYPE]    Script Date: 2020-01-22 오후 3:43:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MA_HULL_VENDOR_TYPE](
	[NO_IMO] [nvarchar](10) NOT NULL,
	[CD_VENDOR] [nvarchar](20) NOT NULL,
	[NO_TYPE] [int] NOT NULL,
	[CLS_L] [nvarchar](4) NULL,
	[CLS_M] [nvarchar](4) NULL,
	[CLS_S] [nvarchar](4) NULL,
	[DC_RMK1] [nvarchar](500) NULL,
	[DC_RMK2] [nvarchar](500) NULL,
	[DC_RMK3] [nvarchar](500) NULL,
	[DC_RMK4] [nvarchar](500) NULL,
	[DC_RMK5] [nvarchar](500) NULL,
	[DC_RMK6] [nvarchar](500) NULL,
	[DC_RMK7] [nvarchar](500) NULL,
	[DC_RMK8] [nvarchar](500) NULL,
	[DC_RMK9] [nvarchar](500) NULL,
	[DC_RMK10] [nvarchar](500) NULL,
	[DC_RMK11] [nvarchar](500) NULL,
	[DC_RMK12] [nvarchar](500) NULL,
	[DC_RMK13] [nvarchar](500) NULL,
	[DC_RMK14] [nvarchar](500) NULL,
	[DC_RMK15] [nvarchar](500) NULL,
	[DC_RMK] [nvarchar](500) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](10) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_P_CZ_MA_HULL_VENDOR_TYPE] PRIMARY KEY CLUSTERED 
(
	[NO_IMO] ASC,
	[CD_VENDOR] ASC,
	[NO_TYPE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


