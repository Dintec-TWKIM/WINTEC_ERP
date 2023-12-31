USE [NEOE]
GO

/****** Object:  Table [NEOE].[HR_PEVALU_CMTS]    Script Date: 2016-11-17 오후 2:41:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[HR_PEVALU_CMTS](
	[CD_COMPANY] [nvarchar](7) NOT NULL,
	[CD_EVALU] [nvarchar](8) NOT NULL,
	[CD_EVTYPE] [nvarchar](3) NOT NULL,
	[CD_EVNUMBER] [nvarchar](3) NOT NULL,
	[NO_EMPM] [nvarchar](10) NOT NULL,
	[NO_EMPAN] [nvarchar](10) NOT NULL,
	[DC_COMMENTS] [nvarchar](1500) NULL,
	[ID_INSERT] [nvarchar](15) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[ID_UPDATE] [nvarchar](15) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_PEVALU_CMTS] PRIMARY KEY CLUSTERED 
(
	[CD_COMPANY] ASC,
	[CD_EVALU] ASC,
	[CD_EVTYPE] ASC,
	[CD_EVNUMBER] ASC,
	[NO_EMPM] ASC,
	[NO_EMPAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


