USE [NEOE]
GO

/****** Object:  View [NEOE].[V_CZ_SA_QTN_LOG_EMP]    Script Date: 2015-06-19 오전 11:42:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [NEOE].[V_CZ_SA_QTN_LOG_EMP]
AS

SELECT QH.CD_COMPANY,
       QH.NO_FILE,
       ISNULL(MC.CD_FLAG1, 'S-358') AS CD_FLAG1,
       MC.CD_FLAG2,
       MC.CD_FLAG3
FROM CZ_SA_QTNH QH
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
LEFT JOIN CZ_MA_CODEDTL MC ON MC.CD_COMPANY = QH.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00050' AND MC.CD_SYSDEF = (CASE WHEN QH.CD_PARTNER_GRP IN ('54','57') THEN (CASE WHEN ISNULL(MP.CD_PARTNER_GRP, '') = '' THEN QH.CD_PARTNER_GRP ELSE MP.CD_PARTNER_GRP END)
                                                                                                                    WHEN QH.CD_PARTNER IN ('05749', '10416') THEN QH.CD_PARTNER
                                                                                                                                                             ELSE QH.CD_PARTNER_GRP END)
WHERE QH.CD_COMPANY = 'K100'

GO