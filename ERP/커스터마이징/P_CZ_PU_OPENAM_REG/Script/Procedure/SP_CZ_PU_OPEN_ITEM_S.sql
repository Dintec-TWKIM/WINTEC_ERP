USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_OPEN_ITEM_S]    Script Date: 2016-10-26 오후 3:43:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************          
**  System : 구매자재          
**  Sub System : 구매자재관리           
**  Page  : 구매/자재기준관리          
**  Desc  : 품목전개 버턴 정보 검색
**  Return Values          
**          
**  작    성    자  :           
**  작    성    일 :           
**  수    정    자     : 허성철          
*********************************************          
** Change History          
*********************************************          
*********************************************/   
ALTER PROCEDURE [NEOE].[SP_CZ_PU_OPEN_ITEM_S]  
(
	@P_CD_COMPANY	NVARCHAR(7),        -- 회사  
	@P_CD_PLANT		NVARCHAR(7),        -- 공장
	@P_YM_STANDARD	NCHAR(6),           -- 기준년월
	@P_CD_SL		NVARCHAR(7),          
	@P_CLS_ITEM		NCHAR(3),
	@P_CD_ITEM		NVARCHAR(20)
)  
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   B.CD_PLANT,
	   @P_YM_STANDARD AS YM_STANDARD,
	   @P_CD_SL AS CD_SL,
	   SL.NM_SL AS NM_SL,   
	   B.CD_ITEM,  
	   B.NM_ITEM,  
	   B.STND_ITEM,   
	   B.UNIT_IM,  
	   B.CLS_ITEM,  
	   0 AS QT_BAS,  
	   0 AS UM_BAS,   
	   0 AS AM_BAS,  
	   0 AS QT_GOOD_INV,   
	   0 AS QT_REJECT_INV,   
	   0 AS QT_INSP_INV,   
	   0 AS QT_TRANS_INV,  
	   Z.NM_SYSDEF AS NM_CLSITEM,
	   (CASE WHEN B.FG_SERNO  = '002' THEN 'YES' ELSE 'NO' END) AS NO_LOT
FROM MA_PITEM B 
LEFT JOIN MA_CODEDTL Z ON Z.CD_COMPANY = B.CD_COMPANY AND Z.CD_SYSDEF = B.CLS_ITEM AND Z.CD_FIELD ='MA_B000010'
LEFT JOIN MA_SL SL ON SL.CD_COMPANY = @P_CD_COMPANY AND SL.CD_PLANT = @P_CD_PLANT AND SL.CD_SL = @P_CD_SL
WHERE B.CD_COMPANY = @P_CD_COMPANY  
AND B.CD_PLANT = @P_CD_PLANT  
AND (ISNULL(@P_CLS_ITEM, '') = '' OR B.CLS_ITEM = @P_CLS_ITEM)
AND (ISNULL(@P_CD_ITEM, '') = '' OR B.CD_ITEM = @P_CD_ITEM)  
AND B.YN_USE = 'Y'  

GO

