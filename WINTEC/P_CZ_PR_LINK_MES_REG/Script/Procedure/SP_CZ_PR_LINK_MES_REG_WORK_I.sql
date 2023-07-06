SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [NEOE].[SP_CZ_PR_LINK_MES_REG_WORK_I]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_PLANT		NVARCHAR(7),
	@P_NO_MES		NVARCHAR(20)
)
AS

DECLARE
	@ERRMSG					NVARCHAR(255),
	@V_DOCU_YM				NVARCHAR(6),
	@V_YN_REQ				NVARCHAR(1), --자재 출고 생성여부 (자재투입, 자재청구, 생산출고)
	@V_YN_REQ_ALL_BF		NVARCHAR(1), --자재 출고 생성여부 (자재투입, 자재청구, 생산출고) - B/F 가 아닌 자재도 B/F처럼 처리
	@V_YN_QT_WORK			NVARCHAR(1),
	@V_SERVER_KEY			NVARCHAR(50) 

SET	@V_DOCU_YM = LEFT(NEOE.SF_SYSDATE(GETDATE()), 6)
SET @V_YN_REQ = 'Y'
SET @V_YN_REQ_ALL_BF = 'Y'
SELECT @V_YN_QT_WORK = ISNULL(YN_QT_WORK, 'N') FROM PR_CFG_PLANT WHERE CD_COMPANY = @P_CD_COMPANY AND CD_PLANT = @P_CD_PLANT
SELECT @V_SERVER_KEY = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG WHERE YN_UPGRADE = 'Y'

DECLARE @V_MATL_EXC NVARCHAR(3)
SELECT	@V_MATL_EXC	= CD_EXC FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = 'PR_LINK_MES_자재출고기준'
SET		@V_MATL_EXC = ISNULL(@V_MATL_EXC,'000')

--PR_LINK_MES테이블 정보 변수	
DECLARE
	@P_NO_WO				NVARCHAR(20),
	@P_CD_ITEM				NVARCHAR(50),
	@P_CD_WC				NVARCHAR(7),
	@P_CD_OP				NVARCHAR(4),
	@P_CD_WCOP				NVARCHAR(4),
	@P_DT_WORK				NVARCHAR(8),
	@P_QT_WORK				NUMERIC(17, 4),
	@P_QT_REJECT			NUMERIC(17, 4),
	@P_QT_MOVE				NUMERIC(17, 4),
	@P_QT_BAD				NUMERIC(17, 4),
	@P_NO_EMP				NVARCHAR(10),
	@P_CD_SL_IN				NVARCHAR(7),
	@P_CD_SL_BAD_IN			NVARCHAR(7),
	@P_YN_REWORK			NVARCHAR(1),
	@P_YN_MANDAY			NVARCHAR(1),	
	@V_TM_WORK				NVARCHAR(15),	
	@P_TM_WO_T				NUMERIC(17, 4),	
	@P_QT_WO_ROLL			NUMERIC(17, 4),
	@P_TM_EQ_T				NUMERIC(17, 4),
	@P_NO_LOT				NVARCHAR(100),
	@P_NO_SFT				NVARCHAR(3),
	@P_CD_EQUIP				NVARCHAR(30),
	@P_CD_REJECT			NVARCHAR(4),
	@P_CD_RESOURCE			NVARCHAR(4),
	@P_DC_RMK1				NVARCHAR(100),
	@P_DC_RMK2				NVARCHAR(100),
	@P_NO_PJT				NVARCHAR(20),
	@P_SEQ_PROJECT			NUMERIC(5, 0)
	
SELECT	
	@P_NO_WO				= NO_WO				,
	@P_CD_ITEM				= CD_ITEM			,
	@P_CD_WC				= CD_WC				,
	@P_CD_OP				= CD_OP				,
	@P_CD_WCOP				= CD_WCOP			,
	@P_DT_WORK				= DT_WORK			,
	@P_QT_WORK				= ISNULL(QT_WORK, 0),
	@P_QT_REJECT			= ISNULL(QT_REJECT, 0),
	@P_QT_MOVE				= ISNULL(QT_WORK, 0) - ISNULL(QT_REJECT, 0),
	@P_QT_BAD				= ISNULL(QT_BAD, 0) ,
	@P_NO_EMP				= NO_EMP			,
	@P_CD_SL_IN				= CD_SL_IN			,
	@P_CD_SL_BAD_IN			= CD_SL_BAD_IN		,
	@P_YN_REWORK			= ISNULL(YN_REWORK, 'N'),
	@P_YN_MANDAY			= ISNULL(YN_MANDAY, 'N'),
	@P_TM_WO_T				= TM_WO_T			,	
	@P_QT_WO_ROLL			= ISNULL(QT_WO_ROLL, 0),	
	@P_TM_EQ_T				= TM_EQ_T			,
	@P_NO_LOT				= NO_LOT			,
	@P_NO_SFT				= NO_SFT			,
	@P_CD_EQUIP				= CD_EQUIP			,
	@P_CD_REJECT			= CD_REJECT			,
	@P_CD_RESOURCE			= CD_RESOURCE		,
	@P_DC_RMK1				= DC_RMK1			,
	@P_DC_RMK2				= DC_RMK2			,
	@P_NO_PJT				= NO_PJT			,
	@P_SEQ_PROJECT			= SEQ_PROJECT
FROM	PR_LINK_MES
WHERE	CD_COMPANY	= @P_CD_COMPANY
AND		CD_PLANT	= @P_CD_PLANT
AND		NO_MES		= @P_NO_MES

IF (
	@V_MATL_EXC = '200'
	OR	@V_SERVER_KEY LIKE 'ONEGENE%'	--원진
)
BEGIN
	--생산출고를 생성하지 않음
	SET @V_YN_REQ = 'N'
END

ELSE IF (
	@V_MATL_EXC = '100'
	OR	@V_SERVER_KEY LIKE 'GCI%'		--지앤피
	OR	@V_SERVER_KEY LIKE 'SEALC%'		--성은
	OR	@V_SERVER_KEY LIKE 'WINANT%'	--위너콤
	OR	@V_SERVER_KEY LIKE 'FDWL%'		--푸드웰
	OR	@V_SERVER_KEY LIKE 'YDVALVE%'	--영도산업
)
BEGIN
	--B/F항목만 생산출고를 생성
	SET @V_YN_REQ_ALL_BF = 'N'
END

--기타
ELSE IF (
		@V_SERVER_KEY LIKE 'IKGR%'		--아이케이
	OR	@V_SERVER_KEY LIKE 'EDGC%'		--이원다이애그노믹스
)
BEGIN
	--비고2 에 따라 생산출고 생성
	SET @V_YN_REQ = CASE WHEN ISNULL(@P_DC_RMK2, 'Y') = 'N' THEN 'N' ELSE 'Y' END
END

PRINT '4. 작업실적생성 START'

