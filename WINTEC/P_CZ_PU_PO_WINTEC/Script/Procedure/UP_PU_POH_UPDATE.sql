USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_POH_UPDATE]    Script Date: 2022-03-24 오후 3:42:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER  PROC [NEOE].[UP_PU_POH_UPDATE]
(          
@P_NO_PO                  NVARCHAR(20),                -- 발주번호          
@P_CD_COMPANY             NVARCHAR(7),                -- 회사                  
@P_DT_PO                  NCHAR(8),                -- 발주일          R
@P_CD_PURGRP              NVARCHAR(7),                -- 구매그룹          
@P_NO_EMP                 NVARCHAR(10),                -- 담당자                  
@P_CD_PJT                 NVARCHAR(20),                -- 프로젝트                  
@P_AM_EX                  NUMERIC(19,6),                -- 금액          
@P_AM                     NUMERIC(17,4),                -- 원화금액          
@P_VAT                    NUMERIC(17,4),                -- 부가세          
@P_DC50_PO                NVARCHAR(400),                -- 비고                  
@P_FG_TAXP                NCHAR(3),                --                  
@P_DTS_UPDATE             NVARCHAR(14),                -- 등록일          
@P_ID_UPDATE              NVARCHAR(15),                -- 등록자          
@P_DC_RMK2                NVARCHAR(200),                -- 비고            
@TP_TRANSPORT             NVARCHAR(3) ,      
@COND_PAY                 NVARCHAR(3) ,      
@COND_PAY_DLV             NVARCHAR(20) ,      
@COND_PRICE               NVARCHAR(3) ,      
@ARRIVER                  NVARCHAR(100),      
@LOADING                  NVARCHAR(100) = NULL,    
@P_YN_BUDGET			  NVARCHAR(1),    
@P_BUDGET_PASS			  NVARCHAR(1),  
@P_COND_PRICE_DLV		  NVARCHAR(100) = NULL,  
@P_CD_ARRIVER			  NVARCHAR(4) = NULL,   
@P_CD_LOADING			  NVARCHAR(4) = NULL,  
@P_DC_RMK_TEXT			  NTEXT     = NULL,
@P_FG_PAYMENT			  NVARCHAR(3) = NULL,           
@P_COND_SHIPMENT		  NVARCHAR(3) = NULL, 
@P_FREIGHT_CHARGE		  NVARCHAR(3) = NULL,
@P_DC_RMK_TEXT2			  NVARCHAR(4000) = NULL,
@P_CD_STND_PAY			  NVARCHAR(4) = NULL, 
@P_COND_DAYS			  NUMERIC(4,0) = 0,
@P_CD_ORGIN				  NVARCHAR(6) = NULL,
@P_DELIVERY_TERMS		  NVARCHAR(100) = NULL,
@P_DELIVERY_TIME		  NVARCHAR(100) = NULL,
@P_VALIDITY				  NVARCHAR(100) = NULL,
@P_TP_PACKING			  NVARCHAR(3) = NULL,
@P_DELIVERY_COST		  NUMERIC(17,4) = 0,
@P_INSPECTION			  NVARCHAR(100) = NULL,
@P_DOCUMENT_REQUIRED      NVARCHAR(100) = NULL,
@P_SUPPLIER				  NVARCHAR(20) = NULL,
@P_MANUFACTURER			  NVARCHAR(20) = NULL,
@P_NO_ORDER				  NVARCHAR(200) = NULL,
@P_NM_PACKING			  NVARCHAR(100) = NULL,
@P_SHIP_DATE              NVARCHAR(8) = NULL,
@P_DACU_NO                NVARCHAR(20) = NULL,
@P_CD_USERDEF1			  NVARCHAR(20) = '',
@P_CD_USERDEF2			  NVARCHAR(20) = '',
@P_CD_USERDEF3			  NVARCHAR(20) = '',
@P_CD_USERDEF4			  NVARCHAR(20) = '',
@P_TXT_USERDEF3			  NVARCHAR(400) = '' 
)          
          
AS          
BEGIN          
DECLARE           
@P_ERRNO         INT,          
@P_ERRMSG        VARCHAR(255),
@P_DT_PO_H41	 NVARCHAR(8),
@P_NO_UPDATEHST  INT           
          
