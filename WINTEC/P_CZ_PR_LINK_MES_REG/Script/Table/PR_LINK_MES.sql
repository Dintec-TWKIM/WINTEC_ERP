USE [NEOE]
GO

/****** Object:  Table [NEOE].[PR_LINK_MES]    Script Date: 2021-03-09 오후 4:34:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[PR_LINK_MES](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[NO_MES] [nvarchar](20) NOT NULL,
	[YN_END] [nvarchar](1) NULL,
	[CD_ITEM] [nvarchar](50) NOT NULL,
	[DT_WORK] [nvarchar](8) NULL,
	[NO_EMP] [nvarchar](10) NOT NULL,
	[CD_WC] [nvarchar](7) NULL,
	[CD_OP] [nvarchar](4) NULL,
	[CD_WCOP] [nvarchar](4) NULL,
	[YN_WO] [nvarchar](1) NULL,
	[NO_WO] [nvarchar](20) NULL,
	[YN_WORK] [nvarchar](1) NULL,
	[NO_WORK] [nvarchar](20) NULL,
	[QT_WORK] [numeric](17, 4) NOT NULL,
	[QT_REJECT] [numeric](17, 4) NOT NULL,
	[QT_MOVE] [numeric](17, 4) NOT NULL,
	[QT_BAD] [numeric](17, 4) NULL,
	[CD_SL_IN] [nvarchar](7) NULL,
	[CD_SL_BAD_IN] [nvarchar](7) NULL,
	[YN_REWORK] [nvarchar](1) NOT NULL,
	[YN_MANDAY] [nvarchar](1) NULL,
	[TM_WO_T] [numeric](17, 4) NULL,
	[QT_WO_ROLL] [numeric](17, 4) NULL,
	[TM_EQ_T] [numeric](17, 4) NULL,
	[NO_REQ] [nvarchar](20) NULL,
	[NO_IO_BILL] [nvarchar](20) NULL,
	[NO_LOT] [nvarchar](100) NULL,
	[NO_SFT] [nvarchar](3) NULL,
	[CD_EQUIP] [nvarchar](30) NULL,
	[CD_REJECT] [nvarchar](4) NULL,
	[CD_RESOURCE] [nvarchar](4) NULL,
	[YN_ERR] [nvarchar](1) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[CD_ITEM_PARTNER] [nvarchar](20) NULL,
	[DC_RMK1] [nvarchar](100) NULL,
	[DC_RMK2] [nvarchar](100) NULL,
	[YN_UPDATE] [nvarchar](1) NULL,
	[NO_MES_MGMT] [nvarchar](20) NULL,
	[NO_WORK_BAD] [nvarchar](20) NULL,
	[NO_PJT] [nvarchar](20) NULL,
	[SEQ_PROJECT] [numeric](5, 0) NULL,
	[NO_SO] [nvarchar](20) NULL,
	[NO_LINE_SO] [numeric](5, 0) NULL,
	[CD_USERDEF1] [nvarchar](20) NULL,
	[CD_USERDEF2] [nvarchar](4) NULL,
	[CD_USERDEF3] [nvarchar](4) NULL,
	[CD_USERDEF4] [nvarchar](4) NULL,
	[CD_USERDEF5] [nvarchar](4) NULL,
	[TXT_USERDEF1] [nvarchar](200) NULL,
	[TXT_USERDEF2] [nvarchar](200) NULL,
	[TXT_USERDEF3] [nvarchar](200) NULL,
	[TXT_USERDEF4] [nvarchar](200) NULL,
	[TXT_USERDEF5] [nvarchar](200) NULL,
	[NUM_USERDEF1] [numeric](17, 4) NULL,
	[NUM_USERDEF2] [numeric](17, 4) NULL,
 CONSTRAINT [PK_PR_LINK_MES] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PLANT] ASC,
	[NO_MES] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [NEOE].[PR_LINK_MES] ADD  CONSTRAINT [DF_PR_LINK_MES_YN_END]  DEFAULT ('N') FOR [YN_END]
GO

ALTER TABLE [NEOE].[PR_LINK_MES] ADD  CONSTRAINT [DF_PR_LINK_MES_YN_WO]  DEFAULT ('N') FOR [YN_WO]
GO

ALTER TABLE [NEOE].[PR_LINK_MES] ADD  CONSTRAINT [DF_PR_LINK_MES_YN_WORK]  DEFAULT ('Y') FOR [YN_WORK]
GO

ALTER TABLE [NEOE].[PR_LINK_MES] ADD  CONSTRAINT [DF_PR_LINK_MES_QT_WORK]  DEFAULT ((0)) FOR [QT_WORK]
GO

ALTER TABLE [NEOE].[PR_LINK_MES] ADD  CONSTRAINT [DF_PR_LINK_MES_QT_REJECT]  DEFAULT ((0)) FOR [QT_REJECT]
GO

ALTER TABLE [NEOE].[PR_LINK_MES] ADD  CONSTRAINT [DF_PR_LINK_MES_QT_MOVE]  DEFAULT ((0)) FOR [QT_MOVE]
GO

ALTER TABLE [NEOE].[PR_LINK_MES] ADD  CONSTRAINT [DF_PR_LINK_MES_QT_BAD]  DEFAULT ((0)) FOR [QT_BAD]
GO

ALTER TABLE [NEOE].[PR_LINK_MES] ADD  CONSTRAINT [DF_PR_LINK_MES_YN_REWORK]  DEFAULT ('N') FOR [YN_REWORK]
GO

ALTER TABLE [NEOE].[PR_LINK_MES] ADD  CONSTRAINT [DF_PR_LINK_MES_YN_MANDAY]  DEFAULT ('N') FOR [YN_MANDAY]
GO

ALTER TABLE [NEOE].[PR_LINK_MES] ADD  CONSTRAINT [DF_PR_LINK_MES_YN_ERR]  DEFAULT ('N') FOR [YN_ERR]
GO


