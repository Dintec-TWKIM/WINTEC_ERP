USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_IV_REGH_SUB_S]    Script Date: 2015-06-23 오후 4:38:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*****************************************
**  System : 구매                         
**  Sub System : 매입                         
**  Page  : 매입등록
**  Desc  :  매입등록- 국내일경우도움창
**  Return Values                            
**   
                         
**  작   성   자 :  오성영                        
**  작   성   일 :  2008-07-03
**  수   정   자 :                             
*********************************************                            
** Change History                     
** 2008.10.30 구매승인서 도움창 쿼리 기준으로 대폭 수정             
** 2009.12.28 구매요청 구매그룹 조건추가, 수불형태 조건추가 -SMR(김헌섭/KT,크라제)
** 2010.03.08 창고조건추가 -SMR(김헌섭/KT,크라제)
** 2012.08.29 시스템통제설정 (CIS)로 승인체크(YN_STATUS4)된 입고건만 조회
*********************************************/
ALTER PROCEDURE [NEOE].[SP_CZ_PU_IV_REG_SUB_S]      
(      
	@P_CD_COMPANY		NVARCHAR(7),    
	@P_DT_FROM			NVARCHAR(8),  
	@P_DT_TO			NVARCHAR(8),           
	@P_CD_PARTNER		NVARCHAR(20),      
	@P_NO_PO			NVARCHAR(20),
	@P_NO_IO			NVARCHAR(20),
	@P_FG_TRANS			NVARCHAR(3)    
)
AS  

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

CREATE TABLE #MM_QTIO
(
	CD_COMPANY			NVARCHAR(7),
	CD_PLANT			NVARCHAR(7),
	NO_IO				NVARCHAR(20),
	NO_IOLINE			NUMERIC(5, 0),
	CD_PARTNER			NVARCHAR(20),
	CD_ITEM				NVARCHAR(20),
	CD_PJT				NVARCHAR(20),
	FG_TAX				NVARCHAR(3),
	DT_IO				NCHAR(8),
	CD_EXCH				NVARCHAR(3),
	NO_PSO_MGMT			NVARCHAR(20),
	NO_PSOLINE_MGMT		NUMERIC(5, 0),
	NO_ISURCV			NVARCHAR(20),
	NO_ISURCVLINE		NUMERIC(5, 0),
	SEQ_PROJECT			NUMERIC(5, 0),
	CD_UNIT_MM			NVARCHAR(3),
	QT_UNIT_MM			NUMERIC(17, 4),
	QT_CLS_MM			NUMERIC(17, 4),
	QT_IO				NUMERIC(17, 4),
	QT_CLS				NUMERIC(17, 4),
	UM_EX				NUMERIC(15, 4),
	AM					NUMERIC(17, 4),
	NO_IO_MGMT			NVARCHAR(20),
	NO_IOLINE_MGMT		NUMERIC(5, 0),
	NO_LC				NVARCHAR(20),
	NO_LCLINE			NUMERIC(5, 0),
	FG_TPIO				NCHAR(3),
	NO_EMP				NVARCHAR(10),
	CD_QTIOTP			NCHAR(3),
	TP_UM_TAX			NVARCHAR(3),
	NO_WBS				NVARCHAR(20),
	NO_CBS				NVARCHAR(20),
	GI_PARTNER			NVARCHAR(20),
	CD_SL				NVARCHAR(7),
	UM_WEIGHT			NUMERIC(17, 4),
	TOT_WEIGHT			NUMERIC(17, 4)
)

CREATE TABLE #MM_QTIO1
(
	CD_COMPANY			NVARCHAR(7),
	CD_PLANT			NVARCHAR(7),
	NO_IO				NVARCHAR(20),
	NO_IOLINE			NUMERIC(5, 0),
	CD_PARTNER			NVARCHAR(20),
	CD_ITEM				NVARCHAR(20),
	CD_PJT				NVARCHAR(20),
	FG_TAX				NVARCHAR(3),
	DT_IO				NCHAR(8),
	CD_EXCH				NVARCHAR(3),
	NO_PSO_MGMT			NVARCHAR(20),
	NO_PSOLINE_MGMT		NUMERIC(5, 0),
	NO_ISURCV			NVARCHAR(20),
	NO_ISURCVLINE		NUMERIC(5, 0),
	SEQ_PROJECT			NUMERIC(5, 0),
	CD_UNIT_MM			NVARCHAR(3),
	QT_UNIT_MM			NUMERIC(17, 4),
	QT_CLS_MM			NUMERIC(17, 4),
	QT_IO				NUMERIC(17, 4),
	QT_CLS				NUMERIC(17, 4),
	UM_EX				NUMERIC(15, 4),
	NO_IO_MGMT			NVARCHAR(20),
	NO_IOLINE_MGMT		NUMERIC(5, 0),
	NO_LC				NVARCHAR(20),
	NO_LCLINE			NUMERIC(5, 0),
	FG_TPIO				NCHAR(3),
	NO_EMP				NVARCHAR(10),
	CD_QTIOTP			NCHAR(3),
	TP_UM_TAX			NVARCHAR(3),
	NO_WBS				NVARCHAR(20),
	NO_CBS				NVARCHAR(20),
	GI_PARTNER			NVARCHAR(20),
	CD_SL				NVARCHAR(7),
	UM_WEIGHT			NUMERIC(17, 4),
	TOT_WEIGHT			NUMERIC(17, 4)
)

CREATE NONCLUSTERED INDEX MM_QTIO ON #MM_QTIO (CD_COMPANY, NO_IO);

CREATE NONCLUSTERED INDEX MM_QTIO1 ON #MM_QTIO1 (CD_COMPANY, NO_IO);

INSERT INTO #MM_QTIO
(
	CD_COMPANY,
	CD_PLANT,
	NO_IO,
	NO_IOLINE,
	CD_PARTNER,
	CD_ITEM,
	CD_PJT,
	FG_TAX,
	DT_IO,
	CD_EXCH,
	NO_PSO_MGMT,
	NO_PSOLINE_MGMT,
	NO_ISURCV,
	NO_ISURCVLINE,
	SEQ_PROJECT,
	CD_UNIT_MM,
	QT_UNIT_MM,
	QT_CLS_MM,
	QT_IO,
	QT_CLS,
	UM_EX,
	AM,
	NO_IO_MGMT,
	NO_IOLINE_MGMT,
	NO_LC,
	NO_LCLINE,
	FG_TPIO,
	NO_EMP,
	CD_QTIOTP,
	TP_UM_TAX,
	NO_WBS,
	NO_CBS,
	GI_PARTNER,
	CD_SL,
	UM_WEIGHT,
	TOT_WEIGHT
)
SELECT IH.CD_COMPANY,
	   IH.CD_PLANT,
	   IH.NO_IO,
	   IL.NO_IOLINE,
	   IL.CD_PARTNER,
	   IL.CD_ITEM,
	   IL.CD_PJT,
	   IL.FG_TAX,
	   IL.DT_IO,
	   IL.CD_EXCH,
	   IL.NO_PSO_MGMT,
	   IL.NO_PSOLINE_MGMT,
	   IL.NO_ISURCV,
	   IL.NO_ISURCVLINE,
	   IL.SEQ_PROJECT,
	   IL.CD_UNIT_MM,
	   IL.QT_UNIT_MM,
	   IL.QT_CLS_MM,
	   IL.QT_IO,
	   IL.QT_CLS,
	   IL.UM_EX,
	   IL.AM,
	   IL.NO_IO_MGMT,
	   IL.NO_IOLINE_MGMT,
	   IL.NO_LC,
	   IL.NO_LCLINE,
	   IL.FG_TPIO,
	   IL.NO_EMP,
	   IL.CD_QTIOTP,
	   IL.TP_UM_TAX,
	   IL.NO_WBS,
	   IL.NO_CBS,
	   IL.GI_PARTNER,
	   IL.CD_SL,
	   IL.UM_WEIGHT,
	   IL.TOT_WEIGHT
FROM MM_QTIOH IH
JOIN MM_QTIO IL ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IO = IH.NO_IO
WHERE IH.CD_COMPANY = @P_CD_COMPANY
AND IH.DT_IO BETWEEN @P_DT_FROM AND @P_DT_TO
AND IH.FG_TRANS = @P_FG_TRANS
AND (ISNULL(@P_NO_IO, '') = '' OR IH.NO_IO = @P_NO_IO)
AND (ISNULL(@P_CD_PARTNER, '') = '' OR IH.CD_PARTNER = @P_CD_PARTNER)
AND IL.FG_PS ='1'             
AND IL.FG_IO IN ('001','030') --구매입고 
AND IL.YN_AM ='Y'         
AND IL.YN_PURSALE = 'Y'          
AND IL.CD_BIN <> 'Y'
AND IL.QT_UNIT_MM > IL.QT_CLS_MM
AND IL.QT_IO > IL.QT_CLS
AND (ISNULL(@P_NO_PO, '') = '' OR IL.NO_PSO_MGMT LIKE @P_NO_PO + '%')
AND (IL.CD_ITEM NOT LIKE 'SD%' OR NOT EXISTS (SELECT 1 
											  FROM PU_IVL IV
											  WHERE IV.CD_COMPANY = IL.CD_COMPANY
											  AND IV.NO_PO = IL.NO_PSO_MGMT
											  AND IV.NO_POLINE = IL.NO_PSOLINE_MGMT))

