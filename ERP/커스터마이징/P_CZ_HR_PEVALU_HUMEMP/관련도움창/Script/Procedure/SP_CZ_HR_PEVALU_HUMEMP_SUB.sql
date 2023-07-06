USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALU_HUMEMP_SUB]    Script Date: 2015-10-27 오후 7:26:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************  
**  System			: 인사관리  
**  Sub System		: 인사평가  
**  Page			: 평가자/피평가자 조회    
**  Desc			: 복사조회    
**      
**  Return Values    
**    
**  작    성    자  : 강연철    
**  작    성    일	: 2015.06.16    
**  수    정    자  :     
**  수  정  내  용  :     
*********************************************    
** Change History    
*********************************************    
*********************************************/    
    
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALU_HUMEMP_SUB]
AS    

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED      

SELECT HM.CD_COMPANY,
	   HM.CD_EVALU,
	   HM.CD_EVTYPE,
	   HM.CD_EVNUMBER,
	   HM.CD_GROUP,
	   SE.NM_EVALU,
	   CE.NM_HCODE AS NM_EVTYPE, 
	   GE.NM_HCODE AS NM_GROUP,
	   CH.NM_HCODE AS NM_EVNUMBER
FROM HR_PEVALU_HUMEMPM HM
LEFT JOIN HR_PEVALU_SCHE SE ON HM.CD_COMPANY = SE.CD_COMPANY AND HM.CD_EVALU = SE.CD_EVALU    
LEFT JOIN HR_PEVALU_CODE CE ON HM.CD_COMPANY = CE.CD_COMPANY AND HM.CD_EVALU = CE.CD_EVALU AND HM.CD_EVTYPE	= CE.CD_HCODE AND CE.CD_FIELD = '100'    
LEFT JOIN HR_PEVALU_CODE GE ON HM.CD_COMPANY = GE.CD_COMPANY AND HM.CD_EVALU = GE.CD_EVALU AND HM.CD_GROUP = GE.CD_HCODE AND GE.CD_FIELD = '200'  
LEFT JOIN HR_PEVALU_CODE CH ON HM.CD_COMPANY = CH.CD_COMPANY AND HM.CD_EVALU = CH.CD_EVALU AND HM.CD_EVNUMBER = CH.CD_HCODE AND CH.CD_FIELD = '300'  
GROUP BY HM.CD_COMPANY, 
		 HM.CD_EVALU,
		 HM.CD_EVTYPE,
		 HM.CD_EVNUMBER,
		 HM.CD_GROUP,
		 SE.NM_EVALU,
		 CE.NM_HCODE,
		 GE.NM_HCODE,
		 CH.NM_HCODE

GO