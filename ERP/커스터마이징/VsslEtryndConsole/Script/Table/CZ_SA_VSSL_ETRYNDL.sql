USE [NEOE]
GO

/****** Object:  Table [NEOE].[CZ_SA_VSSL_ETRYNDL]    Script Date: 2018-11-27 오후 7:56:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NEOE].[CZ_SA_VSSL_ETRYNDL](
	[CD_PRTAG] [nvarchar](3) NOT NULL,
	[NM_PRTAG] [nvarchar](10) NOT NULL,
	[DT_ETRYPT_YEAR] [nvarchar](4) NOT NULL,
	[CNT_ETRYPT] [nvarchar](3) NOT NULL,
	[CLSGN] [nvarchar](10) NOT NULL,
	[NM_VSSL] [nvarchar](20) NOT NULL,
	[NM_ETRYND] [nvarchar](2) NOT NULL,
	[DTS_ETRYPT] [nvarchar](14) NULL,
	[DTS_TKOFF] [nvarchar](14) NULL,
	[NM_IBOBPRT] [nvarchar](2) NULL,
	[CD_LAIDUPFCLTY] [nvarchar](3) NULL,
	[CD_LAIDUPFCLTY_SUB] [nvarchar](2) NULL,
	[NM_LAIDUPFCLTY] [nvarchar](20) NULL,
	[YN_PILTG] [nvarchar](1) NULL,
	[CD_LDADNG_FRGHT_CL] [nvarchar](2) NULL,
	[TON_LDADNG] [numeric](9, 4) NULL,
	[TON_TRNPDT] [numeric](9, 4) NULL,
	[TON_LANDNG_FRGHT] [numeric](9, 4) NULL,
	[TON_LD_FRGHT] [numeric](9, 4) NULL,
	[GRTG] [numeric](9, 4) NULL,
	[INTRL_GRTG] [numeric](9, 4) NULL,
	[NM_SATMN_ENTRPS] [nvarchar](30) NULL,
	[CNT_CREW] [numeric](3, 0) NULL,
	[CNT_KORAN_CREW] [numeric](3, 0) NULL,
	[CNT_FRGNR_CREW] [numeric](3, 0) NULL,
	[DTS_INSERT] [nvarchar](14) NULL,
	[DTS_UPDATE] [nvarchar](14) NULL,
 CONSTRAINT [PK_CZ_MA_VSSL_ENTRYDL_1] PRIMARY KEY CLUSTERED 
(
	[CD_PRTAG] ASC,
	[DT_ETRYPT_YEAR] ASC,
	[CNT_ETRYPT] ASC,
	[CLSGN] ASC,
	[NM_VSSL] ASC,
	[NM_ETRYND] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


