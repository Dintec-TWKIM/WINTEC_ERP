USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MM_STOCK_WARNING]    Script Date: 2020-12-15 오후 2:03:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_MM_STOCK_WARNING]
(
	@P_CD_COMPANY	NVARCHAR(7)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_ID_INSERT    NVARCHAR(10)

CREATE TABLE #CZ_SA_STOCK_BOOK
(  
	NO_FILE         NVARCHAR(20),
    CD_ITEM         NVARCHAR(20),
    QT_INV          NUMERIC(15, 4),
    QT_REMAIN       NUMERIC(15, 4),
    QT_MINUS        NUMERIC(15, 4),
    ID_INSERT       NVARCHAR(10)
)

;WITH A AS
(
    SELECT SB.CD_COMPANY,
           SB.NO_FILE,
           SB.CD_ITEM,
           SB.QT_STOCK,
           SB.QT_BOOK,
           SB.QT_GI,
           SB.ID_INSERT,
           SUM(IIF(SB.QT_BOOK > ISNULL(SB.QT_GI, 0), SB.QT_BOOK - ISNULL(SB.QT_GI, 0), 0)) OVER (PARTITION BY SB.CD_ITEM ORDER BY SB.DTS_INSERT, SB.NO_FILE, SB.NO_LINE) AS QT_REMAIN
    FROM CZ_SA_STOCK_BOOK SB
    JOIN SA_SOH SH ON SH.CD_COMPANY = SB.CD_COMPANY AND SH.NO_SO = SB.NO_FILE
    WHERE SB.CD_COMPANY = @P_CD_COMPANY
    AND SB.QT_BOOK > ISNULL(SB.QT_GI, 0)
    AND ISNULL(SH.YN_CLOSE, 'N') = 'N'
    
),
B AS
(
    SELECT A.CD_COMPANY,
           A.NO_FILE,
           A.CD_ITEM,
           IIF(A.QT_BOOK > ISNULL(A.QT_GI, 0), A.QT_BOOK - ISNULL(A.QT_GI, 0), 0) AS QT_REMAIN,
           IV.QT_INV,
           (A.QT_REMAIN - IV.QT_INV) AS QT_MINUS,
           A.ID_INSERT
    FROM A
    LEFT JOIN (SELECT CD_COMPANY, CD_ITEM, 
                      (QT_GOOD_OPEN + QT_GOOD_GR - QT_GOOD_GI) AS QT_INV
               FROM MM_PINVN
               WHERE P_YR = CONVERT(NVARCHAR(4), GETDATE(), 112)
               AND CD_SL = 'SL02') IV
    ON IV.CD_COMPANY = A.CD_COMPANY AND IV.CD_ITEM = A.CD_ITEM
    WHERE A.QT_REMAIN > IV.QT_INV
)
INSERT INTO #CZ_SA_STOCK_BOOK
SELECT B.NO_FILE,
       B.CD_ITEM,
       B.QT_INV,
       B.QT_REMAIN,
       (CASE WHEN B.QT_REMAIN < B.QT_MINUS THEN B.QT_REMAIN ELSE B.QT_MINUS END) AS QT_MINUS,
       B.ID_INSERT
FROM B
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = B.CD_COMPANY AND MU.ID_USER = B.ID_INSERT
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = MU.CD_COMPANY AND ME.NO_EMP = MU.NO_EMP
WHERE ISNULL(ME.DT_RETIRE, '00000000') = '00000000'

DECLARE SRC_CURSOR CURSOR FOR
SELECT ID_INSERT 
FROM #CZ_SA_STOCK_BOOK
GROUP BY ID_INSERT

DECLARE @V_CONTENTS NVARCHAR(1000)

DECLARE @sCOIDs		NVARCHAR(4000)
DECLARE @sUserIDs	NVARCHAR(4000)
DECLARE @sUserNMs	NVARCHAR(1000)
DECLARE @sGrpIDs	NVARCHAR(4000)

DECLARE @sMsgID		NVARCHAR(30)

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_ID_INSERT

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @V_CONTENTS = '*** 재고수량 부족 알림' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10)

    SELECT @V_CONTENTS += STRING_AGG('파일번호 : ' + NO_FILE + CHAR(13) + CHAR(10) + 
                                     '재코코드 : ' + CD_ITEM + CHAR(13) + CHAR(10) + 
                                     '현재고 : ' + FORMAT(QT_INV, '#,##0') + CHAR(13) + CHAR(10) + 
                                     '미납수량 : ' + FORMAT(QT_REMAIN, '#,##0') + CHAR(13) + CHAR(10) + 
                                     '부족수량 : ' + FORMAT(QT_MINUS, '#,##0'), CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10))   
    FROM #CZ_SA_STOCK_BOOK
    WHERE ID_INSERT = @V_ID_INSERT

    SET @V_CONTENTS += CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + '업무에 참고하시기 바랍니다.
    
※ 본 쪽지는 발신 전용 입니다.'

    SELECT @sCOIDs = STRING_AGG('1', ','),
		   @sUserIDs = STRING_AGG(user_id, ','),
		   @sUserNMs = STRING_AGG(user_nm_kr, ','),
		   @sGrpIDs = STRING_AGG('2330', ',')
	FROM NeoBizboxS2.BX.TCMG_USER
	WHERE EXISTS (SELECT 1 
				  FROM string_split(@V_ID_INSERT, '|') A
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
    
    FETCH NEXT FROM SRC_CURSOR INTO @V_ID_INSERT
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

DROP TABLE #CZ_SA_STOCK_BOOK

GO