USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_CZ_PU_POST_RPT_SUMITEM_SELECT]    Script Date: 2023-05-03 오후 6:02:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*******************************************************************************          
*******************************************************************************/
ALTER PROC [NEOE].[UP_CZ_PU_POST_RPT_SUMITEM_SELECT]
(
	@P_CD_COMPANY        NVARCHAR(7),
	@P_CD_BIZAREA        NVARCHAR(7),
	@P_CD_PLANT          NVARCHAR(7),
	@P_TYPE_DT           NVARCHAR(3),
	@P_DT_PO_FROM        NVARCHAR(8),
	@P_DT_PO_TO          NVARCHAR(8),
	@P_FG_POST           NVARCHAR(4000),
	@P_FG_TRANS          NVARCHAR(3),
	@P_CD_PURGRP		 NVARCHAR(4000),
	@P_NO_EMP            NVARCHAR(10),
	@P_TYPE_UNIT         NVARCHAR(3),
	@P_YN_RCV            NVARCHAR(4), 
	@P_TAB_NAME          NVARCHAR(50),
	@P_MULTI_CD_SL		 VARCHAR(8000),
	@P_CD_CC             NVARCHAR(12),
	@P_REQ_PURGRP		NVARCHAR(4000), --요청구매그룹
	@P_REQ_DEPT			NVARCHAR(12), --요청부서
	@P_REQ_EMP 			NVARCHAR(10),  --요청자
	@P_NO_PO			NVARCHAR(20),  --발주번호 2011.03.04 황슬기 추가
	@P_NO_PARTNER		NVARCHAR(20),   --거래처 2011.03.04 황슬기 추가
	@P_CD_PJT			NVARCHAR(20),
	@P_CD_ITEM			NVARCHAR(20),
    @P_FG_LANG     NVARCHAR(4) = NULL	--언어
)
AS
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG


DECLARE @DATE_YYYY        NVARCHAR(4)
SELECT @DATE_YYYY = CONVERT(NVARCHAR(4), GETDATE(), 112)


