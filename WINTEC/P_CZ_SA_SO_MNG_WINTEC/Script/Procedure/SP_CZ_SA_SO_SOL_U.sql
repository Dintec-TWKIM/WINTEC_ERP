USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SO_SOL_U]    Script Date: 2019-11-14 오후 3:12:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************    
*********************************************/    
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SO_SOL_U]    
(    
    @P_CD_COMPANY       NVARCHAR(7),         --회사    
    @P_NO_SO            NVARCHAR(20),        --수주번호    
    @P_SEQ_SO           NUMERIC(5,0),        --수주이력항번    
    @P_STA_SO           NVARCHAR(3),         --수주상태    
    @P_DC1              NVARCHAR(200),       --비고1    
    @P_DC2              NVARCHAR(200),       --비고2    
    @P_FG_USE           NVARCHAR(4),         --수주용도1    
    @P_FG_USE2          NVARCHAR(4),         --수주용도2
    @P_ID_UPDATE        NVARCHAR(15),        --UPDATE 한 사람  
    @P_TXT_USERDEF1		NVARCHAR(100) = NULL,
    @P_TXT_USERDEF2		NVARCHAR(100) = NULL,
    @P_TXT_USERDEF3		NVARCHAR(100) = NULL,
	@P_DT_EXPECT		NVARCHAR(8) = NULL,  --최초납기요구일
	@P_DT_DUEDATE		NVARCHAR(8) = NULL,	 --납기요구일
	@P_DT_REQGI			NVARCHAR(8) = NULL,	 --출하예정일
	@P_NUM_USERDEF3		NUMERIC(17, 4) = 0,	 --신규제작소요일
	@P_QT_SCORE			NUMERIC(17, 4) = 0,	 --납기준수율점수
    @P_TXT_USERDEF5     NVARCHAR(100),       --납품장소
    @P_CD_USERDEF3      NVARCHAR(3)          --엔진타입
)    
AS    
    
SET NOCOUNT ON    
  
DECLARE @P_DTS_UPDATE NVARCHAR(14)

SET  @P_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')          

DECLARE @V_COUNT INT, 
        @V_QT_GIR NUMERIC(17, 4),
        @V_CD_EXC NVARCHAR(3)

SELECT @V_CD_EXC = CD_EXC FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '업체별프로세스'

/* 수주 변경일(DTS_STA_HST),변경자(ID_STA_HST),변경전상태(STA_SO_HST) 추가 2010.03.02 */  
DECLARE  @V_STA_SO_HST NVARCHAR(3), @V_DTS_STA_HST NVARCHAR(14), @V_ID_STA_HST NVARCHAR(15)    
  
 SELECT  @V_STA_SO_HST = STA_SO,  @V_DTS_STA_HST = ISNULL(DTS_UPDATE, DTS_INSERT), @V_ID_STA_HST = ISNULL(ID_UPDATE, ID_INSERT )  
   FROM  SA_SOL   
  WHERE  CD_COMPANY = @P_CD_COMPANY  
    AND  NO_SO = @P_NO_SO  
    AND  SEQ_SO = @P_SEQ_SO  
   
   
    
IF (@P_STA_SO = 'O') -- 미정으로 변경시    
BEGIN    
    SELECT @V_COUNT = COUNT(*)    
      FROM TR_LCEXL    
     WHERE CD_COMPANY = @P_CD_COMPANY    
       AND NO_SO = @P_NO_SO    
       AND NO_LINE_SO = @P_SEQ_SO    
    
    IF (@V_COUNT > 0)    
    BEGIN     
       RAISERROR ('TR_M000089', 18, 1) --이미 LC등록 혹은 LC의뢰되어 수정, 삭제가 불가능합니다.    
       RETURN    
    END    
    
    SELECT @V_QT_GIR = QT_GIR    
      FROM  SA_SOL    
     WHERE  CD_COMPANY = @P_CD_COMPANY    
       AND  NO_SO = @P_NO_SO    
       AND  SEQ_SO = @P_SEQ_SO    
    
    IF (@V_QT_GIR > 0)     --의뢰수량이 0보다 큰면    
    BEGIN    
       RAISERROR ('SA_M000117', 18, 1) --이미 출하의뢰되어 수정, 삭제가 불가능합니다.    
       RETURN    
    END    
