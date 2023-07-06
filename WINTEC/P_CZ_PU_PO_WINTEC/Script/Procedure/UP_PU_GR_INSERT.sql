USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_GR_INSERT]    Script Date: 2022-03-24 ���� 5:52:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_PU_GR_INSERT]               
(              
	 @P_YN_RETURN		NCHAR(1) ,  -- ��ǰ����              
	 @P_NO_IO			NVARCHAR(20),  -- ���ҹ�ȣ              
	 @P_NO_IOLINE		NUMERIC(5) ,  -- ���� �׹�              
	 @P_CD_COMPANY		NVARCHAR(7)  ,  -- ȸ��              
	 @P_CD_PLANT		NVARCHAR(7)  ,  -- ����              
	 @P_CD_SL			NVARCHAR(7)  ,  -- â��              
	 @P_DT_IO			NCHAR(8)  ,  -- ������              
	 @P_NO_ISURCV		NVARCHAR(20)  ,  -- �Ƿڹ�ȣ              
	 @P_NO_ISURCVLINE	NUMERIC(5) ,  -- �Ƿڶ���              
	 @P_NO_PSO_MGMT		NVARCHAR(20)  ,  -- ���� ��ȣ              
	 @P_NO_PSOLINE_MGMT NUMERIC(5) ,  -- ���ֶ���              
	 @P_FG_PS			NCHAR(1)  ,  -- �������               
	 @P_FG_TPIO			NCHAR(3)  ,  -- �԰�����              
	 @P_FG_IO			NCHAR(3)  ,  -- ��������              
	 @P_CD_QTIOTP		NCHAR(3)  ,  -- ��������              
	 @P_FG_TRANS		NCHAR(3)  ,  -- �ŷ�����              
	 @P_FG_TAX			NCHAR(3)  ,  -- ��������              
	 @P_CD_PARTNER		NVARCHAR(20)  ,  -- �ŷ�ó              
	 @P_CD_ITEM			NVARCHAR(50)  ,  -- ǰ��              
	 @P_QT_GOOD_INV		NUMERIC(17,4) ,  -- ��ǰ����              
	 @P_QT_REJECT_INV	NUMERIC(17,4) ,  -- ��ǰ����              
	 @P_CD_EXCH			NVARCHAR(3)  ,  -- ȯ��              
	 @P_RT_EXCH			NUMERIC(11,4) ,  -- ȯ��              
	 @P_UM_EX			NUMERIC(19,6) ,  -- �ܰ�              
	 @P_UM				NUMERIC(19,6) ,  -- ��ȭ�ܰ�            
	 @AM_EX				NUMERIC(19,6) ,  -- �ݾ�            
	 @AM				NUMERIC(17,4) ,  -- ��ȭ�ݾ�              
	 @P_VAT				NUMERIC(17,4) ,  -- �ΰ���              
	 @P_P_FG_TAXP		NCHAR(3),  --               
	 @P_YN_AM			NCHAR(1),  -- ����ȯ����              
	 @P_CD_PJT			NVARCHAR(20)  ,  -- ������Ʈ              
	 @P_NO_LC			NVARCHAR(20)  ,  -- LC��ȣ              
	 @P_P_NO_LCLINE		NUMERIC(5) ,  -- LC����              
	 @P_NO_EMP			NVARCHAR(10)  ,  -- �����              
	 @P_CD_GROUP		NVARCHAR(7)  ,  --  ���ű׷�              
	 @P_CD_UNIT_MM		NVARCHAR(3)  ,  -- ���ִ���              
	 @P_QT_GOOD_MM		NUMERIC(17,4) ,  -- ���� ����              
	 @P_QT_REJECT_MM	NUMERIC(17,4) ,  -- ���� ����              
	 @P_UM_EX_PSO		NUMERIC(19,6),  -- ���� �ܰ�              
	 @P_YN_INSP			NCHAR(1),              
	 @P_YN_PURCHASE     NCHAR(1) , --��������         
	 @P_DC_RMK			NVARCHAR(200)	= NULL,  
	 @P_CD_WH			NVARCHAR(7)		= NULL,    -- W/H 
	 @P_SEQ_PROJECT		NUMERIC(5,0)	= NULL,
	 @P_NO_WBS			NVARCHAR(20)	= NULL,
	 @P_NO_CBS			NVARCHAR(20)	= NULL ,    
	 @P_TP_UM_TAX		NVARCHAR(3)		= '002', --�ΰ������Կ��� 002:����, 001:����
	 @P_DC_RMK2			NVARCHAR(200)	= NULL,
	 @P_UM_WEIGHT	    NUMERIC(17,4)	= 0,
	 @P_TOT_WEIGHT      NUMERIC(17,4)	= 0,
	 @P_CD_USERDEF1		NVARCHAR(100)	= NULL,
	 @P_CD_USERDEF2		NVARCHAR(100)	= NULL,
	 @P_DATE_USERDEF1	NVARCHAR(8)		= NULL,
	 @P_NM_USERDEF1		NVARCHAR(100)	= NULL,
	 @P_NM_USERDEF2		NVARCHAR(100)	= NULL,
	 @P_GI_PARTNER		NVARCHAR(20)    = NULL,
	 @P_CD_USERDEF3     NVARCHAR(100)	= NULL,
	 @P_CD_USERDEF4     NVARCHAR(100)	= NULL,
	 @P_CD_USERDEF5     NVARCHAR(100)	= NULL,
	 @P_NM_USERDEF3     NVARCHAR(50)	= NULL,
	 @P_NM_USERDEF4     NVARCHAR(50)	= NULL,
	 @P_CD_USERDEF6		NVARCHAR(100)	= NULL,
	 @P_TXT_USERDEF1    NVARCHAR(100)	= NULL,
	 @P_DC_RMK2_IO		NVARCHAR(200)	= NULL,
	 @P_NUM_USERDEF1	NUMERIC(17,4) = 0,
	 @P_NUM_USERDEF2	NUMERIC(17,4) = 0
)              
              
