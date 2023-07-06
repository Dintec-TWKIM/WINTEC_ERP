USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_ADPAYMENT_TRANS_DOCU]    Script Date: 2015-05-29 오전 8:56:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PU_ADPAYMENT_TRANS_DOCU] 
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_LANGUAGE		NVARCHAR(5) = 'KR',
	@P_NO_ADPAY		NVARCHAR(20),
	@P_NO_PO		NVARCHAR(20),
	@P_NO_MODULE	NVARCHAR(3), -- 회계전표유형 : 선급금(250) 
    @P_FG_EX_CC		NVARCHAR(3) = '000', -- CC Group By 제외 옵션
	@P_ID_INSERT	NVARCHAR(15)          
) 
AS 
    DECLARE @IN_CD_COMPANY NVARCHAR(7) 
    DECLARE @IN_NO_MDOCU NVARCHAR(20) 
    DECLARE @V_IN_NO_MDOCU NVARCHAR(20) 
    DECLARE @IN_CD_PARTNER NVARCHAR(20) 
    DECLARE @IN_NO_COMPANY NVARCHAR(20) 
    DECLARE @IN_CD_CC NVARCHAR(24) 
    DECLARE @IN_CD_PJT NVARCHAR(20) 
    DECLARE @IN_ID_WRITE NVARCHAR(10) 
    DECLARE @IN_CD_BIZAREA NVARCHAR(7) 
    DECLARE @NO_BIZAREA NVARCHAR(20) 
    DECLARE @IN_TP_TAX NCHAR(3) 
    DECLARE @NM_TAX NCHAR(15) 
    DECLARE @IN_FG_TRANS NCHAR(3) 
    DECLARE @IN_CD_WDEPT NVARCHAR(12) 
    DECLARE @IN_DT_ACCT NCHAR(8) 
    DECLARE @IN_CD_PC NVARCHAR(7) 
    DECLARE @IN_CD_ACCT NVARCHAR(10) 
    DECLARE @IN_AM_DR NUMERIC(19, 4) 
    DECLARE @IN_AM_CR NUMERIC(19, 4) 
    DECLARE @IN_AM_SUPPLY NUMERIC(19, 4) 
    DECLARE @IN_TP_ACAREA NVARCHAR(3) 
    DECLARE @IN_TP_DRCR NCHAR(1) 
    DECLARE @IN_CD_EXCH NVARCHAR(3) 
    DECLARE @IN_RT_EXCH NUMERIC(17, 4) 
    DECLARE @IN_CD_EMPLOY NVARCHAR(10) 
    DECLARE @IN_AM_EXDO NUMERIC(19, 4) 
    DECLARE @IN_NM_BIZAREA NVARCHAR(50) 
    DECLARE @IN_NM_CC NVARCHAR(50) 
    DECLARE @IN_NM_KOR NVARCHAR(50) 
    DECLARE @IN_LN_PARTNER NVARCHAR(50) 
    DECLARE @IN_NM_PROJECT NVARCHAR(50) 
    DECLARE @IN_NM_DEPT NVARCHAR(50) 
    DECLARE @IN_TYPE NCHAR(1) 
    DECLARE @IN_CD_RELATION NCHAR(3) 
    DECLARE @P_NOTE NVARCHAR(100) 
    DECLARE @P_NM_TP NVARCHAR(20) 
    DECLARE @P_ERRORCODE NCHAR(10) 
    DECLARE @P_ERRORMSG NVARCHAR(300) 
    DECLARE @ERRNO INT -- ERROR 번호                                                     
    DECLARE @ERRMSG NVARCHAR(255) -- ERROR 메시지                                                     
    DECLARE @CD_DOCU NVARCHAR(3) -- 전표유형                                           
    DECLARE @DT_PAY_PREARRANGED NVARCHAR(8) -- 지급예정일 (전표 자금예정일로 넘어간다)                                         
    DECLARE @CD_FUND NVARCHAR(4) -- 자금과목                                                           
    DECLARE @CD_BUDGET NVARCHAR(10) -- 예산과목                                   
    DECLARE @CD_BGACCT NVARCHAR(10) -- 예산계정과목                                   
    DECLARE @FG_PAYBILL NVARCHAR(3) -- 지급조건                                       
    DECLARE @FG_BUDGET NVARCHAR(3) -- 예산체크                                                                
    DECLARE @CUR_DATE NVARCHAR(8) --현재일자                                              
    DECLARE @AM_TOT NUMERIC(17, 4) /*추가: 합계금액 0은 회계전표안만들고 전표처리 20090518*/              
    DECLARE @V_SERVER_KEY NVARCHAR(50) --서버키 --서버키처리추가 (대신정보 표준적요설정 2010-07-01 LSH)                
    DECLARE @NM_EXCH NVARCHAR(50) 
    DECLARE @NO_LC NVARCHAR(20) 
    DECLARE @DT_DUE NVARCHAR(8) --만기일자     
    DECLARE @AM_EX NUMERIC(19, 4) --외화금액   
    DECLARE @CD_EXCH NVARCHAR(3) --환율   
    DECLARE @RT_VAT NUMERIC(19, 15) --과세율   
    DECLARE @P_YN_ISS NVARCHAR(1) --- 계산서발행형태   
    DECLARE @V_CD_UMNG2 NVARCHAR(20) = NULL    
    DECLARE @V_VAT_TAX NUMERIC(17, 4) = 0 -- 사용자정의 관리항목 A22 (AVL에서 NO_INVOICE 사용) 2012.07.27 전정식
    DECLARE @P_PO_CONDITION NVARCHAR(4) 
    DECLARE @P_NM_CONDITION NVARCHAR(60)
	DECLARE @V_ST_DOCU		NVARCHAR(1)
	DECLARE @V_TP_EVIDENCE	NVARCHAR(1) -- 증빙
	DECLARE @V_CD_BANK		NVARCHAR(20) -- 은행코드
	DECLARE @V_NM_BANK		NVARCHAR(50) -- 은행명
	DECLARE @V_CD_DEPOSIT	NVARCHAR(20) -- 계좌코드
	DECLARE @V_NO_DEPOSIT	NVARCHAR(20) -- 계좌번호

    -------------------------------------------------------------------------------------------------------   
    SELECT @V_SERVER_KEY = MAX(SERVER_KEY) 
      FROM CM_SERVER_CONFIG 
     WHERE YN_UPGRADE = 'Y' 

    SELECT @CUR_DATE = CONVERT(NVARCHAR(8), GETDATE(), 112) 

    --SELECT @AM_TOT = ( AM_K + VAT_TAX )     
    --  FROM PU_IVH A                               
    --WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV                                                                 
    --IF ( @AM_TOT = 0 ) /*공급금액+부가세 = 0이면 회계전표안만들고 TP_AIS = 'Y'처리 20090210*/                                                   
    --BEGIN                                                   
    -- UPDATE PU_IVH SET TP_AIS = 'Y' WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV                     
    -- RETURN                                                   
    --END                                                    
    -- 예산통제 체크             
    SELECT @FG_BUDGET = CD_EXC 
      FROM MA_EXC 
     WHERE CD_COMPANY = @P_CD_COMPANY 
       AND EXC_TITLE = '구매예산CHK' 
       AND CD_MODULE = 'PU' 

	SET @V_ST_DOCU = '1'

DECLARE CUR_PU_ADPAYMENT CURSOR FOR 
SELECT AP.CD_COMPANY,
	   AP.NO_ADPAY AS NO_MDOCU,
	   PH.CD_PARTNER,
	   PL.CD_CC,
	   CC.NM_CC,
	   PL.CD_PJT,
	   PH.NO_EMP AS ID_WRITE,
	   AP.NO_BIZAREA AS CD_BIZAREA,
	   PL.FG_TAX AS TP_TAX,
	   PH.FG_TRANS,
	   AP.CD_DEPT AS CD_WDEPT,
	   AP.DT_ACCT,
	   MB.CD_PC,
	   AL.CD_ACCT,
	   SUM(CASE WHEN PL.YN_RETURN = 'Y' THEN -AP.AM ELSE AP.AM END) AS AM_DR,
	   0 AS AM_CR,
	   0 AS AM_SUPPLY,
	   '1' AS TP_DRCR,
	   PH.CD_EXCH,
	   PH.RT_EXCH,
	   PH.NO_EMP AS CD_EMPLOY,
	   SUM(CASE WHEN PL.YN_RETURN = 'Y' THEN -AP.AM_EX ELSE AP.AM_EX END) AS AM_EXDO,
	   MB.NM_BIZAREA,
	   ME.NM_KOR,
	   MP.LN_PARTNER,
	   PJ.NM_PROJECT,
	   MD.NM_DEPT,
	   'A' AS TYPE,
	   FA.CD_RELATION,
	   AP.NO_BIZAREA,
	   MC.NM_SYSDEF AS NM_TAX,
	   MP.NO_COMPANY,
	   (CASE WHEN FA.TP_DRCR = '1' AND FA.YN_BAN = 'Y' THEN '4' ELSE '0' END) AS TP_ACAREA,
	   AP.CD_DOCU,
	   AP.DT_PAY_SCHEDULE AS DT_PAY_PREARRANGED,
	   AL.CD_FUND,
	   (CASE WHEN @FG_BUDGET = 'Y' AND ISNULL(PL.CD_BGACCT, '') = '' THEN AL.CD_ACCT 
																	 ELSE ISNULL(PL.CD_BGACCT, '') END) AS CD_BGACCT,                                                              
       (CASE WHEN @FG_BUDGET = 'Y' AND ISNULL(PL.CD_BUDGET, '') = '' THEN AP.CD_DEPT 
																	 ELSE ISNULL(PL.CD_BUDGET, '') END) AS CD_BUDGET,
	   AH.NM_TP,
	   PH.FG_PAYMENT,
	   '' AS NO_LC,
	   '' AS DT_DUE,
	   AP.YN_JEONJA,
	   SUM(CASE PL.YN_RETURN WHEN 'Y' THEN 0 - PL.VAT ELSE PL.VAT END) AS VAT_TAX,
	   AP.PO_CONDITION,
	   MC1.NM_SYSDEF AS NM_CONDITION
