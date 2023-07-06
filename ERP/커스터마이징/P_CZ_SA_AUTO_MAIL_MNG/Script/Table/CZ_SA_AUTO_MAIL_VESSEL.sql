USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_AUTO_MAIL_VESSEL]    Script Date: 2023-01-19 ���� 9:53:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_AUTO_MAIL_VESSEL](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[NO_IMO] [nvarchar](10) NOT NULL,
	[SEQ] [numeric](5, 0) NOT NULL,
	[YN_SO] [nvarchar](1) NULL,
	[YN_PACK] [nvarchar](1) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_AUTO_MAIL_VESSEL] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PARTNER] ASC,
	[NO_IMO] ASC,
	[SEQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


