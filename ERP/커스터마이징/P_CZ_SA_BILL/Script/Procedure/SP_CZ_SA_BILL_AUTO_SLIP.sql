USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_BILL_AUTO_SLIP]    Script Date: 2016-01-13 오후 9:02:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- 설    명 : 영업>채권관리>수금 등록 -> 미결전표처리
-- 수 정 자 : 김명수
-- 수 정 일 : 2010.06.11
-- 유    형 : 수정
-- 내    역 : 에러발생시 ROLLBACK 처리
-- ================================================
--*******************************************      
-- Change History      
--*******************************************      
--[인자수정 -- 기존인자 ]    
--@P_CD_COMPANY  NVARCHAR(7),      
--@P_FG_TP     NVARCHAR(3),  -- 계정처리유형(MA_AISPOSTL 참조)         
--@P_CD_TP     NVARCHAR(3),  -- 계정처리번호(MA_AISPOSTL 참조)        
--@P_NO_SLIP    NVARCHAR(20), -- 수금번호      
--@P_ID_INSERT    NVARCHAR(15)      
--2009.12.01 : 전표유형 추가  
--2009.12.01 CD_FUND 추가 (자금과목) -SMR
--2009.12.22 --CD_PC: 영업그룹의 CD_CC에서 사업장의 회계단위로 대체 --사업장이 없다면 영업그룹으로 대치
--2010.02.23 --전표유형  NULL 이면 DEFAULT 값(33)으로 SET -SMR(제다/김형석)
--2012.01.13 --대변:선수금/대변외상매출금에 금융기관/예적금계촤 추가 (포티스/정기현)
--2012.02.27 --프로젝트명 추가(PIMS:D20120227082)
--2012.05.12 --세화PNC는 카드일경우 금융기관을 거래처로 
--*******************************************/      
ALTER PROCEDURE [NEOE].[SP_CZ_SA_BILL_AUTO_SLIP]    
(            
	@P_CD_COMPANY		NVARCHAR(7),             
	@P_NO_RCP			NVARCHAR(20),    
	@P_ID_INSERT		NVARCHAR(15),
	@P_YN_BATCH			NVARCHAR(1)	= 'N',		-- N : 통합전표처리유무, Y : 통합전표처리
	@P_MULTI_NO_RCP		TEXT		= ''		-- 멀티수금번호       
)            
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
   
DECLARE
		@V_CNT			INT,
	    @V_MAX_DT_RCP   NVARCHAR(8),
	    @P_AM_FSIZE			NUMERIC(3,0),     
		@P_AM_UPDOWN		NVARCHAR(3),      
		@P_AM_FORMAT		NVARCHAR(50)   
EXEC UP_SF_GETUNIT_AMEX @P_CD_COMPANY,'SA', 'I',  @P_AM_FSIZE OUTPUT,  @P_AM_UPDOWN OUTPUT,  @P_AM_FORMAT OUTPUT  	
--서버키처리추가 (제원인터네셔널 표준적요설정 2012-03-05 LSH)
DECLARE @V_SERVER_KEY NVARCHAR(50)    

DECLARE @V_CD_UMNG1     NVARCHAR(10),
        @V_CD_UMNG3     NVARCHAR(10),
        @V_CD_BUDGET    NVARCHAR(20),
        @V_CD_BGACCT    NVARCHAR(20),
        @V_CD_WDEPT     NVARCHAR(12),
        @V_CD_DEPT      NVARCHAR(12),
        @V_NM_DEPT      NVARCHAR(50),
        @V_CD_DOCU_2    NVARCHAR(3),    -- 전표유형
        @V_AM_RCP_EX	NUMERIC(19,6), 
        @V_AM_RCP_A_EX	NUMERIC(19,6),
        @V_CD_ENV		NVARCHAR(3),
		@V_ST_DOCUAPP	NVARCHAR(1),
		@V_ST_DOCU		NVARCHAR(1)

SET     @V_CD_UMNG1     = ''
SET     @V_CD_UMNG3     = ''

DECLARE @V_NO_IV		NVARCHAR(20)
  
SELECT @V_SERVER_KEY = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG WHERE YN_UPGRADE = 'Y'

SELECT @V_ST_DOCUAPP = ST_DOCUAPP 
FROM MA_USER 
WHERE CD_COMPANY = @P_CD_COMPANY 
AND ID_USER = @P_ID_INSERT

IF (@V_ST_DOCUAPP = '2' OR @V_ST_DOCUAPP = '3')
	SET @V_ST_DOCU = '2'
ELSE
	SET @V_ST_DOCU = '1'

IF(ISNULL(CONVERT(NVARCHAR(4000),@P_MULTI_NO_RCP),'') = '') --일괄전표처리일때 체크
BEGIN
     SELECT @V_AM_RCP_EX   = AM_RCP_EX, 
            @V_AM_RCP_A_EX = AM_RCP_A_EX
     FROM   SA_RCPH
     WHERE  CD_COMPANY = @P_CD_COMPANY
     AND    NO_RCP     = @P_NO_RCP
                
     IF (ISNULL(@V_AM_RCP_EX,0) = 0 AND ISNULL(@V_AM_RCP_A_EX,0) = 0 ) /*수금액, 선수금액이 0이면 회계전표안만들고 TP_AIS = 'Y'처리 20090210*/            
     BEGIN            
          UPDATE SA_RCPH 
          SET    TP_AIS = 'Y' 
          WHERE  CD_COMPANY = @P_CD_COMPANY 
          AND    NO_RCP     = @P_NO_RCP
          RETURN     
     END
END 
	
IF 	@P_YN_BATCH = 'Y'
BEGIN
	-- 1. 수금번호에 해당하는 수금일자는 동일해야한다.
	SELECT	@V_CNT	= COUNT(DISTINCT DT_RCP)
	FROM	SA_RCPH
	WHERE	CD_COMPANY = @P_CD_COMPANY 
	AND		((@P_YN_BATCH = 'N' AND NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))
	
	IF @V_CNT > 1
	BEGIN
		RAISERROR ('선택하신 수금번호의 수금일자가 상이한 내용이 존재합니다.', 18, 1)
		RETURN
	END
	
	-- 2. 수금번호에 해당하는 회계단위는 동일해야한다.
	SELECT	@V_CNT	= COUNT(DISTINCT B.CD_PC)
	FROM	SA_RCPH H
	INNER JOIN MA_BIZAREA B
	ON		H.CD_COMPANY	= B.CD_COMPANY AND H.CD_BIZAREA = B.CD_BIZAREA
	WHERE	H.CD_COMPANY = @P_CD_COMPANY 
	AND		((@P_YN_BATCH = 'N' AND H.NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND H.NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))	
	
	IF @V_CNT > 1
	BEGIN
		RAISERROR ('선택하신 수금번호의 회계단위가 상이한 내용이 존재합니다.', 18, 1)
		RETURN
	END	
	
	-- 2. 선택한 수금번호의 전표상태가 Y 인 내용 체크
	SELECT	@V_CNT	= COUNT(TP_AIS)
	FROM	SA_RCPH
	WHERE	CD_COMPANY	= @P_CD_COMPANY 
	AND		((@P_YN_BATCH = 'N' AND NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))	
	AND		TP_AIS		= 'Y'
	
	IF @V_CNT > 1
	BEGIN
		RAISERROR ('선택하신 수금번호의 전표상태가 처리인 내용이 존재합니다.', 18, 1)
		RETURN
	END
	
	SELECT	TOP 1 @V_MAX_DT_RCP = DT_RCP 
	FROM	SA_RCPH
	WHERE	CD_COMPANY = @P_CD_COMPANY 
	AND		((@P_YN_BATCH = 'N' AND NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))
END
	
DECLARE @GTB_DOCU TABLE
(
	CD_COMPANY		NVARCHAR(7), 	
	NO_RCP			NVARCHAR(20), 
	NO_LINE			NUMERIC(5,0), 
	CD_BIZAREA		NVARCHAR(50), 
	CD_WDEPT		NVARCHAR(50),               
	BILL_PARTNER	NVARCHAR(100),      --카드일경우 거래처는 금융기관임  20090409        
	FG_TAX			NVARCHAR(10), 
	ID_WRITE		NVARCHAR(20), 
	DT_PROCESS		NVARCHAR(8), 
	AM_DR			NUMERIC(17,4),              
	AM_CR			NUMERIC(17,4), 
	AM_SUPPLY		NUMERIC(17,4), 
	DT_TAX			NVARCHAR(8), 
	FG_TRANS		NVARCHAR(10),               
	CD_CC			NVARCHAR(20), 
	NO_EMP			NVARCHAR(20), 
	CD_PJT			NVARCHAR(20),
	CD_EXCH			NVARCHAR(4),
	NM_EXCH			NVARCHAR(50), 
	RT_EXCH			NUMERIC(17,4), 
	CD_DEPT			NVARCHAR(20), 
	CD_PC			NVARCHAR(7),             
	NM_BIZAREA		NVARCHAR(50), 
	FG_AIS			NVARCHAR(10), 
	CD_ACCT			NVARCHAR(30), 
	CLS_ITEM		NVARCHAR(10),               
	TP_DRCR			NVARCHAR(1),
	NM_DEPT			NVARCHAR(50), 
	NM_CC			NVARCHAR(50), 
	NM_EMP			NVARCHAR(50), 
	NM_PJT			NVARCHAR(50),    
	NM_PARTNER		NVARCHAR(50),        --카드일경우 거래처는 금융기관임           		
	CD_RELATION		NVARCHAR(6), --연동항목:일반'10' 어음일경우만 '40'setting (20100324) 어음계정이 FIX가 아니고 계정등록의 연동항목이 40이면 모두 받을어음임 
	NM_TAX			NVARCHAR(50), 
	NO_COMPANY		NVARCHAR(50),
	TP_ACAREA		NVARCHAR(10), --20100518 백광(본지점회계처리)
	CD_DEPOSIT		NVARCHAR(50), -- 예적금코드(예금, 당좌, 당좌)				
	CD_CARD			NVARCHAR(50),      -- 신용카드          
	CD_BANK			NVARCHAR(50),	-- 금융기관				     
	NM_BANK			NVARCHAR(50),			-- 금융기관명						
	CD_MNG			NVARCHAR(50),				-- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
	DT_END			NVARCHAR(8),			-- 어음만기일자      CASE B.FG_RCP WHEN '006' THEN B.DT_DUE ELSE NULL END 제거20080902    
	AM_EX			NUMERIC(19,6),         -- 외화금액          
	CD_DOCU			NVARCHAR(50),
	CD_FUND			NVARCHAR(50),
	FG_JATA			NVARCHAR(3),
	NM_FG_JATA		NVARCHAR(200),
	NM_ISSUE		NVARCHAR(20),
	NO_RELATION		NVARCHAR(20), -- 가수금
	SEQ_RELATION	NUMERIC(5,0)  -- 가수금
)	

DECLARE @V_CD_EXC_BAN_H NVARCHAR(3)
SELECT  @V_CD_EXC_BAN_H = ISNULL(CD_EXC, '000') FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '수금전표헤더반제'

DECLARE @V_CD_EXC NVARCHAR(3)    
SELECT  @V_CD_EXC = ISNULL(CD_EXC,'N')  
FROM    MA_EXC  
WHERE   CD_COMPANY = @P_CD_COMPANY  
AND     CD_MODULE = 'SA'  
AND     EXC_TITLE = '수금등록-가수금사용여부'  

