USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_SEND_MSG]    Script Date: 2016-11-17 오후 5:00:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
    
ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_SEND_MSG]        
(             
	@P_CD_COMPANY           NVARCHAR(7)
)        
AS

IF EXISTS (SELECT 1 
		   FROM MA_CALENDAR
		   WHERE CD_COMPANY = @P_CD_COMPANY
		   AND FG1_HOLIDAY = 'W'
		   AND DT_CAL = CONVERT(CHAR(8), NEOE.SF_SYSDATE(GETDATE()), 112))
BEGIN
	DECLARE SRC_CURSOR CURSOR FOR

	WITH A AS
	(
		SELECT WH.NO_KEY,
			   WH.ID_PUR,
			   MU.NM_USER AS NM_PUR,
			   (ISNULL(WH.CD_RMK, '0') + 1) AS CD_RMK,
			   MP.LN_PARTNER,
			   CONVERT(DATETIME, LEFT(WH.DTS_INSERT, 8) + ' ' + 
				   				     SUBSTRING(WH.DTS_INSERT, 9, 2) + ':' + 
				   				     SUBSTRING(WH.DTS_INSERT, 11, 2) + ':' + 
				   				     SUBSTRING(WH.DTS_INSERT, 13, 2)) AS DTS_INSERT,
			   (SELECT COUNT(1) 
				FROM MA_CALENDAR
				WHERE CD_COMPANY = WH.CD_COMPANY
				AND FG1_HOLIDAY = 'H'
				AND DT_CAL BETWEEN LEFT(WH.DTS_INSERT, 8) AND CONVERT(CHAR(8), GETDATE(), 112)) AS QT_HOLIDAY
		FROM CZ_MA_WORKFLOWH WH
		LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = WH.CD_COMPANY AND QH.NO_FILE = WH.NO_KEY
		LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = WH.CD_COMPANY AND MP.CD_PARTNER = WH.ID_LOG
		LEFT JOIN MA_USER MU ON MU.CD_COMPANY = WH.CD_COMPANY AND MU.ID_USER = WH.ID_PUR
		WHERE WH.CD_COMPANY = @P_CD_COMPANY
		AND WH.TP_STEP = '21'
		AND ISNULL(WH.YN_DONE, 'N') = 'N'
		AND ISNULL(QH.YN_CLOSE, 'N') = 'N'
	),
	B AS
	(
		SELECT A.NO_KEY,
			   A.ID_PUR,
			   A.NM_PUR,
			   A.CD_RMK,
			   A.LN_PARTNER,
			   (DATEDIFF(SECOND, A.DTS_INSERT, GETDATE()) / 86400) - A.QT_HOLIDAY AS QT_DAY,
			   CONVERT(VARCHAR, DATEADD(S, DATEDIFF(SECOND, A.DTS_INSERT, GETDATE()) % 86400, ''), 8) AS DT_HHMMSS
		FROM A
	)
	SELECT B.NO_KEY,
		   B.ID_PUR + '|' + 'S-495|S-448' AS NO_EMP,
		   B.CD_RMK,
		   (CASE WHEN B.QT_DAY >= 10 THEN '** 재고견적 지연 알림(2차-' +  CONVERT(NVARCHAR, B.CD_RMK) + '번째) - 파일번호 : ' + B.NO_KEY + ' (' + B.NM_PUR + ')' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
										  '요청매입처 : ' + B.LN_PARTNER + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
										  '경과시간 : ' + (TRIM(CONVERT(CHAR(4), B.QT_DAY)) + '일 ' + B.DT_HHMMSS) + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
										  '사유 확인하여 이수훈DR, 김태연JI 에게 쪽지 수신일 안에 회신바랍니다.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
										  '(특이사항 없는 견적 지연 시 부서장 보고 예정)'
								     ELSE '** 재고견적 지연 알림(1차-' +  CONVERT(NVARCHAR, B.CD_RMK) + '번째) - 파일번호 : ' + B.NO_KEY + ' (' + B.NM_PUR + ')' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
										  '요청매입처 : ' + B.LN_PARTNER + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
										  '경과시간 : ' + (TRIM(CONVERT(CHAR(4), B.QT_DAY)) + '일 ' + B.DT_HHMMSS) + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
										  '사유 확인하여 이수훈DR, 김태연JI 에게 쪽지 수신일 안에 회신바랍니다.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
										  '(사유 워크플로우-견적비고 에도 작성할 것)' END) AS DC_CONTENTS
	FROM B
	WHERE B.QT_DAY >= 3
	
	DECLARE @V_NO_KEY	NVARCHAR(20)
	DECLARE @V_NO_EMP	NVARCHAR(20)
	DECLARE @V_CD_RMK	NVARCHAR(4)
	DECLARE @V_CONTENTS NVARCHAR(1000)
	
	DECLARE @sCOIDs		NVARCHAR(4000)
	DECLARE @sUserIDs	NVARCHAR(4000)
	DECLARE @sUserNMs	NVARCHAR(1000)
	DECLARE @sGrpIDs	NVARCHAR(4000)
	
	DECLARE @sMsgID		NVARCHAR(30)
	
	OPEN SRC_CURSOR
	FETCH NEXT FROM SRC_CURSOR INTO @V_NO_KEY,
									@V_NO_EMP,
									@V_CD_RMK,
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
	
		UPDATE CZ_MA_WORKFLOWH
		SET CD_RMK = @V_CD_RMK
		WHERE CD_COMPANY = @P_CD_COMPANY
		AND TP_STEP = '21'
		AND NO_KEY = @V_NO_KEY
	
		FETCH NEXT FROM SRC_CURSOR INTO @V_NO_KEY,
										@V_NO_EMP,
										@V_CD_RMK,
										@V_CONTENTS
	END
	
	CLOSE SRC_CURSOR
	DEALLOCATE SRC_CURSOR
END

GO