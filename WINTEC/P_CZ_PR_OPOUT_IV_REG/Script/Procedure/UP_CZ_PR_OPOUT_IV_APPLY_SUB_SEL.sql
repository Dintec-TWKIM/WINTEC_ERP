USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_CZ_PR_OPOUT_IV_APPLY_SUB_SEL]    Script Date: 2023-04-06 오후 2:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*******************************************/  
/*********************************************/
/*********************************************/  
/*********************************************/ 
ALTER PROCEDURE [NEOE].[UP_CZ_PR_OPOUT_IV_APPLY_SUB_SEL]
(        
    @P_CD_COMPANY       NVARCHAR(7),          
    @P_CD_PLANT         NVARCHAR(7),          
    @P_DT_WORK_START    NVARCHAR(8),          
    @P_DT_WORK_END      NVARCHAR(8),          
    @P_CD_PARTNER       NVARCHAR(20),          
    @P_CD_ITEM          NVARCHAR(50),          
    @P_FG_TAXP          NVARCHAR(3),          
    @P_FG_TRANS         NVARCHAR(3),          
    @P_FG_TAX           NVARCHAR(3),         
    @P_CD_EXCH          NVARCHAR(3),
    @P_NO_EMP			NVARCHAR(20)  = NULL,
    @P_CD_PROJECT		NVARCHAR(20)  = NULL,
    @P_FG_LANG     NVARCHAR(4) = NULL	--언어
) 
AS
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG

SET ANSI_WARNINGS OFF
SET ARITHIGNORE ON
SET ARITHABORT OFF
       
