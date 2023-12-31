USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_PARTNER_HISTORY2_S]    Script Date: 2017-05-25 오후 1:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_PARTNER_HISTORY2_S]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PARTNER		NVARCHAR(20),
	@P_DT_START			NVARCHAR(8),
	@P_DT_END			NVARCHAR(8)
) 
AS
 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT QH.NO_FILE,
	   ME.NM_KOR AS NM_EMP,
	   MI.NM_ITEMGRP,
	   QH.DT_INQ,
	   QH.DT_QTN,
	   QL.DT_PO,
	   QL.DT_IN,
	   QL.LT,
	   DATEDIFF(DAY, QL.DT_PO, QL.DT_IN) AS LT_IN,
	   1 AS QT_INQ,
	   (CASE WHEN QL.CD_COMPANY IS NULL THEN 0 ELSE 1 END) AS QT_QTN,
	   (CASE WHEN QL.QT_SO > 0 THEN 1 ELSE 0 END) AS QT_SO,
	   (CASE WHEN QL.DT_PO IS NULL THEN 0 ELSE 1 END) AS QT_PO,
	   (CASE WHEN QL.DT_PO IS NULL AND QL.QT_STOCK > 0 THEN 1 ELSE 0 END) AS QT_STOCK,
	   QH.AM_INQ,
	   QL.AM_PO_QTN,
	   QL.AM_SO_QTN,
	   QL.AM_SO,
	   QL.AM_SO_PO,
	   QL.AM_PO,
	   QL.AM_STOCK,
	   (ISNULL(QL.AM_SO_QTN, 0) - ISNULL(QL.AM_PO_QTN, 0)) AS AM_PROFIT_QTN,
	   (ISNULL(QL.AM_SO_PO, 0) - ISNULL(QL.AM_PO, 0)) AS AM_PROFIT_PO,
	   (ISNULL(QL.AM_SO, 0) - (ISNULL(QL.AM_PO, 0) + ISNULL(QL.AM_STOCK, 0))) AS AM_PROFIT,
	   ROUND(CASE WHEN ISNULL(QL.AM_SO_QTN, 0) = 0 THEN 0
											       ELSE ((ISNULL(QL.AM_SO_QTN, 0) - ISNULL(QL.AM_PO_QTN, 0)) / ISNULL(QL.AM_SO_QTN, 0)) * 100 END, 2) AS RT_PROFIT_QTN,
	   ROUND(CASE WHEN ISNULL(QL.AM_SO_PO, 0) = 0 THEN 0
											      ELSE ((ISNULL(QL.AM_SO_PO, 0) - ISNULL(QL.AM_PO_QTN, 0)) / ISNULL(QL.AM_SO_PO, 0)) * 100 END, 2) AS RT_PROFIT_PO,
	   ROUND(CASE WHEN ISNULL(QL.AM_SO, 0) = 0 THEN 0
											   ELSE ((ISNULL(QL.AM_SO, 0) - (ISNULL(QL.AM_PO, 0) + ISNULL(QL.AM_STOCK, 0))) / ISNULL(QL.AM_SO, 0)) * 100 END, 2) AS RT_PROFIT
FROM (SELECT QH.CD_COMPANY, QH.NO_FILE, QH.CD_PARTNER, 
			 QH1.NO_EMP, QH.DT_INQ, QH.DT_QTN,
			 MAX(QL1.GRP_ITEM) AS GRP_ITEM,
			 SUM(QL.AM_KR) AS AM_INQ
	  FROM CZ_PU_QTNH QH
	  JOIN CZ_SA_QTNH QH1 ON QH1.CD_COMPANY = QH.CD_COMPANY AND QH1.NO_FILE = QH.NO_FILE
	  JOIN CZ_PU_QTNL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE AND QL.CD_PARTNER = QH.CD_PARTNER
	  JOIN CZ_SA_QTNL QL1 ON QL1.CD_COMPANY = QL.CD_COMPANY AND QL1.NO_FILE = QL.NO_FILE AND QL1.NO_LINE = QL.NO_LINE
	  WHERE ISNULL(QL1.TP_BOM, 'S') IN ('S', 'P')
	  GROUP BY QH.CD_COMPANY, QH.NO_FILE, QH.CD_PARTNER,
			   QH1.NO_EMP, QH.DT_INQ, QH.DT_QTN) QH
LEFT JOIN (SELECT QL.CD_COMPANY, QL.NO_FILE, QL.CD_PARTNER,
		   	      MAX(PH.DT_PO) AS DT_PO,
		   	      MAX(IL.DT_IO) AS DT_IN,
				  MAX(PL.LT) AS LT,
				  SUM(SL.QT_SO) AS QT_SO,
				  SUM(SB.QT_STOCK) AS QT_STOCK,
		   	      SUM(QL1.AM_KR_P) AS AM_PO_QTN,
		   	      SUM(QL1.AM_KR_S) AS AM_SO_QTN,
		   	      SUM(SL.AM_KR_S) AS AM_SO,
				  SUM(ISNULL(SL.UM_KR_S, 0) * (ISNULL(SL.QT_SO, 0) - ISNULL(SB.QT_STOCK, 0))) AS AM_SO_PO,
		   	      SUM(PL.AM) AS AM_PO,
		   	      SUM(SB.UM_KR * SB.QT_STOCK) AS AM_STOCK  
		   FROM CZ_PU_QTNL QL
		   JOIN CZ_SA_QTNL QL1 ON QL1.CD_COMPANY = QL.CD_COMPANY AND QL1.NO_FILE = QL.NO_FILE AND QL1.NO_LINE = QL.NO_LINE
		   LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
		   LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = QL.CD_COMPANY AND SB.NO_FILE = QL.NO_FILE AND SB.NO_LINE = QL.NO_LINE
		   LEFT JOIN PU_POL PL ON PL.CD_COMPANY = QL.CD_COMPANY AND PL.NO_SO = QL.NO_FILE AND PL.NO_LINE = QL.NO_LINE
		   LEFT JOIN PU_POH PH ON PH.CD_COMPANY = PL.CD_COMPANY AND PH.NO_PO = PL.NO_PO
		   LEFT JOIN (SELECT IH.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT,
		   				     MAX(IH.DT_IO) AS DT_IO 
		   		      FROM MM_QTIOH IH
		   		      LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IO = IH.NO_IO
		   		      GROUP BY IH.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT) IL
		   ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
		   WHERE QL.YN_CHOICE = 'Y'
		   GROUP BY QL.CD_COMPANY, QL.NO_FILE, QL.CD_PARTNER) QL
ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE AND QL.CD_PARTNER = QH.CD_PARTNER
LEFT JOIN MA_ITEMGRP MI ON MI.CD_COMPANY = QH.CD_COMPANY AND MI.USE_YN = 'Y' AND MI.CD_ITEMGRP = QH.GRP_ITEM
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP
WHERE QH.CD_COMPANY = @P_CD_COMPANY
AND QH.CD_PARTNER = @P_CD_PARTNER
AND QH.DT_QTN BETWEEN @P_DT_START AND @P_DT_END

GO