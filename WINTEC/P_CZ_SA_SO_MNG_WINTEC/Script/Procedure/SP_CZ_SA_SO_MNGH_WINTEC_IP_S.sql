USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SO_MNGH_WINTEC_IP_S]    Script Date: 2022-05-25 오후 6:06:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_SO_MNGH_WINTEC_IP_S]
(          
    @P_CD_COMPANY			NVARCHAR(7),
    @P_CD_PLANT				NVARCHAR(7),
    @P_DT_FROM			    NVARCHAR(8),
    @P_DT_TO				NVARCHAR(8),
    @P_MULTI_SALEGRP		NVARCHAR(4000),
    @P_NO_EMP				NVARCHAR(10),
    @P_TP_BUSI				NVARCHAR(3),
    @P_CD_PARTNER			NVARCHAR(20),
    @P_MULTI_TP_SO			NVARCHAR(4000),
    @P_STA_SO				NVARCHAR(3),
    @P_NO_PROJECT			NVARCHAR(20),
    @P_YN_AFTER				NVARCHAR(1),
    @P_FG_DEAL				NVARCHAR(7),
    @P_MULTI_SALEORG		NVARCHAR(4000) = '',
    @P_CD_PARTNER_GRP		NVARCHAR(20) = '',
    @P_CD_PARTNER_GRP2		NVARCHAR(20) = '',
    @P_NO_EMP_LOGIN         NVARCHAR(10) = '',
    @P_NO_SO                NVARCHAR(20) = NULL,
    @P_ST_STAT              NVARCHAR(5) = NULL,
    @P_ST_PROC              NVARCHAR(1) = NULL,
    @P_GI_PARTNER           NVARCHAR(20) = NULL,
    @P_FG_PO	            NVARCHAR(20) = NULL,
    @P_FG_LANG				NVARCHAR(4) = NULL,
    @P_SO_STAT				NVARCHAR(2) = NULL,
    @P_CD_ITEM_FR			NVARCHAR(50) = NULL,
    @P_CD_ITEM_TO			NVARCHAR(50) = NULL,
    @P_NO_HULL              NVARCHAR(100)    
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG

DECLARE @V_YN_MFG_AUTH  NVARCHAR(1)

SELECT @V_YN_MFG_AUTH = ISNULL(YN_MFG_AUTH, 'N')
FROM MA_ENV
WHERE CD_COMPANY = @P_CD_COMPANY

SELECT 'N' S, 
       SH.NO_SO, 
       SH.DT_SO, 
       SH.CD_PARTNER,
	   MP.LN_PARTNER,
	   ST.NM_SO, 
       SH.CD_EXCH, 
       SG.NM_SALEGRP, 
       ME.NM_KOR, 
       SH.NO_PROJECT, 
       PJ.NM_PROJECT, 
       SH.DC_RMK,       
       SH.CD_SALEGRP, 
       SH.NO_EMP, 
       SH.RT_EXCH, 
       SH.TP_PRICE, 
       SH.NO_PROJECT, 
       SH.TP_VAT, 
       MC.NM_SYSDEF AS NM_TP_VAT, 
       SH.FG_VAT,
       SH.RT_VAT, 
       SH.FG_TAXP, 
       SH.FG_BILL, 
       SH.FG_TRANSPORT, 
       SH.NO_CONTRACT,
       PP.FG_DEAL,
       SG.CD_SALEORG, 
       OG.NM_SALEORG,
       SL.AM_SO, 
       SL.AM_WONAMT, 
       ISNULL(SL.QT_SO, 0) AS QT_SO, 
       SL.AM_VAT,
       SH.MEMO_CD, 
       SH.CHECK_PEN,
       (ISNULL(SL.AM_WONAMT, 0) + ISNULL(SL.AM_VAT, 0)) AS AMVAT_SO,
       SH.NO_PO_PARTNER,
       SH.DTS_INSERT,
       CR.AM_CREDIT,
       ISNULL(CR.AM_CREDIT,0) - (ISNULL(SL.AM_WONAMT, 0) + ISNULL(SL.AM_VAT, 0)) AS AM_PRE_CREDIT,
       ISNULL(FG1.ST_STAT, '2') AS ST_STAT,
       SH.DC_RMK1,
       ISNULL(ME1.NM_KOR, MU.NM_USER) AS NM_ID_INSERT,
       ST.RET,
       SH.TXT_USERDEF4,
       SH.TP_SO,
       SH.NO_HST,
       (SELECT TOP 1 TXT_USERDEF1 FROM DZSN_SA_SOL_DLV WHERE CD_COMPANY = @P_CD_COMPANY AND NO_SO = SH.NO_SO) AS TEMPUR_DLV_TXT_USERDEF1,
       (SELECT COUNT(1) FROM MA_FILEINFO WHERE CD_COMPANY = @P_CD_COMPANY AND CD_MODULE = 'SA' AND CD_FILE = SH.NO_SO + '_' + CONVERT(NVARCHAR, SH.NO_HST)) AS FILE_CNT,
       MC1.NM_SYSDEF AS NM_EXCH,
       SH.TXT_USERDEF2,  --현대머티리얼 전용
       SH.TXT_USERDEF3,  --현대머티리얼 전용
       CONVERT(NVARCHAR(4000), SH.DC_RMK_TEXT) AS DC_RMK_TEXT,
       ISNULL(SL.QT_GIR,0) AS QT_GIR,
       ISNULL(SL.QT_GI,0) AS QT_GI,
       SH.TXT_USERDEF1,
       FG.APP_NO_EMP_END,
       ME2.NM_KOR AS APP_NM_EMP_END,
	   SH.TXT_USERDEF1,
	   (SELECT NM_PTR FROM FI_PARTNERPTR WHERE CD_COMPANY = SH.CD_COMPANY AND CD_PARTNER = SH.CD_PARTNER AND SEQ = SH.NUM_USERDEF2) AS NM_PUR,
	   (SELECT NM_PTR FROM FI_PARTNERPTR WHERE CD_COMPANY = SH.CD_COMPANY AND CD_PARTNER = SH.CD_PARTNER AND SEQ = SH.NUM_USERDEF3) AS NM_DESIGN,
	   MP1.LN_PARTNER AS NM_NEGO,
	   (SELECT NM_PTR FROM FI_PARTNERPTR WHERE CD_COMPANY = SH.CD_COMPANY AND CD_PARTNER = SH.CD_PARTNER AND SEQ = SH.NUM_USERDEF1) AS NM_TAKE,
	   SH.DTS_UPDATE AS DTS_UPDATE,
	   SL.DTS_UPDATE AS DTS_UPDATE1,
       SH.TXT_USERDEF2 AS NO_LC,
       MS.DTS_SEND_NEW,
       MS.DTS_SEND_MODIFY
FROM SA_SOH SH
JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
			 SUM(SL.AM_SO) AS AM_SO,
			 SUM(SL.AM_WONAMT) AS AM_WONAMT,
			 SUM(SL.AM_VAT) AS AM_VAT,
			 SUM(SL.QT_SO) AS QT_SO,
			 SUM(SL.QT_GIR) AS QT_GIR,
			 SUM(SL.QT_GI) AS QT_GI,
			 MAX(SL.DTS_UPDATE) AS DTS_UPDATE
	  FROM SA_SOL SL
      LEFT JOIN SA_PROJECTL PL ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_PROJECT = SL.NO_PROJECT AND PL.SEQ_PROJECT = SL.SEQ_PROJECT
      WHERE SL.CD_COMPANY = @P_CD_COMPANY
      AND (SL.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)
      AND (SL.TP_BUSI = @P_TP_BUSI OR @P_TP_BUSI = '' OR @P_TP_BUSI IS NULL)
      AND (SL.STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)
      AND (SL.NO_PROJECT = @P_NO_PROJECT OR @P_NO_PROJECT = '' OR @P_NO_PROJECT IS NULL) --SH.NO_PROJECT를 SL.NO_PO
      AND (SL.GI_PARTNER = @P_GI_PARTNER OR @P_GI_PARTNER = '' OR @P_GI_PARTNER IS NULL)
      AND (@P_FG_PO IS NULL OR @P_FG_PO = '' OR (@P_FG_PO = 'Y' AND ISNULL(SL.QT_PO, 0) > 0) OR (@P_FG_PO = 'N' AND ISNULL(SL.QT_PO, 0) = 0))
      AND (SL.CD_ITEM >= @P_CD_ITEM_FR OR @P_CD_ITEM_FR = '' OR @P_CD_ITEM_FR IS NULL)
      AND (SL.CD_ITEM <= @P_CD_ITEM_TO OR @P_CD_ITEM_TO = '' OR @P_CD_ITEM_TO IS NULL)
      AND (ISNULL(PL.YN_AFTER, 'N') = @P_YN_AFTER OR @P_YN_AFTER = '' OR @P_YN_AFTER IS NULL)
      AND (SL.TXT_USERDEF6 = @P_NO_HULL OR @P_NO_HULL = '' OR @P_NO_HULL IS NULL)
      AND (@P_ST_PROC IS NULL OR @P_ST_PROC = '' OR ISNULL((SELECT TOP 1 DL.ST_PROC
		                                                    FROM SA_GIRL GL
		                                                    LEFT JOIN SA_DLV_L DL ON GL.CD_COMPANY = DL.CD_COMPANY AND GL.NO_GIR = DL.NO_REQ AND GL.SEQ_GIR = DL.NO_REQ_LINE
		                                                    WHERE GL.CD_COMPANY = @P_CD_COMPANY 
                                                            AND GL.NO_SO = SL.NO_SO 
                                                            AND GL.SEQ_SO = SL.SEQ_SO), 'O') = @P_ST_PROC)
	  GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN MA_PARTNER MP1 ON MP1.CD_COMPANY = SH.CD_COMPANY AND MP1.CD_PARTNER = SH.NO_NEGO
LEFT JOIN SA_TPSO ST ON ST.CD_COMPANY = SH.CD_COMPANY AND ST.TP_SO = SH.TP_SO
LEFT JOIN FI_GWDOCU FG ON FG.CD_COMPANY = SH.CD_COMPANY AND FG.NO_DOCU = SH.NO_PROJECT
LEFT JOIN FI_GWDOCU FG1 ON FG1.CD_COMPANY = SH.CD_COMPANY AND FG1.NO_DOCU = SH.NO_SO
LEFT JOIN MA_SALEGRP SG	ON SG.CD_COMPANY = SH.CD_COMPANY AND SG.CD_SALEGRP = SH.CD_SALEGRP
LEFT JOIN MA_SALEORG OG ON OG.CD_COMPANY = SG.CD_COMPANY AND OG.CD_SALEORG = SG.CD_SALEORG
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
LEFT JOIN MA_EMP ME1 ON ME1.CD_COMPANY = SH.CD_COMPANY AND ME1.NO_EMP = SH.ID_INSERT
LEFT JOIN MA_EMP ME2 ON ME2.CD_COMPANY = FG.CD_COMPANY AND ME2.NO_EMP = FG.APP_NO_EMP_END
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = SH.CD_COMPANY AND MU.ID_USER = SH.ID_INSERT
LEFT JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = SH.CD_COMPANY AND PJ.NO_PROJECT= SH.NO_PROJECT
LEFT JOIN SA_PJTH_PRE PP ON PP.CD_COMPANY = SH.CD_COMPANY AND PP.CD_PJT = SH.NO_PROJECT
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = SH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000040' AND MC.CD_SYSDEF = SH.TP_VAT
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = SH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = SH.CD_EXCH
LEFT JOIN (SELECT CD_COMPANY, NO_SO,
                  MAX(CASE WHEN TP_MAIL = '001' THEN DTS_INSERT ELSE NULL END) AS DTS_SEND_NEW,
                  MAX(CASE WHEN TP_MAIL = '002' THEN DTS_INSERT ELSE NULL END) AS DTS_SEND_MODIFY
           FROM CZ_SA_SOL_MAIL_SEND_WINTEC
           GROUP BY CD_COMPANY, NO_SO) MS
ON MS.CD_COMPANY = SH.CD_COMPANY AND MS.NO_SO = SH.NO_SO
LEFT JOIN (SELECT CR.CD_COMPANY, 
                  CR.CD_PARTNER, 
                  (ISNULL(CR.TOT_CREDIT,0) - (SUM(AM_IV1) + SUM(AM_IV2) + SUM(AM_IV3) - SUM(AM_RCP))) AS AM_CREDIT
		   FROM SA_PTRCREDIT CR
		   LEFT JOIN (SELECT CD_COMPANY,
							 CD_PARTNER,   
							 AM_IV AS AM_IV1,  
							 0 AM_IV2,  
							 0 AM_IV3,  
							 0 AM_RCP  
					  FROM SA_AR   
					  WHERE CD_COMPANY = @P_CD_COMPANY  
					  AND YYMM = LEFT(@P_DT_FROM,4) + '00'
					  UNION ALL
                      SELECT CD_COMPANY,
							 CD_PARTNER,   
							 0 AM_IV1,  
							 SUM(AM_IV - AM_RCP) AS AM_IV2,  
							 0 AM_IV3,  
							 0 AM_RCP  
					  FROM SA_AR  
					  WHERE	CD_COMPANY = @P_CD_COMPANY
					  AND YYMM BETWEEN LEFT(@P_DT_FROM, 4) + '01' AND LEFT(CONVERT(NVARCHAR(8), DATEADD(MM, -1, @P_DT_TO), 112), 6)
					  GROUP BY CD_COMPANY, CD_PARTNER
					  UNION ALL  
                      SELECT CD_COMPANY,
							 CD_PARTNER,   
							 0 AS AM_IV1,  
							 0 AS AM_IV2,  
							 SUM(AM_IV) AS AM_IV3,  
							 SUM(AM_RCP) AS AM_RCP  
					  FROM SA_ARD   
					  WHERE	CD_COMPANY = @P_CD_COMPANY
					  AND DT_AR BETWEEN LEFT(@P_DT_FROM, 6) + '01' AND @P_DT_TO  
					  GROUP BY CD_COMPANY, CD_PARTNER)AR 
           ON CR.CD_COMPANY = AR.CD_COMPANY AND CR.CD_PARTNER = AR.CD_PARTNER 
		   WHERE CR.CD_COMPANY = @P_CD_COMPANY
		   GROUP BY CR.CD_COMPANY, CR.CD_PARTNER, CR.TOT_CREDIT) CR
ON CR.CD_COMPANY = SH.CD_COMPANY AND CR.CD_PARTNER = SH.CD_PARTNER
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.DTS_INSERT BETWEEN @P_DT_FROM AND @P_DT_TO
AND (SH.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)
AND (SH.TP_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_TP_SO)) OR @P_MULTI_TP_SO = '' OR @P_MULTI_TP_SO IS NULL)
AND (SH.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEGRP)) OR @P_MULTI_SALEGRP = '' OR @P_MULTI_SALEGRP IS NULL)
AND (SH.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)
AND (PP.FG_DEAL = @P_FG_DEAL OR @P_FG_DEAL = '' OR @P_FG_DEAL IS NULL)
AND (@P_MULTI_SALEORG IS NULL OR @P_MULTI_SALEORG = '' OR OG.CD_SALEORG IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEORG)))
AND (@P_CD_PARTNER_GRP = MP.CD_PARTNER_GRP OR @P_CD_PARTNER_GRP ='' OR @P_CD_PARTNER_GRP IS NULL)
AND (@P_CD_PARTNER_GRP2 = MP.CD_PARTNER_GRP_2 OR @P_CD_PARTNER_GRP2 ='' OR @P_CD_PARTNER_GRP2 IS NULL)
AND (@V_YN_MFG_AUTH = 'N' OR (@V_YN_MFG_AUTH = 'Y' AND @P_NO_EMP_LOGIN = '') OR
    (@V_YN_MFG_AUTH = 'Y' AND EXISTS (SELECT 1
                                      FROM MA_MFG_AUTH AUTH
                                      WHERE SH.CD_COMPANY = AUTH.CD_COMPANY
                                      AND SH.CD_SALEGRP = AUTH.CD_AUTH
                                      AND AUTH.FG_AUTH = 'SA_GROUP'              
                                      AND AUTH.NO_EMP = @P_NO_EMP_LOGIN)))
AND (@P_NO_SO IS NULL OR @P_NO_SO = '' OR SH.NO_SO LIKE '%' + @P_NO_SO + '%')
AND (@P_ST_STAT IS NULL OR @P_ST_STAT = '' OR FG1.ST_STAT = @P_ST_STAT OR (FG1.ST_STAT IS NULL AND @P_ST_STAT = '2'))
AND ((@P_SO_STAT = '0' AND ISNULL(SL.QT_GIR, 0) > 0) OR 
     (@P_SO_STAT = '1' AND ISNULL(SL.QT_SO, 0) > ISNULL(SL.QT_GI, 0) AND ISNULL(QT_GI, 0) > 0) OR 
     (@P_SO_STAT = '2' AND ISNULL(SL.QT_SO,0) <= ISNULL(SL.QT_GI,0)) OR 
     @P_SO_STAT = '' OR @P_SO_STAT IS NULL)            
ORDER BY SH.DT_SO DESC

GO


