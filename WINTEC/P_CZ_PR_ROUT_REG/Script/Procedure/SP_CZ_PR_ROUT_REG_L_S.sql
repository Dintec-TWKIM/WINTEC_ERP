USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_ROUT_REG_L_S]    Script Date: 2021-02-18 오후 1:09:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--UP_PR_ROUT_L_SELECT
ALTER PROCEDURE [NEOE].[SP_CZ_PR_ROUT_REG_L_S]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_PLANT		NVARCHAR(7),
	@P_NO_OPPATH	NVARCHAR(3),
	@P_CD_ITEM		NVARCHAR(50),
	@P_CD_WC	    NVARCHAR(4000) = NULL,
	@P_CD_WCOP		NVARCHAR(4000) = NULL,
    @P_FG_LANG      NVARCHAR(4) = NULL,	--언어
	@P_YN_INSP		NVARCHAR(3),
	@P_YN_FILE		NVARCHAR(3),
	@P_YN_USE		NVARCHAR(1)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG

SELECT	RL.CD_OP,
		RL.CD_WC,
		RL.CD_WCOP,
		WC.NM_WC,
		OP.NM_OP,
		RL.CD_ITEM,
		RL.NO_OPPATH,
		RL.TM_SETUP,
		RL.TM,
		RL.TM_MOVE,
		RL.CD_RSRC,
		ISNULL(RL.TP_RSRC, '') AS TP_RSRC,
		RL.YN_RECEIPT,
		RL.YN_BF,
		RL.YN_QC,
		RL.QT_OVERLAP,
		RL.YN_PAR,
		RL.N_OPSPLIT,
		RL.DY_SBCNT,
		ISNULL(RL.CD_TOOL, '') AS CD_TOOL,
		RL.YN_FINAL,
		RL.TP_OPPATH,
		MI.UNIT_MO,
		RL.CD_PLANT,
		RL.CD_ITEM AS CD_MATL,
		MI.NM_ITEM,
		MI.STND_ITEM,
		RH.DC_OPPATH,
		OP.NM_OP AS NM_WCOP,
		RL.CD_EQUIP,
		EQ.NM_EQUIP,
		RL.RT_YIELD,
		RA.ROUT_ANS_CNT,
		RL.QT_LABOR_PLAN,
		RL.SET_REASON,
		RL.DC_RMK,
		CONVERT(NVARCHAR(8),RL.DTS_INSERT, 112) AS INSERT_DT,
		RL.ID_INSERT AS INSERT_NO_EMP,
		ME1.NM_KOR AS INSERT_NM_EMP,
		CONVERT(NVARCHAR(8),RL.DTS_UPDATE, 112) AS UPDATE_DT,
		RL.ID_UPDATE AS UPDATE_NO_EMP,
		ME2.NM_KOR AS UPDATE_NM_EMP,
		RL.SET_METHOD,
		ISNULL(RL.DY_PLAN, 0) AS DY_PLAN,
		RL.NO_SFT,
		SF.NM_SFT,
		RL.YN_ROUT_SU_IV,
		RL.YN_INSP,
		RL.DC_OP,
		RI.QT_INSP,
		RF.QT_FILE,
		ISNULL(RL.NUM_USERDEF1, 0) AS NUM_USERDEF1,
		ISNULL(RL.NUM_USERDEF2, 0) AS NUM_USERDEF2,
		ISNULL(RL.NUM_USERDEF3, 0) AS NUM_USERDEF3,
		ISNULL(RL.NUM_USERDEF4, 0) AS NUM_USERDEF4,
		ISNULL(RL.NUM_USERDEF5, 0) AS NUM_USERDEF5,
		ISNULL(RL.NUM_USERDEF6, 0) AS NUM_USERDEF6,
		ISNULL(RL.NUM_USERDEF7, 0) AS NUM_USERDEF7,
		ISNULL(RL.NUM_USERDEF8, 0) AS NUM_USERDEF8,
		ISNULL(RL.NUM_USERDEF9, 0) AS NUM_USERDEF9,
		RL.TXT_USERDEF1,
		RL.TXT_USERDEF2,
		RL.TXT_USERDEF3,
		RL.TXT_USERDEF4,
		RL.TXT_USERDEF5,
		RL.TXT_USERDEF6,
		RL.TXT_USERDEF7,
		RL.TXT_USERDEF8,
		RL.TXT_USERDEF9,
		RL.CD_USERDEF1,
		RL.CD_USERDEF2,
		RL.CD_USERDEF3,
		RL.YN_USE
FROM PR_ROUT_L RL
JOIN PR_ROUT RH ON RL.CD_COMPANY = RH.CD_COMPANY AND RL.CD_PLANT = RH.CD_PLANT AND RL.CD_ITEM = RH.CD_ITEM AND RL.NO_OPPATH = RH.NO_OPPATH AND RL.TP_OPPATH = RH.TP_OPPATH
JOIN MA_PITEM MI ON RL.CD_COMPANY = MI.CD_COMPANY AND RL.CD_PLANT = MI.CD_PLANT AND RL.CD_ITEM = MI.CD_ITEM
LEFT JOIN MA_WC WC ON WC.CD_COMPANY = @P_CD_COMPANY AND WC.CD_PLANT = @P_CD_PLANT AND WC.CD_WC = RL.CD_WC
LEFT JOIN PR_WCOP OP ON OP.CD_COMPANY = @P_CD_COMPANY AND OP.CD_PLANT = @P_CD_PLANT AND OP.CD_WC = RL.CD_WC AND OP.CD_WCOP = RL.CD_WCOP
LEFT JOIN PR_EQUIP EQ ON EQ.CD_COMPANY = RL.CD_COMPANY AND EQ.CD_PLANT = RL.CD_PLANT AND EQ.CD_EQUIP = RL.CD_EQUIP
LEFT JOIN (SELECT CD_COMPANY, CD_PLANT, CD_ITEM, NO_OPPATH, CD_OP,
				  COUNT(NO_OPPATH) AS ROUT_ANS_CNT
		   FROM PR_ROUT_ASN
		   GROUP BY CD_COMPANY, CD_PLANT, CD_ITEM, NO_OPPATH, CD_OP) RA 
ON RL.CD_COMPANY = RA.CD_COMPANY AND RL.CD_PLANT = RA.CD_PLANT AND RL.CD_ITEM = RA.CD_ITEM AND RL.NO_OPPATH = RA.NO_OPPATH AND RL.CD_OP = RA.CD_OP
LEFT JOIN MA_EMP ME1 ON RL.CD_COMPANY = ME1.CD_COMPANY AND RL.ID_INSERT = ME1.NO_EMP
LEFT JOIN MA_EMP ME2 ON RL.CD_COMPANY = ME2.CD_COMPANY AND RL.ID_UPDATE = ME2.NO_EMP
LEFT JOIN PR_SHIFT SF ON RL.CD_COMPANY = SF.CD_COMPANY AND RL.CD_PLANT = SF.CD_PLANT AND RL.NO_SFT = SF.NO_SFT
LEFT JOIN (SELECT CD_COMPANY, CD_PLANT, CD_ITEM, NO_OPPATH, CD_OP, CD_WCOP,
				  COUNT(NO_INSP) AS QT_INSP
		   FROM CZ_PR_ROUT_INSP
		   GROUP BY CD_COMPANY, CD_PLANT, CD_ITEM, NO_OPPATH, CD_OP, CD_WCOP) RI
ON RI.CD_COMPANY = RL.CD_COMPANY AND RI.CD_PLANT = RL.CD_PLANT AND RI.CD_ITEM = RL.CD_ITEM AND RI.NO_OPPATH = RL.NO_OPPATH AND RI.CD_OP = RL.CD_OP AND RI.CD_WCOP = RL.CD_WCOP
LEFT JOIN (SELECT CD_COMPANY, CD_PLANT, CD_ITEM, NO_OPPATH, CD_OP, CD_WCOP,
		   	      COUNT(NO_SEQ) AS QT_FILE
		   FROM CZ_PR_ROUT_FILE
		   GROUP BY CD_COMPANY, CD_PLANT, CD_ITEM, NO_OPPATH, CD_OP, CD_WCOP) RF
ON RF.CD_COMPANY = RL.CD_COMPANY AND RF.CD_PLANT = RL.CD_PLANT AND RF.CD_ITEM = RL.CD_ITEM AND RF.NO_OPPATH = RL.NO_OPPATH AND RF.CD_OP = RL.CD_OP AND RF.CD_WCOP = RL.CD_WCOP
WHERE RL.CD_COMPANY = @P_CD_COMPANY
AND RL.CD_PLANT = @P_CD_PLANT
AND RL.NO_OPPATH = @P_NO_OPPATH
AND	RL.CD_ITEM = @P_CD_ITEM
AND (ISNULL(@P_CD_WC, '') = '' OR RL.CD_WC IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_WC)))
AND	(ISNULL(@P_CD_WCOP, '') = '' OR RL.CD_WCOP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_WCOP)))
AND (ISNULL(@P_YN_INSP, '') = '' OR (@P_YN_INSP = '001' AND ISNULL(RI.QT_INSP, '') <> '') OR (@P_YN_INSP = '002' AND ISNULL(RI.QT_INSP, '') = ''))
AND (ISNULL(@P_YN_FILE, '') = '' OR (@P_YN_FILE = '001' AND ISNULL(RF.QT_FILE, '') <> '') OR (@P_YN_FILE = '002' AND ISNULL(RF.QT_FILE, '') = ''))
AND (ISNULL(@P_YN_USE, 'N') = 'N' OR RL.YN_USE = 'Y')
ORDER BY RL.CD_OP