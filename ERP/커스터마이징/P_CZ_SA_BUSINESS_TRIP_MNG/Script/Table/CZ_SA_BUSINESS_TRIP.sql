USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_BUSINESS_TRIP]    Script Date: 2017-11-21 오후 1:45:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_BUSINESS_TRIP](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_BIZ_TRIP] [nvarchar](20) NOT NULL,
	[DT_START] [nvarchar](8) NULL,
	[DT_END] [nvarchar](8) NULL,
	[DC_DATE] [nvarchar](100) NULL,
	[DC_LOCATION] [nvarchar](300) NULL,
	[DC_PURPOSE] [nvarchar](500) NULL,
	[DC_START] [nvarchar](max) NULL,
	[DC_END] [nvarchar](max) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_BUSINESS_TRIP] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_BIZ_TRIP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