FROM PU_ADPAYMENT AP
JOIN PU_POL PL ON PL.CD_COMPANY = AP.CD_COMPANY AND PL.NO_PO = AP.NO_PO AND PL.NO_LINE = AP.NO_POLINE
JOIN PU_POH PH ON PH.CD_COMPANY = PL.CD_COMPANY AND PH.NO_PO = PL.NO_PO
JOIN MA_AISPOSTH AH ON AH.CD_COMPANY = PL.CD_COMPANY AND AH.FG_TP = '200' AND AH.CD_TP = PL.FG_PURCHASE
JOIN MA_AISPOSTL AL ON AL.CD_COMPANY = AH.CD_COMPANY AND AL.FG_TP = AH.FG_TP AND AL.CD_TP = AH.CD_TP AND AL.FG_AIS = '216'
JOIN FI_ACCTCODE FA ON FA.CD_COMPANY = AL.CD_COMPANY AND FA.CD_ACCT = AL.CD_ACCT
LEFT JOIN MA_CC CC ON CC.CD_COMPANY = PL.CD_COMPANY AND CC.CD_CC = PL.CD_CC
LEFT JOIN MA_BIZAREA MB ON MB.CD_COMPANY = AP.CD_COMPANY AND MB.CD_BIZAREA = AP.NO_BIZAREA
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = PH.CD_COMPANY AND ME.NO_EMP = PH.NO_EMP
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = AP.CD_COMPANY AND MD.CD_DEPT = AP.CD_DEPT
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PL.CD_COMPANY AND MC.CD_FIELD = 'FI_T000011' AND MC.CD_SYSDEF = PL.FG_TAX
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = AP.CD_COMPANY AND MC1.CD_FIELD = 'PU_C000078' AND MC1.CD_SYSDEF = AP.PO_CONDITION
LEFT JOIN (SELECT CD_COMPANY, NO_PROJECT,
				  MAX(NM_PROJECT) AS NM_PROJECT 
		   FROM SA_PROJECTH
		   GROUP BY CD_COMPANY, NO_PROJECT) PJ 
ON PJ.CD_COMPANY = PL.CD_COMPANY AND PJ.NO_PROJECT = PL.CD_PJT
WHERE AP.CD_COMPANY = @P_CD_COMPANY
AND AP.NO_ADPAY = @P_NO_ADPAY
AND AP.AM <> 0
AND ISNULL(AP.TP_AIS, 'Y') = 'Y'
GROUP BY AP.CD_COMPANY,
		 AP.NO_ADPAY,
		 PH.CD_PARTNER,
		 PL.CD_CC,
		 CC.NM_CC,
		 PL.CD_PJT,
		 PH.NO_EMP,
		 AP.NO_BIZAREA,
		 PL.FG_TAX,
		 PH.FG_TRANS,
		 AP.CD_DEPT,
		 AP.DT_ACCT,
		 MB.CD_PC,
	     AL.CD_ACCT,
		 PH.CD_EXCH,
		 PH.RT_EXCH,
		 PH.NO_EMP,
		 MB.NM_BIZAREA,
		 ME.NM_KOR,
		 MP.LN_PARTNER,
		 PJ.NM_PROJECT,
		 MD.NM_DEPT,
		 FA.CD_RELATION,
		 AP.NO_BIZAREA,
		 MC.NM_SYSDEF,
		 MP.NO_COMPANY,
		 FA.TP_DRCR,
		 FA.YN_BAN,
		 AP.CD_DOCU,
		 AP.DT_PAY_SCHEDULE,
		 AL.CD_FUND,
		 PL.CD_BGACCT,
		 PL.CD_BUDGET,
		 AH.NM_TP,
		 PH.FG_PAYMENT,
		 AP.YN_JEONJA,
		 AP.PO_CONDITION,
		 MC1.NM_SYSDEF
