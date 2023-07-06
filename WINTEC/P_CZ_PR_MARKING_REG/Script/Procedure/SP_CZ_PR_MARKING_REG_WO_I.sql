USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MARKING_REG_WO_I]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_MARKING_REG_WO_I]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_WO			NVARCHAR(20),
	@P_NO_LINE			NUMERIC(5, 0),
	@P_ID_INSERT		NVARCHAR(15)
)
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

DECLARE @V_CD_PLANT			NVARCHAR(7)
DECLARE @V_CD_ITEM			NVARCHAR(20)
DECLARE @V_CD_WC			NVARCHAR(7)
DECLARE @V_CD_OP			NVARCHAR(4)
DECLARE @V_CD_WCOP			NVARCHAR(4)
DECLARE @V_CD_EQUIP			NVARCHAR(30)
DECLARE @V_CD_SL_IN			NVARCHAR(7)
DECLARE @V_CD_SL_BAD_IN		NVARCHAR(7)
DECLARE @V_NO_MES			NVARCHAR(20)
DECLARE @V_QT_WORK		    NUMERIC(17, 4)
DECLARE @V_ERRMSG			VARCHAR(255)

BEGIN TRAN SP_CZ_PR_MARKING_REG_WO_I
BEGIN TRY

SELECT @V_CD_PLANT = WO.CD_PLANT,
	   @V_CD_ITEM = WO.CD_ITEM,
	   @V_CD_WC = WR.CD_WC,
	   @V_CD_OP = WR.CD_OP,
	   @V_CD_WCOP = WR.CD_WCOP,
	   @V_CD_EQUIP = WR.CD_EQUIP,
	   @V_CD_SL_IN = MI.CD_SL,
	   @V_CD_SL_BAD_IN = OP.CD_SL_BAD,
	   @V_QT_WORK = ISNULL(WM.QT_MARKING, 0) - ISNULL(WR.QT_WORK, 0)
FROM PR_WO WO
LEFT JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = WO.CD_COMPANY AND MI.CD_PLANT = WO.CD_PLANT AND MI.CD_ITEM = WO.CD_ITEM
LEFT JOIN PR_WCOP OP ON OP.CD_COMPANY = WO.CD_COMPANY AND OP.CD_PLANT = WO.CD_PLANT AND OP.CD_WC = WR.CD_WC AND OP.CD_WCOP = WR.CD_WCOP
LEFT JOIN (SELECT WM.CD_COMPANY, WM.NO_WO, WM.NO_LINE,
		   	      COUNT(1) AS QT_MARKING 
		   FROM CZ_PR_WO_INSP WM
		   WHERE WM.NO_INSP = 998 
		   AND WM.YN_MARKING = 'Y'
		   GROUP BY WM.CD_COMPANY, WM.NO_WO, WM.NO_LINE) WM
ON WM.CD_COMPANY = WO.CD_COMPANY AND WM.NO_WO = WO.NO_WO AND WM.NO_LINE = WR.NO_LINE
WHERE WO.CD_COMPANY = @P_CD_COMPANY
AND WO.NO_WO = @P_NO_WO
AND WR.NO_LINE = @P_NO_LINE

IF @V_QT_WORK > 0
BEGIN
	EXEC SP_CZ_PR_LINK_MES_I @P_CD_COMPANY,
							 @V_CD_PLANT,
							 @V_CD_ITEM,
							 @P_ID_INSERT,
							 @V_CD_WC,
							 @V_CD_OP,
							 @V_CD_WCOP,
							 'N',
							 @P_NO_WO,
							 'Y',
							 @V_QT_WORK,
							 0,
							 0,
							 @V_CD_SL_IN,
							 @V_CD_SL_BAD_IN,
							 'N',
							 'N',
							 0,
							 0,
							 0,
							 '',
							 '',
							 @V_CD_EQUIP,
							 '',
							 '',
							 NULL,
							 @P_ID_INSERT,
							 @V_NO_MES OUTPUT

	EXEC SP_CZ_PR_LINK_MES_BATCH @P_CD_COMPANY, @V_CD_PLANT, @V_NO_MES
END
ELSE
BEGIN
	SET @V_ERRMSG = '실적등록 대상 수량이 없습니다.'
    GOTO ERROR
END

COMMIT TRAN SP_CZ_PR_MARKING_REG_WO_I

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_PR_MARKING_REG_WO_I
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN
ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK TRAN SP_CZ_PR_MARKING_REG_WO_I

GO