USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SOL_U]    Script Date: 2019-11-04 오전 10:37:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************************************/ 
/*********************************************************************************************/         
/*********************************************************************************************/ 
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOL_U_WINTEC]
(        
    @P_CD_COMPANY			NVARCHAR(7),		--회사        
    @P_NO_SO				NVARCHAR(20),       --수주번호                
    @P_SEQ_SO				NUMERIC(5),         --수주항번        
    @P_CD_PLANT				NVARCHAR(7),        --공장        
    @P_CD_ITEM				NVARCHAR(50),       --품목        
    @P_UNIT_SO				NVARCHAR(3),        --단위
	@P_DT_EXPECT			NVARCHAR(8),		--최초납기요구일
    @P_DT_DUEDATE			NVARCHAR(8),        --납기요구일        
    @P_DT_REQGI				NVARCHAR(8),        --출하예정일        
    @P_QT_SO				NUMERIC(17, 4),     --수량        
    @P_UM_SO				NUMERIC(19, 6),     --단가        
    @P_AM_SO				NUMERIC(19, 6),     --금액        
    @P_AM_WONAMT			NUMERIC(17, 4),     --원화금액        
    @P_AM_VAT				NUMERIC(17, 4),     --부가세(원화)        
    @P_UNIT_IM				NVARCHAR(3),        --재고단위        
    @P_QT_IM				NUMERIC(17, 4),     --재고수량        
    @P_CD_SL				NVARCHAR(7),        --창고        
    @P_TP_ITEM				NVARCHAR(3),        --품목타입        
    @P_ID_UPDATE			NVARCHAR(15),       --사용자ID        
    @P_GI_PARTNER			NVARCHAR(20),       --납품처          
    @P_NO_PROJECT			NVARCHAR(7),        --프로젝트번호      
    @P_SEQ_PROJECT			NUMERIC(5),         --프로젝트항번      
    @P_ITEM_PARTNER			NVARCHAR(40),       --발주품번        
    @P_NM_ITEM_PARTNER		NVARCHAR(50),       --거래처품번        
    @P_DC1					NVARCHAR(200),        
    @P_DC2					NVARCHAR(200),           
    @P_UMVAT_SO				NUMERIC(19, 6),     --부가세포함단가      
    @P_AMVAT_SO				NUMERIC(17, 4),     --부가세포함금액      
	@P_CD_SHOP				NVARCHAR(6),		--접수코드(쇼핑몰)
    @P_CD_SPITEM			NVARCHAR(40),       --상품코드  
    @P_CD_OPT				NVARCHAR(40),       --옵션코드  
    @P_RT_DSCNT				NUMERIC(15, 4),     --할인율  
    @P_UM_BASE				NUMERIC(19, 6),     --기준단가  

    /*** SA_SOL_DLV 테이블에 에 저장될 내역들 ***/  
    @P_NM_CUST_DLV			NVARCHAR(30),       --수취인  
    @P_CD_ZIP				NVARCHAR(12),       --우편번호  
    @P_ADDR1				NVARCHAR(300),      --주소1  
    @P_ADDR2				NVARCHAR(200),      --주소2(상세주소)  
    @P_NO_TEL_D1			NVARCHAR(20),       --전화(회사전화 나 집전화)  
    @P_NO_TEL_D2			NVARCHAR(20),       --이동전화(이동통신전화)  
    @P_TP_DLV				NVARCHAR(3),        --배송방법  
    @P_DC_REQ				NVARCHAR(400),      --비고   
    @P_FG_TRACK_L			NVARCHAR(5),        --배송정보 TRACK 기능추가 => SO : 수주등록, M : 출고요청등록, R : 출하반품의뢰등록   
    @P_TP_DLV_DUE			NVARCHAR(4),        --납품방법  
    @P_FG_USE				NVARCHAR(4),        --수주용도
    @P_TP_VAT       		NVARCHAR(3),        --과세구분      
	@P_RT_VAT				NUMERIC(5, 2),      --부가세율   
	@P_CD_CC				NVARCHAR(12),
	@P_NO_POLINE_PARTNER	NUMERIC(5,0)	= 0,	--장은경 2010.06.17 거래처PO항번
	@P_UM_OPT				NUMERIC(15,4)	= 0,	--특수단가 : 장은경 2010.07.20
	@P_NO_PO_PARTNER		NVARCHAR(50)	= NULL,	--장은경 : 2010.07.27 거래처PO번호
	@P_CD_WH				NVARCHAR(7)		= NULL,	--심재희 : 2010.11.11 W/H 추가
	@P_NO_SO_ORIGINAL		NVARCHAR(20)	= NULL,	--서건수 : 2010.11.12 원천수주번호 추가             
	@P_SEQ_SO_ORIGINAL		NUMERIC(5)		= 0,	--서건수 : 2010.11.12 원천수주항번 추가
	@P_NUM_USERDEF1			NUMERIC(17,4)	= 0,	--심재희 : 2010.12.30 사용자정의금액1(한국로스트왁스 : 금형비)
	@P_NUM_USERDEF2			NUMERIC(17,4)	= 0,	--심재희 : 2010.12.30 사용자정의금액2(한국로스트왁스 : 개발비)
    @P_NUM_USERDEF3			NUMERIC(17,4)	= 0,	--심재희 : 2010.11.15 사용자정의금액3(조선호텔베이커리요청)
	@P_NUM_USERDEF4			NUMERIC(17,4)	= 0,    --심재희 : 2010.11.15 사용자정의금액4(조선호텔베이커리요청)
	@P_NUM_USERDEF5			NUMERIC(17,4)	= 0,	--심재희 : 2010.11.15 사용자정의금액5(조선호텔베이커리요청)
	@P_NUM_USERDEF6			NUMERIC(17,4)	= 0,	--심재희 : 2010.11.15 사용자정의금액6(조선호텔베이커리요청)	
    @P_CD_MNGD1             NVARCHAR(20)    = NULL, --PIMS:D20120227052
	@P_CD_MNGD2             NVARCHAR(20)    = NULL, --PIMS:D20120227052
	@P_CD_MNGD3             NVARCHAR(20)    = NULL, --PIMS:D20120227052
	@P_CD_MNGD4             NVARCHAR(20)    = NULL, --PIMS:D20120227052
	@P_TXT_USERDEF1         NVARCHAR(200),
	@P_TXT_USERDEF2         NVARCHAR(100),
	@P_YN_OPTION            NCHAR(1),               --선급검사여부
	@P_NO_ORDER             NVARCHAR(40)    = NULL, --오준회 : 2013.08.30 배송지정보 주문번호          (부강샘스요청)
	@P_NM_CUST              NVARCHAR(30)    = NULL, --오준회 : 2013.08.30 배송지정보 주문자            (부강샘스요청)
	@P_NO_TEL1              NVARCHAR(20)    = NULL, --오준회 : 2013.08.30 배송지정보 주문자전화번호    (부강샘스요청)
	@P_NO_TEL2              NVARCHAR(20)    = NULL, --오준회 : 2013.08.30 배송지정보 주문자이동전화번호(부강샘스요청)
	@P_DLV_TXT_USERDEF1     NVARCHAR(100)   = NULL, --오준회 : 2013.08.30 배송지정보 사용자정의텍스트1 (부강샘스요청)
	@P_CD_ITEM_REF          NVARCHAR(20)    = NULL, --오준회 : 2013.11.26 SET품목코드                  (시몬스)
    @P_YN_PICKING           NCHAR(1)	    = NULL, --오준회 : 2013.11.26 배차여부                     (시몬스)
    @P_DLV_CD_USERDEF1      NVARCHAR(3)     = NULL, --오준회 : 2013.11.26 배송지정보 사용자정의코드1   (시몬스)  
    @P_FG_USE2              NVARCHAR(4)     = NULL,
    @P_CD_USERDEF1          NVARCHAR(3)	    = NULL, --선급검사기관1
    @P_CD_USERDEF2          NVARCHAR(3)	    = NULL, --선급검사기관2
    @P_UM_EX	            NUMERIC(19,6)	= NULL,
    @P_CD_USERDEF4			NVARCHAR(6)		= NULL, --최정훈 : D20160825004 (히오키코리아)
    @P_TXT_USERDEF3			NVARCHAR(200)	= NULL, --자재번호
    @P_TXT_USERDEF4			NVARCHAR(200)	= NULL, --도장 COLOR
	@P_TXT_USERDEF5   		NVARCHAR(200)	= NULL, --납품장소
    @P_TXT_USERDEF6			NVARCHAR(200)	= NULL, --호선번호
    @P_TXT_USERDEF7			NVARCHAR(200)	= NULL, --최정훈 : D20160825004 (히오키코리아)
    @P_TXT_USERDEF8			NVARCHAR(200)	= NULL, --최정훈 : D20160825004 (히오키코리아)
    @P_TXT_USERDEF9			NVARCHAR(200)	= NULL, --최정훈 : D20160825004 (히오키코리아)
    @P_TXT_USERDEF10		NVARCHAR(200)	= NULL, --최정훈 : D20160825004 (히오키코리아)
    @P_TXT_USERDEF11		NVARCHAR(200)	= NULL, --최정훈 : D20160825004 (히오키코리아)
    @P_UM_EX_PO				NUMERIC(17,4)   = 0,
    @P_AM_EX_PO				NUMERIC(19,6)   = 0,
    @P_NO_EST				NVARCHAR(20) = NULL,
    @P_NO_EST_HST			NUMERIC(3, 0) = 0,
    @P_SEQ_EST				NUMERIC(5, 0) = 0,
    
    @P_TP_BUSI      		NVARCHAR(3),            --거래구분 
    @P_TP_GI        		NVARCHAR(3),            --출하형태      
    @P_GIR        			NVARCHAR(1),            --의뢰여부      
	@P_GI        			NVARCHAR(1),            --출하여부      
	@P_IV        			NVARCHAR(1),            --매출여부      
	@P_TRADE        		NVARCHAR(1),            --수출여부      
	@P_STA_SO	       		NVARCHAR(3),
    @P_CD_USERDEF3          NVARCHAR(3),            --엔진타입
	@P_NUM_USERDEF7			NUMERIC(17,4)	= 0,
	@P_NUM_USERDEF8			NUMERIC(17,4)	= 0,
	@P_NUM_USERDEF9			NUMERIC(17,4)	= 0
)        
      
