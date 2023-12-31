USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_AUTO_MAIL_PACK_LOG]    Script Date: 2023-05-22 ���� 6:42:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_AUTO_MAIL_PACK_LOG](
	[IDX] [int] IDENTITY(1,1) NOT NULL,
	[CD_COMPANY] [nvarchar](7) NULL,
	[DT_SEND] [nvarchar](8) NULL,
	[YN_SEND] [nvarchar](1) NULL,
	[NO_SO] [nvarchar](20) NULL,
	[DC_RMK] [nvarchar](100) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_AUTO_MAIL_PACK_LOG] PRIMARY KEY CLUSTERED 
(
	[IDX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


