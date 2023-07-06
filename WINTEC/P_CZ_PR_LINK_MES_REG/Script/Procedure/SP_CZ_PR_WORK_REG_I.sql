USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WORK_REG_I]    Script Date: 2021-11-15 오전 11:38:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_WORK_REG_I]
(
	@P_CD_COMPANY				NVARCHAR(7), 
	@P_NO_WORK					NVARCHAR(20), 
	@P_NO_WO					NVARCHAR(20), 
	@P_CD_PLANT					NVARCHAR(7), 
	@P_CD_OP					NVARCHAR(4), 
	@P_CD_ITEM					NVARCHAR(50), 
	@P_NO_EMP					NVARCHAR(10), 
	@P_DT_WORK					NVARCHAR(8), 
	@P_QT_WORK					NUMERIC(17, 4), 
	@P_QT_REJECT				NUMERIC(17, 4), 
	@P_QT_MOVE					NUMERIC(17, 4), 
	@P_YN_REWORK				NVARCHAR(1), 
	@P_TM_LABOR					NVARCHAR(6), 
	@P_CD_RSRC_LABOR			NVARCHAR(10), 
	@P_TM_MACH					NVARCHAR(6), 
	@P_CD_RSRC_MACH				NVARCHAR(10), 
	@P_CD_WC					NVARCHAR(7), 
	@P_ID_INSERT				NVARCHAR(15), 
	@P_FG_MOVE					NVARCHAR(1), 
	@P_NO_LOT					NVARCHAR(100), 
	@P_NO_SFT					NVARCHAR(3), 
    @P_CD_EQUIP         		NVARCHAR(30),			--2009.08.21 추가
	@P_NO_IO_202_102			NVARCHAR(20),			-- 공정수불번호(이동출고('202')와이동입고('102'))
	@P_NO_IO_203				NVARCHAR(20),			-- 공정수불번호(생산출고('203'))
	@P_NO_IO_LINE_202			NUMERIC(5, 0),          -- 공정수불항번(이동출고('202'))
	@P_NO_IO_LINE_102			NUMERIC(5, 0),          -- 공정수불항번(이동입고('102'))
	@P_NO_IO_LINE_203			NUMERIC(5, 0),          -- 공정수불항번(생산출고('203'))
	@P_CD_SL					NVARCHAR(7), 
    @P_CD_WCOP_SUB      		NVARCHAR(7)     = '', 
    @P_DC_RMK1          		NVARCHAR(100)   = '', 
    @P_DC_RMK2          		NVARCHAR(100)   = '', 
    @P_YN_SUBCON        		NVARCHAR(1)     = 'N',      --외주여부
    @P_NO_OPOUT_PO      		NVARCHAR(20)    = '',      --발주번호
    @P_NO_OPOUT_LINE    		NUMERIC(5, 0)   = 0,       --발주항번
    @P_NO_REL					NVARCHAR(20)    = '',
    @P_QT_RSRC_LABOR			NUMERIC(17, 4)  = 0,		--작업이원 추가 20111213 최인성 정기현 김성호
    @P_NO_WORK_TRACKING			NVARCHAR(20)    = '',
    @P_DC_RMK3					NVARCHAR(100)   = '', --리켐으로 인한추가
    @P_QT_RATE_CALC				NUMERIC(17, 4)  = 0,	--리켐배부율로 사용
    @P_DT_LIMIT					NVARCHAR(8)     = '', --유효일자
    @P_QT_WO					NUMERIC(17, 4)  = 0,
    @P_QT_WO_WORK				NUMERIC(17, 4)  = 0,
    @P_QT_CHCOEF				NUMERIC(17, 6)  = 0,--변환수량
    @P_QT_WORK_CHCOEF			NUMERIC(17, 4)  = 0,--변환실적입력량
    @P_QT_WORK_BAD_CHCOEF		NUMERIC(17, 4)  = 0,--변환실적불량입력량
	@P_CD_MNG1					NVARCHAR(200)   = NULL,
	@P_CD_MNG2					NVARCHAR(200)   = NULL,
	@P_CD_MNG3					NVARCHAR(200)   = NULL,
	@P_CD_MNG4					NVARCHAR(200)   = NULL,
	@P_CD_MNG5					NVARCHAR(200)   = NULL,
	@P_CD_MNG6					NVARCHAR(200)   = NULL,
	@P_CD_MNG7					NVARCHAR(200)   = NULL,
	@P_CD_MNG8					NVARCHAR(200)   = NULL,
	@P_CD_MNG9					NVARCHAR(200)   = NULL,
	@P_CD_MNG10					NVARCHAR(200)   = NULL,
	@P_CD_MNG11					NVARCHAR(200)   = NULL,
	@P_CD_MNG12					NVARCHAR(200)   = NULL,
	@P_CD_MNG13					NVARCHAR(200)   = NULL,
	@P_CD_MNG14					NVARCHAR(200)   = NULL,
	@P_CD_MNG15					NVARCHAR(200)   = NULL,
	@P_CD_MNG16					NVARCHAR(200)   = NULL,
	@P_CD_MNG17					NVARCHAR(200)   = NULL,
	@P_CD_MNG18					NVARCHAR(200)   = NULL,
	@P_CD_MNG19					NVARCHAR(200)   = NULL,
	@P_CD_MNG20					NVARCHAR(200)   = NULL,
	@P_CD_POST					NVARCHAR(30)    = NULL,
	@P_CD_USERDEF1				NVARCHAR(20)    = NULL,
	@P_CD_DEPT_WORK				NVARCHAR(12)    = NULL,
	@P_TXT_USERDEF1				NVARCHAR(200)	= NULL,
	@P_TXT_USERDEF2				NVARCHAR(200)	= NULL,
	@P_TXT_USERDEF3				NVARCHAR(200)	= NULL,
	@P_CD_USERDEF2				NVARCHAR(4)		= NULL,
	@P_CD_USERDEF3				NVARCHAR(4)		= NULL,
	@P_NUM_USERDEF1				NUMERIC(17, 4)	= 0,
	@P_NUM_USERDEF2				NUMERIC(17, 4)	= 0,
	@P_NUM_USERDEF3				NUMERIC(17, 4)	= 0,
	@P_NUM_USERDEF4				NUMERIC(17, 4)	= 0,
	@P_NUM_USERDEF5				NUMERIC(17, 4)	= 0,
	@P_NUM_USERDEF6				NUMERIC(17, 4)	= 0,
	@P_NUM_USERDEF7				NUMERIC(17, 4)	= 0,
	@P_NUM_USERDEF8				NUMERIC(17, 4)	= 0,
	@P_NUM_USERDEF9				NUMERIC(17, 4)	= 0,
	@P_NUM_USERDEF10			NUMERIC(17, 4)	= 0
)
AS

BEGIN
 
DECLARE
@P_DAY					NVARCHAR(6),			-- 채번할때필요한자리(년도/월) 날짜
@ERRMSG					VARCHAR(255),			-- ERROR 메시지

@TP_GR					NVARCHAR(3),            -- 작업지시번호의수불형태
@P_CD_DEPT				NVARCHAR(12),           -- 담당자의부서

@P_FG_IO				NVARCHAR(3),	
@P_BEFORE_CD_OP			NVARCHAR(8),			-- 이전공정
@P_NEXT_CD_OP			NVARCHAR(8),			-- 다음공정
@P_LAST_CD_OP			NVARCHAR(8),			-- 마지막공정
@P_FIRST_CD_OP			NVARCHAR(8),			-- 첫공정
@P_ROUTCOUNT			INT,					-- 현작업지시(PR_WO)의공정의갯수

@NO_IO_103				NVARCHAR(20),			-- 공정수불번호(생산입고('103'))
@NO_IO_LINE_103			NUMERIC(5, 0),			-- 공정수불항번(생산입고('103'))
@P_CD_WC_NEXT_CD_OP 	NVARCHAR(7),			-- 실적발생한공정의다음공정의작업장(이동출고의관련작업장(CD_WC_TO))과실적발생한현재공정의작업장(이동입고의주작업장(CD_WC_FR))에들어가야함

@YN_AUTORCV				NVARCHAR(1),        --◈ 작업지시번호의 자동입고처리여부(작업지시등록시 ◈공장환경설정에서 가져온다.)
@YN_AUTORCV_REQ			NVARCHAR(1),        --★ 생산품자동입고의뢰처리 //i01
@NO_IO_MM				NVARCHAR(20),        --   자재의수불번호
@NO_RCV					NVARCHAR(20),        -- 입고의뢰번호
@YN_LOT					NVARCHAR(1),

@CD_PJT         		NVARCHAR(20),
@P_YN_QC				NVARCHAR(1),
@P_FG_OPQC				NVARCHAR(3),
@P_YN_AUTO_CLS			NVARCHAR(1),
@P_YN_QT_WORK			NVARCHAR(1),
@V_QC_YN				NVARCHAR(4),--품질사용여부
@V_FG_PQC				NVARCHAR(1),--MA_PITEM의 생산입고검사여부 Y,N	
@V_YN_UNIT				NVARCHAR(1),
@V_SEQ_PROJECT			NUMERIC(5, 0),
@V_YN_INSPECT			NVARCHAR(1), --통제와 품목검사타는 것을 보고 넣어주도록함,
@V_CD_PARTNER			NVARCHAR(20),--작업시지라우팅의 QC검사가 Y이면서 공장품목등록의 공정검사가 Y일 경우 MM_QC테이블에 거래처를 넣어준다.
@V_SERVER_KEY			NVARCHAR(25),
@P_MA_EXC_QC			NVARCHAR(3),
@P_MA_EXC_AUTO_CLS		NVARCHAR(3),
@V_QT_REMAIN			NUMERIC(17, 4),
@V_QT_START				NUMERIC(17, 4)

SELECT @V_SERVER_KEY = (SELECT MAX(SERVER_KEY) FROM CM_SERVER_CONFIG WHERE YN_UPGRADE = 'Y')

SELECT @V_SEQ_PROJECT = 0
SELECT @V_YN_INSPECT = 'N'