--
PRINT '<0 작업실적 생성 변수선언> START'
--
DECLARE
	@V_NO_WORK				NVARCHAR(20),   -- 작업실적번호
	@V_ST_WO				NCHAR(1),		-- 작업지시상태
	@V_WO_ROUT_CNT			INT,			-- 작업지시 공정경로 갯수
	@V_WO_ROUT_CNT2			INT,			-- 작업지시 공정 유무
	@V_NO_IO_202_102		NVARCHAR(20),	-- 공정수불번호(이동출고('202')와이동입고('102'))
	@V_NO_IO_203			NVARCHAR(20),	-- 공정수불번호(생산출고('203'))
	
	@V_YN_AUTOBAD			NCHAR(1),		-- 자동공정불량처리여부
	@V_FG_AUTOBAD			NVARCHAR(3),	-- 자동공정불량처리내역
	@V_YN_AUTOBAD_REQ		NCHAR(1),		-- 공정불량자동입고의뢰여부
	@V_YN_AUTOBAD_RCV		NCHAR(1),		-- 공정불량자동입고처리여부
	@V_NO_WORK_BAD			NVARCHAR(20),   -- 불량실적처리하는 작업실적번호  
	@V_NO_IO_BAD_202_102	NVARCHAR(20),	-- 불량실적이동출고, 이동입고번호  
	@V_NO_IO_BAD_203		NVARCHAR(20),   -- 불량품 생산출고번호  
	@V_NO_REQ_BAD			NVARCHAR(20),	-- 불량입고의뢰번호  
	@V_NO_IO_BAD			NVARCHAR(20),   -- 불량입고번호  
	@V_CD_DEPT				NVARCHAR(12),	-- 부서코드

	@V_YN_SUBCON			NVARCHAR(20),   -- 공정외주여부 
	@V_NO_OPOUT_PO			NVARCHAR(20),   -- 공정외주발주번호  
	@V_NO_OPOUT_PO_LINE		NUMERIC(5,0),   -- 공정외주발주항번  
  
	@V_NO_REQ				NVARCHAR(20),   -- 자재청구번호
	@V_NO_REQ_LINE			NUMERIC(5),		-- 자재청구라인번호
	@V_NO_II          		NVARCHAR(20),   -- 자재투입번호
	@V_NO_IO_101            NVARCHAR(20),   -- 생산수불소요자재입고번호
	@V_NO_IO_MM             NVARCHAR(20),   -- 작업지시소요자재생산출고번호
	@V_NO_IO_MM_LINE		NUMERIC(5,0),	-- 작업지시소요자재생산출고라인번호
	@V_NO_LINE				NUMERIC(5,0),	-- 소요자재항번  
	@V_CD_MATL				NVARCHAR(50),	-- 소요자재코드
	@V_YN_BF				NVARCHAR(1),	-- 소요자재 B/F 유무
	@V_FG_GIR				NVARCHAR(1),	-- 소요자재 불출관리여부
	@V_FG_SERNO				NVARCHAR(3),	-- 소요자재 LOT시리얼여부
	@V_CD_SL_OT				NVARCHAR(7),	-- 출고창고
	@V_QT_INPUT				NUMERIC(17,4),	-- 투입시킬 수량  
	@V_QT_REMS				NUMERIC(17,4),	-- 소요잔량  
	@V_QT_INPUT_REAL		NUMERIC(17,4),	-- 실제투입 되는 수량
	@V_TP_GI				NVARCHAR(3),	-- 출고형태,
	@V_YN_AM				NVARCHAR(1),	-- 유무환구분
	@V_NO_WOLINE			NUMERIC(5, 0)   -- 작업지시 공정라인번호
--
PRINT '<0 작업실적 생성 변수선언> END'
--

--
PRINT '<1 작업실적 필수값 가져오기> START'
--
SELECT	@V_ST_WO	= ST_WO,
		@V_TP_GI	= TP_GI
FROM	PR_WO 
WHERE	CD_COMPANY	= @P_CD_COMPANY
AND		CD_PLANT	= @P_CD_PLANT
AND		NO_WO		= @P_NO_WO

/*
SELECT	@V_WO_ROUT_CNT = COUNT(1)  
FROM	PR_WO_ROUT  
WHERE	CD_COMPANY	= @P_CD_COMPANY  
AND		NO_WO		= @P_NO_WO  
*/

SELECT	@V_WO_ROUT_CNT2 = COUNT(1)
FROM	PR_WO_ROUT
WHERE	CD_COMPANY	= @P_CD_COMPANY
AND		NO_WO		= @P_NO_WO
AND		CD_OP		= @P_CD_OP
AND		CD_WC		= @P_CD_WC
AND		CD_WCOP		= @P_CD_WCOP

SELECT	@V_CD_DEPT	= CD_DEPT
FROM	MA_EMP
WHERE	CD_COMPANY	= @P_CD_COMPANY
AND		NO_EMP		= @P_NO_EMP

SELECT	@V_YN_AM	= ISNULL(YN_AM, 'Y')
FROM	MM_EJTP
WHERE	CD_COMPANY	= @P_CD_COMPANY
AND		CD_QTIOTP	= @V_TP_GI

--SELECT	@V_YN_SUBCON = CASE ISNULL(TP_WC,'') WHEN '002' THEN 'Y' ELSE 'N' END
--FROM	MA_WC
--WHERE	CD_COMPANY	= @P_CD_COMPANY 
--AND		CD_PLANT	= @P_CD_PLANT
--AND		CD_WC		= @P_CD_WC

SELECT	@V_YN_SUBCON = ISNULL(YN_SUBCON, 'N'),
		@V_NO_WOLINE = NO_LINE
FROM	PR_WO_ROUT
WHERE	CD_COMPANY	= @P_CD_COMPANY
AND		NO_WO		= @P_NO_WO
AND		CD_OP		= @P_CD_OP
AND		CD_WC		= @P_CD_WC
AND		CD_WCOP		= @P_CD_WCOP

SET @V_NO_OPOUT_PO = ''
SET @V_NO_OPOUT_PO_LINE = 0

IF (@V_YN_SUBCON = 'Y')
BEGIN
	SELECT	@V_NO_OPOUT_PO = NO_PO, @V_NO_OPOUT_PO_LINE = NO_LINE
	FROM	PR_OPOUT_POL
	WHERE	CD_COMPANY	= @P_CD_COMPANY 
	AND		CD_PLANT	= @P_CD_PLANT
	AND		NO_WO		= @P_NO_WO
	AND		CD_OP		= @P_CD_OP
	AND		CD_WC		= @P_CD_WC
	AND		CD_WCOP		= @P_CD_WCOP
	AND		CD_ITEM		= @P_CD_ITEM
END
--
PRINT '<1 작업실적 필수값 가져오기> END'
--

--
PRINT '<2 유효성 체크> START'
--
IF (ISNULL(@P_NO_WO, '') = '')
BEGIN
	SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]작업지시번호가 없습니다.' 
	GOTO ERROR
END

IF (ISNULL(@V_ST_WO, 'C') = 'C')
BEGIN
	SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]작업지시번호(' + @P_NO_WO + ')가 마감(C)된 전표입니다.' 
	GOTO ERROR
END

--IF (ISNULL(@V_ST_WO, 'R') <> 'R')
--BEGIN
--	SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]작업지시번호(' + @P_NO_WO + ')가 발행(R)이 아닙니다.' 
--	GOTO ERROR
--END

/*
--대륜산업의 경우 다공정임 (해당업체 문제 없을시 추후 패키지로 체크 제외해도 상관없을듯)
IF (@V_SERVER_KEY LIKE 'DRAIR%') SET @V_WO_ROUT_CNT = 1

IF (@V_WO_ROUT_CNT > 1)  
BEGIN  
	SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]작업지시번호(' + @P_NO_WO + ')의 경로가 다공정입니다.' 
	GOTO ERROR
END  
*/

IF (@V_WO_ROUT_CNT2 = 0)
BEGIN
	SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]작업지시번호(' + @P_NO_WO + ')의 공정이 잘못되었습니다.'
	GOTO ERROR
END

IF (ISNULL(@P_CD_SL_IN, '') = '')  
BEGIN  
	SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]입고창고(PR_LINK_MES.CD_SL_IN)는 필수값 입니다.' 
	GOTO ERROR
END  

IF (ISNULL(@P_NO_EMP, '') = '')  
BEGIN  
	SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]담당자(PR_LINK_MES.NO_EMP)는 필수값 입니다.' 
	GOTO ERROR
END  

IF (ISNULL(@V_CD_DEPT, '') = '')  
BEGIN  
	SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]부서(MA_EMP.CD_DEPT)는 필수값 입니다.' 
	GOTO ERROR
END  
--
PRINT '<2 유효성 체크> END'
--

