USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_REWORK_REG_I]    Script Date: 2021-11-15 오전 11:44:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [NEOE].[SP_CZ_PR_REWORK_REG_I]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_WORK			NVARCHAR(20),                
	@P_NO_WO			NVARCHAR(20),        
	@P_CD_PLANT			NVARCHAR(7),        
	@P_CD_OP			NVARCHAR(4),        
	@P_CD_ITEM			NVARCHAR(50),        
	@P_NO_EMP			NVARCHAR(10),        
	@P_DT_WORK			NVARCHAR(8),        
	@P_QT_WORK			NUMERIC(17,4),        
	@P_QT_REJECT		NUMERIC(17,4), 
	@P_CD_REJECT		NVARCHAR(7),        
	@P_QT_MOVE			NUMERIC(17,4),        
	@P_YN_REWORK		NVARCHAR(1), 
	@P_TM_LABOR			NVARCHAR(6),        
	@P_CD_RSRC_LABOR	NVARCHAR(10),        
	@P_TM_MACH			NVARCHAR(6),        
	@P_CD_RSRC_MACH		NVARCHAR(10),        
	@P_CD_WC			NVARCHAR(7),        
	@P_DC_REJECT		NVARCHAR(40),        
	@P_ID_INSERT		NVARCHAR(15),        
	@P_FG_MOVE			NVARCHAR(1),         
	@P_TP_REWORK		NVARCHAR(8), 
	@P_CD_REWORK		NVARCHAR(8),
	@P_NO_LOT			NVARCHAR(50), 
	@P_NO_SFT			NVARCHAR(3), 
    @P_CD_EQUIP         NVARCHAR(30), 
	@P_CD_SL			NVARCHAR(7), 

	@P_NO_IO_202_102	NVARCHAR(20),        -- 공정수불번호(이동출고('202')와이동입고('102'))
	@P_NO_IO_203		NVARCHAR(20),        -- 공정수불번호(생산출고('203'))
	@P_NO_IO_LINE_202	NUMERIC(5, 0),                -- 공정수불항번(이동출고('202'))
	@P_NO_IO_LINE_102	NUMERIC(5, 0),                -- 공정수불항번(이동입고('102'))
	@P_NO_IO_LINE_203	NUMERIC(5, 0),                -- 공정수불항번(생산출고('203'))


    @P_YN_SUBCON        NVARCHAR(1) = 'N',      --외주여부
    @P_NO_OPOUT_PO      NVARCHAR(20) = '',      --발주번호
    @P_NO_OPOUT_LINE    NUMERIC(5, 0) = 0,      --발주항번
    @P_NO_REL			NVARCHAR(20) = '',
    @P_QT_RSRC_LABOR	NUMERIC(17, 4) = 0		--작업이원 추가 20111213 최인성 정기현 김성호
)
AS

BEGIN

DECLARE	@ERRMSG				NVARCHAR(255),	-- ERROR 메시지
		@P_DTS_INSERT		VARCHAR(14),		-- 입력일
		@P_CD_DEPT			NVARCHAR(12)		-- 담당자의부서

DECLARE	@P_NEXT_CD_OP		NVARCHAR(8),		-- 다음공정
		@P_BEFORE_CD_OP		NVARCHAR(8),		-- 이전공정
		@P_LAST_CD_OP		NVARCHAR(8),		-- 마지막공정
		@P_FIRST_CD_OP		NVARCHAR(8),		-- 첫공정
		@P_ROUTCOUNT		INT				-- 현작업지시(PR_WO)의공정의갯수

--DECLARE @NO_IO_103                NVARCHAR(20),        -- 공정수불번호(생산입고('103'))
--                @NO_IO_LINE_103        NUMERIC(5, 0),                -- 공정수불항번(생산입고('103'))

DECLARE @NO_IO_MGMT_103			NVARCHAR(20), 
		@NO_LINE_IO_MGMT_103	NUMERIC(5, 0), 
		@NO_LINE_IO2_MGMT_103	NUMERIC(5, 0), 
		@FG_SLIP_MGMT_103		NVARCHAR(3), 
		@NO_LOT_MGMT_103		NVARCHAR(50), 

		@NO_IO_MGMT_102			NVARCHAR(20), 
		@NO_LINE_IO_MGMT_102	NUMERIC(5, 0), 
		@NO_LINE_IO2_MGMT_102	NUMERIC(5, 0), 
		@FG_SLIP_MGMT_102		NVARCHAR(3), 
		@NO_LOT_MGMT_102		NVARCHAR(50), 

		@NO_LINE_IO2			NUMERIC(5, 0), 

		@QT_IO_102				NUMERIC(17, 4), 
		@QT_MGMT_102			NUMERIC(17, 4), 
		@QT_IO_REMAIN_102		NUMERIC(17, 4), 

		@QT_IO_SUM				NUMERIC(17, 4), 

		@P_QT_WORK_REMAIN		NUMERIC(17,4),        

		@P_CD_WC_NEXT_CD_OP		NVARCHAR(14), 

		@NO_RCV					NVARCHAR(20), 
		@CD_GISL				NVARCHAR(7),			-- 품목의출고창고
		@TP_GR					NVARCHAR(3)

DECLARE	@YN_AUTORCV				NVARCHAR(1), 
		@NO_IO_MM				NVARCHAR(20),          -- 자재의수불번호
        @YN_AUTORCV_REQ			NVARCHAR(1),
		@V_NO_PJT			NVARCHAR(20),
		@V_SEQ_PROJECT		NUMERIC(5, 0)

SET @P_DTS_INSERT = NEOE.SF_SYSDATE(GETDATE())


--수량은DEFAULT 값을으로넣어준다.
SELECT @NO_LINE_IO_MGMT_103 = 0, @NO_LINE_IO2_MGMT_103 = 0, @NO_LINE_IO_MGMT_102 = 0, @NO_LINE_IO2_MGMT_102 = 0

--재작업수량을넣는다. 재작업시LOT NO를KEYING했을때이전공정의LOT NO와같은건이여러건일때
--어느LOT NO를가져와서어느LOT NO의관련수량을수정할지여부와그만큼해당LOT NO를가져온만큼값의줄어듦을표현하기위한변수
SELECT @P_QT_WORK_REMAIN = @P_QT_WORK


SELECT @P_CD_DEPT = ISNULL(	(	SELECT CD_DEPT
								FROM MA_EMP
								WHERE CD_COMPANY = @P_CD_COMPANY
									AND NO_EMP = @P_NO_EMP        ), '')

SELECT @TP_GR =	ISNULL(	(	SELECT TP_GR FROM PR_WO
							WHERE CD_COMPANY = @P_CD_COMPANY
								AND CD_PLANT = @P_CD_PLANT
								AND NO_WO = @P_NO_WO        ), '')