IF(@P_DT_WORK IS NOT NULL AND @P_DT_WORK != '')
	SET @P_DAY = LEFT(@P_DT_WORK, 6)
ELSE
	SET @P_DAY = CONVERT(NVARCHAR(6), GETDATE(), 112)
	

SELECT @P_CD_DEPT = ISNULL(( SELECT	CD_DEPT
                               FROM	MA_EMP
                              WHERE	CD_COMPANY = @P_CD_COMPANY
                                AND	NO_EMP = @P_NO_EMP), '')
                                
                                
SELECT	@P_YN_AUTO_CLS = ISNULL(YN_AUTO_CLS, 'N'),	-- 생산수량자동마감
		@P_YN_QT_WORK  = ISNULL(YN_QT_WORK, 'N')	-- 지시수량 대비 실적수량 초과
FROM	PR_CFG_PLANT
WHERE	CD_COMPANY = @P_CD_COMPANY
AND		CD_PLANT   = @P_CD_PLANT	


SELECT	@P_CD_SL   = CASE WHEN ISNULL(@P_CD_SL,'') = '' THEN ISNULL(CD_SL,'') ELSE @P_CD_SL END,
		@P_FG_OPQC = ISNULL(FG_OPQC,'N'),	--공정검사
		@V_FG_PQC  = ISNULL(FG_PQC,'N'),	--생산입고검사
		@YN_LOT	   = CASE WHEN ISNULL(FG_SERNO,'') = '002' THEN 'Y' ELSE 'N' END
FROM	MA_PITEM
WHERE	CD_COMPANY = @P_CD_COMPANY
AND		CD_PLANT   = @P_CD_PLANT
AND		CD_ITEM    = @P_CD_ITEM

IF (@V_SERVER_KEY LIKE 'SWPNC%') SET @YN_LOT = 'Y'

--◈ 작업지시번호의 '자동입고처리' 여부(작업지시등록시 ◈'공장환경설정' 에서 가져온다.)
SELECT	@YN_AUTORCV		= YN_AUTORCV, 
		@YN_AUTORCV_REQ = YN_AUTORCV_REQ,	--★ 생산품자동입고의뢰처리 //i01 
        @CD_PJT			= NO_PJT,
        @V_SEQ_PROJECT	= SEQ_PROJECT,
        @TP_GR			= ISNULL(TP_GR, '')
FROM	PR_WO
WHERE	CD_COMPANY = @P_CD_COMPANY
AND		CD_PLANT   = @P_CD_PLANT
AND		NO_WO      = @P_NO_WO           


SELECT @P_YN_QC    = ISNULL(YN_QC,'N'),
	   @V_QT_REMAIN = ISNULL(QT_START,0) - ISNULL(QT_WORK,0),
	   @V_QT_START = ISNULL(QT_START,0)
  FROM PR_WO_ROUT WITH(NOLOCK)
 WHERE CD_COMPANY = @P_CD_COMPANY
   AND CD_PLANT   = @P_CD_PLANT
   AND NO_WO      = @P_NO_WO
   AND CD_OP      = @P_CD_OP
   
   
SELECT @V_QC_YN = CD_EXC --'000', 'QU', '000 : 사용안함(기본), 100 : 수불전검사, 200 : 수불후검사'
  FROM MA_EXC
 WHERE CD_COMPANY = @P_CD_COMPANY
   AND EXC_TITLE ='생산입고등록_검사'


SELECT	@P_MA_EXC_QC = ISNULL((SELECT CD_EXC FROM MA_EXC WHERE	CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '검사처리등록_수불생성기준'), '000')

SELECT	@P_MA_EXC_AUTO_CLS = ISNULL((SELECT CD_EXC FROM MA_EXC WHERE	CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '작업실적등록_생산수량자동마감기준'), '000')

IF (@P_YN_QT_WORK = 'N' AND ISNULL(@P_QT_WORK, 0) > ISNULL(@V_QT_REMAIN, 0))
BEGIN
	SELECT @ERRMSG = '실적입력량이 작업잔량보다 많습니다.'
	GOTO ERROR 
END

IF (@V_QT_START <= 0)
BEGIN
	IF (@V_SERVER_KEY LIKE 'WISE%')
	BEGIN
		PRINT '실적초과옵션사용시 이전공정의 실적이 없어도 실적이 입력가능하도록'
	END
	ELSE 
	BEGIN
		SELECT @ERRMSG = '시작수량(QT_START)이 없습니다. 재조회후 다시 입력하세요.'
		GOTO ERROR 
	END
END

---------------------------------------------------------------------------------------

SET @P_QT_MOVE = ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0)    --이동수량 = 실적수량 - 불량수량

---------------------------------------------------------------------------------------
--작업시지라우팅의 QC검사가 Y이면서 공장품목등록의 공정검사가 Y일 경우 MM_QC테이블에 거래처를 넣어준다.
SELECT @V_CD_PARTNER = (SELECT CD_PARTNER FROM PR_OPOUT_POH WHERE CD_COMPANY = @P_CD_COMPANY AND CD_PLANT = @P_CD_PLANT AND NO_PO = @P_NO_OPOUT_PO)
---------------------------------------------------------------------------------------
--공정외주인 경우 외화단가, 외화금액, 원화단가, 원화금액, 부가세, 원화금액합계를 넣어줘야한다.
--그래서 여기서 공정외주발주에서 구해오는 구문을 넣었다.
--외주공정일 경우 공장환경설정의 공정외주자동발주 'Y' 인경우 공정외주발주등록 존재 여부 (존재하지 않으면 공정외주발주등록을 한다.)

IF (@P_YN_SUBCON = 'Y')
BEGIN
	IF NOT EXISTS (	SELECT	'X'
					FROM	PR_OPOUT_POL L WITH(NOLOCK)
					JOIN	PR_OPOUT_POH H ON H.CD_COMPANY = @P_CD_COMPANY AND H.CD_PLANT = @P_CD_PLANT AND H.NO_PO = L.NO_PO
					WHERE	L.CD_COMPANY = @P_CD_COMPANY
					AND		L.CD_PLANT = @P_CD_PLANT
					AND		L.NO_PO = @P_NO_OPOUT_PO
					AND		L.NO_LINE = @P_NO_OPOUT_LINE
					)
	BEGIN  
		------------------------------------------------------------------------
		DECLARE	@P_CD_PARTNER	NVARCHAR(20),	
				@P_UM			NUMERIC(19, 6), 
				@P_AM			NUMERIC(17, 4), 
				@P_AM_VAT		NUMERIC(17, 4), 
				@P_AM_SUM		NUMERIC(17, 4), 
				@P_AM_VAT_SUM	NUMERIC(17, 4)
		
		------------------------------------------------------------------------
		SELECT	@P_CD_PARTNER = WCOP.CD_PARTNER, @P_UM = SM.UM
		FROM	PR_WCOP WCOP WITH(NOLOCK)
		LEFT	JOIN SU_UM_OP SM ON SM.CD_COMPANY = WCOP.CD_COMPANY 
								AND SM.CD_PLANT = WCOP.CD_PLANT 
								AND SM.CD_WC = WCOP.CD_WC 
								AND SM.CD_WCOP = WCOP.CD_WCOP 
								AND SM.CD_PARTNER = WCOP.CD_PARTNER 
								AND SM.CD_ITEM = @P_CD_ITEM 
								AND SM.CD_EXCH = '000' 
								AND SM.DT_START >= @P_DT_WORK
								AND SM.DT_END <= @P_DT_WORK
		WHERE	WCOP.CD_COMPANY = @P_CD_COMPANY
		AND		WCOP.CD_PLANT = @P_CD_PLANT
		AND		WCOP.CD_WC = @P_CD_WC
		AND		WCOP.CD_WCOP = @P_CD_WCOP_SUB
		
		------------------------------------------------------------------------
		SELECT	@P_NO_OPOUT_LINE = NO_LINE 
		FROM	PR_WO_ROUT WITH(NOLOCK)
		WHERE	CD_COMPANY = @P_CD_COMPANY
		AND		CD_PLANT = @P_CD_PLANT
		AND		NO_WO = @P_NO_WO
		AND		CD_OP = @P_CD_OP
		AND		CD_WC = @P_CD_WC
		AND		CD_WCOP = @P_CD_WCOP_SUB
		------------------------------------------------------------------------
		SELECT	@P_AM = @P_QT_MOVE * @P_UM	--라인 금액
		SELECT	@P_AM_VAT = @P_AM * 10		--라인 부가세
		SELECT	@P_AM_SUM = SUM(AM), 
				@P_AM_VAT_SUM = SUM(AM_VAT)
		FROM	PR_OPOUT_POL WITH(NOLOCK)
		WHERE	CD_COMPANY = @P_CD_COMPANY 
		AND		CD_PLANT = @P_CD_PLANT 
		AND		NO_PO = @P_NO_OPOUT_PO
		------------------------------------------------------------------------
		--공정외주발주번호채번

		EXEC CP_GETNO @P_CD_COMPANY= @P_CD_COMPANY, @P_CD_MODULE= 'PR', @P_CD_CLASS = '30', @P_DOCU_YM = @P_DT_WORK, @P_NO = @P_NO_OPOUT_PO OUTPUT
		
		IF (@@ERROR <> 0 )
		BEGIN 
			SELECT @ERRMSG = '실적 저장시 공정외주발주번호 작업을정상적으로처리하지못했습니다.' 
			GOTO ERROR 
		END
		
		------------------------------------------------------------------------
		EXEC	UP_PR_OPOUT_POH_INSERT	@P_CD_COMPANY,	@P_CD_PLANT,	@P_NO_OPOUT_PO,		@P_CD_PARTNER,	@P_NO_EMP, 
										@P_DT_WORK,		'000',			1,					'21',			10, 
										@P_AM_SUM,		@P_AM_SUM,		@P_AM_VAT_SUM,		@P_DC_RMK1,		@P_ID_INSERT
		IF (@@ERROR <> 0 )
		BEGIN 
			SELECT @ERRMSG = '실적 저장시 공정외주발주등록 헤더 작업을정상적으로처리하지못했습니다.' 
			GOTO ERROR 
		END
						
		------------------------------------------------------------------------ 
		EXEC	UP_PR_OPOUT_POL_INSERT	@P_CD_COMPANY,	@P_CD_PLANT,	@P_NO_OPOUT_PO,		@P_NO_OPOUT_LINE,	@P_NO_WO,
										@P_CD_OP,	    @P_CD_WC,	    @P_CD_WCOP_SUB,		@P_CD_ITEM,			@P_DT_WORK,
										@P_QT_WORK,	    0,				0,					@P_UM,				@P_AM,
										@P_UM,		    @P_AM,		    @P_AM_VAT,			@P_DC_RMK1,			@P_ID_INSERT, 
										0,				0
		IF (@@ERROR <> 0 )
		BEGIN 
			SELECT @ERRMSG = '실적 저장시 공정외주발주등록 라인 작업을정상적으로처리하지못했습니다.' 
			GOTO ERROR 
		END  
	END
	---------------------------------------------------------------------------------------------------------------------------
