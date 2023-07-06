USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SOL_I]    Script Date: 2016-10-10 오후 5:51:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************************************/ 
/*********************************************************************************************/ 
/*********************************************************************************************/ 
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOL_I]      
(      
	@P_SERVERKEY			NVARCHAR(50),		--업체서버키
	@P_CD_COMPANY   		NVARCHAR(7),		--회사      
	@P_NO_SO        		NVARCHAR(20),		--수주번호              
	@P_SEQ_SO       		NUMERIC(5),         --수주항번      
	@P_CD_PLANT     		NVARCHAR(7),        --공장      
	@P_CD_ITEM      		NVARCHAR(50),		--품목      
	@P_UNIT_SO      		NVARCHAR(3),        --단위      
	@P_DT_DUEDATE   		NVARCHAR(8),        --납기요구일      
	@P_DT_REQGI     		NVARCHAR(8),        --출고예정일      
	@P_QT_SO        		NUMERIC(17, 4),     --수량      
	@P_UM_SO        		NUMERIC(19, 6),     --단가      
	@P_AM_SO        		NUMERIC(19, 6),     --금액      
	@P_AM_WONAMT    		NUMERIC(17, 4),     --원화금액      
	@P_AM_VAT       		NUMERIC(17, 4),     --부가세(원화)      
	@P_UNIT_IM      		NVARCHAR(3),        --재고단위      
	@P_QT_IM        		NUMERIC(17, 4),     --재고수량      
	@P_CD_SL        		NVARCHAR(7),        --창고      
	@P_TP_ITEM      		NVARCHAR(3),        --품목타입      
	@P_STA_SO       		NVARCHAR(3),        --수주상태      
	@P_TP_BUSI      		NVARCHAR(3),        --거래구분      
	@P_TP_GI        		NVARCHAR(3),        --출고형태      
	@P_TP_IV        		NVARCHAR(3),        --매출형태    
	@P_GIR        			NVARCHAR(1),        --의뢰여부      
	@P_GI        			NVARCHAR(1),        --출고여부      
	@P_IV        			NVARCHAR(1),        --매출여부      
	@P_TRADE        		NVARCHAR(1),        --수출여부      
	@P_TP_VAT       		NVARCHAR(3),        --과세구분      
	@P_RT_VAT				NUMERIC(5, 2),      --부가세율        
	@P_GI_PARTNER   		NVARCHAR(20),		--납품처        
	@P_ID_INSERT    		NVARCHAR(15),		--사용자ID      
	@P_NO_PROJECT   		NVARCHAR(20),		--프로젝트    
	@P_SEQ_PROJECT  		NUMERIC(5),			--프로젝트항번    
	@P_CD_ITEM_PARTNER		NVARCHAR(160),		--거래처품목    
	@P_NM_ITEM_PARTNER		NVARCHAR(50),		--거래처품목명    
	@P_DC1					NVARCHAR(200),           
	@P_DC2					NVARCHAR(200),     
	@P_UMVAT_SO				NUMERIC(19, 6),		--부가세포함단가    
	@P_AMVAT_SO				NUMERIC(17, 4), 	--부가세포함금액    
	@P_CD_SHOP				NVARCHAR(6),		--접수코드(쇼핑몰)
	@P_CD_SPITEM			NVARCHAR(40),		--상품코드
	@P_CD_OPT				NVARCHAR(40), 		--옵션코드
    @P_RT_DSCNT				NUMERIC(15, 4),		--할인율     
	@P_UM_BASE				NUMERIC(19, 6),     --기준단가
	
	/*** SA_SOL_DLV 테이블에 에 저장될 내역들 ***/
	@P_NM_CUST_DLV			NVARCHAR(30), 		--수취인
	@P_CD_ZIP				NVARCHAR(12), 		--우편번호
	@P_ADDR1				NVARCHAR(300), 		--주소1
	@P_ADDR2				NVARCHAR(200), 		--주소2(상세주소)
	@P_NO_TEL_D1			NVARCHAR(20), 		--전화(회사전화 나 집전화)
	@P_NO_TEL_D2			NVARCHAR(20), 		--이동전화(이동통신전화)
	@P_TP_DLV				NVARCHAR(3), 		--배송방법
	@P_DC_REQ				NVARCHAR(400),  	--비고 
	@P_FG_TRACK_L			NVARCHAR(5), 		--배송정보 TRACK 기능추가 => SO : 수주등록, M : 출고요청등록, R : 출고반품의뢰등록 
	@P_TP_DLV_DUE			NVARCHAR(4),	    --납품방법
	
	@P_FG_USE				NVARCHAR(4),        --수주용도
	@P_CD_CC				NVARCHAR(12),       
	@P_NO_IO_MGMT			NVARCHAR(20)	= NULL,	--장은경 : 2010.06.16 관련수불번호
	@P_NO_IOLINE_MGMT		NUMERIC(5,0)	= 0,	--장은경 : 2010.06.16 관련수불라인번호
	@P_NO_POLINE_PARTNER	NUMERIC(5,0)	= 0,	--장은경 : 2010.06.17 거래처PO항번 추가
	@P_UM_OPT				NUMERIC(15,4)	= 0,	--장은경 : 2010.07.20 특수단가 추가
	@P_NO_PO_PARTNER		NVARCHAR(50)	= NULL,	--장은경 : 2010.07.27 거래처PO번호 추가
	@P_CD_WH				NVARCHAR(7)		= NULL,	--심재희 : 2010.11.11 W/H 추가
	@P_NO_SO_ORIGINAL		NVARCHAR(20)	= NULL,	--서건수 : 2010.11.12 원천수주번호 추가             
	@P_SEQ_SO_ORIGINAL		NUMERIC(5,0)	= 0,	--서건수 : 2010.11.12 원천수주항번 추가
	@P_NUM_USERDEF1			NUMERIC(17,4)	= 0,	--심재희 : 2010.12.30 사용자정의금액1(한국로스트왁스 : 금형비)
	@P_NUM_USERDEF2			NUMERIC(17,4)	= 0,    --심재희 : 2010.12.30 사용자정의금액2(한국로스트왁스 : 개발비)
	@P_NUM_USERDEF3			NUMERIC(17,4)	= 0,	--심재희 : 2010.11.15 사용자정의금액3(조선호텔베이커리요청)
	@P_NUM_USERDEF4			NUMERIC(17,4)	= 0,    --심재희 : 2010.11.15 사용자정의금액4(조선호텔베이커리요청)
	@P_NUM_USERDEF5			NUMERIC(17,4)	= 0,	--심재희 : 2010.11.15 사용자정의금액5(조선호텔베이커리요청)
	@P_NUM_USERDEF6			NUMERIC(17,4)	= 0,	--심재희 : 2010.11.15 사용자정의금액6(조선호텔베이커리요청)	
	@P_CD_MNGD1             NVARCHAR(20)    = NULL, --PIMS:D20120227052
	@P_CD_MNGD2             NVARCHAR(20)    = NULL, --PIMS:D20120227052
	@P_CD_MNGD3             NVARCHAR(20)    = NULL, --PIMS:D20120227052
	@P_CD_MNGD4             NVARCHAR(20)    = NULL, --PIMS:D20120227052
    @P_TXT_USERDEF1         NVARCHAR(100)   = NULL,
	@P_TXT_USERDEF2         NVARCHAR(100)   = NULL,
	@P_YN_OPTION            NCHAR(1)        = NULL,
	@P_NO_ORDER             NVARCHAR(40)    = NULL, --오준회 : 2013.08.30 배송지정보 주문번호          (부강샘스요청)
	@P_NM_CUST              NVARCHAR(30)    = NULL, --오준회 : 2013.08.30 배송지정보 주문자            (부강샘스요청)
	@P_NO_TEL1              NVARCHAR(20)    = NULL, --오준회 : 2013.08.30 배송지정보 주문자전화번호    (부강샘스요청)
	@P_NO_TEL2              NVARCHAR(20)    = NULL, --오준회 : 2013.08.30 배송지정보 주문자이동전화번호(부강샘스요청)
	@P_DLV_TXT_USERDEF1     NVARCHAR(100)   = NULL, --오준회 : 2013.08.30 배송지정보 사용자정의텍스트1 (부강샘스요청)
	@P_CD_ITEM_REF          NVARCHAR(20)    = NULL, --오준회 : 2013.11.26 SET품목코드                  (시몬스)
    @P_YN_PICKING           NCHAR(1)	    = NULL, --오준회 : 2013.11.26 배차여부                     (시몬스)
    @P_DLV_CD_USERDEF1      NVARCHAR(3)     = NULL, --오준회 : 2013.11.26 배송지정보 사용자정의코드1   (시몬스)          
    @P_FG_USE2              NVARCHAR(4)     = NULL,
    @P_NO_LINK				NVARCHAR(20)	= NULL,
    @P_NO_LINE_LINK			NUMERIC(5,0)	= NULL,
    @P_NO_RELATION          NVARCHAR(20)    = NULL,
    @P_SEQ_RELATION         NUMERIC(5,0)	= NULL,
    @P_CD_USERDEF1          NVARCHAR(3)	    = NULL
)      
AS      
      