SET @P_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')              
          
UPDATE  PU_POH           
SET DT_PO =@P_DT_PO, CD_PURGRP=@P_CD_PURGRP,NO_EMP=@P_NO_EMP,CD_PJT=@P_CD_PJT, AM_EX =@P_AM_EX, AM=@P_AM,VAT=@P_VAT,          
 DC50_PO=@P_DC50_PO,DTS_UPDATE=@P_DTS_UPDATE,ID_UPDATE=@P_ID_UPDATE ,FG_TAXP =@P_FG_TAXP,        
 DC_RMK2 = @P_DC_RMK2,      
 TP_TRANSPORT = @TP_TRANSPORT,      
 COND_PAY = @COND_PAY,      
 COND_PAY_DLV = @COND_PAY_DLV,      
 COND_PRICE = @COND_PRICE,      
 ARRIVER = @ARRIVER ,    
 LOADING = @LOADING ,    
 YN_BUDGET = @P_YN_BUDGET,    
 BUDGET_PASS = @P_BUDGET_PASS,  
 COND_PRICE_DLV = @P_COND_PRICE_DLV,    
 CD_ARRIVER = @P_CD_ARRIVER,    
 CD_LOADING = @P_CD_LOADING,
 DC_RMK_TEXT = @P_DC_RMK_TEXT,
 FG_PAYMENT = @P_FG_PAYMENT,
 COND_SHIPMENT = @P_COND_SHIPMENT,
 FREIGHT_CHARGE = @P_FREIGHT_CHARGE,
 DC_RMK_TEXT2 = @P_DC_RMK_TEXT2,
 CD_STND_PAY = @P_CD_STND_PAY, 
 COND_DAYS =  @P_COND_DAYS , 
 CD_ORGIN = @P_CD_ORGIN,
 DELIVERY_TERMS = @P_DELIVERY_TERMS,
 DELIVERY_TIME = @P_DELIVERY_TIME,
 VALIDITY = @P_VALIDITY,
 TP_PACKING = @P_TP_PACKING,
 DELIVERY_COST = @P_DELIVERY_COST,
 INSPECTION = @P_INSPECTION,
 DOCUMENT_REQUIRED = @P_DOCUMENT_REQUIRED,
 SUPPLIER = @P_SUPPLIER,
 MANUFACTURER = @P_MANUFACTURER,
 NO_ORDER = @P_NO_ORDER,
 NM_PACKING = @P_NM_PACKING ,
 SHIP_DATE  = @P_SHIP_DATE,
 DACU_NO    = @P_DACU_NO,
 CD_USERDEF1 = @P_CD_USERDEF1,
 CD_USERDEF2 = @P_CD_USERDEF2,
 CD_USERDEF3 = @P_CD_USERDEF3,
 CD_USERDEF4 = @P_CD_USERDEF4,
 TXT_USERDEF3 = @P_TXT_USERDEF3  
 
     
WHERE NO_PO =@P_NO_PO AND CD_COMPANY =@P_CD_COMPANY          
IF (@@ERROR <> 0 )          
BEGIN            
      SELECT @P_ERRNO  = 100000,            
             @P_ERRMSG = 'CM_M100010'            
      GOTO ERROR            
END 

          
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
             
--PU_POH LOG (D20130829078)
SELECT @P_NO_UPDATEHST = COUNT(1) FROM PU_POH_LOG WHERE NO_PO = @P_NO_PO AND CD_COMPANY = @P_CD_COMPANY AND FG_STAT = 'UPDATE'

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
			YN_AM,		@P_DTS_UPDATE,	@P_ID_UPDATE,	'',				'',			'',				'',
			FG_TRANS,	FG_TRACK,		DC_RMK2,		'UPDATE',		@P_NO_UPDATEHST +1 
	  FROM PU_POH
	 WHERE CD_COMPANY = @P_CD_COMPANY
	   AND NO_PO = @P_NO_PO 	
 END
          
RETURN          
ERROR:            
   RAISERROR (@P_ERRMSG, 18,1 )     
END
GO


