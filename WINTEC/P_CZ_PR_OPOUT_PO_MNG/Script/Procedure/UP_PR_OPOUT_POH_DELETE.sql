USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PR_OPOUT_POH_DELETE]    Script Date: 2022-08-11 오전 9:54:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [NEOE].[UP_CZ_PR_OPOUT_POH_DELETE]
(
    @P_CD_COMPANY   NVARCHAR(7),
    @P_CD_PLANT     NVARCHAR(7),
    @P_NO_PO        NVARCHAR(20),
    @P_NM_PAGE		NVARCHAR(30) = NULL
)
AS

DECLARE @ERRMSG			NVARCHAR(255),
		@V_SERVER_KEY	NVARCHAR(50),
		@V_CNT			INT
		
SELECT @V_SERVER_KEY = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG WHERE YN_UPGRADE = 'Y'

BEGIN
	SELECT	@V_CNT = SUM(ISNULL(QT_CLS,0))
	FROM	PR_OPOUT_IVL
	WHERE	CD_COMPANY	= @P_CD_COMPANY
	AND		CD_PLANT	= @P_CD_PLANT
	AND		NO_PO		= @P_NO_PO

	IF (ISNULL(@V_CNT, 0) <> 0)
	BEGIN
		SELECT @ERRMSG = '[UP_PR_OPOUT_POH_DELETE]공정외주매입내역이 있습니다. 외주매입을 먼저 삭제 하세요.' GOTO ERROR END
END

BEGIN
	SELECT	@V_CNT = SUM(ISNULL(QT_RCV,0))
	FROM	PR_OPOUT_POL
	WHERE	CD_COMPANY	= @P_CD_COMPANY
	AND		CD_PLANT	= @P_CD_PLANT
	AND		NO_PO		= @P_NO_PO

	IF(ISNULL(@V_CNT,0) <> 0)
	BEGIN
		SELECT @ERRMSG = '[UP_PR_OPOUT_POH_DELETE]공정외주실적이 있습니다. 실적을 먼저 삭제 하세요.' GOTO ERROR END
END

-- 헤더만 삭제된다고 하여 삭제전 이력 남기기 [이력테이블은 전용 삭제 트리거에서 생성]
IF (@V_SERVER_KEY LIKE 'DINTEC%')
BEGIN
	UPDATE PR_OPOUT_POH
	SET DC_RMK = 'PAGE['+ISNULL(@P_NM_PAGE,'')+'], SP[UP_PR_OPOUT_POH_DELETE]'
	WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_PLANT = @P_CD_PLANT
		AND NO_PO = @P_NO_PO
END

DELETE FROM PR_OPOUT_POL
WHERE CD_COMPANY = @P_CD_COMPANY
    AND CD_PLANT = @P_CD_PLANT
    AND NO_PO = @P_NO_PO

IF (@@ERROR <> 0 ) BEGIN SELECT @ERRMSG = '[UP_PR_OPOUT_POH_DELETE]PR_OPOUT_POL TABLE DELETE ERROR.' GOTO ERROR END

DELETE	
FROM	PR_OPOUT_POH
WHERE	CD_COMPANY	= @P_CD_COMPANY
AND		CD_PLANT	= @P_CD_PLANT
AND		NO_PO		= @P_NO_PO

IF (@@ERROR <> 0 ) BEGIN SELECT @ERRMSG = '[UP_PR_OPOUT_POH_DELETE]PR_OPOUT_POH TABLE DELETE ERROR.' GOTO ERROR END

RETURN
ERROR: RAISERROR (@ERRMSG, 18 , 1)
RETURN
GO

