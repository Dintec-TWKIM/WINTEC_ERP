USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIR_AUTO_REG]    Script Date: 2016-11-04 오후 3:34:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIR_AUTO_REG]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_SO			NVARCHAR(20),
	@P_CD_MAIN_CATEGORY	NVARCHAR(4),
	@P_CD_SUB_CATEGORY	NVARCHAR(4),
	@P_CD_RMK			NVARCHAR(3),
	@P_DT_START			NVARCHAR(8),
	@P_DT_END			NVARCHAR(8),
	@P_DC_RMK			NVARCHAR(MAX),
	@P_ID_USER			NVARCHAR(15)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

BEGIN TRAN SP_CZ_SA_GIR_AUTO_REG
BEGIN TRY

IF ISNULL(@P_DT_END, '') = ''
BEGIN
	UPDATE SD
	SET SD.YN_GIR_AUTO = 'E'
	FROM CZ_SA_SOH_DLV SD
	WHERE SD.CD_COMPANY = @P_CD_COMPANY
	AND SD.NO_SO = @P_NO_SO
	AND ISNULL(SD.YN_USE, 'N') = 'Y'

	PRINT '출고예정일 누락'

	RETURN
END

DECLARE @V_CD_PARTNER NVARCHAR(20)
DECLARE @V_NO_IMO NVARCHAR(10)
DECLARE @V_CD_DELIVERY_TO NVARCHAR(20)

SELECT @V_CD_PARTNER = SH.CD_PARTNER,
	   @V_NO_IMO = SH.NO_IMO,
	   @V_CD_DELIVERY_TO = SD.CD_DLV_TO
FROM SA_SOH SH
LEFT JOIN CZ_SA_SOH_DLV SD ON SD.CD_COMPANY = SH.CD_COMPANY AND SD.NO_SO = SH.NO_SO AND ISNULL(SD.YN_USE, 'N') = 'Y'
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.NO_SO = @P_NO_SO

IF (NOT EXISTS (SELECT 1 
		        FROM MA_PARTNER MP
		        JOIN CZ_MA_PARTNER MP1 ON MP1.CD_COMPANY = MP.CD_COMPANY AND MP1.CD_PARTNER = MP.CD_PARTNER
		        WHERE MP.CD_COMPANY = @P_CD_COMPANY
		        AND MP.CD_PARTNER = @V_CD_PARTNER
		        AND ISNULL(MP1.YN_USE_GIR, 'N') = 'Y') OR EXISTS (SELECT 1 
																  FROM CZ_MA_CODEDTL MC
																  WHERE MC.CD_COMPANY = @P_CD_COMPANY
																  AND MC.CD_FIELD = 'CZ_SA00062'
																  AND MC.CD_SYSDEF = @V_NO_IMO))
BEGIN
	UPDATE SD
	SET SD.YN_GIR_AUTO = 'E'
	FROM CZ_SA_SOH_DLV SD
	WHERE SD.CD_COMPANY = @P_CD_COMPANY
	AND SD.NO_SO = @P_NO_SO
	AND ISNULL(SD.YN_USE, 'N') = 'Y'

	PRINT '협조전 작성불가 매출처 또는 호선'

	RETURN
END

CREATE TABLE #EWS
(
    YN_EXCEPT NVARCHAR(1),
	YN_PAY	  NVARCHAR(1),
    WN_MSG1   NVARCHAR(400),
    WN_MSG2   NVARCHAR(400),
    WN_MSG3   NVARCHAR(400),
    WN_MSG4   NVARCHAR(400),
    WN_MSG5   NVARCHAR(400),
	WN_MSG6   NVARCHAR(400),
	WN_MSG7   NVARCHAR(400),
    WN_LEVEL  NVARCHAR(1)
)

INSERT #EWS
EXEC SP_CZ_SA_EALRY_WARNING_SYSTEM @P_CD_COMPANY, @V_CD_PARTNER

IF EXISTS (SELECT * 
		   FROM #EWS
		   WHERE ISNULL(YN_EXCEPT, 'N') = 'N'
		   AND WN_LEVEL = '2')
