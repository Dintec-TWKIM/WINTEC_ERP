USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PU_ETAX_DETAILH]    Script Date: 2022-05-06 오전 11:17:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PU_ETAX_DETAILH](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[DT_MONTH] [nvarchar](6) NOT NULL,
	[IDX] [int] NOT NULL,
	[AM] [numeric](17, 4) NULL,
	[MISS_ROW] [nvarchar](500) NULL,
	[DC_LOG] [nvarchar](500) NULL,
	[NM_FILE] [nvarchar](500) NULL,
	[YN_CONFIRM] [nvarchar](1) NULL,
	[DC_RMK] [nvarchar](500) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_PU_ETAX_DETAILH] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PARTNER] ASC,
	[DT_MONTH] ASC,
	[IDX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


