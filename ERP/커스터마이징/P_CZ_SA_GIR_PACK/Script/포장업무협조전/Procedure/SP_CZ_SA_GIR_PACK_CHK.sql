USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIR_PACK_CHK]    Script Date: 2015-06-01 오후 5:33:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

/*******************************************    
**  System      : 영업    
**  Sub System  : 납품의뢰관리    
**  Page        : 납품의뢰등록    
**  Desc        : 의뢰수량체크     
**    
**  Return Values    
**    
**  작    성    자    :     
**  작    성    일    :     
**  수    정    자    : NJIN    
*********************************************    
** Change History    
*********************************************    
*********************************************/    
ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIR_PACK_CHK]     
(    
    @P_CD_COMPANY       NVARCHAR(7),    
    @P_NO_GIR           NVARCHAR(20),    
    @P_SEQ_GIR          NUMERIC(3),    
    @P_NO_SO            NVARCHAR(20),    
    @P_SEQ_SO           NUMERIC(3),    
    @P_NO_LC            NVARCHAR(20),    
    @P_SEQ_LC           NUMERIC(3),    
    @P_QT_NEW           NUMERIC(17, 4),    
    @P_RESULT           INT OUTPUT,  
    @P_NOTSEQ_GIR		NUMERIC(17,4) OUTPUT  
)    
AS    
DECLARE   
  @V_QT_SO  NUMERIC(17, 4),  -- 수주수량  
  @V_TOT_QT_GIR NUMERIC(17, 4),  -- 총 의뢰수량  
  @V_SEQ_GIR  NUMERIC(17, 4),  -- (SA_GIRL)현재 의뢰라인의 의뢰수량  
  @V_NOTSEQ_GIR NUMERIC(17,4),  -- 현재 의뢰라인이 아닌 분할된 의뢰수량  
  @V_LIMIT_GIR NUMERIC(17,4)  -- 입력가능한 의뢰수량  
    
  --@V_QT_MINUS NUMERIC(17, 4),    
    
        --@V_QT_GI    NUMERIC(17, 4)   
SET @V_QT_SO = 0  
SET @V_TOT_QT_GIR = 0  
SET @V_SEQ_GIR = 0  
SET @V_NOTSEQ_GIR = 0  
SET @V_LIMIT_GIR = 0  
SET @P_NOTSEQ_GIR = 0  
          
-- 수주건에 대한 수주수량 조회
SELECT  @V_QT_SO  = QT_SO
  FROM  SA_SOL   
 WHERE  CD_COMPANY = @P_CD_COMPANY  AND NO_SO = @P_NO_SO AND SEQ_SO = @P_SEQ_SO  

-- 수주건에 대한 총 의뢰수량 조회
SELECT @V_TOT_QT_GIR = ISNULL(SUM(QT_GIR), 0)  
  FROM CZ_SA_GIRL_PACK
WHERE  CD_COMPANY = @P_CD_COMPANY  AND NO_SO = @P_NO_SO AND SEQ_SO = @P_SEQ_SO

-- 현재 의뢰라인의 의뢰수량   
SELECT  @V_SEQ_GIR = QT_GIR    
  FROM  CZ_SA_GIRL_PACK    
 WHERE  CD_COMPANY = @P_CD_COMPANY    
   AND  NO_GIR  = @P_NO_GIR    
   AND  SEQ_GIR  = @P_SEQ_GIR   
     
-- 분할된 의뢰수량 = 총의뢰수량 - 현재라인의 의뢰수량  
SET @V_NOTSEQ_GIR = @V_TOT_QT_GIR - @V_SEQ_GIR  
SET @P_NOTSEQ_GIR = @V_NOTSEQ_GIR  
-- 입력가능한 의뢰수량 = 총 수주수량 - 분할된의뢰수량  
SET @V_LIMIT_GIR = @V_QT_SO - @V_NOTSEQ_GIR  
  
SET @P_RESULT = 1  
IF (@P_QT_NEW > @V_LIMIT_GIR)    
BEGIN  
 SET @P_RESULT = 0  
END    
  
RETURN  

GO

