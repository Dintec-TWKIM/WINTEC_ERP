USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_HULL_DOM_RPTH_S]    Script Date: 2018-03-06 오후 7:43:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_MA_HULL_DOM_RPTH_S]
(
	@P_CD_COMPANY      NVARCHAR(7),
    @P_TP_DATE		   NVARCHAR(3),
	@P_DT_FROM		   NVARCHAR(8),
	@P_DT_TO		   NVARCHAR(8),
	@P_NO_IMO		   NVARCHAR(10),
	@P_CD_PARTNER	   NVARCHAR(20),
	@P_CD_NATION	   NVARCHAR(4),
	@P_DC_SHIPBUILDER  NVARCHAR(100),
	@P_YN_DOM		   NVARCHAR(1)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT ISNULL(MH.YN_DOM, 'N') AS YN_DOM,
	   MH.NO_IMO,
	   MH.NM_VESSEL,
	   MP.LN_PARTNER,
	   MC2.NM_SYSDEF AS NM_NATION,
	   MH.DC_SHIPBUILDER,
	   MC3.NM_SYSDEF AS NM_TP_SHIP,
	   QH.DT_QTN,
	   GH.QT_GIR,
	   GH.DT_GIR,
	   (SELECT TOP 1 MC.NM_SYSDEF
	    FROM CZ_SA_GIRH_WORK_DETAIL WD
	    JOIN SA_GIRH GH ON GH.CD_COMPANY = WD.CD_COMPANY AND GH.NO_GIR = WD.NO_GIR
	    LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = WD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00007' AND MC.CD_SYSDEF = WD.CD_SUB_CATEGORY
	    WHERE WD.CD_COMPANY = @P_CD_COMPANY
		AND WD.NO_IMO = MH.NO_IMO
	    AND WD.CD_MAIN_CATEGORY = '001' 
	    AND WD.CD_SUB_CATEGORY <> '000'
	    ORDER BY GH.DT_GIR DESC) AS NM_PORT,
	   HR.CD_AGENT,
	   MC1.NM_SYSDEF AS NM_AGENT,
	   HR.CD_PUR,
	   MC.NM_SYSDEF AS NM_PUR,
	   ISNULL(QH.QT_QTN, 0) AS QT_QTN,
	   ISNULL(QH.QT_SO, 0) AS QT_SO,
	   ISNULL(QH.AM_QTN, 0) AS AM_QTN,
	   ISNULL(QH.AM_SO, 0) AS AM_SO,
	   ISNULL(QH.AM_STOCK, 0) AS AM_STOCK,
	   ISNULL(QH.AM_PO, 0) AS AM_PO,
	   (ISNULL(QH.AM_SO, 0) - (ISNULL(QH.AM_PO, 0) + ISNULL(QH.AM_STOCK, 0))) AS AM_PROFIT,
	   (CASE WHEN ISNULL(QH.QT_QTN, 0) = 0 THEN 0
										   ELSE ROUND(CONVERT(FLOAT, ISNULL(QH.QT_SO, 0)) / CONVERT(FLOAT, ISNULL(QH.QT_QTN, 0)) * 100, 2) END) AS RT_SO,
	   (CASE WHEN ISNULL(QH.AM_SO, 0) = 0 THEN 0
										  ELSE ROUND((ISNULL(QH.AM_SO, 0) - (ISNULL(QH.AM_PO, 0) + ISNULL(QH.AM_STOCK, 0))) / ISNULL(QH.AM_SO, 0) * 100 ,2) END) AS RT_PROFIT,
	   HR.DC_RMK_DOM,
	   (SELECT TOP 1 CONVERT(CHAR(19), CONVERT(DATETIME, LEFT(DTS_ETRYPT, 8) + ' ' + SUBSTRING(DTS_ETRYPT, 9, 2) + ':' + SUBSTRING(DTS_ETRYPT, 11, 2) + ':' + SUBSTRING(DTS_ETRYPT, 13, 2)), 20) + ' (' + NM_PRTAG + ')'
	    FROM CZ_SA_VSSL_ETRYNDH
		WHERE CALL_SIGN = MH.CALL_SIGN
		ORDER BY DTS_ETRYPT DESC) AS DTS_ETRYPT
FROM CZ_MA_HULL MH
LEFT JOIN CZ_MA_HULL_RPT HR ON HR.CD_COMPANY = @P_CD_COMPANY AND HR.NO_IMO = MH.NO_IMO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = @P_CD_COMPANY AND MP.CD_PARTNER = MH.CD_PARTNER
LEFT JOIN (SELECT QH.NO_IMO,
		   	      MAX(QH.DT_QTN) AS DT_QTN,
		   	      COUNT(QH.NO_FILE) AS QT_QTN,
		   	      SUM(QL.QT_SO) AS QT_SO,
		   	      SUM(QL.AM_QTN) AS AM_QTN,
		   	      SUM(QL.AM_SO) AS AM_SO,
		   	      SUM(QL.AM_STOCK) AS AM_STOCK,
		   	      SUM(QL.AM_PO) AS AM_PO
		   FROM CZ_SA_QTNH QH
		   JOIN (SELECT QL.CD_COMPANY, QL.NO_FILE,
		   			    COUNT(DISTINCT SL.NO_SO) AS QT_SO,
		   			    SUM(QL.AM_KR_S) AS AM_QTN,
		   			    SUM(SL.AM_KR_S) AS AM_SO,
		   			    SUM(ISNULL(SB.QT_STOCK, 0) * ISNULL(SB.UM_KR, 0)) AS AM_STOCK,
		   			    SUM(PL.AM) AS AM_PO  
		   	     FROM CZ_SA_QTNL QL
		   	     LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
		   	     LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
		   	     LEFT JOIN (SELECT CD_COMPANY, NO_SO, NO_SOLINE,
		   		    			   SUM(AM) AS AM 
		   		   	        FROM PU_POL
		   		   	        WHERE CD_ITEM NOT LIKE 'SD%'
		   		   	        GROUP BY CD_COMPANY, NO_SO, NO_SOLINE) PL 
		   	     ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
		   	     WHERE QL.CD_ITEM NOT LIKE 'SD%'
		   	     GROUP BY QL.CD_COMPANY, QL.NO_FILE) QL
		   ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
		   WHERE QH.CD_COMPANY = @P_CD_COMPANY
		   AND (@P_TP_DATE = '001' OR (QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO))
		   AND (@P_TP_DATE = '000' OR (EXISTS (SELECT 1 FROM SA_SOH SH
		   							   WHERE SH.CD_COMPANY = QH.CD_COMPANY
		   							   AND SH.NO_SO = QH.NO_FILE 
		   							   AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO)))
		   GROUP BY QH.NO_IMO) QH
ON QH.NO_IMO = MH.NO_IMO
LEFT JOIN (SELECT WD.NO_IMO,
		   	      MAX(GH.DT_GIR) AS DT_GIR,
		   	      COUNT(WD.NO_GIR) AS QT_GIR 
		   FROM CZ_SA_GIRH_WORK_DETAIL WD
		   JOIN SA_GIRH GH ON GH.CD_COMPANY = WD.CD_COMPANY AND GH.NO_GIR = WD.NO_GIR
		   WHERE WD.CD_COMPANY = @P_CD_COMPANY
		   GROUP BY WD.NO_IMO) GH
ON GH.NO_IMO = MH.NO_IMO
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = @P_CD_COMPANY AND MC.CD_FIELD = 'CZ_MA00019' AND MC.CD_SYSDEF = HR.CD_PUR
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = @P_CD_COMPANY AND MC1.CD_FIELD = 'CZ_MA00020' AND MC1.CD_SYSDEF = HR.CD_AGENT
LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = @P_CD_COMPANY AND MC2.CD_FIELD = 'MA_B000020' AND MC2.CD_SYSDEF = MP.CD_NATION
LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = @P_CD_COMPANY AND MC3.CD_FIELD = 'CZ_MA00002' AND MC3.CD_SYSDEF = MH.TP_SHIP
WHERE 1=1
AND (ISNULL(@P_NO_IMO, '') = '' OR MH.NO_IMO = @P_NO_IMO)
AND (ISNULL(@P_CD_PARTNER, '') = '' OR MH.CD_PARTNER = @P_CD_PARTNER)
AND (ISNULL(@P_CD_NATION, '') = '' OR MP.CD_NATION = @P_CD_NATION)
AND (ISNULL(@P_DC_SHIPBUILDER, '') = '' OR MH.DC_SHIPBUILDER LIKE @P_DC_SHIPBUILDER + '%')
AND (@P_YN_DOM = 'N' OR (MH.YN_DOM = 'Y' OR EXISTS (SELECT 1 
													FROM CZ_SA_GIRH_WORK_DETAIL WD
													WHERE WD.CD_COMPANY = @P_CD_COMPANY
													AND WD.NO_IMO = MH.NO_IMO
													AND WD.CD_MAIN_CATEGORY = '001' 
													AND WD.CD_SUB_CATEGORY <> '000') OR EXISTS (SELECT 1
																								FROM CZ_SA_VSSL_ETRYNDH
																								WHERE CALL_SIGN = MH.CALL_SIGN)))

GO

