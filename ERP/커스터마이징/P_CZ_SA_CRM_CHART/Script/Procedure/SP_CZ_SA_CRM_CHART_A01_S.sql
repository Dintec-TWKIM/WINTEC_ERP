USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_CHART_A01_S]    Script Date: 2017-07-13 오후 3:13:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_CHART_A01_S]
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
	   SH.CD_PARTNER,
	   ISNULL(SUM(SL.AM_KR_S), 0) AS AM_SO,
	   ISNULL(SUM(SL.AM_EX_S), 0) AS AM_EX_SO 
FROM SA_SOH SH
LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_PLANT = SL.CD_PLANT AND MI.CD_ITEM = SL.CD_ITEM
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = SH.CD_COMPANY AND SG.CD_SALEGRP = SH.CD_SALEGRP
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO
AND SL.CD_ITEM NOT LIKE 'SD%'
AND (ISNULL(@P_CD_PARTNER, '') = '' OR SH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
AND (ISNULL(@P_NO_IMO, '') = '' OR SH.NO_IMO = @P_NO_IMO)
AND (ISNULL(@P_NO_EMP, '') = '' OR SH.NO_EMP = @P_NO_EMP)
AND (ISNULL(@P_CD_ITEMGRP, '') = '' OR MI.GRP_ITEM = @P_CD_ITEMGRP)
AND (ISNULL(@P_CD_SALEORG, '') = '' OR SG.CD_SALEORG = @P_CD_SALEORG)
AND (ISNULL(@P_CD_SALEGRP, '') = '' OR SH.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
GROUP BY SH.CD_PARTNER, MP.LN_PARTNER
ORDER BY SUM(SL.AM_KR_S) DESC

GO

