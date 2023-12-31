USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_PARTNER_PIC_LOG_XML]    Script Date: 2018-04-10 오전 10:43:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_PARTNER_PIC_LOG_XML] 
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
FROM CZ_CRM_PARTNER_PIC_LOG A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
			  CD_PARTNER	NVARCHAR(20),
			  SEQ			NUMERIC(5, 0),
			  NO_INDEX		INT) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.CD_PARTNER = B.CD_PARTNER
AND A.SEQ = B.SEQ
AND A.NO_INDEX = B.NO_INDEX
-- ================================================== INSERT
INSERT INTO CZ_CRM_PARTNER_PIC_LOG 
(
	CD_COMPANY,
	CD_PARTNER,
	SEQ,
	NO_INDEX,
	DT_LOG,
	DC_CONTENTS,
	QT_RATE,
	DC_RMK,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   CD_PARTNER,
	   SEQ,
	   NO_INDEX,
	   DT_LOG,
	   DC_CONTENTS,
	   QT_RATE,
	   DC_RMK, 
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY		NVARCHAR(7),
				CD_PARTNER		NVARCHAR(20),
				SEQ				NUMERIC(5, 0),
				NO_INDEX		INT,
				DT_LOG			NVARCHAR(8),
			    DC_CONTENTS		NVARCHAR(500),
				QT_RATE			NUMERIC(4, 0),
				DC_RMK			NVARCHAR(1000)) 
-- ================================================== UPDATE    
UPDATE A 
   SET A.DT_LOG = B.DT_LOG,
	   A.DC_CONTENTS = B.DC_CONTENTS,
	   A.QT_RATE = B.QT_RATE,
	   A.DC_RMK = B.DC_RMK,
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_CRM_PARTNER_PIC_LOG A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY		NVARCHAR(7),
				CD_PARTNER		NVARCHAR(20),
				SEQ				NUMERIC(5, 0),
				NO_INDEX		INT,
				DT_LOG			NVARCHAR(8),
			    DC_CONTENTS		NVARCHAR(500),
				QT_RATE			NUMERIC(4, 0),
				DC_RMK			NVARCHAR(1000)) B 
  ON A.CD_COMPANY = B.CD_COMPANY
  AND A.CD_PARTNER = B.CD_PARTNER
  AND A.SEQ = B.SEQ
  AND A.NO_INDEX = B.NO_INDEX

EXEC SP_XML_REMOVEDOCUMENT @DOC 




GO

