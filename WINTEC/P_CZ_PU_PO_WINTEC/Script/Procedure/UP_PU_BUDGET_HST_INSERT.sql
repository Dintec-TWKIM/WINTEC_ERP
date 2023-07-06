USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_BUDGET_HST_INSERT]    Script Date: 2022-03-24 오후 5:58:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************      
**  System : 구매자재관리      
**  Sub System : 발주관리      
**  Page  : 구매요청등록      
**  Desc  : 예산체크이력
**  참  고 : 
**  Return Values      
**      
**  작    성    자  : 이승훈
**  작    성    일  : 2009.11.19
**  수    정    자     :      
**  수    정    내   용 :       
*********************************************      
** Change History      
*********************************************      
*********************************************/     
  
ALTER  PROC [NEOE].[UP_PU_BUDGET_HST_INSERT]      
(      
	@P_CD_COMPANY NVARCHAR(7),
	@P_NO_PU	   NVARCHAR(20),
	@P_NENU_TYPE  NVARCHAR(20),
	--NO_HST     NUMERIC(3, 0)  NOT NULL DEFAULT (0),
	@P_CD_BUDGET NVARCHAR(40),
	@P_NM_BUDGET NVARCHAR(100),
	@P_CD_BGACCT NVARCHAR(40),
	@P_NM_BGACCT NVARCHAR(100),

	@P_AM_ACTSUM NUMERIC (17,4),
	@P_AM_JSUM   NUMERIC (17,4),
	@P_RT_JSUM   NUMERIC (17,4),
	@P_AM        NUMERIC (17,4),
	@P_AM_JAN    NUMERIC (17,4),
	@P_TP_BUNIT  NVARCHAR(40),

	@P_ERROR_MSG NVARCHAR(255),
	@P_ID_INSERT  NVARCHAR(15),   -- 등록자  
	@P_CD_BIZPLAN NVARCHAR(20) = '',
	@P_NM_BIZPLAN NVARCHAR(50) = ''
)      
AS      
BEGIN      
 DECLARE      
 @P_ERRNO      INT,          -- ERROR번호      
 @P_ERRMSG     VARCHAR(255), -- ERROR 메시지      
 @P_DTS_INSERT NVARCHAR(14)  -- 등록일  

 SET @P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')  
      
 INSERT INTO PU_BUDGET_HST (
		CD_COMPANY,
		NO_PU,
		NENU_TYPE,
		--NO_HST,
		CD_BUDGET, 
		NM_BUDGET,
		CD_BGACCT,
		NM_BGACCT,
		AM_ACTSUM,
		AM_JSUM,
		RT_JSUM,
		AM,
		AM_JAN,
		TP_BUNIT,
		ERROR_MSG,
		DTS_INSERT,
		ID_INSERT,
		CD_BIZPLAN,
		NM_BIZPLAN
		)      
         VALUES
		( 
		@P_CD_COMPANY,
		@P_NO_PU,
		@P_NENU_TYPE,
		--@P_NO_HST,
		@P_CD_BUDGET, 
		@P_NM_BUDGET,
		@P_CD_BGACCT,
		@P_NM_BGACCT,
		@P_AM_ACTSUM,
		@P_AM_JSUM,
		@P_RT_JSUM,
		@P_AM,
		@P_AM_JAN,
		@P_TP_BUNIT,
		@P_ERROR_MSG,
		@P_DTS_INSERT,
		@P_ID_INSERT,
		@P_CD_BIZPLAN,
		@P_NM_BIZPLAN
		)      
       
 IF (@@ERROR <> 0 )      
 BEGIN        
       SELECT @P_ERRNO  = 100000,        
              @P_ERRMSG = 'CM_M100010'        
       GOTO ERROR        
 END       
      
        
RETURN      
ERROR:        
    RAISERROR (@P_ERRMSG ,18,1)       
END
GO