BEGIN
	UPDATE SD
	SET SD.YN_GIR_AUTO = 'E'
	FROM CZ_SA_SOH_DLV SD
	WHERE SD.CD_COMPANY = @P_CD_COMPANY
	AND SD.NO_SO = @P_NO_SO
	AND ISNULL(SD.YN_USE, 'N') = 'Y'

	DROP TABLE #EWS
	
	PRINT '조기경보시스템 걸림'

	RETURN
END

DROP TABLE #EWS

DECLARE @V_ERRMSG		VARCHAR(255)   -- ERROR 메시지
DECLARE @V_NO_GIR		NVARCHAR(20)
DECLARE @V_NO_INV		NVARCHAR(20)
DECLARE @V_YM			NVARCHAR(6)
DECLARE @V_CD_PLANT		NVARCHAR(7)
DECLARE @V_QT_BL		INT
DECLARE @V_AM_SO		NUMERIC(17, 4)
DECLARE @V_NO_EMAIL		NVARCHAR(1000)
DECLARE @V_YN_DS		NVARCHAR(1)
DECLARE @V_YN_BL		NVARCHAR(1)
DECLARE @V_DC_RMK3		NVARCHAR(MAX)

SET @V_YM = SUBSTRING(CONVERT(CHAR(8), GETDATE(), 112), 3, 4)

SELECT @V_CD_PLANT = MAX(SL.CD_PLANT),
	   @V_QT_BL = SUM(CASE WHEN ISNULL(PL.YN_BL, 'N') = 'Y' THEN PL.QT_PO ELSE 0 END),
	   @V_AM_SO = SUM(SL.AM_SO)
FROM SA_SOL SL
LEFT JOIN PU_POL PL ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
WHERE SL.CD_COMPANY = @P_CD_COMPANY
AND SL.NO_SO = @P_NO_SO

EXEC CP_GETNO @P_CD_COMPANY, 'SA', '03', @V_YM, @V_NO_GIR OUTPUT
EXEC CP_GETNO @P_CD_COMPANY, 'TRE', '05', @V_YM, @V_NO_INV OUTPUT

INSERT INTO SA_GIRH
(
	CD_COMPANY,
	NO_GIR,
	DT_GIR,
	CD_PARTNER,
	CD_PLANT,
	NO_EMP, 
	TP_BUSI,
	YN_RETURN,
	ID_INSERT,
	DTS_INSERT
)
SELECT SH.CD_COMPANY,
	   @V_NO_GIR,
	   CONVERT(CHAR(8), GETDATE(), 112) AS DT_GIR,
	   SH.CD_PARTNER,
	   @V_CD_PLANT,
	   @P_ID_USER,
	   TP.TP_BUSI,
	   'N' AS YN_RETURN,
	   @P_ID_USER,
	   NEOE.SF_SYSDATE(GETDATE())
FROM SA_SOH SH
LEFT JOIN SA_TPSO TP ON TP.CD_COMPANY = SH.CD_COMPANY AND TP.TP_SO = SH.TP_SO
WHERE SH.CD_COMPANY	= @P_CD_COMPANY
AND SH.NO_SO = @P_NO_SO

