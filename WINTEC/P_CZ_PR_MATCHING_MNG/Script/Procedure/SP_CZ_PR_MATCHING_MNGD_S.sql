USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MATCHING_MNGD_S]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_MATCHING_MNGD_S]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_WO			NVARCHAR(20),
    @P_NO_LINE          NUMERIC(5, 0)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT WO.CD_COMPANY,
           WO.NO_WO,
           WD.SEQ_WO,
           WD.NO_ID,
           WO.CD_ITEM,
           MI.TP_POS,
           (MI.TP_POS + '_IN') AS TP_POS_IN,
           (MI.TP_POS + '_OUT') AS TP_POS_OUT,
           MD.NO_ID_C,
           MD.CD_GRADE_IN,
           MD.CD_GRADE_OUT
    FROM PR_WO WO
    JOIN CZ_PR_WO_REQ_D WD ON WD.CD_COMPANY = WO.CD_COMPANY AND WD.NO_WO = WO.NO_WO
    LEFT JOIN CZ_PR_MATCHING_ITEM MI ON MI.CD_COMPANY = WO.CD_COMPANY AND MI.CD_PLANT = WO.CD_PLANT AND MI.CD_ITEM = WO.CD_ITEM 
    LEFT JOIN CZ_PR_MATCHING_DATA MD ON MD.CD_COMPANY = WO.CD_COMPANY AND MD.CD_PLANT = WO.CD_PLANT AND MD.NO_ID = WD.NO_ID AND MD.CD_PITEM = MI.CD_PITEM
    WHERE WO.CD_COMPANY = @P_CD_COMPANY
    AND WO.NO_WO = @P_NO_WO
),
B AS
(
    SELECT PV2.CD_COMPANY,
           PV2.NO_WO,
           PV2.SEQ_WO,
           PV2.NO_ID,
           PV2.CD_ITEM,
           MAX(PV2.[001]) AS [001],
           MAX(PV2.[001_IN]) AS [001_IN],
           MAX(PV2.[001_OUT]) AS [001_OUT],
           MAX(PV2.[002]) AS [002],
           MAX(PV2.[002_IN]) AS [002_IN],
           MAX(PV2.[002_OUT]) AS [002_OUT],
           MAX(PV2.[003]) AS [003],
           MAX(PV2.[003_IN]) AS [003_IN],
           MAX(PV2.[003_OUT]) AS [003_OUT],
           MAX(PV2.[004]) AS [004],
           MAX(PV2.[004_IN]) AS [004_IN],
           MAX(PV2.[004_OUT]) AS [004_OUT],
           MAX(PV2.[005]) AS [005],
           MAX(PV2.[005_IN]) AS [005_IN],
           MAX(PV2.[005_OUT]) AS [005_OUT]
    FROM A WO
    PIVOT(MAX(WO.NO_ID_C) FOR WO.TP_POS IN ([001], [002], [003], [004], [005])) AS PV
    PIVOT(MAX(PV.CD_GRADE_IN) FOR PV.TP_POS_IN IN ([001_IN], [002_IN], [003_IN], [004_IN], [005_IN])) AS PV1
    PIVOT(MAX(PV1.CD_GRADE_OUT) FOR PV1.TP_POS_OUT IN ([001_OUT], [002_OUT], [003_OUT], [004_OUT], [005_OUT])) AS PV2
    GROUP BY PV2.CD_COMPANY,
             PV2.NO_WO,
             PV2.SEQ_WO,
             PV2.NO_ID,
             PV2.CD_ITEM
)
SELECT 'N' AS S,
       B.NO_WO,
       @P_NO_LINE AS NO_LINE,
       B.SEQ_WO,
       B.NO_ID,
       B.CD_ITEM,
       B.[001],
       B.[001_IN],
       B.[001_OUT],
       B.[002],
       B.[002_IN],
       B.[002_OUT],
       B.[003],
       B.[003_IN],
       B.[003_OUT],
       B.[004],
       B.[004_IN],
       B.[004_OUT],
       B.[005],
       B.[005_IN],
       B.[005_OUT],
       WI.NO_SO,
       WI.DC_RMK
FROM B
LEFT JOIN CZ_PR_WO_INSP WI ON WI.CD_COMPANY = B.CD_COMPANY AND WI.NO_WO = B.NO_WO AND WI.SEQ_WO = B.SEQ_WO AND WI.NO_INSP = 997

GO