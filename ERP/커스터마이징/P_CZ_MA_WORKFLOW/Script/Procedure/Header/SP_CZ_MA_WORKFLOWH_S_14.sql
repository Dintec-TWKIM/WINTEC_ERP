USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOWH_S_14]    Script Date: 2018-08-13 오전 9:47:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- 협조전작성
ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOWH_S_14]  
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_ID_USER				NVARCHAR(15),
	@P_NO_KEY				NVARCHAR(20),
	@P_CD_SALEORG			NVARCHAR(7),		
	@P_CD_SALEGRP			NVARCHAR(500)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT SH.CD_COMPANY,
	   SH.NO_SO AS NO_KEY,
	   SH.NO_PO_PARTNER,
	   WH.ID_SALES,
	   (SELECT MU.NM_USER 
	    FROM MA_USER MU 
		WHERE MU.CD_COMPANY = WH.CD_COMPANY 
		AND MU.ID_USER = WH.ID_SALES) AS NM_SALES,
	   (SELECT MU.NM_USER 
	    FROM MA_USER MU 
		WHERE MU.CD_COMPANY = WH.CD_COMPANY 
		AND MU.ID_USER = WH.ID_LOG) AS NM_LOG,
	   MP.LN_PARTNER,
	   MH.NM_VESSEL,
	   (SELECT SUM(IH.WEIGHT)
		FROM MM_QTIOH IH
		WHERE IH.CD_COMPANY = SH.CD_COMPANY 
		AND (IH.YN_RETURN = 'N' OR IH.YN_RETURN IS NULL)
		AND IH.WEIGHT > 0
		AND EXISTS(SELECT 1 
				   FROM PU_POL PL
				   JOIN MM_QTIO IL ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
				   WHERE IL.CD_COMPANY = IH.CD_COMPANY
				   AND IL.NO_IO = IH.NO_IO
				   AND PL.NO_SO = SH.NO_SO)) AS WEIGHT,
	   SL.QT_SO AS QT_SO,
	   SL.QT_STOCK AS QT_STOCK,
	   SL.QT_BOOK AS QT_BOOK,
	   SL.QT_PO AS QT_PO,
	   SL.QT_IN AS QT_IN,
	   SL.QT_GIR AS QT_GIR,
	   SL.QT_OUT AS QT_OUT,
	   SH.DT_SO,
	   SL.DT_PO,
	   SL.DT_IN,
	   SL.DT_OUT,
	   SH.DC_RMK_TEXT,
	   SH.DC_RMK_TEXT2
FROM SA_SOH SH
JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
	  		 SUM(SL.QT_SO) AS QT_SO,
	  		 SUM(SB.QT_STOCK) AS QT_STOCK,
	  		 SUM(SB.QT_BOOK) AS QT_BOOK,
	  		 SUM(ISNULL(SL.QT_PO, 0) + ISNULL(SB.QT_STOCK, 0)) AS QT_PO,
	  		 SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM, 0) 
	  		 							   ELSE (ISNULL(QI.QT_IO, 0) + ISNULL(SB.QT_BOOK, 0)) END) AS QT_IN,
	  		 SUM(SL.QT_GIR) AS QT_GIR,
	  		 SUM(SL.QT_GI) AS QT_OUT,
	  		 MAX(QI.DT_PO) AS DT_PO,
	  		 MAX(QI.DT_IO) AS DT_IN,
	  		 MAX(QO.DT_IO) AS DT_OUT
	  FROM SA_SOL SL
	  JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
	  LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
	  LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
	  					MAX(PH.DT_PO) AS DT_PO,
	  					MAX(OH.DT_IO) AS DT_IO,
	  	  	   			SUM(OL.QT_IO) AS QT_IO
	  	  		 FROM PU_POH PH
	  			 LEFT JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
	  	  		 LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = PL.CD_COMPANY AND OL.NO_PSO_MGMT = PL.NO_PO AND OL.NO_PSOLINE_MGMT = PL.NO_LINE
	  	  		 LEFT JOIN MM_QTIOH OH ON OH.CD_COMPANY = OL.CD_COMPANY AND OH.NO_IO = OL.NO_IO
	  			 WHERE (OH.YN_RETURN IS NULL OR OH.YN_RETURN = 'N')
	  	  		 GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) QI 
	  ON QI.CD_COMPANY = SL.CD_COMPANY AND QI.NO_SO = SL.NO_SO AND QI.NO_SOLINE = SL.SEQ_SO
	  LEFT JOIN (SELECT SL.CD_COMPANY, SL.NO_SO, SL.SEQ_SO,
				 	    MAX(IH.DT_IO) AS DT_IO,
				 	    SUM(IL.QT_IO) AS QT_BOM 
				 FROM SA_SOL SL
				 JOIN MM_QTIO IL ON IL.CD_COMPANY = SL.CD_COMPANY AND IL.NO_PSO_MGMT = SL.NO_SO AND IL.NO_PSOLINE_MGMT = SL.SEQ_SO
				 JOIN MM_QTIOH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO
				 WHERE IL.FG_PS = '1'
				 AND IL.FG_IO = '002'
				 AND (IH.YN_RETURN IS NULL OR IH.YN_RETURN = 'N')
				 GROUP BY SL.CD_COMPANY, SL.NO_SO, SL.SEQ_SO) QO 
	  ON QO.CD_COMPANY = SL.CD_COMPANY AND QO.NO_SO = SL.NO_SO AND QO.SEQ_SO = SL.SEQ_SO
	  WHERE SL.CD_ITEM NOT LIKE 'SD%'
	  AND (ISNULL(@P_CD_COMPANY, '') = '' OR SL.CD_COMPANY = @P_CD_COMPANY)
      AND (ISNULL(@P_NO_KEY, '') = '' OR SL.NO_SO = @P_NO_KEY)
	  GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN CZ_MA_WORKFLOWH WH ON WH.CD_COMPANY = SH.CD_COMPANY AND WH.NO_KEY = SH.NO_SO AND WH.TP_STEP = '11' AND WH.YN_DONE = 'Y'
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
WHERE (SH.YN_CLOSE IS NULL OR SH.YN_CLOSE = 'N')
AND SL.QT_SO > SL.QT_GIR
AND SL.QT_PO = SL.QT_IN
AND EXISTS (SELECT 1 
			FROM CZ_MA_WORKFLOWH WH
			LEFT JOIN MA_USER MU ON MU.CD_COMPANY = WH.CD_COMPANY AND MU.ID_USER = WH.ID_SALES
			WHERE WH.CD_COMPANY = SH.CD_COMPANY
			AND WH.NO_KEY = SH.NO_SO
			AND WH.TP_STEP = '11'
			AND WH.YN_DONE = 'Y'
			AND (ISNULL(@P_ID_USER, '') = '' OR WH.ID_LOG = @P_ID_USER)
			AND (ISNULL(@P_CD_SALEGRP, '') = '' OR MU.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
			AND (ISNULL(@P_CD_SALEORG, '') = '' OR EXISTS (SELECT 1 
											               FROM MA_SALEGRP
											               WHERE CD_COMPANY = MU.CD_COMPANY
											               AND CD_SALEGRP = MU.CD_SALEGRP
											               AND CD_SALEORG = @P_CD_SALEORG)))

GO