INSERT INTO #MM_QTIO1
(
	CD_COMPANY,
	CD_PLANT,
	NO_IO,
	NO_IOLINE,
	CD_PARTNER,
	CD_ITEM,
	CD_PJT,
	FG_TAX,
	DT_IO,
	CD_EXCH,
	NO_PSO_MGMT,
	NO_PSOLINE_MGMT,
	NO_ISURCV,
	NO_ISURCVLINE,
	SEQ_PROJECT,
	CD_UNIT_MM,
	QT_UNIT_MM,
	QT_CLS_MM,
	QT_IO,
	QT_CLS,
	UM_EX,
	NO_IO_MGMT,
	NO_IOLINE_MGMT,
	NO_LC,
	NO_LCLINE,
	FG_TPIO,
	NO_EMP,
	CD_QTIOTP,
	TP_UM_TAX,
	NO_WBS,
	NO_CBS,
	GI_PARTNER,
	CD_SL,
	UM_WEIGHT,
	TOT_WEIGHT
)
SELECT CD_COMPANY,
	   CD_PLANT,
	   NO_IO,
	   NO_IOLINE,
	   CD_PARTNER,
	   CD_ITEM,
	   CD_PJT,
	   FG_TAX,
	   DT_IO,
	   CD_EXCH,
	   NO_PSO_MGMT,
	   NO_PSOLINE_MGMT,
	   NO_ISURCV,
	   NO_ISURCVLINE,
	   SEQ_PROJECT,
	   CD_UNIT_MM,
	   QT_UNIT_MM,
	   QT_CLS_MM,
	   QT_IO,
	   QT_CLS,
	   UM_EX,
	   NO_IO_MGMT,
	   NO_IOLINE_MGMT,
	   NO_LC,
	   NO_LCLINE,
	   FG_TPIO,
	   NO_EMP,
	   CD_QTIOTP,
	   TP_UM_TAX,
	   NO_WBS,
	   NO_CBS,
	   GI_PARTNER,
	   CD_SL,
	   UM_WEIGHT,
	   TOT_WEIGHT
FROM #MM_QTIO
--UNION ALL
--SELECT IL1.CD_COMPANY,
--	   IL1.CD_PLANT,
--	   IL1.NO_IO,
--	   IL1.NO_IOLINE,
--	   IL1.CD_PARTNER,
--	   PL.CD_ITEM,
--	   IL1.CD_PJT,
--	   IL1.FG_TAX,
--	   IL1.DT_IO,
--	   IL1.CD_EXCH,
--	   PL.NO_PO AS NO_PSO_MGMT,
--	   PL.NO_LINE AS NO_PSOLINE_MGMT,
--	   IL1.NO_ISURCV,
--	   IL1.NO_ISURCVLINE,
--	   IL1.SEQ_PROJECT,
--	   IL1.CD_UNIT_MM,
--	   PL.QT_PO AS QT_UNIT_MM,
--	   0 AS QT_CLS_MM,
--	   PL.QT_PO AS QT_IO,
--	   0 AS QT_CLS,
--	   PL.UM_EX AS UM_EX,
--	   IL1.NO_IO_MGMT,
--	   IL1.NO_IOLINE_MGMT,
--	   IL1.NO_LC,
--	   IL1.NO_LCLINE,
--	   IL1.FG_TPIO,
--	   IL1.NO_EMP,
--	   IL1.CD_QTIOTP,
--	   IL1.TP_UM_TAX,
--	   IL1.NO_WBS,
--	   IL1.NO_CBS,
--	   IL1.GI_PARTNER,
--	   IL1.CD_SL,
--	   IL1.UM_WEIGHT,
--	   IL1.TOT_WEIGHT
--FROM PU_POL PL
--JOIN (SELECT IL.*,
--	  	     ROW_NUMBER() OVER (PARTITION BY IL.CD_COMPANY, IL.NO_PSO_MGMT ORDER BY IH.AM_IO DESC, IL.NO_IOLINE DESC) AS IDX
--	  FROM #MM_QTIO IL
--	  JOIN (SELECT CD_COMPANY, NO_IO,
--	  			   SUM(AM) AS AM_IO
--	  	    FROM #MM_QTIO
--	  	    GROUP BY CD_COMPANY, NO_IO) IH
--	  ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO) IL1
--ON IL1.CD_COMPANY = PL.CD_COMPANY AND IL1.NO_PSO_MGMT = PL.NO_PO AND IL1.IDX = 1
--WHERE PL.CD_COMPANY = @P_CD_COMPANY
--AND PL.CD_ITEM LIKE 'SD%'
--AND ISNULL(PL.QT_RCV, 0) = 0
--AND NOT EXISTS (SELECT 1 
--				FROM PU_IVL IV
--				WHERE IV.CD_COMPANY = PL.CD_COMPANY
--				AND IV.NO_PO = PL.NO_PO
--				AND IV.NO_POLINE = PL.NO_LINE)

