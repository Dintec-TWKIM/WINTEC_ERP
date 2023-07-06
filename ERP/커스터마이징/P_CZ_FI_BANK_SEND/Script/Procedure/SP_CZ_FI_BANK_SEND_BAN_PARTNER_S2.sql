USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_BANK_SEND_BAN_PARTNER_S2]    Script Date: 2018-03-15 오전 10:33:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_FI_BANK_SEND_BAN_PARTNER_S2]
( 
	@P_CD_COMPANY	NVARCHAR(7), 
	@P_CD_PC		NVARCHAR(4000), 
	@P_CD_ACCT		NVARCHAR(4000), 
	@P_DT_FACCT		NVARCHAR(8), 
	@P_DT_TACCT		NVARCHAR(8), 
	@P_TP_GUBUN		NVARCHAR(1), 
	@P_CD_FPARTNER  NVARCHAR(20), 
	@P_CD_TPARTNER  NVARCHAR(20), 
	@P_DT_END_F		NVARCHAR(8), 
	@P_DT_END_T		NVARCHAR(8), 
	@P_CD_DOCU		NVARCHAR(3), 
	@P_TP_DEPOSIT	NVARCHAR(1) = NULL,		-- 거래처계좌여부 (대림) 
	@P_TP_SET		NCHAR(1),
	@P_YN_BAN		NCHAR(1),			-- 반제완료여부
	@P_YN_TRAN		NCHAR(1),			-- 이체완료여부
	@P_DT_PAYMENT_F NCHAR(8),			--나이스전용(지급예정일자)
	@P_DT_PAYMENT_T NCHAR(8),			--나이스전용(지급예정일자)		
	@P_CD_PARTNER_GRP NVARCHAR(10)		--거래처구분(한국토요타자동차)
) 
AS 

/******************************************* 
**  SYSTEM          : 회계관리 
**  SUB SYSTEM      : 은행연동-거래내역관리(반제) 
**  PAGE            : P_FI_BANK_SEND_BAN 
**  DESC            : 상단 그리드 조회 
**  RETURN VALUES   : 
** 
**  작    성    자  : 
**  작    성    일  :  
** 
**  수    정    자  : 박창수
**  수  정  내  용  : 거래구분이 거래처일경우
********************************************* 
** CHANGE HISTORY 
********************************************* 
** 2014.09.11 윤나라 '거래구분-이체계좌등록'일 경우 사업자등록번호 나오도록 수정
** 2014.12.05 윤나라 
*********************************************/ 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
    
IF @P_TP_SET = '1'
BEGIN 
	SELECT DISTINCT 'N' S, 
					BH.DT_ACCT AS ACCT_DATE, 
					BH.NO_ACCT AS DOCU_NO, 
					BH.NO_DOCU, 
					BH.NO_DOLINE AS LINE_NO,
					(CASE WHEN ISNULL(BH.NO_DOCU,'') = '' THEN NULL ELSE BH.NO_DOCU + '-' + CONVERT(VARCHAR,BH.NO_DOLINE) END) AS NO_DOCU_LINE_NO, 
					BH.CD_PC AS NODE_CODE, 
					BH.CD_ACCT AS ACCT_CODE, 
					BH.NM_ACCT AS ACCT_NAME,
					BH.CD_EXCH,
					BH.NM_EXCH,	
					BH.AMT, -- 금액
					BH.AMT_BAN, -- 반재금액
					BH.AM_EX,
					BH.AM_EXBAN,
					ISNULL(KH.TRANS_AMT, 0) AS TRANS_AMT_A, -- 이체금액
					ISNULL(KH.TRANS_AMT_EX, 0) AS TRANS_AMT_EX_A,
					(CASE WHEN (BH.AMT_BAN > 0 OR BH.AM_EXBAN > 0) THEN (BH.AMT - BH.AMT_BAN) ELSE (BH.AMT - ISNULL(KH.TRANS_AMT, 0)) END) AS TRANS_AMT, -- 이체가능금액
					(CASE WHEN (BH.AMT_BAN > 0 OR BH.AM_EXBAN > 0) THEN (BH.AM_EX - BH.AM_EXBAN) ELSE (BH.AM_EX - ISNULL(KH.TRANS_AMT_EX, 0)) END) AS TRANS_AMT_EX,
					BH.DT_END, 
					BH.NM_NOTE AS CLIENT_NOTE, 
					'' AS TRANS_NOTE,
					BD.BANK_CODE AS TRANS_BANK_CODE, 
					MC.NM_SYSDEF AS TRANS_BANK_NAME, 
					BD.CD_DEPOSIT AS TRANS_ACCT_NO, 
					BD.TR_CODE AS CUST_CODE, 
					BD.TR_NAME AS CUST_NAME, 
					BD.NM_DEPOSIT AS TRANS_NAME, 
					'' AS TRANS_DATE, 
					'' AS TRANS_SEQ, 
					'' AS BANK_CODE, 
					'' AS ACCT_NO, 
					'' AS SEQ, 
				    MP.NO_COMPANY, 
					MD.NM_DEPT, 
					CC.NM_CC, 
					PH.NM_PROJECT AS NM_PJT,
					PC.NM_PC,
					BH.CD_DEPOSIT, 
					FD.NM_DEPOSIT,
					KH.NM_DEPOSIT_BANK,
					BH.D61,
					'' AS CONTNO,
					'' AS NM_BANK_EN,
					'' AS CD_BANK_NATION,
					'' AS NM_BANK_NATION,
					'' AS NO_SORT,
					'' AS CD_DEPOSIT_NATION,
					'' AS NM_DEPOSIT_NATION,
					'' AS NO_SWIFT,
					'' AS DC_DEPOSIT_TEL,
					'' AS DC_DEPOSIT_ADDRESS,
					'' AS NO_BANK_BIC
	FROM (SELECT BH.CD_COMPANY, 
				 FD.DT_ACCT, 
				 BH.NO_DOCU, 
				 FD.NO_ACCT, 
				 BH.NO_DOLINE, 
				 BH.CD_PC, 
				 BH.CD_ACCT, 
				 AC.NM_ACCT, 
				 FD.NM_NOTE, 
				 (CASE WHEN @P_TP_GUBUN = '1' THEN FD.CD_PARTNER  
				 	   WHEN @P_TP_GUBUN = '2' THEN FD.CD_EMPLOY  
				 	   ELSE FD.CD_UMNG5 END) AS TR_CODE,
			     FD.CD_EXCH,
				 MC.NM_SYSDEF AS NM_EXCH, 
				 BH.AM_DOCU AS AMT,
				 BH.AM_BAN AS AMT_BAN,
				 BH.AM_EX,
				 BH.AM_EXBAN,
				 FD.DT_END,
				 FD.CD_DEPT, 
				 FD.CD_CC,
				 FD.CD_PJT, 
				 FD.CD_DEPOSIT,
		         (CASE 'D61' WHEN FD.CD_MNG1 THEN FD.NM_MNGD1 
				 			 WHEN FD.CD_MNG2 THEN FD.NM_MNGD2 
				 			 WHEN FD.CD_MNG3 THEN FD.NM_MNGD3 
				 			 WHEN FD.CD_MNG4 THEN FD.NM_MNGD4 
				             WHEN FD.CD_MNG5 THEN FD.NM_MNGD5 
				 			 WHEN FD.CD_MNG6 THEN FD.NM_MNGD6 
				 			 WHEN FD.CD_MNG7 THEN FD.NM_MNGD7 
				 			 WHEN FD.CD_MNG8 THEN FD.NM_MNGD8 END) AS D61
	      FROM FI_BANH BH 
		  JOIN FI_DOCU FD ON BH.NO_DOCU = FD.NO_DOCU AND BH.NO_DOLINE = FD.NO_DOLINE AND BH.CD_PC = FD.CD_PC AND BH.CD_COMPANY = FD.CD_COMPANY 
		  JOIN FI_ACCTCODE AC ON BH.CD_COMPANY = AC.CD_COMPANY AND BH.CD_ACCT = AC.CD_ACCT
		  LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = FD.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = FD.CD_EXCH
		  WHERE	(FD.DT_ACCT >= @P_DT_FACCT AND FD.DT_ACCT <= @P_DT_TACCT AND (BH.ST_DOCU = '2' OR BH.CD_ACCT = '12320'))  
		  AND (BH.CD_PC IN (SELECT CD_STR FROM TF_GETSPLIT(@P_CD_PC)))  
		  AND (BH.CD_COMPANY = @P_CD_COMPANY AND BH.CD_ACCT IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_ACCT))) 
		  AND (AC.CD_COMPANY = @P_CD_COMPANY AND AC.CD_ACCT IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_ACCT))) 
		  AND ((@P_CD_FPARTNER = '' OR @P_CD_FPARTNER IS NULL) AND (@P_CD_TPARTNER = '' OR @P_CD_TPARTNER IS NULL) 
            OR (BH.CD_PARTNER >= @P_CD_FPARTNER AND  BH.CD_PARTNER <= @P_CD_TPARTNER) 
			OR (FD.CD_EMPLOY >= @P_CD_FPARTNER AND FD.CD_EMPLOY <= @P_CD_TPARTNER) 
			OR (FD.CD_UMNG5 >= @P_CD_FPARTNER AND FD.CD_UMNG5 <= @P_CD_TPARTNER)) 
		  AND (@P_DT_END_F = '' OR @P_DT_END_F IS NULL OR ISNULL(FD.DT_END,'') >= @P_DT_END_F) 
		  AND (@P_DT_END_T = '' OR @P_DT_END_T IS NULL OR ISNULL(FD.DT_END,'') <= @P_DT_END_T)  
		  AND (@P_CD_DOCU = '' OR @P_CD_DOCU IS NULL OR FD.CD_DOCU = @P_CD_DOCU) 
		  AND BH.NO_DOCU NOT IN (SELECT NO_DOCU 
								 FROM BANK_SENDSUB 
								 WHERE C_CODE = @P_CD_COMPANY 
								 AND NODE_CODE IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PC)) 
								 AND ACCT_DATE BETWEEN @P_DT_FACCT AND @P_DT_TACCT 
								 AND LINE_NO = BH.NO_DOLINE)) BH
	LEFT JOIN (SELECT C_CODE,
					  NO_DOCU,
					  NO_DOLINE,
					  SUM(TRANS_AMT_EX) AS TRANS_AMT_EX,
					  SUM(TRANS_AMT) AS TRANS_AMT,
					  NM_DEPOSIT_BANK
			   FROM BANK_SENDH
			   WHERE C_CODE = @P_CD_COMPANY 
			   AND NODE_CODE IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PC)) 
			   GROUP BY C_CODE, NO_DOCU, NO_DOLINE, NM_DEPOSIT_BANK) KH
	ON BH.CD_COMPANY = KH.C_CODE AND BH.NO_DOCU = KH.NO_DOCU AND BH.NO_DOLINE = KH.NO_DOLINE
	LEFT JOIN (SELECT CD_COMPANY,
					  CD_DEPOSIT,
					  NM_DEPOSIT,
					  TR_CODE,
					  TR_NAME,
					  BANK_CODE,
					  NM_RMK
			   FROM	BANK_DEPOSIT   
			   WHERE TP_DEPOSIT = @P_TP_GUBUN 
			   AND (@P_CD_FPARTNER = '' OR @P_CD_FPARTNER IS NULL OR TR_CODE >= @P_CD_FPARTNER)  
			   AND (@P_CD_TPARTNER = '' OR @P_CD_TPARTNER IS NULL OR TR_CODE <= @P_CD_TPARTNER)) BD 
	ON BH.CD_COMPANY = BD.CD_COMPANY AND BH.TR_CODE = BD.TR_CODE
	LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = BD.CD_COMPANY AND MC.CD_FIELD = 'FI_T000013' AND MC.CD_SYSDEF = BD.BANK_CODE
	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = BD.CD_COMPANY AND MP.CD_PARTNER = BD.TR_CODE
	LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = BH.CD_COMPANY AND MD.CD_DEPT = BH.CD_DEPT
	LEFT JOIN MA_CC CC ON CC.CD_COMPANY = BH.CD_COMPANY AND CC.CD_CC = BH.CD_CC
	LEFT JOIN SA_PROJECTH PH ON PH.CD_COMPANY = BH.CD_COMPANY AND PH.NO_PROJECT = BH.CD_PJT AND PH.NO_SEQ = 1
	LEFT JOIN MA_PC PC ON PC.CD_COMPANY = BH.CD_COMPANY AND PC.CD_PC = BH.CD_PC
	LEFT JOIN FI_DEPOSIT FD ON FD.CD_COMPANY = BH.CD_COMPANY AND FD.CD_DEPOSIT = BH.CD_DEPOSIT
	LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = BH.CD_COMPANY AND SH.NO_SO = BH.CD_PJT
	WHERE ((@P_TP_DEPOSIT = '' OR @P_TP_DEPOSIT IS NULL) 
	   OR (@P_TP_DEPOSIT = 'Y' AND BD.TR_CODE IS NOT NULL) 
	   OR (@P_TP_DEPOSIT = 'N' AND BD.TR_CODE IS NULL))
	-- 반제여부, 이체여부를 같이 체크함
    AND (((@P_YN_BAN = '' OR @P_YN_BAN IS NULL)
	   OR (@P_YN_BAN = 'N' AND (BH.AMT - BH.AMT_BAN) <> 0)
	   OR (@P_YN_BAN = 'Y' AND (BH.AMT - BH.AMT_BAN) = 0))
	AND ((@P_YN_TRAN = '' OR @P_YN_TRAN IS NULL)
						  OR (@P_YN_TRAN = 'N' AND (BH.AMT - ISNULL(KH.TRANS_AMT, 0)) <> 0)
						  OR (@P_YN_TRAN = 'Y' AND (BH.AMT - ISNULL(KH.TRANS_AMT, 0)) = 0)))
	ORDER BY BH.NO_DOCU, BH.NO_DOLINE
