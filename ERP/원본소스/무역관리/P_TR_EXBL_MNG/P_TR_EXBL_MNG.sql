IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_MNG_H_SELECT'))
DROP PROCEDURE UP_TR_EXBL_MNG_H_SELECT
GO 

/**********************************************************************************************************  
 * 프로시저명: UP_TR_EXBL_MNG_H_SELECT
 * 관련페이지: 선적관리 
 * 설      명: 선적관리 헤더조회
 * 작  성  자: 
 * EXEC UP_TR_EXBL_MNG_H_SELECT '','','','','','','',''
 *********************************************************************************************************/
 
CREATE PROCEDURE UP_TR_EXBL_MNG_H_SELECT
	@P_CD_COMPANY        NVARCHAR(7),                --회사코드  
	@P_CD_BIZAREA        NVARCHAR(7),                --사업장  
	@P_DT_FROM           NVARCHAR(8),                --작성기간시작일  
	@P_DT_TO             NVARCHAR(8),                --작성기간 종료일  
	@P_CD_PARTNER        NVARCHAR(7),                --거래처코드  
	@P_CD_SALEGRP        NVARCHAR(7),                --영업그룹  
	@P_NO_EMP            NVARCHAR(10),				 --담당자  
	@P_YN_SLIP           NVARCHAR(1)  
AS  
BEGIN
	SELECT 'N' CHK, H.NO_BL, H.NO_TO, H.NO_INV, H.CD_BIZAREA, H.CD_SALEGRP, H.NO_EMP, H.CD_PARTNER, H.DT_BALLOT,   
	H.CD_EXCH, H.RT_EXCH, H.AM_EX, H.AM, H.AM_EXNEGO, H.AM_NEGO, H.YN_SLIP, H.NO_SLIP, H.CD_EXPORT, H.DT_LOADING,   
	H.DT_ARRIVAL, H.SHIP_CORP, H.NM_VESSEL, H.PORT_LOADING, H.PORT_NATION, H.PORT_ARRIVER, H.COND_SHIPMENT,   
	H.FG_BL, H.FG_LC, H.COND_PAY, H.COND_DAYS, H.DT_PAYABLE, H.REMARK1, H.REMARK2, H.REMARK3,   
	(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = @P_CD_COMPANY AND CD_BIZAREA = H.CD_BIZAREA) NM_BIZAREA,  --사업장명      
	(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_SALEGRP = H.CD_SALEGRP AND CD_COMPANY = @P_CD_COMPANY)  NM_SALEGRP, --영업그룹명      
	(SELECT NM_KOR     FROM MA_EMP WHERE CD_COMPANY = @P_CD_COMPANY AND NO_EMP = H.NO_EMP) NM_KOR,    --담당자명      
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = @P_CD_COMPANY AND CD_PARTNER = H.CD_PARTNER) LN_PARTNER,   --거래처  
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = @P_CD_COMPANY AND CD_PARTNER = H.CD_EXPORT) NM_EXPORT,     --거래처    
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = @P_CD_COMPANY AND CD_PARTNER = H.SHIP_CORP) NM_SHIP_CORP     --거래처    
	FROM TR_EXBL H  
	WHERE H.CD_COMPANY = @P_CD_COMPANY  
	AND H.CD_BIZAREA = @P_CD_BIZAREA  
	AND H.DT_BALLOT BETWEEN @P_DT_FROM AND @P_DT_TO  
	AND (H.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)  
	AND (H.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)  
	AND (H.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)  
	AND (H.YN_SLIP = @P_YN_SLIP OR @P_YN_SLIP = '' OR @P_YN_SLIP IS NULL)  
	ORDER BY H.NO_BL  
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_MNG_L_SELECT'))
DROP PROCEDURE UP_TR_EXBL_MNG_L_SELECT
GO 

/**********************************************************************************************************  
 * 프로시저명: UP_TR_EXBL_MNG_L_SELECT
 * 관련페이지: 선적관리 
 * 설      명: 선적관리 라인조회
 * 작  성  자: 
 * EXEC UP_TR_EXBL_MNG_L_SELECT '','','','','','','',''
 *********************************************************************************************************/
 
CREATE PROCEDURE UP_TR_EXBL_MNG_L_SELECT  
	@P_CD_COMPANY        NVARCHAR(7),    --회사코드  
	@P_CD_BIZAREA        NVARCHAR(7),    --사업장  
	@P_DT_FROM           NVARCHAR(8),    --작성기간시작일  
	@P_DT_TO             NVARCHAR(8),    --작성기간 종료일  
	@P_CD_PARTNER        NVARCHAR(7),    --거래처코드  
	@P_CD_SALEGRP        NVARCHAR(7),    --영업그룹  
	@P_NO_EMP            NVARCHAR(10),   --담당자  
	@P_YN_SLIP           NVARCHAR(1),    --전표처리여부  
	@P_NO_BL             NVARCHAR(20)    --BL번호  
