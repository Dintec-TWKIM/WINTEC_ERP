USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MM_QTIO_DETAIL]    Script Date: 2016-02-19 오후 7:14:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MM_QTIO_DETAIL](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_IO] [nvarchar](20) NOT NULL,
	[NO_IOLINE] [numeric](5, 0) NOT NULL,
	[CD_PLANT] [nvarchar](7) NULL,
	[CD_ITEM] [nvarchar](20) NULL,
	[DT_IO] [nvarchar](8) NULL,
	[FG_PS] [nvarchar](1) NULL,
	[QT_IO] [numeric](17, 4) NULL,
	[UM] [numeric](15, 4) NULL,
	[QT_CURR] [numeric](17, 4) NULL,
	[UM_STOCK] [numeric](15, 4) NULL,
	[CD_PJT] [nvarchar](20) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_MM_QTIO] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_IO] ASC,
	[NO_IOLINE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


