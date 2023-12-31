USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MM_QTIO_OLD]    Script Date: 2015-12-24 오후 7:58:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MM_QTIO_OLD](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[DT_IO] [nvarchar](8) NOT NULL,
	[NO_LINE] [nvarchar](3) NOT NULL,
	[FG_TRANS] [nvarchar](3) NULL,
	[CD_QTIOTP] [nvarchar](3) NULL,
	[QT_IO] [numeric](17, 4) NULL,
	[UM] [numeric](15, 4) NULL,
	[CD_PJT] [nvarchar](20) NULL,
	[CD_PARTNER] [nvarchar](20) NULL,
	[DT_PO] [nvarchar](8) NULL,
	[FG_PS] [nvarchar](1) NULL,
 CONSTRAINT [PK_CZ_MM_QTIO_OLD] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PLANT] ASC,
	[CD_ITEM] ASC,
	[DT_IO] ASC,
	[NO_LINE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


