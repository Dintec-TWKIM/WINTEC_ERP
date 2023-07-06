USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_CLAIMH]    Script Date: 2023-05-02 오전 9:28:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_CLAIMH](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_CLAIM] [nvarchar](8) NOT NULL,
	[NO_SO] [nvarchar](20) NOT NULL,
	[CD_STATUS] [nvarchar](1) NOT NULL,
	[NO_IMO] [nvarchar](10) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[NO_SALES_EMP] [nvarchar](10) NOT NULL,
	[TP_CLAIM] [nvarchar](3) NOT NULL,
	[TP_CAUSE] [nvarchar](3) NOT NULL,
	[TP_ITEM] [nvarchar](3) NOT NULL,
	[AM_ITEM_RCV] [numeric](14, 2) NULL,
	[AM_ADD_RCV] [numeric](14, 2) NULL,
	[AM_ITEM_PRO] [numeric](14, 2) NULL,
	[AM_ADD_PRO] [numeric](14, 2) NULL,
	[AM_ITEM_CLS] [numeric](14, 2) NULL,
	[AM_ADD_CLS] [numeric](14, 2) NULL,
	[DC_PROGRESS] [nvarchar](max) NULL,
	[DC_RESULT] [nvarchar](max) NULL,
	[DC_PREVENTION] [nvarchar](max) NULL,
	[DC_CLOSING] [nvarchar](max) NULL,
	[NO_EMP] [nvarchar](10) NOT NULL,
	[DT_INPUT] [nvarchar](8) NOT NULL,
	[DT_EXPECTED_CLOSING_PRO] [nvarchar](8) NULL,
	[DT_EXPECTED_CLOSING] [nvarchar](8) NULL,
	[DT_CLOSING] [nvarchar](8) NULL,
	[CD_SUPPLIER_REWARD] [nvarchar](3) NULL,
	[DC_SUPPLIER_REWARD] [nvarchar](max) NULL,
	[DC_CREDIT_NOTE] [nvarchar](max) NULL,
	[CD_CREDIT_EXCH] [nvarchar](4) NULL,
	[AM_CREDIT] [numeric](14, 2) NULL,
	[TP_REASON] [nvarchar](4) NULL,
	[TP_RETURN] [nvarchar](4) NULL,
	[TP_REQUEST] [nvarchar](4) NULL,
	[DC_RECEIVE] [nvarchar](max) NULL,
	[IMAGE1] [nvarchar](100) NULL,
	[IMAGE2] [nvarchar](100) NULL,
	[IMAGE3] [nvarchar](100) NULL,
	[IMAGE4] [nvarchar](100) NULL,
	[IMAGE5] [nvarchar](100) NULL,
	[IMAGE6] [nvarchar](100) NULL,
	[DC_IMAGE1] [nvarchar](100) NULL,
	[DC_IMAGE2] [nvarchar](100) NULL,
	[DC_IMAGE3] [nvarchar](100) NULL,
	[DC_IMAGE4] [nvarchar](100) NULL,
	[DC_IMAGE5] [nvarchar](100) NULL,
	[DC_IMAGE6] [nvarchar](100) NULL,
	[DC_RMK] [nvarchar](max) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[DC_PACK_SIZE] [nvarchar](100) NULL,
	[QT_PACK_WEIGHT] [numeric](14, 2) NULL,
 CONSTRAINT [PK_CZ_AS_CLAIM_H] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_CLAIM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


