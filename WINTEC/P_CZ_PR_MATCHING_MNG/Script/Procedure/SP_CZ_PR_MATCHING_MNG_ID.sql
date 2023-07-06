USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MATCHING_MNG_ID]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_MATCHING_MNG_ID]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
    @P_CD_ITEM          NVARCHAR(10),
	@P_NO_ID_FROM		NVARCHAR(10),
    @P_NO_ID_TO		    NVARCHAR(10),
    @P_CD_GRADE_IN      NVARCHAR(4),
    @P_CD_GRADE_OUT     NVARCHAR(4)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT WO.CD_COMPANY,
           WO.NO_WO,
           WR.NO_LINE,
           RI.NO_INSP,
           WO.CD_ITEM AS CD_PITEM,
           WD.SEQ_WO,
           WD.NO_ID,
           RI.DC_ITEM,
           RI.DC_SPEC,
           RI.CD_CLEAR_GRP,
           RI.TP_DATA,
           WI1.NO_DATA,
           ISNULL(NULLIF(WD.NO_HEAT, ''), WO.TXT_USERDEF1) AS NO_HEAT,
           (SELECT MAX(WI2.NO_HEAT) AS NO_HEAT 
            FROM CZ_PR_WO_INSP WI2 
            WHERE WI2.CD_COMPANY = WO.CD_COMPANY 
            AND WI2.NO_WO = WO.NO_WO  
            AND WI2.NO_INSP IN (0, 999, 995) 
            AND WI2.SEQ_WO = WD.SEQ_WO) AS NO_LOT
    FROM PR_WO WO
    LEFT JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
    LEFT JOIN CZ_PR_WO_REQ_D WD ON WD.CD_COMPANY = WO.CD_COMPANY AND WD.NO_WO = WO.NO_WO
    LEFT JOIN CZ_PR_ROUT_INSP RI ON RI.CD_COMPANY = WO.CD_COMPANY AND RI.CD_PLANT = WO.CD_PLANT AND RI.CD_ITEM = WO.CD_ITEM AND RI.NO_OPPATH = WO.PATN_ROUT AND RI.CD_OP = WR.CD_OP AND RI.CD_WCOP = WR.CD_WCOP
    LEFT JOIN CZ_PR_WO_INSP WI UNPIVOT (NO_DATA FOR NO_DATA_ALL IN (NO_DATA1, NO_DATA2, NO_DATA3, NO_DATA4, NO_DATA5)) WI1 
    ON WI1.CD_COMPANY = WO.CD_COMPANY AND WI1.NO_WO = WO.NO_WO AND WI1.NO_LINE = WR.NO_LINE AND WI1.NO_INSP = RI.NO_INSP AND WI1.SEQ_WO = WD.SEQ_WO
    WHERE WO.CD_COMPANY = @P_CD_COMPANY
    AND WO.CD_PLANT = @P_CD_PLANT
    AND WO.CD_ITEM = @P_CD_ITEM
    AND (ISNULL(@P_NO_ID_FROM, '')  = '' OR WD.NO_ID >= ISNULL(@P_NO_ID_FROM, '')) 
    AND (ISNULL(@P_NO_ID_TO, '') = '' OR WD.NO_ID <= ISNULL(@P_NO_ID_TO, ''))
    AND ISNULL(RI.YN_ASSY, 'N') = 'Y'
    AND RI.CD_CLEAR_GRP IS NOT NULL
    AND EXISTS (SELECT 1 
                FROM PR_WO_ROUT WR1
                LEFT JOIN CZ_PR_WO_INSP WI1 ON WI1.CD_COMPANY = WR1.CD_COMPANY AND WI1.NO_WO = WR1.NO_WO AND WI1.NO_LINE = WR1.NO_LINE
                WHERE WR1.CD_COMPANY = WO.CD_COMPANY
                AND WR1.CD_PLANT = WO.CD_PLANT
                AND WR1.NO_WO = WO.NO_WO
                AND WR1.YN_FINAL = 'Y'
                AND WI1.SEQ_WO = WD.SEQ_WO) -- 마지막 공정 완료
    AND NOT EXISTS (SELECT 1 
				    FROM CZ_PR_WO_INSP WI
				    WHERE WI.CD_COMPANY = WO.CD_COMPANY
				    AND WI.NO_WO = WO.NO_WO
				    AND WI.SEQ_WO = WD.SEQ_WO
				    AND WI.NO_INSP = -1) -- 불량품 제외
    AND NOT EXISTS (SELECT 1 
                    FROM CZ_PR_MATCHING_DATA MD
                    WHERE MD.CD_COMPANY = WD.CD_COMPANY
                    AND MD.CD_PLANT = WO.CD_PLANT 
                    AND MD.NO_ID_C = WD.NO_ID) -- 등록된 품목 제외
    AND NOT EXISTS (SELECT 1 
                    FROM CZ_PR_MATCHING_DEACTIVATE MD
                    WHERE MD.CD_COMPANY = WO.CD_COMPANY
                    AND MD.CD_PLANT = WO.CD_PLANT
                    AND MD.NO_ID = WD.NO_ID) -- 대기 품목 제외
    AND NOT EXISTS (SELECT 1 
                    FROM CZ_PR_ASSEMBLING_SA_SOL SL
                    WHERE SL.CD_COMPANY = WO.CD_COMPANY
                    AND SL.CD_PLANT = WO.CD_PLANT
                    AND SL.NO_ID = WD.NO_ID) -- 단품 판매 품목 제외
),
B AS
(
    SELECT A.CD_COMPANY,
           A.CD_PITEM,
           A.NO_ID,
           A.DC_SPEC AS DC_SPEC_IN,
           NULL AS DC_SPEC_OUT,
           (CASE WHEN A.TP_DATA = 'MAX' THEN MAX(A.NO_DATA)
                 WHEN A.TP_DATA = 'MIN' THEN MIN(A.NO_DATA) END) AS NO_DATA_IN,
           NULL AS NO_DATA_OUT,
           A.CD_CLEAR_GRP AS CD_CLEAR_GRP_IN,
           NULL AS CD_CLEAR_GRP_OUT,
           A.NO_HEAT,
           A.NO_LOT
    FROM A
    WHERE A.DC_ITEM = '내경'
    GROUP BY A.CD_COMPANY,
             A.CD_PITEM,
             A.NO_ID,
             A.DC_SPEC,
             A.TP_DATA,
             A.CD_CLEAR_GRP,
             A.NO_HEAT,
             A.NO_LOT
    UNION ALL
    SELECT A.CD_COMPANY,
           A.CD_PITEM,
           A.NO_ID,
           NULL AS DC_SPEC_IN,
           A.DC_SPEC AS DC_SPEC_OUT,
           NULL AS NO_DATA_IN,
           (CASE WHEN A.TP_DATA = 'MAX' THEN MAX(A.NO_DATA)
                 WHEN A.TP_DATA = 'MIN' THEN MIN(A.NO_DATA) END) AS NO_DATA_OUT,
           NULL AS CD_CLEAR_GRP_IN,
           A.CD_CLEAR_GRP AS CD_CLEAR_GRP_OUT,
           A.NO_HEAT,
           A.NO_LOT
    FROM A
    WHERE A.DC_ITEM = '외경'
    GROUP BY A.CD_COMPANY,
             A.CD_PITEM,
             A.NO_ID,
             A.DC_SPEC,
             A.TP_DATA,
             A.CD_CLEAR_GRP,
             A.NO_HEAT,
             A.NO_LOT
),
C AS
(
    SELECT B.CD_COMPANY,
           B.CD_PITEM,
           B.NO_ID,
           MAX(B.DC_SPEC_IN) AS DC_SPEC_IN,
           MAX(B.DC_SPEC_OUT) AS DC_SPEC_OUT,
           MAX(B.NO_DATA_IN) AS NO_DATA_IN,
           MAX(B.NO_DATA_OUT) AS NO_DATA_OUT,
           MAX(B.CD_CLEAR_GRP_IN) AS CD_CLEAR_GRP_IN,
           MAX(B.CD_CLEAR_GRP_OUT) AS CD_CLEAR_GRP_OUT,
           B.NO_HEAT,
           B.NO_LOT 
    FROM B
    GROUP BY B.CD_COMPANY,
             B.CD_PITEM,
             B.NO_ID,
             B.NO_HEAT,
             B.NO_LOT
    UNION ALL
    SELECT ID.CD_COMPANY,
           ID.CD_ITEM AS CD_PITEM,
           ID.NO_ID,
           ID.DC_SPEC_IN,
           ID.DC_SPEC_OUT,
           ID.NO_DATA_IN,
           ID.NO_DATA_OUT,
           ID.CD_CLEAR_GRP_IN,
           ID.CD_CLEAR_GRP_OUT,
           ID.NO_HEAT,
           ID.NO_LOT 
    FROM CZ_PR_MATCHING_ID_OLD ID
    WHERE ID.CD_COMPANY = @P_CD_COMPANY
    AND ID.CD_PLANT = @P_CD_PLANT
    AND ID.CD_ITEM = @P_CD_ITEM
    AND (ISNULL(@P_NO_ID_FROM, '')  = '' OR ID.NO_ID >= ISNULL(@P_NO_ID_FROM, '')) 
    AND (ISNULL(@P_NO_ID_TO, '') = '' OR ID.NO_ID <= ISNULL(@P_NO_ID_TO, ''))
    AND NOT EXISTS (SELECT 1 
                    FROM CZ_PR_MATCHING_DATA MD
                    WHERE MD.CD_COMPANY = ID.CD_COMPANY
                    AND MD.CD_PLANT = ID.CD_PLANT 
                    AND MD.NO_ID_C = ID.NO_ID) -- 등록된 품목 제외
    AND NOT EXISTS (SELECT 1 
                    FROM CZ_PR_MATCHING_DEACTIVATE MD
                    WHERE MD.CD_COMPANY = ID.CD_COMPANY
                    AND MD.CD_PLANT = ID.CD_PLANT
                    AND MD.NO_ID = ID.NO_ID) -- 대기 품목 제외
    AND NOT EXISTS (SELECT 1 
                    FROM CZ_PR_ASSEMBLING_SA_SOL SL
                    WHERE SL.CD_COMPANY = ID.CD_COMPANY
                    AND SL.CD_PLANT = ID.CD_PLANT
                    AND SL.NO_ID = ID.NO_ID) -- 단품 판매 품목 제외
)
SELECT 'N' AS S,
       C.CD_PITEM,
       C.NO_ID,
       C.NO_DATA_IN,
       C.NO_DATA_OUT,
       MG.CD_GRADE AS CD_GRADE_IN,
       MG1.CD_GRADE AS CD_GRADE_OUT,
       CONVERT(NUMERIC(6, 4), CONVERT(NUMERIC(10, 4), REPLACE(ISNULL(NULLIF(C.DC_SPEC_IN, ''), '0'), 'Ø', '')) + (C.NO_DATA_IN / 1000)) AS QT_SPEC_IN,
       CONVERT(NUMERIC(6, 4), CONVERT(NUMERIC(10, 4), REPLACE(ISNULL(NULLIF(C.DC_SPEC_OUT, ''), '0'), 'Ø', '')) + (C.NO_DATA_OUT / 1000)) AS QT_SPEC_OUT,
       C.CD_CLEAR_GRP_IN,
       C.CD_CLEAR_GRP_OUT,
       MC.NM_SYSDEF AS NM_CLEAR_GRP_IN,
       MC1.NM_SYSDEF AS NM_CLEAR_GRP_OUT,
       C.NO_HEAT,
       C.NO_LOT
