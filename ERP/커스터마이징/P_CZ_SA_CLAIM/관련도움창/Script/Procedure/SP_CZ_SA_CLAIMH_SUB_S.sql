USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CLAIMH_SUB_S]    Script Date: 2015-04-14 오전 8:31:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_SA_CLAIMH_SUB_S]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_NO_SO		NVARCHAR(20),
	@P_DT_SO_FROM	NVARCHAR(8),
	@P_DT_SO_TO		NVARCHAR(8)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT SH.NO_SO,
	   SH.NO_CONTRACT,
	   SH.CD_PARTNER,
	   MP.LN_PARTNER,
	   SH.NO_IMO,
	   MH.NO_HULL,
	   MH.NM_VESSEL,
	   SH.NO_EMP AS NO_SALES_EMP,
	   ME.NM_KOR AS NM_SALES_EMP,
	   SH.DT_SO 
FROM SA_SOH SH
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
WHERE SH.CD_COMPANY = @P_CD_COMPANY 
AND SH.DT_SO BETWEEN @P_DT_SO_FROM AND @P_DT_SO_TO
AND (ISNULL(@P_NO_SO, '') = '' OR SH.NO_SO LIKE '%' + @P_NO_SO + '%') 
   
GO

