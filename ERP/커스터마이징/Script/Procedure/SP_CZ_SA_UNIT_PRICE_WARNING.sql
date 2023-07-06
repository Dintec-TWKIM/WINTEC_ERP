USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_UNIT_PRICE_WARNING]    Script Date: 2020-12-15 오후 2:03:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_SA_UNIT_PRICE_WARNING]
(
	@P_CD_COMPANY	NVARCHAR(7)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_SUBJECT NVARCHAR(50)
DECLARE @V_HTML NVARCHAR(MAX)
DECLARE @V_BODY	NVARCHAR(MAX) = ''

SELECT @V_SUBJECT = '단가변동감지 보고서 (' + CONVERT(CHAR(10), DATEADD(DAY, -1, GETDATE()), 102) + ')'

CREATE TABLE #SA_SOL
(
	CD_ITEM		NVARCHAR(20),
	CD_SPEC		NVARCHAR(20), 
	NO_DRAWING	NVARCHAR(20), 
	QT_SO		NUMERIC(17, 4)
)

CREATE NONCLUSTERED INDEX SA_SOL1 ON #SA_SOL (CD_ITEM)
CREATE NONCLUSTERED INDEX SA_SOL2 ON #SA_SOL (CD_SPEC)
CREATE NONCLUSTERED INDEX SA_SOL3 ON #SA_SOL (NO_DRAWING)

CREATE TABLE #MA_PITEM
(
	CD_SPEC		NVARCHAR(1000),
	QT_ITEM		INT,
	CD_ITEM		NVARCHAR(20)
)

CREATE NONCLUSTERED INDEX MA_PITEM ON #MA_PITEM (CD_SPEC)

INSERT INTO #SA_SOL
(
	CD_ITEM,
	CD_SPEC,
	NO_DRAWING,
	QT_SO
)
SELECT SL.CD_ITEM,
	   QL.CD_SPEC,
	   QL.NO_DRAWING,
	   SUM(SL.QT_SO) AS QT_SO
FROM CZ_SA_QTNH QH
JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
JOIN CZ_PU_QTNL QL1 ON QL1.CD_COMPANY = QL.CD_COMPANY AND QL1.NO_FILE = QL.NO_FILE AND QL1.NO_LINE = QL.NO_LINE AND QL1.CD_PARTNER = '11823'
JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
WHERE QH.CD_COMPANY = @P_CD_COMPANY
AND QH.DT_INQ BETWEEN CONVERT(NVARCHAR(8), DATEADD(MONTH, -2, GETDATE()), 112) AND CONVERT(NVARCHAR(8), GETDATE(), 112)
AND ISNULL(QL.YN_SERIES, 'N') <> 'Y'
AND ISNULL(QL.YN_DUP, 'N') <> 'Y'
AND ISNULL(QL.YN_OUTLIER, 'N') <> 'Y'
AND LEFT(QH.NO_FILE, 2) IN ('DB', 'DS', 'FB', 'NB', 'NS', 'SB', 'A-', 'D-', 'N-')
AND NOT EXISTS (SELECT 1 
				FROM CZ_MA_CODEDTL MC 
				WHERE MC.CD_COMPANY = QH.CD_COMPANY 
				AND MC.CD_FIELD = 'CZ_DX00011' 
				AND MC.CD_FLAG1 = QH.CD_PARTNER 
				AND MC.CD_FLAG2 = ISNULL(QL.CD_SUPPLIER, QL1.CD_PARTNER))
GROUP BY SL.CD_ITEM, QL.CD_SPEC, QL.NO_DRAWING

INSERT INTO #MA_PITEM
(
	CD_SPEC,
	QT_ITEM,
	CD_ITEM
)
SELECT MI.CD_SPEC,
	   COUNT(DISTINCT MI.CD_ITEM) AS QT_ITEM,
	   MAX(MI.CD_ITEM) AS CD_ITEM
FROM (SELECT A.value AS CD_SPEC,
	  	     MI.CD_ITEM
	  FROM MA_PITEM MI
	  CROSS APPLY string_split(MI.STND_DETAIL_ITEM, ',') A
	  WHERE MI.CD_COMPANY = @P_CD_COMPANY
	  AND ISNULL(MI.STND_DETAIL_ITEM, '') <> ''
	  AND MI.YN_USE = 'Y'
	  UNION
	  SELECT A.value AS CD_SPEC,
	  	     MI.CD_ITEM
	  FROM MA_PITEM MI
	  CROSS APPLY string_split(MI.UCODE2, ',') A
	  WHERE MI.CD_COMPANY = @P_CD_COMPANY
	  AND ISNULL(MI.UCODE2, '') <> ''
	  AND MI.YN_USE = 'Y') MI
GROUP BY MI.CD_SPEC

DELETE PW
FROM CZ_SA_UNIT_PRICE_WARNING PW
WHERE PW.CD_COMPANY = @P_CD_COMPANY
AND PW.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)

