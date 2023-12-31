USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_IVL_AUTO_PARTIAL_RPT_S]    Script Date: 2015-06-22 오후 7:27:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_IVL_AUTO_PARTIAL_RPT_S]
(
	@P_CD_COMPANY		    NVARCHAR(7),
    @P_NO_IV				NVARCHAR(20)
)

AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT IL.CD_COMPANY,
           IL.NO_SO,
           SUM(IL.QT_CLS) AS QT_CLS,
           SUM(SL.QT_SO) AS QT_SO
    FROM (SELECT IL.CD_COMPANY, IL.NO_SO, IL.NO_SOLINE,
			     SUM(IL.QT_CLS) AS QT_CLS
		  FROM SA_IVL IL
		  WHERE IL.CD_COMPANY = @P_CD_COMPANY
          AND IL.NO_IV = @P_NO_IV
          AND IL.CD_ITEM NOT LIKE 'SD%'
		  GROUP BY IL.CD_COMPANY, IL.NO_SO, IL.NO_SOLINE) IL
    LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = IL.CD_COMPANY AND SL.NO_SO = IL.NO_SO AND SL.SEQ_SO = IL.NO_SOLINE
    GROUP BY IL.CD_COMPANY, IL.NO_SO
),
B AS
(
	SELECT A.CD_COMPANY,
	       A.NO_SO,
	       A.QT_CLS,
           IL.QT_CLS AS QT_CLS1,
	       A.QT_SO,
	       SL.QT_SO AS QT_SO1
	FROM A
	JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
	             SUM(SL.QT_SO) AS QT_SO
	      FROM SA_SOL SL
	      WHERE SL.CD_ITEM NOT LIKE 'SD%'
	      GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
	ON SL.CD_COMPANY = A.CD_COMPANY AND SL.NO_SO = A.NO_SO
    JOIN (SELECT IL.CD_COMPANY, IL.NO_SO,
                 SUM(IL.QT_CLS) AS QT_CLS 
          FROM SA_IVL IL
          WHERE IL.CD_ITEM NOT LIKE 'SD%'
          GROUP BY IL.CD_COMPANY, IL.NO_SO) IL
    ON IL.CD_COMPANY = A.CD_COMPANY AND IL.NO_SO = A.NO_SO
	WHERE (A.QT_CLS <> A.QT_SO OR A.QT_CLS <> IL.QT_CLS)
	AND IL.QT_CLS = SL.QT_SO	
),
C AS
(
    SELECT IL.CD_COMPANY,
           IL.NO_IV,
           IL.NO_LINE 
    FROM B
    JOIN SA_IVL IL ON IL.CD_COMPANY = B.CD_COMPANY AND IL.NO_SO = B.NO_SO
)
SELECT (CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.QT_GI_CLS 
									 ELSE IL.QT_GI_CLS END) AS QT_GI_CLS, 
	   (CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.AM_EX_CLS 
									 ELSE IL.AM_EX_CLS END) AS AM_EX, 
	   (CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.UM_EX_CLS 
									 ELSE IL.UM_EX_CLS END) AS UM_EX, 
	   (CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.AM_CLS 
									 ELSE IL.AM_CLS END) AS AM_CLS, 
	   (CASE WHEN IL.YN_RETURN = 'Y' THEN -IH.AM_K 
									 ELSE IH.AM_K END) AS AM_TOT, 
	   (CASE WHEN IL.YN_RETURN = 'Y' THEN -IH.VAT_TAX 
									 ELSE IH.VAT_TAX END) AS VAT_TOT, 
	   IH.NO_VOLUME, -- 권 
	   (CASE WHEN ISNULL(IL.YN_RETURN, 'N') = 'Y' THEN -IL.VAT 
												  ELSE IL.VAT END) VAT,
	   ROUND(CASE WHEN ISNULL(IL.YN_RETURN, 'N') = 'Y' THEN -(IL.VAT / (CASE WHEN ISNULL(IH.RT_EXCH, 0) = 0 THEN 1 
																											ELSE IH.RT_EXCH END)) 
	   												   ELSE (IL.VAT / (CASE WHEN ISNULL(IH.RT_EXCH, 0) = 0 THEN 1 
																										   ELSE IH.RT_EXCH END)) END, 2) AS VAT_EX,
	   IH.NO_HO, -- 호 
	   RIGHT('00000'+ CAST (ISNULL(IH.NO_SEQ,0) AS VARCHAR(5)),5) NO_SEQ, -- 일련번호
	   IL.UM_EX_CLS UM, 
	   IL.NO_IV, 
	   IL.NO_IV_ORG AS NO_IV2,
	   IL.NO_LINE, 
	   IL.CD_ITEM, 
	   IL.NO_IO,
	   IL.NO_SO,
	   (CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.UM_ITEM_CLS 
									 ELSE IL.UM_ITEM_CLS END) AS UM_ITEM_CLS,
	   IL.DT_TAX,
	   SUBSTRING(IL.DT_TAX, 3, 2) DT_TAX_YY, 
	   SUBSTRING(IL.DT_TAX, 5, 2) DT_TAX_MM, 
	   SUBSTRING(IL.DT_TAX, 7, 2) DT_TAX_DD, 
	   C.NM_ITEM, 
	   C.STND_ITEM, 
	   C.UNIT_IM,
	   PT.NM_PLANT,
	   AH.NM_TP AS NM_TP_IV,
	   PH.NM_PROJECT,
	   IL.TP_IV, IL.ID_UPDATE, IL.CD_COMPANY, 
	   MC2.CD_FLAG1,
	   (CASE WHEN ISNULL(IL.YN_RETURN, 'N') = 'Y' THEN -(IL.AM_CLS + IL.VAT) 
												  ELSE (IL.AM_CLS + IL.VAT) END) AM_SUM, 
	   (CASE WHEN ISNULL(IL.YN_RETURN, 'N') = 'Y' THEN -(IL.AM_EX_CLS + ROUND(IL.VAT / (CASE WHEN ISNULL(IH.RT_EXCH, 0) = 0 THEN 1 
																															ELSE IH.RT_EXCH END), 2)) 
	   										      ELSE (IL.AM_EX_CLS + ROUND(IL.VAT / (CASE WHEN ISNULL(IH.RT_EXCH, 0) = 0 THEN 1 
																														   ELSE IH.RT_EXCH END), 2)) END) AS AM_SUM_EX, 
	   IL.CD_CC, M.NM_CC,
	   IL.DC_RMK AS DC_RMK_IV,
	   E.TP_BUSINESS,
	   SG.NM_SALEGRP,
	   OG.NM_SALEORG, OG.EN_SALEORG, ME.NM_ENG, HP.DC_SIGN,
	   OH.DT_IO,
	   IL.UD_NM_01, F1.NM_MNGD AS NM_MNGD1,
	   IL.UD_NM_02, F2.NM_MNGD AS NM_MNGD2, 
	   IL.UD_NM_03, F3.NM_MNGD AS NM_MNGD3, 
	   IL.UD_NM_04,
	   IL.DT_BL,
	   C.WEIGHT AS WEIGHT,
	   ROUND(IL.QT_GI_CLS * ISNULL(C.WEIGHT, 0), 2) AS QT_WEIGHT,
	   SL.CD_ITEM_REF,
	   P.NM_ITEM AS NM_ITEM_REF,
       P.STND_ITEM AS STND_ITEM_REF,
       SL.YN_PICKING,
       C.CLS_ITEM,
	   OL.DC_RMK AS QTIO_DC_RMK,
       P.CD_USERDEF15 PITEM_CD_USERDEF15, P.CD_USERDEF16 PITEM_CD_USERDEF16,
       SL.FG_USE2,
       PG.CD_PJTGRP,
       PG.NM_PJTGRP,
       IL.CD_PJT,
       IL.ID_MEMO, IL.CD_WBS, IL.NO_SHARE, IL.NO_ISSUE,
	   MH.INVOICE_COMPANY, MH.INVOICE_ADDRESS, MH.INVOICE_TEL, MH.INVOICE_EMAIL, MH.INVOICE_RMK,
	   MH.NO_IMO, MH.NO_HULL, MH.NM_VESSEL,
	   MC6.CD_FLAG1 AS CD_NATION_INVOICE,
	   IL.CD_EXCH,
	   MC3.NM_SYSDEF AS NM_EXCH,
	   SH.DT_SO,
	   SH.NO_PO_PARTNER,
	   SH.COND_PAY,
	   MC4.NM_SYSDEF AS NM_COND_PAY,
	   SH.TP_TRANS, SH.PORT_LOADING AS TP_TRANSPORT,
	   MC5.NM_SYSDEF + ' ' + SH.PORT_LOADING AS NM_TRANSPORT,
	   WD.DT_LOADING,
	   MC.NM_SYSDEF AS NM_MAIN_CATEGORY,
	   MC1.NM_SYSDEF AS NM_SUB_CATEGORY,
	   (CASE WHEN QH.CD_COMPANY IS NULL THEN SL.SEQ_SO
			 WHEN IL.CD_ITEM LIKE 'SD%' THEN 99999 
							            ELSE QL.NO_DSP END) AS NO_DSP,
	   (CASE WHEN QH.CD_COMPANY IS NULL THEN C.NM_ITEM
			 WHEN (IL.CD_ITEM NOT LIKE 'SD%' OR IL.CD_ITEM = QL.CD_ITEM)THEN QL.NM_SUBJECT
			 ELSE '' END) AS NM_SUBJECT,
	   (CASE WHEN QH.CD_COMPANY IS NULL THEN ''
			 WHEN (IL.CD_ITEM NOT LIKE 'SD%' OR IL.CD_ITEM = QL.CD_ITEM)THEN QL.CD_ITEM_PARTNER
			 ELSE '' END) AS CD_ITEM_PARTNER,
	   (CASE WHEN QH.CD_COMPANY IS NULL THEN SL.DC1
			 WHEN (IL.CD_ITEM NOT LIKE 'SD%' OR IL.CD_ITEM = QL.CD_ITEM)THEN QL.NM_ITEM_PARTNER
			 ELSE C.NM_ITEM END) AS NM_ITEM_PARTNER,
	   QL.YN_DSP_RMK,
	   (CASE WHEN IL.CD_ITEM LIKE 'SD%' THEN NULL 
									    ELSE QL.DC_RMK END) AS DC_RMK,
	   GH.NO_EMP,
	   ME1.NM_KOR AS NM_LOG_EMP,
	   IH.RT_EXCH,
	   (CASE WHEN QH.CD_COMPANY IS NULL THEN ISNULL(SL.UM_BASE, 0)
			 WHEN IL.CD_ITEM LIKE 'SD%' THEN ISNULL(IL.UM_EX_CLS, 0) 
										ELSE ISNULL(SL.UM_EX_Q, 0) END) AS UM_EX_Q,
	   ((CASE WHEN IL.YN_RETURN = 'Y' THEN -1 
									  ELSE 1 END) * (CASE WHEN QH.CD_COMPANY IS NULL THEN ISNULL(SL.UM_BASE, 0) * ISNULL(IL.QT_GI_CLS, 0)
														  WHEN IL.CD_ITEM LIKE 'SD%' THEN ISNULL(IL.AM_EX_CLS, 0) 
	   																				 ELSE ROUND(ISNULL(SL.UM_EX_Q, 0) * ISNULL(IL.QT_GI_CLS, 0), 2) END)) AS AM_EX_Q,
	   (CASE WHEN QH.CD_COMPANY IS NULL THEN ISNULL(SL.RT_DSCNT, 0)
			 WHEN IL.CD_ITEM LIKE 'SD%' THEN 0 
										ELSE ISNULL(SL.RT_DC, 0) END) AS RT_DC,
	   -((CASE WHEN IL.YN_RETURN = 'Y' THEN -1 
									   ELSE 1 END) * (CASE WHEN QH.CD_COMPANY IS NULL THEN (ISNULL(SL.UM_BASE, 0) - ISNULL(SL.UM_SO, 0)) * ISNULL(IL.QT_GI_CLS, 0)
														   WHEN IL.CD_ITEM LIKE 'SD%' THEN 0
	   																				  ELSE ROUND(ISNULL(SL.UM_EX_Q, 0) * ISNULL(IL.QT_GI_CLS, 0), 2) - ROUND(ISNULL(SL.UM_EX_S, 0) * ISNULL(IL.QT_GI_CLS, 0), 2) END)) AS AM_DC,
	   (CASE WHEN QH.CD_COMPANY = NULL THEN 'N' 
									   ELSE 'Y' END) AS YN_QTN,
	   MP.EN_PARTNER AS EN_DELIVERY_TO,
	   SL.NO_PO_PARTNER,
	   FP.NM_PTR,
	   (CASE WHEN IL.CD_ITEM LIKE 'SD%' THEN 99999 ELSE (CASE WHEN QH.CD_COMPANY IS NULL THEN SL.SEQ_SO ELSE QL.NO_DSP END) END) AS SEQ_ORDER,
	   IH.DC_REMARK,
	   (CASE WHEN SL.SEQ_SO > 90000 THEN SL.NM_ITEM_PARTNER ELSE NULL END) AS NM_ITEM_SOL,
	   WD.DT_IV,
	   OL.NO_ISURCV AS NO_GIR