--SELECT @P_CD_SL =	ISNULL(	(	SELECT CD_SL
--							FROM MA_PITEM
--							WHERE CD_COMPANY = @P_CD_COMPANY
--								AND CD_PLANT = @P_CD_PLANT
--								AND CD_ITEM = @P_CD_ITEM        ), '')

SELECT @CD_GISL =	ISNULL(	(	SELECT CD_GISL
								FROM MA_PITEM
								WHERE CD_COMPANY = @P_CD_COMPANY
									AND CD_PLANT = @P_CD_PLANT
									AND CD_ITEM = @P_CD_ITEM        ), '')

---- 작업지시번호의자동입고처리여부(작업지시등록시공장환경설정에서가져온다.)
SELECT	@YN_AUTORCV = YN_AUTORCV, @YN_AUTORCV_REQ = YN_AUTORCV_REQ,
		@V_NO_PJT = ISNULL(NO_PJT, ''),
		@V_SEQ_PROJECT = ISNULL(SEQ_PROJECT, 0)
FROM PR_WO
WHERE CD_COMPANY = @P_CD_COMPANY
	AND CD_PLANT = @P_CD_PLANT
	AND NO_WO = @P_NO_WO

SET @P_QT_MOVE = @P_QT_WORK - @P_QT_REJECT
/*
--TEST
PRINT '---------TEST'
DECLARE @P_QT_MOVE_STR NVARCHAR(100)
DECLARE @P_QT_WORK_STR NVARCHAR(100)


SELECT @P_QT_MOVE_STR = STR(@P_QT_MOVE), @P_QT_WORK_STR = STR(@P_QT_WORK)
PRINT @P_QT_MOVE_STR
PRINT @P_QT_WORK_STR
--TEST
*/
DECLARE @VAT_RATE       NUMERIC(17, 4), 
        @UM_EX          NUMERIC(19, 6), 
        @UM_WORK        NUMERIC(19, 6), 
        @AM_EX          NUMERIC(19, 6), 
        @AM_WORK        NUMERIC(17, 4), 
        @AM_VAT_WORK    NUMERIC(17, 4), 
        @AM_HAP_WORK    NUMERIC(17, 4)

--공정외주인 경우 외화단가, 외화금액, 원화단가, 원화금액, 부가세, 원화금액합계를 넣어줘야한다.
--그래서 여기서 공정외주발주에서 구해오는 구문을 넣었다.
IF (@P_YN_SUBCON = 'Y')
BEGIN
    --공정외주발주의 부가세율, 외화단가, 외화금액, 원화단가, 원화금액 을 구해온다.
    SELECT @VAT_RATE = H.VAT_RATE, 
        @UM_EX = L.UM_EX, 
        @UM_WORK = L.UM, 
        @AM_EX = L.UM_EX * @P_QT_MOVE, 
        @AM_WORK = L.UM * @P_QT_MOVE
    FROM PR_OPOUT_POL L
        INNER JOIN PR_OPOUT_POH H ON H.CD_COMPANY = @P_CD_COMPANY
                                AND H.CD_PLANT = @P_CD_PLANT
                                AND H.NO_PO = L.NO_PO
    WHERE L.CD_COMPANY = @P_CD_COMPANY
        AND L.CD_PLANT = @P_CD_PLANT
        AND L.NO_PO = @P_NO_OPOUT_PO
        AND L.NO_LINE = @P_NO_OPOUT_LINE

    --외회단가, 외화금액, 원화단가, 원화금액 값이 없는 경우는 0을 넣어준다.(그런 경우가 있으면 않되지만)
    --부가세율의 값이 없는 경우는 1을 넣어준다
    SELECT @UM_EX = ISNULL(@UM_EX, 0), 
        @UM_WORK = ISNULL(@UM_WORK, 0), 
        @AM_EX = ISNULL(@AM_EX, 0), 
        @AM_WORK = ISNULL(@AM_WORK, 0), 
        @AM_VAT_WORK = ISNULL(@AM_VAT_WORK, 1)

    --부가세율을 구해온다.
    SELECT @VAT_RATE = CASE WHEN @VAT_RATE IS NULL OR @VAT_RATE = 0 THEN 1 ELSE @VAT_RATE END

    --부가세율을 이용해서 부가세를 구한다.(부가세금액 * 부가세율 * 0.01)
    SELECT @AM_VAT_WORK = @AM_WORK * @VAT_RATE * 0.01

    --원화금액과 부가세를 합하여 합계금액을 넣어준다.
    SELECT @AM_HAP_WORK = ISNULL(@AM_WORK, 0) + ISNULL(@AM_VAT_WORK, 0)
END

EXEC UP_PR_WORK_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_NO_WORK = @P_NO_WORK, 
	@P_NO_WO = @P_NO_WO, @P_CD_PLANT = @P_CD_PLANT, @P_CD_OP = @P_CD_OP, 
	@P_CD_ITEM = @P_CD_ITEM, @P_DT_WORK = @P_DT_WORK, @P_QT_WORK = @P_QT_WORK, 
	@P_QT_REJECT = @P_QT_REJECT, @P_CD_REJECT = @P_CD_REJECT, @P_YN_REWORK = @P_YN_REWORK, 
	@P_YN_BADWORK = 'N', 
	@P_TM_LABOR = @P_TM_LABOR, @P_CD_RSRC_LABOR = @P_CD_RSRC_LABOR,
	@P_TM_MACH = @P_TM_MACH, @P_CD_RSRC_MACH = @P_CD_RSRC_MACH, @P_NO_EMP = @P_NO_EMP, 
	@P_CD_WC = @P_CD_WC, @P_ID_INSERT = @P_ID_INSERT, @P_TP_REWORK = @P_TP_REWORK, 
	@P_CD_REWORK = @P_CD_REWORK, @P_DC_REJECT = @P_DC_REJECT, @P_QT_MOVE = @P_QT_MOVE, 
	@P_NO_SFT = @P_NO_SFT, @P_CD_EQUIP = @P_CD_EQUIP, 
    @P_YN_SUBCON = @P_YN_SUBCON, 
    @P_NO_OPOUT_PO = @P_NO_OPOUT_PO, @P_NO_OPOUT_PO_LINE = @P_NO_OPOUT_LINE, 
    @P_DC_RMK1 = '', @P_DC_RMK2 = '', 
    @P_UM_EX = @UM_EX, @P_AM_EX = @AM_EX, @P_UM_WORK = @UM_WORK, @P_AM_WORK = @AM_WORK, 
    @P_AM_VAT_WORK = @AM_VAT_WORK, @P_AM_HAP_WORK = @AM_HAP_WORK,
    @P_NO_REL = @P_NO_REL, @P_QT_RSRC_LABOR = @P_QT_RSRC_LABOR,
    @P_NO_LOT = @P_NO_LOT

IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END


