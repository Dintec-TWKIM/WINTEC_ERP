USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CLAIML_S]    Script Date: 2015-04-14 오전 8:32:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_SA_CLAIML_S]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_CLAIM			NVARCHAR(16),
	@P_CD_SUPPLIER		NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @P_AM_FSIZE NUMERIC(3, 0)   -- 소수점    

IF @P_CD_COMPANY = 'S100'
	SET @P_AM_FSIZE = 2
ELSE
	SET @P_AM_FSIZE = 0
	 
SELECT CL.NO_CLAIM,
       CL.NO_SO,
	   CL.SEQ_SO,
	   QL.NO_DSP,
	   CL.CD_ITEM,
	   MI.NM_ITEM,
	   QL.NM_SUBJECT,
	   QL.CD_ITEM_PARTNER,
	   QL.NM_ITEM_PARTNER,
	   SL.QT_SO,
	   CL.QT_CLAIM,
	   ISNULL(OL.QT_IO, 0) AS QT_OUT_RETURN,
	   ISNULL(IL.QT_IO, 0) AS QT_IN_RETURN,
	   ISNULL(IL1.QT_OUT, 0) AS QT_IO_OUT,
	   ISNULL(IL1.QT_IN, 0) AS QT_IO_IN,
	   IL1.CD_ITEM AS CD_ITEM_CHANGE,
	   SL.UNIT_SO,
	   MI.CLS_ITEM,
	   MC.NM_SYSDEF AS NM_CLS_ITEM,
	   CL.CD_SUPPLIER,
	   MP.LN_PARTNER,
	   ISNULL(PL.UM_PO, 0) AS UM_PO,
	   ISNULL(PL.AM_PO, 0) AS AM_PO,
	   ISNULL(SB.UM_KR, 0) AS UM_STOCK,
	   ISNULL(SB.AM_KR, 0) AS AM_STOCK,
	   ISNULL(SL.UM_SO, 0) AS UM_SO,
	   ISNULL(SL.AM_SO, 0) AS AM_SO,
	   ISNULL(CONVERT(NVARCHAR, PL.LT), SB.LT) AS LT,
	   (ISNULL(CL.QT_CLAIM, 0) * ISNULL(SL.UM_SO, 0)) AS AM_CLAIM,
	   (ISNULL(CL.QT_CLAIM, 0) * ISNULL(PL.UM_PO, SB.UM_KR)) AS AM_CLAIM_PO,
	   (ISNULL(CL.QT_CLAIM, 0) * (ISNULL(SL.UM_SO, 0) - ISNULL(PL.UM_PO, SB.UM_KR))) AS AM_PROFIT,
	   (CASE WHEN (ISNULL(CL.QT_CLAIM, 0) * ISNULL(SL.UM_SO, 0)) = 0 THEN 0
																     ELSE ROUND(((ISNULL(CL.QT_CLAIM, 0) * (ISNULL(SL.UM_SO, 0) - ISNULL(PL.UM_PO, SB.UM_KR))) / (ISNULL(CL.QT_CLAIM, 0) * ISNULL(SL.UM_SO, 0))) * 100, 2) END) AS RT_PROFIT,
	   IL2.DT_IO AS DT_OUT
FROM CZ_SA_CLAIML CL
LEFT JOIN (SELECT SL.CD_COMPANY, SL.NO_SO, SL.SEQ_SO,
				  SL.UNIT_SO,
				  SL.QT_SO,
				  ISNULL(SL.UM_KR_S, ROUND(SL.UM_SO * SH.RT_EXCH, @P_AM_FSIZE)) AS UM_SO,
				  ISNULL(SL.AM_KR_S, SL.AM_WONAMT) AS AM_SO		   
		   FROM SA_SOL SL
		   LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO) SL
ON SL.CD_COMPANY = CL.CD_COMPANY AND SL.NO_SO = CL.NO_SO AND SL.SEQ_SO = CL.SEQ_SO
LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE, 
				  SUM(PL.UM) AS UM_PO,
				  SUM(PL.AM) AS AM_PO,
				  MAX(PL.LT) AS LT 
		   FROM PU_POL PL
		   GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
