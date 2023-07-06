USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALU_HUMEMP_S1]    Script Date: 2015-10-27 오후 7:08:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*******************************************
**  System		: 인사관리
**  Sub System	: 인사평가
**  Page		: 평가자/피평가자등록
**  Desc		: 평가자 조회  
**    
**  Return Values  
**  
**  작    성    자  : 강연철  
**  작    성    일	: 2015.05.22
**  수    정    자  :   
**  수  정  내  용  :   
*********************************************  
** Change History  
*********************************************  
*********************************************/  
  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALU_HUMEMP_S1]  
(
	@P_CD_EVALU		NVARCHAR(8),  
	@P_CD_EVTYPE	NVARCHAR(3),
	@P_CD_EVNUMBER	NVARCHAR(3),
	@P_CD_GROUP		NVARCHAR(3)  
)  
AS  
  
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   HP.CD_COMPANY,
	   HP.CD_EVALU,
	   HP.CD_EVTYPE,
	   HP.CD_EVNUMBER,
	   HP.NO_EMPM,
	   HP.CD_GROUP,
	   EP.NM_KOR,
	   HP.CD_BIZAREA,
	   HP.CD_DEPT,
	   DT.NM_DEPT,
	   HP.CD_EMP,
	   HP.TP_EMP,
	   HP.CD_DUTY_RANK,
	   CD.NM_SYSDEF AS NM_DUTY_RANK,
	   HP.CD_DUTY_STEP,
	   HP.CD_DUTY_RESP,
	   HP.CD_DUTY_TYPE,
	   HP.CD_PAY_STEP,
	   HP.CD_DUTY_WORK,
	   HP.CD_JOB_SERIES,
	   HP.CD_PJT,
	   HP.CD_CC,
	   HP.NUM_EWEIGHT
FROM HR_PEVALU_HUMEMPM HP
LEFT JOIN MA_EMP EP ON EP.CD_COMPANY = HP.CD_BIZAREA AND EP.NO_EMP = HP.NO_EMPM
LEFT JOIN MA_DEPT DT ON DT.CD_COMPANY = EP.CD_COMPANY AND DT.CD_BIZAREA	= EP.CD_BIZAREA AND	DT.CD_DEPT = EP.CD_DEPT
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = HP.CD_COMPANY AND CD.CD_FIELD = 'HR_H000002' AND CD.CD_SYSDEF = HP.CD_DUTY_RANK
WHERE HP.CD_EVALU = @P_CD_EVALU
AND	HP.CD_EVTYPE = @P_CD_EVTYPE
AND	HP.CD_GROUP = @P_CD_GROUP
AND	HP.CD_EVNUMBER = @P_CD_EVNUMBER

GO