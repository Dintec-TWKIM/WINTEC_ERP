
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRSCH_SELECT_PRINT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRSCH_SELECT_PRINT
GO

  /*******************************************                            
**  System : 영업                          
**  Sub System : 출하                          
**  Page  : 출하의뢰관리(SA_GIRSCH)                          
**  Desc  :  납품의뢰현황 출력물      
**  Return Values                            
**                            
**  작    성    자  :  오 성 영                         
**  작    성    일  :  2007-07-09                          
**  수    정    자  :                            
*********************************************                            
** Change History                         
**            
*********************************************/           
  
  
CREATE  PROC UP_SA_GIRSCH_SELECT_PRINT
(            
   @CD_COMPANY  NVARCHAR(7),   --회사코드            
   @DT_GIR_FROM  NVARCHAR(8),            
   @DT_GIR_TO   NVARCHAR(8),            
   @CD_BIZAREA   NVARCHAR(7),  --사업장            
   @TP_BUSI   NVARCHAR(3),  --거래구분            
   @CD_PLANT   NVARCHAR(7), --공장            
   @CD_SL   NVARCHAR(7),  --창고            
   @STA_SO   NVARCHAR(3),  --처리상태            
   @RET   NVARCHAR(3),  --출하구분            
   @UNIT   NVARCHAR(3),  --단위선택(수배단위, 재고단위)            
   @TP_SO   NVARCHAR(4), --수주유형            
   @STATE   NCHAR(1),  --상태            
   @FG_TRANSPORT NVARCHAR(3), -- 운송수단          
   @NO_PK VARCHAR(8000)      
)            
AS          
    
