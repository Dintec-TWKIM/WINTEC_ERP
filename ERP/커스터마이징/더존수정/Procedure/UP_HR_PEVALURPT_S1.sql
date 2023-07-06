USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_HR_PEVALURPT_S1]    Script Date: 2015-11-11 오후 5:28:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System		: 인사관리
**  Sub System	: 인사평가
**  Page		: 평가등록현황
**  Desc		: HEADER 조회
**  
**  Return Values
**
**  작    성    자 	: 강연철
**  작    성    일	: 2015.05.28
**  수    정    자  : 
**  수  정  내  용  : 
*********************************************
** Change History
*********************************************
*********************************************/

ALTER PROCEDURE [NEOE].[UP_HR_PEVALURPT_S1]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_EVALU		NVARCHAR(8),
	@P_CD_EVTYPE	NVARCHAR(3),
	@P_CD_GROUP		NVARCHAR(3),
	@P_NO_EMPM		NVARCHAR(10),
	@P_NO_EMPAN		NVARCHAR(4000)
)
AS
BEGIN	
	SELECT	HN.CD_COMPANY,
			HN.CD_EVALU,
			HN.CD_EVTYPE,
			HN.CD_GROUP,
			HN.NO_EMPM,
			HN.NO_EMPAN,
			EP.NM_KOR
	FROM	HR_PEVALU_HUMEMPAN HN
	LEFT OUTER JOIN MA_EMP EP
	ON		EP.CD_COMPANY	= HN.CD_BIZAREA
	AND		EP.NO_EMP		= HN.NO_EMPAN
	WHERE	HN.CD_COMPANY	= @P_CD_COMPANY
	AND		HN.CD_EVALU		= @P_CD_EVALU
	AND		HN.CD_EVTYPE	= @P_CD_EVTYPE
	AND		HN.CD_GROUP		= @P_CD_GROUP
	AND		HN.NO_EMPM		= @P_NO_EMPM
	AND		(@P_NO_EMPAN IS NULL OR @P_NO_EMPAN  = '' OR HN.NO_EMPAN IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_NO_EMPAN)))
	GROUP BY HN.CD_COMPANY, HN.CD_EVALU, HN.CD_EVTYPE, HN.CD_EVTYPE, HN.CD_GROUP, HN.NO_EMPM, HN.NO_EMPAN, EP.NM_KOR
END
GO

