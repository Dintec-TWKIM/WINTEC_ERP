USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIRSCH_PACK_S]    Script Date: 2017-07-07 오후 2:23:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIRSCH_PACK_S]  
(
	@P_SERVER_KEY			NVARCHAR(50) = '',
	@P_CD_COMPANY			NVARCHAR(7),
	@P_NO_GIR				NVARCHAR(MAX),
	@P_NO_PACK              NUMERIC(5, 0) = 0
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE	@V_SERVER_PATH NVARCHAR(MAX)

SELECT @V_SERVER_PATH = ASSEMBLY_HOST_SERVER
FROM CM_SERVER_CONFIG
WHERE SERVER_KEY = @P_SERVER_KEY

SELECT 'N' AS S,
	   PH.CD_COMPANY,
	   CP.NM_COMPANY,
	   PH.NO_GIR,
	   PH.NO_PACK,
	   (PH.NO_GIR + '_' + CONVERT(NVARCHAR(5), PH.NO_PACK)) AS NO_KEY,
	   PH.NM_PACK,
	   PH.DT_PACK,
	   PH.CD_TYPE,
	   MC.NM_SYSDEF AS NM_TYPE,
	   PH.CD_SIZE,
	   MC2.NM_SYSDEF AS NM_SIZE,
	   PH.QT_NET_WEIGHT,
	   PH.QT_GROSS_WEIGHT,
	   CONVERT(INT, PH.QT_WIDTH) AS QT_WIDTH,
	   CONVERT(INT, PH.QT_LENGTH) AS QT_LENGTH,
	   CONVERT(INT, PH.QT_HEIGHT) AS QT_HEIGHT,
	   PH.DC_RMK,
	   GH.DT_GIR,
	   GH.CD_PARTNER,
	   MP.LN_PARTNER AS NM_PARTNER,
	   GH.NO_IMO,
	   MH.NO_HULL,
	   MH.NM_VESSEL,
	   GH.NO_IO,
	   GH.DT_IO,
	   STUFF((SELECT DISTINCT ',' + B.NO_PO_PARTNER
			  FROM CZ_SA_PACKL A
			  JOIN SA_SOH B ON B.CD_COMPANY = A.CD_COMPANY AND B.NO_SO = A.NO_FILE
			  WHERE A.CD_COMPANY = PH.CD_COMPANY
			  AND A.NO_GIR = PH.NO_GIR
			  AND A.NO_PACK = PH.NO_PACK
			  FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 1, '') AS NO_PO_PARTNER,
	   GH.NM_ORIGIN,
	   (CASE WHEN ISNULL(MC1.NM_SYSDEF_E, '') = '' THEN ISNULL(GH.PORT_ARRIVER, '')
			 WHEN ISNULL(GH.PORT_ARRIVER, '') = '' THEN MC1.NM_SYSDEF_E
												   ELSE (ISNULL(GH.PORT_ARRIVER, '') + ' ' + ISNULL(MC3.NM_SYSDEF, MC1.NM_SYSDEF_E)) END) AS PORT_ARRIVER,
	   ISNULL(PQ.QT_PACK, 0) AS QT_PACK,
	   ME.NM_ENG,
	   (CASE WHEN ISNULL(PL.DC_SIGN, '') = '' THEN '' ELSE ISNULL(@V_SERVER_PATH, '') + '/shared/image/human/sign/' + PL.CD_COMPANY + '/' + PL.DC_SIGN END) AS PATH_SIGN,
	   MU.NM_USER AS NM_INSERT,
	   MU1.NM_USER AS NM_UPDATE,
	   PH.DTS_INSERT,
	   PH.DTS_UPDATE,
	   PH.CD_LOCATION,
	   GH.CD_QR_PL,
	   GH.DC_RMK_PL,
	   (CASE WHEN ISNULL(GH.NM_SUB_CATEGORY, '') = '' THEN '' 
													  ELSE ISNULL(GH.NM_SUB_CATEGORY, '') + ' ' + 
														   (CASE WHEN ISNULL(GH.NM_TAEGBAE, '') = '' THEN '' ELSE ISNULL(GH.NM_TAEGBAE, '') + ' ' END) +
														   (CASE WHEN ISNULL(GH.DC_RMK_PL, '') = '' THEN ISNULL(GH.NM_DELIVERY, '') ELSE REPLACE(REPLACE(GH.DC_RMK_PL, CHAR(10), ' '), CHAR(13), ' ') END) END) AS DC_RMK_PACKING,
	   (CASE WHEN ISNULL(GH.NM_SUB_CATEGORY, '') = '' THEN '' 
													  ELSE LEFT(ISNULL(GH.NM_DELIVERY, ''), 15) + ' ' +
														   (CASE WHEN ISNULL(GH.NM_TAEGBAE, '') = '' THEN '' ELSE ISNULL(GH.NM_TAEGBAE, '') + ' ' END) + 
														   (CASE WHEN ISNULL(GH.NM_SHIP_LOCATION, '') = '' THEN '' ELSE ISNULL(GH.NM_SHIP_LOCATION, '') + ' ' END) +
														   (CASE WHEN ISNULL(GH.DC_VESSEL, '') = '' THEN '' ELSE ISNULL(GH.DC_VESSEL, '') + ' 타배 ' END) +
														   (CASE WHEN ISNULL(GH.NM_PORT, '') = '' THEN '' ELSE ISNULL(GH.NM_PORT, '') + ' ' END) +
														   TRIM(REPLACE(REPLACE(ISNULL(GH.NM_SUB_CATEGORY, ''), '물류부', ''), '포워더', '')) END) AS DC_RMK_SHIPPING,
	   ((CASE WHEN ISNULL(GH.DT_COMPLETE, '') = '' THEN '' ELSE CONVERT(CHAR(10), CONVERT(DATETIME, GH.DT_COMPLETE), 23) END) + ' (' + CONVERT(NVARCHAR, CONVERT(INT, PL.QT_PACK)) + ')') AS DC_RMK_PACKING1,
	   CONVERT(CHAR(10), CONVERT(DATETIME, GH.DT_COMPLETE), 23) AS DT_COMPLETE
FROM CZ_SA_PACKH PH
LEFT JOIN (SELECT CD_COMPANY, NO_GIR,
				  COUNT(NO_PACK) AS QT_PACK
		   FROM CZ_SA_PACKH
		   GROUP BY CD_COMPANY, NO_GIR) PQ
ON PQ.CD_COMPANY = PH.CD_COMPANY AND PQ.NO_GIR = PH.NO_GIR
JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK, 
		     MAX(SO.NO_SALES) AS NO_SALES,
			 MAX(PT.DC_SIGN) AS DC_SIGN,
			 COUNT(1) AS QT_PACK
	  FROM CZ_SA_PACKL PL
	  LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = PL.CD_COMPANY AND SH.NO_SO = PL.NO_FILE
	  LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = SH.CD_COMPANY AND SG.CD_SALEGRP = SH.CD_SALEGRP
	  LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = SG.CD_COMPANY AND SO.CD_SALEORG = SG.CD_SALEORG
	  LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SO.CD_COMPANY AND ME.NO_EMP = SO.NO_SALES
	  LEFT JOIN HR_PHOTO PT ON PT.CD_COMPANY = ME.CD_COMPANY AND PT.NO_EMP = ME.NO_EMP
	  GROUP BY PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK) PL
ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR AND PL.NO_PACK = PH.NO_PACK
LEFT JOIN (SELECT GH.CD_COMPANY,
				  GH.NO_GIR,
				  GH.DT_GIR,
				  GH.CD_PARTNER,
				  GD.NO_IMO,
				  IL.NO_IO,
				  IL.DT_IO,
				  GL.NO_PO_PARTNER,
				  GL.NM_ORIGIN,
				  GL.PORT_ARRIVER,
				  GL.ARRIVER_COUNTRY,
				  GL.NO_TO,
				  ISNULL(GL.CD_QR_PL, 'PL') AS CD_QR_PL,
				  GD.DC_RMK_PL,
				  MC.NM_SYSDEF AS NM_MAIN_CATEGORY,
				  (CASE WHEN (GD.CD_MAIN_CATEGORY = '001' OR
							  (GD.CD_MAIN_CATEGORY = '002' AND GD.CD_SUB_CATEGORY = 'DIR') OR
							  (GD.CD_MAIN_CATEGORY = '003' AND GD.CD_SUB_CATEGORY IN ('001', '002', '003', '004', '005', '010'))) THEN MC1.NM_SYSDEF ELSE '' END) AS NM_SUB_CATEGORY,
				  MD.LN_PARTNER AS NM_DELIVERY,
				  GD.DT_COMPLETE,
				  MC2.NM_SYSDEF AS NM_TAEGBAE,
				  MC3.NM_SYSDEF AS NM_SHIP_LOCATION,
				  GR.DC_VESSEL,
				  MC4.NM_SYSDEF AS NM_PORT
		   FROM SA_GIRH GH
		   LEFT JOIN CZ_SA_GIRH_WORK_DETAIL GD ON GD.CD_COMPANY = GH.CD_COMPANY AND GD.NO_GIR = GH.NO_GIR
		   LEFT JOIN CZ_SA_GIRH_REMARK GR ON GR.CD_COMPANY = GH.CD_COMPANY AND GR.NO_GIR = GH.NO_GIR
		   LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = GD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00006' AND MC.CD_SYSDEF = GD.CD_MAIN_CATEGORY
		   LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = GD.CD_COMPANY AND MC1.CD_FIELD = MC.CD_FLAG1 AND MC1.CD_SYSDEF = GD.CD_SUB_CATEGORY
		   LEFT JOIN CZ_MA_CODEDTL MC2 ON MC2.CD_COMPANY = GD.CD_COMPANY AND MC2.CD_FIELD = 'CZ_SA00067' AND MC2.CD_SYSDEF = GD.CD_TAEGBAE
		   LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = GR.CD_COMPANY AND MC3.CD_FIELD = 'CZ_SA00043' AND MC3.CD_SYSDEF = GR.CD_SHIP_LOCATION
		   LEFT JOIN MA_CODEDTL MC4 ON MC4.CD_COMPANY = GR.CD_COMPANY AND MC4.CD_FIELD = 'CZ_SA00070' AND MC4.CD_SYSDEF = GR.CD_PORT
		   LEFT JOIN CZ_MA_DELIVERY MD ON MD.CD_COMPANY = GD.CD_COMPANY AND MD.CD_PARTNER = GD.CD_DELIVERY_TO
		   LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR, 
							 MAX(TH.DESCRIPTION) AS NO_PO_PARTNER,
							 (SELECT NM_SYSDEF_E
							  FROM MA_CODEDTL
							  WHERE CD_COMPANY = GL.CD_COMPANY
							  AND CD_FIELD = 'MA_B000020'
							  AND CD_SYSDEF = MAX(TH.CD_ORIGIN)) AS NM_ORIGIN,
							 MAX(TH.PORT_ARRIVER) AS PORT_ARRIVER,
							 MAX(TH.ARRIVER_COUNTRY) AS ARRIVER_COUNTRY,
							 MAX(TH.NO_TO) AS NO_TO,
							 MAX(GL.TXT_USERDEF3) AS CD_QR_PL
					  FROM SA_GIRL GL
					  LEFT JOIN CZ_TR_INVH TH ON TH.CD_COMPANY = GL.CD_COMPANY AND TH.NO_INV = GL.NO_INV
					  GROUP BY GL.CD_COMPANY, GL.NO_GIR) GL
		   ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
		   LEFT JOIN (SELECT IL.CD_COMPANY, IL.NO_ISURCV, IL.NO_IO, IL.DT_IO 
					  FROM MM_QTIO IL
					  WHERE IL.FG_PS = '2'
					  GROUP BY IL.CD_COMPANY, IL.NO_ISURCV, IL.NO_IO, IL.DT_IO) IL
		   ON IL.CD_COMPANY = GH.CD_COMPANY AND IL.NO_ISURCV = GH.NO_GIR
		   UNION ALL
		   SELECT PH.CD_COMPANY,
				  PH.NO_GIR,
		   		  PH.DT_GIR,
				  PH.CD_PARTNER,
				  PD.NO_IMO,
				  '' AS NO_IO,
				  '' AS DT_IO,
				  PL.NO_PO_PARTNER,
				  PL.NM_ORIGIN,
				  PL.PORT_ARRIVER,
				  PL.ARRIVER_COUNTRY,
				  PL.NO_TO,
				  'PACK' AS CD_QR_PL,
				  '' AS DC_RMK_PL,
				  MC.NM_SYSDEF AS NM_MAIN_CATEGORY,
				  (CASE WHEN PD.CD_SUB_CATEGORY = '007' THEN MC1.NM_SYSDEF ELSE '' END) AS NM_SUB_CATEGORY,
				  MD.LN_PARTNER AS NM_DELIVERY,
				  PD.DT_COMPLETE,
				  NULL AS NM_TAEGBAE,
				  NULL AS NM_SHIP_LOCATION,
				  NULL AS DC_VESSEL,
				  NULL AS NM_PORT
		   FROM CZ_SA_GIRH_PACK PH 
		   LEFT JOIN CZ_SA_GIRH_PACK_DETAIL PD ON PD.CD_COMPANY = PH.CD_COMPANY AND PD.NO_GIR = PH.NO_GIR
		   LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00020' AND MC.CD_SYSDEF = PD.CD_PACK_CATEGORY
		   LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = PD.CD_COMPANY AND MC1.CD_FIELD = MC.CD_FLAG1 AND MC1.CD_SYSDEF = PD.CD_SUB_CATEGORY
		   LEFT JOIN CZ_MA_DELIVERY MD ON MD.CD_COMPANY = PD.CD_COMPANY AND MD.CD_PARTNER = PD.CD_COLLECT_FROM
		   LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR, 
							 MAX(TH.DESCRIPTION) AS NO_PO_PARTNER,
							 (SELECT NM_SYSDEF_E
							  FROM MA_CODEDTL
							  WHERE CD_COMPANY = PL.CD_COMPANY
							  AND CD_FIELD = 'MA_B000020'
							  AND CD_SYSDEF = MAX(TH.CD_ORIGIN)) AS NM_ORIGIN,
							 MAX(TH.PORT_ARRIVER) AS PORT_ARRIVER,
							 MAX(TH.ARRIVER_COUNTRY) AS ARRIVER_COUNTRY,
							 MAX(TH.NO_TO) AS NO_TO
					  FROM CZ_SA_GIRL_PACK PL
					  LEFT JOIN CZ_TR_INVH TH ON TH.CD_COMPANY = PL.CD_COMPANY AND TH.NO_INV = PL.NO_INV
					  GROUP BY PL.CD_COMPANY, PL.NO_GIR) PL
		   ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR) GH
