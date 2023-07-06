USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_POL_UPDATE]    Script Date: 2022-03-24 오후 3:45:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  발주 등록 수정 (PU_POL)   */        
ALTER PROC [NEOE].[UP_PU_POL_UPDATE]    
(        
 @NO_PO				NVARCHAR(20),  -- 발주번호        
 @NO_LINE			NUMERIC(5),  -- 발주 라인        
 @CD_COMPANY		NVARCHAR(7),  -- 회사        
 @CD_PLANT			NVARCHAR(7),  -- 공장           
 @CD_UNIT_MM		NVARCHAR(3),  -- 발주단위         
 @DT_LIMIT			NCHAR(8),  -- 납기일        
 @QT_PO_MM			NUMERIC(17,4),  -- 발주수량        
 @QT_PO				NUMERIC(17,4),  -- 발주재고수량          
 @UM_EX_PO			NUMERIC(19,6),  -- 발주단가        
 @UM_EX				NUMERIC(19,6),  -- 발주재고단가        
 @AM_EX				NUMERIC(19,6),  -- 금액        
 @UM				NUMERIC(19,6),  -- 원화단가        
 @AM				NUMERIC(17,4),  -- 원화금액        
 @VAT				NUMERIC(17,4),  -- 부가세        
 @CD_SL   			NVARCHAR(7),  -- 창고          
 @RT_PO   			NUMERIC(15,4),         
 @CD_PJT  			NVARCHAR(20),  -- 프로젝트        
 @DC1 				NVARCHAR(200),    
 @DC2 				NVARCHAR(200),     
 @UMVAT_PO			NUMERIC(17, 4) = 0,     
 @AMVAT_PO			NUMERIC(17, 4) = 0,    
 @CD_CC				NVARCHAR(12)  = '',  
 @P_CD_BUDGET		NVARCHAR(20),  
 @P_CD_BGACCT		NVARCHAR(10),  
 @P_GI_PARTNER		NVARCHAR(20) = NULL,  
 @DC3				NVARCHAR(500) = NULL, -- 2010/12/30 조형우 DC3(비고3) 컬럼 추가,  
 @P_DT_PLAN  		NVARCHAR(8) = NULL,   -- 2011.04.01 : 김현철 추가  
 @P_DC4      		NVARCHAR(500) = NULL,  -- 2011.04.01 : 김현철 추가  
 @P_UM_EX_AR 		NUMERIC(15,4) = 0, -- 2011.08.17 : 황슬기 추가 (면적단가)
 @P_NO_WBS 			NVARCHAR(20) = NULL,  
 @P_NO_CBS 			NVARCHAR(20) = NULL,
 @P_CD_ITEM			NVARCHAR(20) = NULL,
 @P_CD_USERDEF1		NVARCHAR(40) = NULL,
 @P_CD_USERDEF2		NVARCHAR(40) = NULL,
 @P_NM_USERDEF1		NVARCHAR(100) = NULL,
 @P_NM_USERDEF2		NVARCHAR(100) = NULL ,
 @P_TP_UM_TAX   	NVARCHAR(3)  = '002', --부가세포함여부 002:별도, 001:포함
 @P_DT_EXDATE		NVARCHAR(8) = NULL,
 @P_CD_ITEM_ORIGIN	NVARCHAR(20) = NULL,
 @P_AM_EX_TRANS		NUMERIC(17,4) = 0,
 @P_AM_TRANS		NUMERIC(17,4) = 0,
 @P_NO_LINE_PJTBOM	NUMERIC(5,0) = 0,
 @P_FG_PACKING		NVARCHAR(4) = NULL,  
 @P_NUM_USERDEF1	NUMERIC(17,4) = 0,
 @P_AM_REBATE_EX	NUMERIC(17,4) = 0,
 @P_AM_REBATE	    NUMERIC(17,4) = 0,
 @P_UM_REBATE	    NUMERIC(15,4) = 0,
 @P_ID_UPDATE		NVARCHAR(15) = NULL,
 @P_DATE_USERDEF1	NCHAR(8) = NULL,
 @P_DATE_USERDEF2   NCHAR(8) = NULL,
 @P_TXT_USERDEF1	NVARCHAR(100) = NULL,
 @P_TXT_USERDEF2	NVARCHAR(100) = NULL,
 @P_CDSL_USERDEF1   NVARCHAR(7) = NULL,
 @P_FG_TAX			NVARCHAR(3),
 @P_NUM_USERDEF2	NUMERIC(17,4) = 0,
 @P_CLS_L			NVARCHAR(4) = NULL,
 @P_CLS_M			NVARCHAR(4) = NULL,
 @P_CLS_S			NVARCHAR(4) = NULL,
 @P_GRP_ITEM		NVARCHAR(20) = NULL,
 @P_NUM_STND_ITEM_1 NUMERIC(17,4) = 0,
 @P_NUM_STND_ITEM_2 NUMERIC(17,4) = 0,
 @P_NUM_STND_ITEM_3 NUMERIC(17,4) = 0,
 @P_NUM_STND_ITEM_4 NUMERIC(17,4) = 0,
 @P_NUM_STND_ITEM_5 NUMERIC(17,4) = 0,
 @P_UM_WEIGHT		NUMERIC(17,4) = 0,
 @P_WEIGHT			NUMERIC(17,4) = 0,
 @P_TOT_WEIGHT		NUMERIC(17,4) = 0,
 @P_STND_ITEM		NVARCHAR(200) = NULL,
 @P_NO_RELATION      NVARCHAR(20) = '',
 @P_SEQ_RELATION     NUMERIC(5,0) = 0,
 @P_NUM_USERDEF3     NUMERIC(17,4) = 0,
 @P_NUM_USERDEF4     NUMERIC(17,4) = 0,
 @P_NUM_USERDEF5     NUMERIC(17,4) = 0,
 @P_CD_BIZPLAN		NVARCHAR(20) = NULL,
 @P_CD_USERDEF3		NVARCHAR(20) = NULL,
 @P_CD_USERDEF4		NVARCHAR(20) = NULL,
 @P_NM_USERDEF3		NVARCHAR(200) = NULL,
 @P_NM_USERDEF4		NVARCHAR(100) = NULL,
 @P_CD_ACCT			NVARCHAR(20) = '',
 @P_NM_USERDEF5		NVARCHAR(50) = '',
 @P_DC50_PO			NVARCHAR(100) = NULL,
 @P_SEQ_PROJECT		NUMERIC(5,0) = 0
)      
AS    
DECLARE 
  @ERRNO		INT,        
  @ERRMSG		NVARCHAR(255),        
  @QT_GAP		NUMERIC(17,4),        
  @QT_REQ		NUMERIC(17,4),        
  @YN_ORDER		NCHAR(1),        
  @FG_POST		NCHAR(3),          
  @NO_CONTRACT  NVARCHAR(20),  -- 계약번호        
  @NO_CTLINE	NUMERIC(3),  -- 계약라인        
  @NO_PR		NVARCHAR(20),  -- 요청번호        
  @NO_PRLINE    NUMERIC(5),  -- 요청라인         
  @NO_APP		NVARCHAR(20),  -- 품의번호        
  @NO_APPLINE   NUMERIC(5),  -- 품의라인        
  @YN_IMPORT	NCHAR(1),        
  @QT_TR		NUMERIC(17,4),
  @P_DTS_UDDATE	NVARCHAR(15),
  @V_SERVER_KEY	NVARCHAR(25),
  @V_STND		CHAR(1),
  @V_PJT_AM		NUMERIC(19,6),     
  @V_PO_AM			NUMERIC(19,6),
  @V_FG_PURCHASE		NVARCHAR(3) 
 
