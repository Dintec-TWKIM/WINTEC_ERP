USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_HR_PEVALU_OBJECT_I]    Script Date: 2015-10-23 오후 3:57:27 ******/
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
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALU_OBJECT_I]  
(  
	@P_CD_COMPANY NVARCHAR(7),  
	@P_CD_EVALU  NVARCHAR(8),  
	@P_CD_EVTYPE NVARCHAR(3),  
	@P_CD_EVOTYPE NVARCHAR(3),  
	@P_NO_EMP  NVARCHAR(10),  
	@P_CD_OTASK  NVARCHAR(3),  
	@P_NM_OTASK  NVARCHAR(500), 
	@P_DC_DOBJECT NVARCHAR(300),  
	@P_CD_SCALE  NVARCHAR(3),  
	@P_RT_WEIGHT NUMERIC(17, 5),  
	@P_YN_APP  NVARCHAR(1),  
	@P_DT_APP  NVARCHAR(8),  
	@P_ID_INSERT NVARCHAR(15)  
)  
AS  
DECLARE @P_DTS_INSERT VARCHAR(15)  
SET		@P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')  
   
BEGIN  
	INSERT INTO HR_PEVALU_OBJECT   
	(   CD_COMPANY, CD_EVALU, CD_EVTYPE, CD_EVOTYPE, NO_EMP,   
		CD_OTASK, NM_OTASK, DC_DOBJECT, CD_SCALE, RT_WEIGHT, YN_APP,   
		DT_APP, ID_INSERT, DTS_INSERT )  
	VALUES ( @P_CD_COMPANY, @P_CD_EVALU, @P_CD_EVTYPE, @P_CD_EVOTYPE, @P_NO_EMP,  
		@P_CD_OTASK, @P_NM_OTASK, @P_DC_DOBJECT, @P_CD_SCALE, (@P_RT_WEIGHT * 0.01), @P_YN_APP,  
		@P_DT_APP, @P_ID_INSERT, @P_DTS_INSERT )  
END
GO

