USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_CZ_PU_POST_RPT_L_SELECT]    Script Date: 2023-05-03 오후 6:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*******************************************                          
*********************************************/    
ALTER PROC [NEOE].[UP_CZ_PU_POST_RPT_L_SELECT]
(
	@P_CD_COMPANY        NVARCHAR(7),
	@P_CD_BIZAREA        NVARCHAR(7),
	@P_CD_PLANT          NVARCHAR(7),
	@P_TYPE_DT           NVARCHAR(3),
	@P_DT_PO_FROM        NVARCHAR(8),
	@P_DT_PO_TO          NVARCHAR(8),
	@P_FG_POST           NVARCHAR(4000),
	@P_FG_TRANS          NVARCHAR(3),
	@P_CD_PURGRP         NVARCHAR(4000),
	@P_NO_EMP            NVARCHAR(10),
	@P_TYPE_UNIT         NVARCHAR(3),
	@P_YN_RCV            NVARCHAR(4), 
	@P_TAB_NAME          NVARCHAR(50), 
	@P_CONDITION         NVARCHAR(50),
	@P_MULTI_CD_SL		 VARCHAR(8000),
    @P_CD_CC             NVARCHAR(12),
    @P_REQ_PURGRP		 NVARCHAR(4000), --요청구매그룹
	@P_REQ_DEPT			 NVARCHAR(12), --요청부서
	@P_REQ_EMP 			 NVARCHAR(10),  --요청자  
	@P_NO_PO			 NVARCHAR(20),  --발주번호 2011.03.04 황슬기 추가
	@P_NO_PARTNER		 NVARCHAR(20),   --거래처 2011.03.04 황슬기 추가
	@P_CD_PJT			 NVARCHAR(20),
	@P_CD_ITEM			NVARCHAR(20),
    @P_FG_LANG     NVARCHAR(4) = NULL	--언어
)
AS
DECLARE
	@V_STR_CONV1 NVARCHAR(300),
	@V_STR_CONV2 NVARCHAR(300)
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG

	SET @V_STR_CONV1 = NEOE.FN_GET_MF_DD(@P_CD_COMPANY, 'PU', '유환', @P_FG_LANG)
	SET @V_STR_CONV2 = NEOE.FN_GET_MF_DD(@P_CD_COMPANY, 'PU', '무환', @P_FG_LANG)

SET NOCOUNT ON

DECLARE @DATE_YYYY        NVARCHAR(4),
		@V_TEST         NVARCHAR(12)
		
SELECT @DATE_YYYY = CONVERT(NVARCHAR(4), GETDATE(), 112)
SET @V_TEST =(select CD_EXC from ma_exc where CD_COMPANY =@P_CD_COMPANY and EXC_TITLE ='구매-수주적용설정')

IF (@P_TAB_NAME = 'NO_PO')
        BEGIN
         SELECT L.NO_PO, L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	            L.CD_UNIT_MM, 
	            L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, MC.NM_CC,

	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO END QT_PO, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_REQ_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_REQ END QT_REQ, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RCV END QT_GR, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RETURN_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RETURN END QT_RETURN, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_TR_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_TR END QT_TR, 
	        
	        CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS_MM, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS_MM, 0)
																				 END)
				 WHEN @P_TYPE_UNIT = '002' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS, 0)
																				 END)
			END QT_CLS, 
	        
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM - L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO - L.QT_RCV END QT_REMAIN, 
	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, PJ.NM_PROJECT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2,
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 

        --                H.NO_PO, H.CD_PLANT, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, H.FG_TAX, 
        --                H.TP_UM_TAX, H.CD_PJT, H.CD_EXCH, H.RT_EXCH, H.AM_EX, H.AM, H.VAT, H.DC50_PO, H.TP_PROCESS, H.FG_TAXP, H.YN_AM, 
        --                H.FG_TRANS

	        M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, A.NM_SYSDEF NM_POST, S.NM_SL, J.NM_QTIOTP NM_FG_RCV, CUR_INV.QT_INV,
	        PRL.CD_PURGRP AS REQPURGRP,
			G1.NM_PURGRP AS NM_REQ_PURGRP, 
			E1.NM_KOR AS REQ_NM_KOR,
		    DE.NM_DEPT AS REQ_NM_DEPT,
		    ISNULL(MAX(PU_REV.QT_REV_MM), 0) AS QT_REV_MM,
		    M.NO_MODEL, --2011.10.12 모델코드추가 이장원
		    L.CD_PJT,
		    L.SEQ_PROJECT,
		    SPL.CD_ITEM AS CD_PJT_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM AS NM_PJT_ITEM, --프로젝트 품목명 
			I2.STND_ITEM AS PJT_ITEM_STND,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN AS NO_PJT_DESIGN,
			M.GRP_ITEM, 
		    MI.NM_ITEMGRP, 
		    M.GRP_MFG, 
		    MC2.NM_SYSDEF AS NM_GRP_MFG, 
		    M.STND_DETAIL_ITEM, 
		    M.MAT_ITEM, 
		    MPA.LN_PARTNER AS NM_PARTNER,
		    ISNULL(MAX(PU_REV.QT_PASS_MM), 0) AS QT_PASS_MM,
		    ISNULL(MAX(PU_REV.QT_BAD_MM), 0) AS QT_BAD_MM

        FROM PU_POH H
	        INNER JOIN PU_POL L ON L.CD_COMPANY = @P_CD_COMPANY
	                                        AND L.NO_PO = H.NO_PO
	        INNER JOIN DZSN_MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY
	                                                AND P.CD_PLANT = @P_CD_PLANT

            LEFT OUTER JOIN ( SELECT CD_COMPANY, NO_PO, NO_POLINE AS NO_LINE,
                                     SUM(ISNULL(QT_REV_MM, 0)) AS QT_REV_MM,
                                     SUM(ISNULL(QT_PASS_MM, 0)) AS QT_PASS_MM,
                                     SUM(ISNULL(QT_BAD_MM, 0)) AS QT_BAD_MM
                                FROM PU_REV
                               WHERE ISNULL(FG_IO,'001') = '001' 
                               GROUP BY CD_COMPANY, NO_PO, NO_POLINE ) AS PU_REV
                         ON PU_REV.CD_COMPANY = L.CD_COMPANY
                        AND PU_REV.NO_PO = L.NO_PO
                        AND PU_REV.NO_LINE = L.NO_LINE
                        

	        LEFT OUTER JOIN MM_QTIO Q ON Q.CD_COMPANY = @P_CD_COMPANY
	                                                 AND Q.NO_PSO_MGMT = L.NO_PO
	                                                 AND Q.NO_PSOLINE_MGMT = L.NO_LINE
	                                                 AND Q.FG_IO IN ('001', '030')
	        LEFT OUTER JOIN DZSN_MA_PITEM M ON M.CD_COMPANY = @P_CD_COMPANY
	                                                        AND M.CD_PLANT = L.CD_PLANT
	                                                        AND M.CD_ITEM = L.CD_ITEM
	        LEFT OUTER JOIN DZSN_MA_CODEDTL A ON A.CD_COMPANY = @P_CD_COMPANY
	                                                                AND A.CD_FIELD = 'PU_C000009'        --오더상태
	                                                                AND A.CD_SYSDEF = L.FG_POST
	        LEFT OUTER JOIN DZSN_MA_SL S ON S.CD_COMPANY = @P_CD_COMPANY
	                                                        AND S.CD_BIZAREA = P.CD_BIZAREA
	                                                        AND S.CD_PLANT = L.CD_PLANT
	                                                        AND S.CD_SL = L.CD_SL
	        LEFT OUTER JOIN DZSN_MM_EJTP J ON J.CD_COMPANY = @P_CD_COMPANY
	                                                        AND J.CD_QTIOTP = L.FG_RCV

	        LEFT OUTER JOIN DZSN_SA_PROJECTH PJ ON PJ.NO_PROJECT = L.CD_PJT AND PJ.CD_COMPANY = L.CD_COMPANY


	        LEFT OUTER JOIN (
						        SELECT CD_SL,ISNULL(SUM(QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR+QT_TRANS_GR), 0) 
								        - ISNULL(SUM(QT_GOOD_GI + QT_REJECT_GI +QT_INSP_GI+QT_TRANS_GI), 0) QT_INV
						        FROM  MM_OHSLINVM
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND YY_INV = @DATE_YYYY
								        AND CD_PLANT = @P_CD_PLANT
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
						        GROUP BY CD_SL
						        UNION ALL
						        SELECT CD_SL, QT_GOOD_INV QT_INV
						        FROM MM_OPENQTL
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND CD_PLANT = @P_CD_PLANT
								        AND YM_STANDARD = @DATE_YYYY
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
                            ) CUR_INV ON 
								        CUR_INV.CD_SL = M.CD_SL

	        LEFT OUTER JOIN DZSN_MA_SL MS ON MS.CD_SL = M.CD_SL AND MS.CD_PLANT = M.CD_PLANT AND MS.CD_COMPANY = M.CD_COMPANY AND MS.CD_BIZAREA = M.CD_BIZAREA
            LEFT OUTER JOIN DZSN_MA_CC MC ON L.CD_CC = MC.CD_CC AND MC.CD_COMPANY = L.CD_COMPANY 
            
            LEFT OUTER JOIN PU_PRL PRL ON PRL.NO_PR = L.NO_PR AND PRL.NO_PRLINE = L.NO_PRLINE AND PRL.CD_COMPANY = L.CD_COMPANY
			LEFT OUTER JOIN DZSN_MA_PURGRP G1 ON PRL.CD_PURGRP = G1.CD_PURGRP  AND PRL.CD_COMPANY = G1.CD_COMPANY 	
			LEFT OUTER JOIN PU_PRH PRH ON PRH.CD_COMPANY = PRL.CD_COMPANY AND PRH.NO_PR = PRL.NO_PR  AND PRH.CD_PLANT  = PRL.CD_PLANT -- 요청부서, 요청자조회조건때문에추가
			LEFT OUTER JOIN  DZSN_MA_EMP E1 ON PRH.NO_EMP= E1.NO_EMP AND  E1.CD_COMPANY =@P_CD_COMPANY 
			LEFT OUTER JOIN DZSN_MA_DEPT DE ON PRH.CD_DEPT = DE.CD_DEPT AND @P_CD_COMPANY = DE.CD_COMPANY 
			LEFT OUTER JOIN DZSN_MA_PARTNER MP ON MP.CD_COMPANY  = @P_CD_COMPANY AND MP.CD_PARTNER = H.CD_PARTNER
			LEFT OUTER JOIN SA_PROJECTL SPL ON L.CD_COMPANY = SPL.CD_COMPANY AND L.CD_PJT = SPL.NO_PROJECT AND L.SEQ_PROJECT = SPL.SEQ_PROJECT    	  
			LEFT JOIN DZSN_MA_PITEM I2 ON SPL.CD_COMPANY = I2.CD_COMPANY AND SPL.CD_PLANT = I2.CD_PLANT AND SPL.CD_ITEM = I2.CD_ITEM    
			LEFT OUTER JOIN MM_QTIOH QH ON QH.CD_COMPANY = Q.CD_COMPANY AND QH.NO_IO = Q.NO_IO
			LEFT OUTER JOIN DZSN_MA_ITEMGRP MI ON M.GRP_ITEM = MI.CD_ITEMGRP AND M.CD_COMPANY = MI.CD_COMPANY
			LEFT OUTER JOIN DZSN_MA_CODEDTL MC2 ON MC2.CD_COMPANY = M.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000066' AND MC2.CD_SYSDEF = M.GRP_MFG  
			LEFT OUTER JOIN DZSN_MA_PARTNER MPA ON MPA.CD_COMPANY = M.CD_COMPANY AND MPA.CD_PARTNER = M.PARTNER
                    WHERE H.CD_COMPANY = @P_CD_COMPANY
	                    AND P.CD_BIZAREA = @P_CD_BIZAREA
	                    AND (L.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)
	                    AND ((@P_TYPE_DT = '001' AND (H.DT_PO BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)) 
	                            OR (@P_TYPE_DT = '002' AND (L.DT_LIMIT BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)))
	                    --AND (L.FG_POST = @P_FG_POST OR @P_FG_POST = '' OR @P_FG_POST IS NULL)
	                    AND (L.FG_POST IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_FG_POST)) OR @P_FG_POST = '' OR @P_FG_POST IS NULL)    
	                    AND (H.FG_TRANS = @P_FG_TRANS OR @P_FG_TRANS = '' OR @P_FG_TRANS IS NULL)
	                    AND (H.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PURGRP)) OR @P_CD_PURGRP = '' OR @P_CD_PURGRP IS NULL)
	                    AND (H.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)
	                    --@P_TYPE_UNIT
	                    AND ((@P_YN_RCV = '' OR @P_YN_RCV IS NULL OR @P_YN_RCV = '001')
	                            OR (@P_YN_RCV = '002' AND L.QT_PO_MM - L.QT_RCV_MM = 0)
							    OR (@P_YN_RCV = '003' AND L.QT_PO_MM - L.QT_RCV_MM > 0))
	                    AND H.NO_PO = @P_CONDITION
	                    AND (L.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
                        AND ((L.CD_CC = @P_CD_CC) OR (@P_CD_CC = '') OR (@P_CD_CC IS NULL)) 
                        AND  (( PRL.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_REQ_PURGRP))) OR (@P_REQ_PURGRP = '' ) OR (@P_REQ_PURGRP  IS NULL)) --요청구매그룹  
						AND (( PRH.CD_DEPT  =@P_REQ_DEPT) OR (@P_REQ_DEPT = '' ) OR (@P_REQ_DEPT  IS NULL)) --요청부서
						AND (( PRH.NO_EMP  =@P_REQ_EMP) OR (@P_REQ_EMP = '' ) OR (@P_REQ_EMP  IS NULL)) -- 요청인원
						AND (( L.NO_PO LIKE '%' + @P_NO_PO + '%') OR (@P_NO_PO = '') OR (@P_NO_PO IS NULL))
						AND ((MP.CD_PARTNER = @P_NO_PARTNER) OR (@P_NO_PARTNER = '') OR (@P_NO_PARTNER IS NULL))   
						AND (@P_CD_PJT = '' OR @P_CD_PJT IS NULL OR @P_CD_PJT = L.CD_PJT) 
						AND (@P_CD_ITEM = '' OR @P_CD_ITEM IS NULL OR @P_CD_ITEM = L.CD_ITEM)
                    GROUP BY L.NO_PO, L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	                    L.CD_UNIT_MM, 
	                    L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 
	                    L.QT_PO_MM, L.QT_PO, L.QT_REQ_MM, L.QT_REQ, L.QT_RCV_MM, L.QT_RCV, L.QT_RETURN_MM, L.QT_RETURN, 
	                    L.QT_TR_MM, L.QT_TR, 
	                    L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, MC.NM_CC,
	                    L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	                    L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, PJ.NM_PROJECT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	                    L.NO_APPLINE, L.DC1, L.DC2, 
	                    L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 
	                    M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, A.NM_SYSDEF, S.NM_SL, J.NM_QTIOTP, CUR_INV.QT_INV,
	                    PRL.CD_PURGRP ,
						G1.NM_PURGRP, 
						E1.NM_KOR ,
						DE.NM_DEPT,
					    M.NO_MODEL, --2011.10.12 모델코드추가 이장원	
					    L.CD_PJT,
						L.SEQ_PROJECT,
						SPL.CD_ITEM, --프로젝트 품목코드  
						I2.NM_ITEM, --프로젝트 품목명 
						I2.STND_ITEM,
						L.DT_PLAN,
						M.UNIT_PO,
						M.NO_DESIGN,
						I2.NO_DESIGN,
						M.GRP_ITEM, 
						MI.NM_ITEMGRP, 
						M.GRP_MFG, 
						MC2.NM_SYSDEF, 
						M.STND_DETAIL_ITEM, 
						M.MAT_ITEM, 
						MPA.LN_PARTNER 
        END
