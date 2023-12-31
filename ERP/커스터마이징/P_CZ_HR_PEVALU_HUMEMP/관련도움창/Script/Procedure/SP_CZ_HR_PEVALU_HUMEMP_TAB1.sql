USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALU_HUMEMP_TAB1]    Script Date: 2015-10-28 오전 9:08:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALU_HUMEMP_TAB1]  
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_DT_ENTER_TO		NVARCHAR(8),
	@P_CD_BIZAREA		VARCHAR(8000),
	@P_CD_DEPT			VARCHAR(MAX),
	@P_TP_EMP			VARCHAR(8000),
	@P_CD_DUTY_RANK		VARCHAR(8000),
	@P_CD_DUTY_STEP		VARCHAR(8000),
	@P_CD_DUTY_RESP		VARCHAR(8000),
	@P_DC_SEARCH		VARCHAR(8000)
)  
AS  

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   ME.CD_COMPANY,
	   MC.NM_COMPANY,
	   ME.CD_BIZAREA,
	   MB.NM_BIZAREA,
	   ME.CD_DEPT,
	   MD.NM_DEPT,
	   ME.NO_EMP,
	   ME.NM_KOR,
	   ME.CD_DUTY_RANK,
	   CD.NM_SYSDEF AS NM_DUTY_RANK,
	   ME.DT_ENTER,
	   ME.DT_RETIRE,
	   ME.DT_BAN,
	   ME.DT_LASTBAN,
	   ME.CD_PAY_STEP,
	   ME.DT_GENTER,
	   ME.CD_EMP,
	   ME.TP_EMP,
	   ME.CD_DUTY_STEP,
	   ME.CD_DUTY_RESP,
	   ME.CD_DUTY_TYPE,
	   ME.CD_DUTY_WORK,
	   ME.CD_JOB_SERIES,
	   ME.CD_CC,
	   ME.CD_PJT
FROM MA_EMP ME	
JOIN MA_COMPANY MC ON MC.CD_COMPANY = ME.CD_COMPANY
JOIN MA_BIZAREA MB ON ME.CD_COMPANY = MB.CD_COMPANY AND ME.CD_BIZAREA = MB.CD_BIZAREA
JOIN MA_DEPT MD ON ME.CD_COMPANY = MD.CD_COMPANY AND ME.CD_DEPT = MD.CD_DEPT
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = ME.CD_COMPANY AND CD.CD_FIELD = 'HR_H000002' AND CD.CD_SYSDEF = ME.CD_DUTY_RANK
WHERE ME.CD_COMPANY <> 'TEST' 
AND ISNULL(ME.CD_EMP, '') <> ''
AND ISNULL(ME.CD_INCOM, '001') <> '099' -- 퇴직자 제외
AND (ISNULL(@P_CD_COMPANY, '') = '' OR ME.CD_COMPANY = @P_CD_COMPANY) --회사
AND (ISNULL(@P_DT_ENTER_TO, '') = '' OR ME.DT_ENTER <= @P_DT_ENTER_TO) -- 입사일자T	
AND (ISNULL(@P_CD_BIZAREA, '') = '' OR ME.CD_BIZAREA IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_BIZAREA)))	-- 사업장  
AND (ISNULL(@P_CD_DEPT, '') = '' OR ME.CD_DEPT IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_DEPT))) -- 부서
AND (ISNULL(@P_TP_EMP, '') = '' OR ME.TP_EMP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_TP_EMP))) -- 사원구분
AND (ISNULL(@P_CD_DUTY_RANK, '') = '' OR ME.CD_DUTY_RANK IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_DUTY_RANK))) -- 직위  
AND (ISNULL(@P_CD_DUTY_STEP, '') = '' OR ME.CD_DUTY_STEP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_DUTY_STEP))) -- 직급
AND (ISNULL(@P_CD_DUTY_RESP, '') = '' OR ME.CD_DUTY_RESP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_DUTY_RESP))) -- 직책
AND (ISNULL(@P_DC_SEARCH, '') = '' OR ME.NO_EMP LIKE '%' + @P_DC_SEARCH + '%' OR ME.NM_KOR LIKE '%' + @P_DC_SEARCH + '%') 
ORDER BY MB.NM_BIZAREA, MD.NM_DEPT, ME.NM_KOR ASC

GO