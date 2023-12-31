USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_TARIFF_DHL_PAY_H_XML]    Script Date: 2016-06-28 오후 1:28:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_MA_TARIFF_DHL_PAY_H_XML] 
(
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10), 
    @DOC			INT = NULL
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML 

-- ================================================== DELETE
DELETE A 
FROM CZ_MA_TARIFF_DHL_PAY_H A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
			  DT_MONTH		NVARCHAR(6)) B 
ON A.CD_COMPANY = B.CD_COMPANY 
AND A.DT_MONTH = B.DT_MONTH
-- ================================================== INSERT
INSERT INTO CZ_MA_TARIFF_DHL_PAY_H 
(
	CD_COMPANY,
	DT_MONTH,
	RT_FSC,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   DT_MONTH,
	   RT_FSC,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY			NVARCHAR(7),
				DT_MONTH			NVARCHAR(6),
				RT_FSC				NUMERIC(18, 4))
-- ================================================== UPDATE    
UPDATE A 
   SET A.RT_FSC = B.RT_FSC,
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_MA_TARIFF_DHL_PAY_H A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY			NVARCHAR(7),
				DT_MONTH			NVARCHAR(6),
				RT_FSC				NUMERIC(18, 4)) B 
  ON A.CD_COMPANY = B.CD_COMPANY
  AND A.DT_MONTH = B.DT_MONTH

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO