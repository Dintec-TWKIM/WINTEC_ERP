USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_H_CZ_SA_PARTNER_SUB]    Script Date: 2015-07-08 오전 10:52:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_H_CZ_SA_PARTNER_SUB]
(
	@P_CD_COMPANY       NVARCHAR(7),
	@P_TP_QUERY         NVARCHAR(3),
	@P_NO_FILE			NVARCHAR(20),
	@P_SEARCH			NVARCHAR(MAX)
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

IF (@P_TP_QUERY = '000')
BEGIN
	SELECT DISTINCT CD_PARTNER, LN_PARTNER AS NM_PARTNER FROM MA_PARTNER
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND (ISNULL(@P_SEARCH, '') = '' OR CD_PARTNER LIKE '%' + @P_SEARCH + '%' OR LN_PARTNER LIKE '%' + @P_SEARCH + '%') 
END
ELSE IF (@P_TP_QUERY = '001')
BEGIN
	SELECT DISTINCT PQ.CD_PARTNER, MP.LN_PARTNER AS NM_PARTNER FROM CZ_PU_QTNH PQ
	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PQ.CD_COMPANY AND MP.CD_PARTNER = PQ.CD_PARTNER
	WHERE PQ.CD_COMPANY = @P_CD_COMPANY
	AND PQ.NO_FILE = @P_NO_FILE
	AND (ISNULL(@P_SEARCH, '') = '' OR PQ.CD_PARTNER LIKE '%' + @P_SEARCH + '%' OR MP.LN_PARTNER LIKE '%' + @P_SEARCH + '%') 
END
ELSE IF (@P_TP_QUERY = '002')
BEGIN
	SELECT DISTINCT PH.CD_PARTNER, MP.LN_PARTNER AS NM_PARTNER FROM PU_POH PH
	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
	WHERE PH.CD_COMPANY = @P_CD_COMPANY
	AND PH.CD_PJT = @P_NO_FILE
	AND (ISNULL(@P_SEARCH, '') = '' OR PH.CD_PARTNER LIKE '%' + @P_SEARCH + '%' OR MP.LN_PARTNER LIKE '%' + @P_SEARCH + '%') 
END

GO