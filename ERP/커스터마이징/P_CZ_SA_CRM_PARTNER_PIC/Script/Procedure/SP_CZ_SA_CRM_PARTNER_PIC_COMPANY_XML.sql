USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_PARTNER_PIC_HISTORY_XML]    Script Date: 2018-04-10 오후 2:11:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_PARTNER_PIC_COMPANY_XML] 
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
FROM CZ_CRM_PARTNER_PIC_COMPANY A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY NVARCHAR(7),
			  CD_PARTNER NVARCHAR(20),
			  SEQ		 NUMERIC(5, 0),
			  NO_INDEX	 NUMERIC(5, 0)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.CD_PARTNER = B.CD_PARTNER
AND A.SEQ = B.SEQ
AND A.NO_INDEX = B.NO_INDEX
-- ================================================== INSERT
INSERT INTO CZ_CRM_PARTNER_PIC_COMPANY 
(
	CD_COMPANY,
	CD_PARTNER,
	SEQ,
	NO_INDEX,
	DT_JOIN,
	DT_RETIRE,
	CD_EX_COMPANY,
	NM_EX_COMPANY,
	DC_DEPT,
	DC_DUTY_RESP,
	DC_RMK,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   CD_PARTNER,
	   SEQ,
	   NO_INDEX,
	   DT_JOIN,
	   DT_RETIRE,
	   CD_EX_COMPANY,
	   NM_EX_COMPANY,
	   DC_DEPT,
	   DC_DUTY_RESP,
	   DC_RMK,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY		NVARCHAR(7),
				CD_PARTNER		NVARCHAR(20),
				SEQ				NUMERIC(5, 0),
				NO_INDEX		NUMERIC(5, 0),
				DT_JOIN			NVARCHAR(8),
				DT_RETIRE		NVARCHAR(8),
				CD_EX_COMPANY	NVARCHAR(20),
				NM_EX_COMPANY	NVARCHAR(50),
				DC_DEPT			NVARCHAR(100),
				DC_DUTY_RESP	NVARCHAR(100),
				DC_RMK			NVARCHAR(MAX)) 
-- ================================================== UPDATE    
UPDATE A 
   SET A.DT_JOIN = B.DT_JOIN,
	   A.DT_RETIRE = B.DT_RETIRE,
	   A.CD_EX_COMPANY = B.CD_EX_COMPANY,
	   A.NM_EX_COMPANY = B.NM_EX_COMPANY,
	   A.DC_DEPT = B.DC_DEPT,
	   A.DC_DUTY_RESP = B.DC_DUTY_RESP,
	   A.DC_RMK = B.DC_RMK,
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_CRM_PARTNER_PIC_COMPANY A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY		NVARCHAR(7),
				CD_PARTNER		NVARCHAR(20),
				SEQ				NUMERIC(5, 0),
				NO_INDEX		NUMERIC(5, 0),
				DT_JOIN			NVARCHAR(8),
				DT_RETIRE		NVARCHAR(8),
				CD_EX_COMPANY	NVARCHAR(20),
				NM_EX_COMPANY	NVARCHAR(50),
				DC_DEPT			NVARCHAR(100),
				DC_DUTY_RESP	NVARCHAR(100),
				DC_RMK			NVARCHAR(MAX)) B 
  ON A.CD_COMPANY = B.CD_COMPANY
  AND A.CD_PARTNER = B.CD_PARTNER
  AND A.SEQ = B.SEQ
  AND A.NO_INDEX = B.NO_INDEX

EXEC SP_XML_REMOVEDOCUMENT @DOC 




GO

