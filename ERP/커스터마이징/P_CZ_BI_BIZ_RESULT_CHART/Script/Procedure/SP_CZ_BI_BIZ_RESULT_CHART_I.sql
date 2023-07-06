USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_BI_BIZ_RESULT_CHART_I]    Script Date: 2018-02-12 오전 9:40:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [NEOE].[SP_CZ_BI_BIZ_RESULT_CHART_I]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_DT_ACCT			NVARCHAR(6),
	@P_AM_ACCT			NUMERIC(18, 2),
	@P_ID_INSERT		NVARCHAR(15)
) 
AS
 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

IF EXISTS (SELECT 1 FROM CZ_SA_BIZ_RESULT WHERE CD_COMPANY = @P_CD_COMPANY AND TP_GUBUN = '003' AND CD_DEPT = 'NEW' AND DT_BIZ_RESULT = @P_DT_ACCT)
BEGIN
	UPDATE CZ_SA_BIZ_RESULT
	SET AM_BIZ_RESULT = @P_AM_ACCT,
		ID_UPDATE = @P_ID_INSERT,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND TP_GUBUN = '003'
	AND CD_DEPT = 'NEW'
	AND DT_BIZ_RESULT = @P_DT_ACCT
END
ELSE
BEGIN
	INSERT INTO CZ_SA_BIZ_RESULT
	(
		CD_COMPANY,
		TP_GUBUN,
		CD_DEPT,
		DT_BIZ_RESULT,
		AM_BIZ_RESULT,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES
	(
		@P_CD_COMPANY,
		'003',
		'NEW',
		@P_DT_ACCT,
		@P_AM_ACCT,
		@P_ID_INSERT,
		NEOE.SF_SYSDATE(GETDATE())
	)
END

GO

