USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_POL_DELETE]    Script Date: 2022-03-24 오후 3:46:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


------*------ 
    
/* 발주라인 삭제   */    
ALTER PROC [NEOE].[UP_PU_POL_DELETE]
(    
@NO_PO  NVARCHAR(20),  -- 발주번호    
@NO_LINE  NUMERIC(5),  -- 발주 라인    
@CD_COMPANY  NVARCHAR(7)  -- 회사    
)    
AS    
DECLARE        @ERRNO      INT,    
	@ERRMSG    NVARCHAR(255),    

	@QT_PO   NUMERIC(17,4),  -- 발주수량    
	@QT_REQ  NUMERIC(17,4),  -- 의뢰수량    
	@YN_ORDER  NVARCHAR(1),  -- 오더유무    
	@FG_POST   NVARCHAR(3),  -- 발주상태      
	@NO_CONTRACT  NVARCHAR(20),  -- 계약번호    
	@NO_CTLINE  NUMERIC(3),  -- 계약라인    
	@NO_PR   NVARCHAR(20),  -- 요청번호    
	@NO_PRLINE  NUMERIC(5),  -- 요청라인     
	@NO_APP  NVARCHAR(20),  -- 품의번호    
	@NO_APPLINE  NUMERIC(5),  -- 품의라인    
	@YN_IMPORT  NVARCHAR(1),    
	@QT_TR   NUMERIC(17,4)    
    
-- 존재 유뮤  -- 존재 하지 않으면 어떻게 할까..     
IF EXISTS (SELECT 1 FROM PU_POL WHERE NO_PO = @NO_PO AND NO_LINE =@NO_LINE AND CD_COMPANY = @CD_COMPANY)    
BEGIN    
SELECT @QT_PO = 0 - QT_PO , @QT_REQ = QT_REQ, @YN_ORDER = YN_ORDER , @FG_POST = FG_POST,    
	@NO_CONTRACT = NO_CONTRACT, @NO_CTLINE = NO_CTLINE, @NO_PR = NO_PR, @NO_PRLINE = NO_PRLINE,    
	@NO_APP = NO_APP, @NO_APPLINE = NO_APPLINE , @YN_IMPORT =YN_IMPORT, @QT_TR = QT_TR    
FROM PU_POL     
WHERE NO_PO = @NO_PO AND NO_LINE = @NO_LINE AND CD_COMPANY = @CD_COMPANY    
  
/* PU_POL.TD_PU_POL 트리거로 옮김    
  -- 자동승인이면 .. 의뢰량이 0이여야함    
  IF( @YN_ORDER = 'Y')    
  BEGIN    
   IF(ISNULL( @QT_REQ, 0)  > 0)    
   BEGIN      
         SELECT @ERRNO  = 100000,      
                      @ERRMSG = 'PU_M000038'      
         GOTO ERROR      
   END     
  END    
  ELSE  -- 확정이 안된것만..삭제가능..     
  BEGIN     
   IF(ISNULL( @FG_POST, 'O') <> 'O' )    
   BEGIN      
         SELECT @ERRNO  = 100000,      
                      @ERRMSG = 'PU_M000038'      
         GOTO ERROR      
   END     
  END     
    
  IF( @YN_IMPORT = 'Y')    
  BEGIN    
   IF(ISNULL( @QT_TR, 0)  > 0)    
   BEGIN      
         SELECT @ERRNO  = 100000,      
                      @ERRMSG = 'PU_M000038'      
         GOTO ERROR      
   END     
  END    
      
  -- 품의등록 잔량관리    
  IF( ISNULL(@NO_APP, '') <> '' AND ISNULL(@NO_APPLINE,0) > 0 AND @QT_PO <> 0 )    
  BEGIN    
   EXEC UP_PU_APPL_FROM_POL_UPDATE @CD_COMPANY, @NO_APP, @NO_APPLINE ,@QT_PO      
   IF (@@ERROR <> 0 )    
   BEGIN      
         SELECT @ERRNO  = 100000,      
                      @ERRMSG = 'CM_M100010'      
         GOTO ERROR      
   END     
  END    
      
     
  -- 구매요청 잔량관리    
  IF( ISNULL(@NO_PR, '') <> '' AND ISNULL(@NO_PRLINE,0) > 0  AND @QT_PO <> 0 )    
  BEGIN    
   EXEC UP_PU_PRL_FROM_POL_UPDATE @CD_COMPANY, @NO_PR, @NO_PRLINE ,@QT_PO      
   IF (@@ERROR <> 0 )    
   BEGIN      
         SELECT @ERRNO  = 100000,      
                      @ERRMSG = 'CM_M100010'      
         GOTO ERROR      
   END     
  END      
 */   
-- 발주 라인 삭제    
DELETE PU_POL WHERE NO_PO = @NO_PO AND NO_LINE = @NO_LINE AND CD_COMPANY = @CD_COMPANY

IF (@@ERROR <> 0 ) BEGIN SELECT @ERRNO = 100000, @ERRMSG = '[UP_PU_POL_DELETE]작업을 정상적으로 처리하지 못했습니다.' GOTO ERROR END    


 /*   
  --구매요청 오더상태 UPDATE: 등록시 'O', 발주시 'R', 입고완료 또는 발주마감시 'C'----------시작    
  -- 현재는 발주삭제 시점.. 발주수량(QT_PO)이 0이면 'O'로 상태 변경    
     
  -- 발주 라인 테이블(PU_POL)에서 구매품의 번호와 구매품의 라인 번호를 가져옴    
  --NO_APP, NO_APPLINE, CD_COMPANY    
  SELECT @NO_PR = NO_PR, @NO_PRLINE = NO_PRLINE     
  FROM PU_POL    
  WHERE CD_COMPANY = @CD_COMPANY    
   AND NO_PO = @NO_PO    
   AND NO_LINE = @NO_LINE    
     
  IF (@@ERROR <> 0 )    
  BEGIN      
        SELECT @ERRNO  = 100000,      
                     @ERRMSG = 'CM_M100010'      
        GOTO ERROR      
  END     
     
  -- 실제 UPDATE 구문.    
  -- 구매품의 라인 상태를  발주수량(QT_PO)이 0이면 오더상태를 등록시점 'O' 로 상태 UPDATE    
  --NO_PR, NO_PRLINE, CD_COMPANY    
  IF ( (SELECT ISNULL(QT_PO, 0) FROM PU_PRL    
   WHERE CD_COMPANY = @CD_COMPANY    
    AND NO_PR = @NO_PR    
    AND NO_PRLINE = @NO_PRLINE) = 0 
  )   
  BEGIN    
   UPDATE PU_PRL    
   SET FG_POST = 'O'    
   WHERE CD_COMPANY = @CD_COMPANY    
    AND NO_PR = @NO_PR    
    AND NO_PRLINE = @NO_PRLINE    
      
   IF (@@ERROR <> 0 )    
   BEGIN      
SELECT @ERRNO  = 100000,      
                      @ERRMSG = 'CM_M100010'      
         GOTO ERROR      
   END     
  END    
  --구매요청 오더상태 UPDATE: 등록시 'O', 발주시 'R', 입고완료 또는 발주마감시 'C'----------끝    
 */  
END    
 
RETURN

ERROR: RAISERROR(@ERRMSG, 16, 1) -- 2012/08/13:자동화 변경[RAISERROR], 변경전:RAISERROR @ERRNO @ERRMSG 


GO


