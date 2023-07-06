
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRR_REG_SELECT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRR_REG_SELECT
GO

/*******************************************    
**  System : 영업    
**  Sub System : 출하의뢰관리     
**  Page  : 출하반품의뢰등록    
**  Desc  : 출하반품의뢰등록  라인 조회(헤더, 라인의 스키마 정보도 가져온다)    
**    
**  Return Values    
**    
**  작    성    자  :     
**  작    성    일 :     
**  수    정    자     : 허성철    
*********************************************    
** Change History    
*********************************************    
*********************************************/    
CREATE      PROCEDURE UP_SA_GIRR_REG_SELECT    
(    
 @P_CD_COMPANY NVARCHAR(7),  
 @P_NO_GIR  NVARCHAR(20),  
 @P_CD_PLANT  NVARCHAR(7)  
)  
AS    
SET NOCOUNT ON  
  
SELECT   A.NO_GIR,  --의뢰번호  
    A.DT_GIR,   --의뢰일자   
    A.CD_PARTNER, --거래처  
    (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER, --거래처명  
    A.CD_PLANT,  --공장  
    A.TP_BUSI,  --거래구분  
    B.CD_SALEGRP, --영업그룹코드  
    (SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = B.CD_SALEGRP) NM_SALEGRP , --영업그룹명  
    A.NO_EMP,  --사원  
    (SELECT NM_KOR FROM MA_EMP WHERE CD_COMPANY = A.CD_COMPANY AND NO_EMP = A.NO_EMP) NM_KOR, --담당자명  
    B.TP_GI,   --출하형태(반품형태)  
    (SELECT NM_QTIOTP FROM MM_EJTP WHERE  CD_COMPANY = A.CD_COMPANY AND CD_QTIOTP = B.TP_GI) NM_GI, --출하형태명(반품형태명)  
    B.TP_VAT,  --과세구분  
    B.RT_VAT,  --과세구분  
    B.IV,    --유무환구분  
    B.CD_EXCH,  --환종  
    B.RT_EXCH,  --환율  
    B.FG_TAXP,  --처리구분  
    A.DC_RMK  --비고  
FROM   SA_GIRH A  
INNER JOIN    
(   
 SELECT  NO_GIR, CD_SALEGRP, TP_GI, TP_VAT, RT_VAT, IV, CD_EXCH, RT_EXCH, FG_TAXP  
 FROM  SA_GIRL   
 WHERE  CD_COMPANY = @P_CD_COMPANY  
 AND   NO_GIR = @P_NO_GIR  
 AND   RET = 'Y'  
 GROUP BY NO_GIR, CD_SALEGRP, TP_GI, TP_VAT, RT_VAT, IV, CD_EXCH, RT_EXCH, FG_TAXP  
) B  
ON    B.NO_GIR = A.NO_GIR  
WHERE   A.CD_COMPANY = @P_CD_COMPANY  
AND    A.NO_GIR = @P_NO_GIR  
ORDER BY  A.NO_GIR  
  
SELECT   A.SEQ_GIR,       --의뢰항번  
    A.CD_ITEM,       --품목코드  
    A.TP_ITEM,       --품목타입  
    A.DT_DUEDATE,      --납품요청일  
    A.DT_REQGI,       --출하예정일  
    A.YN_INSPECT,       --검사유무  
    A.CD_SL,        --창고코드  
    (SELECT NM_SL FROM MA_SL WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = B.CD_PLANT AND CD_SL = A.CD_SL GROUP BY CD_SL, NM_SL) NM_SL, --창고명  
    A.QT_GIR,        --반품수량  
    A.UM,        --단가(UI에서계산)  
    A.AM_GIR,       --금액(UI에서계산)  
    A.AM_GIRAMT,       --원화금액(UI에서계산)  
    A.AM_VAT,       --부가세(UI에서계산)   
    A.UNIT,        --재고단위  
    A.QT_GIR_IM,       --반품재고수량  
    A.GI_PARTNER,      --납품처  
    A.NO_PROJECT,      --프로젝트  
    (SELECT NM_PROJECT FROM SA_PROJECTH WHERE CD_COMPANY = A.CD_COMPANY AND NO_PROJECT = A.NO_PROJECT) NM_PROJECT, --프로젝트명      
    A.NO_EMP,       --담당자  
    B.NM_ITEM,       --품목명  
    B.STND_ITEM,       --규격  
    B.UNIT_SO_FACT,      --재고단위수량  
    A.NO_IO_MGMT,      --반품수불번호  
    A.NO_IOLINE_MGMT,     --반품수불항번  
    A.NO_SO_MGMT,      --반품수주번호  
    A.NO_SOLINE_MGMT,     --반품수주항번  
    A.QT_GI,        --반품출하수량  
    A.NO_LC,        --LC번호  
    A.SEQ_LC,        --LC항번  
    A.TP_IV,       --매출형태  
    A.DC_RMK  
FROM   SA_GIRL A  
LEFT OUTER JOIN MA_PITEM B  
ON    B.CD_COMPANY = A.CD_COMPANY  
AND    B.CD_PLANT = @P_CD_PLANT  
AND    B.CD_ITEM = A.CD_ITEM  
WHERE   A.CD_COMPANY = @P_CD_COMPANY  
AND    A.NO_GIR = @P_NO_GIR  
AND    A.RET = 'Y'  
ORDER BY  A.NO_GIR, A.SEQ_GIR  
  
SET NOCOUNT OFF  
  GO
  
  
  
  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRR_REG_SELECT1') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRR_REG_SELECT1
GO

  /*******************************************  
**  System : 영업  
**  Sub System : 납품의뢰관리  
**  Page  : 납품의뢰등록  
**  Desc  : 반품의뢰수량체크   
*********  모두 더한 반품출하수량과 새로운 반품의뢰수량을 더해서 그 값이 모두 더한 반품출하수량보다 작으면 수정 불가  
**  Return Values  
**  
**  작    성    자  :   
**  작    성    일 :   
**  수    정    자     : 허성철  
*********************************************  
** Change History  
*********************************************  
*********************************************/  
CREATE PROCEDURE UP_SA_GIRR_REG_SELECT1
(  
 @P_CD_COMPANY   NVARCHAR(7),  
 @P_NO_GIR   NVARCHAR(20),  
 @P_SEQ_GIR    NUMERIC(3),  
 @P_NO_IO    NVARCHAR(20),  
 @P_NO_IOLINE   NUMERIC(5),  
 @P_QT_NEW   NUMERIC(17, 4),  
 @P_RESULT   INT OUTPUT  
)  
AS  
SET NOCOUNT ON  
DECLARE  @V_QT_UNIT_MM  NUMERIC(17, 4),  
   @V_QT_GI   NUMERIC(17, 4)  
  
SET @V_QT_UNIT_MM = 0  
SET @P_RESULT = 0  
  
--출하수량을 변수에 저장한다.  
SELECT  @V_QT_GI = QT_GI  
FROM  SA_GIRL  
WHERE  CD_COMPANY = @P_CD_COMPANY  
AND   NO_GIR = @P_NO_GIR  
AND   SEQ_GIR = @P_SEQ_GIR  
  
--새로운 의뢰수량이 반품출하수량보다 작으면 리턴  
IF (@P_QT_NEW < @V_QT_GI)  
BEGIN  
 RETURN  
END  
  
--출하관리수량과 반품의뢰수량의 차를 변수에 저장한다.   
SELECT  @V_QT_UNIT_MM = QT_UNIT_MM - QT_RETURN_MM  
FROM  MM_QTIO  
WHERE  CD_COMPANY = @P_CD_COMPANY  
AND   NO_IO = @P_NO_IO  
AND   NO_IOLINE = @P_NO_IOLINE  
  
--새로운 의뢰수량과 기존의 의뢰수량의 차를 변수에 저장한다.  
SELECT  @P_QT_NEW = @P_QT_NEW - QT_GIR  
FROM  SA_GIRL  
WHERE  CD_COMPANY = @P_CD_COMPANY  
AND   NO_GIR = @P_NO_GIR  
AND   SEQ_GIR = @P_SEQ_GIR  
  
--출하수량과 반품수량의 차가 새로운 의뢰수량과 기존의 의뢰수량의 차보다 같거나 크면  
IF (@V_QT_UNIT_MM >= @P_QT_NEW)  
BEGIN  
 SET @P_RESULT = 1  
END  
  
SET NOCOUNT OFF  
   
  GO
  
  
  
  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRR_REG_DELETE') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRR_REG_DELETE
GO

    
/*******************************************    
**  System : 영업    
**  Sub System : 고객납품의뢰관리    
**  Page  : 고객납품의뢰등록    
**  Desc  : 반품의뢰라인 헤더 삭제    
********   반품의뢰항번에 따라 커서를 돌면서 의뢰라인을 하나씩 삭제한다.     
**  Return Values    
**    
**  작    성    자  :     
**  작    성    일 :     
**  수    정    자     : 허성철    
*********************************************    
** Change History    
*********************************************    
*********************************************/    
CREATE PROCEDURE UP_SA_GIRR_REG_DELETE
(    
 @P_CD_COMPANY  NVARCHAR(7),   --회사    
 @P_NO_GIR   NVARCHAR(20)   --의뢰번호    
)    
AS    
  
DECLARE   
  @V_NO_GIR_L  INT,  
  @V_NO_GIR_H  INT,  
  @ERRNO    INT,      
  @ERRMSG   NVARCHAR(255)  
  
 DELETE     
 FROM SA_GIRL    
 WHERE CD_COMPANY = @P_CD_COMPANY    
   AND  NO_GIR  = @P_NO_GIR    
  
  
  SELECT @V_NO_GIR_L = COUNT(NO_GIR)  
  FROM SA_GIRL  
  WHERE CD_COMPANY = @P_CD_COMPANY  AND   
    NO_GIR = @P_NO_GIR  
     
  
  IF(@V_NO_GIR_L > 0)  
  BEGIN  
   SELECT @ERRNO  = 100000, @ERRMSG = 'Line삭제가되지않았습니다 '   
   RETURN  
  END  
  
  
 DELETE     
 FROM SA_GIRH    
 WHERE CD_COMPANY = @P_CD_COMPANY  AND  
      NO_GIR  = @P_NO_GIR    
    
  
  SELECT @V_NO_GIR_H = COUNT(NO_GIR)  
  FROM SA_GIRH  
  WHERE CD_COMPANY = @P_CD_COMPANY  AND  
    NO_GIR = @P_NO_GIR  
  
  IF(@V_NO_GIR_H > 0)  
  BEGIN  
   SELECT @ERRNO  = 100000, @ERRMSG = 'Header삭제가되지않았습니다 '   
   RETURN  
  END  
    
  GO  
   
   
   
   
   
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRR_REG_INSERT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRR_REG_INSERT
GO

   
   
   /*******************************************  
**  System : 영업  
**  Sub System : 출하의뢰관리  
**  Page  : 고객납품의뢰등록  
**  Desc  : 반품의뢰 헤더 저장  
**  
**  Return Values  
**  
**  작    성    자  :   
**  작    성    일  :   
**  수    정    자  : 허성철  
*********************************************  
** Change History  
*********************************************  
*********************************************/  
CREATE PROCEDURE UP_SA_GIRR_REG_INSERT
(    
 @P_CD_COMPANY  NVARCHAR(7),  --회사  
 @P_NO_GIR   NVARCHAR(20), --의뢰번호  
 @P_DT_GIR   NVARCHAR(8),  --의뢰일자  
 @P_CD_PARTNER  NVARCHAR(7),  --거래처  
 @P_CD_PLANT   NVARCHAR(7),  --공장  
 @P_NO_EMP   NVARCHAR(20), --사번  
 @P_TP_BUSI   NVARCHAR(3),  --거래구분  
 @P_DC_RMK   NVARCHAR(100), --비고  
 @P_ID_INSERT   NVARCHAR(15)  --사용자  
)     
AS     
SET NOCOUNT ON  
  
DECLARE @V_DTS_INSERT VARCHAR(14)  
  
SET @V_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')  
  
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
 DC_RMK,   
 ID_INSERT,   
 DTS_INSERT  
)    
VALUES  
(  
 @P_CD_COMPANY,  
 @P_NO_GIR,   
 @P_DT_GIR,   
 @P_CD_PARTNER,  
 @P_CD_PLANT,   
 @P_NO_EMP,   
 @P_TP_BUSI,  
 'Y',  
 @P_DC_RMK,   
 @P_ID_INSERT,   
 @V_DTS_INSERT  
)    
  GO
  
    
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRR_REG_UPDATE') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRR_REG_UPDATE
GO

  
/*******************************************  
**  System : 영업  
**  Sub System : 출하의뢰관리   
**  Page  : 고객납품의뢰등록  
**  Desc  : 고객납품의뢰 헤더 수정  
**  
**  Return Values  
**  
**  작    성    자  :   
**  작    성    일 :   
**  수    정    자     : 허성철  
*********************************************  
** Change History  
*********************************************  
*********************************************/  
CREATE PROCEDURE UP_SA_GIRR_REG_UPDATE
(    
 @P_CD_COMPANY NVARCHAR(7),    
 @P_NO_GIR  NVARCHAR(20), --의뢰번호  
 @P_DT_GIR  NVARCHAR(8),  --의뢰일자  
 @P_CD_PARTNER NVARCHAR(7),  --거래처  
 @P_NO_EMP  NVARCHAR(10), --사번  
 @P_DC_RMK  NVARCHAR(100), --비고  
 @P_ID_UPDATE NVARCHAR(15)  --아이디  
)     
AS    
  
