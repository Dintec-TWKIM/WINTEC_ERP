USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_HULL_VENDOR_TYPE_EXCEL]    Script Date: 2019-01-16 오전 10:57:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER PROCEDURE [NEOE].[SP_CZ_MA_HULL_VENDOR_TYPE_EXCEL] 
(
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10)
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_DOC INT

EXEC SP_XML_PREPAREDOCUMENT @V_DOC OUTPUT, @P_XML 

SELECT * INTO #XML
FROM OPENXML (@V_DOC, '/XML/ROW', 2) WITH 
(
    NO_IMO			NVARCHAR(10),
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
	DC_RMK			NVARCHAR(500),
	YN_DELETE		NVARCHAR(1)
)

EXEC SP_XML_REMOVEDOCUMENT @V_DOC

-- DELETE
DELETE A
FROM CZ_MA_HULL_VENDOR_TYPE A
JOIN #XML B ON B.NO_IMO = A.NO_IMO AND B.CD_VENDOR = A.CD_VENDOR AND B.NO_TYPE = A.NO_TYPE
WHERE B.YN_DELETE = 'Y'

DELETE A
FROM CZ_MA_HULL_VENDOR_ITEM A
JOIN #XML B ON B.NO_IMO = A.NO_IMO AND B.CD_VENDOR = A.CD_VENDOR AND B.NO_TYPE = A.NO_TYPE
WHERE B.YN_DELETE = 'Y';

-- INSERT
WITH A AS
(
	SELECT A.NO_IMO,
		   A.CD_VENDOR,
	       ISNULL(B.NO_TYPE, 0) + ROW_NUMBER() OVER (PARTITION BY A.NO_IMO, A.CD_VENDOR ORDER BY A.NO_IMO, A.CD_VENDOR) AS NO_TYPE,
	       A.CLS_L,
	       A.CLS_M,
	       A.CLS_S,
		   A.DC_RMK1,
	       A.DC_RMK2,
	       A.DC_RMK3,
	       A.DC_RMK4,
	       A.DC_RMK5,
		   A.DC_RMK6,
		   A.DC_RMK7,
	       A.DC_RMK8,
	       A.DC_RMK9,
	       A.DC_RMK10,
	       A.DC_RMK11,
		   A.DC_RMK12,
		   A.DC_RMK13,
		   A.DC_RMK14,
		   A.DC_RMK15,
	       A.DC_RMK,
		   @P_ID_USER AS ID_INSERT,
	       NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
    FROM #XML A
	LEFT JOIN (SELECT NO_IMO, CD_VENDOR,
					  MAX(NO_TYPE) AS NO_TYPE
			   FROM CZ_MA_HULL_VENDOR_TYPE 
			   GROUP BY NO_IMO, CD_VENDOR) B
	ON B.NO_IMO = A.NO_IMO AND B.CD_VENDOR = A.CD_VENDOR
	WHERE A.NO_TYPE IS NULL
    AND ISNULL(A.YN_DELETE, 'N') = 'N'
)
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
FROM A;

-- UPDATE
MERGE INTO CZ_MA_HULL_VENDOR_TYPE AS T
	  USING (SELECT NO_IMO,
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
			        DC_RMK 
	         FROM #XML
			 WHERE NO_TYPE IS NOT NULL
			 AND ISNULL(YN_DELETE, 'N') = 'N') AS S
	  ON T.NO_IMO = S.NO_IMO
	  AND T.CD_VENDOR = S.CD_VENDOR
	  AND T.NO_TYPE = S.NO_TYPE
	  WHEN MATCHED THEN
		UPDATE SET T.CLS_L = S.CLS_L,
				   T.CLS_M = S.CLS_M,
				   T.CLS_S = S.CLS_S,
				   T.DC_RMK1 = S.DC_RMK1,
				   T.DC_RMK2 = S.DC_RMK2,
				   T.DC_RMK3 = S.DC_RMK3,
				   T.DC_RMK4 = S.DC_RMK4,
				   T.DC_RMK5 = S.DC_RMK5,
				   T.DC_RMK6 = S.DC_RMK6,
				   T.DC_RMK7 = S.DC_RMK7,
				   T.DC_RMK8 = S.DC_RMK8,
				   T.DC_RMK9 = S.DC_RMK9,
				   T.DC_RMK10 = S.DC_RMK10,
				   T.DC_RMK11 = S.DC_RMK11,
				   T.DC_RMK12 = S.DC_RMK12,
				   T.DC_RMK13 = S.DC_RMK13,
				   T.DC_RMK14 = S.DC_RMK14,
				   T.DC_RMK15 = S.DC_RMK15,
				   T.DC_RMK = S.DC_RMK,
				   T.ID_UPDATE = @P_ID_USER,
				   T.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE());

DROP TABLE #XML

GO