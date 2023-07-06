USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_INQ_RPTH_000_000_S]    Script Date: 2019-01-08 오후 4:33:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_SA_INQ_RPTH_000_000_S]  
(
    @P_CD_COMPANY			NVARCHAR(7),
	@P_NO_KEY			    NVARCHAR(20),
    @P_DT_INQ_FROM			NVARCHAR(8),
    @P_DT_INQ_TO		    NVARCHAR(8),
    @P_ID_SALES				NVARCHAR(10),
	@P_ID_TYPIST			NVARCHAR(10),
	@P_CD_PARTNER			NVARCHAR(20),
	@P_CD_SUPPLIER			NVARCHAR(20),
	@P_NO_IMO				NVARCHAR(10),
	@P_TP_STEP				NVARCHAR(4)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT WH.CD_COMPANY,
	   MC.NM_COMPANY,
	   WH.NO_KEY,
	   QH.NO_REF,
	   SH.NO_PO_PARTNER,
	   SUBSTRING(WH.DTS_INSERT, 0, 9) AS DT_FILE,
	   MU.NM_USER AS NM_SALES,
	   MU1.NM_USER AS NM_TYPIST,
	   MU2.NM_USER AS NM_PUR,
	   MU3.NM_USER AS NM_LOG,
	   MP.LN_PARTNER AS NM_PARTNER,
	   MH.NM_VESSEL
FROM (SELECT WH.CD_COMPANY, WH.NO_KEY,
			 MIN(WH.DTS_INSERT) AS DTS_INSERT
	  FROM CZ_MA_WORKFLOWH WH
	  WHERE WH.DTS_INSERT BETWEEN @P_DT_INQ_FROM + '000000' AND @P_DT_INQ_TO + '999999'
	  AND (ISNULL(@P_NO_KEY, '') = '' OR WH.NO_KEY = @P_NO_KEY)
	  AND (ISNULL(@P_CD_COMPANY, '') = '' OR WH.CD_COMPANY = @P_CD_COMPANY)
	  AND (ISNULL(@P_TP_STEP, '') = '' OR WH.TP_STEP = @P_TP_STEP)
	  AND (ISNULL(@P_CD_SUPPLIER, '') = '' OR EXISTS (SELECT 1
												      FROM CZ_MA_WORKFLOWL WL
												      WHERE WL.CD_COMPANY = WH.CD_COMPANY
													  AND WL.NO_KEY = WH.NO_KEY
													  AND WL.TP_STEP = WH.TP_STEP
													  AND (WL.YN_INCLUDED IS NULL OR WL.YN_INCLUDED = 'N')
													  AND WL.CD_SUPPLIER = @P_CD_SUPPLIER
													  AND ((WL.CD_COMPANY = 'S100' AND WL.NO_KEY LIKE 'DS%') OR ISNULL(WL.NM_FILE, '') <> '')
												      UNION ALL
												      SELECT 1
												      FROM CZ_SRM_QTNH_ATTACHMENT SA
												      JOIN CZ_SRM_QTNH SQ ON SQ.CD_COMPANY = SA.CD_COMPANY AND SQ.CD_PARTNER = SA.CD_PARTNER AND SQ.NO_FILE = SA.NO_FILE
												      WHERE SA.CD_COMPANY = WH.CD_COMPANY
													  AND SA.NO_FILE = WH.NO_KEY
													  AND SA.CD_PARTNER = @P_CD_SUPPLIER
													  AND ((SA.CD_COMPANY = 'S100' AND SA.NO_FILE LIKE 'DS%') OR ISNULL(SA.FILE_NAME, '') <> '')
												      AND SQ.CD_STATUS IN ('S', 'C')))
	  GROUP BY WH.CD_COMPANY, WH.NO_KEY) WH
LEFT JOIN CZ_MA_WORKFLOWH WH1 ON WH1.CD_COMPANY = WH.CD_COMPANY AND WH1.NO_KEY = WH.NO_KEY AND WH1.TP_STEP = '01'
LEFT JOIN CZ_MA_WORKFLOWH WH2 ON WH2.CD_COMPANY = WH.CD_COMPANY AND WH2.NO_KEY = WH.NO_KEY AND WH2.TP_STEP = '08'
LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = WH.CD_COMPANY AND QH.NO_FILE = WH.NO_KEY
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = WH.CD_COMPANY AND SH.NO_SO = WH.NO_KEY
LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = WH.CD_COMPANY
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = WH1.CD_COMPANY AND MU.ID_USER = WH1.ID_SALES
LEFT JOIN MA_USER MU1 ON MU1.CD_COMPANY = WH1.CD_COMPANY AND MU1.ID_USER = WH1.ID_TYPIST
LEFT JOIN MA_USER MU2 ON MU2.CD_COMPANY = WH2.CD_COMPANY AND MU2.ID_USER = WH2.ID_PUR
LEFT JOIN MA_USER MU3 ON MU3.CD_COMPANY = WH2.CD_COMPANY AND MU3.ID_USER = WH2.ID_LOG
WHERE (ISNULL(@P_CD_PARTNER, '') = '' OR QH.CD_PARTNER = @P_CD_PARTNER)
AND (ISNULL(@P_NO_IMO, '') = '' OR QH.NO_IMO = @P_NO_IMO)
AND (ISNULL(@P_ID_SALES, '') = '' OR WH1.ID_SALES = @P_ID_SALES)
AND (ISNULL(@P_ID_TYPIST, '') = '' OR WH1.ID_TYPIST = @P_ID_TYPIST)
OPTION(RECOMPILE)

GO

