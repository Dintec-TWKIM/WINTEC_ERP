USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_PU_IV_MNG_TRANS_DOCU]    Script Date: 2016-11-21 오후 4:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************                                                      
 *********************************************************************************************************/                                                         
ALTER PROCEDURE [NEOE].[SP_CZ_PU_IV_MNG_TRANS_DOCU]                                                      
(                                                      
	@P_CD_COMPANY               NVARCHAR(7),                                                      
	@P_NO_IV                    NVARCHAR(20),                                                      
	@P_NO_MODULE                NVARCHAR(3),                -- /* 회계전표유형 : 국내매입(210) , 구매/외주(001) */                                                      
	@P_FG_EX_CC                 NVARCHAR(3) = '000',         -- CC Group By 제외 옵션       
	@P_OPT_ITEM_RPT_GUBUN       NVARCHAR(1)   =   NULL,     --내역표시구분            
    @P_OPT_ITEM_RPT_TEXT        NVARCHAR(50)  =   NULL,     --내역표시임의내용        
    @P_OPT_ITEM_NM_GUBUN        NVARCHAR(1)   =   NULL,      --품목표시구분 
    @P_ID_USER					NVARCHAR(15)  =   NULL
)                                                      
AS                                                      

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
                                                   
DECLARE @IN_CD_COMPANY                NVARCHAR(7)                                                      
DECLARE @IN_NO_MDOCU                  NVARCHAR(20)                                                      
DECLARE @V_IN_NO_MDOCU                NVARCHAR(20)                                                      
DECLARE @IN_CD_PARTNER                NVARCHAR(20)                                                      
DECLARE @IN_NO_COMPANY                NVARCHAR(20)                                                      
DECLARE @IN_CD_CC                     NVARCHAR(24)                                                      
DECLARE @IN_CD_PJT                    NVARCHAR(20)                                                      
DECLARE @IN_ID_WRITE                  NVARCHAR(10)                                                      
DECLARE @IN_CD_BIZAREA                NVARCHAR(7)                                                      
DECLARE @NO_BIZAREA                   NVARCHAR(20)                                                     
DECLARE @IN_TP_TAX                    NCHAR(3)              
DECLARE @NM_TAX						  NCHAR(15)                           
DECLARE @IN_FG_TRANS                  NCHAR(3)                                                      
DECLARE @IN_CD_WDEPT                  NVARCHAR(12)                                                      
DECLARE @IN_DT_ACCT                   NCHAR(8)                                                      
DECLARE @IN_CD_PC                     NVARCHAR(7)                                                      
DECLARE @IN_CD_ACCT                   NVARCHAR(10)                                                      
DECLARE @IN_AM_DR                     NUMERIC(19,4)                                                      
DECLARE @IN_AM_CR                     NUMERIC(19,4)                                                    
DECLARE @IN_AM_SUPPLY                 NUMERIC(19,4)                                          
DECLARE @IN_TP_ACAREA                 NVARCHAR(3)                                                           
DECLARE @IN_TP_DRCR                   NCHAR(1)                                                      
DECLARE @IN_CD_EXCH                   NVARCHAR(3)                                                      
DECLARE @IN_RT_EXCH                   NUMERIC(17,4)                                                      
DECLARE @IN_CD_EMPLOY                 NVARCHAR(10)                                                      
DECLARE @IN_AM_EXDO                   NUMERIC(19,4)                                                      
DECLARE @IN_NM_BIZAREA                NVARCHAR(50)                                      
DECLARE @IN_NM_CC                     NVARCHAR(50)                                                      
DECLARE @IN_NM_KOR                    NVARCHAR(50)                                 
DECLARE @IN_LN_PARTNER                NVARCHAR(50)                                                      
DECLARE @IN_NM_PROJECT                NVARCHAR(50)                                                      
DECLARE @IN_NM_DEPT                   NVARCHAR(50)                                                      
DECLARE @IN_TYPE                      NCHAR(1)                                                      
DECLARE @IN_CD_RELATION               NCHAR(3)                                                  
                                                  
DECLARE @P_NOTE						  NVARCHAR(100)                                                  
DECLARE @P_NM_TP					  NVARCHAR(20)                                                  
                                                
DECLARE @P_ERRORCODE				  NCHAR(10)                                     
DECLARE @P_ERRORMSG					  NVARCHAR(300)                                                       
                                                    
DECLARE @ERRNO						  INT            -- ERROR 번호                                                  
DECLARE @ERRMSG						  NVARCHAR(255)   -- ERROR 메시지                                                  
DECLARE @CD_DOCU                  	  NVARCHAR(3)     -- 전표유형                                        
DECLARE @DT_PAY_PREARRANGED       	  NVARCHAR(8)     -- 지급예정일 (전표 자금예정일로 넘어간다)                                      
DECLARE @CD_FUND                  	  NVARCHAR(4)     -- 자금과목                                                        
DECLARE @CD_BUDGET                	  NVARCHAR(20)    -- 예산과목                                
DECLARE @CD_BGACCT					  NVARCHAR(10)    -- 예산계정과목                                
DECLARE @FG_PAYBILL					  NVARCHAR(3)     -- 지급조건                                    
DECLARE @FG_BUDGET					  NVARCHAR(3)     -- 예산체크                                                             
DECLARE @CUR_DATE					  NVARCHAR(8) --현재일자                                                        
/*추가: 합계금액 0은 회계전표안만들고 전표처리 20090518*/                                                
DECLARE @AM_TOT						  NUMERIC(17, 4)     
--서버키처리추가 (대신정보 표준적요설정 2010-07-01 LSH)          
DECLARE @V_SERVER_KEY             	  NVARCHAR(50) --서버키              
DECLARE @NM_EXCH                  	  NVARCHAR(50)  
DECLARE @NO_LC                    	  NVARCHAR(20)  
DECLARE @DT_DUE                   	  NVARCHAR(8)  --만기일자  
DECLARE @AM_EX                    	  NUMERIC(19,4) --외화금액
DECLARE @CD_EXCH                  	  NVARCHAR(3)  --환율
DECLARE @RT_VAT                   	  NUMERIC(19,15)  --과세율
DECLARE	@P_YN_ISS				  	  NVARCHAR(1) --- 계산서발행형태
DECLARE @V_CD_UMNG2               	  NVARCHAR(20)     -- 사용자정의 관리항목 A22 (AVL에서 NO_INVOICE 사용) 2012.07.27 전정식
DECLARE @V_VAT_TAX				  	  NUMERIC(17,4)
DECLARE @V_NO_PO_MAX			  	  NVARCHAR(20)
DECLARE @V_NM_ITEM_MAX			  	  NVARCHAR(200)
DECLARE @V_CD_EXC				  	  NVARCHAR(3)
DECLARE @V_CD_EXC_MENU			  	  NVARCHAR(3)
DECLARE @V_CD_EXC_TAX			  	  NVARCHAR(3)  
DECLARE @V_UM						  NUMERIC(17,4)
DECLARE @V_QT						  NUMERIC(17,4)
DECLARE @V_USER_ONLY			      NVARCHAR(100)
DECLARE @V_CD_ITEM_MAX			      NVARCHAR(20)
DECLARE @V_NM_PUMM					  NVARCHAR(100) --동아전용 (적요를 품의로) 2015.03.11 김동우
DECLARE @V_CD_BIZPLAN				  NVARCHAR(20)
DECLARE @V_BUDGET_EXC				  NVARCHAR(3) 
DECLARE @V_AM_BUDGET				  NUMERIC(17,4)
DECLARE @V_USER_ONLY2				  NVARCHAR(50)
DECLARE @V_NO_ACCT					  NUMERIC(5,0)
DECLARE @V_ST_DOCU					  NCHAR(1)
DECLARE @V_ID_ACCEPT                  NVARCHAR(10)
DECLARE @V_TP_EVIDENCE				  NVARCHAR(1) --증빙   
-------------------------------------------------------------------------------------------------------

SET    @V_CD_UMNG2 = NULL
SET    @V_VAT_TAX = 0     
SELECT @V_SERVER_KEY = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG  WHERE YN_UPGRADE = 'Y'                     
SELECT @CUR_DATE = CONVERT(NVARCHAR(8), GETDATE(), 112)          
SELECT @V_CD_EXC = ISNULL(CD_EXC,'000') FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '매입전표처리' AND CD_MODULE = 'PU'
SELECT @V_CD_EXC_MENU = ISNULL(CD_EXC,'000') FROM MA_EXC_MENU WHERE CD_COMPANY = @P_CD_COMPANY AND CD_TITLE = 'PU_A00000013' AND NM_TITLE ='매입전표-작성사원옵션설정' AND CD_MODULE = 'PU'
SELECT @V_CD_EXC_TAX = CD_EXC FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '발주등록(공장)-의제부가세적용'
SET    @V_CD_EXC_TAX = ISNULL(@V_CD_EXC_TAX,'000')  
                                                  
SELECT @AM_TOT = ( AM_K + VAT_TAX )  
  FROM PU_IVH A                            
WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV                                                              
                                                
IF ( @AM_TOT = 0 ) /*공급금액+부가세 = 0이면 회계전표안만들고 TP_AIS = 'Y'처리 20090210*/                                                
BEGIN                                                
 UPDATE PU_IVH SET TP_AIS = 'Y' WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV                  
 RETURN                                                
END                                                 
/*<======*/                                                
-- 예산통제 체크          
SELECT @FG_BUDGET = CD_EXC FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '구매예산CHK' AND CD_MODULE = 'PU'           

SELECT @V_BUDGET_EXC = MAX(CD_EXC) FROM MA_EXC 
WHERE EXC_TITLE IN ('구매요청등록-예산체크사용유무(회계예산연동)','구매품의등록-예산체크사용유무(회계예산연동)', '구매발주등록-예산체크사용유무(회계예산연동)')
AND CD_COMPANY = @P_CD_COMPANY
SET @V_BUDGET_EXC =  ISNULL(@V_BUDGET_EXC,'')     

DECLARE @P_AM_FSIZE NUMERIC(3, 0)   -- 소수점    

IF @P_CD_COMPANY = 'S100'
	SET @P_AM_FSIZE = 2
ELSE
	SET @P_AM_FSIZE = 0
                                                    
DECLARE CUR_PU_IV_MNG CURSOR FOR
/* 차변 : 상품 */
SELECT H.CD_COMPANY,
	   L.NO_IV AS NO_MDOCU,
	   H.CD_PARTNER,         
       --L.CD_CC AS CD_CC,         
	   (CASE WHEN @P_FG_EX_CC = '004' THEN '' ELSE L.CD_CC END) AS CD_CC,
	   (CASE WHEN @P_FG_EX_CC = '004' THEN '' ELSE C.NM_CC END) AS NM_CC,
	   --C.NM_CC AS NM_CC,                                    
	   --ISNULL(L.CD_PJT,'') AS CD_PJT , 
	   (CASE WHEN @P_FG_EX_CC = '003' THEN '' ELSE L.CD_PJT END) AS CD_PJT, 
	   MAX(H.NO_EMP) AS ID_WRITE, 
	   --H.CD_BIZAREA_TAX AS CD_BIZAREA,                                                       
	   (CASE WHEN @V_CD_EXC = '100' THEN H.CD_BIZAREA ELSE H.CD_BIZAREA_TAX END) AS CD_BIZAREA,
	   H.FG_TAX AS TP_TAX,
	   H.FG_TRANS,
	   H.CD_DEPT AS CD_WDEPT,
	   H.DT_PROCESS AS DT_ACCT, 
	   --B.CD_PC,
	   (CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END) AS CD_PC,     
	   A.CD_ACCT, 
	   SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - (L.AM_CLS + CASE WHEN @V_CD_EXC_TAX = '100' AND M05.YN_FICT = 'Y'
															   THEN L.VAT ELSE 0.0 END )
 									 ELSE L.AM_CLS + CASE WHEN @V_CD_EXC_TAX = '100' AND M05.YN_FICT = 'Y'
														  THEN L.VAT ELSE 0.0 END END) AS AM_DR,                                                      
	   0 AS AM_CR,
	   0 AS AM_SUPPLY,                            
	   '1' AS TP_DRCR,
	   (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN '000' ELSE L.CD_EXCH END) AS CD_EXCH, 
	   (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN 1 ELSE L.RT_EXCH END) AS RT_EXCH, 
	   MAX(POH.NO_EMP) AS CD_EMPLOY,                                                      
	   SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_EX_CLS ELSE L.AM_EX_CLS END) AS AM_EXDO,                                                      
	   --B.NM_BIZAREA,
	   (CASE WHEN @V_CD_EXC = '100' THEN B2.NM_BIZAREA ELSE B.NM_BIZAREA END) AS NM_BIZAREA,  
	   MAX(E.NM_KOR) AS NM_KOR,
	   P.LN_PARTNER,  
	   (CASE WHEN @P_FG_EX_CC IN ('003') THEN '' 
										 ELSE (SELECT TOP 1 NM_PROJECT FROM SA_PROJECTH  WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = CASE WHEN @P_FG_EX_CC = '003' THEN '' ELSE L.CD_PJT END) END) AS NM_PROJECT,                                                
	   --(SELECT TOP 1 NM_PROJECT FROM SA_PROJECTH  WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = ISNULL(L.CD_PJT,'')) AS NM_PROJECT,
	   -- PJ.NM_PROJECT,                                                  
	   D.NM_DEPT, 
	   'A' AS TYPE, 
	   --'10' CD_RELATION, --연동항목:일반        
	   MAX(FAT.CD_RELATION) AS CD_RELATION,  --연동항목:일반 2012.02.06 김광석, 김인영 요청 계정코드에 해당하는 연동항목으로...                                                                                                                         
	   B.NO_BIZAREA,               
	   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'MA_B000046') AS NM_TAX,                                                  
	   P.NO_COMPANY,
	   (CASE FAT.TP_DRCR WHEN '1' THEN (CASE FAT.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END) ELSE '0' END) AS TP_ACAREA,                                        
	   H.CD_DOCU,                                       
	   H.DT_PROCESS, 
	   --,H.DT_PAY_PREARRANGED                                      
	   A.CD_FUND,    -- 자금과목                                
	   --POL.CD_BGACCT,  -- 예산계정과목                               
	   --(CASE WHEN @FG_BUDGET = 'Y' AND ISNULL(POL.CD_BGACCT,'') = '' THEN A.CD_ACCT ELSE ISNULL(POL.CD_BGACCT,'') END) AS CD_BGACCT,  -- 예산계정과목                               
	   --POL.CD_BUDGET,  -- 예산과목                            
	   --(CASE WHEN @FG_BUDGET = 'Y' AND ISNULL(POL.CD_BUDGET,'') = '' THEN H.CD_DEPT ELSE ISNULL(POL.CD_BUDGET,'') END) AS CD_BUDGET,  -- 예산과목
	   (CASE WHEN A.FG_AIS = '230' OR A.FG_AIS = '231' THEN A.CD_ACCT ELSE NULL END) AS CD_BGACCT,                            
	   (CASE WHEN A.FG_AIS = '230' OR A.FG_AIS = '231' THEN L.CD_CC ELSE NULL END) AS CD_BUDGET,                            
	   MAX(MH.NM_TP) AS NM_TP,  -- 매출유형명                              
	   H.FG_PAYBILL,  -- 지급조건    
	   MAX(L.NO_LC) AS NO_LC, 
	   H.DT_DUE AS DT_DUE,
	   H.YN_JEONJA,
	   SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END) AS VAT_TAX,
	   MAX(L.NO_PO) AS NO_PO_MAX,
	   MAX(M.NM_ITEM) AS NM_ITEM_MAX,
	   MAX(CASE WHEN @V_SERVER_KEY LIKE 'UNIPOINT%' THEN (L.AM_CLS/L.QT_CLS) ELSE 0 END) AS UNIPINT_UM,
	   SUM(CASE WHEN @V_SERVER_KEY LIKE 'UNIPOINT%' OR @V_SERVER_KEY LIKE 'KPCI%' THEN (CASE L.YN_RETURN WHEN 'Y' THEN - L.QT_CLS 
																												  ELSE L.QT_CLS END)
																				  ELSE 0 END) AS UNIPINT_QT, 
	   MAX(L.CD_ITEM) AS CD_ITEM_MAX,
	   (CASE WHEN @V_BUDGET_EXC = 'Y' THEN ISNULL(POL.CD_BIZPLAN,'') END) AS CD_BIZPLAN,
	   SUM(CASE WHEN @V_BUDGET_EXC = 'Y' THEN (CASE WHEN ISNULL(PRL.YN_BUDGET,'N') = 'Y' OR ISNULL(APPL.YN_BUDGET,'N') = 'Y' OR ISNULL(POL.YN_BUDGET,'N') = 'Y' THEN (CASE L.YN_RETURN WHEN 'Y' THEN L.AM_CLS * -1 
																																													            ELSE L.AM_CLS END) 
																																								ELSE 0 END) END) AS AM_BUDGET,
	   '' AS CD_USERDEF1_PO,	                                          							                              
	   '' AS CD_USERDEF2_PO