DECLARE @V_DTS_INSERT NVARCHAR(14), @P_FG_TRACK NVARCHAR(5)      
SET		@V_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')      

--수주의 FG_TRACK 이 (INDIRECT => I : 사전프로젝트에서 만들어준 수주 DATA는 업데이트 하거나 삭제 할수 없음)
SELECT @P_FG_TRACK = FG_TRACK FROM SA_SOH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_SO = @P_NO_SO 
      
IF (@P_FG_TRACK = 'I')      
BEGIN      
	--BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.      
	RAISERROR ('BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.', 18, 1)
	RETURN      
END

EXEC UP_SA_SO_HOSTORY_CHK @P_CD_COMPANY, @P_NO_SO
IF @@ERROR <> 0 RETURN 

INSERT INTO SA_SOL      
(      
	CD_COMPANY,			NO_SO,			    SEQ_SO,			    CD_PLANT,		    CD_ITEM,  
	UNIT_SO,			DT_DUEDATE,		    DT_REQGI,   	    QT_SO,			    UM_SO,   
	AM_SO,				AM_WONAMT,		    AM_VAT,			    UNIT_IM,		    QT_IM,       
	CD_SL,				TP_ITEM,		    STA_SO,			    TP_BUSI,		    TP_GI,      
	TP_IV,				GIR,                GI,                 IV,                 TRADE,		
	TP_VAT,			    RT_VAT,             GI_PARTNER,		    ID_INSERT,          NO_HST,			    
	DTS_INSERT,		    NO_PROJECT, 	    SEQ_PROJECT,	    CD_ITEM_PARTNER,    NM_ITEM_PARTNER,	
	DC1,			    DC2,			    UMVAT_SO,		    AMVAT_SO,           CD_SHOP,            
	CD_SPITEM,		    CD_OPT,             RT_DSCNT,           UM_BASE,        	FG_USE, 
	CD_CC,              NO_IO_MGMT,         NO_IOLINE_MGMT,		NO_POLINE_PARTNER,	UM_OPT,
	NO_PO_PARTNER,		CD_WH,				NO_SO_ORIGINAL,		SEQ_SO_ORIGINAL,	NUM_USERDEF1,
	NUM_USERDEF2,       NUM_USERDEF3,       NUM_USERDEF4,       NUM_USERDEF5,       NUM_USERDEF6,
	CD_MNGD1,           CD_MNGD2,           CD_MNGD3,           CD_MNGD4,           TXT_USERDEF1,
	TXT_USERDEF2,       YN_OPTION,          CD_ITEM_REF,        YN_PICKING,         FG_USE2,
	NO_RELATION,        SEQ_RELATION,       CD_USERDEF1
)      
VALUES      
(      
	@P_CD_COMPANY,		@P_NO_SO,		    @P_SEQ_SO,		    @P_CD_PLANT,	    @P_CD_ITEM, 
	@P_UNIT_SO,			@P_DT_DUEDATE,      @P_DT_REQGI,	    @P_QT_SO,		    @P_UM_SO,   
	@P_AM_SO,			@P_AM_WONAMT,       @P_AM_VAT,		    @P_UNIT_IM,		    @P_QT_IM,  
	@P_CD_SL,			@P_TP_ITEM,		    @P_STA_SO,		    @P_TP_BUSI,		    @P_TP_GI,  
	@P_TP_IV,			@P_GIR,             @P_GI,              @P_IV,              @P_TRADE,	
	@P_TP_VAT,		    @P_RT_VAT,          @P_GI_PARTNER,      @P_ID_INSERT,       '0',				
	@V_DTS_INSERT,	    @P_NO_PROJECT,      @P_SEQ_PROJECT,     @P_CD_ITEM_PARTNER, @P_NM_ITEM_PARTNER, 
	@P_DC1,			    @P_DC2,			    @P_UMVAT_SO,	    @P_AMVAT_SO ,       @P_CD_SHOP,         
	@P_CD_SPITEM,	    @P_CD_OPT,          @P_RT_DSCNT,        @P_UM_BASE,     	@P_FG_USE, 
	@P_CD_CC,           @P_NO_IO_MGMT,      @P_NO_IOLINE_MGMT,  @P_NO_POLINE_PARTNER, @P_UM_OPT,
	@P_NO_PO_PARTNER,	@P_CD_WH,			@P_NO_SO_ORIGINAL,	@P_SEQ_SO_ORIGINAL,	@P_NUM_USERDEF1,
	@P_NUM_USERDEF2,    @P_NUM_USERDEF3,    @P_NUM_USERDEF4,    @P_NUM_USERDEF5,    @P_NUM_USERDEF6,
	@P_CD_MNGD1,        @P_CD_MNGD2,        @P_CD_MNGD3,        @P_CD_MNGD4,        @P_TXT_USERDEF1,
	@P_TXT_USERDEF2,    @P_YN_OPTION,       @P_CD_ITEM_REF,     @P_YN_PICKING,      @P_FG_USE2,
	@P_NO_RELATION,     @P_SEQ_RELATION,    @P_CD_USERDEF1
)      

