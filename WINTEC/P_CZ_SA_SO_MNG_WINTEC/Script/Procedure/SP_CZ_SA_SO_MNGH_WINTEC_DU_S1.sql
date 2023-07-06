USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SO_MNGH_WINTEC_DU_S1]    Script Date: 2019-11-14 오후 3:11:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_SO_MNGH_WINTEC_DU_S1]        
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

DECLARE @V_YN_MFG_AUTH  NVARCHAR(1)

SELECT @V_YN_MFG_AUTH = ISNULL(YN_MFG_AUTH, 'N')
FROM MA_ENV
WHERE CD_COMPANY = @P_CD_COMPANY

SELECT SH.NO_SO,
       SL.SEQ_SO,
	   SH.CD_PARTNER,
	   MP.LN_PARTNER,
	   SH.DT_SO,
	   SL.CD_USERDEF3,
	   SL.TXT_USERDEF6,
	   SL.CD_ITEM,
	   MI.NM_ITEM,
	   MI.NO_DESIGN,
	   SH.NO_PO_PARTNER,
	   SL.QT_SO,
       ISNULL(SL.QT_GIR, 0) AS QT_GIR,
       ISNULL(SL.QT_GI, 0) AS QT_GI,
       (SL.QT_SO - ISNULL(SL.QT_GI, 0)) AS QT_REMAIN,
	   SL.DT_EXPECT,
	   SL.DT_DUEDATE,
	   SL.DT_REQGI,
	   OL.DT_IO,
	   DATEDIFF(DAY, SL.DT_DUEDATE, ISNULL(OL.DT_IO, CONVERT(CHAR(8), GETDATE(), 112))) AS DT_DELAY,
	   SL.STA_SO,
	   SL.TXT_USERDEF5,
       SH.CD_EXCH,
       CD1.NM_SYSDEF AS NM_EXCH,
	   SL.UM_SO,
	   SL.AM_SO,
	   SL.AM_WONAMT,
       SL.AM_VAT,
	   (ISNULL(SL.AM_WONAMT, 0) + ISNULL(SL.AM_VAT, 0)) AS AM_SUM,
	   SL.TXT_USERDEF3,
       SL.TXT_USERDEF6,
       SL.TXT_USERDEF7,
	   SH.DC_RMK_TEXT,

       SL.DC1,  
       SL.DC2,
       SL.FG_USE,
       SL.FG_USE2,
       SL.TXT_USERDEF1,
       SL.TXT_USERDEF2,
	   SL.NUM_USERDEF3,
       SL.YN_OPTION,
       SL.CD_USERDEF1,
       SL.CD_USERDEF2,
       SH.DC_RMK1,
       SH.DTS_INSERT,
	   (CASE WHEN SL.NUM_USERDEF4 <> 0 THEN SL.NUM_USERDEF4 
	   								   ELSE (SELECT TOP 1 NM_SYSDEF
	   									     FROM MA_CODEDTL 
	   									     WHERE CD_COMPANY = SH.CD_COMPANY
	   									     AND CD_FIELD = 'CZ_WIN0005'
	   									     AND CD_FLAG1 = SH.CD_PARTNER
	   									     AND CD_FLAG2 <= DATEDIFF(DAY, SL.DT_DUEDATE, ISNULL(OL.DT_IO, CONVERT(CHAR(8), GETDATE(), 112)))
	   									     ORDER BY CONVERT(INT, NM_SYSDEF)) END) AS QT_SCORE,
       SH.TXT_USERDEF2 AS NO_LC,
       (CASE WHEN EXISTS (SELECT 1 
                          FROM PR_BOM_ECN EB
                          WHERE EB.CD_COMPANY = SL.CD_COMPANY
                          AND EB.CD_ITEM = SL.CD_ITEM) THEN 'Y' ELSE 'N' END) AS YN_EBOM,
       (CASE WHEN EXISTS (SELECT 1 
                          FROM PR_BOM BM
                          WHERE BM.CD_COMPANY = SL.CD_COMPANY
                          AND BM.CD_ITEM = SL.CD_ITEM) THEN 'Y' ELSE 'N' END) AS YN_BOM
FROM SA_SOH SH
JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN MA_CODEDTL CD1 ON CD1.CD_COMPANY = SH.CD_COMPANY AND CD1.CD_FIELD = 'MA_B000005' AND CD1.CD_SYSDEF = SH.CD_EXCH
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_PLANT = SL.CD_PLANT AND MI.CD_ITEM = SL.CD_ITEM
LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
				  MAX(OH.DT_IO) AS DT_IO
		   FROM MM_QTIOH OH
		   JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
		   WHERE OL.FG_PS = '2'
           AND OL.CD_QTIOTP NOT IN ('500')
		   GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) OL
ON OL.CD_COMPANY = SL.CD_COMPANY AND OL.NO_PSO_MGMT = SL.NO_SO AND OL.NO_PSOLINE_MGMT = SL.SEQ_SO
LEFT JOIN MA_SALEGRP SG	ON SG.CD_COMPANY = SH.CD_COMPANY AND SG.CD_SALEGRP = SH.CD_SALEGRP
LEFT JOIN MA_SALEORG OG ON OG.CD_COMPANY = SG.CD_COMPANY AND OG.CD_SALEORG = SG.CD_SALEORG
LEFT JOIN SA_PJTH_PRE PP ON PP.CD_COMPANY = SH.CD_COMPANY AND PP.CD_PJT = SH.NO_PROJECT
LEFT JOIN FI_GWDOCU FG1 ON FG1.CD_COMPANY = SH.CD_COMPANY AND FG1.NO_DOCU = SH.NO_SO
LEFT JOIN SA_PROJECTL PL ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_PROJECT = SL.NO_PROJECT AND PL.SEQ_PROJECT = SL.SEQ_PROJECT
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SL.DT_DUEDATE BETWEEN @P_DT_FROM AND @P_DT_TO
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
ORDER BY SH.NO_SO DESC, SL.SEQ_SO

GO