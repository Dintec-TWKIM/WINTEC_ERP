USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIRR_REG_SUBH_S]    Script Date: 2015-09-03 오후 6:57:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIRR_REG_SUBH_S]
(  
    @P_CD_COMPANY			NVARCHAR(7),
    @P_DT_FROM				NVARCHAR(8),
    @P_DT_TO				NVARCHAR(8),
    @P_NO_SO				NVARCHAR(20),
	@P_NO_GIR				NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   OH.NO_IO,
	   OH.DT_IO,
	   ME.NM_KOR AS NM_KOR,
	   OH.DC_RMK
FROM MM_QTIOH OH
JOIN(SELECT OL.CD_COMPANY,
		    OL.NO_IO
     FROM MM_QTIO OL
     WHERE OL.FG_IO = '010'
     AND OL.QT_UNIT_MM - OL.QT_RETURN_MM > 0
	 AND (ISNULL(@P_NO_SO, '') = '' OR OL.NO_PSO_MGMT = @P_NO_SO)
	 AND (ISNULL(@P_NO_GIR, '') = '' OR OL.NO_ISURCV = @P_NO_GIR)
     GROUP BY OL.CD_COMPANY, OL.NO_IO) OL
ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = OH.CD_COMPANY AND ME.NO_EMP = OH.NO_EMP
WHERE OH.CD_COMPANY = @P_CD_COMPANY
AND OH.DT_IO BETWEEN @P_DT_FROM AND @P_DT_TO

GO