FROM PU_IVH H                                                     
JOIN PU_IVL L ON H.NO_IV = L.NO_IV  AND H.CD_COMPANY = L.CD_COMPANY
JOIN MA_PITEM M ON L.CD_ITEM = M.CD_ITEM AND L.CD_PLANT = M.CD_PLANT AND L.CD_COMPANY = M.CD_COMPANY
JOIN MA_CODEDTL CD ON CD.CD_COMPANY = M.CD_COMPANY AND CD.CD_FIELD = 'MA_B000010' AND CD.CD_SYSDEF = M.CLS_ITEM                        
JOIN MA_AISPOSTL A ON A.CD_COMPANY = L.CD_COMPANY AND A.FG_TP = '200' AND A.CD_TP = L.FG_TPPURCHASE AND A.FG_AIS = CD.CD_FLAG3 
JOIN MA_AISPOSTH MH ON A.CD_COMPANY = MH.CD_COMPANY AND A.CD_TP = MH.CD_TP AND A.FG_TP = MH.FG_TP
JOIN FI_ACCTCODE FAT ON FAT.CD_ACCT = A.CD_ACCT AND FAT.CD_COMPANY = A.CD_COMPANY
JOIN MA_BIZAREA B ON H.CD_BIZAREA_TAX = B.CD_BIZAREA AND H.CD_COMPANY = B.CD_COMPANY
JOIN MA_BIZAREA B2 ON H.CD_BIZAREA = B2.CD_BIZAREA AND H.CD_COMPANY = B2.CD_COMPANY
LEFT JOIN PU_POL POL ON L.NO_PO  = POL.NO_PO AND L.NO_POLINE = POL.NO_LINE AND L.CD_COMPANY = POL.CD_COMPANY
LEFT JOIN PU_POH POH ON POH.CD_COMPANY = POL.CD_COMPANY AND POH.NO_PO = POL.NO_PO                                  
LEFT JOIN PU_PRL PRL ON POL.NO_PR = PRL.NO_PR AND POL.NO_PRLINE = PRL.NO_PRLINE AND POL.CD_COMPANY = PRL.CD_COMPANY                                  
LEFT JOIN PU_APPL APPL ON POL.NO_APP = APPL.NO_APP AND POL.NO_APPLINE = APPL.NO_APPLINE AND POL.CD_COMPANY = APPL.CD_COMPANY                                                
LEFT JOIN MA_CC C ON L.CD_CC = C.CD_CC  AND L.CD_COMPANY = C.CD_COMPANY    
LEFT JOIN MA_EMP E ON POH.NO_EMP = E.NO_EMP AND POH.CD_COMPANY = E.CD_COMPANY    
LEFT JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER AND H.CD_COMPANY = P.CD_COMPANY
LEFT JOIN SA_PROJECTH PJ ON L.CD_COMPANY = PJ.CD_COMPANY AND L.CD_PJT = PJ.NO_PROJECT
LEFT JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT AND H.CD_COMPANY = D.CD_COMPANY                                                      
LEFT JOIN V_PU_MA_B000046 M05 ON M05.CD_COMPANY = H.CD_COMPANY AND M05.CD_FIELD = 'MA_B000046' AND M05.CD_SYSDEF = H.FG_TAX              
WHERE H.NO_IV = @P_NO_IV                                                      
AND H.CD_COMPANY = @P_CD_COMPANY                                                      
AND H.TP_AIS ='N' -- 전표발생여부                                                      
AND H.YN_PURSUB ='N' -- 구매                                        
AND L.AM_CLS <> 0
AND A.FG_AIS NOT IN ('230', '231')
--AND PJ.NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)                                                      
GROUP BY H.CD_COMPANY,
	     L.NO_IV,
		 H.CD_PARTNER,         
		 (CASE WHEN @P_FG_EX_CC = '004' THEN '' ELSE L.CD_CC END), 
		 (CASE WHEN @P_FG_EX_CC = '004' THEN '' ELSE C.NM_CC END), 
		 --L.CD_CC,         
		 --C.NM_CC,        
		 --L.CD_PJT,
		 (CASE WHEN @P_FG_EX_CC = '003' THEN '' ELSE L.CD_PJT END), 
		 (CASE WHEN @P_CD_COMPANY = 'W100' THEN '' ELSE H.NO_EMP END), 
		 --H.CD_BIZAREA_TAX,                                                       
		 (CASE WHEN @V_CD_EXC = '100' THEN H.CD_BIZAREA ELSE H.CD_BIZAREA_TAX END),  
		 H.FG_TAX,
		 H.FG_TRANS,
		 H.CD_DEPT,
		 H.DT_PROCESS, 
		 --B.CD_PC, 
		 (CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END),  
		 A.CD_ACCT,
		 (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN '000' ELSE L.CD_EXCH END),
		 (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN 1 ELSE L.RT_EXCH END),                                                       
		 (CASE WHEN @P_CD_COMPANY = 'W100' THEN '' ELSE POH.NO_EMP END),
		 --B.NM_BIZAREA, 
		 (CASE WHEN @V_CD_EXC = '100' THEN B2.NM_BIZAREA ELSE B.NM_BIZAREA END),  
		 (CASE WHEN @P_CD_COMPANY = 'W100' THEN '' ELSE E.NM_KOR END),
		 P.LN_PARTNER,
		 --PJ.NM_PROJECT, 
		 (CASE WHEN @P_FG_EX_CC = '003' THEN '' ELSE PJ.NM_PROJECT END), 
		 D.NM_DEPT,
		 L.CD_COMPANY,                                                   
		 B.NO_BIZAREA,
		 P.NO_COMPANY,
		 (CASE FAT.TP_DRCR WHEN '1' THEN (CASE FAT.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END) ELSE '0' END),                                        
		 H.CD_DOCU,   
		 --,H.DT_PAY_PREARRANGED                                                   
		 A.CD_FUND,
		 --ISNULL(POL.CD_BGACCT,''),
		 --ISNULL(POL.CD_BUDGET,''),
		 (CASE WHEN A.FG_AIS = '230' OR A.FG_AIS = '231' THEN A.CD_ACCT ELSE NULL END),                            
	     (CASE WHEN A.FG_AIS = '230' OR A.FG_AIS = '231' THEN L.CD_CC ELSE NULL END),
		 H.FG_PAYBILL,
		 H.DT_DUE,
		 H.YN_JEONJA,
		 (CASE WHEN @V_SERVER_KEY LIKE 'UNIPOINT%' OR @V_SERVER_KEY LIKE 'KPCI%' THEN L.CD_ITEM ELSE '' END),
		 (CASE WHEN @V_BUDGET_EXC = 'Y' THEN ISNULL(POL.CD_BIZPLAN,'') END)
UNION ALL
/* 차변 : 환차손 */
SELECT H.CD_COMPANY,
	   L.NO_IV AS NO_MDOCU,
	   H.CD_PARTNER,         
       --L.CD_CC AS CD_CC,         
	   (CASE WHEN @P_FG_EX_CC = '004' THEN '' ELSE L.CD_CC END) AS CD_CC,
	   (CASE WHEN @P_FG_EX_CC = '004' THEN '' ELSE C.NM_CC END) AS NM_CC,
	   --C.NM_CC AS NM_CC,                                    
	   --ISNULL(L.CD_PJT,'') AS CD_PJT , 
	   (CASE WHEN @P_FG_EX_CC = '003' THEN '' ELSE L.CD_PJT END) AS CD_PJT, 
	   H.NO_EMP AS ID_WRITE, 
	   --H.CD_BIZAREA_TAX AS CD_BIZAREA,                                                       
	   (CASE WHEN @V_CD_EXC = '100' THEN H.CD_BIZAREA ELSE H.CD_BIZAREA_TAX END) AS CD_BIZAREA,
	   H.FG_TAX AS TP_TAX,
	   H.FG_TRANS,
	   H.CD_DEPT AS CD_WDEPT,
	   H.DT_PROCESS AS DT_ACCT, 
	   --B.CD_PC,
	   (CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END) AS CD_PC,     
	   A.CD_ACCT, 
	   SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - (L.AM_CLS + CASE WHEN @V_CD_EXC_TAX = '100' AND M05.YN_FICT = 'Y'
															   THEN L.VAT ELSE 0.0 END )
 									 ELSE L.AM_CLS + CASE WHEN @V_CD_EXC_TAX = '100' AND M05.YN_FICT = 'Y'
														  THEN L.VAT ELSE 0.0 END END) AS AM_DR,                                                      
	   0 AS AM_CR,
	   0 AS AM_SUPPLY,                            
	   '1' AS TP_DRCR,
	   (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN '000' ELSE L.CD_EXCH END) AS CD_EXCH, 
	   (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN 1 ELSE L.RT_EXCH END) AS RT_EXCH, 
	   POH.NO_EMP AS CD_EMPLOY,                                                      
	   SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_EX_CLS ELSE L.AM_EX_CLS END) AS AM_EXDO,                                                      
	   --B.NM_BIZAREA,
	   (CASE WHEN @V_CD_EXC = '100' THEN B2.NM_BIZAREA ELSE B.NM_BIZAREA END) AS NM_BIZAREA,  
	   E.NM_KOR,
	   P.LN_PARTNER,  
	   (CASE WHEN @P_FG_EX_CC IN ('003') THEN '' 
										 ELSE (SELECT TOP 1 NM_PROJECT FROM SA_PROJECTH  WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = CASE WHEN @P_FG_EX_CC = '003' THEN '' ELSE L.CD_PJT END) END) AS NM_PROJECT,                                                
	   --(SELECT TOP 1 NM_PROJECT FROM SA_PROJECTH  WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = ISNULL(L.CD_PJT,'')) AS NM_PROJECT,
	   -- PJ.NM_PROJECT,                                                  
	   D.NM_DEPT, 
	   'A' AS TYPE, 
	   --'10' CD_RELATION, --연동항목:일반        
	   MAX(FAT.CD_RELATION) AS CD_RELATION,  --연동항목:일반 2012.02.06 김광석, 김인영 요청 계정코드에 해당하는 연동항목으로...                                                                                                                         
	   B.NO_BIZAREA,               
	   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'MA_B000046') AS NM_TAX,                                                  
	   P.NO_COMPANY,
	   (CASE FAT.TP_DRCR WHEN '1' THEN (CASE FAT.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END) ELSE '0' END) AS TP_ACAREA,                                        
	   H.CD_DOCU,                                       
	   H.DT_PROCESS, 
	   --,H.DT_PAY_PREARRANGED                                      
	   A.CD_FUND,    -- 자금과목                                
	   --POL.CD_BGACCT,  -- 예산계정과목                               
	   --(CASE WHEN @FG_BUDGET = 'Y' AND ISNULL(POL.CD_BGACCT,'') = '' THEN A.CD_ACCT ELSE ISNULL(POL.CD_BGACCT,'') END) AS CD_BGACCT,  -- 예산계정과목                               
	   --POL.CD_BUDGET,  -- 예산과목                            
	   --(CASE WHEN @FG_BUDGET = 'Y' AND ISNULL(POL.CD_BUDGET,'') = '' THEN H.CD_DEPT ELSE ISNULL(POL.CD_BUDGET,'') END) AS CD_BUDGET,  -- 예산과목
	   (CASE WHEN A.FG_AIS = '230' OR A.FG_AIS = '231' THEN A.CD_ACCT ELSE NULL END) AS CD_BGACCT,                            
	   (CASE WHEN A.FG_AIS = '230' OR A.FG_AIS = '231' THEN L.CD_CC ELSE NULL END) AS CD_BUDGET,                            
	   MAX(MH.NM_TP) AS NM_TP,  -- 매출유형명                              
	   H.FG_PAYBILL,  -- 지급조건    
	   MAX(L.NO_LC) AS NO_LC, 
	   H.DT_DUE AS DT_DUE,
	   H.YN_JEONJA,
	   SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END) AS VAT_TAX,
	   MAX(L.NO_PO) AS NO_PO_MAX,
	   MAX(M.NM_ITEM) AS NM_ITEM_MAX,
	   MAX(CASE WHEN @V_SERVER_KEY LIKE 'UNIPOINT%' THEN (L.AM_CLS/L.QT_CLS) ELSE 0 END) AS UNIPINT_UM,
	   SUM(CASE WHEN @V_SERVER_KEY LIKE 'UNIPOINT%' OR @V_SERVER_KEY LIKE 'KPCI%' THEN (CASE L.YN_RETURN WHEN 'Y' THEN - L.QT_CLS 
																												  ELSE L.QT_CLS END)
																				  ELSE 0 END) AS UNIPINT_QT, 
	   MAX(L.CD_ITEM) AS CD_ITEM_MAX,
	   (CASE WHEN @V_BUDGET_EXC = 'Y' THEN ISNULL(POL.CD_BIZPLAN,'') END) AS CD_BIZPLAN,
	   SUM(CASE WHEN @V_BUDGET_EXC = 'Y' THEN (CASE WHEN ISNULL(PRL.YN_BUDGET,'N') = 'Y' OR ISNULL(APPL.YN_BUDGET,'N') = 'Y' OR ISNULL(POL.YN_BUDGET,'N') = 'Y' THEN (CASE L.YN_RETURN WHEN 'Y' THEN L.AM_CLS * -1 
																																													            ELSE L.AM_CLS END) 
																																								ELSE 0 END) END) AS AM_BUDGET,
	   '' AS CD_USERDEF1_PO,	                                          							                              
	   '' AS CD_USERDEF2_PO