ELSE IF (@P_TAB_NAME = 'DT')                        --일자별
BEGIN
    SELECT 'N' CHK, 
	    --HEADER 내역-------------------------
	        H.NO_PO, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, 
	         H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, 
            MC.NM_CC,
            --H.AM_EX, H.AM, H.VAT, 
            H.DC50_PO DC_RMK, H.TP_PROCESS, 
	        H.FG_TAXP, 
	        CASE WHEN H.YN_AM = 'Y' THEN @V_STR_CONV1 ELSE @V_STR_CONV2 END YN_AM, 
	        H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	        E.NM_SYSDEF NM_TRANS, F.NM_TP NM_PURCHASE, G.NM_PROJECT, 

	        --LINE 내역-------------------------
	        L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	        L.CD_UNIT_MM, 
	        L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 

	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO END QT_PO, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_REQ_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_REQ END QT_REQ, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RCV END QT_GR, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RETURN_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RETURN END QT_RETURN, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_TR_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_TR END QT_TR, 
	        --CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(ISNULL(Q.QT_CLS_MM, 0)) WHEN @P_TYPE_UNIT = '002' THEN SUM(ISNULL(Q.QT_CLS, 0)) END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS_MM, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS_MM, 0)
																				 END)
				 WHEN @P_TYPE_UNIT = '002' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS, 0)
																				 END)
			END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM - L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO - L.QT_RCV END QT_REMAIN, 

	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2, 
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 
		    M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF NM_POST, S.NM_SL, J.NM_QTIOTP NM_FG_RCV, CUR_INV.QT_INV,
	        PRL.CD_PURGRP AS REQPURGRP,
			G1.NM_PURGRP AS NM_REQ_PURGRP, 
			E1.NM_KOR AS REQ_NM_KOR,
			DE.NM_DEPT AS REQ_NM_DEPT,
		    ISNULL(MAX(PU_REV.QT_REV_MM), 0) AS QT_REV_MM,
		    M.NO_MODEL, --2011.10.12 모델코드추가 이장원
		    L.SEQ_PROJECT,
		    SPL.CD_ITEM AS CD_PJT_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM AS NM_PJT_ITEM, --프로젝트 품목명 
			I2.STND_ITEM AS PJT_ITEM_STND,
			H.DT_WEB,
			H.FG_WEB,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN AS NO_PJT_DESIGN,
			H.DC_RMK2,
			H.NO_ORDER,
			L.CD_SL,
			S.NM_SL,
			M.GRP_ITEM, 
		    MI.NM_ITEMGRP, 
		    M.GRP_MFG, 
		    MC2.NM_SYSDEF AS NM_GRP_MFG, 
		    M.STND_DETAIL_ITEM, 
		    M.MAT_ITEM, 
		    MPA.LN_PARTNER AS NM_PARTNER,
		    ISNULL(MAX(PU_REV.QT_PASS_MM), 0) AS QT_PASS_MM,
		    ISNULL(MAX(PU_REV.QT_BAD_MM), 0) AS QT_BAD_MM

	            FROM PU_POH H
	    INNER JOIN PU_POL L ON L.CD_COMPANY = @P_CD_COMPANY
	                                    AND L.NO_PO = H.NO_PO
	    INNER JOIN DZSN_MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY
	                                            AND P.CD_PLANT = H.CD_PLANT

        LEFT OUTER JOIN ( SELECT CD_COMPANY, NO_PO, NO_POLINE AS NO_LINE,
                                 SUM(ISNULL(QT_REV_MM, 0)) AS QT_REV_MM,
                                 SUM(ISNULL(QT_PASS_MM, 0)) AS QT_PASS_MM,
                                 SUM(ISNULL(QT_BAD_MM, 0)) AS QT_BAD_MM
                            FROM PU_REV
                           WHERE ISNULL(FG_IO,'001') = '001'  
                           GROUP BY CD_COMPANY, NO_PO, NO_POLINE ) AS PU_REV
                     ON PU_REV.CD_COMPANY = L.CD_COMPANY
                    AND PU_REV.NO_PO = L.NO_PO
                    AND PU_REV.NO_LINE = L.NO_LINE

	    LEFT OUTER JOIN MM_QTIO Q ON Q.CD_COMPANY = @P_CD_COMPANY
	                                                    AND Q.NO_PSO_MGMT = L.NO_PO
	                                                    AND Q.NO_PSOLINE_MGMT = L.NO_LINE
	                                                    AND Q.FG_IO IN ('001', '030')
	    LEFT OUTER JOIN DZSN_MA_PITEM M ON M.CD_COMPANY = @P_CD_COMPANY
	                                                    AND M.CD_PLANT = L.CD_PLANT
	                                                    AND M.CD_ITEM = L.CD_ITEM
	    LEFT OUTER JOIN DZSN_MA_PARTNER A ON A.CD_COMPANY  = @P_CD_COMPANY
	                                                            AND A.CD_PARTNER = H.CD_PARTNER
	    LEFT OUTER JOIN DZSN_MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY
	                                                    AND B.NO_EMP = H.NO_EMP
	    LEFT OUTER JOIN DZSN_MA_PURGRP C ON C.CD_COMPANY = @P_CD_COMPANY
	                                                            AND C.CD_PURGRP = H.CD_PURGRP
	    LEFT OUTER JOIN DZSN_PU_TPPO D ON D.CD_COMPANY = @P_CD_COMPANY
	                                                    AND D.CD_TPPO = H.CD_TPPO
	    LEFT OUTER JOIN DZSN_MA_CODEDTL E ON E.CD_COMPANY = @P_CD_COMPANY
	                                                            AND E.CD_FIELD = 'PU_C000016'        --거래구분
	                                                            AND E.CD_SYSDEF = H.FG_TRANS
	    LEFT OUTER JOIN DZSN_MA_AISPOSTH F ON F.CD_COMPANY = @P_CD_COMPANY
	                                                            AND F.FG_TP = '200'        --매입형태
	                                                            AND F.CD_TP = L.FG_PURCHASE

	    LEFT OUTER JOIN DZSN_SA_PROJECTH G ON G.NO_PROJECT = L.CD_PJT AND G.CD_COMPANY = L.CD_COMPANY
    	
	    LEFT OUTER JOIN DZSN_MA_SL S ON S.CD_COMPANY = @P_CD_COMPANY
	                                                    AND S.CD_BIZAREA = P.CD_BIZAREA
	                                                    AND S.CD_PLANT = L.CD_PLANT
	                                                    AND S.CD_SL = L.CD_SL
	    LEFT OUTER JOIN DZSN_MM_EJTP J ON J.CD_COMPANY = @P_CD_COMPANY
	                                                    AND J.CD_QTIOTP = L.FG_RCV
	    LEFT OUTER JOIN DZSN_MA_CODEDTL K ON K.CD_COMPANY = @P_CD_COMPANY
	                                                            AND K.CD_FIELD = 'PU_C000009'        --오더상태
	                                                            AND K.CD_SYSDEF = L.FG_POST

	    LEFT OUTER JOIN DZSN_MA_SL MS ON MS.CD_SL = M.CD_SL AND MS.CD_PLANT = M.CD_PLANT AND MS.CD_COMPANY = M.CD_COMPANY AND MS.CD_BIZAREA = M.CD_BIZAREA

	    LEFT OUTER JOIN (
						    SELECT CD_SL,ISNULL(SUM(QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR+QT_TRANS_GR), 0) 
								    - ISNULL(SUM(QT_GOOD_GI + QT_REJECT_GI +QT_INSP_GI+QT_TRANS_GI), 0) QT_INV
						    FROM  MM_OHSLINVM
						    WHERE CD_COMPANY = @P_CD_COMPANY
								    AND YY_INV = @DATE_YYYY
								    AND CD_PLANT = @P_CD_PLANT
								    AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
						    GROUP BY CD_SL
						    UNION ALL
						    SELECT CD_SL, QT_GOOD_INV QT_INV
						    FROM MM_OPENQTL
						    WHERE CD_COMPANY = @P_CD_COMPANY
								    AND CD_PLANT = @P_CD_PLANT
								    AND YM_STANDARD = @DATE_YYYY
								    AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
                        ) CUR_INV ON 
								    CUR_INV.CD_SL = M.CD_SL
    LEFT OUTER JOIN DZSN_MA_CC MC ON L.CD_CC = MC.CD_CC AND MC.CD_COMPANY = L.CD_COMPANY 
    LEFT OUTER JOIN PU_PRL PRL ON PRL.NO_PR = L.NO_PR AND PRL.NO_PRLINE = L.NO_PRLINE AND PRL.CD_COMPANY = L.CD_COMPANY
	LEFT OUTER JOIN DZSN_MA_PURGRP G1 ON PRL.CD_PURGRP = G1.CD_PURGRP  AND PRL.CD_COMPANY = G1.CD_COMPANY 	
	LEFT OUTER JOIN PU_PRH PRH ON PRH.CD_COMPANY = PRL.CD_COMPANY AND PRH.NO_PR = PRL.NO_PR  AND PRH.CD_PLANT  = PRL.CD_PLANT -- 요청부서, 요청자조회조건때문에추가
	LEFT OUTER JOIN  DZSN_MA_EMP E1 ON PRH.NO_EMP= E1.NO_EMP AND  E1.CD_COMPANY =@P_CD_COMPANY 
	LEFT OUTER JOIN DZSN_MA_DEPT DE ON PRH.CD_DEPT = DE.CD_DEPT AND @P_CD_COMPANY = DE.CD_COMPANY
	LEFT OUTER JOIN SA_PROJECTL SPL ON L.CD_COMPANY = SPL.CD_COMPANY AND L.CD_PJT = SPL.NO_PROJECT AND L.SEQ_PROJECT = SPL.SEQ_PROJECT    	  
	LEFT JOIN DZSN_MA_PITEM I2 ON SPL.CD_COMPANY = I2.CD_COMPANY AND SPL.CD_PLANT = I2.CD_PLANT AND SPL.CD_ITEM = I2.CD_ITEM    
	LEFT OUTER JOIN MM_QTIOH QH ON QH.CD_COMPANY = Q.CD_COMPANY AND QH.NO_IO = Q.NO_IO
	LEFT OUTER JOIN DZSN_MA_ITEMGRP MI ON M.GRP_ITEM = MI.CD_ITEMGRP AND M.CD_COMPANY = MI.CD_COMPANY
	LEFT OUTER JOIN DZSN_MA_CODEDTL MC2 ON MC2.CD_COMPANY = M.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000066' AND MC2.CD_SYSDEF = M.GRP_MFG  
	LEFT OUTER JOIN DZSN_MA_PARTNER MPA ON MPA.CD_COMPANY = M.CD_COMPANY AND MPA.CD_PARTNER = M.PARTNER
              WHERE H.CD_COMPANY = @P_CD_COMPANY
	            AND P.CD_BIZAREA = @P_CD_BIZAREA
	            AND (L.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)
	            AND ((@P_TYPE_DT = '001' AND (H.DT_PO BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)) 
	             OR (@P_TYPE_DT = '002' AND (L.DT_LIMIT BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)))
	            --AND (L.FG_POST = @P_FG_POST OR @P_FG_POST = '' OR @P_FG_POST IS NULL)
	            AND (L.FG_POST IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_FG_POST)) OR @P_FG_POST = '' OR @P_FG_POST IS NULL)    
                AND (H.FG_TRANS = @P_FG_TRANS OR @P_FG_TRANS = '' OR @P_FG_TRANS IS NULL)
                AND (H.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PURGRP)) OR @P_CD_PURGRP = '' OR @P_CD_PURGRP IS NULL)
                AND (H.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)
                --@P_TYPE_UNIT
                AND ((@P_YN_RCV = '' OR @P_YN_RCV IS NULL OR @P_YN_RCV = '001')
                        OR (@P_YN_RCV = '002' AND L.QT_PO_MM - L.QT_RCV_MM = 0)
                        OR (@P_YN_RCV = '003' AND L.QT_PO_MM - L.QT_RCV_MM > 0))
                AND H.DT_PO = @P_CONDITION
                AND (L.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)	
                AND ((L.CD_CC = @P_CD_CC) OR (@P_CD_CC = '') OR (@P_CD_CC IS NULL)) 
                AND  (( PRL.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_REQ_PURGRP))) OR (@P_REQ_PURGRP = '' ) OR (@P_REQ_PURGRP  IS NULL)) --요청구매그룹  
				AND (( PRH.CD_DEPT  =@P_REQ_DEPT) OR (@P_REQ_DEPT = '' ) OR (@P_REQ_DEPT  IS NULL)) --요청부서
				AND (( PRH.NO_EMP  =@P_REQ_EMP) OR (@P_REQ_EMP = '' ) OR (@P_REQ_EMP  IS NULL)) -- 요청인원
				AND (( L.NO_PO LIKE '%' + @P_NO_PO + '%') OR (@P_NO_PO = '') OR (@P_NO_PO IS NULL))
				AND ((A.CD_PARTNER = @P_NO_PARTNER) OR (@P_NO_PARTNER = '') OR (@P_NO_PARTNER IS NULL)) 
				AND (@P_CD_PJT = '' OR @P_CD_PJT IS NULL OR @P_CD_PJT = L.CD_PJT) 
				AND (@P_CD_ITEM = '' OR @P_CD_ITEM IS NULL OR @P_CD_ITEM = L.CD_ITEM)
       GROUP BY H.NO_PO, H.CD_PLANT, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, 
	            H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, H.DC50_PO, H.TP_PROCESS, 
	            H.FG_TAXP, H.YN_AM, H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	            E.NM_SYSDEF, F.NM_TP, G.NM_PROJECT, 
	            L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	            L.CD_UNIT_MM, MC.NM_CC,
	            L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 
	            L.QT_PO_MM, L.QT_PO, L.QT_REQ_MM, L.QT_REQ, L.QT_RCV_MM, L.QT_RCV, L.QT_RETURN_MM, L.QT_RETURN, 
	            L.QT_TR_MM, L.QT_TR, 
	            L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	            L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	            L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	            L.NO_APPLINE, L.DC1, L.DC2, 
	            L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 
	            M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF, S.NM_SL, J.NM_QTIOTP, CUR_INV.QT_INV,
	            PRL.CD_PURGRP ,
				G1.NM_PURGRP, 
				E1.NM_KOR ,
				DE.NM_DEPT,
				M.NO_MODEL, --2011.10.12 모델코드추가 이장원
				L.SEQ_PROJECT,
				SPL.CD_ITEM, --프로젝트 품목코드  
				I2.NM_ITEM , --프로젝트 품목명 
				I2.STND_ITEM,
				H.DT_WEB,
				H.FG_WEB,
				L.DT_PLAN,
				M.UNIT_PO,
				M.NO_DESIGN,
				I2.NO_DESIGN,
				H.DC_RMK2,
				H.NO_ORDER,
				L.CD_SL,
				S.NM_SL,
				M.GRP_ITEM, 
				MI.NM_ITEMGRP, 
				M.GRP_MFG, 
				MC2.NM_SYSDEF, 
				M.STND_DETAIL_ITEM, 
				M.MAT_ITEM, 
				MPA.LN_PARTNER
      ORDER BY H.DT_PO
    END
ELSE IF (@P_TAB_NAME = 'CD_PARTNER')        --거래처별
    BEGIN
        SELECT 'N' CHK, 
	            --HEADER 내역-------------------------
	            H.NO_PO, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, 
	            H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, H.DC50_PO DC_RMK, H.TP_PROCESS, 
			    MC.NM_CC, H.FG_TAXP, 
	            CASE WHEN H.YN_AM = 'Y' THEN @V_STR_CONV1 ELSE @V_STR_CONV2 END YN_AM, 
	            H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	            E.NM_SYSDEF NM_TRANS, F.NM_TP NM_PURCHASE, 

	            --LINE 내역-------------------------
	            L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	            L.CD_UNIT_MM,L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 

	            CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO END QT_PO, 
	            CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_REQ_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_REQ END QT_REQ, 
	            CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RCV END QT_GR, 
	            CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RETURN_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RETURN END QT_RETURN, 
	            CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_TR_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_TR END QT_TR, 
	            --CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(ISNULL(Q.QT_CLS_MM, 0)) WHEN @P_TYPE_UNIT = '002' THEN SUM(ISNULL(Q.QT_CLS, 0)) END QT_CLS, 
	            CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS_MM, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS_MM, 0)
																				 END)
				 WHEN @P_TYPE_UNIT = '002' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS, 0)
																				 END)
				END QT_CLS, 
	            CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM - L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO - L.QT_RCV END QT_REMAIN, 

	            L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	            L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	            L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, G.NM_PROJECT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	            L.NO_APPLINE, L.DC1, L.DC2, 
	            L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 

	            M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF NM_POST, S.NM_SL, J.NM_QTIOTP NM_FG_RCV, CUR_INV.QT_INV,
	            PRL.CD_PURGRP AS REQPURGRP,
				G1.NM_PURGRP AS NM_REQ_PURGRP, 
				E1.NM_KOR AS REQ_NM_KOR,
				DE.NM_DEPT AS REQ_NM_DEPT,

                ISNULL(MAX(PU_REV.QT_REV_MM), 0) AS QT_REV_MM,
			     CASE WHEN @V_TEST  = '100' THEN      PRL.NO_SO    --요청
				 	  WHEN @V_TEST  = '200' THEN      L.NO_SO     --발주
				      WHEN @V_TEST  = '500' THEN     AP.NO_SO     --품의
							  END  AS NO_SO                --2011.09.23 수주번호 추가 이장원
			    ,MP.LN_PARTNER      AS SO_GI_PARTNER--2011.09.23 납품처명 추가 이장원
			    ,PT.LN_PARTNER AS SO_LN_PARTNER --2011.09.23 거래처명 추가 이장원
			    ,M.NO_MODEL, --2011.10.12 모델코드추가 이장원
				 L.SEQ_PROJECT,
				 SPL.CD_ITEM AS CD_PJT_ITEM, --프로젝트 품목코드  
				 I2.NM_ITEM AS NM_PJT_ITEM , --프로젝트 품목명 
				 I2.STND_ITEM AS PJT_ITEM_STND,
				 H.DT_WEB,
				 H.FG_WEB,
				 L.DT_PLAN,
				 M.UNIT_PO,
				 M.NO_DESIGN,
				 I2.NO_DESIGN AS NO_PJT_DESIGN,
				 H.DC_RMK2,
				 H.NO_ORDER,
				 L.CD_SL,
				 S.NM_SL,
				 M.GRP_ITEM, 
				MI.NM_ITEMGRP, 
				M.GRP_MFG, 
				MC2.NM_SYSDEF AS NM_GRP_MFG, 
				M.STND_DETAIL_ITEM, 
				M.MAT_ITEM, 
				MPA.LN_PARTNER AS NM_PARTNER,
				ISNULL(MAX(PU_REV.QT_PASS_MM), 0) AS QT_PASS_MM,
				ISNULL(MAX(PU_REV.QT_BAD_MM), 0) AS QT_BAD_MM

            FROM PU_POH H
	            INNER JOIN PU_POL L ON L.CD_COMPANY = @P_CD_COMPANY
	                                            AND L.NO_PO = H.NO_PO
	            INNER JOIN DZSN_MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY
	                                                    AND P.CD_PLANT = H.CD_PLANT

                LEFT OUTER JOIN ( SELECT CD_COMPANY, NO_PO, NO_POLINE AS NO_LINE,
										 SUM(ISNULL(QT_REV_MM, 0)) AS QT_REV_MM,
										 SUM(ISNULL(QT_PASS_MM, 0)) AS QT_PASS_MM,
										 SUM(ISNULL(QT_BAD_MM, 0)) AS QT_BAD_MM
								 FROM PU_REV
								WHERE ISNULL(FG_IO,'001') = '001' 
								 GROUP BY CD_COMPANY, NO_PO, NO_POLINE ) AS PU_REV
								 ON PU_REV.CD_COMPANY = L.CD_COMPANY
									 AND PU_REV.NO_PO = L.NO_PO
									 AND PU_REV.NO_LINE = L.NO_LINE

	            LEFT OUTER JOIN MM_QTIO Q ON Q.CD_COMPANY = @P_CD_COMPANY
	                                                            AND Q.NO_PSO_MGMT = L.NO_PO
	                                                            AND Q.NO_PSOLINE_MGMT = L.NO_LINE
	                                                            AND Q.FG_IO IN ('001', '030')
	            LEFT OUTER JOIN DZSN_MA_PITEM M ON M.CD_COMPANY = @P_CD_COMPANY
	                                                            AND M.CD_PLANT = L.CD_PLANT
	                                                            AND M.CD_ITEM = L.CD_ITEM
	            LEFT OUTER JOIN DZSN_MA_PARTNER A ON A.CD_COMPANY  = @P_CD_COMPANY
	                                                                    AND A.CD_PARTNER = H.CD_PARTNER
	            LEFT OUTER JOIN DZSN_MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY
	                                                            AND B.NO_EMP = H.NO_EMP
	            LEFT OUTER JOIN DZSN_MA_PURGRP C ON C.CD_COMPANY = @P_CD_COMPANY
	                                                                    AND C.CD_PURGRP = H.CD_PURGRP
	            LEFT OUTER JOIN DZSN_PU_TPPO D ON D.CD_COMPANY = @P_CD_COMPANY
	                                                            AND D.CD_TPPO = H.CD_TPPO
	            LEFT OUTER JOIN DZSN_MA_CODEDTL E ON E.CD_COMPANY = @P_CD_COMPANY
	                                   AND E.CD_FIELD = 'PU_C000016'        --거래구분
	                                                                    AND E.CD_SYSDEF = H.FG_TRANS
	            LEFT OUTER JOIN DZSN_MA_AISPOSTH F ON F.CD_COMPANY = @P_CD_COMPANY
	                                                                    AND F.FG_TP = '200'        --매입형태
	                                                                    AND F.CD_TP = L.FG_PURCHASE
	            LEFT OUTER JOIN DZSN_SA_PROJECTH G ON G.NO_PROJECT = L.CD_PJT AND G.CD_COMPANY = L.CD_COMPANY

	            LEFT OUTER JOIN DZSN_MA_SL S ON S.CD_COMPANY = @P_CD_COMPANY
	                                                            AND S.CD_BIZAREA = P.CD_BIZAREA
	                                                            AND S.CD_PLANT = L.CD_PLANT
	                                 AND S.CD_SL = L.CD_SL
	            LEFT OUTER JOIN DZSN_MM_EJTP J ON J.CD_COMPANY = @P_CD_COMPANY
	                                                            AND J.CD_QTIOTP = L.FG_RCV
	            LEFT OUTER JOIN DZSN_MA_CODEDTL K ON K.CD_COMPANY = @P_CD_COMPANY
	                                                                    AND K.CD_FIELD = 'PU_C000009'        --오더상태
	                                                                    AND K.CD_SYSDEF = L.FG_POST

	            LEFT OUTER JOIN DZSN_SA_PROJECTH PJ ON PJ.NO_PROJECT = L.CD_PJT AND PJ.CD_COMPANY = L.CD_COMPANY

                LEFT OUTER JOIN (
						                SELECT CD_SL,ISNULL(SUM(QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR+QT_TRANS_GR), 0) 
								                - ISNULL(SUM(QT_GOOD_GI + QT_REJECT_GI +QT_INSP_GI+QT_TRANS_GI), 0) QT_INV
						                FROM  MM_OHSLINVM
						                WHERE CD_COMPANY = @P_CD_COMPANY
								                AND YY_INV = @DATE_YYYY
								                AND CD_PLANT = @P_CD_PLANT
								                AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
						                GROUP BY CD_SL
						                UNION ALL
						                SELECT CD_SL, QT_GOOD_INV QT_INV
						                FROM MM_OPENQTL
						                WHERE CD_COMPANY = @P_CD_COMPANY
								                AND CD_PLANT = @P_CD_PLANT
								                AND YM_STANDARD = @DATE_YYYY
								                AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
                                    ) CUR_INV ON 
								                CUR_INV.CD_SL = M.CD_SL

     LEFT OUTER JOIN DZSN_MA_SL MS ON MS.CD_SL = M.CD_SL AND MS.CD_PLANT = M.CD_PLANT AND MS.CD_COMPANY = M.CD_COMPANY AND MS.CD_BIZAREA = M.CD_BIZAREA
     LEFT OUTER JOIN DZSN_MA_CC MC ON L.CD_CC = MC.CD_CC AND MC.CD_COMPANY = L.CD_COMPANY 
     LEFT OUTER JOIN PU_PRL PRL ON PRL.NO_PR = L.NO_PR AND PRL.NO_PRLINE = L.NO_PRLINE AND PRL.CD_COMPANY = L.CD_COMPANY
	 LEFT OUTER JOIN DZSN_MA_PURGRP G1 ON PRL.CD_PURGRP = G1.CD_PURGRP  AND PRL.CD_COMPANY = G1.CD_COMPANY 	
	 LEFT OUTER JOIN PU_PRH PRH ON PRH.CD_COMPANY = PRL.CD_COMPANY AND PRH.NO_PR = PRL.NO_PR  AND PRH.CD_PLANT  = PRL.CD_PLANT -- 요청부서, 요청자조회조건때문에추가
	 LEFT OUTER JOIN  DZSN_MA_EMP E1 ON PRH.NO_EMP= E1.NO_EMP AND  E1.CD_COMPANY =@P_CD_COMPANY 
	 LEFT OUTER JOIN DZSN_MA_DEPT DE ON PRH.CD_DEPT = DE.CD_DEPT AND @P_CD_COMPANY = DE.CD_COMPANY  
	
	 LEFT OUTER JOIN PU_APPL AP ON AP.CD_COMPANY = L.CD_COMPANY AND AP.NO_APP = L.NO_APP AND AP.NO_APPLINE = L.NO_APPLINE
	 LEFT OUTER JOIN SA_SOL SL ON SL.CD_COMPANY = H.CD_COMPANY 
								 AND  SL.NO_SO = (CASE  WHEN @V_TEST  = '100' THEN      PRL.NO_SO    
												        WHEN @V_TEST  = '200' THEN      L.NO_SO    
												        WHEN @V_TEST  = '500' THEN      AP.NO_SO  END ) 
						
								 AND SL.SEQ_SO = (CASE  WHEN @V_TEST  = '100'	 THEN      PRL.NO_SOLINE    
														WHEN @V_TEST  = '200'   THEN        L.NO_SOLINE    
												        WHEN @V_TEST  = '500' THEN         AP.NO_SOLINE  END )  
						
	LEFT OUTER JOIN SA_SOH SH ON SL.NO_SO = SH.NO_SO AND SL.CD_COMPANY = SH.CD_COMPANY 
	LEFT OUTER JOIN DZSN_MA_PARTNER PT	ON	SH.CD_COMPANY = PT.CD_COMPANY AND SH.CD_PARTNER = PT.CD_PARTNER 
    LEFT OUTER JOIN DZSN_MA_PARTNER  MP ON SL.CD_COMPANY = MP.CD_COMPANY AND SL.GI_PARTNER = MP.CD_PARTNER  
	LEFT OUTER JOIN SA_PROJECTL SPL ON L.CD_COMPANY = SPL.CD_COMPANY AND L.CD_PJT = SPL.NO_PROJECT AND L.SEQ_PROJECT = SPL.SEQ_PROJECT    	  
	LEFT JOIN DZSN_MA_PITEM I2 ON SPL.CD_COMPANY = I2.CD_COMPANY AND SPL.CD_PLANT = I2.CD_PLANT AND SPL.CD_ITEM = I2.CD_ITEM    
	LEFT OUTER JOIN MM_QTIOH QH ON QH.CD_COMPANY = Q.CD_COMPANY AND QH.NO_IO = Q.NO_IO
    --LEFT OUTER JOIN MA_PARTNER_HKCOS_V MV ON MV.CD_COMPANY = PT.CD_COMPANY AND MV.WMS2 = PT.CD_PARTNER  
	LEFT OUTER JOIN DZSN_MA_ITEMGRP MI ON M.GRP_ITEM = MI.CD_ITEMGRP AND M.CD_COMPANY = MI.CD_COMPANY
	LEFT OUTER JOIN DZSN_MA_CODEDTL MC2 ON MC2.CD_COMPANY = M.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000066' AND MC2.CD_SYSDEF = M.GRP_MFG  
	LEFT OUTER JOIN DZSN_MA_PARTNER MPA ON MPA.CD_COMPANY = M.CD_COMPANY AND MPA.CD_PARTNER = M.PARTNER
	  
            WHERE H.CD_COMPANY = @P_CD_COMPANY
	            AND P.CD_BIZAREA = @P_CD_BIZAREA
	            AND (L.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)
	            AND ((@P_TYPE_DT = '001' AND (H.DT_PO BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)) 
	                    OR (@P_TYPE_DT = '002' AND (L.DT_LIMIT BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)))
	            --AND (L.FG_POST = @P_FG_POST OR @P_FG_POST = '' OR @P_FG_POST IS NULL)
	            AND (L.FG_POST IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_FG_POST)) OR @P_FG_POST = '' OR @P_FG_POST IS NULL)    
	            AND (H.FG_TRANS = @P_FG_TRANS OR @P_FG_TRANS = '' OR @P_FG_TRANS IS NULL)
	            AND (H.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PURGRP)) OR @P_CD_PURGRP = '' OR @P_CD_PURGRP IS NULL)
	            AND (H.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)
	            --@P_TYPE_UNIT
	            AND ((@P_YN_RCV = '' OR @P_YN_RCV IS NULL OR @P_YN_RCV = '001')
	                    OR (@P_YN_RCV = '002' AND L.QT_PO_MM - L.QT_RCV_MM = 0)
	                    OR (@P_YN_RCV = '003' AND L.QT_PO_MM - L.QT_RCV_MM > 0))
	            AND H.CD_PARTNER = @P_CONDITION
	            AND (L.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)	
                AND ((L.CD_CC = @P_CD_CC) OR (@P_CD_CC = '') OR (@P_CD_CC IS NULL))   
				AND  (( PRL.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_REQ_PURGRP))) OR (@P_REQ_PURGRP = '' ) OR (@P_REQ_PURGRP  IS NULL)) --요청구매그룹  
				AND (( PRH.CD_DEPT  =@P_REQ_DEPT) OR (@P_REQ_DEPT = '' ) OR (@P_REQ_DEPT  IS NULL)) --요청부서
				AND (( PRH.NO_EMP  =@P_REQ_EMP) OR (@P_REQ_EMP = '' ) OR (@P_REQ_EMP  IS NULL)) -- 요청인원
				AND (( L.NO_PO LIKE '%' + @P_NO_PO + '%') OR (@P_NO_PO = '') OR (@P_NO_PO IS NULL)) 
				AND ((A.CD_PARTNER = @P_NO_PARTNER) OR (@P_NO_PARTNER = '') OR (@P_NO_PARTNER IS NULL))
				AND (@P_CD_PJT = '' OR @P_CD_PJT IS NULL OR @P_CD_PJT = L.CD_PJT) 
				AND (@P_CD_ITEM = '' OR @P_CD_ITEM IS NULL OR @P_CD_ITEM = L.CD_ITEM)
            GROUP BY H.NO_PO, H.CD_PLANT, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, 
	            H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, H.DC50_PO, H.TP_PROCESS, 
	            H.FG_TAXP, H.YN_AM, H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	            E.NM_SYSDEF, F.NM_TP, G.NM_PROJECT, 
	            L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	            L.CD_UNIT_MM, MC.NM_CC,
	            L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 
	            L.QT_PO_MM, L.QT_PO, L.QT_REQ_MM, L.QT_REQ, L.QT_RCV_MM, L.QT_RCV, L.QT_RETURN_MM, L.QT_RETURN, 
	            L.QT_TR_MM, L.QT_TR, 
	            L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	            L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	            L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	            L.NO_APPLINE, L.DC1, L.DC2, 
	            L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 
	            M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF, S.NM_SL, J.NM_QTIOTP, CUR_INV.QT_INV,
				PRL.CD_PURGRP ,
				G1.NM_PURGRP, 
				E1.NM_KOR ,
				DE.NM_DEPT ,
			    PRL.NO_SO   , --요청
			    L.NO_SO ,    --발주
		        AP.NO_SO ,   --품의
			    MP.LN_PARTNER,  
				PT.LN_PARTNER,  --2011.09.23 거래처명 추가 이장원
				M.NO_MODEL, --2011.10.12 모델코드추가 이장원
			    L.CD_PJT,
				L.SEQ_PROJECT,
				SPL.CD_ITEM, --프로젝트 품목코드  
				I2.NM_ITEM, --프로젝트 품목명 
				I2.STND_ITEM,
				H.DT_WEB,
				H.FG_WEB,
				L.DT_PLAN,
				M.UNIT_PO,
				M.NO_DESIGN,
				I2.NO_DESIGN,
				H.DC_RMK2,
				H.NO_ORDER,
				L.CD_SL,
				S.NM_SL,
				M.GRP_ITEM, 
				MI.NM_ITEMGRP, 
				M.GRP_MFG, 
				MC2.NM_SYSDEF, 
				M.STND_DETAIL_ITEM, 
				M.MAT_ITEM, 
				MPA.LN_PARTNER
            ORDER BY H.DT_PO
    END
ELSE IF (@P_TAB_NAME = 'CD_ITEM')                --품목별
    BEGIN
        SELECT 'N' CHK, 
	        --HEADER 내역-------------------------
	        H.NO_PO, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, MC.NM_CC,
	        H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, H.DC50_PO DC_RMK, H.TP_PROCESS, 
	        H.FG_TAXP, 
	        CASE WHEN H.YN_AM = 'Y' THEN @V_STR_CONV1 ELSE @V_STR_CONV2 END YN_AM, 
	        H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	        E.NM_SYSDEF NM_TRANS, F.NM_TP NM_PURCHASE, 

	        --LINE 내역-------------------------
	        L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	        L.CD_UNIT_MM, 
	        L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 

	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO END QT_PO, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_REQ_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_REQ END QT_REQ, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RCV END QT_GR, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RETURN_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RETURN END QT_RETURN, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_TR_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_TR END QT_TR, 
	        --CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(ISNULL(Q.QT_CLS_MM, 0)) WHEN @P_TYPE_UNIT = '002' THEN SUM(ISNULL(Q.QT_CLS, 0)) END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS_MM, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS_MM, 0)
																				 END)
				 WHEN @P_TYPE_UNIT = '002' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS, 0)
																				 END)
			END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM - L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO - L.QT_RCV END QT_REMAIN, 

	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, G.NM_PROJECT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2, 
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 

	        M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF NM_POST, S.NM_SL, J.NM_QTIOTP NM_FG_RCV, CUR_INV.QT_INV,
	        PRL.CD_PURGRP AS REQPURGRP,
			G1.NM_PURGRP AS NM_REQ_PURGRP, 
			E1.NM_KOR AS REQ_NM_KOR,
			DE.NM_DEPT AS REQ_NM_DEPT,
		    ISNULL(MAX(PU_REV.QT_REV_MM), 0) AS QT_REV_MM,
		    M.NO_MODEL, --2011.10.12 모델코드추가 이장원
			L.SEQ_PROJECT,
			SPL.CD_ITEM AS CD_PJT_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM AS NM_PJT_ITEM, --프로젝트 품목명 
			I2.STND_ITEM AS PJT_ITEM_STND,
			H.DT_WEB,
			H.FG_WEB,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN AS NO_PJT_DESIGN,
			H.DC_RMK2,
			H.NO_ORDER,
			L.CD_SL,
			S.NM_SL,
		    ISNULL(MAX(PU_REV.QT_PASS_MM), 0) AS QT_PASS_MM,
		    ISNULL(MAX(PU_REV.QT_BAD_MM), 0) AS QT_BAD_MM

        FROM PU_POH H
	        INNER JOIN PU_POL L ON L.CD_COMPANY = @P_CD_COMPANY
	                                        AND L.NO_PO = H.NO_PO
	        INNER JOIN DZSN_MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY
	                                                AND P.CD_PLANT = H.CD_PLANT

        LEFT OUTER JOIN ( SELECT CD_COMPANY, NO_PO, NO_POLINE AS NO_LINE,
                                 SUM(ISNULL(QT_REV_MM, 0)) AS QT_REV_MM,
                                 SUM(ISNULL(QT_PASS_MM, 0)) AS QT_PASS_MM,
                                 SUM(ISNULL(QT_BAD_MM, 0)) AS QT_BAD_MM
                            FROM PU_REV
                           WHERE ISNULL(YN_APPRO,'002') = '002' 
                           GROUP BY CD_COMPANY, NO_PO, NO_POLINE ) AS PU_REV
                     ON PU_REV.CD_COMPANY = L.CD_COMPANY
                    AND PU_REV.NO_PO = L.NO_PO
                    AND PU_REV.NO_LINE = L.NO_LINE

	        LEFT OUTER JOIN MM_QTIO Q ON Q.CD_COMPANY = @P_CD_COMPANY
	                                          AND Q.NO_PSO_MGMT = L.NO_PO
	                                                        AND Q.NO_PSOLINE_MGMT = L.NO_LINE
	                                                        AND Q.FG_IO IN ('001', '030')
	        LEFT OUTER JOIN DZSN_MA_PITEM M ON M.CD_COMPANY = @P_CD_COMPANY
	                                                        AND M.CD_PLANT = L.CD_PLANT
	                                                        AND M.CD_ITEM = L.CD_ITEM
	        LEFT OUTER JOIN DZSN_MA_PARTNER A ON A.CD_COMPANY  = @P_CD_COMPANY
	                                                                AND A.CD_PARTNER = H.CD_PARTNER
	        LEFT OUTER JOIN DZSN_MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY
	                                                        AND B.NO_EMP = H.NO_EMP
	        LEFT OUTER JOIN DZSN_MA_PURGRP C ON C.CD_COMPANY = @P_CD_COMPANY
	                                                                AND C.CD_PURGRP = H.CD_PURGRP
	        LEFT OUTER JOIN DZSN_PU_TPPO D ON D.CD_COMPANY = @P_CD_COMPANY
	                                                        AND D.CD_TPPO = H.CD_TPPO
	        LEFT OUTER JOIN DZSN_MA_CODEDTL E ON E.CD_COMPANY = @P_CD_COMPANY
	                                                                AND E.CD_FIELD = 'PU_C000016'        --거래구분
	                                                                AND E.CD_SYSDEF = H.FG_TRANS
	        LEFT OUTER JOIN DZSN_MA_AISPOSTH F ON F.CD_COMPANY = @P_CD_COMPANY
	                                                                AND F.FG_TP = '200'        --매입형태
	                                                                AND F.CD_TP = L.FG_PURCHASE
	        LEFT OUTER JOIN DZSN_SA_PROJECTH G ON G.NO_PROJECT = L.CD_PJT AND G.CD_COMPANY = L.CD_COMPANY

	        LEFT OUTER JOIN DZSN_MA_SL S ON S.CD_COMPANY = @P_CD_COMPANY
	                                                        AND S.CD_BIZAREA = P.CD_BIZAREA
	                                                        AND S.CD_PLANT = L.CD_PLANT
	                                                        AND S.CD_SL = L.CD_SL
	        LEFT OUTER JOIN DZSN_MM_EJTP J ON J.CD_COMPANY = @P_CD_COMPANY
	                                                        AND J.CD_QTIOTP = L.FG_RCV
	        LEFT OUTER JOIN DZSN_MA_CODEDTL K ON K.CD_COMPANY = @P_CD_COMPANY
	                                                                AND K.CD_FIELD = 'PU_C000009'        --오더상태
	                                                                AND K.CD_SYSDEF = L.FG_POST
	        LEFT OUTER JOIN DZSN_SA_PROJECTH PJ ON PJ.NO_PROJECT = L.CD_PJT AND PJ.CD_COMPANY = L.CD_COMPANY

            LEFT OUTER JOIN (
						        SELECT CD_SL,ISNULL(SUM(QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR+QT_TRANS_GR), 0) 
								        - ISNULL(SUM(QT_GOOD_GI + QT_REJECT_GI +QT_INSP_GI+QT_TRANS_GI), 0) QT_INV
						        FROM  MM_OHSLINVM
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND YY_INV = @DATE_YYYY
								        AND CD_PLANT = @P_CD_PLANT
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
						        GROUP BY CD_SL
						        UNION ALL
						        SELECT CD_SL, QT_GOOD_INV QT_INV
						        FROM MM_OPENQTL
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND CD_PLANT = @P_CD_PLANT
								        AND YM_STANDARD = @DATE_YYYY
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
                            ) CUR_INV ON 
								        CUR_INV.CD_SL = M.CD_SL

	LEFT OUTER JOIN DZSN_MA_SL MS ON MS.CD_SL = M.CD_SL AND MS.CD_PLANT = M.CD_PLANT AND MS.CD_COMPANY = M.CD_COMPANY AND MS.CD_BIZAREA = M.CD_BIZAREA
    LEFT OUTER JOIN DZSN_MA_CC MC ON L.CD_CC = MC.CD_CC AND MC.CD_COMPANY = L.CD_COMPANY 
    LEFT OUTER JOIN PU_PRL PRL ON PRL.NO_PR = L.NO_PR AND PRL.NO_PRLINE = L.NO_PRLINE AND PRL.CD_COMPANY = L.CD_COMPANY
	LEFT OUTER JOIN DZSN_MA_PURGRP G1 ON PRL.CD_PURGRP = G1.CD_PURGRP  AND PRL.CD_COMPANY = G1.CD_COMPANY 	
	LEFT OUTER JOIN PU_PRH PRH ON PRH.CD_COMPANY = PRL.CD_COMPANY AND PRH.NO_PR = PRL.NO_PR  AND PRH.CD_PLANT  = PRL.CD_PLANT -- 요청부서, 요청자조회조건때문에추가
	LEFT OUTER JOIN  DZSN_MA_EMP E1 ON PRH.NO_EMP= E1.NO_EMP AND  E1.CD_COMPANY =@P_CD_COMPANY 
	LEFT OUTER JOIN DZSN_MA_DEPT DE ON PRH.CD_DEPT = DE.CD_DEPT AND @P_CD_COMPANY = DE.CD_COMPANY   	
	LEFT OUTER JOIN SA_PROJECTL SPL ON L.CD_COMPANY = SPL.CD_COMPANY AND L.CD_PJT = SPL.NO_PROJECT AND L.SEQ_PROJECT = SPL.SEQ_PROJECT    	  
	LEFT JOIN DZSN_MA_PITEM I2 ON SPL.CD_COMPANY = I2.CD_COMPANY AND SPL.CD_PLANT = I2.CD_PLANT AND SPL.CD_ITEM = I2.CD_ITEM    
	LEFT OUTER JOIN MM_QTIOH QH ON QH.CD_COMPANY = Q.CD_COMPANY AND QH.NO_IO = Q.NO_IO
        WHERE H.CD_COMPANY = @P_CD_COMPANY
	        AND P.CD_BIZAREA = @P_CD_BIZAREA
	        AND (L.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)
	        AND ((@P_TYPE_DT = '001' AND (H.DT_PO BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)) 
	                OR (@P_TYPE_DT = '002' AND (L.DT_LIMIT BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)))
	        --AND (L.FG_POST = @P_FG_POST OR @P_FG_POST = '' OR @P_FG_POST IS NULL)
	        AND (L.FG_POST IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_FG_POST)) OR @P_FG_POST = '' OR @P_FG_POST IS NULL)    
	        AND (H.FG_TRANS = @P_FG_TRANS OR @P_FG_TRANS = '' OR @P_FG_TRANS IS NULL)
	        AND (H.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PURGRP)) OR @P_CD_PURGRP = '' OR @P_CD_PURGRP IS NULL)
	        AND (H.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)
	        --@P_TYPE_UNIT
	        AND ((@P_YN_RCV = '' OR @P_YN_RCV IS NULL OR @P_YN_RCV = '001')
	                OR (@P_YN_RCV = '002' AND L.QT_PO_MM - L.QT_RCV_MM = 0)
	                OR (@P_YN_RCV = '003' AND L.QT_PO_MM - L.QT_RCV_MM > 0))
	        AND L.CD_ITEM = @P_CONDITION
	        AND (L.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)	
            AND ((L.CD_CC = @P_CD_CC) OR (@P_CD_CC = '') OR (@P_CD_CC IS NULL))   
			AND  (( PRL.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_REQ_PURGRP))) OR (@P_REQ_PURGRP = '' ) OR (@P_REQ_PURGRP  IS NULL)) --요청구매그룹  
			AND (( PRH.CD_DEPT  =@P_REQ_DEPT) OR (@P_REQ_DEPT = '' ) OR (@P_REQ_DEPT  IS NULL)) --요청부서
			AND (( PRH.NO_EMP  =@P_REQ_EMP) OR (@P_REQ_EMP = '' ) OR (@P_REQ_EMP  IS NULL)) -- 요청인원
			AND (( L.NO_PO LIKE '%' + @P_NO_PO + '%') OR (@P_NO_PO = '') OR (@P_NO_PO IS NULL))
			AND ((A.CD_PARTNER = @P_NO_PARTNER) OR (@P_NO_PARTNER = '') OR (@P_NO_PARTNER IS NULL)) 
			AND (@P_CD_PJT = '' OR @P_CD_PJT IS NULL OR @P_CD_PJT = L.CD_PJT) 
			AND (@P_CD_ITEM = '' OR @P_CD_ITEM IS NULL OR @P_CD_ITEM = L.CD_ITEM)
        GROUP BY H.NO_PO, H.CD_PLANT, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, 
	        H.TP_UM_TAX,H.CD_EXCH, H.RT_EXCH, H.DC50_PO, H.TP_PROCESS, 
	        H.FG_TAXP, H.YN_AM, H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	        E.NM_SYSDEF, F.NM_TP, G.NM_PROJECT, 
	        L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	        L.CD_UNIT_MM, 
	        L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT,MC.NM_CC, 
	        L.QT_PO_MM, L.QT_PO, L.QT_REQ_MM, L.QT_REQ, L.QT_RCV_MM, L.QT_RCV, L.QT_RETURN_MM, L.QT_RETURN, 
	        L.QT_TR_MM, L.QT_TR, 
	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2, 
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 
	        M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF, S.NM_SL, J.NM_QTIOTP, CUR_INV.QT_INV,
	        PRL.CD_PURGRP ,
			G1.NM_PURGRP, 
			E1.NM_KOR ,
			DE.NM_DEPT ,
			M.NO_MODEL, --2011.10.12 모델코드추가 이장원
			L.SEQ_PROJECT,
			SPL.CD_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM, --프로젝트 품목명 
			I2.STND_ITEM,
			H.DT_WEB,
			H.FG_WEB,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN,
			H.DC_RMK2,
			H.NO_ORDER,
			L.CD_SL,
			S.NM_SL
			
			
        ORDER BY H.DT_PO
    END
