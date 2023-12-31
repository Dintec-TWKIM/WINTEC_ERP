SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_ITEM_GRP_RPT_VC01_S]
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
	AND IG.CD_ITEMGRP = 'VC01'
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
           SUM(SW.QT_APPLY) OVER (PARTITION BY WO.NO_WO, WR.CD_OP ORDER BY (CASE WHEN SL.QT_GIR > 0 THEN '1' ELSE '2' END), SL.DT_DUEDATE, SL.NO_SO, SL.SEQ_SO) AS QT_TOTAL 
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
	   A4.NO_SO,
	   A4.SEQ_SO,
	   A4.LN_PARTNER,
	   A4.DT_SO,
	   A4.DC_RMK1,
	   A4.NM_SYSDEF AS NM_ENGINE,
	   A4.CD_ITEM,
	   A4.NM_ITEM,
	   A4.TXT_USERDEF6 AS NO_HULL,
       A4.TXT_USERDEF7,
	   A4.NO_PO_PARTNER,
	   A4.NO_DESIGN1,
	   A4.QT_SO,
	   A4.QT_NOT_GI,
	   A4.AM_WONAMT,
	   A4.CD_USERDEF1 AS NM_CERT,
	   A4.DT_EXPECT,
	   A4.DT_DUEDATE,
	   A4.DT_REQGI,
	   A4.DT_IO,
	   A4.TXT_USERDEF5 AS NM_DELIVERY,
	   A4.DC1,
	   A4.TXT_USERDEF8,
	   A4.NM_ST_WO,

	   MAX([VC01]) AS DC_VC01,
	   MAX([2VP01]) AS DC_2VP01,
	   MAX([2VP02]) AS DC_2VP02,
	   MAX([2VP03]) AS DC_2VP03,
	   MAX([2VP04]) AS DC_2VP04,
	   MAX([2VP05]) AS DC_2VP05,
	   MAX([2VP06]) AS DC_2VP06,
	   MAX([2VP07]) AS DC_2VP07,
	   MAX([2VP08]) AS DC_2VP08,
	   MAX([2VP09]) AS DC_2VP09,
	   MAX([2VP10]) AS DC_2VP10,
	   MAX([2VP12]) AS DC_2VP12,
	   MAX([2VP13]) AS DC_2VP13,
	   MAX([2VP14]) AS DC_2VP14,
	   MAX([2VP15]) AS DC_2VP15,
	   MAX([2VP21]) AS DC_2VP21,
	   MAX([2VP23]) AS DC_2VP23,
	   MAX([2VP24]) AS DC_2VP24,
	   MAX([2VS01]) AS DC_2VS01,
	   MAX([2VS02]) AS DC_2VS02,
	   MAX([2VS03]) AS DC_2VS03,
	   MAX([2VS04]) AS DC_2VS04,
	   MAX([2VS05]) AS DC_2VS05,
	   MAX([2VS06]) AS DC_2VS06,
	   MAX([2VS07]) AS DC_2VS07,
	   MAX([2VSM]) AS DC_2VSM,
	   MAX([2VP16]) AS DC_2VP16,
	   MAX([2VP17]) AS DC_2VP17,
	   MAX([2VP18]) AS DC_2VP18,
	   MAX([2VP19]) AS DC_2VP19,
	   MAX([2VP20]) AS DC_2VP20,
	   MAX([2VP22]) AS DC_2VP22,
	   MAX([2VP25]) AS DC_2VP25,
	   MAX([2VP26]) AS DC_2VP26,
	   MAX([2VP27]) AS DC_2VP27,
	   MAX([2VP28]) AS DC_2VP28,

	   MAX([VC011]) AS DC_VC011,
	   MAX([2VP011]) AS DC_2VP011,
	   MAX([2VP021]) AS DC_2VP021,
	   MAX([2VP031]) AS DC_2VP031,
	   MAX([2VP041]) AS DC_2VP041,
	   MAX([2VP051]) AS DC_2VP051,
	   MAX([2VP061]) AS DC_2VP061,
	   MAX([2VP071]) AS DC_2VP071,
	   MAX([2VP081]) AS DC_2VP081,
	   MAX([2VP091]) AS DC_2VP091,
	   MAX([2VP101]) AS DC_2VP101,
	   MAX([2VP121]) AS DC_2VP121,
	   MAX([2VP131]) AS DC_2VP131,
	   MAX([2VP141]) AS DC_2VP141,
	   MAX([2VP151]) AS DC_2VP151,
	   MAX([2VP211]) AS DC_2VP211,
	   MAX([2VP231]) AS DC_2VP231,
	   MAX([2VP241]) AS DC_2VP241,
	   MAX([2VS011]) AS DC_2VS011,
	   MAX([2VS021]) AS DC_2VS021,
	   MAX([2VS031]) AS DC_2VS031,
	   MAX([2VS041]) AS DC_2VS041,
	   MAX([2VS051]) AS DC_2VS051,
	   MAX([2VS061]) AS DC_2VS061,
	   MAX([2VS071]) AS DC_2VS071,
	   MAX([2VSM1]) AS DC_2VSM1,
	   MAX([2VP161]) AS DC_2VP161,
	   MAX([2VP171]) AS DC_2VP171,
	   MAX([2VP181]) AS DC_2VP181,
	   MAX([2VP191]) AS DC_2VP191,
	   MAX([2VP201]) AS DC_2VP201,
	   MAX([2VP221]) AS DC_2VP221,
	   MAX([2VP251]) AS DC_2VP251,
	   MAX([2VP261]) AS DC_2VP261,
	   MAX([2VP271]) AS DC_2VP271,
	   MAX([2VP281]) AS DC_2VP281,

	   MAX([VC012]) AS DC_VC012,
	   MAX([2VP012]) AS DC_2VP012,
	   MAX([2VP022]) AS DC_2VP022,
	   MAX([2VP032]) AS DC_2VP032,
	   MAX([2VP042]) AS DC_2VP042,
	   MAX([2VP052]) AS DC_2VP052,
	   MAX([2VP062]) AS DC_2VP062,
	   MAX([2VP072]) AS DC_2VP072,
	   MAX([2VP082]) AS DC_2VP082,
	   MAX([2VP092]) AS DC_2VP092,
	   MAX([2VP102]) AS DC_2VP102,
	   MAX([2VP122]) AS DC_2VP122,
	   MAX([2VP132]) AS DC_2VP132,
	   MAX([2VP142]) AS DC_2VP142,
	   MAX([2VP152]) AS DC_2VP152,
	   MAX([2VP212]) AS DC_2VP212,
	   MAX([2VP232]) AS DC_2VP232,
	   MAX([2VP242]) AS DC_2VP242,
	   MAX([2VS012]) AS DC_2VS012,
	   MAX([2VS022]) AS DC_2VS022,
	   MAX([2VS032]) AS DC_2VS032,
	   MAX([2VS042]) AS DC_2VS042,
	   MAX([2VS052]) AS DC_2VS052,
	   MAX([2VS062]) AS DC_2VS062,
	   MAX([2VS072]) AS DC_2VS072,
	   MAX([2VSM2]) AS DC_2VSM2,
	   MAX([2VP162]) AS DC_2VP162,
	   MAX([2VP172]) AS DC_2VP172,
	   MAX([2VP182]) AS DC_2VP182,
	   MAX([2VP192]) AS DC_2VP192,
	   MAX([2VP202]) AS DC_2VP202,
	   MAX([2VP222]) AS DC_2VP222,
	   MAX([2VP252]) AS DC_2VP252,
	   MAX([2VP262]) AS DC_2VP262,
	   MAX([2VP272]) AS DC_2VP272,
	   MAX([2VP282]) AS DC_2VP282,

	   MAX([2VP203]) AS DC_2VP203
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
			 MI1.STND_ITEM,
	  	     MI1.GRP_ITEM,
			 MI1.GRP_ITEM + '1' AS GRP_ITEM1,
			 MI1.GRP_ITEM + '2' AS GRP_ITEM2,
			 MI1.GRP_ITEM + '3' AS GRP_ITEM3,
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
	  JOIN #SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
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
PIVOT (MAX(A.NO_DESIGN) FOR A.GRP_ITEM IN ([VC01],
										   [2VP01],
										   [2VP02],
										   [2VP03],
										   [2VP04],
										   [2VP05],
										   [2VP06],
										   [2VP07],
										   [2VP08],
										   [2VP09],
										   [2VP10],
										   [2VP12],
										   [2VP13],
										   [2VP14],
										   [2VP15],
										   [2VP21],
										   [2VP23],
										   [2VP24],
										   [2VS01],
										   [2VS02],
										   [2VS03],
										   [2VS04],
										   [2VS05],
										   [2VS06],
										   [2VS07],
										   [2VSM],
										   [2VP16],
										   [2VP17],
										   [2VP18],
										   [2VP19],
										   [2VP20],
										   [2VP22],
										   [2VP25],
										   [2VP26],
										   [2VP27],
										   [2VP28])) A1
PIVOT (MAX(A1.DC_RMK) FOR A1.GRP_ITEM1 IN ([VC011],
										   [2VP011],
										   [2VP021],
										   [2VP031],
										   [2VP041],
										   [2VP051],
										   [2VP061],
										   [2VP071],
										   [2VP081],
										   [2VP091],
										   [2VP101],
										   [2VP121],
										   [2VP131],
										   [2VP141],
										   [2VP151],
										   [2VP211],
										   [2VP231],
										   [2VP241],
										   [2VS011],
										   [2VS021],
										   [2VS031],
										   [2VS041],
										   [2VS051],
										   [2VS061],
										   [2VS071],
										   [2VSM1],
										   [2VP161],
										   [2VP171],
										   [2VP181],
										   [2VP191],
										   [2VP201],
										   [2VP221],
										   [2VP251],
										   [2VP261],
										   [2VP271],
										   [2VP281])) A2
PIVOT (MAX(A2.NM_WC) FOR A2.GRP_ITEM2 IN ([VC012],
										  [2VP012],
										  [2VP022],
										  [2VP032],
										  [2VP042],
										  [2VP052],
										  [2VP062],
										  [2VP072],
										  [2VP082],
										  [2VP092],
										  [2VP102],
										  [2VP122],
										  [2VP132],
										  [2VP142],
										  [2VP152],
										  [2VP212],
										  [2VP232],
										  [2VP242],
										  [2VS012],
										  [2VS022],
										  [2VS032],
										  [2VS042],
										  [2VS052],
										  [2VS062],
										  [2VS072],
										  [2VSM2],
										  [2VP162],
										  [2VP172],
										  [2VP182],
										  [2VP192],
										  [2VP202],
										  [2VP222],
										  [2VP252],
										  [2VP262],
										  [2VP272],
										  [2VP282])) A3
PIVOT (MAX(A3.STND_ITEM) FOR A3.GRP_ITEM3 IN ([2VP203])) A4
GROUP BY A4.NO_SO,
		 A4.SEQ_SO,
	     A4.LN_PARTNER,
	     A4.DT_SO,
		 A4.DC_RMK1,
		 A4.SEQ_SO,
	     A4.NM_SYSDEF,
		 A4.CD_ITEM,
	     A4.NM_ITEM,
	     A4.TXT_USERDEF6,
         A4.TXT_USERDEF7,
	     A4.NO_PO_PARTNER,
	     A4.NO_DESIGN1,
	     A4.QT_SO,
		 A4.QT_NOT_GI,
		 A4.AM_WONAMT,
	     A4.CD_USERDEF1,
		 A4.DT_EXPECT,
	     A4.DT_DUEDATE,
	     A4.DT_REQGI,
	     A4.DT_IO,
	     A4.TXT_USERDEF5,
		 A4.DC1,
		 A4.TXT_USERDEF8,
		 A4.NM_ST_WO
HAVING (ISNULL(@P_NO_DESIGN, '') = '' OR MAX([VC01]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP01]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP02]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP03]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP04]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP05]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP06]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP07]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP08]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP09]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP10]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP12]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP13]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP14]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP15]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP21]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP23]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP24]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VS01]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VS02]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VS03]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VS04]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VS05]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VS06]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VS07]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VSM]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP16]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP17]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP18]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP19]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP20]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP22]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP25]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP26]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP27]) LIKE @P_NO_DESIGN + '%' OR
										 MAX([2VP28]) LIKE @P_NO_DESIGN + '%')
ORDER BY A4.DT_DUEDATE ASC, A4.NO_SO ASC

DROP TABLE #ITEM_GRP
DROP TABLE #BOM
DROP TABLE #STOCK
DROP TABLE #ST_WO
DROP TABLE #SA_INV
DROP TABLE #PR_WO_ROUT
DROP TABLE #SA_SOL

GO
