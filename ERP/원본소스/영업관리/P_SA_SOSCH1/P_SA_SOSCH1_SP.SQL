
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_SOSCH1_SELECT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_SOSCH1_SELECT
GO


/*******************************************              
**  System : 영업관리              
**  Sub System : 수주관리          
**  Page  : 수주현황  
**  Desc  :  수주현황 탭별 헤더 조회      
**              
**  Return Values              
**              
**  작    성    자  : 허성철              
**  작    성    일 : 07-04-05  
** Change History              
*********************************************              
*********************************************/      
CREATE PROCEDURE UP_SA_SOSCH1_SELECT
(      
 @P_CD_COMPANY   NVARCHAR(7),  --회사  
 @P_DT_SO_FROM NCHAR(8),  --수주일자FROM  
 @P_DT_SO_TO  NCHAR(8),  --수주일자TO  
 @P_CD_PLANT  NVARCHAR(7),  --공장  
 @P_CD_PARTNER NVARCHAR(7),  --거래처  
 @P_CD_SALEGRP NVARCHAR(7),  --영업그룹  
 @P_NO_EMP  NVARCHAR(10), --담당자  
 @P_TP_SO  NVARCHAR(4),  --수주형태  
 @P_STA_SO  NVARCHAR(3),  --수주상태  
 @P_TAB_GUBUN NCHAR(1)    --@P_TAB_GUBUN : 0-> 수주번호별, 1-> 거래처별, 2-> 품목별, 3-> 수주일자별, 4-> 수주형태별, 5-> 영업그룹별, 6->프로젝트별  
)  
AS  
SET NOCOUNT ON  
  
IF (@P_TAB_GUBUN = '0')   --수주번호별  
BEGIN  
 SELECT  'N' S,  --선택  
    A.NO_SO,  --수주번호  
    A.DT_SO,  --수주일자  
    (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = A.TP_SO) NM_SO, --수주형태명  
    A.CD_EXCH, --환종  
    A.RT_EXCH, --환율  
    (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = A.CD_SALEGRP) NM_SALEGRP, --영업그룹명  
    A.FG_TRANSPORT,  
    ISNULL(A.NO_PROJECT, '') NO_PROJECT,--프로젝트    
    A.FG_TAXP,  
    A.FG_BILL    
 FROM  SA_SOH A  
 INNER JOIN  
 (  
  SELECT NO_SO --수주번호  
  FROM SA_SOL   
  WHERE CD_COMPANY = @P_CD_COMPANY  
  AND  (CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)  
  AND  (STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)  
  GROUP BY NO_SO  
 ) B  
 ON   B.NO_SO = A.NO_SO  
 WHERE  A.CD_COMPANY = @P_CD_COMPANY  
 AND   A.DT_SO BETWEEN  @P_DT_SO_FROM AND @P_DT_SO_TO  
 AND   (A.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)  
 AND   (A.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)  
 AND   (A.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)  
 AND   (A.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)  
