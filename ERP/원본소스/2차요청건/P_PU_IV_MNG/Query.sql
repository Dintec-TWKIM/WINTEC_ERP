IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_PU_IV_MNG_SELECT_H') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_PU_IV_MNG_SELECT_H
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_PU_IV_MNG_SELECT_H
 * ����������: ���� >> ������� >> ���Ե�� ��� ��ȸ
 * ��  ��  ��: ??(������ ���� ������ �����س���(������:������))
 * EXEC UP_PU_IV_MNG_SELECT_H '',''
 *********************************************************************************************************/ 
 
CREATE PROCEDURE UP_PU_IV_MNG_SELECT_H
	@CD_BIZAREA   NVARCHAR(7),  
	@DT_PROCESS_FROM NCHAR(8),  
	@DT_PROCESS_TO  NCHAR(8),  
	@CD_COMPANY   NVARCHAR(7),  
	@YN_PURSUB   NCHAR(1),  
	@FG_TRANS   NCHAR(3),  
	@TP_AIS    NCHAR(1),  
	@CD_PARTNER   NVARCHAR(7),  
	@CD_DEPT   NVARCHAR(12),  
	@NO_EMP    NVARCHAR(10)  
AS  
BEGIN  
	SELECT   DISTINCT 'F' AS CHK,
		H.NO_IV,
		H.DT_PROCESS,
		H.CD_PARTNER,
		P.LN_PARTNER,
		H.YN_PURSUB,  
		H.CD_BIZAREA,
		P.NO_COMPANY AS NO_BIZAREA,
		H.FG_TRANS,
		CT.NM_SYSDEF AS NM_TRANS,   
		H.FG_TAXP,
		H.FG_TAX,
		C.NM_SYSDEF AS NM_TAX, 
		H.TP_AIS,
		H.AM_K,
		H.VAT_TAX, 
		ISNULL(L.NO_LC,'') NO_LC,  
		B.CD_PC,H.CD_DEPT, 
		D.NM_DEPT,
		H.NO_EMP, 
		E.NM_KOR,
		PC.NM_PC  
		--CASE WHEN L.YN_RETURN = 'Y' THEN 0 - H.AM_K ELSE H.AM_K END AM_K ,  
		--CASE WHEN L.YN_RETURN = 'Y' THEN 0 - H.VAT_TAX ELSE H.VAT_TAX END VAT_TAX   
	FROM PU_IVH H  
		INNER JOIN  PU_IVL L ON L.CD_COMPANY = H.CD_COMPANY AND L.NO_IV = H.NO_IV  
		LEFT OUTER JOIN MA_PARTNER P ON P.CD_COMPANY = H.CD_COMPANY AND P.CD_PARTNER = H.CD_PARTNER  
		LEFT OUTER JOIN MA_CODEDTL C ON C.CD_COMPANY = H.CD_COMPANY AND C.CD_SYSDEF = H.FG_TAX AND C.CD_FIELD = 'MA_B000046'  
		LEFT OUTER JOIN MA_CODEDTL CT ON CT.CD_COMPANY = H.CD_COMPANY AND CT.CD_SYSDEF = H.FG_TRANS AND CT.CD_FIELD = 'PU_C000016'  
		INNER JOIN  MA_BIZAREA B ON B.CD_BIZAREA = H.CD_BIZAREA AND B.CD_COMPANY = H.CD_COMPANY 
		LEFT OUTER JOIN MA_DEPT D ON D.CD_DEPT = H.CD_DEPT AND D.CD_COMPANY = H.CD_COMPANY  
		LEFT OUTER JOIN MA_EMP E ON E.NO_EMP = H.NO_EMP AND E.CD_COMPANY = H.CD_COMPANY  
		LEFT OUTER JOIN MA_PC PC ON PC.CD_PC = B.CD_PC AND PC.CD_COMPANY = B.CD_COMPANY  
	WHERE   H.CD_BIZAREA = @CD_BIZAREA  
	AND    H.DT_PROCESS BETWEEN @DT_PROCESS_FROM AND @DT_PROCESS_TO  
	AND    H.CD_COMPANY = @CD_COMPANY  
	AND    H.YN_PURSUB IN (@YN_PURSUB)  
	AND    ((  H.FG_TRANS = @FG_TRANS) OR (@FG_TRANS = '' ) OR (@FG_TRANS IS NULL))  
	AND    ((  H.TP_AIS  = @TP_AIS) OR (@TP_AIS = '' ) OR (@TP_AIS IS NULL))  
	AND    ((  H.CD_PARTNER = @CD_PARTNER) OR (@CD_PARTNER = '' ) OR (@CD_PARTNER IS NULL))  
	AND    ((  H.CD_DEPT = @CD_DEPT) OR (@CD_DEPT = '' ) OR (@CD_DEPT IS NULL))  
	AND    ((  H.NO_EMP = @NO_EMP) OR (@NO_EMP = '' ) OR (@NO_EMP IS NULL))  
	--AND   H.YN_EXPIV ='N'  
	ORDER BY  H.NO_IV,H.CD_PARTNER  

	--exec UP_PU_IV_MNG_SELECT_H @CD_BIZAREA = N'BI00001', @DT_PROCESS_FROM = N'20041101        ', @DT_PROCESS_TO = N'20041111        ', @CD_COMPANY = N'1000000', @YN_PURSUB = N'N ', @FG_TRANS = N'      ', @TP_AIS = N'  ', @CD_PARTNER = N'', @CD_DEPT = N'', @
	NO_EMP = N''   
END
GO  
  

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_PU_IV_MNG_SELECT_L') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_PU_IV_MNG_SELECT_L
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_PU_IV_MNG_SELECT_L
 * ����������: ���� >> ������� >> ���Ե�� ���� ��ȸ
 * ��  ��  ��: ??(������ ���� ������ �����س���(������:������))
 * EXEC UP_PU_IV_MNG_SELECT_L '',''
 *********************************************************************************************************/ 
 
CREATE PROCEDURE UP_PU_IV_MNG_SELECT_L
	@CD_COMPANY NVARCHAR(7),   
	@NO_IV		NVARCHAR(20)   
