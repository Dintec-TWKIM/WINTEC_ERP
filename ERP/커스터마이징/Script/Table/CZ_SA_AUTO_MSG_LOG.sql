USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_AUTO_MSG_LOG]    Script Date: 2023-04-19 오후 1:56:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_AUTO_MSG_LOG](
	[IDX] [int] IDENTITY(1,1) NOT NULL,
	[CD_COMPANY] [nvarchar](7) NULL,
	[TP_MSG] [nvarchar](100) NULL,
	[DC_EMP] [nvarchar](max) NULL,
	[DC_CONTENTS] [nvarchar](max) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_AUTO_MSG_LOG] PRIMARY KEY CLUSTERED 
(
	[IDX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