FROM SA_IVH IH 
JOIN SA_IVL IL ON IH.NO_IV = IL.NO_IV AND IH.CD_COMPANY = IL.CD_COMPANY
LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = IL.CD_COMPANY AND OL.NO_IO = IL.NO_IO AND OL.NO_IOLINE = IL.NO_IOLINE
LEFT JOIN MM_QTIOH OH ON OH.CD_COMPANY = IL.CD_COMPANY AND OH.NO_IO = IL.NO_IO 
LEFT JOIN CZ_SA_GIRH_WORK_DETAIL WD ON WD.CD_COMPANY = OL.CD_COMPANY AND WD.NO_GIR = OL.NO_ISURCV
LEFT JOIN CZ_MA_DELIVERY MP ON MP.CD_COMPANY = WD.CD_COMPANY AND MP.CD_PARTNER = WD.CD_DELIVERY_TO
LEFT JOIN SA_GIRH GH ON GH.CD_COMPANY = OL.CD_COMPANY AND GH.NO_GIR = OL.NO_ISURCV
LEFT JOIN SA_SOL SL ON IL.CD_COMPANY = SL.CD_COMPANY AND IL.CD_PLANT = SL.CD_PLANT AND IL.NO_SO = SL.NO_SO AND IL.NO_SOLINE = SL.SEQ_SO
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO
LEFT JOIN MA_PITEM C ON IL.CD_ITEM = C.CD_ITEM AND IL.CD_PLANT = C.CD_PLANT AND IL.CD_COMPANY = C.CD_COMPANY 
LEFT JOIN MA_PARTNER E ON E.CD_COMPANY = IH.CD_COMPANY AND IH.CD_PARTNER = E.CD_PARTNER
LEFT JOIN MA_CC M ON M.CD_COMPANY = IL.CD_COMPANY AND M.CD_CC = IL.CD_CC
LEFT JOIN MA_SALEGRP SG ON IL.CD_COMPANY = SG.CD_COMPANY AND IL.CD_SALEGRP = SG.CD_SALEGRP 
LEFT JOIN MA_SALEORG OG ON SG.CD_COMPANY = OG.CD_COMPANY AND SG.CD_SALEORG = OG.CD_SALEORG
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = OG.CD_COMPANY AND ME.NO_EMP = OG.NO_SALES
LEFT JOIN MA_EMP ME1 ON ME1.CD_COMPANY = GH.CD_COMPANY AND ME1.NO_EMP = GH.NO_EMP
LEFT JOIN HR_PHOTO HP ON HP.CD_COMPANY = OG.CD_COMPANY AND HP.NO_EMP = OG.NO_SALES      
LEFT JOIN FI_MNGD F1 ON F1.CD_MNG = 'A21' AND IL.UD_NM_01 = F1.CD_MNGD AND IL.CD_COMPANY = F1.CD_COMPANY
LEFT JOIN FI_MNGD F2 ON F2.CD_MNG = 'A22' AND IL.UD_NM_02 = F2.CD_MNGD AND IL.CD_COMPANY = F2.CD_COMPANY
LEFT JOIN FI_MNGD F3 ON F3.CD_MNG = 'A25' AND IL.UD_NM_03 = F3.CD_MNGD AND IL.CD_COMPANY = F3.CD_COMPANY
LEFT JOIN MA_PITEM P ON SL.CD_COMPANY = P.CD_COMPANY AND SL.CD_PLANT = P.CD_PLANT AND SL.CD_ITEM_REF = P.CD_ITEM            
LEFT JOIN SA_PROJECTH PH ON PH.NO_PROJECT = IL.CD_PJT AND PH.CD_COMPANY = IL.CD_COMPANY
LEFT JOIN MA_PJTGRPD PGD ON PGD.CD_COMPANY = PH.CD_COMPANY AND PGD.CD_PJT = PH.NO_PROJECT		
LEFT JOIN MA_PJTGRP PG ON PG.CD_COMPANY = PGD.CD_COMPANY AND PG.CD_PJTGRP = PGD.CD_PJTGRP
LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = IL.CD_COMPANY AND QH.NO_FILE = IL.NO_SO
LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = IL.CD_COMPANY AND QL.NO_FILE = IL.NO_SO AND QL.NO_LINE = IL.NO_SOLINE
LEFT JOIN FI_PARTNERPTR FP ON FP.CD_COMPANY = QH.CD_COMPANY AND FP.CD_PARTNER = QH.CD_PARTNER AND FP.SEQ = QH.SEQ_ATTN
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = (CASE WHEN ISNULL(WD.NO_IMO_BILL, '') <> '' THEN WD.NO_IMO_BILL 
											 WHEN ISNULL(WD.NO_IMO, '') <> '' THEN WD.NO_IMO 
																			  ELSE SH.NO_IMO END)
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = WD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00006' AND MC.CD_SYSDEF = WD.CD_MAIN_CATEGORY
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = WD.CD_COMPANY AND MC1.CD_FIELD = MC.CD_FLAG1 AND MC1.CD_SYSDEF = WD.CD_SUB_CATEGORY
LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = IH.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000040' AND MC2.CD_SYSDEF = IH.FG_TAX
LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = IL.CD_COMPANY AND MC3.CD_FIELD = 'MA_B000005' AND MC3.CD_SYSDEF = IL.CD_EXCH
LEFT JOIN MA_CODEDTL MC4 ON MC4.CD_COMPANY = SH.CD_COMPANY AND MC4.CD_FIELD = 'CZ_SA00013' AND MC4.CD_SYSDEF = SH.COND_PAY
LEFT JOIN MA_CODEDTL MC5 ON MC5.CD_COMPANY = SH.CD_COMPANY AND MC5.CD_FIELD = 'TR_IM00003' AND MC5.CD_SYSDEF = SH.TP_TRANS
LEFT JOIN MA_CODEDTL MC6 ON MC6.CD_COMPANY = IH.CD_COMPANY AND MC6.CD_FIELD = 'MA_B000020' AND MC6.CD_SYSDEF = MH.CD_NATION
LEFT JOIN MA_PLANT PT ON PT.CD_COMPANY = IH.CD_COMPANY AND PT.CD_BIZAREA = IH.CD_BIZAREA AND PT.CD_PLANT = IL.CD_PLANT
LEFT JOIN MA_AISPOSTH AH ON AH.CD_COMPANY = IL.CD_COMPANY AND AH.FG_TP = '100' AND AH.CD_TP = IL.TP_IV
WHERE IH.CD_COMPANY = @P_CD_COMPANY
AND EXISTS (SELECT 1 
            FROM C 
            WHERE C.CD_COMPANY = IL.CD_COMPANY 
            AND C.NO_IV = IL.NO_IV 
            AND C.NO_LINE = IL.NO_LINE)
ORDER BY SL.NO_SO, (CASE WHEN IL.CD_ITEM LIKE 'SD%' THEN 99999 ELSE (CASE WHEN QH.CD_COMPANY IS NULL THEN SL.SEQ_SO ELSE QL.NO_DSP END) END) ASC

GO