USE [NEOE]
GO

/****** Object:  Table [NEOE].[MA_PARTNER_DEPOSIT_LOG]    Script Date: 2017-01-04 오전 11:01:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[MA_PARTNER_DEPOSIT_LOG](
	[DTS_INSERT] [nvarchar](8) NULL,
	[DTS_JOBDATE] [nvarchar](14) NULL,
	[ID_USER] [nvarchar](30) NULL,
	[CD_JOB] [nvarchar](1) NULL,
	[CD_PARTNER] [nvarchar](20) NULL,
	[CD_COMPANY] [nvarchar](7) NULL,
	[NO_DEPOSIT] [nvarchar](40) NULL,
	[NO_DEPOSIT_N] [nvarchar](40) NULL,
	[CD_BANK] [nvarchar](20) NULL,
	[CD_BANK_N] [nvarchar](20) NULL,
	[USE_YN] [nvarchar](4) NULL,
	[USE_YN_N] [nvarchar](4) NULL,
	[NM_DEPOSIT] [nvarchar](100) NULL,
	[NM_DEPOSIT_N] [nvarchar](100) NULL
) ON [PRIMARY]

GO