END 
ELSE IF @P_TP_SET = '2' 
BEGIN 
	SELECT DISTINCT 'N' S, 
				    BH.DT_ACCT AS ACCT_DATE, 
				    BH.NO_ACCT AS DOCU_NO, 
					BH.NO_DOCU, 
					BH.NO_DOLINE AS LINE_NO, 
					(CASE WHEN ISNULL(BH.NO_DOCU,'')='' THEN NULL ELSE BH.NO_DOCU + '-' + CONVERT(VARCHAR,BH.NO_DOLINE) END) AS NO_DOCU_LINE_NO,
					BH.CD_PC AS NODE_CODE, 
					BH.CD_ACCT AS ACCT_CODE, 
					BH.NM_ACCT AS ACCT_NAME, 
					BH.CD_EXCH,
					BH.NM_EXCH,	
					BH.AMT, -- 금액
					BH.AMT_BAN, -- 반재금액
					BH.AM_EX,
					BH.AM_EXBAN,
					ISNULL(KH.TRANS_AMT, 0) AS TRANS_AMT_A, -- 이체금액
					ISNULL(KH.TRANS_AMT_EX, 0) AS TRANS_AMT_EX_A,
					(CASE WHEN (BH.AMT_BAN > 0 OR BH.AM_EXBAN > 0) THEN (BH.AMT - BH.AMT_BAN) ELSE (BH.AMT - ISNULL(KH.TRANS_AMT, 0)) END) AS TRANS_AMT, -- 이체가능금액
					(CASE WHEN (BH.AMT_BAN > 0 OR BH.AM_EXBAN > 0) THEN (BH.AM_EX - BH.AM_EXBAN) ELSE (BH.AM_EX - ISNULL(KH.TRANS_AMT_EX, 0)) END) AS TRANS_AMT_EX,
					BH.DT_END, 
					BH.NM_NOTE AS CLIENT_NOTE, 
					'' AS TRANS_NOTE, 
					PD.CD_BANK AS TRANS_BANK_CODE, 
					PD.NM_BANK AS TRANS_BANK_NAME, 
					PD.NO_DEPOSIT AS TRANS_ACCT_NO, 
					MP.CD_PARTNER AS CUST_CODE, 
					MP.LN_PARTNER AS CUST_NAME, 
					PD.NM_DEPOSIT AS TRANS_NAME,
					'' AS TRANS_DATE, 
					'' AS TRANS_SEQ, 
					'' AS BANK_CODE, 
					'' AS ACCT_NO, 
					'' AS SEQ, 
					MP.NO_COMPANY,
					MD.NM_DEPT, 
					CC.NM_CC, 
					PH.NM_PROJECT AS NM_PJT, 
					PC.NM_PC,
					BH.CD_DEPOSIT, 
					FD.NM_DEPOSIT,
					KH.NM_DEPOSIT_BANK,
					BH.D61,
					'' AS CONTNO,
					PD.NM_BANK_EN,
					PD.CD_BANK_NATION,
					PD.NM_BANK_NATION,
					PD.NO_SORT,
					PD.CD_DEPOSIT_NATION,
					PD.NM_DEPOSIT_NATION,
					PD.NO_SWIFT,
					PD.DC_DEPOSIT_TEL,
					PD.DC_DEPOSIT_ADDRESS,
					PD.NO_BANK_BIC
	FROM (SELECT BH.CD_COMPANY, 
				 FD.DT_ACCT, 
				 BH.NO_DOCU, 
				 FD.NO_ACCT, 
				 BH.NO_DOLINE, 
				 BH.CD_PC, 
				 BH.CD_ACCT, 
				 AC.NM_ACCT, 
				 FD.NM_NOTE, 
				 FD.CD_EXCH,
				 MC.NM_SYSDEF AS NM_EXCH, 
				 BH.AM_DOCU AS AMT,
				 BH.AM_BAN AS AMT_BAN,
				 BH.AM_EX,
				 BH.AM_EXBAN,
				 FD.DT_END,
				 FD.CD_DEPT, 
				 FD.CD_CC,
				 FD.CD_PJT, 
				 FD.CD_DEPOSIT, 
				 BH.CD_PARTNER,
		         (CASE 'D61' WHEN FD.CD_MNG1 THEN FD.NM_MNGD1
							 WHEN FD.CD_MNG2 THEN FD.NM_MNGD2 
							 WHEN FD.CD_MNG3 THEN FD.NM_MNGD3 
							 WHEN FD.CD_MNG4 THEN FD.NM_MNGD4 
				             WHEN FD.CD_MNG5 THEN FD.NM_MNGD5 
							 WHEN FD.CD_MNG6 THEN FD.NM_MNGD6 
							 WHEN FD.CD_MNG7 THEN FD.NM_MNGD7 
							 WHEN FD.CD_MNG8 THEN FD.NM_MNGD8 END) AS D61
		  FROM FI_BANH BH 
		  JOIN FI_DOCU FD ON BH.NO_DOCU = FD.NO_DOCU AND BH.NO_DOLINE = FD.NO_DOLINE AND BH.CD_PC = FD.CD_PC AND BH.CD_COMPANY = FD.CD_COMPANY 
		  JOIN FI_ACCTCODE AC ON BH.CD_COMPANY = AC.CD_COMPANY AND BH.CD_ACCT = AC.CD_ACCT
		  LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = FD.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = FD.CD_EXCH
		  WHERE	(FD.DT_ACCT >= @P_DT_FACCT AND FD.DT_ACCT <= @P_DT_TACCT AND (BH.ST_DOCU = '2' OR BH.CD_ACCT = '12320'))  
		  AND (BH.CD_PC IN (SELECT CD_STR FROM TF_GETSPLIT(@P_CD_PC)))  
		  AND (BH.CD_COMPANY = @P_CD_COMPANY AND BH.CD_ACCT IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_ACCT))) 
		  AND (AC.CD_COMPANY = @P_CD_COMPANY AND AC.CD_ACCT IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_ACCT ))) 
		  AND ((@P_CD_FPARTNER = '' OR @P_CD_FPARTNER IS NULL) AND (@P_CD_TPARTNER = '' OR @P_CD_TPARTNER IS NULL) 
			OR (BH.CD_PARTNER >= @P_CD_FPARTNER AND  BH.CD_PARTNER <= @P_CD_TPARTNER) 
			OR (FD.CD_EMPLOY >= @P_CD_FPARTNER AND FD.CD_EMPLOY <= @P_CD_TPARTNER) 
			OR (FD.CD_UMNG5 >= @P_CD_FPARTNER AND FD.CD_UMNG5 <= @P_CD_TPARTNER)) 
		  AND (@P_DT_END_F = '' OR @P_DT_END_F IS NULL OR ISNULL(FD.DT_END,'') >= @P_DT_END_F) 
		  AND (@P_DT_END_T = '' OR @P_DT_END_T IS NULL OR ISNULL(FD.DT_END,'') <= @P_DT_END_T)  
		  AND (@P_CD_DOCU = '' OR @P_CD_DOCU IS NULL OR FD.CD_DOCU = @P_CD_DOCU) 
		  AND BH.NO_DOCU NOT IN (SELECT NO_DOCU 
								 FROM BANK_SENDSUB 
								 WHERE C_CODE = @P_CD_COMPANY 
								 AND NODE_CODE IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PC)) 
								 AND ACCT_DATE BETWEEN @P_DT_FACCT AND @P_DT_TACCT 
								 AND LINE_NO = BH.NO_DOLINE)) BH 
	LEFT JOIN (SELECT C_CODE,
					  NO_DOCU,
					  NO_DOLINE,
					  SUM(TRANS_AMT_EX) AS TRANS_AMT_EX,
					  SUM(TRANS_AMT) AS TRANS_AMT,
					  NM_DEPOSIT_BANK
			   FROM BANK_SENDH
			   WHERE C_CODE = @P_CD_COMPANY 
			   AND NODE_CODE IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PC)) 
			   GROUP BY C_CODE, NO_DOCU, NO_DOLINE, NM_DEPOSIT_BANK) KH
	ON BH.CD_COMPANY = KH.C_CODE AND BH.NO_DOCU = KH.NO_DOCU AND BH.NO_DOLINE = KH.NO_DOLINE
	LEFT JOIN (SELECT PD.CD_COMPANY,
					  PD.CD_PARTNER,
					  PD2.NO_DEPOSIT,
					  PD2.CD_BANK,
					  MC.NM_SYSDEF AS NM_BANK,
					  PD2.NM_BANK AS NM_BANK_EN,
					  PD2.CD_BANK_NATION,
					  MC1.NM_SYSDEF AS NM_BANK_NATION,
					  PD2.NO_SORT,
					  PD2.CD_DEPOSIT_NATION,
					  MC2.NM_SYSDEF AS NM_DEPOSIT_NATION,
					  PD2.NO_SWIFT,
					  PD2.NM_DEPOSIT,
					  PD2.DC_DEPOSIT_TEL,
					  PD2.DC_DEPOSIT_ADDRESS,
					  PD2.NO_BANK_BIC,
					  PD2.DC_RMK
			   FROM	(SELECT CD_COMPANY, CD_PARTNER,
					 	    MAX(NO_DEPOSIT) AS NO_DEPOSIT 
					 FROM MA_PARTNER_DEPOSIT
					 WHERE USE_YN = 'Y'
					 GROUP BY CD_COMPANY, CD_PARTNER) PD
			   LEFT JOIN MA_PARTNER_DEPOSIT PD1 ON PD1.CD_COMPANY = PD.CD_COMPANY AND PD1.CD_PARTNER = PD.CD_PARTNER AND PD1.NO_DEPOSIT = PD.NO_DEPOSIT
			   LEFT JOIN CZ_MA_PARTNER_DEPOSIT PD2 ON PD2.CD_COMPANY = PD1.CD_COMPANY AND PD2.CD_PARTNER = PD1.CD_PARTNER AND PD2.NO_DEPOSIT = PD1.NO_DEPOSIT 
			   LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PD1.CD_COMPANY AND MC.CD_FIELD = 'FI_T000013' AND MC.CD_SYSDEF = PD1.CD_BANK
			   LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = PD2.CD_COMPANY AND MC1.CD_FIELD = 'CZ_MA00016' AND MC1.CD_SYSDEF = PD2.CD_BANK_NATION
			   LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = PD2.CD_COMPANY AND MC2.CD_FIELD = 'CZ_MA00016' AND MC2.CD_SYSDEF = PD2.CD_DEPOSIT_NATION) PD 
	ON BH.CD_COMPANY = PD.CD_COMPANY AND BH.CD_PARTNER = PD.CD_PARTNER 
	LEFT JOIN MA_PARTNER MP ON BH.CD_COMPANY = MP.CD_COMPANY AND BH.CD_PARTNER = MP.CD_PARTNER 
	LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = BH.CD_COMPANY AND MD.CD_DEPT = BH.CD_DEPT
	LEFT JOIN MA_CC CC ON CC.CD_COMPANY = BH.CD_COMPANY AND CC.CD_CC = BH.CD_CC
	LEFT JOIN SA_PROJECTH PH ON PH.CD_COMPANY = BH.CD_COMPANY AND PH.NO_PROJECT = BH.CD_PJT
	LEFT JOIN MA_PC PC ON PC.CD_COMPANY = BH.CD_COMPANY AND PC.CD_PC = BH.CD_PC
	LEFT JOIN FI_DEPOSIT FD ON FD.CD_COMPANY = BH.CD_COMPANY AND FD.CD_DEPOSIT = BH.CD_DEPOSIT
	WHERE ((@P_TP_DEPOSIT = '' OR @P_TP_DEPOSIT IS NULL) 
		OR (@P_TP_DEPOSIT = 'Y' AND PD.CD_PARTNER IS NOT NULL) 
		OR (@P_TP_DEPOSIT = 'N' AND PD.CD_PARTNER IS NULL) )
	-- 반제여부, 이체여부를 같이 체크함
    AND (((@P_YN_BAN = '' OR @P_YN_BAN IS NULL)
	OR (@P_YN_BAN = 'N' AND (BH.AMT - BH.AMT_BAN) <> 0)
	OR (@P_YN_BAN = 'Y' AND (BH.AMT - BH.AMT_BAN) = 0))
	AND ((@P_YN_TRAN = '' OR @P_YN_TRAN IS NULL)
	 OR (@P_YN_TRAN = 'N' AND (BH.AMT - ISNULL(KH.TRANS_AMT, 0)) <> 0)
	 OR (@P_YN_TRAN = 'Y' AND (BH.AMT - ISNULL(KH.TRANS_AMT, 0)) = 0)))
	ORDER BY BH.NO_DOCU, BH.NO_DOLINE 
