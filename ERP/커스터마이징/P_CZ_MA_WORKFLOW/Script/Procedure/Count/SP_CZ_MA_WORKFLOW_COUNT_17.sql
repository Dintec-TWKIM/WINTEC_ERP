USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_COUNT_17]    Script Date: 2018-08-21 오후 4:41:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- 인수증등록
ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_COUNT_17]  
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
FROM SA_GIRH GH
LEFT JOIN CZ_SA_GIRH_WORK_DETAIL GD ON GD.CD_COMPANY = GH.CD_COMPANY AND GD.NO_GIR = GH.NO_GIR
LEFT JOIN (SELECT CD_COMPANY, CD_FILE
		   FROM MA_FILEINFO 
		   WHERE CD_MODULE = 'SA'
		   AND ID_MENU = 'P_CZ_SA_GIM_REG'
		   GROUP BY CD_COMPANY, CD_FILE) MF
ON MF.CD_COMPANY = GH.CD_COMPANY AND MF.CD_FILE = GH.NO_GIR + '_' + GH.CD_COMPANY
WHERE GH.STA_GIR = 'C' 
AND (GH.YN_RETURN IS NULL OR GH.YN_RETURN = 'N')
AND ((GD.DT_LOADING IS NULL OR GD.DT_LOADING = '') OR MF.CD_FILE IS NULL)
AND EXISTS (SELECT 1 
			FROM MM_QTIO
			WHERE CD_COMPANY = GH.CD_COMPANY
			AND NO_ISURCV = GH.NO_GIR
			AND DT_IO > '20151231')
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
											   AND SG.CD_SALEORG = @P_CD_SALEORG))

GO