FROM PU_IVH H                                                     
JOIN PU_IVL L ON H.NO_IV = L.NO_IV  AND H.CD_COMPANY = L.CD_COMPANY
JOIN MA_PITEM M ON L.CD_ITEM = M.CD_ITEM AND L.CD_PLANT = M.CD_PLANT AND L.CD_COMPANY = M.CD_COMPANY
JOIN MA_CODEDTL CD ON CD.CD_COMPANY = M.CD_COMPANY AND CD.CD_FIELD = 'MA_B000010' AND CD.CD_SYSDEF = M.CLS_ITEM                        
JOIN MA_AISPOSTL A ON A.CD_COMPANY = L.CD_COMPANY AND A.FG_TP = '200' AND A.CD_TP = L.FG_TPPURCHASE AND A.FG_AIS = CD.CD_FLAG3 
JOIN MA_AISPOSTH MH ON A.CD_COMPANY = MH.CD_COMPANY AND A.CD_TP = MH.CD_TP AND A.FG_TP = MH.FG_TP
JOIN FI_ACCTCODE FAT ON FAT.CD_ACCT = A.CD_ACCT AND FAT.CD_COMPANY = A.CD_COMPANY
JOIN MA_BIZAREA B ON H.CD_BIZAREA_TAX = B.CD_BIZAREA AND H.CD_COMPANY = B.CD_COMPANY
JOIN MA_BIZAREA B2 ON H.CD_BIZAREA = B2.CD_BIZAREA AND H.CD_COMPANY = B2.CD_COMPANY
LEFT JOIN PU_POL POL ON L.NO_PO  = POL.NO_PO AND L.NO_POLINE = POL.NO_LINE AND L.CD_COMPANY = POL.CD_COMPANY
LEFT JOIN PU_POH POH ON POH.CD_COMPANY = POL.CD_COMPANY AND POH.NO_PO = POL.NO_PO                                  
LEFT JOIN PU_PRL PRL ON POL.NO_PR = PRL.NO_PR AND POL.NO_PRLINE = PRL.NO_PRLINE AND POL.CD_COMPANY = PRL.CD_COMPANY                                  
LEFT JOIN PU_APPL APPL ON POL.NO_APP = APPL.NO_APP AND POL.NO_APPLINE = APPL.NO_APPLINE AND POL.CD_COMPANY = APPL.CD_COMPANY                                                
LEFT JOIN MA_CC C ON L.CD_CC = C.CD_CC  AND L.CD_COMPANY = C.CD_COMPANY    
LEFT JOIN MA_EMP E ON POH.NO_EMP = E.NO_EMP AND POH.CD_COMPANY = E.CD_COMPANY    
LEFT JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER AND H.CD_COMPANY = P.CD_COMPANY
LEFT JOIN SA_PROJECTH PJ ON L.CD_COMPANY = PJ.CD_COMPANY AND L.CD_PJT = PJ.NO_PROJECT
LEFT JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT AND H.CD_COMPANY = D.CD_COMPANY                                                      
LEFT JOIN V_PU_MA_B000046 M05 ON M05.CD_COMPANY = H.CD_COMPANY AND M05.CD_FIELD = 'MA_B000046' AND M05.CD_SYSDEF = H.FG_TAX              
WHERE H.NO_IV = @P_NO_IV                                                      
AND H.CD_COMPANY = @P_CD_COMPANY                                                      
AND H.TP_AIS ='N' -- 전표발생여부                                                      
AND H.YN_PURSUB ='N' -- 구매                                        
AND L.AM_CLS <> 0
AND A.FG_AIS = '230'
--AND PJ.NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)                                                      
GROUP BY H.CD_COMPANY,
	     L.NO_IV,
		 H.CD_PARTNER,         
		 (CASE WHEN @P_FG_EX_CC = '004' THEN '' ELSE L.CD_CC END), 
		 (CASE WHEN @P_FG_EX_CC = '004' THEN '' ELSE C.NM_CC END), 
		 --L.CD_CC,         
		 --C.NM_CC,        
		 --L.CD_PJT,
		 (CASE WHEN @P_FG_EX_CC = '003' THEN '' ELSE L.CD_PJT END), 
		 H.NO_EMP, 
		 --H.CD_BIZAREA_TAX,                                                       
		 (CASE WHEN @V_CD_EXC = '100' THEN H.CD_BIZAREA ELSE H.CD_BIZAREA_TAX END),  
		 H.FG_TAX,
		 H.FG_TRANS,
		 H.CD_DEPT,
		 H.DT_PROCESS, 
		 --B.CD_PC, 
		 (CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END),  
		 A.CD_ACCT,
		 (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN '000' ELSE L.CD_EXCH END),
		 (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN 1 ELSE L.RT_EXCH END),                                                       
		 POH.NO_EMP,
		 --B.NM_BIZAREA, 
		 (CASE WHEN @V_CD_EXC = '100' THEN B2.NM_BIZAREA ELSE B.NM_BIZAREA END),  
		 E.NM_KOR,
		 P.LN_PARTNER,
		 --PJ.NM_PROJECT, 
		 (CASE WHEN @P_FG_EX_CC = '003' THEN '' ELSE PJ.NM_PROJECT END), 
		 D.NM_DEPT,
		 L.CD_COMPANY,                                                   
		 B.NO_BIZAREA,
		 P.NO_COMPANY,
		 (CASE FAT.TP_DRCR WHEN '1' THEN (CASE FAT.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END) ELSE '0' END),                                        
		 H.CD_DOCU,   
		 --,H.DT_PAY_PREARRANGED                                                   
		 A.CD_FUND,
		 --ISNULL(POL.CD_BGACCT,''),
		 --ISNULL(POL.CD_BUDGET,''),
		 (CASE WHEN A.FG_AIS = '230' OR A.FG_AIS = '231' THEN A.CD_ACCT ELSE NULL END),                            
	     (CASE WHEN A.FG_AIS = '230' OR A.FG_AIS = '231' THEN L.CD_CC ELSE NULL END),
		 H.FG_PAYBILL,
		 H.DT_DUE,
		 H.YN_JEONJA,
		 (CASE WHEN @V_SERVER_KEY LIKE 'UNIPOINT%' OR @V_SERVER_KEY LIKE 'KPCI%' THEN L.CD_ITEM ELSE '' END),
		 (CASE WHEN @V_BUDGET_EXC = 'Y' THEN ISNULL(POL.CD_BIZPLAN,'') END)
/* 차변 : 부가세 */                      
UNION ALL                                      
SELECT H.CD_COMPANY,
	   L.NO_IV AS NO_MDOCU,
	   H.CD_PARTNER,         
	   --'' CD_CC,
	   MAX(CASE WHEN @V_SERVER_KEY = 'DKONT' THEN MCC.CD_FLAG1 
			    WHEN @V_SERVER_KEY = 'WHASUNG' THEN L.CD_CC ELSE '' END) AS CD_CC, 
	   --'' AS NM_CC,         
	   MAX(CASE WHEN @V_SERVER_KEY = 'DKONT' THEN CC.NM_CC 
				WHEN @V_SERVER_KEY = 'WHASUNG' THEN CC2.NM_CC ELSE '' END) AS NM_CC, 
	   '' CD_PJT,
	   H.NO_EMP AS ID_WRITE,
	   H.CD_BIZAREA_TAX AS CD_BIZAREA,                                                       
	   H.FG_TAX AS TP_TAX,
	   H.FG_TRANS,
	   H.CD_DEPT AS CD_WDEPT,
	   H.DT_PROCESS AS DT_ACCT, 
	   --B.CD_PC,
	   (CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END) AS CD_PC,
	   A.CD_ACCT,                                                      
	   (CASE WHEN @V_CD_EXC_TAX = '100' AND MAX(M05.YN_FICT) = 'Y' THEN 0
																   ELSE SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT 
																									  ELSE L.VAT END) END) AS AM_DR,                                                 
	   0 AS AM_CR,
	   SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - (L.AM_CLS + (CASE WHEN @V_CD_EXC_TAX = '100' AND M05.YN_FICT = 'Y' THEN L.VAT 
																												 ELSE 0.0 END))
									 ELSE L.AM_CLS + (CASE WHEN @V_CD_EXC_TAX = '100' AND M05.YN_FICT = 'Y' THEN L.VAT 
																											ELSE 0.0 END) END) AS AM_SUPPLY,                                                      
	   '1' AS TP_DRCR,
	   MAX(H.CD_EXCH) AS CD_EXCH, 
	   MAX(H.RT_EXCH) AS RT_EXCH, 
	   '' AS CD_EMPLOY,                                                      
	   (CASE WHEN @V_CD_EXC_TAX = '100' AND MAX(M05.YN_FICT) = 'Y' THEN 0
																   ELSE SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - ROUND(L.AM_EX_CLS * M05.RT_VAT, @P_AM_FSIZE) 
																								      ELSE ROUND(L.AM_EX_CLS * M05.RT_VAT, @P_AM_FSIZE) END) END) AS AM_EXDO,                                                      
	   B.NM_BIZAREA,  
	   E.NM_KOR,
	   P.LN_PARTNER,
	   '' AS NM_PROJECT,
	   D.NM_DEPT,
	   'V' AS TYPE, 
	   '30' AS CD_RELATION, --연동항목:일반                                                       
	   B.NO_BIZAREA,                                                  
	   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'MA_B000046') AS NM_TAX,                       
	   P.NO_COMPANY,
	   '0' AS TP_ACAREA ,                                        
	   H.CD_DOCU,               
	   H.DT_PROCESS,
	   --,H.DT_PAY_PREARRANGED                                      
	   '' AS CD_FUND,    -- 자금과목                                
	   '' AS CD_BGACCT,    -- 예산과목                                
	   '' AS CD_BUDGET,                            
	   MAX(MH.NM_TP) AS NM_TP,  -- 매출유형명                              
	   '' AS FG_PAYBILL,  -- 지급조건   
	   '' AS NO_LC,                  
	   '' AS DT_DUE,
	   H.YN_JEONJA,
	   SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END) AS VAT_TAX,
	   MAX(L.NO_PO) AS NO_PO_MAX,
	   MAX(M.NM_ITEM) AS NM_ITEM_MAX,
	   0 AS UNIPINT_UM,
	   SUM(L.QT_CLS) AS UNIPINT_QT,
	   MAX(L.CD_ITEM) AS CD_ITEM_MAX,
	   '' AS CD_BIZPLAN,
	   0 AS AM_BUDGET,
	   '' AS CD_USERDEF1_PO,	                                          							                              
	   '' AS CD_USERDEF2_PO		                        
FROM PU_IVH H                                                     
JOIN PU_IVL L ON H.NO_IV  = L.NO_IV AND H.CD_COMPANY = L.CD_COMPANY                                                       
JOIN MA_PITEM M ON L.CD_ITEM = M.CD_ITEM  AND L.CD_PLANT = M.CD_PLANT AND L.CD_COMPANY = M.CD_COMPANY                                         
JOIN MA_AISPOSTL A ON L.CD_COMPANY = A.CD_COMPANY  AND A.FG_TP ='200' AND L.FG_TPPURCHASE = A.CD_TP AND A.FG_AIS = '210' --부가세                                              
JOIN MA_AISPOSTH MH ON A.CD_COMPANY = MH.CD_COMPANY AND A.CD_TP = MH.CD_TP AND A.FG_TP = MH.FG_TP                         
JOIN FI_ACCTCODE FAT ON FAT.CD_ACCT = A.CD_ACCT AND FAT.CD_COMPANY = A.CD_COMPANY                                                      
JOIN MA_BIZAREA B ON H.CD_BIZAREA_TAX = B.CD_BIZAREA  AND H.CD_COMPANY = B.CD_COMPANY  
JOIN MA_BIZAREA B2 ON H.CD_BIZAREA = B2.CD_BIZAREA  AND H.CD_COMPANY = B2.CD_COMPANY                                                       
--JOIN PU_POL POL ON L.NO_PO  = POL.NO_PO AND L.NO_POLINE = POL.NO_LINE AND L.CD_COMPANY = POL.CD_COMPANY                                 
LEFT JOIN MA_EMP E ON H.NO_EMP = E.NO_EMP AND H.CD_COMPANY = E.CD_COMPANY                                                      
LEFT JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER  AND H.CD_COMPANY = P.CD_COMPANY                                                              
LEFT JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT  AND H.CD_COMPANY = D.CD_COMPANY                                                      
LEFT JOIN V_PU_MA_B000046 M05 ON M05.CD_COMPANY = H.CD_COMPANY AND M05.CD_FIELD = 'MA_B000046' AND M05.CD_SYSDEF = H.FG_TAX       
LEFT JOIN MA_CODEDTL MCC ON MCC.CD_FIELD='MA_B000062' AND MCC.CD_SYSDEF = B.CD_AREA AND MCC.CD_COMPANY = B.CD_COMPANY
LEFT JOIN MA_CC CC ON CC.CD_CC = MCC.CD_FLAG1 AND CC.CD_COMPANY = MCC.CD_COMPANY
LEFT JOIN MA_CC CC2 ON CC2.CD_CC = L.CD_CC AND CC2.CD_COMPANY = L.CD_COMPANY
WHERE H.NO_IV = @P_NO_IV                                                      
AND H.CD_COMPANY = @P_CD_COMPANY                                                     
AND H.TP_AIS ='N'  -- 전표발생여부                                                      
AND H.YN_PURSUB ='N' --구매                           
AND H.FG_TAX <> '99'
AND H.FG_TRANS = '001'
AND A.FG_AIS NOT IN ('230', '231')
--
-- 2015.07.22 D20150722076 김현수, 허상규
-- 계정처리유형의 전표유형코드가 CH1 값의 경우
-- 부가세를 생성하지 않고 중국의 전용화면에서 부가세를 처리한다.
--
AND ISNULL(H.CD_DOCU, '') <> 'CH1'
AND ((@V_SERVER_KEY = 'DYPNF' AND ISNULL( H.FG_TAX , '') <> '') OR (@V_SERVER_KEY <> 'DYPNF'))
GROUP BY H.CD_COMPANY,
		 L.NO_IV,
		 H.CD_PARTNER,
		 H.NO_EMP,
		 H.CD_BIZAREA_TAX,                        
		 H.FG_TAX,
		 H.FG_TRANS,
		 H.CD_DEPT,
		 H.DT_PROCESS,
		 --B.CD_PC, 
		 (CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END),
	     A.CD_ACCT,                                                      
		 B.NM_BIZAREA,
		 E.NM_KOR,
		 P.LN_PARTNER,
		 D.NM_DEPT,
		 B.NO_BIZAREA,
		 P.NO_COMPANY,                                        
		 H.CD_DOCU,
		 --,H.DT_PAY_PREARRANGED                                                                       
		 H.YN_JEONJA                                             
