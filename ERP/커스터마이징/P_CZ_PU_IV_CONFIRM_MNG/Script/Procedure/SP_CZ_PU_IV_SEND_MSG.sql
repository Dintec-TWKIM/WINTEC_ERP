USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_IV_SEND_MSG]    Script Date: 2016-11-17 오후 5:00:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
    
ALTER PROCEDURE [NEOE].[SP_CZ_PU_IV_SEND_MSG]        
(             
	@P_CD_COMPANY           NVARCHAR(7)
)        
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE SRC_CURSOR CURSOR FOR

WITH A AS
(
	SELECT IC.CD_COMPANY,
		   IC.SEQ,
		   IC.DT_END,
		   IC.NO_EMP,
		   CONVERT(DATETIME, LEFT(IC.DTS_INSERT, 8) + ' ' + 
		   				     SUBSTRING(IC.DTS_INSERT, 9, 2) + ':' + 
		   				     SUBSTRING(IC.DTS_INSERT, 11, 2) + ':' + 
		   				     SUBSTRING(IC.DTS_INSERT, 13, 2)) AS DTS_INSERT1,
		   (SELECT COUNT(1) 
			FROM MA_CALENDAR
			WHERE CD_COMPANY = IC.CD_COMPANY
			AND FG1_HOLIDAY = 'H'
			AND DT_CAL BETWEEN LEFT(IC.DTS_INSERT, 8) AND CONVERT(CHAR(8), GETDATE(), 112)) AS QT_HOLIDAY,
		   (CASE WHEN IC.NO_PO LIKE '%ST%' AND EXISTS (SELECT 1 
													   FROM string_split(IC.NO_EMP, '|') A
													   JOIN MA_EMP ME ON ME.CD_COMPANY = @P_CD_COMPANY AND ME.NO_EMP = A.value AND ME.CD_CC = '010900') THEN 'S-495' 
																																						ELSE '' END) AS NO_EMP_ADD
	FROM CZ_PU_IV_CONFIRM IC
	WHERE IC.CD_COMPANY = @P_CD_COMPANY
	AND ISNULL(IC.YN_DONE, 'N') = 'N'
	AND ISNULL(IC.YN_RETURN, 'N') = 'N'
),
B AS
(
	SELECT A.CD_COMPANY,
		   A.SEQ,
		   A.DT_END,
		   (A.NO_EMP + '|' + A.NO_EMP_ADD) AS NO_EMP,
		   (DATEDIFF(SECOND, A.DTS_INSERT1, GETDATE()) / 86400) - A.QT_HOLIDAY AS QT_DAY,
		   CONVERT(VARCHAR, DATEADD(S, DATEDIFF(SECOND, A.DTS_INSERT1, GETDATE()) % 86400, ''), 8) AS DT_HHMMSS
	FROM A
)
SELECT B.SEQ,
       (CASE WHEN B.QT_DAY >= 2 THEN B.NO_EMP + '|' + 'S-347' ELSE B.NO_EMP END) AS NO_EMP,
       '** 세금계산서 확인 요청 (재알림)' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + 
	   '순번 : ' + CONVERT(NVARCHAR, B.SEQ) + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + 
	   (CASE WHEN B.QT_DAY >= 1 THEN '해당 건은 확인 요청한지 ' + TRIM(CONVERT(CHAR(4), B.QT_DAY)) + '일 이상 경과된 건 입니다. (경과시간 : ' + (TRIM(CONVERT(CHAR(4), B.QT_DAY)) + '일 ' + B.DT_HHMMSS) + ')' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10)
								ELSE '해당 건은 마감일자가 지나거나 도래한 건 입니다. (마감일자 : ' + CONVERT(CHAR(10), CONVERT(DATETIME, B.DT_END), 111) + ')' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) END) +
	   '조속히 확인 후 워크플로우 -> 계산서확인 단계를 통해서 회신 바랍니다.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
	   '※ 본 쪽지는 발신전용 입니다.' AS CONTENTS
FROM B
WHERE (B.QT_DAY >= 1 OR (ISNULL(B.DT_END, '') <> '' AND DATEDIFF(DAY, GETDATE(), B.DT_END) <= 1))

DECLARE @V_SEQ		INT
DECLARE @V_NO_EMP	NVARCHAR(20)
DECLARE @V_CONTENTS NVARCHAR(1000)

DECLARE @sCOIDs		NVARCHAR(4000)
DECLARE @sUserIDs	NVARCHAR(4000)
DECLARE @sUserNMs	NVARCHAR(1000)
DECLARE @sGrpIDs	NVARCHAR(4000)

DECLARE @sMsgID		NVARCHAR(30)

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_SEQ,
								@V_NO_EMP,
								@V_CONTENTS

WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT @sCOIDs = STRING_AGG('1', ','),
		   @sUserIDs = STRING_AGG(user_id, ','),
		   @sUserNMs = STRING_AGG(user_nm_kr, ','),
		   @sGrpIDs = STRING_AGG('2330', ',')
	FROM NeoBizboxS2.BX.TCMG_USER
	WHERE EXISTS (SELECT 1 
				  FROM string_split(@V_NO_EMP, '|') A
				  WHERE A.value = logon_cd)

	EXEC NeoBizboxS2.BX.PMS_MSG_C @nGrpID = 2330,	
								  @nCOID = 1,	
								  @nUserID = 242, -- ERP(관리자)
								  @sBiz_yn = '0',
								  @sRev_yn = '0',
								  @sContent	= @V_CONTENTS,
								  @sTarget_url = '',
								  @sCOIDs = @sCOIDs,	
								  @sUserIDs = @sUserIDs,
								  @sDeptIDs	= '',
								  @sUserNMs	= @sUserNMs,
								  @sDeptNMs	= '',
								  @nEffect = 1073741824,
								  @nHeight = 180,
								  @nOffSet = 0,
								  @nTextclr = 0,
								  @sFaceNM = '맑은 고딕',
								  @nAttachMID = 0,
								  @nAttachDID = 0,
								  @sFileNMs = '',
								  @sFileSZs = '',
								  @sMsgID = @sMsgID,
								  @sInsertINF = 'Y',
								  @sGrpIDs = @sGrpIDs

	UPDATE CZ_PU_IV_CONFIRM
	SET YN_SEND = 'Y',
	    DTS_SEND = NEOE.SF_SYSDATE(GETDATE()),
		ID_UPDATE = 'SYSTEM',
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND SEQ = @V_SEQ

	FETCH NEXT FROM SRC_CURSOR INTO @V_SEQ,
									@V_NO_EMP,
									@V_CONTENTS
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

GO