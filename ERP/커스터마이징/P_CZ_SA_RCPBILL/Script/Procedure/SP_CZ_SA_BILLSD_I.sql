USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_BILLSD_I]    Script Date: 2016-04-28 오후 2:33:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- 설    명 : 영업>채권관리>선수금등록 -> LINE INSERT
-- 수 정 자 : 김명수
-- 수 정 일 : 2010.06.11
-- 유    형 : 수정
-- 내    역 : 에러발생시 ROLLBACK 처리
-- ================================================

ALTER PROCEDURE [NEOE].[SP_CZ_SA_BILLSD_I]
(
	@P_CD_COMPANY		NVARCHAR(7), 
	@P_NO_BILLS			NVARCHAR(20), 
	@P_NO_IV			NVARCHAR(20), 
	@P_DT_IV			NVARCHAR(8), 
	@P_AM_TARGET		NUMERIC(17,4), 
	@P_AM_RCPS			NUMERIC(17,4), 
	@P_GUBUN			NVARCHAR(40), 
	@P_CD_PARTNER		NVARCHAR(20),
	@P_CD_EXCH_IV		NVARCHAR(3),
	@P_RT_EXCH_IV		NUMERIC(17,4), 
	@P_AM_TARGET_EX		NUMERIC(17,4), 
	@P_AM_RCPS_EX		NUMERIC(17,4), 
	@P_AM_PL			NUMERIC(17,4), 
	@P_NO_RCP			NVARCHAR(20),
	@P_NO_DOCU_RCP		NVARCHAR(20),
	@P_NO_DOLINE_RCP	NUMERIC(5, 0),
	@P_NO_DOCU_IV		NVARCHAR(20),
	@P_NO_DOLINE_IV 	NUMERIC(5, 0),
	@P_TP_SO			NVARCHAR(3)
) 
AS 
BEGIN
    DECLARE
	@ERRNO                    INT,
    @ERRMSG                   VARCHAR(255)

--IF (@GUBUN = '0')
--BEGIN
--	UPDATE         SA_IVH
--	SET AM_BAN = AM_BAN + @AM_BAN
--	WHERE CD_COMPANY = @CD_COMPANY
--	        AND NO_IV = @NO_IV
--END
--ELSE IF (@GUBUN = '1')
--BEGIN        
--	UPDATE SA_AR
--	SET AM_BAN = AM_BAN + @AM_BAN
--	WHERE CD_COMPANY = @CD_COMPANY
--	        AND CD_PARTNER = @CD_PARTNER
--	        AND YYMM = @NO_IV
--END
--트리거로 이관 

INSERT INTO SA_BILLSD
(
	CD_COMPANY, 
	NO_BILLS, 
	NO_IV, 
	DT_IV, 
	AM_TARGET, 
	AM_RCPS, 
	GUBUN,
    CD_EXCH_IV, 
	RT_EXCH_IV, 
	AM_TARGET_EX, 
	AM_RCPS_EX, 
	AM_PL, 
	NO_RCP,
	NO_DOCU_RCP,
	NO_DOLINE_RCP,
	NO_DOCU_IV,
	NO_DOLINE_IV,
	TP_SO 
)
VALUES
(
	@P_CD_COMPANY, 
	@P_NO_BILLS, 
	@P_NO_IV, 
	@P_DT_IV, 
	@P_AM_TARGET, 
	@P_AM_RCPS, 
	@P_GUBUN,
	@P_CD_EXCH_IV, 
	@P_RT_EXCH_IV, 
	@P_AM_TARGET_EX, 
	@P_AM_RCPS_EX, 
	@P_AM_PL, 
	@P_NO_RCP,
	@P_NO_DOCU_RCP,
	@P_NO_DOLINE_RCP,
	@P_NO_DOCU_IV,
	@P_NO_DOLINE_IV,
	@P_TP_SO
)

IF (@@ERROR <> 0 )
BEGIN  
      SELECT @ERRNO  = 100000,  
             @ERRMSG = 'SA_BILLSD INSERT ERROR '  
      GOTO ERROR  
END

RETURN
ERROR: 
    ROLLBACK TRAN 
    RAISERROR (@ERRMSG, 18 , 1)  
END
GO

