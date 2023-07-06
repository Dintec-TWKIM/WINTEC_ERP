USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALU_OBJECT_D]    Script Date: 2015-11-17 오전 9:54:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System		: 인사관리
**  Sub System	: 인사평가
**  Page		: 목표등록
**  Desc		: 조회
**    
**  Return Values  
**  
**  작    성    자  : 강연철  
**  작    성    일	: 2015.06.01  
**  수    정    자  :   
**  수  정  내  용  :   
*********************************************  
** Change History  
*********************************************  
*********************************************/ 
  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALU_OBJECT_D]  
(  
	@P_CD_COMPANY	NVARCHAR(7),  
	@P_CD_EVALU		NVARCHAR(8),  
	@P_CD_EVTYPE	NVARCHAR(3),  
	@P_CD_EVOTYPE	NVARCHAR(3),  
	@P_NO_EMP		NVARCHAR(10)  
)  
AS  
BEGIN  
	DELETE FROM HR_PEVALU_OBJECT   
	WHERE	CD_COMPANY	= @P_CD_COMPANY  
	AND		CD_EVALU	= @P_CD_EVALU  
	AND		CD_EVTYPE	= @P_CD_EVTYPE  
	AND		NO_EMP		= @P_NO_EMP  
	AND		CD_EVOTYPE	= @P_CD_EVOTYPE  
END
GO

