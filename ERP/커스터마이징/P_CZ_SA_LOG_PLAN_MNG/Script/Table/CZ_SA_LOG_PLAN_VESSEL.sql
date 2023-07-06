USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_LOG_PLAN_VESSEL]    Script Date: 2023-03-10 오전 11:49:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_LOG_PLAN_VESSEL](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_REV] [int] NOT NULL,
	[NO_IDX] [int] NOT NULL,
	[NM_VESSEL] [nvarchar](50) NULL,
	[NO_IMO] [nvarchar](10) NULL,
	[DC_LOCATION] [nvarchar](20) NULL,
	[DC_PORT] [nvarchar](20) NULL,
	[DC_EMP] [nvarchar](100) NULL,
	[DC_ETB] [nvarchar](10) NULL,
	[DC_ETD] [nvarchar](10) NULL,
	[DT_COMPLETE] [nvarchar](8) NULL,
	[DC_CAR] [nvarchar](50) NULL,
	[DC_ITEM] [nvarchar](max) NULL,
	[DC_TEL] [nvarchar](10) NULL,
	[DC_ETC] [nvarchar](max) NULL,
	[NO_GIR] [nvarchar](1000) NULL,
	[DC_SORT] [nvarchar](10) NULL,
	[ID_INSERT] [nvarchar](14) NULL,
	[DTS_INSERT] [nvarchar](15) NULL,
	[ID_UPDATE] [nvarchar](14) NULL,
	[DTS_UPDATE] [nvarchar](15) NULL,
 CONSTRAINT [PK_CZ_SA_LOG_PLAN_VESSEL] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_REV] ASC,
	[NO_IDX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