INSERT INTO CZ_SA_GIRH_WORK_DETAIL  
(
	CD_COMPANY,
	NO_GIR,
	CD_MAIN_CATEGORY,
	CD_SUB_CATEGORY,
	YN_PACKING,
	YN_TAX_RETURN,
	CD_DELIVERY_TO,
	SEQ_DELIVERY_PIC,
	CD_FREIGHT,
	CD_FORWARDER,
	WEIGHT,
	DT_START,
	DT_COMPLETE,
	YN_AUTO_SUBMIT,
	DT_AUTO_COMPLETE,
	DT_BILL,
	FG_REASON,
	NO_IMO,
	NO_IMO_BILL,
	CD_RMK,
	TP_CHARGE,
	DC_RMK,
	DC_RMK1,
	DC_RMK2,
	DC_RMK3,
	DC_RMK4,
	DTS_CUTOFF,
    ID_INSERT,			
	DTS_INSERT
)  
SELECT SH.CD_COMPANY,
	   @V_NO_GIR AS NO_GIR,
	   @P_CD_MAIN_CATEGORY AS CD_MAIN_CATEGORY,
	   @P_CD_SUB_CATEGORY AS CD_SUB_CATEGORY,
	   'Y' AS YN_PACKING,
	   (CASE WHEN @V_QT_BL > 0 THEN 'Y' ELSE 'N' END) AS YN_TAX_RETURN,
	   @V_CD_DELIVERY_TO AS CD_DELIVERY_TO,
	   NULL AS SEQ_DELIVERY_PIC,
	   NULL AS CD_FREIGHT,
	   NULL AS CD_FORWARDER,
	   NULL AS WEIGHT,		     
	   @P_DT_START AS DT_START,
	   @P_DT_END AS DT_COMPLETE,
	   NULL AS YN_AUTO_SUBMIT,
	   '2' AS DT_AUTO_COMPLETE,
	   NULL AS DT_BILL,
	   NULL AS FG_REASON,
	   SH.NO_IMO,
	   NULL AS NO_IMO_BILL,
	   @P_CD_RMK AS CD_RMK,
	   (CASE WHEN ISNULL(SD.CD_CHARGE, '') = '' THEN ''
			 WHEN (ISNULL(SD.CD_CHARGE, '') = 'M' OR ISNULL(SD.YN_CHARGE, 'N') <> 'Y') THEN '002'
			 WHEN ISNULL(SD.CD_CHARGE, '') = 'A' THEN '001' END) AS TP_CHARGE,
	   (CASE WHEN @P_CD_MAIN_CATEGORY = '001' THEN (CASE WHEN ISNULL(@P_DC_RMK, '') = '' THEN SD.DC_DLV ELSE @P_DC_RMK END) ELSE NULL END) AS DC_RMK,
	   NULL AS DC_RMK1,
	   NULL AS DC_RMK2,
	   NULL AS DC_RMK3,
	   (CASE WHEN @P_CD_MAIN_CATEGORY <> '001' THEN (CASE WHEN ISNULL(@P_DC_RMK, '') = '' THEN SD.DC_DLV ELSE @P_DC_RMK END) ELSE NULL END) AS DC_RMK4,
	   (ISNULL(SD.DT_CUTOFF, '') + LEFT(ISNULL(SD.TM_CUTOFF, ''), 2)) AS DTS_CUTOFF,
	   @P_ID_USER,
	   NEOE.SF_SYSDATE(GETDATE()) 
FROM SA_SOH SH
LEFT JOIN CZ_SA_SOH_DLV SD ON SD.CD_COMPANY = SH.CD_COMPANY AND SD.NO_SO = SH.NO_SO AND ISNULL(SD.YN_USE, 'N') = 'Y'
WHERE SH.CD_COMPANY	= @P_CD_COMPANY
AND SH.NO_SO = @P_NO_SO

IF (@P_CD_MAIN_CATEGORY = '001' AND (ISNULL(@P_DC_RMK, '') <> '' OR EXISTS (SELECT 1 
																			FROM CZ_SA_SOH_DLV SD 
																			WHERE SD.CD_COMPANY = @P_CD_COMPANY
																			AND SD.NO_SO = @P_NO_SO
																			AND ISNULL(SD.YN_USE, 'N') = 'Y'
																			AND ISNULL(SD.DC_DLV, '') <> '')))
