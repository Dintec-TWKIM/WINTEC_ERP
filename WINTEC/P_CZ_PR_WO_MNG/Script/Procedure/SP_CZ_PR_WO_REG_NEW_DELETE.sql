USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WO_REG_NEW_DELETE]    Script Date: 2021-04-29 오후 2:16:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- UP_PR_WO_REG_NEW_DELETE
ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_REG_NEW_DELETE] 
( 
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_PLANT		NVARCHAR(7),
	@P_NO_WO		NVARCHAR(20)
) 
AS 

DECLARE @ERRMSG			NVARCHAR(255), 
		@V_SERVER_KEY	NVARCHAR(50),
		@V_FG_WO		NVARCHAR(3),
		@V_FG_AUTO		NVARCHAR(3),
		@V_ST_WO		NVARCHAR(1)

SELECT	@V_SERVER_KEY = MAX(SERVER_KEY)
FROM	CM_SERVER_CONFIG
WHERE	YN_UPGRADE = 'Y'
	
BEGIN
	--오더형태등록-자동프로세스처리 => '000' : 사용안함, '005' : 작업지시 + 작업REL, '015' : 작업지시 + 작업REL + 작업실적
	SELECT	@V_FG_AUTO = T.FG_AUTO,
			@V_ST_WO   = W.ST_WO,
			@V_FG_WO = W.FG_WO
	FROM	PR_WO W 
	INNER JOIN PR_TPWO T ON T.CD_COMPANY = W.CD_COMPANY AND T.TP_WO = W.TP_ROUT
	WHERE	W.CD_COMPANY = @P_CD_COMPANY 
	AND		W.CD_PLANT	 = @P_CD_PLANT
	AND		W.NO_WO		 = @P_NO_WO
								
	IF (@V_FG_AUTO IS NULL OR @V_FG_AUTO = '') SET @V_FG_AUTO = '000'
	IF (@V_ST_WO IS NULL OR @V_ST_WO = '') SET @V_ST_WO = 'C'
	IF (@V_FG_WO IS NULL OR @V_FG_WO = '') SET @V_ST_WO = ''
	
	IF (@V_ST_WO IN ('S', 'C') AND @V_FG_AUTO <> '015') 
	BEGIN 
		SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE]작업지시상태가 실적을 넣은 이후(START) 이후의 자료는 삭제할 수 없습니다. 작업지시상태를 확인해 주십시오.' 
		GOTO ERROR 
	END 

	IF EXISTS (SELECT 1 
		       FROM CZ_PR_WO_REQ_D WD
		       WHERE WD.CD_COMPANY = @P_CD_COMPANY
		       AND WD.NO_WO = @P_NO_WO)
	BEGIN
		SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] 생산 투입 중인 작업지시는 삭제 할 수 없습니다.'                     
		GOTO ERROR	
	END
			
	--코드관리(PR_0000007[작업지시구분]) => '001' : MPS, '002' : MRP, '003' : 비계획, '004' : 자재소요 
	IF (@V_FG_WO = '002')
	BEGIN 
		UPDATE	PR_MRP_REQ 
		SET		YN_CONFIRM  = 'N', 
				NO_REQ		= NULL, 
				FG_REQ		= NULL, 
				NO_LINE_REQ = 0 
		WHERE	CD_COMPANY	= @P_CD_COMPANY 
		AND		CD_PLANT	= @P_CD_PLANT 
		AND		NO_REQ		= @P_NO_WO 

		IF (@@ERROR <> 0 ) 
		BEGIN 
			SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_MRP_REQ[UPDATE]작업을 정상적으로 처리하지 못했습니다.' 
			GOTO ERROR 
		END 
	END 
	ELSE IF (@V_FG_WO = '004')
	BEGIN 
		UPDATE	PR_MRQL 
		SET		NO = '' 
		WHERE	CD_COMPANY	= @P_CD_COMPANY 
		AND		CD_PLANT	= @P_CD_PLANT 
		AND		NO			= @P_NO_WO 

		IF (@@ERROR <> 0 ) 
		BEGIN 
			SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_MRQL[UPDATE]작업을 정상적으로 처리하지 못했습니다.' 
			GOTO ERROR 
		END 


		UPDATE	PR_MRQL_TA 
		SET		NO_IO = '' 
		WHERE	CD_COMPANY	= @P_CD_COMPANY 
		AND		CD_PLANT	= @P_CD_PLANT 
		AND		NO_IO		= @P_NO_WO 

		IF (@@ERROR <> 0 ) 
		BEGIN 
			SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_MRQL_TA[UPDATE]작업을 정상적으로 처리하지 못했습니다.' 
			GOTO ERROR 
		END 


		UPDATE	PR_MRQL_DT 
		SET		NO = '' 
		WHERE	CD_COMPANY	= @P_CD_COMPANY 
		AND		CD_PLANT	= @P_CD_PLANT 
		AND		NO			= @P_NO_WO 

		IF (@@ERROR <> 0 ) 
		BEGIN 
			SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_MRQL_DT[UPDATE]작업을 정상적으로 처리하지 못했습니다.' 
			GOTO ERROR 
		END 
	END 
	ELSE IF (@V_FG_WO = '001')
	BEGIN 
		UPDATE	PR_SPLAN 
		SET		NO_PROC		 = '', 
				NO_PROC_LINE = 0, 
				FG_PROC		 = '', 
				ST_PROC		 = 'N' 
		WHERE	CD_COMPANY	= @P_CD_COMPANY 
		AND		CD_PLANT	= @P_CD_PLANT 
		AND		NO_PROC		= @P_NO_WO 

		IF (@@ERROR <> 0 ) 
		BEGIN 
			SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_SPLAN[UPDATE]작업을 정상적으로 처리하지 못했습니다.' 
			GOTO ERROR 
		END 
	END 
	
	--자동실적 사용의 경우, 수동으로 실적 삭제시에는 아래 구문 안타도록
	IF (@V_FG_AUTO = '015' AND @V_ST_WO IN ('S', 'C'))
	BEGIN 
		EXEC UP_PR_WORK_REG_NO_WO_DELETE @P_CD_COMPANY, @P_CD_PLANT, @P_NO_WO
		
		IF (@@ERROR <> 0 ) 
		BEGIN 
			SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] UP_PR_WORK_REG_NO_WO_DELETE SP CALL ERROR.'
			GOTO ERROR 
		END 
	END 
	
	--자동릴리즈 사용의 경우, 수동으로 릴리즈 취소시에는 아래 구문 안타도록
	IF (@V_FG_AUTO IN ('005', '015') AND @V_ST_WO <> 'O')
	BEGIN 
		EXEC UP_PR_WO_REL_INSERT	@P_CD_COMPANY	= @P_CD_COMPANY,	@P_CD_PLANT		= @P_CD_PLANT,
									@P_NO_WO		= @P_NO_WO,			@P_ST_WO		= 'O',
									@P_DT_RELEASE	= NULL,				@P_QT_ITEM		= 0,
									@P_ID_INSERT	= NULL,				@P_NO_LOT		= NULL,
									@P_DC_RMK		= NULL,				@P_PATN_ROUT	= NULL
		IF (@@ERROR <> 0 ) 
		BEGIN 
			SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] UP_PR_WO_REL_INSERT SP CALL ERROR.'
			GOTO ERROR 
		END 
	END 


	DELETE	PR_WO_ROUT 
	WHERE	CD_COMPANY	= @P_CD_COMPANY 
	AND		CD_PLANT	= @P_CD_PLANT 
	AND		NO_WO		= @P_NO_WO 

	IF (@@ERROR <> 0 ) 
	BEGIN 
		SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_WO_ROUT[DELETE]작업을 정상적으로 처리하지 못했습니다.' 
		GOTO ERROR 
	END 


	DELETE	PR_WO_BILL 
	WHERE	CD_COMPANY	= @P_CD_COMPANY 
	AND		CD_PLANT	= @P_CD_PLANT 
	AND		NO_WO		= @P_NO_WO 

	IF (@@ERROR <> 0 ) 
	BEGIN 
		SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_WO_BILL[DELETE]작업을 정상적으로 처리하지 못했습니다.' 
		GOTO ERROR 
	END 


	-- 우선은 해당 테이블을 지운다. 
	-- 나중에 문제가 생기면 다시 복구할 예정(그럴 일은 절대 없을것으로 판단) -_-;; 
	---- 연산품, 반제품 작업지시 삭제 
	--DELETE	PR_WO_SIDE 
	--WHERE		CD_COMPANY = @P_CD_COMPANY 
	--AND		CD_PLANT = @P_CD_PLANT 
	--AND		NO_WO = @P_NO_WO 
	-- 
	--IF (@@ERROR <> 0 ) 
	--BEGIN 
	--	SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_WO_SIDE[DELETE]작업을 정상적으로 처리하지 못했습니다.' 
	--	GOTO ERROR 
	--END 


	--작업지시의 수주추적테이블(PR_SA_SOL_PR_WO_MAPPING)을 삭제한다. 
	--CD_COMPANY, CD_PLANT, NO_SO, NO_LINE, NO_WO 
	DELETE	PR_SA_SOL_PR_WO_MAPPING 
	WHERE	CD_COMPANY	= @P_CD_COMPANY 
	AND		CD_PLANT	= @P_CD_PLANT 
	AND		NO_WO		= @P_NO_WO 

	DELETE	CZ_PR_SA_SOL_PR_WO_MAPPING 
	WHERE	CD_COMPANY	= @P_CD_COMPANY 
	AND		CD_PLANT	= @P_CD_PLANT 
	AND		NO_WO		= @P_NO_WO 

	IF (@@ERROR <> 0 ) 
	BEGIN 
		SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_SA_SOL_PR_WO_MAPPING[DELETE]작업을 정상적으로 처리하지 못했습니다.' 
		GOTO ERROR 
	END 


	--작업지시의 생산요청테이블(PR_PRQ_WO_LINK)을 삭제한다. 
	--CD_COMPANY, NO_PRQ, NO_PRQ_LINE, NO_WO 
	DELETE	PR_PRQ_WO_LINK 
	WHERE	CD_COMPANY	= @P_CD_COMPANY 
	AND		CD_PLANT	= @P_CD_PLANT 
	AND		NO_WO		= @P_NO_WO 

	IF (@@ERROR <> 0 ) 
	BEGIN 
		SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_PRQ_WO_LINK[DELETE]작업을 정상적으로 처리하지 못했습니다.' 
		GOTO ERROR 
	END 


	DELETE	PR_WO 
	WHERE	CD_COMPANY	= @P_CD_COMPANY 
	AND		CD_PLANT	= @P_CD_PLANT 
	AND		NO_WO		= @P_NO_WO 

	IF (@@ERROR <> 0 ) 
	BEGIN 
		SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_WO[DELETE]작업을 정상적으로 처리하지 못했습니다.' 
		GOTO ERROR 
	END 


	--김정문 알로에만 추가함.
	--MRP에서 생성된 작지 삭제하도록함
	IF(@V_SERVER_KEY = 'ALOE') 
	BEGIN
		DELETE	PR_MRP_REQL
		WHERE	CD_COMPANY	= @P_CD_COMPANY
		AND		CD_PLANT	= @P_CD_PLANT
		AND		NO_REQ		= @P_NO_WO
		
		IF (@@ERROR <> 0 ) 
		BEGIN 
			SELECT @ERRMSG = '[UP_PR_WO_REG_NEW_DELETE] PR_MRP_REQL[DELETE]작업을 정상적으로 처리하지 못했습니다.' 
			GOTO ERROR 
		END 
	END
END

RETURN 
ERROR: RAISERROR (@ERRMSG, 18, 1)
RETURN
GO

