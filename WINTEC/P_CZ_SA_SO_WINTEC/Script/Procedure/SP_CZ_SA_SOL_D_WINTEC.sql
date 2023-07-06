SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*******************************************************************************                      
 --제  목 : 수주라인 삭제   
 --작성일 : 2008.12.08            
 --작성자 : 정남진                
 --화면명 : UP_SA_SOL_DELETE      
              
 --EXEC UP_SA_SOL_DELETE              
*******************************************************************************/            
/*******************************************************************************      
 --* 사용화면 : 나중에 쿼리를 분리하거나 수정내역을 반영해줘야 하기때문에 사용한 화면을 저장해둔다.     
  
 --* 수주등록 화면명    : FG_TRACK  : 관리자      : 본 쿼리 사용여부  
       
 --* 1.수주등록     : M   : NJin   관리 : 사용  
 --* 2.수주등록용역    : YV  : NJin   관리 : 사용  
 --* 3.수주등록(거래처)   : P   : NJin   관리 : 사용  
 --* 4.일괄수주등록    : ME  : NJin   관리 : 실재 사용안함  
 --* 5.일괄수주등록(부가세포함) : MEV  : NJin   관리 : 실재 사용안함  
 --* 6.일괄수주등록(부가세포함)02 : MEV02  : NJin   관리 : 실재 사용안함  
 --* 7.일괄수주등록(부가세포함)EC : MEVEC  : NJin   관리 : 실재 사용안함  
 --* 8.수주이력등록    : H   : NJin   관리 : 미개발  
 --* 9.수주웹등록     : W   : 장기주 관리 : 실재 사용안함  
 *******************************************************************************/        
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOL_D_WINTEC]      
(      
    @P_CD_COMPANY NVARCHAR(7), --회사      
    @P_NO_SO  NVARCHAR(20),  --수주번호              
    @P_SEQ_SO  NUMERIC(3)  --수주항번      
)      
AS              
  
DECLARE @V_STA_SO NVARCHAR(3), @P_FG_TRACK NVARCHAR(5)  
SELECT @V_STA_SO = STA_SO FROM SA_SOL WHERE CD_COMPANY = @P_CD_COMPANY AND NO_SO = @P_NO_SO AND SEQ_SO = @P_SEQ_SO      
      
IF (@V_STA_SO <> 'O')      
BEGIN      
 --이미 수주확정, 종결 되어 수정, 삭제가 불가능합니다.      
 RAISERROR('SA_M000116', 16, 1) -- 2012/08/13:자동화 변경[RAISERROR], 변경전:RAISERROR 100000 'SA_M000116' 
     
 RETURN      
END      
  
  
  
--수주의 FG_TRACK 이 (INDIRECT => I : 사전프로젝트에서 만들어준 수주 DATA는 업데이트 하거나 삭제 할수 없음)  
SELECT @P_FG_TRACK = FG_TRACK FROM SA_SOH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_SO = @P_NO_SO   
        
IF (@P_FG_TRACK = 'I')        
BEGIN        
 --BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.        
 RAISERROR('BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.', 16, 1) -- 2012/08/13:자동화 변경[RAISERROR], 변경전:RAISERROR 100000 'BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.' 
       
 RETURN        
END  

IF EXISTS (SELECT 1 
           FROM CZ_PR_SA_SOL_PR_WO_MAPPING
           WHERE CD_COMPANY = @P_CD_COMPANY
           AND NO_SO = @P_NO_SO
           AND NO_LINE = @P_SEQ_SO)
BEGIN
    RAISERROR('작업지시에 할당된 수주데이터는 할당해제 후 삭제 해야 합니다.', 16, 1)
    RETURN
END

IF EXISTS (SELECT 1 
           FROM CZ_PR_SA_SOL_PU_PRL_MAPPING
           WHERE CD_COMPANY = @P_CD_COMPANY
           AND NO_SO = @P_NO_SO
           AND NO_LINE = @P_SEQ_SO)
BEGIN
    RAISERROR('구매요청에 할당된 수주데이터는 할당해제 후 삭제 해야 합니다.', 16, 1)
    RETURN
END

IF EXISTS (SELECT 1 
           FROM CZ_PR_SA_SOL_SU_PRL_MAPPING
           WHERE CD_COMPANY = @P_CD_COMPANY
           AND NO_SO = @P_NO_SO
           AND NO_LINE = @P_SEQ_SO)
BEGIN
    RAISERROR('외주요청에 할당된 수주데이터는 할당해제 후 삭제 해야 합니다.', 16, 1)
    RETURN
END
  
EXEC UP_SA_SO_HOSTORY_CHK @P_CD_COMPANY, @P_NO_SO
IF @@ERROR <> 0 RETURN
  
DELETE SA_SOL_DLV      
 WHERE CD_COMPANY = @P_CD_COMPANY       
   AND NO_SO = @P_NO_SO      
   AND SEQ_SO = @P_SEQ_SO       
   AND FG_TRACK = 'SO'  
      
DELETE SA_SOL      
 WHERE CD_COMPANY = @P_CD_COMPANY       
   AND NO_SO = @P_NO_SO      
   AND SEQ_SO = @P_SEQ_SO  
GO
