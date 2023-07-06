USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_LOG_PLAN_WORD]    Script Date: 2023-01-10 오후 4:20:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_LOG_PLAN_WORD](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[SEQ] [int] IDENTITY(1,1) NOT NULL,
	[DC_WORD] [nvarchar](100) NULL,
	[ID_INSERT] [nvarchar](14) NULL,
	[DTS_INSERT] [nvarchar](15) NULL,
	[ID_UPDATE] [nvarchar](14) NULL,
	[DTS_UPDATE] [nvarchar](15) NULL,
 CONSTRAINT [PK_CZ_SA_LOG_PLAN_WORD] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[SEQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


