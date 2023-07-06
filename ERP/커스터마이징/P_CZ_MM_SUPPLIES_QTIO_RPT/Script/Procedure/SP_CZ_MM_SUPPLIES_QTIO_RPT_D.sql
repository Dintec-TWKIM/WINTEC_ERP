USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_MM_SUPPLIES_QTIO_RPT_D]    Script Date: 2022-11-25 오후 5:21:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MM_SUPPLIES_QTIO_RPT_D]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_IO			NVARCHAR(20)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE CZ_MM_SUPPLIES_QTIO
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_IO = @P_NO_IO