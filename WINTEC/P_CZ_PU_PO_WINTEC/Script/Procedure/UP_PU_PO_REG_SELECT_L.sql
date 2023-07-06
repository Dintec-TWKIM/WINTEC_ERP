USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_PO_REG_SELECT_L]    Script Date: 2022-03-24 오후 3:22:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_PU_PO_REG_SELECT_L]        
(        
    @P_CD_COMPANY        NVARCHAR(7),        
    @P_CD_PLANT                NVARCHAR(7),         
    @P_NO_PO                NVARCHAR(20),        
    @P_NO_POLINE        NUMERIC(17, 4),
    @P_FG_LANG     NVARCHAR(4) = NULL	--언어
) AS
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG
        
SELECT 'N' AS CHK,  A.NO_PO, A.NO_POLINE, A.NO_LINE, A.CD_MATL, P.NM_ITEM, P.STND_ITEM, P.STND_DETAIL_ITEM, P.UNIT_MO, A.QT_NEED_UNIT, A.QT_NEED,         
    A.QT_REQ, A.QT_NEED - A.QT_REQ QT_LOSS, B.QT_PO, D.NO_ECN, D.NO_HST, D.ECN_DT, D.FG_BOM,  A.CD_PLANT ,
    A.NO_RELATION, A.SEQ_RELATION, A.TXT_USERDEF1, A.NUM_USERDEF1, A.TXT_USERDEF2,A.TXT_USERDEF3,A.TXT_USERDEF4,A.TXT_USERDEF5,A.NUM_USERDEF2
    
  FROM PU_POLL A INNER JOIN PU_POL B     
    ON B.CD_COMPANY = @P_CD_COMPANY        
   AND B.NO_PO = @P_NO_PO        
   AND B.NO_LINE = @P_NO_POLINE        
INNER JOIN PU_POH C ON C.CD_COMPANY = @P_CD_COMPANY        
                                 AND C.CD_PLANT = @P_CD_PLANT        
                                 AND C.NO_PO = @P_NO_PO        
LEFT OUTER JOIN DZSN_MA_PITEM P ON P.CD_COMPANY = @P_CD_COMPANY        
                                         AND P.CD_PLANT = @P_CD_PLANT        
                                         AND P.CD_ITEM = A.CD_MATL        
LEFT OUTER JOIN DZSN_SU_BOML D ON D.CD_COMPANY = @P_CD_COMPANY        
                                         AND D.CD_PLANT = @P_CD_PLANT        
                                         AND D.CD_PARTNER = C.CD_PARTNER        
                                         AND D.CD_ITEM = B.CD_ITEM        
                                         AND D.CD_MATL = A.CD_MATL        
WHERE A.CD_COMPANY = @P_CD_COMPANY        
AND A.NO_PO = @P_NO_PO        
AND A.NO_POLINE = @P_NO_POLINE
GO


