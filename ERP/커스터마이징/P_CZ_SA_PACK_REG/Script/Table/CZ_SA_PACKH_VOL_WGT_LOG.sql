USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_PACKH_VOL_WGT_LOG]    Script Date: 2023-03-28 오전 9:42:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_PACKH_VOL_WGT_LOG](
	[IDX] [int] IDENTITY(1,1) NOT NULL,
	[CD_COMPANY] [nvarchar](7) NULL,
	[NO_GIR] [nvarchar](20) NULL,
	[CD_PACK_TYPE] [nvarchar](4) NULL,
	[QT_WIDTH] [numeric](18, 4) NULL,
	[QT_LENGTH] [numeric](18, 4) NULL,
	[QT_HEIGHT] [numeric](18, 4) NULL,
	[QT_GROSS_WEIGHT] [numeric](18, 4) NULL,
	[QT_VOLUME_WEIGHT] [numeric](18, 4) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_PACKH_VOL_WGT_LOG] PRIMARY KEY CLUSTERED 
(
	[IDX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


