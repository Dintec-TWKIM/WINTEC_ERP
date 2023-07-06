USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_ASSEMBLING_ITEM_AUTO]    Script Date: 2023-04-25 오후 4:32:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_ASSEMBLING_ITEM_AUTO]          
(
	@P_CD_COMPANY	NVARCHAR(7)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE 
FROM CZ_PR_ASSEMBLING_ITEM
WHERE CD_COMPANY = @P_CD_COMPANY

INSERT INTO CZ_PR_ASSEMBLING_ITEM
(
	CD_COMPANY,
	CD_PLANT,
	CD_ITEM,
	CD_PITEM,
	QT_ITEM,
	YN_MATCHING,
	CD_MITEM,
	ID_INSERT,
	DTS_INSERT
)
SELECT MI.CD_COMPANY,
	   MI.CD_PLANT,
	   MI.CD_ITEM,
	   ISNULL(PM.CD_PITEM, BM.CD_MATL) AS CD_PITEM,
	   BM.QT_ITEM_NET,
	   (CASE WHEN PM.CD_PITEM IS NOT NULL THEN 'Y' ELSE 'N' END) AS YN_MATCHING,
	   PM.CD_ITEM AS CD_MITEM,
	   'SYSTEM' AS ID_INSERT,
	   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
FROM MA_PITEM MI
JOIN PR_ROUT_ASN BM ON BM.CD_COMPANY = MI.CD_COMPANY AND BM.CD_ITEM = MI.CD_ITEM
LEFT JOIN MA_PITEM MI1 ON MI1.CD_COMPANY = BM.CD_COMPANY AND MI1.CD_ITEM = BM.CD_MATL
LEFT JOIN CZ_PR_MATCHING_ITEM PM ON PM.CD_COMPANY = BM.CD_COMPANY AND PM.CD_PLANT = BM.CD_PLANT AND PM.CD_ITEM = BM.CD_MATL
WHERE MI.CD_COMPANY = @P_CD_COMPANY
AND MI1.TP_PROC = 'M'
AND MI.CD_ITEM NOT IN ('2VC70006')
AND EXISTS (SELECT 1
			FROM PR_ROUT_L RL
			LEFT JOIN PR_WCOP OP ON OP.CD_COMPANY = RL.CD_COMPANY AND OP.CD_PLANT = RL.CD_PLANT AND OP.CD_WC = RL.CD_WC AND OP.CD_WCOP = RL.CD_WCOP
			WHERE RL.CD_COMPANY = MI.CD_COMPANY
			AND RL.CD_ITEM = MI.CD_ITEM
			AND OP.NM_OP LIKE '%조립%')
AND EXISTS (SELECT 1 
			FROM PR_ROUT_L RL
			WHERE RL.CD_COMPANY = BM.CD_COMPANY
			AND RL.CD_PLANT = BM.CD_PLANT
			AND RL.CD_ITEM = BM.CD_ITEM
			AND RL.NO_OPPATH = BM.NO_OPPATH
			AND RL.TP_OPPATH = BM.TP_OPPATH
			AND RL.CD_OP = BM.CD_OP
			AND RL.YN_USE = 'Y')

