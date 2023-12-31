USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_GIR_AUTO_MAIL_SETTING]    Script Date: 2022-05-03 오후 4:21:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_GIR_AUTO_MAIL_SETTING](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_GIR] [nvarchar](20) NOT NULL,
	[TP_SEND] [nvarchar](3) NULL,
	[TP_TRANS] [nvarchar](4) NULL,
	[YN_DHL] [nvarchar](1) NULL,
	[YN_FEDEX] [nvarchar](1) NULL,
	[YN_TPO] [nvarchar](1) NULL,
	[YN_SK] [nvarchar](1) NULL,
	[YN_SR] [nvarchar](1) NULL,
	[YN_ETC] [nvarchar](1) NULL,
	[CD_PARTNER_ETC] [nvarchar](20) NULL,
	[DC_EMAIL_ETC] [nvarchar](100) NULL,
	[CD_COUNTRY_DHL] [nvarchar](4) NULL,
	[CD_COUNTRY_FEDEX] [nvarchar](4) NULL,
	[DC_COUNTRY_ETC] [nvarchar](100) NULL,
	[DC_CONSIGNEE] [nvarchar](1000) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_GIR_AUTO_MAIL_SETTING] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_GIR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