BEGIN
	INSERT INTO CZ_SA_GIRH_REMARK
	(
		CD_COMPANY,
		NO_GIR,
		YN_REVIEW,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES
	(
		@P_CD_COMPANY,
		@V_NO_GIR,
		'Y',
		@P_ID_USER,
		NEOE.SF_SYSDATE(GETDATE())
	)
END

INSERT INTO CZ_TR_INVH
(
	NO_INV,
	CD_COMPANY,
	DT_BALLOT,
	CD_BIZAREA,
	CD_SALEGRP,
	NO_EMP,     
	FG_LC,
	CD_PARTNER,
	CD_EXCH,
	AM_EX,
	DT_LOADING,
	CD_ORIGIN,     
	CD_AGENT,
	CD_EXPORT,
	CD_PRODUCT,
	SHIP_CORP,
	NM_VESSEL,
	COND_TRANS,     
	TP_TRANSPORT,
	TP_TRANS,         
	TP_PACKING,       
	CD_WEIGHT,          
	GROSS_WEIGHT,       
	NET_WEIGHT,     
	PORT_LOADING,    
	PORT_ARRIVER,     
	DESTINATION,      
	NO_SCT,             
	NO_ECT,             
	CD_NOTIFY,     
	DT_TO,           
	NO_LC,            
	NO_SO,            
	REMARK1,            
	REMARK2,            
	REMARK3,     
	REMARK4,         
	REMARK5,          
	DTS_INSERT,       
	ID_INSERT,          
	NM_NOTIFY,          
	ADDR1_NOTIFY,
	ADDR2_NOTIFY,    
	CD_CONSIGNEE,     
	NM_CONSIGNEE,     
	ADDR1_CONSIGNEE,    
	ADDR2_CONSIGNEE,    
	REMARK,
	NM_PARTNER,      
	ADDR1_PARTNER,    
	ADDR2_PARTNER,    
	NM_EXPORT,          
	ADDR1_EXPORT,       
	ADDR2_EXPORT,
	COND_PRICE,      
	DESCRIPTION,      
	GROSS_VOLUME,     
	FG_FREIGHT, 
    AM_FREIGHT,      
	YN_RETURN,        
	DT_SAILING_ON,    
	TXT_REMARK2,		  
	CD_BANK,			  
	COND_PAY,
	ARRIVER_COUNTRY,
	YN_INSURANCE
)            
SELECT @V_NO_INV,       
	   SH.CD_COMPANY,    
	   CONVERT(CHAR(8), GETDATE(), 112) AS DT_BALLOT,     
	   SH.CD_BIZAREA,      
	   SH.CD_SALEGRP,      
	   SH.NO_EMP,     
	   TP.TP_BUSI,        
	   SH.CD_PARTNER,    
	   SH.CD_EXCH,       
	   @V_AM_SO AS AM_EX,           
	   CONVERT(CHAR(8), GETDATE(), 112) AS DT_LOADING,      
	   '001' AS CD_ORIGIN,     
	   NULL AS CD_AGENT,     
	   MP1.CD_PARTNER AS CD_EXPORT,     
	   '001' AS CD_PRODUCT,    
	   NULL AS SHIP_CORP,       
	   NULL AS NM_VESSEL,       
	   NULL AS COND_TRANS,     
	   SH.TP_TRANSPORT, 
	   SH.TP_TRANS,      
	   NULL AS TP_PACKING,    
	   NULL AS CD_WEIGHT,       
	   0 AS GROSS_WEIGHT,    
	   0 AS NET_WEIGHT,     
	   SH.PORT_LOADING, 
	   NULL AS PORT_ARRIVER,  
	   NULL AS DESTINATION,   
	   0 AS NO_SCT,          
	   0 AS NO_ECT,          
	   NULL AS CD_NOTIFY,     
	   CONVERT(CHAR(8), GETDATE(), 112) AS DT_TO,        
	   NULL AS NO_LC,         
	   NULL AS NO_SO,         
	   NULL AS REMARK1,         
	   NULL AS REMARK2,         
	   NULL AS REMARK3,     
	   NULL AS REMARK4,      
	   NULL AS REMARK5,
	   SH.DTS_INSERT,    
	   SH.ID_INSERT,       
	   NULL AS NM_NOTIFY,       
	   NULL AS ADDR1_NOTIFY,
	   NULL AS ADDR2_NOTIFY,
	   NULL AS CD_CONSIGNEE,
	   NULL AS NM_CONSIGNEE,
	   NULL AS ADDR1_CONSIGNEE,
	   NULL AS ADDR2_CONSIGNEE,
	   NULL AS REMARK,
	   MP.LN_PARTNER AS NM_PARTNER,
	   MP.DC_ADS1_H AS ADDR1_PARTNER,
	   MP.DC_ADS1_D AS ADDR2_PARTNER,
	   MP1.LN_PARTNER AS NM_EXPORT,       
	   MP1.DC_ADS1_H AS ADDR1_EXPORT,    
	   MP1.DC_ADS1_D AS ADDR2_EXPORT,
	   NULL AS COND_PRICE,   
	   SH.NO_PO_PARTNER AS DESCRIPTION,   
	   0 AS GROSS_VOLUME,  
	   NULL AS FG_FREIGHT, 
	   0 AS AM_FREIGHT,   
	   'N' AS YN_RETURN,     
	   NULL AS DT_SAILING_ON, 
	   NULL AS TXT_REMARK2,	  
	   NULL AS CD_BANK,		  
	   SH.COND_PAY,
	   NULL AS ARRIVER_COUNTRY,
	   NULL AS YN_INSURANCE
FROM SA_SOH SH
LEFT JOIN SA_TPSO TP ON TP.CD_COMPANY = SH.CD_COMPANY AND TP.TP_SO = SH.TP_SO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN MA_PARTNER MP1 ON MP1.CD_COMPANY = SH.CD_COMPANY AND MP1.CD_PARTNER = (CASE SH.CD_COMPANY WHEN 'K100' THEN '00000'
																								    WHEN 'K200' THEN '09989'
																									WHEN 'S100' THEN '10286' END)
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.NO_SO = @P_NO_SO

INSERT INTO SA_GIRL 
(
	CD_COMPANY,
	NO_GIR,
	SEQ_GIR,     
	CD_ITEM,		
	TP_ITEM,
	DT_DUEDATE,  
	DT_REQGI,   
	YN_INSPECT,  
	CD_SL,		
	TP_GI,
	QT_GIR,
	QT_GIR_STOCK,  
	CD_EXCH,    
	UM,          
	AM_GIR,		
	AM_GIRAMT,
	AM_VAT,      
	UNIT,       
	QT_GIR_IM,   
	GI_PARTNER,	
	NO_PROJECT,
	RT_EXCH,     
	RT_VAT,     
	NO_SO,       
	SEQ_SO,		
	CD_SALEGRP,
	TP_VAT,      
	NO_EMP,     
	TP_IV,       
	FG_TAXP,		
	TP_BUSI,
	GIR,         
	IV,         
	RET,         
	SUBCONT,		
	NO_LC,
	SEQ_LC,      
	FG_LC_OPEN, 
	DC_RMK,      
	CD_WH,       
	NO_PMS,     
	SEQ_PROJECT, 
	YN_PICKING,	
	CD_USERDEF1,
	NO_INV,      
	TP_UM_TAX,  
	UMVAT_GIR,
	YN_ADD_STOCK,
	ID_INSERT,	
	DTS_INSERT
)
SELECT SH.CD_COMPANY, 
       @V_NO_GIR,
	   SL.SEQ_SO,     
	   SL.CD_ITEM,		
	   SL.TP_ITEM,
	   SL.DT_DUEDATE,  
	   SL.DT_REQGI,   
	   'N' AS YN_INSPECT,  
	   (CASE WHEN SL.CD_ITEM LIKE 'SD%' THEN 'VL01' ELSE PL.CD_SL END) AS CD_SL,
	   SL.TP_GI,
	   SL.QT_SO AS QT_GIR,
	   ISNULL(SB.QT_BOOK, 0) AS QT_GIR_STOCK,
	   SH.CD_EXCH,    
	   SL.UM_SO AS UM,          
	   SL.AM_SO AS AM_GIR,		
	   SL.AM_WONAMT AS AM_GIRAMT,
	   SL.AM_VAT,      
	   SL.UNIT_IM AS UNIT,       
	   SL.QT_SO AS QT_GIR_IM,   
	   SL.GI_PARTNER,	
	   SL.NO_PROJECT,
	   SH.RT_EXCH,     
	   SL.RT_VAT,     
	   SL.NO_SO,       
	   SL.SEQ_SO,		
	   SH.CD_SALEGRP,
	   SL.TP_VAT,      
	   SH.NO_EMP,     
	   SL.TP_IV,       
	   SH.FG_TAXP,		
	   SL.TP_BUSI,
	   SL.GIR,         
	   SL.IV,         
	   'N',         
	   'N',		
	   NULL AS NO_LC,
	   0 AS SEQ_LC,      
	   NULL AS FG_LC_OPEN, 
	   SL.DC1 AS DC_RMK,      
	   SL.CD_WH,       
	   NULL AS NO_PMS,     
	   SL.SEQ_PROJECT, 
	   SL.YN_PICKING,	
	   SL.CD_USERDEF1,
	   @V_NO_INV,      
	   NULL AS TP_UM_TAX,  
	   0 AS UMVAT_GIR,
	   NULL AS YN_ADD_STOCK,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
FROM SA_SOH SH
JOIN SA_SOL SL ON SH.CD_COMPANY	= SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO
LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
				  MAX(CASE WHEN IL.CD_SL IN ('SL01', 'VL01', 'VL02') THEN IL.CD_SL 
					   	   WHEN IL1.CD_SL = 'SL01' THEN IL1.CD_SL									   
					   	   ELSE 'SL01' END) AS CD_SL
		   FROM PU_POL PL
		   LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
		   LEFT JOIN MM_QTIO IL1 ON IL1.CD_COMPANY = IL.CD_COMPANY AND IL1.NO_IO_MGMT = IL.NO_IO AND IL1.NO_IOLINE_MGMT = IL.NO_IOLINE
		   WHERE NOT EXISTS (SELECT 1 
							 FROM MM_QTIOH IH
							 WHERE IH.CD_COMPANY = IL.CD_COMPANY
							 AND IH.NO_IO = IL.NO_IO
							 AND IH.YN_RETURN = 'Y')
		   GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
WHERE SH.CD_COMPANY	= @P_CD_COMPANY 
AND SH.NO_SO = @P_NO_SO

INSERT INTO CZ_TR_INVL 
(
	CD_COMPANY,
	NO_INV,
	NO_LINE,
	CD_PLANT,
	CD_ITEM,
	DT_DELIVERY,
	QT_INVENT,
	UM_INVENT,
	AM_INVENT,
	QT_SO,
	UM_SO,
	AM_EXSO,
	NO_PO,
	NO_LINE_PO,
	ID_INSERT,
	DTS_INSERT,
	YN_PRINT
) 
SELECT SL.CD_COMPANY, 
	   @V_NO_INV,
	   SL.SEQ_SO,
	   SL.CD_PLANT,
	   SL.CD_ITEM,
	   SL.DT_DUEDATE,
	   SL.QT_SO,
	   ISNULL(SL.UM_SO, 0) AS UM_SO,
	   SL.AM_SO,
	   SL.QT_SO,
	   ISNULL(SL.UM_SO, 0) AS UM_SO,
	   SL.AM_SO,
	   SL.NO_SO,
	   SL.SEQ_SO,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()),
	   'Y'
