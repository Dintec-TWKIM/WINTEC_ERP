USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_WO_SA_SOL_CHANGE_S3]    Script Date: 2021-03-02 오전 10:51:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PR_WO_SA_SOL_CHANGE_S3]
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_NO_SO			NVARCHAR(20),
	@P_SEQ_SO			NUMERIC(5, 0),
    @P_NO_WO			NVARCHAR(20),
	@P_YN_EXPECT_GIR	NVARCHAR(1),
	@P_YN_EXPECT_GI		NVARCHAR(1),
	@P_YN_EXPECT_CLOSE  NVARCHAR(1)
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT WM.CD_COMPANY,
	   WM.CD_PLANT,
	   @P_NO_SO AS NO_SO_S,
	   @P_SEQ_SO AS SEQ_SO,
	   WM.NO_WO,
	   WM.NO_SO,
	   WM.NO_LINE,
	   WM.CD_ITEM,
	   WM.QT_APPLY,
	   SL.DT_DUEDATE,
	   SL.QT_GIR,
	   SL.QT_GI,
	   (CASE WHEN ISNULL(WI.NO_SO, '') = '' THEN 'N' ELSE 'Y' END) AS YN_USE
FROM CZ_PR_SA_SOL_PR_WO_MAPPING WM
LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = WM.CD_COMPANY AND SL.NO_SO = WM.NO_SO AND SL.SEQ_SO = WM.NO_LINE
LEFT JOIN (SELECT WI.CD_COMPANY, WI.NO_WO, WI.NO_SO
		   FROM CZ_PR_WO_INSP WI 
		   WHERE WI.NO_INSP IN (996, 997)
		   GROUP BY WI.CD_COMPANY, WI.NO_WO, WI.NO_SO) WI
ON WI.CD_COMPANY = WM.CD_COMPANY AND WI.NO_WO = WM.NO_WO AND WI.NO_SO = WM.NO_SO
WHERE WM.CD_COMPANY = @P_CD_COMPANY
AND WM.NO_WO = @P_NO_WO
AND (ISNULL(@P_YN_EXPECT_GIR, 'N') = 'N' OR ISNULL(SL.QT_SO, 0) > ISNULL(SL.QT_GIR, 0))
AND (ISNULL(@P_YN_EXPECT_GI, 'N') = 'N' OR ISNULL(SL.QT_SO, 0) > ISNULL(SL.QT_GI, 0))
AND (ISNULL(@P_YN_EXPECT_CLOSE, 'N') = 'N' OR SL.STA_SO <> 'C')
ORDER BY SL.DT_DUEDATE DESC

GO