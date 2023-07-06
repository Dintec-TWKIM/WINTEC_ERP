USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_PACK_MNGH_S]    Script Date: 2015-09-14 오후 3:58:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_PACK_MNGH_S]
(
	@P_SERVER_KEY			NVARCHAR(50) = '',
	@P_CD_COMPANY			NVARCHAR(100) = '',
	@P_LANGUAGE				NVARCHAR(5) = 'KR',
	@P_DT_PACK_FROM			NVARCHAR(8) = '',
	@P_DT_PACK_TO			NVARCHAR(8) = '',
	@P_NO_GIR				NVARCHAR(20) = '',
	@P_NO_FILE				NVARCHAR(20) = '',
	@P_NO_EMP				NVARCHAR(10)
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
	   STUFF((SELECT DISTINCT ',' + SH.NO_PO_PARTNER 
			  FROM SA_SOH SH
			  WHERE SH.CD_COMPANY = PH.CD_COMPANY
			  AND EXISTS (SELECT 1 
						  FROM CZ_SA_PACKL
						  WHERE CD_COMPANY = PH.CD_COMPANY
						  AND NO_GIR = PH.NO_GIR
						  AND NO_PACK = PH.NO_PACK
						  AND NO_FILE = SH.NO_SO)
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
	   AT.FILE_NAME,
	   PL.NO_FILE,
	   CC.NM_CC,
	   PH.YN_OUTSOURCING,
	   GH.CD_QR_PL,
	   GH.DC_RMK_PL,
	   PL.QT_ITEM
FROM CZ_SA_PACKH PH
JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK,
		     COUNT(1) AS QT_ITEM,
			 MAX(PL.NO_FILE) AS NO_FILE,
			 MAX(SG.CD_CC) AS CD_CC,
		     MAX(SO.NO_SALES) AS NO_SALES,
			 MAX(PT.DC_SIGN) AS DC_SIGN
	  FROM CZ_SA_PACKL PL
	  LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = PL.CD_COMPANY AND SH.NO_SO = PL.NO_FILE
	  LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = SH.CD_COMPANY AND SG.CD_SALEGRP = SH.CD_SALEGRP
	  LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = SG.CD_COMPANY AND SO.CD_SALEORG = SG.CD_SALEORG
	  LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SO.CD_COMPANY AND ME.NO_EMP = SO.NO_SALES
	  LEFT JOIN HR_PHOTO PT ON PT.CD_COMPANY = ME.CD_COMPANY AND PT.NO_EMP = ME.NO_EMP
	  WHERE (ISNULL(@P_NO_FILE, '') = '' OR PL.NO_FILE = @P_NO_FILE)
	  GROUP BY PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK) PL
ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR AND PL.NO_PACK = PH.NO_PACK
LEFT JOIN (SELECT CD_COMPANY, NO_GIR,
				  COUNT(NO_PACK) AS QT_PACK
		   FROM CZ_SA_PACKH
		   GROUP BY CD_COMPANY, NO_GIR) PQ
ON PQ.CD_COMPANY = PH.CD_COMPANY AND PQ.NO_GIR = PH.NO_GIR
LEFT JOIN (SELECT A.CD_COMPANY, A.NO_GIR, A.NO_PACK, A.FILE_NAME
		   FROM CZ_SA_PACKH_ATTACHMENT A
		   JOIN (SELECT CD_COMPANY, NO_GIR, NO_PACK,
		   				MAX(SEQ) AS SEQ 
		   		 FROM CZ_SA_PACKH_ATTACHMENT
		   		 GROUP BY CD_COMPANY, NO_GIR, NO_PACK) B
		   ON B.CD_COMPANY = A.CD_COMPANY AND B.NO_GIR = A.NO_GIR AND B.NO_PACK = A.NO_PACK AND B.SEQ = A.SEQ) AT
ON AT.CD_COMPANY = PH.CD_COMPANY AND AT.NO_GIR = PH.NO_GIR AND AT.NO_PACK = PH.NO_PACK
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
				  GD.DC_RMK_PL
		   FROM SA_GIRH GH
		   LEFT JOIN CZ_SA_GIRH_WORK_DETAIL GD ON GD.CD_COMPANY = GH.CD_COMPANY AND GD.NO_GIR = GH.NO_GIR
		   LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR, 
							 TH.DESCRIPTION AS NO_PO_PARTNER,
							 MC.NM_SYSDEF_E AS NM_ORIGIN,
							 TH.PORT_ARRIVER,
							 TH.ARRIVER_COUNTRY,
							 TH.NO_TO,
							 GL.CD_QR_PL
					  FROM (SELECT CD_COMPANY, NO_GIR, NO_INV,
								   MAX(TXT_USERDEF3) AS CD_QR_PL
						    FROM SA_GIRL
							GROUP BY CD_COMPANY, NO_GIR, NO_INV) GL
					  LEFT JOIN CZ_TR_INVH TH ON TH.CD_COMPANY = GL.CD_COMPANY AND TH.NO_INV = GL.NO_INV
					  LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = GL.CD_COMPANY AND MC.CD_FIELD = 'MA_B000020' AND MC.CD_SYSDEF = TH.CD_ORIGIN) GL
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
				  '' AS DC_RMK_PL
		   FROM CZ_SA_GIRH_PACK PH 
		   LEFT JOIN CZ_SA_GIRH_PACK_DETAIL PD ON PD.CD_COMPANY = PH.CD_COMPANY AND PD.NO_GIR = PH.NO_GIR
		   LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR, 
							 TH.DESCRIPTION AS NO_PO_PARTNER,
							 MC.NM_SYSDEF_E AS NM_ORIGIN,
							 TH.PORT_ARRIVER,
							 TH.ARRIVER_COUNTRY,
							 TH.NO_TO
					  FROM (SELECT CD_COMPANY, NO_GIR, NO_INV 
							FROM CZ_SA_GIRL_PACK
							GROUP BY CD_COMPANY, NO_GIR, NO_INV) PL
					  LEFT JOIN CZ_TR_INVH TH ON TH.CD_COMPANY = PL.CD_COMPANY AND TH.NO_INV = PL.NO_INV
					  LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PL.CD_COMPANY AND MC.CD_FIELD = 'MA_B000020' AND MC.CD_SYSDEF = TH.CD_ORIGIN) PL
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
LEFT JOIN MA_CC CC ON CC.CD_COMPANY = PL.CD_COMPANY AND CC.CD_CC = PL.CD_CC
WHERE PH.CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_COMPANY))
AND PH.DT_PACK BETWEEN @P_DT_PACK_FROM AND @P_DT_PACK_TO
AND (ISNULL(@P_NO_GIR, '') = '' OR PH.NO_GIR = @P_NO_GIR)
AND (ISNULL(@P_NO_EMP, '') = '' OR PH.ID_INSERT = @P_NO_EMP)
ORDER BY PH.CD_COMPANY, PH.NO_GIR, PH.NO_PACK

GO