DECLARE @V_DTS_UPDATE VARCHAR(14)  
SET @V_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')  
  
UPDATE SA_GIRH  
SET  DT_GIR  = @P_DT_GIR,     
  NO_EMP  = @P_NO_EMP,    
  CD_PARTNER  = @P_CD_PARTNER,  
  DC_RMK  = @P_DC_RMK,     
  ID_UPDATE = @P_ID_UPDATE,  
  DTS_UPDATE = @V_DTS_UPDATE  
WHERE CD_COMPANY = @P_CD_COMPANY  
AND  NO_GIR  = @P_NO_GIR  
  
    GO
  
    
 
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRR_REG_INSERT1') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRR_REG_INSERT1
GO
 
  
  /*******************************************  
**  System : 영업  
**  Sub System : 출하의뢰관리   
**  Page  : 출하반품의뢰등록  
**  Desc  : 출하반품의뢰 라인 저장  
**  
**  Return Values  
**  
**  작    성    자  :   
**  작    성    일 :   
**  수    정    자     : 허성철  
*********************************************  
** Change History  
*********************************************  
*********************************************/  
CREATE PROCEDURE UP_SA_GIRR_REG_INSERT1
(    
 @P_CD_COMPANY  NVARCHAR(7),  --회사   
 @P_NO_GIR   NVARCHAR(20), --의뢰번호  
 @P_SEQ_GIR   NUMERIC(3),  --의뢰항번  
 @P_CD_ITEM   NVARCHAR(20), --품목코드   
 @P_TP_ITEM   NVARCHAR(3),  --품목타입  
 @P_DT_DUEDATE  NVARCHAR(8),  --납기요구일  
 @P_DT_REQGI   NVARCHAR(8),  --출하예정일  
 @P_YN_INSPECT  NCHAR(1),  --검사유무  
 @P_CD_SL   NVARCHAR(7),  --창고  
 @P_TP_GI   NVARCHAR(3),  --출하유형  
 @P_QT_GIR   NUMERIC(17, 4), --수량  
 @P_CD_EXCH   NVARCHAR(3),  --환종  
 @P_UM    NUMERIC(15, 4), --단가  
 @P_AM_GIR   NUMERIC(17, 4), --금액  
 @P_AM_GIRAMT  NUMERIC(17, 4), --원화금액  
 @P_AM_VAT   NUMERIC(17, 4), --부가세  
 @P_UNIT    NVARCHAR(3),  --재고단위  
 @P_QT_GIR_IM   NUMERIC(17, 4), --재고수량  
 @P_GI_PARTNER  NVARCHAR(7),  --납품처  
 @P_NO_PROJECT  NVARCHAR(20), --프로젝트  
 @P_CD_SALEGRP  NVARCHAR(7),  --영업그룹  
 @P_RT_EXCH   NUMERIC(15, 4), --환종  
 @P_RT_VAT   NUMERIC(5, 2),  --부가세율  
 @P_TP_VAT   NVARCHAR(3),  --과세구분  
 @P_NO_EMP   NVARCHAR(20), --담당자  
 @P_TP_IV    NVARCHAR(3),  --매출형태  
 @P_FG_TAXP   NVARCHAR(3),  --계산서처리방법  
 @P_TP_BUSI   NVARCHAR(3),  --거래구분   
 @P_IV    NCHAR(1),  --유무환구분(매출유무)   
 @P_NO_IO_MGMT  NVARCHAR(20), --관련수불번호   
 @P_NO_IOLINE_MGMT NUMERIC(3),  --관련수불항번  
 @P_NO_SO_MGMT  NVARCHAR(20), --관련수주번호  
 @P_NO_SOLINE_MGMT NUMERIC(3),  --관련수주항번  
 @P_NO_LC   NVARCHAR(20), --LC번호  
 @P_SEQ_LC   NUMERIC(3),  --LC항번  
 @P_ID_INSERT   NVARCHAR(15)  --아이디  
)     
AS     
SET NOCOUNT ON  
DECLARE @V_DTS_INSERT VARCHAR(14)  
  
