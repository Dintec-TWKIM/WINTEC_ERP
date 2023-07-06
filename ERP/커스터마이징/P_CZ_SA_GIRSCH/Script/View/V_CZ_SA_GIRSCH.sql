USE [NEOE]
GO

/****** Object:  View [NEOE].[V_CZ_SA_GIRSCH]    Script Date: 2015-06-19 오전 11:42:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [NEOE].[V_CZ_SA_GIRSCH]
AS

SELECT GL.CD_COMPANY,
	   MC.NM_COMPANY,
	   GL.NO_GIR,
	   SL.NO_SO,
	   GL.RET,
	   GL.CD_SALEGRP,
	   WD.DTS_SUBMIT,
	   WD.DTS_CONFIRM,
	   FT.QT_TAX,
	   SH.TP_SO,
	   SH.NO_EMP AS NO_SA_EMP,
	   GH.DT_GIR,
	   GH.CD_PARTNER,
	   MP.LN_PARTNER,
	   MP.CD_PARTNER_GRP,
	   MD5.NM_SYSDEF AS NM_PARTNER_GRP,
	   GH.CD_PLANT,
	   PT.NM_PLANT,
	   GH.YN_RETURN,
	   MD3.NM_SYSDEF AS NM_RETURN,
	   MD4.NM_SYSDEF AS NM_BUSI,
	   GH.STA_GIR,
	   MD8.NM_SYSDEF AS NM_STA_GIR,
	   ME.NM_KOR AS NM_EMP_SALE,
	   WD.DC_RMK,
	   WD.DC_RMK1,
	   WD.DC_RMK2,
	   WD.DC_RESULT,
	   '001' AS CD_TYPE,
	   MD6.NM_SYSDEF AS NM_TYPE1,
	   MD6.NM_SYSDEF_E AS NM_TYPE1_E,
	   WD.CD_MAIN_CATEGORY AS CD_TYPE2,
	   MD.NM_SYSDEF AS NM_TYPE2,
	   MD.NM_SYSDEF_E AS NM_TYPE2_E,
	   WD.CD_SUB_CATEGORY AS CD_TYPE3,
	   MD7.NM_SYSDEF AS NM_TYPE3,
	   MD7.NM_SYSDEF_E AS NM_TYPE3_E,
	   WD.NO_IMO,
	   MH.NM_VESSEL,
	   WD.DT_COMPLETE,
	   WD.YN_PACKING,
	   GH.NO_EMP,
	   ME2.NM_KOR AS NM_EMP_GIR,
	   WD.DT_LOADING + '000000' AS DT_DONE,
	   WD.CD_DELIVERY_TO AS CD_GIR_PARTNER,
	   MP2.LN_PARTNER AS NM_GIR_PARTNER,
	   NEOE.FN_CZ_YN_URGENT(GL.CD_COMPANY, 'N', WD.CD_MAIN_CATEGORY, WD.CD_SUB_CATEGORY, WD.DTS_SUBMIT, WD.DT_COMPLETE) AS YN_URGENT,
	   MH.NO_HULL,
	   IH.NM_CONSIGNEE,
	   IH.ADDR1_CONSIGNEE,
	   IH.ADDR2_CONSIGNEE,
	   IH.NM_NOTIFY,
	   IH.ADDR1_NOTIFY,
	   IH.ADDR2_NOTIFY,
	   IH.REMARK1 AS TEL,
	   IH.REMARK2 AS EMAIL,
	   IH.REMARK3 AS PIC,
	   IH.ADDR1_PARTNER,
	   IH.ADDR2_PARTNER,
	   MD2.NM_SYSDEF_E AS ARRIVER_COUNTRY,
	   IH.PORT_LOADING,
	   (CASE WHEN ISNULL(MD2.NM_SYSDEF_E, '') = '' THEN ISNULL(IH.PORT_ARRIVER, '')
			 WHEN ISNULL(IH.PORT_ARRIVER, '') = '' THEN MD2.NM_SYSDEF_E 
												   ELSE (ISNULL(IH.PORT_ARRIVER, '') + ' ' + MD2.NM_SYSDEF_E) END) AS PORT_ARRIVER,
	   IH.DT_SAILING_ON,
	   MD9.NM_SYSDEF AS NM_HS,
	   MD10.NM_SYSDEF_E AS NM_ORIGIN,
	   (MD11.NM_SYSDEF + ' ' + IH.PORT_LOADING) AS NM_TRANS,
	   MD12.NM_SYSDEF AS NM_COND_PAY,
	   QO.NO_IO,
	   QOH.DT_IO,
	   SH.NO_PO_PARTNER,
	   SO.NO_SALES,
	   ME4.NM_ENG,
	   (CASE WHEN QH.CD_COMPANY IS NULL THEN 'Y' ELSE 'N' END) AS YN_AUTO, -- 자동프로세스확인
	   WD.DT_START,
	   WD.DT_BILL,
	   GH.MEMO_CD,
	   GH.CHECK_PEN,
	   WD.DTS_INSERT,
	   WD.DTS_UPDATE,
	   PH.QT_BOX,
	   WD.CD_RMK,
	   MD1.NM_SYSDEF AS NM_RMK,
	   WD.YN_AUTO_SUBMIT
FROM SA_GIRL GL
LEFT JOIN SA_GIRH GH ON GH.CD_COMPANY = GL.CD_COMPANY AND GH.NO_GIR = GL.NO_GIR 
LEFT JOIN CZ_SA_GIRH_WORK_DETAIL WD ON WD.CD_COMPANY = GL.CD_COMPANY AND WD.NO_GIR = GL.NO_GIR
LEFT JOIN CZ_TR_INVH IH ON IH.CD_COMPANY = GL.CD_COMPANY AND IH.NO_INV = GL.NO_INV
LEFT JOIN MM_QTIO QO ON QO.CD_COMPANY = GL.CD_COMPANY AND QO.NO_ISURCV = GL.NO_GIR AND QO.NO_ISURCVLINE = GL.SEQ_GIR
LEFT JOIN MM_QTIOH QOH ON QOH.CD_COMPANY = QO.CD_COMPANY AND QOH.NO_IO = QO.NO_IO
LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = GL.CD_COMPANY AND SL.NO_SO = ISNULL(GL.NO_SO, GL.NO_SO_MGMT) AND SL.SEQ_SO = (CASE WHEN GL.SEQ_SO = 0 THEN GL.NO_SOLINE_MGMT ELSE GL.SEQ_SO END)
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO
LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = SH.CD_COMPANY AND QH.NO_FILE = SH.NO_SO
LEFT JOIN (SELECT CD_COMPANY, NO_SO, NO_SOLINE, ISNULL(SUM(QT_TAX), 0) AS QT_TAX 
		   FROM CZ_FI_IMP_PMTL
		   GROUP BY CD_COMPANY, NO_SO, NO_SOLINE) FT
ON FT.CD_COMPANY = GL.CD_COMPANY AND FT.NO_SO = SL.NO_SO AND FT.NO_SOLINE = SL.SEQ_SO
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = WD.NO_IMO
LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = GL.CD_COMPANY AND SG.CD_SALEGRP = GL.CD_SALEGRP
LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = SG.CD_COMPANY AND SO.CD_SALEORG = SG.CD_SALEORG
LEFT JOIN MA_PLANT PT ON PT.CD_COMPANY = GH.CD_COMPANY AND PT.CD_PLANT = GH.CD_PLANT
LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = GL.CD_COMPANY
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = GH.CD_COMPANY AND MP.CD_PARTNER = GH.CD_PARTNER
LEFT JOIN CZ_MA_DELIVERY MP2 ON MP2.CD_COMPANY = WD.CD_COMPANY AND MP2.CD_PARTNER = WD.CD_DELIVERY_TO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = MP.CD_COMPANY AND ME.NO_EMP = MP.CD_EMP_SALE
LEFT JOIN MA_EMP ME2 ON ME2.CD_COMPANY = GH.CD_COMPANY AND ME2.NO_EMP = GH.NO_EMP
LEFT JOIN MA_EMP ME4 ON ME4.CD_COMPANY = SO.CD_COMPANY AND ME4.NO_EMP = SO.NO_SALES
LEFT JOIN MA_CODEDTL MD ON MD.CD_COMPANY = WD.CD_COMPANY AND MD.CD_FIELD = 'CZ_SA00006' AND MD.CD_SYSDEF = WD.CD_MAIN_CATEGORY
LEFT JOIN MA_CODEDTL MD1 ON MD1.CD_COMPANY = WD.CD_COMPANY AND MD1.CD_FIELD = 'CZ_SA00032' AND MD1.CD_SYSDEF = WD.CD_RMK
LEFT JOIN MA_CODEDTL MD2 ON MD2.CD_COMPANY = IH.CD_COMPANY AND MD2.CD_FIELD = 'MA_B000020' AND MD2.CD_SYSDEF = IH.ARRIVER_COUNTRY
LEFT JOIN MA_CODEDTL MD3 ON MD3.CD_COMPANY = GH.CD_COMPANY AND MD3.CD_FIELD = 'PU_C000027' AND MD3.CD_SYSDEF = GH.YN_RETURN
LEFT JOIN MA_CODEDTL MD4 ON MD4.CD_COMPANY = GH.CD_COMPANY AND MD4.CD_FIELD = 'PU_C000016' AND MD4.CD_SYSDEF = GH.TP_BUSI
LEFT JOIN MA_CODEDTL MD5 ON MD5.CD_COMPANY = MP.CD_COMPANY AND MD5.CD_FIELD = 'MA_B000065' AND MD5.CD_SYSDEF = MP.CD_PARTNER_GRP
LEFT JOIN MA_CODEDTL MD6 ON MD6.CD_COMPANY = WD.CD_COMPANY AND MD6.CD_FIELD = 'CZ_SA00021' AND MD6.CD_SYSDEF = '001'
LEFT JOIN MA_CODEDTL MD7 ON MD7.CD_COMPANY = WD.CD_COMPANY AND MD7.CD_FIELD = MD.CD_FLAG1 AND MD7.CD_SYSDEF = WD.CD_SUB_CATEGORY
LEFT JOIN MA_CODEDTL MD8 ON MD8.CD_COMPANY = GH.CD_COMPANY AND MD8.CD_FIELD = 'CZ_SA00030' AND MD8.CD_SYSDEF = GH.STA_GIR
LEFT JOIN MA_CODEDTL MD9 ON MD9.CD_COMPANY = IH.CD_COMPANY AND MD9.CD_FIELD = 'CZ_SA00018' AND MD9.CD_SYSDEF = IH.CD_PRODUCT
LEFT JOIN MA_CODEDTL MD10 ON MD10.CD_COMPANY = IH.CD_COMPANY AND MD10.CD_FIELD = 'MA_B000020' AND MD10.CD_SYSDEF = IH.CD_ORIGIN
LEFT JOIN MA_CODEDTL MD11 ON MD11.CD_COMPANY = IH.CD_COMPANY AND MD11.CD_FIELD = 'TR_IM00003' AND MD11.CD_SYSDEF = IH.TP_TRANS
LEFT JOIN MA_CODEDTL MD12 ON MD12.CD_COMPANY = IH.CD_COMPANY AND MD12.CD_FIELD = 'CZ_SA00013' AND MD12.CD_SYSDEF = IH.COND_PAY
LEFT JOIN (SELECT CD_COMPANY, NO_GIR, COUNT(NO_PACK) AS QT_BOX 
		   FROM CZ_SA_PACKH
		   GROUP BY CD_COMPANY, NO_GIR) PH
ON PH.CD_COMPANY = GH.CD_COMPANY AND PH.NO_GIR = GH.NO_GIR
UNION ALL
SELECT GL.CD_COMPANY,
	   MC.NM_COMPANY,
	   GL.NO_GIR,
	   SL.NO_SO,
	   GL.RET,
	   GL.CD_SALEGRP,
	   PD.DTS_SUBMIT,
	   PD.DTS_CONFIRM,
	   0 AS QT_TAX,
	   SH.TP_SO,
	   SH.NO_EMP AS NO_SA_EMP,
	   GH.DT_GIR,
	   GH.CD_PARTNER,
	   MP.LN_PARTNER,
	   MP.CD_PARTNER_GRP,
	   MD5.NM_SYSDEF AS NM_PARTNER_GRP,
	   GH.CD_PLANT,
	   PT.NM_PLANT,
	   GH.YN_RETURN,
	   MD3.NM_SYSDEF AS NM_RETURN,
	   MD4.NM_SYSDEF AS NM_BUSI,
	   GH.STA_GIR,
	   MD8.NM_SYSDEF AS NM_STA_GIR,
	   ME.NM_KOR AS NM_EMP_SALE,
	   PD.DC_RMK,
	   PD.DC_RMK1,
	   PD.DC_RMK2,
	   PD.DC_RESULT,
	   '002' AS CD_TYPE,
	   MD6.NM_SYSDEF AS NM_TYPE1,
	   MD6.NM_SYSDEF_E AS NM_TYPE1_E,
	   PD.CD_PACK_CATEGORY AS CD_TYPE2,
	   MD.NM_SYSDEF AS NM_TYPE2,
	   MD.NM_SYSDEF_E AS NM_TYPE2_E,
	   PD.CD_SUB_CATEGORY AS CD_TYPE3,
	   MD7.NM_SYSDEF AS NM_TYPE3,
	   MD7.NM_SYSDEF_E AS NM_TYPE3_E,
	   PD.NO_IMO,
	   MH.NM_VESSEL,
	   PD.DT_COMPLETE,
	   PD.YN_PACKING,
	   GH.NO_EMP,
	   ME2.NM_KOR AS NM_EMP_GIR,
	   PD.DT_PACKING AS DT_DONE,
	   PD.CD_COLLECT_FROM AS CD_GIR_PARTNER,
	   MP2.LN_PARTNER AS NM_GIR_PARTNER,
	   NEOE.FN_CZ_YN_URGENT(GL.CD_COMPANY, 'Y', PD.CD_PACK_CATEGORY, PD.CD_SUB_CATEGORY, PD.DTS_SUBMIT, PD.DT_COMPLETE) AS YN_URGENT,
	   MH.NO_HULL,
	   IH.NM_CONSIGNEE,
	   IH.ADDR1_CONSIGNEE,
	   IH.ADDR2_CONSIGNEE,
	   IH.NM_NOTIFY,
	   IH.ADDR1_NOTIFY,
	   IH.ADDR2_NOTIFY,
	   IH.REMARK1 AS TEL,
	   IH.REMARK2 AS EMAIL,
	   IH.REMARK3 AS PIC,
	   IH.ADDR1_PARTNER,
	   IH.ADDR2_PARTNER,
	   MD2.NM_SYSDEF_E AS ARRIVER_COUNTRY,
	   IH.PORT_LOADING,
	   (CASE WHEN ISNULL(MD2.NM_SYSDEF_E, '') = '' THEN ISNULL(IH.PORT_ARRIVER, '')
			 WHEN ISNULL(IH.PORT_ARRIVER, '') = '' THEN MD2.NM_SYSDEF_E 
												   ELSE (ISNULL(IH.PORT_ARRIVER, '') + ' ' + MD2.NM_SYSDEF_E) END) AS PORT_ARRIVER,
	   IH.DT_SAILING_ON,
	   MD9.NM_SYSDEF AS NM_HS,
	   MD10.NM_SYSDEF_E AS NM_ORIGIN,
	   (MD11.NM_SYSDEF + ' ' + IH.PORT_LOADING) AS NM_TRANS,
	   MD12.NM_SYSDEF AS NM_COND_PAY,
	   '' AS NO_IO,
	   '' AS DT_IO,
	   SH.NO_PO_PARTNER,
	   SO.NO_SALES,
	   ME4.NM_ENG,
	   'N' AS YN_AUTO,
	   PD.DT_START,
	   '' AS DT_BILL,
	   GH.MEMO_CD,
	   GH.CHECK_PEN,
	   PD.DTS_INSERT,
	   PD.DTS_UPDATE,
	   PH.QT_BOX,
	   PD.CD_RMK,
	   MD1.NM_SYSDEF AS NM_RMK,
	   'N' AS YN_AUTO_SUBMIT
FROM CZ_SA_GIRL_PACK GL
LEFT JOIN CZ_SA_GIRH_PACK GH ON GH.CD_COMPANY = GL.CD_COMPANY AND GH.NO_GIR = GL.NO_GIR 
LEFT JOIN CZ_SA_GIRH_PACK_DETAIL PD ON PD.CD_COMPANY = GL.CD_COMPANY AND PD.NO_GIR = GL.NO_GIR
LEFT JOIN CZ_TR_INVH IH ON IH.CD_COMPANY = GL.CD_COMPANY AND IH.NO_INV = GL.NO_INV
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = GL.CD_COMPANY  AND SH.NO_SO = GL.NO_SO
LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = GL.CD_COMPANY AND SL.NO_SO = GL.NO_SO AND SL.SEQ_SO = GL.SEQ_SO
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = PD.NO_IMO
LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = GL.CD_COMPANY AND SG.CD_SALEGRP = GL.CD_SALEGRP
LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = SG.CD_COMPANY AND SO.CD_SALEORG = SG.CD_SALEORG
LEFT JOIN MA_PLANT PT ON PT.CD_COMPANY = GH.CD_COMPANY AND PT.CD_PLANT = GH.CD_PLANT
LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = GL.CD_COMPANY
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = GH.CD_COMPANY AND MP.CD_PARTNER = GH.CD_PARTNER
LEFT JOIN CZ_MA_DELIVERY MP2 ON MP2.CD_COMPANY = PD.CD_COMPANY AND MP2.CD_PARTNER = PD.CD_COLLECT_FROM
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = MP.CD_COMPANY AND ME.NO_EMP = MP.CD_EMP_SALE
LEFT JOIN MA_EMP ME2 ON ME2.CD_COMPANY = GH.CD_COMPANY AND ME2.NO_EMP = GH.NO_EMP
LEFT JOIN MA_EMP ME4 ON ME4.CD_COMPANY = SO.CD_COMPANY AND ME4.NO_EMP = SO.NO_SALES
LEFT JOIN MA_CODEDTL MD ON MD.CD_COMPANY = PD.CD_COMPANY AND MD.CD_FIELD = 'CZ_SA00020' AND MD.CD_SYSDEF = PD.CD_PACK_CATEGORY
LEFT JOIN MA_CODEDTL MD1 ON MD1.CD_COMPANY = PD.CD_COMPANY AND MD1.CD_FIELD = 'CZ_SA00032' AND MD1.CD_SYSDEF = PD.CD_RMK
LEFT JOIN MA_CODEDTL MD2 ON MD2.CD_COMPANY = IH.CD_COMPANY AND MD2.CD_FIELD = 'MA_B000020' AND MD2.CD_SYSDEF = IH.ARRIVER_COUNTRY
LEFT JOIN MA_CODEDTL MD3 ON MD3.CD_COMPANY = GH.CD_COMPANY AND MD3.CD_FIELD = 'PU_C000027' AND MD3.CD_SYSDEF = GH.YN_RETURN
LEFT JOIN MA_CODEDTL MD4 ON MD4.CD_COMPANY = GH.CD_COMPANY AND MD4.CD_FIELD = 'PU_C000016' AND MD4.CD_SYSDEF = GH.TP_BUSI
LEFT JOIN MA_CODEDTL MD5 ON MD5.CD_COMPANY = MP.CD_COMPANY AND MD5.CD_FIELD = 'MA_B000065' AND MD5.CD_SYSDEF = MP.CD_PARTNER_GRP
LEFT JOIN MA_CODEDTL MD6 ON MD6.CD_COMPANY = PD.CD_COMPANY AND MD6.CD_FIELD = 'CZ_SA00021' AND MD6.CD_SYSDEF = '002'
LEFT JOIN MA_CODEDTL MD7 ON MD7.CD_COMPANY = PD.CD_COMPANY AND MD7.CD_FIELD = MD.CD_FLAG1 AND MD7.CD_SYSDEF = PD.CD_SUB_CATEGORY
LEFT JOIN MA_CODEDTL MD8 ON MD8.CD_COMPANY = GH.CD_COMPANY AND MD8.CD_FIELD = 'CZ_SA00030' AND MD8.CD_SYSDEF = GH.STA_GIR
LEFT JOIN MA_CODEDTL MD9 ON MD9.CD_COMPANY = IH.CD_COMPANY AND MD9.CD_FIELD = 'CZ_SA00018' AND MD9.CD_SYSDEF = IH.CD_PRODUCT
LEFT JOIN MA_CODEDTL MD10 ON MD10.CD_COMPANY = IH.CD_COMPANY AND MD10.CD_FIELD = 'MA_B000020' AND MD10.CD_SYSDEF = IH.CD_ORIGIN
LEFT JOIN MA_CODEDTL MD11 ON MD11.CD_COMPANY = IH.CD_COMPANY AND MD11.CD_FIELD = 'TR_IM00003' AND MD11.CD_SYSDEF = IH.TP_TRANS
LEFT JOIN MA_CODEDTL MD12 ON MD12.CD_COMPANY = IH.CD_COMPANY AND MD12.CD_FIELD = 'CZ_SA00013' AND MD12.CD_SYSDEF = IH.COND_PAY
LEFT JOIN (SELECT CD_COMPANY, NO_GIR, COUNT(NO_PACK) AS QT_BOX 
		   FROM CZ_SA_PACKH
		   GROUP BY CD_COMPANY, NO_GIR) PH
ON PH.CD_COMPANY = GH.CD_COMPANY AND PH.NO_GIR = GH.NO_GIR

GO

