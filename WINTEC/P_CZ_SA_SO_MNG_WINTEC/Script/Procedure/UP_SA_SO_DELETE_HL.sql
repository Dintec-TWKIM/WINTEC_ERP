USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SO_DELETE_HL]    Script Date: 2019-11-14 오후 3:13:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************    
**  System : 영업    
**  Sub System : 수주관리    
**  Page  : 수주등록    
**  Desc  : 수주등록 Head, Line 일괄 삭제    
**    
**  Return Values    
**    
**  작    성    자  :  오성영    
**  작    성    일 :     
**  수    정    자     :     
*********************************************    
** Change History   
1.트리거에서 오류 발생시 바로 나갈 수 있도록 처리함 IF @@ERROR <> 0 RETURN
  삭제시 트리거 오류 발생하여도 프로시저를 계속 수행함.
*********************************************    
*********************************************/    
ALTER PROCEDURE [NEOE].[UP_SA_SO_DELETE_HL]    
(    
	@P_CD_COMPANY	NVARCHAR(7),   --회사    
	@P_MULTI_SO		NVARCHAR(4000)  
)    
AS    
SET NOCOUNT ON    

--WEB수주 통해서 들어온 수주 삭제시 WEB수주테이블 승인상태, 날짜 업데이트
IF EXISTS ( SELECT  1
            FROM    SA_SOH
            WHERE   CD_COMPANY  = @P_CD_COMPANY
            AND     NO_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SO))
            AND     FG_TRACK    = 'IUW' )
BEGIN
    UPDATE  WEB_SOH
    SET     STA_CONF    = 'N',
            DT_CONF     = ''
    WHERE   CD_COMPANY  = @P_CD_COMPANY
    AND     NO_SO_WEB IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SO))
END

DELETE 
FROM	SA_SOL  
WHERE	CD_COMPANY = @P_CD_COMPANY  
AND		NO_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SO))  

IF @@ERROR <> 0	RETURN

DELETE 
FROM	SA_SOL_DLV 
WHERE	CD_COMPANY = @P_CD_COMPANY  
AND		NO_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SO))  
AND		FG_TRACK = 'SO'

IF @@ERROR <> 0 RETURN
 
--수주이력(라인) 삭제
DELETE	SA_SOLH       
WHERE	CD_COMPANY = @P_CD_COMPANY        
AND		NO_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SO))  

--화우테크놀러지 루미스트 소요자재 삭제
DELETE
FROM	SA_SOLL
WHERE	CD_COMPANY = @P_CD_COMPANY          
AND		NO_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SO))

IF @@ERROR <> 0 RETURN

DELETE
FROM	SA_SOH  
WHERE	CD_COMPANY = @P_CD_COMPANY  
AND		NO_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SO))  

--수주이력(헤더) 삭제
DELETE	SA_SOHH
WHERE	CD_COMPANY = @P_CD_COMPANY        
AND		NO_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SO))  

IF @@ERROR <> 0 RETURN
  
IF EXISTS (SELECT 'X' FROM SA_SOH_DLV WHERE CD_COMPANY = @P_CD_COMPANY AND NO_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SO)))    
DELETE
FROM	SA_SOH_DLV          
WHERE	CD_COMPANY = @P_CD_COMPANY          
AND		NO_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SO))   

IF @@ERROR <> 0 RETURN
GO

