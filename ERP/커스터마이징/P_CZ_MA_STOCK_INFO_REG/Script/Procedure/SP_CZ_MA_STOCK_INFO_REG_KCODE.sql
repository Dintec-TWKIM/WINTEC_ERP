USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[PS_CZ_MA_SOURCING_REG]    Script Date: 2019-04-29 오후 5:36:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MA_STOCK_INFO_REG_KCODE]
(
	  @P_CD_COMPANY		NVARCHAR(7), 
	  @P_NO_DRAWING		NVARCHAR(20),
	  @P_CD_PARTNER		NVARCHAR(20), 
	  @P_CD_ITEM		NVARCHAR(20), 
	  @P_NM_ITEM		NVARCHAR(50),
	  @P_NO_PO			NVARCHAR(20),
	  @P_TP_DATE		NVARCHAR(3),
	  @P_DT_START		NVARCHAR(8),
	  @P_DT_END			NVARCHAR(8),
	  @P_QT_QTN_FROM    NUMERIC(5, 0),
	  @P_QT_QTN_TO      NUMERIC(5, 0),
	  @P_TP_PO			NVARCHAR(3),
	  @P_YN_STOCK		NVARCHAR(1),
	  @P_YN_INV			NVARCHAR(1),
	  @P_YN_PACK		NVARCHAR(1),
	  @P_YN_WEIGHT		NVARCHAR(1),
	  @P_YN_PARTNER		NVARCHAR(1),
	  @P_YN_IMAGE		NVARCHAR(1),
	  @P_YN_GIR			NVARCHAR(1),
	  @P_CD_ITEM_MULTI  NVARCHAR(4000)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

SELECT 'N' AS S,
	   KH.KCODE,
	   KH.WEIGHT_LOG,
	   KH.WEIGHT_HGS,
	   MI1.WEIGHT AS WEIGHT_ITEM,
	   KH.CD_PARTNER1, 
	   (SELECT LN_PARTNER FROM MA_PARTNER MP WHERE MP.CD_COMPANY = KH.CD_COMPANY AND MP.CD_PARTNER = KH.CD_PARTNER1) AS NM_PARTNER1,
	   KH.CD_PARTNER2,
	   MP1.LN_PARTNER AS NM_PARTNER2,
	   KH.DC_IMAGE1,
	   KH.DC_IMAGE2,
	   KH.DC_IMAGE3,
	   KH.DC_IMAGE4,
	   KH.DC_IMAGE5,
	   MI1.YN_ITEM_IMAGE,
	   QL.QT_ITEM,
	   MI.DT_IO,
	   MI.NO_PO,
	   MI.NO_LINE,
	   MI.CD_ITEM,
	   MI.NM_ITEM,
	   MI.CD_ITEM_PARTNER,
	   MI.NM_ITEM_PARTNER,
	   MI.YN_PACK,
	   MI.YN_GIR,
	   MI.CD_LOCATION,
	   (CASE WHEN MI.CLS_ITEM = '009' THEN (ISNULL(ML.QT_OPEN_SL02, 0) + ISNULL(OM.QT_MONTH_SL02, 0)) 
									  ELSE (ISNULL(ML.QT_OPEN_SL01, 0) + ISNULL(OM.QT_MONTH_SL01, 0)) END) AS QT_INV,
	   KH.DC_RMK,
	   KH.ID_INSERT,
	   MU1.NM_USER AS NM_INSERT, 
	   KH.DTS_INSERT, 
	   KH.ID_UPDATE,
	   MU2.NM_USER	AS NM_UPDATE, 
	   LEFT(KH.DTS_UPDATE, 8) AS DTS_UPDATE,
	   ((CASE WHEN KH.CD_COMPANY = 'K200' THEN 'http://erpd.dintec.co.kr/stock/m/write.aspx?code=' 
										  ELSE 'http://erp.dintec.co.kr/stock/m/write.aspx?code=' END) + KH.KCODE) AS CD_QR  
FROM CZ_MA_KCODE_HGS KH
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = KH.CD_COMPANY AND MP.CD_PARTNER = KH.CD_PARTNER1
LEFT JOIN MA_PARTNER MP1 ON MP1.CD_COMPANY = KH.CD_COMPANY AND MP1.CD_PARTNER = KH.CD_PARTNER2
LEFT JOIN MA_USER MU1 ON MU1.CD_COMPANY = KH.CD_COMPANY AND MU1.ID_USER = KH.ID_INSERT
LEFT JOIN MA_USER MU2 ON MU2.CD_COMPANY = KH.CD_COMPANY AND MU2.ID_USER = KH.ID_UPDATE
LEFT JOIN (SELECT QL.CD_COMPANY, QL.NO_DRAWING,
				  SUM(CASE WHEN QH.DT_INQ BETWEEN CONVERT(CHAR(8), DATEADD(YEAR, -1, GETDATE()), 112) AND CONVERT(CHAR(8), GETDATE(), 112) THEN 1 ELSE 0 END) AS QT_ITEM,
				  MAX(QL.NO_ENGINE) AS NO_ENGINE 
		   FROM CZ_SA_QTNH QH 
		   JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
		   GROUP BY QL.CD_COMPANY, QL.NO_DRAWING) QL
ON QL.CD_COMPANY = KH.CD_COMPANY AND QL.NO_DRAWING = KH.KCODE
LEFT JOIN (SELECT B.CD_COMPANY,
				  B.NO_DRAWING,
				  B.DT_IO,
				  B.NO_PO,
				  B.NO_LINE,
				  B.CD_ITEM,
				  B.NM_ITEM,
				  B.CD_LOCATION,
				  B.CLS_ITEM,
				  B.CD_ITEM_PARTNER,
				  B.NM_ITEM_PARTNER,
				  B.YN_PACK,
				  B.YN_GIR
		   FROM (SELECT A.CD_COMPANY,
		 				A.NO_DRAWING,
		 				A.DT_IO,
		 				A.NO_PO,
		 				A.NO_LINE,
		 				A.CD_ITEM,
		 				A.NM_ITEM,
		 				A.CD_LOCATION,
		 				A.CLS_ITEM,
		 				A.CD_ITEM_PARTNER,
		 				A.NM_ITEM_PARTNER,
		 				A.YN_PACK,
						A.YN_GIR,
		 				ROW_NUMBER() OVER (PARTITION BY A.CD_COMPANY, A.NO_DRAWING ORDER BY A.DT_IO DESC) AS IDX 
			     FROM (SELECT IH.CD_COMPANY,
							  ISNULL(QL.NO_DRAWING, MI.NO_STND) AS NO_DRAWING,
							  IH.DT_IO,
							  PL.NO_PO,
							  PL.NO_LINE,
							  PL.CD_ITEM,
							  MI.NM_ITEM,
							  ISNULL(LC.CD_LOCATION, MI.CD_ZONE) AS CD_LOCATION,
							  MI.CLS_ITEM,
							  QL.CD_ITEM_PARTNER,
							  QL.NM_ITEM_PARTNER,
							  (CASE WHEN EXISTS (SELECT 1 
							  				     FROM CZ_SA_PACKL PK 
							  				     WHERE PK.CD_COMPANY = QL.CD_COMPANY
							  				     AND PK.NO_FILE = QL.NO_FILE
							  				     AND PK.NO_QTLINE = QL.NO_LINE) THEN 'Y' ELSE 'N' END) AS YN_PACK,
							  (CASE WHEN EXISTS (SELECT 1 
							  				     FROM SA_GIRH GH
												 JOIN SA_GIRL GL ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR 
							  				     WHERE GH.CD_COMPANY = QL.CD_COMPANY
												 AND GH.STA_GIR IN ('R', 'C')
							  				     AND GL.NO_SO = QL.NO_FILE
							  				     AND GL.SEQ_SO = QL.NO_LINE) THEN 'Y' ELSE 'N' END) AS YN_GIR
					   FROM PU_POL PL
					   JOIN MA_PITEM MI ON MI.CD_COMPANY = PL.CD_COMPANY AND MI.CD_PLANT = PL.CD_PLANT AND MI.CD_ITEM = PL.CD_ITEM
					   LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = PL.CD_COMPANY AND QL.NO_FILE = PL.NO_SO AND QL.NO_LINE = PL.NO_LINE
					   LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
					   LEFT JOIN MM_QTIO_LOCATION LC ON LC.CD_COMPANY = IL.CD_COMPANY AND LC.NO_IO = IL.NO_IO AND LC.NO_IOLINE = IL.NO_IOLINE
					   LEFT JOIN MM_QTIOH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO
					   WHERE IH.CD_COMPANY = @P_CD_COMPANY
					   AND (@P_TP_DATE <> '001' OR (IH.DT_IO BETWEEN @P_DT_START AND @P_DT_END))
					   AND (ISNULL(@P_CD_ITEM, '') = '' OR PL.CD_ITEM LIKE @P_CD_ITEM + '%')
					   AND (ISNULL(@P_CD_ITEM_MULTI, '') = '' OR PL.CD_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_ITEM_MULTI)))
					   AND (ISNULL(@P_NM_ITEM, '') = '' OR MI.NM_ITEM LIKE '%' + @P_NM_ITEM + '%')
					   AND (ISNULL(@P_NO_PO, '') = '' OR PL.NO_PO LIKE @P_NO_PO + '%')
					   AND (ISNULL(@P_TP_PO, '') = '' OR (@P_TP_PO = '001' AND EXISTS (SELECT 1 
																					   FROM PU_POH PH
																					   WHERE PH.CD_COMPANY = IL.CD_COMPANY
																					   AND PH.NO_PO = IL.NO_PSO_MGMT
																					   AND PH.CD_TPPO IN ('1300', '1400', '2300', '2400'))) OR
														 (@P_TP_PO = '002' AND EXISTS (SELECT 1 
																					   FROM PU_POH PH
																					   WHERE PH.CD_COMPANY = IL.CD_COMPANY
																					   AND PH.NO_PO = IL.NO_PSO_MGMT
																					   AND PH.CD_TPPO NOT IN ('1300', '1400', '2300', '2400'))))) A
				 WHERE A.NO_DRAWING IS NOT NULL
		         AND A.NO_DRAWING <> ''
		         AND (ISNULL(@P_NO_DRAWING, '') = '' OR A.NO_DRAWING LIKE @P_NO_DRAWING + '%')
		         AND (ISNULL(@P_YN_PACK, 'N') = 'N' OR A.YN_PACK = 'N')
				 AND (ISNULL(@P_YN_GIR, 'N') = 'N' OR A.YN_GIR = 'N'))B
		   WHERE IDX = 1) MI 
ON MI.CD_COMPANY = KH.CD_COMPANY AND MI.NO_DRAWING = KH.KCODE
LEFT JOIN (SELECT OL.CD_COMPANY, OL.CD_ITEM,
				  SUM(CASE WHEN OL.CD_SL = 'SL01' THEN ISNULL(OL.QT_GOOD_INV, 0) + ISNULL(OL.QT_REJECT_INV, 0) + ISNULL(OL.QT_INSP_INV, 0) + ISNULL(OL.QT_TRANS_INV, 0) ELSE 0 END) AS QT_OPEN_SL01,
	  	   		  SUM(CASE WHEN OL.CD_SL = 'SL02' THEN ISNULL(OL.QT_GOOD_INV, 0) + ISNULL(OL.QT_REJECT_INV, 0) + ISNULL(OL.QT_INSP_INV, 0) + ISNULL(OL.QT_TRANS_INV, 0) ELSE 0 END) AS QT_OPEN_SL02
	  	   FROM MM_OPENQTL OL
	  	   WHERE OL.YM_STANDARD = CONVERT(NVARCHAR, YEAR(GETDATE())) + '00'
		   AND OL.CD_SL IN ('SL01', 'SL02')
	  	   GROUP BY OL.CD_COMPANY, OL.CD_ITEM) ML
ON ML.CD_COMPANY = MI.CD_COMPANY AND ML.CD_ITEM = MI.CD_ITEM
LEFT JOIN (SELECT CD_COMPANY, CD_ITEM,
		   		  SUM(CASE WHEN CD_SL = 'SL01' THEN ISNULL(QT_GOOD_GR, 0) - ISNULL(QT_GOOD_GI, 0) ELSE 0 END) AS QT_MONTH_SL01,
				  SUM(CASE WHEN CD_SL = 'SL02' THEN ISNULL(QT_GOOD_GR, 0) - ISNULL(QT_GOOD_GI, 0) ELSE 0 END) AS QT_MONTH_SL02
		   FROM MM_OHSLINVM
		   WHERE YY_INV = YEAR(GETDATE())
		   AND CD_SL IN ('SL01', 'SL02')
		   GROUP BY CD_COMPANY, CD_ITEM) OM
ON OM.CD_COMPANY = MI.CD_COMPANY AND OM.CD_ITEM = MI.CD_ITEM
LEFT JOIN (SELECT CD_COMPANY,
		   	      CD_ITEM,
				  WEIGHT,
		   	      (CASE WHEN ((IMAGE1 IS NULL OR (IMAGE1 NOT LIKE '%.jpg' AND IMAGE1 NOT LIKE '%.png')) AND
		   					  (IMAGE2 IS NULL OR (IMAGE2 NOT LIKE '%.jpg' AND IMAGE2 NOT LIKE '%.png')) AND 
		   					  (IMAGE3 IS NULL OR (IMAGE3 NOT LIKE '%.jpg' AND IMAGE3 NOT LIKE '%.png')) AND 
		   					  (IMAGE4 IS NULL OR (IMAGE4 NOT LIKE '%.jpg' AND IMAGE4 NOT LIKE '%.png')) AND 
		   					  (IMAGE5 IS NULL OR (IMAGE5 NOT LIKE '%.jpg' AND IMAGE5 NOT LIKE '%.png')) AND 
		   					  (IMAGE6 IS NULL OR (IMAGE6 NOT LIKE '%.jpg' AND IMAGE6 NOT LIKE '%.png'))) THEN 'N' ELSE 'Y' END) AS YN_ITEM_IMAGE
		   FROM MA_PITEM) MI1
ON MI1.CD_COMPANY = MI.CD_COMPANY AND MI1.CD_ITEM = MI.CD_ITEM
WHERE KH.CD_COMPANY = @P_CD_COMPANY
AND QL.NO_ENGINE IS NOT NULL
AND (@P_TP_DATE <> '001' OR MI.DT_IO IS NOT NULL)
AND (@P_TP_DATE <> '002' OR (KH.DTS_UPDATE BETWEEN @P_DT_START + '000000' AND @P_DT_END + '999999'))
AND	(ISNULL(@P_NO_DRAWING, '') = '' OR KH.KCODE LIKE @P_NO_DRAWING + '%')
AND	(ISNULL(@P_CD_PARTNER, '') = '' OR KH.CD_PARTNER1 = @P_CD_PARTNER OR KH.CD_PARTNER2 = @P_CD_PARTNER)
AND (ISNULL(@P_CD_ITEM, '') = '' OR MI.CD_ITEM IS NOT NULL)
AND (ISNULL(@P_CD_ITEM_MULTI, '') = '' OR MI.CD_ITEM IS NOT NULL)
AND (ISNULL(@P_NM_ITEM, '') = '' OR MI.NM_ITEM IS NOT NULL)
AND (ISNULL(@P_NO_PO, '') = '' OR MI.NO_PO IS NOT NULL)
AND (ISNULL(@P_YN_STOCK, 'N') = 'N' OR MI.CLS_ITEM <> '009')
AND (ISNULL(@P_YN_INV, 'N') = 'N' OR (CASE WHEN MI.CLS_ITEM = '009' THEN (ISNULL(ML.QT_OPEN_SL02, 0) + ISNULL(OM.QT_MONTH_SL02, 0)) 
																	ELSE (ISNULL(ML.QT_OPEN_SL01, 0) + ISNULL(OM.QT_MONTH_SL01, 0)) END) > 0)
AND (ISNULL(@P_YN_PACK, 'N') = 'N' OR MI.YN_PACK IS NULL OR MI.YN_PACK = 'N')
AND (ISNULL(@P_YN_GIR, 'N') = 'N' OR MI.YN_GIR IS NULL OR MI.YN_GIR = 'N')
AND (ISNULL(@P_YN_WEIGHT, 'N') = 'N' OR ((KH.WEIGHT_LOG IS NULL OR KH.WEIGHT_LOG = 0) AND
									     (MI1.WEIGHT IS NULL OR MI1.WEIGHT = 0)))
AND (ISNULL(@P_YN_PARTNER, 'N') = 'N' OR ((KH.CD_PARTNER1 IS NULL OR KH.CD_PARTNER1 = '') AND 
										  (KH.CD_PARTNER2 IS NULL OR KH.CD_PARTNER2 = '')))
AND (ISNULL(@P_YN_IMAGE, 'N') = 'N' OR ((KH.DC_IMAGE1 IS NULL OR (KH.DC_IMAGE1 NOT LIKE '%.jpg' AND KH.DC_IMAGE1 NOT LIKE '%.png')) AND 
										(KH.DC_IMAGE2 IS NULL OR (KH.DC_IMAGE2 NOT LIKE '%.jpg' AND KH.DC_IMAGE2 NOT LIKE '%.png')) AND
										(KH.DC_IMAGE3 IS NULL OR (KH.DC_IMAGE3 NOT LIKE '%.jpg' AND KH.DC_IMAGE3 NOT LIKE '%.png')) AND 
										(KH.DC_IMAGE4 IS NULL OR (KH.DC_IMAGE4 NOT LIKE '%.jpg' AND KH.DC_IMAGE4 NOT LIKE '%.png')) AND 
										(KH.DC_IMAGE5 IS NULL OR (KH.DC_IMAGE5 NOT LIKE '%.jpg' AND KH.DC_IMAGE5 NOT LIKE '%.png')) AND
										(MI1.YN_ITEM_IMAGE IS NULL OR MI1.YN_ITEM_IMAGE = 'N')))
AND ((@P_QT_QTN_TO = 0 AND QL.QT_ITEM >= @P_QT_QTN_FROM) OR 
	 (@P_QT_QTN_FROM = 0 AND QL.QT_ITEM <= @P_QT_QTN_TO) OR
	 (QL.QT_ITEM BETWEEN @P_QT_QTN_FROM AND @P_QT_QTN_TO))
ORDER BY MI.CD_LOCATION, MI.NO_PO, MI.CD_ITEM_PARTNER

GO