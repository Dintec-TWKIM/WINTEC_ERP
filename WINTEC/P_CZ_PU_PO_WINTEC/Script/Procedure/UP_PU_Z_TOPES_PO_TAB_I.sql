USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_Z_TOPES_PO_TAB_I]    Script Date: 2022-03-24 ���� 6:04:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_PU_Z_TOPES_PO_TAB_I]    
(    
	@P_CD_COMPANY  	 NVARCHAR(7),    
	@P_CD_PLANT		 NVARCHAR(7),     
	@P_NO_PO       	 NVARCHAR(20),     
	@P_NO_SEQ      	 NUMERIC(5, 0),     
	@P_FG_IV       	 NVARCHAR(3),      
	@P_DT_IV_PLAN  	 NVARCHAR(8),     
	@P_RT_IV       	 NUMERIC(19, 6),     
	@P_AM            NUMERIC(17, 4),     
	@P_VAT           NUMERIC(17, 4),
	@P_DT_BAN_PLAN   NVARCHAR(8),  
	@P_RT_BAN		 NUMERIC(19, 6),
	@P_AM_BAN		 NUMERIC(17, 4),
	@P_AM_BANK		 NUMERIC(17, 4),
	@P_SERVER_KEY	 NVARCHAR(25),
	@P_ID_INSERT	 NVARCHAR(15)
)    
AS    
DECLARE  @ERRNO      INT,     
		 @ERRMSG     NVARCHAR(255)  ,
		 @DTS_INSERT NVARCHAR(14)
SELECT @DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')     

    
INSERT PU_CUST_PUBLIC    
(CD_NO_PK_1,CD_NO_PK_2,CD_NO_PK_3,CD_NO_PK_4,CD_1,DT_1,UM_RT_ETC_1,AM_1,AM_2,DT_2,UM_RT_ETC_2,AM_3,AM_4,NM_BUSINESS,CD_CUST_SERVER_KEY,SQ_BUSINESS,DTS_INSERT,ID_INSERT)    
VALUES(@P_CD_COMPANY, @P_NO_PO, @P_CD_PLANT,@P_NO_SEQ, @P_FG_IV,@P_DT_IV_PLAN, @P_RT_IV, @P_AM, @P_VAT, @P_DT_BAN_PLAN, @P_RT_BAN, @P_AM_BAN, @P_AM_BANK,'P_PU_PO_REG2',@P_SERVER_KEY,1,@DTS_INSERT,@P_ID_INSERT)    
    
IF (@@ERROR <> 0 ) BEGIN SELECT @ERRNO  = 100000, @ERRMSG = '�۾��� ���������� ó������ ���߽��ϴ�.' GOTO ERROR END    
    
RETURN    
ERROR: RAISERROR (@ERRMSG, 18 ,1)
GO

