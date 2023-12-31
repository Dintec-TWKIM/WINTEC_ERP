SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_BILL_MNG_S]  
(  
	@CD_COMPANY			NVARCHAR(7),	--회사코드
	@DT_RPC_FROM		NVARCHAR(8),	--수금일 FROM
	@DT_RPC_TO			NVARCHAR(8),	--수금일 TO
	@CD_TP				NVARCHAR(3),	--수금형태
	@TP_BUSI			NVARCHAR(3),	--거래구분
	@CD_PARTNER			NVARCHAR(20),	--매출처
	@BILL_PARTNER		NVARCHAR(20),	--수금처
	@P_MULTI_SALEGRP			NVARCHAR(4000),	--영업그룹
	@NO_EMP				NVARCHAR(10),   --담당자
	@TABIDX				NVARCHAR(1),    --TAB INDEX  
	@KEY				NVARCHAR(4000), --TAB 별 PK 값 
	@PAGE_ORDER			NVARCHAR(1),    --레포트에서 사용할 값인지 UI에 조회용으로 쓸건지를 구분하는 변수( 'R' : 레포트, 'S' : 조회 )
	@TP_AIS				NVARCHAR(1),    --전표처리상태   
	@TP_BANJE			NVARCHAR(1),    --반제완료여부 '1':완료 '0':미완료
	@P_CD_BIZAREA		NVARCHAR(4000),	--사업장
	@P_FG_RCP			NVARCHAR(3),     --수금구분
    @P_CD_PARTNER_GRP	NVARCHAR(10),
    @P_FG_LANG     NVARCHAR(4) = NULL,	--언어
	@P_MULTI_SALEORG			NVARCHAR(4000) = NULL
)
AS
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG


DECLARE @V_SERVER_KEY NVARCHAR(50)   
SELECT  @V_SERVER_KEY = MAX(SERVER_KEY) 
FROM    CM_SERVER_CONFIG 
WHERE   YN_UPGRADE = 'Y' 
   
IF ( @TABIDX = '0' AND @PAGE_ORDER = 'S' ) --수금번호별 HEAD  
	BEGIN 
		--헤더에 GROUP BY가 사용되지 않는 이유는 금액이 모두 라인에서 SUM 되어 오기 때문에 라인을 JOIN 걸어주지 않아도 되기 때문이다.
		--추후 SA_RCPL 인 라인 테이블을 조인 걸어야 한다면~ 무조건 전체를 TAB별로 GROUP BY를 걸어줘야 한다.
		SELECT  'N' AS S, SR.NO_RCP,   
				SR.DT_RCP, SR.CD_TP, SR.TP_BUSI, MC.NM_SYSDEF AS NM_TP_BUSI, 
				SR.CD_PARTNER, M01.LN_PARTNER, M01.NM_CEO,
				SR.BILL_PARTNER, M02.LN_PARTNER AS LN_BILL_PARTNER, M02.NM_CEO AS BILL_NM_CEO,
				SR.CD_SALEGRP, MS.NM_SALEGRP, SR.NO_EMP, SR.NO_PROJECT,   
				SR.AM_RCP_EX,		--정상수금(외화)
				SR.AM_RCP,			--정상수금(원화)
				SR.AM_RCP_EX_TX,	--반제액(외화)
				SR.AM_RCP_TX,		--반제액(원화)
				SR.AM_RCP_A_EX,		--선수금(외화)
				SR.AM_RCP_A,		--선수금(원화)
				SR.AM_RCPS, SR.TP_AIS, SR.DC_RMK, SR.NO_EMP, ME.NM_KOR, 
                SR.CD_EXCH, MB.NM_SYSDEF AS NM_EXCH, SR.RT_EXCH, M01.SN_PARTNER,
				M01.FG_BILL,        --수금조건
			   ( SELECT DTL1.NM_SYSDEF FROM DZSN_MA_CODEDTL DTL1 WHERE DTL1.CD_COMPANY = M01.CD_COMPANY
													AND DTL1.CD_FIELD = 'SA_B000002'
													AND DTL1.CD_SYSDEF = M01.FG_BILL
				) AS FG_BILL_NAME, --수금조건명
				M01.DT_RCP_PREARRANGED, --수금예정일
				SR.TP_AIS,  --전표처리여부
				SR.FG_MAP,
				SR.CD_BIZAREA,
                SR.TP_BILLS,
				--ISNULL((SELECT TOP 1 FD.NO_DOCU FROM FI_DOCU FD WITH(READUNCOMMITTED) WHERE FD.CD_COMPANY = SR.CD_COMPANY AND FD.NO_MDOCU = SR.NO_RCP), '') NO_DOCU, -- 2011-04-20, 이승훈 추가함.
				ISNULL(F.NO_DOCU, '') NO_DOCU,		-- 장은경(2014.02.03)
				ISNULL(E.CD_PC, CC.CD_PC) AS CD_PC,
				SR.CD_BIZAREA,
				E.NM_BIZAREA,
				(SELECT LN_PARTNER FROM DZSN_MA_PARTNER WHERE CD_COMPANY = @CD_COMPANY AND CD_PARTNER = (SELECT TOP 1 CD_BANK FROM SA_RCPL WHERE CD_COMPANY = SR.CD_COMPANY AND NO_RCP = SR.NO_RCP )) NM_CD_BANK,
				MP.CD_PARTNER_GRP AS CD_PARTNER_GRP,
				PG.NM_SYSDEF AS NM_PARTNER_GRP,
				SR.GI_PARTNER,
				MP2.LN_PARTNER AS NM_GI_PARTNER,
				SR.AM_RCP_K, SR.AM_RCP_K_TX, SR.AM_RCP_VAT, SR.AM_RCP_VAT_TX,
				SR.NO_PROJECT,
				PJ.NM_PROJECT,
				F.ST_DOCU,
				SG.CD_SALEORG,
				SG.NM_SALEORG,
				RD.AM_RCP AS AM_FEE,
				RD.AM_RCP_EX AS AM_FEE_EX,
				RD.AM_RCP1 AS AM_MISC,
				RD.AM_RCP1_EX AS AM_MISC_EX,
				F.CD_DEPOSIT
		  FROM  SA_RCPH SR   
		  LEFT OUTER JOIN DZSN_MA_PARTNER M01 ON SR.CD_COMPANY = M01.CD_COMPANY AND SR.CD_PARTNER = M01.CD_PARTNER
		  LEFT OUTER JOIN DZSN_MA_PARTNER M02 ON SR.CD_COMPANY = M02.CD_COMPANY AND SR.BILL_PARTNER = M02.CD_PARTNER
		  LEFT OUTER JOIN DZSN_MA_SALEGRP MS ON SR.CD_COMPANY = MS.CD_COMPANY AND SR.CD_SALEGRP = MS.CD_SALEGRP
		  LEFT OUTER JOIN DZSN_MA_CODEDTL MC ON SR.CD_COMPANY = MC.CD_COMPANY AND SR.TP_BUSI = MC.CD_SYSDEF AND MC.CD_FIELD = 'PU_C000016'
		  LEFT OUTER JOIN DZSN_MA_CODEDTL MB ON SR.CD_COMPANY = MB.CD_COMPANY AND SR.CD_EXCH = MB.CD_SYSDEF AND MB.CD_FIELD = 'PU_C000004'
          LEFT OUTER JOIN DZSN_MA_EMP     ME ON SR.CD_COMPANY = ME.CD_COMPANY AND SR.NO_EMP = ME.NO_EMP
          INNER JOIN DZSN_MA_CC CC ON MS.CD_COMPANY = CC.CD_COMPANY AND MS.CD_CC = CC.CD_CC
		  LEFT OUTER JOIN DZSN_MA_BIZAREA E ON SR.CD_BIZAREA = E.CD_BIZAREA AND SR.CD_COMPANY = E.CD_COMPANY
		  -- 장은경(2014.02.03)
		  LEFT OUTER JOIN	(
					-- WITH(READUNCOMMITTED)
					SELECT	P.CD_COMPANY, P.NO_RCP, MIN(D.NO_DOCU) NO_DOCU, MIN(D.ST_DOCU) ST_DOCU,
							MAX(D.CD_DEPOSIT) AS CD_DEPOSIT
					FROM	SA_RCPH P
					INNER JOIN FI_DOCU D	ON	P.CD_COMPANY = D.CD_COMPANY AND P.NO_RCP = D.NO_MDOCU 
					WHERE	P.CD_COMPANY = @CD_COMPANY AND P.DT_RCP BETWEEN @DT_RPC_FROM AND @DT_RPC_TO 	
					GROUP BY P.CD_COMPANY, P.NO_RCP
					) F		ON	SR.CD_COMPANY = F.CD_COMPANY AND SR.NO_RCP = F.NO_RCP 
		 LEFT OUTER JOIN DZSN_MA_PARTNER MP ON MP.CD_COMPANY = SR.CD_COMPANY AND MP.CD_PARTNER = SR.CD_PARTNER
		 LEFT OUTER JOIN DZSN_MA_CODEDTL PG ON PG.CD_COMPANY = SR.CD_COMPANY AND PG.CD_SYSDEF = MP.CD_PARTNER_GRP AND PG.CD_FIELD = 'MA_B000065'
		 LEFT OUTER JOIN DZSN_MA_PARTNER MP2 ON MP2.CD_COMPANY = SR.CD_COMPANY AND MP2.CD_PARTNER = SR.GI_PARTNER
		 LEFT OUTER JOIN DZSN_MA_CODEDTL C3 ON C3.CD_COMPANY = F.CD_COMPANY AND C3.CD_FIELD = 'FI_J000003' AND C3.CD_SYSDEF = F.ST_DOCU
		 LEFT OUTER JOIN (
							SELECT CD_COMPANY, NO_PROJECT, MAX(NM_PROJECT) NM_PROJECT
							FROM SA_PROJECTH
							WHERE CD_COMPANY = @CD_COMPANY
							GROUP BY CD_COMPANY, NO_PROJECT
							) PJ ON PJ.CD_COMPANY = SR.CD_COMPANY AND PJ.NO_PROJECT = SR.NO_PROJECT 
		 LEFT OUTER JOIN DZSN_MA_SALEORG SG ON MS.CD_COMPANY = SG.CD_COMPANY AND MS.CD_SALEORG = SG.CD_SALEORG
		 LEFT JOIN (SELECT RD.CD_COMPANY, RD.NO_RCP,
					       SUM(CASE WHEN RD.FG_RCP = '008' THEN (CASE WHEN RD.AM_RCP_A = 0 THEN RD.AM_RCP 
																						   ELSE RD.AM_RCP_A END) 
													       ELSE 0 END) AS AM_RCP, -- 수수료
						   SUM(CASE WHEN RD.FG_RCP = '008' THEN (CASE WHEN RD.AM_RCP_A_EX = 0 THEN RD.AM_RCP_EX 
																							  ELSE RD.AM_RCP_A_EX END) 
														   ELSE 0 END) AS AM_RCP_EX, -- 수수료
						   SUM(CASE WHEN RD.FG_RCP = '020' THEN (CASE WHEN RD.AM_RCP_A = 0 THEN RD.AM_RCP 
																						   ELSE RD.AM_RCP_A END) 
														   ELSE 0 END) AS AM_RCP1, -- 잡이익
						   SUM(CASE WHEN RD.FG_RCP = '020' THEN (CASE WHEN RD.AM_RCP_A_EX = 0 THEN RD.AM_RCP_EX 
																							  ELSE RD.AM_RCP_A_EX END) 
													       ELSE 0 END) AS AM_RCP1_EX -- 잡이익
					FROM SA_RCPL RD
					GROUP BY RD.CD_COMPANY, RD.NO_RCP) RD
		 ON RD.CD_COMPANY = SR.CD_COMPANY AND RD.NO_RCP = SR.NO_RCP
		 WHERE  SR.CD_COMPANY = @CD_COMPANY  
		   AND  SR.DT_RCP BETWEEN @DT_RPC_FROM AND @DT_RPC_TO  
		   AND (SR.CD_TP = @CD_TP OR @CD_TP = '' OR @CD_TP IS NULL) 
		   AND (SR.TP_BUSI = @TP_BUSI OR @TP_BUSI  = '' OR @TP_BUSI  IS NULL)  
		   AND (SR.CD_PARTNER = @CD_PARTNER OR @CD_PARTNER = '' OR @CD_PARTNER IS NULL)  
		   AND (SR.BILL_PARTNER = @BILL_PARTNER OR @BILL_PARTNER = '' OR @BILL_PARTNER IS NULL)   
		   AND (@P_MULTI_SALEGRP IS NULL OR @P_MULTI_SALEGRP = '' OR (SR.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEGRP))))  
		   AND (SR.NO_EMP = @NO_EMP OR @NO_EMP = '' OR @NO_EMP IS NULL) 
		   AND (SR.TP_AIS = @TP_AIS OR @TP_AIS = '' OR @TP_AIS IS NULL) 
		   AND (( @TP_BANJE = '0' AND SR.AM_RCP > SR.AM_RCP_TX )
		   OR   ( @TP_BANJE = '1' AND SR.AM_RCP = SR.AM_RCP_TX )
		   OR   ( @TP_BANJE = ''  OR @TP_BANJE IS NULL ))
		   --AND (SR.CD_BIZAREA = @P_CD_BIZAREA OR @P_CD_BIZAREA = '' OR @P_CD_BIZAREA IS NULL)
		   	AND		(SR.CD_BIZAREA   IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_BIZAREA)) OR @P_CD_BIZAREA = '' OR @P_CD_BIZAREA IS NULL)    
		   AND (@P_FG_RCP = '' OR @P_FG_RCP IS NULL OR EXISTS ( SELECT 1 
		                                                        FROM   SA_RCPL L 
					                                            WHERE  L.FG_RCP     = @P_FG_RCP
					                                            AND    L.NO_RCP     = SR.NO_RCP
					                                            AND    L.CD_COMPANY = SR.CD_COMPANY )
	   	        )
	   	   AND (MP.CD_PARTNER_GRP = @P_CD_PARTNER_GRP OR @P_CD_PARTNER_GRP = '' OR @P_CD_PARTNER_GRP IS NULL) 
		   AND (@P_MULTI_SALEORG IS NULL OR @P_MULTI_SALEORG = '' OR (SG.CD_SALEORG IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEORG))))  
		   --AND (SR.NO_RCP = @KEY OR @KEY = '' OR @KEY IS NULL) 

		SELECT  SR.NO_RCP, SR.DT_RCP, SR.CD_TP, C01.NM_SYSDEF AS NM_FG_ISSUE, 
				SR.TP_BUSI, C02.NM_SYSDEF AS NM_TP_BUSI, SL.FG_RCP, C03.NM_SYSDEF AS NM_FG_RCP, 
				SL.NO_MGMT, SL.FG_JATA, C04.NM_SYSDEF AS NM_FG_JATA, 
				SL.AM_RCP_EX,		--정상수금(외화)
				SL.AM_RCP,			--정상수금(원화)
				SL.AM_RCP_A_EX,		--선수금(외화)
				SL.AM_RCP_A,		--선수금(원화)
				SL.DC_BANK,			--발행기관
				SL.CD_BANK,			--금융기관
				--C05.NM_SYSDEF AS NM_CD_BANK, 
				(SELECT LN_PARTNER FROM DZSN_MA_PARTNER WHERE CD_COMPANY = SL.CD_COMPANY AND CD_PARTNER = SL.CD_BANK) NM_CD_BANK,
				SL.NM_ISSUE,		--발행인
				SL.DT_DUE, SL.DY_TURN, 
				SR.CD_PARTNER, M01.LN_PARTNER, M01.NM_CEO, SR.BILL_PARTNER, M02.LN_PARTNER AS LN_BILL_PARTNER, M02.NM_CEO AS BILL_NM_CEO,
				SR.CD_SALEGRP, MS.NM_SALEGRP, SR.NO_EMP, SR.NO_PROJECT,   
				SR.AM_RCPS, SR.TP_AIS, SR.DC_RMK, SL.DY_TURN, 
			   (SUBSTRING(SL.DTS_INSERT, 1, 4) + '/'+ SUBSTRING(SL.DTS_INSERT, 5, 2) + '/'+ SUBSTRING(SL.DTS_INSERT, 7, 2) + '/'+ 
				SUBSTRING(SL.DTS_INSERT, 9, 2) + ':'+ SUBSTRING(SL.DTS_INSERT, 11, 2)) AS DTS_INSERT, 
                MD.NM_KOR AS ID_INSERT,
                SL.AM_RCP_K,
				SL.VAT,
				SG.CD_SALEORG,
				SG.NM_SALEORG
		  FROM  SA_RCPH SR
		  INNER JOIN  SA_RCPL SL ON SR.CD_COMPANY = SL.CD_COMPANY AND SR.NO_RCP = SL.NO_RCP
		  LEFT OUTER JOIN DZSN_MA_PARTNER M01 ON SR.CD_COMPANY = M01.CD_COMPANY AND SR.CD_PARTNER = M01.CD_PARTNER
		  LEFT OUTER JOIN DZSN_MA_PARTNER M02 ON SR.CD_COMPANY = M02.CD_COMPANY AND SR.BILL_PARTNER = M02.CD_PARTNER
		  LEFT OUTER JOIN DZSN_MA_SALEGRP MS  ON SR.CD_COMPANY = MS.CD_COMPANY  AND SR.CD_SALEGRP = MS.CD_SALEGRP
		  LEFT OUTER JOIN DZSN_MA_CODEDTL C01 ON SR.CD_COMPANY = C01.CD_COMPANY AND SR.CD_TP   = C01.CD_SYSDEF AND C01.CD_FIELD = 'SA_B000032'	
		  LEFT OUTER JOIN DZSN_MA_CODEDTL C02 ON SR.CD_COMPANY = C02.CD_COMPANY AND SR.TP_BUSI = C02.CD_SYSDEF AND C02.CD_FIELD = 'PU_C000016'	
		  LEFT OUTER JOIN DZSN_MA_CODEDTL C03 ON SL.CD_COMPANY = C03.CD_COMPANY AND SL.FG_RCP  = C03.CD_SYSDEF AND C03.CD_FIELD = 'SA_B000002'	
		  LEFT OUTER JOIN DZSN_MA_CODEDTL C04 ON SL.CD_COMPANY = C04.CD_COMPANY AND SL.FG_JATA = C04.CD_SYSDEF AND C04.CD_FIELD = 'SA_B000012'	
		  --LEFT  JOIN DZSN_MA_CODEDTL C05 ON SL.CD_COMPANY = C05.CD_COMPANY AND SL.CD_BANK = C05.CD_SYSDEF AND C05.CD_FIELD = 'MA_B000043'	
          LEFT OUTER JOIN DZSN_MA_EMP     MD  ON SL.CD_COMPANY = MD.CD_COMPANY  AND SL.ID_INSERT = MD.NO_EMP
		  LEFT OUTER JOIN DZSN_MA_SALEORG SG ON MS.CD_COMPANY = SG.CD_COMPANY AND MS.CD_SALEORG = SG.CD_SALEORG
		 WHERE  SR.CD_COMPANY = @CD_COMPANY  
		   AND  SR.DT_RCP BETWEEN @DT_RPC_FROM AND @DT_RPC_TO  
		   AND (SR.CD_TP = @CD_TP OR @CD_TP = '' OR @CD_TP IS NULL) 
		   AND (SR.TP_BUSI = @TP_BUSI OR @TP_BUSI  = '' OR @TP_BUSI  IS NULL)  
		   AND (SR.CD_PARTNER = @CD_PARTNER OR @CD_PARTNER = '' OR @CD_PARTNER IS NULL)  
		   AND (SR.BILL_PARTNER = @BILL_PARTNER OR @BILL_PARTNER = '' OR @BILL_PARTNER IS NULL)   
		   AND (@P_MULTI_SALEGRP IS NULL OR @P_MULTI_SALEGRP = '' OR (SR.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEGRP))))  
		   AND (SR.NO_EMP = @NO_EMP OR @NO_EMP = '' OR @NO_EMP IS NULL) 
		   AND (SR.TP_AIS = @TP_AIS OR @TP_AIS = '' OR @TP_AIS IS NULL) 
		   AND (( @TP_BANJE = '0' AND SR.AM_RCP > SR.AM_RCP_TX ) 
		   OR   ( @TP_BANJE = '1' AND SR.AM_RCP = SR.AM_RCP_TX ) 
		   OR   ( @TP_BANJE = ''  OR @TP_BANJE IS NULL ))
		   --AND (SR.CD_BIZAREA = @P_CD_BIZAREA OR @P_CD_BIZAREA = '' OR @P_CD_BIZAREA IS NULL)
		   	AND		(SR.CD_BIZAREA   IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_BIZAREA)) OR @P_CD_BIZAREA = '' OR @P_CD_BIZAREA IS NULL)  
			AND (@P_MULTI_SALEORG IS NULL OR @P_MULTI_SALEORG = '' OR (SG.CD_SALEORG IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEORG))))  
		   --AND (SR.NO_RCP = @KEY) 

		SELECT  SR.NO_RCP,
				SD.NO_TX,
				SD.TP_SO,
				SD.DT_IV,
				SD.AM_IV_EX,
				SD.AM_IV,
				SD.RT_EXCH_IV,
				SD.AM_PL,
				CASE WHEN SIGN(SD.AM_PL) = 1   THEN 0 - (SD.AM_PL) ELSE 0 END AS AM_PL_LOSS,
				CASE WHEN SIGN(SD.AM_PL) = -1  THEN 0 - (SD.AM_PL) ELSE 0 END AS AM_PL_PROFIT,
				SD.AM_RCP_TX,
				SD.AM_RCP_TX_EX,
				MD.NM_KOR AS ID_INSERT,
			   (SUBSTRING(SD.DTS_INSERT, 1, 4) + '/'+ SUBSTRING(SD.DTS_INSERT, 5, 2) + '/'+ SUBSTRING(SD.DTS_INSERT, 7, 2) + '/'+ 
				SUBSTRING(SD.DTS_INSERT, 9, 2) + ':'+ SUBSTRING(SD.DTS_INSERT, 11, 2)) AS DTS_INSERT, 
				SD.AM_IV_VAT,
				SD.AM_RCP_VAT,
				SD.AM_IV_K,
				SD.AM_RCP_K,
				SD.CD_USERDEF1,
				IH.DT_RCP_RSV,
				SG.CD_SALEORG,
				SG.NM_SALEORG
		  FROM  SA_RCPH SR
		  INNER JOIN SA_RCPD SD ON SR.CD_COMPANY = SD.CD_COMPANY AND SR.NO_RCP = SD.NO_RCP
		  LEFT OUTER JOIN DZSN_MA_EMP   MD  ON SD.CD_COMPANY = MD.CD_COMPANY  AND SD.ID_INSERT = MD.NO_EMP
		  LEFT OUTER JOIN SA_IVH IH ON IH.CD_COMPANY = SR.CD_COMPANY AND IH.NO_IV = SD.NO_TX
		  LEFT OUTER JOIN DZSN_MA_SALEGRP MS  ON SR.CD_COMPANY = MS.CD_COMPANY  AND SR.CD_SALEGRP = MS.CD_SALEGRP
		  LEFT OUTER JOIN DZSN_MA_SALEORG SG ON MS.CD_COMPANY = SG.CD_COMPANY AND MS.CD_SALEORG = SG.CD_SALEORG
		 WHERE  SR.CD_COMPANY = @CD_COMPANY  
		   AND  SR.DT_RCP BETWEEN @DT_RPC_FROM AND @DT_RPC_TO  
		   AND (SR.CD_TP = @CD_TP OR @CD_TP = '' OR @CD_TP IS NULL) 
		   AND (SR.TP_BUSI = @TP_BUSI OR @TP_BUSI  = '' OR @TP_BUSI  IS NULL)  
		   AND (SR.CD_PARTNER = @CD_PARTNER OR @CD_PARTNER = '' OR @CD_PARTNER IS NULL)  
		   AND (SR.BILL_PARTNER = @BILL_PARTNER OR @BILL_PARTNER = '' OR @BILL_PARTNER IS NULL)   
		   AND (@P_MULTI_SALEGRP IS NULL OR @P_MULTI_SALEGRP = '' OR (SR.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEGRP))))  
		   AND (SR.NO_EMP = @NO_EMP OR @NO_EMP = '' OR @NO_EMP IS NULL) 
		   AND (SR.TP_AIS = @TP_AIS OR @TP_AIS = '' OR @TP_AIS IS NULL) 
		   AND (( @TP_BANJE = '0' AND SR.AM_RCP > SR.AM_RCP_TX ) 
		   OR   ( @TP_BANJE = '1' AND SR.AM_RCP = SR.AM_RCP_TX ) 
		   OR   ( @TP_BANJE = ''  OR @TP_BANJE IS NULL ))
		   --AND (SR.CD_BIZAREA = @P_CD_BIZAREA OR @P_CD_BIZAREA = '' OR @P_CD_BIZAREA IS NULL)
		   
		   	AND		(SR.CD_BIZAREA   IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_BIZAREA)) OR @P_CD_BIZAREA = '' OR @P_CD_BIZAREA IS NULL)  
			AND (@P_MULTI_SALEORG IS NULL OR @P_MULTI_SALEORG = '' OR (SG.CD_SALEORG IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEORG))))  
		   	--AND (SR.NO_RCP = @KEY)
		   
		  UNION ALL
		  
		  SELECT D.NO_RCP, 
		         D.NO_IV,
		         '' TP_SO, 
		         D.DT_IV,        --마감일자
		         D.AM_TARGET_EX, --대상액
		         D.AM_TARGET,    --대상액(원화)
		         D.RT_EXCH_IV,   --마감환율
		         D.AM_PL,        --환차손 
		         CASE WHEN SIGN(D.AM_PL) = 1   THEN 0 - (D.AM_PL) ELSE 0 END AS AM_PL_LOSS,
			     CASE WHEN SIGN(D.AM_PL) = -1  THEN 0 - (D.AM_PL) ELSE 0 END AS AM_PL_PROFIT,
		         D.AM_RCPS_EX,   --반제액
		         D.AM_RCPS,      --반제액(원화)
		         '' ID_INSERT,
		         '' DTS_INSERT,
		         0.0 AS AM_IV_VAT,
				 0.0 AS AM_RCP_VAT,
				 0.0 AS AM_IV_K,
				 0.0 AS AM_RCP_K  ,
				 '' CD_USERDEF1,
				 '' DT_RCP_RSV,
				 '' CD_SALEORG,
				 '' NM_SALEORG
         FROM    SA_BILLSH H   
                 INNER JOIN SA_BILLSD D ON H.CD_COMPANY = D.CD_COMPANY AND H.NO_BILLS = D.NO_BILLS  
         WHERE  (H.CD_COMPANY = @CD_COMPANY)  
         AND    (H.CD_TP = @CD_TP OR @CD_TP = '' OR @CD_TP IS NULL)   
         AND    (H.TP_BUSI = @TP_BUSI OR @TP_BUSI  = '' OR @TP_BUSI  IS NULL)    
         AND    (H.CD_PARTNER = @CD_PARTNER OR @CD_PARTNER = '' OR @CD_PARTNER IS NULL)    
         AND    (H.BILL_PARTNER = @BILL_PARTNER OR @BILL_PARTNER = '' OR @BILL_PARTNER IS NULL)     
         AND    (H.NO_EMP = @NO_EMP OR @NO_EMP = '' OR @NO_EMP IS NULL)   
         AND	(@P_MULTI_SALEGRP IS NULL OR @P_MULTI_SALEGRP = '' OR (H.CD_BILLTGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEGRP))))  
         AND    @V_SERVER_KEY LIKE 'SLFIRE%' --신라파이어 
	
	END