DECLARE @P_AM_FSIZE		NUMERIC(3,0),	-- 소수점      
		@P_AM_UPDOWN	NVARCHAR(3),	-- 올림구분      
		@P_AM_FORMAT	NVARCHAR(50),	-- 형태(#,###,###,###.00)        
		@YN_MFG_AUTH	NCHAR(1)

EXEC UP_SF_GETUNIT_AM  @P_CD_COMPANY,'PR', 'I',  @P_AM_FSIZE OUTPUT,  @P_AM_UPDOWN OUTPUT,  @P_AM_FORMAT OUTPUT   -- 모듈별통제값가져오기      

SELECT	@YN_MFG_AUTH  = ISNULL((SELECT	YN_MFG_AUTH
								FROM	MA_ENV
								WHERE	CD_COMPANY = @P_CD_COMPANY ), 'N')

SELECT 
'N' CHK,         
R.CD_PLANT,         
R.NO_WORK,         
R.DT_WORK,         
R.CD_ITEM,         
P.NM_ITEM,         
P.STND_ITEM,         
P.UNIT_IM,         
R.QT_MOVE - R.QT_CLS AS QT_MOVE,         
CONVERT(decimal,L.AM_EX) / L.QT_PO    AS UM_EX,   -- R.UM_EX        
CONVERT(decimal,L.AM) / L.QT_PO     AS UM_WORK,   -- R.UM_WORK        
R.QT_CLS,        
R.QT_MOVE,        
(ISNULL(R.QT_MOVE,0) - R.QT_CLS) * CONVERT(decimal,L.AM_EX) / L.QT_PO AS AM_EX,      
(ISNULL(R.QT_MOVE,0) - R.QT_CLS) * CONVERT(decimal,L.AM) / L.QT_PO  AS AM_WORK,      
NEOE.FN_SF_GETUNIT_AM ('PR', '', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, ((ISNULL(R.QT_MOVE, 0) - R.QT_CLS ) * CONVERT(decimal,L.AM) / L.QT_PO) * (CONVERT(NUMERIC(15,4), D.CD_FLAG1) / 100)) AS AM_VAT_WORK,      

((ISNULL(R.QT_MOVE,0) - R.QT_CLS) * CONVERT(decimal,L.AM) / L.QT_PO) +      
NEOE.FN_SF_GETUNIT_AM ('PR', '', @P_AM_FSIZE, @P_AM_UPDOWN, @P_AM_FORMAT, ((ISNULL(R.QT_MOVE, 0) - R.QT_CLS ) * CONVERT(decimal,L.AM) / L.QT_PO) * (CONVERT(NUMERIC(15,4), D.CD_FLAG1) / 100)) AS AM_HAP_WORK,         
     
R.NO_WO    AS NO_WO,         
R.NO_OPOUT_PO  AS NO_PO,         
R.NO_OPOUT_PO_LINE AS NO_POLINE,         
R.NO_EMP,         
B.NM_KOR,        
R.CD_OP,         
R.CD_WC,         
R.CD_WCOP,         
R.QT_CLS,         
R.QT_MOVE - R.QT_CLS AS QT_MOVE_ORIGIN,         
R.DC_RMK1,         
R.DC_RMK2,         
W.FG_TPPURCHASE,         
H.CD_PARTNER,         
A.LN_PARTNER,               
H.FG_TAX,         
D.NM_SYSDEF   AS NM_FG_TAX,  
ISNULL(D.CD_FLAG1, 0) AS TAX_RATE,     
'001'    AS FG_TRANS,         
NEOE.FN_MA_GET_CODEDTL_NM_SYSDEF(@P_CD_COMPANY, 'PU_C000016', '001') AS NM_FG_TRANS,  
H.CD_EXCH,         
NEOE.FN_MA_GET_CODEDTL_NM_SYSDEF(@P_CD_COMPANY, 'MA_B000005', H.CD_EXCH) AS NM_EXCH, 
H.RT_EXCH,               
E.NM_WC,         
P.CD_CC,         
J.NM_CC,         
F.NM_OP,         
K.NM_TP,           
L.UM_MATL,        
L.UM_SOUL,      
X.UM_MATL AS STD_UM_MATL,        
X.UM_SOUL AS STD_UM_SOUL,        
X.UM  AS STD_UM,    
A.FG_PAYBILL, --2011.08.23.REJINA (지급구분)      
L.UNIT_CH,
L.QT_CHCOEF,
L.OLD_QT_PO,
R.QT_WORK_CHCOEF,
R.QT_WORK_BAD_CHCOEF,
W.NO_PJT AS CD_PJT,
SH.NM_PROJECT,
W.SEQ_PROJECT,
SL.CD_ITEM AS CD_PJT_ITEM, --프로젝트 품목코드    
I2.NM_ITEM AS NM_PJT_ITEM, --프로젝트 품목명    
I2.STND_ITEM AS PJT_ITEM_STND, --프로젝트 품목 규격
P.EN_ITEM,
P.STND_DETAIL_ITEM,
P.MAT_ITEM,
P.NM_MAKER,
P.BARCODE,
P.NO_MODEL,
W.TXT_USERDEF1 AS TXT_USERDEF1_WO
FROM PR_WORK R 
JOIN PR_OPOUT_POH H ON H.CD_COMPANY = R.CD_COMPANY AND H.CD_PLANT = R.CD_PLANT AND H.NO_PO = R.NO_OPOUT_PO        
JOIN PR_OPOUT_POL L ON L.CD_COMPANY = R.CD_COMPANY AND L.CD_PLANT = R.CD_PLANT AND L.NO_PO = R.NO_OPOUT_PO AND L.NO_LINE = R.NO_OPOUT_PO_LINE        
JOIN PR_WO W ON W.CD_COMPANY = R.CD_COMPANY AND W.CD_PLANT = R.CD_PLANT AND W.NO_WO = R.NO_WO        
JOIN DZSN_MA_PITEM P ON P.CD_COMPANY = R.CD_COMPANY AND P.CD_PLANT = R.CD_PLANT AND P.CD_ITEM = R.CD_ITEM        
LEFT JOIN DZSN_MA_PARTNER A ON A.CD_COMPANY = H.CD_COMPANY AND A.CD_PARTNER = H.CD_PARTNER        
LEFT JOIN DZSN_MA_EMP B ON B.CD_COMPANY = R.CD_COMPANY AND B.NO_EMP = R.NO_EMP        
LEFT JOIN DZSN_MA_CODEDTL D ON D.CD_COMPANY = H.CD_COMPANY AND D.CD_SYSDEF = H.FG_TAX AND D.CD_FIELD = 'MA_B000046'   --부가세 매입   
LEFT JOIN DZSN_MA_WC E ON E.CD_COMPANY = R.CD_COMPANY AND E.CD_PLANT = R.CD_PLANT AND E.CD_WC = R.CD_WC
LEFT JOIN DZSN_PR_WCOP F ON F.CD_COMPANY = R.CD_COMPANY AND F.CD_PLANT = R.CD_PLANT AND F.CD_WC = R.CD_WC AND F.CD_WCOP = R.CD_WCOP
LEFT JOIN DZSN_MA_CC J ON J.CD_COMPANY = P.CD_COMPANY AND J.CD_CC = P.CD_CC
LEFT JOIN DZSN_MA_AISPOSTH K ON K.CD_COMPANY = W.CD_COMPANY AND K.FG_TP = '500' AND K.CD_TP = W.FG_TPPURCHASE
LEFT JOIN SU_UM_OP X ON R.CD_COMPANY = X.CD_COMPANY AND R.CD_PLANT = X.CD_PLANT AND R.CD_WC = X.CD_WC AND R.CD_WCOP  = X.CD_WCOP
					AND H.CD_PARTNER = X.CD_PARTNER AND R.CD_ITEM  = X.CD_ITEM AND H.CD_EXCH  = X.CD_EXCH
					AND X.DT_START = (SELECT MAX(DT_START) 
									  FROM	 SU_UM_OP
									  WHERE	 CD_COMPANY = R.CD_COMPANY 
									  AND	 CD_PLANT	= R.CD_PLANT 
									  AND	 CD_WC		= R.CD_WC 
									  AND	 CD_WCOP	= R.CD_WCOP
									  AND	 CD_PARTNER = H.CD_PARTNER 
									  AND	 CD_ITEM	= R.CD_ITEM 
									  AND	 CD_EXCH	= H.CD_EXCH)
LEFT JOIN DZSN_SA_PROJECTH SH ON W.CD_COMPANY = SH.CD_COMPANY AND W.NO_PJT = SH.NO_PROJECT
LEFT JOIN SA_PROJECTL SL ON W.CD_COMPANY = SL.CD_COMPANY AND W.NO_PJT = SL.NO_PROJECT AND W.SEQ_PROJECT = SL.SEQ_PROJECT
LEFT JOIN DZSN_MA_PITEM I2 ON SL.CD_COMPANY = I2.CD_COMPANY AND SL.CD_PLANT = I2.CD_PLANT AND SL.CD_ITEM = I2.CD_ITEM   
WHERE R.CD_COMPANY = @P_CD_COMPANY        
    AND R.CD_PLANT  = @P_CD_PLANT        
    AND R.DT_WORK BETWEEN @P_DT_WORK_START AND @P_DT_WORK_END        
    AND (H.CD_PARTNER  = @P_CD_PARTNER OR @P_CD_PARTNER = '' OR @P_CD_PARTNER IS NULL)        
    AND (R.CD_ITEM = @P_CD_ITEM OR @P_CD_ITEM = '' OR @P_CD_ITEM IS NULL)        
    AND (H.CD_EXCH = @P_CD_EXCH OR @P_CD_EXCH = '' OR @P_CD_EXCH IS NULL)        
--    AND R.YN_SUBCON = 'Y'        
    AND (R.QT_MOVE > 0 AND R.QT_MOVE > R.QT_CLS)
	AND  (  (@YN_MFG_AUTH <> 'Y' OR  ISNULL(@P_NO_EMP,'') =  '')
		 OR	(@YN_MFG_AUTH =  'Y' AND ISNULL(@P_NO_EMP,'') <> '' AND EXISTS (SELECT	1
																			FROM	MA_MFG_AUTH AUTH
																			WHERE	AUTH.CD_COMPANY = W.CD_COMPANY
																			AND		AUTH.CD_AUTH	= W.TP_ROUT
																			AND		AUTH.FG_AUTH	= 'PR_TPWO'
																			AND		AUTH.NO_EMP		= @P_NO_EMP))	)
	AND  (  (@YN_MFG_AUTH <> 'Y' OR  ISNULL(@P_NO_EMP,'') =  '')
		 OR	(@YN_MFG_AUTH =  'Y' AND ISNULL(@P_NO_EMP,'') <> '' AND EXISTS (SELECT	1
																			FROM	MA_MFG_AUTH AUTH
																			WHERE	AUTH.CD_COMPANY = R.CD_COMPANY
																			AND		AUTH.CD_AUTH	= R.CD_WC
																			AND		AUTH.FG_AUTH	= 'MA_WC'
																			AND		AUTH.NO_EMP		= @P_NO_EMP))	)
    AND (W.NO_PJT = @P_CD_PROJECT OR @P_CD_PROJECT = '' OR @P_CD_PROJECT IS NULL)
ORDER BY H.CD_PARTNER, R.NO_WORK
