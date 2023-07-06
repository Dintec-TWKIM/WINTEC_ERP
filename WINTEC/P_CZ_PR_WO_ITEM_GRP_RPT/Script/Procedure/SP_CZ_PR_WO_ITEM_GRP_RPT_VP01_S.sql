SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_ITEM_GRP_RPT_VP01_S]
(  
    @P_CD_COMPANY       NVARCHAR(7),
	@P_NO_SO			NVARCHAR(20),
	@P_CD_PARTNER		NVARCHAR(20),
	@P_DT_FROM			NVARCHAR(8),
	@P_DT_TO			NVARCHAR(8),
	@P_NO_DESIGN		NVARCHAR(20),
	@P_YN_EX_GI			NVARCHAR(1),
	@P_NO_OPPATH		NVARCHAR(3),
	@P_ST_WO			NVARCHAR(3),
	@P_YN_EX_CLOSE		NVARCHAR(1),
	@P_YN_EX_GIR		NVARCHAR(1)
)  
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

CREATE TABLE #ITEM_GRP
(
	CD_COMPANY	NVARCHAR(7),
	CD_ITEMGRP	NVARCHAR(20)
)

CREATE NONCLUSTERED INDEX ITEM_GRP ON #ITEM_GRP (CD_COMPANY, CD_ITEMGRP)

CREATE TABLE #BOM
(
	CD_COMPANY	NVARCHAR(7), 
	CD_PLANT	NVARCHAR(7), 
	CD_ITEM_T	NVARCHAR(20), 
	CD_ITEM		NVARCHAR(20), 
	CD_MATL		NVARCHAR(20), 
	LEVEL		INT, 
	PATH		NVARCHAR(100), 
	QT			NUMERIC(25, 10)
)

CREATE NONCLUSTERED INDEX BOM ON #BOM (CD_COMPANY, CD_PLANT, CD_ITEM_T)

CREATE TABLE #STOCK
(
	CD_COMPANY	NVARCHAR(7),
	CD_PLANT	NVARCHAR(7),
	NO_SO		NVARCHAR(20),
	SEQ_SO		NUMERIC(5, 0),
	CD_ITEM		NVARCHAR(20),
	CD_GRSL		NVARCHAR(7),
	QT_APPLY	NUMERIC(17, 4)
)

CREATE NONCLUSTERED INDEX STOCK ON #STOCK (CD_COMPANY, CD_PLANT, NO_SO, SEQ_SO, CD_ITEM)

CREATE TABLE #ST_WO
(
 	NO_SO	   NVARCHAR(20),
 	SEQ_SO	   NUMERIC(5, 0),
	ST_WO	   NVARCHAR(3)
)

CREATE NONCLUSTERED INDEX ST_WO ON #ST_WO (NO_SO, SEQ_SO)

CREATE TABLE #SA_INV
(
	CD_COMPANY	NVARCHAR(7),
	CD_PLANT	NVARCHAR(7),
	NO_SO		NVARCHAR(20),
	SEQ_SO		NUMERIC(5, 0),
	CD_MATL		NVARCHAR(20),
	CD_WC		NVARCHAR(7),
	DC_RMK		NVARCHAR(100),
	YN_REMAIN	NVARCHAR(1)
)

CREATE NONCLUSTERED INDEX SA_INV ON #SA_INV (CD_COMPANY, CD_PLANT, NO_SO, SEQ_SO, CD_MATL)

CREATE TABLE #PR_WO_ROUT
(
	CD_COMPANY	NVARCHAR(7),
	NO_WO		NVARCHAR(20),
	NO_LINE		NUMERIC(5, 0),
	QT_ROUT		NUMERIC(17, 4)
)

CREATE NONCLUSTERED INDEX PR_WO_ROUT ON #PR_WO_ROUT (CD_COMPANY, NO_WO, NO_LINE)

CREATE TABLE #SA_SOL
(
    CD_COMPANY	    NVARCHAR(7),
	NO_SO		    NVARCHAR(20),
    SEQ_SO          NUMERIC(5, 0),
    TXT_USERDEF6    NVARCHAR(100),
    TXT_USERDEF7    NVARCHAR(100),
    NO_PO_PARTNER   NVARCHAR(50),
    QT_SO           NUMERIC(17, 4),
    AM_WONAMT       NUMERIC(17, 4),
    CD_USERDEF1     NVARCHAR(4),
    DT_EXPECT       NVARCHAR(8),
    DT_DUEDATE      NVARCHAR(8),
    DT_REQGI        NVARCHAR(8),
    TXT_USERDEF5    NVARCHAR(100),
    TXT_USERDEF8    NVARCHAR(100),
    DC1             NVARCHAR(200),
    QT_GIR          NUMERIC(17, 4),
    QT_GI           NUMERIC(17, 4),
    CD_PLANT        NVARCHAR(7),
    CD_ITEM         NVARCHAR(20),
    CD_USERDEF3     NVARCHAR(3)
)

CREATE NONCLUSTERED INDEX SA_SOL ON #SA_SOL (CD_COMPANY, NO_SO, SEQ_SO)

