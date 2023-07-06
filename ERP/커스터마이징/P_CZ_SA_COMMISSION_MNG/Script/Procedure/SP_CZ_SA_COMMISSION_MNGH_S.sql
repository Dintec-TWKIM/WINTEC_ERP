USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_COMMISSION_MNGH_S]    Script Date: 2018-04-16 오후 1:35:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [NEOE].[SP_CZ_SA_COMMISSION_MNGH_S]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_DT_START			NVARCHAR(8),
	@P_DT_END			NVARCHAR(8),
	@P_CD_PARTNER		NVARCHAR(20),
	@P_NO_COMMISSION	NVARCHAR(20),
	@P_ID_USER			NVARCHAR(20)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   CH.YN_ADDED,
	   CH.CD_COMPANY,
	   CH.NO_COMMISSION,
	   GW.ST_STAT,
	   MC1.NM_SYSDEF AS NM_STAT,
	   LEFT(CH.DTS_INSERT, 8) AS DT_COMMISSION,
	   CH.ID_INSERT,
	   MU.NM_USER,
	   CH.CD_PARTNER,
	   MP.LN_PARTNER,
	   CD.NM_EXCH,
	   CD.AM_COMMISSION AS AM_COMMISSION1,
	   CH.DC_COMMISSION,
	   CH.YN_CHARGE,
	   CH.TP_DATE,
	   CH.DT_START,
	   CH.DT_END,
	   CH.RT_COMMISSION,
	   CH.AM_COMMISSION,
	   GW.ID_WRITE,
	   CH.NO_GW_DOCU
FROM CZ_SA_COMMISSIONH CH
LEFT JOIN (SELECT CD.CD_COMPANY, CD.NO_COMMISSION,
				  MAX(MC.NM_SYSDEF) AS NM_EXCH,
				  SUM(CD.AM_COMMISSION) AS AM_COMMISSION
		   FROM CZ_SA_COMMISSIOND CD
		   LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = CD.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = CD.CD_EXCH
		   GROUP BY CD.CD_COMPANY, CD.NO_COMMISSION) CD
ON CD.CD_COMPANY = CH.CD_COMPANY AND CD.NO_COMMISSION = CH.NO_COMMISSION
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = CH.CD_COMPANY AND MP.CD_PARTNER = CH.CD_PARTNER
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = CH.CD_COMPANY AND MU.ID_USER = CH.ID_INSERT
LEFT JOIN FI_GWDOCU GW ON GW.CD_COMPANY = 'K100' AND GW.CD_PC = '010000' AND GW.NO_DOCU = CH.CD_COMPANY + '-' + CH.NO_GW_DOCU
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = GW.CD_COMPANY AND MC1.CD_FIELD = 'PU_C000065' AND MC1.CD_SYSDEF = GW.ST_STAT
WHERE CH.CD_COMPANY = @P_CD_COMPANY
AND CH.DTS_INSERT BETWEEN @P_DT_START + '000000' AND @P_DT_END + '999999'
AND (ISNULL(@P_NO_COMMISSION, '') = '' OR CH.NO_COMMISSION = @P_NO_COMMISSION)
AND (ISNULL(@P_CD_PARTNER, '') = '' OR CH.CD_PARTNER = @P_CD_PARTNER)
AND (ISNULL(@P_ID_USER, '') = '' OR CH.ID_INSERT = @P_ID_USER)

GO

