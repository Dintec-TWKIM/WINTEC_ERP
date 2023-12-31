USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_HSD_DATA_LOG]    Script Date: 2022-04-04 ���� 5:33:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_HSD_DATA_LOG](
	[NO_HSD] [int] NOT NULL,
	[NO_ORDER] [nvarchar](100) NOT NULL,
	[NO_PJT] [nvarchar](255) NULL,
	[DT_SO] [nvarchar](255) NULL,
	[NM_ITEM] [nvarchar](255) NULL,
	[QT_SO] [int] NULL,
	[NM_PARTNER] [nvarchar](255) NULL,
	[NM_VESSEL] [nvarchar](255) NULL,
	[TP_ENGINE] [nvarchar](255) NULL,
	[NO_PO_PARTNER] [nvarchar](255) NULL,
	[YN_LOSS] [nvarchar](1) NULL,
	[DC_RMK] [nvarchar](255) NULL,
 CONSTRAINT [PK_CZ_SA_HSD_DATA_LOG] PRIMARY KEY CLUSTERED 
(
	[NO_HSD] ASC,
	[NO_ORDER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


