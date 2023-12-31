USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_BUSINESS_TRIP_SCHEDULE]    Script Date: 2022-10-20 오후 3:03:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_BUSINESS_TRIP_SCHEDULE](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_BIZ_TRIP] [nvarchar](20) NOT NULL,
	[NO_INDEX] [int] NOT NULL,
	[DT_FROM] [nvarchar](8) NULL,
	[DT_TO] [nvarchar](8) NULL,
	[DC_SCHEDULE] [nvarchar](300) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_BUSINESS_TRIP_SCHEDULE] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_BIZ_TRIP] ASC,
	[NO_INDEX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


