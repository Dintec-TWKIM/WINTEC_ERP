USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_CHART_K04_S]    Script Date: 2017-07-13 오후 3:13:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_CHART_K04_S]
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

SELECT PH.NO_PO, PH.DT_PO, PL.NM_ITEM_PARTNER,
	   PH.CD_PARTNER AS CD_SUPPLIER,
	   MP.LN_PARTNER AS NM_SUPPLIER,
	   PL.CD_PARTNER AS CD_PARTNER,
	   PL.LN_PARTNER AS LN_PARTNER,
	   PL.NM_VESSEL,
	   (REPLACE(REPLACE(REPLACE(REPLACE(PL.NM_ITEM_PARTNER, ' ', ''), CHAR(9), ''), CHAR(10), ''), CHAR(13), '')) AS NM_KEY,
	   PL.UM_PO,
	   PL.UM_QTN,
	   PL.UM_SO
FROM PU_POH PH
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
JOIN (SELECT PL.CD_COMPANY, PL.NO_PO, QL.NM_ITEM_PARTNER,
			 MAX(QH.CD_PARTNER) AS CD_PARTNER,
			 MAX(MP.LN_PARTNER) AS LN_PARTNER,
			 MAX(MH.NM_VESSEL) AS NM_VESSEL,
			 MAX(PL.UM) AS UM_PO,
			 MAX(QL.UM_KR_S) AS UM_QTN,
			 MAX(SL.UM_KR_S) AS UM_SO 
	  FROM PU_POL PL
	  LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = PL.CD_COMPANY AND QL.NO_FILE = PL.NO_SO AND QL.NO_LINE = PL.NO_SOLINE
	  LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = QL.CD_COMPANY AND QH.NO_FILE = QL.NO_FILE
	  LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
	  LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
	  LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
	  WHERE ISNULL(QL.NM_ITEM_PARTNER, '') <> ''
	  GROUP BY PL.CD_COMPANY, PL.NO_PO, QL.NM_ITEM_PARTNER) PL
ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
WHERE PH.CD_COMPANY = @P_CD_COMPANY
AND PH.DT_PO BETWEEN @P_DT_FROM AND @P_DT_TO
AND (ISNULL(@P_CD_PARTNER, '') = '' OR PH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
AND (ISNULL(@P_NO_EMP, '') = '' OR PH.NO_EMP = @P_NO_EMP)

GO