END
-----------------------------------------------------------

EXEC UP_PR_WORK_INSERT	@P_CD_COMPANY = @P_CD_COMPANY, @P_NO_WORK = @P_NO_WORK, 
						@P_NO_WO = @P_NO_WO, @P_CD_PLANT = @P_CD_PLANT, @P_CD_OP = @P_CD_OP, 
						@P_CD_ITEM = @P_CD_ITEM, @P_DT_WORK = @P_DT_WORK, @P_QT_WORK = @P_QT_WORK, 
						@P_QT_REJECT = @P_QT_REJECT,@P_YN_REWORK = @P_YN_REWORK, 
						@P_YN_BADWORK = 'N', 
						@P_TM_LABOR = @P_TM_LABOR, @P_CD_RSRC_LABOR = @P_CD_RSRC_LABOR,
						@P_TM_MACH = @P_TM_MACH, @P_CD_RSRC_MACH = @P_CD_RSRC_MACH, @P_NO_EMP = @P_NO_EMP, 
						@P_CD_WC = @P_CD_WC, @P_ID_INSERT = @P_ID_INSERT, @P_QT_MOVE = @P_QT_MOVE, 
						@P_NO_LOT = @P_NO_LOT, @P_NO_SFT = @P_NO_SFT, @P_CD_EQUIP = @P_CD_EQUIP, 
						@P_YN_SUBCON = @P_YN_SUBCON, 
						@P_NO_OPOUT_PO = @P_NO_OPOUT_PO, @P_NO_OPOUT_PO_LINE = @P_NO_OPOUT_LINE, 
						@P_CD_WCOP_SUB = @P_CD_WCOP_SUB, 
						@P_DC_RMK1 = @P_DC_RMK1, @P_DC_RMK2 = @P_DC_RMK2,
						@P_NO_REL = @P_NO_REL,
						@P_QT_RSRC_LABOR = @P_QT_RSRC_LABOR,
						@P_NO_WORK_TRACKING = @P_NO_WORK_TRACKING,
						@P_DC_RMK3 = @P_DC_RMK3,
						@P_QT_RATE_CALC = @P_QT_RATE_CALC,
						@P_DT_LIMIT = @P_DT_LIMIT,
						@P_QT_CHCOEF = @P_QT_CHCOEF,
						@P_QT_WORK_CHCOEF = @P_QT_WORK_CHCOEF,
						@P_QT_WORK_BAD_CHCOEF = @P_QT_WORK_BAD_CHCOEF,
						@P_CD_POST = @P_CD_POST,
						@P_CD_USERDEF1 = @P_CD_USERDEF1,
						@P_CD_DEPT_WORK = @P_CD_DEPT_WORK,
					    @P_TXT_USERDEF1	 = @P_TXT_USERDEF1,
					    @P_TXT_USERDEF2	 = @P_TXT_USERDEF2,
					    @P_TXT_USERDEF3	 = @P_TXT_USERDEF3,
					    @P_CD_USERDEF2	 = @P_CD_USERDEF2,
					    @P_CD_USERDEF3	 = @P_CD_USERDEF3,
					    @P_NUM_USERDEF1	 = @P_NUM_USERDEF1,
					    @P_NUM_USERDEF2	 = @P_NUM_USERDEF2,
					    @P_NUM_USERDEF3	 = @P_NUM_USERDEF3,
					    @P_NUM_USERDEF4	 = @P_NUM_USERDEF4,
					    @P_NUM_USERDEF5	 = @P_NUM_USERDEF5,
					    @P_NUM_USERDEF6	 = @P_NUM_USERDEF6,
					    @P_NUM_USERDEF7	 = @P_NUM_USERDEF7,
					    @P_NUM_USERDEF8	 = @P_NUM_USERDEF8,
					    @P_NUM_USERDEF9	 = @P_NUM_USERDEF9,
					    @P_NUM_USERDEF10 = @P_NUM_USERDEF10

IF (@@ERROR <> 0) 
BEGIN 
	SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.a' 
	GOTO ERROR 
END


