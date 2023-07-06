USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SOH_INSERT]    Script Date: 2019-11-11 오후 4:01:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************************************************                    
*******************************************************************************/          
/*******************************************************************************    
 *******************************************************************************/       
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOH_BATCH_I_WINTEC]      
(      
	@P_CD_COMPANY		NVARCHAR(7),    		--회사      
	@P_NO_SO			NVARCHAR(20),   		--수주번호      
	@P_CD_BIZAREA		NVARCHAR(7),    		--사업장      
	@P_DT_SO			NVARCHAR(8),    		--수주일자      
	@P_CD_PARTNER		NVARCHAR(20),    		--거래처      
	@P_CD_SALEGRP		NVARCHAR(7),    		--영업그룹      
	@P_NO_EMP			NVARCHAR(10),   		--사번      
	@P_TP_SO			NVARCHAR(4),			--수주유형      
	@P_CD_EXCH			NVARCHAR(3),			--환종(화폐단위)      
	@P_RT_EXCH			NUMERIC(15, 4), 		--환율(화폐단위)      
	@P_TP_PRICE			NVARCHAR(3),			--단가유형      
	@P_NO_PROJECT		NVARCHAR(20),			--프로젝트      
	@P_TP_VAT			NVARCHAR(3),			--과세구분(VAT구분)      
	@P_RT_VAT			NUMERIC(5, 2),  		--부가세율      
	@P_FG_VAT			NVARCHAR(3),			--부가세포함여부      
	@P_FG_TAXP			NVARCHAR(3),			--계산서처리방법      
	@P_DC_RMK			NVARCHAR(100),  		--비고      
	@P_FG_BILL			NVARCHAR(3),			--결재방법      
	@P_FG_TRANSPORT		NVARCHAR(3),			--운송방법      
	@P_NO_CONTRACT		NVARCHAR(40),			--계약번호      
	@P_STA_SO			NVARCHAR(3),			--수주상태      
	@P_FG_TRACK			NVARCHAR(5),			--수주등록 추적컬럼      
	@P_NO_PO_PARTNER	NVARCHAR(50),			--거래처발주번호
	@P_ID_INSERT		NVARCHAR(15),			--사용자ID
	@P_RMA_REASON       NVARCHAR(4) = '',		--반품사유(2011.06.22)
	@P_DC_RMK1			NVARCHAR(100) = '',		--헤더비고1(20140227)
	@P_TXT_USERDEF1     NVARCHAR(100) = NULL,	--거래처공정
	@P_NUM_USERDEF1     NUMERIC(17, 4) = 0,		--인수자
	@P_NUM_USERDEF2     NUMERIC(17, 4) = 0,		--거래처구매담당자
	@P_NUM_USERDEF3     NUMERIC(17, 4) = 0,		--거래처설계담당자
	@P_NO_NEGO			NVARCHAR(20) = NULL		--입고처
)      
AS      

DECLARE @V_DTS_INSERT NVARCHAR(14)      
SET		@V_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')      
  
--수주의 FG_TRACK 이 (INDIRECT => I : 사전프로젝트에서 만들어준 수주 DATA는 업데이트 하거나 삭제 할수 없음)
SELECT @P_FG_TRACK = FG_TRACK FROM SA_SOH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_SO = @P_NO_SO 
      
IF (@P_FG_TRACK = 'I')      
BEGIN      
	--BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.      
	RAISERROR ('BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.'      , 18, 1 )
	RETURN      
END
    
INSERT INTO SA_SOH      
(      
	CD_COMPANY,			NO_SO,				CD_BIZAREA,			DT_SO,				CD_PARTNER,		      
	CD_SALEGRP,			NO_EMP,				TP_SO,				CD_EXCH,			RT_EXCH,	      
	TP_PRICE,			NO_PROJECT,			TP_VAT,				RT_VAT,				FG_VAT,			
	FG_TAXP,			DC_RMK,				FG_BILL,			FG_TRANSPORT,		NO_CONTRACT,	   
	STA_SO,				FG_TRACK,			NO_PO_PARTNER,		NO_HST,				ID_INSERT,		
	DTS_INSERT,			RMA_REASON,			DC_RMK1,			TXT_USERDEF1,		NUM_USERDEF1,		
	NUM_USERDEF2,		NUM_USERDEF3,		NO_NEGO
)      
VALUES      
(      
	@P_CD_COMPANY,		@P_NO_SO,			@P_CD_BIZAREA,		@P_DT_SO,			@P_CD_PARTNER, 
	@P_CD_SALEGRP,		@P_NO_EMP,			@P_TP_SO,			@P_CD_EXCH,			@P_RT_EXCH,  
	@P_TP_PRICE,		@P_NO_PROJECT,		@P_TP_VAT,			@P_RT_VAT,			@P_FG_VAT,  
	@P_FG_TAXP,			@P_DC_RMK,			@P_FG_BILL,			@P_FG_TRANSPORT,	@P_NO_CONTRACT, 
	@P_STA_SO,			@P_FG_TRACK,		@P_NO_PO_PARTNER,   '0',				@P_ID_INSERT,	
	@V_DTS_INSERT,		@P_RMA_REASON,		@P_DC_RMK1,			@P_TXT_USERDEF1,    @P_NUM_USERDEF1,	
	@P_NUM_USERDEF2,	@P_NUM_USERDEF3,    @P_NO_NEGO
)
GO

