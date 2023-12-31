USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_EXCHANGE_D]    Script Date: 2015-06-18 오후 1:52:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_MA_EXCHANGE_D]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_YYMMDD		NVARCHAR(8),
	@P_CURR_SOUR	NVARCHAR(3), 
	@P_CURR_DEST	NVARCHAR(3), 
	@P_NO_SEQ		NUMERIC(2,0)
) AS

SET NOCOUNT ON

DELETE FROM	MA_EXCHANGE
WHERE CD_COMPANY = @P_CD_COMPANY
AND	YYMMDD = @P_YYMMDD
AND	NO_SEQ = @P_NO_SEQ
AND	CURR_SOUR = @P_CURR_SOUR	
AND	CURR_DEST = @P_CURR_DEST

DELETE FROM	CZ_MA_EXCHANGE
WHERE CD_COMPANY = @P_CD_COMPANY
AND	YYMMDD = @P_YYMMDD
AND	NO_SEQ = @P_NO_SEQ
AND	CURR_SOUR = @P_CURR_SOUR	
AND	CURR_DEST = @P_CURR_DEST

SET NOCOUNT OFF
GO

