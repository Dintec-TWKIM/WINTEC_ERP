USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SO_TPBUSI_SELECT]    Script Date: 2019-11-11 오후 4:02:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*******************************************    
**  System : 영업    
**  Sub System : 수주관리    
**  Page  : 수주등록    
**  Desc  : 거래구분 조회    
**    
**  Return Values    
**    
**  작    성    자  :     
**  작    성    일 :     
**  수    정    자     :   정남진
*********************************************    
** Change History    
*********************************************    
*********************************************/    
ALTER PROCEDURE [NEOE].[UP_SA_SO_TPBUSI_SELECT]    
(    
 @P_CD_COMPANY   NVARCHAR(7),  --회사    
 @P_TPSO   NVARCHAR(4), -- 수주형태  
 @P_VAT    NVARCHAR(3), -- 부가세코드  
 @P_TP_BUSI   NVARCHAR(3) OUTPUT,  -- 거래구분  
 @P_FLAG    NVARCHAR(3) OUTPUT,  -- 부가세율  
 @P_CONF   NVARCHAR(3) OUTPUT  -- 자동승인여부  
   
)    
AS    
    
SET NOCOUNT ON    
  
BEGIN    
 SELECT @P_TP_BUSI = TP_BUSI, @P_CONF = CONF FROM SA_TPSO WHERE CD_COMPANY = @P_CD_COMPANY AND TP_SO = @P_TPSO  
 SELECT @P_FLAG = CD_FLAG1 FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000040' AND CD_COMPANY = @P_CD_COMPANY AND CD_SYSDEF = @P_VAT  
END  
  
    
SET NOCOUNT OFF    


GO

