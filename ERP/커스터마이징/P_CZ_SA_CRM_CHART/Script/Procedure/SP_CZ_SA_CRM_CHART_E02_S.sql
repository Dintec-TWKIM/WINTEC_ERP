USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_CHART_E02_S]    Script Date: 2017-07-13 오후 3:13:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_CHART_E02_S]
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

SELECT ME.NM_KOR AS NM_EMP,
	   QH.NO_EMP,
	   ISNULL(QH.QT_QTN, 0) AS QT_QTN,
	   ISNULL(QH.QT_SO, 0) AS QT_SO,
	   ISNULL(QH.AM_QTN, 0) AS AM_QTN,
	   ISNULL(QH.AM_SO, 0) AS AM_SO,
	   ROUND((CASE WHEN ISNULL(QH.QT_QTN, 0) = 0 THEN 0
												 ELSE (ISNULL(QH.QT_SO, 0) / ISNULL(QH.QT_QTN, 0)) * 100 END), 2) AS RT_QT_SO,
	   ROUND((CASE WHEN ISNULL(QH.AM_QTN, 0) = 0 THEN 0
												 ELSE (ISNULL(QH.AM_SO, 0) / ISNULL(QH.AM_QTN, 0)) * 100 END), 2) AS RT_AM_SO
FROM (SELECT QH.CD_COMPANY, QH.NO_EMP,
	  		 SUM(1.0) AS QT_QTN,
	  		 SUM(CASE WHEN SH.CD_COMPANY IS NOT NULL THEN 1.0 ELSE 0 END) AS QT_SO,
	  		 SUM(QL.AM_QTN) AS AM_QTN,
	  		 SUM(QL.AM_SO) AS AM_SO
	  FROM CZ_SA_QTNH QH
	  LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = QH.CD_COMPANY AND SH.NO_SO = QH.NO_FILE
	  LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
	  LEFT JOIN (SELECT QL.CD_COMPANY, QL.NO_FILE,
	  				    SUM(QL.AM_KR_S) AS AM_QTN,
						SUM(SL.AM_KR_S) AS AM_SO
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
	  GROUP BY QH.CD_COMPANY, QH.NO_EMP) QH
JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP
WHERE ISNULL(ME.DT_RETIRE, '00000000') = '00000000'
ORDER BY QH.QT_SO DESC

GO