USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_CRM_PARTNER_MEMO]    Script Date: 2017-11-14 오전 8:58:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_CRM_PARTNER_MEMO](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[NO_INDEX] [numeric](18, 0) NOT NULL,
	[DC_COMMENT] [nvarchar](max) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_MA_PARTNER_COMMENT] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PARTNER] ASC,
	[NO_INDEX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


