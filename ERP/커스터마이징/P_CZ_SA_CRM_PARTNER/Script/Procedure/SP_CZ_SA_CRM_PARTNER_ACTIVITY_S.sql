USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CUST_REL_MNG_PIC_COMMENT_S]    Script Date: 2017-06-08 오후 7:52:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_PARTNER_ACTIVITY_S]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_PARTNER	NVARCHAR(20)
) 
AS
 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT PA.CD_COMPANY,
	   PA.CD_PARTNER,
	   PA.NO_INDEX,
	   PA.YN_TODO,
	   PA.NM_TITLE,
	   PA.DT_START,
	   PA.DT_END,
	   PA.NO_PIC,
	   PA.NM_PIC,
	   PA.NO_EMP,
	   PA.NM_EMP,
	   PA.DC_ACTIVITY,
	   PA.ID_INSERT 
FROM CZ_CRM_PARTNER_ACTIVITY PA
WHERE PA.CD_COMPANY = @P_CD_COMPANY
AND PA.CD_PARTNER = @P_CD_PARTNER

GO