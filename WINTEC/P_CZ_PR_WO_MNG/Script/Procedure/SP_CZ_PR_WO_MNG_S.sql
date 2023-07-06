USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WO_MNG_S]    Script Date: 2021-03-02 오전 10:51:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_MNG_S]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
	@P_DT_START			NVARCHAR(8),
	@P_DT_END			NVARCHAR(8),
	@P_FG_WO			NVARCHAR(3),
	@ST_WO				NVARCHAR(5),
	@P_NO_PJT			NVARCHAR(20),
	@P_TP_WO			NVARCHAR(4000),
	@P_GRP_MFG			NVARCHAR(4000),
	@P_PATN_ROUT		NVARCHAR(3)		= NULL,
	@P_GRP_ITEM			NVARCHAR(20)	= NULL,
	@P_CD_WC			NVARCHAR(4000)	= NULL,
	@P_CD_WCOP			NVARCHAR(4000)	= NULL,
	@P_NO_WO_FR			NVARCHAR(20)	= NULL,
	@P_NO_WO_TO			NVARCHAR(20)	= NULL,
	@P_NO_SFT			NVARCHAR(20)	= NULL,		
	@P_NO_EMP			NVARCHAR(20)	= NULL,
	@P_CD_ITEM_FR		NVARCHAR(50)	= NULL,
	@P_CD_ITEM_TO		NVARCHAR(50)	= NULL,
    @P_FG_LANG			NVARCHAR(4)		= NULL	--언어
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG


DECLARE @V_ST_OP_P		NVARCHAR(1), 
		@V_ST_OP_O		NVARCHAR(1), 
		@V_ST_OP_R		NVARCHAR(1), 
		@V_ST_OP_S		NVARCHAR(1), 
		@V_ST_OP_C		NVARCHAR(1)

SELECT  @V_ST_OP_P = CASE WHEN CHARINDEX('P', ISNULL(@ST_WO, 0)) > 0 THEN 'P' ELSE 'X' END, 
		@V_ST_OP_O = CASE WHEN CHARINDEX('O', ISNULL(@ST_WO, 0)) > 0 THEN 'O' ELSE 'X' END, 
		@V_ST_OP_R = CASE WHEN CHARINDEX('R', ISNULL(@ST_WO, 0)) > 0 THEN 'R' ELSE 'X' END, 
		@V_ST_OP_S = CASE WHEN CHARINDEX('S', ISNULL(@ST_WO, 0)) > 0 THEN 'S' ELSE 'X' END, 
		@V_ST_OP_C = CASE WHEN CHARINDEX('C', ISNULL(@ST_WO, 0)) > 0 THEN 'C' ELSE 'X' END
			