END    
ELSE IF (@P_STA_SO = 'C')    --종결로 변경시    
BEGIN    
    SELECT  @V_COUNT = COUNT(*)
      FROM  SA_SOL    
     WHERE  CD_COMPANY = @P_CD_COMPANY    
       AND  NO_SO = @P_NO_SO    
       AND  SEQ_SO = @P_SEQ_SO    
     --AND  QT_GIR > 0     --의뢰수량이 0보다 큰거만 가져온다.    
     --AND  QT_SO = QT_GI    --수주수량과 출하수량이 같은거만 가져온다.    
       AND STA_SO <> 'C'    
    
    IF (@V_COUNT = 0)    
    BEGIN    
        --해당 데이터는 계획, 확정, 종결이 포함되어 있으므로 종결할 수 없습니다.    
        --RAISERROR 100000 'SA_M000086'    
        RETURN    
    END    
END    

UPDATE SA_SOL    
SET STA_SO = @P_STA_SO,    
    DC1 = @P_DC1,    
    DC2 = @P_DC2,  
    FG_USE = @P_FG_USE,
    FG_USE2 = @P_FG_USE2, 
    DTS_UPDATE = @P_DTS_UPDATE,  
    ID_UPDATE = @P_ID_UPDATE,   
    STA_SO_HST = @V_STA_SO_HST,    
    DTS_STA_HST = @V_DTS_STA_HST,  
    ID_STA_HST = @V_ID_STA_HST,    
    TXT_USERDEF1 = @P_TXT_USERDEF1,
    TXT_USERDEF2 = @P_TXT_USERDEF2,
    TXT_USERDEF3 = @P_TXT_USERDEF3,
    TXT_USERDEF5 = @P_TXT_USERDEF5,
    CD_USERDEF3 = @P_CD_USERDEF3,
	DT_EXPECT = @P_DT_EXPECT,
	DT_DUEDATE = @P_DT_DUEDATE,
	DT_REQGI = @P_DT_REQGI,
	NUM_USERDEF3 = @P_NUM_USERDEF3,
	NUM_USERDEF4 = @P_QT_SCORE
WHERE CD_COMPANY  = @P_CD_COMPANY    
AND NO_SO = @P_NO_SO    
AND SEQ_SO = @P_SEQ_SO