ELSE IF (@P_TAB_NAME = 'PROJECT')                --프로젝트별
    BEGIN
        SELECT 'N' CHK, 
	        --HEADER 내역-------------------------
	        H.NO_PO, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT,MC.NM_CC, 
	        H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, H.DC50_PO DC_RMK, H.TP_PROCESS, 
	        H.FG_TAXP, 
	        CASE WHEN H.YN_AM = 'Y' THEN @V_STR_CONV1 ELSE @V_STR_CONV2 END YN_AM, 
	        H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	        E.NM_SYSDEF NM_TRANS, F.NM_TP NM_PURCHASE,

	        --LINE 내역-------------------------
	        L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	        L.CD_UNIT_MM, 
	        L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 

	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO END QT_PO, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_REQ_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_REQ END QT_REQ, 
			CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RCV END QT_GR, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RETURN_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RETURN END QT_RETURN, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_TR_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_TR END QT_TR, 
	        --CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(ISNULL(Q.QT_CLS_MM, 0)) WHEN @P_TYPE_UNIT = '002' THEN SUM(ISNULL(Q.QT_CLS, 0)) END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS_MM, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS_MM, 0)
																				 END)
				 WHEN @P_TYPE_UNIT = '002' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS, 0)
																				 END)
			END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM - L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO - L.QT_RCV END QT_REMAIN, 

	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ,L.CD_PJT, G.NM_PROJECT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2, 
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 
	        M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF NM_POST, S.NM_SL, J.NM_QTIOTP NM_FG_RCV, CUR_INV.QT_INV,
	        PRL.CD_PURGRP AS REQPURGRP,
			G1.NM_PURGRP AS NM_REQ_PURGRP, 
			E1.NM_KOR AS REQ_NM_KOR,
			DE.NM_DEPT AS REQ_NM_DEPT	,
			ISNULL(MAX(PU_REV.QT_REV_MM), 0) AS QT_REV_MM,
			M.NO_MODEL, --2011.10.12 모델코드추가 이장원
			L.SEQ_PROJECT,
			SPL.CD_ITEM AS CD_PJT_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM AS NM_PJT_ITEM, --프로젝트 품목명 
			I2.STND_ITEM AS PJT_ITEM_STND,
			H.DT_WEB,
			H.FG_WEB,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN AS NO_PJT_DESIGN,
			H.DC_RMK2,
			H.NO_ORDER,
			M.GRP_ITEM, 
		    MI.NM_ITEMGRP, 
		    M.GRP_MFG, 
		    MC2.NM_SYSDEF AS NM_GRP_MFG, 
		    M.STND_DETAIL_ITEM, 
		    M.MAT_ITEM, 
		    MPA.LN_PARTNER AS NM_PARTNER,
		    ISNULL(MAX(PU_REV.QT_PASS_MM), 0) AS QT_PASS_MM,
		    ISNULL(MAX(PU_REV.QT_BAD_MM), 0) AS QT_BAD_MM,
		    L.AM - ISNULL(SUM(Q.AM),0) AS AM_GR_REMAIN
	        
        FROM PU_POH H
	        INNER JOIN PU_POL L ON L.CD_COMPANY = @P_CD_COMPANY
	                                        AND L.NO_PO = H.NO_PO
	        INNER JOIN DZSN_MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY
	                                                AND P.CD_PLANT = H.CD_PLANT

        LEFT OUTER JOIN ( SELECT CD_COMPANY, NO_PO, NO_POLINE AS NO_LINE,
                                 SUM(ISNULL(QT_REV_MM, 0)) AS QT_REV_MM,
                                 SUM(ISNULL(QT_PASS_MM, 0)) AS QT_PASS_MM,
                                 SUM(ISNULL(QT_BAD_MM, 0)) AS QT_BAD_MM
                            FROM PU_REV
                           WHERE ISNULL(FG_IO,'001') = '001' 
                           GROUP BY CD_COMPANY, NO_PO, NO_POLINE ) AS PU_REV
                     ON PU_REV.CD_COMPANY = L.CD_COMPANY
                    AND PU_REV.NO_PO = L.NO_PO
                    AND PU_REV.NO_LINE = L.NO_LINE

	        LEFT OUTER JOIN MM_QTIO Q ON Q.CD_COMPANY = @P_CD_COMPANY
	                                                        AND Q.NO_PSO_MGMT = L.NO_PO
	                                                        AND Q.NO_PSOLINE_MGMT = L.NO_LINE
	                                                        AND Q.FG_IO IN ('001', '030')
	        LEFT OUTER JOIN DZSN_MA_PITEM M ON M.CD_COMPANY = @P_CD_COMPANY
	                                                        AND M.CD_PLANT = L.CD_PLANT
	                                                        AND M.CD_ITEM = L.CD_ITEM
	        LEFT OUTER JOIN DZSN_MA_PARTNER A ON A.CD_COMPANY  = @P_CD_COMPANY
	                                                                AND A.CD_PARTNER = H.CD_PARTNER
	        LEFT OUTER JOIN DZSN_MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY
	                                                        AND B.NO_EMP = H.NO_EMP
	        LEFT OUTER JOIN DZSN_MA_PURGRP C ON C.CD_COMPANY = @P_CD_COMPANY
	                                                                AND C.CD_PURGRP = H.CD_PURGRP
	        LEFT OUTER JOIN DZSN_PU_TPPO D ON D.CD_COMPANY = @P_CD_COMPANY
	                        AND D.CD_TPPO = H.CD_TPPO
	        LEFT OUTER JOIN DZSN_MA_CODEDTL E ON E.CD_COMPANY = @P_CD_COMPANY
	                                                                AND E.CD_FIELD = 'PU_C000016'        --거래구분
	                                                                AND E.CD_SYSDEF = H.FG_TRANS
	        LEFT OUTER JOIN DZSN_MA_AISPOSTH F ON F.CD_COMPANY = @P_CD_COMPANY
	                                                                AND F.FG_TP = '200'        --매입형태
	                                                                AND F.CD_TP = L.FG_PURCHASE
	        LEFT OUTER JOIN DZSN_MA_SL S ON S.CD_COMPANY = @P_CD_COMPANY
	                                                        AND S.CD_BIZAREA = P.CD_BIZAREA
	                                                        AND S.CD_PLANT = L.CD_PLANT
	                                                        AND S.CD_SL = L.CD_SL
	        LEFT OUTER JOIN DZSN_MM_EJTP J ON J.CD_COMPANY = @P_CD_COMPANY
	                                                        AND J.CD_QTIOTP = L.FG_RCV
	        LEFT OUTER JOIN DZSN_MA_CODEDTL K ON K.CD_COMPANY = @P_CD_COMPANY
	                                                                AND K.CD_FIELD = 'PU_C000009'        --오더상태
	                                                                AND K.CD_SYSDEF = L.FG_POST
	        LEFT OUTER JOIN DZSN_SA_PROJECTH G ON G.NO_PROJECT = L.CD_PJT AND G.CD_COMPANY = L.CD_COMPANY

	        LEFT OUTER JOIN (
						        SELECT CD_SL,ISNULL(SUM(QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR+QT_TRANS_GR), 0) 
								        - ISNULL(SUM(QT_GOOD_GI + QT_REJECT_GI +QT_INSP_GI+QT_TRANS_GI), 0) QT_INV
						        FROM  MM_OHSLINVM
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND YY_INV = @DATE_YYYY
								        AND CD_PLANT = @P_CD_PLANT
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
						        GROUP BY CD_SL
						        UNION ALL
						        SELECT CD_SL, QT_GOOD_INV QT_INV
						        FROM MM_OPENQTL
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND CD_PLANT = @P_CD_PLANT
								        AND YM_STANDARD = @DATE_YYYY
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
                            ) CUR_INV ON 
								        CUR_INV.CD_SL = M.CD_SL

	        LEFT OUTER JOIN DZSN_MA_SL MS ON MS.CD_SL = M.CD_SL AND MS.CD_PLANT = M.CD_PLANT AND MS.CD_COMPANY = M.CD_COMPANY AND MS.CD_BIZAREA = M.CD_BIZAREA
            LEFT OUTER JOIN DZSN_MA_CC MC ON L.CD_CC = MC.CD_CC AND MC.CD_COMPANY = L.CD_COMPANY 
		    LEFT OUTER JOIN PU_PRL PRL ON PRL.NO_PR = L.NO_PR AND PRL.NO_PRLINE = L.NO_PRLINE AND PRL.CD_COMPANY = L.CD_COMPANY
			LEFT OUTER JOIN DZSN_MA_PURGRP G1 ON PRL.CD_PURGRP = G1.CD_PURGRP  AND PRL.CD_COMPANY = G1.CD_COMPANY 	
			LEFT OUTER JOIN PU_PRH PRH ON PRH.CD_COMPANY = PRL.CD_COMPANY AND PRH.NO_PR = PRL.NO_PR  AND PRH.CD_PLANT  = PRL.CD_PLANT -- 요청부서, 요청자조회조건때문에추가
			LEFT OUTER JOIN  DZSN_MA_EMP E1 ON PRH.NO_EMP= E1.NO_EMP AND  E1.CD_COMPANY =@P_CD_COMPANY 
			LEFT OUTER JOIN DZSN_MA_DEPT DE ON PRH.CD_DEPT = DE.CD_DEPT AND @P_CD_COMPANY = DE.CD_COMPANY  
			LEFT OUTER JOIN SA_PROJECTL SPL ON L.CD_COMPANY = SPL.CD_COMPANY AND L.CD_PJT = SPL.NO_PROJECT AND L.SEQ_PROJECT = SPL.SEQ_PROJECT    	  
			LEFT JOIN DZSN_MA_PITEM I2 ON SPL.CD_COMPANY = I2.CD_COMPANY AND SPL.CD_PLANT = I2.CD_PLANT AND SPL.CD_ITEM = I2.CD_ITEM    
			LEFT OUTER JOIN MM_QTIOH QH ON QH.CD_COMPANY = Q.CD_COMPANY AND QH.NO_IO = Q.NO_IO
			LEFT OUTER JOIN DZSN_MA_ITEMGRP MI ON M.GRP_ITEM = MI.CD_ITEMGRP AND M.CD_COMPANY = MI.CD_COMPANY
			LEFT OUTER JOIN DZSN_MA_CODEDTL MC2 ON MC2.CD_COMPANY = M.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000066' AND MC2.CD_SYSDEF = M.GRP_MFG  
			LEFT OUTER JOIN DZSN_MA_PARTNER MPA ON MPA.CD_COMPANY = M.CD_COMPANY AND MPA.CD_PARTNER = M.PARTNER
        WHERE H.CD_COMPANY = @P_CD_COMPANY
	        AND P.CD_BIZAREA = @P_CD_BIZAREA
	        AND (L.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)
	        AND ((@P_TYPE_DT = '001' AND (H.DT_PO BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)) 
	                OR (@P_TYPE_DT = '002' AND (L.DT_LIMIT BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)))
	        --AND (L.FG_POST = @P_FG_POST OR @P_FG_POST = '' OR @P_FG_POST IS NULL)
	        AND (L.FG_POST IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_FG_POST)) OR @P_FG_POST = '' OR @P_FG_POST IS NULL)    
	        AND (H.FG_TRANS = @P_FG_TRANS OR @P_FG_TRANS = '' OR @P_FG_TRANS IS NULL)
	        AND (H.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PURGRP)) OR @P_CD_PURGRP = '' OR @P_CD_PURGRP IS NULL)
	        AND (H.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)
	        --@P_TYPE_UNIT
	        AND ((@P_YN_RCV = '' OR @P_YN_RCV IS NULL OR @P_YN_RCV = '001')
	                OR (@P_YN_RCV = '002' AND L.QT_PO_MM - L.QT_RCV_MM = 0)
	                OR (@P_YN_RCV = '003' AND L.QT_PO_MM - L.QT_RCV_MM > 0))
	        AND ISNULL(L.CD_PJT, '') = @P_CONDITION
	        AND (L.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)	
            AND ((L.CD_CC = @P_CD_CC) OR (@P_CD_CC = '') OR (@P_CD_CC IS NULL))   
			AND  (( PRL.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_REQ_PURGRP))) OR (@P_REQ_PURGRP = '' ) OR (@P_REQ_PURGRP  IS NULL)) --요청구매그룹  
			AND (( PRH.CD_DEPT  =@P_REQ_DEPT) OR (@P_REQ_DEPT = '' ) OR (@P_REQ_DEPT  IS NULL)) --요청부서
			AND (( PRH.NO_EMP  =@P_REQ_EMP) OR (@P_REQ_EMP = '' ) OR (@P_REQ_EMP  IS NULL)) -- 요청인원
			AND (( L.NO_PO LIKE '%' + @P_NO_PO + '%') OR (@P_NO_PO = '') OR (@P_NO_PO IS NULL))
			AND ((A.CD_PARTNER = @P_NO_PARTNER) OR (@P_NO_PARTNER = '') OR (@P_NO_PARTNER IS NULL)) 
			AND (@P_CD_PJT = '' OR @P_CD_PJT IS NULL OR @P_CD_PJT = L.CD_PJT) 
			AND (@P_CD_ITEM = '' OR @P_CD_ITEM IS NULL OR @P_CD_ITEM = L.CD_ITEM)
        GROUP BY H.NO_PO, H.CD_PLANT, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, 
	        H.TP_UM_TAX,  H.CD_EXCH, H.RT_EXCH, H.DC50_PO, H.TP_PROCESS, 
	        H.FG_TAXP, H.YN_AM, H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	        E.NM_SYSDEF, F.NM_TP,MC.NM_CC,
	        L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	        L.CD_UNIT_MM, 
	        L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 
	        L.QT_PO_MM, L.QT_PO, L.QT_REQ_MM, L.QT_REQ, L.QT_RCV_MM, L.QT_RCV, L.QT_RETURN_MM, L.QT_RETURN, 
	        L.QT_TR_MM, L.QT_TR, 
	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, G.NM_PROJECT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2, 
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 
	        M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF, S.NM_SL, J.NM_QTIOTP, CUR_INV.QT_INV,
			PRL.CD_PURGRP ,
			G1.NM_PURGRP, 
			E1.NM_KOR ,
			DE.NM_DEPT,
		    M.NO_MODEL, --2011.10.12 모델코드추가 이장원 
			L.SEQ_PROJECT,
			SPL.CD_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM, --프로젝트 품목명 
			I2.STND_ITEM,
			H.FG_WEB,
			H.DT_WEB,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN,
			H.DC_RMK2,
			H.NO_ORDER,
			M.GRP_ITEM, 
			MI.NM_ITEMGRP, 
			M.GRP_MFG, 
			MC2.NM_SYSDEF, 
			M.STND_DETAIL_ITEM, 
			M.MAT_ITEM, 
			MPA.LN_PARTNER
			
        ORDER BY H.DT_PO
    END