;WITH A AS
(
	SELECT IL.CD_COMPANY, IL.NO_IO,
		   MAX(IL.FG_TAX) AS FG_TAX,
		   MAX(IL.NO_PSO_MGMT) AS NO_PO,
		   MAX(PL.CD_PJT) AS NO_SO,
		   MAX(IL.NO_EMP) AS NO_EMP,
		   SUM(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2)) AS AM_EX,
		   SUM(ROUND(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END))) AS AM,
		   SUM(ROUND(ROUND(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) * (CONVERT(NUMERIC(17, 4), CASE WHEN ISNULL(MC.CD_FLAG1,'') = '' THEN '0' ELSE MC.CD_FLAG1 END) / 100), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END))) AS VAT,
		   SUM(ROUND(ISNULL(AP.AM, 0) * ((ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)) / ISNULL(PL.QT_PO_MM, 1)), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END))) AS AP_AM,
		   SUM(CASE WHEN AP.YN_TRANS = 'N' THEN 1 ELSE 0 END) AS QT_NOT_TRANS
	FROM #MM_QTIO1 IL
	LEFT JOIN PU_POL PL ON PL.CD_COMPANY = IL.CD_COMPANY AND PL.NO_PO = IL.NO_PSO_MGMT AND PL.NO_LINE = IL.NO_PSOLINE_MGMT
	LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = IL.CD_COMPANY AND MC.CD_FIELD = 'MA_B000046' AND MC.CD_SYSDEF = IL.FG_TAX
	LEFT JOIN MA_EXCHANGE EX ON EX.CD_COMPANY = IL.CD_COMPANY AND EX.YYMMDD = IL.DT_IO AND EX.NO_SEQ = '1' AND EX.CURR_SOUR = IL.CD_EXCH AND EX.CURR_DEST = '000'
	LEFT JOIN (SELECT AP.CD_COMPANY, AP.NO_PO, AP.NO_POLINE, 
			   	      SUM(AP.AM) AS AM,
					  (CASE WHEN SUM(FD.TRANS_AMT) > 0 THEN 'Y' ELSE 'N' END) AS YN_TRANS
			   FROM PU_ADPAYMENT AP
			   LEFT JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU,
						  	     SUM(BD.TRANS_AMT) AS TRANS_AMT
						  FROM FI_DOCU FD
						  JOIN BANK_SENDD BD ON BD.C_CODE = FD.CD_COMPANY AND BD.NO_DOCU = FD.NO_DOCU AND BD.LINE_NO = FD.NO_DOLINE
						  GROUP BY FD.CD_COMPANY, FD.NO_MDOCU) FD
			   ON FD.CD_COMPANY = AP.CD_COMPANY AND FD.NO_MDOCU = AP.NO_ADPAY
			   WHERE EXISTS (SELECT 1
			   			     FROM (SELECT AP1.CD_COMPANY, AP1.NO_ADPAY, AP1.AM AS AM_ADPAY, 0 AS AM_BILLS
			   			     	   FROM PU_ADPAYMENT AP1
			   				       WHERE AP1.CD_COMPANY = AP.CD_COMPANY
			   				       AND AP1.NO_ADPAY = AP.NO_ADPAY
			   			     	   UNION ALL
			   			     	   SELECT AP1.CD_COMPANY, AP1.NO_ADPAY, 0 AS AM_ADPAY, AP1.AM_BILLS AS AM_BILLS 
			   			     	   FROM CZ_PU_ADPAYMENT_BILL_L AP1
			   				       WHERE AP1.CD_COMPANY = AP.CD_COMPANY
			   				       AND AP1.NO_ADPAY = AP.NO_ADPAY) AP
			   			     GROUP BY AP.CD_COMPANY, AP.NO_ADPAY
			   			     HAVING SUM(AP.AM_ADPAY) > SUM(AP.AM_BILLS))
			   GROUP BY AP.CD_COMPANY, AP.NO_PO, AP.NO_POLINE) AP
	ON AP.CD_COMPANY = IL.CD_COMPANY AND AP.NO_PO = IL.NO_PSO_MGMT AND AP.NO_POLINE = IL.NO_PSOLINE_MGMT
	WHERE NOT EXISTS(SELECT 1
				     FROM PU_IVH VH
				     JOIN PU_IVL VL ON VH.CD_COMPANY = VL.CD_COMPANY AND VH.NO_IV = VL.NO_IV
				     WHERE VH.CD_COMPANY = IL.CD_COMPANY      
                     AND VL.NO_PO = IL.NO_PSO_MGMT                                         
                     AND VH.YN_EXPIV = 'Y'               
                     AND VH.YN_PURSUB = 'N')
	GROUP BY IL.CD_COMPANY, IL.NO_IO
)
SELECT 'N' AS S,
	   'N' AS YN_CONFIRM,
	   IH.NO_IO,
	   IH.DT_IO,
	   IH.CD_PARTNER,
	   (SELECT LN_PARTNER FROM MA_PARTNER MP WHERE MP.CD_COMPANY = IH.CD_COMPANY AND MP.CD_PARTNER = IH.CD_PARTNER) AS LN_PARTNER,
	   (CASE WHEN IH.YN_RETURN = 'Y' THEN -ISNULL(IL.AM_EX, 0) ELSE ISNULL(IL.AM_EX, 0) END) AS AM_PO_EX,
	   (CASE WHEN IH.YN_RETURN = 'Y' THEN -ISNULL(IL.AM, 0) ELSE ISNULL(IL.AM, 0) END) AS AM_PO,
	   (CASE WHEN IH.YN_RETURN = 'Y' THEN -ISNULL(IL.VAT, 0) ELSE ISNULL(IL.VAT, 0) END) AS VAT,
	   (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ISNULL(IL.AM, 0) + ISNULL(IL.VAT, 0))  
									 ELSE (ISNULL(IL.AM, 0) + ISNULL(IL.VAT, 0)) END) AS AM_TOT,
	   ISNULL(IL.AP_AM, 0) AS AM_ADPAY,
	   (CASE WHEN ISNULL(IL.AP_AM, 0) > 0 AND IL.QT_NOT_TRANS = 0 THEN 'Y' ELSE 'N' END) AS YN_TRANS,
	   IH.FG_TRANS,
	   (SELECT NM_SYSDEF FROM MA_CODEDTL MC WHERE MC.CD_COMPANY = IH.CD_COMPANY AND MC.CD_FIELD = 'PU_C000016' AND MC.CD_SYSDEF = IH.FG_TRANS) AS NM_TRANS,
	   IL.FG_TAX,
	   IL.NO_PO,
	   IL.NO_SO,
	   PH.NO_EMP,
	   PH.NO_ORDER,
	   ME.CD_CC,
	   (SELECT NM_KOR FROM MA_EMP ME WHERE ME.CD_COMPANY = PH.CD_COMPANY AND ME.NO_EMP = PH.NO_EMP) AS NM_PO_EMP,
	   (SELECT NM_KOR FROM MA_EMP ME WHERE ME.CD_COMPANY = IH.CD_COMPANY AND ME.NO_EMP = IH.NO_EMP) AS NM_IO_EMP,
	   (SELECT NM_VESSEL FROM SA_SOH SH JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO WHERE SH.CD_COMPANY = PH.CD_COMPANY AND SH.NO_SO = PH.CD_PJT) AS NM_VESSEL,
	   IH.DC_RMK,
	   IC.DC_RMK AS DC_RMK_CONFIRM,
	   IC.DC_RMK_WF,
	   IC.AM_EX,
	   IC.DT_END
