USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_POP_REG_SUB_S]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_POP_REG_SUB_S]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
	@P_NO_WO			NVARCHAR(20),
	@P_NO_LINE			NUMERIC(5, 0)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT WO.NO_WO,
	   ISNULL(WO.NO_SO, '') + '/' + ISNULL(CONVERT(CHAR, WO.NO_LINE_SO), '') AS NO_SO,
	   WO.CD_ITEM,
	   MI.NM_ITEM,
	   MI.NO_DESIGN,
	   MI.MAT_ITEM,
	   MI.STND_ITEM,
	   OP.NM_OP,
	   OP1.NM_OP AS NM_OP_NEXT,
	   WR.QT_START,
	   WR.QT_OUTPO,
	   (ISNULL(WR.QT_START, 0) - (ISNULL(WR.QT_WORK, 0) + ISNULL(WI.QT_OPOUT, 0))) AS QT_REMAIN,
	   (ISNULL(WR.QT_REJECT, 0) - ISNULL(WR.QT_REWORK, 0) - ISNULL(WR.QT_BAD, 0)) AS QT_REWORK_REMAIN,
	   RL.NO_OPPATH,
	   WR.CD_WC,
	   WR.CD_OP,
	   WR.CD_WCOP,
	   MI.CD_SL AS CD_SL_IN,
	   OP.CD_SL_BAD,
	   ISNULL(WR.YN_SUBCON, 'N') AS YN_SUBCON,
	   ISNULL(NULLIF(RL.YN_INSP, ''), 'N') AS YN_INSP,
	   RL.DC_OP,
	   WR.CD_EQUIP,
	   EQ.NM_EQUIP,
	   MC.CD_FLAG2 AS CD_HEAT
FROM PR_WO WO
LEFT JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = WO.CD_COMPANY AND MI.CD_PLANT = WO.CD_PLANT AND MI.CD_ITEM = WO.CD_ITEM
LEFT JOIN PR_WCOP OP ON OP.CD_COMPANY = WO.CD_COMPANY AND OP.CD_PLANT = WO.CD_PLANT AND OP.CD_WC = WR.CD_WC AND OP.CD_WCOP = WR.CD_WCOP
LEFT JOIN PR_WCOP OP1 ON OP1.CD_COMPANY = WO.CD_COMPANY AND OP1.CD_PLANT = WO.CD_PLANT AND OP1.CD_WC = WR.CD_WC_NEXT AND OP1.CD_WCOP = WR.CD_WCOP_NEXT
LEFT JOIN PR_ROUT_L RL ON RL.CD_COMPANY = WO.CD_COMPANY AND RL.CD_PLANT = WO.CD_PLANT AND RL.CD_ITEM = WO.CD_ITEM AND RL.NO_OPPATH = WO.PATN_ROUT AND RL.CD_OP = WR.CD_OP AND RL.CD_WCOP = WR.CD_WCOP AND RL.YN_USE = 'Y'
LEFT JOIN PR_EQUIP EQ ON EQ.CD_COMPANY = WR.CD_COMPANY AND EQ.CD_PLANT = WR.CD_PLANT AND EQ.CD_EQUIP = WR.CD_EQUIP
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = WR.CD_COMPANY AND MC.CD_FIELD = 'CZ_WIN0011' AND MC.CD_FLAG1 = WR.CD_EQUIP
LEFT JOIN (SELECT WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE,
                  COUNT(1) AS QT_OPOUT
           FROM CZ_PR_WO_INSP WI
           WHERE NO_INSP IN ('994', '995')
           GROUP BY WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE) WI
ON WI.CD_COMPANY = WO.CD_COMPANY AND WI.NO_WO = WO.NO_WO AND WR.NO_LINE = WI.NO_LINE
WHERE WO.CD_COMPANY = @P_CD_COMPANY
AND WO.CD_PLANT = @P_CD_PLANT
AND WO.NO_WO = @P_NO_WO
AND WR.NO_LINE = @P_NO_LINE

GO