;WITH ITEM_GRP
(
	CD_COMPANY,
	CD_ITEMGRP,
	NM_ITEMGRP,
    HD_ITEMGRP,
	LEVEL,
	PATH
)
AS
(
	SELECT IG.CD_COMPANY,
		   IG.CD_ITEMGRP,
	       IG.NM_ITEMGRP,
		   IG.HD_ITEMGRP,
		   LEVEL = 1,
		   CONVERT(VARCHAR(1000), IG.CD_ITEMGRP) AS PATH
	FROM MA_ITEMGRP IG
	WHERE IG.CD_COMPANY = @P_CD_COMPANY
	AND IG.CD_ITEMGRP = 'VP01'
	UNION ALL
	SELECT IG1.CD_COMPANY,
		   IG1.CD_ITEMGRP,
	       IG1.NM_ITEMGRP,
		   IG1.HD_ITEMGRP,
		   LEVEL + 1, 
		   CONVERT(VARCHAR(1000), PATH + ' -> ' + IG1.CD_ITEMGRP) AS PATH
	FROM ITEM_GRP IG
	JOIN MA_ITEMGRP IG1 ON IG1.CD_COMPANY = IG.CD_COMPANY AND IG1.HD_ITEMGRP = IG.CD_ITEMGRP
)
INSERT INTO #ITEM_GRP
(
	CD_COMPANY,
	CD_ITEMGRP
)
SELECT CD_COMPANY,
	   CD_ITEMGRP
FROM ITEM_GRP

;WITH BOM 
(
	CD_COMPANY, 
	CD_PLANT, 
	CD_ITEM_T, 
	CD_ITEM, 
	CD_MATL, 
	LEVEL, 
	PATH, 
	QT
) 
AS
(
	SELECT PB.CD_COMPANY,
		   PB.CD_PLANT,
		   PB.CD_ITEM AS CD_ITEM_T,
		   PB.CD_ITEM, 
		   PB.CD_MATL, 
		   LEVEL = 1, 
		   CONVERT(VARCHAR(1000), PB.CD_MATL),
		   PB1.QT_ITEM_NET
    FROM PR_ROUT_ASN PB
	JOIN PR_BOM PB1 ON PB1.CD_COMPANY = PB.CD_COMPANY AND PB1.CD_PLANT = PB.CD_PLANT AND PB1.CD_ITEM = PB.CD_ITEM AND PB1.CD_MATL = PB.CD_MATL AND PB1.DT_END >= CONVERT(CHAR(8), GETDATE(), 112)
	JOIN MA_PITEM MI ON MI.CD_COMPANY = PB.CD_COMPANY AND MI.CD_PLANT = PB.CD_PLANT AND MI.CD_ITEM = PB.CD_MATL AND MI.TP_PROC = 'M' AND ISNULL(MI.YN_USE, 'N') = 'Y'
	JOIN MA_PITEM MI1 ON MI1.CD_COMPANY = PB.CD_COMPANY AND MI1.CD_PLANT = PB.CD_PLANT AND MI1.CD_ITEM = PB.CD_ITEM
	JOIN #ITEM_GRP IG ON IG.CD_COMPANY = MI1.CD_COMPANY AND IG.CD_ITEMGRP = MI1.GRP_ITEM
	WHERE PB.CD_COMPANY = @P_CD_COMPANY
	AND PB.NO_OPPATH = @P_NO_OPPATH
	AND EXISTS (SELECT 1 
		        FROM PR_ROUT_L RL
		        WHERE RL.CD_COMPANY = PB.CD_COMPANY
		        AND RL.CD_PLANT = PB.CD_PLANT
		        AND RL.CD_ITEM = PB.CD_ITEM
		        AND RL.NO_OPPATH = PB.NO_OPPATH
		        AND RL.TP_OPPATH = PB.TP_OPPATH
		        AND RL.CD_OP = PB.CD_OP
			    AND RL.YN_USE = 'Y')
	AND EXISTS (SELECT 1 
				FROM MA_PITEM MI 
				JOIN #ITEM_GRP IG ON IG.CD_COMPANY = MI.CD_COMPANY AND IG.CD_ITEMGRP = MI.GRP_ITEM
				WHERE MI.CD_COMPANY = PB.CD_COMPANY 
				AND MI.CD_PLANT = PB.CD_PLANT 
				AND MI.CD_ITEM = PB.CD_ITEM)
	UNION ALL
	SELECT BM.CD_COMPANY,
		   BM.CD_PLANT,
		   BM.CD_ITEM_T,
		   PB.CD_ITEM, 
		   PB.CD_MATL, 
		   LEVEL + 1, 
		   CONVERT(VARCHAR(1000), PATH + ' -> ' + PB.CD_MATL),
		   PB1.QT_ITEM_NET
	FROM BOM BM
	JOIN PR_ROUT_ASN PB ON PB.CD_ITEM = BM.CD_MATL AND PB.NO_OPPATH = @P_NO_OPPATH
	JOIN PR_BOM PB1 ON PB1.CD_COMPANY = PB.CD_COMPANY AND PB1.CD_PLANT = PB.CD_PLANT AND PB1.CD_ITEM = PB.CD_ITEM AND PB1.CD_MATL = PB.CD_MATL AND PB1.DT_END >= CONVERT(CHAR(8), GETDATE(), 112)
	JOIN MA_PITEM MI ON MI.CD_COMPANY = PB.CD_COMPANY AND MI.CD_PLANT = PB.CD_PLANT AND MI.CD_ITEM = PB.CD_MATL AND MI.TP_PROC = 'M' AND ISNULL(MI.YN_USE, 'N') = 'Y'
	WHERE EXISTS (SELECT 1 
		          FROM PR_ROUT_L RL
		          WHERE RL.CD_COMPANY = PB.CD_COMPANY
		          AND RL.CD_PLANT = PB.CD_PLANT
		          AND RL.CD_ITEM = PB.CD_ITEM
		          AND RL.NO_OPPATH = PB.NO_OPPATH
		          AND RL.TP_OPPATH = PB.TP_OPPATH
		          AND RL.CD_OP = PB.CD_OP
			      AND RL.YN_USE = 'Y')
)
INSERT INTO #BOM
SELECT CD_COMPANY, 
	   CD_PLANT, 
	   CD_ITEM_T, 
	   CD_ITEM, 
	   CD_MATL, 
	   LEVEL, 
	   PATH, 
	   QT 
FROM BOM
UNION ALL
SELECT PB.CD_COMPANY,
	   PB.CD_PLANT,
	   PB.CD_ITEM AS CD_ITEM_T,
	   PB.CD_ITEM, 
	   PB.CD_ITEM, 
	   LEVEL = 0, 
	   CONVERT(VARCHAR(1000), PB.CD_ITEM),
	   1
FROM PR_ROUT_ASN PB
JOIN MA_PITEM MI1 ON MI1.CD_COMPANY = PB.CD_COMPANY AND MI1.CD_PLANT = PB.CD_PLANT AND MI1.CD_ITEM = PB.CD_ITEM
JOIN #ITEM_GRP IG ON IG.CD_COMPANY = MI1.CD_COMPANY AND IG.CD_ITEMGRP = MI1.GRP_ITEM
WHERE PB.CD_COMPANY = @P_CD_COMPANY
AND PB.NO_OPPATH = @P_NO_OPPATH
AND EXISTS (SELECT 1 
		    FROM PR_ROUT_L RL
		    WHERE RL.CD_COMPANY = PB.CD_COMPANY
		    AND RL.CD_PLANT = PB.CD_PLANT
		    AND RL.CD_ITEM = PB.CD_ITEM
		    AND RL.NO_OPPATH = PB.NO_OPPATH
		    AND RL.TP_OPPATH = PB.TP_OPPATH
		    AND RL.CD_OP = PB.CD_OP
			AND RL.YN_USE = 'Y')
GROUP BY PB.CD_COMPANY, PB.CD_PLANT, PB.CD_ITEM

INSERT INTO #STOCK
SELECT QH.CD_COMPANY, QH.CD_PLANT, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM,
	   MAX(QL.CD_GRSL) AS CD_GRSL,
	   SUM(QL.QT_GI) AS QT_APPLY
FROM MM_GIREQH QH
JOIN MM_GIREQL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_GIREQ = QH.NO_GIREQ
WHERE QH.CD_COMPANY = @P_CD_COMPANY
AND QL.CD_SL = 'SL_STND'
GROUP BY QH.CD_COMPANY, QH.CD_PLANT, QL.NO_SO, QL.SEQ_SO, QL.CD_ITEM
HAVING SUM(QL.QT_GI) > 0

INSERT INTO #PR_WO_ROUT
SELECT WR.CD_COMPANY,
       WR.NO_WO,
       WR.NO_LINE,
       (WR.QT_MOVE - (SUM(WR.QT_BAD) OVER (PARTITION BY WR.CD_COMPANY, WR.NO_WO ORDER BY WR.NO_LINE DESC) - WR.QT_BAD) + WR.QT_WIP) AS QT_ROUT
FROM PR_WO_ROUT WR

INSERT INTO #SA_SOL
(
    CD_COMPANY,
    NO_SO,
    SEQ_SO,
    TXT_USERDEF6,
    TXT_USERDEF7,
    NO_PO_PARTNER,
    QT_SO,
    AM_WONAMT,
    CD_USERDEF1,
    DT_EXPECT,
    DT_DUEDATE,
    DT_REQGI,
    TXT_USERDEF5,
    TXT_USERDEF8,
    DC1,
    QT_GIR,
    QT_GI,
    CD_PLANT,
    CD_ITEM,
    CD_USERDEF3
)
SELECT SL.CD_COMPANY,
       SL.NO_SO,
       SL.SEQ_SO,
       SL.TXT_USERDEF6,
       SL.TXT_USERDEF7,
       SL.NO_PO_PARTNER,
       SL.QT_SO,
       SL.AM_WONAMT,
       SL.CD_USERDEF1,
       SL.DT_EXPECT,
       SL.DT_DUEDATE,
       SL.DT_REQGI,
       SL.TXT_USERDEF5,
       SL.TXT_USERDEF8,
       SL.DC1,
       SL.QT_GIR,
       SL.QT_GI,
       SL.CD_PLANT,
       SL.CD_ITEM,
       SL.CD_USERDEF3
FROM SA_SOL SL
WHERE SL.CD_COMPANY = @P_CD_COMPANY
AND SL.DT_DUEDATE BETWEEN @P_DT_FROM AND @P_DT_TO

