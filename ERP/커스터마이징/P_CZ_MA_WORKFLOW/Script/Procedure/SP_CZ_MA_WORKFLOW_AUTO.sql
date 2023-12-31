USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_WORKFLOW_AUTO]    Script Date: 2018-07-16 오후 3:33:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MA_WORKFLOW_AUTO]
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_CD_PARTNER			NVARCHAR(20)
)  
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE	@V_3DAYS_BEFORE NVARCHAR(8)

SELECT TOP 3 @V_3DAYS_BEFORE = DT_CAL 
FROM MA_CALENDAR
WHERE CD_COMPANY = @P_CD_COMPANY
AND FG1_HOLIDAY = 'W'
AND DT_CAL < CONVERT(NVARCHAR(8), GETDATE(), 112)
ORDER BY DT_CAL DESC;

WITH A AS
(
	SELECT MU.ID_USER 
	FROM MA_USER MU
	WHERE MU.CD_COMPANY = @P_CD_COMPANY
	AND EXISTS (SELECT 1 
				FROM MA_CODEDTL MC
				WHERE MC.CD_COMPANY = MU.CD_COMPANY
			    AND MC.CD_FIELD = 'CZ_MA00021'
			    AND MC.CD_FLAG1 = @P_CD_PARTNER
				AND MC.CD_FLAG2 LIKE '%' + MU.ID_USER + '%')
),
B AS
(
	SELECT MU.ID_USER,
		   ROW_NUMBER() OVER (ORDER BY WH.QT_WF) AS NO_RANK
	FROM A MU
	LEFT JOIN (SELECT ID_PUR,
					  COUNT(1) AS QT_WF 
			   FROM CZ_MA_WORKFLOWH
			   WHERE CD_COMPANY = @P_CD_COMPANY
			   AND TP_STEP = '21'
			   AND DTS_INSERT >= CONVERT(NVARCHAR(8), GETDATE(), 112) + '000000'
			   GROUP BY ID_PUR) WH 
	ON WH.ID_PUR = MU.ID_USER
),
CZ_SA_QTNH1 AS
(
	SELECT (ROW_NUMBER() OVER (ORDER BY QH.CD_COMPANY) % (SELECT COUNT(1) FROM A) + 1) AS NO_RANK,
		   QH.CD_COMPANY,
		   QH.NO_FILE AS NO_KEY,
		   ISNULL(WH.ID_SALES, QH.NO_EMP) AS ID_SALES,
		   WH.ID_TYPIST,
		   WH1.ID_LOG
	FROM (SELECT QH.CD_COMPANY,
				 QH.NO_FILE,
			     QH.NO_EMP 
		  FROM CZ_SA_QTNH QH
		  WHERE QH.CD_COMPANY = @P_CD_COMPANY
		  AND QH.NO_FILE NOT LIKE 'DY%'
	      AND QH.NO_FILE NOT LIKE 'HN%'
	      AND QH.NO_FILE NOT LIKE 'JK%'
	      AND QH.NO_FILE NOT LIKE 'ZB%'
	      AND (QH.YN_CLOSE = 'N' OR QH.YN_CLOSE IS NULL)
		  AND EXISTS (SELECT * 
				      FROM CZ_PU_QTNH
			          WHERE CD_COMPANY = QH.CD_COMPANY
				      AND NO_FILE = QH.NO_FILE
					  AND DT_INQ <= @V_3DAYS_BEFORE
				      AND CD_PARTNER = @P_CD_PARTNER)
		  AND NOT EXISTS (SELECT * 
					      FROM CZ_MA_WORKFLOWH
					      WHERE CD_COMPANY = QH.CD_COMPANY
					      AND NO_KEY = QH.NO_FILE
					      AND TP_STEP = '05'
					      AND YN_DONE = 'Y')
		  AND NOT EXISTS (SELECT * 
					      FROM CZ_MA_WORKFLOWH
					      WHERE CD_COMPANY = QH.CD_COMPANY
					      AND NO_KEY = QH.NO_FILE
					      AND TP_STEP = '21')) QH
	LEFT JOIN CZ_MA_WORKFLOWH WH ON WH.CD_COMPANY = QH.CD_COMPANY AND WH.NO_KEY = QH.NO_FILE AND WH.TP_STEP = '01'
	LEFT JOIN CZ_MA_WORKFLOWH WH1 ON WH.CD_COMPANY = QH.CD_COMPANY AND WH.NO_KEY = QH.NO_FILE AND WH.TP_STEP = '08'
)
INSERT INTO CZ_MA_WORKFLOWH
(
	CD_COMPANY,
	TP_STEP,
	NO_KEY,
	ID_SALES,
	ID_TYPIST,
	ID_LOG,
	ID_PUR,
	YN_DONE,
	DTS_DONE,
	DC_RMK,
	INSERT_HIST,
	ID_INSERT,
	DTS_INSERT
)
SELECT QH.CD_COMPANY,
	   '21' AS TP_STEP,
	   QH.NO_KEY,
	   QH.ID_SALES,
	   QH.ID_TYPIST,
	   @P_CD_PARTNER AS ID_LOG,
	   MC.ID_USER AS ID_PUR,
	   'N' AS YN_DONE,
	   NULL AS DTS_DONE,
	   '자동등록건 - ' + (SELECT MP.LN_PARTNER 
						  FROM MA_PARTNER MP
						  WHERE MP.CD_COMPANY = @P_CD_COMPANY
						  AND MP.CD_PARTNER = @P_CD_PARTNER) AS DC_RMK,
	   'SP_CZ_MA_WORKFLOW_AUTO' AS INSERT_HIST,
	   'SYSTEM' AS ID_INSERT,
	   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
FROM CZ_SA_QTNH1 QH
JOIN B MC ON MC.NO_RANK = QH.NO_RANK

GO