AS        
    
DECLARE @V_DTS_UPDATE   NVARCHAR(14),
        @V_STA_SO       NVARCHAR(3),
        @P_FG_TRACK     NVARCHAR(5),
        @V_UM_EX		NUMERIC(19,6),
        @V_SERVER_KEY	NVARCHAR(25)
        
SET     @V_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')        

SELECT  @V_STA_SO   = STA_SO
FROM    SA_SOL
WHERE   CD_COMPANY  = @P_CD_COMPANY
AND     NO_SO       = @P_NO_SO
AND     SEQ_SO      = @P_SEQ_SO

SELECT  @V_SERVER_KEY = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG WHERE YN_UPGRADE = 'Y'     


IF(@P_UM_EX IS NULL)
BEGIN 
	IF(@P_QT_IM = 0)
	BEGIN
		SELECT @V_UM_EX = 0
	END 
	ELSE 
	BEGIN
		SELECT @V_UM_EX = (@P_AM_SO / @P_QT_IM)
	END 
END
ELSE
BEGIN 
	 SELECT @V_UM_EX = @P_UM_EX
END 

IF(@V_SERVER_KEY != 'DAEKHON' AND @V_SERVER_KEY != 'BOBOO') --대곤, 보부하이테크
BEGIN
	IF(@V_STA_SO <> 'O')
	BEGIN
		RAISERROR ('수주상태가 미정인 것만 수정 할 수 있습니다. ', 18, 1)
		RETURN
	END
