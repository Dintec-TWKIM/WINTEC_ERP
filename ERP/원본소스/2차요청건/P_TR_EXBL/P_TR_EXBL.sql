IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_SELECT'))
DROP PROCEDURE UP_TR_EXBL_SELECT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXBL_SELECT
 * ����������: ���� >> ������� >> ��ȸ
 * ��  ��  ��: 
 * EXEC UP_TR_EXBL_SELECT '',''
 *********************************************************************************************************/ 
  
CREATE PROCEDURE UP_TR_EXBL_SELECT
	@P_CD_COMPANY   NVARCHAR(7),  
	@P_NO_BL		NVARCHAR(20) -- ������ȣ  
AS  
BEGIN
	SELECT  NO_BL,		-- B/L��ȣ  
		NO_TO,			-- �����ȣ  
		NO_INV,			-- �����ȣ  
		CD_BIZAREA,		-- �����  
		CD_SALEGRP,		-- �����׷�  
		NO_EMP,			-- �����  
		CD_PARTNER,		-- �ŷ�ó  
		DT_BALLOT,		-- ��ǥ����  
		CD_EXCH,		-- ȯ��  
		RT_EXCH,		-- ȯ��  
		AM_EX,			-- ��ȭ�ݾ�  
		FLOOR(AM) AS AM,-- ��ȭ�ݾ�  
		AM_EXNEGO,		-- NEGO��ȭ�ݾ�  
		AM_NEGO,		-- NEGO��ȭ�ݾ�  
		YN_SLIP,		-- ��ǥó������  
		NO_SLIP,		-- ��ǥ��ȣ  
		CD_EXPORT,		-- ������  
		DT_LOADING,		-- ����������  
		DT_ARRIVAL,		-- ����������  
		SHIP_CORP,		-- ����  
		NM_VESSEL,		-- VESSEL��  
		PORT_LOADING,	-- ������  
		PORT_NATION,	-- ������  
		PORT_ARRIVER,	-- ������  
		COND_SHIPMENT,	-- ��������  
		FG_BL,			-- ��������  
		FG_LC,			-- LC����  
		COND_PAY,		-- ��������  
		COND_DAYS,		-- �����ϼ�  
		DT_PAYABLE,		-- ����������  
		REMARK1,		-- ���1  
		REMARK2,		-- ���2  
		REMARK3,		-- ���3  
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) NM_PARTNER,    
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.SHIP_CORP) NM_SHIP_CORP,    
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_EXPORT) NM_EXPORT,    
		(SELECT NM_KOR FROM MA_EMP WHERE CD_COMPANY = A.CD_COMPANY AND NO_EMP = A.NO_EMP) NM_KOR,    
		(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = A.CD_SALEGRP) NM_SALEGRP,    
		(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = A.CD_COMPANY AND CD_BIZAREA = A.CD_BIZAREA) NM_BIZAREA  
	FROM TR_EXBL A  
	WHERE NO_BL = @P_NO_BL    
	AND CD_COMPANY = @P_CD_COMPANY 
 END
 GO  
  

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_DELETE'))
DROP PROCEDURE UP_TR_EXBL_DELETE
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXBL_DELETE
 * ����������: ���� >> ������� >> ����
 * ��  ��  ��: 
 * EXEC UP_TR_EXBL_DELETE '',''
 *********************************************************************************************************/ 
  
CREATE PROCEDURE UP_TR_EXBL_DELETE    
	@P_CD_COMPANY   NVARCHAR(7),  
	@P_NO_BL		NVARCHAR(40)  
AS    
BEGIN
	IF EXISTS (SELECT * FROM TR_COSTEXH WHERE CD_COMPANY = @P_CD_COMPANY AND FG_PRODUCT = '004' AND NO_BL = @P_NO_BL)  
	BEGIN  
		SET NOCOUNT OFF  
		RAISERROR -5000 '�Ǹ� ��� ��ϵǾ� �־� ������ �� �����ϴ�.'  
		RETURN  
	END  
  
	DELETE FROM TR_EXBLL
	WHERE NO_BL = @P_NO_BL
	AND CD_COMPANY = @P_CD_COMPANY
  
	DELETE FROM TR_EXBL    
	WHERE NO_BL = @P_NO_BL
	AND	CD_COMPANY = @P_CD_COMPANY
END
GO

  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_INSERT'))
DROP PROCEDURE UP_TR_EXBL_INSERT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXBL_INSERT
 * ����������: ���� >> ������� >> ����
 * ��  ��  ��: 
 * EXEC UP_TR_EXBL_INSERT '',''
 *********************************************************************************************************/ 
 
CREATE PROCEDURE UP_TR_EXBL_INSERT    
	@P_NO_BL			NVARCHAR(20),    
	@P_CD_COMPANY		NVARCHAR(7),    
	@P_NO_TO			NVARCHAR(20),    
	@P_NO_INV			NVARCHAR(20),    
	@P_CD_BIZAREA		NVARCHAR(7),    
	
	@P_CD_SALEGRP		NVARCHAR(7),    
	@P_NO_EMP			NVARCHAR(10),    
	@P_CD_PARTNER		NVARCHAR(7),    
	@P_DT_BALLOT		NCHAR(8),    
	@P_CD_EXCH			NVARCHAR(3),    
	
	@P_RT_EXCH			NUMERIC(15,4),    
	@P_AM_EX			NUMERIC(17,4),    
	@P_AM				NUMERIC(17,4),    
	@P_AM_EXNEGO		NUMERIC(17,4),    
	@P_AM_NEGO			NUMERIC(17,4),    
	
	@P_YN_SLIP			NCHAR(1),    
	@P_NO_SLIP			NVARCHAR(12),    
	@P_CD_EXPORT		NVARCHAR(7),    
	@P_DT_LOADING		NCHAR(8),    
	@P_DT_ARRIVAL		NCHAR(8),    
	
	@P_SHIP_CORP		NVARCHAR(7),    
	@P_NM_VESSEL		NVARCHAR(50),    
	@P_PORT_LOADING		NVARCHAR(50),    
	@P_PORT_NATION		NVARCHAR(3),    
	@P_PORT_ARRIVER		NVARCHAR(50),    
	
	@P_COND_SHIPMENT	NVARCHAR(3),    
	@P_FG_BL			NVARCHAR(3),    
	@P_FG_LC			NVARCHAR(3),    
	@P_COND_PAY			NVARCHAR(3),    
	@P_COND_DAYS		NUMERIC(4,0),    
	
	@P_DT_PAYABLE		NCHAR(8),    
	@P_REMARK1			NVARCHAR(100),    
	@P_REMARK2			NVARCHAR(100),    
	@P_REMARK3			NVARCHAR(100),    
	@P_ID_INSERT		NVARCHAR(15)    
