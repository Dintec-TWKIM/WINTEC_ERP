IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXTO_SELECT'))
DROP PROCEDURE UP_TR_EXTO_SELECT
GO

/***********************************************************************    
**  System : ����  
**  Sub System : ����  
**  Page  : ������  
**  Desc  : ������ �ʱ�ȭ �� ��ȸ   
**  ������ : 
************************************************************************/

CREATE PROC UP_TR_EXTO_SELECT
	@CD_COMPANY		NVARCHAR(7),    
	@NO_TO			NVARCHAR(20)  -- �����ȣ  
AS    
BEGIN
	SELECT   
		NO_TO,			-- �����ȣ  
		NO_INV,			-- �����ȣ  
		CD_BIZAREA,		-- �����  
		CD_SALEGRP,		-- �����׷�  
		NO_EMP,			-- �����  
		FG_LC,			-- L/C����  
		CD_PARTNER,		-- �ŷ�ó  
		FG_EXLICENSE,	-- ������屸��   
		NO_EXLICENSE,	-- ��������ȣ  
		DT_LICENSE,		-- ��������  
		CD_EXCH,		-- ȯ��  
		RT_LICENSE,		-- ����ȯ��  
		RT_EXCH,		-- ��ǥȯ��  
		AM_EX,			-- �����ȭ�ݾ�  
		AM,				-- �����ȭ�ݾ�  
		AM_EXFOB,		-- FOB��ȭ�ݾ�  
		AM_FOB,			-- FOB��ȭ�ݾ�  
		AM_FREIGHT,		-- ����  
		AM_INSUR,		-- �����  
		CD_AGENT,		-- ������  
		CD_PRODUCT,		-- ������  
		CD_EXPORT,		-- ������  
		FG_RETURN,		--   
		DT_DECLARE,		-- �Ű�����   
		DC_DECLARE,		-- ��۽Ű���  
		CD_CUSTOMS,		-- ���Ҽ���  
		DC_CY,			-- ������ġ���  
		NO_INSP,		-- �˻�����ȣ  
		DT_INSP,		-- �˻����߱���   
		NO_QUAR,		-- �˿�����ȣ  
		DT_QUAR,		-- �˿����߱���  
		TP_PACKING,		-- ��������  
		CNT_PACKING,	-- �����尹��  
		REMARK1,		-- ���1  
		REMARK2,		-- ���2  
		REMARK3,		-- ���3  
		YN_BL,			-- B/L��Ͽ���  
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) NM_PARTNER,    
		(SELECT NM_KOR FROM MA_EMP WHERE CD_COMPANY = A.CD_COMPANY AND NO_EMP = A.NO_EMP) NM_KOR,    
		(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = A.CD_SALEGRP) NM_SALEGRP,    
		(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = A.CD_COMPANY AND CD_BIZAREA = A.CD_BIZAREA) NM_BIZAREA,    
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_EXPORT) NM_EXPORT,    
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PRODUCT) NM_PRODUCT,    
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_AGENT) NM_AGENT,    
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_CUSTOMS) NM_CUSTOMS    
	FROM TR_EXTO A    
	WHERE A.CD_COMPANY = @CD_COMPANY 
	AND A.NO_TO = @NO_TO  
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXTO_DELETE'))
DROP PROCEDURE UP_TR_EXTO_DELETE
GO

/***********************************************************************    
**  System : ����  
**  Sub System : ����  
**  Page  : ������  
**  Desc  : ������ ����
**  ������ : 
************************************************************************/

CREATE PROCEDURE UP_TR_EXTO_DELETE  
	@P_CD_COMPANY   NVARCHAR(7),  
	@P_NO_TO		NVARCHAR(20)  
AS    
BEGIN  
	IF EXISTS (SELECT * FROM TR_COSTEXH WHERE CD_COMPANY = @P_CD_COMPANY AND  FG_PRODUCT = '003' AND  NO_TO = @P_NO_TO)  
	BEGIN  
		SET NOCOUNT OFF  
		RAISERROR -5000 '�ǸŰ�� ��ϵǾ� �����Ƿ� ������ �� �����ϴ�.'  
		RETURN  
	END  

	-- ������ ���� ����
	DELETE FROM TR_EXTOL 
	WHERE CD_COMPANY = @P_CD_COMPANY 
	AND NO_TO = @P_NO_TO 

	-- ������ �������
	DELETE FROM TR_EXTO 
	WHERE CD_COMPANY = @P_CD_COMPANY 
	AND NO_TO = @P_NO_TO  
