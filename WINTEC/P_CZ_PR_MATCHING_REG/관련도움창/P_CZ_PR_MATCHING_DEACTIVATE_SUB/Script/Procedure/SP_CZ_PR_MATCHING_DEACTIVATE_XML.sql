USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MATCHING_DEACTIVATE_XML]    Script Date: 2016-11-30 오후 5:22:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_PR_MATCHING_DEACTIVATE_XML] 
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
FROM CZ_PR_MATCHING_DEACTIVATE A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY		NVARCHAR(7),
			  CD_PLANT			NVARCHAR(7),
			  NO_ID				NVARCHAR(20)) B 
 ON A.CD_COMPANY = B.CD_COMPANY
AND A.CD_PLANT = B.CD_PLANT
AND A.NO_ID = B.NO_ID

-- ================================================== UPDATE    
UPDATE A 
   SET A.STA_DEACTIVATE = B.STA_DEACTIVATE,
	   A.NUM_P1_OUT = B.NUM_P1_OUT,
	   A.NUM_P2_OUT = B.NUM_P2_OUT,
	   A.NUM_P3_OUT = B.NUM_P3_OUT,
	   A.NUM_P1_IN = B.NUM_P1_IN,
	   A.NUM_P2_IN = B.NUM_P2_IN,
	   A.NUM_P3_IN = B.NUM_P3_IN,
	   A.DC_RMK = B.DC_RMK,
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_PR_MATCHING_DEACTIVATE A 
JOIN OPENXML (@DOC, '/XML/U', 2) 
        WITH (CD_COMPANY		NVARCHAR(7),
			  CD_PLANT			NVARCHAR(7),
			  NO_ID				NVARCHAR(20),
			  STA_DEACTIVATE	NVARCHAR(4),
			  NUM_P1_OUT		NUMERIC(7, 4),
			  NUM_P2_OUT		NUMERIC(7, 4),
			  NUM_P3_OUT		NUMERIC(7, 4),
			  NUM_P1_IN			NUMERIC(7, 4),
			  NUM_P2_IN			NUMERIC(7, 4),
			  NUM_P3_IN			NUMERIC(7, 4),
		      DC_RMK			NVARCHAR(500)) B 
 ON A.CD_COMPANY = B.CD_COMPANY
AND A.CD_PLANT = B.CD_PLANT
AND A.NO_ID = B.NO_ID

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO

