USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_ASSEMBLING_MNG_ID]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_ASSEMBLING_MNG_ID]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
    @P_CD_ITEM          NVARCHAR(10),
	@P_NO_ID_FROM		NVARCHAR(10),
    @P_NO_ID_TO		    NVARCHAR(10)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
       WO.CD_ITEM AS CD_PITEM,
       WD.NO_ID,
       ISNULL(NULLIF(WD.NO_HEAT, ''), WO.TXT_USERDEF1) AS NO_HEAT,
       (SELECT MAX(WI2.NO_HEAT) AS NO_HEAT 
        FROM CZ_PR_WO_INSP WI2 
        WHERE WI2.CD_COMPANY = WO.CD_COMPANY 
        AND WI2.NO_WO = WO.NO_WO  
        AND WI2.NO_INSP IN (0, 999, 995)
        AND WI2.SEQ_WO = WD.SEQ_WO) AS NO_LOT
FROM PR_WO WO
LEFT JOIN CZ_PR_WO_REQ_D WD ON WD.CD_COMPANY = WO.CD_COMPANY AND WD.NO_WO = WO.NO_WO
WHERE WO.CD_COMPANY = @P_CD_COMPANY
AND WO.CD_PLANT = @P_CD_PLANT
AND WO.CD_ITEM = @P_CD_ITEM
AND (ISNULL(@P_NO_ID_FROM, '')  = '' OR WD.NO_ID >= ISNULL(@P_NO_ID_FROM, '')) 
AND (ISNULL(@P_NO_ID_TO, '') = '' OR WD.NO_ID <= ISNULL(@P_NO_ID_TO, ''))
AND EXISTS (SELECT 1 
            FROM PR_WO_ROUT WR1
            LEFT JOIN CZ_PR_WO_INSP WI1 ON WI1.CD_COMPANY = WR1.CD_COMPANY AND WI1.NO_WO = WR1.NO_WO AND WI1.NO_LINE = WR1.NO_LINE
            WHERE WR1.CD_COMPANY = WO.CD_COMPANY
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
                FROM CZ_PR_ASSEMBLING_DATA AD
                WHERE AD.CD_COMPANY = WD.CD_COMPANY
                AND AD.CD_PLANT = WO.CD_PLANT
                AND AD.NO_ID_C = WD.NO_ID) -- 등록된 품목 제외
AND NOT EXISTS (SELECT 1 
                FROM CZ_PR_ASSEMBLING_DEACTIVATE MD
                WHERE MD.CD_COMPANY = WO.CD_COMPANY
                AND MD.CD_PLANT = WO.CD_PLANT
                AND MD.NO_ID = WD.NO_ID) -- 대기 품목 제외
AND NOT EXISTS (SELECT 1 
                FROM CZ_PR_ASSEMBLING_SA_SOL SL
                WHERE SL.CD_COMPANY = WO.CD_COMPANY
                AND SL.CD_PLANT = WO.CD_PLANT
                AND SL.NO_ID = WD.NO_ID) -- 단품 판매 품목 제외
UNION ALL
SELECT 'N' AS S,
       OD.CD_ITEM AS CD_PITEM,
       OD.NO_ID,
       OD.NO_HEAT,
       OD.NO_LOT 
FROM CZ_PR_ASSEMBLING_ID_OLD OD
WHERE OD.CD_COMPANY = @P_CD_COMPANY
AND OD.CD_PLANT = @P_CD_PLANT
AND OD.CD_ITEM = @P_CD_ITEM
AND (ISNULL(@P_NO_ID_FROM, '')  = '' OR OD.NO_ID >= ISNULL(@P_NO_ID_FROM, '')) 
AND (ISNULL(@P_NO_ID_TO, '') = '' OR OD.NO_ID <= ISNULL(@P_NO_ID_TO, ''))
AND NOT EXISTS (SELECT 1 
                FROM CZ_PR_ASSEMBLING_DATA MD
                WHERE MD.CD_COMPANY = OD.CD_COMPANY
                AND MD.CD_PLANT = OD.CD_PLANT
                AND MD.NO_ID_C = OD.NO_ID) -- 등록된 품목 제외
AND NOT EXISTS (SELECT 1 
                FROM CZ_PR_ASSEMBLING_DEACTIVATE MD
                WHERE MD.CD_COMPANY = OD.CD_COMPANY
                AND MD.CD_PLANT = OD.CD_PLANT
                AND MD.NO_ID = OD.NO_ID) -- 대기 품목 제외
AND NOT EXISTS (SELECT 1 
                FROM CZ_PR_ASSEMBLING_SA_SOL SL
                WHERE SL.CD_COMPANY = OD.CD_COMPANY
                AND SL.CD_PLANT = OD.CD_PLANT
                AND SL.NO_ID = OD.NO_ID) -- 단품 판매 품목 제외
ORDER BY NO_ID ASC

GO