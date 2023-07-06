USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_AUTO_MAIL_SA_READY_LOG]    Script Date: 2022-08-03 ¿ÀÀü 9:21:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_AUTO_MAIL_SA_READY_LOG](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[DT_SEND] [nvarchar](8) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[NO_IMO] [nvarchar](10) NOT NULL,
	[CD_PARTNER_GRP] [nvarchar](10) NOT NULL,
	[TO_EMAIL] [nvarchar](max) NOT NULL,
	[IDX] [int] NOT NULL,
	[NM_VESSEL] [nvarchar](50) NULL,
	[NO_PO_PARTNER] [nvarchar](50) NULL,
	[DT_SO] [nvarchar](8) NULL,
	[NO_SO] [nvarchar](20) NULL,
	[DT_IN] [nvarchar](8) NULL,
	[WEIGHT] [nvarchar](10) NULL,
	[DC_RMK] [nvarchar](50) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


