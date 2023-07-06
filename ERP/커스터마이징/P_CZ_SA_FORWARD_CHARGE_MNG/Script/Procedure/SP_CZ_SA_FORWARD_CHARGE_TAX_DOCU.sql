USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_FORWARD_CHARGE_TAX_DOCU]    Script Date: 2017-07-19 오전 9:01:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_FORWARD_CHARGE_TAX_DOCU]                                                      
(                                                      
	@P_CD_COMPANY	NVARCHAR(7),
	@P_DT_MONTH		NVARCHAR(6),
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
		@V_NM_BIZAREA	NVARCHAR(50),
		@V_NO_KEY		NVARCHAR(20)

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SET @V_NO_KEY = 'FORD_TAX_' + @P_DT_MONTH

SELECT @V_NO_DOCU = MAX(NO_DOCU) 
FROM FI_DOCU
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_MODULE = @P_NO_MODULE
AND NO_MDOCU = @V_NO_KEY

IF @V_NO_DOCU IS NOT NULL
BEGIN
	SET @V_ERRMSG = @V_NO_DOCU + ' 로 이미 등록된 전표가 있습니다.'
    GOTO ERROR
END

DECLARE SRC_CURSOR CURSOR FOR

/* 차변 : 지급수수료 */
SELECT '1' AS TP_DRCR,
	   '56900' AS CD_ACCT,
	   '통관수수료-' + CONVERT(NVARCHAR, CONVERT(INT, ISNULL(FL.SEQ_TAX_MONTH, 0))) + 
       ' 수주금액 : ' + FORMAT(ISNULL(OL.AM_SO, 0), '##,##0') AS NM_NOTE,
	   ISNULL(FL.AM_TAX_MONTH, 0) AS AM_DR,
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
	   NULL AS NO_EMP,
	   NULL AS NM_EMP,
	   0 AS AM_SUPPLY,
	   '' AS FG_TAX,
	   '' AS NM_TAX,
	   NULL AS NO_COMPANY,
	   0 AS AM_VAT,
	   MB.CD_BIZAREA,
	   MB.NM_BIZAREA
FROM CZ_SA_FORWARD_CHARGE_L FL
LEFT JOIN FI_ACCTCODE AC ON AC.CD_COMPANY = FL.CD_COMPANY AND AC.CD_ACCT = '56900'
LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_IO, OL.CD_PJT, SL.AM_SO,
		          ROW_NUMBER() OVER (PARTITION BY OL.CD_COMPANY, OL.NO_IO ORDER BY SL.AM_SO DESC) AS IDX 
		   FROM (SELECT CD_COMPANY, NO_IO, CD_PJT
		         FROM MM_QTIO
		         GROUP BY CD_COMPANY, NO_IO, CD_PJT) OL
		   LEFT JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
		   				     SUM(SL.AM_WONAMT) AS AM_SO
		   		      FROM SA_SOL SL
		   		      GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
		   ON SL.CD_COMPANY = OL.CD_COMPANY AND SL.NO_SO = OL.CD_PJT) OL
ON OL.CD_COMPANY = FL.CD_COMPANY AND OL.NO_IO = FL.NO_IO AND OL.IDX = 1
LEFT JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = OL.CD_COMPANY AND PJ.NO_PROJECT = OL.CD_PJT
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = OL.CD_COMPANY AND SH.NO_SO = OL.CD_PJT
LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = SH.CD_COMPANY AND SG.CD_SALEGRP = SH.CD_SALEGRP
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = SG.CD_COMPANY AND MC.CD_CC = SG.CD_CC
LEFT JOIN MA_BIZAREA MB ON MB.CD_COMPANY = FL.CD_COMPANY
WHERE FL.CD_COMPANY = @P_CD_COMPANY
AND FL.DT_TAX_MONTH = @P_DT_MONTH
AND ISNULL(FL.AM_TAX_MONTH, 0) > 0
UNION ALL
/* 차변 : 부가세대급금 */
SELECT '1' AS TP_DRCR,
	   '12700' AS CD_ACCT,
	   '부가세대급금' AS NM_NOTE,
	   ISNULL(FL.AM_VAT_MONTH, 0) AS AM_DR,
	   0 AS AM_CR,
	   '0' AS TP_ACAREA,
	   AC.CD_RELATION,
	   NULL AS CD_BANK,
	   NULL AS NM_BANK,
	   NULL AS CD_DEPOSIT,
	   NULL AS NO_DEPOSIT,
	   NULL AS CD_CC,
	   NULL AS NM_CC,
	   NULL AS CD_PJT,
	   NULL AS NM_PJT,
	   MP.CD_PARTNER AS CD_PARTNER,
	   MP.LN_PARTNER AS NM_PARTNER,
	   NULL AS NO_EMP,
	   NULL AS NM_EMP,
	   ISNULL(FL.AM_TAX_MONTH, 0) AS AM_SUPPLY,
	   '21' AS FG_TAX,
	   CD.NM_SYSDEF AS NM_TAX,
	   MP.NO_COMPANY AS NO_COMPANY,
	   ISNULL(FL.AM_VAT_MONTH, 0) AS AM_VAT,
	   MB.CD_BIZAREA,
	   MB.NM_BIZAREA
