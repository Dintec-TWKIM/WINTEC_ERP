USE [NEOE]
GO

/****** Object:  Trigger [NEOE].[TI_PU_ADPAYMENT]    Script Date: 2015-05-07 오전 9:55:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [NEOE].[TI_PU_ADPAYMENT] ON [NEOE].[PU_ADPAYMENT] 
AFTER INSERT 
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

      DECLARE CUR_TI_PU_ADPAYMENT CURSOR FOR 
        SELECT I.NO_PO 
               ,I.NO_POLINE 
               ,I.CD_COMPANY 
               ,I.QT_ADPAY_MM 
               ,I.AM_EX 
               ,I.AM 
          FROM INSERTED I 

      OPEN CUR_TI_PU_ADPAYMENT 

      FETCH NEXT FROM CUR_TI_PU_ADPAYMENT INTO @NO_PO, @NO_POLINE, @CD_COMPANY, 
      @QT_ADPAY_MM, @AM_EX_ADPAY, @AM_ADPAY 

      WHILE ( @@FETCH_STATUS <> -1 ) 
        BEGIN 
            IF ( @@FETCH_STATUS <> -2 ) 
              BEGIN 
                  EXEC SP_CZ_PU_POL_FROM_ADPAY_UPDATE 
                    @CD_COMPANY, 
                    @NO_PO, 
                    @NO_POLINE, 
                    @QT_ADPAY_MM, 
                    0, 
                    @AM_EX_ADPAY, 
                    0, 
                    @AM_ADPAY, 
                    0 
              END 

            FETCH NEXT FROM CUR_TI_PU_ADPAYMENT INTO @NO_PO, @NO_POLINE, 
            @CD_COMPANY, 
            @QT_ADPAY_MM, @AM_EX_ADPAY, @AM_ADPAY 
        END 

      CLOSE CUR_TI_PU_ADPAYMENT 

      DEALLOCATE CUR_TI_PU_ADPAYMENT 

      RETURN 

      ERROR: 

      RAISERROR (@ERRMSG,18,1) 

      RETURN 
  END 
--##TRANSACTION END## 
 
GO

