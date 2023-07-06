USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SO_MNG_WINTEC_CHECK_S]    Script Date: 2019-11-14 오후 3:11:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_SO_MNG_WINTEC_CHECK_S]
(
    @P_CD_COMPANY           NVARCHAR(7),    --회사
    @P_MULTI_NO_SO          NVARCHAR(4000), --수주번호
    @P_CD_PLANT             NVARCHAR(7),    --공장
    @P_TP_BUSI              NVARCHAR(3),    --거래구분
    @P_STA_SO               NVARCHAR(3),    --수주상태
    @P_NO_PROJECT           NVARCHAR(20),   --프로젝트
    @P_YN_AFTER             NVARCHAR(1),    --변경여부
    @P_DT_SO_FROM           NCHAR(8),       --수주일자
    @P_DT_SO_TO             NCHAR(8),       --수주일자
    @P_DT_GUBUN             NVARCHAR(4),    --'SO' 수주일자 조회, 'DU' 납기일자 조회
    @P_FG_LANG              NVARCHAR(4) = NULL	--언어
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_QT_FSIZE		NUMERIC(3,0),     
		@V_QT_UPDOWN    NVARCHAR(3),      
		@V_QT_FORMAT	NVARCHAR(50)

EXEC UP_SF_GETUNIT_QT @P_CD_COMPANY, 'SA', 'I', @V_QT_FSIZE OUTPUT, @V_QT_UPDOWN OUTPUT, @V_QT_FORMAT OUTPUT 

-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG

SELECT 'N' S,        
       SL.NO_SO,           --수주번호        
       SL.SEQ_SO,        
       SH.CD_PARTNER,      --거래처        
       SL.CD_PLANT,        --공장코드        
       P.YN_AFTER,         --프로젝트 라인이 결재후 변경 되었는지를 체크 변경이'Y'      
       SL.CD_ITEM,         --품목코드        
       IT.NM_ITEM,         --품목명        
       IT.STND_ITEM,       --규격        
       IT.UNIT_SO,         --단위
       SL.DT_EXPECT,	   --최초납기요구일
       SL.DT_DUEDATE,      --납기요구일        
       SL.DT_REQGI,        --출하예정일        
       SL.QT_SO,           --수량                
       SL.UM_SO,           --단가        
       SL.AM_SO,           --금액        
       SL.AM_WONAMT,       --원화금액        
       SL.AM_VAT,          --부가세        
       SL.TP_BUSI,         --거래구분        
       SL.STA_SO,          --수주상태
       SL.CD_SL,                
       --(SELECT NM_SL FROM DZSN_MA_SL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_PLANT = SL.CD_PLANT AND CD_SL = SL.CD_SL) NM_SL,
       MS.NM_SL,  
       IT.UNIT_IM,         --관리단위        
       SL.QT_IM,           --관리수량        
       SL.QT_GI,        
       SL.QT_GIR,        
       SL.QT_LC,         
       SL.GI_PARTNER,  
       PT.LN_PARTNER AS NM_GI_PARTNER,               
       SL.CD_ITEM_PARTNER, --거래처품번  
       SL.NM_ITEM_PARTNER, --거래처품명  
       SL.DC1,             --비고1  
       SL.DC2,             --비고2  
       INV.QT_INV,         --현재고
       SL.ID_STA_HST,      -- 이전 변경자  
       (SELECT NM_USER FROM DZSN_MA_USER WHERE CD_COMPANY = @P_CD_COMPANY AND ID_USER = SL.ID_STA_HST) AS NM_ID_STA_HST,   --이전 변경자  
       SL.ID_UPDATE,       -- 변경자  
       (SELECT NM_USER FROM DZSN_MA_USER WHERE CD_COMPANY = @P_CD_COMPANY AND ID_USER = SL.ID_UPDATE) AS NM_ID_UPDATE,     --변경자  
       SUBSTRING(SL.DTS_STA_HST,1,8) AS DTS_STA_HST,       --이전 변경일  
       SUBSTRING(SL.DTS_UPDATE,1,8) AS DTS_UPDATE,         --변경일  
       (SL.AM_WONAMT + SL.AM_VAT) AS AM_SUM,               --합계 컬럼  
       CAST(SE.RT_MARGIN AS NUMERIC(17,1)) AS RT_MARGIN,   --마진율  
       SL.UMVAT_SO,        --단가 부가세 포함
       IT.STND_DETAIL_ITEM,
       SL.NO_PO_PARTNER,
       SL.NO_POLINE_PARTNER,
       SH.CD_EXCH,
       IT.YN_ATP,
       SL.FG_USE,
       IT.NO_DESIGN,
       SL.NUM_USERDEF1, 
       SL.NUM_USERDEF2, 
       SL.NUM_USERDEF3, 
       SL.NUM_USERDEF5, 
       SL.NUM_USERDEF6,
       SL.TXT_USERDEF1, 
       SL.TXT_USERDEF2, 
       SL.TXT_USERDEF3,
       EJ.YN_AM, 
       SH.RT_EXCH, 
       SL.RT_VAT,
       IT.MAT_ITEM,
       
       ISNULL((SELECT TOP 1 B.ST_PROC
               FROM SA_GIRL A
	           LEFT JOIN SA_DLV_L B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_GIR = B.NO_REQ AND A.SEQ_GIR = B.NO_REQ_LINE
	           WHERE A.CD_COMPANY = @P_CD_COMPANY 
               AND A.NO_SO = SL.NO_SO 
               AND A.SEQ_SO = SL.SEQ_SO), 'O') AS ST_PROC,
       SL.FG_USE2,
       SL.UM_BASE,
       SL.RT_DSCNT,
       IT.CLS_ITEM,
       SL.NO_PROJECT, 
       (SELECT NM_PROJECT FROM DZSN_SA_PROJECTH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_PROJECT = SL.NO_PROJECT) AS NM_PROJECT,
       IT.NUM_USERDEF1 AS PITEM_NUM_USERDEF1,
       IG.CD_ITEMGRP,
       IG.NM_ITEMGRP,
       IT.GRP_MFG,
       CD1.NM_SYSDEF AS NM_GRP_MFG,
       SL.NO_RELATION, 
       SL.SEQ_RELATION,
       IT.FG_SERNO, 
       IT.UNIT_SO_FACT,
       SL.NO_IO_MGMT, 
       SL.NO_IOLINE_MGMT,
       MS.MNG_SL,
       IT.EN_ITEM,
       DLV.TXT_USERDEF1 AS TEMPUR_DLV_TXT_USERDEF1, 
       (CASE WHEN ISNULL(SL.QT_PO, 0) > 0 THEN 'Y' ELSE 'N' END) AS FG_PO,
       (SELECT TOP 1 NM_CUST_DLV FROM DZSN_SA_SOL_DLV WHERE CD_COMPANY  = @P_CD_COMPANY AND NO_SO = SL.NO_SO AND SEQ_SO = SL.SEQ_SO) AS NM_CUST_DLV,
       SH.TXT_USERDEF4,
       IT.CD_USERDEF3 AS WISE_PITEM_CD_USERDEF3,
       
       (SELECT UD_NM_01 FROM SA_LINK WHERE CD_COMPANY = SL.CD_COMPANY AND SL.NO_RELATION = NO_LINK AND SL.SEQ_RELATION = NO_LINE_LINK) AS UD_NM_01,
       (SELECT UD_NM_03 FROM SA_LINK WHERE CD_COMPANY = SL.CD_COMPANY AND SL.NO_RELATION = NO_LINK AND SL.SEQ_RELATION = NO_LINE_LINK) AS UD_NM_03,
       (SELECT UD_NM_06 FROM SA_LINK WHERE CD_COMPANY = SL.CD_COMPANY AND SL.NO_RELATION = NO_LINK AND SL.SEQ_RELATION = NO_LINE_LINK) AS UD_NM_06,
       SH.DT_SO,
       0 AS QT_USE,
       IT.WEIGHT,
       NEOE.FN_SF_GETUNIT_QT2('SA', '', @V_QT_FSIZE, @V_QT_UPDOWN, @V_QT_FORMAT, IT.WEIGHT * SL.QT_SO) AS QT_WEIGHT,
	   SL.TXT_USERDEF4,
	   SL.TXT_USERDEF5,
	   SL.TXT_USERDEF6,
	   SL.TXT_USERDEF7,
	   
	   CD3.NM_SYSDEF AS CSFOOD_NM_TXT_USERDEF1,
	   CD4.NM_SYSDEF AS CSFOOD_NM_TXT_USERDEF2,
	   CD5.NM_SYSDEF AS CSFOOD_NM_TXT_USERDEF3,
	   CD6.NM_SYSDEF AS CSFOOD_NM_TXT_USERDEF4,
	   IT.PARTNER AS ITEM_PARTNER,
	   SH.TXT_USERDEF1 AS SOH_TXT_USERDEF1,
	   SL.CD_MNGD1,
	   SL.CD_CC,
	   MC.NM_CC,
       
	   IT.NO_MODEL,
	   IT.NO_DESIGN,
	   (ISNULL(SL.QT_SO, 0) - ISNULL(SL.QT_GI, 0)) AS QT_NOT_GI,
       OL.QT_IO_RETURN AS QT_RETURN,
	   OL.DT_IO AS DT_OUT,
	   DATEDIFF(DAY, SL.DT_DUEDATE, ISNULL(OL.DT_IO, CONVERT(CHAR(8), GETDATE(), 112))) AS DT_DELAY,
	   (CASE WHEN SL.NUM_USERDEF4 <> 0 THEN SL.NUM_USERDEF4 
	   								   ELSE (SELECT TOP 1 NM_SYSDEF
	   									     FROM MA_CODEDTL 
	   									     WHERE CD_COMPANY = SH.CD_COMPANY
	   									     AND CD_FIELD = 'CZ_WIN0005'
	   									     AND CD_FLAG1 = SH.CD_PARTNER
	   									     AND CD_FLAG2 <= DATEDIFF(DAY, SL.DT_DUEDATE, ISNULL(OL.DT_IO, CONVERT(CHAR(8), GETDATE(), 112)))
	   									     ORDER BY CONVERT(INT, NM_SYSDEF)) END) AS QT_SCORE,
	   SL.NUM_USERDEF3,
	   SL.DC1,
       SL.YN_OPTION,
       SL.CD_USERDEF1,
       SL.CD_USERDEF2,
       SL.CD_USERDEF3
FROM SA_SOH SH  
JOIN SA_SOL SL ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO  
JOIN DZSN_MA_PITEM IT ON SL.CD_COMPANY = IT.CD_COMPANY AND SL.CD_PLANT = IT.CD_PLANT AND SL.CD_ITEM = IT.CD_ITEM  
LEFT JOIN SA_PROJECTL P ON SL.CD_COMPANY = P.CD_COMPANY AND SL.NO_PROJECT = P.NO_PROJECT AND SL.SEQ_PROJECT = P.SEQ_PROJECT   
LEFT JOIN DZSN_MA_PARTNER PT ON SL.CD_COMPANY = PT.CD_COMPANY AND SL.GI_PARTNER = PT.CD_PARTNER   
LEFT JOIN V_MM_PINVN INV ON SL.CD_COMPANY = INV.CD_COMPANY AND SL.CD_PLANT = INV.CD_PLANT AND SL.CD_ITEM = INV.CD_ITEM AND SL.CD_SL = INV.CD_SL
--LEFT JOIN  MM_PINVN INV  ON  (SL.CD_COMPANY = INV.CD_COMPANY AND SL.CD_PLANT = INV.CD_PLANT AND SL.CD_ITEM = INV.CD_ITEM AND SL.CD_SL = INV.CD_SL AND P_YR = SUBSTRING(@P_DT_SO_FROM, 1, 4))      
LEFT JOIN SA_PJTL_PRE SE ON SH.CD_COMPANY = SE.CD_COMPANY AND IT.CD_PLANT = SE.CD_PLANT AND SL.NO_PROJECT = SE.CD_PJT AND P.SEQ_PROJECT = SE.NO_SEQ
LEFT JOIN DZSN_SA_TPSO ST ON SH.CD_COMPANY = ST.CD_COMPANY AND SH.TP_SO = ST.TP_SO
LEFT JOIN DZSN_MM_EJTP EJ ON ST.CD_COMPANY = EJ.CD_COMPANY AND ST.TP_GI = EJ.CD_QTIOTP
LEFT JOIN DZSN_MA_ITEMGRP IG ON IT.CD_COMPANY = IG.CD_COMPANY AND IT.GRP_ITEM = IG.CD_ITEMGRP
LEFT JOIN DZSN_MA_CODEDTL CD1 ON IT.CD_COMPANY = CD1.CD_COMPANY AND CD1.CD_FIELD = 'MA_B000066' AND CD1.CD_SYSDEF = IT.GRP_MFG
LEFT JOIN DZSN_MA_SL MS ON SL.CD_COMPANY = MS.CD_COMPANY AND SL.CD_PLANT = MS.CD_PLANT AND SL.CD_SL = MS.CD_SL
LEFT JOIN DZSN_SA_SOL_DLV DLV ON  SL.CD_COMPANY = DLV.CD_COMPANY AND SL.NO_SO = DLV.NO_SO AND SL.SEQ_SO = DLV.SEQ_SO AND DLV.FG_TRACK = 'SO'
LEFT JOIN DZSN_MA_CODEDTL CD3 ON CD3.CD_COMPANY = SL.CD_COMPANY AND CD3.CD_FIELD = 'CZ_SHIP001' AND CD3.CD_SYSDEF = SL.TXT_USERDEF1
LEFT JOIN DZSN_MA_CODEDTL CD4 ON CD4.CD_COMPANY = SL.CD_COMPANY AND CD4.CD_FIELD = 'CZ_SHIP002' AND CD4.CD_SYSDEF = SL.TXT_USERDEF2
LEFT JOIN DZSN_MA_CODEDTL CD5 ON CD5.CD_COMPANY = SL.CD_COMPANY AND CD5.CD_FIELD = 'CZ_SHIP003' AND CD5.CD_SYSDEF = SL.TXT_USERDEF3
LEFT JOIN DZSN_MA_CODEDTL CD6 ON CD6.CD_COMPANY = SL.CD_COMPANY AND CD6.CD_FIELD = 'CZ_SHIP004' AND CD6.CD_SYSDEF = SL.TXT_USERDEF4
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = SL.CD_COMPANY AND MC.CD_CC = SL.CD_CC
LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
				  MAX(OH.DT_IO) AS DT_IO,
				  SUM(CASE WHEN OH.YN_RETURN = 'Y' THEN OL.QT_IO ELSE 0 END) AS QT_IO_RETURN 
		   FROM MM_QTIOH OH
		   JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
		   WHERE OL.FG_PS = '2'
		   GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) OL
ON OL.CD_COMPANY = SL.CD_COMPANY AND OL.NO_PSO_MGMT = SL.NO_SO AND OL.NO_PSOLINE_MGMT = SL.SEQ_SO
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.NO_SO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_NO_SO))
AND (ISNULL(@P_STA_SO, '')  = '' OR SL.STA_SO = @P_STA_SO)  
AND (ISNULL(@P_YN_AFTER, '') = '' OR ISNULL(P.YN_AFTER, 'N') = @P_YN_AFTER)
AND (ISNULL(@P_NO_PROJECT, '') = '' OR SL.NO_PROJECT = @P_NO_PROJECT) --2011.04.01 추가

GO