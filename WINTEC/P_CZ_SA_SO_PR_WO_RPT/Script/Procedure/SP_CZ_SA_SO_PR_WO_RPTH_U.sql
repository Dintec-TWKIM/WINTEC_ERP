USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SO_PR_WO_RPTH_U]    Script Date: 2019-11-14 오후 3:11:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_SA_SO_PR_WO_RPTH_U]        
(  
    @P_CD_COMPANY		NVARCHAR(7),
	@P_NO_SO			NVARCHAR(20),
	@P_SEQ_SO			NUMERIC(5, 0),
	@P_DT_EXPECT		NVARCHAR(8),
	@P_ID_UPDATE		NVARCHAR(15)		
)        
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

UPDATE SA_SOL
SET CD_MNGD1 = @P_DT_EXPECT,
	ID_UPDATE = @P_ID_UPDATE,
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_SO = @P_NO_SO
AND SEQ_SO = @P_SEQ_SO

GO