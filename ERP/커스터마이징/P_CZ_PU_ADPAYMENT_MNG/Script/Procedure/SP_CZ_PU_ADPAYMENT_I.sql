USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_ADPAYMENT_I]    Script Date: 2015-05-07 오전 9:44:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PU_ADPAYMENT_I] 
(
	@P_CD_COMPANY      NVARCHAR(7), 
    @P_CD_PLANT        NVARCHAR(7), 
    @P_NO_BIZAREA      NVARCHAR(7), 
    @P_NO_ADPAY        NVARCHAR(20), 
    @P_NO_ADPAYLINE    NUMERIC(5, 0), 
    @P_DT_ADPAY        NCHAR(8), 
    @P_NO_PO           NVARCHAR(20), 
    @P_NO_POLINE       NUMERIC(5, 0), 
    @P_CD_EXCH         NVARCHAR(3), 
    @P_RT_EXCH         NUMERIC(11, 4), 
    @P_AM              NUMERIC(17, 4), 
    @P_AM_EX           NUMERIC(17, 4), 
    @P_CD_DOCU         NVARCHAR(3), 
    @P_QT_ADPAY_MM     NUMERIC(17, 4), 
    @P_CD_DEPT         NVARCHAR(12), 
    @P_YN_JEONJA       NVARCHAR(1), 
    @P_TP_AIS          NVARCHAR(1), 
    @P_DT_PAY_SCHEDULE NCHAR(8), 
    @P_PO_CONDITION    NVARCHAR(4), 
    @P_DT_ACCT         NCHAR(8),
	@P_ID_INSERT	   NVARCHAR(15)
) 
AS 

BEGIN 
	DECLARE @P_ERRMSG NVARCHAR(255)
	
	INSERT PU_ADPAYMENT 
	(
		CD_COMPANY, 
	    CD_PLANT, 
	    NO_BIZAREA, 
	    NO_ADPAY, 
	    NO_ADPAYLINE, 
	    DT_ADPAY, 
	    NO_PO, 
	    NO_POLINE, 
	    CD_EXCH, 
	    RT_EXCH, 
	    AM, 
	    AM_EX, 
	    CD_DOCU, 
	    QT_ADPAY_MM, 
	    CD_DEPT, 
	    YN_JEONJA, 
	    TP_AIS,
	    DT_PAY_SCHEDULE, 
	    PO_CONDITION, 
	    DT_ACCT,
		ID_INSERT,
		DTS_INSERT
	) 
	VALUES
	(
		@P_CD_COMPANY, 
	    @P_CD_PLANT, 
	    @P_NO_BIZAREA, 
	    @P_NO_ADPAY, 
	    @P_NO_ADPAYLINE, 
	    @P_DT_ADPAY, 
	    @P_NO_PO, 
	    @P_NO_POLINE, 
	    @P_CD_EXCH, 
	    @P_RT_EXCH, 
	    @P_AM, 
	    @P_AM_EX, 
	    @P_CD_DOCU, 
	    @P_QT_ADPAY_MM, 
	    @P_CD_DEPT, 
	    @P_YN_JEONJA, 
	    @P_TP_AIS, 
	    @P_DT_PAY_SCHEDULE, 
	    @P_PO_CONDITION, 
	    @P_DT_ACCT,
		@P_ID_INSERT,
		NEOE.SF_SYSDATE(GETDATE()) 
	) 
	
	RETURN 
	
	ERROR: 
	RAISERROR (@P_ERRMSG,18,1) 
END   

GO

