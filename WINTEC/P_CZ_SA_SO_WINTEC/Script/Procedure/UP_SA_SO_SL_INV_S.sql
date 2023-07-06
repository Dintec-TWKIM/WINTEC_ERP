USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SO_SL_INV_S]    Script Date: 2019-11-04 오전 10:40:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_SA_SO_SL_INV_S]
(
    @P_CD_COMPANY       NVARCHAR(7),
    @P_NO_SO            NVARCHAR(20),
    @P_MULTI_CD_ITEM    NVARCHAR(4000),
    @P_FG_LANG     NVARCHAR(4) = NULL	--언어
)
AS
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG


DECLARE @V_DT_SO		NCHAR(8),
	    @V_CD_PLANT		NVARCHAR(7)

SELECT	@V_DT_SO = DT_SO
FROM	SA_SOH
WHERE	CD_COMPANY	= @P_CD_COMPANY AND NO_SO = @P_NO_SO

SELECT	@V_CD_PLANT	= MIN(CD_PLANT)
FROM	SA_SOL
WHERE	CD_COMPANY	= @P_CD_COMPANY AND NO_SO = @P_NO_SO 

SELECT  SH.CD_PARTNER,
        PN.LN_PARTNER,
        SL.CD_ITEM,
        MP.NM_ITEM,
        MP.STND_ITEM,
        SL.QT_SO,
        X.CD_SL,
        X.NM_SL,
        X.QT_INV AS SL_QT_INV,
        SH.DC_RMK
FROM    SA_SOL SL
        INNER JOIN SA_SOH SH ON SL.NO_SO = SH.NO_SO AND SL.CD_COMPANY = SH.CD_COMPANY
        LEFT OUTER JOIN DZSN_MA_PARTNER PN ON SH.CD_COMPANY = PN.CD_COMPANY AND SH.CD_PARTNER = PN.CD_PARTNER
        LEFT OUTER JOIN DZSN_MA_PITEM MP ON SL.CD_COMPANY = MP.CD_COMPANY AND SL.CD_ITEM = MP.CD_ITEM AND SL.CD_PLANT = MP.CD_PLANT
        LEFT OUTER JOIN (
            SELECT  Y.CD_PLANT, Y.CD_SL, MS.NM_SL, Y.CD_ITEM, SUM(Y.QT_INV) QT_INV
            FROM    (
                        SELECT  CD_COMPANY, CD_PLANT, CD_SL, CD_ITEM,
                                QT_GOOD_INV QT_INV
                        FROM    MM_OPENQTL
                        WHERE   CD_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_ITEM))
                        AND     YM_STANDARD = LEFT(@V_DT_SO, 4) + '00'
                        AND     CD_PLANT = @V_CD_PLANT
                        AND     CD_COMPANY = @P_CD_COMPANY

                        UNION ALL

                        SELECT  CD_COMPANY, L.CD_PLANT, L.CD_SL, L.CD_ITEM,
                                L.QT_GOOD_GR - L.QT_GOOD_GI + L.QT_REJECT_GR - L.QT_REJECT_GI + L.QT_TRANS_GR - L.QT_TRANS_GI + L.QT_INSP_GR - L.QT_INSP_GI  QT_INV      
                        FROM    MM_OHSLINVD  L   
                        WHERE   L.DT_IO BETWEEN LEFT(@V_DT_SO, 4) + '0101' AND @V_DT_SO
                        AND     L.CD_COMPANY = @P_CD_COMPANY
                        AND     CD_PLANT = @V_CD_PLANT
                        AND     CD_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_ITEM))
                    ) Y
                    LEFT OUTER JOIN DZSN_MA_SL MS ON	Y.CD_COMPANY = MS.CD_COMPANY AND Y.CD_PLANT = MS.CD_PLANT AND Y.CD_SL = MS.CD_SL
            GROUP BY Y.CD_PLANT, Y.CD_SL, MS.NM_SL, Y.CD_ITEM
            HAVING SUM(Y.QT_INV) <> 0
        ) X ON	SL.CD_PLANT = X.CD_PLANT AND SL.CD_ITEM = X.CD_ITEM
WHERE   SL.CD_COMPANY = @P_CD_COMPANY
AND     SL.NO_SO = @P_NO_SO
GO