AS  
BEGIN  
	SELECT 'F' AS CHK,
		L.NO_IV,
		L.NO_IO,
		L.DT_TAX, 
		L.CD_ITEM,
		M.NM_ITEM,
		M.UNIT_MO,
		Q.CD_UNIT_MM,
		L.NO_PO,
		L.NO_POLINE,  
		L.FG_TPPURCHASE, 
		C.NM_TP AS NM_TPPURCHASE,
		L.CD_PURGRP,
		G.NM_PURGRP,
		L.NO_EMP,
		E.NM_KOR,
		L.CD_PJT, 
		PJ.NM_PROJECT,  
		L.NO_IV,
		L.NO_LINE, 
		Q.DT_IO, 
		H.CD_BIZAREA, 
		H.CD_PARTNER,
		L.CD_PLANT,
		L.NO_IOLINE,  
		CASE WHEN L.YN_RETURN = 'Y' THEN 0 - L.QT_RCV_CLS ELSE L.QT_RCV_CLS END QT_RCV_CLS ,  
		L.UM_ITEM_CLS,  
		--CASE WHEN L.YN_RETURN = 'Y' THEN 0 - L.UM_ITEM_CLS ELSE L.UM_ITEM_CLS END UM_ITEM_CLS ,  
		CASE WHEN L.YN_RETURN = 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END AM_CLS ,  
		CASE WHEN L.YN_RETURN = 'Y' THEN 0 - L.VAT ELSE L.VAT END VAT    
	FROM PU_IVL L  
		INNER JOIN  PU_IVH H ON H.CD_COMPANY = L.CD_COMPANY AND H.NO_IV = L.NO_IV AND H.CD_COMPANY = @CD_COMPANY AND H.NO_IV = @NO_IV  
		LEFT OUTER JOIN MA_PITEM M ON M.CD_COMPANY = L.CD_COMPANY AND M.CD_ITEM = L.CD_ITEM AND M.CD_PLANT = L.CD_PLANT  
		LEFT OUTER JOIN MM_QTIO Q ON Q.NO_IO = L.NO_IO AND Q.NO_IOLINE = L.NO_IOLINE AND Q.CD_COMPANY = L.CD_COMPANY  
		LEFT OUTER JOIN MA_PURGRP G ON G.CD_COMPANY = L.CD_COMPANY AND G.CD_PURGRP = L.CD_PURGRP  
		LEFT OUTER JOIN MA_EMP E ON E.CD_COMPANY = L.CD_COMPANY AND E.NO_EMP = L.NO_EMP  
		LEFT OUTER JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = L.CD_COMPANY AND PJ.NO_PROJECT = L.CD_PJT AND PJ.NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)  
		LEFT OUTER JOIN MA_AISPOSTH C ON C.CD_COMPANY = L.CD_COMPANY AND C.CD_TP = L.FG_TPPURCHASE AND C.FG_TP = '200'  
	ORDER BY  L.NO_IO,L.DT_TAX, L.CD_ITEM  
END 
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_PU_IVH_DELETE') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_PU_IVH_DELETE
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_PU_IVH_DELETE
 * ����������: ���� >> ������� >> ���Ե�� ����
 * ��  ��  ��: ??(������ ���� ������ �����س���(������:������))
 * EXEC UP_PU_IVH_DELETE '',''
 *********************************************************************************************************/ 
  
CREATE PROCEDURE UP_PU_IVH_DELETE 
	@NO_IV		NVARCHAR(20),     
	@CD_COMPANY NVARCHAR(7)    