FROM C
LEFT JOIN CZ_PR_MATCHING_GRADE MG ON MG.CD_COMPANY = C.CD_COMPANY AND MG.CD_CLEAR_GRP = C.CD_CLEAR_GRP_IN AND MG.QT_SPEC = C.NO_DATA_IN
LEFT JOIN CZ_PR_MATCHING_GRADE MG1 ON MG1.CD_COMPANY = C.CD_COMPANY AND MG1.CD_CLEAR_GRP = C.CD_CLEAR_GRP_OUT AND MG1.QT_SPEC = C.NO_DATA_OUT
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = C.CD_COMPANY AND MC.CD_FIELD = 'CZ_WIN0013' AND MC.CD_SYSDEF = C.CD_CLEAR_GRP_IN
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = C.CD_COMPANY AND MC1.CD_FIELD = 'CZ_WIN0013' AND MC1.CD_SYSDEF = C.CD_CLEAR_GRP_OUT
WHERE 1=1
AND (ISNULL(@P_CD_GRADE_IN, '') = '' OR MG.CD_GRADE = @P_CD_GRADE_IN)
AND (ISNULL(@P_CD_GRADE_OUT, '') = '' OR MG1.CD_GRADE = @P_CD_GRADE_OUT)
ORDER BY C.NO_ID ASC
OPTION(RECOMPILE)

GO