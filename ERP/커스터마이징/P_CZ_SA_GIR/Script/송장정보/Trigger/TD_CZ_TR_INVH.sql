USE [NEOE]
GO

/****** Object:  Trigger [NEOE].[TD_CZ_TR_INVH]    Script Date: 2015-10-22 오후 5:32:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**********************************************************************************************************    
 * 프로시저명: TD_TR_INVH  
 * 관련페이지: 수출송장 DELETE 트리거  
 * 설      명:   
 * 작  성  자: 이재윤  
 *********************************************************************************************************/   
  
ALTER TRIGGER [NEOE].[TD_CZ_TR_INVH] ON [NEOE].[CZ_TR_INVH]  
AFTER DELETE  
AS  
BEGIN  
DECLARE @CD_COMPANY  NVARCHAR(7),  
        @NO_INV      NVARCHAR(20),   
        @NO_TO       NVARCHAR(20),   
        @NO_BL       NVARCHAR(20) 
           
SELECT  @CD_COMPANY  = CD_COMPANY,        --회사  
        @NO_INV      = NO_INV,             --송장번호  
		@NO_TO		 = NO_TO, 
		@NO_BL		 = NO_BL 
FROM    DELETED  
 
--IF (@NO_TO IS NOT NULL OR @NO_TO <> '')    
-- BEGIN    
--   RAISERROR ('통관자료가 존재하므로 삭제할 수 없습니다.' ,18 ,1)    
--   RETURN    
-- END   

IF (@NO_BL IS NOT NULL OR @NO_BL <> '')    
 BEGIN    
   RAISERROR ('선적자료가 존재하므로 삭제할 수 없습니다.', 18, 1)    
   RETURN    
 END   

  
SET NOCOUNT ON  
DECLARE @NO_LINE NUMERIC(5)    

DECLARE CUR CURSOR FOR    
SELECT NO_LINE FROM SHIP_MARK WHERE CD_COMPANY = @CD_COMPANY AND NO_INV = @NO_INV  
    
OPEN CUR    
FETCH NEXT FROM CUR INTO @NO_LINE    
WHILE @@FETCH_STATUS = 0    
BEGIN    
 DELETE    
 FROM SHIP_MARK    
 WHERE CD_COMPANY = @CD_COMPANY  
 AND  NO_INV  = @NO_INV    
 AND  NO_LINE  = @NO_LINE    
  
 FETCH NEXT FROM CUR INTO @NO_LINE    
END  
  
CLOSE CUR  
DEALLOCATE CUR  
END
GO

