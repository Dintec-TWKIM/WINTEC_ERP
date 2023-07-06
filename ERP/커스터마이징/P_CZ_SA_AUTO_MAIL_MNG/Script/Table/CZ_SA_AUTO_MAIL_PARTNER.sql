USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_AUTO_MAIL_PARTNER]    Script Date: 2023-01-10 오후 3:17:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_AUTO_MAIL_PARTNER](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[TP_PARTNER] [nvarchar](3) NOT NULL,
	[QT_LT_OVERDUE] [int] NULL,
	[TP_PERIOD] [nvarchar](3) NULL,
	[YN_MON] [nvarchar](1) NULL,
	[YN_TUE] [nvarchar](1) NULL,
	[YN_WED] [nvarchar](1) NULL,
	[YN_THU] [nvarchar](1) NULL,
	[YN_FRI] [nvarchar](1) NULL,
	[TP_SEND] [nvarchar](3) NULL,
	[NO_EMP_SEND] [nvarchar](10) NULL,
	[YN_READY_INFO] [nvarchar](1) NULL,
	[YN_ORDER_STAT] [nvarchar](1) NULL,
	[QT_WEEK] [int] NULL,
	[TP_DOW_RI] [nvarchar](3) NULL,
	[YN_MON_WO] [nvarchar](1) NULL,
	[YN_TUE_WO] [nvarchar](1) NULL,
	[YN_WED_WO] [nvarchar](1) NULL,
	[YN_THU_WO] [nvarchar](1) NULL,
	[YN_FRI_WO] [nvarchar](1) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](10) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_AUTO_MAIL_PARTNER] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PARTNER] ASC,
	[TP_PARTNER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