--공정이동등록
IF (@P_FG_MOVE = 'Y')
BEGIN
	--다음공정을구한다.
	SELECT @P_NEXT_CD_OP = MIN(CD_OP)
	FROM PR_WO_ROUT
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO
		AND CD_OP > @P_CD_OP

	--이전공정을구한다.
	SELECT @P_BEFORE_CD_OP = MAX(CD_OP)
	FROM PR_WO_ROUT
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO
		AND CD_OP < @P_CD_OP

	--마지막공정을구한다.
	SELECT @P_LAST_CD_OP = MAX(CD_OP)
	FROM PR_WO_ROUT
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO

	--첫번째공정을구한다.
	SELECT @P_FIRST_CD_OP = MIN(CD_OP) 
	FROM PR_WO_ROUT
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO

	--공정의갯수를구한다.(공정이하나인경우를위해서)        --> 공정이하나인경우는첫번째공정이마지막공정이된다.
	SELECT @P_ROUTCOUNT = COUNT(1) 
	FROM PR_WO_ROUT
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO

	--마지막공정이아닌경우
	IF (@P_CD_OP <> @P_LAST_CD_OP OR @P_ROUTCOUNT = 1)
	BEGIN
		IF (@P_QT_WORK > 0)        --작업수량이보다크면이동출고('202'), 이동입고('102') 공정수불전표발생
		BEGIN
			IF (@P_ROUTCOUNT > 1)        --공정의수가개이상일때만이구문을실행한다.
			BEGIN
				-- 이동출고전표(FG_SLIP = '202')의관련작업장(CD_WC_TO)과이동입고전표(FG_SLIP = '102')의주작업장(CD_WC_FR)에들어가야함.
				SELECT @P_CD_WC_NEXT_CD_OP = CD_WC 
				FROM PR_WO_ROUT
				WHERE CD_COMPANY = @P_CD_COMPANY
					AND CD_PLANT = @P_CD_PLANT
					AND NO_WO = @P_NO_WO
					AND CD_OP = @P_NEXT_CD_OP        --현재공정이마지막공정이아니으로다음공정은존재한다고가정.

				EXEC UP_PR_QTIOH_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT = @P_CD_PLANT, @P_NO_IO = @P_NO_IO_202_102, @P_FG_SLIP = N'202', @P_TP_SLIP = N'1', @P_YN_WIP = N'Y', @P_DT_IO = @P_DT_WORK, @P_CD_DEPT = @P_CD_DEPT, @P_CD_WC = @P_CD_WC, @P_NO_EMP = @P_NO_EMP, @P_ID_INSERT = @P_ID_INSERT
		                
				IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

				EXEC UP_PR_QTIO_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT = @P_CD_PLANT, @P_NO_IO = @P_NO_IO_202_102, @P_NO_LINE_IO = @P_NO_IO_LINE_202, @P_FG_SLIP = N'202', @P_TP_SLIP = N'1', @P_YN_WIP = N'Y', @P_CD_ITEM = @P_CD_ITEM, @P_CD_WC_FR = @P_CD_WC, @P_CD_WC_TO = @P_CD_WC_NEXT_CD_OP, @P_DT_IO = @P_DT_WORK, @P_QT_IO = @P_QT_WORK, @P_QT_PROC = @P_QT_WORK, @P_NO_WO = @P_NO_WO, @P_NO_SRC = @P_NO_WORK, @P_CD_OP = @P_CD_OP, @P_ID_INSERT = @P_ID_INSERT
		                
				IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

				EXEC UP_PR_QTIO_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT = @P_CD_PLANT, @P_NO_IO = @P_NO_IO_202_102, @P_NO_LINE_IO = @P_NO_IO_LINE_102, @P_FG_SLIP = N'102', @P_TP_SLIP = N'1', @P_YN_WIP = N'Y', @P_CD_ITEM = @P_CD_ITEM, @P_CD_WC_FR = @P_CD_WC_NEXT_CD_OP, @P_CD_WC_TO = @P_CD_WC, @P_DT_IO = @P_DT_WORK, @P_QT_IO = @P_QT_WORK, @P_QT_PROC = @P_QT_WORK, @P_NO_WO = @P_NO_WO, @P_NO_SRC = @P_NO_WORK, @P_CD_OP = @P_CD_OP, @P_ID_INSERT = @P_ID_INSERT
		                
				IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END
			END
			---------------------------------------------------------------------------------------------------
			----공정수불LOT(PR_QTIOLOT) 생성구문시작(첫번째공정에서만생성한다.)                                                        ---
			---------------------------------------------------------------------------------------------------

			--현재공정이첫번재공정이면이전에생성시키지않았던PR_QTIOLOT을생산입고전표(FG_SLIP = '103')로생성한다.
			--이때LOT번호가없으면(이때는PR_QTIOLOT를생성시키는프로시져를화면에서직접호출) 생성시키지않는다.
			IF (@P_CD_OP = @P_FIRST_CD_OP AND ISNULL(@P_NO_LOT, '') <> '')
			BEGIN
				IF (        EXISTS        (        SELECT 1
								FROM PR_QTIOLOT
								WHERE CD_COMPANY = @P_CD_COMPANY
									AND CD_PLANT = @P_CD_PLANT
									AND NO_WO = @P_NO_WO
									AND CD_OP = @P_FIRST_CD_OP
									AND FG_SLIP = '103'                --생산입고전표(FG_SLIP = '103')
									AND NO_LOT = @P_NO_LOT
						)        )
				BEGIN
					-- 이구문은같은작업지시의생산입고전표(FG_SLIP = '103')로같은LOT 번호가있으면에러가발생한다.
					-- 작업실적등록을할때생성시키는생산입고전표(FG_SLIP = '103')의LOT 번호는같은경우수량만더해주는구문이있다.
					-- 만약이구문이변경되어같은LOT 번호를생성시켜주는구문으로바뀐다면... 이구문또한바뀌어야한다.
					SELECT @NO_IO_MGMT_103 = NO_IO, 
						@NO_LINE_IO_MGMT_103 = NO_LINE_IO, 
						@NO_LINE_IO2_MGMT_103 = NO_LINE_IO2, 
						@FG_SLIP_MGMT_103 = FG_SLIP, 
						@NO_LOT_MGMT_103 = NO_LOT
					FROM PR_QTIOLOT
					WHERE CD_COMPANY = @P_CD_COMPANY
						AND CD_PLANT = @P_CD_PLANT
						AND NO_WO = @P_NO_WO
						AND CD_OP = @P_FIRST_CD_OP
						AND FG_SLIP = '103'                --생산입고전표(FG_SLIP = '103')
						AND NO_LOT = @P_NO_LOT

					IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업지시번호(' + @P_NO_WO + ')의생산입고전표참조번호를받아오다가에러가발생했습니다. 같은NOT 번호(' + @P_NO_LOT + ')로생성된생산입고전표가건이상일수도있습니다. 체크해보십시오. 만약그렇다면이구문은수정되어야합니다.' GOTO ERROR END
				END

				-- PR_QTIOLOT의이동출고전표를생선한다.(FG_SLIP = '202')
				INSERT PR_QTIOLOT(CD_COMPANY, CD_PLANT, NO_IO, NO_LINE_IO, FG_SLIP, NO_LOT, CD_ITEM, DT_IO, CD_SL, QT_IO, QT_MGMT, YN_RETURN, NO_WO, NO_WORK, CD_OP, CD_WC, CD_WCOP, NO_IO_MGMT, NO_LINE_IO_MGMT, NO_LINE_IO_MGMT2, FG_SLIP_MGMT, NO_LOT_MGMT, NO_IO_SC, NO_LINE_IO_SC, NO_LINE_IO_SC2, FG_SLIP_SC, NO_LOT_SC, NO_IO_MM, NO_IOLINE_MM, NO_LOT_MM, ID_INSERT, DTS_INSERT)
				SELECT @P_CD_COMPANY, @P_CD_PLANT, NO_IO, NO_LINE_IO, '202', @P_NO_LOT, CD_ITEM, DT_IO, NULL, QT_IO, 0, 'N', NO_WO, @P_NO_WORK, CD_OP, @P_CD_WC, CD_WCOP, @NO_IO_MGMT_103, @NO_LINE_IO_MGMT_103, @NO_LINE_IO2_MGMT_103, @FG_SLIP_MGMT_103, @NO_LOT_MGMT_103, @NO_IO_MGMT_103, @NO_LINE_IO_MGMT_103, @NO_LINE_IO2_MGMT_103, @FG_SLIP_MGMT_103, @NO_LOT_MGMT_103, NULL, 0, NULL, ID_INSERT, DTS_INSERT
				FROM PR_QTIO
				WHERE CD_COMPANY = @P_CD_COMPANY
					AND CD_PLANT = @P_CD_PLANT
					AND NO_IO = @P_NO_IO_202_102
					AND NO_LINE_IO = @P_NO_IO_LINE_202
					AND FG_SLIP = '202'

				IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

				-- PR_QTIOLOT의이동입고전표를생선한다.(FG_SLIP = '102')
				INSERT PR_QTIOLOT(CD_COMPANY, CD_PLANT, NO_IO, NO_LINE_IO, FG_SLIP, NO_LOT, CD_ITEM, DT_IO, CD_SL, QT_IO, QT_MGMT, YN_RETURN, NO_WO, NO_WORK, CD_OP, CD_WC, CD_WCOP, NO_IO_MGMT, NO_LINE_IO_MGMT, FG_SLIP_MGMT, NO_LOT_MGMT, NO_IO_SC, NO_LINE_IO_SC, NO_LINE_IO_SC2, FG_SLIP_SC, NO_LOT_SC, NO_IO_MM, NO_IOLINE_MM, NO_LOT_MM, ID_INSERT, DTS_INSERT)
				SELECT @P_CD_COMPANY, @P_CD_PLANT, NO_IO, NO_LINE_IO, '102', @P_NO_LOT, CD_ITEM, DT_IO, NULL, QT_IO, 0, 'N', NO_WO, @P_NO_WORK, CD_OP, @P_CD_WC, CD_WCOP, @P_NO_IO_202_102, @P_NO_IO_LINE_202, '202', @P_NO_LOT, @NO_IO_MGMT_103, @NO_LINE_IO_MGMT_103, @NO_LINE_IO2_MGMT_103, @FG_SLIP_MGMT_103, @NO_LOT_MGMT_103, NULL, 0, NULL, ID_INSERT, DTS_INSERT
				FROM PR_QTIO
				WHERE CD_COMPANY = @P_CD_COMPANY
					AND CD_PLANT = @P_CD_PLANT
					AND NO_IO = @P_NO_IO_202_102
					AND NO_LINE_IO = @P_NO_IO_LINE_102
					AND FG_SLIP = '102'

				IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

			END
			--현재공정이첫번재공정이면이전에생성시키지않았던PR_QTIOLOT을생산입고전표(FG_SLIP = '103')로생성한다.
			--이때LOT번호가없으면(이때는PR_QTIOLOT를생성시키는프로시져를화면에서직접호출) 생성시키지않는다.
			--첫번째공정이아닌공정에서LOT번호가있다는것은재작업이란의미이다. 따라서이경우는재작업인경우이다.
			ELSE IF (@P_CD_OP <> @P_FIRST_CD_OP AND ISNULL(@P_NO_LOT, '') <> '')
			BEGIN
				PRINT '이경우는재작업'

				SELECT @QT_IO_SUM = ISNULL(                (        SELECT SUM(QT_IO - QT_MGMT)
													FROM PR_QTIOLOT
													WHERE CD_COMPANY = @P_CD_COMPANY
														AND CD_PLANT = @P_CD_PLANT
														AND NO_WO = @P_NO_WO
														AND CD_OP = @P_BEFORE_CD_OP
														AND FG_SLIP = '102'                --이동입고전표(FG_SLIP = '102')
														AND NO_LOT = @P_NO_LOT
														AND QT_IO - QT_MGMT > 0        ), 0)

				IF (@QT_IO_SUM <> 0 AND @P_QT_WORK > @QT_IO_SUM)
				BEGIN
					SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업지시번호(' + @P_NO_WO + '), 공정(' + @P_BEFORE_CD_OP + '), 전표구분(이동입고전표[102]), LOT번호(' + @P_NO_LOT + ')인공중수불LOT(PR_QTIOLOT)의관련수량(QT_MGMT)의합이' + LTRIM(STR(@QT_IO_SUM)) + '입니다. 재작업수량(' + LTRIM(STR(@P_QT_WORK)) + ')이관련수량보다적습니다. LOT번호를변경하던지수량을변경하십시오.' GOTO ERROR
				END

				DECLARE CUR_QTIOLOT CURSOR FOR
				SELECT NO_IO, NO_LINE_IO, NO_LINE_IO2, FG_SLIP, NO_LOT, QT_IO, QT_MGMT, QT_IO - QT_MGMT
				FROM PR_QTIOLOT
				WHERE CD_COMPANY = @P_CD_COMPANY
					AND CD_PLANT = @P_CD_PLANT
					AND NO_WO = @P_NO_WO
					AND CD_OP = @P_BEFORE_CD_OP
					AND FG_SLIP = '102'                --이동입고전표(FG_SLIP = '102')
					AND NO_LOT = @P_NO_LOT
					AND QT_IO - QT_MGMT > 0

				OPEN CUR_QTIOLOT
				FETCH NEXT FROM CUR_QTIOLOT INTO @NO_IO_MGMT_102, @NO_LINE_IO_MGMT_102, @NO_LINE_IO2_MGMT_102, @FG_SLIP_MGMT_102, @NO_LOT_MGMT_102, @QT_IO_102, @QT_MGMT_102, @QT_IO_REMAIN_102

				WHILE @@FETCH_STATUS = 0
				BEGIN
					SELECT @P_QT_WORK_REMAIN = @P_QT_WORK_REMAIN - @QT_IO_REMAIN_102

					SELECT @NO_LINE_IO2 = ISNULL(        (        SELECT MAX(NO_LINE_IO2)
													FROM PR_QTIO A
														INNER JOIN PR_QTIOLOT B ON B.CD_COMPANY = @P_CD_COMPANY
																			AND B.CD_PLANT = @P_CD_PLANT
																			AND B.NO_IO = @P_NO_IO_202_102
																			AND B.NO_LINE_IO = @P_NO_IO_LINE_202
																			AND B.FG_SLIP = A.FG_SLIP
																			AND B.NO_LOT = @P_NO_LOT
													WHERE A.CD_COMPANY = @P_CD_COMPANY
														AND A.CD_PLANT = @P_CD_PLANT
														AND A.NO_IO = @P_NO_IO_202_102
														AND A.NO_LINE_IO = @P_NO_IO_LINE_202
														AND A.FG_SLIP = '202'        ), -1) + 1

					-- PR_QTIOLOT의이동출고전표를생선한다.(FG_SLIP = '202')
					INSERT PR_QTIOLOT(CD_COMPANY, CD_PLANT, NO_IO, NO_LINE_IO, NO_LINE_IO2, FG_SLIP, NO_LOT, CD_ITEM, DT_IO, CD_SL, QT_IO, QT_MGMT, YN_RETURN, NO_WO, NO_WORK, CD_OP, CD_WC, CD_WCOP, NO_IO_MGMT, NO_LINE_IO_MGMT, NO_LINE_IO_MGMT2, FG_SLIP_MGMT, NO_LOT_MGMT, NO_IO_SC, NO_LINE_IO_SC, NO_LINE_IO_SC2, FG_SLIP_SC, NO_LOT_SC, NO_IO_MM, NO_IOLINE_MM, NO_LOT_MM, ID_INSERT, DTS_INSERT)
					SELECT @P_CD_COMPANY, @P_CD_PLANT, NO_IO, NO_LINE_IO, @NO_LINE_IO2, '202', @P_NO_LOT, CD_ITEM, DT_IO, NULL, 
						CASE WHEN @P_QT_WORK_REMAIN >= 0 THEN @QT_IO_REMAIN_102 ELSE @P_QT_WORK_REMAIN + @QT_IO_REMAIN_102 END, 
						0, 'N', NO_WO, @P_NO_WORK, CD_OP, @P_CD_WC, CD_WCOP, @NO_IO_MGMT_102, @NO_LINE_IO_MGMT_102, @NO_LINE_IO2_MGMT_102, @FG_SLIP_MGMT_102, @NO_LOT_MGMT_102, NULL, 0, 0, NULL, NULL, NULL, 0, NULL, ID_INSERT, DTS_INSERT
					FROM PR_QTIO
					WHERE CD_COMPANY = @P_CD_COMPANY
						AND CD_PLANT = @P_CD_PLANT
						AND NO_IO = @P_NO_IO_202_102
						AND NO_LINE_IO = @P_NO_IO_LINE_202
						AND FG_SLIP = '202'

					IF (@@ERROR <> 0)
					BEGIN
						CLOSE CUR_QTIOLOT
						DEALLOCATE CUR_QTIOLOT
						SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.'  
						GOTO ERROR  
					END

					-- PR_QTIOLOT의이동입고전표를생선한다.(FG_SLIP = '102')
					INSERT PR_QTIOLOT(CD_COMPANY, CD_PLANT, NO_IO, NO_LINE_IO, NO_LINE_IO2, FG_SLIP, NO_LOT, CD_ITEM, DT_IO, CD_SL, QT_IO, QT_MGMT, YN_RETURN, NO_WO, NO_WORK, CD_OP, CD_WC, CD_WCOP, NO_IO_MGMT, NO_LINE_IO_MGMT, NO_LINE_IO_MGMT2, FG_SLIP_MGMT, NO_LOT_MGMT, NO_IO_SC, NO_LINE_IO_SC, NO_LINE_IO_SC2, FG_SLIP_SC, NO_LOT_SC, NO_IO_MM, NO_IOLINE_MM, NO_LOT_MM, ID_INSERT, DTS_INSERT)
					SELECT @P_CD_COMPANY, @P_CD_PLANT, NO_IO, NO_LINE_IO, @NO_LINE_IO2, '102', @P_NO_LOT, CD_ITEM, DT_IO, NULL, 
						CASE WHEN @P_QT_WORK_REMAIN >= 0 THEN @QT_IO_REMAIN_102 ELSE @P_QT_WORK_REMAIN + @QT_IO_REMAIN_102 END, 
						0, 'N', NO_WO, @P_NO_WORK, CD_OP, @P_CD_WC, CD_WCOP, @P_NO_IO_202_102, @P_NO_IO_LINE_202, @NO_LINE_IO2, '202', @P_NO_LOT, NULL, 0, 0, NULL, NULL, NULL, 0, NULL, ID_INSERT, DTS_INSERT
					FROM PR_QTIO
					WHERE CD_COMPANY = @P_CD_COMPANY
						AND CD_PLANT = @P_CD_PLANT
						AND NO_IO = @P_NO_IO_202_102
						AND NO_LINE_IO = @P_NO_IO_LINE_102
						AND FG_SLIP = '102'

					IF (@@ERROR <> 0)
					BEGIN
						CLOSE CUR_QTIOLOT
						DEALLOCATE CUR_QTIOLOT
						SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.'  
						GOTO ERROR  
					END

					--작업잔량이없으면PR_QTIOLOT을생성시켜주는CURSOR 를빠져나간다.
					IF (@P_QT_WORK_REMAIN <= 0)
					BEGIN
						BREAK
					END

				FETCH NEXT FROM CUR_QTIOLOT INTO @NO_IO_MGMT_102, @NO_LINE_IO_MGMT_102, @NO_LINE_IO2_MGMT_102, @FG_SLIP_MGMT_102, @NO_LOT_MGMT_102, @QT_IO_102, @QT_MGMT_102, @QT_IO_REMAIN_102
				END

				CLOSE CUR_QTIOLOT
				DEALLOCATE CUR_QTIOLOT

			END

			---------------------------------------------------------------------------------------------------
			----공정수불LOT(PR_QTIOLOT) 생성구문끝                                                                                                  ---
			---------------------------------------------------------------------------------------------------
		END

		IF (@P_ROUTCOUNT > 1)        --공정의수가 1개이상일때만이구문을실행한다.
		BEGIN
			--PR_WO_ROUT의재공수량(QT_WIP), 이동수량(QT_MOVE) UPDATE
			UPDATE PR_WO_ROUT
			SET QT_WIP = ISNULL(A.QT_WIP, 0) - ISNULL(@P_QT_WORK, 0) + ISNULL(@P_QT_REJECT, 0), -- - ISNULL(A.QT_BAD, 0), 
				QT_MOVE = ISNULL(A.QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0)
			FROM PR_WO_ROUT A
			WHERE A.CD_COMPANY = @P_CD_COMPANY
				AND A.CD_PLANT = @P_CD_PLANT
				AND A.NO_WO = @P_NO_WO
				AND A.CD_OP = @P_CD_OP
				
			------------------------------------------------------------------------------------------------------------------------------------------
			--작업지시분할
			------------------------------------------------------------------------------------------------------------------------------------------
			UPDATE PR_WO_ROUT_REL
			SET QT_WIP = ISNULL(A.QT_WIP, 0) - ISNULL(@P_QT_WORK, 0) + ISNULL(@P_QT_REJECT, 0), -- - ISNULL(A.QT_BAD, 0), 
				QT_MOVE = ISNULL(A.QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0)
			FROM PR_WO_ROUT_REL A
			WHERE A.CD_COMPANY = @P_CD_COMPANY
				AND A.CD_PLANT = @P_CD_PLANT
				AND A.NO_WO = @P_NO_WO
				AND A.CD_OP = @P_CD_OP
				AND A.NO_REL = @P_NO_REL
				
			------------------------------------------------------------------------------------------------------------------------------------------
			--작업지시분할
			------------------------------------------------------------------------------------------------------------------------------------------

			IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

			--PR_WO_ROUT의입고, 재공수량UPDATE
			UPDATE PR_WO_ROUT
			SET QT_START = ISNULL(QT_START, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0), 
				QT_WIP = ISNULL(QT_WIP, 0) +  ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0)
			WHERE CD_COMPANY = @P_CD_COMPANY
				AND CD_PLANT = @P_CD_PLANT
				AND NO_WO = @P_NO_WO
				AND CD_OP = @P_NEXT_CD_OP
				
			------------------------------------------------------------------------------------------------------------------------------------------
			--작업지시분할
			------------------------------------------------------------------------------------------------------------------------------------------
			
			UPDATE PR_WO_ROUT_REL
			SET QT_START = ISNULL(QT_START, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0), 
				QT_WIP = ISNULL(QT_WIP, 0) +  ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0)
			WHERE CD_COMPANY = @P_CD_COMPANY
				AND CD_PLANT = @P_CD_PLANT
				AND NO_WO = @P_NO_WO
				AND CD_OP = @P_NEXT_CD_OP
				
			------------------------------------------------------------------------------------------------------------------------------------------
			--작업지시분할
			------------------------------------------------------------------------------------------------------------------------------------------

			IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END
		END
	END
	IF (@P_CD_OP = @P_LAST_CD_OP OR @P_ROUTCOUNT = 1)                --마지막공정이면서공정이개인경우
	BEGIN

		EXEC UP_PR_QTIOH_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT = @P_CD_PLANT, @P_NO_IO = @P_NO_IO_203, @P_FG_SLIP = N'203', @P_TP_SLIP = N'1', @P_YN_WIP = N'Y', @P_DT_IO = @P_DT_WORK, @P_CD_DEPT = @P_CD_DEPT, @P_CD_WC = @P_CD_WC, @P_NO_EMP = @P_NO_EMP, @P_ID_INSERT = @P_ID_INSERT

		IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

		EXEC UP_PR_QTIO_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT = @P_CD_PLANT, @P_NO_IO = @P_NO_IO_203, @P_NO_LINE_IO = @P_NO_IO_LINE_203, @P_FG_SLIP = N'203', @P_TP_SLIP = N'1', @P_YN_WIP = N'Y', @P_CD_ITEM = @P_CD_ITEM, @P_CD_WC_FR = @P_CD_WC, @P_CD_WC_TO = '', @P_DT_IO = @P_DT_WORK, @P_QT_IO = @P_QT_WORK, @P_QT_PROC = @P_QT_WORK, @P_NO_WO = @P_NO_WO, @P_NO_SRC = @P_NO_WORK, @P_CD_OP = @P_CD_OP, @P_ID_INSERT = @P_ID_INSERT

		IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

		--작업지시공정경로(PR_WO_ROUT) 현재공정(@P_CD_OP)의재공수량(QT_WIP), 이동수량(QT_MOVE) UPDATE
		UPDATE PR_WO_ROUT
		SET QT_WIP = ISNULL(A.QT_WIP, 0) - ISNULL(@P_QT_WORK, 0) + ISNULL(@P_QT_REJECT, 0)-- - ISNULL(A.QT_BAD, 0)
			, QT_MOVE = ISNULL(A.QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0)
		FROM PR_WO_ROUT A
		WHERE A.CD_COMPANY = @P_CD_COMPANY
			AND A.CD_PLANT = @P_CD_PLANT
			AND A.NO_WO = @P_NO_WO
			AND A.CD_OP = @P_CD_OP
			
		------------------------------------------------------------------------------------------------------------------------------------------
		--작업지시분할
		------------------------------------------------------------------------------------------------------------------------------------------
		UPDATE PR_WO_ROUT_REL
		SET QT_WIP = ISNULL(A.QT_WIP, 0) - ISNULL(@P_QT_WORK, 0) + ISNULL(@P_QT_REJECT, 0),-- - ISNULL(A.QT_BAD, 0)
			 QT_MOVE = ISNULL(A.QT_MOVE, 0) + ISNULL(@P_QT_WORK, 0) - ISNULL(@P_QT_REJECT, 0)
		FROM PR_WO_ROUT_REL A
		WHERE A.CD_COMPANY = @P_CD_COMPANY
			AND A.CD_PLANT = @P_CD_PLANT
			AND A.NO_WO = @P_NO_WO
			AND A.CD_OP = @P_CD_OP	
			AND A.NO_REL = @P_NO_REL
		------------------------------------------------------------------------------------------------------------------------------------------
		--작업지시분할
		------------------------------------------------------------------------------------------------------------------------------------------	
			

		--현재공정이마지막공정이면서LOT번호를KEYING한경우PR_QTIOLOT을생산출고전표(FG_SLIP = '203')로생성한다.
		IF (@P_CD_OP = @P_LAST_CD_OP AND ISNULL(@P_NO_LOT, '') <> '')
		BEGIN
			-- PR_QTIOLOT의이동출고전표를생선한다.(FG_SLIP = '203')
			INSERT PR_QTIOLOT(CD_COMPANY, CD_PLANT, NO_IO, NO_LINE_IO, FG_SLIP, NO_LOT, CD_ITEM, DT_IO, CD_SL, QT_IO, QT_MGMT, YN_RETURN, NO_WO, NO_WORK, CD_OP, CD_WC, CD_WCOP, NO_IO_MGMT, NO_LINE_IO_MGMT, FG_SLIP_MGMT, NO_LOT_MGMT, NO_IO_SC, NO_LINE_IO_SC, FG_SLIP_SC, NO_LOT_SC, NO_IO_MM, NO_IOLINE_MM, NO_LOT_MM, ID_INSERT, DTS_INSERT)
			SELECT @P_CD_COMPANY, @P_CD_PLANT, NO_IO, NO_LINE_IO, '203', @P_NO_LOT, CD_ITEM, DT_IO, @P_CD_SL, QT_IO, 0, 'N', NO_WO, @P_NO_WORK, CD_OP, @P_CD_WC, CD_WCOP, NULL, 0, NULL, NULL, NULL, 0, NULL, NULL, NULL, 0, NULL, ID_INSERT, DTS_INSERT
			FROM PR_QTIO
			WHERE CD_COMPANY = @P_CD_COMPANY
				AND CD_PLANT = @P_CD_PLANT
				AND NO_IO = @P_NO_IO_203
				AND NO_LINE_IO = @P_NO_IO_LINE_203
				AND FG_SLIP = '203'

			IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END
		END
	END
