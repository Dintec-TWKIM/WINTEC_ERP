USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_OPOUT_WORK_INSP_U]    Script Date: 2023-02-09 오전 9:57:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_PR_OPOUT_WORK_INSP_U]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_WO			NVARCHAR(20),
	@P_NO_LINE			NUMERIC(5, 0),
	@P_SEQ_WO			NUMERIC(5, 0),
	@P_NO_INSP			NVARCHAR(8), -- -1 : 불량, 995 : 외주
	@P_NO_HEAT			NVARCHAR(20),
	@P_ID_USER			NVARCHAR(15),
	@P_NO_OPOUT_WORK	NVARCHAR(20),
	@P_CD_REJECT		NVARCHAR(10),
	@P_CD_RESOURCE		NVARCHAR(10)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

UPDATE CZ_PR_WO_INSP
SET NO_INSP = @P_NO_INSP,
	DT_INSP = CONVERT(CHAR(8), GETDATE(), 112),
	NO_HEAT = @P_NO_HEAT,
	NO_EMP = @P_ID_USER,
	DTS_INSERT = NEOE.SF_SYSDATE(GETDATE()),
	ID_INSERT = @P_ID_USER,
	NO_OPOUT_WORK = @P_NO_OPOUT_WORK,
	CD_REJECT = @P_CD_REJECT,
	CD_RESOURCE = @P_CD_RESOURCE
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_WO = @P_NO_WO
AND NO_LINE = @P_NO_LINE
AND SEQ_WO = @P_SEQ_WO
AND NO_INSP = '994'