AS
BEGIN
	SELECT D.NO_INV,     D.NO_LINE,    D.CD_PLANT,   D.CD_ITEM,      D.DT_DELIVERY, 
	       D.QT_INVENT,  D.UM_INVENT,  D.AM_INVENT,  D.HS_DISC,      D.QT_SO, 
	       D.UM_SO,      D.AM_EXSO,    D.NO_QTIO,    D.NO_LINE_QTIO, D.NO_LC, 
	       D.NO_LINE_LC, D.NO_PO,      D.NO_LINE_PO, D.NO_TO,        D.NO_BL, 
	       D.DT_TO,      O.QT_CLS,     O.AM_CLS,     P.NM_PLANT,     M.NM_ITEM,
	       M.STND_ITEM,  M.UNIT_SO  
	FROM TR_EXBL H  
	INNER JOIN TR_EXBLL L ON L.CD_COMPANY = @P_CD_COMPANY AND L.NO_BL = H.NO_BL  
	INNER JOIN TR_INVL D ON D.CD_COMPANY = @P_CD_COMPANY AND D.NO_INV = L.NO_INV  
	LEFT OUTER JOIN MM_QTIO O ON O.CD_COMPANY = @P_CD_COMPANY AND O.NO_IO = D.NO_QTIO AND O.NO_IOLINE = D.NO_LINE_QTIO  
	LEFT OUTER JOIN MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY AND P.CD_PLANT = D.CD_PLANT  
	LEFT OUTER JOIN MA_PITEM M ON M.CD_COMPANY = @P_CD_COMPANY AND M.CD_PLANT = D.CD_PLANT AND M.CD_ITEM = D.CD_ITEM  
	WHERE H.CD_COMPANY = @P_CD_COMPANY  
	AND H.CD_BIZAREA = @P_CD_BIZAREA  
	AND H.DT_BALLOT BETWEEN @P_DT_FROM AND @P_DT_TO  
	AND (H.CD_PARTNER = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)  
	AND (H.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)  
	AND (H.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)  
	AND (H.YN_SLIP = @P_YN_SLIP OR @P_YN_SLIP = '' OR @P_YN_SLIP IS NULL)  
	AND H.NO_BL = @P_NO_BL  
	ORDER BY H.NO_BL 
END
GO 


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXBL_MNG_DELETE'))
DROP PROCEDURE UP_TR_EXBL_MNG_DELETE
GO 

/**********************************************************************************************************  
 * 프로시저명: UP_TR_EXBL_MNG_DELETE
 * 관련페이지: 선적관리 
 * 설      명: 선적관리 삭제
 * 작  성  자: 
 * EXEC UP_TR_EXBL_MNG_DELETE '',''
 *********************************************************************************************************/
 
CREATE PROCEDURE UP_TR_EXBL_MNG_DELETE  
	@P_CD_COMPANY   NVARCHAR(7),    --회사코드  
	@P_NO_BL         NVARCHAR(20)   --BL번호  
AS
BEGIN
	DECLARE @ERRNO  INT,   
	        @ERRMSG NVARCHAR(255)  
  
	IF (EXISTS(SELECT 1 FROM TR_EXBL WHERE CD_COMPANY = @P_CD_COMPANY AND NO_BL = @P_NO_BL AND YN_SLIP = 'Y'))  
	BEGIN  
		SELECT @ERRNO  = 100000, @ERRMSG = '[UP_TR_EXBL_MNG_DELETE]BL번호(' + @P_NO_BL + ')가 전표처리되었습니다.' GOTO ERROR  
	END  
  
	--MM_QTIO에 있는 QT_CLS, AM_CLS, QT_CLS_MM의 값을 0으로 초기화해준다.  
	EXEC UP_TR_EXBL_QTIO_DELETE_UPDATE @P_CD_COMPANY = @P_CD_COMPANY, @P_NO_BL = @P_NO_BL  
  
	IF (@@ERROR <> 0) BEGIN SELECT @ERRNO  = 100000, @ERRMSG = '[UP_TR_EXBL_MNG_DELETE]작업을 정상적으로 처리하지 못했습니다.' GOTO ERROR END  
  
	--TR_EXBL, TR_EXBLL 테이블 삭제  
	EXEC UP_TR_EXBL_DELETE @P_CD_COMPANY = @P_CD_COMPANY, @P_NO_BL = @P_NO_BL  
  
	IF (@@ERROR <> 0) BEGIN SELECT @ERRNO  = 100000, @ERRMSG = '[UP_TR_EXBL_MNG_DELETE]작업을 정상적으로 처리하지 못했습니다.' GOTO ERROR END  
  
	RETURN  
		ERROR: RAISERROR @ERRNO @ERRMSG
END
GO
  

--UP_TR_EXBL_DOCU, UP_FI_DOCU_AUTODEL는 공통으로 사용한다.