-- 통합전표이면서 본죽, 테스트서버 108번 일경우
IF @P_YN_BATCH = 'Y' AND (ISNULL(NEOE.FN_SERVER_KEY('BON'), 'N') = 'Y' OR ISNULL(NEOE.FN_SERVER_KEY('DZSQL'), 'N') = 'Y')
BEGIN
	INSERT INTO @GTB_DOCU
	SELECT	A.CD_COMPANY, '***' NO_RCP, 1 NO_LINE, '' CD_BIZAREA, (SELECT CD_DEPT FROM MA_EMP WHERE CD_COMPANY = @P_CD_COMPANY AND NO_EMP = @P_ID_INSERT) CD_WDEPT,               
			'' BILL_PARTNER,      --카드일경우 거래처는 금융기관임  20090409        
			NULL AS FG_TAX, @P_ID_INSERT ID_WRITE, 
			@V_MAX_DT_RCP DT_PROCESS, SUM(B.AM_RCP + B.AM_RCP_A ) AM_DR,              
			0.0 AM_CR, 0.0 AM_SUPPLY, @V_MAX_DT_RCP  DT_TAX, A.TP_BUSI AS FG_TRANS,               
			'' CD_CC, @P_ID_INSERT NO_EMP, NULL AS CD_PJT,               
			A.CD_EXCH,(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.CD_EXCH AND CD_FIELD = 'MA_B000005') NM_EXCH,                       
			A.RT_EXCH, NULL CD_DEPT, ISNULL(E.CD_PC, CC.CD_PC)  AS CD_PC,             
			'' NM_BIZAREA, '' FG_AIS, L.CD_ACCT, '' CLS_ITEM,               
			'1' TP_DRCR, -- 차대구분 1: 차변              
			NULL NM_DEPT, NULL NM_CC, 
			NULL NM_EMP, NULL AS NM_PJT,    
			'' NM_PARTNER,        --카드일경우 거래처는 금융기관임           		
			CASE	F.CD_RELATION 
					WHEN '40' THEN '40' 
					WHEN '45' THEN '45' ELSE '10' 
			END AS CD_RELATION, --연동항목:일반'10' 어음일경우만 '40'setting (20100324) 어음계정이 FIX가 아니고 계정등록의 연동항목이 40이면 모두 받을어음임 
			NULL AS NM_TAX, 
			'' NO_COMPANY, -- ?    
			CASE	F.CD_ACTYPE WHEN '90' THEN '1' ELSE '0' END  AS TP_ACAREA, --20100518 백광(본지점회계처리)
			CASE	WHEN B.FG_RCP IN ('002', '005', '006') THEN B.NO_MGMT ELSE NULL END CD_DEPOSIT, -- 예적금코드(예금, 당좌, 당좌)				
			CASE	B.FG_RCP WHEN '006' THEN B.NO_MGMT ELSE NULL END AS CD_CARD,      -- 신용카드          
			CASE	WHEN B.FG_RCP IN ('002','003','005','006','014','017') THEN B.CD_BANK ELSE NULL END CD_BANK,	-- 금융기관				     
			CASE	WHEN B.FG_RCP IN ('002','003','005','006','014','017') THEN M2.LN_PARTNER END NM_BANK,			-- 금융기관명						
			CASE	WHEN B.FG_RCP IN ('003', '004', '005','017') THEN B.NO_MGMT --어음, 당좌, 전자어음번호			   
					ELSE NULL 
			END AS CD_MNG,				-- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
			B.DT_DUE AS DT_END,			-- 어음만기일자      CASE B.FG_RCP WHEN '006' THEN B.DT_DUE ELSE NULL END 제거20080902    
			NEOE.FN_SF_GETUNIT_AMEX ('SA','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, (SUM(B.AM_RCP_EX + B.AM_RCP_A_EX ))) AS AM_EX,         -- 외화금액          
			A.CD_DOCU,
			L.CD_FUND,
			CASE WHEN B.FG_JATA = '001' THEN '1' ELSE '2' END  AS FG_JATA,
			MAX(JATA.NM_SYSDEF) AS NM_FG_JATA,
			B.NM_ISSUE,
			NULL NO_RELATION,
		    NULL SEQ_RELATION 
	FROM	SA_RCPH A           
	INNER JOIN SA_RCPL B 
	ON		A.NO_RCP     = B.NO_RCP AND A.CD_COMPANY = B.CD_COMPANY                  
	INNER JOIN MA_SALEGRP G 
	ON		A.CD_COMPANY = G.CD_COMPANY AND A.CD_SALEGRP = G.CD_SALEGRP          
	INNER JOIN MA_CC CC 
	ON		G.CD_COMPANY = CC.CD_COMPANY AND G.CD_CC = CC.CD_CC      
	INNER JOIN MA_EMP D 
	ON		A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY              
	LEFT OUTER JOIN MA_BIZAREA E 
	ON		A.CD_BIZAREA = E.CD_BIZAREA AND A.CD_COMPANY = E.CD_COMPANY              
	INNER JOIN MA_AISPOSTL L -- 수금형태              
	ON		A.CD_COMPANY = L.CD_COMPANY AND A.CD_TP = L.CD_TP         
	INNER JOIN FI_ACCTCODE F -- 수금형태에 따른 계정과목          
	ON		L.CD_ACCT    = F.CD_ACCT AND L.CD_COMPANY = F.CD_COMPANY        
	LEFT OUTER JOIN MA_PARTNER P 
	ON		A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY        
	LEFT OUTER JOIN MA_PARTNER M2	ON	B.CD_COMPANY = M2.CD_COMPANY AND B.CD_BANK = M2.CD_PARTNER 
	LEFT OUTER JOIN MA_CODEDTL JATA ON  B.CD_COMPANY = JATA.CD_COMPANY AND B.FG_JATA = JATA.CD_SYSDEF AND JATA.CD_FIELD = 'SA_B000012'
	WHERE   
	B.NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))
	AND		B.CD_COMPANY = @P_CD_COMPANY --AND A.TP_AIS = 'N' -- 전표처리여부             
	AND		L.FG_TP = '300'			-- 형태코드 : 수금형태('300')          
	AND		L.FG_AIS = ( CASE B.FG_RCP      
							 WHEN '001' THEN  '301' -- 현금      
							 WHEN '002' THEN  '302' -- 예금      
							 WHEN '003' THEN  '303' -- 어음      
							 WHEN '004' THEN  '304' -- 수표      
							 WHEN '005' THEN  '305' -- 당좌      
							 WHEN '006' THEN  '306' -- 카드      
							 WHEN '007' THEN  '307' -- 할인      
							 WHEN '008' THEN  '308' -- 수수료      
							 WHEN '009' THEN  '309' -- 상계      
							 WHEN '010' THEN  '310' -- 잡손실      
							 WHEN '011' THEN  '311' -- 대손      
							 WHEN '012' THEN  '312' -- 환차손      
							 WHEN '013' THEN  '313' -- 환차익      
							 WHEN '014' THEN  '314' -- 구매카드      
							 WHEN '015' THEN  '315' -- 상품권자      
							 WHEN '016' THEN  '316' -- 상품권타  
							 WHEN '017' THEN  '317' -- 전자어음         
						 END )  
	GROUP BY A.CD_COMPANY, A.TP_BUSI,		
			A.CD_EXCH, A.RT_EXCH, ISNULL(E.CD_PC, CC.CD_PC),
			L.CD_ACCT,				
			CASE	F.CD_RELATION 
					WHEN '40' THEN '40' 
					WHEN '45' THEN '45' ELSE '10' 
			END, --연동항목:일반'10' 어음일경우만 '40'setting (20100324) 어음계정이 FIX가 아니고 계정등록의 연동항목이 40이면 모두 받을어음임 		
			CASE	F.CD_ACTYPE WHEN '90' THEN '1' ELSE '0' END, --20100518 백광(본지점회계처리)
			CASE	WHEN B.FG_RCP IN ('002', '005', '006') THEN B.NO_MGMT ELSE NULL END, -- 예적금코드(예금, 당좌, 당좌)				
			CASE	B.FG_RCP WHEN '006' THEN B.NO_MGMT ELSE NULL END,      -- 신용카드          
			CASE	WHEN B.FG_RCP IN ('002','003','005','006','014','017') THEN B.CD_BANK ELSE NULL END,	-- 금융기관	
			CASE	WHEN B.FG_RCP IN ('002','003','005','006','014','017') THEN M2.LN_PARTNER END,			     		
			CASE	WHEN B.FG_RCP IN ('003', '004', '005','017') THEN B.NO_MGMT --어음, 당좌, 전자어음번호			   
					ELSE NULL 
			END,				-- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
			B.DT_DUE,			-- 어음만기일자      CASE B.FG_RCP WHEN '006' THEN B.DT_DUE ELSE NULL END 제거20080902    		
			A.CD_DOCU,
			L.CD_FUND, B.FG_JATA, B.NM_ISSUE
