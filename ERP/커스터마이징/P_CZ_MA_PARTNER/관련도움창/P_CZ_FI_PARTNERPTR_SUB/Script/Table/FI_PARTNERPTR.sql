USE [NEOE]
GO

/****** Object:  Table [NEOE].[FI_PARTNERPTR]    Script Date: 2022-12-27 오전 11:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[FI_PARTNERPTR](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[SEQ] [numeric](5, 0) NOT NULL,
	[NM_PTR] [nvarchar](30) NULL,
	[NM_EMAIL] [nvarchar](80) NOT NULL,
	[NO_HP] [nvarchar](20) NULL,
	[NO_TEL] [nvarchar](30) NULL,
	[CLIENT_NOTE] [nvarchar](100) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[USE_YN] [nvarchar](1) NULL,
	[NO_FAX] [nvarchar](20) NULL,
	[NM_DEPT] [nvarchar](100) NULL,
	[NM_DUTY_RESP] [nvarchar](100) NULL,
	[EN_DUTY_RESP] [nvarchar](100) NULL,
	[EN_PTR] [nvarchar](100) NULL,
	[TP_PTR] [nvarchar](3) NULL,
	[DC_PHOTO] [nvarchar](50) NULL,
	[YN_TYPE] [nvarchar](1) NULL,
	[YN_OUTSTANDING_INV] [nvarchar](1) NULL,
	[NO_WEHAGO_KEY] [nvarchar](100) NULL,
	[YN_LIMIT] [nvarchar](1) NULL,
	[YN_SO] [nvarchar](1) NULL,
	[NO_MOBILE] [nvarchar](100) NULL,
 CONSTRAINT [PK_FI_PARTNERPTR] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PARTNER] ASC,
	[SEQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