AS    
BEGIN  
	DECLARE @P_DTS_INSERT VARCHAR(14),    
			@P_CNT  NUMERIC(4,0)    
    
	SET @P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')    
	SET @P_CNT = 0    
    
	BEGIN    
		SELECT @P_CNT =  ISNULL(COUNT(*),0)    
		FROM TR_EXBL    
		WHERE CD_COMPANY = @P_CD_COMPANY AND NO_BL = @P_NO_BL    
	END    
    
	-- �ߺ��� �ڷᰡ ������ INSERT    
	IF @P_CNT = 0     
	BEGIN    
		INSERT INTO TR_EXBL (NO_BL,			CD_COMPANY,		NO_TO,		NO_INV,		CD_BIZAREA,		CD_SALEGRP,     
							 NO_EMP,		CD_PARTNER,		DT_BALLOT,  CD_EXCH,	RT_EXCH,		AM_EX,     
							 AM,			AM_EXNEGO,		AM_NEGO,	YN_SLIP,	NO_SLIP,		CD_EXPORT,     
							 DT_LOADING,	DT_ARRIVAL,		SHIP_CORP,  NM_VESSEL,  PORT_LOADING,	PORT_NATION,     
							 PORT_ARRIVER,  COND_SHIPMENT,	FG_BL,		FG_LC,		COND_PAY,		COND_DAYS,     
							 DT_PAYABLE,	REMARK1,		REMARK2,	REMARK3,	ID_INSERT,		DTS_INSERT)
							    
		VALUES(@P_NO_BL,		@P_CD_COMPANY,		@P_NO_TO,		@P_NO_INV,		@P_CD_BIZAREA,		@P_CD_SALEGRP,     
			   @P_NO_EMP,		@P_CD_PARTNER,		@P_DT_BALLOT,	@P_CD_EXCH,		@P_RT_EXCH,			@P_AM_EX,     
			   FLOOR(@P_AM),	@P_AM_EXNEGO,		@P_AM_NEGO,		'N',			@P_NO_SLIP,			@P_CD_EXPORT,     
			   @P_DT_LOADING,	@P_DT_ARRIVAL,		@P_SHIP_CORP,	@P_NM_VESSEL,	@P_PORT_LOADING,	@P_PORT_NATION,     
			   @P_PORT_ARRIVER, @P_COND_SHIPMENT,	@P_FG_BL,		@P_FG_LC,		@P_COND_PAY,		@P_COND_DAYS,     
			   @P_DT_PAYABLE,	@P_REMARK1,			@P_REMARK2,		@P_REMARK3,		@P_ID_INSERT,		@P_DTS_INSERT)
			
		-- ������̺��� �������θ� 'Y' ������Ʈ �Ѵ�.    
		UPDATE TR_EXTO SET   YN_BL = 'Y'    
		WHERE CD_COMPANY = @P_CD_COMPANY AND NO_TO = @P_NO_TO    
	END    
END
GO   


  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_UPDATE'))
DROP PROCEDURE UP_TR_EXBL_UPDATE
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXBL_UPDATE
 * ����������: ���� >> ������� >> ����
 * ��  ��  ��: 
 * EXEC UP_TR_EXBL_UPDATE '',''
 *********************************************************************************************************/ 
 
CREATE PROCEDURE UP_TR_EXBL_UPDATE    
	@P_NO_BL			NVARCHAR(20),    
	@P_CD_COMPANY		NVARCHAR(7),    
	@P_NO_TO			NVARCHAR(20),    
	@P_NO_INV			NVARCHAR(20),    
	@P_CD_BIZAREA		NVARCHAR(7),    
	
	@P_CD_SALEGRP		NVARCHAR(7),    
	@P_NO_EMP			NVARCHAR(10),    
	@P_CD_PARTNER		NVARCHAR(7),    
	@P_DT_BALLOT		NCHAR(8),    
	@P_CD_EXCH			NVARCHAR(3),    
	
	@P_RT_EXCH			NUMERIC(15,4),    
	@P_AM_EX			NUMERIC(17,4),    
	@P_AM				NUMERIC(17,4),    
	@P_AM_EXNEGO		NUMERIC(17,4),    
	@P_AM_NEGO			NUMERIC(17,4),    
	
	@P_YN_SLIP			NCHAR(1),    
	@P_NO_SLIP			NVARCHAR(12),    
	@P_CD_EXPORT		NVARCHAR(7),    
	@P_DT_LOADING		NCHAR(8),    
	@P_DT_ARRIVAL		NCHAR(8),    
	
	@P_SHIP_CORP		NVARCHAR(7),    
	@P_NM_VESSEL		NVARCHAR(50),    
	@P_PORT_LOADING		NVARCHAR(50),    
	@P_PORT_NATION		NVARCHAR(3),    
	@P_PORT_ARRIVER		NVARCHAR(50),    
	
	@P_COND_SHIPMENT	NVARCHAR(3),    
	@P_FG_BL			NVARCHAR(3),    
	@P_FG_LC			NVARCHAR(3),    
	@P_COND_PAY			NVARCHAR(3),    
	@P_COND_DAYS		NUMERIC(4,0),    
	
	@P_DT_PAYABLE		NCHAR(8),    
	@P_REMARK1			NVARCHAR(100),    
	@P_REMARK2			NVARCHAR(100),    
	@P_REMARK3			NVARCHAR(100),    
	@P_ID_UPDATE		NVARCHAR(15)    
AS    
BEGIN 
	DECLARE @P_DTS_UPDATE VARCHAR(14)    
	SET @P_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')    

	UPDATE TR_EXBL 
	SET     
		NO_TO			= @P_NO_TO,  
		NO_INV			= @P_NO_INV,  
		CD_BIZAREA		= @P_CD_BIZAREA,     
		CD_SALEGRP		= @P_CD_SALEGRP,  
		NO_EMP			= @P_NO_EMP,  
		CD_PARTNER		= @P_CD_PARTNER,     
		DT_BALLOT		= @P_DT_BALLOT,  
		CD_EXCH			= @P_CD_EXCH,  
		RT_EXCH			= @P_RT_EXCH,     
		AM_EX			= @P_AM_EX,  
		AM				= FLOOR(@P_AM),  
		AM_EXNEGO		= @P_AM_EXNEGO,     
		AM_NEGO			= @P_AM_NEGO,  
		YN_SLIP			= @P_YN_SLIP,  
		NO_SLIP			= @P_NO_SLIP,     
		CD_EXPORT		= @P_CD_EXPORT,  
		DT_LOADING		= @P_DT_LOADING,  
		DT_ARRIVAL		= @P_DT_ARRIVAL,     
		SHIP_CORP		= @P_SHIP_CORP,  
		NM_VESSEL		= @P_NM_VESSEL,  
		PORT_LOADING    = @P_PORT_LOADING,     
		PORT_NATION		= @P_PORT_NATION,  
		PORT_ARRIVER    = @P_PORT_ARRIVER,  
		COND_SHIPMENT   = @P_COND_SHIPMENT,     
		FG_BL			= @P_FG_BL,  
		FG_LC			= @P_FG_LC,  
		COND_PAY		= @P_COND_PAY,     
		COND_DAYS		= @P_COND_DAYS,  
		DT_PAYABLE		= @P_DT_PAYABLE,  
		REMARK1			= @P_REMARK1,     
		REMARK2			= @P_REMARK2,  
		REMARK3			= @P_REMARK3,  
		ID_UPDATE		= @P_ID_UPDATE,     
		DTS_UPDATE		= @P_DTS_UPDATE    
	WHERE NO_BL = @P_NO_BL    
	AND CD_COMPANY = @P_CD_COMPANY        
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_LINE_INSERT'))
DROP PROCEDURE UP_TR_EXBL_LINE_INSERT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXBL_LINE_INSERT
 * ����������: ���� >> ������� >> ��������
 * ��  ��  ��: 
 * EXEC UP_TR_EXBL_LINE_INSERT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXBL_LINE_INSERT
	@P_CD_COMPANY	NVARCHAR(7),
	@P_NO_BL		NVARCHAR(20),
	@P_NO_TO		NVARCHAR(20),
	@P_NO_INV		NVARCHAR(20),
	@P_DTS_INSERT	NVARCHAR(14),
	@P_ID_INSERT	NVARCHAR(15),
	@P_DTS_UPDATE	NVARCHAR(14),
	@P_ID_UPDATE	NVARCHAR(15)
