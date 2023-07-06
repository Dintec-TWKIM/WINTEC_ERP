USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_GIRH_LOG]    Script Date: 2017-01-11 오후 2:16:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_GIRH_LOG](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_GIR] [nvarchar](20) NOT NULL,
	[TP_GIR] [nvarchar](4) NOT NULL,
	[NO_HST] [numeric](5, 0) NOT NULL,
	[DT_GIR] [nvarchar](8) NULL,
	[NO_EMP] [nvarchar](10) NULL,
	[TP_BUSI] [nvarchar](3) NULL,
	[CD_PLANT] [nvarchar](7) NULL,
	[CD_PARTNER] [nvarchar](20) NULL,
	[GI_PARTNER] [nvarchar](20) NULL,
	[STA_GIR] [nvarchar](3) NULL,
	[YN_RETURN] [nchar](1) NULL,
	[DC_RMK] [nvarchar](100) NULL,
	[TP_GI] [nvarchar](6) NULL,
	[FG_UM] [nvarchar](3) NULL,
	[DC_RMK1] [nvarchar](100) NULL,
	[DC_RMK2] [nvarchar](100) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_DELETE] [nvarchar](15) NULL,
	[ID_DELETE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_GIRH_LOG_1] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_GIR] ASC,
	[TP_GIR] ASC,
	[NO_HST] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [NEOE].[CZ_SA_GIRH_LOG] ADD  CONSTRAINT [DF_CZ_SA_GIRH_LOG_SEQ_GIR1]  DEFAULT ((0)) FOR [NO_HST]
GO


