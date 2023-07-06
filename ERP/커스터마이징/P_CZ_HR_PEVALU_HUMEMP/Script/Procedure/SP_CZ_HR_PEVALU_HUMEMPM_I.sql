USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_PEVALU_HUMEMPM_I]    Script Date: 2015-11-17 오후 8:04:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System		: 인사관리
**  Sub System	: 인사평가
**  Page		: 평가자/피평가자등록
**  Desc		: 평가자 저장  
**    
**  Return Values  
**  
**  작    성    자  : 강연철  
**  작    성    일	: 2015.05.26  
**  수    정    자  :   
**  수  정  내  용  :   
*********************************************  
** Change History  
*********************************************  
*********************************************/  
  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_PEVALU_HUMEMPM_I]  
(  
	@P_CD_COMPANY		NVARCHAR(7),  
	@P_CD_EVALU			NVARCHAR(8),  
	@P_CD_EVTYPE		NVARCHAR(3),  
	@P_CD_EVNUMBER		NVARCHAR(3),
	@P_NO_EMPM			NVARCHAR(10),
	@P_CD_BIZAREA		NVARCHAR(7),
	@P_CD_DEPT			NVARCHAR(12),
	@P_CD_EMP			NVARCHAR(3),
	@P_TP_EMP			NVARCHAR(3),
	@P_CD_DUTY_RANK		NVARCHAR(3),
	@P_CD_DUTY_STEP		NVARCHAR(3),
	@P_CD_DUTY_RESP		NVARCHAR(3),
	@P_CD_DUTY_TYPE		NVARCHAR(3),
	@P_CD_PAY_STEP		NVARCHAR(3),
	@P_CD_DUTY_WORK		NVARCHAR(3),
	@P_CD_JOB_SERIES	NVARCHAR(3),
	@P_CD_CC			NVARCHAR(3),
	@P_CD_PJT			NVARCHAR(3),
	@P_NUM_EWEIGHT		NUMERIC(17, 5),
	@P_ID_INSERT		NVARCHAR(15),
	@P_CD_GROUP			NVARCHAR(3)
)  
AS

IF NOT EXISTS (SELECT 1 
			   FROM HR_PEVALU_HUMEMPM
			   WHERE CD_COMPANY	= @P_CD_COMPANY
			   AND CD_EVALU	= @P_CD_EVALU
			   AND CD_EVTYPE = @P_CD_EVTYPE
			   AND CD_EVNUMBER = @P_CD_EVNUMBER
			   AND CD_GROUP	= @P_CD_GROUP
			   AND NO_EMPM = @P_NO_EMPM)
BEGIN
	INSERT INTO HR_PEVALU_HUMEMPM   
	(
		CD_COMPANY,
		CD_EVALU,
		CD_EVTYPE,
		CD_EVNUMBER,
		CD_GROUP,
		NO_EMPM,
		CD_BIZAREA,
		CD_DEPT,
		CD_EMP,
		TP_EMP,
		CD_DUTY_RANK,
		CD_DUTY_STEP,
		CD_DUTY_RESP,
		CD_DUTY_TYPE,
		CD_PAY_STEP,
		CD_DUTY_WORK,
		CD_JOB_SERIES,
		CD_CC,
		CD_PJT,
		NUM_EWEIGHT,
		ID_INSERT,
		DTS_INSERT 
	) 	
	VALUES   
	(
		@P_CD_COMPANY,
		@P_CD_EVALU,
		@P_CD_EVTYPE,
		@P_CD_EVNUMBER,
		@P_CD_GROUP,
		@P_NO_EMPM,
		@P_CD_BIZAREA,
		@P_CD_DEPT,
		@P_CD_EMP,
		@P_TP_EMP,
		@P_CD_DUTY_RANK,
		@P_CD_DUTY_STEP,
		@P_CD_DUTY_RESP,
		@P_CD_DUTY_TYPE,
		@P_CD_PAY_STEP,
		@P_CD_DUTY_WORK,
		@P_CD_JOB_SERIES,
		@P_CD_CC,
		@P_CD_PJT,
		@P_NUM_EWEIGHT, 
		@P_ID_INSERT,  
		NEOE.SF_SYSDATE(GETDATE())
	)

	IF @P_CD_EVNUMBER = '000'
	BEGIN
		INSERT INTO HR_PEVALU_HUMEMPAN   
		(
			CD_COMPANY,
			CD_EVALU,
			CD_EVTYPE,
			CD_EVNUMBER,
			CD_GROUP,
			NO_EMPM,
			NO_EMPAN,
			CD_BIZAREA,
			CD_DEPT,
			CD_EMP,
			TP_EMP,
			CD_DUTY_RANK,
			CD_DUTY_STEP,
			CD_DUTY_RESP,
			CD_DUTY_TYPE,
			CD_PAY_STEP,
			CD_DUTY_WORK,
			CD_JOB_SERIES,
			CD_CC,
			CD_PJT,
			ID_INSERT,
			DTS_INSERT
		)  
		VALUES   
		(
			@P_CD_COMPANY,
			@P_CD_EVALU,
			@P_CD_EVTYPE,
			@P_CD_EVNUMBER,
			@P_CD_GROUP,
			@P_NO_EMPM,
			@P_NO_EMPM,
			@P_CD_BIZAREA,
			@P_CD_DEPT,
			@P_CD_EMP,
			@P_TP_EMP,
			@P_CD_DUTY_RANK,
			@P_CD_DUTY_STEP,
			@P_CD_DUTY_RESP,
			@P_CD_DUTY_TYPE,
			@P_CD_PAY_STEP,
			@P_CD_DUTY_WORK,
			@P_CD_JOB_SERIES,
			@P_CD_CC,
			@P_CD_PJT, 
			@P_ID_INSERT,  
			NEOE.SF_SYSDATE(GETDATE()) 
		) 
	END
END

GO

