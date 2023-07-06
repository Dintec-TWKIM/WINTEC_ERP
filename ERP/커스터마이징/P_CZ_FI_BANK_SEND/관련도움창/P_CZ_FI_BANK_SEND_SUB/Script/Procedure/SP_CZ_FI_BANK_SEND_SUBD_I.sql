USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_BANK_SEND_SUBD_I]    Script Date: 2018-01-15 오후 5:35:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_FI_BANK_SEND_SUBD_I]
(
	@P_CD_COMPANY  		VARCHAR(20),	--회사
	@P_NODE_CODE  		VARCHAR(20),	--회계단위
	@P_TRANS_DATE  		CHAR(8),		--파일작성일
	@P_TRANS_SEQ  		VARCHAR(16),	--파일순번
	@P_BANK_CODE  		VARCHAR(10),	--출금은행
	@P_ACCT_NO  		VARCHAR(20),	--출금계좌
	@P_SEQ				VARCHAR(16),	--순번
	@P_TRANS_BANK_CODE	VARCHAR(10),	--지금은행
	@P_TRANS_ACCT_NO  	VARCHAR(40),	--지급계좌
	@P_TRANS_NAME  		VARCHAR(100),	--지급계좌명
	@P_CD_EXCH			NVARCHAR(3),	--통화명
	@P_TRANS_AMT_EX  	NUMERIC(19,4),	--이체외화금액
	@P_TRANS_AMT  		NUMERIC(19,4),	--이체원화금액
	@P_CLIENT_NOTE  	VARCHAR(100),	--받는사람적요
	@P_CUST_CODE  		VARCHAR(20),	--거래처(사원)
	@P_CUST_NAME		VARCHAR(50),	--거래처명/사원명
	@P_NO_DOCU			VARCHAR(20),	--전표번호
	@P_ACCT_DATE		CHAR(8),		--회계일자
	@P_DOCU_NO			NUMERIC(6, 0),	--회계번호
	@P_LINE_NO			NUMERIC(6, 0),	--라인번호
	@P_ACCT_CODE		VARCHAR(20),	--계정코드
	@P_AMT_EX			NUMERIC(19, 4),	--전표외화금액
	@P_AMT				NUMERIC(19, 4),	--전표원화금액
	@P_TP_CHARGE		NVARCHAR(4),    --해외은행수수료부담
	@P_TP_SEND_BY		NVARCHAR(4),	--송금방법
	@P_DC_RELATION		NVARCHAR(50),	--신청인과의관계
	@P_ID_INSERT  		VARCHAR(20)
)AS

/*******************************************
**  SYSTEM		: 회계관리
**  SUB SYSTEM	: 은행연동 - 거래내역관리
**  PAGE		: 이체내역관리
**  DESC		: 이체내역관리 도움창 저장(전료건)
**
**  RETURN VALUES
**
**  작    성    자 	: 박창수
**  작    성    일	: 2011.03.22
*********************************************
** CHANGE HISTORY
*********************************************
* 2015.12.24 윤나라 INSERT_TIME추가 
*********************************************/

DECLARE	@P_DTS_INSERT NVARCHAR(8),
		@P_INSERT_TIME  		VARCHAR (10),
		@P_AM_DOCU				NUMERIC(19,4),
		@P_AM_BAN				NUMERIC(19,4)

SET @P_DTS_INSERT = CONVERT(NVARCHAR, GETDATE(), 112)
SET @P_INSERT_TIME = SUBSTRING(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':',''),9,6)

--이체가능금액확인
--SELECT @P_AM_DOCU= AM_DOCU, @P_AM_BAN = AM_BAN
--FROM FI_BANH
--WHERE	NO_DOCU = @P_NO_DOCU
--AND		NO_DOLINE = @P_LINE_NO
--AND		CD_PC = @P_NODE_CODE
--AND		CD_COMPANY = @P_CD_COMPANY

--IF @P_AM_DOCU < @P_AM_BAN + @P_TRANS_AMT
--BEGIN
--	RAISERROR('이체할 수 있는 금액을 초과했습니다.',18,1);
--	RETURN
--END

INSERT INTO BANK_SENDD 
(
	C_CODE,		
	NODE_CODE,		
	NO_DOCU,		
	ACCT_DATE,		
	DOCU_NO,		
	LINE_NO,	
	SLIP_NO,		
	TRANS_DATE,
	TRANS_SEQ,
	ACCT_CODE,
	AMT_EX,		
	AMT,
	CD_EXCH,
    TRANS_AMT_EX,		
	TRANS_AMT,		
	CUST_CODE,	
	CUST_NAME,		
	BANK_CODE,		
	ACCT_NO,		
	SEQ,		
	TRANS_BANK_CODE,		
	TRANS_ACCT_NO,	
	TRANS_NAME,		
	CLIENT_NOTE,
	TP_CHARGE,
	TP_SEND_BY,
	DC_RELATION,		
	INSERT_DATE,
	INSERT_TIME,		
	INSERT_ID
	)
VALUES
(
	@P_CD_COMPANY,		
	@P_NODE_CODE,		
	@P_NO_DOCU,		
	@P_ACCT_DATE,		
	@P_DOCU_NO,		
	@P_LINE_NO,	
	1,		
	@P_TRANS_DATE,		
	@P_TRANS_SEQ,
	@P_ACCT_CODE,
	@P_AMT_EX,		
	@P_AMT,
	@P_CD_EXCH,
	@P_TRANS_AMT_EX,	
	@P_TRANS_AMT,		
	@P_CUST_CODE,	
	@P_CUST_NAME,		
	@P_BANK_CODE,		
	@P_ACCT_NO,		
	@P_SEQ,		
	@P_TRANS_BANK_CODE,		
	@P_TRANS_ACCT_NO,	
	@P_TRANS_NAME,		
	@P_CLIENT_NOTE,
	@P_TP_CHARGE,
	@P_TP_SEND_BY,
	@P_DC_RELATION,
	@P_DTS_INSERT,	
	@P_INSERT_TIME,		
	@P_ID_INSERT
)

GO

