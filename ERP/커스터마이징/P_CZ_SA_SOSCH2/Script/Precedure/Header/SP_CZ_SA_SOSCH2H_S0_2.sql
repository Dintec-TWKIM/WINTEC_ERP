USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SOSCH2H_S0_2]    Script Date: 2018-08-01 오후 5:48:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOSCH2H_S0_2] 
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_SO_PRE		NVARCHAR(5) = '',
	@P_NO_SO			NVARCHAR(20) = '',
	@P_DT_SO_FROM		NCHAR(8),
	@P_DT_SO_TO			NCHAR(8),
	@P_MULTI_SALEGRP	NVARCHAR(4000) = '',
	@P_TP_BUSI			NVARCHAR(3) = '',
	@P_STA_SO			NVARCHAR(3) = '',     
	@P_TP_SO			NVARCHAR(1000) = '',
	@P_CD_PARTNER	    NVARCHAR(4000) = '',
	@P_CD_PARTNER_GRP	NVARCHAR(4000) = '',
	@P_NO_EMP_MULTI		NVARCHAR(1000) = '',
	@P_MULTI_GRP_MFG	NVARCHAR(4000) = '',
	@P_NO_PO_PARTNER	NVARCHAR(50) = '',
	@P_CD_SUPPLIER		NVARCHAR(4000) = '',
	@P_NO_IMO			NVARCHAR(4000) = '',
	@P_PO_STATE			NVARCHAR(3), -- 001 -> 전체, 002 -> 미발주, 003 -> 발주
	@P_IN_STATE			NVARCHAR(3), -- 001 -> 전체, 002 -> 미입고, 003 -> 입고   
	@P_GIR_STATE		NVARCHAR(3), -- 001 -> 전체, 002 -> 미의뢰, 003 -> 의뢰   
	@P_OUT_STATE		NVARCHAR(3), -- 001 -> 전체, 002 -> 미출고, 003 -> 출고   
	@P_IV_STATE			NVARCHAR(3), -- 001 -> 전체, 002 -> 미매출, 003 -> 매출
	@P_CLOSE_STATE		NVARCHAR(3), -- 001 -> 전체, 002 -> 미마감, 003 -> 마감
	@P_ID_LOG			NVARCHAR(15) = '',
	@P_YN_AUTO_SUBMIT	NVARCHAR(1) = 'N',
	@P_YN_EX_CHARGE		NVARCHAR(1) = 'N',
	@P_YN_EX_FREE		NVARCHAR(1) = 'N',
	@P_YN_EX_READY		NVARCHAR(1) = 'N',
	@P_YN_EX_FORWARD	NVARCHAR(1) = 'N',
	@P_YN_URGENT		NVARCHAR(1) = 'N'
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

CREATE TABLE #SA_SOH
(
	CD_COMPANY		NVARCHAR(7),
	NO_SO			NVARCHAR(20),
	NO_PO_PARTNER	NVARCHAR(50),
	DT_SO		    NVARCHAR(8),
	CD_PARTNER		NVARCHAR(20),
	NO_EMP			NVARCHAR(10),
	NO_PROJECT		NVARCHAR(20),
	DC_RMK_TEXT2	NVARCHAR(4000),
	DC_RMK1			NVARCHAR(100),
	NO_DOCU			NVARCHAR(20),
	TP_SO			NVARCHAR(4),
	NO_IMO			NVARCHAR(10),
	CD_SALEGRP		NVARCHAR(7),
	CD_PARTNER_GRP	NVARCHAR(10),
	TP_TRANS		NVARCHAR(3),
	TP_PACKING		NVARCHAR(3),
	CD_EXCH			NVARCHAR(3),
	RT_EXCH			NUMERIC(15, 4),
	COND_PAY		NVARCHAR(3),
	YN_CLOSE		NVARCHAR(1),
	PORT_LOADING	NVARCHAR(50),
	MEMO_CD			NVARCHAR(40),
	CHECK_PEN		NVARCHAR(40),
	DC_RMK_TEXT		TEXT,
	TXT_USERDEF1	NVARCHAR(100),
	TXT_USERDEF3	NVARCHAR(100),
	DT_DLV			NVARCHAR(8),
	CD_DLV_MAIN		NVARCHAR(6),
	CD_DLV_SUB		NVARCHAR(6),
	DC_DLV		    NVARCHAR(1000),
	DT_CONTRACT		NVARCHAR(8)
)

CREATE NONCLUSTERED INDEX SA_SOH ON #SA_SOH (CD_COMPANY, NO_SO)

