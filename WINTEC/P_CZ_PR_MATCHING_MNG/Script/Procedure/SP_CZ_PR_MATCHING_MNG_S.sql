USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MATCHING_MNG_S]    Script Date: 2023-02-20 오후 3:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_MATCHING_MNG_S]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
	@P_CD_ITEM			NVARCHAR(20),
	@P_NO_WO			NVARCHAR(20),
	@P_NO_SO			NVARCHAR(20),
	@P_YN_END			NVARCHAR(1)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   WO.NO_WO,
	   WR.NO_LINE,
	   WR.CD_OP,
	   OP.NM_OP,
	   WO.CD_ITEM,
	   MI.NM_ITEM,
	   WO.QT_ITEM,
	   WR.QT_WIP,
	   WI.QT_MATCHING,
	   WO.QT_WORK,
	   MI1.CD_ITEM_001,
	   MI1.NM_ITEM_001,
	   MI1.NO_DESIGN_001,
	   MI1.CD_ITEM_002,
	   MI1.NM_ITEM_002,
	   MI1.NO_DESIGN_002,
	   MI1.CD_ITEM_003,
	   MI1.NM_ITEM_003,
	   MI1.NO_DESIGN_003,
	   MI1.CD_ITEM_004,
	   MI1.NM_ITEM_004,
	   MI1.NO_DESIGN_004,
	   MI1.CD_ITEM_005,
	   MI1.NM_ITEM_005,
	   MI1.NO_DESIGN_005
FROM PR_WO WO
JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
LEFT JOIN PR_WCOP OP ON OP.CD_COMPANY = WR.CD_COMPANY AND OP.CD_WC = WR.CD_WC AND OP.CD_WCOP = WR.CD_WCOP
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = WO.CD_COMPANY AND MI.CD_PLANT = WO.CD_PLANT AND MI.CD_ITEM = WO.CD_ITEM
LEFT JOIN (SELECT WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE,
		   	      COUNT(1) AS QT_MATCHING 
		   FROM CZ_PR_WO_INSP WI
		   WHERE WI.NO_INSP = 997
		   GROUP BY WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE) WI
ON WI.CD_COMPANY = WO.CD_COMPANY AND WI.NO_WO = WO.NO_WO AND WI.NO_LINE = WR.NO_LINE
LEFT JOIN (SELECT PV.CD_COMPANY,
		          PV.CD_PLANT,
		          PV.CD_ITEM,
		          PV.[001] AS CD_ITEM_001,
		          MI1.NM_ITEM AS NM_ITEM_001,
				  MI1.NO_DESIGN AS NO_DESIGN_001,
		          PV.[002] AS CD_ITEM_002,
		          MI2.NM_ITEM AS NM_ITEM_002,
		          MI2.NO_DESIGN AS NO_DESIGN_002,
				  PV.[003] AS CD_ITEM_003,
		          MI3.NM_ITEM AS NM_ITEM_003,
				  MI3.NO_DESIGN AS NO_DESIGN_003,
				  PV.[004] AS CD_ITEM_004,
		          MI4.NM_ITEM AS NM_ITEM_004,
				  MI4.NO_DESIGN AS NO_DESIGN_004,
				  PV.[005] AS CD_ITEM_005,
		          MI5.NM_ITEM AS NM_ITEM_005,
				  MI5.NO_DESIGN AS NO_DESIGN_005
		   FROM (SELECT * 
		         FROM CZ_PR_MATCHING_ITEM) MI
		   PIVOT (MAX(MI.CD_PITEM) FOR MI.TP_POS IN ([001], [002], [003], [004], [005])) AS PV
		   LEFT JOIN MA_PITEM MI1 ON MI1.CD_COMPANY = PV.CD_COMPANY AND MI1.CD_PLANT = PV.CD_PLANT AND MI1.CD_ITEM = PV.[001]
		   LEFT JOIN MA_PITEM MI2 ON MI2.CD_COMPANY = PV.CD_COMPANY AND MI2.CD_PLANT = PV.CD_PLANT AND MI2.CD_ITEM = PV.[002]
		   LEFT JOIN MA_PITEM MI3 ON MI3.CD_COMPANY = PV.CD_COMPANY AND MI3.CD_PLANT = PV.CD_PLANT AND MI3.CD_ITEM = PV.[003]
		   LEFT JOIN MA_PITEM MI4 ON MI4.CD_COMPANY = PV.CD_COMPANY AND MI4.CD_PLANT = PV.CD_PLANT AND MI4.CD_ITEM = PV.[004]
		   LEFT JOIN MA_PITEM MI5 ON MI5.CD_COMPANY = PV.CD_COMPANY AND MI5.CD_PLANT = PV.CD_PLANT AND MI5.CD_ITEM = PV.[005]) MI1
ON MI1.CD_COMPANY = WO.CD_COMPANY AND MI1.CD_PLANT = WO.CD_PLANT AND MI1.CD_ITEM = WO.CD_ITEM
WHERE WO.CD_COMPANY = @P_CD_COMPANY
AND WO.CD_PLANT = @P_CD_PLANT
AND OP.NM_OP LIKE '%현합%'
AND (WR.QT_WIP > 0 OR @P_YN_END = 'N')
AND (ISNULL(@P_CD_ITEM, '') = '' OR WO.CD_ITEM = @P_CD_ITEM)
AND (ISNULL(@P_NO_WO, '') = '' OR WO.NO_WO = @P_NO_WO)
AND (ISNULL(@P_NO_SO, '') = '' OR EXISTS (SELECT 1 
										  FROM CZ_PR_SA_SOL_PR_WO_MAPPING SW
										  WHERE SW.CD_COMPANY = WO.CD_COMPANY
										  AND SW.CD_PLANT = WO.CD_PLANT
										  AND SW.NO_WO = WO.NO_WO
										  AND SW.NO_SO = @P_NO_SO))
AND EXISTS (SELECT 1 
            FROM CZ_PR_WO_REQ_D WD
            WHERE WD.CD_COMPANY = WO.CD_COMPANY
            AND WD.NO_WO = WO.NO_WO)

