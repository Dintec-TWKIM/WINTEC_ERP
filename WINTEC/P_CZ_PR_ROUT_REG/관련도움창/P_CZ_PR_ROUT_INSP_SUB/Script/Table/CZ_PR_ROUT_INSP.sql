USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PR_ROUT_INSP]    Script Date: 2021-12-03 오후 3:08:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PR_ROUT_INSP](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_PLANT] [nvarchar](7) NOT NULL,
	[CD_ITEM] [nvarchar](20) NOT NULL,
	[NO_OPPATH] [nvarchar](3) NOT NULL,
	[CD_OP] [nvarchar](4) NOT NULL,
	[CD_WCOP] [nvarchar](7) NOT NULL,
	[NO_INSP] [numeric](4, 0) NOT NULL,
	[DC_ITEM] [nvarchar](30) NULL,
	[CD_MEASURE] [nvarchar](4) NULL,
	[DC_MEASURE] [nvarchar](10) NULL,
	[DC_LOCATION] [nvarchar](20) NULL,
	[DC_SPEC] [nvarchar](30) NULL,
	[QT_MIN] [numeric](17, 4) NULL,
	[QT_MAX] [numeric](17, 4) NULL,
	[TP_DATA] [nvarchar](3) NULL,
	[YN_CERT] [nvarchar](1) NULL,
	[YN_USE] [nvarchar](1) NULL,
	[YN_SAMPLING] [nvarchar](1) NULL,
	[YN_ASSY] [nvarchar](1) NULL,
	[CD_CLEAR_GRP] [nvarchar](4) NULL,
	[CNT_INSP] [tinyint] NULL,
	[DC_RMK] [nvarchar](200) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_PR_ROUT_INSP] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_PLANT] ASC,
	[CD_ITEM] ASC,
	[NO_OPPATH] ASC,
	[CD_OP] ASC,
	[CD_WCOP] ASC,
	[NO_INSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'검사스펙순번' , @level0type=N'SCHEMA',@level0name=N'NEOE', @level1type=N'TABLE',@level1name=N'CZ_PR_ROUT_INSP', @level2type=N'COLUMN',@level2name=N'NO_INSP'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'항목1' , @level0type=N'SCHEMA',@level0name=N'NEOE', @level1type=N'TABLE',@level1name=N'CZ_PR_ROUT_INSP', @level2type=N'COLUMN',@level2name=N'DC_ITEM'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'측정장비' , @level0type=N'SCHEMA',@level0name=N'NEOE', @level1type=N'TABLE',@level1name=N'CZ_PR_ROUT_INSP', @level2type=N'COLUMN',@level2name=N'DC_MEASURE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'위치' , @level0type=N'SCHEMA',@level0name=N'NEOE', @level1type=N'TABLE',@level1name=N'CZ_PR_ROUT_INSP', @level2type=N'COLUMN',@level2name=N'DC_LOCATION'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'스펙' , @level0type=N'SCHEMA',@level0name=N'NEOE', @level1type=N'TABLE',@level1name=N'CZ_PR_ROUT_INSP', @level2type=N'COLUMN',@level2name=N'DC_SPEC'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MIN(최소값)' , @level0type=N'SCHEMA',@level0name=N'NEOE', @level1type=N'TABLE',@level1name=N'CZ_PR_ROUT_INSP', @level2type=N'COLUMN',@level2name=N'QT_MIN'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MAX(최대값)' , @level0type=N'SCHEMA',@level0name=N'NEOE', @level1type=N'TABLE',@level1name=N'CZ_PR_ROUT_INSP', @level2type=N'COLUMN',@level2name=N'QT_MAX'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'사용유무(USE_TYPE)' , @level0type=N'SCHEMA',@level0name=N'NEOE', @level1type=N'TABLE',@level1name=N'CZ_PR_ROUT_INSP', @level2type=N'COLUMN',@level2name=N'YN_USE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'비고' , @level0type=N'SCHEMA',@level0name=N'NEOE', @level1type=N'TABLE',@level1name=N'CZ_PR_ROUT_INSP', @level2type=N'COLUMN',@level2name=N'DC_RMK'
GO


