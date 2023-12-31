USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_ROUT_REG_L_U]    Script Date: 2021-02-18 오후 1:16:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--UP_PR_ROUT_L_U
ALTER PROCEDURE [NEOE].[SP_CZ_PR_ROUT_REG_L_U]
(    
	@P_CD_WCOP          NVARCHAR(7),
	@P_CD_OP			NVARCHAR(4),
	@P_CD_WC			NVARCHAR(7),
	@P_CD_ITEM          NVARCHAR(50),
	@P_NO_OPPATH        NVARCHAR(3),
	@P_CD_PLANT         NVARCHAR(7),
	@P_CD_COMPANY       NVARCHAR(7),
	@P_TM_SETUP         NVARCHAR(6),
	@P_TM				NVARCHAR(6),
	@P_TM_MOVE          NVARCHAR(6),
	@P_CD_RSRC          NVARCHAR(7),
	@P_TP_RSRC          NCHAR(3),
	@P_YN_RECEIPT		NCHAR(1),
	@P_YN_BF			NCHAR(1),
	@P_QT_OVERLAP       NUMERIC(17,4),
	@P_N_OPSPLIT        NUMERIC(3,0),
	@P_YN_PAR			NCHAR(1),
	@P_DY_SBCNT         NUMERIC(3,0),
	@P_CD_TOOL          NVARCHAR(7),
	@P_ID_UPDATE        NVARCHAR(15),
	@P_YN_QC			NCHAR(1),
	@P_YN_FINAL         NVARCHAR(1),
	@P_TP_OPPATH        NVARCHAR(3),
	@P_CD_EQUIP			NVARCHAR(30),
	@P_RT_YIELD			NUMERIC(17,4),
	@P_QT_LABOR_PLAN	NUMERIC(17,4),
	@P_SET_REASON		NVARCHAR(3),
	@P_DC_RMK			NVARCHAR(100),
	@P_SET_METHOD		NVARCHAR(3),
	@P_DY_PLAN			NUMERIC(5,0),
	@P_NO_SFT			NVARCHAR(3),
	@P_YN_ROUT_SU_IV	NVARCHAR(1),
	@P_YN_INSP			NVARCHAR(1),
	@P_DC_OP			NVARCHAR(MAX),
	@P_YN_USE			NVARCHAR(1),
	@P_NUM_USERDEF1		NUMERIC(17, 4),
	@P_NUM_USERDEF2		NUMERIC(17, 4),
	@P_NUM_USERDEF3		NUMERIC(17, 4),
	@P_TXT_USERDEF1		NVARCHAR(4000),
	@P_TXT_USERDEF2		NVARCHAR(200),
	@P_TXT_USERDEF3		NVARCHAR(200),
	@P_CD_USERDEF1		NVARCHAR(3),
	@P_CD_USERDEF2		NVARCHAR(3),
	@P_CD_USERDEF3		NVARCHAR(3),
	@P_NUM_USERDEF4		NUMERIC(17, 4),
	@P_NUM_USERDEF5		NUMERIC(17, 4),
	@P_NUM_USERDEF6		NUMERIC(17, 4),
	@P_NUM_USERDEF7		NUMERIC(17, 4),
	@P_NUM_USERDEF8		NUMERIC(17, 4),
	@P_NUM_USERDEF9		NUMERIC(17, 4),
	@P_TXT_USERDEF4		NVARCHAR(200),
	@P_TXT_USERDEF5		NVARCHAR(200),
	@P_TXT_USERDEF6		NVARCHAR(200),
	@P_TXT_USERDEF7		NVARCHAR(200),
	@P_TXT_USERDEF8		NVARCHAR(200),
	@P_TXT_USERDEF9		NVARCHAR(200)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_ERRMSG NVARCHAR(255)   -- ERROR 메시지

BEGIN TRAN SP_CZ_PR_ROUT_REG_L_U
BEGIN TRY

IF EXISTS (SELECT 1
		   FROM PR_WO WO
		   JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
		   WHERE WO.CD_COMPANY = @P_CD_COMPANY
		   AND WO.CD_PLANT = @P_CD_PLANT
		   AND WO.PATN_ROUT = @P_NO_OPPATH
		   AND WO.CD_ITEM = @P_CD_ITEM
		   AND WR.CD_OP = @P_CD_OP) AND EXISTS (SELECT 1 
										        FROM PR_ROUT_L RL
										        WHERE RL.CD_OP = @P_CD_OP
										        AND RL.CD_ITEM = @P_CD_ITEM
										        AND RL.NO_OPPATH = @P_NO_OPPATH
										        AND RL.TP_OPPATH = @P_TP_OPPATH
										        AND RL.CD_PLANT = @P_CD_PLANT
										        AND RL.CD_COMPANY = @P_CD_COMPANY
										        AND (RL.CD_WCOP <> @P_CD_WCOP OR RL.CD_WC <> @P_CD_WC))
BEGIN
	SELECT @V_ERRMSG = '작업 진행 중인 공정은 수정 할 수 없습니다.'                     
	GOTO ERROR	
END

UPDATE PR_ROUT_L
SET	CD_WCOP = @P_CD_WCOP,
	CD_WC = @P_CD_WC,
	TM_SETUP = @P_TM_SETUP,
	TM = @P_TM,
	TM_MOVE = @P_TM_MOVE,
	CD_RSRC = @P_CD_RSRC,
	TP_RSRC = @P_TP_RSRC,
	YN_RECEIPT = @P_YN_RECEIPT,
	YN_BF = @P_YN_BF,
	QT_OVERLAP = @P_QT_OVERLAP,
	N_OPSPLIT = @P_N_OPSPLIT,
	YN_PAR = @P_YN_PAR,
	DY_SBCNT = @P_DY_SBCNT,
	CD_TOOL = @P_CD_TOOL,
	ID_UPDATE = @P_ID_UPDATE,
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE()),
	YN_QC = @P_YN_QC,
	YN_FINAL = @P_YN_FINAL,
	TP_OPPATH = @P_TP_OPPATH, 
	CD_EQUIP = @P_CD_EQUIP, 
	RT_YIELD = @P_RT_YIELD,
	QT_LABOR_PLAN = @P_QT_LABOR_PLAN,
	SET_REASON = @P_SET_REASON,
	DC_RMK = @P_DC_RMK,
	SET_METHOD = @P_SET_METHOD,
	DY_PLAN = @P_DY_PLAN,
	NO_SFT = @P_NO_SFT,
	YN_ROUT_SU_IV = @P_YN_ROUT_SU_IV,
	YN_INSP = @P_YN_INSP,
	DC_OP = @P_DC_OP,
	YN_USE = @P_YN_USE,
	NUM_USERDEF1 = @P_NUM_USERDEF1,
	NUM_USERDEF2 = @P_NUM_USERDEF2,
	NUM_USERDEF3 = @P_NUM_USERDEF3,
	TXT_USERDEF1 = @P_TXT_USERDEF1,
	TXT_USERDEF2 = @P_TXT_USERDEF2,
	TXT_USERDEF3 = @P_TXT_USERDEF3,
	CD_USERDEF1 = @P_CD_USERDEF1,
	CD_USERDEF2 = @P_CD_USERDEF2,
	CD_USERDEF3 = @P_CD_USERDEF3,
	NUM_USERDEF4 = @P_NUM_USERDEF4,
	NUM_USERDEF5 = @P_NUM_USERDEF5,
	NUM_USERDEF6 = @P_NUM_USERDEF6,
	NUM_USERDEF7 = @P_NUM_USERDEF7,
	NUM_USERDEF8 = @P_NUM_USERDEF8,
	NUM_USERDEF9 = @P_NUM_USERDEF9,
	TXT_USERDEF4 = @P_TXT_USERDEF4,
	TXT_USERDEF5 = @P_TXT_USERDEF5,
	TXT_USERDEF6 = @P_TXT_USERDEF6,
	TXT_USERDEF7 = @P_TXT_USERDEF7,
	TXT_USERDEF8 = @P_TXT_USERDEF8,
	TXT_USERDEF9 = @P_TXT_USERDEF9
WHERE CD_OP = @P_CD_OP
AND CD_ITEM = @P_CD_ITEM
AND NO_OPPATH = @P_NO_OPPATH
AND TP_OPPATH = @P_TP_OPPATH
AND CD_PLANT = @P_CD_PLANT
AND CD_COMPANY = @P_CD_COMPANY

COMMIT TRAN SP_CZ_PR_ROUT_REG_L_U

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_PR_ROUT_REG_L_U
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN
ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK TRAN SP_CZ_PR_ROUT_REG_L_U

GO