/* 대변 : 외상매입금, 원화금액 + 부가세 */                                                      
UNION ALL
SELECT H.CD_COMPANY,
	   L.NO_IV AS NO_MDOCU,
	   H.CD_PARTNER,         
	   (CASE WHEN (@P_FG_EX_CC IN ('001','004') OR @P_CD_COMPANY = 'W100') THEN '' ELSE L.CD_CC END) AS CD_CC,         
	   (CASE WHEN (@P_FG_EX_CC IN ('001','004') OR @P_CD_COMPANY = 'W100') THEN '' ELSE C.NM_CC END) AS NM_CC,                                    
	   (CASE WHEN @P_FG_EX_CC IN ('002','003') THEN '' ELSE L.CD_PJT END) AS CD_PJT,  
	   MAX(H.NO_EMP) AS ID_WRITE, 
	   --H.CD_BIZAREA_TAX AS CD_BIZAREA,                                                       
	   (CASE WHEN @V_CD_EXC = '100' THEN H.CD_BIZAREA ELSE H.CD_BIZAREA_TAX END) AS CD_BIZAREA,
	   -- H.FG_TAX AS TP_TAX, 
	   '' AS TP_TAX, 
	   H.FG_TRANS,
	   H.CD_DEPT AS CD_WDEPT,
	   H.DT_PROCESS AS DT_ACCT, 
	   --B.CD_PC,
	   (CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END) AS CD_PC,
	   A.CD_ACCT,                                  
	   0 AS AM_DR,
	   --
	   -- 2015.07.22 D20150722076 김현수, 허상규
	   -- 계정처리유형의 전표유형코드가 CH1 값의 경우
	   -- 부가세를 생성하지 않고 중국의 전용화면에서 부가세를 처리한다.
	   --
	   SUM(CASE WHEN (L.YN_RETURN = 'Y' OR M.CLS_ITEM = '018') THEN -L.AM_CLS ELSE L.AM_CLS END) + 
	   SUM(CASE WHEN ISNULL(H.CD_DOCU, '') = 'CH1' THEN 0 
												   ELSE (CASE L.YN_RETURN WHEN 'Y' THEN -L.VAT ELSE L.VAT END) END) AS AM_CR,                                                        
	   0 AS AM_SUPPLY,                                                           
	   '2' AS TP_DRCR,
	   (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN '000' ELSE L.CD_EXCH END) AS CD_EXCH, 
	   (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN 1 ELSE L.RT_EXCH END) AS RT_EXCH,
	   MAX(POH.NO_EMP) AS CD_EMPLOY,                                                      
	   SUM(CASE WHEN (L.YN_RETURN = 'Y' OR M.CLS_ITEM = '018') THEN -L.AM_EX_CLS ELSE L.AM_EX_CLS END) + 
	   SUM(CASE WHEN ISNULL(H.CD_DOCU, '') = 'CH1' THEN 0 
												   ELSE (CASE L.YN_RETURN WHEN 'Y' THEN 0 - ROUND(L.AM_EX_CLS * M05.RT_VAT, @P_AM_FSIZE)
																				   ELSE ROUND(L.AM_EX_CLS * M05.RT_VAT, @P_AM_FSIZE) END) END) AS AM_EXDO,                                                      
	   --B.NM_BIZAREA,
	   (CASE WHEN @V_CD_EXC = '100' THEN B2.NM_BIZAREA ELSE B.NM_BIZAREA END) AS NM_BIZAREA,
	   MAX(E.NM_KOR) AS NM_KOR,
	   P.LN_PARTNER,                                                  
	   --(SELECT TOP 1 NM_PROJECT FROM SA_PROJECTH  WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = ISNULL(L.CD_PJT,'')) AS NM_PROJECT,
	   --PJ.NM_PROJECT,                                                   
	   (CASE WHEN @P_FG_EX_CC IN ('002','003') THEN '' ELSE PJ.NM_PROJECT END) AS NM_PROJECT,  
	   D.NM_DEPT,
	   'T' AS TYPE, 
	   '10' AS CD_RELATION, --연동항목:일반                                                     
	   B.NO_BIZAREA,                                             
	   --(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'FI_T000011') AS NM_TAX,
	   '' AS NM_TAX,
	   P.NO_COMPANY, 
	   (CASE FAT.TP_DRCR WHEN '2' THEN (CASE FAT.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END) ELSE '0' END) AS TP_ACAREA,                                        
	   H.CD_DOCU,                                    
	   H.DT_PAY_PREARRANGED,                                    
	   A.CD_FUND,                                
	   '' AS CD_BGACCT,                            
	   '' AS CD_BUDGET,                              
	   MAX(MH.NM_TP) AS NM_TP,  -- 매출유형명                                 
	   H.FG_PAYBILL,  -- 지급조건    
	   MAX(L.NO_LC) AS NO_LC,
	   H.DT_DUE,
	   H.YN_JEONJA,
	   (CASE WHEN @V_CD_EXC_TAX = '100' AND MAX(M05.YN_FICT) = 'Y' THEN 0
																   ELSE SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT 
																									  ELSE L.VAT END) END) AS VAT_TAX,
	   MAX(L.NO_PO) AS NO_PO_MAX,
	   MAX(M.NM_ITEM) AS NM_ITEM_MAX,
	   0 AS UNIPINT_UM,
	   SUM(CASE WHEN @V_SERVER_KEY LIKE 'KPCI%' THEN (CASE L.YN_RETURN WHEN 'Y' THEN - L.QT_CLS 
																			    ELSE L.QT_CLS END)
												ELSE 0 END) AS UNIPINT_QT,
	   MAX(L.CD_ITEM) AS CD_ITEM_MAX,
	   '' AS CD_BIZPLAN,
	   0 AS AM_BUDGET,
	   (CASE WHEN @V_SERVER_KEY LIKE 'HDS%' THEN POL.CD_USERDEF1 ELSE '' END) AS CD_USERDEF1_PO,
       (CASE WHEN @V_SERVER_KEY LIKE 'HDS%' THEN POL.CD_USERDEF2 ELSE '' END) AS CD_USERDEF2_PO			               
FROM PU_IVH H                              
JOIN PU_IVL L ON H.NO_IV  = L.NO_IV AND H.CD_COMPANY = L.CD_COMPANY          
JOIN MA_PITEM M ON L.CD_ITEM  = M.CD_ITEM AND L.CD_PLANT = M.CD_PLANT AND L.CD_COMPANY = M.CD_COMPANY
JOIN MA_AISPOSTL A ON A.CD_COMPANY = L.CD_COMPANY AND A.FG_TP = '200' AND A.CD_TP = L.FG_TPPURCHASE AND A.FG_AIS = (CASE WHEN H.CD_COMPANY = 'W100' THEN (CASE WHEN M.CLS_ITEM = '001' THEN (CASE WHEN H.FG_TRANS = '001' THEN '211' ELSE '212' END) 
																																													   ELSE '213' END)
																																					ELSE (CASE WHEN H.FG_TRANS = '001' THEN '211' ELSE '212' END) END)
JOIN MA_AISPOSTH MH ON A.CD_COMPANY = MH.CD_COMPANY AND A.CD_TP = MH.CD_TP AND A.FG_TP = MH.FG_TP                                 
JOIN FI_ACCTCODE FAT ON FAT.CD_ACCT = A.CD_ACCT AND FAT.CD_COMPANY = A.CD_COMPANY 
JOIN MA_BIZAREA B ON H.CD_BIZAREA_TAX = B.CD_BIZAREA AND H.CD_COMPANY = B.CD_COMPANY   
JOIN MA_BIZAREA B2 ON H.CD_BIZAREA = B2.CD_BIZAREA AND H.CD_COMPANY = B2.CD_COMPANY
LEFT JOIN PU_POL POL ON L.NO_PO = POL.NO_PO AND L.NO_POLINE = POL.NO_LINE AND L.CD_COMPANY = POL.CD_COMPANY
LEFT JOIN PU_POH POH ON POH.CD_COMPANY = POL.CD_COMPANY AND POH.NO_PO = POL.NO_PO                   
LEFT JOIN MA_CC C ON L.CD_CC = C.CD_CC AND L.CD_COMPANY = C.CD_COMPANY            
LEFT JOIN MA_EMP E ON POH.NO_EMP = E.NO_EMP AND POH.CD_COMPANY = E.CD_COMPANY    
LEFT JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER AND H.CD_COMPANY = P.CD_COMPANY
LEFT JOIN SA_PROJECTH PJ ON L.CD_COMPANY = PJ.CD_COMPANY AND L.CD_PJT = PJ.NO_PROJECT
LEFT JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT AND H.CD_COMPANY = D.CD_COMPANY                                                      
LEFT JOIN V_PU_MA_B000046 M05 ON M05.CD_COMPANY = H.CD_COMPANY AND M05.CD_FIELD = 'MA_B000046' AND M05.CD_SYSDEF = H.FG_TAX                                                        
WHERE H.NO_IV = @P_NO_IV                                                      
AND H.CD_COMPANY = @P_CD_COMPANY                                                      
AND H.TP_AIS ='N'  -- 전표발생여부                                                      
AND H.YN_PURSUB ='N' -- 구매                                             
AND L.AM_CLS <> 0
--AND PJ.NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)
GROUP BY H.CD_COMPANY,
		 L.NO_IV,
		 H.CD_PARTNER,         
		 (CASE WHEN (@P_FG_EX_CC IN ('001','004') OR @P_CD_COMPANY = 'W100') THEN '' ELSE L.CD_CC END),       
		 (CASE WHEN (@P_FG_EX_CC IN ('001','004') OR @P_CD_COMPANY = 'W100') THEN '' ELSE C.NM_CC END),                                    
		 (CASE WHEN @P_FG_EX_CC IN ('002','003') THEN '' ELSE L.CD_PJT END),   
		 (CASE WHEN @P_CD_COMPANY = 'W100' THEN '' ELSE H.NO_EMP END), 
		 --H.CD_BIZAREA_TAX,                                                       
		 (CASE WHEN @V_CD_EXC = '100' THEN H.CD_BIZAREA ELSE H.CD_BIZAREA_TAX END),     
		 --H.FG_TAX, 
		 H.FG_TRANS,
		 H.CD_DEPT,
		 H.DT_PROCESS,
		 --B.CD_PC,
		 (CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END),  
		 A.CD_ACCT,
		 (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN '000' ELSE L.CD_EXCH END),
		 (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN 1 ELSE L.RT_EXCH END),  
		 (CASE WHEN @P_CD_COMPANY = 'W100' THEN '' ELSE POH.NO_EMP END),
		 --B.NM_BIZAREA,
		 (CASE WHEN @V_CD_EXC = '100' THEN B2.NM_BIZAREA ELSE B.NM_BIZAREA END),
		 (CASE WHEN @P_CD_COMPANY = 'W100' THEN '' ELSE E.NM_KOR END),
		 P.LN_PARTNER,  
		 (CASE WHEN @P_FG_EX_CC IN ('002','003') THEN '' ELSE PJ.NM_PROJECT END),  
		 D.NM_DEPT,
		 L.CD_COMPANY,                                                   
		 B.NO_BIZAREA,
		 P.NO_COMPANY,
		 (CASE FAT.TP_DRCR WHEN '2' THEN (CASE FAT.YN_BAN WHEN 'Y' THEN '4' 
																   ELSE '0' END)
					                ELSE '0' END),                                        
		 H.CD_DOCU,
		 H.DT_PAY_PREARRANGED,                                    
		 A.CD_FUND,
		 H.FG_PAYBILL,
		 H.DT_DUE,
		 H.YN_JEONJA,
		 (CASE WHEN @V_SERVER_KEY LIKE 'KPCI%' THEN L.CD_ITEM ELSE '' END), 
		 (CASE WHEN @V_SERVER_KEY LIKE 'HDS%' THEN POL.CD_USERDEF1 ELSE '' END),
		 (CASE WHEN @V_SERVER_KEY LIKE 'HDS%' THEN POL.CD_USERDEF2 ELSE '' END)
/* 대변 : 환차익 */
UNION ALL
SELECT H.CD_COMPANY,
	   L.NO_IV AS NO_MDOCU,
	   H.CD_PARTNER,         
	   (CASE WHEN @P_FG_EX_CC IN ('001','004') THEN '' ELSE L.CD_CC END) AS CD_CC,         
	   (CASE WHEN @P_FG_EX_CC IN ('001','004') THEN '' ELSE C.NM_CC END) AS NM_CC,                                    
	   (CASE WHEN @P_FG_EX_CC IN ('002','003') THEN '' ELSE L.CD_PJT END) AS CD_PJT,  
	   H.NO_EMP AS ID_WRITE, 
	   --H.CD_BIZAREA_TAX AS CD_BIZAREA,                                                       
	   (CASE WHEN @V_CD_EXC = '100' THEN H.CD_BIZAREA ELSE H.CD_BIZAREA_TAX END) AS CD_BIZAREA,
	   -- H.FG_TAX AS TP_TAX, 
	   '' AS TP_TAX, 
	   H.FG_TRANS,
	   H.CD_DEPT AS CD_WDEPT,
	   H.DT_PROCESS AS DT_ACCT, 
	   --B.CD_PC,
	   (CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END) AS CD_PC,
	   A.CD_ACCT,                                  
	   0 AS AM_DR,
	   --
	   -- 2015.07.22 D20150722076 김현수, 허상규
	   -- 계정처리유형의 전표유형코드가 CH1 값의 경우
	   -- 부가세를 생성하지 않고 중국의 전용화면에서 부가세를 처리한다.
	   --
	   SUM(L.AM_CLS) AS AM_CR,                                                        
	   0 AS AM_SUPPLY,                                                           
	   '2' AS TP_DRCR,
	   (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN '000' ELSE L.CD_EXCH END) AS CD_EXCH, 
	   (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN 1 ELSE L.RT_EXCH END) AS RT_EXCH,
	   POH.NO_EMP AS CD_EMPLOY,                                                      
	   SUM(L.AM_EX_CLS) AS AM_EXDO,                                                      
	   --B.NM_BIZAREA,
	   (CASE WHEN @V_CD_EXC = '100' THEN B2.NM_BIZAREA ELSE B.NM_BIZAREA END) AS NM_BIZAREA,
	   E.NM_KOR,
	   P.LN_PARTNER,                                                  
	   --(SELECT TOP 1 NM_PROJECT FROM SA_PROJECTH  WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = ISNULL(L.CD_PJT,'')) AS NM_PROJECT,
	   --PJ.NM_PROJECT,                                                   
	   (CASE WHEN @P_FG_EX_CC IN ('002','003') THEN '' ELSE PJ.NM_PROJECT END) AS NM_PROJECT,  
	   D.NM_DEPT,
	   'T' AS TYPE, 
	   '10' AS CD_RELATION, --연동항목:일반                                                     
	   B.NO_BIZAREA,                                             
	   --(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = H.CD_COMPANY AND CD_SYSDEF = H.FG_TAX  AND CD_FIELD = 'FI_T000011') AS NM_TAX,
	   '' AS NM_TAX,
	   P.NO_COMPANY, 
	   (CASE FAT.TP_DRCR WHEN '2' THEN (CASE FAT.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END) ELSE '0' END) AS TP_ACAREA,                                        
	   H.CD_DOCU,                                    
	   H.DT_PAY_PREARRANGED,                                    
	   A.CD_FUND,                                
	   '' AS CD_BGACCT,                            
	   '' AS CD_BUDGET,                              
	   MAX(MH.NM_TP) AS NM_TP,  -- 매출유형명                                 
	   H.FG_PAYBILL,  -- 지급조건    
	   MAX(L.NO_LC) AS NO_LC,
	   H.DT_DUE,
	   H.YN_JEONJA,
	   (CASE WHEN @V_CD_EXC_TAX = '100' AND MAX(M05.YN_FICT) = 'Y' THEN 0
																   ELSE SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT 
																									  ELSE L.VAT END) END) AS VAT_TAX,
	   MAX(L.NO_PO) AS NO_PO_MAX,
	   MAX(M.NM_ITEM) AS NM_ITEM_MAX,
	   0 AS UNIPINT_UM,
	   SUM(CASE WHEN @V_SERVER_KEY LIKE 'KPCI%' THEN (CASE L.YN_RETURN WHEN 'Y' THEN - L.QT_CLS 
																			    ELSE L.QT_CLS END)
												ELSE 0 END) AS UNIPINT_QT,
	   MAX(L.CD_ITEM) AS CD_ITEM_MAX,
	   '' AS CD_BIZPLAN,
	   0 AS AM_BUDGET,
	   (CASE WHEN @V_SERVER_KEY LIKE 'HDS%' THEN POL.CD_USERDEF1 ELSE '' END) AS CD_USERDEF1_PO,
       (CASE WHEN @V_SERVER_KEY LIKE 'HDS%' THEN POL.CD_USERDEF2 ELSE '' END) AS CD_USERDEF2_PO			               
