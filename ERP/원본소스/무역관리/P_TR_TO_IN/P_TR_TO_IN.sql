IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_TO_IN_INSERT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_TO_IN_INSERT
GO 
/**********************************************************************************************************  
 * 프로시저명: UP_TR_TO_IN_INSERT
 * 관련페이지: 
 * 작  성  자: 
 * EXEC UP_TR_TO_IN_INSERT '',''
 *********************************************************************************************************/
    
CREATE PROC UP_TR_TO_IN_INSERT
(  
	@P_NO_TO   NVARCHAR(20),  
	@P_CD_COMPANY   NVARCHAR(7),  
	@P_CD_PURGRP   NVARCHAR(7),  
	@P_CD_PARTNER   NVARCHAR(7),  
	@P_NO_SCBL    NVARCHAR(20),  
	@P_CD_BANK    NVARCHAR(7),  
	@P_DT_TO    NCHAR(8),  
	@P_FG_LC     NCHAR(3),  
	@P_CD_EXCH    NVARCHAR(3),  
	@P_RT_EXCH    NUMERIC(11,4),  
	@P_AM_EX     NUMERIC(17,4),  
	@P_AM_LICENSE    NUMERIC(17,4),  
	@P_AM      NUMERIC(17,4),  
	@P_COND_PRICE    NCHAR(3),  
	@P_CD_CUSTOMS   NVARCHAR(7),  
	@P_NO_LICENSE   NVARCHAR(20),  
	@P_DT_LICENSE    NCHAR(8),  
	@P_NO_INSP    NVARCHAR(20),  
	@P_DT_INSP    NCHAR(8),  
	@P_NO_QUAR    NVARCHAR(20),  
	@P_DT_QUAR    NCHAR(8),  
	@P_REMARK    NVARCHAR(100),  
	@P_TOT_WEIGHT    NUMERIC(17,4),  
	@P_CD_UNIT    NVARCHAR(3),  
	@P_WEIGHT     NUMERIC(17,4),  
	@P_YN_DISTRIBU   NCHAR(1),  
	@P_ID_INSERT   NVARCHAR(15),  
	@P_NO_EMP    NVARCHAR(10),  
	@P_NO_COST    NVARCHAR(20),  
	@P_DT_DISTRIBU   NCHAR(8)  
)  
AS  
BEGIN  
	 DECLARE  
	 @ERRNO       INT,          
	 @ERRMSG     VARCHAR(255),  
	 @P_DTS_INSERT  VARCHAR(14)  
	 SET @P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')   
	
	 IF EXISTS ( SELECT TOP 1 NO_TO FROM TR_TO_IMH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_TO = @P_NO_TO )  
	 BEGIN  
	 SELECT @ERRNO  = 100000, @ERRMSG = ' 해당 [' + @P_NO_TO + '] 번호가 중복되어 저장할 수 없습니다.                  [통관 현황]을 통해 확인해주십시요' GOTO ERROR      
	 END  
	  
	 INSERT INTO TR_TO_IMH (NO_TO, CD_COMPANY, CD_PURGRP, CD_PARTNER, NO_SCBL, CD_BANK, DT_TO, FG_LC, CD_EXCH,   
	  RT_EXCH, AM_EX, AM_LICENSE, AM, COND_PRICE, CD_CUSTOMS, NO_LICENSE, DT_LICENSE, NO_INSP,   
	  DT_INSP, NO_QUAR, DT_QUAR, REMARK, TOT_WEIGHT, CD_UNIT, WEIGHT, YN_DISTRIBU, ID_INSERT, DTS_INSERT,   
	  NO_EMP, NO_COST, DT_DISTRIBU)  
	 VALUES (@P_NO_TO, @P_CD_COMPANY, @P_CD_PURGRP, @P_CD_PARTNER, @P_NO_SCBL, @P_CD_BANK, @P_DT_TO, @P_FG_LC, @P_CD_EXCH,   
	  @P_RT_EXCH, @P_AM_EX, @P_AM_LICENSE, @P_AM, @P_COND_PRICE, @P_CD_CUSTOMS, @P_NO_LICENSE, @P_DT_LICENSE, @P_NO_INSP,   
	  @P_DT_INSP, @P_NO_QUAR, @P_DT_QUAR, @P_REMARK, @P_TOT_WEIGHT, @P_CD_UNIT, @P_WEIGHT, @P_YN_DISTRIBU, @P_ID_INSERT, @P_DTS_INSERT,   
	  @P_NO_EMP, @P_NO_COST, @P_DT_DISTRIBU )  
	 IF (@@ERROR <> 0 )  
	 BEGIN    
		   SELECT @ERRNO  = 100000,    
						@ERRMSG = 'CM_M100010'    
		   GOTO ERROR    
	 END      
	  
	 UPDATE TR_BL_IMH  
	 SET YN_ENTRY = 'Y' , DT_ENTRY = @P_DT_TO  
	 WHERE NO_BL = @P_NO_SCBL AND CD_COMPANY = @P_CD_COMPANY  
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



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_TO_IN_UPDATE') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_TO_IN_UPDATE
GO 
/**********************************************************************************************************  
 * 프로시저명: UP_TR_TO_IN_UPDATE
 * 관련페이지: 
 * 작  성  자: 
 * EXEC UP_TR_TO_IN_UPDATE '',''
 *********************************************************************************************************/
