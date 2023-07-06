USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_GIRH_WORK_DETAIL]    Script Date: 2023-05-22 ¿ÀÀü 9:40:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_GIRH_WORK_DETAIL](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_GIR] [nvarchar](20) NOT NULL,
	[NO_GIREQ] [nvarchar](20) NULL,
	[NO_IMO] [nvarchar](10) NULL,
	[CD_MAIN_CATEGORY] [nvarchar](3) NULL,
	[CD_SUB_CATEGORY] [nvarchar](3) NULL,
	[YN_PACKING] [nvarchar](1) NULL,
	[YN_TAX_RETURN] [nvarchar](1) NULL,
	[CD_DELIVERY_TO] [nvarchar](20) NULL,
	[SEQ_DELIVERY_PIC] [nvarchar](5) NULL,
	[CD_FREIGHT] [nvarchar](3) NULL,
	[CD_FORWARDER] [nvarchar](20) NULL,
	[WEIGHT] [numeric](17, 4) NULL,
	[DT_START] [nvarchar](8) NULL,
	[DT_COMPLETE] [nvarchar](8) NULL,
	[DTS_SUBMIT] [nvarchar](14) NULL,
	[DTS_CONFIRM] [nvarchar](14) NULL,
	[FG_REASON] [nvarchar](1) NULL,
	[DT_WORKING] [nvarchar](14) NULL,
	[DT_PACKING] [nvarchar](14) NULL,
	[DT_LOADING] [nvarchar](8) NULL,
	[YN_AUTO_SUBMIT] [nvarchar](1) NULL,
	[DT_AUTO_COMPLETE] [int] NULL,
	[DT_BILL] [nvarchar](8) NULL,
	[YN_BILL] [nvarchar](1) NULL,
	[NO_WORK_EMP] [nvarchar](10) NULL,
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
	[NO_IMO_BILL] [nvarchar](10) NULL,
	[TP_CHARGE] [nvarchar](4) NULL,
	[QT_RECIEPT_PAGE] [int] NULL,
	[NO_DELIVERY_EMAIL] [nvarchar](1000) NULL,
	[YN_CPR] [nvarchar](1) NULL,
	[DTS_CPR] [nvarchar](14) NULL,
	[YN_EXCLUDE_CPR] [nvarchar](1) NULL,
	[DC_RMK_CI] [nvarchar](1000) NULL,
	[DC_RMK_CPR] [nvarchar](1000) NULL,
	[DC_RMK_PL] [nvarchar](1000) NULL,
	[DTS_PRINT] [nvarchar](14) NULL,
	[DT_IV] [nvarchar](8) NULL,
	[DTS_CUTOFF] [nvarchar](14) NULL,
	[CD_TAEGBAE] [nvarchar](4) NULL,
	[DC_RMK4] [nvarchar](max) NULL,
	[DC_RMK5] [nvarchar](max) NULL,
	[ID_CONFIRM] [nvarchar](15) NULL,
 CONSTRAINT [PK_CZ_SA_GIRH_WORK_DETAIL] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_GIR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