END

UPDATE PR_WO_ROUT
SET QT_REWORK = ISNULL(QT_REWORK, 0) + ISNULL(@P_QT_WORK, 0)
WHERE CD_COMPANY = @P_CD_COMPANY
	AND CD_PLANT = @P_CD_PLANT
	AND NO_WO = @P_NO_WO
	AND CD_OP = @P_CD_OP
	
	
------------------------------------------------------------------------------------------------------------------------------------------
--작업지시분할
------------------------------------------------------------------------------------------------------------------------------------------		

UPDATE PR_WO_ROUT_REL
SET QT_REWORK = ISNULL(QT_REWORK, 0) + ISNULL(@P_QT_WORK, 0)
WHERE CD_COMPANY = @P_CD_COMPANY
	AND CD_PLANT = @P_CD_PLANT
	AND NO_WO = @P_NO_WO
	AND CD_OP = @P_CD_OP
	AND NO_REL = @P_NO_REL

------------------------------------------------------------------------------------------------------------------------------------------
--작업지시분할
------------------------------------------------------------------------------------------------------------------------------------------		

--*******************마지막공정이면자동생산입고의뢰, 자동입고***********************************
IF (@P_LAST_CD_OP = @P_CD_OP)
BEGIN
	IF (@P_QT_MOVE <> 0.0)
	BEGIN

        IF (@YN_AUTORCV_REQ = 'Y')
        BEGIN
		    --생산(생산입고의뢰등록)------------------------------------------------------------------------------------
		    EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_MODULE = 'PR', @P_CD_CLASS = '06', @P_DOCU_YM = @P_DT_WORK, @P_NO = @NO_RCV OUTPUT

		    IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

		    EXEC UP_PR_RCVH_INSERT @P_CD_COMPANY = @P_CD_COMPANY,		@P_CD_PLANT= @P_CD_PLANT,		@P_NO_REQ = @NO_RCV, 
								   @P_DT_REQ= @P_DT_WORK,				@P_NO_EMP= @P_NO_EMP,			@P_DC_RMK = '',
								   @P_ID_INSERT = @P_ID_INSERT

		    IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

			    EXEC UP_PR_RCVL_INSERT 
				@P_CD_COMPANY = @P_CD_COMPANY,		@P_CD_PLANT= @P_CD_PLANT,		@P_NO_REQ = @NO_RCV, 
			    @P_NO_LINE = 1,						@P_CD_WC = @P_CD_WC,			@P_CD_ITEM = @P_CD_ITEM, 
			    @P_DT_REQ = @P_DT_WORK,				@P_QT_REQ = @P_QT_MOVE,			@P_QT_REQ_W = @P_QT_MOVE, 
			    @P_QT_REQ_B = 0,					@P_YN_QC=N'N',					@P_QT_RCV=0,				
			    @P_CD_SL = @P_CD_SL,				@P_NO_WO = @P_NO_WO,			@P_NO_WORK = @P_NO_WORK, 
			    @P_TP_WB = '0',						@P_DC_RMK = '',					@P_ID_INSERT = @P_ID_INSERT

		    IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END
		    --생산(생산입고의뢰등록)------------------------------------------------------------------------------------
        END

		IF (@YN_AUTORCV_REQ = 'Y' AND @YN_AUTORCV = 'Y')        --작업지시번호의자동입고처리여부(작업지시등록시공장환경설정에서가져온다.)
		BEGIN
			--구매(생산입고등록)------------------------------------------------------------------------------------
			EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_MODULE = 'PU', @P_CD_CLASS = '18', @P_DOCU_YM = @P_DT_WORK, @P_NO = @NO_IO_MM OUTPUT

			IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

			EXEC UP_PU_MM_QTIOH_INSERT @P_NO_IO= @NO_IO_MM, @P_CD_COMPANY = @P_CD_COMPANY, @P_CD_PLANT = @P_CD_PLANT, @P_CD_PARTNER = NULL, @P_FG_TRANS=N'1', @P_YN_RETURN=N'N', @P_DT_IO = @P_DT_WORK, @P_GI_PARTNER = '', @P_CD_DEPT = @P_CD_DEPT, @P_NO_EMP = @P_NO_EMP, @P_DC_RMK=NULL, @P_ID_INSERT = @P_ID_INSERT, @P_CD_QTIOTP = @TP_GR
			
			IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

			--@P_QT_INSP의값을생각해봐야함(현재는그냥)
			EXEC UP_PU_WGR_INSERT 
					@P_YN_RETURN = 'N',				@P_NO_IO = @NO_IO_MM,			@P_NO_IOLINE = 1, 
					@P_CD_COMPANY = @P_CD_COMPANY,	@P_CD_PLANT = @P_CD_PLANT,		@P_CD_SL = @P_CD_SL, 
					@P_DT_IO = @P_DT_WORK,			@P_NO_ISURCV = @NO_RCV,			@P_NO_ISURCVLINE = 1, 
					@P_NO_PSO_MGMT = @P_NO_WO,		@P_NO_PSO_MGMT2 = @P_NO_WORK,	@P_NO_PSOLINE_MGMT = NULL, 
					@P_CD_ITEM = @P_CD_ITEM,		@P_QT_GOOD_INV = @P_QT_MOVE,	@P_QT_REJECT_INV = 0, 
					@P_NO_EMP = @P_NO_EMP,			@P_CD_QTIOTP = @TP_GR,			@P_QT_INSP = 0, 
					@P_NO_WORK = @P_NO_WORK,		@P_CD_PJT = @V_NO_PJT,			@P_FG_IO = '002',
					@P_YN_INSPECT = 'N',			@SEQ_PROJECT = @V_SEQ_PROJECT,	@P_UM_EVAL = 0,
					@P_CD_PARTNER = NULL,			@P_DC_RMK = NULL,				@P_CD_USERDEF1 = NULL,
					@P_CD_USERDEF2 = NULL,			@P_CD_USERDEF3 = NULL,			@P_CD_USERDEF4 = NULL,
					@P_CD_USERDEF5 = NULL,			@P_CD_CC = NULL,				@P_NO_IO_MGMT = NULL,
					@P_CD_WC = @P_CD_WC
			

			IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END
			--구매(생산입고등록)------------------------------------------------------------------------------------

			--생산출고전표(FG_SLIP = '203')의LOT가있으면자재의생산입고LOT(MM_QTIOLOT.FG_IO = '002') 를생선한다.
			--현재공정이마지막공정이면서LOT번호를KEYING한경우자재의생산입고LOT(MM_QTIOLOT.FG_IO = '002') 를생선한다.
			--현재공정이마지막공정이면서LOT번호를작지에서입력한경우자재의생산입고LOT(MM_QTIOLOT.FG_IO = '002') 를생선한다.
			IF (ISNULL(@P_NO_LOT, '') <> '')
			BEGIN
				EXEC UP_MM_QTIOLOT_INSERT @P_CD_COMPANY = @P_CD_COMPANY, @P_NO_IO = @NO_IO_MM, @P_NO_IOLINE = 1, 
					@P_NO_LOT = @P_NO_LOT, @P_CD_ITEM = @P_CD_ITEM, @P_DT_IO = @P_DT_WORK, @P_FG_PS = '1', @P_FG_IO = '002', 
					@P_CD_QTIOTP = '981', @P_CD_SL = @P_CD_SL, @P_QT_IO = @P_QT_MOVE, @P_NO_IO_MGMT = NULL, 
					@P_NO_IOLINE_MGMT = 0, @P_NO_IOLINE_MGMT2 = 0, @P_YN_RETURN = 'N', @CD_PLANT_PR = @P_CD_PLANT, @NO_IO_PR = @P_NO_IO_203, 
					@NO_LINE_IO_PR = @P_NO_IO_LINE_203, @NO_LINE_IO2_PR = 0, @FG_SLIP_PR = '203', @NO_LOT_PR = @P_NO_LOT

				IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END
			END
		END
	END
