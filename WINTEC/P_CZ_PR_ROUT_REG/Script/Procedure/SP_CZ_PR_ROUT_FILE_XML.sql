USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_ROUT_FILE_XML]    Script Date: 2021-02-18 오후 1:19:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_ROUT_FILE_XML]
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
FROM CZ_PR_ROUT_FILE A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
		      CD_PLANT		NVARCHAR(7),
		      CD_ITEM		NVARCHAR(20),
		      NO_OPPATH		NVARCHAR(3),
		      CD_OP			NVARCHAR(4),
			  CD_WCOP		NVARCHAR(7),
		      NO_SEQ		NUMERIC(5, 0)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.CD_PLANT = B.CD_PLANT
AND A.CD_ITEM = B.CD_ITEM
AND A.NO_OPPATH = B.NO_OPPATH
AND A.CD_OP = B.CD_OP
AND A.CD_WCOP = B.CD_WCOP
AND A.NO_SEQ = B.NO_SEQ
-- ================================================== INSERT
INSERT INTO CZ_PR_ROUT_FILE 
(
	CD_COMPANY,
	CD_PLANT,
	CD_ITEM,
	NO_OPPATH,
	CD_OP,
	CD_WCOP,
	NO_SEQ,
	NM_FILE,
	DC_FILE,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   CD_PLANT,
	   CD_ITEM,
	   NO_OPPATH,
	   CD_OP,
	   CD_WCOP,
	   NO_SEQ,
	   NM_FILE,
	   DC_FILE,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
FROM OPENXML (@DOC, '/XML/I', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
			  CD_PLANT		NVARCHAR(7),
			  CD_ITEM		NVARCHAR(20),
			  NO_OPPATH		NVARCHAR(3),
			  CD_OP			NVARCHAR(4),
			  CD_WCOP		NVARCHAR(7),
			  NO_SEQ		NUMERIC(5, 0),
			  NM_FILE		NVARCHAR(200),
			  DC_FILE		NVARCHAR(100)) 
-- ================================================== UPDATE    
UPDATE A 
SET A.DC_FILE = B.DC_FILE,
	A.ID_UPDATE = @P_ID_USER, 
	A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_PR_ROUT_FILE A 
JOIN OPENXML (@DOC, '/XML/U', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
			  CD_PLANT		NVARCHAR(7),
			  CD_ITEM		NVARCHAR(20),
			  NO_OPPATH		NVARCHAR(3),
			  CD_OP			NVARCHAR(4),
			  CD_WCOP		NVARCHAR(7),
			  NO_SEQ		NUMERIC(5, 0),
			  NM_FILE		NVARCHAR(200),
			  DC_FILE		NVARCHAR(100)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.CD_PLANT = B.CD_PLANT
AND A.CD_ITEM = B.CD_ITEM
AND A.NO_OPPATH = B.NO_OPPATH
AND A.CD_OP = B.CD_OP
AND A.CD_WCOP = B.CD_WCOP
AND A.NO_SEQ = B.NO_SEQ

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO