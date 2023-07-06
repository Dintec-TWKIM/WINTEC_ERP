USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_AUTO_MAIL_READY_INFO]    Script Date: 2016-01-13 오후 9:02:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
 
ALTER PROCEDURE [NEOE].[SP_CZ_SA_AUTO_MAIL_READY_INFO]    
(            
	@P_CD_COMPANY		NVARCHAR(7),
    @P_CD_PARTNER       NVARCHAR(20),
    @P_DT_FROM          NVARCHAR(8),
    @P_DT_TO            NVARCHAR(8),
    @P_NO_SO            NVARCHAR(20),
    @P_YN_SEND_TARGET   NVARCHAR(1),
    @P_CD_PARTNER_GRP	NVARCHAR(4)
)            
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT SL.CD_COMPANY, 
           SL.NO_SO,
           SL.SEQ_SO,
           SL.CD_ITEM,
           SL.QT_SO,
           SL.QT_GIR,
           SB.QT_BOOK,
           (CASE WHEN LEFT(ISNULL(SB.DTS_UPDATE, SB.DTS_INSERT), 8) > ISNULL(PL.DT_IO, '') THEN LEFT(ISNULL(SB.DTS_UPDATE, SB.DTS_INSERT), 8)
                                                                                           ELSE ISNULL(PL.DT_IO, '') END) AS DT_IN, 
           (CASE WHEN QL.TP_BOM = 'P' THEN ISNULL(BM.QT_BOM, 0) 
	 		                          ELSE (ISNULL(PL.QT_IO, 0) + ISNULL(SB.QT_BOOK, 0)) END) AS QT_IN 
    FROM SA_SOL SL
    JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
    LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
	LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
	                  SUM(IL.QT_IO) AS QT_IO,
                      MAX(IL.DT_IO) AS DT_IO
	           FROM PU_POL PL
	           LEFT JOIN PU_RCVL RL ON RL.CD_COMPANY = PL.CD_COMPANY AND RL.NO_PO = PL.NO_PO AND RL.NO_POLINE = PL.NO_LINE
	           LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = RL.CD_COMPANY AND IL.NO_ISURCV = RL.NO_RCV AND IL.NO_ISURCVLINE = RL.NO_LINE
	           GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
	ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
	LEFT JOIN (SELECT OH.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
	                  SUM(OL.QT_IO) AS QT_BOM
	           FROM MM_QTIOH OH
	           LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
	           WHERE OH.YN_RETURN <> 'Y' 
	           AND OL.FG_PS = '1' 
	           AND OL.FG_IO = '002'
	           GROUP BY OH.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) BM
	ON BM.CD_COMPANY = SL.CD_COMPANY AND BM.NO_PSO_MGMT = SL.NO_SO AND BM.NO_PSOLINE_MGMT = SL.SEQ_SO
    WHERE SL.CD_COMPANY = @P_CD_COMPANY
    AND LEFT(SL.NO_SO, 2) NOT IN ('SB', 'HB')
    AND SL.CD_ITEM NOT LIKE 'SD%'
    AND (ISNULL(@P_NO_SO, '') = '' OR SL.NO_SO = @P_NO_SO)
),
B AS
(
    SELECT SL.CD_COMPANY, SL.NO_SO,
           SUM(SL.QT_SO) AS QT_SO,
           SUM(SL.QT_GIR) AS QT_GIR,
           SUM(SL.QT_IN) AS QT_IN,
           SUM(ISNULL(SL.QT_BOOK, 0) * ISNULL(MI.WEIGHT, 0)) AS WEIGHT_STOCK,
           SUM(CASE WHEN ISNULL(SL.QT_BOOK, 0) > 0 AND ISNULL(MI.WEIGHT, 0) = 0 THEN 1 ELSE 0 END) AS QT_STOCK,
           MAX(SL.DT_IN) AS DT_IN
    FROM A SL
    LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_ITEM = SL.CD_ITEM
    GROUP BY SL.CD_COMPANY, SL.NO_SO
),
C AS
(
    SELECT B.CD_COMPANY,
           B.NO_SO,
           B.QT_SO,
           B.QT_GIR,
           B.QT_IN,
           B.DT_IN,
           SL.CNT_SEND,
           MF.YN_INCLUDE,
           MF.YN_EXCLUDE,
           (CASE WHEN ((ISNULL(IL.QT_PO, 0) = 0 AND ISNULL(B.QT_STOCK, 0) = 0) OR 
                       (ISNULL(B.WEIGHT_STOCK, 0) + ISNULL(IL.WEIGHT_PO, 0)) >= 20) THEN ISNULL(B.WEIGHT_STOCK, 0) + ISNULL(IL.WEIGHT_PO, 0) ELSE 0 END) AS WEIGHT,
           (CASE WHEN ((ISNULL(PK.QT_VW, 0) > 0 AND ISNULL(B.QT_STOCK, 0) = 0) OR 
                       (ISNULL(PK.QT_VW, 0) + ISNULL(B.WEIGHT_STOCK, 0)) >= 20) THEN ISNULL(PK.QT_VW, 0) + ISNULL(B.WEIGHT_STOCK, 0) ELSE 0 END) AS QT_VW
    FROM B
    LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = B.CD_COMPANY AND SH.NO_SO = B.NO_SO
    LEFT JOIN CZ_SA_AUTO_MAIL_PARTNER AM ON AM.CD_COMPANY = SH.CD_COMPANY AND AM.CD_PARTNER = SH.CD_PARTNER AND AM.TP_PARTNER = '002'
    LEFT JOIN CZ_SA_AUTO_MAIL_FILE MF ON MF.CD_COMPANY = SH.CD_COMPANY AND MF.NO_SO = SH.NO_SO
    LEFT JOIN (SELECT CD_COMPANY, NO_SO, 
                      COUNT(1) AS CNT_SEND,
                      MAX(DT_SEND) AS DT_SEND
    		   FROM CZ_SA_AUTO_MAIL_SA_READY_LOG
    		   GROUP BY CD_COMPANY, NO_SO) SL 
    ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
    LEFT JOIN (SELECT IH.CD_COMPANY, IL.CD_PJT,
                      SUM(CASE WHEN IH.WEIGHT IS NULL THEN 1 ELSE 0 END) AS QT_PO,
                      SUM(IH.WEIGHT) AS WEIGHT_PO
               FROM MM_QTIOH IH
               JOIN (SELECT IL.CD_COMPANY, IL.NO_IO,
                            MAX(IL.CD_PJT) AS CD_PJT 
                     FROM MM_QTIO IL
                     WHERE IL.CD_QTIOTP IN ('100', '110', '120', '121')
                     GROUP BY IL.CD_COMPANY, IL.NO_IO) IL 
               ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IO = IH.NO_IO
               GROUP BY IH.CD_COMPANY, IL.CD_PJT) IL
    ON IL.CD_COMPANY = B.CD_COMPANY AND IL.CD_PJT = B.NO_SO
    LEFT JOIN (SELECT PH.CD_COMPANY, PH.CD_PJT,
                      CONVERT(INT, ROUND(SUM(PK.QT_VW), 0)) AS QT_VW
               FROM PU_POH PH
               LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_PO,
                                 SUM(PL.QT_PO) AS QT_PO,
                                 SUM(KL.QT_PACK) AS QT_PACK
                          FROM PU_POL PL
                          LEFT JOIN (SELECT CD_COMPANY, NO_PO, NO_LINE,
                                            SUM(QT_PACK) AS QT_PACK
                                     FROM CZ_PU_PACKL
                                     GROUP BY CD_COMPANY, NO_PO, NO_LINE) KL 
                          ON KL.CD_COMPANY = PL.CD_COMPANY AND KL.NO_PO = PL.NO_PO AND KL.NO_LINE = PL.NO_LINE
                          WHERE PL.CD_ITEM NOT LIKE 'SD%'
                          GROUP BY PL.CD_COMPANY, PL.NO_PO) PL
               ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
               LEFT JOIN (SELECT PK.CD_COMPANY, PK.NO_PO,
                                 SUM((PK.SIZE_X * PK.SIZE_Y * PK.SIZE_Z) / 5000000) AS QT_VW,
                                 SUM(CASE WHEN ISNULL(PK.SIZE_X, 0) = 0 OR ISNULL(PK.SIZE_Y, 0) = 0 OR ISNULL(PK.SIZE_Z, 0) = 0 THEN 1 ELSE 0 END) AS QT_SIZE 
                          FROM CZ_PU_PACKH PK
                          GROUP BY PK.CD_COMPANY, PK.NO_PO) PK 
               ON PK.CD_COMPANY = PH.CD_COMPANY AND PK.NO_PO = PH.NO_PO
               GROUP BY PH.CD_COMPANY, PH.CD_PJT
               HAVING SUM(PL.QT_PO) = SUM(PL.QT_PACK)
               AND SUM(PK.QT_SIZE) = 0) PK
    ON PK.CD_COMPANY = B.CD_COMPANY AND PK.CD_PJT = B.NO_SO
    WHERE B.DT_IN BETWEEN @P_DT_FROM AND @P_DT_TO
    AND B.QT_SO > B.QT_GIR
    AND B.QT_SO = B.QT_IN
    AND ISNULL(SH.YN_CLOSE, 'N') <> 'Y'
    AND (ISNULL(@P_CD_PARTNER, '') = '' OR SH.CD_PARTNER = @P_CD_PARTNER)
    AND (ISNULL(@P_YN_SEND_TARGET, 'N') = 'N' OR (ISNULL(AM.YN_READY_INFO, 'N') = 'Y' AND
                                                  ISNULL(MF.YN_EXCLUDE, 'N') = 'N' AND
                                                  ((ISNULL(SL.DT_SEND, '') = '' AND 
                                                    B.DT_IN BETWEEN CONVERT(CHAR(8), DATEADD(YEAR, -1, GETDATE()), 112) AND CONVERT(CHAR(8), GETDATE(), 112)) OR
                                                   (ISNULL(SL.DT_SEND, '') <> '' AND
                                                    ISNULL(AM.QT_WEEK, 0) > 0 AND
                                                    ISNULL(AM.TP_DOW_RI, '') <> '' AND
                                                    DATEADD(WEEK, ISNULL(AM.QT_WEEK, 0), SL.DT_SEND) <= GETDATE() AND 
                                                    ((ISNULL(AM.TP_DOW_RI, '') = 'MON' AND DATEPART(DW, GETDATE()) = 2) OR 
	                                                 (ISNULL(AM.TP_DOW_RI, '') = 'TUE' AND DATEPART(DW, GETDATE()) = 3) OR 
	                                                 (ISNULL(AM.TP_DOW_RI, '') = 'WED' AND DATEPART(DW, GETDATE()) = 4) OR 
	                                                 (ISNULL(AM.TP_DOW_RI, '') = 'THU' AND DATEPART(DW, GETDATE()) = 5) OR 
	                                                 (ISNULL(AM.TP_DOW_RI, '') = 'FRI' AND DATEPART(DW, GETDATE()) = 6))))))
),
D AS
(
    SELECT C.CD_COMPANY,
           C.NO_SO,
           C.DT_IN,
           C.YN_EXCLUDE,
           C.YN_INCLUDE,
           (CASE WHEN C.WEIGHT >= C.QT_VW THEN C.WEIGHT ELSE C.QT_VW END) AS WEIGHT,
           (CASE WHEN ISNULL(C.CNT_SEND, 1) > 1 THEN CONVERT(VARCHAR, ISNULL(C.CNT_SEND, 1)) + 
                                                     (CASE WHEN RIGHT(ISNULL(C.CNT_SEND, 1), 1) = 1 THEN 'st Notice'
													       WHEN RIGHT(ISNULL(C.CNT_SEND, 1), 1) = 2 THEN 'nd Notice'
													       WHEN RIGHT(ISNULL(C.CNT_SEND, 1), 1) = 3 THEN 'rd Notice' ELSE 'th Notice' END)
		                                        ELSE '' END) AS DC_RMK
    FROM C
)
SELECT SH.CD_PARTNER,
       MP.LN_PARTNER,
       SH.NO_IMO,
       MH.NM_VESSEL,
       SH.NO_EMP,
       SH.CD_PARTNER_GRP,
       SH.NO_PO_PARTNER,
       SH.DT_SO,
       SH.NO_SO,
       D.DT_IN,
       D.YN_EXCLUDE,
       D.YN_INCLUDE,
       D.DC_RMK,
       MV.TO_EMAIL,
       (CASE WHEN D.WEIGHT >= 20 THEN 'Heavy'
             WHEN D.WEIGHT <= 0 THEN '-'
             WHEN D.WEIGHT < 20 THEN CONVERT(NVARCHAR, CONVERT(NUMERIC(10, 2), ROUND(D.WEIGHT, 2))) + ' KG' 
                                ELSE NULL END) AS WEIGHT,
       VE.CD_FLAG1,
       VE.CD_FLAG2