;WITH A AS
(
    SELECT SL.CD_COMPANY, SL.CD_PLANT, SL.NO_SO, SL.SEQ_SO, 
		   SW.CD_ITEM,
           WO.NO_WO,
           WR.CD_WC, WR.CD_OP, WR.QT_START, WR.QT_WIP, WR.YN_FINAL, WP.DC_RMK, 
           SW.QT_APPLY,
		   WR1.QT_ROUT,
           SUM(SW.QT_APPLY) OVER (PARTITION BY WO.NO_WO, WR.CD_OP ORDER BY (CASE WHEN SL.QT_SO = ISNULL(SL.QT_GIR, 0) THEN '1' ELSE '2' END), SL.DT_DUEDATE, SL.NO_SO, SL.SEQ_SO) AS QT_TOTAL 
    FROM SA_SOL SL
    JOIN CZ_PR_SA_SOL_PR_WO_MAPPING SW ON SW.CD_COMPANY = SL.CD_COMPANY AND SW.NO_SO = SL.NO_SO AND SW.NO_LINE = SL.SEQ_SO
    LEFT JOIN PR_WO WO ON WO.CD_COMPANY = SW.CD_COMPANY AND WO.NO_WO = SW.NO_WO
    LEFT JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
    LEFT JOIN PR_WCOP WP ON WP.CD_COMPANY = WR.CD_COMPANY AND WP.CD_PLANT = WR.CD_PLANT AND WP.CD_WCOP = WR.CD_WCOP AND WP.CD_WC = WR.CD_WC
	LEFT JOIN #PR_WO_ROUT WR1 ON WR1.CD_COMPANY = WR.CD_COMPANY AND WR1.NO_WO = WR.NO_WO AND WR1.NO_LINE = WR.NO_LINE
    WHERE SL.CD_COMPANY = @P_CD_COMPANY
),
B AS
(
    SELECT A.CD_COMPANY, A.CD_PLANT, A.NO_SO, A.SEQ_SO, 
		   A.CD_ITEM,
		   A.NO_WO,
		   A.CD_WC, A.CD_OP, A.DC_RMK,
		   A.QT_APPLY,
		   (CASE WHEN A.YN_FINAL = 'Y' AND (ISNULL(A.QT_ROUT, 0) - ISNULL(A.QT_WIP, 0)) >= A.QT_TOTAL THEN 'Y' ELSE 'N' END) AS YN_DONE,
           ROW_NUMBER() OVER (PARTITION BY A.NO_SO, A.SEQ_SO, A.CD_ITEM, A.NO_WO ORDER BY A.CD_OP DESC) AS IDX
    FROM A
    WHERE A.QT_ROUT >= A.QT_TOTAL
),
C AS
(
    SELECT ST.CD_COMPANY, ST.CD_PLANT, ST.NO_SO, ST.SEQ_SO, ST.CD_ITEM,
		   '재고' AS NO_WO, 
		   '재고' AS CD_WC,
    	   '999' AS CD_OP,
		   MS.NM_SL AS DC_RMK,
		   ST.QT_APPLY,
		   'Y' AS YN_DONE
	FROM #STOCK ST
	LEFT JOIN MA_SL MS ON MS.CD_COMPANY = ST.CD_COMPANY AND MS.CD_SL = ST.CD_GRSL
    UNION ALL
    SELECT B.CD_COMPANY, B.CD_PLANT, B.NO_SO, B.SEQ_SO, B.CD_ITEM,
           B.NO_WO,
           B.CD_WC,
		   B.CD_OP,
		   (CASE WHEN B.YN_DONE = 'Y' THEN MS.NM_SL ELSE B.DC_RMK END) DC_RMK,
		   B.QT_APPLY,
		   B.YN_DONE
    FROM B
	LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = B.CD_COMPANY AND MI.CD_PLANT = B.CD_PLANT AND MI.CD_ITEM = B.CD_ITEM
	LEFT JOIN MA_SL MS ON MS.CD_COMPANY = MI.CD_COMPANY AND MS.CD_PLANT = MI.CD_PLANT AND MS.CD_SL = MI.CD_SL
    WHERE B.IDX = 1
),
D AS
(
    SELECT C.CD_COMPANY, C.CD_PLANT, C.NO_SO, C.SEQ_SO, C.CD_ITEM,
		   C.YN_DONE, C.DC_RMK, C.CD_WC,
		   CONVERT(INT, SL.QT_SO * BM.QT) AS QT_BOM,
		   CONVERT(INT, SUM(C.QT_APPLY) OVER (PARTITION BY C.NO_SO, C.SEQ_SO, C.CD_ITEM ORDER BY C.CD_OP)) AS QT_TOTAL,
		   ROW_NUMBER() OVER (PARTITION BY C.NO_SO, C.SEQ_SO, C.CD_ITEM ORDER BY C.YN_DONE, C.CD_OP) AS IDX
    FROM #SA_SOL SL
    LEFT JOIN #BOM BM ON BM.CD_COMPANY = SL.CD_COMPANY AND BM.CD_PLANT = SL.CD_PLANT AND BM.CD_ITEM_T = SL.CD_ITEM
    JOIN C ON C.CD_COMPANY = SL.CD_COMPANY AND C.CD_PLANT = SL.CD_PLANT AND C.NO_SO = SL.NO_SO AND C.SEQ_SO = SL.SEQ_SO AND C.CD_ITEM = BM.CD_MATL
)
INSERT INTO #SA_INV
(
	CD_COMPANY,
	CD_PLANT,
	NO_SO,
	SEQ_SO,
	CD_MATL,
	CD_WC,
	DC_RMK,
	YN_REMAIN
)
SELECT D.CD_COMPANY,
	   D.CD_PLANT,
	   D.NO_SO,
	   D.SEQ_SO,
	   D.CD_ITEM,
	   (CASE WHEN D.YN_DONE = 'Y' AND D.QT_BOM = (ISNULL(SW.QT_APPLY, 0) + ISNULL(ST.QT_APPLY, 0)) THEN '완료' ELSE D.CD_WC END) AS CD_WC,
	   (CASE WHEN D.QT_BOM > (ISNULL(SW.QT_APPLY, 0) + ISNULL(ST.QT_APPLY, 0)) THEN CONVERT(VARCHAR, D.QT_TOTAL) + '(' + CONVERT(VARCHAR, CONVERT(INT, ISNULL(SW.QT_APPLY, 0) + ISNULL(ST.QT_APPLY, 0))) + ')' + '/' + CONVERT(VARCHAR, D.QT_BOM)
	   																		   ELSE D.DC_RMK END) AS DC_RMK,
	   (CASE WHEN D.QT_BOM > (ISNULL(SW.QT_APPLY, 0) + ISNULL(ST.QT_APPLY, 0)) THEN 'Y' ELSE 'N' END) AS YN_REMAIN
