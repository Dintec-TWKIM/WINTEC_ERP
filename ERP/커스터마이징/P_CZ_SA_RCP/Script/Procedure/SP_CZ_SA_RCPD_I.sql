USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_RCPD_I]    Script Date: 2016-05-17 오후 3:48:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- 설    명 : 영업>채권관리>수금등록 반제 추가
-- 수 정 자 : 김명수
-- 수 정 일 : 2010.06.11
-- 유    형 : 수정
-- 내    역 : 에러발생시 ROLLBACK 처리
-- ================================================
ALTER PROCEDURE [NEOE].[SP_CZ_SA_RCPD_I]
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
	@P_BILL_PARTNER		NVARCHAR(20),	-- HEAD : 수금처
	@P_TYPE				NVARCHAR(1),	-- 0 : 매출(수금등록), 1 : 채권
	@P_NO_DOCU			NVARCHAR(20),
	@P_NO_DOLINE		NUMERIC(5, 0),
	@P_ID_INSERT		NVARCHAR(15),
	@P_CD_USERDEF1		NVARCHAR(3)		= NULL
)
AS
SET NOCOUNT ON

DECLARE @V_ERRNO	INT,			-- ERROR 번호  
        @V_ERRMSG	NVARCHAR(255)	-- ERROR 메시지 

DECLARE @P_DTS_INSERT VARCHAR(14)
SET @P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')

INSERT INTO SA_RCPD 
(
	CD_COMPANY,		NO_RCP,			NO_TX,			DT_IV,				TP_SO,        
	AM_IV_EX,		AM_IV,			RT_EXCH_IV,		AM_RCP_TX_EX,		AM_RCP_TX,
	AM_PL,			NO_DOCU,		NO_DOLINE,		ID_INSERT,		DTS_INSERT
)
VALUES
(
	@P_CD_COMPANY,	@P_NO_RCP,		@P_NO_TX,		@P_DT_IV,			@P_TP_SO,        
	@P_AM_IV_EX,	@P_AM_IV,		@P_RT_EXCH_IV,	@P_AM_RCP_TX_EX,	@P_AM_RCP_TX,
	@P_AM_PL,		@P_NO_DOCU,		@P_NO_DOLINE,	@P_ID_INSERT,	@P_DTS_INSERT
)

IF (@@ERROR <> 0 )  
BEGIN    
    SELECT @V_ERRNO  =  100000, 
        @V_ERRMSG = '작업을 정상적으로 처리하지 못했습니다.' 
    GOTO ERROR
END


--IF(@P_TYPE = '0') - SA_RCPD 트리거로이관 20080428
--BEGIN
--  -- 수금
--  UPDATE SA_IVH
--     SET AM_BAN = AM_BAN + @P_AM_RCP_TX, 
--         NO_BAN = @P_NO_RCP, 
--         ID_UPDATE = @P_ID_INSERT, 
--         DTS_UPDATE = @P_DTS_INSERT
--   WHERE CD_COMPANY = @P_CD_COMPANY        
--     AND NO_IV = @P_NO_TX        
--END
--ELSE IF(@P_TYPE = '1')
--BEGIN
--  --채권
--  UPDATE SA_AR
--     SET AM_BAN = AM_BAN + @P_AM_RCP_TX,
--         ID_UPDATE = @P_ID_INSERT,
--         DTS_UPDATE = @P_DTS_INSERT
--   WHERE CD_COMPANY = @P_CD_COMPANY
--     AND YYMM = SUBSTRING(@P_DT_IV, 1, 4) + '00'                
--     AND CD_PARTNER = @P_BILL_PARTNER
--END
SET NOCOUNT OFF
RETURN

ERROR:    
    ROLLBACK TRAN
    RAISERROR (@V_ERRMSG, 18, 1)
GO

