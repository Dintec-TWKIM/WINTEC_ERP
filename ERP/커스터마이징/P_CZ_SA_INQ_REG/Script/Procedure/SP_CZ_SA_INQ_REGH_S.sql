USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_INQ_REG_S]    Script Date: 2015-07-03 오전 11:04:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_INQ_REGH_S]
(
	@P_CD_COMPANY       NVARCHAR(7),
	@P_TP_STEP          NVARCHAR(3),
	@P_NO_KEY           NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   WH.NO_KEY,
	   WH.TP_SALES,
	   MC.NM_SYSDEF AS NM_TP_SALES,			
	   WH.ID_SALES,
	   MU.NM_USER AS NM_SALES,
	   WH.ID_TYPIST,
	   MU1.NM_USER AS NM_TYPIST,
	   WH.ID_PUR,
	   MU2.NM_USER AS NM_PUR,
	   WH.ID_LOG,
	   MU3.NM_USER AS NM_LOG,
	   WH.DC_RMK
FROM CZ_MA_WORKFLOWH WH
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = WH.CD_COMPANY AND MU.ID_USER = WH.ID_SALES
LEFT JOIN MA_USER MU1 ON MU1.CD_COMPANY = WH.CD_COMPANY AND MU1.ID_USER = WH.ID_TYPIST
LEFT JOIN MA_USER MU2 ON MU2.CD_COMPANY = WH.CD_COMPANY AND MU2.ID_USER = WH.ID_PUR
LEFT JOIN MA_USER MU3 ON MU3.CD_COMPANY = WH.CD_COMPANY AND MU3.ID_USER = WH.ID_LOG
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = WH.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00023' AND MC.CD_SYSDEF = WH.TP_SALES
WHERE WH.CD_COMPANY = @P_CD_COMPANY
AND WH.TP_STEP = @P_TP_STEP
AND WH.NO_KEY = @P_NO_KEY

GO