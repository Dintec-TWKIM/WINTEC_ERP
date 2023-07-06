SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIR_REMAIN_MSG]
(
	@P_CD_COMPANY			NVARCHAR(7)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_NO_GIR   NVARCHAR(20)
DECLARE @V_DT_GIR   NVARCHAR(10)
DECLARE @V_NO_EMP   NVARCHAR(20)
DECLARE @V_ID_LOG   NVARCHAR(20)
DECLARE @V_QT_DIFF  INT

DECLARE @V_CONTENTS     NVARCHAR(MAX)

DECLARE @sCOIDs		    NVARCHAR(4000)
DECLARE @sUserIDs	    NVARCHAR(4000)
DECLARE @sUserNMs	    NVARCHAR(1000)
DECLARE @sGrpIDs	    NVARCHAR(4000)
DECLARE @sMsgID		    NVARCHAR(30)

DECLARE SRC_CURSOR CURSOR FOR
SELECT GH.NO_GIR,
       FORMAT(CONVERT(DATETIME, GH.DT_GIR), 'yyyy/MM/dd') AS DT_GIR,
       GH.NO_EMP,
       (SELECT WH.ID_LOG 
        FROM CZ_MA_WORKFLOWH WH
        WHERE WH.CD_COMPANY = GL.CD_COMPANY
        AND WH.NO_KEY = GL.NO_SO
        AND WH.TP_STEP = '08') AS ID_LOG,
       DATEDIFF(DAY, GH.DT_GIR, GETDATE()) AS QT_DIFF
FROM SA_GIRH GH
JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR,
             MAX(GL.NO_SO) AS NO_SO
      FROM SA_GIRL GL
      JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_FILE = GL.NO_SO AND QL.NO_LINE = GL.SEQ_SO
      GROUP BY GL.CD_COMPANY, GL.NO_GIR) GL
ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
WHERE GH.CD_COMPANY = @P_CD_COMPANY
AND ISNULL(GH.STA_GIR, '') = ''
AND ISNULL(GH.YN_RETURN, 'N') = 'N'
AND DATEDIFF(DAY, GH.DT_GIR, GETDATE()) >= 1
UNION ALL
SELECT PH.NO_GIR,
       FORMAT(CONVERT(DATETIME, PH.DT_GIR), 'yyyy/MM/dd') AS DT_GIR,
       PH.NO_EMP,
       (SELECT WH.ID_LOG 
        FROM CZ_MA_WORKFLOWH WH
        WHERE WH.CD_COMPANY = PL.CD_COMPANY
        AND WH.NO_KEY = PL.NO_SO
        AND WH.TP_STEP = '08') AS ID_LOG,
       DATEDIFF(DAY, PH.DT_GIR, GETDATE()) AS QT_DIFF 
FROM CZ_SA_GIRH_PACK PH
JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR,
             MAX(GL.NO_SO) AS NO_SO
      FROM CZ_SA_GIRL_PACK GL
      JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_FILE = GL.NO_SO AND QL.NO_LINE = GL.SEQ_SO
      GROUP BY GL.CD_COMPANY, GL.NO_GIR) PL
ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR
WHERE PH.CD_COMPANY = @P_CD_COMPANY
AND ISNULL(PH.STA_GIR, '') = ''
AND ISNULL(PH.YN_RETURN, 'N') = 'N'
AND DATEDIFF(DAY, PH.DT_GIR, GETDATE()) >= 1

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_NO_GIR, @V_DT_GIR, @V_NO_EMP, @V_ID_LOG, @V_QT_DIFF

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @V_CONTENTS = '*** 미제출 협조전 알림' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
                      '협조전번호 : ' + @V_NO_GIR + CHAR(13) + CHAR(10) +
                      '의뢰일자 : ' + @V_DT_GIR + CHAR(13) + CHAR(10) +
                      '경과일수 : ' + CONVERT(NVARCHAR, @V_QT_DIFF) + '일 경과' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
    				  '※ 본 쪽지는 발신전용 입니다.'

    SELECT @sCOIDs = STRING_AGG('1', ','),
    	   @sUserIDs = STRING_AGG(user_id, ','),
    	   @sUserNMs = STRING_AGG(user_nm_kr, ','),
    	   @sGrpIDs = STRING_AGG('2330', ',')
    FROM NeoBizboxS2.BX.TCMG_USER
    WHERE logon_cd = (CASE WHEN @V_NO_EMP = 'SYSADMIN' THEN @V_ID_LOG ELSE @V_NO_EMP END)

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
    
    FETCH NEXT FROM SRC_CURSOR INTO @V_NO_GIR, @V_DT_GIR, @V_NO_EMP, @V_ID_LOG, @V_QT_DIFF
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

