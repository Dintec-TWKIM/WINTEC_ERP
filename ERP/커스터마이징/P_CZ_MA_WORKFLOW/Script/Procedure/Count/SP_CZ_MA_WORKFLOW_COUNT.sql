USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_COUNT]    Script Date: 2018-08-21 오후 1:59:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_COUNT]  
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_TP_STEP				NVARCHAR(2),
	@P_NO_KEY				NVARCHAR(20),
	@P_ID_USER				NVARCHAR(15),
	@P_CD_SALEORG			NVARCHAR(7),
	@P_CD_SALEGRP			NVARCHAR(500)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT COUNT(1) AS CNT 
FROM CZ_MA_WORKFLOWH WH 
LEFT JOIN CZ_MA_WORKFLOWH WH1 ON WH1.CD_COMPANY = WH.CD_COMPANY AND WH1.NO_KEY = WH.NO_KEY AND WH1.TP_STEP = '21'
LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = WH.CD_COMPANY AND QH.NO_FILE = WH.NO_KEY
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = WH.CD_COMPANY AND SH.NO_SO = WH.NO_KEY
LEFT JOIN MA_USER MU ON MU.CD_COMPANY = WH.CD_COMPANY AND MU.ID_USER = WH.ID_SALES
WHERE WH.TP_STEP = @P_TP_STEP
AND (WH.YN_DONE IS NULL OR WH.YN_DONE = 'N')
AND (WH.TP_STEP = '07'
  OR (WH.TP_STEP IN ('01', '02', '03', '04', '05', '06', '21') AND (QH.YN_CLOSE IS NULL OR QH.YN_CLOSE = 'N'))
  OR (WH.TP_STEP IN ('08', '09', '10', '11', '13') AND (SH.YN_CLOSE IS NULL OR SH.YN_CLOSE = 'N')))
AND (ISNULL(@P_CD_COMPANY, '') = '' OR WH.CD_COMPANY = @P_CD_COMPANY)
AND (ISNULL(@P_NO_KEY, '') = '' OR WH.NO_KEY = @P_NO_KEY)
AND (ISNULL(@P_CD_SALEGRP, '') = '' OR MU.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_SALEGRP)))
AND (ISNULL(@P_CD_SALEORG, '') = '' OR EXISTS (SELECT 1 
											   FROM MA_SALEGRP
											   WHERE CD_COMPANY = MU.CD_COMPANY
											   AND CD_SALEGRP = MU.CD_SALEGRP
											   AND CD_SALEORG = @P_CD_SALEORG))
AND (ISNULL(@P_ID_USER, '') = ''
  OR (WH.TP_STEP = '01' AND WH.ID_SALES = @P_ID_USER)
  --OR (WH.TP_STEP = '02' AND (CASE WHEN WH.CD_COMPANY = 'K100' AND
		--							   WH.NO_KEY LIKE 'FB%' AND
		--							   EXISTS (SELECT 1 
		--									   FROM CZ_MA_WORKFLOWL
		--									   WHERE CD_COMPANY = WH.CD_COMPANY
		--									   AND NO_KEY = WH.NO_KEY
		--									   AND TP_STEP = '01'
		--									   AND (ISNULL(YN_PARSING, 'N') = 'Y')) AND
		--							   NOT EXISTS(SELECT 1 
		--									      FROM CZ_MA_WORKFLOWL
		--									      WHERE CD_COMPANY = WH.CD_COMPANY
		--									      AND NO_KEY = WH.NO_KEY
		--									      AND TP_STEP = '01'
		--									      AND (NM_FILE LIKE '현대웹%' OR 
		--											   NM_FILE LIKE '정아%')) THEN WH.ID_SALES 
		--																	  ELSE WH.ID_TYPIST END) = @P_ID_USER)
  OR (WH.TP_STEP = '02' AND WH.ID_TYPIST = @P_ID_USER)
  OR (WH.TP_STEP = '03' AND WH.ID_SALES = @P_ID_USER)
  OR (WH.TP_STEP = '04' AND WH.ID_TYPIST = @P_ID_USER)
  OR (WH.TP_STEP = '05' AND ((ISNULL(QH.NO_EMP_QTN, WH.ID_SALES) = @P_ID_USER) OR (WH1.YN_DONE = 'Y' AND WH.ID_SALES = @P_ID_USER)))
  OR (WH.TP_STEP = '06' AND WH.ID_TYPIST = @P_ID_USER)
  OR (WH.TP_STEP = '07' AND WH.ID_SALES = @P_ID_USER)
  OR (WH.TP_STEP = '08' AND WH.ID_PUR = @P_ID_USER)
  OR (WH.TP_STEP = '09' AND WH.ID_PUR = @P_ID_USER)
  OR (WH.TP_STEP = '10' AND WH.ID_PUR = @P_ID_USER)
  OR (WH.TP_STEP = '11' AND WH.ID_PUR = @P_ID_USER)
  OR (WH.TP_STEP = '13' AND WH.ID_LOG = @P_ID_USER)
  OR (WH.TP_STEP = '21' AND WH.ID_SALES = @P_ID_USER))

GO

