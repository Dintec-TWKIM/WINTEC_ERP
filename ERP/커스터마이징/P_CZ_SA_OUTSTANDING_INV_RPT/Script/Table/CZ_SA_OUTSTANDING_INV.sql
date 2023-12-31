USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_OUTSTANDING_INV]    Script Date: 2017-12-07 오전 11:00:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_OUTSTANDING_INV](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[DT_MONTH] [nvarchar](6) NOT NULL,
	[AM_IV] [numeric](18, 4) NULL,
	[AM_RCP] [numeric](18, 4) NULL,
	[AM_REMAIN] [numeric](18, 4) NULL,
	[QT_REMAIN] [int] NULL,
	[AM_IV_MONTH] [numeric](18, 4) NULL,
	[QT_IV_MONTH] [int] NULL,
	[AM_RCP_MONTH] [numeric](18, 4) NULL,
	[QT_RCP_MONTH] [int] NULL,
	[DT_RETURN] [numeric](18, 4) NULL,
	[DT_REMAIN] [numeric](18, 4) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_OUTSTANDING_INV] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PARTNER] ASC,
	[DT_MONTH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


