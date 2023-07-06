USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_IMP_PMT_MNGL_XML]    Script Date: 2015-10-26 오후 8:12:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_FI_IMP_PMT_MNGL_XML] 
(
    @P_CD_COMPANY	NVARCHAR(7),
	@P_NO_IMPORT	NVARCHAR(14),
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10), 
    @DOC			INT = NULL
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML 

-- ================================================== DELETE
DELETE A 
  FROM CZ_FI_IMP_PMTL A 
       JOIN OPENXML (@DOC, '/XML/D', 2) 
               WITH (NO_IMPORT		NVARCHAR(14),
					 NO_PO			NVARCHAR(20),
					 NO_POLINE		NUMERIC(5, 0)) B 
       ON A.CD_COMPANY = @P_CD_COMPANY
	   AND A.NO_IMPORT = B.NO_IMPORT
	   AND A.NO_PO = B.NO_PO
	   AND A.NO_POLINE = B.NO_POLINE
-- ================================================== INSERT
INSERT INTO CZ_FI_IMP_PMTL
(
	CD_COMPANY,
	NO_IMPORT,
	NO_PO,
	NO_POLINE,
	NO_SO,
	NO_SOLINE,
	CD_PLANT,
	CD_ITEM,
	QT_PO,
	QT_TAX,
	UM_EX,
	AM_EX,
	AM,
	NO_RAN,
	AM_TAX,
	ID_INSERT,
	DTS_INSERT
)
SELECT @P_CD_COMPANY,
	   @P_NO_IMPORT,
	   NO_PO,
	   NO_POLINE,
	   NO_SO,
	   NO_SOLINE,
	   CD_PLANT,
	   CD_ITEM,
	   QT_PO_MM,
	   QT_PO_MM,
	   UM_EX,
	   AM_EX,
	   AM,
	   NO_RAN,
	   AM_TAX,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (NO_PO			NVARCHAR(20),
				NO_POLINE		NUMERIC(5, 0),
				NO_SO			NVARCHAR(20),
				NO_SOLINE		NUMERIC(5, 0),
				CD_PLANT		NVARCHAR(7),
				CD_ITEM			NVARCHAR(20),
				QT_PO_MM		NUMERIC(17, 4),
				UM_EX			NUMERIC(15, 4),
				AM_EX			NUMERIC(17, 4),
				AM				NUMERIC(17, 4),
				NO_RAN			NVARCHAR(20),
				AM_TAX			NUMERIC(17, 4))
-- ================================================== UPDATE    
UPDATE A 
   SET A.NO_RAN = B.NO_RAN,
	   A.AM_TAX = B.AM_TAX,
	   A.QT_RETURN = B.QT_RETURN,
	   A.AM_RETURN = B.AM_RETURN,
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_FI_IMP_PMTL A 
       JOIN OPENXML (@DOC, '/XML/U', 2) 
               WITH (NO_IMPORT		NVARCHAR(14),
					 NO_PO			NVARCHAR(20),
					 NO_POLINE		NUMERIC(5, 0),
					 NO_RAN			NVARCHAR(20),
					 AM_TAX			NUMERIC(17, 4),
					 QT_RETURN		NUMERIC(17, 4),
					 AM_RETURN		NUMERIC(17, 4)) B 
       ON A.CD_COMPANY = @P_CD_COMPANY
	   AND A.NO_IMPORT = B.NO_IMPORT
	   AND A.NO_PO = B.NO_PO
	   AND A.NO_POLINE = B.NO_POLINE

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO

