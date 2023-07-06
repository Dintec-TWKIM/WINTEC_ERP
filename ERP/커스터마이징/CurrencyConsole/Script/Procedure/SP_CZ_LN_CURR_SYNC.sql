USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_LN_CURR_SYNC]    Script Date: 2019-06-20 오후 4:17:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [NEOE].[SP_CZ_LN_CURR_SYNC]
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

MERGE INTO CZ_LN_CURR_TOTAL AS T 
      USING (SELECT *
             FROM V_CZ_LN_CURR) S
	  ON T.DAY = S.DAY 
      WHEN MATCHED THEN 
        UPDATE SET T.EXC_R = (CASE WHEN S.EXC_R = 'NaN' THEN T.EXC_R ELSE S.EXC_R END),
				   T.YEN_EXC_R = (CASE WHEN S.YEN_EXC_R = 'NaN' THEN T.YEN_EXC_R ELSE S.YEN_EXC_R END),
				   T.EURO_EXC_R = (CASE WHEN S.EURO_EXC_R = 'NaN' THEN T.EURO_EXC_R ELSE S.EURO_EXC_R END),
				   T.CALL_R = (CASE WHEN S.CALL_R = 'NaN' THEN T.CALL_R ELSE S.CALL_R END),
				   T.KOSPI = (CASE WHEN S.KOSPI = 'NaN' THEN T.KOSPI ELSE S.KOSPI END),
				   T.DOW_JONES = (CASE WHEN S.DOW_JONES = 'NaN' THEN T.DOW_JONES ELSE S.DOW_JONES END),
				   T.NASDAQ = (CASE WHEN S.NASDAQ = 'NaN' THEN T.NASDAQ ELSE S.NASDAQ END),
				   T.WTI = (CASE WHEN S.WTI = 'NaN' THEN T.WTI ELSE S.WTI END),
				   T.CURR_BAL = (CASE WHEN S.CURR_BAL = 'NaN' THEN T.CURR_BAL ELSE S.CURR_BAL END),  
                   T.CAPIT_BAL = (CASE WHEN S.CAPIT_BAL = 'NaN' THEN T.CAPIT_BAL ELSE S.CAPIT_BAL END), 
                   T.CUS_PR_K = (CASE WHEN S.CUS_PR_K = 'NaN' THEN T.CUS_PR_K ELSE S.CUS_PR_K END),
                   T.CUS_PR_US = (CASE WHEN S.CUS_PR_US = 'NaN' THEN T.CUS_PR_US ELSE S.CUS_PR_US END), 
                   T.PROD_PR_K = (CASE WHEN S.PROD_PR_K = 'NaN' THEN T.PROD_PR_K ELSE S.PROD_PR_K END),
                   T.PROD_PR_US = (CASE WHEN S.PROD_PR_US = 'NaN' THEN T.PROD_PR_US ELSE S.PROD_PR_US END),
                   T.INDUST_K = (CASE WHEN S.INDUST_K = 'NaN' THEN T.INDUST_K ELSE S.INDUST_K END),
                   T.INDUST_US = (CASE WHEN S.INDUST_US = 'NaN' THEN T.INDUST_US ELSE S.INDUST_US END),
                   T.US_FED_R = (CASE WHEN S.US_FED_R = 'NaN' THEN T.US_FED_R ELSE S.US_FED_R END),
				   T.EC_R_K = (CASE WHEN S.EC_R_K = 'NaN' THEN T.EC_R_K ELSE S.EC_R_K END),
				   T.EC_R_US = (CASE WHEN S.EC_R_US = 'NaN' THEN T.EC_R_US ELSE S.EC_R_US END),
				   T.GDP_K = (CASE WHEN S.GDP_K = 'NaN' THEN T.GDP_K ELSE S.GDP_K END),
				   T.GDP_US = (CASE WHEN S.GDP_US = 'NaN' THEN T.GDP_US ELSE S.GDP_US END)
      WHEN NOT MATCHED THEN 
        INSERT (DAY,
			    EXC_R,
			    YEN_EXC_R,
			    EURO_EXC_R,
			    CALL_R,
			    KOSPI,
			    DOW_JONES,
			    NASDAQ,
			    WTI,
				CURR_BAL, 
                CAPIT_BAL, 
                CUS_PR_K, 
                CUS_PR_US, 
                PROD_PR_K, 
                PROD_PR_US, 
                INDUST_K, 
                INDUST_US, 
                US_FED_R,
			    EC_R_K,
			    EC_R_US,
			    GDP_K,
			    GDP_US) 
		VALUES (S.DAY,
				S.EXC_R,
				S.YEN_EXC_R,
				S.EURO_EXC_R,
				S.CALL_R,
				S.KOSPI,
				S.DOW_JONES,
				S.NASDAQ,
				S.WTI,
				S.CURR_BAL, 
                S.CAPIT_BAL, 
                S.CUS_PR_K, 
                S.CUS_PR_US, 
                S.PROD_PR_K, 
                S.PROD_PR_US, 
                S.INDUST_K, 
                S.INDUST_US, 
                S.US_FED_R,
				S.EC_R_K,
				S.EC_R_US,
				S.GDP_K,
				S.GDP_US);

GO

