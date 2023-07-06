USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_PARTNER_SUPPLIERL_S]    Script Date: 2018-06-01 오후 6:15:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_PARTNER_SUPPLIERL_S]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PARTNER		NVARCHAR(20),
	@P_CD_SUPPLIER		NVARCHAR(20),
	@P_DT_START			NVARCHAR(8),
	@P_DT_END			NVARCHAR(8)
) 
AS
 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT QL.CD_SUPPLIER, 
	   ISNULL(QL.CD_ITEM, '') AS CD_ITEM,
	   MAX(QL.NM_ITEM) AS NM_ITEM,
	   COUNT(DISTINCT QL.NO_FILE) AS QT_FILE,
	   COUNT(1) AS QT_ITEM,
	   SUM(CASE WHEN QL.YN_CHOICE = 'Y' THEN 1 ELSE 0 END) AS QT_ITEM_CHOICE,
	   MAX(QL.UM) AS UM,
	   MAX(QL.UM1) AS UM1
FROM (SELECT QL1.CD_PARTNER AS CD_SUPPLIER,
			 QH.NO_FILE,
			 (CASE WHEN MI.CLS_ITEM = '009' THEN QL.CD_ITEM ELSE NULL END) AS CD_ITEM,
			 MI.NM_ITEM,
			 QL.UM_KR AS UM,
			 QL1.UM_KR AS UM1,
			 QL1.YN_CHOICE 
	  FROM CZ_PU_QTNH QH
	  JOIN CZ_PU_QTNL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.CD_PARTNER = QH.CD_PARTNER AND QL.NO_FILE = QH.NO_FILE
	  JOIN CZ_PU_QTNL QL1 ON QL1.CD_COMPANY = QL.CD_COMPANY AND QL1.NO_FILE = QL.NO_FILE AND QL1.NO_LINE = QL.NO_LINE
	  LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = QL.CD_COMPANY AND MI.CD_ITEM = QL.CD_ITEM
	  WHERE QH.CD_COMPANY = @P_CD_COMPANY
	  AND QH.DT_QTN BETWEEN @P_DT_START AND @P_DT_END
	  AND QH.CD_PARTNER = @P_CD_PARTNER
	  AND QL1.CD_PARTNER = @P_CD_SUPPLIER
	  AND ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%'
	  AND ISNULL(QL.AM_KR, 0) <> 0
	  AND ISNULL(QL1.AM_KR, 0) <> 0) QL
GROUP BY QL.CD_SUPPLIER, QL.CD_ITEM
ORDER BY COUNT(1) DESC

GO

