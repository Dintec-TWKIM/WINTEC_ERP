USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_MONTHLYRPT_PIVOT_S]    Script Date: 2015-06-01 오전 10:19:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System : 구매
**  Sub System : 발주
**  Page  : 구매요청현황(피봇)
**  Desc  :  구매요청현황 라인부분
**  Return Values
**
**  작    성    자  :
**  작    성    일  :
**  수    정    자  :
*********************************************
** Change History
** 2013.02.28 : D20130225211 : 발주현황(피봇) 개발
*********************************************/
ALTER PROCEDURE [NEOE].[SP_CZ_SA_MONTHLYRPT_PIVOT_S]
(
	@P_CD_COMPANY	    NVARCHAR(7),
	@P_YM				NVARCHAR(6),
    @P_TP_PLAN			NVARCHAR(1),
	@P_YN_EXCLUDE_FREE	NVARCHAR(1),
	@P_YN_CHARGE		NVARCHAR(1),
	@P_TP_QTN			NVARCHAR(3)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @T_YEAR			NVARCHAR(4)
DECLARE @T_MONTH		NVARCHAR(2)

SET @T_YEAR = LEFT(@P_YM, 4)
SET @T_MONTH = RIGHT(@P_YM, 2);

WITH PC
AS
(
	SELECT CD_COMPANY,
		   TP_PLAN,
		   CD_KEY,
		   AM_TOTWON AS AM_YEARWON,
		   (AM_TOT_JAN + AM_TOT_FEB + AM_TOT_MAR + AM_TOT_APR + AM_TOT_MAY + AM_TOT_JUN) AS AM_HALF_YEAR1,
		   (AM_TOT_JUL + AM_TOT_AUG + AM_TOT_SEP + AM_TOT_OCT + AM_TOT_NOV + AM_TOT_DEC) AS AM_HALF_YEAR2,
		   (CASE WHEN @T_MONTH = '01' THEN AM_TOT_JAN 
		         WHEN @T_MONTH = '02' THEN AM_TOT_FEB
		         WHEN @T_MONTH = '03' THEN AM_TOT_MAR
		         WHEN @T_MONTH = '04' THEN AM_TOT_APR
		         WHEN @T_MONTH = '05' THEN AM_TOT_MAY
		         WHEN @T_MONTH = '06' THEN AM_TOT_JUN
		         WHEN @T_MONTH = '07' THEN AM_TOT_JUL
		         WHEN @T_MONTH = '08' THEN AM_TOT_AUG
		         WHEN @T_MONTH = '09' THEN AM_TOT_SEP
		         WHEN @T_MONTH = '10' THEN AM_TOT_OCT
		         WHEN @T_MONTH = '11' THEN AM_TOT_NOV
		         WHEN @T_MONTH = '12' THEN AM_TOT_DEC END) AS AM_MONTHWON
	FROM CZ_SA_PTR_PLAN
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND YY_PLAN = @T_YEAR
)
SELECT SH.NO_SO,
	   LEFT(SH.DT_CONTRACT, 6) AS DT_SO,
	   ST.NM_SO AS TP_SO,
	   ME.NM_KOR AS NM_EMP,
	   ME1.NM_KOR AS NM_EMP_QTN,
	   ME2.NM_KOR AS NM_EMP_STK,
	   MS.CD_SALEGRP,
	   MS.NM_SALEGRP,
	   SO.CD_SALEORG,
	   SO.NM_SALEORG,
	   MS.CD_CC,
	   MC.NM_CC,
	   MP.CD_PARTNER,
	   MP.LN_PARTNER AS NM_PARTNER,
	   SH.CD_PARTNER_GRP,
	   CD.NM_SYSDEF AS NM_PARTNER_GRP,
	   (CASE WHEN @P_TP_QTN = '000' THEN PL.NM_SUPPLIER ELSE (SELECT MP.LN_PARTNER 
															  FROM CZ_MA_WORKFLOWH WH
															  LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = WH.CD_COMPANY AND MP.CD_PARTNER = WH.ID_LOG
															  WHERE WH.CD_COMPANY = SH.CD_COMPANY
															  AND WH.NO_KEY = SH.NO_SO
															  AND WH.TP_STEP = '21') END) AS NM_SUPPLIER,
	   (CASE WHEN LEFT(SH.DT_CONTRACT, 6) = @P_YM THEN (CASE WHEN ST.RET = 'Y' THEN -SL.AM_SO ELSE SL.AM_SO END) ELSE 0 END) AS AM_SO,
	   (CASE WHEN LEFT(SH.DT_CONTRACT, 6) = @P_YM THEN (ISNULL(SL.AM_STOCK, 0) + ISNULL(PL.AM_PO, 0)) ELSE 0 END) AS AM_PO,
	   (CASE WHEN DATEPART(MONTH, SH.DT_CONTRACT) BETWEEN 1 AND 6 THEN (CASE WHEN ST.RET = 'Y' THEN -SL.AM_SO ELSE SL.AM_SO END) ELSE 0 END) AS AM_SO_HALF_YAER1,
	   (CASE WHEN DATEPART(MONTH, SH.DT_CONTRACT) BETWEEN 1 AND 6 THEN (ISNULL(SL.AM_STOCK, 0) + ISNULL(PL.AM_PO, 0)) ELSE 0 END) AS AM_PO_HALF_YEAR1,
	   (CASE WHEN DATEPART(MONTH, SH.DT_CONTRACT) BETWEEN 1 AND 6 THEN 0 ELSE (CASE WHEN ST.RET = 'Y' THEN -SL.AM_SO ELSE SL.AM_SO END) END) AS AM_SO_HALF_YAER2,
	   (CASE WHEN DATEPART(MONTH, SH.DT_CONTRACT) BETWEEN 1 AND 6 THEN 0 ELSE (ISNULL(SL.AM_STOCK, 0) + ISNULL(PL.AM_PO, 0)) END) AS AM_PO_HALF_YEAR2,
	   (CASE WHEN ST.RET = 'Y' THEN -SL.AM_SO ELSE SL.AM_SO END) AS AM_SO_YEAR,
	   (ISNULL(SL.AM_STOCK, 0) + ISNULL(PL.AM_PO, 0)) AS AM_PO_YEAR,
	   PC.AM_MONTHWON,
	   PC.AM_HALF_YEAR1,
	   PC.AM_HALF_YEAR2,
	   PC.AM_YEARWON
FROM SA_SOH SH
JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
			 SUM(CASE WHEN (ISNULL(@P_YN_EXCLUDE_FREE, 'N') = 'N' OR (ISNULL(EJ.YN_AM, 'N') = 'Y' OR SL.TP_GI IN ('250', '251'))) THEN SL.AM_WONAMT ELSE 0 END) AS AM_SO,
	         SUM(ISNULL(SB.UM_KR * SB.QT_STOCK, 0)) AS AM_STOCK
	  FROM SA_SOL SL
	  LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
	  LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_PLANT = SL.CD_PLANT AND MI.CD_ITEM = SL.CD_ITEM
	  LEFT JOIN MM_EJTP EJ ON EJ.CD_COMPANY = SL.CD_COMPANY AND EJ.CD_QTIOTP = SL.TP_GI
	  WHERE (ISNULL(@P_YN_CHARGE, 'N') = 'Y' OR ISNULL(MI.CLS_ITEM, '') <> '010')
	  GROUP BY SL.CD_COMPANY, SL.NO_SO) SL 
ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO,
		          MAX(MP.LN_PARTNER) AS NM_SUPPLIER,
		          SUM(PL.AM) AS AM_PO
		   FROM PU_POH PH
		   LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
		   LEFT JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
		   LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = PL.CD_COMPANY AND MI.CD_PLANT = PL.CD_PLANT AND MI.CD_ITEM = PL.CD_ITEM
		   WHERE (ISNULL(@P_YN_CHARGE, 'N') = 'Y' OR ISNULL(MI.CLS_ITEM, '') <> '010')
		   AND (ISNULL(@P_YN_EXCLUDE_FREE, 'N') = 'N' OR EXISTS (SELECT 1 
		   										                 FROM MM_EJTP EJ 
		   									                     WHERE EJ.CD_COMPANY = PL.CD_COMPANY 
		   										                 AND EJ.CD_QTIOTP = PL.FG_RCV 
		   										                 AND ISNULL(EJ.YN_AM, 'N') = 'Y'))
		   GROUP BY PL.CD_COMPANY, PL.NO_SO) PL
ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO
LEFT JOIN SA_TPSO ST ON ST.CD_COMPANY = SH.CD_COMPANY AND ST.TP_SO = SH.TP_SO
LEFT JOIN MA_SALEGRP MS ON MS.CD_COMPANY = SH.CD_COMPANY AND MS.CD_SALEGRP = (CASE WHEN @P_TP_QTN = '000' THEN SH.CD_SALEGRP ELSE '010900' END)
LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = MS.CD_COMPANY AND SO.CD_SALEORG = MS.CD_SALEORG
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = MS.CD_COMPANY AND MC.CD_CC = MS.CD_CC
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = MP.CD_COMPANY AND CD.CD_FIELD = 'MA_B000065' AND CD.CD_SYSDEF = SH.CD_PARTNER_GRP
LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = SH.CD_COMPANY AND QH.NO_FILE = SH.NO_SO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
LEFT JOIN MA_EMP ME1 ON ME1.CD_COMPANY = QH.CD_COMPANY AND ME1.NO_EMP = QH.NO_EMP_QTN
LEFT JOIN MA_EMP ME2 ON ME2.CD_COMPANY = QH.CD_COMPANY AND ME2.NO_EMP = QH.NO_EMP_STK
LEFT JOIN PC ON PC.CD_COMPANY = SH.CD_COMPANY AND PC.TP_PLAN = @P_TP_PLAN AND PC.CD_KEY = (CASE WHEN @P_TP_PLAN = '0' THEN MS.CD_CC
																			                    WHEN @P_TP_PLAN = '1' THEN SH.CD_PARTNER_GRP
																			                    WHEN @P_TP_PLAN = '2' THEN MS.CD_SALEGRP
																			                    WHEN @P_TP_PLAN = '3' THEN MP.CD_PARTNER END)
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.DT_CONTRACT BETWEEN @T_YEAR + '0101' AND @T_YEAR + '1231'
AND (LEFT(SH.NO_SO, 3) = 'ENG' OR EXISTS (SELECT 1 
                                              FROM MA_CODEDTL MD1 
                                              WHERE MD1.CD_COMPANY = SH.CD_COMPANY 
                                              AND MD1.CD_FIELD = 'CZ_SA00023' 
                                              AND MD1.CD_SYSDEF NOT IN ('00', 'ZZ', 'T-', 'CL', 'CN', 'CS') 
                                              AND MD1.CD_SYSDEF = LEFT(SH.NO_SO, 2)))
