USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_PARTNER_PIC_HULL_ITEM_GROUP_S]    Script Date: 2018-04-13 오후 1:32:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_PARTNER_PIC_HULL_LIST_S]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_IMO			NVARCHAR(10),
	@P_DT_START			NVARCHAR(8),
	@P_DT_END			NVARCHAR(8),
	@P_TP_LIST			NVARCHAR(3)
)
AS
 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

IF @P_TP_LIST = '001'
BEGIN
	SELECT IG.NM_ITEMGRP,
		   COUNT(1) AS QT_QTN,
		   SUM(CASE WHEN ISNULL(SH.NO_SO, '') = '' THEN 0 ELSE 1 END) AS QT_SO,
		   (CASE WHEN CONVERT(FLOAT, COUNT(1)) = 0 THEN 0
												   ELSE ROUND(CONVERT(FLOAT, SUM(CASE WHEN ISNULL(SH.NO_SO, '') = '' THEN 0 ELSE 1 END)) / CONVERT(FLOAT, COUNT(1)) * 100, 2) END) AS RT_SO,
		   SUM(QL.AM_QTN) AS AM_QTN,
		   SUM(QL.AM_SO) AS AM_SO,
		   SUM(QL.AM_PO) AS AM_PO,
		   SUM(QL.AM_STOCK) AS AM_STOCK,
		   SUM(ISNULL(QL.AM_SO, 0) - (ISNULL(QL.AM_PO, 0) + ISNULL(QL.AM_STOCK, 0))) AS AM_PROFIT,
		   (CASE WHEN SUM(QL.AM_SO) = 0 THEN 0
			   						    ELSE ROUND(SUM(ISNULL(QL.AM_SO, 0) - (ISNULL(QL.AM_PO, 0) + ISNULL(QL.AM_STOCK, 0))) / SUM(QL.AM_SO) * 100, 2) END) AS RT_PROFIT 
	FROM CZ_SA_QTNH QH
	JOIN (SELECT QL.CD_COMPANY, QL.NO_FILE, QL.GRP_ITEM,
		  		 SUM(QL.AM_KR_S) AS AM_QTN,
		         SUM(SL.AM_KR_S) AS AM_SO,
		         SUM(PL.AM) AS AM_PO,
		         SUM(ISNULL(SB.QT_STOCK, 0) * ISNULL(SB.UM_KR, 0)) AS AM_STOCK 
		  FROM CZ_SA_QTNL QL
		  LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
		  LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
		  LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
		  					SUM(PL.AM) AS AM 
		  		     FROM PU_POL PL
		  			 GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
		  ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
		  WHERE ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%'
		  GROUP BY QL.CD_COMPANY, QL.NO_FILE, QL.GRP_ITEM) QL
	ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
	LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = QH.CD_COMPANY AND SH.NO_SO = QH.NO_FILE
	LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = QL.CD_COMPANY AND IG.CD_ITEMGRP = QL.GRP_ITEM
	WHERE QH.CD_COMPANY = @P_CD_COMPANY
	AND QH.NO_IMO = @P_NO_IMO
	AND QH.DT_QTN BETWEEN @P_DT_START AND @P_DT_END
	GROUP BY IG.NM_ITEMGRP	
