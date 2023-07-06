USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALU_HUMEMPAN_D]    Script Date: 2015-11-17 오후 8:03:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System		: 인사관리
**  Sub System	: 인사평가
**  Page		: 평가자/피평가자등록
**  Desc		: 피평가자 삭제  
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
  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALU_HUMEMPAN_D]  
(  
	@P_CD_EVALU			NVARCHAR(8),  
	@P_CD_EVTYPE		NVARCHAR(3),  
	@P_CD_EVNUMBER		NVARCHAR(3),
	@P_CD_GROUP			NVARCHAR(3),
	@P_NO_EMPM			NVARCHAR(10),
	@P_NO_EMPAN			NVARCHAR(10)
)  
AS  

DELETE 
FROM HR_PEVALU_HUMEMPAN   
WHERE CD_EVALU = @P_CD_EVALU
AND	CD_EVTYPE = @P_CD_EVTYPE
AND	CD_EVNUMBER	= @P_CD_EVNUMBER
AND	CD_GROUP = @P_CD_GROUP
AND	NO_EMPM	= @P_NO_EMPM
AND	NO_EMPAN = @P_NO_EMPAN 

GO