FROM SA_SOH SH
JOIN SA_SOL SL ON SH.CD_COMPANY	= SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO
WHERE SH.CD_COMPANY	= @P_CD_COMPANY 
AND SH.NO_SO = @P_NO_SO

SELECT @V_NO_EMAIL = MD.NO_EMAIL 
FROM CZ_MA_DELIVERY MD
WHERE MD.CD_COMPANY = @P_CD_COMPANY
AND MD.CD_PARTNER = @V_CD_DELIVERY_TO

SET @V_YN_DS = (CASE WHEN EXISTS (SELECT 1 
								  FROM SA_SOH SH
								  WHERE SH.CD_COMPANY = @P_CD_COMPANY
								  AND SH.NO_SO = @P_NO_SO
								  AND (SH.CD_PARTNER = '10286' OR SH.NO_SO LIKE 'DS%')) THEN 'Y' ELSE 'N' END)

SET @V_YN_BL = (CASE WHEN EXISTS (SELECT 1 
								  FROM PU_POL PL
								  WHERE PL.CD_COMPANY = @P_CD_COMPANY
								  AND PL.NO_SO = @P_NO_SO
								  AND ISNULL(PL.YN_BL, 'N') = 'Y') THEN 'Y' ELSE 'N' END)

IF @V_YN_BL = 'Y'
BEGIN
	UPDATE WD
	SET WD.DC_RMK = '★관세환급 받아야 하는 건 입니다.'
	FROM CZ_SA_GIRH_WORK_DETAIL WD
	WHERE WD.CD_COMPANY = @P_CD_COMPANY
	AND WD.NO_GIR = @V_NO_GIR