AS    
BEGIN    
    
	DECLARE    
		@ERRNO			INT,    
		@ERRMSG			VARCHAR(255),    
		@NO_LINE		NUMERIC(5),    
		@NO_IO			NVARCHAR(20),    
		@NO_IOLINE		NUMERIC(5),     
		@NO_PO			NVARCHAR(20),    
		@NO_POLINE		NUMERIC(5),     
		@TP_AIS			NCHAR(1),    
		@QT_RCV_CLS		NUMERIC(17,4),     
		@UM_ITEM_CLS	NUMERIC(15,4),    
		@AM_CLS			NUMERIC(17,4),    
		@VAT			NUMERIC(17,4),     
		@CD_PURGRP		NVARCHAR(7),    
		@CD_BIZAREA		NVARCHAR(7),     
		@CD_PARTNER		NVARCHAR(7),    
		@CD_ITEM		NVARCHAR(20),    
		@DT_PROCESS		NCHAR(8),    
		@FG_TRANS		NCHAR(3),    
		@YN_RETURN		NCHAR(1),     
		@QT_CLS			NUMERIC(17,4),     
		@CD_PLANT		NVARCHAR(7),     
		@CD_QTIOTP		NVARCHAR(7)    
    
	IF EXISTS (SELECT 1 FROM PU_IVL WHERE NO_IV = @NO_IV AND  CD_COMPANY =@CD_COMPANY)    
	BEGIN    
		/* PU_IVL.TD_PU_IVL Ʈ���ŷ� �ű�     
		DECLARE CUR_IVLDELETE CURSOR FOR       
		SELECT   L.NO_LINE,L.NO_IO,L. NO_IOLINE, L.NO_PO,L.NO_POLINE, H.TP_AIS,    
		  0 - L.QT_RCV_CLS AS QT_RCV_CLS  , L.UM_ITEM_CLS, 0 - L.AM_CLS AS AM_CLS,0- L.VAT AS VAT,     
		  L.CD_PURGRP,H.CD_BIZAREA, H.CD_PARTNER,L.CD_ITEM,H.DT_PROCESS,    
		  H.FG_TRANS,L.YN_RETURN, 0 - L.QT_CLS AS QT_CLS, L.CD_PLANT,     
		  CASE WHEN H.YN_EXPIV ='N' THEN Q.CD_QTIOTP WHEN H.YN_EXPIV ='Y' THEN PL.FG_RCV ELSE Q.CD_QTIOTP END CD_QTIOTP    

		FROM   PU_IVL L  
		INNER JOIN  PU_IVH H   
		ON    H.NO_IV = L.NO_IV    
		AND    H.CD_COMPANY = L.CD_COMPANY    
		LEFT OUTER JOIN MM_QTIO Q  
		ON    Q.NO_IO = L.NO_IO  
		AND    Q.NO_IOLINE = L.NO_IOLINE  
		AND    Q.CD_COMPANY = L.CD_COMPANY  
		INNER JOIN  MA_PITEM M  
		ON    M.CD_COMPANY = L.CD_COMPANY  
		AND    M.CD_ITEM = L.CD_ITEM  
		AND    M.CD_PLANT = L.CD_PLANT  
		LEFT OUTER JOIN PU_POL PL    
		ON    PL.NO_PO = L.NO_PO  
		AND    PL.NO_LINE = L.NO_POLINE  
		AND    PL.CD_COMPANY = L.CD_COMPANY  
		WHERE   L.NO_IV = @NO_IV    
		AND    L.CD_COMPANY = @CD_COMPANY    

		OPEN CUR_IVLDELETE       
		FETCH NEXT FROM CUR_IVLDELETE       
		INTO  @NO_LINE,@NO_IO,@NO_IOLINE, @NO_PO,@NO_POLINE, @TP_AIS,    
		@QT_RCV_CLS  , @UM_ITEM_CLS,@AM_CLS,@VAT, @CD_PURGRP,    
		@CD_BIZAREA, @CD_PARTNER,@CD_ITEM,@DT_PROCESS,@FG_TRANS,    
		@YN_RETURN, @QT_CLS, @CD_PLANT, @CD_QTIOTP    
		WHILE (@@FETCH_STATUS = 0)       
		BEGIN          
		 
		IF( @TP_AIS = 'Y')    
		BEGIN      
			 SELECT @ERRNO  = 100000,      
						  @ERRMSG = 'PU_M000094'      
			 GOTO ERROR      
		END       
		   
		    
		--���Խ��� ����    
		EXEC UP_PU_AP_UPDATE @CD_PARTNER,@CD_PURGRP,@CD_BIZAREA,@CD_COMPANY,@DT_PROCESS,@AM_CLS,@VAT,    
		   @YN_RETURN, 'N',@CD_ITEM,@FG_TRANS,@CD_PLANT,@CD_QTIOTP,@QT_CLS     
		IF (@@ERROR <> 0 )     
		BEGIN      
			 SELECT @ERRNO  = 100000,      
						  @ERRMSG = 'CM_M100010'      
			 GOTO ERROR      
		END       
		 
		 
		-- ����    
		IF( @NO_IOLINE > 0)    
		BEGIN    
		-- �����ܷ�����    
		EXEC UP_PU_MM_QTIO_FROM_IVL_UPDATE @NO_IO, @NO_IOLINE,@CD_COMPANY,@QT_CLS,@AM_CLS,@VAT,@QT_RCV_CLS, 'Y'    
		IF (@@ERROR <> 0 )     
		BEGIN      
			  SELECT @ERRNO  = 100000,      
						   @ERRMSG = 'CM_M100010'      
			  GOTO ERROR      
		END       
		END      
		   
		 
		DELETE PU_IVL WHERE NO_IV = @NO_IV AND NO_LINE =@NO_LINE AND CD_COMPANY = @CD_COMPANY    
		IF (@@ERROR <> 0 )    
		BEGIN      
			 SELECT @ERRNO  = 100000,      
						  @ERRMSG = 'CM_M100010'      
			 GOTO ERROR      
		END        
		 
		   
		FETCH NEXT FROM CUR_IVLDELETE     
		INTO  @NO_LINE,@NO_IO,@NO_IOLINE, @NO_PO,@NO_POLINE, @TP_AIS,    
		@QT_RCV_CLS  , @UM_ITEM_CLS,@AM_CLS,@VAT, @CD_PURGRP,    
		@CD_BIZAREA, @CD_PARTNER,@CD_ITEM,@DT_PROCESS,@FG_TRANS,    
		@YN_RETURN, @QT_CLS, @CD_PLANT, @CD_QTIOTP    
		END       
		CLOSE CUR_IVLDELETE     DEALLOCATE CUR_IVLDELETE         
		*/  
   
		-- ���� ����  
		DELETE PU_IVL WHERE NO_IV = @NO_IV AND CD_COMPANY = @CD_COMPANY    
		IF (@@ERROR <> 0 )    
			BEGIN      
			SELECT  @ERRNO  = 100000,      
					@ERRMSG = 'CM_M100010'      
			GOTO ERROR      
		END     
	  
		-- ��� ����  
		DELETE PU_IVH WHERE NO_IV = @NO_IV AND  CD_COMPANY =@CD_COMPANY    
		IF (@@ERROR <> 0 )    
			BEGIN      
			SELECT  @ERRNO  = 100000,      
					@ERRMSG = 'CM_M100010'      
			GOTO ERROR      
		END        
	END    
   
	RETURN    
	ERROR:      
		RAISERROR @ERRNO @ERRMSG      
END
GO    
    
    
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_PU_IV_MNG_TRANS_DOCU') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_PU_IV_MNG_TRANS_DOCU
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_PU_IV_MNG_TRANS_DOCU
 * ����������: ���� >> ������� >> ��ǥó��
 * ��  ��  ��: ??(������ ���� ������ �����س���(������:������))
 * EXEC UP_PU_IV_MNG_TRANS_DOCU '',''
 *********************************************************************************************************/     
  
CREATE PROCEDURE UP_PU_IV_MNG_TRANS_DOCU
	@P_CD_COMPANY        NVARCHAR(7),  
	@P_NO_IV             NVARCHAR(20),  
	@P_NO_MODULE         NVARCHAR(3)       -- /* ȸ����ǥ���� : ��������(210) , ����/����(001) */  
