USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_ADPAYMENT_BILL_HI]    Script Date: 2015-08-06 오전 11:58:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PU_ADPAYMENT_BILL_HI]  
(  
	@P_CD_COMPANY               NVARCHAR(7),   
	@P_NO_BILL					NVARCHAR(20),   
	@P_DT_BILL					NVARCHAR(8),  
	@P_CD_SALEGRP               NVARCHAR(7),   
	@P_CD_PARTNER               NVARCHAR(20),
	@P_NO_EMP                   NVARCHAR(10),   
	@P_TP_BUSI                  NVARCHAR(3),   
	@P_AM_RCPBILL               NUMERIC(17,4),   
	@P_AM_IV                    NUMERIC(17,4),   
	@P_DC_RMK                   NVARCHAR(100),   
	@P_CD_TP                    NVARCHAR(12),   
	@P_ST_BILL                  NCHAR(1),  
	@P_CD_DOCU                  NVARCHAR(3) = NULL,
	@P_ID_INSERT				NVARCHAR(15)
)   
AS   
BEGIN  
DECLARE @ERRNO      INT,  
        @ERRMSG     VARCHAR(255)  
  
	INSERT INTO CZ_PU_ADPAYMENT_BILL_H  
	(
		CD_COMPANY,
		NO_BILLS,
		CD_PARTNER,
		DT_BILLS,
		TP_BUSI,
		AM_BILLS,
		CD_BILLTGRP,
		NO_EMP,
		DC_RMK,
		AM_IV,
		CD_TP,
		ST_BILL,
		CD_DOCU,
		ID_INSERT,
		DTS_INSERT 
	)  
	VALUES
	(
		@P_CD_COMPANY,
		@P_NO_BILL,
		@P_CD_PARTNER,
		@P_DT_BILL,
		@P_TP_BUSI,
		@P_AM_RCPBILL,
		@P_CD_SALEGRP,
		@P_NO_EMP,
		@P_DC_RMK,
		@P_AM_IV,
		@P_CD_TP,
		@P_ST_BILL,
		@P_CD_DOCU,
		@P_ID_INSERT,
		NEOE.SF_SYSDATE(GETDATE())
	)  
  
IF (@@ERROR <> 0 )  
BEGIN    
      SELECT @ERRNO  = 100000,    
                   @ERRMSG = 'CZ_PU_ADPAYMENT_BILL_H INSERT error '    
      GOTO ERROR    
END  
  
RETURN  
ERROR:    
    ROLLBACK TRAN
    RAISERROR (@ERRMSG, 18 , 1)  
END
GO

