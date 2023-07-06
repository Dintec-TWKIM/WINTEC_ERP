USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PR_MATCHING_DATA]    Script Date: 2021-12-28 오전 11:37:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PR_MATCHING_DATA](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[NO_WO] [nvarchar](20) NOT NULL,
	[NO_LINE] [numeric](5, 0) NOT NULL,
	[SEQ_WO] [numeric](5, 0) NOT NULL,
	[NO_ID] [nvarchar](10) NOT NULL,
	[TP_POS] [nvarchar](4) NOT NULL,
	[NO_ID_C] [nvarchar](10) NOT NULL,
	[CD_PITEM] [nvarchar](20) NULL,
	[CD_GRADE_IN] [nvarchar](1) NULL,
	[CD_GRADE_OUT] [nvarchar](1) NULL,
	[NO_DATA_IN] [numeric](18, 4) NULL,
	[NO_DATA_OUT] [numeric](18, 4) NULL,
	[QT_SPEC_IN] [numeric](6, 4) NULL,
	[QT_SPEC_OUT] [numeric](6, 4) NULL,
	[CD_CLEAR_GRP_IN] [nvarchar](4) NULL,
	[CD_CLEAR_GRP_OUT] [nvarchar](4) NULL,
	[NO_HEAT] [nvarchar](200) NULL,
	[NO_LOT] [nvarchar](20) NULL,
	[DT_MATCH] [nvarchar](8) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](10) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_PR_MATCHING_DATA] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PLANT] ASC,
	[NO_WO] ASC,
	[NO_LINE] ASC,
	[SEQ_WO] ASC,
	[NO_ID] ASC,
	[TP_POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