IF ( @P_TYPE_UNIT = '001')    ---------------------------------------------------------------------------- 발주단위          

	BEGIN
		SELECT 
			    MP.GRP_ITEM, 
			    MIG.NM_ITEMGRP, -- 품목군명
			    MP.GRP_MFG,
			    C4.NM_SYSDEF AS NM_GRP_MFG, -- 제품군명
			    MP.CLS_S,     -- 소분류
			    C1.NM_SYSDEF AS NM_CLS_S, --소분류명 
			    MP.CLS_M,     -- 중분류
			    C2.NM_SYSDEF AS NM_CLS_M, --소분류명 
			    MP.CLS_L,     -- 대분류
			    C3.NM_SYSDEF AS NM_CLS_L, --소분류명 
			    ITEM_SUM.CD_ITEM,    
			    MP.NM_ITEM,  
			    MP.STND_ITEM,     
			    MP.UNIT_IM,  
			    ITEM_SUM.QT_PO,
			    ITEM_SUM.QT_REQ,
			    ITEM_SUM.QT_GR,
			    ITEM_SUM.QT_REMAN, 
	            ITEM_SUM.NM_CC,
			    CUR_INV.QT_INV, -- 현재고
			    G1.NM_PURGRP AS NM_REQ_PURGRP, 
				E1.NM_KOR AS REQ_NM_KOR,
				DE.NM_DEPT AS REQ_NM_DEPT,
				ITEM_SUM.DC_RMK2,
                ITEM_SUM.QT_REV_MM, -- 가입고수량,
                MP.NO_MODEL, --2011.10.12 모델코드추가 이장원
                MP.UNIT_PO,
                MP.NO_DESIGN,
                MP.STND_DETAIL_ITEM, 
			    MP.MAT_ITEM, 
			    MPA.LN_PARTNER,
				ITEM_SUM.QT_PASS_MM,
				ITEM_SUM.QT_BAD_MM 
				
		  FROM
				(
					SELECT L.CD_ITEM,L.CD_PLANT,L.CD_COMPANY,MC.NM_CC, L.NO_PR,L.NO_PRLINE,-- NM_ITEM, UNIT_IM, CLS_S, CLS_M, CLS_L, GRP_ITEM, GRP_MFG,
						   SUM(QT_PO_MM) QT_PO, SUM(QT_REQ_MM) QT_REQ, SUM(QT_RCV_MM) QT_GR, (SUM(QT_PO_MM) - SUM(QT_RCV_MM)) QT_REMAN, H.DC_RMK2,
                           SUM(QT_REV_MM) AS QT_REV_MM, SUM(QT_PASS_MM) AS QT_PASS_MM, SUM(QT_BAD_MM) AS QT_BAD_MM
					FROM
						PU_POH H
							INNER JOIN PU_POL L ON 
											L.CD_COMPANY = H.CD_COMPANY
											AND L.NO_PO = H.NO_PO

							INNER JOIN DZSN_MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY
													AND P.CD_PLANT = H.CD_PLANT
                        LEFT OUTER JOIN DZSN_MA_CC MC ON L.CD_CC = MC.CD_CC AND MC.CD_COMPANY = L.CD_COMPANY
                        LEFT OUTER JOIN DZSN_MA_PARTNER A ON A.CD_COMPANY  = @P_CD_COMPANY  
						AND A.CD_PARTNER = H.CD_PARTNER  

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
                            
					WHERE
							H.CD_COMPANY = @P_CD_COMPANY
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
					        AND ((L.CD_CC = @P_CD_CC) OR (@P_CD_CC = '') OR (@P_CD_CC IS NULL))
					        AND ((L.NO_PO LIKE '%' + @P_NO_PO + '%') OR (@P_NO_PO = '') OR (@P_NO_PO IS NULL))
					        AND ((A.CD_PARTNER = @P_NO_PARTNER) OR (@P_NO_PARTNER = '') OR (@P_NO_PARTNER IS NULL))    
							AND (@P_CD_ITEM = '' OR @P_CD_ITEM IS NULL OR @P_CD_ITEM = L.CD_ITEM)
					GROUP BY
							L.CD_ITEM, L.CD_PLANT, L.CD_COMPANY, MC.NM_CC, L.NO_PR,L.NO_PRLINE, H.DC_RMK2

				) ITEM_SUM 

	    INNER JOIN DZSN_MA_PITEM MP ON
					    ITEM_SUM.CD_ITEM = MP.CD_ITEM AND
					    ITEM_SUM.CD_PLANT = MP.CD_PLANT AND
					    ITEM_SUM.CD_COMPANY = MP.CD_COMPANY

	    LEFT OUTER JOIN DZSN_MA_ITEMGRP MIG ON
					    MIG.CD_ITEMGRP = MP.GRP_ITEM  AND
					    MIG.CD_COMPANY = MP.CD_COMPANY 

	    LEFT OUTER JOIN (
							SELECT P.CD_COMPANY , P.P_YR, P.CD_PLANT,P.CD_ITEM,      
								   SUM(  (P.QT_GOOD_OPEN + P.QT_REJECT_OPEN + P.QT_INSP_OPEN + P.QT_TRANS_OPEN ) + (P.QT_GOOD_GR + P.QT_REJECT_GR + P.QT_INSP_GR + P.QT_TRANS_GR) - (P.QT_GOOD_GI + P.QT_REJECT_GI + P.QT_INSP_GI + P.QT_TRANS_GI))  AS QT_INV           
								 	
							FROM  MM_PINVN  P INNER JOIN DZSN_MA_PITEM MA    
							ON P.CD_COMPANY = MA.CD_COMPANY   
							AND P.CD_PLANT = MA.CD_PLANT   
							AND P.CD_ITEM = MA.CD_ITEM      
							WHERE P.P_YR     = LEFT(CONVERT(NVARCHAR, GETDATE(), 121), 4)                 
							AND P.CD_COMPANY = @P_CD_COMPANY            
							AND (P.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL )         
							AND MA.CLS_ITEM IN ('001', '002', '003', '004', '005', '009')  
							AND (P.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		 
							GROUP BY P_YR,P.CD_COMPANY, P.CD_ITEM , P.CD_PLANT 	  
								  
                        ) CUR_INV ON 
								    CUR_INV.CD_ITEM = MP.CD_ITEM AND CUR_INV.CD_PLANT = MP.CD_PLANT AND CUR_INV.CD_COMPANY = MP.CD_COMPANY

	     LEFT OUTER JOIN   -- 소분류
	      (  
		       SELECT CD_SYSDEF, NM_SYSDEF, CD_COMPANY  
		       FROM DZSN_MA_CODEDTL   
		       WHERE CD_FIELD ='MA_B000032'  
	      ) C1 ON MP.CLS_S = C1.CD_SYSDEF AND MP.CD_COMPANY = C1.CD_COMPANY  

	     LEFT OUTER JOIN -- 중분류  
	      (  
		       SELECT CD_SYSDEF, NM_SYSDEF, CD_COMPANY  
		       FROM DZSN_MA_CODEDTL   
		       WHERE CD_FIELD ='MA_B000031'  
	      ) C2 ON MP.CLS_M = C2.CD_SYSDEF AND MP.CD_COMPANY = C2.CD_COMPANY  

	    LEFT OUTER JOIN   -- 대분류
	      (  
		       SELECT CD_SYSDEF, NM_SYSDEF, CD_COMPANY  
		       FROM DZSN_MA_CODEDTL   
		       WHERE CD_FIELD ='MA_B000030'  
	      ) C3 ON MP.CLS_L = C3.CD_SYSDEF AND MP.CD_COMPANY = C3.CD_COMPANY  

	    LEFT OUTER JOIN   
	      (  
		       SELECT CD_SYSDEF, NM_SYSDEF, CD_COMPANY  
		       FROM DZSN_MA_CODEDTL   
		       WHERE CD_FIELD ='MA_B000066'  
	      ) C4 ON MP.GRP_MFG = C4.CD_SYSDEF AND ITEM_SUM.CD_COMPANY = C4.CD_COMPANY 

	    LEFT OUTER JOIN DZSN_MA_SL MS ON MS.CD_SL = MP.CD_SL AND MS.CD_PLANT = MP.CD_PLANT AND MS.CD_COMPANY = MP.CD_COMPANY AND MS.CD_BIZAREA = MP.CD_BIZAREA
		
		LEFT OUTER JOIN PU_PRL PRL ON PRL.NO_PR = ITEM_SUM.NO_PR AND PRL.NO_PRLINE = ITEM_SUM.NO_PRLINE AND PRL.CD_COMPANY = ITEM_SUM.CD_COMPANY
		LEFT OUTER JOIN DZSN_MA_PURGRP G1 ON PRL.CD_PURGRP = G1.CD_PURGRP  AND PRL.CD_COMPANY = G1.CD_COMPANY 	
		LEFT OUTER JOIN PU_PRH PRH ON PRH.CD_COMPANY = PRL.CD_COMPANY  -- 요청부서, 요청자조회조건때문에추가
							AND PRH.NO_PR = PRL.NO_PR
							AND PRH.CD_PLANT  = PRL.CD_PLANT

		 LEFT OUTER JOIN  DZSN_MA_EMP E1 ON PRH.NO_EMP= E1.NO_EMP    
									AND  E1.CD_COMPANY =@P_CD_COMPANY
		 LEFT OUTER JOIN DZSN_MA_DEPT DE ON PRH.CD_DEPT = DE.CD_DEPT   
									AND @P_CD_COMPANY = DE.CD_COMPANY 
		 LEFT OUTER JOIN DZSN_MA_PARTNER MPA ON MPA.CD_COMPANY = MP.CD_COMPANY AND MPA.CD_PARTNER = MP.PARTNER							
		WHERE 
		    (( PRL.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_REQ_PURGRP))) OR (@P_REQ_PURGRP = '' ) OR (@P_REQ_PURGRP  IS NULL)) --요청구매그룹  
		AND (( PRH.CD_DEPT  =@P_REQ_DEPT) OR (@P_REQ_DEPT = '' ) OR (@P_REQ_DEPT  IS NULL)) --요청부서
		AND (( PRH.NO_EMP  =@P_REQ_EMP) OR (@P_REQ_EMP = '' ) OR (@P_REQ_EMP  IS NULL)) -- 요청인원		 
			
	END

ELSE   /* 재고단위 -------------------------------------------------------------------------------------- */

	BEGIN
		SELECT 
			MP.GRP_ITEM, 
			MIG.NM_ITEMGRP, -- 품목군명

			MP.GRP_MFG,
			C4.NM_SYSDEF AS NM_GRP_MFG, -- 제품군명

			MP.CLS_S,     -- 소분류
			C1.NM_SYSDEF AS NM_CLS_S, --소분류명 

			MP.CLS_M,     -- 중분류
			C2.NM_SYSDEF AS NM_CLS_M, --소분류명 

			MP.CLS_L,     -- 대분류
			C3.NM_SYSDEF AS NM_CLS_L, --소분류명 

			ITEM_SUM.CD_ITEM,    
			MP.NM_ITEM,  
			MP.STND_ITEM,     
			MP.UNIT_IM,  
			ITEM_SUM.QT_PO,
			ITEM_SUM.QT_REQ,
			ITEM_SUM.QT_GR,
		    ITEM_SUM.NM_CC,
			ITEM_SUM.QT_REMAN,
			ITEM_SUM.DC_RMK2,
            ITEM_SUM.QT_REV_MM ,-- 가입고수량
            MP.NO_MODEL, --2011.10.12 모델코드추가 이장원
            MP.UNIT_PO,
            MP.NO_DESIGN,
            MP.STND_DETAIL_ITEM, 
		    MP.MAT_ITEM, 
		    MPA.LN_PARTNER,
		    ITEM_SUM.QT_PASS_MM,
		    ITEM_SUM.QT_BAD_MM
            
		FROM
				(
					SELECT L.CD_ITEM,L.CD_PLANT,L.CD_COMPANY,MC.NM_CC, L.NO_PR,L.NO_PRLINE, --NM_ITEM,STND_ITEM, UNIT_IM, CLS_S, CLS_M, CLS_L, GRP_ITEM, GRP_MFG,
						   SUM(QT_PO) QT_PO, SUM(QT_REQ) QT_REQ, SUM(QT_RCV) QT_GR, (SUM(QT_PO) - SUM(QT_RCV)) QT_REMAN, H.DC_RMK2,
                           SUM(QT_REV_MM) AS QT_REV_MM, SUM(QT_PASS_MM) AS QT_PASS_MM, SUM(QT_BAD_MM) AS QT_BAD_MM
					FROM
						PU_POH H
							INNER JOIN PU_POL L ON 
											L.CD_COMPANY = H.CD_COMPANY
											AND L.NO_PO = H.NO_PO

							INNER JOIN MM_QTIO MQ ON 
											MQ.NO_PSO_MGMT = L.NO_PO AND
											MQ.NO_PSOLINE_MGMT = L.NO_LINE AND
											MQ.CD_COMPANY = L.CD_COMPANY

							INNER JOIN DZSN_MA_PLANT P ON P.CD_COMPANY = @P_CD_COMPANY
													AND P.CD_PLANT = H.CD_PLANT
                            LEFT OUTER JOIN DZSN_MA_CC MC ON L.CD_CC = MC.CD_CC AND MC.CD_COMPANY = L.CD_COMPANY 
                            LEFT OUTER JOIN DZSN_MA_PARTNER A ON A.CD_COMPANY  = @P_CD_COMPANY  
							AND A.CD_PARTNER = H.CD_PARTNER 

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
					WHERE
							H.CD_COMPANY = @P_CD_COMPANY
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
							AND (( L.NO_PO LIKE '%' + @P_NO_PO + '%') OR (@P_NO_PO = '') OR (@P_NO_PO IS NULL))
							AND ((A.CD_PARTNER = @P_NO_PARTNER) OR (@P_NO_PARTNER = '') OR (@P_NO_PARTNER IS NULL)) 
							AND MQ.FG_IO = '001'
							AND (@P_CD_ITEM = '' OR @P_CD_ITEM IS NULL OR @P_CD_ITEM = L.CD_ITEM)
					GROUP BY
							L.CD_ITEM, L.CD_PLANT, L.CD_COMPANY, MC.NM_CC, L.NO_PR,L.NO_PRLINE, H.DC_RMK2
				) ITEM_SUM 
	    INNER JOIN DZSN_MA_PITEM MP ON
					    ITEM_SUM.CD_ITEM = MP.CD_ITEM AND
					    ITEM_SUM.CD_PLANT = MP.CD_PLANT AND
					    ITEM_SUM.CD_COMPANY = MP.CD_COMPANY

	    LEFT OUTER JOIN DZSN_MA_ITEMGRP MIG ON
					    MIG.CD_ITEMGRP = MP.GRP_ITEM  AND
					    MIG.CD_COMPANY = MP.CD_COMPANY 

	    LEFT OUTER JOIN (
							SELECT P.CD_COMPANY , P.P_YR, P.CD_PLANT,P.CD_ITEM,      
								   SUM(  (P.QT_GOOD_OPEN + P.QT_REJECT_OPEN + P.QT_INSP_OPEN + P.QT_TRANS_OPEN ) + (P.QT_GOOD_GR + P.QT_REJECT_GR + P.QT_INSP_GR + P.QT_TRANS_GR) - (P.QT_GOOD_GI + P.QT_REJECT_GI + P.QT_INSP_GI + P.QT_TRANS_GI))  AS QT_INV           
								 	
							FROM  MM_PINVN  P INNER JOIN DZSN_MA_PITEM MA    
							ON P.CD_COMPANY = MA.CD_COMPANY   
							AND P.CD_PLANT = MA.CD_PLANT   
							AND P.CD_ITEM = MA.CD_ITEM      
							WHERE P.P_YR     = LEFT(CONVERT(NVARCHAR, GETDATE(), 121), 4)                 
							AND P.CD_COMPANY = @P_CD_COMPANY            
							AND (P.CD_PLANT = @P_CD_PLANT OR @P_CD_PLANT = '' OR @P_CD_PLANT IS NULL )         
							AND MA.CLS_ITEM IN ('001', '002', '003', '004', '005', '009')  
							AND (P.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)) OR @P_MULTI_CD_SL = '' OR @P_MULTI_CD_SL IS NULL)		 
							GROUP BY P_YR,P.CD_COMPANY, P.CD_ITEM , P.CD_PLANT 	  
                        ) CUR_INV ON 
								    CUR_INV.CD_ITEM = MP.CD_ITEM AND 
								    CUR_INV.CD_PLANT = MP.CD_PLANT
    							

	 LEFT OUTER JOIN   -- 소분류
	      (  
		       SELECT CD_SYSDEF, NM_SYSDEF, CD_COMPANY  
		       FROM DZSN_MA_CODEDTL   
		       WHERE CD_FIELD ='MA_B000032'  
	      ) C1 ON MP.CLS_S = C1.CD_SYSDEF AND MP.CD_COMPANY = C1.CD_COMPANY  

	     LEFT OUTER JOIN -- 중분류 R 
	      (  
		       SELECT CD_SYSDEF, NM_SYSDEF, CD_COMPANY  
		       FROM DZSN_MA_CODEDTL   
		       WHERE CD_FIELD ='MA_B000031'  
	      ) C2 ON MP.CLS_M = C2.CD_SYSDEF AND MP.CD_COMPANY = C2.CD_COMPANY  

	    LEFT OUTER JOIN   -- 대분류
	      (  
		       SELECT CD_SYSDEF, NM_SYSDEF, CD_COMPANY  
		       FROM DZSN_MA_CODEDTL   
		       WHERE CD_FIELD ='MA_B000030'  
	      ) C3 ON MP.CLS_L = C3.CD_SYSDEF AND MP.CD_COMPANY = C3.CD_COMPANY  

	    LEFT OUTER JOIN   
	      (  
		       SELECT CD_SYSDEF, NM_SYSDEF, CD_COMPANY  
		       FROM DZSN_MA_CODEDTL   
		       WHERE CD_FIELD ='MA_B000066'  
	      ) C4 ON MP.GRP_MFG = C4.CD_SYSDEF AND ITEM_SUM.CD_COMPANY = C4.CD_COMPANY  

	    LEFT OUTER JOIN DZSN_MA_SL MS ON MS.CD_SL = MP.CD_SL AND MS.CD_PLANT = MP.CD_PLANT AND MS.CD_COMPANY = MP.CD_COMPANY AND MS.CD_BIZAREA = MP.CD_BIZAREA

		LEFT OUTER JOIN PU_PRL PRL ON PRL.NO_PR = ITEM_SUM.NO_PR AND PRL.NO_PRLINE = ITEM_SUM.NO_PRLINE AND PRL.CD_COMPANY = ITEM_SUM.CD_COMPANY
		LEFT OUTER JOIN DZSN_MA_PURGRP G1 ON PRL.CD_PURGRP = G1.CD_PURGRP  AND PRL.CD_COMPANY = G1.CD_COMPANY 	
		LEFT OUTER JOIN PU_PRH PRH ON PRH.CD_COMPANY = PRL.CD_COMPANY  -- 요청부서, 요청자조회조건때문에추가
							AND PRH.NO_PR = PRL.NO_PR
							AND PRH.CD_PLANT  = PRL.CD_PLANT

		 LEFT OUTER JOIN  DZSN_MA_EMP E1 ON PRH.NO_EMP= E1.NO_EMP    
									AND  E1.CD_COMPANY =@P_CD_COMPANY
		 LEFT OUTER JOIN DZSN_MA_DEPT DE ON PRH.CD_DEPT = DE.CD_DEPT   
									AND @P_CD_COMPANY = DE.CD_COMPANY 
		 LEFT OUTER JOIN DZSN_MA_PARTNER MPA ON MPA.CD_COMPANY = MP.CD_COMPANY AND MPA.CD_PARTNER = MP.PARTNER							
		WHERE 
		    (( PRL.CD_PURGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_REQ_PURGRP))) OR (@P_REQ_PURGRP = '' ) OR (@P_REQ_PURGRP  IS NULL)) --요청구매그룹  
		AND (( PRH.CD_DEPT  =@P_REQ_DEPT) OR (@P_REQ_DEPT = '' ) OR (@P_REQ_DEPT  IS NULL)) --요청부서
		AND (( PRH.NO_EMP  =@P_REQ_EMP) OR (@P_REQ_EMP = '' ) OR (@P_REQ_EMP  IS NULL)) -- 요청인원	
	END