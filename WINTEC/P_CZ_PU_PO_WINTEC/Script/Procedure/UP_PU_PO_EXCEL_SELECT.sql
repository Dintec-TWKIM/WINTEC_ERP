USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_PO_EXCEL_SELECT]    Script Date: 2022-03-24 오후 6:13:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
ALTER PROCEDURE [NEOE].[UP_PU_PO_EXCEL_SELECT]    
(    
	@P_CD_COMPANY  NVARCHAR(7),    
	@P_CD_PLANT    NVARCHAR(7),    
	@P_MULTI_ITEM  NVARCHAR(4000),
    @P_FG_LANG     NVARCHAR(4) = NULL	--언어
)    
AS
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG
    
    
SET NOCOUNT ON    
    
SELECT          CD_ITEM,    
                NM_ITEM,                --품목명    
                STND_ITEM,                --규격    
                UNIT_PO,                --단위    
                UNIT_IM,                --관리단위    
                CD_SL,    
                (SELECT NM_SL FROM DZSN_MA_SL WHERE  CD_SL = A.CD_SL AND CD_PLANT = A.CD_PLANT AND CD_BIZAREA = A.CD_BIZAREA AND CD_COMPANY = A.CD_COMPANY) NM_SL,         
                UNIT_PO_FACT,
				A.CD_PLANT,
				LOTSIZE,
				(SELECT NM_SYSDEF FROM DZSN_MA_CODEDTL MC WHERE MC.CD_COMPANY = @P_CD_COMPANY AND MC.CD_FIELD = 'MA_B000010' AND MC.CD_SYSDEF = A.CLS_ITEM) AS NM_CLS_ITEM ,
				A.GRP_MFG,
				(SELECT NM_SYSDEF FROM DZSN_MA_CODEDTL MC1 WHERE MC1.CD_COMPANY = @P_CD_COMPANY AND MC1.CD_FIELD = 'MA_B000066' AND MC1.CD_SYSDEF = A.GRP_MFG) AS NM_GRPMFG ,
				A.CD_CC,
				C.NM_CC,
				A.EN_ITEM,
				A.FG_SERNO,
				A.LT_ITEM,
				A.NM_MAKER

FROM            DZSN_MA_PITEM A    
LEFT JOIN		DZSN_MA_CC C ON C.CD_CC = A.CD_CC AND C.CD_COMPANY = A.CD_COMPANY
WHERE           A.CD_COMPANY = @P_CD_COMPANY     
AND             A.CD_PLANT = @P_CD_PLANT    
AND            (CD_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_ITEM)) OR @P_MULTI_ITEM IS NULL OR @P_MULTI_ITEM = '')    
  
    
SET NOCOUNT OFF
GO


