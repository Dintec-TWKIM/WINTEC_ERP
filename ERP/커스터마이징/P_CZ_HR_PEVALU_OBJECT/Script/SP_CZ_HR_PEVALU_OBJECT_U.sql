USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALU_OBJECT_U]    Script Date: 2015-11-17 오전 9:53:25 ******/
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
  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALU_OBJECT_U]  
(  
	@P_CD_COMPANY NVARCHAR(7),  
	@P_CD_EVALU  NVARCHAR(8),  
	@P_CD_EVTYPE NVARCHAR(3),  
	@P_CD_EVOTYPE NVARCHAR(3),  
	@P_NO_EMP  NVARCHAR(10),  
	@P_CD_OTASK  NVARCHAR(3),  
	@P_NM_OTASK  NVARCHAR(500),  
	@P_DC_DOBJECT NVARCHAR(300),  
	@P_CD_SCALE  NVARCHAR(3),  
	@P_RT_WEIGHT NUMERIC(17, 5),  
	@P_YN_APP  NVARCHAR(1),  
	@P_DT_APP  NVARCHAR(8),  
	@P_ID_UPDATE NVARCHAR(15)  
)  
AS  
DECLARE @P_DTS_UPDATE VARCHAR(15)  
SET @P_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')  
   
BEGIN  
	UPDATE	HR_PEVALU_OBJECT  
	SET		CD_OTASK	= @P_CD_OTASK,  
			NM_OTASK	= @P_NM_OTASK,
			DC_DOBJECT	= @P_DC_DOBJECT,  
			CD_SCALE	= @P_CD_SCALE,  
			RT_WEIGHT	= @P_RT_WEIGHT * 0.01,  
			YN_APP		= @P_YN_APP,  
			DT_APP		= @P_DT_APP,  
			ID_UPDATE	= @P_ID_UPDATE,  
			DTS_UPDATE	= @P_DTS_UPDATE  
	WHERE	CD_COMPANY	= @P_CD_COMPANY  
	AND		CD_EVALU	= @P_CD_EVALU  
	AND		CD_EVTYPE	= @P_CD_EVTYPE  
	AND		NO_EMP		= @P_NO_EMP  
	AND		CD_EVOTYPE	= @P_CD_EVOTYPE  
END  
  

GO