--
PRINT '<3 작업실적번호 채번> START'
--
BEGIN
	EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
				  @P_CD_MODULE  = 'PR',
				  @P_CD_CLASS   = '05',
				  @P_DOCU_YM    = @V_DOCU_YM,
				  @P_NO         = @V_NO_WORK OUTPUT

	IF (@@ERROR <> 0)
	BEGIN
		SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
					   + 'CD_MODULE = ''PR'', '
					   + 'CD_CLASS = ''05'' '
					   + '작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR
	END
END
--
PRINT '<3 작업실적번호 채번> END'

--  
PRINT '<4 공정수불번호 자동채번> START'
--  
BEGIN  
	EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
				  @P_CD_MODULE  = 'PR',
				  @P_CD_CLASS   = '06',
				  @P_DOCU_YM    = @V_DOCU_YM,
				  @P_NO         = @V_NO_IO_202_102 OUTPUT

	IF (@@ERROR <> 0)
	BEGIN
		SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
					   + 'CD_MODULE = ''PR'', '
					   + 'CD_CLASS = ''08'' '
					   + '작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR
	END
END  

BEGIN  
	EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
				  @P_CD_MODULE  = 'PR',
				  @P_CD_CLASS   = '13',
				  @P_DOCU_YM    = @V_DOCU_YM,
				  @P_NO         = @V_NO_IO_203 OUTPUT

	IF (@@ERROR <> 0)
	BEGIN
		SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
					   + 'CD_MODULE = ''PR'', '
					   + 'CD_CLASS = ''13'' '
					   + '작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR
	END
END  
--  
PRINT '<4 공정수불번호 자동채번> END'  
--  

--
PRINT '<5 작업실적 생성> START'
--
BEGIN
	--
	PRINT '<5.1 작업실적 INSERT> START'
	--
	IF (@P_YN_REWORK = 'Y')
	BEGIN
		DECLARE @V_QT_MOVE_TEMP NUMERIC(17, 4)
		SET @V_QT_MOVE_TEMP = ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0)
		
		EXEC SP_CZ_PR_REWORK_REG_I
								   @P_CD_COMPANY     = @P_CD_COMPANY,
								   @P_NO_WORK        = @V_NO_WORK,
								   @P_NO_WO          = @P_NO_WO,
								   @P_CD_PLANT       = @P_CD_PLANT,
								   @P_CD_OP          = @P_CD_OP,
								   @P_CD_ITEM        = @P_CD_ITEM,
								   @P_NO_EMP         = @P_NO_EMP,
								   @P_DT_WORK        = @P_DT_WORK,
								   @P_QT_WORK        = @P_QT_WORK,
								   @P_QT_REJECT      = @P_QT_REJECT,
								   @P_CD_REJECT		 = @P_CD_REJECT,
								   @P_QT_MOVE		 = @V_QT_MOVE_TEMP,
								   @P_YN_REWORK		 = @P_YN_REWORK,
								   @P_TM_LABOR       = NULL,
								   @P_CD_RSRC_LABOR  = NULL,
								   @P_TM_MACH        = NULL,				
								   @P_CD_RSRC_MACH   = NULL,
								   @P_CD_WC          = @P_CD_WC,
								   @P_DC_REJECT		 = '',
								   @P_ID_INSERT      = 'MES_SCHEDULER',
								   @P_FG_MOVE        = 'Y',
								   @P_TP_REWORK		 = NULL,
								   @P_CD_REWORK		 = NULL,
								   @P_NO_LOT         = @P_NO_LOT,
								   @P_NO_SFT         = @P_NO_SFT,
								   @P_CD_EQUIP       = @P_CD_EQUIP,
								   @P_CD_SL			 = @P_CD_SL_IN,
								   @P_NO_IO_202_102  = @V_NO_IO_202_102,
								   @P_NO_IO_203      = @V_NO_IO_203,
								   @P_NO_IO_LINE_202 = 1,
								   @P_NO_IO_LINE_102 = 2,
								   @P_NO_IO_LINE_203 = 1,
								   @P_YN_SUBCON      = @V_YN_SUBCON,
								   @P_NO_OPOUT_PO    = @V_NO_OPOUT_PO,
								   @P_NO_OPOUT_LINE  = @V_NO_OPOUT_PO_LINE,
								   @P_NO_REL		 = NULL
								   
		IF (@@ERROR <> 0)  
		BEGIN  
			SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]SP_CZ_PR_REWORK_REG_I작업을정상적으로처리하지못했습니다.' 
			GOTO ERROR  
		END 
	END
	ELSE
	BEGIN
		EXEC SP_CZ_PR_WORK_REG_I @P_CD_COMPANY     = @P_CD_COMPANY,  
								 @P_NO_WORK        = @V_NO_WORK,  
								 @P_NO_WO          = @P_NO_WO,  
								 @P_CD_PLANT       = @P_CD_PLANT,  
								 @P_CD_OP          = @P_CD_OP,  
								 @P_CD_ITEM        = @P_CD_ITEM,  
								 @P_NO_EMP         = @P_NO_EMP,  
								 @P_DT_WORK        = @P_DT_WORK,  
								 @P_QT_WORK		   = @P_QT_WORK,  
								 @P_QT_REJECT      = @P_QT_REJECT,  
								 @P_QT_MOVE        = @P_QT_MOVE,  
								 @P_YN_REWORK      = @P_YN_REWORK,  
								 @P_TM_LABOR       = NULL,  
								 @P_CD_RSRC_LABOR  = NULL,  
								 @P_TM_MACH        = NULL,  
								 @P_CD_RSRC_MACH   = NULL,  
								 @P_CD_WC          = @P_CD_WC,  
								 @P_ID_INSERT      = 'MES_SCHEDULER',  
								 @P_FG_MOVE        = 'Y',  
								 @P_NO_LOT         = @P_NO_LOT,  
								 @P_NO_SFT         = @P_NO_SFT,  
								 @P_CD_EQUIP       = @P_CD_EQUIP,  
								 @P_NO_IO_202_102  = @V_NO_IO_202_102,  
								 @P_NO_IO_203      = @V_NO_IO_203,  
								 @P_NO_IO_LINE_202 = 1,  
								 @P_NO_IO_LINE_102 = 2,  
								 @P_NO_IO_LINE_203 = 1,  
								 @P_CD_SL          = @P_CD_SL_IN,  
								 @P_DC_RMK1        = @P_DC_RMK1,  
								 @P_DC_RMK2        = @P_DC_RMK2,  
								 @P_YN_SUBCON      = @V_YN_SUBCON,
								 @P_NO_OPOUT_PO    = @V_NO_OPOUT_PO,
								 @P_NO_OPOUT_LINE  = @V_NO_OPOUT_PO_LINE

		IF (@@ERROR <> 0)  
		BEGIN  
			SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]SP_CZ_PR_WORK_REG_I작업을정상적으로처리하지못했습니다.' 
			GOTO ERROR  
		END 
	END 
	--
	PRINT '<5.1 작업실적 INSERT> END'
	--
	
	--  
	PRINT '<5.2 YN_MANDAY가 ''Y''인 건은 작업공수 INSERT> START' 
	--  
	IF (@P_YN_MANDAY = 'Y')
	BEGIN  
		SET @V_TM_WORK = '000000'
		
		IF (@P_TM_WO_T > 0)
		BEGIN
			SET @V_TM_WORK = RIGHT('00' + CONVERT(NVARCHAR(2), FLOOR(@P_TM_WO_T / 3600)), 2)
							+ RIGHT('00' + CONVERT(NVARCHAR(2), FLOOR((@P_TM_WO_T % 3600) / 60)), 2)
							+ RIGHT('00' + CONVERT(NVARCHAR(2), FLOOR((@P_TM_WO_T % 60))), 2)
		END
				
		EXEC UP_PR_WORK_MANDAY_SUB_INSERT	@P_CD_COMPANY       = @P_CD_COMPANY,  
											@P_CD_PLANT         = @P_CD_PLANT,  
											@P_NO_WORK			= @V_NO_WORK,  
											@P_NO_MANDAY_LINE   = 1,  
											@P_NO_WO			= @P_NO_WO,  
											@P_NO_ROUT_LINE		= 1,  
											@P_CD_OP			= @P_CD_OP,  
											@P_CD_WCOP			= @P_CD_WCOP,  
											@P_CD_WC			= @P_CD_WC,  
											@P_QT_WORK			= @P_QT_WORK,  
											@P_QT_MOVE			= @P_QT_MOVE,  
											@P_QT_BAD			= @P_QT_BAD,  
											@P_QT_REWORK		= 0,  
											@P_NO_OPOUT_PO		= @V_NO_OPOUT_PO,  
											@P_NO_OPOUT_PO_LINE = @V_NO_OPOUT_PO_LINE,  
											@P_YN_SUBCON		= @V_YN_SUBCON,  
											@P_TM_READ			= '000000',  
											@P_TM_WO_WAIT		= '000000',  
											@P_TM_WORK			= @V_TM_WORK,  
											@P_TM_MOVE			= '000000',  
											@P_TM_WAIT			= '000000',  
											@P_TM_WO_S			= '000000',  
											@P_TM_WO_E			= '000000',  
											@P_TM_WO_T			= @P_TM_WO_T,  
											@P_QT_WO_ROLL		= @P_QT_WO_ROLL,  
											@P_CD_EQUIP			= NULL,  
											@P_TM_EQ_S			= '000000',  
											@P_TM_EQ_E			= '000000',  
											@P_TM_EQ_T			= @P_TM_EQ_T,  
											@P_QT_EQ_ROLL		= 0,  
											@P_DC_RMK			= NULL,  
											@P_ID_INSERT		= 'MES_SCHEDULER'  

		IF (@@ERROR <> 0)  
		BEGIN  
			SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_WORK_MANDAY_SUB_INSERT작업을정상적으로처리하지못했습니다.' 
			GOTO ERROR  
		END  
	END  
	--  
	PRINT '<5.2 YN_MANDAY가 ''Y''인 건은 작업공수 INSERT> END'  
	--  