AS
BEGIN
	IF NOT EXISTS(SELECT TOP 1 NO_BL FROM TR_EXBLL WHERE CD_COMPANY = @P_CD_COMPANY AND NO_BL = @P_NO_BL AND NO_TO = @P_NO_TO AND NO_INV = @P_NO_INV)
	BEGIN
		INSERT INTO TR_EXBLL(CD_COMPANY, NO_BL, NO_TO, NO_INV, DTS_INSERT, ID_INSERT)
		VALUES(@P_CD_COMPANY, @P_NO_BL, @P_NO_TO, @P_NO_INV, @P_DTS_INSERT, @P_ID_INSERT)
	END
	
	ELSE
	BEGIN
		UPDATE TR_EXBLL
		SET
			DTS_UPDATE	= @P_DTS_UPDATE,
			ID_UPDATE	= @P_ID_UPDATE
		WHERE CD_COMPANY = @P_CD_COMPANY 
		AND NO_BL = @P_NO_BL 
		AND NO_TO = @P_NO_TO 
		AND NO_INV = @P_NO_INV
	END
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_INV_SELECT'))
DROP PROCEDURE UP_TR_EXBL_INV_SELECT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXBL_INV_SELECT
 * ����������: ���� >> ������� >> �����ȣ��ȸ
 * ��  ��  ��: 
 * EXEC UP_TR_EXBL_INV_SELECT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXBL_INV_SELECT 
	@P_CD_COMPANY	NVARCHAR(7),    
	@P_NO_TO		NVARCHAR(1000)
AS
BEGIN   
	SELECT A.*, B.AM_EX
	FROM
	(
		SELECT  DISTINCT A.CD_COMPANY, A.NO_TO,	  B.NO_INV,		A.CD_BIZAREA,     A.CD_SALEGRP,   A.NO_EMP,     
				A.FG_LC,	  A.CD_PARTNER, A.FG_EXLICENSE,   A.NO_EXLICENSE, A.DT_LICENSE,   
				A.CD_EXCH,    A.RT_LICENSE, A.RT_EXCH,	A.AM,      
				A.AM_EXFOB,   A.AM_FOB,     A.AM_FREIGHT,     A.AM_INSUR,     A.CD_AGENT,  
				A.CD_PRODUCT, A.CD_EXPORT,  A.FG_RETURN,      A.DT_DECLARE,   A.DC_DECLARE,    
				A.CD_CUSTOMS, A.DC_CY,      A.NO_INSP,		  A.DT_INSP,      A.NO_QUAR,    
				A.DT_QUAR,    A.TP_PACKING, A.CNT_PACKING,    A.REMARK1,      A.REMARK2,     
				A.REMARK3,    A.YN_BL,    
				(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) NM_PARTNER,    
				(SELECT NM_KOR FROM MA_EMP WHERE CD_COMPANY = A.CD_COMPANY AND NO_EMP = A.NO_EMP) NM_KOR,    
				(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = A.CD_SALEGRP) NM_SALEGRP,    
				(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_EXPORT) NM_EXPORT,    
				(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PRODUCT) NM_PRODUCT,    
				(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_AGENT) NM_AGENT,    
				(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_CUSTOMS) NM_CUSTOMS,    
				(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = A.CD_COMPANY AND CD_BIZAREA = A.CD_BIZAREA) NM_BIZAREA    
		FROM  TR_EXTO A
		LEFT OUTER JOIN TR_EXTOL B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_TO = B.NO_TO
		LEFT OUTER JOIN TR_INVL INVL ON B.CD_COMPANY = INVL.CD_COMPANY AND B.NO_INV = INVL.NO_INV
		WHERE A.CD_COMPANY = @P_CD_COMPANY
		AND B.NO_TO IN (SELECT * FROM TF_GETSPLIT(@P_NO_TO))  
	)A

	INNER JOIN
	(
		SELECT A.CD_COMPANY, A.NO_TO, INVL.NO_INV, SUM(INVL.AM_EXSO) AM_EX
		FROM TR_EXTO A
		LEFT OUTER JOIN TR_EXTOL B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_TO = B.NO_TO
		LEFT OUTER JOIN TR_INVL INVL ON B.CD_COMPANY = INVL.CD_COMPANY AND B.NO_INV = INVL.NO_INV
		WHERE A.CD_COMPANY = @P_CD_COMPANY
		AND B.NO_TO IN (SELECT * FROM TF_GETSPLIT(@P_NO_TO)) 
		GROUP BY A.CD_COMPANY, A.NO_TO, INVL.NO_INV
	)B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_TO = B.NO_TO AND A.NO_INV = B.NO_INV
END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_DOCU'))
DROP PROCEDURE UP_TR_EXBL_DOCU
GO 
/**********************************************************************************************************  
**  System : ����
**  Sub System : �������    
**  Page  : ������� ��ǥó�� 
**  ��  �� :       
**  Return Values      
**      
**  ��    ��    ��  : ������ ���強      
**  ��    ��    ��  : 2008.01.03 
**  ��    ��    ��     :      
**  ��    ��    ��   �� :  
 *********************************************************************************************************/     
