USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_UNIT_PRICE_WARNING]    Script Date: 2021-07-23 오전 11:20:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_UNIT_PRICE_WARNING](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[DT_SEND] [nvarchar](8) NOT NULL,
	[CD_SPEC] [nvarchar](20) NOT NULL,
	[DT_QTN] [nvarchar](8) NULL,
	[NO_FILE] [nvarchar](20) NULL,
	[NO_LINE] [int] NULL,
	[CD_ITEM] [nvarchar](20) NULL,
	[NM_MODEL] [nvarchar](100) NULL,
	[STAND_PRC] [numeric](15, 4) NULL,
	[UM_KR] [numeric](15, 4) NULL,
	[GEOMEAN] [numeric](15, 4) NULL,
	[SD] [int] NULL,
	[GEOMEAN1] [int] NULL,
	[GEOMEAN2] [int] NULL,
	[TP_PLUS] [nvarchar](1) NULL,
	[QT_INV] [numeric](17, 4) NULL,
	[QT_SO] [numeric](17, 4) NULL,
	[DT_STOCK_MONTH] [nvarchar](100) NULL,
	[YN_STOCK_PO] [nvarchar](1) NULL,
	[CHK_UCODE] [nvarchar](1) NULL,
	[CHK_ITEM] [nvarchar](1) NULL,
	[ID_INSERT] [nvarchar](14) NULL,
	[DTS_INSERT] [nvarchar](15) NULL,
	[ID_UPDATE] [nvarchar](14) NULL,
	[DTS_UPDATE] [nvarchar](15) NULL,
 CONSTRAINT [PK_CZ_SA_UNIT_PRICE_WARNING] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[DT_SEND] ASC,
	[CD_SPEC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


