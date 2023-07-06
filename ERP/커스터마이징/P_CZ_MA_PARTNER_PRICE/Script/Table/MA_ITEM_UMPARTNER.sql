USE [NEOE]
GO

/****** Object:  Table [NEOE].[MA_ITEM_UMPARTNER]    Script Date: 2022-12-26 오후 6:33:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[MA_ITEM_UMPARTNER](
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NOT NULL,
	[FG_UM] [nchar](3) NOT NULL,
	[CD_EXCH] [nvarchar](3) NOT NULL,
	[NO_LINE] [numeric](5, 0) NOT NULL,
	[TP_UMMODULE] [nchar](3) NOT NULL,
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[UM_ITEM] [numeric](15, 4) NOT NULL,
	[UM_ITEM_LOW] [numeric](15, 4) NULL,
	[SDT_UM] [nchar](8) NOT NULL,
	[EDT_UM] [nchar](8) NOT NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[FG_REASON] [nvarchar](4) NULL,
	[DC_RMK] [nvarchar](100) NULL,
	[RT_MARGIN] [numeric](17, 4) NULL,
	[UP_PCOST] [numeric](17, 4) NULL,
	[UM_MTRL] [numeric](17, 4) NULL,
	[DT_APPLY] [nvarchar](8) NULL,
	[AM_PAKING] [numeric](17, 4) NULL,
	[CD_USERDEF1] [nvarchar](3) NULL,
	[NUM_USERDEF1] [numeric](19, 6) NULL,
	[NUM_USERDEF2] [numeric](19, 6) NULL,
	[TXT_USERDEF1] [nvarchar](200) NULL,
	[LT] [numeric](5, 0) NULL,
	[NUM_USERDEF3] [numeric](19, 6) NULL,
	[DC_PRICE_TERMS] [nvarchar](20) NULL,
	[NUM_USERDEF4] [numeric](19, 6) NULL,
	[NUM_USERDEF5] [numeric](19, 6) NULL,
	[NUM_USERDEF6] [numeric](19, 6) NULL,
	[NUM_USERDEF7] [numeric](19, 6) NULL,
	[NUM_USERDEF8] [numeric](19, 6) NULL,
	[NUM_USERDEF9] [numeric](19, 6) NULL,
	[NUM_USERDEF10] [numeric](19, 6) NULL,
	[CD_USERDEF2] [nvarchar](20) NULL,
	[CD_EXCH_S] [nvarchar](3) NULL,
	[UM_ITEM_S] [numeric](15, 4) NULL,
	[FILE_PATH_MNG] [nvarchar](300) NULL,
	[RT_PROFIT_A] [numeric](11, 4) NULL,
	[RT_PROFIT_B] [numeric](11, 4) NULL,
	[RT_PROFIT_C] [numeric](11, 4) NULL,
	[CD_PARTNER_STD] [nvarchar](20) NULL,
 CONSTRAINT [PK_MA_ITEM_UMPARTNER] PRIMARY KEY CLUSTERED 
(
	[CD_ITEM] ASC,
	[CD_PARTNER] ASC,
	[FG_UM] ASC,
	[CD_EXCH] ASC,
	[SDT_UM] ASC,
	[TP_UMMODULE] ASC,
	[CD_COMPANY] ASC,
	[CD_PLANT] ASC,
	[NO_LINE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [NEOE].[MA_ITEM_UMPARTNER] ADD  CONSTRAINT [DF_MA_ITEM_UMPARTNER_NO_LINE]  DEFAULT ((0)) FOR [NO_LINE]
GO

ALTER TABLE [NEOE].[MA_ITEM_UMPARTNER] ADD  CONSTRAINT [DF_MA_ITEM_UMPARTNER_CD_PLANT]  DEFAULT ((1000000)) FOR [CD_PLANT]
GO

ALTER TABLE [NEOE].[MA_ITEM_UMPARTNER] ADD  CONSTRAINT [DF_MA_ITEM_UMPARTNER_EDT_UM]  DEFAULT ((0)) FOR [EDT_UM]
GO

ALTER TABLE [NEOE].[MA_ITEM_UMPARTNER] ADD  CONSTRAINT [DF_MA_ITEM_UMPARTNER_RT_MARGIN]  DEFAULT ((0)) FOR [RT_MARGIN]
GO

ALTER TABLE [NEOE].[MA_ITEM_UMPARTNER] ADD  CONSTRAINT [DF_MA_ITEM_UMPARTNER_UP_PCOST]  DEFAULT ((0)) FOR [UP_PCOST]
GO

ALTER TABLE [NEOE].[MA_ITEM_UMPARTNER] ADD  CONSTRAINT [DF_MA_ITEM_UMPARTNER_UM_MTRL]  DEFAULT ((0)) FOR [UM_MTRL]
GO


