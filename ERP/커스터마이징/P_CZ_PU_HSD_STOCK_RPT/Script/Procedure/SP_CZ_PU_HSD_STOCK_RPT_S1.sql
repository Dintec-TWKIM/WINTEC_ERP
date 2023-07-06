USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_HSD_STOCK_RPT_S1]    Script Date: 2015-06-16 坷饶 3:36:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PU_HSD_STOCK_RPT_S1] 
(
	@P_CD_COMPANY    NVARCHAR(7),
    @P_DT_FROM       NVARCHAR(8),
    @P_DT_TO         NVARCHAR(8),
    @P_YN_CANCEL     NVARCHAR(1)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

CREATE TABLE #惯林前格
(
    CD_COMPANY   NVARCHAR(7),
    CD_ITEM	     NVARCHAR(20),
    DC1          NVARCHAR(200)
)

CREATE NONCLUSTERED INDEX 惯林前格 ON #惯林前格 (CD_COMPANY, CD_ITEM)
CREATE NONCLUSTERED INDEX 惯林前格1 ON #惯林前格 (CD_COMPANY, DC1)

SELECT * INTO #LAMELLA 
FROM (VALUES ('DC50049'),
             ('DC60059'),
             ('DC70048'),
             ('DC98051'),
             ('DE50001'),
             ('DE60493'),
             ('DE80080'),
             ('DE90131'),
             ('DE95026')) AS A(CD_ITEM)

INSERT INTO #惯林前格
SELECT PL.CD_COMPANY, 
       PL.CD_ITEM, 
       PL.DC1 
FROM (SELECT PL.CD_COMPANY, 
             PL.CD_ITEM,
             PL.DC1
      FROM PU_POH PH
      JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
      WHERE PH.CD_COMPANY = @P_CD_COMPANY
      AND PH.CD_PARTNER = '01340'
      AND ISNULL(PL.DC1, '') LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]-[0-9][0-9][0-9]'
      AND NOT EXISTS (SELECT 1 
                      FROM CZ_PU_POL_HSD PL1
                      WHERE PL1.CD_COMPANY = PL.CD_COMPANY
                      AND PL1.NO_PO = PL.NO_PO
                      AND PL1.NO_LINE = PL.NO_LINE)
      UNION ALL
      SELECT PL.CD_COMPANY,
             PL.CD_ITEM,
             PL.DC1
      FROM CZ_PU_POL_HSD PL) PL
GROUP BY PL.CD_COMPANY, PL.CD_ITEM, PL.DC1

;WITH A AS
(
    SELECT HD.QT_SO,
           (SELECT MAX(SH.NO_SO) AS NO_SO
            FROM SA_SOH SH
            WHERE SH.CD_COMPANY = @P_CD_COMPANY
            GROUP BY SH.NO_PO_PARTNER
            HAVING SH.NO_PO_PARTNER = HD.NO_PO_PARTNER
            AND COUNT(1) = 1) AS NO_FILE,
           HD.CD_ITEM,
           REPLACE(HD.DT_SO, '-', '') AS DT_SO,
           HD.NO_PO_PARTNER,
           HD.NM_PARTNER,
           HD.NM_VESSEL,
           HD.TP_ENGINE AS NM_MODEL,
           HD.NO_ORDER AS DC1,
           HD.NO_PJT AS NO_ORDER,
           HD.YN_LOSS,
           HD.DC_RMK,
           (CASE WHEN ISNULL(SB.QT_STOCK, 0) = 0 OR (ISNULL(SB.QT_STOCK, 0) - (HD.QT_SO_SUM - HD.QT_SO)) >= HD.QT_SO THEN 0
                                                                                                                     ELSE (HD.QT_SO - (ISNULL(SB.QT_STOCK, 0) - (HD.QT_SO_SUM - HD.QT_SO))) END) AS QT_MINUS
    FROM (SELECT *,
                 SUM(HD.QT_SO) OVER (PARTITION BY HD.NO_PO_PARTNER, A.CD_ITEM ORDER BY HD.QT_SO, HD.NO_HSD) AS QT_SO_SUM 
          FROM CZ_SA_HSD_DATA_LOG HD
          JOIN #惯林前格 A ON A.DC1 = HD.NO_ORDER
          WHERE REPLACE(HD.DT_SO, '-', '') BETWEEN @P_DT_FROM AND @P_DT_TO) HD
    LEFT JOIN (SELECT SH.NO_PO_PARTNER, SB.CD_ITEM,
                      SUM(SB.QT_STOCK_HSD) AS QT_STOCK 
               FROM (SELECT SB.*,
                            (CASE WHEN SB.CD_ITEM IN (SELECT * FROM #LAMELLA) THEN ROUND(ISNULL(SB.QT_BOOK, 0) + ISNULL(SB.QT_HOLD, 0) / 3, 0) 
                                                                              ELSE ISNULL(SB.QT_BOOK, 0) + ISNULL(SB.QT_HOLD, 0) END) AS QT_STOCK_HSD
                     FROM CZ_SA_STOCK_BOOK SB) SB
               JOIN SA_SOH SH ON SH.CD_COMPANY = SB.CD_COMPANY AND SH.NO_SO = SB.NO_FILE
               GROUP BY SH.NO_PO_PARTNER, SB.CD_ITEM) SB
    ON SB.NO_PO_PARTNER = HD.NO_PO_PARTNER AND SB.CD_ITEM = HD.CD_ITEM
)
SELECT A.QT_SO,
       A.NO_FILE,
       A.CD_ITEM,
       A.DT_SO,
       A.NO_PO_PARTNER,
       A.NM_PARTNER,
       A.NM_VESSEL,
       A.NM_MODEL,
       A.DC1,
       A.NO_ORDER,
       A.YN_LOSS,
       A.DC_RMK,
       A.QT_MINUS 
FROM A
WHERE (@P_YN_CANCEL = '' OR (@P_YN_CANCEL = 'N' AND A.QT_MINUS <= 0) OR (@P_YN_CANCEL = 'Y' AND A.QT_MINUS > 0))

DROP TABLE #惯林前格

GO