---- 부가세계정 : 차변: 부가세                          
--UNION ALL            
-- SELECT                                                  
--  H.CD_COMPANY ,L.NO_IV AS NO_MDOCU, H.CD_PARTNER,            
--  '' CD_CC,           
--  '' AS NM_CC,            
--  '' CD_PJT , H.NO_EMP  AS ID_WRITE, H.CD_BIZAREA_TAX AS CD_BIZAREA,                                                          
--  H.FG_TAX AS TP_TAX, H.FG_TRANS,H.CD_DEPT AS CD_WDEPT, H.DT_PROCESS AS DT_ACCT, B.CD_PC, A.CD_ACCT,                                                         
--  SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS  AM_DR,                                                    
--  0 AS AM_CR,                                                          
--  SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END ) AS  AM_SUPPLY,                                                         
--  '1' AS TP_DRCR  ,   
--  '000' AS CD_EXCH,    
--  1 AS RT_EXCH,    
--  '' AS CD_EMPLOY,                                                         
--  SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS AM_EXDO,                                                         
--  B.NM_BIZAREA,     
--  E.NM_KOR, P.LN_PARTNER, '' AS NM_PROJECT, D.NM_DEPT,   
--  'V' AS TYPE,    
--  '30' CD_RELATION, --연동항목:일반                                                          
--  B.NO_BIZAREA,                                                     
--  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,                          
--  P.NO_COMPANY, '0' TP_ACAREA ,                                           
--  H.CD_DOCU,                  
--  H.DT_PROCESS, --,H.DT_PAY_PREARRANGED                                         
--  '' AS CD_FUND,    -- 자금과목                                   
--  '' AS CD_BGACCT,    -- 예산과목                                   
--  '' AS CD_BUDGET,                               
--  MAX(MH.NM_TP) AS NM_TP,  -- 매출유형명                                 
--  L.FG_PAYMENT AS FG_PAYBILL,  -- 지급조건      
--  '' AS NO_LC,                     
--  '' AS DT_DUE,   
--    H.YN_JEONJA,   
--    SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS VAT_TAX            
--  FROM                                                        
--  PU_POH H                                                        
--   JOIN PU_POL L ON H.NO_PO  = L.NO_PO AND H.CD_COMPANY = L.CD_COMPANY                                                          
--   JOIN MA_PITEM M ON L.CD_ITEM = M.CD_ITEM  AND L.CD_PLANT = M.CD_PLANT  AND L.CD_COMPANY = M.CD_COMPANY                                            
--   JOIN MA_AISPOSTL A ON L.CD_COMPANY = A.CD_COMPANY  AND A.FG_TP ='200'  AND L.FG_TPPURCHASE = A.CD_TP                                                  
--   JOIN MA_AISPOSTH MH ON                                 
--       A.CD_COMPANY = MH.CD_COMPANY AND                                            
--       A.CD_TP = MH.CD_TP AND -- 매출형태                                  
--       MH.FG_TP='200'                            
--   JOIN FI_ACCTCODE FAT ON FAT.CD_ACCT = A.CD_ACCT AND FAT.CD_COMPANY = A.CD_COMPANY                                                         
--   JOIN MA_BIZAREA B ON H.CD_BIZAREA_TAX = B.CD_BIZAREA  AND H.CD_COMPANY = B.CD_COMPANY                                                          
--   --JOIN PU_POL POL ON L.NO_PO  = POL.NO_PO AND L.NO_POLINE = POL.NO_LINE AND L.CD_COMPANY = POL.CD_COMPANY                                    
--   LEFT JOIN MA_EMP E ON H.NO_EMP = E.NO_EMP AND H.CD_COMPANY = E.CD_COMPANY                                                         
--   LEFT JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER  AND H.CD_COMPANY = P.CD_COMPANY                                                                 
--   LEFT JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT  AND H.CD_COMPANY = D.CD_COMPANY                                                         
--  WHERE                                                        
--  H.NO_IV = @P_NO_IV                                                         
--  AND H.CD_COMPANY = @P_CD_COMPANY                                                        
--  AND H.TP_AIS ='N'  -- 전표발생여부                                                         
--  AND H.YN_PURSUB ='N' --구매                              
--  AND A.FG_AIS = '210' --부가세          
--  AND H.FG_TAX <> '99'                                             
--  GROUP BY                         
--  H.CD_COMPANY ,L.NO_IV, H.CD_PARTNER, H.NO_EMP , H.CD_BIZAREA_TAX,                           
--  H.FG_TAX, H.FG_TRANS,H.CD_DEPT, H.DT_PROCESS, B.CD_PC, A.CD_ACCT,                     
--  B.NM_BIZAREA,  E.NM_KOR, P.LN_PARTNER, D.NM_DEPT, B.NO_BIZAREA, P.NO_COMPANY,                                           
--  H.CD_DOCU,--,  --,H.DT_PAY_PREARRANGED                                                                          
--  H.YN_JEONJA           
------ 채무계정(외상매입금) : 대변: 원화금액+부가세                                                          
UNION ALL 
SELECT AP.CD_COMPANY,
	   AP.NO_ADPAY AS NO_MDOCU,
	   PH.CD_PARTNER,
	   (CASE WHEN @P_FG_EX_CC = '001' THEN '' ELSE PL.CD_CC END) AS CD_CC,
	   (CASE WHEN @P_FG_EX_CC = '001' THEN '' ELSE CC.NM_CC END) AS NM_CC,
	   (CASE WHEN @P_FG_EX_CC = '002' THEN '' ELSE PL.CD_PJT END) AS CD_PJT, 
	   PH.NO_EMP AS ID_WRITE,
	   AP.NO_BIZAREA AS CD_BIZAREA,
	   '' AS TP_TAX,
	   PH.FG_TRANS,
	   AP.CD_DEPT AS CD_WDEPT,
	   AP.DT_ACCT,
	   MB.CD_PC,
	   AL.CD_ACCT,
	   0 AS AM_DR,
	   SUM(CASE WHEN PL.YN_RETURN = 'Y' THEN -AP.AM ELSE AP.AM END) AS AM_CR,
	   0 AS AM_SUPPLY,
	   '2' AS TP_DRCR,
	   PH.CD_EXCH,
	   PH.RT_EXCH,
	   PH.NO_EMP AS CD_EMPLOY,
	   SUM(CASE WHEN PL.YN_RETURN = 'Y' THEN -AP.AM_EX ELSE AP.AM_EX END) AS AM_EXDO,
	   MB.NM_BIZAREA,
	   ME.NM_KOR,
	   MP.LN_PARTNER,
	   (CASE WHEN @P_FG_EX_CC = '002' THEN '' ELSE PJ.NM_PROJECT END) AS NM_PROJECT,
	   MD.NM_DEPT,
	   'T' AS TYPE,
	   FA.CD_RELATION,
	   AP.NO_BIZAREA,
	   '' AS NM_TAX,
	   MP.NO_COMPANY,
	   (CASE WHEN FA.TP_DRCR = '2' AND FA.YN_BAN = 'Y' THEN '4' ELSE '0' END) AS TP_ACAREA,
	   AP.CD_DOCU,
	   AP.DT_PAY_SCHEDULE AS DT_PAY_PREARRANGED,
	   AL.CD_FUND,
	   '' AS CD_BGACCT,                                                              
       '' AS CD_BUDGET,
	   AH.NM_TP,
	   PH.FG_PAYMENT,
	   '' AS NO_LC,
	   '' AS DT_DUE,
	   AP.YN_JEONJA,
	   SUM(CASE PL.YN_RETURN WHEN 'Y' THEN 0 - PL.VAT ELSE PL.VAT END) AS VAT_TAX,
	   AP.PO_CONDITION,
	   MC.NM_SYSDEF AS NM_CONDITION
