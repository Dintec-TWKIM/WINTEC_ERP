USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_POL_DELETE]    Script Date: 2022-03-24 ���� 3:46:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


------*------ 
    
/* ���ֶ��� ����   */    
ALTER PROC [NEOE].[UP_PU_POL_DELETE]
(    
@NO_PO  NVARCHAR(20),  -- ���ֹ�ȣ    
@NO_LINE  NUMERIC(5),  -- ���� ����    
@CD_COMPANY  NVARCHAR(7)  -- ȸ��    
)    
AS    
DECLARE        @ERRNO      INT,    
	@ERRMSG    NVARCHAR(255),    

	@QT_PO   NUMERIC(17,4),  -- ���ּ���    
	@QT_REQ  NUMERIC(17,4),  -- �Ƿڼ���    
	@YN_ORDER  NVARCHAR(1),  -- ��������    
	@FG_POST   NVARCHAR(3),  -- ���ֻ���      
	@NO_CONTRACT  NVARCHAR(20),  -- ����ȣ    
	@NO_CTLINE  NUMERIC(3),  -- ������    
	@NO_PR   NVARCHAR(20),  -- ��û��ȣ    
	@NO_PRLINE  NUMERIC(5),  -- ��û����     
	@NO_APP  NVARCHAR(20),  -- ǰ�ǹ�ȣ    
	@NO_APPLINE  NUMERIC(5),  -- ǰ�Ƕ���    
	@YN_IMPORT  NVARCHAR(1),    
	@QT_TR   NUMERIC(17,4)    
    
-- ���� ����  -- ���� ���� ������ ��� �ұ�..     
IF EXISTS (SELECT 1 FROM PU_POL WHERE NO_PO = @NO_PO AND NO_LINE =@NO_LINE AND CD_COMPANY = @CD_COMPANY)    
BEGIN    
SELECT @QT_PO = 0 - QT_PO , @QT_REQ = QT_REQ, @YN_ORDER = YN_ORDER , @FG_POST = FG_POST,    
	@NO_CONTRACT = NO_CONTRACT, @NO_CTLINE = NO_CTLINE, @NO_PR = NO_PR, @NO_PRLINE = NO_PRLINE,    
	@NO_APP = NO_APP, @NO_APPLINE = NO_APPLINE , @YN_IMPORT =YN_IMPORT, @QT_TR = QT_TR    
FROM PU_POL     
WHERE NO_PO = @NO_PO AND NO_LINE = @NO_LINE AND CD_COMPANY = @CD_COMPANY    
  
/* PU_POL.TD_PU_POL Ʈ���ŷ� �ű�    
  -- �ڵ������̸� .. �Ƿڷ��� 0�̿�����    
  IF( @YN_ORDER = 'Y')    
  BEGIN    
   IF(ISNULL( @QT_REQ, 0)  > 0)    
   BEGIN      
         SELECT @ERRNO  = 100000,      
                      @ERRMSG = 'PU_M000038'      
         GOTO ERROR      
   END     
  END    
  ELSE  -- Ȯ���� �ȵȰ͸�..��������..     
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
      
  -- ǰ�ǵ�� �ܷ�����    
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
      
     
  -- ���ſ�û �ܷ�����    
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
-- ���� ���� ����    
DELETE PU_POL WHERE NO_PO = @NO_PO AND NO_LINE = @NO_LINE AND CD_COMPANY = @CD_COMPANY

IF (@@ERROR <> 0 ) BEGIN SELECT @ERRNO = 100000, @ERRMSG = '[UP_PU_POL_DELETE]�۾��� ���������� ó������ ���߽��ϴ�.' GOTO ERROR END    


 /*   
  --���ſ�û �������� UPDATE: ��Ͻ� 'O', ���ֽ� 'R', �԰�Ϸ� �Ǵ� ���ָ����� 'C'----------����    
  -- ����� ���ֻ��� ����.. ���ּ���(QT_PO)�� 0�̸� 'O'�� ���� ����    
     
  -- ���� ���� ���̺�(PU_POL)���� ����ǰ�� ��ȣ�� ����ǰ�� ���� ��ȣ�� ������    
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
     
  -- ���� UPDATE ����.    
  -- ����ǰ�� ���� ���¸�  ���ּ���(QT_PO)�� 0�̸� �������¸� ��Ͻ��� 'O' �� ���� UPDATE    
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
  --���ſ�û �������� UPDATE: ��Ͻ� 'O', ���ֽ� 'R', �԰�Ϸ� �Ǵ� ���ָ����� 'C'----------��    
 */  
END    
 
RETURN

ERROR: RAISERROR(@ERRMSG, 16, 1) -- 2012/08/13:�ڵ�ȭ ����[RAISERROR], ������:RAISERROR @ERRNO @ERRMSG 


GO


