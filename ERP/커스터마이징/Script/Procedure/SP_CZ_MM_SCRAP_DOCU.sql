SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                                                        
ALTER PROCEDURE [NEOE].[SP_CZ_MM_SCRAP_DOCU]                                                      
(                                                      
	@P_CD_COMPANY	NVARCHAR(7),                                                      
	@P_NO_SCRAP     NVARCHAR(20),                                      
	@P_NO_MODULE    NVARCHAR(3)					
)                                                      
AS
                                                      
DECLARE @V_ERRMSG		NVARCHAR(255),
		@V_NO_DOCU		NVARCHAR(20),

		@V_TP_DRCR		NVARCHAR(1),
		@V_CD_ACCT		NVARCHAR(10),
		@V_NM_NOTE		NVARCHAR(150),
		@V_AM_DR		NUMERIC(19,4),
        @V_AM_CR        NUMERIC(19,4),
		@V_TP_ACAREA	NVARCHAR(3),
	    @V_CD_RELATION  NCHAR(2),
		@V_CD_BANK		NVARCHAR(20),
		@V_NM_BANK		NVARCHAR(50),
		@V_CD_DEPOSIT	NVARCHAR(20),
		@V_NO_DEPOSIT	NVARCHAR(20),
		@V_CD_CC        NVARCHAR(12),
		@V_NM_CC		NVARCHAR(50),
		@V_CD_PJT		NVARCHAR(20),
		@V_NM_PJT		NVARCHAR(50),
		@V_CD_PARTNER	NVARCHAR(20),
		@V_NM_PARTNER	NVARCHAR(50),
		@V_NO_EMP       NVARCHAR(10),
		@V_NM_EMP       NVARCHAR(50),
		@V_AM_SUPPLY    NUMERIC(19,4),
		@V_FG_TAX	    NVARCHAR(4),
		@V_NM_TAX       NVARCHAR(200),
		@V_NO_COMPANY	NVARCHAR(20),
		@V_AM_VAT		NUMERIC(17,4),
		@V_CD_BIZAREA	NVARCHAR(7),
		@V_NM_BIZAREA	NVARCHAR(50)

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT @V_NO_DOCU = MAX(NO_DOCU) 
FROM FI_DOCU
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_MODULE = @P_NO_MODULE
AND NO_MDOCU = @P_NO_SCRAP

IF @V_NO_DOCU IS NOT NULL
BEGIN
	SET @V_ERRMSG = @V_NO_DOCU + ' 로 이미 등록된 전표가 있습니다.'
    GOTO ERROR
END

DECLARE SRC_CURSOR CURSOR FOR

/* 차변 : 재고자산감모손실 */
SELECT '1' AS TP_DRCR,
	   '63900' AS CD_ACCT,
	   SH.NM_SCRAP AS NM_NOTE,
	   ISNULL(SL.AM, 0) AS AM_DR,
	   0 AS AM_CR,
	   '0' AS TP_ACAREA,
	   AC.CD_RELATION,
	   NULL AS CD_BANK,
	   NULL AS NM_BANK,
	   NULL AS CD_DEPOSIT,
	   NULL AS NO_DEPOSIT,
	   MC.CD_CC,
	   MC.NM_CC,
	   PJ.NO_PROJECT AS CD_PJT,
	   PJ.NM_PROJECT AS NM_PJT,
	   NULL AS CD_PARTNER,
	   NULL AS NM_PARTNER,
	   ME.NO_EMP,
	   ME.NM_KOR AS NM_EMP,
	   0 AS AM_SUPPLY,
	   '21' AS FG_TAX,
	   CD.NM_SYSDEF AS NM_TAX,
	   NULL AS NO_COMPANY,
	   0 AS AM_VAT,
	   MB.CD_BIZAREA,
	   MB.NM_BIZAREA
