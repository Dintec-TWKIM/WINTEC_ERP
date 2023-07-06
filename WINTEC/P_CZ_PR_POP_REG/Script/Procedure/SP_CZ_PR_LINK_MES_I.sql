USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_LINK_MES_I]    Script Date: 2021-02-18 오후 1:12:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_LINK_MES_I]
(
	@P_CD_COMPANY		NVARCHAR(7), -- 회사코드
	@P_CD_PLANT			NVARCHAR(7), -- 공장코드
	@P_CD_ITEM			NVARCHAR(50), -- 품목코드
	@P_NO_EMP			NVARCHAR(10), -- 담당자코드
	@P_CD_WC			NVARCHAR(7), -- 작업장코드
	@P_CD_OP			NVARCHAR(4), -- 공정순번
	@P_CD_WCOP			NVARCHAR(4), -- 공정코드
	@P_YN_WO			NVARCHAR(1), -- 작업지시생성여부
	@P_NO_WO			NVARCHAR(20), -- 작업지시번호
	@P_YN_WORK			NVARCHAR(1), -- 작업실적생성여부
	@P_QT_WORK			NUMERIC(17, 4), -- 실적수량
	@P_QT_REJECT		NUMERIC(17, 4), -- 불량수량
	@P_QT_BAD			NUMERIC(17, 4), -- 불량처리수량
	@P_CD_SL_IN			NVARCHAR(7), -- 입고창고
	@P_CD_SL_BAD_IN		NVARCHAR(7), -- 불량입고창고
	@P_YN_REWORK		NVARCHAR(1), -- 재작업여부

	@P_YN_MANDAY		NVARCHAR(1), -- 작업공수사용여부
	@P_TM_WO_T			NUMERIC(17, 4), -- 작업시간
	@P_QT_WO_ROLL		NUMERIC(17, 4), -- 작업인원
	@P_TM_EQ_T			NUMERIC(17, 4), -- 설비작업시간
	
	@P_NO_LOT			NVARCHAR(100), -- LOT 번호
	@P_NO_SFT			NVARCHAR(3), -- SFT

	@P_CD_EQUIP			NVARCHAR(30), -- 설비코드
	@P_CD_REJECT		NVARCHAR(4), -- 불량종류코드
	@P_CD_RESOURCE		NVARCHAR(4), -- 불량원인코드
	@P_CD_USERDEF1		NVARCHAR(20) = NULL, -- POP 입력여부

	@P_ID_INSERT		NVARCHAR(15),

	@P_NO_MES			NVARCHAR(20) OUTPUT
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_DOCU_YM		NVARCHAR(6),
		@V_NO_MES		NVARCHAR(20)

SET	@V_DOCU_YM = LEFT(NEOE.SF_SYSDATE(GETDATE()), 6) 

EXEC CP_GETNO @P_CD_COMPANY = @P_CD_COMPANY,
			  @P_CD_MODULE  = 'CZ',
			  @P_CD_CLASS   = '80',
			  @P_DOCU_YM    = @V_DOCU_YM,
			  @P_NO         = @V_NO_MES OUTPUT

INSERT INTO PR_LINK_MES
(
	CD_COMPANY,
	CD_PLANT,
	NO_MES,
	CD_ITEM,
	DT_WORK,
	NO_EMP,
	CD_WC,
	CD_OP,
	CD_WCOP,
	YN_WO,
	NO_WO,
	YN_WORK,
	QT_WORK,
	QT_REJECT,
	QT_BAD,
	CD_SL_IN,
	CD_SL_BAD_IN,
	YN_REWORK,
	YN_MANDAY,
	TM_WO_T,
	QT_WO_ROLL,
	TM_EQ_T,
	NO_LOT,
	NO_SFT,
	CD_EQUIP,
	CD_REJECT,
	CD_RESOURCE,
	CD_USERDEF1,
	ID_INSERT,
	DTS_INSERT
) 
VALUES 
(
	@P_CD_COMPANY,
	@P_CD_PLANT,
	@V_NO_MES,
	@P_CD_ITEM,
	CONVERT(CHAR(8), GETDATE(), 112),
	@P_NO_EMP,
	@P_CD_WC,
	@P_CD_OP,
	@P_CD_WCOP,
	@P_YN_WO,
	@P_NO_WO,
	@P_YN_WORK,
	@P_QT_WORK,
	@P_QT_REJECT,
	@P_QT_BAD,
	@P_CD_SL_IN,
	@P_CD_SL_BAD_IN,
	@P_YN_REWORK,
	@P_YN_MANDAY,
	@P_TM_WO_T,
	@P_QT_WO_ROLL,
	@P_TM_EQ_T,
	@P_NO_LOT,
	@P_NO_SFT,
	@P_CD_EQUIP,
	@P_CD_REJECT,
	@P_CD_RESOURCE,
	@P_CD_USERDEF1,
	@P_ID_INSERT,
	NEOE.SF_SYSDATE(GETDATE())
)

SET @P_NO_MES = @V_NO_MES