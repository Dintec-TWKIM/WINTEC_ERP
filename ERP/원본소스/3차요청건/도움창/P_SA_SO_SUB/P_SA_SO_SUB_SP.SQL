
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_SO_SUB_SELECT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_SO_SUB_SELECT
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO


/*******************************************
**  System : 영업
**  Sub System : 고객납품의뢰관리
**  Page  : 고객납품의뢰등록 ->  납품의뢰(수주적용) 도움창
**  Desc  : 납품의뢰 조회
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
CREATE  PROCEDURE UP_SA_SO_SUB_SELECT
(
	@P_CD_COMPANY 		NVARCHAR(7),
	@P_DT_SO_FROM 		NCHAR(8),
	@P_DT_SO_TO 		NCHAR(8),
	@P_CD_PLANT 		NVARCHAR(7),
	@P_TP_BUSI 			NVARCHAR(3),
	@P_CD_PARTNER 		NVARCHAR(7),
	@P_CD_SALEGRP 		NVARCHAR(7),
	@P_CD_SL 			NVARCHAR(7)
)
AS
SET NOCOUNT ON

SELECT			'N' S,
				A.NO_SO,			--수주번호
				A.DT_SO,			--수주일자
				(SELECT NM_SO FROM SA_TPSO WHERE CD_COMPANY = A.CD_COMPANY AND TP_SO = A.TP_SO) NM_SO,						--수주형태
				A.CD_EXCH,		--환종
				(SELECT NM_SALEGRP FROM MA_SALEGRP WHERE CD_COMPANY = A.CD_COMPANY AND CD_SALEGRP = A.CD_SALEGRP) NM_SALEGRP,--영업그룹
				(SELECT NM_KOR FROM MA_EMP WHERE CD_COMPANY = A.CD_COMPANY AND NO_EMP = A.NO_EMP) NM_KOR,					--담당자
				A.DC_RMK
FROM			SA_SOH A
INNER JOIN
(
	SELECT	NO_SO 
	FROM	SA_SOL
	WHERE	CD_COMPANY = @P_CD_COMPANY
	AND		CD_PLANT = @P_CD_PLANT
	AND		TP_BUSI = @P_TP_BUSI
	AND		(CD_SL = @P_CD_SL OR @P_CD_SL = '' OR @P_CD_SL IS NULL)
	AND		STA_SO = 'R'	--수주상태가 확정인거만
	AND		(QT_SO - QT_GIR) > 0	--수주수량 - 의뢰수량이 0보다 큰거만
	GROUP BY NO_SO
) B
ON				B.NO_SO = A.NO_SO
WHERE			A.CD_COMPANY = @P_CD_COMPANY
AND				A.DT_SO BETWEEN @P_DT_SO_FROM AND @P_DT_SO_TO
AND				A.CD_PARTNER = @P_CD_PARTNER
AND				(A.CD_SALEGRP = @P_CD_SALEGRP OR @P_CD_SALEGRP = '' OR @P_CD_SALEGRP IS NULL)
ORDER BY		A.NO_SO


go

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_SA_SO_SUB_SELECT1') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_SA_SO_SUB_SELECT1
GO


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System : 영업
**  Sub System : 고객납품의뢰관리
**  Page  : 고객납품의뢰등록 ->  납품의뢰(수주적용) 도움창
**  Desc  : 수주등록 라인조회
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
CREATE  PROCEDURE UP_SA_SO_SUB_SELECT1
(
	@P_CD_COMPANY 		NVARCHAR(7),
	@P_NO_SO			NVARCHAR(20),
	@P_CD_PLANT 		NVARCHAR(7),
	@P_TP_BUSI 			NVARCHAR(3),
	@P_CD_SL 			NVARCHAR(7)
)
AS
--SA_GIRL에 들어갈 SA_SOL데이터들 (SA_GIRL 컬럼명을 맞춘다) UI에서 넘김
SELECT			'N' S,
				A.NO_SO,			--수주번호
				A.SEQ_SO,		--수주라인항번
				A.CD_ITEM,		--품목코드
				B.NM_ITEM,		--품목명
				A.TP_ITEM,		--품목타입
				B.STND_ITEM,		--규격
				B.UNIT_SO,		--단위
				A.DT_DUEDATE	,	--납기요구일
				A.DT_REQGI,		--출하예정일
				A.QT_SO - A.QT_GIR QT_GIR, --수량
				A.UM_SO UM,		--단가
				A.AM_SO - A.AM_EXGIR AM_GIR, --금액
				A.AM_WONAMT - A.AM_GIR AM_GIRAMT,	--원화금액
				0.0000 AM_VAT,	--부가세(UI단에서 계산한다)
				A.CD_SL,			--창고코드
				(SELECT NM_SL FROM MA_SL WHERE CD_COMPANY = A.CD_COMPANY AND CD_PLANT = A.CD_PLANT AND CD_SL = A.CD_SL GROUP BY CD_SL, NM_SL) NM_SL, --창고명
				A.UNIT_IM UNIT,		--관리단위
				A.QT_IM - A.QT_GIR_IM QT_GIR_IM,	--의뢰재고수량
				0 SEQ_GIR,
				'N' YN_INSPECT,	--검사유무 ->품목에 따라 온다고 했지만 어떻게 될지 모름
				A.TP_GI,			--출하형태
				(SELECT NM_QTIOTP FROM MM_EJTP WHERE  CD_COMPANY = A.CD_COMPANY AND CD_QTIOTP = A.TP_GI) NM_GI, --출하형태명
				C.CD_EXCH,		--환종
				'' GI_PARTNER,	--납품처코드
				'' LN_PARTNER,	--납품처명
				C.NO_PROJECT,	--프로젝트코드
				(SELECT NM_PROJECT FROM SA_PROJECTH WHERE CD_COMPANY = A.CD_COMPANY AND	NO_PROJECT = C.NO_PROJECT) NM_PROJECT, --프로젝트명				
				C.RT_EXCH,		--환율
				C.RT_VAT,		--부가세율
				B.UNIT_SO_FACT,	--품목재고수량
				C.CD_SALEGRP,	--영업그룹
				C.NO_EMP,		--담당자
				A.TP_IV,			--매출형태
				C.TP_VAT,		--과세구분
				C.FG_TAXP,		--계산서처리방법
				A.TP_BUSI,		--거래구분
				0.0000 QT_GI,		--출하수량
				'' NO_LC,			--LC번호
				0 SEQ_LC			--LC항번

FROM			SA_SOL A
LEFT OUTER JOIN	MA_PITEM B
ON				B.CD_COMPANY = A.CD_COMPANY
AND				B.CD_PLANT = A.CD_PLANT
AND				B.CD_ITEM = A.CD_ITEM
INNER JOIN		SA_SOH C --프로젝트, 환종, 환율, 부가세율, 매출형태, 과세구분, 계산서처리방법을 가져오기위해
ON				C.CD_COMPANY = A.CD_COMPANY
AND				C.NO_SO = A.NO_SO
WHERE			A.CD_COMPANY = @P_CD_COMPANY
AND				A.NO_SO = @P_NO_SO
AND				A.CD_PLANT = @P_CD_PLANT
AND				A.TP_BUSI = @P_TP_BUSI
AND				(A.CD_SL = @P_CD_SL OR @P_CD_SL = '' OR @P_CD_SL IS NULL)
AND				A.STA_SO = 'R'	--수주상태가 확정인거만
AND				(A.QT_SO - A.QT_GIR) > 0	--수주수량 - 의뢰수량이 0보다 큰거만

GO












