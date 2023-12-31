ALTER PROCEDURE [NEOE].[SP_CZ_QR_LABEL_GI_D]
(
	  @P_CD_COMPANY		NVARCHAR(7), 
	  @P_NO_KEY			NVARCHAR(20), 
	  @P_NO_LINE		INT, 
	  @P_NO_SEQ			INT
)
AS

DECLARE @V_ERRMSG		NVARCHAR(255)
DECLARE @V_YN_READ		NVARCHAR(1)

SELECT @V_YN_READ = YN_READ 
FROM CZ_QR_LABELL 
WHERE CD_COMPANY = @P_CD_COMPANY 
AND NO_KEY = @P_NO_KEY 
AND NO_LINE = @P_NO_LINE 
AND NO_SEQ = @P_NO_SEQ

IF @V_YN_READ = 'Y'
BEGIN
	SET @V_ERRMSG = '이미 처리된 아이템 입니다!'
	GOTO ERROR
END

DELETE FROM CZ_QR_LABELL 
WHERE CD_COMPANY = @P_CD_COMPANY 
AND NO_KEY = @P_NO_KEY 
AND NO_LINE = @P_NO_LINE 
AND NO_SEQ = @P_NO_SEQ

RETURN
ERROR:RAISERROR(@V_ERRMSG, 18, 1)