AS
BEGIN
	/*  
	SP_COLUMNS PU_IVH  
	SP_COLUMNS PU_IVL  
	SP_COLUMNS MA_PURGRP  
	SP_COLUMNS MA_AISPOSTL  
	*/  
	DECLARE @IN_CD_COMPANY		NVARCHAR(7)  
	DECLARE @IN_NO_MDOCU        NVARCHAR(20)  
	DECLARE @IN_CD_PARTNER      NVARCHAR(7)  
	DECLARE @IN_CD_CC           NVARCHAR(7)  
	DECLARE @IN_CD_PJT          NVARCHAR(20)  
	DECLARE @IN_ID_WRITE        NVARCHAR(10)  
	DECLARE @IN_CD_BIZAREA      NVARCHAR(7)  
	DECLARE @IN_TP_TAX          NCHAR(3)  
	DECLARE @IN_FG_TRANS        NCHAR(3)  
	DECLARE @IN_CD_WDEPT        NVARCHAR(12)  
	DECLARE @IN_DT_ACCT         NCHAR(8)  
	DECLARE @IN_CD_PC           NVARCHAR(7)  
	DECLARE @IN_CD_ACCT         NVARCHAR(10)  
	DECLARE @IN_AM_DR           NUMERIC(19,4)  
	DECLARE @IN_AM_CR           NUMERIC(19,4)  
	DECLARE @IN_TP_DRCR         NCHAR(1)  
	DECLARE @IN_CD_EXCH         NVARCHAR(3)  
	DECLARE @IN_RT_EXCH         NUMERIC(17,4)  
	DECLARE @IN_CD_EMPLOY       NVARCHAR(10)  
	DECLARE @IN_AM_EXDO         NUMERIC(19,4)  
	DECLARE @IN_NM_BIZAREA      NVARCHAR(50)  
	DECLARE @IN_NM_CC           NVARCHAR(50)  
	DECLARE @IN_NM_KOR          NVARCHAR(50)  
	DECLARE @IN_LN_PARTNER      NVARCHAR(50)  
	DECLARE @IN_NM_PROJECT      NVARCHAR(50)  
	DECLARE @IN_NM_DEPT         NVARCHAR(50)  
	DECLARE @IN_TYPE            NCHAR(1)  
  
	-----------------------------------------------------------------------------------------------------------
	IF @P_NO_MODULE = '001'        -- ȸ����ǥ���� : ����/�����̸�  
	DECLARE CUR_PU_IV_MNG CURSOR FOR 
	----------------------------------------------------------------------------------------------------------- 
	 
	SELECT H.CD_COMPANY,  
		L.NO_IV NO_MDOCU,  
		H.CD_PARTNER,  
		G.CD_CC,  
		L.CD_PJT,  
		H.NO_EMP ID_WRITE,  
		H.CD_BIZAREA,   
		H.FG_TAX TP_TAX,  
		H.FG_TRANS,  
		H.CD_DEPT CD_WDEPT,  
		H.DT_PROCESS DT_ACCT,  
		B.CD_PC,  
		A.CD_ACCT,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END ) AS AM_DR,  
		0 AS AM_CR,           
		'1' AS TP_DRCR,  
		L.CD_EXCH,  
		L.RT_EXCH,  
		L.NO_EMP CD_EMPLOY,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_EX_CLS ELSE L.AM_EX_CLS END ) AS AM_EXDO,  
		B.NM_BIZAREA,  
		C.NM_CC,  
		E.NM_KOR,  
		P.LN_PARTNER,  
		'' NM_PROJECT,  
		D.NM_DEPT,  
		'A' AS TYPE  
	FROM PU_IVH H,		PU_IVL L,	MA_PITEM M,		MA_AISPOSTL A,		MA_PURGRP G, 
		 MA_BIZAREA B,	MA_CC C,	MA_EMP E,		MA_PARTNER P,		MA_DEPT D  
	WHERE H.NO_IV = @P_NO_IV  
	AND H.CD_COMPANY = @P_CD_COMPANY  
	AND H.TP_AIS ='N' -- ��ǥ�߻�����  
	AND H.YN_PURSUB IN('Y','O') -- ����  
	AND H.NO_IV  = L.NO_IV  
	AND H.CD_COMPANY = L.CD_COMPANY  
	AND L.CD_ITEM = M.CD_ITEM  
	AND L.CD_PLANT = M.CD_PLANT  
	AND L.CD_COMPANY = M.CD_COMPANY  
	AND L.CD_COMPANY = A.CD_COMPANY  
	AND A.FG_TP ='200' -- ��������  
	AND L.FG_TPPURCHASE = A.CD_TP  
	AND A.FG_AIS = '214'  
	AND L.CD_PURGRP = G.CD_PURGRP  
	AND L.CD_COMPANY = G.CD_COMPANY  
	AND H.CD_BIZAREA = B.CD_BIZAREA  
	AND H.CD_COMPANY = B.CD_COMPANY   
	AND G.CD_CC *= C.CD_CC  
	AND G.CD_COMPANY *= C.CD_COMPANY  
	AND H.NO_EMP *= E.NO_EMP  
	AND H.CD_COMPANY *= E.CD_COMPANY  
	AND H.CD_PARTNER *= P.CD_PARTNER  
	AND H.CD_COMPANY *= P.CD_COMPANY  
	AND H.CD_DEPT *= D.CD_DEPT  
	AND H.CD_COMPANY *= D.CD_COMPANY          
	GROUP BY H.CD_COMPANY,	L.NO_IV,		H.CD_PARTNER,	G.CD_CC,	L.CD_PJT, 
			 H.NO_EMP,		H.CD_BIZAREA,	H.FG_TAX,		H.FG_TRANS,	H.CD_DEPT, 
			 H.DT_PROCESS,	B.CD_PC,		A.CD_ACCT,		L.CD_EXCH,	L.RT_EXCH,   
			 L.NO_EMP,		B.NM_BIZAREA,	C.NM_CC,		E.NM_KOR,	P.LN_PARTNER, 
			 D.NM_DEPT
	
	-- �ΰ������� : ����: �ΰ���   
	UNION ALL  
	SELECT H.CD_COMPANY,
		L.NO_IV AS NO_MDOCU, 
		H.CD_PARTNER, 
		'' CD_CC, 
		'' CD_PJT, 
		H.NO_EMP AS ID_WRITE, 
		H.CD_BIZAREA,   
		H.FG_TAX AS TP_TAX, 
		H.FG_TRANS,
		H.CD_DEPT AS CD_WDEPT, 
		H.DT_PROCESS AS DT_ACCT, 
		B.CD_PC, 
		A.CD_ACCT,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS AM_DR,  
		0 AS AM_CR,           
		'1' AS TP_DRCR,
		'000' AS CD_EXCH, 
		1 AS RT_EXCH, 
		'' AS CD_EMPLOY,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS AM_EXDO,  
		B.NM_BIZAREA, 
		'' AS NM_CC, 
		E.NM_KOR, 
		P.LN_PARTNER,
		'' AS NM_PROJECT, 
		D.NM_DEPT,'V' AS TYPE  
	FROM PU_IVH H , PU_IVL L, MA_PITEM M , MA_AISPOSTL A, MA_BIZAREA B,  MA_EMP E, MA_PARTNER P, MA_DEPT D  
	WHERE H.NO_IV = @P_NO_IV  
	AND H.CD_COMPANY = @P_CD_COMPANY  
	AND H.TP_AIS ='N'  -- ��ǥ�߻�����  
	AND H.YN_PURSUB IN('Y','O') -- ����  
	AND H.NO_IV  = L.NO_IV   
	AND H.CD_COMPANY = L.CD_COMPANY   
	AND L.CD_ITEM = M.CD_ITEM  
	AND L.CD_PLANT = M.CD_PLANT  
	AND L.CD_COMPANY = M.CD_COMPANY  
	AND L.CD_COMPANY = A.CD_COMPANY  
	AND A.FG_TP ='200' -- ��������  
	AND L.FG_TPPURCHASE = A.CD_TP   
	AND A.FG_AIS = '210' --�ΰ���          
	AND H.CD_BIZAREA = B.CD_BIZAREA  
	AND H.CD_COMPANY = B.CD_COMPANY   
	AND H.NO_EMP *= E.NO_EMP  
	AND H.CD_COMPANY *= E.CD_COMPANY  
	AND H.CD_DEPT *= D.CD_DEPT  
	AND H.CD_COMPANY *= D.CD_COMPANY  
	AND H.CD_PARTNER *= P.CD_PARTNER  
	AND H.CD_COMPANY *= P.CD_COMPANY          
	GROUP BY H.CD_COMPANY,	L.NO_IV,		H.CD_PARTNER,	H.NO_EMP,		H.CD_BIZAREA,   
			 H.FG_TAX,		H.FG_TRANS,		H.CD_DEPT,		H.DT_PROCESS,	B.CD_PC, 
			 A.CD_ACCT,		B.NM_BIZAREA,	E.NM_KOR,		P.LN_PARTNER,	D.NM_DEPT  
  
	-- ä������(�ܻ���Ա�) : �뺯: ��ȭ�ݾ�+�ΰ���   
	UNION ALL  
	SELECT H.CD_COMPANY,
		L.NO_IV AS NO_MDOCU, 
		H.CD_PARTNER, 
		G.CD_CC, 
		L.CD_PJT, 
		H.NO_EMP AS ID_WRITE, 
		H.CD_BIZAREA,   
		H.FG_TAX AS TP_TAX, 
		H.FG_TRANS,
		H.CD_DEPT AS CD_WDEPT, 
		H.DT_PROCESS AS DT_ACCT, 
		B.CD_PC,
		A.CD_ACCT,  
		0 AS AM_DR,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END )  +   
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS AM_CR,           
		'2' AS TP_DRCR, 
		L.CD_EXCH, 
		L.RT_EXCH, 
		L.NO_EMP AS CD_EMPLOY,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_EX_CLS ELSE L.AM_EX_CLS END ) AS AM_EXDO,  
		B.NM_BIZAREA, 
		C.NM_CC, 
		E.NM_KOR, 
		P.LN_PARTNER,
		'' NM_PROJECT, 
		D.NM_DEPT,
		'T' AS TYPE  
	FROM PU_IVH H,		PU_IVL L,	MA_PITEM M,		MA_AISPOSTL A,	MA_PURGRP G, 
		 MA_BIZAREA B,	MA_CC C,	MA_EMP E,		MA_PARTNER P,	MA_DEPT D  
	WHERE H.NO_IV = @P_NO_IV  
	AND H.CD_COMPANY = @P_CD_COMPANY  
	AND H.TP_AIS ='N'  -- ��ǥ�߻�����  
	AND H.YN_PURSUB IN('Y','O') -- ����  
	AND H.NO_IV  = L.NO_IV  
	AND H.CD_COMPANY = L.CD_COMPANY  
	AND L.CD_ITEM = M.CD_ITEM  
	AND L.CD_PLANT = M.CD_PLANT  
	AND L.CD_COMPANY = M.CD_COMPANY  
	AND L.CD_COMPANY = A.CD_COMPANY  
	AND A.FG_TP ='200' -- ��������  
	AND L.FG_TPPURCHASE = A.CD_TP  
	AND A.FG_AIS = '215'  
	AND L.CD_PURGRP = G.CD_PURGRP  
	AND L.CD_COMPANY = G.CD_COMPANY  
	AND H.CD_BIZAREA = B.CD_BIZAREA  
	AND H.CD_COMPANY = B.CD_COMPANY   
	AND G.CD_CC *= C.CD_CC  
	AND G.CD_COMPANY *= C.CD_COMPANY  
	AND H.NO_EMP *= E.NO_EMP  
	AND H.CD_COMPANY *= E.CD_COMPANY  
	AND H.CD_PARTNER *= P.CD_PARTNER  
	AND H.CD_COMPANY *= P.CD_COMPANY  
	AND H.CD_DEPT *= D.CD_DEPT  
	AND H.CD_COMPANY *= D.CD_COMPANY          
	GROUP BY H.CD_COMPANY,	L.NO_IV,		H.CD_PARTNER,	G.CD_CC,		L.CD_PJT , 
			 H.NO_EMP,		H.CD_BIZAREA,	H.FG_TAX,		H.FG_TRANS,		H.CD_DEPT, 
			 H.DT_PROCESS,	B.CD_PC,		A.CD_ACCT,		L.CD_EXCH,		L.RT_EXCH,   
			 L.NO_EMP,		B.NM_BIZAREA,	C.NM_CC,		E.NM_KOR,		P.LN_PARTNER,
			 D.NM_DEPT  
  
    -----------------------------------------------------------------------------------------------------------
	ELSE        -- �����̸�  
	DECLARE CUR_PU_IV_MNG CURSOR FOR 
	-----------------------------------------------------------------------------------------------------------
	 
	SELECT H.CD_COMPANY,
		L.NO_IV AS NO_MDOCU, 
		H.CD_PARTNER, 
		G.CD_CC, 
		L.CD_PJT, 
		H.NO_EMP AS ID_WRITE, 
		H.CD_BIZAREA,   
		H.FG_TAX AS TP_TAX, 
		H.FG_TRANS,
		H.CD_DEPT AS CD_WDEPT, 
		H.DT_PROCESS AS DT_ACCT, 
		B.CD_PC,
		A.CD_ACCT,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END ) AS AM_DR,  
		0 AS AM_CR,           
		'1' AS TP_DRCR , 
		L.CD_EXCH, 
		L.RT_EXCH, 
		L.NO_EMP AS CD_EMPLOY,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_EX_CLS ELSE L.AM_EX_CLS END ) AS AM_EXDO,  
		B.NM_BIZAREA, 
		C.NM_CC, 
		E.NM_KOR, 
		P.LN_PARTNER,
		PJ.NM_PROJECT, 
		D.NM_DEPT, 'A' AS TYPE  
	FROM PU_IVH H,		PU_IVL L,	MA_PITEM M,		MA_AISPOSTL A,	MA_PURGRP G, 
		 MA_BIZAREA B,	MA_CC C,	MA_EMP E,		MA_PARTNER P,	SA_PROJECTH PJ, 
		 MA_DEPT D  
	WHERE H.NO_IV = @P_NO_IV  
	AND H.CD_COMPANY = @P_CD_COMPANY  
	AND H.TP_AIS ='N' -- ��ǥ�߻�����  
	AND H.YN_PURSUB ='N' -- ����  
	AND H.NO_IV  = L.NO_IV  
	AND H.CD_COMPANY = L.CD_COMPANY  
	AND L.CD_ITEM = M.CD_ITEM  
	AND L.CD_PLANT = M.CD_PLANT  
	AND L.CD_COMPANY = M.CD_COMPANY  
	AND L.CD_COMPANY = A.CD_COMPANY  
	AND A.FG_TP ='200' -- ��������  
	AND L.FG_TPPURCHASE = A.CD_TP  
	AND A.FG_AIS =  (CASE M.CLS_ITEM WHEN '001' THEN '201' WHEN '002' THEN '202' WHEN '003' THEN '205' WHEN '004' THEN '204'   
				 WHEN '005' THEN '203' WHEN '006' THEN '209' WHEN '007' THEN '207' WHEN '008' THEN '208'  ELSE '208' END )   
	AND L.CD_PURGRP = G.CD_PURGRP  
	AND L.CD_COMPANY = G.CD_COMPANY  
	AND H.CD_BIZAREA = B.CD_BIZAREA  
	AND H.CD_COMPANY = B.CD_COMPANY   
	AND G.CD_CC *= C.CD_CC  
	AND G.CD_COMPANY *= C.CD_COMPANY  
	AND H.NO_EMP *= E.NO_EMP  
	AND H.CD_COMPANY *= E.CD_COMPANY  
	AND H.CD_PARTNER *= P.CD_PARTNER  
	AND H.CD_COMPANY *= P.CD_COMPANY  
	AND H.CD_DEPT *= D.CD_DEPT  
	AND H.CD_COMPANY *= D.CD_COMPANY  
	AND L.CD_COMPANY *= PJ.CD_COMPANY  
	AND L.CD_PJT *= PJ.NO_PROJECT  
	AND PJ.NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)  
	GROUP BY H.CD_COMPANY,	L.NO_IV,		H.CD_PARTNER,	G.CD_CC,	L.CD_PJT, 
			 H.NO_EMP,		H.CD_BIZAREA,	H.FG_TAX,		H.FG_TRANS,	H.CD_DEPT, 
			 H.DT_PROCESS,	B.CD_PC,		A.CD_ACCT,		L.CD_EXCH,	L.RT_EXCH,   
			 L.NO_EMP,		B.NM_BIZAREA,	C.NM_CC,		E.NM_KOR,	P.LN_PARTNER,
			 PJ.NM_PROJECT, D.NM_DEPT 
  
	-- �ΰ������� : ����: �ΰ���   
	UNION ALL  
	SELECT H.CD_COMPANY,
		L.NO_IV AS NO_MDOCU, 
		H.CD_PARTNER, 
		'' CD_CC, 
		'' CD_PJT, 
		H.NO_EMP AS ID_WRITE, 
		H.CD_BIZAREA,   
		H.FG_TAX AS TP_TAX, 
		H.FG_TRANS,
		H.CD_DEPT AS CD_WDEPT, 
		H.DT_PROCESS AS DT_ACCT, 
		B.CD_PC, 
		A.CD_ACCT,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS  AM_DR,  
		0 AS AM_CR,           
		'1' AS TP_DRCR,
		'000' AS CD_EXCH, 
		1 AS RT_EXCH, 
		'' AS CD_EMPLOY,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS AM_EXDO,  
		B.NM_BIZAREA, 
		'' AS NM_CC, 
		E.NM_KOR, 
		P.LN_PARTNER,
		'' AS NM_PROJECT, 
		D.NM_DEPT,'V' AS TYPE  
	FROM PU_IVH H , PU_IVL L, MA_PITEM M , MA_AISPOSTL A, MA_BIZAREA B,  MA_EMP E, MA_PARTNER P, MA_DEPT D  
	WHERE H.NO_IV = @P_NO_IV  
	AND H.CD_COMPANY = @P_CD_COMPANY  
	AND H.TP_AIS ='N'  -- ��ǥ�߻�����  
	AND H.YN_PURSUB ='N' --����    
	AND H.NO_IV  = L.NO_IV   
	AND H.CD_COMPANY = L.CD_COMPANY   
	AND L.CD_ITEM = M.CD_ITEM  
	AND L.CD_PLANT = M.CD_PLANT  
	AND L.CD_COMPANY = M.CD_COMPANY  
	AND L.CD_COMPANY = A.CD_COMPANY  
	AND A.FG_TP ='200' -- ��������  
	AND L.FG_TPPURCHASE = A.CD_TP   
	AND A.FG_AIS = '210' --�ΰ���          
	AND H.CD_BIZAREA = B.CD_BIZAREA  
	AND H.CD_COMPANY = B.CD_COMPANY   
	AND H.NO_EMP *= E.NO_EMP  
	AND H.CD_COMPANY *= E.CD_COMPANY  
	AND H.CD_DEPT *= D.CD_DEPT  
	AND H.CD_COMPANY *= D.CD_COMPANY  
	AND H.CD_PARTNER *= P.CD_PARTNER  
	AND H.CD_COMPANY *= P.CD_COMPANY          
	GROUP BY H.CD_COMPANY,	L.NO_IV,		H.CD_PARTNER,	H.NO_EMP,		H.CD_BIZAREA,   
			 H.FG_TAX,		H.FG_TRANS,		H.CD_DEPT,		H.DT_PROCESS,	B.CD_PC, 
			 A.CD_ACCT,		B.NM_BIZAREA,	E.NM_KOR,		P.LN_PARTNER,	D.NM_DEPT  
  
	-- ä������(�ܻ���Ա�) : �뺯: ��ȭ�ݾ�+�ΰ���   
	UNION ALL  
	SELECT H.CD_COMPANY,
		L.NO_IV AS NO_MDOCU, 
		H.CD_PARTNER, 
		G.CD_CC, 
		L.CD_PJT, 
		H.NO_EMP AS ID_WRITE, 
		H.CD_BIZAREA,   
		H.FG_TAX AS TP_TAX, 
		H.FG_TRANS,
		H.CD_DEPT AS CD_WDEPT, 
		H.DT_PROCESS AS DT_ACCT, 
		B.CD_PC,
		A.CD_ACCT,  
		0 AS AM_DR,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_CLS ELSE L.AM_CLS END )  +   
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.VAT ELSE L.VAT END ) AS AM_CR,           
		'2' AS TP_DRCR, 
		L.CD_EXCH, 
		L.RT_EXCH, 
		L.NO_EMP AS CD_EMPLOY,  
		SUM(CASE L.YN_RETURN WHEN 'Y' THEN 0 - L.AM_EX_CLS ELSE L.AM_EX_CLS END ) AS AM_EXDO,  
		B.NM_BIZAREA, 
		C.NM_CC, 
		E.NM_KOR, 
		P.LN_PARTNER,
		PJ.NM_PROJECT, 
		D.NM_DEPT,'T' AS TYPE  
	FROM PU_IVH H,		PU_IVL L,	MA_PITEM M, MA_AISPOSTL A,	MA_PURGRP G, 
		 MA_BIZAREA B,	MA_CC C,	MA_EMP E,	MA_PARTNER P,	SA_PROJECTH PJ, MA_DEPT D  
	WHERE H.NO_IV = @P_NO_IV  
	AND H.CD_COMPANY = @P_CD_COMPANY  
	AND H.TP_AIS ='N'  -- ��ǥ�߻�����  
	AND H.YN_PURSUB ='N' -- ����  
	AND H.NO_IV  = L.NO_IV  
	AND H.CD_COMPANY = L.CD_COMPANY  
	AND L.CD_ITEM = M.CD_ITEM  
	AND L.CD_PLANT = M.CD_PLANT  
	AND L.CD_COMPANY = M.CD_COMPANY  
	AND L.CD_COMPANY = A.CD_COMPANY  
	AND A.FG_TP ='200' -- ��������  
	AND L.FG_TPPURCHASE = A.CD_TP  
	AND A.FG_AIS =  (CASE H.FG_TRANS WHEN '001' THEN '211' WHEN '002' THEN '212' WHEN '003' THEN '212'  ELSE '211' END )   
	AND L.CD_PURGRP = G.CD_PURGRP  
	AND L.CD_COMPANY = G.CD_COMPANY  
	AND H.CD_BIZAREA = B.CD_BIZAREA  
	AND H.CD_COMPANY = B.CD_COMPANY   
	AND G.CD_CC *= C.CD_CC  
	AND G.CD_COMPANY *= C.CD_COMPANY  
	AND H.NO_EMP *= E.NO_EMP  
	AND H.CD_COMPANY *= E.CD_COMPANY  
	AND H.CD_PARTNER *= P.CD_PARTNER  
	AND H.CD_COMPANY *= P.CD_COMPANY  
	AND H.CD_DEPT *= D.CD_DEPT  
	AND H.CD_COMPANY *= D.CD_COMPANY  
	AND L.CD_COMPANY *= PJ.CD_COMPANY  
	AND L.CD_PJT *= PJ.NO_PROJECT  
	AND PJ.NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE CD_COMPANY = L.CD_COMPANY AND NO_PROJECT = L.CD_PJT)  
	GROUP BY H.CD_COMPANY,	L.NO_IV,		H.CD_PARTNER,	G.CD_CC,		L.CD_PJT, 
			 H.NO_EMP,		H.CD_BIZAREA,	H.FG_TAX,		H.FG_TRANS,		H.CD_DEPT, 
			 H.DT_PROCESS,	B.CD_PC,		A.CD_ACCT,		L.CD_EXCH,		L.RT_EXCH,   
			 L.NO_EMP,		B.NM_BIZAREA,	C.NM_CC,		E.NM_KOR,		P.LN_PARTNER,
			 PJ.NM_PROJECT, D.NM_DEPT  
  
  
	----------------------------------------------------------------------------------------------------
	-- ���⼭ ���� ��ǥó�� �ϱ� ���� �κ�  ---  
	DECLARE @P_NO_DOCU NVARCHAR(20)        -- ��ǥ��ȣ  
	DECLARE @P_DT_PROCESS NVARCHAR(8)  
  
	-- �������� �˾ƿ���  
	SELECT @P_DT_PROCESS = DT_PROCESS  
	FROM PU_IVH  
	WHERE CD_COMPANY = @P_CD_COMPANY AND NO_IV = @P_NO_IV  
  
	-- ��ǥ��ȣ ä��  
	EXEC UP_FI_DOCU_CREATE_SEQ @P_CD_COMPANY, 'FI01', @P_DT_PROCESS, @P_NO_DOCU OUTPUT  

	DECLARE @P_NO_DOLINE INT  
	SET @P_NO_DOLINE = 0  
  
	OPEN CUR_PU_IV_MNG  

	FETCH NEXT FROM CUR_PU_IV_MNG INTO 
		@IN_CD_COMPANY,	@IN_NO_MDOCU,	@IN_CD_PARTNER,		@IN_CD_CC,		@IN_CD_PJT,
		@IN_ID_WRITE,	@IN_CD_BIZAREA,	@IN_TP_TAX,			@IN_FG_TRANS,	@IN_CD_WDEPT,
		@IN_DT_ACCT,	@IN_CD_PC,		@IN_CD_ACCT,		@IN_AM_DR,		@IN_AM_CR,
		@IN_TP_DRCR,	@IN_CD_EXCH,	@IN_RT_EXCH,		@IN_CD_EMPLOY,	@IN_AM_EXDO,
		@IN_NM_BIZAREA,	@IN_NM_CC,		@IN_NM_KOR,			@IN_LN_PARTNER,	@IN_NM_PROJECT,
		@IN_NM_DEPT,	@IN_TYPE  
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		SET @P_NO_DOLINE = @P_NO_DOLINE + 1  
	  
		EXEC UP_FI_AUTODOCU  
			@P_NO_DOCU,          -- ��ǥ��ȣ  
			@P_NO_DOLINE,        -- ���ι�ȣ  
			@IN_CD_PC,           -- ȸ�����  
			@IN_CD_COMPANY,      -- ȸ���ڵ�            
			@IN_CD_WDEPT,        -- �ۼ��μ�  
			@IN_ID_WRITE,        -- �ۼ���  
			@IN_DT_ACCT,         -- �������� = ȸ������ = ó������  
			0,                   -- ȸ���ȣ        �̰��̴ϱ� NO_ACCT  
			'3',                 -- ��ǥ����-��ü TP_DOCU  
			'11',                -- ��ǥ����-�Ϲ� CD_DOCU  
			'1',                 -- ��ǥ����-�̰� ST_DOCU  
			NULL,                -- ������  
			@IN_TP_DRCR,         -- ���뱸��        TP_DRCR  
			@IN_CD_ACCT,         -- �����ڵ�  
			NULL,                -- ����  
			@IN_AM_DR,           -- �����ݾ� AM_DR  
			@IN_AM_CR,           -- �뺯�ݾ�        AM_CR  
			0,                   -- ����������-���� TP_ACAREA  
			10,                  -- �����׸�-�Ϲ� CD_RELATION    
			NULL,                -- �����ڵ� CD_BUDGET            
			NULL,                -- �ڱݰ��� CD_FUND            
			@IN_TP_TAX,          -- �������� TP_TAX                    
			NULL,                -- ������ǥ��ȣ NO_BDOCU            
			NULL,                -- ������ǥ���� NO_BDOLINE            
			'0',                 -- Ÿ�뱸�� TP_ETCACCT            
			@IN_CD_BIZAREA,      -- �ͼӻ����  
			@IN_CD_CC,           -- �ڽ�Ʈ����  
			@IN_CD_PJT,          -- ������Ʈ  
			@IN_CD_WDEPT,        -- �μ�  
			@IN_CD_EMPLOY,       -- ��� CD_EMPLOY  
			@IN_CD_PARTNER,      -- �ŷ�ó CD_PARTNER  
			NULL,                -- �������ڵ� CD_DEPOSIT  
			NULL,                -- ī���ȣ CD_CARD    
			NULL,                -- �����ڵ� CD_BANK  : MA_PARTNER���� FG_PARTNER = '002' �ΰ�  
			NULL,                -- ǰ���ڵ� NO_ITEM    
			NULL,                -- CD_UMNG1    
			NULL,                -- CD_UMNG2    
			NULL,                -- CD_UMNG3    
			NULL,                -- CD_UMNG4    
			NULL,                -- CD_UMNG5    
			@IN_NO_MDOCU,        -- ������ȣ = ��꼭��ȣ CD_MNG            
			NULL,                -- �ŷ����� CD_TRADE    
			@IN_DT_ACCT,         -- �ŷ�����, ��������, �߻����� DT_START    
			NULL,                -- �������� DT_END            
			@IN_CD_EXCH,         -- ȯ��        CD_EXCH  
			@IN_RT_EXCH,         -- ȯ��        RT_EXCH  
			@IN_AM_EXDO,         -- ��ȭ�ݾ� AM_EXDO   
			'A01',               -- �����׸�(�����) CD_MNG1    
			@IN_CD_BIZAREA,      -- CD_MNGD1    
			@IN_NM_BIZAREA,      -- NM_MNGD1    
			'A02',               -- �����׸�(CC) CD_MNG2    
			@IN_CD_CC,           -- CD_MNGD2    
			@IN_NM_CC,           -- NM_MNGD2    
			'A03',               -- �����׸�(�μ�) CD_MNG3    
			@IN_CD_WDEPT,        -- CD_MNGD3    
			@IN_NM_DEPT,         -- NM_MNGD3    
			'A04',               -- �����׸�(�����) CD_MNG4    
			@IN_ID_WRITE,        -- CD_MNGD4    
			@IN_NM_KOR,          -- NM_MNGD4    
			'A05',               -- �����׸�(������Ʈ) CD_MNG5    
			@IN_CD_PJT,          -- CD_MNGD5    
			@IN_NM_PROJECT,      -- NM_MNGD5    
			'A06',               -- �����׸�(�ŷ�ó) CD_MNG6    
			@IN_CD_PARTNER,      -- CD_MNGD6    
			@IN_LN_PARTNER,      -- NM_MNGD6    
			NULL,                -- �����׸�(����ǥ�ؾ�) CD_MNG7    
			NULL,                -- CD_MNGD7    
			NULL,                -- NM_MNGD7    
			NULL,                -- �����׸�(�߻�����) CD_MNG8    
			NULL,                -- CD_MNGD8    
			NULL,                -- NM_MNGD8    
			@P_NO_MODULE,        -- ��ⱸ��(����:002) NO_MODULE   
			@IN_NO_MDOCU,        -- ��������ȣ = Ÿ���pkey NO_MDOCU    
			NULL,                -- ��������ڵ� CD_EPNOTE   
			@IN_ID_WRITE,        -- ��ǥó����  
			NULL,                -- ������� CD_BGACCT   
			NULL,                -- ���Ǳ��� TP_EPNOTE   
			NULL,                -- ǰ�ǳ��� NM_PUMM    
			@P_DT_PROCESS,       -- �ۼ����� DT_WRITE    
			0,                   -- AM_ACTSUM   
			0,                   -- AM_JSUM    
			'N',                  -- YN_GWARE    
			NULL                  -- �����ȹ�ڵ� CD_BIZPLAN  
	  
		IF (@P_NO_MODULE = '210') -- ȸ����ǥ���� : �������� �̸�  
		BEGIN   
		  UPDATE PU_IVH SET TP_AIS = 'Y' WHERE CD_COMPANY = @IN_CD_COMPANY AND NO_IV = @IN_NO_MDOCU  
		END   
	          
	  
		FETCH NEXT FROM CUR_PU_IV_MNG INTO 
		@IN_CD_COMPANY,	@IN_NO_MDOCU,	@IN_CD_PARTNER,	@IN_CD_CC,		@IN_CD_PJT,
		@IN_ID_WRITE,	@IN_CD_BIZAREA,	@IN_TP_TAX,		@IN_FG_TRANS,	@IN_CD_WDEPT,
		@IN_DT_ACCT,	@IN_CD_PC,		@IN_CD_ACCT,	@IN_AM_DR,		@IN_AM_CR,
		@IN_TP_DRCR,	@IN_CD_EXCH,	@IN_RT_EXCH,	@IN_CD_EMPLOY,	@IN_AM_EXDO,
		@IN_NM_BIZAREA,	@IN_NM_CC,		@IN_NM_KOR,		@IN_LN_PARTNER,	@IN_NM_PROJECT,
		@IN_NM_DEPT,	@IN_TYPE  
	END  
  
	CLOSE CUR_PU_IV_MNG  
	DEALLOCATE CUR_PU_IV_MNG  
  
END
GO