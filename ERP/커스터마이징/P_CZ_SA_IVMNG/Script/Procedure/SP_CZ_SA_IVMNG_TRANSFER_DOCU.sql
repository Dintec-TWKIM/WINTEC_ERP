USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_IVMNG_TRANSFER_DOCU]    Script Date: 2015-05-27 오전 10:39:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- 설    명 : 영업>매출관리>매출관리 --> 매출관리 전표처리 (Ver1.1)
-- 수 정 자 : 김명수
-- 수 정 일 : 2010.06.11
-- 유    형 : 수정
-- 내    역 : 에러발생시 ROLLBACK 처리
-- ================================================
--*******************************************                
-- Change History                
--*******************************************
--
--*******************************************/
ALTER PROCEDURE [NEOE].[SP_CZ_SA_IVMNG_TRANSFER_DOCU]              
(                      
    @P_CD_COMPANY               NVARCHAR(7),
	@P_LANGUAGE				    NVARCHAR(5) = 'KR',                       
    @P_NO_IV                    NVARCHAR(20),                      
    @P_OPT_CD_SVC               NVARCHAR(5)   =   NULL,     --발행방법                
    @P_OPT_ITEM_RPT_GUBUN       NVARCHAR(1)   =   NULL,     --내역표시구분            
    @P_OPT_ITEM_RPT_TEXT        NVARCHAR(100) =   NULL,     --내역표시임의내용        
    @P_OPT_ITEM_NM_GUBUN        NVARCHAR(1)   =   NULL,     --품목표시구분            
    @P_OPT_SELL_DAM_GUBUN       NVARCHAR(1)   =   NULL,     --공급자연락처 표시구분   
    @P_OPT_SELL_DAM_NM          NVARCHAR(20)  =   NULL,     --Login 한 USER의 사원명을 넘긴다. 
    @P_OPT_SELL_DAM_EMAIL       NVARCHAR(50)  =   NULL,     --공급자e-mail          
    @P_OPT_SELL_DAM_MOBIL       NVARCHAR(20)  =   NULL,     --공급자Hp
	@P_ID_USER					NVARCHAR(15)  =   NULL		--Login 한 USER의 ID를 넘긴다.
)                      
AS                   
/* 20100304 에 추가
   회계에서 영업의 부가세(세금계산서)전표를 회계 전자세금계산서에서 발행할경우
   품목내역표시및 발행자정보에 대한 옵션을 제공하기위해 사용된다,
   
   FI_TAX에 입력되는 내용이다. 현재는 ORACLE- KOIS만 선택적으로 사용되어지고있으며
   회계에서 영업의 미결전표를 전자세금계산서에서 발행하는것을 막고있다
   당 인자는 부가세 데이터 관련하여 UP_SA_IVMNG_TRANSFER_DOCU_TAX 에 인자로 전달된다.
   
	OPT_ITEM_PRT_GUBUN	내역표시 0:모두 1:임의					
	OPT_ITEM_PRT_TEXT	내역표시 1:임의일 경우 (한줄처리시) 기표할 내용					
	OPT_ITEM_NM_GUBUN	품목표시 0:표시안함 1:기본코드표시 2:영문명표시 3:마감비고 우선표시 (kois요청-옵션)					
														
	OPT_SELL_DAM_GUBUN	공급자연락처 표시 0:임의(로그인사용자) 1:마감담당자 2:개별지정					
	OPT_SELL_DAM_NM		공급자명					
	OPT_SELL_DAM_EMAIL	공급자E_MAIL					
	OPT_SELL_DAM_MOBIL	공급자핸드폰번호					
														
	OPT_CD_SVC			발행옵션					
*/

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
          
-- CC_SA_IV024 (전표처리 조회)                        
-- ◎ 전표처리                        
-- 계정과목 & 채권계정                        
DECLARE @CD_COMPANY     NVARCHAR(7),
        @NO_COMPANY     NVARCHAR(20),
        @NO_IV          NVARCHAR(20),
        @V_NO_IV        NVARCHAR(20),
        @CD_BIZAREA     NVARCHAR(7),
        @CD_WDEPT       NVARCHAR(12),
        @BILL_PARTNER   NVARCHAR(20),
        @FG_TAX         NCHAR(2),
        @NM_TAX         NVARCHAR(50),
        @ID_WRITE       NVARCHAR(10),
        @DT_PROCESS     NCHAR(8),
        @AM_DR          NUMERIC(19,4),
        @AM_CR          NUMERIC(19,4),
        @AM_SUPPLY      NUMERIC(19,4),
        @DT_TAX         NCHAR(8),
        @FG_TRANS       NVARCHAR(2),
        @CD_CC          NVARCHAR(12),
        @NO_EMP         NVARCHAR(10),
        @CD_PJT         NVARCHAR(20),
        @CD_EXCH        NVARCHAR(3),
        @NM_EXCH        NVARCHAR(50),
        @RT_EXCH        NUMERIC(11,4),
        @AM_EX          NUMERIC(19,4),
        @CD_DEPT        NVARCHAR(12),
        @CD_PC          NVARCHAR(7),
        @NM_BIZAREA     NVARCHAR(50),
        @FG_AIS         NCHAR(3),
        @CD_ACCT        NVARCHAR(10),
        @CLS_ITEM       NVARCHAR(3),
        @TP_ACAREA      NVARCHAR(3),
        @TP_DRCR        NVARCHAR(1),
        @NM_DEPT        NVARCHAR(50),
        @NM_CC          NVARCHAR(50),
        @NM_EMP         NVARCHAR(50),
        @NM_PJT         NVARCHAR(50),
        @NM_PARTNER     NVARCHAR(50),
        @YN_ISS         NVARCHAR(1),
        @T_CD_RELATION  NCHAR(2),   --부가세 연동항목(31)매출
        @P_ERRORCODE    NCHAR(10),
        @P_ERRORMSG     NVARCHAR(300),
            
        @P_PUMM         NVARCHAR(150),
        @P_NOTE         NVARCHAR(150),
        @P_NM_TP        NVARCHAR(20),
        @CD_CC_TEMP     NVARCHAR(12),
        @CD_DOCU        NVARCHAR(3),
        @CD_FUND        NVARCHAR(4),

        @CUR_DATE       NVARCHAR(8),    --현재일자
        @AM_EXDO        NUMERIC(19,4),  --외화금액

        @ERRNO          INT,
        @ERRMSG         NVARCHAR(255),
              
        @AM_TOT         NUMERIC(17, 4),
        @VAT_TAX        NUMERIC(15, 4), --부가세 20121009추가(관리항목)
        @NO_ITEM        NVARCHAR(20),	--품목코드
        @NM_ITEM        NVARCHAR(50),
		@NO_IO			NVARCHAR(20),	--출고번호
        
        @V_SERVER_KEY   NVARCHAR(50),   --서버키처리추가 (대신정보 표준적요설정 2010-07-01 LSH)

        @V_UD_NM_01     NVARCHAR(20),
        @V_UD_NM_02     NVARCHAR(20),
        @V_UD_NM_03     NVARCHAR(20),
        @V_UD_NM_04     NVARCHAR(20),

        @V_RT_EXCH      NUMERIC(11,4),                        
		
        @V_CD_EXC       NVARCHAR(3),    --전용코드추가 20120307 - 회계단위/귀속사업장 선택 LSH 000:기본(부가세사업장) 100:사용자설정(일반사업장)
        @V_CD_EXC2      NVARCHAR(3),    --[매출관리-전표처리 시 프로젝트 관리항목 설정]
        @V_CD_EXC_MENU  NVARCHAR(3)    --미결전표처리 담당자 관리항목 설정값
        
