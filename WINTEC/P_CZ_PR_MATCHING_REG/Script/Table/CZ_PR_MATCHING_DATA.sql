USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PR_MATCHING_DATA]    Script Date: 2020-07-10 오전 9:16:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PR_MATCHING_DATA](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[NO_ID_L] [nvarchar](20) NOT NULL,
	[NO_ID_C] [nvarchar](20) NOT NULL,
	[NO_ID_R] [nvarchar](20) NOT NULL,
	[STA_MATCHING] [nvarchar](4) NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[NO_WO_L] [nvarchar](20) NULL,
	[CD_PITEM_L] [nvarchar](20) NULL,
	[NUM_P1_L] [numeric](7, 4) NULL,
	[NUM_P2_L] [numeric](7, 4) NULL,
	[NUM_P3_L] [numeric](7, 4) NULL,
	[NUM_MIN_L] [numeric](7, 4) NULL,
	[NUM_C1] [numeric](7, 4) NULL,
	[NO_WO_C] [nvarchar](20) NULL,
	[CD_PITEM_C] [nvarchar](20) NULL,
	[NUM_P1_OUT_C] [numeric](7, 4) NULL,
	[NUM_P2_OUT_C] [numeric](7, 4) NULL,
	[NUM_P3_OUT_C] [numeric](7, 4) NULL,
	[NUM_MAX_C] [numeric](7, 4) NULL,
	[NUM_P1_IN_C] [numeric](7, 4) NULL,
	[NUM_P2_IN_C] [numeric](7, 4) NULL,
	[NUM_P3_IN_C] [numeric](7, 4) NULL,
	[NUM_MIN_C] [numeric](7, 4) NULL,
	[NUM_C2] [numeric](7, 4) NULL,
	[NO_WO_R] [nvarchar](20) NULL,
	[CD_PITEM_R] [nvarchar](20) NULL,
	[NUM_P1_R] [numeric](7, 4) NULL,
	[NUM_P2_R] [numeric](7, 4) NULL,
	[NUM_P3_R] [numeric](7, 4) NULL,
	[NUM_MAX_R] [numeric](7, 4) NULL,
	[NUM_MATCHING_SIZE1] [numeric](7, 4) NULL,
	[NUM_MATCHING_SIZE2] [numeric](7, 4) NULL,
	[NO_SO] [nvarchar](20) NULL,
	[DC_RMK] [nvarchar](500) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_PR_MATCHING_DATA] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PLANT] ASC,
	[NO_ID_L] ASC,
	[NO_ID_C] ASC,
	[NO_ID_R] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