END

IF @V_YN_DS = 'Y' OR @V_YN_BL = 'Y' OR ISNULL(@V_NO_EMAIL, '') = ''
BEGIN
	UPDATE WD
	SET WD.YN_EXCLUDE_CPR = 'Y'
	FROM CZ_SA_GIRH_WORK_DETAIL WD
	WHERE WD.CD_COMPANY = @P_CD_COMPANY
	AND WD.NO_GIR = @V_NO_GIR	
END

IF EXISTS (SELECT 1 
		   FROM SA_GIRL GL
		   LEFT JOIN MM_EJTP JT ON JT.CD_COMPANY = GL.CD_COMPANY AND JT.CD_QTIOTP = GL.TP_GI
		   WHERE GL.CD_COMPANY = @P_CD_COMPANY
		   AND GL.NO_GIR = @V_NO_GIR
		   AND ISNULL(JT.YN_AM, '') = 'N')
BEGIN
	UPDATE GD
	SET GD.DC_RMK_CI = (CASE WHEN ISNULL(GD.DC_RMK_CI, '') = '' THEN '90번 대체품 수출로 진행 부탁드립니다.'
																ELSE ISNULL(GD.DC_RMK_CI, '') + CHAR(13) + CHAR(10) + '90번 대체품 수출로 진행 부탁드립니다.' END)
	FROM CZ_SA_GIRH_WORK_DETAIL GD
	WHERE GD.CD_COMPANY = @P_CD_COMPANY
    AND GD.NO_GIR = @V_NO_GIR