FROM D
LEFT JOIN (SELECT CD_COMPANY, NO_SO, NO_LINE, CD_ITEM,
			      CONVERT(INT, SUM(QT_APPLY)) AS QT_APPLY
		   FROM CZ_PR_SA_SOL_PR_WO_MAPPING
		   GROUP BY CD_COMPANY, NO_SO, NO_LINE, CD_ITEM) SW
ON SW.CD_COMPANY = D.CD_COMPANY AND SW.NO_SO = D.NO_SO AND SW.NO_LINE = D.SEQ_SO AND SW.CD_ITEM = D.CD_ITEM
LEFT JOIN #STOCK ST ON ST.CD_COMPANY = D.CD_COMPANY AND ST.NO_SO = D.NO_SO AND ST.SEQ_SO = D.SEQ_SO AND ST.CD_ITEM = D.CD_ITEM 
WHERE D.IDX = 1

;WITH A AS
(
	SELECT SL.NO_SO,
    	   SL.SEQ_SO,
    	   (CASE WHEN MI.TP_ITEM = 'SUB' THEN 'CMP'
                 WHEN MI.TP_ITEM <> 'CMP' AND MI.TP_ITEM <> '현합' THEN 'PAR' 
                 ELSE MI.TP_ITEM END) AS TP_ITEM,
    	   1 AS QT_ITEM,
    	   (CASE WHEN IV.CD_WC = '완료' THEN 1 ELSE 0 END) AS QT_DONE
    FROM #SA_SOL SL
    LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_PLANT = SL.CD_PLANT AND MI.CD_ITEM = SL.CD_ITEM
    LEFT JOIN #SA_INV IV ON IV.CD_COMPANY = SL.CD_COMPANY AND IV.CD_PLANT = SL.CD_PLANT AND IV.NO_SO = SL.NO_SO AND IV.SEQ_SO = SL.SEQ_SO AND IV.CD_MATL = SL.CD_ITEM
    UNION ALL
    SELECT SL.NO_SO,
    	   SL.SEQ_SO,
    	   (CASE WHEN MI.TP_ITEM = 'SUB' THEN 'CMP'
                 WHEN MI.TP_ITEM <> 'CMP' AND MI.TP_ITEM <> '현합' THEN 'PAR' 
                 ELSE MI.TP_ITEM END) AS TP_ITEM,
    	   1 AS QT_ITEM,
    	   (CASE WHEN IV.CD_WC = '완료' THEN 1 ELSE 0 END) AS QT_DONE
    FROM #SA_SOL SL
    LEFT JOIN #BOM BM ON BM.CD_COMPANY = SL.CD_COMPANY AND BM.CD_PLANT = SL.CD_PLANT AND BM.CD_ITEM_T = SL.CD_ITEM
    LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = BM.CD_COMPANY AND MI.CD_PLANT = BM.CD_PLANT AND MI.CD_ITEM = BM.CD_MATL
    LEFT JOIN #SA_INV IV ON IV.CD_COMPANY = SL.CD_COMPANY AND IV.CD_PLANT = SL.CD_PLANT AND IV.NO_SO = SL.NO_SO AND IV.SEQ_SO = SL.SEQ_SO AND IV.CD_MATL = BM.CD_MATL
),
B AS
(
	SELECT A.NO_SO, A.SEQ_SO, A.TP_ITEM,
		   (CASE WHEN SUM(A.QT_ITEM) = SUM(A.QT_DONE) THEN 'Y' ELSE 'N' END) AS YN_DONE
	FROM A
	GROUP BY A.NO_SO, A.SEQ_SO, A.TP_ITEM
),
C AS
(
	SELECT B.NO_SO,
		   B.SEQ_SO,
		   MAX(CASE WHEN B.TP_ITEM = 'CMP' AND B.YN_DONE = 'Y' THEN '003'
				    WHEN B.TP_ITEM = '현합' AND B.YN_DONE = 'Y' THEN '002'
				    WHEN B.TP_ITEM = 'PAR' AND B.YN_DONE = 'Y' THEN '001' END) AS ST_WO
	FROM B
	GROUP BY B.NO_SO, B.SEQ_SO
)
INSERT INTO #ST_WO
(
	NO_SO,
	SEQ_SO,
	ST_WO
)
SELECT C.NO_SO,
	   C.SEQ_SO,
	   C.ST_WO
