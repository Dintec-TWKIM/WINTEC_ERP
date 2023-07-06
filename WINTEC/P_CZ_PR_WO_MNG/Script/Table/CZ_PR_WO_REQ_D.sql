USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PR_WO_REQ_D]    Script Date: 2022-12-27 오후 6:15:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PR_WO_REQ_D](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_WO] [nvarchar](20) NOT NULL,
	[SEQ_WO] [numeric](5, 0) NOT NULL,
	[CD_ITEM] [nvarchar](20) NULL,
	[DT_NO_ID] [nvarchar](8) NULL,
	[NO_YEAR] [nvarchar](1) NULL,
	[NO_MONTH] [nvarchar](1) NULL,
	[NO_SEQ] [numeric](18, 0) NULL,
	[NO_ID] [nvarchar](10) NULL,
	[NO_ID_OLD] [nvarchar](10) NULL,
	[NO_HEAT] [nvarchar](200) NULL,
	[CD_QR] [nvarchar](100) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[NO_PREFIX] [nvarchar](1) NULL,
	[DTS_COUNTING] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_PR_WO_REQ_D] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_WO] ASC,
	[SEQ_WO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


