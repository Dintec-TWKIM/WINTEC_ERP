USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_OPOUT_PR_SCH_SUB_S]    Script Date: 2022-10-19 오후 5:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_OPOUT_PR_SCH_SUB_S]  
(  
    @P_CD_COMPANY   NVARCHAR(7),   
	@P_CD_PLANT     NVARCHAR(7),   
	@P_DT_PR_FROM	NVARCHAR(8),
	@P_DT_PR_TO		NVARCHAR(8)
)  
AS  

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 
	'N' AS S
,	A.NO_PR
,	A.DT_PR
,	A.NO_EMP
,	B.NM_KOR
FROM CZ_PR_OPOUT_PR A
LEFT JOIN MA_EMP B ON B.CD_COMPANY = A.CD_COMPANY AND B.CD_PLANT = A.CD_PLANT AND B.NO_EMP = A.NO_EMP
WHERE A.CD_COMPANY = @P_CD_COMPANY
AND A.CD_PLANT = @P_CD_PLANT
GROUP BY A.NO_PR, A.DT_PR, A.NO_EMP, B.NM_KOR
HAVING A.DT_PR BETWEEN @P_DT_PR_FROM AND @P_DT_PR_TO

