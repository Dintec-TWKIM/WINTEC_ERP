USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_CHART_P08_S]    Script Date: 2017-07-13 오후 3:13:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_CHART_P08_S]
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

SELECT SL.NM_ITEMGRP,
	   SL.AM_SO,
	   SL.AM_EX_SO,
	   (ISNULL(SL.AM_PO, 0) + ISNULL(SL.AM_STOCK, 0)) AS AM_PO_ALL,
	   SL.AM_PO,
	   SL.AM_EX_PO,
	   SL.AM_STOCK,
	   (ISNULL(SL.AM_SO, 0) - (ISNULL(SL.AM_PO, 0) + ISNULL(SL.AM_STOCK, 0))) AS AM_PROFIT,
	   (CASE WHEN ISNULL(SL.AM_SO, 0) = 0 THEN 0 
										  ELSE ROUND(((ISNULL(SL.AM_SO, 0) - (ISNULL(SL.AM_PO, 0) + ISNULL(SL.AM_STOCK, 0))) / ISNULL(SL.AM_SO, 0)) * 100, 2) END) AS RT_PROFIT
FROM (SELECT IG.NM_ITEMGRP,
		  	 SUM(ISNULL(SL.AM_KR_S, SL.AM_WONAMT)) AS AM_SO,
			 SUM(ISNULL(SL.AM_EX_S, SL.AM_SO)) AS AM_EX_SO,
		  	 SUM(ISNULL(SB.QT_STOCK, 0) * ISNULL(SB.UM_KR, 0)) AS AM_STOCK,
		  	 SUM(PL.AM_PO) AS AM_PO,
			 SUM(PL.AM_EX_PO) AS AM_EX_PO
	  FROM SA_SOH SH
	  LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = SH.CD_COMPANY AND SG.CD_SALEGRP = SH.CD_SALEGRP
	  LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	  LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_ITEM = SL.CD_ITEM
	  LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
	  LEFT JOIN (SELECT CD_COMPANY, NO_SO, NO_SOLINE,
	  				    SUM(AM) AS AM_PO,
	  				    SUM(AM_EX) AS AM_EX_PO 
	  		     FROM PU_POL
	  		     GROUP BY CD_COMPANY, NO_SO, NO_SOLINE) PL
	  ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
	  LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = MI.CD_COMPANY AND IG.CD_ITEMGRP = MI.GRP_ITEM
	  WHERE SH.CD_COMPANY = @P_CD_COMPANY
	  AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO
	  AND SL.CD_ITEM NOT LIKE 'SD%'
	  AND (ISNULL(@P_CD_PARTNER, '') = '' OR SH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
	  AND (ISNULL(@P_NO_IMO, '') = '' OR SH.NO_IMO = @P_NO_IMO)
	  AND (ISNULL(@P_NO_EMP, '') = '' OR SH.NO_EMP = @P_NO_EMP)
	  AND (ISNULL(@P_CD_ITEMGRP, '') = '' OR MI.GRP_ITEM = @P_CD_ITEMGRP)
	  AND (ISNULL(@P_CD_SALEORG, '') = '' OR SG.CD_SALEORG = @P_CD_SALEORG)
	  AND (ISNULL(@P_CD_SALEGRP, '') = '' OR SH.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
	  GROUP BY IG.NM_ITEMGRP) SL

GO