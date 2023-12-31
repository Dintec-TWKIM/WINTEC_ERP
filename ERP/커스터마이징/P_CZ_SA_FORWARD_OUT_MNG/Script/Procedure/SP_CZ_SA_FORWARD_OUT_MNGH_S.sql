USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_FORWARD_OUT_MNGH_S]    Script Date: 2016-06-27 오후 5:01:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_FORWARD_OUT_MNGH_S]          
(  
	@P_CD_COMPANY		NVARCHAR(7),
	@P_DT_FROM			NVARCHAR(8),
	@P_DT_TO			NVARCHAR(8),
	@P_CD_PARTNER		NVARCHAR(20)
)  
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
	SELECT FO.CD_COMPANY, FO.CD_PARTNER, FO.DT_FORWARD,
		   COUNT(1) AS QT_BOX
	FROM CZ_SA_FORWARD_OUT FO 
	WHERE (ISNULL(@P_CD_COMPANY, '') = '' OR FO.CD_COMPANY = @P_CD_COMPANY)
	AND FO.DT_FORWARD BETWEEN @P_DT_FROM AND @P_DT_TO
	AND (ISNULL(@P_CD_PARTNER, '') = '' OR FO.CD_PARTNER = @P_CD_PARTNER)
	GROUP BY FO.CD_COMPANY, FO.CD_PARTNER, FO.DT_FORWARD
)
SELECT A.CD_COMPANY,
	   MC.NM_COMPANY,
	   A.CD_PARTNER,
	   ISNULL(MC1.NM_SYSDEF, MD.LN_PARTNER) AS LN_PARTNER,
	   A.DT_FORWARD,
	   A.QT_BOX
FROM A
LEFT JOIN CZ_MA_DELIVERY MD ON MD.CD_COMPANY = A.CD_COMPANY AND MD.CD_PARTNER = A.CD_PARTNER
LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = A.CD_COMPANY
LEFT JOIN CZ_MA_CODEDTL MC1 ON MC1.CD_COMPANY = A.CD_COMPANY AND MC1.CD_FIELD = 'CZ_SA00067' AND MC1.CD_SYSDEF = A.CD_PARTNER


GO