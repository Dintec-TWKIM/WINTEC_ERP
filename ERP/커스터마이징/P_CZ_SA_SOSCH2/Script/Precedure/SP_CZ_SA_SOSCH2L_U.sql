USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SOSCH2L_U]    Script Date: 2015-12-09 오후 8:11:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOSCH2L_U]  
(  
    @P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
	@P_CD_ITEM			NVARCHAR(20),
	@P_DC_RMK_LOG		NVARCHAR(1000),
	@P_ID_UPDATE		NVARCHAR(15)
)
AS

IF EXISTS (SELECT 1 
		   FROM CZ_MA_PITEM MI
		   WHERE MI.CD_COMPANY = @P_CD_COMPANY
		   AND MI.CD_PLANT = @P_CD_PLANT
		   AND MI.CD_ITEM = @P_CD_ITEM)
BEGIN
	UPDATE CZ_MA_PITEM
	SET DC_RMK_LOG = @P_DC_RMK_LOG,
	    ID_UPDATE = @P_ID_UPDATE,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
    AND CD_PLANT = @P_CD_PLANT
	AND CD_ITEM = @P_CD_ITEM 
END
ELSE
BEGIN
	INSERT INTO CZ_MA_PITEM
	(
		CD_COMPANY,
		CD_PLANT,
		CD_ITEM,
		DC_RMK_LOG,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES
	(
		@P_CD_COMPANY,
		@P_CD_PLANT,
		@P_CD_ITEM,
		@P_DC_RMK_LOG,
		@P_ID_UPDATE,
		NEOE.SF_SYSDATE(GETDATE())
	)
END
	
GO

