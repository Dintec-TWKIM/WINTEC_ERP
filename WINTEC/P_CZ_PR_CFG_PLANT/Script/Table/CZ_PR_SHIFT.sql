USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PR_SHIFT]    Script Date: 2023-07-03 ¿ÀÈÄ 6:42:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PR_SHIFT](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[NO_CAL] [nvarchar](20) NOT NULL,
	[TP_START] [nchar](1) NOT NULL,
	[TM_START] [nvarchar](6) NOT NULL,
	[TP_END] [nchar](1) NOT NULL,
	[TM_END] [nvarchar](6) NOT NULL,
	[TM_STOP] [nvarchar](6) NULL,
	[TM_SFT] [nvarchar](6) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[QT_WORKER] [numeric](17, 4) NULL,
	[CD_DEPT] [nvarchar](12) NULL,
	[YN_USE] [nvarchar](1) NULL,
	[DC_RMK] [nvarchar](100) NULL
) ON [PRIMARY]
GO


