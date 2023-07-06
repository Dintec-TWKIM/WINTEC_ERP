USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALUH_U]    Script Date: 2015-11-17 오전 10:09:48 ******/
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
  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALUH_U]  
(  
	@P_CD_COMPANY   NVARCHAR(7),
	@P_CD_EVALU	    NVARCHAR(8),
	@P_CD_EVTYPE	NVARCHAR(3),
	@P_CD_EVNUMBER	NVARCHAR(3),
	@P_NO_EMPM		NVARCHAR(10),
	@P_NO_EMPAN		NVARCHAR(10),
	@P_DC_COMMENT1	NVARCHAR(1500),
	@P_ID_INSERT	NVARCHAR(15)
)  
AS

IF NOT EXISTS (SELECT 1 
			   FROM HR_PEVALU_CMTS 
			   WHERE CD_COMPANY	= @P_CD_COMPANY
			   AND CD_EVALU	= @P_CD_EVALU
			   AND CD_EVTYPE = @P_CD_EVTYPE
			   AND CD_EVNUMBER = @P_CD_EVNUMBER
			   AND NO_EMPM = @P_NO_EMPM
			   AND NO_EMPAN	= @P_NO_EMPAN)
BEGIN
	INSERT INTO HR_PEVALU_CMTS 
	(	
		CD_COMPANY,
		CD_EVALU,
		CD_EVTYPE,
		CD_EVNUMBER,
		NO_EMPM,
		NO_EMPAN,
		DC_COMMENTS,
		ID_INSERT,
		DTS_INSERT
	)	
	VALUES 
	(
		@P_CD_COMPANY,
		@P_CD_EVALU,
		@P_CD_EVTYPE,
		@P_CD_EVNUMBER,
		@P_NO_EMPM,
		@P_NO_EMPAN,
		@P_DC_COMMENT1,
		@P_ID_INSERT,
		NEOE.SF_SYSDATE(GETDATE())
	)
END
ELSE
BEGIN
	UPDATE HR_PEVALU_CMTS
	SET	DC_COMMENTS = @P_DC_COMMENT1,
		ID_UPDATE = @P_ID_INSERT,
		DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND	CD_EVALU = @P_CD_EVALU
	AND	CD_EVTYPE = @P_CD_EVTYPE
	AND	CD_EVNUMBER	= @P_CD_EVNUMBER
	AND	NO_EMPM	= @P_NO_EMPM
	AND	NO_EMPAN = @P_NO_EMPAN
END

GO

