USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_CHART_P05_S]    Script Date: 2017-07-13 오후 3:13:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_CHART_P05_S]
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

SELECT QL.DT_QTN,
	   QL.AM_QTN,
	   QL.AM_EX_QTN,
	   QL.AM_PO,
	   QL.AM_EX_PO,
	   QL.AM_PROFIT,
	   (CASE WHEN ISNULL(QL.AM_QTN, 0) = 0 THEN 0 
										   ELSE ROUND((ISNULL(QL.AM_PROFIT, 0) / ISNULL(QL.AM_QTN, 0)) * 100, 2) END) AS RT_PROFIT
FROM (SELECT LEFT(QH.DT_QTN, @P_LENGTH) AS DT_QTN,
	  	     SUM(QL.AM_QTN) AS AM_QTN,
			 SUM(QL.AM_EX_QTN) AS AM_EX_QTN,
	  	     SUM(QL.AM_PO) AS AM_PO,
			 SUM(QL.AM_EX_PO) AS AM_EX_PO,
			 SUM(ISNULL(QL.AM_QTN, 0) - ISNULL(QL.AM_PO, 0)) AS AM_PROFIT
	  FROM CZ_SA_QTNH QH
	  LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
	  LEFT JOIN (SELECT CD_COMPANY, NO_FILE,
						SUM(AM_KR_S) AS AM_QTN,
			            SUM(AM_EX_S) AS AM_EX_QTN,
	  	                SUM(AM_KR_P) AS AM_PO,
			            SUM(AM_EX_P) AS AM_EX_PO 
				 FROM CZ_SA_QTNL
				 WHERE CD_ITEM NOT LIKE 'SD%'
				 AND (ISNULL(@P_CD_ITEMGRP, '') = '' OR GRP_ITEM = @P_CD_ITEMGRP)
				 GROUP BY CD_COMPANY, NO_FILE) QL 
      ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
	  WHERE QH.CD_COMPANY = @P_CD_COMPANY
	  AND QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO
	  AND (ISNULL(@P_CD_PARTNER, '') = '' OR QH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
      AND (ISNULL(@P_NO_IMO, '') = '' OR QH.NO_IMO = @P_NO_IMO)
      AND (ISNULL(@P_NO_EMP, '') = '' OR QH.NO_EMP = @P_NO_EMP)
      AND (ISNULL(@P_CD_SALEORG, '') = '' OR SG.CD_SALEORG = @P_CD_SALEORG)
      AND (ISNULL(@P_CD_SALEGRP, '') = '' OR QH.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
	  GROUP BY LEFT(QH.DT_QTN, @P_LENGTH)) QL
ORDER BY QL.DT_QTN

GO