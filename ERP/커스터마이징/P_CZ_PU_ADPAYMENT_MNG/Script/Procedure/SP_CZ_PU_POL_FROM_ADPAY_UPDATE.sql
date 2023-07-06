USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_POL_FROM_ADPAY_UPDATE]    Script Date: 2015-05-07 오전 9:45:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PU_POL_FROM_ADPAY_UPDATE] (@CD_COMPANY       NVARCHAR(7) 
														  ,@NO_PO           NVARCHAR(20) 
														  ,@NO_POLINE       NUMERIC(5, 0) 
														  ,@QT_ADPAY_MM_NEW NUMERIC(17, 4) 
														  ,@QT_ADPAY_MM_OLD NUMERIC(17, 4) 
														  ,@AM_EX_ADPAY_NEW NUMERIC(17, 4) 
														  ,@AM_EX_ADPAY_OLD NUMERIC(17, 4) 
														  ,@AM_ADPAY_NEW    NUMERIC(17, 4) 
														  ,@AM_ADPAY_OLD    NUMERIC(17, 4)) 
AS 
  BEGIN 
      DECLARE @ERRMSG VARCHAR(255) 

      UPDATE PU_POL 
         SET QT_ADPAY_MM = ISNULL(QT_ADPAY_MM, 0) + ( 
                           @QT_ADPAY_MM_NEW - @QT_ADPAY_MM_OLD ) 
             ,AM_EX_ADPAY = ISNULL(AM_EX_ADPAY, 0) + ( 
                            @AM_EX_ADPAY_NEW - @AM_EX_ADPAY_OLD ) 
             ,AM_ADPAY = ISNULL(AM_ADPAY, 0) + ( @AM_ADPAY_NEW - @AM_ADPAY_OLD ) 
       WHERE NO_PO = @NO_PO 
         AND NO_LINE = @NO_POLINE 
         AND CD_COMPANY = @CD_COMPANY 

      IF ( @@ERROR <> 0 ) 
        BEGIN 
            SELECT @ERRMSG = '[SP_CZ_PU_POL_FROM_ADPAY_UPDATE]작업을 정상적으로 처리하지 못했습니다.' 
			GOTO ERROR 
		END 
  END 

RETURN 

ERROR: 

RAISERROR (@ERRMSG,18,1)   
GO

