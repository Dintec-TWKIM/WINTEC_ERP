USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_CZ_PR_OPOUT_PR_MNG_SELECT]    Script Date: 2022-12-12 오후 6:41:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[UP_CZ_PR_OPOUT_PR_MNG_SELECT]
(
    @P_CD_COMPANY   NVARCHAR(7), 
    @P_CD_PLANT     NVARCHAR(7), 
    @P_DT_START     NVARCHAR(8), 
    @P_DT_END       NVARCHAR(8), 
    @P_NO_WO		NVARCHAR(20)	= NULL
)
AS


BEGIN

	SELECT 
		'N' AS CHK
	,	A.CD_COMPANY
	,	A.CD_PLANT
	,	A.NO_PR
	,	A.NO_LINE
	,	A.DT_PR
	,	A.NO_EMP
	,	J.NM_KOR
	,	A.NO_WO
	,	B.NO_LINE AS NO_WO_LINE
	,	A.CD_OP
	,	A.CD_WC
	,	E.NM_WC
	,	A.CD_WCOP
	,	D.NM_OP
	,	A.CD_ITEM
	,	C.NM_ITEM
	,	C.STND_ITEM
	,	C.NO_DESIGN
	,	C.UNIT_IM
	,	B.QT_START
	,	A.QT_PR
	,	A.DC_RMK
	,	(CASE WHEN SUM(F.QT_RCV) = A.QT_PR THEN '완료'
			  WHEN SUM(F.QT_PO) > 0 THEN '발주'
		 ELSE '요청' END) ST_PR
	,	A.CD_PARTNER
	,	G.LN_PARTNER
	,	A.DT_DUE
	,	B.CD_WCOP_NEXT
	,	H.NM_OP AS NM_OP_NEXT
	FROM CZ_PR_OPOUT_PR A
	LEFT JOIN PR_WO_ROUT B ON B.CD_COMPANY = A.CD_COMPANY AND B.CD_PLANT = A.CD_PLANT AND B.NO_WO = A.NO_WO AND B.CD_OP = A.CD_OP
	LEFT JOIN MA_PITEM C ON C.CD_COMPANY = A.CD_COMPANY AND C.CD_PLANT = A.CD_PLANT AND C.CD_ITEM = A.CD_ITEM
	LEFT JOIN PR_WCOP D ON D.CD_COMPANY = A.CD_COMPANY AND D.CD_PLANT = A.CD_PLANT AND D.CD_WC = A.CD_WC AND D.CD_WCOP = A.CD_WCOP
	LEFT JOIN MA_WC E ON E.CD_COMPANY = A.CD_COMPANY AND E.CD_PLANT = A.CD_PLANT AND E.CD_WC = A.CD_WC
	LEFT JOIN PR_OPOUT_POL F ON F.CD_COMPANY = A.CD_COMPANY AND F.CD_PLANT = A.CD_PLANT AND F.NO_PR = A.NO_PR AND F.NO_WO = A.NO_WO
	LEFT JOIN MA_PARTNER G ON G.CD_COMPANY = A.CD_COMPANY AND G.CD_PARTNER = A.CD_PARTNER
	LEFT JOIN PR_WCOP H ON H.CD_COMPANY = B.CD_COMPANY AND H.CD_PLANT = B.CD_PLANT AND H.CD_WC = B.CD_WC_NEXT AND H.CD_WCOP = B.CD_WCOP_NEXT
	LEFT JOIN MA_WC I ON I.CD_COMPANY = B.CD_COMPANY AND I.CD_PLANT = B.CD_PLANT AND I.CD_WC = B.CD_WC_NEXT
	LEFT  JOIN DZSN_MA_EMP J ON J.CD_COMPANY = A.CD_COMPANY AND J.NO_EMP = A.NO_EMP
	WHERE	(A.CD_COMPANY = @P_CD_COMPANY)
	AND		(A.CD_PLANT   = @p_CD_PLANT)
	AND		(A.NO_WO = @P_NO_WO OR @P_NO_WO = '' OR @P_NO_WO IS NULL)
	GROUP BY 	A.CD_COMPANY, A.CD_PLANT ,A.NO_PR, A.NO_LINE, A.DT_PR, A.NO_EMP, A.NO_WO, B.NO_LINE, A.CD_OP, A.CD_WC, E.NM_WC, A.CD_WCOP, D.NM_OP
			  , A.CD_ITEM,	C.NM_ITEM, C.STND_ITEM,	C.NO_DESIGN, C.UNIT_IM,	B.QT_START, A.QT_PR, A.DC_RMK, A.CD_PARTNER, G.LN_PARTNER, A.DT_DUE
			  , B.CD_WCOP_NEXT, H.NM_OP, J.NM_KOR
	HAVING A.DT_PR BETWEEN @P_DT_START AND @P_DT_END


END
