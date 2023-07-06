USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_POH_I]    Script Date: 2022-03-24 오후 3:29:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER  PROC [NEOE].[UP_PU_POH_I]                
(                
	 @P_NO_PO				NVARCHAR(20),                -- 발주번호                
	 @P_CD_COMPANY			NVARCHAR(7),                -- 회사                
	 @P_CD_PLANT			NVARCHAR(7),                -- 공장                
	 @P_CD_PARTNER			NVARCHAR(20),                -- 거래처                
	 @P_DT_PO				NVARCHAR(8),                -- 발주일                
	 @P_CD_PURGRP			NVARCHAR(7),                -- 구매그룹                
	 @P_NO_EMP				NVARCHAR(10),                -- 담당자                
	 @P_CD_TPPO				NVARCHAR(7),                -- 발주형태                
	 @P_FG_UM				NVARCHAR(3),                -- 단가유형                
	 @P_FG_PAYMENT			NVARCHAR(3),                -- 지급유형                
	 @P_FG_TAX				NVARCHAR(3),                -- 과세구분                
	 @P_TP_UM_TAX			NVARCHAR(3),                -- 부가세포함여부                
	 @P_CD_PJT				NVARCHAR(20),                -- 프로젝트                
	 @P_CD_EXCH				NVARCHAR(3),                -- 환종                
	 @P_RT_EXCH				NUMERIC(17,4),                -- 환율                
	 @P_AM_EX				NUMERIC(19,6),                -- 금액                
	 @P_AM					NUMERIC(17,4),                -- 원화금액                
	 @P_VAT					NUMERIC(17,4),                -- 부가세                
	 @P_DC50_PO     		NVARCHAR(400),                -- 비고                
	 @P_TP_PROCESS  		NVARCHAR(1),                                        
	 @P_FG_TAXP     		NVARCHAR(3),                --                
	 @P_YN_AM       		NVARCHAR(1),                -- 유무환구분                
	 @P_DTS_INSERT  		NVARCHAR(14),                -- 등록일                
	 @P_ID_INSERT   		NVARCHAR(15),                -- 등록자                
	 @P_FG_TRANS   			NVARCHAR(3),                 -- 거래구분                 
	 @P_FG_TRACK   			NVARCHAR(3) = 'M',            
	 @DC_RMK2      			NVARCHAR(200) = NULL,            
	 @TP_TRANSPORT 			NVARCHAR(3) = NULL,          
	 @COND_PAY     			NVARCHAR(3) = NULL,          
	 @COND_PAY_DLV 			NVARCHAR(20) = NULL,          
	 @COND_PRICE   			NVARCHAR(3) = NULL,          
	 @ARRIVER      			NVARCHAR(100) = NULL,          
	 @LOADING      			NVARCHAR(100) = NULL,        
	 @P_YN_BUDGET			NVARCHAR(1) = NULL,        
	 @P_BUDGET_PASS			NVARCHAR(1) = NULL,    
	 @P_COND_PRICE_DLV		NVARCHAR(100) = NULL,    
	 @P_CD_ARRIVER  		NVARCHAR(4) = NULL,     
	 @P_CD_LOADING  		NVARCHAR(4) = NULL,    
	 @P_DC_RMK_TEXT 		NTEXT      = NULL,     
	 @P_CD_AGENCY   		NVARCHAR(20) = NULL,
	 @P_AM_NEGO				NUMERIC(17,4) =0,
	 @P_COND_SHIPMENT		NVARCHAR(3) = NULL, 
	 @P_FREIGHT_CHARGE		NVARCHAR(3) = NULL,
	 @P_DC_RMK_TEXT2		NVARCHAR(4000) = NULL,
	 @P_CD_STND_PAY			NVARCHAR(4) = NULL,
	 @P_COND_DAYS			NUMERIC(4,0) = 0,
	 @P_CD_ORGIN			NVARCHAR(6) = NULL,
	 @P_DELIVERY_TERMS		NVARCHAR(100) = NULL,
	 @P_DELIVERY_TIME		NVARCHAR(100) = NULL,
	 @P_VALIDITY			NVARCHAR(100) = NULL,
	 @P_TP_PACKING			NVARCHAR(3) = NULL,
	 @P_DELIVERY_COST		NUMERIC(17,4) = 0,
	 @P_INSPECTION			NVARCHAR(100) = NULL,
     @P_DOCUMENT_REQUIRED   NVARCHAR(100) = NULL,
     @P_SUPPLIER			NVARCHAR(20) = NULL,
     @P_MANUFACTURER		NVARCHAR(20) = NULL,
     @P_NO_ORDER			NVARCHAR(200) = NULL,
     @P_NM_PACKING			NVARCHAR(100) = NULL,
     @P_SHIP_DATE           NVARCHAR(8) = NULL,
     @P_DACU_NO             NVARCHAR(20) = NULL,
     @P_TP_GR				NVARCHAR(3) = NULL,
     @P_DT_PROCESS_IV		NVARCHAR(8) = NULL,
     @P_DT_PAY_PRE_IV		NVARCHAR(8) = NULL,
     @P_DT_DUE_IV			NVARCHAR(8) = NULL,
     @P_FG_PAYBILL_IV		NVARCHAR(8) = NULL,
     @P_CD_DOCU_IV			NVARCHAR(8) = NULL,
     @P_AM_K_IV				NUMERIC(17,4) = 0,
     @P_VAT_TAX_IV			NUMERIC(17,4) = 0,
     @P_AM_EX_IV			NUMERIC(19,6) = 0,
     @P_TXT_USERDEF4		NVARCHAR(400) = NULL,
     @P_DC_RMK_IV			NVARCHAR(200) = NULL,
     @P_CD_USERDEF1			NVARCHAR(20) = '',
     @P_CD_USERDEF2			NVARCHAR(20) = '',
     @P_CD_USERDEF3			NVARCHAR(20) = '',
     @P_CD_USERDEF4			NVARCHAR(20) = '',
     @P_CD_BIZAREA_TAX		NVARCHAR(7)= '',
     @P_TXT_USERDEF3		NVARCHAR(400) = ''
)                 
AS                
DECLARE @P_ERRNO     INT,                
        @P_ERRMSG    NVARCHAR(255),  
        @P_DT_PO_H41 NVARCHAR(8),
        @P_NO_UPDATEHST INT               
                
