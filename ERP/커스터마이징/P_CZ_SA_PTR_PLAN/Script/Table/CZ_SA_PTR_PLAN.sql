USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_PTR_PLAN]    Script Date: 2015-10-29 오전 9:53:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_PTR_PLAN](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[YY_PLAN] [nvarchar](4) NOT NULL,
	[TP_PLAN] [nvarchar](1) NOT NULL,
	[CD_KEY] [nvarchar](20) NOT NULL,
	[AM_TOTWON] [numeric](17, 4) NULL,
	[AM_TOT_JAN] [numeric](17, 4) NULL,
	[AM_TOT_FEB] [numeric](17, 4) NULL,
	[AM_TOT_MAR] [numeric](17, 4) NULL,
	[AM_TOT_APR] [numeric](17, 4) NULL,
	[AM_TOT_MAY] [numeric](17, 4) NULL,
	[AM_TOT_JUN] [numeric](17, 4) NULL,
	[AM_TOT_JUL] [numeric](17, 4) NULL,
	[AM_TOT_AUG] [numeric](17, 4) NULL,
	[AM_TOT_SEP] [numeric](17, 4) NULL,
	[AM_TOT_OCT] [numeric](17, 4) NULL,
	[AM_TOT_NOV] [numeric](17, 4) NULL,
	[AM_TOT_DEC] [numeric](17, 4) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
 CONSTRAINT [PK_CZ_SA_PTR_PLAN] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[YY_PLAN] ASC,
	[TP_PLAN] ASC,
	[CD_KEY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_CZ_SA_PTR_PLAN_AM_TOTWON]  DEFAULT ((0)) FOR [AM_TOTWON]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_CJAN]  DEFAULT ((0)) FOR [AM_TOT_JAN]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN1]  DEFAULT ((0)) FOR [AM_TOT_FEB]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN2]  DEFAULT ((0)) FOR [AM_TOT_MAR]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN3]  DEFAULT ((0)) FOR [AM_TOT_APR]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN4]  DEFAULT ((0)) FOR [AM_TOT_MAY]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN5]  DEFAULT ((0)) FOR [AM_TOT_JUN]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN6]  DEFAULT ((0)) FOR [AM_TOT_JUL]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN7]  DEFAULT ((0)) FOR [AM_TOT_AUG]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN8]  DEFAULT ((0)) FOR [AM_TOT_SEP]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN9]  DEFAULT ((0)) FOR [AM_TOT_OCT]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN10]  DEFAULT ((0)) FOR [AM_TOT_NOV]
GO

ALTER TABLE [NEOE].[CZ_SA_PTR_PLAN] ADD  CONSTRAINT [DF_Table_1_AM_TOT_JAN11]  DEFAULT ((0)) FOR [AM_TOT_DEC]
GO