DECLARE @V_CD_UMNG1     NVARCHAR(10),
        @V_CD_UMNG3     NVARCHAR(10),
        @V_CD_BUDGET    NVARCHAR(20),
        @V_CD_BGACCT    NVARCHAR(20),
		@V_ST_DOCU		NVARCHAR(1)

SET     @V_CD_UMNG1     = ''
SET     @V_CD_UMNG3     = ''

SELECT  @V_CD_EXC       = ISNULL(CD_EXC,'000') FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '매출전표처리'
SELECT  @V_CD_EXC       = ISNULL(@V_CD_EXC,'000')
SELECT  @V_SERVER_KEY   = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG WHERE YN_UPGRADE = 'Y'
SELECT  @CUR_DATE       = CONVERT(NVARCHAR(8), GETDATE(), 112)
SELECT  @V_CD_EXC_MENU  = CD_EXC FROM MA_EXC_MENU WHERE CD_COMPANY = @P_CD_COMPANY AND ID_MENU = 'P_SA_IVMNG' AND CD_TITLE = 'SA_A00000001'
SELECT  @V_CD_EXC_MENU  = ISNULL(@V_CD_EXC_MENU, '000')
SELECT  @V_CD_EXC2      = CD_EXC FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '매출관리-전표처리 시 프로젝트 관리항목 설정'
SELECT  @V_CD_EXC2      = ISNULL(@V_CD_EXC2, '000')

SET @V_ST_DOCU = '1'

SELECT  @AM_TOT = ( AM_K + VAT_TAX )           
FROM    SA_IVH A              
WHERE   CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV                        
          
IF ( @AM_TOT = 0 ) /*공급금액+부가세 = 0이면 회계전표안만들고 TP_AIS = 'Y'처리 20090210*/          
BEGIN          
    UPDATE SA_IVH SET TP_AIS = 'Y' WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV                        
    RETURN          
END           
          
--회계전표 매핑테이블초기화 20100330 LSH
DELETE
FROM    SA_IV_DOCU_MAPP
WHERE   NO_IV = @P_NO_IV
AND     CD_COMPANY = @P_CD_COMPANY 

----------------------------------------------------------------------------------------          
DECLARE SRC_CURSOR CURSOR FOR                        
                        