SET @P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')                
                
INSERT PU_POH ( NO_PO, CD_COMPANY, CD_PLANT, CD_PARTNER, DT_PO,             
				CD_PURGRP, NO_EMP, CD_TPPO, FG_UM, FG_PAYMENT, FG_TAX, TP_UM_TAX,             
				CD_PJT, CD_EXCH, RT_EXCH, AM_EX, AM, VAT, DC50_PO, TP_PROCESS,             
				DTS_INSERT, ID_INSERT, FG_TAXP, YN_AM, FG_TRANS, FG_TRACK,DC_RMK2,          
				TP_TRANSPORT,COND_PAY,COND_PAY_DLV,COND_PRICE,ARRIVER ,LOADING ,        
				YN_BUDGET, BUDGET_PASS, COND_PRICE_DLV, CD_ARRIVER, CD_LOADING,  
				DC_RMK_TEXT, CD_AGENCY, AM_NEGO , COND_SHIPMENT, FREIGHT_CHARGE,
				DC_RMK_TEXT2, CD_STND_PAY, COND_DAYS, CD_ORGIN,DELIVERY_TERMS,
				DELIVERY_TIME, VALIDITY, TP_PACKING, DELIVERY_COST, INSPECTION,
				DOCUMENT_REQUIRED, SUPPLIER, MANUFACTURER,NO_ORDER, NM_PACKING,
				SHIP_DATE,DACU_NO, TXT_USERDEF4,CD_USERDEF1,CD_USERDEF2,CD_USERDEF3,CD_USERDEF4,
				TXT_USERDEF3  
)                
VALUES(	@P_NO_PO, @P_CD_COMPANY, @P_CD_PLANT, @P_CD_PARTNER, @P_DT_PO,             
		@P_CD_PURGRP, @P_NO_EMP, @P_CD_TPPO, @P_FG_UM, @P_FG_PAYMENT, @P_FG_TAX, @P_TP_UM_TAX,             
		@P_CD_PJT, @P_CD_EXCH, @P_RT_EXCH, @P_AM_EX, @P_AM, @P_VAT, @P_DC50_PO, @P_TP_PROCESS,             
		@P_DTS_INSERT,@P_ID_INSERT, @P_FG_TAXP, @P_YN_AM, @P_FG_TRANS, @P_FG_TRACK,@DC_RMK2,          
		@TP_TRANSPORT,@COND_PAY,@COND_PAY_DLV,@COND_PRICE,@ARRIVER,@LOADING,        
		@P_YN_BUDGET, @P_BUDGET_PASS , @P_COND_PRICE_DLV , @P_CD_ARRIVER,  @P_CD_LOADING,  
		@P_DC_RMK_TEXT, @P_CD_AGENCY, @P_AM_NEGO, @P_COND_SHIPMENT, @P_FREIGHT_CHARGE,
		@P_DC_RMK_TEXT2, @P_CD_STND_PAY,   @P_COND_DAYS , @P_CD_ORGIN, @P_DELIVERY_TERMS,
		@P_DELIVERY_TIME, @P_VALIDITY, @P_TP_PACKING, @P_DELIVERY_COST, @P_INSPECTION,
		@P_DOCUMENT_REQUIRED, @P_SUPPLIER, @P_MANUFACTURER, @P_NO_ORDER, @P_NM_PACKING,
		@P_SHIP_DATE,@P_DACU_NO, @P_TXT_USERDEF4,@P_CD_USERDEF1,@P_CD_USERDEF2,@P_CD_USERDEF3,@P_CD_USERDEF4,
		@P_TXT_USERDEF3 
)                
                
