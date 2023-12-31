USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_PARTNER_PIC_PEOPLE_XML]    Script Date: 2018-04-10 오후 4:06:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_PARTNER_PIC_PEOPLE_XML] 
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
FROM CZ_CRM_PARTNER_PIC_PEOPLE A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY	 NVARCHAR(7),
			  CD_PARTNER	 NVARCHAR(20),
			  SEQ			 NUMERIC(5, 0),
			  CD_PARTNER_SUB NVARCHAR(20),
			  SEQ_SUB		 NUMERIC(5, 0)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.CD_PARTNER = B.CD_PARTNER
AND A.SEQ = B.SEQ
AND A.CD_PARTNER_SUB = B.CD_PARTNER_SUB
AND A.SEQ_SUB = B.SEQ_SUB
-- ================================================== INSERT
INSERT INTO CZ_CRM_PARTNER_PIC_PEOPLE 
(
	CD_COMPANY,
	CD_PARTNER,
	SEQ,
	CD_PARTNER_SUB,
	SEQ_SUB,
	DC_RMK,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   CD_PARTNER,
	   SEQ,
	   CD_PARTNER_SUB,
	   SEQ_SUB,
	   DC_RMK,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY		NVARCHAR(7),
				CD_PARTNER		NVARCHAR(20),
				SEQ				NUMERIC(5, 0),
				CD_PARTNER_SUB	NVARCHAR(20),
				SEQ_SUB			NUMERIC(5, 0),
				DC_RMK			NVARCHAR(MAX)) 
-- ================================================== UPDATE    
UPDATE A 
   SET A.DC_RMK = B.DC_RMK,
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_CRM_PARTNER_PIC_PEOPLE A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY		NVARCHAR(7),
				CD_PARTNER		NVARCHAR(20),
				SEQ				NUMERIC(5, 0),
				CD_PARTNER_SUB	NVARCHAR(20),
				SEQ_SUB			NUMERIC(5, 0),
				DC_RMK			NVARCHAR(MAX)) B 
  ON A.CD_COMPANY = B.CD_COMPANY
  AND A.CD_PARTNER = B.CD_PARTNER
  AND A.SEQ = B.SEQ
  AND A.CD_PARTNER_SUB = B.CD_PARTNER_SUB
  AND A.SEQ_SUB = B.SEQ_SUB

EXEC SP_XML_REMOVEDOCUMENT @DOC 




GO