IF (@P_STA_SO = 'R' AND @V_CD_EXC = '009')
BEGIN
    DECLARE @V_CD_ITEM      NVARCHAR(20),
            @V_CD_PARTNER   NVARCHAR(20),
            @V_TP_PRICE     NVARCHAR(3),
            @V_CD_EXCH      NVARCHAR(3),
            @V_DT_SO        NVARCHAR(8),
            @V_CD_PLANT     NVARCHAR(7),
            @V_FG_USE       NVARCHAR(4),
            @V_UM_SO        NUMERIC(15,4),
            @V_UM_ITEM      NUMERIC(15,4),
            @V_UM_ITEM_LOW  NUMERIC(15,4),
            @V_TXT_USERDEF1 NVARCHAR(1)     --단가변경여부
    
    SELECT  @V_CD_ITEM      = L.CD_ITEM,
            @V_CD_PARTNER   = H.CD_PARTNER,
            @V_TP_PRICE     = H.TP_PRICE,
            @V_CD_EXCH      = H.CD_EXCH,
            @V_DT_SO        = H.DT_SO,
            @V_CD_PLANT     = L.CD_PLANT,
            @V_FG_USE       = L.FG_USE,
            @V_UM_SO        = L.UM_SO,
            @V_TXT_USERDEF1 = L.TXT_USERDEF1
    FROM    SA_SOL L
            INNER JOIN SA_SOH H ON L.CD_COMPANY = H.CD_COMPANY AND H.NO_SO = L.NO_SO
    WHERE   L.CD_COMPANY = @P_CD_COMPANY
    AND     L.NO_SO = @P_NO_SO
    AND     L.SEQ_SO = @P_SEQ_SO

    IF (@V_FG_USE = '010' AND @V_TXT_USERDEF1 = 'Y')
    BEGIN
        SELECT	@V_UM_ITEM      = T.UM_ITEM,
                @V_UM_ITEM_LOW  = T.UM_ITEM_LOW
        FROM    (   SELECT  TOP 1 MAX(SDT_UM) SDT_UM, UM_ITEM, UM_ITEM_LOW
                    FROM	MA_ITEM_UMPARTNER
                    WHERE	CD_ITEM     = @V_CD_ITEM
                    AND     CD_PARTNER  = @V_CD_PARTNER
                    AND     FG_UM       = @V_TP_PRICE
                    AND     CD_EXCH     = @V_CD_EXCH
                    AND     @V_DT_SO BETWEEN SDT_UM AND EDT_UM
                    AND		TP_UMMODULE	= '002'
                    AND     CD_COMPANY	= @P_CD_COMPANY
                    AND     CD_PLANT    = @V_CD_PLANT
                    AND     NO_LINE     = 1
                    GROUP BY UM_ITEM, UM_ITEM_LOW
                    ORDER BY SDT_UM DESC ) T
                    
        IF (@V_UM_SO < @V_UM_ITEM)
        BEGIN
            UPDATE  MA_ITEM_UMPARTNER
            SET     UM_ITEM     = @V_UM_SO
            WHERE	CD_ITEM     = @V_CD_ITEM
            AND     CD_PARTNER  = @V_CD_PARTNER
            AND     FG_UM       = @V_TP_PRICE
            AND     CD_EXCH     = @V_CD_EXCH
            AND     @V_DT_SO BETWEEN SDT_UM AND EDT_UM
            AND		TP_UMMODULE	= '002'
            AND     CD_COMPANY	= @P_CD_COMPANY
            AND     CD_PLANT    = @V_CD_PLANT
            AND     NO_LINE     = 1
        END
        
        IF (@V_UM_SO < @V_UM_ITEM_LOW)
        BEGIN
            UPDATE  MA_ITEM_UMPARTNER
            SET     UM_ITEM_LOW = @V_UM_SO
            WHERE	CD_ITEM     = @V_CD_ITEM
            AND     CD_PARTNER  = @V_CD_PARTNER
            AND     FG_UM       = @V_TP_PRICE
            AND     CD_EXCH     = @V_CD_EXCH
            AND     @V_DT_SO BETWEEN SDT_UM AND EDT_UM
            AND		TP_UMMODULE	= '002'
            AND     CD_COMPANY	= @P_CD_COMPANY
            AND     CD_PLANT    = @V_CD_PLANT
            AND     NO_LINE     = 1
        END
    END
END
   
--수주관리에서 라인별 종결 처리시 해당 라인별 수주수량을 의뢰수량으로
--업데이트 처리 후, sa_projectl.qt_po 수량도 의뢰수량으로 업데이트 처리(BY SJH) - 문점용 대리 요청사항
IF ( SELECT CD_EXC FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '종결수량적용여부') = 'Y'
BEGIN
    IF (@P_STA_SO = 'C')
    BEGIN
        UPDATE SA_SOL
           SET QT_SO = QT_GIR,
               QT_IM = QT_GIR_IM
         WHERE CD_COMPANY = @P_CD_COMPANY
           AND NO_SO = @P_NO_SO
           AND SEQ_SO = @P_SEQ_SO
           AND QT_SO <> QT_GIR
           AND QT_GIR > 0 
    END
