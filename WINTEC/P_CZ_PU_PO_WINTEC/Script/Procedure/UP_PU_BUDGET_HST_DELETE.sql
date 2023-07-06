USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_BUDGET_HST_DELETE]    Script Date: 2022-03-24 오후 5:56:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



  
 ALTER PROC [NEOE].[UP_PU_BUDGET_HST_DELETE]      
(      
@P_CD_COMPANY NVARCHAR(7),
@P_NO_PU           NVARCHAR(20),
@P_NENU_TYPE  NVARCHAR(20)
--NO_HST     NUMERIC(3, 0)  NOT NULL DEFAULT (0),
)      
AS      
BEGIN      
 DECLARE      
 @P_ERRNO      INT,          -- ERROR번호      
 @P_ERRMSG     VARCHAR(255), -- ERROR 메시지      
 @P_DTS_INSERT NVARCHAR(14)  -- 등록일  

 --SET @P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')  
      
 DELETE FROM PU_BUDGET_HST 
 WHERE  CD_COMPANY = @P_CD_COMPANY
   AND        NO_PU      = @P_NO_PU
   AND        NENU_TYPE  = @P_NENU_TYPE
       
 IF (@@ERROR <> 0 )      
 BEGIN        
       SELECT @P_ERRNO  = 100000,        
              @P_ERRMSG = 'CM_M100010'        
       GOTO ERROR        
 END       
      
        
RETURN      
ERROR:        
    RAISERROR(@P_ERRMSG, 16, 1) -- 2012/08/13:자동화 변경[RAISERROR], 변경전:RAISERROR @P_ERRNO @P_ERRMSG 
       
END        
  
  


GO


