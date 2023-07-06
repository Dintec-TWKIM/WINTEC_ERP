USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PR_MATCHING_ITEM]    Script Date: 2017-10-23 오후 6:24:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PR_MATCHING_ITEM](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[CD_PITEM_L] [nvarchar](20) NULL,
	[CD_PITEM_C] [nvarchar](20) NULL,
	[CD_PITEM_R] [nvarchar](20) NULL,
	[NUM_SPEC1_MIN] [numeric](7, 4) NULL,
	[NUM_SPEC1_MAX] [numeric](7, 4) NULL,
	[NUM_SPEC2_MIN] [numeric](7, 4) NULL,
	[NUM_SPEC2_MAX] [numeric](7, 4) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](10) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_PR_MATCHING_ITEM] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PLANT] ASC,
	[CD_ITEM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


