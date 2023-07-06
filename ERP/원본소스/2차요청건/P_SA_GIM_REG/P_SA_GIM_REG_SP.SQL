
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIM_REG_SELECT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIM_REG_SELECT
GO


/*******************************************    
**  System : 영업    
**  Sub System : 출하관리     
**  Page  : 출하관리  
**  Desc  : 출하관리 헤더 조회  
**  Return Values    
**    
**  작    성    자  :     
**  작    성    일 :     
**  수    정    자     : 허성철    
*********************************************    
** Change History    
*********************************************    
*********************************************/  
CREATE PROCEDURE UP_SA_GIM_REG_SELECT
(      
 @P_CD_COMPANY   NVARCHAR(7),  --회사  
 @P_DT_IO_FROM NCHAR(8),  --수불일자FROM  
 @P_DT_IO_TO  NCHAR(8),  --수불일자TO  
 @P_CD_PLANT  NVARCHAR(7),  --공장  
 @P_CD_PARTNER NVARCHAR(7),  --거래처  
 @P_NO_EMP  NVARCHAR(10), --사번  
 @P_FG_TRANS  NVARCHAR(3),  --거래구분  
 @P_YN_RETURN NVARCHAR(1),  --출하구분(반품유무)  
 @P_CD_QTIOTP NVARCHAR(3),  --출하형태  
 @P_CD_SL  NVARCHAR(7)  --창고  
)      
AS    
  
SELECT  'N' S,   --선택  
   A.NO_IO,   --수불번호  
   A.DT_IO,   --수불일자  
   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = A.CD_COMPANY AND CD_PARTNER = A.CD_PARTNER) LN_PARTNER, --거래처명     
   (SELECT NM_KOR FROM MA_EMP WHERE CD_COMPANY = A.CD_COMPANY AND NO_EMP = A.NO_EMP) NM_KOR, --이름  
   A.YN_RETURN,  
   A.DC_RMK  
FROM  MM_QTIOH A  
INNER JOIN  
(  
 SELECT  NO_IO  
 FROM  MM_QTIO  
 WHERE  CD_COMPANY = @P_CD_COMPANY  
 AND   (FG_TRANS = @P_FG_TRANS OR @P_FG_TRANS ='' OR @P_FG_TRANS IS NULL)  
 AND   (CD_QTIOTP = @P_CD_QTIOTP OR @P_CD_QTIOTP = '' OR @P_CD_QTIOTP IS NULL)  
 AND   (CD_SL = @P_CD_SL OR @P_CD_SL = '' OR @P_CD_SL IS NULL)  
 AND   FG_PS = '2'    --출고  
 AND   FG_IO IN ('010','041','042') --판매출고, 판매반품, 판매반품정리  
 GROUP BY NO_IO  
) B  
ON B.NO_IO = A.NO_IO  
WHERE  A.CD_COMPANY = @P_CD_COMPANY  
AND   A.DT_IO BETWEEN @P_DT_IO_FROM AND @P_DT_IO_TO  
AND   A.CD_PLANT = @P_CD_PLANT  
AND   (A.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)  
AND   (A.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)  
AND   (A.YN_RETURN = @P_YN_RETURN OR @P_YN_RETURN = '' OR @P_YN_RETURN IS NULL)  
ORDER BY A.NO_IO  
  
  GO
  
  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIM_REG_SELECT1') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIM_REG_SELECT1
GO

  
  /*******************************************    
**  System : 영업    
**  Sub System : 출하관리     
**  Page  : 출하관리  
**  Desc  : 출하관리 헤더 조회  
**  Return Values    
**    
**  작    성    자  :     
**  작    성    일 :     
**  수    정    자     : 허성철    
*********************************************    
** Change History    
*********************************************    
*********************************************/  
CREATE PROCEDURE UP_SA_GIM_REG_SELECT1
(      
 @P_CD_COMPANY   NVARCHAR(7),  --회사  
 @P_NO_IO  NVARCHAR(20), --수불번호  
 @P_FG_TRANS  NVARCHAR(3),  --거래구분  
 @P_CD_QTIOTP NVARCHAR(3),  --출하형태  
 @P_CD_SL  NVARCHAR(7)  --창고  
)      
AS    
  