LEFT JOIN (SELECT CD_COMPANY, NO_FILE, NO_LINE, 
				  ISNULL(UM_KR, 0) AS UM_KR,
				  ISNULL(AM_KR, 0) AS AM_KR,
				  'STOCK' AS LT 
		   FROM CZ_SA_STOCK_BOOK) SB
ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = CL.CD_COMPANY AND MI.CD_ITEM = CL.CD_ITEM
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = CL.CD_COMPANY AND MP.CD_PARTNER = CL.CD_SUPPLIER
LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
				  SUM(OL.QT_IO) AS QT_IO 
		   FROM MM_QTIO OL
		   LEFT JOIN MM_QTIOH OH ON OH.CD_COMPANY = OL.CD_COMPANY AND OH.NO_IO = OL.NO_IO
		   WHERE OH.YN_RETURN = 'Y'
		   AND OL.FG_PS = '2'
		   GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) OL
ON OL.CD_COMPANY = SL.CD_COMPANY AND OL.NO_PSO_MGMT = SL.NO_SO AND OL.NO_PSOLINE_MGMT = SL.SEQ_SO
LEFT JOIN (SELECT IL.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT,
				  SUM(IL.QT_IO) AS QT_IO 
		   FROM MM_QTIO IL
		   LEFT JOIN MM_QTIOH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO
		   WHERE IH.YN_RETURN = 'Y'
		   AND IL.FG_PS = '1'
		   GROUP BY IL.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT) IL
ON IL.CD_COMPANY = SL.CD_COMPANY AND IL.NO_PSO_MGMT = SL.NO_SO AND IL.NO_PSOLINE_MGMT = SL.SEQ_SO
LEFT JOIN (SELECT IL.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT,
				  MAX(IL2.CD_ITEM) AS CD_ITEM,
		   		  SUM(IL1.QT_IO) AS QT_OUT,
		   		  SUM(IL2.QT_IO) AS QT_IN
		   FROM MM_QTIO IL
		   JOIN MM_QTIO IL1 ON IL1.CD_COMPANY = IL.CD_COMPANY AND IL1.NO_IO_MGMT = IL.NO_IO AND IL1.NO_IOLINE_MGMT = IL.NO_IOLINE AND IL1.CD_QTIOTP = '400'
		   JOIN MM_QTIO IL2 ON IL2.CD_COMPANY = IL1.CD_COMPANY AND IL2.NO_IO_MGMT = IL1.NO_IO AND IL2.NO_IOLINE_MGMT = IL1.NO_IOLINE AND IL2.CD_QTIOTP = '410'
		   WHERE IL.FG_PS = '2' 
		   GROUP BY IL.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT) IL1 
ON IL1.CD_COMPANY = CL.CD_COMPANY AND IL1.NO_PSO_MGMT = CL.NO_SO AND IL1.NO_PSOLINE_MGMT = CL.SEQ_SO
LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
				  MAX(OH.DT_IO) AS DT_IO 
		   FROM MM_QTIO OL
		   LEFT JOIN MM_QTIOH OH ON OH.CD_COMPANY = OL.CD_COMPANY AND OH.NO_IO = OL.NO_IO
		   WHERE OL.FG_PS = '2'
		   GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) IL2
ON IL2.CD_COMPANY = CL.CD_COMPANY AND IL2.NO_PSO_MGMT = CL.NO_SO AND IL2.NO_PSOLINE_MGMT = CL.SEQ_SO
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = MI.CD_COMPANY AND MC.CD_FIELD = 'MA_B000010' AND MC.CD_SYSDEF = MI.CLS_ITEM
WHERE CL.CD_COMPANY = @P_CD_COMPANY 
AND CL.NO_CLAIM = @P_NO_CLAIM 
AND (ISNULL(@P_CD_SUPPLIER, '') = '' OR CL.CD_SUPPLIER = @P_CD_SUPPLIER)
  
GO

