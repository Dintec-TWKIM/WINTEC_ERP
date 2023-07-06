USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WO_SIMULATION_I]    Script Date: 2020-06-30 오후 7:14:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_SIMULATION_I]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
	@P_PATN_ROUT		NVARCHAR(3),
	@P_CD_ITEM			NVARCHAR(50),
	@P_DT_WORK			NVARCHAR(8),
	@P_QT_WORK			NUMERIC(17, 4),
	@P_NO_EMP			NVARCHAR(10),
	@P_NO_SO		    NVARCHAR(20),
	@P_NO_LINE_SO	    NUMERIC(5, 0),
	@P_ID_INSERT		NVARCHAR(15)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE
	@ERRMSG					NVARCHAR(255),
	@V_DOCU_YM				NVARCHAR(6),
	@V_QT_WORK				NUMERIC(17, 4)

SET	@V_DOCU_YM = SUBSTRING(NEOE.SF_SYSDATE(GETDATE()), 3, 4)

SET @V_QT_WORK = CEILING(@P_QT_WORK)

PRINT '1. 작업지시생성 START'

--
-- <0 작업지시 생성 변수선언> START
--
DECLARE
	@V_NO_WO				NVARCHAR(20),   -- 작업지시번호  
	@V_TP_WO				NVARCHAR(3),    -- 오더형태  
	@V_YN_QC				NCHAR(1),		-- 검사여부  
	@V_YN_BF				NCHAR(1),		-- B/F여부
	@V_TP_GI				NVARCHAR(3),    -- 출고형태  
	@V_TP_GR				NVARCHAR(3),    -- 입고형태  
	@V_FG_TPPURCHASE		NVARCHAR(3),    -- 공정외주매입형태  
	@V_YN_AUTOREQ			NCHAR(1),       -- 공장환경설정.지시자재자동청구
	@V_YN_AUTORCV			NCHAR(1),       -- 공장환경설정.생산품자동입고처리
	@V_YN_CTRL_QTIO			NCHAR(1),       -- 공장환경설정.잔량통제
	@V_YN_AUTOPO			NCHAR(1),       -- 공장환경설정.공정외주자동발주
	@V_YN_AUTO_CLS			NCHAR(1),       -- 공장환경설정.생산수량자동마감
	@V_YN_AUTORCV_REQ		NCHAR(1),       -- 공장환경설정.지시자재자동청구
	@V_TP_WC				NVARCHAR(3),	-- W/C정보등록 작업장구분
	@V_YN_SUBCON			NVARCHAR(1),	-- 공정외주구분
	
	@V_NO_LINE				NUMERIC(5, 0),  -- 항번	
	@V_CD_MATL				NVARCHAR(20),   -- 소요자재 코드  
	@V_QT_NEED				NUMERIC(17, 4), -- 소요자재 소요수량  
	@V_QT_NEED_NET			NUMERIC(19, 6), -- 소요자재 단위소요수량  
	@V_YN_BF_MATL			NCHAR(1),		-- 소요자재 B/F여부 
	@V_FG_GIR				NCHAR(1),		-- 소요자재 불출관리여부  
	@V_CD_SL_OT				NVARCHAR(7),    -- 소요자재 출고창고
	@V_CD_WC				NVARCHAR(7),	-- 작업장 코드
	@V_CD_OP				NVARCHAR(4),    -- OP 번호
	@V_CD_WCOP				NVARCHAR(4),    -- 공정 코드
	@V_CD_EQUIP				NVARCHAR(30),	-- 설비
	@V_YN_FINAL				NVARCHAR(1)		-- 최종실적여부
--
-- <0 작업지시 생성 변수선언> END
--

--
-- <1 작업지시 필수값 가져오기> START
--
SELECT TOP 1 @V_TP_WO = TP_WO FROM PR_TPWO WHERE CD_COMPANY = @P_CD_COMPANY