END
GO  


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXTO_INSERT'))
DROP PROCEDURE UP_TR_EXTO_INSERT
GO

/***********************************************************************    
**  System : ����  
**  Sub System : ����  
**  Page  : ������  
**  Desc  : ������ ����
**  ������ : 
************************************************************************/
    
CREATE PROCEDURE UP_TR_EXTO_INSERT  
	@P_NO_TO		NVARCHAR(20),    
	@P_CD_COMPANY   NVARCHAR(7),    
	@P_NO_INV		NVARCHAR(20),    
	@P_CD_BIZAREA   NVARCHAR(7),    
	@P_CD_SALEGRP   NVARCHAR(7),    
	
	@P_NO_EMP		NVARCHAR(10),    
	@P_FG_LC		NCHAR(3),    
	@P_CD_PARTNER   NVARCHAR(7),    
	@P_FG_EXLICENSE NCHAR(3),    
	@P_NO_EXLICENSE NVARCHAR(20),    
	
	@P_DT_LICENSE   NCHAR(8),    
	@P_CD_EXCH		NVARCHAR(3),    
	@P_RT_LICENSE   NUMERIC(15,4),    
	@P_RT_EXCH		NUMERIC(15,4),    
	@P_AM_EX		NUMERIC(17,4),    
	
	@P_AM			NUMERIC(17,4),    
	@P_AM_EXFOB		NUMERIC(17,4),    
	@P_AM_FOB		NUMERIC(17,4),    
	@P_AM_FREIGHT   NUMERIC(17,4),    
	@P_AM_INSUR		NUMERIC(17,4),    
	
	@P_CD_AGENT		NVARCHAR(7),    
	@P_CD_PRODUCT   NVARCHAR(7),    
	@P_CD_EXPORT	NVARCHAR(7),    
	@P_FG_RETURN	NCHAR(3),    
	@P_DT_DECLARE   NCHAR(8),    
	
	@P_DC_DECLARE   NVARCHAR(50),    
	@P_CD_CUSTOMS   NVARCHAR(7),    
	@P_DC_CY		NVARCHAR(50),    
	@P_NO_INSP		NVARCHAR(20),    
	@P_DT_INSP		NCHAR(8),    
	
	@P_NO_QUAR		NVARCHAR(20),    
	@P_DT_QUAR		NCHAR(8),    
	@P_TP_PACKING   NVARCHAR(3),    
	@P_CNT_PACKING  NUMERIC(3,0),    
	@P_REMARK1		NVARCHAR(100),    
	
	@P_REMARK2		NVARCHAR(100),    
	@P_REMARK3		NVARCHAR(100),    
	@P_YN_BL		NCHAR(1),    
	@P_ID_INSERT   NVARCHAR(15)    
AS    
BEGIN  
	DECLARE @P_DTS_INSERT VARCHAR(14)    
	SET @P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')    
	    
	INSERT INTO TR_EXTO(NO_TO,			CD_COMPANY,		NO_INV,			CD_BIZAREA,		CD_SALEGRP,		NO_EMP,     
					    FG_LC,			CD_PARTNER,		FG_EXLICENSE,	NO_EXLICENSE,	DT_LICENSE,		CD_EXCH,     
					    RT_LICENSE,		RT_EXCH,		AM_EX,			AM,				AM_EXFOB,		AM_FOB,     
					    AM_FREIGHT,		AM_INSUR,		CD_AGENT,		CD_PRODUCT,		CD_EXPORT,		FG_RETURN,     
					    DT_DECLARE,		DC_DECLARE,		CD_CUSTOMS,		DC_CY,			NO_INSP,		DT_INSP,     
					    NO_QUAR,		DT_QUAR,		TP_PACKING,		CNT_PACKING,	REMARK1,		REMARK2,     
					    REMARK3,		YN_BL,			DTS_INSERT,		ID_INSERT)    
	
	VALUES(@P_NO_TO,		@P_CD_COMPANY,  @P_NO_INV,			@P_CD_BIZAREA,		@P_CD_SALEGRP,  @P_NO_EMP,     
		   @P_FG_LC,		@P_CD_PARTNER,  @P_FG_EXLICENSE,	@P_NO_EXLICENSE,	@P_DT_LICENSE,  @P_CD_EXCH,     
		   @P_RT_LICENSE,	@P_RT_EXCH,		@P_AM_EX,			FLOOR(@P_AM),		@P_AM_EXFOB,	@P_AM_FOB,     
		   @P_AM_FREIGHT,	@P_AM_INSUR,	@P_CD_AGENT,		@P_CD_PRODUCT,		@P_CD_EXPORT,	@P_FG_RETURN,     
		   @P_DT_DECLARE,	@P_DC_DECLARE,  @P_CD_CUSTOMS,		@P_DC_CY,			@P_NO_INSP,		@P_DT_INSP,     
		   @P_NO_QUAR,		@P_DT_QUAR,		@P_TP_PACKING,		@P_CNT_PACKING,		@P_REMARK1,		@P_REMARK2,     
		   @P_REMARK3,		@P_YN_BL,		@P_DTS_INSERT,		@P_ID_INSERT)    
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXTO_UPDATE'))
DROP PROCEDURE UP_TR_EXTO_UPDATE
GO

