SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                                                        
ALTER PROCEDURE [NEOE].[SP_CZ_SA_AUTO_MAIL_VESSEL_MSG]                                                      
(                                                      
	@P_CD_COMPANY	NVARCHAR(7),
    @P_NO_SO        NVARCHAR(20)		
)                                                      
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_CD_PARTNER   NVARCHAR(20)
DECLARE @V_LN_PARTNER   NVARCHAR(50)
DECLARE @V_NO_IMO       NVARCHAR(10)
DECLARE @V_NM_VESSEL    NVARCHAR(50)
DECLARE @V_NO_EMP       NVARCHAR(50)
DECLARE @V_NO_SO		NVARCHAR(20)

DECLARE @V_CONTENTS		NVARCHAR(MAX)

DECLARE @sCOIDs		    NVARCHAR(4000)
DECLARE @sUserIDs	    NVARCHAR(4000)
DECLARE @sUserNMs	    NVARCHAR(1000)
DECLARE @sGrpIDs	    NVARCHAR(4000)

DECLARE @sMsgID		    NVARCHAR(30)

DECLARE SRC_CURSOR CURSOR FOR
SELECT SH.CD_PARTNER, SH.NO_IMO,
       MAX(MP.LN_PARTNER) AS NM_PARTNER,
       MAX(MH.NM_VESSEL) AS NM_VESSEL,
	   MAX(SH.NO_SO) AS NO_SO,
       ISNULL(MAX(VE.CD_FLAG1), '') AS NO_EMP
FROM SA_SOH SH
LEFT JOIN V_CZ_SA_QTN_LOG_EMP VE ON VE.CD_COMPANY = SH.CD_COMPANY AND VE.NO_FILE = SH.NO_SO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.NO_SO = @P_NO_SO
AND ISNULL(SH.NO_IMO, '') <> '' 
AND ISNULL(SH.CD_PARTNER_GRP, '') <> ''
AND EXISTS (SELECT 1 
			FROM CZ_SA_AUTO_MAIL_PARTNER AP
			WHERE AP.CD_COMPANY = SH.CD_COMPANY
			AND AP.CD_PARTNER = SH.CD_PARTNER
			AND AP.TP_PARTNER = '002'
		    AND ISNULL(AP.YN_READY_INFO, 'N') = 'Y')
AND NOT EXISTS (SELECT 1 
                FROM CZ_SA_AUTO_MAIL_VESSEL MV
                WHERE MV.CD_COMPANY = SH.CD_COMPANY
                AND MV.CD_PARTNER = SH.CD_PARTNER
                AND MV.NO_IMO = SH.NO_IMO
				AND ISNULL(MV.YN_SO, 'N') = 'Y')
GROUP BY SH.CD_PARTNER, SH.NO_IMO

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_CD_PARTNER, @V_NO_IMO, @V_LN_PARTNER, @V_NM_VESSEL, @V_NO_SO, @V_NO_EMP

WHILE @@FETCH_STATUS = 0
BEGIN
	SET @V_CONTENTS = '*** Ready Info 매출처 담당자 지정 요청 ' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
                      '매출처 : ' + @V_LN_PARTNER + ' (' + @V_CD_PARTNER + ')' + CHAR(13) + CHAR(10) +
                      '호선 : ' + @V_NM_VESSEL + ' (' + @V_NO_IMO + ')' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
					  '수주번호 : ' + @V_NO_SO + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
                      '자동메일발송관리 -> 매출처 -> 담당자(ReadyInfo)에 매출처 담당자 등록하시기 바랍니다.' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
					  '※ 본 쪽지는 발신전용 입니다.'

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
	
    FETCH NEXT FROM SRC_CURSOR INTO @V_CD_PARTNER, @V_NO_IMO, @V_LN_PARTNER, @V_NM_VESSEL, @V_NO_SO, @V_NO_EMP
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

GO