FROM D
LEFT JOIN V_CZ_SA_QTN_LOG_EMP VE ON VE.CD_COMPANY = D.CD_COMPANY AND VE.NO_FILE = D.NO_SO
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = D.CD_COMPANY AND SH.NO_SO = D.NO_SO
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN (SELECT MV.CD_COMPANY, MV.CD_PARTNER, MV.NO_IMO,
                  STRING_AGG(FP.NM_EMAIL, ';') AS TO_EMAIL
           FROM CZ_SA_AUTO_MAIL_VESSEL MV
           LEFT JOIN FI_PARTNERPTR FP ON FP.CD_COMPANY = MV.CD_COMPANY AND FP.CD_PARTNER = MV.CD_PARTNER AND FP.SEQ = MV.SEQ
           WHERE MV.CD_COMPANY = @P_CD_COMPANY
           AND ISNULL(MV.YN_SO, 'N') = 'Y'
           GROUP BY MV.CD_COMPANY, MV.CD_PARTNER, MV.NO_IMO) MV
ON MV.CD_COMPANY = SH.CD_COMPANY AND MV.CD_PARTNER = SH.CD_PARTNER AND MV.NO_IMO = SH.NO_IMO
WHERE (ISNULL(@P_CD_PARTNER_GRP, '') = '' OR MP.CD_PARTNER_GRP = @P_CD_PARTNER_GRP)
AND NOT EXISTS (SELECT 1 
                FROM CZ_MA_CODEDTL MC
                WHERE MC.CD_COMPANY = SH.CD_COMPANY
                AND MC.CD_FIELD = 'CZ_SA00048'
                AND MC.CD_SYSDEF = SH.CD_GIR_MAIN)

GO