FROM CZ_MM_SCRAPH SH
LEFT JOIN (SELECT SL.CD_COMPANY, SL.NO_SCRAP,
                  SUM(ROUND(SL.AM, 0)) AS AM
           FROM CZ_MM_SCRAPL SL
           GROUP BY SL.CD_COMPANY, SL.NO_SCRAP) SL
ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SCRAP = SH.NO_SCRAP
LEFT JOIN FI_ACCTCODE AC ON AC.CD_COMPANY = SH.CD_COMPANY AND AC.CD_ACCT = '63900'
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = ME.CD_COMPANY AND MC.CD_CC = (CASE ME.CD_COMPANY WHEN 'K100' THEN (CASE WHEN ME.CD_CC IN ('010310', '010320', '010340', '010410', '010420', '010430', '010450') THEN ME.CD_CC ELSE '010301' END)
																					   WHEN 'K200' THEN (CASE WHEN ME.CD_CC IN ('020210', '020220', '020240') THEN ME.CD_CC ELSE '020200' END) END)
LEFT JOIN MA_BIZAREA MB ON MB.CD_COMPANY = MC.CD_COMPANY AND MB.CD_BIZAREA = MC.CD_BIZAREA
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = SH.CD_COMPANY AND CD.CD_FIELD = 'MA_B000046' AND CD.CD_SYSDEF = '21'
LEFT JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = SH.CD_COMPANY AND PJ.NO_PROJECT = SH.NO_FILE
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.NO_SCRAP = @P_NO_SCRAP
AND ISNULL(SL.AM, 0) > 0
UNION ALL
/* 대변 : 상품 */    
SELECT '2' AS TP_DRCR,
	   '15100' AS CD_ACCT,
	   SH.NM_SCRAP AS NM_NOTE,
	   0 AS AM_DR,
	   ISNULL(SL.AM, 0) AS AM_CR,
	   '0' AS TP_ACAREA,
	   AC.CD_RELATION,
	   NULL AS CD_BANK,
	   NULL AS NM_BANK,
	   NULL AS CD_DEPOSIT,
	   NULL AS NO_DEPOSIT,
	   MC.CD_CC,
	   MC.NM_CC,
	   PJ.NO_PROJECT AS CD_PJT,
	   PJ.NM_PROJECT AS NM_PJT,
	   NULL AS CD_PARTNER,
	   NULL AS NM_PARTNER,
	   ME.NO_EMP,
	   ME.NM_KOR AS NM_EMP,
	   0 AS AM_SUPPLY,
	   NULL AS FG_TAX,
	   NULL AS NM_TAX,
	   NULL AS NO_COMPANY,
	   0 AS AM_VAT,
	   NULL AS CD_BIZAREA,
	   NULL AS NM_BIZAREA
FROM CZ_MM_SCRAPH SH
LEFT JOIN (SELECT SL.CD_COMPANY, SL.NO_SCRAP,
                  SUM(ROUND(SL.AM, 0)) AS AM
           FROM CZ_MM_SCRAPL SL
           GROUP BY SL.CD_COMPANY, SL.NO_SCRAP) SL
ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SCRAP = SH.NO_SCRAP
LEFT JOIN FI_ACCTCODE AC ON AC.CD_COMPANY = SH.CD_COMPANY AND AC.CD_ACCT = '15100'
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = ME.CD_COMPANY AND MC.CD_CC = (CASE ME.CD_COMPANY WHEN 'K100' THEN (CASE WHEN ME.CD_CC IN ('010310', '010320', '010340', '010410', '010420', '010430', '010450') THEN ME.CD_CC ELSE '010301' END)
																					   WHEN 'K200' THEN (CASE WHEN ME.CD_CC IN ('020210', '020220', '020240') THEN ME.CD_CC ELSE '020200' END) END)
LEFT JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = SH.CD_COMPANY AND PJ.NO_PROJECT = SH.NO_FILE
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.NO_SCRAP = @P_NO_SCRAP
AND ISNULL(SL.AM, 0) > 0

