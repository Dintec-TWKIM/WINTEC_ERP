USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_AUTO_MAIL_PO_LIMIT]    Script Date: 2020-12-15 오후 2:03:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_AUTO_MAIL_PO_LIMIT]
(
	@P_CD_COMPANY	NVARCHAR(7)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_CD_PARTNER	    NVARCHAR(20)
DECLARE @V_LN_PARTNER	    NVARCHAR(50)
DECLARE @V_QT_LT_OVERDUE    INT
DECLARE @V_CD_AREA          NVARCHAR(4)
DECLARE @V_NO_EMP           NVARCHAR(10)
DECLARE @V_FROM_EMAIL	    NVARCHAR(200)
DECLARE @V_TO_EMAIL	        NVARCHAR(MAX)

DECLARE SRC_CURSOR CURSOR FOR
WITH A AS
(
    SELECT PH.CD_COMPANY,
           PH.CD_PARTNER,
		   VE.CD_FLAG2,
           MAX(AM.TP_SEND) AS TP_SEND,
           MAX(AM.QT_LT_OVERDUE) AS QT_LT_OVERDUE, 
           MAX(CASE WHEN AM.TP_PERIOD = 'DAY' AND DATEPART(DW, GETDATE()) IN (2, 3, 4, 5, 6) THEN 'Y'
				    WHEN AM.TP_PERIOD = 'WEK' AND AM.YN_MON = 'Y' AND DATEPART(DW, GETDATE()) = 2 THEN 'Y'
				    WHEN AM.TP_PERIOD = 'WEK' AND AM.YN_TUE = 'Y' AND DATEPART(DW, GETDATE()) = 3 THEN 'Y'
				    WHEN AM.TP_PERIOD = 'WEK' AND AM.YN_WED = 'Y' AND DATEPART(DW, GETDATE()) = 4 THEN 'Y'
				    WHEN AM.TP_PERIOD = 'WEK' AND AM.YN_THU = 'Y' AND DATEPART(DW, GETDATE()) = 5 THEN 'Y'
				    WHEN AM.TP_PERIOD = 'WEK' AND AM.YN_FRI = 'Y' AND DATEPART(DW, GETDATE()) = 6 THEN 'Y'
				    ELSE 'N' END) YN_SEND
    FROM PU_POH PH
    JOIN (SELECT PL.CD_COMPANY, PL.NO_PO,
    	  	     MAX(PL.DT_LIMIT) AS DT_LIMIT
    	  FROM PU_POL PL
    	  WHERE PL.QT_PO > ISNULL(PL.QT_RCV, 0)
    	  AND PL.CD_ITEM NOT LIKE 'SD%'
		  AND EXISTS (SELECT 1 
                      FROM SA_SOL SL
                      WHERE SL.CD_COMPANY = PL.CD_COMPANY
                      AND SL.NO_SO = PL.NO_SO
                      AND SL.SEQ_SO = PL.NO_SOLINE
                      AND ISNULL(SL.QT_SO, 0) > ISNULL(SL.QT_GIR, 0)) -- 협조전작성건 제외
    	  GROUP BY PL.CD_COMPANY, PL.NO_PO) PL 
    ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
    JOIN CZ_SA_AUTO_MAIL_PARTNER AM ON AM.CD_COMPANY = PH.CD_COMPANY AND AM.TP_PARTNER = '001' AND AM.CD_PARTNER = PH.CD_PARTNER
    LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = PH.CD_COMPANY AND SH.NO_SO = PH.CD_PJT
    LEFT JOIN CZ_SA_DEFERRED_DELIVERY DD ON DD.CD_COMPANY = PH.CD_COMPANY AND DD.TP_TYPE = '2' AND DD.NO_SO = PH.CD_PJT AND DD.NO_KEY = PH.NO_PO AND DD.DT_LIMIT = PL.DT_LIMIT
    LEFT JOIN V_CZ_SA_QTN_LOG_EMP VE ON VE.CD_COMPANY = SH.CD_COMPANY AND VE.NO_FILE = SH.NO_SO
    WHERE PH.CD_COMPANY = @P_CD_COMPANY
    AND DATEDIFF(DAY, PL.DT_LIMIT, GETDATE()) >= ISNULL(AM.QT_LT_OVERDUE, 0) -- 납기일 설정일 이상 경과
    AND DATEDIFF(DAY, PH.DT_PO, GETDATE()) >= 5 -- 발주한지 5일 이상 경과
    AND SH.DT_SO BETWEEN DATEADD(YEAR, -3, GETDATE()) AND CONVERT(CHAR(8), GETDATE(), 112) -- 3년치 수주
    AND ISNULL(SH.YN_CLOSE, 'N') <> 'Y' -- 수주마감 제외
    AND ISNULL(DD.YN_EXCLUDE, 'N') <> 'Y' -- 제외건 제외
    AND (ISNULL(DD.DT_EXPECT, '') = '' OR DATEDIFF(DAY, DD.DT_EXPECT, GETDATE()) >= 2) -- 입고예정일 2일 이상 경과
    AND (ISNULL(DD.DTS_SEND, '') = '' OR DATEDIFF(DAY, LEFT(DD.DTS_SEND, 8), GETDATE()) >= 1) -- 메일 발송일 1일 이상 경과
	AND VE.CD_FLAG1 IS NOT NULL
	AND EXISTS (SELECT 1 
				FROM MA_CODEDTL MC 
				WHERE MC.CD_COMPANY = PH.CD_COMPANY 
				AND MC.CD_FIELD = 'CZ_SA00023' 
				AND MC.CD_SYSDEF = LEFT(PH.NO_PO, 2) 
				AND MC.CD_SYSDEF NOT IN ('00', 'ZZ') 
				AND MC.CD_SYSDEF NOT IN ('SB', 'NS', 'BE', 'TE', 'HB'))
    GROUP BY PH.CD_COMPANY, PH.CD_PARTNER, VE.CD_FLAG2
),
B AS 
(
    SELECT A.CD_COMPANY,
           A.CD_PARTNER,
		   MP.LN_PARTNER,
           A.QT_LT_OVERDUE,
           MP.CD_AREA,
           (CASE WHEN A.TP_SEND = 'ALL' THEN 'ALL' ELSE A.CD_FLAG2 END) AS NO_EMP,
           (CASE WHEN A.TP_SEND = 'ALL' THEN (CASE WHEN ISNULL(MP.CD_AREA, '100') = '200' THEN 'DINTEC Delivery Mgmt Team<delivery@dintec.co.kr>'
                                                                                          ELSE '(주)딘텍 영업물류팀<delivery@dintec.co.kr>' END) 
                 WHEN A.TP_SEND = 'EMP' THEN (CASE WHEN ISNULL(MP.CD_AREA, '100') = '200' THEN ISNULL(ME.NM_ENG, '') + '/DINTEC<' +  ISNULL(ME.NO_EMAIL, '') + '>'
                                                                                          ELSE ISNULL(ME.NM_KOR, '') + '/(주)딘텍<' +  ISNULL(ME.NO_EMAIL, '') + '>' END)
                                        ELSE '(주)딘텍 영업물류팀<delivery@dintec.co.kr>' END) AS FROM_EMAIL
    FROM A
    LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = A.CD_COMPANY AND MP.CD_PARTNER = A.CD_PARTNER
    LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = A.CD_COMPANY AND ME.NO_EMP = A.CD_FLAG2
    WHERE A.YN_SEND = 'Y'
),
C AS
(
    SELECT B.CD_COMPANY,
           B.CD_PARTNER,
		   B.LN_PARTNER,
           B.FROM_EMAIL,
           B.NO_EMP,
           MAX(B.CD_AREA) AS CD_AREA,
           MAX(B.QT_LT_OVERDUE) AS QT_LT_OVERDUE
    FROM B
    GROUP BY B.CD_COMPANY,
             B.CD_PARTNER,
			 B.LN_PARTNER,
             B.FROM_EMAIL,
             B.NO_EMP
)
SELECT C.CD_PARTNER,
	   C.LN_PARTNER,
       C.QT_LT_OVERDUE,
       (CASE WHEN C.CD_PARTNER = '17359' THEN '100' ELSE C.CD_AREA END) AS CD_AREA,
       C.NO_EMP,
       C.FROM_EMAIL,
       (SELECT STRING_AGG(A.NM_EMAIL, ';') 
		FROM (SELECT DISTINCT FP.NM_EMAIL 
			  FROM FI_PARTNERPTR FP
			  WHERE FP.CD_COMPANY = C.CD_COMPANY
			  AND FP.CD_PARTNER = C.CD_PARTNER
			  AND FP.YN_LIMIT = 'Y') A) AS TO_EMAIL
FROM C

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_CD_PARTNER, @V_LN_PARTNER, @V_QT_LT_OVERDUE, @V_CD_AREA, @V_NO_EMP, @V_FROM_EMAIL, @V_TO_EMAIL

WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @V_SUBJECT NVARCHAR(50)
	DECLARE @V_HTML NVARCHAR(MAX)
	DECLARE @V_BODY	NVARCHAR(MAX) = ''
	DECLARE @V_HTML_RED NVARCHAR(MAX)

	IF @V_CD_AREA = '100'
		SET @V_SUBJECT = '[딘텍] 납기 확인 요청드립니다.'
	ELSE
		SET @V_SUBJECT = '[Dintec] REQUEST FOR READINESS'
	
	IF EXISTS (SELECT 1 
			   FROM CZ_SA_AUTO_MAIL_PO_LIMIT_LOG PH
			   WHERE PH.CD_COMPANY =  @P_CD_COMPANY
			   AND PH.CD_PARTNER = @V_CD_PARTNER
			   AND PH.FROM_EMAIL = @V_FROM_EMAIL
			   AND PH.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112))
	BEGIN
		DELETE PH
		FROM CZ_SA_AUTO_MAIL_PO_LIMIT_LOG PH
		WHERE PH.CD_COMPANY =  @P_CD_COMPANY
		AND PH.CD_PARTNER = @V_CD_PARTNER
		AND PH.FROM_EMAIL = @V_FROM_EMAIL
		AND PH.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
	END

	INSERT INTO CZ_SA_AUTO_MAIL_PO_LIMIT_LOG
	(
		CD_COMPANY,
		NO_PO,
		NO_SO,
		DT_LIMIT,
		DT_SEND,
		IDX,
		CD_PARTNER,
		NO_ORDER,
		NM_VESSEL,
		NO_HULL,
		DT_PO,
		DT_EXPECT,
		YN_URGENT,
		NM_KOR,
		NO_TEL,
		DC_RMK1,
		FROM_EMAIL,
		TO_EMAIL,
		ID_INSERT,
		DTS_INSERT
	)
	SELECT PH.CD_COMPANY,
		   PH.NO_PO,
		   PH.CD_PJT AS NO_SO,
		   PL.DT_LIMIT,
		   CONVERT(CHAR(8), GETDATE(), 112) AS DT_SEND,
		   ROW_NUMBER() OVER (PARTITION BY PH.NO_PO ORDER BY PL.DT_LIMIT DESC) AS IDX,
		   @V_CD_PARTNER,
		   PH.NO_ORDER,
		   MH.NM_VESSEL,
		   MH.NO_HULL,
		   PH.DT_PO,
		   DD.DT_EXPECT,
		   DD.YN_URGENT,
		   ME.NM_KOR,
		   ME.NO_TEL,
		   DD.DC_RMK1,
		   @V_FROM_EMAIL,
		   @V_TO_EMAIL,
		   'SYSTEM' AS ID_INSERT,
		   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
	FROM PU_POH PH
	JOIN (SELECT PL.CD_COMPANY, PL.NO_PO,
				 MAX(PL.DT_LIMIT) AS DT_LIMIT
		  FROM PU_POL PL
		  WHERE 1=1
		  AND PL.QT_PO > ISNULL(PL.QT_RCV, 0)
		  AND PL.CD_ITEM NOT LIKE 'SD%'
		  AND EXISTS (SELECT 1 
                      FROM SA_SOL SL
                      WHERE SL.CD_COMPANY = PL.CD_COMPANY
                      AND SL.NO_SO = PL.NO_SO
                      AND SL.SEQ_SO = PL.NO_SOLINE
                      AND ISNULL(SL.QT_SO, 0) > ISNULL(SL.QT_GIR, 0)) -- 협조전작성건 제외
		  GROUP BY PL.CD_COMPANY, PL.NO_PO) PL 
	ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
	LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = PH.CD_COMPANY AND SH.NO_SO = PH.CD_PJT
	LEFT JOIN CZ_SA_DEFERRED_DELIVERY DD ON DD.CD_COMPANY = PH.CD_COMPANY AND DD.TP_TYPE = '2' AND DD.NO_SO = PH.CD_PJT AND DD.NO_KEY = PH.NO_PO AND DD.DT_LIMIT = PL.DT_LIMIT
	LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
	LEFT JOIN V_CZ_SA_QTN_LOG_EMP VE ON VE.CD_COMPANY = SH.CD_COMPANY AND VE.NO_FILE = SH.NO_SO
	LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = VE.CD_COMPANY AND ME.NO_EMP = VE.CD_FLAG1
	WHERE PH.CD_COMPANY = @P_CD_COMPANY
	AND PH.CD_PARTNER = @V_CD_PARTNER
	AND DATEDIFF(DAY, PL.DT_LIMIT, GETDATE()) >= ISNULL(@V_QT_LT_OVERDUE, 0)  -- 납기일 설정일 이상 경과
	AND (ISNULL(@V_NO_EMP, 'ALL') = 'ALL' OR VE.CD_FLAG2 = @V_NO_EMP)
	AND DATEDIFF(DAY, PH.DT_PO, GETDATE()) >= 5 -- 발주한지 5일 이상 경과
	AND SH.DT_SO BETWEEN DATEADD(YEAR, -3, GETDATE()) AND CONVERT(CHAR(8), GETDATE(), 112) -- 3년치 수주
	AND ISNULL(SH.YN_CLOSE, 'N') <> 'Y' -- 수주마감 제외
	AND ISNULL(DD.YN_EXCLUDE, 'N') <> 'Y' -- 제외건 제외
	AND (ISNULL(DD.DT_EXPECT, '') = '' OR DATEDIFF(DAY, DD.DT_EXPECT, GETDATE()) >= 2) -- 입고예정일 2일 이상 경과
	AND (ISNULL(DD.DTS_SEND, '') = '' OR DATEDIFF(DAY, LEFT(DD.DTS_SEND, 8), GETDATE()) >= 1) -- 메일 발송일 1일 이상 경과
	AND VE.CD_FLAG1 IS NOT NULL
	AND EXISTS (SELECT 1 
				FROM MA_CODEDTL MC 
				WHERE MC.CD_COMPANY = PH.CD_COMPANY 
				AND MC.CD_FIELD = 'CZ_SA00023' 
				AND MC.CD_SYSDEF = LEFT(PH.NO_PO, 2) 
				AND MC.CD_SYSDEF NOT IN ('00', 'ZZ') 
				AND MC.CD_SYSDEF NOT IN ('SB', 'NS', 'BE', 'TE', 'HB'))

	IF @V_CD_AREA = '100'
	BEGIN
		;WITH A AS
		(
		    SELECT PH.IDX,
		           '<tr style="height:30px">' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:center; font-weight:normal; width:150px">' + ISNULL(PH.NM_KOR, '') + ' (' + RIGHT(ISNULL(PH.NO_TEL, ''), 4) + ')</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:150px">' + ISNULL(PH.NO_SO, '') + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:150px">' + ISNULL(PH.NO_ORDER, '') + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:150px">' + ISNULL(PH.NM_VESSEL, '') + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:100px">' + ISNULL(PH.NO_HULL, '') + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:center; font-weight:normal; width:100px">' + (CASE WHEN ISNULL(TRY_CONVERT(DATETIME, PH.DT_PO), '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, ISNULL(PH.DT_PO, '')), 23) END) + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:center; font-weight:normal; width:100px">' + (CASE WHEN ISNULL(TRY_CONVERT(DATETIME, PH.DT_LIMIT), '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, ISNULL(PH.DT_LIMIT, '')), 23) END) + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:center; font-weight:normal; width:100px">' + (CASE WHEN ISNULL(TRY_CONVERT(DATETIME, PH.DT_EXPECT), '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, ISNULL(PH.DT_EXPECT, '')), 23) END) + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:250px">' + ISNULL(PH.DC_RMK1, '') + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:250px">' + '' + '</th>' +
				   '</tr>' AS DC_BODY
		    FROM CZ_SA_AUTO_MAIL_PO_LIMIT_LOG PH
			WHERE PH.CD_COMPANY =  @P_CD_COMPANY
			AND PH.CD_PARTNER = @V_CD_PARTNER
			AND PH.FROM_EMAIL = @V_FROM_EMAIL
			AND PH.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
		)
		SELECT @V_BODY = STRING_AGG(A.DC_BODY, '') WITHIN GROUP (ORDER BY A.IDX ASC)
		FROM A
	END
	ELSE
	BEGIN
		;WITH A AS
		(
			SELECT PH.IDX,
				   '<tr style="height:30px">' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:150px">' + ISNULL(PH.NO_SO, '') + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:150px">' + ISNULL(PH.NO_ORDER, '') + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:150px">' + ISNULL(PH.NM_VESSEL, '') + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:100px">' + ISNULL(PH.NO_HULL, '') + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:center; font-weight:normal; width:100px">' + (CASE WHEN ISNULL(TRY_CONVERT(DATETIME, PH.DT_PO), '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, ISNULL(PH.DT_PO, '')), 23) END) + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:center; font-weight:normal; width:100px">' + (CASE WHEN ISNULL(TRY_CONVERT(DATETIME, PH.DT_LIMIT), '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, ISNULL(PH.DT_LIMIT, '')), 23) END) + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:center; font-weight:normal; width:100px">' + (CASE WHEN ISNULL(TRY_CONVERT(DATETIME, PH.DT_EXPECT), '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, ISNULL(PH.DT_EXPECT, '')), 23) END) + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:250px">' + ISNULL(PH.DC_RMK1, '') + '</th>' +
				   '<th style="border:solid 1px black; ' + (CASE WHEN ISNULL(PH.YN_URGENT, 'N') = 'Y' THEN 'color: #FF0000;' ELSE '' END) + 'text-align:left; font-weight:normal; width:250px">' + '' + '</th>' +
				   '</tr>' AS DC_BODY
			FROM CZ_SA_AUTO_MAIL_PO_LIMIT_LOG PH
			WHERE PH.CD_COMPANY =  @P_CD_COMPANY
			AND PH.CD_PARTNER = @V_CD_PARTNER
			AND PH.FROM_EMAIL = @V_FROM_EMAIL
			AND PH.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)	
		)
		SELECT @V_BODY = STRING_AGG(A.DC_BODY, '') WITHIN GROUP (ORDER BY A.IDX ASC)
		FROM A
	END
	
	IF EXISTS (SELECT 1 
			   FROM CZ_SA_AUTO_MAIL_PO_LIMIT_LOG PH
			   WHERE PH.CD_COMPANY =  @P_CD_COMPANY
			   AND PH.CD_PARTNER = @V_CD_PARTNER
			   AND PH.FROM_EMAIL = @V_FROM_EMAIL
			   AND PH.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
			   AND PH.YN_URGENT = 'Y')
		SET @V_HTML_RED = '<br>
						   <span style="font-size: 10pt; font-family: 맑은 고딕; color: #FF0000;">
						   빨간색으로 표시된 건은 긴급 문의건입니다.<br><br>
						   한번 더 검토 부탁 드립니다.
						   </span>
						   <br>'
	ELSE
		SET @V_HTML_RED = ''

	IF @V_CD_AREA = '100'
	BEGIN
		SET @V_HTML = '<div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   수신 : ' + @V_LN_PARTNER + ' 담당자 님<br>
					   발신 : (주)딘텍 영업물류팀<br><br>
					   귀사의 일익 번창하심을 기원합니다.<br><br>
					   납기 확인 요청 드리오니, <span style="background:yellow;mso-highlight:yellow">일정 검토 후 하기 입고예정일란에 기입하여 회신 부탁 드립니다.</span><br>' + @V_HTML_RED + '<br>
					   </div>
					   
					   <table style="width:1500px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;">
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="250px" align="center"></colgroup>
					   <colgroup width="250px" align="center"></colgroup>
					   <tbody>
						<tr style="height:30px">
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">딘텍 담당자<br>(051-664-내선번호)</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">딘텍 Ref No.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">매입처 Ref No.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">호선명</th>                                    
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">호선번호</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">발주일자</th>                                    
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">납기일자</th>
						 <th style="border:solid 1px black; color: #FF0000; text-align:center; background-color:Silver; width:100px">입고예정일<br>(년/월/일)</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:250px">딘텍 요청사항</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:250px">매입처 요청사항</th>
						</tr>'
						+ @V_BODY +
					   '</tbody>
					   </table>
					   
					   <div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   <br>감사합니다.
					   </div>'
	END
	ELSE
	BEGIN
		SET @V_HTML = '<div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   Good day, sir/ madam.<br><br>
					   Kindly find below the list of open orders still pending.<br>
					   Please advise readiness for each in <span style="background:yellow;mso-highlight:yellow">“EXPECTED READINESS”</span> column:<br><br>
					   </div>
					   
					   <table style="width:1500px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;">
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="300px" align="center"></colgroup>
					   <colgroup width="300px" align="center"></colgroup>
					   <tbody>
						<tr style="height:30px">
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">OUR REF NO.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">YOUR REF NO.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">VESSEL NAME</th>                                    
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">HULL NO.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">ORDERED</th>                                    
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:100px">DUE DATE</th>
						 <th style="border:solid 1px black; color: #FF0000; text-align:center; background-color:Silver; width:100px">EXPECTED READINESS<br>(YYYY/MM/DD)</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:250px">OUR REMARKS</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:250px">YOUR REMARKS</th>
						</tr>'
						+ @V_BODY +
					   '</tbody>
					   </table>
					   
					   <div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   <br>Awaiting your feedback
					   </div>'
	END

	BEGIN TRY
		IF EXISTS (SELECT 1 
				   FROM CZ_SA_AUTO_MAIL_PO_LIMIT_LOG LG
				   WHERE LG.CD_COMPANY =  @P_CD_COMPANY
				   AND LG.CD_PARTNER = @V_CD_PARTNER
				   AND LG.FROM_EMAIL = @V_FROM_EMAIL
				   AND LG.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112))
		BEGIN
			EXEC msdb.dbo.sp_send_dbmail @PROFILE_NAME = 'AUTO_MAIL',	
									     @RECIPIENTS = @V_TO_EMAIL,
									     @COPY_RECIPIENTS = @V_FROM_EMAIL,
										 @FROM_ADDRESS = @V_FROM_EMAIL,
									     @REPLY_TO = @V_FROM_EMAIL,
									     @SUBJECT = @V_SUBJECT,	
									     @BODY = @V_HTML,	
									     @BODY_FORMAT = 'HTML'
		END

		UPDATE DD
		SET DD.TP_SEND = '자동납기문의',
		    DD.DTS_SEND = NEOE.SF_SYSDATE(GETDATE()),
		    DD.ID_UPDATE = 'SYSTEM',
		    DD.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
		FROM CZ_SA_DEFERRED_DELIVERY DD
		WHERE DD.CD_COMPANY = @P_CD_COMPANY 
		AND DD.TP_TYPE = '2'
		AND EXISTS (SELECT 1 
					FROM CZ_SA_AUTO_MAIL_PO_LIMIT_LOG LG 
					WHERE LG.CD_COMPANY = DD.CD_COMPANY 
					AND LG.NO_SO = DD.NO_SO 
					AND LG.NO_PO = DD.NO_KEY 
					AND LG.DT_LIMIT = DD.DT_LIMIT
					AND LG.CD_PARTNER = @V_CD_PARTNER
					AND LG.FROM_EMAIL = @V_FROM_EMAIL
					AND LG.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112))
		
		INSERT INTO CZ_SA_DEFERRED_DELIVERY
		(
			CD_COMPANY,
			TP_TYPE,
			NO_SO,
			NO_KEY,
			DT_LIMIT,
		    TP_SEND,
		    DTS_SEND,
			ID_INSERT,
			DTS_INSERT
		)
		SELECT LG.CD_COMPANY,
		       '2' AS TP_TYPE,
		       LG.NO_SO,
		       LG.NO_PO AS NO_KEY,
		       LG.DT_LIMIT,
		       '자동납기문의' AS TP_SEND,
		       NEOE.SF_SYSDATE(GETDATE()) AS DTS_SEND,
		       'SYSTEM' AS ID_INSERT,
		       NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
		FROM CZ_SA_AUTO_MAIL_PO_LIMIT_LOG LG
		WHERE LG.CD_COMPANY =  @P_CD_COMPANY
		AND LG.CD_PARTNER = @V_CD_PARTNER
		AND LG.FROM_EMAIL = @V_FROM_EMAIL
		AND LG.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
		AND NOT EXISTS (SELECT 1 
		                FROM CZ_SA_DEFERRED_DELIVERY DD
		                WHERE DD.CD_COMPANY = LG.CD_COMPANY
		                AND DD.NO_SO = LG.NO_SO
		                AND DD.NO_KEY = LG.NO_PO
		                AND DD.TP_TYPE = '2'
		                AND DD.DT_LIMIT = LG.DT_LIMIT)
	END TRY
	BEGIN CATCH
		DECLARE @V_SUBJECT2 NVARCHAR(500) = '자동 납기 메일 발송 오류 발생 (' + @V_CD_PARTNER + ')'
		DECLARE @V_ERROR NVARCHAR(1000) = ERROR_MESSAGE()

		EXEC msdb.dbo.sp_send_dbmail @PROFILE_NAME = 'AUTO_MAIL',	
							         @RECIPIENTS = @V_FROM_EMAIL,
							         @SUBJECT = @V_SUBJECT2,	
							         @BODY = @V_ERROR
	END CATCH

	FETCH NEXT FROM SRC_CURSOR INTO @V_CD_PARTNER, @V_LN_PARTNER, @V_QT_LT_OVERDUE, @V_CD_AREA, @V_NO_EMP, @V_FROM_EMAIL, @V_TO_EMAIL
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

GO