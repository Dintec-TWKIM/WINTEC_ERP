USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PR_WO_INSP]    Script Date: 2021-12-13 오전 11:48:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PR_WO_INSP](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_WO] [nvarchar](20) NOT NULL,
	[NO_LINE] [numeric](5, 0) NOT NULL,
	[SEQ_WO] [numeric](5, 0) NOT NULL,
	[NO_INSP] [numeric](4, 0) NOT NULL,
	[DT_INSP] [nvarchar](8) NULL,
	[NO_EMP] [nvarchar](10) NULL,
	[NO_DATA1] [numeric](18, 4) NULL,
	[NO_DATA2] [numeric](18, 4) NULL,
	[NO_DATA3] [numeric](18, 4) NULL,
	[NO_DATA4] [numeric](18, 4) NULL,
	[NO_DATA5] [numeric](18, 4) NULL,
	[CD_REJECT] [nvarchar](5) NULL,
	[CD_RESOURCE] [nvarchar](5) NULL,
	[NO_HEAT] [nvarchar](20) NULL,
	[YN_MARKING] [nvarchar](1) NULL,
	[DC_RMK] [nvarchar](500) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[NO_SO] [nvarchar](20) NULL,
 CONSTRAINT [PK_CZ_PR_WO_INSP_1] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_WO] ASC,
	[SEQ_WO] ASC,
	[NO_LINE] ASC,
	[NO_INSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'검사스펙순번' , @level0type=N'SCHEMA',@level0name=N'NEOE', @level1type=N'TABLE',@level1name=N'CZ_PR_WO_INSP', @level2type=N'COLUMN',@level2name=N'NO_INSP'
GO