CREATE  PROC UP_TR_TO_IN_UPDATE
(  
	@NO_TO   NVARCHAR(20),  
	@CD_COMPANY   NVARCHAR(7),  
	@CD_PURGRP   NVARCHAR(7),  
	@CD_PARTNER   NVARCHAR(7),  
	@NO_SCBL   NVARCHAR(20),  
	@CD_BANK   NVARCHAR(7),  
	@DT_TO   NCHAR(8),  
	@FG_LC    NCHAR(3),  
	@CD_EXCH   NVARCHAR(3),  
	@RT_EXCH   NUMERIC(11,4),  
	@AM_EX    NUMERIC(17,4),  
	@AM_LICENSE    NUMERIC(17,4),  
	@AM     NUMERIC(17,4),  
	@COND_PRICE    NCHAR(3),  
	@CD_CUSTOMS   NVARCHAR(7),  
	@NO_LICENSE   NVARCHAR(20),  
	@DT_LICENSE    NCHAR(8),  
	@NO_INSP   NVARCHAR(20),  
	@DT_INSP    NCHAR(8),  
	@NO_QUAR   NVARCHAR(20),  
	@DT_QUAR    NCHAR(8),  
	@REMARK   NVARCHAR(100),  
	@TOT_WEIGHT    NUMERIC(17,4),  
	@CD_UNIT    NVARCHAR(3),  
	@WEIGHT    NUMERIC(17,4),  
	@YN_DISTRIBU    NCHAR(1),   
	@ID_UPDATE   NVARCHAR(15),  
	@NO_EMP   NVARCHAR(10),  
	@NO_COST   NVARCHAR(20),  
	@DT_DISTRIBU   NCHAR(8) = ''  
)  
AS  
BEGIN  
	 DECLARE  @ERRNO      INT,   @ERRMSG    VARCHAR(255)  
	 DECLARE @DTS_UPDATE VARCHAR(14)  
	   
	 SET @DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')   

	 UPDATE  TR_TO_IMH  
	 SET  DT_TO = @DT_TO,  
	  RT_EXCH = @RT_EXCH, AM_EX = @AM_EX,AM_LICENSE = @AM_LICENSE, AM = @AM, COND_PRICE = @COND_PRICE, CD_CUSTOMS = @CD_CUSTOMS, NO_LICENSE = @NO_LICENSE, DT_LICENSE = @DT_LICENSE, NO_INSP =@NO_INSP,   
	  DT_INSP = @DT_INSP, NO_QUAR = @NO_QUAR, DT_QUAR = @DT_QUAR, REMARK = @REMARK, TOT_WEIGHT = @TOT_WEIGHT, CD_UNIT = @CD_UNIT, WEIGHT = @WEIGHT,   
	  DTS_UPDATE = @DTS_UPDATE, NO_EMP = @NO_EMP  
	 WHERE NO_TO = @NO_TO AND CD_COMPANY = @CD_COMPANY  
	  
	 IF (@@ERROR <> 0 )  
	 BEGIN    
		   SELECT @ERRNO  = 100000,    
						@ERRMSG = 'CM_M100010'    
		   GOTO ERROR    
	 END      
	
	 UPDATE TR_BL_IMH  
	 SET YN_ENTRY = 'Y' , DT_ENTRY = @DT_TO  
	 WHERE NO_BL = @NO_SCBL AND CD_COMPANY = @CD_COMPANY  
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
  
  

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_TO_IN_DELETE') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_TO_IN_DELETE
GO 
/**********************************************************************************************************  
 * 프로시저명: UP_TR_TO_IN_DELETE
 * 관련페이지: 
 * 작  성  자: 
 * EXEC UP_TR_TO_IN_DELETE '',''
 *********************************************************************************************************/ 
