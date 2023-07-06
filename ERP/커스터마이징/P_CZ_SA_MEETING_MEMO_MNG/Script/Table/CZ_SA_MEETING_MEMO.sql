USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_MEETING_MEMO]    Script Date: 2017-11-21 오후 1:45:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_MEETING_MEMO](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_MEETING] [nvarchar](20) NOT NULL,
	[CD_PARTNER] [nvarchar](20) NULL,
	[DT_MEETING] [nvarchar](8) NULL,
	[DC_TIME] [nvarchar](100) NULL,
	[DC_LOCATION] [nvarchar](500) NULL,
	[DC_SUBJECT] [nvarchar](500) NULL,
	[DC_PURPOSE] [nvarchar](500) NULL,
	[DC_MEETING] [nvarchar](max) NULL,
	[NO_GW_DOCU] [nvarchar](20) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_MEETING_MEMO] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_MEETING] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


