USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_RCPBILL_TRANSFER_DOCU]    Script Date: 2016-03-04 오후 2:52:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- 설    명 : 영업>채권관리>수금등록 -> 미결전표처리
-- 수 정 자 : 김명수
-- 수 정 일 : 2010.06.11
-- 유    형 : 수정
-- 내    역 : 에러발생시 ROLLBACK 처리
-- ================================================
--*******************************************      
-- Change History  
--2010.02.23 --전표유형  NULL 이면 DEFAULT 값(34)으로 SET -SMR(제다/김형석)
--*******************************************    
ALTER PROCEDURE [NEOE].[SP_CZ_SA_RCPBILL_TRANSFER_DOCU]    
(            
    @P_CD_COMPANY   NVARCHAR(7),  
    @P_NO_BILLS     NVARCHAR(20),  
    @P_ID_INSERT    NVARCHAR(15)  
)  
AS  

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

-- CC_SA_IV024 (전표처리 조회)              
-- ◎ 전표처리              
-- 계정과목 & 채권계정              
DECLARE @CD_COMPANY     NVARCHAR(7)      
DECLARE @NO_COMPANY     NVARCHAR(20)                
DECLARE @NO_BILLS       NVARCHAR(20)          
DECLARE @V_NO_RCP       NVARCHAR(20)                  
DECLARE @CD_BIZAREA     NVARCHAR(7)              
DECLARE @CD_WDEPT       NVARCHAR(12)              
DECLARE @BILL_PARTNER   NVARCHAR(20)              
DECLARE @FG_TAX         NCHAR(2)       
DECLARE @NM_TAX         NVARCHAR(50)             
DECLARE @ID_WRITE       NVARCHAR(10)              
DECLARE @DT_PROCESS     NCHAR(8)              
DECLARE @AM_DR          NUMERIC(19,4)              
DECLARE @AM_CR          NUMERIC(19,4)              
DECLARE @AM_SUPPLY      NUMERIC(19,4)              
DECLARE @DT_TAX         NCHAR(8)              
DECLARE @FG_TRANS       NVARCHAR(2)              
DECLARE @CD_CC          NVARCHAR(12)              
DECLARE @NO_EMP         NVARCHAR(10)              
DECLARE @CD_PJT         NVARCHAR(20)              
DECLARE @CD_EXCH        NVARCHAR(3)              
DECLARE @NM_EXCH        NVARCHAR(50)             
DECLARE @RT_EXCH        NUMERIC(11,4)              
DECLARE @CD_DEPT        NVARCHAR(12)              
DECLARE @CD_PC          NVARCHAR(7)              
DECLARE @NM_BIZAREA     NVARCHAR(50)              
DECLARE @FG_AIS         NCHAR(3)              
DECLARE @CD_ACCT        NVARCHAR(10)              
DECLARE @CLS_ITEM       NVARCHAR(3)     
DECLARE @TP_ACAREA      NVARCHAR(3)             
DECLARE @TP_DRCR        NVARCHAR(1)              
DECLARE @NM_DEPT        NVARCHAR(50)              
DECLARE @NM_CC          NVARCHAR(50)              
DECLARE @NM_EMP         NVARCHAR(50)              
DECLARE @NM_PJT         NVARCHAR(50)              
DECLARE @NM_PARTNER     NVARCHAR(50)              
DECLARE @T_CD_RELATION  NCHAR(2)  --부가세 연동항목(31)매출         
DECLARE @P_ERRORCODE    NCHAR(10)      
DECLARE @P_ERRORMSG     NVARCHAR(300)        
      
DECLARE @V_CD_DEPOSIT   NVARCHAR(20)  -- 예적금코드          
DECLARE @V_CD_CARD      NVARCHAR(20)  -- 신용카드          
DECLARE @V_CD_BANK      NVARCHAR(7)   -- 금융기관          
DECLARE @V_NM_BANK      NVARCHAR(50)  -- 금융기관          
--DECLARE @V_NO_ITEM   NVARCHAR(20)   -- 품목코드          
DECLARE @V_CD_MNG       NVARCHAR(20)  -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
--DECLARE @V_CD_TRADE   NVARCHAR(2)   -- 거래구분(자산변동,지급어음정리,받을어음정리,유가증권거래구분) * 받을어음 : FI_F000006 지급어음 : FI_F000007          
--DECLARE @V_DT_START   NCHAR(8)      -- 발생일자(증빙일자, 자금예정일자)              
DECLARE @V_DT_END       NCHAR(8)      -- 어음만기일자          
DECLARE @V_AM_EXDO      NUMERIC(19,4) -- 외화금액          
    
DECLARE @ERRNO          INT,            
        @ERRMSG         NVARCHAR(255),      
        @CD_CC_TEMP     NVARCHAR(12)          
    
DECLARE @V_NM_NOTE      NVARCHAR(100) -- 적요    
DECLARE @V_LN_PARTNER   NVARCHAR(50)  -- 거래처명    
DECLARE @V_CD_TP        NVARCHAR(3)   -- 수금형태코드    
DECLARE @V_NM_TP  NVARCHAR(20)  -- 수금형태명          
DECLARE @V_CD_DOCU      VARCHAR(3)    -- 전표유형              
        
DECLARE @CUR_DATE   NVARCHAR(8) --현재일자

DECLARE @V_NO_BDOCU		NVARCHAR(20) --원인전표번호
DECLARE @V_NO_BDOLINE	NUMERIC(5,0) --원인전표라인

DECLARE @V_SERVER_KEY   VARCHAR(50)

DECLARE	@V_ST_DOCU		NVARCHAR(1)
DECLARE	@V_TP_EVIDENCE  NVARCHAR(1)

SELECT  @V_SERVER_KEY = MIN(SERVER_KEY)
FROM    CM_SERVER_CONFIG
WHERE   YN_UPGRADE = 'Y'

