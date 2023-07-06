USE [NEOE]
GO

/****** Object:  Trigger [NEOE].[TU_PU_ADPAYMENT]    Script Date: 2015-05-07 오전 9:55:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [NEOE].[TU_PU_ADPAYMENT] 
ON [NEOE].[PU_ADPAYMENT] 
AFTER UPDATE 
AS 
  BEGIN 
      DECLARE     @NUMROWS INT, 
                    @ERRNO INT, 
                   @ERRMSG NVARCHAR(255), 
                    @NO_PO NVARCHAR(20), 
                @NO_POLINE NUMERIC(5), 
               @CD_COMPANY NVARCHAR(7), 
              @QT_ADPAY_MM NUMERIC(17, 4), 
              @AM_EX_ADPAY NUMERIC(17, 4), 
                 @AM_ADPAY NUMERIC(17, 4) 

      SELECT @NUMROWS = @@ROWCOUNT 

      IF @NUMROWS = 0 
        RETURN 

      DECLARE CUR_TU_PU_ADPAYMENT CURSOR FOR 
        SELECT D.NO_PO 
               ,D.NO_POLINE 
               ,D.CD_COMPANY 
               ,D.QT_ADPAY_MM 
               ,D.AM_EX 
               ,D.AM 
          FROM DELETED D 

      OPEN CUR_TU_PU_ADPAYMENT 

      FETCH NEXT FROM CUR_TU_PU_ADPAYMENT INTO @NO_PO, @NO_POLINE, @CD_COMPANY, 
      @QT_ADPAY_MM, @AM_EX_ADPAY, @AM_ADPAY 

      WHILE ( @@FETCH_STATUS <> -1 ) 
        BEGIN 
            IF ( @@FETCH_STATUS <> -2 ) 
              BEGIN 
                  DELETE FROM PU_ADPAYMENT 
                   WHERE NO_PO = @NO_PO 
                     AND NO_POLINE = @NO_POLINE 
                     AND CD_COMPANY = @CD_COMPANY 
              END 

            FETCH NEXT FROM CUR_TU_PU_ADPAYMENT INTO @NO_PO, @NO_POLINE, 
            @CD_COMPANY, 
            @QT_ADPAY_MM, @AM_EX_ADPAY, @AM_ADPAY 
        END 

      CLOSE CUR_TU_PU_ADPAYMENT 

      DEALLOCATE CUR_TU_PU_ADPAYMENT 

      RETURN 

      ERROR: 

      RAISERROR (@ERRMSG,18,1) 

      RETURN 
  END 
--##TRANSACTION END##
GO

