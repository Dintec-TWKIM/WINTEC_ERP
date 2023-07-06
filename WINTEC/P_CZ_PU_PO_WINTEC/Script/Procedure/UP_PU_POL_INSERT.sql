USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_POL_INSERT]    Script Date: 2022-03-24 ���� 3:43:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--2010.02.08 ���������� ���� �����ʵ� �߰�-SMR(����/������)        
--2010.02.09 no_po =''�ΰ� ������ϰ� �����߰�      
ALTER PROC [NEOE].[UP_PU_POL_INSERT]      
(      
    @NO_PO				NVARCHAR(20),  -- ���ֹ�ȣ         
    @NO_LINE			NUMERIC(5),  -- ���ֶ���         
    @CD_COMPANY			NVARCHAR(7),  -- ȸ��         
    @CD_PLANT			NVARCHAR(7),  -- ����         
    @NO_CONTRACT		NVARCHAR(20),  -- ����ȣ         
    @NO_CTLINE			NUMERIC(3),  -- ������  
    @NO_PR				NVARCHAR(20),  -- ��û��ȣ         
    @NO_PRLINE			NUMERIC(5),  -- ��û����         
    @FG_TRANS			NCHAR(3),  -- �ŷ�����         
    @CD_ITEM			NVARCHAR(50),  -- ǰ��         
    @CD_UNIT_MM			NVARCHAR(3),  -- ���ִ���         
    @FG_RCV				NCHAR(3),  -- �Ƿڱ���         
    @FG_PURCHASE		NCHAR(3),  -- ���ű���         
    @DT_LIMIT			NCHAR(8),  -- ������         
    @QT_PO_MM			NUMERIC(17,4),  -- ���ּ���         
    @QT_PO				NUMERIC(17,4),  -- ����������         
    @QT_REQ				NUMERIC(17,4),  -- �Ƿڼ���         
    @QT_RCV				NUMERIC(17,4),  -- �԰����         
    @FG_TAX				NVARCHAR(3),  -- ��������         
    @UM_EX_PO			NUMERIC(19,6),  -- ���ִܰ�          
    @UM_EX				NUMERIC(19,6),  -- �������ܰ�         
    @AM_EX				NUMERIC(19,6),  -- �ݾ�         
    @UM					NUMERIC(19,6),  -- ��ȭ�ܰ�         
    @AM					NUMERIC(17,4),  -- ��ȭ�ݾ�         
    @VAT				NUMERIC(17,4),  -- �ΰ���         
    @CD_SL				NVARCHAR(7),  -- â��         
    @FG_POST			NCHAR(3),  --           
    @FG_POCON			NCHAR(3),  --          
    @YN_AUTORCV			NCHAR(1),  --          
    @YN_RCV				NCHAR(1),          
    @YN_RETURN			NCHAR(1),          
    @YN_ORDER			NCHAR(1),          
    @YN_SUBCON			NCHAR(1),          
    @YN_IMPORT			NCHAR(1),          
    @RT_PO				NUMERIC(15,4),          
    @YN_REQ				NCHAR(1),  --           
    @CD_PJT				NVARCHAR(20),  -- ������Ʈ         
    @NO_APP				NVARCHAR(20),  -- ǰ�ǹ�ȣ         
    @NO_APPLINE			NUMERIC(5),  -- ǰ�Ƕ���         
    @DC1				NVARCHAR(200),  -- ���      
    @DC2				NVARCHAR(200),   -- ���      
    @SEQ_PROJECT		NUMERIC(5,0),       
    @UMVAT_PO			NUMERIC(17, 4) = 0,       
    @AMVAT_PO			NUMERIC(17, 4) = 0,      
    @CD_CC				NVARCHAR(12) = NULL,      
    @CD_BUDGET			NVARCHAR(20) = NULL,      
    @CD_BGACCT			NVARCHAR(10) = NULL,      
    @NO_SO				NVARCHAR(20) = NULL,      
    @NO_SOLINE			NUMERIC(5,0) = 0,  
    @P_GI_PARTNER		NVARCHAR(20) = NULL ,  
    @DC3				NVARCHAR(500) = NULL, -- 2010/12/30 ������ DC3(���3) �÷� �߰�  
    @P_NO_VMI			NVARCHAR(20) = NULL,  
    @P_SEQ_VMI			NUMERIC(5,0) = 0,  
    @P_NO_IO_MGMT		NVARCHAR(20) = NULL,  
    @P_NO_IOLINE_MGMT	NUMERIC(5) = 0,  
    @P_DT_PLAN			NVARCHAR(8) = NULL,   -- 2011.04.01 : ����ö �߰�  
    @P_DC4				NVARCHAR(500) = NULL,  -- 2011.04.01 : ����ö �߰�
    @P_UM_EX_AR			NUMERIC(15,4) = 0, -- 2011.08.17 : Ȳ���� �߰� (�����ܰ�)
    @P_NO_WBS			NVARCHAR(20) = NULL,  
    @P_NO_CBS			NVARCHAR(20) = NULL,
    @P_CD_USERDEF1		NVARCHAR(40) = NULL,
    @P_CD_USERDEF2		NVARCHAR(40) = NULL,
    @P_NM_USERDEF1		NVARCHAR(100) = NULL,
    @P_NM_USERDEF2		NVARCHAR(100) = NULL,
    @P_NO_PREIV			NVARCHAR(20) = NULL,
    @P_NO_PREIVLINE		NUMERIC(5,0) =0,
    @P_TP_UM_TAX		NVARCHAR(3) = '002',  --�ΰ������Կ��� 002:����, 001:����
    @P_DT_EXDATE		NVARCHAR(8) = NULL,
    @P_CD_ITEM_ORIGIN	NVARCHAR(20) = NULL,
    @P_AM_EX_TRANS		NUMERIC(17,4) = 0,
    @P_AM_TRANS			NUMERIC(17,4) = 0,
    @P_NO_LINE_PJTBOM	NUMERIC(5,0) = 0,
    @P_FG_PACKING		NVARCHAR(4) = NULL,
    @P_NUM_USERDEF	    NUMERIC(17,4) = 0,
    @P_AM_REBATE_EX	    NUMERIC(17,4) = 0,
    @P_AM_REBATE	    NUMERIC(17,4) = 0,
    @P_UM_REBATE	    NUMERIC(15,4) = 0,
    @P_ID_INSERT		NVARCHAR(15) = NULL,
    @P_DATE_USERDEF1	NCHAR(8) = NULL,
	@P_DATE_USERDEF2    NCHAR(8) = NULL,
    @P_TXT_USERDEF1		NVARCHAR(100) = NULL,
	@P_TXT_USERDEF2		NVARCHAR(100) = NULL,
    @P_CDSL_USERDEF1    NVARCHAR(7) = NULL,
    @P_NUM_USERDEF2	    NUMERIC(17,4) = 0,
	@P_CLS_L			NVARCHAR(4) = NULL,
	@P_CLS_M			NVARCHAR(4) = NULL,
	@P_CLS_S			NVARCHAR(4) = NULL,
	@P_GRP_ITEM			NVARCHAR(20) = NULL,
	@P_NUM_STND_ITEM_1  NUMERIC(17,4) = 0,
	@P_NUM_STND_ITEM_2  NUMERIC(17,4) = 0,
	@P_NUM_STND_ITEM_3  NUMERIC(17,4) = 0,
	@P_NUM_STND_ITEM_4  NUMERIC(17,4) = 0,
	@P_NUM_STND_ITEM_5  NUMERIC(17,4) = 0,
	@P_UM_WEIGHT		NUMERIC(17,4) = 0,
	@P_WEIGHT			NUMERIC(17,4) = 0,
	@P_TOT_WEIGHT		NUMERIC(17,4) = 0,
	@P_STND_ITEM		NVARCHAR(200) = NULL,
	@P_NO_RELATION      NVARCHAR(20) = '',
	@P_SEQ_RELATION     NUMERIC(5,0) = 0,
	@P_NUM_USERDEF3     NUMERIC(17,4) = 0,
	@P_NUM_USERDEF4     NUMERIC(17,4) = 0,
	@P_NUM_USERDEF5     NUMERIC(17,4) = 0,
	@P_NO_QUO			NVARCHAR(20) = '',
	@P_NO_QUOLINE		NUMERIC(5,0) = 0,
	@P_CD_BIZPLAN		NVARCHAR(20) = NULL,
	@P_CD_USERDEF3		NVARCHAR(20) = NULL,
	@P_CD_USERDEF4		NVARCHAR(20) = NULL,
	@P_NM_USERDEF3		NVARCHAR(200) = NULL,
	@P_NM_USERDEF4		NVARCHAR(100) = NULL,
	@P_YN_BUDGET		NVARCHAR(1) = 'N',
	@P_BUDGET_PASS		NVARCHAR(1) = 'N',
	@P_NO_IO			NVARCHAR(20) = NULL,
	@P_UNIT_IM			NVARCHAR(10) = '',
	@P_NM_ITEM		    NVARCHAR(200) = '',
	@P_PAGE_ID			NVARCHAR(100) = '',
	@P_CD_ACCT			NVARCHAR(20) = '',
	@P_NM_USERDEF5	    NVARCHAR(50) = '',
	@P_STND_DETAIL_ITEM	NVARCHAR(200) = '',
	@P_MAT_ITEM		    NVARCHAR(100) = '',
	@P_DC50_PO			NVARCHAR(100) = NULL
)         
AS      
DECLARE @ERRNO				INT,          
		@ERRMSG				NVARCHAR(255),
		@P_DTS_INSERT		NVARCHAR(15),
		@P_AM_FSIZE			NUMERIC(3,0),   -- �Ҽ���        
		@P_AM_UPDOWN		NVARCHAR(3),   -- �ø�����        
		@P_AM_FORMAT		NVARCHAR(50),   -- ����(#,###,###,###.00)    
		@V_SERVER_KEY		NVARCHAR(25),
		@V_STND				CHAR(1),
		@V_NEW_ID			NVARCHAR(36),
		@V_PJT_AM			NUMERIC(19,6),     
		@V_PO_AM			NUMERIC(19,6),
		@V_FG_PURCHASE      NCHAR(3)
    	
SET @ERRNO = 0        
 
SELECT @P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')  
EXEC UP_SF_GETUNIT_AM @CD_COMPANY,'PU', 'I',  @P_AM_FSIZE OUTPUT,  @P_AM_UPDOWN OUTPUT,  @P_AM_FORMAT OUTPUT   		
    
SELECT @V_SERVER_KEY = SERVER_KEY
  FROM CM_SERVER_CONFIG
 WHERE YN_UPGRADE = 'Y'    
 
 
IF(( SELECT ISNULL(YN_PMS,'N') FROM MA_ENV WHERE CD_COMPANY = @CD_COMPANY) = 'Y')
BEGIN
	SELECT @V_NEW_ID = NEWID()
END            
          
    
--(ũ���������� �ӽ������� �߰��Ѵ�.)      
-- 2010.02.09 �߰�      
IF LEN(ISNULL(@NO_PO,'')) < 3       
BEGIN      
   IF (@@ERROR <> 0) BEGIN SELECT @ERRNO = 100000, @ERRMSG = '�۾�������������ó���������߽��ϴ�. [NO_PO�� ������ �� �� �����ϴ�.]-'+str(@NO_LINE) GOTO ERROR END      
END   
  
-- 2010.02.19 �߰�    
IF @YN_ORDER = 'Y' AND ISNULL(@FG_POST,'0') = 'O'    
BEGIN      
   IF (@@ERROR <> 0) BEGIN SELECT @ERRNO = 100000, @ERRMSG = '�۾�������������ó���������߽��ϴ�. [���ֻ��°��� Ȯ���ϼ���]-'+ @FG_POST GOTO ERROR END      
END   

-- 2011-06-24 ��������п��� Ʋ������ ���ͼ� ��ġ�� ����
IF @QT_PO <> @QT_PO_MM * @RT_PO
BEGIN      
   IF (@@ERROR <> 0) BEGIN SELECT @ERRNO = 100000, @ERRMSG = 'ǰ�� : '+ @CD_ITEM + ' �� �������� (���ּ��� * ȯ�����) �� ��ġ���� �ʽ��ϴ�. �ٽ�Ȯ���Ͽ��ֽñ� �ٶ��ϴ�. ' GOTO ERROR END      
END   

 
-----------------------------------------------------  
  IF (@V_SERVER_KEY =  'KORAVL')
  BEGIN
	SELECT @V_PJT_AM = AM 
	  FROM SA_PROJECTL_D
	 WHERE CD_COMPANY = @CD_COMPANY
	   AND NO_PROJECT = @CD_PJT
	   AND SEQ_PROJECT = @SEQ_PROJECT
	   AND NO_LINE = @P_NO_LINE_PJTBOM
	   
	SELECT @V_PO_AM = ISNULL(SUM(AM),0)
	  FROM PU_POL
	 WHERE CD_COMPANY = @CD_COMPANY
	   AND CD_PJT = @CD_PJT
	   AND SEQ_PROJECT = @SEQ_PROJECT
	   AND NO_LINE_PJTBOM = @P_NO_LINE_PJTBOM
	   
	IF(@V_PJT_AM < @V_PO_AM + @AM )
	BEGIN
		SELECT @ERRMSG = '���ֱݾ� (' + CONVERT(VARCHAR(30),(@V_PO_AM + @AM) ) + ')�� ������Ʈ �ݾ�('+CONVERT(VARCHAR(30),@V_PJT_AM) +')�� �ʰ��� �� �����ϴ�.' +  CHAR(13) +       
						 'ǰ���ڵ� :' + @CD_ITEM +
						 '     ������Ʈ��ȣ :' + @CD_PJT + '     UNIT�׹�:' + CONVERT(VARCHAR(5),@SEQ_PROJECT)+ '     BOM�׹�:' + CONVERT(VARCHAR(5),@P_NO_LINE_PJTBOM)        
		GOTO ERROR       
	END
  END
  
--����SM�� ��� �԰��� ������� �Էµ� CD_ITEM�� ���� ��� MA_PITEM�� INSERTó��    
  IF (@V_SERVER_KEY =  'SINJINSM')
  BEGIN     
    
		SELECT @V_STND = '1' 
		  FROM MA_PITEM P
		 WHERE P.CD_COMPANY = @CD_COMPANY
		   AND P.CD_PLANT = @CD_PLANT
		   AND P.CD_ITEM = @CD_ITEM
		
		--����ǰ���� �������� ���� ���    
		IF ISNULL(@V_STND,'') != '1'
		BEGIN  
			EXEC UP_PU_STND_PITEM_INSERT
			@CD_COMPANY,		@CD_PLANT,			@CD_ITEM,			'001',				@P_GRP_ITEM,
			@P_CLS_L,			@P_CLS_M,			@P_CLS_S,			'EA',				'EA',
			1,					1,					@P_WEIGHT,			'KG',				@P_UM_WEIGHT,		
			@P_ID_INSERT,		NULL,				NULL,				NULL,				NULL,
			NULL,				@P_NUM_STND_ITEM_1,	@P_NUM_STND_ITEM_2,	@P_NUM_STND_ITEM_3,	@P_NUM_STND_ITEM_4,
			@P_NUM_STND_ITEM_5,	@P_STND_ITEM  
		END
  END
  
  IF(@P_PAGE_ID = 'P_PU_Z_JONGHAP_PO_REG2' AND ISNULL(@CD_ITEM,'') = '')
  BEGIN
  
  	DECLARE @V_CD_ITME NVARCHAR(20),
  			@V_DT_SEQ  NVARCHAR(8),
  			@V_COL_LEN INT
  	  	  
	SELECT @V_CD_ITME = P.CD_ITEM 
	  FROM MA_PITEM P
	 WHERE P.CD_COMPANY = @CD_COMPANY
	   AND P.CD_PLANT = @CD_PLANT
	   AND P.CLS_L = @P_CLS_L
	   AND P.NUM_STND_ITEM_1 = @P_NUM_STND_ITEM_1
	   AND P.NUM_STND_ITEM_2 = @P_NUM_STND_ITEM_2
	   AND P.NUM_STND_ITEM_3 = @P_NUM_STND_ITEM_3
	   AND P.NUM_STND_ITEM_4 = @P_NUM_STND_ITEM_4
	   AND P.UNIT_PO = @CD_UNIT_MM
	   AND P.UNIT_IM = @P_UNIT_IM
	  
		  
	  IF ISNULL(@V_CD_ITME,'') = ''
	  BEGIN  
	
	    SET @V_COL_LEN = LEN(@P_CLS_L)
	    
		SELECT @V_DT_SEQ = SUBSTRING(MAX(CD_ITEM),@V_COL_LEN + 9 ,5)
		  FROM MA_PITEM
		 WHERE CD_COMPANY = @CD_COMPANY
		   AND CD_PLANT = @CD_PLANT
		   AND CD_ITEM LIKE @P_CLS_L + SUBSTRING(@P_DTS_INSERT,1,8) + '%'	   
		    
		 IF(ISNULL(@V_DT_SEQ,'') = '') 
		    SET @V_DT_SEQ = '00001'
		 ELSE
			SET @V_DT_SEQ = CONVERT(INT,@V_DT_SEQ) + 1 
			
			SET @V_COL_LEN = LEN(@V_DT_SEQ)
					    
			SET @CD_ITEM = @P_CLS_L + SUBSTRING(@P_DTS_INSERT,1,8) + REPLICATE('0',5 - @V_COL_LEN )+ @V_DT_SEQ 
		   
	        EXEC UP_PU_Z_JONGHAP_ITME_I @CD_COMPANY, @CD_PLANT, @CD_ITEM, @P_NM_ITEM,
										@P_CLS_L, @P_UNIT_IM, @CD_UNIT_MM, @RT_PO, @RT_PO,
										@P_ID_INSERT, @P_NUM_STND_ITEM_1, @P_NUM_STND_ITEM_2,
										@P_NUM_STND_ITEM_3, @P_NUM_STND_ITEM_4, @P_NUM_STND_ITEM_5,
										@P_STND_DETAIL_ITEM, @P_MAT_ITEM   
										
	  END
	  ELSE
	  BEGIN
		SET @CD_ITEM = @V_CD_ITME
		
		UPDATE MA_PITEM
		   SET  NM_ITEM = @P_NM_ITEM
		 WHERE CD_COMPANY = @CD_COMPANY
		   AND CD_PLANT = @CD_PLANT
		   AND CD_ITEM = @CD_ITEM
		   
	  END
		
  END
  	
  IF(@V_SERVER_KEY LIKE 'MEERE%' AND @CD_COMPANY = '1000' AND @CD_PLANT = '1000')
  BEGIN  
	SET @V_FG_PURCHASE = NEOE.FN_Z_MEERE_GET_FG_PURCHASE('1000', '1000', @NO_PO, @CD_PJT, '')
	IF(ISNULL(@V_FG_PURCHASE, '') <> '')	
	BEGIN	
		SET @FG_PURCHASE = @V_FG_PURCHASE
	END	
  END	    
           
INSERT INTO PU_POL          
(  
	NO_PO, NO_LINE,CD_COMPANY,CD_PLANT,NO_CONTRACT,NO_CTLINE,NO_PR,NO_PRLINE,FG_TRANS,CD_ITEM,          
	CD_UNIT_MM,FG_RCV,FG_PURCHASE,DT_LIMIT,QT_PO_MM,QT_PO,QT_REQ,QT_RCV,FG_TAX,UM_EX,AM_EX,AM,          
	VAT,CD_SL,FG_POST,FG_POCON,YN_AUTORCV,YN_RCV,YN_RETURN,YN_ORDER,YN_SUBCON,YN_IMPORT,UM,          
	RT_PO,YN_REQ,CD_PJT,UM_EX_PO,NO_APP,NO_APPLINE, DC1, DC2, SEQ_PROJECT, UMVAT_PO, AMVAT_PO,      
	CD_CC,      
	CD_BUDGET,      
	CD_BGACCT,      
	NO_SO,      
	NO_SOLINE,  
	GI_PARTNER,  
	DC3, -- 2010/12/30 ������ DC3(���3) �÷� �߰�  
	NO_IO_MGMT,  
	NO_IOLINE_MGMT,  
	DT_PLAN,  
	DC4,
	UM_EX_AR ,
	NO_WBS,
	NO_CBS,
	CD_USERDEF1,
	CD_USERDEF2,
	NM_USERDEF1,
	NM_USERDEF2,
	TP_UM_TAX,
	DT_EXDATE,
	CD_ITEM_ORIGIN,
	AM_EX_TRANS,
	AM_TRANS,
	NO_LINE_PJTBOM,
	FG_PACKING,
	NUM_USERDEF1,
	AM_REBATE_EX,	
	AM_REBATE,	
	UM_REBATE,
	ID_INSERT,
	DTS_INSERT,
	DATE_USERDEF1,
	DATE_USERDEF2,
	TXT_USERDEF1,
	TXT_USERDEF2,
	CDSL_USERDEF1,
	NUM_USERDEF2,
	NUM_STND_ITEM_1, 
	NUM_STND_ITEM_2,               
	NUM_STND_ITEM_3,
	NUM_STND_ITEM_4,               
	NUM_STND_ITEM_5,
	UM_WEIGHT,
	TOT_WEIGHT,
	NO_RELATION,
	SEQ_RELATION,
	ID_MEMO,
	NUM_USERDEF3,
	NUM_USERDEF4,
	NUM_USERDEF5,
	NO_QUO,
	NO_QUOLINE,
	CD_BIZPLAN,
	CD_USERDEF3,
	CD_USERDEF4,
	NM_USERDEF3,
	NM_USERDEF4,
	YN_BUDGET, BUDGET_PASS,
	CD_ACCT, NM_USERDEF5,
	DC50_PO
)          
VALUES          
( 
	@NO_PO, @NO_LINE,@CD_COMPANY,@CD_PLANT,
	@NO_CONTRACT,@NO_CTLINE,@NO_PR,@NO_PRLINE,@FG_TRANS,@CD_ITEM,          
	@CD_UNIT_MM,@FG_RCV,@FG_PURCHASE,@DT_LIMIT,@QT_PO_MM,@QT_PO,@QT_REQ,@QT_RCV,@FG_TAX,@UM_EX,@AM_EX,@AM,          
	NEOE.FN_SF_GETUNIT_AM('PU','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT,@VAT),@CD_SL,@FG_POST,@FG_POCON,@YN_AUTORCV,@YN_RCV,@YN_RETURN,@YN_ORDER,@YN_SUBCON,@YN_IMPORT,@UM,          
	@RT_PO,@YN_REQ,@CD_PJT,@UM_EX_PO,@NO_APP,@NO_APPLINE, @DC1, @DC2, @SEQ_PROJECT, @UMVAT_PO, @AMVAT_PO,      
	@CD_CC,      
	@CD_BUDGET,      
	@CD_BGACCT,      
	@NO_SO,      
	@NO_SOLINE,  
	@P_GI_PARTNER ,  
	@DC3, -- 2010/12/30 ������ DC3(���3) �÷� �߰�  
	@P_NO_IO_MGMT,  
	@P_NO_IOLINE_MGMT,  
	@P_DT_PLAN,  
	@P_DC4,
	@P_UM_EX_AR,
	@P_NO_WBS,
	@P_NO_CBS,
	@P_CD_USERDEF1,
	@P_CD_USERDEF2,
	@P_NM_USERDEF1,
	@P_NM_USERDEF2,
	@P_TP_UM_TAX,
	@P_DT_EXDATE,
	@P_CD_ITEM_ORIGIN,
	@P_AM_EX_TRANS,
	@P_AM_TRANS, 
	@P_NO_LINE_PJTBOM,
	@P_FG_PACKING,
	@P_NUM_USERDEF,
	@P_AM_REBATE_EX,	
	@P_AM_REBATE,
	@P_UM_REBATE,
	@P_ID_INSERT,
	@P_DTS_INSERT,
	@P_DATE_USERDEF1,
	@P_DATE_USERDEF2,
	@P_TXT_USERDEF1,
	@P_TXT_USERDEF2,	
	@P_CDSL_USERDEF1,
	@P_NUM_USERDEF2,
	@P_NUM_STND_ITEM_1, 
	@P_NUM_STND_ITEM_2,               
	@P_NUM_STND_ITEM_3,
	@P_NUM_STND_ITEM_4,               
	@P_NUM_STND_ITEM_5,
	@P_UM_WEIGHT,   
	@P_TOT_WEIGHT,
	@P_NO_RELATION,
	@P_SEQ_RELATION, 	
	@V_NEW_ID,
	@P_NUM_USERDEF3,
	@P_NUM_USERDEF4,
	@P_NUM_USERDEF5,
	@P_NO_QUO,
	@P_NO_QUOLINE,
	@P_CD_BIZPLAN,
	@P_CD_USERDEF3,
	@P_CD_USERDEF4,
	@P_NM_USERDEF3,
	@P_NM_USERDEF4,
	@P_YN_BUDGET, @P_BUDGET_PASS,
	@P_CD_ACCT, @P_NM_USERDEF5,
	@P_DC50_PO
)

IF(ISNULL(@P_NO_PREIV,'') <>'') --�ű����͸������
BEGIN
	UPDATE PU_PRE_IVLL
	   SET NO_PO = @NO_PO,
	       NO_POLINE = @NO_LINE
	  WHERE NO_PREIV = @P_NO_PREIV
	    AND NO_PREIVLINE = @P_NO_PREIVLINE
END
      
IF (@@ERROR <> 0) BEGIN SELECT @ERRNO = 100000, @ERRMSG = '[UP_PU_POL_INSERT]�۾�������������ó���������߽��ϴ�.' GOTO ERROR END      
  
IF (NEOE.FN_SERVER_KEY('KFL') IS NOT NULL)  
BEGIN    
    IF (ISNULL(@P_NO_VMI ,'') <> '' AND ISNULL(@P_SEQ_VMI,0) <> 0 )  
    BEGIN    
        UPDATE VMI_QTIO_KFL    
        SET NO_PSO = @NO_PO,     
            SEQ_PSO = @NO_LINE  
        WHERE CD_COMPANY = @CD_COMPANY    
            AND CD_PLANT = @CD_PLANT    
            AND FG_QTIO = 'PU'    
            AND NO_VMI = @P_NO_VMI    
            AND SEQ_VMI = @P_SEQ_VMI    
        
        IF (@@ERROR <> 0) BEGIN SELECT @ERRNO  = 100000, @ERRMSG = '[VMI����]�۾�������������ó���������߽��ϴ�.' GOTO ERROR END    
    END   
END  

EXEC  UP_PU_RCV_PROCESS_I 'PU_POL',@CD_COMPANY,@NO_PO,@NO_LINE,@P_ID_INSERT,'N','Y'
EXEC  UP_MM_QTIO_PROCESS_I 'PU_POL',@CD_COMPANY,@NO_PO,@NO_LINE,@P_ID_INSERT,'N','Y',NULL,NULL,NULL,NULL,NULL,NULL,@P_NO_IO
EXEC  UP_PU_IV_PROCESS_I 'PU_POL',@CD_COMPANY,@NO_PO,@NO_LINE,@P_ID_INSERT,'N','Y' 
  
--  --���ſ�û���°���(MS �߰�)---> ���ſ�û��Ͻ�'O', ���ֽ�'R', �԰�Ϸ�Ǵ¹��ָ�����'C'          
--  --���ſ�û���°�'O'�ΰ��'R'��UPDATE          
--  SELECT @FG_POST_PRL = FG_POST FROM PU_PRL          
--  WHERE CD_COMPANY = @CD_COMPANY          
--   AND NO_PR = @NO_PR          
--   AND NO_PRLINE = @NO_PRLINE          
         
/* PU_POL.T      
I_PU_POLƮ���ŷοű�       
 -- ǰ�ǵ���ܷ�����         
 IF( ISNULL(@NO_APP, '') <> '' AND ISNULL(@NO_APPLINE,0) > 0)          
 BEGIN          
  EXEC UP_PU_APPL_FROM_POL_UPDATE @CD_COMPANY, @NO_APP, @NO_APPLINE ,@QT_PO            
  IF (@@ERROR <> 0 )          
  BEGIN            
--        SELECT @ERRNO        
      sa_projectl
= 100000,            
--                     @ERRMSG = 'CM_M100010'            
        GOTO ERROR            
  END           
 END          
          
           
          
 -- ���ſ�û�ܷ�����         
 IF( ISNULL(@NO_PR, '') <> '' AND ISNULL(@NO_PRLINE,0) > 0)          
 BEGIN          
  EXEC UP_PU_PRL_FROM_POL_UPDATE @C      
      
D_COMPANY, @NO_PR, @NO_PRLINE ,@QT_PO            
  IF (@@ERROR <> 0 )          
  BEGIN            
--        SELECT @ERRNO  = 100000,            
--                     @ERRMSG = 'CM_M100010'            
        GOTO ERROR            
  END           
 END          
*/          
 -- ����ܷ�����( �����Ұ��̱⶧�����ȸ���)        
      
          
RETURN      
      
ERROR: RAISERROR (@ERRMSG, 18 , 1)      
RETURN
GO


