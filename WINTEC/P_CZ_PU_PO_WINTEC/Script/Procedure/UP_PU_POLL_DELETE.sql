USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_POLL_DELETE]    Script Date: 2022-03-24 ���� 6:01:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--���ֵ�� ������� DELETE  
ALTER PROCEDURE [NEOE].[UP_PU_POLL_DELETE]  
(  
	@P_CD_COMPANY        NVARCHAR(7),
	@P_CD_PLANT          NVARCHAR(7),    
	@P_NO_PO             NVARCHAR(20),   
	@P_NO_POLINE         NUMERIC(5,0),   
	@P_NO_LINE           NUMERIC(5,0),
	@P_NO_RELATION		 NVARCHAR(20) = NULL,
	@P_SEQ_RELATION		 NUMERIC(5,0) = 0,
	@P_NUM_USERDEF1		 NUMERIC(19,6) = 0,
	@P_QT_NEED           NUMERIC(17, 4) = 0
)
AS  
DECLARE        @ERRNO        INT,   
               @ERRMSG       NVARCHAR(255),
               @V_SERVER_KEY NVARCHAR(25)  
--                @CD_MATL                        NVARCHAR(20),   
--                @QT_REQ                                NUMERIC(17, 4)  
--  
--SELECT @CD_MATL = CD_MATL, @QT_REQ = QT_REQ  
--FROM SU_POLL  
--WHERE CD_COMPANY = @P_CD_COMPANY  
--        AND NO_PO = @P_NO_PO  
--        AND NO_POLINE = @P_NO_POLINE  
--        AND NO_LINE = @P_NO_LINE  
--  
--IF (@QT_REQ > 0)  
--BEGIN  
--        SELECT @ERRNO  = 100000, @ERRMSG = '�����Ϸ��� ����('+@CD_MATL+')�� �Ƿڷ�(QT_REQ)�� �����մϴ�. ������ �� �����ϴ�.'  
--END  
			     
SELECT @V_SERVER_KEY = SERVER_KEY
  FROM CM_SERVER_CONFIG
 WHERE YN_UPGRADE = 'Y'   
 

 
IF (@V_SERVER_KEY LIKE 'DONGWOON%' )
BEGIN
	UPDATE MM_QTIOLOT
	   SET NUM_USERDEF1 = ISNULL(NUM_USERDEF1,0) - ISNULL(@P_QT_NEED,0)
	 WHERE NO_IO = @P_NO_RELATION
	   AND NO_IOLINE = @P_SEQ_RELATION
	   AND NO_IOLINE2 = @P_NUM_USERDEF1
	   AND CD_COMPANY = @P_CD_COMPANY
END  
 
DELETE PU_POLL  
WHERE CD_COMPANY = @P_CD_COMPANY 
AND CD_PLANT = @P_CD_PLANT
AND NO_PO = @P_NO_PO  
AND NO_POLINE = @P_NO_POLINE  
AND NO_LINE = @P_NO_LINE  
  
IF (@@ERROR <> 0 ) BEGIN SELECT @ERRNO  = 100000, @ERRMSG = '�۾��� ���������� ó������ ���߽��ϴ�.' GOTO ERROR END  
  
RETURN  
ERROR: RAISERROR (@ERRMSG, 18 ,1 )
GO


