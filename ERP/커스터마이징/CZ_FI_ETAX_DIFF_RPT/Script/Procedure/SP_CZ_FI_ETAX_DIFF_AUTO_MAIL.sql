USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_ETAX_DIFF_AUTO_MAIL]    Script Date: 2020-12-15 오후 2:03:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_FI_ETAX_DIFF_AUTO_MAIL]
(
	@P_CD_COMPANY	NVARCHAR(7)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_DT_MONTH NVARCHAR(6) = CONVERT(CHAR(6), DATEADD(MONTH, -1, GETDATE()), 112)

DECLARE @V_BUY_DAM_EMAIL NVARCHAR(40),
        @V_BUY_DAM_EMAIL2 NVARCHAR(40)

CREATE TABLE #FI_ETAX
(
	NO_ETAX         NVARCHAR(50),
    DT_START        NCHAR(8),
    NO_COMPANY1     NVARCHAR(200),
    NM_COMPANY1     NVARCHAR(100),
    AM_SUM          NUMERIC(19, 0),
    AM_TAXSTD       NUMERIC(19, 0),
    AM_ADDTAX       NUMERIC(19, 0),
    TP_TAX          NVARCHAR(20),
    NM_BIGO         NVARCHAR(100),
    BUY_DAM_EMAIL   NVARCHAR(40),
    BUY_DAM_EMAIL2  NVARCHAR(40),
    NM_ITEM         NVARCHAR(50)
)

CREATE NONCLUSTERED INDEX FI_ETAX ON #FI_ETAX (BUY_DAM_EMAIL, BUY_DAM_EMAIL2)

;WITH A1 AS
(
    SELECT EX.NO_COMPANY1,
           EX.DT_START,
           COUNT(1) AS CNT,
           SUM(EX.AM_TAXSTD) AS AM_TAXSTD,
           SUM(EX.AM_ADDTAX) AS AM_ADDTAX
    FROM FI_ETAX EX
    WHERE EX.CD_COMPANY = @P_CD_COMPANY
    AND EX.DT_START LIKE @V_DT_MONTH + '%'
    AND EX.TP_GUBUN = '2'
    GROUP BY EX.NO_COMPANY1, EX.DT_START
),
B1 AS
(
    SELECT TX.ID_UPDATE,
           TX.DT_START,
           COUNT(1) AS CNT,
           SUM(TX.AM_TAXSTD) AS AM_TAXSTD,
           SUM(TX.AM_ADDTAX) AS AM_ADDTAX
    FROM FI_TAX TX
    WHERE TX.CD_COMPANY = @P_CD_COMPANY
    AND TX.DT_START LIKE @V_DT_MONTH + '%'
    AND TX.TP_TAX IN ('21', '22', '23', '38', '43', '69')
    GROUP BY TX.ID_UPDATE, TX.DT_START
),
C1 AS
(
    SELECT A.NO_COMPANY1,
           A.DT_START,
           A.CNT AS CNT1,
           A.AM_TAXSTD AS AM_TAXSTD1,
           A.AM_ADDTAX AS AM_ADDTAX1,
           B.ID_UPDATE AS NO_COMPANY2,
           B.CNT AS CNT2,
           B.AM_TAXSTD AS AM_TAXSTD2,
           B.AM_ADDTAX AS AM_ADDTAX2,
           (A.CNT - B.CNT) AS CNT_DIFF,
           (A.AM_TAXSTD - B.AM_TAXSTD) AS AM_TAXSTD_DIFF,
           (A.AM_ADDTAX - B.AM_ADDTAX) AS AM_ADDTAX_DIFF
    FROM A1 A
    LEFT JOIN B1 B ON B.ID_UPDATE = A.NO_COMPANY1 AND B.DT_START = A.DT_START
    WHERE (ISNULL(A.AM_TAXSTD, 0) <> ISNULL(B.AM_TAXSTD, 0) OR ISNULL(A.AM_ADDTAX, 0) <> ISNULL(B.AM_ADDTAX, 0))
),
D1 AS
(
    SELECT EX.NO_ETAX,
           EX.DT_START,
           EX.NO_COMPANY1,
           EX.NM_COMPANY1,
           EX.AM_SUM,
           EX.AM_TAXSTD,
           EX.AM_ADDTAX,
           EX.TP_TAX,
           EX.NM_BIGO,
           EX.BUY_DAM_EMAIL,
           EX.BUY_DAM_EMAIL2,
           EX.NM_ITEM,
           ROW_NUMBER() OVER (PARTITION BY EX.NO_COMPANY1, EX.DT_START, EX.AM_TAXSTD, EX.AM_ADDTAX ORDER BY EX.DT_START) AS IDX
    FROM FI_ETAX EX
    WHERE EX.CD_COMPANY = @P_CD_COMPANY
    AND EX.DT_START LIKE @V_DT_MONTH + '%'
    AND EX.TP_GUBUN = '2'
    AND EXISTS (SELECT 1 
                FROM C1 C 
                WHERE C.NO_COMPANY1 = EX.NO_COMPANY1
                AND C.DT_START = EX.DT_START)
),
E1 AS
(
    SELECT D.NO_ETAX,
           D.DT_START,
           D.NO_COMPANY1,
           D.NM_COMPANY1,
           D.AM_SUM,
           D.AM_TAXSTD,
           D.AM_ADDTAX,
           D.TP_TAX,
           D.NM_BIGO,
           D.BUY_DAM_EMAIL,
           D.BUY_DAM_EMAIL2,
           D.NM_ITEM
    FROM D1 D
    LEFT JOIN (SELECT TX.ID_UPDATE,
                      TX.DT_START,
                      TX.AM_TAXSTD,
                      TX.AM_ADDTAX,
                      ROW_NUMBER() OVER (PARTITION BY TX.ID_UPDATE, TX.DT_START, TX.AM_TAXSTD, TX.AM_ADDTAX ORDER BY TX.DT_START) AS IDX
               FROM FI_TAX TX
               WHERE TX.CD_COMPANY = @P_CD_COMPANY
               AND TX.DT_START LIKE @V_DT_MONTH + '%'
               AND TX.TP_TAX IN ('21', '22', '23', '38', '43', '69')) TX
    ON TX.ID_UPDATE = D.NO_COMPANY1 AND TX.DT_START = D.DT_START AND TX.AM_TAXSTD = D.AM_TAXSTD AND TX.AM_ADDTAX = D.AM_ADDTAX AND TX.IDX = D.IDX
    WHERE TX.IDX IS NULL
),
A2 AS
(
    SELECT EX.NO_COMPANY1,
           EX.DT_START,
           COUNT(1) AS CNT,
           SUM(EX.AM_TAXSTD) AS AM_TAXSTD,
           SUM(EX.AM_ADDTAX) AS AM_ADDTAX
    FROM FI_ETAX2 EX
    WHERE EX.CD_COMPANY = @P_CD_COMPANY
    AND EX.DT_START LIKE @V_DT_MONTH + '%'
    AND EX.TP_GUBUN = '2'
    GROUP BY EX.NO_COMPANY1, EX.DT_START
),
B2 AS
(
    SELECT TX.ID_UPDATE,
           TX.DT_START,
           COUNT(1) AS CNT,
           SUM(TX.AM_TAXSTD) AS AM_TAXSTD,
           SUM(TX.AM_ADDTAX) AS AM_ADDTAX
    FROM FI_TAX TX
    WHERE TX.CD_COMPANY = @P_CD_COMPANY
    AND TX.DT_START LIKE @V_DT_MONTH + '%'
    AND TX.TP_TAX IN ('26','27','29','32','40','44','46','48','51','54','61','63','65','67')
    GROUP BY TX.ID_UPDATE, TX.DT_START
),
C2 AS
(
    SELECT A.NO_COMPANY1,
           A.DT_START,
           A.CNT AS CNT1,
           A.AM_TAXSTD AS AM_TAXSTD1,
           A.AM_ADDTAX AS AM_ADDTAX1,
           B.ID_UPDATE AS NO_COMPANY2,
           B.CNT AS CNT2,
           B.AM_TAXSTD AS AM_TAXSTD2,
           B.AM_ADDTAX AS AM_ADDTAX2,
           (A.CNT - B.CNT) AS CNT_DIFF,
           (A.AM_TAXSTD - B.AM_TAXSTD) AS AM_TAXSTD_DIFF,
           (A.AM_ADDTAX - B.AM_ADDTAX) AS AM_ADDTAX_DIFF
    FROM A2 A
    LEFT JOIN B2 B ON B.ID_UPDATE = A.NO_COMPANY1 AND B.DT_START = A.DT_START
    WHERE (ISNULL(A.AM_TAXSTD, 0) <> ISNULL(B.AM_TAXSTD, 0) OR ISNULL(A.AM_ADDTAX, 0) <> ISNULL(B.AM_ADDTAX, 0))
),
D2 AS
(
    SELECT EX.NO_ETAX,
           EX.DT_START,
           EX.NO_COMPANY1,
           EX.NM_COMPANY1,
           EX.AM_SUM,
           EX.AM_TAXSTD,
           EX.AM_ADDTAX,
           EX.TP_TAX,
           EX.NM_BIGO,
           EX.BUY_DAM_EMAIL,
           EX.BUY_DAM_EMAIL2,
           EX.NM_ITEM,
           ROW_NUMBER() OVER (PARTITION BY EX.NO_COMPANY1, EX.DT_START, EX.AM_TAXSTD, EX.AM_ADDTAX ORDER BY EX.DT_START) AS IDX
    FROM FI_ETAX2 EX
    WHERE EX.CD_COMPANY = @P_CD_COMPANY
    AND EX.DT_START LIKE @V_DT_MONTH + '%'
    AND EX.TP_GUBUN = '2'
    AND EXISTS (SELECT 1 
                FROM C2 C 
                WHERE C.NO_COMPANY1 = EX.NO_COMPANY1
                AND C.DT_START = EX.DT_START)
),
E2 AS
(
    SELECT D.NO_ETAX,
           D.DT_START,
           D.NO_COMPANY1,
           D.NM_COMPANY1,
           D.AM_SUM,
           D.AM_TAXSTD,
           D.AM_ADDTAX,
           D.TP_TAX,
           D.NM_BIGO,
           D.BUY_DAM_EMAIL,
           D.BUY_DAM_EMAIL2,
           D.NM_ITEM
    FROM D2 D
    LEFT JOIN (SELECT TX.ID_UPDATE,
                      TX.DT_START,
                      TX.AM_TAXSTD,
                      TX.AM_ADDTAX,
                      ROW_NUMBER() OVER (PARTITION BY TX.ID_UPDATE, TX.DT_START, TX.AM_TAXSTD, TX.AM_ADDTAX ORDER BY TX.DT_START) AS IDX
               FROM FI_TAX TX
               WHERE TX.CD_COMPANY = @P_CD_COMPANY
               AND TX.DT_START LIKE @V_DT_MONTH + '%'
               AND TX.TP_TAX IN ('26','27','29','32','40','44','46','48','51','54','61','63','65','67')) TX
    ON TX.ID_UPDATE = D.NO_COMPANY1 AND TX.DT_START = D.DT_START AND TX.AM_TAXSTD = D.AM_TAXSTD AND TX.AM_ADDTAX = D.AM_ADDTAX AND TX.IDX = D.IDX
    WHERE TX.IDX IS NULL
),
E3 AS
(
    SELECT E.NO_ETAX,
           E.DT_START,
           E.NO_COMPANY1,
           E.NM_COMPANY1,
           E.AM_SUM,
           E.AM_TAXSTD,
           E.AM_ADDTAX,
           E.TP_TAX,
           E.NM_BIGO,
           E.BUY_DAM_EMAIL,
           E.BUY_DAM_EMAIL2,
           E.NM_ITEM
    FROM E1 E
    UNION ALL
    SELECT E.NO_ETAX,
           E.DT_START,
           E.NO_COMPANY1,
           E.NM_COMPANY1,
           E.AM_SUM,
           E.AM_TAXSTD,
           E.AM_ADDTAX,
           E.TP_TAX,
           E.NM_BIGO,
           E.BUY_DAM_EMAIL,
           E.BUY_DAM_EMAIL2,
           E.NM_ITEM
    FROM E2 E
)
INSERT INTO #FI_ETAX
(
    NO_ETAX,
    DT_START,
    NO_COMPANY1,
    NM_COMPANY1,
    AM_SUM,
    AM_TAXSTD,
    AM_ADDTAX,
    TP_TAX,
    NM_BIGO,
    BUY_DAM_EMAIL,
    BUY_DAM_EMAIL2,
    NM_ITEM
)
SELECT E3.NO_ETAX,
       E3.DT_START,
       E3.NO_COMPANY1,
       E3.NM_COMPANY1,
       E3.AM_SUM,
       E3.AM_TAXSTD,
       E3.AM_ADDTAX,
       E3.TP_TAX,
       E3.NM_BIGO,
       E3.BUY_DAM_EMAIL,
       E3.BUY_DAM_EMAIL2,
       E3.NM_ITEM
FROM E3
WHERE ISNULL(E3.BUY_DAM_EMAIL, '') NOT LIKE '%invoice@dintec.co.kr%' AND ISNULL(E3.BUY_DAM_EMAIL2, '') NOT LIKE '%invoice@dintec.co.kr%'
AND ISNULL(E3.BUY_DAM_EMAIL, '') NOT LIKE '%invoice2@dintec.co.kr%' AND ISNULL(E3.BUY_DAM_EMAIL2, '') NOT LIKE '%invoice2@dintec.co.kr%'
AND ISNULL(E3.BUY_DAM_EMAIL, '') NOT LIKE '%invoice@dubheco.com%' AND ISNULL(E3.BUY_DAM_EMAIL2, '') NOT LIKE '%invoice@dubheco.com%'
AND ISNULL(E3.BUY_DAM_EMAIL, '') NOT LIKE '%hjno@shinecustoms.com%' AND ISNULL(E3.BUY_DAM_EMAIL2, '') NOT LIKE '%hjno@shinecustoms.com%'
AND (ISNULL(E3.BUY_DAM_EMAIL, '') <> '' OR ISNULL(E3.BUY_DAM_EMAIL2, '') <> '')

DECLARE SRC_CURSOR CURSOR FOR
SELECT BUY_DAM_EMAIL, BUY_DAM_EMAIL2 
FROM #FI_ETAX
GROUP BY BUY_DAM_EMAIL, BUY_DAM_EMAIL2

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_BUY_DAM_EMAIL, @V_BUY_DAM_EMAIL2

WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @V_SUBJECT NVARCHAR(50)
	DECLARE @V_HTML NVARCHAR(MAX)
	DECLARE @V_BODY	NVARCHAR(MAX) = ''
    DECLARE @V_FROM_EMAIL NVARCHAR(50)
    DECLARE @V_TO_EMAIL NVARCHAR(50)
    DECLARE @V_COPY_RECIPIENTS NVARCHAR(50)

	SET @V_SUBJECT = '미정산 세금계산서 확인 요청'

    IF @P_CD_COMPANY = 'K100'
        SET @V_FROM_EMAIL = 'invoice@dintec.co.kr'
    ELSE
        SET @V_FROM_EMAIL = 'invoice@dubheco.com'

    IF ISNULL(@V_BUY_DAM_EMAIL2, '') = ''
    BEGIN
        IF (@V_BUY_DAM_EMAIL NOT LIKE '%dintec.co.kr' AND @V_BUY_DAM_EMAIL NOT LIKE '%dubheco.com')
            SET @V_TO_EMAIL = @V_FROM_EMAIL
        ELSE
            SET @V_TO_EMAIL = @V_BUY_DAM_EMAIL    
    END
    ELSE
    BEGIN
        IF (@V_BUY_DAM_EMAIL NOT LIKE '%dintec.co.kr' AND @V_BUY_DAM_EMAIL NOT LIKE '%dubheco.com' AND
            @V_BUY_DAM_EMAIL2 NOT LIKE '%dintec.co.kr' AND @V_BUY_DAM_EMAIL2 NOT LIKE '%dubheco.com')
            SET @V_TO_EMAIL = @V_FROM_EMAIL
        ELSE
            SET @V_TO_EMAIL = @V_BUY_DAM_EMAIL + ';' + @V_BUY_DAM_EMAIL2
    END

    IF (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%hjkim01@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%hjkim01@dintec.co.kr%') OR
       (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%logistics@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%logistics@dintec.co.kr%') OR
       (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%logistitcs@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%logistitcs@dintec.co.kr%') OR
       (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%hmpark@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%hmpark@dintec.co.kr%') OR
       (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%sd2-ts@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%sd2-ts@dintec.co.kr%') OR
       (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%invoice6@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%invoice6@dintec.co.kr%') OR
       (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%tjkim@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%tjkim@dintec.co.kr%') OR
       (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%it@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%it@dintec.co.kr%') OR
       (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%dykim@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%dykim@dintec.co.kr%') OR
       (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%sangwon.ha@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%sangwon.ha@dintec.co.kr%') OR
       (ISNULL(@V_BUY_DAM_EMAIL, '') LIKE '%log@dintec.co.kr%' OR ISNULL(@V_BUY_DAM_EMAIL2, '') LIKE '%log@dintec.co.kr%')
       
    BEGIN
        SET @V_COPY_RECIPIENTS = NULL
    END
    ELSE
    BEGIN
        SET @V_COPY_RECIPIENTS = @V_FROM_EMAIL
    END
	
	;WITH A AS
	(
		SELECT DT_START,
			   '<tr style="height:30px">' +
			   '<th style="border:solid 1px black; text-align:center; font-weight:normal; width:250px">' + ISNULL(NO_ETAX, '') + '</th>' +
			   '<th style="border:solid 1px black; text-align:center; font-weight:normal; width:150px">' + ISNULL(CONVERT(CHAR(10), CONVERT(DATETIME, DT_START), 111), '') + '</th>' +
			   '<th style="border:solid 1px black; text-align:center; font-weight:normal; width:150px">' + ISNULL(NO_COMPANY1, '') + '</th>' +
			   '<th style="border:solid 1px black; text-align:center; font-weight:normal; width:100px">' + ISNULL(NM_COMPANY1, '') + '</th>' +
			   '<th style="border:solid 1px black; text-align:right; font-weight:normal; width:100px">' + ISNULL(CONVERT(NVARCHAR, FORMAT(AM_TAXSTD, '#,##0')),'') + '</th>' +
			   '<th style="border:solid 1px black; text-align:right; font-weight:normal; width:100px">' + ISNULL(CONVERT(NVARCHAR, FORMAT(AM_ADDTAX, '#,##0')),'') + '</th>' +
			   '<th style="border:solid 1px black; text-align:right; font-weight:normal; width:100px">' + ISNULL(CONVERT(NVARCHAR, FORMAT(AM_SUM, '#,##0')),'') + '</th>' +
			   '<th style="border:solid 1px black; text-align:left; font-weight:normal; width:250px">' + ISNULL(NM_BIGO, '') + '</th>' +
			   '<th style="border:solid 1px black; text-align:left; font-weight:normal; width:250px">' + ISNULL(NM_ITEM, '') + '</th>' +
               '<th style="border:solid 1px black; text-align:center; font-weight:normal; width:150px">' + ISNULL(TP_TAX, '') + '</th>' +
			   '</tr>' AS DC_BODY
		FROM #FI_ETAX
        WHERE BUY_DAM_EMAIL = @V_BUY_DAM_EMAIL
        AND BUY_DAM_EMAIL2 = @V_BUY_DAM_EMAIL2
	)
	SELECT @V_BODY = STRING_AGG(CONVERT(NVARCHAR(MAX), A.DC_BODY), '') WITHIN GROUP (ORDER BY DT_START ASC)
	FROM A
	
    IF @P_CD_COMPANY = 'K100'
    BEGIN
        SET @V_HTML = '<div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   수신 : 수신자제위<br><br>
                       안녕하십니까 ?<br><br>
                       아래는 세금계산서가 발행 되었지만 처리되지 않은 리스트 입니다.<br><br>'
        IF @V_COPY_RECIPIENTS IS NULL
        BEGIN
            SET @V_HTML = @V_HTML + '세금계산서 확인하시고 처리하시기 바랍니다.<br><br>'
        END
        ELSE
        BEGIN
            SET @V_HTML = @V_HTML + '개인 메일로 접수 받아서 매입정산 누락 가능성이 있으므로, invoice@dintec.co.kr 메일로 세금계산서 전달 바랍니다.<br><br>
                                     추후에는 invoice@dintec.co.kr 메일로 세금계산서 발행될 수 있도록 매입처에 협조요청 부탁드립니다.<br><br>'
        END

	    SET @V_HTML = @V_HTML + '</div>
					   
					   <table style="width:1500px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;">
					   <colgroup width="250px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="250px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <tbody>
						<tr style="height:30px">
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:250px">승인번호</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">작성일자</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">사업자등록번호</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">상호</th>                                    
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">공급가액</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">세액</th>                                    
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">합계</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">비고</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:250px">품목비고</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">유형</th>
						</tr>'
						+ @V_BODY +
					   '</tbody>
					   </table>
					   
					   <div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   <br>
                       감사합니다.
					   </div>'
    END
    ELSE
    BEGIN
        SET @V_HTML = '<div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   수신 : 수신자제위<br><br>
                       안녕하십니까 ?<br><br>
                       아래는 세금계산서가 발행 되었지만 처리되지 않은 리스트 입니다.<br><br>'

        IF @V_COPY_RECIPIENTS IS NULL
        BEGIN
            SET @V_HTML = @V_HTML + '세금계산서 확인하시고 처리하시기 바랍니다.<br><br>'
        END
        ELSE
        BEGIN
            SET @V_HTML = @V_HTML + '개인 메일로 접수 받아서 매입정산 누락 가능성이 있으므로, invoice@dubheco.com 메일로 세금계산서 전달 바랍니다.<br><br>
                                     추후에는 invoice@dubheco.com 메일로 세금계산서 발행될 수 있도록 매입처에 협조요청 부탁드립니다.<br><br>'
        END

	    SET @V_HTML = @V_HTML + '</div>
					   
					   <table style="width:1500px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;">
					   <colgroup width="250px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="250px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <tbody>
						<tr style="height:30px">
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:250px">승인번호</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">작성일자</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">사업자등록번호</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">상호</th>                                    
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">공급가액</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">세액</th>                                    
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">합계</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">비고</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:250px">품목비고</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">유형</th>
						</tr>'
						+ @V_BODY +
					   '</tbody>
					   </table>
					   
					   <div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   <br>
                       감사합니다.
					   </div>'
    END

    --SET @V_FROM_EMAIL = 'khkim@dintec.co.kr'
    --SET @V_TO_EMAIL = 'khkim@dintec.co.kr'
    --SET @V_COPY_RECIPIENTS = NULL

	EXEC msdb.dbo.sp_send_dbmail @PROFILE_NAME = 'AUTO_MAIL',
								 @RECIPIENTS = @V_TO_EMAIL,
                                 @COPY_RECIPIENTS = @V_COPY_RECIPIENTS,
								 @FROM_ADDRESS = @V_FROM_EMAIL,
								 @REPLY_TO = @V_FROM_EMAIL,
								 @SUBJECT = @V_SUBJECT,	
								 @BODY = @V_HTML,	
								 @BODY_FORMAT = 'HTML'

	FETCH NEXT FROM SRC_CURSOR INTO @V_BUY_DAM_EMAIL, @V_BUY_DAM_EMAIL2
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

DROP TABLE #FI_ETAX

GO