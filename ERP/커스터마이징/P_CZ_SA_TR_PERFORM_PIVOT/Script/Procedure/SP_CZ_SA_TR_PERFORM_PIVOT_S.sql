USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_TRADE_PERFORMANCE_PIVOT_S]    Script Date: 2016-05-24 오후 6:39:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_SA_TR_PERFORM_PIVOT_S]
(
	@P_CD_COMPANY	    NVARCHAR(7),
	@P_TP_GUBUN			NVARCHAR(1),
	@P_DT_TYPE			NVARCHAR(3),
	@P_DT_FROM			NVARCHAR(8),
	@P_DT_TO			NVARCHAR(8),
	@P_CD_PARTNER_GRP	NVARCHAR(4000) = '',
	@P_CD_PARTNER_GRP2	NVARCHAR(4000) = '',
	@P_CD_PARTNER		NVARCHAR(4000) = '',
	@P_GRP_ITEM			NVARCHAR(20) = '',
	@P_GRP_MFC			NVARCHAR(4) = '',
	@P_CD_SALEGRP		NVARCHAR(4000) = '',
	@P_NO_EMP			NVARCHAR(20) = '',
	@P_YN_CHARGE		NVARCHAR(1),
	@P_YN_CLAIM			NVARCHAR(1),
	@P_YN_CLOSE			NVARCHAR(1)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

IF @P_TP_GUBUN = '0'
BEGIN
	SELECT QH.CD_COMPANY,
		   QH.NM_KOR,
		   QH.YYYY,
		   QH.LN_PARTNER,
		   QH.LN_PARTNER_SP,
		   QH.NM_VESSEL,
		   QH.NM_PARTNER_GRP,
		   QH.NM_SALEGRP,
		   QH.NM_SALEORG,
		   QH.NM_EXCH_Q,
		   QH.NM_EXCH_S,
		   QH.NM_FG_PARTNER,
		   QL.NM_ITEMGRP,
		   QL.NM_GRP_MFG,
		   QL.NM_CLS_L,
		   QL.NM_CLS_M,
		   QL.NM_CLS_S,
		   SUM(QH.QT_FILE_QTN) AS QT_FILE_QTN,
		   SUM(QH.QT_FILE_SO) AS QT_FILE_SO
	FROM (SELECT QH.CD_COMPANY,
			     QH.NO_FILE,
			     QH.CD_EXCH AS CD_EXCH_Q,
			     SH.CD_EXCH AS CD_EXCH_S,
				 (CASE WHEN ((@P_DT_TYPE = '001' AND QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '002') THEN LEFT(QH.DT_QTN, 4)
					   WHEN ((@P_DT_TYPE = '001' AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '003') THEN LEFT(SH.DT_SO, 4)
					   WHEN ((@P_DT_TYPE = '001' AND SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '004') THEN LEFT(SH.DT_CONTRACT, 4) END) AS YYYY,
			     ME.NM_KOR,
			     MP.LN_PARTNER,
			     ISNULL(QH1.LN_PARTNER, '') AS LN_PARTNER_SP,
			     MH.NM_VESSEL,
			     SG.NM_SALEGRP,
			     SO.NM_SALEORG,
			     MC.NM_SYSDEF AS NM_PARTNER_GRP,
			     MC1.NM_SYSDEF AS NM_EXCH_Q,
			     MC2.NM_SYSDEF AS NM_EXCH_S,
				 1.0 AS QT_FILE_QTN,
				 (CASE WHEN SH.CD_COMPANY IS NULL THEN 0 ELSE 1.0 END) AS QT_FILE_SO,
				 MC3.NM_SYSDEF AS NM_FG_PARTNER
			     FROM CZ_SA_QTNH QH
			     LEFT JOIN (SELECT QH.NO_FILE, 
						 	       MAX(MP.LN_PARTNER) AS LN_PARTNER 
						    FROM CZ_SA_QTNH QH
						 	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
						    WHERE QH.CD_COMPANY = 'S100'
						    GROUP BY QH.NO_FILE) QH1 
				 ON (QH1.NO_FILE = QH.NO_REF OR QH1.NO_FILE = QH.NO_FILE)
				 LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = QH.CD_COMPANY AND SH.NO_SO = QH.NO_FILE
				 LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP
				 LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
				 LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
				 LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = QH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000065' AND MC.CD_SYSDEF = QH.CD_PARTNER_GRP
				 LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = QH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = QH.CD_EXCH
				 LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = SH.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000005' AND MC2.CD_SYSDEF = SH.CD_EXCH
				 LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MP.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000001' AND MC3.CD_SYSDEF = MP.FG_PARTNER
				 LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = SG.CD_COMPANY AND SO.CD_SALEORG = SG.CD_SALEORG
				 WHERE QH.CD_COMPANY = @P_CD_COMPANY
				 AND (((@P_DT_TYPE = '001' OR @P_DT_TYPE = '002') AND (QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
				      ((@P_DT_TYPE = '001' OR @P_DT_TYPE = '003') AND (SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
					  ((@P_DT_TYPE = '001' OR @P_DT_TYPE = '004') AND (SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO)))
				 AND (ISNULL(@P_YN_CLAIM, 'N') = 'Y' OR QH.NO_FILE NOT LIKE 'CL%')
				 AND (ISNULL(@P_CD_PARTNER, '') = '' OR MP.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
				 AND (ISNULL(@P_CD_PARTNER_GRP, '') = '' OR QH.CD_PARTNER_GRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP)))
				 AND (ISNULL(@P_CD_PARTNER_GRP2, '') = '' OR MP.CD_PARTNER_GRP_2 IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP2)))
				 AND (ISNULL(@P_CD_SALEGRP, '') = '' OR SG.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
				 AND (ISNULL(@P_NO_EMP, '') = '' OR ME.NO_EMP = @P_NO_EMP)
				 AND (@P_YN_CLOSE = 'A' OR
					 (@P_YN_CLOSE = 'Y' AND ISNULL(QH.YN_CLOSE, 'N') = 'Y') OR 
					 (@P_YN_CLOSE = 'N' AND ISNULL(QH.YN_CLOSE, 'N') <> 'Y'))) QH
	JOIN (SELECT QL.CD_COMPANY,
				 QL.NO_FILE,
				 MAX(CASE WHEN IG.NM_ITEMGRP IS NULL THEN '비용' 
				 							         ELSE IG.NM_ITEMGRP END) AS NM_ITEMGRP,
				 MAX(MC.NM_SYSDEF) AS NM_GRP_MFG,
				 MAX(MC1.NM_SYSDEF) AS NM_CLS_L,
				 MAX(MC2.NM_SYSDEF) AS NM_CLS_M,
				 MAX(MC3.NM_SYSDEF) AS NM_CLS_S
				 FROM CZ_SA_QTNL QL
				 LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = QL.CD_COMPANY AND MI.CD_ITEM = QL.CD_ITEM
				 LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = QL.CD_COMPANY AND IG.USE_YN = 'Y' AND IG.CD_ITEMGRP = QL.GRP_ITEM
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = MI.CD_COMPANY AND MC.CD_FIELD = 'MA_B000066' AND MC.CD_SYSDEF = MI.GRP_MFG
				 LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = MI.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000030' AND MC1.CD_SYSDEF = MI.CLS_L
				 LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = MI.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000031' AND MC2.CD_SYSDEF = MI.CLS_M
				 LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MI.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000032' AND MC3.CD_SYSDEF = MI.CLS_S 
				 WHERE (ISNULL(@P_YN_CHARGE, 'Y') = 'Y' OR ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%')
				 AND (ISNULL(@P_GRP_ITEM, '') = '' OR QL.GRP_ITEM = @P_GRP_ITEM)
				 AND (ISNULL(@P_GRP_MFC, '') = '' OR MI.GRP_MFG = @P_GRP_MFC)
				 GROUP BY QL.CD_COMPANY,
						  QL.NO_FILE) QL
	ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
	GROUP BY QH.CD_COMPANY,
		     QH.NM_KOR,
		     QH.YYYY,
		     QH.LN_PARTNER,
		     QH.LN_PARTNER_SP,
		     QH.NM_VESSEL,
		     QH.NM_PARTNER_GRP,
		     QH.NM_SALEGRP,
		     QH.NM_SALEORG,
		     QH.NM_EXCH_Q,
		     QH.NM_EXCH_S,
			 QH.NM_FG_PARTNER,
		     QL.NM_ITEMGRP,
		     QL.NM_GRP_MFG,
		     QL.NM_CLS_L,
		     QL.NM_CLS_M,
		     QL.NM_CLS_S
END
ELSE IF @P_TP_GUBUN = '1'
BEGIN
	SELECT QH.CD_COMPANY,
		   QH.NM_KOR,
		   QH.YYYY,
		   QH.LN_PARTNER,
		   QH.LN_PARTNER_SP,
		   QH.NM_VESSEL,
		   QH.NM_PARTNER_GRP,
		   QH.NM_SALEGRP,
		   QH.NM_SALEORG,
		   QH.NM_EXCH_Q,
		   QH.NM_EXCH_S,
		   QH.NM_FG_PARTNER,
		   QL.NM_ITEMGRP,
		   QL.NM_GRP_MFG,
		   QL.NM_CLS_L,
		   QL.NM_CLS_M,
		   QL.NM_CLS_S,
		   SUM(QL.QT_ITEM_QTN) AS QT_ITEM_QTN,
		   SUM(QL.QT_ITEM_SO) AS QT_ITEM_SO
	FROM (SELECT QH.CD_COMPANY,
			     QH.NO_FILE,
			     QH.CD_EXCH AS CD_EXCH_Q,
			     SH.CD_EXCH AS CD_EXCH_S,
				 (CASE WHEN ((@P_DT_TYPE = '001' AND QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '002') THEN LEFT(QH.DT_QTN, 4)
					   WHEN ((@P_DT_TYPE = '001' AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '003') THEN LEFT(SH.DT_SO, 4)
					   WHEN ((@P_DT_TYPE = '001' AND SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '004') THEN LEFT(SH.DT_CONTRACT, 4) END) AS YYYY,
			     ME.NM_KOR,
			     MP.LN_PARTNER,
			     ISNULL(QH1.LN_PARTNER, '') AS LN_PARTNER_SP,
			     MH.NM_VESSEL,
			     SG.NM_SALEGRP,
			     SO.NM_SALEORG,
			     MC.NM_SYSDEF AS NM_PARTNER_GRP,
			     MC1.NM_SYSDEF AS NM_EXCH_Q,
			     MC2.NM_SYSDEF AS NM_EXCH_S,
				 MC3.NM_SYSDEF AS NM_FG_PARTNER
			     FROM CZ_SA_QTNH QH
			     LEFT JOIN (SELECT QH.NO_FILE, 
						 	       MAX(MP.LN_PARTNER) AS LN_PARTNER 
						    FROM CZ_SA_QTNH QH
						 	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
						    WHERE QH.CD_COMPANY = 'S100'
						    GROUP BY QH.NO_FILE) QH1 
				 ON (QH1.NO_FILE = QH.NO_REF OR QH1.NO_FILE = QH.NO_FILE)
				 LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = QH.CD_COMPANY AND SH.NO_SO = QH.NO_FILE
				 LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP
				 LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
				 LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
				 LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = QH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000065' AND MC.CD_SYSDEF = QH.CD_PARTNER_GRP
				 LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = QH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = QH.CD_EXCH
				 LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = SH.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000005' AND MC2.CD_SYSDEF = SH.CD_EXCH
				 LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MP.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000001' AND MC3.CD_SYSDEF = MP.FG_PARTNER
				 LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = SG.CD_COMPANY AND SO.CD_SALEORG = SG.CD_SALEORG
				 WHERE QH.CD_COMPANY = @P_CD_COMPANY
				 AND (((@P_DT_TYPE = '001' OR @P_DT_TYPE = '002') AND (QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
				      ((@P_DT_TYPE = '001' OR @P_DT_TYPE = '003') AND (SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
					  ((@P_DT_TYPE = '001' OR @P_DT_TYPE = '004') AND (SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO)))
				 AND (ISNULL(@P_YN_CLAIM, 'N') = 'Y' OR QH.NO_FILE NOT LIKE 'CL%')
				 AND (ISNULL(@P_CD_PARTNER, '') = '' OR MP.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
				 AND (ISNULL(@P_CD_PARTNER_GRP, '') = '' OR QH.CD_PARTNER_GRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP)))
				 AND (ISNULL(@P_CD_PARTNER_GRP2, '') = '' OR MP.CD_PARTNER_GRP_2 IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP2)))
				 AND (ISNULL(@P_CD_SALEGRP, '') = '' OR SG.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
				 AND (ISNULL(@P_NO_EMP, '') = '' OR ME.NO_EMP = @P_NO_EMP)
				 AND (@P_YN_CLOSE = 'A' OR
					 (@P_YN_CLOSE = 'Y' AND ISNULL(QH.YN_CLOSE, 'N') = 'Y') OR 
					 (@P_YN_CLOSE = 'N' AND ISNULL(QH.YN_CLOSE, 'N') <> 'Y'))) QH
	JOIN (SELECT QL.CD_COMPANY,
				 QL.NO_FILE,
				 (CASE WHEN IG.NM_ITEMGRP IS NULL THEN '비용' 
				 							      ELSE IG.NM_ITEMGRP END) AS NM_ITEMGRP,
				 MC.NM_SYSDEF AS NM_GRP_MFG,
				 MC1.NM_SYSDEF AS NM_CLS_L,
				 MC2.NM_SYSDEF AS NM_CLS_M,
				 MC3.NM_SYSDEF AS NM_CLS_S,
				 SUM(1.0) AS QT_ITEM_QTN,
				 SUM(CASE WHEN SL.CD_COMPANY IS NULL THEN 0 ELSE 1.0 END) AS QT_ITEM_SO
				 FROM CZ_SA_QTNL QL
				 LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
				 LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = QL.CD_COMPANY AND MI.CD_ITEM = QL.CD_ITEM
				 LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = QL.CD_COMPANY AND IG.USE_YN = 'Y' AND IG.CD_ITEMGRP = QL.GRP_ITEM
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = MI.CD_COMPANY AND MC.CD_FIELD = 'MA_B000066' AND MC.CD_SYSDEF = MI.GRP_MFG
				 LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = MI.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000030' AND MC1.CD_SYSDEF = MI.CLS_L
				 LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = MI.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000031' AND MC2.CD_SYSDEF = MI.CLS_M
				 LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MI.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000032' AND MC3.CD_SYSDEF = MI.CLS_S 
				 WHERE (ISNULL(@P_YN_CHARGE, 'Y') = 'Y' OR ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%')
				 AND (ISNULL(@P_GRP_ITEM, '') = '' OR QL.GRP_ITEM = @P_GRP_ITEM)
				 AND (ISNULL(@P_GRP_MFC, '') = '' OR MI.GRP_MFG = @P_GRP_MFC)
				 GROUP BY QL.CD_COMPANY,
						  QL.NO_FILE,
						  IG.NM_ITEMGRP,
						  MC.NM_SYSDEF,
						  MC1.NM_SYSDEF,
						  MC2.NM_SYSDEF,
						  MC3.NM_SYSDEF) QL
	ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
	GROUP BY QH.CD_COMPANY,
		     QH.NM_KOR,
		     QH.YYYY,
		     QH.LN_PARTNER,
		     QH.LN_PARTNER_SP,
		     QH.NM_VESSEL,
		     QH.NM_PARTNER_GRP,
		     QH.NM_SALEGRP,
		     QH.NM_SALEORG,
		     QH.NM_EXCH_Q,
		     QH.NM_EXCH_S,
			 QH.NM_FG_PARTNER,
		     QL.NM_ITEMGRP,
		     QL.NM_GRP_MFG,
		     QL.NM_CLS_L,
		     QL.NM_CLS_M,
		     QL.NM_CLS_S
END
ELSE IF @P_TP_GUBUN = '2'
BEGIN
	SELECT QH.CD_COMPANY,
		   QH.NM_KOR,
		   QH.YYYY,
		   QH.LN_PARTNER,
		   QH.LN_PARTNER_SP,
		   QH.NM_VESSEL,
		   QH.NM_PARTNER_GRP,
		   QH.NM_SALEGRP,
		   QH.NM_SALEORG,
		   QH.NM_EXCH_Q,
		   QH.NM_EXCH_S,
		   QH.NM_FG_PARTNER,
		   QL.NM_ITEMGRP,
		   QL.NM_GRP_MFG,
		   QL.NM_CLS_L,
		   QL.NM_CLS_M,
		   QL.NM_CLS_S,
		   SUM(QL.QT_QTN) AS QT_QTN,
		   SUM(QL.QT_SO) AS QT_SO
	FROM (SELECT QH.CD_COMPANY,
			     QH.NO_FILE,
			     QH.CD_EXCH AS CD_EXCH_Q,
			     SH.CD_EXCH AS CD_EXCH_S,
				 (CASE WHEN ((@P_DT_TYPE = '001' AND QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '002') THEN LEFT(QH.DT_QTN, 4)
					   WHEN ((@P_DT_TYPE = '001' AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '003') THEN LEFT(SH.DT_SO, 4)
					   WHEN ((@P_DT_TYPE = '001' AND SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '004') THEN LEFT(SH.DT_CONTRACT, 4) END) AS YYYY,
			     ME.NM_KOR,
			     MP.LN_PARTNER,
			     ISNULL(QH1.LN_PARTNER, '') AS LN_PARTNER_SP,
			     MH.NM_VESSEL,
			     SG.NM_SALEGRP,
			     SO.NM_SALEORG,
			     MC.NM_SYSDEF AS NM_PARTNER_GRP,
			     MC1.NM_SYSDEF AS NM_EXCH_Q,
			     MC2.NM_SYSDEF AS NM_EXCH_S,
				 MC3.NM_SYSDEF AS NM_FG_PARTNER
			     FROM CZ_SA_QTNH QH
			     LEFT JOIN (SELECT QH.NO_FILE, 
						 	       MAX(MP.LN_PARTNER) AS LN_PARTNER 
						    FROM CZ_SA_QTNH QH
						 	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
						    WHERE QH.CD_COMPANY = 'S100'
						    GROUP BY QH.NO_FILE) QH1 
				 ON (QH1.NO_FILE = QH.NO_REF OR QH1.NO_FILE = QH.NO_FILE)
				 LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = QH.CD_COMPANY AND SH.NO_SO = QH.NO_FILE
				 LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP
				 LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
				 LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
				 LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = QH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000065' AND MC.CD_SYSDEF = QH.CD_PARTNER_GRP
				 LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = QH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = QH.CD_EXCH
				 LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = SH.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000005' AND MC2.CD_SYSDEF = SH.CD_EXCH
				 LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MP.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000001' AND MC3.CD_SYSDEF = MP.FG_PARTNER
				 LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = SG.CD_COMPANY AND SO.CD_SALEORG = SG.CD_SALEORG
				 WHERE QH.CD_COMPANY = @P_CD_COMPANY
				 AND (((@P_DT_TYPE = '001' OR @P_DT_TYPE = '002') AND (QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
				      ((@P_DT_TYPE = '001' OR @P_DT_TYPE = '003') AND (SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
					  ((@P_DT_TYPE = '001' OR @P_DT_TYPE = '004') AND (SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO)))
				 AND (ISNULL(@P_YN_CLAIM, 'N') = 'Y' OR QH.NO_FILE NOT LIKE 'CL%')
				 AND (ISNULL(@P_CD_PARTNER, '') = '' OR MP.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
				 AND (ISNULL(@P_CD_PARTNER_GRP, '') = '' OR QH.CD_PARTNER_GRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP)))
				 AND (ISNULL(@P_CD_PARTNER_GRP2, '') = '' OR MP.CD_PARTNER_GRP_2 IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP2)))
				 AND (ISNULL(@P_CD_SALEGRP, '') = '' OR SG.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
				 AND (ISNULL(@P_NO_EMP, '') = '' OR ME.NO_EMP = @P_NO_EMP)
				 AND (@P_YN_CLOSE = 'A' OR
					 (@P_YN_CLOSE = 'Y' AND ISNULL(QH.YN_CLOSE, 'N') = 'Y') OR 
					 (@P_YN_CLOSE = 'N' AND ISNULL(QH.YN_CLOSE, 'N') <> 'Y'))) QH
	JOIN (SELECT QL.CD_COMPANY,
				 QL.NO_FILE,
				 (CASE WHEN IG.NM_ITEMGRP IS NULL THEN '비용' 
				 							      ELSE IG.NM_ITEMGRP END) AS NM_ITEMGRP,
				 MC.NM_SYSDEF AS NM_GRP_MFG,
				 MC1.NM_SYSDEF AS NM_CLS_L,
				 MC2.NM_SYSDEF AS NM_CLS_M,
				 MC3.NM_SYSDEF AS NM_CLS_S,
				 SUM(QL.QT_QTN) AS QT_QTN,
				 SUM(SL.QT_SO) AS QT_SO
				 FROM CZ_SA_QTNL QL
				 LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
				 LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = QL.CD_COMPANY AND MI.CD_ITEM = QL.CD_ITEM
				 LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = QL.CD_COMPANY AND IG.USE_YN = 'Y' AND IG.CD_ITEMGRP = QL.GRP_ITEM
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = MI.CD_COMPANY AND MC.CD_FIELD = 'MA_B000066' AND MC.CD_SYSDEF = MI.GRP_MFG
				 LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = MI.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000030' AND MC1.CD_SYSDEF = MI.CLS_L
				 LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = MI.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000031' AND MC2.CD_SYSDEF = MI.CLS_M
				 LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MI.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000032' AND MC3.CD_SYSDEF = MI.CLS_S 
				 WHERE (ISNULL(@P_YN_CHARGE, 'Y') = 'Y' OR ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%')
				 AND (ISNULL(@P_GRP_ITEM, '') = '' OR QL.GRP_ITEM = @P_GRP_ITEM)
				 AND (ISNULL(@P_GRP_MFC, '') = '' OR MI.GRP_MFG = @P_GRP_MFC)
				 GROUP BY QL.CD_COMPANY,
						  QL.NO_FILE,
						  IG.NM_ITEMGRP,
						  MC.NM_SYSDEF,
						  MC1.NM_SYSDEF,
						  MC2.NM_SYSDEF,
						  MC3.NM_SYSDEF) QL
	ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
	GROUP BY QH.CD_COMPANY,
		     QH.NM_KOR,
		     QH.YYYY,
		     QH.LN_PARTNER,
		     QH.LN_PARTNER_SP,
		     QH.NM_VESSEL,
		     QH.NM_PARTNER_GRP,
		     QH.NM_SALEGRP,
		     QH.NM_SALEORG,
		     QH.NM_EXCH_Q,
		     QH.NM_EXCH_S,
			 QH.NM_FG_PARTNER,
		     QL.NM_ITEMGRP,
		     QL.NM_GRP_MFG,
		     QL.NM_CLS_L,
		     QL.NM_CLS_M,
		     QL.NM_CLS_S
END
ELSE IF @P_TP_GUBUN = '3'
BEGIN
	SELECT QH.CD_COMPANY,
		   QH.NM_KOR,
		   QH.YYYY,
		   QH.LN_PARTNER,
		   QH.LN_PARTNER_SP,
		   QH.NM_VESSEL,
		   QH.NM_PARTNER_GRP,
		   QH.NM_SALEGRP,
		   QH.NM_SALEORG,
		   QH.NM_EXCH_Q,
		   QH.NM_EXCH_S,
		   QH.NM_FG_PARTNER,
		   QL.NM_ITEMGRP,
		   QL.NM_GRP_MFG,
		   QL.NM_CLS_L,
		   QL.NM_CLS_M,
		   QL.NM_CLS_S,
		   SUM(QL.AM_EX_QTN) AS AM_EX_QTN,
		   SUM(QL.AM_KR_QTN) AS AM_KR_QTN,
		   SUM(QL.AM_EX_SO) AS AM_EX_SO,
		   SUM(QL.AM_KR_SO) AS AM_KR_SO
	FROM (SELECT QH.CD_COMPANY,
			     QH.NO_FILE,
			     QH.CD_EXCH AS CD_EXCH_Q,
			     SH.CD_EXCH AS CD_EXCH_S,
				 (CASE WHEN ((@P_DT_TYPE = '001' AND QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '002') THEN LEFT(QH.DT_QTN, 4)
					   WHEN ((@P_DT_TYPE = '001' AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '003') THEN LEFT(SH.DT_SO, 4)
					   WHEN ((@P_DT_TYPE = '001' AND SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '004') THEN LEFT(SH.DT_CONTRACT, 4) END) AS YYYY,
			     ME.NM_KOR,
			     MP.LN_PARTNER,
			     ISNULL(QH1.LN_PARTNER, '') AS LN_PARTNER_SP,
			     MH.NM_VESSEL,
			     SG.NM_SALEGRP,
			     SO.NM_SALEORG,
			     MC.NM_SYSDEF AS NM_PARTNER_GRP,
			     MC1.NM_SYSDEF AS NM_EXCH_Q,
			     MC2.NM_SYSDEF AS NM_EXCH_S,
				 MC3.NM_SYSDEF AS NM_FG_PARTNER
			     FROM CZ_SA_QTNH QH
			     LEFT JOIN (SELECT QH.NO_FILE, 
						 	       MAX(MP.LN_PARTNER) AS LN_PARTNER 
						    FROM CZ_SA_QTNH QH
						 	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
						    WHERE QH.CD_COMPANY = 'S100'
						    GROUP BY QH.NO_FILE) QH1 
				 ON (QH1.NO_FILE = QH.NO_REF OR QH1.NO_FILE = QH.NO_FILE)
				 LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = QH.CD_COMPANY AND SH.NO_SO = QH.NO_FILE
				 LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP
				 LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
				 LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
				 LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = QH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000065' AND MC.CD_SYSDEF = QH.CD_PARTNER_GRP
				 LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = QH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = QH.CD_EXCH
				 LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = SH.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000005' AND MC2.CD_SYSDEF = SH.CD_EXCH
				 LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MP.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000001' AND MC3.CD_SYSDEF = MP.FG_PARTNER
				 LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = SG.CD_COMPANY AND SO.CD_SALEORG = SG.CD_SALEORG
				 WHERE QH.CD_COMPANY = @P_CD_COMPANY
				 AND (((@P_DT_TYPE = '001' OR @P_DT_TYPE = '002') AND (QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
				      ((@P_DT_TYPE = '001' OR @P_DT_TYPE = '003') AND (SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
					  ((@P_DT_TYPE = '001' OR @P_DT_TYPE = '004') AND (SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO)))
				 AND (ISNULL(@P_YN_CLAIM, 'N') = 'Y' OR QH.NO_FILE NOT LIKE 'CL%')
				 AND (ISNULL(@P_CD_PARTNER, '') = '' OR MP.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
				 AND (ISNULL(@P_CD_PARTNER_GRP, '') = '' OR QH.CD_PARTNER_GRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP)))
				 AND (ISNULL(@P_CD_PARTNER_GRP2, '') = '' OR MP.CD_PARTNER_GRP_2 IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP2)))
				 AND (ISNULL(@P_CD_SALEGRP, '') = '' OR SG.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
				 AND (ISNULL(@P_NO_EMP, '') = '' OR ME.NO_EMP = @P_NO_EMP)
				 AND (@P_YN_CLOSE = 'A' OR
					 (@P_YN_CLOSE = 'Y' AND ISNULL(QH.YN_CLOSE, 'N') = 'Y') OR 
					 (@P_YN_CLOSE = 'N' AND ISNULL(QH.YN_CLOSE, 'N') <> 'Y'))) QH
	JOIN (SELECT QL.CD_COMPANY,
				 QL.NO_FILE,
				 (CASE WHEN IG.NM_ITEMGRP IS NULL THEN '비용' 
				 							      ELSE IG.NM_ITEMGRP END) AS NM_ITEMGRP,
				 MC.NM_SYSDEF AS NM_GRP_MFG,
				 MC1.NM_SYSDEF AS NM_CLS_L,
				 MC2.NM_SYSDEF AS NM_CLS_M,
				 MC3.NM_SYSDEF AS NM_CLS_S,
				 SUM(QL.AM_EX_S) AS AM_EX_QTN,
				 SUM(QL.AM_KR_S) AS AM_KR_QTN,
				 SUM(SL.AM_EX_S) AS AM_EX_SO,
				 SUM(SL.AM_KR_S) AS AM_KR_SO
				 FROM CZ_SA_QTNL QL
				 LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
				 LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = QL.CD_COMPANY AND MI.CD_ITEM = QL.CD_ITEM
				 LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = QL.CD_COMPANY AND IG.USE_YN = 'Y' AND IG.CD_ITEMGRP = QL.GRP_ITEM
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = MI.CD_COMPANY AND MC.CD_FIELD = 'MA_B000066' AND MC.CD_SYSDEF = MI.GRP_MFG
				 LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = MI.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000030' AND MC1.CD_SYSDEF = MI.CLS_L
				 LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = MI.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000031' AND MC2.CD_SYSDEF = MI.CLS_M
				 LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MI.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000032' AND MC3.CD_SYSDEF = MI.CLS_S 
				 WHERE (ISNULL(@P_YN_CHARGE, 'Y') = 'Y' OR ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%')
				 AND (ISNULL(@P_GRP_ITEM, '') = '' OR QL.GRP_ITEM = @P_GRP_ITEM)
				 AND (ISNULL(@P_GRP_MFC, '') = '' OR MI.GRP_MFG = @P_GRP_MFC)
				 GROUP BY QL.CD_COMPANY,
						  QL.NO_FILE,
						  IG.NM_ITEMGRP,
						  MC.NM_SYSDEF,
						  MC1.NM_SYSDEF,
						  MC2.NM_SYSDEF,
						  MC3.NM_SYSDEF) QL
	ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
	GROUP BY QH.CD_COMPANY,
		     QH.NM_KOR,
		     QH.YYYY,
		     QH.LN_PARTNER,
		     QH.LN_PARTNER_SP,
		     QH.NM_VESSEL,
		     QH.NM_PARTNER_GRP,
		     QH.NM_SALEGRP,
		     QH.NM_SALEORG,
		     QH.NM_EXCH_Q,
		     QH.NM_EXCH_S,
			 QH.NM_FG_PARTNER,
		     QL.NM_ITEMGRP,
		     QL.NM_GRP_MFG,
		     QL.NM_CLS_L,
		     QL.NM_CLS_M,
		     QL.NM_CLS_S
END
ELSE
BEGIN
	SELECT QH.CD_COMPANY,
		   QH.NM_KOR,
		   QH.YYYY,
		   QH.LN_PARTNER,
		   QH.LN_PARTNER_SP,
		   QH.NM_VESSEL,
		   QH.NM_PARTNER_GRP,
		   QH.NM_SALEGRP,
		   QH.NM_SALEORG,
		   QH.NM_EXCH_Q,
		   QH.NM_EXCH_S,
		   QH.NM_FG_PARTNER,
		   QL.NM_ITEMGRP,
		   QL.NM_GRP_MFG,
		   QL.NM_CLS_L,
		   QL.NM_CLS_M,
		   QL.NM_CLS_S,
		   SUM(QL.AM_SO) AS AM_SO,
		   SUM(QL.AM_STOCK) AS AM_STOCK,
		   SUM(QL.AM_PO) AS AM_PO
	FROM (SELECT QH.CD_COMPANY,
			     QH.NO_FILE,
			     QH.CD_EXCH AS CD_EXCH_Q,
			     SH.CD_EXCH AS CD_EXCH_S,
				 (CASE WHEN ((@P_DT_TYPE = '001' AND QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '002') THEN LEFT(QH.DT_QTN, 4)
					   WHEN ((@P_DT_TYPE = '001' AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '003') THEN LEFT(SH.DT_SO, 4)
					   WHEN ((@P_DT_TYPE = '001' AND SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO) OR @P_DT_TYPE = '004') THEN LEFT(SH.DT_CONTRACT, 4) END) AS YYYY,
			     ME.NM_KOR,
			     MP.LN_PARTNER,
			     ISNULL(QH1.LN_PARTNER, '') AS LN_PARTNER_SP,
			     MH.NM_VESSEL,
			     SG.NM_SALEGRP,
			     SO.NM_SALEORG,
			     MC.NM_SYSDEF AS NM_PARTNER_GRP,
			     MC1.NM_SYSDEF AS NM_EXCH_Q,
			     MC2.NM_SYSDEF AS NM_EXCH_S,
				 MC3.NM_SYSDEF AS NM_FG_PARTNER
			     FROM CZ_SA_QTNH QH
			     LEFT JOIN (SELECT QH.NO_FILE, 
						 	       MAX(MP.LN_PARTNER) AS LN_PARTNER 
						    FROM CZ_SA_QTNH QH
						 	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
						    WHERE QH.CD_COMPANY = 'S100'
						    GROUP BY QH.NO_FILE) QH1 
				 ON (QH1.NO_FILE = QH.NO_REF OR QH1.NO_FILE = QH.NO_FILE)
				 JOIN SA_SOH SH ON SH.CD_COMPANY = QH.CD_COMPANY AND SH.NO_SO = QH.NO_FILE
				 LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP
				 LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
				 LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
				 LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = QH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000065' AND MC.CD_SYSDEF = QH.CD_PARTNER_GRP
				 LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = QH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = QH.CD_EXCH
				 LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = SH.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000005' AND MC2.CD_SYSDEF = SH.CD_EXCH
				 LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MP.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000001' AND MC3.CD_SYSDEF = MP.FG_PARTNER
				 LEFT JOIN MA_SALEORG SO ON SO.CD_COMPANY = SG.CD_COMPANY AND SO.CD_SALEORG = SG.CD_SALEORG
				 WHERE QH.CD_COMPANY = @P_CD_COMPANY
				 AND (((@P_DT_TYPE = '001' OR @P_DT_TYPE = '002') AND (QH.DT_QTN BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
				      ((@P_DT_TYPE = '001' OR @P_DT_TYPE = '003') AND (SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
					  ((@P_DT_TYPE = '001' OR @P_DT_TYPE = '004') AND (SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO)))
				 AND (ISNULL(@P_YN_CLAIM, 'N') = 'Y' OR QH.NO_FILE NOT LIKE 'CL%')
				 AND (ISNULL(@P_CD_PARTNER, '') = '' OR MP.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER)))
				 AND (ISNULL(@P_CD_PARTNER_GRP, '') = '' OR QH.CD_PARTNER_GRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP)))
				 AND (ISNULL(@P_CD_PARTNER_GRP2, '') = '' OR MP.CD_PARTNER_GRP_2 IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PARTNER_GRP2)))
				 AND (ISNULL(@P_CD_SALEGRP, '') = '' OR SG.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
				 AND (ISNULL(@P_NO_EMP, '') = '' OR ME.NO_EMP = @P_NO_EMP)
				 AND (@P_YN_CLOSE = 'A' OR
					 (@P_YN_CLOSE = 'Y' AND ISNULL(QH.YN_CLOSE, 'N') = 'Y') OR 
					 (@P_YN_CLOSE = 'N' AND ISNULL(QH.YN_CLOSE, 'N') <> 'Y'))) QH
	JOIN (SELECT QL.CD_COMPANY,
				 QL.NO_FILE,
				 (CASE WHEN IG.NM_ITEMGRP IS NULL THEN '비용' 
				 							      ELSE IG.NM_ITEMGRP END) AS NM_ITEMGRP,
				 MC.NM_SYSDEF AS NM_GRP_MFG,
				 MC1.NM_SYSDEF AS NM_CLS_L,
				 MC2.NM_SYSDEF AS NM_CLS_M,
				 MC3.NM_SYSDEF AS NM_CLS_S,
				 SUM(SL.AM_KR_S) AS AM_SO,
				 SUM(ISNULL(SB.QT_STOCK, 0) * ISNULL(SB.UM_KR, 0)) AS AM_STOCK,
				 SUM(PL.AM_PO) AS AM_PO
				 FROM CZ_SA_QTNL QL
				 JOIN SA_SOL SL ON SL.CD_COMPANY = QL.CD_COMPANY AND SL.NO_SO = QL.NO_FILE AND SL.SEQ_SO = QL.NO_LINE
				 LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
				 LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
								   SUM(PL.AM) AS AM_PO 
							FROM PU_POL PL
							GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
				 ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
				 LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = QL.CD_COMPANY AND MI.CD_ITEM = QL.CD_ITEM
				 LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = QL.CD_COMPANY AND IG.USE_YN = 'Y' AND IG.CD_ITEMGRP = QL.GRP_ITEM
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = MI.CD_COMPANY AND MC.CD_FIELD = 'MA_B000066' AND MC.CD_SYSDEF = MI.GRP_MFG
				 LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = MI.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000030' AND MC1.CD_SYSDEF = MI.CLS_L
				 LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = MI.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000031' AND MC2.CD_SYSDEF = MI.CLS_M
				 LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = MI.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000032' AND MC3.CD_SYSDEF = MI.CLS_S 
				 WHERE (ISNULL(@P_YN_CHARGE, 'Y') = 'Y' OR ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%')
				 AND (ISNULL(@P_GRP_ITEM, '') = '' OR QL.GRP_ITEM = @P_GRP_ITEM)
				 AND (ISNULL(@P_GRP_MFC, '') = '' OR MI.GRP_MFG = @P_GRP_MFC)
				 GROUP BY QL.CD_COMPANY,
						  QL.NO_FILE,
						  IG.NM_ITEMGRP,
						  MC.NM_SYSDEF,
						  MC1.NM_SYSDEF,
						  MC2.NM_SYSDEF,
						  MC3.NM_SYSDEF) QL
	ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
	GROUP BY QH.CD_COMPANY,
		     QH.NM_KOR,
		     QH.YYYY,
		     QH.LN_PARTNER,
		     QH.LN_PARTNER_SP,
		     QH.NM_VESSEL,
		     QH.NM_PARTNER_GRP,
		     QH.NM_SALEGRP,
		     QH.NM_SALEORG,
		     QH.NM_EXCH_Q,
		     QH.NM_EXCH_S,
			 QH.NM_FG_PARTNER,
		     QL.NM_ITEMGRP,
		     QL.NM_GRP_MFG,
		     QL.NM_CLS_L,
		     QL.NM_CLS_M,
		     QL.NM_CLS_S
END

GO