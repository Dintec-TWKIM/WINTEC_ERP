ALTER PROCEDURE [NEOE].[SP_CZ_QR_LABEL_GR_D]
(
	@P_CD_COMPANY		NVARCHAR(7), 
	@P_NO_PO			NVARCHAR(20), 
	@P_NO_LINE			INT, 
	@P_NO_SEQ			INT, 
	@P_YN_GI			NVARCHAR(1)
)
AS

DECLARE @V_ERRMSG		NVARCHAR(255)
DECLARE @V_YN_READ		NVARCHAR(1)

SELECT @V_YN_READ = YN_READ 
FROM CZ_QR_LABEL 
WHERE CD_COMPANY = @P_CD_COMPANY 
AND NO_PO = @P_NO_PO 
AND NO_LINE = @P_NO_LINE 
AND NO_SEQ = @P_NO_SEQ

IF @V_YN_READ = 'Y'
BEGIN
	SET @V_ERRMSG = '이미 처리된 아이템 입니다!'
	GOTO ERROR
END

BEGIN TRY
BEGIN TRAN SP_CZ_QR_LABEL_GR_D

DELETE 
FROM CZ_QR_LABEL 
WHERE CD_COMPANY = @P_CD_COMPANY 
AND NO_PO = @P_NO_PO 
AND NO_LINE = @P_NO_LINE 
AND NO_SEQ = @P_NO_SEQ

IF LEFT(@P_NO_PO, 2) != 'ST'
BEGIN
	DECLARE @V_NO_SO NVARCHAR(20)

	SET @V_NO_SO = (SELECT TOP 1 CD_PJT 
					FROM PU_POL 
					WHERE CD_COMPANY = @P_CD_COMPANY 
					AND NO_PO = @P_NO_PO)

	IF @P_YN_GI = 'Y'
		EXECUTE SP_CZ_QR_LABEL_GI_D @P_CD_COMPANY, @V_NO_SO, @P_NO_LINE, @P_NO_SEQ	
END

COMMIT TRAN SP_CZ_QR_LABEL_GR_D
END TRY
BEGIN CATCH
ROLLBACK TRAN SP_CZ_QR_LABEL_GR_D
SET @V_ERRMSG = ERROR_MESSAGE() 
GOTO ERROR
END CATCH

RETURN
ERROR:RAISERROR(@V_ERRMSG, 18, 1)


