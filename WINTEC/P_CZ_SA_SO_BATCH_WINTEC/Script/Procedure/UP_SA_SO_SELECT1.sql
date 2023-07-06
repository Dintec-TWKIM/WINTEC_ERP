USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SO_SELECT1]    Script Date: 2019-11-11 오후 4:02:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************  
**  System : 영업  
**  Sub System : 수주관리  
**  Page  : 수주등록  
**  Desc  : 단가유형별 조회  
**  
**  Return Values  
**  
**  작    성    자  :   
**  작    성    일 :   
**  수    정    자     : 허성철  
*********************************************  
** Change History : 아~ 이것도 나중에 구매쪽이랑 합쳐버려야지..  
*********************************************  
*********************************************/  
ALTER PROCEDURE [NEOE].[UP_SA_SO_SELECT1]  
(  
	@P_CD_COMPANY		NVARCHAR(7),                --회사  
	@P_CD_ITEM          NVARCHAR(50),                --품목  
	@P_CD_PARTNER       NVARCHAR(20),                --거래처  
	@P_FG_UM            NVARCHAR(3),                --단가유형  
	@P_CD_EXCH          NVARCHAR(3),                --환종  
	@P_TP_SALEPRICE     NVARCHAR(3),                --단가적용형태  
	@P_DT_SO            NVARCHAR(8),                --수주일자  
	@P_UM               NUMERIC(19, 6) OUTPUT  
)  
AS  
  
IF (@P_TP_SALEPRICE = '002')                        --유형별단가이면  
BEGIN  
SELECT @P_UM = A.UM_ITEM  
  FROM (   
          SELECT TOP 1 MAX(SDT_UM) AS SDT_UM, UM_ITEM--*  
            FROM MA_ITEM_UM  
           WHERE CD_COMPANY = @P_CD_COMPANY  
             AND CD_ITEM = @P_CD_ITEM  
             AND FG_UM = @P_FG_UM  
             AND CD_EXCH = @P_CD_EXCH  
             AND TP_UMMODULE = '002'                --영업  
             AND SDT_UM <= @P_DT_SO  
             AND EDT_UM >= @P_DT_SO  
           GROUP BY UM_ITEM  
           ORDER BY SDT_UM DESC, UM_ITEM DESC   
    ) A  
END  
  
----NJIN 수정  
IF (@P_TP_SALEPRICE = '003')                        --거래처별단가이면  
BEGIN  
SELECT @P_UM = A.UM_ITEM  
  FROM (   
          SELECT TOP 1 MAX(SDT_UM) AS SDT_UM, UM_ITEM--*  
            FROM MA_ITEM_UMPARTNER  
           WHERE CD_COMPANY = @P_CD_COMPANY  
             AND CD_ITEM = @P_CD_ITEM  
             AND CD_PARTNER = @P_CD_PARTNER  
             AND FG_UM = @P_FG_UM  
             AND CD_EXCH = @P_CD_EXCH  
             AND TP_UMMODULE = '002'                --영업  
             AND SDT_UM <= @P_DT_SO  
             AND EDT_UM >= @P_DT_SO  
           GROUP BY UM_ITEM  
           ORDER BY SDT_UM DESC, UM_ITEM DESC   
    ) A  
END  

--TEST CODE
--SELECT @P_UM = 3456.6789
  
/* 기존 되어 있던것   
 * 수주일이 거래처별단가 기간과 묶여 있지 않았음  
 */  

--SELECT                @P_UM = UM_ITEM  
--FROM                MA_ITEM_UM  
--WHERE                CD_COMPANY = @P_CD_COMPANY  
--AND                        CD_ITEM = @P_CD_ITEM  
--AND                        FG_UM = @P_FG_UM  
--AND                        CD_EXCH = @P_CD_EXCH  
--AND                        TP_UMMODULE = '002'                --영업  
--AND SDT_UM <= @P_DT_SO  
--             AND EDT_UM >= @P_DT_SO  
--GROUP        BY        UM_ITEM  
--

--        SELECT                @P_UM = UM_ITEM  
--        FROM                MA_ITEM_UMPARTNER  
--        WHERE                CD_COMPANY = @P_CD_COMPANY  
--        AND                        CD_ITEM = @P_CD_ITEM  
--        AND                        CD_PARTNER = @P_CD_PARTNER  
--        AND                        FG_UM = @P_FG_UM  
--        AND                        CD_EXCH = @P_CD_EXCH  
--        AND                        TP_UMMODULE = '002'                --영업  
--        GROUP        BY        UM_ITEM
GO

