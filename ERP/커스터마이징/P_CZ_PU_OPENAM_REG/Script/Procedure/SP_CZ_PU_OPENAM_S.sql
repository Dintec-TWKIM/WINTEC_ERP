USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_OPENAM_S]    Script Date: 2016-10-25 오전 9:33:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************      
**  System : 구매자재      
**  Sub System : 구매자재관리       
**  Page  : 구매/자재기준관리      
**  Desc  : 기초재고등록 조회(프리폼 초기화)    
**  Return Values      
**      
**  작    성    자  :       
**  작    성    일 :       
**  수    정    자     : 허성철      
*********************************************      
** Change History      
*********************************************      
*********************************************/     
ALTER PROCEDURE [NEOE].[SP_CZ_PU_OPENAM_S]
(      
	@P_CD_COMPANY		NVARCHAR(7),		-- 회사
	@P_CD_PLANT			NVARCHAR(7),		-- 공장
	@P_YM_STANDARD		NCHAR(6),			-- 기준년월
	@P_NO_EMP			NVARCHAR(10),		-- 담당자
	@P_CLS_ITEM			NVARCHAR(3),
	@P_CD_ITEM			NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_FSIZE_AM  NUMERIC(3,0),  
		@V_UPDOWN_AM NVARCHAR(3),  
		@V_FORMAT_AM NVARCHAR(50)  

--- 원화금액 설정값 가져오기 - 통제환경설정관리에 등록된 데이타를 받아오기 위한 프로시져를 실행해 OUTPUT으로 받아옵니다  
EXEC UP_SF_GETUNIT_AM @P_CD_COMPANY, 'PU', 'S', @V_FSIZE_AM OUTPUT, @V_UPDOWN_AM OUTPUT, @V_FORMAT_AM OUTPUT  
IF @@ERROR <> 0 RETURN    
 
SELECT A.YM_STANDARD,
	   A.CD_PLANT,
	   A.YY_AIS,
	   A.YM_FSTANDARD,
	   A.CD_DEPT,
	   A.NO_EMP,
	   A.DT_INPUT,
	   A.DC50_PO, 
	   C.NM_DEPT,
	   D.NM_KOR,
	   F.NM_PLANT
FROM MM_AMINVH A	
LEFT JOIN MA_DEPT C ON C.CD_COMPANY = A.CD_COMPANY AND C.CD_DEPT = A.CD_DEPT
LEFT JOIN MA_EMP D ON D.CD_COMPANY = A.CD_COMPANY AND D.NO_EMP = A.NO_EMP
LEFT JOIN MA_PLANT F ON F.CD_COMPANY = A.CD_COMPANY AND F.CD_PLANT = A.CD_PLANT
WHERE A.CD_COMPANY = @P_CD_COMPANY
AND A.CD_PLANT = @P_CD_PLANT
AND A.YM_STANDARD = @P_YM_STANDARD
AND (ISNULL(@P_NO_EMP, '') = '' OR A.NO_EMP = @P_NO_EMP)

SELECT 'N' AS S, 
	   A.CD_ITEM,
	   A.QT_BAS,
	   A.UM_BAS, 
	   --A.AM_BAS, 
       NEOE.FN_SF_GETUNIT_AM('PU', '', @V_FSIZE_AM, @V_UPDOWN_AM, @V_FORMAT_AM, A.AM_BAS) AS AM_BAS, 
       B.CLS_ITEM,
	   B.NM_ITEM,
	   B.STND_ITEM,
	   B.UNIT_IM, 
	   Z.NM_SYSDEF AS NM_CLSITEM,
	   A.YM_STANDARD,
	   A.CD_PLANT
FROM MM_AMINVL A 
JOIN MM_AMINVH H ON H.CD_COMPANY = A.CD_COMPANY AND H.CD_PLANT = A.CD_PLANT AND H.YM_STANDARD = A.YM_STANDARD
JOIN MA_PITEM B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_ITEM = B.CD_ITEM AND A.CD_PLANT = B.CD_PLANT 
LEFT JOIN MA_CODEDTL Z ON Z.CD_COMPANY = B.CD_COMPANY AND Z.CD_SYSDEF = B.CLS_ITEM AND Z.CD_FIELD ='MA_B000010'
WHERE A.CD_COMPANY = @P_CD_COMPANY
AND A.CD_PLANT = @P_CD_PLANT
AND A.YM_STANDARD = @P_YM_STANDARD
AND (ISNULL(@P_NO_EMP, '') = '' OR H.NO_EMP = @P_NO_EMP)
AND (ISNULL(@P_CLS_ITEM, '') = '' OR B.CLS_ITEM = @P_CLS_ITEM)
AND (ISNULL(@P_CD_ITEM, '') = '' OR B.CD_ITEM = @P_CD_ITEM)

GO