/***********************************************************************    
**  System : ����  
**  Sub System : ����  
**  Page  : ������  
**  Desc  : ������ ����
**  ������ : 
************************************************************************/
  
CREATE PROCEDURE UP_TR_EXTO_UPDATE  
	@P_NO_TO		NVARCHAR(20),    
	@P_CD_COMPANY   NVARCHAR(7),    
	@P_NO_INV		NVARCHAR(20),    
	@P_CD_BIZAREA   NVARCHAR(7),    
	@P_CD_SALEGRP   NVARCHAR(7),    
	
	@P_NO_EMP		NVARCHAR(10),    
	@P_FG_LC		NCHAR(3),    
	@P_CD_PARTNER   NVARCHAR(7),    
	@P_FG_EXLICENSE NCHAR(3),    
	@P_NO_EXLICENSE NVARCHAR(20),    
	
	@P_DT_LICENSE   NCHAR(8),    
	@P_CD_EXCH		NVARCHAR(3),    
	@P_RT_LICENSE   NUMERIC(15,4),    
	@P_RT_EXCH		NUMERIC(15,4),    
	@P_AM_EX		NUMERIC(17,4),    
	
	@P_AM			NUMERIC(17,4),    
	@P_AM_EXFOB		NUMERIC(17,4),    
	@P_AM_FOB		NUMERIC(17,4),    
	@P_AM_FREIGHT   NUMERIC(17,4),    
	@P_AM_INSUR		NUMERIC(17,4),    
	
	@P_CD_AGENT		NVARCHAR(7),    
	@P_CD_PRODUCT   NVARCHAR(7),    
	@P_CD_EXPORT	NVARCHAR(7),    
	@P_FG_RETURN	NCHAR(3),    
	@P_DT_DECLARE   NCHAR(8),    
	
	@P_DC_DECLARE   NVARCHAR(50),    
	@P_CD_CUSTOMS   NVARCHAR(7),    
	@P_DC_CY		NVARCHAR(50),    
	@P_NO_INSP		NVARCHAR(20),    
	@P_DT_INSP		NCHAR(8),    
	
	@P_NO_QUAR		NVARCHAR(20),    
	@P_DT_QUAR		NCHAR(8),    
	@P_TP_PACKING   NVARCHAR(3),    
	@P_CNT_PACKING  NUMERIC(3,0),    
	@P_REMARK1		NVARCHAR(100),    
	
	@P_REMARK2		NVARCHAR(100),    
	@P_REMARK3		NVARCHAR(100),    
	@P_YN_BL		NCHAR(1),    
	@P_ID_UPDATE	NVARCHAR(15)    
