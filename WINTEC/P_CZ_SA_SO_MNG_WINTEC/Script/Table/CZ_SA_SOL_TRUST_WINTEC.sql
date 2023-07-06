USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_SOL_TRUST_WINTEC]    Script Date: 2022-09-08 ���� 4:00:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_SOL_TRUST_WINTEC](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_SO] [nvarchar](20) NOT NULL,
	[SEQ_SO] [numeric](5, 0) NOT NULL,
	[IDX] [int] NOT NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[NO_TRUST] [nvarchar](15) NULL,
	[NO_ID] [nvarchar](10) NULL,
	[YN_TRUST] [nvarchar](1) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](10) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_SOL_TRUST_WINTEC] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_SO] ASC,
	[SEQ_SO] ASC,
	[IDX] ASC,
	[CD_ITEM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