END
       
     
--라인의 수주상태가 모두 초기 상태일때 즉 'O'일때 헤더의 상태도 'O'로 바꿔준다.    
IF NOT EXISTS (SELECT  'X'     
     FROM  SA_SOL     
    WHERE  CD_COMPANY = @P_CD_COMPANY     
      AND  NO_SO = @P_NO_SO     
      AND (STA_SO <> 'O' AND STA_SO <> '' AND STA_SO IS NOT NULL))    
BEGIN    
 UPDATE SA_SOH    
    SET STA_SO   = @P_STA_SO    
  WHERE CD_COMPANY = @P_CD_COMPANY    
    AND NO_SO   = @P_NO_SO    
END    
ELSE IF NOT EXISTS (SELECT  'X'     
                     FROM  SA_SOL     
                    WHERE  CD_COMPANY = @P_CD_COMPANY     
                      AND  NO_SO = @P_NO_SO    
                      AND (STA_SO <> @P_STA_SO))    
BEGIN    
-- IF EXISTS (SELECT 'X' FROM SA_SOL WHERE CD_COMPANY = @P_CD_COMPANY AND  NO_SO = @P_NO_SO AND STA_SO = 'O')    
--  SET @P_STA_SO = 'O'    
-- ELSE IF EXISTS (SELECT 'X' FROM SA_SOL WHERE CD_COMPANY = @P_CD_COMPANY AND  NO_SO = @P_NO_SO AND STA_SO <> 'O' AND STA_SO = 'R')    
--  SET @P_STA_SO = 'R'    
    
 UPDATE SA_SOH    
    SET STA_SO   = @P_STA_SO    
  WHERE CD_COMPANY = @P_CD_COMPANY    
    AND NO_SO   = @P_NO_SO    
END    
    
--ELSE IF NOT EXISTS (SELECT  'X'     
--     FROM  SA_SOL     
--    WHERE  CD_COMPANY = @P_CD_COMPANY     
--      AND  NO_SO = @P_NO_SO     
--      AND (STA_SO <> 'P'))    
--BEGIN    
-- UPDATE SA_SOH    
--    SET STA_SO   = @P_STA_SO    
--  WHERE CD_COMPANY = @P_CD_COMPANY    
--    AND NO_SO   = @P_NO_SO    
--END    
--ELSE IF NOT EXISTS (SELECT  'X'     
--     FROM  SA_SOL     
--    WHERE  CD_COMPANY = @P_CD_COMPANY     
--      AND  NO_SO = @P_NO_SO     
--      AND (STA_SO <> 'R'))    
--BEGIN    
-- UPDATE SA_SOH    
--    SET STA_SO   = @P_STA_SO    
--  WHERE CD_COMPANY = @P_CD_COMPANY    
--    AND NO_SO   = @P_NO_SO    
--END    
--ELSE IF NOT EXISTS (SELECT  'X'     
--     FROM  SA_SOL     
--    WHERE  CD_COMPANY = @P_CD_COMPANY     
--      AND  NO_SO = @P_NO_SO     
--      AND (STA_SO <> 'S'))    
--BEGIN    
-- UPDATE SA_SOH    
--    SET STA_SO   = @P_STA_SO    
--  WHERE CD_COMPANY = @P_CD_COMPANY    
--    AND NO_SO   = @P_NO_SO    
--END    
--ELSE IF NOT EXISTS (SELECT  'X'     
--     FROM  SA_SOL     
--    WHERE  CD_COMPANY = @P_CD_COMPANY     
--      AND  NO_SO = @P_NO_SO     
--      AND (STA_SO <> 'C'))    
--BEGIN    
-- UPDATE SA_SOH    
--    SET STA_SO   = @P_STA_SO    
--  WHERE CD_COMPANY = @P_CD_COMPANY    
--    AND NO_SO   = @P_NO_SO    
--END
GO

