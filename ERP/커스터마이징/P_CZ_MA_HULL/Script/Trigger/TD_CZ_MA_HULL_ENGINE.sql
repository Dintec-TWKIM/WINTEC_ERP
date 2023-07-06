USE [NEOE]
GO

/****** Object:  Trigger [NEOE].[TD_CZ_MA_HULL_ENGINE]    Script Date: 2020-09-02 오후 6:39:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [NEOE].[TD_CZ_MA_HULL_ENGINE] ON [NEOE].[CZ_MA_HULL_ENGINE] FOR DELETE AS                  
BEGIN                  
DECLARE                             
	@NUMROWS		INT,                 
	@ERRMSG			NVARCHAR(255),
	@V_NO_IMO		NVARCHAR(10),
	@V_NO_ENGINE	INT,
	@V_CD_ENGINE	NVARCHAR(3),
	@V_NM_MODEL		NVARCHAR(100),
	@V_CD_MAKER		NVARCHAR(4)


SELECT @NUMROWS = @@ROWCOUNT                  
IF @NUMROWS = 0 RETURN                  

/********************************************************************************************/                  
DECLARE CUR_TD_CZ_MA_HULL_ENGINE CURSOR FOR                  
SELECT D.NO_IMO, D.NO_ENGINE, D.CD_ENGINE, D.NM_MODEL, D.CD_MAKER
FROM DELETED D
OPEN CUR_TD_CZ_MA_HULL_ENGINE                  
		
FETCH NEXT FROM CUR_TD_CZ_MA_HULL_ENGINE                   
INTO @V_NO_IMO, @V_NO_ENGINE, @V_CD_ENGINE, @V_NM_MODEL, @V_CD_MAKER
		
WHILE (@@FETCH_STATUS <> -1)                  
BEGIN                  
	IF (@@FETCH_STATUS <> -2)                  
	BEGIN
		INSERT INTO CZ_MA_HULL_ENGINE_LOG
		(
			NO_IMO,
			NO_ENGINE,
			CD_ENGINE,
			NM_MODEL,
			CD_MAKER,
			DTS_DELETE
		)
		VALUES
		(
			@V_NO_IMO,
			@V_NO_ENGINE,
			@V_CD_ENGINE,
			@V_NM_MODEL,
			@V_CD_MAKER,
			NEOE.SF_SYSDATE(GETDATE())
		)

		IF (@@ERROR <> 0 )                  
		BEGIN                  
			CLOSE CUR_TD_CZ_MA_HULL_ENGINE                  
			DEALLOCATE CUR_TD_CZ_MA_HULL_ENGINE                  
			
			SELECT @ERRMSG = ' CAN NOT DELETE CZ_MA_HULL_ENGINE '
			GOTO ERROR                  
		END
	END
FETCH NEXT FROM CUR_TD_CZ_MA_HULL_ENGINE                   
INTO @V_NO_IMO, @V_NO_ENGINE, @V_CD_ENGINE, @V_NM_MODEL, @V_CD_MAKER
END                   
	 
CLOSE CUR_TD_CZ_MA_HULL_ENGINE           
DEALLOCATE CUR_TD_CZ_MA_HULL_ENGINE
 
RETURN                  
ERROR: RAISERROR (@ERRMSG, 18, 1)  
END
GO

