USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_ENGINE_XML]    Script Date: 2016-05-31 오전 9:18:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [NEOE].[SP_CZ_MA_HULL_ENGINE_XML] 
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
FROM CZ_MA_HULL_ENGINE A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (NO_IMO		NVARCHAR(10),
			  NO_ENGINE 	INT) B 
ON A.NO_IMO = B.NO_IMO
AND A.NO_ENGINE = B.NO_ENGINE
-- ================================================== INSERT
INSERT INTO CZ_MA_HULL_ENGINE 
(
	NO_IMO,
	NO_ENGINE,
	CD_ENGINE,
	NM_MODEL,
	CD_MAKER,
	CAPACITY,
	SERIAL,
	CLS_L,
	CLS_M,
	CLS_S,
	DC_VERSION,
	DC_RMK,
	YN_SYNC,
	ID_INSERT,
	DTS_INSERT
)
SELECT NO_IMO,
	   NO_ENGINE,
	   CD_ENGINE,
	   NM_MODEL,
	   CD_MAKER,
	   CAPACITY,
	   SERIAL,
	   CLS_L,
	   CLS_M,
	   CLS_S,
	   DC_VERSION,
	   REPLACE(DC_RMK, CHAR(10), CHAR(13) + CHAR(10)),
	   YN_SYNC,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (NO_IMO			NVARCHAR(10), 
				NO_ENGINE		INT,
				CD_ENGINE		NVARCHAR(3),
				NM_MODEL		NVARCHAR(100),
				CD_MAKER		NVARCHAR(4),
				CAPACITY		NUMERIC(10, 0),
				SERIAL			NVARCHAR(20),
				CLS_L			NVARCHAR(4),
				CLS_M			NVARCHAR(4),
				CLS_S			NVARCHAR(4),
				DC_VERSION		NVARCHAR(50),
				DC_RMK			NVARCHAR(100),
				YN_SYNC			NVARCHAR(1)) 
-- ================================================== UPDATE    
UPDATE A 
 SET A.CD_ENGINE = B.CD_ENGINE,
	 A.NM_MODEL = B.NM_MODEL,
	 A.CD_MAKER = B.CD_MAKER,
	 A.CAPACITY = B.CAPACITY,
	 A.SERIAL = B.SERIAL,
	 A.CLS_L = B.CLS_L,
	 A.CLS_M = B.CLS_M,
	 A.CLS_S = B.CLS_S,
	 A.DC_VERSION = B.DC_VERSION,
	 A.DC_RMK = REPLACE(B.DC_RMK, CHAR(10), CHAR(13) + CHAR(10)),
	 A.YN_SYNC = B.YN_SYNC,
	 A.ID_UPDATE = @P_ID_USER,
	 A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_MA_HULL_ENGINE A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (NO_IMO			NVARCHAR(10), 
				NO_ENGINE		INT,
				CD_ENGINE		NVARCHAR(3),
				NM_MODEL		NVARCHAR(100),
				CD_MAKER		NVARCHAR(4),
				CAPACITY		NUMERIC(10, 0),
				SERIAL			NVARCHAR(20),
				CLS_L			NVARCHAR(4),
				CLS_M			NVARCHAR(4),
				CLS_S			NVARCHAR(4),
				DC_VERSION		NVARCHAR(50),
				DC_RMK			NVARCHAR(100),
				YN_SYNC			NVARCHAR(1)) B 
  ON A.NO_IMO = B.NO_IMO
  AND A.NO_ENGINE = B.NO_ENGINE

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO

