USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MATCHING_ITEM_AUTO]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_MATCHING_ITEM_AUTO]          
(
	@P_CD_COMPANY	NVARCHAR(7)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE 
FROM CZ_PR_MATCHING_ITEM
WHERE CD_COMPANY = @P_CD_COMPANY

;WITH A AS
(
	SELECT MI.CD_COMPANY, 
		   MI.CD_PLANT, 
		   MI.CD_ITEM,
		   MI.NM_ITEM
	FROM MA_PITEM MI
	WHERE MI.CD_COMPANY = @P_CD_COMPANY
	AND EXISTS (SELECT 1 
			    FROM CZ_PR_ROUT_INSP RI
				WHERE RI.CD_COMPANY = MI.CD_COMPANY
				AND RI.CD_PLANT = MI.CD_PLANT
				AND RI.CD_ITEM = MI.CD_ITEM
				AND RI.YN_ASSY = 'Y'
				AND RI.CD_CLEAR_GRP IS NOT NULL )	
)
INSERT INTO CZ_PR_MATCHING_ITEM
(
	CD_COMPANY,
	CD_PLANT,
	CD_ITEM,
	CD_PITEM,
	TP_POS,
	ID_INSERT,
	DTS_INSERT
)
SELECT BM.CD_COMPANY,
	   BM.CD_PLANT,
	   BM.CD_ITEM,
	   BM.CD_MATL,
	   MC.CD_FLAG1 AS TP_POS,
	   'SYSTEM' AS ID_INSERT,
	   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
FROM PR_BOM BM
JOIN A ON A.CD_COMPANY = BM.CD_COMPANY AND A.CD_PLANT = BM.CD_PLANT AND A.CD_ITEM = BM.CD_MATL
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = A.CD_COMPANY AND MC.CD_FIELD = 'CZ_WIN0014' AND MC.NM_SYSDEF = A.NM_ITEM
WHERE BM.CD_COMPANY = @P_CD_COMPANY
AND BM.DT_END >= CONVERT(CHAR(8), GETDATE(), 112)
AND NOT EXISTS (SELECT 1 
				FROM CZ_PR_MATCHING_ITEM MI
				WHERE MI.CD_COMPANY = BM.CD_COMPANY
				AND MI.CD_PLANT = BM.CD_PLANT
				AND MI.CD_ITEM = BM.CD_ITEM
				AND MI.CD_PITEM = BM.CD_MATL)

GO