/*************************************************        
 라인 -  재고단위          
*************************************************/          
    
 IF(@UNIT = '002')            
   BEGIN       
     
  SELECT    
   'N' S,     --선택      
   A.NO_GIR,             
   A.CD_PARTNER,             
   B.UNIT,      
   B.CD_ITEM,    
   B.QT_GIR,             
   B.QT_GI,            
   B.AM_GIR,     
   B.UM ,            
   E.FG_TRANSPORT,      
   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_SYSDEF = E.FG_TRANSPORT AND CD_COMPANY = @CD_COMPANY  AND CD_FIELD = 'TR_IM00008' ) NM_TRANSPORT, -- 운송수단             
   A.CD_PLANT,             
   B.CD_SL,              
   A.STA_GIR,      
   A.DT_GIR,           
   B.TP_GI,            
      B.NO_PROJECT,            
   B.CD_SALEGRP,            
    
   (SELECT NM_PROJECT FROM SA_PROJECTH WHERE NO_PROJECT = B.NO_PROJECT AND CD_COMPANY = B.CD_COMPANY AND NO_SEQ =            
   (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE NO_PROJECT = B.NO_PROJECT AND CD_COMPANY = B.CD_COMPANY)) NM_PROJECT,           
   (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,            
   (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_QTIOTP,            
   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER ,        
   (SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,        
   (SELECT STND_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) STND_ITEM,        
   (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,     
   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027') NM_RETURN,            
   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) NM_PARTNER,            
   (SELECT NM_PLANT FROM MA_PLANT WHERE CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_PLANT,            
   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = A.TP_BUSI AND CD_COMPANY = A.CD_COMPANY) NM_BUSI,            
   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = A.STA_GIR AND CD_COMPANY = A.CD_COMPANY) NM_STA_GIR,             
   G.NM_QTIOTP AS NM_TP_GI            
    
  FROM          
   SA_GIRH A              
    INNER JOIN SA_GIRL B     ON  A.NO_GIR = B.NO_GIR AND  A.CD_COMPANY = B.CD_COMPANY            
    INNER JOIN MA_PLANT C  ON  A.CD_COMPANY = C.CD_COMPANY AND  A.CD_PLANT = C.CD_PLANT             
    INNER JOIN MA_PITEM D  ON  A.CD_COMPANY = D.CD_COMPANY AND B.CD_ITEM = D.CD_ITEM AND  A.CD_PLANT = D.CD_PLANT    
    LEFT OUTER JOIN SA_SOH E     ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO            
    LEFT OUTER JOIN SA_SOL F      ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO AND B.SEQ_SO = F.SEQ_SO                
    LEFT OUTER JOIN MM_EJTP G ON A.TP_GI = G.CD_QTIOTP AND G.CD_COMPANY = @CD_COMPANY            
       WHERE         
     A.CD_COMPANY = @CD_COMPANY      
     AND   (    ( @STATE = '0'  and  B.NO_GIR IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
              OR  ( @STATE = '1'  and  B.CD_ITEM IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
        OR  ( @STATE = '2'  and  A.CD_PARTNER IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
         OR  ( @STATE = '3'  and  B.CD_SALEGRP IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
        OR  ( @STATE = '4'  and  A.DT_GIR IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
        OR  ( @STATE = '5'  and  B.TP_GI IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
        OR  ( @STATE = '6'  and  B.NO_PROJECT IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
                             )    
     AND  A.DT_GIR BETWEEN @DT_GIR_FROM AND @DT_GIR_TO            
     AND  C.CD_BIZAREA = @CD_BIZAREA            
     AND ((  A.TP_BUSI  =@TP_BUSI) OR (@TP_BUSI = '' ) OR (@TP_BUSI IS NULL))            
     AND ((  A.CD_PLANT  =@CD_PLANT) OR (@CD_PLANT = '' ) OR (@CD_PLANT IS NULL))            
     AND ((  B.CD_SL  =@CD_SL) OR (@CD_SL = '' ) OR (@CD_SL IS NULL))            
     AND ((  B.RET  =@RET) OR (@RET = '' ) OR (@RET IS NULL))            
     AND ((  E.TP_SO  = @TP_SO) OR (@TP_SO = '' ) OR (@TP_SO IS NULL))            
     AND ((  E.FG_TRANSPORT  =@FG_TRANSPORT) OR (@FG_TRANSPORT = '' ) OR (@FG_TRANSPORT IS NULL))            
     AND (( @STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@STA_SO IS NULL) OR (@STA_SO = '') )          
 ORDER BY    
     (CASE @STATE    
       WHEN  '0' THEN B.NO_GIR    
       WHEN  '1' THEN B.CD_ITEM    
       WHEN  '2' THEN A.CD_PARTNER    
       WHEN  '3' THEN B.CD_SALEGRP    
       WHEN  '4' THEN A.DT_GIR    
       WHEN  '5' THEN B.TP_GI    
       WHEN  '6' THEN B.NO_PROJECT    
      END)    /* 레포트 디자이너에서 정렬이 되지 않는 관계로 Order By 를 동적쿼리로 뽑아서 DT로 넘겼다 ㅡㅡ; */    
    
END    
    
    
/*************************************************        
 라인 - 수주단위          
*************************************************/            
 ELSE            
        
   BEGIN            
         
    SELECT    
   'N' S,     --선택      
   A.NO_GIR,             
   A.CD_PARTNER,             
   B.UNIT,      
   B.CD_ITEM,    
   B.QT_GIR_IM AS QT_GIR,             
   B.QT_GI_IM AS QT_GI,            
   B.AM_GIR,     
   CONVERT(numeric(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,           
   B.UM,            
   E.FG_TRANSPORT,       
   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_SYSDEF = E.FG_TRANSPORT AND CD_COMPANY = @CD_COMPANY  AND CD_FIELD = 'TR_IM00008' ) NM_TRANSPORT, -- 운송수단             
   A.CD_PLANT,             
   B.CD_SL,              
   A.STA_GIR,      
   A.DT_GIR,           
   B.TP_GI,            
      B.NO_PROJECT,            
   B.CD_SALEGRP,            
    
   (SELECT NM_PROJECT FROM SA_PROJECTH WHERE NO_PROJECT = B.NO_PROJECT AND CD_COMPANY = B.CD_COMPANY AND NO_SEQ =            
   (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE NO_PROJECT = B.NO_PROJECT AND CD_COMPANY = B.CD_COMPANY)) NM_PROJECT,           
   (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,            
   (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_QTIOTP,            
   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER ,        
   (SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,        
   (SELECT STND_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) STND_ITEM,        
   (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,     
   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027') NM_RETURN,            
   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) NM_PARTNER,            
   (SELECT NM_PLANT FROM MA_PLANT WHERE CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_PLANT,            
   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = A.TP_BUSI AND CD_COMPANY = A.CD_COMPANY) NM_BUSI,            
   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = A.STA_GIR AND CD_COMPANY = A.CD_COMPANY) NM_STA_GIR,             
   G.NM_QTIOTP AS NM_TP_GI            
    
  FROM          
   SA_GIRH A              
    INNER JOIN SA_GIRL B     ON  A.NO_GIR = B.NO_GIR AND  A.CD_COMPANY = B.CD_COMPANY            
    INNER JOIN MA_PLANT C  ON  A.CD_COMPANY = C.CD_COMPANY AND  A.CD_PLANT = C.CD_PLANT             
    INNER JOIN MA_PITEM D  ON  A.CD_COMPANY = D.CD_COMPANY AND B.CD_ITEM = D.CD_ITEM AND  A.CD_PLANT = D.CD_PLANT    
    LEFT OUTER JOIN SA_SOH E     ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO            
    LEFT OUTER JOIN SA_SOL F      ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO AND B.SEQ_SO = F.SEQ_SO                
    LEFT OUTER JOIN MM_EJTP G ON A.TP_GI = G.CD_QTIOTP AND G.CD_COMPANY = @CD_COMPANY            
       WHERE         
     A.CD_COMPANY = @CD_COMPANY      
        --AND B.NO_GIR IN (SELECT * FROM TF_GETSPLIT(@NO_GIR))          
     AND   (    ( @STATE = '0'  and  B.NO_GIR IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
              OR  ( @STATE = '1'  and  B.CD_ITEM IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
        OR  ( @STATE = '2'  and  A.CD_PARTNER IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
         OR  ( @STATE = '3'  and  B.CD_SALEGRP IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
        OR  ( @STATE = '4'  and  A.DT_GIR IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
        OR  ( @STATE = '5'  and  B.TP_GI IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
        OR  ( @STATE = '6'  and  B.NO_PROJECT IN (SELECT * FROM TF_GETSPLIT(@NO_PK)) )     
                             )    
     AND  A.DT_GIR BETWEEN @DT_GIR_FROM AND @DT_GIR_TO            
     AND  C.CD_BIZAREA = @CD_BIZAREA            
     AND ((  A.TP_BUSI  =@TP_BUSI) OR (@TP_BUSI = '' ) OR (@TP_BUSI IS NULL))            
     AND ((  A.CD_PLANT  =@CD_PLANT) OR (@CD_PLANT = '' ) OR (@CD_PLANT IS NULL))            
     AND ((  B.CD_SL  =@CD_SL) OR (@CD_SL = '' ) OR (@CD_SL IS NULL))           
     AND ((  B.RET  =@RET) OR (@RET = '' ) OR (@RET IS NULL))            
     AND ((  E.TP_SO  = @TP_SO) OR (@TP_SO = '' ) OR (@TP_SO IS NULL))            
     AND ((  E.FG_TRANSPORT  =@FG_TRANSPORT) OR (@FG_TRANSPORT = '' ) OR (@FG_TRANSPORT IS NULL))            
     AND (( @STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@STA_SO IS NULL) OR (@STA_SO = '') )          
   ORDER BY    
     (CASE @STATE    
       WHEN  '0' THEN B.NO_GIR    
       WHEN  '1' THEN B.CD_ITEM    
       WHEN  '2' THEN A.CD_PARTNER    
       WHEN  '3' THEN B.CD_SALEGRP    
       WHEN  '4' THEN A.DT_GIR    
       WHEN  '5' THEN B.TP_GI    
       WHEN  '6' THEN B.NO_PROJECT    
      END)     
 END     
    
GO
  
  
      
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRSCH_SELECT_H') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRSCH_SELECT_H
GO

        
/*******************************************                          
**  System : 영업                        
**  Sub System : 출하                        
**  Page  : 출하의뢰관리(SA_GIRSCH)                        
**  Desc  :  납품의뢰현황 헤더    
**  Return Values                          
**                          
**  작    성    자  :  오 성 영                       
**  작    성    일  :  2007-07-09                        
**  수    정    자  :                          
*********************************************                          
** Change History                       
**          
*********************************************/                      
          
          
CREATE PROC UP_SA_GIRSCH_SELECT_H
(              
   @P_CD_COMPANY  NVARCHAR(7),   --회사코드              
   @P_DT_GIR_FROM  NVARCHAR(8),              
   @P_DT_GIR_TO   NVARCHAR(8),              
   @P_CD_BIZAREA   NVARCHAR(7),  --사업장              
   @P_TP_BUSI   NVARCHAR(3),  --거래구분              
   @P_CD_PLANT   NVARCHAR(7), --공장              
   @P_CD_SL   NVARCHAR(7),  --창고              
   @P_STA_SO   NVARCHAR(3),  --처리상태              
   @P_RET   NVARCHAR(3),  --출하구분              
   @P_UNIT   NVARCHAR(3),  --단위선택(수배단위, 재고단위)              
   @P_TP_SO   NVARCHAR(4), --수주유형              
   @P_STATE   NCHAR(1),  --상태              
   @P_FG_TRANSPORT NVARCHAR(3), -- 운송수단            
   @P_CLS_ITEM NVARChAR(3) -- 계정구분    
)              
AS              
          
/*************************************************          
    헤더  -  의뢰번호별           
*************************************************/                
IF(RTRIM(@P_STATE) = '0')              
BEGIN              
          
    SELECT     
 DISTINCT 'N' S, A.NO_GIR, A.DT_GIR, A.CD_PARTNER, A.CD_PLANT,       
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027') NM_RETURN,              
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) NM_PARTNER,              
 (SELECT NM_PLANT FROM MA_PLANT WHERE CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_PLANT,              
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = A.TP_BUSI AND CD_COMPANY = A.CD_COMPANY) NM_BUSI,              
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = A.STA_GIR AND CD_COMPANY = A.CD_COMPANY) NM_STA_GIR,-- A.TP_GI, D.NM_QTIOTP AS NM_TP_GI              
 A.DC_RMK  
    FROM            
 SA_GIRH A           
 INNER JOIN SA_GIRL B           ON A.NO_GIR = B.NO_GIR  AND  A.CD_COMPANY = B.CD_COMPANY                 
 INNER JOIN MA_PLANT C        ON A.CD_COMPANY = C.CD_COMPANY  AND A.CD_PLANT = C.CD_PLANT                
 LEFT OUTER JOIN MM_EJTP D ON B.TP_GI = D.CD_QTIOTP AND D.CD_COMPANY = @P_CD_COMPANY    
 LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO --AND E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT           /* 납품의뢰현황에서 출하구분에서 반품이 조회되지 않는 문제 - 수주적용 받지 않더라도 현황에 보여줘야 하기 떄문에 INNER를 LEFT OUTER로 수정 */    
 LEFT OUTER  JOIN SA_SOL F ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO    AND B.SEQ_SO = F.SEQ_SO          
 INNER JOIN MA_PITEM G  ON  A.CD_COMPANY = G.CD_COMPANY AND B.CD_ITEM = G.CD_ITEM AND  A.CD_PLANT = G.CD_PLANT      
    WHERE           
       A.CD_COMPANY = @P_CD_COMPANY              
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO              
 AND  C.CD_BIZAREA = @P_CD_BIZAREA              
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))              
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))              
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))              
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))              
 AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))           
 AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))     
 AND ((  G.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))                     
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )            
       
END              
    
/**************************************************************************************************************          
                       헤더 - 품목코드별            
**************************************************************************************************************/              
            
ELSE IF(RTRIM(@P_STATE) = '1')              
  BEGIN              
           
  SELECT    
 DISTINCT 'N' S,          
 B.CD_ITEM,           
 D.NM_ITEM,          
 D.STND_ITEM,          
 D.UNIT_IM              
              
 FROM            
 SA_GIRH A                
 INNER JOIN SA_GIRL B     ON  A.NO_GIR = B.NO_GIR AND  A.CD_COMPANY = B.CD_COMPANY              
 INNER JOIN MA_PLANT C  ON  A.CD_COMPANY = C.CD_COMPANY AND  A.CD_PLANT = C.CD_PLANT               
 INNER JOIN MA_PITEM D  ON  A.CD_COMPANY = D.CD_COMPANY AND  A.CD_PLANT = D.CD_PLANT  AND B.CD_ITEM = D.CD_ITEM                  
 LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO  -- AND E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT             
 LEFT OUTER  JOIN SA_SOL F ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO    AND B.SEQ_SO = F.SEQ_SO          
    
 WHERE           
 A.CD_COMPANY = @P_CD_COMPANY              
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO              
 AND  C.CD_BIZAREA = @P_CD_BIZAREA              
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))              
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))              
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))              
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))     
 AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))              
 AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))              
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))             
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )            
    
END              
          
