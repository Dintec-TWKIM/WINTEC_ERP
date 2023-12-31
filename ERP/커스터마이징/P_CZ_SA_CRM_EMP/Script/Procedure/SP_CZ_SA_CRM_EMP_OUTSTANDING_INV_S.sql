USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CUST_REL_MNG_TIME_LINE_S]    Script Date: 2017-05-25 오후 1:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_EMP_OUTSTANDING_INV_S]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_EMP			NVARCHAR(10),
	@P_DT_STRAT			NVARCHAR(8),
	@P_DT_END			NVARCHAR(8),
	@P_YN_REMAIN		NVARCHAR(1)
) 
AS
 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_DT_TODAY CHAR(8) = CONVERT(CHAR(8), GETDATE(), 112)

SELECT IH.NO_IV,
	   SH.NO_SO,
	   SH.NO_PO_PARTNER,
	   IH.DT_IV,
	   IH.DT_RCP,
	   IH.DT_PROGRESS,
	   MP.LN_PARTNER AS NM_PARTNER,
	   SH.NO_IMO,
	   MH.NM_VESSEL,
	   ME.NO_EMP,
	   ME.NM_KOR AS NM_EMP,
	   MD.NM_DEPT,
	   SG.NM_SALEGRP,
	   MC.NM_SYSDEF AS NM_EXCH,
	   IH.RT_EXCH,
 	   ISNULL(IH.AM_EX_CLS, 0) AS AM_EX_CLS,
	   ISNULL(IH.AM_CLS, 0) AS AM_CLS,
	   ISNULL(IH.AM_EX_CHARGE, 0) AS AM_EX_CHARGE,
	   ISNULL(IH.AM_CHARGE, 0) AS AM_CHARGE,
	   (ISNULL(IH.AM_EX_CLS, 0) - ISNULL(IH.AM_EX_CHARGE, 0)) AS AM_EX_NET,
	   (ISNULL(IH.AM_CLS, 0) - ISNULL(IH.AM_CHARGE, 0)) AS AM_NET,
	   ISNULL(IH.AM_RCP_EX, 0) AS AM_RCP_EX,
	   ISNULL(IH.AM_RCP, 0) AS AM_RCP,
	   (ISNULL(IH.AM_EX_CLS, 0) - ISNULL(IH.AM_RCP_EX, 0)) AS AM_EX_REMAIN,
	   (ISNULL(IH.AM_CLS, 0) - ISNULL(IH.AM_RCP, 0)) AS AM_REMAIN,
	   SH.DC_RMK1,
	   AR.DC_RMK