FROM C
WHERE C.ST_WO IS NOT NULL

SELECT 'N' AS S,
	   A3.NO_SO,
	   A3.SEQ_SO,
	   A3.LN_PARTNER,
	   A3.DT_SO,
	   A3.DC_RMK1,
	   A3.NM_SYSDEF AS NM_ENGINE,
	   A3.CD_ITEM,
	   A3.NM_ITEM,
	   A3.TXT_USERDEF6 AS NO_HULL,
       A3.TXT_USERDEF7,
	   A3.NO_PO_PARTNER,
	   A3.NO_DESIGN1,
	   A3.QT_SO,
	   A3.QT_NOT_GI,
	   A3.AM_WONAMT,
	   A3.CD_USERDEF1 AS NM_CERT,
	   A3.DT_EXPECT,
	   A3.DT_DUEDATE,
	   A3.DT_REQGI,
	   A3.DT_IO,
	   A3.TXT_USERDEF5 AS NM_DELIVERY,
	   A3.DC1,
	   A3.TXT_USERDEF8,
	   A3.NM_ST_WO,
	   MAX([4PP01]) AS DC_4PP01,
	   MAX([4PP02]) AS DC_4PP02,
	   MAX([4PP03]) AS DC_4PP03,
	   MAX([4PP04]) AS DC_4PP04,
	   MAX([4PP05]) AS DC_4PP05,
	   MAX([4PP06]) AS DC_4PP06,
	   MAX([4PP07]) AS DC_4PP07,
	   MAX([4PP08]) AS DC_4PP08,
	   MAX([4PP09]) AS DC_4PP09,
	   MAX([4PP10]) AS DC_4PP10,
	   MAX([4PP11]) AS DC_4PP11,
	   MAX([4PP12]) AS DC_4PP12,
	   MAX([4PP13]) AS DC_4PP13,
	   MAX([4PP14]) AS DC_4PP14,
	   MAX([4PP15]) AS DC_4PP15,
	   MAX([4PP16]) AS DC_4PP16,
	   MAX([4PP17]) AS DC_4PP17,
	   MAX([4PP18]) AS DC_4PP18,
	   MAX([4PP19]) AS DC_4PP19,
	   MAX([4PP20]) AS DC_4PP20,
	   MAX([4PP21]) AS DC_4PP21,
	   MAX([4PP22]) AS DC_4PP22,
	   MAX([4PP23]) AS DC_4PP23,
	   MAX([4PP24]) AS DC_4PP24,
	   MAX([4PP25]) AS DC_4PP25,
	   MAX([VP01]) AS DC_VP01,

	   MAX([4PP011]) AS DC_4PP011,
	   MAX([4PP021]) AS DC_4PP021,
	   MAX([4PP031]) AS DC_4PP031,
	   MAX([4PP041]) AS DC_4PP041,
	   MAX([4PP051]) AS DC_4PP051,
	   MAX([4PP061]) AS DC_4PP061,
	   MAX([4PP071]) AS DC_4PP071,
	   MAX([4PP081]) AS DC_4PP081,
	   MAX([4PP091]) AS DC_4PP091,
	   MAX([4PP101]) AS DC_4PP101,
	   MAX([4PP111]) AS DC_4PP111,
	   MAX([4PP121]) AS DC_4PP121,
	   MAX([4PP131]) AS DC_4PP131,
	   MAX([4PP141]) AS DC_4PP141,
	   MAX([4PP151]) AS DC_4PP151,
	   MAX([4PP161]) AS DC_4PP161,
	   MAX([4PP171]) AS DC_4PP171,
	   MAX([4PP181]) AS DC_4PP181,
	   MAX([4PP191]) AS DC_4PP191,
	   MAX([4PP201]) AS DC_4PP201,
	   MAX([4PP211]) AS DC_4PP211,
	   MAX([4PP221]) AS DC_4PP221,
	   MAX([4PP231]) AS DC_4PP231,
	   MAX([4PP241]) AS DC_4PP241,
	   MAX([4PP251]) AS DC_4PP251,
	   MAX([VP011]) AS DC_VP011,

	   MAX([4PP012]) AS DC_4PP012,
	   MAX([4PP022]) AS DC_4PP022,
	   MAX([4PP032]) AS DC_4PP032,
	   MAX([4PP042]) AS DC_4PP042,
	   MAX([4PP052]) AS DC_4PP052,
	   MAX([4PP062]) AS DC_4PP062,
	   MAX([4PP072]) AS DC_4PP072,
	   MAX([4PP082]) AS DC_4PP082,
	   MAX([4PP092]) AS DC_4PP092,
	   MAX([4PP102]) AS DC_4PP102,
	   MAX([4PP112]) AS DC_4PP112,
	   MAX([4PP122]) AS DC_4PP122,
	   MAX([4PP132]) AS DC_4PP132,
	   MAX([4PP142]) AS DC_4PP142,
	   MAX([4PP152]) AS DC_4PP152,
	   MAX([4PP162]) AS DC_4PP162,
	   MAX([4PP172]) AS DC_4PP172,
	   MAX([4PP182]) AS DC_4PP182,
	   MAX([4PP192]) AS DC_4PP192,
	   MAX([4PP202]) AS DC_4PP202,
	   MAX([4PP212]) AS DC_4PP212,
	   MAX([4PP222]) AS DC_4PP222,
	   MAX([4PP232]) AS DC_4PP232,
	   MAX([4PP242]) AS DC_4PP242,
	   MAX([4PP252]) AS DC_4PP252,
	   MAX([VP012]) AS DC_VP012
