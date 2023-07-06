USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[CZ_MAILBAGLIST_DH]    Script Date: 2023-05-03 오후 4:45:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--리스트삭제용
ALTER PROCEDURE [NEOE].[CZ_MAILBAGLIST_DH]
(
	@P_CD_SEND		NVARCHAR(2),
	@P_DT_ACCT		NVARCHAR(14)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE CZ_MAILBAGLIST_H
WHERE CD_SEND = @P_CD_SEND
AND DT_ACCT	= @P_DT_ACCT