END

IF EXISTS (SELECT 1 
		   FROM CZ_SA_SOH_DLV SD
		   WHERE SD.CD_COMPANY = @P_CD_COMPANY
		   AND SD.NO_SO = @P_NO_SO
		   AND (ISNULL(SD.CD_CHARGE, '') = 'M' OR ISNULL(SD.YN_CHARGE, 'N') <> 'Y')
		   AND ISNULL(SD.YN_USE, 'N') = 'Y')
BEGIN
	INSERT INTO CZ_SA_GIRH_CHARGE
	(
		CD_COMPANY,
		NO_GIR,
		CD_ITEM,
		AM_EX_CHARGE,
		ID_INSERT,
		DTS_INSERT
	)
	SELECT SD.CD_COMPANY,
		   @V_NO_GIR AS NO_GIR,
		   'SD0001' AS CD_ITEM,
		   (CASE WHEN ISNULL(SD.YN_CHARGE, 'N') <> 'Y' THEN 0 ELSE SD.AM_CHARGE END) AS AM_EX_CHARGE,
		   'SYSTEM' AS ID_INSERT,
		   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
	FROM CZ_SA_SOH_DLV SD
	WHERE SD.CD_COMPANY = @P_CD_COMPANY
	AND SD.NO_SO = @P_NO_SO
	AND ISNULL(SD.YN_USE, 'N') = 'Y'
END

UPDATE SA_GIRH
SET STA_GIR = 'O',
    ID_UPDATE = @P_ID_USER,
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_GIR = @V_NO_GIR

;WITH A AS
(
    SELECT PH.NO_GIR,
    	   PH.NO_PACK,
    	   (CASE WHEN PL.QT_FILE = 1 THEN PL.NO_FILE ELSE PL.NO_FILE + ' 외' + CONVERT(NVARCHAR, PL.QT_FILE - 1) END) AS NO_FILE,
    	   (CASE PH.CD_TYPE WHEN '002' THEN '(P)' WHEN '003' THEN '(W)' ELSE '' END) + FORMAT(PH.QT_WIDTH, 'g15') + ' X ' + FORMAT(PH.QT_LENGTH, 'g15') + ' X ' + FORMAT(PH.QT_HEIGHT, 'g15') + ' (mm), ' + FORMAT(PH.QT_GROSS_WEIGHT, 'g15') + ' kg' AS DC_PACK
    FROM CZ_SA_PACKH PH
    JOIN CZ_SA_GIRH_PACK_DETAIL PD ON PD.CD_COMPANY = PH.CD_COMPANY AND PD.NO_GIR = PH.NO_GIR AND PD.CD_PACK_CATEGORY = '001'
    JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK,
    	  	     MAX(DISTINCT CASE WHEN SL.QT_SO > SL.QT_GI THEN PL.NO_FILE ELSE NULL END) AS NO_FILE,
    	  	     COUNT(DISTINCT PL.NO_FILE) AS QT_FILE
    	  FROM CZ_SA_PACKL PL
          JOIN SA_SOL SL ON SL.CD_COMPANY = PL.CD_COMPANY AND SL.NO_SO = PL.NO_FILE AND SL.SEQ_SO = PL.NO_QTLINE
    	  GROUP BY PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK) PL  
    ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR AND PL.NO_PACK = PH.NO_PACK
    JOIN CZ_SA_PACKL PL1 ON PL1.CD_COMPANY = PH.CD_COMPANY AND PL1.NO_GIR = PH.NO_GIR AND PL1.NO_PACK = PH.NO_PACK
    JOIN SA_SOL SL ON SL.CD_COMPANY = PL1.CD_COMPANY AND SL.NO_SO = PL1.NO_FILE AND SL.SEQ_SO = PL1.NO_QTLINE
    WHERE PH.CD_COMPANY = @P_CD_COMPANY
    AND PL1.NO_FILE = @P_NO_SO
),
B AS
(
    SELECT DISTINCT '[' + A.NO_GIR + '-' + CONVERT(NVARCHAR, A.NO_PACK) + '/' + A.NO_FILE + '] ' + A.DC_PACK AS DC_RMK_PACK
    FROM A
)
SELECT @V_DC_RMK3 = STRING_AGG(B.DC_RMK_PACK, ' ') 
FROM B

