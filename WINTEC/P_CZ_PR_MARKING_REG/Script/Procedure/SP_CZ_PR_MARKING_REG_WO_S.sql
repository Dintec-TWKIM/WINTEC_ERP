USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MARKING_REG_WO_S]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_MARKING_REG_WO_S]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_EQUIP			NVARCHAR(30),
	@P_NO_WO			NVARCHAR(20),
	@P_CD_ITEM			NVARCHAR(20)
)
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

SELECT WO.NO_WO,
	   WR.NO_LINE,
	   OP.NM_OP,
	   WO.CD_ITEM,
	   MI.NM_ITEM,
	   MI.NO_DESIGN,
	   MI.STND_ITEM,
	   ISNULL(WR.QT_START, 0) AS QT_START,
	   ISNULL(WM.QT_MARKING, 0) AS QT_MARKING,
	   ISNULL(WR.QT_WORK, 0) AS QT_WORK,
	   (MI.CD_ITEM + '_' + WR.CD_WCOP) AS NM_FILE
FROM PR_WO WO
JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
LEFT JOIN PR_WCOP OP ON OP.CD_COMPANY = WR.CD_COMPANY AND OP.CD_PLANT = WR.CD_PLANT AND OP.CD_WC = WR.CD_WC AND OP.CD_WCOP = WR.CD_WCOP
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = WO.CD_COMPANY AND MI.CD_PLANT = WO.CD_PLANT AND MI.CD_ITEM = WO.CD_ITEM
LEFT JOIN (SELECT WM.CD_COMPANY, WM.NO_WO, WM.NO_LINE,
		   	      COUNT(1) AS QT_MARKING 
		   FROM CZ_PR_WO_INSP WM
		   WHERE WM.NO_INSP = 998 
		   AND WM.YN_MARKING = 'Y'
		   GROUP BY WM.CD_COMPANY, WM.NO_WO, WM.NO_LINE) WM
ON WM.CD_COMPANY = WO.CD_COMPANY AND WM.NO_WO = WO.NO_WO AND WM.NO_LINE = WR.NO_LINE
WHERE WO.CD_COMPANY = @P_CD_COMPANY
AND (ISNULL(@P_NO_WO, '') = '' OR WO.NO_WO = @P_NO_WO)
AND (ISNULL(@P_CD_ITEM, '') = '' OR WO.CD_ITEM = @P_CD_ITEM)
AND WR.QT_WIP > 0
AND WR.CD_EQUIP = @P_CD_EQUIP
AND WR.CD_WC = 'W531'
AND (ISNULL(WR.QT_START, 0) - ISNULL(WR.QT_WORK, 0)) > 0
AND EXISTS (SELECT 1 
		    FROM CZ_PR_WO_REQ_D WD
			WHERE WD.CD_COMPANY = WO.CD_COMPANY
			AND WD.NO_WO = WO.NO_WO)


GO