/*차변 : 외상매출금 OR 외화외상매출금 */              
SELECT --DISTINCT                         
        A.CD_COMPANY,                         
        A.NO_IV,                         
        CASE WHEN @V_CD_EXC = '100' THEN A.CD_BIZAREA ELSE A.CD_BIZAREA_TAX END AS CD_BIZAREA,                         
        A.CD_DEPT CD_WDEPT,                         
        A.BILL_PARTNER,            
        A.FG_TAX,                         
        A.NO_EMP ID_WRITE,                         
        A.DT_PROCESS,                        
        CASE ISNULL(L.TP_DRCR,'1') WHEN '1' THEN SUM(CASE B.YN_RETURN WHEN 'N' THEN (B.AM_CLS + B.VAT) 
																	  WHEN 'Y' THEN -(B.AM_CLS + B.VAT) END) 
								   ELSE 0.0 END AM_DR,                        
        CASE ISNULL(L.TP_DRCR,'1') WHEN '2' THEN 0 - SUM(CASE B.YN_RETURN WHEN 'N' THEN (B.AM_CLS + B.VAT) 
																		  WHEN 'Y' THEN -(B.AM_CLS + B.VAT) END) 
								   ELSE 0.0 END AM_CR,                        
        0.0 AM_SUPPLY,             
        B.DT_TAX,                         
        B.FG_TRANS,                         
        B.CD_CC,                         
        (CASE WHEN @V_CD_EXC_MENU = '000' THEN B.NO_EMP ELSE A.NO_EMP END) NO_EMP,
        (CASE WHEN @V_CD_EXC2 IN ('001', '003') THEN '' ELSE ISNULL(B.CD_PJT,'') END) AS CD_PJT,          
        B.CD_EXCH,
        (SELECT (CASE @P_LANGUAGE WHEN 'KR' THEN NM_SYSDEF
		 						  WHEN 'US' THEN NM_SYSDEF_E
		 						  WHEN 'JP' THEN NM_SYSDEF_JP
		 						  WHEN 'CH' THEN NM_SYSDEF_CH END)
		 FROM MA_CODEDTL
		 WHERE CD_COMPANY = A.CD_COMPANY
		 AND CD_FIELD = 'MA_B000005'
		 AND CD_SYSDEF = B.CD_EXCH) AS NM_EXCH,                               
        B.RT_EXCH,                
        CASE ISNULL(L.TP_DRCR,'1') WHEN '1' THEN SUM(CASE B.YN_RETURN WHEN 'N' THEN ROUND(B.AM_EX_CLS + (ISNULL(B.VAT, 0) / ISNULL(B.RT_EXCH, 1)), 2) 
																	  WHEN 'Y' THEN -ROUND(B.AM_EX_CLS + (ISNULL(B.VAT, 0) / ISNULL(B.RT_EXCH, 1)), 2) END)
											ELSE 0 - SUM(CASE B.YN_RETURN WHEN 'N' THEN ROUND(B.AM_EX_CLS + (ISNULL(B.VAT, 0) / ISNULL(B.RT_EXCH, 1)), 2)   
																		  WHEN 'Y' THEN -ROUND(B.AM_EX_CLS + (ISNULL(B.VAT, 0) / ISNULL(B.RT_EXCH, 1)), 2) END) 
        END AM_EX,                                
        A.CD_DEPT,                        
        --(SELECT CD_PC FROM MA_CC WHERE CD_CC = (SELECT CD_CC FROM MA_DEPT WHERE CD_COMPANY = A.CD_COMPANY AND CD_DEPT = A.CD_DEPT) AND CD_COMPANY = A.CD_COMPANY) CD_PC,                        
        CASE WHEN @V_CD_EXC = '100' THEN E2.CD_PC ELSE E.CD_PC END  AS CD_PC,   
        CASE WHEN @V_CD_EXC = '100' THEN E2.NM_BIZAREA ELSE E.NM_BIZAREA END AS NM_BIZAREA,                        
        L.FG_AIS,                        
        L.CD_ACCT,                        
        '' CLS_ITEM,                         
        ISNULL(L.TP_DRCR,'1') AS  TP_DRCR, --'1' TP_DRCR, -- 차대구분 1: 차변                        
        (SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = A.CD_DEPT AND CD_COMPANY = A.CD_COMPANY) NM_DEPT,                        
        (SELECT NM_CC FROM MA_CC WHERE CD_CC = B.CD_CC AND CD_COMPANY = A.CD_COMPANY) NM_CC,                        
        (SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = (CASE WHEN @V_CD_EXC_MENU = '000' THEN B.NO_EMP ELSE A.NO_EMP END) AND CD_COMPANY = @P_CD_COMPANY) NM_EMP,                        
        (SELECT NM_PROJECT FROM SA_PROJECTH WHERE NO_PROJECT = (CASE WHEN @V_CD_EXC2 IN ('001', '003') THEN '' ELSE ISNULL(B.CD_PJT,'') END) AND CD_COMPANY = A.CD_COMPANY ) NM_PJT,                
        (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.BILL_PARTNER) NM_PARTNER,
        (SELECT (CASE WHEN YN_JEONJA = '3' THEN '2' ELSE '0' END) FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.BILL_PARTNER) AS YN_JEONJA,
        F.CD_RELATION, --'10' CD_RELATION, --연동항목:일반   20120901 AVL요청:전정식
        (SELECT (CASE @P_LANGUAGE WHEN 'KR' THEN NM_SYSDEF
		 						  WHEN 'US' THEN NM_SYSDEF_E
		 						  WHEN 'JP' THEN NM_SYSDEF_JP
		 						  WHEN 'CH' THEN NM_SYSDEF_CH END)
		 FROM MA_CODEDTL
		 WHERE CD_COMPANY = A.CD_COMPANY
		 AND CD_FIELD = 'FI_T000011'
		 AND CD_SYSDEF = A.FG_TAX) AS NM_TAX,
        A.NO_BIZAREA,               
        (CASE F.TP_DRCR WHEN '1' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA ,        
        A.CD_DOCU,      
        L.CD_FUND,    
        MAX(H.NM_TP) AS NM_TP    ,
        MAX(B.UD_NM_01) AS UD_NM_01,
        MAX(B.UD_NM_02) AS UD_NM_02,
        MAX(B.UD_NM_03) AS UD_NM_03,
        MAX(B.UD_NM_04) AS UD_NM_04,
        SUM(CASE B.YN_RETURN WHEN 'N' THEN B.VAT WHEN 'Y' THEN -(B.VAT) END) AS AM_VAT,
        CASE WHEN @V_SERVER_KEY <> 'SLFIRE' THEN NULL ELSE MAX(B.CD_ITEM) END NO_ITEM,
        CASE WHEN @V_SERVER_KEY <> 'SLFIRE' THEN NULL ELSE
			(SELECT NM_ITEM FROM MA_PITEM WHERE CD_ITEM = MAX(B.CD_ITEM) AND CD_COMPANY = @P_CD_COMPANY AND CD_PLANT = MAX(B.CD_PLANT)) END NM_ITEM,
		MAX(B.NO_IO) AS NO_IO
FROM    SA_IVH A
        JOIN SA_IVL B ON A.NO_IV = B.NO_IV AND A.CD_COMPANY = B.CD_COMPANY                        
        --LEFT JOIN MA_EMP D ON B.NO_EMP = D.NO_EMP AND B.CD_COMPANY = D.CD_COMPANY                        
        JOIN MA_BIZAREA E ON A.CD_BIZAREA_TAX = E.CD_BIZAREA AND A.CD_COMPANY = E.CD_COMPANY                        
        JOIN MA_BIZAREA E2 ON A.CD_BIZAREA = E2.CD_BIZAREA AND A.CD_COMPANY = E2.CD_COMPANY   
        JOIN MA_AISPOSTL L ON B.CD_COMPANY = L.CD_COMPANY AND L.FG_TP = '100' AND B.TP_IV = L.CD_TP AND L.FG_AIS = (CASE WHEN B.FG_TRANS = '001' THEN '101' ELSE '102' END)
        JOIN MA_AISPOSTH H ON L.CD_COMPANY = H.CD_COMPANY AND L.CD_TP = H.CD_TP AND L.FG_TP = H.FG_TP                 
        JOIN FI_ACCTCODE F ON F.CD_ACCT = L.CD_ACCT AND F.CD_COMPANY = L.CD_COMPANY              
WHERE   B.NO_IV = @P_NO_IV
AND     B.CD_COMPANY = @P_CD_COMPANY
AND     A.TP_AIS = 'N'      --전표처리여부
GROUP BY A.CD_COMPANY, A.DT_PROCESS, B.DT_TAX, A.BILL_PARTNER, B.CD_CC,
        (CASE WHEN @V_CD_EXC2 IN ('001', '003') THEN '' ELSE ISNULL(B.CD_PJT,'') END),               
        A.NO_EMP, A.NO_IV, 
        --A.CD_BIZAREA_TAX,                        
        CASE WHEN @V_CD_EXC = '100' THEN A.CD_BIZAREA ELSE A.CD_BIZAREA_TAX END,
        A.CD_DEPT, A.FG_TAX, B.FG_TRANS,
        CASE WHEN @V_CD_EXC_MENU = '000' THEN B.NO_EMP ELSE A.NO_EMP END,
        B.CD_EXCH, B.RT_EXCH, --D.CD_DEPT,
        --E.CD_PC, 
        CASE WHEN @V_CD_EXC = '100' THEN E2.CD_PC ELSE E.CD_PC END,
        --E.NM_BIZAREA,               
        CASE WHEN @V_CD_EXC = '100' THEN E2.NM_BIZAREA ELSE E.NM_BIZAREA END,
        L.FG_AIS, L.CD_ACCT,   A.NO_BIZAREA, F.YN_BAN,  (CASE F.TP_DRCR WHEN '1' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END),        
        A.CD_DOCU ,L.CD_FUND, L.TP_DRCR,
        F.CD_RELATION                  
HAVING  SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_CLS + B.VAT WHEN 'Y' THEN -(B.AM_CLS + B.VAT) END) <> 0          
         
UNION ALL                        
                        
--------------------------------------------------------------------------------------------------------------------- 매출계정                        
/*대변 : 원자재/부자재/제품/반제품/상품/ 매출금 */     
SELECT  --DISTINCT 
        A.CD_COMPANY, A.NO_IV,
        --A.CD_BIZAREA_TAX AS CD_BIZAREA, 
        CASE WHEN @V_CD_EXC = '100' THEN A.CD_BIZAREA ELSE A.CD_BIZAREA_TAX END AS CD_BIZAREA,
        A.CD_DEPT, A.CD_PARTNER, A.FG_TAX, A.NO_EMP, A.DT_PROCESS,    
        CASE ISNULL(L.TP_DRCR,'2') WHEN '1' THEN  0 - SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_CLS WHEN 'Y' THEN -B.AM_CLS END) ELSE 0.0 END AM_DR,                        
        CASE ISNULL(L.TP_DRCR,'2') WHEN '2' THEN  SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_CLS WHEN 'Y' THEN -B.AM_CLS END) ELSE 0.0 END AM_CR,                        
        0.0 AM_SUPPLY,                        
        B.DT_TAX, B.FG_TRANS, B.CD_CC,
        (CASE WHEN @V_CD_EXC_MENU = '000' THEN B.NO_EMP ELSE A.NO_EMP END) NO_EMP,
        (CASE WHEN @V_CD_EXC2 IN ('002', '003') THEN '' ELSE ISNULL(B.CD_PJT,'') END) AS CD_PJT,
        B.CD_EXCH,
        (SELECT (CASE @P_LANGUAGE WHEN 'KR' THEN NM_SYSDEF
		 						  WHEN 'US' THEN NM_SYSDEF_E
		 						  WHEN 'JP' THEN NM_SYSDEF_JP
		 						  WHEN 'CH' THEN NM_SYSDEF_CH END)
		 FROM MA_CODEDTL
		 WHERE CD_COMPANY = A.CD_COMPANY
		 AND CD_FIELD = 'MA_B000005'
		 AND CD_SYSDEF = B.CD_EXCH) AS NM_EXCH,                    
        B.RT_EXCH,                 
        CASE ISNULL(L.TP_DRCR,'2') WHEN '2' THEN SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_EX_CLS  WHEN 'Y' THEN -(B.AM_EX_CLS ) END) 
        ELSE 0 - SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_EX_CLS  WHEN 'Y' THEN -(B.AM_EX_CLS ) END) 
        END AM_EX, 
        A.CD_DEPT,                        
        --(SELECT CD_PC FROM MA_CC WHERE CD_CC = (SELECT CD_CC FROM MA_DEPT WHERE CD_COMPANY = A.CD_COMPANY AND CD_DEPT = A.CD_DEPT) AND CD_COMPANY = A.CD_COMPANY) CD_PC,                        
        --E.CD_PC,                        
        CASE WHEN @V_CD_EXC = '100' THEN E2.CD_PC ELSE E.CD_PC END AS CD_PC,
        --E.NM_BIZAREA,                        
        CASE WHEN @V_CD_EXC = '100' THEN E2.NM_BIZAREA ELSE E.NM_BIZAREA END AS NM_BIZAREA,
        L.FG_AIS, L.CD_ACCT,                        
        C.CLS_ITEM,  
        ISNULL(L.TP_DRCR,'2') AS TP_DRCR, --'2' TP_DRCR,                        
        (SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = A.CD_DEPT AND CD_COMPANY = A.CD_COMPANY) NM_DEPT,                        
        (SELECT NM_CC FROM MA_CC WHERE CD_CC = B.CD_CC AND CD_COMPANY = A.CD_COMPANY) NM_CC,                        
        (SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = (CASE WHEN @V_CD_EXC_MENU = '000' THEN B.NO_EMP ELSE A.NO_EMP END) AND CD_COMPANY = @P_CD_COMPANY) NM_EMP,                        
        /*              
        (SELECT NM_PROJECT FROM SA_PROJECTH WHERE NO_PROJECT = B.CD_PJT AND CD_COMPANY = A.CD_COMPANY AND NO_SEQ =                        
        (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE NO_PROJECT = B.CD_PJT AND CD_COMPANY = A.CD_COMPANY)) NM_PJT,         */              
        (SELECT NM_PROJECT FROM SA_PROJECTH WHERE NO_PROJECT = (CASE WHEN @V_CD_EXC2 IN ('002', '003') THEN '' ELSE ISNULL(B.CD_PJT,'') END) AND CD_COMPANY = A.CD_COMPANY ) NM_PJT,                
        (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) NM_PARTNER,
        (SELECT (CASE WHEN YN_JEONJA = '3' THEN '2' ELSE '0' END) FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) AS YN_JEONJA,
        '10' CD_RELATION, --연동항목:일반
        (SELECT (CASE @P_LANGUAGE WHEN 'KR' THEN NM_SYSDEF
								  WHEN 'US' THEN NM_SYSDEF_E
								  WHEN 'JP' THEN NM_SYSDEF_JP
								  WHEN 'CH' THEN NM_SYSDEF_CH END)
		 FROM MA_CODEDTL
		 WHERE CD_COMPANY = A.CD_COMPANY
		 AND CD_FIELD = 'FI_T000011'
		 AND CD_SYSDEF = A.FG_TAX) AS NM_TAX,                                
        A.NO_BIZAREA,              
        --CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END TP_ACAREA              
        (CASE F.TP_DRCR WHEN '2' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA  ,        
        A.CD_DOCU,      
        L.CD_FUND,    
        MAX(H.NM_TP) AS NM_TP,
        MAX(B.UD_NM_01) AS  UD_NM_01,
        MAX(B.UD_NM_02) AS  UD_NM_02,
        MAX(B.UD_NM_03) AS UD_NM_03,
        MAX(B.UD_NM_04) AS UD_NM_04,
        SUM(CASE B.YN_RETURN WHEN 'N' THEN B.VAT WHEN 'Y' THEN -(B.VAT) END) AS AM_VAT,
        CASE WHEN @V_SERVER_KEY <> 'SLFIRE' THEN NULL ELSE MAX(B.CD_ITEM) END NO_ITEM,
        CASE WHEN @V_SERVER_KEY <> 'SLFIRE' THEN NULL ELSE
			(SELECT NM_ITEM FROM MA_PITEM WHERE CD_ITEM = MAX(B.CD_ITEM) AND CD_COMPANY = @P_CD_COMPANY AND CD_PLANT = MAX(B.CD_PLANT)) END NM_ITEM,
	    MAX(B.NO_IO) AS NO_IO                 
FROM    SA_IVH A
        JOIN SA_IVL B ON A.NO_IV = B.NO_IV AND A.CD_COMPANY = B.CD_COMPANY                        
        JOIN MA_PITEM C ON B.CD_ITEM = C.CD_ITEM AND B.CD_PLANT = C.CD_PLANT AND B.CD_COMPANY = C.CD_COMPANY                        
        --LEFT JOIN MA_EMP D ON B.NO_EMP = D.NO_EMP AND B.CD_COMPANY = D.CD_COMPANY
		JOIN MA_CODEDTL CD ON CD.CD_COMPANY = C.CD_COMPANY AND CD.CD_FIELD = 'MA_B000010' AND CD.CD_SYSDEF = C.CLS_ITEM                        
        JOIN MA_BIZAREA E ON A.CD_BIZAREA_TAX = E.CD_BIZAREA AND A.CD_COMPANY = E.CD_COMPANY
        JOIN MA_BIZAREA E2 ON A.CD_BIZAREA = E2.CD_BIZAREA AND A.CD_COMPANY = E2.CD_COMPANY                            
        JOIN MA_AISPOSTL L ON B.CD_COMPANY = L.CD_COMPANY AND L.FG_TP = '100' AND B.TP_IV = L.CD_TP AND L.FG_AIS = CD.CD_FLAG2
        JOIN MA_AISPOSTH H ON L.CD_COMPANY = H.CD_COMPANY AND L.CD_TP = H.CD_TP AND L.FG_TP = H.FG_TP   
        JOIN FI_ACCTCODE F ON F.CD_ACCT = L.CD_ACCT AND F.CD_COMPANY = L.CD_COMPANY              
WHERE   B.NO_IV = @P_NO_IV
AND     B.CD_COMPANY = @P_CD_COMPANY
AND     A.TP_AIS = 'N'      -- 전표처리여부
GROUP BY A.CD_COMPANY, A.DT_PROCESS, B.DT_TAX, A.CD_PARTNER, B.CD_CC,
        (CASE WHEN @V_CD_EXC2 IN ('002', '003') THEN '' ELSE ISNULL(B.CD_PJT,'') END),
        A.NO_EMP, A.NO_IV,
        --A.CD_BIZAREA_TAX,
        CASE WHEN @V_CD_EXC = '100' THEN A.CD_BIZAREA ELSE A.CD_BIZAREA_TAX END,
        A.CD_DEPT, A.FG_TAX, B.FG_TRANS,
        CASE WHEN @V_CD_EXC_MENU = '000' THEN B.NO_EMP ELSE A.NO_EMP END,
        B.CD_EXCH, B.RT_EXCH, --D.CD_DEPT,
        --E.CD_PC, 
        CASE WHEN @V_CD_EXC = '100' THEN E2.CD_PC ELSE E.CD_PC END,
        --E.NM_BIZAREA, 
        CASE WHEN @V_CD_EXC = '100' THEN E2.NM_BIZAREA ELSE E.NM_BIZAREA END,
        L.FG_AIS,
        L.CD_ACCT, C.CLS_ITEM, A.NO_BIZAREA, F.YN_BAN,  (CASE F.TP_DRCR WHEN '2' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END),        
        A.CD_DOCU,L.CD_FUND, L.TP_DRCR
HAVING  SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_CLS WHEN 'Y' THEN -B.AM_CLS END) <> 0