BEGIN
	-- 헤더 조회
	SELECT 'N' AS CHK,	
			WO.NO_WO,
			WO.CD_ITEM,
			MI.NM_ITEM,
			MI.STND_ITEM,
			MI.UNIT_MO,
			MI.UNIT_IM,
			WO.QT_ITEM,
			WO.QT_WORK,
			WO.DT_REL,
			WO.DT_DUE,
			WO.DT_RELEASE,
			WO.DT_CLOSE,
			WO.FG_WO,
			CD.NM_SYSDEF AS NM_FG_WO,
			WO.ST_WO,
			CD1.NM_SYSDEF AS NM_ST_WO,
			WO.CD_PLANT,
			WO.TP_ROUT,
			TW.NM_TP_WO AS NM_TP_ROUT,
			WO.TP_GI,
			EJ.NM_QTIOTP AS NM_TP_GI,
			WO.TP_GR,
			EJ1.NM_QTIOTP AS NM_TP_GR,
			WO.PATN_ROUT,
			WO.PATN_ROUT + '-' + RT.DC_OPPATH AS NM_PATN_ROUT,
			WO.NO_LOT,
			WO.NO_SO,
			WO.NO_LINE_SO,
			WO.NO_PJT,
			PJ.NM_PROJECT,
			WO.DC_RMK,
			MI.FG_SERNO,
			CD2.NM_SYSDEF AS FG_LOTNO,
			MI.GRP_ITEM,
			IG.NM_ITEMGRP,
			TW.FG_AUTO,
			WO.CD_USERDEF1,
			WO.CD_USERDEF2,
			WO.CD_USERDEF3,
			WO.CD_USERDEF4,
			WO.CD_USERDEF5,
			MP.LN_PARTNER, --수주거래처명
			SL.NO_RELATION,
			MI.STND_DETAIL_ITEM,
			MI.MAT_ITEM,
			WO.SEQ_PROJECT,
			PL.CD_ITEM AS CD_PJT_ITEM,
			MI1.NM_ITEM AS NM_PJT_ITEM,
			MI1.STND_ITEM AS PJT_ITEM_STND,
			MI.CLS_L,
			CD3.NM_SYSDEF AS NM_CLS_L,
			MI.CLS_M,
			CD4.NM_SYSDEF AS NM_CLS_M,
			MI.CLS_S,
			CD5.NM_SYSDEF AS NM_CLS_S,
			WO.NO_EMP,
			ME.NM_KOR,
			WO.DT_LIMIT,
			WO.NUM_USERDEF1,
			WO.NUM_USERDEF2,
			MI.TP_ITEM AS CD_TP_ITEM,
			CD6.NM_SYSDEF AS TP_ITEM,
			WO.NO_SOURCE,
			MI.PARTNER AS CD_PARTNER_ITEM,
			MP1.LN_PARTNER AS LN_PARTNER_ITEM,
			MI.GRP_MFG,
			CD7.NM_SYSDEF AS NM_GRP_MFG,
			WO.CD_PACKUNIT,
			MI2.NM_ITEM AS NM_PACKUNIT,
			WO.TXT_USERDEF1 AS NO_HEAT,
			WO.TXT_USERDEF2,
			WK.QT_RCVREQ,
			ISNULL(WO.TXT_USERDEF1, '') + ISNULL(WO.TXT_USERDEF2, '') AS Z_BCWP_USERDEF,
			ISNULL(FI.COUNT_FILE,'0') + '건' AS FILE_PATH_MNG,
			WO.DC_RMK2,
			MI.EN_ITEM,
            MI.NM_MAKER,
            MI.BARCODE,
            MI.NO_MODEL,
            MI.NO_DESIGN,
            PR.NO_PRQ,
			(CASE WHEN EXISTS (SELECT 1 
							   FROM CZ_PR_WO_REQ_D WD
							   WHERE WD.CD_COMPANY = WO.CD_COMPANY
							   AND WD.NO_WO = WO.NO_WO) THEN 'Y' ELSE 'N' END) AS YN_INPUT
	FROM PR_WO WO
	JOIN MA_PITEM MI ON MI.CD_COMPANY = WO.CD_COMPANY AND MI.CD_PLANT = WO.CD_PLANT AND MI.CD_ITEM = WO.CD_ITEM
	JOIN PR_TPWO TW ON TW.CD_COMPANY = WO.CD_COMPANY AND TW.TP_WO = WO.TP_ROUT
	LEFT JOIN MM_EJTP EJ ON EJ.CD_COMPANY = WO.CD_COMPANY AND EJ.CD_QTIOTP = WO.TP_GI
	LEFT JOIN MM_EJTP EJ1 ON EJ1.CD_COMPANY = WO.CD_COMPANY AND EJ1.CD_QTIOTP = WO.TP_GR
	LEFT JOIN PR_ROUT RT ON RT.CD_COMPANY = WO.CD_COMPANY AND RT.CD_PLANT = WO.CD_PLANT AND RT.CD_ITEM = WO.CD_ITEM AND RT.NO_OPPATH = WO.PATN_ROUT
	LEFT JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = WO.CD_COMPANY AND PJ.NO_PROJECT = WO.NO_PJT
	LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = MI.CD_COMPANY AND IG.CD_ITEMGRP = MI.GRP_ITEM
	LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = WO.CD_COMPANY AND SL.NO_SO = WO.NO_SO AND WO.NO_LINE_SO = SL.SEQ_SO
	LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = SL.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
    LEFT JOIN SA_PROJECTL PL ON WO.CD_COMPANY = PL.CD_COMPANY AND WO.NO_PJT = PL.NO_PROJECT AND WO.SEQ_PROJECT = PL.SEQ_PROJECT
    LEFT JOIN MA_PITEM MI1 ON PL.CD_COMPANY = MI1.CD_COMPANY AND PL.CD_PLANT = MI1.CD_PLANT AND PL.CD_ITEM = MI1.CD_ITEM  
    LEFT JOIN MA_EMP ME ON WO.CD_COMPANY = ME.CD_COMPANY AND WO.CD_PLANT = ME.CD_PLANT AND WO.NO_EMP = ME.NO_EMP
	LEFT JOIN MA_PARTNER MP1 ON MI.CD_COMPANY = MP1.CD_COMPANY AND MI.PARTNER = MP1.CD_PARTNER  
	LEFT JOIN MA_EMP ME1 ON WO.CD_COMPANY = ME1.CD_COMPANY AND WO.CD_PACKUNIT = ME1.NO_EMP
	LEFT JOIN MA_PITEM MI2 ON WO.CD_COMPANY = MI2.CD_COMPANY AND WO.CD_PLANT = MI2.CD_PLANT AND WO.CD_PACKUNIT = MI2.CD_ITEM
	LEFT JOIN (SELECT CD_COMPANY, CD_PLANT, NO_WO,
					  MAX(NO_PRQ) AS NO_PRQ
			   FROM PR_PRQ_WO_LINK
			   GROUP BY CD_COMPANY, CD_PLANT, NO_WO) PR 
	ON PR.CD_COMPANY = WO.CD_COMPANY AND PR.CD_PLANT = WO.CD_PLANT AND PR.NO_WO = WO.NO_WO
	LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = WO.CD_COMPANY AND CD.CD_FIELD = 'PR_0000007' AND CD.CD_SYSDEF = WO.FG_WO
	LEFT JOIN MA_CODEDTL CD1 ON CD1.CD_COMPANY = WO.CD_COMPANY AND CD1.CD_FIELD = 'PR_0000006' AND CD1.CD_SYSDEF = WO.ST_WO
	LEFT JOIN MA_CODEDTL CD2 ON CD2.CD_COMPANY = MI.CD_COMPANY AND CD2.CD_FIELD = 'MA_B000015' AND CD2.CD_SYSDEF = MI.FG_SERNO
	LEFT JOIN MA_CODEDTL CD3 ON CD3.CD_COMPANY = MI.CD_COMPANY AND CD3.CD_FIELD = 'MA_B000030' AND CD3.CD_SYSDEF = MI.CLS_L
	LEFT JOIN MA_CODEDTL CD4 ON CD4.CD_COMPANY = MI.CD_COMPANY AND CD4.CD_FIELD = 'MA_B000031' AND CD4.CD_SYSDEF = MI.CLS_M
	LEFT JOIN MA_CODEDTL CD5 ON CD5.CD_COMPANY = MI.CD_COMPANY AND CD5.CD_FIELD = 'MA_B000032' AND CD5.CD_SYSDEF = MI.CLS_S
	LEFT JOIN MA_CODEDTL CD6 ON CD6.CD_COMPANY = MI.CD_COMPANY AND CD6.CD_FIELD = 'MA_B000011' AND CD6.CD_SYSDEF = MI.TP_ITEM
	LEFT JOIN MA_CODEDTL CD7 ON CD7.CD_COMPANY = MI.CD_COMPANY AND CD7.CD_FIELD = 'MA_B000066' AND CD7.CD_SYSDEF = MI.GRP_MFG
	LEFT JOIN (SELECT CD_COMPANY, CD_PLANT, NO_WO,
					  SUM(QT_RCVREQ) AS QT_RCVREQ
			   FROM PR_WORK
			   GROUP BY CD_COMPANY, CD_PLANT, NO_WO) WK 
    ON WK.CD_COMPANY = WO.CD_COMPANY AND WK.CD_PLANT = WO.CD_PLANT AND WK.NO_WO = WO.NO_WO
	LEFT JOIN (SELECT CD_COMPANY,
					  CD_FILE,
					  CONVERT(NVARCHAR,COUNT(1)) AS COUNT_FILE
			   FROM MA_FILEINFO  
		       WHERE CD_COMPANY = @P_CD_COMPANY
			   AND ID_MENU = 'P_PR_WO_REG02'
		       GROUP BY CD_COMPANY, CD_FILE) FI 
    ON FI.CD_COMPANY = WO.CD_COMPANY AND FI.CD_FILE = WO.NO_WO
	WHERE WO.CD_COMPANY = @P_CD_COMPANY
	AND WO.CD_PLANT = @P_CD_PLANT
	AND	WO.DT_REL BETWEEN @P_DT_START AND @P_DT_END
	AND	(WO.FG_WO = @P_FG_WO OR @P_FG_WO = '' OR @P_FG_WO IS NULL)
	AND	WO.ST_WO IN (@V_ST_OP_P, @V_ST_OP_O, @V_ST_OP_R, @V_ST_OP_S, @V_ST_OP_C)
	AND	(WO.NO_PJT = @P_NO_PJT OR @P_NO_PJT = '' OR @P_NO_PJT IS NULL)
	AND	(WO.TP_ROUT IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_TP_WO)) OR ISNULL(@P_TP_WO, '') = '')
	AND	(WO.PATN_ROUT = @P_PATN_ROUT OR @P_PATN_ROUT = '' OR @P_PATN_ROUT IS NULL)
	AND	(MI.GRP_ITEM = @P_GRP_ITEM  OR @P_GRP_ITEM  = '' OR @P_GRP_ITEM  IS NULL)
	AND	(WO.NO_WO >= @P_NO_WO_FR OR @P_NO_WO_FR = '' OR @P_NO_WO_FR IS NULL)
	AND	(WO.NO_WO <= @P_NO_WO_TO OR @P_NO_WO_TO = '' OR @P_NO_WO_TO IS NULL)
	AND	(MI.GRP_MFG IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_GRP_MFG)) OR ISNULL(@P_GRP_MFG, '') = '') 
	AND	(WO.NO_EMP = @P_NO_EMP OR @P_NO_EMP IS NULL OR @P_NO_EMP = '')
	AND	(WO.CD_ITEM >= @P_CD_ITEM_FR OR @P_CD_ITEM_FR = '' OR @P_CD_ITEM_FR IS NULL)
	AND	(WO.CD_ITEM <= @P_CD_ITEM_TO OR @P_CD_ITEM_TO = '' OR @P_CD_ITEM_TO IS NULL)
	AND	(ISNULL(@P_CD_WC, '') = '' OR EXISTS (SELECT 1 
											  FROM PR_WO_ROUT B 
											  WHERE B.CD_COMPANY = WO.CD_COMPANY 
											  AND B.CD_PLANT = WO.CD_PLANT 
											  AND B.NO_WO = WO.NO_WO
											  AND B.CD_WC IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_WC))))
	AND	(ISNULL(@P_CD_WCOP, '') = '' OR EXISTS (SELECT 1 
												FROM PR_WO_ROUT B 
												WHERE B.CD_COMPANY = WO.CD_COMPANY 
												AND B.CD_PLANT = WO.CD_PLANT 
												AND B.NO_WO = WO.NO_WO
												AND B.CD_WCOP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_WCOP))))
	AND	(@P_NO_SFT = '' OR @P_NO_SFT IS NULL OR EXISTS (SELECT 1 
													    FROM PR_WO_ROUT B 
														WHERE B.CD_COMPANY = WO.CD_COMPANY 
														AND B.CD_PLANT = WO.CD_PLANT 
														AND B.NO_WO = WO.NO_WO
														AND B.NO_SFT = @P_NO_SFT))
	ORDER BY WO.NO_WO

	--라인조회
	SELECT WO.NO_WO,
		   WO.CD_ITEM,
		   MI.NM_ITEM,
		   MI.STND_ITEM,
		   MI.UNIT_MO,
		   MI.UNIT_IM,
		   WO.QT_ITEM,
		   WO.FG_WO,
		   CD.NM_SYSDEF AS NM_FG_WO,
		   WO.ST_WO,
		   CD1.NM_SYSDEF AS NM_ST_WO,
		   WO.CD_PLANT,
		   WO.TP_ROUT,
		   TW.NM_TP_WO AS NM_TP_ROUT,
		   WO.TP_GI,
		   EJ.NM_QTIOTP AS NM_TP_GI,
		   WO.TP_GR,
		   EJ1.NM_QTIOTP AS NM_TP_GR,
		   WR.DT_REL,
		   WR.DT_DUE,
		   WO.DT_RELEASE,
		   WO.DT_CLOSE,
		   WR.FG_WC,
		   CD2.NM_SYSDEF AS NM_FG_WC,
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
		   WP.NM_OP AS NM_WCOP,
		   WR.CD_WC,
		   WC.NM_WC,
		   WO.NO_SO,
		   WO.PATN_ROUT,
		   RT.DC_OPPATH AS NM_PATN_ROUT,
		   WO.DC_RMK AS DC_RMK_WO,
		   WR.DC_RMK,
		   WR.DC_RMK_1,
		   WR.DC_RMK_2,
		   WR.NO_LINE,
		   MI.GRP_ITEM,
		   IG.NM_ITEMGRP,
		   WO.NO_PJT,
		   PJ.NM_PROJECT,
		   WR.NO_SFT,
		   SF.NM_SFT,
		   WR.CD_EQUIP,
		   EQ.NM_EQUIP,
		   WO.SEQ_PROJECT,
		   PL.CD_ITEM AS CD_PJT_ITEM,
		   MI1.NM_ITEM AS NM_PJT_ITEM,
		   MI1.STND_ITEM AS PJT_ITEM_STND,
		   MI.CLS_L,
		   CD4.NM_SYSDEF AS NM_CLS_L,
		   MI.CLS_M,
		   CD5.NM_SYSDEF AS NM_CLS_M,
		   MI.CLS_S,
		   CD6.NM_SYSDEF AS NM_CLS_S,
		   MI.MAT_ITEM,
		   WO.NO_EMP,
		   ME.NM_KOR,
		   MI.TP_ITEM AS CD_TP_ITEM,
		   CD7.NM_SYSDEF AS TP_ITEM,
		   WO.CD_USERDEF1,
		   WO.CD_USERDEF2,
		   WO.CD_USERDEF3,
		   WO.CD_USERDEF4,
		   WO.CD_USERDEF5,
		   WO.NUM_USERDEF1,
		   WO.NUM_USERDEF2,
		   WO.TXT_USERDEF1,
		   WO.TXT_USERDEF2,
		   WO.TXT_USERDEF3,
		   WO.TXT_USERDEF4,
		   WO.TXT_USERDEF5,
		   STUFF((SELECT '-' + X3.NM_OP
		   		  FROM PR_WO X1
		   		  JOIN PR_WO_ROUT X2 ON X2.CD_COMPANY = X1.CD_COMPANY AND X2.NO_WO = X1.NO_WO
		   		  JOIN PR_WCOP X3 ON X3.CD_COMPANY = X2.CD_COMPANY AND X3.CD_PLANT = X2.CD_PLANT AND X3.CD_WC = X2.CD_WC AND X3.CD_WCOP = X2.CD_WCOP
		   		  WHERE X1.CD_COMPANY = @P_CD_COMPANY
		   		  AND X1.CD_PLANT = @P_CD_PLANT
		   		  AND	X1.DT_REL BETWEEN @P_DT_START AND @P_DT_END
		   		  AND X1.NO_WO = WO.NO_WO
		   		  GROUP BY X2.CD_OP, X2.CD_WCOP, X3.NM_OP
		   		  ORDER BY X2.CD_OP, X2.CD_WCOP
		   		  FOR XML PATH('')),1,1,'') AS NM_OP_LIST,
		   WO.NO_LOT,
		   WO.DT_LIMIT,
		   ISNULL(WO.TXT_USERDEF1, '') + ISNULL(WO.TXT_USERDEF2, '') AS Z_BCWP_USERDEF,
           MI.STND_DETAIL_ITEM,
		   MI.EN_ITEM,
           MI.NM_MAKER,
           MI.BARCODE,
           MI.NO_MODEL,
           MI.NO_DESIGN,
		   MP.LN_PARTNER,
		   MI.WEIGHT,
		   SH.DC_RMK AS DC_RMK_SO,
		   (WR.QT_WO * MI.WEIGHT) AS QT_ST_MM
	FROM PR_WO WO
	JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.CD_PLANT = WO.CD_PLANT AND WR.NO_WO = WO.NO_WO
	JOIN PR_WCOP WP ON WP.CD_COMPANY = WR.CD_COMPANY AND WP.CD_PLANT = WR.CD_PLANT AND WP.CD_WCOP = WR.CD_WCOP AND WP.CD_WC = WR.CD_WC
	JOIN MA_WC WC ON WC.CD_COMPANY = WR.CD_COMPANY AND WC.CD_PLANT = WR.CD_PLANT AND WC.CD_WC = WR.CD_WC
	JOIN MA_PITEM MI ON MI.CD_COMPANY = WO.CD_COMPANY AND MI.CD_PLANT = WO.CD_PLANT AND MI.CD_ITEM = WO.CD_ITEM
	JOIN PR_TPWO TW ON TW.CD_COMPANY = WO.CD_COMPANY AND TW.TP_WO = WO.TP_ROUT
	LEFT JOIN MM_EJTP EJ ON EJ.CD_COMPANY = WO.CD_COMPANY AND EJ.CD_QTIOTP = WO.TP_GI
	LEFT JOIN MM_EJTP EJ1 ON EJ1.CD_COMPANY = WO.CD_COMPANY AND EJ1.CD_QTIOTP = WO.TP_GR
	LEFT JOIN PR_ROUT RT ON RT.CD_COMPANY = WO.CD_COMPANY AND RT.CD_PLANT = WO.CD_PLANT AND RT.CD_ITEM = WO.CD_ITEM AND RT.NO_OPPATH = WO.PATN_ROUT
	LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = WO.CD_COMPANY AND IG.CD_ITEMGRP = MI.GRP_ITEM
	LEFT JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = WO.CD_COMPANY AND PJ.NO_PROJECT = WO.NO_PJT
	LEFT JOIN PR_SHIFT SF ON SF.CD_COMPANY = WR.CD_COMPANY AND SF.CD_PLANT = WR.CD_PLANT AND SF.NO_SFT = WR.NO_SFT
	LEFT JOIN PR_EQUIP EQ ON EQ.CD_COMPANY = WR.CD_COMPANY AND EQ.CD_PLANT = WR.CD_PLANT AND EQ.CD_EQUIP = WR.CD_EQUIP
    LEFT JOIN SA_PROJECTL PL ON WO.CD_COMPANY = PL.CD_COMPANY AND WO.NO_PJT = PL.NO_PROJECT AND WO.SEQ_PROJECT = PL.SEQ_PROJECT
    LEFT JOIN MA_PITEM MI1 ON PL.CD_COMPANY = MI1.CD_COMPANY AND PL.CD_PLANT = MI1.CD_PLANT AND PL.CD_ITEM = MI1.CD_ITEM  
	LEFT JOIN MA_EMP ME ON WO.CD_COMPANY = ME.CD_COMPANY AND WO.CD_PLANT = ME.CD_PLANT AND WO.NO_EMP = ME.NO_EMP	
	LEFT JOIN SA_SOL SL  ON SL.CD_COMPANY = WO.CD_COMPANY AND SL.NO_SO = WO.NO_SO AND WO.NO_LINE_SO = SL.SEQ_SO
	LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = SL.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
	LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = WO.CD_COMPANY AND CD.CD_FIELD = 'PR_0000007' AND CD.CD_SYSDEF = WO.FG_WO
	LEFT JOIN MA_CODEDTL CD1 ON CD1.CD_COMPANY = WO.CD_COMPANY AND CD1.CD_FIELD = 'PR_0000006' AND CD1.CD_SYSDEF = WO.ST_WO
	LEFT JOIN MA_CODEDTL CD2 ON CD2.CD_COMPANY = WR.CD_COMPANY AND CD2.CD_FIELD = 'MA_B000019' AND CD2.CD_SYSDEF = WR.FG_WC
	LEFT JOIN MA_CODEDTL CD3 ON CD3.CD_COMPANY = WR.CD_COMPANY AND CD3.CD_FIELD = 'PR_0000009' AND CD3.CD_SYSDEF = WR.ST_OP
	LEFT JOIN MA_CODEDTL CD4 ON CD4.CD_COMPANY = MI.CD_COMPANY AND CD4.CD_FIELD = 'MA_B000030' AND CD4.CD_SYSDEF = MI.CLS_L
	LEFT JOIN MA_CODEDTL CD5 ON CD5.CD_COMPANY = MI.CD_COMPANY AND CD5.CD_FIELD = 'MA_B000031' AND CD5.CD_SYSDEF = MI.CLS_M
	LEFT JOIN MA_CODEDTL CD6 ON CD6.CD_COMPANY = MI.CD_COMPANY AND CD6.CD_FIELD = 'MA_B000032' AND CD6.CD_SYSDEF = MI.CLS_S
	LEFT JOIN MA_CODEDTL CD7 ON CD7.CD_COMPANY = MI.CD_COMPANY AND CD7.CD_FIELD = 'MA_B000011' AND CD7.CD_SYSDEF = MI.TP_ITEM
	WHERE WO.CD_COMPANY = @P_CD_COMPANY
	AND WO.CD_PLANT = @P_CD_PLANT
	AND	WO.DT_REL BETWEEN @P_DT_START AND @P_DT_END
	AND	(WO.FG_WO = @P_FG_WO OR @P_FG_WO = '' OR @P_FG_WO IS NULL)
	AND	WO.ST_WO IN (@V_ST_OP_P, @V_ST_OP_O, @V_ST_OP_R, @V_ST_OP_S, @V_ST_OP_C)
	AND	(WO.NO_PJT = @P_NO_PJT OR @P_NO_PJT = '' OR @P_NO_PJT IS NULL)
	AND	(WO.TP_ROUT IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_TP_WO)) OR ISNULL(@P_TP_WO, '') = '')
	AND	(WO.PATN_ROUT = @P_PATN_ROUT OR @P_PATN_ROUT = '' OR @P_PATN_ROUT IS NULL)
	AND	(MI.GRP_ITEM  = @P_GRP_ITEM  OR @P_GRP_ITEM  = '' OR @P_GRP_ITEM  IS NULL)
	AND	(WR.CD_WC IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_WC)) OR ISNULL(@P_CD_WC,   '') = '')
	AND	(WR.CD_WCOP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_WCOP)) OR ISNULL(@P_CD_WCOP, '') = '')
	AND	(WO.NO_WO >= @P_NO_WO_FR OR @P_NO_WO_FR = '' OR @P_NO_WO_FR IS NULL)
	AND	(WO.NO_WO <= @P_NO_WO_TO OR @P_NO_WO_TO = '' OR @P_NO_WO_TO IS NULL)
	AND	(MI.GRP_MFG IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_GRP_MFG)) OR ISNULL(@P_GRP_MFG, '') = '')
	AND	(WR.NO_SFT = @P_NO_SFT OR @P_NO_SFT = '' OR @P_NO_SFT IS NULL) 
	AND	(WO.NO_EMP = @P_NO_EMP OR @P_NO_EMP IS NULL OR @P_NO_EMP = '')
	AND	(WO.CD_ITEM >= @P_CD_ITEM_FR OR @P_CD_ITEM_FR = '' OR @P_CD_ITEM_FR IS NULL)
	AND	(WO.CD_ITEM <= @P_CD_ITEM_TO OR @P_CD_ITEM_TO = '' OR @P_CD_ITEM_TO IS NULL)
	ORDER BY WO.NO_WO, WR.CD_OP

	OPTION(RECOMPILE) --D20200203235 : 대경기업 인덱스 이상하게탐.. 원인 찾을수 없어서 리컴파일넣엇음..
END
GO
