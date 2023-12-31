USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_LN_CURR_FORECAST]    Script Date: 2019-06-17 오전 11:55:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [NEOE].[SP_CZ_LN_CURR_FORECAST]
(
	@P_TP_GUBUN		NVARCHAR(100),
	@P_DT_QUARTER	NVARCHAR(10),
	@P_DT_MONTH		NVARCHAR(10),
	@P_RT_USD		NVARCHAR(50)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

IF EXISTS (SELECT 1 
		   FROM CZ_LN_CURR_FORECAST
		   WHERE TP_GUBUN = @P_TP_GUBUN
		   AND DT_QUARTER = @P_DT_QUARTER
		   AND DT_MONTH = @P_DT_MONTH)
BEGIN
	UPDATE CZ_LN_CURR_FORECAST
	SET RT_USD = ISNULL(@P_RT_USD, RT_USD),
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE TP_GUBUN = @P_TP_GUBUN
	AND DT_QUARTER = @P_DT_QUARTER
	AND DT_MONTH = @P_DT_MONTH
END
ELSE
BEGIN
	INSERT INTO CZ_LN_CURR_FORECAST
	(
	    TP_GUBUN,
		DT_QUARTER,
		DT_MONTH,
		RT_USD,
		DTS_INSERT
	)
	VALUES
	(
		@P_TP_GUBUN,
		@P_DT_QUARTER,
		@P_DT_MONTH,
		@P_RT_USD,
		NEOE.SF_SYSDATE(GETDATE())
	)
END

GO

