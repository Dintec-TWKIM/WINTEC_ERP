
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_PU_IV_MNG_TRANS_DOCU') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_PU_IV_MNG_TRANS_DOCU
GO


/**********************************************************************************************************    
**  System : 구매관리        
**  Sub System : 매입관리        
**  Page  : 등록        
**  Desc  :  미결전표처리        
**  참  고 :         
**  Return Values        
**        
**  작    성    자  : 
**  작    성    일  :   2008.02.05        
**  수    정    자  :  오성영,  김헌섭               
**  수   정 내  용	 :  UP_FI_AUTODOCU -> UP_FI_AUTODOCU_1  변환작업 및 관리항목 
 *********************************************************************************************************/       
CREATE PROCEDURE UP_PU_IV_MNG_TRANS_DOCU    
(    
	@P_CD_COMPANY        NVARCHAR(7),    
	@P_NO_IV             NVARCHAR(20),    
	@P_NO_MODULE         NVARCHAR(3)                -- /* 회계전표유형 : 국내매입(210) , 구매/외주(001) */    
)    
AS    
 
DECLARE @IN_CD_COMPANY        NVARCHAR(7)    
DECLARE @IN_NO_MDOCU        NVARCHAR(20)    
DECLARE @IN_CD_PARTNER        NVARCHAR(7)    
DECLARE @IN_NO_COMPANY        NVARCHAR(20)    
DECLARE @IN_CD_CC                NVARCHAR(7)    
DECLARE @IN_CD_PJT                NVARCHAR(20)    
DECLARE @IN_ID_WRITE        NVARCHAR(10)    
DECLARE @IN_CD_BIZAREA    NVARCHAR(7)    
DECLARE @NO_BIZAREA  NVARCHAR(20)   
DECLARE @IN_TP_TAX            NCHAR(3)    
DECLARE @NM_TAX			NCHAR(15)    
DECLARE @IN_FG_TRANS        NCHAR(3)    
DECLARE @IN_CD_WDEPT        NVARCHAR(12)    
DECLARE @IN_DT_ACCT                NCHAR(8)    
DECLARE @IN_CD_PC                NVARCHAR(7)    
DECLARE @IN_CD_ACCT                NVARCHAR(10)    
DECLARE @IN_AM_DR                NUMERIC(19,4)    
DECLARE @IN_AM_CR                NUMERIC(19,4)    
DECLARE @IN_AM_SUPPLY            NUMERIC(19,4)    
DECLARE @IN_TP_DRCR                NCHAR(1)    
DECLARE @IN_CD_EXCH                NVARCHAR(3)    
DECLARE @IN_RT_EXCH                NUMERIC(17,4)    
DECLARE @IN_CD_EMPLOY        NVARCHAR(10)    
DECLARE @IN_AM_EXDO                NUMERIC(19,4)    
DECLARE @IN_NM_BIZAREA        NVARCHAR(50)    
DECLARE @IN_NM_CC                NVARCHAR(50)    
DECLARE @IN_NM_KOR                NVARCHAR(50)    
DECLARE @IN_LN_PARTNER        NVARCHAR(50)    
DECLARE @IN_NM_PROJECT        NVARCHAR(50)    
DECLARE @IN_NM_DEPT                NVARCHAR(50)    
DECLARE @IN_TYPE                NCHAR(1)    
DECLARE @IN_CD_RELATION   NCHAR(3)

DECLARE @P_ERRORCODE  NCHAR(10)  
DECLARE @P_ERRORMSG NVARCHAR(300)     
  
  
    
IF @P_NO_MODULE = '001'        -- 회계전표유형 : 구매/외주이면  -----------------------------------------------------------------------------------------------------     
  
