USE [NEOE]
GO

/****** Object:  Table [NEOE].[PU_ADPAYMENT]    Script Date: 2016-10-25 오전 11:01:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[PU_ADPAYMENT](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[NO_BIZAREA] [nvarchar](7) NOT NULL,
	[NO_ADPAY] [nvarchar](20) NOT NULL,
	[NO_ADPAYLINE] [numeric](5, 0) NOT NULL,
	[DT_ADPAY] [nchar](8) NULL,
	[NO_PO] [nvarchar](20) NOT NULL,
	[NO_POLINE] [numeric](5, 0) NOT NULL,
	[CD_EXCH] [nvarchar](3) NOT NULL,
	[RT_EXCH] [numeric](11, 4) NOT NULL,
	[AM] [numeric](17, 4) NOT NULL,
	[AM_EX] [numeric](17, 4) NOT NULL,
	[CD_DOCU] [nvarchar](3) NULL,
	[QT_ADPAY_MM] [numeric](17, 4) NOT NULL,
	[CD_DEPT] [nvarchar](12) NOT NULL,
	[YN_JEONJA] [nvarchar](1) NULL,
	[TP_AIS] [nvarchar](1) NULL,
	[DT_PAY_SCHEDULE] [nchar](8) NULL,
	[PO_CONDITION] [nvarchar](4) NULL,
	[DT_ACCT] [nchar](8) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
 CONSTRAINT [PK_PU_ADPAYMENT] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_ADPAY] ASC,
	[NO_ADPAYLINE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


