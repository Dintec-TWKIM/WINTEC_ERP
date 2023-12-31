USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_ROUT_EQUIP_XML]    Script Date: 2023-06-27 오후 4:19:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_ROUT_EQUIP_XML]
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
FROM CZ_PR_ROUT_EQUIP A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
		      CD_PLANT		NVARCHAR(7),
		      CD_ITEM		NVARCHAR(20),
		      NO_OPPATH		NVARCHAR(3),
		      CD_OP			NVARCHAR(4),
			  CD_WCOP		NVARCHAR(7),
		      CD_EQUIP		NVARCHAR(30)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.CD_PLANT = B.CD_PLANT
AND A.CD_ITEM = B.CD_ITEM
AND A.NO_OPPATH = B.NO_OPPATH
AND A.CD_OP = B.CD_OP
AND A.CD_WCOP = B.CD_WCOP
AND A.CD_EQUIP = B.CD_EQUIP
-- ================================================== INSERT
INSERT INTO CZ_PR_ROUT_EQUIP 
(
	CD_COMPANY,
	CD_PLANT,
	CD_ITEM,
	NO_OPPATH,
	CD_OP,
	CD_WC,
	CD_WCOP,
	CD_EQUIP,
	QT_LABOR_PLAN,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   CD_PLANT,
	   CD_ITEM,
	   NO_OPPATH,
	   CD_OP,
	   CD_WC,
	   CD_WCOP,
	   CD_EQUIP,
	   QT_LABOR_PLAN,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
FROM OPENXML (@DOC, '/XML/I', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
			  CD_PLANT		NVARCHAR(7),
			  CD_ITEM		NVARCHAR(20),
			  NO_OPPATH		NVARCHAR(3),
			  CD_OP			NVARCHAR(4),
			  CD_WC			NVARCHAR(7),
			  CD_WCOP		NVARCHAR(7),
			  CD_EQUIP		NVARCHAR(30),
			  QT_LABOR_PLAN	NUMERIC(17,4)) 
-- ================================================== UPDATE    
UPDATE A 
SET A.CD_EQUIP = B.CD_EQUIP,
	A.QT_LABOR_PLAN = B.QT_LABOR_PLAN,
	A.ID_UPDATE = @P_ID_USER, 
	A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_PR_ROUT_EQUIP A 
JOIN OPENXML (@DOC, '/XML/U', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
			  CD_PLANT		NVARCHAR(7),
			  CD_ITEM		NVARCHAR(20),
			  NO_OPPATH		NVARCHAR(3),
			  CD_OP			NVARCHAR(4),
			  CD_WCOP		NVARCHAR(7),
			  CD_EQUIP		NVARCHAR(30),
			  QT_LABOR_PLAN	NUMERIC(17,4)) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.CD_PLANT = B.CD_PLANT
AND A.CD_ITEM = B.CD_ITEM
AND A.NO_OPPATH = B.NO_OPPATH
AND A.CD_OP = B.CD_OP
AND A.CD_WCOP = B.CD_WCOP
AND A.CD_EQUIP = B.CD_EQUIP

EXEC SP_XML_REMOVEDOCUMENT @DOC 