SELECT	@V_TP_GI         = TP_GI,  
		@V_TP_GR         = TP_GR,  
		@V_FG_TPPURCHASE = FG_TPPURCHASE  
FROM	PR_TPWO  
WHERE	CD_COMPANY	= @P_CD_COMPANY  
AND		TP_WO       = @V_TP_WO

IF (@V_TP_GI IS NULL OR @V_TP_GI = '') SET @V_TP_GI = '983'  
IF (@V_TP_GR IS NULL OR @V_TP_GR = '') SET @V_TP_GR = '981'  
IF (@V_FG_TPPURCHASE IS NULL OR @V_FG_TPPURCHASE = '') SET @V_FG_TPPURCHASE = '800'  

SELECT	@V_YN_AUTOREQ		= YN_AUTOREQ,  
		@V_YN_AUTORCV		= YN_AUTORCV,  
		@V_YN_CTRL_QTIO		= YN_CTRL_QTIO,  
		@V_YN_AUTOPO		= YN_AUTOPO,  
		@V_YN_AUTO_CLS		= YN_AUTO_CLS,  
		@V_YN_AUTORCV_REQ	= YN_AUTORCV_REQ
FROM	PR_CFG_PLANT
WHERE	CD_COMPANY	= @P_CD_COMPANY 
AND		CD_PLANT	= @P_CD_PLANT

--
-- <1 작업지시 필수값 가져오기> END
--

--
-- <2 작업지시번호 채번> START
--
BEGIN
	EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
				  @P_CD_MODULE  = 'CZ',
				  @P_CD_CLASS   = '01',
				  @P_DOCU_YM    = @V_DOCU_YM,
				  @P_NO         = @V_NO_WO OUTPUT

	IF (@@ERROR <> 0)
	BEGIN
		SELECT @ERRMSG = '[SP_CZ_PR_WO_SIMULATION_I]CP_GETNO '
					   + 'CD_MODULE = ''CZ'', '
					   + 'CD_CLASS = ''01'' '
					   + '작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR
	END
END
--
-- <2 작업지시번호 채번> END
--

