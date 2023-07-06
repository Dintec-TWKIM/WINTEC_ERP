USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIH_I]    Script Date: 2015-10-21 오전 8:54:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIH_I]      
(        
 @P_CD_COMPANY	       NVARCHAR(7),			--회사      
 @P_NO_IO              NVARCHAR(20),		--수불번호
 @P_NO_GIR             NVARCHAR(20),		--의뢰번호
 @P_CD_PLANT           NVARCHAR(7),			--공장코드    
 @P_CD_PARTNER         NVARCHAR(20),        --거래처    
 @P_FG_TRANS           NVARCHAR(3),			--거래구분    
 @P_DT_IO              NVARCHAR(8),			--수불일자      
 @P_CD_DEPT            NVARCHAR(12),		--부서      
 @P_NO_EMP             NVARCHAR(10),		--담당자      
 @P_DC_RMK             NVARCHAR(100),       --비고      
 @P_YN_RETURN	       NCHAR(1),			--반품여부    
 @P_ID_INSERT          NVARCHAR(15)			--등록자      
)       
AS      
  
SET NOCOUNT ON    
  
DECLARE @V_DTS_INSERT NVARCHAR(14)    
SET  @V_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')      
	
INSERT INTO MM_QTIOH    
(    
 CD_COMPANY,  NO_IO,   CD_PLANT,  CD_PARTNER,  FG_TRANS,    
 DT_IO,   CD_DEPT,  NO_EMP,   DC_RMK,   YN_RETURN,    
 ID_INSERT,  DTS_INSERT    
)    
VALUES    
(    
 @P_CD_COMPANY, @P_NO_IO,  @P_CD_PLANT, @P_CD_PARTNER,  @P_FG_TRANS,    
 @P_DT_IO,  @P_CD_DEPT,  @P_NO_EMP,  @P_DC_RMK,  @P_YN_RETURN,                                    
 @P_ID_INSERT, @V_DTS_INSERT    
)

UPDATE SA_GIRH
SET STA_GIR = 'C',
	ID_UPDATE = @P_ID_INSERT,
	DTS_UPDATE = @V_DTS_INSERT
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_GIR = @P_NO_GIR

GO