END

IF (@P_TP_BUSI = '' OR
	@P_TP_GI   = '' OR
	@P_GIR     = '' OR
	@P_GI      = '' OR
	@P_IV      = '' OR
	@P_TRADE   = '' OR
	@P_STA_SO  = '')
BEGIN
	RAISERROR ('수주유형 설정값이 중 없는 데이터가 존재합니다. ', 18, 1)
		RETURN
END

DECLARE @V_QT_SO NUMERIC(17, 4)

SELECT @V_QT_SO = SL.QT_SO 
FROM SA_SOL SL
WHERE SL.CD_COMPANY = @P_CD_COMPANY
AND SL.NO_SO = @P_NO_SO
AND SL.SEQ_SO = @P_SEQ_SO

IF ISNULL(@V_QT_SO, 0) <> ISNULL(@P_QT_SO, 0)
BEGIN
    IF EXISTS (SELECT 1 
               FROM CZ_PR_SA_SOL_PR_WO_MAPPING
               WHERE CD_COMPANY = @P_CD_COMPANY
               AND NO_SO = @P_NO_SO
               AND NO_LINE = @P_SEQ_SO)
    BEGIN
        RAISERROR('작업지시에 할당된 수주데이터는 할당해제 후 삭제 해야 합니다.', 16, 1)
        RETURN
    END
    
    IF EXISTS (SELECT 1 
               FROM CZ_PR_SA_SOL_PU_PRL_MAPPING
               WHERE CD_COMPANY = @P_CD_COMPANY
               AND NO_SO = @P_NO_SO
               AND NO_LINE = @P_SEQ_SO)
    BEGIN
        RAISERROR('구매요청에 할당된 수주데이터는 할당해제 후 삭제 해야 합니다.', 16, 1)
        RETURN
    END
    
    IF EXISTS (SELECT 1 
               FROM CZ_PR_SA_SOL_SU_PRL_MAPPING
               WHERE CD_COMPANY = @P_CD_COMPANY
               AND NO_SO = @P_NO_SO
               AND NO_LINE = @P_SEQ_SO)
    BEGIN
        RAISERROR('외주요청에 할당된 수주데이터는 할당해제 후 삭제 해야 합니다.', 16, 1)
        RETURN
    END    