AS    
BEGIN   
	DECLARE @P_DTS_UPDATE VARCHAR(14)    
	SET @P_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')    

	UPDATE TR_EXTO 
	SET     
		NO_INV			= @P_NO_INV,  
		CD_BIZAREA		= @P_CD_BIZAREA,  
		CD_SALEGRP		= @P_CD_SALEGRP,     
		NO_EMP			= @P_NO_EMP,  
		FG_LC			= @P_FG_LC,  
		CD_PARTNER		= @P_CD_PARTNER,     
		FG_EXLICENSE    = @P_FG_EXLICENSE,  
		NO_EXLICENSE    = @P_NO_EXLICENSE,  
		DT_LICENSE		= @P_DT_LICENSE,     
		CD_EXCH			= @P_CD_EXCH,  
		RT_LICENSE		= @P_RT_LICENSE,  
		RT_EXCH			= @P_RT_EXCH,     
		AM_EX			= @P_AM_EX,  
		AM				= FLOOR(@P_AM),  
		AM_EXFOB		= @P_AM_EXFOB,     
		AM_FOB			= @P_AM_FOB,  
		AM_FREIGHT		= @P_AM_FREIGHT,  
		AM_INSUR		= @P_AM_INSUR,     
		CD_AGENT		= @P_CD_AGENT,  
		CD_PRODUCT		= @P_CD_PRODUCT,  
		CD_EXPORT		= @P_CD_EXPORT,     
		FG_RETURN		= @P_FG_RETURN,  
		DT_DECLARE		= @P_DT_DECLARE,  
		DC_DECLARE		= @P_DC_DECLARE,     
		CD_CUSTOMS		= @P_CD_CUSTOMS,  
		DC_CY			= @P_DC_CY,  
		NO_INSP			= @P_NO_INSP,     
		DT_INSP			= @P_DT_INSP,  
		NO_QUAR			= @P_NO_QUAR,  
		DT_QUAR			= @P_DT_QUAR,     
		TP_PACKING		= @P_TP_PACKING,  
		CNT_PACKING		= @P_CNT_PACKING,  
		REMARK1			= @P_REMARK1,     
		REMARK2			= @P_REMARK2,  
		REMARK3			= @P_REMARK3,  
		YN_BL			= @P_YN_BL,     
		DTS_UPDATE		= @P_DTS_UPDATE,  
		ID_UPDATE		= @P_ID_UPDATE    
	WHERE NO_TO = @P_NO_TO    
	AND CD_COMPANY = @P_CD_COMPANY        
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXTO_LINE_INSERT'))
DROP PROCEDURE UP_TR_EXTO_LINE_INSERT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXTO_LINE_INSERT
 * ����������: ���� >> ������ >> ������ ��������
 * ��  ��  ��: ������
 * EXEC UP_TR_EXTO_LINE_INSERT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXTO_LINE_INSERT
	@CD_COMPANY		NVARCHAR(7),	--ȸ���ڵ�
	@NO_TO			NVARCHAR(20),	--�����ȣ
	@NO_INV			NVARCHAR(20),	--�����ȣ
	@NO_BL			NVARCHAR(20),	--������ȣ
	@DT_DECLARE		NCHAR(8),		--�Ű���
	@DTS_INSERT		NVARCHAR(14),	--�����
	@ID_INSERT		NVARCHAR(15),	--�����
	@DTS_UPDATE		NVARCHAR(14),	--������
	@ID_UPDATE		NVARCHAR(15)	--������
AS
BEGIN
	IF NOT EXISTS(SELECT TOP 1 NO_TO FROM TR_EXTOL WHERE CD_COMPANY = @CD_COMPANY AND NO_TO = @NO_TO AND NO_INV = @NO_INV)
	BEGIN
		INSERT INTO TR_EXTOL(NO_TO, CD_COMPANY, NO_INV, NO_BL, DT_DECLARE, DTS_INSERT, ID_INSERT)
		VALUES(@NO_TO, @CD_COMPANY, @NO_INV, @NO_BL, @DT_DECLARE, @DTS_INSERT, @ID_INSERT)
	END
	
	ELSE
	BEGIN
		UPDATE TR_EXTOL
		SET
			NO_BL = @NO_BL,
			DT_DECLARE = @DT_DECLARE,
			DTS_UPDATE = @DTS_UPDATE,
			ID_UPDATE = @ID_UPDATE
		WHERE CD_COMPANY = @CD_COMPANY
		AND NO_TO = @NO_TO
		AND NO_INV = @NO_INV
	END
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXTO_COSTBF_SELECT'))
DROP PROCEDURE UP_TR_EXTO_COSTBF_SELECT
GO 

/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXTO_COSTBF_SELECT
 * ����������: 
 * ��      ��: �Ǹź���� ��ư Ŭ���� �ѱ� ���ڰ��� �����´�.
 * ��  ��  ��: 
 * EXEC UP_TR_EXTO_COSTBF_SELECT '',''
 *********************************************************************************************************/
 
