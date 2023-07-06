USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_CZ_PR_OPOUT_PO_MNG_L_SELECT]    Script Date: 2022-12-09 오후 4:15:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_CZ_PR_OPOUT_PO_MNG_L_SELECT]
(
    @P_CD_COMPANY   NVARCHAR(7), 
    @P_CD_PLANT     NVARCHAR(7), 
    @P_CONDITION    NVARCHAR(20),
    @P_NO_EMP		NVARCHAR(10)	= NULL,
    @P_NO_WO		NVARCHAR(20)	= NULL,
    @P_FG_LANG		NVARCHAR(4)		= NULL,
	@P_CD_SEARCH1	NVARCHAR(3)		= NULL,
	@P_TXT_SEARCH1	NVARCHAR(100)	= NULL
)
AS
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG


DECLARE	@YN_MFG_AUTH NCHAR(1)
SELECT	@YN_MFG_AUTH  = ISNULL((SELECT	YN_MFG_AUTH
								FROM	MA_ENV
								WHERE	CD_COMPANY = @P_CD_COMPANY ), 'N')

BEGIN
	SELECT	L.CD_PLANT, 
			L.NO_PO,		L.NO_LINE, 
			L.NO_WO, 
			L.CD_OP, 
			L.CD_WC,		A.NM_WC, 
			L.CD_WCOP,		B.NM_OP, 
			L.NO_WORK, 
			L.CD_ITEM,		P.NM_ITEM,			P.STND_ITEM,			P.UNIT_IM,
			L.DT_DUE, 
			L.QT_PO, 
			L.QT_RCV, 
			L.QT_CLS, 
			L.UM_EX, 
			L.AM_EX, 
			L.UM, 
			L.AM, 
			L.AM_VAT, 
			L.DC_RMK, 
			L.AM + L.AM_VAT AS AM_SUM, 
			H.VAT_RATE, 
			P.FG_SERNO,		NEOE.FN_MA_GET_CODEDTL_NM_SYSDEF(@P_CD_COMPANY,'MA_B000015',P.FG_SERNO) AS NM_FG_SERNO, 
			P.GRP_ITEM,		G.NM_ITEMGRP AS NM_GRP_ITEM,  
			L.OLD_QT_PO,
			L.UNIT_CH,
			L.QT_CHCOEF, 
			H.CD_PARTNER,
			PARTNER.LN_PARTNER,
			PARTNER.NO_COMPANY,
			PARTNER.DC_ADS1_H + ' ' + PARTNER.DC_ADS1_D AS DC_ADS,
			PARTNER.TP_JOB,
			PARTNER.CLS_JOB,
			P.EN_ITEM,
            P.STND_DETAIL_ITEM,
            P.NM_MAKER,
            P.BARCODE,
            P.NO_MODEL,
            P.NO_DESIGN,
            P.MAT_ITEM,
            P.CLS_L,
            P.CLS_M,
            P.CLS_S,
            W.TXT_USERDEF1 AS TXT_USERDEF1_WO,
			R.NO_LINE AS NO_WO_LINE
	FROM PR_OPOUT_POL L
		INNER JOIN PR_OPOUT_POH H ON H.CD_COMPANY = L.CD_COMPANY AND H.CD_PLANT = L.CD_PLANT AND H.NO_PO = L.NO_PO
		INNER JOIN PR_WO W ON W.CD_COMPANY = L.CD_COMPANY AND W.CD_PLANT = L.CD_PLANT AND W.NO_WO = L.NO_WO  
		LEFT  JOIN DZSN_MA_PITEM		P ON P.CD_COMPANY = L.CD_COMPANY AND P.CD_PLANT = L.CD_PLANT AND P.CD_ITEM = L.CD_ITEM
		LEFT  JOIN DZSN_MA_WC		A ON A.CD_COMPANY = L.CD_COMPANY AND A.CD_PLANT = L.CD_PLANT AND A.CD_WC = L.CD_WC
		LEFT  JOIN DZSN_PR_WCOP		B ON B.CD_COMPANY = L.CD_COMPANY AND B.CD_PLANT = L.CD_PLANT AND B.CD_WC = L.CD_WC AND B.CD_WCOP = L.CD_WCOP
		LEFT  JOIN DZSN_MA_ITEMGRP	G ON G.CD_COMPANY = P.CD_COMPANY AND G.CD_ITEMGRP = P.GRP_ITEM
		LEFT  JOIN DZSN_MA_PARTNER	PARTNER ON PARTNER.CD_COMPANY = H.CD_COMPANY AND PARTNER.CD_PARTNER = H.CD_PARTNER
		LEFT  JOIN PR_WO_ROUT R ON R.CD_COMPANY = L.CD_COMPANY AND R.CD_PLANT = L.CD_PLANT AND R.NO_WO = L.NO_WO AND R.CD_OP = L.CD_OP
	WHERE	(H.CD_COMPANY = @P_CD_COMPANY)
	AND		(H.CD_PLANT   = @p_CD_PLANT)
	AND		(L.NO_PO	  = @P_CONDITION)
	AND		(L.NO_WO = @P_NO_WO OR @P_NO_WO = '' OR @P_NO_WO IS NULL)
	AND (	(@YN_MFG_AUTH <> 'Y' OR  ISNULL(@P_NO_EMP,'') =  '')
	    OR	(@YN_MFG_AUTH =  'Y' AND ISNULL(@P_NO_EMP,'') <> ''  AND EXISTS (SELECT	1
																			 FROM	MA_MFG_AUTH AUTH
																			 WHERE	AUTH.CD_COMPANY = L.CD_COMPANY
																			 AND	AUTH.CD_AUTH	= L.CD_WC
																			 AND	AUTH.FG_AUTH	= 'MA_WC'
																			 AND	AUTH.NO_EMP		= @P_NO_EMP)) )
	AND (	(@P_TXT_SEARCH1 IS NULL OR @P_TXT_SEARCH1 = '')
		OR	(CASE @P_CD_SEARCH1 
					WHEN '001' THEN P.CD_ITEM
					WHEN '002' THEN P.NM_ITEM
					WHEN '003' THEN P.STND_ITEM
					WHEN '004' THEN P.EN_ITEM
					WHEN '005' THEN P.STND_DETAIL_ITEM
					WHEN '006' THEN P.NM_MAKER
					WHEN '007' THEN P.BARCODE
					WHEN '008' THEN P.NO_MODEL
					WHEN '009' THEN P.NO_DESIGN END LIKE '%' + @P_TXT_SEARCH1 + '%')
		OR	((@P_CD_SEARCH1 IS NULL OR @P_CD_SEARCH1 = '') AND 
					P.CD_ITEM LIKE '%' + @P_TXT_SEARCH1 + '%' OR   
					P.NM_ITEM LIKE '%' + @P_TXT_SEARCH1 + '%' OR   
					P.STND_ITEM LIKE '%' + @P_TXT_SEARCH1 + '%' OR  
					P.EN_ITEM LIKE '%' + @P_TXT_SEARCH1 + '%' OR  
					P.STND_DETAIL_ITEM LIKE '%' + @P_TXT_SEARCH1 + '%' OR  
					P.NM_MAKER LIKE '%' + @P_TXT_SEARCH1 + '%' OR
					P.BARCODE LIKE '%' + @P_TXT_SEARCH1 + '%' OR
					P.NO_MODEL LIKE '%' + @P_TXT_SEARCH1 + '%' OR  
					P.NO_DESIGN LIKE '%' + @P_TXT_SEARCH1 + '%')	)
END
