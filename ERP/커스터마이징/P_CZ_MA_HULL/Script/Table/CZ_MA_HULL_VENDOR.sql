USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MA_HULL_VENDOR]    Script Date: 2020-01-22 오후 3:42:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MA_HULL_VENDOR](
	[NO_IMO] [nvarchar](10) NOT NULL,
	[CD_VENDOR] [nvarchar](20) NOT NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](10) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_MA_HULL_VENDOR] PRIMARY KEY CLUSTERED 
(
	[NO_IMO] ASC,
	[CD_VENDOR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


