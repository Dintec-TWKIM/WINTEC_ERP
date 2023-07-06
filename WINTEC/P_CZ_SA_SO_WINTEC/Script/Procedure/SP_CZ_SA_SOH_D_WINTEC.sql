SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*******************************************************************************                    
 제  목 : 수주삭제       
 작성일 : 2008.12.08          
 작성자 : 정남진              
 화면명 : UP_SA_SO_DELETE    
            
 EXEC UP_SA_SO_DELETE            
*******************************************************************************/          
/*******************************************************************************    
 * 사용화면 : 나중에 쿼리를 분리하거나 수정내역을 반영해줘야 하기때문에 사용한 화면을 저장해둔다.   

 * 수주등록 화면명				: FG_TRACK  : 관리자      : 본 쿼리 사용여부
     
 * 1.수주등록					: M			: NJin   관리 : 사용
 * 2.수주등록용역				: YV		: NJin   관리 : 사용
 * 3.수주등록(거래처)			: P			: NJin   관리 : 사용
 * 4.일괄수주등록				: ME		: NJin   관리 : 실재 사용안함
 * 5.일괄수주등록(부가세포함)	: MEV		: NJin   관리 : 실재 사용안함
 * 6.일괄수주등록(부가세포함)02 : MEV02		: NJin   관리 : 실재 사용안함
 * 7.일괄수주등록(부가세포함)EC : MEVEC		: NJin   관리 : 실재 사용안함
 * 8.수주이력등록				: H			: NJin   관리 : 미개발
 * 9.수주웹등록					: W			: 장기주 관리 : 실재 사용안함
 *******************************************************************************/  
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOH_D_WINTEC]        
(        
	@P_CD_COMPANY	NVARCHAR(7),		--회사        
	@P_NO_SO		NVARCHAR(20)		--수주번호        
)        
AS        
      


DECLARE @V_STA_SO INT, @P_FG_TRACK NVARCHAR(5),
		@V_SERVER_KEY	VARCHAR(50)
		
 SELECT @V_STA_SO = COUNT(*) FROM SA_SOL WHERE CD_COMPANY = @P_CD_COMPANY AND NO_SO = @P_NO_SO AND STA_SO <> 'O'        
        
SELECT  @V_SERVER_KEY = MAX(SERVER_KEY)
FROM    CM_SERVER_CONFIG    
WHERE   YN_UPGRADE = 'Y'  

IF (@V_STA_SO > 0)        
BEGIN        
	--이미 수주확정, 종결 되어 수정, 삭제가 불가능합니다.        
	RAISERROR ('SA_M000116', 18, 1)
	RETURN        
END        



--수주의 FG_TRACK 이 (INDIRECT => I : 사전프로젝트에서 만들어준 수주 DATA는 업데이트 하거나 삭제 할수 없음)
SELECT @P_FG_TRACK = FG_TRACK FROM SA_SOH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_SO = @P_NO_SO 
      
IF (@P_FG_TRACK = 'I')      
BEGIN      
	--BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.      
	RAISERROR ('BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.', 18, 1)
	RETURN      
END

IF EXISTS (SELECT 1 
           FROM CZ_PR_SA_SOL_PR_WO_MAPPING
           WHERE CD_COMPANY = @P_CD_COMPANY
           AND NO_SO = @P_NO_SO)
BEGIN
    RAISERROR('작업지시에 할당된 수주데이터는 할당해제 후 삭제 해야 합니다.', 16, 1)
    RETURN
END

IF EXISTS (SELECT 1 
           FROM CZ_PR_SA_SOL_PU_PRL_MAPPING
           WHERE CD_COMPANY = @P_CD_COMPANY
           AND NO_SO = @P_NO_SO)
BEGIN
    RAISERROR('구매요청에 할당된 수주데이터는 할당해제 후 삭제 해야 합니다.', 16, 1)
    RETURN
END

IF EXISTS (SELECT 1 
           FROM CZ_PR_SA_SOL_SU_PRL_MAPPING
           WHERE CD_COMPANY = @P_CD_COMPANY
           AND NO_SO = @P_NO_SO)
BEGIN
    RAISERROR('외주요청에 할당된 수주데이터는 할당해제 후 삭제 해야 합니다.', 16, 1)
    RETURN
END

IF @V_SERVER_KEY IN ('GUJUTEC', 'DZSQLv4')
BEGIN

	DECLARE @V_CNT INT
	
	SELECT @V_CNT = COUNT(CD_CHANCE)
	FROM SA_SOH  
	WHERE	CD_COMPANY = @P_CD_COMPANY
		AND CD_CHANCE IN ( SELECT CD_CHANCE
							FROM SA_SOH  
							WHERE	NO_SO  = @P_NO_SO 
							AND CD_COMPANY = @P_CD_COMPANY)
									
	IF (@V_CNT = 1)
	BEGIN
	UPDATE SA_CHANCE
	SET STEP_CHANCE = '001'
	WHERE CD_COMPANY = @P_CD_COMPANY 
		AND CD_CHANCE = (SELECT CD_CHANCE 
							FROM SA_SOH  
							WHERE	NO_SO  = @P_NO_SO 
									AND CD_COMPANY = @P_CD_COMPANY)
	END									
END

--WEB수주 통해서 들어온 수주 삭제시 WEB수주테이블 승인상태, 날짜 업데이트
IF (@P_FG_TRACK = 'IUW')
BEGIN

	IF(@V_SERVER_KEY = 'HCFD')
	BEGIN 
		UPDATE  WEB_SOH
		SET     STA_CONF    = 'N',
				DT_CONF     = '',
				NO_EMP_CONF ='' 
		WHERE   CD_COMPANY  = @P_CD_COMPANY
		AND     NO_SO_WEB   = @P_NO_SO
	END 
	ELSE 
	BEGIN 
		UPDATE  WEB_SOH
		SET     STA_CONF    = 'N',
				DT_CONF     = ''
		WHERE   CD_COMPANY  = @P_CD_COMPANY
		AND     NO_SO_WEB   = @P_NO_SO
	END 
END
        
DELETE SA_SOL_DLV        
 WHERE CD_COMPANY = @P_CD_COMPANY        
   AND NO_SO = @P_NO_SO    
   AND FG_TRACK = 'SO'

DELETE SA_SOL        
 WHERE CD_COMPANY = @P_CD_COMPANY        
   AND NO_SO = @P_NO_SO        
 
DELETE SA_SOH        
 WHERE CD_COMPANY = @P_CD_COMPANY        
   AND NO_SO = @P_NO_SO      
   
DELETE SA_SOLH
 WHERE CD_COMPANY = @P_CD_COMPANY        
   AND NO_SO = @P_NO_SO        
 
DELETE SA_SOHH       
 WHERE CD_COMPANY = @P_CD_COMPANY        
   AND NO_SO = @P_NO_SO
   
--화우테크놀러지 루미시트 소요자재 삭제
DELETE SA_SOLL
 WHERE CD_COMPANY = @P_CD_COMPANY
   AND NO_SO = @P_NO_SO   
      
IF EXISTS (SELECT 'X' FROM SA_SOH_DLV WHERE CD_COMPANY = @P_CD_COMPANY AND NO_SO = @P_NO_SO)  
DELETE SA_SOH_DLV        
 WHERE CD_COMPANY = @P_CD_COMPANY        
   AND NO_SO  = @P_NO_SO
GO