AS              
BEGIN                
 DECLARE              
 @ERRNO				INT,              
 @ERRMSG			VARCHAR(255),              
-- @AM_EX   NUMERIC(17,4) ,              
-- @AM    NUMERIC(17,4) ,              
 @QT_IO				NUMERIC(17,4),              
 @QT_UNIT_MM		NUMERIC(17,4),              
 @YN_EXIV			NVARCHAR(3),
 @V_INSPECT_CHECK	NVARCHAR(3) ,
 @V_SERVER_KEY		NVARCHAR(25),
 @V_FG_TPIO			NCHAR(3)              
 --@QT_REQ  NUMERIC(17, 4),               
 --@QT_REQ_PRL  NUMERIC(17, 4),               
 --@QT_GR   NUMERIC(17, 4),               
 --@QT_PO   NUMERIC(17, 4),               
 --@QT_RCV  NUMERIC(17, 4),               
 --@QT_RCV_PRL  NUMERIC(17, 4),               
 --@NO_PR   VARCHAR(20),               
 --@NO_PRLINE  NUMERIC(17, 4),               
 --@QT_RCV_SUM   NUMERIC(17, 4),               
 --@QT_PO_MM_CLS NUMERIC(17, 4)              
              
              
              
 SET @QT_IO = @P_QT_GOOD_INV + @P_QT_REJECT_INV              
 SET @QT_UNIT_MM = @P_QT_GOOD_MM + @P_QT_REJECT_MM              
-- SET @AM_EX = @QT_IO*@P_UM_EX              
-- SET @AM = @QT_IO*@P_UM               
 SET @ERRNO = 0            
 
   
 IF EXISTS(              
   SELECT 1                
   FROM PU_IVH HI,PU_IVL LI               
   WHERE HI.NO_IV = LI.NO_IV AND HI.CD_COMPANY = LI.CD_COMPANY              
    AND HI.CD_COMPANY =@P_CD_COMPANY                                                  
    AND LI.NO_PO = @P_NO_PSO_MGMT                   
    AND LI.NO_POLINE = @P_NO_PSOLINE_MGMT                
    AND HI.YN_EXPIV = 'Y'              
    AND HI.YN_PURSUB = 'N'   )              
  SET @YN_EXIV = 'Y'          
 ELSE           
  SET @YN_EXIV ='N'              

SELECT @V_SERVER_KEY = MAX(SERVER_KEY)
  FROM CM_SERVER_CONFIG
 WHERE YN_UPGRADE = 'Y'    
 