FROM PU_IVH H                              
JOIN PU_IVL L ON H.NO_IV  = L.NO_IV AND H.CD_COMPANY = L.CD_COMPANY          
JOIN MA_PITEM M ON L.CD_ITEM = M.CD_ITEM AND L.CD_PLANT = M.CD_PLANT AND L.CD_COMPANY = M.CD_COMPANY
JOIN MA_CODEDTL CD ON CD.CD_COMPANY = M.CD_COMPANY AND CD.CD_FIELD = 'MA_B000010' AND CD.CD_SYSDEF = M.CLS_ITEM                        
JOIN MA_AISPOSTL A ON A.CD_COMPANY = L.CD_COMPANY AND A.FG_TP = '200' AND A.CD_TP = L.FG_TPPURCHASE AND A.FG_AIS = CD.CD_FLAG3
JOIN MA_AISPOSTH MH ON A.CD_COMPANY = MH.CD_COMPANY AND A.CD_TP = MH.CD_TP AND A.FG_TP = MH.FG_TP                                 
JOIN FI_ACCTCODE FAT ON FAT.CD_ACCT = A.CD_ACCT AND FAT.CD_COMPANY = A.CD_COMPANY 
JOIN MA_BIZAREA B ON H.CD_BIZAREA_TAX = B.CD_BIZAREA AND H.CD_COMPANY = B.CD_COMPANY   
JOIN MA_BIZAREA B2 ON H.CD_BIZAREA = B2.CD_BIZAREA AND H.CD_COMPANY = B2.CD_COMPANY
LEFT JOIN PU_POL POL ON L.NO_PO = POL.NO_PO AND L.NO_POLINE = POL.NO_LINE AND L.CD_COMPANY = POL.CD_COMPANY
LEFT JOIN PU_POH POH ON POH.CD_COMPANY = POL.CD_COMPANY AND POH.NO_PO = POL.NO_PO                   
LEFT JOIN MA_CC C ON L.CD_CC = C.CD_CC AND L.CD_COMPANY = C.CD_COMPANY            
LEFT JOIN MA_EMP E ON POH.NO_EMP = E.NO_EMP AND POH.CD_COMPANY = E.CD_COMPANY    
LEFT JOIN MA_PARTNER P ON H.CD_PARTNER = P.CD_PARTNER AND H.CD_COMPANY = P.CD_COMPANY
LEFT JOIN SA_PROJECTH PJ ON L.CD_COMPANY = PJ.CD_COMPANY AND L.CD_PJT = PJ.NO_PROJECT
LEFT JOIN MA_DEPT D ON H.CD_DEPT = D.CD_DEPT AND H.CD_COMPANY = D.CD_COMPANY                                                      
LEFT JOIN V_PU_MA_B000046 M05 ON M05.CD_COMPANY = H.CD_COMPANY AND M05.CD_FIELD = 'MA_B000046' AND M05.CD_SYSDEF = H.FG_TAX                                                        
WHERE H.NO_IV = @P_NO_IV                                                      
AND H.CD_COMPANY = @P_CD_COMPANY                                                      
AND H.TP_AIS ='N'  -- 전표발생여부                                                      
AND H.YN_PURSUB ='N' -- 구매                                             
AND L.AM_CLS <> 0
AND A.FG_AIS = '231'
--AND PJ.NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)
GROUP BY H.CD_COMPANY,
		 L.NO_IV,
		 H.CD_PARTNER,         
		 (CASE WHEN @P_FG_EX_CC IN ('001','004') THEN '' ELSE L.CD_CC END),       
		 (CASE WHEN @P_FG_EX_CC IN ('001','004') THEN '' ELSE C.NM_CC END),                                    
		 (CASE WHEN @P_FG_EX_CC IN ('002','003') THEN '' ELSE L.CD_PJT END),   
		 H.NO_EMP , 
		 --H.CD_BIZAREA_TAX,                                                       
		 (CASE WHEN @V_CD_EXC = '100' THEN H.CD_BIZAREA ELSE H.CD_BIZAREA_TAX END),     
		 --H.FG_TAX, 
		 H.FG_TRANS,
		 H.CD_DEPT,
		 H.DT_PROCESS,
		 --B.CD_PC,
		 (CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END),  
		 A.CD_ACCT,
		 (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN '000' ELSE L.CD_EXCH END),
		 (CASE WHEN @V_SERVER_KEY LIKE 'HYDGT%' THEN 1 ELSE L.RT_EXCH END),  
		 POH.NO_EMP,
		 --B.NM_BIZAREA,
		 (CASE WHEN @V_CD_EXC = '100' THEN B2.NM_BIZAREA ELSE B.NM_BIZAREA END),
		 E.NM_KOR,
		 P.LN_PARTNER,  
		 (CASE WHEN @P_FG_EX_CC IN ('002','003') THEN '' ELSE PJ.NM_PROJECT END),  
		 D.NM_DEPT,
		 L.CD_COMPANY,                                                   
		 B.NO_BIZAREA,
		 P.NO_COMPANY,
		 (CASE FAT.TP_DRCR WHEN '2' THEN (CASE FAT.YN_BAN WHEN 'Y' THEN '4' 
																   ELSE '0' END)
					                ELSE '0' END),                                        
		 H.CD_DOCU,
		 H.DT_PAY_PREARRANGED,                                    
		 A.CD_FUND,
		 H.FG_PAYBILL,
		 H.DT_DUE,
		 H.YN_JEONJA,
		 (CASE WHEN @V_SERVER_KEY LIKE 'KPCI%' THEN L.CD_ITEM ELSE '' END), 
		 (CASE WHEN @V_SERVER_KEY LIKE 'HDS%' THEN POL.CD_USERDEF1 ELSE '' END),
		 (CASE WHEN @V_SERVER_KEY LIKE 'HDS%' THEN POL.CD_USERDEF2 ELSE '' END)
------------------------------------------------------------------------------------------------------------------------------------------------  -----------------------------------------------------------------------------------------------------       



                                    
-- 여기서 부터 전표처리 하기 위한 부분  ---                                                      
DECLARE @P_NO_DOCU NVARCHAR(20)        -- 전표번호                               
DECLARE @P_DT_PROCESS NVARCHAR(8)                                                      
DECLARE @V_CD_PC NVARCHAR(7)   
DECLARE @V_CD_ENV NVARCHAR(3)
DECLARE @V_NO_CARD NVARCHAR(20)
DECLARE @V_NM_CARD NVARCHAR(50)
-- 매출일자 알아오기                                                      
SELECT @P_DT_PROCESS = DT_PROCESS                                                      
FROM PU_IVH                                                      
WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV

-- 선용 매입 카드번호 조회
IF @P_CD_COMPANY = 'K100'
BEGIN
	SELECT TOP 1 @V_NO_CARD = FC.NO_CARD, 
	             @V_NM_CARD = FC.NM_CARD
	FROM PU_IVH IH
	JOIN (SELECT IL.CD_COMPANY, IL.NO_IV, IL.NO_PO 
		  FROM PU_IVL IL
	      GROUP BY IL.CD_COMPANY, IL.NO_IV, IL.NO_PO) IL 
	ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IV = IH.NO_IV
	JOIN PU_POH PH ON PH.CD_COMPANY = IL.CD_COMPANY AND PH.NO_PO = IL.NO_PO
	JOIN (SELECT C_CODE,
				 ADMIN_NO,
		  	     MAX(ACCT_NO) AS ACCT_NO
		  FROM CARD_TEMP
		  GROUP BY C_CODE, ADMIN_NO) CT
	ON CT.C_CODE = PH.CD_COMPANY AND CT.ADMIN_NO = PH.NO_ORDER
	JOIN (SELECT CD_COMPANY, REPLACE(NO_CARD, '-', '') AS ACCT_NO, NO_CARD, NM_CARD
	      FROM FI_CARD) FC
	ON FC.CD_COMPANY = CT.C_CODE AND FC.ACCT_NO = CT.ACCT_NO
	WHERE IH.CD_COMPANY = @P_CD_COMPANY
	AND IH.NO_IV = @P_NO_IV
	AND IH.CD_PARTNER = '20505'
	AND ISNULL(PH.NO_ORDER, '') <> ''
END
                                                      
/*   2009.12.04 두ROW 이상이 올 수 있어서 본SQL로 이동                                             
-- 매입형태명 가져오기                    
SELECT @P_NM_TP = AH.NM_TP                               
  FROM MA_AISPOSTH AH                               
  INNER JOIN PU_IVL PL ON                                                  
       AH.CD_COMPANY = PL.CD_COMPANY AND                                                   
       AH.CD_TP    = PL.FG_TPPURCHASE                                                    
 WHERE PL.CD_COMPANY = @P_CD_COMPANY AND AH.FG_TP = '200' AND PL.NO_IV = @P_NO_IV                                                     
 GROUP BY NM_TP                                                     
*/                              
                                                
-- 전표번호 채번                                                      
--EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT                                                    

SELECT TOP 1 @V_CD_ENV = CD_ENV FROM MA_ENVD WHERE TP_ENV = 'TP_NODOCU' AND CD_COMPANY = @P_CD_COMPANY

IF @V_CD_ENV = '1'
BEGIN

	SELECT @V_CD_PC = CASE WHEN @V_CD_EXC = '100' THEN B2.CD_PC ELSE B.CD_PC END
	FROM PU_IVH H
	INNER JOIN PU_IVL L ON                                                   
                         H.NO_IV  = L.NO_IV AND                                                   
                         H.CD_COMPANY = L.CD_COMPANY  
    INNER JOIN MA_AISPOSTL A  ON                                              
						 L.FG_TPPURCHASE = A.CD_TP  AND                                       
                         L.CD_COMPANY = A.CD_COMPANY                            
	INNER JOIN MA_BIZAREA B ON                                                   
							 H.CD_BIZAREA_TAX = B.CD_BIZAREA  AND                                    
							 H.CD_COMPANY = B.CD_COMPANY   
	INNER JOIN MA_BIZAREA B2 ON                                                       
						 H.CD_BIZAREA = B2.CD_BIZAREA  AND                                                       
						 H.CD_COMPANY = B2.CD_COMPANY 
	WHERE                                    
	H.NO_IV = @P_NO_IV                                                      
	AND H.CD_COMPANY = @P_CD_COMPANY                                                      
	AND H.TP_AIS ='N'  -- 전표발생여부                                                      
	AND H.YN_PURSUB ='N' -- 구매    					         
	AND A.FG_TP ='200' -- 매입형태                                                      
    AND A.FG_AIS =  (CASE H.FG_TRANS WHEN '001' THEN '211' WHEN '002' THEN '212' WHEN '003' THEN '212'  ELSE '211' END )                                              
    AND L.AM_CLS <> 0  
	-- 전표번호회계단위채번
	EXEC UP_FI_DOCU_CREATE_SEQ4 @P_CD_COMPANY, @V_CD_PC, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT

	SELECT @P_NO_DOCU = MAX(NO_SEQ2)
	FROM MA_AUTOSEQ_NO2 
	WHERE NO_AUTOSEQ = 'FI01'
	AND CD_COMPANY = @P_CD_COMPANY
	AND CD_PC = @V_CD_PC
	AND DT_DAY = @P_DT_PROCESS

END
ELSE
BEGIN
	-- 전표번호회사단위채번
	EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT
END

SET @V_NO_ACCT = 0
SET @V_ST_DOCU = '1'
SET @V_ID_ACCEPT = NULL
                                                      
DECLARE @P_NO_DOLINE INT                                                     
SET @P_NO_DOLINE = 0                              
                                                      
OPEN CUR_PU_IV_MNG                                                      
                                                      
FETCH NEXT FROM CUR_PU_IV_MNG INTO @IN_CD_COMPANY,@IN_NO_MDOCU,@IN_CD_PARTNER                                                  
,@IN_CD_CC,@IN_NM_CC,@IN_CD_PJT,@IN_ID_WRITE,@IN_CD_BIZAREA,@IN_TP_TAX,@IN_FG_TRANS,@IN_CD_WDEPT,                                                  
@IN_DT_ACCT,@IN_CD_PC,@IN_CD_ACCT,@IN_AM_DR,@IN_AM_CR,@IN_AM_SUPPLY,@IN_TP_DRCR,@IN_CD_EXCH                                                  
,@IN_RT_EXCH,  @IN_CD_EMPLOY,@IN_AM_EXDO,@IN_NM_BIZAREA,@IN_NM_KOR,                                                  
@IN_LN_PARTNER,@IN_NM_PROJECT,@IN_NM_DEPT,@IN_TYPE, @IN_CD_RELATION, @NO_BIZAREA,                             
@NM_TAX, @IN_NO_COMPANY, @IN_TP_ACAREA ,                                  
@CD_DOCU,@DT_PAY_PREARRANGED, @CD_FUND,@CD_BGACCT,@CD_BUDGET ,@P_NM_TP,@FG_PAYBILL,@NO_LC,@DT_DUE,
@P_YN_ISS, @V_VAT_TAX, @V_NO_PO_MAX , @V_NM_ITEM_MAX, @V_UM, @V_QT, @V_CD_ITEM_MAX, @V_CD_BIZPLAN, @V_AM_BUDGET,
@V_USER_ONLY, @V_USER_ONLY2                                              
                                                  