END
ELSE IF @P_TP_LIST = '002'
BEGIN
	SELECT MP.LN_PARTNER, PO.CD_PARTNER,
		   SUM(PO.QT_INQ) AS QT_INQ,
		   SUM(PO.QT_PO) AS QT_PO,
		   SUM(PO.QT_STOCK) AS QT_STOCK,
		   SUM(PO.AM_INQ) AS AM_INQ,
		   SUM(PO.AM_PO) AS AM_PO,
		   SUM(PO.AM_STOCK) AS AM_STOCK,
		   (CASE WHEN SUM(PO.QT_INQ) = 0 THEN 0
									     ELSE ROUND(SUM(ISNULL(PO.QT_PO, 0) + ISNULL(PO.QT_STOCK, 0)) / SUM(PO.QT_INQ) * 100, 2) END) AS RT_PO 
	FROM CZ_SA_QTNH QH
	JOIN (SELECT QL.CD_COMPANY, QL.NO_FILE, QL.CD_PARTNER,
				 1.0 AS QT_INQ,
				 0.0 AS QT_PO,
				 0.0 AS QT_STOCK,
	      	     SUM(QL.AM_KR) AS AM_INQ,
				 0 AS AM_PO,
				 0 AS AM_STOCK 
	      FROM CZ_PU_QTNL QL
	      GROUP BY QL.CD_COMPANY, QL.NO_FILE, QL.CD_PARTNER
	      UNION ALL
	      SELECT PL.CD_COMPANY, PL.NO_SO, PH.CD_PARTNER,
				 0.0 AS QT_INQ,
				 1.0 AS QT_PO,
				 0.0 AS QT_STOCK,
				 0 AS AM_INQ,
	      	     SUM(PL.AM) AS AM_PO,
				 0 AS AM_STOCK
	      FROM PU_POL PL
	      JOIN PU_POH PH ON PH.CD_COMPANY = PL.CD_COMPANY AND PH.NO_PO = PL.NO_PO
	      GROUP BY PL.CD_COMPANY, PL.NO_SO, PH.CD_PARTNER
	      UNION ALL
	      SELECT SB.CD_COMPANY, SB.NO_FILE, 'STOCK' AS CD_PARTNER,
				 0.0 AS QT_INQ,
				 0.0 AS QT_PO,
				 1.0 AS QT_STOCK,
				 0 AS AM_INQ,
				 0 AS AM_PO,
	      	     SUM(ISNULL(SB.QT_STOCK, 0) * ISNULL(SB.UM_KR, 0)) AS AM_STOCK 
	      FROM CZ_SA_STOCK_BOOK SB
	      GROUP BY SB.CD_COMPANY, SB.NO_FILE) PO
	ON PO.CD_COMPANY = QH.CD_COMPANY AND PO.NO_FILE = QH.NO_FILE
	JOIN MA_PARTNER MP ON MP.CD_COMPANY = PO.CD_COMPANY AND MP.CD_PARTNER = PO.CD_PARTNER
	WHERE QH.CD_COMPANY = @P_CD_COMPANY
	AND QH.NO_IMO = @P_NO_IMO
	AND QH.DT_QTN BETWEEN @P_DT_START AND @P_DT_END
	GROUP BY PO.CD_PARTNER, MP.LN_PARTNER
	ORDER BY SUM(PO.AM_INQ) DESC
END
ELSE IF @P_TP_LIST = '003'
BEGIN
	SELECT LEFT(QH.DT_QTN, 6) AS DT_QTN,
		   COUNT(1) AS QT_QTN,
		   SUM(CASE WHEN ISNULL(SH.NO_SO, '') = '' THEN 0 ELSE 1 END) AS QT_SO,
		   (CASE WHEN CONVERT(FLOAT, COUNT(1)) = 0 THEN 0
												   ELSE ROUND(CONVERT(FLOAT, SUM(CASE WHEN ISNULL(SH.NO_SO, '') = '' THEN 0 ELSE 1 END)) / CONVERT(FLOAT, COUNT(1)) * 100, 2) END) AS RT_SO,
		   SUM(QL.AM_QTN) AS AM_QTN,
		   SUM(QL.AM_SO) AS AM_SO,
		   SUM(QL.AM_PO) AS AM_PO,
		   SUM(QL.AM_STOCK) AS AM_STOCK,
		   SUM(ISNULL(QL.AM_SO, 0) - (ISNULL(QL.AM_PO, 0) + ISNULL(QL.AM_STOCK, 0))) AS AM_PROFIT,
		   (CASE WHEN SUM(QL.AM_SO) = 0 THEN 0
			   						    ELSE ROUND(SUM(ISNULL(QL.AM_SO, 0) - (ISNULL(QL.AM_PO, 0) + ISNULL(QL.AM_STOCK, 0))) / SUM(QL.AM_SO) * 100, 2) END) AS RT_PROFIT 
	FROM CZ_SA_QTNH QH
	JOIN (SELECT QL.CD_COMPANY, QL.NO_FILE,
		  		 SUM(QL.AM_KR_S) AS AM_QTN,
		         SUM(SL.AM_KR_S) AS AM_SO,
		         SUM(PL.AM) AS AM_PO,
		         SUM(ISNULL(SB.QT_STOCK, 0) * ISNULL(SB.UM_KR, 0)) AS AM_STOCK 
		  FROM CZ_SA_QTNL QL
		  LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
		  LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
		  LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
		  					SUM(PL.AM) AS AM 
		  		     FROM PU_POL PL
		  			 GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
		  ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
		  WHERE ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%'
		  GROUP BY QL.CD_COMPANY, QL.NO_FILE) QL
	ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
	LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = QH.CD_COMPANY AND SH.NO_SO = QH.NO_FILE
	WHERE QH.CD_COMPANY = @P_CD_COMPANY
	AND QH.NO_IMO = @P_NO_IMO
	AND QH.DT_QTN BETWEEN @P_DT_START AND @P_DT_END
	GROUP BY LEFT(QH.DT_QTN, 6)	
END
GO