CREATE PROC UP_TR_TO_IN_DELETE
(  
 @NO_TO  NVARCHAR(20),  
 @CD_COMPANY  NVARCHAR(7)  
)  
AS  
BEGIN  
	DECLARE @ERRNO      INT,  
			@ERRMSG    VARCHAR(255),  
			@NO_SCBL  NVARCHAR(20),  
			@NO_COST  NVARCHAR(20),  
			@YN_DISTRIBU  NCHAR(1),  
			@NO_LINE  NUMERIC(3)  

	 -- 라인 삭제  
	 DELETE FROM TR_TO_IML WHERE NO_TO = @NO_TO AND CD_COMPANY = @CD_COMPANY  
	 IF (@@ERROR <> 0 )  
	 BEGIN    
		   SELECT @ERRNO  = 100000,    
				  @ERRMSG = 'CM_M100010'    
		   GOTO ERROR    
	 END      
   
	 DELETE FROM TR_TO_IMH WHERE NO_TO = @NO_TO AND CD_COMPANY = @CD_COMPANY  
	 IF (@@ERROR <> 0 )  
	 BEGIN    
		   SELECT @ERRNO  = 100000,    
						@ERRMSG = 'CM_M100010'    
		   GOTO ERROR    
	 END     
   
	 UPDATE TR_BL_IMH  
	 SET YN_ENTRY = 'N' , DT_ENTRY = '0000000'  
	 WHERE NO_BL = @NO_SCBL AND CD_COMPANY = @CD_COMPANY  
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
  
  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_TO_LINE_INSERT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_TO_LINE_INSERT
GO 
/**********************************************************************************************************  
 * 프로시저명: UP_TR_TO_LINE_INSERT
 * 관련페이지: 
 * 작  성  자: 
 * EXEC UP_TR_TO_LINE_INSERT '',''
 *********************************************************************************************************/   
CREATE PROC UP_TR_TO_LINE_INSERT  
(  
	@NO_TO   NVARCHAR(20),  
	@NO_LINE   NUMERIC(3),  
	@CD_COMPANY    NVARCHAR(7),  
	@NO_BL    NVARCHAR(20),  
	@NO_BLLINE    NUMERIC(3),  
	@CD_ITEM    NVARCHAR(20),  
	@QT_TO    NUMERIC(17,4),  
	@CD_UNIT_MM   NVARCHAR(3),  
	@QT_TO_MM    NUMERIC(17,4),  
	@UM_EX_PO    NUMERIC(15,4),  
	@UM_EX    NUMERIC(15,4),  
	@AM_EX    NUMERIC(17,4),  
	@UM     NUMERIC(15,4),  
	@AM     NUMERIC(17,4),  
	@CD_PJT    NVARCHAR(20),  
	@YN_PURCHASE   NCHAR(1),  
	@FG_TPPURCHASE   NCHAR(3),  
	@CD_QTIOTP    NCHAR(3),  
	@RT_CUSTOMS    NUMERIC(5,2),  
	@RT_SPEC     NUMERIC(5,2),  
	@YN_AUTORCV   NCHAR(1),  
	@CD_PLANT    NVARCHAR(20),  
	@CD_SL    NVARCHAR(20),  
	@DT_DELIVERY   NCHAR(8)  
)  
AS  
BEGIN  
	DECLARE @ERRNO      INT,   -- ERROR 번호  
			@ERRMSG     VARCHAR(255)  -- ERROR 메시지   
  
	INSERT INTO TR_TO_IML (NO_TO, NO_LINE, CD_COMPANY, NO_BL, NO_BLLINE, CD_ITEM, QT_TO, CD_UNIT_MM, QT_TO_MM,   
	UM_EX_PO, UM_EX, AM_EX, UM, AM, CD_PJT, YN_PURCHASE, FG_TPPURCHASE, CD_QTIOTP,   
	RT_CUSTOMS, RT_SPEC, YN_AUTORCV, CD_PLANT, CD_SL, DT_DELIVERY )  
	VALUES (@NO_TO, @NO_LINE, @CD_COMPANY, @NO_BL, @NO_BLLINE, @CD_ITEM, @QT_TO, @CD_UNIT_MM, @QT_TO_MM,   
	@UM_EX_PO, @UM_EX, @AM_EX, @UM, @AM, @CD_PJT, @YN_PURCHASE, @FG_TPPURCHASE, @CD_QTIOTP,   
	@RT_CUSTOMS, @RT_SPEC, @YN_AUTORCV, @CD_PLANT, @CD_SL, @DT_DELIVERY)  
  
	IF (@@ERROR <> 0 )  
	BEGIN    
	SELECT @ERRNO  = 100000,    
		@ERRMSG = 'CM_M100010'    
	GOTO ERROR    
	END     
  
	EXEC UP_TR_TOIMH_FROM_TO_UPDATE_2 @NO_TO,  @CD_COMPANY   
  
	IF (@@ERROR <> 0 )  
	BEGIN    
	SELECT @ERRNO  = 100000,    
		@ERRMSG = 'Header Update에 문제가 있습니다.'    
	GOTO ERROR    
	END     

	RETURN  
	ERROR:    
		RAISERROR @ERRNO @ERRMSG     