/******************************************************************************          
               헤더  -  거래처별           
******************************************************************************/              
          
ELSE IF(RTRIM(@P_STATE) = '2')              
BEGIN              
        
  SELECT      
 DISTINCT 'N' S, A.CD_PARTNER,            
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER            
 FROM           
 SA_GIRH A           
 INNER JOIN SA_GIRL B    ON  A.NO_GIR = B.NO_GIR  AND  A.CD_COMPANY = B.CD_COMPANY             
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY  AND A.CD_PLANT = C.CD_PLANT            
 LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO  --AND E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT              
 LEFT OUTER  JOIN SA_SOL F ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO    AND B.SEQ_SO = F.SEQ_SO          
 INNER JOIN MA_PITEM G  ON  A.CD_COMPANY = G.CD_COMPANY AND  A.CD_PLANT = G.CD_PLANT  AND B.CD_ITEM = G.CD_ITEM        
 WHERE           
 A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA            
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))      
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))            
 AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
 AND ((  G.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))             
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )           
          
    
END              
          
/*************************************************          
    헤더  -  영업그룹별           
*************************************************/                
          
ELSE IF(RTRIM(@P_STATE) = '3')         
         
BEGIN              
          
 SELECT      
 DISTINCT 'N' S, B.CD_SALEGRP,              
 (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP              
FROM            
 SA_GIRH A           
 INNER JOIN SA_GIRL B ON A.NO_GIR = B.NO_GIR  AND  A.CD_COMPANY = B.CD_COMPANY             
 INNER JOIN MA_PLANT C ON A.CD_COMPANY = C.CD_COMPANY  AND A.CD_PLANT = C.CD_PLANT             
 LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO  --AND E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT              
 LEFT OUTER JOIN SA_SOL F  ON B.NO_SO = F.NO_SO  AND B.SEQ_SO = F.SEQ_SO    AND B.CD_COMPANY = F.CD_COMPANY              
 INNER JOIN MA_PITEM G  ON  A.CD_COMPANY = G.CD_COMPANY AND  A.CD_PLANT = G.CD_PLANT  AND B.CD_ITEM = G.CD_ITEM        
 WHERE            
 A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA            
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))              
 AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))              
 AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))              
 AND ((  G.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))            
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )            
        
END              
          
/*************************************************          
    헤더  -  의뢰일자별           
*************************************************/                
          
ELSE IF(RTRIM(@P_STATE) = '4')              
BEGIN              
           
 SELECT      
 DISTINCT 'N' S, A.DT_GIR              
 FROM      
 SA_GIRH A          
 INNER JOIN SA_GIRL B    ON A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY            
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY   AND A.CD_PLANT = C.CD_PLANT            
 LEFT OUTER JOIN SA_SOH E     ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO -- AND E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT             
 LEFT OUTER JOIN SA_SOL F      ON B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO  AND B.SEQ_SO = F.SEQ_SO               
 INNER JOIN MA_PITEM G  ON  A.CD_COMPANY = G.CD_COMPANY AND  A.CD_PLANT = G.CD_PLANT  AND B.CD_ITEM = G.CD_ITEM        
 WHERE            
 A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA            
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))            
 AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))           
 AND ((  G.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))             
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )            
          
    
END              
          
/*************************************************          
    헤더  -  출하형태별           
*************************************************/               
          
ELSE IF(RTRIM(@P_STATE) = '5')              
BEGIN              
          
 SELECT      
 DISTINCT 'N' S, B.TP_GI,              
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_QTIOTP              
   FROM    
 SA_GIRH A          
 INNER JOIN  SA_GIRL B ON  A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY                 
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY    AND A.CD_PLANT = C.CD_PLANT                
 INNER JOIN MA_PITEM D ON  A.CD_COMPANY = D.CD_COMPANY   AND A.CD_PLANT = D.CD_PLANT     AND B.CD_ITEM = D.CD_ITEM               
 LEFT OUTER JOIN SA_SOH E ON  B.CD_COMPANY = E.CD_COMPANY AND B.NO_SO = E.NO_SO-- AND E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT                
 LEFT OUTER JOIN SA_SOL F ON  B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO  AND B.SEQ_SO = F.SEQ_SO         
   WHERE    
 A.CD_COMPANY = @P_CD_COMPANY              
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO              
 AND  C.CD_BIZAREA = @P_CD_BIZAREA              
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))              
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))              
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))              
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))       
 AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))            
 AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))                     
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))             
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )                         
            
          
END              
          
/*************************************************          
    헤더  -  프로젝트별           
***********************************************/                
          
ELSE IF(RTRIM(@P_STATE) = '6')              
BEGIN              
    
 SELECT      
 DISTINCT 'N' S, B.NO_PROJECT,              
 (SELECT NM_PROJECT FROM SA_PROJECTH  
 WHERE NO_PROJECT = B.NO_PROJECT AND CD_COMPANY = B.CD_COMPANY   
 AND NO_SEQ = (SELECT MAX(NO_SEQ) FROM SA_PROJECTH WHERE NO_PROJECT = B.NO_PROJECT AND CD_COMPANY = B.CD_COMPANY)) NM_PROJECT              
 FROM    
 SA_GIRH A          
 INNER JOIN  SA_GIRL B ON  A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY                 
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY    AND A.CD_PLANT = C.CD_PLANT                
 INNER JOIN MA_PITEM D ON  A.CD_COMPANY = D.CD_COMPANY   AND A.CD_PLANT = D.CD_PLANT     AND B.CD_ITEM = D.CD_ITEM               
 LEFT OUTER JOIN SA_SOH E ON  B.CD_COMPANY = E.CD_COMPANY AND B.NO_SO = E.NO_SO -- AND E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT                
 LEFT OUTER JOIN SA_SOL F ON  B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO  AND B.SEQ_SO = F.SEQ_SO         
   WHERE    
 A.CD_COMPANY = @P_CD_COMPANY              
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO              
 AND  C.CD_BIZAREA = @P_CD_BIZAREA              
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))              
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))              
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))              
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))       
 AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))            
 AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))              
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))             
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )                         
 AND RTRIM(RTRIM(ISNULL(B.NO_PROJECT,''))) <> ''  
   
     
END       
    
/**************************************************************************************************************          
    품목집계별            
**************************************************************************************************************/              
            
ELSE IF(RTRIM(@P_STATE) = '7')              
  BEGIN              
    
         
 IF(@P_UNIT = '002')              
    BEGIN      
    
    
    SELECT     
   B.CD_ITEM,           
   G.NM_ITEM,          
   G.STND_ITEM,          
   B.UNIT,    
   G.CLS_ITEM,                
   SUM(B.QT_GIR_IM) QT_GIR,               
   SUM(B.QT_GI_IM) QT_GI,               
   SUM(B.QT_GIR_IM - B.QT_GI_IM) QT_NOTPROC,    
   SUM(B.AM_GIR) AM_GIR,               
   SUM(B.AM_GIRAMT) AM_GIRAMT              
    FROM            
   SA_GIRH A           
   INNER JOIN SA_GIRL B            ON A.NO_GIR = B.NO_GIR  AND  A.CD_COMPANY = B.CD_COMPANY                 
   INNER JOIN MA_PLANT C        ON A.CD_COMPANY = C.CD_COMPANY  AND A.CD_PLANT = C.CD_PLANT             
   LEFT OUTER JOIN MM_EJTP D ON A.TP_GI = D.CD_QTIOTP AND D.CD_COMPANY = @P_CD_COMPANY              
   LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO  AND E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT                 
   LEFT OUTER JOIN SA_SOL F ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO    AND B.SEQ_SO = F.SEQ_SO          
   INNER JOIN MA_PITEM G  ON  A.CD_COMPANY = G.CD_COMPANY AND  A.CD_PLANT = G.CD_PLANT  AND B.CD_ITEM = G.CD_ITEM                  
    
    WHERE           
   A.CD_COMPANY = @P_CD_COMPANY              
   AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO              
   AND  C.CD_BIZAREA = @P_CD_BIZAREA              
   AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))              
   AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))              
   AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))              
   AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))              
   AND ((  G.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))             
   AND (( @P_STA_SO = 'Y' AND B.QT_GIR_IM = B.QT_GI_IM ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR_IM > B.QT_GI_IM ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )     
    
    GROUP BY     
   B.CD_ITEM,           
   G.NM_ITEM,          
   G.STND_ITEM,          
   B.UNIT,    
   G.CLS_ITEM                           
          
      END              
               
 ELSE       
            
  BEGIN              
    
  SELECT     
   B.CD_ITEM,           
   G.NM_ITEM,          
   G.STND_ITEM,          
   B.UNIT,    
   G.CLS_ITEM,                
   SUM(B.QT_GIR) QT_GIR,               
   SUM(B.QT_GI) QT_GI,               
   SUM(B.QT_GIR - B.QT_GI) QT_NOTPROC,    
   SUM(B.AM_GIR) AM_GIR,               
   SUM(B.AM_GIRAMT) AM_GIRAMT              
  FROM            
   SA_GIRH A           
   INNER JOIN SA_GIRL B            ON A.NO_GIR = B.NO_GIR  AND  A.CD_COMPANY = B.CD_COMPANY                 
   INNER JOIN MA_PLANT C        ON A.CD_COMPANY = C.CD_COMPANY  AND A.CD_PLANT = C.CD_PLANT             
   LEFT OUTER JOIN MM_EJTP D ON A.TP_GI = D.CD_QTIOTP AND D.CD_COMPANY = @P_CD_COMPANY              
   LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO AND E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT                  
   LEFT OUTER JOIN SA_SOL F ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO    AND B.SEQ_SO = F.SEQ_SO          
   INNER JOIN MA_PITEM G  ON  A.CD_COMPANY = G.CD_COMPANY AND  A.CD_PLANT = G.CD_PLANT  AND B.CD_ITEM = G.CD_ITEM                  
         
  WHERE           
   A.CD_COMPANY = @P_CD_COMPANY              
   AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO              
   AND  C.CD_BIZAREA = @P_CD_BIZAREA              
   AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))              
   AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))              
   AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))              
   AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))              
   AND ((  G.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))             
   AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )     
    
  GROUP BY     
   B.CD_ITEM,           
   G.NM_ITEM,          
   G.STND_ITEM,          
   B.UNIT,    
   G.CLS_ITEM                           
    
  END              
