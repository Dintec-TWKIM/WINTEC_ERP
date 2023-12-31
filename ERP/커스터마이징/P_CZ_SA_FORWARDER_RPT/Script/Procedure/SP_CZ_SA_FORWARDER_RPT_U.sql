USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_FORWARDER_RPT_U]    Script Date: 2016-01-27 오후 4:47:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 
ALTER PROCEDURE [NEOE].[SP_CZ_SA_FORWARDER_RPT_U]
(
    @P_CD_COMPANY		NVARCHAR(7),
	@P_NO_GIR			NVARCHAR(20),
	@P_WEIGHT			NUMERIC(17,4),
	@P_CD_SUB_CATEGORY	NVARCHAR(3),
	@P_AM_FORWARDER_A	NUMERIC(14,2),
	@P_AM_FORWARDER_B	NUMERIC(14,2),
	@P_AM_FORWARDER_C	NUMERIC(14,2),
	@P_CD_FORWARDER		NVARCHAR(3),
	@P_FG_REASON	    NVARCHAR(1),
	@P_ID_UPDATE		NVARCHAR(15)
)
AS

UPDATE CZ_SA_GIRH_WORK_DETAIL
SET WEIGHT = @P_WEIGHT,
	CD_SUB_CATEGORY = @P_CD_SUB_CATEGORY,
	CD_FORWARDER = @P_CD_FORWARDER,
	FG_REASON = @P_FG_REASON,
    ID_UPDATE = @P_ID_UPDATE,
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_GIR = @P_NO_GIR

UPDATE CZ_SA_GIRH_FORWARDER
SET AM_PRICE = @P_AM_FORWARDER_A,
    ID_UPDATE = @P_ID_UPDATE,
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_GIR = @P_NO_GIR
AND CD_FORWARDER = '001'

UPDATE CZ_SA_GIRH_FORWARDER
SET AM_PRICE = @P_AM_FORWARDER_B,
    ID_UPDATE = @P_ID_UPDATE,
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_GIR = @P_NO_GIR
AND CD_FORWARDER = '002'

UPDATE CZ_SA_GIRH_FORWARDER
SET AM_PRICE = @P_AM_FORWARDER_C,
    ID_UPDATE = @P_ID_UPDATE,
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_GIR = @P_NO_GIR
AND CD_FORWARDER = '004'


GO