FROM PU_ADPAYMENT AP
JOIN PU_POL PL ON PL.CD_COMPANY = AP.CD_COMPANY AND PL.NO_PO = AP.NO_PO AND PL.NO_LINE = AP.NO_POLINE
JOIN PU_POH PH ON PH.CD_COMPANY = PL.CD_COMPANY AND PH.NO_PO = PL.NO_PO
JOIN MA_AISPOSTH AH ON AH.CD_COMPANY = PL.CD_COMPANY AND AH.FG_TP = '200' AND AH.CD_TP = PL.FG_PURCHASE
JOIN MA_AISPOSTL AL ON AL.CD_COMPANY = AH.CD_COMPANY AND AL.FG_TP = AH.FG_TP AND AL.CD_TP = AH.CD_TP AND AL.FG_AIS = '217'
JOIN FI_ACCTCODE FA ON FA.CD_COMPANY = AL.CD_COMPANY AND FA.CD_ACCT = AL.CD_ACCT
LEFT JOIN MA_CC CC ON CC.CD_COMPANY = PL.CD_COMPANY AND CC.CD_CC = PL.CD_CC
LEFT JOIN MA_BIZAREA MB ON MB.CD_COMPANY = AP.CD_COMPANY AND MB.CD_BIZAREA = AP.NO_BIZAREA
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = PH.CD_COMPANY AND ME.NO_EMP = PH.NO_EMP
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = AP.CD_COMPANY AND MD.CD_DEPT = AP.CD_DEPT
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = AP.CD_COMPANY AND MC.CD_FIELD = 'PU_C000078' AND MC.CD_SYSDEF = AP.PO_CONDITION
LEFT JOIN (SELECT CD_COMPANY, NO_PROJECT,
				  MAX(NM_PROJECT) AS NM_PROJECT 
		   FROM SA_PROJECTH
		   GROUP BY CD_COMPANY, NO_PROJECT) PJ 
ON PJ.CD_COMPANY = PL.CD_COMPANY AND PJ.NO_PROJECT = PL.CD_PJT
WHERE AP.CD_COMPANY = @P_CD_COMPANY
AND AP.NO_ADPAY = @P_NO_ADPAY
AND AP.AM <> 0
AND ISNULL(AP.TP_AIS, 'Y') = 'Y'
GROUP BY AP.CD_COMPANY,
		 AP.NO_ADPAY,
		 PH.CD_PARTNER,
		 (CASE WHEN @P_FG_EX_CC = '001' THEN '' ELSE PL.CD_CC END),
	     (CASE WHEN @P_FG_EX_CC = '001' THEN '' ELSE CC.NM_CC END),
	     (CASE WHEN @P_FG_EX_CC = '002' THEN '' ELSE PL.CD_PJT END), 
		 PH.NO_EMP,
		 AP.NO_BIZAREA,
		 PL.FG_TAX,
		 PH.FG_TRANS,
		 AP.CD_DEPT,
		 AP.DT_ACCT,
		 MB.CD_PC,
	     AL.CD_ACCT,
		 PH.CD_EXCH,
		 PH.RT_EXCH,
		 PH.NO_EMP,
		 MB.NM_BIZAREA,
		 ME.NM_KOR,
		 MP.LN_PARTNER,
		 (CASE WHEN @P_FG_EX_CC = '002' THEN '' ELSE PJ.NM_PROJECT END),
		 MD.NM_DEPT,
		 FA.CD_RELATION,
		 AP.NO_BIZAREA,
		 MC.NM_SYSDEF,
		 MP.NO_COMPANY,
		 FA.TP_DRCR,
		 FA.YN_BAN,
		 AP.CD_DOCU,
		 AP.DT_PAY_SCHEDULE,
		 AL.CD_FUND,
		 PL.CD_BGACCT,
		 PL.CD_BUDGET,
		 AH.NM_TP,
		 PH.FG_PAYMENT,
		 AP.YN_JEONJA,
		 AP.PO_CONDITION,
		 MC.NM_SYSDEF
------------------------------------------------------------------------------------------------------------------------------------------------  -----------------------------------------------------------------------------------------------------        
-- 여기서 부터 전표처리 하기 위한 부분  ---                                                         
DECLARE @P_NO_DOCU NVARCHAR(20) 
-- 전표번호                                  
DECLARE @P_DT_PROCESS NVARCHAR(8) 
DECLARE @P_NO_ACCT NUMERIC(5, 0)   -- 회계번호
DECLARE @P_ID_ACCT NVARCHAR(15)    -- 승인자

-- 선지급일자 알아오기                                                         
SELECT @P_DT_PROCESS = MAX(DT_ADPAY) 
FROM PU_ADPAYMENT 
WHERE CD_COMPANY = @P_CD_COMPANY 
AND NO_ADPAY = @P_NO_ADPAY 

-- 전표번호 채번                                                         
EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT 

-- 회계번호 채번
SET @P_NO_ACCT = 0
SET @P_ID_ACCT = NULL