WHILE @@FETCH_STATUS = 0                                                      
BEGIN                                                      
                                       
SET @P_NO_DOLINE = @P_NO_DOLINE + 1                                                      
                                                  
/* 계정과목이 등재되어 있는지 체크하는 구문 */                               
IF( @IN_CD_ACCT IS NULL OR @IN_CD_ACCT = '')                                                  
BEGIN                                                  
        SELECT @ERRNO  = 100000,                                                    
                   @ERRMSG = '계정과목이 누락되었습니다. 확인하십시요'                                                    
       GOTO ERROR                                                    
END                                   
 --EXEC UP_PU_IV_MNG_TRANS_DOCU '0327', 'PTX20121100001', '210', '001'                                  
IF @IN_CD_RELATION = '30' -- 연동 항목이 '일반' 이 아니면 NO_MDOCU값 넣어준다.                                                  
SET @V_IN_NO_MDOCU = @IN_NO_MDOCU                                                   
ELSE         
SET @V_IN_NO_MDOCU = NULL                                                  
                                                     
SELECT @P_NOTE = @IN_LN_PARTNER + ' ' + @P_NM_TP  

DECLARE @V_CD_EXC_NOTE NVARCHAR(3)

SELECT  @V_CD_EXC_NOTE = CD_EXC 
FROM    MA_EXC 
WHERE   CD_COMPANY = @P_CD_COMPANY 
AND     EXC_TITLE = '메뉴별적요 사용 여부'

IF ISNULL(@V_CD_EXC_NOTE,'N') = 'Y'
BEGIN
	SET @P_NOTE      = NULL   
	SELECT @P_NOTE = @IN_LN_PARTNER + ' ' + @P_NM_TP
		
	EXEC UP_PU_NOTE_SETTING_S @P_CD_COMPANY, @P_NO_IV, 'P_PU_IV_MNG', @P_NOTE OUTPUT   
END

IF @IN_TYPE = 'A'
BEGIN
   IF @IN_TP_TAX IN ( '22','50')   -- 불공제일때 
   BEGIN
      SET @IN_AM_DR = @IN_AM_DR + @V_VAT_TAX
   END
END
ELSE IF @IN_TYPE = 'V'  -- 부가세일때
BEGIN
PRINT  @IN_TYPE  
   IF @IN_TP_TAX IN ( '22','50')   -- 불공제일때 
   BEGIN
      SET @IN_AM_DR = 0
   END
END            
--PRINT  @IN_TYPE                     
--PRINT @IN_AM_DR                                
                               
IF @V_SERVER_KEY = 'LOCL'  OR --개발          
   @V_SERVER_KEY = 'DZSQL' OR --개발          
   @V_SERVER_KEY = 'DSIC2' OR --대신정보 표준적요:거래처 수주유형 (품목명) 20100701     
   @V_SERVER_KEY = 'YEST'     --예스티 표준적요:거래처 수주유형 (품목명) 20110401    
BEGIN          
 SELECT @P_NOTE = ISNULL(@IN_LN_PARTNER,'') + ' '+ISNULL(PT.NM_TPPO,'')+' (' + ISNULL(MP.NM_ITEM,'') + ')'          
   FROM PU_IVL L INNER JOIN MA_PITEM MP          
     ON L.CD_COMPANY = MP.CD_COMPANY          
    AND L.CD_PLANT   = MP.CD_PLANT          
    AND L.CD_ITEM    = MP.CD_ITEM           
     LEFT JOIN PU_POH PH          
  ON L.CD_COMPANY = PH.CD_COMPANY          
    AND L.NO_PO      = PH.NO_PO          
     LEFT JOIN PU_TPPO PT          
  ON PH.CD_COMPANY = PT.CD_COMPANY          
    AND PH.CD_TPPO    = PT.CD_TPPO           
    WHERE L.CD_COMPANY = @P_CD_COMPANY AND L.NO_IV = @P_NO_IV                                     
      AND L.NO_LINE = 1                 
END                 
           
--SELECT @DT_PAY_PREARRANGED = CASE WHEN  @DT_PAY_PREARRANGED IS NULL THEN @P_DT_PROCESS ELSE @DT_PAY_PREARRANGED END                                     
SELECT @NM_EXCH = NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_SYSDEF = @IN_CD_EXCH AND CD_FIELD = 'MA_B000005'  
--SET @DT_DUE = ISNULL(@DT_DUE, @DT_PAY_PREARRANGED)  

-- 아이리버  매입전표처리시 국내거래인데 외화일경우 부가세계정 관리항목에 외화*0.1 해서 외화부가세 넣어달라고 허정민 요청
IF @V_SERVER_KEY = 'IRIVER' OR @V_SERVER_KEY = 'DZSQL1'
BEGIN
     
     SELECT @RT_VAT = RT_VAT FROM V_PU_MA_B000046 WHERE CD_COMPANY = @P_CD_COMPANY AND CD_SYSDEF = @IN_TP_TAX  AND CD_FIELD = 'MA_B000046'     
     SET @RT_VAT = ISNULL(@RT_VAT,0)
    IF @IN_TYPE = 'A'
    BEGIN
       
       SET @AM_EX = @IN_AM_EXDO
       SET @CD_EXCH  = @IN_CD_EXCH
    END
    ELSE IF @IN_TYPE = 'V'
    BEGIN
		IF @IN_FG_TRANS = '001' AND @CD_EXCH <> '000' 
		BEGIN	
			SET @IN_AM_EXDO	= @AM_EX * @RT_VAT
		END       
    END  
     ELSE --IF @IN_TYPE = 'T'
           SET @IN_AM_EXDO	= @AM_EX * (1+@RT_VAT)
END



-- 한국AVL 매입번호 A22에 넣어달라고..
IF @V_SERVER_KEY = 'DZSQL' OR
	 @V_SERVER_KEY = 'SQL_108' OR
	 @V_SERVER_KEY = 'KORAVL' 
BEGIN    
  SET @V_CD_UMNG2 = @IN_NO_MDOCU
END



---젬백스앤카엘 
IF @V_SERVER_KEY = 'JAMBAK' 
BEGIN
	SET @P_NOTE = @IN_LN_PARTNER + ' ' + @V_NO_PO_MAX + '##' + '(' + @V_NM_ITEM_MAX + ') 외'
END

-- 제이씨코리아 표준적요:거래처 수주유형 (품목명) 20130221   
------------------------------------------------------------------------------------------------------------                          
IF @V_SERVER_KEY = 'JCK'     -- 제이씨코리아표준적요:거래처수주유형(품목명) 20130219      
BEGIN            
 SELECT @P_NOTE = ISNULL(@IN_LN_PARTNER,'') + ' ['+ISNULL(CDTL.NM_SYSDEF,'')+'] 품목:' + ISNULL(MP.NM_ITEM,'') + ' 외'            
   FROM PU_IVL L INNER JOIN MA_PITEM MP            
     ON L.CD_COMPANY = MP.CD_COMPANY            
    AND L.CD_PLANT   = MP.CD_PLANT            
    AND L.CD_ITEM    = MP.CD_ITEM             
     LEFT JOIN PU_POH PH            
  ON L.CD_COMPANY = PH.CD_COMPANY            
    AND L.NO_PO      = PH.NO_PO            
     LEFT JOIN PU_TPPO PT            
  ON PH.CD_COMPANY = PT.CD_COMPANY            
    AND PH.CD_TPPO    = PT.CD_TPPO        
     LEFT JOIN MA_CODEDTL CDTL   
  ON L.CD_COMPANY= CDTL.CD_COMPANY AND MP.CLS_M  = CDTL.CD_SYSDEF
     AND CDTL.CD_FIELD = 'MA_B000031'    
    WHERE L.CD_COMPANY = @P_CD_COMPANY AND L.NO_IV = @P_NO_IV                                       
      AND L.NO_LINE = 1            
               
END                              

-- 서호전기 전표처리시 프로젝트명_거래처명_품목명(TOP1)
------------------------------------------------------------------------------------------------------------
IF @V_SERVER_KEY = 'SUHO' OR @V_SERVER_KEY LIKE 'DAEYONG%'  --서호전기
BEGIN
	SELECT @P_NOTE = ISNULL(PJT.NM_PROJECT,'') + '_'+ ISNULL(@IN_LN_PARTNER,'')+ '_' + ISNULL(MP.NM_ITEM,'')
      FROM PU_IVL L
INNER JOIN MA_PITEM MP ON L.CD_COMPANY = MP.CD_COMPANY AND L.CD_PLANT = MP.CD_PLANT AND L.CD_ITEM = MP.CD_ITEM
LEFT  JOIN SA_PROJECTH PJT ON L.CD_PJT = PJT.NO_PROJECT AND L.CD_COMPANY = PJT.CD_COMPANY
       AND PJT.NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE CD_COMPANY = PJT.CD_COMPANY AND NO_PROJECT = PJT.NO_PROJECT)
     WHERE L.CD_COMPANY = @P_CD_COMPANY
       AND L.NO_IV = @P_NO_IV
       AND L.NO_LINE = 1
END
------------------------------------------------------------------------------------------------------------

-- 인텍전기전자전용 적요 D20130911023 
IF @V_SERVER_KEY = 'ENTEC'
BEGIN
	SELECT @P_NOTE = @P_NM_TP   
END

------------------------------------------------------------------------------------------------------------

IF @V_SERVER_KEY LIKE 'INTERBEX%' OR @V_SERVER_KEY LIKE 'KEUNDAN%' -- 인텍벡스테크놀로지 적요 D20150311 (고려은단도 같은 적요로직 (20160215))
BEGIN
 DECLARE @V_DC_RMK NVARCHAR(400)
 
 SELECT @V_DC_RMK = ISNULL(DC_RMK,'')
 FROM PU_IVH
 WHERE CD_COMPANY = @P_CD_COMPANY
 AND   NO_IV = @P_NO_IV
 
 IF @V_DC_RMK <> '' AND @V_DC_RMK IS NOT NULL
 BEGIN
	SET @P_NOTE = @V_DC_RMK
 END 
END       
------------------------------------------------------------------------------------------------------------
IF @V_SERVER_KEY LIKE 'DONGAH%' -- 동아공업 적요 & 품의  D20150311 
BEGIN
 SELECT @P_NOTE = @IN_LN_PARTNER + ' ' + @P_NM_TP 
 SET @V_NM_PUMM = @P_NOTE      
END 

-- (주)선경홀로그램 전표처리시 거래처명_전표유형_품목명(TOP1)_매입금액  
------------------------------------------------------------------------------------------------------------  
IF @V_SERVER_KEY LIKE 'SKHLG%' -- 선경홀로그램
BEGIN  
 SELECT @P_NOTE = @IN_LN_PARTNER +'_' + ISNULL(CDTL.NM_SYSDEF,'') +'_' + ISNULL(MP.NM_ITEM,'') + '_' +CONVERT(NVARCHAR(17), H.AM_K + H.VAT_TAX )
      FROM PU_IVL L  
INNER JOIN PU_IVH H ON L.NO_IV = H.NO_IV AND L.CD_COMPANY = H.CD_COMPANY      
INNER JOIN MA_PITEM MP     ON L.CD_COMPANY = MP.CD_COMPANY  AND L.CD_PLANT = MP.CD_PLANT       AND L.CD_ITEM = MP.CD_ITEM  
LEFT  JOIN PU_POH PH       ON L.CD_COMPANY = PH.CD_COMPANY  AND L.NO_PO      = PH.NO_PO       
LEFT  JOIN PU_TPPO PT      ON PH.CD_COMPANY = PT.CD_COMPANY AND PH.CD_TPPO    = PT.CD_TPPO     
LEFT  JOIN MA_CODEDTL CDTL ON L.CD_COMPANY  = CDTL.CD_COMPANY AND H.CD_DOCU  = CDTL.CD_SYSDEF   AND CDTL.CD_FIELD = 'FI_J000002'      
     WHERE L.CD_COMPANY = @P_CD_COMPANY  
       AND L.NO_IV = @P_NO_IV  
       AND L.NO_LINE = 1
END  

DECLARE @V_NM_ITEM   NVARCHAR(200)


SELECT @V_NM_ITEM = ''

SELECT TOP 1 @V_NM_ITEM = ITEM.NM_ITEM

  FROM PU_IVL L 
  INNER JOIN MA_PITEM ITEM ON L.CD_ITEM = ITEM.CD_ITEM AND L.CD_PLANT = ITEM.CD_PLANT AND L.CD_COMPANY = ITEM.CD_COMPANY
  WHERE L.NO_IV = @P_NO_IV AND L.CD_COMPANY = @P_CD_COMPANY
  
-----------------------------------------------------------------------------------------------------------------
  IF @V_SERVER_KEY LIKE 'SIMMONS%' --시몬스 적요
  BEGIN
	IF @IN_CD_RELATION = '30'
	BEGIN
	   SELECT @P_NOTE = @IN_DT_ACCT + ' ' + '부가세대급금' + ' ' + @IN_LN_PARTNER	
	END
	ELSE
	BEGIN
		SELECT @P_NOTE = ISNULL(@V_NM_ITEM,'') + ' ' + @IN_LN_PARTNER + ' ' + @P_NM_TP 
	END 
 
  END
