USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_IMP_PMT_MNG_APPLICATION_DOCU]    Script Date: 2017-07-19 오전 9:01:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

                                                        
ALTER PROCEDURE [NEOE].[SP_CZ_FI_IMP_PMT_MNG_RETURN_DOCU]                                                      
(                                                      
	@P_CD_COMPANY	NVARCHAR(7),                                                      
	@P_NO_RETURN    NVARCHAR(20),
	@P_CD_BANK		NVARCHAR(20),
	@P_CD_DEPOSIT	NVARCHAR(20),                                                      
	@P_NO_MODULE    NVARCHAR(3),
	@P_ID_INSERT    NVARCHAR(15)					
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
		@V_NO_EMP       NVARCHAR(10),
		@V_NM_EMP       NVARCHAR(50)

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT @V_NO_DOCU = MAX(NO_DOCU) 
FROM FI_DOCU
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_MODULE = @P_NO_MODULE
AND NO_MDOCU = @P_NO_RETURN

IF @V_NO_DOCU IS NOT NULL
BEGIN
	SET @V_ERRMSG = @V_NO_DOCU + ' 로 이미 등록된 전표가 있습니다.'
    GOTO ERROR
END

DECLARE SRC_CURSOR CURSOR FOR

/* 차변 : 보통예금 */
SELECT '1' AS TP_DRCR,
	   '10300' AS CD_ACCT,
	   ('수입관세환급 (' + SUBSTRING(FL.NO_IMPORT, 1, 5)  + '-' + SUBSTRING(FL.NO_IMPORT, 6, 2) + '-' + SUBSTRING(FL.NO_IMPORT, 8, 7) + ')') AS NM_NOTE,
	   ISNULL(FL.AM_RETURN, 0) AS AM_DR,
	   0 AS AM_CR,
	   '0' AS TP_ACAREA,
	   AC.CD_RELATION,
	   MP.CD_PARTNER AS CD_BANK,
	   MP.LN_PARTNER AS NM_BANK,
	   FD.CD_DEPOSIT,
	   FD.NO_DEPOSIT,
	   NULL AS CD_CC,
	   NULL AS NM_CC,
	   NULL AS CD_PJT,
	   NULL AS NM_PJT,
	   NULL AS NO_EMP,
	   NULL AS NM_EMP
FROM (SELECT CD_COMPANY, NO_IMPORT,
			 SUM(AM_RETURN) AS AM_RETURN 
	  FROM CZ_FI_IMP_PMTL
	  WHERE CD_COMPANY = @P_CD_COMPANY
	  AND NO_RETURN = @P_NO_RETURN
	  AND ISNULL(AM_RETURN, 0) > 0
	  GROUP BY CD_COMPANY, NO_IMPORT) FL
LEFT JOIN FI_ACCTCODE AC ON AC.CD_COMPANY = FL.CD_COMPANY AND AC.CD_ACCT = '10300'
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = FL.CD_COMPANY AND MP.CD_PARTNER = @P_CD_BANK
LEFT JOIN FI_DEPOSIT FD ON FD.CD_COMPANY = FL.CD_COMPANY AND FD.CD_DEPOSIT = @P_CD_DEPOSIT
UNION ALL
/* 대변 : 상품관세환급금(61300), 상품매입관세(52120) */    
SELECT (CASE WHEN FL.CD_COMPANY = 'K100' THEN '1' ELSE '2' END) AS TP_DRCR,
	   (CASE WHEN FL.CD_COMPANY = 'K100' THEN '52120' ELSE '61300' END) AS CD_ACCT,
	   ('수입관세환급 (' + PJ.NO_PROJECT + ')') AS NM_NOTE,
	   (CASE WHEN FL.CD_COMPANY = 'K100' THEN -ISNULL(FL.AM_RETURN, 0) ELSE 0 END) AS AM_DR,
	   (CASE WHEN FL.CD_COMPANY = 'K100' THEN 0 ELSE ISNULL(FL.AM_RETURN, 0) END) AS AM_CR,
	   '0' AS TP_ACAREA,
	   AC.CD_RELATION,
	   NULL AS CD_BANK,
	   NULL AS NM_BANK,
	   NULL AS CD_DEPOSIT,
	   NULL AS NO_DEPOSIT,
	   MC.CD_CC AS CD_CC,
	   MC.NM_CC AS NM_CC,
	   PJ.NO_PROJECT AS CD_PJT,
	   PJ.NM_PROJECT AS NM_PJT,
	   ME.NO_EMP AS NO_EMP,
	   ME.NM_KOR AS NM_EMP
FROM (SELECT CD_COMPANY, NO_IMPORT, NO_PO,
			 SUM(AM_RETURN) AS AM_RETURN 
      FROM CZ_FI_IMP_PMTL
	  WHERE CD_COMPANY = @P_CD_COMPANY
	  AND NO_RETURN = @P_NO_RETURN
	  AND ISNULL(AM_RETURN, 0) > 0
	  GROUP BY CD_COMPANY, NO_IMPORT, NO_PO) FL
LEFT JOIN FI_ACCTCODE AC ON AC.CD_COMPANY = FL.CD_COMPANY AND AC.CD_ACCT = (CASE WHEN FL.CD_COMPANY = 'K100' THEN '52120' ELSE '61300' END)
LEFT JOIN PU_POH PH ON PH.CD_COMPANY = FL.CD_COMPANY AND PH.NO_PO = FL.NO_PO
LEFT JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = PH.CD_COMPANY AND PJ.NO_PROJECT = PH.CD_PJT
LEFT JOIN MA_PURGRP PG ON PG.CD_COMPANY = PH.CD_COMPANY AND PG.CD_PURGRP = PH.CD_PURGRP
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = PG.CD_COMPANY AND MC.CD_CC = PG.CD_CC
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = PH.CD_COMPANY AND ME.NO_EMP = PH.NO_EMP

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
DECLARE @V_DT_RETURN	NVARCHAR(8)		-- 환급일자

SELECT @V_DT_RETURN = MAX(IH.DT_RETURN) 
FROM CZ_FI_IMP_PMTL IL
LEFT JOIN CZ_FI_IMP_PMTH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IMPORT = IL.NO_IMPORT
WHERE IL.CD_COMPANY = @P_CD_COMPANY
AND IL.NO_RETURN = @P_NO_RETURN

SET @V_NO_DOLINE = 0
SET @V_DT_ACCT = @V_DT_RETURN
SET @V_DT_WRITE = CONVERT(NVARCHAR(8), GETDATE(), 112)
SET @V_NM_PUMM = '환급일자 : ' + ISNULL(CONVERT(NVARCHAR(10), CONVERT(DATETIME, @V_DT_RETURN), 111), '')

SELECT @V_ID_WEMP = ME.NO_EMP,
	   @V_ID_ACCT = ME.NO_EMP,
	   @V_CD_PC = MB.CD_PC,
	   @V_CD_WDEPT = ME.CD_DEPT,
	   @V_ST_DOCUAPP = ST_DOCUAPP 
FROM MA_USER MU
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = MU.CD_COMPANY AND ME.NO_EMP = MU.NO_EMP
LEFT JOIN MA_BIZAREA MB ON MB.CD_COMPANY = ME.CD_COMPANY AND MB.CD_BIZAREA = ME.CD_BIZAREA 
WHERE MU.CD_COMPANY = @P_CD_COMPANY
AND MU.ID_USER = @P_ID_INSERT

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
								@V_NO_EMP,
								@V_NM_EMP

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
						  NULL,					  --귀속사업장 CD_BIZAREA
						  NULL,					  --귀속사업장명 NM_BIZAREA
						  @V_CD_CC,				  --코스트센터 CD_CC
						  @V_NM_CC,				  --코스트센터명 NM_CC
						  @V_CD_PJT,			  --프로젝트 CD_PJT
						  @V_NM_PJT,			  --프로젝트명 NM_PJT
						  NULL,					  --부서 CD_DEPT
						  NULL,					  --부서명 NM_DEPT
						  @V_NO_EMP,			  --사원 CD_EMPLOY
						  @V_NM_EMP,			  --사원명 NM_EMPLOY  
						  NULL,					  --거래처 CD_PARTNER
						  NULL,					  --거래처명 NM_PARTNER
						  @V_CD_DEPOSIT,		  --예적금코드 CD_DEPOSIT
						  @V_NO_DEPOSIT,		  --예적금명 NM_DEPOSIT
						  NULL,					  --카드번호 CD_CARD
						  NULL,					  --카드명 NM_CARD
						  @V_CD_BANK,			  --은행코드 CD_BANK  : MA_PARTNER에서 FG_PARTNER = '002' 인것
						  @V_NM_BANK,			  --은행명 NM_BANK
						  NULL,					  --품목코드 NO_ITEM
						  NULL,					  --품목명 NM_ITEM
						  NULL,					  --세무구분 TP_TAX
						  NULL,					  --세무구분명 NM_TAX
						  NULL,					  --거래구분 CD_TRADE
						  NULL,					  --거래구분명 NM_TRADE
						  NULL,					  --통화코드 CD_EXCH
						  NULL,					  --통화명 NM_EXCH
						  NULL,					  --사용자정의1 CD_UMNG1
						  NULL,					  --사용자정의2 CD_UMNG2
						  NULL,					  --사용자정의3 CD_UMNG3
						  NULL,					  --사용자정의4 CD_UMNG4
						  NULL,					  --사용자정의5 CD_UMNG5
						  NULL,					  --NO_COMPANY
						  NULL,					  --과세표준 AM_SUPPLY
						  @P_NO_RETURN,			  --관리번호 = 계산서번호 CD_MNG
						  NULL,					  --수금에정일 DT_START
						  NULL,					  --만기일자 DT_END
						  0,					  --환율 RT_EXCH
						  0,					  --외화금액 AM_EXDO
						  @P_NO_MODULE,			  --모듈구분(D01) NO_MODULE
						  @P_NO_RETURN,		      --모듈관리번호 = 타모듈pkey NO_MDOCU
						  @V_CD_ACCT,			  --지출결의코드 CD_EPNOTE
						  @P_ID_INSERT,			  --전표처리자 ID_INSERT
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
						  NULL,					  --NUMERIC(17,4) = 0		--부가세 (전방추가)
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
									@V_NO_EMP,
									@V_NM_EMP
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

RETURN

ERROR:
    ROLLBACK TRAN
    RAISERROR (@V_ERRMSG, 18, 1)

GO

