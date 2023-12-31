USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MAILBAGLIST_H]    Script Date: 2023-05-01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MAILBAGLIST_H](
	[CD_COMPANY] [nvarchar](20) NOT NULL,
    [NO_EMP] [nvarchar](10) NOT NULL,
    [SEND_COMPANY] [nvarchar](2) NOT NULL,
    [DAYS_RECORD] [nvarchar](14) NOT NULL

    CONSTRAINT CZ_MAILBAGLIST_H_PK PRIMARY KEY (SEND_COMPANY, DAYS_RECORD)
    )
GO