SET @V_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')  
  
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
 CD_EXCH,  
 UM,  
 AM_GIR,  
 AM_GIRAMT,  
 AM_VAT,  
 UNIT,  
 QT_GIR_IM,  
 GI_PARTNER,   
 NO_PROJECT,  
 CD_SALEGRP,  
 RT_EXCH,  
 RT_VAT,  
 TP_VAT,  
 NO_EMP,  
 TP_IV,  
 FG_TAXP,  
 TP_BUSI,  
 NO_IO_MGMT,  
 NO_IOLINE_MGMT,  
 NO_SO_MGMT,  
 NO_SOLINE_MGMT,  
 GIR,      
 IV,  
 RET,    
 SUBCONT,  
 NO_LC,   
 SEQ_LC,    
 ID_INSERT,  
 DTS_INSERT  
)  
VALUES  
(  
 @P_CD_COMPANY,  
 @P_NO_GIR,  
 @P_SEQ_GIR,  
 @P_CD_ITEM,  
 @P_TP_ITEM,  
 @P_DT_DUEDATE,  
 @P_DT_REQGI,  
 @P_YN_INSPECT,  
 @P_CD_SL,  
 @P_TP_GI,  
 @P_QT_GIR,  
 @P_CD_EXCH,  
 @P_UM,  
 @P_AM_GIR,  
 @P_AM_GIRAMT,  
 @P_AM_VAT,  
 @P_UNIT,  
 @P_QT_GIR_IM,  
 @P_GI_PARTNER,   
 @P_NO_PROJECT,  
 @P_CD_SALEGRP,  
 @P_RT_EXCH,  
 @P_RT_VAT,  
 @P_TP_VAT,  
 @P_NO_EMP,  
 @P_TP_IV ,  
 @P_FG_TAXP,  
 @P_TP_BUSI,  
 @P_NO_IO_MGMT,  
 @P_NO_IOLINE_MGMT,  
 @P_NO_SO_MGMT,  
 @P_NO_SOLINE_MGMT,  
 'Y',    --의뢰유무  
 @P_IV,     
 'Y',    --반품유무  
 'N',    --외주유무  
 @P_NO_LC,   
 @P_SEQ_LC,    
 @P_ID_INSERT,  
 @V_DTS_INSERT  
)  
  
