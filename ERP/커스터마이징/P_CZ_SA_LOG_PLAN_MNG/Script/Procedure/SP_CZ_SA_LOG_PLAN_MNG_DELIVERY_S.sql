USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_LOG_PLAN_MNG_DELIVERY_S]    Script Date: 2016-02-12 오후 1:40:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_LOG_PLAN_MNG_DELIVERY_S]  
(
    @P_NO_REV       INT
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
       PD.CD_COMPANY,
       PD.NO_REV,
       PD.NO_IDX,
       PD.NO_GIR,
       PD.DC_LOCATION,
       PD.DC_TASK,
       PD.CD_PARTNER,
       PD.LN_PARTNER,
       PD.NO_EMP,
       ME.NM_KOR AS NM_EMP,
       PD.QT_ITEM,
       PD.NO_IMO,
       MH.NM_VESSEL,
       PD.DT_COMPLETE,
       PD.NO_SO_PRE,
       PD.DC_UPDATE,
       PD.DC_LIMIT,
       PD.DC_ETC,
       STUFF((SELECT '+' + MC.CD_FLAG2 + CONVERT(VARCHAR, CONVERT(INT, PH.QT_WIDTH))
			  FROM CZ_SA_PACKH PH
			  JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PH.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00026' AND MC.CD_SYSDEF = PH.CD_TYPE
			  WHERE PH.CD_COMPANY = PD.CD_COMPANY
			  AND PH.NO_GIR = PD.NO_GIR
			  FOR XML PATH('')), 1,1, '') AS DC_PACK,
       (SELECT SUM(CASE WHEN PH.CD_TYPE IN ('002', '003') THEN CONVERT(INT, PH.QT_WIDTH) / 1000.0 ELSE 0 END)
        FROM CZ_SA_PACKH PH
        WHERE PH.CD_COMPANY = PD.CD_COMPANY
        AND PH.NO_GIR = PD.NO_GIR) AS QT_PACK
FROM CZ_SA_LOG_PLAN_DELIVERY PD
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PD.CD_COMPANY AND MP.CD_PARTNER = PD.CD_PARTNER
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = PD.CD_COMPANY AND ME.NO_EMP = PD.NO_EMP
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = PD.NO_IMO
WHERE PD.CD_COMPANY IN ('K100', 'K200') 
AND PD.NO_REV = @P_NO_REV
ORDER BY PD.DT_COMPLETE, PD.DC_LOCATION, MP.LN_PARTNER, PD.NO_GIR

GO