END
ELSE
BEGIN
INSERT INTO @GTB_DOCU
SELECT	A.CD_COMPANY, A.NO_RCP, B.NO_LINE, A.CD_BIZAREA AS CD_BIZAREA,
        CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END AS CD_WDEPT,               
		--카드일경우 거래처는 금융기관임  20090409 
		--세화PNC는 카드일경우 금융기관을 거래처로 20120512
		CASE WHEN @V_SERVER_KEY <> 'SWPNC' AND B.FG_RCP = '006' THEN B.CD_BANK ELSE A.BILL_PARTNER END AS BILL_PARTNER, 
		NULL AS FG_TAX, A.NO_EMP ID_WRITE, A.DT_RCP AS DT_PROCESS,(B.AM_RCP + B.AM_RCP_A ) AM_DR,              
		0.0 AM_CR, 0.0 AM_SUPPLY,A.DT_RCP  AS DT_TAX, A.TP_BUSI AS FG_TRANS,               
		CC.CD_CC,A.NO_EMP,A.NO_PROJECT AS CD_PJT,               
		A.CD_EXCH,(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.CD_EXCH AND CD_FIELD = 'MA_B000005') NM_EXCH,                       
		A.RT_EXCH, D.CD_DEPT AS CD_DEPT, ISNULL(E.CD_PC, CC.CD_PC)  AS CD_PC,             
		E.NM_BIZAREA AS NM_BIZAREA, L.FG_AIS,L.CD_ACCT,'' CLS_ITEM,               
		'1' TP_DRCR, -- 차대구분 1: 차변              
		(SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = D.CD_DEPT AND CD_COMPANY = D.CD_COMPANY) NM_DEPT, CC.NM_CC AS NM_CC, 
		(SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = A.NO_EMP AND CD_COMPANY = A.CD_COMPANY) NM_EMP,
		PH.NM_PROJECT AS NM_PJT,    
		CASE WHEN @V_SERVER_KEY <> 'SWPNC' AND B.FG_RCP = '006' THEN      
				(SELECT M2.LN_PARTNER FROM MA_PARTNER M2 WHERE M2.CD_COMPANY = B.CD_COMPANY AND M2.CD_PARTNER = B.CD_BANK)    
		ELSE    
				(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.BILL_PARTNER)    
		END AS NM_PARTNER,        --카드일경우 거래처는 금융기관임           		
		CASE	F.CD_RELATION 
				WHEN '40' THEN '40' 
				WHEN '45' THEN '45' ELSE '10' 
		END AS CD_RELATION, --연동항목:일반'10' 어음일경우만 '40'setting (20100324) 어음계정이 FIX가 아니고 계정등록의 연동항목이 40이면 모두 받을어음임 
		NULL AS NM_TAX, 
		P.NO_COMPANY, -- ?    
		CASE	F.CD_ACTYPE WHEN '90' THEN '1' ELSE '0' END  AS TP_ACAREA, --20100518 백광(본지점회계처리)
		CASE	WHEN B.FG_RCP IN ('002', '005', '006') THEN B.NO_MGMT ELSE NULL END CD_DEPOSIT, -- 예적금코드(예금, 당좌, 당좌)				
		CASE	B.FG_RCP WHEN '006' THEN B.NO_MGMT ELSE NULL END AS CD_CARD,      -- 신용카드          
		CASE	WHEN B.FG_RCP IN ('002','003','004','005','006','014','017') THEN B.CD_BANK ELSE NULL END CD_BANK,	-- 금융기관				     
		CASE	WHEN B.FG_RCP IN ('002','003','004','005','006','014','017') THEN 
				(SELECT M2.LN_PARTNER FROM MA_PARTNER M2 WHERE M2.CD_COMPANY = B.CD_COMPANY AND M2.CD_PARTNER = B.CD_BANK) 
				ELSE NULL
		END NM_BANK,				-- 금융기관명						
		CASE	WHEN B.FG_RCP IN ('003', '004', '005','017') THEN B.NO_MGMT --어음, 당좌, 전자어음번호			   
				ELSE NULL 
		END AS CD_MNG,				-- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
		B.DT_DUE AS DT_END,			-- 어음만기일자      CASE B.FG_RCP WHEN '006' THEN B.DT_DUE ELSE NULL END 제거20080902    
		NEOE.FN_SF_GETUNIT_AMEX ('SA','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, ((B.AM_RCP_EX + B.AM_RCP_A_EX ))) AS AM_EX,         -- 외화금액          
		A.CD_DOCU,
		L.CD_FUND,
		CASE WHEN B.FG_JATA = '001' THEN '1' ELSE '2' END  AS FG_JATA,
		JATA.NM_SYSDEF AS NM_FG_JATA,
		B.NM_ISSUE,
		B.NO_RELATION,
		B.SEQ_RELATION  
FROM	SA_RCPH A           
INNER JOIN SA_RCPL B 
ON		A.NO_RCP     = B.NO_RCP AND A.CD_COMPANY = B.CD_COMPANY                  
INNER JOIN MA_SALEGRP G 
ON		A.CD_COMPANY = G.CD_COMPANY AND A.CD_SALEGRP = G.CD_SALEGRP          
INNER JOIN MA_CC CC 
ON		G.CD_COMPANY = CC.CD_COMPANY AND G.CD_CC = CC.CD_CC      
INNER JOIN MA_EMP D 
ON		A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY              
LEFT OUTER JOIN MA_BIZAREA E 
ON		A.CD_BIZAREA = E.CD_BIZAREA AND A.CD_COMPANY = E.CD_COMPANY              
INNER JOIN MA_AISPOSTL L -- 수금형태              
ON		A.CD_COMPANY = L.CD_COMPANY AND A.CD_TP = L.CD_TP         
INNER JOIN FI_ACCTCODE F -- 수금형태에 따른 계정과목          
ON		L.CD_ACCT    = F.CD_ACCT AND L.CD_COMPANY = F.CD_COMPANY        
LEFT OUTER JOIN MA_PARTNER P 
ON		A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY
LEFT OUTER JOIN SA_PROJECTH PH
ON      A.CD_COMPANY = PH.CD_COMPANY AND A.NO_PROJECT = PH.NO_PROJECT
LEFT OUTER JOIN MA_CODEDTL JATA ON  B.CD_COMPANY = JATA.CD_COMPANY AND B.FG_JATA = JATA.CD_SYSDEF AND JATA.CD_FIELD = 'SA_B000012'
WHERE   ((@P_YN_BATCH = 'N' AND B.NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND B.NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))
AND		B.CD_COMPANY = @P_CD_COMPANY AND A.TP_AIS = 'N' -- 전표처리여부             
AND		L.FG_TP = '300'			-- 형태코드 : 수금형태('300')          
AND		L.FG_AIS = ( CASE B.FG_RCP      
                         WHEN '001' THEN  '301' -- 현금      
                         WHEN '002' THEN  '302' -- 예금      
                         WHEN '003' THEN  '303' -- 어음      
                         WHEN '004' THEN  '304' -- 수표      
                         WHEN '005' THEN  '305' -- 당좌      
                         WHEN '006' THEN  '306' -- 카드      
                         WHEN '007' THEN  '307' -- 할인      
                         WHEN '008' THEN  '308' -- 수수료      
                         WHEN '009' THEN  '309' -- 상계      
                         WHEN '010' THEN  '310' -- 잡손실      
                         WHEN '011' THEN  '311' -- 대손      
                         WHEN '012' THEN  '312' -- 환차손      
                         WHEN '013' THEN  '313' -- 환차익      
                         WHEN '014' THEN  '314' -- 구매카드      
                         WHEN '015' THEN  '315' -- 상품권자      
					     WHEN '016' THEN  '316' -- 상품권타  
						 WHEN '017' THEN  '317' -- 전자어음
						 WHEN '018' THEN  '318' -- 가수금          
                     END )    
END


-- CC_SA_IV024 (전표처리 조회)              
-- ◎ 전표처리              
-- 계정과목 & 채권계정              
DECLARE	@CD_COMPANY				NVARCHAR(7),      
		@NO_COMPANY				NVARCHAR(20),                
		@NO_RCP					NVARCHAR(20),          
		@V_NO_RCP				NVARCHAR(20),                  
		@CD_BIZAREA				NVARCHAR(7),              
		@CD_WDEPT				NVARCHAR(12),              
		@BILL_PARTNER			NVARCHAR(20),              
		@FG_TAX					NCHAR(2),       
		@NM_TAX					NVARCHAR(50),
		@ID_WRITE				NVARCHAR(10),              
		@DT_PROCESS				NCHAR(8),              
		@AM_DR					NUMERIC(19,4),
		@AM_CR					NUMERIC(19,4),  
		@AM_SUPPLY				NUMERIC(19,4),              
		@DT_TAX					NCHAR(8),              
		@FG_TRANS				NVARCHAR(2),
		@CD_CC					NVARCHAR(12),          
		@NO_EMP					NVARCHAR(10),           
		@CD_PJT					NVARCHAR(20),             
		@CD_EXCH				NVARCHAR(3),            
		@NM_EXCH				NVARCHAR(50),             
		@RT_EXCH				NUMERIC(11,4),              
		@CD_DEPT				NVARCHAR(12),            
		@CD_PC					NVARCHAR(7),       
		@NM_BIZAREA				NVARCHAR(50),              
		@FG_AIS					NCHAR(3),              
		@CD_ACCT				NVARCHAR(10),
		@CLS_ITEM				NVARCHAR(3),    
		@TP_ACAREA				NVARCHAR(3),            
		@TP_DRCR				NVARCHAR(1),             
		@NM_DEPT				NVARCHAR(50),              
		@NM_CC					NVARCHAR(50),              
		@NM_EMP					NVARCHAR(50),              
		@NM_PJT					NVARCHAR(50),              
		@NM_PARTNER				NVARCHAR(50),              
		@T_CD_RELATION			NCHAR(2),					--부가세 연동항목(31)매출         
		@P_ERRORCODE			NCHAR(10),      
		@P_ERRORMSG				NVARCHAR(300),
      
		@V_CD_DEPOSIT			NVARCHAR(20),				-- 예적금코드          
		@V_CD_DEPOSIT_CODE		NVARCHAR(20),				-- 예적금코드          
		@V_CD_CARD				NVARCHAR(20),				-- 신용카드          
		@V_CD_BANK				NVARCHAR(7),				-- 금융기관          
		@V_NM_BANK				NVARCHAR(50),				-- 금융기관          
		--@V_NO_ITEM   NVARCHAR(20)  -- 품목코드          
		@V_CD_MNG				NVARCHAR(50),     -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
		--@V_CD_TRADE   NVARCHAR(2)  -- 거래구분(자산변동,지급어음정리,받을어음정리,유가증권거래구분) * 받을어음 : FI_F000006 지급어음 : FI_F000007          
		--@V_DT_START   NCHAR(8)     -- 발생일자(증빙일자, 자금예정일자)              
		@V_DT_END				NCHAR(8),					-- 어음만기일자          
		@V_AM_EXDO				NUMERIC(19,4),				-- 외화금액          
		    
		@V_NM_NOTE				NVARCHAR(100),				--적요    
		@V_LN_PARTNER			NVARCHAR(50),				--거래처명    
		@V_CD_TP				NVARCHAR(3),				-- 수금형태코드    
		@V_NM_TP				NVARCHAR(20),				-- 수금형태명          
		@V_CD_DOCU				NVARCHAR(3),				-- 전표유형  
		@V_CD_FUND				NVARCHAR(4),				-- 전표유형 
		
		@V_NO_LINE				NUMERIC(17,4),

		@V_DOCU_CNT				INTEGER,    
    
		@CUR_DATE				NVARCHAR(8),				--현재일자

		@ERRNO					INT,            
		@ERRMSG					NVARCHAR(255),      
		@CD_CC_TEMP				NVARCHAR(12),
		@V_FG_JATA				NVARCHAR(3),
		@V_NM_FG_JATA			NVARCHAR(200),
		@V_NM_ISSUE				NVARCHAR(20),
		
		@V_ID_WRITE             NVARCHAR(10),
		@V_NO_RELATION          NVARCHAR(20),   
        @V_SEQ_RELATION         NUMERIC(5,0) 
    
SET @V_DOCU_CNT = 0
        
DECLARE SRC_CURSOR CURSOR FOR              
              
/*차변 : 수금유형별 계정 금액    
         CASE FG_RCP      
             WHEN '001' THEN  '301' -- 현금      
             WHEN '002' THEN  '302' -- 예금      
             WHEN '003' THEN  '303' -- 어음      
             WHEN '004' THEN  '304' -- 수표      
             WHEN '005' THEN  '305' -- 당좌      
             WHEN '006' THEN  '306' -- 카드      
             WHEN '007' THEN  '307' -- 할인      
             WHEN '008' THEN  '308' -- 수수료      
             WHEN '009' THEN  '309' -- 상계      
             WHEN '010' THEN  '310' -- 잡손실      
             WHEN '011' THEN  '311' -- 대손      
             WHEN '012' THEN  '312' -- 환차손      
             WHEN '013' THEN  '313' -- 환차익      
             WHEN '014' THEN  '314' -- 구매카드      
             WHEN '015' THEN  '315' -- 상품권자      
             WHEN '016' THEN  '316' -- 상품권타  
             WHEN '017' THEN  '317' -- 전자어음     
           END   */    
    
SELECT *, NULL AS NO_IV FROM @GTB_DOCU     
    
UNION ALL              
/*차변 : 환차손*/    
    
SELECT	--DISTINCT               
		A.CD_COMPANY,A.NO_RCP, -1 NO_LINE, A.CD_BIZAREA AS CD_BIZAREA,
		CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END CD_WDEPT,
		A.BILL_PARTNER,               
		NULL AS FG_TAX,A.NO_EMP ID_WRITE,A.DT_RCP AS DT_PROCESS,SUM(B.AM_PL ) AM_DR,              
		0.0 AM_CR,0.0 AM_SUPPLY,A.DT_RCP AS DT_TAX,A.TP_BUSI AS FG_TRANS,               
		CC.CD_CC,A.NO_EMP,A.NO_PROJECT AS CD_PJT,               
		A.CD_EXCH,(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.CD_EXCH AND CD_FIELD = 'MA_B000005') NM_EXCH,            
		A.RT_EXCH, D.CD_DEPT AS CD_DEPT,ISNULL(E.CD_PC,CC.CD_PC)  AS CD_PC,E.NM_BIZAREA AS NM_BIZAREA,              
		L.FG_AIS, L.CD_ACCT,'' CLS_ITEM,               
		'1' TP_DRCR, -- 차대구분 1: 차변              
		(SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = D.CD_DEPT AND CD_COMPANY = D.CD_COMPANY) NM_DEPT,              
		CC.NM_CC AS NM_CC,(SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = A.NO_EMP AND CD_COMPANY = A.CD_COMPANY) NM_EMP,              
		PH.NM_PROJECT AS NM_PJT, (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.BILL_PARTNER) NM_PARTNER,            
		'10' CD_RELATION, --연동항목:일반            
		NULL AS NM_TAX, P.NO_COMPANY, 
		'0' AS TP_ACAREA,
		NULL AS CD_DEPOSIT,  -- 예적금코드          
		NULL AS CD_CARD,      -- 신용카드          
		NULL AS CD_BANK,      -- 금융기관       
		NULL AS NM_BANK,      -- 금융기관명       
		NULL AS CD_MNG,       -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
		NULL AS DT_END,       -- 어음만기일자          
		NEOE.FN_SF_GETUNIT_AMEX ('SA','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, (SUM(B.AM_RCP_TX_EX))) AS AM_EX,         -- 외화금액          
		A.CD_DOCU,L.CD_FUND  ,
		NULL AS FG_JATA,
		NULL AS NM_FG_JATA,
		NULL AS NM_ISSUE,
		NULL AS NO_IV,
		NULL NO_RELATION,
	    NULL SEQ_RELATION
FROM	SA_RCPH A           
INNER JOIN SA_RCPD B 
ON		A.NO_RCP     = B.NO_RCP AND A.CD_COMPANY = B.CD_COMPANY              
INNER JOIN MA_SALEGRP G 
ON		A.CD_COMPANY = G.CD_COMPANY AND A.CD_SALEGRP = G.CD_SALEGRP      
INNER JOIN MA_CC CC 
ON		G.CD_COMPANY = CC.CD_COMPANY AND G.CD_CC = CC.CD_CC      
INNER JOIN MA_EMP D 
ON		A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY              
LEFT OUTER JOIN MA_BIZAREA E 
ON		A.CD_BIZAREA = E.CD_BIZAREA AND A.CD_COMPANY = E.CD_COMPANY              
INNER JOIN MA_AISPOSTL L -- 수금형태              
ON		A.CD_COMPANY = L.CD_COMPANY AND A.CD_TP = L.CD_TP     
INNER JOIN FI_ACCTCODE F -- 수금형태에 따른 계정과목              
ON		L.CD_ACCT    = F.CD_ACCT AND L.CD_COMPANY = F.CD_COMPANY    
LEFT OUTER JOIN MA_PARTNER P 
ON     A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY        
LEFT OUTER JOIN SA_PROJECTH PH
ON      A.CD_COMPANY = PH.CD_COMPANY AND A.NO_PROJECT = PH.NO_PROJECT
WHERE   ((@P_YN_BATCH = 'N' AND B.NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND B.NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))
AND		B.CD_COMPANY = @P_CD_COMPANY
AND		B.AM_PL > 0 AND A.TP_AIS = 'N'	-- 전표처리여부             
AND		L.FG_TP = '300'					-- 형태코드 : 수금형태('300')          
AND		L.FG_AIS = '312'				-- 환차손    
GROUP BY A.CD_COMPANY,A.NO_RCP,A.CD_BIZAREA,E.NM_BIZAREA,CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END,A.BILL_PARTNER,               
		 A.NO_EMP,A.DT_RCP,A.TP_BUSI,CC.CD_CC,A.NO_EMP,A.NO_PROJECT,A.CD_EXCH,               
		 A.RT_EXCH,D.CD_DEPT, CC.CD_PC,E.CD_PC,L.FG_AIS,L.CD_ACCT,D.CD_DEPT,    
		 D.CD_COMPANY,CC.NM_CC,P.NO_COMPANY,A.CD_DOCU, L.CD_FUND, PH.NM_PROJECT
UNION ALL              
      
--------------------------------------------------------------------------------------------------------------------- 매출계정              
-- 대변 : 외상매출금 OR 외화외상매출금 
    
-- 수금 반제했을경우 SA_RCPD를 통해 반제한 SA_IVH의 기준환율을 가져와야 한다 외상매출금의 관리항목필수임    
-- 반제하지안았을경우 반제안된 나머지 금액은 현행 SA_RCPL 수금환율로 금액을 분리하여 분개한다 20090602    


SELECT	--DISTINCT               
		A.CD_COMPANY,A.NO_RCP, -1 NO_LINE, A.CD_BIZAREA AS CD_BIZAREA,
		CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END CD_WDEPT,
		A.BILL_PARTNER,               
		NULL AS FG_TAX,A.NO_EMP ID_WRITE,A.DT_RCP AS DT_PROCESS,             
		0.0 AM_DR,
		CASE WHEN @V_SERVER_KEY = 'DONGYANG' THEN SUM(B.AM_RCP_TX) ELSE SUM(B.AM_RCP_TX + B.AM_PL) END AM_CR,
		0.0 AM_SUPPLY,          
		A.DT_RCP AS DT_TAX,A.TP_BUSI AS FG_TRANS,               
		CC.CD_CC, A.NO_EMP, A.NO_PROJECT AS CD_PJT,               
		A.CD_EXCH,(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.CD_EXCH AND CD_FIELD = 'MA_B000005') NM_EXCH,           
		B.RT_EXCH_IV AS RT_EXCH, D.CD_DEPT AS CD_DEPT,              
		ISNULL(E.CD_PC,CC.CD_PC)  AS CD_PC,E.NM_BIZAREA AS NM_BIZAREA,              
		L.FG_AIS, L.CD_ACCT,              
		'' CLS_ITEM, '2' TP_DRCR, -- 차대구분 2: 대변              
		(SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = D.CD_DEPT AND CD_COMPANY = D.CD_COMPANY) NM_DEPT,              
		CC.NM_CC AS NM_CC,(SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = A.NO_EMP AND CD_COMPANY = A.CD_COMPANY) NM_EMP,              
		PH.NM_PROJECT AS NM_PJT, (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.BILL_PARTNER) NM_PARTNER,            
		'10' CD_RELATION,	--연동항목:일반            
		NULL AS NM_TAX, P.NO_COMPANY, 
		CASE WHEN (@V_SERVER_KEY LIKE 'HDS%') OR (@V_SERVER_KEY LIKE 'DZSQL%') THEN '4' ELSE '0' END AS TP_ACAREA, 
		B1.CD_DEPOSIT AS CD_DEPOSIT, --NULL AS CD_DEPOSIT,		-- 예적금코드          
		B1.CD_CARD AS CD_CARD,    --NULL AS CD_CARD,		-- 신용카드          
		B1.CD_BANK AS CD_BANK,    --NULL AS CD_BANK,		-- 금융기관       
		B1.NM_BANK AS NM_BANK,    --NULL AS NM_BANK,		-- 금융기관명       
		B1.CD_MNG AS CD_MNG,     --NULL AS CD_MNG,			-- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
		B1.DT_END AS DT_END,      --NULL AS DT_END,			-- 어음만기일자          
		
		NEOE.FN_SF_GETUNIT_AMEX ('SA','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, (SUM(B.AM_RCP_TX_EX ))) AS AM_EX,         -- 외화금액          
		A.CD_DOCU, L.CD_FUND  ,
		NULL AS FG_JATA,
		NULL AS NM_FG_JATA,
		NULL AS NM_ISSUE,
		--CASE WHEN (@V_SERVER_KEY LIKE 'HDS%') OR (@V_SERVER_KEY LIKE 'DZSQL%') THEN B.NO_TX ELSE NULL END NO_IV,
		B.NO_TX AS NO_IV,
		NULL NO_RELATION,
	    NULL SEQ_RELATION
FROM	SA_RCPH A           
INNER JOIN SA_RCPD B 
ON		A.NO_RCP     = B.NO_RCP AND A.CD_COMPANY = B.CD_COMPANY              
LEFT JOIN ( SELECT     B.CD_COMPANY,
						B.NO_RCP,
						CASE	WHEN B.FG_RCP IN ('002', '005', '006') THEN B.NO_MGMT ELSE NULL END CD_DEPOSIT, -- 예적금코드(예금, 당좌, 당좌)				
						CASE	B.FG_RCP WHEN '006' THEN B.NO_MGMT ELSE NULL END AS CD_CARD,      -- 신용카드          
						CASE	WHEN B.FG_RCP IN ('002','003','005','006','014','017') THEN B.CD_BANK ELSE NULL END CD_BANK,	-- 금융기관				     
						CASE	WHEN B.FG_RCP IN ('002','003','005','006','014','017') THEN 
								(SELECT M2.LN_PARTNER FROM MA_PARTNER M2 WHERE M2.CD_COMPANY = B.CD_COMPANY AND M2.CD_PARTNER = B.CD_BANK) 
								ELSE NULL
						END NM_BANK,				-- 금융기관명						
						CASE	WHEN B.FG_RCP IN ('003', '004', '005','017') THEN B.NO_MGMT --어음, 당좌, 전자어음번호			   
								ELSE NULL 
						END AS CD_MNG,				-- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
						B.DT_DUE AS DT_END			-- 어음만기일자      CASE B.FG_RCP WHEN '006' THEN B.DT_DUE ELSE NULL END 제거20080902    
			   FROM		SA_RCPL B
			   WHERE   ((@P_YN_BATCH = 'N' AND B.NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND B.NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))
				AND     B.CD_COMPANY = @P_CD_COMPANY                  
				AND     B.NO_LINE = 1
			) B1
ON		A.NO_RCP		= B1.NO_RCP AND A.CD_COMPANY = B1.CD_COMPANY     
INNER JOIN MA_SALEGRP G 
ON		A.CD_COMPANY = G.CD_COMPANY AND A.CD_SALEGRP = G.CD_SALEGRP      
INNER JOIN MA_CC CC 
ON		G.CD_COMPANY = CC.CD_COMPANY AND G.CD_CC = CC.CD_CC      
INNER JOIN MA_EMP D 
ON		A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY              
LEFT OUTER JOIN MA_BIZAREA E 
ON		A.CD_BIZAREA = E.CD_BIZAREA AND A.CD_COMPANY = E.CD_COMPANY              
INNER JOIN MA_AISPOSTL L -- 수금형태              
ON		A.CD_COMPANY = L.CD_COMPANY AND B.TP_SO = L.CD_TP     
INNER JOIN FI_ACCTCODE F -- 수금형태에 따른 계정과목              
ON		L.CD_ACCT    = F.CD_ACCT AND L.CD_COMPANY = F.CD_COMPANY    
LEFT OUTER JOIN MA_PARTNER P 
ON		A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY        
LEFT OUTER JOIN SA_PROJECTH PH
ON      A.CD_COMPANY = PH.CD_COMPANY AND A.NO_PROJECT = PH.NO_PROJECT
WHERE   ((@P_YN_BATCH = 'N' AND B.NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND B.NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))
AND		B.CD_COMPANY = @P_CD_COMPANY                  
AND		A.TP_AIS	= 'N'		-- 전표처리여부             
AND		L.FG_TP		= '100'		-- 형태코드 : 수금형태('300')          
AND		L.FG_AIS	= '101'              
GROUP BY	A.CD_COMPANY, A.NO_RCP,A.CD_BIZAREA,E.NM_BIZAREA,CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END,A.BILL_PARTNER,               
			A.NO_EMP,A.DT_RCP,A.DT_RCP,A.TP_BUSI,CC.CD_CC,A.NO_EMP,A.NO_PROJECT,               
			A.CD_EXCH,B.RT_EXCH_IV,D.CD_DEPT,CC.CD_PC,E.CD_PC,L.FG_AIS,              
			L.CD_ACCT,D.CD_DEPT,D.CD_COMPANY,CC.NM_CC,P.NO_COMPANY,A.CD_DOCU,L.CD_FUND,
			B1.CD_DEPOSIT,B1.CD_CARD, B1.CD_BANK, B1.NM_BANK, B1.CD_MNG, B1.DT_END, PH.NM_PROJECT,
			--CASE WHEN (@V_SERVER_KEY LIKE 'HDS%') OR (@V_SERVER_KEY LIKE 'DZSQL%') THEN B.NO_TX ELSE NULL END
			B.NO_TX
    
-- 반제안된 나머지금액을 외상매출로
UNION ALL    
SELECT	--DISTINCT               
		A.CD_COMPANY, A.NO_RCP, -1 NO_LINE, A.CD_BIZAREA AS CD_BIZAREA,
		CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END CD_WDEPT,               
		A.BILL_PARTNER,NULL AS FG_TAX,A.NO_EMP ID_WRITE, A.DT_RCP AS DT_PROCESS,              
		0.0 AM_DR, 
		CASE WHEN @V_SERVER_KEY = 'DONGYANG' THEN SUM(B.AM_RCP ) - ( ISNULL(MAX(PD.S_AM_RCP_TX),0) + ISNULL(MAX(ABS(PD.S_AM_PL)),0)) ELSE SUM(B.AM_RCP ) - ( ISNULL(MAX(PD.S_AM_RCP_TX),0) + ISNULL(MAX(PD.S_AM_PL),0)) END AM_CR,              
		0.0 AM_SUPPLY,          
		A.DT_RCP AS DT_TAX,A.TP_BUSI AS FG_TRANS,CC.CD_CC,               
		A.NO_EMP,A.NO_PROJECT AS CD_PJT,               
		A.CD_EXCH,(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.CD_EXCH AND CD_FIELD = 'MA_B000005') NM_EXCH,          
		A.RT_EXCH,              
		D.CD_DEPT AS CD_DEPT,ISNULL(E.CD_PC,CC.CD_PC)  AS CD_PC, 
		E.NM_BIZAREA AS NM_BIZAREA,L.FG_AIS,L.CD_ACCT, '' CLS_ITEM,               
		'2' TP_DRCR,		-- 차대구분 2: 대변   
		(SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = D.CD_DEPT AND CD_COMPANY = D.CD_COMPANY) NM_DEPT,              
		CC.NM_CC AS NM_CC, (SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = A.NO_EMP AND CD_COMPANY = A.CD_COMPANY) NM_EMP,              
		PH.NM_PROJECT AS NM_PJT, (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.BILL_PARTNER) NM_PARTNER,            
		'10' CD_RELATION,	--연동항목:일반            
		NULL AS NM_TAX, P.NO_COMPANY, 
		'0' AS TP_ACAREA, 
		NULL AS CD_DEPOSIT,		-- 예적금코드          
		NULL AS CD_CARD,		-- 신용카드          
		NULL AS CD_BANK,		-- 금융기관       
		NULL AS NM_BANK,		-- 금융기관명    
		NULL AS CD_MNG,			-- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
		NULL AS DT_END,			-- 어음만기일자          
		NEOE.FN_SF_GETUNIT_AMEX ('SA','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, (SUM(B.AM_RCP_EX ) - ISNULL(MAX(PD.S_AM_RCP_TX_EX),0))) AS AM_EX,         -- 외화금액          
		A.CD_DOCU,L.CD_FUND  ,
		NULL AS FG_JATA,
		NULL AS NM_FG_JATA,
		NULL AS NM_ISSUE,
		NULL AS NO_IV,
		NULL NO_RELATION,
	    NULL SEQ_RELATION
FROM    SA_RCPH A           
INNER JOIN SA_RCPL B 
ON		A.NO_RCP     = B.NO_RCP AND A.CD_COMPANY = B.CD_COMPANY              
LEFT OUTER JOIN ( 
				SELECT	PD.NO_RCP, PD.CD_COMPANY,    
						SUM(ISNULL(AM_RCP_TX,0)) AS S_AM_RCP_TX,    
						SUM(ISNULL(AM_PL,0)) AS S_AM_PL,    
						NEOE.FN_SF_GETUNIT_AMEX ('SA','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, (SUM(ISNULL(AM_RCP_TX_EX,0)))) AS S_AM_RCP_TX_EX    
				FROM	SA_RCPD PD     				
				WHERE   ((@P_YN_BATCH = 'N' AND PD.NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND PD.NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))				
                    AND PD.CD_COMPANY = @P_CD_COMPANY     
				GROUP BY PD.NO_RCP, PD.CD_COMPANY     
				) PD 
ON		A.NO_RCP		= PD.NO_RCP AND A.CD_COMPANY = PD.CD_COMPANY     
INNER JOIN MA_SALEGRP G 
ON		A.CD_COMPANY	= G.CD_COMPANY AND A.CD_SALEGRP = G.CD_SALEGRP      
INNER JOIN MA_CC CC 
ON		G.CD_COMPANY	= CC.CD_COMPANY AND G.CD_CC = CC.CD_CC      
INNER JOIN MA_EMP D 
ON		A.NO_EMP		= D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY              
LEFT OUTER JOIN MA_BIZAREA E 
ON		A.CD_BIZAREA	= E.CD_BIZAREA AND A.CD_COMPANY = E.CD_COMPANY              
INNER JOIN MA_AISPOSTL L -- 수금형태              
ON		A.CD_COMPANY = L.CD_COMPANY AND A.CD_TP = L.CD_TP     
INNER JOIN FI_ACCTCODE F -- 수금형태에 따른 계정과목              
ON		L.CD_ACCT    = F.CD_ACCT AND L.CD_COMPANY = F.CD_COMPANY    
LEFT OUTER JOIN MA_PARTNER P 
ON		A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY        
LEFT OUTER JOIN SA_PROJECTH PH
ON      A.CD_COMPANY = PH.CD_COMPANY AND A.NO_PROJECT = PH.NO_PROJECT
WHERE   ((@P_YN_BATCH = 'N' AND B.NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND B.NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))
AND		B.CD_COMPANY = @P_CD_COMPANY
AND		B.AM_RCP <> 0 
AND		A.TP_AIS = 'N'  -- 전표처리여부             
AND		L.FG_TP = '300' -- 형태코드 : 수금형태('300')          
AND		L.FG_AIS = (CASE A.TP_BUSI WHEN '001' THEN '351' ELSE '352' END)  -- 반제계정 : 국내('351'), 구매승인서('352')              
GROUP BY A.CD_COMPANY,A.NO_RCP,A.CD_BIZAREA,E.NM_BIZAREA,CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END,A.BILL_PARTNER,A.NO_EMP,               
		 A.DT_RCP,A.DT_RCP,A.TP_BUSI,CC.CD_CC,A.NO_EMP,A.NO_PROJECT,A.CD_EXCH,A.RT_EXCH,              
	     D.CD_DEPT,CC.CD_PC,E.CD_PC,L.FG_AIS,L.CD_ACCT,D.CD_DEPT,D.CD_COMPANY,CC.NM_CC,    
		 P.NO_COMPANY,A.CD_DOCU,L.CD_FUND, PH.NM_PROJECT  
--HAVING ((@V_SERVER_KEY <> 'DONGYANG' AND SUM(ABS(B.AM_RCP_EX) ) - ISNULL(MAX(ABS(PD.S_AM_RCP_TX_EX)),0) > 0) OR (@V_SERVER_KEY = 'DONGYANG' AND SUM(ABS(B.AM_RCP_EX) ) - ISNULL(MAX(ABS(PD.S_AM_RCP_TX_EX)),0) > 0 AND  ISNULL(MAX(ABS(PD.S_AM_PL)),0) <> 0))
HAVING ((@V_SERVER_KEY <> 'DONGYANG' AND SUM(ABS(B.AM_RCP_EX) ) - ISNULL(MAX(ABS(PD.S_AM_RCP_TX_EX)),0) > 0) OR (@V_SERVER_KEY = 'DONGYANG' AND SUM(ABS(B.AM_RCP_EX)) - ISNULL(MAX(ABS(PD.S_AM_RCP_TX_EX)),0) - ISNULL(MAX(ABS(PD.S_AM_PL)),0) > 0))
    
UNION ALL              
/*대변 : 선수금 OR 선수금(해외)*/    
    
SELECT	--DISTINCT               
		A.CD_COMPANY,A.NO_RCP, -1 NO_LINE, A.CD_BIZAREA AS CD_BIZAREA,
		CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END CD_WDEPT,
		A.BILL_PARTNER,               
		NULL AS FG_TAX,A.NO_EMP ID_WRITE,A.DT_RCP AS DT_PROCESS,
		0.0 AM_DR,SUM(B.AM_RCP_A ) AM_CR,0.0 AM_SUPPLY,          
		A.DT_RCP AS DT_TAX, A.TP_BUSI AS FG_TRANS,CC.CD_CC,A.NO_EMP,A.NO_PROJECT AS CD_PJT,               
		A.CD_EXCH,(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.CD_EXCH AND CD_FIELD = 'MA_B000005') NM_EXCH,          
		A.RT_EXCH, D.CD_DEPT AS CD_DEPT,              
		ISNULL(E.CD_PC,CC.CD_PC)  AS CD_PC, E.NM_BIZAREA AS NM_BIZAREA,L.FG_AIS,L.CD_ACCT,              
		'' CLS_ITEM,'2' TP_DRCR, -- 차대구분 2: 대변              
		(SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = D.CD_DEPT AND CD_COMPANY = D.CD_COMPANY) NM_DEPT,              
		CC.NM_CC AS NM_CC, (SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = A.NO_EMP AND CD_COMPANY = A.CD_COMPANY) NM_EMP,              
		PH.NM_PROJECT AS NM_PJT,(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.BILL_PARTNER) NM_PARTNER,            
		'10' CD_RELATION, --연동항목:일반            
		NULL AS NM_TAX, P.NO_COMPANY, 
		'0' AS TP_ACAREA,
		
		CASE	WHEN B.FG_RCP IN ('002', '005', '006') THEN B.NO_MGMT ELSE NULL END CD_DEPOSIT, -- 예적금코드(예금, 당좌, 당좌)				
		CASE	B.FG_RCP WHEN '006' THEN B.NO_MGMT ELSE NULL END AS CD_CARD,      -- 신용카드          
		CASE	WHEN B.FG_RCP IN ('002','003','005','006','014','017') THEN B.CD_BANK ELSE NULL END CD_BANK,	-- 금융기관				     
		CASE	WHEN B.FG_RCP IN ('002','003','005','006','014','017') THEN 
				(SELECT M2.LN_PARTNER FROM MA_PARTNER M2 WHERE M2.CD_COMPANY = B.CD_COMPANY AND M2.CD_PARTNER = B.CD_BANK) 
				ELSE NULL
		END NM_BANK,				-- 금융기관명						
		CASE	WHEN B.FG_RCP IN ('003', '004', '005','017') THEN B.NO_MGMT --어음, 당좌, 전자어음번호			   
				ELSE NULL 
		END AS CD_MNG,				-- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
		B.DT_DUE AS DT_END,			-- 어음만기일자      CASE B.FG_RCP WHEN '006' THEN B.DT_DUE ELSE NULL END 제거20080902    

		NEOE.FN_SF_GETUNIT_AMEX ('SA','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, (SUM(B.AM_RCP_A_EX ))) AS AM_EX,        -- 외화금액          
		A.CD_DOCU,L.CD_FUND  ,
		B.FG_JATA AS FG_JATA,
		MAX(JATA.NM_SYSDEF) AS NM_FG_JATA,
		B.NM_ISSUE AS NM_ISSUE,
		NULL AS NO_IV,
		NULL NO_RELATION,
	    NULL SEQ_RELATION
FROM	SA_RCPH A           
INNER JOIN SA_RCPL B 
ON		A.NO_RCP     = B.NO_RCP AND A.CD_COMPANY = B.CD_COMPANY              
INNER JOIN MA_SALEGRP G 
ON		A.CD_COMPANY = G.CD_COMPANY AND A.CD_SALEGRP = G.CD_SALEGRP      
INNER JOIN MA_CC CC 
ON		G.CD_COMPANY = CC.CD_COMPANY AND G.CD_CC = CC.CD_CC      
INNER JOIN MA_EMP D 
ON		A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY              
LEFT OUTER JOIN MA_BIZAREA E 
ON		A.CD_BIZAREA = E.CD_BIZAREA AND A.CD_COMPANY = E.CD_COMPANY              
INNER JOIN MA_AISPOSTL L -- 수금형태              
ON		A.CD_COMPANY = L.CD_COMPANY AND A.CD_TP = L.CD_TP     
INNER JOIN FI_ACCTCODE F -- 수금형태에 따른 계정과목              
ON		L.CD_ACCT    = F.CD_ACCT AND L.CD_COMPANY = F.CD_COMPANY    
LEFT OUTER JOIN MA_PARTNER P 
ON		A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY       
LEFT OUTER JOIN SA_PROJECTH PH
ON      A.CD_COMPANY = PH.CD_COMPANY AND A.NO_PROJECT = PH.NO_PROJECT
LEFT OUTER JOIN MA_CODEDTL JATA ON B.CD_COMPANY = JATA.CD_COMPANY AND B.FG_JATA = JATA.CD_SYSDEF AND JATA.CD_FIELD = 'SA_B000012'
WHERE   ((@P_YN_BATCH = 'N' AND B.NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND B.NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))
AND		B.CD_COMPANY = @P_CD_COMPANY 
AND		B.AM_RCP_A <> 0 
AND		A.TP_AIS = 'N'			-- 전표처리여부             
AND		L.FG_TP = '300'			-- 형태코드 : 수금형태('300')          
AND		L.FG_AIS = (CASE A.TP_BUSI WHEN '001' THEN '331' ELSE '333' END)  -- 선수금계정 : 국내('351'), 구매승인서('352')              
GROUP BY A.CD_COMPANY, A.NO_RCP,A.CD_BIZAREA,E.NM_BIZAREA,CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END,A.BILL_PARTNER,A.NO_EMP,               
		 A.DT_RCP,A.DT_RCP,A.TP_BUSI,CC.CD_CC,A.NO_EMP,A.NO_PROJECT,A.CD_EXCH,A.RT_EXCH,              
		 D.CD_DEPT,CC.CD_PC,E.CD_PC,L.FG_AIS,L.CD_ACCT,D.CD_DEPT,D.CD_COMPANY,CC.NM_CC, P.NO_COMPANY,A.CD_DOCU, L.CD_FUND,
		 B.FG_RCP, B.NO_MGMT, B.CD_BANK, B.CD_COMPANY, B.DT_DUE, PH.NM_PROJECT, B.FG_JATA, B.NM_ISSUE
    
UNION ALL     
-- 대변 : 환차익
    
SELECT	DISTINCT               
		A.CD_COMPANY,A.NO_RCP, -1 NO_LINE, A.CD_BIZAREA AS CD_BIZAREA,
		CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END CD_WDEPT,
		A.BILL_PARTNER,NULL AS FG_TAX,               
		A.NO_EMP ID_WRITE,A.DT_RCP AS DT_PROCESS,              
		0.0 AM_DR,SUM(0 - B.AM_PL ) AM_CR,0.0 AM_SUPPLY,          
		A.DT_RCP AS DT_TAX,A.TP_BUSI AS FG_TRANS,CC.CD_CC,A.NO_EMP,A.NO_PROJECT AS CD_PJT,               
		A.CD_EXCH,(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.CD_EXCH AND CD_FIELD = 'MA_B000005') NM_EXCH,           
		A.RT_EXCH,D.CD_DEPT AS CD_DEPT,              
		ISNULL(E.CD_PC,CC.CD_PC)  AS CD_PC,E.NM_BIZAREA AS NM_BIZAREA,L.FG_AIS,L.CD_ACCT,              
		'' CLS_ITEM,               
		'2' TP_DRCR, -- 차대구분 2: 대변              
		(SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = D.CD_DEPT AND CD_COMPANY = D.CD_COMPANY) NM_DEPT,              
		CC.NM_CC AS NM_CC, (SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = A.NO_EMP AND CD_COMPANY = A.CD_COMPANY) NM_EMP,              
		PH.NM_PROJECT AS NM_PJT,(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.BILL_PARTNER) NM_PARTNER,            
		'10' CD_RELATION, --연동항목:일반            
		NULL AS NM_TAX,P.NO_COMPANY,'0' AS TP_ACAREA,
		NULL AS CD_DEPOSIT,		-- 예적금코드          
		NULL AS CD_CARD,		-- 신용카드          
		NULL AS CD_BANK,		-- 금융기관       
		NULL AS NM_BANK,		-- 금융기관명       
		NULL AS CD_MNG,			-- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
		NULL AS DT_END,			-- 어음만기일자          
		NEOE.FN_SF_GETUNIT_AMEX ('SA','', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, (SUM(B.AM_RCP_TX_EX ))) AS AM_EX,        -- 외화금액          
		A.CD_DOCU,L.CD_FUND,
		NULL AS FG_JATA,
		NULL AS NM_FG_JATA,
		NULL AS NM_ISSUE,
		NULL AS NO_IV,
		NULL NO_RELATION,
	    NULL SEQ_RELATION 
FROM	SA_RCPH A           
INNER JOIN SA_RCPD B 
ON		A.NO_RCP     = B.NO_RCP AND A.CD_COMPANY = B.CD_COMPANY              
INNER JOIN MA_SALEGRP G
ON		A.CD_COMPANY = G.CD_COMPANY AND A.CD_SALEGRP = G.CD_SALEGRP      
INNER JOIN MA_CC CC 
ON		G.CD_COMPANY = CC.CD_COMPANY AND G.CD_CC = CC.CD_CC      
INNER JOIN MA_EMP D 
ON		A.NO_EMP = D.NO_EMP AND A.CD_COMPANY = D.CD_COMPANY              
LEFT OUTER JOIN MA_BIZAREA E 
ON		A.CD_BIZAREA = E.CD_BIZAREA AND A.CD_COMPANY = E.CD_COMPANY              
INNER JOIN MA_AISPOSTL L -- 수금형태              
ON		A.CD_COMPANY = L.CD_COMPANY AND A.CD_TP      = L.CD_TP     
INNER JOIN FI_ACCTCODE F -- 수금형태에 따른 계정과목              
ON		L.CD_ACCT    = F.CD_ACCT AND L.CD_COMPANY = F.CD_COMPANY    
LEFT OUTER JOIN MA_PARTNER P 
ON		A.CD_PARTNER = P.CD_PARTNER AND A.CD_COMPANY = P.CD_COMPANY        
LEFT OUTER JOIN SA_PROJECTH PH
ON      A.CD_COMPANY = PH.CD_COMPANY AND A.NO_PROJECT = PH.NO_PROJECT
WHERE   ((@P_YN_BATCH = 'N' AND B.NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND B.NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))
AND		B.CD_COMPANY = @P_CD_COMPANY           
AND		B.AM_PL < 0 
AND		A.TP_AIS = 'N'   -- 전표처리여부             
AND		L.FG_TP = '300'  -- 형태코드 : 수금형태('300')          
AND		L.FG_AIS = '313'  -- 환차익     
GROUP BY A.CD_COMPANY,A.NO_RCP,A.CD_BIZAREA,E.NM_BIZAREA, CASE WHEN @V_SERVER_KEY = 'KORFNT' THEN G.CD_CC ELSE D.CD_DEPT END,A.BILL_PARTNER,A.NO_EMP,               
		 A.DT_RCP,A.DT_RCP,A.TP_BUSI,CC.CD_CC,A.NO_EMP,A.NO_PROJECT,A.CD_EXCH,A.RT_EXCH,              
		 D.CD_DEPT,CC.CD_PC,E.CD_PC,L.FG_AIS,L.CD_ACCT,D.CD_DEPT,D.CD_COMPANY,CC.NM_CC,    
		 P.NO_COMPANY,A.CD_DOCU,L.CD_FUND, PH.NM_PROJECT    
              
-------------------------------------------              
-- 여기서 부터 전표처리 하기 위한 부분  ---              
DECLARE	@P_NO_DOCU		NVARCHAR(20), -- 전표번호              
		@P_DT_PROCESS	NVARCHAR(8),   
        
		@V_FG_RCP_GUBN  NVARCHAR(3),  
		@P_CD_TRADE		NVARCHAR(2),  --받을어음정리구분코드    
		@P_NM_TRADE		NVARCHAR(50), --받을어음정리구분명    
 
		@P_NO_EBILL		NVARCHAR(20), -- 전자어음 EB번호

		@P_NO_ACCT		NUMERIC(5, 0),   -- 회계번호
        @P_ID_ACCT		NVARCHAR(15)	 -- 승인자

SELECT @CUR_DATE = CONVERT(NVARCHAR(8), GETDATE(), 112)
        
-- 수금일자 알아오기              
SELECT	TOP 1 @P_DT_PROCESS =  DT_RCP    
FROM	SA_RCPH           
WHERE	CD_COMPANY = @P_CD_COMPANY 
AND		((@P_YN_BATCH = 'N' AND NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))    
            
 
SELECT TOP 1 @V_CD_ENV = CD_ENV FROM MA_ENVD WHERE TP_ENV = 'TP_NODOCU' AND CD_COMPANY = @P_CD_COMPANY

IF @V_CD_ENV = '1'
BEGIN

SELECT	@CD_PC	= B.CD_PC
FROM	SA_RCPH H
INNER JOIN MA_BIZAREA B ON H.CD_COMPANY	= B.CD_COMPANY AND H.CD_BIZAREA = B.CD_BIZAREA
WHERE H.CD_COMPANY = @P_CD_COMPANY 
AND	  H.NO_RCP = @P_NO_RCP

-- 전표번호회계단위채번
EXEC UP_FI_DOCU_CREATE_SEQ4 @P_CD_COMPANY, @CD_PC, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT

SELECT @P_NO_DOCU = MAX(NO_SEQ2)
FROM MA_AUTOSEQ_NO2 
WHERE NO_AUTOSEQ = 'FI01'
AND CD_COMPANY = @P_CD_COMPANY
AND CD_PC = @CD_PC
AND DT_DAY = @P_DT_PROCESS

END
ELSE
BEGIN
-- 전표번호회사단위채번
EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT
END
              
-- 전표번호 채번              
--EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT

-- 회계번호 채번
IF @V_ST_DOCU = '1'
BEGIN
	SET @P_NO_ACCT = 0
	SET @P_ID_ACCT = NULL
END
ELSE
BEGIN
	EXEC UP_FI_DOCU_CREATE_SEQ4 @P_CD_COMPANY, @CD_PC , 'FI04', @P_DT_PROCESS, @P_NO_ACCT OUTPUT
	SELECT @P_ID_ACCT = NO_EMP FROM MA_USER WHERE CD_COMPANY = @P_CD_COMPANY AND ID_USER = @P_ID_INSERT
END
     
-- NO_DOLINE 땜시              
DECLARE	@P_NO_DOLINE NUMERIC(4,0)              
SET @P_NO_DOLINE = 0              
              
-- 금액 문자로              
DECLARE @AM_DR_CR	VARCHAR(40)
--제원적요로직  20120305            
DECLARE @NM_BANK_NAME  	VARCHAR(40)

              
OPEN SRC_CURSOR              
              
FETCH NEXT FROM SRC_CURSOR INTO @CD_COMPANY,@NO_RCP, @V_NO_LINE, @CD_BIZAREA ,@CD_WDEPT,@BILL_PARTNER,@FG_TAX ,@ID_WRITE ,@DT_PROCESS ,@AM_DR   ,@AM_CR, @AM_SUPPLY, @DT_TAX    ,@FG_TRANS   ,@CD_CC   ,@NO_EMP, @CD_PJT, @CD_EXCH , @NM_EXCH, @RT_EXCH ,@CD_DEPT ,@CD_PC, 
		@NM_BIZAREA ,@FG_AIS,@CD_ACCT ,@CLS_ITEM ,@TP_DRCR ,@NM_DEPT ,@NM_CC  ,@NM_EMP  ,@NM_PJT  ,@NM_PARTNER, @T_CD_RELATION, @NM_TAX, @NO_COMPANY, @TP_ACAREA,    
		@V_CD_DEPOSIT,  -- 예적금코드          
		@V_CD_CARD,      -- 신용카드          
		@V_CD_BANK,      -- 금융기관          
		@V_NM_BANK,      -- 금융기관          
		@V_CD_MNG,       -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
		@V_DT_END,       -- 어음만기일자          
		@V_AM_EXDO,      -- 외화금액                    
		@V_CD_DOCU,      -- 전표유형  
		@V_CD_FUND,       -- 자금과목
		@V_FG_JATA,
		@V_NM_FG_JATA,	
		@V_NM_ISSUE,
		@V_NO_IV,
		@V_NO_RELATION,  
        @V_SEQ_RELATION
        
WHILE @@FETCH_STATUS = 0              
BEGIN              
 SET @P_NO_DOLINE = @P_NO_DOLINE + 1              
 SET @AM_DR_CR = CONVERT(VARCHAR(40), @AM_SUPPLY)              
    
IF @T_CD_RELATION <> '10'  /* 부가세관련계정(10)이 아니면 */    
SET @V_NO_RCP = @NO_RCP     
ELSE    
SET @V_NO_RCP = NULL    
        
SELECT @V_CD_DEPOSIT_CODE = NULL    
IF  (ISNULL(@V_CD_DEPOSIT,'') <> '' )    
BEGIN    
 /*예적금코드찾기*/    
 SELECT TOP 1 @V_CD_DEPOSIT_CODE = CD_DEPOSIT FROM FI_DEPOSIT WHERE CD_COMPANY = @CD_COMPANY AND NO_DEPOSIT = @V_CD_DEPOSIT    
    
 IF ( ISNULL(@V_CD_DEPOSIT_CODE,'') = '' ) /*없으면 계좌번호그대로 대치*/    
  SELECT @V_CD_DEPOSIT_CODE = @V_CD_DEPOSIT    
END     
    
    
SET @V_NM_NOTE      = NULL   --적요 /*네오팜임시20090121*/    
    
SELECT  @V_CD_TP = CD_TP FROM SA_RCPH WHERE NO_RCP = @P_NO_RCP AND CD_COMPANY = @P_CD_COMPANY    
SELECT  @V_NM_TP = NM_TP FROM MA_AISPOSTH WHERE CD_COMPANY = @P_CD_COMPANY AND FG_TP = '300' AND CD_TP = @V_CD_TP    
    
SELECT @V_NM_NOTE = @NM_PARTNER + '(' + @V_NM_TP + ')'    

DECLARE @V_CD_EXC_NOTE NVARCHAR(3)
SELECT  @V_CD_EXC_NOTE = CD_EXC FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '메뉴별적요 사용 여부'

IF(@V_CD_EXC_NOTE = 'Y')
BEGIN
      
	DECLARE @V_CD_COL1 NVARCHAR(30)
	DECLARE @V_CD_COL2 NVARCHAR(30)
	DECLARE @V_CD_COL3 NVARCHAR(30)
	DECLARE @V_CD_COL4 NVARCHAR(30)
	DECLARE @V_CD_COL5 NVARCHAR(30)
	
	DECLARE @V_TXT_SC1 NVARCHAR(4)
	DECLARE @V_TXT_SC2 NVARCHAR(4)
	DECLARE @V_TXT_SC3 NVARCHAR(4)
	DECLARE @V_TXT_SC4 NVARCHAR(4)
	DECLARE @V_TXT_SC5 NVARCHAR(4)
	
	DECLARE @V_NOTE_1 NVARCHAR(200)
	DECLARE @V_NOTE_2 NVARCHAR(200)
	DECLARE @V_NOTE_3 NVARCHAR(200)
	DECLARE @V_NOTE_4 NVARCHAR(200)
	DECLARE @V_NOTE_5 NVARCHAR(200)
	
	SELECT 
		@V_CD_COL1 = CD_COL1,
		@V_CD_COL2 = CD_COL2,
		@V_CD_COL3 = CD_COL3,
		@V_CD_COL4 = CD_COL4,
		@V_CD_COL5 = CD_COL5,
		@V_TXT_SC1 = ISNULL(NEOE.FN_MA_GET_CODEDTL_NM_SYSDEF(@P_CD_COMPANY, 'MA_SPECHAR', H.TXT_SC1), ''),
		@V_TXT_SC2 = ISNULL(NEOE.FN_MA_GET_CODEDTL_NM_SYSDEF(@P_CD_COMPANY, 'MA_SPECHAR', H.TXT_SC2), ''),
		@V_TXT_SC3 = ISNULL(NEOE.FN_MA_GET_CODEDTL_NM_SYSDEF(@P_CD_COMPANY, 'MA_SPECHAR', H.TXT_SC3), ''),
		@V_TXT_SC4 = ISNULL(NEOE.FN_MA_GET_CODEDTL_NM_SYSDEF(@P_CD_COMPANY, 'MA_SPECHAR', H.TXT_SC4), ''),
		@V_TXT_SC5 = ISNULL(NEOE.FN_MA_GET_CODEDTL_NM_SYSDEF(@P_CD_COMPANY, 'MA_SPECHAR', H.TXT_SC5), '')
	FROM MA_MENUNOTEH H		
	WHERE H.CD_COMPANY = @P_CD_COMPANY AND H.ID_MENU = 'P_SA_BILL'
	
	DECLARE @V_NOTE_CD_PARTNER NVARCHAR(20)
	DECLARE @V_NOTE_LN_PARTNER NVARCHAR(50)
	DECLARE @V_NOTE_BILL_PARTNER NVARCHAR(20)
	DECLARE @V_NOTE_LN_BILL_PARTNER NVARCHAR(50)
	DECLARE @V_NOTE_NO_PROJECT NVARCHAR(20)
	DECLARE @V_NOTE_NM_PROJECT NVARCHAR(100) 
	DECLARE @V_NOTE_CD_TP NVARCHAR(3)
	DECLARE @V_NOTE_NM_TP NVARCHAR(20)
	DECLARE @V_NOTE_TP_BUSI NVARCHAR(4)
	DECLARE @V_NOTE_NM_BUSI NVARCHAR(200)
	DECLARE @V_NOTE_NM_KOR NVARCHAR(50)
	DECLARE @V_NOTE_DC_RMK NVARCHAR(100)
	DECLARE @V_NOTE_NO_RCP NVARCHAR(20)
	
	SELECT
		@V_NOTE_CD_PARTNER = H.CD_PARTNER,
		@V_NOTE_LN_PARTNER = PTN1.LN_PARTNER,
		@V_NOTE_BILL_PARTNER = H.BILL_PARTNER,
		@V_NOTE_LN_BILL_PARTNER = PTN2.LN_PARTNER,
		@V_NOTE_NO_PROJECT = H.NO_PROJECT,
		@V_NOTE_NM_PROJECT = PJT.NM_PROJECT,
		@V_NOTE_CD_TP = H.CD_TP,
		@V_NOTE_NM_TP = AIS.NM_TP,
		@V_NOTE_TP_BUSI = H.TP_BUSI,
		@V_NOTE_NM_BUSI = C1.NM_SYSDEF,
		@V_NOTE_NM_KOR = EMP.NM_KOR,
		@V_NOTE_DC_RMK = H.DC_RMK,
		@V_NOTE_NO_RCP = H.NO_RCP
	FROM SA_RCPH H
	LEFT OUTER JOIN MA_PARTNER PTN1 ON H.CD_COMPANY = PTN1.CD_COMPANY AND H.CD_PARTNER = PTN1.CD_PARTNER
	LEFT OUTER JOIN MA_PARTNER PTN2 ON H.CD_COMPANY = PTN2.CD_COMPANY AND H.BILL_PARTNER = PTN2.CD_PARTNER
	LEFT OUTER JOIN SA_PROJECTH PJT ON H.CD_COMPANY = PJT.CD_COMPANY AND H.NO_PROJECT = PJT.NO_PROJECT
	LEFT OUTER JOIN MA_AISPOSTH AIS ON H.CD_COMPANY = AIS.CD_COMPANY AND H.CD_TP = AIS.CD_TP AND AIS.FG_TP = '300'
	LEFT OUTER JOIN MA_CODEDTL C1 ON H.CD_COMPANY = C1.CD_COMPANY AND C1.CD_FIELD = 'PU_C000016' AND H.TP_BUSI = C1.CD_SYSDEF
	LEFT OUTER JOIN MA_EMP EMP ON H.CD_COMPANY = EMP.CD_COMPANY AND H.NO_EMP = EMP.NO_EMP
	WHERE H.CD_COMPANY = @P_CD_COMPANY AND H.NO_RCP = @P_NO_RCP
	
	IF ISNULL(@V_CD_COL1, '') <> ''
	BEGIN
		SET @V_NOTE_1 = CASE WHEN @V_CD_COL1 = '001' THEN ISNULL(@V_NOTE_CD_PARTNER, '')	
							 WHEN @V_CD_COL1 = '002' THEN ISNULL(@V_NOTE_LN_PARTNER, '')	
							 WHEN @V_CD_COL1 = '003' THEN ISNULL(@V_NOTE_BILL_PARTNER, '')	
							 WHEN @V_CD_COL1 = '004' THEN ISNULL(@V_NOTE_LN_BILL_PARTNER, '')	
							 WHEN @V_CD_COL1 = '005' THEN ISNULL(@V_NOTE_NO_PROJECT, '')			
							 WHEN @V_CD_COL1 = '006' THEN ISNULL(@V_NOTE_NM_PROJECT, '')		
							 WHEN @V_CD_COL1 = '007' THEN ISNULL(@V_NOTE_CD_TP, '') 
							 WHEN @V_CD_COL1 = '008' THEN ISNULL(@V_NOTE_NM_TP, '') 
							 WHEN @V_CD_COL1 = '009' THEN ISNULL(@V_NOTE_TP_BUSI, '')
							 WHEN @V_CD_COL1 = '010' THEN ISNULL(@V_NOTE_NM_BUSI, '')
							 WHEN @V_CD_COL1 = '011' THEN ISNULL(@V_NOTE_NM_KOR, '') 
							 WHEN @V_CD_COL1 = '012' THEN ISNULL(@V_NOTE_DC_RMK, '') 
							 WHEN @V_CD_COL1 = '013' THEN ISNULL(@V_NOTE_NO_RCP, '')
							 ELSE '' END
							 
	    SET @V_NOTE_1 = @V_NOTE_1 + ISNULL((CASE WHEN @V_TXT_SC1 = '공백' THEN ' ' ELSE @V_TXT_SC1 END), '')	    
	   
	END
	
	IF ISNULL(@V_CD_COL2, '') <> ''
	BEGIN
		SET @V_NOTE_2 = CASE WHEN @V_CD_COL2 = '001' THEN ISNULL(@V_NOTE_CD_PARTNER, '')	
							 WHEN @V_CD_COL2 = '002' THEN ISNULL(@V_NOTE_LN_PARTNER, '')	
							 WHEN @V_CD_COL2 = '003' THEN ISNULL(@V_NOTE_BILL_PARTNER, '')	
							 WHEN @V_CD_COL2 = '004' THEN ISNULL(@V_NOTE_LN_BILL_PARTNER, '')	
							 WHEN @V_CD_COL2 = '005' THEN ISNULL(@V_NOTE_NO_PROJECT, '')			
							 WHEN @V_CD_COL2 = '006' THEN ISNULL(@V_NOTE_NM_PROJECT, '')		
							 WHEN @V_CD_COL2 = '007' THEN ISNULL(@V_NOTE_CD_TP, '') 
							 WHEN @V_CD_COL2 = '008' THEN ISNULL(@V_NOTE_NM_TP, '') 
							 WHEN @V_CD_COL2 = '009' THEN ISNULL(@V_NOTE_TP_BUSI, '')
							 WHEN @V_CD_COL2 = '010' THEN ISNULL(@V_NOTE_NM_BUSI, '')
							 WHEN @V_CD_COL2 = '011' THEN ISNULL(@V_NOTE_NM_KOR, '') 
							 WHEN @V_CD_COL2 = '012' THEN ISNULL(@V_NOTE_DC_RMK, '') 
							 WHEN @V_CD_COL2 = '013' THEN ISNULL(@V_NOTE_NO_RCP, '')
							 ELSE '' END
							 
	    SET @V_NOTE_2 = @V_NOTE_2 + ISNULL((CASE WHEN @V_TXT_SC1 = '공백' THEN ' ' ELSE @V_TXT_SC2 END), '')	    
	   
	END
	
	IF ISNULL(@V_CD_COL3, '') <> ''
	BEGIN
		SET @V_NOTE_3 = CASE WHEN @V_CD_COL3 = '001' THEN ISNULL(@V_NOTE_CD_PARTNER, '')	
							 WHEN @V_CD_COL3 = '002' THEN ISNULL(@V_NOTE_LN_PARTNER, '')	
							 WHEN @V_CD_COL3 = '003' THEN ISNULL(@V_NOTE_BILL_PARTNER, '')	
							 WHEN @V_CD_COL3 = '004' THEN ISNULL(@V_NOTE_LN_BILL_PARTNER, '')	
							 WHEN @V_CD_COL3 = '005' THEN ISNULL(@V_NOTE_NO_PROJECT, '')			
							 WHEN @V_CD_COL3 = '006' THEN ISNULL(@V_NOTE_NM_PROJECT, '')		
							 WHEN @V_CD_COL3 = '007' THEN ISNULL(@V_NOTE_CD_TP, '') 
							 WHEN @V_CD_COL3 = '008' THEN ISNULL(@V_NOTE_NM_TP, '') 
							 WHEN @V_CD_COL3 = '009' THEN ISNULL(@V_NOTE_TP_BUSI, '')
							 WHEN @V_CD_COL3 = '010' THEN ISNULL(@V_NOTE_NM_BUSI, '')
							 WHEN @V_CD_COL3 = '011' THEN ISNULL(@V_NOTE_NM_KOR, '') 
							 WHEN @V_CD_COL3 = '012' THEN ISNULL(@V_NOTE_DC_RMK, '') 
							 WHEN @V_CD_COL3 = '013' THEN ISNULL(@V_NOTE_NO_RCP, '')
							 ELSE '' END
							 
	    SET @V_NOTE_3 = @V_NOTE_3 + ISNULL((CASE WHEN @V_TXT_SC3 = '공백' THEN ' ' ELSE @V_TXT_SC3 END), '')	    
	   
	END
	
	IF ISNULL(@V_CD_COL4, '') <> ''
	BEGIN
		SET @V_NOTE_4 = CASE WHEN @V_CD_COL4 = '001' THEN ISNULL(@V_NOTE_CD_PARTNER, '')	
							 WHEN @V_CD_COL4 = '002' THEN ISNULL(@V_NOTE_LN_PARTNER, '')	
							 WHEN @V_CD_COL4 = '003' THEN ISNULL(@V_NOTE_BILL_PARTNER, '')	
							 WHEN @V_CD_COL4 = '004' THEN ISNULL(@V_NOTE_LN_BILL_PARTNER, '')	
							 WHEN @V_CD_COL4 = '005' THEN ISNULL(@V_NOTE_NO_PROJECT, '')			
							 WHEN @V_CD_COL4 = '006' THEN ISNULL(@V_NOTE_NM_PROJECT, '')		
							 WHEN @V_CD_COL4 = '007' THEN ISNULL(@V_NOTE_CD_TP, '') 
							 WHEN @V_CD_COL4 = '008' THEN ISNULL(@V_NOTE_NM_TP, '') 
							 WHEN @V_CD_COL4 = '009' THEN ISNULL(@V_NOTE_TP_BUSI, '')
							 WHEN @V_CD_COL4 = '010' THEN ISNULL(@V_NOTE_NM_BUSI, '')
							 WHEN @V_CD_COL4 = '011' THEN ISNULL(@V_NOTE_NM_KOR, '') 
							 WHEN @V_CD_COL4 = '012' THEN ISNULL(@V_NOTE_DC_RMK, '') 
							 WHEN @V_CD_COL4 = '013' THEN ISNULL(@V_NOTE_NO_RCP, '')
							 ELSE '' END
							 
	    SET @V_NOTE_4 = @V_NOTE_4 + ISNULL((CASE WHEN @V_TXT_SC4 = '공백' THEN ' ' ELSE @V_TXT_SC4 END), '')	    
	   
	END
	
	IF ISNULL(@V_CD_COL5, '') <> ''
	BEGIN
		SET @V_NOTE_5 = CASE WHEN @V_CD_COL5 = '001' THEN ISNULL(@V_NOTE_CD_PARTNER, '')	
							 WHEN @V_CD_COL5 = '002' THEN ISNULL(@V_NOTE_LN_PARTNER, '')	
							 WHEN @V_CD_COL5 = '003' THEN ISNULL(@V_NOTE_BILL_PARTNER, '')	
							 WHEN @V_CD_COL5 = '004' THEN ISNULL(@V_NOTE_LN_BILL_PARTNER, '')	
							 WHEN @V_CD_COL5 = '005' THEN ISNULL(@V_NOTE_NO_PROJECT, '')			
							 WHEN @V_CD_COL5 = '006' THEN ISNULL(@V_NOTE_NM_PROJECT, '')		
							 WHEN @V_CD_COL5 = '007' THEN ISNULL(@V_NOTE_CD_TP, '') 
							 WHEN @V_CD_COL5 = '008' THEN ISNULL(@V_NOTE_NM_TP, '') 
							 WHEN @V_CD_COL5 = '009' THEN ISNULL(@V_NOTE_TP_BUSI, '')
							 WHEN @V_CD_COL5 = '010' THEN ISNULL(@V_NOTE_NM_BUSI, '')
							 WHEN @V_CD_COL5 = '011' THEN ISNULL(@V_NOTE_NM_KOR, '') 
							 WHEN @V_CD_COL5 = '012' THEN ISNULL(@V_NOTE_DC_RMK, '') 
							 WHEN @V_CD_COL5 = '013' THEN ISNULL(@V_NOTE_NO_RCP, '')
							 ELSE '' END
							 
	    SET @V_NOTE_5 = @V_NOTE_5 + ISNULL((CASE WHEN @V_TXT_SC5 = '공백' THEN ' ' ELSE @V_TXT_SC5 END), '')    
	   
	END
	
	SET @V_NM_NOTE = ISNULL(@V_NOTE_1, '') +ISNULL(@V_NOTE_2, '') + ISNULL(@V_NOTE_3, '') + ISNULL(@V_NOTE_4, '') + ISNULL(@V_NOTE_5, '')
END	

--전자어음  
IF ( @T_CD_RELATION = '45')
BEGIN
	-- 전자어음 채번 수정해라 : 장은경, 서건수 2010.11.16 라인별채번으로 수정... 그전에는 FETCH 위에 있었음
	SELECT	@V_FG_RCP_GUBN = B.FG_RCP    
	FROM	SA_RCPH A
	INNER JOIN SA_RCPL B 
	ON		A.NO_RCP     = B.NO_RCP AND A.CD_COMPANY = B.CD_COMPANY                   
	WHERE	A.CD_COMPANY = @P_CD_COMPANY AND A.NO_RCP = @NO_RCP AND B.NO_LINE = @V_NO_LINE

	SET @P_NO_EBILL = ''

	IF (@V_FG_RCP_GUBN = '017')
	BEGIN
		EXEC CP_GETNO @P_CD_COMPANY, 'FI', '25', @CUR_DATE, @P_NO_EBILL OUTPUT 
	END    

	SET @V_CD_MNG = @P_NO_EBILL + '|' + @V_CD_MNG 
END     

--IF( @CD_ACCT = '11000' ) /* 만일 어음일 경우 */    
IF( @T_CD_RELATION = '40' OR @T_CD_RELATION = '45') /* 만일 어음일 경우 */    
BEGIN    
	SET @P_CD_TRADE = '1'    
	SET @P_NM_TRADE = '보유'    
END 
   
SET @V_CD_DOCU = CASE WHEN ISNULL(@V_CD_DOCU, '') = '' THEN '33' ELSE @V_CD_DOCU END  -- 기본값 설정   


IF (@V_SERVER_KEY = 'KOREAF')
BEGIN
	--차변/대변 :(거래처명)외상대
	--단,입금이면 대변: 외상대 금융기간명 입금  으로 
	SELECT @V_NM_NOTE = '(' + ISNULL(@NM_PARTNER,'') + ')' + '외상대'
	IF @TP_DRCR = '2'
	BEGIN
		SELECT @NM_BANK_NAME = NULL
		SELECT @NM_BANK_NAME = ISNULL((SELECT M2.LN_PARTNER FROM MA_PARTNER M2 WHERE M2.CD_COMPANY = B.CD_COMPANY AND M2.CD_PARTNER = B.CD_BANK),'') 
		  FROM SA_RCPL B
		   WHERE B.CD_COMPANY = @CD_COMPANY AND B.NO_RCP = @NO_RCP AND B.FG_RCP = '002' --예금 
		IF  @NM_BANK_NAME IS NOT NULL
		BEGIN  
			SELECT @V_NM_NOTE = '외상대 ' + ISNULL(@NM_BANK_NAME,'') + ' ' + '입금'   
		END	
	END 

END 
ELSE IF (@V_SERVER_KEY = 'ENTEC')   --인텍전기전자
BEGIN
    SET @V_NM_NOTE = @V_NM_TP
END
ELSE IF (@V_SERVER_KEY = 'SIMMONS')
BEGIN
    SET @CUR_DATE = @P_DT_PROCESS
END

IF (@V_SERVER_KEY = 'DOMINO2')  
BEGIN  
    SET @V_ID_WRITE = @P_ID_INSERT  
    SET @V_CD_WDEPT = (SELECT CD_DEPT FROM MA_EMP WHERE CD_COMPANY = '01' AND NO_EMP = @P_ID_INSERT);
    SET @V_NM_NOTE = '식자재 입금';
    SET @V_CD_DOCU = '11';
    SET @V_CD_BUDGET = '10200000';
    SET @V_CD_BGACCT = '500100';
    
    IF EXISTS (        
        SELECT  1        
        FROM    FI_ACCTCODE        
        WHERE   CD_COMPANY = @P_CD_COMPANY        
        AND     CD_ACCT = @CD_ACCT        
        AND     (CD_MNG1 = 'A21' OR CD_MNG2 = 'A21' OR CD_MNG3 = 'A21' OR CD_MNG4 = 'A21' OR
                 CD_MNG5 = 'A21' OR CD_MNG6 = 'A21' OR CD_MNG7 = 'A21' OR CD_MNG8 = 'A21'))        
    BEGIN        
        SET @V_CD_UMNG1 = '01';        
    END        
                   
    IF EXISTS (        
        SELECT  1        
        FROM    FI_ACCTCODE        
        WHERE   CD_COMPANY = @P_CD_COMPANY        
        AND     CD_ACCT = @CD_ACCT        
        AND     (CD_MNG1 = 'A23' OR CD_MNG2 = 'A23' OR CD_MNG3 = 'A23' OR CD_MNG4 = 'A23' OR
                 CD_MNG5 = 'A23' OR CD_MNG6 = 'A23' OR CD_MNG7 = 'A23' OR CD_MNG8 = 'A23'))        
    BEGIN        
        SET @V_CD_UMNG3 = '01';        
    END   
END  
ELSE  
BEGIN  
    SET @V_ID_WRITE = @ID_WRITE 
    SET @V_CD_WDEPT = @CD_WDEPT; 
END

IF (@V_SERVER_KEY = 'KORFNT')   --한국가구
BEGIN
    SET @V_CD_DEPT = @V_CD_WDEPT
    SELECT @V_NM_DEPT = NM_DEPT FROM MA_DEPT WHERE CD_COMPANY = @P_CD_COMPANY AND CD_DEPT = @V_CD_WDEPT
END
ELSE
BEGIN
    SET @V_CD_DEPT = @CD_DEPT
    SET @V_NM_DEPT = @NM_DEPT
END

IF (@V_SERVER_KEY = 'EDIYA' AND (SELECT COUNT(*) FROM SA_RCPL WHERE CD_COMPANY = @P_CD_COMPANY AND NO_RCP = @P_NO_RCP AND FG_RCP = '016') > 0 AND @TP_DRCR = '1' AND @CD_ACCT = '12001')
BEGIN
	SET @BILL_PARTNER = '100417' 
	SET @NM_PARTNER = '(주)해피머니아이엔씨'
END

IF(@V_SERVER_KEY = 'WONIL') --원일식품
BEGIN
     DECLARE @DC_RMK NVARCHAR(100)
     SELECT @DC_RMK = DC_RMK
	 FROM   SA_RCPH 
	 WHERE  CD_COMPANY = @P_CD_COMPANY 
	 AND    NO_RCP     = @P_NO_RCP
	 
	 SET @V_NM_NOTE = @NM_PARTNER + '(' + @V_NM_TP + ')' + ' ' + @DC_RMK   
END

DECLARE @V_NO_BDOCU NVARCHAR(20)
DECLARE @V_NO_BDOLINE NUMERIC(5,0)
SET @V_NO_BDOCU = NULL
SET @V_NO_BDOLINE = NULL
--IF((@V_SERVER_KEY LIKE 'HDS%' OR @V_SERVER_KEY LIKE 'DZSQL%'  ) AND ISNULL(@V_NO_IV, '') <> '' ) --현대HDS헤더반제
IF(ISNULL(@V_NO_IV, '') <> '')
BEGIN
     SELECT @V_NO_BDOCU   = M.NO_DOCU,    
			@V_NO_BDOLINE = M.NO_DOLINE,
			@V_AM_EXDO = (CASE WHEN M.CD_EXCH = '000' THEN NULL ELSE @V_AM_EXDO END) /*금액이 원화일경우 외화반제금액에 NULL값을 넣어 반제대상에서 제외하기위함 20100621*/    
      FROM   SA_IV_DOCU_MAPP M  
             INNER JOIN FI_DOCU F ON M.CD_COMPANY = F.CD_COMPANY AND M.CD_PC = F.CD_PC AND M.NO_DOCU = F.NO_DOCU AND M.NO_DOLINE = F.NO_DOLINE    
      WHERE  M.CD_COMPANY = @P_CD_COMPANY    
      AND EXISTS ( SELECT 1 
                   FROM   SA_IVL L    
                   WHERE  M.CD_COMPANY = L.CD_COMPANY    
                   AND    M.NO_IV      = L.NO_IV     
                   AND    M.CD_CC      = L.CD_CC         
                   AND    ISNULL(M.CD_PJT,'')  = ISNULL(L.CD_PJT,'')    
                   AND    ISNULL(M.CD_EXCH,'') = ISNULL(L.CD_EXCH,'')    
                   AND    M.NO_EMP     = L.NO_EMP    
                   AND    L.CD_COMPANY = @P_CD_COMPANY    
                   AND    L.NO_IV      = @V_NO_IV    
                   AND    ISNULL(M.CD_PJT, '')     = ISNULL(@CD_PJT, '')  
                   AND    ISNULL(L.CD_CC, '')	   = ISNULL(@CD_CC, '')
                   )
	--IF @CD_EXCH = '000'  /*금액이 원화일경우 외화반제금액에 NULL값을 넣어 반제대상에서 제외하기위함 20100621*/
	--BEGIN
	--	SET @V_AM_EXDO = NULL
	--END                   
END
ELSE IF((@V_SERVER_KEY LIKE 'SOLIDTECH%' OR @V_SERVER_KEY LIKE 'SKAD%') AND @V_CD_EXC = 'Y' AND ISNULL(@V_NO_RELATION,'') <> '')  -- 가수금
BEGIN 
     SET @V_NO_BDOCU   = @V_NO_RELATION  
     SET @V_NO_BDOLINE = @V_SEQ_RELATION  
END  

EXEC UP_FI_AUTODOCU_1              
        @P_NO_DOCU,             -- 전표번호              
		@P_NO_DOLINE,           -- 라인번호              
		@CD_PC,                 -- 회계단위              
		@CD_COMPANY,            -- 회사코드                 
		@V_CD_WDEPT,            -- 작성부서              
		@V_ID_WRITE,            -- 작성자              
		@P_DT_PROCESS,          -- 매출일자 = 회계일자 = 처리일자              
		@P_NO_ACCT,             -- 회계번호 NO_ACCT
		'3',                    -- 전표구분-대체 TP_DOCU              
		--'33',                 -- 전표유형-일반 CD_DOCU    '11'-->'33(수금등록)'으로 수정     
		@V_CD_DOCU,				-- 전표유형 변경 2009.12.01  
		@V_ST_DOCU,             -- 전표상태-미결 ST_DOCU              
		@P_ID_ACCT,				-- 승인자              
		@TP_DRCR,               -- 차대구분               
		@CD_ACCT,               -- 계정코드              
		@V_NM_NOTE,             -- 적요 NULL            
		@AM_DR,                 -- 차변금액                 
		@AM_CR,                 -- 대변금액              
		@TP_ACAREA,             -- '4' 일 경우 반제원인전표이므로 FI_BANH에 Insert된다        
		@T_CD_RELATION,         -- 연동항목-일반 CD_RELATION                
		@V_CD_BUDGET,           -- 예산코드 CD_BUDGET                 
		@V_CD_FUND,             -- 자금과목 CD_FUND                 
		@V_NO_BDOCU,			-- 원인전표번호 NO_BDOCU                 
		@V_NO_BDOLINE,			-- 원인전표라인 NO_BDOLINE      
		'0',                    -- 타대구분 TP_ETCACCT                 
		@CD_BIZAREA,            -- 귀속사업장      
		@NM_BIZAREA,    
		@CD_CC,                 -- 코스트센터              
		@NM_CC,    
		@CD_PJT,                -- 프로젝트     
		@NM_PJT,    
		@V_CD_DEPT,             -- 부서     
		@V_NM_DEPT,           
		@NO_EMP,                -- 사원 CD_EMPLOY  
		@NM_EMP,    
		@BILL_PARTNER,          -- 거래처 CD_PARTNER              
		@NM_PARTNER,    
		@V_CD_DEPOSIT_CODE,     -- 예적금코드 CD_DEPOSIT (계좌번호로 코드찾기 20090105)    
		@V_CD_DEPOSIT,          -- NM_DEPOSIT    
		@V_CD_CARD,             -- 카드번호 CD_CARD      
		@V_CD_CARD,             -- NM_CARD              
		@V_CD_BANK,             -- 은행코드 CD_BANK  : MA_PARTNER에서 FG_PARTNER = '002' 인것     
		@V_NM_BANK,             -- NM_BANK            
		NULL,                   -- 품목코드 NO_ITEM      
		NULL,                   -- NM_ITEM      
		@FG_TAX,                -- 세무구분코드 TP_TAX      
		@NM_TAX,                -- 세무구분명       
		@P_CD_TRADE,            -- 거래구분코드 CD_TRADE     거래구분(자산변동,지급어음정리,받을어음정리,유가증권거래구분) * 받을어음 : FI_F000006 지급어음 : FI_F000007                  
		@P_NM_TRADE,            -- 거래구분명   NM_TRADE        
		@CD_EXCH,               -- 환종      
		@NM_EXCH,               -- 환종명 @NM_EXCH                  
		@V_CD_UMNG1,            -- CD_UMNG1                
		NULL,                   -- CD_UMNG2                
		@V_CD_UMNG3,            -- CD_UMNG3                
		NULL,                   -- CD_UMNG4             
		NULL,                   -- CD_UMNG5         
		@NO_COMPANY,            --  NO_RES      
		@AM_SUPPLY,             --  AM_SUPPLY      
		@V_CD_MNG,              -- 관리번호 = 계산서번호 or 어음번호 CD_MNG  전자어음경우 관리번호+전자어음채번어음            
		@DT_PROCESS,            -- 거래일자, 시작일자, 발생일자 DT_START                
		@V_DT_END,              -- 만기일자 DT_END                 
		@RT_EXCH,               -- 환율               
		@V_AM_EXDO,             -- 외화금액 AM_EXDO               
		'130',                  -- 모듈구분(국내매출:002 수금등록:130) NO_MODULE       
		@NO_RCP,                -- 모듈관리번호 = 타모듈pkey NO_MDOCU                
		NULL,                   -- 지출결의코드 CD_EPNOTE               
		@ID_WRITE,              -- 전표처리자              
		@V_CD_BGACCT,                   -- 예산계정 CD_BGACCT               
		NULL,                   -- 결의구분 TP_EPNOTE               
		NULL,                   -- 품의내역 NM_PUMM                
		@CUR_DATE,              --시스템일자로 20100506 @P_DT_PROCESS,                        -- 작성일자 DT_WRITE                
		0,                      -- AM_ACTSUM               
		0,                      -- AM_JSUM                
		'N',                    -- YN_GWARE                
		NULL,                   -- 사업계획코드 CD_BIZPLAN              
		NULL,                   --CD_ETC      
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
		NULL,
		NULL,
		NULL,
		NULL,
		@V_FG_JATA,
		@V_NM_FG_JATA,
		@V_NM_ISSUE
	
    
IF( @T_CD_RELATION = '40' ) /* 만일 어음일 경우 */    
BEGIN    
	EXEC  UP_SA_BILL_AUTO_SLIP_EHUM  @CD_COMPANY, @V_CD_MNG, @P_NO_DOCU, @P_NO_DOLINE, '1'    
END    

IF @V_SERVER_KEY LIKE 'TYPHC%'
BEGIN
	IF(@FG_AIS = '317' AND @T_CD_RELATION = '45' ) /* 만일 전자어음일 경우 */    
	BEGIN    
		EXEC  UP_SA_BILL_AUTO_SLIP_EBILL  @CD_COMPANY, @V_CD_MNG, @P_NO_DOCU, @P_NO_DOLINE, '1'    
	END 
END
ELSE
BEGIN
	IF( @T_CD_RELATION = '45' ) /* 만일 전자어음일 경우 */    
	BEGIN    
		EXEC  UP_SA_BILL_AUTO_SLIP_EBILL  @CD_COMPANY, @V_CD_MNG, @P_NO_DOCU, @P_NO_DOLINE, '1'    
	END 
END
                
 FETCH NEXT FROM SRC_CURSOR INTO @CD_COMPANY,@NO_RCP, @V_NO_LINE, @CD_BIZAREA ,@CD_WDEPT,@BILL_PARTNER,@FG_TAX ,@ID_WRITE ,@DT_PROCESS ,@AM_DR   ,@AM_CR   , @AM_SUPPLY, @DT_TAX    ,@FG_TRANS   ,@CD_CC   ,@NO_EMP , @CD_PJT,  @CD_EXCH , @NM_EXCH, @RT_EXCH 

,@CD_DEPT ,@CD_PC  ,        
								 @NM_BIZAREA ,@FG_AIS,  @CD_ACCT ,@CLS_ITEM ,@TP_DRCR ,@NM_DEPT ,@NM_CC  ,@NM_EMP  ,@NM_PJT  ,@NM_PARTNER, @T_CD_RELATION, @NM_TAX, @NO_COMPANY,  @TP_ACAREA,    
								 @V_CD_DEPOSIT,  -- 예적금코드          
								 @V_CD_CARD,      -- 신용카드          
								 @V_CD_BANK,      -- 금융기관     
								 @V_NM_BANK,      -- 금융기관          
								 @V_CD_MNG,       -- 관리번호(어음번호,자산번호,차입금번호,유가증권,LC NO,본지점)          
								 @V_DT_END,       -- 어음만기일자          
								 @V_AM_EXDO,      -- 외화금액                    
								 @V_CD_DOCU,      -- 전표유형
								 @V_CD_FUND,       -- 자금과목   
								 @V_FG_JATA,
								 @V_NM_FG_JATA,	
								 @V_NM_ISSUE,
								 @V_NO_IV,
								 @V_NO_RELATION,  
                                 @V_SEQ_RELATION      
END              
              
CLOSE SRC_CURSOR              
DEALLOCATE SRC_CURSOR            

-- 2010.11.03 장은경
SELECT	@V_DOCU_CNT = COUNT(DISTINCT NO_DOCU)
FROM	FI_DOCU
WHERE	CD_COMPANY = @P_CD_COMPANY 
AND		((@P_YN_BATCH = 'N' AND NO_MDOCU = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND NO_MDOCU IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))

IF @V_DOCU_CNT > 1
BEGIN
      SELECT @ERRNO  = 100000,      
             @ERRMSG = '전표처리에러- 전표번호가 하나이상 생성되었습니다. :' + @P_NO_RCP     
      GOTO ERROR  	
END     
              
-- 전표처리가 제대로 되었으면              
UPDATE	SA_RCPH 
SET		TP_AIS = 'Y' 
WHERE	CD_COMPANY = @P_CD_COMPANY
AND		((@P_YN_BATCH = 'N' AND NO_RCP = @P_NO_RCP) OR (@P_YN_BATCH = 'Y' AND NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_NO_RCP))))
AND		TP_AIS = 'N'              
            
IF ( @@ROWCOUNT = 0  )    
BEGIN    
      SELECT @ERRNO  = 100000,      
             @ERRMSG = '전표처리에러- 해당 수금데이터의 상태가 전표처리중에 변경되었습니다. :' + @P_NO_RCP     
      GOTO ERROR      
END     
      
RETURN             
    
ERROR:      
    RAISERROR (@ERRMSG, 18, 1)
GO

