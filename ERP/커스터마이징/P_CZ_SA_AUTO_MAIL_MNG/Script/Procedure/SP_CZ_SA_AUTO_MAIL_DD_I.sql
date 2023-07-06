USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_AUTO_MAIL_DD_I]    Script Date: 2016-01-13 오후 9:02:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
 
ALTER PROCEDURE [NEOE].[SP_CZ_SA_AUTO_MAIL_DD_I]    
(            
	@P_CD_COMPANY		NVARCHAR(7),
	@P_TP_TYPE			NVARCHAR(1),
	@P_NO_SO			NVARCHAR(20),
	@P_NO_KEY			NVARCHAR(20),
	@P_DT_LIMIT			NVARCHAR(8),
	@P_DT_EXPECT		NVARCHAR(8),
	@P_DT_REPLY			NVARCHAR(8),
	@P_YN_EXCLUDE		NVARCHAR(1),
	@P_YN_URGENT		NVARCHAR(1),
	@P_DC_RMK			NVARCHAR(MAX),
	@P_DC_RMK1			NVARCHAR(MAX),
	@P_CD_RMK			NVARCHAR(4),
	@P_ID_INSERT		NVARCHAR(15)
)            
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

IF NOT EXISTS (SELECT 1 
			   FROM CZ_SA_DEFERRED_DELIVERY 
			   WHERE CD_COMPANY = @P_CD_COMPANY
			   AND TP_TYPE = @P_TP_TYPE
			   AND NO_SO = @P_NO_SO
			   AND NO_KEY = @P_NO_KEY
			   AND DT_LIMIT = @P_DT_LIMIT)
BEGIN
	INSERT INTO CZ_SA_DEFERRED_DELIVERY
	(
		CD_COMPANY,
		TP_TYPE,
		NO_SO,
		NO_KEY,
		DT_LIMIT,
		DT_EXPECT,
		DT_REPLY,
		YN_EXCLUDE,
		YN_URGENT,
		DC_RMK,
		DC_RMK1,
		CD_RMK,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES
	(
		@P_CD_COMPANY,
		@P_TP_TYPE,
		@P_NO_SO,
		@P_NO_KEY,
		@P_DT_LIMIT,
		@P_DT_EXPECT,
		@P_DT_REPLY,
		@P_YN_EXCLUDE,
		@P_YN_URGENT,
		@P_DC_RMK,
		@P_DC_RMK1,
		@P_CD_RMK,
		@P_ID_INSERT,
		NEOE.SF_SYSDATE(GETDATE())
	)
END
ELSE
BEGIN
	UPDATE CZ_SA_DEFERRED_DELIVERY
	SET YN_EXCLUDE	= @P_YN_EXCLUDE,
		YN_URGENT	= @P_YN_URGENT,
	    DT_EXPECT	= @P_DT_EXPECT,
	    DT_REPLY	= @P_DT_REPLY,
	    DC_RMK		= @P_DC_RMK,
		DC_RMK1		= @P_DC_RMK1,
		CD_RMK		= @P_CD_RMK,
		ID_UPDATE	= @P_ID_INSERT,
		DTS_UPDATE	= NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND TP_TYPE = @P_TP_TYPE
	AND NO_SO = @P_NO_SO
	AND NO_KEY = @P_NO_KEY
	AND DT_LIMIT = @P_DT_LIMIT
END

GO