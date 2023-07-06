USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MM_MONTHLY_STOCK]    Script Date: 2019-11-05 오후 4:46:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MM_MONTHLY_STOCK](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[YMM] [nvarchar](6) NOT NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[STAND_PRC] [numeric](15, 4) NULL,
	[QT_INV] [numeric](15, 4) NULL,
	[QT_AVL] [numeric](15, 4) NULL,
	[QT_SO] [numeric](15, 4) NULL,
	[QT_HOLD] [numeric](15, 4) NULL,
	[QT_STOCK_MONTH] [numeric](15, 4) NULL,
	[QT_BOOK_MONTH] [numeric](15, 4) NULL,
	[QT_PO] [numeric](15, 4) NULL,
	[QT_PO_MONTH] [numeric](15, 4) NULL,
	[QT_GR] [numeric](15, 4) NULL,
	[QT_GI] [numeric](15, 4) NULL,
	[AM_INV] [numeric](17, 4) NULL,
	[AM_AVL] [numeric](17, 4) NULL,
	[AM_SO] [numeric](17, 4) NULL,
	[AM_HOLD] [numeric](17, 4) NULL,
	[AM_STOCK_MONTH] [numeric](17, 4) NULL,
	[AM_BOOK_MONTH] [numeric](17, 4) NULL,
	[AM_PO] [numeric](17, 4) NULL,
	[AM_PO_MONTH] [numeric](17, 4) NULL,
	[AM_GR] [numeric](17, 4) NULL,
	[AM_GI] [numeric](17, 4) NULL,
	[QT_BOOK_3MONTH] [numeric](15, 4) NULL,
	[QT_BOOK_6MONTH] [numeric](15, 4) NULL,
	[QT_BOOK_12MONTH] [numeric](15, 4) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_MM_MONTHLY_STOCK] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_ITEM] ASC,
	[YMM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


