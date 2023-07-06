USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_GIRH_REMARK]    Script Date: 2023-03-24 ¿ÀÈÄ 6:18:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_GIRH_REMARK](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_GIR] [nvarchar](20) NOT NULL,
	[NM_DELIVERY] [nvarchar](100) NULL,
	[DC_DELIVERY_ADDR] [nvarchar](200) NULL,
	[DC_DELIVERY_TEL] [nvarchar](100) NULL,
	[DC_DESTINATION] [nvarchar](50) NULL,
	[YN_EDIT] [nvarchar](1) NULL,
	[YN_REVIEW] [nvarchar](1) NULL,
	[YN_SHIPPING] [nvarchar](1) NULL,
	[YN_CI] [nvarchar](1) NULL,
	[YN_RECEIPT] [nvarchar](1) NULL,
	[YN_ETC] [nvarchar](1) NULL,
	[YN_REFUND] [nvarchar](1) NULL,
	[NO_GIR_SG] [nvarchar](20) NULL,
	[YN_LOADING] [nvarchar](1) NULL,
	[DC_RMK_ADD] [nvarchar](500) NULL,
	[YN_DOMESTIC] [nvarchar](1) NULL,
	[CD_SHIP_LOCATION] [nvarchar](4) NULL,
	[YN_PRE_PHOTO] [nvarchar](1) NULL,
	[YN_POST_PHOTO] [nvarchar](1) NULL,
	[DC_PHOTO] [nvarchar](100) NULL,
	[YN_SPEC_CHECK] [nvarchar](1) NULL,
	[DC_SPEC] [nvarchar](100) NULL,
	[YN_SEPARATE_PACK] [nvarchar](1) NULL,
	[DC_SEPARATE] [nvarchar](100) NULL,
	[YN_CERT] [nvarchar](1) NULL,
	[YN_UNIDENTIFIABLE] [nvarchar](1) NULL,
	[NO_FILE] [nvarchar](100) NULL,
	[TM_DELIVERY] [nvarchar](3) NULL,
	[DC_RMK_PACK] [nvarchar](500) NULL,
	[CD_PORT] [nvarchar](4) NULL,
	[DTS_ETB] [nvarchar](11) NULL,
	[DTS_ETD] [nvarchar](11) NULL,
	[DC_AGENCY] [nvarchar](50) NULL,
	[DC_RMK_SHIP] [nvarchar](200) NULL,
	[YN_UNPACK] [nvarchar](1) NULL,
	[DC_UNPACK] [nvarchar](100) NULL,
	[DTS_DEADLINE] [nvarchar](11) NULL,
	[DC_VESSEL] [nvarchar](100) NULL,
	[DC_SHIP_CRANE] [nvarchar](100) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_GIRH_REMARK] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_GIR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