--
-- <3 작업지시 INSERT> START
--
BEGIN
	EXEC UP_PR_WO_INSERT @P_CD_COMPANY     = @P_CD_COMPANY,
						 @P_NO_WO          = @V_NO_WO,
						 @P_CD_PLANT       = @P_CD_PLANT,
						 @P_NO_EMP         = @P_NO_EMP,
						 @P_CD_ITEM        = @P_CD_ITEM,
						 @P_QT_ITEM        = @V_QT_WORK,
						 @P_FG_WO          = '003',	--작업지시구분 : '비계획'
						 @P_ST_WO          = 'O',
						 @P_PATN_ROUT      = @P_PATN_ROUT,
						 @P_TP_ROUT        = @V_TP_WO,
						 @P_NO_LOT         = NULL,
						 @P_NO_SO          = @P_NO_SO,
						 @P_NO_PJT         = NULL,
						 @P_DT_REL         = @P_DT_WORK,
						 @P_DT_DUE         = @P_DT_WORK,
						 @P_QT_WORK        = 0,
						 @P_NO_LINE_SO     = @P_NO_LINE_SO,
						 @P_DT_CLOSE       = NULL,
						 @P_TP_GI          = @V_TP_GI,
						 @P_TP_GR          = @V_TP_GR,
						 @P_DT_RELEASE     = NULL,
						 @P_YN_AUTOREQ     = @V_YN_AUTOREQ,
						 @P_YN_AUTORCV     = @V_YN_AUTORCV,
						 @P_YN_CTRL_QTIO   = @V_YN_CTRL_QTIO,
						 @P_YN_AUTOPO      = @V_YN_AUTOPO,
						 @P_YN_AUTO_CLS    = @V_YN_AUTO_CLS,
						 @P_FG_TPPURCHASE  = @V_FG_TPPURCHASE,
						 @P_DC_RMK         = NULL,
						 @P_YN_AUTORCV_REQ = @V_YN_AUTORCV_REQ,
						 @P_QT_LOSS        = 0,
						 @P_CD_PARTNER     = NULL,
						 @P_NO_SOURCE      = NULL,
						 @P_NO_EMP_DESIGN  = NULL,
						 @P_DC_RMK_CONTENT = NULL,
						 @P_FG_WO_ST       = NULL,
						 @P_DC_REV         = NULL,
						 @P_ID_INSERT      = @P_ID_INSERT,
						 @P_DT_DESIGN      = NULL,
						 @P_DT_INSERT      = NULL,
						 @P_DC_RMK2		   = NULL,
						 @P_DC_RMK3		   = NULL,
						 @P_NO_FR		   = NULL,
						 @P_QT_BATCH_SIZE  = NULL,
						 @P_DT_LIMIT	   = NULL,
						 @P_CD_USERDEF1	   = NULL,
						 @P_CD_USERDEF2	   = NULL,
						 @P_CD_USERDEF3	   = NULL,
						 @P_CD_USERDEF4	   = NULL,
						 @P_NUM_USERDEF1   = 0.0,
						 @P_NUM_USERDEF2   = 0.0,
						 @P_CD_PACKUNIT	   = NULL,
						 @P_CD_USERDEF5	   = NULL,
						 @P_CD_SL		   = NULL,
						 @P_TXT_USERDEF1   = NULL,
						 @P_TXT_USERDEF2   = NULL,
						 @P_TXT_USERDEF3   = NULL,
						 @P_TXT_USERDEF4   = NULL,
						 @P_TXT_USERDEF5   = NULL
	IF (@@ERROR <> 0)
	BEGIN
		SELECT @ERRMSG = '[SP_CZ_PR_WO_SIMULATION_I]UP_PR_WO_INSERT작업을정상적으로처리하지못했습니다.' 
		GOTO ERROR
	END
END
--
-- <3 작업지시 INSERT> END
--

--  
-- <4 작업지시경로 생성> START  
--  
BEGIN 
    --  
	-- <4.0 공정경로에서 데이터 찾는 커서> START
	--  
	DECLARE CUR_WO_MATL_INSERT CURSOR FOR 
	
	SELECT CONVERT(NUMERIC(5, 0), ROW_NUMBER() OVER (ORDER BY RL.CD_OP ASC)) AS NO_LINE,
		   RL.CD_WC,
		   RL.CD_OP,
		   RL.CD_WCOP,
		   ISNULL(NULLIF(RL.YN_QC, ''), 'N') AS YN_QC, 
		   ISNULL(NULLIF(RL.YN_BF, ''), MI.FG_BF) AS YN_BF,
		   WC.TP_WC, 
		   (CASE ISNULL(WC.TP_WC,'') WHEN '002' THEN 'Y' ELSE 'N' END) AS YN_SUBCON,
		   RL.CD_EQUIP,
		   RL.YN_FINAL
	FROM PR_ROUT RH
	JOIN PR_ROUT_L RL ON RL.CD_COMPANY = RH.CD_COMPANY AND RL.CD_PLANT = RH.CD_PLANT AND RL.CD_ITEM = RH.CD_ITEM AND RL.NO_OPPATH = RH.NO_OPPATH AND RL.TP_OPPATH = RH.TP_OPPATH AND RL.YN_USE = 'Y'
	LEFT JOIN MA_WC WC ON WC.CD_COMPANY = RL.CD_COMPANY AND	WC.CD_PLANT = RL.CD_PLANT AND WC.CD_WC = RL.CD_WC
	LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = RH.CD_COMPANY AND MI.CD_PLANT = RH.CD_PLANT AND MI.CD_ITEM = RH.CD_ITEM
	WHERE RH.CD_COMPANY = @P_CD_COMPANY
	AND	RH.CD_PLANT = @P_CD_PLANT
	AND	RH.CD_ITEM = @P_CD_ITEM
	AND RH.NO_OPPATH = @P_PATN_ROUT

	OPEN CUR_WO_MATL_INSERT  
		FETCH NEXT FROM CUR_WO_MATL_INSERT  
		INTO @V_NO_LINE,
			 @V_CD_WC,
		     @V_CD_OP,
			 @V_CD_WCOP,
			 @V_YN_QC,
			 @V_YN_BF,
			 @V_TP_WC,
			 @V_YN_SUBCON,
			 @V_CD_EQUIP,
			 @V_YN_FINAL

		WHILE (@@FETCH_STATUS = 0)  
		BEGIN 
		--  
		-- <4 작업지시경로 INSERT> START  
		--  
		EXEC UP_PR_WO_ROUT_INSERT	@P_CD_COMPANY   = @P_CD_COMPANY,  
									@P_NO_WO        = @V_NO_WO,  
									@P_NO_LINE      = @V_NO_LINE,  
									@P_CD_OP        = @V_CD_OP,  
									@P_CD_PLANT     = @P_CD_PLANT,  
									@P_CD_WC        = @V_CD_WC,  
									@P_CD_WCOP      = @V_CD_WCOP,  
									@P_DT_REL       = @P_DT_WORK,  
									@P_DT_DUE       = @P_DT_WORK,  
									@P_ST_OP        = 'O',  
									@P_FG_WC        = @V_TP_WC,  
									@P_YN_WORK      = NULL,  
									@P_QT_WO        = @V_QT_WORK,  
									@P_QT_WIP       = 0.0,  
									@P_QT_WORK      = 0.0,  
									@P_QT_REJECT    = 0.0,  
									@P_QT_REWORK    = 0.0,  
									@P_QT_MOVE      = 0.0,  
									@P_TM_SETUP     = NULL,  
									@P_CD_RSRC1     = NULL,  
									@P_TM_LABOR     = NULL,  
									@P_CD_RSRC2     = NULL,  
									@P_TM_MACH      = NULL,  
									@P_TM_MOVE      = NULL,  
									@P_DY_SUBCON    = 0.0,  
									@P_TM_LABOR_ACT = NULL,  
									@P_TM_MACH_ACT  = NULL,  
									@P_CD_TOOL      = NULL,  
									@P_TM_REL       = NULL,  
									@P_TM_DUE       = NULL,  
									@P_YN_BF        = @V_YN_BF,  
									@P_YN_RECEIPT   = 'Y',  
									@P_TM           = NULL,  
									@P_YN_PAR       = 'N',  
									@P_YN_QC        = @V_YN_QC,  
									@P_QT_CLS       = 0.0,  
									@P_CD_OP_BASE   = NULL,  
									@P_YN_FINAL     = @V_YN_FINAL,  
									@P_QT_START     = 0.0,  
									@P_QT_OUTPO     = 0.0,  
									@P_QT_RCV       = 0.0,  
									@P_DT_CLOSE     = NULL,  
									@P_YN_SUBCON    = @V_YN_SUBCON,  
									@P_ID_INSERT    = @P_ID_INSERT,  
									@P_DC_RMK       = NULL,  
									@P_NO_SFT       = NULL,  
									@P_DC_RMK_1     = NULL,  
									@P_CD_EQUIP     = @V_CD_EQUIP
	
		IF (@@ERROR <> 0)  
		BEGIN  
			SELECT @ERRMSG = '[SP_CZ_PR_WO_SIMULATION_I]UP_PR_WO_ROUT_INSERT작업을정상적으로처리하지못했습니다.' 
			GOTO ERROR  
		END
		--  
		-- <4 작업지시경로 INSERT> END  
		--  
		FETCH NEXT FROM CUR_WO_MATL_INSERT  
		INTO @V_NO_LINE,
			 @V_CD_WC,
		     @V_CD_OP,
			 @V_CD_WCOP,
			 @V_YN_QC,
			 @V_YN_BF,
			 @V_TP_WC,
			 @V_YN_SUBCON,
			 @V_CD_EQUIP,
			 @V_YN_FINAL
	END  
	CLOSE CUR_WO_MATL_INSERT  
	DEALLOCATE CUR_WO_MATL_INSERT  
END 
--  
-- <4 작업지시경로 생성> END  
--  

--  
-- <5 작업지시소요자재 생성> START  
--  
BEGIN  
	--  
	-- <5.0 공정경로BOM에서 데이터 찾는 커서> START
	--  
	DECLARE CUR_WO_MATL_INSERT CURSOR FOR  

	SELECT CONVERT(NUMERIC(5, 0), ROW_NUMBER() OVER (ORDER BY RL.CD_OP ASC)) AS NO_LINE, 
		   RL.CD_WC,
		   RL.CD_OP,
		   RL.CD_WCOP,
		   B.CD_MATL,
		   PB.QT_ITEM_NET AS QT_NEED_NET,
		   I.FG_BF AS YN_BF,
		   I.FG_GIR,
		   B.CD_SL_OT
	FROM PR_ROUT RH
	JOIN PR_ROUT_L RL ON RL.CD_COMPANY = RH.CD_COMPANY AND RL.CD_PLANT = RH.CD_PLANT AND RL.CD_ITEM = RH.CD_ITEM AND RL.NO_OPPATH = RH.NO_OPPATH AND RL.TP_OPPATH = RH.TP_OPPATH AND RL.YN_USE = 'Y'
	JOIN PR_ROUT_ASN B ON B.CD_COMPANY = RL.CD_COMPANY AND B.CD_PLANT = RL.CD_PLANT AND B.CD_ITEM = RL.CD_ITEM AND B.NO_OPPATH = RL.NO_OPPATH AND B.TP_OPPATH = RL.TP_OPPATH AND B.CD_OP = RL.CD_OP
	JOIN PR_BOM PB ON PB.CD_COMPANY = B.CD_COMPANY AND PB.CD_PLANT = B.CD_PLANT AND PB.CD_ITEM = B.CD_ITEM AND PB.CD_MATL = B.CD_MATL AND PB.DT_END >= CONVERT(CHAR(8), GETDATE(), 112)
	JOIN MA_PITEM I ON I.CD_COMPANY = B.CD_COMPANY AND I.CD_PLANT = B.CD_PLANT AND I.CD_ITEM = B.CD_MATL 
	WHERE RH.CD_COMPANY	= @P_CD_COMPANY
	AND	RH.CD_PLANT	= @P_CD_PLANT
	AND	RH.CD_ITEM = @P_CD_ITEM
	AND	RH.NO_OPPATH = @P_PATN_ROUT
	AND RL.YN_USE = 'Y'

	OPEN CUR_WO_MATL_INSERT  

		FETCH NEXT FROM CUR_WO_MATL_INSERT  

		INTO	@V_NO_LINE,
				@V_CD_WC,
				@V_CD_OP,
				@V_CD_WCOP,
				@V_CD_MATL,  
				@V_QT_NEED_NET,  
				@V_YN_BF_MATL,  
				@V_FG_GIR,  
				@V_CD_SL_OT


	WHILE (@@FETCH_STATUS = 0)  
	BEGIN  
		--
		-- <5.1 유효성 체크> START
		--
		IF (@V_CD_SL_OT IS NULL OR @V_CD_SL_OT = '')  
		BEGIN  
			CLOSE CUR_WO_MATL_INSERT  
			DEALLOCATE CUR_WO_MATL_INSERT  

			SELECT @ERRMSG = '[SP_CZ_PR_WO_SIMULATION_I] 공정경로BOM에 소요자재[' + @V_CD_MATL + ']의 출고창고가 없습니다.' 
			GOTO ERROR  
		END  
		--
		-- <5.1 유효성 체크> END
		--

		--
		-- <5.2 소요량 및 단위소요량 계산> START
		--
		SELECT @V_QT_NEED = CONVERT(NUMERIC(17,4), ROUND(@V_QT_NEED_NET * @V_QT_WORK, 4))
		SELECT @V_QT_NEED_NET = CONVERT(NUMERIC(19,6), ROUND(@V_QT_NEED / @V_QT_WORK, 6))
		--
		-- <5.2 소요량 및 단위소요량 계산> END
		--
		
		--  
		-- <5.3 작업지시소요자재 INSERT> START  
		--  
		BEGIN  
			EXEC UP_PR_WO_BILL_INSERT	@P_CD_COMPANY    = @P_CD_COMPANY,  
										@P_NO_WO         = @V_NO_WO,  
										@P_NO_LINE       = @V_NO_LINE,  
										@P_CD_PLANT      = @P_CD_PLANT,  
										@P_CD_OP         = @V_CD_OP,
										@P_CD_MATL       = @V_CD_MATL,  
										@P_CD_WC         = @V_CD_WC,  
										@P_DT_NEED       = @P_DT_WORK,  
										@P_FG_NEED       = NULL,  
										@P_QT_NEED       = @V_QT_NEED,  
										@P_QT_REQ        = 0.0,  
										@P_QT_ISU        = 0.0,  
										@P_QT_USE        = 0.0,  
										@P_QT_RTN        = 0.0,  
										@P_NO_REQ        = NULL,  
										@P_YN_BF         = @V_YN_BF_MATL,  
										@P_QT_NEED_NET   = @V_QT_NEED_NET,  
										@P_CD_WCOP       = @V_CD_WCOP,  
										@P_QT_REQ_RETURN = 0.0,  
										@P_QT_TRN        = 0.0,  
										@P_FG_GIR        = @V_FG_GIR,  
										@P_ID_INSERT     = @P_ID_INSERT,  
										@P_CD_SL_IN      = NULL,  
										@P_CD_SL_OT      = @V_CD_SL_OT,  
										@P_DC_RMK		 = NULL,  
										@P_QT_ADJUST     = 0.0,  
										@P_NO_SORT		 = 0.0,  
										@P_NO_LOT		 = NULL,  
										@P_CD_TUIP_GROUP = NULL,  
										@P_NO_TUIP_SEQ   = NULL,  
										@P_QT_NEED_BOM   = 0.0,  
										@P_RT_SCRAP		 = 0.0  

			IF (@@ERROR <> 0)  
			BEGIN  
				CLOSE CUR_WO_MATL_INSERT  
				DEALLOCATE CUR_WO_MATL_INSERT  

				SELECT @ERRMSG = '[SP_CZ_PR_WO_SIMULATION_I]UP_PR_WO_BILL_INSERT작업을정상적으로처리하지못했습니다.' 
				GOTO ERROR  
			END  
		END
		--  
		-- <5.3 작업지시소요자재 INSERT> END  
		--  
		
		FETCH NEXT FROM CUR_WO_MATL_INSERT  

		INTO	@V_NO_LINE,  
				@V_CD_WC,
				@V_CD_OP,
				@V_CD_WCOP,
				@V_CD_MATL,  
				@V_QT_NEED_NET,  
				@V_YN_BF_MATL,  
				@V_FG_GIR,  
				@V_CD_SL_OT
				
	END  
	CLOSE CUR_WO_MATL_INSERT  
	DEALLOCATE CUR_WO_MATL_INSERT  
	--  
	-- <5.0 공정경로BOM에서 데이터 찾는 커서> END
	--  
END
--  
-- <5 작업지시소요자재 생성> END  
--  

SELECT @V_NO_WO AS NO_WO

PRINT '작업지시생성 END'

RETURN
ERROR: RAISERROR (@ERRMSG, 18, 1)
GO