/* 공정이동등록*/
IF (@P_FG_MOVE = 'Y')
BEGIN
	----이전공정을구한다.
	SELECT @P_BEFORE_CD_OP = MAX(CD_OP)
	FROM PR_WO_ROUT WITH(NOLOCK)
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO
		AND CD_OP < @P_CD_OP

	--다음공정을구한다.
	SELECT @P_NEXT_CD_OP = MIN(CD_OP)
	FROM PR_WO_ROUT WITH(NOLOCK)
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO
		AND CD_OP > @P_CD_OP

	--마지막공정을구한다.
	SELECT @P_LAST_CD_OP = MAX(CD_OP)
	FROM PR_WO_ROUT WITH(NOLOCK)
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO

	--첫번째공정을구한다.
	SELECT @P_FIRST_CD_OP = MIN(CD_OP) 
	FROM PR_WO_ROUT WITH(NOLOCK)
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO

	--공정의갯수를구한다.(공정이하나인경우를위해서)        --> 공정이하나인경우는첫번째공정이마지막공정이된다.
	SELECT @P_ROUTCOUNT = COUNT(1)
	FROM PR_WO_ROUT WITH(NOLOCK)
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO

	--초기공정작업시작시PR_WO.ST_WO 상태값'S'로변경
	IF (@P_FIRST_CD_OP = @P_CD_OP)
	BEGIN
		UPDATE PR_WO
		SET ST_WO = 'S'
		WHERE CD_COMPANY = @P_CD_COMPANY
			 AND CD_PLANT = @P_CD_PLANT
				AND NO_WO = @P_NO_WO
				AND ST_WO = 'R'
	END

	--마지막공정이아닌경우
	IF (@P_CD_OP <> @P_LAST_CD_OP OR @P_ROUTCOUNT = 1)
	BEGIN
		IF (@P_QT_MOVE > 0)        --이동수량이보다크면이동출고('202'), 이동입고('102') 공정수불전표발생
		BEGIN
				IF (@P_ROUTCOUNT > 1)        --공정의수가개이상일때만이구문을실행한다.
				BEGIN
						-- 이동출고전표(FG_SLIP = '202')의관련작업장(CD_WC_TO)과이동입고전표(FG_SLIP = '102')의주작업장(CD_WC_FR)에들어가야함.
						SELECT @P_CD_WC_NEXT_CD_OP = CD_WC
						FROM PR_WO_ROUT WITH(NOLOCK)
						WHERE CD_COMPANY = @P_CD_COMPANY
								AND CD_PLANT = @P_CD_PLANT
								AND NO_WO = @P_NO_WO
								AND CD_OP = @P_NEXT_CD_OP        --현재공정이마지막공정이아니으로다음공정은존재한다고가정.

						--이동출고전표생성
						--EXEC CP_GETNO @P_CD_COMPANY, 'PR', '06', @P_DAY, @P_NO_IO_202_102 OUTPUT

						--IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

						EXEC UP_PR_QTIOH_INSERT @P_CD_COMPANY = @P_CD_COMPANY,	@P_CD_PLANT = @P_CD_PLANT,	@P_NO_IO = @P_NO_IO_202_102, 
												@P_FG_SLIP = N'202',			@P_TP_SLIP = N'1',			@P_YN_WIP = N'Y', 
												@P_DT_IO = @P_DT_WORK,			@P_CD_DEPT = @P_CD_DEPT,	@P_CD_WC = @P_CD_WC, 
												@P_NO_EMP = @P_NO_EMP,			@P_ID_INSERT = @P_ID_INSERT
						IF (@@ERROR <> 0) 
						BEGIN	
							SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
							GOTO ERROR 
						END

						--이동출고전표의항번생성
						--SET @P_NO_IO_LINE_202 = 1        --방금채번한공정수불번호의항번은그수불의MAX 값을구하지않고도초기값으로가정...

						EXEC UP_PR_QTIO_INSERT	@P_CD_COMPANY = @P_CD_COMPANY,		@P_CD_PLANT = @P_CD_PLANT,	@P_NO_IO = @P_NO_IO_202_102, 
												@P_NO_LINE_IO = @P_NO_IO_LINE_202,	@P_FG_SLIP = N'202',		@P_TP_SLIP = N'1', 
												@P_YN_WIP = N'Y',					@P_CD_ITEM = @P_CD_ITEM,	@P_CD_WC_FR = @P_CD_WC, 
												@P_CD_WC_TO = @P_CD_WC_NEXT_CD_OP,	@P_DT_IO = @P_DT_WORK,		@P_QT_IO = @P_QT_MOVE, 
												@P_QT_PROC = @P_QT_MOVE,			@P_NO_WO = @P_NO_WO,		@P_NO_SRC = @P_NO_WORK, 
												@P_CD_OP = @P_CD_OP,				@P_ID_INSERT = @P_ID_INSERT
						IF (@@ERROR <> 0) 
						BEGIN 
							SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
							GOTO ERROR 
						END

						--이동입고전표의항번생성
						--SET @P_NO_IO_LINE_102 = @P_NO_IO_LINE_202 + 1

						EXEC UP_PR_QTIO_INSERT	@P_CD_COMPANY = @P_CD_COMPANY,		@P_CD_PLANT = @P_CD_PLANT,	@P_NO_IO = @P_NO_IO_202_102, 
												@P_NO_LINE_IO = @P_NO_IO_LINE_102,	@P_FG_SLIP = N'102',		@P_TP_SLIP = N'1', 
												@P_YN_WIP = N'Y',					@P_CD_ITEM = @P_CD_ITEM,	@P_CD_WC_FR = @P_CD_WC_NEXT_CD_OP, 
												@P_CD_WC_TO = @P_CD_WC,				@P_DT_IO = @P_DT_WORK,		@P_QT_IO = @P_QT_MOVE, 
												@P_QT_PROC = @P_QT_MOVE,			@P_NO_WO = @P_NO_WO,		@P_NO_SRC = @P_NO_WORK, 
												@P_CD_OP = @P_CD_OP,				@P_ID_INSERT = @P_ID_INSERT

						IF (@@ERROR <> 0) 
						BEGIN 
							SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
							GOTO ERROR 
						END
						
						IF (@P_YN_QC = 'Y' AND @P_FG_OPQC = 'Y' AND @P_MA_EXC_QC = '000')
						BEGIN 
							EXEC UP_MM_QC_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_NO_REQ = @P_NO_WORK, @P_DT_REQ = @P_DT_WORK, @P_CD_PARTNER = @V_CD_PARTNER, @P_NO_EMP = @P_NO_EMP, @P_FG_IO = '998' 
							
							IF (@@ERROR <> 0) 
							BEGIN 
								SELECT @ERRMSG = '[UP_MM_QC_INSERT]작업을정상적으로처리하지못했습니다.' 
								GOTO ERROR 
							END
							
							EXEC UP_MM_QCL_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_NO_REQ = @P_NO_WORK, @P_NO_LINE = 1, @P_CD_PLANT = @P_CD_PLANT, @P_CD_ITEM = @P_CD_ITEM, @P_QT_REQ = @P_QT_WORK, @P_DC_RMK = ''
							
							IF (@@ERROR <> 0) 
							BEGIN 
								SELECT @ERRMSG = '[UP_MM_QCL_INSERT]작업을정상적으로처리하지못했습니다.' 
								GOTO ERROR 
							END						
						END
				END
				---------------------------------------------------------------------------------------------------
				----공정수불LOT(PR_QTIOLOT) 생성구문시작(첫번째공정에서만생성한다.)                                                        ---
				---------------------------------------------------------------------------------------------------

				--현재공정이첫번재공정이면이전에생성시키지않았던PR_QTIOLOT을생산입고전표(FG_SLIP = '103')로생성한다.
				--이때LOT번호가없으면(이때는PR_QTIOLOT를생성시키는프로시져를화면에서직접호출) 생성시키지않는다.
				
                IF (@P_CD_OP = @P_FIRST_CD_OP AND ISNULL(@P_NO_LOT, '') <> '' AND @YN_LOT = 'Y')
				BEGIN
						-- PR_QTIOLOT의이동출고전표를생선한다.(FG_SLIP = '202')
						INSERT PR_QTIOLOT(	CD_COMPANY,		CD_PLANT,		NO_IO,				NO_LINE_IO,			FG_SLIP, 
											NO_LOT,			CD_ITEM,		DT_IO,				CD_SL,				QT_IO, 
											QT_MGMT,		YN_RETURN,		NO_WO,				NO_WORK,			CD_OP, 
											CD_WC,			CD_WCOP,		NO_IO_MGMT,			NO_LINE_IO_MGMT,	FG_SLIP_MGMT, 
											NO_LOT_MGMT,	NO_IO_SC,		NO_LINE_IO_SC,		FG_SLIP_SC,			NO_LOT_SC, 
											NO_IO_MM,		NO_IOLINE_MM,	NO_LOT_MM,			ID_INSERT,			DTS_INSERT, NO_SRC,
											DT_LIMIT )
									SELECT	@P_CD_COMPANY,	@P_CD_PLANT,	NO_IO,				NO_LINE_IO,			'202', 
											@P_NO_LOT,		CD_ITEM,		DT_IO,				NULL,				QT_IO, 
											0,				'N',			NO_WO,				@P_NO_WORK,			CD_OP, 
											@P_CD_WC,		CD_WCOP,		@NO_IO_103,			@NO_IO_LINE_103,	'103', 
											@P_NO_LOT,		@NO_IO_103,		@NO_IO_LINE_103,	'103',				@P_NO_LOT, 
											NULL,			0,				NULL,				ID_INSERT,			DTS_INSERT, @P_NO_WORK,
											@P_DT_LIMIT
									FROM	PR_QTIO
									WHERE	CD_COMPANY = @P_CD_COMPANY
									AND		CD_PLANT = @P_CD_PLANT
									AND		NO_IO = @P_NO_IO_202_102
									AND		NO_LINE_IO = @P_NO_IO_LINE_202
									AND		FG_SLIP = '202'

						IF (@@ERROR <> 0) 
						BEGIN 
							SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
							GOTO ERROR 
						END

						-- PR_QTIOLOT의이동입고전표를생선한다.(FG_SLIP = '102')
						INSERT PR_QTIOLOT(	CD_COMPANY,		CD_PLANT,		NO_IO,				NO_LINE_IO,			FG_SLIP, 
											NO_LOT,			CD_ITEM,		DT_IO,				CD_SL,				QT_IO, 
											QT_MGMT,		YN_RETURN,		NO_WO,				NO_WORK,			CD_OP, 
											CD_WC,			CD_WCOP,		NO_IO_MGMT,			NO_LINE_IO_MGMT,	FG_SLIP_MGMT, 
											NO_LOT_MGMT,	NO_IO_SC,		NO_LINE_IO_SC,		FG_SLIP_SC,			NO_LOT_SC, 
											NO_IO_MM,		NO_IOLINE_MM,	NO_LOT_MM,			ID_INSERT,			DTS_INSERT, NO_SRC,
											DT_LIMIT )
									SELECT	@P_CD_COMPANY,	@P_CD_PLANT,	NO_IO,				NO_LINE_IO,			'102', 
											@P_NO_LOT,		CD_ITEM,		DT_IO,				NULL,				QT_IO, 
											0,				'N',			NO_WO,				@P_NO_WORK,			CD_OP, 
											@P_CD_WC,		CD_WCOP,		@P_NO_IO_202_102,	@P_NO_IO_LINE_202,	'202', 
											@P_NO_LOT,		@NO_IO_103,		@NO_IO_LINE_103,	'103',				@P_NO_LOT, 
											NULL,			0,				NULL,				ID_INSERT,			DTS_INSERT, @P_NO_WORK,
											@P_DT_LIMIT
									FROM	PR_QTIO
									WHERE	CD_COMPANY = @P_CD_COMPANY
									AND		CD_PLANT = @P_CD_PLANT
									AND		NO_IO = @P_NO_IO_202_102
									AND		NO_LINE_IO = @P_NO_IO_LINE_102
									AND		FG_SLIP = '102'

						IF (@@ERROR <> 0) 
						BEGIN	
							SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
							GOTO ERROR 
						END
				END

				---------------------------------------------------------------------------------------------------
				----공정수불LOT(PR_QTIOLOT) 생성구문끝                                                                                                ---
				---------------------------------------------------------------------------------------------------
		END

		IF (@P_ROUTCOUNT > 1)        --공정의수가개이상일때만이구문을실행한다.
		BEGIN
				--작업지시공정경로(PR_WO_ROUT) 현재공정(@P_CD_OP)의재공수량(QT_WIP), 작업수량(QT_WORK), 이동수량(QT_MOVE), 상태값(ST_OP) UPDATE
				UPDATE	PR_WO_ROUT
				SET		QT_WIP = A.QT_START - (ISNULL(A.QT_WORK, 0) + @P_QT_WORK) + (ISNULL(A.QT_REJECT, 0) + @P_QT_REJECT) - A.QT_REWORK - ISNULL(A.QT_BAD, 0),
						QT_WORK = ISNULL(QT_WORK,0) + ISNULL(@P_QT_WORK, 0),
						QT_MOVE = ISNULL(QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0),
						ST_OP = 'S'--CASE WHEN A.QT_WO > ( ISNULL(QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0) ) THEN 'S' ELSE CASE WHEN @P_YN_AUTO_CLS = 'Y' THEN 'C' ELSE 'S' END END
				FROM	PR_WO_ROUT A
				WHERE	A.CD_COMPANY = @P_CD_COMPANY
				AND		CD_PLANT = @P_CD_PLANT
				AND		A.NO_WO = @P_NO_WO
				AND		A.CD_OP = @P_CD_OP
				
				------------------------------------------------------------------------------------------------------------------------------------------
				--작업지시분할
				------------------------------------------------------------------------------------------------------------------------------------------
				UPDATE PR_WO_ROUT_REL
				SET		QT_WIP = A.QT_REL - (ISNULL(A.QT_WORK, 0) + @P_QT_WORK) + (ISNULL(A.QT_REJECT, 0) + @P_QT_REJECT) - A.QT_REWORK - ISNULL(A.QT_BAD, 0),
						QT_WORK = ISNULL(QT_WORK,0) + ISNULL(@P_QT_WORK, 0),
						QT_MOVE = ISNULL(QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0),
						ST_OP = 'S',--CASE WHEN A.QT_REL > ( ISNULL(QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0) ) THEN 'S' ELSE CASE WHEN @P_YN_AUTO_CLS = 'Y' THEN 'C' ELSE 'S' END END,
						YN_CLOSE = 'N'--CASE WHEN A.QT_REL > ( ISNULL(QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0) ) THEN 'N' ELSE 'Y' END
				FROM	PR_WO_ROUT_REL A
				WHERE	A.CD_COMPANY = @P_CD_COMPANY
				AND		CD_PLANT = @P_CD_PLANT
				AND		A.NO_WO = @P_NO_WO
				AND		A.CD_OP = @P_CD_OP
				AND		A.NO_REL = @P_NO_REL
				
				------------------------------------------------------------------------------------------------------------------------------------------
				--작업지시분할
				------------------------------------------------------------------------------------------------------------------------------------------
				

				IF (@@ERROR <> 0) 
				BEGIN 
					SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR 
				END

				--작업지시공정경로(PR_WO_ROUT) 다음공정(@P_NEXT_CD_OP)의재공수량(QT_WIP), 시작수량(QT_START) UPDATE
				UPDATE	PR_WO_ROUT
				SET		QT_START = ISNULL(QT_START, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0), -- + ISNULL(@P_QT_REWORK, 0)
						QT_WIP = ISNULL(QT_WIP, 0) +  ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0)-- + ISNULL(@P_QT_REWORK, 0)
				WHERE	CD_COMPANY = @P_CD_COMPANY
				AND		CD_PLANT = @P_CD_PLANT
				AND		NO_WO = @P_NO_WO
				AND		CD_OP = @P_NEXT_CD_OP
				
				------------------------------------------------------------------------------------------------------------------------------------------
				--작업지시분할
				------------------------------------------------------------------------------------------------------------------------------------------
				
				UPDATE	PR_WO_ROUT_REL
				SET		QT_START = ISNULL(QT_START, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0), -- + ISNULL(@P_QT_REWORK, 0)
						QT_WIP = ISNULL(QT_WIP, 0) +  ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0)-- + ISNULL(@P_QT_REWORK, 0)
				WHERE	CD_COMPANY = @P_CD_COMPANY
				AND		CD_PLANT = @P_CD_PLANT
				AND		NO_WO = @P_NO_WO
				AND		CD_OP = @P_NEXT_CD_OP
				AND		NO_REL = @P_NO_REL
				
				------------------------------------------------------------------------------------------------------------------------------------------
				--작업지시분할
				------------------------------------------------------------------------------------------------------------------------------------------

				IF (@@ERROR <> 0) 
				BEGIN 
					SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR 
				END
		END
	END

	IF (@P_CD_OP = @P_LAST_CD_OP OR @P_ROUTCOUNT = 1)                --마지막공정이거나공정이1개인경우
	BEGIN
		--생산출고전표생성
		--EXEC CP_GETNO @P_CD_COMPANY, 'PR', '13', @P_DAY, @P_NO_IO_203 OUTPUT

		--IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

		EXEC UP_PR_QTIOH_INSERT	@P_CD_COMPANY = @P_CD_COMPANY,	@P_CD_PLANT = @P_CD_PLANT,	@P_NO_IO = @P_NO_IO_203, 
								@P_FG_SLIP = N'203',			@P_TP_SLIP = N'1',			@P_YN_WIP = N'Y', 
								@P_DT_IO = @P_DT_WORK,			@P_CD_DEPT = @P_CD_DEPT,	@P_CD_WC = @P_CD_WC, 
								@P_NO_EMP = @P_NO_EMP,			@P_ID_INSERT = @P_ID_INSERT

		IF (@@ERROR <> 0) 
		BEGIN 
			SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
			GOTO ERROR 
		END

		--SELECT @P_NO_IO_LINE_203 = 1        -- 방금채번한공정수불의항번은MAX값을구할필요없이초기번호로세팅한다.

		EXEC UP_PR_QTIO_INSERT	@P_CD_COMPANY = @P_CD_COMPANY,		@P_CD_PLANT = @P_CD_PLANT,	@P_NO_IO = @P_NO_IO_203, 
								@P_NO_LINE_IO = @P_NO_IO_LINE_203,	@P_FG_SLIP = N'203',		@P_TP_SLIP = N'1', 
								@P_YN_WIP = N'Y',					@P_CD_ITEM = @P_CD_ITEM,	@P_CD_WC_FR = @P_CD_WC, 
								@P_CD_WC_TO = '',					@P_DT_IO = @P_DT_WORK,		@P_QT_IO = @P_QT_MOVE, 
								@P_QT_PROC = @P_QT_MOVE,			@P_NO_WO = @P_NO_WO,		@P_NO_SRC = @P_NO_WORK, 
								@P_CD_OP = @P_CD_OP,				@P_ID_INSERT = @P_ID_INSERT

		IF (@@ERROR <> 0) 
		BEGIN 
			SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
			GOTO ERROR 
		END
		
		IF (@P_YN_QC = 'Y' AND @P_FG_OPQC = 'Y' AND @P_MA_EXC_QC = '000')
		BEGIN 
			EXEC UP_MM_QC_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_NO_REQ = @P_NO_WORK, @P_DT_REQ = @P_DT_WORK, @P_CD_PARTNER = @V_CD_PARTNER, @P_NO_EMP = @P_NO_EMP, @P_FG_IO = '998' 
			
			IF (@@ERROR <> 0) 
			BEGIN 
				SELECT @ERRMSG = '[UP_MM_QC_INSERT]작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR 
			END
			
			EXEC UP_MM_QCL_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_NO_REQ = @P_NO_WORK, @P_NO_LINE = 1, @P_CD_PLANT = @P_CD_PLANT, @P_CD_ITEM = @P_CD_ITEM, @P_QT_REQ = @P_QT_WORK, @P_DC_RMK = ''
			
			IF (@@ERROR <> 0) 
			BEGIN 
				SELECT @ERRMSG = '[UP_MM_QCL_INSERT]작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR 
			END						
		END		

		--작업지시공정경로(PR_WO_ROUT) 현재공정(@P_CD_OP)의재공수량(QT_WIP), 작업수량(QT_WORK), 이동수량(QT_MOVE), 상태값(ST_OP) UPDATE
		UPDATE	PR_WO_ROUT
		SET		QT_WIP = A.QT_START - (ISNULL(A.QT_WORK, 0) + @P_QT_WORK) + (ISNULL(A.QT_REJECT, 0) + @P_QT_REJECT) - A.QT_REWORK - ISNULL(A.QT_BAD, 0),
				QT_WORK = ISNULL(QT_WORK,0) + ISNULL(@P_QT_WORK, 0),
				QT_MOVE = ISNULL(QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0),
				ST_OP = 'S'--CASE WHEN A.QT_WO > ( ISNULL(QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0) ) THEN 'S' ELSE CASE WHEN @P_YN_AUTO_CLS = 'Y' THEN 'C' ELSE 'S' END END
		FROM	PR_WO_ROUT A
		WHERE	A.CD_COMPANY = @P_CD_COMPANY
		AND		A.CD_PLANT = @P_CD_PLANT
		AND		A.NO_WO = @P_NO_WO
		AND		A.CD_OP = @P_CD_OP
		
		---------작업지시분할--------------------------------------------------------------------------------------------------
		UPDATE	PR_WO_ROUT_REL
		SET		QT_WIP = A.QT_REL - (ISNULL(A.QT_WORK, 0) + @P_QT_WORK) + (ISNULL(A.QT_REJECT, 0) + @P_QT_REJECT) - A.QT_REWORK - ISNULL(A.QT_BAD, 0),
				QT_WORK = ISNULL(QT_WORK,0) + ISNULL(@P_QT_WORK, 0),
				QT_MOVE = ISNULL(QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0),
				ST_OP = 'S',--CASE WHEN A.QT_REL > ( ISNULL(QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0) ) THEN 'S' ELSE CASE WHEN @P_YN_AUTO_CLS = 'Y' THEN 'C' ELSE 'S' END END,
				YN_CLOSE = 'N'--CASE WHEN A.QT_REL > ( ISNULL(QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0) ) THEN 'N' ELSE 'Y' END
		FROM	PR_WO_ROUT_REL A
		WHERE	A.CD_COMPANY = @P_CD_COMPANY
		AND		A.CD_PLANT = @P_CD_PLANT
		AND		A.NO_WO = @P_NO_WO
		AND		A.CD_OP = @P_CD_OP
		AND		A.NO_REL = @P_NO_REL
		
		------------------------------------------------------------------------------------------------------------
		

		--현재공정이마지막공정이면서LOT번호를KEYING한경우PR_QTIOLOT을생산출고전표(FG_SLIP = '203')로생성한다.
		IF (@P_CD_OP = @P_LAST_CD_OP AND ISNULL(@P_NO_LOT, '') <> '' AND @YN_LOT = 'Y')
		BEGIN
				-- PR_QTIOLOT의이동출고전표를생선한다.(FG_SLIP = '203')
				INSERT PR_QTIOLOT(	CD_COMPANY,		CD_PLANT,		NO_IO,			NO_LINE_IO,			FG_SLIP, 
									NO_LOT,			CD_ITEM,		DT_IO,			CD_SL,				QT_IO, 
									QT_MGMT,		YN_RETURN,		NO_WO,			NO_WORK,			CD_OP, 
									CD_WC,			CD_WCOP,		NO_IO_MGMT,		NO_LINE_IO_MGMT,	FG_SLIP_MGMT, 
									NO_LOT_MGMT,	NO_IO_SC,		NO_LINE_IO_SC,	FG_SLIP_SC,			NO_LOT_SC, 
									NO_IO_MM,		NO_IOLINE_MM,	NO_LOT_MM,		ID_INSERT,			DTS_INSERT,
									NO_SRC,			DT_LIMIT,		CD_MNG1,		CD_MNG2,			CD_MNG3,
									CD_MNG4,		CD_MNG5,		CD_MNG6,		CD_MNG7,			CD_MNG8,
									CD_MNG9,		CD_MNG10,		CD_MNG11,		CD_MNG12,			CD_MNG13,
									CD_MNG14,		CD_MNG15,		CD_MNG16,		CD_MNG17,			CD_MNG18,
									CD_MNG19,		CD_MNG20
									)
							SELECT	@P_CD_COMPANY,	@P_CD_PLANT,	NO_IO,			NO_LINE_IO,			'203', 
									@P_NO_LOT,		CD_ITEM,		DT_IO,			@P_CD_SL,			QT_IO, 
									0,				'N',			NO_WO,			@P_NO_WORK,			CD_OP, 
									@P_CD_WC,		CD_WCOP,		NULL,			0,					NULL, 
									NULL,			NULL,			0,				NULL,				NULL, 
									NULL,			0,				NULL,			ID_INSERT,			DTS_INSERT,
									@P_NO_WORK,		@P_DT_LIMIT,	@P_CD_MNG1,		@P_CD_MNG2,			@P_CD_MNG3,
									@P_CD_MNG4,		@P_CD_MNG5,		@P_CD_MNG6,		@P_CD_MNG7,			@P_CD_MNG8,
									@P_CD_MNG9,		@P_CD_MNG10,	@P_CD_MNG11,	@P_CD_MNG12,		@P_CD_MNG13,
									@P_CD_MNG14,	@P_CD_MNG15,	@P_CD_MNG16,	@P_CD_MNG17,		@P_CD_MNG18,
									@P_CD_MNG19,	@P_CD_MNG20
							FROM	PR_QTIO
							WHERE	CD_COMPANY = @P_CD_COMPANY
							AND		CD_PLANT = @P_CD_PLANT
							AND		NO_IO = @P_NO_IO_203
							AND		NO_LINE_IO = @P_NO_IO_LINE_203
							AND		FG_SLIP = '203'

				IF (@@ERROR <> 0) 
				BEGIN	
					SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR 
				END
		END
	END
