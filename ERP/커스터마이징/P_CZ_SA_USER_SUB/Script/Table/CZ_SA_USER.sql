USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_USER]    Script Date: 2020-07-13 오후 2:18:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_USER](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[ID_USER] [nvarchar](15) NOT NULL,
	[TP_SALES] [nvarchar](max) NULL,
	[TP_SALES_DEF] [nvarchar](2) NULL,
	[TP_BIZ] [nvarchar](2) NULL,
	[ID_SALES] [nvarchar](max) NULL,
	[ID_SALES_DEF] [nvarchar](15) NULL,
	[ID_TYPIST] [nvarchar](max) NULL,
	[ID_TYPIST_DEF] [nvarchar](15) NULL,
	[ID_TYPIST_AUTO] [nvarchar](max) NULL,
	[ID_TYPIST_MAPS] [nvarchar](max) NULL,
	[ID_PUR] [nvarchar](max) NULL,
	[ID_PUR_DEF] [nvarchar](15) NULL,
	[ID_LOG] [nvarchar](max) NULL,
	[ID_LOG_DEF] [nvarchar](15) NULL,
	[ID_FIRST_APPROVAL] [nvarchar](15) NULL,
	[ID_SECOND_APPROVAL] [nvarchar](15) NULL,
	[ID_EXPECT] [nvarchar](15) NULL,
	[DTS_EXPECT] [nvarchar](14) NULL,
	[YN_EXPECT] [nvarchar](1) NULL,
	[CD_PARTNER_GRP] [nvarchar](10) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_USER] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[ID_USER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

