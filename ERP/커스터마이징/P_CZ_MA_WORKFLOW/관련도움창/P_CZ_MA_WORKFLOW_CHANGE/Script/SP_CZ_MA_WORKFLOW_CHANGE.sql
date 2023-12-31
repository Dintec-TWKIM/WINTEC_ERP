USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_CHANGE]    Script Date: 2016-10-28 오후 5:31:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_CHANGE]  
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_NO_KEY				NVARCHAR(20),
	@P_TP_STEP				NVARCHAR(3),
	@P_DT_START				NVARCHAR(8),
	@P_DT_END				NVARCHAR(8),
	@P_YN_DONE				NVARCHAR(1)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   WH.CD_COMPANY,
	   MC.NM_COMPANY,
	   WH.NO_KEY,
	   WH.TP_STEP,
	   CD.NM_SYSDEF AS NM_STEP,
	   WH.ID_SALES,
	   MU.NM_USER AS NM_SALES,
	   WH.ID_TYPIST,
	   MU1.NM_USER AS NM_TYPIST,
	   WH.ID_PUR,
	   MU2.NM_USER AS NM_PUR,
	   WH.ID_LOG,
	   MU3.NM_USER AS NM_LOG,
	   WH.DTS_INSERT,
	   WH.YN_DONE,
	   WH.DTS_DONE,
	   MU4.NM_USER AS NM_UPDATE,
	   (CASE WHEN WH.TP_STEP = '21' THEN MP.LN_PARTNER ELSE NULL END) AS NM_SUPPLIER,
	   WH.DC_RMK
FROM CZ_MA_WORKFLOWH WH
LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = WH.CD_COMPANY
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = WH.CD_COMPANY AND MP.CD_PARTNER = WH.ID_LOG
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = WH.CD_COMPANY AND CD.CD_FIELD = 'CZ_MA00004' AND CD.CD_SYSDEF = WH.TP_STEP
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = WH.CD_COMPANY AND MU.ID_USER = WH.ID_SALES
LEFT JOIN MA_USER MU1 ON MU1.CD_COMPANY = WH.CD_COMPANY AND MU1.ID_USER = WH.ID_TYPIST
LEFT JOIN MA_USER MU2 ON MU2.CD_COMPANY = WH.CD_COMPANY AND MU2.ID_USER = WH.ID_PUR
LEFT JOIN MA_USER MU3 ON MU3.CD_COMPANY = WH.CD_COMPANY AND MU3.ID_USER = WH.ID_LOG
LEFT JOIN MA_USER MU4 ON MU4.CD_COMPANY = WH.CD_COMPANY AND MU4.ID_USER = WH.ID_UPDATE
WHERE WH.CD_COMPANY = @P_CD_COMPANY
AND (ISNULL(@P_NO_KEY, '') = '' OR WH.NO_KEY = @P_NO_KEY)
AND (ISNULL(@P_TP_STEP, '') = '' OR WH.TP_STEP = @P_TP_STEP)
AND (ISNULL(@P_YN_DONE, 'N') = 'N' OR ISNULL(WH.YN_DONE, 'N') = 'N')
AND (ISNULL(@P_DT_START, '') = '' OR WH.DTS_DONE BETWEEN @P_DT_START + '000000' AND @P_DT_END + '999999')
ORDER BY WH.CD_COMPANY,  WH.NO_KEY, WH.TP_STEP

GO

