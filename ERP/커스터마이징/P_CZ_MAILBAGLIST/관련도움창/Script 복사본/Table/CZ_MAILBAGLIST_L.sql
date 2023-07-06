USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_MAILBAGLIST_L]    Script Date: 2023-05-01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_MAILBAGLIST_L](
    [SEND_COMPANY] [nvarchar](2) NOT NULL,
    [DAYS_RECORD] [nvarchar](14) NOT NULL,
    [SEND_NUM] [nvarchar](14) NOT NULL,
    [SEND_CONTENTS] [nvarchar](MAX) NULL,
    [SEND_QT] [numeric](3, 0) NULL,
    [SEND_ID] [nvarchar](15) NULL,
    [RECEIVE_ID] [nvarchar](15) NULL,
    [INSPECT_ID] [nvarchar](15) NULL,
    [DC_RMK] [nvarchar](MAX) NULL,
    [DTS_UPDATE] [nvarchar](14) NULL,
    [ID_UPDATE] [nvarchar](15) NULL,
    [DTS_INSERT] [nvarchar](14) NULL,
    [ID_INSERT] [nvarchar](15) NULL

    CONSTRAINT CZ_MAILBAGLIST_L_PK PRIMARY KEY (SEND_COMPANY, DAYS_RECORD,SEND_NUM)
    )
GO