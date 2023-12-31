USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_OUTSTANDING_INVOICE_RPT_IV_S]    Script Date: 2017-12-06 오후 5:51:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_OUTSTANDING_INV_RPT_RCP_S]          
(  
	@P_CD_COMPANY		NVARCHAR(7),
	@P_DT_MONTH			NVARCHAR(6),
	@P_NO_IV			NVARCHAR(20)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT RD.NO_IV,
	   RH.NO_RCP,
       MAX(RH.DT_RCP) AS DT_RCP,
       SUM(RD.AM_RCP) AS AM_RCP,
	   SUM(RD.AM_RCP_A) AS AM_RCP_A
FROM SA_RCPH RH
LEFT JOIN (SELECT RD.CD_COMPANY, RD.NO_RCP, RD.NO_TX AS NO_IV,
  				  (ISNULL(RD.AM_RCP_TX, 0) + ISNULL(RD.AM_PL, 0)) AS AM_RCP,
				  0 AS AM_RCP_A
		   FROM SA_RCPD RD 
		   UNION ALL
		   SELECT BD.CD_COMPANY, BD.NO_RCP, BD.NO_IV,
				  0 AS AM_RCP,
  				  (ISNULL(BD.AM_RCPS, 0) + ISNULL(BD.AM_PL, 0)) AS AM_RCP_A
		   FROM SA_BILLSD BD) RD
ON RD.CD_COMPANY = RH.CD_COMPANY AND RD.NO_RCP = RH.NO_RCP
WHERE RH.CD_COMPANY = @P_CD_COMPANY
AND RD.NO_IV = @P_NO_IV
AND LEFT(RH.DT_RCP, 6) <= @P_DT_MONTH
GROUP BY RD.NO_IV, RH.NO_RCP
ORDER BY RH.NO_RCP DESC

GO

