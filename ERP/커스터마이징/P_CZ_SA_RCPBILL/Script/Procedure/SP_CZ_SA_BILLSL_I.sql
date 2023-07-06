USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_BILLSL_I]    Script Date: 2016-04-28 오후 2:32:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- 설    명 : 영업>채권관리>선수금등록 -> LINE INSERT
--            영업>전용메뉴>KTIS>선수금정리등록(KTIS) -> LINE INSERT
-- 수 정 자 : 김명수
-- 수 정 일 : 2010.06.11
-- 유    형 : 수정
-- 내    역 : 에러발생시 ROLLBACK 처리
-- ================================================
-- HISTORY  : 2010.09.16 관리번호 추가(BY SJH)
-- ================================================

ALTER PROCEDURE [NEOE].[SP_CZ_SA_BILLSL_I]
(
	@P_CD_COMPANY		NVARCHAR(7), 
	@P_NO_BILLS			NVARCHAR(20), 
	@P_NO_LINE			NUMERIC(3,0),
	@P_AM_TARGET		NUMERIC(17,4), 
	@P_AM_BILLS			NUMERIC(17,4), 
	@P_NO_RCP			NVARCHAR(20),
	@P_CD_EXCH			NVARCHAR(3),
	@P_RT_EXCH			NUMERIC(17,4), 
	@P_AM_TARGET_EX		NUMERIC(17,4), 
	@P_AM_BILLS_EX		NUMERIC(17,4),
	@P_NO_MGMT			NVARCHAR(40) = NULL,
	@P_NO_DOCU			NVARCHAR(20),
	@P_NO_DOLINE		NUMERIC(5, 0),
	@P_CD_PJT			NVARCHAR(20)
) 
AS 
BEGIN

DECLARE	@ERRNO		INT,
		@ERRMSG		VARCHAR(255)

INSERT INTO SA_BILLSL
(
	CD_COMPANY,	
	NO_BILLS,	
	NO_LINE,		
	AM_TARGET,		
	AM_BILLS,	
	NO_RCP,
	CD_EXCH,		
	RT_EXCH,	
	AM_TARGET_EX,	
	AM_BILLS_EX,	
	NO_MGMT,
	NO_DOCU,
	NO_DOLINE,
	CD_PJT  
)
VALUES
(
	@P_CD_COMPANY,	
	@P_NO_BILLS,	
	@P_NO_LINE,		
	@P_AM_TARGET,		
	@P_AM_BILLS,	
	@P_NO_RCP,
	@P_CD_EXCH,		
	@P_RT_EXCH,	
	@P_AM_TARGET_EX,	
	@P_AM_BILLS_EX,	
	@P_NO_MGMT,
	@P_NO_DOCU,
	@P_NO_DOLINE,
	@P_CD_PJT 
)

IF (@@ERROR <> 0 )
BEGIN  
SELECT	@ERRNO	= 100000,  
		@ERRMSG	= 'SA_BILLSL INSERT ERROR'  
GOTO	ERROR  
END

RETURN

ERROR:  
    ROLLBACK TRAN
    RAISERROR (@ERRMSG, 18 , 1)  
END
GO

