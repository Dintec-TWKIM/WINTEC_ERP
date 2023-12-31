USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_GIRH_CHARGE]    Script Date: 2021-01-13 오전 11:24:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_GIRH_CHARGE](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_GIR] [nvarchar](20) NOT NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[AM_EX_CHARGE] [numeric](17, 4) NULL,
	[DC_RMK] [nvarchar](max) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_GIRH_CHARGE] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_GIR] ASC,
	[CD_ITEM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


