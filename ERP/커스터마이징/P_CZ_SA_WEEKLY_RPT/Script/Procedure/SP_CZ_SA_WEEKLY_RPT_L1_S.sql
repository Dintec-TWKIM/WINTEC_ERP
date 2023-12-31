USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_WEEKLY_RPT_L1_S]    Script Date: 2018-11-26 오후 3:09:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_WEEKLY_RPT_L1_S]
(
    @P_CD_COMPANY	NVARCHAR(7),
    @P_NO_EMP       NVARCHAR(10),
    @P_DT_YEAR      NVARCHAR(4),
    @P_QT_WEEK      INT
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
       RL.NO_EMP,
       RL.DT_YEAR,
       RL.QT_WEEK,
       RL.TP_GUBUN,
       QH.NO_FILE AS NO_KEY,
       QH.DT_QTN,
       MP.LN_PARTNER,
       ME.NM_KOR,
       SG.NM_SALEGRP,
       MC.NM_SYSDEF AS NM_PARTNER_GRP,
       IG.NM_ITEMGRP,
       QL.QT_ITEM,
       MC1.NM_SYSDEF AS NM_EXCH,
       QH.RT_EXCH,
       QL.AM_QTN_EX,
       QL.AM_QTN_P,
       QL.AM_QTN_S,
       QL.AM_PROFIT,
       ROUND((CASE WHEN ISNULL(QL.AM_QTN_S, 0) = 0 THEN 0 ELSE ISNULL(QL.AM_PROFIT, 0) / ISNULL(QL.AM_QTN_S, 0) END) * 100, 2) AS RT_PROFIT,
       RL.DC_RMK,
       MH.NM_VESSEL,
       (SELECT (CASE WHEN ISNULL(QL.CNT, 0) = 0 THEN ''
                     WHEN ISNULL(QL.CNT, 0) = 1 THEN ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) 
                                                ELSE ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) + ' 외' + CONVERT(NVARCHAR, ISNULL(QL.CNT, 0) - 1) + '개' END) AS NM_SUPPLIER
        FROM (SELECT QL.CD_COMPANY, QL.NO_FILE, QL.CD_SUPPLIER,
                     ROW_NUMBER() OVER (PARTITION BY QL.CD_COMPANY, QL.NO_FILE ORDER BY QL.NO_DSP ASC) AS IDX,
                     SUM(1) OVER (PARTITION BY QL.CD_COMPANY, QL.NO_FILE ORDER BY QL.NO_DSP DESC) AS CNT  
              FROM (SELECT QL.CD_COMPANY, QL.NO_FILE, QL.CD_SUPPLIER,
                           MIN(QL.NO_DSP) AS NO_DSP
                    FROM CZ_SA_QTNL QL
                    WHERE QL.CD_COMPANY = RL.CD_COMPANY
                    AND QL.NO_FILE = RL.NO_KEY 
                    AND QL.CD_SUPPLIER IS NOT NULL
                    GROUP BY QL.CD_COMPANY, QL.NO_FILE, QL.CD_SUPPLIER) QL) QL
        JOIN MA_PARTNER MP ON MP.CD_COMPANY = QL.CD_COMPANY AND MP.CD_PARTNER = QL.CD_SUPPLIER
        WHERE QL.IDX = 1) AS NM_SUPPLIER
FROM CZ_SA_WEEKLY_RPT_L RL
JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = RL.CD_COMPANY AND QH.NO_FILE = RL.NO_KEY
LEFT JOIN (SELECT QL.CD_COMPANY, QL.NO_FILE,
                  MAX(QL.GRP_ITEM) AS GRP_ITEM,
                  COUNT(1) AS QT_ITEM,
                  SUM(QL.AM_KR_P) AS AM_QTN_P,
                  SUM(QL.AM_EX_S) AS AM_QTN_EX,
                  SUM(QL.AM_KR_S) AS AM_QTN_S,
                  SUM(ISNULL(QL.AM_KR_S, 0) - ISNULL(QL.AM_KR_P, 0)) AS AM_PROFIT
           FROM CZ_SA_QTNL QL
           WHERE ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%'
           GROUP BY QL.CD_COMPANY, QL.NO_FILE) QL
ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP
LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = QL.CD_COMPANY AND IG.CD_ITEMGRP = QL.GRP_ITEM
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = QH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000065' AND MC.CD_SYSDEF = QH.CD_PARTNER_GRP
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = QH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = QH.CD_EXCH
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
WHERE RL.CD_COMPANY = @P_CD_COMPANY
AND RL.NO_EMP = @P_NO_EMP
AND RL.DT_YEAR = @P_DT_YEAR
AND RL.QT_WEEK = @P_QT_WEEK
AND RL.TP_GUBUN = '001'

GO