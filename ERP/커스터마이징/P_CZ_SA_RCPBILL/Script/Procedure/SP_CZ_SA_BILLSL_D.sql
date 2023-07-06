USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_BILLSL_D]    Script Date: 2016-04-28 오후 2:32:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- 설    명 : 영업>채권관리>선수금등록 -> LINE DELETE
--          : 영업>전용메뉴>KTIS>선수금정리등록(KTIS) -> LINE DELETE
-- 수 정 자 : 김명수
-- 수 정 일 : 2010.06.11
-- 유    형 : 수정
-- 내    역 : 에러발생시 ROLLBACK 처리
-- ================================================

ALTER PROCEDURE [NEOE].[SP_CZ_SA_BILLSL_D]
(
	@CD_COMPANY             NVARCHAR(7), 
	@NO_BILLS               NVARCHAR(20), 
	@NO_LINE                NUMERIC(3,0)
) 
AS 
BEGIN
    DECLARE
	@ERRNO               INT,
    @ERRMSG              VARCHAR(255), 
	@NO_RCP              NVARCHAR(20), 
	@AM_BILLS            NUMERIC(17,4)

--SELECT @NO_RCP = NO_RCP, @AM_BILLS = AM_BILLS 
--FROM SA_BILLSL
--WHERE CD_COMPANY = @CD_COMPANY
--	AND NO_BILLS = @NO_BILLS
--	AND NO_LINE = @NO_LINE
--
--UPDATE SA_RCPH
--SET AM_RCPS = AM_RCPS - @AM_BILLS
--WHERE CD_COMPANY = @CD_COMPANY
--	AND NO_RCP = @NO_RCP
--트리거로 이관 

DELETE SA_BILLSL
WHERE CD_COMPANY = @CD_COMPANY
	AND NO_BILLS = @NO_BILLS
	AND NO_LINE = @NO_LINE

IF (@@ERROR <> 0 )
BEGIN  
      SELECT @ERRNO  = 100000,  
                   @ERRMSG = 'CM_M100010'  
      GOTO ERROR  
END

RETURN
ERROR:  
    ROLLBACK TRAN
    RAISERROR (@ERRMSG, 18 , 1)  
END
GO

