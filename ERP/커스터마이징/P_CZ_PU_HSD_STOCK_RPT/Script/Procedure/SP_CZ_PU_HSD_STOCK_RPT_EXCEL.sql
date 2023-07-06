USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_HSD_STOCK_RPT_EXCEL]    Script Date: 2015-06-16 ���� 3:36:33 ******/
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
	[������]						NVARCHAR(255),
	[ǰ��]						NVARCHAR(255),
	[�Ǹż���]					INT,
	[�Ǹ�ó]						NVARCHAR(255),
	[��������]					NVARCHAR(255),
	[�Ǹż��� Engine Type]		NVARCHAR(255),
	[PO REF.No]					NVARCHAR(255),
	[�нǿ���]					NVARCHAR(1),
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
	   [������],
	   [ǰ��],
	   [�Ǹż���],
	   [�Ǹ�ó],
	   [��������],
	   [�Ǹż��� Engine Type],
	   [PO REF.No],
	   [�нǿ���],
	   [Remark]
FROM #XML A
WHERE ISNULL([�Ǹż���], 0) > 0

GO