USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_NEXT_STEP]    Script Date: 2015-11-25 오전 8:59:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_NEXT_STEP]  
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_STEP					NVARCHAR(2),
	@P_NO_KEY				NVARCHAR(20),
	@P_ID_USER				NVARCHAR(15),
	@P_DC_RMK				NVARCHAR(MAX)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_ID_SALES NVARCHAR(15)
DECLARE @V_ID_PUR NVARCHAR(15)
DECLARE @V_CD_SUPPLIER NVARCHAR(20)
DECLARE @V_NM_SUPPLIER NVARCHAR(50)
DECLARE @V_DC_MSG NVARCHAR(500)
DECLARE @V_DC_RMK NVARCHAR(MAX)

IF ISNULL(@P_NO_KEY, '') = ''
BEGIN
	SET @V_DC_MSG = '[SP_CZ_MA_WORKFLOW_NEXT_STEP] 파일번호가 없습니다.' + ' [단계 : ' + @P_STEP + ', 비고 : ' + @P_DC_RMK + ']'

	RAISERROR(@V_DC_MSG, 16, 1)
	RETURN
END

IF @P_STEP <> '01' AND 
   @P_STEP <> '02' AND 
   NOT EXISTS (SELECT 1 
	           FROM CZ_SA_QTNH QH
	           WHERE QH.CD_COMPANY = @P_CD_COMPANY 
	           AND QH.NO_FILE = @P_NO_KEY)
BEGIN
	SET @V_DC_MSG = '등록 된 견적 데이터가 없습니다.'

	RAISERROR(@V_DC_MSG, 16, 1)
	RETURN
END

DECLARE @V_CD_PARTNER NVARCHAR(20)

SELECT @V_CD_PARTNER = QH.CD_PARTNER 
FROM CZ_SA_QTNH QH 
WHERE  QH.CD_COMPANY = @P_CD_COMPANY
AND QH.NO_FILE = @P_NO_KEY

IF @P_STEP = '21'
BEGIN
	SELECT TOP 1 @V_CD_SUPPLIER = QH.CD_PARTNER,
				 @V_NM_SUPPLIER = MP.LN_PARTNER
    FROM CZ_PU_QTNH QH
	JOIN MA_CODEDTL MC ON MC.CD_COMPANY = QH.CD_COMPANY AND MC.CD_FLAG1 = QH.CD_PARTNER
	JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
	WHERE QH.CD_COMPANY = @P_CD_COMPANY
	AND QH.NO_FILE = @P_NO_KEY
	AND MC.CD_FIELD = 'CZ_MA00021'

	IF ISNULL(@V_CD_SUPPLIER, '') = ''
	BEGIN
		SET @V_DC_MSG = '[' + @P_NO_KEY + '] ' + '요청가능한 매입처가 등록되어 있지 않습니다.'

	    RAISERROR(@V_DC_MSG, 16, 1)
	    RETURN
	END

	SET @V_DC_RMK = @V_NM_SUPPLIER + ' 견적요청'
END
ELSE IF @P_STEP = '06'
BEGIN
	SELECT @V_DC_RMK = WH.DC_RMK 
	FROM CZ_MA_WORKFLOWH WH
	WHERE WH.CD_COMPANY = @P_CD_COMPANY
	AND WH.NO_KEY = @P_NO_KEY
	AND WH.TP_STEP = '05'
END
ELSE
BEGIN
	SET @V_DC_RMK = @P_DC_RMK
END

IF (SELECT COUNT(1) FROM CZ_MA_WORKFLOWH WHERE CD_COMPANY = @P_CD_COMPANY AND TP_STEP = @P_STEP AND NO_KEY = @P_NO_KEY) > 0
BEGIN
	UPDATE CZ_MA_WORKFLOWH
	SET YN_DONE = 'N',
		DTS_DONE = NULL,
		UPDATE_HIST = 'SP_CZ_MA_WORKFLOW_NEXT_STEP',
		ID_UPDATE = @P_ID_USER,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND TP_STEP = @P_STEP
	AND NO_KEY = @P_NO_KEY
