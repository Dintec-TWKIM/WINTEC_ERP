USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_IV_BASE_S]    Script Date: 2015-10-27 오전 11:48:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_IV_BASE_S]  
(
    @P_CD_COMPANY			NVARCHAR(7),
	@P_TP_DATE			    NVARCHAR(3),
	@P_DT_FROM				NVARCHAR(8),
	@P_DT_TO				NVARCHAR(8),
	@P_CD_CC				NVARCHAR(12),
	@P_CD_PARTNER			NVARCHAR(4000),
	@P_NO_IV				NVARCHAR(20),
	@P_YN_CONFIRM			NVARCHAR(1) = 'N',
	@P_YN_CHARGE			NVARCHAR(1) = 'N',
	@P_YN_SALE_ACCT			NVARCHAR(1) = 'N'
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @P_AM_FSIZE		NUMERIC(3, 0)   -- 소수점    

IF @P_CD_COMPANY = 'S100'
	SET @P_AM_FSIZE = 2
ELSE
	SET @P_AM_FSIZE = 0

SELECT IH.CD_COMPANY,
	   IH.NO_IV,
	   FD.NO_DOCU,
	   IH.CD_PARTNER,
	   MP.LN_PARTNER,
	   IH.DT_PROCESS,
	   FD.DT_ACCT,
	   RH.DT_RCP,
	   IL.CD_CC,
	   MC.NM_CC,
	   ISNULL(IL.AM_SO, 0) AS AM_SO,
	   ISNULL(IL.AM_SA_IV, 0) AS AM_SA_IV,
	   (ISNULL(IL.AM_PO, 0) + ISNULL(IL.AM_STOCK, 0)) AS AM_PO,
	   (ISNULL(IL.AM_PU_IV, 0) + ISNULL(IL.AM_STOCK, 0)) AS AM_PU_IV,
	   ISNULL(IL.AM_CHARGE, 0) AS AM_CHARGE,
	   (CASE WHEN (ISNULL(IL.AM_PO, 0) + ISNULL(IL.AM_STOCK, 0)) > ((ISNULL(IL.AM_PU_IV, 0) + ISNULL(IL.AM_STOCK, 0)) + ISNULL(IL.AM_CHARGE, 0)) THEN (ISNULL(IL.AM_PO, 0) + ISNULL(IL.AM_STOCK, 0)) 
																																				 ELSE ((ISNULL(IL.AM_PU_IV, 0) + ISNULL(IL.AM_STOCK, 0)) + ISNULL(IL.AM_CHARGE, 0)) END) AS AM_SA_IV_BASE,
	   (CASE WHEN (ISNULL(IL.AM_PO, 0) + ISNULL(IL.AM_STOCK, 0)) = 0 AND ISNULL(IL.AM_SA_IV, 0) > 0 THEN ISNULL(PP.AM_PO_PJT, 0) ELSE 0 END) AS AM_PO_PJT,
	   (CASE WHEN ISNULL(IL.AM_SA_IV, 0) = 0 THEN 0
											 ELSE ROUND(((ISNULL(IL.AM_SA_IV, 0) - (CASE WHEN (ISNULL(IL.AM_PO, 0) + ISNULL(IL.AM_STOCK, 0)) > ((ISNULL(IL.AM_PU_IV, 0) + ISNULL(IL.AM_STOCK, 0)) + ISNULL(IL.AM_CHARGE, 0)) THEN (ISNULL(IL.AM_PO, 0) + ISNULL(IL.AM_STOCK, 0)) 
																																																							 ELSE (ISNULL(IL.AM_PU_IV, 0) + ISNULL(IL.AM_STOCK, 0)) END)) / ISNULL(IL.AM_SA_IV, 0)) * 100, 2) END) AS RT_PROFIT,
	   FD.ST_DOCU,
	   NULL AS NO_SO
FROM SA_IVH IH
LEFT JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU,
				  MAX(FD.NO_DOCU) AS NO_DOCU,
				  MAX(FD.DT_ACCT) AS DT_ACCT,
				  MAX(FD.ST_DOCU) AS ST_DOCU
		   FROM FI_DOCU FD
		   LEFT JOIN FI_ACCTCODE FA ON FA.CD_COMPANY = FD.CD_COMPANY AND FA.CD_ACCT = FD.CD_ACCT
		   WHERE (@P_YN_SALE_ACCT = 'N' OR (FA.CD_ACGRP = '510' AND FA.YN_FILL = 'Y'))
		   GROUP BY FD.CD_COMPANY, FD.NO_MDOCU) FD
