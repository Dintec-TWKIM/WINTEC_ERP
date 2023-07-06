USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_BANK_SEND_SUBH_I]    Script Date: 2018-01-15 오후 5:35:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_FI_BANK_SEND_SUBH_I]
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
	@P_TRANS_AMT  		NUMERIC(19,4),	--이체금액							
	@P_CLIENT_NOTE  	VARCHAR(100),	--받는사람적요					
	@P_TRANS_NOTE		VARCHAR(100),	--보내는사람적요						
	@P_CUST_CODE  		VARCHAR(20),	--거래처(사원)
	@P_CUST_NAME		VARCHAR(50),	--거래처명/사원명						
	@P_ID_INSERT  		VARCHAR(20),
	@P_NO_COMPANY		VARCHAR(20),
	@P_NO_DOCU			VARCHAR(20),
	@P_LINE_NO			NUMERIC(5, 0),	--라인번호
	@P_DC_RMK			NVARCHAR(200),
	@P_TP_CHARGE		NVARCHAR(4),    --해외은행수수료부담
	@P_TP_SEND_BY		NVARCHAR(4),	--송금방법
	@P_DC_RELATION		NVARCHAR(50)	--신청인과의관계
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

DECLARE	@P_DTS_INSERT	NVARCHAR(8),
		@P_INSERT_TIME  VARCHAR (10)

SET @P_DTS_INSERT = CONVERT(NVARCHAR, GETDATE(), 112)
SET @P_INSERT_TIME = SUBSTRING(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':',''),9,6)

INSERT INTO BANK_SENDH 
(
	C_CODE,		
	NODE_CODE,		
	TRANS_DATE,		
	TRANS_SEQ,		
	BANK_CODE,		
	ACCT_NO,	
	SEQ,		
	TRANS_BANK_CODE,
	TRANS_ACCT_NO,		
	TRANS_NAME,
	CD_EXCH,
	TRANS_AMT_EX,		
	TRANS_AMT,		
	CLIENT_NOTE,	
	TRANS_NOTE,
	CUST_CODE,
	CUST_NAME,		
	INSERT_DATE,
	INSERT_TIME,		
	INSERT_ID,
	NO_COMPANY,
	TRANS_GU,
	NO_DOCU,
	NO_DOLINE,
	TRANS_FLAG,
	DC_RMK,
	TP_CHARGE,
	TP_SEND_BY,
	DC_RELATION
)
VALUES
(
	@P_CD_COMPANY,		
	@P_NODE_CODE,		
	@P_TRANS_DATE,		
	@P_TRANS_SEQ,		
	@P_BANK_CODE,		
	@P_ACCT_NO,	
	@P_SEQ,		
	@P_TRANS_BANK_CODE,
	@P_TRANS_ACCT_NO,		
	@P_TRANS_NAME,
	@P_CD_EXCH,
    @P_TRANS_AMT_EX,
	@P_TRANS_AMT,		
	@P_CLIENT_NOTE,
	@P_TRANS_NOTE,
	@P_CUST_CODE,
	@P_CUST_NAME,		
	@P_DTS_INSERT,	
	@P_INSERT_TIME,		
	@P_ID_INSERT,
	@P_NO_COMPANY,
	'0',
	@P_NO_DOCU,
	@P_LINE_NO,
	'EE',
	@P_DC_RMK,
	@P_TP_CHARGE,
	@P_TP_SEND_BY,
	@P_DC_RELATION
)

GO