ELSE IF ( @PAGE_ORDER = 'R' )  --여기 주석 이하부분은 레포트 출력때문에 호출
	BEGIN 
  		SELECT  SR.NO_RCP,   
				SR.DT_RCP, SR.CD_TP, SR.TP_BUSI, MC.NM_SYSDEF AS NM_TP_BUSI, 
				SR.CD_PARTNER, M01.LN_PARTNER, M01.NM_CEO,
				SR.BILL_PARTNER, M02.LN_PARTNER AS LN_BILL_PARTNER, M02.NM_CEO AS BILL_NM_CEO,
				SR.CD_SALEGRP, MS.NM_SALEGRP, SR.NO_EMP, SR.NO_PROJECT,   
				/*라인데이터를 봐야한다
				--SR.AM_RCP_EX,		--정상수금(외화)
				--SR.AM_RCP,		--정상수금(원화)
				--SR.AM_RCP_EX_TX,	--반제액(외화)
				--SR.AM_RCP_TX,		--반제액(원화)
				--SR.AM_RCP_A_EX,	--선수금(외화)
				--SR.AM_RCP_A,		--선수금(원화)
				--SR.AM_RCPS, 
				*/
				SL.DC_BANK,			--발행기관
				SL.CD_BANK,			--금융기관
				--C04.NM_SYSDEF AS NM_CD_BANK, 
				(SELECT LN_PARTNER FROM DZSN_MA_PARTNER WHERE CD_COMPANY = SL.CD_COMPANY AND CD_PARTNER = SL.CD_BANK) NM_CD_BANK,
				SL.NM_ISSUE,		--발행인
				SR.TP_AIS, SR.DC_RMK, 
			    SL.NO_LINE, SL.FG_RCP, SL.NO_MGMT, SL.FG_JATA, SL.AM_RCP, 
				SL.AM_RCP_A, SL.DT_DUE, SL.DY_TURN,
                C02.NM_SYSDEF AS NM_FG_RCP, C03.NM_SYSDEF AS NM_FG_JATA, 
                C01.NM_SYSDEF AS NM_FG_ISSUE, 
			   (SUBSTRING(SL.DTS_INSERT, 1, 4) + '/'+ SUBSTRING(SL.DTS_INSERT, 5, 2) + '/'+ SUBSTRING(SL.DTS_INSERT, 7, 2) + '/'+ 
				SUBSTRING(SL.DTS_INSERT, 9, 2) + ':'+ SUBSTRING(SL.DTS_INSERT, 11, 2)) AS DTS_INSERT, 
                MD.NM_KOR AS ID_INSERT,
				M01.FG_BILL,        --수금조건
				M01.DT_RCP_PREARRANGED, --수금예정일  
				SG.CD_SALEORG,
				SG.NM_SALEORG
		  FROM  SA_RCPH SR   
          JOIN  SA_RCPL SL ON SR.CD_COMPANY = SL.CD_COMPANY AND SR.NO_RCP = SL.NO_RCP
		  LEFT  JOIN DZSN_MA_PARTNER M01 ON SR.CD_COMPANY = M01.CD_COMPANY AND SR.CD_PARTNER = M01.CD_PARTNER
		  LEFT  JOIN DZSN_MA_PARTNER M02 ON SR.CD_COMPANY = M02.CD_COMPANY AND SR.BILL_PARTNER = M02.CD_PARTNER
		  LEFT  JOIN DZSN_MA_SALEGRP MS  ON SR.CD_COMPANY = MS.CD_COMPANY  AND SR.CD_SALEGRP = MS.CD_SALEGRP
		  LEFT  JOIN DZSN_MA_CODEDTL MC  ON SR.CD_COMPANY = MC.CD_COMPANY  AND SR.TP_BUSI = MC.CD_SYSDEF  AND MC.CD_FIELD = 'PU_C000016'
		  LEFT  JOIN DZSN_MA_CODEDTL C01 ON SR.CD_COMPANY = C01.CD_COMPANY AND SR.CD_TP   = C01.CD_SYSDEF AND C01.CD_FIELD = 'SA_B000032'	
		  LEFT  JOIN DZSN_MA_CODEDTL C02 ON SL.CD_COMPANY = C02.CD_COMPANY AND SL.FG_RCP  = C02.CD_SYSDEF AND C02.CD_FIELD = 'SA_B000002'	
		  LEFT  JOIN DZSN_MA_CODEDTL C03 ON SL.CD_COMPANY = C03.CD_COMPANY AND SL.FG_JATA = C03.CD_SYSDEF AND C03.CD_FIELD = 'SA_B000012'	
		  --LEFT  JOIN DZSN_MA_CODEDTL C04 ON SL.CD_COMPANY = C04.CD_COMPANY AND SL.CD_BANK = C04.CD_SYSDEF AND C04.CD_FIELD = 'MA_B000043'
          LEFT  JOIN DZSN_MA_EMP     MD  ON SL.CD_COMPANY = MD.CD_COMPANY  AND SL.ID_INSERT = MD.NO_EMP	
		  LEFT OUTER JOIN DZSN_MA_SALEORG SG ON MS.CD_COMPANY = SG.CD_COMPANY AND MS.CD_SALEORG = SG.CD_SALEORG
		 WHERE  SR.CD_COMPANY = @CD_COMPANY  
		   AND  SR.DT_RCP BETWEEN @DT_RPC_FROM AND @DT_RPC_TO  
		   AND (SR.CD_TP = @CD_TP OR @CD_TP = '' OR @CD_TP IS NULL) 
		   AND (SR.TP_BUSI = @TP_BUSI OR @TP_BUSI  = '' OR @TP_BUSI  IS NULL)  
		   AND (SR.CD_PARTNER = @CD_PARTNER OR @CD_PARTNER = '' OR @CD_PARTNER IS NULL)  
		   AND (SR.BILL_PARTNER = @BILL_PARTNER OR @BILL_PARTNER = '' OR @BILL_PARTNER IS NULL)   
		   AND (@P_MULTI_SALEGRP IS NULL OR @P_MULTI_SALEGRP = '' OR (SR.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEGRP))))  
		   AND (SR.NO_EMP = @NO_EMP OR @NO_EMP = '' OR @NO_EMP IS NULL) 
		   AND (SR.TP_AIS = @TP_AIS OR @TP_AIS = '' OR @TP_AIS IS NULL)  
		   AND (( @TP_BANJE = '0' AND SR.AM_RCP > SR.AM_RCP_TX ) 
		   OR   ( @TP_BANJE = '1' AND SR.AM_RCP = SR.AM_RCP_TX ) 
		   OR   ( @TP_BANJE = ''  OR @TP_BANJE IS NULL ))
		   AND ((@TABIDX = '0'  AND  SR.NO_RCP IN (SELECT * FROM TF_GETSPLIT(@KEY)))     
	       OR   (@TABIDX = '1'  AND  SR.BILL_PARTNER IN (SELECT * FROM TF_GETSPLIT(@KEY)))     
	       OR   (@TABIDX = '2'  AND  SR.CD_PARTNER IN (SELECT * FROM TF_GETSPLIT(@KEY)))     
	       OR   (@TABIDX = '3'  AND  SR.DT_RCP IN (SELECT * FROM TF_GETSPLIT(@KEY))) 
	       OR   (@TABIDX = '4'  AND  SR.CD_SALEGRP IN (SELECT * FROM TF_GETSPLIT(@KEY))) 
	       OR   (@TABIDX = '5'  AND  SR.CD_TP IN (SELECT * FROM TF_GETSPLIT(@KEY)))) 
	       --AND (SR.CD_BIZAREA = @P_CD_BIZAREA OR @P_CD_BIZAREA = '' OR @P_CD_BIZAREA IS NULL)
		   	AND		(SR.CD_BIZAREA   IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_BIZAREA)) OR @P_CD_BIZAREA = '' OR @P_CD_BIZAREA IS NULL)  
			AND (@P_MULTI_SALEORG IS NULL OR @P_MULTI_SALEORG = '' OR (SG.CD_SALEORG IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_SALEORG))))  
	END
GO
