USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_PR_OPOUT_IV_TRANS_DOCU]    Script Date: 2023-04-04 오후 3:55:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [NEOE].[UP_CZ_PR_OPOUT_IV_TRANS_DOCU]    
(    
    @P_CD_COMPANY       NVARCHAR(7),    
    @P_NO_IV            NVARCHAR(20),    
    @P_NO_MODULE        NVARCHAR(3)        --회계전표유형 : 국내매입(210), 구매/외주(001)    
)    
AS    
DECLARE @ERRMSG         NVARCHAR(255)    
    
--의문점.    
--1. CD_CC 를 W/C에서 끌어오지 않나? (W/C는 무시하나)    
--2. CD_CC 를 라인으로 가져가지는 않는가?    
--3. CD_EXCH 를 라인으로 가져가지는 않는가?(환종이 틀리면 묶어갈수가 없는가?)    
--4. RT_EXCH 도 3번과 같은 얘기    
--5. 반품여부는 고려하지 않는가?    
--6. 거래구분 컬럼은 가져가는것이 좋지 않나?    
    
--PR_OPOUT_IVH(매입등록 헤더 테이블)    
--PR_OPOUT_IVL(매입등록 라인 테이블)    
    
DECLARE @IN_CD_COMPANY  NVARCHAR(7)    
DECLARE @IN_NO_MDOCU    NVARCHAR(20)    
DECLARE @IN_CD_PARTNER  NVARCHAR(20)    
DECLARE @IN_CD_CC       NVARCHAR(12)    
DECLARE @IN_CD_PJT      NVARCHAR(20)    
DECLARE @IN_ID_WRITE    NVARCHAR(10)    
DECLARE @IN_CD_BIZAREA  NVARCHAR(7)    
DECLARE @IN_TP_TAX      NVARCHAR(3)    
DECLARE @IN_NM_TP_TAX   NVARCHAR(60)    
DECLARE @IN_FG_TRANS    NVARCHAR(3)    
DECLARE @IN_CD_WDEPT    NVARCHAR(12)    
DECLARE @IN_DT_ACCT     NVARCHAR(8)    
DECLARE @IN_CD_PC       NVARCHAR(7)    
DECLARE @IN_CD_ACCT     NVARCHAR(10)    
DECLARE @IN_AM_DR       NUMERIC(19,4)    
DECLARE @IN_AM_CR       NUMERIC(19,4)    
DECLARE @IN_AM_SUPPLY   NUMERIC(19,4)    
DECLARE @IN_TP_DRCR     NCHAR(1)    
DECLARE @IN_CD_EXCH     NVARCHAR(3)    
DECLARE @IN_RT_EXCH     NUMERIC(17,4)    
DECLARE @IN_CD_EMPLOY   NVARCHAR(10)    
DECLARE @IN_AM_EXDO     NUMERIC(19,4)    
DECLARE @IN_NM_BIZAREA  NVARCHAR(50)    
DECLARE @IN_NM_CC       NVARCHAR(50)    
DECLARE @IN_NM_KOR      NVARCHAR(50)    
DECLARE @IN_LN_PARTNER  NVARCHAR(50)    
DECLARE @IN_NM_PROJECT  NVARCHAR(50)    
DECLARE @IN_NM_DEPT     NVARCHAR(50)    
DECLARE @IN_CD_RELATION NVARCHAR(2)    
    
DECLARE @TP_ACAREA      NVARCHAR(3)    
    
DECLARE @P_NO_DOLINE    INT

DECLARE @P_NO_DOCU      NVARCHAR(20)    --전표번호    
DECLARE @P_DT_PROCESS   NVARCHAR(8)     --매입일자    
DECLARE @DTS_INSERT     NVARCHAR(8)    
    
DECLARE @P_NOTE NVARCHAR(50)      
DECLARE @P_NM_TP NVARCHAR(20)      
    
DECLARE @FG_TPPURCHASE  NVARCHAR(3),    --MA_AISPOSTH.CD_TP : 계정처리유형의 매입형태        
        @FG_TRANS       NVARCHAR(3)    --거래구분 : 국내('001')        
        

-- 2011.09.22 Rejina 통화구분환종[MA_B000005]        
DECLARE @IN_CD_EXCH_NM  NVARCHAR(100)      
        
--TEST 임시 데이타(컬럼을 만들거나 값을 넣어줘야 할것 같다.)        
    
SELECT  @FG_TRANS = '001'         
    
    
--SELECT  @FG_TPPURCHASE = '001', MI.CD_CC = '1000'        
-- FG_TPPURCHASE,CD_CC     
    
-- <<START>> # 2011/04/14. Modified by REJINA ------------------------------            
SELECT @FG_TPPURCHASE = FG_TPPURCHASE,      
       @P_DT_PROCESS = DT_IV    
  FROM PR_OPOUT_IVH    
 WHERE CD_COMPANY  = @P_CD_COMPANY    
   AND NO_IV   = @P_NO_IV        
            
