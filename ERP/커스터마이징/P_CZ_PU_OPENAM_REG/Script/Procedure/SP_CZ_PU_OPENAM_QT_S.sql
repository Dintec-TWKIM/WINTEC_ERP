USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_OPENAM_QT_S]    Script Date: 2016-10-26 오후 3:42:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*******************************************        
**  System : 구매자재        
**  Sub System : 구매자재관리         
**  Page  : 구매/자재기준관리        
**  Desc  : 자재적용 버턴 정보 검색         
**  Return Values        
**        
**  작    성    자  :         
**  작    성    일 :         
**  수    정    자     : 허성철        
*********************************************        
** Change History        
*********************************************        
*********************************************/

ALTER PROCEDURE [NEOE].[SP_CZ_PU_OPENAM_QT_S]  
(
	@P_CD_COMPANY	NVARCHAR(7),        -- 회사  
	@P_CD_PLANT		NVARCHAR(7),        -- 공장          
	@P_YM_STANDARD	NCHAR(6),           -- 기준년월
	@P_CLS_ITEM		NVARCHAR(3),
	@P_CD_ITEM		NVARCHAR(20)          
)  
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
  
SELECT 'N' AS S,
	   A.CD_PLANT,
	   A.YM_STANDARD AS YM_STANDARD,
	   A.CD_ITEM,
	   B.CLS_ITEM,
	   B.NM_ITEM,
	   B.STND_ITEM,
	   B.UNIT_IM,  
	   SUM(A.QT_GOOD_INV + A.QT_REJECT_INV + A.QT_INSP_INV) AS QT_BAS,   
	   0 AS UM_BAS,
	   0 AS AM_BAS,  
	   Z.NM_SYSDEF AS NM_CLSITEM
FROM MM_OPENQTL A      
JOIN MA_PITEM B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_ITEM = B.CD_ITEM AND A.CD_PLANT = B.CD_PLANT
LEFT JOIN MA_CODEDTL Z ON B.CD_COMPANY = Z.CD_COMPANY AND B.CLS_ITEM = Z.CD_SYSDEF AND Z.CD_FIELD ='MA_B000010'
WHERE A.CD_COMPANY = @P_CD_COMPANY  
AND A.CD_PLANT = @P_CD_PLANT  
AND A.YM_STANDARD = @P_YM_STANDARD
AND (ISNULL(@P_CLS_ITEM, '') = '' OR B.CLS_ITEM = @P_CLS_ITEM)
AND (ISNULL(@P_CD_ITEM, '') = '' OR B.CD_ITEM = @P_CD_ITEM)
GROUP BY A.CD_ITEM,
		 A.CD_PLANT,
		 A.YM_STANDARD, 
		 B.CLS_ITEM,
		 B.NM_ITEM,
		 B.STND_ITEM,
		 B.UNIT_IM,
		 Z.NM_SYSDEF  

GO