CREATE PROCEDURE UP_TR_EXTO_COSTBF_SELECT  
	@P_CD_COMPANY	NVARCHAR(7),  
	@P_NO_TO		NVARCHAR(20)  
AS      
BEGIN
	SELECT EXTO.DT_LICENSE, 
		EXTO.CD_BIZAREA,
		(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE EXTO.CD_COMPANY = CD_COMPANY AND EXTO.CD_BIZAREA = CD_BIZAREA) AS NM_BIZAREA,
		EXTO.NO_EMP,
		EMP.NM_KOR AS NM_EMP,
		EMP.CD_DEPT,
		(SELECT NM_DEPT FROM MA_DEPT WHERE EXTO.CD_COMPANY = CD_COMPANY AND EMP.CD_DEPT = CD_DEPT) AS NM_DEPT,
		SAL.CD_CC,
		(SELECT NM_CC FROM MA_CC WHERE EXTO.CD_COMPANY = CD_COMPANY AND SAL.CD_CC = CD_CC) AS NM_CC
	FROM TR_EXTO EXTO
	LEFT OUTER JOIN MA_EMP EMP ON EXTO.CD_COMPANY = EMP.CD_COMPANY AND EXTO.NO_EMP = EMP.NO_EMP
	LEFT OUTER JOIN MA_SALEGRP SAL ON EXTO.CD_COMPANY = SAL.CD_COMPANY AND EXTO.CD_SALEGRP = SAL.CD_SALEGRP
	WHERE EXTO.CD_COMPANY = @P_CD_COMPANY
	AND EXTO.NO_TO = @P_NO_TO
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXTO_INVOICE_SELECT'))
DROP PROCEDURE UP_TR_EXTO_INVOICE_SELECT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXTO_INVOICE_SELECT
 * ����������: 
 * ��      ��: 
 * ��  ��  ��: 
 * EXEC UP_TR_EXTO_INVOICE_SELECT '',''
 *********************************************************************************************************/
 
CREATE PROCEDURE UP_TR_EXTO_INVOICE_SELECT  
	@P_CD_COMPANY	NVARCHAR(7),  
	@P_NO_INV		NVARCHAR(1000)  
AS      
BEGIN
	SELECT 
		A.NO_INV,    		A.DT_BALLOT,    	A.CD_BIZAREA,    	A.CD_SALEGRP,  		A.NO_EMP,   
		A.FG_LC,    		A.CD_PARTNER,    	A.CD_EXCH,    		A.AM_EX,    		A.DT_LOADING,    
		A.CD_ORIGIN,   		A.CD_AGENT,    		A.CD_EXPORT,    	A.CD_PRODUCT,    	A.SHIP_CORP,    
		A.NM_VESSEL,    	A.COND_TRANS,   	A.TP_TRANSPORT,  	A.TP_TRANS,    		A.TP_PACKING,    
		A.CD_WEIGHT,    	A.GROSS_WEIGHT,    	A.NET_WEIGHT,   	A.PORT_LOADING,    	A.PORT_ARRIVER,    
		A.DESTINATION,    	A.NO_SCT,    		A.NO_ECT,    		A.CD_CUST_IN,   	A.DT_TO,    
		A.NO_LC,    		A.NO_SO,    		A.REMARK1,    		A.REMARK2,    		A.REMARK3,   
		A.REMARK4,    		A.REMARK5,  
		(SELECT NM_KOR FROM MA_EMP WHERE CD_COMPANY = A.CD_COMPANY AND NO_EMP = A.NO_EMP) NM_KOR,    
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) NM_PARTNER,  
		(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = A.CD_SALEGRP) NM_SALEGRP,  
		(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = A.CD_COMPANY AND CD_BIZAREA = A.CD_BIZAREA) NM_BIZAREA,  
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_AGENT) NM_AGENT,  
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_EXPORT) NM_EXPORT,  
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PRODUCT) NM_PRODUCT,  
		A.NO_TO 
	FROM TR_INVH A
	WHERE A.CD_COMPANY = @P_CD_COMPANY
	AND A.NO_INV IN (SELECT * FROM TF_GETSPLIT(@P_NO_INV))  
END
GO