IF(@V_SERVER_KEY LIKE 'MEERE%' AND @P_CD_COMPANY = '1000' AND @P_CD_PLANT = '1000')
  BEGIN  
	SET @V_FG_TPIO = NEOE.FN_Z_MEERE_GET_FG_PURCHASE('1000', '1000', @P_NO_PSO_MGMT, @P_CD_PJT, @P_DT_IO)
	IF(ISNULL(@V_FG_TPIO, '') <> '')	
	BEGIN	
		SET @P_FG_TPIO = @V_FG_TPIO
	END	
  END	    
      
         
 -- MM_QTIO  ���� ���ν���              
 EXEC  UP_PU_MM_QTIO_INSERT               
	@P_YN_RETURN , @P_NO_IO,@P_NO_IOLINE ,@P_CD_COMPANY ,@P_CD_PLANT , @P_CD_SL  ,''  ,@YN_EXIV,@P_DT_IO ,@P_NO_ISURCV   ,              
	@P_NO_ISURCVLINE,@P_NO_PSO_MGMT ,@P_NO_PSOLINE_MGMT,@P_FG_PS,@P_YN_PURCHASE,@P_FG_TPIO,@P_FG_IO  ,@P_CD_QTIOTP,@P_FG_TRANS,              
	@P_FG_TAX,@P_CD_PARTNER,@P_CD_ITEM ,@QT_IO ,0 ,0 ,0,@P_QT_REJECT_INV,@P_QT_GOOD_INV,              
	@P_CD_EXCH,@P_RT_EXCH,@P_UM_EX ,@AM_EX ,@P_UM ,@AM ,0,0,@P_VAT ,0 ,@P_P_FG_TAXP ,@P_UM ,NULL  ,              
	@P_YN_AM  ,@P_CD_PJT,0, 0,0,@P_NO_LC ,@P_P_NO_LCLINE,0,0,@P_GI_PARTNER,@P_NO_EMP,@P_CD_GROUP,NULL ,NULL,NULL ,              
	NULL ,NULL  ,NULL ,@P_YN_INSP , NULL ,@P_CD_UNIT_MM  ,@QT_UNIT_MM  ,@P_UM_EX_PSO ,0,0 ,NULL ,NULL,NULL , NULL,      
	NULL,  @P_DC_RMK ,NULL, @P_CD_WH, @P_YN_INSP, NULL, NULL, @P_NO_CBS, @P_NO_WBS,	@P_SEQ_PROJECT,
	@P_DC_RMK2 ,@P_CD_USERDEF1,@P_CD_USERDEF2,@P_CD_USERDEF3,@P_CD_USERDEF4,@P_CD_USERDEF5 ,@P_TP_UM_TAX,
	0,--NO_LINE_PJTBOM
	'', --NO_AS
	0,  --NO_ASLINE
	'', --CD_CENTER
	'', --CD_ITEM_REF
	'', --NO_TRACK
	0,  --NO_TRACKLINE
	@P_UM_WEIGHT,
	@P_TOT_WEIGHT,
	0,
	@P_NM_USERDEF1,
	@P_NM_USERDEF2,
	@P_DATE_USERDEF1,
	@P_NUM_USERDEF1,
	NULL,   
	NULL   ,    
	@P_NM_USERDEF3,  
	@P_NM_USERDEF4,
	@P_CD_USERDEF6,
	@P_TXT_USERDEF1,
	@P_DC_RMK2_IO,
	@P_NUM_USERDEF2
              
 IF (@@ERROR <> 0 )              
 BEGIN                
       SELECT @ERRNO  = 100000,                
                    @ERRMSG = 'CM_M100010'                
       GOTO ERROR                
 END                 
 
SELECT	@V_INSPECT_CHECK = CD_EXC 
FROM	MA_EXC A 
WHERE	CD_COMPANY = @P_CD_COMPANY
AND		CD_MODULE = 'PU'  
AND		EXC_TITLE = '�����԰���_�˻�'

IF @V_INSPECT_CHECK = '200' AND @P_YN_INSP = 'Y'
BEGIN
	EXEC NEOE.UP_MM_QC_AFTER_PROCESS_I @P_CD_COMPANY, @P_NO_IO, @P_NO_IOLINE 
END 
            
