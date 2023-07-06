ALTER PROCEDURE [NEOE].[SP_CZ_QR_LABEL_GR_STOCK_S]
(
	@P_MODE			INT, 
	@P_CD_COMPANY	NVARCHAR(7), 
	@P_NO_FILE		NVARCHAR(20), 
	@P_NO_HULL		NVARCHAR(20),
	@P_CD_SUPPLIER	NVARCHAR(20), 
	@P_CD_ITEM		NVARCHAR(20) 
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

IF @P_MODE = 0
BEGIN
	SELECT 'N' AS S,
		   PL.CD_COMPANY,
	       PL.NO_PO,
		   QL.NO_FILE,
		   PL.NO_LINE,
		   D.NO_SEQ,
		   MP.LN_PARTNER AS NM_SUPPLIER,
		   ME.NM_KOR AS NM_EMP,
		   PH.NO_ORDER,
		   MP1.LN_PARTNER AS NM_BUYER,
		   MH.NM_VESSEL,
		   SH.NO_PO_PARTNER AS NO_REF,
		   QL.NO_DSP,
		   QL.NM_SUBJECT,
		   PL.CD_ITEM,
		   QL.CD_ITEM_PARTNER,
		   MI.NM_ITEM, 
		   (CASE WHEN ISNULL(QL.YN_DSP_RMK, 'N') = 'Y' AND ISNULL(QL.DC_RMK, '') <> '' THEN QL.NM_ITEM_PARTNER + ' (*OFFER:' + QL.DC_RMK + ')' 
																					   ELSE QL.NM_ITEM_PARTNER END) AS NM_ITEM_PARTNER,
		   PL.CD_ITEM,
		   MD.NM_SYSDEF AS NM_UNIT,
		   PL.QT_PO,
		   (SELECT ISNULL(SUM(QT_IO), 0) 
		    FROM MM_QTIO OL
			WHERE OL.CD_COMPANY = PH.CD_COMPANY
			AND OL.NO_PSO_MGMT = PH.NO_PO
			AND OL.NO_PSOLINE_MGMT = PL.NO_LINE
			AND OL.FG_PS = '1') AS QT_GR,
		   ISNULL(D.QT, 0) AS QT_LABEL,
		   (ISNULL(PL.QT_PO, 0) - ISNULL(D.QT, 0)) AS QT_WORK
	FROM PU_POH PH
	JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
	LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = PL.CD_COMPANY AND QL.NO_FILE = PL.CD_PJT AND QL.NO_LINE = PL.NO_LINE
	LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = QL.CD_COMPANY AND QH.NO_FILE = QL.NO_FILE
	LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = PL.CD_COMPANY AND SH.NO_SO = PL.CD_PJT
	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
	LEFT JOIN MA_PARTNER MP1 ON MP1.CD_COMPANY = QH.CD_COMPANY AND MP1.CD_PARTNER = QH.CD_PARTNER
	LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = PH.CD_COMPANY AND ME.NO_EMP = PH.NO_EMP
	LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
	LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = PL.CD_COMPANY AND MI.CD_PLANT = PL.CD_PLANT AND MI.CD_ITEM = PL.CD_ITEM
	LEFT JOIN MA_CODEDTL MD ON MD.CD_COMPANY = QL.CD_COMPANY AND MD.CD_FIELD = 'MA_B000004' AND MD.CD_SYSDEF = QL.UNIT
	LEFT JOIN (SELECT CD_COMPANY, NO_PO, NO_LINE, 
			   	      -1 AS NO_SEQ,
			   	      SUM(QT) AS QT
			   FROM CZ_QR_LABEL
			   GROUP BY CD_COMPANY, NO_PO, NO_LINE) D
	ON D.CD_COMPANY = PL.CD_COMPANY AND D.NO_PO = PL.NO_PO AND D.NO_LINE = PL.NO_LINE
	WHERE PL.CD_COMPANY = @P_CD_COMPANY
	AND PL.CD_ITEM NOT LIKE 'SD%'
	AND ISNULL(PL.QT_PO, 0) - ISNULL(D.QT, 0) > 0
	AND (ISNULL(@P_NO_FILE, '') = '' OR PL.CD_PJT = @P_NO_FILE OR PL.NO_PO = @P_NO_FILE)
	AND (ISNULL(@P_NO_HULL, '') = '' OR MH.NO_HULL = @P_NO_HULL)
	AND (ISNULL(@P_CD_SUPPLIER, '') = '' OR PH.CD_PARTNER = @P_CD_SUPPLIER)
	AND (ISNULL(@P_CD_ITEM, '') = '' OR PL.CD_ITEM = @P_CD_ITEM)
	ORDER BY PL.NO_PO, QL.NO_DSP
	OPTION(RECOMPILE)
END
ELSE
BEGIN
	SELECT 'N' AS S,
	       PL.CD_COMPANY,
	       PL.NO_PO,
		   QL.NO_FILE,
		   PL.NO_LINE,
		   D.NO_SEQ,
		   MP.LN_PARTNER AS NM_SUPPLIER,
		   ME.NM_KOR AS NM_EMP,
		   PH.NO_ORDER,
		   MP1.LN_PARTNER AS NM_BUYER,
		   MH.NM_VESSEL,
		   SH.NO_PO_PARTNER AS NO_REF,
		   QL.NO_DSP,
		   QL.NM_SUBJECT,
		   PL.CD_ITEM,
		   QL.CD_ITEM_PARTNER,
		   MI.NM_ITEM, 
		   (CASE WHEN ISNULL(QL.YN_DSP_RMK, 'N') = 'Y' AND ISNULL(QL.DC_RMK, '') <> '' THEN QL.NM_ITEM_PARTNER + ' (*OFFER:' + QL.DC_RMK + ')' 
																					   ELSE QL.NM_ITEM_PARTNER END) AS NM_ITEM_PARTNER,
		   PL.CD_ITEM,
		   MD.NM_SYSDEF AS NM_UNIT,
		   PL.QT_PO,
		   (SELECT ISNULL(SUM(QT_IO), 0) 
		    FROM MM_QTIO OL
			WHERE OL.CD_COMPANY = PH.CD_COMPANY
			AND OL.NO_PSO_MGMT = PH.NO_PO
			AND OL.NO_PSOLINE_MGMT = PL.NO_LINE
			AND OL.FG_PS = '1') AS QT_GR,
		   ISNULL(D.QT, 0) AS QT_LABEL,
		   ISNULL(D.QT, 0) AS QT_WORK
	FROM PU_POH PH
	JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
	LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = PL.CD_COMPANY AND QL.NO_FILE = PL.CD_PJT AND QL.NO_LINE = PL.NO_LINE
	LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = QL.CD_COMPANY AND QH.NO_FILE = QL.NO_FILE
	LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = PL.CD_COMPANY AND SH.NO_SO = PL.CD_PJT
	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
	LEFT JOIN MA_PARTNER MP1 ON MP1.CD_COMPANY = QH.CD_COMPANY AND MP1.CD_PARTNER = QH.CD_PARTNER
	LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = PH.CD_COMPANY AND ME.NO_EMP = PH.NO_EMP
	LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
	LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = PL.CD_COMPANY AND MI.CD_PLANT = PL.CD_PLANT AND MI.CD_ITEM = PL.CD_ITEM
	LEFT JOIN MA_CODEDTL MD ON MD.CD_COMPANY = QL.CD_COMPANY AND MD.CD_FIELD = 'MA_B000004' AND MD.CD_SYSDEF = QL.UNIT
	JOIN CZ_QR_LABEL D ON D.CD_COMPANY = PL.CD_COMPANY AND D.NO_PO = PL.NO_PO AND D.NO_LINE = PL.NO_LINE
	WHERE PL.CD_COMPANY = @P_CD_COMPANY
	AND PL.CD_ITEM NOT LIKE 'SD%'
	AND (ISNULL(@P_NO_FILE, '') = '' OR PL.CD_PJT = @P_NO_FILE OR PL.NO_PO = @P_NO_FILE)
	AND (ISNULL(@P_NO_HULL, '') = '' OR MH.NO_HULL = @P_NO_HULL)
	AND (ISNULL(@P_CD_SUPPLIER, '') = '' OR PH.CD_PARTNER = @P_CD_SUPPLIER)
	AND (ISNULL(@P_CD_ITEM, '') = '' OR PL.CD_ITEM = @P_CD_ITEM)
	ORDER BY PL.NO_PO, QL.NO_DSP
	OPTION(RECOMPILE)
END