USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_CHART_A04_S]    Script Date: 2017-07-13 오후 3:13:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_CHART_A04_S]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_DT_FROM			NVARCHAR(8),
	@P_DT_TO			NVARCHAR(8),
	@P_CD_PARTNER		NVARCHAR(1000),
	@P_NO_IMO			NVARCHAR(10),
	@P_NO_EMP			NVARCHAR(10),
	@P_CD_ITEMGRP		NVARCHAR(20),
	@P_CD_SALEORG		NVARCHAR(7),
	@P_CD_SALEGRP		NVARCHAR(1000),
	@P_LENGTH			NUMERIC(1)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT MP.LN_PARTNER,
	   QH.CD_PARTNER,
	   ISNULL(COUNT(1), 0) AS QT_QTN_FILE,
	   ISNULL(SUM(CASE WHEN ISNULL(SH.NO_SO, '') = '' THEN 0 ELSE 1 END), 0) AS QT_SO_FILE,
	   ISNULL(SUM(QL.QT_QTN_ITEM), 0) AS QT_QTN_ITEM,
	   ISNULL(SUM(QL.QT_SO_ITEM), 0) AS QT_SO_ITEM,
	   (CASE WHEN CONVERT(FLOAT, ISNULL(COUNT(1), 0)) = 0 THEN 0
														  ELSE ROUND(CONVERT(FLOAT, ISNULL(SUM(CASE WHEN ISNULL(SH.NO_SO, '') = '' THEN 0 ELSE 1 END), 0)) / CONVERT(FLOAT, ISNULL(COUNT(1), 0)) * 100, 2) END) AS RT_SO_FILE,
	   (CASE WHEN CONVERT(FLOAT, ISNULL(SUM(QL.QT_QTN_ITEM), 0)) = 0 THEN 0
																	 ELSE ROUND(CONVERT(FLOAT, ISNULL(SUM(QL.QT_SO_ITEM), 0)) / CONVERT(FLOAT, ISNULL(SUM(QL.QT_QTN_ITEM), 0)) * 100, 2) END) AS RT_SO_ITEM
FROM CZ_SA_QTNH QH
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = QH.CD_COMPANY AND SH.NO_SO = QH.NO_FILE
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
JOIN (SELECT QL.CD_COMPANY, QL.NO_FILE, 
	  		 COUNT(1) AS QT_QTN_ITEM,
			 SUM(CASE WHEN SL.CD_COMPANY IS NOT NULL THEN 1 ELSE 0 END) AS QT_SO_ITEM
	  FROM CZ_SA_QTNL QL
	  LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
	  WHERE (ISNULL(@P_CD_ITEMGRP, '') = '' OR QL.GRP_ITEM = @P_CD_ITEMGRP)
	  GROUP BY QL.CD_COMPANY, QL.NO_FILE) QL 
ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
WHERE QH.CD_COMPANY = @P_CD_COMPANY
AND QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO
AND (ISNULL(@P_CD_PARTNER, '') = '' OR QH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
AND (ISNULL(@P_NO_IMO, '') = '' OR QH.NO_IMO = @P_NO_IMO)
AND (ISNULL(@P_NO_EMP, '') = '' OR QH.NO_EMP = @P_NO_EMP)
AND (ISNULL(@P_CD_SALEORG, '') = '' OR SG.CD_SALEORG = @P_CD_SALEORG)
AND (ISNULL(@P_CD_SALEGRP, '') = '' OR QH.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
GROUP BY QH.CD_PARTNER, MP.LN_PARTNER
ORDER BY ISNULL(SUM(CASE WHEN ISNULL(SH.NO_SO, '') = '' THEN 0 ELSE 1 END), 0) DESC

GO