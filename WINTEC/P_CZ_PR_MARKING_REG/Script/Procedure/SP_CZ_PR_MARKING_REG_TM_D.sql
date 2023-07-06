USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MARKING_REG_TM_D]    Script Date: 2022-09-14 오전 9:49:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER PROCEDURE [NEOE].[SP_CZ_PR_MARKING_REG_TM_D]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_SO			NVARCHAR(20),
	@P_SEQ_SO			NUMERIC(5, 0),
	@P_CD_ITEM			NVARCHAR(20)
)
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @NO_LINE		NUMERIC(5, 0)

SELECT @NO_LINE = A.NO_LINE
FROM (SELECT TOP 1 TW.IDX AS NO_LINE
	  FROM CZ_SA_SOL_TRUST_WINTEC TW
	  WHERE TW.CD_COMPANY = @P_CD_COMPANY
	  AND TW.NO_SO = @P_NO_SO
	  AND TW.SEQ_SO = @P_SEQ_SO
	  AND TW.CD_ITEM = @P_CD_ITEM
	  AND ISNULL(TW.YN_TRUST, 'N') = 'Y'
	  ORDER BY TW.IDX DESC) A

DELETE A
FROM CZ_SA_TRUST_MARKING A
WHERE A.CD_COMPANY = @P_CD_COMPANY
AND A.NO_SO = @P_NO_SO
AND A.SEQ_SO = @P_SEQ_SO
AND A.CD_ITEM = @P_CD_ITEM
AND A.NO_LINE = @NO_LINE


UPDATE CZ_SA_SOL_TRUST_WINTEC
SET YN_TRUST = NULL, NO_ID = NULL
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_SO = @P_NO_SO
AND SEQ_SO = @P_SEQ_SO
AND CD_ITEM = @P_CD_ITEM
AND IDX = @NO_LINE

GO


