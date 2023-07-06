USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CONTRACT_RPT_RE]    Script Date: 2016-10-04 오후 6:24:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



  
ALTER PROCEDURE [NEOE].[SP_CZ_SA_CONTRACT_RPT_RE]    
(    
    @P_CD_COMPANY       NVARCHAR(7),
    @P_NO_SO            NVARCHAR(20),
    @P_ID_UPDATE        NVARCHAR(15)
)    
AS    

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_ERRMSG   VARCHAR(255),   -- ERROR 메시지
	    @V_YN_DONE	NVARCHAR(1)

BEGIN TRAN SP_CZ_SA_CONTRACT_RPT_RE
BEGIN TRY

IF NOT EXISTS (SELECT 1 
			   FROM CZ_MA_WORKFLOWH 
			   WHERE CD_COMPANY = @P_CD_COMPANY 
			   AND NO_KEY = @P_NO_SO
			   AND TP_STEP = '12')
BEGIN
	SELECT @V_ERRMSG = '수발주통보서 결재 대상 건이 없습니다.'                      
	GOTO ERROR
END

IF NOT EXISTS (SELECT 1 
			   FROM CZ_MA_WORKFLOWH 
			   WHERE CD_COMPANY = @P_CD_COMPANY 
			   AND NO_KEY = @P_NO_SO
			   AND TP_STEP = '12'
			   AND (YN_DONE = 'F' OR YN_DONE = 'Y'))
BEGIN
	SELECT @V_ERRMSG = '수발주통보서 결재 진행중이거나 완료된 건이 없습니다.'                      
	GOTO ERROR
END

SELECT @V_YN_DONE = YN_DONE
FROM CZ_MA_WORKFLOWH
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_KEY = @P_NO_SO
AND TP_STEP = '12'

IF NOT EXISTS (SELECT 1 
			   FROM CZ_SA_CONTRACT_RPTH 
			   WHERE CD_COMPANY = @P_CD_COMPANY
			   AND NO_SO = @P_NO_SO)
BEGIN
	INSERT INTO CZ_SA_CONTRACT_RPTH
	(
		CD_COMPANY,
		NO_SO,
		YN_RE_APPROVAL,
		DTS_RE_APPROVAL,
		YN_BEFORE_STATUS,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES
	(
		@P_CD_COMPANY,
		@P_NO_SO,
		'Y',
		NEOE.SF_SYSDATE(GETDATE()),
		@V_YN_DONE,
		@P_ID_UPDATE,
		NEOE.SF_SYSDATE(GETDATE())
	)
END
ELSE
BEGIN
	UPDATE CZ_SA_CONTRACT_RPTH
	SET ID_FIRST_APPROVAL = NULL,
		DTS_FIRST_APPROVAL = NULL,
		ID_SECOND_APPROVAL = NULL,
		DTS_SECOND_APPROVAL =  NULL,
		YN_RE_APPROVAL = 'Y',
		DTS_RE_APPROVAL = NEOE.SF_SYSDATE(GETDATE()),
		YN_BEFORE_STATUS = @V_YN_DONE,
		ID_UPDATE = @P_ID_UPDATE,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND NO_SO = @P_NO_SO
END

UPDATE CZ_MA_WORKFLOWH
SET YN_DONE = 'N',
    DTS_DONE = NULL,
	UPDATE_HIST = 'SP_CZ_SA_CONTRACT_RPT_RE',
	ID_UPDATE = @P_ID_UPDATE,
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_KEY = @P_NO_SO
AND TP_STEP = '12'

COMMIT TRAN SP_CZ_SA_CONTRACT_RPT_RE

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_SA_SO_TP_SO_U
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN
ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK TRAN SP_CZ_SA_SO_TP_SO_U


GO