END

--*******************마지막공정이면자동생산입고의뢰, 자동입고***********************************
IF (@P_LAST_CD_OP = @P_CD_OP)   --마지막 공정
BEGIN
	IF (@P_QT_MOVE <> 0.0)      --이동수량 있음
	BEGIN
		--검사품목이면서 수불전 검사가 아닌경우
		IF(ISNULL(@V_QC_YN, '') <> '100')
		BEGIN
			--생산(생산입고의뢰등록)▼▼▼------------------------------------------------------------------------------------
			--IF (@YN_AUTORCV_REQ = 'Y' AND @YN_AUTORCV = 'N') --20120514 최인성 의뢰등록인 경우는 의뢰등록만 가능해야하는데 IF문이 바뀌었음.
			IF (@YN_AUTORCV_REQ = 'Y')   
			BEGIN    --★ i01
				EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_MODULE = 'PR', @P_CD_CLASS = '06', @P_DOCU_YM = @P_DT_WORK, @P_NO = @NO_RCV OUTPUT

				IF (@@ERROR <> 0) 
				BEGIN 
					SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR 
				END

				EXEC UP_PR_RCVH_INSERT @P_CD_COMPANY = @P_CD_COMPANY, 
									   @P_CD_PLANT= @P_CD_PLANT, 
									   @P_NO_REQ = @NO_RCV, 
									   @P_DT_REQ= @P_DT_WORK, 
									   @P_NO_EMP= @P_NO_EMP, 
									   @P_DC_RMK = '', 
									   @P_ID_INSERT = @P_ID_INSERT

				IF (@@ERROR <> 0) 
				BEGIN 
					SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR 
				END


				IF (@P_YN_QC = 'Y' AND @V_FG_PQC = 'Y')
				BEGIN
						SELECT @V_YN_INSPECT = 'Y'
				END


				--@P_YN_QC 는품질모듈이돌아갈때공장품목에서세팅한값을가지고와야함.
				--현재화면에서는@P_QT_REQ_W 와@P_QT_REQ_B은을넣고있음
				EXEC UP_PR_RCVL_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT= @P_CD_PLANT, @P_NO_REQ = @NO_RCV, 
					@P_NO_LINE = 1, @P_CD_WC = @P_CD_WC, @P_CD_ITEM = @P_CD_ITEM, @P_DT_REQ = @P_DT_WORK, @P_QT_REQ = @P_QT_MOVE, 
					@P_QT_REQ_W = @P_QT_MOVE, @P_QT_REQ_B = 0, @P_YN_QC = @V_YN_INSPECT, @P_QT_RCV=0, @P_CD_SL = @P_CD_SL, @P_NO_WO = @P_NO_WO, 
					@P_NO_WORK = @P_NO_WORK, @P_TP_WB = '0', @P_DC_RMK = '', @P_ID_INSERT = @P_ID_INSERT

				IF (@@ERROR <> 0) 
				BEGIN 
					SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR 
				END
						
			END --(@YN_AUTORCV_REQ = 'Y')   --★ i01
			--생산(생산입고의뢰등록)▲▲▲------------------------------------------------------------------------------------


			--◈ 작업지시번호의 자동입고처리여부 (작업지시등록시◈공장환경설정에서가져온다.)
			IF (@YN_AUTORCV_REQ = 'Y' AND @YN_AUTORCV = 'Y')
			BEGIN
				--구매(생산입고등록)------------------------------------------------------------------------------------
				EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_MODULE = 'PU', @P_CD_CLASS = '18', @P_DOCU_YM = @P_DT_WORK, @P_NO = @NO_IO_MM OUTPUT

				IF (@@ERROR <> 0) 
				BEGIN 
					SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR 
				END

				EXEC UP_PU_MM_QTIOH_INSERT @P_NO_IO= @NO_IO_MM, @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT = @P_CD_PLANT, @P_CD_PARTNER = NULL, @P_FG_TRANS=N'1', @P_YN_RETURN=N'N', @P_DT_IO = @P_DT_WORK, @P_GI_PARTNER = '', @P_CD_DEPT = @P_CD_DEPT, @P_NO_EMP = @P_NO_EMP, @P_DC_RMK=NULL, @P_ID_INSERT = @P_ID_INSERT, @P_CD_QTIOTP = @TP_GR
				
				IF (@@ERROR <> 0) 
				BEGIN 
					SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR 
				END

				IF (@V_QC_YN = '200' AND @V_FG_PQC = 'Y')
				BEGIN
						SELECT @V_YN_INSPECT = 'Y'
				END
				
				--@P_QT_INSP의값을생각해봐야함(현재는그냥)
				EXEC UP_PU_WGR_INSERT 
						@P_YN_RETURN = 'N',				@P_NO_IO = @NO_IO_MM,			@P_NO_IOLINE = 1, 
						@P_CD_COMPANY = @P_CD_COMPANY,	@P_CD_PLANT = @P_CD_PLANT,		@P_CD_SL = @P_CD_SL, 
						@P_DT_IO = @P_DT_WORK,			@P_NO_ISURCV = @NO_RCV,			@P_NO_ISURCVLINE = 1, 
						@P_NO_PSO_MGMT = @P_NO_WO,		@P_NO_PSO_MGMT2 = @P_NO_WORK,	@P_NO_PSOLINE_MGMT = NULL, 
						@P_CD_ITEM = @P_CD_ITEM,		@P_QT_GOOD_INV = @P_QT_MOVE,	@P_QT_REJECT_INV = 0, 
						@P_NO_EMP = @P_NO_EMP,			@P_CD_QTIOTP = @TP_GR,			@P_QT_INSP = 0, 
						@P_NO_WORK = @P_NO_WORK,		@P_CD_PJT = @CD_PJT,			@P_FG_IO = '002',
						@P_YN_INSPECT = @V_YN_INSPECT,	@SEQ_PROJECT = @V_SEQ_PROJECT,	@P_UM_EVAL = 0,
						@P_CD_PARTNER = NULL,			@P_DC_RMK = NULL,				@P_CD_USERDEF1 = NULL,
						@P_CD_USERDEF2 = NULL,			@P_CD_USERDEF3 = NULL,			@P_CD_USERDEF4 = NULL,
						@P_CD_USERDEF5 = NULL,			@P_CD_CC = NULL,				@P_NO_IO_MGMT = NULL,
						@P_CD_WC = @P_CD_WC
				
				IF (@V_SERVER_KEY LIKE 'CYMT%')
				BEGIN
					UPDATE	MM_QTIO
					SET		NM_USERDEF1 = @P_TXT_USERDEF1,
							NM_USERDEF2 = @P_TXT_USERDEF2
					WHERE	NO_IO = @NO_IO_MM
					AND		NO_IOLINE = 1
					AND		CD_COMPANY = @P_CD_COMPANY
				END
				--구매(생산입고등록)------------------------------------------------------------------------------------

				--생산출고전표(FG_SLIP = '203')의LOT가있으면자재의생산입고LOT(MM_QTIOLOT.FG_IO = '002') 를생선한다.
				--현재공정이마지막공정이면서LOT번호를KEYING한경우자재의생산입고LOT(MM_QTIOLOT.FG_IO = '002') 를생선한다.
				--현재공정이마지막공정이면서LOT번호를작지에서입력한경우자재의생산입고LOT(MM_QTIOLOT.FG_IO = '002') 를생선한다.
				IF (ISNULL(@P_NO_LOT, '') <> '' AND @YN_LOT = 'Y')
				BEGIN
					EXEC UP_MM_QTIOLOT_INSERT	@P_CD_COMPANY     = @P_CD_COMPANY,	@P_NO_IO           = @NO_IO_MM,		@P_NO_IOLINE   = 1, 
												@P_NO_LOT         = @P_NO_LOT,		@P_CD_ITEM         = @P_CD_ITEM,	@P_DT_IO       = @P_DT_WORK, 
												@P_FG_PS          = '1',			@P_FG_IO           = '002',			@P_CD_QTIOTP   = '981',			
												@P_CD_SL          = @P_CD_SL,		@P_QT_IO           = @P_QT_MOVE,	@P_NO_IO_MGMT  = @NO_IO_MM, 
												@P_NO_IOLINE_MGMT = 1,				@P_NO_IOLINE_MGMT2 = 0,				@P_YN_RETURN   = 'N', 
												@CD_PLANT_PR      = @P_CD_PLANT,	@NO_IO_PR          = @P_NO_IO_203,	@NO_LINE_IO_PR = @P_NO_IO_LINE_203, 
												@NO_LINE_IO2_PR   = 0,				@FG_SLIP_PR        = '203',			@NO_LOT_PR     = @P_NO_LOT,
												@P_DT_LIMIT       = @P_DT_LIMIT,	@P_CD_MNG1         = @P_CD_MNG1,	@P_CD_MNG2     = @P_CD_MNG2,
												@P_CD_MNG3        = @P_CD_MNG3,		@P_CD_MNG4         = @P_CD_MNG4,	@P_CD_MNG5     = @P_CD_MNG5,
												@P_CD_MNG6        = @P_CD_MNG6,		@P_CD_MNG7         = @P_CD_MNG7,	@P_CD_MNG8     = @P_CD_MNG8,
												@P_CD_MNG9        = @P_CD_MNG9,		@P_CD_MNG10        = @P_CD_MNG10,	@P_CD_MNG11    = @P_CD_MNG11,
												@P_CD_MNG12       = @P_CD_MNG12,	@P_CD_MNG13        = @P_CD_MNG13,	@P_CD_MNG14    = @P_CD_MNG14,
												@P_CD_MNG15       = @P_CD_MNG15,	@P_CD_MNG16        = @P_CD_MNG16,	@P_CD_MNG17    = @P_CD_MNG17,
												@P_CD_MNG18       = @P_CD_MNG18,	@P_CD_MNG19        = @P_CD_MNG19,	@P_CD_MNG20    = @P_CD_MNG20

					IF (@@ERROR <> 0) 
					BEGIN 
						SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
						GOTO ERROR 
					END
					
					IF (@V_SERVER_KEY LIKE 'AJNSF%')
					BEGIN
						UPDATE	MM_QTIOLOT
						SET		DC_RMK = @P_TXT_USERDEF2
						WHERE	NO_IO = @NO_IO_MM
						AND		NO_IOLINE = 1
						AND		CD_COMPANY = @P_CD_COMPANY
						AND		NO_LOT = @P_NO_LOT
					END
				END
			END --(@YN_AUTORCV = 'Y')
		END	--IF(ISNULL(@V_FG_PQC, 'N') <> 'Y' AND ISNULL(@V_QC_YN, '') <> '100')
		ELSE IF(ISNULL(@V_QC_YN, '') = '100') --100 : 수불전검사
		BEGIN
			IF(ISNULL(@V_FG_PQC, 'N') = 'N')--생산입고검사여부
			BEGIN
					--생산(생산입고의뢰등록)▼▼▼------------------------------------------------------------------------------------
					--IF (@YN_AUTORCV_REQ = 'Y' AND @YN_AUTORCV = 'N') --20120514 최인성 의뢰등록인 경우는 의뢰등록만 가능해야하는데 IF문이 바뀌었음.
					IF (@YN_AUTORCV_REQ = 'Y')   
					BEGIN    --★ i01
						EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_MODULE = 'PR', @P_CD_CLASS = '06', @P_DOCU_YM = @P_DT_WORK, @P_NO = @NO_RCV OUTPUT

						IF (@@ERROR <> 0) 
						BEGIN 
							SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
							GOTO ERROR 
						END

						EXEC UP_PR_RCVH_INSERT @P_CD_COMPANY = @P_CD_COMPANY, 
											   @P_CD_PLANT= @P_CD_PLANT, 
											   @P_NO_REQ = @NO_RCV, 
											   @P_DT_REQ= @P_DT_WORK, 
											   @P_NO_EMP= @P_NO_EMP, 
											   @P_DC_RMK = '', 
											   @P_ID_INSERT = @P_ID_INSERT

						IF (@@ERROR <> 0) 
						BEGIN 
							SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
							GOTO ERROR 
						END


						IF (@P_YN_QC = 'Y' AND @V_FG_PQC = 'Y')
						BEGIN
								SELECT @V_YN_INSPECT = 'Y'
						END


						--@P_YN_QC 는품질모듈이돌아갈때공장품목에서세팅한값을가지고와야함.
						--현재화면에서는@P_QT_REQ_W 와@P_QT_REQ_B은을넣고있음
						EXEC UP_PR_RCVL_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT= @P_CD_PLANT, @P_NO_REQ = @NO_RCV, 
							@P_NO_LINE = 1, @P_CD_WC = @P_CD_WC, @P_CD_ITEM = @P_CD_ITEM, @P_DT_REQ = @P_DT_WORK, @P_QT_REQ = @P_QT_MOVE, 
							@P_QT_REQ_W = @P_QT_MOVE, @P_QT_REQ_B = 0, @P_YN_QC = @V_YN_INSPECT, @P_QT_RCV=0, @P_CD_SL = @P_CD_SL, @P_NO_WO = @P_NO_WO, 
							@P_NO_WORK = @P_NO_WORK, @P_TP_WB = '0', @P_DC_RMK = '', @P_ID_INSERT = @P_ID_INSERT

						IF (@@ERROR <> 0) 
						BEGIN 
							SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
							GOTO ERROR 
						END
					END --(@YN_AUTORCV_REQ = 'Y')   --★ i01
					--생산(생산입고의뢰등록)▲▲▲------------------------------------------------------------------------------------


					--◈ 작업지시번호의 자동입고처리여부 (작업지시등록시◈공장환경설정에서가져온다.)
					IF (@YN_AUTORCV_REQ = 'Y' AND @YN_AUTORCV = 'Y')
					BEGIN
						--구매(생산입고등록)------------------------------------------------------------------------------------
						EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_MODULE = 'PU', @P_CD_CLASS = '18', @P_DOCU_YM = @P_DT_WORK, @P_NO = @NO_IO_MM OUTPUT

						IF (@@ERROR <> 0) 
						BEGIN 
							SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
							GOTO ERROR 
						END

						EXEC UP_PU_MM_QTIOH_INSERT @P_NO_IO= @NO_IO_MM, @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT = @P_CD_PLANT, @P_CD_PARTNER = NULL, @P_FG_TRANS=N'1', @P_YN_RETURN=N'N', @P_DT_IO = @P_DT_WORK, @P_GI_PARTNER = '', @P_CD_DEPT = @P_CD_DEPT, @P_NO_EMP = @P_NO_EMP, @P_DC_RMK=NULL, @P_ID_INSERT = @P_ID_INSERT, @P_CD_QTIOTP = @TP_GR
						
						IF (@@ERROR <> 0) 
						BEGIN 
							SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
							GOTO ERROR 
						END


						IF (@V_QC_YN = '200' AND @V_FG_PQC = 'Y')
						BEGIN
								SELECT @V_YN_INSPECT = 'Y'
						END
						
						--@P_QT_INSP의값을생각해봐야함(현재는그냥)
						EXEC UP_PU_WGR_INSERT 
								@P_YN_RETURN = 'N',				@P_NO_IO = @NO_IO_MM,			@P_NO_IOLINE = 1, 
								@P_CD_COMPANY = @P_CD_COMPANY,	@P_CD_PLANT = @P_CD_PLANT,		@P_CD_SL = @P_CD_SL, 
								@P_DT_IO = @P_DT_WORK,			@P_NO_ISURCV = @NO_RCV,			@P_NO_ISURCVLINE = 1, 
								@P_NO_PSO_MGMT = @P_NO_WO,		@P_NO_PSO_MGMT2 = @P_NO_WORK,	@P_NO_PSOLINE_MGMT=NULL, 
								@P_CD_ITEM = @P_CD_ITEM,		@P_QT_GOOD_INV = @P_QT_MOVE,	@P_QT_REJECT_INV = 0, 
								@P_NO_EMP = @P_NO_EMP,			@P_CD_QTIOTP = @TP_GR,			@P_QT_INSP = 0, 
								@P_NO_WORK = @P_NO_WORK,		@P_CD_PJT = @CD_PJT,			@P_FG_IO = '002',
								@P_YN_INSPECT = @V_YN_INSPECT,	@SEQ_PROJECT = @V_SEQ_PROJECT,	@P_UM_EVAL = 0,
								@P_CD_PARTNER = NULL,			@P_DC_RMK = NULL,				@P_CD_USERDEF1 = NULL,
								@P_CD_USERDEF2 = NULL,			@P_CD_USERDEF3 = NULL,			@P_CD_USERDEF4 = NULL,
								@P_CD_USERDEF5 = NULL,			@P_CD_CC = NULL,				@P_NO_IO_MGMT = NULL,
								@P_CD_WC = @P_CD_WC
						
						IF (@V_SERVER_KEY LIKE 'CYMT%')
						BEGIN
							UPDATE	MM_QTIO
							SET		NM_USERDEF1 = @P_TXT_USERDEF1,
									NM_USERDEF2 = @P_TXT_USERDEF2
							WHERE	NO_IO = @NO_IO_MM
							AND		NO_IOLINE = 1
							AND		CD_COMPANY = @P_CD_COMPANY
						END
						--구매(생산입고등록)------------------------------------------------------------------------------------

						--생산출고전표(FG_SLIP = '203')의LOT가있으면자재의생산입고LOT(MM_QTIOLOT.FG_IO = '002') 를생선한다.
						--현재공정이마지막공정이면서LOT번호를KEYING한경우자재의생산입고LOT(MM_QTIOLOT.FG_IO = '002') 를생선한다.
						--현재공정이마지막공정이면서LOT번호를작지에서입력한경우자재의생산입고LOT(MM_QTIOLOT.FG_IO = '002') 를생선한다.
						IF (ISNULL(@P_NO_LOT, '') <> '' AND @YN_LOT = 'Y')
						BEGIN
							EXEC UP_MM_QTIOLOT_INSERT	@P_CD_COMPANY = @P_CD_COMPANY,	@P_NO_IO = @NO_IO_MM,				@P_NO_IOLINE = 1, 
														@P_NO_LOT = @P_NO_LOT,			@P_CD_ITEM = @P_CD_ITEM,			@P_DT_IO = @P_DT_WORK, 
														@P_FG_PS = '1',					@P_FG_IO = '002',					@P_CD_QTIOTP = '981',			
														@P_CD_SL = @P_CD_SL,			@P_QT_IO = @P_QT_MOVE,				@P_NO_IO_MGMT = @NO_IO_MM, 
														@P_NO_IOLINE_MGMT = 1,			@P_NO_IOLINE_MGMT2 = 0,				@P_YN_RETURN = 'N', 
														@CD_PLANT_PR = @P_CD_PLANT,		@NO_IO_PR = @P_NO_IO_203,			@NO_LINE_IO_PR = @P_NO_IO_LINE_203, 
														@NO_LINE_IO2_PR = 0,			@FG_SLIP_PR = '203',				@NO_LOT_PR = @P_NO_LOT,
														@P_DT_LIMIT = @P_DT_LIMIT

							IF (@@ERROR <> 0) 
							BEGIN 
								SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
								GOTO ERROR 
							END
					
							IF (@V_SERVER_KEY LIKE 'AJNSF%')
							BEGIN
								UPDATE	MM_QTIOLOT
								SET		DC_RMK = @P_TXT_USERDEF2
								WHERE	NO_IO = @NO_IO_MM
								AND		NO_IOLINE = 1
								AND		CD_COMPANY = @P_CD_COMPANY
								AND		NO_LOT = @P_NO_LOT
							END
						END
					END --(@YN_AUTORCV = 'Y')
			END--(ISNULL(@V_FG_PQC, 'N') = 'N')--생산입고검사여부
			ELSE IF (ISNULL(@V_FG_PQC, 'N') = 'Y' AND @P_MA_EXC_QC = '100')--생산입고검사여부, 검사수불생성기준
			BEGIN
				--생산(생산입고의뢰등록)▼▼▼------------------------------------------------------------------------------------
				IF (@YN_AUTORCV_REQ = 'Y')   
				BEGIN    --★ i01
					EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_MODULE = 'PR', @P_CD_CLASS = '06', @P_DOCU_YM = @P_DT_WORK, @P_NO = @NO_RCV OUTPUT

					IF (@@ERROR <> 0) 
					BEGIN 
						SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
						GOTO ERROR 
					END

					EXEC UP_PR_RCVH_INSERT @P_CD_COMPANY = @P_CD_COMPANY, 
										   @P_CD_PLANT= @P_CD_PLANT, 
										   @P_NO_REQ = @NO_RCV, 
										   @P_DT_REQ= @P_DT_WORK, 
										   @P_NO_EMP= @P_NO_EMP, 
										   @P_DC_RMK = '', 
										   @P_ID_INSERT = @P_ID_INSERT

					IF (@@ERROR <> 0) 
					BEGIN 
						SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
						GOTO ERROR 
					END

					IF (@P_YN_QC = 'Y' AND @V_FG_PQC = 'Y')
					BEGIN
							SELECT @V_YN_INSPECT = 'Y'
					END

					--@P_YN_QC 는품질모듈이돌아갈때공장품목에서세팅한값을가지고와야함.
					--현재화면에서는@P_QT_REQ_W 와@P_QT_REQ_B은을넣고있음
					EXEC UP_PR_RCVL_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT= @P_CD_PLANT, @P_NO_REQ = @NO_RCV, 
						@P_NO_LINE = 1, @P_CD_WC = @P_CD_WC, @P_CD_ITEM = @P_CD_ITEM, @P_DT_REQ = @P_DT_WORK, @P_QT_REQ = @P_QT_MOVE, 
						@P_QT_REQ_W = @P_QT_MOVE, @P_QT_REQ_B = 0, @P_YN_QC = @V_YN_INSPECT, @P_QT_RCV=0, @P_CD_SL = @P_CD_SL, @P_NO_WO = @P_NO_WO, 
						@P_NO_WORK = @P_NO_WORK, @P_TP_WB = '0', @P_DC_RMK = '', @P_ID_INSERT = @P_ID_INSERT

					IF (@@ERROR <> 0) 
					BEGIN 
						SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
						GOTO ERROR 
					END				
				END --(@YN_AUTORCV_REQ = 'Y')   --★ i01
				--생산(생산입고의뢰등록)▲▲▲------------------------------------------------------------------------------------
			END
		END --ELSE IF(ISNULL(@V_QC_YN, '') = '100')
	END --(@P_QT_MOVE <> 0.0)