END  
--
PRINT '<5 작업실적 생성> END'
--

--
PRINT '<6 불량수량이 있으면 불량내역, 불량실적, 불량입고의뢰, 불량입고 처리> START'
--
IF (@P_QT_REJECT > 0)
BEGIN
	--
	PRINT '<6.1 유효성 체크> START'
	--
	IF (ISNULL(@P_CD_REJECT, '') = '')
	BEGIN
		SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]불량코드(PR_LINK_MES.CD_REJECT)는 필수값 입니다.' 
		GOTO ERROR
	END
	--
	PRINT '<6.1 유효성 체크> END'
	--
	
	--
	PRINT '<6.2 불량종류코드 또는 불량원인코드가 있으면 불량내역 INSERT> START'
	--
	EXEC UP_PR_WORKL_INSERT @P_CD_COMPANY  = @P_CD_COMPANY,
							@P_NO_WO       = @P_NO_WO,
							@P_NO_WORK     = @V_NO_WORK,
							@P_NO_LINE     = 1,
							@P_CD_REJECT   = @P_CD_REJECT,
							@P_CD_RESOURCE = @P_CD_RESOURCE,
							@P_TM_WORK     = NULL,
							@P_QT_WORK     = @P_QT_WORK,
							@P_QT_REJECT   = @P_QT_REJECT,
							@P_DC_RMK      = NULL,
							@P_NO_WOLINE   = @V_NO_WOLINE,
							@P_NO_SFT      = @P_NO_SFT,
							@P_ID_INSERT   = 'MES_SCHEDULER',
							@P_CD_SL_BAD   = @P_CD_SL_BAD_IN,
							@P_NO_REL      = NULL
							
	IF (@@ERROR <> 0)
	BEGIN
		SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_WORKL_INSERT작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR
	END
	--
	PRINT '<6.2 불량종류코드 또는 불량원인코드가 있으면 불량내역 INSERT> END'
END

