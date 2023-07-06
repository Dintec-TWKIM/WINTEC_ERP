USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MA_CERT_WINTEC]    Script Date: 2019-12-02 오후 7:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MA_CERT_WINTEC](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[SEQ] [numeric](5, 0) NOT NULL,
	[NO_DOC] [nvarchar](50) NULL,
	[CD_SUPPLIER] [nvarchar](20) NULL,
	[DC_TYPE] [nvarchar](50) NULL,
	[CD_ITEM] [nvarchar](20) NULL,
	[NO_DRAWING] [nvarchar](100) NULL,
	[DC_MAT] [nvarchar](50) NULL,
	[DC_SIZE] [nvarchar](50) NULL,
	[TP_UNIT] [nvarchar](4) NULL,
	[QT_IN] [numeric](10, 0) NULL,
	[NO_HEAT] [nvarchar](50) NULL,
	[NO_LOT] [nvarchar](50) NULL,
	[NM_FILE] [nvarchar](200) NULL,
	[DC_RMK] [nvarchar](max) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](10) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_MA_CERT_WINTEC] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PLANT] ASC,
	[SEQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


