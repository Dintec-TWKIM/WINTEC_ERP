USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_GIRH_PACK_DETAIL]    Script Date: 2023-05-22 ¿ÀÀü 9:41:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_GIRH_PACK_DETAIL](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_GIR] [nvarchar](20) NOT NULL,
	[NO_GIREQ] [nvarchar](20) NULL,
	[NO_IMO] [nvarchar](10) NULL,
	[CD_PACK_CATEGORY] [nvarchar](3) NULL,
	[CD_SUB_CATEGORY] [nvarchar](3) NULL,
	[YN_PACKING] [nvarchar](1) NULL,
	[CD_COLLECT_FROM] [nvarchar](20) NULL,
	[SEQ_COLLECT_PIC] [nvarchar](5) NULL,
	[DT_START] [nvarchar](8) NULL,
	[DT_COMPLETE] [nvarchar](8) NULL,
	[DTS_SUBMIT] [nvarchar](14) NULL,
	[DTS_CONFIRM] [nvarchar](14) NULL,
	[DT_PACKING] [nvarchar](14) NULL,
	[NO_PACK_EMP] [nvarchar](10) NULL,
	[CD_RMK] [nvarchar](3) NULL,
	[DC_RMK] [nvarchar](max) NULL,
	[DC_RMK1] [nvarchar](max) NULL,
	[DC_RMK2] [nvarchar](max) NULL,
	[DC_RMK3] [nvarchar](max) NULL,
	[DC_RESULT] [nvarchar](max) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
	[DC_RMK_CI] [nvarchar](1000) NULL,
	[DTS_PRINT] [nvarchar](14) NULL,
	[DC_RMK4] [nvarchar](max) NULL,
	[DC_RMK5] [nvarchar](max) NULL,
	[ID_CONFIRM] [nvarchar](15) NULL,
 CONSTRAINT [PK_CZ_SA_GIRH_PACK_DETAIL] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_GIR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


