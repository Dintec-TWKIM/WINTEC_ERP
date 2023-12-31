USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_EXCHANGE_SYNC]    Script Date: 2015-11-24 오후 1:47:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_MA_EXCHANGE_SYNC]
(
	@P_CD_COMPANY		NVARCHAR(7)
) 
AS

MERGE INTO MA_EXCHANGE AS T 
      USING (SELECT 'TEST' AS CD_COMPANY,
	                YYMMDD,
                    NO_SEQ,
					QUOTATION_TIME,
					CURR_SOUR,
					CURR_DEST,
					RATE_BASE,
					RATE_SALE,
					RATE_BUY
               FROM MA_EXCHANGE
              WHERE CD_COMPANY = 'K100') S
	  ON T.CD_COMPANY = S.CD_COMPANY 
	  AND T.YYMMDD = S.YYMMDD
	  AND T.NO_SEQ = S.NO_SEQ
	  AND T.CURR_SOUR = S.CURR_SOUR
	  AND T.CURR_DEST = S.CURR_DEST
      WHEN MATCHED THEN 
        UPDATE SET RATE_BASE = S.RATE_BASE,
				   RATE_SALE = S.RATE_SALE,
				   RATE_BUY = S.RATE_BUY,
				   QUOTATION_TIME = S.QUOTATION_TIME,
				   ID_UPDATE = 'SYNC',
				   DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
      WHEN NOT MATCHED THEN 
        INSERT (CD_COMPANY,
	            YYMMDD,
                NO_SEQ,
				QUOTATION_TIME,
				CURR_SOUR,
				CURR_DEST,
				RATE_BASE,
				RATE_SALE,
				RATE_BUY,			 
				ID_INSERT,
				DTS_INSERT) 
		VALUES (S.CD_COMPANY, 
				S.YYMMDD,
                S.NO_SEQ,
				S.QUOTATION_TIME,
				S.CURR_SOUR,
				S.CURR_DEST,
				S.RATE_BASE,
				S.RATE_SALE,
				S.RATE_BUY,	 
				'SYNC',			
				NEOE.SF_SYSDATE(GETDATE()));

MERGE INTO CZ_MA_EXCHANGE AS T 
      USING (SELECT 'TEST' AS CD_COMPANY,
	                YYMMDD,
                    NO_SEQ,
					CURR_SOUR,
					CURR_DEST,
					RATE_PURCHASE,
					RATE_SALES
               FROM CZ_MA_EXCHANGE
              WHERE CD_COMPANY = 'K100') S
	  ON T.CD_COMPANY = S.CD_COMPANY 
	  AND T.YYMMDD = S.YYMMDD
	  AND T.NO_SEQ = S.NO_SEQ
	  AND T.CURR_SOUR = S.CURR_SOUR
	  AND T.CURR_DEST = S.CURR_DEST
      WHEN MATCHED THEN 
        UPDATE SET RATE_PURCHASE = S.RATE_PURCHASE,
				   RATE_SALES = S.RATE_SALES,
				   ID_UPDATE = 'SYNC',
				   DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
      WHEN NOT MATCHED THEN 
        INSERT (CD_COMPANY,
	            YYMMDD,
                NO_SEQ,
				CURR_SOUR,
				CURR_DEST,
				RATE_PURCHASE,
				RATE_SALES,			 
				ID_INSERT,
				DTS_INSERT) 
		VALUES (S.CD_COMPANY, 
				S.YYMMDD,
                S.NO_SEQ,
				S.CURR_SOUR,
				S.CURR_DEST,
				S.RATE_PURCHASE,
				S.RATE_SALES,			 
				'SYNC',			
				NEOE.SF_SYSDATE(GETDATE()));

GO

