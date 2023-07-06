USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WO_ROUT_EQUIP_MNG_WO]    Script Date: 2021-03-02 오전 10:51:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_ROUT_EQUIP_MNG_WO]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
	@P_CD_EQUIP			NVARCHAR(30),
	@P_DT_START			NVARCHAR(8),
	@P_DT_END			NVARCHAR(8),
	@P_NO_WO			NVARCHAR(20),
	@P_YN_CLOSE			NVARCHAR(1)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT WO.NO_WO,
	   WO.CD_ITEM,
	   MI.NM_ITEM,
	   MI.NO_DESIGN,
	   MI.UNIT_IM,
	   WO.QT_ITEM,
	   WO.FG_WO,
	   CD.NM_SYSDEF AS NM_FG_WO,
	   WO.ST_WO,
	   CD1.NM_SYSDEF AS NM_ST_WO,
	   WO.TP_ROUT,
	   TW.NM_TP_WO AS NM_TP_ROUT,
	   WR.DT_REL,
	   WR.DT_DUE,
	   WO.DT_RELEASE,
	   WO.DT_CLOSE,
	   WR.FG_WC,
	   CD2.NM_SYSDEF AS NM_FG_WC,
	   WR.CD_WC,
	   WC.NM_WC,
	   WR.CD_OP,
	   WP.NM_OP,
	   WR.ST_OP,
	   CD3.NM_SYSDEF AS NM_ST_OP,
	   CASE WHEN ISNULL(WR.QT_WO, 0) = 0 THEN 0 ELSE ROUND((ISNULL(WR.QT_WORK, 0) / WR.QT_WO) * 100, 4) END RT_WORK,
	   ISNULL(WR.QT_WO, 0) AS QT_WO,
	   ISNULL(WR.QT_WORK, 0) AS QT_WORK,
	   ISNULL(QT_START, 0) AS QT_START,
	   ISNULL(QT_REJECT, 0) AS QT_REJECT,
	   ISNULL(QT_REWORK, 0) AS QT_REWORK,
	   ISNULL(QT_MOVE, 0) AS QT_MOVE,
	   ISNULL(QT_WIP, 0) AS QT_WIP,
	   WR.CD_WCOP,
	   WR.NO_LINE
FROM PR_WO WO
JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.CD_PLANT = WO.CD_PLANT AND WR.NO_WO = WO.NO_WO
JOIN PR_WCOP WP ON WP.CD_COMPANY = WR.CD_COMPANY AND WP.CD_PLANT = WR.CD_PLANT AND WP.CD_WCOP = WR.CD_WCOP AND WP.CD_WC = WR.CD_WC
JOIN MA_WC WC ON WC.CD_COMPANY = WR.CD_COMPANY AND WC.CD_PLANT = WR.CD_PLANT AND WC.CD_WC = WR.CD_WC
JOIN PR_TPWO TW ON TW.CD_COMPANY = WO.CD_COMPANY AND TW.TP_WO = WO.TP_ROUT
JOIN MA_PITEM MI ON MI.CD_COMPANY = WO.CD_COMPANY AND MI.CD_PLANT = WO.CD_PLANT AND MI.CD_ITEM = WO.CD_ITEM
LEFT JOIN PR_EQUIP EQ ON EQ.CD_COMPANY = WR.CD_COMPANY AND EQ.CD_PLANT = WR.CD_PLANT AND EQ.CD_EQUIP = WR.CD_EQUIP
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = WO.CD_COMPANY AND CD.CD_FIELD = 'PR_0000007' AND CD.CD_SYSDEF = WO.FG_WO
LEFT JOIN MA_CODEDTL CD1 ON CD1.CD_COMPANY = WO.CD_COMPANY AND CD1.CD_FIELD = 'PR_0000006' AND CD1.CD_SYSDEF = WO.ST_WO
LEFT JOIN MA_CODEDTL CD2 ON CD2.CD_COMPANY = WR.CD_COMPANY AND CD2.CD_FIELD = 'MA_B000019' AND CD2.CD_SYSDEF = WR.FG_WC
LEFT JOIN MA_CODEDTL CD3 ON CD3.CD_COMPANY = WR.CD_COMPANY AND CD3.CD_FIELD = 'PR_0000009' AND CD3.CD_SYSDEF = WR.ST_OP
WHERE WO.CD_COMPANY = @P_CD_COMPANY
AND WO.CD_PLANT = @P_CD_PLANT
AND WR.CD_EQUIP = @P_CD_EQUIP
AND	WO.DT_REL BETWEEN @P_DT_START AND @P_DT_END
AND (ISNULL(@P_NO_WO, '') = '' OR WO.NO_WO = @P_NO_WO)
AND (ISNULL(@P_YN_CLOSE, 'N') = 'N' OR WR.ST_OP <> 'C')


GO