USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_TR_INVL_LOG]    Script Date: 2017-01-13 오후 1:29:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_TR_INVL_LOG](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_INV] [nvarchar](20) NOT NULL,
	[NO_HST] [numeric](5, 0) NOT NULL,
	[NO_LINE] [numeric](5, 0) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[DT_DELIVERY] [nchar](8) NOT NULL,
	[QT_INVENT] [numeric](17, 4) NOT NULL,
	[UM_INVENT] [numeric](15, 4) NOT NULL,
	[AM_INVENT] [numeric](17, 4) NOT NULL,
	[QT_SO] [numeric](17, 4) NULL,
	[UM_SO] [numeric](15, 4) NOT NULL,
	[AM_EXSO] [numeric](17, 4) NOT NULL,
	[NO_PO] [nvarchar](20) NULL,
	[NO_LINE_PO] [numeric](5, 0) NULL,
	[YN_PRINT] [nvarchar](1) NOT NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_DELETE] [nvarchar](14) NULL,
	[ID_DELETE] [nvarchar](15) NULL,
 CONSTRAINT [PK_CZ_TR_INVL_LOG] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_INV] ASC,
	[NO_HST] ASC,
	[NO_LINE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