CREATE TABLE #SA_GIRL
(
	CD_COMPANY		NVARCHAR(7),
	NO_SO			NVARCHAR(20),
	YN_AUTO_SUBMIT	NVARCHAR(1),
	QT_GIR			NUMERIC(17, 4),
	QT_OUT			NUMERIC(17, 4)
)

CREATE NONCLUSTERED INDEX SA_GIRL ON #SA_GIRL (CD_COMPANY, NO_SO)

CREATE TABLE #SA_GIRL1
(
	CD_COMPANY		NVARCHAR(7),
	NO_SO			NVARCHAR(20),
	QT_GIR_RETURN	NUMERIC(17, 4),
	QT_OUT_RETURN	NUMERIC(17, 4)
)

CREATE NONCLUSTERED INDEX SA_GIRL1 ON #SA_GIRL1 (CD_COMPANY, NO_SO)

CREATE TABLE #SA_PACK
(
	CD_COMPANY		NVARCHAR(7),
	NO_SO			NVARCHAR(20),
	QT_GIR_PACK		NUMERIC(17, 4),
	QT_PACK			NUMERIC(17, 4)
)

CREATE NONCLUSTERED INDEX SA_PACK ON #SA_PACK (CD_COMPANY, NO_SO)

SELECT EW.CD_COMPANY,
	   EW.CD_PARTNER,
	   EW.CD_LEVEL
INTO #CZ_SA_EWS
FROM CZ_SA_EWS_LOG EW 
WHERE EW.CD_COMPANY = @P_CD_COMPANY
AND EW.DT_EWS = CONVERT(CHAR(8), GETDATE(), 112)

INSERT INTO #SA_SOH
SELECT SH.CD_COMPANY, 
	   SH.NO_SO,
	   SH.NO_PO_PARTNER,
	   SH.DT_SO,
	   SH.CD_PARTNER,
	   SH.NO_EMP,
	   SH.NO_PROJECT,
	   SH.DC_RMK_TEXT2,
	   SH.DC_RMK1,
	   SH.NO_DOCU,
	   SH.TP_SO,
	   SH.NO_IMO,
	   SH.CD_SALEGRP,
	   SH.CD_PARTNER_GRP,
	   SH.TP_TRANS,
	   SH.TP_PACKING,
	   SH.CD_EXCH,
	   SH.RT_EXCH,
	   SH.COND_PAY,
	   SH.YN_CLOSE,
	   SH.PORT_LOADING,
	   SH.MEMO_CD,
	   SH.CHECK_PEN,
	   SH.DC_RMK_TEXT,
	   SH.TXT_USERDEF1,
	   SH.TXT_USERDEF3,
	   SD.DT_DLV,
	   SD.CD_DLV_MAIN,
	   SD.CD_DLV_SUB,
	   SD.DC_DLV,
	   SH.DT_CONTRACT
FROM SA_SOH SH
LEFT JOIN CZ_SA_SOH_DLV SD ON SD.CD_COMPANY = SH.CD_COMPANY AND SD.NO_SO = SH.NO_SO AND ISNULL(SD.YN_USE, 'N') = 'Y'
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.NO_SO LIKE @P_NO_SO + '%'
AND (@P_NO_SO_PRE = '' OR SH.NO_SO LIKE @P_NO_SO_PRE + '%')
AND (@P_TP_SO = '' OR SH.TP_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_TP_SO)))
AND (@P_CD_PARTNER = '' OR SH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
AND (@P_CD_PARTNER_GRP = '' OR SH.CD_PARTNER_GRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP)))
AND (@P_NO_EMP_MULTI = '' OR SH.NO_EMP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_NO_EMP_MULTI)))
AND (@P_NO_IMO = '' OR SH.NO_IMO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_NO_IMO)))
AND (@P_STA_SO = '' OR SH.STA_SO = @P_STA_SO)
AND (@P_MULTI_SALEGRP = '' OR SH.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEGRP)))
AND (@P_NO_PO_PARTNER = '' OR SH.NO_PO_PARTNER LIKE '%' + @P_NO_PO_PARTNER + '%')
AND (@P_YN_EX_FORWARD = 'N' OR ISNULL(SH.CD_GIR_MAIN, '') <> 'WM001')
AND (ISNULL(@P_YN_URGENT, 'N') = 'N' OR SD.YN_URGENT = @P_YN_URGENT)
AND (@P_YN_EX_READY = 'N' OR NOT EXISTS (SELECT 1 
										 FROM CZ_SA_AUTO_MAIL_PARTNER AM
										 WHERE AM.CD_COMPANY = SH.CD_COMPANY
										 AND AM.CD_PARTNER = SH.CD_PARTNER
										 AND AM.TP_PARTNER = '002'
										 AND AM.YN_READY_INFO = 'Y'))