ON GH.CD_COMPANY = PH.CD_COMPANY AND GH.NO_GIR = PH.NO_GIR
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = GH.CD_COMPANY AND MP.CD_PARTNER = GH.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = GH.NO_IMO
LEFT JOIN MA_COMPANY CP ON CP.CD_COMPANY = PH.CD_COMPANY
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = PL.CD_COMPANY AND ME.NO_EMP = PL.NO_SALES
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = PH.CD_COMPANY AND MU.ID_USER = PH.ID_INSERT
LEFT JOIN MA_USER MU1 ON MU1.CD_COMPANY = PH.CD_COMPANY AND MU1.ID_USER = PH.ID_UPDATE
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PH.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00026' AND MC.CD_SYSDEF = PH.CD_TYPE
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = GH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000020' AND MC1.CD_SYSDEF = GH.ARRIVER_COUNTRY
LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = PH.CD_COMPANY AND MC2.CD_FIELD = MC.CD_FLAG1 AND MC2.CD_SYSDEF = PH.CD_SIZE
LEFT JOIN CZ_MA_CODEDTL MC3 ON MC3.CD_COMPANY = GH.CD_COMPANY AND MC3.CD_FIELD = 'CZ_MA00004' AND MC3.CD_SYSDEF = GH.NO_TO
WHERE PH.CD_COMPANY = @P_CD_COMPANY
AND PH.NO_GIR IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_NO_GIR))
AND (ISNULL(@P_NO_PACK, 0) = 0 OR PH.NO_PACK = @P_NO_PACK)
ORDER BY PH.CD_COMPANY, PH.NO_GIR, PH.NO_PACK