AND (@P_TP_QTN = '000' OR EXISTS (SELECT 1 
								  FROM MA_EMP ME
								  WHERE ME.CD_COMPANY = QH.CD_COMPANY
								  AND ME.NO_EMP = (CASE WHEN @P_TP_QTN = '001' THEN QH.NO_EMP_STK ELSE QH.NO_EMP_QTN END)
								  AND ME.CD_DEPT = '010900'))
UNION ALL
SELECT NULL AS NO_SO,
	   NULL AS DT_SO,
	   NULL AS TP_SO,
	   NULL AS NM_EMP,
	   NULL AS NM_EMP_QTN,
	   NULL AS NM_EMP_STK,
	   (CASE WHEN MS.NM_SALEGRP IS NOT NULL THEN PC.CD_KEY ELSE NULL END) AS CD_SALEGRP,
	   MS.NM_SALEGRP,
	   SO.CD_SALEORG AS CD_SALEORG,
	   SO.NM_SALEORG AS NM_SALEORG,
	   (CASE WHEN MC.NM_CC IS NOT NULL THEN PC.CD_KEY ELSE NULL END) AS CD_CC,
	   MC.NM_CC,
	   (CASE WHEN MP.LN_PARTNER IS NOT NULL THEN PC.CD_KEY ELSE NULL END) AS CD_PARTNER,
	   MP.LN_PARTNER AS NM_PARTNER,
	   (CASE WHEN CD.NM_SYSDEF IS NOT NULL THEN PC.CD_KEY ELSE NULL END) AS CD_PARTNER_GRP,
	   CD.NM_SYSDEF AS NM_PARTNER_GRP,
	   NULL AS NM_SUPPLIER,
	   NULL AS AM_SO,
	   NULL AS AM_PO,
	   NULL AS AM_SO_HALF_YAER1,
	   NULL AS AM_PO_HALF_YEAR1,
	   NULL AS AM_SO_HALF_YAER2,
	   NULL AS AM_PO_HALF_YEAR2,
	   NULL AS AM_SO_YEAR,
	   NULL AS AM_PO_YEAR,
	   PC.AM_MONTHWON,
	   PC.AM_HALF_YEAR1,
	   PC.AM_HALF_YEAR2,
	   PC.AM_YEARWON 
FROM PC
LEFT JOIN MA_SALEGRP MS ON MS.CD_COMPANY = PC.CD_COMPANY AND MS.CD_SALEGRP = PC.CD_KEY
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = PC.CD_COMPANY AND MC.CD_CC = PC.CD_KEY
LEFT JOIN MA_SALEGRP MS1 ON MS1.CD_COMPANY = MC.CD_COMPANY AND MS1.CD_CC = MC.CD_CC AND MS1.USE_YN = 'Y'
LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = MS1.CD_COMPANY AND SO.CD_SALEORG = MS1.CD_SALEORG
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PC.CD_COMPANY AND MP.CD_PARTNER = PC.CD_KEY
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = PC.CD_COMPANY AND CD.CD_FIELD = 'MA_B000065' AND CD.CD_SYSDEF = PC.CD_KEY
WHERE PC.CD_COMPANY = @P_CD_COMPANY
AND PC.TP_PLAN = @P_TP_PLAN
AND @P_TP_QTN = '000'


GO