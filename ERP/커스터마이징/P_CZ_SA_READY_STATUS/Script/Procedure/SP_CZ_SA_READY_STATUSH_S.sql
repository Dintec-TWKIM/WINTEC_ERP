USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_ORDER_TRACKING_H]    Script Date: 2016-06-10 오후 5:36:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_READY_STATUSH_S]
(
	@P_CD_COMPANY      NVARCHAR(7),
	@P_DT_FROM		   NVARCHAR(8),
	@P_DT_TO		   NVARCHAR(8),
	@P_CD_PARTNER	   NVARCHAR(20),
	@P_NO_IMO		   NVARCHAR(10),
	@P_NO_SO		   NVARCHAR(20),
	@P_NO_PO_PARTNER   NVARCHAR(50)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

CREATE TABLE #SA_SOL
(
	CD_COMPANY	NVARCHAR(7),
	NO_SO		NVARCHAR(20),
	SEQ_SO		NUMERIC(5, 0),
	NO_DSP		NVARCHAR(20),
	DT_DUEDATE	NVARCHAR(8),
	QT			NUMERIC(17, 4),
	QT_SO		NUMERIC(17, 4),
	QT_PO		NUMERIC(17, 4),
	QT_IN 		NUMERIC(17, 4),
	QT_GIR 		NUMERIC(17, 4),
	QT_GI 		NUMERIC(17, 4)
)

INSERT INTO #SA_SOL
SELECT SL.CD_COMPANY,
	   SL.NO_SO,
	   SL.SEQ_SO,
	   QL.NO_DSP, 
	   SL.DT_DUEDATE,
	   SUM(QL.QT) AS QT,
	   SUM(SL.QT_SO) AS QT_SO,
	   (SUM(SL.QT_PO) + SUM(ISNULL(SB.QT_STOCK, 0))) AS QT_PO,
	   SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(IL.QT_IN, 0)
									 ELSE ISNULL(PL.QT_RCV, 0) + ISNULL(SB.QT_BOOK, 0) END) AS QT_IN,
	   SUM(SL.QT_GIR) AS QT_GIR,
	   SUM(SL.QT_GI) AS QT_GI 
FROM SA_SOH SH
JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = SH.CD_COMPANY AND QH.NO_FILE = SH.NO_SO
JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
					 SUM(PL.QT_PO) AS QT_PO,
					 SUM(PL.QT_RCV) AS QT_RCV
			  FROM PU_POL PL
			  GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
LEFT JOIN (SELECT IL.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT, SUM(IL.QT_IO) AS QT_IN 
			  FROM MM_QTIO IL
			  WHERE IL.FG_PS = '1'
			  GROUP BY IL.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT) IL
ON IL.CD_COMPANY = SL.CD_COMPANY AND IL.NO_PSO_MGMT = SL.NO_SO AND IL.NO_PSOLINE_MGMT = SL.SEQ_SO
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND ((ISNULL(@P_DT_FROM, '') = '' AND ISNULL(@P_DT_TO, '') = '') OR SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO)
AND SL.CD_ITEM NOT LIKE 'SD%'
AND (ISNULL(@P_CD_PARTNER, '') = '' OR SH.CD_PARTNER = @P_CD_PARTNER)
AND (ISNULL(@P_NO_IMO, '') = '' OR SH.NO_IMO = @P_NO_IMO)
AND (ISNULL(@P_NO_SO, '') = '' OR SH.NO_SO = @P_NO_SO)
AND (ISNULL(@P_NO_PO_PARTNER, '') = '' OR SH.NO_PO_PARTNER LIKE @P_NO_PO_PARTNER + '%')
GROUP BY SL.CD_COMPANY, SL.NO_SO, SL.SEQ_SO, QL.NO_DSP, SL.DT_DUEDATE
ORDER BY QL.NO_DSP

