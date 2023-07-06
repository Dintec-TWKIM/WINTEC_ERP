USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_OPENQT_C]    Script Date: 2016-10-26 오후 3:52:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*******************************************        
**  System : 구매자재        
**  Sub System : 구매자재관리         
**  Page  : 구매/자재기준관리        
**  Desc  : 기초재고등록 헤더 카운터 조회      
**  Return Values        
**        
**  작    성    자  :         
**  작    성    일 :         
**  수    정    자     : 허성철        
*********************************************        
** Change History        
*********************************************        
*********************************************/  
ALTER PROCEDURE [NEOE].[SP_CZ_PU_OPENQT_C]       
(
	@P_CD_COMPANY	NVARCHAR(7),	-- 회사          
	@P_CD_PLANT		NVARCHAR(7),	-- 공장          
	@P_YM_STANDARD	NCHAR(6),		-- 기준년월          
	@P_CD_SL		NVARCHAR(7),	-- 보관장소   
	@P_COUNT		INT OUTPUT       
)          
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT  @P_COUNT = COUNT(*)
FROM    MM_OPENQTH  
WHERE   CD_COMPANY  = @P_CD_COMPANY          
AND     CD_PLANT    = @P_CD_PLANT          
AND     YM_STANDARD = @P_YM_STANDARD          
AND     CD_SL = @P_CD_SL

GO