IF @P_CD_COMPANY = 'K100'
BEGIN
	SET @V_CD_BANK = '08358'
	SET @V_NM_BANK = '기업은행'
	SET @V_CD_DEPOSIT = '20001'
	SET @V_NO_DEPOSIT = '092-073109-01-012'
END
ELSE IF @P_CD_COMPANY = 'K200'
BEGIN
	SET @V_CD_BANK = '08358'
	SET @V_NM_BANK = '기업은행'
	SET @V_CD_DEPOSIT = '70001'
	SET @V_NO_DEPOSIT = '092-090795-01-012'
END
ELSE IF @P_CD_COMPANY = 'S100'
BEGIN
	IF EXISTS (SELECT 1 
			   FROM PU_ADPAYMENT
			   WHERE CD_COMPANY = @P_CD_COMPANY
			   AND NO_ADPAY = @P_NO_ADPAY
			   AND CD_EXCH = 'SGD')
    BEGIN
		SET @V_CD_BANK = '13000'
		SET @V_NM_BANK = 'OCBC'
		SET @V_CD_DEPOSIT = '10001'
		SET @V_NO_DEPOSIT = '686385626001'	
	END
	ELSE
	BEGIN
		SET @V_CD_BANK = '13000'
		SET @V_NM_BANK = 'OCBC'
		SET @V_CD_DEPOSIT = '10000'
		SET @V_NO_DEPOSIT = '503208001301'
	END
END
ELSE
BEGIN
	SET @V_CD_BANK = NULL
	SET @V_NM_BANK = NULL
	SET @V_CD_DEPOSIT = NULL
	SET @V_NO_DEPOSIT = NULL
END

DECLARE @P_NO_DOLINE INT 

SET @P_NO_DOLINE = 0 

OPEN CUR_PU_ADPAYMENT 

FETCH NEXT FROM CUR_PU_ADPAYMENT INTO @IN_CD_COMPANY,
									  @IN_NO_MDOCU, 
									  @IN_CD_PARTNER,
									  @IN_CD_CC,
									  @IN_NM_CC,
									  @IN_CD_PJT,
									  @IN_ID_WRITE, 
									  @IN_CD_BIZAREA,
									  @IN_TP_TAX,
									  @IN_FG_TRANS,
									  @IN_CD_WDEPT,
									  @IN_DT_ACCT, 
									  @IN_CD_PC,
									  @IN_CD_ACCT,
									  @IN_AM_DR,
									  @IN_AM_CR,
									  @IN_AM_SUPPLY,
									  @IN_TP_DRCR, 
									  @IN_CD_EXCH,
									  @IN_RT_EXCH,
									  @IN_CD_EMPLOY,
									  @IN_AM_EXDO,
									  @IN_NM_BIZAREA, 
									  @IN_NM_KOR,
									  @IN_LN_PARTNER,
									  @IN_NM_PROJECT,
									  @IN_NM_DEPT,
									  @IN_TYPE, 
									  @IN_CD_RELATION,
									  @NO_BIZAREA,
									  @NM_TAX,
									  @IN_NO_COMPANY,
									  @IN_TP_ACAREA, 
									  @CD_DOCU,
									  @DT_PAY_PREARRANGED,
									  @CD_FUND,
									  @CD_BGACCT,
									  @CD_BUDGET,
									  @P_NM_TP, 
									  @FG_PAYBILL,
									  @NO_LC,
									  @DT_DUE,
									  @P_YN_ISS,
									  @V_VAT_TAX,
									  @P_PO_CONDITION, 
									  @P_NM_CONDITION 

WHILE @@FETCH_STATUS = 0 
BEGIN 
SET @P_NO_DOLINE = @P_NO_DOLINE + 1 

/* 계정과목이 등재되어 있는지 체크하는 구문 */ 
IF( @IN_CD_ACCT IS NULL OR @IN_CD_ACCT = '' ) 
BEGIN 
    SELECT @ERRNO = 100000, 
	     @ERRMSG = '계정과목이 누락되었습니다. 확인하십시요'
	GOTO ERROR 
END 

--EXEC UP_PU_IV_MNG_TRANS_DOCU '0327', 'PTX20121100001', '210', '001'                                     
IF @IN_CD_RELATION <> '10' 
  SET @V_IN_NO_MDOCU = @IN_NO_MDOCU -- 연동 항목이 '일반' 이 아니면 NO_MDOCU값 넣어준다.                                                      
ELSE 
  SET @V_IN_NO_MDOCU = NULL 

IF( @V_SERVER_KEY = 'TRIGEM' ) 
BEGIN 
    SELECT @P_NOTE = @IN_LN_PARTNER + '_' + @P_NO_PO + '_' + '선급금' 
END 
ELSE 
BEGIN 
    SELECT @P_NOTE = @IN_LN_PARTNER + ' ' + @P_NM_TP 
END 

--IF @IN_TYPE = 'A' 
--BEGIN 
--    IF @IN_TP_TAX IN ( '22', '50' ) -- 불공제일때    
--    BEGIN 
--        SET @IN_AM_DR = @IN_AM_DR + @V_VAT_TAX 
--    END 
--END 
--ELSE IF @IN_TYPE = 'V' -- 부가세일때   
--BEGIN 
--    PRINT @IN_TYPE 

--    IF @IN_TP_TAX IN ( '22', '50' ) -- 불공제일때    
--    BEGIN 
--        SET @IN_AM_DR = 0 
--    END 
--END 