UNION ALL                        
              
--------------------------------------------------------------------------------------------------------------------- 부가세계정                        
 /* 대변: 부가세예수금 */              
SELECT  --DISTINCT 
        A.CD_COMPANY, A.NO_IV,
        A.CD_BIZAREA_TAX AS CD_BIZAREA, 
        A.CD_DEPT, A.CD_PARTNER, A.FG_TAX, A.NO_EMP, A.DT_PROCESS,                        
        CASE ISNULL(L.TP_DRCR,'2') WHEN '1' THEN 0 - SUM(CASE B.YN_RETURN WHEN 'N' THEN B.VAT WHEN 'Y' THEN -B.VAT END) ELSE 0.0 END AM_DR,                        
        CASE ISNULL(L.TP_DRCR,'2') WHEN '2' THEN SUM(CASE B.YN_RETURN WHEN 'N' THEN B.VAT WHEN 'Y' THEN -B.VAT END) ELSE 0.0 END AM_CR,                        
        SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_CLS WHEN 'Y' THEN -B.AM_CLS END) AM_SUPPLY,    --공급가액(20071010)                    
        B.DT_TAX, B.FG_TRANS,                         
        --B.CD_CC,
        --B.NO_EMP,                         
        --null, -- 부가세예수금(부가세대급금) 계정에서는 CD_CC가 의미가 없음                        
        MAX(CASE WHEN @V_SERVER_KEY = 'WHASUNG' THEN B.CD_CC ELSE NULL END),
        null, -- 부가세예수금(부가세대급금) 계정에서는 NO_EMP가 의미가 없음                        
        NULL,--B.CD_PJT,               
        CASE WHEN @V_SERVER_KEY = 'DAEYONG' AND A.FG_TAX = '15' THEN A.CD_EXCH ELSE '000' END CD_EXCH,
        (SELECT (CASE @P_LANGUAGE WHEN 'KR' THEN NM_SYSDEF
		 						  WHEN 'US' THEN NM_SYSDEF_E
		 						  WHEN 'JP' THEN NM_SYSDEF_JP
		 						  WHEN 'CH' THEN NM_SYSDEF_CH END)
		 FROM MA_CODEDTL
		 WHERE CD_COMPANY = A.CD_COMPANY
		 AND CD_FIELD = 'MA_B000005'
		 AND CD_SYSDEF = (CASE WHEN @V_SERVER_KEY = 'DAEYONG' AND A.FG_TAX = '15' THEN A.CD_EXCH ELSE '000' END)) AS NM_EXCH,                    
        CASE WHEN @V_SERVER_KEY = 'DAEYONG' AND A.FG_TAX = '15' THEN A.RT_EXCH  ELSE 1 END RT_EXCH,                
        CASE WHEN @V_SERVER_KEY = 'DAEYONG' AND A.FG_TAX = '15' THEN CASE ISNULL(L.TP_DRCR,'2') WHEN '2' THEN  SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_EX_CLS  WHEN 'Y' THEN -(B.AM_EX_CLS ) END)
                                                                     ELSE 0 - SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_EX_CLS  WHEN 'Y' THEN -(B.AM_EX_CLS ) END) END 
        ELSE CASE ISNULL(L.TP_DRCR,'2') WHEN '2' THEN  SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_CLS  WHEN 'Y' THEN -(B.AM_CLS ) END)
             ELSE 0 - SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_CLS  WHEN 'Y' THEN -(B.AM_CLS ) END) END 
        END  AM_EX, 
                                               
        --CASE ISNULL(L.TP_DRCR,'2') WHEN '2' THEN  SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_EX_CLS  WHEN 'Y' THEN -(B.AM_EX_CLS ) END)
        --ELSE 0 - SUM(CASE B.YN_RETURN WHEN 'N' THEN B.AM_EX_CLS  WHEN 'Y' THEN -(B.AM_EX_CLS ) END) 
        --END  AM_EX,                                         
        A.CD_DEPT,                        
        --(SELECT CD_PC FROM MA_CC WHERE CD_CC = (SELECT CD_CC FROM MA_DEPT WHERE CD_COMPANY = A.CD_COMPANY AND CD_DEPT = A.CD_DEPT) AND CD_COMPANY = A.CD_COMPANY) CD_PC,                        
        --E.CD_PC,                        
        CASE WHEN @V_CD_EXC = '100' THEN E2.CD_PC ELSE E.CD_PC END AS CD_PC,
        E.NM_BIZAREA,                        
        L.FG_AIS, L.CD_ACCT,                        
        '' CLS_ITEM,  
        ISNULL(L.TP_DRCR,'2') AS TP_DRCR, --'2' TP_DRCR,                        
        (SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = A.CD_DEPT AND CD_COMPANY = A.CD_COMPANY) NM_DEPT,                        
        --(SELECT NM_CC FROM MA_CC WHERE CD_CC = B.CD_CC AND CD_COMPANY = A.CD_COMPANY) NM_CC,                        
        --(SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = B.NO_EMP AND CD_COMPANY = A.CD_COMPANY) NM_EMP,                        
        --null, -- 부가세예수금(부가세대급금) 계정에서는 NM_CC가 의미가 없음                        
        CASE WHEN @V_SERVER_KEY = 'WHASUNG' THEN (SELECT NM_CC FROM MA_CC WHERE CD_CC = MAX(B.CD_CC) AND CD_COMPANY = A.CD_COMPANY) ELSE NULL END,
        NULL, -- 부가세예수금(부가세대급금) 계정에서는 NM_KOR이 의미가 없음                        
        /*              
        (SELECT NM_PROJECT FROM SA_PROJECTH WHERE NO_PROJECT = B.CD_PJT AND CD_COMPANY = A.CD_COMPANY AND NO_SEQ =                        
        (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE NO_PROJECT = B.CD_PJT AND CD_COMPANY = A.CD_COMPANY)) NM_PJT,    */                    
        NULL AS NM_PJT,
        (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) NM_PARTNER,
        (SELECT (CASE WHEN YN_JEONJA = '3' THEN '2' ELSE '0' END) FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) AS YN_JEONJA,
        '31' CD_RELATION, --연동항목:매출(부가세 예수금)
        (SELECT (CASE @P_LANGUAGE WHEN 'KR' THEN NM_SYSDEF
		 						  WHEN 'US' THEN NM_SYSDEF_E
		 						  WHEN 'JP' THEN NM_SYSDEF_JP
		 						  WHEN 'CH' THEN NM_SYSDEF_CH END)
		 FROM MA_CODEDTL
		 WHERE CD_COMPANY = A.CD_COMPANY
		 AND CD_FIELD = 'FI_T000011'
		 AND CD_SYSDEF = A.FG_TAX) AS NM_TAX,               
        A.NO_BIZAREA,
        '0' TP_ACAREA,
        A.CD_DOCU,  
        L.CD_FUND,    
        MAX(H.NM_TP) AS NM_TP,
        MAX(B.UD_NM_01) AS UD_NM_01,
        MAX(B.UD_NM_02) AS UD_NM_02,
        MAX(B.UD_NM_03) AS UD_NM_03,
        MAX(B.UD_NM_04) AS UD_NM_04,
        SUM(CASE B.YN_RETURN WHEN 'N' THEN B.VAT WHEN 'Y' THEN -(B.VAT) END) AS AM_VAT,
        NULL,
        NULL,
		MAX(B.NO_IO) AS NO_IO
