USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_PITEM_LT_SUB_S]    Script Date: 2016-06-27 오후 5:01:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MA_PITEM_LT_SUB_S]          
(  
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_ITEM			NVARCHAR(20)
)  
AS  

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT PH.NO_PO,
	   PL.CD_ITEM,
	   PH.DT_PO,
	   IL.DT_IO,
	   PL.QT_PO,
	   IL.QT_IO,
	   DATEDIFF(DAY, PH.DT_PO, IL.DT_IO) AS LT 
FROM PU_POH PH
JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
JOIN MM_QTIO IL ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
WHERE PH.CD_COMPANY = @P_CD_COMPANY
AND PH.DT_PO BETWEEN NEOE.TODAY(-366) AND NEOE.TODAY(-1)
AND PH.NO_PO LIKE 'ST%'
AND PL.CD_ITEM = @P_CD_ITEM
ORDER BY PH.DT_PO

GO