SELECT SH.CD_PARTNER,
	   MP.LN_PARTNER,
	   SH.NO_IMO,
	   MH.NM_VESSEL,
	   SH.NO_PO_PARTNER,
	   SH.NO_SO,
	   SH.DT_SO,
	   SL.DT_DUEDATE,
	   SL.TP_STATUS,
	   MC.NM_SYSDEF_E AS NM_TP_STATUS,
	   SD.DT_EXPECT,
	   (CASE WHEN SL.QT_SO = SL.QT_IN THEN 'ALL'
			 WHEN SL.QT_IN = 0		  THEN ''
									  ELSE STUFF((SELECT CHAR(10) + ',' + CAST(NO_DSP AS NVARCHAR)
												  FROM #SA_SOL
												  WHERE CD_COMPANY = SH.CD_COMPANY
												  AND NO_SO = SH.NO_SO
												  AND QT_SO = QT_IN 
												  FOR XML PATH('')),1,1,'') END) AS DC_ITEMS,
	   PK.QT_PACK, 
	   STUFF((SELECT CHAR(10) + ('[' + PL.NO_GIR + '_' + CAST(PH.NO_PACK AS NVARCHAR) + '] ' +
								 'Weight : ' + CAST(CAST(PH.QT_GROSS_WEIGHT AS NUMERIC(14,2)) AS NVARCHAR) + ' (kg)' +
								 ' Size : ' + CAST(CAST(PH.QT_WIDTH AS INT) AS NVARCHAR) + ' X ' 
											+ CAST(CAST(PH.QT_LENGTH AS INT) AS NVARCHAR) + ' X ' 
											+ CAST(CAST(PH.QT_HEIGHT AS INT) AS NVARCHAR) + ' (mm)')
			  FROM CZ_SA_PACKH PH
			  JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK, OL.NO_IO, PL.NO_FILE 
					FROM CZ_SA_PACKL PL
					JOIN SA_GIRL GL ON GL.CD_COMPANY = PL.CD_COMPANY AND GL.NO_GIR = PL.NO_GIR AND GL.SEQ_GIR = PL.SEQ_GIR
					LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
					GROUP BY PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK, OL.NO_IO, PL.NO_FILE) PL
			  ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR AND PL.NO_PACK = PH.NO_PACK
			  WHERE PH.CD_COMPANY = SH.CD_COMPANY
			  AND PL.NO_FILE = SH.NO_SO
			  FOR XML PATH('')),1,1,'') AS DC_PACK,
	   SH.YN_ONTIME,
	   (CASE WHEN SL.QT_GI > 0 AND SL.QT_SO <> SL.QT_GI THEN 'Y' ELSE 'N' END) AS YN_PARTIAL,
	   SH.DC_RMK_READY
FROM SA_SOH SH
JOIN (SELECT CD_COMPANY, 
			 NO_SO,
			 MAX(DT_DUEDATE) AS DT_DUEDATE,
			 SUM(QT) AS QT,
			 SUM(QT_SO) AS QT_SO,
			 SUM(QT_PO) AS QT_PO,
			 SUM(QT_IN) AS QT_IN,
			 SUM(QT_GIR) AS QT_GIR,
			 SUM(QT_GI) AS QT_GI,
			 (CASE WHEN SUM(QT_SO) = SUM(QT_GI) THEN '005'
				   WHEN SUM(QT_SO) = SUM(QT_GIR) THEN '004'
				   WHEN SUM(QT_SO) = SUM(QT_IN) THEN '003'
				   WHEN SUM(QT_SO) = SUM(QT_PO) THEN '002'
				   ELSE '001' END) TP_STATUS
	  FROM #SA_SOL
	  GROUP BY CD_COMPANY, NO_SO) SL
ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN (SELECT PH.CD_COMPANY, PL.NO_FILE, COUNT(1) AS QT_PACK
		   FROM CZ_SA_PACKH PH
		   JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK, PL.NO_FILE 
				 FROM CZ_SA_PACKL PL
				 JOIN SA_GIRL GL ON GL.CD_COMPANY = PL.CD_COMPANY AND GL.NO_GIR = PL.NO_GIR AND GL.SEQ_GIR = PL.SEQ_GIR
				 GROUP BY PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK, PL.NO_FILE) PL
		   ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR AND PL.NO_PACK = PH.NO_PACK
		   GROUP BY PH.CD_COMPANY, PL.NO_FILE) PK
ON PK.CD_COMPANY = SH.CD_COMPANY AND PK.NO_FILE = SH.NO_SO
LEFT JOIN (SELECT CD_COMPANY, NO_SO, MAX(DT_EXPECT) AS DT_EXPECT
		   FROM CZ_SA_DEFERRED_DELIVERY
		   WHERE TP_TYPE = '2'
		   GROUP BY CD_COMPANY, NO_SO) SD
ON SD.CD_COMPANY = SH.CD_COMPANY AND SD.NO_SO = SH.NO_SO
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = SL.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00034' AND MC.CD_SYSDEF = SL.TP_STATUS
ORDER BY SH.CD_PARTNER, SH.NO_IMO, SH.DT_SO

DROP TABLE #SA_SOL

GO