SELECT PL.CD_COMPANY,
	   PL.NO_GIR,
	   PL.NO_PACK,
	   (PL.NO_GIR + '_' + CONVERT(NVARCHAR(5), PL.NO_PACK)) AS NO_KEY,
	   PL.SEQ_GIR,
	   PL.NO_FILE,
	   SL.NO_PO_PARTNER,
	   QL.NO_LINE AS NO_QTLINE,
	   QL.NO_DSP,
	   PL.CD_ITEM,
	   MP.NM_ITEM,
	   QL.CD_ITEM_PARTNER,
	   QL.NM_ITEM_PARTNER,
	   QL.YN_DSP_RMK,
	   QL.DC_RMK,
	   PL.QT_PACK,
	   QL.NM_SUBJECT,
	   SL.UNIT_IM,
	   SL.UM_EX_Q,
	   (ISNULL(SL.UM_EX_Q, 0) * ISNULL(PL.QT_PACK, 0)) AS AM_EX_Q,
	   SL.RT_DC,
	   -((ISNULL(SL.UM_EX_Q, 0) - ISNULL(SL.UM_EX_S, 0)) * ISNULL(PL.QT_PACK, 0)) AS AM_DC,
	   (ISNULL(SL.UM_EX_S, 0) * ISNULL(PL.QT_PACK, 0)) AS AM_GIR,
	   SH.CD_EXCH,
	   CD.NM_SYSDEF AS NM_EXCH
FROM CZ_SA_PACKL PL
LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = PL.CD_COMPANY AND QL.NO_FILE = PL.NO_FILE AND QL.NO_LINE = PL.NO_QTLINE
LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = PL.CD_COMPANY AND SL.NO_SO = PL.NO_FILE AND SL.SEQ_SO = PL.NO_QTLINE
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO
LEFT JOIN MA_PITEM MP ON MP.CD_COMPANY = PL.CD_COMPANY AND MP.CD_ITEM = PL.CD_ITEM
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = SH.CD_COMPANY AND CD.CD_FIELD = 'MA_B000005' AND CD.CD_SYSDEF = SH.CD_EXCH
WHERE PL.CD_COMPANY = @P_CD_COMPANY
AND PL.NO_GIR IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_NO_GIR))
AND (ISNULL(@P_NO_PACK, 0) = 0 OR PL.NO_PACK = @P_NO_PACK)
ORDER BY PL.NO_GIR, PL.NO_PACK, PL.NO_FILE, QL.NO_DSP


GO

