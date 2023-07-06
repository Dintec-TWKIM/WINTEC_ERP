USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_COUNT_18]    Script Date: 2018-08-21 오후 4:44:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- 계산서발행
ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_COUNT_18]  
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_NO_KEY				NVARCHAR(20),
	@P_CD_SALEORG			NVARCHAR(7),		
	@P_CD_SALEGRP			NVARCHAR(500)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT COUNT(1) AS CNT
FROM MM_QTIOH OH
JOIN (SELECT OL.CD_COMPANY, OL.NO_IO
	  FROM MM_QTIO OL
	  WHERE OL.FG_PS = '2' 
	  AND OL.YN_AM = 'Y' 
	  AND OL.QT_IO > OL.QT_CLS
	  AND (ISNULL(@P_NO_KEY, '') = '' OR OL.NO_IO = @P_NO_KEY)
	  AND (ISNULL(@P_CD_SALEGRP, '') = '' OR OL.CD_GROUP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
	  AND (ISNULL(@P_CD_SALEORG, '') = '' OR EXISTS (SELECT 1 
													 FROM MA_SALEGRP
													 WHERE CD_COMPANY = OL.CD_COMPANY
													 AND CD_SALEGRP = OL.CD_GROUP
													 AND CD_SALEORG = @P_CD_SALEORG))
	  GROUP BY OL.CD_COMPANY, OL.NO_IO) OL
ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
WHERE (ISNULL(@P_CD_COMPANY, '') = '' OR OH.CD_COMPANY = @P_CD_COMPANY)

GO