END
--*******************마지막공정이면자동생산입고의뢰, 자동입고끝***********************************

DECLARE @QT_MOVE_START	NUMERIC(17, 4)

SELECT @QT_MOVE_START = ISNULL(	(	SELECT QT_MOVE
									FROM PR_WO_ROUT
									WHERE CD_COMPANY = @P_CD_COMPANY
										AND NO_WO = @P_NO_WO
										AND CD_OP = @P_BEFORE_CD_OP	), 0)

--공정의작업지시수량(QT_WO)과이동수량(QT_MOVE)이같은건이존재하면마감된것으로간주하여
--공정의상태(ST_OP)를마감으로하고마감일(DT_CLOSE)을입력한다.
IF (	EXISTS	(	SELECT 1
				FROM PR_WO_ROUT
				WHERE CD_COMPANY = @P_CD_COMPANY
					AND NO_WO = @P_NO_WO
					AND CD_OP = @P_CD_OP
					AND ((@P_FIRST_CD_OP = @P_CD_OP AND QT_WO = QT_MOVE + QT_BAD)
						OR (@P_FIRST_CD_OP <> @P_CD_OP AND @QT_MOVE_START = QT_START AND QT_START = QT_MOVE + QT_BAD))
--					AND QT_WO = QT_MOVE	)	)
				)
		AND (EXISTS	(	SELECT 1
						FROM PR_WO_ROUT
						WHERE CD_COMPANY = @P_CD_COMPANY
							AND NO_WO = @P_NO_WO
							AND (CD_OP = @P_BEFORE_CD_OP)
							AND ST_OP = 'C'
				) OR @P_CD_OP = @P_FIRST_CD_OP)
	)
