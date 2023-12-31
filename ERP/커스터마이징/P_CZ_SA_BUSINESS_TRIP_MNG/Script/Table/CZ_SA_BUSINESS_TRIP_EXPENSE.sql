USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_BUSINESS_TRIP_EXPENSE]    Script Date: 2017-11-20 오후 3:26:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_BUSINESS_TRIP_EXPENSE](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_BIZ_TRIP] [nvarchar](20) NOT NULL,
	[NO_INDEX] [numeric](18, 0) NOT NULL,
	[TP_EXPENSE] [nvarchar](100) NULL,
	[AM_EXPENSE] [numeric](18, 4) NULL,
	[DC_EXPENSE] [nvarchar](200) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_BUSINESS_TRIP_EXPENSE] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_BIZ_TRIP] ASC,
	[NO_INDEX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


