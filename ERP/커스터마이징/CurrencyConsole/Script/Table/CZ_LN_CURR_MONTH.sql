USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_LN_CURR_MONTH]    Script Date: 2019-06-07 오후 4:40:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_LN_CURR_MONTH](
	[MONTH] [nvarchar](6) NOT NULL,
	[CURR_BAL] [nvarchar](50) NULL,
	[CAPIT_BAL] [nvarchar](50) NULL,
	[KOSPI] [nvarchar](50) NULL,
	[DOW_JONES] [nvarchar](50) NULL,
	[NASDAQ] [nvarchar](50) NULL,
	[CUS_PR_K] [nvarchar](50) NULL,
	[CUS_PR_US] [nvarchar](50) NULL,
	[PROD_PR_K] [nvarchar](50) NULL,
	[PROD_PR_US] [nvarchar](50) NULL,
	[INDUST_K] [nvarchar](50) NULL,
	[INDUST_US] [nvarchar](50) NULL,
	[WTI] [nvarchar](50) NULL,
	[US_FED_R] [nvarchar](50) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_LN_CURR_ROW_DATA] PRIMARY KEY CLUSTERED 
(
	[MONTH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

