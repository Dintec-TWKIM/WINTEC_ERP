USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_OPENQT_S]    Script Date: 2016-10-25 오전 9:52:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************  
**  System  : 구매/자재관리  
**  Sub System  : 구매자재기준관리  
**  Page  : 기초재고등록  
**  Desc  : 기초재고등록 조회
**  참  고  :   
**  Return Values  
**  
**  작    성    자  : 
**  작    성    일 : 
**  수    정    자      :  허성철
**  수    정    내   용 : 
*********************************************  
** Change History  
*********************************************  
*********************************************/   
/* 자재재고 라인 정보 검색  */  
ALTER PROCEDURE [NEOE].[SP_CZ_PU_OPENQT_S]
(  
	@P_CD_COMPANY          NVARCHAR(7),                -- 회사    
	@P_CD_PLANT            NVARCHAR(7),                -- 공장    
	@P_YM_STANDARD         NCHAR(6),                   -- 기준년월    
	@P_CD_SL               NVARCHAR(7),                -- 창고
	@P_CD_DEPT             NVARCHAR(12),               -- 부서    
	@P_NO_EMP              NVARCHAR(10),               -- 담당자
	@P_CLS_ITEM			   NVARCHAR(3),
	@P_CD_ITEM			   NVARCHAR(20)    
)  
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
  
BEGIN  
	EXEC UP_PU_OPENQT_SUB_SELECT @P_CD_COMPANY, @P_CD_PLANT, @P_YM_STANDARD, @P_CD_SL, @P_CD_DEPT, @P_NO_EMP
	
	SELECT 'N' AS S,
		   A.CD_ITEM,
		   A.YM_STANDARD,
		   A.CD_SL,
		   A.CD_PLANT,
		   A.QT_GOOD_INV,        
		   A.QT_REJECT_INV,
		   A.QT_INSP_INV,
		   A.QT_TRANS_INV,        
		   B.NM_ITEM,
		   B.STND_ITEM,
		   B.UNIT_IM,
		   B.CLS_ITEM,  
		   Z.NM_SYSDEF AS NM_CLSITEM  ,
		   (CASE WHEN  B.FG_SERNO  = '002' THEN 'YES' ELSE 'NO' END) AS NO_LOT
	FROM MM_OPENQTL A   
	JOIN MA_PITEM B ON B.CD_COMPANY = A.CD_COMPANY AND B.CD_ITEM = A.CD_ITEM AND B.CD_PLANT = A.CD_PLANT  
	JOIN MA_CODEDTL Z ON Z.CD_COMPANY = B.CD_COMPANY AND Z.CD_SYSDEF = B.CLS_ITEM AND Z.CD_FIELD ='MA_B000010'  
	WHERE A.CD_COMPANY = @P_CD_COMPANY  
	AND A.CD_PLANT = @P_CD_PLANT  
	AND A.YM_STANDARD = @P_YM_STANDARD  
	AND A.CD_SL = @P_CD_SL  
	AND (ISNULL(@P_CLS_ITEM, '') = '' OR B.CLS_ITEM = @P_CLS_ITEM)
	AND (ISNULL(@P_CD_ITEM, '') = '' OR B.CD_ITEM = @P_CD_ITEM)
END

GO