/* MM_QTIO.TI_MM_QTIO Ʈ���ŷ� �ű�             
 -- �Ƿ� ���̺� �ܷ�����              
 IF( ISNULL(@P_NO_ISURCVLINE,0) >0 AND  ISNULL(@P_NO_ISURCV,'') <> '')              
 BEGIN              
              
  EXEC UP_PU_RCVL_FROM_QTIO_UPDATE @P_CD_COMPANY , @P_NO_ISURCV ,@P_NO_ISURCVLINE,@P_DT_IO,@QT_IO ,@QT_UNIT_MM,@P_VAT              
  IF (@@ERROR <> 0 )              
  BEGIN                
--        SELECT @ERRNO  = 100000,                
--                     @ERRMSG = 'CM_M100010'                
        GOTO ERROR                
  END               
 END               
            
              
 --���� ���̺� �ܷ�����              
 IF( ISNULL(@P_NO_PSOLINE_MGMT,0) >0 AND ISNULL(@P_NO_PSO_MGMT,'') <> '')              
 BEGIN              
  EXEC UP_PU_POL_FROM_QTIO_UPDATE @P_CD_COMPANY ,@P_NO_PSO_MGMT ,@P_NO_PSOLINE_MGMT ,@QT_IO,@QT_UNIT_MM,@P_FG_IO              
  IF (@@ERROR <> 0 )              
  BEGIN                
--        SELECT @ERRNO  = 100000,                
--                     @ERRMSG = 'CM_M100010'                
        GOTO ERROR                
  END                 
 END                  
*/ 
/** 2012.12.11  �ʿ���� �Ǵܿ� ������ (�ݵ�� ��� /�Ź̶�,������,�輺ȣ
     
 IF( @P_YN_INSP = 'Y' AND @V_INSPECT_CHECK <> '200')              
 BEGIN              
  UPDATE QU_QC_REQ_TEMP              
  SET NO_IO = @P_NO_IO, NO_IOLINE = @P_NO_IOLINE, QT_GOOD_IO =@P_QT_GOOD_INV , QT_BAD_IO =@P_QT_REJECT_INV ,               
    QT_GOOD_IO_O =@P_QT_GOOD_MM , QT_BAD_IO_O = @P_QT_REJECT_MM              
  WHERE CD_COMPANY = @P_CD_COMPANY               
   AND NO_REQ = @P_NO_ISURCV               
   AND NO_LINE_REQ = @P_NO_ISURCVLINE               
   AND FG_INSP = 'IQC'              
  IF (@@ERROR <> 0 )              
  BEGIN                
--        SELECT @ERRNO  = 100000,                
--                     @ERRMSG = 'CM_M100010'                
        GOTO ERROR                
  END                
 END              
***/
            
