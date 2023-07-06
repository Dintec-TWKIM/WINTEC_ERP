USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_RCPD_U]    Script Date: 2016-05-17 오후 3:48:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- 설    명 : 영업>채권관리>수금등록 반제 수정
-- 수 정 자 : 김명수
-- 수 정 일 : 2010.06.11
-- 유    형 : 수정
-- 내    역 : 에러발생시 ROLLBACK 처리
-- ================================================
ALTER PROCEDURE [NEOE].[SP_CZ_SA_RCPD_U]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_RCP			NVARCHAR(20),
	@P_NO_TX			NVARCHAR(20),
	@P_DT_IV			NVARCHAR(8),
	@P_TP_SO			NVARCHAR(3),
	@P_AM_IV_EX			NUMERIC(17,4),
	@P_AM_IV			NUMERIC(17,4),
	@P_RT_EXCH_IV		NUMERIC(11,4),
	@P_AM_RCP_TX_EX		NUMERIC(17,4),
	@P_AM_RCP_TX		NUMERIC(17,4),
	@P_AM_PL			NUMERIC(17,4),
	@P_NO_DOCU			NVARCHAR(20),
	@P_NO_DOLINE		NUMERIC(5, 0),
	@P_ID_UPDATE		NVARCHAR(15),
	@P_CD_USERDEF1		NVARCHAR(3)		= NULL
)
AS
SET NOCOUNT ON

DECLARE @V_ERRNO           INT,                -- ERROR 번호  
        @V_ERRMSG          NVARCHAR(255)       -- ERROR 메시지 

DECLARE @P_DTS_UPDATE VARCHAR(14)
SET @P_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')

UPDATE SA_RCPD
SET    DT_IV        = @P_DT_IV,
       TP_SO        = @P_TP_SO,
	   AM_IV_EX     = @P_AM_IV_EX,        
	   AM_IV        = @P_AM_IV,        
	   RT_EXCH_IV   = @P_RT_EXCH_IV,
	   AM_RCP_TX_EX = @P_AM_RCP_TX_EX,
	   AM_RCP_TX    = @P_AM_RCP_TX,
	   AM_PL        = @P_AM_PL,
	   NO_DOCU		= @P_NO_DOCU,
	   NO_DOLINE	= @P_NO_DOLINE, 
	   ID_UPDATE    = @P_ID_UPDATE,
	   DTS_UPDATE   = @P_DTS_UPDATE
WHERE  CD_COMPANY   = @P_CD_COMPANY 
AND    NO_RCP       = @P_NO_RCP  
AND    NO_TX        = @P_NO_TX 

IF (@@ERROR <> 0 )  
BEGIN    
    SELECT @V_ERRNO  =  100000, 
        @V_ERRMSG = '작업을 정상적으로 처리하지 못했습니다.' 
    GOTO ERROR
END

SET NOCOUNT OFF
RETURN

ERROR:    
    ROLLBACK TRAN
    RAISERROR (@V_ERRMSG, 18, 1)
GO

