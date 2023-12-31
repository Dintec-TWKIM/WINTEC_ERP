USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CUST_REL_MNG_PIC_COMMENT_XML]    Script Date: 2017-06-08 오후 7:57:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_MEETING_ATTENDEE_XML] 
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
FROM CZ_SA_MEETING_ATTENDEE A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
			  NO_MEETING	NVARCHAR(20),
			  TP_INOUT		NVARCHAR(3),
			  NO_INDEX		NUMERIC(18, 0)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_MEETING = B.NO_MEETING
AND A.TP_INOUT = B.TP_INOUT
AND A.NO_INDEX = B.NO_INDEX
-- ================================================== INSERT
INSERT INTO CZ_SA_MEETING_ATTENDEE 
(
	CD_COMPANY,
	NO_MEETING,
	TP_INOUT,
	CD_PARTNER,
	NO_INDEX,
	CD_COMPANY_ATTENDEE,
	NO_ATTENDEE,
	NM_ATTENDEE,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   NO_MEETING,
	   TP_INOUT,
	   CD_PARTNER,
	   NO_INDEX,
	   CD_COMPANY_ATTENDEE,
	   NO_ATTENDEE,
	   NM_ATTENDEE,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY				NVARCHAR(7),
				NO_MEETING				NVARCHAR(20),
				TP_INOUT				NVARCHAR(3),
				CD_PARTNER				NVARCHAR(20),
				NO_INDEX				NUMERIC(18, 0),
				CD_COMPANY_ATTENDEE		NVARCHAR(7),
				NO_ATTENDEE				NVARCHAR(20),
				NM_ATTENDEE				NVARCHAR(100))
-- ================================================== UPDATE    
UPDATE A 
   SET A.CD_COMPANY_ATTENDEE = B.CD_COMPANY_ATTENDEE,
	   A.NO_ATTENDEE = B.NO_ATTENDEE,
	   A.NM_ATTENDEE = B.NM_ATTENDEE,
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_SA_MEETING_ATTENDEE A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY				NVARCHAR(7),
				NO_MEETING				NVARCHAR(20),
				TP_INOUT				NVARCHAR(3),
				NO_INDEX				NUMERIC(18, 0),
				CD_COMPANY_ATTENDEE		NVARCHAR(7),
				NO_ATTENDEE				NVARCHAR(20),
				NM_ATTENDEE				NVARCHAR(100)) B 
  ON A.CD_COMPANY = B.CD_COMPANY
  AND A.NO_MEETING = B.NO_MEETING
  AND A.TP_INOUT = B.TP_INOUT
  AND A.NO_INDEX = B.NO_INDEX

EXEC SP_XML_REMOVEDOCUMENT @DOC 




GO

