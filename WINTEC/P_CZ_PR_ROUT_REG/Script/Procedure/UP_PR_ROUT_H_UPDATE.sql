USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PR_ROUT_H_UPDATE]    Script Date: 2021-02-18 오후 1:26:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_PR_ROUT_H_UPDATE]
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_CD_ITEM				NVARCHAR(50),
	@P_CD_PLANT				NVARCHAR(7),
	@P_NO_OPPATH			NVARCHAR(3),
	@P_DC_OPPATH			NVARCHAR(50),
	@P_CD_DEPT				NVARCHAR(12),
	@P_NO_EMP               NVARCHAR(10),
	@P_ID_UPDATE			NVARCHAR(15),
	@P_TP_OPPATH			NVARCHAR(3), 
	@P_NO_EMP_WRIT			NVARCHAR(10),
	@P_NO_EMP_CONF			NVARCHAR(10),
	@P_TP_WO				NVARCHAR(3)
)
AS

DECLARE @P_DTS_UPDATE	VARCHAR(14), 
		@COUNT			INT

SET		@P_DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())

SELECT	@COUNT = COUNT(1)
  FROM	PR_ROUT
 WHERE  CD_COMPANY = @P_CD_COMPANY
   AND  CD_ITEM    = @P_CD_ITEM
   AND  CD_PLANT   = @P_CD_PLANT
   AND  NO_OPPATH  = @P_NO_OPPATH
   AND  TP_OPPATH  = @P_TP_OPPATH


IF (@COUNT = 0)
BEGIN
		INSERT INTO PR_ROUT 
		(
			CD_COMPANY,		CD_ITEM,        CD_PLANT,			NO_OPPATH,			DC_OPPATH,                
			CD_DEPT,		NO_EMP,			ID_INSERT,			DTS_INSERT,			TP_OPPATH,
			NO_EMP_WRIT,	NO_EMP_CONF,	TP_WO
		)
		VALUES
		(
			@P_CD_COMPANY,  @P_CD_ITEM,     @P_CD_PLANT,        @P_NO_OPPATH,       @P_DC_OPPATH,                
			@P_CD_DEPT,     @P_NO_EMP,      @P_ID_UPDATE,       @P_DTS_UPDATE,      @P_TP_OPPATH,
			@P_NO_EMP_WRIT,	@P_NO_EMP_CONF,	@P_TP_WO
		)
END
ELSE
BEGIN
		UPDATE  PR_ROUT 
		   SET  DC_OPPATH   = @P_DC_OPPATH,        
				CD_DEPT		= @P_CD_DEPT,                
				NO_EMP      = @P_NO_EMP,
				ID_UPDATE   = @P_ID_UPDATE,        
				DTS_UPDATE  = @P_DTS_UPDATE,        
				TP_OPPATH   = @P_TP_OPPATH,
				NO_EMP_WRIT = @P_NO_EMP_WRIT,        
				NO_EMP_CONF = @P_NO_EMP_CONF,
				TP_WO		= @P_TP_WO
		 WHERE  CD_COMPANY  = @P_CD_COMPANY
		   AND  CD_ITEM     = @P_CD_ITEM
		   AND  CD_PLANT    = @P_CD_PLANT
		   AND  NO_OPPATH   = @P_NO_OPPATH
		   AND  TP_OPPATH   = @P_TP_OPPATH
END
GO

