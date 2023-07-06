USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_TR_INVH_LOG]    Script Date: 2020-12-22 오전 11:15:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_TR_INVH_LOG](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_INV] [nvarchar](20) NOT NULL,
	[NO_HST] [numeric](5, 0) NOT NULL,
	[DT_BALLOT] [nchar](8) NOT NULL,
	[CD_BIZAREA] [nvarchar](7) NULL,
	[CD_SALEGRP] [nvarchar](7) NOT NULL,
	[NO_EMP] [nvarchar](10) NULL,
	[FG_LC] [nchar](3) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[CD_EXCH] [nvarchar](3) NULL,
	[AM_EX] [numeric](17, 4) NOT NULL,
	[DT_LOADING] [nchar](8) NOT NULL,
	[CD_ORIGIN] [nvarchar](3) NULL,
	[CD_EXPORT] [nvarchar](7) NULL,
	[CD_PRODUCT] [nvarchar](7) NULL,
	[TP_TRANSPORT] [nvarchar](3) NULL,
	[TP_TRANS] [nvarchar](3) NULL,
	[PORT_LOADING] [nvarchar](50) NULL,
	[PORT_ARRIVER] [nvarchar](50) NULL,
	[CD_NOTIFY] [nvarchar](20) NULL,
	[DT_TO] [nchar](8) NULL,
	[REMARK1] [nvarchar](100) NULL,
	[REMARK2] [nvarchar](100) NULL,
	[REMARK3] [nvarchar](100) NULL,
	[REMARK4] [nvarchar](100) NULL,
	[REMARK5] [nvarchar](100) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[NO_TO] [nvarchar](20) NULL,
	[NO_BL] [nvarchar](20) NULL,
	[NM_NOTIFY] [nvarchar](100) NULL,
	[ADDR1_NOTIFY] [nvarchar](400) NULL,
	[ADDR2_NOTIFY] [nvarchar](400) NULL,
	[CD_CONSIGNEE] [nvarchar](50) NULL,
	[NM_CONSIGNEE] [nvarchar](100) NULL,
	[ADDR1_CONSIGNEE] [nvarchar](400) NULL,
	[ADDR2_CONSIGNEE] [nvarchar](400) NULL,
	[REMARK] [text] NULL,
	[NM_PARTNER] [nvarchar](100) NULL,
	[ADDR1_PARTNER] [nvarchar](400) NULL,
	[ADDR2_PARTNER] [nvarchar](400) NULL,
	[NM_EXPORT] [nvarchar](100) NULL,
	[ADDR1_EXPORT] [nvarchar](400) NULL,
	[ADDR2_EXPORT] [nvarchar](400) NULL,
	[COND_PRICE] [nvarchar](3) NULL,
	[DESCRIPTION] [nvarchar](150) NULL,
	[DT_SAILING_ON] [nvarchar](8) NULL,
	[YN_RETURN] [nvarchar](1) NOT NULL,
	[COND_PAY] [nvarchar](6) NULL,
	[ARRIVER_COUNTRY] [nvarchar](3) NULL,
	[YN_INSURANCE] [nvarchar](1) NULL,
	[DTS_DELETE] [nvarchar](14) NULL,
	[ID_DELETE] [nvarchar](15) NULL,
 CONSTRAINT [PK_CZ_TR_INVH_LOG_1] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_INV] ASC,
	[NO_HST] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


