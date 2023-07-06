USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALU_HUMEMPM_U]    Script Date: 2015-11-17 오후 8:04:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System		: 인사관리
**  Sub System	: 인사평가
**  Page		: 평가자/피평가자등록
**  Desc		: 평가자 UPDATE  
**    
**  Return Values  
**  
**  작    성    자  : 강연철  
**  작    성    일	: 2015.05.26  
**  수    정    자  :   
**  수  정  내  용  :   
*********************************************  
** Change History  
*********************************************  
*********************************************/  
  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALU_HUMEMPM_U]  
(  
	@P_CD_COMPANY		NVARCHAR(7),  
	@P_CD_EVALU			NVARCHAR(8),  
	@P_CD_EVTYPE		NVARCHAR(3),  
	@P_CD_EVNUMBER		NVARCHAR(3),
	@P_CD_GROUP			NVARCHAR(3),
	@P_NO_EMPM			NVARCHAR(10),
	@P_NUM_EWEIGHT		NUMERIC(17, 5),
	@P_ID_UPDATE		NVARCHAR(15)
)  
AS  

UPDATE HR_PEVALU_HUMEMPM   
SET NUM_EWEIGHT	= @P_NUM_EWEIGHT,
	ID_UPDATE = @P_ID_UPDATE,
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = @P_CD_COMPANY
AND	CD_EVALU = @P_CD_EVALU
AND	CD_EVTYPE = @P_CD_EVTYPE
AND	CD_EVNUMBER	= @P_CD_EVNUMBER
AND	CD_GROUP = @P_CD_GROUP
AND	NO_EMPM = @P_NO_EMPM

GO

