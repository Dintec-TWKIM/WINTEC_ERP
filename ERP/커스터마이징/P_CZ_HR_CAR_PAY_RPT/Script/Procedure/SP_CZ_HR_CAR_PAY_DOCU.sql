USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_CAR_PAY_DOCU]    Script Date: 2017-08-18 오전 9:31:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
                                                        
ALTER PROCEDURE [NEOE].[SP_CZ_HR_CAR_PAY_DOCU]                                                      
(                                                      
	@P_CD_COMPANY	NVARCHAR(7),                                                      
	@P_YYYYMM       NVARCHAR(6),                                                      
	@P_NO_MODULE    NVARCHAR(3),
	@P_ID_INSERT    NVARCHAR(15)					
)                                                      
AS
                                                      
DECLARE @V_ERRMSG		NVARCHAR(255),
		@V_NO_MDOCU		NVARCHAR(20),
		@V_NO_DOCU		NVARCHAR(20),
		@V_TP_DRCR		NVARCHAR(1),
		@V_CD_ACCT		NVARCHAR(10),
		@V_NM_NOTE		NVARCHAR(150),
		@V_AM_DR		NUMERIC(19,4),
        @V_AM_CR        NUMERIC(19,4),
		@V_TP_ACAREA	NVARCHAR(3),
	    @V_CD_RELATION  NCHAR(2),
		@V_CD_BIZAREA   NVARCHAR(7),
		@V_NM_BIZAREA   NVARCHAR(50),
		@V_CD_CC        NVARCHAR(12),
		@V_NM_CC		NVARCHAR(50),
		@V_NO_EMP       NVARCHAR(10),
		@V_NM_EMP       NVARCHAR(50),
		@V_CD_FUND		NVARCHAR(4),
		@V_AM_SUPPLY    NUMERIC(19,4)

SET @V_NO_MDOCU = ('CAR_' + @P_YYYYMM)

SELECT @V_NO_DOCU = MAX(NO_DOCU) 
FROM FI_DOCU
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_MODULE = @P_NO_MODULE
AND NO_MDOCU = @V_NO_MDOCU

IF @V_NO_DOCU IS NOT NULL
BEGIN
	SET @V_ERRMSG = @V_NO_DOCU + ' 로 이미 등록된 전표가 있습니다.'
    GOTO ERROR
END

DECLARE SRC_CURSOR CURSOR FOR

/* 차변 : 국내출장비-교통비 */
SELECT '1' AS TP_DRCR,
	   '55014' AS CD_ACCT,
	   CP.NM_CC + ' ' + RIGHT(@P_YYYYMM, 2) + '월 출장-교통비 정산 (' + CP.CNT + '명)' AS NM_NOTE,
	   ISNULL(CP.AM, 0) AS AM_DR,
	   0 AS AM_CR,
	   '0' AS TP_ACAREA,
	   AC.CD_RELATION,
	   CP.CD_BIZAREA,
	   MB.NM_BIZAREA,
	   CP.CD_CC,
	   CP.NM_CC,
	   NULL AS NO_EMP,
	   NULL AS NM_EMP,
	   NULL AS CD_FUND,
	   0.0 AS AM_SUPPLY
FROM (SELECT CH.CD_COMPANY, ME.CD_CC,
			 MAX(MC.NM_CC) AS NM_CC,
			 MAX(MC.CD_BIZAREA) AS CD_BIZAREA,
			 SUM(CL.AM) AS AM,
			 CONVERT(NVARCHAR, COUNT(1)) AS CNT
	  FROM CZ_HR_CAR_PAYH CH
	  LEFT JOIN (SELECT CD_COMPANY, YM, NO_EMP,
						SUM(AM) AS AM 
				 FROM CZ_HR_CAR_PAYL
				 GROUP BY CD_COMPANY, YM, NO_EMP) CL
	  ON CL.CD_COMPANY = CH.CD_COMPANY AND CL.YM = CH.YM AND CL.NO_EMP = CH.NO_EMP
	  LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = CH.CD_COMPANY AND ME.NO_EMP = CH.NO_EMP
	  LEFT JOIN MA_CC MC ON MC.CD_COMPANY = ME.CD_COMPANY AND MC.CD_CC = ME.CD_CC
	  WHERE CH.CD_COMPANY = @P_CD_COMPANY
	  AND CH.YM = @P_YYYYMM
	  AND CH.YN_PAY = 'Y'
	  GROUP BY CH.CD_COMPANY, ME.CD_CC) CP
LEFT JOIN MA_BIZAREA MB ON MB.CD_COMPANY = CP.CD_COMPANY AND MB.CD_BIZAREA = CP.CD_BIZAREA
LEFT JOIN FI_ACCTCODE AC ON AC.CD_COMPANY = CP.CD_COMPANY AND AC.CD_ACCT = '55014'
UNION ALL
/* 대변 : 미지급 비용 */    
SELECT '2' AS TP_DRCR,
	   '31200' AS CD_ACCT,
	   RIGHT(@P_YYYYMM, 2) + '월 ' + ME.NM_KOR + ' 출장-교통비 정산' AS NM_NOTE,
	   0 AS AM_DR,
	   CP.AM AS AM_CR,
	   '4' AS TP_ACAREA,
	   AC.CD_RELATION,
	   NULL AS CD_BIZAREA,
	   NULL AS NM_BIZAREA,
	   NULL AS CD_CC,
	   NULL AS NM_CC,
	   CP.NO_EMP AS NO_EMP,
	   ME.NM_KOR AS NM_EMP,
	   NULL AS CD_FUND,
	   0.0 AS AM_SUPPLY
