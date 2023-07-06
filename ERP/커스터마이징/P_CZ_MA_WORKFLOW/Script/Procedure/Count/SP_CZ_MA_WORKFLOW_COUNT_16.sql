USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_COUNT_16]    Script Date: 2018-08-21 오후 4:36:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- 납품완결
ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_COUNT_16]  
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_ID_USER				NVARCHAR(15),
	@P_NO_KEY				NVARCHAR(20),
	@P_CD_SALEORG			NVARCHAR(7),		
	@P_CD_SALEGRP			NVARCHAR(500)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT COUNT(1) AS CNT
FROM CZ_SA_PACKH PH
JOIN (SELECT GH.CD_COMPANY, GH.NO_GIR
      FROM SA_GIRH GH
	  LEFT JOIN CZ_SA_GIRH_WORK_DETAIL GD ON GD.CD_COMPANY = GH.CD_COMPANY AND GD.NO_GIR = GH.NO_GIR
	  WHERE GH.STA_GIR = 'C'
	  AND (ISNULL(@P_CD_COMPANY, '') = '' OR GH.CD_COMPANY = @P_CD_COMPANY)
      AND (ISNULL(@P_NO_KEY, '') = '' OR GH.NO_GIR = @P_NO_KEY)
	  AND (ISNULL(@P_ID_USER, '') = '' OR GH.NO_EMP = @P_ID_USER)
	  AND (ISNULL(@P_CD_SALEGRP, '') = '' OR EXISTS (SELECT 1 
	  											     FROM SA_GIRL GL
	  											     WHERE GL.CD_COMPANY = GH.CD_COMPANY
	  											     AND GL.NO_GIR = GH.NO_GIR
	  											     AND GL.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP))))
	  AND (ISNULL(@P_CD_SALEORG, '') = '' OR EXISTS (SELECT 1 
	  											     FROM SA_GIRL GL
	  											     JOIN MA_SALEGRP SG ON SG.CD_COMPANY = GL.CD_COMPANY AND SG.CD_SALEGRP = GL.CD_SALEGRP
	  											     WHERE GL.CD_COMPANY = GH.CD_COMPANY
	  											     AND GL.NO_GIR = GH.NO_GIR
	  											     AND SG.CD_SALEORG = @P_CD_SALEORG))) GH
ON GH.CD_COMPANY = PH.CD_COMPANY AND GH.NO_GIR = PH.NO_GIR
WHERE PH.CD_LOCATION IS NOT NULL

GO

