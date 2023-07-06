USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIR_FORMAT_S]    Script Date: 2016-03-29 오후 1:52:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIR_FORMAT_S]
(
	@P_CD_COMPANY       NVARCHAR(7),
	@P_CD_PARTNER       NVARCHAR(20),
	@P_NO_IMO			NVARCHAR(10),
	@P_YN_PACK			NVARCHAR(1)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

IF @P_YN_PACK = 'Y'
SELECT MAX(GH.DT_GIR) AS DT_GIR,
	   GH.CD_PARTNER,
	   MP.LN_PARTNER,
	   PD.NO_IMO,
	   MH.NO_HULL,
	   MH.NM_VESSEL,	
	   MD.CD_PARTNER AS CD_DELIVERY_TO,
	   MD.LN_PARTNER AS NM_DELIVERY_TO, 
	   PD.CD_PACK_CATEGORY AS CD_MAIN_CATEGORY,
	   MC.NM_SYSDEF AS NM_MAIN_CATEGORY,
	   PD.CD_SUB_CATEGORY,
	   MC1.NM_SYSDEF AS NM_SUB_CATEGORY,
	   PD.CD_RMK,
	   MC2.NM_SYSDEF AS NM_RMK,
	   TH.CD_CONSIGNEE,
	   TH.NM_CONSIGNEE,
	   TH.ADDR1_CONSIGNEE,
	   TH.ADDR2_CONSIGNEE,
	   TH.CD_PRODUCT,
	   TH.ARRIVER_COUNTRY,
	   MC3.NM_SYSDEF_E AS NM_ARRIVER_COUNTRY,
	   TH.PORT_ARRIVER,
	   TH.REMARK1,
	   TH.REMARK2,
	   TH.REMARK3,
	   (CASE WHEN ISNULL(PD.CD_COLLECT_FROM, '') <> '' AND MD.CD_PARTNER NOT LIKE 'DLV%' THEN 'Y' ELSE 'N' END) AS YN_ERROR,
	   MAX(ISNULL(MD.DC_ADDRESS, '') + ' ' + ISNULL(MD.DC_ADDRESS1, '')) AS DC_ADDRESS,
       MAX(CASE WHEN ISNULL(MD.NM_PIC, '') = '' THEN TRIM(ISNULL(MD.NO_TEL, '')) ELSE TRIM(ISNULL(MD.NM_PIC, '') + ' ' + ISNULL(MD.NO_TEL, '')) END) AS NO_TEL,
	   (CASE WHEN ISNULL(PD.CD_COLLECT_FROM, '') <> '' AND MD.CD_PARTNER NOT LIKE 'DLV%' THEN MD1.LN_PARTNER ELSE NULL END) AS NM_DELIVERY_TO_OLD 
FROM CZ_SA_GIRH_PACK GH
LEFT JOIN CZ_SA_GIRH_PACK_DETAIL PD ON PD.CD_COMPANY = GH.CD_COMPANY AND PD.NO_GIR = GH.NO_GIR
LEFT JOIN (SELECT CD_COMPANY, NO_GIR, NO_INV 
		   FROM CZ_SA_GIRL_PACK
		   GROUP BY CD_COMPANY, NO_GIR, NO_INV) GL
ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
LEFT JOIN CZ_TR_INVH TH ON TH.CD_COMPANY = GL.CD_COMPANY AND TH.NO_INV = GL.NO_INV
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = PD.NO_IMO
LEFT JOIN CZ_MA_DELIVERY_MAP DM ON DM.CD_COMPANY = PD.CD_COMPANY AND DM.CD_PARTNER_OLD = PD.CD_COLLECT_FROM
LEFT JOIN CZ_MA_DELIVERY MD ON MD.CD_COMPANY = PD.CD_COMPANY AND MD.CD_PARTNER = ISNULL(DM.CD_PARTNER_NEW, PD.CD_COLLECT_FROM)
LEFT JOIN CZ_MA_DELIVERY MD1 ON MD1.CD_COMPANY = PD.CD_COMPANY AND MD1.CD_PARTNER = PD.CD_COLLECT_FROM
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = GH.CD_COMPANY AND MP.CD_PARTNER = GH.CD_PARTNER
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00020' AND MC.CD_SYSDEF = PD.CD_PACK_CATEGORY
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = PD.CD_COMPANY AND MC1.CD_FIELD = MC.CD_FLAG1 AND MC1.CD_SYSDEF = PD.CD_SUB_CATEGORY
LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = PD.CD_COMPANY AND MC2.CD_FIELD = 'CZ_SA00032' AND MC2.CD_SYSDEF = PD.CD_RMK
LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = TH.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000020' AND MC3.CD_SYSDEF = TH.ARRIVER_COUNTRY
WHERE GH.CD_COMPANY = @P_CD_COMPANY
AND (ISNULL(@P_CD_PARTNER, '') = '' OR GH.CD_PARTNER = @P_CD_PARTNER)
AND (ISNULL(@P_NO_IMO, '') = '' OR PD.NO_IMO = @P_NO_IMO)
GROUP BY GH.CD_PARTNER, MP.LN_PARTNER, PD.NO_IMO, MH.NO_HULL, MH.NM_VESSEL,
		 PD.CD_COLLECT_FROM, MD.CD_PARTNER, MD.LN_PARTNER, PD.CD_PACK_CATEGORY, PD.CD_SUB_CATEGORY, PD.CD_RMK, MC.NM_SYSDEF, MC1.NM_SYSDEF, MC2.NM_SYSDEF,
		 TH.CD_CONSIGNEE, TH.NM_CONSIGNEE, TH.ADDR1_CONSIGNEE, TH.ADDR2_CONSIGNEE, TH.CD_PRODUCT, TH.ARRIVER_COUNTRY, MC3.NM_SYSDEF_E, TH.PORT_ARRIVER, TH.REMARK1, TH.REMARK2, TH.REMARK3,
		 MD1.LN_PARTNER
ORDER BY GH.CD_PARTNER, PD.NO_IMO, MAX(GH.DT_GIR)
ELSE
SELECT MAX(GH.DT_GIR) AS DT_GIR,
	   GH.CD_PARTNER,
	   MP.LN_PARTNER,
	   WD.NO_IMO,
	   MH.NO_HULL,
	   MH.NM_VESSEL,
	   MD.CD_PARTNER AS CD_DELIVERY_TO,
	   MD.LN_PARTNER AS NM_DELIVERY_TO, 
	   WD.CD_MAIN_CATEGORY,
	   MC.NM_SYSDEF AS NM_MAIN_CATEGORY,
	   WD.CD_SUB_CATEGORY,
	   MC1.NM_SYSDEF AS NM_SUB_CATEGORY,
	   WD.CD_RMK,
	   MC2.NM_SYSDEF AS NM_RMK,
	   TH.CD_CONSIGNEE,
	   TH.NM_CONSIGNEE,
	   TH.ADDR1_CONSIGNEE,
	   TH.ADDR2_CONSIGNEE,
	   TH.CD_PRODUCT,
	   TH.ARRIVER_COUNTRY,
	   MC3.NM_SYSDEF_E AS NM_ARRIVER_COUNTRY,
	   TH.PORT_ARRIVER,
	   TH.REMARK1,
	   TH.REMARK2,
	   TH.REMARK3,
	   (CASE WHEN ISNULL(WD.CD_DELIVERY_TO, '') <> '' AND MD.CD_PARTNER NOT LIKE 'DLV%' THEN 'Y' ELSE 'N' END) AS YN_ERROR,
	   MAX(ISNULL(MD.DC_ADDRESS, '') + ' ' + ISNULL(MD.DC_ADDRESS1, '')) AS DC_ADDRESS,
       MAX(CASE WHEN ISNULL(MD.NM_PIC, '') = '' THEN TRIM(ISNULL(MD.NO_TEL, '')) ELSE TRIM(ISNULL(MD.NM_PIC, '') + ' ' + ISNULL(MD.NO_TEL, '')) END) AS NO_TEL,
	   (CASE WHEN ISNULL(WD.CD_DELIVERY_TO, '') <> '' AND MD.CD_PARTNER NOT LIKE 'DLV%' THEN MD1.LN_PARTNER ELSE NULL END) AS NM_DELIVERY_TO_OLD
FROM SA_GIRH GH
LEFT JOIN CZ_SA_GIRH_WORK_DETAIL WD ON WD.CD_COMPANY = GH.CD_COMPANY AND WD.NO_GIR = GH.NO_GIR
LEFT JOIN (SELECT CD_COMPANY, NO_GIR, NO_INV 
		   FROM SA_GIRL
		   GROUP BY CD_COMPANY, NO_GIR, NO_INV) GL
ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
LEFT JOIN CZ_TR_INVH TH ON TH.CD_COMPANY = GL.CD_COMPANY AND TH.NO_INV = GL.NO_INV
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = WD.NO_IMO
LEFT JOIN CZ_MA_DELIVERY_MAP DM ON DM.CD_COMPANY = WD.CD_COMPANY AND DM.CD_PARTNER_OLD = WD.CD_DELIVERY_TO
LEFT JOIN CZ_MA_DELIVERY MD ON MD.CD_COMPANY = WD.CD_COMPANY AND MD.CD_PARTNER = ISNULL(DM.CD_PARTNER_NEW, WD.CD_DELIVERY_TO)
LEFT JOIN CZ_MA_DELIVERY MD1 ON MD1.CD_COMPANY = WD.CD_COMPANY AND MD1.CD_PARTNER = WD.CD_DELIVERY_TO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = GH.CD_COMPANY AND MP.CD_PARTNER = GH.CD_PARTNER
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = WD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00006' AND MC.CD_SYSDEF = WD.CD_MAIN_CATEGORY
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = WD.CD_COMPANY AND MC1.CD_FIELD = MC.CD_FLAG1 AND MC1.CD_SYSDEF = WD.CD_SUB_CATEGORY
LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = WD.CD_COMPANY AND MC2.CD_FIELD = 'CZ_SA00032' AND MC2.CD_SYSDEF = WD.CD_RMK
LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = TH.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000020' AND MC3.CD_SYSDEF = TH.ARRIVER_COUNTRY
WHERE GH.CD_COMPANY = @P_CD_COMPANY
AND (ISNULL(@P_CD_PARTNER, '') = '' OR GH.CD_PARTNER = @P_CD_PARTNER)
AND (ISNULL(@P_NO_IMO, '') = '' OR WD.NO_IMO = @P_NO_IMO)
GROUP BY GH.CD_PARTNER, MP.LN_PARTNER, WD.NO_IMO, MH.NO_HULL, MH.NM_VESSEL,
		 WD.CD_DELIVERY_TO, MD.CD_PARTNER, MD.LN_PARTNER, WD.CD_MAIN_CATEGORY, WD.CD_SUB_CATEGORY, WD.CD_RMK, MC.NM_SYSDEF, MC1.NM_SYSDEF, MC2.NM_SYSDEF,
		 TH.CD_CONSIGNEE, TH.NM_CONSIGNEE, TH.ADDR1_CONSIGNEE, TH.ADDR2_CONSIGNEE, TH.CD_PRODUCT, TH.ARRIVER_COUNTRY, MC3.NM_SYSDEF_E, TH.PORT_ARRIVER, TH.REMARK1, TH.REMARK2, TH.REMARK3,
		 MD1.LN_PARTNER
ORDER BY GH.CD_PARTNER, WD.NO_IMO, MAX(GH.DT_GIR)

GO