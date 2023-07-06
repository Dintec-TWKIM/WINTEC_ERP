USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SOSCH2H_DLV_U]    Script Date: 2015-12-09 오후 8:11:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOSCH2H_DLV_U]  
(  
    @P_CD_COMPANY		NVARCHAR(7),
	@P_NO_SO			NVARCHAR(20),
	@P_DT_DLV			NVARCHAR(8),
	@P_ID_UPDATE		NVARCHAR(15)
)
AS

IF EXISTS (SELECT 1 
		   FROM CZ_SA_SOH_DLV
		   WHERE CD_COMPANY = @P_CD_COMPANY
		   AND NO_SO = @P_NO_SO
		   AND YN_USE = 'Y'
		   AND (DT_DLV <> @P_DT_DLV))
BEGIN
	SELECT CD_COMPANY,
		   NO_SO,
		   (SEQ + 1) AS SEQ,
		   YN_GIR_AUTO,
		   YN_URGENT,
		   YN_CALENDAR,
		   YN_NONGR,
		   CD_DLV_MAIN,
		   CD_DLV_SUB,
		   CD_DLV_TO,
		   @P_DT_DLV AS DT_DLV,
		   TM_DLV,
		   YN_CHARGE,
		   CD_CHARGE,
		   AM_CHARGE,
		   DC_DLV,
		   DT_CUTOFF,
		   TM_CUTOFF,
		   CD_DLV_PO,
		   DT_DLV_PO,
		   TM_DLV_PO,
		   DC_DLV_PO,
		   'Y' AS YN_USE,
		   @P_ID_UPDATE AS ID_INSERT,
		   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
	INTO #CZ_SA_SOH_DLV
	FROM CZ_SA_SOH_DLV
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND NO_SO = @P_NO_SO
	AND YN_USE = 'Y'

	UPDATE CZ_SA_SOH_DLV
	SET YN_USE = 'N',
	    ID_UPDATE = @P_ID_UPDATE,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND NO_SO = @P_NO_SO
	AND YN_USE = 'Y'
	
	INSERT INTO CZ_SA_SOH_DLV
	(
		CD_COMPANY,
		NO_SO,
		SEQ,
		YN_GIR_AUTO,
		YN_URGENT,
		YN_CALENDAR,
		YN_NONGR,
		CD_DLV_MAIN,
		CD_DLV_SUB,
		CD_DLV_TO,
		DT_DLV,
		TM_DLV,
		YN_CHARGE,
		CD_CHARGE,
		AM_CHARGE,
		DC_DLV,
		DT_CUTOFF,
		TM_CUTOFF,
		CD_DLV_PO,
		DT_DLV_PO,
		TM_DLV_PO,
		DC_DLV_PO,
		YN_USE,
		ID_INSERT,
		DTS_INSERT
	)
	SELECT * 
	FROM #CZ_SA_SOH_DLV

	DROP TABLE #CZ_SA_SOH_DLV
END
	
GO