-- <<START>> # 2011/04/13. Modified by REJINA ------------------------------    
DECLARE @IN_NO_COMPANY			NVARCHAR(20)  -- 사업자번호    
DECLARE @V_IN_NO_MDOCU			NVARCHAR(20)       
DECLARE @DT_PAY_PREARRANGED		NVARCHAR(8)   -- 지급예정일 (전표 자금예정일로 넘어간다)                                                  
DECLARE @P_ERRORCODE			NCHAR(10)                                               
DECLARE @P_ERRORMSG				NVARCHAR(300)          
---<<END>>   # 2011/04/13. Modified by REJINA ------------------------------    


-- <<START>> # 2011/08/24. Modified by REJINA ------------------------------    
DECLARE @FG_PAYBILL				NVARCHAR(3)
DECLARE @DT_DUE					NVARCHAR(8)       
---<<END>>   # 2011/04/13. Modified by REJINA ------------------------------    

    
SET @P_NO_DOLINE = 0    
SET @DTS_INSERT = CONVERT(VARCHAR(8), GETDATE(), 112)    
    
-- 물류/제조환경설정 외주매입전표 그룹옵션 추가
DECLARE @V_CD_EXC2 NVARCHAR(3)
SET		@V_CD_EXC2 = ISNULL(  (	SELECT	ISNULL(CD_EXC, '000') 
								FROM	MA_EXC 
								WHERE	CD_COMPANY = @P_CD_COMPANY 
								AND		EXC_TITLE  = '공정외주매입전표-그룹옵션설정' ), '000'	)

DECLARE @V_EXC_AMEX	NVARCHAR(3)
SELECT	@V_EXC_AMEX = ISNULL(CD_EXC,'000') FROM MA_EXC_MENU WHERE CD_COMPANY = @P_CD_COMPANY AND CD_TITLE = 'PR_A00000001' AND ID_MENU = 'P_PR_OPOUT_IV_MNG' --전표금액설정 : 전표생성시 환종이 KRW일경우에는 외화금액0으로 입력

-- 전표번호 채번    
EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT    
    
IF (@@ERROR <> 0 ) BEGIN SELECT @ERRMSG = '[UP_PR_OPOUT_IV_TRANS_DOCU]작업을 정상적으로 처리하지 못했습니다.' GOTO ERROR END    
    