FROM (SELECT SH.NO_SO,
			 MP.LN_PARTNER,
	  	     SH.DT_SO,
			 SH.DC_RMK1,
			 SL.SEQ_SO,
			 SL.NO_PO_PARTNER,
	  	     SL.QT_SO,
			 (ISNULL(SL.QT_SO, 0) - ISNULL(OL.QT_IO, 0)) AS QT_NOT_GI,
			 SL.AM_WONAMT,
			 SL.DT_EXPECT,
	         SL.DT_DUEDATE,
	         SL.DT_REQGI,
			 SL.TXT_USERDEF5,
			 SL.TXT_USERDEF6,
             SL.TXT_USERDEF7,
			 SL.CD_USERDEF1,
			 SL.DC1,
			 SL.TXT_USERDEF8,
			 MI.CD_ITEM,
			 MI.NM_ITEM,
	  	     MI.NO_DESIGN AS NO_DESIGN1,
	  	     MI1.NO_DESIGN,
	  	     MI1.GRP_ITEM,
			 MI1.GRP_ITEM + '1' AS GRP_ITEM1,
			 MI1.GRP_ITEM + '2' AS GRP_ITEM2,
			 CD.NM_SYSDEF,
			 OL.DT_IO,
			 (CASE WHEN IV.CD_WC = '완료' THEN '완료' 
				   WHEN IV.YN_REMAIN = 'Y' THEN WC.NM_WC + '(부분)'
				   ELSE WC.NM_WC END) AS NM_WC,
			 ISNULL(IV.DC_RMK, '/') AS DC_RMK,
			 (CASE WHEN GL.QT_PACKING > 0 THEN '포장완료'
			       WHEN QI.QT_NOT_DONE IS NOT NULL AND QI.QT_NOT_DONE = 0 THEN '검사완료'
				   WHEN QI.QT_NOT_DONE IS NOT NULL AND QI.QT_NOT_DONE > 0 THEN '검사신청'
				   WHEN ST.ST_WO = '003' THEN '조립완료'
				   WHEN ST.ST_WO = '002' THEN '현합완료'
				   WHEN ST.ST_WO = '001' THEN '생산완료' END) AS NM_ST_WO
	  FROM SA_SOH SH
	  LEFT JOIN #SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	  LEFT JOIN #BOM BM ON BM.CD_COMPANY = SL.CD_COMPANY AND BM.CD_PLANT = SL.CD_PLANT AND BM.CD_ITEM_T = SL.CD_ITEM
	  LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_PLANT = SL.CD_PLANT AND MI.CD_ITEM = SL.CD_ITEM
	  LEFT JOIN MA_PITEM MI1 ON MI1.CD_COMPANY = BM.CD_COMPANY AND MI1.CD_PLANT = BM.CD_PLANT AND MI1.CD_ITEM = BM.CD_MATL
	  JOIN #ITEM_GRP IG ON IG.CD_COMPANY = MI.CD_COMPANY AND IG.CD_ITEMGRP = MI.GRP_ITEM
	  LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
	  LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = SL.CD_COMPANY AND CD.CD_FIELD = 'CZ_WIN0003' AND CD.CD_SYSDEF = SL.CD_USERDEF3
	  LEFT JOIN #SA_INV IV ON IV.CD_COMPANY = SL.CD_COMPANY AND IV.CD_PLANT = SL.CD_PLANT AND IV.NO_SO = SL.NO_SO AND IV.SEQ_SO = SL.SEQ_SO AND IV.CD_MATL = BM.CD_MATL
	  LEFT JOIN #ST_WO ST ON ST.NO_SO = SL.NO_SO AND ST.SEQ_SO = SL.SEQ_SO
	  LEFT JOIN MA_WC WC ON WC.CD_COMPANY = IV.CD_COMPANY AND WC.CD_PLANT = IV.CD_PLANT AND WC.CD_WC = IV.CD_WC
	  LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO,
					    SUM(CASE WHEN GL.CD_USERDEF1 = 'Y' THEN 1 ELSE 0 END) AS QT_PACKING
			     FROM SA_GIRL GL
				 GROUP BY GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO) GL
	  ON GL.CD_COMPANY = SL.CD_COMPANY AND GL.NO_SO = SL.NO_SO AND GL.SEQ_SO = SL.SEQ_SO
	  LEFT JOIN (SELECT QI.CD_COMPANY, QI.NO_SO,
			     	    SUM(CASE WHEN ISNULL(QI.TP_RESULT, '') = '' THEN 1 ELSE 0 END) AS QT_NOT_DONE
			     FROM CZ_QU_INSP_SCHEDULE QI
			     GROUP BY QI.CD_COMPANY, QI.NO_SO) QI
	  ON QI.CD_COMPANY = SH.CD_COMPANY AND QI.NO_SO = SH.NO_SO
	  LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
				 	    SUM(OL.QT_IO) AS QT_IO,
						MAX(OH.DT_IO) AS DT_IO
				 FROM MM_QTIOH OH
				 JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
				 WHERE OL.FG_PS = '2'
				 AND OL.CD_QTIOTP IN ('200', '210') 
				 GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) OL
	  ON OL.CD_COMPANY = SL.CD_COMPANY AND OL.NO_PSO_MGMT = SL.NO_SO AND OL.NO_PSOLINE_MGMT = SL.SEQ_SO
	  WHERE SH.CD_COMPANY = @P_CD_COMPANY
	  AND MI.TP_PROC = 'M'
	  AND (ISNULL(@P_NO_SO, '') = '' OR SH.NO_SO = @P_NO_SO)
	  AND (ISNULL(@P_CD_PARTNER, '') = '' OR SH.CD_PARTNER = @P_CD_PARTNER)
	  AND (ISNULL(@P_YN_EX_GIR, 'N') = 'N' OR SL.QT_SO > SL.QT_GIR)
	  AND (ISNULL(@P_YN_EX_GI, 'N') = 'N' OR SL.QT_SO > SL.QT_GI)
	  AND (ISNULL(@P_YN_EX_CLOSE, 'N') = 'N' OR ISNULL(SH.STA_SO, '') <> 'C')
	  AND (ISNULL(@P_ST_WO , '') = '' OR (@P_ST_WO = '006' AND GL.QT_PACKING IS NOT NULL AND GL.QT_PACKING > 0)
									  OR (@P_ST_WO = '005' AND QI.QT_NOT_DONE IS NOT NULL AND QI.QT_NOT_DONE = 0)
									  OR (@P_ST_WO = '004' AND QI.QT_NOT_DONE IS NOT NULL AND QI.QT_NOT_DONE > 0)
									  OR ST.ST_WO = @P_ST_WO)) A
