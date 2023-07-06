USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_AUTO_MAIL_SA_READY]    Script Date: 2020-12-15 오후 2:03:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_AUTO_MAIL_SA_READY]
(
	@P_CD_COMPANY	NVARCHAR(7)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_DT_TO CHAR(8) = CONVERT(CHAR(8), GETDATE(), 112)

CREATE TABLE #CZ_SA_AUTO_MAIL_READY_INFO
(  
	CD_PARTNER      NVARCHAR(20),
    LN_PARTNER      NVARCHAR(50),
	NO_IMO			NVARCHAR(10),
    NM_VESSEL       NVARCHAR(50),
	NO_EMP			NVARCHAR(10),
	CD_PARTNER_GRP	NVARCHAR(10),
    NO_PO_PARTNER   NVARCHAR(50),
    DT_SO           NVARCHAR(8),
    NO_SO           NVARCHAR(20),
    DT_IN           NVARCHAR(8),
    YN_EXCLUDE		NVARCHAR(1),
	YN_INCLUDE		NVARCHAR(1),
	DC_RMK			NVARCHAR(50),
	TO_EMAIL		NVARCHAR(MAX),
	WEIGHT          NVARCHAR(20),
	CD_FLAG1		NVARCHAR(2000),
	CD_FLAG2		NVARCHAR(2000)
)

CREATE TABLE #EWS
(
    CD_COMPANY  NVARCHAR(7),
    CD_PARTNER  NVARCHAR(20),
    LN_PARTNER  NVARCHAR(50),
    DT_TODAY    NVARCHAR(8),
    WN_LEVEL    INT
)

INSERT #CZ_SA_AUTO_MAIL_READY_INFO
EXEC SP_CZ_SA_AUTO_MAIL_READY_INFO @P_CD_COMPANY, NULL, '20160101', @V_DT_TO, NULL, 'Y', NULL

INSERT #EWS
EXEC SP_CZ_SA_OUTSTANDING_INVOICE_S @P_CD_COMPANY

DECLARE @V_CD_PARTNER	    NVARCHAR(20)
DECLARE @V_LN_PARTNER	    NVARCHAR(50)
DECLARE @V_NO_IMO			NVARCHAR(10)
DECLARE @V_NM_VESSEL		NVARCHAR(50)
DECLARE @V_CD_PARTNER_GRP	NVARCHAR(10)
DECLARE @V_FROM_EMAIL	    NVARCHAR(MAX)
DECLARE @V_TO_EMAIL	        NVARCHAR(MAX)
DECLARE @V_CC_EMAIL			NVARCHAR(MAX)
DECLARE @V_CD_AREA			NVARCHAR(3)

DECLARE SRC_CURSOR CURSOR FOR
WITH A AS
(
	SELECT RI.CD_PARTNER, RI.NO_IMO, RI.NM_VESSEL, RI.TO_EMAIL, RI.CD_PARTNER_GRP, RI.CD_FLAG1, RI.CD_FLAG2
	FROM #CZ_SA_AUTO_MAIL_READY_INFO RI
	WHERE ISNULL(RI.TO_EMAIL, '') <> ''
	AND NOT EXISTS (SELECT 1 
				    FROM #EWS ES
					WHERE ES.CD_PARTNER = RI.CD_PARTNER
					AND ES.WN_LEVEL = 2)
	GROUP BY RI.CD_PARTNER, RI.NO_IMO, RI.NM_VESSEL, RI.TO_EMAIL, RI.CD_PARTNER_GRP, RI.CD_FLAG1, RI.CD_FLAG2
)
SELECT A.CD_PARTNER,
       MP.LN_PARTNER,
	   A.NO_IMO,
	   A.NM_VESSEL,
       A.CD_PARTNER_GRP,
       'DINTEC<' +  ISNULL(ME.NO_EMAIL, '') + '>' AS FROM_EMAIL,
	   ISNULL(ME.NO_EMAIL, '') AS CC_EMAIL,
       A.TO_EMAIL,
	   MP.CD_AREA
FROM A
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = @P_CD_COMPANY AND MP.CD_PARTNER = A.CD_PARTNER
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = @P_CD_COMPANY AND ME.NO_EMP = A.CD_FLAG1
WHERE A.CD_FLAG1 IS NOT NULL

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_CD_PARTNER, @V_LN_PARTNER, @V_NO_IMO, @V_NM_VESSEL, @V_CD_PARTNER_GRP, @V_FROM_EMAIL, @V_CC_EMAIL, @V_TO_EMAIL, @V_CD_AREA

WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @V_SUBJECT NVARCHAR(100)
	DECLARE @V_HTML NVARCHAR(MAX)
	DECLARE @V_BODY	NVARCHAR(MAX) = ''
	DECLARE @V_HTML_RED NVARCHAR(MAX)

	DECLARE @V_COUNT			INT
	DECLARE @VV_NO_IMO			NVARCHAR(10)
	DECLARE @VV_NM_VESSEL		NVARCHAR(50)
	DECLARE @VV_NO_PO_PARTNER	NVARCHAR(50)
	DECLARE @VV_NO_SO			NVARCHAR(20)

	IF EXISTS (SELECT 1 
			   FROM CZ_SA_AUTO_MAIL_SA_READY_LOG SL
			   WHERE SL.CD_COMPANY =  @P_CD_COMPANY
			   AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
			   AND SL.CD_PARTNER = @V_CD_PARTNER
			   AND SL.NO_IMO = @V_NO_IMO
			   AND SL.CD_PARTNER_GRP = @V_CD_PARTNER_GRP
			   AND SL.TO_EMAIL = @V_TO_EMAIL)
	BEGIN
		DELETE SL
		FROM CZ_SA_AUTO_MAIL_SA_READY_LOG SL
		WHERE SL.CD_COMPANY =  @P_CD_COMPANY
		AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
		AND SL.CD_PARTNER = @V_CD_PARTNER
		AND SL.NO_IMO = @V_NO_IMO
	    AND SL.CD_PARTNER_GRP = @V_CD_PARTNER_GRP
	    AND SL.TO_EMAIL = @V_TO_EMAIL
	END

	INSERT INTO CZ_SA_AUTO_MAIL_SA_READY_LOG
	(
	    CD_COMPANY,
		DT_SEND,
		CD_PARTNER,
		NO_IMO,
		CD_PARTNER_GRP,
		TO_EMAIL,
		IDX,
		NM_VESSEL,
	    NO_PO_PARTNER,
	    DT_SO,
	    NO_SO,
	    DT_IN,
	    WEIGHT,
		DC_RMK,
		ID_INSERT,
		DTS_INSERT
	)
	SELECT @P_CD_COMPANY AS CD_COMPANY,
		   CONVERT(CHAR(8), GETDATE(), 112) AS DT_SEND,
		   RI.CD_PARTNER,
		   RI.NO_IMO,
		   RI.CD_PARTNER_GRP,
		   RI.TO_EMAIL,
		   ROW_NUMBER() OVER (PARTITION BY RI.CD_PARTNER, RI.NO_IMO, RI.CD_PARTNER_GRP, RI.TO_EMAIL ORDER BY RI.DT_SO ASC) AS IDX,
		   RI.NM_VESSEL,
	       RI.NO_PO_PARTNER,
	       RI.DT_SO,
	       RI.NO_SO,
	       RI.DT_IN,
	       RI.WEIGHT,
		   RI.DC_RMK,
		   'SYSTEM' AS ID_INSERT,
		   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
	FROM #CZ_SA_AUTO_MAIL_READY_INFO RI
	WHERE RI.CD_PARTNER = @V_CD_PARTNER
	AND RI.NO_IMO = @V_NO_IMO
	AND RI.CD_PARTNER_GRP = @V_CD_PARTNER_GRP
	AND RI.TO_EMAIL = @V_TO_EMAIL

	SELECT @V_COUNT = ISNULL(COUNT(1), 0)
	FROM CZ_SA_AUTO_MAIL_SA_READY_LOG SL
	WHERE SL.CD_COMPANY = @P_CD_COMPANY
	AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
	AND SL.CD_PARTNER = @V_CD_PARTNER
	AND SL.NO_IMO = @V_NO_IMO
	AND SL.CD_PARTNER_GRP = @V_CD_PARTNER_GRP
	AND SL.TO_EMAIL = @V_TO_EMAIL

	IF @V_COUNT > 1
	BEGIN
		IF @V_CD_PARTNER = '00942'
		BEGIN
			SELECT @VV_NO_IMO = SL.NO_IMO,
				   @VV_NM_VESSEL = SL.NM_VESSEL,
				   @VV_NO_PO_PARTNER = MAX(SL.NO_PO_PARTNER)
			FROM CZ_SA_AUTO_MAIL_SA_READY_LOG SL
			WHERE SL.CD_COMPANY = @P_CD_COMPANY
			AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
			AND SL.CD_PARTNER = @V_CD_PARTNER
			AND SL.NO_IMO = @V_NO_IMO
			AND SL.CD_PARTNER_GRP = @V_CD_PARTNER_GRP
		    AND SL.TO_EMAIL = @V_TO_EMAIL
			GROUP BY SL.NO_IMO, SL.NM_VESSEL

			SET @V_SUBJECT = '[DINTEC] ' + @VV_NM_VESSEL + '(' + @VV_NO_IMO + ')' + '_' + @VV_NO_PO_PARTNER + ' - READY INFO ' + CONVERT(NVARCHAR, @V_COUNT) + ' ORDERS'
		END
		ELSE
		BEGIN
			SET @V_SUBJECT = '[DINTEC] ' + @V_NM_VESSEL + ' - READY INFO ' + CONVERT(NVARCHAR, @V_COUNT) + ' ORDERS' 
		END
	END
	ELSE
	BEGIN
		SELECT @VV_NO_IMO = SL.NO_IMO,
			   @VV_NM_VESSEL = SL.NM_VESSEL,
			   @VV_NO_PO_PARTNER = SL.NO_PO_PARTNER,
			   @VV_NO_SO = SL.NO_SO
		FROM CZ_SA_AUTO_MAIL_SA_READY_LOG SL
	    WHERE SL.CD_COMPANY = @P_CD_COMPANY
	    AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
	    AND SL.CD_PARTNER = @V_CD_PARTNER
	    AND SL.NO_IMO = @V_NO_IMO
	    AND SL.CD_PARTNER_GRP = @V_CD_PARTNER_GRP
	    AND SL.TO_EMAIL = @V_TO_EMAIL

		IF @V_CD_PARTNER = '00942'
		BEGIN
			SET @V_SUBJECT = '[DINTEC] ' + @VV_NM_VESSEL + '(' + @VV_NO_IMO + ')' + '_' + @VV_NO_PO_PARTNER + '/' + @VV_NO_SO + ' - READY INFO'
		END
		ELSE
		BEGIN
			SET @V_SUBJECT = '[DINTEC] ' + @V_NM_VESSEL + '_' + @VV_NO_PO_PARTNER + '/' + @VV_NO_SO + ' - READY INFO'
		END
	END

	;WITH A AS
	(
		SELECT SL.IDX,
		       '<tr style="height:30px">' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.WEIGHT, '') = 'Heavy' THEN 'color: #FF0000;' ELSE '' END) + ' font-weight:normal; width:200px">' + ISNULL(SL.NM_VESSEL, '') + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.WEIGHT, '') = 'Heavy' THEN 'color: #FF0000;' ELSE '' END) + ' font-weight:normal; width:200px">' + ISNULL(SL.NO_PO_PARTNER, '') + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.WEIGHT, '') = 'Heavy' THEN 'color: #FF0000;' ELSE '' END) + ' font-weight:normal; width:100px">' + ISNULL(SL.NO_SO, '') + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.WEIGHT, '') = 'Heavy' THEN 'color: #FF0000;' ELSE '' END) + ' font-weight:normal; width:100px">' + ISNULL(SL.WEIGHT, '') + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.WEIGHT, '') = 'Heavy' THEN 'color: #FF0000;' ELSE '' END) + ' font-weight:normal; width:100px">' + (CASE WHEN ISNULL(SL.DT_IN, '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, ISNULL(SL.DT_IN, '')), 23) END) + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.WEIGHT, '') = 'Heavy' THEN 'color: #FF0000;' ELSE '' END) + ' font-weight:normal; width:100px">' + ISNULL(SL.DC_RMK, '') + '</th>' +
			   '</tr>' AS DC_BODY
		FROM CZ_SA_AUTO_MAIL_SA_READY_LOG SL
		WHERE SL.CD_COMPANY = @P_CD_COMPANY
		AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
		AND SL.CD_PARTNER = @V_CD_PARTNER
		AND SL.NO_IMO = @V_NO_IMO
		AND SL.CD_PARTNER_GRP = @V_CD_PARTNER_GRP
	    AND SL.TO_EMAIL = @V_TO_EMAIL
	)
	SELECT @V_BODY = STRING_AGG(CONVERT(VARCHAR(MAX), A.DC_BODY), '') WITHIN GROUP (ORDER BY A.IDX ASC)
    FROM A

	IF ISNULL(@V_CD_AREA, '') = '100'
	BEGIN
		SET @V_HTML = '<div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   To : ' + @V_LN_PARTNER + '<br><br>
					   일일 업무에 수고가 많으십니다.<br><br>
					   하기 오더 준비 완료 되어 있으므로 송품 지시 부탁 드립니다.<br><br>
					   - Normal : 1-2 working days<br>
					   - Urgent : Notify us to make immediate arrangement.<br><br>
					   </div>
						  
					   <table style="width:800px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;">
						<colgroup width="200px" align="center"></colgroup>
						<colgroup width="200px" align="center"></colgroup>
						<colgroup width="100px" align="center"></colgroup>
						<colgroup width="100px" align="center"></colgroup>
						<colgroup width="100px" align="center"></colgroup>
						<colgroup width="100px" align="center"></colgroup>
						<tbody>
						<tr style="height:30px">
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:200px">Vessel</th>
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:200px">Order No.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Dintec No.</th>                                    
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Approx.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Ready date</th>
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Remark</th>
						</tr>'
						+ @V_BODY +
					   '</tbody>
					   </table>
					   
					   <div style="text-align:left; font-size:10pt; font-family:맑은 고딕; color:blue; font-weight:bold; text-decoration:underline;">
						※ Heavy parts : Packing needed for exact dimensions and weight.
					   </div>
					   
					   <div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;"><br>Thanks.</div>'	
	END
	ELSE
	BEGIN
		SET @V_HTML = '<div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   To : ' + @V_LN_PARTNER + '<br><br>
					   Thanks for your orders.<br><br>
					   This is to request you <span style="color:blue;text-decoration:underline">delivery instruction(s)</span> for following ready order(s).<br><br>
					   Kindly note the time required for delivery arrangement after instructions.<br><br>
					   - Normal : 1-2 working days<br>
					   - Urgent : Notify us to make immediate arrangement.<br><br>
					   </div>
						  
					   <table style="width:800px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;">
						<colgroup width="200px" align="center"></colgroup>
						<colgroup width="200px" align="center"></colgroup>
						<colgroup width="100px" align="center"></colgroup>
						<colgroup width="100px" align="center"></colgroup>
						<colgroup width="100px" align="center"></colgroup>
						<colgroup width="100px" align="center"></colgroup>
						<tbody>
						<tr style="height:30px">
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:200px">Vessel</th>
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:200px">Order No.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Dintec No.</th>                                    
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Approx.</th>
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Ready date</th>
						 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Remark</th>
						</tr>'
						+ @V_BODY +
					   '</tbody>
					   </table>
					   
					   <div style="text-align:left; font-size:10pt; font-family:맑은 고딕; color:blue; font-weight:bold; text-decoration:underline;">
						※ Heavy parts : Packing needed for exact dimensions and weight.
					   </div>

					   <div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
					   <br>Pick up address is as below<br><br>
					   DINTEC CO.,LTD<br>
					   2nd floor, 48, Eogokgongdan 2-gil,<br> 
					   Yangsan-si, Gyeongsangnam-do,<br>
					   Republic of Korea 50591<br><br>
					   TEL : +82 070 7430 3872
					   </div>
					   
					   <div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;"><br>Thanks.</div>'
	END

	BEGIN TRY
		--SET @V_TO_EMAIL = 'khkim@dintec.co.kr'
		--SET @V_FROM_EMAIL = 'khkim@dintec.co.kr'
		--SET @V_CC_EMAIL = 'khkim@dintec.co.kr'

		EXEC msdb.dbo.sp_send_dbmail @PROFILE_NAME = 'AUTO_MAIL',	
								     @RECIPIENTS = @V_TO_EMAIL,
									 @COPY_RECIPIENTS = @V_CC_EMAIL,
								     @FROM_ADDRESS = @V_FROM_EMAIL,
								     @REPLY_TO = @V_CC_EMAIL,
								     @SUBJECT = @V_SUBJECT,	
								     @BODY = @V_HTML,	
								     @BODY_FORMAT = 'HTML'
	END TRY
	BEGIN CATCH
		DECLARE @V_SUBJECT2 NVARCHAR(500) = 'Ready Info 자동 메일 발송 오류 발생 (' + @V_CD_PARTNER + ')'
		DECLARE @V_ERROR NVARCHAR(1000) = ERROR_MESSAGE()

		EXEC msdb.dbo.sp_send_dbmail @PROFILE_NAME = 'AUTO_MAIL',	
							         @RECIPIENTS = @V_FROM_EMAIL,
							         @SUBJECT = @V_SUBJECT2,	
							         @BODY = @V_ERROR
	END CATCH

	FETCH NEXT FROM SRC_CURSOR INTO @V_CD_PARTNER, @V_LN_PARTNER, @V_NO_IMO, @V_NM_VESSEL, @V_CD_PARTNER_GRP, @V_FROM_EMAIL, @V_CC_EMAIL, @V_TO_EMAIL, @V_CD_AREA
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

DROP TABLE #CZ_SA_AUTO_MAIL_READY_INFO
DROP TABLE #EWS

GO