END
GO  
  
  
  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'SP_TR_BL_SELECT_SAVE') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE SP_TR_BL_SELECT_SAVE
GO 
/**********************************************************************************************************  
 * 프로시저명: SP_TR_BL_SELECT_SAVE
 * 관련페이지: 
 * 작  성  자: 
 * EXEC SP_TR_BL_SELECT_SAVE '',''
 *********************************************************************************************************/   
CREATE PROC SP_TR_BL_SELECT_SAVE          
(          
	@CD_COMPANY  NVARCHAR(7),          
	@NO_BL   NVARCHAR(20)          
)          
AS
BEGIN          
	-- [ 통관등록메인 화면]에서 BL내역과 [통관내역등록]을 일치시켜 자동저장하기 위해서 이 SP 호출  
	SELECT B.NO_LINE,  
		B.NO_BL,      
		B.NO_LINE AS NO_BLLINE,      
		B.CD_ITEM,       
		B.QT_BL AS QT_TO,    -- 2007.5.31 수정      
		B.CD_UNIT_MM,        
		B.QT_BL_MM AS QT_TO_MM, -- 2007.5.31 수정     
		B.UM_EX_PO,       
		B.UM_EX,       
		(B.AM_EX - B.AM_EXTO) AM_EX,        
		B.UM,      
		(B.AM - B.AM_TO) AM,        
		B.CD_PJT,      
		CASE ISNULL(B.FG_TPPURCHASE,'0') WHEN '0' THEN 'N' ELSE 'Y' END  YN_PURCHASE,      
		B.FG_TPPURCHASE,      
		B.CD_QTIOTP,      
		B.YN_AUTORCV,      
		B.CD_PLANT,      
		B.CD_SL,      
		B.DT_DELIVERY      
	FROM  TR_BL_IMH A  
	INNER JOIN TR_BL_IML B ON B.CD_COMPANY = @CD_COMPANY AND B.NO_BL = A.NO_BL     
	WHERE A.CD_COMPANY = @CD_COMPANY 
	AND A.NO_BL = @NO_BL
END
GO  


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_TO_NO_SELECT_VIEW') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_TO_NO_SELECT_VIEW
GO 
/**********************************************************************************************************  
**  System : 무역                      
**  Sub System : 수입                      
**  Page  : 통관등록(P_TR_BL)                      
**  Desc  : 통관등록 프리폼 초기화에만 쓰임  
 *********************************************************************************************************/       
      