ON FD.CD_COMPANY = IH.CD_COMPANY AND FD.NO_MDOCU = IH.NO_IV
LEFT JOIN SA_RCPH RH ON RH.CD_COMPANY = IH.CD_COMPANY AND RH.NO_RCP = IH.NO_BAN
JOIN (SELECT IL.CD_COMPANY, IL.NO_IV, IL.CD_CC,
			 SUM((CASE WHEN IL.YN_RETURN = 'Y' THEN -1 ELSE 1 END) * 
			     (CASE WHEN (IL.CD_ITEM LIKE 'SD%' AND ISNULL(IL.QT_CLS, 0) = 0) THEN ISNULL(IL.AM_CLS, 0) 
		  																		 ELSE ISNULL(IL.QT_CLS, 0) * (CASE WHEN QL.NO_FILE IS NULL THEN ROUND(ISNULL(SL.UM_SO, 0) * ISNULL(SH.RT_EXCH, 0), @P_AM_FSIZE) 
		  																																   ELSE ISNULL(SL.UM_KR_S, 0) END) END)) AS AM_SO,
			 SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.AM_CLS ELSE IL.AM_CLS END) AS AM_SA_IV,
			 SUM(CASE WHEN (ISNULL(SL.QT_PO, 0) = 0 OR (IL.CD_ITEM LIKE 'SD%' AND ISNULL(IL.QT_CLS, 0) = 0)) THEN 0 
																											 ELSE ((CASE WHEN IL.YN_RETURN = 'Y' THEN -1 ELSE 1 END) * ISNULL(GL.QT_GIR, 0) * (ISNULL(PL.AM_PO, 0) / ISNULL(SL.QT_PO, 0))) END) AS AM_PO,
			 SUM(CASE WHEN (ISNULL(SL.QT_PO, 0) = 0 OR (IL.CD_ITEM LIKE 'SD%' AND ISNULL(IL.QT_CLS, 0) = 0)) THEN 0 
																											 ELSE ((CASE WHEN IL.YN_RETURN = 'Y' THEN -1 ELSE 1 END) * ISNULL(GL.QT_GIR, 0) * (ISNULL(PL.AM_IV, 0) / ISNULL(SL.QT_PO, 0))) END) AS AM_PU_IV,
			 SUM(CASE WHEN (IL.CD_ITEM LIKE 'SD%' AND ISNULL(IL.QT_CLS, 0) = 0) THEN 0 
																				ELSE ((CASE WHEN IL.YN_RETURN = 'Y' THEN -1 ELSE 1 END) * ISNULL(GL.QT_GIR, 0) * ISNULL(SH.UM_CHARGE, 0)) END) AS AM_CHARGE,
		  	 SUM(CASE WHEN (IL.CD_ITEM LIKE 'SD%' AND ISNULL(IL.QT_CLS, 0) = 0) THEN 0
																				ELSE ((CASE WHEN IL.YN_RETURN = 'Y' THEN -1 ELSE 1 END) * (ISNULL(GL.QT_GIR_STOCK, 0) * ISNULL(SL.UM_STOCK, 0))) END) AS AM_STOCK
	  FROM SA_IVL IL
	  LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = IL.CD_COMPANY AND MI.CD_PLANT = IL.CD_PLANT AND MI.CD_ITEM = IL.CD_ITEM
	  LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = IL.CD_COMPANY AND OL.NO_IO = IL.NO_IO AND OL.NO_IOLINE = IL.NO_IOLINE
	  LEFT JOIN (SELECT CD_COMPANY, NO_GIR, SEQ_GIR,
						(ISNULL(QT_GIR, 0) - ISNULL(QT_GIR_STOCK, 0)) AS QT_GIR,
						ISNULL(QT_GIR_STOCK, 0) AS QT_GIR_STOCK 
			     FROM SA_GIRL) GL 
	  ON GL.CD_COMPANY = OL.CD_COMPANY AND GL.NO_GIR = OL.NO_ISURCV AND GL.SEQ_GIR = OL.NO_ISURCVLINE
	  LEFT JOIN (SELECT SL.CD_COMPANY, SL.NO_SO, SL.SEQ_SO,
						SL.UM_SO,
						SL.UM_KR_S,
						SB.UM_KR AS UM_STOCK,
						(ISNULL(SL.QT_SO, 0) - ISNULL(SB.QT_STOCK, 0)) AS QT_PO 
			     FROM SA_SOL SL
				 LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO) SL 
	  ON SL.CD_COMPANY = IL.CD_COMPANY AND SL.NO_SO = IL.NO_SO AND SL.SEQ_SO = IL.NO_SOLINE
	  LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
			   	  	    SUM(ISNULL(PL.QT_PO, 0) * ISNULL(PL.UM, 0)) AS AM_PO,
			   	  	    SUM(ISNULL(PL.QT_PO, 0) * ISNULL(IL.UM_IV, 0)) AS AM_IV
			   	 FROM PU_POL PL
			   	 LEFT JOIN (SELECT CD_COMPANY, NO_PO, NO_POLINE,
			   	 				   ROUND(SUM(CASE WHEN CD_ITEM NOT LIKE 'SD%' THEN (CASE WHEN YN_RETURN = 'Y' THEN -AM_CLS ELSE AM_CLS END) 
																			  ELSE 0 END) / SUM(CASE WHEN ISNULL(QT_CLS, 0) = 0 THEN 1 ELSE ISNULL(QT_CLS, 0) END), @P_AM_FSIZE) AS UM_IV
			   	 		    FROM PU_IVL
			   	 		    GROUP BY CD_COMPANY, NO_PO, NO_POLINE) IL 
			   	 ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PO = PL.NO_PO AND IL.NO_POLINE = PL.NO_LINE
			   	 GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
	  ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
	  LEFT JOIN (SELECT SH.CD_COMPANY,
						SH.NO_SO,
						SH.RT_EXCH,
						ROUND(CASE WHEN ISNULL(SL.QT_PO, 0) = 0 THEN 0 ELSE ISNULL(PL.AM_CHARGE, 0) / ISNULL(SL.QT_PO, 0) END, @P_AM_FSIZE) AS UM_CHARGE 
				 FROM SA_SOH SH
				 JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
							  SUM(ISNULL(SL.QT_SO, 0) - ISNULL(SB.QT_STOCK, 0)) AS QT_PO  
					   FROM SA_SOL SL
					   LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
					   GROUP BY SL.CD_COMPANY, SL.NO_SO) SL 
				 ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
				 LEFT JOIN (SELECT CD_COMPANY, CD_PJT,
			   					   SUM(AM_CLS) AS AM_CHARGE 
			   				FROM PU_IVL
			   				WHERE CD_ITEM LIKE 'SD%'
			   				GROUP BY CD_COMPANY, CD_PJT) PL
				 ON PL.CD_COMPANY = SH.CD_COMPANY AND PL.CD_PJT = SH.NO_SO) SH 
	  ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO
	  LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
	  WHERE (@P_YN_CHARGE = 'Y' OR (IL.CD_ITEM NOT LIKE 'SD%' OR ISNULL(IL.QT_CLS, 0) <> 0))
	  AND (@P_YN_SALE_ACCT = 'N' OR EXISTS (SELECT 1 
											FROM MA_AISPOSTL AL
											LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = MI.CD_COMPANY AND CD.CD_FIELD = 'MA_B000010' AND CD.CD_SYSDEF = MI.CLS_ITEM
											LEFT JOIN FI_ACCTCODE FA ON FA.CD_COMPANY = AL.CD_COMPANY AND FA.CD_ACCT = AL.CD_ACCT
											WHERE AL.CD_COMPANY = IL.CD_COMPANY 
											AND AL.FG_TP = '100' 
											AND AL.CD_TP = IL.TP_IV 
											AND AL.FG_AIS = CD.CD_FLAG2
											AND FA.CD_ACGRP = '510' 
											AND FA.YN_FILL = 'Y'))
	  GROUP BY IL.CD_COMPANY, IL.NO_IV, IL.CD_CC) IL
ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IV = IH.NO_IV
LEFT JOIN (SELECT IL.CD_COMPANY, IL.NO_IV, IL.CD_CC,
		   		  SUM(ISNULL(PL.AM_PO, 0) + ISNULL(SB.AM_STOCK, 0)) AS AM_PO_PJT
		   FROM (SELECT IL.CD_COMPANY, IL.NO_IV, IL.CD_PJT, IL.CD_CC 
		   		 FROM SA_IVL IL
		   		 LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = IL.CD_COMPANY AND MI.CD_PLANT = IL.CD_PLANT AND MI.CD_ITEM = IL.CD_ITEM
		   		 WHERE ISNULL(IL.CD_PJT, '') <> ''
		   		 AND ((IL.CD_ITEM NOT LIKE 'SD%' AND IL.CD_ITEM NOT LIKE 'ENG%') OR 
		   			  (IL.CD_ITEM LIKE 'SD%' AND MI.CLS_ITEM <> '010' AND MI.CLS_ITEM <> '016') OR
		   			  (IL.CD_ITEM LIKE 'ENG%' AND MI.CLS_ITEM <> '010'))
		   		 GROUP BY IL.CD_COMPANY, IL.NO_IV, IL.CD_PJT, IL.CD_CC) IL
		   LEFT JOIN (SELECT SB.CD_COMPANY, SB.NO_FILE,
		   		   			 SUM(SB.UM_KR * SB.QT_STOCK) AS AM_STOCK
		   			  FROM CZ_SA_STOCK_BOOK SB
		   			  GROUP BY SB.CD_COMPANY, SB.NO_FILE) SB
		   ON SB.CD_COMPANY = IL.CD_COMPANY AND SB.NO_FILE = IL.CD_PJT
		   LEFT JOIN (SELECT PL.CD_COMPANY, PL.CD_PJT,
		   		   			 SUM(PL.AM) AS AM_PO
		   			  FROM PU_POL PL
		   			  GROUP BY PL.CD_COMPANY, PL.CD_PJT) PL
		   ON PL.CD_COMPANY = IL.CD_COMPANY AND PL.CD_PJT = IL.CD_PJT
		   GROUP BY IL.CD_COMPANY, IL.NO_IV, IL.CD_CC) PP
