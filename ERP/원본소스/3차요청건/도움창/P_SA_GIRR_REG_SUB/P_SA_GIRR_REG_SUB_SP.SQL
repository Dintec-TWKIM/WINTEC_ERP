
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRR_REG_SUB_SELECT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRR_REG_SUB_SELECT
GO

/*******************************************
**  System : 영업
**  Sub System : 출하의뢰관리	
**  Page  : 출하납품의뢰등록
**  Desc  : 반품의뢰 도움창 헤더조회
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
CREATE   PROCEDURE UP_SA_GIRR_REG_SUB_SELECT
(  
	@P_CD_COMPANY		NVARCHAR(7),		--회사
	@P_DT_IO_FROM		NCHAR(8),		--수불일자FROM
	@P_DT_IO_TO			NCHAR(8),		--수불일자TO
	@P_CD_PLANT			NVARCHAR(7),		--공장
	@P_CD_GROUP		NVARCHAR(7),		--영업그룹
	@P_CD_PARTNER		NVARCHAR(7),		--거래처
	@P_FG_TAX			NCHAR(3),		--과세구분
	@P_CD_SL			NVARCHAR(7),		--창고
	@P_CD_QTIOTP		NVARCHAR(3),		--출하형태
	@P_NO_EMP			NVARCHAR(10),	--담당자
	@P_CD_PJT			NVARCHAR(20)		--프로젝트
)  
AS  
SET NOCOUNT ON

SELECT		'N' S,			--선택
			A.NO_IO,			--수불번호
			A.DT_IO,			--수불일자
			(SELECT NM_KOR FROM MA_EMP WHERE CD_COMPANY = A.CD_COMPANY AND NO_EMP = A.NO_EMP) NM_KOR,	--담당자
			A.DC_RMK		--비고
FROM		MM_QTIOH A
INNER JOIN
(
	SELECT		NO_IO
	FROM		MM_QTIO
	WHERE		CD_COMPANY = @P_CD_COMPANY
	AND			(CD_GROUP = @P_CD_GROUP OR @P_CD_GROUP = '' OR @P_CD_GROUP IS NULL)
	AND			FG_TAX = @P_FG_TAX
	AND			(CD_SL = @P_CD_SL OR @P_CD_SL = '' OR @P_CD_SL IS NULL)
	AND			(CD_QTIOTP = @P_CD_QTIOTP OR @P_CD_QTIOTP = '' OR @P_CD_QTIOTP IS NULL)
	AND			(CD_PJT = @P_CD_PJT OR @P_CD_PJT = '' OR @P_CD_PJT IS NULL)
	AND			QT_UNIT_MM - QT_RETURN_MM > 0
	AND			FG_IO = '010'
	GROUP BY	NO_IO
) B
ON	B.NO_IO = A.NO_IO
WHERE		A.CD_COMPANY = @P_CD_COMPANY
AND			A.DT_IO BETWEEN @P_DT_IO_FROM AND @P_DT_IO_TO
AND			A.CD_PLANT = @P_CD_PLANT
AND			A.CD_PARTNER = @P_CD_PARTNER
AND			(A.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)		
	

SET NOCOUNT OFF




IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_GIRR_REG_SUB_SELECT1') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_GIRR_REG_SUB_SELECT1
GO
/*******************************************
**  System : 영업
**  Sub System : 출하의뢰관리	
**  Page  : 출하납품의뢰등록
**  Desc  : 반품의뢰 도움창 라인조회
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
CREATE  PROCEDURE UP_SA_GIRR_REG_SUB_SELECT1
(  
	@P_CD_COMPANY		NVARCHAR(7),		--회사
	@P_NO_IO			NVARCHAR(20),	--수불번호
	@P_CD_GROUP		NVARCHAR(7),		--영업그룹
	@P_FG_TAX			NCHAR(3),		--과세구분
	@P_CD_SL			NVARCHAR(7),		--창고
	@P_CD_QTIOTP		NCHAR(3),		--출하형태
	@P_CD_PJT			NVARCHAR(20)		--프로젝트
)  
AS  
SET NOCOUNT ON
--SA_GIRL에 들어갈 MM_QTIO(SA_GIRL조인해서 SA_GIRL데이터를 넣는다)데이터들 (SA_GIRL 컬럼명을 맞춘다)
SELECT			'N' S,				--선택
				A.NO_IO,				--수불번호
				A.NO_IOLINE,			--수불항번
				(SELECT NM_SL FROM MA_SL WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT AND CD_SL = A.CD_SL GROUP BY CD_SL, NM_SL) NM_SL, --창고명
				(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = A.CD_GROUP) NM_SALEGRP,		--영업그룹
				A.NO_PSO_MGMT,		--수주번호
				A.CD_ITEM,			--품목코드
				C.NM_ITEM,			--품목명
				C.STND_ITEM,			--규격
				C.UNIT_SO,			--단위
				A.QT_IO - A.QT_RETURN QT_IO,				--수량
				A.UM_EX,									--단가
				(A.QT_IO - A.QT_RETURN) * A.UM_EX AM_EX,		--금액
				(A.QT_IO - A.QT_RETURN) * A.UM_EX * A.RT_EXCH AM,	--원화금액

				0 SEQ_GIR,							--의뢰항번
				B.CD_ITEM,							--품목코드
				B.TP_ITEM,							--품목타입
				B.DT_DUEDATE,						--납품요청일
				B.DT_DUEDATE DT_REQGI,				--출하예정일(반품일때에는 납품요청일로 한다)
				B.YN_INSPECT,						--검사유무
				A.CD_SL,								--창고코드
				(SELECT NM_SL FROM MA_SL WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT AND CD_SL = A.CD_SL GROUP BY CD_SL, NM_SL) NM_SL, --창고명
				A.QT_UNIT_MM - A.QT_RETURN_MM QT_GIR,	--반품수량
				B.UM,								--단가
				(A.QT_UNIT_MM - A.QT_RETURN_MM) * B.UM AM_GIR,	--금액
				0.0000 AM_GIRAMT,						--원화금액(UI에서계산)
				0.0000 AM_VAT,						--부가세(UI에서계산)	
				B.UNIT,								--재고단위
				A.QT_IO - A.QT_RETURN QT_GIR_IM,		--반품재고수량
				B.GI_PARTNER,						--납품처
				B.NO_PROJECT,						--프로젝트
				(SELECT NM_PROJECT FROM SA_PROJECTH WHERE CD_COMPANY = A.CD_COMPANY AND	NO_PROJECT = B.NO_PROJECT) NM_PROJECT, --프로젝트명				
				B.NO_EMP,							--담당자
				C.UNIT_SO_FACT,						--재고단위수량
				A.NO_IO NO_IO_MGMT,					--반품수불번호
				A.NO_IOLINE NO_IOLINE_MGMT,			--반품수불항번
				A.NO_PSO_MGMT NO_SO_MGMT,			--반품수주번호
				A.NO_PSOLINE_MGMT NO_SOLINE_MGMT,	--반품수주항번
				0.0000 QT_GI,							--반품출하수량
				A.NO_LC,								--LC번호
				A.NO_LCLINE SEQ_LC,					--LC항번
				A.FG_TPIO TP_IV						--매출형태
FROM			MM_QTIO A
INNER JOIN		SA_GIRL B
ON				B.CD_COMPANY = A.CD_COMPANY
AND				B.NO_GIR = A.NO_ISURCV
AND				B.SEQ_GIR = A.NO_ISURCVLINE
LEFT OUTER JOIN	MA_PITEM C
ON				C.CD_COMPANY = A.CD_COMPANY
AND				C.CD_PLANT = A.CD_PLANT
AND				C.CD_ITEM = A.CD_ITEM
WHERE			A.CD_COMPANY = @P_CD_COMPANY
AND				A.NO_IO = @P_NO_IO
AND				(A.CD_GROUP = @P_CD_GROUP OR @P_CD_GROUP = '' OR @P_CD_GROUP IS NULL)
AND				A.FG_TAX = @P_FG_TAX
AND				(A.CD_SL = @P_CD_SL OR @P_CD_SL = '' OR @P_CD_SL IS NULL)
AND				(A.CD_QTIOTP = @P_CD_QTIOTP OR @P_CD_QTIOTP = '' OR @P_CD_QTIOTP IS NULL)
AND				(A.CD_PJT = @P_CD_PJT OR @P_CD_PJT = '' OR @P_CD_PJT IS NULL)
AND				A.QT_UNIT_MM - A.QT_RETURN_MM > 0
AND				A.FG_IO = '010'		--판매출고인거만

SET NOCOUNT OFF





















