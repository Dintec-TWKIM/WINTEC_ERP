SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_LOG_WORK_MSG]
(
	@P_CD_COMPANY			NVARCHAR(7)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_DT_FROM NVARCHAR(8) = CONVERT(CHAR(8), DATEADD(DAY, -7, GETDATE()), 112)
DECLARE @V_DT_TO   NVARCHAR(8) = CONVERT(CHAR(8), DATEADD(DAY, -3, GETDATE()), 112)
DECLARE @V_QT_GR INT
DECLARE @V_QT_GR_ITEM INT
DECLARE @V_QT_PACK INT
DECLARE @V_QT_PACK_ITEM INT
DECLARE @V_QT_GI INT
DECLARE @V_QT_GI_ITEM INT

SELECT @V_QT_GR = COUNT(DISTINCT NO_IO),
       @V_QT_GR_ITEM = COUNT(1)
FROM MM_QTIO
WHERE CD_COMPANY IN ('K100', 'K200')
AND DT_IO BETWEEN @V_DT_FROM AND @V_DT_TO
AND FG_PS = '1'
AND (NO_IO LIKE 'PDA%' OR NO_IO = 'GR%')

SELECT @V_QT_GI = COUNT(DISTINCT OL.NO_IO),
       @V_QT_GI_ITEM = COUNT(1)
FROM MM_QTIO OL
WHERE OL.CD_COMPANY IN ('K100', 'K200')
AND OL.DT_IO BETWEEN @V_DT_FROM AND @V_DT_TO
AND OL.FG_PS = '2'
AND (OL.NO_IO LIKE 'DN%' OR OL.NO_IO LIKE 'DO%')
AND EXISTS (SELECT 1 
            FROM CZ_SA_QTNL QL
            WHERE QL.CD_COMPANY = OL.CD_COMPANY
            AND QL.NO_FILE = OL.NO_PSO_MGMT
            AND QL.NO_LINE = OL.NO_PSOLINE_MGMT)

SELECT @V_QT_PACK = COUNT(1),
       @V_QT_PACK_ITEM = SUM(PL.QT_ITEM)
FROM CZ_SA_PACKH PH
LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK,
                  COUNT(1) AS QT_ITEM
           FROM NEOE.CZ_SA_PACKL PL
           GROUP BY PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK) PL
ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR AND PL.NO_PACK = PH.NO_PACK
WHERE PH.CD_COMPANY IN ('K100', 'K200')
AND PH.DT_PACK BETWEEN @V_DT_FROM AND @V_DT_TO

DECLARE @V_CONTENTS			   NVARCHAR(MAX)

DECLARE @sCOIDs		    NVARCHAR(4000)
DECLARE @sUserIDs	    NVARCHAR(4000)
DECLARE @sUserNMs	    NVARCHAR(1000)
DECLARE @sGrpIDs	    NVARCHAR(4000)

DECLARE @sMsgID		    NVARCHAR(30)

SET @V_CONTENTS = '*** 지난주 물류업무 건수 알림' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
                  '대상기간 : ' + CONVERT(CHAR(10), CONVERT(DATETIME, @V_DT_FROM), 23) + ' ~ ' + CONVERT(CHAR(10), CONVERT(DATETIME, @V_DT_TO), 23) + CHAR(13) + CHAR(10) +
                  '입고 : ' + CONVERT(NVARCHAR, FORMAT(@V_QT_GR, '#,#')) + '건 (' + CONVERT(NVARCHAR, FORMAT(@V_QT_GR_ITEM, '#,#')) + '종)' + CHAR(13) + CHAR(10) +
                  '포장 : ' + CONVERT(NVARCHAR, FORMAT(@V_QT_PACK, '#,#')) + '건 (' + CONVERT(NVARCHAR, FORMAT(@V_QT_PACK_ITEM, '#,#')) + '종)' + CHAR(13) + CHAR(10) +
                  '출고 : ' + CONVERT(NVARCHAR, FORMAT(@V_QT_GI, '#,#')) + '건 (' + CONVERT(NVARCHAR, FORMAT(@V_QT_GI_ITEM, '#,#')) + '종)' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) +
				  '※ 본 쪽지는 발신전용 입니다.'

SELECT @sCOIDs = STRING_AGG('1', ','),
	   @sUserIDs = STRING_AGG(user_id, ','),
	   @sUserNMs = STRING_AGG(user_nm_kr, ','),
	   @sGrpIDs = STRING_AGG('2330', ',')
FROM NeoBizboxS2.BX.TCMG_USER
WHERE EXISTS (SELECT 1 
			  FROM string_split('S-391|S-343|S-223', '|') A
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

GO