------------------------------------------
-- 여기서 부터 전표처리 하기 위한 부분  --
------------------------------------------
DECLARE @V_NO_DOLINE	NUMERIC(4, 0)	-- 전표라인
DECLARE @V_DT_ACCT		NVARCHAR(8)		-- 회계일자
DECLARE @V_NO_ACCT		NUMERIC(5, 0)	-- 회계번호 
DECLARE @V_DT_WRITE		NVARCHAR(8)		-- 작성일자
DECLARE @V_ID_WEMP		NVARCHAR(10)	-- 작성자
DECLARE @V_ID_ACCT		NVARCHAR(10)	-- 승인자
DECLARE @V_CD_PC		NVARCHAR(7)		-- 회계단위
DECLARE @V_CD_WDEPT		NVARCHAR(12)	-- 작성부서
DECLARE @V_ST_DOCUAPP	NVARCHAR(1)		-- 전표권한
DECLARE @V_ST_DOCU		NVARCHAR(1)		-- 전표상태
DECLARE @V_NM_PUMM      NVARCHAR(150)	-- 품의내역
DECLARE @V_TP_EVIDENCE	NVARCHAR(1)		-- 증빙
DECLARE @V_DT_GI	    NVARCHAR(8)		-- 등록일자

SELECT @V_DT_GI = MAX(DT_GI),
	   @V_NO_EMP = MAX(NO_EMP)
FROM CZ_MM_SCRAPH
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_SCRAP = @P_NO_SCRAP

SET @V_NO_DOLINE = 0
SET @V_DT_ACCT = @V_DT_GI	

SET @V_DT_WRITE = CONVERT(NVARCHAR(8), GETDATE(), 112)
SET @V_NM_PUMM = NULL

SELECT @V_ID_WEMP = ME.NO_EMP,
	   @V_ID_ACCT = ME.NO_EMP,
	   @V_CD_PC = MB.CD_PC,
	   @V_CD_WDEPT = ME.CD_DEPT,
	   @V_ST_DOCUAPP = ST_DOCUAPP 
FROM MA_USER MU
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = MU.CD_COMPANY AND ME.NO_EMP = MU.NO_EMP
LEFT JOIN MA_BIZAREA MB ON MB.CD_COMPANY = ME.CD_COMPANY AND MB.CD_BIZAREA = ME.CD_BIZAREA 
WHERE MU.CD_COMPANY = @P_CD_COMPANY
AND MU.ID_USER = @V_NO_EMP

--IF (@V_ST_DOCUAPP = '2' OR @V_ST_DOCUAPP = '3')
--	SET @V_ST_DOCU = '2'
--ELSE
	SET @V_ST_DOCU = '1'

-- 전표번호 채번
EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @V_DT_ACCT, @V_NO_DOCU OUTPUT

--회계번호 채번
IF @V_ST_DOCU = '1'
BEGIN
	SET @V_NO_ACCT = 0
	SET @V_ID_ACCT = NULL
END
ELSE
BEGIN
	EXEC UP_FI_DOCU_CREATE_SEQ4 @P_CD_COMPANY, @V_CD_PC , 'FI04', @V_DT_ACCT, @V_NO_ACCT OUTPUT
END

OPEN SRC_CURSOR
FETCH NEXT FROM SRC_CURSOR INTO @V_TP_DRCR,
								@V_CD_ACCT,
								@V_NM_NOTE,
								@V_AM_DR,
								@V_AM_CR,
								@V_TP_ACAREA,
								@V_CD_RELATION,
								@V_CD_BANK,
								@V_NM_BANK,
								@V_CD_DEPOSIT,
								@V_NO_DEPOSIT,
								@V_CD_CC,
								@V_NM_CC,
								@V_CD_PJT,
								@V_NM_PJT,
								@V_CD_PARTNER,
								@V_NM_PARTNER,
								@V_NO_EMP,
								@V_NM_EMP,
								@V_AM_SUPPLY,
								@V_FG_TAX,
								@V_NM_TAX,
								@V_NO_COMPANY,
								@V_AM_VAT,
								@V_CD_BIZAREA,
								@V_NM_BIZAREA

IF @@FETCH_STATUS = -1 
BEGIN 
	RAISERROR ('전표처리할 데이터가 존재하지 않습니다', 18, 1)
	CLOSE SRC_CURSOR
	DEALLOCATE SRC_CURSOR
	RETURN
END