SET @NM_EXCH = (SELECT (CASE @P_LANGUAGE WHEN 'KR' THEN NM_SYSDEF
						                 WHEN 'US' THEN NM_SYSDEF_E
						                 WHEN 'JP' THEN NM_SYSDEF_JP
						                 WHEN 'CH' THEN NM_SYSDEF_CH END)
				FROM MA_CODEDTL
				WHERE CD_COMPANY = @P_CD_COMPANY
				AND CD_FIELD = 'MA_B000005'
				AND CD_SYSDEF = @IN_CD_EXCH)

-- 증빙 추가
SET @V_TP_EVIDENCE = (SELECT TP_EVIDENCE
					  FROM CZ_FI_ACCT_EVIDENCEL
					  WHERE CD_COMPANY = @IN_CD_COMPANY
					  AND ISNULL(FG_TAX, '') = @IN_TP_TAX
					  AND CD_ACCT = @IN_CD_ACCT)

EXEC UP_FI_AUTODOCU_1 @P_NO_DOCU, -- 전표번호                                                         
					  @P_NO_DOLINE, -- 라인번호                                                         
					  @IN_CD_PC,-- 회계단위                                         
					  @IN_CD_COMPANY, -- 회사코드                                                                   
					  @IN_CD_WDEPT, -- 작성부서                                                         
					  @IN_ID_WRITE, -- 작성자                                                         
					  @IN_DT_ACCT, -- 매출일자 = 회계일자 = 처리일자                                                 
					  @P_NO_ACCT, -- 회계번호 미결이니까 NO_ACCT                            
					  '3', -- 전표구분-대체 TP_DOCU                                                         
					  --'45',                                        -- 전표유형-일반 CD_DOCU    11->45 20080624                                                     
					  @CD_DOCU, --전표유형 (2009.11.28  전표유형 추가 SMR (REQ KHS))           
					  @V_ST_DOCU, -- 전표상태-미결 ST_DOCU                                                         
					  @P_ID_ACCT,-- 승인자      
					  @IN_TP_DRCR, -- 차대구분 TP_DRCR                                                         
					  @IN_CD_ACCT, -- 계정코드                                                         
					  @P_NOTE, -- 적요                                                         
					  @IN_AM_DR, -- 차변금액 AM_DR                                                         
					  @IN_AM_CR,-- 대변금액 AM_CR                         
					  @IN_TP_ACAREA, -- 본지점구분-안함 TP_ACAREA                                                         
					  @IN_CD_RELATION, -- 연동항목-일반 CD_RELATION                                                           
					  @CD_BUDGET, -- 예산코드 CD_BUDGET                                                                   
					  @CD_FUND, -- 자금과목 CD_FUND                                                      
					  NULL, -- 원인전표번호 NO_BDOCU                                                                   
					  NULL, -- 원인전표라인 NO_BDOLINE                                                                   
					  '0', -- 타대구분 TP_ETCACCT                                                     
					  @IN_CD_BIZAREA, -- 귀속사업장                                                       
					  @IN_NM_BIZAREA, 
					  @IN_CD_CC, -- 코스트센터                                                         
					  @IN_NM_CC, 
					  @IN_CD_PJT, -- 프로젝트                                                       
					  @IN_NM_PROJECT, 
					  @IN_CD_WDEPT,-- 부서             
					  @IN_NM_DEPT, 
					  @IN_CD_EMPLOY, -- 사원 CD_EMPLOY                                                         
					  @IN_NM_KOR, 
					  @IN_CD_PARTNER, -- 매입처 CD_PARTNER                                                         
					  @IN_LN_PARTNER, 
					  @V_CD_DEPOSIT, -- 예적금코드 CD_DEPOSIT                                                        
					  @V_NO_DEPOSIT, -- NM_DEPOSIT,                                                      
					  NULL, -- 카드번호 CD_CARD                                               
					  NULL, -- NM_CARD                                                         
					  @V_CD_BANK, -- 은행코드 CD_BANK  : MA_PARTNER에서 FG_PARTNER = '002' 인것                                                         
					  @V_NM_BANK, -- NM_BANK,                                                     
					  NULL, -- 품목코드 NO_ITEM                                                           
					  NULL,-- NM_ITEM,                                                     
					  @IN_TP_TAX, -- 세무구분 TP_TAX                                                        
					  @NM_TAX, 
					  NULL, -- 거래구분 CD_TRADE                                                          
					  NULL,-- NM_TRADE                                                     
					  @IN_CD_EXCH, -- 환종        CD_EXCH                                       
					  @NM_EXCH, -- NM_EXCH,                                                     
					  NULL, -- CD_UMNG1                                                           
					  @V_CD_UMNG2, -- CD_UMNG2                                                           
					  NULL, -- CD_UMNG3                                                           
					  NULL,-- CD_UMNG4                   
					  NULL, -- CD_UMNG5                                                       
					  @IN_NO_COMPANY, -- NO_RES                                                     
					  @IN_AM_SUPPLY, --AM_SUPPLY                                                     
					  @V_IN_NO_MDOCU, -- CD_MNG                                                     
					  --       @IN_DT_ACCT,      -- 거래일자, 시작일자, 발생일자 DT_START                                                           
					  @DT_PAY_PREARRANGED, --ISNULL(@DT_PAY_PREARRANGED,@P_DT_PROCESS),    -- 자금예정일(지급예정일 2009.11.30 SMR)                                         
					  @DT_DUE, -- 만기일자 DT_END                                                                   
					  @IN_RT_EXCH, -- 환율        RT_EXCH                                                         
					  @IN_AM_EXDO, -- 외화금액 AM_EXDO                                                          
					  @P_NO_MODULE, -- 모듈구분(매출:002) NO_MODULE                                                          
					  @IN_NO_MDOCU, -- 모듈관리번호 = 타모듈pkey NO_MDOCU                                                           
					  @CD_BGACCT, -- 지출결의코드 CD_EPNOTE  (지출결의에 예산계정코드를 입력해야 화면에 예산계정명을 볼 수 있다/지출결의와 함께 쓰기때문이란다)                           
					  @IN_ID_WRITE, -- 전표처리자                                                         
					  @CD_BGACCT, -- 예산계정 CD_BGACCT                                                          
					  NULL, -- 결의구분 TP_EPNOTE                                                          
					  NULL, -- 품의내역 NM_PUMM                                                           
					  @CUR_DATE, -- 현재일자로 20100506 @P_DT_PROCESS,    -- 작성일자 DT_WRITE                                                           
					  0,-- AM_ACTSUM                                    
					  0,-- AM_JSUM                                                      
					  'N', -- YN_GWARE                                                           
					  NULL, -- 사업계획코드 CD_BIZPLAN                                                       
					  NULL,--CD_ETC                                                       
					  @P_ERRORCODE, 
					  @P_ERRORMSG, 
					  NULL, 
					  @NO_LC, 
					  @FG_PAYBILL,-- 지급조건                       
					  NULL,--NM_ARRIVE 
					  NULL,--CD_PAYDEDUCT 
					  NULL,--JUMIN_NO 
					  NULL,--DT_PAY 
					  NULL,--NO_INV 
					  NULL,--NO_TO 
					  @V_VAT_TAX,--P_VAT_TAX 
					  @P_PO_CONDITION, 
					  @P_NM_CONDITION, 
					  @P_NO_PO,
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

