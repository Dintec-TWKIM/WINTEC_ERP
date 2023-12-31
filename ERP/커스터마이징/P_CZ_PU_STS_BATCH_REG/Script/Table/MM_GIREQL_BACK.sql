USE [NEOE]
GO

/****** Object:  Table [NEOE].[MM_GIREQL_BACK]    Script Date: 2017-11-23 오후 3:19:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[MM_GIREQL_BACK](
	[NO_GIREQ] [nvarchar](20) NOT NULL,
	[NO_LINE] [numeric](5, 0) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_ITEM] [nvarchar](20) NULL,
	[DT_DELIVERY] [nchar](8) NULL,
	[CD_SL] [nvarchar](7) NULL,
	[QT_GIREQ] [numeric](17, 4) NOT NULL,
	[QT_GI] [numeric](17, 4) NOT NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[CD_GRSL] [nvarchar](7) NULL,
	[NO_PO] [nvarchar](20) NULL,
	[NO_PO_LINE] [numeric](5, 0) NULL,
	[NO_PO_MAL_LINE] [numeric](5, 0) NULL,
	[YN_APPEND] [nvarchar](1) NULL,
	[NO_WO] [nvarchar](20) NULL,
	[NO_WO_LINE] [numeric](5, 0) NULL,
	[CD_WC] [nvarchar](7) NULL,
	[CD_WCOP] [nvarchar](4) NULL,
	[QT_PO_MM] [numeric](17, 4) NOT NULL,
	[CD_WORKITEM] [nvarchar](20) NULL,
	[QT_WORK] [numeric](17, 4) NOT NULL,
	[CD_PJT] [nvarchar](20) NULL,
	[DC_RMK] [nvarchar](100) NULL,
	[QT_REQ] [numeric](17, 4) NULL,
	[QT_RES] [numeric](17, 4) NULL,
	[QT_REM] [numeric](17, 4) NULL,
	[QT_ASS] [numeric](17, 4) NULL,
	[QT_USE] [numeric](17, 4) NULL,
	[QT_WEIGHT] [numeric](17, 4) NULL,
	[UNIT_WEIGHT] [nvarchar](3) NULL,
	[UM_EX_PSO] [numeric](17, 4) NULL,
	[UM_EX] [numeric](17, 4) NULL,
	[UM] [numeric](17, 4) NULL,
	[AM_EX] [numeric](17, 4) NULL,
	[AM] [numeric](17, 4) NULL,
	[CD_EXCH] [nvarchar](3) NULL,
	[RT_EXCH] [numeric](17, 4) NULL,
	[DT_REC] [nvarchar](8) NULL,
	[YN_CLOSE] [nvarchar](3) NULL,
	[NO_SO] [nvarchar](20) NULL,
	[SEQ_SO] [numeric](5, 0) NULL,
	[SEQ_MGMT] [numeric](5, 0) NULL,
	[SEQ_PROJECT] [numeric](5, 0) NULL,
	[NO_WBS] [nvarchar](20) NULL,
	[NO_CBS] [nvarchar](20) NULL,
	[NO_LOT_DISP] [nvarchar](50) NULL,
	[CD_USERDEF1] [nvarchar](50) NULL,
	[CD_USERDEF2] [nvarchar](50) NULL,
	[CD_USERDEF3] [nvarchar](50) NULL,
	[NO_LINE_PJTBOM] [numeric](5, 0) NOT NULL,
	[QT_MGREQ] [numeric](17, 4) NULL,
	[YN_CONFIRM] [nvarchar](1) NULL,
	[FG_POST] [nvarchar](3) NULL,
	[DT_CONFIRM] [nvarchar](14) NULL,
	[NO_INTPO] [nvarchar](20) NULL,
	[SEQ_INTPO] [numeric](5, 0) NULL,
	[QT_DLV] [numeric](17, 4) NULL,
	[FG_GUBUN] [nvarchar](4) NULL,
	[NO_LINK] [nvarchar](20) NULL,
	[NO_LINE_LINK] [numeric](5, 0) NULL,
	[YN_PICKING] [nchar](1) NULL,
	[CD_CENTER] [nvarchar](20) NULL,
	[NO_AS] [nvarchar](20) NULL,
	[NO_ASLINE] [numeric](5, 0) NULL,
	[CD_USERDEF5] [nvarchar](20) NULL,
	[CD_USERDEF6] [nvarchar](20) NULL,
	[TXT_USERDEF1] [nvarchar](100) NULL,
	[CD_ITEM_REF] [nvarchar](20) NULL,
	[GI_PARTNER] [nvarchar](20) NULL,
	[CD_CC] [nvarchar](12) NULL,
	[SETPOS_NO] [nvarchar](20) NULL,
	[ID_MEMO] [nvarchar](36) NULL,
	[CD_WBS] [nvarchar](20) NULL,
	[NO_SHARE] [nvarchar](20) NULL,
	[NO_ISSUE] [nvarchar](20) NULL,
	[NM_USERDEF1] [nvarchar](50) NULL,
	[NM_USERDEF2] [nvarchar](50) NULL,
	[DATE_USERDEF1] [nvarchar](8) NULL,
	[NUM_USERDEF1] [nvarchar](50) NULL,
	[NUM_USERDEF2] [numeric](17, 4) NULL,
	[NUM_USERDEF3] [numeric](17, 4) NULL,
	[YN_SEND_LEGACY] [nchar](1) NULL,
	[ID_DELETE] [nvarchar](15) NULL,
	[DTS_DELETE] [nvarchar](14) NULL,
 CONSTRAINT [PK_MM_GIREQL_BACK] PRIMARY KEY CLUSTERED 
(
	[NO_GIREQ] ASC,
	[NO_LINE] ASC,
	[CD_COMPANY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_QT_GIREQ]  DEFAULT ((0)) FOR [QT_GIREQ]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_QT_GI]  DEFAULT ((0)) FOR [QT_GI]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_YN_APPEND]  DEFAULT ('N') FOR [YN_APPEND]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF__MM_GIREQL__NO_WO__1C6E9405]  DEFAULT ((0)) FOR [NO_WO_LINE]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF__MM_GIREQL__QT_PO__1D62B83E]  DEFAULT ((0)) FOR [QT_PO_MM]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF__MM_GIREQL__QT_WO__1E56DC77]  DEFAULT ((0)) FOR [QT_WORK]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_CD_PJT]  DEFAULT ('') FOR [CD_PJT]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_QT_REQ]  DEFAULT ((0)) FOR [QT_REQ]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_QT_RES]  DEFAULT ((0)) FOR [QT_RES]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_QT_REM]  DEFAULT ((0)) FOR [QT_REM]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_QT_ASS]  DEFAULT ((0)) FOR [QT_ASS]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_QT_USE]  DEFAULT ((0)) FOR [QT_USE]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF__MM_GIREQL__QT_WE__2503DA06]  DEFAULT ((0)) FOR [QT_WEIGHT]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF__MM_GIREQL__UNIT___25F7FE3F]  DEFAULT (NULL) FOR [UNIT_WEIGHT]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF__MM_GIREQL_BACK__DT_RE__33B64C86]  DEFAULT (NULL) FOR [DT_REC]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF__MM_GIREQL__SEQ_S__27E046B1]  DEFAULT ((0)) FOR [SEQ_SO]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF__MM_GIREQL__SEQ_M__28D46AEA]  DEFAULT ((0)) FOR [SEQ_MGMT]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF__MM_GIREQL__SEQ_P__29C88F23]  DEFAULT ((0)) FOR [SEQ_PROJECT]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_NO_LINE_PJTBOM]  DEFAULT ((0)) FOR [NO_LINE_PJTBOM]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_FG_POST]  DEFAULT ('O') FOR [FG_POST]
GO

ALTER TABLE [NEOE].[MM_GIREQL_BACK] ADD  CONSTRAINT [DF_MM_GIREQL_BACK_ID_MEMO]  DEFAULT (newid()) FOR [ID_MEMO]
GO