----------------------------------------------------------------------------------------------------------------- 
  IF @V_SERVER_KEY LIKE 'KPCI%' --케이피아이씨코포레이션 적요 
  BEGIN
    SET @V_USER_ONLY = ''
      
	IF @IN_TYPE = 'V'
	BEGIN
	  SELECT    @P_NOTE = ISNULL(MP.STND_ITEM,'') + ' ' + ISNULL(MP.STND_DETAIL_ITEM,'') + ' ' + CASE WHEN CEILING(@V_QT) = @V_QT THEN CONVERT(NVARCHAR,CEILING(@V_QT)) ELSE CONVERT(NVARCHAR,@V_QT)END + ISNULL(MP.UNIT_IM,'') + ' ' + @NM_EXCH + ' ' + CAST(CASE WHEN @NM_EXCH = 'KRW' THEN CONVERT(NUMERIC(17,0), L.AM_EX_CLS) ELSE CONVERT(NUMERIC(17,2), L.AM_EX_CLS) END AS NVARCHAR) + ' ' + ISNULL(CH.NO_ORDER,'')
	                      + ' ' + ISNULL(CD1.NM_SYSDEF,''),
				@V_USER_ONLY = ISNULL(CH.NO_ORDER,'')
		FROM    PU_IVL L
				INNER JOIN MA_PITEM MP ON L.CD_COMPANY = MP.CD_COMPANY AND L.CD_PLANT = MP.CD_PLANT AND L.CD_ITEM = MP.CD_ITEM
				LEFT  JOIN PU_POL SL ON L.CD_COMPANY = SL.CD_COMPANY AND L.NO_PO = SL.NO_PO AND L.NO_POLINE = SL.NO_LINE
				LEFT  JOIN SA_Z_KPCI_CONTRACT_H CH ON SL.CD_COMPANY = CH.CD_COMPANY AND SL.NO_RELATION = CH.NO_CONTRACT 
				LEFT OUTER JOIN MA_CODEDTL CD1 ON MP.CD_COMPANY = CD1.CD_COMPANY AND CD1.CD_FIELD = 'MA_B000066' AND MP.GRP_MFG = CD1.CD_SYSDEF 
		WHERE   L.CD_COMPANY = @P_CD_COMPANY 
		AND     L.NO_IV      = @P_NO_IV
		AND     L.CD_ITEM    = @V_CD_ITEM_MAX
    END
    ELSE
    BEGIN
      SELECT    @P_NOTE = ISNULL(MP.STND_ITEM,'') + ' ' + ISNULL(MP.STND_DETAIL_ITEM,'') + ' ' + CASE WHEN CEILING(@V_QT) = @V_QT THEN CONVERT(NVARCHAR,CEILING(@V_QT)) ELSE CONVERT(NVARCHAR,@V_QT)END + ISNULL(MP.UNIT_IM,'') + ' ' + @NM_EXCH + ' ' + CAST(CASE WHEN @NM_EXCH = 'KRW' THEN CONVERT(NUMERIC(17,0), @IN_AM_EXDO) ELSE CONVERT(NUMERIC(17,2), @IN_AM_EXDO) END AS NVARCHAR) + ' ' + ISNULL(CH.NO_ORDER,'')
						  + ' ' + ISNULL(CD1.NM_SYSDEF,''),
                @V_USER_ONLY = ISNULL(CH.NO_ORDER,'')
        FROM    PU_IVL L
                INNER JOIN MA_PITEM MP ON L.CD_COMPANY = MP.CD_COMPANY AND L.CD_PLANT = MP.CD_PLANT AND L.CD_ITEM = MP.CD_ITEM
                LEFT  JOIN PU_POL SL ON L.CD_COMPANY = SL.CD_COMPANY AND L.NO_PO = SL.NO_PO AND L.NO_POLINE = SL.NO_LINE
                LEFT  JOIN SA_Z_KPCI_CONTRACT_H CH ON SL.CD_COMPANY = CH.CD_COMPANY AND SL.NO_RELATION = CH.NO_CONTRACT  
                LEFT OUTER JOIN MA_CODEDTL CD1 ON MP.CD_COMPANY = CD1.CD_COMPANY AND CD1.CD_FIELD = 'MA_B000066' AND MP.GRP_MFG = CD1.CD_SYSDEF 
        WHERE   L.CD_COMPANY = @P_CD_COMPANY 
        AND     L.NO_IV      = @P_NO_IV
        AND     L.CD_ITEM    = @V_CD_ITEM_MAX
    END

  END
----------------------------------------------------------------------------------------------------------------- 
IF @V_SERVER_KEY LIKE 'SLFIRE%' --신라파이어 적요 
BEGIN
	DECLARE @V_CNT		    INT,
		    @V_NM_CLS_ITME	NVARCHAR(200)
	        
	SELECT TOP 1 @V_CNT = MAX(A.CNT),
				 @V_CD_ITEM_MAX = L.CD_ITEM,
				 @V_NM_ITEM_MAX = P.NM_ITEM,
				 @V_NM_CLS_ITME = D.NM_SYSDEF
	  FROM PU_IVL L
	  INNER JOIN MA_PITEM P ON L.CD_ITEM = P.CD_ITEM AND L.CD_PLANT = P.CD_PLANT AND L.CD_COMPANY = P.CD_COMPANY
	  INNER JOIN MA_CODEDTL D ON D.CD_FIELD = 'MA_B000010' AND D.CD_SYSDEF = P.CLS_ITEM AND D.CD_COMPANY = P.CD_COMPANY
	  INNER JOIN ( SELECT COUNT(DISTINCT CD_ITEM) CNT FROM PU_IVL WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV)A
	  ON 1=1
	 WHERE L.CD_COMPANY = @P_CD_COMPANY
	   AND L.NO_IV = @P_NO_IV
  GROUP BY L.CD_ITEM, P.NM_ITEM, D.NM_SYSDEF
  
  SELECT @P_NOTE = CASE WHEN @V_CNT > 1 THEN @V_NM_ITEM_MAX + ' 외_' + @IN_LN_PARTNER + '_' + @P_NM_TP + '_' + @V_NM_CLS_ITME + ' ' 
			       ELSE @V_NM_ITEM_MAX + '_' + @IN_LN_PARTNER + '_' + @P_NM_TP + '_' + @V_NM_CLS_ITME END 
  
END
-----------------------------------------------------------------------------------------------------------------  
IF @V_SERVER_KEY LIKE 'WGBNG%' --우진비앤지 적요 
BEGIN
	
	DECLARE @IN_FG_TAXP NVARCHAR(3)
	
	SELECT @IN_FG_TAXP = FG_TAXP FROM PU_IVH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV
	
	IF EXISTS (        
				SELECT 1 FROM PU_IVH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV
				AND FG_TAX = '23' AND FG_TRANS = '001' AND FG_TAXP = '001'
	)
	BEGIN
		SELECT @P_NOTE = ISNULL(P.NM_ITEM, '') + '[' + ISNULL(P.UNIT_IM, '') + ', ' + REPLACE(CONVERT(NVARCHAR,CAST(CEILING(ISNULL(L.QT_CLS, 0)) AS MONEY), 1), '.00', '') + ' * ' + REPLACE(CONVERT(NVARCHAR,CAST(CEILING((L.AM_CLS/ISNULL(L.QT_CLS, 1)))AS MONEY), 1), '.00', '') + ']'
		FROM PU_IVL L
		INNER JOIN MA_PITEM P ON L.CD_ITEM = P.CD_ITEM AND L.CD_PLANT = P.CD_PLANT AND L.CD_COMPANY = P.CD_COMPANY
		WHERE L.CD_COMPANY = @P_CD_COMPANY
		AND   L.NO_IV = @P_NO_IV
		AND   L.NO_LINE = 1
	END
END

-----------------------------------------------------------------------------------------------------------------  
IF @V_SERVER_KEY LIKE 'GDC%' --갤럭시아디바이스 적요 
BEGIN
	DECLARE @V_MAX_NM_ITEM NVARCHAR(200)
	
	BEGIN
		SELECT @V_MAX_NM_ITEM = B.NM_ITEM
		FROM
		(
			SELECT P.NM_ITEM, ROW_NUMBER() OVER (PARTITION BY L.NO_IV ORDER BY L.AM_CLS DESC) AS RANKS
			FROM PU_IVL L
			INNER JOIN MA_PITEM P ON L.CD_ITEM = P.CD_ITEM AND L.CD_PLANT = P.CD_PLANT AND L.CD_COMPANY = P.CD_COMPANY
			WHERE L.CD_COMPANY = @P_CD_COMPANY
			AND   L.NO_IV = @P_NO_IV
		)B
		WHERE B.RANKS = 1
	END

	BEGIN
		SELECT @P_NOTE = @IN_LN_PARTNER + ', ' + ISNULL(@V_MAX_NM_ITEM, '') + ' 외 ' + CONVERT(NVARCHAR, COUNT(L.CD_ITEM) - 1) + '건'
		FROM PU_IVL L
		INNER JOIN MA_PITEM P ON L.CD_ITEM = P.CD_ITEM AND L.CD_PLANT = P.CD_PLANT AND L.CD_COMPANY = P.CD_COMPANY
		WHERE L.CD_COMPANY = @P_CD_COMPANY
		AND   L.NO_IV = @P_NO_IV
		GROUP BY L.NO_IV
	END
END

-----------------------------------------------------------------------------------------------------------------

IF @V_SERVER_KEY = 'GIGAVIS'
BEGIN
	SELECT @DT_PAY_PREARRANGED = ''
	SELECT @DT_DUE = '00000000'   
END

IF @V_CD_EXC_MENU = '001' AND ISNULL(@P_ID_USER,'') <> ''
BEGIN
	SELECT @IN_ID_WRITE = @P_ID_USER
	SELECT @IN_CD_WDEPT = E.CD_DEPT, 
	       @IN_NM_DEPT  = D.NM_DEPT
	  FROM MA_EMP E INNER JOIN MA_DEPT D ON E.CD_DEPT = D.CD_DEPT AND E.CD_COMPANY = D.CD_COMPANY 
	 WHERE E.CD_COMPANY = @IN_CD_COMPANY 
	   AND E.NO_EMP = @P_ID_USER
END 
-----------------------------------------------------------------------------------------------------------------
/* CD_UMNG 세팅 START */   
DECLARE @V_CD_UMNG1  NVARCHAR(10)       
DECLARE @V_CD_UMNG3  NVARCHAR(10)
    SET @V_CD_UMNG1 = ''
    SET @V_CD_UMNG3 = ''

IF @V_SERVER_KEY LIKE 'DOMINO%'
BEGIN
	SET @CD_BUDGET = '10201000'
	SET @CD_BGACCT = '101700'

	IF EXISTS (        
	  SELECT 1        
		FROM FI_ACCTCODE        
	   WHERE CD_COMPANY = @IN_CD_COMPANY        
		 AND CD_ACCT = @IN_CD_ACCT        
		 AND (   CD_MNG1 = 'A21' OR CD_MNG2 = 'A21' OR CD_MNG3 = 'A21' OR CD_MNG4 = 'A21'        
			  OR CD_MNG5 = 'A21' OR CD_MNG6 = 'A21' OR CD_MNG7 = 'A21' OR CD_MNG8 = 'A21')        
			)        
	BEGIN        
		SET @V_CD_UMNG1 = '01';        
	END        
	           
	IF EXISTS (        
	  SELECT 1        
		FROM FI_ACCTCODE        
	   WHERE CD_COMPANY = @IN_CD_COMPANY        
		 AND CD_ACCT = @IN_CD_ACCT        
		 AND (   CD_MNG1 = 'A23' OR CD_MNG2 = 'A23' OR CD_MNG3 = 'A23' OR CD_MNG4 = 'A23'        
			  OR CD_MNG5 = 'A23' OR CD_MNG6 = 'A23' OR CD_MNG7 = 'A23' OR CD_MNG8 = 'A23')        
			 )        
	BEGIN        
		SET @V_CD_UMNG3 = '01';        
	END
  
/* CD_UMNG 세팅 END */ 
END