END
ELSE IF @P_TP_SET = '3'
BEGIN 
	SELECT DISTINCT 'N' S, 
					BH.DT_ACCT AS ACCT_DATE, 
					BH.NO_ACCT AS DOCU_NO, 
					BH.NO_DOCU, 
					BH.NO_DOLINE AS LINE_NO, 
					(CASE WHEN ISNULL(BH.NO_DOCU,'')='' THEN NULL ELSE BH.NO_DOCU + '-' + CONVERT(VARCHAR,BH.NO_DOLINE) END) AS NO_DOCU_LINE_NO,
					BH.CD_PC AS NODE_CODE, 
					BH.CD_ACCT AS ACCT_CODE, 
					BH.NM_ACCT AS ACCT_NAME, 
					BH.CD_EXCH,
					BH.NM_EXCH,	
					BH.AMT, -- 금액
					BH.AMT_BAN, -- 반재금액
					BH.AM_EX,
					BH.AM_EXBAN,
					ISNULL(KH.TRANS_AMT, 0) AS TRANS_AMT_A, -- 이체금액
					ISNULL(KH.TRANS_AMT_EX, 0) AS TRANS_AMT_EX_A,
					(CASE WHEN (BH.AMT_BAN > 0 OR BH.AM_EXBAN > 0) THEN (BH.AMT - BH.AMT_BAN) ELSE (BH.AMT - ISNULL(KH.TRANS_AMT, 0)) END) AS TRANS_AMT, -- 이체가능금액
					(CASE WHEN (BH.AMT_BAN > 0 OR BH.AM_EXBAN > 0) THEN (BH.AM_EX - BH.AM_EXBAN) ELSE (BH.AM_EX - ISNULL(KH.TRANS_AMT_EX, 0)) END) AS TRANS_AMT_EX,
					BH.DT_END, 
					BH.NM_NOTE AS CLIENT_NOTE, 
					'' AS TRANS_NOTE, 
					PD1.CD_BANK AS TRANS_BANK_CODE, 
					MC.NM_SYSDEF AS TRANS_BANK_NAME, 
					PD1.NO_DEPOSIT AS TRANS_ACCT_NO, 
					MP.CD_PARTNER AS CUST_CODE, 
					MP.LN_PARTNER AS CUST_NAME, 
					PD1.NM_DEPOSIT AS TRANS_NAME, 
					'' AS TRANS_DATE, 
					'' AS TRANS_SEQ, 
					'' AS BANK_CODE, 
					'' AS ACCT_NO, 
					'' AS SEQ, 
					MP.NO_COMPANY,
					MD.NM_DEPT, 
					CC.NM_CC, 
					PH.NM_PROJECT AS NM_PJT, 
					PC.NM_PC,
					BH.CD_DEPOSIT, 
					FD.NM_DEPOSIT,
					KH.NM_DEPOSIT_BANK,
					BH.D61,
					'' AS CONTNO,
					'' AS NM_BANK_EN,
					'' AS CD_BANK_NATION,
					'' AS NM_BANK_NATION,
					'' AS NO_SORT,
					'' AS CD_DEPOSIT_NATION,
					'' AS NM_DEPOSIT_NATION,
					'' AS NO_SWIFT,
					'' AS DC_DEPOSIT_TEL,
					'' AS DC_DEPOSIT_ADDRESS,
					'' AS NO_BANK_BIC
	FROM (SELECT BH.CD_COMPANY, 
				 FD.DT_ACCT, 
				 BH.NO_DOCU, 
				 FD.NO_ACCT, 
				 BH.NO_DOLINE, 
				 BH.CD_PC, 
				 BH.CD_ACCT, 
				 AC.NM_ACCT, 
				 FD.NM_NOTE, 
				 (CASE WHEN @P_TP_GUBUN = '1' THEN FD.CD_PARTNER  
				 	   WHEN @P_TP_GUBUN = '2' THEN FD.CD_EMPLOY  
				 	   ELSE FD.CD_UMNG5 END) AS TR_CODE, 
				 FD.CD_EXCH,
				 MC.NM_SYSDEF AS NM_EXCH, 
				 BH.AM_DOCU AS AMT,
				 BH.AM_BAN AS AMT_BAN,
				 BH.AM_EX,
				 BH.AM_EXBAN,
				 FD.DT_END,
				 FD.CD_DEPT, 
				 FD.CD_CC,
				 FD.CD_PJT, 
				 FD.CD_DEPOSIT, 
				 BH.CD_PARTNER, 
				 (CASE 'C15' WHEN FD.CD_MNG1 THEN FD.CD_MNGD1 
							 WHEN FD.CD_MNG2 THEN FD.CD_MNGD2 
							 WHEN FD.CD_MNG3 THEN FD.CD_MNGD3 
							 WHEN FD.CD_MNG4 THEN FD.CD_MNGD4 
							 WHEN FD.CD_MNG5 THEN FD.CD_MNGD5 
							 WHEN FD.CD_MNG6 THEN FD.CD_MNGD6 
							 WHEN FD.CD_MNG7 THEN FD.CD_MNGD7 
							 WHEN FD.CD_MNG8 THEN FD.CD_MNGD8 
							 ELSE '' END ) AS NO_DEPOSIT,
		         (CASE 'D61' WHEN FD.CD_MNG1 THEN FD.NM_MNGD1 
							 WHEN FD.CD_MNG2 THEN FD.NM_MNGD2 
							 WHEN FD.CD_MNG3 THEN FD.NM_MNGD3 
							 WHEN FD.CD_MNG4 THEN FD.NM_MNGD4 
				             WHEN FD.CD_MNG5 THEN FD.NM_MNGD5 
							 WHEN FD.CD_MNG6 THEN FD.NM_MNGD6 
							 WHEN FD.CD_MNG7 THEN FD.NM_MNGD7 
							 WHEN FD.CD_MNG8 THEN FD.NM_MNGD8 END) AS D61
				 FROM FI_BANH BH 
				 JOIN FI_DOCU FD ON BH.NO_DOCU = FD.NO_DOCU AND BH.NO_DOLINE = FD.NO_DOLINE AND BH.CD_PC = FD.CD_PC AND BH.CD_COMPANY = FD.CD_COMPANY 
				 JOIN FI_ACCTCODE AC ON BH.CD_COMPANY = AC.CD_COMPANY AND BH.CD_ACCT = AC.CD_ACCT
				 LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = FD.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = FD.CD_EXCH
				 WHERE (FD.DT_ACCT >= @P_DT_FACCT AND FD.DT_ACCT <= @P_DT_TACCT AND (BH.ST_DOCU = '2' OR BH.CD_ACCT = '12320'))  
				 AND (BH.CD_PC IN (SELECT CD_STR FROM TF_GETSPLIT(@P_CD_PC)))  
				 AND (BH.CD_COMPANY = @P_CD_COMPANY AND BH.CD_ACCT IN ( SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_ACCT))) 
				 AND (AC.CD_COMPANY = @P_CD_COMPANY AND AC.CD_ACCT IN ( SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_ACCT))) 
				 AND ((@P_CD_FPARTNER = '' OR @P_CD_FPARTNER IS NULL) AND (@P_CD_TPARTNER = '' OR @P_CD_TPARTNER IS NULL) 
				  OR (BH.CD_PARTNER >= @P_CD_FPARTNER AND  BH.CD_PARTNER <= @P_CD_TPARTNER) 
				  OR (FD.CD_EMPLOY >= @P_CD_FPARTNER AND FD.CD_EMPLOY <= @P_CD_TPARTNER) 
				  OR (FD.CD_UMNG5 >= @P_CD_FPARTNER AND FD.CD_UMNG5 <= @P_CD_TPARTNER)) 
				 AND (@P_DT_END_F = '' OR @P_DT_END_F IS NULL OR ISNULL(FD.DT_END,'') >= @P_DT_END_F) 
				 AND (@P_DT_END_T = '' OR @P_DT_END_T IS NULL OR ISNULL(FD.DT_END,'') <= @P_DT_END_T)  
				 AND (@P_CD_DOCU = '' OR @P_CD_DOCU IS NULL OR FD.CD_DOCU = @P_CD_DOCU) 
				 AND BH.NO_DOCU NOT IN (SELECT NO_DOCU 
									    FROM BANK_SENDSUB 
									    WHERE C_CODE = @P_CD_COMPANY 
									    AND NODE_CODE IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PC)) 
									    AND ACCT_DATE BETWEEN @P_DT_FACCT AND @P_DT_TACCT 
									    AND LINE_NO  = BH.NO_DOLINE)) BH 
	LEFT JOIN (SELECT C_CODE,
					  NO_DOCU,
					  NO_DOLINE,
					  SUM(TRANS_AMT_EX) AS TRANS_AMT_EX,
					  SUM(TRANS_AMT) AS TRANS_AMT,
					  NM_DEPOSIT_BANK
			   FROM BANK_SENDH
			   WHERE C_CODE = @P_CD_COMPANY 
			   AND NODE_CODE IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_PC)) 
			   GROUP BY C_CODE, NO_DOCU, NO_DOLINE, NM_DEPOSIT_BANK) KH
	ON BH.CD_COMPANY = KH.C_CODE AND BH.NO_DOCU = KH.NO_DOCU AND BH.NO_DOLINE = KH.NO_DOLINE
	LEFT JOIN MA_PARTNER_DEPOSIT PD1 ON BH.CD_COMPANY = PD1.CD_COMPANY AND BH.CD_PARTNER = PD1.CD_PARTNER AND BH.NO_DEPOSIT = PD1.NO_DEPOSIT 
	LEFT JOIN MA_PARTNER MP ON BH.CD_COMPANY = MP.CD_COMPANY AND BH.CD_PARTNER = MP.CD_PARTNER
	LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = PD1.CD_COMPANY AND MC.CD_FIELD = 'FI_T000013' AND MC.CD_SYSDEF = PD1.CD_BANK
	LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = BH.CD_COMPANY AND MD.CD_DEPT = BH.CD_DEPT
	LEFT JOIN MA_CC CC ON CC.CD_COMPANY = BH.CD_COMPANY AND CC.CD_CC = BH.CD_CC
	LEFT JOIN SA_PROJECTH PH ON PH.CD_COMPANY = BH.CD_COMPANY AND PH.NO_PROJECT = BH.CD_PJT AND PH.NO_SEQ = 1
	LEFT JOIN MA_PC PC ON PC.CD_COMPANY = BH.CD_COMPANY AND PC.CD_PC = BH.CD_PC
	LEFT JOIN FI_DEPOSIT FD ON FD.CD_COMPANY = BH.CD_COMPANY AND FD.CD_DEPOSIT = BH.CD_DEPOSIT
	WHERE ((@P_TP_DEPOSIT = '' OR @P_TP_DEPOSIT IS NULL) 
	    OR (@P_TP_DEPOSIT = 'Y' AND PD1.CD_PARTNER IS NOT NULL) 
		OR (@P_TP_DEPOSIT = 'N' AND PD1.CD_PARTNER IS NULL) )
	-- 반제여부, 이체여부를 같이 체크함
    AND	(((@P_YN_BAN = '' OR @P_YN_BAN IS NULL)
	 OR (@P_YN_BAN = 'N' AND (BH.AMT - BH.AMT_BAN) <> 0)
	 OR (@P_YN_BAN = 'Y' AND (BH.AMT - BH.AMT_BAN) = 0))
	AND	((@P_YN_TRAN = '' OR @P_YN_TRAN IS NULL)
	 OR (@P_YN_TRAN = 'N' AND (BH.AMT - ISNULL(KH.TRANS_AMT, 0)) <> 0)
	 OR (@P_YN_TRAN = 'Y' AND (BH.AMT - ISNULL(KH.TRANS_AMT, 0)) = 0)))					
	ORDER BY BH.NO_DOCU, BH.NO_DOLINE 
END
GO