FROM    SA_IVH A
        JOIN SA_IVL B ON A.NO_IV = B.NO_IV AND A.CD_COMPANY = B.CD_COMPANY
        --LEFT JOIN MA_EMP D ON B.NO_EMP = D.NO_EMP AND B.CD_COMPANY = D.CD_COMPANY
        JOIN MA_BIZAREA E ON A.CD_BIZAREA_TAX = E.CD_BIZAREA AND A.CD_COMPANY = E.CD_COMPANY
        JOIN MA_BIZAREA E2 ON A.CD_BIZAREA = E2.CD_BIZAREA AND A.CD_COMPANY = E2.CD_COMPANY
        JOIN MA_AISPOSTL L ON B.CD_COMPANY = L.CD_COMPANY AND B.TP_IV = L.CD_TP -- 매출형태
        JOIN MA_AISPOSTH H ON L.CD_COMPANY = H.CD_COMPANY AND L.CD_TP = H.CD_TP AND L.FG_TP = H.FG_TP
        JOIN FI_ACCTCODE F ON F.CD_ACCT = L.CD_ACCT AND F.CD_COMPANY = L.CD_COMPANY
WHERE   B.NO_IV = @P_NO_IV
AND     B.CD_COMPANY = @P_CD_COMPANY
AND     A.TP_AIS = 'N'      --전표처리여부
AND     L.FG_TP = '100'     --형태코드 : 매출형태('100')
AND     L.FG_AIS = '103'    --채권계정 : 부가세('103')  
AND     A.FG_TAX <> '99'    --한국AVL 임의코드(99:영수증)는 전표라인생성안되게 20120903 <-한국AVL은 프로시져를 별로도 분리 했으나 유클릭에서 사용(D20130305042)
AND		A.FG_TRANS = '001'
GROUP BY A.CD_COMPANY, A.NO_IV,
        A.CD_BIZAREA_TAX, 
        A.CD_DEPT,
        A.CD_PARTNER, A.FG_TAX, A.NO_EMP,  A.DT_PROCESS,
        B.DT_TAX,  B.FG_TRANS, --B.CD_PJT,
        CASE WHEN @V_SERVER_KEY = 'DAEYONG' AND A.FG_TAX = '15' THEN A.CD_EXCH ELSE '000' END,
        CASE WHEN @V_SERVER_KEY = 'DAEYONG' AND A.FG_TAX = '15' THEN A.RT_EXCH  ELSE 1 END,
        --B.CD_EXCH,
        --B.RT_EXCH,
        --E.CD_PC,
        CASE WHEN @V_CD_EXC = '100' THEN E2.CD_PC ELSE E.CD_PC END,
        E.NM_BIZAREA, L.FG_AIS, L.CD_ACCT, A.NO_BIZAREA, F.YN_BAN,      --D.CD_DEPT
        A.CD_DOCU,  L.CD_FUND, L.TP_DRCR
HAVING  (@V_SERVER_KEY = 'MACROGEN' AND A.FG_TAX <> 'XX' ) OR @V_SERVER_KEY <> 'MACROGEN' 
--HAVING SUM(CASE B.YN_RETURN WHEN 'N' THEN B.VAT WHEN 'Y' THEN -B.VAT END) <> 0 

