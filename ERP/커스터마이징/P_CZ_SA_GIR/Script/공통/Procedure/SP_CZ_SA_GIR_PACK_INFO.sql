SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIR_PACK_INFO]  
(  
    @P_CD_COMPANY   NVARCHAR(7),
    @P_NO_SO        NVARCHAR(20),
	@P_SEQ_SO		NUMERIC(5, 0),
    @P_QT_GIR       NUMERIC(17, 4)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT PH.NO_GIR,
    	   PH.NO_PACK,
    	   (CASE WHEN PL.QT_FILE = 1 THEN PL.NO_FILE ELSE PL.NO_FILE + ' ��' + CONVERT(NVARCHAR, PL.QT_FILE - 1) END) AS NO_FILE,
    	   (CASE PH.CD_TYPE WHEN '002' THEN '(P)' WHEN '003' THEN '(W)' ELSE '' END) + FORMAT(PH.QT_WIDTH, 'g15') + ' X ' + FORMAT(PH.QT_LENGTH, 'g15') + ' X ' + FORMAT(PH.QT_HEIGHT, 'g15') + ' (mm), ' + FORMAT(PH.QT_GROSS_WEIGHT, 'g15') + ' kg' AS DC_PACK,
            PL1.QT_PACK,
            SUM(PL1.QT_PACK) OVER (ORDER BY PH.DTS_INSERT DESC) AS QT_SUM
    FROM CZ_SA_PACKH PH
    JOIN CZ_SA_GIRH_PACK_DETAIL PD ON PD.CD_COMPANY = PH.CD_COMPANY AND PD.NO_GIR = PH.NO_GIR AND PD.CD_PACK_CATEGORY = '001'
    JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK,
    	  	     MAX(DISTINCT CASE WHEN SL.QT_SO > SL.QT_GI THEN PL.NO_FILE ELSE NULL END) AS NO_FILE,
    	  	     COUNT(DISTINCT PL.NO_FILE) AS QT_FILE
    	  FROM CZ_SA_PACKL PL
          JOIN SA_SOL SL ON SL.CD_COMPANY = PL.CD_COMPANY AND SL.NO_SO = PL.NO_FILE AND SL.SEQ_SO = PL.NO_QTLINE
    	  GROUP BY PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK) PL  
    ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR AND PL.NO_PACK = PH.NO_PACK
    JOIN CZ_SA_PACKL PL1 ON PL1.CD_COMPANY = PH.CD_COMPANY AND PL1.NO_GIR = PH.NO_GIR AND PL1.NO_PACK = PH.NO_PACK
    JOIN SA_SOL SL ON SL.CD_COMPANY = PL1.CD_COMPANY AND SL.NO_SO = PL1.NO_FILE AND SL.SEQ_SO = PL1.NO_QTLINE
    WHERE PH.CD_COMPANY = @P_CD_COMPANY
    AND PL1.NO_FILE = @P_NO_SO 
    AND PL1.NO_QTLINE = @P_SEQ_SO
    AND SL.QT_SO > SL.QT_GI
)
SELECT A.NO_GIR,
       A.NO_PACK,
       A.NO_FILE,
       A.DC_PACK
FROM A
WHERE QT_SUM <= @P_QT_GIR

GO
