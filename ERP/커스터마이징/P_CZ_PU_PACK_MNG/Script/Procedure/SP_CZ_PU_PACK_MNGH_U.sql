USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_PACK_MNGH_U]    Script Date: 2016-07-05 오후 6:14:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PU_PACK_MNGH_U]
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_NO_PO				NVARCHAR(20),
	@P_NO_PACK				NUMERIC(5, 0),
	@P_DC_RMK				NVARCHAR(MAX), 
	@P_ID_UPDATE			NVARCHAR(15)
) 
AS 

UPDATE CZ_PU_PACKH			
SET	DC_RMK = @P_DC_RMK, 
	ID_UPDATE = @P_ID_UPDATE, 
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = @P_CD_COMPANY 
AND NO_PO = @P_NO_PO
AND NO_PACK = @P_NO_PACK


GO

