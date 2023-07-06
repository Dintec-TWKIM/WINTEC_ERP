USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_CLAIML]    Script Date: 2017-11-08 오후 3:37:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_CLAIML](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_CLAIM] [nvarchar](8) NOT NULL,
	[NO_SO] [nvarchar](20) NOT NULL,
	[SEQ_SO] [numeric](5, 0) NOT NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[QT_SO] [numeric](17, 4) NULL,
	[QT_CLAIM] [numeric](17, 4) NOT NULL,
	[CD_SUPPLIER] [nvarchar](20) NULL,
	[NO_DSP] [numeric](7, 2) NULL,
	[NM_SUBJECT] [nvarchar](1000) NULL,
	[CD_ITEM_PARTNER] [nvarchar](30) NULL,
	[NM_ITEM_PARTNER] [nvarchar](1000) NULL,
	[AM_CLAIM] [numeric](17, 4) NOT NULL,
	[UM_SO] [numeric](15, 4) NULL,
	[AM_SO] [numeric](17, 4) NULL,
	[UM_PO] [numeric](15, 4) NULL,
	[AM_PO] [numeric](17, 4) NULL,
	[UM_STOCK] [numeric](15, 4) NULL,
	[AM_STOCK] [numeric](17, 4) NULL,
	[LT] [int] NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_CLAIML_1] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_CLAIM] ASC,
	[NO_SO] ASC,
	[SEQ_SO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [NEOE].[CZ_SA_CLAIML] ADD  CONSTRAINT [DF_CZ_SA_CLAIML_QT_SO]  DEFAULT ((0)) FOR [QT_SO]
GO