END --(@P_LAST_CD_OP = @P_CD_OP)
--*******************마지막공정이면자동생산입고의뢰, 자동입고끝***********************************

DECLARE @QT_MOVE_START	NUMERIC(17, 4)

SELECT @QT_MOVE_START = ISNULL((	SELECT	QT_MOVE
									FROM	PR_WO_ROUT
									WHERE	CD_COMPANY = @P_CD_COMPANY
									AND		NO_WO = @P_NO_WO
									AND		CD_OP = @P_BEFORE_CD_OP	), 0)


--공정의작업지시수량(QT_WO)과이동수량(QT_MOVE)이같은건이존재하면마감된것으로간주하여
--공정의상태(ST_OP)를마감으로하고마감일(DT_CLOSE)을입력한다.
IF (EXISTS(	SELECT 1
			  FROM PR_WO_ROUT
			 WHERE CD_COMPANY = @P_CD_COMPANY
			   AND NO_WO = @P_NO_WO
			   AND CD_OP = @P_CD_OP
			   AND ((@P_YN_QT_WORK = 'N' 
					AND ((@P_FIRST_CD_OP = @P_CD_OP  AND QT_WO = QT_MOVE + ISNULL(QT_BAD, 0))
					  OR (@P_FIRST_CD_OP <> @P_CD_OP AND @QT_MOVE_START = QT_START AND QT_START = QT_MOVE + ISNULL(QT_BAD, 0))))
			     OR (@P_YN_QT_WORK = 'Y' 
					AND ((@P_FIRST_CD_OP = @P_CD_OP  AND QT_WO <= QT_MOVE + ISNULL(QT_BAD, 0))
					  OR (@P_FIRST_CD_OP <> @P_CD_OP AND @QT_MOVE_START >= QT_START AND QT_START <= QT_MOVE + ISNULL(QT_BAD, 0))))))
   AND (EXISTS(	SELECT 1
				  FROM PR_WO_ROUT
				 WHERE (CD_COMPANY = @P_CD_COMPANY)
				   AND (NO_WO = @P_NO_WO)
				   AND (CD_OP = @P_BEFORE_CD_OP)
				   AND (ST_OP = 'C')) OR @P_CD_OP = @P_FIRST_CD_OP))