END

EXEC UP_SA_SO_HOSTORY_CHK @P_CD_COMPANY, @P_NO_SO
IF @@ERROR <> 0 RETURN
      
UPDATE  SA_SOL        
   SET  CD_PLANT			= @P_CD_PLANT,         
        CD_ITEM				= @P_CD_ITEM,         
        UNIT_SO				= @P_UNIT_SO,
		DT_EXPECT			= @P_DT_EXPECT,
        DT_DUEDATE			= @P_DT_DUEDATE,         
        DT_REQGI			= @P_DT_REQGI,         
        QT_SO				= @P_QT_SO,         
        UM_SO				= @P_UM_SO,         
        AM_SO				= @P_AM_SO,         
        AM_WONAMT			= @P_AM_WONAMT,         
        AM_VAT				= @P_AM_VAT,         
        UNIT_IM				= @P_UNIT_IM,         
        QT_IM				= @P_QT_IM,         
        CD_SL				= @P_CD_SL,         
        TP_ITEM				= @P_TP_ITEM,         
        ID_UPDATE			= @P_ID_UPDATE,        
        DTS_UPDATE			= @V_DTS_UPDATE,        
        GI_PARTNER			= @P_GI_PARTNER,       
        --프로젝트 번호와 프로젝트 항번은 수정될수 없다. 2009.02.10 일단 트리거에 걸려있음  
        --NO_PROJECT		= @P_NO_PROJECT,       
        --SEQ_PROJECT		= @P_SEQ_PROJECT,       
        CD_ITEM_PARTNER		= @P_ITEM_PARTNER,        
        NM_ITEM_PARTNER		= @P_NM_ITEM_PARTNER,        
        DC1					= @P_DC1,        
        DC2					= @P_DC2,         
        UMVAT_SO			= @P_UMVAT_SO,         
        AMVAT_SO			= @P_AMVAT_SO,   
        CD_SHOP				= @P_CD_SHOP,         
        CD_SPITEM			= @P_CD_SPITEM,         
        CD_OPT				= @P_CD_OPT,   
        RT_DSCNT			= @P_RT_DSCNT,      
        UM_BASE				= @P_UM_BASE, 
        FG_USE				= @P_FG_USE, 
        TP_VAT				= @P_TP_VAT,  
        RT_VAT				= @P_RT_VAT, 
        CD_CC				= @P_CD_CC,
        NO_POLINE_PARTNER	= @P_NO_POLINE_PARTNER,
        UM_OPT				= ISNULL(@P_UM_OPT, 0),
        NO_PO_PARTNER		= @P_NO_PO_PARTNER,
        CD_WH				= @P_CD_WH,
		NO_SO_ORIGINAL		= @P_NO_SO_ORIGINAL,           
	    SEQ_SO_ORIGINAL		= @P_SEQ_SO_ORIGINAL,
	    NUM_USERDEF1		= @P_NUM_USERDEF1,
	    NUM_USERDEF2		= @P_NUM_USERDEF2,
	    NUM_USERDEF3		= @P_NUM_USERDEF3,
	    NUM_USERDEF4		= @P_NUM_USERDEF4,
	    NUM_USERDEF5		= @P_NUM_USERDEF5,
	    NUM_USERDEF6		= @P_NUM_USERDEF6,
	    CD_MNGD1            = @P_CD_MNGD1,
	    CD_MNGD2            = @P_CD_MNGD2,
	    CD_MNGD3            = @P_CD_MNGD3,
	    CD_MNGD4            = @P_CD_MNGD4,
	    TXT_USERDEF1        = @P_TXT_USERDEF1,
	    TXT_USERDEF2        = @P_TXT_USERDEF2,
	    YN_OPTION           = @P_YN_OPTION,
	    CD_ITEM_REF         = @P_CD_ITEM_REF,
	    YN_PICKING          = @P_YN_PICKING,
	    FG_USE2             = @P_FG_USE2,
	    CD_USERDEF1         = @P_CD_USERDEF1,
	    CD_USERDEF2         = @P_CD_USERDEF2,
	    UM_EX				= @V_UM_EX,
	    TXT_USERDEF3		= @P_TXT_USERDEF3,
	    TXT_USERDEF4		= @P_TXT_USERDEF4,
	    TXT_USERDEF5		= @P_TXT_USERDEF5,
	    TXT_USERDEF6		= @P_TXT_USERDEF6,
	    TXT_USERDEF7		= @P_TXT_USERDEF7,
	    TXT_USERDEF8		= @P_TXT_USERDEF8,
	    TXT_USERDEF9		= @P_TXT_USERDEF9,
	    TXT_USERDEF10		= @P_TXT_USERDEF10,
	    TXT_USERDEF11		= @P_TXT_USERDEF11,
		UM_EX_PO			= @P_UM_EX_PO,
		AM_EX_PO			= @P_AM_EX_PO,
		NO_EST				= @P_NO_EST,	
		NO_EST_HST			= @P_NO_EST_HST,
		SEQ_EST				= @P_SEQ_EST,
		TP_BUSI				= @P_TP_BUSI,
		TP_GI  				= @P_TP_GI,  
		GIR    				= @P_GIR,    
		GI     				= @P_GI,     
		IV     				= @P_IV,     
		TRADE  				= @P_TRADE,
		STA_SO				= @P_STA_SO,
		NUM_USERDEF7		= @P_NUM_USERDEF7,
		NUM_USERDEF8		= @P_NUM_USERDEF8,
		NUM_USERDEF9		= @P_NUM_USERDEF9,
		CD_USERDEF3         = @P_CD_USERDEF3							
 WHERE  CD_COMPANY			= @P_CD_COMPANY         
   AND  NO_SO				= @P_NO_SO        
   AND  SEQ_SO				= @P_SEQ_SO         
        
