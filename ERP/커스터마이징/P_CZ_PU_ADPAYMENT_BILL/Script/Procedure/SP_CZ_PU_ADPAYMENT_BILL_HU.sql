USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_ADPAYMENT_BILL_HU]    Script Date: 2015-08-06 오후 1:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PU_ADPAYMENT_BILL_HU]  
(  
	@P_CD_COMPANY           NVARCHAR(7),   
	@P_NO_BILL				NVARCHAR(20),   
	@P_DT_BILL				NVARCHAR(8),  
	@P_CD_SALEGRP           NVARCHAR(7),   
	@P_CD_PARTNER           NVARCHAR(20),
	@P_NO_EMP				NVARCHAR(10),   
	@P_TP_BUSI				NVARCHAR(3),   
	@P_AM_RCPBILL           NUMERIC(17,4),   
	@P_AM_IV                NUMERIC(17,4),   
	@P_DC_RMK				NVARCHAR(100),   
	@P_CD_TP                NVARCHAR(12), 
	@P_CD_DOCU				NVARCHAR(3),
	@P_ID_UPDATE		    NVARCHAR(15)   
)   
AS   
BEGIN  
DECLARE @ERRNO      INT,  
        @ERRMSG     VARCHAR(255)  

	UPDATE CZ_PU_ADPAYMENT_BILL_H  
	SET CD_COMPANY = @P_CD_COMPANY,
		NO_BILLS = @P_NO_BILL,
		CD_PARTNER = @P_CD_PARTNER,
		DT_BILLS = @P_DT_BILL,
		TP_BUSI = @P_TP_BUSI,
		AM_BILLS = @P_AM_RCPBILL,
		CD_BILLTGRP = @P_CD_SALEGRP,
		NO_EMP = @P_NO_EMP,
		DC_RMK = @P_DC_RMK,
		AM_IV = @P_AM_IV,
		CD_TP = @P_CD_TP,
		CD_DOCU = @P_CD_DOCU,
		ID_UPDATE = @P_ID_UPDATE,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY  
	AND NO_BILLS = @P_NO_BILL  
  
IF (@@ERROR <> 0 )  
BEGIN    
      SELECT @ERRNO  = 100000,    
                   @ERRMSG = 'CM_M100010'    
      GOTO ERROR    
END  
  
RETURN  
ERROR:    
    ROLLBACK TRAN
    RAISERROR (@ERRMSG, 18 , 1)    
END
GO