IF (@@ERROR <> 0)BEGIN SELECT @P_ERRNO = 100000, @P_ERRMSG = 'CM_M100010' GOTO ERROR END                
  
  
--4. 적용 시, max invoice 번호를 발주헤더 비고에 입력 및 저장  
--   단, 저장 시 이전 10일 것에 대한 invoice 번호에 대해서 일괄로 다시 update   
--  (h41이 수신되기 전에 담당자가 직접 invoice를 입력해서 등록하는 경우가 있기 떄문에 이에 대해서 처리를 하기 위함임)  
             
 IF (SELECT CD_EXC    
    FROM MA_EXC    
   WHERE CD_COMPANY = @P_CD_COMPANY  
     AND EXC_TITLE = 'H41적용여부' ) = 'Y'  
 BEGIN  
  UPDATE PU_H41_ITEM_NIKON  
     SET NO_PO = @P_NO_PO  
    WHERE CD_COMPANY = @P_CD_COMPANY  
      AND INVOICE_NUMBER = @P_DC50_PO  
  IF (@@ERROR <> 0)BEGIN SELECT @P_ERRNO = 100000, @P_ERRMSG = 'CM_M100010' GOTO ERROR END                 
          
        SET @P_DT_PO_H41 = CONVERT(NVARCHAR,DATEADD(DAY,-10,@P_DT_PO),112)  
          
        --       
     UPDATE PU_H41_ITEM_NIKON  
     SET NO_PO = B.DC50_PO  
    FROM PU_H41_ITEM_NIKON A  
         LEFT OUTER JOIN PU_POH B ON A.INVOICE_NUMBER = @P_DC50_PO  AND A.CD_COMPANY = B.CD_COMPANY                        
    WHERE A.CD_COMPANY = @P_CD_COMPANY        
      AND ISNULL(A.NO_PO,'') = ''  
      AND B.DT_PO >= @P_DT_PO_H41 AND B.DT_PO <= @P_DT_PO  
   IF (@@ERROR <> 0)BEGIN SELECT @P_ERRNO = 100000, @P_ERRMSG = 'CM_M100010' GOTO ERROR END                 
       
 END
 
 IF(@P_TP_GR = '104')
 BEGIN
	INSERT PU_POH_SUB(CD_COMPANY, NO_PO, DT_PROCESS_IV, DT_PAY_PRE_IV, DT_DUE_IV, FG_PAYBILL_IV, CD_DOCU_IV, AM_K_IV, VAT_TAX_IV, AM_EX_IV, DC_RMK_IV, CD_BIZAREA_TAX)
	VALUES (@P_CD_COMPANY, @P_NO_PO, @P_DT_PROCESS_IV, @P_DT_PAY_PRE_IV, @P_DT_DUE_IV, @P_FG_PAYBILL_IV, @P_CD_DOCU_IV, @P_AM_K_IV, @P_VAT_TAX_IV, @P_AM_EX_IV, @P_DC_RMK_IV, @P_CD_BIZAREA_TAX)
 END
   
   
 --PU_POH LOG (D20130829078)
 SELECT @P_NO_UPDATEHST = COUNT(1) FROM PU_POH_LOG WHERE NO_PO = @P_NO_PO AND CD_COMPANY = @P_CD_COMPANY AND FG_STAT = 'INSERT'
 BEGIN
	INSERT INTO PU_POH_LOG 
		(
			NO_PO,		CD_COMPANY,		CD_PLANT,		CD_PARTNER,		DT_PO,		CD_PURGRP,		NO_EMP,		
			CD_TPPO,	FG_UM,			FG_PAYMENT,		FG_TAX,			TP_UM_TAX,	CD_PJT,			CD_EXCH,		
			RT_EXCH,	AM_EX,			AM,				VAT,			DC50_PO,	TP_PROCESS,		FG_TAXP,		
			YN_AM,		DTS_INSERT,		ID_INSERT,		DTS_UPDATE,		ID_UPDATE,	DTS_DELETE,		ID_DELETE,	
			FG_TRANS,	FG_TRACK,		DC_RMK2,		FG_STAT,		NO_UPDATEHST
		)
	SELECT	NO_PO,		CD_COMPANY,		CD_PLANT,		CD_PARTNER,		DT_PO,		CD_PURGRP,		NO_EMP,
			CD_TPPO,	FG_UM,			FG_PAYMENT,		FG_TAX,			TP_UM_TAX,	CD_PJT,			CD_EXCH,
			RT_EXCH,	AM_EX,			AM,				VAT,			DC50_PO,	TP_PROCESS,		FG_TAXP,
			YN_AM,		@P_DTS_INSERT,	@P_ID_INSERT,	'',				'',			'',				'',
			FG_TRANS,	FG_TRACK,		DC_RMK2,		'INSERT',		@P_NO_UPDATEHST + 1
	  FROM PU_POH
	 WHERE CD_COMPANY = @P_CD_COMPANY
	   AND NO_PO = @P_NO_PO 	
 END
                
RETURN                
ERROR: RAISERROR  (@P_ERRMSG, 18, 1)
GO