SET NOCOUNT OFF  
GO  
  
  
  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRR_REG_UPDATE1') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRR_REG_UPDATE1
GO

  
    
/*******************************************  
**  System : 영업  
**  Sub System : 출하의뢰관리   
**  Page  : 출하반품의뢰등록  
**  Desc  : 출하반품의뢰 라인 수정  
**  
**  Return Values  
**  
**  작    성    자  :   
**  작    성    일 :   
**  수    정    자     : 허성철  
*********************************************  
** Change History  
*********************************************  
*********************************************/ 
CREATE PROCEDURE UP_SA_GIRR_REG_UPDATE1
(  
 @P_CD_COMPANY  NVARCHAR(7),  
 @P_NO_GIR   NVARCHAR(20),  
 @P_SEQ_GIR   NUMERIC(3),  
 @P_CD_SL   NVARCHAR(7),  
 @P_QT_GIR   NUMERIC(17, 4),  
 @P_UM    NUMERIC(15, 4),  
 @P_AM_GIR   NUMERIC(17, 4),  
 @P_AM_GIRAMT  NUMERIC(17, 4),  
 @P_AM_VAT   NUMERIC(17, 4),  
 @P_QT_GIR_IM   NUMERIC(17, 4),  
 @P_NO_PROJECT  NVARCHAR(20),  
 @P_ID_UPDATE  NVARCHAR(15)  
)   
AS   
SET NOCOUNT ON  
DECLARE @P_DTS_UPDATE NVARCHAR(14)  
  
