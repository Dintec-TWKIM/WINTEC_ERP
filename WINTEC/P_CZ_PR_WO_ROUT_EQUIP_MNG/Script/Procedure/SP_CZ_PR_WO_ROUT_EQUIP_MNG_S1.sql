USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WO_ROUT_EQUIP_MNG_S1]    Script Date: 2021-03-02 오전 10:51:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_ROUT_EQUIP_MNG_S1]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
	@P_DT_START			NVARCHAR(8),
	@P_DT_END			NVARCHAR(8),
	@P_NO_WO			NVARCHAR(20),
	@P_CD_EQUIP			NVARCHAR(30),
	@P_YN_CLOSE			NVARCHAR(1)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT PE.CD_EQIP_GRP,
       MC.NM_SYSDEF AS NM_EQUIP_GRP,
	   PE.CD_EQUIP,
	   PE.NM_EQUIP,
	   ISNULL(WR.QT_TOTAL, 0) AS QT_TOTAL,
	   ISNULL(WR.QT_WO, 0) AS QT_WO,
	   ISNULL(WR.QT_START, 0) AS QT_START,
	   ISNULL(WR.QT_WORK, 0) AS QT_WORK,
	   ISNULL(WR.QT_REMAIN, 0) AS QT_REMAIN
FROM PR_EQUIP PE
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PE.CD_COMPANY AND MC.CD_FIELD = 'PR_E000003' AND MC.CD_SYSDEF = PE.CD_EQIP_GRP
LEFT JOIN (SELECT WR.CD_COMPANY, WR.CD_EQUIP,
				  COUNT(1) AS QT_TOTAL,
				  SUM(WR.QT_WO) AS QT_WO,
				  SUM(WR.QT_START) AS QT_START,
				  SUM(WR.QT_WORK) AS QT_WORK,
				  SUM(ISNULL(WR.QT_WO, 0) - ISNULL(WR.QT_WORK, 0)) AS QT_REMAIN
		   FROM PR_WO WO
		   JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
		   WHERE WO.CD_COMPANY = @P_CD_COMPANY
		   AND WO.CD_PLANT = @P_CD_PLANT
		   AND WO.DT_REL BETWEEN @P_DT_START AND @P_DT_END
		   AND (ISNULL(@P_NO_WO, '') = '' OR WO.NO_WO = @P_NO_WO)
		   AND (ISNULL(@P_YN_CLOSE, 'N') = 'N' OR WR.ST_OP <> 'C')
		   GROUP BY WR.CD_COMPANY, WR.CD_EQUIP) WR
ON WR.CD_COMPANY = PE.CD_COMPANY AND WR.CD_EQUIP = PE.CD_EQUIP
WHERE PE.CD_COMPANY = @P_CD_COMPANY
AND PE.CD_PLANT = @P_CD_PLANT
AND (ISNULL(@P_CD_EQUIP, '') = '' OR PE.CD_EQUIP = @P_CD_EQUIP)
ORDER BY MC.NM_SYSDEF, PE.CD_EQUIP

GO