SET @V_ST_DOCU = '1'
        
DECLARE SRC_CURSOR CURSOR FOR              

/*차변 : 선수금 OR 선수금(해외)*/
SELECT A.CD_COMPANY,               
       A.NO_BILLS,               
       RCP.CD_BIZAREA AS CD_BIZAREA,               
       D.CD_DEPT AS CD_WDEPT,               
       A.BILL_PARTNER,               
       NULL AS FG_TAX,               
       A.NO_EMP AS ID_WRITE,               
       A.DT_BILLS AS DT_PROCESS,           
       (B.AM_BILLS) AS AM_DR,              
       0.0 AS AM_CR,              
       0.0 AS AM_SUPPLY,          
       A.DT_BILLS AS DT_TAX,               
       A.TP_BUSI AS FG_TRANS,               
       CC.CD_CC,               
       A.NO_EMP,               
       RCP.NO_PROJECT AS CD_PJT,
       B.CD_EXCH,         
       CD.NM_SYSDEF AS NM_EXCH,          
       B.RT_EXCH,              
       D.CD_DEPT AS CD_DEPT,              
       CC.CD_PC  AS CD_PC,
       E.NM_BIZAREA AS NM_BIZAREA,              
       L.FG_AIS,              
       L.CD_ACCT,              
       '' CLS_ITEM,               
       '1' TP_DRCR, -- 차대구분 1: 차변              
       MD.NM_DEPT AS NM_DEPT,              
       CC.NM_CC AS NM_CC,
       ME.NM_KOR AS NM_EMP,
       SP.NM_PROJECT AS NM_PJT,
       MP.LN_PARTNER AS NM_PARTNER,            
       '10' CD_RELATION, --연동항목:일반            
       NULL AS NM_TAX,
       P.NO_COMPANY,
       '0' AS TP_ACAREA,
       NULL AS CD_DEPOSIT,   -- 예적금코드          
       NULL AS CD_CARD,      -- 신용카드          
       NULL AS CD_BANK,      -- 금융기관       
       NULL AS NM_BANK,      -- 금융기관명    
       NULL AS CD_MNG,       -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
       NULL AS DT_END,       -- 어음만기일자          
       B.AM_BILLS_EX AS AM_EX,         -- 외화금액          
       A.CD_DOCU,            -- 전표유형
	   BH.NO_DOCU AS NO_BDOCU,
	   BH.NO_DOLINE AS NO_BDOLINE
FROM SA_BILLSH A           
JOIN SA_BILLSL B ON A.NO_BILLS = B.NO_BILLS AND A.CD_COMPANY = B.CD_COMPANY  
JOIN MA_SALEGRP G ON A.CD_COMPANY = G.CD_COMPANY AND A.CD_BILLTGRP = G.CD_SALEGRP      
JOIN MA_CC CC ON G.CD_COMPANY = CC.CD_COMPANY AND G.CD_CC = CC.CD_CC      
JOIN MA_EMP D ON A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY                          
JOIN MA_AISPOSTL L ON A.CD_COMPANY = L.CD_COMPANY AND A.CD_TP = L.CD_TP       -- 수금형태  
JOIN FI_BANH BH ON BH.CD_COMPANY = B.CD_COMPANY AND BH.NO_DOCU = B.NO_DOCU AND BH.NO_DOLINE = B.NO_DOLINE
LEFT JOIN MA_PARTNER P ON A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY      
LEFT JOIN SA_RCPH RCP ON B.NO_RCP = RCP.NO_RCP AND B.CD_COMPANY = RCP.CD_COMPANY              
LEFT JOIN MA_BIZAREA E ON RCP.CD_BIZAREA = E.CD_BIZAREA AND RCP.CD_COMPANY = E.CD_COMPANY
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = B.CD_COMPANY AND CD.CD_FIELD = 'MA_B000005' AND CD.CD_SYSDEF = B.CD_EXCH
LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = D.CD_COMPANY AND MD.CD_DEPT = D.CD_DEPT
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = A.CD_COMPANY AND ME.NO_EMP = A.NO_EMP
LEFT JOIN SA_PROJECTH SP ON SP.CD_COMPANY = RCP.CD_COMPANY AND SP.NO_PROJECT = RCP.NO_PROJECT
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = A.CD_COMPANY AND MP.CD_PARTNER = A.BILL_PARTNER
WHERE B.NO_BILLS = @P_NO_BILLS   
AND B.CD_COMPANY = @P_CD_COMPANY   
AND B.AM_BILLS <> 0   
AND A.ST_BILL = 'N' -- 전표처리여부             
AND L.FG_TP = '300' -- 형태코드 : 수금형태('300')          
AND L.FG_AIS = (CASE A.TP_BUSI WHEN '001' THEN '331' ELSE '333' END)  -- 선수금계정 : 국내('351'), 해외('333')    
UNION ALL              
/*차변 : 환차손*/    
SELECT A.CD_COMPANY,               
	   A.NO_BILLS,               
	   IVH.CD_BIZAREA_TAX AS CD_BIZAREA,               
	   D.CD_DEPT AS CD_WDEPT,               
	   A.BILL_PARTNER,               
	   NULL AS FG_TAX,               
	   A.NO_EMP AS ID_WRITE,               
	   A.DT_BILLS AS DT_PROCESS,              
	   SUM(B.AM_PL) AS AM_DR,              
	   0.0 AS AM_CR,              
	   0.0 AS AM_SUPPLY,          
	   A.DT_BILLS AS DT_TAX,               
	   A.TP_BUSI AS FG_TRANS,               
	   CC.CD_CC,               
	   A.NO_EMP,               
	   PH.NO_PROJECT AS CD_PJT,               
	   B.CD_EXCH_IV AS CD_EXCH,       
	   MC.NM_SYSDEF AS NM_EXCH,            
	   B.RT_EXCH_IV AS RT_EXCH,              
	   D.CD_DEPT AS CD_DEPT,              
	   CC.CD_PC  AS CD_PC,
	   E.NM_BIZAREA AS NM_BIZAREA,              
	   L.FG_AIS,              
	   L.CD_ACCT,              
	   '' AS CLS_ITEM,               
	   '1' AS TP_DRCR, -- 차대구분 1: 차변              
	   MD.NM_DEPT AS NM_DEPT,              
	   CC.NM_CC AS NM_CC,
	   ME.NM_KOR AS NM_EMP,              
	   PH.NM_PROJECT AS NM_PJT,    
	   MP.LN_PARTNER AS NM_PARTNER,            
	   '10' AS CD_RELATION, --연동항목:일반            
	   NULL AS NM_TAX,
	   P.NO_COMPANY,
	   '0' AS TP_ACAREA,
	   NULL AS CD_DEPOSIT,  -- 예적금코드          
	   NULL AS CD_CARD,      -- 신용카드          
	   NULL AS CD_BANK,      -- 금융기관       
	   NULL AS NM_BANK,      -- 금융기관명       
	   NULL AS CD_MNG,       -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
	   NULL AS DT_END,       -- 어음만기일자          
	   SUM(B.AM_RCPS_EX) AS AM_EX,         -- 외화금액          
	   A.CD_DOCU,            -- 전표유형
	   NULL AS NO_BDOCU,
	   NULL AS NO_BDOLINE  