CREATE PROC UP_TR_TO_NO_SELECT_VIEW     
(        
	@P_CD_COMPANY  NVARCHAR(7),     
	@P_NO_TO    NVARCHAR(20)       
)        
AS        
BEGIN        
	SELECT        
	   DISTINCT TTI.NO_TO,  -- 통관번호      
	   TTI.CD_COMPANY,      
	   TTI.CD_PURGRP,  -- 구매그룹      
	   TTI.CD_PARTNER,  -- 거래처      
	   TTI.NO_SCBL,       
	   TTI.CD_BANK,    -- 거래은행      
	   TTI.DT_TO,     -- 신고일      
	   TTI.FG_LC,    -- LC구분      
	   TTI.CD_EXCH,  -- 통화      
	   TTI.RT_EXCH, -- 면장환율      
	   TTI.AM_EX,      
	   TTI.AM_LICENSE, -- 면장금액      
	   TTI.AM,      
	   TTI.COND_PRICE,   -- 가격조건      
	   TTI.CD_CUSTOMS,  -- 관할세관      
	   TTI.NO_LICENSE, -- 면장번호      
	   TTI.DT_LICENSE, -- 면허일      
	   TTI.NO_INSP, -- 검사증번호      
	   TTI.DT_INSP, -- 검사일      
	   TTI.NO_QUAR,   -- 검역증 번호      
	   TTI.DT_QUAR, -- 검역일      
	   TTI.REMARK,    -- 비고      
	   TTI.TOT_WEIGHT, -- 총중량      
	   TTI.CD_UNIT, -- 총중량 단위      
	   TTI.WEIGHT, -- 순중량      
	   TTI.YN_DISTRIBU, -- 정산상태      
	   TTI.NO_EMP,      
	   TTI.ID_INSERT,        
	   TTI.DTS_INSERT,      
	   TTI.ID_UPDATE,      
	   TTI.DTS_UPDATE,      
	   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = TTI.CD_COMPANY AND CD_PARTNER = TTI.CD_PARTNER) LN_PARTNER,       
	   (SELECT NM_KOR     FROM MA_EMP     WHERE CD_COMPANY = TTI.CD_COMPANY AND NO_EMP = TTI.NO_EMP) NM_KOR,       
	   (SELECT NM_PURGRP  FROM MA_PURGRP   WHERE TTI.CD_PURGRP = CD_PURGRP AND TTI.CD_COMPANY = CD_COMPANY ) NM_PURGRP,       
	   (SELECT LN_PARTNER FROM MA_PARTNER  WHERE TTI.CD_BANK = CD_PARTNER  AND TTI.CD_COMPANY = CD_COMPANY  ) NM_BANK,       
	   (SELECT NM_SYSDEF  FROM MA_CODEDTL  WHERE TTI.CD_EXCH = CD_SYSDEF  AND TTI.CD_COMPANY  = CD_COMPANY AND CD_FIELD = 'MA_B000005' ) NM_EXCH,      
	   (SELECT LN_PARTNER FROM MA_PARTNER  WHERE TTI.CD_CUSTOMS = CD_PARTNER  AND TTI.CD_COMPANY = CD_COMPANY   ) NM_CUSTOMS,           
	   (SELECT NO_SCLC       FROM  TR_BL_IMH   WHERE TTI.CD_COMPANY = CD_COMPANY AND TTI.NO_SCBL = NO_BL  ) NO_SCLC,      
	   TTI.NO_COST,      
	   TTI.DT_DISTRIBU,        
	   (SELECT TOP 1 NO_TO FROM PU_RCVL WHERE TTI.NO_TO = NO_TO  AND TTI.CD_COMPANY = CD_COMPANY  ) RCVL,      
	   D.CD_CC,                    
	   H.NM_CC,  
	   CASE WHEN ( SELECT TOP 1 NO_TO FROM TR_TO_IML WHERE CD_COMPANY = TTI.CD_COMPANY AND NO_TO = TTI.NO_TO ) IS NOT NULL THEN 'Y' ELSE 'N' END AS EXIST_NO_TO                  
	FROM TR_TO_IMH AS TTI         
	LEFT OUTER JOIN MA_PURGRP D ON D.CD_COMPANY = @P_CD_COMPANY AND D.CD_PURGRP = TTI.CD_PURGRP                  
	LEFT OUTER JOIN MA_CC H     ON H.CD_COMPANY = @P_CD_COMPANY AND H.CD_CC = D.CD_CC                      
	WHERE TTI.CD_COMPANY = @P_CD_COMPANY 
	AND TTI.NO_TO = @P_NO_TO      
END
GO

   

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_TO_IN_DELETE') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_TO_IN_DELETE
GO 
/**********************************************************************************************************  
 * 프로시저명: UP_TR_TO_IN_DELETE
 * 관련페이지: 
 * 작  성  자: 
 * EXEC UP_TR_TO_IN_DELETE '',''
 *********************************************************************************************************/ 
CREATE  PROC UP_TR_TO_IN_DELETE  
(  
	@NO_TO  NVARCHAR(20),  
	@CD_COMPANY  NVARCHAR(7)  
)  
AS  
BEGIN  
	DECLARE	@ERRNO      INT,  
			@ERRMSG    VARCHAR(255),  
			@NO_SCBL  NVARCHAR(20),  
			@NO_COST  NVARCHAR(20),  
			@YN_DISTRIBU  NCHAR(1),  
			@NO_LINE  NUMERIC(3)  

	-- 라인 삭제  
	DELETE FROM TR_TO_IML WHERE NO_TO = @NO_TO AND CD_COMPANY = @CD_COMPANY  
	IF (@@ERROR <> 0 )  
	BEGIN    
	   SELECT @ERRNO  = 100000,    
			  @ERRMSG = 'CM_M100010'    
	   GOTO ERROR    
	END      
   
	DELETE FROM TR_TO_IMH WHERE NO_TO = @NO_TO AND CD_COMPANY = @CD_COMPANY  
	IF (@@ERROR <> 0 )  
	BEGIN    
	   SELECT @ERRNO  = 100000,    
					@ERRMSG = 'CM_M100010'    
	   GOTO ERROR    
	END     
  
	UPDATE TR_BL_IMH  
	SET YN_ENTRY = 'N' , DT_ENTRY = '0000000'  
	WHERE NO_BL = @NO_SCBL AND CD_COMPANY = @CD_COMPANY  
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
