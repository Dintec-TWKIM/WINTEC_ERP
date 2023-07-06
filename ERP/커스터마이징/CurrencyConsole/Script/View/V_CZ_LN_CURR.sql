USE [NEOE]
GO

/****** Object:  View [NEOE].[V_CZ_LN_CURR]    Script Date: 2019-06-10 오전 11:59:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER VIEW [NEOE].[V_CZ_LN_CURR]
AS

WITH DT_DAY AS
(
	SELECT DY.DAY,
	       LEFT(DY.DAY, 4) + CONVERT(NVARCHAR, CEILING(CONVERT(NUMERIC, SUBSTRING(DY.DAY, 5, 2)) / 3.0)) AS QUARTER,
	  	   LEFT(DY.DT_QUARTER_BEFORE, 4) + CONVERT(NVARCHAR, CEILING(CONVERT(NUMERIC, SUBSTRING(DT_QUARTER_BEFORE, 5, 2)) / 3.0)) AS QUARTER_BEFORE,
	  	   LEFT(DY.DAY, 4) AS YEAR,
	  	   LEFT(DY.DT_YEAR_BEFORE, 4) AS YEAR_BEFORE,
	  	   DY.EXC_R,
	  	   DY.YEN_EXC_R,
	  	   DY.EURO_EXC_R,
	  	   DY.CALL_R,
	  	   DY.KOSPI,
	  	   DY.DOW_JONES,
	       DY.NASDAQ,
	  	   DY.WTI
	  FROM (SELECT DY.DAY,
	  		       DY.EXC_R,
	  	           DY.YEN_EXC_R,
	  	           DY.EURO_EXC_R,
	  	           DY.CALL_R,
	  	           DY.KOSPI,
	  	           DY.DOW_JONES,
	               DY.NASDAQ,
	  	           DY.WTI,
	  		       CONVERT(CHAR(8), DATEADD(QUARTER, -1, DY.DAY), 112) AS DT_QUARTER_BEFORE,
	  		       CONVERT(CHAR(8), DATEADD(YEAR, -1, DY.DAY), 112) AS DT_YEAR_BEFORE
	        FROM CZ_LN_CURR_DAY DY) DY
	  WHERE DY.EXC_R IS NOT NULL
),
DT_MONTH AS
(
	SELECT MN.CURR_BAL, 
	       MN.CAPIT_BAL, 
	       MN.KOSPI, 
	       MN.DOW_JONES, 
	       MN.NASDAQ, 
	       MN.CUS_PR_K, 
	       MN.CUS_PR_US, 
	       MN.PROD_PR_K, 
	       MN.PROD_PR_US, 
	       MN.INDUST_K, 
	       MN.INDUST_US, 
	       MN.WTI, 
	       MN.US_FED_R,
	       (SELECT MIN(DAY) 
	        FROM CZ_LN_CURR_DAY
	        WHERE EXC_R IS NOT NULL
		   AND LEFT(DAY, 6) = MN.MONTH) AS DAY
    FROM CZ_LN_CURR_MONTH MN
),
DT_FORECAST AS
(
	SELECT DT_QUARTER,
		   [삼성선물],
	       [신한은행],
	       [우리은행],
	       [Barclays],
	       [BNK부산은행],
	       [Citigroup],
	       [Credit Agricole],
	       [ING],
	       [ING Financial Markets],
	       [JPMorgan Chase],
	       [KB국민은행],
	       [KDB산업은행],
	       [Morgan Stanley],
	       [Societe Generale],
	       [Standard Chartered],
	       [Trading Economics],
	       [Wells Fargo]
	FROM (SELECT TP_GUBUN,
				 DT_QUARTER,
				 RT_USD,
		  	     ROW_NUMBER() OVER (PARTITION BY TP_GUBUN, DT_QUARTER ORDER BY DT_MONTH DESC) AS IDX 
		  FROM CZ_LN_CURR_FORECAST) A
	PIVOT (MAX(A.RT_USD) FOR TP_GUBUN IN ([삼성선물],
										  [신한은행],
										  [우리은행],
										  [Barclays],
										  [BNK부산은행],
										  [Citigroup],
										  [Credit Agricole],
										  [ING],
										  [ING Financial Markets],
										  [JPMorgan Chase],
										  [KB국민은행],
										  [KDB산업은행],
										  [Morgan Stanley],
										  [Societe Generale],
										  [Standard Chartered],
										  [Trading Economics],
										  [Wells Fargo])) AS PVT
	WHERE IDX = 1
)
SELECT DY.DAY,
	   DY.EXC_R,
	   DY.YEN_EXC_R,
	   DY.EURO_EXC_R,
	   DY.CALL_R,
	   DY.KOSPI, 
	   DY.DOW_JONES, 
	   DY.NASDAQ, 
	   DY.WTI, 
	   MN.CURR_BAL, 
	   MN.CAPIT_BAL, 
	   MN.CUS_PR_K, 
	   MN.CUS_PR_US, 
	   MN.PROD_PR_K, 
	   MN.PROD_PR_US, 
	   MN.INDUST_K, 
	   MN.INDUST_US, 
	   MN.US_FED_R,
	   ISNULL(QT.EC_R_K, (SELECT QT.EC_R_K 
						  FROM CZ_LN_CURR_QUARTER QT
						  WHERE QT.QUARTER = DY.QUARTER_BEFORE)) AS EC_R_K,
	   ISNULL(QT.EC_R_US, (SELECT QT.EC_R_US 
						   FROM CZ_LN_CURR_QUARTER QT
						   WHERE QT.QUARTER = DY.QUARTER_BEFORE)) AS EC_R_US,
	   ISNULL(YR.GDP_K, (SELECT YR.GDP_K 
						 FROM CZ_LN_CURR_YEAR YR
					     WHERE YR.YEAR = DY.YEAR_BEFORE)) AS GDP_K,
	   ISNULL(YR.GDP_US, (SELECT YR.GDP_US 
						  FROM CZ_LN_CURR_YEAR YR
					      WHERE YR.YEAR = DY.YEAR_BEFORE)) AS GDP_US,
	   [삼성선물] AS RT_FORE1,
	   [신한은행] AS RT_FORE2,
	   [우리은행] AS RT_FORE3,
	   [Barclays] AS RT_FORE4,
	   [BNK부산은행] AS RT_FORE5,
	   [Citigroup] AS RT_FORE6,
	   [Credit Agricole] AS RT_FORE7,
	   [ING] AS RT_FORE8,
	   [ING Financial Markets] AS RT_FORE9,
	   [JPMorgan Chase] AS RT_FORE10,
	   [KB국민은행] AS RT_FORE11,
	   [KDB산업은행] AS RT_FORE12,
	   [Morgan Stanley] AS RT_FORE13,
	   [Societe Generale] AS RT_FORE14,
	   [Standard Chartered] AS RT_FORE15,
	   [Trading Economics] AS RT_FORE16,
	   [Wells Fargo] AS RT_FORE17
FROM DT_DAY DY
LEFT JOIN DT_MONTH MN ON MN.DAY = DY.DAY
LEFT JOIN DT_FORECAST FC ON FC.DT_QUARTER = DY.QUARTER
LEFT JOIN CZ_LN_CURR_QUARTER QT ON QT.QUARTER = DY.QUARTER
LEFT JOIN CZ_LN_CURR_YEAR YR ON YR.YEAR = DY.YEAR



GO