DECLARE CUR_PU_IV_MNG CURSOR FOR    
  
 SELECT     
	H.CD_COMPANY,    
	L.NO_IV NO_MDOCU,    
	H.CD_PARTNER,    
	G.CD_CC,    
	L.CD_PJT,    
	H.NO_EMP ID_WRITE,    
	H.CD_BIZAREA,     
	H.FG_TAX TP_TAX,    
	H.FG_TRANS,    
	H.CD_DEPT CD_WDEPT,    
	H.DT_PROCESS DT_ACCT,    
	B.CD_PC,    
	A.CD_ACCT,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END ) AS AM_DR,    
	0 AS AM_CR,    
	0 AM_SUPPLY,            
	'1' AS TP_DRCR,    
	L.CD_EXCH,    
	L.RT_EXCH,    
	L.NO_EMP CD_EMPLOY,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_EX_CLS ELSE L.AM_EX_CLS END ) AS AM_EXDO,    
	B.NM_BIZAREA,    
	C.NM_CC,    
	E.NM_KOR,    
	P.LN_PARTNER,    
	'' NM_PROJECT,    
	D.NM_DEPT,    
	'A' AS TYPE,  
	'10' CD_RELATION, --연동항목:일반    
        B.NO_BIZAREA,
        (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,
	P.NO_COMPANY
  
 FROM   
	PU_IVH H   
	   INNER JOIN PU_IVL L ON H.NO_IV  = L.NO_IV  AND H.CD_COMPANY = L.CD_COMPANY    
	   INNER JOIN MA_PITEM M ON L.CD_ITEM = M.CD_ITEM  AND L.CD_PLANT = M.CD_PLANT AND L.CD_COMPANY = M.CD_COMPANY    
	   INNER JOIN MA_AISPOSTL A ON L.CD_COMPANY = A.CD_COMPANY AND L.FG_TPPURCHASE = A.CD_TP     
	   INNER JOIN MA_PURGRP G ON L.CD_PURGRP = G.CD_PURGRP AND L.CD_COMPANY = G.CD_COMPANY    
	   INNER JOIN MA_BIZAREA B ON H.CD_BIZAREA = B.CD_BIZAREA AND H.CD_COMPANY = B.CD_COMPANY     
	   LEFT OUTER JOIN MA_CC C ON G.CD_CC  = C.CD_CC AND G.CD_COMPANY = C.CD_COMPANY    
	   LEFT OUTER JOIN MA_EMP E ON H.NO_EMP = E.NO_EMP AND H.CD_COMPANY = E.CD_COMPANY    
	   LEFT OUTER JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER AND H.CD_COMPANY = P.CD_COMPANY    
	   LEFT OUTER JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT AND H.CD_COMPANY = D.CD_COMPANY            
 WHERE   
	  H.NO_IV = @P_NO_IV  AND   
	  H.CD_COMPANY = @P_CD_COMPANY AND   
	  H.TP_AIS ='N'  AND  -- 전표발생여부  
	  H.YN_PURSUB IN('Y','O') AND -- 외주    
	  A.FG_TP ='200' AND -- 매입형태    
	  A.FG_AIS = '214'    
 GROUP BY   
	  H.CD_COMPANY ,L.NO_IV, H.CD_PARTNER, G.CD_CC, L.CD_PJT , H.NO_EMP , H.CD_BIZAREA,     
	  H.FG_TAX, H.FG_TRANS,H.CD_DEPT, H.DT_PROCESS, B.CD_PC, A.CD_ACCT,L.CD_EXCH, L.RT_EXCH,     
	  L.NO_EMP,B.NM_BIZAREA, C.NM_CC, E.NM_KOR, P.LN_PARTNER, D.NM_DEPT, B.NO_BIZAREA, P.NO_COMPANY   

  
-- 부가세계정 : 차변: 부가세     
UNION ALL    

 SELECT   
	H.CD_COMPANY ,L.NO_IV AS NO_MDOCU, H.CD_PARTNER, '' CD_CC, '' CD_PJT , H.NO_EMP  AS ID_WRITE, H.CD_BIZAREA,     
	H.FG_TAX AS TP_TAX, H.FG_TRANS,H.CD_DEPT AS CD_WDEPT, H.DT_PROCESS AS DT_ACCT, B.CD_PC, A.CD_ACCT,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS  AM_DR,    
	0 AS AM_CR,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END ) AS  AM_SUPPLY,    
	'1' AS TP_DRCR  ,'000' AS CD_EXCH, 1 AS RT_EXCH, '' AS CD_EMPLOY,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS AM_EXDO,    
	B.NM_BIZAREA, '' AS NM_CC, E.NM_KOR, P.LN_PARTNER,'' AS NM_PROJECT, D.NM_DEPT,'V' AS TYPE, '30' CD_RELATION, --연동항목:부가세예수금     
        B.NO_BIZAREA,
        (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,
	P.NO_COMPANY         
 FROM   
	  PU_IVH H   
	   INNER JOIN PU_IVL L ON H.NO_IV  = L.NO_IV AND H.CD_COMPANY = L.CD_COMPANY      
	   INNER JOIN MA_PITEM M ON L.CD_ITEM = M.CD_ITEM AND L.CD_PLANT = M.CD_PLANT AND L.CD_COMPANY = M.CD_COMPANY    
	   INNER JOIN MA_AISPOSTL A ON L.CD_COMPANY = A.CD_COMPANY AND L.FG_TPPURCHASE = A.CD_TP     
	   INNER JOIN MA_BIZAREA B ON H.CD_BIZAREA = B.CD_BIZAREA AND H.CD_COMPANY = B.CD_COMPANY     
	   LEFT OUTER JOIN MA_EMP E ON H.NO_EMP = E.NO_EMP  AND H.CD_COMPANY = E.CD_COMPANY    
	   LEFT OUTER JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER AND H.CD_COMPANY = P.CD_COMPANY         
	   LEFT OUTER JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT AND H.CD_COMPANY = D.CD_COMPANY    
 WHERE  
	H.NO_IV = @P_NO_IV AND    
	H.CD_COMPANY = @P_CD_COMPANY AND    
	H.TP_AIS ='N' AND  -- 전표발생여부    
	H.YN_PURSUB IN('Y','O') AND  -- 외주    
	A.FG_TP ='200' AND  -- 매입형태    
	A.FG_AIS = '210' --부가세            
 GROUP BY   
	H.CD_COMPANY ,L.NO_IV, H.CD_PARTNER, H.NO_EMP , H.CD_BIZAREA,     
	H.FG_TAX, H.FG_TRANS,H.CD_DEPT, H.DT_PROCESS, B.CD_PC, A.CD_ACCT,    
	B.NM_BIZAREA,  E.NM_KOR, P.LN_PARTNER, D.NM_DEPT, B.NO_BIZAREA, P.NO_COMPANY          
     
-- 채무계정(외상매입금) : 대변: 원화금액+부가세     
UNION ALL    
 SELECT   
	H.CD_COMPANY ,L.NO_IV AS NO_MDOCU, H.CD_PARTNER, G.CD_CC, L.CD_PJT , H.NO_EMP  AS ID_WRITE, H.CD_BIZAREA,     
	H.FG_TAX AS TP_TAX, H.FG_TRANS,H.CD_DEPT AS CD_WDEPT, H.DT_PROCESS AS DT_ACCT, B.CD_PC,A.CD_ACCT,    
	0 AS AM_DR,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END )  +     
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS AM_CR,    
	0 AM_SUPPLY,            
	'2' AS TP_DRCR , L.CD_EXCH, L.RT_EXCH, L.NO_EMP AS CD_EMPLOY,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_EX_CLS ELSE L.AM_EX_CLS END ) AS AM_EXDO,    
	B.NM_BIZAREA, C.NM_CC, E.NM_KOR, P.LN_PARTNER,'' NM_PROJECT, D.NM_DEPT ,'T' AS TYPE, '10' CD_RELATION, --연동항목:일반    
        B.NO_BIZAREA,
        (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,
	P.NO_COMPANY         
 FROM   
	  PU_IVH H   
	   INNER JOIN PU_IVL L ON H.NO_IV  = L.NO_IV  AND H.CD_COMPANY = L.CD_COMPANY    
	   INNER JOIN MA_PITEM M ON L.CD_ITEM = M.CD_ITEM  AND L.CD_PLANT = M.CD_PLANT AND L.CD_COMPANY = M.CD_COMPANY    
	   INNER JOIN MA_AISPOSTL A ON L.CD_COMPANY = A.CD_COMPANY AND L.FG_TPPURCHASE = A.CD_TP    
	   INNER JOIN MA_PURGRP G ON L.CD_PURGRP = G.CD_PURGRP  AND L.CD_COMPANY = G.CD_COMPANY    
	   INNER JOIN MA_BIZAREA B ON H.CD_BIZAREA = B.CD_BIZAREA AND H.CD_COMPANY = B.CD_COMPANY     
	   LEFT OUTER JOIN MA_CC C ON G.CD_CC = C.CD_CC AND G.CD_COMPANY = C.CD_COMPANY    
	   LEFT OUTER JOIN MA_EMP E ON H.NO_EMP = E.NO_EMP AND H.CD_COMPANY = E.CD_COMPANY    
	   LEFT OUTER JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER AND H.CD_COMPANY = P.CD_COMPANY    
	   LEFT OUTER JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT AND H.CD_COMPANY = D.CD_COMPANY     
 WHERE   
	H.NO_IV = @P_NO_IV AND    
	H.CD_COMPANY = @P_CD_COMPANY AND    
	H.TP_AIS ='N' AND   -- 전표발생여부    
	H.YN_PURSUB IN('Y','O') AND  -- 외주    
	A.FG_TP ='200' AND  -- 매입형태    
	A.FG_AIS = '215'    
 GROUP BY   
	H.CD_COMPANY ,L.NO_IV, H.CD_PARTNER, G.CD_CC, L.CD_PJT , H.NO_EMP , H.CD_BIZAREA,     
	H.FG_TAX, H.FG_TRANS,H.CD_DEPT, H.DT_PROCESS, B.CD_PC, A.CD_ACCT,L.CD_EXCH, L.RT_EXCH,     
	L.NO_EMP,B.NM_BIZAREA, C.NM_CC, E.NM_KOR, P.LN_PARTNER,D.NM_DEPT, B.NO_BIZAREA, P.NO_COMPANY          
     
    
ELSE        -- 구매이면  ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------    
  
DECLARE CUR_PU_IV_MNG CURSOR FOR    
  
SELECT   
	H.CD_COMPANY ,L.NO_IV AS NO_MDOCU, H.CD_PARTNER, G.CD_CC, L.CD_PJT , H.NO_EMP AS ID_WRITE, H.CD_BIZAREA,     
	H.FG_TAX AS TP_TAX, H.FG_TRANS,H.CD_DEPT AS CD_WDEPT, H.DT_PROCESS AS DT_ACCT, B.CD_PC,A.CD_ACCT,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END ) AS AM_DR,    
	0 AS AM_CR,  0 AM_SUPPLY,            
	'1' AS TP_DRCR , L.CD_EXCH, L.RT_EXCH, L.NO_EMP AS CD_EMPLOY,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_EX_CLS ELSE L.AM_EX_CLS END ) AS AM_EXDO,    
	B.NM_BIZAREA, C.NM_CC, E.NM_KOR, P.LN_PARTNER,
	 (SELECT TOP 1 NM_PROJECT FROM SA_PROJECTH  WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)  AS NM_PROJECT,-- PJ.NM_PROJECT,
	D.NM_DEPT, 'A' AS TYPE, '10' CD_RELATION, --연동항목:일반   
        B.NO_BIZAREA,
        (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,
	P.NO_COMPANY         
FROM   
	PU_IVH H   
		INNER JOIN PU_IVL L ON H.NO_IV  = L.NO_IV  AND H.CD_COMPANY = L.CD_COMPANY    
		INNER JOIN MA_PITEM M ON L.CD_ITEM = M.CD_ITEM  AND L.CD_PLANT = M.CD_PLANT  AND L.CD_COMPANY = M.CD_COMPANY    
		INNER JOIN MA_AISPOSTL A ON L.CD_COMPANY = A.CD_COMPANY  AND L.FG_TPPURCHASE = A.CD_TP  AND A.FG_TP ='200' -- 매입형태    
		INNER JOIN MA_PURGRP G ON L.CD_PURGRP = G.CD_PURGRP  AND L.CD_COMPANY = G.CD_COMPANY    
		INNER JOIN MA_BIZAREA B ON H.CD_BIZAREA = B.CD_BIZAREA  AND H.CD_COMPANY = B.CD_COMPANY     
		LEFT OUTER JOIN MA_CC C ON G.CD_CC = C.CD_CC  AND G.CD_COMPANY = C.CD_COMPANY    
		LEFT OUTER JOIN MA_EMP E ON H.NO_EMP = E.NO_EMP AND H.CD_COMPANY = E.CD_COMPANY    
		LEFT OUTER JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER  AND H.CD_COMPANY = P.CD_COMPANY    
		LEFT OUTER JOIN SA_PROJECTH PJ ON L.CD_COMPANY = PJ.CD_COMPANY  AND L.CD_PJT = PJ.NO_PROJECT    
		LEFT OUTER JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT  AND H.CD_COMPANY = D.CD_COMPANY    
WHERE   
	H.NO_IV = @P_NO_IV    
	AND H.CD_COMPANY = @P_CD_COMPANY    
	AND H.TP_AIS ='N' -- 전표발생여부    
	AND H.YN_PURSUB ='N' -- 구매    
	AND A.FG_AIS =  (CASE M.CLS_ITEM WHEN '001' THEN '201' WHEN '002' THEN '202' WHEN '003' THEN '205' WHEN '004' THEN '204'     
	WHEN '005' THEN '203' WHEN '006' THEN '209' WHEN '007' THEN '207' WHEN '008' THEN '208'  ELSE '208' END )     
	--AND PJ.NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)    
GROUP BY   
	H.CD_COMPANY ,L.NO_IV, H.CD_PARTNER, G.CD_CC, L.CD_PJT , H.NO_EMP , H.CD_BIZAREA,     
	H.FG_TAX, H.FG_TRANS,H.CD_DEPT, H.DT_PROCESS, B.CD_PC, A.CD_ACCT,L.CD_EXCH, L.RT_EXCH,     
	L.NO_EMP,B.NM_BIZAREA, C.NM_CC, E.NM_KOR, P.LN_PARTNER,PJ.NM_PROJECT, D.NM_DEPT, L.CD_COMPANY, L.CD_PJT, B.NO_BIZAREA, P.NO_COMPANY       

  
-- 부가세계정 : 차변: 부가세     
UNION ALL    
  
SELECT   
	H.CD_COMPANY ,L.NO_IV AS NO_MDOCU, H.CD_PARTNER, '' CD_CC, '' CD_PJT , H.NO_EMP  AS ID_WRITE, H.CD_BIZAREA,     
	H.FG_TAX AS TP_TAX, H.FG_TRANS,H.CD_DEPT AS CD_WDEPT, H.DT_PROCESS AS DT_ACCT, B.CD_PC, A.CD_ACCT,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS  AM_DR,    
	0 AS AM_CR,     
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END ) AS  AM_SUPPLY,    
	'1' AS TP_DRCR  ,'000' AS CD_EXCH, 1 AS RT_EXCH, '' AS CD_EMPLOY,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS AM_EXDO,    
	B.NM_BIZAREA, '' AS NM_CC, E.NM_KOR, P.LN_PARTNER, '' AS NM_PROJECT, D.NM_DEPT,'V' AS TYPE, '30' CD_RELATION, --연동항목:일반     
        B.NO_BIZAREA,
        (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,
	P.NO_COMPANY         
FROM   
	PU_IVH H   
		INNER JOIN PU_IVL L ON H.NO_IV  = L.NO_IV AND H.CD_COMPANY = L.CD_COMPANY     
		INNER JOIN MA_PITEM M ON L.CD_ITEM = M.CD_ITEM  AND L.CD_PLANT = M.CD_PLANT  AND L.CD_COMPANY = M.CD_COMPANY    
		INNER JOIN MA_AISPOSTL A ON L.CD_COMPANY = A.CD_COMPANY  AND A.FG_TP ='200'  AND L.FG_TPPURCHASE = A.CD_TP     
		INNER JOIN MA_BIZAREA B ON H.CD_BIZAREA = B.CD_BIZAREA  AND H.CD_COMPANY = B.CD_COMPANY     
		LEFT OUTER JOIN MA_EMP E ON H.NO_EMP = E.NO_EMP AND H.CD_COMPANY = E.CD_COMPANY    
		LEFT OUTER JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER  AND H.CD_COMPANY = P.CD_COMPANY            
		LEFT OUTER JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT  AND H.CD_COMPANY = D.CD_COMPANY    
WHERE   
	H.NO_IV = @P_NO_IV    
	AND H.CD_COMPANY = @P_CD_COMPANY    
	AND H.TP_AIS ='N'  -- 전표발생여부    
	AND H.YN_PURSUB ='N' --구매      
	AND A.FG_AIS = '210' --부가세            
GROUP BY   
	H.CD_COMPANY ,L.NO_IV, H.CD_PARTNER, H.NO_EMP , H.CD_BIZAREA,     
	H.FG_TAX, H.FG_TRANS,H.CD_DEPT, H.DT_PROCESS, B.CD_PC, A.CD_ACCT,    
	B.NM_BIZAREA,  E.NM_KOR, P.LN_PARTNER, D.NM_DEPT, B.NO_BIZAREA, P.NO_COMPANY         
    
-- 채무계정(외상매입금) : 대변: 원화금액+부가세     
UNION ALL   


SELECT   
	H.CD_COMPANY ,L.NO_IV AS NO_MDOCU, H.CD_PARTNER, G.CD_CC, L.CD_PJT , H.NO_EMP  AS ID_WRITE, H.CD_BIZAREA,     
	H.FG_TAX AS TP_TAX, H.FG_TRANS,H.CD_DEPT AS CD_WDEPT, H.DT_PROCESS AS DT_ACCT, B.CD_PC,A.CD_ACCT,    
	0 AS AM_DR,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END )  +     
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS AM_CR,      
	0 AM_SUPPLY,         
	'2' AS TP_DRCR , L.CD_EXCH, L.RT_EXCH, L.NO_EMP AS CD_EMPLOY,    
	SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_EX_CLS ELSE L.AM_EX_CLS END ) AS AM_EXDO,    
	B.NM_BIZAREA, C.NM_CC, E.NM_KOR, P.LN_PARTNER,
	 (SELECT TOP 1 NM_PROJECT FROM SA_PROJECTH  WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)  AS NM_PROJECT,-- PJ.NM_PROJECT, 
	D.NM_DEPT,'T' AS TYPE, '10' CD_RELATION, --연동항목:일반   
        B.NO_BIZAREA,
        (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,
	P.NO_COMPANY         
FROM   
	PU_IVH H   
		INNER JOIN PU_IVL L ON H.NO_IV  = L.NO_IV AND H.CD_COMPANY = L.CD_COMPANY    
		INNER JOIN MA_PITEM M  ON L.CD_ITEM  = M.CD_ITEM  AND L.CD_PLANT = M.CD_PLANT AND L.CD_COMPANY = M.CD_COMPANY    
		INNER JOIN MA_AISPOSTL A  ON L.FG_TPPURCHASE = A.CD_TP  AND L.CD_COMPANY = A.CD_COMPANY    
		INNER JOIN MA_PURGRP G ON L.CD_PURGRP = G.CD_PURGRP  AND L.CD_COMPANY = G.CD_COMPANY    
		INNER JOIN MA_BIZAREA B ON H.CD_BIZAREA = B.CD_BIZAREA  AND H.CD_COMPANY = B.CD_COMPANY     
		LEFT OUTER JOIN MA_CC C ON G.CD_CC = C.CD_CC  AND G.CD_COMPANY = C.CD_COMPANY    
		LEFT OUTER JOIN MA_EMP E ON H.NO_EMP = E.NO_EMP AND H.CD_COMPANY = E.CD_COMPANY    
		LEFT OUTER JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER  AND H.CD_COMPANY = P.CD_COMPANY    
		LEFT OUTER JOIN SA_PROJECTH PJ  ON L.CD_COMPANY = PJ.CD_COMPANY  AND L.CD_PJT = PJ.NO_PROJECT    
		LEFT OUTER JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT AND H.CD_COMPANY = D.CD_COMPANY    
WHERE   
	  H.NO_IV = @P_NO_IV    
	  AND H.CD_COMPANY = @P_CD_COMPANY    
	  AND H.TP_AIS ='N'  -- 전표발생여부    
	  AND H.YN_PURSUB ='N' -- 구매    
	  AND A.FG_TP ='200' -- 매입형태    
	  AND A.FG_AIS =  (CASE H.FG_TRANS WHEN '001' THEN '211' WHEN '002' THEN '212' WHEN '003' THEN '212'  ELSE '211' END )     
	  --AND PJ.NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)    