FROM SA_BILLSH A           
JOIN SA_BILLSD B ON A.NO_BILLS = B.NO_BILLS AND A.CD_COMPANY = B.CD_COMPANY              
JOIN FI_DOCU FD ON FD.CD_COMPANY = B.CD_COMPANY AND FD.NO_DOCU = B.NO_DOCU_IV AND FD.NO_DOLINE = B.NO_DOLINE_IV
JOIN MA_CC CC ON FD.CD_COMPANY = CC.CD_COMPANY AND FD.CD_CC = CC.CD_CC      
JOIN MA_EMP D ON A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY                            
JOIN MA_AISPOSTL L ON A.CD_COMPANY = L.CD_COMPANY AND A.CD_TP = L.CD_TP -- 수금형태    
JOIN FI_ACCTCODE F ON L.CD_ACCT = F.CD_ACCT AND L.CD_COMPANY = F.CD_COMPANY -- 수금형태에 따른 계정과목
LEFT JOIN MA_PARTNER P ON A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY        
LEFT JOIN SA_IVH IVH ON B.NO_IV = IVH.NO_IV AND B.CD_COMPANY = IVH.CD_COMPANY     
LEFT JOIN MA_BIZAREA E ON IVH.CD_BIZAREA_TAX = E.CD_BIZAREA AND IVH.CD_COMPANY = E.CD_COMPANY
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = B.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = B.CD_EXCH_IV
LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = D.CD_COMPANY AND MD.CD_DEPT = D.CD_DEPT
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = A.CD_COMPANY AND ME.NO_EMP = A.NO_EMP
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = A.CD_PARTNER AND MP.CD_PARTNER = A.BILL_PARTNER
LEFT JOIN SA_PROJECTH PH ON PH.CD_COMPANY = FD.CD_COMPANY AND PH.NO_PROJECT = FD.CD_PJT
WHERE B.NO_BILLS = @P_NO_BILLS 
AND B.CD_COMPANY = @P_CD_COMPANY 
AND B.AM_PL > 0 AND A.ST_BILL = 'N' -- 전표처리여부             
AND L.FG_TP = '300' -- 형태코드 : 수금형태('300')          
AND L.FG_AIS = '312' -- 환차손    
GROUP BY A.CD_COMPANY,               
	     A.NO_BILLS,               
	     D.CD_DEPT,               
	     A.BILL_PARTNER,               
	     A.NO_EMP,               
	     A.DT_BILLS,              
	     A.DT_BILLS,               
	     A.TP_BUSI,               
	     CC.CD_CC,               
	     A.NO_EMP,                           
	     B.CD_EXCH_IV,             
	     B.CD_COMPANY,       
	     B.RT_EXCH_IV,              
	     D.CD_DEPT,              
	     CC.CD_PC,    
	     L.FG_AIS,              
	     L.CD_ACCT,              
	     D.CD_DEPT,    
	     D.CD_COMPANY,              
	     CC.NM_CC,    
	     P.NO_COMPANY,    
	     IVH.CD_BIZAREA_TAX,    
	     E.NM_BIZAREA,  
	     A.CD_DOCU,
		 MC.NM_SYSDEF,
		 MD.NM_DEPT,
		 ME.NM_KOR,
		 MP.LN_PARTNER,
		 PH.NO_PROJECT,
		 PH.NM_PROJECT