SET @P_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')  
  
UPDATE SA_GIRL  
SET  CD_SL   = @P_CD_SL,  
  QT_GIR   = @P_QT_GIR,  
  UM    = @P_UM,  
  AM_GIR   = @P_AM_GIR,  
  AM_GIRAMT  = @P_AM_GIRAMT,  
  AM_VAT   = @P_AM_VAT,  
  QT_GIR_IM  = @P_QT_GIR_IM,  
  NO_PROJECT  = @P_NO_PROJECT,  
  ID_UPDATE  = @P_ID_UPDATE,  
  DTS_UPDATE  = @P_DTS_UPDATE   
WHERE CD_COMPANY  = @P_CD_COMPANY  
AND  NO_GIR   = @P_NO_GIR  
AND  SEQ_GIR   = @P_SEQ_GIR  
  
  
SET NOCOUNT OFF  
  
 GO 
  
  
  
  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRR_REG_DELETE1') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRR_REG_DELETE1
GO

  
  /*******************************************  
**  System : 영업  
**  Sub System : 고객납품의뢰관리  
**  Page  : 출하반품의뢰등록  
**  Desc  : 반품의뢰라인 삭제  
**  Return Values  
**  
**  작    성    자  :   
**  작    성    일 :   
**  수    정    자     : 허성철  
*********************************************  
** Change History  
*********************************************  
*********************************************/  
CREATE PROCEDURE UP_SA_GIRR_REG_DELETE1
(  
 @P_CD_COMPANY  NVARCHAR(7),   --회사  
 @P_NO_GIR   NVARCHAR(20),  --의뢰번호  
 @P_SEQ_GIR   NUMERIC(3)   --의뢰항번  
)  
AS  
SET NOCOUNT ON  
  
DELETE   
FROM SA_GIRL  
WHERE CD_COMPANY = @P_CD_COMPANY  
AND  NO_GIR  = @P_NO_GIR  
AND  SEQ_GIR  = @P_SEQ_GIR  
  
SET NOCOUNT OFF  
  
  GO
  
  
  
  
  
  