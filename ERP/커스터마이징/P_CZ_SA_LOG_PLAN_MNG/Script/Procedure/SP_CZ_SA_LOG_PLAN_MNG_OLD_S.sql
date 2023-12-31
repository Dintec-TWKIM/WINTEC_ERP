USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_LOG_PLAN_MNG_OLD_S]    Script Date: 2016-02-12 오후 1:40:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_LOG_PLAN_MNG_OLD_S]  
(
    @P_DT_START	    NVARCHAR(8),
    @P_DT_END       NVARCHAR(8)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
       PV.NO_IDX,
       PV.CD_COMPANY,
       PV.NO_IMO,
       PV.NM_VESSEL,
       PV.DC_LOCATION,
       PV.DC_PORT,
       PV.DC_EMP,
       PV.DC_ETB,
       PV.DC_ETD,
       PV.DT_COMPLETE,
       PV.DC_CAR,
       PV.DC_ITEM,
       PV.DC_TEL,
       PV.DC_ETC,
       PV.NO_GIR,
       STUFF((SELECT '+' + MC.CD_FLAG2 + CONVERT(VARCHAR, CONVERT(INT, PH.QT_WIDTH))
			  FROM CZ_SA_LOG_PLAN_OLD_L PS
              JOIN CZ_SA_PACKH PH ON PH.CD_COMPANY = PS.CD_COMPANY AND PH.NO_GIR = PS.NO_GIR
			  JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PH.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00026' AND MC.CD_SYSDEF = PH.CD_TYPE
              WHERE PS.NO_IDX = PV.NO_IDX
			  FOR XML PATH('')), 1,1, '') AS DC_PACK,
       (SELECT SUM(CASE WHEN PH.CD_TYPE IN ('002', '003') THEN CONVERT(INT, PH.QT_WIDTH) / 1000.0 ELSE 0 END)
        FROM CZ_SA_LOG_PLAN_SHIP PS
        JOIN CZ_SA_PACKH PH ON PH.CD_COMPANY = PS.CD_COMPANY AND PH.NO_GIR = PS.NO_GIR
        WHERE PS.NO_IDX = PV.NO_IDX) AS QT_PACK
FROM CZ_SA_LOG_PLAN_OLD PV
WHERE PV.DT_COMPLETE BETWEEN @P_DT_START AND @P_DT_END
ORDER BY PV.DT_COMPLETE, PV.DC_EMP, PV.DC_LOCATION, PV.DC_ETB

GO