UNION ALL
--------------------------------------------------------------------------------------------------------------------- 매출계정              
/*대변 (선수금정리일경우-현행    ): 선수금정리 OR 선수금정리(해외) (EX):외상매출금 OR 외화외상매출금) */    
/* OR */    
/*대변 (선수금반환일경우-20080408 ): 현금 */
SELECT DISTINCT A.CD_COMPANY,               
				A.NO_BILLS,               
				IVH.CD_BIZAREA_TAX AS CD_BIZAREA,               
				D.CD_DEPT AS CD_WDEPT,               
				A.BILL_PARTNER,               
				NULL AS FG_TAX,               
				A.NO_EMP AS ID_WRITE,               
				A.DT_BILLS AS DT_PROCESS,              
				0.0 AS AM_DR,              
				SUM(B.AM_RCPS + ISNULL(AM_PL, 0)) AS AM_CR,              
				0.0 AS AM_SUPPLY,          
				A.DT_BILLS AS DT_TAX,               
				A.TP_BUSI AS FG_TRANS,               
				CC.CD_CC,               
				A.NO_EMP,               
				SP.NO_PROJECT AS CD_PJT,
				B.CD_EXCH_IV AS CD_EXCH,         
				MC.NM_SYSDEF AS NM_EXCH,          
				B.RT_EXCH_IV AS RT_EXCH,              
				D.CD_DEPT AS CD_DEPT,              
				CC.CD_PC  AS CD_PC,
				E.NM_BIZAREA AS NM_BIZAREA,              
				L.FG_AIS,              
				L.CD_ACCT,              
				'' CLS_ITEM,               
				'2' TP_DRCR, -- 차대구분 2: 대변              
				MD.NM_DEPT AS NM_DEPT,              
				CC.NM_CC AS NM_CC,
				ME.NM_KOR AS NM_EMP,    
				SP.NM_PROJECT AS NM_PJT,
				MP.LN_PARTNER AS NM_PARTNER,            
				'10' AS CD_RELATION, --연동항목:일반            
				NULL AS NM_TAX,
				P.NO_COMPANY, -- ?    
				'0' AS TP_ACAREA,
				NULL AS CD_DEPOSIT,  -- 예적금코드          
				NULL AS CD_CARD,      -- 신용카드          
				NULL AS CD_BANK,      -- 금융기관       
				NULL AS NM_BANK,      -- 금융기관명    
				NULL AS CD_MNG,       -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
				NULL AS DT_END,       -- 어음만기일자          
				(CASE WHEN B.CD_EXCH_IV = '000' THEN NULL ELSE SUM(B.AM_RCPS_EX) END) AS AM_EX,        -- 외화금액          
				A.CD_DOCU,            -- 전표유형
				BH.NO_DOCU AS NO_BDOCU,
				BH.NO_DOLINE AS NO_BDOLINE   
FROM SA_BILLSH A           
JOIN SA_BILLSD B ON A.NO_BILLS = B.NO_BILLS AND A.CD_COMPANY = B.CD_COMPANY              
JOIN FI_DOCU FD ON FD.CD_COMPANY = B.CD_COMPANY AND FD.NO_DOCU = B.NO_DOCU_IV AND FD.NO_DOLINE = B.NO_DOLINE_IV
JOIN MA_CC CC ON FD.CD_COMPANY = CC.CD_COMPANY AND FD.CD_CC = CC.CD_CC
JOIN MA_EMP D ON A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY
JOIN MA_AISPOSTL L ON B.CD_COMPANY = L.CD_COMPANY AND B.TP_SO = L.CD_TP -- 수금형태
JOIN FI_BANH BH ON BH.CD_COMPANY = B.CD_COMPANY AND BH.NO_DOCU = B.NO_DOCU_IV AND BH.NO_DOLINE = B.NO_DOLINE_IV
LEFT JOIN MA_PARTNER P ON A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY
LEFT JOIN SA_IVH IVH ON B.NO_IV = IVH.NO_IV AND B.CD_COMPANY = IVH.CD_COMPANY    
LEFT JOIN MA_BIZAREA E ON IVH.CD_BIZAREA_TAX = E.CD_BIZAREA AND IVH.CD_COMPANY = E.CD_COMPANY
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = B.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = B.CD_EXCH_IV
LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = D.CD_COMPANY AND MD.CD_DEPT = D.CD_DEPT
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = A.CD_COMPANY AND ME.NO_EMP = A.NO_EMP
LEFT JOIN SA_PROJECTH SP ON SP.CD_COMPANY = FD.CD_COMPANY AND SP.NO_PROJECT = FD.CD_PJT
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = A.CD_COMPANY AND MP.CD_PARTNER = A.BILL_PARTNER
WHERE B.NO_BILLS = @P_NO_BILLS 
AND B.CD_COMPANY = @P_CD_COMPANY 
AND B.AM_RCPS <> 0 
AND A.ST_BILL = 'N' -- 전표처리여부             
AND A.TP_BILLS = 'N' -- (선수금정리일경우만) <===============    
AND L.FG_TP = '100'  -- 형태코드 : 수금형태('300')          
AND L.FG_AIS = '101'  -- 정리계정 : 국내('332'), 구매승인서('334')              
GROUP BY A.CD_COMPANY,               
		 A.NO_BILLS,               
		 D.CD_DEPT,               
		 A.BILL_PARTNER,               
		 A.NO_EMP,               
		 A.DT_BILLS,              
		 A.DT_BILLS,               
		 A.TP_BUSI,               
		 CC.CD_CC,               
		 A.NO_EMP,               
		 SP.NO_PROJECT,
		 B.CD_EXCH_IV,         
		 B.CD_COMPANY,             
		 B.RT_EXCH_IV,              
		 D.CD_DEPT,              
		 CC.CD_PC,    
		 L.FG_AIS,              
		 L.CD_ACCT,              
		 D.CD_DEPT,    
		 D.CD_COMPANY,              
		 CC.NM_CC,    
		 P.NO_COMPANY,    
		 IVH.CD_BIZAREA_TAX,    
		 E.NM_BIZAREA,  
		 A.CD_DOCU, -- 전표유형
		 BH.NO_DOCU,
		 BH.NO_DOLINE,
		 MC.NM_SYSDEF,
		 MD.NM_DEPT,
		 ME.NM_KOR,
		 SP.NM_PROJECT,
		 MP.LN_PARTNER 
