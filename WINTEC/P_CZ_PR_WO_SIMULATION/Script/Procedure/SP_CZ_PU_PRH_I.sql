USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_PRH_I]    Script Date: 2021-03-12 오후 3:39:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************          
*********************************************/  
ALTER PROCEDURE [NEOE].[SP_CZ_PU_PRH_I]      
(      
	@P_NO_PR		NVARCHAR(20),
	@P_CD_PLANT		NVARCHAR(7),
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_DEPT		NVARCHAR(12),
	@P_DT_PR		NCHAR(8),
	@P_NO_EMP		NVARCHAR(10),
	@P_CD_PJT		NVARCHAR(20),
	@P_FG_PR_TP		NCHAR(3),
	@P_NO_PRTYPE	NVARCHAR(20),
	@P_DC_RMK		NVARCHAR(100),
	@P_ID_INSERT	NVARCHAR(15)
)      
AS

INSERT INTO PU_PRH       
(
	CD_COMPANY,
	CD_PLANT,
	NO_PR,
	CD_DEPT,
	DT_PR,
	NO_EMP,
	CD_PJT,
	FG_PR_TP,
	NO_PRTYPE,
	DC_RMK,
	ID_INSERT,
	DTS_INSERT
)      
VALUES 
(
	@P_CD_COMPANY,
	@P_CD_PLANT,
	@P_NO_PR,
	@P_CD_DEPT,
	@P_DT_PR,
	@P_NO_EMP,
	@P_CD_PJT,
	@P_FG_PR_TP,
	@P_NO_PRTYPE,
	@P_DC_RMK,
	@P_ID_INSERT,
	NEOE.SF_SYSDATE(GETDATE())
)

GO