USE [NEOE]
GO

/****** Object:  Table [NEOE].[SA_GIRL]    Script Date: 2017-04-12 오전 10:32:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[SA_GIRL](
	[NO_GIR] [nvarchar](20) NOT NULL,
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[SEQ_GIR] [numeric](5, 0) NOT NULL,
	[SO_PARTNER] [nvarchar](20) NULL,
	[GI_PARTNER] [nvarchar](20) NULL,
	[CD_PARTNER] [nvarchar](20) NULL,
	[BILL_PARTNER] [nvarchar](20) NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[TP_ITEM] [nvarchar](3) NULL,
	[UNIT] [nvarchar](3) NULL,
	[DT_DUEDATE] [nvarchar](8) NULL,
	[DT_REQGI] [nvarchar](8) NULL,
	[YN_INSPECT] [nchar](1) NULL,
	[QT_GIR] [numeric](17, 4) NULL,
	[UM] [numeric](15, 4) NULL,
	[AM_GIR] [numeric](17, 4) NULL,
	[CD_EXCH] [nvarchar](3) NULL,
	[RT_EXCH] [numeric](15, 4) NULL,
	[AM_GIRAMT] [numeric](17, 4) NULL,
	[NO_PROJECT] [nvarchar](20) NULL,
	[CD_SALEGRP] [nvarchar](7) NULL,
	[NO_EMP] [nvarchar](20) NULL,
	[STA_GIR] [nvarchar](3) NULL,
	[NO_SO] [nvarchar](20) NULL,
	[SEQ_SO] [numeric](5, 0) NULL,
	[NO_LC] [nvarchar](20) NULL,
	[SEQ_LC] [numeric](5, 0) NULL,
	[QT_GI] [numeric](17, 4) NULL,
	[TP_GI] [nvarchar](3) NULL,
	[TP_IV] [nvarchar](3) NULL,
	[TP_BUSI] [nvarchar](3) NULL,
	[CONF] [nchar](1) NULL,
	[GIR] [nchar](1) NULL,
	[GI] [nchar](1) NULL,
	[IV] [nchar](1) NULL,
	[TRADE] [nchar](1) NULL,
	[CM] [nchar](1) NULL,
	[RET] [nchar](1) NULL,
	[SUBCONT] [nchar](1) NULL,
	[QT_INSPECT] [numeric](17, 4) NULL,
	[QT_PASS] [numeric](17, 4) NULL,
	[QT_REJECT] [numeric](17, 4) NULL,
	[YN_DECISION] [nchar](1) NULL,
	[TP_VAT] [nvarchar](3) NULL,
	[RT_VAT] [numeric](5, 2) NULL,
	[QT_GIR_IM] [numeric](17, 4) NULL,
	[QT_GI_IM] [numeric](17, 4) NULL,
	[FG_TAXP] [nchar](3) NULL,
	[AM_VAT] [numeric](17, 4) NULL,
	[CD_SL] [nvarchar](7) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[NO_IO_MGMT] [nvarchar](20) NULL,
	[NO_IOLINE_MGMT] [numeric](5, 0) NULL,
	[NO_SO_MGMT] [nvarchar](20) NULL,
	[NO_SOLINE_MGMT] [numeric](5, 0) NULL,
	[AM_EXGI] [numeric](17, 4) NULL,
	[AM_GI] [numeric](17, 4) NULL,
	[FG_LC_OPEN] [nvarchar](3) NULL,
	[DC_RMK] [nvarchar](100) NULL,
	[DC_RMK2] [nvarchar](100) NULL,
	[CD_WH] [nvarchar](7) NULL,
	[QT_QC_PASS] [numeric](17, 4) NOT NULL,
	[QT_QC_BAD] [numeric](17, 4) NOT NULL,
	[YN_QC_FIX] [nvarchar](1) NULL,
	[QT_GR_PASS] [numeric](17, 4) NOT NULL,
	[QT_GR_BAD] [numeric](17, 4) NOT NULL,
	[NO_LINK] [nvarchar](20) NULL,
	[NO_LINE_LINK] [numeric](5, 0) NULL,
	[NO_INSP_BASE] [nvarchar](20) NULL,
	[NO_PMS] [nvarchar](20) NULL,
	[SEQ_PROJECT] [numeric](5, 0) NOT NULL,
	[YN_PICKING] [nchar](1) NULL,
	[QT_DLV] [numeric](17, 4) NULL,
	[CD_USERDEF1] [nvarchar](4) NULL,
	[NO_INV] [nvarchar](20) NULL,
	[TP_UM_TAX] [nvarchar](3) NULL,
	[UMVAT_GIR] [numeric](15, 4) NULL,
	[ID_MEMO] [nvarchar](36) NULL,
	[CD_WBS] [nvarchar](20) NULL,
	[NO_SHARE] [nvarchar](20) NULL,
	[NO_ISSUE] [nvarchar](20) NULL,
	[QT_GIR_STOCK] [numeric](17, 4) NULL,
	[YN_GI_STOCK] [nvarchar](1) NULL,
	[ID_GI_STOCK] [nvarchar](15) NULL,
	[DTS_GI_STOCK] [nvarchar](14) NULL,
	[NO_LOT] [nvarchar](100) NULL,
	[NUM_USERDEF1] [numeric](17, 4) NULL,
	[NUM_USERDEF2] [numeric](17, 4) NULL,
	[NUM_USERDEF3] [numeric](17, 4) NULL,
	[NUM_USERDEF4] [numeric](17, 4) NULL,
	[NUM_USERDEF5] [numeric](17, 4) NULL,
	[YN_SEND_LEGACY] [nchar](1) NULL,
	[TXT_USERDEF1] [nvarchar](100) NULL,
	[CD_USERDEF2] [nvarchar](4) NULL,
	[TXT_USERDEF2] [nvarchar](100) NULL,
	[YN_ADD_STOCK] [nvarchar](1) NULL,
 CONSTRAINT [PK_SA_GIRL8] PRIMARY KEY NONCLUSTERED 
(
	[NO_GIR] ASC,
	[CD_COMPANY] ASC,
	[SEQ_GIR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__SEQ_GIR__205E2788]  DEFAULT ((0)) FOR [SEQ_GIR]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__DT_DUED__21524BC1]  DEFAULT ('00000000') FOR [DT_DUEDATE]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__DT_REQG__22466FFA]  DEFAULT ('00000000') FOR [DT_REQGI]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_GIR__233A9433]  DEFAULT ((0)) FOR [QT_GIR]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__UM__242EB86C]  DEFAULT ((0)) FOR [UM]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__AM_GIR__2522DCA5]  DEFAULT ((0)) FOR [AM_GIR]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__RT_EXCH__261700DE]  DEFAULT ((1)) FOR [RT_EXCH]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__AM_GIRA__270B2517]  DEFAULT ((0)) FOR [AM_GIRAMT]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__SEQ_SO__27FF4950]  DEFAULT ((0)) FOR [SEQ_SO]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__SEQ_LC__28F36D89]  DEFAULT ((0)) FOR [SEQ_LC]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_GI__29E791C2]  DEFAULT ((0)) FOR [QT_GI]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_INSP__2ADBB5FB]  DEFAULT ((0)) FOR [QT_INSPECT]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_PASS__2BCFDA34]  DEFAULT ((0)) FOR [QT_PASS]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_REJE__2CC3FE6D]  DEFAULT (NULL) FOR [QT_REJECT]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__RT_VAT__2DB822A6]  DEFAULT ((0)) FOR [RT_VAT]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_GIR___3232BF00]  DEFAULT ((0)) FOR [QT_GIR_IM]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_GI_I__3326E339]  DEFAULT ((0)) FOR [QT_GI_IM]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__AM_VAT__2EAC46DF]  DEFAULT ((0)) FOR [AM_VAT]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF_SA_GIRL_CD_SL]  DEFAULT (NULL) FOR [CD_SL]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__AM_EXGI__3AF77FCA]  DEFAULT ((0)) FOR [AM_EXGI]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__AM_GI__3BEBA403]  DEFAULT ((0)) FOR [AM_GI]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_QC_P__21AD4471]  DEFAULT ((0)) FOR [QT_QC_PASS]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_QC_B__22A168AA]  DEFAULT ((0)) FOR [QT_QC_BAD]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_GR_P__3D133789]  DEFAULT ((0)) FOR [QT_GR_PASS]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__QT_GR_B__3E075BC2]  DEFAULT ((0)) FOR [QT_GR_BAD]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF_SA_GIRL_NO_LINE_LINK]  DEFAULT ((0)) FOR [NO_LINE_LINK]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF__SA_GIRL__SEQ_PRO__4FE24DF2]  DEFAULT ((0)) FOR [SEQ_PROJECT]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF_SA_GIRL_ID_MEMO]  DEFAULT (newid()) FOR [ID_MEMO]
GO

ALTER TABLE [NEOE].[SA_GIRL] ADD  CONSTRAINT [DF_SA_GIRL_QT_GIR1]  DEFAULT ((0)) FOR [QT_GIR_STOCK]
GO