BEGIN
	
	UPDATE PR_WO_ROUT
	SET DT_CLOSE = @P_DT_WORK,
		ST_OP = 'C'
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_WO = @P_NO_WO
		AND CD_OP = @P_CD_OP
		
		
	UPDATE PR_WO_ROUT_REL
	 SET DT_CLOSE = @P_DT_WORK,
		 ST_OP = 'C',
		 YN_CLOSE = 'Y'
   WHERE CD_COMPANY = @P_CD_COMPANY
	 AND CD_PLANT = @P_CD_PLANT
	 AND NO_WO = @P_NO_WO
	 AND CD_OP = @P_CD_OP
	 AND NO_REL = @P_NO_REL
	
		
	IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END

	--마지막공정이마감됐으면작업지시를마감시킨다.(PR_WO.ST_WO = 'C')
	IF (@P_CD_OP = @P_LAST_CD_OP)
	BEGIN
		UPDATE PR_WO
		SET DT_CLOSE = @P_DT_WORK,
			ST_WO = 'C'
		WHERE CD_COMPANY = @P_CD_COMPANY
			AND CD_PLANT = @P_CD_PLANT
			AND NO_WO = @P_NO_WO
			
			
		IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = '[SP_CZ_PR_REWORK_REG_I]작업을정상적으로처리하지못했습니다.' GOTO ERROR END
	END
	