PIVOT (MAX(A.NO_DESIGN) FOR A.GRP_ITEM IN ([4PP01],
										   [4PP02],
										   [4PP03],
										   [4PP04],
										   [4PP05],
										   [4PP06],
										   [4PP07],
										   [4PP08],
										   [4PP09],
										   [4PP10],
										   [4PP11],
										   [4PP12],
										   [4PP13],
										   [4PP14],
										   [4PP15],
										   [4PP16],
										   [4PP17],
										   [4PP18],
										   [4PP19],
										   [4PP20],
										   [4PP21],
										   [4PP22],
										   [4PP23],
										   [4PP24],
										   [4PP25],
										   [VP01])) A1
PIVOT (MAX(A1.DC_RMK) FOR A1.GRP_ITEM1 IN ([4PP011],
										  [4PP021],
										  [4PP031],
										  [4PP041],
										  [4PP051],
										  [4PP061],
										  [4PP071],
										  [4PP081],
										  [4PP091],
										  [4PP101],
										  [4PP111],
										  [4PP121],
										  [4PP131],
										  [4PP141],
										  [4PP151],
										  [4PP161],
										  [4PP171],
										  [4PP181],
										  [4PP191],
										  [4PP201],
										  [4PP211],
										  [4PP221],
										  [4PP231],
										  [4PP241],
										  [4PP251],
										  [VP011])) A2
PIVOT (MAX(A2.NM_WC) FOR A2.GRP_ITEM2 IN ([4PP012],
										  [4PP022],
										  [4PP032],
										  [4PP042],
										  [4PP052],
										  [4PP062],
										  [4PP072],
										  [4PP082],
										  [4PP092],
										  [4PP102],
										  [4PP112],
										  [4PP122],
										  [4PP132],
										  [4PP142],
										  [4PP152],
										  [4PP162],
										  [4PP172],
										  [4PP182],
										  [4PP192],
										  [4PP202],
										  [4PP212],
										  [4PP222],
										  [4PP232],
										  [4PP242],
										  [4PP252],
										  [VP012])) A3
GROUP BY A3.NO_SO,
		 A3.SEQ_SO,
		 A3.LN_PARTNER,
	     A3.DT_SO,
		 A3.DC_RMK1,
		 A3.SEQ_SO,
	     A3.NM_SYSDEF,
		 A3.CD_ITEM,
	     A3.NM_ITEM,
	     A3.TXT_USERDEF6,
         A3.TXT_USERDEF7,
	     A3.NO_PO_PARTNER,
	     A3.NO_DESIGN1,
	     A3.QT_SO,
		 A3.QT_NOT_GI,
		 A3.AM_WONAMT,
	     A3.CD_USERDEF1,
	     A3.DT_EXPECT,
	     A3.DT_DUEDATE,
	     A3.DT_REQGI,
	     A3.DT_IO,
	     A3.TXT_USERDEF5,
		 A3.DC1,
		 A3.TXT_USERDEF8,
		 A3.NM_ST_WO
HAVING (ISNULL(@P_NO_DESIGN, '') = '' OR MAX([4PP01]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP02]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP03]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP04]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP05]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP06]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP07]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP08]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP09]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP10]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP11]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP12]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP13]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP14]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP15]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP16]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP17]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP18]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP19]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP20]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP21]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP22]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP23]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP24]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([4PP25]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([VP01]) LIKE @P_NO_DESIGN + '%')
ORDER BY A3.DT_DUEDATE ASC, A3.NO_SO ASC

DROP TABLE #ITEM_GRP
DROP TABLE #BOM
DROP TABLE #STOCK
DROP TABLE #ST_WO
DROP TABLE #SA_INV
DROP TABLE #PR_WO_ROUT
DROP TABLE #SA_SOL

GO
