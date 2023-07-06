ALTER PROCEDURE [NEOE].[SP_CZ_QR_LABEL_GI_S]
(
	@P_MODE			INT, 
	@P_CD_COMPANY	NVARCHAR(7), 
	@P_DT_FROM		NVARCHAR(8),
	@P_DT_TO		NVARCHAR(8),
	@P_NO_FILE		NVARCHAR(20),
	@P_NO_HULL		NVARCHAR(20),
	@P_CD_ITEM		NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
       GL.CD_COMPANY,
	   GL.NO_GIR,
	   GL.SEQ_GIR,
	   GL.NO_SO, 
	   GL.SEQ_SO,
	   LB.NO_SEQ,
	   MP.LN_PARTNER AS NM_BUYER,
	   MH.NM_VESSEL,
	   SH.NO_PO_PARTNER AS NO_REF,
	   QL.NO_DSP,
	   QL.NM_SUBJECT,
	   QL.CD_ITEM_PARTNER,
	   QL.NM_ITEM_PARTNER,
	   GL.CD_ITEM,
	   MI.CD_ZONE,
	   MC.NM_SYSDEF AS NM_UNIT,
	   ISNULL(GL.QT_GIR, 0) AS QT_GIR,
	   ISNULL(GL.QT_GI, 0) AS QT_GI,
	   ISNULL(LB.QT, 0) AS QT_LABEL,
	   ISNULL(GL.QT_GIR, 0) AS QT_WORK,
	   SL.QR,
	   SL.EQ,
	   SL.PART,
	   SL.LOCATION,
	   SL.PART_DESC
FROM (SELECT GL.CD_COMPANY, GL.NO_GIR, GL.SEQ_GIR, GL.NO_SO, GL.SEQ_SO, GL.CD_ITEM, GL.QT_GIR, GL.QT_GI,
			 GH.DT_GIR, GH.CD_PLANT, GH.CD_PARTNER, GD.NO_IMO 
	  FROM SA_GIRL GL
	  LEFT JOIN SA_GIRH GH ON GH.CD_COMPANY = GL.CD_COMPANY AND GH.NO_GIR = GL.NO_GIR
	  LEFT JOIN CZ_SA_GIRH_WORK_DETAIL GD ON GD.CD_COMPANY = GH.CD_COMPANY AND GD.NO_GIR = GH.NO_GIR
	  WHERE GL.CD_ITEM NOT LIKE 'SD%'
	  UNION ALL
	  SELECT GL.CD_COMPANY, GL.NO_GIR, GL.SEQ_GIR, GL.NO_SO, GL.SEQ_SO, GL.CD_ITEM, GL.QT_GIR, GL.QT_GI,
			 GH.DT_GIR, GH.CD_PLANT, GH.CD_PARTNER, WD.NO_IMO 
	  FROM CZ_SA_GIRL_PACK GL
	  LEFT JOIN CZ_SA_GIRH_PACK GH ON GH.CD_COMPANY = GL.CD_COMPANY AND GH.NO_GIR = GL.NO_GIR
	  LEFT JOIN CZ_SA_GIRH_PACK_DETAIL WD ON WD.CD_COMPANY = GH.CD_COMPANY AND WD.NO_GIR = GH.NO_GIR
	  WHERE GL.CD_ITEM NOT LIKE 'SD%') GL
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = GL.CD_COMPANY AND SH.NO_SO = GL.NO_SO
LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_FILE = GL.NO_SO AND QL.NO_LINE = GL.SEQ_SO
LEFT JOIN CZ_QR_LABELL LB ON LB.CD_COMPANY = GL.CD_COMPANY AND LB.NO_KEY = GL.NO_GIR AND LB.NO_LINE = GL.SEQ_GIR
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = GL.CD_COMPANY AND MI.CD_PLANT = GL.CD_PLANT AND MI.CD_ITEM = GL.CD_ITEM
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = QL.CD_COMPANY AND MC.CD_FIELD = 'MA_B000004' AND MC.CD_SYSDEF = QL.UNIT
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = GL.CD_COMPANY AND MP.CD_PARTNER = GL.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = GL.NO_IMO
LEFT JOIN CZ_SA_SOL_LABEL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
WHERE GL.CD_COMPANY = @P_CD_COMPANY
AND GL.DT_GIR BETWEEN @P_DT_FROM AND @P_DT_TO
AND (ISNULL(@P_NO_FILE, '') = '' OR GL.NO_SO = @P_NO_FILE)
AND (ISNULL(@P_NO_HULL, '') = '' OR MH.NO_HULL = @P_NO_HULL)
AND (ISNULL(@P_CD_ITEM, '') = '' OR GL.CD_ITEM = @P_CD_ITEM)
AND ((@P_MODE = 0 AND ISNULL(LB.QT, 0) = 0) OR (@P_MODE <> 0 AND ISNULL(LB.QT, 0) > 0))
OPTION(RECOMPILE)