------------------------------------------
-- 여기서 부터 전표처리 하기 위한 부분  --
------------------------------------------
DECLARE @P_NO_DOCU      NVARCHAR(20)    -- 전표번호
DECLARE @P_DT_PROCESS   NVARCHAR(8)
/*20091231일추가*/                        
DECLARE @P_FG_BILL      NVARCHAR(3)     -- 결제방법  
DECLARE @P_DT_RCP_RSV   NVARCHAR(8)     -- 수금예정일 
DECLARE @P_NO_ACCT		NUMERIC(5, 0)	-- 회계번호 
DECLARE @P_ID_ACCT		NVARCHAR(15)    -- 승인자
  
-- 매출일자,결제방법,수금예정일 알아오기
SELECT  @P_DT_PROCESS = DT_PROCESS,
        @P_FG_BILL    = FG_BILL,
        @P_DT_RCP_RSV = DT_RCP_RSV
FROM    SA_IVH
WHERE   CD_COMPANY = @P_CD_COMPANY
AND     NO_IV = @P_NO_IV

-- 전표번호 채번
EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT

-- 회계번호 채번
SET @P_NO_ACCT = 0
SET @P_ID_ACCT = NULL

-- NO_DOLINE 땜시
DECLARE @P_NO_DOLINE    NUMERIC(4,0)
SET     @P_NO_DOLINE = 0
-- 금액 문자로
DECLARE @AM_DR_CR       VARCHAR(40)

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @CD_COMPANY, @NO_IV, @CD_BIZAREA, @CD_WDEPT, @BILL_PARTNER,
                                @FG_TAX, @ID_WRITE, @DT_PROCESS, @AM_DR, @AM_CR,
                                @AM_SUPPLY, @DT_TAX, @FG_TRANS, @CD_CC, @NO_EMP,
                                @CD_PJT, @CD_EXCH, @NM_EXCH, @RT_EXCH, @AM_EX,
                                @CD_DEPT, @CD_PC, @NM_BIZAREA, @FG_AIS, @CD_ACCT,
                                @CLS_ITEM, @TP_DRCR, @NM_DEPT, @NM_CC, @NM_EMP,
                                @NM_PJT, @NM_PARTNER, @YN_ISS, @T_CD_RELATION, @NM_TAX, @NO_COMPANY,
                                @TP_ACAREA, @CD_DOCU, @CD_FUND, @P_NM_TP, @V_UD_NM_01,
                                @V_UD_NM_02, @V_UD_NM_03, @V_UD_NM_04, @VAT_TAX, @NO_ITEM, @NM_ITEM, @NO_IO

IF @@FETCH_STATUS = -1 
BEGIN 
	RAISERROR ('매출처리할 데이터가 존재하지 않습니다', 18, 1)
	CLOSE SRC_CURSOR
	DEALLOCATE SRC_CURSOR
	RETURN
END

