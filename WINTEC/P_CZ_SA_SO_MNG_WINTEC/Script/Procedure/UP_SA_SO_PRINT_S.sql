USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SO_PRINT_S]    Script Date: 2019-11-14 오후 3:14:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_SA_SO_PRINT_S]            
(            
    @P_CD_COMPANY   NVARCHAR(7),            
    @P_NO_SO        NVARCHAR(20),
    @P_SEQ_SO		NUMERIC(5,0) = NULL,
    @P_FG_LANG     NVARCHAR(4) = NULL	--언어
)            
AS

-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG
           
 DECLARE
		@V_YM			NVARCHAR(6),
		@V_CD_PLANT		NVARCHAR(7),
		@V_FSIZE_AM     NUMERIC(3,0),
		@V_UPDOWN_AM	NVARCHAR(3),
		@V_FORMAT_AM	NVARCHAR(50),
		@V_DT_SO		NVARCHAR(8),
		@V_YN_PJT       NCHAR(1),   --프로젝트형 사용여부
        @V_YN_UNIT		NCHAR(1)    --UNIT 사용여부
        
SET     @V_YN_PJT   = 'N'
SET     @V_YN_UNIT  = 'N'
		
EXEC UP_SF_GETUNIT_AM @P_CD_COMPANY, 'SA', 'I', @V_FSIZE_AM OUTPUT, @V_UPDOWN_AM OUTPUT, @V_FORMAT_AM OUTPUT
IF @@ERROR <> 0 RETURN		
		
SELECT	@V_YM		= SUBSTRING(DT_SO, 1, 6)
FROM	SA_SOH 
WHERE	CD_COMPANY	= @P_CD_COMPANY AND NO_SO = @P_NO_SO 

SELECT	@V_DT_SO = DT_SO
FROM	SA_SOH 
WHERE	CD_COMPANY	= @P_CD_COMPANY AND NO_SO = @P_NO_SO 

SELECT	@V_CD_PLANT	= MIN(CD_PLANT)
FROM	SA_SOL
WHERE	CD_COMPANY	= @P_CD_COMPANY 
AND NO_SO = @P_NO_SO 
AND (SEQ_SO = @P_SEQ_SO  OR @P_SEQ_SO IS NULL)
	
SELECT	@V_YM		= MAX(YM_STANDARD)
FROM	MM_AMINVL
WHERE	CD_COMPANY	= @P_CD_COMPANY AND CD_PLANT = @V_CD_PLANT AND YM_STANDARD <= @V_YM 
	
SET     @V_YM = ISNULL(@V_YM, '000000')

SELECT  @V_YN_PJT   = YN_PJT,
        @V_YN_UNIT  = YN_UNIT
FROM    MA_ENV
WHERE   CD_COMPANY  = @P_CD_COMPANY

SELECT  A.NO_SO, A.SEQ_SO, A.STA_SO AS STA_SO1, A.TP_ITEM, A.CD_PLANT,         
        A.CD_ITEM, B.NM_ITEM, B.STND_ITEM, B.UNIT_SO, B.MAT_ITEM, B.GRP_MFG,         
        CASE WHEN ISNULL(B.UNIT_SO_FACT, 0) = 0         
        THEN 1 ELSE B.UNIT_SO_FACT END UNIT_SO_FACT,         
        A.DT_DUEDATE, A.DT_REQGI, A.QT_SO, A.UM_SO, A.AM_SO,         
        A.AM_WONAMT, A.AM_VAT, B.UNIT_IM, A.QT_IM, A.CD_SL,         
        ISNULL(B.LT_GI, 0) LT_GI, MS.NM_SL,
        A.GI_PARTNER, MP.LN_PARTNER AS NM_GI_PARTNER,
        MP.NO_TEL1, MP.NO_FAX1, MP.CD_EMP_PARTNER, MP.DC_ADS1_H, MP.DC_ADS1_D,        
        A.DC1, A.DC2, A.NO_PROJECT, SH.NM_PROJECT, A.SEQ_PROJECT, A.CD_ITEM_PARTNER, A.NM_ITEM_PARTNER,         
        A.UMVAT_SO, A.AMVAT_SO, B.GRP_ITEM, MI.NM_ITEMGRP, B.DT_VALID,         
        ISNULL(MG.AM_ITEM, 0) AM_ITEM, MC.NM_SYSDEF, A.CD_SHOP, A.CD_SPITEM, A.CD_OPT,
        D.NM_CUST_DLV, D.CD_ZIP, D.ADDR1, D.ADDR2, D.NO_TEL_D1, D.NO_TEL_D2,         
        D.TP_DLV, NEOE.FN_MA_GET_CODEDTL_NM_SYSDEF(@P_CD_COMPANY, 'EC_0000002', D.TP_DLV) AS NM_TP_DLV,
        D.DC_REQ, D.FG_TRACK,
        ISNULL(A.RT_DSCNT, 0) AS RT_DSCNT, ISNULL(A.UM_BASE, 0) AS UM_BASE, D.TP_DLV_DUE, 
        ISNULL(B.FG_MODEL, 'N') AS FG_MODEL, 
        A.FG_USE, A.CD_CC, C.NM_CC, A.TP_VAT, A.RT_VAT, 
        ISNULL(A.UMVAT_SO, 0) AS UMVAT_SO, ISNULL(A.AMVAT_SO, 0) AS AMVAT_SO, 
        ME.NM_KOR AS NM_MANAGER1, 
        A.NO_IO_MGMT, A.NO_IOLINE_MGMT, A.NO_POLINE_PARTNER,
        -- 2010.07.20 : 장은경 - 중량, 중량단위 추가
        ISNULL(A.UM_OPT, 0) UM_OPT, ISNULL(B.WEIGHT, 0.0) WEIGHT, B.UNIT_WEIGHT,
        -- 2010.07.22 : 장은경 - 예상이익 산출
        ISNULL(MM.UM_INV, 0.0) UM_INV,
        NEOE.FN_SF_GETUNIT_AM('SA', '', @V_FSIZE_AM, @V_UPDOWN_AM, @V_FORMAT_AM, (A.AM_WONAMT - ISNULL(MM.UM_INV, 0.0) * A.QT_SO)) AM_PROFIT,
        -- 2010.07.27 : 장은경 - 라인 거래처 po번호 추가
        A.NO_PO_PARTNER,
        B.YN_ATP,
        B.CUR_ATP_DAY,
        A.CD_WH,
        WH.NM_WH,
		A.NO_SO_ORIGINAL,
		A.SEQ_SO_ORIGINAL,
		A.NUM_USERDEF1,
		A.NUM_USERDEF2,
		MC1.NM_SYSDEF AS NM_GRP_MFG,
		X.QT_INV AS SL_QT_INV,
		B.FG_SERNO, --002:LOT, 003:SERIAL
		H.CD_EXCH,   --환종추가 20110804 SJH
		B.QT_WIDTH * 1000 AS QT_WIDTH, 
		B.QT_LENGTH,
		B.QT_WIDTH * B.QT_LENGTH AS AREA,
		B.QT_WIDTH * B.QT_LENGTH * A.QT_SO AS TOTAL_AREA,
		B.NM_MAKER,
        0.0 AS QT_USEINV,   --가용재고(한국화장품에서 사용)
		A.NUM_USERDEF3, A.NUM_USERDEF4, A.NUM_USERDEF5, A.NUM_USERDEF6,
		ISNULL(B.NUM_USERDEF3, 0) AS PITEM_NUM_USERDEF3,
		(CASE WHEN ISNULL(B.UNIT_GI_FACT, 0) = 0 THEN 1 ELSE B.UNIT_GI_FACT END) AS UNIT_GI_FACT,
		A.QT_SO * ISNULL(B.NUM_USERDEF3, 0) AS AM_PACKING,
		A.QT_SO / (CASE WHEN ISNULL(B.UNIT_GI_FACT, 0) = 0 THEN 1 ELSE B.UNIT_GI_FACT END) AS QT_PACKING,
		B.NUM_USERDEF4 AS PITEM_NUM_USERDEF4, B.NUM_USERDEF5 AS PITEM_NUM_USERDEF5, B.NUM_USERDEF6 AS PITEM_NUM_USERDEF6,    --2011.12.21 SJH(PIMS:D20111205101)
		B.NUM_USERDEF7 AS PITEM_NUM_USERDEF7,    --2012.04.25 SJH(PIMS:D20120417017)
		B.EN_ITEM,
		A.CD_MNGD1, F1.NM_MNGD AS NM_MNGD1, A.CD_MNGD2, F2.NM_MNGD AS NM_MNGD2, A.CD_MNGD3, F3.NM_MNGD AS NM_MNGD3, A.CD_MNGD4,
		A.TXT_USERDEF1, A.TXT_USERDEF2, B.UNIT_GI, B.STND_DETAIL_ITEM, A.YN_OPTION,
		ISNULL(B.NUM_USERDEF1 * A.QT_SO,0) AS AM_STD_SALE,
        CASE WHEN ISNULL(B.NUM_USERDEF1 * A.QT_SO,0) = 0 THEN 0 
             ELSE ROUND(((ISNULL(B.NUM_USERDEF1 * A.QT_SO,0) - ISNULL(A.AM_WONAMT,0)) / ISNULL(B.NUM_USERDEF1 * A.QT_SO,0)) * 100,2) END RT_STD_SALE,
        B.NUM_USERDEF1 AS PITEM_NUM_USERDEF1, B.NUM_USERDEF2 AS PITEM_NUM_USERDEF2,
        SL.CD_ITEM AS CD_UNIT,
        UNIT.NM_ITEM AS NM_UNIT,
        UNIT.STND_ITEM AS STND_UNIT,
        D.NO_ORDER,
        D.NM_CUST,
        D.NO_TEL1,
        D.NO_TEL2,
        D.TXT_USERDEF1 AS DLV_TXT_USERDEF1,
        A.TP_IV,
        FLOOR(A.QT_SO / (CASE WHEN ISNULL(B.UNIT_GI_FACT, 0) = 0 THEN 1 ELSE B.UNIT_GI_FACT END)) AS QT_PACK,
        (A.QT_SO % (CASE WHEN ISNULL(B.UNIT_GI_FACT, 0) = 0 THEN 1 ELSE B.UNIT_GI_FACT END)) AS QT_REMAIN,
        S.CD_COURSE, (SELECT NM_SYSDEF FROM DZSN_MA_CODEDTL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'SA_B000066' AND CD_SYSDEF = S.CD_COURSE) NM_COURSE,
        (SELECT TOP 1 CI.CD_ITEM_PARTNER FROM DZSN_PU_CUSTITEM CI WHERE CI.CD_COMPANY = @P_CD_COMPANY AND CI.CD_PLANT = A.CD_PLANT AND CI.CD_PARTNER = H.CD_PARTNER AND A.CD_ITEM = CI.CD_ITEM) CD_ITEM_PARTNER,
        (SELECT TOP 1 CI.NM_ITEM_PARTNER FROM DZSN_PU_CUSTITEM CI WHERE CI.CD_COMPANY = @P_CD_COMPANY AND CI.CD_PLANT = A.CD_PLANT AND CI.CD_PARTNER = H.CD_PARTNER AND A.CD_ITEM = CI.CD_ITEM) NM_ITEM_PARTNER,
        (SELECT TOP 1 CI.UNIT_IM_PARTNER FROM DZSN_PU_CUSTITEM CI WHERE CI.CD_COMPANY = @P_CD_COMPANY AND CI.CD_PLANT = A.CD_PLANT AND CI.CD_PARTNER = H.CD_PARTNER AND A.CD_ITEM = CI.CD_ITEM) UNIT_IM_PARTNER,
        MP.DC_ADS1_H GI_DC_ADS1_H, MP.DC_ADS1_D GI_DC_ADS1_D,
        MP1.NM_TEXT,
        MP.NO_TEL1 AS GI_NO_TEL1, -- 2014.07.02 권기석 추가, 납품처의 전화번호
        MP.NO_FAX1 AS GI_NO_FAX1,  -- 2014.07.02 권기석 추가, 납품처의 팩스번호
        B.NO_MODEL,
        H.DT_SO,
        H.NO_EMP,	ME2.NM_KOR,
        H.DC_RMK,
        MP1.DC_ADS1_H,
        H.DC_RMK1,
        MP.NO_COMPANY, --사업자등록번호
        MP.TP_JOB, --업태
        MP.CLS_JOB, --종목
        MP.NM_CEO, --대표자명
        H.CD_PARTNER,
        MP2.LN_PARTNER AS NM_PARTNER,
        MP2.LN_PARTNER AS LN_PARTNER,
        B.CD_USERDEF3 PITEM_CD_USERDEF3, B.NUM_USERDEF1 PITEM_NUM_USERDEF1, B.NO_HS,
        CASE WHEN ISNULL(B.NUM_USERDEF1,0) = 0 THEN 0 ELSE CONVERT(NUMERIC(17,4), A.QT_SO / B.NUM_USERDEF1) END AS PCS, -- 다이킨코리아 전용
        H.CD_TRANSPORT,  -- 다이킨코리아 전용
        MP3.LN_PARTNER AS NM_TRANSPROT,  -- 다이킨코리아 전용
        MP3.DC_ADS1_H,  -- 다이킨코리아 전용
        MP3.DC_ADS1_D,  -- 다이킨코리아 전용
        MP3.NO_TEL1,  -- 다이킨코리아 전용
        H.COND_PRICE,  -- 다이킨코리아 전용
        (SELECT NM_SYSDEF FROM DZSN_MA_CODEDTL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'TR_IM00002' AND CD_SYSDEF = H.COND_PRICE) NM_COND_PRICE,
        H.FG_TRANSPORT,  -- 다이킨코리아 전용
        (SELECT NM_SYSDEF FROM DZSN_MA_CODEDTL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'TR_IM00008' AND CD_SYSDEF = H.FG_TRANSPORT) NM_FG_TRANSPORT,
        H.COND_PAY,  -- 다이킨코리아 전용
        (SELECT NM_SYSDEF FROM DZSN_MA_CODEDTL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'TR_IM00004' AND CD_SYSDEF = H.COND_PAY) NM_COND_PAY,
        (SELECT NM_SYSDEF FROM DZSN_MA_CODEDTL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'MA_B000005' AND CD_SYSDEF = H.CD_EXCH) NM_EXCH,
        H.TP_SO,
        TS.NM_SO,
        A.AM_SO / 10 AS AM_SO_VAT, --다이킨코리아
        MP3.DC_ADS3_H, --다이킨코리아
        MP3.DC_ADS3_D, --다이킨코리아
        MP3.NO_TEL3, --다이킨코리아
        0.0 AS AM_SO_HAP,
        0.0 AS AM_TOTAL,
        A.UM_EX_PO,
        A.TXT_USERDEF3,
        (SELECT MAX(DT_LIMIT) FROM PU_POL WHERE CD_COMPANY = A.CD_COMPANY AND NO_SO = A.NO_SO AND NO_SOLINE = A.SEQ_SO) PU_DT_LIMIT, --한국쯔바키모도
        BR.NO_FAX AS NO_FAX_BIZAREA,
        PH.DC_RMK_TEXT2 AS DC_RMK_TEXT2_PO,
        H.RT_EXCH * A.UM_SO AS UM_SOL, --쯔바키모토 전용
        MP.NO_CUER_AGENCY AS NO_CUER_AGENCY_GI,
        MP.NM_CURE_AGENCY AS NM_CURE_AGENCY_GI, 
        MP1.NO_CUER_AGENCY, 
        MP1.NM_CURE_AGENCY,
        MP1.NM_PTR AS SOH_NM_PTR,
        MP.NM_PTR AS SOL_NM_PTR,
        H.COND_TRANS,
        H.PORT_LOADING,
        H.PORT_ARRIVER,
		ME2.NM_ENG,
		(SELECT NM_SYSDEF FROM DZSN_MA_CODEDTL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'MA_B000004' AND CD_SYSDEF = B.UNIT_IM) AS NM_UNIT_IM	,
		A.TXT_USERDEF4,
		MP2.DC_ADS1_H AS DC_ADS1_H_PARTNER,
		MP2.DC_ADS1_D AS DC_ADS1_D_PARTNER,
		H.NO_INV
  FROM  SA_SOL A
  INNER JOIN  SA_SOH H ON A.CD_COMPANY = H.CD_COMPANY AND A.NO_SO = H.NO_SO
  LEFT OUTER JOIN DZSN_SA_SOL_DLV D ON A.CD_COMPANY = D.CD_COMPANY AND A.NO_SO = D.NO_SO AND A.SEQ_SO = D.SEQ_SO AND D.FG_TRACK = 'SO'
  LEFT OUTER JOIN DZSN_MA_PITEM  B  ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PLANT = B.CD_PLANT AND A.CD_ITEM = B.CD_ITEM
  LEFT OUTER JOIN DZSN_MA_EMP ME ON A.CD_COMPANY = ME.CD_COMPANY AND B.NO_MANAGER1 = ME.NO_EMP        
  LEFT OUTER JOIN DZSN_MA_CODEDTL MC ON B.CD_COMPANY = MC.CD_COMPANY AND MC.CD_FIELD = 'MA_B000031' AND B.CLS_M = MC.CD_SYSDEF
  LEFT OUTER JOIN DZSN_MA_CODEDTL MC1 ON B.CD_COMPANY = MC1.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000066' AND B.GRP_MFG = MC1.CD_SYSDEF
  LEFT OUTER JOIN DZSN_MA_SL MS ON A.CD_COMPANY = MS.CD_COMPANY AND A.CD_PLANT = MS.CD_PLANT AND A.CD_SL = MS.CD_SL
  LEFT OUTER JOIN DZSN_MA_PARTNER  MP ON A.CD_COMPANY = MP.CD_COMPANY AND A.GI_PARTNER = MP.CD_PARTNER
  LEFT OUTER JOIN MA_GRPITEM_UM_HDS MG ON A.CD_COMPANY = MG.CD_COMPANY AND A.CD_PLANT = MG.CD_PLANT AND B.GRP_ITEM = MG.GRP_ITEM AND H.TP_PRICE = MG.FG_UM        
  LEFT OUTER JOIN DZSN_MA_ITEMGRP MI ON A.CD_COMPANY = MI.CD_COMPANY AND B.GRP_ITEM = MI.CD_ITEMGRP
  LEFT OUTER JOIN DZSN_MA_CC C ON C.CD_COMPANY = A.CD_COMPANY AND C.CD_CC = A.CD_CC
  LEFT OUTER JOIN (SELECT * FROM MM_AMINVL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_PLANT = @V_CD_PLANT AND YM_STANDARD = @V_YM) MM
  ON A.CD_COMPANY = MM.CD_COMPANY AND A.CD_PLANT = MM.CD_PLANT AND A.CD_ITEM = MM.CD_ITEM AND
  (@V_YN_PJT  = 'N' OR (@V_YN_PJT  = 'Y' AND MM.CD_PJT = A.NO_PROJECT)) AND  
  (@V_YN_UNIT = 'N' OR (@V_YN_UNIT = 'Y' AND MM.SEQ_PROJECT = A.SEQ_PROJECT))
  LEFT OUTER JOIN DZSN_SA_PROJECTH SH ON A.CD_COMPANY = SH.CD_COMPANY AND A.NO_PROJECT = SH.NO_PROJECT
  LEFT OUTER JOIN DZSN_MA_WH WH ON A.CD_COMPANY = WH.CD_COMPANY AND A.CD_WH = WH.CD_WH
  LEFT OUTER JOIN DZSN_FI_MNGD F1 ON F1.CD_MNG = 'A21' AND A.CD_MNGD1 = F1.CD_MNGD AND A.CD_COMPANY = F1.CD_COMPANY
  LEFT OUTER JOIN DZSN_FI_MNGD F2 ON F2.CD_MNG = 'A22' AND A.CD_MNGD2 = F2.CD_MNGD AND A.CD_COMPANY = F2.CD_COMPANY
  LEFT OUTER JOIN DZSN_FI_MNGD F3 ON F3.CD_MNG = 'A25' AND A.CD_MNGD3 = F3.CD_MNGD AND A.CD_COMPANY = F3.CD_COMPANY
  LEFT OUTER JOIN MA_PARTNER_SUB S ON H.CD_COMPANY = S.CD_COMPANY AND H.CD_PARTNER = S.CD_PARTNER   
  LEFT OUTER JOIN
    ( 
        SELECT  Y.CD_PLANT, Y.CD_SL, Y.CD_ITEM, 
                SUM(Y.QT_INV) QT_INV
        FROM    (  
                    SELECT  CD_COMPANY, CD_PLANT, CD_SL, CD_ITEM, QT_GOOD_INV QT_INV       
                    FROM    MM_OPENQTL        
                    WHERE   CD_COMPANY = @P_CD_COMPANY 
                    AND     YM_STANDARD = LEFT(@V_DT_SO,4) + '00'

                    UNION ALL

                    SELECT  CD_COMPANY, L.CD_PLANT, L.CD_SL, L.CD_ITEM,       
                            L.QT_GOOD_GR - L.QT_GOOD_GI + L.QT_REJECT_GR - L.QT_REJECT_GI + L.QT_TRANS_GR - L.QT_TRANS_GI + L.QT_INSP_GR - L.QT_INSP_GI  QT_INV      
                    FROM    MM_OHSLINVD  L   
                    WHERE   L.CD_COMPANY = @P_CD_COMPANY
                    AND     L.DT_IO BETWEEN LEFT(@V_DT_SO,4) + '0101' AND @V_DT_SO
                ) Y
                LEFT OUTER JOIN DZSN_MA_PITEM MI ON Y.CD_COMPANY = MI.CD_COMPANY AND Y.CD_PLANT = MI.CD_PLANT AND Y.CD_ITEM = MI.CD_ITEM
        WHERE   MI.CLS_ITEM IN ('001', '002', '003', '004', '005', '009')
        GROUP BY Y.CD_PLANT, Y.CD_SL, Y.CD_ITEM
    ) X ON A.CD_PLANT = X.CD_PLANT AND A.CD_ITEM = X.CD_ITEM AND A.CD_SL = X.CD_SL
  LEFT OUTER JOIN SA_PROJECTL SL ON A.CD_COMPANY = SL.CD_COMPANY AND A.NO_PROJECT = SL.NO_PROJECT AND A.SEQ_PROJECT = SL.SEQ_PROJECT
  LEFT OUTER JOIN DZSN_MA_PITEM UNIT ON SL.CD_COMPANY = UNIT.CD_COMPANY AND SL.CD_PLANT = UNIT.CD_PLANT AND SL.CD_ITEM = UNIT.CD_ITEM
  LEFT OUTER JOIN DZSN_MA_PARTNER MP1 ON H.CD_COMPANY = MP1.CD_COMPANY AND H.CD_PARTNER = MP1.CD_PARTNER    
  LEFT OUTER JOIN DZSN_MA_EMP ME2 ON H.CD_COMPANY = ME2.CD_COMPANY AND H.NO_EMP = ME2.NO_EMP    
  LEFT OUTER JOIN DZSN_MA_PARTNER  MP2 ON H.CD_COMPANY = MP2.CD_COMPANY AND H.CD_PARTNER = MP2.CD_PARTNER
  LEFT OUTER JOIN DZSN_MA_PARTNER  MP3 ON H.CD_COMPANY = MP3.CD_COMPANY AND H.CD_TRANSPORT = MP3.CD_PARTNER
  LEFT OUTER JOIN DZSN_SA_TPSO TS ON TS.CD_COMPANY = H.CD_COMPANY AND TS.TP_SO = H.TP_SO
  LEFT OUTER JOIN DZSN_MA_BIZAREA BR ON BR.CD_COMPANY = H.CD_COMPANY AND BR.CD_BIZAREA = H.CD_BIZAREA
  LEFT OUTER JOIN PU_POL PL ON A.CD_COMPANY = PL.CD_COMPANY AND A.NO_SO = PL.NO_SO AND A.SEQ_SO = PL.NO_SOLINE
  LEFT OUTER JOIN PU_POH PH ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO 
 WHERE  A.CD_COMPANY = @P_CD_COMPANY
 AND    A.NO_SO = @P_NO_SO
 AND	(A.SEQ_SO = @P_SEQ_SO OR @P_SEQ_SO IS NULL)
 ORDER BY A.SEQ_SO
GO

