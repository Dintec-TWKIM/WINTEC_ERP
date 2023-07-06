USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CUST_REL_MNG_PIC_COMMENT_XML]    Script Date: 2017-06-08 오후 7:57:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_MEETING_MEMO_XML] 
(
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10), 
    @DOC			INT = NULL
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML 

-- ================================================== DELETE
INSERT INTO CZ_SA_MEETING_MEMO_LOG
(
	CD_COMPANY,
	NO_MEETING,
	SEQ,
	CD_PARTNER,
	DT_MEETING,
	DC_TIME,
	DC_LOCATION,
	DC_SUBJECT,
	DC_PURPOSE,
	DC_MEETING,
	TP_LOG,
	ID_LOG,
	DTS_LOG
)
SELECT A.CD_COMPANY,
	   A.NO_MEETING,
	   (SELECT ISNULL(MAX(SEQ), 0) 
	    FROM CZ_SA_MEETING_MEMO_LOG
		WHERE CD_COMPANY = A.CD_COMPANY
		AND NO_MEETING = A.NO_MEETING) + ROW_NUMBER() OVER (PARTITION BY A.CD_COMPANY, A.NO_MEETING ORDER BY A.CD_COMPANY, A.NO_MEETING) AS SEQ,
	   A.CD_PARTNER,
	   A.DT_MEETING,
	   A.DC_TIME,
	   A.DC_LOCATION,
	   A.DC_SUBJECT,
	   A.DC_PURPOSE,
	   A.DC_MEETING,
	   'D' AS TP_LOG,
	   @P_ID_USER AS ID_LOG,
	   NEOE.SF_SYSDATE(GETDATE()) AS DTS_LOG
FROM CZ_SA_MEETING_MEMO A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY NVARCHAR(7),
			  NO_MEETING NVARCHAR(20)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_MEETING = B.NO_MEETING

DELETE A 
FROM CZ_SA_MEETING_MEMO A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY NVARCHAR(7),
			  NO_MEETING NVARCHAR(20)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_MEETING = B.NO_MEETING

DELETE A 
FROM CZ_SA_MEETING_ATTENDEE A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY NVARCHAR(7),
			  NO_MEETING NVARCHAR(20)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_MEETING = B.NO_MEETING
-- ================================================== INSERT
INSERT INTO CZ_SA_MEETING_MEMO 
(
	CD_COMPANY,
	NO_MEETING,
	CD_PARTNER,
	DT_MEETING,
	DC_TIME,
	DC_LOCATION,
	DC_SUBJECT,
	DC_PURPOSE,
	DC_MEETING,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   NO_MEETING,
	   CD_PARTNER,
	   DT_MEETING,
	   REPLACE(DC_TIME, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_LOCATION, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_SUBJECT, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_PURPOSE, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_MEETING, CHAR(10), CHAR(13) + CHAR(10)),
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY		NVARCHAR(7),
				NO_MEETING		NVARCHAR(20),
				CD_PARTNER		NVARCHAR(20),
				DT_MEETING		NVARCHAR(8),
				DC_TIME			NVARCHAR(100),
				DC_LOCATION		NVARCHAR(500),
				DC_SUBJECT		NVARCHAR(500),
				DC_PURPOSE		NVARCHAR(500),
				DC_MEETING		NVARCHAR(MAX))

INSERT INTO CZ_SA_MEETING_MEMO_LOG
(
	CD_COMPANY,
	NO_MEETING,
	SEQ,
	CD_PARTNER,
	DT_MEETING,
	DC_TIME,
	DC_LOCATION,
	DC_SUBJECT,
	DC_PURPOSE,
	DC_MEETING,
	TP_LOG,
	ID_LOG,
	DTS_LOG
)
SELECT A.CD_COMPANY,
	   A.NO_MEETING,
	   (SELECT ISNULL(MAX(SEQ), 0) 
	    FROM CZ_SA_MEETING_MEMO_LOG
		WHERE CD_COMPANY = A.CD_COMPANY
		AND NO_MEETING = A.NO_MEETING) + ROW_NUMBER() OVER (PARTITION BY A.CD_COMPANY, A.NO_MEETING ORDER BY A.CD_COMPANY, A.NO_MEETING) AS SEQ,
	   A.CD_PARTNER,
	   A.DT_MEETING,
	   A.DC_TIME,
	   A.DC_LOCATION,
	   A.DC_SUBJECT,
	   A.DC_PURPOSE,
	   A.DC_MEETING,
	   'I' AS TP_LOG,
	   @P_ID_USER AS ID_LOG,
	   NEOE.SF_SYSDATE(GETDATE()) AS DTS_LOG
