USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_LOG_PLAN_MNG_DELIVERY_XML]    Script Date: 2016-02-12 오후 1:40:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_LOG_PLAN_MNG_DELIVERY_XML]  
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
FROM CZ_SA_LOG_PLAN_DELIVERY A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
              NO_REV        INT,
			  NO_IDX 		INT) B 
ON A.CD_COMPANY = B.CD_COMPANY 
AND A.NO_REV = B.NO_REV
AND A.NO_IDX = B.NO_IDX
-- ================================================== INSERT
INSERT INTO CZ_SA_LOG_PLAN_DELIVERY 
(
    CD_COMPANY,
    NO_REV,
    NO_IDX,
    NO_GIR,
    DC_LOCATION,
    DC_TASK,
    CD_PARTNER,
    LN_PARTNER,
    NO_EMP,
    QT_ITEM,
    NO_IMO,
    DT_COMPLETE,
    NO_SO_PRE,
    DC_UPDATE,
    DC_LIMIT,
    DC_ETC,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
       NO_REV,
       NO_IDX,
       NO_GIR,
       DC_LOCATION,
       DC_TASK,
       CD_PARTNER,
       LN_PARTNER,
       NO_EMP,
       QT_ITEM,
       NO_IMO,
       DT_COMPLETE,
       NO_SO_PRE,
       REPLACE(DC_UPDATE, CHAR(10), CHAR(13) + CHAR(10)) AS DC_UPDATE,
       DC_LIMIT,
       REPLACE(DC_ETC, CHAR(10), CHAR(13) + CHAR(10)) AS DC_ETC,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY      NVARCHAR(7),
                NO_REV          INT,
                NO_IDX          INT,
                NO_GIR          NVARCHAR(20),
                DC_LOCATION     NVARCHAR(20),
                DC_TASK         NVARCHAR(20),
                CD_PARTNER      NVARCHAR(20),
                LN_PARTNER      NVARCHAR(50),
                NO_EMP          NVARCHAR(10),
                QT_ITEM         INT,
                NO_IMO          NVARCHAR(10),
                DT_COMPLETE     NVARCHAR(8),
                NO_SO_PRE       NVARCHAR(50),
                DC_UPDATE       NVARCHAR(500),
                DC_LIMIT        NVARCHAR(50),
                DC_PACK         NVARCHAR(100),
                DC_ETC          NVARCHAR(MAX)) 
-- ================================================== UPDATE    
UPDATE A 
   SET A.DC_LOCATION = B.DC_LOCATION,
	   A.DC_TASK = B.DC_TASK,
	   A.CD_PARTNER = B.CD_PARTNER,
       A.LN_PARTNER = B.LN_PARTNER,
	   A.NO_EMP = B.NO_EMP,
	   A.QT_ITEM = B.QT_ITEM,
	   A.NO_IMO = B.NO_IMO,
	   A.DT_COMPLETE = B.DT_COMPLETE,
	   A.NO_SO_PRE = B.NO_SO_PRE,
	   A.DC_UPDATE = B.DC_UPDATE,
	   A.DC_LIMIT = B.DC_LIMIT,
	   A.DC_ETC = B.DC_ETC,
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_SA_LOG_PLAN_DELIVERY A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY      NVARCHAR(7),
                NO_REV          INT,
                NO_IDX          INT,
                NO_GIR          NVARCHAR(20),
                DC_LOCATION     NVARCHAR(20),
                DC_TASK         NVARCHAR(20),
                CD_PARTNER      NVARCHAR(20),
                LN_PARTNER      NVARCHAR(50),
                NO_EMP          NVARCHAR(10),
                QT_ITEM         INT,
                NO_IMO          NVARCHAR(10),
                DT_COMPLETE     NVARCHAR(8),
                NO_SO_PRE       NVARCHAR(50),
                DC_UPDATE       NVARCHAR(500),
                DC_LIMIT        NVARCHAR(50),
                DC_ETC          NVARCHAR(MAX)) B 
  ON A.CD_COMPANY = B.CD_COMPANY 
  AND A.NO_REV = B.NO_REV
  AND A.NO_IDX = B.NO_IDX

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO