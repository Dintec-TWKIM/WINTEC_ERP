USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_WBARCODE_S2]    Script Date: 2021-03-24 오전 10:45:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
*********************************************/
ALTER PROCEDURE [NEOE].[SP_CZ_HR_WBARCODE_S2]
(
	@P_CD_COMPANY  		NVARCHAR(7),	
	@P_MULTI_BIZAREA	NVARCHAR(4000),
	@P_MULTI_DEPT		NTEXT,
	@P_MULTI_EMP		NVARCHAR(1000),
	@P_DT_FROM			NVARCHAR(8),
	@P_DT_TO			NVARCHAR(8),
	@P_FG_LANG			NVARCHAR(4) = NULL	--언어
)
AS
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG

DECLARE	@P_CD_ACAL	NVARCHAR(3)
DECLARE	@P_TP_CAL	NVARCHAR(3)
DECLARE @ID INT, @ROWCOUNT INT
		
SELECT IDENTITY(INT, 1, 1) AS ID,
	   CD_ACAL,
	   TP_CAL
INTO #UP_HR_WBARCODE_CUR
FROM HR_WBASIS
WHERE CD_COMPANY = @P_CD_COMPANY
AND CD_BIZAREA IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_BIZAREA))

SET @ROWCOUNT = @@ROWCOUNT
CREATE CLUSTERED INDEX IX_#UP_HR_WBARCODE_CUR ON #UP_HR_WBARCODE_CUR (ID)
SET @ID = 0

-- 달력사용형태, 달력종류 조회
WHILE(@ID < @ROWCOUNT)
BEGIN
	SET @ID = @ID + 1
	SELECT @P_CD_ACAL = CD_ACAL,
		   @P_TP_CAL = TP_CAL
	FROM #UP_HR_WBARCODE_CUR
	WHERE ID = @ID
	
	IF(@P_CD_ACAL IS NULL OR @P_CD_ACAL = '')
	BEGIN
		-- 근태달력이 설정되지 않았습니다.
		RAISERROR ('HR_M500151', 18, 1)
		RETURN
	END

	IF(@P_CD_ACAL = '001' AND (@P_TP_CAL IS NULL OR @P_TP_CAL = ''))
	BEGIN
		-- 사업장에 대한 달력이 설정되지 않았습니다.
		RAISERROR ('HR_M500152', 18, 1)
		RETURN
	END
END

SELECT 'N' S,
	   W.NO_CARD,
	   W.DT_WORK,
	   (CASE WHEN C.FG1_HOLIDAY = 'H' THEN '004' ELSE '001' END) AS FG1_HOLIDAY,
	   C.TP_WEEK,
	   M.NO_EMP,
	   E.NM_KOR,	
	   W.CD_WCODE,
	   W.TM_CARD,
	   W.TM_CARD OLD_TM_CARD,
	   (SELECT NM_KOR FROM DZSN_MA_EMP WHERE CD_COMPANY = @P_CD_COMPANY AND NO_EMP = U.NO_EMP) AS NM_UPDATE,
	   (CASE WHEN ISNULL(W.DTS_UPDATE, '') = '' OR W.DTS_UPDATE = '00000000' THEN '' ELSE SUBSTRING(W.DTS_UPDATE, 1, 4) + '-' + 
	   																					  SUBSTRING(W.DTS_UPDATE, 5 ,2) + '-' + 
	   																					  SUBSTRING(W.DTS_UPDATE, 7, 2) + ' ' + 
	   																					  SUBSTRING(W.DTS_UPDATE, 9, 2) + ':' +
	   																					  SUBSTRING(W.DTS_UPDATE, 11, 2) END) AS DTSUPDATE
FROM HR_WBARCODE AS W
JOIN HR_WECMATCH AS M ON W.CD_COMPANY = M.CD_COMPANY AND W.NO_CARD = M.NO_CARD
JOIN DZSN_MA_EMP AS E ON M.CD_COMPANY = E.CD_COMPANY AND M.NO_EMP = E.NO_EMP
LEFT JOIN (SELECT CD_COMPANY,
				  DT_CAL,
				  TP_WEEK,
				  FG1_HOLIDAY
		   FROM MA_CALENDAR
		   WHERE CD_COMPANY	= @P_CD_COMPANY
		   AND TP_CAL = '001'
		   AND DT_CAL BETWEEN @P_DT_FROM AND @P_DT_TO) AS C
ON W.CD_COMPANY = C.CD_COMPANY AND W.DT_WORK = C.DT_CAL
LEFT JOIN MA_USER U ON W.CD_COMPANY = U.CD_COMPANY AND W.ID_UPDATE = U.ID_USER
WHERE W.CD_COMPANY = @P_CD_COMPANY
AND W.DT_WORK BETWEEN @P_DT_FROM AND @P_DT_TO
AND (E.CD_BIZAREA IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_BIZAREA)) OR @P_MULTI_BIZAREA = '' OR @P_MULTI_BIZAREA IS NULL)
AND (@P_MULTI_DEPT IS NULL OR CONVERT(NVARCHAR, @P_MULTI_DEPT) = '' OR E.CD_DEPT IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_DEPT)))
AND (@P_MULTI_EMP IS NULL OR CONVERT(NVARCHAR, @P_MULTI_EMP) = '' OR E.NO_EMP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_EMP)))
ORDER BY W.DT_WORK ASC
GO

