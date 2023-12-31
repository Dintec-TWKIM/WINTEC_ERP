USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_HULL_NO_IMO_U]    Script Date: 2016-04-18 오후 2:59:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--SP_CZ_MA_HULL_U '9624500', '9607710'
ALTER PROCEDURE [NEOE].[SP_CZ_MA_HULL_NO_IMO_U]
(
    @P_NO_IMO_BEFORE  NVARCHAR(10),
	@P_NO_IMO_AFTER   NVARCHAR(10)
)
AS

DECLARE @V_ERRMSG   VARCHAR(255)   -- ERROR 메시지

BEGIN TRAN SP_CZ_MA_HULL_U
BEGIN TRY

UPDATE CZ_MA_HULL
SET NO_IMO = @P_NO_IMO_AFTER
WHERE NO_IMO = @P_NO_IMO_BEFORE

UPDATE CZ_MA_ENGINE_PARTS
SET NO_IMO = @P_NO_IMO_AFTER
WHERE NO_IMO = @P_NO_IMO_BEFORE

UPDATE CZ_SA_QTNH
SET NO_IMO = @P_NO_IMO_AFTER
WHERE NO_IMO = @P_NO_IMO_BEFORE

UPDATE CZ_SA_QTNH_HST
SET NO_IMO = @P_NO_IMO_AFTER
WHERE NO_IMO = @P_NO_IMO_BEFORE

UPDATE SA_SOH
SET NO_IMO = @P_NO_IMO_AFTER
WHERE NO_IMO = @P_NO_IMO_BEFORE

UPDATE CZ_SA_GIRH_WORK_DETAIL
SET NO_IMO = @P_NO_IMO_AFTER
WHERE NO_IMO = @P_NO_IMO_BEFORE

UPDATE CZ_SA_GIRH_PACK_DETAIL
SET NO_IMO = @P_NO_IMO_AFTER
WHERE NO_IMO = @P_NO_IMO_BEFORE

UPDATE CZ_SA_IVMNG_INVOICE
SET NO_IMO = @P_NO_IMO_AFTER
WHERE NO_IMO = @P_NO_IMO_BEFORE

UPDATE CZ_SA_CLAIMH
SET NO_IMO = @P_NO_IMO_AFTER
WHERE NO_IMO = @P_NO_IMO_BEFORE

COMMIT TRAN SP_CZ_MA_HULL_U

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_MA_HULL_U
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN
ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK TRAN SP_CZ_MA_HULL_U


GO