AND (@P_ID_LOG = '' OR EXISTS (SELECT 1 
							   FROM CZ_MA_WORKFLOWH WH
							   WHERE WH.CD_COMPANY = SH.CD_COMPANY
							   AND WH.NO_KEY = SH.NO_SO
							   AND WH.TP_STEP = '08'
							   AND WH.ID_LOG = @P_ID_LOG))
AND (@P_CLOSE_STATE <> '002' OR SH.YN_CLOSE = 'N' OR SH.YN_CLOSE IS NULL)
AND (@P_CLOSE_STATE <> '003' OR SH.YN_CLOSE = 'Y' AND EXISTS (SELECT 1 
															  FROM FI_GWDOCU GW
															  WHERE GW.CD_COMPANY = 'K100' 
															  AND GW.CD_PC = '010000'
															  AND GW.NO_DOCU = SH.NO_DOCU
															  AND (GW.ST_STAT IS NULL OR GW.ST_STAT IN ('0', '-1', '2', '3'))))
AND (@P_CLOSE_STATE <> '004' OR SH.YN_CLOSE = 'Y' AND EXISTS (SELECT 1 
															  FROM FI_GWDOCU GW
															  WHERE GW.CD_COMPANY = 'K100' 
															  AND GW.CD_PC = '010000' 
															  AND GW.NO_DOCU = SH.NO_DOCU
															  AND GW.ST_STAT = '1'))

INSERT INTO #SA_GIRL
SELECT GL.CD_COMPANY, 
	   GL.NO_SO,
	   MAX(WD.YN_AUTO_SUBMIT) AS YN_AUTO_SUBMIT,
	   SUM(GL.QT_GIR) AS QT_GIR,
	   SUM(OL.QT_IO) AS QT_OUT
FROM SA_GIRL GL
LEFT JOIN CZ_SA_GIRH_WORK_DETAIL WD ON WD.CD_COMPANY = GL.CD_COMPANY AND WD.NO_GIR = GL.NO_GIR
LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
WHERE EXISTS (SELECT 1 
			  FROM #SA_SOH
			  WHERE CD_COMPANY = GL.CD_COMPANY
			  AND NO_SO = GL.NO_SO)
GROUP BY GL.CD_COMPANY, GL.NO_SO

INSERT INTO #SA_GIRL1
SELECT GL.CD_COMPANY, 
	   GL.NO_SO_MGMT AS NO_SO,
	   SUM(-GL.QT_GIR) AS QT_GIR_RETURN,
	   SUM(-OL.QT_IO) AS QT_OUT_RETURN 
FROM SA_GIRL GL
LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
WHERE EXISTS (SELECT 1 
			  FROM #SA_SOH
			  WHERE CD_COMPANY = GL.CD_COMPANY
			  AND NO_SO = GL.NO_SO_MGMT)
GROUP BY GL.CD_COMPANY, GL.NO_SO_MGMT

INSERT INTO #SA_PACK
SELECT PL.CD_COMPANY, PL.NO_SO,
	   SUM(PL.QT_GIR) AS QT_GIR_PACK,
	   SUM(PK.QT_PACK) AS QT_PACK 
FROM CZ_SA_GIRH_PACK PH
LEFT JOIN CZ_SA_GIRL_PACK PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR
LEFT JOIN (SELECT CD_COMPANY, NO_GIR, SEQ_GIR,
				  SUM(QT_PACK) AS QT_PACK 
		   FROM CZ_SA_PACKL
		   GROUP BY CD_COMPANY, NO_GIR, SEQ_GIR) PK 
ON PK.CD_COMPANY = PL.CD_COMPANY AND PK.NO_GIR = PL.NO_GIR AND PK.SEQ_GIR = PL.SEQ_GIR
WHERE EXISTS (SELECT 1 
			  FROM #SA_SOH
			  WHERE CD_COMPANY = PL.CD_COMPANY
			  AND NO_SO = PL.NO_SO)
GROUP BY PL.CD_COMPANY, PL.NO_SO

SELECT 'N' AS S,
	   SH.NO_SO,
	   SH.NO_PO_PARTNER,
	   SH.DT_SO,
	   SH.CD_PARTNER,
	   MP.LN_PARTNER,
	   MH.NO_IMO,
	   MH.NO_HULL,
	   MH.NM_VESSEL,
	   SH.NO_EMP,
	   ME.NM_KOR,
	   SH.NO_PROJECT,
	   PJ.NM_PROJECT,
	   ST.NM_SO,
	   SH.DC_RMK_TEXT2,
	   SH.DC_RMK1,
	   ISNULL(RH.AM_RCP_A, 0) AS AM_RCP_A,
	   SL.DT_PO,
	   (SELECT MAX(DT_EXPECT)
		FROM CZ_SA_DEFERRED_DELIVERY
		WHERE CD_COMPANY = SL.CD_COMPANY
		AND NO_SO = SL.NO_SO
		AND TP_TYPE = '2') AS DT_EXPECT_IN,
	   SL.DT_IN,
	   SL.DT_OUT,
	   SL.DT_IV,
	   RH.DT_RCP,
	   SL.DT_LIMIT,
	   SL.DT_DUEDATE,
	   (SELECT SUM(IH.WEIGHT)
		FROM MM_QTIOH IH
		WHERE IH.CD_COMPANY = SL.CD_COMPANY 
		AND (IH.YN_RETURN = 'N' OR IH.YN_RETURN IS NULL)
		AND IH.WEIGHT > 0
		AND EXISTS(SELECT 1 
				   FROM PU_POL PL
				   JOIN MM_QTIO IL ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
				   WHERE IL.CD_COMPANY = IH.CD_COMPANY
				   AND IL.NO_IO = IH.NO_IO
				   AND PL.NO_SO = SL.NO_SO)) AS WEIGHT,
	   (SELECT FORMAT(SUM((PK.SIZE_X * PK.SIZE_Y * PK.SIZE_Z) / 5000000), '#,##0.##KG')
		FROM CZ_PU_PACKH PK
		WHERE EXISTS (SELECT 1 
		              FROM PU_POH PH 
		              WHERE PH.CD_COMPANY = PK.CD_COMPANY 
		              AND PH.NO_PO = PK.NO_PO
					  AND PH.CD_COMPANY = SH.CD_COMPANY
		              AND PH.CD_PJT = SH.NO_SO)) AS VOLUME_WEIGHT,
	   MC.CD_CC,
	   MC.NM_CC,
	   CD3.NM_SYSDEF AS NM_EXCH,
	   SH.RT_EXCH,
	   ISNULL(CASE WHEN ST.RET = 'Y' THEN -SL.AM_EX_SO ELSE SL.AM_EX_SO END, 0) AS AM_EX_SO,
	   ISNULL(CASE WHEN ST.RET = 'Y' THEN -SL.AM_SO ELSE SL.AM_SO END, 0) AS AM_SO,
	   (CASE WHEN ISNULL(CD5.CD_FLAG1, '') <> '' THEN 'Y' ELSE 'N' END) AS YN_PROFORMA,
	   CD.NM_SYSDEF AS NM_PARTNER_GRP,
	   CD1.NM_SYSDEF AS NM_NATION,
	   SL.YN_PACK,
	   ISNULL(SH.YN_CLOSE, 'N') AS YN_CLOSE,
	   GW.ST_STAT,
	   (CD2.NM_SYSDEF + ' ' + SH.PORT_LOADING) AS NM_TP_TRANSPORT,
	   SL.NM_SUBJECT,
	   GL.YN_AUTO_SUBMIT,
	   SH.MEMO_CD,
	   SH.CHECK_PEN,
	   ISNULL(CASE WHEN ST.RET = 'Y' THEN -SL.QT_SO ELSE SL.QT_SO END, 0) AS QT_SO,
	   ISNULL(SL.QT_STOCK, 0) AS QT_STOCK,
	   ISNULL(SL.QT_PO, 0) AS QT_PO,
	   ISNULL(SL.QT_BOOK, 0) AS QT_BOOK,
	   ISNULL(SL.QT_REQ, 0) AS QT_REQ,
	   ISNULL(SL.QT_REQ_RETURN, 0) AS QT_REQ_RETURN,
	   ISNULL(SL.QT_IN, 0) AS QT_IN,
	   ISNULL(SL.QT_IN_RETURN, 0) AS QT_IN_RETURN,
	   ISNULL(CASE WHEN ST.RET = 'Y' THEN -GL.QT_GIR ELSE GL.QT_GIR END, 0) AS QT_GIR,
	   ISNULL(GL1.QT_GIR_RETURN, 0) AS QT_GIR_RETURN,
	   ISNULL(PK.QT_GIR_PACK, 0) AS QT_GIR_PACK,
	   ISNULL(PK.QT_PACK, 0) AS QT_PACK,
	   ((ISNULL(SL.QT_IN, 0) + ISNULL(SL.QT_IN_RETURN, 0)) - ISNULL(GL.QT_GIR, 0)) AS QT_READY,
	   ISNULL(CASE WHEN ST.RET = 'Y' THEN -GL.QT_OUT ELSE GL.QT_OUT END, 0) AS QT_OUT,
	   ISNULL(GL1.QT_OUT_RETURN, 0) AS QT_OUT_RETURN,
	   (ISNULL(SL.QT_SO, 0) - ISNULL(GL.QT_OUT, 0)) AS QT_GI_REMAIN,
	   ISNULL(SL.QT_IV, 0) AS QT_IV,
	   ISNULL(SL.QT_IV_RETURN, 0) AS QT_IV_RETURN,
	   (ISNULL(GL.QT_OUT, 0) - ISNULL(SL.QT_IV, 0)) AS QT_IV_REMAIN,
	   SH.DC_RMK_TEXT,
	   CD4.NM_SYSDEF AS NM_PACKING,
	   (SELECT MU.NM_USER
	    FROM CZ_MA_WORKFLOWH WH
		LEFT JOIN MA_USER MU ON MU.CD_COMPANY = WH.CD_COMPANY AND MU.ID_USER = WH.ID_LOG
		WHERE WH.CD_COMPANY = SH.CD_COMPANY
		AND WH.TP_STEP = '08'
		AND WH.NO_KEY = SH.NO_SO) AS NM_LOG,
	   (SELECT TOP 1 CONVERT(CHAR(19), CONVERT(DATETIME, LEFT(DTS_ETRYPT, 8) + ' ' + SUBSTRING(DTS_ETRYPT, 9, 2) + ':' + SUBSTRING(DTS_ETRYPT, 11, 2) + ':' + SUBSTRING(DTS_ETRYPT, 13, 2)), 20) + ' (' + NM_PRTAG + ')'
	    FROM CZ_SA_VSSL_ETRYNDH
		WHERE ISNULL(CALL_SIGN, '') <> ''
		AND CALL_SIGN = MH.CALL_SIGN
		ORDER BY DTS_ETRYPT DESC) AS DTS_ETRYPT,
	   SH.TXT_USERDEF1,
	   SH.TXT_USERDEF3,
	   DATEDIFF(DAY, SL.DT_DUEDATE, SL.DT_OUT) AS DT_OUT_DIFF,
	   SL.NM_ITEMGRP,
	   RD.QT_READY_INFO,
	   RD.DT_READY_INFO,
	   SH.DT_DLV,
	   SH.CD_DLV_MAIN,
	   SH.CD_DLV_SUB,
	   SH.DC_DLV,
	   (CASE ISNULL(EW.CD_LEVEL, 0) WHEN 0 THEN '정상'
									WHEN 1 THEN '주의요망'
									WHEN 2 THEN '사용불가' END) AS NM_EWS_LEVEL,
       (CASE WHEN WL.NO_KEY IS NULL THEN 'N' ELSE 'Y' END) AS YN_51_FILE,
	   SH.DT_CONTRACT,
	   SL.QT_ITEM,
	   (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_IN, 0)) AS QT_IN_REMAIN
FROM #SA_SOH SH 
JOIN (SELECT SL.CD_COMPANY,
	   		 SL.NO_SO,
	   		 MAX(QL.NM_SUBJECT) AS NM_SUBJECT,
	   		 MAX(QI.DT_PO) AS DT_PO,
	   		 MAX(QI.DT_IO) AS DT_IN,
	   		 MAX(QO.DT_IO) AS DT_OUT,
	   		 MAX(QO.DT_PROCESS) AS DT_IV,
	   		 MAX(QI.DT_LIMIT) AS DT_LIMIT,
	   		 MAX((CASE WHEN SL.LT IN (997, 998, 999) THEN '' ELSE SL.DT_DUEDATE END)) AS DT_DUEDATE,
	   		 MAX(QI.YN_PACK) AS YN_PACK,
	   		 SUM(SL.QT_SO) AS QT_SO,
	   		 SUM(SB.QT_STOCK) AS QT_STOCK,
	   		 SUM(ISNULL(SL.QT_PO, 0) + ISNULL(SB.QT_STOCK, 0)) AS QT_PO,
	   		 SUM(SB.QT_BOOK) AS QT_BOOK,
			 SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM, 0) 
	   		 							   ELSE (ISNULL(QI.QT_REQ, 0) + ISNULL(SB.QT_BOOK, 0)) END) AS QT_REQ,
			 SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM_RETURN, 0) 
	   		 							   ELSE ISNULL(QI1.QT_REQ_RETURN, 0) END) AS QT_REQ_RETURN,
	   		 SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM, 0) 
			 							   ELSE (ISNULL(QI.QT_IO, 0) + ISNULL(SB.QT_BOOK, 0)) END) AS QT_IN,
	   		 SUM(CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(QO.QT_BOM_RETURN, 0) 
			 							   ELSE ISNULL(QI1.QT_IO_RETURN, 0) END) AS QT_IN_RETURN,
	   		 SUM(QO.QT_IV) AS QT_IV,
	   		 SUM(QO.QT_IV_RETURN) AS QT_IV_RETURN,
	   		 SUM(SL.AM_SO) AS AM_EX_SO,
			 SUM(SL.AM_WONAMT) AS AM_SO,
			 MAX(IG.NM_ITEMGRP) AS NM_ITEMGRP,
			 COUNT(1) AS QT_ITEM
	  FROM SA_SOL SL
	  LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
	  LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
	  LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_PLANT = SL.CD_PLANT AND MI.CD_ITEM = SL.CD_ITEM
	  LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = MI.CD_COMPANY AND IG.CD_ITEMGRP = MI.GRP_ITEM
	  LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
						MAX(PH.DT_PO) AS DT_PO,
						MAX(PL.DT_LIMIT) AS DT_LIMIT,
						MAX(IH.DT_IO) AS DT_IO,
						MAX(CASE WHEN ISNULL(IH.DC_RMK, '') = '' THEN 'N' ELSE 'Y' END) AS YN_PACK,
	  		   			SUM(RL.QT_REQ) AS QT_REQ,
		   		        SUM(IL.QT_IO) AS QT_IO
		         FROM PU_POH PH
	             LEFT JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
		         LEFT JOIN PU_RCVL RL ON RL.CD_COMPANY = PL.CD_COMPANY AND RL.NO_PO = PL.NO_PO AND RL.NO_POLINE = PL.NO_LINE
		         LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = RL.CD_COMPANY AND IL.NO_ISURCV = RL.NO_RCV AND IL.NO_ISURCVLINE = RL.NO_LINE
		         LEFT JOIN MM_QTIOH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO
				 WHERE EXISTS (SELECT 1 
							   FROM #SA_SOH
							   WHERE CD_COMPANY = PL.CD_COMPANY
							   AND NO_SO = PL.NO_SO)
	  		     GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) QI 
	  ON QI.CD_COMPANY = SL.CD_COMPANY AND QI.NO_SO = SL.NO_SO AND QI.NO_SOLINE = SL.SEQ_SO
	  LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
				        SUM(-RL.QT_REQ) AS QT_REQ_RETURN,
				        SUM(-IL.QT_IO) AS QT_IO_RETURN
		         FROM PU_POH PH
	             LEFT JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
		         LEFT JOIN PU_RCVL RL ON RL.CD_COMPANY = PL.CD_COMPANY AND RL.NO_PO_MGMT = PL.NO_PO AND RL.NO_POLINE_MGMT = PL.NO_LINE
		         LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = RL.CD_COMPANY AND IL.NO_ISURCV = RL.NO_RCV AND IL.NO_ISURCVLINE = RL.NO_LINE
				 WHERE EXISTS (SELECT 1 
							   FROM #SA_SOH
							   WHERE CD_COMPANY = PL.CD_COMPANY
							   AND NO_SO = PL.NO_SO)
	  		     GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) QI1 
	  ON QI1.CD_COMPANY = SL.CD_COMPANY AND QI1.NO_SO = SL.NO_SO AND QI1.NO_SOLINE = SL.SEQ_SO
	  LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
	  		   			MAX(CASE WHEN OL.CD_QTIOTP IN ('200','240','250','210','241','251') THEN OH.DT_IO ELSE NULL END) AS DT_IO,
						MAX(IH.DT_PROCESS) AS DT_PROCESS,
	  					SUM(CASE WHEN OH.YN_RETURN <> 'Y' AND OL.FG_PS = '1' AND OL.FG_IO = '002' THEN OL.QT_IO  ELSE 0 END) AS QT_BOM,
	  					SUM(CASE WHEN OH.YN_RETURN = 'Y' AND OL.FG_PS = '1' AND OL.FG_IO = '002' THEN -OL.QT_IO ELSE 0 END) AS QT_BOM_RETURN,
						SUM(CASE WHEN IL.YN_RETURN <> 'Y' THEN IL.QT_CLS ELSE 0 END) AS QT_IV,
	  				    SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.QT_CLS ELSE 0 END) AS QT_IV_RETURN
	  			 FROM MM_QTIOH OH
	  			 LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
				 LEFT JOIN SA_IVL IL ON IL.CD_COMPANY = OL.CD_COMPANY AND IL.NO_IO = OL.NO_IO AND IL.NO_IOLINE = OL.NO_IOLINE AND IL.CD_ITEM = OL.CD_ITEM
				 LEFT JOIN SA_IVH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IV = IL.NO_IV
				 WHERE EXISTS (SELECT 1 
							   FROM #SA_SOH
							   WHERE CD_COMPANY = OL.CD_COMPANY
							   AND NO_SO = OL.NO_PSO_MGMT)
	  			 GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) QO 
	  ON QO.CD_COMPANY = SL.CD_COMPANY AND QO.NO_PSO_MGMT = SL.NO_SO AND QO.NO_PSOLINE_MGMT = SL.SEQ_SO
	  WHERE EXISTS (SELECT 1 
				    FROM #SA_SOH
				    WHERE CD_COMPANY = SL.CD_COMPANY
				    AND NO_SO = SL.NO_SO)
	  AND (@P_TP_BUSI = '' OR SL.TP_BUSI = @P_TP_BUSI)
	  AND (@P_YN_EX_CHARGE = 'N' OR SL.CD_ITEM NOT LIKE 'SD%')
	  AND (@P_MULTI_GRP_MFG = '' OR EXISTS (SELECT 1 
										    FROM MA_PITEM MI 
										    WHERE MI.CD_COMPANY = SL.CD_COMPANY 
										    AND MI.CD_PLANT = SL.CD_PLANT 
										    AND MI.CD_ITEM = SL.CD_ITEM
										    AND MI.GRP_MFG IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_GRP_MFG))))
	  AND (@P_YN_EX_FREE = 'N' OR (@P_YN_EX_FREE = 'Y' AND EXISTS (SELECT 1 
														           FROM MM_EJTP ET 
														           WHERE ET.CD_COMPANY = SL.CD_COMPANY 
														           AND ET.CD_QTIOTP = SL.TP_GI
														           AND ET.YN_AM = 'Y')))
	  AND (@P_CD_SUPPLIER = '' OR EXISTS (SELECT 1 
										  FROM PU_POL PL
										  LEFT JOIN PU_POH PH ON PH.CD_COMPANY = PL.CD_COMPANY AND PL.NO_PO = PH.NO_PO
										  WHERE PL.CD_COMPANY = SL.CD_COMPANY
										  AND PL.NO_SO = SL.NO_SO
										  AND PL.NO_SOLINE = SL.SEQ_SO
										  AND PH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SUPPLIER))) OR (SB.CD_COMPANY IS NOT NULL AND EXISTS (SELECT 1 
																																							     FROM GETTABLEFROMSPLIT(@P_CD_SUPPLIER)
																																								 WHERE CD_STR = 'STOCK')))
	  GROUP BY SL.CD_COMPANY, SL.NO_SO) SL 
ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN (SELECT A.CD_COMPANY, A.NO_SO,
		          COUNT(1) AS QT_READY_INFO,
		          MIN(A.DT_SEND) AS DT_READY_INFO
		   FROM (SELECT CD_COMPANY, NO_SO, DT_SEND 
				 FROM CZ_SA_AUTO_MAIL_PACK_LOG
				 UNION ALL
				 SELECT CD_COMPANY, NO_SO, DT_SEND 
				 FROM CZ_SA_AUTO_MAIL_SA_READY_LOG) A
		   GROUP BY A.CD_COMPANY, A.NO_SO) RD
ON RD.CD_COMPANY = SH.CD_COMPANY AND RD.NO_SO = SH.NO_SO
LEFT JOIN #SA_GIRL GL ON GL.CD_COMPANY = SH.CD_COMPANY AND GL.NO_SO = SH.NO_SO
LEFT JOIN #SA_GIRL1 GL1 ON GL1.CD_COMPANY = SH.CD_COMPANY AND GL1.NO_SO = SH.NO_SO
LEFT JOIN #SA_PACK PK ON PK.CD_COMPANY = SH.CD_COMPANY AND PK.NO_SO = SH.NO_SO
LEFT JOIN (SELECT CD_COMPANY, NO_PROJECT,
				  MAX(DT_RCP) AS DT_RCP,
				  SUM(AM_RCP_A) AS AM_RCP_A
	       FROM SA_RCPH 
		   GROUP BY CD_COMPANY, NO_PROJECT) RH
ON RH.CD_COMPANY = SH.CD_COMPANY AND RH.NO_PROJECT = SH.NO_PROJECT
LEFT JOIN (SELECT WL.CD_COMPANY, WL.NO_KEY
		   FROM CZ_MA_WORKFLOWL WL
		   WHERE WL.TP_STEP = '51'
		   GROUP BY WL.CD_COMPANY, WL.NO_KEY) WL
