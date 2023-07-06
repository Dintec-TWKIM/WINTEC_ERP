USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_AUTO_MAIL_SA_ORDER]    Script Date: 2020-12-15 오후 2:03:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_AUTO_MAIL_SA_ORDER]
(
	@P_CD_COMPANY	NVARCHAR(7)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_CD_PARTNER	    NVARCHAR(20)
DECLARE @V_LN_PARTNER	    NVARCHAR(50)
DECLARE @V_NO_IMO			NVARCHAR(10)
DECLARE @V_NM_VESSEL		NVARCHAR(50)		
DECLARE @V_FROM_EMAIL	    NVARCHAR(MAX)
DECLARE @V_TO_EMAIL	        NVARCHAR(MAX)
DECLARE @V_CC_EMAIL			NVARCHAR(MAX)

SELECT CD_FLAG1 AS CD_PARTNER,
       CD_FLAG2 AS NO_IMO,
       CD_FLAG3 AS NO_EMAIL INTO #VESSEL 
FROM CZ_MA_CODEDTL
WHERE CD_COMPANY = 'K100'
AND CD_FIELD = 'CZ_SA00057'

DECLARE SRC_CURSOR CURSOR FOR
SELECT AM.CD_PARTNER,
	   MP.LN_PARTNER,
	   SH.NO_IMO,
	   MH.NM_VESSEL,
	   'DINTEC<' +  ISNULL(ME1.NO_EMAIL, '') + '>' AS FROM_EMAIL,
	   (ISNULL(ME.NO_EMAIL, '') + ';' + ISNULL(ME1.NO_EMAIL, '')) AS CC_EMAIL,
	   (CASE WHEN AM.CD_PARTNER = '04262' THEN 'FSO@seapeak.com'
			 WHEN AM.CD_PARTNER = '01034' THEN (SELECT VS.NO_EMAIL 
											    FROM #VESSEL VS
												WHERE VS.CD_PARTNER = AM.CD_PARTNER
												AND VS.NO_IMO = SH.NO_IMO)
										  ELSE (SELECT STRING_AGG(CONVERT(NVARCHAR(MAX), A.NM_EMAIL), ';') 
												FROM (SELECT DISTINCT FP.NM_EMAIL 
													  FROM FI_PARTNERPTR FP
													  WHERE FP.CD_COMPANY = AM.CD_COMPANY
													  AND FP.CD_PARTNER = AM.CD_PARTNER
													  AND FP.YN_SO = 'Y') A) END) AS TO_EMAIL
FROM CZ_SA_AUTO_MAIL_PARTNER AM
JOIN (SELECT SH.CD_COMPANY, SH.CD_PARTNER, VE.CD_FLAG1, VE.CD_FLAG2,
			 (CASE WHEN SH.CD_PARTNER IN ('04262', '01034') THEN SH.NO_IMO ELSE '' END) AS NO_IMO,
			 ROW_NUMBER() OVER (PARTITION BY SH.CD_COMPANY, SH.CD_PARTNER, (CASE WHEN SH.CD_PARTNER IN ('04262', '01034') THEN SH.NO_IMO ELSE '' END) ORDER BY COUNT(1) DESC) AS IDX,
			 MAX(CASE WHEN SL.QT_SO > ISNULL(SL.QT_GIR, 0) THEN 'Y' ELSE 'N' END) AS YN_ORDER_STATUS
	  FROM SA_SOH SH
	  JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
				   MAX(CASE WHEN SL.QT_SO > ISNULL(SL.QT_GIR, 0) THEN SL.DT_DUEDATE 
																ELSE NULL END) AS DT_DUEDATE,
				   SUM(SL.QT_SO) AS QT_SO,
				   SUM(SL.QT_GIR) AS QT_GIR
			FROM SA_SOL SL
			WHERE SL.CD_ITEM NOT LIKE 'SD%'
			GROUP BY SL.CD_COMPANY, SL.NO_SO) SL 
	  ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	  LEFT JOIN V_CZ_SA_QTN_LOG_EMP VE ON VE.CD_COMPANY = SH.CD_COMPANY AND VE.NO_FILE = SH.NO_SO
	  LEFT JOIN CZ_SA_DEFERRED_DELIVERY DD ON DD.CD_COMPANY = SH.CD_COMPANY AND DD.TP_TYPE = '0' AND DD.NO_SO = SH.NO_SO AND DD.NO_KEY = SH.NO_SO AND DD.DT_LIMIT = SL.DT_DUEDATE
	  WHERE ISNULL(SH.YN_CLOSE, 'N') = 'N'
	  AND ISNULL(DD.YN_EXCLUDE, 'N') <> 'Y'
	  AND LEFT(SH.NO_SO, 2) NOT IN ('SB', 'HB')
	  GROUP BY SH.CD_COMPANY, SH.CD_PARTNER, VE.CD_FLAG1, VE.CD_FLAG2, (CASE WHEN SH.CD_PARTNER IN ('04262', '01034') THEN SH.NO_IMO ELSE '' END)) SH
ON SH.CD_COMPANY = AM.CD_COMPANY AND SH.CD_PARTNER = AM.CD_PARTNER AND SH.IDX = 1
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = AM.CD_COMPANY AND MP.CD_PARTNER = AM.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.CD_FLAG1
LEFT JOIN MA_EMP ME1 ON ME1.CD_COMPANY = SH.CD_COMPANY AND ME1.NO_EMP = SH.CD_FLAG2
WHERE AM.CD_COMPANY = @P_CD_COMPANY
AND AM.TP_PARTNER = '002'
AND ISNULL(AM.YN_ORDER_STAT, 'N') = 'Y'
AND ISNULL(SH.YN_ORDER_STATUS, 'N') = 'Y'
AND ((ISNULL(AM.YN_MON, 'N') = 'Y' AND DATEPART(DW, GETDATE()) = 2) OR 
	 (ISNULL(AM.YN_TUE, 'N') = 'Y' AND DATEPART(DW, GETDATE()) = 3) OR 
	 (ISNULL(AM.YN_WED, 'N') = 'Y' AND DATEPART(DW, GETDATE()) = 4) OR 
	 (ISNULL(AM.YN_THU, 'N') = 'Y' AND DATEPART(DW, GETDATE()) = 5) OR 
	 (ISNULL(AM.YN_FRI, 'N') = 'Y' AND DATEPART(DW, GETDATE()) = 6))

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_CD_PARTNER, @V_LN_PARTNER, @V_NO_IMO, @V_NM_VESSEL, @V_FROM_EMAIL, @V_CC_EMAIL, @V_TO_EMAIL

WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @V_SUBJECT NVARCHAR(100)
	DECLARE @V_HTML NVARCHAR(MAX)
	DECLARE @V_BODY	NVARCHAR(MAX) = ''
	DECLARE @V_HTML_RED NVARCHAR(MAX)

	IF @V_CD_PARTNER IN ('04262', '01034')
	BEGIN
		SET @V_SUBJECT = '[DINTEC] ' + @V_NM_VESSEL + ' - ORDER STATUS'
	END
	ELSE
	BEGIN
		SET @V_SUBJECT = '[DINTEC] ' + @V_LN_PARTNER + ' - ORDER STATUS'	
	END

	IF EXISTS (SELECT 1 
			   FROM CZ_SA_AUTO_MAIL_SA_ORDER_LOG SL
			   WHERE SL.CD_COMPANY =  @P_CD_COMPANY
			   AND SL.CD_PARTNER = @V_CD_PARTNER
			   AND SL.NO_IMO = @V_NO_IMO
			   AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112))
	BEGIN
		DELETE SL
		FROM CZ_SA_AUTO_MAIL_SA_ORDER_LOG SL
		WHERE SL.CD_COMPANY =  @P_CD_COMPANY
		AND SL.CD_PARTNER = @V_CD_PARTNER
		AND SL.NO_IMO = @V_NO_IMO
		AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
	END

	;WITH A AS
	(
	    SELECT SL.CD_COMPANY, SL.NO_SO,
	           SL.QT_SO,
	           SL.QT_GIR,
	           ISNULL(PL.DT_EXPECT, '') AS DT_EXPECT, 
	           (CASE WHEN ISNULL(SB.QT_STOCK, 0) = ISNULL(SB.QT_BOOK, 0) THEN LEFT(SB.DTS_UPDATE, 8) ELSE NULL END) AS DT_BOOK,
	           (CASE WHEN SL.QT_SO > ISNULL(SL.QT_GI, 0) THEN SL.DT_DUEDATE ELSE NULL END) AS DT_DUEDATE,
	           (CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(BM.QT_BOM, 0) 
		 		                          ELSE (ISNULL(PL.QT_IO, 0) + ISNULL(SB.QT_BOOK, 0)) END) AS QT_IN 
	    FROM SA_SOL SL
	    JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
		LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
		LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
		                  SUM(IL.QT_IO) AS QT_IO,
		                  MAX(DD.DT_EXPECT) AS DT_EXPECT
		           FROM PU_POL PL
		           LEFT JOIN PU_RCVL RL ON RL.CD_COMPANY = PL.CD_COMPANY AND RL.NO_PO = PL.NO_PO AND RL.NO_POLINE = PL.NO_LINE
		           LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = RL.CD_COMPANY AND IL.NO_ISURCV = RL.NO_RCV AND IL.NO_ISURCVLINE = RL.NO_LINE
		           LEFT JOIN CZ_SA_DEFERRED_DELIVERY DD ON DD.CD_COMPANY = PL.CD_COMPANY AND DD.TP_TYPE = '2' AND DD.NO_SO = PL.NO_SO AND DD.NO_KEY = PL.NO_PO AND DD.DT_LIMIT = PL.DT_LIMIT
		           GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
		ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
		LEFT JOIN (SELECT OH.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
		                  SUM(OL.QT_IO) AS QT_BOM
		           FROM MM_QTIOH OH
		           LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
		           WHERE OH.YN_RETURN <> 'Y' 
		           AND OL.FG_PS = '1' 
		           AND OL.FG_IO = '002'
		           GROUP BY OH.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) BM
		ON BM.CD_COMPANY = SL.CD_COMPANY AND BM.NO_PSO_MGMT = SL.NO_SO AND BM.NO_PSOLINE_MGMT = SL.SEQ_SO
	    WHERE SL.CD_ITEM NOT LIKE 'SD%'
	)
	INSERT INTO CZ_SA_AUTO_MAIL_SA_ORDER_LOG
	(
	    CD_COMPANY,
		CD_PARTNER,
		NO_IMO,
		DT_SEND,
		IDX,
		NO_SO,
	    NM_VESSEL,
	    NO_PO_PARTNER,
	    DT_SO,
	    DT_DUEDATE,
	    ST_SO,
	    DC_ITEM,
	    DT_EXPECT,
	    CD_RMK,
	    DC_RMK1,
		ID_INSERT,
		DTS_INSERT
	)
	SELECT SH.CD_COMPANY,
		   SH.CD_PARTNER,
		   (CASE WHEN SH.CD_PARTNER IN ('04262', '01034') THEN SH.NO_IMO ELSE '' END) AS NO_IMO,
		   CONVERT(CHAR(8), GETDATE(), 112) AS DT_SEND,
		   ROW_NUMBER() OVER (PARTITION BY SH.CD_PARTNER ORDER BY SL.DT_DUEDATE ASC) AS IDX,
		   SH.NO_SO,
		   MH.NM_VESSEL,
		   SH.NO_PO_PARTNER,
		   SH.DT_SO,
		   SL.DT_DUEDATE,
		   (CASE WHEN SL.QT_SO = SL.QT_IN THEN 'READY'
		         WHEN SL.QT_IN > 0 THEN 'PARTIAL'
		         WHEN DATEDIFF(DAY, SL.DT_DUEDATE, GETDATE()) > 0 THEN 'DELAYED'
		         ELSE 'PENDING' END) AS ST_SO,
		   (CASE WHEN SL.QT_SO = SL.QT_IN THEN 'ALL'
		         WHEN SL.QT_IN > 0 THEN '<a href="http://tracking.dintec.co.kr/order/status.aspx?noPo=' + SH.NO_PO_PARTNER + '&ref=' + SH.NO_SO + '" style="color:#0000ff; text-decoration:underline;" target="_blank">Detail</a>'
		         ELSE '' END) AS DC_ITEM,
		   (CASE WHEN SL.QT_SO = SL.QT_IN THEN ''
				 WHEN DATEDIFF(DAY, SL.DT_DUEDATE, GETDATE()) > 0 THEN SL.DT_EXPECT 
		                                                          ELSE '' END) AS DT_EXPECT,
		   DD.CD_RMK,
		   DD.DC_RMK1,
		   'SYSTEM' AS ID_INSERT,
		   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
	FROM SA_SOH SH
	JOIN (SELECT A.CD_COMPANY, A.NO_SO,
	             SUM(A.QT_SO) AS QT_SO,
	             SUM(A.QT_GIR) AS QT_GIR,
	             SUM(A.QT_IN) AS QT_IN,
	             MAX(CASE WHEN A.QT_SO > ISNULL(A.QT_GIR, 0) THEN A.DT_DUEDATE ELSE NULL END) AS DT_DUEDATE,
		         MAX(CASE WHEN A.QT_SO <= A.QT_IN THEN NULL
					      WHEN ISNULL(A.DT_BOOK, '') >= CONVERT(CHAR(8), DATEADD(DAY, 3, A.DT_EXPECT), 112) THEN A.DT_BOOK
					      WHEN ISNULL(A.DT_EXPECT, '') <> '' THEN CONVERT(CHAR(8), DATEADD(DAY, 3, A.DT_EXPECT), 112)
	                      ELSE ISNULL(A.DT_DUEDATE, '') END) AS DT_EXPECT
	      FROM A
	      GROUP BY A.CD_COMPANY, A.NO_SO) SL 
	ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
	LEFT JOIN CZ_SA_DEFERRED_DELIVERY DD ON DD.CD_COMPANY = SH.CD_COMPANY AND DD.TP_TYPE = '0' AND DD.NO_SO = SH.NO_SO AND DD.NO_KEY = SH.NO_SO AND DD.DT_LIMIT = SL.DT_DUEDATE
	WHERE SH.CD_COMPANY = @P_CD_COMPANY
	AND SH.CD_PARTNER = @V_CD_PARTNER
	AND (@V_CD_PARTNER NOT IN ('04262', '01034') OR SH.NO_IMO = @V_NO_IMO)
	AND ISNULL(SH.YN_CLOSE, 'N') = 'N'
	AND LEFT(SH.NO_SO, 2) NOT IN ('SB', 'HB')
	AND ISNULL(DD.YN_EXCLUDE, 'N') <> 'Y'
	AND SL.QT_SO > ISNULL(SL.QT_GIR, 0)

	;WITH A AS
	(
		SELECT SL.IDX,
		       '<tr style="height:30px">' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.ST_SO, '') = 'READY' THEN 'background-color:Yellow;' ELSE '' END) + ' font-weight:normal; width:300px">' + ISNULL(SL.NM_VESSEL, '') + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.ST_SO, '') = 'READY' THEN 'background-color:Yellow;' ELSE '' END) + ' font-weight:normal; width:300px">' + ISNULL(SL.NO_PO_PARTNER, '') + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.ST_SO, '') = 'READY' THEN 'background-color:Yellow;' ELSE '' END) + ' font-weight:normal; width:100px">' + ISNULL(SL.NO_SO, '') + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.ST_SO, '') = 'READY' THEN 'background-color:Yellow;' ELSE '' END) + ' font-weight:normal; width:100px">' + (CASE WHEN ISNULL(SL.DT_SO, '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, ISNULL(SL.DT_SO, '')), 23) END) + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.ST_SO, '') = 'READY' THEN 'background-color:Yellow;' ELSE '' END) + ' font-weight:normal; width:100px">' + (CASE WHEN ISNULL(SL.DT_DUEDATE, '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, ISNULL(SL.DT_DUEDATE, '')), 23) END) + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE ISNULL(SL.ST_SO, '') WHEN 'READY' THEN 'background-color:Yellow; font-weight:bold;'
																									  WHEN 'PARTIAL' THEN 'color:#0070c0; font-weight:bold;'
																									  WHEN 'DELAYED' THEN 'color:red; font-weight:bold;'
																									  ELSE 'font-weight:normal;' END) + ' width:100px">' + ISNULL(SL.ST_SO, '') + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.ST_SO, '') = 'READY' THEN 'background-color:Yellow;' ELSE '' END) + ' font-weight:normal; width:100px">' + ISNULL(SL.DC_ITEM, '') + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.ST_SO, '') = 'READY' THEN 'background-color:Yellow;' ELSE '' END) + ' font-weight:normal; width:100px">' + (CASE WHEN ISNULL(SL.DT_EXPECT, '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, ISNULL(SL.DT_EXPECT, '')), 23) END) + '</th>' +
				'<th style="border:solid 1px black; text-align:center; ' + (CASE WHEN ISNULL(SL.ST_SO, '') = 'READY' THEN 'background-color:Yellow;' ELSE '' END) + ' font-weight:normal; width:300px">' + (CASE WHEN ISNULL(SL.DC_RMK1, '') <> '' THEN ISNULL(SL.DC_RMK1, '') ELSE ISNULL(CD.NM_SYSDEF, '') END) + '</th>' +
			   '</tr>' AS DC_BODY
		FROM CZ_SA_AUTO_MAIL_SA_ORDER_LOG SL
		LEFT JOIN CZ_MA_CODEDTL CD ON CD.CD_COMPANY = SL.CD_COMPANY AND CD.CD_FIELD = 'CZ_SA00052' AND CD.CD_SYSDEF = SL.CD_RMK
		WHERE SL.CD_COMPANY = @P_CD_COMPANY
		AND SL.CD_PARTNER = @V_CD_PARTNER
		AND SL.NO_IMO = @V_NO_IMO
		AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)	
	)
	SELECT @V_BODY = STRING_AGG(A.DC_BODY, '') WITHIN GROUP (ORDER BY A.IDX ASC)
	FROM A
	
	SET @V_HTML = '<div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕;">
				   To: ' + @V_LN_PARTNER +'<br><br>
				   This message is a regular weekly order status update for your reference,<br><br> 
				   </div>
					  
				   <table style="width:1500px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;">
					<colgroup width="300px" align="center"></colgroup>
					<colgroup width="300px" align="center"></colgroup>
					<colgroup width="100px" align="center"></colgroup>
					<colgroup width="100px" align="center"></colgroup>
					<colgroup width="100px" align="center"></colgroup>
					<colgroup width="100px" align="center"></colgroup>
					<colgroup width="100px" align="center"></colgroup>
					<colgroup width="100px" align="center"></colgroup>
					<colgroup width="300px" align="center"></colgroup>
					<tbody>
					<tr style="height:30px">
					 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:300px">Vessel</th>
					 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:300px">Order No.</th>
					 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Dintec No.</th>                                    
					 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Ordered date</th>
					 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Due date</th>                                    
					 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Status</th>
					 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Ready item</th>
					 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:100px">Expected Readiness</th>
					 <th style="border:solid 1px black; text-align:center; background-color:#92D050; width:300px">Remark</th>
					</tr>'
					+ @V_BODY +
				   '</tbody>
				   </table>  
				   <div style="text-align:left; font-size: 10pt; font-family: 맑은 고딕; color: #0070c0; font-weight: bold;">
					<br>※ Please let us have your delivery instruction for ready / partially ready orders
				   </div>'

	BEGIN TRY
		EXEC msdb.dbo.sp_send_dbmail @PROFILE_NAME = 'AUTO_MAIL',	
								     @RECIPIENTS = @V_TO_EMAIL,
									 @COPY_RECIPIENTS = @V_CC_EMAIL,
								     @FROM_ADDRESS = @V_FROM_EMAIL,
								     @REPLY_TO = @V_CC_EMAIL,
								     @SUBJECT = @V_SUBJECT,	
								     @BODY = @V_HTML,	
								     @BODY_FORMAT = 'HTML'

		UPDATE DD
		SET DD.TP_SEND = 'OS자동발송',
		    DD.DTS_SEND = NEOE.SF_SYSDATE(GETDATE()),
		    DD.ID_UPDATE = 'SYSTEM',
		    DD.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
		FROM CZ_SA_DEFERRED_DELIVERY DD
		WHERE DD.CD_COMPANY = @P_CD_COMPANY 
		AND DD.TP_TYPE = '0'
		AND EXISTS (SELECT 1 
					FROM CZ_SA_AUTO_MAIL_SA_ORDER_LOG SL 
					WHERE SL.CD_COMPANY = DD.CD_COMPANY 
					AND SL.NO_SO = DD.NO_SO 
					AND SL.NO_SO = DD.NO_KEY 
					AND SL.DT_DUEDATE = DD.DT_LIMIT
					AND SL.CD_PARTNER = @V_CD_PARTNER
					AND SL.NO_IMO = @V_NO_IMO
		            AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112))
		

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
		SELECT SL.CD_COMPANY,
		       '0' AS TP_TYPE,
		       SL.NO_SO,
		       SL.NO_SO AS NO_KEY,
		       SL.DT_DUEDATE,
		       'OS자동발송' AS TP_SEND,
		       NEOE.SF_SYSDATE(GETDATE()) AS DTS_SEND,
		       'SYSTEM' AS ID_INSERT,
		       NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
		FROM CZ_SA_AUTO_MAIL_SA_ORDER_LOG SL
		WHERE SL.CD_COMPANY =  @P_CD_COMPANY
		AND SL.CD_PARTNER = @V_CD_PARTNER
		AND SL.NO_IMO = @V_NO_IMO
		AND SL.DT_SEND = CONVERT(CHAR(8), GETDATE(), 112)
		AND NOT EXISTS (SELECT 1 
		                FROM CZ_SA_DEFERRED_DELIVERY DD
		                WHERE DD.CD_COMPANY = SL.CD_COMPANY
		                AND DD.NO_SO = SL.NO_SO
		                AND DD.NO_KEY = SL.NO_SO
		                AND DD.TP_TYPE = '0'
		                AND DD.DT_LIMIT = SL.DT_DUEDATE)
	END TRY
	BEGIN CATCH
		DECLARE @V_SUBJECT2 NVARCHAR(500) = 'Order Status 자동 메일 발송 오류 발생 (' + @V_CD_PARTNER + ')'
		DECLARE @V_ERROR NVARCHAR(1000) = ERROR_MESSAGE()

		EXEC msdb.dbo.sp_send_dbmail @PROFILE_NAME = 'AUTO_MAIL',	
							         @RECIPIENTS = @V_FROM_EMAIL,
							         @SUBJECT = @V_SUBJECT2,	
							         @BODY = @V_ERROR
	END CATCH

	FETCH NEXT FROM SRC_CURSOR INTO @V_CD_PARTNER, @V_LN_PARTNER, @V_NO_IMO, @V_NM_VESSEL, @V_FROM_EMAIL, @V_CC_EMAIL, @V_TO_EMAIL
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

DROP TABLE #VESSEL

GO