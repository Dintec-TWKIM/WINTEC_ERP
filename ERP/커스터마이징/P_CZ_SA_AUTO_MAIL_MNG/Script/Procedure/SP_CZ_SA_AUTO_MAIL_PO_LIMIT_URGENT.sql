USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_AUTO_MAIL_PO_LIMIT_URGENT]    Script Date: 2020-12-15 오후 2:03:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_AUTO_MAIL_PO_LIMIT_URGENT]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_EMAIL            NVARCHAR(20),
	@P_NO_SO            NVARCHAR(20),
	@P_DC_RMK           NVARCHAR(200)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_CD_PARTNER	    NVARCHAR(20)
DECLARE @V_LN_PARTNER	    NVARCHAR(50)
DECLARE @V_CD_PJT           NVARCHAR(20)
DECLARE @V_NO_ORDER         NVARCHAR(200)
DECLARE @V_CD_AREA          NVARCHAR(4)
DECLARE @V_TO_EMAIL	        NVARCHAR(MAX)

DECLARE SRC_CURSOR CURSOR FOR
SELECT PH.CD_PARTNER,
       MP.LN_PARTNER,
       MP.CD_AREA,
       PH.CD_PJT,
       PH.NO_ORDER,
       (SELECT STRING_AGG(A.NM_EMAIL, ';') 
		FROM (SELECT DISTINCT FP.NM_EMAIL 
			  FROM FI_PARTNERPTR FP
			  WHERE FP.CD_COMPANY = PH.CD_COMPANY
			  AND FP.CD_PARTNER = PH.CD_PARTNER
			  AND FP.YN_LIMIT = 'Y') A) AS TO_EMAIL
FROM PU_POH PH
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
WHERE PH.CD_COMPANY = @P_CD_COMPANY
AND PH.CD_PJT = @P_NO_SO
AND EXISTS (SELECT 1 
            FROM PU_POL PL
            WHERE PL.CD_COMPANY = PH.CD_COMPANY
            AND PL.NO_PO = PH.NO_PO
            AND PL.QT_PO > PL.QT_RCV)

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_CD_PARTNER, @V_LN_PARTNER, @V_CD_AREA, @V_CD_PJT, @V_NO_ORDER, @V_TO_EMAIL

WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @V_SUBJECT NVARCHAR(50)
	DECLARE @V_HTML NVARCHAR(MAX)
	DECLARE @V_BODY	NVARCHAR(MAX) = ''

	IF @V_CD_AREA = '100'
		SET @V_SUBJECT = '<긴급> [딘텍] ' + @V_CD_PJT + '건 납기 확인 요청드립니다.'
	ELSE
		SET @V_SUBJECT = '<Urgent> [Dintec] ' + @V_CD_PJT + 'REQUEST FOR READINESS'

    IF @V_CD_AREA = '100'
	BEGIN
		SET @V_HTML = '<div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   수신 : ' + @V_LN_PARTNER + ' 담당자 님<br>
					   발신 : (주)딘텍 영업물류팀<br><br>
					   귀사의 일익 번창하심을 기원합니다.<br><br>
					   납기 확인 요청 드리오니, <span style="background:yellow;mso-highlight:yellow">일정 검토 후 하기 입고예정일란에 기입하여 회신 부탁 드립니다.</span><br><br>
					   </div>
					   
                       <table style="width:400px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;">
					   <colgroup width="150px" align="center"></colgroup>
                       <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <tbody>
						<tr style="height:30px">
                         <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">딘텍 Ref No.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">매입처 Ref No.</th>
						 <th style="border:solid 1px black; color: #FF0000; text-align:center; background-color:Silver; width:100px">입고예정일<br>(년/월/일)</th>
						</tr>
                        <tr style="height:30px">
                         <th style="border:solid 1px black; text-align:left; font-weight:normal; width:150px">' + @V_CD_PJT + '</th>
                         <th style="border:solid 1px black; text-align:left; font-weight:normal; width:150px">' + @V_NO_ORDER + '</th>
                         <th style="border:solid 1px black; text-align:center; font-weight:normal; width:100px">' + '' + '</th>
				        </tr>
                       </tbody>
					   </table>

					   <div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   <br>' + (CASE WHEN ISNULL(@P_DC_RMK, '') = '' THEN '' ELSE @P_DC_RMK + '<br><br>' END) + '감사합니다.
					   </div>'
	END
	ELSE
	BEGIN
		SET @V_HTML = '<div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   Good day, sir/ madam.<br><br>
					   Kindly find below the list of open orders still pending.<br>
					   Please advise readiness for each in <span style="background:yellow;mso-highlight:yellow">“EXPECTED READINESS”</span> column:<br><br>
					   </div>

                       <table style="width:400px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;">
					   <colgroup width="150px" align="center"></colgroup>
                       <colgroup width="150px" align="center"></colgroup>
					   <colgroup width="100px" align="center"></colgroup>
					   <tbody>
						<tr style="height:30px">
                         <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">OUR REF NO.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:Silver; width:150px">YOUR REF NO.</th>
						 <th style="border:solid 1px black; color: #FF0000; text-align:center; background-color:Silver; width:100px">EXPECTED READINESS<br>(YYYY/MM/DD)</th>
						</tr>
                        <tr style="height:30px">
                         <th style="border:solid 1px black; text-align:left; font-weight:normal; width:150px">' + @V_CD_PJT + '</th>
                         <th style="border:solid 1px black; text-align:left; font-weight:normal; width:150px">' + @V_NO_ORDER + '</th>
                         <th style="border:solid 1px black; text-align:center; font-weight:normal; width:100px">' + '' + '</th>
				        </tr>
                       </tbody>
					   </table>
					   
					   <div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   <br>' + (CASE WHEN ISNULL(@P_DC_RMK, '') = '' THEN '' ELSE @P_DC_RMK + '<br><br>' END) + 'Awaiting your feedback
					   </div>'
	END

    SET @V_TO_EMAIL = 'khkim@dintec.co.kr'

    EXEC msdb.dbo.sp_send_dbmail @PROFILE_NAME = 'AUTO_MAIL',	
								 @RECIPIENTS = @V_TO_EMAIL,
								 @COPY_RECIPIENTS = @P_EMAIL,
								 @FROM_ADDRESS = @P_EMAIL,
								 @REPLY_TO = @P_EMAIL,
								 @SUBJECT = @V_SUBJECT,	
								 @BODY = @V_HTML,	
								 @BODY_FORMAT = 'HTML'

	FETCH NEXT FROM SRC_CURSOR INTO @V_CD_PARTNER, @V_LN_PARTNER, @V_CD_AREA, @V_CD_PJT, @V_NO_ORDER, @V_TO_EMAIL
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

GO