UNION ALL
SELECT A.CD_COMPANY,               
	   A.NO_BILLS,               
	   RCP.CD_BIZAREA  AS CD_BIZAREA,               
	   D.CD_DEPT AS CD_WDEPT,               
	   A.BILL_PARTNER,               
	   NULL AS FG_TAX,               
	   A.NO_EMP AS ID_WRITE,               
	   A.DT_BILLS AS DT_PROCESS,              
	   0.0 AS AM_DR,              
	   (B.AM_BILLS) AS AM_CR,              
	   0.0 AS AM_SUPPLY,          
	   A.DT_BILLS AS DT_TAX,               
	   A.TP_BUSI AS FG_TRANS,               
	   CC.CD_CC,               
	   A.NO_EMP,               
	   RCP.NO_PROJECT AS CD_PJT,               
	   B.CD_EXCH,         
	   MC.NM_SYSDEF AS NM_EXCH,          
	   B.RT_EXCH,              
	   D.CD_DEPT AS CD_DEPT,              
	   CC.CD_PC  AS CD_PC,
	   E.NM_BIZAREA AS NM_BIZAREA,              
	   L.FG_AIS,              
	   L.CD_ACCT,              
	   '' AS CLS_ITEM,               
	   '2' AS TP_DRCR, -- 차대구분 2: 대변              
	   MD.NM_DEPT AS NM_DEPT,              
	   CC.NM_CC AS NM_CC,
	   ME.NM_KOR AS NM_EMP,   
	   SP.NO_PROJECT AS NM_PJT,
	   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.BILL_PARTNER) NM_PARTNER,            
	   '10' CD_RELATION, --연동항목:일반            
	   NULL AS NM_TAX,
	   P.NO_COMPANY, -- ?    
	   '0' AS TP_ACAREA,
	   NULL AS CD_DEPOSIT,  -- 예적금코드          
	   NULL AS CD_CARD,      -- 신용카드          
	   NULL AS CD_BANK,      -- 금융기관       
	   NULL AS NM_BANK,      -- 금융기관명    
	   B.NO_MGMT AS CD_MNG,       -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
	   NULL AS DT_END,       -- 어음만기일자          
	   (B.AM_BILLS_EX) AS AM_EX,         -- 외화금액          
	   A.CD_DOCU,            -- 전표유형
	   NULL AS NO_BDOCU,
	   NULL AS NO_BDOLINE  