IF (@P_QT_BAD > 0)
BEGIN
	--
	PRINT '<6.3 불량실적 필수값 가져오기> START'
	--	
	SELECT	@V_YN_AUTOBAD		= YN_AUTOBAD,
			@V_FG_AUTOBAD		= FG_AUTOBAD,
			@V_YN_AUTOBAD_REQ	= YN_AUTOBAD_REQ,
			@V_YN_AUTOBAD_RCV	= YN_AUTOBAD_RCV
	FROM	PR_CFG_PLANT
	WHERE	CD_COMPANY	= @P_CD_COMPANY
	AND		CD_PLANT	= @P_CD_PLANT
	--
	PRINT '<6.3 불량실적 필수값 가져오기> END'
	--
	
	--
	PRINT '<6.4 유효성 체크> START'
	--
	IF (ISNULL(@P_CD_SL_BAD_IN, '') = '')
	BEGIN
		SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]불량입고창고(PR_LINK_MES.CD_SL_BAD_IN)는 필수값 입니다.' 
		GOTO ERROR
	END
	
	/*
	IF (ISNULL(@V_YN_AUTOBAD, 'N') = 'N')
	BEGIN
		SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]공장환경설정에자동공정불량처리여부가설정되어있지않습니다.작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR
	END

	IF (ISNULL(@V_FG_AUTOBAD, '000') <> '001')
	BEGIN
		SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]공장환경설정에자동공정불량처리내역값이불량내역등록으로설정되어있지않습니다.작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR
	END
	
	IF (ISNULL(@V_YN_AUTOBAD_REQ, 'N') = 'N')
	BEGIN
		SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]공장환경설정에공정불량자동입고의뢰여부가설정되어있지않습니다.작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR
	END
	
	IF (ISNULL(@V_YN_AUTOBAD_RCV, 'N') = 'N')
	BEGIN
		SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]공장환경설정에공정불량자동입고처리여부가설정되어있지않습니다.작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR
	END
	*/
	--
	PRINT '<6.4 유효성 체크> END'
	--
	
	--
	PRINT '<6.5 불량실적 생성> START'
	--
	BEGIN
		--
		PRINT '<6.5.1 불량실적번호 채번> START'
		--
		BEGIN
			EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
						  @P_CD_MODULE  = 'PR',
						  @P_CD_CLASS   = '05',
						  @P_DOCU_YM    = @V_DOCU_YM,
						  @P_NO         = @V_NO_WORK_BAD OUTPUT

			IF (@@ERROR <> 0)
			BEGIN
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
							   + 'CD_MODULE = ''PR'', '
							   + 'CD_CLASS = ''05'' '
							   + '작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR
			END
		END
		--
		PRINT '<6.5.1 불량실적번호 채번> END'
		--
		
		--
		PRINT '<6.5.2 불량품 이동출고, 이동입고 번호를 채번한다.> START'
		--
		BEGIN
			EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
						  @P_CD_MODULE  = 'PR',
						  @P_CD_CLASS   = '06',
						  @P_DOCU_YM    = @V_DOCU_YM,
						  @P_NO         = @V_NO_IO_BAD_202_102 OUTPUT

			IF (@@ERROR <> 0)
			BEGIN
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
							   + 'CD_MODULE = ''PR'', '
							   + 'CD_CLASS = ''06'' '
							   + '작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR
			END
		END
		--
		PRINT '<6.5.2 불량품 이동출고, 이동입고 번호를 채번한다.> END'
		--
		
		--
		PRINT '<6.5.3 불량품 제품출고 번호를 채번한다.> START'
		--
		BEGIN
			EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
						  @P_CD_MODULE  = 'PR',
						  @P_CD_CLASS   = '13',
						  @P_DOCU_YM    = @V_DOCU_YM,
						  @P_NO         = @V_NO_IO_BAD_203 OUTPUT

			IF (@@ERROR <> 0)
			BEGIN
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
							   + 'CD_MODULE = ''PR'', '
							   + 'CD_CLASS = ''13'' '
							   + '작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR
			END
		END
		--
		PRINT '<6.5.3 불량품 제품출고 번호를 채번한다.> END'
		--
		
		--
		PRINT '<6.5.4 불량실적 INSERT> START'
		--
		BEGIN
			EXEC UP_PR_BADWORK_REG_INSERT @P_CD_COMPANY	    = @P_CD_COMPANY,
										  @P_NO_WORK		= @V_NO_WORK_BAD,
										  @P_NO_WO		    = @P_NO_WO,
										  @P_CD_PLANT		= @P_CD_PLANT,
										  @P_CD_OP		    = @P_CD_OP,
										  @P_CD_ITEM		= @P_CD_ITEM,
										  @P_NO_EMP		    = @P_NO_EMP,
										  @P_DT_WORK		= @P_DT_WORK,
										  @P_QT_WORK		= @P_QT_BAD,
										  @P_QT_REJECT	    = 0.0,
										  @P_CD_REJECT	    = NULL,
										  @P_QT_MOVE		= 0.0,
										  @P_YN_REWORK	    = 'N',
										  @P_YN_BADWORK	    = 'Y',
										  @P_TM_LABOR		= '0',
										  @P_CD_RSRC_LABOR  = '',
										  @P_TM_MACH		= '0',
										  @P_CD_RSRC_MACH	= '',
										  @P_CD_WC		    = @P_CD_WC,
										  @P_DC_REJECT	    = '',
										  @P_ID_INSERT	    = 'MES_SCHEDULER',
										  @P_FG_MOVE		= 'Y',
										  @P_TP_REWORK	    = NULL,
										  @P_CD_REWORK	    = NULL,
										  @P_NO_LOT		    = @P_NO_LOT,
										  @P_NO_SFT		    = @P_NO_SFT,
										  @P_CD_EQUIP		= @P_CD_EQUIP,
										  @P_NO_IO_202_102  = @V_NO_IO_BAD_202_102,
										  @P_NO_IO_203	    = @V_NO_IO_BAD_203,
										  @P_NO_IO_LINE_202 = 1,
										  @P_NO_IO_LINE_102 = 2,
										  @P_NO_IO_LINE_203 = 1,
										  @P_YN_SUBCON      = @V_YN_SUBCON,
										  @P_NO_PO			= @V_NO_OPOUT_PO,
										  @P_NO_POLINE		= @V_NO_OPOUT_PO_LINE
			
			IF (@@ERROR <> 0)
			BEGIN
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_BADWORK_REG_INSERT작업을정상적으로처리하지못했습니다.'
				GOTO ERROR
			END
		END
		--
		PRINT '<6.5.4 불량실적 INSERT> END'
		--
		
		--
		PRINT '<6.5.5 불량처리 INSERT> START'
		--
		BEGIN
			EXEC UP_PR_WORK_BAD_INSERT @P_CD_COMPANY  = @P_CD_COMPANY,
									   @P_NO_WORK     = @V_NO_WORK_BAD,
									   @P_NO_WORK_SRC = @V_NO_WORK,
									   @P_NO_LINE_SRC = 1,
									   @P_NO_WO       = @P_NO_WO,
									   @P_QT_BAD      = @P_QT_BAD,
									   @P_ID_INSERT   = 'MES_SCHEDULER'
			
			IF (@@ERROR <> 0)
			BEGIN
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_WORK_BAD_INSERT작업을정상적으로처리하지못했습니다.'
				GOTO ERROR
			END
		END
		--
		PRINT '<6.5.5 불량처리 INSERT> END'
		--
		
		--
		PRINT '<6.5.6 불량입고의뢰, 불량입고 생성> START'
		-- 불량입고의뢰 프로시저에서 자동으로 불량입고처리까지 한다.
		--
		IF (ISNULL(@V_YN_AUTOBAD_REQ, 'N') = 'Y')
		BEGIN
			--
			PRINT '<6.5.6.1 불량입고의뢰 번호 채번> START'
			--
			BEGIN
				EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
							  @P_CD_MODULE  = 'PR',
							  @P_CD_CLASS   = '06',
							  @P_DOCU_YM    = @V_DOCU_YM,
							  @P_NO         = @V_NO_REQ_BAD OUTPUT

				IF (@@ERROR <> 0)
				BEGIN
					SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
								   + 'CD_MODULE = ''PR'', '
								   + 'CD_CLASS = ''06'' '
								   + '작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR
				END
			END
			--
			PRINT '<6.5.6.1 불량입고의뢰 번호 채번> END'
			--
			
			--
			PRINT '<6.5.6.2 불량입고번호 채번> START'
			--
			IF (ISNULL(@V_YN_AUTOBAD_RCV, 'N') = 'Y')
			BEGIN
				EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
							  @P_CD_MODULE  = 'PU',
							  @P_CD_CLASS   = '18',
							  @P_DOCU_YM    = @V_DOCU_YM,
							  @P_NO         = @V_NO_IO_BAD OUTPUT

				IF (@@ERROR <> 0)
				BEGIN
					SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
								   + 'CD_MODULE = ''PU'', '
								   + 'CD_CLASS = ''18'' '
								   + '작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR
				END
			END
			--
			PRINT '<6.5.6.2 불량입고번호 채번> START'
			--
			
			--
			PRINT '<6.5.6.3 불량입고의뢰헤더 INSERT> START'
			--
			BEGIN
				EXEC UP_PR_WORK_BAD_AUTO_H_INSERT @P_CD_COMPANY		= @P_CD_COMPANY,
												  @P_CD_PLANT		= @P_CD_PLANT,
												  @P_NO_REQ			= @V_NO_REQ_BAD,
												  @P_DT_REQ			= @P_DT_WORK,
												  @P_NO_EMP			= @P_NO_EMP,
												  @P_DC_RMK       	= NULL,
												  @P_CD_DEPT		= @V_CD_DEPT,
												  @P_NO_IO			= @V_NO_IO_BAD,
												  @P_YN_AUTOBAD_RCV	= @V_YN_AUTOBAD_RCV,
												  @P_ID_INSERT		= 'MES_SCHEDULER'
				
				IF (@@ERROR <> 0)
				BEGIN
					SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_WORK_BAD_AUTO_H_INSERT작업을정상적으로처리하지못했습니다.'
					GOTO ERROR
				END
			END
			--
			PRINT '<6.5.6.3 불량입고의뢰헤더 INSERT> END'
			--
			
			--
			PRINT '<6.5.6.4 불량입고의뢰라인 INSERT> START'
			--
			BEGIN
				EXEC UP_PR_WORK_BAD_AUTO_L_INSERT @P_CD_COMPANY		= @P_CD_COMPANY,
												  @P_CD_PLANT		= @P_CD_PLANT,
												  @P_NO_REQ			= @V_NO_REQ_BAD,
												  @P_NO_LINE		= 1,
												  @P_CD_WC			= @P_CD_WC,
												  @P_CD_ITEM		= @P_CD_ITEM,
												  @P_DT_REQ			= @P_DT_WORK,
												  @P_QT_REQ			= @P_QT_BAD,
												  @P_QT_REQ_W		= @P_QT_BAD,
												  @P_QT_REQ_B		= 0.0,
												  @P_YN_QC			= 'N',
												  @P_QT_RCV			= 0.0,
												  @P_CD_SL			= @P_CD_SL_BAD_IN,
												  @P_NO_WO			= @P_NO_WO,
												  @P_NO_WORK		= @V_NO_WORK_BAD,
												  @P_TP_WB			= '0',
												  @P_TP_GR			= '981',
												  @P_NO_IO			= @V_NO_IO_BAD,
												  @P_YN_AUTOBAD_RCV	= @V_YN_AUTOBAD_RCV,
												  @P_NO_EMP			= @P_NO_EMP,
												  @P_NO_LOT			= @P_NO_LOT
				
				IF (@@ERROR <> 0)
				BEGIN
					SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_WORK_BAD_AUTO_L_INSERT작업을정상적으로처리하지못했습니다.'
					GOTO ERROR
				END
			END
			--
			PRINT '<6.5.6.4 불량입고의뢰라인 INSERT> END'
			--
		END
		--
		PRINT '<6.5.6 불량입고의뢰, 불량입고 생성> END'
		--
	END
	--
	PRINT '<6.5 불량실적 생성> END'
	--
