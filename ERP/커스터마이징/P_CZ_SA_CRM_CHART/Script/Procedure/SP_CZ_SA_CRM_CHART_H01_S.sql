USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_CHART_H01_S]    Script Date: 2017-07-13 오후 3:13:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_CHART_H01_S]
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

SELECT ISNULL(MP.LN_PARTNER, '') AS LN_PARTNER,
	   MH.CD_PARTNER,
	   COUNT(1) AS QT_HULL
FROM CZ_MA_HULL MH
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = @P_CD_COMPANY AND MP.CD_PARTNER = MH.CD_PARTNER
WHERE 1=1
AND (ISNULL(@P_CD_PARTNER, '') = '' OR MH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
AND (ISNULL(@P_NO_IMO, '') = '' OR MH.NO_IMO = @P_NO_IMO)
GROUP BY MH.CD_PARTNER, MP.LN_PARTNER
ORDER BY COUNT(1) DESC

GO