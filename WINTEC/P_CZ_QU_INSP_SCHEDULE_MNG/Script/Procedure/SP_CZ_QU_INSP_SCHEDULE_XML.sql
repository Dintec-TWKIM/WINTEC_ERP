USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_QU_INSP_SCHEDULE_XML]    Script Date: 2015-10-26 오후 7:48:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_QU_INSP_SCHEDULE_XML] 
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
FROM CZ_QU_INSP_SCHEDULE A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY NVARCHAR(7),
			  NO_SO		 NVARCHAR(20),
			  TP_COMPANY NVARCHAR(4),
			  DT_INSP	 NVARCHAR(8)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_SO = B.NO_SO
AND A.TP_COMPANY = B.TP_COMPANY
AND A.DT_INSP = B.DT_INSP
-- ================================================== INSERT
INSERT INTO CZ_QU_INSP_SCHEDULE 
(
	CD_COMPANY,
	NO_SO,
	TP_COMPANY,
	DT_INSP,
	TP_INSP,
	TP_CONTENTS,
	TP_RESULT,
	NO_CERT,
	DC_RMK,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   NO_SO,
	   TP_COMPANY,
	   DT_INSP,
	   TP_INSP,
	   TP_CONTENTS,
	   TP_RESULT,
	   NO_CERT,
	   REPLACE(DC_RMK, CHAR(10), CHAR(13) + CHAR(10)),		
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY	NVARCHAR(7),
			    NO_SO		NVARCHAR(20),
			    TP_COMPANY	NVARCHAR(4),
			    DT_INSP		NVARCHAR(8),
				TP_INSP		NVARCHAR(4),
				TP_CONTENTS	NVARCHAR(4),
				TP_RESULT	NVARCHAR(4),
				NO_CERT		NVARCHAR(200),
				DC_RMK	    NVARCHAR(500)) 
-- ================================================== UPDATE    
UPDATE A 
   SET A.TP_INSP = B.TP_INSP,
	   A.TP_CONTENTS = B.TP_CONTENTS,
	   A.TP_RESULT = B.TP_RESULT,
	   A.NO_CERT = B.NO_CERT,
	   A.DC_RMK = REPLACE(B.DC_RMK, CHAR(10), CHAR(13) + CHAR(10)),
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_QU_INSP_SCHEDULE A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY	NVARCHAR(7),
			    NO_SO		NVARCHAR(20),
			    TP_COMPANY	NVARCHAR(4),
			    DT_INSP		NVARCHAR(8),
				TP_INSP		NVARCHAR(4),
				TP_CONTENTS	NVARCHAR(4),
				TP_RESULT	NVARCHAR(4),
				NO_CERT		NVARCHAR(200),
				DC_RMK	    NVARCHAR(500)) B 
  ON A.CD_COMPANY = B.CD_COMPANY
  AND A.NO_SO = B.NO_SO
  AND A.TP_COMPANY = B.TP_COMPANY
  AND A.DT_INSP = B.DT_INSP

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO