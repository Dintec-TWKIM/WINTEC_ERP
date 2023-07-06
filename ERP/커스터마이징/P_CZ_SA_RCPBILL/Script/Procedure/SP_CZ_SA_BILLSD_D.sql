USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_BILLSD_D]    Script Date: 2016-04-28 오후 2:33:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- 설    명 : 영업>채권관리>선수금등록 -> LINE DELETE
-- 수 정 자 : 김명수
-- 수 정 일 : 2010.06.11
-- 유    형 : 수정
-- 내    역 : 에러발생시 ROLLBACK 처리
-- ================================================

ALTER PROCEDURE [NEOE].[SP_CZ_SA_BILLSD_D]
(
	@CD_COMPANY       NVARCHAR(7), 
	@NO_BILLS         NVARCHAR(20), 
	@NO_IV            NVARCHAR(20), 
	@GUBUN            NVARCHAR(40), 
	@CD_PARTNER       NVARCHAR(20),
	@NO_RCP           NVARCHAR(20)
) 
AS 
BEGIN
    DECLARE
	@ERRNO                 INT,
    @ERRMSG                VARCHAR(255), 
	@AM_BAN                NUMERIC(17,4)

--SELECT @AM_BAN = ISNULL(AM_RCPS, 0)
--FROM SA_BILLSD
--WHERE CD_COMPANY = @CD_COMPANY
--	AND NO_BILLS = @NO_BILLS
--	AND NO_IV = @NO_IV
--
--IF (@GUBUN = '0')
--BEGIN
--	UPDATE         SA_IVH
--	SET AM_BAN = ISNULL(AM_BAN, 0) - @AM_BAN
--	WHERE CD_COMPANY = @CD_COMPANY
--	        AND NO_IV = @NO_IV
--END
--ELSE IF (@GUBUN = '1')
--BEGIN        
--	UPDATE SA_AR
--	SET AM_BAN = ISNULL(AM_BAN, 0) - @AM_BAN
--	WHERE CD_COMPANY = @CD_COMPANY
--	        AND CD_PARTNER = @CD_PARTNER
--	        AND YYMM = @NO_IV
--END
-- 트리거로 이관 

DELETE SA_BILLSD
WHERE CD_COMPANY = @CD_COMPANY
	AND NO_BILLS = @NO_BILLS
	AND NO_IV = @NO_IV
	AND NO_RCP = @NO_RCP

IF (@@ERROR <> 0 )
BEGIN  
      SELECT @ERRNO  = 100000,  
             @ERRMSG = 'SA_BILLSD DELETE ERROR'  
      GOTO ERROR  
END

RETURN
ERROR:  
    ROLLBACK TRAN
    RAISERROR (@ERRMSG, 18 , 1)  
END
GO