FROM SA_BILLSH A           
JOIN SA_BILLSL B ON A.NO_BILLS = B.NO_BILLS AND A.CD_COMPANY = B.CD_COMPANY 
JOIN MA_SALEGRP G ON A.CD_COMPANY = G.CD_COMPANY AND A.CD_BILLTGRP = G.CD_SALEGRP
JOIN MA_CC CC ON G.CD_COMPANY = CC.CD_COMPANY AND G.CD_CC = CC.CD_CC
JOIN MA_EMP D ON A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY
JOIN MA_AISPOSTL L ON A.CD_COMPANY = L.CD_COMPANY AND A.CD_TP = L.CD_TP -- 수금형태  
JOIN FI_ACCTCODE F ON L.CD_ACCT = F.CD_ACCT AND L.CD_COMPANY = F.CD_COMPANY -- 수금형태에 따른 계정과목
LEFT JOIN MA_PARTNER P ON A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY
LEFT JOIN SA_RCPH RCP ON B.NO_RCP = RCP.NO_RCP AND B.CD_COMPANY = RCP.CD_COMPANY
LEFT JOIN MA_BIZAREA E ON RCP.CD_BIZAREA = E.CD_BIZAREA AND RCP.CD_COMPANY = E.CD_COMPANY      
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = B.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = B.CD_EXCH
LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = D.CD_COMPANY AND MD.CD_DEPT = D.CD_DEPT
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = A.CD_COMPANY AND ME.NO_EMP = A.NO_EMP
LEFT JOIN SA_PROJECTH SP ON SP.CD_COMPANY = RCP.CD_COMPANY AND SP.NO_PROJECT = RCP.NO_PROJECT 
WHERE B.NO_BILLS = @P_NO_BILLS
AND B.CD_COMPANY = @P_CD_COMPANY 
AND B.AM_BILLS <> 0 
AND A.ST_BILL = 'N' -- 전표처리여부
AND L.FG_TP = '300' -- 형태코드 : 수금형태('300')          
AND A.TP_BILLS = 'R' -- (선수금반환일경우) <------------------------    
AND L.FG_AIS = (CASE A.TP_BUSI WHEN '001' THEN '353' ELSE '354' END)  -- 정리계정 : 국내('332'), 구매승인서('334')
UNION ALL              
/*대변 : 환차익*/
SELECT DISTINCT
	   A.CD_COMPANY,               
	   A.NO_BILLS,               
	   IVH.CD_BIZAREA_TAX AS CD_BIZAREA,               
	   D.CD_DEPT AS CD_WDEPT,               
	   A.BILL_PARTNER,               
	   NULL AS FG_TAX,               
	   A.NO_EMP ID_WRITE,               
	   A.DT_BILLS AS DT_PROCESS,              
	   0.0 AM_DR,              
	   SUM(0 - B.AM_PL ) AM_CR,              
	   0.0 AM_SUPPLY,          
	   A.DT_BILLS AS DT_TAX,               
	   A.TP_BUSI AS FG_TRANS,               
	   CC.CD_CC,               
	   A.NO_EMP,               
	   PH.NO_PROJECT AS CD_PJT,               
	   B.CD_EXCH_IV AS CD_EXCH,        
	   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = B.CD_COMPANY AND CD_SYSDEF = B.CD_EXCH_IV AND CD_FIELD = 'MA_B000005') NM_EXCH,           
	   B.RT_EXCH_IV AS RT_EXCH,              
	   D.CD_DEPT AS CD_DEPT,              
	   CC.CD_PC  AS CD_PC, --(SELECT CD_PC FROM MA_CC WHERE CD_CC = (SELECT CD_CC FROM MA_DEPT WHERE CD_COMPANY = D.CD_COMPANY AND CD_DEPT = D.CD_DEPT) AND CD_COMPANY = A.CD_COMPANY) CD_PC,              
	   E.NM_BIZAREA AS NM_BIZAREA,              
	   L.FG_AIS,              
	   L.CD_ACCT,              
	   '' CLS_ITEM,               
	   '2' TP_DRCR, -- 차대구분 2: 대변              
	   (SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = D.CD_DEPT AND CD_COMPANY = D.CD_COMPANY) NM_DEPT,              
	   CC.NM_CC AS NM_CC, -- (SELECT NM_CC FROM MA_CC WHERE CD_CC = B.CD_CC AND CD_COMPANY = A.CD_COMPANY) NM_CC,              
	   (SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = A.NO_EMP AND CD_COMPANY = A.CD_COMPANY) NM_EMP,              
	   PH.NM_PROJECT AS NM_PJT,    
	   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.BILL_PARTNER) NM_PARTNER,            
	   '10' CD_RELATION, --연동항목:일반            
	   NULL AS NM_TAX, --(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,      
	   P.NO_COMPANY, -- ?    
	   '0' AS TP_ACAREA, -- (CASE F.TP_DRCR WHEN '1' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA    
	   NULL AS CD_DEPOSIT,  -- 예적금코드          
	   NULL AS CD_CARD,      -- 신용카드          
	   NULL AS CD_BANK,      -- 금융기관       
	   NULL AS NM_BANK,      -- 금융기관명       
	   NULL AS CD_MNG,       -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
	   NULL AS DT_END,       -- 어음만기일자          
	   SUM(B.AM_RCPS_EX ) AS AM_EX,         -- 외화금액          
	   A.CD_DOCU,            -- 전표유형
	   NULL AS NO_BDOCU,
	   NULL AS NO_BDOLINE  
FROM SA_BILLSH A           
JOIN SA_BILLSD B ON A.NO_BILLS = B.NO_BILLS AND A.CD_COMPANY = B.CD_COMPANY
JOIN FI_DOCU FD ON FD.CD_COMPANY = B.CD_COMPANY AND FD.NO_DOCU = B.NO_DOCU_IV AND FD.NO_DOLINE = B.NO_DOLINE_IV
JOIN MA_CC CC ON FD.CD_COMPANY = CC.CD_COMPANY AND FD.CD_CC = CC.CD_CC
JOIN MA_EMP D ON A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY
JOIN MA_AISPOSTL L ON A.CD_COMPANY = L.CD_COMPANY AND A.CD_TP = L.CD_TP -- 수금형태
JOIN FI_ACCTCODE F ON L.CD_ACCT    = F.CD_ACCT AND L.CD_COMPANY = F.CD_COMPANY -- 수금형태에 따른 계정과목
LEFT JOIN MA_PARTNER P ON A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY
LEFT JOIN SA_IVH IVH ON B.NO_IV = IVH.NO_IV AND B.CD_COMPANY = IVH.CD_COMPANY
LEFT JOIN MA_BIZAREA E ON IVH.CD_BIZAREA_TAX = E.CD_BIZAREA AND IVH.CD_COMPANY = E.CD_COMPANY
LEFT JOIN SA_PROJECTH PH ON PH.CD_COMPANY = FD.CD_COMPANY AND PH.NO_PROJECT = FD.CD_PJT        
WHERE B.NO_BILLS = @P_NO_BILLS 
AND B.CD_COMPANY = @P_CD_COMPANY 
AND B.AM_PL < 0 
AND A.ST_BILL = 'N' -- 전표처리여부             
AND L.FG_TP = '300' -- 형태코드 : 수금형태('300')          
AND L.FG_AIS = '313'  -- 환차익    
GROUP BY A.CD_COMPANY,               
		 A.NO_BILLS,               
		 D.CD_DEPT,               
		 A.BILL_PARTNER,               
		 A.NO_EMP,               
		 A.DT_BILLS,              
		 A.DT_BILLS,               
		 A.TP_BUSI,               
		 CC.CD_CC,               
		 A.NO_EMP,                  
		 B.CD_EXCH_IV,     
		 B.CD_COMPANY,                       
		 B.RT_EXCH_IV,              
		 D.CD_DEPT,          
		 CC.CD_PC,    
		 L.FG_AIS,              
		 L.CD_ACCT,              
		 D.CD_DEPT,    
		 D.CD_COMPANY,              
		 CC.NM_CC,    
		 P.NO_COMPANY,    
		 IVH.CD_BIZAREA_TAX,    
		 E.NM_BIZAREA,  
		 A.CD_DOCU, -- 전표유형
		 PH.NO_PROJECT,
		 PH.NM_PROJECT   

-------------------------------------------              
-- 여기서 부터 전표처리 하기 위한 부분  ---              
DECLARE @P_NO_DOCU NVARCHAR(20) -- 전표번호              
DECLARE @P_DT_PROCESS NVARCHAR(8)              
DECLARE @P_NO_ACCT NUMERIC(5, 0) -- 회계번호
DECLARE @P_ID_ACCT NVARCHAR(15)	 -- 승인자

SELECT @CUR_DATE = CONVERT(NVARCHAR(8), GETDATE(), 112)
            
-- 수금일자 알아오기              
SELECT @P_DT_PROCESS = DT_BILLS    
FROM SA_BILLSH              
WHERE CD_COMPANY = @P_CD_COMPANY AND NO_BILLS  = @P_NO_BILLS              
              
-- 전표번호 채번              
EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT              

-- 회계번호 채번
SET @P_NO_ACCT = 0
SET @P_ID_ACCT = NULL
              
-- NO_DOLINE 땜시              
DECLARE @P_NO_DOLINE NUMERIC(4,0)              
SET @P_NO_DOLINE = 0              
              
-- 금액 문자로              
DECLARE @AM_DR_CR VARCHAR(40)              
              
OPEN SRC_CURSOR              
FETCH NEXT FROM SRC_CURSOR INTO @CD_COMPANY,@NO_BILLS,@CD_BIZAREA ,@CD_WDEPT,@BILL_PARTNER,@FG_TAX ,@ID_WRITE ,@DT_PROCESS ,@AM_DR   ,@AM_CR, @AM_SUPPLY, @DT_TAX    ,@FG_TRANS   ,@CD_CC   ,@NO_EMP, @CD_PJT, @CD_EXCH , @NM_EXCH, @RT_EXCH ,@CD_DEPT ,@CD_PC,         
@NM_BIZAREA ,@FG_AIS,@CD_ACCT ,@CLS_ITEM ,@TP_DRCR ,@NM_DEPT ,@NM_CC  ,@NM_EMP  ,@NM_PJT  ,@NM_PARTNER, @T_CD_RELATION, @NM_TAX, @NO_COMPANY, @TP_ACAREA,    
 @V_CD_DEPOSIT,  -- 예적금코드          
 @V_CD_CARD,      -- 신용카드          
 @V_CD_BANK,      -- 금융기관           
 @V_NM_BANK,      -- 금융기관          
 @V_CD_DEPOSIT, --예적금계좌 @V_CD_MNG,       -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
 @V_DT_END,       -- 어음만기일자          
 @V_AM_EXDO,      -- 외화금액                    
 @V_CD_DOCU,
 @V_NO_BDOCU,
 @V_NO_BDOLINE   
        
WHILE @@FETCH_STATUS = 0              
BEGIN
 SET @P_NO_DOLINE = @P_NO_DOLINE + 1              
 SET @AM_DR_CR = CONVERT(VARCHAR(40), @AM_SUPPLY)              
    
IF @T_CD_RELATION <> '10'  /* 부가세관련계정(10)이 아니면 */    
SET @V_NO_RCP = @NO_BILLS     
ELSE    
SET @V_NO_RCP = NULL    
              
    
SET @V_NM_NOTE      = NULL   --적요 /*네오팜임시20090121*/    
    
SELECT  @V_CD_TP = CD_TP FROM SA_BILLSH WHERE NO_BILLS = @P_NO_BILLS AND CD_COMPANY = @P_CD_COMPANY    
SELECT  @V_NM_TP = NM_TP FROM MA_AISPOSTH WHERE CD_COMPANY = @P_CD_COMPANY AND FG_TP = '300' AND CD_TP = @V_CD_TP    
    
SELECT @V_NM_NOTE = @NM_PARTNER + '(' + @V_NM_TP + ')'    

IF (@V_SERVER_KEY = 'ENTEC')   --인텍전기전자
BEGIN
    SET @V_NM_NOTE = @V_NM_TP
END

SET @V_CD_DOCU = ISNULL(@V_CD_DOCU,'34')

-- 증빙 추가
SET @V_TP_EVIDENCE = (SELECT TP_EVIDENCE
					  FROM CZ_FI_ACCT_EVIDENCEL
					  WHERE CD_COMPANY = @CD_COMPANY
					  AND ISNULL(FG_TAX, '') = ''
					  AND CD_ACCT = @CD_ACCT)
    
EXEC UP_FI_AUTODOCU_1 @P_NO_DOCU,                        -- 전표번호              
					  @P_NO_DOLINE,                      -- 라인번호              
					  @CD_PC,                            -- 회계단위              
					  @CD_COMPANY,                       -- 회사코드                 
					  @CD_WDEPT,                         -- 작성부서              
					  @ID_WRITE,                         -- 작성자              
					  @P_DT_PROCESS,                     -- 매출일자 = 회계일자 = 처리일자              
					  @P_NO_ACCT,                        -- 회계번호 NO_ACCT
					  '3',                               -- 전표구분-대체 TP_DOCU              
					  @V_CD_DOCU,                        -- 전표유형-일반 CD_DOCU    '11'-->'34(선수금정리)'으로 수정     
					  @V_ST_DOCU,                        -- 전표상태-미결 ST_DOCU              
					  @P_ID_ACCT,                        -- 승인자              
					  @TP_DRCR,                          -- 차대구분               
					  @CD_ACCT,                          -- 계정코드              
					  @V_NM_NOTE,                        -- 적요  NULL            
					  @AM_DR,                            -- 차변금액                 
					  @AM_CR,                            -- 대변금액              
					  @TP_ACAREA,                        --   '4' 일 경우 반제원인전표이므로 FI_BANH에 Insert된다        
					  @T_CD_RELATION,                    -- 연동항목-일반 CD_RELATION                
					  NULL,                                        -- 예산코드 CD_BUDGET                 
					  NULL,                                        -- 자금과목 CD_FUND                 
					  @V_NO_BDOCU,                                 -- 원인전표번호 NO_BDOCU                 
					  @V_NO_BDOLINE,                               -- 원인전표라인 NO_BDOLINE      
					  '0',                                                -- 타대구분 TP_ETCACCT                 
					  @CD_BIZAREA,                                -- 귀속사업장      
					  @NM_BIZAREA,    
					  @CD_CC,                                        -- 코스트센터              
					  @NM_CC,    
					  @CD_PJT,                                        -- 프로젝트     
					  @NM_PJT,    
					  @CD_DEPT,                                -- 부서     
					  @NM_DEPT,    
					  @NO_EMP,                                -- 사원 CD_EMPLOY         
					  @NM_EMP,    
					  @BILL_PARTNER,                        -- 거래처 CD_PARTNER              
					  @NM_PARTNER,    
					  @V_CD_DEPOSIT,                -- 예적금코드 CD_DEPOSIT     
					  @V_CD_DEPOSIT,                -- NM_DEPOSIT    
					  @V_CD_CARD,                        -- 카드번호 CD_CARD      
					  @V_CD_CARD,                        -- NM_CARD              
					  @V_CD_BANK,                        -- 은행코드 CD_BANK  : MA_PARTNER에서 FG_PARTNER = '002' 인것     
					  @V_NM_BANK,                        -- NM_BANK             
					  NULL,                                        -- 품목코드 NO_ITEM      
					  NULL,                                        -- NM_ITEM      
					  @FG_TAX,                                -- 세무구분코드 TP_TAX      
					  @NM_TAX,                                -- 세무구분명       
					  @FG_TRANS,                                -- 거래구분코드 CD_TRADE     거래구분(자산변동,지급어음정리,받을어음정리,유가증권거래구분) * 받을어음 : FI_F000006 지급어음 : FI_F000007                  
					  NULL,                                        -- 거래구분명   NM_TRADE        
					  @CD_EXCH,                                -- 환종      
					  @NM_EXCH,                                -- 환종명 @NM_EXCH                  
					  NULL,                                        -- CD_UMNG1                
					  NULL,                -- CD_UMNG2                
					  NULL,                                        -- CD_UMNG3                
					  NULL,                                        -- CD_UMNG4                
					  NULL,                                        -- CD_UMNG5         
					  @NO_COMPANY,                        --  NO_RES      
					  @AM_SUPPLY,                                --  AM_SUPPLY      
					  @V_CD_MNG,                        -- 관리번호 = 계산서번호 or 어음번호 CD_MNG                 
					  @DT_PROCESS,                -- 거래일자, 시작일자, 발생일자 DT_START                
					  @V_DT_END,                        -- 만기일자 DT_END                 
					  @RT_EXCH,                                -- 환율               
					  @V_AM_EXDO,                        -- 외화금액 AM_EXDO               
					  '151',                                        -- 모듈구분(국내매출:002 수금등록:130) NO_MODULE       
					  @NO_BILLS,                                -- 모듈관리번호 = 타모듈pkey NO_MDOCU                
					  NULL,                                        -- 지출결의코드 CD_EPNOTE               
					  @ID_WRITE,                                -- 전표처리자              
					  NULL,                                        -- 예산계정 CD_BGACCT               
					  NULL,                                        -- 결의구분 TP_EPNOTE               
					  NULL,                                        -- 품의내역 NM_PUMM                
					  @CUR_DATE, --시스템일자로 20100506, @P_DT_PROCESS,                        -- 작성일자 DT_WRITE                
					  0,                                                -- AM_ACTSUM               
					  0,                                                -- AM_JSUM                
					  'N',                                            -- YN_GWARE                
					  NULL,                                        -- 사업계획코드 CD_BIZPLAN              
					  NULL,                                        --CD_ETC      
					  @P_ERRORCODE,      
					  @P_ERRORMSG,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  0,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  @V_TP_EVIDENCE -- 증빙 TP_EVIDENCE

--   IF @T_CD_RELATION = '31'           
--   BEGIN          
--        EXEC UP_FI_AUTODOCU_TAX @CD_COMPANY, @CD_PC, @P_NO_DOCU, @P_NO_DOLINE, @AM_SUPPLY          
--   END                  
    
--IF( @CD_ACCT = '11000' ) /* 만일 어음일 경우 (이건수금예제임) */    
--BEGIN    
-- EXEC  UP_SA_BILL_AUTO_SLIP_EHUM  @CD_COMPANY, @V_CD_MNG, @P_NO_DOCU, @P_NO_DOLINE, '1'    
--END    
                 
 FETCH NEXT FROM SRC_CURSOR INTO @CD_COMPANY,@NO_BILLS,@CD_BIZAREA ,@CD_WDEPT,@BILL_PARTNER,@FG_TAX ,@ID_WRITE ,@DT_PROCESS ,@AM_DR   ,@AM_CR   , @AM_SUPPLY, @DT_TAX    ,@FG_TRANS   ,@CD_CC   ,@NO_EMP , @CD_PJT,  @CD_EXCH , @NM_EXCH, @RT_EXCH, @CD_DEPT, @CD_PC,        
 @NM_BIZAREA ,@FG_AIS,  @CD_ACCT ,@CLS_ITEM ,@TP_DRCR ,@NM_DEPT ,@NM_CC  ,@NM_EMP  ,@NM_PJT  ,@NM_PARTNER, @T_CD_RELATION, @NM_TAX, @NO_COMPANY,  @TP_ACAREA,    
 @V_CD_DEPOSIT,  -- 예적금코드          
 @V_CD_CARD,      -- 신용카드          
 @V_CD_BANK,      -- 금융기관          
 @V_NM_BANK,      -- 금융기관          
 @V_CD_DEPOSIT, --예적금계좌 @V_CD_MNG,       -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
 @V_DT_END,       -- 어음만기일자          
 @V_AM_EXDO,      -- 외화금액                    
 @V_CD_DOCU,
 @V_NO_BDOCU,
 @V_NO_BDOLINE 
END              
              
CLOSE SRC_CURSOR              
DEALLOCATE SRC_CURSOR              
      
      
-- 전표처리가 제대로 되었으면                
UPDATE SA_BILLSH SET ST_BILL = 'Y' WHERE CD_COMPANY = @P_CD_COMPANY AND NO_BILLS = @P_NO_BILLS AND ST_BILL = 'N'
          
IF ( @@ROWCOUNT = 0  )  
BEGIN  
      SELECT @ERRNO  = 100000,    
             @ERRMSG = '전표처리에러- 해당 수금데이터의 상태가 처리중에 변경되었습니다. :' + @P_NO_BILLS   
      GOTO ERROR    
END      
RETURN                
      
  
ERROR:   
    ROLLBACK TRAN 
    RAISERROR (@ERRMSG, 18 , 1)
GO

