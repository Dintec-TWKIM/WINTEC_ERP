IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_IMCOSTEX_SELECT'))
DROP PROCEDURE UP_TR_IMCOSTEX_SELECT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_IMCOSTEX_SELECT
 * ����������: 
 * ��  ��  ��: 
 * EXEC UP_TR_IMCOSTEX_SELECT 'LCT080212_3','0327'
 *********************************************************************************************************/   
CREATE PROC UP_TR_IMCOSTEX_SELECT  
	@NO_COST  NVARCHAR(20),    
	@CD_COMPANY  NVARCHAR(7)    
AS    
BEGIN    
	-- CC_TR_IMCOST001 (�δ��� Head ��ȸ)    
	SELECT NO_COST,    
		FG_STEP,     
		NO_LC,    
		NO_HST,    
		NO_BL,    
		NO_TO,     
		CD_DEPT,    
		NO_EMP,    
		CD_BIZAREA,     
		DT_BALLOT,    
		CD_PARTNER,
		CD_CC,     
		YN_SLIP,     
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = A.CD_PARTNER AND CD_COMPANY = A.CD_COMPANY) LN_PARTNER,
		(SELECT NM_CC FROM MA_CC WHERE CD_CC = A.CD_CC AND CD_COMPANY = A.CD_COMPANY) NM_CC,    
		(SELECT NM_DEPT FROM MA_DEPT WHERE CD_DEPT = A.CD_DEPT AND CD_COMPANY = A.CD_COMPANY) NM_DEPT,    
		(SELECT NM_KOR FROM MA_EMP WHERE NO_EMP = A.NO_EMP AND CD_COMPANY = A.CD_COMPANY) NM_KOR    
	FROM TR_IMCOSTH A    
	WHERE NO_COST = @NO_COST    
	AND CD_COMPANY =@CD_COMPANY    
      
	-- CC_TR_IMCOST002  (�δ��� Line ��ȸ)    
	SELECT     
		NO_COST,     
		NO_LINE,     
		CD_COST,    
		AM_COST,     
		FG_TAX AS FG_TAX,     
		VAT,    
		RT_VAT,     
		CD_EXCH,    
		AM_EX,     
		CD_PARTNER,
		AM_DISTRIBUT,     
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = A.CD_PARTNER AND CD_COMPANY = A.CD_COMPANY) LN_PARTNER,    
		(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'TR_IM00007' AND CD_SYSDEF = A.CD_COST AND CD_COMPANY = A.CD_COMPANY) NM_COST    
	FROM TR_IMCOSTL A    
	WHERE A.NO_COST = @NO_COST    
	AND A.CD_COMPANY = @CD_COMPANY    
	ORDER BY A.NO_LINE    
     
	-- CC_TR_DISTRIBU028 (�δ��뿡�� �������꿩�� üũ)    
	SELECT COUNT(1)  AS DIS_CNT   
	FROM TR_DISTRIBU    
	WHERE NO_COST = @NO_COST    
	AND CD_COMPANY = @CD_COMPANY    
  
	-- �δ��뿡�� ��ǥó������ üũ)    

	SELECT COUNT(1) AS JUNPYO_CNT    
	FROM TR_IMCOSTH A   
	WHERE A.YN_SLIP = 'Y' AND A.CD_COMPANY = @CD_COMPANY AND A.NO_COST = @NO_COST  
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_IMCOSTHEX_DELETE'))
DROP PROCEDURE UP_TR_IMCOSTHEX_DELETE
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_IMCOSTHEX_DELETE
 * ����������: 
 * ��  ��  ��: 
 * EXEC UP_TR_IMCOSTHEX_DELETE '',''
 *********************************************************************************************************/   
CREATE PROC UP_TR_IMCOSTHEX_DELETE  
(  
	@CD_COMPANY  NVARCHAR(7),   
	@NO_COST  NVARCHAR(20)  
)  
AS  
BEGIN  
  
	DECLARE @ERRNO    INT,    
			@ERRMSG   VARCHAR(255),  
			@FG_STEP  NVARCHAR(7),  
			@NO_LC    NVARCHAR(20),  
			@NO_BL    NVARCHAR(20),  
			@NO_TO    NVARCHAR(20),  
			@NO_HST   NUMERIC(3)  
 
	DELETE TR_IMCOSTL 
	WHERE NO_COST = @NO_COST 
	AND CD_COMPANY = @CD_COMPANY    
	
	IF (@@ERROR <> 0 )  
	BEGIN    
	   SELECT @ERRNO  = 100000,    
					@ERRMSG = 'CM_M100010'    
	   GOTO ERROR    
	END   
  
	DELETE TR_IMCOSTH 
	WHERE NO_COST = @NO_COST 
	AND CD_COMPANY = @CD_COMPANY    
	
	IF (@@ERROR <> 0 )  
	BEGIN    
	   SELECT @ERRNO  = 100000,    
					@ERRMSG = 'CM_M100010'    
	   GOTO ERROR    
	END   
  
	RETURN  
	ERROR:    
		RAISERROR @ERRNO @ERRMSG    
END
GO  


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_IMCOSTHEX_INSERT'))
DROP PROCEDURE UP_TR_IMCOSTHEX_INSERT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_IMCOSTHEX_INSERT
 * ����������: 
 * ��  ��  ��: 
 * EXEC UP_TR_IMCOSTHEX_INSERT '',''
 *********************************************************************************************************/ 
CREATE PROCEDURE UP_TR_IMCOSTHEX_INSERT  
	@NO_COST		NVARCHAR(20),   
	@CD_COMPANY		NVARCHAR(7),    
	@FG_STEP		NCHAR(2),   
	@NO_LC			NVARCHAR(20),   
	@NO_HST			NUMERIC(3),   
	@NO_BL			NVARCHAR(20),   
	@NO_TO			NVARCHAR(20),   
	@CD_DEPT		NVARCHAR(12),   
	@NO_EMP			NVARCHAR(10),   
	@CD_BIZAREA		NVARCHAR(7),    
	@DT_BALLOT		NCHAR(8),   
	@CD_PARTNER		NVARCHAR(7),   
	@CD_CC			NVARCHAR(7)  
AS  
BEGIN
	IF NOT EXISTS(SELECT TOP 1 NO_COST FROM TR_IMCOSTH WHERE CD_COMPANY = @CD_COMPANY AND NO_COST= @NO_COST)
	BEGIN    
		INSERT INTO TR_IMCOSTH (NO_COST,	CD_COMPANY, FG_STEP,	NO_LC,		NO_HST, 
								NO_BL,		NO_TO,		CD_DEPT,	NO_EMP,		CD_BIZAREA, 
								DT_BALLOT,	CD_PARTNER, CD_CC,		YN_SLIP )  
		VALUES( @NO_COST,	@CD_COMPANY,	@FG_STEP,	@NO_LC,		@NO_HST, 
				@NO_BL,		@NO_TO,			@CD_DEPT,	@NO_EMP,	@CD_BIZAREA, 
				@DT_BALLOT, @CD_PARTNER,	@CD_CC,		'N')  
	END

	ELSE
	BEGIN
		UPDATE TR_IMCOSTH  
		SET 
			CD_DEPT = @CD_DEPT, 
			NO_EMP = @NO_EMP , 
			CD_BIZAREA = @CD_BIZAREA, 
			DT_BALLOT = @DT_BALLOT,
			CD_CC = @CD_CC  
		WHERE NO_COST = @NO_COST 
		AND CD_COMPANY = @CD_COMPANY  
	END
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_IMCOSTLEX_INSERT'))
DROP PROCEDURE UP_TR_IMCOSTLEX_INSERT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_IMCOSTLEX_INSERT
 * ����������: 
 * ��  ��  ��: 
 * EXEC UP_TR_IMCOSTLEX_INSERT '',''
 *********************************************************************************************************/   
CREATE PROC UP_TR_IMCOSTLEX_INSERT  
	@NO_COST		NVARCHAR(20),   
	@CD_COMPANY		NVARCHAR(20),   
	@NO_LINE		NUMERIC(3),   
	@CD_COST		NVARCHAR(3),   
	@AM_COST		NUMERIC(17,4),   
	@FG_TAX			NVARCHAR(3),    
	@VAT			NUMERIC(17,4),   
	@RT_VAT			NUMERIC(5,2),   
	@CD_EXCH		NVARCHAR(20),   
	@AM_EX			NUMERIC(17,4),   
	@CD_PARTNER		NVARCHAR(7),    
	@AM_DISTRIBUT	NUMERIC(17,4)   
AS  
BEGIN 
	IF NOT EXISTS(SELECT TOP 1 NO_COST FROM TR_IMCOSTL WHERE CD_COMPANY = @CD_COMPANY AND NO_COST = @NO_COST AND NO_LINE = @NO_LINE)
	BEGIN
		INSERT INTO TR_IMCOSTL (NO_COST,	CD_COMPANY,		NO_LINE,	CD_COST,	AM_COST, 
								FG_TAX,		VAT,			RT_VAT,		CD_EXCH,	AM_EX, 		
								CD_PARTNER, AM_DISTRIBUT)  
		VALUES (@NO_COST,		@CD_COMPANY,	@NO_LINE,	@CD_COST, @AM_COST, 
				@FG_TAX,		@VAT,			@RT_VAT,	@CD_EXCH, @AM_EX, 
				@CD_PARTNER,	@AM_DISTRIBUT)
	END

	ELSE
	BEGIN
		UPDATE TR_IMCOSTL  
		SET  
			CD_COST= @CD_COST, 
			AM_COST= @AM_COST,  
			FG_TAX= @FG_TAX,  
			VAT = @VAT,  
			RT_VAT = @RT_VAT,  
			CD_EXCH = @CD_EXCH,  
			AM_EX = @AM_EX,  
			CD_PARTNER = @CD_PARTNER,  
			AM_DISTRIBUT =@AM_DISTRIBUT  
		WHERE CD_COMPANY = @CD_COMPANY  
		AND NO_COST = @NO_COST   
		AND NO_LINE = @NO_LINE  
	END
END
GO 


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_IMCOSTLEX_DELETE'))
DROP PROCEDURE UP_TR_IMCOSTLEX_DELETE
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_IMCOSTLEX_DELETE
 * ����������: 
 * ��  ��  ��: 
 * EXEC UP_TR_IMCOSTLEX_DELETE '',''
 *********************************************************************************************************/ 
