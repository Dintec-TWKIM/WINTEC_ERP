USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_SUPPLIER_AUTO]    Script Date: 2019-02-26 오후 5:08:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_SUPPLIER_AUTO]
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_CD_PARTNER			NVARCHAR(20),
	@P_NO_FILE				NVARCHAR(20)
)  
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_ID_PUR		NVARCHAR(50)
DECLARE @V_CD_PARTNER	NVARCHAR(20)
DECLARE @V_ERRMSG		VARCHAR(255)   -- ERROR 메시지

BEGIN TRAN SP_CZ_MA_WORKFLOW_SUPPLIER_AUTO
BEGIN TRY

DECLARE @WORK TABLE
(
	TEAM	NVARCHAR(100),	
	EMP		NVARCHAR(100)
)
INSERT INTO @WORK
SELECT TEAM = NEOE.SPLIT(WORK, ':', 1),	
	   EMP  = NEOE.SPLIT(WORK, ':', 2)	
FROM (SELECT WORK = NEOE.TRIM(NEOE.DECODE_ASCII(SPLIT.A.value('.', 'NVARCHAR(100)')))
	  FROM (SELECT X = CONVERT(XML, '<N>' + REPLACE(NEOE.ENCODE_ASCII(ISNULL(NULLIF(':' + CD_FLAG5, ':'), CD_FLAG1)), '/', '</N><N>') + '</N>')	-- 백업부터 먼저 검색 후 담당자 검색
		    FROM CZ_MA_CODEDTL
		    WHERE 1 = 1
	        AND CD_COMPANY = @P_CD_COMPANY
	        AND CD_FIELD = 'CZ_DX00009'
		    AND CD_SYSDEF = @P_CD_PARTNER
		    AND YN_USE = 'Y') AS A CROSS APPLY X.nodes ('/N[.!='''']') AS SPLIT(A)) AS A

SELECT @V_CD_PARTNER = QH.CD_PARTNER,
	   @V_ID_PUR = W.EMP
FROM (SELECT QH.CD_PARTNER,
			 SUBSTRING(SG.NM_SALEGRP, CHARINDEX('팀', SG.NM_SALEGRP) -1, 2) AS NM_TEAM
	  FROM CZ_SA_QTNH QH
	  JOIN MA_SALEGRP	SG ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
	  WHERE QH.CD_COMPANY = @P_CD_COMPANY
	  AND QH.NO_FILE = @P_NO_FILE) QH
JOIN @WORK W ON W.TEAM = '' OR W.TEAM LIKE '%' + QH.NM_TEAM + '%'

IF @V_ID_PUR IS NULL
BEGIN
	SET @V_ERRMSG = '해당 매입처 담당자가 지정되어 있지 않습니다. [' + @P_CD_PARTNER + ']' 
	GOTO ERROR
END

IF NOT EXISTS (SELECT 1 
			   FROM CZ_MA_WORKFLOWH WH
			   WHERE WH.CD_COMPANY = @P_CD_COMPANY
			   AND WH.NO_KEY = @P_NO_FILE
			   AND WH.TP_STEP = '21')
BEGIN
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
	SELECT QH.CD_COMPANY AS CD_COMPANY,
		   '21' AS TP_STEP,
		   QH.NO_FILE AS NO_KEY,
		   ISNULL(WH.ID_SALES, QH.NO_EMP) AS ID_SALES,
		   WH.ID_TYPIST,
		   @P_CD_PARTNER AS ID_LOG,
		   @V_ID_PUR AS ID_PUR,
		   'N' AS YN_DONE,
		   NULL AS DTS_DONE,
		   (CASE WHEN LEFT(@P_NO_FILE, 2) IN ('NB', 'NS', 'SB') THEN (SELECT CD_FLAG1 
																	  FROM CZ_MA_CODEDTL 
																	  WHERE CD_COMPANY = @P_CD_COMPANY
																	  AND CD_FIELD = 'CZ_DX00014'
																	  AND CD_SYSDEF = LEFT(@P_NO_FILE, 2))
																ELSE (SELECT CD_FLAG1 
																	  FROM CZ_MA_CODEDTL 
																	  WHERE CD_COMPANY = @P_CD_COMPANY
																	  AND CD_FIELD = 'CZ_DX00014'
																	  AND CD_SYSDEF = @V_CD_PARTNER) END) AS DC_RMK,
	       'SP_CZ_MA_WORKFLOW_SUPPLIER_AUTO' AS INSERT_HIST,
	       'SYSTEM' AS ID_INSERT,
	       NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
	FROM CZ_SA_QTNH QH
	LEFT JOIN CZ_MA_WORKFLOWH WH ON WH.CD_COMPANY = QH.CD_COMPANY AND WH.NO_KEY = QH.NO_FILE AND WH.TP_STEP = '01'
	LEFT JOIN CZ_MA_WORKFLOWH WH1 ON WH.CD_COMPANY = QH.CD_COMPANY AND WH.NO_KEY = QH.NO_FILE AND WH.TP_STEP = '08'
	WHERE QH.CD_COMPANY = @P_CD_COMPANY
	AND QH.NO_FILE = @P_NO_FILE

	UPDATE QH
	SET QH.NO_EMP_QTN = @V_ID_PUR,
		QH.NO_EMP_STK = @V_ID_PUR
	FROM CZ_SA_QTNH QH
	WHERE QH.CD_COMPANY = @P_CD_COMPANY
	AND QH.NO_FILE = @P_NO_FILE
END

COMMIT

END TRY
BEGIN CATCH
	ROLLBACK
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN
ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK

GO