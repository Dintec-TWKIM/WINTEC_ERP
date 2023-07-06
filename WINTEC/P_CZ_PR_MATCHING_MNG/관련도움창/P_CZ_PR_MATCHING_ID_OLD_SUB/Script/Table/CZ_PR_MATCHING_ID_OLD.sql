USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PR_MATCHING_ID_OLD]    Script Date: 2022-12-27 오후 6:13:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PR_MATCHING_ID_OLD](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[NO_ID] [nvarchar](10) NOT NULL,
	[CD_ITEM] [nvarchar](20) NULL,
	[DC_SPEC_IN] [nvarchar](30) NULL,
	[DC_SPEC_OUT] [nvarchar](30) NULL,
	[NO_DATA_IN] [numeric](18, 4) NULL,
	[NO_DATA_OUT] [numeric](18, 4) NULL,
	[CD_CLEAR_GRP_IN] [nvarchar](4) NULL,
	[CD_CLEAR_GRP_OUT] [nvarchar](4) NULL,
	[NO_HEAT] [nvarchar](200) NULL,
	[NO_LOT] [nvarchar](20) NULL,
	[DC_RMK] [nvarchar](100) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](10) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[DTS_COUNTING] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_PR_MATCHING_ITEM_OLD] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PLANT] ASC,
	[NO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