DECLARE SRC_CURSOR CURSOR FOR
SELECT GH.NO_GIR,
       FORMAT(CONVERT(DATETIME, LEFT(WD.DTS_CONFIRM, 8)), 'yyyy/MM/dd') AS DT_CONFIRM,
       GH.NO_EMP,
       (SELECT WH.ID_LOG 
        FROM CZ_MA_WORKFLOWH WH
        WHERE WH.CD_COMPANY = GL.CD_COMPANY
        AND WH.NO_KEY = GL.NO_SO
        AND WH.TP_STEP = '08') AS ID_LOG,
       DATEDIFF(DAY, LEFT(WD.DTS_CONFIRM, 8), GETDATE()) AS QT_DIFF
FROM SA_GIRH GH
JOIN CZ_SA_GIRH_WORK_DETAIL WD ON WD.CD_COMPANY = GH.CD_COMPANY AND WD.NO_GIR = GH.NO_GIR
JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR,
             MAX(GL.NO_SO) AS NO_SO
      FROM SA_GIRL GL
      JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_FILE = GL.NO_SO AND QL.NO_LINE = GL.SEQ_SO
      GROUP BY GL.CD_COMPANY, GL.NO_GIR) GL
ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
WHERE GH.CD_COMPANY = @P_CD_COMPANY
AND ISNULL(GH.STA_GIR, '') = 'R'
AND ISNULL(GH.YN_RETURN, 'N') = 'N'
AND DATEDIFF(DAY, LEFT(WD.DTS_CONFIRM, 8), GETDATE()) >= 7
UNION ALL
SELECT PH.NO_GIR,
       FORMAT(CONVERT(DATETIME, LEFT(PD.DTS_CONFIRM, 8)), 'yyyy/MM/dd') AS DT_CONFIRM,
       PH.NO_EMP,
       (SELECT WH.ID_LOG 
        FROM CZ_MA_WORKFLOWH WH
        WHERE WH.CD_COMPANY = PL.CD_COMPANY
        AND WH.NO_KEY = PL.NO_SO
        AND WH.TP_STEP = '08') AS ID_LOG,
       DATEDIFF(DAY, LEFT(PD.DTS_CONFIRM, 8), GETDATE()) AS QT_DIFF 
FROM CZ_SA_GIRH_PACK PH
JOIN CZ_SA_GIRH_PACK_DETAIL PD ON PD.CD_COMPANY = PH.CD_COMPANY AND PD.NO_GIR = PH.NO_GIR
JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR,
             MAX(GL.NO_SO) AS NO_SO
      FROM CZ_SA_GIRL_PACK GL
      JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_FILE = GL.NO_SO AND QL.NO_LINE = GL.SEQ_SO
      GROUP BY GL.CD_COMPANY, GL.NO_GIR) PL
ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR
WHERE PH.CD_COMPANY = @P_CD_COMPANY
AND ISNULL(PH.STA_GIR, '') = 'R'
AND ISNULL(PH.YN_RETURN, 'N') = 'N'
AND DATEDIFF(DAY, LEFT(PD.DTS_CONFIRM, 8), GETDATE()) >= 7

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_NO_GIR, @V_DT_GIR, @V_NO_EMP, @V_ID_LOG, @V_QT_DIFF

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @V_CONTENTS = '*** 미종결 협조전 알림' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
                      '협조전번호 : ' + @V_NO_GIR + CHAR(13) + CHAR(10) +
                      '확정일자 : ' + @V_DT_GIR + CHAR(13) + CHAR(10) +
                      '경과일수 : ' + CONVERT(NVARCHAR, @V_QT_DIFF) + '일 경과' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
    				  '※ 본 쪽지는 발신전용 입니다.'

    SELECT @sCOIDs = STRING_AGG('1', ','),
    	   @sUserIDs = STRING_AGG(user_id, ','),
    	   @sUserNMs = STRING_AGG(user_nm_kr, ','),
    	   @sGrpIDs = STRING_AGG('2330', ',')
    FROM NeoBizboxS2.BX.TCMG_USER
    WHERE logon_cd = (CASE WHEN @V_NO_EMP = 'SYSADMIN' THEN @V_ID_LOG ELSE @V_NO_EMP END)

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
    
    FETCH NEXT FROM SRC_CURSOR INTO @V_NO_GIR, @V_DT_GIR, @V_NO_EMP, @V_ID_LOG, @V_QT_DIFF
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

GO
