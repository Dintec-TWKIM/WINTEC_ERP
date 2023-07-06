USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SOH_U]    Script Date: 2019-11-04 오전 10:36:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************************************/ 
/*********************************************************************************************/ 
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOH_U_WINTEC]      
(      
	@P_CD_COMPANY		NVARCHAR(7),        --회사      
	@P_NO_SO			NVARCHAR(20),       --수주번호                      
	@P_DT_SO			NVARCHAR(8),        --수주일자      
	@P_CD_PARTNER		NVARCHAR(20),       --거래처      
	@P_CD_SALEGRP		NVARCHAR(7),        --영업그룹      
	@P_NO_EMP			NVARCHAR(10),       --사번      
	@P_CD_EXCH			NVARCHAR(3),        --환종(화폐단위)      
	@P_RT_EXCH			NUMERIC(15, 4),     --환율(화폐단위)      
	@P_TP_PRICE			NVARCHAR(3),        --단가유형      
	@P_NO_PROJECT		NVARCHAR(20),       --프로젝트      
	@P_TP_VAT			NVARCHAR(3),        --과세구분(VAT구분)      
	@P_FG_VAT			NVARCHAR(3),        --부가세포함여부      
	@P_FG_TAXP			NVARCHAR(3),        --계산서처리방법      
	@P_DC_RMK			NVARCHAR(100),		--비고      
	@P_FG_BILL			NVARCHAR(3),		--결재방법      
	@P_FG_TRANSPORT		NVARCHAR(3),		--운송방법      
	@P_NO_CONTRACT		NVARCHAR(40),		--계약번호      
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
	@P_DC_RMK_TEXT		NTEXT,				--멀티비고(2011.01.05 SJH)
	@P_ID_UPDATE		NVARCHAR(15),		--사용자ID
	@P_RMA_REASON		NVARCHAR(4),        --반품사유      
	@P_DC_RMK1          NVARCHAR(100),      --비고1
	@P_COND_PRICE       NVARCHAR(3),        --가격조건
	@P_DT_USERDEF1      NVARCHAR(8),        --날짜사용자정의1
	@P_DT_USERDEF2      NVARCHAR(8),        --날짜사용자정의2
	@P_TXT_USERDEF1     NVARCHAR(200),      --거래처공정
	@P_TXT_USERDEF2     NVARCHAR(100),      --텍스트사용자정의2
	@P_TXT_USERDEF3     NVARCHAR(100),      --텍스트사용자정의3
	@P_CD_USERDEF1      NVARCHAR(4),        --코드사용자정의1
	@P_CD_USERDEF2      NVARCHAR(4),        --코드사용자정의2
	@P_CD_USERDEF3      NVARCHAR(4),        --코드사용자정의3
	@P_NUM_USERDEF1     NUMERIC(17, 4),     --인수자
	@P_NUM_USERDEF2     NUMERIC(17, 4),     --거래처구매담당자
	@P_NUM_USERDEF3     NUMERIC(17, 4),     --거래처설계담당자
	@P_NUM_USERDEF4     NUMERIC(17, 4),     --숫자사용자정의4
	@P_NUM_USERDEF5     NUMERIC(17, 4),     --숫자사용자정의5
	@P_CD_BANK_SO		NVARCHAR(20),		--수출TAB(은행)
	@P_DC_RMK_TEXT2     NVARCHAR(4000),	    --비고TAB(비고2) 
	@P_NO_INV		    NVARCHAR(20)	= NULL,  --송장번호
	@P_TXT_USERDEF4     NVARCHAR(200)   = NULL,  --텍스트사용자정의4
	@P_CD_CHANCE        NVARCHAR(20)    = NULL,   --영업기회번호
	@P_TP_SO			NVARCHAR(4)		= NULL,
	@P_RT_VAT			NUMERIC(5, 2)	= 0,
	@P_STA_SO			NVARCHAR(3)		= NULL,
	@P_NO_NEGO			NVARCHAR(20)	= NULL  --입고처
	
)      
AS      
DECLARE @V_SYSDATE		VARCHAR(14),
		@V_STA_SO		INT

SET @V_SYSDATE = NEOE.SF_SYSDATE(GETDATE())	      

--수주상태가 진행이 아닌거만            
SELECT	@V_STA_SO = COUNT(*) 
FROM	SA_SOL 
WHERE	CD_COMPANY = @P_CD_COMPANY AND NO_SO = @P_NO_SO AND STA_SO <> 'O' 

