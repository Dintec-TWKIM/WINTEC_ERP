USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MARKING_REG_TM_I]    Script Date: 2022-09-14 오전 10:02:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER PROCEDURE [NEOE].[SP_CZ_PR_MARKING_REG_TM_I]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_SO			NVARCHAR(20),
	@P_SEQ_SO			NUMERIC(5, 0),
	@P_CD_ITEM			NVARCHAR(20),
	@P_DC_TEXT			NVARCHAR(20),
	@P_NO_ID			NVARCHAR(20),
	@P_ID_INSERT		NVARCHAR(15)
)
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @NO_LINE	NUMERIC(5, 0)

SELECT @NO_LINE = A.NO_LINE
FROM (SELECT TW.IDX AS NO_LINE
	  FROM CZ_SA_SOL_TRUST_WINTEC TW
	  WHERE TW.CD_COMPANY = @P_CD_COMPANY
	  AND TW.NO_SO = @P_NO_SO
	  AND TW.SEQ_SO = @P_SEQ_SO
	  AND TW.CD_ITEM = @P_CD_ITEM
	  AND TW.NO_TRUST = @P_DC_TEXT) A

INSERT INTO CZ_SA_TRUST_MARKING
(
	CD_COMPANY,
	NO_SO,
	SEQ_SO,
	CD_ITEM,
	NO_LINE,
	DC_TEXT,
	NO_ID,
	DTS_INSERT,
	ID_INSERT
)
SELECT @P_CD_COMPANY,
	   @P_NO_SO,
	   @P_SEQ_SO,
	   @P_CD_ITEM,
	   @NO_LINE,
	   @P_DC_TEXT,
	   @P_NO_ID,
	   NEOE.SF_SYSDATE(GETDATE()),
	   @P_ID_INSERT

UPDATE CZ_SA_SOL_TRUST_WINTEC
SET YN_TRUST = 'Y', NO_ID = @P_NO_ID
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_SO = @P_NO_SO
AND SEQ_SO = @P_SEQ_SO
AND CD_ITEM = @P_CD_ITEM
AND IDX = @NO_LINE

GO


