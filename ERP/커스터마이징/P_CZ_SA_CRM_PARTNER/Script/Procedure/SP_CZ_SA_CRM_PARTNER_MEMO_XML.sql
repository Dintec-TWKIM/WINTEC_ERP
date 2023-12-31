USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CUST_REL_MNG_PIC_COMMENT_XML]    Script Date: 2017-06-08 오후 7:57:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_PARTNER_MEMO_XML] 
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
FROM CZ_CRM_PARTNER_MEMO A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY NVARCHAR(7),
			  CD_PARTNER NVARCHAR(20),
			  NO_INDEX	 NUMERIC(18, 0)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.CD_PARTNER = B.CD_PARTNER
AND A.NO_INDEX = B.NO_INDEX
-- ================================================== INSERT
INSERT INTO CZ_CRM_PARTNER_MEMO 
(
	CD_COMPANY,
	CD_PARTNER,
	NO_INDEX,
	DC_COMMENT,			 
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   CD_PARTNER,
	   NO_INDEX,
	   REPLACE(DC_COMMENT, CHAR(10), CHAR(13) + CHAR(10)),
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY	NVARCHAR(7),
				CD_PARTNER	NVARCHAR(20),
				NO_INDEX	NUMERIC(18, 0),
				DT_COMMENT	NVARCHAR(8),
			    DC_COMMENT	NVARCHAR(MAX)) 
-- ================================================== UPDATE    
UPDATE A 
   SET A.DC_COMMENT = REPLACE(B.DC_COMMENT, CHAR(10), CHAR(13) + CHAR(10)),
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_CRM_PARTNER_MEMO A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY	NVARCHAR(7),
				CD_PARTNER	NVARCHAR(20),
				NO_INDEX	NUMERIC(18, 0),
				DT_COMMENT	NVARCHAR(8),
			    DC_COMMENT	NVARCHAR(MAX)) B 
  ON A.CD_COMPANY = B.CD_COMPANY
  AND A.CD_PARTNER = B.CD_PARTNER
  AND A.NO_INDEX = B.NO_INDEX

EXEC SP_XML_REMOVEDOCUMENT @DOC 




GO

