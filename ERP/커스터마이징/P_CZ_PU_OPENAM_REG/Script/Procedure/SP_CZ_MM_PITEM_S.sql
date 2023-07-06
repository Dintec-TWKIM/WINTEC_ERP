USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MM_PITEM_S]    Script Date: 2016-10-26 오후 3:35:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************                        
*********************************************/       
ALTER PROCEDURE [NEOE].[SP_CZ_MM_PITEM_S]  
(
	@P_CD_COMPANY		NVARCHAR(7),   --회사코드
	@P_CD_PLANT			NVARCHAR(7),
	@P_MULTI_CD_ITEM	TEXT  
)  
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT ITEM.CD_COMPANY, 
	   ITEM.CD_PLANT,
	   CD_ITEM,
	   NM_ITEM,
	   STND_ITEM,
	   UNIT_IM,
	   FG_SERNO,
	   MP.NM_PLANT,
	   SL.CD_SL,
	   SL.NM_SL, 
	   MPG.CD_PURGRP,
	   MPG.NM_PURGRP,     
       ISNULL(ITEM.UNIT_PO_FACT,1) AS UNIT_PO_FACT,
	   ISNULL(ITEM.WEIGHT,1) AS WEIGHT,    
       ITEM.FG_SERNO,    
       IG.NM_ITEMGRP AS GRP_ITEMNM, -- 제품군명       
       STND_DETAIL_ITEM,    
       BARCODE,  
       ITEM.CLS_ITEM,  
       C1.NM_SYSDEF AS NM_CLS_ITEM ,
       ITEM.UNIT_PO,
       (CASE WHEN ITEM.FG_SERNO  = '002' THEN 'YES' ELSE 'NO' END) AS NO_LOT,
       ITEM.FG_SLQC,
       ITEM.PARTNER,
       PART.LN_PARTNER,
       ITEM.GRP_MFG,
       MC8.NM_SYSDEF AS NM_GRPMFG,
       ITEM.LT_ITEM,
       ITEM.NM_USERDEF1,
       ITEM.NM_USERDEF2,
       ITEM.NM_MAKER,
       ITEM.QT_MIN,
       ITEM.QT_MAX,
       ITEM.LOTSIZE,
       ITEM.GRP_ITEM,
       ITEM.WEIGHT,
       ITEM.TP_PROC,
       MC9.NM_SYSDEF AS NM_TP_PROC,
       ITEM.MAT_ITEM,
       ITEM.NO_DESIGN
FROM MA_PITEM ITEM       
LEFT JOIN MA_PLANT MP ON ITEM.CD_PLANT = MP.CD_PLANT AND ITEM.CD_COMPANY = MP.CD_COMPANY
LEFT JOIN MA_SL SL ON ITEM.CD_SL = SL.CD_SL AND ITEM.CD_BIZAREA = SL.CD_BIZAREA AND ITEM.CD_PLANT = SL.CD_PLANT AND ITEM.CD_COMPANY = SL.CD_COMPANY
LEFT JOIN MA_PURGRP MPG ON ITEM.CD_PURGRP = MPG.CD_PURGRP AND ITEM.CD_COMPANY = MPG.CD_COMPANY      
LEFT JOIN MA_ITEMGRP IG ON ITEM.CD_COMPANY = IG.CD_COMPANY AND ITEM.GRP_ITEM = IG.CD_ITEMGRP
LEFT JOIN MA_CODEDTL C1 ON C1.CD_FIELD = 'MA_B000010' AND C1.CD_SYSDEF = ITEM.CLS_ITEM AND C1.CD_COMPANY = ITEM.CD_COMPANY
LEFT JOIN MA_PARTNER PART ON PART.CD_PARTNER = ITEM.PARTNER AND PART.CD_COMPANY = ITEM.CD_COMPANY
LEFT JOIN MA_CODEDTL MC8 ON MC8.CD_FIELD ='MA_B000066' AND MC8.CD_SYSDEF = ITEM.GRP_MFG AND MC8.CD_COMPANY = ITEM.CD_COMPANY    
LEFT JOIN MA_CODEDTL MC9 ON MC9.CD_FIELD ='MA_B000009' AND MC9.CD_SYSDEF = ITEM.TP_PROC AND MC9.CD_COMPANY = ITEM.CD_COMPANY   
WHERE ITEM.CD_COMPANY = @P_CD_COMPANY      
AND ITEM.CD_PLANT = @P_CD_PLANT      
AND ITEM.CD_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@P_MULTI_CD_ITEM))

GO