/*** SA_SOL_DLV 테이블에 에 저장될 내역들 ***/
INSERT INTO SA_SOL_DLV      
(      
	CD_COMPANY,			NO_SO,			SEQ_SO,			     NM_CUST_DLV, 		CD_ZIP, 
	ADDR1,				ADDR2,			NO_TEL_D1,		     NO_TEL_D2,			TP_DLV, 
	DC_REQ,				FG_TRACK,		TP_DLV_DUE,          NO_ORDER,          NM_CUST,
	NO_TEL1,            NO_TEL2,        TXT_USERDEF1,        CD_USERDEF1,		NO_LINK,	
	NO_LINE_LINK
)      
VALUES      
(      
	@P_CD_COMPANY,		@P_NO_SO,		@P_SEQ_SO,		     @P_NM_CUST_DLV,    @P_CD_ZIP, 
	@P_ADDR1,			@P_ADDR2,		@P_NO_TEL_D1,	     @P_NO_TEL_D2,		@P_TP_DLV, 
	@P_DC_REQ,			@P_FG_TRACK_L,	@P_TP_DLV_DUE,       @P_NO_ORDER,       @P_NM_CUST,
	@P_NO_TEL1,         @P_NO_TEL2,     @P_DLV_TXT_USERDEF1, @P_DLV_CD_USERDEF1,@P_NO_LINK,
	@P_NO_LINE_LINK 
)


-- 자동프로세스 여부
EXEC SP_CZ_SA_GIR_PROCESS_I @P_SERVERKEY, 'SA_SOL', @P_CD_COMPANY, @P_NO_SO, @P_SEQ_SO, @P_ID_INSERT

EXEC SP_CZ_SA_GI_PROCESS_I @P_SERVERKEY, 'SA_SOL', @P_CD_COMPANY, @P_NO_SO, @P_SEQ_SO, @P_ID_INSERT

EXEC SP_CZ_SA_IV_PROCESS_I @P_SERVERKEY, 'SA_SOL', @P_CD_COMPANY, @P_NO_SO, @P_SEQ_SO, @P_ID_INSERT
GO

