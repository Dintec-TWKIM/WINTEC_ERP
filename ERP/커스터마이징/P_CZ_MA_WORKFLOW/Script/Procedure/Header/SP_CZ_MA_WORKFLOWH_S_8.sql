USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOWH_S_8]    Script Date: 2018-08-13 오전 9:46:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- 수주등록
ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOWH_S_8]  
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_ID_USER				NVARCHAR(15),
	@P_NO_KEY				NVARCHAR(20),
	@P_CD_SALEORG			NVARCHAR(7),		
	@P_CD_SALEGRP			NVARCHAR(500)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   (CASE WHEN SH.CD_COMPANY IS NULL THEN 'N' ELSE 'Y' END) AS YN_REG,
	   WH.CD_COMPANY,
	   WH.NO_KEY,
	   MP.LN_PARTNER AS NM_PARTNER,
	   MH.NM_VESSEL,
	   WH.ID_SALES,
	   MU.NM_USER AS NM_SALES,
	   MU1.NM_USER AS NM_PUR,
	   WH.DTS_INSERT,
	   WH.DC_RMK
FROM CZ_MA_WORKFLOWH WH
LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = WH.CD_COMPANY AND QH.NO_FILE = WH.NO_KEY
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = WH.CD_COMPANY AND SH.NO_SO = WH.NO_KEY
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = WH.CD_COMPANY AND MU.ID_USER = WH.ID_SALES
LEFT JOIN MA_USER MU1 ON MU1.CD_COMPANY = WH.CD_COMPANY AND MU1.ID_USER = WH.ID_PUR
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
WHERE WH.TP_STEP = '08'
AND (WH.YN_DONE IS NULL OR WH.YN_DONE = 'N')
AND (SH.YN_CLOSE IS NULL OR SH.YN_CLOSE = 'N')
AND (ISNULL(@P_CD_COMPANY, '') = '' OR WH.CD_COMPANY = @P_CD_COMPANY)
AND (ISNULL(@P_ID_USER, '') = '' OR WH.ID_PUR = @P_ID_USER)
AND (ISNULL(@P_NO_KEY, '') = '' OR WH.NO_KEY = @P_NO_KEY)
AND (ISNULL(@P_CD_SALEGRP, '') = '' OR MU.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
AND (ISNULL(@P_CD_SALEORG, '') = '' OR EXISTS (SELECT 1 
											   FROM MA_SALEGRP
											   WHERE CD_COMPANY = MU.CD_COMPANY
											   AND CD_SALEGRP = MU.CD_SALEGRP
											   AND CD_SALEORG = @P_CD_SALEORG))

GO