DECLARE CUR_PR_OPOUT_IV_TRANS_DOCU CURSOR FOR    
    
	--외주가공비(차변 : DEBIT)을 넣는다.    
	SELECT	H.CD_COMPANY, 
			L.NO_IV NO_MDOCU, 
			H.CD_PARTNER, 
			MI.CD_CC AS CD_CC, 
			CASE WHEN @V_CD_EXC2 IN ('100', '200') THEN '' ELSE L.CD_PJT END AS CD_PJT,
			H.NO_EMP ID_WRITE,  
			H.CD_BIZAREA_TAX,    
			H.FG_TAX TP_TAX,     
			N.NM_SYSDEF NM_TP_TAX, 
			'001' FG_TRANS, 
			E.CD_DEPT CD_WDEPT, 
			H.DT_IV DT_ACCT, 
			B.CD_PC, 
			A.CD_ACCT,     
			SUM(L.AM_CLS) AM_DR, 
			0 AM_CR,     
			0 AM_SUPPLY,     
			'1' TP_DRCR, 
			H.CD_EXCH, 
			H.RT_EXCH, 
			H.NO_EMP CD_EMPLOY,     
			SUM(L.AM_EXCLS) AM_EX_DR,     
			B.NM_BIZAREA, 
			C.NM_CC, 
			E.NM_KOR, 
			P.LN_PARTNER, 
			CASE WHEN @V_CD_EXC2 IN ('100', '200') THEN '' ELSE PRJ.NM_PROJECT END AS NM_PROJECT,
			D.NM_DEPT, 
			'10' CD_RELATION,     
			(CASE F.TP_DRCR WHEN '1' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA,

			-- <<START>> # 2011/04/13. Modified by REJINA ------------------------------    
			P.NO_COMPANY,				-- 사업자번호    
			H.DT_PAY_PREARRANGED,		-- 지급예정일자    
			-- <<END>>   # 2011/04/13. Modified by REJINA ------------------------------    
	    
	    
	    
			-- <<START>> # 2011/08/24. Modified by REJINA ------------------------------    
			H.FG_PAYBILL,				--지급구분
			H.DT_DUE					--만기일자
			-- <<END>>   # 2011/04/23. Modified by REJINA ------------------------------    
	        
	 FROM	PR_OPOUT_IVH H    
			INNER JOIN PR_OPOUT_IVL L ON L.CD_COMPANY	= @P_CD_COMPANY    
									 AND L.NO_IV		= H.NO_IV    
			INNER JOIN MA_AISPOSTL A ON A.CD_COMPANY	= @P_CD_COMPANY    
									 AND A.FG_TP		= '500'                --외주형태    
									 AND A.CD_TP		= CASE WHEN ISNULL(L.FG_TPPURCHASE, '') = '' THEN @FG_TPPURCHASE
															   ELSE L.FG_TPPURCHASE END
									 AND A.FG_AIS		= '501'        --외주가공비    
			INNER JOIN FI_ACCTCODE F ON F.CD_COMPANY	= @P_CD_COMPANY    
									 AND F.CD_ACCT		= A.CD_ACCT   
			INNER JOIN MA_BIZAREA B ON B.CD_COMPANY		= @P_CD_COMPANY    
									--AND B.CD_BIZAREA		= H.CD_BIZAREA_TAX (2011/04/13 Rejina : 부가세사업장이 없는경우 처리)    
									 AND B.CD_BIZAREA	= (CASE WHEN ISNULL(H.CD_BIZAREA_TAX,'') <> '' THEN H.CD_BIZAREA_TAX     
																ELSE (SELECT Z.CD_BIZAREA     
																		FROM MA_PLANT Z     
																	   WHERE H.CD_COMPANY	= Z.CD_COMPANY    
																		 AND H.CD_PLANT		= Z.CD_PLANT) END)  
			LEFT OUTER JOIN MA_EMP E ON E.CD_COMPANY	= @P_CD_COMPANY    
									AND E.NO_EMP		= H.NO_EMP    
			LEFT OUTER JOIN MA_PARTNER P ON P.CD_COMPANY= @P_CD_COMPANY    
									AND P.CD_PARTNER	= H.CD_PARTNER    
			LEFT OUTER JOIN MA_DEPT D ON D.CD_COMPANY	= @P_CD_COMPANY    
									 AND D.CD_DEPT		= E.CD_DEPT    
			LEFT OUTER JOIN MA_CODEDTL N ON N.CD_COMPANY = @P_CD_COMPANY    
									 AND N.CD_FIELD		 = 'MA_B000046'    
									 AND N.CD_SYSDEF	= H.FG_TAX    
			LEFT OUTER JOIN SA_PROJECTH PRJ ON PRJ.CD_COMPANY = @P_CD_COMPANY    
										AND PRJ.NO_PROJECT = L.CD_PJT    
										AND PRJ.NO_SEQ = (  SELECT MAX(NO_SEQ) FROM SA_PROJECTH    
															WHERE CD_COMPANY = @P_CD_COMPANY    
																	AND NO_PROJECT = PRJ.NO_PROJECT )    
			LEFT OUTER JOIN MA_PITEM MI ON MI.CD_COMPANY = @P_CD_COMPANY
									   AND MI.CD_ITEM = L.CD_ITEM
			LEFT OUTER JOIN MA_CC C ON C.CD_COMPANY		= @P_CD_COMPANY    
								   AND C.CD_CC			= MI.CD_CC    
	WHERE H.CD_COMPANY	= @P_CD_COMPANY     
	  AND H.NO_IV		= @P_NO_IV    
	  AND H.YN_DOCU		= 'N'      --전표발행여부    
	GROUP BY	H.CD_COMPANY, 
				L.NO_IV, 
				H.CD_PARTNER, 
				MI.CD_CC,
				CASE WHEN @V_CD_EXC2 IN ('100', '200') THEN '' ELSE L.CD_PJT END,
				H.NO_EMP,  
				H.CD_BIZAREA_TAX,    
				H.FG_TAX,     
				N.NM_SYSDEF, 
				E.CD_DEPT, 
				H.DT_IV, 
				B.CD_PC, 
				A.CD_ACCT,     
				H.CD_EXCH, 
				H.RT_EXCH, 
				H.NO_EMP,        
				B.NM_BIZAREA, 
				C.NM_CC, 
				E.NM_KOR, 
				P.LN_PARTNER, 
				CASE WHEN @V_CD_EXC2 IN ('100', '200') THEN '' ELSE PRJ.NM_PROJECT END,
				D.NM_DEPT, 
				(CASE F.TP_DRCR WHEN '1' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END),

				-- <<START>> # 2011/04/13. Modified by REJINA ------------------------------    
				P.NO_COMPANY,				-- 사업자번호    
				H.DT_PAY_PREARRANGED,		-- 지급예정일자    
				-- <<END>>   # 2011/04/13. Modified by REJINA ------------------------------    

				-- <<START>> # 2011/08/24. Modified by REJINA ------------------------------    
				H.FG_PAYBILL,				--지급구분
				H.DT_DUE					--만기일자
				-- <<END>>   # 2011/04/23. Modified by REJINA ------------------------------    
	    
	UNION ALL    
	    
	--부가세대급금(차변 : DEBIT)을 넣는다.    
	SELECT	H.CD_COMPANY, 
			L.NO_IV NO_MDOCU, 
			H.CD_PARTNER, 
			NULL CD_CC, 
			NULL CD_PJT, 
			H.NO_EMP ID_WRITE,
			H.CD_BIZAREA_TAX,
			H.FG_TAX TP_TAX,     
			N.NM_SYSDEF NM_TP_TAX, 
			@FG_TRANS FG_TRANS, 
			E.CD_DEPT CD_WDEPT, 
			H.DT_IV DT_ACCT, 
			B.CD_PC, 
			A.CD_ACCT,     
			SUM(L.AM_VAT) AM_DR, 
			0 AM_CR,     
			SUM(L.AM_CLS) AM_SUPPLY,     
			'1' TP_DRCR, 
			H.CD_EXCH, 
			H.RT_EXCH, 
			NULL CD_EMPLOY, --L.NO_EMP CD_EMPLOY,     
			SUM(L.AM_VAT) AM_EX_DR,     
			B.NM_BIZAREA, 
			'' NM_CC, 
			E.NM_KOR, 
			P.LN_PARTNER, 
			'' NM_PROJECT, 
			D.NM_DEPT, 
			'30' CD_RELATION,     
			(CASE F.TP_DRCR WHEN '1' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA,
			-- <<START>> # 2011/04/13. Modified by REJINA ------------------------------    
			P.NO_COMPANY,				-- 사업자번호    
			H.DT_PAY_PREARRANGED,		-- 지급예정일자    
			-- <<END>>   # 2011/04/13. Modified by REJINA ------------------------------    
	        
			-- <<START>> # 2011/08/24. Modified by REJINA ------------------------------    
			H.FG_PAYBILL,				--지급구분
			H.DT_DUE					--만기일자
			-- <<END>>   # 2011/04/23. Modified by REJINA ------------------------------    
			
	FROM PR_OPOUT_IVH H          
		INNER JOIN PR_OPOUT_IVL L ON L.CD_COMPANY = @P_CD_COMPANY    
							AND L.NO_IV = H.NO_IV    
		INNER JOIN MA_AISPOSTL A ON A.CD_COMPANY = @P_CD_COMPANY    
								AND A.FG_TP = '500'                --외주형태    
								AND A.CD_TP = CASE WHEN ISNULL(L.FG_TPPURCHASE, '') = '' THEN @FG_TPPURCHASE
												   ELSE L.FG_TPPURCHASE END
								AND A.FG_AIS = '502'        --부가세대급금    
		INNER JOIN FI_ACCTCODE F ON F.CD_COMPANY = @P_CD_COMPANY    
								AND F.CD_ACCT = A.CD_ACCT    
		INNER JOIN MA_BIZAREA B ON B.CD_COMPANY = @P_CD_COMPANY    
	                                
								--AND B.CD_BIZAREA = H.CD_BIZAREA_TAX    
		   AND B.CD_BIZAREA = (CASE WHEN ISNULL(H.CD_BIZAREA_TAX,'') <> '' THEN H.CD_BIZAREA_TAX     
					  ELSE (SELECT Z.CD_BIZAREA     
							  FROM MA_PLANT Z     
							 WHERE H.CD_COMPANY = Z.CD_COMPANY    
							AND H.CD_PLANT  = Z.CD_PLANT) END)                                 
	-------------------------------------------------------------------    
	    
		LEFT OUTER JOIN MA_EMP E ON E.CD_COMPANY = @P_CD_COMPANY    
								AND E.NO_EMP = H.NO_EMP    
		LEFT OUTER JOIN MA_PARTNER P ON P.CD_COMPANY = @P_CD_COMPANY    
								AND P.CD_PARTNER = H.CD_PARTNER    
		LEFT OUTER JOIN MA_DEPT D ON D.CD_COMPANY = @P_CD_COMPANY    
								AND D.CD_DEPT = E.CD_DEPT    
		LEFT OUTER JOIN MA_CODEDTL N ON N.CD_COMPANY = @P_CD_COMPANY    
									AND N.CD_FIELD = 'MA_B000046'    
									AND N.CD_SYSDEF = H.FG_TAX    
	WHERE H.CD_COMPANY		= @P_CD_COMPANY    
	  AND H.NO_IV			= @P_NO_IV    
	  AND H.YN_DOCU			= 'N'        --전표발행여부    
	GROUP BY	H.CD_COMPANY, 
				L.NO_IV, 
				H.CD_PARTNER, 
				H.NO_EMP,
				H.CD_BIZAREA_TAX,
				H.FG_TAX, 
				N.NM_SYSDEF, 
				E.CD_DEPT, 
				H.DT_IV, 
				B.CD_PC, 
				A.CD_ACCT,     
				H.CD_EXCH, 
				H.RT_EXCH, --L.NO_EMP,     
				B.NM_BIZAREA, 
				E.NM_KOR, 
				P.LN_PARTNER, 
				D.NM_DEPT, 
				F.TP_DRCR, 
				F.YN_BAN,
				-- <<START>> # 2011/04/13. Modified by REJINA ------------------------------    
				P.NO_COMPANY,				-- 사업자번호    
		 		H.DT_PAY_PREARRANGED,		-- 지급예정일자    
				-- <<END>>   # 2011/04/13. Modified by REJINA ------------------------------    

				-- <<START>> # 2011/08/24. Modified by REJINA ------------------------------    
				H.FG_PAYBILL,				--지급구분
				H.DT_DUE					--만기일자
				-- <<END>>   # 2011/04/23. Modified by REJINA ------------------------------    

	UNION ALL    
	    
	--채무계정[외상매입금](대변 : CREDIT)을 넣는다.    
	SELECT		H.CD_COMPANY, 
				L.NO_IV NO_MDOCU, 
				H.CD_PARTNER, 
				NULL AS CD_CC, 
				CASE WHEN @V_CD_EXC2 IN ('100', '300') THEN '' ELSE L.CD_PJT END AS CD_PJT,
				H.NO_EMP ID_WRITE,
				H.CD_BIZAREA_TAX,
				H.FG_TAX TP_TAX,
				N.NM_SYSDEF NM_TP_TAX, 
				@FG_TRANS FG_TRANS, 
				E.CD_DEPT CD_WDEPT, 
				H.DT_IV DT_ACCT, 
				B.CD_PC, 
				A.CD_ACCT,     
				0 AM_DR,     
				SUM(L.AM_CLS) +    
				SUM(L.AM_VAT) AM_CR,     
				0 AM_SUPPLY,     
				'2' TP_DRCR, 
				H.CD_EXCH, 
				H.RT_EXCH, 
				H.NO_EMP CD_EMPLOY,     
				SUM(L.AM_EXCLS) AM_EX_DR,     
				B.NM_BIZAREA, 
				C.NM_CC, 
				E.NM_KOR, 
				P.LN_PARTNER, 
				CASE WHEN @V_CD_EXC2 IN ('100', '300') THEN '' ELSE PRJ.NM_PROJECT END AS NM_PROJECT, 
				D.NM_DEPT, 
				'10' CD_RELATION,     
				(CASE F.TP_DRCR WHEN '2' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA,

				-- <<START>> # 2011/04/13. Modified by REJINA ------------------------------    
				P.NO_COMPANY,				-- 사업자번호    
				H.DT_PAY_PREARRANGED,		-- 지급예정일자    
				-- <<END>>   # 2011/04/13. Modified by REJINA ------------------------------    
				
				-- <<START>> # 2011/08/24. Modified by REJINA ------------------------------    
				H.FG_PAYBILL,				--지급구분
				H.DT_DUE					--만기일자
				-- <<END>>   # 2011/04/23. Modified by REJINA ------------------------------    
			
	FROM PR_OPOUT_IVH H    
		INNER JOIN PR_OPOUT_IVL L ON L.CD_COMPANY = @P_CD_COMPANY    
							AND L.NO_IV = H.NO_IV    
		INNER JOIN MA_AISPOSTL A ON A.CD_COMPANY = @P_CD_COMPANY    
								AND A.FG_TP = '500'                --외주형태    
								AND A.CD_TP = CASE WHEN ISNULL(L.FG_TPPURCHASE, '') = '' THEN @FG_TPPURCHASE
												   ELSE L.FG_TPPURCHASE END
								AND A.FG_AIS = '503'        --외상매입금    
		INNER JOIN FI_ACCTCODE F ON F.CD_COMPANY = @P_CD_COMPANY    
								AND F.CD_ACCT = A.CD_ACCT    
		INNER JOIN MA_BIZAREA B ON B.CD_COMPANY = @P_CD_COMPANY    
							   --AND B.CD_BIZAREA = H.CD_BIZAREA_TAX    
		   AND B.CD_BIZAREA = (CASE WHEN ISNULL(H.CD_BIZAREA_TAX,'') <> '' THEN H.CD_BIZAREA_TAX     
					  ELSE (SELECT Z.CD_BIZAREA     
							  FROM MA_PLANT Z     
							 WHERE H.CD_COMPANY = Z.CD_COMPANY    
							AND H.CD_PLANT  = Z.CD_PLANT) END)    
		INNER JOIN MA_PITEM MI ON MI.CD_COMPANY = @P_CD_COMPANY AND MI.CD_ITEM = L.CD_ITEM
	-------------------------------------------------------------------    
	    
		LEFT OUTER JOIN MA_CC C ON C.CD_COMPANY = @P_CD_COMPANY    
								AND C.CD_CC = '1000'    
		LEFT OUTER JOIN MA_EMP E ON E.CD_COMPANY = @P_CD_COMPANY    
								AND E.NO_EMP = H.NO_EMP    
		LEFT OUTER JOIN MA_PARTNER P ON P.CD_COMPANY = @P_CD_COMPANY    
								AND P.CD_PARTNER = H.CD_PARTNER    
		LEFT OUTER JOIN MA_DEPT D ON D.CD_COMPANY = @P_CD_COMPANY    
								AND D.CD_DEPT = E.CD_DEPT    
		LEFT OUTER JOIN MA_CODEDTL N ON N.CD_COMPANY = @P_CD_COMPANY    
								AND N.CD_FIELD = 'MA_B000046'    
								AND N.CD_SYSDEF = H.FG_TAX    
		LEFT OUTER JOIN SA_PROJECTH PRJ ON PRJ.CD_COMPANY = @P_CD_COMPANY    
								AND PRJ.NO_PROJECT = L.CD_PJT    
								AND PRJ.NO_SEQ = (  SELECT MAX(NO_SEQ) FROM SA_PROJECTH    
													WHERE CD_COMPANY = @P_CD_COMPANY    
													AND NO_PROJECT = PRJ.NO_PROJECT )    
	WHERE H.CD_COMPANY	= @P_CD_COMPANY    
	  AND H.NO_IV		= @P_NO_IV    
	  AND H.YN_DOCU		= 'N'        --전표발행여부    
	GROUP BY	H.CD_COMPANY, 
				L.NO_IV, 
				H.CD_PARTNER, 
				CASE WHEN @V_CD_EXC2 IN ('100', '300') THEN '' ELSE L.CD_PJT END, 
				H.NO_EMP,
				H.CD_BIZAREA_TAX,
				H.FG_TAX, 
				N.NM_SYSDEF, 
				E.CD_DEPT, 
				H.DT_IV, 
				B.CD_PC, 
				A.CD_ACCT,     
				H.CD_EXCH, 
				H.RT_EXCH, 
				H.NO_EMP,     
				B.NM_BIZAREA, 
				C.NM_CC, 
				E.NM_KOR, 
				P.LN_PARTNER, 
				CASE WHEN @V_CD_EXC2 IN ('100', '300') THEN '' ELSE PRJ.NM_PROJECT END, 
				D.NM_DEPT, 
				F.TP_DRCR, 
				F.YN_BAN,
				-- <<START>> # 2011/04/13. Modified by REJINA ------------------------------    
				P.NO_COMPANY,				-- 사업자번호    
		 		H.DT_PAY_PREARRANGED,		-- 지급예정일자    
				-- <<END>>   # 2011/04/13. Modified by REJINA ------------------------------    

				-- <<START>> # 2011/08/24. Modified by REJINA ------------------------------    
				H.FG_PAYBILL,				--지급구분
				H.DT_DUE					--만기일자
				-- <<END>>   # 2011/04/23. Modified by REJINA ------------------------------    
     
OPEN CUR_PR_OPOUT_IV_TRANS_DOCU    
    
FETCH NEXT FROM CUR_PR_OPOUT_IV_TRANS_DOCU     
    
 INTO	@IN_CD_COMPANY,
		@IN_NO_MDOCU, 
		@IN_CD_PARTNER, 
		@IN_CD_CC, 
		@IN_CD_PJT, 
		@IN_ID_WRITE, 
		@IN_CD_BIZAREA,    
		@IN_TP_TAX, 
		@IN_NM_TP_TAX, 
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
		@IN_NM_CC,     
		@IN_NM_KOR, 
		@IN_LN_PARTNER, 
		@IN_NM_PROJECT, 
		@IN_NM_DEPT, 
		@IN_CD_RELATION, 
		@TP_ACAREA,
		
		@IN_NO_COMPANY,				--부가사업장
		@DT_PAY_PREARRANGED,		--지급예정일자
		@FG_PAYBILL,				--지급구분
		@DT_DUE						--만기일자
	    

WHILE @@FETCH_STATUS = 0    
BEGIN    
----------------------------------------------------------------------------------------------    
    
    
    SET @P_NO_DOLINE = @P_NO_DOLINE + 1    
    SET @FG_TPPURCHASE = CASE WHEN ISNULL(@FG_TPPURCHASE,'') = '' THEN '001' ELSE @FG_TPPURCHASE END
    
    -- 매입형태명 가져오기    
    --SELECT @P_NM_TP = NM_TP    
    --FROM MA_AISPOSTH    
    --WHERE CD_COMPANY = @P_CD_COMPANY    
    --    AND FG_TP = '500'    
    --    AND CD_TP = @FG_TPPURCHASE   
    
    --SELECT @P_NOTE = @IN_LN_PARTNER + ' ' + @P_NM_TP      

	DECLARE @V_MAX_NM_ITEM NVARCHAR(200)
	
	SELECT @V_MAX_NM_ITEM = B.NM_ITEM
	FROM
	(
		SELECT P.NM_ITEM, ROW_NUMBER() OVER (PARTITION BY L.NO_IV ORDER BY L.AM_CLS DESC) AS RANKS
		FROM PR_OPOUT_IVL L
		INNER JOIN MA_PITEM P ON L.CD_ITEM = P.CD_ITEM AND L.CD_PLANT = P.CD_PLANT AND L.CD_COMPANY = P.CD_COMPANY
		WHERE L.CD_COMPANY = @P_CD_COMPANY
		AND   L.NO_IV = @P_NO_IV
	)B
	WHERE B.RANKS = 1

	SELECT @P_NOTE = ISNULL(@V_MAX_NM_ITEM, '') + ' 외 ' + CONVERT(NVARCHAR, COUNT(L.CD_ITEM) - 1) + '건'
	FROM PR_OPOUT_IVL L
	INNER JOIN MA_PITEM P ON L.CD_ITEM = P.CD_ITEM AND L.CD_PLANT = P.CD_PLANT AND L.CD_COMPANY = P.CD_COMPANY
	WHERE L.CD_COMPANY = @P_CD_COMPANY
	AND   L.NO_IV = @P_NO_IV
	GROUP BY L.NO_IV
    
 IF @IN_CD_RELATION <> '10' -- 연동 항목이 '일반' 이 아니면 NO_MDOCU값 넣어준다.                                                            
  SET @V_IN_NO_MDOCU = @IN_NO_MDOCU                                                             
 ELSE                   
  SET @V_IN_NO_MDOCU = NULL     
    
    --2011.09.22 Rejina 환종명칭 넘겨주기..
    SELECT @IN_CD_EXCH_NM = NM_SYSDEF
      FROM MA_CODEDTL A
     WHERE CD_COMPANY = @P_CD_COMPANY   
       AND CD_FIELD   = 'MA_B000005'
       AND CD_SYSDEF  = @IN_CD_EXCH
       
	IF (ISNULL(@V_EXC_AMEX, '') = '100')
	BEGIN
		IF (@IN_CD_EXCH = '000')
		BEGIN
			SET @IN_AM_EXDO = 0
		END
	END
	
    -- # 2011/04/13. Modified by REJINA  ------------------------------    
    --IF (@IN_AM_EXDO <> 0)    
	IF (@IN_AM_DR <> 0 OR @IN_AM_CR <> 0)
    BEGIN    
      
	  EXEC UP_FI_AUTODOCU_1        
				@P_NO_DOCU,				-- 전표번호            
				@P_NO_DOLINE,			-- 라인번호            
				@IN_CD_PC,				-- 회계단위            
				@IN_CD_COMPANY,			-- 회사코드                      
				@IN_CD_WDEPT,			-- 작성부서            
				@IN_ID_WRITE,           -- 작성자            
				@IN_DT_ACCT,            -- 매출일자 = 회계일자 = 처리일자            
				0,                      -- 회계번호 미결이니까 NO_ACCT            
				'3',                    -- 전표구분-대체  TP_DOCU            
				'63',                   -- 전표유형-일반  CD_DOCU            
				'1',                    -- 전표상태-미결  ST_DOCU            
				NULL,                   -- 승인자            
				@IN_TP_DRCR,            -- 차대구분    TP_DRCR            
				@IN_CD_ACCT,            -- 계정코드            
				@P_NOTE,                -- 적요            
				@IN_AM_DR,				-- 차변금액    AM_DR            
				@IN_AM_CR,				-- 대변금액    AM_CR            
				@TP_ACAREA,				-- '4' 일 경우 반제원인전표이므로 FI_BANH에 Insert된다(전에 있던 주석 : 본지점구분-안함 TP_ACAREA)            
				@IN_CD_RELATION,		-- 연동항목-일반  CD_RELATION     
				NULL,                   -- 예산코드    CD_BUDGET                      
				NULL,                   -- 자금과목    CD_FUND                      
				NULL,                   -- 원인전표번호   NO_BDOCU                      
				NULL,                   -- 원인전표라인   NO_BDOLINE                      
				'0',                    -- 타대구분    TP_ETCACCT                      
				@IN_CD_BIZAREA,			-- 귀속사업장            
				@IN_NM_BIZAREA,          
				@IN_CD_CC,				-- 코스트센터            
				@IN_NM_CC,                   
				@IN_CD_PJT,				-- 프로젝트            
				@IN_NM_PROJECT,          
				@IN_CD_WDEPT,			-- 부서            
				@IN_NM_DEPT,          
				@IN_CD_EMPLOY,			-- 사원     CD_EMPLOY            
				@IN_NM_KOR,          
				@IN_CD_PARTNER,			-- 거래처    CD_PARTNER            
				@IN_LN_PARTNER,          
				NULL,                   -- 예적금코드   CD_DEPOSIT            
				NULL,                   -- 카드번호    CD_CARD              
				NULL,                   -- 은행코드    CD_BANK  : MA_PARTNER에서 FG_PARTNER = '002' 인것            
				NULL,                   -- 품목코드    NO_ITEM              
				NULL,                   -- CD_UMNG1              
				NULL,                   -- CD_UMNG2              
				NULL,                   -- CD_UMNG3              
				NULL,                   -- CD_UMNG4                      
				@IN_TP_TAX,				-- 세무구분    TP_TAX               
				@IN_NM_TP_TAX,          
				NULL,                   -- 거래구분    CD_TRADE              
				NULL,					-- NM_TRADE                                                            
				@IN_CD_EXCH,            -- 환종     CD_EXCH                     
				@IN_CD_EXCH_NM,         -- 환종명칭 2011.09.22 Add.. 
				NULL,					-- NM_EXCH,                                                            
				NULL,					-- CD_UMNG1                                                                  
				NULL,					-- CD_UMNG2                                                                  
				NULL,					-- CD_UMNG3                                                                  
				NULL,					-- CD_UMNG4                                                                  
				@IN_NO_COMPANY,             
				@IN_AM_SUPPLY,			-- AM_SUPPLY            
				@V_IN_NO_MDOCU,			-- CD_MNG 연동항목          
				@DT_PAY_PREARRANGED,    -- 자금예정일(지급예정일 2011.02.22 SMR)           
				@DT_DUE,				-- 만기일자(2011/05/02Rejina:지급예정일과 동일케처리)           
				                        --         (2011/08/24Rejina:만기일자 컬럼 추라)     
				@IN_RT_EXCH,            -- 환율     RT_EXCH            
				@IN_AM_EXDO,            -- 외화금액    AM_EXDO             
				@P_NO_MODULE,			-- 모듈구분(매출:002) NO_MODULE             
				@IN_NO_MDOCU,			-- 관리번호 = 계산서번호CD_MNG                      
				NULL,                   -- 지출결의코드   CD_EPNOTE             
				@IN_ID_WRITE,           -- 전표처리자                                                     
				NULL,                   -- 예산계정    CD_BGACCT             
				NULL,                   -- 결의구분    TP_EPNOTE             
				NULL,                   -- 품의내역    NM_PUMM              
				@DTS_INSERT,            -- 현재일자로 20100506 @P_DT_PROCESS,    -- 작성일자 DT_WRITE                                                                  
				0,          
				0,						-- AM_JSUM                                                             
				'N',					-- YN_GWARE                                                                  
				NULL,					-- 사업계획코드 CD_BIZPLAN                                                              
				NULL,					--CD_ETC                                                              
				@P_ERRORCODE,                                                              
				@P_ERRORMSG,                              
				NULL,                              
				NULL,                              
				@FG_PAYBILL,			-- 지급조건                              
				NULL              
			             
      
  IF (@@ERROR <> 0 )     
  BEGIN 
	SELECT @ERRMSG = '작업을 정상적으로 처리하지 못했습니다.' 
	CLOSE CUR_PR_OPOUT_IV_TRANS_DOCU DEALLOCATE CUR_PR_OPOUT_IV_TRANS_DOCU 
	GOTO ERROR 
  END               
    
  --추가 :김정근 20071010 부가세 추가로직            
  IF (@IN_CD_RELATION IN ('30', '31') )            
   BEGIN            
    EXEC UP_FI_AUTODOCU_TAX @IN_CD_COMPANY, @IN_CD_PC, @P_NO_DOCU, @P_NO_DOLINE, @IN_AM_SUPPLY            
   END       
 END     
----------------------------------------------------------------------------------------------    
FETCH NEXT FROM CUR_PR_OPOUT_IV_TRANS_DOCU     
INTO	@IN_CD_COMPANY,
		@IN_NO_MDOCU, 
		@IN_CD_PARTNER, 
		@IN_CD_CC, 
		@IN_CD_PJT, 
		@IN_ID_WRITE, 
		@IN_CD_BIZAREA,    
		@IN_TP_TAX, 
		@IN_NM_TP_TAX, 
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
		@IN_NM_CC,     
		@IN_NM_KOR, 
		@IN_LN_PARTNER, 
		@IN_NM_PROJECT, 
		@IN_NM_DEPT, 
		@IN_CD_RELATION, 
		@TP_ACAREA,    
		@IN_NO_COMPANY, 
		@DT_PAY_PREARRANGED,
		@FG_PAYBILL,
		@DT_DUE   
END    
    
CLOSE CUR_PR_OPOUT_IV_TRANS_DOCU    
DEALLOCATE CUR_PR_OPOUT_IV_TRANS_DOCU    
    
IF (@P_NO_MODULE = '800') -- 회계전표유형 : 국내매입 이면    
BEGIN    
    UPDATE PR_OPOUT_IVH    
    SET YN_DOCU = 'Y',     
        NO_DOCU = @P_NO_DOCU    
    WHERE CD_COMPANY = @P_CD_COMPANY    
     AND NO_IV = @IN_NO_MDOCU    
    
    IF (@@ERROR <> 0 ) BEGIN SELECT @ERRMSG = '[UP_PR_OPOUT_IV_TRANS_DOCU]작업을 정상적으로 처리하지 못했습니다.' GOTO ERROR END    
END    
    
RETURN    
ERROR: RAISERROR (@ERRMSG, 18, 1)