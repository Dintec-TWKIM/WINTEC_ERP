USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_IVH_AUTO_PARTIAL_RPT_S]    Script Date: 2015-06-26 오전 9:15:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_IVH_AUTO_PARTIAL_RPT_S]        
(        
    @P_CD_COMPANY		    NVARCHAR(7),
	@P_NO_IV				NVARCHAR(20)
)        
AS        

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT IL.CD_COMPANY,
           IL.NO_SO,
           SUM(IL.QT_CLS) AS QT_CLS,
           SUM(SL.QT_SO) AS QT_SO
    FROM (SELECT IL.CD_COMPANY, IL.NO_SO, IL.NO_SOLINE,
				 SUM(IL.QT_CLS) AS QT_CLS
		  FROM SA_IVL IL
		  WHERE IL.CD_COMPANY = @P_CD_COMPANY
		  AND IL.NO_IV = @P_NO_IV
		  AND IL.CD_ITEM NOT LIKE 'SD%'
		  GROUP BY IL.CD_COMPANY, IL.NO_SO, IL.NO_SOLINE) IL
    LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = IL.CD_COMPANY AND SL.NO_SO = IL.NO_SO AND SL.SEQ_SO = IL.NO_SOLINE
    GROUP BY IL.CD_COMPANY, IL.NO_SO
),
B AS
(
	SELECT A.CD_COMPANY,
	       A.NO_SO,
	       A.QT_CLS,
           IL.QT_CLS AS QT_CLS1,
	       A.QT_SO,
	       SL.QT_SO AS QT_SO1
	FROM A
	JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
	             SUM(SL.QT_SO) AS QT_SO
	      FROM SA_SOL SL
	      WHERE SL.CD_ITEM NOT LIKE 'SD%'
	      GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
	ON SL.CD_COMPANY = A.CD_COMPANY AND SL.NO_SO = A.NO_SO
    JOIN (SELECT IL.CD_COMPANY, IL.NO_SO,
                 SUM(IL.QT_CLS) AS QT_CLS 
          FROM SA_IVL IL
          WHERE IL.CD_ITEM NOT LIKE 'SD%'
          GROUP BY IL.CD_COMPANY, IL.NO_SO) IL
    ON IL.CD_COMPANY = A.CD_COMPANY AND IL.NO_SO = A.NO_SO
	WHERE (A.QT_CLS <> A.QT_SO OR A.QT_CLS <> IL.QT_CLS)
	AND IL.QT_CLS = SL.QT_SO	
),
C AS
(
    SELECT IL.CD_COMPANY,
           IL.NO_IV,
           IL.NO_LINE 
    FROM B
    JOIN SA_IVL IL ON IL.CD_COMPANY = B.CD_COMPANY AND IL.NO_SO = B.NO_SO
)
SELECT TOP 1
	   IH.NO_IV,        
	   IH.CD_COMPANY,        
	   IH.CD_BIZAREA,        
	   IH.NO_BIZAREA,        
	   IH.CD_PARTNER,        
	   IH.FG_TRANS,        
	   IH.DT_PROCESS,        
	   SUBSTRING(IH.DT_PROCESS, 3, 2) AS DT_TAX_YY,         
	   SUBSTRING(IH.DT_PROCESS, 5, 2) AS DT_TAX_MM,         
	   SUBSTRING(IH.DT_PROCESS, 7, 2) AS DT_TAX_DD,         
	   ISNULL(IH.AM_K, 0) AS AM_K,        
	   ISNULL(IH.VAT_TAX, 0) AS VAT_TAX,        
	   ROUND(ISNULL(IL.VAT, 0) / (CASE WHEN ISNULL(IH.RT_EXCH, 0) = 0 THEN 1 ELSE IH.RT_EXCH END), 2) AS VAT_TAX_EX,
	   IH.FG_TAX,        
	   IH.TP_SUMTAX,        
	   IH.FG_TAXP,        
	   IH.TP_AIS,        
	   IH.CD_DEPT,        
	   MD.NM_DEPT AS NM_DEPT,        
	   IH.NO_EMP,        
	   IH.NO_LC,        
	   IH.NO_VOLUME,        
	   IH.NO_HO,        
	   RIGHT('00000' + CAST(ISNULL(IH.NO_SEQ,0) AS VARCHAR(5)), 5) AS NO_SEQ,  
	   IH.ID_INSERT,        
	   IH.ID_UPDATE,        
	   IH.DC_REMARK,        
	   0 AS AM_VAT,        
	   0 AS AM_IV,        
	   0 AS AM_EXIV,        
	   MP.LN_PARTNER,
	   CD1.NM_SYSDEF AS NM_TRANS,
	   CD2.NM_SYSDEF AS NM_TAX,   
	   ME.NM_KOR,        
	   MB.NM_BIZAREA AS FROM_COMPANY,  
	   SUBSTRING(MB.NO_BIZAREA, 1, 3) + '-' + SUBSTRING(MB.NO_BIZAREA, 4, 2) + '-' + SUBSTRING(MB.NO_BIZAREA, 6, 5) AS FROM_NOCOMPANY,
	   MB.NM_MASTER AS FROM_NAME,  
	   ISNULL(MB.ADS_H, '') + ' ' +ISNULL(MB.ADS_D, '') AS FROM_ADRESS,
	   MB.NO_TEL AS FROM_TEL,
	   MB.NO_FAX AS FROM_FAX,
	   MB.TP_JOB AS FROM_TPJOB,
	   MB.CLS_JOB AS FROM_CLSJOB,
	   MP2.LN_PARTNER AS TO_COMPANY,         
	   MP2.NM_CEO AS TO_NAME,         
	   SUBSTRING(MP2.NO_COMPANY, 1, 3) + '-' + SUBSTRING(MP2.NO_COMPANY, 4, 2) + '-' + SUBSTRING(MP2.NO_COMPANY, 6, 5) AS TO_NOCOMPANY,
	   ISNULL(MP2.DC_ADS1_H, '') + CHAR(13) + CHAR(10) +ISNULL(MP2.DC_ADS1_D, '') AS TO_ADRESS,
	   MP2.CD_NATION,
	   CD.NM_SYSDEF AS NM_NATION,
	   CD.CD_FLAG1 AS CD_NATION_INVOICE,         
	   MP2.NO_TEL2 AS TO_TEL,
	   MP2.NO_TEL1 AS TO_TEL1,
	   MP2.NO_FAX2 AS TO_FAX,         
	   MP2.TP_JOB  AS TO_TPJOB,         
	   MP2.CLS_JOB AS TO_CLSJOB,  
	   MP2.E_MAIL AS TO_EMAIL,
	   MP2.NM_TEXT AS TO_RMK,      
	   IH.CD_EXCH,
	   CD3.NM_SYSDEF AS CD_EXCH_NAME,
	   IH.RT_EXCH,
	   ROUND(IH.AM_EX, 2) AS AM_EX,
	   (ROUND(IH.AM_EX, 2) + ROUND(ISNULL(IL.VAT, 0) / (CASE WHEN ISNULL(IH.RT_EXCH, 0) = 0 THEN 1 ELSE IH.RT_EXCH END), 2)) AS AM_SUM_EX,
	   ISNULL(IH.AM_BAN_EX, 0) AS AM_BAN_EX,
	   ISNULL(IH.AM_BAN, 0) AS AM_BAN,
	   ((ROUND(IH.AM_EX, 2) + ROUND(ISNULL(IL.VAT, 0) / (CASE WHEN ISNULL(IH.RT_EXCH, 0) = 0 THEN 1 ELSE IH.RT_EXCH END), 2)) - ISNULL(IH.AM_BAN_EX, 0)) AS AM_JAN_EX,
	   ((ISNULL(IH.AM_K, 0) + ISNULL(IH.VAT_TAX, 0)) - ISNULL(IH.AM_BAN, 0)) AS AM_JAN,
	   ISNULL(IL.AM_RCP_A, 0) AS AM_RCP_A,
	   ISNULL(IL.AM_RCP_A_EX, 0) AS AM_RCP_A_EX,
	   IH.CD_BIZAREA_TAX,
	   ISNULL(IH.ETAX_KEY,'') AS ETAX_KEY,
	   IH.ETAX_SYS,
	   IH.ETAX_EMAIL,        
	   IH.BILL_PARTNER,
	   MP1.LN_PARTNER AS BILL_LN_PARTNER,        
	   IH.DTS_INSERT,        
	   IH.TP_RECEIPT,        
	   ETAX.NO,        
	   ETAX.NO_TAX,        
	   ETAX.FG_FINAL,        
	   ETAX.CMP_TAX_ST,         
	   ETAX.ERR_MSG,        
	   (CASE WHEN ISNULL(ETAX.NO,'') <> '' THEN 'E36524'       
										   ELSE (CASE WHEN ISNULL(IH.ETAX_KEY,'') <> '' THEN (CASE WHEN LEFT(IH.ETAX_KEY,2) = 'TX' THEN 'A36524' ELSE 'NEOPORT' END)       
																					    ELSE '' END) END) AS ETAX_TOT_TYPE,      
	   (CASE WHEN ISNULL(ETAX.NO,'') <> '' THEN ETAX.NO_TAX      
										   ELSE (CASE WHEN ISNULL(IH.ETAX_KEY,'') <> '' THEN IH.ETAX_KEY ELSE NULL END) END) AS ETAX_TOT_KEY,      
	   LTRIM(RTRIM(CASE IH.ETAX_SYS WHEN 'xx' THEN IH.ETAX_SYS     
	                                WHEN 'xy' THEN IH.ETAX_SYS     
	                                          ELSE (CASE WHEN ISNULL(ETAX.NO,'') <> '' THEN ETAX.FG_FINAL      
	                                                                                   ELSE (CASE WHEN ISNULL(IH.ETAX_KEY,'') <> '' THEN IH.ETAX_SYS ELSE '00' END) END) END)) AS ETAX_TOT_ST,     
	   IH.DT_RCP_RSV,
	   IH.CD_DOCU,    
	   (ISNULL(IH.AM_K, 0) + ISNULL(IH.VAT_TAX, 0)) AS AM_SUM,
	   CD4.NM_SYSDEF AS NM_CON,
	   IH.ETAX_SEND_TYPE,
	   IH.ETAX_SELL_DAM_NM,     
	   IH.ETAX_SELL_DAM_EMAIL,     
	   IH.ETAX_SELL_DAM_MOBIL,   
	   IH.NM_PTR,
	   IH.EX_EMIL,
	   IH.EX_HP,
	   CD5.NM_SYSDEF AS NM_BUSINESS,
	   MP2.DT_CLOSE,
	   IH.FG_BILL,
	   CD6.NM_SYSDEF AS NM_BILL,
	   ISNULL(FD.NO_DOCU, '') AS NO_DOCU,
	   MP2.TP_BUSINESS,
	   MB.CD_PC,
	   IH.NO_DOCU_GRP,
	   IH.FG_AR_EXC,
	   IH.MEMO_CD,
	   IH.CHECK_PEN,
	   MP2.DT_RCP_PRETOLERANCE,
	   IH.DT_RCP_RSV1,
	   MP2.SN_PARTNER,
	   IH.TXT_USERDEF1, 
	   IH.TXT_USERDEF2, 
	   IH.TXT_USERDEF3,
	   GW.ST_STAT,
	   CD7.NM_SYSDEF AS NM_ST_STAT,
	   FTAX.FINAL_STATUS,
	   SH.ETAX_KEY AS ETAX_KEY_ORIGINAL,
	   IH.YN_ISS,
	   IH.NO_ISS,
	   SH.YN_ISS AS YN_ISS_SRC,
	   SH.NO_ISS AS NO_ISS_SRC,
	   IH.FG_MODIFY_STAT,
	   IL.NM_VESSEL,
	   IL.NO_HULL,
	   IH.NM_USERDEF1 AS YN_AUTO,
	   IL.NM_PTR,
	   STUFF((SELECT DISTINCT ',' + SH.NO_PO_PARTNER
	   	      FROM SA_IVL IL
	   	      JOIN SA_SOH SH ON SH.CD_COMPANY = IL.CD_COMPANY AND SH.NO_SO = IL.NO_SO
	   	      WHERE IL.CD_COMPANY = IH.CD_COMPANY
			  AND EXISTS (SELECT 1 
						  FROM C 
						  WHERE C.CD_COMPANY = IL.CD_COMPANY 
						  AND C.NO_IV = IL.NO_IV 
						  AND C.NO_LINE = IL.NO_LINE)
	   	      FOR XML PATH('')),1,1,'') AS NO_PO_PARTNER,
	   STUFF((SELECT DISTINCT ',' + IL.NO_SO
	   	      FROM SA_IVL IL
	   	      WHERE IL.CD_COMPANY = IH.CD_COMPANY
			  AND EXISTS (SELECT 1 
						  FROM C 
						  WHERE C.CD_COMPANY = IL.CD_COMPANY 
						  AND C.NO_IV = IL.NO_IV 
						  AND C.NO_LINE = IL.NO_LINE)
	   	      FOR XML PATH('')),1,1,'') AS NO_SO
