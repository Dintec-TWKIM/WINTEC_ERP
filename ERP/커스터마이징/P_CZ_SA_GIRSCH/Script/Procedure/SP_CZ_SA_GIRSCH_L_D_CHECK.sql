USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIRSCH_L_D_CHECK]    Script Date: 2015-10-20 오후 3:40:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIRSCH_L_D_CHECK]  
(  
    @P_CD_COMPANY   NVARCHAR(7),        --회사
    @P_NO_GIR       NVARCHAR(20),       --의뢰번호
	@P_SEQ_GIR		NUMERIC(5, 0)
)
AS

DECLARE @V_ERRMSG		NVARCHAR(255)
DECLARE @V_NO_SO		NVARCHAR(20)
DECLARE @V_NO_GIR_ORG	NVARCHAR(20) = NULL 
DECLARE @V_NO_PACK_ORG  NUMERIC(5, 0)
DECLARE @V_QT_GIR_STOCK NUMERIC(17, 4)
DECLARE @V_GRP_ITEM		NVARCHAR(20)

BEGIN TRAN SP_CZ_SA_GIRSCH_L_D_CHECK
BEGIN TRY

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

IF EXISTS (SELECT * 
		   FROM CZ_SA_GIRH_WORK_DETAIL
		   WHERE CD_COMPANY = @P_CD_COMPANY
		   AND NO_GIR = @P_NO_GIR
		   AND ISNULL(DC_RMK1, '') = '')
BEGIN
	SELECT @V_ERRMSG = '수정/취소 사유가 없을 경우 출고 품목 삭제할 수 없습니다.'                     
	GOTO ERROR	
END

SELECT @V_NO_SO = GL.NO_SO,
	   @V_QT_GIR_STOCK = GL.QT_GIR_STOCK,
	   @V_GRP_ITEM = QL.GRP_ITEM
FROM SA_GIRL GL
LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_FILE = GL.NO_SO AND QL.NO_LINE = GL.SEQ_SO 
WHERE GL.CD_COMPANY = @P_CD_COMPANY
AND GL.NO_GIR = @P_NO_GIR
AND GL.SEQ_GIR = @P_SEQ_GIR

IF LEFT(@V_NO_SO, 2) <> 'SB' AND LEFT(@V_NO_SO, 2) <> 'NS' AND @V_GRP_ITEM NOT IN ('GS', 'PV')
BEGIN
	SELECT @V_ERRMSG = '[' + @V_NO_SO + '] SB, NS, 선용, 선식품만 출고 품목 삭제 할 수 있습니다.'
	GOTO ERROR
END

IF (@V_QT_GIR_STOCK > 0 AND EXISTS (SELECT GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO,
									       ISNULL(GL.QT_GIR_STOCK, 0) AS QT_GIR_STOCK,
									       ISNULL(QL.QT_GIREQ, 0) AS QT_GIREQ,
									       ISNULL(QL.QT_GI, 0) AS QT_GI
									FROM (SELECT GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO,
									             SUM(GL.QT_GIR_STOCK) AS QT_GIR_STOCK
									      FROM SA_GIRL GL
									      WHERE GL.CD_COMPANY = @P_CD_COMPANY
									      AND GL.NO_GIR = @P_NO_GIR
									      AND GL.SEQ_GIR = @P_SEQ_GIR
									      AND GL.QT_GIR_STOCK > 0
									      GROUP BY GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO) GL
									LEFT JOIN (SELECT QL.CD_COMPANY, QL.NO_SO, QL.SEQ_SO,
									                  SUM(QL.QT_GIREQ) AS QT_GIREQ,
									                  SUM(QL.QT_GI) AS QT_GI
									           FROM MM_GIREQL QL
									           WHERE QL.CD_SL = 'SL02'
									           GROUP BY QL.CD_COMPANY, QL.NO_SO, QL.SEQ_SO) QL
									ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_SO = GL.NO_SO AND QL.SEQ_SO = GL.SEQ_SO
									WHERE ISNULL(GL.QT_GIR_STOCK, 0) > 0
									AND (ISNULL(QL.QT_GIREQ, 0) > 0 OR ISNULL(QL.QT_GI, 0) > 0)))