;WITH A AS
(
	SELECT QH.CD_COMPANY,
		   ROW_NUMBER() OVER (PARTITION BY QL.CD_SPEC ORDER BY ISNULL(QH1.DT_QTN, QH.DT_QTN) DESC, QL.NO_FILE, QL.NO_LINE) AS IDX, 
		   QH.NO_FILE,
		   ISNULL(QH1.DT_QTN, QH.DT_QTN) AS DT_QTN,
		   QL.NO_LINE,
		   QL.CD_ITEM,
		   QL.CD_SPEC,
		   QL1.UM_KR,
		   HE.NM_MODEL
	FROM CZ_SA_QTNH QH
	JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
	JOIN CZ_PU_QTNL QL1 ON QL1.CD_COMPANY = QL.CD_COMPANY AND QL1.NO_FILE = QL.NO_FILE AND QL1.NO_LINE = QL.NO_LINE AND QL1.CD_PARTNER = '11823'
	JOIN CZ_PU_QTNH QH1 ON QH1.CD_COMPANY = QL1.CD_COMPANY AND QH1.NO_FILE = QL1.NO_FILE AND QH1.CD_PARTNER = QL1.CD_PARTNER
	LEFT JOIN CZ_MA_HULL_ENGINE HE ON HE.NO_IMO = QH.NO_IMO AND HE.NO_ENGINE = QL.NO_ENGINE
	WHERE QH.CD_COMPANY = @P_CD_COMPANY
	AND ISNULL(QH1.DT_QTN, QH.DT_QTN) BETWEEN CONVERT(NVARCHAR(8), DATEADD(MONTH, -2, GETDATE()), 112) AND CONVERT(NVARCHAR(8), GETDATE(), 112)
	AND ISNULL(QL1.UM_KR, 0) > 0
	AND ISNULL(QL.CD_SPEC, '') <> ''
	AND ISNULL(QL.YN_OUTLIER, 'N') <> 'Y'
	AND NOT EXISTS (SELECT 1 
					FROM CZ_MA_CODEDTL MC 
					WHERE MC.CD_COMPANY = QH.CD_COMPANY 
					AND MC.CD_FIELD = 'CZ_DX00011' 
					AND MC.CD_FLAG1 = QH.CD_PARTNER 
					AND MC.CD_FLAG2 = ISNULL(QL.CD_SUPPLIER, QL1.CD_PARTNER))
	AND EXISTS (SELECT 1
				FROM CZ_SA_QTNH QH1
				JOIN CZ_SA_QTNL QL1 ON QL1.CD_COMPANY = QH1.CD_COMPANY AND QL1.NO_FILE = QH1.NO_FILE
				WHERE QH1.CD_COMPANY = QL.CD_COMPANY
				AND QH1.DT_INQ BETWEEN CONVERT(NVARCHAR(8), DATEADD(MONTH, -2, GETDATE()), 112) AND CONVERT(NVARCHAR(8), GETDATE(), 112)
				AND QH1.NO_FILE NOT LIKE 'ZB%'
				AND QL1.CD_SPEC = QL.CD_SPEC
				GROUP BY QL1.CD_SPEC
				HAVING COUNT(1) >= 5)
),
B AS
(
	SELECT *,
		   SUM(LOG10(1.0 * A.UM_KR)) OVER (PARTITION BY A.CD_SPEC ORDER BY A.IDX DESC) AS TEMP, 
		   COUNT(A.UM_KR) OVER (PARTITION BY A.CD_SPEC ORDER BY A.IDX DESC) AS CNT,
		   ROUND(STDEVP(A.UM_KR) OVER (PARTITION BY A.CD_SPEC ORDER BY A.IDX DESC), 0) AS SD
	FROM A
),
C AS
(
	SELECT *,
		   ROUND(POWER(10.000000, B.TEMP / B.CNT), 0) AS GEOMEAN
	FROM B
),
D AS
(
	SELECT *,
		   ROUND(C.GEOMEAN - (2.5 * C.SD), 0) AS GEOMEAN1,
		   ROUND(C.GEOMEAN + (2.5 * C.SD), 0) AS GEOMEAN2
    FROM C
),
E AS
(
	SELECT *,
	       (CASE WHEN D.UM_KR < D.GEOMEAN1 THEN '▼'
				 WHEN D.UM_KR > D.GEOMEAN2 THEN '▲'
				 ELSE '-' END) AS TP_PLUS,
	       (CASE WHEN D.UM_KR >= D.GEOMEAN1 AND D.UM_KR <= D.GEOMEAN2 THEN 'N' ELSE 'Y' END) AS YN_SEND
    FROM D
),
F AS
(
	SELECT E.CD_COMPANY,
		   E.CD_SPEC,
		   E.DT_QTN,
		   E.NO_FILE,
		   E.NO_LINE,
		   E.CD_ITEM,
		   E.NM_MODEL,
		   E.UM_KR,
		   E.GEOMEAN,
		   E.SD,
		   E.GEOMEAN1,
		   E.GEOMEAN2,
		   E.TP_PLUS,
		   (ISNULL(SQ.QT_AVST, 0) + ISNULL(SQ.QT_AVPO, 0)) AS QT_INV
	FROM E
	LEFT JOIN CZ_DX_STOCK_QT SQ ON SQ.CD_COMPANY = @P_CD_COMPANY AND SQ.CD_ITEM = E.CD_ITEM
	WHERE E.IDX = 1
	AND E.YN_SEND = 'Y'
	AND E.DT_QTN >= CONVERT(CHAR(8), DATEADD(DAY, -1, GETDATE()), 112)
),
G AS
(
	SELECT F.*,
		   MI1.STAND_PRC,
		   (CASE WHEN MI.QT_ITEM > 1 OR ISNULL(F.CD_ITEM, '') <> ISNULL(MI.CD_ITEM, '') THEN 'Y' ELSE 'N' END) AS CHK_ITEM,
		   (CASE WHEN ISNULL(MI1.STND_DETAIL_ITEM, '') <> '' AND MI1.STND_DETAIL_ITEM <> F.CD_SPEC THEN 'Y'
				 WHEN ISNULL(MI1.UCODE2, '') <> '' AND MI1.UCODE2 <> F.CD_SPEC THEN 'Y'
				 ELSE '' END) AS CHK_UCODE,
		   (SELECT MAX(A.QT_SO) 
			FROM (SELECT SUM(SL.QT_SO) AS QT_SO  
				  FROM #SA_SOL SL
				  WHERE SL.CD_ITEM = MI.CD_ITEM
				  AND ISNULL(SL.CD_ITEM, '') <> ''
				  UNION ALL
				  SELECT SUM(SL.QT_SO) AS QT_SO  
				  FROM #SA_SOL SL
				  WHERE SL.CD_SPEC = F.CD_SPEC
				  AND ISNULL(SL.CD_SPEC, '') <> ''
				  UNION ALL
				  SELECT SUM(SL.QT_SO) AS QT_SO  
				  FROM #SA_SOL SL
				  WHERE SL.CD_SPEC = (CASE WHEN F.CD_SPEC = MI1.STND_DETAIL_ITEM THEN MI1.UCODE2 ELSE MI1.STND_DETAIL_ITEM END)
				  AND ISNULL(SL.CD_SPEC, '') <> ''
				  UNION ALL
				  SELECT SUM(SL.QT_SO) AS QT_SO  
				  FROM #SA_SOL SL
				  WHERE SL.NO_DRAWING = EI.EZCODE
				  AND ISNULL(SL.NO_DRAWING, '') <> '') A) AS QT_SO
	FROM F
	LEFT JOIN #MA_PITEM MI ON MI.CD_SPEC = F.CD_SPEC
	LEFT JOIN MA_PITEM MI1 ON MI1.CD_COMPANY = F.CD_COMPANY AND MI1.CD_ITEM = MI.CD_ITEM
	LEFT JOIN CZ_EZCODE_PITEM EI ON EI.CD_COMPANY = MI1.CD_COMPANY AND EI.CD_ITEM = MI1.CD_ITEM
)

INSERT INTO CZ_SA_UNIT_PRICE_WARNING
(
	CD_COMPANY,
	DT_SEND,
	CD_SPEC,
	DT_QTN,
	NO_FILE,
	NO_LINE,
	CD_ITEM,
	NM_MODEL,
	STAND_PRC,
	UM_KR,
	GEOMEAN,
	SD,
	GEOMEAN1,
	GEOMEAN2,
	TP_PLUS,
	QT_INV,
	QT_SO,
	DT_STOCK_MONTH,
	YN_STOCK_PO,
  CHK_UCODE,
  CHK_ITEM,
	ID_INSERT,
	DTS_INSERT
)
SELECT @P_CD_COMPANY AS CD_COMPANY,
	   CONVERT(CHAR(8), GETDATE(), 112) AS DT_SEND,
	   G.CD_SPEC,
	   G.DT_QTN,
	   G.NO_FILE,
	   G.NO_LINE,
	   G.CD_ITEM,
	   G.NM_MODEL,
	   G.STAND_PRC,
	   G.UM_KR,
	   G.GEOMEAN,
	   G.SD,
	   G.GEOMEAN1,
	   G.GEOMEAN2,
	   G.TP_PLUS,
	   G.QT_INV,
	   ISNULL(G.QT_SO, 0) AS QT_SO,
	   (CASE WHEN ISNULL(G.QT_SO, 0) = 0 THEN NULL ELSE CONVERT(NVARCHAR, CONVERT(NUMERIC, ROUND((ISNULL(G.QT_INV, 0) / ISNULL(G.QT_SO, 0)) * 12, 0))) + 'M' END) AS DT_STOCK_MONTH,
       (CASE WHEN EXISTS (SELECT 1 
	   				      FROM PU_POL PL
	   				      WHERE PL.CD_COMPANY = @P_CD_COMPANY
	   				      AND PL.NO_PO LIKE '%-ST'
	   				      AND PL.CD_ITEM = G.CD_ITEM
	   				      AND PL.QT_PO > PL.QT_RCV) THEN 'Y' ELSE 'N' END) AS YN_STOCK_PO,
	   G.CHK_UCODE,
	   G.CHK_ITEM,
	   'SYSTEM' AS ID_INSERT,
	   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
FROM G

SELECT @V_BODY = @V_BODY + '<tr style="height:30px">' +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + (CASE WHEN EXISTS (SELECT 1 
																																												                 FROM CZ_SA_UNIT_PRICE_WARNING PW1
																																																 WHERE PW1.CD_COMPANY = PW.CD_COMPANY
																																																 AND PW1.CD_SPEC = PW.CD_SPEC
																																																 AND PW1.TP_PLUS = PW.TP_PLUS
																																																 AND PW1.DT_SEND < CONVERT(CHAR(8), GETDATE(), 112)) THEN 'Old' ELSE 'New' END) + '</th>' +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + ISNULL(CONVERT(CHAR(10), CONVERT(DATETIME, PW.DT_QTN), 111), '') + '</th>' +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + ISNULL(PW.NO_FILE, '') + '</th>' +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + ISNULL(PW.NM_MODEL, '') + '</th>' +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + ISNULL(PW.CD_SPEC, '') + '</th>' +
	   (CASE WHEN PW.CHK_ITEM = 'Y' THEN '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + '확인요' + '</th>' +
										 '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + '확인요' + '</th>' +
										 '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + '확인요' + '</th>'
								    ELSE '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:right; padding-right:10px; font-weight:normal">' + ISNULL(CONVERT(NVARCHAR, FORMAT(PW.QT_INV, '#,##0')),'') + '</th>' +
										 '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:right; padding-right:10px; font-weight:normal">' + ISNULL(CONVERT(NVARCHAR, FORMAT(PW.QT_SO, '#,##0')),'') + '</th>' +
										 '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + ISNULL(PW.DT_STOCK_MONTH, '') + '</th>' END) +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + ISNULL(PW.CD_ITEM, '') + '</th>' +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:right; padding-right:10px; font-weight:normal">' + ISNULL(CONVERT(NVARCHAR, FORMAT(PW.STAND_PRC, '#,##0')),'') + '</th>' +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:right; padding-right:10px; font-weight:normal">' + ISNULL(CONVERT(NVARCHAR, FORMAT(PW.UM_KR, '#,##0')), '') + '</th>' +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + ISNULL(PW.TP_PLUS, '') + '</th>' +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + ISNULL(PW.YN_STOCK_PO, 'N') + '</th>' +
	   '<th style="border:solid 1px black; ' + (CASE WHEN PW.TP_PLUS = '▲' THEN 'color: #FF0000;' ELSE 'color: #0000FF;' END) + ' text-align:center; font-weight:normal">' + ISNULL(PW.CHK_UCODE, 'N') + '</th>' +
	   '</tr>'
FROM CZ_SA_UNIT_PRICE_WARNING PW
WHERE PW.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
ORDER BY RIGHT(PW.NM_MODEL, 6), PW.NO_FILE, PW.DT_QTN DESC

SET @V_HTML = '<div style="text-align:left; font-weight: bold;">*** 단가 변동 리스트</div>

			   <table style="width:1000px; border:2px solid black; font-size: 9pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;">
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<colgroup width="120px" align="center"></colgroup>
				<tbody>
				 <tr style="height:30px">
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">신규여부</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">견적일자</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">파일번호</th>                                    
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">엔진모델</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">U 코드</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">재고수량</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">1년수주수량</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">재고보유개월</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">재고코드</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">재고단가</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">매입단가</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">증감(매입)</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">일반발주여부</th>
				  <th style="border:solid 1px black; text-align:center; background-color:Silver">U코드확인</th>
				 </tr>'
				 + @V_BODY +
			   '</tbody>
			   </table>'

EXEC msdb.dbo.sp_send_dbmail @PROFILE_NAME = 'DINTEC MAIL',	
							 @RECIPIENTS = 'stock@dintec.co.kr;khkim@dintec.co.kr',
							 @SUBJECT = @V_SUBJECT,	
							 @BODY = @V_HTML,	
							 @BODY_FORMAT = 'HTML'

GO