USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_REQ_INSERT]    Script Date: 2022-03-24 ���� 5:48:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

             
ALTER PROC [NEOE].[UP_PU_REQ_INSERT]                  
(                  
	  @P_NO_RCV			NVARCHAR(20),                        -- �Ƿڹ�ȣ                     
	  @P_NO_LINE		NUMERIC(5),                        -- �׹�                  
	  @P_CD_COMPANY		NVARCHAR(7),                -- ȸ��                  
	  @P_NO_PO			NVARCHAR(20),                        -- ���ֹ�ȣ                  
	  @P_NO_POLINE		NUMERIC(5),                        -- �����׹�                  
	  @P_CD_PURGRP		NVARCHAR(7),                        -- ���ű׷�                  
	  @P_DT_LIMIT		NCHAR(8),                                -- ������                   
	  @P_CD_ITEM		NVARCHAR(50),                        -- ǰ��                  
	  @P_QT_REQ			NUMERIC(17,4),                        -- �Ƿڼ���                  
	  @P_YN_INSP		NCHAR(1),                                -- �˻�����                  
	  @P_QT_PASS		NUMERIC(17,4),                -- �հݷ�                  
	  @P_QT_REJECTION   NUMERIC(17,4),        -- ���հݷ�                   
	  @P_CD_UNIT_MM		NVARCHAR(3),                -- �������                  
	  @P_QT_REQ_MM		NUMERIC(17,4),        -- �����Ƿڼ���                  
	  @P_CD_EXCH		NVARCHAR(3),                        -- ȯ��                  
	  @P_RT_EXCH		NUMERIC(11,4),                -- ȯ��                  
	  @P_UM_EX_PO		NUMERIC(19,6),                -- ���ִܰ�                  
	  @P_UM_EX			NUMERIC(19,6),                        -- �ܰ�                  
	  @P_AM_EX			NUMERIC(19,6),                        -- �ݾ�                  
	  @P_UM				NUMERIC(19,6),                        -- ��ȭ�ܰ�                  
	  @P_AM				NUMERIC(17,4),                        -- ��ȭ�ݾ�                  
	  @P_VAT			NUMERIC(17,4),                        -- �ΰ���                  
	  @P_RT_CUSTOMS		NUMERIC(15,2),                -- ����ȯ����                  
	  @P_CD_PJT			NVARCHAR(20),                        -- ������Ʈ                  
	  @P_YN_PURCHASE	NCHAR(1),                        -- ��������                  
	  @P_YN_RETURN		NCHAR(1),                        -- ��ǰ����                  
	  @P_FG_TPPURCHASE	NCHAR(3),                        -- ��������                  
	  @P_FG_RCV			NCHAR(3),                                -- �԰�����                  
	  @P_FG_TRANS		NCHAR(3),                                -- �ŷ�����                  
	  @P_FG_TAX			NCHAR(3),                                -- ���ı���                  
	  @P_FG_TAXP		NCHAR(3),                                -- ��꼭ó������                  
	  @P_YN_AUTORCV		NCHAR(1),                        -- �ڵ��԰�                  
	  @P_YN_REQ			NCHAR(1),                                -- �Ƿ�����                  
	  @P_CD_SL			NVARCHAR(7),                        -- â��                  
	  @P_NO_LC			NVARCHAR(20),                        -- LC��ȣ                  
	  @P_NO_LCLINE		NUMERIC(5),                        -- LC�׹�                  
	  @P_RT_SPEC		NUMERIC(5,2),                        -- Ư�Ҽ���                  
	  @P_NO_EMP			NVARCHAR(10),                        -- �����                   
	  @P_NO_TO			NVARCHAR(20),                        -- �Ű��ȣ                  
	  @P_NO_TO_LINE		NUMERIC(5),                -- �Ű��׹�                
	  @P_CD_PLANT		NVARCHAR(7),                   
	  @P_CD_PARTNER		NVARCHAR(20),                 
	  @P_DT_REQ			NCHAR(8),                   
	  @P_DC_RMK			NVARCHAR(200),            
	  @YN_AUTOSTOCK		CHAR(1) ,           
	  @P_NO_REV			NVARCHAR(20) = NULL,          
	  @P_NO_REVLINE		NUMERIC(5) = 0 ,         
	  @P_CD_WH			NVARCHAR(7) = NULL,
	  @P_DC_RMK2		NVARCHAR(200) = NULL,
	  @P_SEQ_PROJECT	NUMERIC(5,0) = 0,
	  @P_NO_WBS			NVARCHAR(20) = NULL,
	  @P_NO_CBS			NVARCHAR(20)  = NULL,
	  @P_TP_UM_TAX		NVARCHAR(3)  = '002', --�ΰ������Կ��� 002:����, 001:����      
	  @FG_SPECIAL		NVARCHAR(3) = NULL, -- Ưä���� 001: Ưä + �հ� ���� 002: �հ� ���� 003: Ưä ����
	  @P_DATE_USERDEF1	NCHAR(8) = NULL,
	  @P_CDSL_USERDEF1  NVARCHAR(7) = NULL,
	  @P_NUM_USERDEF1   NUMERIC(17,4) = 0,
	  @P_NUM_USERDEF2   NUMERIC(17,4) = 0,
	  @P_UM_WEIGHT	    NUMERIC(17,4) = 0,
	  @P_TOT_WEIGHT     NUMERIC(17,4) = 0,
	  @P_CD_USERDEF1	NVARCHAR(20)  = NULL,
	  @P_CD_USERDEF2	NVARCHAR(20)  = NULL,
	  @P_NM_USERDEF1	NVARCHAR(100)  = NULL,
	  @P_NM_USERDEF2	NVARCHAR(100)  = NULL,
	  @P_DATE_USERDEF2	NCHAR(8)   = NULL,
	  @P_NO_LOT			NVARCHAR(100) = NULL,
	  @P_DT_LIMIT_LOT	NVARCHAR(8) = NULL,
	  @P_ID_INSERT		NVARCHAR(15) = NULL,
	  @P_TXT_USERDEF1	NVARCHAR(100) = NULL,
	  @P_TXT_USERDEF2	NVARCHAR(100) = NULL
)                  
AS                  
 DECLARE                  
 @ERRNO			INT,                
 @ERRMSG			VARCHAR(255),
 @P_CD_BIZAREA      NVARCHAR(7),    
 @P_AM_FSIZE		NUMERIC(3,0),   -- �Ҽ���    
 @P_AM_UPDOWN		NVARCHAR(3),   -- �ø�����    
 @P_AM_FORMAT		NVARCHAR(50),   -- ����(#,###,###,###.00)    
 @V_CD_EXC           NVARCHAR(3),  
 @V_CD_EXC_MENU      NVARCHAR(3)
 
  EXEC UP_SF_GETUNIT_AM @P_CD_COMPANY,'PU', 'I',  @P_AM_FSIZE OUTPUT,  @P_AM_UPDOWN OUTPUT,  @P_AM_FORMAT OUTPUT   -- ��⺰��������������                             
  SET @ERRNO = 0            
  --SET @P_AM = FLOOR(@P_AM)
  SET @P_AM = NEOE.FN_SF_GETUNIT_AM ('PU','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, @P_AM)  -- �ݾ׿������������Ŵ                      
  --SET @P_VAT = FLOOR(@P_VAT)                  
  SET @P_VAT = NEOE.FN_SF_GETUNIT_AM ('PU','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, @P_VAT)  -- �ݾ׿������������Ŵ 
  
  SELECT @V_CD_EXC = CD_EXC FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY  AND EXC_TITLE = '�������������ۿ��� �������'  
  SELECT @V_CD_EXC_MENU = CD_EXC FROM MA_EXC_MENU WHERE CD_COMPANY = @P_CD_COMPANY 
													AND CD_TITLE = 'PU_A00000042'
													AND NM_TITLE = '�԰��Ƿ�_�������������ۿ���_�������'                                                  
                
 -- PU_RCVL ���̺� ����                  
 EXEC UP_PU_RCVL_INSERT               
 @P_NO_RCV,               
 @P_NO_LINE,               
 @P_CD_COMPANY,               
 @P_NO_PO,               
 @P_NO_POLINE,           
 @P_CD_PURGRP,               
 @P_DT_LIMIT,               
 @P_CD_ITEM,               
 @P_QT_REQ,                   
 @P_YN_INSP,               
 @P_QT_PASS,               
 @P_QT_REJECTION,               
 @P_CD_UNIT_MM,                   
 @P_QT_REQ_MM,               
 @P_CD_EXCH,               
 @P_RT_EXCH,               
 @P_UM_EX_PO,                   
 @P_UM_EX,               
 @P_AM_EX,               
 @P_UM,               
 @P_AM,             
 @P_VAT,                  
 @P_RT_CUSTOMS,               
 @P_CD_PJT,               
 @P_YN_PURCHASE,               
 @P_YN_RETURN,                   
 @P_FG_TPPURCHASE,               
 @P_FG_RCV,               
 @P_FG_TRANS,               
 @P_FG_TAX,               
 @P_FG_TAXP,                   
 @P_YN_AUTORCV,               
 @P_YN_REQ,               
 @P_CD_SL,               
 @P_NO_LC,               
 @P_NO_LCLINE,                   
 @P_RT_SPEC,               
 @P_NO_EMP, NULL, NULL, NULL, NULL,                  
 @P_NO_TO,               
 @P_NO_TO_LINE,              
 @P_DC_RMK,             
 @YN_AUTOSTOCK,          
 @P_NO_REV,          
 @P_NO_REVLINE,
 @P_CD_WH,
 @P_DC_RMK2,
 @P_SEQ_PROJECT,
 @P_NO_WBS,
 @P_NO_CBS,
 @P_TP_UM_TAX,
 @FG_SPECIAL,
 @P_DATE_USERDEF1,	
 @P_CDSL_USERDEF1,
 @P_NUM_USERDEF1,
 @P_NUM_USERDEF2,
 @P_UM_WEIGHT,
 @P_TOT_WEIGHT,
 @P_CD_USERDEF1,
 @P_CD_USERDEF2,
 @P_NM_USERDEF1,
 @P_NM_USERDEF2,
 @P_DATE_USERDEF2,
 @P_NO_LOT,
 @P_DT_LIMIT_LOT,
 @P_ID_INSERT,
 @P_TXT_USERDEF1,
 @P_TXT_USERDEF2
   

    
 IF (@@ERROR <> 0 )                  
 BEGIN                    
       SELECT @ERRNO  = 100000,                    
                    @ERRMSG = 'CM_M100010'                    
       GOTO ERROR                    
 END  

 IF (@V_CD_EXC = '100' AND @V_CD_EXC_MENU = '100') 
 BEGIN  
	 EXEC UP_MM_REQ_PUSH_RCV_I @P_CD_COMPANY, @P_NO_RCV, @P_NO_LINE
 END
-- ��ǰ���ΰ�ȹ(���԰�) ���̺� �԰��Ƿ� ���� ���          
 --IF Len(Ltrim(RTrim(isNull(@P_NO_REV,'')))) > 0           
 --BEGIN          
 --    UPDATE PU_REV          
 --       SET NO_REQ     = @P_NO_RCV,          
 --           NO_REQLINE = @P_NO_LINE--,          
 --           QT_REQ_MM  = @P_QT_REQ_MM           
 --     WHERE CD_COMPANY = @P_CD_COMPANY           
 --       AND NO_REV = @P_NO_REV          
 --       AND NO_REVLINE = @P_NO_REVLINE          
 --END           
          
          
/*  PU_RCVL.TI_PU_RCVL Ʈ���ŷ� �ű�               
--SP_HELP QU_QC_REQ_TEMP NO_REQ NO_LINE_REQ FG_INSP CD_ITEM QT_REQ QT_ORG QT_INSP TP_PROC NO_SRC CD_SRC NM_SRC CD_GRP NO_EMP DT_REQ QT_GOOD QT_BAD QT_ORG_ORDER QT_GOOD_ORDER QT_BAD_ORDER QT_GOOD_IO QT_BAD_IO QT_GOOD_IO_O QT_BAD_IO_O NO_IO NO_IOLINE      


   
   
      
      
       
       
           
           
 IF( @P_YN_INSP = 'Y')                  
 BEGIN                  
  SELECT @P_NO_EMP = NO_EMP FROM PU_RCVH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_RCV = @P_NO_RCV                  
                          
  INSERT INTO QU_QC_REQ_TEMP (CD_COMPANY,CD_PLANT,NO_REQ, NO_LINE_REQ,FG_INSP,CD_ITEM,QT_ORG,TP_PROC,CD_SRC,CD_GRP,NO_EMP,DT_REQ,QT_ORG_ORDER)                  
  VALUES (@P_CD_COMPANY,@P_CD_PLANT,@P_NO_RCV,@P_NO_LINE,'IQC',@P_CD_ITEM,@P_QT_REQ,'P',@P_CD_PARTNER,@P_CD_PURGRP,@P_NO_EMP,@P_DT_REQ,@P_QT_REQ_MM)                  
 END                  
 -- ���� ���̺� �ܷ�����                  
 IF( @P_NO_PO <> '' AND @P_NO_POLINE > 0)                  
 BEGIN                  
  EXEC UP_PU_POL_FROM_REQ_UPDATE @P_CD_COMPANY, @P_NO_PO, @P_NO_POLINE, @P_QT_REQ,@P_QT_REQ_MM, @P_AM_EX,@P_AM            
  IF (@@ERROR <> 0)                    
  BEGIN                                              
 GOTO ERROR            
  END                   
 END                  
 IF( @P_FG_TRANS ='003')                  
 BEGIN                  
  -- LC ���̺� �ܷ�����                  
  IF( @P_NO_LC <> '' AND @P_NO_LCLINE > 0)                  
  BEGIN              
   EXEC UP_TR_LC_IML_FROM_REQ_UPDATE @P_CD_COMPANY, @P_NO_LC, @P_NO_LCLINE, @P_QT_REQ,@P_QT_REQ_MM, @P_AM_EX,@P_AM                  
   IF (@@ERROR <> 0 )                   
   BEGIN                    
         GOTO ERROR                    
   END                   
  END                  
 END                  
 ELSE IF( @P_FG_TRANS ='004' OR @P_FG_TRANS ='005' OR @P_FG_TRANS ='006' )                  
 BEGIN                  
  -- ��� ���̺� �ܷ�����                  
  IF( @P_NO_TO <> '' AND @P_NO_TO_LINE > 0)                  
  BEGIN                  
   EXEC UP_TR_TO_IML_FROM_REQ_UPDATE @P_CD_COMPANY, @P_NO_TO, @P_NO_TO_LINE, @P_QT_REQ,@P_QT_REQ_MM, @P_AM_EX,@P_AM                  
   IF (@@ERROR <> 0 )                   
   BEGIN                    
         GOTO ERROR            
   END          
  END                  
 END             
*/                 
RETURN                  
ERROR:                       
IF @ERRNO = 0 RETURN            
ELSE             
   RAISERROR ( @ERRMSG,16,1 )
 --RAISERROR @ERRNO @ERRMSG
GO


