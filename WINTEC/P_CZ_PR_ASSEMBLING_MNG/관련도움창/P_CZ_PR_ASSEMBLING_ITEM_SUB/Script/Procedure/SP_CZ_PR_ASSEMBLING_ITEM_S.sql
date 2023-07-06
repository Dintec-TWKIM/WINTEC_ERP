USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_ASSEMBLING_ITEM_S]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_ASSEMBLING_ITEM_S]          
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_PLANT		NVARCHAR(7),
	@P_CD_ITEM		NVARCHAR(20),
	@P_CD_PITEM 	NVARCHAR(20)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT AI.CD_COMPANY,
	   AI.CD_PLANT,
	   AI.CD_ITEM,
	   MI.NM_ITEM,
	   AI.CD_PITEM,
	   MI1.NM_ITEM AS NM_PITEM,
	   AI.YN_MATCHING,
	   AI.CD_MITEM,
	   MI2.NM_ITEM AS NM_MITEM
FROM CZ_PR_ASSEMBLING_ITEM AI
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = AI.CD_COMPANY AND MI.CD_PLANT = AI.CD_PLANT AND MI.CD_ITEM = AI.CD_ITEM
LEFT JOIN MA_PITEM MI1 ON MI1.CD_COMPANY = AI.CD_COMPANY AND MI1.CD_PLANT = AI.CD_PLANT AND MI1.CD_ITEM = AI.CD_PITEM
LEFT JOIN MA_PITEM MI2 ON MI2.CD_COMPANY = AI.CD_COMPANY AND MI2.CD_PLANT = AI.CD_PLANT AND MI2.CD_ITEM = AI.CD_MITEM
WHERE AI.CD_COMPANY = @P_CD_COMPANY
AND AI.CD_PLANT = @P_CD_PLANT
AND (ISNULL(@P_CD_ITEM, '') = '' OR AI.CD_ITEM = @P_CD_ITEM)
AND (ISNULL(@P_CD_PITEM, '') = '' OR AI.CD_PITEM = @P_CD_PITEM)

GO

