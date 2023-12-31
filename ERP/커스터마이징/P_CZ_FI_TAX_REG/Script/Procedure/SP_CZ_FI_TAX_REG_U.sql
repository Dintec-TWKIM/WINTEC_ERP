USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_TAX_REG_U]    Script Date: 2015-08-11 오전 11:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_FI_TAX_REG_U] 
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_IV			NVARCHAR(20),
	@P_NO_IO			NVARCHAR(20),
	@P_NO_SO			NVARCHAR(20),
	@P_NO_TO			NVARCHAR(20),
	@P_DT_TO			NVARCHAR(8),
	@P_CD_EXCH			NVARCHAR(3),
	@P_RT_EXCH			NUMERIC(15, 4),
	@P_AM_EX			NUMERIC(19, 4),
	@P_AM				NUMERIC(19, 4),
	@P_DT_SHIPPING		NVARCHAR(8),
	@P_DC_RMK			NVARCHAR(550),
	@P_ID_UPDATE		NVARCHAR(15)
) 
AS

IF EXISTS (SELECT 1 FROM CZ_FI_TAX WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV AND NO_IO = @P_NO_IO AND NO_SO = @P_NO_SO)
BEGIN
	UPDATE CZ_FI_TAX
	SET NO_TO = REPLACE(REPLACE(@P_NO_TO, '-', ''), '_', ''),
		DT_TO = @P_DT_TO,
		CD_EXCH = @P_CD_EXCH,
		RT_EXCH = @P_RT_EXCH,
		AM_EX = @P_AM_EX,
		AM = @P_AM,
		DT_SHIPPING = @P_DT_SHIPPING,
		DC_RMK = @P_DC_RMK,
		ID_UPDATE = @P_ID_UPDATE,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND NO_IV = @P_NO_IV
	AND NO_IO = @P_NO_IO
	AND NO_SO = @P_NO_SO
END
ELSE
BEGIN
	INSERT INTO CZ_FI_TAX
	(
		CD_COMPANY,
		NO_IV,
		NO_IO,
		NO_SO,
		NO_TO,
		DT_TO,
		CD_EXCH,
		RT_EXCH,
		AM_EX,
		AM,
		DT_SHIPPING,
		DC_RMK,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES
	(
		@P_CD_COMPANY,
		@P_NO_IV,
		@P_NO_IO,
		@P_NO_SO,
		REPLACE(REPLACE(@P_NO_TO, '-', ''), '_', ''),
		@P_DT_TO,
		@P_CD_EXCH,
		@P_RT_EXCH,
		@P_AM_EX,
		@P_AM,
		@P_DT_SHIPPING,
		@P_DC_RMK,
		@P_ID_UPDATE,
		NEOE.SF_SYSDATE(GETDATE())
	)
END


GO