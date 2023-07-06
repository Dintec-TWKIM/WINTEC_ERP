USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WO_MNG_AUTO_INSP]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_MNG_AUTO_INSP]          
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_NO_WO		NVARCHAR(20),
	@P_NO_LINE		NUMERIC(5, 0),
	@P_SEQ_WO_FROM	NUMERIC(5, 0),
	@P_SEQ_WO_TO	NUMERIC(5, 0)
)  
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

INSERT INTO CZ_PR_WO_INSP
(
	CD_COMPANY,
	NO_WO,
	NO_LINE,
	SEQ_WO,
	NO_INSP,
	DT_INSP,
	NO_EMP,
	DTS_INSERT,
	ID_INSERT
)
SELECT WO.CD_COMPANY,
       WO.NO_WO,
	   WR.NO_LINE,
	   RD.SEQ_WO,
	   999 AS NO_INSP,
	   CONVERT(CHAR(8), GETDATE(), 112) AS DT_INSP,
	   'SYSTEM' AS NO_EMP,
	   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT,
	   'SYSTEM' AS ID_INSERT
FROM PR_WO WO
JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
JOIN CZ_PR_WO_REQ_D RD ON RD.CD_COMPANY = WO.CD_COMPANY AND RD.NO_WO = WO.NO_WO
WHERE WO.CD_COMPANY = @P_CD_COMPANY
AND WO.NO_WO = @P_NO_WO
AND WR.NO_LINE = @P_NO_LINE
AND RD.SEQ_WO >= @P_SEQ_WO_FROM 
AND RD.SEQ_WO <= @P_SEQ_WO_TO
AND NOT EXISTS (SELECT 1 
				FROM CZ_PR_WO_INSP WI
			    WHERE WI.CD_COMPANY = WO.CD_COMPANY
				AND WI.NO_WO = WO.NO_WO
				AND WI.NO_LINE = WR.NO_LINE
				AND WI.SEQ_WO = RD.SEQ_WO)
AND NOT EXISTS (SELECT 1 
				FROM CZ_PR_WO_INSP WI
			    WHERE WI.CD_COMPANY = WO.CD_COMPANY
				AND WI.NO_WO = WO.NO_WO
				AND WI.NO_LINE < WR.NO_LINE
				AND WI.SEQ_WO = RD.SEQ_WO
				AND WI.NO_INSP = -1)

GO