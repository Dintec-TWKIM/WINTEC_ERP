USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALU_OBJECT_S]    Script Date: 2015-11-17 오전 9:55:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System		: 인사관리
**  Sub System	: 인사평가
**  Page		: 목표등록
**  Desc		: 조회
**    
**  Return Values  
**  
**  작    성    자  : 강연철  
**  작    성    일	: 2015.06.01  
**  수    정    자  :   
**  수  정  내  용  :   
*********************************************  
** Change History  
*********************************************  
*********************************************/   
  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALU_OBJECT_S]  
(  
	@P_CD_COMPANY NVARCHAR(7),  
	@P_CD_EVALU  NVARCHAR(8),  
	@P_CD_EVTYPE NVARCHAR(3),  
	@P_NO_EMP  NVARCHAR(12)  
)  
AS  

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT	OT.CD_COMPANY,  
		OT.CD_EVALU,  
		OT.CD_EVTYPE,  
		OT.CD_EVOTYPE,  
		OT.NO_EMP,  
		OT.CD_OTASK,  
		OT.NM_OTASK,  
		OT.DC_DOBJECT,  
		OT.CD_SCALE,  
		(OT.RT_WEIGHT * 100) AS RT_WEIGHT,  
		OT.YN_APP,  
		OT.DT_APP  
FROM	HR_PEVALU_OBJECT OT  
WHERE	OT.CD_COMPANY	= @P_CD_COMPANY  
AND		OT.CD_EVALU		= @P_CD_EVALU  
AND		OT.CD_EVTYPE	= @P_CD_EVTYPE  
AND		OT.NO_EMP		= @P_NO_EMP
ORDER BY CONVERT(NUMERIC, OT.CD_EVOTYPE)  

GO