FROM CZ_SA_MEETING_MEMO A 
JOIN OPENXML (@DOC, '/XML/I', 2) 
        WITH (CD_COMPANY NVARCHAR(7),
			  NO_MEETING NVARCHAR(20)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_MEETING = B.NO_MEETING
-- ================================================== UPDATE
UPDATE A 
   SET A.CD_PARTNER = B.CD_PARTNER,
	   A.DT_MEETING = B.DT_MEETING,
	   A.DC_TIME = REPLACE(B.DC_TIME, CHAR(10), CHAR(13) + CHAR(10)),
	   A.DC_LOCATION = REPLACE(B.DC_LOCATION, CHAR(10), CHAR(13) + CHAR(10)),
	   A.DC_SUBJECT = REPLACE(B.DC_SUBJECT, CHAR(10), CHAR(13) + CHAR(10)),
	   A.DC_PURPOSE = REPLACE(B.DC_PURPOSE, CHAR(10), CHAR(13) + CHAR(10)),
	   A.DC_MEETING = REPLACE(B.DC_MEETING, CHAR(10), CHAR(13) + CHAR(10)),
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_SA_MEETING_MEMO A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY		NVARCHAR(7),
				NO_MEETING		NVARCHAR(20),
				CD_PARTNER		NVARCHAR(20),
				DT_MEETING		NVARCHAR(8),
				DC_TIME			NVARCHAR(100),
				DC_LOCATION		NVARCHAR(500),
				DC_SUBJECT		NVARCHAR(500),
				DC_PURPOSE		NVARCHAR(500),
				DC_MEETING		NVARCHAR(MAX)) B 
  ON A.CD_COMPANY = B.CD_COMPANY
  AND A.NO_MEETING = B.NO_MEETING

INSERT INTO CZ_SA_MEETING_MEMO_LOG
(
	CD_COMPANY,
	NO_MEETING,
	SEQ,
	CD_PARTNER,
	DT_MEETING,
	DC_TIME,
	DC_LOCATION,
	DC_SUBJECT,
	DC_PURPOSE,
	DC_MEETING,
	TP_LOG,
	ID_LOG,
	DTS_LOG
)
SELECT A.CD_COMPANY,
	   A.NO_MEETING,
	   (SELECT ISNULL(MAX(SEQ), 0) 
	    FROM CZ_SA_MEETING_MEMO_LOG
		WHERE CD_COMPANY = A.CD_COMPANY
		AND NO_MEETING = A.NO_MEETING) + ROW_NUMBER() OVER (PARTITION BY A.CD_COMPANY, A.NO_MEETING ORDER BY A.CD_COMPANY, A.NO_MEETING) AS SEQ,
	   A.CD_PARTNER,
	   A.DT_MEETING,
	   A.DC_TIME,
	   A.DC_LOCATION,
	   A.DC_SUBJECT,
	   A.DC_PURPOSE,
	   A.DC_MEETING,
	   'U' AS TP_LOG,
	   @P_ID_USER AS ID_LOG,
	   NEOE.SF_SYSDATE(GETDATE()) AS DTS_LOG
FROM CZ_SA_MEETING_MEMO A 
JOIN OPENXML (@DOC, '/XML/U', 2) 
        WITH (CD_COMPANY NVARCHAR(7),
			  NO_MEETING NVARCHAR(20)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_MEETING = B.NO_MEETING

EXEC SP_XML_REMOVEDOCUMENT @DOC 




GO