END


-----------작업지시분할----------------------------------------------------------------

IF (	EXISTS	(	SELECT 1
				FROM PR_WO_ROUT_REL
				WHERE CD_COMPANY = @P_CD_COMPANY
					AND NO_WO = @P_NO_WO
					AND CD_OP = @P_CD_OP
					AND NO_REL = @P_NO_REL
					AND ((@P_FIRST_CD_OP = @P_CD_OP AND QT_REL = QT_MOVE + QT_BAD)
						OR (@P_FIRST_CD_OP <> @P_CD_OP AND @QT_MOVE_START = QT_START AND QT_REL = QT_MOVE + QT_BAD))
--					AND QT_WO = QT_MOVE	)	)
				)
		AND (EXISTS	(	SELECT 1
						FROM PR_WO_ROUT_REL
						WHERE CD_COMPANY = @P_CD_COMPANY
							AND NO_WO = @P_NO_WO
							AND NO_REL = @P_NO_REL
							AND (CD_OP = @P_BEFORE_CD_OP)
							AND ST_OP = 'C'
				) OR @P_CD_OP = @P_FIRST_CD_OP)
	)
BEGIN

	
	UPDATE PR_WO_ROUT_REL
	 SET DT_CLOSE = @P_DT_WORK,
		 ST_OP = 'C',
		 YN_CLOSE = 'Y'
   WHERE CD_COMPANY = @P_CD_COMPANY
	 AND CD_PLANT = @P_CD_PLANT
	 AND NO_WO = @P_NO_WO
	 AND CD_OP = @P_CD_OP
	 AND NO_REL = @P_NO_REL
	 
END
	
-----------작업지시분할---------------------------------------------------------------- 
--**********************************************************************************
--*****************************************// 공정이동등록끝



RETURN
ERROR: RAISERROR (@ERRMSG, 18, 1)

END
GO


