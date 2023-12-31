USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_WEEKLY_RPT_S2]    Script Date: 2018-11-26 오후 3:09:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_WEEKLY_RPT_S2]
(
    @P_CD_COMPANY	NVARCHAR(7),
    @P_DT_FROM      NVARCHAR(8),
    @P_DT_TO        NVARCHAR(8),
    @P_NO_EMP       NVARCHAR(10),
    @P_DT_YEAR      NVARCHAR(4),
    @P_QT_WEEK      INT,
    @P_AM_SO        NUMERIC(17, 4)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
       @P_NO_EMP AS NO_EMP,
       @P_DT_YEAR AS DT_YEAR,
       @P_QT_WEEK AS QT_WEEK,
       SH.NO_SO,
       SH.DT_SO,
       MP.LN_PARTNER,
       ME.NM_KOR,
       SG.NM_SALEGRP,
       MC.NM_SYSDEF AS NM_PARTNER_GRP,
       IG.NM_ITEMGRP,
       SL.QT_ITEM,
       MC1.NM_SYSDEF AS NM_EXCH,
       SH.RT_EXCH,
       SL.AM_SO_EX,
       SL.AM_SO_S,
       SL.AM_STOCK,
       SL.AM_PO,
       MH.NM_VESSEL,
       (ISNULL(SL.AM_SO_S, 0) - (ISNULL(SL.AM_PO, 0) + ISNULL(SL.AM_STOCK, 0))) AS AM_PROFIT,
       ROUND((CASE WHEN ISNULL(SL.AM_SO_S, 0) = 0 THEN 0 ELSE (ISNULL(SL.AM_SO_S, 0) - (ISNULL(SL.AM_PO, 0) + ISNULL(SL.AM_STOCK, 0))) / ISNULL(SL.AM_SO_S, 0) END) * 100, 2) AS RT_PROFIT,
       (SELECT (CASE WHEN SL.QT_STOCK > 0 AND ISNULL(QL.CNT, 0) = 0 THEN 'STOCK'
                     WHEN SL.QT_STOCK > 0 AND ISNULL(QL.CNT, 0) > 0 THEN 'STOCK 외' + CONVERT(NVARCHAR, QL.CNT) + '개'
                     WHEN SL.QT_STOCK = 0 AND ISNULL(QL.CNT, 0) = 0 THEN ''
                     WHEN SL.QT_STOCK = 0 AND ISNULL(QL.CNT, 0) = 1 THEN ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) 
                                                                    ELSE ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) + ' 외' + CONVERT(NVARCHAR, QL.CNT-1) + '개' END) AS NM_SUPPLIER
        FROM (SELECT PH.CD_COMPANY, PH.CD_PARTNER,
                     ROW_NUMBER() OVER (PARTITION BY PH.CD_COMPANY ORDER BY PH.NO_PO ASC) AS IDX,
                     SUM(1) OVER (PARTITION BY PH.CD_COMPANY ORDER BY PH.NO_PO DESC) AS CNT  
              FROM PU_POH PH
              WHERE PH.CD_COMPANY = SH.CD_COMPANY
              AND PH.CD_PJT = SH.NO_SO) QL
        JOIN MA_PARTNER MP ON MP.CD_COMPANY = QL.CD_COMPANY AND MP.CD_PARTNER = QL.CD_PARTNER
        WHERE QL.IDX = 1) AS NM_SUPPLIER
FROM SA_SOH SH
LEFT JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
                  COUNT(1) AS QT_ITEM,
                  SUM(SB.QT_STOCK) AS QT_STOCK,
                  MAX(QL.GRP_ITEM) AS GRP_ITEM,
                  SUM(SL.AM_EX_S) AS AM_SO_EX,
                  SUM(SL.AM_KR_S) AS AM_SO_S,
                  SUM(ISNULL(SB.UM_KR, 0) * ISNULL(SB.QT_STOCK, 0)) AS AM_STOCK,
                  SUM(PL.AM_PO) AS AM_PO
           FROM SA_SOL SL
           LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
           LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
           LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
                             SUM(PL.AM) AS AM_PO 
                      FROM PU_POL PL
                      WHERE PL.CD_ITEM NOT LIKE 'SD%'
                      GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
           ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
           WHERE SL.CD_ITEM NOT LIKE 'SD%'
           GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = SH.CD_COMPANY AND SG.CD_SALEGRP = SH.CD_SALEGRP
LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = SL.CD_COMPANY AND IG.CD_ITEMGRP = SL.GRP_ITEM
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = SH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000065' AND MC.CD_SYSDEF = SH.CD_PARTNER_GRP
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = SH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = SH.CD_EXCH
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO
AND SH.NO_EMP = @P_NO_EMP
AND (@P_AM_SO = 0 OR SL.AM_SO_S >= @P_AM_SO)
AND EXISTS (SELECT 1 
            FROM CZ_SA_QTNH QH
            WHERE QH.CD_COMPANY = SH.CD_COMPANY
            AND QH.NO_FILE = SH.NO_SO)
AND NOT EXISTS (SELECT 1 
                FROM CZ_SA_WEEKLY_RPT_L RL
                WHERE RL.CD_COMPANY = SH.CD_COMPANY
                AND RL.NO_EMP = @P_NO_EMP
                AND RL.DT_YEAR = @P_DT_YEAR
                AND RL.QT_WEEK = @P_QT_WEEK
                AND RL.TP_GUBUN = '002'
                AND RL.NO_KEY = SH.NO_SO)

GO