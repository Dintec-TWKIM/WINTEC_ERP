USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_ENV_SELECT]    Script Date: 2019-11-11 오후 4:02:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**********************************************************************************************************          
 * 프로시저명: UP_SA_ENV_SELECT  (영업 환경설정)    
 * 관련페이지: P_SA_ENV  1234  
 *   
 * 회사의 전체 환경 설정을 가져온다. : 이유는 한 화면에서 두개 이상의 환경설정을 사용할경우 한번만 가져가기 위해서다.  
 * P_SA_SO : 수주 ( 할인율 적용 )  
 * P_SA_GI : 출하 ( 2중단위허용 여부 )  
 *********************************************************************************************************/           
ALTER PROCEDURE [NEOE].[UP_SA_ENV_SELECT]      
(      
    @P_CD_COMPANY NVARCHAR(7)       
--    ,@FG_TP        NVARCHAR(3)       
)      
AS      
  
SELECT  CD_COMPANY, FG_TP, CD_TP, DC_RMK  
  FROM  SA_ENV      
 WHERE  CD_COMPANY = @P_CD_COMPANY   
--   AND (FG_TP = @FG_TP OR ISNULL(@FG_TP, '') = '')  

GO

