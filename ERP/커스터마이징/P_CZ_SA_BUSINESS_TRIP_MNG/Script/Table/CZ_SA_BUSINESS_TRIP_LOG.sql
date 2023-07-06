USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_BUSINESS_TRIP_LOG]    Script Date: 2020-01-15 오후 3:21:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_BUSINESS_TRIP_LOG](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_BIZ_TRIP] [nvarchar](20) NOT NULL,
	[SEQ] [numeric](18, 0) NOT NULL,
	[DT_START] [nvarchar](8) NULL,
	[DT_END] [nvarchar](8) NULL,
	[DC_DATE] [nvarchar](100) NULL,
	[DC_LOCATION] [nvarchar](300) NULL,
	[DC_PURPOSE] [nvarchar](500) NULL,
	[DC_START] [nvarchar](max) NULL,
	[DC_END] [nvarchar](max) NULL,
	[TP_LOG] [nvarchar](1) NULL,
	[ID_LOG] [nvarchar](15) NULL,
	[DTS_LOG] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_BUSINESS_TRIP_LOG] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_BIZ_TRIP] ASC,
	[SEQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

