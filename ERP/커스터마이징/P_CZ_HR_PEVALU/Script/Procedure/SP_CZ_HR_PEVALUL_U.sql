USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALUL_U]    Script Date: 2015-11-17 오전 10:10:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*******************************************
**  System		: 인사관리
**  Sub System	: 인사평가
**  Page		: 평가등록
**  Desc		: UPDATE  
**    
**  Return Values  
**  
**  작    성    자  : 강연철  
**  작    성    일	: 2015.05.29  
**  수    정    자  :   
**  수  정  내  용  :   
*********************************************  
** Change History  
*********************************************  
*********************************************/  
  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALUL_U]  
(  
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_EVALU		NVARCHAR(8),
	@P_CD_EVTYPE	NVARCHAR(3),
	@P_CD_EVOTYPE	NVARCHAR(3),
	@P_CD_EVNUMBER	NVARCHAR(3),
	@P_CD_GROUP		NVARCHAR(3),
	@P_NO_EMPM		NVARCHAR(10),
	@P_NO_EMPAN		NVARCHAR(10),
	@P_LB_EVITEM	NUMERIC(3, 0),
	@P_RT_WEIGHT	NUMERIC(17, 5),
	@P_PT_SCORE		NUMERIC(17, 4),
	@P_CD_GRADE		NVARCHAR(3),
	@P_PT_HSCORE	NUMERIC(17, 4),
	@P_DC_COMMENT1	NVARCHAR(500),
	@P_ID_INSERT	NVARCHAR(15)
)  
AS  

IF NOT EXISTS (SELECT 1 
			   FROM HR_PEVALU_SCORE
			   WHERE CD_COMPANY	= @P_CD_COMPANY
			   AND CD_EVALU	= @P_CD_EVALU
			   AND CD_EVTYPE = @P_CD_EVTYPE
			   AND CD_EVOTYPE = @P_CD_EVOTYPE
			   AND CD_EVNUMBER = @P_CD_EVNUMBER
			   AND CD_GROUP	= @P_CD_GROUP
			   AND NO_EMPM = @P_NO_EMPM
			   AND NO_EMPAN	= @P_NO_EMPAN)
BEGIN
	INSERT INTO HR_PEVALU_SCORE 
	(
		CD_COMPANY,
		CD_EVALU,
		CD_EVTYPE,
		CD_EVOTYPE,
		CD_EVNUMBER,
		CD_GROUP,
		NO_EMPM,
		NO_EMPAN,
		LB_EVITEM,
		RT_WEIGHT,
		PT_SCORE, 
		CD_GRADE,
		PT_HSCORE,
		DC_COMMENT1,
		ID_INSERT,
		DTS_INSERT	
	)	
	VALUES 
	(
		@P_CD_COMPANY,
		@P_CD_EVALU,
		@P_CD_EVTYPE,
		@P_CD_EVOTYPE,
		@P_CD_EVNUMBER,
		@P_CD_GROUP,
		@P_NO_EMPM,
		@P_NO_EMPAN,
		@P_LB_EVITEM,
		@P_RT_WEIGHT,
		@P_PT_SCORE,
		@P_CD_GRADE,
		@P_PT_HSCORE,
		@P_DC_COMMENT1,
		@P_ID_INSERT,
		NEOE.SF_SYSDATE(GETDATE())
	)
END
ELSE
BEGIN
	UPDATE HR_PEVALU_SCORE
	SET PT_SCORE = @P_PT_SCORE,
		CD_GRADE = @P_CD_GRADE,
		PT_HSCORE = @P_PT_HSCORE,
		DC_COMMENT1 = @P_DC_COMMENT1,
		RT_WEIGHT = @P_RT_WEIGHT,
		ID_UPDATE = @P_ID_INSERT,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND	CD_EVALU = @P_CD_EVALU
	AND	CD_EVTYPE = @P_CD_EVTYPE
	AND	CD_EVOTYPE = @P_CD_EVOTYPE
	AND	CD_EVNUMBER	= @P_CD_EVNUMBER
	AND	CD_GROUP = @P_CD_GROUP
	AND	NO_EMPM	= @P_NO_EMPM
	AND	NO_EMPAN = @P_NO_EMPAN
END

GO