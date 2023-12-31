USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_MEETING_ATTENDEE]    Script Date: 2020-01-15 오후 6:58:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_MEETING_ATTENDEE](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[NO_MEETING] [nvarchar](20) NOT NULL,
	[TP_INOUT] [nvarchar](3) NOT NULL,
	[NO_INDEX] [numeric](18, 0) NOT NULL,
	[CD_COMPANY_ATTENDEE] [nvarchar](7) NULL,
	[NO_ATTENDEE] [nvarchar](20) NOT NULL,
	[NM_ATTENDEE] [nvarchar](100) NULL,
	[CD_PARTNER] [nvarchar](20) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_SA_MEETING_ATTENDEE] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[NO_MEETING] ASC,
	[TP_INOUT] ASC,
	[NO_INDEX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


