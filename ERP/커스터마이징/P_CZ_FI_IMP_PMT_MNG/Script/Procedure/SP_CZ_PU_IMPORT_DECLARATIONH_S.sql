USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_IMPORT_DECLARATIONH_S]    Script Date: 2015-08-12 오후 3:03:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PU_IMPORT_DECLARATIONH_S] 
(
	@P_CD_COMPANY		NVARCHAR(7),
    @P_TP_DATE          NVARCHAR(4),
    @P_DT_FROM          NVARCHAR(8),
    @P_DT_TO            NVARCHAR(8),
    @P_NO_IMPORT        NVARCHAR(30),
    @P_NO_BL            NVARCHAR(30),
    @P_YN_DONE          NVARCHAR(1),
    @P_YN_EXPECT_REG    NVARCHAR(1)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

CREATE TABLE #CZ_PU_IMPORT_DECLARATIONH
(
	CD_COMPANY     NVARCHAR(7),
	NO_IMPORT      NVARCHAR(30),
    NO_BL          NVARCHAR(30)
)

CREATE NONCLUSTERED INDEX CZ_PU_IMPORT_DECLARATIONH ON #CZ_PU_IMPORT_DECLARATIONH (CD_COMPANY, NO_IMPORT, NO_BL)

INSERT INTO #CZ_PU_IMPORT_DECLARATIONH
SELECT IH.CD_COMPANY,
       IH.NO_IMPORT,
       IH.NO_BL
FROM CZ_PU_IMPORT_DECLARATIONH IH
WHERE IH.CD_COMPANY = @P_CD_COMPANY
AND (CASE @P_TP_DATE WHEN '001' THEN IH.DT_IMPORT 
                                ELSE LEFT(IH.DTS_INSERT, 8) END) BETWEEN @P_DT_FROM AND @P_DT_TO
AND (ISNULL(@P_NO_IMPORT, '') = '' OR IH.NO_IMPORT = @P_NO_IMPORT)
AND (ISNULL(@P_NO_BL, '') = '' OR IH.NO_BL = @P_NO_BL)
AND (ISNULL(@P_YN_DONE, '') = '' OR (@P_YN_DONE = 'N' AND ISNULL(IH.YN_DONE, 'N') = 'N') OR (@P_YN_DONE = 'Y' AND ISNULL(IH.YN_DONE, 'N') IN ('Y', 'C')))
AND (ISNULL(@P_YN_EXPECT_REG, 'N') = 'N' OR NOT EXISTS (SELECT 1 
                                                        FROM CZ_FI_IMP_PMTH FH
                                                        WHERE FH.CD_COMPANY = IH.CD_COMPANY
                                                        AND FH.NO_IMPORT = REPLACE(IH.NO_IMPORT, '-', '')))

CREATE TABLE #PU_POH
(
	CD_COMPANY     NVARCHAR(7),
	NO_BL          NVARCHAR(100),
    CD_PJT         NVARCHAR(20),
    NM_EMP         NVARCHAR(40)
)

CREATE NONCLUSTERED INDEX PU_POH ON #PU_POH (CD_COMPANY, NO_BL, CD_PJT, NM_EMP)

INSERT INTO #PU_POH
SELECT PH.CD_COMPANY,
       A.VALUE AS NO_BL,
       PH.CD_PJT,
       ME.NM_KOR AS NM_EMP
FROM PU_POH PH
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = PH.CD_COMPANY AND ME.NO_EMP = PH.NO_EMP
CROSS APPLY STRING_SPLIT(PH.NO_BL, ',') A
WHERE PH.CD_COMPANY = @P_CD_COMPANY
AND EXISTS (SELECT 1 
            FROM #CZ_PU_IMPORT_DECLARATIONH IH
            WHERE IH.CD_COMPANY = PH.CD_COMPANY
            AND IH.NO_BL = A.VALUE)

;WITH A AS
(
    SELECT IH.CD_COMPANY,
           IH.NO_IMPORT,
           IH.DT_IMPORT,
           IH.DT_PAYMENT,
           IH.CD_OFFICE,
           IH.DT_ARRIVAL,
           IH.NO_BL,
           IH.NO_BL_MASTER,
           IH.NO_CARGO,
           IH.DECLARANT,
           IH.IMPORTER,
           IH.TAXPAYER,
           IH.FORWARDER,
           IH.TRADER,
           IH.GROSS_WEIGHT,
           IH.ARRIVAL_PORT,
           IH.EXPORT_COUNTRY,
           IH.ITEM_NAME,
           IH.CURRENCY,
           IH.EXCHANGE_RATE,
           IH.TAXABLE_VAT,
           IH.TAX_CUSTOMS,
           IH.TAX_CONSUMPTION,
           IH.TAX_ENERGY,
           IH.TAX_LIQUOR,
           IH.TAX_EDUCATION,
           IH.TAX_RURAL,
           IH.TAX_VAT,
           IH.TAX_LATE_REPORT,
           IH.TAX_NO_REPORT,
           IH.YN_DONE,
           (CASE ISNULL(IH.YN_DONE, 'N') WHEN 'Y' THEN '완료'
                                         WHEN 'C' THEN '임의처리'
                                         ELSE '미완료' END) AS TP_DONE,
           IH.ID_INSERT,
           IH.DTS_INSERT,
           MF.NAME + '(' + MF.CNT + ')' AS FILE_PATH_MNG,
           (SELECT TOP 1 DC_LOG 
            FROM CZ_PU_IMPORT_DECLARATION_LOG LG
            WHERE LG.CD_COMPANY = IH.CD_COMPANY
            AND LG.NO_IMPORT = IH.NO_IMPORT
            ORDER BY LG.SEQ DESC) AS DC_LOG
    FROM CZ_PU_IMPORT_DECLARATIONH IH
    LEFT JOIN (SELECT CD_COMPANY,
    				  CD_FILE,
    				  MAX(FILE_NAME) AS NAME,
    				  CONVERT(VARCHAR, COUNT(1)) AS CNT 
    		   FROM MA_FILEINFO
    		   WHERE CD_MODULE = 'FI'
    		   AND ID_MENU = 'P_CZ_FI_IMP_PMT_MNG'
    		   GROUP BY CD_COMPANY, CD_MODULE, ID_MENU, CD_FILE) MF 
    ON MF.CD_COMPANY = IH.CD_COMPANY AND MF.CD_FILE = REPLACE(IH.NO_IMPORT, '-', '') + '_' + IH.CD_COMPANY
    WHERE IH.CD_COMPANY = @P_CD_COMPANY
    AND EXISTS (SELECT 1 
                FROM #CZ_PU_IMPORT_DECLARATIONH IH1
                WHERE IH1.CD_COMPANY = IH.CD_COMPANY
                AND IH1.NO_IMPORT = IH.NO_IMPORT)
)
SELECT 'N' AS S,
       A.NO_IMPORT,
       A.DT_IMPORT,
       A.DT_PAYMENT,
       A.CD_OFFICE,
       A.DT_ARRIVAL,
       A.NO_BL,
       A.NO_BL_MASTER,
       A.NO_CARGO,
       A.DECLARANT,
       A.IMPORTER,
       A.TAXPAYER,
       A.FORWARDER,
       A.TRADER,
       A.GROSS_WEIGHT,
       A.ARRIVAL_PORT,
       A.EXPORT_COUNTRY,
       A.ITEM_NAME,
       A.CURRENCY,
       A.EXCHANGE_RATE,
       A.TAXABLE_VAT,
       A.TAX_CUSTOMS,
       A.TAX_CONSUMPTION,
       A.TAX_ENERGY,
       A.TAX_LIQUOR,
       A.TAX_EDUCATION,
       A.TAX_RURAL,
       A.TAX_VAT,
       A.TAX_LATE_REPORT,
       A.TAX_NO_REPORT,
       A.YN_DONE,
       A.TP_DONE,
       A.ID_INSERT,
       A.DTS_INSERT,
       A.FILE_PATH_MNG,
       A.DC_LOG,
       (SELECT STRING_AGG(CONVERT(NVARCHAR(MAX), PH.CD_PJT), ',') 
        FROM (SELECT PH.CD_PJT 
              FROM #PU_POH PH
              WHERE PH.CD_COMPANY = A.CD_COMPANY
              AND PH.NO_BL = A.NO_BL
              AND ISNULL(A.NO_BL, '') <> '' 
              GROUP BY PH.CD_PJT) PH) AS CD_PJT,
       (SELECT STRING_AGG(CONVERT(NVARCHAR(MAX), PH.NM_EMP), ',') 
        FROM (SELECT PH.NM_EMP 
              FROM #PU_POH PH
              WHERE PH.CD_COMPANY = A.CD_COMPANY
              AND PH.NO_BL = A.NO_BL
              AND ISNULL(A.NO_BL, '') <> '' 
              GROUP BY PH.NM_EMP) PH) AS NM_EMP
FROM A

GO