USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_AUTO_MAIL_MNG_JSON1]    Script Date: 2017-01-05 ���� 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_AUTO_MAIL_MNG_JSON1]          
(
	@P_JSON			NVARCHAR(MAX),
	@P_ID_USER		NVARCHAR(10)	
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE AM
FROM CZ_SA_AUTO_MAIL_PARTNER AM
WHERE EXISTS (SELECT 1 
			  FROM OPENJSON(@P_JSON)
			  WITH
			  (
					CD_COMPANY      NVARCHAR(7),
					CD_PARTNER		NVARCHAR(20),
					TP_PARTNER		NVARCHAR(3),
					JSON_FLAG		NVARCHAR(1)
			  ) JS
			  WHERE JS.JSON_FLAG = 'D'
			  AND JS.CD_COMPANY = AM.CD_COMPANY
			  AND JS.CD_PARTNER = AM.CD_PARTNER
			  AND JS.TP_PARTNER = AM.TP_PARTNER)

INSERT INTO CZ_SA_AUTO_MAIL_PARTNER
(
	CD_COMPANY,
	CD_PARTNER,
	TP_PARTNER,
	TP_PERIOD,
	YN_MON,
	YN_TUE,
	YN_WED,
	YN_THU,
	YN_FRI,
	YN_READY_INFO,
	YN_ORDER_STAT,
	QT_WEEK,
	TP_DOW_RI,
	YN_MON_WO,
	YN_TUE_WO,
	YN_WED_WO,
	YN_THU_WO,
	YN_FRI_WO,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   CD_PARTNER,
	   TP_PARTNER,
	   TP_PERIOD,
	   YN_MON,
	   YN_TUE,
	   YN_WED,
	   YN_THU,
	   YN_FRI,
	   YN_READY_INFO,
	   YN_ORDER_STAT,
	   QT_WEEK,
	   TP_DOW_RI,
	   YN_MON_WO,
	   YN_TUE_WO,
	   YN_WED_WO,
	   YN_THU_WO,
	   YN_FRI_WO,
	   @P_ID_USER,
	   NEOE.SF_SYSDATE(GETDATE())
FROM OPENJSON(@P_JSON)
WITH
(
	CD_COMPANY		NVARCHAR(7),
	CD_PARTNER		NVARCHAR(20),
	TP_PARTNER		NVARCHAR(3),
	TP_PERIOD		NVARCHAR(3),
	YN_MON			NVARCHAR(1),
	YN_TUE			NVARCHAR(1),
	YN_WED			NVARCHAR(1),
	YN_THU			NVARCHAR(1),
	YN_FRI			NVARCHAR(1),
	YN_READY_INFO	NVARCHAR(1),
	YN_ORDER_STAT	NVARCHAR(1),
	QT_WEEK			INT,
	TP_DOW_RI		NVARCHAR(3),
	YN_MON_WO		NVARCHAR(1),
	YN_TUE_WO		NVARCHAR(1),
	YN_WED_WO		NVARCHAR(1),
	YN_THU_WO		NVARCHAR(1),
	YN_FRI_WO		NVARCHAR(1),
	JSON_FLAG		NVARCHAR(1)
) JS
WHERE JS.JSON_FLAG = 'I'

UPDATE AM
SET AM.TP_PERIOD = JS.TP_PERIOD,
	AM.YN_MON = JS.YN_MON,
	AM.YN_TUE = JS.YN_TUE,
	AM.YN_WED = JS.YN_WED,
	AM.YN_THU = JS.YN_THU,
	AM.YN_FRI = JS.YN_FRI,
	AM.YN_READY_INFO = JS.YN_READY_INFO,
	AM.YN_ORDER_STAT = JS.YN_ORDER_STAT,
	AM.QT_WEEK = JS.QT_WEEK,
	AM.TP_DOW_RI = JS.TP_DOW_RI,
	AM.YN_MON_WO = JS.YN_MON_WO,
	AM.YN_TUE_WO = JS.YN_TUE_WO,
	AM.YN_WED_WO = JS.YN_WED_WO,
	AM.YN_THU_WO = JS.YN_THU_WO,
	AM.YN_FRI_WO = JS.YN_FRI_WO,
	AM.ID_UPDATE = @P_ID_USER,
	AM.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_SA_AUTO_MAIL_PARTNER AM
JOIN OPENJSON(@P_JSON)
WITH
(
	CD_COMPANY		    NVARCHAR(7),
	CD_PARTNER		    NVARCHAR(20),
	TP_PARTNER		    NVARCHAR(3),
	TP_PERIOD		    NVARCHAR(3),
	YN_MON			    NVARCHAR(1),
	YN_TUE			    NVARCHAR(1),
	YN_WED			    NVARCHAR(1),
	YN_THU			    NVARCHAR(1),
	YN_FRI			    NVARCHAR(1),
	YN_READY_INFO		NVARCHAR(1),
	YN_ORDER_STAT		NVARCHAR(1),
	QT_WEEK				INT,
	TP_DOW_RI			NVARCHAR(3),
	YN_MON_WO		    NVARCHAR(1),
	YN_TUE_WO		    NVARCHAR(1),
	YN_WED_WO		    NVARCHAR(1),
	YN_THU_WO		    NVARCHAR(1),
	YN_FRI_WO		    NVARCHAR(1),
	JSON_FLAG			NVARCHAR(1)
) JS
ON JS.CD_COMPANY = AM.CD_COMPANY AND JS.CD_PARTNER = AM.CD_PARTNER AND JS.TP_PARTNER = AM.TP_PARTNER
WHERE JS.JSON_FLAG = 'U'

GO