UPDATE WD
SET WD.DC_RMK3 = @V_DC_RMK3,
	WD.CD_TAEGBAE = (CASE WHEN MD.CD_PARTNER IN ('DLV230200077', 
						  					     'DLV230200076', 
						  					     'DLV230200075', 
						  					     'DLV230200054') AND WD.CD_MAIN_CATEGORY = '003' AND WD.CD_SUB_CATEGORY = '005' THEN NULL
						  WHEN WD.CD_MAIN_CATEGORY = '003' AND WD.CD_SUB_CATEGORY IN ('004', '005') THEN MD.CD_TAEGBAE ELSE NULL END),
	WD.DC_RMK_PL = (CASE WHEN (WD.CD_MAIN_CATEGORY = '003' AND WD.CD_SUB_CATEGORY = '004') THEN ISNULL(MD.LN_PARTNER, '') + ' ' + 
																							    ISNULL(MD.DC_ADDRESS, '') +  ' ' + ISNULL(MD.DC_ADDRESS1, '') + ' ' + 
																							    ISNULL(MD.NO_TEL, '') + ' ' +
																								ISNULL(MC.NM_SYSDEF, '') ELSE ISNULL(MD.LN_PARTNER, '') END),
    WD.DTS_SUBMIT = NEOE.SF_SYSDATE(GETDATE()),
    WD.ID_UPDATE = @P_ID_USER,
	WD.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_SA_GIRH_WORK_DETAIL WD
LEFT JOIN CZ_MA_DELIVERY_MAP DM ON DM.CD_COMPANY = WD.CD_COMPANY AND DM.CD_PARTNER_OLD = WD.CD_DELIVERY_TO
LEFT JOIN CZ_MA_DELIVERY MD ON MD.CD_COMPANY = WD.CD_COMPANY AND MD.CD_PARTNER = ISNULL(DM.CD_PARTNER_NEW, WD.CD_DELIVERY_TO)
LEFT JOIN CZ_SA_GIRH_REMARK GR ON GR.CD_COMPANY = WD.CD_COMPANY AND GR.NO_GIR = WD.NO_GIR
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = WD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00032' AND MC.CD_SYSDEF = WD.CD_RMK
WHERE WD.CD_COMPANY = @P_CD_COMPANY
AND WD.NO_GIR = @V_NO_GIR

UPDATE SD
SET SD.YN_GIR_AUTO = 'C',
	SD.NO_GIR = @V_NO_GIR
FROM CZ_SA_SOH_DLV SD
WHERE SD.CD_COMPANY = @P_CD_COMPANY
AND SD.NO_SO = @P_NO_SO
AND ISNULL(SD.YN_USE, 'N') = 'Y'

COMMIT TRAN SP_CZ_SA_GIR_AUTO_REG

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_SA_GIR_AUTO_REG
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN
ERROR: RAISERROR(@V_ERRMSG, 16, 1)

GO