/* MM_QTIO.TI_MM_QTIO Ʈ���� �ִ� UP_PU_POL_FROM_QTIO_UPDATE ���ν����� �̵�             
-------------------------------------------���� ���º��� ���� ����------------------------------------              
 --�԰�Ϸ�(�Ƿμ����� �԰������ ������)�� �԰��Ƿ� ���°��� 'C'�� ����.(�԰��Ƿ� ���°��� ��Ͻ� 'O', �԰�Ϸ�� 'C')              
              
 SELECT @QT_REQ = QT_REQ, @QT_GR = QT_GR              
 FROM PU_RCVL              
 WHERE CD_COMPANY = @P_CD_COMPANY              
  AND NO_RCV = @P_NO_ISURCV              
  AND NO_LINE = @P_NO_ISURCVLINE              
              
 SELECT @QT_REQ = ISNULL(@QT_REQ, 0), @QT_GR = ISNULL(@QT_GR, 0)              
              
 --NO_PO, NO_LINE, CD_COMPANY              
 SELECT @QT_PO = QT_PO, @QT_RCV = QT_RCV, @NO_PR = NO_PR, @NO_PRLINE = NO_PRLINE              
 FROM PU_POL              
 WHERE CD_COMPANY = @P_CD_COMPANY              
  AND NO_PO = @P_NO_PSO_MGMT              
  AND NO_LINE = @P_NO_PSOLINE_MGMT              
              
 SELECT @QT_PO = ISNULL(@QT_PO, 0), @QT_RCV = ISNULL(@QT_RCV, 0)              
              
 SELECT @QT_REQ_PRL = QT_PR              
 FROM PU_PRL              
 WHERE CD_COMPANY = @P_CD_COMPANY              
  AND NO_PR = @NO_PR              
  AND NO_PRLINE = @NO_PRLINE              
              
 SELECT @QT_RCV_SUM = SUM(ISNULL(QT_RCV, 0))              
 FROM PU_POL              
 WHERE CD_COMPANY = @P_CD_COMPANY              
  AND NO_PR = @NO_PR              
  AND NO_PRLINE = @NO_PRLINE          
              
 SELECT @QT_PO_MM_CLS = ISNULL(QT_PO_MM, 0)              
 FROM PU_POL              
 WHERE CD_COMPANY = @P_CD_COMPANY              
  AND NO_PR = @NO_PR              
  AND NO_PRLINE = @NO_PRLINE              
  AND FG_POST = 'C'              
  AND ISNULL(QT_RCV, 0) = 0              
              
 SELECT @QT_REQ_PRL = ISNULL(@QT_REQ_PRL, 0), @QT_RCV_SUM = ISNULL(@QT_RCV_SUM, 0), @QT_PO_MM_CLS = ISNULL(@QT_PO_MM_CLS, 0)              
              
 IF (EXISTS(              
   SELECT 1              
   FROM PU_RCVL              
   WHERE CD_COMPANY = @P_CD_COMPANY              
    AND NO_RCV = @P_NO_ISURCV              
    AND NO_LINE = @P_NO_ISURCVLINE) )              
 BEGIN              
              
               
  IF (@QT_REQ = @QT_GR AND @QT_REQ <> 0 AND @QT_GR <> 0)              
  BEGIN              
   UPDATE PU_RCVL              
   SET FG_POST = 'C'              
   WHERE CD_COMPANY = @P_CD_COMPANY              
    AND NO_RCV = @P_NO_ISURCV              
    AND NO_LINE = @P_NO_ISURCVLINE              
  END              
 END              
              
 --���Ź��� ���¸� �԰����(���ּ���(QT_PO) <> �԰����(QT_RCV)) ���� 'S' �Ǵ� �԰�Ϸ�(���ּ���(QT_PO) = �԰����(QT_RCV)) ���� 'C'�� ����              
 IF (EXISTS(              
   SELECT 1              
   FROM PU_POL              
   WHERE CD_COMPANY = @P_CD_COMPANY              
    AND NO_PO = @P_NO_PSO_MGMT              
    AND NO_LINE = @P_NO_PSOLINE_MGMT ) )              
 BEGIN              
  IF (@QT_PO = @QT_RCV AND @QT_PO <> 0 AND @QT_RCV <> 0)              
  BEGIN              
   UPDATE PU_POL              
   SET FG_POST = 'C'              
   WHERE CD_COMPANY = @P_CD_COMPANY              
    AND NO_PO = @P_NO_PSO_MGMT              
    AND NO_LINE = @P_NO_PSOLINE_MGMT              
  END              
  ELSE-- IF (@QT_PO > @QT_RCV AND @QT_PO <> 0 AND @QT_RCV <> 0)              
  BEGIN              
   UPDATE PU_POL              
   SET FG_POST = 'S'              
   WHERE CD_COMPANY = @P_CD_COMPANY              
    AND NO_PO = @P_NO_PSO_MGMT              
    AND NO_LINE = @P_NO_PSOLINE_MGMT              
  END              
 END              
              
 --�԰�Ϸ��(���ſ�û������ �԰������ ������) ���ſ�û�� �������¸� �԰�Ϸ�('C')�� ����              
  --�̶� ���ָ������� �ִ��� üũ�ؾ���.(�� ������ ���Ź��� ���º��� ���� ���ſ�û �������¸� �����ؾ���)              
 IF (EXISTS(              
   SELECT 1              
   FROM PU_PRL              
   WHERE CD_COMPANY = @P_CD_COMPANY              
    AND NO_PR = @NO_PR              
    AND NO_PRLINE = @NO_PRLINE ) AND ISNULL(@NO_PR, '') <> '')              
 BEGIN              
  IF (@QT_PO = @QT_RCV_SUM + @QT_PO_MM_CLS AND @QT_REQ_PRL <> 0 )              
  BEGIN              
   UPDATE PU_PRL              
   SET FG_POST = 'C'              
   WHERE CD_COMPANY = @P_CD_COMPANY              
    AND NO_PR = @NO_PR              
    AND NO_PRLINE = @NO_PRLINE              
  END              
 END              
              
              
               
              
-------------------------------------------���� ���º��� ���� ��------------------------------------              
*/              
RETURN              
ERROR:                
 IF @ERRNO = 0 RETURN            
 ELSE             
  RAISERROR (@ERRMSG, 18,1)             
END
GO