FROM SA_IVH IH
JOIN (SELECT IL.CD_COMPANY, IL.NO_IV,
			 SUM(IL.VAT) AS VAT,
		     MAX(MH.NM_VESSEL) AS NM_VESSEL,
		     MAX(MH.NO_HULL) AS NO_HULL,
			 SUM(RH.AM_RCP_A_EX) AS AM_RCP_A_EX,
			 SUM(RH.AM_RCP_A) AS AM_RCP_A,
			 MAX(FP.NM_PTR) AS NM_PTR
	  FROM (SELECT IL.CD_COMPANY, IL.NO_IV, IL.NO_SO,
				   SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.VAT ELSE IL.VAT END) AS VAT
		    FROM SA_IVL IL
			WHERE EXISTS (SELECT 1 
						  FROM C
						  WHERE C.CD_COMPANY = IL.CD_COMPANY 
						  AND C.NO_IV = IL.NO_IV
						  AND C.NO_LINE = IL.NO_LINE)
			GROUP BY IL.CD_COMPANY, IL.NO_IV, IL.NO_SO) IL
	  LEFT JOIN (SELECT RH.CD_COMPANY, RH.NO_PROJECT,
	  				    SUM(RH.AM_RCP_A_EX) AS AM_RCP_A_EX,
						SUM(RH.AM_RCP_A) AS AM_RCP_A 
				 FROM SA_RCPH RH
				 WHERE RH.AM_RCP_A > 0
				 GROUP BY RH.CD_COMPANY, RH.NO_PROJECT) RH
	  ON RH.CD_COMPANY = IL.CD_COMPANY AND RH.NO_PROJECT = IL.NO_SO
	  LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = IL.CD_COMPANY AND SH.NO_SO = IL.NO_SO
	  LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
	  LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = IL.CD_COMPANY AND QH.NO_FILE = IL.NO_SO
	  LEFT JOIN FI_PARTNERPTR FP ON FP.CD_COMPANY = QH.CD_COMPANY AND FP.CD_PARTNER = QH.CD_PARTNER AND FP.SEQ = QH.SEQ_ATTN
      GROUP BY IL.CD_COMPANY, IL.NO_IV) IL
ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IV = IH.NO_IV
LEFT JOIN (SELECT CD_COMPANY, NO_MDOCU,
				  MAX(NO_DOCU) AS NO_DOCU,
				  MAX(ST_DOCU) AS ST_DOCU 
		   FROM FI_DOCU
		   GROUP BY CD_COMPANY, NO_MDOCU) FD
