USE [NEOE]
GO

/****** Object:  Table [NEOE].[MM_QTIO_LOG]    Script Date: 2016-11-11 오전 10:15:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[MM_QTIO_LOG](
	[NO_IO] [nvarchar](20) NOT NULL,
	[NO_IOLINE] [numeric](5, 0) NOT NULL,
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[DTS_DELETE] [nvarchar](14) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[CD_BIZAREA] [nvarchar](7) NOT NULL,
	[CD_SL] [nvarchar](7) NOT NULL,
	[CD_SECTION] [nvarchar](7) NOT NULL,
	[CD_BIN] [nvarchar](7) NOT NULL,
	[DT_IO] [nchar](8) NOT NULL,
	[NO_ISURCV] [nvarchar](20) NULL,
	[NO_ISURCVLINE] [numeric](5, 0) NULL,
	[NO_PSO_MGMT] [nvarchar](20) NULL,
	[NO_PSOLINE_MGMT] [numeric](5, 0) NULL,
	[FG_PS] [nchar](1) NOT NULL,
	[YN_PURSALE] [nchar](1) NULL,
	[FG_TPIO] [nchar](3) NULL,
	[FG_IO] [nchar](3) NOT NULL,
	[CD_QTIOTP] [nchar](3) NULL,
	[FG_TRANS] [nchar](3) NULL,
	[FG_TAX] [nvarchar](3) NULL,
	[CD_PARTNER] [nvarchar](20) NULL,
	[CD_ITEM] [nvarchar](20) NULL,
	[QT_IO] [numeric](17, 4) NOT NULL,
	[QT_RETURN] [numeric](17, 4) NULL,
	[QT_TRANS_INV] [numeric](17, 4) NOT NULL,
	[QT_INSP_INV] [numeric](17, 4) NOT NULL,
	[QT_REJECT_INV] [numeric](17, 4) NOT NULL,
	[QT_GOOD_INV] [numeric](17, 4) NOT NULL,
	[CD_EXCH] [nvarchar](3) NULL,
	[RT_EXCH] [numeric](11, 4) NULL,
	[UM_EX] [numeric](15, 4) NOT NULL,
	[AM_EX] [numeric](17, 4) NOT NULL,
	[UM] [numeric](15, 4) NOT NULL,
	[AM] [numeric](17, 4) NOT NULL,
	[QT_CLS] [numeric](17, 4) NOT NULL,
	[AM_CLS] [numeric](17, 4) NOT NULL,
	[VAT] [numeric](17, 4) NOT NULL,
	[VAT_CLS] [numeric](17, 4) NOT NULL,
	[FG_TAXP] [nchar](3) NULL,
	[UM_STOCK] [numeric](15, 4) NOT NULL,
	[UM_EVAL] [numeric](15, 4) NULL,
	[YN_AM] [nchar](1) NULL,
	[CD_PJT] [nvarchar](20) NULL,
	[AM_DISTRIBU] [numeric](17, 4) NOT NULL,
	[RT_CUSTOMS] [numeric](5, 2) NOT NULL,
	[AM_CUSTOMS] [numeric](17, 4) NOT NULL,
	[NO_LC] [nvarchar](20) NULL,
	[NO_LCLINE] [numeric](5, 0) NULL,
	[QT_IMSEAL] [numeric](17, 4) NOT NULL,
	[QT_EXLC] [numeric](17, 4) NOT NULL,
	[GI_PARTNER] [nvarchar](20) NULL,
	[NO_EMP] [nvarchar](10) NULL,
	[CD_GROUP] [nvarchar](7) NULL,
	[NO_IO_MGMT] [nvarchar](20) NULL,
	[NO_IOLINE_MGMT] [numeric](5, 0) NULL,
	[CD_BIZAREA_RCV] [nvarchar](7) NULL,
	[CD_PLANT_RCV] [nvarchar](7) NULL,
	[CD_SL_REF] [nvarchar](7) NULL,
	[CD_SECTION_REF] [nvarchar](7) NULL,
	[CD_BIN_REF] [nvarchar](7) NULL,
	[BILL_PARTNER] [nvarchar](20) NULL,
	[CD_UNIT_MM] [nvarchar](3) NULL,
	[QT_UNIT_MM] [numeric](17, 4) NOT NULL,
	[UM_EX_PSO] [numeric](17, 4) NOT NULL,
	[QT_CLS_MM] [numeric](17, 4) NOT NULL,
	[QT_RETURN_MM] [numeric](17, 4) NOT NULL,
	[CD_WC] [nvarchar](7) NULL,
	[CD_OP] [nvarchar](4) NULL,
	[CD_WCOP] [nvarchar](4) NULL,
	[AM_EVAL] [numeric](17, 4) NULL,
	[FG_LC_OPEN] [nvarchar](3) NULL,
	[QT_INV] [numeric](17, 4) NULL,
	[NO_WORK] [nvarchar](20) NULL,
	[CD_CC] [nvarchar](12) NULL,
	[DC_RMK] [nvarchar](200) NULL,
	[CD_WORKITEM] [nvarchar](20) NULL,
	[ID_DELETE] [nvarchar](15) NULL,
	[SEQ_PROJECT] [numeric](5, 0) NULL,
	[NO_TRACK] [nvarchar](40) NULL,
	[NO_TRACK_LINE] [numeric](5, 0) NULL,
	[CD_LOCATION] [nvarchar](20) NULL,
 CONSTRAINT [PK_MM_QTIO_LOG] PRIMARY KEY CLUSTERED 
(
	[NO_IO] ASC,
	[NO_IOLINE] ASC,
	[CD_COMPANY] ASC,
	[DTS_DELETE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [NEOE].[MM_QTIO_LOG] ADD  CONSTRAINT [DF_MM_QTIO_LOG_CD_PJT]  DEFAULT ('') FOR [CD_PJT]
GO

ALTER TABLE [NEOE].[MM_QTIO_LOG] ADD  CONSTRAINT [DF_MM_QTIO_LOG_SEQ_PROJECT]  DEFAULT (0) FOR [SEQ_PROJECT]
GO