/*** SA_SOL_DLV 테이블에 에 저장될 내역들 ***/  
UPDATE  SA_SOL_DLV        
   SET  NM_CUST_DLV			= @P_NM_CUST_DLV,     
        CD_ZIP				= @P_CD_ZIP,     
        ADDR1				= @P_ADDR1,     
        ADDR2				= @P_ADDR2,     
        NO_TEL_D1			= @P_NO_TEL_D1,     
        NO_TEL_D2			= @P_NO_TEL_D2,     
        TP_DLV				= @P_TP_DLV,     
        DC_REQ				= @P_DC_REQ,  
        TP_DLV_DUE			= @P_TP_DLV_DUE,
        NO_ORDER            = @P_NO_ORDER,
        NM_CUST             = @P_NM_CUST,
        NO_TEL1             = @P_NO_TEL1,
        NO_TEL2             = @P_NO_TEL2,
        TXT_USERDEF1        = @P_DLV_TXT_USERDEF1,
        CD_USERDEF1         = @P_DLV_CD_USERDEF1   
 WHERE  CD_COMPANY			= @P_CD_COMPANY         
   AND  NO_SO				= @P_NO_SO        
   AND  SEQ_SO				= @P_SEQ_SO       
   AND  FG_TRACK			= 'SO'
GO