GROUP BY   
	  H.CD_COMPANY ,L.NO_IV, H.CD_PARTNER, G.CD_CC, L.CD_PJT , H.NO_EMP , H.CD_BIZAREA,     
	  H.FG_TAX, H.FG_TRANS,H.CD_DEPT, H.DT_PROCESS, B.CD_PC, A.CD_ACCT,L.CD_EXCH, L.RT_EXCH,     
	  L.NO_EMP,B.NM_BIZAREA, C.NM_CC, E.NM_KOR, P.LN_PARTNER,PJ.NM_PROJECT, D.NM_DEPT, L.CD_COMPANY, L.CD_PJT, B.NO_BIZAREA, P.NO_COMPANY         
  
------------------------------------------------------------------------------------------------------------------------------------------------  -----------------------------------------------------------------------------------------------------      

-- 여기서 부터 전표처리 하기 위한 부분  ---    
DECLARE @P_NO_DOCU NVARCHAR(20)        -- 전표번호    
DECLARE @P_DT_PROCESS NVARCHAR(8)    
    
-- 매출일자 알아오기    
SELECT @P_DT_PROCESS = DT_PROCESS    
FROM PU_IVH    
WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV    
    
-- 전표번호 채번    
EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT    
    
DECLARE @P_NO_DOLINE INT    
SET @P_NO_DOLINE = 0    
    
