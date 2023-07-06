USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_PRL_I]    Script Date: 2021-03-12 오후 3:43:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************                        
*********************************************/                     
ALTER PROCEDURE [NEOE].[SP_CZ_PU_PRL_I]                        
(                        
	 @P_NO_PR			NVARCHAR(20),                  
	 @P_NO_PRLINE		NUMERIC(5),                  
	 @P_CD_COMPANY		NVARCHAR(7),                  
	 @P_CD_PLANT		NVARCHAR(7),                  
	 @P_CD_PURGRP		NVARCHAR(7),                  
	 @P_CD_ITEM			NVARCHAR(50),                  
	 @P_DT_LIMIT		NCHAR(8),                  
	 @P_QT_PR			NUMERIC(17, 4),                 
	 @P_QT_PO			NUMERIC(17, 4),
	 @P_QT_PO_MM	    NUMERIC(17, 4),
	 @P_FG_PRCON		NCHAR(3),                  
	 @P_CD_SL			NVARCHAR(7),                  
	 @P_FG_POST			NCHAR(3),
	 @P_NO_SO			NVARCHAR(20),    
	 @P_NO_SOLINE		NUMERIC(5, 0),
	 @P_RT_PO			NUMERIC(15, 4),
	 @P_ID_INSERT		NVARCHAR(14)
)                        
AS                        
DECLARE	@P_ERRMSG          VARCHAR(255),         
        @V_UNIT_PO_FACT    NUMERIC(15,4),
        @V_EXC_MENU		   NVARCHAR(3),
        @V_NEW_ID		   NVARCHAR(36)

IF(( SELECT ISNULL(YN_PMS,'N') FROM MA_ENV WHERE CD_COMPANY = @P_CD_COMPANY) = 'Y')
BEGIN
	SELECT @V_NEW_ID = NEWID()
END  

 --임시로 통제를 걸었다. 크라제때문에      
--나중에 통제를 넣는 방향으로 처리예정       
    
SELECT @V_UNIT_PO_FACT = ISNULL(UNIT_PO_FACT, 1)     
FROM MA_PITEM 
WHERE CD_ITEM = @P_CD_ITEM 
AND CD_PLANT = @P_CD_PLANT 
AND CD_COMPANY = @P_CD_COMPANY       
 
SELECT @V_EXC_MENU = ISNULL(CD_EXC,'000')  
FROM MA_EXC_MENU WHERE CD_COMPANY = @P_CD_COMPANY AND CD_TITLE = 'PU_A00000018' AND ID_MENU = 'P_PU_PR_REG'
 
IF (@V_EXC_MENU = '000' AND @V_UNIT_PO_FACT <> 0 AND CONVERT(NUMERIC(17,4),@V_UNIT_PO_FACT * @P_QT_PO_MM) <> @P_QT_PR )   --CONVERT로 수량을 계산을 맞추도록 수정함 20100709 최인성    
BEGIN   
	SELECT @P_ERRMSG = @P_CD_ITEM + '  ' + CONVERT(NVARCHAR, @P_QT_PR) + '   ' + CONVERT(NVARCHAR, (CONVERT(NUMERIC(17, 4), @V_UNIT_PO_FACT * @P_QT_PO_MM))) + '    ' + @P_CD_ITEM + '발주단위 수량과 재고단위 수량이 일치 하지 않습니다.'                
    GOTO ERROR         
END

INSERT INTO PU_PRL    
(
	CD_COMPANY,
	CD_PLANT,
	NO_PR,                  
	NO_PRLINE,
    CD_PURGRP,
	CD_ITEM,
	DT_LIMIT,
	QT_PR,
	QT_PO,
	QT_PO_MM,
	FG_PRCON,
	CD_SL,
	FG_POST,
	NO_SO,
	NO_SOLINE,
	RT_PO,
	ID_INSERT,
	DTS_INSERT
)                        
VALUES             
(
	@P_CD_COMPANY,
	@P_CD_PLANT,
	@P_NO_PR,                  
	@P_NO_PRLINE,
    @P_CD_PURGRP,
	@P_CD_ITEM,
	@P_DT_LIMIT,
	CEILING(@P_QT_PR),
	@P_QT_PO,
	@P_QT_PO_MM,
	@P_FG_PRCON,
	@P_CD_SL,
	@P_FG_POST,
	@P_NO_SO,
	@P_NO_SOLINE,
	@P_RT_PO,
	@P_ID_INSERT,           
    NEOE.SF_SYSDATE(GETDATE())
)                        
            
-- LOG 데이터 생성            
INSERT INTO PU_PRL_LOG            
(
	NO_PR,
	NO_PRLINE,
	CD_COMPANY,
	CD_PLANT,
	CD_PURGRP,
	CD_ITEM,
	DT_LIMIT,
	QT_PR,
	QT_PO,
	FG_PRCON,
	CD_SL,
	FG_POST,
	NO_CONTRACT,
	NO_CTLINE,
	QT_APP,
	DC_LINE_RMK,
	CD_PARTNER,
	CD_EXCH,
	RT_EXCH,
	UM_EX_PO,
	RT_PO,
	UM_EX,
	AM_EX,
	AM,
	CD_TPPO,
	CD_CC,
	CD_BUDGET,
	CD_BGACCT,
	QT_PO_MM,
	DTS_INSERT,
	ID_INSERT,
	DTS_UPDATE,
	ID_UPDATE,
	FG_PROCESS,
	DTS_PROCESS,
	ID_PROCESS,
	DT_PLAN
)            
SELECT NO_PR,
	   NO_PRLINE,
	   CD_COMPANY,
	   CD_PLANT,
	   CD_PURGRP,
	   CD_ITEM,
	   DT_LIMIT,
	   QT_PR,
	   QT_PO,
	   FG_PRCON,
	   CD_SL,
	   FG_POST,
	   NO_CONTRACT,
	   NO_CTLINE,
	   QT_APP,
	   DC_LINE_RMK,
	   CD_PARTNER,
	   CD_EXCH,
	   RT_EXCH,
	   UM_EX_PO,
	   RT_PO,
	   UM_EX,
	   AM_EX,
	   AM,
	   CD_TPPO,
	   CD_CC,
	   CD_BUDGET,
	   CD_BGACCT,
	   QT_PO_MM,
	   DTS_INSERT,
	   ID_INSERT,
	   DTS_UPDATE,
	   ID_UPDATE,
	   'INSERT',
	   NEOE.SF_SYSDATE(GETDATE()),
	   @P_ID_INSERT,
	   DT_PLAN           
FROM PU_PRL            
WHERE NO_PR = @P_NO_PR            
AND NO_PRLINE = @P_NO_PRLINE            
AND CD_COMPANY = @P_CD_COMPANY   
                                  
RETURN      
ERROR: RAISERROR (@P_ERRMSG, 18 , 1)                     

GO