FROM (SELECT FL.CD_COMPANY,
			 '05112' AS CD_PARTNER,
			 SUM(FL.AM_TAX_MONTH) AS AM_TAX_MONTH,
			 (SUM(FL.AM_TAX_MONTH) * 0.1) AS AM_VAT_MONTH
	  FROM CZ_SA_FORWARD_CHARGE_L FL
	  WHERE FL.CD_COMPANY = @P_CD_COMPANY
	  AND FL.DT_TAX_MONTH = @P_DT_MONTH
	  GROUP BY FL.CD_COMPANY) FL
LEFT JOIN FI_ACCTCODE AC ON AC.CD_COMPANY = FL.CD_COMPANY AND AC.CD_ACCT = '12700'
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = FL.CD_COMPANY AND CD.CD_FIELD = 'MA_B000046' AND CD.CD_SYSDEF = '21'
LEFT JOIN MA_BIZAREA MB ON MB.CD_COMPANY = FL.CD_COMPANY
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = FL.CD_COMPANY AND MP.CD_PARTNER = FL.CD_PARTNER
WHERE ISNULL(FL.AM_TAX_MONTH, 0) > 0
UNION ALL
/* 대변 : 국내미지급금 */    
SELECT '2' AS TP_DRCR,
	   '30310' AS CD_ACCT,
	   '월말 통관수수료' AS NM_NOTE,
	   0 AS AM_DR,
	   (ISNULL(FL.AM_TAX_MONTH, 0) + ISNULL(FL.AM_VAT_MONTH, 0)) AS AM_CR,
	   '0' AS TP_ACAREA,
	   AC.CD_RELATION,
	   NULL AS CD_BANK,
	   NULL AS NM_BANK,
	   NULL AS CD_DEPOSIT,
	   NULL AS NO_DEPOSIT,
	   NULL AS CD_CC,
	   NULL AS NM_CC,
	   NULL AS CD_PJT,
	   NULL AS NM_PJT,
	   MP.CD_PARTNER AS CD_PARTNER,
	   MP.LN_PARTNER AS NM_PARTNER,
	   NULL AS NO_EMP,
	   NULL AS NM_EMP,
	   0 AS AM_SUPPLY,
	   NULL AS FG_TAX,
	   NULL AS NM_TAX,
	   MP.NO_COMPANY,
	   0 AS AM_VAT,
	   NULL AS CD_BIZAREA,
	   NULL AS NM_BIZAREA
FROM (SELECT FL.CD_COMPANY,
             '05112' AS CD_PARTNER,
	  	     SUM(FL.AM_TAX_MONTH) AS AM_TAX_MONTH,
	  	     (SUM(FL.AM_TAX_MONTH) * 0.1) AS AM_VAT_MONTH
	  FROM CZ_SA_FORWARD_CHARGE_L FL
	  WHERE FL.CD_COMPANY = @P_CD_COMPANY
	  AND FL.DT_TAX_MONTH = @P_DT_MONTH
	  GROUP BY FL.CD_COMPANY) FL
LEFT JOIN FI_ACCTCODE AC ON AC.CD_COMPANY = FL.CD_COMPANY AND AC.CD_ACCT = '30310'
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = FL.CD_COMPANY AND MP.CD_PARTNER = FL.CD_PARTNER
WHERE(ISNULL(FL.AM_TAX_MONTH, 0) + ISNULL(FL.AM_VAT_MONTH, 0)) > 0

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

SET @V_NO_DOLINE = 0
SET @V_DT_ACCT = CONVERT(NVARCHAR(8), EOMONTH(@P_DT_MONTH + '01'), 112)
SET @V_DT_WRITE = CONVERT(NVARCHAR(8), GETDATE(), 112)
SET @V_NM_PUMM = '통관수수료'

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
	SET @V_TP_EVIDENCE = (CASE WHEN @V_CD_ACCT = '56900' THEN '3' ELSE NULL END)
	
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
						  @V_NO_KEY,		      --관리번호 = 계산서번호 CD_MNG
						  NULL,					  --수금에정일 DT_START
						  NULL,					  --만기일자 DT_END
						  0,					  --환율 RT_EXCH
						  0,					  --외화금액 AM_EXDO
						  @P_NO_MODULE,			  --모듈구분(D01) NO_MODULE
						  @V_NO_KEY,		      --모듈관리번호 = 타모듈pkey NO_MDOCU
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

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

UPDATE FH
SET FH.NO_MDOCU_TAX = @V_NO_KEY
FROM CZ_SA_FORWARD_CHARGE_H FH
WHERE FH.CD_COMPANY = @P_CD_COMPANY
AND EXISTS (SELECT 1 
            FROM CZ_SA_FORWARD_CHARGE_L FL
            WHERE FL.CD_COMPANY = FH.CD_COMPANY
            AND FL.CD_PARTNER = FH.CD_PARTNER
            AND FL.TP_DELIVERY = FH.TP_DELIVERY
            AND FL.DT_MONTH = FH.DT_MONTH
            AND FL.DT_TAX_MONTH = @P_DT_MONTH
            AND ISNULL(FL.AM_TAX_MONTH, 0) > 0)

RETURN

ERROR:
    ROLLBACK TRAN
    RAISERROR (@V_ERRMSG, 18, 1)

GO

