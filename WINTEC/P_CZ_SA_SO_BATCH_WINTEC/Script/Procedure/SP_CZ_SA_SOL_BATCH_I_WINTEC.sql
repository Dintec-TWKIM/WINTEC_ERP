USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SOL_INSERT]    Script Date: 2019-11-11 오후 4:01:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************************************************                    
 제  목 : 수주라인 등록 
 작성일 : 2008.12.08          
 작성자 : 정남진              
 화면명 : UP_SA_SOL_INSERT    
            
 EXEC UP_SA_SOL_INSERT            
*******************************************************************************/          
/*******************************************************************************    
 * 사용화면 : 나중에 쿼리를 분리하거나 수정내역을 반영해줘야 하기때문에 사용한 화면을 저장해둔다.   

 * 수주등록 화면명				: FG_TRACK  : 관리자      : 본 쿼리 사용여부
     
 * 1.수주등록					: M			: NJin   관리 : 사용
 * 2.수주등록용역				: YV		: NJin   관리 : 사용
 * 3.수주등록(거래처)			: P			: NJin   관리 : 사용
 * 4.일괄수주등록				: ME		: NJin   관리 : 실재 사용안함
 * 5.일괄수주등록(부가세포함)	: MEV		: NJin   관리 : 실재 사용안함
 * 6.일괄수주등록(부가세포함)02 : MEV02		: NJin   관리 : 실재 사용안함
 * 7.일괄수주등록(부가세포함)EC : MEVEC		: NJin   관리 : 실재 사용안함
 * 8.수주이력등록				: H			: NJin   관리 : 미개발
 * 9.수주웹등록					: W			: 장기주 관리 : 실재 사용안함
 *******************************************************************************/     
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOL_BATCH_I_WINTEC]      
(      
	@P_CD_COMPANY   		NVARCHAR(7),		    --회사      
	@P_NO_SO        		NVARCHAR(20),		    --수주번호              
	@P_SEQ_SO       		NUMERIC(5),             --수주항번      
	@P_CD_PLANT     		NVARCHAR(7),            --공장      
	@P_CD_ITEM      		NVARCHAR(50),		    --품목      
	@P_UNIT_SO      		NVARCHAR(3),            --단위
	@P_DT_EXPECT			NVARCHAR(8),			--최초납기요구일
	@P_DT_DUEDATE   		NVARCHAR(8),            --납기요구일      
	@P_DT_REQGI     		NVARCHAR(8),            --출하예정일      
	@P_QT_SO        		NUMERIC(17, 4),         --수량      
	@P_UM_SO        		NUMERIC(19, 6),         --단가      
	@P_AM_SO        		NUMERIC(19, 6),         --금액      
	@P_AM_WONAMT    		NUMERIC(17, 4),         --원화금액      
	@P_AM_VAT       		NUMERIC(17, 4),         --부가세(원화)      
	@P_UNIT_IM      		NVARCHAR(3),            --재고단위      
	@P_QT_IM        		NUMERIC(17, 4),         --재고수량      
	@P_CD_SL        		NVARCHAR(7),            --창고      
	@P_TP_ITEM      		NVARCHAR(3),            --품목타입      
	@P_STA_SO       		NVARCHAR(3),            --수주상태      
	@P_TP_BUSI      		NVARCHAR(3),            --거래구분      
	@P_TP_GI        		NVARCHAR(3),            --출하형태      
	@P_TP_IV        		NVARCHAR(3),            --매출형태    
	@P_GIR        	    	NVARCHAR(1),            --의뢰여부      
	@P_GI        	    	NVARCHAR(1),            --출하여부      
	@P_IV        	    	NVARCHAR(1),            --매출여부      
	@P_TRADE        		NVARCHAR(1),            --수출여부      
	@P_TP_VAT       		NVARCHAR(3),            --과세구분      
	@P_RT_VAT           	NUMERIC(5, 2),          --부가세율        
	@P_GI_PARTNER   		NVARCHAR(20),           --납품처        
	@P_ID_INSERT    		NVARCHAR(15),		    --사용자ID      
	@P_NO_PROJECT   		NVARCHAR(20),		    --프로젝트    
	@P_SEQ_PROJECT  		NUMERIC(5),			    --프로젝트항번    
	@P_CD_ITEM_PARTNER  	NVARCHAR(160),		    --거래처품목    
	@P_NM_ITEM_PARTNER  	NVARCHAR(50),		    --거래처품목명    
	@P_DC1					NVARCHAR(200),		    --비고1           
	@P_DC2					NVARCHAR(200),		    --비고2
	@P_UMVAT_SO				NUMERIC(19, 6),		    --부가세포함단가    
	@P_AMVAT_SO				NUMERIC(17, 4), 	    --부가세포함금액    
	@P_CD_SHOP				NVARCHAR(6),		    --접수코드(쇼핑몰)
	@P_CD_SPITEM			NVARCHAR(40),		    --상품코드
	@P_CD_OPT				NVARCHAR(40), 		    --옵션코드
    @P_RT_DSCNT				NUMERIC(15, 4),         --할인율     
	@P_UM_BASE				NUMERIC(19, 6),         --기준단가
	
	-- SA_SOL_DLV 테이블에 저장될 내역들
	@P_NM_CUST_DLV			NVARCHAR(30), 		    --수취인
	@P_CD_ZIP				NVARCHAR(12), 		    --우편번호
	@P_ADDR1				NVARCHAR(300), 		    --주소1
	@P_ADDR2				NVARCHAR(200), 		    --주소2(상세주소)
	@P_NO_TEL_D1			NVARCHAR(20), 		    --전화(회사전화 나 집전화)
	@P_NO_TEL_D2			NVARCHAR(20), 		    --이동전화(이동통신전화)
	@P_TP_DLV				NVARCHAR(3), 		    --배송방법
	@P_DC_REQ				NVARCHAR(400),  	    --비고 
	@P_FG_TRACK_L			NVARCHAR(5), 		    --배송정보 TRACK 기능추가 => SO : 수주등록, M : 출고요청등록, R : 출하반품의뢰등록 
	@P_TP_DLV_DUE			NVARCHAR(4),	        --납품방법
	
	@P_FG_USE				NVARCHAR(4),            --수주용도
	@P_CD_CC				NVARCHAR(12),           --C/C코드
	@P_NO_IO_MGMT			NVARCHAR(20)  = NULL,   --장은경 : 2010.06.16 관련수불번호
	@P_NO_IOLINE_MGMT		NUMERIC(5,0)  = 0,	    --장은경 : 2010.06.16 관련수불라인번호
	@P_NO_POLINE_PARTNER	NUMERIC(5,0)  = 0,	    --장은경 : 2010.06.17 거래처PO항번 추가
	
	-- SA_SOL_DLV 테이블에 추가 저장될 내역들
	@P_NO_ORDER             NVARCHAR(40)  = NULL,   --주문번호
	@P_NM_CUST              NVARCHAR(30)  = NULL,   --주문자
	@P_NO_TEL1              NVARCHAR(20)  = NULL,   --주문자전화번호
	@P_NO_TEL2              NVARCHAR(20)  = NULL,   --주문자이동전화
	@P_TXT_USERDEF1         NVARCHAR(100) = NULL,   --사용자정의1
	
	@P_NO_PO_PARTNER		NVARCHAR(50)  = NULL,	--거래처PO번호 추가
	@P_FG_USE2				NVARCHAR(4)   = NULL,	--수주용도2
	@P_NO_RELATION          NVARCHAR(20)  = NULL,
	@P_SEQ_RELATION         NUMERIC(5,0)  = 0,
	@P_NUM_USERDEF1         NUMERIC(17,4) = 0,
	@P_NUM_USERDEF2         NUMERIC(17,4) = 0,
	@P_SOL_TXT_USERDEF1     NVARCHAR(100) = NULL,
	@P_SOL_TXT_USERDEF2     NVARCHAR(100) = NULL,
	@P_CD_MNGD1             NVARCHAR(20)  = NULL,
	@P_CD_MNGD2             NVARCHAR(20)  = NULL,
	@P_CD_MNGD3             NVARCHAR(20)  = NULL,
	@P_CD_MNGD4             NVARCHAR(20)  = NULL,
	@P_TXT_USERDEF3			NVARCHAR(200) = NULL, --자재번호
    @P_TXT_USERDEF4			NVARCHAR(100) = NULL, --도장COLOR
	@P_TXT_USERDEF5			NVARCHAR(100) = NULL, --납품장소
	@P_TXT_USERDEF6			NVARCHAR(100) = NULL, --호선번호
	@P_YN_OPTION			NVARCHAR(1)	  = NULL, --선급검사여부
	@P_CD_USERDEF1			NVARCHAR(4)   = NULL, --선급검사기관1
	@P_CD_USERDEF2			NVARCHAR(3)   = NULL, --선급검사기관2
	@P_CD_USERDEF3			NVARCHAR(3)   = NULL, --엔진타입
	@P_NO_IO				NVARCHAR(20)  = NULL
)      
    