END              
    
GO
   
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRSCH_SELECT_L'))
DROP PROCEDURE UP_SA_GIRSCH_SELECT_L
GO
       
      
/*******************************************                        
**  System : 영업                      
**  Sub System : 출하                      
**  Page  : 출하의뢰관리(SA_GIRSCH)                      
**  Desc  :  납품의뢰현황 라인  
**  Return Values                        
**                        
**  작    성    자  :  오 성 영                     
**  작    성    일  :  2007-07-09                      
**  수    정    자  :                        
*********************************************                        
** Change History                     
**  2008-01-28 납품처 추가
**  2008-02-21 단가, 외화 단가 추가 및 수정
*********************************************/                    
CREATE PROC UP_SA_GIRSCH_SELECT_L          
(            
	   @P_CD_COMPANY  NVARCHAR(7),   --회사코드            
	   @P_KEY   NVARCHAR(20),  
	   @P_DT_GIR_FROM  NVARCHAR(8),            
	   @P_DT_GIR_TO   NVARCHAR(8),            
	   @P_CD_BIZAREA   NVARCHAR(7),  --사업장            
	   @P_TP_BUSI   NVARCHAR(3),  --거래구분            
	   @P_CD_PLANT   NVARCHAR(7), --공장            
	   @P_CD_SL   NVARCHAR(7),  --창고            
	   @P_STA_SO   NVARCHAR(3),  --처리상태            
	   @P_RET   NVARCHAR(3),  --출하구분            
	   @P_UNIT   NVARCHAR(3),  --단위선택(수배단위, 재고단위)            
	   @P_TP_SO   NVARCHAR(4), --수주유형            
	   @P_STATE   NCHAR(1),  --상태            
	   @P_FG_TRANSPORT NVARCHAR(3), -- 운송수단          
	   @P_CLS_ITEM NVARChAR(3) -- 계정구분  
)            
AS            
        
  
      
/**************************************************************************************************************        
                       의뢰번호별          
**************************************************************************************************************/            
IF(RTRIM(@P_STATE) = '0')            
BEGIN  
 IF(@P_UNIT = '001')          /* 수주단위 */  
 BEGIN            
  SELECT   
	B.NO_GIR, B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,  B.UNIT,  B.QT_GIR,B.QT_GI,B.CD_EXCH, B.QT_GIR_IM, B.AM_GIR, B.AM_GIRAMT,             
	B.STA_GIR,B.TP_BUSI,
	--B.UM,            
	CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM,   
	CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM_EX,     
	(SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,            
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI,            
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = B.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
	--(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,            
	(SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = E.TP_SO) NM_SO ,     --수주유형명            
	A.CD_PLANT, ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE F.NO_SO END ) NO_SO, E.FG_TRANSPORT, D.CLS_ITEM,   
	(SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,  
	B.DT_DUEDATE,  
	(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,
	B.GI_PARTNER,      
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER         
  FROM          
	   SA_GIRH A              
		   INNER JOIN SA_GIRL B     ON  A.NO_GIR = B.NO_GIR AND  A.CD_COMPANY = B.CD_COMPANY            
		   INNER JOIN MA_PLANT C  ON  A.CD_COMPANY = C.CD_COMPANY AND  A.CD_PLANT = C.CD_PLANT             
		   INNER JOIN MA_PITEM D  ON  A.CD_COMPANY = D.CD_COMPANY AND B.CD_ITEM = D.CD_ITEM AND  A.CD_PLANT = D.CD_PLANT    
		   LEFT OUTER JOIN SA_SOH E     ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO -- AND E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT         
		   LEFT OUTER JOIN SA_SOL F      ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO AND B.SEQ_SO = F.SEQ_SO                
  WHERE         
	   A.CD_COMPANY = @P_CD_COMPANY    
	   AND B.NO_GIR = @P_KEY  
	   AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
	   AND  C.CD_BIZAREA = @P_CD_BIZAREA            
	   AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
	   AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
	   AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
	   AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
	   AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))            
	   AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
	   AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))            
	   AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )          
 END   
  
           
 ELSE IF (@P_UNIT = '002')  /* 라인 - 재고단위 */  
 BEGIN            
  
    
  