IF ( @@ERROR <> 0 ) 
BEGIN 
    SELECT @ERRNO = 100000 
           ,@ERRMSG = 'CM_M100010' 
    GOTO ERROR 
END 

--IF @IN_TYPE = 'V'      --추가 :김정근 20071010 부가세 추가로직                                                       
-- BEGIN                                                     
--  EXEC UP_FI_AUTODOCU_TAX @IN_CD_COMPANY, @IN_CD_PC, @P_NO_DOCU, @P_NO_DOLINE, @IN_AM_SUPPLY, @P_YN_ISS, @V_VAT_TAX                                                     
-- IF (@@ERROR <> 0 )                                                       
-- BEGIN                                                         
--    SELECT @ERRNO  = 100000,                                                         
--     @ERRMSG = 'CM_M100010'                                                         
--    GOTO ERROR                                    
-- END     
-- END                                                         
--IF (@P_NO_MODULE = '210') -- 회계전표유형 : 국내매입 이면                                                   
--   BEGIN                                                          
--     UPDATE PU_IVH SET TP_AIS = 'Y' WHERE CD_COMPANY = @IN_CD_COMPANY AND NO_IV = @IN_NO_MDOCU                                                         
--  IF (@@ERROR <> 0 )                                                       
--  BEGIN                                                         
--     SELECT @ERRNO  = 100000,                                                         
--      @ERRMSG = 'CM_M100010'                                                         
--     GOTO ERROR                                                         
--  END                                                 
--   END                                                          
FETCH NEXT FROM CUR_PU_ADPAYMENT INTO @IN_CD_COMPANY,
									  @IN_NO_MDOCU, 
									  @IN_CD_PARTNER,
									  @IN_CD_CC,
									  @IN_NM_CC,
									  @IN_CD_PJT,
									  @IN_ID_WRITE, 
									  @IN_CD_BIZAREA,
									  @IN_TP_TAX,
									  @IN_FG_TRANS,
									  @IN_CD_WDEPT,
									  @IN_DT_ACCT, 
									  @IN_CD_PC,
									  @IN_CD_ACCT,
									  @IN_AM_DR,
									  @IN_AM_CR,
									  @IN_AM_SUPPLY,
									  @IN_TP_DRCR, 
									  @IN_CD_EXCH,
									  @IN_RT_EXCH,
									  @IN_CD_EMPLOY,
									  @IN_AM_EXDO,
									  @IN_NM_BIZAREA, 
									  @IN_NM_KOR,
									  @IN_LN_PARTNER,
									  @IN_NM_PROJECT,
									  @IN_NM_DEPT,
									  @IN_TYPE, 
									  @IN_CD_RELATION,
									  @NO_BIZAREA,
									  @NM_TAX,
									  @IN_NO_COMPANY,
									  @IN_TP_ACAREA, 
									  @CD_DOCU,
									  @DT_PAY_PREARRANGED,
									  @CD_FUND,
									  @CD_BGACCT,
									  @CD_BUDGET,
									  @P_NM_TP, 
									  @FG_PAYBILL,
									  @NO_LC,
									  @DT_DUE,
									  @P_YN_ISS,
									  @V_VAT_TAX,
									  @P_PO_CONDITION, 
									  @P_NM_CONDITION 
END 

CLOSE CUR_PU_ADPAYMENT 

DEALLOCATE CUR_PU_ADPAYMENT 

RETURN 

ERROR: 

RAISERROR (@ERRMSG,18,1)   

GO

