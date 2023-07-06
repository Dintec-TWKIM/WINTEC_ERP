USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WO_MNG_U]    Script Date: 2021-05-13 오후 1:28:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_MNG_U]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_PLANT		NVARCHAR(7),
	@P_NO_WO		NVARCHAR(20),
	@P_NO_LINE		NUMERIC(5, 0),
	@P_CD_EQUIP		NVARCHAR(30),
	@P_DC_RMK		NVARCHAR(100),
	@P_DC_RMK_1		NVARCHAR(100),
	@P_ID_UPDATE	NVARCHAR(15)
)
AS

DECLARE @V_DTS_UPDATE	NVARCHAR(16)

SELECT	@V_DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())

UPDATE PR_WO_ROUT
SET CD_EQUIP = @P_CD_EQUIP,
	DC_RMK = @P_DC_RMK,
    DC_RMK_1   = @P_DC_RMK_1,
    ID_UPDATE  = @P_ID_UPDATE,
    DTS_UPDATE = @V_DTS_UPDATE
WHERE CD_COMPANY = @P_CD_COMPANY
AND CD_PLANT   = @P_CD_PLANT
AND NO_WO      = @P_NO_WO
AND NO_LINE    = @P_NO_LINE