END  
ELSE IF (@P_TAB_GUBUN = '1') --거래처별  
BEGIN  
 SELECT  'N' S,  --선택  
    A.CD_PARTNER,  
    (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER --거래처명  
      
 FROM  SA_SOH A  
 INNER JOIN  
 (  
  SELECT NO_SO --수주번호  
  FROM SA_SOL   
  WHERE CD_COMPANY = @P_CD_COMPANY  
  AND  (CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)  
  AND  (STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)  
  GROUP BY NO_SO  
 ) B  
 ON   B.NO_SO = A.NO_SO  
 WHERE  A.CD_COMPANY = @P_CD_COMPANY  
 AND   A.DT_SO BETWEEN  @P_DT_SO_FROM AND @P_DT_SO_TO  
 AND   (A.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)  
 AND   (A.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)  
 AND   (A.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)  
 AND   (A.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)  
 GROUP BY A.CD_COMPANY, A.CD_PARTNER   
END  
ELSE IF (@P_TAB_GUBUN = '2') --품목별  
BEGIN  
 SELECT  'N' S,  --선택  
    B.CD_ITEM,  
    (SELECT NM_ITEM FROM MA_PITEM WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = B.CD_PLANT AND CD_ITEM = B.CD_ITEM) NM_ITEM, --품목명  
    (SELECT STND_ITEM FROM MA_PITEM WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = B.CD_PLANT AND CD_ITEM = B.CD_ITEM) STND_ITEM--규격  
      
 FROM  SA_SOH A  
 INNER JOIN  
 (  
  SELECT NO_SO,  --수주번호  
    CD_PLANT, --공장  
    CD_ITEM  --품목  
  FROM SA_SOL   
  WHERE CD_COMPANY = @P_CD_COMPANY  
  AND  (CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)  
  AND  (STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)  
  GROUP BY NO_SO, CD_PLANT, CD_ITEM  
 ) B  
 ON   B.NO_SO = A.NO_SO  
 WHERE  A.CD_COMPANY = @P_CD_COMPANY  
 AND   A.DT_SO BETWEEN  @P_DT_SO_FROM AND @P_DT_SO_TO  
 AND   (A.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)  
 AND   (A.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)  
 AND   (A.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)  
 AND   (A.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)  
 GROUP BY A.CD_COMPANY, B.CD_PLANT, B.CD_ITEM  
END  
ELSE IF (@P_TAB_GUBUN = '3') --수주일자별  
BEGIN  
 SELECT  'N' S,  --선택  
    A.DT_SO  --수주일자  
 FROM SA_SOH A  
 INNER JOIN  
 (  
  SELECT NO_SO --수주번호  
  FROM SA_SOL   
  WHERE CD_COMPANY = @P_CD_COMPANY  
  AND  (CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)  
  AND  (STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)  
  GROUP BY NO_SO  
 ) B  
 ON   B.NO_SO = A.NO_SO  
 WHERE  A.CD_COMPANY = @P_CD_COMPANY  
 AND   A.DT_SO BETWEEN  @P_DT_SO_FROM AND @P_DT_SO_TO  
 AND   (A.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)  
 AND   (A.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)  
 AND   (A.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)  
 AND   (A.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)  
 GROUP BY A.DT_SO  
END  
ELSE IF (@P_TAB_GUBUN = '4') --수주형태별  
BEGIN  
 SELECT  'N' S,  --선택  
    A.TP_SO,  --수주형태코드  
    (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = A.TP_SO) NM_SO  --수주형태명  
      
 FROM  SA_SOH A  
 INNER JOIN  
 (  
  SELECT NO_SO --수주번호  
  FROM SA_SOL   
  WHERE CD_COMPANY = @P_CD_COMPANY  
  AND  (CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)  
  AND  (STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)  
  GROUP BY NO_SO  
 ) B  
 ON   B.NO_SO = A.NO_SO  
 WHERE  A.CD_COMPANY = @P_CD_COMPANY  
 AND   A.DT_SO BETWEEN  @P_DT_SO_FROM AND @P_DT_SO_TO  
 AND   (A.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)  
 AND   (A.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)  
 AND   (A.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)  
 AND   (A.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)  
 GROUP BY A.CD_COMPANY, A.TP_SO  
END  
ELSE IF (@P_TAB_GUBUN = '5') --영업그룹별  
BEGIN  
 SELECT  'N' S,  --선택  
    A.CD_SALEGRP,  --영업그룹코드  
    (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = A.CD_SALEGRP) NM_SALEGRP --영업그룹명  
 FROM  SA_SOH A  
 INNER JOIN  
 (  
  SELECT NO_SO --수주번호  
  FROM SA_SOL   
  WHERE CD_COMPANY = @P_CD_COMPANY  
  AND  (CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)  
  AND  (STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)  
  GROUP BY NO_SO  
 ) B  
 ON   B.NO_SO = A.NO_SO  
 WHERE  A.CD_COMPANY = @P_CD_COMPANY  
 AND   A.DT_SO BETWEEN  @P_DT_SO_FROM AND @P_DT_SO_TO  
 AND   (A.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)  
 AND   (A.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)  
 AND   (A.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)  
 AND   (A.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)  
 GROUP BY A.CD_COMPANY, A.CD_SALEGRP  
END  
ELSE IF (@P_TAB_GUBUN = '6') --프로젝트별  
BEGIN  
 SELECT  'N' S,   --선택  
    A.NO_PROJECT, --프로젝트코드  
    (SELECT NM_PROJECT FROM SA_PROJECTH WHERE CD_COMPANY = A.CD_COMPANY AND NO_PROJECT = A.NO_PROJECT) NM_PROJECT --프로젝트명  
 FROM  SA_SOH A  
 INNER JOIN  
 (  
  SELECT NO_SO  --수주번호  
  FROM SA_SOL   
  WHERE CD_COMPANY = @P_CD_COMPANY  
  AND  (CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)  
  AND  (STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)  
  GROUP BY NO_SO  
 ) B  
 ON   B.NO_SO = A.NO_SO  
 WHERE  A.CD_COMPANY = @P_CD_COMPANY  
 AND   A.DT_SO BETWEEN  @P_DT_SO_FROM AND @P_DT_SO_TO  
 AND   (A.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)  
 AND   (A.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)  
 AND   (A.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)  
 AND   (A.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)  
 GROUP BY  A.CD_COMPANY, A.NO_PROJECT  
END  
  
SET NOCOUNT OFF  
  
GO

  
  
    
  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_SOSCH1_SELECT1') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_SOSCH1_SELECT1
GO
/*******************************************                
**  System : 영업관리                
**  Sub System : 수주관리            
**  Page  : 수주현황    
**  Desc  :  수주현황 탭별 라인 조회    
**                
**  Return Values                
**                
**  작    성    자  : 허성철                
**  작    성    일 : 07-04-04    
** Change History                
*********************************************                
*********************************************/        
CREATE PROCEDURE UP_SA_SOSCH1_SELECT1    
(        
 @P_CD_COMPANY   NVARCHAR(7),    
 @P_KEY   NVARCHAR(20),    
 @P_DT_SO_FROM NCHAR(8),  --수주일자FROM    
 @P_DT_SO_TO  NCHAR(8),  --수주일자TO    
 @P_CD_PLANT  NVARCHAR(7),  --공장    
 @P_CD_PARTNER NVARCHAR(7),  --거래처    
 @P_CD_SALEGRP NVARCHAR(7),  --영업그룹    
 @P_NO_EMP  NVARCHAR(10), --담당자    
 @P_TP_SO  NVARCHAR(4),  --수주형태    
 @P_STA_SO  NVARCHAR(3),  --수주상태    
 @P_TAB_GUBUN NCHAR(1)    --@P_TAB_GUBUN : 0-> 수주번호별, 1-> 거래처별, 2-> 품목별, 3-> 수주일자별, 4-> 수주형태별, 5-> 영업그룹별, 6->프로젝트별    
)    
AS    
SET NOCOUNT ON    
    
IF (@P_TAB_GUBUN = '0')   --수주번호별    
BEGIN    
 SELECT   A.NO_SO,  --수주번호    
     (SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT) NM_PLANT, --공장    
     A.DT_DUEDATE,--납기요구일    
     A.CD_ITEM, --품목코드    
     C.NM_ITEM, --품목명    
     C.STND_ITEM, --규격    
     C.UNIT_SO, --단위    
     A.QT_SO,  --수주수량    
     A.QT_GIR,  --의뢰수량    
     A.QT_GI,  --출하수량    
     -(A.QT_RETURN) QT_RETURN, --반품수량    
     A.QT_IV ,  --매출수량    
     A.UM_SO,  --단가    
     A.AM_SO,  --금액    
     A.AM_WONAMT, --원화금액    
     A.STA_SO,  --수주상태    
     H.CD_PARTNER,    
     (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = H.CD_COMPANY AND CD_PARTNER = H.CD_PARTNER) LN_PARTNER    
 FROM   SA_SOL A    
 INNER JOIN SA_SOH H    
 ON    H.CD_COMPANY = A.CD_COMPANY    
 AND    H.NO_SO = A.NO_SO    
 LEFT OUTER JOIN MA_PITEM C    
 ON    C.CD_COMPANY = A.CD_COMPANY    
 AND    C.CD_PLANT = A.CD_PLANT    
 AND    C.CD_ITEM = A.CD_ITEM    
 WHERE   A.CD_COMPANY = @P_CD_COMPANY    
 AND    A.NO_SO = @P_KEY    
 AND    (A.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)    
 AND    (A.STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)    
END    

ELSE IF (@P_TAB_GUBUN = '1') --거래처별    
BEGIN    
 SELECT   B.CD_PARTNER,--거래처    
     (SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT) NM_PLANT, --공장    
     A.NO_SO,  --수주번호    
     B.DT_SO,  --수주일자    
     A.DT_DUEDATE,--납기요구일    
     (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = B.CD_SALEGRP) NM_SALEGRP, --영업그룹명    
     A.CD_ITEM, --품목코드    
     C.NM_ITEM, --품목명    
     C.STND_ITEM, --규격    
     C.UNIT_SO, --단위    
     A.QT_SO,  --수주수량    
     A.QT_GIR,  --의뢰수량    
     A.QT_GI,  --출하수량    
     -(A.QT_RETURN) QT_RETURN, --반품수량    
     A.QT_IV ,  --매출수량    
     A.UM_SO,  --단가    
     A.AM_SO,  --금액    
     A.AM_WONAMT, --원화금액    
     B.CD_EXCH, --환종    
     A.STA_SO,  --수주상태    
     (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = B.CD_COMPANY AND CD_PARTNER = B.CD_PARTNER) LN_PARTNER,    
     B.FG_TRANSPORT,  
 B.RT_EXCH,     
  (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = B.CD_COMPANY AND TP_SO = B.TP_SO) NM_SO,  
 ISNULL(B.NO_PROJECT, '') NO_PROJECT,--프로젝트    
 B.FG_TAXP,  
 B.FG_BILL  
  
  
 FROM   SA_SOL A    
 INNER JOIN  SA_SOH B    
 ON    B.CD_COMPANY = A.CD_COMPANY    
 AND    B.NO_SO = A.NO_SO    
 AND    B.CD_PARTNER = @P_KEY    
 AND    B.DT_SO BETWEEN @P_DT_SO_FROM AND @P_DT_SO_TO    
 AND    (B.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)    
 AND    (B.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)    
 AND    (B.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)    
 AND    (B.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)    
 LEFT OUTER JOIN MA_PITEM C    
 ON    C.CD_COMPANY = A.CD_COMPANY    
 AND    C.CD_PLANT = A.CD_PLANT    
 AND    C.CD_ITEM = A.CD_ITEM    
 WHERE   A.CD_COMPANY = @P_CD_COMPANY    
 AND    (A.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)    
 AND    (A.STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)    
END    
ELSE IF (@P_TAB_GUBUN = '2') --품목별    
BEGIN    
 SELECT   A.CD_ITEM, --품목    
     (SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT) NM_PLANT, --공장    
     A.NO_SO,  --수주번호    
     B.DT_SO,  --수주일자    
     A.DT_DUEDATE,--납기요구일    
     (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = B.CD_SALEGRP) NM_SALEGRP, --영업그룹명    
     (SELECT UNIT_SO FROM MA_PITEM WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT AND CD_ITEM = A.CD_ITEM) UNIT_SO, --단위    
     A.QT_SO,  --수주수량    
     A.QT_GIR,  --의뢰수량    
     A.QT_GI,  --출하수량    
     -(A.QT_RETURN) QT_RETURN, --반품수량    
     A.QT_IV ,  --매출수량    
     A.UM_SO,  --단가    
     A.AM_SO,  --금액    
     A.AM_WONAMT, --원화금액    
     B.CD_EXCH, --환종    
     A.STA_SO,  --수주상태    
     B.CD_PARTNER,    
     (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = B.CD_COMPANY AND CD_PARTNER = B.CD_PARTNER) LN_PARTNER,    
     B.FG_TRANSPORT,  
 B.RT_EXCH,     
  (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = B.CD_COMPANY AND TP_SO = B.TP_SO) NM_SO,  
 ISNULL(B.NO_PROJECT, '') NO_PROJECT,--프로젝트    
 B.FG_TAXP,  
 B.FG_BILL    
 FROM   SA_SOL A    
 INNER JOIN  SA_SOH B    
 ON    B.CD_COMPANY = A.CD_COMPANY    
 AND    B.NO_SO = A.NO_SO    
 AND    B.DT_SO BETWEEN @P_DT_SO_FROM AND @P_DT_SO_TO    
 AND    (B.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)    
 AND    (B.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)    
 AND    (B.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)    
 AND    (B.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)    
 WHERE   A.CD_COMPANY = @P_CD_COMPANY    
 AND    A.CD_ITEM = @P_KEY    
 AND    (A.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)    
 AND    (A.STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)    
END    
ELSE IF (@P_TAB_GUBUN = '3') --수주일자별    
BEGIN    
 SELECT   B.DT_SO,  --수주일자    
     (SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT) NM_PLANT, --공장    
     A.NO_SO,  --수주번호    
     A.DT_DUEDATE,--납기요구일    
     (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = B.CD_SALEGRP) NM_SALEGRP, --영업그룹명    
     A.CD_ITEM, --품목코드    
     C.NM_ITEM, --품목명    
     C.STND_ITEM, --규격    
     C.UNIT_SO, --단위    
     A.QT_SO,  --수주수량    
     A.QT_GIR,  --의뢰수량    
     A.QT_GI,  --출하수량    
     -(A.QT_RETURN) QT_RETURN, --반품수량    
     A.QT_IV ,  --매출수량    
     A.UM_SO,  --단가    
     A.AM_SO,  --금액    
     A.AM_WONAMT, --원화금액    
     B.CD_EXCH, --환종    
     A.STA_SO,  --수주상태    
     B.CD_PARTNER,    
     (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = B.CD_COMPANY AND CD_PARTNER = B.CD_PARTNER) LN_PARTNER,    
     B.FG_TRANSPORT ,  
 B.RT_EXCH,     
  (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = B.CD_COMPANY AND TP_SO = B.TP_SO) NM_SO,  
 ISNULL(B.NO_PROJECT, '') NO_PROJECT,--프로젝트    
 B.FG_TAXP,  
 B.FG_BILL   
 FROM   SA_SOL A     
 INNER JOIN  SA_SOH B    
 ON    B.CD_COMPANY = A.CD_COMPANY    
 AND    B.NO_SO = A.NO_SO    
 AND    B.DT_SO = @P_KEY    
 AND    (B.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)    
 AND    (B.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)    
 AND    (B.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)    
 AND    (B.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)    
 LEFT OUTER JOIN MA_PITEM C    
 ON    C.CD_COMPANY = A.CD_COMPANY    
 AND    C.CD_PLANT = A.CD_PLANT    
 AND    C.CD_ITEM = A.CD_ITEM    
 WHERE   A.CD_COMPANY = @P_CD_COMPANY    
 AND    (A.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)    
 AND    (A.STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)    
END    
ELSE IF (@P_TAB_GUBUN = '4') --수주형태별    
BEGIN    
 SELECT   B.TP_SO,  --수주형태    
     (SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT) NM_PLANT, --공장    
     A.NO_SO,  --수주번호    
     B.DT_SO,  --수주일자    
     A.DT_DUEDATE,--납기요구일    
     (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = B.CD_SALEGRP) NM_SALEGRP, --영업그룹명    
     A.CD_ITEM, --품목코드    
     C.NM_ITEM, --품목명    
     C.STND_ITEM, --규격    
     C.UNIT_SO, --단위    
     A.QT_SO,  --수주수량    
     A.QT_GIR,  --의뢰수량    
     A.QT_GI,  --출하수량    
     -(A.QT_RETURN) QT_RETURN, --반품수량    
     A.QT_IV ,  --매출수량    
     A.UM_SO,  --단가    
     A.AM_SO,  --금액    
     A.AM_WONAMT, --원화금액    
     B.CD_EXCH, --환종    
     A.STA_SO,  --수주상태    
     B.CD_PARTNER,    
     (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = B.CD_COMPANY AND CD_PARTNER = B.CD_PARTNER) LN_PARTNER,    
     B.FG_TRANSPORT ,  
 B.RT_EXCH,     
  (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = B.CD_COMPANY AND TP_SO = B.TP_SO) NM_SO,  
 ISNULL(B.NO_PROJECT, '') NO_PROJECT,--프로젝트    
 B.FG_TAXP,  
 B.FG_BILL   
 FROM   SA_SOL A    
 INNER JOIN  SA_SOH B    
 ON    B.CD_COMPANY = A.CD_COMPANY    
 AND    B.NO_SO = A.NO_SO    
 AND    B.TP_SO = @P_KEY   AND    B.DT_SO BETWEEN @P_DT_SO_FROM AND @P_DT_SO_TO    
 AND    (B.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)    
 AND    (B.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)    
 AND    (B.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)    
 AND    (B.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)    
 LEFT OUTER JOIN MA_PITEM C    
 ON    C.CD_COMPANY = A.CD_COMPANY    
 AND    C.CD_PLANT = A.CD_PLANT    
 AND    C.CD_ITEM = A.CD_ITEM    
 WHERE   A.CD_COMPANY = @P_CD_COMPANY    
 AND    (A.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)    
 AND    (A.STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)    
END    
ELSE IF (@P_TAB_GUBUN = '5') --영업그룹별    
BEGIN    
 SELECT   B.CD_SALEGRP,--영업그룹    
     (SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT) NM_PLANT, --공장    
     A.NO_SO,  --수주번호    
     B.DT_SO,  --수주일자    
     A.DT_DUEDATE,--납기요구일    
     A.CD_ITEM, --품목코드    
     C.NM_ITEM, --품목명    
     C.STND_ITEM, --규격    
     C.UNIT_SO, --단위    
     A.QT_SO,  --수주수량    
     A.QT_GIR,  --의뢰수량    
     A.QT_GI,  --출하수량    
     -(A.QT_RETURN) QT_RETURN, --반품수량    
     A.QT_IV ,  --매출수량    
     A.UM_SO,  --단가    
     A.AM_SO,  --금액    
     A.AM_WONAMT, --원화금액    
     B.CD_EXCH, --환종    
     A.STA_SO,  --수주상태    
     B.CD_PARTNER,    
     (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = B.CD_COMPANY AND CD_PARTNER = B.CD_PARTNER) LN_PARTNER,    
     B.FG_TRANSPORT,  
 B.RT_EXCH,     
  (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = B.CD_COMPANY AND TP_SO = B.TP_SO) NM_SO,  
 ISNULL(B.NO_PROJECT, '') NO_PROJECT,--프로젝트    
 B.FG_TAXP,  
 B.FG_BILL    
 FROM   SA_SOL A    
 INNER JOIN  SA_SOH B    
 ON    B.CD_COMPANY = A.CD_COMPANY    
 AND    B.NO_SO = A.NO_SO    
 AND    B.CD_SALEGRP = @P_KEY    
 AND    B.DT_SO BETWEEN @P_DT_SO_FROM AND @P_DT_SO_TO    
 AND    (B.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)    
 AND    (B.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)    
 AND    (B.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)    
 AND    (B.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)    
 LEFT OUTER JOIN MA_PITEM C    
 ON    C.CD_COMPANY = A.CD_COMPANY    
 AND    C.CD_PLANT = A.CD_PLANT    
 AND    C.CD_ITEM = A.CD_ITEM    
 WHERE   A.CD_COMPANY = @P_CD_COMPANY    
 AND    (A.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)    
 AND    (A.STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)    
END    
ELSE IF (@P_TAB_GUBUN = '6') --프로젝트별    
BEGIN    
 SELECT   ISNULL(B.NO_PROJECT, '') NO_PROJECT,--프로젝트    
     (SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT) NM_PLANT, --공장    
     A.NO_SO,  --수주번호    
     B.DT_SO,  --수주일자    
     A.DT_DUEDATE,--납기요구일    
     (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = B.CD_SALEGRP) NM_SALEGRP, --영업그룹명    
     A.CD_ITEM, --품목코드    
     C.NM_ITEM, --품목명    
     C.STND_ITEM, --규격    
     C.UNIT_SO, --단위    
     A.QT_SO,  --수주수량    
     A.QT_GIR,  --의뢰수량    
     A.QT_GI,  --출하수량    
     -(A.QT_RETURN) QT_RETURN, --반품수량    
     A.QT_IV ,  --매출수량    
     A.UM_SO,  --단가    
     A.AM_SO,  --금액    
     A.AM_WONAMT, --원화금액    
     B.CD_EXCH, --환종    
     A.STA_SO,  --수주상태    
     B.CD_PARTNER,    
     (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = B.CD_COMPANY AND CD_PARTNER = B.CD_PARTNER) LN_PARTNER,    
     B.FG_TRANSPORT ,  
 B.RT_EXCH,     
  (SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = B.CD_COMPANY AND TP_SO = B.TP_SO) NM_SO,  
 ISNULL(B.NO_PROJECT, '') NO_PROJECT,--프로젝트    
 B.FG_TAXP,  
 B.FG_BILL   
 FROM   SA_SOL A    
 INNER JOIN  SA_SOH B    
 ON    B.CD_COMPANY = A.CD_COMPANY    
 AND    B.NO_SO = A.NO_SO    
 AND    B.NO_PROJECT = @P_KEY    
 AND    B.DT_SO BETWEEN @P_DT_SO_FROM AND @P_DT_SO_TO    
 AND    (B.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)    
 AND    (B.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)    
 AND    (B.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)    
 AND    (B.TP_SO = @P_TP_SO OR @P_TP_SO = '' OR @P_TP_SO IS NULL)    
 LEFT OUTER JOIN MA_PITEM C    
 ON    C.CD_COMPANY = A.CD_COMPANY    
 AND    C.CD_PLANT = A.CD_PLANT    
 AND    C.CD_ITEM = A.CD_ITEM    
 WHERE   A.CD_COMPANY = @P_CD_COMPANY    
 AND    (A.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)    
 AND    (A.STA_SO = @P_STA_SO OR @P_STA_SO = '' OR @P_STA_SO IS NULL)    
END    
    
SET NOCOUNT OFF    
    GO
    

