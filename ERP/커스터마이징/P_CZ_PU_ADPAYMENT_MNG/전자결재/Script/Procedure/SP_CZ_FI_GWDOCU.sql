USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_GWDOCU]    Script Date: 2015-05-29 오전 10:38:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_FI_GWDOCU]  
(  
	@P_CD_COMPANY   NVARCHAR(7),   --0
	@P_CD_PC        NVARCHAR(7),   --1
	@P_NO_DOCU      NVARCHAR(20),  --2
	@P_ID_WRITE     NVARCHAR(10),  --3 
	@P_DT_ACCT      NVARCHAR(8),   --4
	@P_NM_NOTE      TEXT,          --5
	@P_NM_PUMM      NVARCHAR(100), --6
	@P_YN_REPORT    NVARCHAR(1),   --7
	@P_TP_REPORT    NUMERIC(4, 0), --8
	@P_NO_EMP_CONF  NVARCHAR(10) = NULL,
	@P_CD_SALEGRP   NVARCHAR(7)  = NULL,
	@P_TPSO         NVARCHAR(4)  = NULL
)
AS  

--** ST_STAT 값 : 2:미상신, 0:진행중, 1:종결, -1:반려, 3:취소(삭제)

DECLARE @P_ST_STAT   NVARCHAR(5)

SELECT  @P_ST_STAT = ST_STAT
FROM    FI_GWDOCU
WHERE   CD_COMPANY = @P_CD_COMPANY
AND     CD_PC      = @P_CD_PC  
AND     NO_DOCU    = @P_NO_DOCU

IF (@@ROWCOUNT <> 0)
BEGIN
	IF(@P_ST_STAT = '0')
	BEGIN
		RAISERROR ('▶ 이미 결재 진행중인 전표입니다 ◀', 16, 1)
		RETURN
	END
	ELSE IF(@P_ST_STAT = '1')
	BEGIN
		RAISERROR ('▶ 이미 결재 승인된 전표입니다 ◀', 16, 1)
		RETURN
	END
END  

DELETE 
FROM FI_GWDOCU 
WHERE CD_COMPANY = @P_CD_COMPANY 
AND CD_PC = @P_CD_PC 
AND	NO_DOCU = @P_NO_DOCU

SET @P_ST_STAT = '2' --미상신  
  
INSERT FI_GWDOCU
(
	CD_COMPANY,
	CD_PC,
	NO_DOCU,
	ID_WRITE,
	DT_ACCT,
	NM_PUMM,
	NM_NOTE,
	APP_FORM_KIND,
	ST_STAT
)  
VALUES
(
	@P_CD_COMPANY,
	@P_CD_PC,
	@P_NO_DOCU,
	@P_ID_WRITE,
	@P_DT_ACCT,
	@P_NM_PUMM,
	@P_NM_NOTE,
	@P_TP_REPORT,
	@P_ST_STAT
)  
  
IF (@@ERROR <> 0 ) BEGIN RAISERROR('[UP_SA_GWDOCU]FI_GWDOCU INSERT ERROR! 작업을 정상적으로 처리하지 못했습니다.', 16, 1) RETURN END  
GO