END
--
PRINT '<6 불량수량이 있으면 불량내역, 불량실적, 불량입고의뢰, 불량입고 처리> END'
--

--
PRINT '<7 소요자재 출고 투입, 출고 처리> START'
--
IF (@V_YN_REQ = 'Y')
BEGIN  
	--
	PRINT '<7.1 자재청구번호 채번> START'
	--
	BEGIN
		EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
					  @P_CD_MODULE  = 'PR',
					  @P_CD_CLASS   = '08',
					  @P_DOCU_YM    = @V_DOCU_YM,
					  @P_NO         = @V_NO_REQ OUTPUT

		IF (@@ERROR <> 0)
		BEGIN
			SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
						   + 'CD_MODULE = ''PR'', '
						   + 'CD_CLASS = ''08'' 작업을정상적으로처리하지못했습니다.'
			GOTO ERROR
		END
	END
	--
	PRINT '<7.1 자재청구번호 채번> END'
	--

	--
	PRINT '<7.2 자재투입번호 채번> START'
	--
	BEGIN
		EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
					  @P_CD_MODULE  = 'PR',
					  @P_CD_CLASS   = '04',
					  @P_DOCU_YM    = @V_DOCU_YM,
					  @P_NO         = @V_NO_II OUTPUT
		              
		IF (@@ERROR <> 0)
		BEGIN
			SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
						   + 'CD_MODULE = ''PR'', '
						   + 'CD_CLASS = ''04'' 작업을정상적으로처리하지못했습니다.' 
			GOTO ERROR
		END
	END
	--
	PRINT '<7.2 자재투입번호 채번> END'
	--
	    
	--
	PRINT '<7.3 생산출고번호 채번> START'
	--
	BEGIN
		EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
					  @P_CD_MODULE  = 'PU',
					  @P_CD_CLASS   = '17',
					  @P_DOCU_YM    = @V_DOCU_YM,
					  @P_NO         = @V_NO_IO_MM OUTPUT

		IF (@@ERROR <> 0)
		BEGIN
			SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
						   + 'CD_MODULE = ''PU'', '
						   + 'CD_CLASS = ''17'' 작업을정상적으로처리하지못했습니다.' GOTO ERROR
		END
	END
	--
	PRINT '<7.3 생산출고번호 채번> END'
	--
	
	--  
	PRINT '<7.4 작업지시의 소요자재 중 출고수량이 필요수량보다 적은 건을 찾는 커서> START'
	--  
	DECLARE CUR_WO_MATL_INSERT CURSOR FOR  

		SELECT	B.NO_LINE,
				B.CD_MATL,
				B.CD_SL_OT,
				ISNULL(B.YN_BF, 'N') AS YN_BF,
				B.FG_GIR,
				ISNULL(P.FG_SERNO, '') AS FG_SERNO,
				CASE WHEN @P_QT_WORK = W.QT_ITEM THEN ISNULL(B.QT_NEED, 0) ELSE ISNULL(B.QT_NEED_NET, 0) * @P_QT_WORK END AS QT_INPUT,
				ISNULL(B.QT_NEED, 0) - ISNULL(I.QT_II, 0) AS QT_REMS
		FROM	PR_WO_BILL B
		INNER JOIN PR_WO W ON W.CD_COMPANY = B.CD_COMPANY AND W.CD_PLANT = B.CD_PLANT AND W.NO_WO = B.NO_WO
		INNER JOIN MA_PITEM P ON P.CD_COMPANY = B.CD_COMPANY AND P.CD_PLANT = B.CD_PLANT AND P.CD_ITEM = B.CD_MATL
		LEFT OUTER JOIN (
			SELECT	CD_MATL, NO_LINE, 
					SUM(QT_II) AS QT_II
			FROM	PR_II
			WHERE	CD_COMPANY	= @P_CD_COMPANY
			AND		CD_PLANT	= @P_CD_PLANT
			AND		NO_WO		= @P_NO_WO
			AND		CD_WC		= @P_CD_WC
			AND		CD_OP		= @P_CD_OP
			AND		CD_WCOP		= @P_CD_WCOP
			GROUP BY CD_MATL, NO_LINE
		) I ON I.CD_MATL = B.CD_MATL AND I.NO_LINE = B.NO_LINE
		WHERE	B.CD_COMPANY	= @P_CD_COMPANY
		AND		B.CD_PLANT		= @P_CD_PLANT
		AND		B.NO_WO			= @P_NO_WO
		AND		B.CD_WC			= @P_CD_WC
		AND		B.CD_OP			= @P_CD_OP
		AND		B.CD_WCOP		= @P_CD_WCOP
		--AND	B.FG_GIR		= 'Y'
		--AND		ISNULL(P.FG_SERNO, '') <> '002'
		ORDER BY B.NO_LINE

	OPEN CUR_WO_MATL_INSERT  

		FETCH NEXT FROM CUR_WO_MATL_INSERT  

		INTO	@V_NO_LINE,  
				@V_CD_MATL,
				@V_CD_SL_OT,
				@V_YN_BF,
				@V_FG_GIR,
				@V_FG_SERNO,
				@V_QT_INPUT,  
				@V_QT_REMS


	WHILE (@@FETCH_STATUS = 0)  
	BEGIN  
		--
		PRINT '<7.4.1 투입 및 출고되야하는 수량 계산> START'
		--
		
		--SELECT @V_QT_INPUT_REAL = @V_QT_REMS --실제 투입될수량 = 소요잔량
		
		--실제 투입될수량 = 소요잔량 < 투입수량 ? 소요잔량 : 투입수량
		IF (@V_QT_REMS < @V_QT_INPUT)
			SELECT @V_QT_INPUT_REAL = @V_QT_REMS
		ELSE 
			SELECT @V_QT_INPUT_REAL = @V_QT_INPUT
		
		IF (@V_YN_QT_WORK = 'Y')
			SELECT @V_QT_INPUT_REAL = @V_QT_INPUT
		
		IF (ISNULL(@V_QT_INPUT_REAL, 0) <= 0.0)
		BEGIN
			FETCH NEXT FROM CUR_WO_MATL_INSERT  

			INTO	@V_NO_LINE,  
					@V_CD_MATL,
					@V_CD_SL_OT,
					@V_YN_BF,
					@V_FG_GIR,
					@V_FG_SERNO,
					@V_QT_INPUT,  
					@V_QT_REMS
					
			CONTINUE
		END
		
		IF (ISNULL(@V_FG_SERNO, '') = '003')
		BEGIN
			FETCH NEXT FROM CUR_WO_MATL_INSERT  

			INTO	@V_NO_LINE,  
					@V_CD_MATL,
					@V_CD_SL_OT,
					@V_YN_BF,
					@V_FG_GIR,
					@V_FG_SERNO,
					@V_QT_INPUT,  
					@V_QT_REMS
					
			CONTINUE
		END
		--
		PRINT '<7.4.1 소요량 및 단위소요량 계산> END'
		--
		
		--
		PRINT '<7.4.2 유효성 체크> START'
		--
		IF ((@V_YN_BF = 'Y' OR (@V_YN_BF <> 'Y' AND @V_YN_REQ_ALL_BF = 'Y')) AND (@V_CD_SL_OT IS NULL OR @V_CD_SL_OT = ''))
		BEGIN  
			CLOSE CUR_WO_MATL_INSERT  
			DEALLOCATE CUR_WO_MATL_INSERT  

			SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I] 작업지시번호[' + @P_NO_WO + '], 소요자재[' + @V_CD_MATL + ']의 출고창고가 없습니다.' 
			GOTO ERROR  
		END  
		--
		PRINT '<7.4.2 유효성 체크> END'
		--

		--
		PRINT '<7.4.3 생산수불의 자재입고번호를 채번> START'
		--
		BEGIN
			EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
						  @P_CD_MODULE  = 'PR',
						  @P_CD_CLASS   = '11',
						  @P_DOCU_YM    = @V_DOCU_YM,
						  @P_NO         = @V_NO_IO_101 OUTPUT

			IF (@@ERROR <> 0)
			BEGIN             
				CLOSE CUR_WO_MATL_INSERT  
				DEALLOCATE CUR_WO_MATL_INSERT  
				
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]CP_GETNO '
							   + 'CD_MODULE = ''PR'', '
							   + 'CD_CLASS = ''11'' 작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR
			END
		END
		--
		PRINT '<7.4.3 생산수불의 자재입고번호를 채번> END'
		--
		
		--
		PRINT '<7.4.4 작업실적 자재투입처리> START'
		--
		BEGIN
			EXEC UP_PR_WORK_MATL_INSERT @P_CD_COMPANY = @P_CD_COMPANY,
										@P_CD_PLANT   = @P_CD_PLANT,
										@P_NO_WO      = @P_NO_WO,
										@P_NO_WORK    = @V_NO_WORK,
										@P_CD_OP      = @P_CD_OP,
										@P_CD_WC      = @P_CD_WC,
										@P_CD_WCOP    = @P_CD_WCOP,
										@P_DT_WORK    = @P_DT_WORK,
										@P_CD_MATL    = @V_CD_MATL,
										@P_QT_WORK    = @V_QT_INPUT_REAL,
										@P_NO_IO_201  = '',
										@P_NO_IO_101  = @V_NO_IO_101,
										@P_NO_IO      = @V_NO_IO_MM,
										@P_NO_LINE    = @V_NO_LINE,
										@P_NO_REQ     = @V_NO_REQ,
										@P_CD_DEPT    = @V_CD_DEPT,
										@P_NO_EMP     = @P_NO_EMP,
										@P_CD_SL      = @V_CD_SL_OT,
										@P_NO_II      = @V_NO_II,
										@P_ID_INSERT  = 'MES_SCHEDULER'
										
			IF (@@ERROR <> 0)
			BEGIN
				CLOSE CUR_WO_MATL_INSERT  
				DEALLOCATE CUR_WO_MATL_INSERT  
				
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_WORK_MATL_INSERT작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR
			END
		END
		--
		PRINT '<7.4.4 작업실적 자재투입처리> END'
		--
		
		--
		PRINT '<7.4.5 작업실적 B/F항목이 아닌건에 대한 자재출고처리> START'
		-- B/F건에 대해서는 자재투입에서 출고처리가 이루어짐
		--
		IF (@V_YN_BF <> 'Y' AND @V_YN_REQ_ALL_BF = 'Y')
		BEGIN
			--
			PRINT '<7.4.5.1 자재청구생성> START'
			--
			IF (NOT EXISTS (SELECT 1 FROM PR_REQH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_REQ = @V_NO_REQ))
			BEGIN
				EXEC UP_PR_REQH_INSERT	@P_CD_COMPANY	= @P_CD_COMPANY,
										@P_NO_REQ		= @V_NO_REQ,
										@P_CD_PLANT		= @P_CD_PLANT,
										@P_NO_WO		= @P_NO_WO,
										@P_CD_DEPT		= @V_CD_DEPT,
										@P_NO_EMP		= @P_NO_EMP,
										@P_DT_REQ		= @P_DT_WORK,
										@P_TP_REQ		= '1',
										@P_TP_GI		= @V_TP_GI,
										@P_DC_RMK		= '',
										@P_FG_PRQ		= '',
										@P_ID_INSERT	= 'MES_SCHEDULER'
										
										
				
				IF (@@ERROR <> 0)
				BEGIN
					CLOSE CUR_WO_MATL_INSERT  
					DEALLOCATE CUR_WO_MATL_INSERT  
					
					SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_REQH_INSERT작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR
				END
			END
			
			SELECT @V_NO_REQ_LINE = ISNULL((SELECT MAX(NO_LINE) FROM PR_REQL WHERE CD_COMPANY = @P_CD_COMPANY AND NO_REQ = @V_NO_REQ), 0) + 1
			
			EXEC UP_PR_REQL_INSERT	@P_CD_COMPANY	= @P_CD_COMPANY,
									@P_NO_REQ		= @V_NO_REQ,
									@P_NO_LINE		= @V_NO_REQ_LINE,
									@P_NO_WO		= @P_NO_WO,
									@P_CD_PLANT		= @P_CD_PLANT,
									@P_CD_MATL		= @V_CD_MATL,
									@P_CD_OP		= @P_CD_OP,
									@P_CD_WC		= @P_CD_WC,
									@P_QT_REQ		= @V_QT_INPUT_REAL,
									@P_QT_ISU		= @V_QT_INPUT_REAL,
									@P_QT_USE		= @V_QT_INPUT_REAL,
									@P_NO_LINE_WO	= @V_NO_LINE,
									@P_CD_WCOP		= @P_CD_WCOP,
									@P_QT_RETURN	= 0.0,
									@P_QT_REQ_RETURN= 0.0,
									@P_FG_GIR		= @V_FG_GIR,
									@P_TP_GI		= @V_TP_GI,
									@P_YN_BF		= @V_YN_BF,
									@P_DC_RMK		= '',
									@P_YN_INSPECT	= 'N',
									@P_CD_SL		= @V_CD_SL_OT,
									@P_ID_INSERT	= 'MES_SCHEDULER'
			
			IF (@@ERROR <> 0)
			BEGIN
				CLOSE CUR_WO_MATL_INSERT  
				DEALLOCATE CUR_WO_MATL_INSERT  
				
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_REQH_INSERT작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR
			END
			--
			PRINT '<7.4.5.1 자재청구생성> END'
			--
			
			--
			PRINT '<7.4.5.2 생산출고생성> START'
			--
			IF (NOT EXISTS (SELECT 1 FROM MM_QTIOH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IO = @V_NO_IO_MM))
			BEGIN
				EXEC UP_PU_MM_QTIOH_INSERT	@P_NO_IO		= @V_NO_IO_MM, 
											@P_CD_COMPANY	= @P_CD_COMPANY, 
											@P_CD_PLANT		= @P_CD_PLANT,       
											@P_CD_PARTNER	= '',  
											@P_FG_TRANS		= '001',   
											@P_YN_RETURN	= 'N',     
											@P_DT_IO		= @P_DT_WORK, 
											@P_GI_PARTNER	= '',    
											@P_CD_DEPT		= @V_CD_DEPT,     
											@P_NO_EMP		= @P_NO_EMP, 
											@P_DC_RMK		= '',     
											@P_ID_INSERT	= 'MES_SCHEDULER'
				
				IF (@@ERROR <> 0)
				BEGIN
					CLOSE CUR_WO_MATL_INSERT  
					DEALLOCATE CUR_WO_MATL_INSERT  
					
					SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PU_MM_QTIOH_INSERT작업을정상적으로처리하지못했습니다.' 
					GOTO ERROR
				END
			END
			
			SELECT @V_NO_IO_MM_LINE = ISNULL((SELECT MAX(NO_IOLINE) FROM MM_QTIO WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IO = @V_NO_IO_MM), 0) + 1
			
			EXEC UP_PU_MM_QTIO_INSERT @P_YN_RETURN      = 'N',			 @P_NO_IO          = @V_NO_IO_MM,	@P_NO_IOLINE       = @V_NO_IO_MM_LINE,
									  @P_CD_COMPANY     = @P_CD_COMPANY, @P_CD_PLANT       = @P_CD_PLANT,	@P_CD_SL           = @V_CD_SL_OT,
									  @P_CD_SECTION     = '',			 @P_CD_BIN         = '',			@P_DT_IO           = @P_DT_WORK,
									  @P_NO_ISURCV      = @V_NO_REQ,	 @P_NO_ISURCVLINE  = @V_NO_REQ_LINE,@P_NO_PSO_MGMT     = @P_NO_WO,
									  @P_FG_PS          = '2',			 @P_YN_PURSALE     = 'N',			@P_FG_IO           = '011',
									  @P_CD_QTIOTP      = @V_TP_GI,		 @P_FG_TRANS       = '001',			@P_CD_ITEM         = @V_CD_MATL,
									  @P_QT_IO          = @V_QT_INPUT_REAL,@P_QT_GOOD_INV  = @V_QT_INPUT_REAL,@P_YN_AM         = @V_YN_AM, --'Y',
									  @P_NO_EMP	        = @P_NO_EMP,	 @P_CD_WC          = @P_CD_WC,		@P_CD_OP           = @P_CD_OP,
									  @P_CD_WCOP        = @P_CD_WCOP,	 @P_VAT_CLS        = 0,				@P_NO_PSOLINE_MGMT = @V_NO_LINE,
									  @P_FG_TPIO        = '',			 @P_FG_TAX         = '',			@P_CD_PARTNER      = '',
									  @P_QT_RETURN      = 0,			 @P_QT_TRANS_INV   = 0,				@P_QT_INSP_INV     = 0,
									  @P_QT_REJECT_INV  = 0,			 @P_CD_EXCH        = '',			@P_RT_EXCH         = 0,
									  @P_UM_EX          = 0,			 @P_AM_EX          = 0,				@P_UM              = 0,
									  @P_AM             = 0,			 @P_QT_CLS         = 0,				@P_AM_CLS          = 0,
									  @P_VAT            = 0,			 @P_FG_TAXP        = '',			@P_UM_STOCK        = 0,
									  @P_UM_EVAL        = 0,			 @P_CD_PJT         = @P_NO_PJT,		@P_AM_DISTRIBU     = 0,
									  @P_RT_CUSTOMS     = 0,			 @P_AM_CUSTOMS     = 0,				@P_NO_LC           = '',
									  @P_NO_LCLINE      = 0,			 @P_QT_IMSEAL      = 0,				@P_QT_EXLC         = 0,
									  @P_GI_PARTNER     = '',			 @P_CD_GROUP       = '',			@P_NO_IO_MGMT      = @V_NO_IO_101,
									  @P_NO_IOLINE_MGMT = @V_NO_LINE,	 @P_CD_BIZAREA_RCV = '',			@P_CD_PLANT_RCV    = '',
									  @P_CD_SL_REF      = '',			 @P_CD_SECTION_REF = '',            @P_CD_BIN_REF      = '33',
									  @P_BILL_PARTNER   = '',			 @P_CD_UNIT_MM     = '',			@P_QT_UNIT_MM      = @V_QT_INPUT_REAL,
									  @P_UM_EX_PSO      = 0,			 @P_QT_CLS_MM      = 0,				@P_QT_RETURN_MM    = 0,
									  @P_NO_WORK        = @V_NO_WORK,	 @P_SEQ_PROJECT    = @P_SEQ_PROJECT,@P_CD_WORKITEM	   = @P_CD_ITEM,     
									  @P_ID_INSERT		= 'MES_SCHEDULER'
			
			IF (@@ERROR <> 0)
			BEGIN
				CLOSE CUR_WO_MATL_INSERT  
				DEALLOCATE CUR_WO_MATL_INSERT  
				
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_REQH_INSERT작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR
			END
			
			UPDATE	PR_WO_BILL
			SET		QT_ISU		= ISNULL(QT_ISU, 0) + ISNULL(@V_QT_INPUT_REAL, 0)
			WHERE	CD_COMPANY	= @P_CD_COMPANY
			AND		NO_WO		= @P_NO_WO
			AND		NO_LINE		= @V_NO_LINE	
					
			IF (@@ERROR <> 0)
			BEGIN
				CLOSE CUR_WO_MATL_INSERT  
				DEALLOCATE CUR_WO_MATL_INSERT  
				
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]PR_WO_BILL.QT_ISU UPDATE 작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR
			END
			--
			PRINT '<7.4.5.2 생산출고생성> END'
			--
		END
		--
		PRINT '<7.4.5 작업실적 B/F항목이 아닌건에 대한 자재출고처리> END'
		--
		
		--
		PRINT '<7.4.6 LOT출고항목 선입선출처리> START'
		--
		IF (@V_FG_SERNO = '002' AND (@V_YN_BF = 'Y' OR (@V_YN_BF <> 'Y' AND @V_YN_REQ_ALL_BF = 'Y')))
		BEGIN
			SET @V_NO_IO_MM_LINE = CASE WHEN @V_YN_BF = 'Y' THEN @V_NO_LINE ELSE @V_NO_IO_MM_LINE END
			EXEC UP_CZ_PR_MES_MATL_LOT_FIFO_I
					@P_CD_COMPANY	= @P_CD_COMPANY,
					@P_NO_IO		= @V_NO_IO_MM,
					@P_NO_IOLINE	= @V_NO_IO_MM_LINE,
					@P_CD_ITEM		= @V_CD_MATL,
					@P_QT_IO		= @V_QT_INPUT_REAL,
					@P_DT_IO		= @P_DT_WORK,
					@P_CD_SL		= @V_CD_SL_OT,
					@P_NO_WO		= @P_NO_WO,
					@P_YN_RETURN	= 'N'

            PRINT '1'
										
			IF (@@ERROR <> 0)
			BEGIN
				CLOSE CUR_WO_MATL_INSERT
				DEALLOCATE CUR_WO_MATL_INSERT
				
				SELECT @ERRMSG = '[UP_PR_LINK_MES_REG_WORK_I]UP_PR_MES_MATL_LOT_FIFO_I SP CALL ERROR.' 
				GOTO ERROR
			END
		END
		--
		PRINT '<7.4.6 LOT출고항목 선입선출처리> END'
		--
		
		FETCH NEXT FROM CUR_WO_MATL_INSERT  

		INTO	@V_NO_LINE,  
				@V_CD_MATL,
				@V_CD_SL_OT,
				@V_YN_BF,
				@V_FG_GIR,
				@V_FG_SERNO,
				@V_QT_INPUT,  
				@V_QT_REMS
				
	END  
	CLOSE CUR_WO_MATL_INSERT  
	DEALLOCATE CUR_WO_MATL_INSERT  
	--  
	PRINT '<7.4 작업지시의 소요자재 중 출고수량이 필요수량보다 적은 건을 찾는 커서> END'
	--  
END
--
PRINT '<7 소요자재 출고 투입, 출고 처리> END'
--

PRINT '<LAST. 연동테이블 UPDATE>'
BEGIN
	UPDATE	NEOE.PR_LINK_MES
	SET		NO_WORK		= @V_NO_WORK,
			QT_MOVE		= @P_QT_MOVE,
			YN_WORK		= 'E',
			YN_MANDAY	= CASE WHEN @P_YN_MANDAY = 'Y' THEN 'E' ELSE YN_MANDAY END,
			NO_REQ		= CASE WHEN @V_YN_REQ = 'Y' THEN @V_NO_REQ ELSE NO_REQ END,
			NO_IO_BILL	= CASE WHEN @V_YN_REQ = 'Y' THEN @V_NO_IO_MM ELSE NO_IO_BILL END,
			NO_WORK_BAD = @V_NO_WORK_BAD
	WHERE	CD_COMPANY	= @P_CD_COMPANY
	AND		CD_PLANT	= @P_CD_PLANT
	AND		NO_MES		= @P_NO_MES
END

PRINT '4. 작업실적생성 END'


RETURN
ERROR: RAISERROR (@ERRMSG, 18, 1)
GO
