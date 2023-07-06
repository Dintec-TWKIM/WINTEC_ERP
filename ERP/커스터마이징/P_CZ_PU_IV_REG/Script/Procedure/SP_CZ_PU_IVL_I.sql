USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_IVL_I]    Script Date: 2016-03-30 오후 1:48:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* 의뢰등록 P_PU_REQ_REG  추가*/                  
/*******************************************                  
**  System   : 구매/자재관리                  
**  Sub System  : 매입관리                  
**  Page    : 매입등록                  
**  Desc    : 매입등록                  
**  참   고   :                   
**  Return Values                  
**                  
**  작    성    자   :               
**  작    성    일   :             
**  수    정    자   :  SMR            
**  수    정    내   용  :             
*********************************************                  
** Change History                  
** 2009.09.17 시스템 통제 설정에 따른 CC 적용            
   000 기존데로            
   100 발주유형에 저장된 CC 적용            
   200 구매요청에 저장된 CC 적용            
*********************************************                  
*********************************************/              
ALTER PROCEDURE [NEOE].[SP_CZ_PU_IVL_I]                
(
	@NO_IV			NVARCHAR(20),             -- 마감번호                
	@NO_LINE		NUMERIC(5),               -- 마감항번                
	@CD_COMPANY		NVARCHAR(7),              -- 회사                
	@CD_PLANT		NVARCHAR(7),              -- 공장                
	@NO_IO			NVARCHAR(20),             -- 수불번호                
	@NO_IOLINE		NUMERIC(5),               -- 수불항번                
	@CD_ITEM		NVARCHAR(20),             -- 품목                
	@CD_PURGRP		NVARCHAR(7),              -- 구매그룹                
	@CD_CC			NVARCHAR(12),             -- CC코드                
	@DT_TAX			NCHAR(8),                 -- 마감일                        
	@QT_RCV_CLS		NUMERIC(17,4),            -- 마감수량                
	@UM_ITEM_CLS	NUMERIC(15,4),            -- 마감단가                
	@AM_CLS			NUMERIC(17,4),            -- 마감금액                
	@VAT			NUMERIC(17,4),            -- 부가세                
	@NO_EMP			NVARCHAR(10),             -- 담당자                
	@CD_PJT			NVARCHAR(20),             -- 프로젝트                        
	@FG_TPPURCHASE  NCHAR(3),                 -- 매입형태                
	@NO_PO			NVARCHAR(20),             -- 발주번호                
	@NO_POLINE		NUMERIC(5),               -- 발주항번                
	@CD_EXCH		NVARCHAR(3),              -- 환종                
	@RT_EXCH		NUMERIC(17,4),            -- 환율                
	@YN_RETURN		NCHAR(1),                 -- 반품유무                
	@UM_EX_CLS		NUMERIC(17,4),            -- 마감단가(재고단위)                
	@AM_EX_CLS		NUMERIC(17,4),            -- 마감금액(재고단위)                
	@QT_CLS			NUMERIC(17,4),            -- 마감수량(재고단위)                
	@NO_LC			NVARCHAR(20),             -- LC번호                
	@NO_LCLINE		NUMERIC(5),               -- LC항번                
	@CD_QTIOTP		NCHAR(3),                 -- 수불형태                
	@YN_PURSUB		NCHAR(1),                 -- 구매/외주 구분                
	@CD_PARTNER		NVARCHAR(20),             -- 거래처                        
	@CD_BIZAREA		NVARCHAR(7),              -- 사업장코드                        
	@FG_TRANS		NCHAR(3),                 -- 거래구분                
	@SEQ_PROJECT	NUMERIC(5,0),             -- SEQ_PROJECT
	@NO_WBS			NVARCHAR(20) ,            -- NO_WBS
	@NO_CBS			NVARCHAR(20),             -- NO_CBS
	@TP_UM_TAX		NVARCHAR(3) = '002' 
)                
AS                
DECLARE @ERRNO                    INT,                
@ERRMSG                   VARCHAR(255),            
@CD_CC_N       VARCHAR(12),            
@CD_EXC       VARCHAR(3)--,            
      
--@CD_CC_R      NVARCHAR(24)            
                
--반품이면                         
IF( @YN_RETURN = 'Y')                
 BEGIN
  SET @UM_ITEM_CLS = -(@UM_ITEM_CLS)
  SET @UM_EX_CLS = -(@UM_EX_CLS)                
  SET @AM_CLS = -(@AM_CLS)                
  SET @AM_EX_CLS = -(@AM_EX_CLS)                
  SET @QT_CLS = -(@QT_CLS)                
  SET @QT_RCV_CLS = -(@QT_RCV_CLS)                
  SET @VAT = -(@VAT)                
 END                
        
-- 발주라인의 CC를 적용받는다.        
 SELECT @CD_CC_N = A.CD_CC          
   FROM PU_POL A          
    WHERE A.CD_COMPANY =  @CD_COMPANY          
      AND A.NO_PO = @NO_PO          
      AND A.NO_LINE = @NO_POLINE        
      
  IF LEN(ISNULL(@CD_CC_N,'')) > 0       
     SET @CD_CC = @CD_CC_N      
    
 IF (@CD_CC = '010900' AND @NO_PO LIKE 'ST%' AND EXISTS (SELECT 1 
                                                         FROM MA_PITEM MI
                                                         WHERE MI.CD_COMPANY = @CD_COMPANY
                                                         AND MI.CD_ITEM = @CD_ITEM
                                                         AND MI.CLS_ITEM = '010'))
 BEGIN
    SET @CD_CC = '010301'
 END

 IF (@CD_CC IS NULL )                        
 BEGIN                  
       SELECT @ERRNO  = 100000, @ERRMSG = 'CC가 NULL 입니다. 등록 할 수 없습니다.'     
         GOTO ERROR                  
 END
                     
/*            
SELECT @CD_EXC = CD_EXC             
  FROM MA_EXC             
 WHERE CD_COMPANY = @CD_COMPANY            
   AND EXC_TITLE = '발주-C/C설정'            
   AND CD_MODULE = 'PU'            
            
            
IF @CD_EXC = '100'  -- 발주유형DML             
BEGIN            
 SELECT @CD_CC = B.CD_CC            
      FROM PU_POH A            
           INNER JOIN PU_TPPO B ON A.CD_TPPO = B.CD_TPPO AND A.CD_COMPANY = B.CD_COMPANY            
    WHERE A.CD_COMPANY =  @CD_COMPANY            
      AND A.NO_PO = @NO_PO            
            
             
END             
ELSE IF @CD_EXC = '200' -- 구매요청에서 CD_CC            
BEGIN            
 IF ( SELECT LEN(ISNULL(NO_PR,''))            
    FROM PU_POL A            
  WHERE A.CD_COMPANY =  @CD_COMPANY            
    AND A.NO_PO = @NO_PO             
    AND A.NO_LINE = @NO_POLINE )  > 0            
 BEGIN            
  SELECT @CD_CC = C.CD_CC            
    FROM PU_POL A            
      INNER JOIN PU_PRL B ON A.NO_PR = B.NO_PR AND A.NO_PRLINE = B.NO_PRLINE AND A.CD_COMPANY = B.CD_COMPANY            
               LEFT OUTER JOIN MA_PURGRP C ON B.CD_PURGRP = C.CD_PURGRP AND B.CD_COMPANY = C.CD_COMPANY            
  WHERE A.CD_COMPANY =  @CD_COMPANY            
    AND A.NO_PO = @NO_PO             
    AND A.NO_LINE = @NO_POLINE            
 END            
            
END            
*/          
          
            
-- 매입등록 LINE 추가                
INSERT INTO PU_IVL                
  (NO_IV, NO_LINE, CD_COMPANY, CD_PLANT, NO_IO, NO_IOLINE, CD_ITEM,                 
  CD_PURGRP, CD_CC, DT_TAX, QT_RCV_CLS, UM_ITEM_CLS, AM_CLS, VAT, NO_EMP, CD_PJT,                 
  FG_TPPURCHASE,NO_PO,NO_POLINE,CD_EXCH,RT_EXCH,YN_RETURN,UM_EX_CLS,AM_EX_CLS,                
  QT_CLS,NO_LC,NO_LCLINE,SEQ_PROJECT,NO_WBS,NO_CBS , TP_UM_TAX )                
VALUES                
  (@NO_IV, @NO_LINE, @CD_COMPANY, @CD_PLANT, @NO_IO, @NO_IOLINE, @CD_ITEM,                 
  @CD_PURGRP, @CD_CC, @DT_TAX, @QT_RCV_CLS, @UM_ITEM_CLS, @AM_CLS, @VAT, @NO_EMP, @CD_PJT,                 
  @FG_TPPURCHASE,@NO_PO,@NO_POLINE,@CD_EXCH,@RT_EXCH,@YN_RETURN,@UM_EX_CLS,@AM_EX_CLS,                
  @QT_CLS,@NO_LC,@NO_LCLINE,@SEQ_PROJECT,@NO_WBS,@NO_CBS, @TP_UM_TAX )                
                
---------------------------------------------------------------------------------------------------------------------                
/* PU_IVL.TI_PU_INV 트리거로 옮김                
-- 구매이면                 
IF( @YN_PURSUB ='N')                
BEGIN                
--매입실적 잡음                
EXEC UP_PU_AP_UPDATE        @CD_PARTNER,@CD_PURGRP,@CD_BIZAREA,@CD_COMPANY,@DT_TAX,@AM_CLS,@VAT,                
                                 @YN_RETURN, 'N',@CD_ITEM,@FG_TRANS,@CD_PLANT,@CD_QTIOTP,@QT_CLS                        
IF (@@ERROR <> 0 )                        
BEGIN                  
 SELECT @ERRNO  = 100000, @ERRMSG = 'CM_M100010'                  
 GOTO ERROR                  
END                                 
                
IF( @NO_IOLINE >0)                
BEGIN                
 -- 수불잔량관리                
 EXEC UP_PU_MM_QTIO_FROM_IVL_UPDATE @NO_IO, @NO_IOLINE,@CD_COMPANY,@QT_CLS,@AM_CLS,@VAT,@QT_RCV_CLS, 'N'                
 IF (@@ERROR <> 0 )                        
 BEGIN                  
         SELECT @ERRNO  = 100000, @ERRMSG = 'CM_M100010'                  
         GOTO ERROR                  
 END                         
END           
                                                                         
END                        
                
-------------------------------------------------------------------------------------------------------------------------                        
ELSE IF(@YN_PURSUB = 'O') -- 공정외주이면                
BEGIN                
--------------------------------------------------------------                
PRINT '생산쪽 프로시저 호출'                
--------------------------------------------------------------                
END                
*/            
RETURN                
ERROR:                  
    RAISERROR(@ERRMSG, 16, 1) -- 2012/08/13:자동화 변경[RAISERROR], 변경전:RAISERROR @ERRNO @ERRMSG

GO

