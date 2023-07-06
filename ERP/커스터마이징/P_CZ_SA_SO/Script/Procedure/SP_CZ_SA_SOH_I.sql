USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SOH_I]    Script Date: 2015-10-01 오후 2:19:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************************************/ 
--설    명 : 수주등록(패키지) P_SA_SO
--수 정 자 : 장은경
--수 정 일 : 2010/07/08
--유    형 : 추가
--내    역 : 추가
--           기존 UP_SA_SOH_INSERT 에서 UP_SA_SOH_I로 변경
--			 나머지 관련화면들은 UP_SA_SOH_INSERT 로
/*********************************************************************************************/ 
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOH_I]      
(      
	@P_CD_COMPANY		NVARCHAR(7),    	--회사      
	@P_NO_SO			NVARCHAR(20),   	--수주번호      
	@P_CD_BIZAREA		NVARCHAR(7),    	--사업장      
	@P_DT_SO			NVARCHAR(8),    	--수주일자      
	@P_CD_PARTNER		NVARCHAR(20),    	--거래처      
	@P_CD_SALEGRP		NVARCHAR(7),    	--영업그룹      
	@P_NO_EMP			NVARCHAR(10),   	--사번      
	@P_TP_SO			NVARCHAR(4),		--수주유형      
	@P_CD_EXCH			NVARCHAR(3),		--환종(화폐단위)      
	@P_RT_EXCH			NUMERIC(15, 4), 	--환율(화폐단위)      
	@P_TP_PRICE			NVARCHAR(3),		--단가유형      
	@P_NO_PROJECT		NVARCHAR(20),		--프로젝트      
	@P_TP_VAT			NVARCHAR(3),		--과세구분(VAT구분)      
	@P_RT_VAT			NUMERIC(5, 2),  	--부가세율      
	@P_FG_VAT			NVARCHAR(3),		--부가세포함여부      
	@P_FG_TAXP			NVARCHAR(3),		--계산서처리방법      
	@P_DC_RMK			NVARCHAR(100),  	--비고      
	@P_FG_BILL			NVARCHAR(3),		--결재방법      
	@P_FG_TRANSPORT		NVARCHAR(3),		--운송방법      
	@P_NO_CONTRACT		NVARCHAR(40),		--계약번호      
	@P_STA_SO			NVARCHAR(3),		--수주상태      
	@P_FG_TRACK			NVARCHAR(5),		--수주등록 추적컬럼      
	@P_NO_PO_PARTNER	NVARCHAR(50),		--거래처발주번호
	
	@P_NO_EST			NVARCHAR(20),		--통합견적번호	
	@P_NO_EST_HST		NUMERIC(3, 0),		--통합견적차수
	@P_CD_EXPORT		NVARCHAR(20),		--수출자(거래처)
	@P_CD_PRODUCT		NVARCHAR(20),		--제조자(거래처)(송장table에 있슴)
	@P_COND_TRANS		NVARCHAR(50),		--인도조건(송장table에 있슴)
	@P_COND_PAY			NVARCHAR(3),		--결제형태(TR_IM00004)
	@P_COND_DAYS		NUMERIC(4, 0),		--결제일	
	@P_TP_PACKING		NVARCHAR(3),		--포장형태(TR_IM00011)
	@P_TP_TRANS			NVARCHAR(3),		--운송방법(TR_IM00008)
	@P_TP_TRANSPORT		NVARCHAR(3),		--운송형태(TR_IM00009)
	@P_NM_INSPECT		NVARCHAR(50),		--검사  (key-in)
	@P_PORT_LOADING		NVARCHAR(50),		--선적항(key-in)
	@P_PORT_ARRIVER		NVARCHAR(50),		--도착항(key-in)
	@P_CD_ORIGIN		NVARCHAR(3),		--원산지(MA_B000020)
	@P_DESTINATION		NVARCHAR(50),		--목적지(key-in)
	@P_DT_EXPIRY		NVARCHAR(8),		--유효일자
	@P_CD_NOTIFY		NVARCHAR(20),		--착하통지처(거래처)
	@P_CD_CONSIGNEE		NVARCHAR(20),		--수하인(거래처)
	@P_CD_TRANSPORT		NVARCHAR(20),		--운송사
	@P_DC_RMK_TEXT		TEXT,				--헤더멀티비고
	@P_ID_INSERT		NVARCHAR(15),		--사용자ID
	@P_RMA_REASON		NVARCHAR(4),        --반품사유
	@P_DC_RMK1          NVARCHAR(100),      --비고1
	@P_COND_PRICE       NVARCHAR(3),        --가격조건
	@P_DT_USERDEF1      NVARCHAR(8)     = NULL, --날짜사용자정의1
	@P_DT_USERDEF2      NVARCHAR(8)     = NULL, --날짜사용자정의2
	@P_TXT_USERDEF1     NVARCHAR(100)   = NULL, --텍스트사용자정의1
	@P_TXT_USERDEF2     NVARCHAR(100)   = NULL, --텍스트사용자정의2
	@P_TXT_USERDEF3     NVARCHAR(100)   = NULL, --텍스트사용자정의3
	@P_CD_USERDEF1      NVARCHAR(4)     = NULL, --코드사용자정의1
	@P_CD_USERDEF2      NVARCHAR(4)     = NULL, --코드사용자정의2
	@P_CD_USERDEF3      NVARCHAR(4)     = NULL, --코드사용자정의3
	@P_NUM_USERDEF1     NUMERIC(17, 4)  = 0,    --숫자사용자정의1
	@P_NUM_USERDEF2     NUMERIC(17, 4)  = 0,    --숫자사용자정의2
	@P_NUM_USERDEF3     NUMERIC(17, 4)  = 0,    --숫자사용자정의3
	@P_NUM_USERDEF4     NUMERIC(17, 4)  = 0,    --숫자사용자정의4
	@P_NUM_USERDEF5     NUMERIC(17, 4)  = 0,    --숫자사용자정의5
	@P_CD_BANK_SO		NVARCHAR(20)	= NULL, --수출TAB(은행)
	@P_DC_RMK_TEXT2     NVARCHAR(4000)	= NULL, --비고TAB(비고2) 
	@P_NO_INV		    NVARCHAR(20)	= NULL, --송장번호
	@P_TXT_USERDEF4     NVARCHAR(200)   = NULL, --텍스트사용자정의4
	@P_NO_IMO			NVARCHAR(10)	= NULL, --IMO 번호
	@P_CD_CHANCE        NVARCHAR(20)    = NULL  --영업기회번호
)      
AS      
DECLARE @V_SYSDATE		VARCHAR(14)
SET @V_SYSDATE = NEOE.SF_SYSDATE(GETDATE())		  
    
INSERT INTO SA_SOH      
(      
	CD_COMPANY,		    NO_SO,			    CD_BIZAREA,			DT_SO,				CD_PARTNER,		      
	CD_SALEGRP,		    NO_EMP,			    TP_SO,				CD_EXCH,			RT_EXCH,	      
	TP_PRICE,		    NO_PROJECT,		    TP_VAT,				RT_VAT,				FG_VAT,			
	FG_TAXP,		    DC_RMK,			    FG_BILL,			FG_TRANSPORT,		NO_CONTRACT,	   
	STA_SO,			    FG_TRACK,		    NO_PO_PARTNER,		NO_HST,				
	NO_EST,			    NO_EST_HST,		    CD_EXPORT,			CD_PRODUCT,			COND_TRANS,
	COND_PAY,		    COND_DAYS,		    TP_PACKING,			TP_TRANS,			TP_TRANSPORT,
	NM_INSPECT,		    PORT_LOADING,	    PORT_ARRIVER,		CD_ORIGIN,			DESTINATION,
	DT_EXPIRY,		    CD_NOTIFY,		    CD_CONSIGNEE,		CD_TRANSPORT,		DC_RMK_TEXT,
	ID_INSERT,		    DTS_INSERT,		    RMA_REASON,         DC_RMK1,            COND_PRICE,
	DT_USERDEF1,        DT_USERDEF2,        TXT_USERDEF1,       TXT_USERDEF2,       TXT_USERDEF3,
	CD_USERDEF1,        CD_USERDEF2,        CD_USERDEF3,        NUM_USERDEF1,       NUM_USERDEF2,
	NUM_USERDEF3,       NUM_USERDEF4,       NUM_USERDEF5,		CD_BANK_SO,         DC_RMK_TEXT2,
	NO_INV,             TXT_USERDEF4,       NO_IMO,				CD_CHANCE			
)      
VALUES      
(      
	@P_CD_COMPANY,	    @P_NO_SO,           @P_CD_BIZAREA,		@P_DT_SO,			@P_CD_PARTNER, 
	@P_CD_SALEGRP,      @P_NO_EMP,		    @P_TP_SO,			@P_CD_EXCH,			@P_RT_EXCH,  
	@P_TP_PRICE,	    @P_NO_PROJECT,      @P_TP_VAT,			@P_RT_VAT,			@P_FG_VAT,  
	@P_FG_TAXP,		    @P_DC_RMK,		    @P_FG_BILL,			@P_FG_TRANSPORT,	@P_NO_CONTRACT, 
	@P_STA_SO,		    @P_FG_TRACK,        @P_NO_PO_PARTNER,   0,
	@P_NO_EST,		    @P_NO_EST_HST,	    @P_CD_EXPORT,		@P_CD_PRODUCT,		@P_COND_TRANS,
	@P_COND_PAY,	    @P_COND_DAYS,	    @P_TP_PACKING,		@P_TP_TRANS,		@P_TP_TRANSPORT,
	@P_NM_INSPECT,	    @P_PORT_LOADING,    @P_PORT_ARRIVER,	@P_CD_ORIGIN,		@P_DESTINATION,
	@P_DT_EXPIRY,	    @P_CD_NOTIFY,	    @P_CD_CONSIGNEE,	@P_CD_TRANSPORT,	@P_DC_RMK_TEXT,		
	@P_ID_INSERT,	    @V_SYSDATE,		    @P_RMA_REASON,      @P_DC_RMK1,         @P_COND_PRICE,
	@P_DT_USERDEF1,     @P_DT_USERDEF2,     @P_TXT_USERDEF1,    @P_TXT_USERDEF2,    @P_TXT_USERDEF3,
	@P_CD_USERDEF1,     @P_CD_USERDEF2,     @P_CD_USERDEF3,     @P_NUM_USERDEF1,    @P_NUM_USERDEF2,
	@P_NUM_USERDEF3,    @P_NUM_USERDEF4,    @P_NUM_USERDEF5,	@P_CD_BANK_SO,      @P_DC_RMK_TEXT2,
	@P_NO_INV,          @P_TXT_USERDEF4,    @P_NO_IMO,			@P_CD_CHANCE
)
GO