CREATE PROCEDURE UP_TR_EXBL_DOCU
(      
	@P_CD_COMPANY   NVARCHAR(7), --ȸ���ڵ�       
	@P_NO_BL		NVARCHAR(20) --������ȣ     
)      
AS      
BEGIN    
	DECLARE @CD_COMPANY		NVARCHAR(7)      
	DECLARE @CD_MNG			NVARCHAR(20)      
	DECLARE @CD_BIZAREA		NVARCHAR(7)      
	DECLARE @NM_BIZAREA		NVARCHAR(15)    
	DECLARE @CD_WDEPT		NVARCHAR(12)      
	DECLARE @CD_PARTNER		NVARCHAR(7)      
	DECLARE @NM_PARTNER		NVARCHAR(20)    
	DECLARE @TP_TAX			NCHAR(2)      
	DECLARE @NM_TAX			NCHAR(15)    
	DECLARE @ID_WRITE		NVARCHAR(10)      
	DECLARE @DT_ACCT		NCHAR(8)      
	DECLARE @DT_START		NCHAR(8)      
	DECLARE @DT_WRITE		NCHAR(8)      
	DECLARE @AM_DR			NUMERIC(19,4)      
	DECLARE @AM_CR			NUMERIC(19,4)     
	DECLARE @AM_SUPPLY		NUMERIC(19,4)     
	DECLARE @VAT_AM			NUMERIC(19,4) /* �ΰ��� ���� üũ�ϱ� ���� ���� */    
	DECLARE @DT_TAX			NCHAR(8)      
	DECLARE @CD_CC			NVARCHAR(12)     
	DECLARE @NM_CC			NVARCHAR(20)     
	DECLARE @CD_EMPLOY		NVARCHAR(10)      
	DECLARE @CD_PJT			NVARCHAR(20)      
	DECLARE @CD_EXCH		NVARCHAR(3)      
	DECLARE @RT_EXCH		NUMERIC(11,4)      
	DECLARE @CD_DEPT		NVARCHAR(12)  
	DECLARE @NM_DEPT		NVARCHAR(50)
	DECLARE @CD_ACCT		NVARCHAR(10)      
	DECLARE @CD_PC			NVARCHAR(7)      
	DECLARE @TP_DRCR		NVARCHAR(1)      
	DECLARE @NM_EMP			NVARCHAR(50)      
	DECLARE @NM_PJT			NVARCHAR(50)
	DECLARE @AM_EXSO		NUMERIC(19,4)
	DECLARE @TP_ACAREA		NCHAR(1)  
	DECLARE @CD_RELATION	NCHAR(2)  --�ΰ��� �����׸�(31)����    
	DECLARE @NO_LC			NVARCHAR(20)  
	DECLARE @NO_BL			NVARCHAR(20)  
	DECLARE @NO_BIZAREA		NVARCHAR(20)  
	DECLARE @P_NO_TO		NVARCHAR(20)   -- 2008�� 7�� 21�� ����ΰ��� �߰�
	DECLARE @P_DT_LOADING	NVARCHAR(8)    -- 2008�� 7�� 21�� ����ΰ��� �߰�  
	DECLARE @P_ERRORCODE	NCHAR(10)    
	DECLARE @P_ERRORMSG		NVARCHAR(300) 

	DECLARE EXBL_CURSOR CURSOR FOR      

	/******************* */    
	/* ���� : ��ȭ�ܻ����� */    
	/******************* */    
		SELECT EXBL.CD_COMPANY,       
		MB.CD_PC,    
		(SELECT CD_DEPT FROM MA_EMP WHERE EXBL.CD_COMPANY = CD_COMPANY AND EXBL.NO_EMP = NO_EMP) AS CD_WDEPT,      
		EXBL.NO_EMP AS ID_WRITE,        
		EXBL.DT_BALLOT AS DT_ACCT,     
		'1' TP_DRCR, -- ���뱸�� 1: ����      
		--TC.CD_ACIMUNARRIVAL AS CD_ACCT,
		AISPOSTL.CD_ACCT,
		SUM( QTIO.AM_CLS ) AM_DR,
		0 AM_CR,      
		0 AM_SUPPLY,    
		'10' CD_RELATION,     
		QTIO.FG_TAX AS TP_TAX,       
		EXBL.CD_BIZAREA,    
		SALEGRP.CD_CC,    
		EMP.CD_DEPT,
		(SELECT NM_DEPT FROM MA_DEPT WHERE EXBL.CD_COMPANY = CD_COMPANY AND EMP.CD_DEPT = CD_DEPT) AS NM_DEPT,
		QTIO.NO_EMP AS CD_EMPLOY,       
		EXBL.CD_PARTNER,       
		EXBL.DT_BALLOT AS DT_START,    
		EXBL.DT_BALLOT AS DT_WRITE,    
		(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = EXBL.CD_COMPANY AND CD_BIZAREA = EXBL.CD_BIZAREA) NM_BIZAREA,    
		(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = EXBL.CD_COMPANY AND CD_SYSDEF = QTIO.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,    
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = EXBL.CD_COMPANY AND CD_PARTNER = EXBL.CD_PARTNER) NM_PARTNER,  
		INVL.NO_LC, 
		EXBL.NO_BL, 
		MB.NO_BIZAREA,
		QTIO.CD_PJT,    
		EXBL.CD_EXCH,      
		EXBL.RT_EXCH,
		(SELECT NM_CC FROM MA_CC WHERE EXBL.CD_COMPANY = CD_COMPANY AND SALEGRP.CD_CC = CD_CC)NM_CC,   
		(SELECT NM_KOR FROM MA_EMP WHERE EXBL.CD_COMPANY = CD_COMPANY AND QTIO.NO_EMP = NO_EMP)NM_EMP,
		(SELECT NM_PROJECT FROM SA_PROJECTH WHERE EXBL.CD_COMPANY = CD_COMPANY AND QTIO.CD_PJT = NO_PROJECT)NM_PJT,
		SUM( INVL.AM_EXSO) AM_EXSO,
		(CASE F.TP_DRCR WHEN '1' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA,
		'' NO_TO,
		'' DT_LOADING
	FROM TR_EXBL EXBL
	INNER JOIN TR_EXBLL EXBLL ON EXBL.CD_COMPANY = EXBLL.CD_COMPANY AND EXBL.NO_BL = EXBLL.NO_BL
	INNER JOIN MA_BIZAREA MB ON  EXBL.CD_BIZAREA = MB.CD_BIZAREA AND EXBL.CD_COMPANY = MB.CD_COMPANY      
	INNER JOIN TR_INVL INVL ON EXBLL.CD_COMPANY = INVL.CD_COMPANY AND EXBLL.NO_INV = INVL.NO_INV
	INNER JOIN MM_QTIO QTIO ON EXBL.CD_COMPANY = QTIO.CD_COMPANY AND INVL.NO_QTIO = QTIO.NO_IO AND INVL.NO_LINE_QTIO = QTIO.NO_IOLINE
	INNER JOIN MA_SALEGRP SALEGRP ON EXBL.CD_COMPANY = SALEGRP.CD_COMPANY AND QTIO.CD_GROUP = SALEGRP.CD_SALEGRP	
	INNER JOIN MA_AISPOSTL AISPOSTL ON EXBL.CD_COMPANY = AISPOSTL.CD_COMPANY AND AISPOSTL.FG_TP = '100' AND AISPOSTL.CD_TP = QTIO.FG_TPIO AND AISPOSTL.FG_AIS = '102'
	INNER JOIN MA_EMP EMP ON EMP.CD_COMPANY = EXBL.CD_COMPANY AND QTIO.NO_EMP = EMP.NO_EMP
	INNER JOIN FI_ACCTCODE F ON F.CD_ACCT = AISPOSTL.CD_ACCT AND F.CD_COMPANY = AISPOSTL.CD_COMPANY
	WHERE EXBL.CD_COMPANY = @P_CD_COMPANY
	AND EXBL.NO_BL = @P_NO_BL
	GROUP BY EXBL.CD_COMPANY, MB.CD_PC, EXBL.NO_EMP, EXBL.DT_BALLOT, QTIO.FG_TAX, EXBL.CD_BIZAREA, EXBL.CD_PARTNER,
	INVL.NO_LC, EXBL.NO_BL, MB.NO_BIZAREA, QTIO.CD_GROUP, QTIO.NO_EMP, AISPOSTL.CD_ACCT, SALEGRP.CD_CC, QTIO.CD_PJT,
	EXBL.CD_EXCH, EXBL.RT_EXCH, EMP.CD_DEPT, F.TP_DRCR, F.YN_BAN

	UNION ALL      

	 /******************* */    
	 /* �ΰ��� : ����ΰ��� */    
	 /******************* */    
	SELECT EXBL.CD_COMPANY,       
		MB.CD_PC,    
		(SELECT CD_DEPT FROM MA_EMP WHERE EXBL.CD_COMPANY = CD_COMPANY AND EXBL.NO_EMP = NO_EMP) AS CD_WDEPT,      
		EXBL.NO_EMP AS ID_WRITE,        
		EXBL.DT_BALLOT AS DT_ACCT,     
		'2' TP_DRCR, -- ���뱸�� 1: ����      
		--TC.CD_ACIMUNARRIVAL AS CD_ACCT,
		AISPOSTL.CD_ACCT,
		0 AM_DR,      
		0 AM_CR,      
		SUM( QTIO.AM_CLS ) AM_SUPPLY,    
		'31' CD_RELATION,     
		QTIO.FG_TAX AS TP_TAX,       
		EXBL.CD_BIZAREA,    
		'' CD_CC,    
		'' CD_DEPT,
		'' NM_DEPT,      
		'' CD_EMPLOY,       
		EXBL.CD_PARTNER,       
		EXBL.DT_BALLOT AS DT_START,    
		EXBL.DT_BALLOT AS DT_WRITE,    
		(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = EXBL.CD_COMPANY AND CD_BIZAREA = EXBL.CD_BIZAREA) NM_BIZAREA,    
		(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = EXBL.CD_COMPANY AND CD_SYSDEF = QTIO.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,    
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = EXBL.CD_COMPANY AND CD_PARTNER = EXBL.CD_PARTNER) NM_PARTNER,  
		INVL.NO_LC, 
		EXBL.NO_BL, 
		MB.NO_BIZAREA,
		'' CD_PJT,    
		EXBL.CD_EXCH,      
		EXBL.RT_EXCH,
		'' NM_CC,   
		'' NM_EMP,
		'' NM_PJT,
		SUM( INVL.AM_EXSO) AM_EXSO,
		'0' TP_ACAREA,
		EXBLL.NO_TO AS NO_TO,
		EXBL.DT_LOADING AS DT_LOADING
	FROM TR_EXBL EXBL
	INNER JOIN TR_EXBLL EXBLL ON EXBL.CD_COMPANY = EXBLL.CD_COMPANY AND EXBL.NO_BL = EXBLL.NO_BL
	INNER JOIN MA_BIZAREA MB ON  EXBL.CD_BIZAREA = MB.CD_BIZAREA AND EXBL.CD_COMPANY = MB.CD_COMPANY      
	INNER JOIN TR_INVL INVL ON EXBLL.CD_COMPANY = INVL.CD_COMPANY AND EXBLL.NO_INV = INVL.NO_INV
	INNER JOIN MM_QTIO QTIO ON EXBL.CD_COMPANY = QTIO.CD_COMPANY AND INVL.NO_QTIO = QTIO.NO_IO AND INVL.NO_LINE_QTIO = QTIO.NO_IOLINE
	INNER JOIN MA_SALEGRP SALEGRP ON EXBL.CD_COMPANY = SALEGRP.CD_COMPANY AND QTIO.CD_GROUP = SALEGRP.CD_SALEGRP	
	INNER JOIN MA_AISPOSTL AISPOSTL ON EXBL.CD_COMPANY = AISPOSTL.CD_COMPANY AND AISPOSTL.FG_TP = '100' AND AISPOSTL.CD_TP = QTIO.FG_TPIO AND AISPOSTL.FG_AIS = '103'
	INNER JOIN FI_ACCTCODE F ON F.CD_ACCT = AISPOSTL.CD_ACCT AND F.CD_COMPANY = AISPOSTL.CD_COMPANY
	WHERE EXBL.CD_COMPANY = @P_CD_COMPANY
	AND EXBL.NO_BL = @P_NO_BL
	GROUP BY EXBL.CD_COMPANY, MB.CD_PC, EXBL.NO_EMP, EXBL.DT_BALLOT, QTIO.FG_TAX, EXBL.CD_BIZAREA, EXBL.CD_PARTNER,
	INVL.NO_LC, EXBL.NO_BL, MB.NO_BIZAREA, QTIO.CD_GROUP, QTIO.NO_EMP, AISPOSTL.CD_ACCT, SALEGRP.CD_CC, QTIO.CD_PJT,
	EXBL.CD_EXCH, EXBL.RT_EXCH, F.YN_BAN, EXBLL.NO_TO, EXBL.DT_LOADING
	
	UNION ALL  
	  
	/*********************************************************** */    
	/* �뺯 : */    
	/*********************************************************** */    
	SELECT EXBL.CD_COMPANY,       
		MB.CD_PC,    
		(SELECT CD_DEPT FROM MA_EMP WHERE EXBL.CD_COMPANY = CD_COMPANY AND EXBL.NO_EMP = NO_EMP) AS CD_WDEPT,      
		EXBL.NO_EMP AS ID_WRITE,        
		EXBL.DT_BALLOT AS DT_ACCT,     
		'2' TP_DRCR, -- ���뱸�� 1: ����      
		--TC.CD_ACIMUNARRIVAL AS CD_ACCT,
		AISPOSTL.CD_ACCT,
		0 AM_DR,  
		SUM( QTIO.AM_CLS ) AM_CR,      
		SUM( QTIO.AM_CLS ) AM_SUPPLY,    
		'10' CD_RELATION,     
		QTIO.FG_TAX AS TP_TAX,       
		EXBL.CD_BIZAREA,    
		(SELECT CD_CC FROM MA_SALEGRP WHERE EXBL.CD_COMPANY = CD_COMPANY AND QTIO.CD_GROUP = CD_SALEGRP) AS CD_CC,    
		--(SELECT CD_DEPT FROM MA_EMP WHERE EXBL.CD_COMPANY = CD_COMPANY AND QTIO.NO_EMP = NO_EMP) AS CD_DEPT,    
		EMP.CD_DEPT,
		(SELECT NM_DEPT FROM MA_DEPT WHERE   EXBL.CD_COMPANY = CD_COMPANY AND EMP.CD_DEPT = CD_DEPT) AS NM_DEPT,
		QTIO.NO_EMP AS CD_EMPLOY,       
		EXBL.CD_PARTNER,       
		EXBL.DT_BALLOT AS DT_START,    
		EXBL.DT_BALLOT AS DT_WRITE,    
		(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = EXBL.CD_COMPANY AND CD_BIZAREA = EXBL.CD_BIZAREA) NM_BIZAREA,    
		(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = EXBL.CD_COMPANY AND CD_SYSDEF = QTIO.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,    
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = EXBL.CD_COMPANY AND CD_PARTNER = EXBL.CD_PARTNER) NM_PARTNER,  
		INVL.NO_LC, 
		EXBL.NO_BL, 
		MB.NO_BIZAREA,
		QTIO.CD_PJT,    
		EXBL.CD_EXCH,      
		EXBL.RT_EXCH,
		(SELECT NM_CC FROM MA_CC WHERE EXBL.CD_COMPANY = CD_COMPANY AND SALEGRP.CD_CC = CD_CC)NM_CC,   
		(SELECT NM_KOR FROM MA_EMP WHERE EXBL.CD_COMPANY = CD_COMPANY AND QTIO.NO_EMP = NO_EMP)NM_EMP,
		(SELECT NM_PROJECT FROM SA_PROJECTH WHERE EXBL.CD_COMPANY = CD_COMPANY AND QTIO.CD_PJT = NO_PROJECT)NM_PJT,
		SUM( INVL.AM_EXSO) AM_EXSO,
		(CASE F.TP_DRCR WHEN '1' THEN (CASE F.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA,
		'' NO_TO,
		'' DT_LOADING
		FROM TR_EXBL EXBL
		INNER JOIN TR_EXBLL EXBLL ON EXBL.CD_COMPANY = EXBLL.CD_COMPANY AND EXBL.NO_BL = EXBLL.NO_BL
		INNER JOIN MA_BIZAREA MB ON  EXBL.CD_BIZAREA = MB.CD_BIZAREA AND EXBL.CD_COMPANY = MB.CD_COMPANY      
		INNER JOIN TR_INVL INVL ON EXBLL.CD_COMPANY = INVL.CD_COMPANY AND EXBLL.NO_INV = INVL.NO_INV
		INNER JOIN MM_QTIO QTIO ON EXBL.CD_COMPANY = QTIO.CD_COMPANY AND INVL.NO_QTIO = QTIO.NO_IO AND INVL.NO_LINE_QTIO = QTIO.NO_IOLINE
		INNER JOIN MA_PITEM PITEM ON EXBL.CD_COMPANY = PITEM.CD_COMPANY AND QTIO.CD_PLANT = PITEM.CD_PLANT AND INVL.CD_ITEM = PITEM.CD_ITEM
		INNER JOIN MA_SALEGRP SALEGRP ON EXBL.CD_COMPANY = SALEGRP.CD_COMPANY AND QTIO.CD_GROUP = SALEGRP.CD_SALEGRP	
		INNER JOIN MA_AISPOSTL AISPOSTL ON EXBL.CD_COMPANY = AISPOSTL.CD_COMPANY AND AISPOSTL.FG_TP = '100' AND AISPOSTL.CD_TP = QTIO.FG_TPIO 
		INNER JOIN MA_EMP EMP ON EXBL.CD_COMPANY = EMP.CD_COMPANY AND QTIO.NO_EMP = EMP.NO_EMP
		INNER JOIN FI_ACCTCODE F ON F.CD_ACCT = AISPOSTL.CD_ACCT AND F.CD_COMPANY = AISPOSTL.CD_COMPANY
		AND AISPOSTL.FG_AIS = (CASE PITEM.CLS_ITEM WHEN '001' THEN '110'
								WHEN '002' THEN '110'
								WHEN '003' THEN '118'
								WHEN '004' THEN '114'
								WHEN '005' THEN '106'
								WHEN '006' THEN '106'
								WHEN '007' THEN '122'
								WHEN '008' THEN '120'
								ELSE '118' END)
	WHERE EXBL.CD_COMPANY = @P_CD_COMPANY
	AND EXBL.NO_BL = @P_NO_BL
	GROUP BY EXBL.CD_COMPANY, MB.CD_PC, EXBL.NO_EMP, EXBL.DT_BALLOT, QTIO.FG_TAX, EXBL.CD_BIZAREA, EXBL.CD_PARTNER,
	INVL.NO_LC, EXBL.NO_BL, MB.NO_BIZAREA, QTIO.CD_GROUP, QTIO.NO_EMP, AISPOSTL.CD_ACCT, SALEGRP.CD_CC, QTIO.CD_PJT,
	EXBL.CD_EXCH, EXBL.RT_EXCH, EMP.CD_DEPT, F.TP_DRCR, F.YN_BAN
	
	-------------------------------------------------------------------------------------------------------------------------------------------------------------------------    

	-- ���⼭ ���� ��ǥó�� �ϱ� ���� �κ�  ---      
	DECLARE @P_NO_DOCU NVARCHAR(20) -- ��ǥ��ȣ      
	DECLARE @P_NO_TAX NVARCHAR(20) -- �ΰ�����ȣ      
	DECLARE @P_DT_PROCESS NVARCHAR(8)      
	    
	-- �������� �˾ƿ���      
	SELECT @P_DT_PROCESS = DT_BALLOT      
	FROM TR_EXBL      
	WHERE CD_COMPANY = @P_CD_COMPANY AND NO_BL = @P_NO_BL     
	    
	-- ��ǥ��ȣ ä��      
	EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT      
	       
	-- NO_DOLINE ����      
	DECLARE @P_NO_DOLINE NUMERIC(4,0)      
	SET @P_NO_DOLINE = 0      
	    
	OPEN EXBL_CURSOR      
	    
	FETCH NEXT FROM EXBL_CURSOR INTO @CD_COMPANY, @CD_PC, @CD_WDEPT, @ID_WRITE, @DT_ACCT, @TP_DRCR, @CD_ACCT, @AM_DR,  @AM_CR,  @AM_SUPPLY, @CD_RELATION,     
		@TP_TAX,  @CD_BIZAREA,	@CD_CC,		@CD_DEPT,  @NM_DEPT, @CD_EMPLOY,  @CD_PARTNER,   @DT_START, @DT_WRITE, @NM_BIZAREA, @NM_TAX, @NM_PARTNER,  @NO_LC, @NO_BL, @NO_BIZAREA,
		@CD_PJT,  @CD_EXCH,		@RT_EXCH,	@NM_CC,		@NM_EMP,	@NM_PJT, @AM_EXSO, @TP_ACAREA, @P_NO_TO, @P_DT_LOADING
	WHILE @@FETCH_STATUS = 0      
	BEGIN      
		SET @P_NO_DOLINE = @P_NO_DOLINE + 1      
	 -- SET @AM_DR_CR = CONVERT(VARCHAR(40), @AM_CR+@AM_DR)      
	 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------    
	  
	DECLARE @P_COND NVARCHAR(10)  
	DECLARE @P_COND_BIZAREA NVARCHAR(10)  
	DECLARE @P_NM_EXCH NVARCHAR(20)
	
	SET @P_NM_EXCH = (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'MA_B000005' AND CD_SYSDEF = @CD_EXCH)
	  
	SELECT @P_COND = CD_MNG2 FROM FI_ACCTCODE WHERE CD_ACCT = @CD_ACCT AND CD_COMPANY = @CD_COMPANY  
	  
	--IF(@CD_RELATION = '31')  /* �ΰ��� ��ǥ ó���� ��� CD_RELATION�� 31 �� ���� �ΰ��������� ������ȣ�� CD_MNG(������ȣ)�� �ѱ�� */  
	--SET @CD_MNG = @P_NO_BL
	--SET @P_NO_TAX = @P_NO_BL 
	
	IF(@CD_RELATION = '31')
	BEGIN
		SET @CD_MNG = @P_NO_TO
		SET @P_NO_TAX = @P_NO_TO
	END
	
	ELSE
	BEGIN
		SET @CD_MNG = @P_NO_BL
		SET @P_NO_TAX = @P_NO_BL 
	END

	--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	-- ������ üũ����

	IF(@CD_PC = '' OR @CD_PC = NULL)  
	BEGIN
		RAISERROR 30033 '�ش� ȸ������� �������� �ʾҽ��ϴ�.'
			RETURN -1    
	END  
	
	--IF(@CD_CC = '' OR @CD_CC = NULL)  
	--BEGIN  
	--	RAISERROR 30033 '���� �����׷쿡 ���� COST CENTER�� �̼��� �����Դϴ�.'
	--			RETURN -1
	--END  
	
	IF(@CD_ACCT = '' OR @CD_ACCT = NULL)  
	BEGIN  
		RAISERROR 30033 '���� ǰ�� ���� ȸ������ڵ� ������ �̼����� ���� �����մϴ�.'
			RETURN -1
	END
	--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	EXEC UP_FI_AUTODOCU_1     
	 @P_NO_DOCU,		-- ��ǥ��ȣ  
	  @P_NO_DOLINE,		-- ���ι�ȣ  
	 @CD_PC,			-- ȸ�����  
	 @CD_COMPANY,		-- ȸ���ڵ�  
	 @CD_WDEPT,			-- �ۼ��μ�  
	 @ID_WRITE,         -- �ۼ����  
	 @P_DT_PROCESS,    
	 0,                 -- ȸ���ȣ  
	 '3',                         -- ��ǥ����  
	 '11',                       -- ��ǥ����  
	 '1',                         -- ��ǥ����  
	 NULL,                     -- ���λ��  
	 @TP_DRCR,           -- ���뱸��  
	 @CD_ACCT,          -- �����ڵ�  
	 NULL,                    -- �����  
	 @AM_DR,              -- �����ݾ�  
	 @AM_CR,              -- �뺯�ݾ�  
	 @TP_ACAREA,                          --  @P_TP_ACAREA    -- '4' �� ��� ����������ǥ�̹Ƿ� FI_BANH�� Insert�ȴ�    
	 @CD_RELATION,   -- �����׸�  
	 NULL,                    --  @P_CD_BUDGET    �����ڵ�  
	 NULL,                    --  @P_CD_FUND   �ڱ��ڵ�  
	 NULL,                    --  @P_NO_BDOCU   ������ǥ��ȣ   
	 NULL,                    --  @P_NO_BDOLINE   ������ǥ����  
	 '0',                        --  @P_TP_ETCACCT    Ÿ�뱸��  
	 @CD_BIZAREA,    
	 @NM_BIZAREA,      
	 @CD_CC,    
	 @NM_CC,    
	 @CD_PJT,  --CD_PJT    
	 @NM_PJT,  --NM_PJT    
	 @CD_DEPT,  --CD_DEPT    
	 @NM_DEPT,  --NM_DEPT    
	 @CD_EMPLOY,  --CD_EMPLOY    
	 @NM_EMP,  --NM_EMPLOY    
	 @CD_PARTNER,    
	 @NM_PARTNER,    
	 NULL,  --CD_DEPOSIT    
	 NULL,  --NM_DEPOSIT    
	 NULL,  --CD_CARD    
	 NULL,  --NM_CARD    
	 NULL,  --CD_BANK    
	 NULL,  --NM_BANK    
	 NULL,  --NO_ITEM    
	 NULL,  --NM_ITEM    
	 @TP_TAX,    
	 @NM_TAX,      
	 NULL,  --CD_TRADE    
	 NULL,  --NM_TRADE    
	 @CD_EXCH, --ȯ���ڵ�   
	 @P_NM_EXCH,    --ȯ����
	 NULL,  -- CD_UMNG1    
	 NULL,  -- CD_UMNG2    
	 NULL,  -- CD_UMNG3    
	 NULL,  -- CD_UMNG4    
	 NULL,  -- CD_UMNG5    
	 @NO_BIZAREA,  --  NO_RES    
	 @AM_SUPPLY,  --  AM_SUPPLY    
	 @CD_MNG ,   -- ������ȣ ( ������ȣ ��� �ѱ�� �� )  
	 @DT_START,  -- �߻�����    
	 NULL,   --DT_END   -- ��������  
	 @RT_EXCH,         --P_RT_EXCH   ȯ��  
	 @AM_EXSO,         --AM_EXDO   ��ȭ�ݾ�  
	 '170',     -- ������ȣ�� ����ȣ�� 170���� �ش�  
	 @P_NO_BL,   --  @P_NO_MDOCU   ��������ȣ  
	 NULL,    --  @P_CD_EPNOTE   
	 NULL,   --  @P_ID_INSERT   
	 NULL,   --CD_BGACCT    
	 NULL,   --TP_EPNOTE    
	 NULL,   --NM_PUMM    
	 @P_DT_PROCESS,    
	 0,        --AM_ACTSUM    
	 0,        --AM_JSUM    
	 'N',      --YN_GWARE    
	 NULL,  --CD_BIZPLAN    
	 NULL,  --CD_ETC    
	 @P_ERRORCODE,    
	 @P_ERRORMSG  
  
	--�߰�:������ 20071010 fi_tax�� �߰�����    
	IF @CD_RELATION = '31'     
	BEGIN    
		EXEC UP_FI_AUTODOCU_BL_TAX @CD_COMPANY, @CD_PC, @P_NO_DOCU, @P_NO_DOLINE, @AM_SUPPLY, @P_NO_TO, @P_DT_LOADING
	END    
	         
	-------------------------------------------------------------------------------------------------------------------------------------------------------------------------      
	         
	       
	FETCH NEXT FROM EXBL_CURSOR INTO @CD_COMPANY, @CD_PC, @CD_WDEPT, @ID_WRITE, @DT_ACCT, @TP_DRCR, @CD_ACCT, @AM_DR,  @AM_CR, @AM_SUPPLY, @CD_RELATION,     
	@TP_TAX,  @CD_BIZAREA, @CD_CC, @CD_DEPT,  @NM_DEPT, @CD_EMPLOY,  @CD_PARTNER,   @DT_START, @DT_WRITE, @NM_BIZAREA, @NM_TAX, @NM_PARTNER, @NO_LC, @NO_BL, @NO_BIZAREA,
	@CD_PJT,  @CD_EXCH,		@RT_EXCH,	@NM_CC,		@NM_EMP,	@NM_PJT, @AM_EXSO ,@TP_ACAREA, @P_NO_TO, @P_DT_LOADING
	END      
	      
	CLOSE EXBL_CURSOR      
	DEALLOCATE EXBL_CURSOR     
	      
	-- ��ǥó���� ����� �Ǿ�����      
	UPDATE TR_EXBL SET YN_SLIP = 'Y' WHERE CD_COMPANY = @P_CD_COMPANY AND NO_BL = @P_NO_BL      
END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_COSTBF_SELECT'))
DROP PROCEDURE UP_TR_EXBL_COSTBF_SELECT
GO 

/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXBL_COSTBF_SELECT
 * ����������: 
 * ��      ��: �Ǹź���� ��ư Ŭ���� �ѱ� ���ڰ��� �����´�.
 * ��  ��  ��: 
 * EXEC UP_TR_EXBL_COSTBF_SELECT '',''
 *********************************************************************************************************/
 
CREATE PROCEDURE UP_TR_EXBL_COSTBF_SELECT  
	@P_CD_COMPANY	NVARCHAR(7),  
	@P_NO_BL		NVARCHAR(20)  
AS      
BEGIN
	SELECT EXBL.DT_BALLOT, 
		EXBL.CD_BIZAREA,
		(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE EXBL.CD_COMPANY = CD_COMPANY AND EXBL.CD_BIZAREA = CD_BIZAREA) AS NM_BIZAREA,
		EXBL.NO_EMP,
		EMP.NM_KOR AS NM_EMP,
		EMP.CD_DEPT,
		(SELECT NM_DEPT FROM MA_DEPT WHERE EXBL.CD_COMPANY = CD_COMPANY AND EMP.CD_DEPT = CD_DEPT) AS NM_DEPT,
		SAL.CD_CC,
		(SELECT NM_CC FROM MA_CC WHERE EXBL.CD_COMPANY = CD_COMPANY AND SAL.CD_CC = CD_CC) AS NM_CC
	FROM TR_EXBL EXBL
	LEFT OUTER JOIN MA_EMP EMP ON EXBL.CD_COMPANY = EMP.CD_COMPANY AND EXBL.NO_EMP = EMP.NO_EMP
	LEFT OUTER JOIN MA_SALEGRP SAL ON EXBL.CD_COMPANY = SAL.CD_COMPANY AND EXBL.CD_SALEGRP = SAL.CD_SALEGRP
	WHERE EXBL.CD_COMPANY = @P_CD_COMPANY
	AND EXBL.NO_BL = @P_NO_BL
END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_QTIO_SELECT'))
DROP PROCEDURE UP_TR_EXBL_QTIO_SELECT
GO 

/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXBL_QTIO_SELECT
 * ����������: 
 * ��      ��: 
 * ��  ��  ��: 
 * EXEC UP_TR_EXBL_QTIO_SELECT '0327','IVC080403'
 *********************************************************************************************************/
 
CREATE PROCEDURE UP_TR_EXBL_QTIO_SELECT
	@P_CD_COMPANY	NVARCHAR(7),  
	@P_NO_BL		NVARCHAR(20)  
AS      
BEGIN
	SELECT DISTINCT EXBLL.NO_INV,
		CAST(FLOOR(INVL.AM_EXSO * EXBL.RT_EXCH) AS NUMERIC) AS AM_EXSO,
		INVL.NO_QTIO, 
		INVL.NO_LINE_QTIO,
		INVL.QT_INVENT,
		INVL.UM_INVENT,
		INVL.QT_SO
	FROM TR_EXBL EXBL
	LEFT OUTER JOIN TR_EXBLL EXBLL ON EXBL.CD_COMPANY = EXBLL.CD_COMPANY AND EXBL.NO_BL = EXBLL.NO_BL
	LEFT OUTER JOIN TR_EXTOL EXTOL ON EXBL.CD_COMPANY = EXTOL.CD_COMPANY AND EXBLL.NO_TO = EXTOL.NO_TO
	INNER JOIN TR_EXTO EXTO ON EXBL.CD_COMPANY = EXTO.CD_COMPANY AND EXBLL.NO_TO = EXTO.NO_TO
	LEFT OUTER JOIN TR_INVL INVL ON EXBL.CD_COMPANY = INVL.CD_COMPANY AND EXBLL.NO_INV = INVL.NO_INV 
	WHERE EXBL.CD_COMPANY = @P_CD_COMPANY
	AND EXBLL.NO_BL = @P_NO_BL

	SELECT BL.NO_BL,
		CAST(FLOOR((BL.AM_EX*BL.RT_EXCH))AS NUMERIC) - CAST(SUM(FLOOR(IL.AM_EXSO*BL.RT_EXCH)) AS NUMERIC)  AS AM_CLS
	FROM TR_INVL IL
	JOIN TR_EXBL BL ON BL.NO_BL = IL.NO_BL AND BL.CD_COMPANY = IL.CD_COMPANY
	WHERE BL.CD_COMPANY = @P_CD_COMPANY
	AND BL.NO_BL = @P_NO_BL
	GROUP BY BL.AM_EX,BL.RT_EXCH,BL.NO_BL
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_QTIO_UPDATE'))
DROP PROCEDURE UP_TR_EXBL_QTIO_UPDATE
GO 

/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXBL_QTIO_UPDATE
 * ����������: 
 * ��      ��: 
 * ��  ��  ��: 
 * EXEC UP_TR_EXBL_QTIO_UPDATE '',''
 *********************************************************************************************************/
 
CREATE PROCEDURE UP_TR_EXBL_QTIO_UPDATE
	@P_CD_COMPANY	NVARCHAR(7),  
	@P_NO_QTIO		NVARCHAR(20),
	@P_NO_LINE_QTIO NUMERIC(3,0),
	@P_AM_CLS		NUMERIC(17,4),
	@P_QT_INVENT	NUMERIC(17,4),
	@P_UM_INVENT	NUMERIC(17,4),
	@P_QT_SO		NUMERIC(17,4)
AS      
BEGIN
	UPDATE MM_QTIO
	SET
		QT_CLS		= @P_QT_INVENT,
		AM_CLS		= @P_AM_CLS,
		VAT			= 0,
		VAT_CLS		= 0,
		UM_EX_PSO	= @P_UM_INVENT,
		QT_CLS_MM	= @P_QT_SO
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND NO_IO = @P_NO_QTIO
	AND NO_IOLINE = @P_NO_LINE_QTIO
	
	IF @@ROWCOUNT = 0 
	BEGIN
		RAISERROR 100000 '���ü��ҳ����� ������Ʈ ���� �ʾҽ��ϴ�'
	END
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_QTIO_DELETE_UPDATE'))
DROP PROCEDURE UP_TR_EXBL_QTIO_DELETE_UPDATE
GO 

/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXBL_QTIO_DELETE_UPDATE
 * ����������: 
 * ��      ��: 
 * ��  ��  ��: 
 * EXEC UP_TR_EXBL_QTIO_DELETE_UPDATE '',''
 *********************************************************************************************************/
 
CREATE PROCEDURE UP_TR_EXBL_QTIO_DELETE_UPDATE
	@P_CD_COMPANY	NVARCHAR(7),  
	@P_NO_BL		NVARCHAR(20)
AS      
BEGIN
	UPDATE MM_QTIO 
	SET QT_CLS = 0,
		AM_CLS = 0,
		QT_CLS_MM = 0
	FROM 
	(
		SELECT B.NO_BL,MM_QTIO.NO_IO, MM_QTIO.NO_IOLINE, B.CD_BIZAREA
		FROM  MM_QTIO 
		LEFT OUTER JOIN TR_INVL I ON MM_QTIO.NO_IO = I.NO_QTIO AND MM_QTIO.NO_IOLINE = I.NO_LINE_QTIO AND MM_QTIO.CD_COMPANY = I.CD_COMPANY AND MM_QTIO.CD_PLANT = I.CD_PLANT
		LEFT OUTER JOIN TR_EXBL B ON B.CD_COMPANY = I.CD_COMPANY AND B.NO_BL = I.NO_BL 
		WHERE B.NO_BL = @P_NO_BL
	)A 
	WHERE MM_QTIO.CD_COMPANY = @P_CD_COMPANY
	AND MM_QTIO.NO_IO = A.NO_IO 
	AND MM_QTIO.NO_IOLINE = A.NO_IOLINE
	AND MM_QTIO.CD_BIZAREA = A.CD_BIZAREA
	
	IF @@ROWCOUNT = 0 
	BEGIN
		RAISERROR 100000 '���ü��ҳ����� �ʱ�ȭ���� ���߽��ϴ�'
	END
END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_FI_AUTODOCU_BL_TAX'))
DROP PROCEDURE UP_FI_AUTODOCU_BL_TAX
GO 

CREATE PROCEDURE UP_FI_AUTODOCU_BL_TAX          
	@P_CD_COMPANY   NVARCHAR(7),             
	@P_CD_PC		NVARCHAR(7),        
	@P_NO_DOCU		NVARCHAR(20),        
	@P_NO_DOLINE	NUMERIC(5,0),         
	@P_AM_SUPPLY	NUMERIC(19,4),       
	@P_NO_TO		NVARCHAR(20),	  -- ����Ű��ȣ  -- �߰�  ���� �̿��� ��� '' �Է�
	@P_DT_SHIPPING	NVARCHAR(8)       -- ��������	   -- �߰�  ���� �̿��� ��� '' �Է�
AS            
BEGIN       
	DECLARE @V_NO_TAX NVARCHAR(20)    
	DECLARE @V_ERRORMSG NVARCHAR(1000)    

	SELECT @V_NO_TAX = CD_MNG    
	FROM FI_DOCU    
	WHERE         
	NO_DOCU = @P_NO_DOCU AND -- ��ǥ��ȣ            
	NO_DOLINE = @P_NO_DOLINE AND -- ���ι�ȣ            
	CD_PC = @P_CD_PC AND  -- ȸ�����            
	CD_COMPANY = @P_CD_COMPANY  -- ȸ���ڵ�      
    
	-- �����Ϸ����ϴ� CD_MNG�� �̹� �����ϴ� NO_TAX���� Ȯ��    
	IF EXISTS (SELECT NO_TAX FROM FI_TAX WHERE  CD_COMPANY = @P_CD_COMPANY AND NO_TAX = @V_NO_TAX)    
		BEGIN    
		SET @V_ERRORMSG = @V_NO_TAX + '�� FI_TAX�� �̹� �����ϴ� NO_TAX �Դϴ�.'    
		RAISERROR 100000 @V_ERRORMSG    
		RETURN    
	END    
    
	INSERT INTO FI_TAX ( NO_TAX,	CD_COMPANY,		NO_DOCU,		NO_DOLINE,		CD_PC,        
						ST_DOCU,	CD_BIZAREA,		DT_START,		CD_PARTNER,		AM_TAXSTD,        
						AM_ADDTAX,	TP_TAX,			AM_CASH,		AM_CHECK,		AM_BILL,        
						AM_UNCLT,	RT_SBS,			RT_EXCH,		AM_EX,			AM_EXDO,        
						AM_DO,    	AM_EXMI,    	AM_MI,    		ID_UPDATE,		CD_EXCH,  	
						NO_TO,		DT_SHIPPING,	TP_EXPORT ) 
	SELECT         
		A.CD_MNG,			-- NO_TAX : �ΰ�����ȣ
		A.CD_COMPANY,		-- CD_COMPANY : ȸ���ڵ�    
		A.NO_DOCU,			-- NO_DOCU : ��ǥ��ȣ
		A.NO_DOLINE,		-- NO_DOLINE : ���ι�ȣ  
		A.CD_PC,			-- CD_PC : ȸ�����
		'1',				-- ST_DOCU : ��ǥ ����
		A.CD_BIZAREA,		-- CD_BIZAREA : �Ű�����  
		A.DT_ACCT,			-- DT_START : �߻�����
		A.CD_PARTNER,		-- CD_PARTNER : �ŷ�ó�ڵ�
		@P_AM_SUPPLY,		-- AM_TAXSTD : ����ǥ��(��ȭ)
		A.AM_CR + A.AM_DR,  -- AM_ADDTAX : �ΰ���ġ��
		A.TP_TAX,			-- TP_TAX : ��������      
		0,					-- AM_CASH : ����
		0,					-- AM_CHECK : ��ǥ
		0,					-- AM_BILL : ����
		0,					-- AM_UNCLT : �̼���         
		0,					-- RT_SBS : ������      
		A.RT_EXCH,			-- RT_EXCH : ȯ��(����Ű���)     
		A.AM_EXDO,			-- AM_EX : ��ȭ      
		0,					-- AM_EXDO : �����ȭ      
		0,					-- AM_DO : �����ȭ      
		0,					-- AM_EXMI : �̵�����ȭ      
		0,					-- AM_MI : �̵�����ȭ
		(SELECT TOP 1 NO_COMPANY FROM FI_PARTNO WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) NO_COMPANY, -- ����ڹ�ȣ�� �ϳ��ε� �ŷ�ó�� �ΰ� �ִ°͵��� �־ TOP 1 �� ����� �Ѵ�.  
		--- 2008/07/14 �߰��κ�
		A.CD_EXCH,            -- CD_EXCH : ȯ��
		@P_NO_TO,				-- ��ǥ���� ������ �ü� ���� �ڷ� - �Ű��ȣ
		@P_DT_SHIPPING,		-- ��ǥ���� ������ �ü� ���� �ڷ� - ��������
		(CASE WHEN A.TP_TAX = '15' THEN '1' ELSE '' END)  TP_EXPORT  --���ⱸ��
	FROM FI_DOCU A  
	WHERE A.NO_DOCU = @P_NO_DOCU AND -- ��ǥ��ȣ            
	A.NO_DOLINE = @P_NO_DOLINE AND -- ���ι�ȣ            
	A.CD_PC = @P_CD_PC AND  -- ȸ�����            
	A.CD_COMPANY = @P_CD_COMPANY  -- ȸ���ڵ�          
END
GO
