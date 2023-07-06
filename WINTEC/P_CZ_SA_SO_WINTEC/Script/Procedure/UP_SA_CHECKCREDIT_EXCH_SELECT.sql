USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_CHECKCREDIT_EXCH_SELECT]    Script Date: 2019-11-04 오전 10:35:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_SA_CHECKCREDIT_EXCH_SELECT]    
(    
 @P_CD_COMPANY  NVARCHAR(7),    
 @P_CD_PARTNER  NVARCHAR(20),  --거래처  
 @P_CD_EXCH   NVARCHAR(3),   --환종  
 @P_AM_SUM   NUMERIC(17, 4),   --외화금액(부가세 일단 제외)  
 @P_STA_CHK   NVARCHAR(3),   --001 : 수주확정, 002 : 출하의뢰, 003 : 출하등록    
 @P_TOT_CREDIT  NUMERIC(17, 4) OUTPUT,                   
 @P_JAN    NUMERIC(17, 4) OUTPUT, --잔액    
 @P_LVL_CHK   NVARCHAR(3)  OUTPUT --체크레벨 ( 001 : NOCHECK, 002 : WARNING, 003 : ERROR)    
)    
AS    
SET NOCOUNT OFF    
 

   
DECLARE @V_GRP_CREDIT NVARCHAR(3), --여신그룹    
  @V_STA_CHK  NVARCHAR(3), --체크시점 ( 001 : 수주확정, 002 : 출하의뢰, 003 : 출하등록)    
  @V_DATE   NCHAR(8),  --금일날짜    
  @V_YESTERDAY NCHAR(8),  --금일하루전날    
  @V_YY   NCHAR(4),  --금일년도    
  @V_YY_1   NCHAR(8),  --금일년도 + '0000'    
  @V_YY_FIRST  NCHAR(8),  --금일년도01월01일    
  @V_ARD_JAN  NUMERIC(17, 4), --채권잔액    
  @V_SERVER_KEY NCHAR(1),  
  @V_SECURITY_DATE_FR NVARCHAR(8),    --담보등록 만기일FR  
  @V_SECURITY_DATE NVARCHAR(8),  --담보등록 만기일  
  @V_SECURITY_AM   NUMERIC(17, 4)  --담보금액  
  
    
SET  @V_GRP_CREDIT = NULL    
SET  @V_STA_CHK  = NULL    
SET  @P_LVL_CHK  = NULL    
SET  @V_DATE   = CONVERT(NVARCHAR(30), GETDATE(),112)    
SET  @V_YESTERDAY = CONVERT(NVARCHAR(30), DATEADD(DD, -1, @V_DATE), 112)    
SET  @V_YY   = SUBSTRING(@V_DATE, 1 ,4)    
SET  @V_YY_1   = @V_YY + '0000'    
SET  @V_YY_FIRST  = @V_YY + '0101'    
SET  @P_TOT_CREDIT = NULL    
SET  @V_ARD_JAN  = 0    
  
SELECT @V_SERVER_KEY = NEOE.FN_SERVER_KEY('DZSQL')    
   -- SERVER KEY (업체전용)                  
DECLARE @V_SERVER_KEY2 NVARCHAR(50)                  
SELECT MAX(SERVER_KEY) FROM CM_SERVER_CONFIG          
SELECT @V_SERVER_KEY2 = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG    

--해당 거래처(MA_PARTNER)의 여신여부(FG_CREDIT)가 Y이면 거래처여신정보(SA_PTRCREDIT)의 여신그룹(GRP_CREDIT)을 가져온다.    
SELECT @V_GRP_CREDIT = A.GRP_CREDIT    
FROM SA_CREDIT_EXCH A     
  INNER JOIN MA_PARTNER B ON B.CD_COMPANY = @P_CD_COMPANY AND B.CD_PARTNER = @P_CD_PARTNER AND B.FG_CREDIT = 'Y'    
WHERE A.CD_COMPANY = @P_CD_COMPANY    
AND  A.CD_PARTNER = @P_CD_PARTNER    
AND     A.CD_EXCH    = @P_CD_EXCH  
    
