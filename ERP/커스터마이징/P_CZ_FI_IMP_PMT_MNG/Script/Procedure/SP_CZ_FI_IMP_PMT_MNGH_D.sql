USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_TAX_REG_U]    Script Date: 2015-08-11 오전 11:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_FI_IMP_PMT_MNGH_D] 
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_IMPORT		NVARCHAR(14)
) 
AS

DELETE FROM CZ_FI_IMP_PMTH
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_IMPORT = @P_NO_IMPORT

GO