IF @V_SERVER_KEY LIKE 'UNIPOINT%'
BEGIN
	
	SELECT TOP 1 @V_CD_UMNG1 = PH.CD_TPPO FROM PU_IVL L INNER JOIN PU_POH PH 
	ON L.NO_PO = PH.NO_PO AND L.CD_COMPANY = PH.CD_COMPANY 
	WHERE L.CD_COMPANY = @P_CD_COMPANY AND L.NO_IV = @P_NO_IV
	
	DECLARE @SQL NVARCHAR(1000)
	DECLARE @param NVARCHAR(100)
	DECLARE @return_value NVARCHAR(50)

	SET @param = '@param_output NVARCHAR(50) OUTPUT';
		 
	SET @SQL  = 'SELECT  TOP 1 @param_output = LN_PARTNER FROM WP_SFA_PFLSH_H WP ' + 
				'INNER JOIN MA_PARTNER MP ON WP.CD_PARTNER_END = MP.CD_PARTNER AND WP.CD_COMPANY = MP.CD_COMPANY ' +
				'WHERE	WP.NO_PROJECT = ''' + @IN_CD_PJT + ''' AND WP.CD_COMPANY = ''' +@P_CD_COMPANY + ''''
	EXEC SP_EXECUTESQL @SQL, @param, @return_value OUTPUT
	SET @P_NOTE =  @return_value + ' ' + @IN_NM_PROJECT
	
	IF @IN_CD_EXCH = '000' -- 환종이 '000'이면 금액을 0으로 표기한다(황경인)
	BEGIN
		SET @IN_AM_EXDO = 0.0;
	END
	
END

IF @V_SERVER_KEY = 'SINJINSM' 

BEGIN
	DECLARE @V_NO_APP_SET		NVARCHAR(800)   
	DECLARE	@V_NO_APP			NVARCHAR(20)
	DECLARE @CNT_APP			CHAR(1)
	
	SET @V_NO_APP_SET = ''
	SET @V_NO_APP = ''
	SET @CNT_APP = 'Y'
	SET @V_USER_ONLY = ''
	
	DECLARE CUR_PU_APPL CURSOR FOR   

	SELECT APPL.NO_APP FROM PU_IVL IVL
	INNER JOIN PU_POL POL ON IVL.NO_PO = POL.NO_PO AND IVL.NO_POLINE = POL.NO_LINE AND IVL.CD_COMPANY = POL.CD_COMPANY
	INNER JOIN PU_APPL APPL ON POL.NO_APP = APPL.NO_APP AND POL.NO_APPLINE = APPL.NO_APPLINE AND POL.CD_COMPANY = APPL.CD_COMPANY
	WHERE IVL.CD_COMPANY  = @P_CD_COMPANY
	AND	  IVL.NO_IV = @P_NO_IV	
	GROUP BY APPL.NO_APP

	OPEN CUR_PU_APPL

	-- 커서에서 데이터 가져오기 (반복)
	FETCH NEXT FROM CUR_PU_APPL INTO @V_NO_APP
	-- 데이터 처리 (반복)
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		IF @CNT_APP = 'Y'
		BEGIN
			SELECT @V_NO_APP_SET = @V_NO_APP
			SELECT @CNT_APP = 'N'
		END
		ELSE
		SELECT @V_NO_APP_SET = @V_NO_APP_SET + ','+ @V_NO_APP
		
		
		FETCH NEXT FROM CUR_PU_APPL INTO @V_NO_APP
		
	END
	-- 커서 닫기
	CLOSE CUR_PU_APPL
	-- 해제
	DEALLOCATE CUR_PU_APPL

	SELECT @V_USER_ONLY = SUBSTRING(@V_NO_APP_SET,1,50)
	
END

IF @V_SERVER_KEY = 'DYPNF' --동양 PNF 적요
BEGIN
 SET @P_NOTE =	(SELECT TOP 1 PH.NO_ORDER + ' : ' +'(' + SUBSTRING(H.DT_PROCESS,1,4) + '/' + SUBSTRING(H.DT_PROCESS,5,2) + '/' + SUBSTRING(H.DT_PROCESS,7,2)  + ')'
				  FROM PU_IVH H
				  INNER JOIN PU_IVL L ON H.NO_IV = L.NO_IV AND H.CD_COMPANY = L.CD_COMPANY
				  INNER JOIN PU_POH PH ON PH.NO_PO = L.NO_PO AND PH.CD_COMPANY = L.CD_COMPANY
				  
				  WHERE H.CD_COMPANY = @P_CD_COMPANY
					AND H.NO_IV = @P_NO_IV)
END

-- 증빙 추가
SET @V_TP_EVIDENCE = (SELECT TP_EVIDENCE
					  FROM CZ_FI_ACCT_EVIDENCEL
					  WHERE CD_COMPANY = @IN_CD_COMPANY
					  AND FG_TAX = @IN_TP_TAX
					  AND CD_ACCT = @IN_CD_ACCT)

IF @P_CD_COMPANY = 'W100'
BEGIN
	SET @V_NM_PUMM = @P_NOTE
END
ELSE
BEGIN
	IF EXISTS (SELECT 1
			   FROM PU_IVL IL
			   LEFT JOIN PU_POL PL ON PL.CD_COMPANY = IL.CD_COMPANY AND PL.NO_PO = IL.NO_PO AND PL.NO_LINE = IL.NO_POLINE
			   WHERE IL.CD_COMPANY = @P_CD_COMPANY
			   AND IL.NO_IV = @P_NO_IV
			   AND ISNULL(IL.YN_RETURN, 'N') = 'N'
			   AND ISNULL(PL.AM_EX_ADPAY, 0) > 0)
		SET @V_NM_PUMM = '선지급 금액이 있는 매입건 입니다.' + '/ 발주자 : ' + @IN_NM_KOR
	ELSE
		SET @V_NM_PUMM = '발주자 : ' + @IN_NM_KOR	
END

-----------------------------------------------------------------------------------------------------------------
EXEC UP_FI_AUTODOCU_1 @P_NO_DOCU,               -- 전표번호                                                      
					  @P_NO_DOLINE,                -- 라인번호                                                      
					  @IN_CD_PC,                  -- 회계단위                                      
					  @IN_CD_COMPANY,		-- 회사코드                                                                
					  @IN_CD_WDEPT,          -- 작성부서                                                      
					  @IN_ID_WRITE,          -- 작성자                                                      
					  @IN_DT_ACCT,           -- 매출일자 = 회계일자 = 처리일자                                                                    
					  @V_NO_ACCT,			-- 회계번호 미결이니까 0 NO_ACCT  (SKTS 서버키로 채번),     
					  '3',                   -- 전표구분-대체 TP_DOCU                                    
					  --'45',                -- 전표유형-일반 CD_DOCU    11->45 20080624                                                  
					  @CD_DOCU,				-- 전표유형 (2009.11.28  전표유형 추가 SMR (REQ KHS))        
					  @V_ST_DOCU,			--'1',       -- 전표상태-미결 ST_DOCU  (SKTS 서버키로 '2')                                                    
					  @V_ID_ACCEPT,          --NULL,      -- 승인자 (SKTS 서버키로 @P_ID_USER)
					  @IN_TP_DRCR,           -- 차대구분 TP_DRCR     
					  @IN_CD_ACCT,           -- 계정코드                                                      
					  @P_NOTE,               -- 적요                                                      
					  @IN_AM_DR,                        -- 차변금액 AM_DR                                                      
					  @IN_AM_CR,                      -- 대변금액 AM_CR                      
					  @IN_TP_ACAREA,                -- 본지점구분-안함 TP_ACAREA                                                      
					  @IN_CD_RELATION,        -- 연동항목-일반 CD_RELATION                                                        
					  @CD_BUDGET,                         -- 예산코드 CD_BUDGET                                                                
					  @CD_FUND,             -- 자금과목 CD_FUND                                                   
					  NULL,                                -- 원인전표번호 NO_BDOCU                                                                
					  NULL,                                -- 원인전표라인 NO_BDOLINE                                                                
					  '0',     -- 타대구분 TP_ETCACCT                                                  
					  @IN_CD_BIZAREA,         -- 귀속사업장                                                    
					  @IN_NM_BIZAREA,                                           
					  @IN_CD_CC,              -- 코스트센터                                                      
					  @IN_NM_CC,                             
					  @IN_CD_PJT,                -- 프로젝트                                                    
					  @IN_NM_PROJECT,                                                    
					  @IN_CD_WDEPT,         -- 부서                                                   
					  @IN_NM_DEPT,                                                     
					  @IN_CD_EMPLOY,        -- 사원 CD_EMPLOY                                                      
					  @IN_NM_KOR,                                                  
					  @IN_CD_PARTNER,       -- 거래처 CD_PARTNER                                                      
					  @IN_LN_PARTNER,                                                  
					  NULL,                           -- 예적금코드 CD_DEPOSIT                                                     
					  NULL, -- NM_DEPOSIT,                                                   
					  @V_NO_CARD,                           -- 카드번호 CD_CARD                                            
					  @V_NM_CARD, -- NM_CARD                                                      
					  NULL,                          -- 은행코드 CD_BANK  : MA_PARTNER에서 FG_PARTNER = '002' 인것                                                      
					  NULL, -- NM_BANK,                                                  
					  @V_CD_ITEM_MAX,                          -- 품목코드 NO_ITEM                                                        
					  @V_NM_ITEM_MAX, -- NM_ITEM,                                                  
					  @IN_TP_TAX,    -- 세무구분 TP_TAX                                                     
					  @NM_TAX,                                                  
					  NULL,                          -- 거래구분 CD_TRADE                                                       
					  NULL,                            -- NM_TRADE                                                  
					  @IN_CD_EXCH,          -- 환종        CD_EXCH                                    
					  @NM_EXCH, -- NM_EXCH,                                                  
					  @V_CD_UMNG1,                         -- CD_UMNG1                                                        
					  @V_CD_UMNG2,                         -- CD_UMNG2                                                        
					  @V_CD_UMNG3,                         -- CD_UMNG3                                                        
					  NULL,                         -- CD_UMNG4                
					  NULL,               -- CD_UMNG5                                                    
					  @IN_NO_COMPANY,   -- NO_RES                                                  
					  @IN_AM_SUPPLY,       --AM_SUPPLY                                                  
					  @V_IN_NO_MDOCU,    -- CD_MNG                                                  
--					  @IN_DT_ACCT,      -- 거래일자, 시작일자, 발생일자 DT_START                                                        
					  @DT_PAY_PREARRANGED, --ISNULL(@DT_PAY_PREARRANGED,@P_DT_PROCESS),    -- 자금예정일(지급예정일 2009.11.30 SMR)                                      
					  @DT_DUE,                        -- 만기일자 DT_END                                                                
					  @IN_RT_EXCH,          -- 환율        RT_EXCH                                                      
					  @IN_AM_EXDO,         -- 외화금액 AM_EXDO                                                       
					  '210',                           -- 모듈구분(매출:002) NO_MODULE                                                       
					  @IN_NO_MDOCU,      -- 모듈관리번호 = 타모듈pkey NO_MDOCU                                                        
					  @CD_BGACCT,          -- 지출결의코드 CD_EPNOTE  (지출결의에 예산계정코드를 입력해야 화면에 예산계정명을 볼 수 있다/지출결의와 함께 쓰기때문이란다)                        
					  @IN_ID_WRITE,        -- 전표처리자                                                      
					  @CD_BGACCT,                        -- 예산계정 CD_BGACCT                                                       
					  NULL,                        -- 결의구분 TP_EPNOTE                                                       
					  @V_NM_PUMM,                        -- 품의내역 NM_PUMM                                                        
					  @CUR_DATE, -- 현재일자로 20100506 @P_DT_PROCESS,    -- 작성일자 DT_WRITE                                                        
					  0,         -- AM_ACTSUM                                 
					  0,                             -- AM_JSUM                                                   
					  'N',                            -- YN_GWARE                                                        
					  @V_CD_BIZPLAN,                        -- 사업계획코드 CD_BIZPLAN                                                    
					  NULL,                          --CD_ETC                                                    
					  @P_ERRORCODE,                                                    
					  @P_ERRORMSG,                    
					  NULL,                    
					  @NO_LC,                    
					  @FG_PAYBILL,   -- 지급조건                    
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  @V_VAT_TAX,
					  NULL,
					  NULL,
					  NULL,  -- 90
					  NULL,
					  NULL,
					  NULL,
					  NULL,
					  @V_UM,
					  @V_QT,
					  @V_USER_ONLY,
					  0,
					  @V_USER_ONLY2,
					  @V_TP_EVIDENCE -- 증빙 TP_EVIDENCE               
                                        
  IF (@@ERROR <> 0 )                                                    
  BEGIN                                                      
     SELECT @ERRNO  = 100000,                                                      
      @ERRMSG = 'CM_M100010'                                              
     GOTO ERROR                                                      
  END                                               
                                                       
                                                  
 IF @IN_TYPE = 'V'      --추가 :김정근 20071010 부가세 추가로직                                                    
  BEGIN                                                  
   EXEC UP_FI_AUTODOCU_TAX @IN_CD_COMPANY, @IN_CD_PC, @P_NO_DOCU, @P_NO_DOLINE, @IN_AM_SUPPLY, @P_YN_ISS, 0, NULL, @V_VAT_TAX   
   
   EXEC UP_PU_IVMNG_TRANSFER_DOCU_TAX @P_CD_COMPANY, @P_NO_IV, @IN_CD_PC, @P_NO_DOCU, @P_NO_DOLINE, 
                                           @P_OPT_ITEM_RPT_GUBUN,     --내역표시구분            
                                           @P_OPT_ITEM_RPT_TEXT,      --내역표시임의내용        
                                           @P_OPT_ITEM_NM_GUBUN      --품목표시구분     

IF ISNULL(@V_NO_CARD, '') <> ''
BEGIN
	UPDATE FI_TAX 
	SET NO_CARD = @V_NO_CARD 
	WHERE NO_DOCU = @P_NO_DOCU         
	AND NO_DOLINE = @P_NO_DOLINE         
	AND CD_PC = @IN_CD_PC
	AND CD_COMPANY = @P_CD_COMPANY
END
                            
IF @V_SERVER_KEY LIKE 'SEMICS%' OR @V_SERVER_KEY LIKE 'SQL_%' OR @V_SERVER_KEY LIKE 'DZSQL%' -- 쎄믹스전용 현금승인번호                                                
BEGIN
	DECLARE @V_NO_CASH NVARCHAR(9)
	
	SET @V_NO_CASH = (SELECT TXT_USERDEF1 FROM PU_IVH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV)
	
	IF @V_NO_CASH IS NOT NULL AND NOT @V_NO_CASH = ''
	BEGIN
		UPDATE FI_TAX 
		SET NO_CASH = @V_NO_CASH 
		WHERE NO_DOCU  = @P_NO_DOCU    -- 전표번호         
		AND NO_DOLINE  = @P_NO_DOLINE  -- 라인번호         
		AND CD_PC      = @IN_CD_PC      -- 회계단위         
		AND CD_COMPANY = @P_CD_COMPANY -- 회사코드    
	END    
END       
                                                                     
  IF (@@ERROR <> 0 )                                                    
  BEGIN                                                      
     SELECT @ERRNO  = 100000,                                                      
      @ERRMSG = 'CM_M100010'                                                      
     GOTO ERROR                                 
  END  
  END     
    
                                                    
	IF (@P_NO_MODULE = '210') -- 회계전표유형 : 국내매입 이면                                                
	BEGIN                                                       
		UPDATE PU_IVH SET TP_AIS = 'Y' WHERE CD_COMPANY = @IN_CD_COMPANY AND NO_IV = @IN_NO_MDOCU                                                      
		IF (@@ERROR <> 0 )                                                    
		BEGIN                                                      
			SELECT @ERRNO  = 100000,                                                      
			@ERRMSG = 'CM_M100010'                                                      
			GOTO ERROR                                                      
		END                                              
	END                           
	
	IF (@V_BUDGET_EXC = 'Y' AND @IN_TYPE = 'A' AND @V_AM_BUDGET <> 0) /*예산 회계연동 사용*/
	BEGIN
		-- 예산     FI_GMMSUM_OTHER  마이너스행 생성
		INSERT INTO  FI_GMMSUM_OTHER(ROW_ID,ROW_NO,CD_COMPANY,CD_BUDGET,CD_BIZPLAN,CD_BGACCT,YM_ACCT,AM_ACTION,ID_INSERT)
		VALUES (@P_NO_DOCU, @P_NO_DOLINE, @P_CD_COMPANY, @CD_BUDGET, @V_CD_BIZPLAN, @CD_BGACCT, LEFT(@IN_DT_ACCT,6), @V_AM_BUDGET * -1, @P_ID_USER)
		IF (@@ERROR <> 0 )   
		BEGIN
			SELECT @ERRNO  = 100000,                                                      
			@ERRMSG = 'FI_GMMSUM_OTHER 회계예산 동기화중 에러가 발생했습니다'
			GOTO ERROR
		END
	END
	

--;
                                                  
FETCH NEXT FROM CUR_PU_IV_MNG INTO @IN_CD_COMPANY,@IN_NO_MDOCU,@IN_CD_PARTNER                
,@IN_CD_CC,@IN_NM_CC,@IN_CD_PJT,@IN_ID_WRITE,@IN_CD_BIZAREA,@IN_TP_TAX,@IN_FG_TRANS,@IN_CD_WDEPT,                                                  
@IN_DT_ACCT,@IN_CD_PC,@IN_CD_ACCT,@IN_AM_DR,@IN_AM_CR,@IN_AM_SUPPLY,@IN_TP_DRCR,@IN_CD_EXCH                                                  
,@IN_RT_EXCH,  @IN_CD_EMPLOY,@IN_AM_EXDO,@IN_NM_BIZAREA,@IN_NM_KOR,                                                    
@IN_LN_PARTNER,@IN_NM_PROJECT,@IN_NM_DEPT,@IN_TYPE, @IN_CD_RELATION, @NO_BIZAREA,                                        
@NM_TAX, @IN_NO_COMPANY, @IN_TP_ACAREA ,@CD_DOCU,@DT_PAY_PREARRANGED,                                    
@CD_FUND,@CD_BGACCT,@CD_BUDGET ,@P_NM_TP,@FG_PAYBILL,@NO_LC ,@DT_DUE, @P_YN_ISS, @V_VAT_TAX, @V_NO_PO_MAX, @V_NM_ITEM_MAX ,@V_UM, @V_QT, @V_CD_ITEM_MAX, @V_CD_BIZPLAN, @V_AM_BUDGET,
@V_USER_ONLY, @V_USER_ONLY2
                                          
END                                                      
                                                      
CLOSE CUR_PU_IV_MNG                                                      
DEALLOCATE CUR_PU_IV_MNG   

IF @V_SERVER_KEY LIKE 'TRIGEM%' OR @V_SERVER_KEY LIKE 'SQL_%' OR @V_SERVER_KEY LIKE 'DZSQL%' -- 삼보전용 상계전표발행
BEGIN 
	EXEC ('UP_PU_REVERSE_TRANS_DOCU ' + ''''  + @P_CD_COMPANY + ''', ' + '''' + @P_NO_IV + '''')
	IF @@ERROR <> 0 RETURN
END
	                                                      
RETURN                                                      
                                                  
ERROR:                                   
    RAISERROR ( @ERRMSG , 18, 1 )