--여신그룹이 널이 아니면    
IF (@V_GRP_CREDIT IS NOT NULL)    
BEGIN    
 SELECT @V_STA_CHK = STA_CHK,    
   @P_LVL_CHK = LVL_CHK    
 FROM SA_GRPCREDIT    
 WHERE CD_COMPANY = @P_CD_COMPANY    
 AND GRP_CREDIT  = @V_GRP_CREDIT    
    
 --여신그룹의 체크시점이 해당 호출 프로시져의 체크시점과 같으면    
 IF (@V_STA_CHK = @P_STA_CHK)    
 BEGIN    
  IF (@P_LVL_CHK = '002' OR @P_LVL_CHK = '003')    
  BEGIN    
   --채권잔액 구하는 쿼리    
   SELECT @V_ARD_JAN = SUM(기초채권1) + SUM(기초채권2) + SUM(채권발생액) - SUM(수금액)    
   FROM (    
    SELECT --CD_PARTNER,     
      AM_GI 기초채권1,    
      0 기초채권2,    
      0 채권발생액,    
      0 수금액    
    FROM SA_AR_EXCH     
    WHERE CD_COMPANY = @P_CD_COMPANY    
    AND  CD_PARTNER = @P_CD_PARTNER    
    AND     CD_EXCH    = @P_CD_EXCH  
    AND  YYMM LIKE @V_YY + '%'                
  
    UNION ALL  
    SELECT --L.CD_PARTNER,     
      0 기초채권1,    
      0 기초채권2,    
      SUM(CASE H.YN_RETURN WHEN 'Y' THEN 0 - (L.AM + L.VAT)   
           ELSE L.AM + L.VAT   
       END) AS  채권발생액,    
      0 수금액    
      FROM MM_QTIO L   
      JOIN MM_QTIOH H ON L.CD_COMPANY = H.CD_COMPANY AND L.NO_IO = H.NO_IO  
     WHERE L.CD_COMPANY = @P_CD_COMPANY  
       AND ((L.FG_IO = '010' OR L.FG_IO ='041') AND L.YN_AM = 'Y')  
       AND L.CD_PARTNER = @P_CD_PARTNER  
       AND L.CD_EXCH    = @P_CD_EXCH  
       AND L.DT_IO >= @V_YY_FIRST  
       AND L.DT_IO <= @V_DATE  
  
    UNION ALL    
  
    SELECT  --H.CD_PARTNER,  
      0 기초채권1,    
      0 기초채권2,    
      0 채권발생액,  
      SUM(L.AM_RCP_EX + L.AM_RCP_A_EX)  AS 수금액  
       FROM SA_RCPL L   
       JOIN SA_RCPH H ON L.CD_COMPANY = H.CD_COMPANY AND L.NO_RCP = H.NO_RCP  
      WHERE L.CD_COMPANY = @P_CD_COMPANY  
        AND H.CD_PARTNER = @P_CD_PARTNER  
        AND H.CD_EXCH    = @P_CD_EXCH  
        AND H.DT_RCP >= @V_YY_FIRST  
        AND H.DT_RCP <= @V_DATE  
        
      UNION ALL  
        
     SELECT --H.CD_PARTNER,  
      0 기초채권1,    
      0 기초채권2,    
      0 채권발생액,  
      H.AM_EXNEGO  --H.AM_TARGET 에서 대체 T/T선입은 AM_TARGET에 값이없슴   
      + ( CASE H.TP_NEGO WHEN '003' THEN H.AM_EXNEGO    
            ELSE 0   
       END )  AS 수금액  
      FROM  TR_NEGOEXH H  
     WHERE H.CD_COMPANY = @P_CD_COMPANY  
       AND H.CD_PARTNER = @P_CD_PARTNER  
       AND H.CD_EXCH    = @P_CD_EXCH         
       AND H.DT_NEGO >= @V_YY_FIRST  
       AND H.DT_NEGO <= @V_DATE                      
   
   ) A    
   --GROUP BY CD_PARTNER            
     
   
   --거래처여신정보(SA_PTRCREDIT)의 여신총액(TOT_CREDIT)을 가져온다.    
   SELECT @P_TOT_CREDIT = TOT_CREDIT    
   FROM SA_CREDIT_EXCH     
   WHERE CD_COMPANY = @P_CD_COMPANY    
   AND  CD_PARTNER = @P_CD_PARTNER    
   AND     CD_EXCH    = @P_CD_EXCH  
  
   --여신총액 - 채권잔액 - 금번처리총액(원화금액 + 부가세) > 0이면 통과    
   IF (@P_TOT_CREDIT - @V_ARD_JAN - @P_AM_SUM < 0)     
   BEGIN    
    --SET @P_JAN = @P_TOT_CREDIT - @V_ARD_JAN    
    SET @P_TOT_CREDIT = @P_TOT_CREDIT - @V_ARD_JAN    
    SET @P_JAN = @P_AM_SUM    
    RETURN    
   END    
   ELSE    
   BEGIN    
    SET @P_TOT_CREDIT = NULL    
   END                            
  END    
 END    
END    
    
SET NOCOUNT ON
GO

