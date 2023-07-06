USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_HSD_STOCK_RPT_EXCEL]    Script Date: 2015-06-16 오후 3:36:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PU_HSD_STOCK_RPT_EXCEL] 
(
	@P_XML			XML
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_DOC INT

EXEC SP_XML_PREPAREDOCUMENT @V_DOC OUTPUT, @P_XML 

SELECT * INTO #XML
FROM OPENXML (@V_DOC, '/XML/ROW', 2) WITH 
(
	[No.]						INT,
	[Order No.]					NVARCHAR(255),
	[PJT No.]					NVARCHAR(255),
	[수주일]						NVARCHAR(255),
	[품명]						NVARCHAR(255),
	[판매수량]					INT,
	[판매처]						NVARCHAR(255),
	[선박정보]					NVARCHAR(255),
	[판매선박 Engine Type]		NVARCHAR(255),
	[PO REF.No]					NVARCHAR(255),
	[분실여부]					NVARCHAR(1),
	[Remark]					NVARCHAR(255)
)

EXEC SP_XML_REMOVEDOCUMENT @V_DOC 

INSERT INTO CZ_SA_HSD_DATA_LOG
(
	NO_HSD,
	NO_ORDER,
	NO_PJT,
	DT_SO,
	NM_ITEM,
	QT_SO,
	NM_PARTNER,
	NM_VESSEL,
	TP_ENGINE,
	NO_PO_PARTNER,
	YN_LOSS,
	DC_RMK
)
SELECT [No.],
	   [Order No.],
	   [PJT No.],
	   [수주일],
	   [품명],
	   [판매수량],
	   [판매처],
	   [선박정보],
	   [판매선박 Engine Type],
	   [PO REF.No],
	   [분실여부],
	   [Remark]
FROM #XML A
WHERE ISNULL([판매수량], 0) > 0

GO