IF (@V_STA_SO > 0)   
BEGIN
	RAISERROR ('이미 수주확정, 종결 되어 수정 또는 삭제가 불가능합니다.', 18, 1)
	RETURN      
END  
ELSE
BEGIN
	UPDATE  SA_SOH      
	   SET  DT_SO           = @P_DT_SO,      
			CD_PARTNER      = @P_CD_PARTNER,      
			CD_SALEGRP      = @P_CD_SALEGRP,      
			NO_EMP          = @P_NO_EMP,      
			CD_EXCH         = @P_CD_EXCH,      
			RT_EXCH         = @P_RT_EXCH,      
			TP_PRICE        = @P_TP_PRICE,      
			NO_PROJECT      = @P_NO_PROJECT,      
			TP_VAT          = @P_TP_VAT,      
			FG_VAT          = @P_FG_VAT,      
			FG_TAXP         = @P_FG_TAXP,      
			DC_RMK          = @P_DC_RMK,      
			FG_BILL			= @P_FG_BILL,      
			FG_TRANSPORT	= @P_FG_TRANSPORT,      
			NO_CONTRACT		= @P_NO_CONTRACT,      
			NO_PO_PARTNER	= @P_NO_PO_PARTNER, 
			NO_EST			= @P_NO_EST,
			NO_EST_HST		= @P_NO_EST_HST,
			CD_EXPORT		= @P_CD_EXPORT,
			CD_PRODUCT		= @P_CD_PRODUCT,
			COND_TRANS		= @P_COND_TRANS,
			COND_PAY		= @P_COND_PAY,
			COND_DAYS		= @P_COND_DAYS,
			TP_PACKING		= @P_TP_PACKING,
			TP_TRANS		= @P_TP_TRANS,
			TP_TRANSPORT	= @P_TP_TRANSPORT,
			NM_INSPECT		= @P_NM_INSPECT,
			PORT_LOADING	= @P_PORT_LOADING,
			PORT_ARRIVER	= @P_PORT_ARRIVER,
			CD_ORIGIN		= @P_CD_ORIGIN,
			DESTINATION		= @P_DESTINATION,
			DT_EXPIRY		= @P_DT_EXPIRY,
			CD_NOTIFY		= @P_CD_NOTIFY,
			CD_CONSIGNEE	= @P_CD_CONSIGNEE,		     
			CD_TRANSPORT	= @P_CD_TRANSPORT,
			DC_RMK_TEXT		= @P_DC_RMK_TEXT,
			ID_UPDATE       = @P_ID_UPDATE,              
			DTS_UPDATE      = @V_SYSDATE,
			RMA_REASON		= @P_RMA_REASON,
			DC_RMK1         = @P_DC_RMK1,
			COND_PRICE      = @P_COND_PRICE,
            DT_USERDEF1     = @P_DT_USERDEF1, 
            DT_USERDEF2     = @P_DT_USERDEF2, 
            TXT_USERDEF1    = @P_TXT_USERDEF1,
            TXT_USERDEF2    = @P_TXT_USERDEF2,
            TXT_USERDEF3    = @P_TXT_USERDEF3,
            CD_USERDEF1     = @P_CD_USERDEF1, 
            CD_USERDEF2     = @P_CD_USERDEF2, 
            CD_USERDEF3     = @P_CD_USERDEF3, 
            NUM_USERDEF1    = @P_NUM_USERDEF1,
            NUM_USERDEF2    = @P_NUM_USERDEF2,
            NUM_USERDEF3    = @P_NUM_USERDEF3,
            NUM_USERDEF4    = @P_NUM_USERDEF4,
            NUM_USERDEF5    = @P_NUM_USERDEF5,
            CD_BANK_SO		= @P_CD_BANK_SO,
            DC_RMK_TEXT2    = @P_DC_RMK_TEXT2,
            NO_INV			= @P_NO_INV,
            TXT_USERDEF4    = @P_TXT_USERDEF4,
            CD_CHANCE       = @P_CD_CHANCE,
            TP_SO			= @P_TP_SO,
            RT_VAT			= @P_RT_VAT,
            STA_SO			= @P_STA_SO,
			NO_NEGO			= @P_NO_NEGO
	 WHERE  CD_COMPANY      = @P_CD_COMPANY      
	   AND  NO_SO           = @P_NO_SO
END	   
   
EXEC UP_SA_SO_HOSTORY_CHK @P_CD_COMPANY, @P_NO_SO
IF @@ERROR <> 0 RETURN
GO

