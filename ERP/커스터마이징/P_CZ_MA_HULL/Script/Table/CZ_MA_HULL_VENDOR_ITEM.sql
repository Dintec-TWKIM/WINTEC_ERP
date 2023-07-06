USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MA_HULL_VENDOR_ITEM]    Script Date: 2020-05-28 오후 2:01:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MA_HULL_VENDOR_ITEM](
	[NO_IMO] [nvarchar](10) NOT NULL,
	[CD_VENDOR] [nvarchar](20) NOT NULL,
	[NO_TYPE] [int] NOT NULL,
	[NO_ITEM] [int] NOT NULL,
	[CD_ITEM] [nvarchar](20) NULL,
	[QT_ITEM] [numeric](11, 0) NULL,
	[NO_PART] [nvarchar](100) NULL,
	[NM_PART] [nvarchar](500) NULL,
	[DC_MODEL] [nvarchar](100) NULL,
	[NO_POS] [nvarchar](100) NULL,
	[UCODE] [nvarchar](100) NULL,
	[UCODE2] [nvarchar](100) NULL,
	[NO_DRAWING] [nvarchar](100) NULL,
	[NO_DRAWING2] [nvarchar](100) NULL,
	[UNIT] [nvarchar](4) NULL,
	[DC_RMK] [nvarchar](500) NULL,
	[DC_RMK1] [nvarchar](500) NULL,
	[DC_RMK2] [nvarchar](500) NULL,
	[DC_RMK3] [nvarchar](500) NULL,
	[DC_RMK4] [nvarchar](500) NULL,
	[DC_RMK5] [nvarchar](500) NULL,
	[DC_RMK6] [nvarchar](500) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](10) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[DXDESC] [nvarchar](4000) NULL,
	[DC_RMK7] [nvarchar](500) NULL,
	[DC_RMK8] [nvarchar](500) NULL,
	[DC_RMK9] [nvarchar](500) NULL,
	[DC_RMK10] [nvarchar](500) NULL,
 CONSTRAINT [PK_CZ_MA_HULL_VENDOR_ITEM] PRIMARY KEY CLUSTERED 
(
	[NO_IMO] ASC,
	[CD_VENDOR] ASC,
	[NO_TYPE] ASC,
	[NO_ITEM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


