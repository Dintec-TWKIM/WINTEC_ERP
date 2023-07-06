USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[CZ_MAILBAGLIST_UH]    Script Date: 2023-05-03 ���� 4:46:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[CZ_MAILBAGLIST_UH]
(
	@P_CD_COMPANY	NVARCHAR(20),
	@P_NO_EMP		NVARCHAR(10),
	@P_CD_SEND		NVARCHAR(2),
	@P_DT_ACCT		NVARCHAR(14)
)
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

UPDATE CZ_MAILBAGLIST_H
SET 
	@P_CD_COMPANY	= @P_CD_COMPANY,
	@P_NO_EMP		= @P_NO_EMP
FROM CZ_MAILBAGLIST_H
WHERE CD_SEND	= @P_CD_SEND
AND DT_ACCT		= @P_DT_ACCT