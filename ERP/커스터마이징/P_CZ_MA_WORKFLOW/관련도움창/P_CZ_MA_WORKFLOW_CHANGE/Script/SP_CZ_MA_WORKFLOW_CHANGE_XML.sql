USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_CHANGE_XML]    Script Date: 2016-10-28 오후 7:27:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_CHANGE_XML] 
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
FROM CZ_MA_WORKFLOWH A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY   NVARCHAR(7),
			  NO_KEY	   NVARCHAR(20),
			  TP_STEP	   NVARCHAR(3)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_KEY = B.NO_KEY
AND A.TP_STEP = B.TP_STEP

DELETE A 
FROM CZ_MA_WORKFLOWL A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY   NVARCHAR(7),
			  NO_KEY	   NVARCHAR(20),
			  TP_STEP	   NVARCHAR(3)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.NO_KEY = B.NO_KEY
AND A.TP_STEP = B.TP_STEP
-- ================================================== UPDATE    
UPDATE A 
   SET A.ID_SALES = B.ID_SALES,
	   A.ID_TYPIST = B.ID_TYPIST,
	   A.ID_PUR = B.ID_PUR,
	   A.ID_LOG = B.ID_LOG, 
	   A.YN_DONE = B.YN_DONE,
	   A.DTS_DONE = (CASE WHEN A.YN_DONE = 'N' AND B.YN_DONE = 'Y' THEN NEOE.SF_SYSDATE(GETDATE()) ELSE NULL END),
	   A.UPDATE_HIST = 'SP_CZ_MA_WORKFLOW_CHANGE',
	   A.ID_UPDATE = @P_ID_USER,
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_MA_WORKFLOWH A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY NVARCHAR(7),
			    NO_KEY	   NVARCHAR(20),
			    TP_STEP	   NVARCHAR(3),
			    ID_SALES   NVARCHAR(15),
			    ID_TYPIST  NVARCHAR(15),
			    ID_PUR     NVARCHAR(15),
			    ID_LOG	   NVARCHAR(15),
			    YN_DONE	   NVARCHAR(1)) B 
  ON A.CD_COMPANY = B.CD_COMPANY
  AND A.NO_KEY = B.NO_KEY
  AND A.TP_STEP = B.TP_STEP 

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO

