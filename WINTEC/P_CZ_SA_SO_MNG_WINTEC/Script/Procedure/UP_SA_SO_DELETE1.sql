USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SO_DELETE1]    Script Date: 2019-11-14 오후 3:13:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System : 영업
**  Sub System : 수주관리
**  Page  : 수주등록
**  Desc  : 수주등록 라인 삭제
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
ALTER PROCEDURE [NEOE].[UP_SA_SO_DELETE1]
(
	@P_CD_COMPANY		NVARCHAR(7),		--회사
	@P_NO_SO			NVARCHAR(20),		--수주번호        
	@P_SEQ_SO			NUMERIC(3)			--수주항번
)
AS
SET NOCOUNT ON
DECLARE	@V_STA_SO	NVARCHAR(3)

SELECT	@V_STA_SO	= STA_SO
FROM	SA_SOL
WHERE	CD_COMPANY	= @P_CD_COMPANY
AND		NO_SO		= @P_NO_SO
AND		SEQ_SO		= @P_SEQ_SO

IF (@V_STA_SO <> 'O')
BEGIN
	--이미 수주확정, 종결 되어 수정, 삭제가 불가능합니다.
	RAISERROR ( 'SA_M000116',18,1)
	RETURN
END

  
EXEC UP_SA_SO_HOSTORY_CHK @P_CD_COMPANY, @P_NO_SO
IF @@ERROR <> 0 RETURN
  
DELETE  SA_SOL_DLV      
WHERE   CD_COMPANY  = @P_CD_COMPANY       
AND     NO_SO       = @P_NO_SO      
AND     SEQ_SO      = @P_SEQ_SO       
AND     FG_TRACK    = 'SO'  
      
DELETE  SA_SOL      
WHERE   CD_COMPANY  = @P_CD_COMPANY       
AND     NO_SO       = @P_NO_SO      
AND     SEQ_SO      = @P_SEQ_SO

IF (NOT EXISTS( SELECT 1 FROM SA_SOL  
                WHERE CD_COMPANY = @P_CD_COMPANY  
                    AND NO_SO = @P_NO_SO    )   )
BEGIN
    DELETE SA_SOH
    WHERE CD_COMPANY = @P_CD_COMPANY
        AND NO_SO = @P_NO_SO
END
GO

