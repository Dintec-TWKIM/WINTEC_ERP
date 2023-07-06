USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_COUNT_15]    Script Date: 2018-08-21 오후 4:30:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- 협조전완결
ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_COUNT_15]  
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
FROM (SELECT GH.CD_COMPANY, GH.NO_GIR
	  FROM SA_GIRH GH
	  WHERE GH.YN_RETURN = 'N' 
	  AND (GH.STA_GIR IS NULL OR GH.STA_GIR IN ('', 'O', 'R', 'D'))
	  AND (ISNULL(@P_CD_COMPANY, '') = '' OR GH.CD_COMPANY = @P_CD_COMPANY)
	  AND (ISNULL(@P_NO_KEY, '') = '' OR GH.NO_GIR = @P_NO_KEY)
	  AND (ISNULL(@P_ID_USER, '') = '' OR GH.NO_EMP = @P_ID_USER)
	  AND EXISTS (SELECT 1 
				  FROM SA_GIRL GL
				  JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_FILE = GL.NO_SO AND QL.NO_LINE = GL.SEQ_SO
				  WHERE GL.CD_COMPANY = GH.CD_COMPANY
				  AND GL.NO_GIR = GH.NO_GIR
				  AND (ISNULL(@P_CD_SALEGRP, '') = '' OR GL.CD_SALEGRP = @P_CD_SALEGRP)
				  AND (ISNULL(@P_CD_SALEORG, '') = '' OR EXISTS (SELECT 1 
																 FROM MA_SALEGRP SG
																 WHERE SG.CD_COMPANY = GL.CD_COMPANY
																 AND SG.CD_SALEGRP = GL.CD_SALEGRP
																 AND SG.CD_SALEORG = @P_CD_SALEORG)))
	  UNION ALL
	  SELECT GH.CD_COMPANY, GH.NO_GIR
	  FROM SA_GIRH GH
	  WHERE GH.YN_RETURN = 'Y'
	  AND (ISNULL(@P_CD_COMPANY, '') = '' OR GH.CD_COMPANY = @P_CD_COMPANY)
	  AND (ISNULL(@P_NO_KEY, '') = '' OR GH.NO_GIR = @P_NO_KEY)
	  AND (ISNULL(@P_ID_USER, '') = '' OR GH.NO_EMP = @P_ID_USER)
	  AND EXISTS (SELECT 1 
	  			  FROM SA_GIRL GL
				  JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_FILE = GL.NO_SO_MGMT AND QL.NO_LINE = GL.NO_SOLINE_MGMT
	  			  WHERE GL.CD_COMPANY = GH.CD_COMPANY
	  			  AND GL.NO_GIR = GH.NO_GIR
	  			  AND GL.QT_GIR > GL.QT_GI
				  AND (ISNULL(@P_CD_SALEGRP, '') = '' OR GL.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
				  AND (ISNULL(@P_CD_SALEORG, '') = '' OR EXISTS (SELECT 1 
																 FROM MA_SALEGRP SG
																 WHERE SG.CD_COMPANY = GL.CD_COMPANY
																 AND SG.CD_SALEGRP = GL.CD_SALEGRP
																 AND SG.CD_SALEORG = @P_CD_SALEORG)))) GH

GO

