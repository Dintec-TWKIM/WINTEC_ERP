USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CUST_REL_MNG_PIC_COMMENT_S]    Script Date: 2017-06-08 오후 7:52:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_PARTNER_MEMO_S]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_PARTNER	NVARCHAR(20)
) 
AS
 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT PM.CD_COMPANY,
	   PM.CD_PARTNER,
	   PM.NO_INDEX,
	   PM.DC_COMMENT,
	   PM.ID_INSERT,
	   (MU.NM_USER + '(' + PM.ID_INSERT + ')') AS NM_INSERT,
	   PM.ID_UPDATE,
	   (MU1.NM_USER + '(' + PM.ID_UPDATE + ')') AS NM_UPDATE,
	   LEFT(PM.DTS_INSERT, 8) AS DT_INSERT,
	   LEFT(PM.DTS_UPDATE, 8) AS DT_UPDATE
FROM CZ_CRM_PARTNER_MEMO PM
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = PM.CD_COMPANY AND MU.ID_USER = PM.ID_INSERT
LEFT JOIN MA_USER MU1 ON MU1.CD_COMPANY = PM.CD_COMPANY AND MU1.ID_USER = PM.ID_UPDATE
WHERE PM.CD_COMPANY = @P_CD_COMPANY
AND PM.CD_PARTNER = @P_CD_PARTNER

GO