BEGIN
	SELECT @V_ERRMSG = '재고 출고된 건은 삭제 할 수 없습니다. 재고출고 취소 후 삭제하시기 바랍니다.'
	GOTO ERROR
END

SELECT @V_NO_GIR_ORG = PH.NO_GIR_ORG,
       @V_NO_PACK_ORG = PH.NO_PACK_ORG
FROM CZ_SA_PACKH PH
JOIN CZ_SA_PACKH PH1 ON PH1.CD_COMPANY = PH.CD_COMPANY AND PH1.NO_GIR = PH.NO_GIR_ORG AND PH1.NO_PACK = PH.NO_PACK_ORG
WHERE PH.CD_COMPANY = @P_CD_COMPANY
AND PH.NO_GIR = @P_NO_GIR
AND PH1.CD_TYPE = '003'
AND PH1.YN_OUTSOURCING = 'Y'

IF @V_NO_GIR_ORG IS NOT NULL
BEGIN
	SELECT @V_ERRMSG = '외주목공포장 정보가 있는 협조전은 출고 품목 삭제 할 수 없습니다. (협조전 번호 : ' + @P_NO_GIR + ', 포장정보 : ' + ISNULL(@V_NO_GIR_ORG, '') + '-' + CONVERT(NVARCHAR, ISNULL(@V_NO_PACK_ORG, 0)) + ')'             
    GOTO ERROR
END

IF EXISTS (SELECT 1 
		   FROM MM_QTIO OL
		   JOIN MM_QTIO OL1 ON OL1.CD_COMPANY = OL.CD_COMPANY AND OL1.NO_IO_MGMT = OL.NO_IO AND OL1.NO_IOLINE_MGMT = OL.NO_IOLINE
		   WHERE OL.CD_COMPANY = @P_CD_COMPANY
		   AND OL.NO_ISURCV = @P_NO_GIR)
BEGIN
	SELECT @V_ERRMSG = '계정대체, 창고이동 등에 적용받아 진행되어서 출고 품목 삭제 할 수 없습니다.'
	GOTO ERROR
END

--IF EXISTS (SELECT 1 
--           FROM MM_QTIO IL
--           JOIN PU_POL PL ON PL.CD_COMPANY = IL.CD_COMPANY AND PL.NO_PO = IL.NO_PSO_MGMT AND PL.NO_LINE = IL.NO_PSOLINE_MGMT
--           WHERE EXISTS (SELECT 1 
--                         FROM SA_GIRL GL
--                         WHERE GL.CD_COMPANY = PL.CD_COMPANY
--                         AND GL.NO_SO = PL.NO_SO
--                         AND GL.SEQ_SO = PL.NO_SOLINE
--                         AND GL.NO_GIR = @P_NO_GIR
--                         AND GL.SEQ_GIR = @P_SEQ_GIR)
--           AND IL.QT_CLS > 0)
--BEGIN
--    SELECT @V_ERRMSG = '매입정산이 완료되어서 출고 품목 삭제 할 수 없습니다.'
--	GOTO ERROR
--END

--IF EXISTS (SELECT 1
--           FROM PU_POL PL
--           WHERE EXISTS (SELECT 1 
--                         FROM SA_GIRL GL
--                         WHERE GL.CD_COMPANY = PL.CD_COMPANY
--                         AND GL.NO_SO = PL.NO_SO
--                         AND GL.SEQ_SO = PL.NO_SOLINE
--                         AND GL.NO_GIR = @P_NO_GIR
--                         AND GL.SEQ_GIR = @P_SEQ_GIR)
--           AND PL.QT_ADPAY_MM > 0)
--BEGIN
--    SELECT @V_ERRMSG = '선지급 처리 되어서 출고 품목 삭제 할 수 없습니다.'
--	GOTO ERROR
--END

COMMIT TRAN SP_CZ_SA_GIRSCH_L_D_CHECK

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_SA_GIRSCH_L_D_CHECK
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN
ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK TRAN SP_CZ_SA_GIRSCH_L_D_CHECK

GO

