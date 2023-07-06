USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_LN_CURR_TOTAL]    Script Date: 2019-06-20 오후 3:53:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_LN_CURR_TOTAL](
	[DAY] [nvarchar](8) NOT NULL,
	[EXC_R] [nvarchar](50) NULL,
	[YEN_EXC_R] [nvarchar](50) NULL,
	[EURO_EXC_R] [nvarchar](50) NULL,
	[CALL_R] [nvarchar](50) NULL,
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
	[EC_R_K] [nvarchar](50) NULL,
	[EC_R_US] [nvarchar](50) NULL,
	[GDP_K] [nvarchar](50) NULL,
	[GDP_US] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZ_LN_CURR_TOTAL] PRIMARY KEY CLUSTERED 
(
	[DAY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