SELECT   'N' S,   --선택  
    A.NO_IO,   --수불번호  
    A.NO_IOLINE,  --수불항번  
    A.CD_ITEM,  --품목코드  
    B.NM_ITEM,  --품목명  
    B.STND_ITEM,  --규격  
    A.FG_TRANS,  --거래구분  
    (SELECT NM_SL FROM MA_SL WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT AND CD_SL = A.CD_SL GROUP BY CD_SL, NM_SL) NM_SL, --창고명,  
    (CASE B.FG_SERNO WHEN '003' THEN 'S/N' WHEN '002' THEN 'LOT' ELSE '' END) FG_MNG,  
    A.QT_IO,   --수불수량  
    B.UNIT_IM,  --단위  
    A.QT_UNIT_MM, --수불재고수량  
    A.CD_UNIT_MM, --재고단위  
    A.AM,   --금액  
    A.VAT,   --부가세  
    A.QT_CLS,  --마감수량  
    A.YN_AM,   --유무환구분  
    (SELECT NM_QTIOTP FROM MM_EJTP WHERE  CD_COMPANY = A.CD_COMPANY AND CD_QTIOTP = A.CD_QTIOTP) NM_QTIOTP, --출하형태명  
    A.NO_ISURCV,  --의뢰번호  
    A.NO_PSO_MGMT, --수주번호  
    A.NO_PSOLINE_MGMT,--수주항번  
    A.NO_IO_MGMT, --반품수불번호  
    A.QT_RETURN,  --반품의뢰수량  
    A.CD_QTIOTP,  
    A.FG_IO  
FROM   MM_QTIO A  
LEFT OUTER JOIN MA_PITEM B  
ON    B.CD_COMPANY = A.CD_COMPANY  
AND    B.CD_PLANT = A.CD_PLANT  
AND    B.CD_ITEM = A.CD_ITEM  
  
WHERE   A.CD_COMPANY = @P_CD_COMPANY  
AND    A.NO_IO = @P_NO_IO  
AND    (A.FG_TRANS = @P_FG_TRANS OR @P_FG_TRANS ='' OR @P_FG_TRANS IS NULL)  
AND    (A.CD_QTIOTP = @P_CD_QTIOTP OR @P_CD_QTIOTP = '' OR @P_CD_QTIOTP IS NULL)  
AND    (A.CD_SL = @P_CD_SL OR @P_CD_SL = '' OR @P_CD_SL IS NULL)  
AND    A.FG_PS = '2'    --출고  
AND    A.FG_IO IN ('010','041','042') --판매출고, 판매반품, 판매반품정리  
ORDER BY  A.NO_IO, A.NO_IOLINE  
  
   GO 
  
  
  
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIM_REG_DELETE') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIM_REG_DELETE
GO

  
  /*******************************************    
**  System : 영업    
**  Sub System : 출하관리     
**  Page  : 출하등록    
**  Desc  : 출하등록 라인 삭제  
**  Return Values    
**    
**  작    성    자  :     
**  작    성    일 :     
**  수    정    자     : 허성철    
*********************************************    
** Change History    
*********************************************    
*********************************************/   
CREATE      PROC UP_SA_GIM_REG_DELETE
(    
 @P_CD_COMPANY   NVARCHAR(7),   --회사  
 @P_NO_IO  NVARCHAR(20),  --수불번호  
 @P_NO_IOLINE   NUMERIC(5),   --수불항번  
 @P_NO_PSO_MGMT NVARCHAR(20),  --수주번호  
 @P_NO_PSOLINE_MGMT NUMERIC(5),  --수주항번  
 @P_NO_IO_MGMT NVARCHAR(20)   --반품수불번호  
)    
AS    
SET NOCOUNT ON  
DECLARE @V_STA_SO NVARCHAR(3)  
  
--수주상태를 체크한다.  
SELECT @V_STA_SO = STA_SO  
FROM SA_SOL  
WHERE CD_COMPANY = @P_CD_COMPANY  
AND  NO_SO = @P_NO_PSO_MGMT  
AND  SEQ_SO = @P_NO_PSOLINE_MGMT  
  
IF (@V_STA_SO = 'O')  
BEGIN  
  --수주가 진행 혹은 종결 이므로 저장 할 수 없습니다.  
  RAISERROR 100000 'SA_M000133'  
  RETURN  
END  
  
DELETE   
FROM  MM_QTIO  
WHERE  CD_COMPANY = @P_CD_COMPANY  
AND   NO_IO = @P_NO_IO  
AND   NO_IOLINE = @P_NO_IOLINE  
  
  
SET NOCOUNT OFF  
 GO
 
  
  
  
  
  
  
  
  
  
  
    
  
  
  