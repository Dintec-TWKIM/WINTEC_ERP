USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_PU_IV_CONFIRM]    Script Date: 2021-07-30 오후 6:26:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_PU_IV_CONFIRM](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[SEQ] [int] NOT NULL,
	[TP_CONFIRM] [nvarchar](4) NULL,
	[NO_IO] [nvarchar](20) NULL,
	[NO_ETAX] [nvarchar](30) NULL,
	[NO_PO] [nvarchar](20) NULL,
	[DC_RMK] [nvarchar](1000) NULL,
	[DC_RMK_WF] [nvarchar](1000) NULL,
	[YN_DONE] [nvarchar](1) NULL,
	[DTS_DONE] [nvarchar](14) NULL,
	[DT_END] [nvarchar](8) NULL,
	[AM_EX] [numeric](17, 4) NULL,
	[AM_EX_IO] [numeric](17, 4) NULL,
	[NO_EMP] [nvarchar](100) NULL,
	[YN_SEND] [nvarchar](1) NULL,
	[DTS_SEND] [nvarchar](14) NULL,
	[YN_RETURN] [nvarchar](1) NULL,
	[DTS_RETURN] [nvarchar](14) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_PU_IV_CONFIRM_1] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[SEQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


