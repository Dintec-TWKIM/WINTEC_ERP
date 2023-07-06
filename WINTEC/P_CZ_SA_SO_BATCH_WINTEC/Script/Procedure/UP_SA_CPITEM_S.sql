USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_CPITEM_S]    Script Date: 2019-11-11 오후 4:03:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_SA_CPITEM_S]  
(  
    @P_CD_COMPANY       NVARCHAR(7),  
    @P_CD_PARTNER       NVARCHAR(20),  
    @P_MULTI_CPITEM     NVARCHAR(4000),
    @P_FG_LANG			NVARCHAR(4) = NULL	--언어  
)  
AS  

-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG
  
BEGIN  
    SELECT  CD_ITEM,  
            CD_PARTNER,  
            CD_PLANT,  
            CD_ITEM_PARTNER,  
            NM_ITEM_PARTNER,  
            STND_ITEM,  
            DC_MAKEITEM,  
            NM_ITEM,  
            STND_MA_ITEM,  
            UNIT_IM,  
            UNIT_PO,  
            TP_PROC,  
            CLS_ITEM,  
            TP_ITEM,  
            GRP_ITEM,  
            UNIT_SO,  
            CD_GISL,  
            NM_GISL,  
            LT_GI,  
            UNIT_SO_FACT  
    FROM    V_HELP_CPITEM  
    WHERE   CD_COMPANY = @P_CD_COMPANY  
    AND     CD_PARTNER = @P_CD_PARTNER  
    AND     CD_ITEM_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CPITEM))  
END
GO

