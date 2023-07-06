USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MM_NORMAL_ITEM_RPT_S1]    Script Date: 2019-02-26 오전 8:58:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_MM_NORMAL_ITEM_RPT_S1]
(
    @P_CD_COMPANY   NVARCHAR(100),
	@P_DT_TODAY		NVARCHAR(8),
	@P_NO_SO		NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
	SELECT PL.CD_COMPANY,
		   MC.NM_COMPANY,
		   PL.NO_SO,
		   QL.NO_LINE,
		   PL.NO_SOLINE,
		   QL.NO_DSP,
		   MP.LN_PARTNER,
		   MH.NM_VESSEL,
		   MP1.LN_PARTNER AS NM_SUPPLIER,
		   ME.NM_KOR AS NM_EMP,
		   SL.CD_ITEM,
		   QL.CD_ITEM_PARTNER,
		   QL.NM_ITEM_PARTNER,
		   (ISNULL(SL.QT_SO, 0) * ISNULL(QL.RT_BOM, 0)) AS QT_SO,
		   ISNULL(PL.QT_IN, 0) AS QT_IN,
		   ISNULL(PL.QT_STOCK_PACK, 0) AS QT_STOCK_PACK,
		   ISNULL(PL.QT_IN_RETURN, 0) AS QT_IN_RETURN,
		   (ISNULL(OL.QT_OUT, 0) * ISNULL(QL.RT_BOM, 0)) AS QT_OUT,
		   ISNULL(OL.QT_STOCK_OUT, 0) AS QT_STOCK_OUT,
		   (ISNULL(OL1.QT_OUT_RETURN, 0) * ISNULL(QL.RT_BOM, 0)) AS QT_OUT_RETURN,
		   ISNULL(OL1.QT_STOCK_RETURN, 0) AS QT_STOCK_RETURN,
		   (ISNULL(PL.QT_IO_OUT, 0) + (ISNULL(OL1.QT_IO_OUT, 0) * ISNULL(QL.RT_BOM, 0)) + ISNULL(OL1.QT_STOCK_OUT, 0)) AS QT_IO_OUT,
		   (ISNULL(PL.QT_IO_IN, 0) + (ISNULL(OL1.QT_IO_IN, 0) * ISNULL(QL.RT_BOM, 0))) AS QT_IO_IN,
		   ((ISNULL(PL.QT_IN, 0) + ISNULL(PL.QT_STOCK_PACK, 0) + (ISNULL(OL1.QT_OUT_RETURN, 0) * ISNULL(QL.RT_BOM, 0)) + ISNULL(OL1.QT_STOCK_RETURN, 0)) -
		    ((ISNULL(OL.QT_OUT, 0) * ISNULL(QL.RT_BOM, 0)) + ISNULL(OL.QT_STOCK_OUT, 0) + ISNULL(PL.QT_IN_RETURN, 0) + ISNULL(PL.QT_IO_OUT, 0) + (ISNULL(OL1.QT_IO_OUT, 0) * ISNULL(QL.RT_BOM, 0)) + ISNULL(OL1.QT_STOCK_OUT, 0))) AS QT_INV,
		   PL.NM_EXCH,
		   PL.RT_EXCH,
		   ISNULL(PL.UM_EX, 0) AS UM_EX_PO,
		   ISNULL(PL.UM, 0) AS UM_PO,
		   ISNULL(SB.UM_KR, 0) AS UM_STOCK,
		   (((ISNULL(PL.QT_IN, 0) - (ISNULL(OL.QT_OUT, 0) * ISNULL(QL.RT_BOM, 0)) + (ISNULL(OL1.QT_OUT_RETURN, 0) * ISNULL(QL.RT_BOM, 0)) - ISNULL(PL.QT_IN_RETURN, 0) - ISNULL(PL.QT_IO_OUT, 0) - (ISNULL(OL1.QT_IO_OUT, 0) * ISNULL(QL.RT_BOM, 0))) * ISNULL(PL.UM, 0)) +
		    ((ISNULL(PL.QT_STOCK_PACK, 0) - ISNULL(OL.QT_STOCK_OUT, 0) + ISNULL(OL1.QT_STOCK_RETURN, 0) - ISNULL(OL1.QT_STOCK_OUT, 0)) * ISNULL(SB.UM_KR, 0))) AS AM_INV,
		   PL.DT_IN,
		   STUFF((SELECT DISTINCT ',' + ISNULL((SELECT NM_LOCATION 
				  							    FROM MA_LOCATION
				  							    WHERE CD_COMPANY = LC.CD_COMPANY
				  							    AND (CD_LOCATION = LC.CD_LOCATION OR 
				  							  	     REPLACE(CD_LOCATION, '-', '') = REPLACE(LC.CD_LOCATION, '-', ''))), LC.CD_LOCATION) 
				  FROM MM_QTIO_LOCATION LC
				  WHERE LC.CD_COMPANY = PL.CD_COMPANY
				  AND LC.CD_LOCATION IS NOT NULL
				  AND EXISTS (SELECT 1 
							  FROM MM_QTIO IL
							  WHERE IL.CD_COMPANY = LC.CD_COMPANY
							  AND IL.NO_IO = LC.NO_IO
							  AND IL.NO_IOLINE = LC.NO_IOLINE
				              AND IL.CD_QTIOTP IN ('100', '110', '120', '121')
				              AND IL.CD_PJT = PL.NO_SO
				              AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
							  AND EXISTS (SELECT 1 FROM MM_QTIOH IH
										  WHERE (IH.YN_RETURN IS NULL OR IH.YN_RETURN = 'N')
										  AND IH.DT_IO <= @P_DT_TODAY))
			   	  FOR XML PATH('')),1,1,'') AS NM_LOCATION,
		   OL.NO_GIR,
		   PK.NO_PACK,
		   STUFF((SELECT DISTINCT ',' + ISNULL((SELECT NM_LOCATION 
				  			                    FROM MA_LOCATION
				  			                    WHERE CD_COMPANY = PH.CD_COMPANY
				  			                    AND (CD_LOCATION = PH.CD_LOCATION OR 
				  			                         REPLACE(CD_LOCATION, '-', '') = REPLACE(PH.CD_LOCATION, '-', ''))), PH.CD_LOCATION) 
				  FROM CZ_SA_PACKH PH
				  WHERE PH.CD_COMPANY = PL.CD_COMPANY
				  AND PH.CD_LOCATION IS NOT NULL
				  AND EXISTS (SELECT 1 
				  			  FROM CZ_SA_PACKL PK
				  			  WHERE PK.CD_COMPANY = PH.CD_COMPANY
				  			  AND PK.NO_GIR = PH.NO_GIR
				  			  AND PK.NO_FILE = PL.NO_SO
				  			  AND PK.NO_QTLINE = PL.NO_SOLINE)
			   	  FOR XML PATH('')),1,1,'') AS NM_PACK_LOCATION
	FROM (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.NO_SOLINE,
				 MAX(PL.CD_SUPPLIER) AS CD_SUPPLIER,
				 MAX(PL.DT_IN) AS DT_IN,
				 MAX(PL.NM_EXCH) AS NM_EXCH,
				 MAX(PL.RT_EXCH) AS RT_EXCH,
				 MAX(PL.UM_EX) AS UM_EX,
				 MAX(PL.UM) AS UM,
				 SUM(PL.QT_IN) AS QT_IN,
				 SUM(PL.QT_STOCK_PACK) AS QT_STOCK_PACK,
				 SUM(PL.QT_IN_RETURN) AS QT_IN_RETURN,
				 SUM(PL.QT_IO_OUT) AS QT_IO_OUT,
				 SUM(PL.QT_IO_IN) AS QT_IO_IN
		  FROM (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.NO_SOLINE,
					   MAX(PH.CD_PARTNER) AS CD_SUPPLIER,
					   MAX(IH.DT_IO) AS DT_IN,
					   MAX(MC.NM_SYSDEF) AS NM_EXCH,
					   MAX(PH.RT_EXCH) AS RT_EXCH,
					   MAX(PL.UM_EX) AS UM_EX,
					   MAX(PL.UM) AS UM,
					   SUM(IL.QT_IO) AS QT_IN,
					   0 AS QT_STOCK_PACK,
					   SUM(IL1.QT_IN_RETURN) AS QT_IN_RETURN,
					   SUM(IL1.QT_IO_OUT) AS QT_IO_OUT,
					   SUM(IL1.QT_IO_IN) AS QT_IO_IN
				FROM MM_QTIOH IH
				LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IO = IH.NO_IO
				LEFT JOIN (SELECT IL.CD_COMPANY,
						 	      IL.NO_IO_MGMT,
						 	      IL.NO_IOLINE_MGMT,
								  SUM(CASE WHEN IH.YN_RETURN = 'Y' AND (IL.CD_QTIOTP = '130' OR IL.CD_QTIOTP = '131' OR IL.CD_QTIOTP = '140' OR IL.CD_QTIOTP = '141') THEN IL.QT_IO ELSE 0 END) AS QT_IN_RETURN,
						 	      SUM(CASE WHEN (IL.CD_QTIOTP = '400' OR IL.CD_QTIOTP = '500') THEN IL.QT_IO ELSE 0 END) AS QT_IO_OUT,
						 	      SUM(CASE WHEN (IL.CD_QTIOTP = '400' OR IL.CD_QTIOTP = '500') THEN IL1.QT_IO ELSE 0 END) AS QT_IO_IN
						   FROM MM_QTIO IL
						   LEFT JOIN MM_QTIOH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO
						   LEFT JOIN MM_QTIO IL1 ON IL1.CD_COMPANY = IL.CD_COMPANY AND IL1.NO_IO_MGMT = IL.NO_IO AND IL1.NO_IOLINE_MGMT = IL.NO_IOLINE
						   WHERE IH.DT_IO <= @P_DT_TODAY 
						   AND (IL.CD_QTIOTP = '130' OR IL.CD_QTIOTP = '131' OR IL.CD_QTIOTP = '140' OR IL.CD_QTIOTP = '141' OR IL.CD_QTIOTP = '400' OR IL.CD_QTIOTP = '500')
						   GROUP BY IL.CD_COMPANY, IL.NO_IO_MGMT, IL.NO_IOLINE_MGMT) IL1
				ON IL1.CD_COMPANY = IL.CD_COMPANY AND IL1.NO_IO_MGMT = IL.NO_IO AND IL1.NO_IOLINE_MGMT = IL.NO_IOLINE
				LEFT JOIN PU_POL PL ON PL.CD_COMPANY = IL.CD_COMPANY AND PL.NO_PO = IL.NO_PSO_MGMT AND PL.NO_LINE = IL.NO_PSOLINE_MGMT
				LEFT JOIN PU_POH PH ON PH.CD_COMPANY = PL.CD_COMPANY AND PH.NO_PO = PL.NO_PO
				LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = PH.CD_EXCH
				WHERE IH.CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_COMPANY))
				AND IH.DT_IO <= @P_DT_TODAY
				AND (ISNULL(@P_NO_SO, '') = '' OR PL.NO_SO = @P_NO_SO)
				AND ISNULL(IH.YN_RETURN, 'N') = 'N'
				AND (IL.CD_QTIOTP = '100' OR IL.CD_QTIOTP = '110' OR IL.CD_QTIOTP = '120' OR IL.CD_QTIOTP = '121')
				AND IL.CD_ITEM NOT LIKE 'SD%'
				AND IL.CD_SL = 'SL01'
				GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.NO_SOLINE
				UNION ALL
				SELECT GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO, GL.SEQ_SO,
					   'STOCK' AS CD_SUPPLIER,
					   MAX(IH.DT_IO) AS DT_IN,
					   'KRW' AS NM_EXCH,
					   1 AS RT_EXCH,
					   0 AS UM_EX,
					   0 AS UM,
					   0 AS QT_IN,
					   SUM(GL.QT_GI) AS QT_STOCK_PACK,
					   0 AS QT_IN_RETURN,
					   0 AS QT_IO_OUT,
					   0 AS QT_IO_IN
				FROM MM_GIREQL GL
				LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = GL.CD_COMPANY AND IL.NO_ISURCV = GL.NO_GIREQ AND IL.NO_ISURCVLINE = GL.NO_LINE AND IL.CD_SL = 'SL03'
				LEFT JOIN MM_QTIOH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO
				WHERE GL.CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_COMPANY))
				AND IH.DT_IO <= @P_DT_TODAY
				AND (ISNULL(@P_NO_SO, '') = '' OR GL.NO_SO = @P_NO_SO) 
				AND GL.CD_SL = 'SL02' 
				AND GL.CD_GRSL = 'SL03'
				AND GL.QT_GI > 0
				GROUP BY GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO) PL
		  GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_LINE, PL.NO_SOLINE) PL
	JOIN (SELECT QL.CD_COMPANY, QL.NO_FILE, QL.NO_LINE,
				 QL.NO_DSP,
				 QL.CD_ITEM_PARTNER,
				 QL.NM_ITEM_PARTNER,
				 (CASE WHEN QL.TP_BOM = 'S' THEN 1 ELSE (ISNULL(QL.QT_QTN, 0) / ISNULL(QL1.QT_QTN, 1)) END) AS RT_BOM 
		  FROM CZ_SA_QTNL QL
		  LEFT JOIN CZ_SA_QTNL QL1 ON QL1.CD_COMPANY = QL.CD_COMPANY AND QL1.NO_FILE = QL.NO_FILE AND QL1.NO_LINE = QL.NO_LINE_PARENT) QL
	ON QL.CD_COMPANY = PL.CD_COMPANY AND QL.NO_FILE = PL.NO_SO AND QL.NO_LINE = PL.NO_LINE
	JOIN SA_SOL SL ON SL.CD_COMPANY = PL.CD_COMPANY AND SL.NO_SO = PL.NO_SO AND SL.SEQ_SO = PL.NO_SOLINE
	LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
	LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO
	LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
					  MAX(OL.NO_ISURCV) AS NO_GIR,
			   		  SUM(ISNULL(OL.QT_IO, 0) - ISNULL(GL.QT_GIR_STOCK, 0)) AS QT_OUT,
					  SUM(GL.QT_GIR_STOCK) AS QT_STOCK_OUT
			   FROM MM_QTIOH OH
			   LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
			   LEFT JOIN SA_GIRL GL ON GL.CD_COMPANY = OL.CD_COMPANY AND GL.NO_GIR = OL.NO_ISURCV AND GL.SEQ_GIR = OL.NO_ISURCVLINE
			   WHERE ISNULL(OH.YN_RETURN, 'N') = 'N'
			   AND OH.DT_IO <= @P_DT_TODAY
			   AND OL.CD_QTIOTP IN ('200', '210', '240', '241', '250', '251')
			   AND OL.CD_ITEM NOT LIKE 'SD%'
			   GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) OL
	ON OL.CD_COMPANY = SL.CD_COMPANY AND OL.NO_PSO_MGMT = SL.NO_SO AND OL.NO_PSOLINE_MGMT = SL.SEQ_SO
	LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
			   		  SUM(CASE WHEN ISNULL(GL.QT_GIR_STOCK, 0) = 0 THEN OL.QT_IO ELSE 0 END) AS QT_OUT_RETURN,
					  SUM(CASE WHEN ISNULL(GL.QT_GIR_STOCK, 0) > 0 THEN OL.QT_IO ELSE 0 END) AS QT_STOCK_RETURN,
					  SUM(CASE WHEN ISNULL(GL.QT_GIR_STOCK, 0) = 0 THEN OL2.QT_IO_OUT ELSE 0 END) AS QT_IO_OUT,
					  SUM(CASE WHEN ISNULL(GL.QT_GIR_STOCK, 0) > 0 THEN OL2.QT_IO_OUT ELSE 0 END) AS QT_STOCK_OUT,
					  SUM(OL2.QT_IO_IN) AS QT_IO_IN
			   FROM MM_QTIOH OH
			   LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
			   LEFT JOIN MM_QTIO OL1 ON OL1.CD_COMPANY = OL.CD_COMPANY AND OL1.NO_IO = OL.NO_IO_MGMT AND OL1.NO_IOLINE = OL.NO_IOLINE_MGMT
			   LEFT JOIN (SELECT OL.CD_COMPANY,
			   		 			 OL.NO_IO_MGMT,
			   			  	     OL.NO_IOLINE_MGMT,
			   			  	     SUM(OL.QT_IO) AS QT_IO_OUT,
			   			  	     SUM(OL1.QT_IO) AS QT_IO_IN
			   			  FROM MM_QTIO OL
						  LEFT JOIN MM_QTIO OH ON OH.CD_COMPANY = OL.CD_COMPANY AND OH.NO_IO = OL.NO_IO
			   			  LEFT JOIN MM_QTIO OL1 ON OL1.CD_COMPANY = OL.CD_COMPANY AND OL1.NO_IO_MGMT = OL.NO_IO AND OL1.NO_IOLINE_MGMT = OL.NO_IOLINE
			   			  WHERE OH.DT_IO <= @P_DT_TODAY
						  AND (OL.CD_QTIOTP = '400' OR OL.CD_QTIOTP = '500')
						  GROUP BY OL.CD_COMPANY, OL.NO_IO_MGMT, OL.NO_IOLINE_MGMT) OL2
			   ON OL2.CD_COMPANY = OL.CD_COMPANY AND OL2.NO_IO_MGMT = OL.NO_IO AND OL2.NO_IOLINE_MGMT = OL.NO_IOLINE
			   LEFT JOIN SA_GIRL GL ON GL.CD_COMPANY = OL1.CD_COMPANY AND GL.NO_GIR = OL1.NO_ISURCV AND GL.SEQ_GIR = OL1.NO_ISURCVLINE
			   WHERE OH.YN_RETURN = 'Y'
			   AND OH.DT_IO <= @P_DT_TODAY
			   AND (OL.CD_QTIOTP = '230' OR OL.CD_QTIOTP = '220' OR OL.CD_QTIOTP = '231' OR OL.CD_QTIOTP = '221')
			   AND OL.CD_ITEM NOT LIKE 'SD%'
			   GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) OL1
	ON OL1.CD_COMPANY = SL.CD_COMPANY AND OL1.NO_PSO_MGMT = SL.NO_SO AND OL1.NO_PSOLINE_MGMT = SL.SEQ_SO
	LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_FILE, PL.NO_QTLINE,
					  MAX(PL.NO_GIR) AS NO_PACK
			   FROM CZ_SA_PACKL PL
			   GROUP BY PL.CD_COMPANY, PL.NO_FILE, PL.NO_QTLINE) PK
	ON PK.CD_COMPANY = SL.CD_COMPANY AND PK.NO_FILE = SL.NO_SO AND PK.NO_QTLINE = SL.SEQ_SO
	LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = SH.CD_COMPANY
	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
	LEFT JOIN MA_PARTNER MP1 ON MP1.CD_COMPANY = PL.CD_COMPANY AND MP1.CD_PARTNER = PL.CD_SUPPLIER
	LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
	LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
	WHERE ((ISNULL(PL.QT_IN, 0) + ISNULL(PL.QT_STOCK_PACK, 0) + (ISNULL(OL1.QT_OUT_RETURN, 0) * ISNULL(QL.RT_BOM, 0)) + ISNULL(OL1.QT_STOCK_RETURN, 0)) -
		   ((ISNULL(OL.QT_OUT, 0) * ISNULL(QL.RT_BOM, 0)) + ISNULL(OL.QT_STOCK_OUT, 0) + ISNULL(PL.QT_IN_RETURN, 0) + ISNULL(PL.QT_IO_OUT, 0) + (ISNULL(OL1.QT_IO_OUT, 0) * ISNULL(QL.RT_BOM, 0)) + ISNULL(OL1.QT_STOCK_OUT, 0))) > 0
)
SELECT *,
       (CASE WHEN A.QT_IN > 0 AND QT_OUT = 0 THEN '미출고'
		     WHEN A.QT_IN > 0 AND A.QT_IN > A.QT_OUT THEN '일부출고'
			 WHEN (A.QT_STOCK_RETURN > 0 OR A.QT_OUT_RETURN > 0) AND A.QT_IO_OUT = 0 THEN '계정대체누락' 
			 WHEN A.QT_STOCK_PACK > A.QT_SO THEN '중복출고'
			 WHEN A.QT_IN > A.QT_SO THEN '과입고'
			 WHEN A.QT_STOCK_RETURN > A.QT_IO_OUT THEN '계정대체수량부족'
			 WHEN A.QT_STOCK_PACK > 0 AND A.QT_STOCK_OUT = 0 THEN '재고미출고'
			 WHEN A.QT_STOCK_PACK > 0 AND A.QT_STOCK_PACK > A.QT_STOCK_OUT THEN '재고일부출고'
			 ELSE NULL END) AS DC_REASON
FROM A
OPTION(RECOMPILE)

GO