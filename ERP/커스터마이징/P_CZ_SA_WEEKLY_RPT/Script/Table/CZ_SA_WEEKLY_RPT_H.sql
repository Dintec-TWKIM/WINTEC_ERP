USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_WEEKLY_RPT_H]    Script Date: 2022-09-06 오전 10:34:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_WEEKLY_RPT_H](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_EMP] [nvarchar](10) NOT NULL,
	[DT_YEAR] [nvarchar](4) NOT NULL,
	[QT_WEEK] [int] NOT NULL,
	[YN_CONFIRM] [nvarchar](1) NULL,
	[YN_POST] [nvarchar](1) NULL,
	[DT_FROM] [nvarchar](8) NULL,
	[DT_TO] [nvarchar](8) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_WEEKLY_RPT_H_1] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_EMP] ASC,
	[DT_YEAR] ASC,
	[QT_WEEK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


