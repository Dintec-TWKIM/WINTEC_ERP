USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_STOCK_INFO_REG_ITEM_XML]    Script Date: 2019-05-08 오후 1:58:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_MA_STOCK_INFO_REG_ITEM_XML]
(
	  @P_XML	XML
)
AS

DECLARE @DOC		INT

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML

PRINT CHAR(13) + '==================================================================================================== ▣ XML 임시테이블 생성'
SELECT CD_COMPANY, 
	   CD_ITEM,
	   WEIGHT,
	   CD_PARTNER1,
	   CD_PARTNER2, 
	   DC_RMK1,
	   DC_RMK2,
	   ID_USER, 
	   XML_FLAG
INTO #XML
FROM OPENXML (@DOC, '/XML/ROW', 2) WITH
			 (CD_COMPANY	NVARCHAR(7), 
			  CD_ITEM		NVARCHAR(20),
			  WEIGHT	    NUMERIC(15, 4),
			  CD_PARTNER1	NVARCHAR(20),
			  CD_PARTNER2	NVARCHAR(20), 
			  DC_RMK1		NVARCHAR(200), 
			  DC_RMK2		NVARCHAR(200), 
			  ID_USER		NVARCHAR(10), 
			  XML_FLAG		NVARCHAR(1))

EXEC SP_XML_REMOVEDOCUMENT @DOC

SELECT * INTO #UPDATE FROM #XML WHERE XML_FLAG = 'U'

PRINT CHAR(13) + '==================================================================================================== ▣ 트랜잭션 시작 ↓'
SET XACT_ABORT ON
BEGIN TRAN PX_CZ_MA_SOURCING_REG_UCODE
BEGIN TRY

PRINT CHAR(13) + '==================================================================================================== ▣ UPDATE'
IF EXISTS (SELECT * FROM #UPDATE) BEGIN
	UPDATE A 
	SET A.WEIGHT = (CASE WHEN ISNULL(B.WEIGHT, 0) = 0 THEN A.WEIGHT ELSE B.WEIGHT END),
		A.CD_PARTNER1 = B.CD_PARTNER1,
		A.CD_PARTNER2 = B.CD_PARTNER2, 
	    A.DC_RMK1 = B.DC_RMK1,
		A.DC_RMK2 = B.DC_RMK2,
		A.CD_USERDEF17 = NEOE.SF_SYSDATE(GETDATE()),
		A.ID_UPDATE	= B.ID_USER, 
		A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	FROM MA_PITEM A
	JOIN #UPDATE B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_ITEM = B.CD_ITEM
END

PRINT CHAR(13) + '==================================================================================================== ▣ 트랜잭션 커밋 ↑'
DROP TABLE #XML
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