WHILE @@FETCH_STATUS = 0
BEGIN
	SET @V_NO_DOLINE = @V_NO_DOLINE + 1
	
	-- 증빙 추가
	SET @V_TP_EVIDENCE = (SELECT TP_EVIDENCE
						  FROM CZ_FI_ACCT_EVIDENCEL
						  WHERE CD_COMPANY = @P_CD_COMPANY
						  AND ISNULL(FG_TAX, '') = ''
						  AND CD_ACCT = @V_CD_ACCT)
	
	EXEC UP_FI_AUTODOCU_1 @V_NO_DOCU,			  --전표번호 NO_DOCU
						  @V_NO_DOLINE,			  --라인번호 NO_DOLINE
						  @V_CD_PC,				  --회계단위 CD_PC
						  @P_CD_COMPANY,		  --회사코드 CD_COMPANY
						  @V_CD_WDEPT,			  --작성부서 CD_WDEPT
						  @V_ID_WEMP,			  --작성자	 ID_WRITE
						  @V_DT_ACCT,		      --회계일자 DT_ACCT
						  @V_NO_ACCT,			  --회계번호 NO_ACCT
						  '3',					  --전표구분-대체 TP_DOCU
						  '11',					  --전표유형-일반 CD_DOCU
						  @V_ST_DOCU,			  --전표상태-미결 ST_DOCU
						  @V_ID_ACCT,			  --승인자	 ID_ACCEPT
						  @V_TP_DRCR,			  --차대구분 TP_DRCR
						  @V_CD_ACCT,			  --계정코드 CD_ACCT
						  @V_NM_NOTE,			  --적요	 NM_NOTE
						  @V_AM_DR,				  --차변금액 AM_DR
						  @V_AM_CR,				  --대변금액 AM_CR
						  @V_TP_ACAREA,			  --'4' 일 경우 반제원인전표이므로 FI_BANH에 Insert된다
						  @V_CD_RELATION,		  --연동항목 CD_RELATION
						  @V_CD_CC,				  --예산코드 CD_BUDGET
						  NULL,					  --자금과목 CD_FUND
						  NULL,					  --원인전표번호 NO_BDOCU
						  0,					  --원인전표라인 NO_BDOLINE
						  '0',					  --타대구분 TP_ETCACCT
						  @V_CD_BIZAREA,		  --귀속사업장 CD_BIZAREA
						  @V_NM_BIZAREA,		  --귀속사업장명 NM_BIZAREA
						  @V_CD_CC,				  --코스트센터 CD_CC
						  @V_NM_CC,				  --코스트센터명 NM_CC
						  @V_CD_PJT,			  --프로젝트 CD_PJT
						  @V_NM_PJT,			  --프로젝트명 NM_PJT
						  NULL,					  --부서 CD_DEPT
						  NULL,					  --부서명 NM_DEPT
						  @V_NO_EMP,			  --사원 CD_EMPLOY
						  @V_NM_EMP,			  --사원명 NM_EMPLOY  
						  @V_CD_PARTNER,		  --거래처 CD_PARTNER
						  @V_NM_PARTNER,		  --거래처명 NM_PARTNER
						  @V_CD_DEPOSIT,		  --예적금코드 CD_DEPOSIT
						  @V_NO_DEPOSIT,		  --예적금명 NM_DEPOSIT
						  NULL,					  --카드번호 CD_CARD
						  NULL,					  --카드명 NM_CARD
						  @V_CD_BANK,			  --은행코드 CD_BANK  : MA_PARTNER에서 FG_PARTNER = '002' 인것
						  @V_NM_BANK,			  --은행명 NM_BANK
						  NULL,					  --품목코드 NO_ITEM
						  NULL,					  --품목명 NM_ITEM
						  @V_FG_TAX,			  --세무구분 TP_TAX
						  @V_NM_TAX,			  --세무구분명 NM_TAX
						  NULL,					  --거래구분 CD_TRADE
						  NULL,					  --거래구분명 NM_TRADE
						  NULL,					  --통화코드 CD_EXCH
						  NULL,					  --통화명 NM_EXCH
						  NULL,					  --사용자정의1 CD_UMNG1
						  NULL,					  --사용자정의2 CD_UMNG2
						  NULL,					  --사용자정의3 CD_UMNG3
						  NULL,					  --사용자정의4 CD_UMNG4
						  NULL,					  --사용자정의5 CD_UMNG5
						  @V_NO_COMPANY,		  --NO_COMPANY
						  @V_AM_SUPPLY,			  --과세표준 AM_SUPPLY
						  @P_NO_SCRAP,		      --관리번호 = 계산서번호 CD_MNG
						  NULL,					  --수금에정일 DT_START
						  NULL,					  --만기일자 DT_END
						  0,					  --환율 RT_EXCH
						  0,					  --외화금액 AM_EXDO
						  @P_NO_MODULE,			  --모듈구분(D01) NO_MODULE
						  @P_NO_SCRAP,		      --모듈관리번호 = 타모듈pkey NO_MDOCU
						  @V_CD_ACCT,			  --지출결의코드 CD_EPNOTE
						  @V_NO_EMP,			  --전표처리자 ID_INSERT
						  @V_CD_ACCT,			  --예산계정 CD_BGACCT
						  NULL,					  --결의구분 TP_EPNOTE
						  @V_NM_PUMM,			  --품의내역 NM_PUMM
						  @V_DT_WRITE,			  --작성일자 DT_WRITE              
						  0,					  --AM_ACTSUM
						  0,					  --AM_JSUM
						  'N',					  --YN_GWARE
						  NULL,					  --사업계획코드 CD_BIZPLAN
						  NULL,					  --CD_ETC
						  NULL,
						  NULL,
						  NULL,
						  NULL ,
						  NULL,
						  NULL,					  --@P_NM_ARRIVE NVARCHAR(30) = NULL, -- 미착품구분 (전방추가)     
						  NULL,					  --@P_CD_PAYDEDUCT  NVARCHAR(3) = NULL,    --인사추가 (YTN)    
						  NULL,					  --@P_JUMIN_NO  NVARCHAR(40) = NULL,    --EC 추가 (본죽)    
						  NULL,					  --@P_DT_PAY   VARCHAR(8) = NULL,  --인사추가 (YTN)    
						  NULL,					  --@P_NO_INV  NVARCHAR(20) = NULL,    -- INVOICE NO    
						  NULL,					  --@P_NO_TO   NVARCHAR(20) = NULL,     -- 면장번호(NO_TO)    
						  @V_AM_VAT,			  --NUMERIC(17,4) = 0		--부가세 (전방추가)
						  NULL,
						  NULL,
						  NULL,
						  NULL,
						  NULL,
						  NULL,
						  NULL,
						  0,
						  0,
						  NULL,
						  0,
						  NULL,
						  @V_TP_EVIDENCE

	IF @V_CD_RELATION = '30'                     
    BEGIN                 
        /* FI_TAX 만들기*/   
	    EXEC UP_FI_AUTODOCU_TAX @P_CD_COMPANY, @V_CD_PC, @V_NO_DOCU, @V_NO_DOLINE, @V_AM_SUPPLY, '2', 0, @V_NO_COMPANY
    END

	FETCH NEXT FROM SRC_CURSOR INTO @V_TP_DRCR,
									@V_CD_ACCT,
									@V_NM_NOTE,
									@V_AM_DR,
									@V_AM_CR,
									@V_TP_ACAREA,
									@V_CD_RELATION,
									@V_CD_BANK,
									@V_NM_BANK,
									@V_CD_DEPOSIT,
									@V_NO_DEPOSIT,
									@V_CD_CC,
									@V_NM_CC,
									@V_CD_PJT,
									@V_NM_PJT,
									@V_CD_PARTNER,
									@V_NM_PARTNER,
									@V_NO_EMP,
									@V_NM_EMP,
									@V_AM_SUPPLY,
									@V_FG_TAX,
									@V_NM_TAX,
									@V_NO_COMPANY,
									@V_AM_VAT,
									@V_CD_BIZAREA,
									@V_NM_BIZAREA
END

SELECT @V_NO_DOCU

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

RETURN

ERROR:
    ROLLBACK TRAN
    RAISERROR (@V_ERRMSG, 18, 1)

GO
