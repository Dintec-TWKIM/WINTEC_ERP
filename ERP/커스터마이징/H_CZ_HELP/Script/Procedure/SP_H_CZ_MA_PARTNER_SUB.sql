USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_H_CZ_MA_PARTNER_SUB]    Script Date: 2017-07-12 오후 6:25:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_H_CZ_MA_PARTNER_SUB]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_FG_PARTNER		NVARCHAR(4),
	@P_CLS_PARTNER		NVARCHAR(4),
	@P_CD_AREA			NVARCHAR(4),
	@P_CD_NATION		NVARCHAR(4),
	@P_YN_USE			NVARCHAR(4),
	@P_SEARCH		    NVARCHAR(MAX)
) 
AS
 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT MP.CD_PARTNER, 
	   MP.LN_PARTNER,
	   MP.CLS_PARTNER,
	   MC.NM_SYSDEF AS NM_CLS_PARTNER,
	   MP.FG_PARTNER,
	   MC1.NM_SYSDEF AS NM_FG_PARTNER
FROM MA_PARTNER MP
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = MP.CD_COMPANY AND MC.CD_FIELD = 'MA_B000003' AND MC.CD_SYSDEF =  MP.CLS_PARTNER
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = MP.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000001' AND MC1.CD_SYSDEF =  MP.FG_PARTNER
WHERE MP.CD_COMPANY = @P_CD_COMPANY
AND (ISNULL(@P_SEARCH, '') = '' OR (MP.CD_PARTNER LIKE @P_SEARCH + '%') OR (MP.LN_PARTNER LIKE '%' + @P_SEARCH + '%'))
AND (ISNULL(@P_FG_PARTNER, '') = '' OR MP.FG_PARTNER = @P_FG_PARTNER)
AND (ISNULL(@P_CLS_PARTNER, '') = '' OR MP.CLS_PARTNER = @P_CLS_PARTNER)
AND (ISNULL(@P_CD_AREA, '') = '' OR MP.CD_AREA = @P_CD_AREA)
AND (ISNULL(@P_CD_NATION, '') = '' OR MP.CD_NATION = @P_CD_NATION)
AND (ISNULL(@P_YN_USE, '') = '' OR MP.USE_YN = @P_YN_USE)
ORDER BY MP.CD_PARTNER


GO

