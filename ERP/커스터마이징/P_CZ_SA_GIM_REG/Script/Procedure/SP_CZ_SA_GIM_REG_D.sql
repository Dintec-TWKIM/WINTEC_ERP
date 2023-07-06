USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIM_REG_D]    Script Date: 2015-10-20 오후 8:43:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************      
**  System : 영업      
**  Sub System : 출고관리      
**  Page  : 출고관리      
**  Desc  : 출고관리 Head, Line 일괄 삭제      
**      
**  Return Values      
**      
**  작    성    자  :  오성영      
**  작    성    일  :   2007.07.26    
**  수    정    자  :       
*********************************************      
** Change History      
*********************************************      
** 2008.03.11  출고건 삭제했을 경우 MM_INVOICE_TAG(TAG관련 TABLE ) 역시 Syncronize 시켜야 함으로 수정함.    
*********************************************/      
ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIM_REG_D]  
(      
    @P_CD_COMPANY   NVARCHAR(7),    --회사      
    @P_NO_IO		NVARCHAR(20),
	@P_NO_GIR		NVARCHAR(20),
	@P_ID_DELETE	NVARCHAR(15)
)      
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

BEGIN TRAN SP_CZ_SA_GIM_REG_D
BEGIN TRY

DECLARE @V_ERRMSG		VARCHAR(255)   -- ERROR 메시지
DECLARE @V_NO_GIREQ		NVARCHAR(20)
DECLARE @V_NO_IO		NVARCHAR(20)

IF EXISTS (SELECT 1     
           FROM MM_QTIOH OH
		   JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
           JOIN SA_SOL SL ON SL.CD_COMPANY = OL.CD_COMPANY AND SL.NO_SO = OL.NO_PSO_MGMT AND SL.SEQ_SO = OL.NO_PSOLINE_MGMT
           WHERE OL.CD_COMPANY = @P_CD_COMPANY
           AND OL.NO_IO = @P_NO_IO
		   AND ISNULL(OH.YN_RETURN, 'N') = 'N'    
           AND ISNULL(SL.STA_SO, '') <> 'R')     
BEGIN    
	-- 수주가 진행 혹은 종결 이므로 저장 할 수 없습니다.
	SELECT @V_ERRMSG = 'SA_M000133'
	GOTO ERROR
END

IF EXISTS (SELECT 1 
		   FROM MM_QTIO OL
		   JOIN MM_QTIO OL1 ON OL1.CD_COMPANY = OL.CD_COMPANY AND OL1.NO_IO_MGMT = OL.NO_IO AND OL1.NO_IOLINE_MGMT = OL.NO_IOLINE
		   WHERE OL.CD_COMPANY = @P_CD_COMPANY
		   AND OL.NO_IO = @P_NO_IO)
BEGIN
	SELECT @V_ERRMSG = '[' + @P_NO_IO + '] 계정대체, 창고이동 등에 적용받아 진행되어서 해당 데이터를 삭제 한 후에 출고 삭제가 가능합니다.'
	GOTO ERROR
END

IF (@P_CD_COMPANY = 'K100' OR @P_CD_COMPANY = 'K200') AND
   EXISTS (SELECT 1
		   FROM SA_GIRH GH
		   WHERE GH.CD_COMPANY = @P_CD_COMPANY
		   AND GH.NO_GIR = @P_NO_GIR
		   AND EXISTS (SELECT 1 
		   			   FROM SA_GIRL GL
		   			   JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_FILE = GL.NO_SO AND QL.NO_LINE = GL.SEQ_SO
		   			   WHERE GL.CD_COMPANY = GH.CD_COMPANY
		   			   AND GL.NO_GIR = GH.NO_GIR
					   AND GL.NO_SO NOT LIKE 'ZZ%')) AND
   
   NOT EXISTS (SELECT 1
			   FROM FI_GWDOCU GW
			   JOIN CZ_TEAG_APPDOC_LINE AL ON AL.DOC_ID = GW.APP_DOC_ID
			   WHERE GW.CD_COMPANY = 'K100' 
			   AND GW.CD_PC = '010000' 
			   AND GW.NO_DOCU = @P_CD_COMPANY + '-' + @P_NO_IO + '-D'
			   AND AL.APP_YN = '1')
BEGIN
	SELECT @V_ERRMSG = '[' + @P_NO_IO + '] 결재 진행중이거나 완료 된 상태에만 출고 삭제 가능 합니다.'
	GOTO ERROR
END

EXEC SP_CZ_SA_GIR_LOG @P_CD_COMPANY, '001', @P_NO_GIR, @P_ID_DELETE

SELECT @V_NO_GIREQ = MAX(NO_GIREQ)
FROM MM_GIREQL
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_PO = @P_NO_GIR
AND CD_SL = 'SL03'
AND CD_GRSL = 'SL01'
GROUP BY CD_COMPANY, NO_PO

SELECT @V_NO_IO = MAX(NO_IO)
FROM MM_QTIO
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_ISURCV = @V_NO_GIREQ
GROUP BY CD_COMPANY, NO_ISURCV

DELETE FROM MM_QTIO
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_IO = @V_NO_IO

DELETE FROM MM_QTIOH
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_IO = @V_NO_IO

DELETE FROM MM_GIREQL
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_GIREQ = @V_NO_GIREQ

DELETE FROM MM_GIREQH
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_GIREQ = @V_NO_GIREQ

DELETE FROM MM_QTIO    
WHERE CD_COMPANY = @P_CD_COMPANY    
AND NO_IO = @P_NO_IO
    
DELETE FROM MM_QTIOH    
WHERE CD_COMPANY = @P_CD_COMPANY    
AND NO_IO = @P_NO_IO

UPDATE SA_GIRH
SET STA_GIR = 'R'
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_GIR = @P_NO_GIR

COMMIT TRAN SP_CZ_SA_GIM_REG_D

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_SA_GIM_REG_D
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN
ERROR: RAISERROR(@V_ERRMSG, 18, 1)

GO