SELECT @P_NOTE = @NM_PARTNER + ' ' + @P_NM_TP
SELECT @P_PUMM = @NM_PARTNER + ' ' + @P_NM_TP

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @P_NO_DOLINE = @P_NO_DOLINE + 1
    SET @AM_DR_CR = CONVERT(VARCHAR(40), @AM_SUPPLY)

    IF @T_CD_RELATION <> '10'  /* 부가세관련계정(10)이 아니면 */
        SET @V_NO_IV = @NO_IV
    ELSE
        SET @V_NO_IV = NULL

    IF @CD_EXCH = '000'  /*금액이 원화일경우 외화반제금액에 NULL값을 넣어 반제대상에서 제외하기위함 20100621*/
    BEGIN
	    SET @AM_EXDO = NULL
	    SET @V_RT_EXCH = @RT_EXCH
    END
    ELSE
    BEGIN
		SET @V_RT_EXCH = @RT_EXCH
		SET @AM_EXDO = @AM_EX
    END
	
    EXEC UP_FI_AUTODOCU_1
        @P_NO_DOCU,         --전표번호
        @P_NO_DOLINE,       --라인번호
        @CD_PC,             --회계단위
        @CD_COMPANY,        --회사코드
        @CD_WDEPT,          --작성부서
        @ID_WRITE,          --작성자
        @P_DT_PROCESS,      --매출일자 = 회계일자 = 처리일자
        @P_NO_ACCT,         --회계번호 NO_ACCT
        '3',                --전표구분-대체 TP_DOCU
        --'23',             --전표유형-일반 CD_DOCU
        @CD_DOCU,           --전표유형 추가 -- 2009.11.30 SMR
        @V_ST_DOCU,         --전표상태-미결 ST_DOCU
        @P_ID_ACCT,         --승인자
        @TP_DRCR,           --차대구분
        @CD_ACCT,           --계정코드
        @P_NOTE,            --적요
        @AM_DR,             --차변금액
        @AM_CR,             --대변금액
        @TP_ACAREA,         --'4' 일 경우 반제원인전표이므로 FI_BANH에 Insert된다
        @T_CD_RELATION,     --연동항목-일반 CD_RELATION
        @CD_CC,				--예산코드 CD_BUDGET
        @CD_FUND,           --자금과목 CD_FUND
        NULL,               --원인전표번호 NO_BDOCU
        NULL,               --원인전표라인 NO_BDOLINE
        '0',                --타대구분 TP_ETCACCT
        @CD_BIZAREA,        --귀속사업장
        @NM_BIZAREA,
        @CD_CC,             --코스트센터
        @NM_CC,
        @CD_PJT,            --프로젝트
        @NM_PJT,
        @CD_DEPT,           --부서
        @NM_DEPT,
        @NO_EMP,            --사원 CD_EMPLOY
        @NM_EMP,              
        @BILL_PARTNER,      --매출처 CD_PARTNER
        @NM_PARTNER,
        NULL,               --예적금코드 CD_DEPOSIT
        NULL,               --NM_DEPOSIT
        NULL,               --카드번호 CD_CARD
        NULL,               --NM_CARD
        NULL,               --은행코드 CD_BANK  : MA_PARTNER에서 FG_PARTNER = '002' 인것
        NULL,               --NM_BANK
        @NO_ITEM,           --품목코드 NO_ITEM
        @NM_ITEM,           --NM_ITEM
        @FG_TAX,            --세무구분 TP_TAX
        @NM_TAX,
        @FG_TRANS,          --거래구분 CD_TRADE
        NULL,               --NM_TRADE
        @CD_EXCH,           --환종
        @NM_EXCH,           --@NM_EXCH
        @NO_IO ,			--CD_UMNG1
        @V_UD_NM_02 ,       --CD_UMNG2
        @V_CD_UMNG3,        --CD_UMNG3
        @V_UD_NM_04,        --CD_UMNG4
        @V_UD_NM_03,        --CD_UMNG5
        @NO_COMPANY,        --NO_RES
        @AM_SUPPLY,         --AM_SUPPLY
        @V_NO_IV,           --관리번호 = 계산서번호 CD_MNG
        --@DT_PROCESS,      --거래일자, 시작일자, 발생일자 DT_START
        @P_DT_RCP_RSV,      --수금에정일
        @P_DT_RCP_RSV,      --NULL, -- 만기일자 DT_END (수금예정일 똑같이 넘겨줌:대신정보 20100621)
        @V_RT_EXCH,         --@RT_EXCH, -- 환율
        @AM_EXDO,           --@AM_EX    -- 외화금액 AM_EXDO
        '110',              --모듈구분(매출:002) NO_MODULE
        @NO_IV,             --모듈관리번호 = 타모듈pkey NO_MDOCU
        @CD_ACCT,           --지출결의코드 CD_EPNOTE
        @ID_WRITE,          --전표처리자
        @CD_ACCT,           --예산계정 CD_BGACCT
        NULL,               --결의구분 TP_EPNOTE
        @P_PUMM,            --품의내역 NM_PUMM
        @CUR_DATE,          --현재일자로20100506 @P_DT_PROCESS,        - 작성일자 DT_WRITE              
        0,                  --AM_ACTSUM
        0,                  --AM_JSUM
        'N',                --YN_GWARE
        NULL,               --사업계획코드 CD_BIZPLAN
        NULL,               --CD_ETC
        @P_ERRORCODE,
        @P_ERRORMSG,
        NULL,
        NULL ,
        @P_FG_BILL,
        NULL,               --@P_NM_ARRIVE NVARCHAR(30) = NULL, -- 미착품구분 (전방추가)     
        NULL,               --@P_CD_PAYDEDUCT  NVARCHAR(3) = NULL,    --인사추가 (YTN)    
        NULL,               --@P_JUMIN_NO  NVARCHAR(40) = NULL,    --EC 추가 (본죽)    
        NULL,               --@P_DT_PAY   VARCHAR(8) = NULL,  --인사추가 (YTN)    
        @NO_IV,               --@P_NO_INV  NVARCHAR(20) = NULL,    -- INVOICE NO    
        NULL,               --@P_NO_TO   NVARCHAR(20) = NULL,     -- 면장번호(NO_TO)    
        @VAT_TAX,            --NUMERIC(17,4) = 0		--부가세 (전방추가)
		NULL, -- 구매거래조건(삼보)
	    NULL,
	    NULL, --LINE : 90
	    NULL,  -- 수금 자타구분 20131218 김현정
	    NULL,  -- 수금 자타구분 20131218 김현정
	    NULL,   -- 수금 발행인 20131218 김현정
	    NULL,
	    0, -- 유니포인트 전용 추가
	    0, -- 유니포인트 전용 추가
	    NULL, -- 업체전용
	    0,  -- 업체전용
	    NULL,
	    NULL,    -- 증빙(주홍열추가 2016.10.26)D20160929039  
	    @YN_ISS 

    IF @T_CD_RELATION = '31'                     
    BEGIN                 
        /* FI_TAX 만들기*/   
	    EXEC UP_FI_AUTODOCU_TAX @CD_COMPANY, @CD_PC, @P_NO_DOCU, @P_NO_DOLINE, @AM_SUPPLY, @YN_ISS, 0, @NO_COMPANY
	
	    IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택  

        /* 20100223추가 만든 FI_TAX에 품목내역채우기*/
        EXEC UP_SA_IVMNG_TRANSFER_DOCU_TAX @CD_COMPANY, @V_NO_IV, @CD_PC, @P_NO_DOCU, @P_NO_DOLINE, 
                                           @P_OPT_CD_SVC,             --발행방법                
                                           @P_OPT_ITEM_RPT_GUBUN,     --내역표시구분            
                                           @P_OPT_ITEM_RPT_TEXT,      --내역표시임의내용        
                                           @P_OPT_ITEM_NM_GUBUN,      --품목표시구분            
                                           @P_OPT_SELL_DAM_GUBUN,     --공급자연락처 표시구분   
                                           @P_OPT_SELL_DAM_NM,        --Login 한 USER의 사원명을 넘긴다. 
                                           @P_OPT_SELL_DAM_EMAIL,     --공급자e-mail          
                                           @P_OPT_SELL_DAM_MOBIL      --공급자Hp    
	 
	    IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택
    END    
   
    IF @TP_ACAREA = '4' --반제원인전표에 회계전표 그룹핑추적용 데이터생성 (수금상세전표에서 참조 20100330)
    BEGIN
        INSERT INTO SA_IV_DOCU_MAPP 
        (
            CD_COMPANY,	NO_IV, CD_PC, NO_DOCU, NO_DOLINE,
            CD_CC, CD_PJT, CD_EXCH, CD_DEPT, NO_EMP
        )
        VALUES
        (
            @CD_COMPANY, @NO_IV, @CD_PC, @P_NO_DOCU, @P_NO_DOLINE,
            @CD_CC, @CD_PJT, @CD_EXCH, @CD_DEPT, @NO_EMP
        )

        IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택
    END

    FETCH NEXT FROM SRC_CURSOR INTO @CD_COMPANY, @NO_IV, @CD_BIZAREA, @CD_WDEPT, @BILL_PARTNER,
                                    @FG_TAX ,@ID_WRITE, @DT_PROCESS, @AM_DR, @AM_CR,
                                    @AM_SUPPLY, @DT_TAX, @FG_TRANS, @CD_CC, @NO_EMP,
                                    @CD_PJT, @CD_EXCH, @NM_EXCH, @RT_EXCH, @AM_EX,
                                    @CD_DEPT, @CD_PC, @NM_BIZAREA, @FG_AIS, @CD_ACCT,
                                    @CLS_ITEM, @TP_DRCR, @NM_DEPT, @NM_CC, @NM_EMP,
                                    @NM_PJT, @NM_PARTNER, @YN_ISS, @T_CD_RELATION, @NM_TAX, @NO_COMPANY,
                                    @TP_ACAREA ,@CD_DOCU, @CD_FUND, @P_NM_TP, @V_UD_NM_01,
                                    @V_UD_NM_02, @V_UD_NM_03, @V_UD_NM_04, @VAT_TAX, @NO_ITEM, @NM_ITEM, @NO_IO     
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

-- 전표처리가 제대로 되었으면
UPDATE SA_IVH SET TP_AIS = 'Y' WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV AND TP_AIS = 'N'

IF (@@ROWCOUNT = 0)
BEGIN
      SET   @ERRMSG = '전표처리에러- 해당 매출데이터의 상태가 처리중에 변경되었습니다. : ' + @P_NO_IV
      GOTO ERROR
END

RETURN

ERROR:
    ROLLBACK TRAN
    RAISERROR (@ERRMSG, 18, 1)
GO