END
ELSE
BEGIN
	SELECT @V_ID_SALES = ID_SALES 
	FROM CZ_MA_WORKFLOWH 
	WHERE CD_COMPANY = @P_CD_COMPANY 
	AND TP_STEP = '01' 
	AND NO_KEY = @P_NO_KEY
	
	IF ISNULL(@V_ID_SALES, '') = ''
	BEGIN
		SELECT @V_ID_SALES = NO_EMP 
		FROM CZ_SA_QTNH 
		WHERE CD_COMPANY = @P_CD_COMPANY  
		AND NO_FILE = @P_NO_KEY
	END

	SELECT @V_ID_PUR = ID_PUR 
	FROM CZ_MA_WORKFLOWH 
	WHERE CD_COMPANY = @P_CD_COMPANY 
	AND TP_STEP = '08' 
	AND NO_KEY = @P_NO_KEY

	IF @P_STEP = '21'
	BEGIN
		 WITH A AS
		 (
			SELECT MU.CD_COMPANY,
				   MU.ID_USER 
			FROM MA_USER MU
			WHERE MU.CD_COMPANY = @P_CD_COMPANY
			AND EXISTS (SELECT 1 
						FROM MA_CODEDTL MC
						WHERE MC.CD_COMPANY = MU.CD_COMPANY
					    AND MC.CD_FIELD = 'CZ_MA00021'
					    AND MC.CD_FLAG1 = @V_CD_SUPPLIER
						AND MC.CD_FLAG2 LIKE '%' + MU.ID_USER + '%')
		 )
		 SELECT TOP 1 @V_ID_PUR = MU.ID_USER
		 FROM A MU
		 LEFT JOIN (SELECT CD_COMPANY, ID_PUR,
		 				  COUNT(1) AS QT_WF 
		 		   FROM CZ_MA_WORKFLOWH
		 		   WHERE TP_STEP = '21'
		 		   AND DTS_INSERT >= CONVERT(NVARCHAR(8), GETDATE(), 112) + '000000'
		 		   GROUP BY CD_COMPANY, ID_PUR) WH 
		 ON WH.CD_COMPANY = MU.CD_COMPANY AND WH.ID_PUR = MU.ID_USER
		 ORDER BY WH.QT_WF ASC

		 UPDATE QH
		 SET QH.NO_EMP_QTN = @V_ID_PUR,
			 QH.NO_EMP_STK = @V_ID_PUR
		 FROM CZ_SA_QTNH QH
		 WHERE QH.CD_COMPANY = @P_CD_COMPANY
		 AND QH.NO_FILE = @P_NO_KEY
	END

	INSERT INTO CZ_MA_WORKFLOWH
	(
		CD_COMPANY,
		TP_STEP,
		NO_KEY,
		ID_SALES,
		ID_TYPIST,
		ID_LOG,
		ID_PUR,
		YN_DONE,
		DTS_DONE,
		DC_RMK,
		INSERT_HIST,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES
	(
		@P_CD_COMPANY,
		@P_STEP,
		@P_NO_KEY,
		@V_ID_SALES,
		(SELECT ID_TYPIST FROM CZ_MA_WORKFLOWH WHERE CD_COMPANY = @P_CD_COMPANY AND TP_STEP = '01' AND NO_KEY = @P_NO_KEY),
		(SELECT ID_LOG FROM CZ_MA_WORKFLOWH WHERE CD_COMPANY = @P_CD_COMPANY AND TP_STEP = '08' AND NO_KEY = @P_NO_KEY),
		@V_ID_PUR,
		'N',
		NULL,
		@V_DC_RMK,
		'SP_CZ_MA_WORKFLOW_NEXT_STEP',
		@P_ID_USER,
		NEOE.SF_SYSDATE(GETDATE())
	)
END

IF @P_STEP = '06' AND EXISTS (SELECT 1 
						      FROM CZ_RPA_WORK_QUEUE WQ
							  WHERE WQ.CD_COMPANY = @P_CD_COMPANY
							  AND WQ.CD_RPA LIKE 'SHIPSERV_QTN%'
							  AND WQ.NO_FILE = @P_NO_KEY)
BEGIN
	DELETE WQ 
	FROM CZ_RPA_WORK_QUEUE WQ
	WHERE WQ.CD_COMPANY = @P_CD_COMPANY
	AND WQ.CD_RPA LIKE 'SHIPSERV_QTN%'
	AND WQ.NO_FILE = @P_NO_KEY
END

EXEC PX_CZ_RPA_WORK_QUEUE_4 @P_CD_COMPANY, @NO_FILE = @P_NO_KEY, @CD_PARTNER = @V_CD_PARTNER, @CD_WORKSTEP = @P_STEP, @ID_INSERT = @P_ID_USER

GO