OPEN CUR_PU_IV_MNG    
    
FETCH NEXT FROM CUR_PU_IV_MNG INTO @IN_CD_COMPANY,@IN_NO_MDOCU,@IN_CD_PARTNER,@IN_CD_CC,@IN_CD_PJT,@IN_ID_WRITE,@IN_CD_BIZAREA,@IN_TP_TAX,@IN_FG_TRANS,@IN_CD_WDEPT,@IN_DT_ACCT,@IN_CD_PC,@IN_CD_ACCT,@IN_AM_DR,@IN_AM_CR,@IN_AM_SUPPLY,@IN_TP_DRCR,@IN_CD_EXCH
,@IN_RT_EXCH,  
@IN_CD_EMPLOY,@IN_AM_EXDO,@IN_NM_BIZAREA,@IN_NM_CC,@IN_NM_KOR,@IN_LN_PARTNER,@IN_NM_PROJECT,@IN_NM_DEPT,@IN_TYPE, @IN_CD_RELATION, @NO_BIZAREA, @NM_TAX, @IN_NO_COMPANY   
WHILE @@FETCH_STATUS = 0    
BEGIN    
SET @P_NO_DOLINE = @P_NO_DOLINE + 1    
    
EXEC UP_FI_AUTODOCU_1
	 @P_NO_DOCU,                -- 전표번호    
         @P_NO_DOLINE,        -- 라인번호    
         @IN_CD_PC,                        -- 회계단위    
         @IN_CD_COMPANY,                -- 회사코드              
         @IN_CD_WDEPT,                -- 작성부서    
         @IN_ID_WRITE,                -- 작성자    
         @IN_DT_ACCT,        -- 매출일자 = 회계일자 = 처리일자    
         0,                                -- 회계번호        미결이니까 NO_ACCT    
         '3',                        -- 전표구분-대체 TP_DOCU    
         '11',   -- 전표유형-일반 CD_DOCU    
         '1',                        -- 전표상태-미결 ST_DOCU    
         NULL,                        -- 승인자    
         @IN_TP_DRCR,        -- 차대구분        TP_DRCR    
         @IN_CD_ACCT,        -- 계정코드    
         NULL,                        -- 적요    
         @IN_AM_DR,                -- 차변금액 AM_DR    
         @IN_AM_CR,                -- 대변금액        AM_CR    
         0,                                -- 본지점구분-안함 TP_ACAREA    
         @IN_CD_RELATION,  -- 연동항목-일반 CD_RELATION      
         NULL,                        -- 예산코드 CD_BUDGET              
         NULL,                        -- 자금과목 CD_FUND 
         NULL,                        -- 원인전표번호 NO_BDOCU              
         NULL,                        -- 원인전표라인 NO_BDOLINE              
         '0',                        -- 타대구분 TP_ETCACCT              
         @IN_CD_BIZAREA,                -- 귀속사업장  
	 @IN_NM_BIZAREA,
         @IN_CD_CC,                        -- 코스트센터    
	 @IN_NM_CC,
         @IN_CD_PJT,                        -- 프로젝트  
	 @IN_NM_PROJECT,  
         @IN_CD_WDEPT,                -- 부서 
	 @IN_NM_DEPT,   
         @IN_CD_EMPLOY,                        -- 사원 CD_EMPLOY    
	 @IN_NM_KOR,
         @IN_CD_PARTNER,        -- 거래처 CD_PARTNER    
	 @IN_LN_PARTNER,
         NULL,                        -- 예적금코드 CD_DEPOSIT   
	 NULL, -- NM_DEPOSIT, 
         NULL,                        -- 카드번호 CD_CARD  
	 NULL, -- NM_CARD    
         NULL,                        -- 은행코드 CD_BANK  : MA_PARTNER에서 FG_PARTNER = '002' 인것    
	 NULL, -- NM_BANK,
         NULL,                        -- 품목코드 NO_ITEM      
	 NULL, -- NM_ITEM,
	 @IN_TP_TAX,                -- 세무구분 TP_TAX   
	 @NM_TAX,
	 NULL,                -- 거래구분 CD_TRADE     
	 NULL,                -- NM_TRADE
	 @IN_CD_EXCH,                -- 환종        CD_EXCH    
	 NULL, -- NM_EXCH,
         NULL,                        -- CD_UMNG1      
         NULL,                        -- CD_UMNG2      
         NULL,                        -- CD_UMNG3      
         NULL,                        -- CD_UMNG4      
         NULL,                        -- CD_UMNG5  
	 @IN_NO_COMPANY,-- NO_RES
	 @IN_AM_SUPPLY,   --AM_SUPPLY
         @IN_NO_MDOCU,                        -- 관리번호 = 계산서번호 CD_MNG              
         @IN_DT_ACCT,                -- 거래일자, 시작일자, 발생일자 DT_START      
         NULL,                        -- 만기일자 DT_END              
         @IN_RT_EXCH,                -- 환율        RT_EXCH    
         @IN_AM_EXDO,                -- 외화금액 AM_EXDO     
         '210',                -- 모듈구분(매출:002) NO_MODULE     
         @IN_NO_MDOCU,                -- 모듈관리번호 = 타모듈pkey NO_MDOCU      
         NULL,                        -- 지출결의코드 CD_EPNOTE     
         @IN_ID_WRITE,                -- 전표처리자    
         NULL,                        -- 예산계정 CD_BGACCT     
         NULL,                        -- 결의구분 TP_EPNOTE     
         NULL,                        -- 품의내역 NM_PUMM      
         @P_DT_PROCESS,        -- 작성일자 DT_WRITE      
         0,                                -- AM_ACTSUM     
         0,                                -- AM_JSUM      
         'N',                        -- YN_GWARE      
         NULL,                        -- 사업계획코드 CD_BIZPLAN  
	 NULL,			--CD_ETC  
	 @P_ERRORCODE,  
	 @P_ERRORMSG  
     

	 IF @IN_TYPE = 'V'      --추가 :김정근 20071010 부가세 추가로직  
		 BEGIN  
		  EXEC UP_FI_AUTODOCU_TAX @IN_CD_COMPANY, @IN_CD_PC, @P_NO_DOCU, @P_NO_DOLINE, @IN_AM_SUPPLY  
		 END    
	  
	IF (@P_NO_MODULE = '210') -- 회계전표유형 : 국내매입 이면    
	   BEGIN     
	      UPDATE PU_IVH SET TP_AIS = 'Y' WHERE CD_COMPANY = @IN_CD_COMPANY AND NO_IV = @IN_NO_MDOCU    
	   END     

    
   FETCH NEXT FROM CUR_PU_IV_MNG INTO @IN_CD_COMPANY,@IN_NO_MDOCU,@IN_CD_PARTNER,@IN_CD_CC,@IN_CD_PJT,@IN_ID_WRITE,@IN_CD_BIZAREA,@IN_TP_TAX,@IN_FG_TRANS,@IN_CD_WDEPT,@IN_DT_ACCT,@IN_CD_PC,@IN_CD_ACCT,@IN_AM_DR,@IN_AM_CR,@IN_AM_SUPPLY,@IN_TP_DRCR,
@IN_CD_EXCH,@IN_RT_EXCH,  @IN_CD_EMPLOY,@IN_AM_EXDO,@IN_NM_BIZAREA,@IN_NM_CC,@IN_NM_KOR,@IN_LN_PARTNER,@IN_NM_PROJECT,@IN_NM_DEPT,@IN_TYPE, @IN_CD_RELATION, @NO_BIZAREA, @NM_TAX, @IN_NO_COMPANY     
END    
    
CLOSE CUR_PU_IV_MNG    
DEALLOCATE CUR_PU_IV_MNG    
    
RETURN    
    
  
