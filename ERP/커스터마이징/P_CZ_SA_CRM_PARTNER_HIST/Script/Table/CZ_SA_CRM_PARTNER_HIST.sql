USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_CRM_PARTNER_HIST]    Script Date: 2023-04-18 오후 4:03:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_CRM_PARTNER_HIST](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[SEQ] [int] NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[DT_HIST] [nvarchar](8) NULL,
	[NO_EMP_SALES] [nvarchar](20) NULL,
	[NO_EMP_SALES1] [nvarchar](20) NULL,
	[NO_EMP_SALES2] [nvarchar](20) NULL,
	[NO_EMP_SALES3] [nvarchar](20) NULL,
	[NO_EMP_LOG] [nvarchar](20) NULL,
	[DC_ADDRESS1] [nvarchar](500) NULL,
	[DC_ADDRESS2] [nvarchar](500) NULL,
	[DC_ADDRESS3] [nvarchar](500) NULL,
	[DC_POTAL_ADDR] [nvarchar](200) NULL,
	[DC_POTAL_ID] [nvarchar](50) NULL,
	[DC_POTAL_PW] [nvarchar](50) NULL,
	[TP_ACK_SEND] [nvarchar](4) NULL,
	[YN_DUEDATE] [nvarchar](1) NULL,
	[DC_MARGIN_ENGINE] [nvarchar](300) NULL,
	[DC_MARGIN_SUPPLIER] [nvarchar](300) NULL,
	[DC_COMPETE] [nvarchar](300) NULL,
	[DC_INQ] [nvarchar](500) NULL,
	[DC_QTN] [nvarchar](500) NULL,
	[DC_QTN_SEND] [nvarchar](500) NULL,
	[DC_ACK] [nvarchar](500) NULL,
	[DC_DELIVERY] [nvarchar](1000) NULL,
	[DC_INVOICE] [nvarchar](500) NULL,
	[DC_OUTSTANDING] [nvarchar](500) NULL,
	[DC_RMK] [nvarchar](500) NULL,
	[DC_GS] [nvarchar](500) NULL,
	[DC_PV] [nvarchar](500) NULL,
	[DC_NB] [nvarchar](500) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_CRM_PARTNER_HIST_1] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[SEQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


