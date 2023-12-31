USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_INQ_REGL_D]    Script Date: 2015-07-03 오후 8:04:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_INQ_REGL_D]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_TP_STEP		NVARCHAR(3),
	@P_NO_KEY		NVARCHAR(20),
	@P_NO_LINE		INT
) AS	

DELETE FROM CZ_MA_WORKFLOWL
WHERE CD_COMPANY = @P_CD_COMPANY
AND TP_STEP = @P_TP_STEP
AND NO_KEY = @P_NO_KEY
AND NO_LINE	= @P_NO_LINE

IF @P_TP_STEP <> '01' AND NOT EXISTS (SELECT 1 FROM CZ_MA_WORKFLOWL WHERE CD_COMPANY = @P_CD_COMPANY AND TP_STEP = @P_TP_STEP AND NO_KEY = @P_NO_KEY)
BEGIN
	DELETE FROM CZ_MA_WORKFLOWH
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND TP_STEP = @P_TP_STEP
	AND NO_KEY = @P_NO_KEY
END

GO

