USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_IV_AUTO_S]    Script Date: 2016-03-30 오후 1:48:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
           
ALTER PROCEDURE [NEOE].[SP_CZ_PU_IV_AUTO_S]                
(
	@P_CD_COMPANY           NVARCHAR(7),
    @P_NO_IO                NVARCHAR(MAX)
)                
AS                

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

WITH A AS
(
	SELECT IH.CD_COMPANY,
		   IH.NO_IO,
		   IH.CD_PARTNER,
		   IH.YN_RETURN,
		   IH.FG_TRANS
	FROM MM_QTIOH IH
	WHERE IH.CD_COMPANY = @P_CD_COMPANY
	AND EXISTS (SELECT 1
			    FROM STRING_SPLIT(@P_NO_IO, ',')
				WHERE VALUE = IH.NO_IO)	
)
SELECT IH.CD_PARTNER,
	   IH.FG_TRANS,
	   IL.FG_TPPURCHASE,
	   IL.FG_TAX,
	   IL.CD_EXCH,
	   IL.RT_EXCH,
	   MP.DT_PAY_PREARRANGED,
	   (CASE WHEN IH.YN_RETURN = 'Y' THEN -ISNULL(IL.AM, 0) ELSE ISNULL(IL.AM, 0) END) AS AM,
	   (CASE WHEN IH.YN_RETURN = 'Y' THEN -ISNULL(IL.VAT, 0) ELSE ISNULL(IL.VAT, 0) END) AS VAT,
       (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ISNULL(IL.AM, 0) + ISNULL(IL.VAT, 0))  
									 ELSE (ISNULL(IL.AM, 0) + ISNULL(IL.VAT, 0)) END) AS AM_TOT,
	   ISNULL(IL.AP_AM, 0) AS AM_ADPAY
FROM A IH
JOIN MA_PARTNER MP ON MP.CD_COMPANY = IH.CD_COMPANY AND MP.CD_PARTNER = IH.CD_PARTNER
JOIN (SELECT IL.CD_COMPANY, IL.NO_IO,
			 MAX(IL.FG_TPIO) AS FG_TPPURCHASE,
             MAX(IL.FG_TAX) AS FG_TAX,
             MAX(IL.CD_EXCH) AS CD_EXCH,
             MAX(IL.RT_EXCH) AS RT_EXCH,
             SUM(ROUND(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END))) AS AM,
             SUM(ROUND(ROUND(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) * (CONVERT(NUMERIC(17, 4), CASE WHEN ISNULL(MC.CD_FLAG1,'') = '' THEN '0' ELSE MC.CD_FLAG1 END) / 100), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END))) AS VAT,
             SUM(ROUND(ISNULL(AP.AM, 0) * ((ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)) / ISNULL(PL.QT_PO_MM, 1)), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END))) AS AP_AM
      FROM MM_QTIO IL
	  LEFT JOIN PU_POL PL ON PL.CD_COMPANY = IL.CD_COMPANY AND PL.NO_PO = IL.NO_PSO_MGMT AND PL.NO_LINE = IL.NO_PSOLINE_MGMT
	  LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = IL.CD_COMPANY AND MC.CD_FIELD = 'MA_B000046' AND MC.CD_SYSDEF = IL.FG_TAX
	  LEFT JOIN MA_EXCHANGE EX ON EX.CD_COMPANY = IL.CD_COMPANY AND EX.YYMMDD = IL.DT_IO AND EX.NO_SEQ = '1' AND EX.CURR_SOUR = IL.CD_EXCH AND EX.CURR_DEST = '000'
	  LEFT JOIN (SELECT AP.CD_COMPANY, AP.NO_PO, AP.NO_POLINE, 
				 	    SUM(AP.AM) AS AM
				 FROM PU_ADPAYMENT AP
				 WHERE EXISTS (SELECT 1
				 			   FROM (SELECT AP1.CD_COMPANY, AP1.NO_ADPAY, AP1.AM AS AM_ADPAY, 0 AS AM_BILLS
				 			   	     FROM PU_ADPAYMENT AP1
				 				     WHERE AP1.CD_COMPANY = AP.CD_COMPANY
				 				     AND AP1.NO_ADPAY = AP.NO_ADPAY
				 			   	     UNION ALL
				 			   	     SELECT AP1.CD_COMPANY, AP1.NO_ADPAY, 0 AS AM_ADPAY, AP1.AM_BILLS AS AM_BILLS 
				 			   	     FROM CZ_PU_ADPAYMENT_BILL_L AP1
				 				     WHERE AP1.CD_COMPANY = AP.CD_COMPANY
				 				     AND AP1.NO_ADPAY = AP.NO_ADPAY) AP
				 			   GROUP BY AP.CD_COMPANY, AP.NO_ADPAY
				 			   HAVING SUM(AP.AM_ADPAY) > SUM(AP.AM_BILLS))
				 GROUP BY AP.CD_COMPANY, AP.NO_PO, AP.NO_POLINE) AP
	  ON AP.CD_COMPANY = IL.CD_COMPANY AND AP.NO_PO = IL.NO_PSO_MGMT AND AP.NO_POLINE = IL.NO_PSOLINE_MGMT
	  WHERE IL.QT_IO > IL.QT_CLS
	  AND ISNULL(IL.YN_AM, 'N') = 'Y'
      GROUP BY IL.CD_COMPANY, IL.NO_IO) IL
ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IO = IH.NO_IO

GO