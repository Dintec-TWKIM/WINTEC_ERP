USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_SA_GIM_REG_UPDATE]    Script Date: 2016-04-12 오전 11:07:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*******************************************    
**  System : 영업    
**  Sub System : 출고관리     
**  Page  : 출고등록    
**  Desc  : 출고등록 헤더 비고 업데이트 
**  Return Values    
**    
**  작    성    자  :     
**  작    성    일 :     
**  수    정    자     : 허성철    
*********************************************    
** Change History    
*********************************************    
*********************************************/   
ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIM_REG_U]    
(    
    @P_CD_COMPANY       NVARCHAR(7),   --회사  
    @P_NO_IO            NVARCHAR(20),  --수불번호
	@P_NO_GIR           NVARCHAR(20),  --협조전번호
	@P_DT_LOADING       NVARCHAR(8),   --선적일자
    @P_DC_RMK           NVARCHAR(100), --비고
    @P_FILE_PATH_MNG    NVARCHAR(300), --첨부파일
	@P_ID_UPDATE		NVARCHAR(15)
)    
AS    
  
UPDATE MM_QTIOH
SET DC_RMK = @P_DC_RMK, 
    FILE_PATH_MNG = @P_FILE_PATH_MNG,
	ID_UPDATE = @P_ID_UPDATE,
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = @P_CD_COMPANY  
AND NO_IO = @P_NO_IO  

IF EXISTS (SELECT 1 FROM CZ_SA_GIRH_WORK_DETAIL WHERE CD_COMPANY = @P_CD_COMPANY AND NO_GIR = @P_NO_GIR)
BEGIN
	UPDATE CZ_SA_GIRH_WORK_DETAIL
	SET DT_LOADING = @P_DT_LOADING,
		ID_UPDATE = @P_ID_UPDATE,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND NO_GIR = @P_NO_GIR
END
ELSE
BEGIN
	INSERT INTO CZ_SA_GIRH_WORK_DETAIL
	(CD_COMPANY, NO_GIR, DT_LOADING, ID_INSERT, DTS_INSERT)
	VALUES
	(@P_CD_COMPANY, @P_NO_GIR, @P_DT_LOADING, @P_ID_UPDATE, NEOE.SF_SYSDATE(GETDATE()))
END


GO