BEGIN
	UPDATE PR_WO_ROUT
	   SET DT_CLOSE = @P_DT_WORK,
		   ST_OP = CASE WHEN @P_YN_AUTO_CLS = 'Y' THEN 'C' ELSE 'S' END
	 WHERE CD_COMPANY = @P_CD_COMPANY
	   AND CD_PLANT = @P_CD_PLANT
	   AND NO_WO = @P_NO_WO
	   AND CD_OP = @P_CD_OP
	   
	   
	   -----------작업지시분할----------------------------------------------------------------
	   UPDATE PR_WO_ROUT_REL
	   SET DT_CLOSE = @P_DT_WORK,
		   ST_OP = CASE WHEN @P_YN_AUTO_CLS = 'Y' THEN 'C' ELSE 'S' END,
		   YN_CLOSE = 'Y'
	 WHERE CD_COMPANY = @P_CD_COMPANY
	   AND CD_PLANT = @P_CD_PLANT
	   AND NO_WO = @P_NO_WO
	   AND CD_OP = @P_CD_OP
	   AND NO_REL =  @P_NO_REL
	   -----------작업지시분할----------------------------------------------------------------

	IF (@@ERROR <> 0) 
	BEGIN 
		SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR 
	END

	--마지막공정이마감됐으면작업지시를마감시킨다.(PR_WO.ST_WO = 'C')
	IF (@P_CD_OP = @P_LAST_CD_OP)
	BEGIN
		UPDATE PR_WO
		   SET DT_CLOSE = @P_DT_WORK,
		  	   ST_WO = CASE WHEN @P_YN_AUTO_CLS = 'Y' THEN 'C' ELSE 'S' END
		 WHERE CD_COMPANY = @P_CD_COMPANY
		   AND CD_PLANT = @P_CD_PLANT
		   AND NO_WO = @P_NO_WO

		IF (@@ERROR <> 0) 
		BEGIN	
		  SELECT @ERRMSG = '[SP_CZ_PR_WORK_REG_I]작업을정상적으로처리하지못했습니다.' 
		  GOTO ERROR 
		END
	END
