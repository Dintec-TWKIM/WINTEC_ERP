USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_PAYMENT_DUE_DATE_S]    Script Date: 2019-02-26 ���� 3:00:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PU_PAYMENT_DUE_DATE_S]
(
	@P_CD_COMPANY	NVARCHAR(7)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT IH.NO_IV,
	   MP.LN_PARTNER,
	   FD.NO_DOCU,
	   FD2.NO_DOCU,
	   IH.DT_PROCESS,
	   IH.DT_PAY_PREARRANGED,
	   DATEDIFF(DAY, IH.DT_PAY_PREARRANGED, CONVERT(CHAR(8), GETDATE(), 112)) AS DT_DIFF,
	   MC.NM_SYSDEF AS NM_EXCH,
	   IH.RT_EXCH,
	   IH.AM_EX,
	   IH.AM_K,
	   IH.VAT_TAX,
	   (ISNULL(IH.AM_K, 0) + ISNULL(IH.VAT_TAX, 0)) AS AM_TOTAL,
	   ISNULL(FD1.AM_DOCU, 0) AS AM_DOCU,
	   ISNULL(FD2.AM_DOCU, 0) AS AM_DOCU1,
	   (ISNULL(FD1.AM_DOCU, 0) - ISNULL(FD2.AM_DOCU, 0)) AS AM_REMAIN 
FROM PU_IVH IH
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = IH.CD_COMPANY AND MP.CD_PARTNER = IH.CD_PARTNER
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = IH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = IH.CD_EXCH
LEFT JOIN (SELECT CD_COMPANY, NO_MDOCU, 
				  MAX(NO_DOCU) AS NO_DOCU
		   FROM FI_DOCU
		   GROUP BY CD_COMPANY, NO_MDOCU) FD
ON FD.CD_COMPANY = IH.CD_COMPANY AND FD.NO_MDOCU = IH.NO_IV
LEFT JOIN (SELECT CD_COMPANY, NO_DOCU,
				  SUM(AM_CR) AS AM_DOCU 
		   FROM FI_DOCU
		   WHERE TP_ACAREA = '4'
		   GROUP BY CD_COMPANY, NO_DOCU) FD1
ON FD1.CD_COMPANY = FD.CD_COMPANY AND FD1.NO_DOCU = FD.NO_DOCU
LEFT JOIN (SELECT CD_COMPANY, NO_BDOCU,
			      MAX(NO_DOCU) AS NO_DOCU,
				  SUM(AM_DR) AS AM_DOCU 
		   FROM FI_DOCU
		   GROUP BY CD_COMPANY, NO_BDOCU) FD2
ON FD2.CD_COMPANY = FD.CD_COMPANY AND FD2.NO_BDOCU = FD.NO_DOCU
WHERE IH.CD_COMPANY = @P_CD_COMPANY
AND (ISNULL(FD1.AM_DOCU, 0) = 0 OR ISNULL(FD1.AM_DOCU, 0) > ISNULL(FD2.AM_DOCU, 0))
AND IH.DT_PAY_PREARRANGED <= CONVERT(CHAR(8), DATEADD(DAY, 3, GETDATE()), 112)
AND IH.CD_PARTNER IN ('11823', '00799')
ORDER BY IH.DT_PAY_PREARRANGED

GO