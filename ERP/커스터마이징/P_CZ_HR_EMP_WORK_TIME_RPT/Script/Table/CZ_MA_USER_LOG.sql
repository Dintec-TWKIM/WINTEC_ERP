USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MA_USER_LOG]    Script Date: 2020-01-29 오후 4:30:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MA_USER_LOG](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[DT_WORK] [nvarchar](8) NOT NULL,
	[ID_USER] [nvarchar](10) NOT NULL,
	[DT_TIME] [datetime] NOT NULL,
	[PAGE_ID] [nvarchar](50) NULL,
	[PAGE_NAME] [nvarchar](50) NULL,
	[FLAG] [nvarchar](2) NULL,
	[IP] [nvarchar](15) NULL,
 CONSTRAINT [PK_CZ_MA_USER_LOG] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[ID_USER] ASC,
	[DT_WORK] ASC,
	[DT_TIME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

