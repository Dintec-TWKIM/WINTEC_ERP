USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_BANK_LIMITEH_INSERT]    Script Date: 2018-03-15 오전 11:34:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_FI_BANK_LIMITEH_INSERT]
(
	@P_CD_COMPANY	NVARCHAR(7),	
	@P_CD_PC		NVARCHAR(7),	
	@P_NO_DOCU		NVARCHAR(20),
	@P_NO_DOLINE	NUMERIC(5,0),
	@P_NO_LIMITE	NVARCHAR(20),
	@P_AM_PAY		NUMERIC(19,4),
	@P_AM_TRPAY		NUMERIC(19,4),
	@P_ID_INSERT	NVARCHAR(15)
)AS
 
/*******************************************  
**  SYSTEM		: 회계관리  
**  SUB SYSTEM	: 은행연동 - 거래내역관리
**  PAGE		: 이체내역관리(분할데이터)
**  DESC		: 이체내역관리 
**  
**  RETURN VALUES  
**  
**  작    성    자  : 박창수  
**  작    성    일	: 2012.03.05
*********************************************  
** CHANGE HISTORY  
*********************************************  
*********************************************/  
DECLARE @P_DTS_INSERT NVARCHAR(8)  
  
SET @P_DTS_INSERT = CONVERT(NVARCHAR, GETDATE(), 112)  

INSERT INTO BANK_LIMITEH
(
	CD_COMPANY,	
	CD_PC,		
	NO_DOCU,		
	NO_DOLINE,	
	NO_LIMITE,	
	AM_PAY,		
	AM_TRPAY,	
	ID_INSERT,	
	DTS_INSERT	
)
VALUES
(
	@P_CD_COMPANY,	
	@P_CD_PC,		
	@P_NO_DOCU,		
	@P_NO_DOLINE,	
	@P_NO_LIMITE,	
	@P_AM_PAY,		
	@P_AM_TRPAY,		
	@P_ID_INSERT,	
	@P_DTS_INSERT	
)
GO