SELECT @V_SERVER_KEY = SERVER_KEY
  FROM CM_SERVER_CONFIG
 WHERE YN_UPGRADE = 'Y'                 
      
  SELECT @P_DTS_UDDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')      
        
-- 2011-06-24 영우디지털에서 틀린값이 들어와서 조치를 취함
IF @QT_PO <> @QT_PO_MM * @RT_PO
BEGIN      
   IF (@@ERROR <> 0) BEGIN SELECT @ERRNO = 100000, @ERRMSG = '항번 : ' + CONVERT(NVARCHAR, @NO_LINE) + ' 의 재고수량과 (발주수량 * 환산수량) 이 일치하지 않습니다. 다시확인하여주시기 바랍니다. ' GOTO ERROR END      
END   

-- 발주라인 존재여부 검사         
IF EXISTS (SELECT 1 FROM PU_POL WHERE NO_PO = @NO_PO AND NO_LINE = @NO_LINE AND CD_COMPANY = @CD_COMPANY)        
BEGIN        
 SELECT @QT_GAP = QT_PO, @YN_ORDER = YN_ORDER, @FG_POST = FG_POST, @QT_REQ = QT_REQ ,        
  @NO_CONTRACT = NO_CONTRACT, @NO_CTLINE = NO_CTLINE, @NO_PR= NO_PR,@NO_PRLINE = NO_PRLINE,        
  @NO_APP = NO_APP, @NO_APPLINE=NO_APPLINE, @YN_IMPORT = YN_IMPORT , @QT_TR = QT_TR,  
  @P_CD_BUDGET = CD_BUDGET,  
  @P_CD_BGACCT = CD_BGACCT  
 FROM PU_POL         
 WHERE NO_PO = @NO_PO AND NO_LINE = @NO_LINE AND CD_COMPANY = @CD_COMPANY        
        
  --PU_POL.TU_PU_POL 트리거로 옮김       
  -- 자동승인이면 .. 의뢰량이 0이여야함        
  --IF( @YN_ORDER = 'Y' )        
  --BEGIN        
  -- IF(ISNULL( @QT_REQ, 0)  > 0)        
  -- BEGIN          
  --       SELECT @ERRNO  = 100000,          
  --              @ERRMSG = 'PU_M000038'          
  --       GOTO ERROR          
  -- END         
  --END        
  --ELSE  -- 확정이 안된것만..수정가능..         
  BEGIN         
   IF(ISNULL( @FG_POST, 'O') <> 'O' )        
   BEGIN          
         SELECT @ERRNO  = 100000,          
                @ERRMSG = 'PR_M200015'          
         GOTO ERROR          
   END         
  END         
        
  /*IF( @YN_IMPORT = 'Y')        
  BEGIN        
   IF(ISNULL( @QT_TR, 0)  > 0)        
   BEGIN          
         SELECT @ERRNO  = 100000,          
                      @ERRMSG = 'PU_M000038'          
         GOTO ERROR          
   END         
  END        
  */         
 
  IF (@V_SERVER_KEY =  'KORAVL')
  BEGIN
	SELECT @V_PJT_AM = AM 
	  FROM SA_PROJECTL_D
	 WHERE CD_COMPANY = @CD_COMPANY
	   AND NO_PROJECT = @CD_PJT
	   AND SEQ_PROJECT = @P_SEQ_PROJECT
	   AND NO_LINE = @P_NO_LINE_PJTBOM
	 
	SELECT @V_PO_AM = ISNULL(SUM(AM),0)
	  FROM PU_POL
	 WHERE CD_COMPANY = @CD_COMPANY
	   AND CD_PJT = @CD_PJT
	   AND SEQ_PROJECT = @P_SEQ_PROJECT
	   AND NO_LINE_PJTBOM = @P_NO_LINE_PJTBOM
	   AND NO_PO <> @NO_PO

         
	IF(@V_PJT_AM < @V_PO_AM + @AM )
	BEGIN
		SELECT @ERRMSG = '발주금액 (' + CONVERT(VARCHAR(30),(@V_PO_AM + @AM) ) + ')는 프로젝트 금액('+CONVERT(VARCHAR(30),@V_PJT_AM) +')을 초과할 수 없습니다.' +  CHAR(13) +       
						 '품목코드 :' + @P_CD_ITEM +
						 '     프로젝트번호 :' + @CD_PJT + '     UNIT항번:' + CONVERT(VARCHAR(5),@P_SEQ_PROJECT)+ '     BOM항번:' + CONVERT(VARCHAR(5),@P_NO_LINE_PJTBOM)        
		GOTO ERROR       
	END
  END
   
  --신진SM의 경우 규격형 사용으로 입력된 CD_ITEM이 없을 경우 MA_PITEM에 INSERT처리    
  IF (@V_SERVER_KEY =  'SINJINSM' OR @V_SERVER_KEY =  'DZSQL' OR @V_SERVER_KEY =  'SQL_108')
    BEGIN     
    
		SELECT @V_STND = '1' 
		  FROM MA_PITEM P
		 WHERE P.CD_COMPANY = @CD_COMPANY
		   AND P.CD_PLANT = @CD_PLANT
		   AND P.CD_ITEM = @P_CD_ITEM
		
		--공장품목이 존재하지 않을 경우    
		IF ISNULL(@V_STND,'') != '1'
		BEGIN  
			EXEC UP_PU_STND_PITEM_INSERT
			@CD_COMPANY,		@CD_PLANT,		    @P_CD_ITEM,			'001',				@P_GRP_ITEM,
			@P_CLS_L,			@P_CLS_M,			@P_CLS_S,			'EA',				'EA',
			1,					1,					@P_WEIGHT,			'KG',				@P_UM_WEIGHT,		
			@P_ID_UPDATE,		NULL,				NULL,				NULL,				NULL,
			NULL,				@P_NUM_STND_ITEM_1,	@P_NUM_STND_ITEM_2,	@P_NUM_STND_ITEM_3,	@P_NUM_STND_ITEM_4,					
			@P_NUM_STND_ITEM_5,	@P_STND_ITEM  
		END
	END

  IF(@V_SERVER_KEY LIKE 'MEERE%' AND @CD_COMPANY = '1000' AND @CD_PLANT = '1000')
  BEGIN  
  
	SET @V_FG_PURCHASE = NEOE.FN_Z_MEERE_GET_FG_PURCHASE('1000', '1000', @NO_PO, @CD_PJT, '')
	
  END
    
  -- 라인 수정          
 UPDATE PU_POL         
   SET  CD_UNIT_MM=@CD_UNIT_MM, DT_LIMIT=@DT_LIMIT,QT_PO_MM=@QT_PO_MM,        
		QT_PO =@QT_PO,UM_EX=@UM_EX,AM_EX=@AM_EX,AM=@AM,VAT=@VAT,CD_SL=@CD_SL,UM=@UM,RT_PO=@RT_PO ,        
		CD_PJT=@CD_PJT, UM_EX_PO =@UM_EX_PO, DC1 = @DC1, DC2 = @DC2,     
		UMVAT_PO = @UMVAT_PO, AMVAT_PO = @AMVAT_PO,    
		CD_CC = @CD_CC  ,  
		CD_BUDGET = @P_CD_BUDGET,  
		CD_BGACCT = @P_CD_BGACCT,  
		GI_PARTNER = @P_GI_PARTNER,  
		DC3 = @DC3, --2010/12/30 조형우 DC3(비고3) 컬럼 추가  
		DT_PLAN = @P_DT_PLAN,  
		DC4 = @P_DC4,
		UM_EX_AR = @P_UM_EX_AR ,
		NO_WBS = @P_NO_WBS,
		NO_CBS = @P_NO_CBS,
		CD_ITEM = @P_CD_ITEM,
		CD_USERDEF1 = @P_CD_USERDEF1,
		CD_USERDEF2 = @P_CD_USERDEF2,
		NM_USERDEF1 = @P_NM_USERDEF1,
		NM_USERDEF2 = @P_NM_USERDEF2,
		TP_UM_TAX = @P_TP_UM_TAX,
		DT_EXDATE = @P_DT_EXDATE,
		CD_ITEM_ORIGIN = @P_CD_ITEM_ORIGIN,	
		AM_EX_TRANS = @P_AM_EX_TRANS,	
		AM_TRANS = @P_AM_TRANS,
		NO_LINE_PJTBOM = @P_NO_LINE_PJTBOM,
		FG_PACKING = @P_FG_PACKING,		
		NUM_USERDEF1 = @P_NUM_USERDEF1,
		AM_REBATE_EX = @P_AM_REBATE_EX,	
		AM_REBATE	 = @P_AM_REBATE,
		UM_REBATE = @P_UM_REBATE,
		ID_UPDATE = @P_ID_UPDATE,
		DTS_UPDATE = @P_DTS_UDDATE,
		DATE_USERDEF1 = @P_DATE_USERDEF1,
		DATE_USERDEF2 = @P_DATE_USERDEF2,
		TXT_USERDEF1 = @P_TXT_USERDEF1,
		TXT_USERDEF2 = @P_TXT_USERDEF2,
		CDSL_USERDEF1 = @P_CDSL_USERDEF1,
		FG_TAX = @P_FG_TAX,
		NUM_USERDEF2 = @P_NUM_USERDEF2,
	   NUM_STND_ITEM_1 = @P_NUM_STND_ITEM_1, 
	   NUM_STND_ITEM_2 = @P_NUM_STND_ITEM_2,               
	   NUM_STND_ITEM_3 = @P_NUM_STND_ITEM_3,
	   NUM_STND_ITEM_4 = @P_NUM_STND_ITEM_4,               
	   NUM_STND_ITEM_5 = @P_NUM_STND_ITEM_5,
	   UM_WEIGHT	   = @P_UM_WEIGHT,
	   TOT_WEIGHT	   = @P_TOT_WEIGHT,
	   NO_RELATION     =  @P_NO_RELATION,
	   SEQ_RELATION    =  @P_SEQ_RELATION,
	   NUM_USERDEF3	   = @P_NUM_USERDEF3,
	   NUM_USERDEF4	   = @P_NUM_USERDEF4,
	   NUM_USERDEF5	   = @P_NUM_USERDEF5,
	   CD_BIZPLAN	   = @P_CD_BIZPLAN,	
	   CD_USERDEF3	   = @P_CD_USERDEF3,
	   CD_USERDEF4	   = @P_CD_USERDEF4,
	   NM_USERDEF3	   = @P_NM_USERDEF3,
	   NM_USERDEF4	   = @P_NM_USERDEF4,
	   CD_ACCT		   = @P_CD_ACCT,
	   NM_USERDEF5	   = @P_NM_USERDEF5,
	   DC50_PO		   = @P_DC50_PO,
	   SEQ_PROJECT	   = @P_SEQ_PROJECT,
	   FG_PURCHASE     = CASE WHEN @V_SERVER_KEY LIKE 'MEERE%' AND @CD_COMPANY = '1000' AND @CD_PLANT = '1000' AND ISNULL(@V_FG_PURCHASE, '') <> '' THEN @V_FG_PURCHASE ELSE FG_PURCHASE END
 WHERE NO_PO=@NO_PO         
  AND NO_LINE =@NO_LINE        
  AND CD_COMPANY=@CD_COMPANY         
    
 IF (@@ERROR <> 0) BEGIN SELECT @ERRNO = 100000, @ERRMSG = '[UP_PU_POL_UPDATE]작업을 정상적으로 처리하지 못했습니다.' GOTO ERROR END    
        
  /* PU_POL.TU_PU_POL 트리거로 옮김        
  -- 수량 차이 계산.        
  SET @QT_GAP = @QT_PO - @QT_GAP        
        
          
  -- 품의등록 잔량관리        
  IF( ISNULL(@NO_APP, '') <> '' AND ISNULL(@NO_APPLINE,0) > 0 AND @QT_GAP <> 0 )        
  BEGIN        
   EXEC UP_PU_APPL_FROM_POL_UPDATE @CD_COMPANY, @NO_APP, @NO_APPLINE ,@QT_GAP          
IF (@@ERROR <> 0 )        
   BEGIN        
         SELECT @ERRNO  = 100000,          
                      @ERRMSG = 'CM_M100010'          
         GOTO ERROR          
   END         
  END        
          
         
  -- 구매요청 잔량관리        
  IF( ISNULL(@NO_PR, '') <> '' AND ISNULL(@NO_PRLINE,0) > 0  AND @QT_GAP <> 0 )        
  BEGIN        
   EXEC UP_PU_PRL_FROM_POL_UPDATE @CD_COMPANY, @NO_PR, @NO_PRLINE ,@QT_GAP          
   IF (@@ERROR <> 0 )        
  BEGIN          
         SELECT @ERRNO  = 100000,     
                      @ERRMSG = 'CM_M100010'          
         GOTO ERROR          
   END         
  END          
 */           
END        
ELSE  -- 존재하지 않으면..        
BEGIN          
 SELECT @ERRNO = 100000, @ERRMSG = '[UP_PU_POL_UPDATE]해당 발주건 데이터가 존재 하지 않습니다.' GOTO ERROR    
END         
    
RETURN     
ERROR: RAISERROR (@ERRMSG, 18 , 1)
GO


