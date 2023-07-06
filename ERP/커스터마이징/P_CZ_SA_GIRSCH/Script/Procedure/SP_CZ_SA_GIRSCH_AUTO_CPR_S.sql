USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIRSCH_AUTO_CPR_S]    Script Date: 2017-01-19 오후 4:50:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIRSCH_AUTO_CPR_S]    
(    
    @P_CD_COMPANY       NVARCHAR(7),
	@P_NO_GIR			NVARCHAR(20) = NULL
)    
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT GH.CD_COMPANY,
	   MC.NM_COMPANY,
	   GH.NO_GIR,
	   REPLACE(MH.NM_VESSEL, '/', '') AS NM_VESSEL,
	   GH.CD_PARTNER,
	   MP.LN_PARTNER,
	   WD.CD_DELIVERY_TO,
	   WD.DC_RESULT,
	   ME.NM_KOR,
	   ME.NO_EMAIL,
	   ME.NO_TEL,
	   MD.LN_PARTNER AS NM_DELIVERY,
	   ISNULL(NULLIF(WD.NO_DELIVERY_EMAIL, ''), MD.NO_EMAIL) AS NO_EMAIL_DELIVERY,
	   WD.CD_MAIN_CATEGORY,
	   WD.CD_SUB_CATEGORY,
	   CD1.NM_SYSDEF AS NM_SUB_CATEGORY,
	   CD2.NM_SYSDEF AS NM_RMK,
	   WD.DC_RMK_CPR,
	   WD.DT_COMPLETE,
	   (SELECT MAX(OL.NO_IO) AS NO_IO 
	    FROM MM_QTIO OL
		WHERE OL.CD_COMPANY = GH.CD_COMPANY
		AND OL.NO_ISURCV = GH.NO_GIR) AS NO_IO,
	   (SELECT REPLACE(STRING_AGG(A.NO_PO_PARTNER, ','), '/', '') AS NO_PO_PARTNER
		FROM (SELECT GL.CD_COMPANY, GL.NO_GIR, SL.NO_PO_PARTNER
		      FROM SA_GIRL GL
		      LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = GL.CD_COMPANY AND SL.NO_SO = GL.NO_SO AND SL.SEQ_SO = GL.SEQ_SO
		      WHERE GL.CD_COMPANY = GH.CD_COMPANY
		      AND GL.NO_GIR = GH.NO_GIR
		      GROUP BY GL.CD_COMPANY, GL.NO_GIR, SL.NO_PO_PARTNER) A) AS NO_PO_PARTNER,
	   (SELECT DC_VESSEL 
		FROM CZ_SA_GIRH_REMARK GR
		WHERE GR.CD_COMPANY = GH.CD_COMPANY
		AND GR.NO_GIR = GH.NO_GIR) AS DC_VESSEL
FROM SA_GIRH GH
JOIN CZ_SA_GIRH_WORK_DETAIL WD ON WD.CD_COMPANY = GH.CD_COMPANY AND WD.NO_GIR = GH.NO_GIR
LEFT JOIN CZ_MA_DELIVERY MD ON MD.CD_COMPANY = WD.CD_COMPANY AND MD.CD_PARTNER = WD.CD_DELIVERY_TO
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = WD.NO_IMO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = GH.CD_COMPANY AND MP.CD_PARTNER = GH.CD_PARTNER
LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR,
		          MAX(VE.CD_FLAG1) AS NO_EMP_LOG  
		   FROM SA_GIRL GL
		   JOIN SA_SOH SH ON SH.CD_COMPANY = GL.CD_COMPANY AND SH.NO_SO = GL.NO_SO
		   LEFT JOIN V_CZ_SA_QTN_LOG_EMP VE ON VE.CD_COMPANY = SH.CD_COMPANY AND VE.NO_FILE = SH.NO_SO
		   WHERE VE.CD_FLAG1 IS NOT NULL 
		   GROUP BY GL.CD_COMPANY, GL.NO_GIR) GL
ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = GH.CD_COMPANY AND ME.NO_EMP = (CASE WHEN GH.NO_EMP = 'SYSADMIN' THEN GL.NO_EMP_LOG ELSE GH.NO_EMP END)
LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = GH.CD_COMPANY
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = GH.CD_COMPANY AND CD.CD_FIELD = 'CZ_SA00006' AND CD.CD_SYSDEF = WD.CD_MAIN_CATEGORY
LEFT JOIN MA_CODEDTL CD1 ON CD1.CD_COMPANY = GH.CD_COMPANY AND CD1.CD_FIELD = CD.CD_FLAG1 AND CD1.CD_SYSDEF = WD.CD_SUB_CATEGORY
LEFT JOIN MA_CODEDTL CD2 ON CD2.CD_COMPANY = GH.CD_COMPANY AND CD2.CD_FIELD = 'CZ_SA00032' AND CD2.CD_SYSDEF = WD.CD_RMK
WHERE GH.CD_COMPANY = @P_CD_COMPANY
AND (ISNULL(@P_NO_GIR, '') = '' OR GH.NO_GIR = @P_NO_GIR) 
AND ((WD.CD_MAIN_CATEGORY = '002' AND WD.CD_SUB_CATEGORY IN ('DIR', 'ADV')) OR
     (WD.CD_MAIN_CATEGORY = '003' AND WD.CD_SUB_CATEGORY IN ('001', '002', '004', '005', '007', '010', '011')) OR
	 (GH.CD_COMPANY = 'K100' AND WD.CD_MAIN_CATEGORY = '001' AND GH.CD_PARTNER IN ('01187', '02143', '03448') AND EXISTS (SELECT 1 
																														  FROM CZ_SA_GIRH_REMARK GR
																														  WHERE GR.CD_COMPANY = GH.CD_COMPANY
																														  AND GR.NO_GIR = GH.NO_GIR
																														  AND ISNULL(GR.DC_VESSEL, '') <> '' AND EXISTS (SELECT 1 
																														                                                 FROM MA_FILEINFO MF 
																														                                                 WHERE MF.CD_COMPANY = GH.CD_COMPANY 
																														                                                 AND MF.CD_MODULE = 'SA' 
																														                                                 AND MF.ID_MENU = 'P_CZ_SA_GIR' 
																														                                                 AND MF.CD_FILE = GH.NO_GIR))))
AND GH.STA_GIR = 'C'
AND (ISNULL(@P_NO_GIR, '') <> '' OR ISNULL(WD.YN_CPR, 'N') = 'N')
AND ISNULL(WD.YN_EXCLUDE_CPR, 'N') = 'N'
AND WD.DT_COMPLETE >= '20230328'
AND (NOT (MD.CD_PARTNER IN ('DLV230200006', 'DLV230300396') OR -- 엠엔씨인터내쇼날(택배), 엠엔씨인터내쇼날(픽업)
		  MD.CD_PARTNER IN ('DLV230200250', 'DLV230200272') OR -- 후지글로벌 (전달), 후지글로벌 (픽업)
		  MD.CD_PARTNER = 'DLV230300399') OR -- 현대종합상사 (전달)
	 WD.DT_COMPLETE <= CONVERT(CHAR(8), GETDATE(), 112))
AND ISNULL(NULLIF(WD.NO_DELIVERY_EMAIL, ''), MD.NO_EMAIL) <> ''
AND NOT EXISTS (SELECT 1
				FROM (SELECT GL.CD_COMPANY, GL.NO_GIR,
					  	     SUM(GL.QT_GIR) AS QT_GIR
					  FROM SA_GIRL GL
					  WHERE GL.CD_COMPANY = GH.CD_COMPANY 
					  AND GL.NO_GIR = GH.NO_GIR
					  AND GL.CD_ITEM NOT LIKE 'SD%'
					  GROUP BY GL.CD_COMPANY, GL.NO_GIR) GL
				LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR,
								  SUM(PL.QT_PACK) AS QT_PACK
						   FROM CZ_SA_PACKL PL
						   WHERE PL.CD_ITEM NOT LIKE 'SD%'
						   GROUP BY PL.CD_COMPANY, PL.NO_GIR) PL
				ON PL.CD_COMPANY = GL.CD_COMPANY AND PL.NO_GIR = GL.NO_GIR
				WHERE GL.QT_GIR <> PL.QT_PACK)

GO