ON FD.CD_COMPANY = IH.CD_COMPANY AND FD.NO_MDOCU = IH.NO_IV
LEFT JOIN MA_BIZAREA MB ON MB.CD_COMPANY = IH.CD_COMPANY AND MB.CD_BIZAREA = IH.CD_BIZAREA_TAX
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = IH.CD_COMPANY AND MP.CD_PARTNER = IH.CD_PARTNER
LEFT JOIN MA_PARTNER MP1 ON MP1.CD_COMPANY = IH.CD_COMPANY AND MP1.CD_PARTNER = IH.BILL_PARTNER
LEFT JOIN MA_PARTNER MP2  ON MP2.CD_COMPANY = IH.CD_COMPANY AND MP2.CD_PARTNER = IH.CD_PARTNER
LEFT JOIN TB_TAX ETAX ON IH.CD_COMPANY + '_' + IH.NO_IV = ETAX.NO 
LEFT JOIN FI_GWDOCU GW ON GW.CD_COMPANY = 'K100' AND GW.CD_PC = '010000' AND GW.NO_DOCU = IH.CD_COMPANY + '-' + IH.NO_IV
LEFT JOIN FI_TAX FTAX ON IH.CD_COMPANY = FTAX.CD_COMPANY AND IH.NO_IV = FTAX.NO_TAX
LEFT JOIN SA_IVH SH ON SH.CD_COMPANY = IH.CD_COMPANY AND SH.NO_IV = IH.NO_IV_SRC
LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = IH.CD_COMPANY AND MD.CD_DEPT = IH.CD_DEPT
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = IH.CD_COMPANY AND ME.NO_EMP = IH.NO_EMP
LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = MP2.CD_COMPANY AND CD.CD_FIELD = 'MA_B000020' AND CD.CD_SYSDEF = MP2.CD_NATION
LEFT JOIN MA_CODEDTL CD1 ON CD1.CD_COMPANY = IH.CD_COMPANY AND CD1.CD_FIELD = 'PU_C000016' AND CD1.CD_SYSDEF = IH.FG_TRANS
LEFT JOIN MA_CODEDTL CD2 ON CD2.CD_COMPANY = IH.CD_COMPANY AND CD2.CD_FIELD = 'MA_B000040' AND CD2.CD_SYSDEF = IH.FG_TAX
LEFT JOIN MA_CODEDTL CD3 ON CD3.CD_COMPANY = IH.CD_COMPANY AND CD3.CD_FIELD = 'MA_B000005' AND CD3.CD_SYSDEF = IH.CD_EXCH
LEFT JOIN MA_CODEDTL CD4 ON CD4.CD_COMPANY = MP2.CD_COMPANY AND CD4.CD_FIELD = 'MA_B000073' AND CD4.CD_SYSDEF = MP2.CD_CON
LEFT JOIN MA_CODEDTL CD5 ON CD5.CD_COMPANY = MP2.CD_COMPANY AND CD5.CD_FIELD = 'MA_B000080' AND CD5.CD_SYSDEF = MP2.TP_BUSINESS
LEFT JOIN MA_CODEDTL CD6 ON CD6.CD_COMPANY = IH.CD_COMPANY AND CD6.CD_FIELD = 'SA_B000002' AND CD6.CD_SYSDEF = IH.FG_BILL
LEFT JOIN MA_CODEDTL CD7 ON CD7.CD_COMPANY = GW.CD_COMPANY AND CD7.CD_FIELD = 'PU_C000065' AND CD7.CD_SYSDEF = GW.ST_STAT
WHERE IH.CD_COMPANY = @P_CD_COMPANY
ORDER BY IH.DT_PROCESS DESC

GO