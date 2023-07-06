USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_FI_IMP_PMTL]    Script Date: 2021-11-23 오후 5:29:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_FI_IMP_PMTL](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_IMPORT] [nvarchar](14) NOT NULL,
	[NO_PO] [nvarchar](20) NOT NULL,
	[NO_POLINE] [numeric](5, 0) NOT NULL,
	[NO_SO] [nvarchar](20) NULL,
	[NO_SOLINE] [numeric](5, 0) NULL,
	[CD_PLANT] [nvarchar](7) NULL,
	[CD_ITEM] [nvarchar](20) NULL,
	[NO_RAN] [nvarchar](20) NULL,
	[NO_DSP] [int] NULL,
	[MODEL] [nvarchar](100) NULL,
	[QT_PO] [numeric](17, 4) NULL,
	[QT_TAX] [numeric](17, 4) NULL,
	[QT_RETURN] [numeric](17, 4) NULL,
	[UM_EX] [numeric](15, 4) NULL,
	[AM_EX] [numeric](17, 4) NULL,
	[AM] [numeric](17, 4) NULL,
	[AM_TAX] [numeric](17, 4) NULL,
	[AM_RETURN] [numeric](17, 4) NULL,
	[NO_RETURN] [nvarchar](20) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_FI_IMP_PMTL] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_IMPORT] ASC,
	[NO_PO] ASC,
	[NO_POLINE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


