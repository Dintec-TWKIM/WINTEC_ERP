USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_SOL_MAIL_SEND_WINTEC]    Script Date: 2022-01-20 오후 5:13:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_SOL_MAIL_SEND_WINTEC](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_SO] [nvarchar](20) NOT NULL,
	[SEQ_SO] [nvarchar](5) NOT NULL,
	[TP_MAIL] [nvarchar](4) NOT NULL,
	[IDX] [int] NOT NULL,
	[DC1] [nvarchar](200) NULL,
	[DC2] [nvarchar](200) NULL,
	[ID_INSERT] [nvarchar](10) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_SOL_MAIL_SEND_WINTEC] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_SO] ASC,
	[SEQ_SO] ASC,
	[TP_MAIL] ASC,
	[IDX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