--  B.NO_GIR, B.CD_ITEM, D.NM_ITEM, D.STND_ITEM, B.UNIT, B.QT_GIR_IM AS QT_GIR, B.QT_GI_IM AS QT_GI, B.CD_EXCH,            
--  CONVERT(numeric(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,            
--  B.AM_GIR, B.AM_GIRAMT, B.STA_GIR, B.TP_BUSI,            
--  (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,            
--  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI,            
--  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
--  --(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,           
--  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
--  (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,            
--  (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = E.TP_SO) NM_SO,      --수주유형명            
--  A.CD_PLANT,  ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE F.NO_SO END ) NO_SO, E.FG_TRANSPORT, D.CLS_ITEM  
  
  SELECT   
	B.NO_GIR, B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,  B.UNIT, B.QT_GIR_IM AS QT_GIR, B.QT_GI_IM AS QT_GI, B.CD_EXCH, B.QT_GIR_IM, B.AM_GIR, B.AM_GIRAMT,             
	B.STA_GIR,B.TP_BUSI, 
	CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,  
	CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM_EX,               
	(SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,            
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI,            
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = B.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
	--(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,            
	(SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = E.TP_SO) NM_SO ,     --수주유형명            
	A.CD_PLANT, ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE F.NO_SO END ) NO_SO, E.FG_TRANSPORT, D.CLS_ITEM,  
	(SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,  
	B.DT_DUEDATE,  
	(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,
	B.GI_PARTNER,      
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER                      
  FROM          
	SA_GIRH A              
		INNER JOIN SA_GIRL B     ON  A.NO_GIR = B.NO_GIR AND  A.CD_COMPANY = B.CD_COMPANY            
		INNER JOIN MA_PLANT C  ON  A.CD_COMPANY = C.CD_COMPANY AND  A.CD_PLANT = C.CD_PLANT             
		INNER JOIN MA_PITEM D  ON  A.CD_COMPANY = D.CD_COMPANY AND  A.CD_PLANT = D.CD_PLANT  AND B.CD_ITEM = D.CD_ITEM                
		LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO  --AND  E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT           
		LEFT OUTER  JOIN SA_SOL F ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO    AND B.SEQ_SO = F.SEQ_SO        
  WHERE         
	   A.CD_COMPANY = @P_CD_COMPANY            
	   AND B.NO_GIR = @P_KEY  
	   AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
	   AND  C.CD_BIZAREA = @P_CD_BIZAREA            
	   AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
	   AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
	   AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
	   AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
	   AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))            
	   AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))   
	   AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))                    
	   AND (( @P_STA_SO = 'Y' AND B.QT_GIR_IM = B.QT_GI_IM ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR_IM > B.QT_GI_IM ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )   
 END            
END         
        
/**************************************************************************************************************        
                       품목코드별          
**************************************************************************************************************/            
ELSE IF((@P_STATE) = '1')            
BEGIN  
 IF(@P_UNIT = '001')            
  
 BEGIN            
  
 SELECT   
	B.CD_ITEM,  
	A.DT_GIR, B.NO_GIR, B.TP_GI, B.DT_DUEDATE, B.QT_GIR, B.QT_GI, B.CD_EXCH, B.QT_GIR_IM, B.AM_GIR, B.AM_GIRAMT,  B.SEQ_SO, B.STA_GIR, 
-- B.UM,            
	CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM,   
	CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM_EX,     
	( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE F.NO_SO END ) NO_SO,  
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
	(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,            
	(SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_QTIOTP,            
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
	D.NM_QTIOTP AS NM_TP_GI, E.FG_TRANSPORT, G.CLS_ITEM,  

	B.CD_SL, (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
	(SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = E.TP_SO) NM_SO,  
	(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI,  
	B.GI_PARTNER,      
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER             
FROM          
	 SA_GIRH A         
		 INNER JOIN SA_GIRL B           ON A.NO_GIR = B.NO_GIR  AND  A.CD_COMPANY = B.CD_COMPANY               
		 INNER JOIN MA_PLANT C        ON A.CD_COMPANY = C.CD_COMPANY  AND A.CD_PLANT = C.CD_PLANT           
		 LEFT OUTER JOIN MM_EJTP D ON B.TP_GI = D.CD_QTIOTP AND D.CD_COMPANY = @P_CD_COMPANY            
		 LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO AND  E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT           
		 LEFT OUTER  JOIN SA_SOL F ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO    AND B.SEQ_SO = F.SEQ_SO        
		 INNER JOIN MA_PITEM G  ON  A.CD_COMPANY = G.CD_COMPANY AND  A.CD_PLANT = G.CD_PLANT  AND B.CD_ITEM = G.CD_ITEM                
      
    WHERE         
       A.CD_COMPANY = @P_CD_COMPANY       
 AND B.CD_ITEM = @P_KEY       
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA            
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  G.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )          
        
    END            
      
    ELSE  IF (@P_UNIT = '002')                 
    BEGIN            
     
     SELECT  
-- B.CD_ITEM,      
-- A.DT_GIR, B.NO_GIR, B.TP_GI, B.DT_DUEDATE, B.QT_GIR_IM AS QT_GIR, B.QT_GI_IM AS QT_GI, B.CD_EXCH,             
-- CONVERT(numeric(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END) ) AS UM,             
-- B.AM_GIR, B.AM_GIRAMT, B.SEQ_SO, B.STA_GIR,        
-- ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE F.NO_SO END ) NO_SO,  
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
-- (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,            
-- (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_QTIOTP,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
-- D.NM_QTIOTP AS NM_TP_GI, E.FG_TRANSPORT, G.CLS_ITEM       
  
		B.CD_ITEM,  
		A.DT_GIR, B.NO_GIR, B.TP_GI, B.DT_DUEDATE, B.QT_GIR_IM AS QT_GIR, B.QT_GI_IM AS QT_GI, B.CD_EXCH, B.QT_GIR_IM, B.AM_GIR, B.AM_GIRAMT,  B.SEQ_SO, B.STA_GIR,  
		CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END) ) AS UM,            
		CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM_EX,     
		( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE F.NO_SO END ) NO_SO,  
		(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
		(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,            
		(SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_QTIOTP,            
		(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
		(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
		D.NM_QTIOTP AS NM_TP_GI, E.FG_TRANSPORT, G.CLS_ITEM,  

		B.CD_SL,
		(SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
		(SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = E.TP_SO) NM_SO,  
		(SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI, 
		A.CD_PARTNER, 
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,
		B.GI_PARTNER,      
		(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER         
	FROM          
		SA_GIRH A         
			INNER JOIN SA_GIRL B           ON A.NO_GIR = B.NO_GIR  AND  A.CD_COMPANY = B.CD_COMPANY               
			INNER JOIN MA_PLANT C        ON A.CD_COMPANY = C.CD_COMPANY  AND A.CD_PLANT = C.CD_PLANT           
			LEFT OUTER JOIN MM_EJTP D ON B.TP_GI = D.CD_QTIOTP AND D.CD_COMPANY = @P_CD_COMPANY            
			LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO  AND  E.TP_SO  = @P_TP_SO AND E.FG_TRANSPORT  =@P_FG_TRANSPORT                  
			LEFT OUTER  JOIN SA_SOL F ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO    AND B.SEQ_SO = F.SEQ_SO        
			INNER JOIN MA_PITEM G  ON  A.CD_COMPANY = G.CD_COMPANY AND  A.CD_PLANT = G.CD_PLANT  AND B.CD_ITEM = G.CD_ITEM                

	    WHERE         
	       A.CD_COMPANY = @P_CD_COMPANY    
	 AND B.CD_ITEM = @P_KEY          
	 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
	 AND  C.CD_BIZAREA = @P_CD_BIZAREA            
	 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
	 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
	 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
	 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
	 AND ((  G.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
	 AND (( @P_STA_SO = 'Y' AND B.QT_GIR_IM = B.QT_GI_IM ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR_IM > B.QT_GI_IM ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )   
        
    END            
END          
        
/******************************************************************************        
                거래처별         
******************************************************************************/            
        
ELSE IF(RTRIM(@P_STATE) = '2')            
  
BEGIN  
  IF (@P_UNIT = '001')    
               
  BEGIN            
    
 SELECT    
 A.CD_PARTNER, B.NO_GIR, A.DT_GIR, B.CD_ITEM, D.NM_ITEM, D.STND_ITEM, B.UNIT,  B.QT_GIR, B.QT_GI,             
 B.CD_EXCH,B.QT_GIR_IM, B.AM_GIR,B.AM_GIRAMT, ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,  
 B.SEQ_SO, B.STA_GIR,
--B.UM,            
CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM,   
CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM_EX,    
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
 A.TP_GI, (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TPGI,   
 -- E.NM_QTIOTP AS NM_TP_GI,  
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
  F.FG_TRANSPORT,D.CLS_ITEM,              
  
 (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
 (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = F.TP_SO) NM_SO,  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI,    
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,   
 B.DT_DUEDATE,  
 (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,
B.GI_PARTNER,      
(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER         
FROM         
 SA_GIRH A        
 INNER JOIN  SA_GIRL B ON  A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY               
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY    AND A.CD_PLANT = C.CD_PLANT              
 INNER JOIN MA_PITEM D ON  A.CD_COMPANY = D.CD_COMPANY   AND A.CD_PLANT = D.CD_PLANT  and B.CD_ITEM = D.CD_ITEM             
 LEFT OUTER JOIN MM_EJTP E ON  A.TP_GI  = E.CD_QTIOTP AND E.CD_COMPANY = @P_CD_COMPANY           
 LEFT OUTER JOIN SA_SOH F ON  B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO             
 LEFT OUTER JOIN SA_SOL G ON  B.CD_COMPANY = G.CD_COMPANY AND B.NO_SO = G.NO_SO  AND B.SEQ_SO = G.SEQ_SO                
  WHERE         
 A.CD_COMPANY = @P_CD_COMPANY            
 AND A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND C.CD_BIZAREA = @P_CD_BIZAREA    
 AND A.CD_PARTNER = @P_KEY          
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  F.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))            
 AND ((  F.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR_IM = B.QT_GI_IM ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR_IM > B.QT_GI_IM ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )   
  
  END            
       
  ELSE  IF (@P_UNIT = '002')         
   
  BEGIN            
           
--  SELECT    
-- A.CD_PARTNER,B.NO_GIR, A.DT_GIR,B.CD_ITEM, D.NM_ITEM, D.STND_ITEM, B.UNIT, B.QT_GIR_IM AS QT_GIR,             
-- B.QT_GI_IM AS QT_GI, B.CD_EXCH, B.AM_GIR, B.AM_GIRAMT,( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO, B.SEQ_SO, B.STA_GIR,            
-- CONVERT(numeric(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END) ) AS UM,             
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
-- --(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
-- A.TP_GI,  
-- --E.NM_QTIOTP AS NM_TP_GI,   
-- (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TPGI,   
-- F.FG_TRANSPORT,  
-- D.CLS_ITEM     
  
 SELECT  
 A.CD_PARTNER, B.NO_GIR, A.DT_GIR, B.CD_ITEM, D.NM_ITEM, D.STND_ITEM, B.UNIT,  B.QT_GIR_IM AS QT_GIR, B.QT_GI_IM AS QT_GI,             
 B.CD_EXCH,B.QT_GIR_IM, B.AM_GIR,B.AM_GIRAMT, ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,  
 B.SEQ_SO, B.STA_GIR,
CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,  
CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM_EX,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
 A.TP_GI, (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TPGI,   
 -- E.NM_QTIOTP AS NM_TP_GI,  
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
  F.FG_TRANSPORT,D.CLS_ITEM,  
  
 (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
 (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = F.TP_SO) NM_SO,  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI,    
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,   
 B.DT_DUEDATE,  
 (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,
B.GI_PARTNER,      
(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER         
FROM         
 SA_GIRH A        
 INNER JOIN  SA_GIRL B ON  A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY               
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY    AND A.CD_PLANT = C.CD_PLANT              
 INNER JOIN MA_PITEM D ON  A.CD_COMPANY = D.CD_COMPANY   AND A.CD_PLANT = D.CD_PLANT     AND B.CD_ITEM = D.CD_ITEM             
 LEFT OUTER JOIN MM_EJTP E ON  A.TP_GI  = E.CD_QTIOTP  AND E.CD_COMPANY = @P_CD_COMPANY               
 LEFT OUTER JOIN SA_SOH F ON  B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO             
 LEFT OUTER JOIN SA_SOL G ON  B.CD_COMPANY = G.CD_COMPANY AND B.NO_SO = G.NO_SO  AND B.SEQ_SO = G.SEQ_SO                
  WHERE         
 A.CD_COMPANY = @P_CD_COMPANY            
 AND A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND C.CD_BIZAREA = @P_CD_BIZAREA       
 AND A.CD_PARTNER = @P_KEY               
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  F.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))            
 AND ((  F.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR_IM = B.QT_GI_IM ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR_IM > B.QT_GI_IM ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )   
  
  
 END            
  
END  
  
        
/*************************************************        
    영업그룹별         
*************************************************/              
        
ELSE IF(RTRIM(@P_STATE) = '3')       
   
BEGIN  
  
  IF(@P_UNIT = '001')      
        
  BEGIN            
          
SELECT    
 A.DT_GIR,             
 B.NO_GIR,             
 B.CD_ITEM,             
 B.DT_DUEDATE,             
 B.QT_GIR,            
 B.QT_GI,            
 B.CD_EXCH,             
 B.AM_GIR,            
 B.AM_GIRAMT,             
 ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE F.NO_SO END ) NO_SO,  
 B.SEQ_SO,             
 B.STA_GIR,             
 B.CD_SALEGRP,            
 CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM,   
CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM_EX,          
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
 (SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
 A.TP_GI,  
 --D.NM_QTIOTP AS NM_TP_GI,  
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
 E.FG_TRANSPORT, G.CLS_ITEM,  
  
 (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
 (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = E.TP_SO) NM_SO,  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI, 
 A.CD_PARTNER,   
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,   
 A.DT_GIR,  B.CD_ITEM, G.NM_ITEM, G.STND_ITEM,  B.UNIT,
	B.GI_PARTNER,      
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER                             
   FROM   
 SA_GIRH A         
 INNER JOIN SA_GIRL B           ON A.NO_GIR = B.NO_GIR  AND  A.CD_COMPANY = B.CD_COMPANY               
 INNER JOIN MA_PLANT C        ON A.CD_COMPANY = C.CD_COMPANY  AND A.CD_PLANT = C.CD_PLANT            
 LEFT OUTER JOIN MM_EJTP D ON A.TP_GI = D.CD_QTIOTP AND D.CD_COMPANY = @P_CD_COMPANY            
 LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO              
 LEFT OUTER JOIN SA_SOL F ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO    AND B.SEQ_SO = F.SEQ_SO        
 INNER JOIN MA_PITEM G  ON  A.CD_COMPANY = G.CD_COMPANY AND  A.CD_PLANT = G.CD_PLANT  AND B.CD_ITEM = G.CD_ITEM    
   WHERE         
       A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA      
 AND  B.CD_SALEGRP = @P_KEY  
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))          
 AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
 AND ((  G.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )          
        
  END            
         
  ELSE IF (@P_UNIT = '002')        
      
  BEGIN            
         
-- SELECT    
-- A.DT_GIR,             
-- B.NO_GIR,             
-- B.CD_ITEM,             
-- B.DT_DUEDATE,             
-- B.QT_GIR_IM AS QT_GIR,            
-- B.QT_GI_IM AS QT_GI,            
-- B.CD_EXCH,             
-- B.AM_GIR,            
-- B.AM_GIRAMT,             
-- ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE F.NO_SO END ) NO_SO,  
-- B.SEQ_SO,             
-- B.STA_GIR,             
-- B.CD_SALEGRP,            
-- CONVERT(numeric(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,             
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
-- (SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
-- A.TP_GI,   
-- --D.NM_QTIOTP AS NM_TP_GI,   
-- (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
-- E.FG_TRANSPORT, G.CLS_ITEM  
--  
  
  
SELECT  
  
A.DT_GIR,             
 B.NO_GIR,             
 B.CD_ITEM,             
 B.DT_DUEDATE,             
 B.QT_GIR_IM AS QT_GIR,            
 B.QT_GI_IM AS QT_GI,              
 B.CD_EXCH,             
 B.AM_GIR,            
 B.AM_GIRAMT,             
 ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE F.NO_SO END ) NO_SO,  
 B.SEQ_SO,             
 B.STA_GIR,             
 B.CD_SALEGRP,            
CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,  
CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM_EX,       
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
 (SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
 A.TP_GI,  
 --D.NM_QTIOTP AS NM_TP_GI,  
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
 E.FG_TRANSPORT, G.CLS_ITEM,  
  
 (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
 (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = E.TP_SO) NM_SO,  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI, 
 A.CD_PARTNER,   
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,   
 A.DT_GIR,  B.CD_ITEM, G.NM_ITEM, G.STND_ITEM,  B.UNIT,
	B.GI_PARTNER,      
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER           
   FROM          
 SA_GIRH A         
 INNER JOIN SA_GIRL B           ON A.NO_GIR = B.NO_GIR  AND  A.CD_COMPANY = B.CD_COMPANY               
 INNER JOIN MA_PLANT C        ON A.CD_COMPANY = C.CD_COMPANY  AND A.CD_PLANT = C.CD_PLANT               
 LEFT OUTER JOIN MM_EJTP D ON A.TP_GI = D.CD_QTIOTP AND D.CD_COMPANY = @P_CD_COMPANY            
 LEFT OUTER JOIN SA_SOH E ON B.CD_COMPANY = E.CD_COMPANY  AND B.NO_SO = E.NO_SO              
 LEFT OUTER JOIN SA_SOL F ON B.CD_COMPANY = F.CD_COMPANY  AND B.NO_SO = F.NO_SO    AND B.SEQ_SO = F.SEQ_SO        
 INNER JOIN MA_PITEM G  ON  A.CD_COMPANY = G.CD_COMPANY AND  A.CD_PLANT = G.CD_PLANT  AND B.CD_ITEM = G.CD_ITEM    
             
    WHERE         
       A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA    
 AND  B.CD_SALEGRP = @P_KEY          
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  E.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))            
 AND ((  E.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))          
 AND ((  G.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR_IM = B.QT_GI_IM ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR_IM > B.QT_GI_IM ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )   
  
  END            
END  
  
       
        
/*************************************************        
    헤더  -  의뢰일자별         
*************************************************/              
        
ELSE IF(RTRIM(@P_STATE) = '4')            
      
BEGIN  
    
  IF(@P_UNIT ='001')            
  BEGIN    
          
   SELECT  
 A.DT_GIR,             
 B.NO_GIR,             
 B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,            
 B.DT_DUEDATE,             
 B.QT_GIR,             
 B.QT_GI,             
 B.CD_EXCH,            
 B.QT_GIR_IM,            
 B.AM_GIR,             
 B.AM_GIRAMT,             
 ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,             
 B.SEQ_SO,             
 B.STA_GIR,            
CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM,   
CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM_EX,  
  B.UNIT,           
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,    
 A.CD_PARTNER,        
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,            
 --(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
 A.TP_GI,   
 --E.NM_QTIOTP AS NM_TP_GI,  
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
 F.FG_TRANSPORT, D.CLS_ITEM,  
  
 (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
 (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = F.TP_SO) NM_SO,  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI,    
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,   
 (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,
	B.GI_PARTNER,      
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER            
  FROM  
 SA_GIRH A        
 INNER JOIN  SA_GIRL B ON  A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY               
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY    AND A.CD_PLANT = C.CD_PLANT              
 INNER JOIN MA_PITEM D ON  A.CD_COMPANY = D.CD_COMPANY   AND A.CD_PLANT = D.CD_PLANT     AND B.CD_ITEM = D.CD_ITEM             
 LEFT OUTER JOIN MM_EJTP E ON  A.TP_GI  = E.CD_QTIOTP         
 LEFT OUTER JOIN SA_SOH F ON  B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO             
 LEFT OUTER JOIN SA_SOL G ON  B.CD_COMPANY = G.CD_COMPANY AND B.NO_SO = G.NO_SO  AND B.SEQ_SO = G.SEQ_SO      
  WHERE  
       A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA      
 AND A.DT_GIR = @P_KEY        
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  F.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))          
 AND ((  F.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )                       
          
  END           
          
  ELSE  IF (@P_UNIT = '002')          
         
  BEGIN            
         
-- SELECT    
-- A.DT_GIR,             
-- B.NO_GIR,             
-- B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,            
-- B.DT_DUEDATE,             
-- B.QT_GIR_IM AS QT_GIR,             
-- B.QT_GI_IM AS QT_GI,             
-- B.CD_EXCH,             
-- B.AM_GIR,             
-- B.AM_GIRAMT,             
-- ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,             
-- B.SEQ_SO,             
-- B.STA_GIR,            
-- CONVERT(numeric(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END) ) AS UM,             
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
-- (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
-- A.TP_GI,  
-- -- E.NM_QTIOTP AS NM_TP_GI,    
-- (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
-- F.FG_TRANSPORT, D.CLS_ITEM     
  
  SELECT  
 A.DT_GIR,             
 B.NO_GIR,             
 B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,            
 B.DT_DUEDATE,             
 B.QT_GIR_IM AS QT_GIR,             
 B.QT_GI_IM AS QT_GI,             
 B.CD_EXCH,            
 B.QT_GIR_IM,            
 B.AM_GIR,             
 B.AM_GIRAMT,             
 ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,             
 B.SEQ_SO,             
 B.STA_GIR,            
 
CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,  
CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM_EX,   
 B.UNIT,                     
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN, 
 A.CD_PARTNER,           
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,            
 --(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
 A.TP_GI,   
 --E.NM_QTIOTP AS NM_TP_GI,  
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
 F.FG_TRANSPORT, D.CLS_ITEM,  
  
 (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
 (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = F.TP_SO) NM_SO,  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI,    
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,   
 (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,
	B.GI_PARTNER,      
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER           
     
   FROM  
 SA_GIRH A        
 INNER JOIN  SA_GIRL B ON  A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY               
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY    AND A.CD_PLANT = C.CD_PLANT              
 INNER JOIN MA_PITEM D ON  A.CD_COMPANY = D.CD_COMPANY   AND A.CD_PLANT = D.CD_PLANT     AND B.CD_ITEM = D.CD_ITEM             
 LEFT OUTER JOIN MM_EJTP E ON  A.TP_GI  = E.CD_QTIOTP         
 LEFT OUTER JOIN SA_SOH F ON  B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO             
 LEFT OUTER JOIN SA_SOL G ON  B.CD_COMPANY = G.CD_COMPANY AND B.NO_SO = G.NO_SO  AND B.SEQ_SO = G.SEQ_SO       
   WHERE  
       A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA       
 AND A.DT_GIR = @P_KEY       
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  F.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))          
 AND ((  F.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR_IM = B.QT_GI_IM ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR_IM > B.QT_GI_IM ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )   
          
     
  END     
END  
         
          
        
/*************************************************        
 출하형태별         
*************************************************/             
        
ELSE IF(RTRIM(@P_STATE) = '5')            
  
BEGIN  
  
  IF(@P_UNIT='001')            
  BEGIN            
      
  SELECT    
 A.DT_GIR,             
 B.NO_GIR,             
 B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,            
 B.UNIT,             
 B.QT_GIR,             
 B.QT_GI,             
 B.CD_EXCH,            
 B.QT_GIR_IM,            
 B.AM_GIR,             
 B.AM_GIRAMT,             
 ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,             
 B.SEQ_SO,             
 B.STA_GIR,             
 B.TP_GI,            
CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM,   
CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM_EX,          
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
 --(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
 --E.NM_QTIOTP AS NM_TP_GI,   
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
 F.FG_TRANSPORT, D.CLS_ITEM,  
  
 (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
 (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = F.TP_SO) NM_SO,  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI,    
 B.DT_DUEDATE,  
 (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,  
 A.CD_PARTNER,
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,  
 A.DC_RMK,
	B.GI_PARTNER,      
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER           FROM  
 SA_GIRH A        
 INNER JOIN  SA_GIRL B ON  A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY               
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY    AND A.CD_PLANT = C.CD_PLANT              
 INNER JOIN MA_PITEM D ON  A.CD_COMPANY = D.CD_COMPANY   AND A.CD_PLANT = D.CD_PLANT     AND B.CD_ITEM = D.CD_ITEM             
 LEFT OUTER JOIN MM_EJTP E ON  A.TP_GI  = E.CD_QTIOTP         
 LEFT OUTER JOIN SA_SOH F ON  B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO             
 LEFT OUTER JOIN SA_SOL G ON  B.CD_COMPANY = G.CD_COMPANY AND B.NO_SO = G.NO_SO  AND B.SEQ_SO = G.SEQ_SO       
   WHERE  
         A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA     
 AND B.TP_GI = @P_KEY              
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  F.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))          
 AND ((  F.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )                       
               
  END            
         
  ELSE IF (@P_UNIT = '002')  
           
  BEGIN            
          
-- SELECT    
-- A.DT_GIR,             
-- B.NO_GIR,             
-- B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,            
-- B.UNIT,             
-- B.QT_GIR_IM AS QT_GIR,             
-- B.QT_GI_IM AS QT_GI,             
-- B.CD_EXCH,             
-- B.AM_GIR,             
-- B.AM_GIRAMT,             
-- ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,             
-- B.SEQ_SO,             
-- B.STA_GIR,             
-- B.TP_GI,            
-- CONVERT(numeric(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,             
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
-- --(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
-- --E.NM_QTIOTP AS NM_TP_GI,   
-- (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
--  F.FG_TRANSPORT, D.CLS_ITEM     
  
  
 SELECT    
 A.DT_GIR,             
 B.NO_GIR,             
 B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,   
 B.UNIT,             
  B.QT_GIR_IM AS QT_GIR,             
 B.QT_GI_IM AS QT_GI,             
 B.CD_EXCH,            
 B.QT_GIR_IM,            
 B.AM_GIR,             
 B.AM_GIRAMT,             
 ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,             
 B.SEQ_SO,             
 B.STA_GIR,             
 B.TP_GI,            
CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,  
CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM_EX,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
 --(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
 --E.NM_QTIOTP AS NM_TP_GI,   
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
 F.FG_TRANSPORT, D.CLS_ITEM,  
  
 (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
 (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = F.TP_SO) NM_SO,  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI,    
 B.DT_DUEDATE,  
 (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,  
 A.CD_PARTNER,
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,
	B.GI_PARTNER,      
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = B.GI_PARTNER AND CD_COMPANY = B.CD_COMPANY) GI_LN_PARTNER           
          
   FROM  
 SA_GIRH A        
 INNER JOIN  SA_GIRL B ON  A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY               
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY    AND A.CD_PLANT = C.CD_PLANT              
 INNER JOIN MA_PITEM D ON  A.CD_COMPANY = D.CD_COMPANY   AND A.CD_PLANT = D.CD_PLANT     AND B.CD_ITEM = D.CD_ITEM             
 LEFT OUTER JOIN MM_EJTP E ON  A.TP_GI  = E.CD_QTIOTP         
 LEFT OUTER JOIN SA_SOH F ON  B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO             
 LEFT OUTER JOIN SA_SOL G ON  B.CD_COMPANY = G.CD_COMPANY AND B.NO_SO = G.NO_SO  AND B.SEQ_SO = G.SEQ_SO       
   WHERE  
         A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA       
 AND B.TP_GI = @P_KEY                   
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  F.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))          
 AND ((  F.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR_IM = B.QT_GI_IM ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR_IM > B.QT_GI_IM ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )   
               
  END     
END  
         
           
        
/*************************************************        
  프로젝트별         
***********************************************/              
        
ELSE IF(RTRIM(@P_STATE) = '6')            
  
BEGIN  
  
  IF(@P_UNIT='001')            
  BEGIN            
  
 SELECT   
 A.DT_GIR,             
 B.NO_GIR,             
 B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,            
 B.DT_DUEDATE,             
 B.QT_GIR,             
 B.QT_GI,             
 B.CD_EXCH,            
 B.QT_GIR_IM,            
 B.AM_GIR,             
 B.AM_GIRAMT,             
 ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,             
 B.SEQ_SO,             
 B.STA_GIR,             
 B.NO_PROJECT,            
 CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM,   
CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR END)) AS UM_EX,
 B.UNIT,         
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
 --(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
 (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
 A.TP_GI,  
 --E.NM_QTIOTP AS NM_TP_GI,  
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
 F.FG_TRANSPORT, D.CLS_ITEM,  
  
 (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
 (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = F.TP_SO) NM_SO,  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI, 
 A.CD_PARTNER,   
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,
A.GI_PARTNER,      
(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = A.GI_PARTNER AND CD_COMPANY = A.CD_COMPANY) GI_LN_PARTNER                                     
  FROM  
 SA_GIRH A        
 INNER JOIN  SA_GIRL B ON  A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY               
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY    AND A.CD_PLANT = C.CD_PLANT              
 INNER JOIN MA_PITEM D ON  A.CD_COMPANY = D.CD_COMPANY   AND A.CD_PLANT = D.CD_PLANT     AND B.CD_ITEM = D.CD_ITEM             
 LEFT OUTER JOIN MM_EJTP E ON  A.TP_GI  = E.CD_QTIOTP         
 LEFT OUTER JOIN SA_SOH F ON  B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO             
 LEFT OUTER JOIN SA_SOL G ON  B.CD_COMPANY = G.CD_COMPANY AND B.NO_SO = G.NO_SO  AND B.SEQ_SO = G.SEQ_SO       
   WHERE  
         A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA      
 AND B.NO_PROJECT = @P_KEY       
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  F.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))          
 AND ((  F.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR = B.QT_GI ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR > B.QT_GI ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )                       
 --AND B.NO_PROJECT IS NOT NULL  
                   
  END            
     
  ELSE IF (@P_UNIT = '002')        
         
  BEGIN            
           
--          
--  SELECT   
-- A.DT_GIR,             
-- B.NO_GIR,             
-- B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,            
-- B.DT_DUEDATE,             
-- B.QT_GIR_IM AS QT_GIR,             
-- B.QT_GI_IM AS QT_GI,             
-- B.CD_EXCH,             
-- B.AM_GIR,             
-- B.AM_GIRAMT,             
-- ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,             
-- B.SEQ_SO,             
-- B.STA_GIR,             
-- B.NO_PROJECT,            
-- CONVERT(numeric(17,4),  (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,             
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
-- (SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
-- (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,            
-- (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
-- A.TP_GI,  
-- --E.NM_QTIOTP AS NM_TP_GI,    
-- (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
-- F.FG_TRANSPORT, D.CLS_ITEM      
  
  
  
SELECT   
 A.DT_GIR,             
 B.NO_GIR,             
 B.CD_ITEM, D.NM_ITEM, D.STND_ITEM,            
 B.DT_DUEDATE,             
 B.QT_GIR_IM AS QT_GIR,             
 B.QT_GI_IM AS QT_GI,                   
 B.CD_EXCH,            
 B.QT_GIR_IM,            
 B.AM_GIR,             
 B.AM_GIRAMT,             
 ( CASE @P_RET  WHEN 'Y' THEN B.NO_SO_MGMT ELSE G.NO_SO END ) NO_SO,             
 B.SEQ_SO,             
 B.STA_GIR,             
 B.NO_PROJECT,            
CONVERT(NUMERIC(17,4), (B.AM_GIR /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM,  
CONVERT(NUMERIC(17,4), (B.AM_GIRAMT /  CASE ISNULL(B.QT_GIR_IM, 1) WHEN 0.00 THEN 1 ELSE B.QT_GIR_IM END)) AS UM_EX, 
 B.UNIT,                  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = A.CD_COMPANY AND CD_SYSDEF = A.YN_RETURN AND CD_FIELD = 'PU_C000027')NM_RETURN,            
 --(SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = B.CD_COMPANY AND CD_ITEM = B.CD_ITEM AND CD_PLANT = A.CD_PLANT) NM_ITEM,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'MA_B000005' AND CD_SYSDEF = B.CD_EXCH AND CD_COMPANY = B.CD_COMPANY) NM_EXCH,            
 (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = B.CD_SALEGRP AND CD_COMPANY = B.CD_COMPANY) NM_SALEGRP,            
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'SA_B000020' AND CD_SYSDEF = B.STA_GIR AND CD_COMPANY = B.CD_COMPANY) NM_STA_GIR,             
 A.TP_GI,  
 --E.NM_QTIOTP AS NM_TP_GI,  
 (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = B.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_TP_GI,   
 F.FG_TRANSPORT, D.CLS_ITEM,  
  
 (SELECT NM_SL FROM MA_SL WHERE CD_SL = B.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_COMPANY = A.CD_COMPANY) NM_SL,   
 (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = F.TP_SO) NM_SO,  
 (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'PU_C000016' AND CD_SYSDEF = B.TP_BUSI AND CD_COMPANY = B.CD_COMPANY) NM_BUSI, 
 A.CD_PARTNER,   
 (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER,
A.GI_PARTNER,      
(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_PARTNER = A.GI_PARTNER AND CD_COMPANY = A.CD_COMPANY) GI_LN_PARTNER              
              
          
 FROM  
 SA_GIRH A        
 INNER JOIN  SA_GIRL B ON  A.NO_GIR = B.NO_GIR   AND  A.CD_COMPANY = B.CD_COMPANY               
 INNER JOIN MA_PLANT C ON  A.CD_COMPANY = C.CD_COMPANY    AND A.CD_PLANT = C.CD_PLANT              
 INNER JOIN MA_PITEM D ON  A.CD_COMPANY = D.CD_COMPANY   AND A.CD_PLANT = D.CD_PLANT     AND B.CD_ITEM = D.CD_ITEM             
 LEFT OUTER JOIN MM_EJTP E ON  A.TP_GI  = E.CD_QTIOTP         
 LEFT OUTER JOIN SA_SOH F ON  B.CD_COMPANY = F.CD_COMPANY AND B.NO_SO = F.NO_SO             
 LEFT OUTER JOIN SA_SOL G ON  B.CD_COMPANY = G.CD_COMPANY AND B.NO_SO = G.NO_SO  AND B.SEQ_SO = G.SEQ_SO       
   WHERE  
         A.CD_COMPANY = @P_CD_COMPANY            
 AND  A.DT_GIR BETWEEN @P_DT_GIR_FROM AND @P_DT_GIR_TO            
 AND  C.CD_BIZAREA = @P_CD_BIZAREA   
 AND B.NO_PROJECT = @P_KEY                
 AND ((  A.TP_BUSI  =@P_TP_BUSI) OR (@P_TP_BUSI = '' ) OR (@P_TP_BUSI IS NULL))            
 AND ((  A.CD_PLANT  =@P_CD_PLANT) OR (@P_CD_PLANT = '' ) OR (@P_CD_PLANT IS NULL))            
 AND ((  B.CD_SL  =@P_CD_SL) OR (@P_CD_SL = '' ) OR (@P_CD_SL IS NULL))            
 AND ((  B.RET  =@P_RET) OR (@P_RET = '' ) OR (@P_RET IS NULL))            
 AND ((  F.TP_SO  = @P_TP_SO) OR (@P_TP_SO = '' ) OR (@P_TP_SO IS NULL))          
 AND ((  F.FG_TRANSPORT  =@P_FG_TRANSPORT) OR (@P_FG_TRANSPORT = '' ) OR (@P_FG_TRANSPORT IS NULL))            
 AND ((  D.CLS_ITEM  =@P_CLS_ITEM) OR (@P_CLS_ITEM = '' ) OR (@P_CLS_ITEM IS NULL))           
 AND (( @P_STA_SO = 'Y' AND B.QT_GIR_IM = B.QT_GI_IM ) OR ( @P_STA_SO = 'N'  AND B.QT_GIR_IM > B.QT_GI_IM ) OR (@P_STA_SO IS NULL) OR (@P_STA_SO = '') )   
 --AND B.NO_PROJECT IS NOT NULL  
                   
  END     
END  
GO
         
    
     
    
    
    
    
    
  
  
  
  