FROM MM_QTIOH IH
JOIN A IL ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IO = IH.NO_IO
LEFT JOIN PU_POH PH ON PH.CD_COMPANY = IL.CD_COMPANY AND PH.NO_PO = IL.NO_PO
LEFT JOIN CZ_PU_IV_CONFIRM IC ON IC.CD_COMPANY = IH.CD_COMPANY AND IC.NO_IO = IH.NO_IO AND IC.TP_CONFIRM = '001'
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = PH.CD_COMPANY AND ME.NO_EMP = PH.NO_EMP
WHERE IH.CD_COMPANY = @P_CD_COMPANY
AND IH.DT_IO BETWEEN @P_DT_FROM AND @P_DT_TO
AND IH.FG_TRANS = @P_FG_TRANS
AND (ISNULL(@P_NO_IO, '') = '' OR IH.NO_IO = @P_NO_IO)
AND (ISNULL(@P_CD_PARTNER, '') = '' OR IH.CD_PARTNER = @P_CD_PARTNER)

;WITH A AS
(
	SELECT IH.CD_COMPANY,
		   IH.NO_IO,
		   IH.DT_IO,
		   IH.YN_RETURN,     
		   IH.FG_TRANS,
		   MP.LN_PARTNER,
		   MP.NO_COMPANY,
		   MP.TP_TAX,
		   MP.FG_PAYBILL, 
		   MP.DT_PAY_PREARRANGED,
		   MP.TP_PAY_DD, -- 자금지급예정구분(당월,차월:MA_B000097)
		   ISNULL(MP.DT_PAY_DD, '') AS DT_PAY_DD, -- 자금지급예정일
		   MP.YN_JEONJA,
		   IL.CD_ITEM,
		   IL.CD_UNIT_MM,
		   MI.NM_ITEM,
		   MI.UNIT_IM,
		   MI.STND_ITEM,
		   MI.PARTNER AS PI_PARTNER,
		   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = MI.CD_COMPANY AND CD_PARTNER = MI.PARTNER) AS PI_LN_PARTNER,   
		   (CASE ISNULL(MI.UNIT_PO_FACT,1) WHEN 0 THEN 1 ELSE ISNULL(MI.UNIT_PO_FACT,1) END) AS RATE_EXCHG,   --유지영: 구매단위계수가져오기위해수정       
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ISNULL(IL.QT_UNIT_MM, 0) - ISNULL(IL.QT_CLS_MM, 0)) ELSE (ISNULL(IL.QT_UNIT_MM, 0) - ISNULL(IL.QT_CLS_MM, 0)) END) AS QT_INV,   -- 매입잔량 => 매입잔량      
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ISNULL(IL.QT_UNIT_MM, 0) - ISNULL(IL.QT_CLS_MM, 0)) ELSE (ISNULL(IL.QT_UNIT_MM, 0) - ISNULL(IL.QT_CLS_MM, 0)) END) AS QT_IV,  -- 처리수량 => 적용수량(수량변경가능)      
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)) ELSE (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)) END) AS QT_INV_CLS, -- 잔여수량 => 매입잔량(관리)      
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)) ELSE (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)) END) AS QT_CLS, -- 관리수량 => 적용(관리)수량
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -ISNULL(IL.UM_EX, 0) ELSE ISNULL(IL.UM_EX, 0) END) AS UM_EX,  --외화단가
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ROUND(ISNULL(IL.UM_EX, 0) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END))) 
		   								 ELSE ROUND(ISNULL(IL.UM_EX, 0) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) END) AS UM, --원화단가
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2)) 
		   								 ELSE ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) END) AS AM_EX, --외화금액
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ROUND(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END))) 
		   								 ELSE ROUND(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) END) AS AM_IV, --원화금액
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ROUND(ROUND(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) * (CONVERT(NUMERIC(17, 4), CASE WHEN ISNULL(MC.CD_FLAG1,'') = '' THEN '0' ELSE MC.CD_FLAG1 END) / 100), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END))) 
		   								 ELSE ROUND(ROUND(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) * (CONVERT(NUMERIC(17, 4), CASE WHEN ISNULL(MC.CD_FLAG1,'') = '' THEN '0' ELSE MC.CD_FLAG1 END) / 100), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) END) AS VAT_IV, --부가세
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -(ROUND(ROUND(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) * (1 + (CONVERT(NUMERIC(17, 4), CASE WHEN ISNULL(MC.CD_FLAG1,'') = '' THEN '0' ELSE MC.CD_FLAG1 END) / 100)), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END))) 
		   							     ELSE ROUND(ROUND(ROUND(ISNULL(IL.UM_EX, 0) * (ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)), 2) * ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) * (1 + (CONVERT(NUMERIC(17, 4), CASE WHEN ISNULL(MC.CD_FLAG1,'') = '' THEN '0' ELSE MC.CD_FLAG1 END) / 100)), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) END) AS AM_TOTAL, --총금액
		   ROUND(ISNULL(AP.AM, 0) * ((ISNULL(IL.QT_IO, 0) - ISNULL(IL.QT_CLS, 0)) / ISNULL(PL.QT_PO_MM, 1)), (CASE WHEN IL.CD_COMPANY = 'S100' THEN 2 ELSE 0 END)) AS AM_ADPAY,
		   IL.CD_EXCH,
		   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = IL.CD_COMPANY AND CD_FIELD = 'MA_B000005' AND CD_SYSDEF = IL.CD_EXCH) AS NM_EXCH,
		   ISNULL((CASE WHEN EX.CURR_SOUR = '002' THEN EX.RATE_BASE ELSE ROUND(EX.RATE_BASE, 2) END), 1) AS RT_EXCH,         
		   IL.FG_TAX,
		   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = IL.CD_COMPANY AND CD_FIELD = 'MA_B000046' AND CD_SYSDEF = IL.FG_TAX) AS NM_TAX,    
		   IL.NO_IO_MGMT, 
		   IL.NO_IOLINE_MGMT,
		   IL.NO_LC,
		   IL.NO_LCLINE,
		   IL.CD_PLANT,         
		   IL.FG_TPIO AS FG_TPPURCHASE,         
		   IL.CD_PJT,
		   PJ.NM_PROJECT,
		   PJ.CD_PARTNER AS CD_PARTNER_PJT,
		   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = PJ.CD_COMPANY AND CD_PARTNER = PJ.CD_PARTNER) AS LN_PARTNER_PJT,
		   PJ.NO_EMP AS NO_EMP_PJT,
		   (SELECT NM_KOR FROM MA_EMP WHERE CD_COMPANY = PJ.CD_COMPANY AND NO_EMP = PJ.NO_EMP) AS NM_KOR_PJT,
		   PJ.END_USER AS END_USER,
		   IL.SEQ_PROJECT, 
		   IL.NO_EMP,
		   IL.NO_IOLINE,
		   IL.CD_PARTNER,         
		   IL.NO_PSO_MGMT AS NO_PO,         
		   IL.NO_PSOLINE_MGMT AS NO_POLINE,
		   IL.CD_QTIOTP,
		   (SELECT NM_QTIOTP FROM MM_EJTP WHERE CD_COMPANY = IL.CD_COMPANY AND CD_QTIOTP = IL.CD_QTIOTP) AS NM_QTIOTP, --G.CD_CC,
		   (CASE WHEN (ISNULL(IL.TP_UM_TAX, '002') = '002' OR LEN(IL.TP_UM_TAX) < 1) THEN '002' ELSE '001' END) AS TP_UM_TAX,
		   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = IL.CD_COMPANY AND CD_FIELD = 'PU_C000005' AND CD_SYSDEF = ISNULL(IL.TP_UM_TAX, '002')) AS NM_TP_UM_TAX,
		   IL.NO_WBS,    
		   IL.NO_CBS,
		   IL.GI_PARTNER, 
		   (SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = IL.CD_COMPANY AND CD_PARTNER = IL.GI_PARTNER) AS GI_LN_PARTNER,
		   IL.CD_SL,
		   (SELECT NM_SL FROM MA_SL WHERE CD_COMPANY = IL.CD_COMPANY AND CD_PLANT = IL.CD_PLANT AND CD_SL = IL.CD_SL) AS NM_SL,
		   IL.UM_WEIGHT,
		   (CASE WHEN IH.YN_RETURN = 'Y' THEN -1 * (IL.QT_UNIT_MM - IL.QT_CLS_MM) * MI.WEIGHT 
		   								 ELSE (CASE WHEN IL.QT_CLS_MM = 0 THEN IL.TOT_WEIGHT
		   							 							          ELSE (IL.QT_UNIT_MM - IL.QT_CLS_MM) * MI.WEIGHT END) END) AS TOT_WEIGHT, --유니포인트전용
		   (CASE WHEN ISNULL(MC.CD_FLAG1,'') = '' THEN '0' ELSE MC.CD_FLAG1 END) AS TAX_RATE,
		   PH.CD_PURGRP AS CD_GROUP,
		   PG.NM_PURGRP,
		   PH.FG_PAYMENT,
		   (SELECT NM_KOR FROM MA_EMP WHERE CD_COMPANY = PH.CD_COMPANY AND NO_EMP = PH.NO_EMP) AS NM_KOR,
		   (CASE WHEN ISNULL(PL.CD_CC,'') = '' THEN PG.CD_CC ELSE PL.CD_CC END) AS CD_CC,-- 발주CC가 NULL 이면 구매그룹 CC
		   (SELECT NM_CC FROM MA_CC WHERE CD_COMPANY = PL.CD_COMPANY AND CD_CC = (CASE WHEN ISNULL(PL.CD_CC,'') = '' THEN PG.CD_CC ELSE PL.CD_CC END)) AS NM_CC,
		   PL.NO_APP,
		   PL.DC1,
		   PL.DC2,
		   (SELECT ISNULL(CD_DOCU, (CASE WHEN @P_FG_TRANS = '001' THEN '45' ELSE '46' END)) FROM MA_AISPOSTH WHERE CD_COMPANY = IL.CD_COMPANY AND CD_TP = IL.FG_TPIO AND FG_TP = '200') AS CD_DOCU,
		   PO.TP_PURPRICE,
		   PO.PO_PRICE,
		   (CASE WHEN ISNULL(PH.FG_UM,'') <> '' THEN PH.FG_UM ELSE (SELECT FG_UM FROM PU_RCVH WHERE CD_COMPANY = RL.CD_COMPANY AND NO_RCV = RL.NO_RCV) END) AS FG_UM,
		   RL.DC_RMK AS DC_RMK1,
		   RL.DC_RMK2 AS DC_RMK2,
		   QL.CD_ITEM_PARTNER, 
		   QL.NM_ITEM_PARTNER, 
		   QL.NO_DSP,
		   JL.CD_ITEM AS CD_PJT_ITEM, --프로젝트 품목코드    
		   JI.NM_ITEM AS NM_PJT_ITEM, --프로젝트 품목명    
		   JI.STND_ITEM AS PJT_ITEM_STND, --프로젝트 품목 규격    
		   JG.CD_PJTGRP,
		   JG.NM_PJTGRP,
		   (SELECT NM_TPPO FROM PU_TPPO WHERE CD_COMPANY = PH.CD_COMPANY AND CD_TPPO = PH.CD_TPPO) AS NM_TPPO
	FROM MM_QTIOH IH
	JOIN #MM_QTIO1 IL ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IO = IH.NO_IO
	JOIN MA_PITEM MI ON MI.CD_COMPANY = IL.CD_COMPANY AND MI.CD_PLANT = IL.CD_PLANT AND MI.CD_ITEM = IL.CD_ITEM
	JOIN MA_PARTNER MP ON MP.CD_COMPANY = IH.CD_COMPANY AND MP.CD_PARTNER = IH.CD_PARTNER
	LEFT JOIN SA_PROJECTH PJ ON PJ.CD_COMPANY = IL.CD_COMPANY AND PJ.NO_PROJECT = IL.CD_PJT
	LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = IL.CD_COMPANY AND MC.CD_FIELD = 'MA_B000046' AND MC.CD_SYSDEF = IL.FG_TAX
	LEFT JOIN MA_EXCHANGE EX ON EX.CD_COMPANY = IL.CD_COMPANY AND EX.YYMMDD = IL.DT_IO AND EX.NO_SEQ = '1' AND EX.CURR_SOUR = IL.CD_EXCH AND EX.CURR_DEST = '000'
	LEFT JOIN PU_POL PL ON PL.CD_COMPANY = IL.CD_COMPANY AND PL.NO_PO = IL.NO_PSO_MGMT AND PL.NO_LINE = IL.NO_PSOLINE_MGMT
	LEFT JOIN PU_POH PH ON PH.CD_COMPANY = PL.CD_COMPANY AND PH.NO_PO = PL.NO_PO
	LEFT JOIN MA_PURGRP PG ON PG.CD_COMPANY = PH.CD_COMPANY AND PG.CD_PURGRP = PH.CD_PURGRP
	LEFT JOIN MA_PURORG PO ON PO.CD_COMPANY = PG.CD_COMPANY AND PO.CD_PURORG = PG.CD_PURORG 
	LEFT JOIN PU_RCVL RL ON RL.CD_COMPANY = IL.CD_COMPANY AND RL.NO_RCV = IL.NO_ISURCV AND RL.NO_LINE = IL.NO_ISURCVLINE 
	LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = PL.CD_COMPANY AND QL.NO_FILE = PL.NO_SO AND QL.NO_LINE = PL.NO_SOLINE
	LEFT JOIN MA_PJTGRP JG ON JG.CD_COMPANY = PJ.CD_COMPANY AND JG.CD_PJTGRP = PJ.CD_PJTGRP
	LEFT JOIN SA_PROJECTL JL ON JL.CD_COMPANY = IL.CD_COMPANY AND JL.NO_PROJECT = IL.CD_PJT AND JL.SEQ_PROJECT = IL.SEQ_PROJECT
	LEFT JOIN MA_PITEM JI ON JI.CD_COMPANY = JL.CD_COMPANY AND JI.CD_PLANT = JL.CD_PLANT AND JI.CD_ITEM = JL.CD_ITEM
	LEFT JOIN (SELECT AP.CD_COMPANY, AP.NO_PO, AP.NO_POLINE, 
			 	      SUM(AP.AM) AS AM
			   FROM PU_ADPAYMENT AP
			   WHERE EXISTS (SELECT 1
			 			     FROM (SELECT AP1.CD_COMPANY, AP1.NO_ADPAY, AP1.AM AS AM_ADPAY, 0 AS AM_BILLS
			 			     	   FROM PU_ADPAYMENT AP1
			 				       WHERE AP1.CD_COMPANY = AP.CD_COMPANY
			 				       AND AP1.NO_ADPAY = AP.NO_ADPAY
			 			     	   UNION ALL
			 			     	   SELECT AP1.CD_COMPANY, AP1.NO_ADPAY, 0 AS AM_ADPAY, AP1.AM_BILLS AS AM_BILLS 
			 			     	   FROM CZ_PU_ADPAYMENT_BILL_L AP1
			 				       WHERE AP1.CD_COMPANY = AP.CD_COMPANY
			 				       AND AP1.NO_ADPAY = AP.NO_ADPAY) AP
			 			     GROUP BY AP.CD_COMPANY, AP.NO_ADPAY
			 			     HAVING SUM(AP.AM_ADPAY) > SUM(AP.AM_BILLS))
			 GROUP BY AP.CD_COMPANY, AP.NO_PO, AP.NO_POLINE) AP
	ON AP.CD_COMPANY = IL.CD_COMPANY AND AP.NO_PO = IL.NO_PSO_MGMT AND AP.NO_POLINE = IL.NO_PSOLINE_MGMT
	WHERE IH.CD_COMPANY = @P_CD_COMPANY
	AND IH.DT_IO BETWEEN @P_DT_FROM AND @P_DT_TO
	AND (ISNULL(@P_NO_IO, '') = '' OR IH.NO_IO = @P_NO_IO)
	AND IH.FG_TRANS = @P_FG_TRANS
	AND (ISNULL(@P_CD_PARTNER, '') = '' OR IH.CD_PARTNER = @P_CD_PARTNER)
)
SELECT 'N' AS S,
	   CD_COMPANY,
	   NO_IO,
	   DT_IO,
	   YN_RETURN,     
	   FG_TRANS,
	   LN_PARTNER,
	   NO_COMPANY,
	   TP_TAX,
	   FG_PAYBILL, 
	   DT_PAY_PREARRANGED,
	   TP_PAY_DD,
	   DT_PAY_DD,
	   YN_JEONJA,
	   CD_ITEM,
	   CD_UNIT_MM,
	   NM_ITEM,
	   UNIT_IM,
	   STND_ITEM,
	   PI_PARTNER,
	   PI_LN_PARTNER,   
	   RATE_EXCHG,
	   QT_INV,
	   QT_IV,
	   QT_INV_CLS,
	   QT_CLS,
	   UM_EX,
	   UM,
	   AM_EX,
	   AM_IV,
	   VAT_IV,
	   AM_TOTAL,
	   AM_ADPAY,
	   CD_EXCH,
	   NM_EXCH,
	   RT_EXCH,         
	   FG_TAX,
	   NM_TAX,    
	   NO_IO_MGMT, 
	   NO_IOLINE_MGMT,
	   NO_LC,
	   NO_LCLINE,
	   CD_PLANT,         
	   FG_TPPURCHASE,         
	   CD_PJT,
	   NM_PROJECT,
	   CD_PARTNER_PJT,
	   LN_PARTNER_PJT,
	   NO_EMP_PJT,
	   NM_KOR_PJT,
	   END_USER,
	   SEQ_PROJECT, 
	   NO_EMP,
	   NO_IOLINE,
	   CD_PARTNER,         
	   NO_PO,         
	   NO_POLINE,
	   CD_QTIOTP,
	   NM_QTIOTP,
	   TP_UM_TAX,
	   NM_TP_UM_TAX,
	   NO_WBS,    
	   NO_CBS,
	   GI_PARTNER, 
	   GI_LN_PARTNER,
	   CD_SL,
	   NM_SL,
	   UM_WEIGHT,
	   TOT_WEIGHT,
	   TAX_RATE,
	   CD_GROUP,
	   NM_PURGRP,
	   FG_PAYMENT,
	   NM_KOR,
	   CD_CC,
	   NM_CC,
	   NO_APP,
	   DC1,
	   DC2,
	   '' AS NO_IV,
	   0 AS NO_IVLINE,
	   '' AS NO_TEMP,
	   '' AS CHG_RTEXCH,
	   CD_DOCU,
	   TP_PURPRICE,
	   PO_PRICE,
	   FG_UM,
	   DC_RMK1,
	   DC_RMK2,
	   CD_ITEM_PARTNER, 
	   NM_ITEM_PARTNER, 
	   NO_DSP,
	   CD_PJT_ITEM,
	   NM_PJT_ITEM,
	   PJT_ITEM_STND,
	   NULL AS CD_ACTIVITY,    
	   NULL AS NM_ACTIVITY,
	   NULL AS CD_COST,
	   NULL AS NM_COST,
	   CD_PJTGRP,
	   NM_PJTGRP,
	   NM_TPPO 
FROM A
ORDER BY A.CD_ITEM, A.DT_IO

OPTION(RECOMPILE)

GO