AS   

DECLARE @V_DTS_INSERT NVARCHAR(14), 
		@P_FG_TRACK NVARCHAR(5), 
		@V_SERVER_KEY  NVARCHAR(50),
		@V_CD_SL NVARCHAR(7)

SET	@V_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')      

--수주의 FG_TRACK 이 (INDIRECT => I : 사전프로젝트에서 만들어준 수주 DATA는 업데이트 하거나 삭제 할수 없음)
SELECT @P_FG_TRACK = FG_TRACK FROM SA_SOH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_SO = @P_NO_SO 

IF (@P_FG_TRACK = 'I')      
BEGIN      
	--BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.      
	RAISERROR ('BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.', 18, 1)
	RETURN      
END

IF ISNULL(@P_CD_SL, '') = ''
BEGIN
	SET @V_CD_SL = (SELECT MI.CD_GISL 
					FROM MA_PITEM MI 
					WHERE MI.CD_COMPANY = @P_CD_COMPANY
					AND MI.CD_PLANT = @P_CD_PLANT
					AND MI.CD_ITEM = @P_CD_ITEM)
END
ELSE
BEGIN
	SET @V_CD_SL = @P_CD_SL
END

INSERT INTO SA_SOL      
(      
	CD_COMPANY,
	NO_SO,
	SEQ_SO,
	CD_PLANT,
	CD_ITEM,  
	UNIT_SO,
	DT_DUEDATE,
	DT_REQGI,
	QT_SO,
	UM_SO,   
	AM_SO,
	AM_WONAMT,
	AM_VAT,
	UNIT_IM,
	QT_IM,
	CD_SL,
	TP_ITEM,
	STA_SO,
	TP_BUSI,
	TP_GI,      
	TP_IV,
	GIR,
	GI,
	IV,
	TRADE,		
	TP_VAT,
	RT_VAT,
	GI_PARTNER,
	ID_INSERT,
	NO_HST,			    
	DTS_INSERT,
	NO_PROJECT,
	SEQ_PROJECT,
	CD_ITEM_PARTNER,
	NM_ITEM_PARTNER,	
	DC1,
	DC2,
	UMVAT_SO,
	AMVAT_SO,
	CD_SHOP,            
	CD_SPITEM,
	CD_OPT,
	RT_DSCNT,
	UM_BASE,
	FG_USE, 
	CD_CC,
	NO_IO_MGMT,
	NO_IOLINE_MGMT,
	NO_POLINE_PARTNER,
	NO_PO_PARTNER,
	FG_USE2,
	NO_RELATION,
	SEQ_RELATION,
	NUM_USERDEF1,
	NUM_USERDEF2,
	TXT_USERDEF1,
	TXT_USERDEF2,
	CD_MNGD1,
	CD_MNGD2,
	CD_MNGD3,             
	CD_MNGD4,
	TXT_USERDEF3,
	TXT_USERDEF4,
	TXT_USERDEF5,
	TXT_USERDEF6,
	DT_EXPECT,
	YN_OPTION,
	CD_USERDEF1,
	CD_USERDEF2,
	CD_USERDEF3
)      
VALUES      
(      
	@P_CD_COMPANY,
	@P_NO_SO,
	@P_SEQ_SO,
	@P_CD_PLANT,
	@P_CD_ITEM, 
	@P_UNIT_SO,
	@P_DT_DUEDATE,
	@P_DT_REQGI,
	@P_QT_SO,
	@P_UM_SO,   
	@P_AM_SO,
	@P_AM_WONAMT,
	@P_AM_VAT,
	@P_UNIT_IM,
	@P_QT_IM,  
	@V_CD_SL,
	@P_TP_ITEM,
	@P_STA_SO,
	@P_TP_BUSI,
	@P_TP_GI,  
	@P_TP_IV,
	@P_GIR,
	@P_GI,
	@P_IV,
	@P_TRADE,	
	@P_TP_VAT,
	@P_RT_VAT,
	@P_GI_PARTNER,
	@P_ID_INSERT,
	'0',				
	@V_DTS_INSERT,
	@P_NO_PROJECT,
	@P_SEQ_PROJECT,
	@P_CD_ITEM_PARTNER,
	@P_NM_ITEM_PARTNER, 
	@P_DC1,
	@P_DC2,
	@P_UMVAT_SO,
	@P_AMVAT_SO,
	@P_CD_SHOP,         
	@P_CD_SPITEM,
	@P_CD_OPT,
	@P_RT_DSCNT,
	@P_UM_BASE,
	@P_FG_USE, 
	(SELECT MAX(FC.CD_CC) AS CD_CC 
	 FROM MA_PITEM MI
	 LEFT JOIN FI_PUMOKCC FC ON FC.CD_COMPANY = MI.CD_COMPANY AND FC.CD_ITEMGRP = MI.GRP_ITEM
	 WHERE MI.CD_COMPANY = @P_CD_COMPANY
	 AND MI.CD_PLANT = @P_CD_PLANT
	 AND MI.CD_ITEM = @P_CD_ITEM),
	@P_NO_IO_MGMT,
	@P_NO_IOLINE_MGMT,
	@P_NO_POLINE_PARTNER,
	@P_NO_PO_PARTNER,
	@P_FG_USE2,
	@P_NO_RELATION,
	@P_SEQ_RELATION,
	@P_NUM_USERDEF1,
	@P_NUM_USERDEF2,
	@P_SOL_TXT_USERDEF1,
	@P_SOL_TXT_USERDEF2,
	@P_CD_MNGD1,
	@P_CD_MNGD2,
	@P_CD_MNGD3,          
	@P_CD_MNGD4,
	@P_TXT_USERDEF3,
	@P_TXT_USERDEF4,
	@P_TXT_USERDEF5,
	@P_TXT_USERDEF6,
	@P_DT_EXPECT,
	@P_YN_OPTION,
	@P_CD_USERDEF1,
	@P_CD_USERDEF2,
	@P_CD_USERDEF3
)      

/*** SA_SOL_DLV 테이블에 에 저장될 내역들 ***/
INSERT INTO SA_SOL_DLV      
(      
	CD_COMPANY,			NO_SO,			    SEQ_SO,			    NM_CUST_DLV, 		CD_ZIP, 
	ADDR1,				ADDR2,			    NO_TEL_D1,		    NO_TEL_D2,			TP_DLV, 
	DC_REQ,				FG_TRACK,		    TP_DLV_DUE,         NO_ORDER,           NM_CUST,
	NO_TEL1,            NO_TEL2,            TXT_USERDEF1
)      
VALUES      
(      
	@P_CD_COMPANY,		@P_NO_SO,			@P_SEQ_SO,			@P_NM_CUST_DLV,		@P_CD_ZIP, 
	@P_ADDR1,			@P_ADDR2,			@P_NO_TEL_D1,		@P_NO_TEL_D2,		@P_TP_DLV, 
	@P_DC_REQ,			@P_FG_TRACK_L,		@P_TP_DLV_DUE,		@P_NO_ORDER,		@P_NM_CUST,
	@P_NO_TEL1,			@P_NO_TEL2,			@P_TXT_USERDEF1
)

-- 자동프로세스 여부
EXEC UP_SA_GIR_PROCESS_I '', 'SA_SOL', @P_CD_COMPANY, @P_NO_SO, @P_SEQ_SO, @P_ID_INSERT

EXEC UP_SA_GI_PROCESS_I '', 'SA_SOL', @P_CD_COMPANY, @P_NO_SO, @P_SEQ_SO, 
					@P_ID_INSERT, 'N', NULL, NULL, 'Y', 
					NULL, NULL, NULL, NULL, NULL,
					NULL, @P_NO_IO

EXEC UP_SA_IV_PROCESS_I '', 'SA_SOL', @P_CD_COMPANY, @P_NO_SO, @P_SEQ_SO, @P_ID_INSERT

GO