CREATE PROC UP_TR_IMCOSTLEX_DELETE  
	@CD_COMPANY  NVARCHAR(7),   
	@NO_COST  NVARCHAR(20),     
	@NO_LINE  NUMERIC(3)  
AS  
BEGIN  
	DELETE TR_IMCOSTL 
	WHERE NO_COST = @NO_COST 
	AND CD_COMPANY = @CD_COMPANY 
	AND NO_LINE = @NO_LINE  
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_IMCOSTEX_DOCU'))
DROP PROCEDURE UP_TR_IMCOSTEX_DOCU
GO 
/**********************************************************************************************************    
**  System : ��������        
**  Sub System : ��������        
**  Page  : �δ�����        
**  Desc  : �δ����� �̰���ǥó��        
**  ��  �� :         
**  Return Values        
**        
**  ��    ��    ��  : ������, ���強        
**  ��    ��    ��  : 2007.10.16        
**  ��    ��    ��     :        
**  ��    ��    ��   �� :    
 *********************************************************************************************************/       
CREATE PROCEDURE UP_TR_IMCOSTEX_DOCU  
	@P_CD_COMPANY   NVARCHAR(7),         
	@P_NO_COST		  NVARCHAR(20),
	@P_PROCESS		  NCHAR(1)       
AS        
BEGIN      
	DECLARE @CD_COMPANY  NVARCHAR(7)        
	DECLARE @CD_MNG   NVARCHAR(20)        
	DECLARE @CD_BIZAREA  NVARCHAR(7)        
	DECLARE @NM_BIZAREA  NVARCHAR(15)      
	DECLARE @CD_WDEPT  NVARCHAR(12)        
	DECLARE @CD_PARTNER  NVARCHAR(7)        
	DECLARE @NM_PARTNER  NVARCHAR(20)      
	DECLARE @TP_TAX   NCHAR(2)        
	DECLARE @NM_TAX   NCHAR(15)      
	DECLARE @ID_WRITE  NVARCHAR(10)        
	DECLARE @DT_ACCT  NCHAR(8)        
	DECLARE @DT_START  NCHAR(8)        
	DECLARE @DT_WRITE  NCHAR(8)        
	DECLARE @AM_DR   NUMERIC(19,4)        
	DECLARE @AM_CR   NUMERIC(19,4)       
	DECLARE @AM_SUPPLY  NUMERIC(19,4)       
	DECLARE @VAT_AM   NUMERIC(19,4) /* �ΰ��� ���� üũ�ϱ� ���� ���� */      
	DECLARE @DT_TAX   NCHAR(8)        
	DECLARE @CD_CC   NVARCHAR(12)       
	DECLARE @NM_CC   NVARCHAR(20)       
	DECLARE @CD_EMPLOY  NVARCHAR(10)        
	DECLARE @CD_DEPT  NVARCHAR(12)        
	DECLARE @CD_ACCT  NVARCHAR(10)        
	DECLARE @CD_PC   NVARCHAR(7)        
	DECLARE @TP_DRCR  NVARCHAR(1)        
	DECLARE @CD_RELATION NCHAR(2)  --�ΰ��� �����׸�(31)����      
	DECLARE @NO_LC   NVARCHAR(20)    
	DECLARE @NO_BL   NVARCHAR(20)  
	DECLARE @TP_ACAREA	NVARCHAR(20)  
	DECLARE @NO_BIZAREA  NVARCHAR(20)      
	DECLARE @P_ERRORCODE NCHAR(10)      
	DECLARE @P_ERRORMSG  NVARCHAR(300)       
      
  IF(@P_PROCESS = '0')
  BEGIN 
	DECLARE IMCOST_CURSOR CURSOR FOR        
  
      /******************* */      
	  /* ���� : �δ��� �ݾ� */      
	  /******************* */      
	 SELECT DISTINCT TIH.CD_COMPANY,         
	  MB.CD_PC,      
	  TIH.CD_DEPT AS CD_WDEPT,        
	  TIH.NO_EMP AS ID_WRITE,          
	  TIH.DT_BALLOT AS DT_ACCT,       
	  '1' TP_DRCR, -- ���뱸�� 1: ����        
	  TC.CD_ACIMUNARRIVAL AS CD_ACCT,       
	  SUM( TIL.AM_COST ) AM_DR,        
	  0 AM_CR,        
	  0 AM_SUPPLY,      
	  '10' CD_RELATION,       
	  TIL.FG_TAX AS TP_TAX,         
	  TIH.CD_BIZAREA,      
	  TIH.CD_CC AS CD_CC,      
	  TIH.CD_DEPT,        
	  TIH.NO_EMP AS CD_EMPLOY,         
	  TIL.CD_PARTNER,         
	  TIH.DT_BALLOT AS DT_START,      
	  TIH.DT_BALLOT AS DT_WRITE,      
	  (SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_BIZAREA = TIH.CD_BIZAREA) NM_BIZAREA,      
	  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_SYSDEF = TIL.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,      
	  (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_PARTNER = TIL.CD_PARTNER) NM_PARTNER,    
	  TIH.NO_LC, TIH.NO_BL, MB.NO_BIZAREA
	  ,(CASE G.TP_DRCR WHEN '1' THEN (CASE G.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA
	 FROM TR_IMCOSTH AS TIH       
	 INNER JOIN TR_IMCOSTL AS TIL ON TIH.NO_COST = TIL.NO_COST AND TIH.CD_COMPANY = TIL.CD_COMPANY      
	 INNER JOIN MA_BIZAREA AS MB ON  TIH.CD_BIZAREA = MB.CD_BIZAREA  AND TIH.CD_COMPANY = MB.CD_COMPANY        
	 INNER JOIN TR_CONTROL AS TC ON TC.CD_COMPANY = TIH.CD_COMPANY       
	 INNER JOIN FI_ACCTCODE G ON G.CD_ACCT = TC.CD_ACIMUNARRIVAL AND G.CD_COMPANY = TC.CD_COMPANY 
	 WHERE TIL.NO_COST = @P_NO_COST AND      
	 TIH.CD_COMPANY = @P_CD_COMPANY AND TIH.YN_SLIP = 'N' -- ��ǥó������        
	 GROUP BY TIH.CD_COMPANY,  MB.CD_PC,  TIH.CD_DEPT, TIH.NO_EMP,  TIH.DT_BALLOT,   
		TC.CD_ACIMUNARRIVAL, TIL.FG_TAX,  TIH.CD_BIZAREA, TIH.CD_CC,  TIH.NO_EMP,   
		TIL.CD_PARTNER,  TIH.DT_BALLOT, TIH.NO_COST, TIH.DT_BALLOT,  TIH.NO_LC,   
		TIH.NO_BL,    MB.NO_BIZAREA, G.TP_DRCR, G.YN_BAN
	       
	 UNION ALL        
	     
	 /******************* */      
	 /* ���� : �ΰ��� (�Ǻ�) */      
	 /******************* */      
	 SELECT TIH.CD_COMPANY,         
	  MB.CD_PC,      
	  TIH.CD_DEPT AS CD_WDEPT,        
	  TIH.NO_EMP AS ID_WRITE,          
	  TIH.DT_BALLOT AS DT_ACCT,       
	  '1' TP_DRCR, -- ���뱸�� 1: ����        
	  AISPOSTL.CD_ACCT AS CD_ACCT,      
	  --SUM( TIL.VAT ) AM_DR,        
	  TIL.VAT AS AM_DR,        
	  0.0000 AM_CR,        
	  SUM(AM_COST) AM_SUPPLY,    --���ް���(20071010)      
	  '30' CD_RELATION,       
	  TIL.FG_TAX AS TP_TAX,         
	  TIH.CD_BIZAREA,      
	  TIH.CD_CC AS CD_CC,      
	  TIH.CD_DEPT,        
	  TIH.NO_EMP AS CD_EMPLOY,         
	  TIL.CD_PARTNER,         
	  TIH.DT_BALLOT AS DT_START,      
	  TIH.DT_BALLOT AS DT_WRITE,      
	  (SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_BIZAREA = TIH.CD_BIZAREA) NM_BIZAREA,      
	  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_SYSDEF = TIL.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,      
	  (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_PARTNER = TIL.CD_PARTNER) NM_PARTNER,    
	  TIH.NO_LC, TIH.NO_BL, MB.NO_BIZAREA,'0' TP_ACAREA 
	 FROM TR_IMCOSTH AS TIH       
	 INNER JOIN TR_IMCOSTL AS TIL ON TIH.NO_COST = TIL.NO_COST AND TIH.CD_COMPANY = TIL.CD_COMPANY      
	 INNER JOIN MA_BIZAREA AS MB ON  TIH.CD_BIZAREA = MB.CD_BIZAREA  AND TIH.CD_COMPANY = MB.CD_COMPANY        
	 INNER JOIN TR_CONTROL AS TC ON TC.CD_COMPANY = TIH.CD_COMPANY   
	 INNER JOIN MA_AISPOSTL AS AISPOSTL ON AISPOSTL.CD_COMPANY = TIH.CD_COMPANY AND AISPOSTL.CD_TP='001' AND FG_TP='200' AND FG_AIS='210'      
	 WHERE TIL.NO_COST = @P_NO_COST   
	 AND TIH.CD_COMPANY = @P_CD_COMPANY   
	 AND  TIH.YN_SLIP = 'N' -- ��ǥó������  
	 AND TIL.FG_TAX <> '' OR TIL.FG_TAX <> NULL        
	 GROUP BY TIH.CD_COMPANY, MB.CD_PC,  TIH.CD_DEPT, TIH.NO_EMP,  TIH.DT_BALLOT,    
		TIL.VAT,   TIL.FG_TAX,  TIH.CD_BIZAREA, TIH.CD_DEPT, TIH.CD_CC,        
		TIH.NO_EMP,  TIL.CD_PARTNER, TIH.DT_BALLOT, TIH.NO_COST, TIH.DT_BALLOT,    
		TIH.NO_LC,   TIH.NO_BL,  MB.NO_BIZAREA, AISPOSTL.CD_ACCT, TIL.NO_LINE
	 UNION ALL      
	       
	 /*********************************************************** */      
	 /* �뺯 : �հ�(�δ���ݾ� + �ΰ���(�Ǻ�)) -- ���õ�ϴ��� ó�� ����    */      
	 /*********************************************************** */      
	 SELECT DISTINCT TIH.CD_COMPANY,         
	  MB.CD_PC,      
	  TIH.CD_DEPT AS CD_WDEPT,        
	  TIH.NO_EMP AS ID_WRITE,          
	  TIH.DT_BALLOT AS DT_ACCT,       
	  '2' TP_DRCR, -- ���뱸�� 2: �뺯        
	  (CASE TIH.FG_STEP WHEN 'LC' THEN TC.CD_ACIMLC
		WHEN 'AM' THEN TC.CD_ACIMLC
		WHEN 'BL' THEN TC.CD_ACIMBL       
		WHEN 'TO' THEN TC.CD_ACIMTO
		WHEN 'ZZ' THEN TC.CD_ACIMCOST      
	  END) AS CD_ACCT,       
	  0.0000 AM_DR,        
	  SUM(TIL.AM_COST + TIL.VAT) AM_CR,        
	  0 AM_SUPPLY,      
	  '10' CD_RELATION,       
	  TIL.FG_TAX AS TP_TAX,         
	  TIH.CD_BIZAREA,      
	  TIH.CD_CC AS CD_CC,      
	  TIH.CD_DEPT,        
	  TIH.NO_EMP AS CD_EMPLOY,         
	  TIL.CD_PARTNER,         
	  TIH.DT_BALLOT AS DT_START,      
	  TIH.DT_BALLOT AS DT_WRITE,      
	  (SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_BIZAREA = TIH.CD_BIZAREA) NM_BIZAREA,      
	  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_SYSDEF = TIL.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,      
	  (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_PARTNER = TIL.CD_PARTNER) NM_PARTNER,    
	  TIH.NO_LC, TIH.NO_BL, MB.NO_BIZAREA
	  ,(CASE G.TP_DRCR WHEN '2' THEN (CASE G.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA
	 FROM TR_IMCOSTH AS TIH       
	 INNER JOIN TR_IMCOSTL AS TIL ON TIH.NO_COST = TIL.NO_COST AND TIH.CD_COMPANY = TIL.CD_COMPANY      
	 INNER JOIN MA_BIZAREA AS MB ON  TIH.CD_BIZAREA = MB.CD_BIZAREA  AND TIH.CD_COMPANY = MB.CD_COMPANY        
	 INNER JOIN TR_CONTROL AS TC ON TC.CD_COMPANY = TIH.CD_COMPANY
	 INNER JOIN FI_ACCTCODE G ON G.CD_ACCT = TC.CD_ACIMUNARRIVAL AND G.CD_COMPANY = TC.CD_COMPANY        
	 WHERE TIL.NO_COST = @P_NO_COST   
	 AND TIH.CD_COMPANY = @P_CD_COMPANY   
	 AND  TIH.YN_SLIP = 'N' -- ��ǥó������        
	 GROUP BY TIH.CD_COMPANY, MB.CD_PC, TIH.CD_DEPT, TIH.NO_EMP, TIH.DT_BALLOT, TIH.FG_STEP, TC.CD_ACIMLC, TC.CD_ACIMBL, TC.CD_ACIMTO,  TIL.FG_TAX, TIH.CD_BIZAREA, TIH.CD_DEPT, TIH.CD_CC,        
	 TIH.NO_EMP, TIL.CD_PARTNER, TIH.DT_BALLOT, TIH.NO_COST, TIH.DT_BALLOT,  TIH.NO_LC, TIH.NO_BL, MB.NO_BIZAREA, TC.CD_ACIMCOST, G.TP_DRCR, G.YN_BAN  
  END
  
  ELSE IF(@P_PROCESS = '1')
	
  BEGIN
	DECLARE IMCOST_CURSOR CURSOR FOR 

      /******************* */      
	  /* ���� : �δ��� �ݾ� (�պ�) */      
	  /******************* */      
	 SELECT DISTINCT TIH.CD_COMPANY,         
	  MB.CD_PC,      
	  TIH.CD_DEPT AS CD_WDEPT,        
	  TIH.NO_EMP AS ID_WRITE,          
	  TIH.DT_BALLOT AS DT_ACCT,       
	  '1' TP_DRCR, -- ���뱸�� 1: ����        
	  TC.CD_ACIMUNARRIVAL AS CD_ACCT,       
	  SUM( TIL.AM_COST ) AM_DR,        
	  0 AM_CR,        
	  0 AM_SUPPLY,      
	  '10' CD_RELATION,       
	  TIL.FG_TAX AS TP_TAX,         
	  TIH.CD_BIZAREA,      
	  TIH.CD_CC AS CD_CC,      
	  TIH.CD_DEPT,        
	  TIH.NO_EMP AS CD_EMPLOY,         
	  TIL.CD_PARTNER,         
	  TIH.DT_BALLOT AS DT_START,      
	  TIH.DT_BALLOT AS DT_WRITE,      
	  (SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_BIZAREA = TIH.CD_BIZAREA) NM_BIZAREA,      
	  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_SYSDEF = TIL.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,      
	  (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_PARTNER = TIL.CD_PARTNER) NM_PARTNER,    
	  TIH.NO_LC, TIH.NO_BL, MB.NO_BIZAREA
	  ,(CASE G.TP_DRCR WHEN '1' THEN (CASE G.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA    
	 FROM TR_IMCOSTH AS TIH       
	 INNER JOIN TR_IMCOSTL AS TIL ON TIH.NO_COST = TIL.NO_COST AND TIH.CD_COMPANY = TIL.CD_COMPANY      
	 INNER JOIN MA_BIZAREA AS MB ON  TIH.CD_BIZAREA = MB.CD_BIZAREA  AND TIH.CD_COMPANY = MB.CD_COMPANY        
	 INNER JOIN TR_CONTROL AS TC ON TC.CD_COMPANY = TIH.CD_COMPANY
	 INNER JOIN FI_ACCTCODE G ON G.CD_ACCT = TC.CD_ACIMUNARRIVAL AND G.CD_COMPANY = TC.CD_COMPANY       
	 WHERE TIL.NO_COST = @P_NO_COST   AND      
	 TIH.CD_COMPANY = @P_CD_COMPANY  AND TIH.YN_SLIP = 'N' -- ��ǥó������        
	 GROUP BY TIH.CD_COMPANY,  MB.CD_PC,  TIH.CD_DEPT, TIH.NO_EMP,  TIH.DT_BALLOT,   
		TC.CD_ACIMUNARRIVAL, TIL.FG_TAX,  TIH.CD_BIZAREA, TIH.CD_CC,  TIH.NO_EMP,   
		TIL.CD_PARTNER,  TIH.DT_BALLOT, TIH.NO_COST, TIH.DT_BALLOT,  TIH.NO_LC,   
		TIH.NO_BL,    MB.NO_BIZAREA , TIL.NO_LINE,  G.TP_DRCR, G.YN_BAN     
	       
	 UNION ALL        
	 
	 /******************* */      
	 /* ���� : �ΰ��� (�պ�) */      
	 /******************* */ 
	SELECT TIH.CD_COMPANY,         
	  MB.CD_PC,      
	  TIH.CD_DEPT AS CD_WDEPT,        
	  TIH.NO_EMP AS ID_WRITE,          
	  TIH.DT_BALLOT AS DT_ACCT,       
	  '1' TP_DRCR, -- ���뱸�� 1: ����        
	  AISPOSTL.CD_ACCT AS CD_ACCT,      
	  SUM( TIL.VAT ) AM_DR,        
	  --TIL.VAT AS AM_DR,        
	  0.0000 AM_CR,        
	  0 AM_SUPPLY,    --���ް���(20071010)      
	  '30' CD_RELATION,       
	  TIL.FG_TAX AS TP_TAX,         
	  TIH.CD_BIZAREA,      
	  TIH.CD_CC AS CD_CC,      
	  TIH.CD_DEPT,        
	  TIH.NO_EMP AS CD_EMPLOY,         
	  TIL.CD_PARTNER,         
	  TIH.DT_BALLOT AS DT_START,      
	  TIH.DT_BALLOT AS DT_WRITE,      
	  (SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_BIZAREA = TIH.CD_BIZAREA) NM_BIZAREA,      
	  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_SYSDEF = TIL.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,      
	  (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_PARTNER = TIL.CD_PARTNER) NM_PARTNER,    
	  TIH.NO_LC, TIH.NO_BL, MB.NO_BIZAREA,'0' TP_ACAREA 
	 FROM TR_IMCOSTH AS TIH       
	 INNER JOIN TR_IMCOSTL AS TIL ON TIH.NO_COST = TIL.NO_COST AND TIH.CD_COMPANY = TIL.CD_COMPANY      
	 INNER JOIN MA_BIZAREA AS MB ON  TIH.CD_BIZAREA = MB.CD_BIZAREA  AND TIH.CD_COMPANY = MB.CD_COMPANY        
	 INNER JOIN TR_CONTROL AS TC ON TC.CD_COMPANY = TIH.CD_COMPANY   
	 INNER JOIN MA_AISPOSTL AS AISPOSTL ON AISPOSTL.CD_COMPANY = TIH.CD_COMPANY AND AISPOSTL.CD_TP='001' AND FG_TP='200' AND FG_AIS='210'      
	 WHERE TIL.NO_COST = @P_NO_COST   
	 AND TIH.CD_COMPANY =@P_CD_COMPANY 
	 AND  TIH.YN_SLIP = 'N' -- ��ǥó������  
	 AND TIL.FG_TAX <> '' OR TIL.FG_TAX <> NULL        
	 GROUP BY TIH.CD_COMPANY, MB.CD_PC,  TIH.CD_DEPT, TIH.NO_EMP,   
		/*TIL.VAT, */  TIL.FG_TAX,  TIH.CD_BIZAREA, TIH.CD_DEPT, TIH.CD_CC,        
		TIH.NO_EMP  ,  TIL.CD_PARTNER, TIH.DT_BALLOT, TIH.NO_COST,   
		TIH.NO_LC,   TIH.NO_BL,  MB.NO_BIZAREA, AISPOSTL.CD_ACCT /*, TIL.NO_LINE */
	 UNION ALL      
	       
	 /*********************************************************** */      
	 /* �뺯 : �հ�(�δ���ݾ� + �ΰ���(�պ�)) -- ���õ�ϴ��� ó�� ����    */      
	 /*********************************************************** */      
	 SELECT DISTINCT TIH.CD_COMPANY,         
	  MB.CD_PC,      
	  TIH.CD_DEPT AS CD_WDEPT,        
	  TIH.NO_EMP AS ID_WRITE,          
	  TIH.DT_BALLOT AS DT_ACCT,       
	  '2' TP_DRCR, -- ���뱸�� 2: �뺯        
	  (CASE TIH.FG_STEP WHEN 'LC' THEN TC.CD_ACIMLC
		WHEN 'AM' THEN TC.CD_ACIMLC
		WHEN 'BL' THEN TC.CD_ACIMBL       
		WHEN 'TO' THEN TC.CD_ACIMTO
		WHEN 'ZZ' THEN TC.CD_ACIMCOST      
	  END) AS CD_ACCT,       
	  0.0000 AM_DR,        
	  SUM(TIL.AM_COST + TIL.VAT) AM_CR,        
	  0 AM_SUPPLY,      
	  '10' CD_RELATION,       
	  TIL.FG_TAX AS TP_TAX,         
	  TIH.CD_BIZAREA,      
	  TIH.CD_CC AS CD_CC,      
	  TIH.CD_DEPT,        
	  TIH.NO_EMP AS CD_EMPLOY,         
	  TIL.CD_PARTNER,         
	  TIH.DT_BALLOT AS DT_START,      
	  TIH.DT_BALLOT AS DT_WRITE,      
	  (SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_BIZAREA = TIH.CD_BIZAREA) NM_BIZAREA,      
	  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_SYSDEF = TIL.FG_TAX  AND CD_FIELD = 'FI_T000011') NM_TAX,      
	  (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = TIH.CD_COMPANY AND CD_PARTNER = TIL.CD_PARTNER) NM_PARTNER,    
	  TIH.NO_LC, TIH.NO_BL, MB.NO_BIZAREA
	  ,(CASE G.TP_DRCR WHEN '2' THEN (CASE G.YN_BAN WHEN 'Y' THEN '4' ELSE '0' END )ELSE '0' END) TP_ACAREA      
	 FROM TR_IMCOSTH AS TIH       
	 INNER JOIN TR_IMCOSTL AS TIL ON TIH.NO_COST = TIL.NO_COST AND TIH.CD_COMPANY = TIL.CD_COMPANY      
	 INNER JOIN MA_BIZAREA AS MB ON  TIH.CD_BIZAREA = MB.CD_BIZAREA  AND TIH.CD_COMPANY = MB.CD_COMPANY        
	 INNER JOIN TR_CONTROL AS TC ON TC.CD_COMPANY = TIH.CD_COMPANY
	 INNER JOIN FI_ACCTCODE G ON G.CD_ACCT = TC.CD_ACIMUNARRIVAL AND G.CD_COMPANY = TC.CD_COMPANY       
	 WHERE TIL.NO_COST = @P_NO_COST   
	 AND TIH.CD_COMPANY = @P_CD_COMPANY 
	 AND  TIH.YN_SLIP = 'N' -- ��ǥó������        
	 GROUP BY TIH.CD_COMPANY, MB.CD_PC, TIH.CD_DEPT, TIH.NO_EMP, TIH.DT_BALLOT, TIH.FG_STEP, TC.CD_ACIMLC, TC.CD_ACIMBL, TC.CD_ACIMTO,  TIL.FG_TAX, TIH.CD_BIZAREA, TIH.CD_DEPT, TIH.CD_CC,        
	 TIH.NO_EMP, TIL.CD_PARTNER, TIH.DT_BALLOT, TIH.NO_COST, TIH.DT_BALLOT,  TIH.NO_LC, TIH.NO_BL, MB.NO_BIZAREA, TC.CD_ACIMCOST,  G.TP_DRCR, G.YN_BAN
  END

 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------      
  
 -- ���⼭ ���� ��ǥó�� �ϱ� ���� �κ�  ---        
 DECLARE @P_NO_DOCU NVARCHAR(20) -- ��ǥ��ȣ        
 DECLARE @P_NO_TAX NVARCHAR(20) -- �ΰ�����ȣ        
 DECLARE @P_DT_PROCESS NVARCHAR(8)        
       
 -- �������� �˾ƿ���        
 SELECT @P_DT_PROCESS = DT_BALLOT        
 FROM TR_IMCOSTH        
 WHERE CD_COMPANY = @P_CD_COMPANY AND NO_COST = @P_NO_COST       
       
 -- ��ǥ��ȣ ä��        
 EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT        
  
 -- NO_DOLINE ����        
 DECLARE @P_NO_DOLINE NUMERIC(4,0)        
 SET @P_NO_DOLINE = 0        
       
 OPEN IMCOST_CURSOR        
       
 FETCH NEXT FROM IMCOST_CURSOR INTO @CD_COMPANY, @CD_PC, @CD_WDEPT, @ID_WRITE, @DT_ACCT, @TP_DRCR, @CD_ACCT, @AM_DR,  @AM_CR,  @AM_SUPPLY, @CD_RELATION,       
 @TP_TAX,  @CD_BIZAREA, @CD_CC, @CD_DEPT,  @CD_EMPLOY,  @CD_PARTNER,   @DT_START, @DT_WRITE, @NM_BIZAREA, @NM_TAX, @NM_PARTNER,  @NO_LC, @NO_BL, @NO_BIZAREA, @TP_ACAREA     
 WHILE @@FETCH_STATUS = 0        
 BEGIN        
  SET @P_NO_DOLINE = @P_NO_DOLINE + 1        
   -- SET @AM_DR_CR = CONVERT(VARCHAR(40), @AM_CR+@AM_DR)        
   -------------------------------------------------------------------------------------------------------------------------------------------------------------------------      
      
  DECLARE @P_COND NVARCHAR(10)    
  DECLARE @P_COND_BIZAREA NVARCHAR(10)    
      
  SELECT @P_COND = CD_MNG2 FROM FI_ACCTCODE WHERE CD_ACCT = @CD_ACCT AND CD_COMPANY = @CD_COMPANY    
      
  -- �ΰ�����ȣ ä��        
  EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'TX01', @P_DT_PROCESS, @P_NO_TAX OUTPUT        
  
  
  PRINT('�ΰ�����ȣ')  
  PRINT(@P_NO_TAX)  
        
  IF(@CD_RELATION = '30')  /* �ΰ��� ��ǥ ó���� ��� CD_RELATION�� 30 �� ���� �ΰ��������� �δ����ȣ�� CD_MNG(������ȣ)�� �ѱ�� */    
   SET @CD_MNG = @P_NO_TAX    
  ELSE IF (@P_COND = 'B06')     
   SET @CD_MNG = @NO_LC    
  ELSE    
   SET @CD_MNG = NULL    
  
  EXEC UP_FI_AUTODOCU_1       
  @P_NO_DOCU,      -- ��ǥ��ȣ    
  @P_NO_DOLINE,  -- ���ι�ȣ    
  @CD_PC,         -- ȸ�����    
  @CD_COMPANY,    -- ȸ���ڵ�    
  @CD_WDEPT,        -- �ۼ��μ�    
  @ID_WRITE,          -- �ۼ����    
  @P_DT_PROCESS,      
  0,                           -- ȸ���ȣ    
  '3',                         -- ��ǥ����    
  '11',                       -- ��ǥ����    
  '1',                         -- ��ǥ����    
  NULL,                     -- ���λ��    
  @TP_DRCR,           -- ���뱸��    
  @CD_ACCT,          -- �����ڵ�    
  NULL,                    -- �����    
  @AM_DR,              -- �����ݾ�    
  @AM_CR,              -- �뺯�ݾ�    
  0,                          --  @P_TP_ACAREA    -- '4' �� ��� ����������ǥ�̹Ƿ� FI_BANH�� Insert�ȴ�      
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
  NULL,  --CD_PJT      
  NULL,  --NM_PJT      
  NULL,  --CD_DEPT      
  NULL,  --NM_DEPT      
  NULL,  --CD_EMPLOY      
  NULL,  --NM_EMPLOY      
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
  '000',      
  'KRW',      
  NULL,  -- CD_UMNG1      
  NULL,  -- CD_UMNG2      
  NULL,  -- CD_UMNG3      
  NULL,  -- CD_UMNG4      
  NULL,  -- CD_UMNG5      
  @NO_BIZAREA,  --  NO_RES      
  @AM_SUPPLY,  --  AM_SUPPLY      
  @CD_MNG ,   -- ������ȣ ( �ΰ����� ��� �ѱ�� �� )    
  @DT_START,  -- �߻�����      
  NULL,   --DT_END   -- ��������    
  1,         --P_RT_EXCH   ȯ��    
  0,         --AM_EXDO   ��ȭ�ݾ�    
  '003',     -- @P_NO_MODULE   ��ⱸ��  (�����⿡�� �����ڵ带 ���� ������ �R)    
  @P_NO_COST,   --  @P_NO_MDOCU   ��������ȣ    
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
  
  PRINT(@P_NO_DOCU)    
  PRINT(@P_NO_DOLINE)    
      
      
  --�߰�:������ 20071010 fi_tax�� �߰�����      
  IF @CD_RELATION = '30'       
  BEGIN      
   PRINT(@P_NO_TAX)    
   EXEC UP_FI_AUTODOCU_TAX @CD_COMPANY, @CD_PC, @P_NO_DOCU, @P_NO_DOLINE, @AM_SUPPLY      
  END      
             
  -------------------------------------------------------------------------------------------------------------------------------------------------------------------------        
        
  FETCH NEXT FROM IMCOST_CURSOR INTO @CD_COMPANY, @CD_PC, @CD_WDEPT, @ID_WRITE, @DT_ACCT, @TP_DRCR, @CD_ACCT, @AM_DR,  @AM_CR, @AM_SUPPLY, @CD_RELATION,       
  @TP_TAX,  @CD_BIZAREA, @CD_CC, @CD_DEPT,  @CD_EMPLOY,  @CD_PARTNER,   @DT_START, @DT_WRITE, @NM_BIZAREA, @NM_TAX, @NM_PARTNER, @NO_LC, @NO_BL, @NO_BIZAREA, @TP_ACAREA   
 END        
         
 CLOSE IMCOST_CURSOR        
 DEALLOCATE IMCOST_CURSOR        
         
 -- ��ǥó���� ����� �Ǿ�����        
 UPDATE TR_IMCOSTH SET YN_SLIP = 'Y' WHERE CD_COMPANY = @P_CD_COMPANY AND NO_COST = @P_NO_COST        
END  
GO    