FROM (SELECT CH.CD_COMPANY,
		     CH.NO_EMP,
			 SUM(CL.AM) AS AM
	  FROM CZ_HR_CAR_PAYH CH
	  LEFT JOIN (SELECT CD_COMPANY, YM, NO_EMP,
						SUM(AM) AS AM 
				 FROM CZ_HR_CAR_PAYL
				 GROUP BY CD_COMPANY, YM, NO_EMP) CL
	  ON CL.CD_COMPANY = CH.CD_COMPANY AND CL.YM = CH.YM AND CL.NO_EMP = CH.NO_EMP
	  WHERE CH.CD_COMPANY = @P_CD_COMPANY
	  AND CH.YM = @P_YYYYMM
	  AND CH.YN_PAY = 'Y'
	  GROUP BY CH.CD_COMPANY, CH.NO_EMP) CP
LEFT JOIN FI_ACCTCODE AC ON AC.CD_COMPANY = CP.CD_COMPANY AND AC.CD_ACCT = '31200'
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = CP.CD_COMPANY AND ME.NO_EMP = CP.NO_EMP
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

SET @V_NO_DOLINE = 0
SET @V_DT_ACCT = CONVERT(NVARCHAR(8), DATEADD(DAY, -1, DATEADD(MONTH, 1, @P_YYYYMM + '01')), 112)
SET @V_DT_WRITE = CONVERT(NVARCHAR(8), GETDATE(), 112)
SET @V_NM_PUMM = RIGHT(@P_YYYYMM, 2) + '월 출장-교통비 정산'

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
								@V_CD_BIZAREA,
								@V_NM_BIZAREA,
								@V_CD_CC,
								@V_NM_CC,
								@V_NO_EMP,
								@V_NM_EMP,
								@V_CD_FUND,
								@V_AM_SUPPLY

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
						  @V_CD_FUND,			  --자금과목 CD_FUND
						  NULL,					  --원인전표번호 NO_BDOCU
						  0,					  --원인전표라인 NO_BDOLINE
						  '0',					  --타대구분 TP_ETCACCT
						  @V_CD_BIZAREA,		  --귀속사업장 CD_BIZAREA
						  @V_NM_BIZAREA,		  --귀속사업장명 NM_BIZAREA
						  @V_CD_CC,				  --코스트센터 CD_CC
						  @V_NM_CC,				  --코스트센터명 NM_CC
						  NULL,					  --프로젝트 CD_PJT
						  NULL,					  --프로젝트명 NM_PJT
						  NULL,					  --부서 CD_DEPT
						  NULL,					  --부서명 NM_DEPT
						  @V_NO_EMP,			  --사원 CD_EMPLOY
						  @V_NM_EMP,			  --사원명 NM_EMPLOY  
						  NULL,					  --거래처 CD_PARTNER
						  NULL,					  --거래처명 NM_PARTNER
						  NULL,					  --예적금코드 CD_DEPOSIT
						  NULL,					  --예적금명 NM_DEPOSIT
						  NULL,					  --카드번호 CD_CARD
						  NULL,					  --카드명 NM_CARD
						  NULL,					  --은행코드 CD_BANK  : MA_PARTNER에서 FG_PARTNER = '002' 인것
						  NULL,					  --은행명 NM_BANK
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
						  @V_AM_SUPPLY,			  --과세표준 AM_SUPPLY
						  @V_NO_MDOCU,			  --관리번호 = 계산서번호 CD_MNG
						  NULL,					  --수금에정일 DT_START
						  NULL,					  --만기일자 DT_END
						  0,					  --환율 RT_EXCH
						  0,					  --외화금액 AM_EXDO
						  @P_NO_MODULE,			  --모듈구분(D01) NO_MODULE
						  @V_NO_MDOCU,			  --모듈관리번호 = 타모듈pkey NO_MDOCU
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
						  NULL					  --NUMERIC(17,4) = 0		--부가세 (전방추가)

	FETCH NEXT FROM SRC_CURSOR INTO @V_TP_DRCR,
									@V_CD_ACCT,
									@V_NM_NOTE,
									@V_AM_DR,
									@V_AM_CR,
									@V_TP_ACAREA,
									@V_CD_RELATION,
									@V_CD_BIZAREA,
									@V_NM_BIZAREA,
									@V_CD_CC,
									@V_NM_CC,
									@V_NO_EMP,
									@V_NM_EMP,
									@V_CD_FUND,
									@V_AM_SUPPLY
END

CLOSE SRC_CURSOR
DEALLOCATE SRC_CURSOR

RETURN

ERROR:
    ROLLBACK TRAN
    RAISERROR (@V_ERRMSG, 18, 1)