ELSE IF (@P_TAB_NAME = 'CD_PURGRP')        --구매그룹별
    BEGIN
        SELECT 'N' CHK, 
	        --HEADER 내역-------------------------
	        H.NO_PO, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, MC.NM_CC,
	        H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, H.DC50_PO DC_RMK, H.TP_PROCESS, 
	        H.FG_TAXP, 
	        CASE WHEN H.YN_AM = 'Y' THEN @V_STR_CONV1 ELSE @V_STR_CONV2 END YN_AM, 
	        H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	        E.NM_SYSDEF NM_TRANS, F.NM_TP NM_PURCHASE, G.NM_PROJECT, 

	        --LINE 내역-------------------------
	        L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	        L.CD_UNIT_MM, 
	        L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 

	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO END QT_PO, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_REQ_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_REQ END QT_REQ, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RCV END QT_GR, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RETURN_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RETURN END QT_RETURN, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_TR_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_TR END QT_TR, 
	        --CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(ISNULL(Q.QT_CLS_MM, 0)) WHEN @P_TYPE_UNIT = '002' THEN SUM(ISNULL(Q.QT_CLS, 0)) END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS_MM, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS_MM, 0)
																				 END)
				 WHEN @P_TYPE_UNIT = '002' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS, 0)
																				 END)
			END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM - L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO - L.QT_RCV END QT_REMAIN, 

	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, G.NM_PROJECT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2, 
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 

	        M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF NM_POST, S.NM_SL, J.NM_QTIOTP NM_FG_RCV, CUR_INV.QT_INV,
			PRL.CD_PURGRP AS REQPURGRP,
			G1.NM_PURGRP AS NM_REQ_PURGRP, 
			E1.NM_KOR AS REQ_NM_KOR,
			DE.NM_DEPT AS REQ_NM_DEPT,
			ISNULL(MAX(PU_REV.QT_REV_MM), 0) AS QT_REV_MM,
			M.NO_MODEL, --2011.10.12 모델코드추가 이장원
			L.SEQ_PROJECT,
		    SPL.CD_ITEM AS CD_PJT_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM AS NM_PJT_ITEM, --프로젝트 품목명 
			I2.STND_ITEM AS PJT_ITEM_STND,
			H.FG_WEB,
			H.DT_WEB,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN AS NO_PJT_DESIGN,
			H.DC_RMK2,
			H.NO_ORDER,
			M.GRP_ITEM, 
		    MI.NM_ITEMGRP, 
		    M.GRP_MFG, 
		    MC2.NM_SYSDEF AS NM_GRP_MFG, 
		    M.STND_DETAIL_ITEM, 
		    M.MAT_ITEM, 
		    MPA.LN_PARTNER AS NM_PARTNER,
		    ISNULL(MAX(PU_REV.QT_PASS_MM), 0) AS QT_PASS_MM,
		    ISNULL(MAX(PU_REV.QT_BAD_MM), 0) AS QT_BAD_MM

        FROM PU_POH H
	        INNER JOIN PU_POL L ON L.CD_COMPANY = @P_CD_COMPANY
	                                        AND L.NO_PO = H.NO_PO
	        INNER JOIN DZSN_MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY
	                                                AND P.CD_PLANT = H.CD_PLANT

            LEFT OUTER JOIN ( SELECT CD_COMPANY, NO_PO, NO_POLINE AS NO_LINE,
                                 SUM(ISNULL(QT_REV_MM, 0)) AS QT_REV_MM,
                                 SUM(ISNULL(QT_PASS_MM, 0)) AS QT_PASS_MM,
                                 SUM(ISNULL(QT_BAD_MM, 0)) AS QT_BAD_MM
                            FROM PU_REV
                           WHERE ISNULL(FG_IO,'001') = '001' 
                           GROUP BY CD_COMPANY, NO_PO, NO_POLINE ) AS PU_REV
                         ON PU_REV.CD_COMPANY = L.CD_COMPANY
                     AND PU_REV.NO_PO = L.NO_PO
                     AND PU_REV.NO_LINE = L.NO_LINE

	        LEFT OUTER JOIN MM_QTIO Q ON Q.CD_COMPANY = @P_CD_COMPANY
	                                                        AND Q.NO_PSO_MGMT = L.NO_PO
	                                                        AND Q.NO_PSOLINE_MGMT = L.NO_LINE
	                                                        AND Q.FG_IO IN ('001', '030')
	        LEFT OUTER JOIN DZSN_MA_PITEM M ON M.CD_COMPANY = @P_CD_COMPANY
	                                                        AND M.CD_PLANT = L.CD_PLANT
	                                                        AND M.CD_ITEM = L.CD_ITEM
	        LEFT OUTER JOIN DZSN_MA_PARTNER A ON A.CD_COMPANY  = @P_CD_COMPANY
	                                                                AND A.CD_PARTNER = H.CD_PARTNER
	        LEFT OUTER JOIN DZSN_MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY
	                                                        AND B.NO_EMP = H.NO_EMP
	        LEFT OUTER JOIN DZSN_MA_PURGRP C ON C.CD_COMPANY = @P_CD_COMPANY
	                                   AND C.CD_PURGRP = H.CD_PURGRP
	        LEFT OUTER JOIN DZSN_PU_TPPO D ON D.CD_COMPANY = @P_CD_COMPANY
	                                                        AND D.CD_TPPO = H.CD_TPPO
	        LEFT OUTER JOIN DZSN_MA_CODEDTL E ON E.CD_COMPANY = @P_CD_COMPANY
	                                                                AND E.CD_FIELD = 'PU_C000016'        --거래구분
	                                                                AND E.CD_SYSDEF = H.FG_TRANS
	        LEFT OUTER JOIN DZSN_MA_AISPOSTH F ON F.CD_COMPANY = @P_CD_COMPANY
	                                                                AND F.FG_TP = '200'        --매입형태
	                                                                AND F.CD_TP = L.FG_PURCHASE

	        LEFT OUTER JOIN DZSN_SA_PROJECTH G ON G.NO_PROJECT = L.CD_PJT AND G.CD_COMPANY = L.CD_COMPANY

	        LEFT OUTER JOIN DZSN_MA_SL S ON S.CD_COMPANY = @P_CD_COMPANY
	                                                        AND S.CD_BIZAREA = P.CD_BIZAREA
	                                                        AND S.CD_PLANT = L.CD_PLANT
	                                                        AND S.CD_SL = L.CD_SL
	        LEFT OUTER JOIN DZSN_MM_EJTP J ON J.CD_COMPANY = @P_CD_COMPANY
	                                                        AND J.CD_QTIOTP = L.FG_RCV
	        LEFT OUTER JOIN DZSN_MA_CODEDTL K ON K.CD_COMPANY = @P_CD_COMPANY
	                                                                AND K.CD_FIELD = 'PU_C000009'        --오더상태
	                                                                AND K.CD_SYSDEF = L.FG_POST

	        LEFT OUTER JOIN (
						        SELECT CD_SL,ISNULL(SUM(QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR+QT_TRANS_GR), 0) 
								        - ISNULL(SUM(QT_GOOD_GI + QT_REJECT_GI +QT_INSP_GI+QT_TRANS_GI), 0) QT_INV
						        FROM  MM_OHSLINVM
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND YY_INV = @DATE_YYYY
								        AND CD_PLANT = @P_CD_PLANT
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
						        GROUP BY CD_SL
						        UNION ALL
						        SELECT CD_SL, QT_GOOD_INV QT_INV
						        FROM MM_OPENQTL
						        WHERE CD_COMPANY = @P_CD_COMPANY
								      AND CD_PLANT = @P_CD_PLANT
								        AND YM_STANDARD = @DATE_YYYY
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
                            ) CUR_INV ON 
								        CUR_INV.CD_SL = M.CD_SL

	        LEFT OUTER JOIN DZSN_MA_SL MS ON MS.CD_SL = M.CD_SL AND MS.CD_PLANT = M.CD_PLANT AND MS.CD_COMPANY = M.CD_COMPANY AND MS.CD_BIZAREA = M.CD_BIZAREA
            LEFT OUTER JOIN DZSN_MA_CC MC ON L.CD_CC = MC.CD_CC AND MC.CD_COMPANY = L.CD_COMPANY
            LEFT OUTER JOIN PU_PRL PRL ON PRL.NO_PR = L.NO_PR AND PRL.NO_PRLINE = L.NO_PRLINE AND PRL.CD_COMPANY = L.CD_COMPANY
			LEFT OUTER JOIN DZSN_MA_PURGRP G1 ON PRL.CD_PURGRP = G1.CD_PURGRP  AND PRL.CD_COMPANY = G1.CD_COMPANY 	
			LEFT OUTER JOIN PU_PRH PRH ON PRH.CD_COMPANY = PRL.CD_COMPANY AND PRH.NO_PR = PRL.NO_PR  AND PRH.CD_PLANT  = PRL.CD_PLANT -- 요청부서, 요청자조회조건때문에추가
			LEFT OUTER JOIN  DZSN_MA_EMP E1 ON PRH.NO_EMP= E1.NO_EMP AND  E1.CD_COMPANY =@P_CD_COMPANY 
			LEFT OUTER JOIN DZSN_MA_DEPT DE ON PRH.CD_DEPT = DE.CD_DEPT AND @P_CD_COMPANY = DE.CD_COMPANY  	
			LEFT OUTER JOIN SA_PROJECTL SPL ON L.CD_COMPANY = SPL.CD_COMPANY AND L.CD_PJT = SPL.NO_PROJECT AND L.SEQ_PROJECT = SPL.SEQ_PROJECT    	  
			LEFT JOIN DZSN_MA_PITEM I2 ON SPL.CD_COMPANY = I2.CD_COMPANY AND SPL.CD_PLANT = I2.CD_PLANT AND SPL.CD_ITEM = I2.CD_ITEM    
			LEFT OUTER JOIN MM_QTIOH QH ON QH.CD_COMPANY = Q.CD_COMPANY AND QH.NO_IO = Q.NO_IO
			LEFT OUTER JOIN DZSN_MA_ITEMGRP MI ON M.GRP_ITEM = MI.CD_ITEMGRP AND M.CD_COMPANY = MI.CD_COMPANY
			LEFT OUTER JOIN DZSN_MA_CODEDTL MC2 ON MC2.CD_COMPANY = M.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000066' AND MC2.CD_SYSDEF = M.GRP_MFG  
			LEFT OUTER JOIN DZSN_MA_PARTNER MPA ON MPA.CD_COMPANY = M.CD_COMPANY AND MPA.CD_PARTNER = M.PARTNER
        WHERE H.CD_COMPANY = @P_CD_COMPANY
	        AND P.CD_BIZAREA = @P_CD_BIZAREA
	        AND (L.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)
	        AND ((@P_TYPE_DT = '001' AND (H.DT_PO BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)) 
	                OR (@P_TYPE_DT = '002' AND (L.DT_LIMIT BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)))
	        --AND (L.FG_POST = @P_FG_POST OR @P_FG_POST = '' OR @P_FG_POST IS NULL)
	        AND (L.FG_POST IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_FG_POST)) OR @P_FG_POST = '' OR @P_FG_POST IS NULL)    
	        AND (H.FG_TRANS = @P_FG_TRANS OR @P_FG_TRANS = '' OR @P_FG_TRANS IS NULL)
	        AND (H.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PURGRP)) OR @P_CD_PURGRP = '' OR @P_CD_PURGRP IS NULL)
	        AND (H.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)
	        --@P_TYPE_UNIT
	        AND ((@P_YN_RCV = '' OR @P_YN_RCV IS NULL OR @P_YN_RCV = '001')
	                OR (@P_YN_RCV = '002' AND L.QT_PO_MM - L.QT_RCV_MM = 0)
	                OR (@P_YN_RCV = '003' AND L.QT_PO_MM - L.QT_RCV_MM > 0))
	        AND H.CD_PURGRP = @P_CONDITION
	        AND (L.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)	
            AND ((L.CD_CC = @P_CD_CC) OR (@P_CD_CC = '') OR (@P_CD_CC IS NULL))   
			AND  (( PRL.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_REQ_PURGRP))) OR (@P_REQ_PURGRP = '' ) OR (@P_REQ_PURGRP  IS NULL)) --요청구매그룹  
			AND (( PRH.CD_DEPT  =@P_REQ_DEPT) OR (@P_REQ_DEPT = '' ) OR (@P_REQ_DEPT  IS NULL)) --요청부서
			AND (( PRH.NO_EMP  =@P_REQ_EMP) OR (@P_REQ_EMP = '' ) OR (@P_REQ_EMP  IS NULL)) -- 요청인원
			AND (( L.NO_PO LIKE '%' + @P_NO_PO + '%') OR (@P_NO_PO = '') OR (@P_NO_PO IS NULL)) 
			AND ((A.CD_PARTNER = @P_NO_PARTNER) OR (@P_NO_PARTNER = '') OR (@P_NO_PARTNER IS NULL))
			AND (@P_CD_PJT = '' OR @P_CD_PJT IS NULL OR @P_CD_PJT = L.CD_PJT) 
			AND (@P_CD_ITEM = '' OR @P_CD_ITEM IS NULL OR @P_CD_ITEM = L.CD_ITEM)
        GROUP BY H.NO_PO, H.CD_PLANT, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, 
	        H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, H.DC50_PO, H.TP_PROCESS, 
	        H.FG_TAXP, H.YN_AM, H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	        E.NM_SYSDEF, F.NM_TP, G.NM_PROJECT, 
	        L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	        L.CD_UNIT_MM, MC.NM_CC,
	        L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 
	        L.QT_PO_MM, L.QT_PO, L.QT_REQ_MM, L.QT_REQ, L.QT_RCV_MM, L.QT_RCV, L.QT_RETURN_MM, L.QT_RETURN, 
	        L.QT_TR_MM, L.QT_TR, 
	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2, 
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 
	        M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF, S.NM_SL, J.NM_QTIOTP, CUR_INV.QT_INV,
	        PRL.CD_PURGRP ,
			G1.NM_PURGRP, 
			E1.NM_KOR ,
			DE.NM_DEPT,
			M.NO_MODEL, --2011.10.12 모델코드추가 이장원 
			L.SEQ_PROJECT,
			SPL.CD_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM, --프로젝트 품목명 
			I2.STND_ITEM,
			H.FG_WEB,
			H.DT_WEB,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN,
			H.DC_RMK2,
			H.NO_ORDER,
			M.GRP_ITEM, 
			MI.NM_ITEMGRP, 
			M.GRP_MFG, 
			MC2.NM_SYSDEF, 
			M.STND_DETAIL_ITEM, 
			M.MAT_ITEM, 
			MPA.LN_PARTNER
			
        ORDER BY H.DT_PO
    END
ELSE IF (@P_TAB_NAME = 'TP_PU')                        --발주형태별
    BEGIN
        SELECT 'N' CHK, 
	        --HEADER 내역-------------------------
	        H.NO_PO, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, MC.NM_CC,
	        H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, H.DC50_PO DC_RMK, H.TP_PROCESS, 
	        H.FG_TAXP, 
	        CASE WHEN H.YN_AM = 'Y' THEN @V_STR_CONV1 ELSE @V_STR_CONV2 END YN_AM, 
	        H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	        E.NM_SYSDEF NM_TRANS, F.NM_TP NM_PURCHASE, 

	        --LINE 내역-------------------------
	        L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	        L.CD_UNIT_MM, 
	        L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 

	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO END QT_PO, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_REQ_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_REQ END QT_REQ, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RCV END QT_GR, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RETURN_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RETURN END QT_RETURN, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_TR_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_TR END QT_TR, 
	        --CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(ISNULL(Q.QT_CLS_MM, 0)) WHEN @P_TYPE_UNIT = '002' THEN SUM(ISNULL(Q.QT_CLS, 0)) END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS_MM, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS_MM, 0)
																				 END)
				 WHEN @P_TYPE_UNIT = '002' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS, 0)
																				 END)
			END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM - L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO - L.QT_RCV END QT_REMAIN, 

	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, G.NM_PROJECT,L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2, 
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 

	        M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF NM_POST, S.NM_SL, J.NM_QTIOTP NM_FG_RCV, CUR_INV.QT_INV,
	        PRL.CD_PURGRP AS REQPURGRP,
			G1.NM_PURGRP AS NM_REQ_PURGRP, 
			E1.NM_KOR AS REQ_NM_KOR,
			DE.NM_DEPT AS REQ_NM_DEPT,
		    ISNULL(MAX(PU_REV.QT_REV_MM), 0) AS QT_REV_MM,
		    M.NO_MODEL, --2011.10.12 모델코드추가 이장원
		    L.SEQ_PROJECT,
			SPL.CD_ITEM AS CD_PJT_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM AS NM_PJT_ITEM, --프로젝트 품목명 
			I2.STND_ITEM AS PJT_ITEM_STND,
			H.FG_WEB,
			H.DT_WEB,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN AS NO_PJT_DESIGN,
			H.DC_RMK2,
			H.NO_ORDER,
			M.GRP_ITEM, 
		    MI.NM_ITEMGRP, 
		    M.GRP_MFG, 
		    MC2.NM_SYSDEF AS NM_GRP_MFG, 
		    M.STND_DETAIL_ITEM, 
		    M.MAT_ITEM, 
		    MPA.LN_PARTNER AS NM_PARTNER,
		    ISNULL(MAX(PU_REV.QT_PASS_MM), 0) AS QT_PASS_MM,
		    ISNULL(MAX(PU_REV.QT_BAD_MM), 0) AS QT_BAD_MM
	        
        FROM PU_POH H
	        INNER JOIN PU_POL L ON L.CD_COMPANY = @P_CD_COMPANY
	                                        AND L.NO_PO = H.NO_PO
	        INNER JOIN DZSN_MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY
	                                                AND P.CD_PLANT = H.CD_PLANT

            LEFT OUTER JOIN ( SELECT CD_COMPANY, NO_PO, NO_POLINE AS NO_LINE,
									 SUM(ISNULL(QT_REV_MM, 0)) AS QT_REV_MM,
									 SUM(ISNULL(QT_PASS_MM, 0)) AS QT_PASS_MM,
									 SUM(ISNULL(QT_BAD_MM, 0)) AS QT_BAD_MM
                              FROM PU_REV
                             WHERE ISNULL(FG_IO,'001') = '001' 
                              GROUP BY CD_COMPANY, NO_PO, NO_POLINE ) AS PU_REV
                      ON PU_REV.CD_COMPANY = L.CD_COMPANY
                    AND PU_REV.NO_PO = L.NO_PO
                    AND PU_REV.NO_LINE = L.NO_LINE

	        LEFT OUTER JOIN MM_QTIO Q ON Q.CD_COMPANY = @P_CD_COMPANY
	                                                        AND Q.NO_PSO_MGMT = L.NO_PO
	                                                        AND Q.NO_PSOLINE_MGMT = L.NO_LINE
	                                                        AND Q.FG_IO IN ('001', '030')
	        LEFT OUTER JOIN DZSN_MA_PITEM M ON M.CD_COMPANY = @P_CD_COMPANY
	                                                        AND M.CD_PLANT = L.CD_PLANT
	                                                        AND M.CD_ITEM = L.CD_ITEM
	        LEFT OUTER JOIN DZSN_MA_PARTNER A ON A.CD_COMPANY  = @P_CD_COMPANY
	                                                        AND A.CD_PARTNER = H.CD_PARTNER
	        LEFT OUTER JOIN DZSN_MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY
	                                                        AND B.NO_EMP = H.NO_EMP
	        LEFT OUTER JOIN DZSN_MA_PURGRP C ON C.CD_COMPANY = @P_CD_COMPANY
	                                                        AND C.CD_PURGRP = H.CD_PURGRP
	        LEFT OUTER JOIN DZSN_PU_TPPO D ON D.CD_COMPANY = @P_CD_COMPANY
	                                                        AND D.CD_TPPO = H.CD_TPPO
	        LEFT OUTER JOIN DZSN_MA_CODEDTL E ON E.CD_COMPANY = @P_CD_COMPANY
	                                                                AND E.CD_FIELD = 'PU_C000016'        --거래구분
	                                                                AND E.CD_SYSDEF = H.FG_TRANS
	        LEFT OUTER JOIN DZSN_MA_AISPOSTH F ON F.CD_COMPANY = @P_CD_COMPANY
	                                                                AND F.FG_TP = '200'        --매입형태
	                                                                AND F.CD_TP = L.FG_PURCHASE

	        LEFT OUTER JOIN DZSN_SA_PROJECTH G ON G.NO_PROJECT = L.CD_PJT AND G.CD_COMPANY = L.CD_COMPANY

	        LEFT OUTER JOIN DZSN_MA_SL S ON S.CD_COMPANY = @P_CD_COMPANY
	                                                        AND S.CD_BIZAREA = P.CD_BIZAREA
	                                                        AND S.CD_PLANT = L.CD_PLANT
	                                                        AND S.CD_SL = L.CD_SL
	        LEFT OUTER JOIN DZSN_MM_EJTP J ON J.CD_COMPANY = @P_CD_COMPANY
	                                                        AND J.CD_QTIOTP = L.FG_RCV
	        LEFT OUTER JOIN DZSN_MA_CODEDTL K ON K.CD_COMPANY = @P_CD_COMPANY
	                                                                AND K.CD_FIELD = 'PU_C000009'        --오더상태
	                                                                AND K.CD_SYSDEF = L.FG_POST

	        LEFT OUTER JOIN (
						        SELECT CD_SL,ISNULL(SUM(QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR+QT_TRANS_GR), 0) 
								        - ISNULL(SUM(QT_GOOD_GI + QT_REJECT_GI +QT_INSP_GI+QT_TRANS_GI), 0) QT_INV
						  FROM  MM_OHSLINVM
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND YY_INV = @DATE_YYYY
								        AND CD_PLANT = @P_CD_PLANT
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
						        GROUP BY CD_SL
						        UNION ALL
						        SELECT CD_SL, QT_GOOD_INV QT_INV
						        FROM MM_OPENQTL
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND CD_PLANT = @P_CD_PLANT
								        AND YM_STANDARD = @DATE_YYYY
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
                            ) CUR_INV ON 
								        CUR_INV.CD_SL = M.CD_SL

	        LEFT OUTER JOIN DZSN_MA_SL MS ON MS.CD_SL = M.CD_SL AND MS.CD_PLANT = M.CD_PLANT AND MS.CD_COMPANY = M.CD_COMPANY AND MS.CD_BIZAREA = M.CD_BIZAREA
			LEFT OUTER JOIN DZSN_MA_CC MC ON L.CD_CC = MC.CD_CC AND MC.CD_COMPANY = L.CD_COMPANY 
			LEFT OUTER JOIN PU_PRL PRL ON PRL.NO_PR = L.NO_PR AND PRL.NO_PRLINE = L.NO_PRLINE AND PRL.CD_COMPANY = L.CD_COMPANY
			LEFT OUTER JOIN DZSN_MA_PURGRP G1 ON PRL.CD_PURGRP = G1.CD_PURGRP  AND PRL.CD_COMPANY = G1.CD_COMPANY 	
			LEFT OUTER JOIN PU_PRH PRH ON PRH.CD_COMPANY = PRL.CD_COMPANY AND PRH.NO_PR = PRL.NO_PR  AND PRH.CD_PLANT  = PRL.CD_PLANT -- 요청부서, 요청자조회조건때문에추가
			LEFT OUTER JOIN  DZSN_MA_EMP E1 ON PRH.NO_EMP= E1.NO_EMP AND  E1.CD_COMPANY =@P_CD_COMPANY 
			LEFT OUTER JOIN DZSN_MA_DEPT DE ON PRH.CD_DEPT = DE.CD_DEPT AND @P_CD_COMPANY = DE.CD_COMPANY  	
			LEFT OUTER JOIN SA_PROJECTL SPL ON L.CD_COMPANY = SPL.CD_COMPANY AND L.CD_PJT = SPL.NO_PROJECT AND L.SEQ_PROJECT = SPL.SEQ_PROJECT    	  
			LEFT JOIN DZSN_MA_PITEM I2 ON SPL.CD_COMPANY = I2.CD_COMPANY AND SPL.CD_PLANT = I2.CD_PLANT AND SPL.CD_ITEM = I2.CD_ITEM    
			LEFT OUTER JOIN MM_QTIOH QH ON QH.CD_COMPANY = Q.CD_COMPANY AND QH.NO_IO = Q.NO_IO
			LEFT OUTER JOIN DZSN_MA_ITEMGRP MI ON M.GRP_ITEM = MI.CD_ITEMGRP AND M.CD_COMPANY = MI.CD_COMPANY
			LEFT OUTER JOIN DZSN_MA_CODEDTL MC2 ON MC2.CD_COMPANY = M.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000066' AND MC2.CD_SYSDEF = M.GRP_MFG  
			LEFT OUTER JOIN DZSN_MA_PARTNER MPA ON MPA.CD_COMPANY = M.CD_COMPANY AND MPA.CD_PARTNER = M.PARTNER
        WHERE H.CD_COMPANY = @P_CD_COMPANY
	        AND P.CD_BIZAREA = @P_CD_BIZAREA
	        AND (L.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)
	        AND ((@P_TYPE_DT = '001' AND (H.DT_PO BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)) 
	                OR (@P_TYPE_DT = '002' AND (L.DT_LIMIT BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)))
	        --AND (L.FG_POST = @P_FG_POST OR @P_FG_POST = '' OR @P_FG_POST IS NULL)
	        AND (L.FG_POST IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_FG_POST)) OR @P_FG_POST = '' OR @P_FG_POST IS NULL)    
	        AND (H.FG_TRANS = @P_FG_TRANS OR @P_FG_TRANS = '' OR @P_FG_TRANS IS NULL)
	        AND (H.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PURGRP)) OR @P_CD_PURGRP = '' OR @P_CD_PURGRP IS NULL)
	        AND (H.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)
	        --@P_TYPE_UNIT
	        AND ((@P_YN_RCV = '' OR @P_YN_RCV IS NULL OR @P_YN_RCV = '001')
	                OR (@P_YN_RCV = '002' AND L.QT_PO_MM - L.QT_RCV_MM = 0)
	                OR (@P_YN_RCV = '003' AND L.QT_PO_MM - L.QT_RCV_MM > 0))
	        AND H.CD_TPPO = @P_CONDITION
	        AND (L.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)	
            AND ((L.CD_CC = @P_CD_CC) OR (@P_CD_CC = '') OR (@P_CD_CC IS NULL))   
            AND  (( PRL.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_REQ_PURGRP))) OR (@P_REQ_PURGRP = '' ) OR (@P_REQ_PURGRP  IS NULL)) --요청구매그룹  
			AND (( PRH.CD_DEPT  =@P_REQ_DEPT) OR (@P_REQ_DEPT = '' ) OR (@P_REQ_DEPT  IS NULL)) --요청부서
			AND (( PRH.NO_EMP  =@P_REQ_EMP) OR (@P_REQ_EMP = '' ) OR (@P_REQ_EMP  IS NULL)) -- 요청인원
			AND (( L.NO_PO LIKE '%' + @P_NO_PO + '%') OR (@P_NO_PO = '') OR (@P_NO_PO IS NULL))
			AND ((A.CD_PARTNER = @P_NO_PARTNER) OR (@P_NO_PARTNER = '') OR (@P_NO_PARTNER IS NULL)) 
			AND (@P_CD_PJT = '' OR @P_CD_PJT IS NULL OR @P_CD_PJT = L.CD_PJT) 
			AND (@P_CD_ITEM = '' OR @P_CD_ITEM IS NULL OR @P_CD_ITEM = L.CD_ITEM)
        GROUP BY H.NO_PO, H.CD_PLANT, H.CD_PARTNER, H.DT_PO, H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT, 
	         H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, H.DC50_PO, H.TP_PROCESS, 
	        H.FG_TAXP, H.YN_AM, H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	        E.NM_SYSDEF, F.NM_TP, G.NM_PROJECT, 
	        L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	        L.CD_UNIT_MM, MC.NM_CC,
	        L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 
	        L.QT_PO_MM, L.QT_PO, L.QT_REQ_MM, L.QT_REQ, L.QT_RCV_MM, L.QT_RCV, L.QT_RETURN_MM, L.QT_RETURN, 
	        L.QT_TR_MM, L.QT_TR, 
	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2, 
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 
	        M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF, S.NM_SL, J.NM_QTIOTP, CUR_INV.QT_INV,
            PRL.CD_PURGRP ,
			G1.NM_PURGRP, 
			E1.NM_KOR ,
		    DE.NM_DEPT,
		    M.NO_MODEL, --2011.10.12 모델코드추가 이장원 
			L.SEQ_PROJECT,
			SPL.CD_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM, --프로젝트 품목명 
			I2.STND_ITEM,
			H.FG_WEB,
			H.DT_WEB,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN,
			H.DC_RMK2,
			H.NO_ORDER,
			M.GRP_ITEM, 
			MI.NM_ITEMGRP, 
			M.GRP_MFG, 
			MC2.NM_SYSDEF, 
			M.STND_DETAIL_ITEM, 
			M.MAT_ITEM, 
			MPA.LN_PARTNER
			
        ORDER BY H.DT_PO
    END
ELSE IF (@P_TAB_NAME = 'TP_CC')                    -- C/C별    
    BEGIN
        SELECT 'N' CHK, 
	        --HEADER 내역-------------------------
	        H.NO_PO, H.CD_PARTNER,  H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT,-- H.DT_PO,
	        H.TP_UM_TAX, H.CD_EXCH, H.RT_EXCH, H.DT_PO, MC.NM_CC,
            --H.AM_EX, H.AM, H.VAT, 
            H.DC50_PO DC_RMK, H.TP_PROCESS, 
	        H.FG_TAXP, 
	        CASE WHEN H.YN_AM = 'Y' THEN @V_STR_CONV1 ELSE @V_STR_CONV2 END YN_AM, 
	        H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, 
	        E.NM_SYSDEF NM_TRANS, F.NM_TP NM_PURCHASE, G.NM_PROJECT, 

	        --LINE 내역-------------------------
	        L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	        L.CD_UNIT_MM, 
	        L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 

	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO END QT_PO, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_REQ_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_REQ END QT_REQ, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RCV END QT_GR, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_RETURN_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_RETURN END QT_RETURN, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_TR_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_TR END QT_TR, 
	        --CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(ISNULL(Q.QT_CLS_MM, 0)) WHEN @P_TYPE_UNIT = '002' THEN SUM(ISNULL(Q.QT_CLS, 0)) END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS_MM, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS_MM, 0)
																				 END)
				 WHEN @P_TYPE_UNIT = '002' THEN SUM(CASE WHEN QH.YN_RETURN = 'Y' THEN ISNULL(Q.QT_CLS, 0) * -1
																				 ELSE ISNULL(Q.QT_CLS, 0)
																				 END)
			END QT_CLS, 
	        CASE WHEN @P_TYPE_UNIT = '001' THEN L.QT_PO_MM - L.QT_RCV_MM WHEN @P_TYPE_UNIT = '002' THEN L.QT_PO - L.QT_RCV END QT_REMAIN, 

	        L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	        L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	        L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	        L.NO_APPLINE, L.DC1, L.DC2,
	        L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가  
	        M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF NM_POST, S.NM_SL, J.NM_QTIOTP NM_FG_RCV, CUR_INV.QT_INV, L.CD_CC,
	        PRL.CD_PURGRP AS REQPURGRP,
			G1.NM_PURGRP AS NM_REQ_PURGRP, 
			E1.NM_KOR AS REQ_NM_KOR,
			DE.NM_DEPT AS REQ_NM_DEPT,
		    ISNULL(MAX(PU_REV.QT_REV_MM), 0) AS QT_REV_MM,
		    M.NO_MODEL, --2011.10.12 모델코드추가 이장원
			L.SEQ_PROJECT,
			SPL.CD_ITEM AS CD_PJT_ITEM, --프로젝트 품목코드  
			I2.NM_ITEM AS NM_PJT_ITEM, --프로젝트 품목명 
			I2.STND_ITEM AS PJT_ITEM_STND,
			H.FG_WEB,
			H.DT_WEB,
			L.DT_PLAN,
			M.UNIT_PO,
			M.NO_DESIGN,
			I2.NO_DESIGN AS NO_PJT_DESIGN,
			H.DC_RMK2,
			H.NO_ORDER,
			M.GRP_ITEM, 
		    MI.NM_ITEMGRP, 
		    M.GRP_MFG, 
		    MC2.NM_SYSDEF AS NM_GRP_MFG, 
		    M.STND_DETAIL_ITEM, 
		    M.MAT_ITEM, 
		    MPA.LN_PARTNER AS NM_PARTNER,
		    ISNULL(MAX(PU_REV.QT_PASS_MM), 0) AS QT_PASS_MM,
		    ISNULL(MAX(PU_REV.QT_BAD_MM), 0) AS QT_BAD_MM

        FROM PU_POH H
	        INNER JOIN PU_POL L ON L.CD_COMPANY = @P_CD_COMPANY
	                                        AND L.NO_PO = H.NO_PO
	        INNER JOIN DZSN_MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY
	                                                AND P.CD_PLANT = H.CD_PLANT

			LEFT OUTER JOIN ( SELECT CD_COMPANY, NO_PO, NO_POLINE AS NO_LINE,
									SUM(ISNULL(QT_REV_MM, 0)) AS QT_REV_MM,
                                     SUM(ISNULL(QT_PASS_MM, 0)) AS QT_PASS_MM,
                                     SUM(ISNULL(QT_BAD_MM, 0)) AS QT_BAD_MM
                            FROM PU_REV
                           WHERE ISNULL(FG_IO,'001') = '001' 
                           GROUP BY CD_COMPANY, NO_PO, NO_POLINE ) AS PU_REV
                     ON PU_REV.CD_COMPANY = L.CD_COMPANY
                    AND PU_REV.NO_PO = L.NO_PO
                    AND PU_REV.NO_LINE = L.NO_LINE

	        LEFT OUTER JOIN MM_QTIO Q ON Q.CD_COMPANY = @P_CD_COMPANY
	                                                        AND Q.NO_PSO_MGMT = L.NO_PO
	                                                        AND Q.NO_PSOLINE_MGMT = L.NO_LINE
	                                                        AND Q.FG_IO IN ('001', '030')
	        LEFT OUTER JOIN DZSN_MA_PITEM M ON M.CD_COMPANY = @P_CD_COMPANY
	                                                        AND M.CD_PLANT = L.CD_PLANT
	                                                        AND M.CD_ITEM = L.CD_ITEM
	        LEFT OUTER JOIN DZSN_MA_PARTNER A ON A.CD_COMPANY  = @P_CD_COMPANY
	                                                        AND A.CD_PARTNER = H.CD_PARTNER
	        LEFT OUTER JOIN DZSN_MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY
	                                                        AND B.NO_EMP = H.NO_EMP
	        LEFT OUTER JOIN DZSN_MA_PURGRP C ON C.CD_COMPANY = @P_CD_COMPANY
	                                                        AND C.CD_PURGRP = H.CD_PURGRP
	        LEFT OUTER JOIN DZSN_PU_TPPO D ON D.CD_COMPANY = @P_CD_COMPANY
	                                                        AND D.CD_TPPO = H.CD_TPPO
	        LEFT OUTER JOIN DZSN_MA_CODEDTL E ON E.CD_COMPANY = @P_CD_COMPANY
	                                                                AND E.CD_FIELD = 'PU_C000016'        --거래구분
	                                                                AND E.CD_SYSDEF = H.FG_TRANS
	        LEFT OUTER JOIN DZSN_MA_AISPOSTH F ON F.CD_COMPANY = @P_CD_COMPANY
	                                                                AND F.FG_TP = '200'        --매입형태
	                                                                AND F.CD_TP = L.FG_PURCHASE

	        LEFT OUTER JOIN DZSN_SA_PROJECTH G ON G.NO_PROJECT = L.CD_PJT AND G.CD_COMPANY = L.CD_COMPANY
        	
	        LEFT OUTER JOIN DZSN_MA_SL S ON S.CD_COMPANY = @P_CD_COMPANY
	                                                        AND S.CD_BIZAREA = P.CD_BIZAREA
	                                                        AND S.CD_PLANT = L.CD_PLANT
	                                                        AND S.CD_SL = L.CD_SL
	        LEFT OUTER JOIN DZSN_MM_EJTP J ON J.CD_COMPANY = @P_CD_COMPANY
	                                     AND J.CD_QTIOTP = L.FG_RCV
	        LEFT OUTER JOIN DZSN_MA_CODEDTL K ON K.CD_COMPANY = @P_CD_COMPANY
	                                                                AND K.CD_FIELD = 'PU_C000009'        --오더상태
	                                                                AND K.CD_SYSDEF = L.FG_POST

	        LEFT OUTER JOIN DZSN_MA_SL MS ON MS.CD_SL = M.CD_SL AND MS.CD_PLANT = M.CD_PLANT AND MS.CD_COMPANY = M.CD_COMPANY AND MS.CD_BIZAREA = M.CD_BIZAREA

	        LEFT OUTER JOIN (
						        SELECT CD_SL,ISNULL(SUM(QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR+QT_TRANS_GR), 0) 
								        - ISNULL(SUM(QT_GOOD_GI + QT_REJECT_GI +QT_INSP_GI+QT_TRANS_GI), 0) QT_INV
						        FROM  MM_OHSLINVM
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND YY_INV = @DATE_YYYY
								        AND CD_PLANT = @P_CD_PLANT
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
						        GROUP BY CD_SL
						        UNION ALL
						        SELECT CD_SL, QT_GOOD_INV QT_INV
						        FROM MM_OPENQTL
						        WHERE CD_COMPANY = @P_CD_COMPANY
								        AND CD_PLANT = @P_CD_PLANT
								        AND YM_STANDARD = @DATE_YYYY
								        AND (CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		
                            ) CUR_INV ON 
								        CUR_INV.CD_SL = M.CD_SL
            LEFT OUTER JOIN DZSN_MA_CC MC ON L.CD_CC = MC.CD_CC AND MC.CD_COMPANY = L.CD_COMPANY
                 LEFT OUTER JOIN PU_PRL PRL ON PRL.NO_PR = L.NO_PR AND PRL.NO_PRLINE = L.NO_PRLINE AND PRL.CD_COMPANY = L.CD_COMPANY
	LEFT OUTER JOIN DZSN_MA_PURGRP G1 ON PRL.CD_PURGRP = G1.CD_PURGRP  AND PRL.CD_COMPANY = G1.CD_COMPANY 	
	LEFT OUTER JOIN PU_PRH PRH ON PRH.CD_COMPANY = PRL.CD_COMPANY AND PRH.NO_PR = PRL.NO_PR  AND PRH.CD_PLANT  = PRL.CD_PLANT -- 요청부서, 요청자조회조건때문에추가
	LEFT OUTER JOIN  DZSN_MA_EMP E1 ON PRH.NO_EMP= E1.NO_EMP AND  E1.CD_COMPANY =@P_CD_COMPANY 
	LEFT OUTER JOIN DZSN_MA_DEPT DE ON PRH.CD_DEPT = DE.CD_DEPT AND @P_CD_COMPANY = DE.CD_COMPANY  		
	LEFT OUTER JOIN SA_PROJECTL SPL ON L.CD_COMPANY = SPL.CD_COMPANY AND L.CD_PJT = SPL.NO_PROJECT AND L.SEQ_PROJECT = SPL.SEQ_PROJECT    	  
	LEFT JOIN DZSN_MA_PITEM I2 ON SPL.CD_COMPANY = I2.CD_COMPANY AND SPL.CD_PLANT = I2.CD_PLANT AND SPL.CD_ITEM = I2.CD_ITEM    
	LEFT OUTER JOIN MM_QTIOH QH ON QH.CD_COMPANY = Q.CD_COMPANY AND QH.NO_IO = Q.NO_IO
	LEFT OUTER JOIN DZSN_MA_ITEMGRP MI ON M.GRP_ITEM = MI.CD_ITEMGRP AND M.CD_COMPANY = MI.CD_COMPANY
	LEFT OUTER JOIN DZSN_MA_CODEDTL MC2 ON MC2.CD_COMPANY = M.CD_COMPANY AND MC2.CD_FIELD = 'MA_B000066' AND MC2.CD_SYSDEF = M.GRP_MFG  
	LEFT OUTER JOIN DZSN_MA_PARTNER MPA ON MPA.CD_COMPANY = M.CD_COMPANY AND MPA.CD_PARTNER = M.PARTNER
            WHERE H.CD_COMPANY = @P_CD_COMPANY
	            AND P.CD_BIZAREA = @P_CD_BIZAREA
	            AND (L.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL)
	            AND ((@P_TYPE_DT = '001' AND (H.DT_PO BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)) 
	                    OR (@P_TYPE_DT = '002' AND (L.DT_LIMIT BETWEEN @P_DT_PO_FROM AND @P_DT_PO_TO)))
	            --AND (L.FG_POST = @P_FG_POST OR @P_FG_POST = '' OR @P_FG_POST IS NULL)
	            AND (L.FG_POST IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_FG_POST)) OR @P_FG_POST = '' OR @P_FG_POST IS NULL)    
	            AND (H.FG_TRANS = @P_FG_TRANS OR @P_FG_TRANS = '' OR @P_FG_TRANS IS NULL)
	            AND (H.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PURGRP)) OR @P_CD_PURGRP = '' OR @P_CD_PURGRP IS NULL)
	            AND (H.NO_EMP = @P_NO_EMP OR @P_NO_EMP = '' OR @P_NO_EMP IS NULL)
	            --@P_TYPE_UNIT
	            AND ((@P_YN_RCV = '' OR @P_YN_RCV IS NULL OR @P_YN_RCV = '001')
	                    OR (@P_YN_RCV = '002' AND L.QT_PO_MM - L.QT_RCV_MM = 0)
	                    OR (@P_YN_RCV = '003' AND L.QT_PO_MM - L.QT_RCV_MM > 0))
            	
	            AND (L.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)	
                AND (ISNULL(L.CD_CC,'') = @P_CONDITION) 
                AND  (( PRL.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_REQ_PURGRP))) OR (@P_REQ_PURGRP = '' ) OR (@P_REQ_PURGRP  IS NULL)) --요청구매그룹  
AND (( PRH.CD_DEPT  =@P_REQ_DEPT) OR (@P_REQ_DEPT = '' ) OR (@P_REQ_DEPT  IS NULL)) --요청부서
AND (( PRH.NO_EMP  =@P_REQ_EMP) OR (@P_REQ_EMP = '' ) OR (@P_REQ_EMP  IS NULL)) -- 요청인원
AND (( L.NO_PO LIKE '%' + @P_NO_PO + '%') OR (@P_NO_PO = '') OR (@P_NO_PO IS NULL))
AND ((A.CD_PARTNER = @P_NO_PARTNER) OR (@P_NO_PARTNER = '') OR (@P_NO_PARTNER IS NULL))  
AND (@P_CD_PJT = '' OR @P_CD_PJT IS NULL OR @P_CD_PJT = L.CD_PJT) 
AND (@P_CD_ITEM = '' OR @P_CD_ITEM IS NULL OR @P_CD_ITEM = L.CD_ITEM)

            GROUP BY H.NO_PO, H.CD_PLANT, H.CD_PARTNER,  H.CD_PURGRP, H.NO_EMP, H.CD_TPPO, H.FG_UM, H.FG_PAYMENT,-- H.DT_PO,
	            H.TP_UM_TAX,H.CD_EXCH, H.RT_EXCH, H.DC50_PO, H.TP_PROCESS, 
	            H.FG_TAXP, H.YN_AM, H.FG_TRANS, P.NM_PLANT, A.LN_PARTNER, B.NM_KOR, C.NM_PURGRP, D.NM_TPPO, H.DT_PO,
	            E.NM_SYSDEF, F.NM_TP, G.NM_PROJECT, 
	            L.NO_LINE, L.CD_PLANT, L.NO_CONTRACT, L.NO_CTLINE, L.NO_PR, L.NO_PRLINE, L.FG_TRANS, L.CD_ITEM, 
	            L.CD_UNIT_MM, 
	            L.FG_RCV, L.FG_PURCHASE, L.DT_LIMIT, 
	            L.QT_PO_MM, L.QT_PO, L.QT_REQ_MM, L.QT_REQ, L.QT_RCV_MM, L.QT_RCV, L.QT_RETURN_MM, L.QT_RETURN, 
	            L.QT_TR_MM, L.QT_TR, MC.NM_CC,
	            L.FG_TAX, L.UM_EX_PO, L.UM_EX, L.AM_EX, 
	            L.UM, L.AM, L.VAT, L.CD_SL, L.FG_POST, L.FG_POCON, L.YN_AUTORCV, L.YN_RCV, L.YN_RETURN, L.YN_ORDER, L.YN_SUBCON, 
	            L.YN_IMPORT, L.RT_PO, L.YN_REQ, L.CD_PJT, L.AM_EXTR, L.AM_TR, L.AM_EXREQ, L.NO_APP, 
	            L.NO_APPLINE, L.DC1, L.DC2, 
	            L.DC3, -- 2010/12/31 조형우 DC3(비고3) 컬럼 추가 
	            M.NM_ITEM, M.STND_ITEM, M.UNIT_IM, K.NM_SYSDEF, S.NM_SL, J.NM_QTIOTP, CUR_INV.QT_INV, L.CD_CC,
				PRL.CD_PURGRP ,
				G1.NM_PURGRP, 
				E1.NM_KOR ,
				DE.NM_DEPT,
				M.NO_MODEL, --2011.10.12 모델코드추가 이장원 
				L.SEQ_PROJECT,
				SPL.CD_ITEM, --프로젝트 품목코드  
				I2.NM_ITEM , --프로젝트 품목명 
				I2.STND_ITEM,
				H.FG_WEB,
				H.DT_WEB,
				L.DT_PLAN,
				M.UNIT_PO,
				M.NO_DESIGN,
				I2.NO_DESIGN,
				H.DC_RMK2,
				H.NO_ORDER,
				M.GRP_ITEM, 
				MI.NM_ITEMGRP, 
				M.GRP_MFG, 
				MC2.NM_SYSDEF, 
				M.STND_DETAIL_ITEM, 
				M.MAT_ITEM, 
				MPA.LN_PARTNER
				
            ORDER BY L.CD_CC
    END;

SET NOCOUNT OFF