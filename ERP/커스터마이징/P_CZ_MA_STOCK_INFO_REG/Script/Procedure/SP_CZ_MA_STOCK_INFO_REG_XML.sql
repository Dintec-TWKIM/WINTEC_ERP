USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[PX_CZ_MA_SOURCING_REG]    Script Date: 2019-04-29 오후 5:37:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MA_STOCK_INFO_REG_XML]
(
	  @P_XML	XML
)
AS

DECLARE @DOC		INT

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML

PRINT CHAR(13) + '==================================================================================================== ▣ XML 임시테이블 생성'
SELECT CD_COMPANY, 
	   KCODE,
	   WEIGHT_LOG,
	   CD_PARTNER1,
	   CD_PARTNER2, 
	   DC_RMK, 
	   ID_USER, 
	   XML_FLAG
INTO #XML
FROM OPENXML (@DOC, '/XML/ROW', 2) WITH
			 (CD_COMPANY	NVARCHAR(7), 
			  KCODE			NVARCHAR(20),
			  WEIGHT_LOG	NUMERIC(15, 4),
			  CD_PARTNER1	NVARCHAR(20),
			  CD_PARTNER2	NVARCHAR(20), 
			  DC_RMK		NVARCHAR(500), 
			  ID_USER		NVARCHAR(10), 
			  XML_FLAG		NVARCHAR(1))

EXEC SP_XML_REMOVEDOCUMENT @DOC

SELECT * INTO #DELETE FROM #XML WHERE XML_FLAG = 'D'
SELECT * INTO #INSERT FROM #XML WHERE XML_FLAG = 'I'
SELECT * INTO #UPDATE FROM #XML WHERE XML_FLAG = 'U'

PRINT CHAR(13) + '==================================================================================================== ▣ 트랜잭션 시작 ↓'
SET XACT_ABORT ON
BEGIN TRAN PX_CZ_MA_SOURCING_REG
BEGIN TRY

PRINT CHAR(13) + '==================================================================================================== ▣ DELETE'
IF EXISTS (SELECT * FROM #DELETE) BEGIN
	DELETE A 
	FROM CZ_MA_KCODE_HGS A 
	WHERE EXISTS (SELECT 1 
				  FROM #DELETE 
				  WHERE CD_COMPANY = A.CD_COMPANY 
				  AND KCODE = A.KCODE)
END

PRINT CHAR(13) + '==================================================================================================== ▣ INSERT'
IF EXISTS (SELECT * FROM #INSERT) BEGIN
	INSERT INTO CZ_MA_KCODE_HGS
	(
		CD_COMPANY, 
		KCODE,
		WEIGHT_LOG,
		CD_PARTNER1,
		CD_PARTNER2, 
		DC_RMK, 
		ID_INSERT, 
		DTS_INSERT
	)
	SELECT CD_COMPANY, 
		   KCODE, 
		   WEIGHT_LOG,
		   CD_PARTNER1,
		   CD_PARTNER2,
		   DC_RMK, 
		   ID_USER, 
		   NEOE.SF_SYSDATE(GETDATE())
	FROM #INSERT A
END

PRINT CHAR(13) + '==================================================================================================== ▣ UPDATE'
IF EXISTS (SELECT * FROM #UPDATE) BEGIN
	UPDATE A 
	SET A.WEIGHT_LOG = (CASE WHEN ISNULL(B.WEIGHT_LOG, 0) = 0 THEN A.WEIGHT_LOG ELSE B.WEIGHT_LOG END),
	    A.CD_PARTNER1 = B.CD_PARTNER1,
		A.CD_PARTNER2 = B.CD_PARTNER2, 
	    A.DC_RMK = B.DC_RMK, 
		A.ID_UPDATE	= B.ID_USER, 
		A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	FROM CZ_MA_KCODE_HGS A
	JOIN #UPDATE B ON A.CD_COMPANY = B.CD_COMPANY AND A.KCODE = B.KCODE
END

PRINT CHAR(13) + '==================================================================================================== ▣ 트랜잭션 커밋 ↑'
DROP TABLE #XML
DROP TABLE #DELETE
DROP TABLE #INSERT
DROP TABLE #UPDATE

COMMIT
--ROLLBACK
END TRY
BEGIN CATCH
ROLLBACK
DECLARE @ERRMSG		NVARCHAR(255) = 'DINTEC ERROR : ' + ERROR_MESSAGE() GOTO ERROR
END CATCH

RETURN
ERROR:RAISERROR(@ERRMSG, 18, 1)

GO