END
--**********************************************************************************
--*****************************************// 공정이동등록끝

IF (@P_CD_OP = @P_LAST_CD_OP AND NEOE.FN_SERVERKEY('N','RKP|')  = 'Y')	
BEGIN
	EXEC UP_PR_Z_RKP_MOLDSHOT_IU @P_CD_COMPANY, @P_CD_PLANT, @P_NO_WORK, @P_DT_WORK, @P_CD_ITEM, @P_QT_WORK
END
ELSE IF (@P_CD_OP = @P_LAST_CD_OP AND NEOE.FN_SERVERKEY('N','BCWP|BCWH|')  = 'Y')	
BEGIN
	UPDATE PR_WO
	   SET DT_CLOSE = @P_DT_WORK,
	  	   ST_WO    = 'C'
	 WHERE CD_COMPANY = @P_CD_COMPANY
	   AND CD_PLANT = @P_CD_PLANT
	   AND NO_WO = @P_NO_WO
	   AND ST_WO <> 'C'
END

--
-- 통제값 : 작업실적등록_생산수량자동마감기준 이 '100'일 경우
-- 마지막 공정이면서, 실적수량이 공정의 시작수량보다 크거나같을경우 작업지시 자동 마감
--
IF (@P_CD_OP = @P_LAST_CD_OP AND @P_MA_EXC_AUTO_CLS = '100' AND 
	EXISTS( SELECT	1
			FROM	PR_WO_ROUT A
			WHERE	CD_COMPANY = @P_CD_COMPANY
			AND		CD_PLANT   = @P_CD_PLANT
			AND		NO_WO	   = @P_NO_WO
			AND		CD_OP	   = @P_CD_OP
			AND		QT_START  <= QT_WORK
			AND		QT_START  >= QT_WO))	
BEGIN
	UPDATE PR_WO
	   SET DT_CLOSE = @P_DT_WORK,
	  	   ST_WO    = CASE WHEN @P_YN_AUTO_CLS = 'Y' THEN 'C' ELSE 'S' END
	 WHERE CD_COMPANY = @P_CD_COMPANY
	   AND CD_PLANT = @P_CD_PLANT
	   AND NO_WO = @P_NO_WO
	   AND ST_WO <> 'C'
END

RETURN
ERROR: RAISERROR(@ERRMSG, 18, 1)

END
GO