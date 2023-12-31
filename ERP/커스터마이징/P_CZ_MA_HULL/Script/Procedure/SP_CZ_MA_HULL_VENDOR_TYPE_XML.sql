USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_HULL_VENDOR_TYPE_XML]    Script Date: 2016-05-31 오전 9:18:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [NEOE].[SP_CZ_MA_HULL_VENDOR_TYPE_XML] 
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
FROM CZ_MA_HULL_VENDOR_TYPE A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (NO_IMO		NVARCHAR(10),
			  CD_VENDOR		NVARCHAR(20),
			  NO_TYPE 		INT) B 
ON A.NO_IMO = B.NO_IMO
AND A.CD_VENDOR = B.CD_VENDOR
AND A.NO_TYPE = B.NO_TYPE

DELETE A
FROM MA_FILEINFO A
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (NO_IMO		NVARCHAR(10),
			  CD_VENDOR		NVARCHAR(20),
			  NO_TYPE 		INT) B 
ON A.CD_COMPANY = 'K100' 
AND A.CD_MODULE = 'MA' 
AND A.ID_MENU = 'P_CZ_MA_HULL' 
AND A.CD_FILE = B.NO_IMO + '_' + B.CD_VENDOR + '_' + CONVERT(CHAR, B.NO_TYPE) 

-- ================================================== INSERT
INSERT INTO CZ_MA_HULL_VENDOR_TYPE 
(
	NO_IMO,
	CD_VENDOR,
	NO_TYPE,
	CLS_L,
	CLS_M,
	CLS_S,
	DC_RMK1,
	DC_RMK2,
	DC_RMK3,
	DC_RMK4,
	DC_RMK5,
	DC_RMK6,
	DC_RMK7,
	DC_RMK8,
	DC_RMK9,
	DC_RMK10,
	DC_RMK11,
	DC_RMK12,
	DC_RMK13,
	DC_RMK14,
	DC_RMK15,
	DC_RMK,
	ID_INSERT,
	DTS_INSERT
)
SELECT NO_IMO,
	   CD_VENDOR,
	   NO_TYPE,
	   CLS_L,
	   CLS_M,
	   CLS_S,
	   REPLACE(DC_RMK1, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK2, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK3, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK4, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK5, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK6, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK7, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK8, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK9, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK10, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK11, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK12, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK13, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK14, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK15, CHAR(10), CHAR(13) + CHAR(10)),
	   REPLACE(DC_RMK, CHAR(10), CHAR(13) + CHAR(10)),
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (NO_IMO			NVARCHAR(10),
				CD_VENDOR		NVARCHAR(20),
				NO_TYPE			INT,
				CLS_L			NVARCHAR(4),
				CLS_M			NVARCHAR(4),
				CLS_S			NVARCHAR(4),
				DC_RMK1			NVARCHAR(500),
				DC_RMK2			NVARCHAR(500),
				DC_RMK3			NVARCHAR(500),
				DC_RMK4			NVARCHAR(500),
				DC_RMK5			NVARCHAR(500),
				DC_RMK6			NVARCHAR(500),
				DC_RMK7			NVARCHAR(500),
				DC_RMK8			NVARCHAR(500),
				DC_RMK9			NVARCHAR(500),
				DC_RMK10	    NVARCHAR(500),
				DC_RMK11	    NVARCHAR(500),
				DC_RMK12	    NVARCHAR(500),
				DC_RMK13	    NVARCHAR(500),
				DC_RMK14	    NVARCHAR(500),
				DC_RMK15	    NVARCHAR(500),
				DC_RMK			NVARCHAR(500)) 
-- ================================================== UPDATE    
UPDATE A 
 SET A.CLS_L = B.CLS_L,
	 A.CLS_M = B.CLS_M,
	 A.CLS_S = B.CLS_S,
	 A.DC_RMK1 = REPLACE(B.DC_RMK1, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK2 = REPLACE(B.DC_RMK2, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK3 = REPLACE(B.DC_RMK3, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK4 = REPLACE(B.DC_RMK4, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK5 = REPLACE(B.DC_RMK5, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK6 = REPLACE(B.DC_RMK6, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK7 = REPLACE(B.DC_RMK7, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK8 = REPLACE(B.DC_RMK8, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK9 = REPLACE(B.DC_RMK9, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK10 = REPLACE(B.DC_RMK10, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK11 = REPLACE(B.DC_RMK11, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK12 = REPLACE(B.DC_RMK12, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK13 = REPLACE(B.DC_RMK13, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK14 = REPLACE(B.DC_RMK14, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK15 = REPLACE(B.DC_RMK15, CHAR(10), CHAR(13) + CHAR(10)),
	 A.DC_RMK = REPLACE(B.DC_RMK, CHAR(10), CHAR(13) + CHAR(10)),
	 A.ID_UPDATE = @P_ID_USER,
	 A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_MA_HULL_VENDOR_TYPE A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (NO_IMO			NVARCHAR(10),
			    CD_VENDOR		NVARCHAR(20),
				NO_TYPE			INT,
				CLS_L			NVARCHAR(4),
				CLS_M			NVARCHAR(4),
				CLS_S			NVARCHAR(4),
				DC_RMK1			NVARCHAR(500),
				DC_RMK2			NVARCHAR(500),
				DC_RMK3			NVARCHAR(500),
				DC_RMK4			NVARCHAR(500),
				DC_RMK5			NVARCHAR(500),
				DC_RMK6			NVARCHAR(500),
				DC_RMK7			NVARCHAR(500),
				DC_RMK8			NVARCHAR(500),
				DC_RMK9			NVARCHAR(500),
				DC_RMK10		NVARCHAR(500),
				DC_RMK11		NVARCHAR(500),
				DC_RMK12		NVARCHAR(500),
				DC_RMK13		NVARCHAR(500),
				DC_RMK14		NVARCHAR(500),
				DC_RMK15		NVARCHAR(500),
				DC_RMK			NVARCHAR(500)) B 
  ON A.NO_IMO = B.NO_IMO
  AND A.CD_VENDOR = B.CD_VENDOR
  AND A.NO_TYPE	= B.NO_TYPE

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO

