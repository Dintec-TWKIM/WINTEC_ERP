USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_IV_AUTO_MNGL4_S]    Script Date: 2016-11-17 오후 5:00:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
    
ALTER PROCEDURE [NEOE].[SP_CZ_PU_IV_AUTO_MNGL4_S]        
(             
	@P_CD_COMPANY           NVARCHAR(7),
	@P_CD_PARTNER			NVARCHAR(20),
	@P_NO_FILE				NVARCHAR(20)
)        
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT PH.CD_PARTNER,
	   @P_NO_FILE AS NO_FILE,
	   PH.NO_PO,
	   PL.NO_IO,
	   PL.AM_PO,
	   PL.AM_IO,
	   PL.AM_IV,
	   (ISNULL(PL.AM_IO, 0) - ISNULL(PL.AM_IV, 0)) AS AM_REMAIN
FROM PU_POH PH
LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_PO, IL.NO_IO,
		          SUM(CASE WHEN ISNULL(IH.YN_RETURN, 'N') = 'Y' THEN -PL.AM ELSE PL.AM END) AS AM_PO,
		          SUM(CASE WHEN IH.YN_RETURN = 'Y' THEN -IL.AM ELSE IL.AM END) AS AM_IO,
		          SUM(CASE WHEN IH.YN_RETURN = 'Y' THEN -IL.AM_CLS ELSE IL.AM_CLS END) AS AM_IV
		   FROM PU_POL PL
		   LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
		   LEFT JOIN MM_QTIOH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO
		   WHERE ISNULL(IL.YN_AM, 'Y') = 'Y'
		   GROUP BY PL.CD_COMPANY, PL.NO_PO, IL.NO_IO) PL
ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
WHERE PH.CD_COMPANY = @P_CD_COMPANY
AND PH.CD_PARTNER = @P_CD_PARTNER
AND (PH.CD_PJT = @P_NO_FILE OR PH.CD_PJT = @P_NO_FILE + '-ST' OR PH.CD_PJT = LEFT(@P_NO_FILE, 10))

GO