ON PP.CD_COMPANY = IL.CD_COMPANY AND PP.NO_IV = IL.NO_IV AND PP.CD_CC = IL.CD_CC
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = IH.CD_COMPANY AND MP.CD_PARTNER = IH.CD_PARTNER
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = IL.CD_COMPANY AND MC.CD_CC = IL.CD_CC
WHERE IH.CD_COMPANY = @P_CD_COMPANY
AND (ISNULL(@P_NO_IV, '') = '' OR IH.NO_IV = @P_NO_IV)
AND (@P_TP_DATE <> '000' OR (IH.DT_PROCESS BETWEEN @P_DT_FROM AND @P_DT_TO))
AND (@P_TP_DATE <> '001' OR (FD.DT_ACCT BETWEEN @P_DT_FROM AND @P_DT_TO))
AND (@P_TP_DATE <> '002' OR (RH.DT_RCP BETWEEN @P_DT_FROM AND @P_DT_TO))
AND (ISNULL(@P_CD_PARTNER, '') = '' OR IH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
AND (ISNULL(@P_CD_CC, '') = '' OR IL.CD_CC = @P_CD_CC)
AND (@P_YN_CONFIRM = 'N' OR FD.ST_DOCU = '2')
AND (@P_YN_SALE_ACCT = 'N' OR FD.NO_DOCU IS NOT NULL)

GO