FROM (SELECT IH.CD_COMPANY,
			 IH.NO_IV,
			 MAX(IH.CD_EXCH) AS CD_EXCH,
			 MAX(IH.RT_EXCH) AS RT_EXCH,
	         MAX(IL.CD_PJT) AS CD_PJT,
	  	     MAX(IL.NO_SO) AS NO_SO,
	  	     MIN(IH.DT_PROCESS) AS DT_IV,
	  	     MAX(RH.DT_RCP) AS DT_RCP,
	  	     DATEDIFF(MONTH, MIN(IH.DT_PROCESS), @V_DT_TODAY) AS DT_PROGRESS,
	  	     SUM(ISNULL(IL.AM_EX_CLS, 0) + ISNULL(IL.VAT_EX, 0)) AS AM_EX_CLS,
	  	     SUM(ISNULL(IL.AM_CLS, 0) + ISNULL(IL.VAT, 0)) AS AM_CLS,
			 SUM(IL.AM_EX_CHARGE) AS AM_EX_CHARGE,
			 SUM(IL.AM_CHARGE) AS AM_CHARGE,
	  	     SUM(CASE WHEN ISNULL(IH.AM_IV, 0) = ISNULL(IH.AM_BAN, 0) THEN (ISNULL(IL.AM_EX_CLS, 0) + ISNULL(IL.VAT_EX, 0)) ELSE RH.AM_RCP_EX END) AS AM_RCP_EX,
	  	     SUM(CASE WHEN ISNULL(IH.AM_IV, 0) = ISNULL(IH.AM_BAN, 0) THEN (ISNULL(IL.AM_CLS, 0) + ISNULL(IL.VAT, 0)) ELSE RH.AM_RCP END) AS AM_RCP
	  FROM (SELECT IH.CD_COMPANY, IH.NO_IV,
	  		       IH.DT_PROCESS,
				   IH.CD_EXCH,
				   IH.RT_EXCH,
	  			   (ISNULL(IH.AM_K, 0) + ISNULL(IH.VAT_TAX, 0)) AS AM_IV,
	  		       ISNULL(IH.AM_BAN, 0) AS AM_BAN 
	  	    FROM SA_IVH IH
			WHERE IH.CD_COMPANY = @P_CD_COMPANY
			AND IH.DT_PROCESS BETWEEN @P_DT_STRAT AND @P_DT_END) IH
	  JOIN (SELECT IL.CD_COMPANY, IL.NO_IV, IL.CD_PJT,
	  			   MAX(IL.NO_SO) AS NO_SO,
	  		       SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.AM_EX_CLS ELSE IL.AM_EX_CLS END) AS AM_EX_CLS,
	  	           SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.AM_CLS ELSE IL.AM_CLS END) AS AM_CLS,
				   SUM(CASE WHEN IL.CD_ITEM LIKE 'SD%' THEN (ISNULL(IL.AM_EX_CLS, 0) + ROUND(IL.VAT / IL.RT_EXCH, 2)) ELSE 0 END) AS AM_EX_CHARGE,
				   SUM(CASE WHEN IL.CD_ITEM LIKE 'SD%' THEN (ISNULL(IL.AM_CLS, 0) + ISNULL(IL.VAT, 0)) ELSE 0 END) AS AM_CHARGE,
	  	           SUM(ROUND((CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.VAT ELSE IL.VAT END) / IL.RT_EXCH, 2)) AS VAT_EX,
	  	           SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.VAT ELSE IL.VAT END) AS VAT
	  	    FROM SA_IVL IL
	  	    GROUP BY IL.CD_COMPANY, IL.NO_IV, IL.CD_PJT) IL
	  ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IV = IH.NO_IV
	  LEFT JOIN (SELECT RH.CD_COMPANY, RD.NO_TX, RD.CD_PJT,
	  	     	        MAX(RH.DT_RCP) AS DT_RCP,
	  	     	        SUM(RD.AM_RCP_TX_EX) AS AM_RCP_EX, 
	  	     	        SUM(RD.AM_RCP) AS AM_RCP
	  	         FROM SA_RCPH RH
	  	         JOIN (SELECT RD.CD_COMPANY, RD.NO_RCP, RD.NO_TX, FD.CD_PJT,
	  	         		      RD.AM_RCP_TX_EX,
	  	         			  (ISNULL(RD.AM_RCP_TX, 0) + ISNULL(RD.AM_PL, 0)) AS AM_RCP 
	  	         	   FROM SA_RCPD RD
	  	         	   LEFT JOIN FI_DOCU FD ON FD.CD_COMPANY = RD.CD_COMPANY AND FD.NO_DOCU = RD.NO_DOCU AND FD.NO_DOLINE = RD.NO_DOLINE
	  	         	   UNION ALL
	  	         	   SELECT BD.CD_COMPANY, BD.NO_RCP, BD.NO_IV, FD.CD_PJT,
	  	         	    BD.AM_RCPS_EX,
	  	         	    (ISNULL(BD.AM_RCPS, 0) + ISNULL(BD.AM_PL, 0)) AS AM_RCP
	  	         	   FROM SA_BILLSD BD
	  	         	   LEFT JOIN FI_DOCU FD ON FD.CD_COMPANY = BD.CD_COMPANY AND FD.NO_DOCU = BD.NO_DOCU_IV AND FD.NO_DOLINE = BD.NO_DOLINE_IV) RD
	  	         ON RD.CD_COMPANY = RH.CD_COMPANY AND RD.NO_RCP = RH.NO_RCP
				 WHERE RD.NO_TX IS NOT NULL
				 AND RH.DT_RCP <= @V_DT_TODAY
	  	         GROUP BY RH.CD_COMPANY, RD.NO_TX, RD.CD_PJT) RH
	  ON RH.CD_COMPANY = IL.CD_COMPANY AND RH.NO_TX = IL.NO_IV AND RH.CD_PJT = IL.CD_PJT
	  GROUP BY IH.CD_COMPANY, IH.NO_IV, (CASE WHEN ISNULL(IL.CD_PJT, '') = '' THEN IL.NO_SO ELSE IL.CD_PJT END)) IH
LEFT JOIN SA_SOH SH1 ON SH1.CD_COMPANY = IH.CD_COMPANY AND SH1.NO_SO = IH.CD_PJT
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = IH.CD_COMPANY AND SH.NO_SO = ISNULL(SH1.NO_SO, IH.NO_SO)
LEFT JOIN CZ_SA_AC_RECEIVABLE AR ON AR.CD_COMPANY = IH.CD_COMPANY AND AR.NO_KEY = IH.NO_IV
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = (CASE WHEN ISNULL(AR.NO_EMP, '') = '' THEN SH.NO_EMP ELSE AR.NO_EMP END)
LEFT JOIN (SELECT CD_COMPANY, NO_EMP,
				  MAX(CD_SALEGRP) AS CD_SALEGRP 
		   FROM MA_USER
		   GROUP BY CD_COMPANY, NO_EMP) MU
ON MU.CD_COMPANY = ME.CD_COMPANY AND MU.NO_EMP = ME.NO_EMP
LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = ME.CD_COMPANY AND MD.CD_DEPT = ME.CD_DEPT
LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = MU.CD_COMPANY AND SG.CD_SALEGRP = MU.CD_SALEGRP
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = IH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = IH.CD_EXCH
WHERE (@P_YN_REMAIN = 'Y' OR (ISNULL(IH.AM_CLS, 0) - ISNULL(IH.AM_RCP, 0)) <> 0)
AND ME.NO_EMP = @P_NO_EMP
ORDER BY IH.DT_IV

GO