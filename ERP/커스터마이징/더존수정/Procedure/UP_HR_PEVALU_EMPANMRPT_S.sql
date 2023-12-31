USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_HR_PEVALU_EMPANMRPT_S]    Script Date: 2015-11-11 오후 6:55:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*******************************************
**  System		: 인사관리
**  Sub System	: 인사평가
**  Page		: 피평가자별평가자현황
**  Desc		: 조회  
**    
**  Return Values  
**  
**  작    성    자  : 강연철  
**  작    성    일	: 2015.06.18  
**  수    정    자  :   
**  수  정  내  용  :   
*********************************************  
** Change History  
*********************************************  
*********************************************/  
  
ALTER PROCEDURE [NEOE].[UP_HR_PEVALU_EMPANMRPT_S]  
(  
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_EVALU		NVARCHAR(8),
	@P_CD_EVTYPE	NVARCHAR(3),
	@P_CD_GROUP		NVARCHAR(3),
	@P_CD_EVNUMBER	NVARCHAR(3)
)  
AS  
BEGIN 
	
	SELECT	HN.CD_COMPANY,
			HN.CD_BIZAREA AS CD_BIZAREAN,
			BA1.NM_BIZAREA AS NM_BIZAREAN,
			HN.CD_DEPT AS CD_DEPTN,
			DT1.NM_DEPT AS NM_DEPTN,
			HN.NO_EMPAN,
			EP1.NM_KOR AS NM_EMPAN,
			HN.CD_DUTY_RANK AS CD_DUTY_RANKAN,
			RK1.NM_SYSDEF AS NM_DUTY_RANKAN,
			HN.CD_DUTY_STEP AS CD_DUTY_STEPAN,
			SP1.NM_SYSDEF AS NM_DUTY_STEPAN,
			HN.CD_DUTY_RESP AS CD_DUTY_RESPAN,
			RP1.NM_SYSDEF AS NM_DUTY_RESPAN,
			HM.CD_BIZAREA AS CD_BIZAREAM,
			BA2.NM_BIZAREA AS NM_BIZAREAM,
			HM.CD_DEPT AS CD_DEPTM,
			DT2.NM_DEPT AS NM_DEPTM,
			HM.NO_EMPM,
			EP2.NM_KOR AS NM_EMPM,
			HM.CD_DUTY_RANK AS CD_DUTY_RANKM,
			RK2.NM_SYSDEF AS NM_DUTY_RANKM,
			HM.CD_DUTY_STEP AS CD_DUTY_STEPM,
			SP2.NM_SYSDEF AS NM_DUTY_STEPM,
			HM.CD_DUTY_RESP AS CD_DUTY_RESPM,
			RP2.NM_SYSDEF AS NM_DUTY_RESPM
	FROM	HR_PEVALU_HUMEMPAN HN
	LEFT OUTER JOIN HR_PEVALU_HUMEMPM HM
	ON		HN.CD_COMPANY	= HM.CD_COMPANY
	AND		HN.CD_EVALU		= HM.CD_EVALU
	AND		HN.CD_EVTYPE	= HM.CD_EVTYPE
	AND		HN.CD_EVNUMBER	= HM.CD_EVNUMBER
	AND		HN.CD_GROUP		= HM.CD_GROUP
	AND		HN.NO_EMPM		= HM.NO_EMPM
	LEFT OUTER JOIN MA_BIZAREA BA1
	ON		HN.CD_BIZAREA	= BA1.CD_COMPANY
	AND		HN.CD_BIZAREA	= BA1.CD_BIZAREA
	LEFT OUTER JOIN MA_DEPT DT1
	ON		HN.CD_BIZAREA	= DT1.CD_COMPANY
	AND		HN.CD_BIZAREA	= DT1.CD_BIZAREA
	AND		HN.CD_DEPT		= DT1.CD_DEPT
	LEFT OUTER JOIN MA_EMP EP1
	ON		HN.CD_BIZAREA	= EP1.CD_COMPANY
	AND		HN.NO_EMPAN		= EP1.NO_EMP
	LEFT OUTER JOIN MA_CODEDTL RK1
	ON		HN.CD_BIZAREA	= RK1.CD_COMPANY
	AND		RK1.CD_FIELD	= 'HR_H000002'
	AND		HN.CD_DUTY_RANK	= RK1.CD_SYSDEF
	LEFT OUTER JOIN MA_CODEDTL SP1
	ON		HN.CD_BIZAREA	= SP1.CD_COMPANY
	AND		SP1.CD_FIELD	= 'HR_H000003'
	AND		HN.CD_DUTY_STEP	= SP1.CD_SYSDEF
	LEFT OUTER JOIN MA_CODEDTL RP1
	ON		HN.CD_BIZAREA	= RP1.CD_COMPANY
	AND		RP1.CD_FIELD	= 'HR_H000052'
	AND		HN.CD_DUTY_RESP	= RP1.CD_SYSDEF
	LEFT OUTER JOIN MA_BIZAREA BA2
	ON		HM.CD_COMPANY	= BA2.CD_COMPANY
	AND		HM.CD_BIZAREA	= BA2.CD_BIZAREA
	LEFT OUTER JOIN MA_DEPT DT2
	ON		HM.CD_COMPANY	= DT2.CD_COMPANY
	AND		HM.CD_BIZAREA	= DT2.CD_BIZAREA
	AND		HM.CD_DEPT		= DT2.CD_DEPT
	LEFT OUTER JOIN MA_EMP EP2
	ON		HM.CD_COMPANY   = EP2.CD_COMPANY
	AND		HM.NO_EMPM		= EP2.NO_EMP
	LEFT OUTER JOIN MA_CODEDTL RK2
	ON		HM.CD_COMPANY	= RK2.CD_COMPANY
	AND		RK2.CD_FIELD	= 'HR_H000002'
	AND		HM.CD_DUTY_RANK	= RK2.CD_SYSDEF
	LEFT OUTER JOIN MA_CODEDTL SP2
	ON		HM.CD_COMPANY	= SP2.CD_COMPANY
	AND		SP2.CD_FIELD	= 'HR_H000003'
	AND		HM.CD_DUTY_STEP	= SP2.CD_SYSDEF
	LEFT OUTER JOIN MA_CODEDTL RP2
	ON		HM.CD_COMPANY	= RP2.CD_COMPANY
	AND		RP2.CD_FIELD	= 'HR_H000052'
	AND		HM.CD_DUTY_RESP	= RP2.CD_SYSDEF
	WHERE	HN.CD_COMPANY	= @P_CD_COMPANY
	AND		HN.CD_EVALU		= @P_CD_EVALU
	AND		HN.CD_EVTYPE	= @P_CD_EVTYPE
	AND		HN.CD_GROUP		= @P_CD_GROUP
	AND		HN.CD_EVNUMBER	= @P_CD_EVNUMBER
	ORDER BY HN.NO_EMPAN
END  


GO

