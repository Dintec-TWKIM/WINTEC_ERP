USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SO_SUBL_S]    Script Date: 2016-12-13 오전 11:37:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System : 영업
**  Sub System : 수주관리
**  Page  : 수주등록
**  Desc  : 수주등록 도움창 조회 라인
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
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SO_SUBL_S]
(
    @P_CD_COMPANY		NVARCHAR(7),
    @P_NO_SO			NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT SL.NO_SO,        --수주번호
       SL.SEQ_SO,       --항번
       SL.CD_ITEM,      --품목코드
	   MI.NM_ITEM,      --품목명
	   MI.STND_ITEM,    --규격
	   MI.UNIT_SO,      --단위
	   SL.QT_SO,        --수량        
	   SL.UM_SO,        --단가
	   SL.AM_SO,        --금액
	   SL.AM_WONAMT,    --원화금액
	   SL.AM_VAT,       --부가세
	   SL.STA_SO,       --수주상태
	   SL.TP_GI,        --출고형태
	   SL.TP_BUSI,      --거래구분
	   SL.TP_IV,        --매출형태
	   SL.GIR,          --자동프로세스처리(의뢰 유무)
	   SL.GI,           --자동프로세스처리(출고 유무)
	   SL.IV,           --자동프로세스처리(매출 유무)
	   SL.TRADE,        --수출여부
	   SL.DT_DUEDATE,   --납기요구일(2012.06.08)
	   SL.DT_REQGI      --출고예정일(2012.06.08)
FROM SA_SOL SL
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_PLANT = SL.CD_PLANT AND MI.CD_ITEM = SL.CD_ITEM
WHERE SL.CD_COMPANY = @P_CD_COMPANY
AND SL.NO_SO = @P_NO_SO

GO

