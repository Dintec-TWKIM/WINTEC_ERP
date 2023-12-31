USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_OPOUT_WORK_ID_S]    Script Date: 2023-02-09 오전 9:44:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_OPOUT_WORK_ID_S]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_WO			NVARCHAR(20),
	@P_NO_LINE			NUMERIC(5, 0),
	@P_NO_PO			NVARCHAR(20),
	@P_NO_PR			NVARCHAR(20)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   WD.NO_WO,
	   WR.NO_LINE,
	   WD.SEQ_WO,
	   WD.NO_ID,
	   'N' AS YN_GOOD,
	   'N' AS YN_BAD,
	   WI.CD_REJECT,
	   WI.CD_RESOURCE
FROM CZ_PR_WO_REQ_D WD
LEFT JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WD.CD_COMPANY AND WR.NO_WO = WD.NO_WO
LEFT JOIN CZ_PR_WO_INSP WI ON WI.CD_COMPANY = WD.CD_COMPANY AND WI.NO_WO = WD.NO_WO AND WI.SEQ_WO = WD.SEQ_WO
WHERE WD.CD_COMPANY = @P_CD_COMPANY
AND WD.NO_WO = @P_NO_WO
AND WR.NO_LINE = @P_NO_LINE 
AND NOT EXISTS (SELECT 1 
				FROM CZ_PR_WO_INSP WI
				WHERE WI.CD_COMPANY = WD.CD_COMPANY
				AND WI.NO_WO = WD.NO_WO
				AND WI.SEQ_WO = WD.SEQ_WO
				AND WI.NO_INSP = -1)
AND EXISTS (SELECT 1 
	    	FROM CZ_PR_WO_INSP WI
			WHERE WI.CD_COMPANY = WD.CD_COMPANY
			AND WI.NO_WO = WD.NO_WO
			AND WI.NO_LINE = WR.NO_LINE
			AND WI.SEQ_WO = WD.SEQ_WO
			AND WI.NO_INSP = 994
			AND WI.NO_OPOUT_PO = @P_NO_PO
			AND WI.NO_OPOUT_PR = @P_NO_PR)
AND (ISNULL(WR.CD_OP_BEFORE, '') = '' OR EXISTS (SELECT 1 
											     FROM CZ_PR_WO_INSP WI
											     JOIN PR_WO_ROUT WR1 ON WR1.CD_COMPANY = WI.CD_COMPANY AND WR1.NO_WO = WI.NO_WO AND WR1.NO_LINE = WI.NO_LINE
											     WHERE WI.CD_COMPANY = WD.CD_COMPANY
											     AND WI.NO_WO = WD.NO_WO
											     AND WR1.CD_OP = WR.CD_OP_BEFORE
											     AND WR1.CD_WC = WR.CD_WC_BEFORE
											     AND WR1.CD_WCOP = WR.CD_WCOP_BEFORE
											     AND WI.SEQ_WO = WD.SEQ_WO))
ORDER BY WD.SEQ_WO