ON WL.CD_COMPANY = SH.CD_COMPANY AND WL.NO_KEY = SH.NO_SO
LEFT JOIN FI_GWDOCU GW ON GW.CD_COMPANY = 'K100' AND GW.CD_PC = '010000' AND GW.NO_DOCU = SH.NO_DOCU
LEFT JOIN SA_TPSO ST ON ST.CD_COMPANY = SH.CD_COMPANY AND ST.TP_SO = SH.TP_SO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
LEFT JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = SH.CD_COMPANY AND PJ.NO_PROJECT = SH.NO_PROJECT
LEFT JOIN MA_SALEGRP MS ON MS.CD_COMPANY = SH.CD_COMPANY AND MS.CD_SALEGRP = SH.CD_SALEGRP
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = MS.CD_COMPANY AND MC.CD_CC = MS.CD_CC
LEFT JOIN #CZ_SA_EWS EW ON EW.CD_COMPANY = SH.CD_COMPANY AND EW.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = SH.CD_COMPANY AND CD.CD_FIELD = 'MA_B000065' AND CD.CD_SYSDEF = SH.CD_PARTNER_GRP
LEFT JOIN MA_CODEDTL CD1 ON CD1.CD_COMPANY = MP.CD_COMPANY AND CD1.CD_FIELD = 'MA_B000020' AND CD1.CD_SYSDEF = MP.CD_NATION
LEFT JOIN MA_CODEDTL CD2 ON CD2.CD_COMPANY = SH.CD_COMPANY AND CD2.CD_FIELD = 'TR_IM00003' AND CD2.CD_SYSDEF = SH.TP_TRANS
LEFT JOIN MA_CODEDTL CD3 ON CD3.CD_COMPANY = SH.CD_COMPANY AND CD3.CD_FIELD = 'MA_B000005' AND CD3.CD_SYSDEF = SH.CD_EXCH
LEFT JOIN MA_CODEDTL CD4 ON CD4.CD_COMPANY = SH.CD_COMPANY AND CD4.CD_FIELD = 'TR_IM00011' AND CD4.CD_SYSDEF = SH.TP_PACKING
LEFT JOIN MA_CODEDTL CD5 ON CD5.CD_COMPANY = SH.CD_COMPANY AND CD5.CD_FIELD = 'CZ_SA00013' AND CD5.CD_SYSDEF = SH.COND_PAY
WHERE 1=1
AND (@P_YN_AUTO_SUBMIT = 'N' OR GL.YN_AUTO_SUBMIT = 'Y')
AND (@P_PO_STATE <> '002' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_PO, 0)) > 0)
AND (@P_PO_STATE <> '003' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_PO, 0)) = 0)
AND (@P_IN_STATE <> '002' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_IN, 0)) > 0)
AND (@P_IN_STATE <> '003' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_IN, 0)) = 0)
AND (@P_GIR_STATE <> '002' OR (ISNULL(SL.QT_SO, 0) - ISNULL(GL.QT_GIR, 0)) > 0)
AND (@P_GIR_STATE <> '003' OR (ISNULL(SL.QT_SO, 0) - ISNULL(GL.QT_GIR, 0)) = 0)
AND (@P_OUT_STATE <> '002' OR (ISNULL(SL.QT_SO, 0) - ISNULL(GL.QT_OUT, 0)) > 0)
AND (@P_OUT_STATE <> '003' OR (ISNULL(SL.QT_SO, 0) - ISNULL(GL.QT_OUT, 0)) = 0)
AND (@P_IV_STATE <> '002' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_IV, 0)) > 0)
AND (@P_IV_STATE <> '003' OR (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_IV, 0)) = 0)

DROP TABLE #SA_SOH
DROP TABLE #SA_GIRL
DROP TABLE #SA_GIRL1
DROP TABLE #SA_PACK
DROP TABLE #CZ_SA_EWS

GO

