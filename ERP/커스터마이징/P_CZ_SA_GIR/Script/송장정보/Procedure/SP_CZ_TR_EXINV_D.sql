USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_TR_EXINV_D]    Script Date: 2015-04-27 오후 8:16:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************    
**  System : 무역  
**  Sub System : 수출  
**  Page  : 송장작성  
**  Desc  : 송장작성 삭제   
**  수정자 : 허성철    
************************************************************************/
   
ALTER PROCEDURE [NEOE].[SP_CZ_TR_EXINV_D]
@P_CD_COMPANY        NVARCHAR(7),     
@P_NO_INV                NVARCHAR(20)     
AS
BEGIN
  
IF EXISTS ( SELECT * FROM TR_COSTEXH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_INVOICE = @P_NO_INV AND FG_PRODUCT = '002')  
BEGIN  
	RAISERROR ('판매 경비가 등록되어 있어 삭제할 수 없습니다', 18, 1)
	RETURN
END  
  
DELETE  
FROM CZ_TR_INVL  
WHERE CD_COMPANY = @P_CD_COMPANY  
AND  NO_INV  = @P_NO_INV
  
DELETE   
FROM TR_PACKING    
WHERE CD_COMPANY = @P_CD_COMPANY  
AND  NO_INV = @P_NO_INV      
  
DELETE   
FROM CZ_TR_INVH    
WHERE CD_COMPANY = @P_CD_COMPANY    
AND  NO_INV = @P_NO_INV   
END
GO

