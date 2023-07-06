USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_BILL_SUB_S]    Script Date: 2015-12-11 오후 3:18:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System			: 영업관리
**  Sub System      : 영업관리
**  Page            : 수금등록
**  Desc            : 반제등록 도움창 조회
**  참           고 : 
**  Return Values
**
**  수    정    자  : 장은경
**  작    성    일  : 2006.09.05
**  수    정    자  :이승훈 (환종추가및 환율및 외화반제금액표시추가 20080430 
**  수    정    내   용 : 기존 SP_SA_BILL_SUB_SELECT
*********************************************
** Change History
*********************************************
*********************************************/
ALTER PROCEDURE [NEOE].[SP_CZ_SA_BILL_SUB_S]
(
    @P_CD_COMPANY        NVARCHAR(7),
	@P_LANGUAGE			 NVARCHAR(5) = 'KR',
    @P_DT_FROM           NVARCHAR(8),
    @P_DT_TO             NVARCHAR(8),
    @P_CD_PARTNER        NVARCHAR(20),
    @P_BILL_PARTNER      NVARCHAR(20),
    @P_TP_BUSI           NVARCHAR(3),
    @P_MULTI_NO_TX       NVARCHAR(4000),
    @P_CD_DEPT           NVARCHAR(12),
    @P_NO_EMP            NVARCHAR(10),
    @P_CD_PJT            NVARCHAR(20) = NULL,
    @P_GI_PARTNER		 NVARCHAR(20) = NULL,
    @P_CD_BIZAREA		 NVARCHAR(7) = NULL,
	@P_DT_BILL			 NVARCHAR(8)   
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' S,
	   T.NO_IV,
	   T.DT_PROCESS,
	   T.CD_EXCH AS CD_EXCH,   --환종
	   (SELECT (CASE @P_LANGUAGE WHEN 'KR' THEN NM_SYSDEF
							     WHEN 'US' THEN NM_SYSDEF_E
							     WHEN 'JP' THEN NM_SYSDEF_JP
							     WHEN 'CH' THEN NM_SYSDEF_CH END)
		FROM MA_CODEDTL
		WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_FIELD = 'MA_B000005'
		AND CD_SYSDEF = T.CD_EXCH) AS NM_EXCH,
	   T.RT_EXCH AS RT_EXCH,   --환율
	   (CASE WHEN ISNULL(ME.RATE_BASE, 0) = 0 THEN 1 ELSE ISNULL(ME.RATE_BASE, 0) END) AS RT_EXCH_CUR,
	   T.AM_EX   AS AM_EX,     --마감금액(외화)
	   T.AM_K    AS AM_K,      --마감금액(원화)
	   T.AM_VAT  AS AM_VAT,    --마감부가세(원화)
	   T.AM_K + T.AM_VAT   AS AM_TOT,      --마감합계금액(원화)
	   T.AM_BAN_EX     AS AM_BAN_EX,  --기반제액(외화)
	   T.AM_BAN        AS AM_BAN,     --기반제액(원화)
	   T.AM_RCP_EX     AS AM_RCP_EX,     --반제대상액(외화) --> EDIT대상
	   T.AM_RCP_JAN_EX AS AM_RCP_JAN_EX, --반제잔액(외화)   -->자동계산
	   T.AM_RCP        AS AM_RCP,        --반제대상액(원화) --> EDIT대상 
	   T.AM_PL         AS AM_PL_LOSS,      --환차손(원화)   -->자동계산 
	   T.AM_PL         AS AM_PL_PROFIT,    --환차익(원화)   -->자동계산 
	   T.AM_PL         AS AM_PL,         --환차손익(원화)   -->자동계산 
	   T.AM_RCP_JAN    AS AM_RCP_JAN,     --반제잔액(원화)   --자동계산 
	   T.NM_DEPT		AS NM_DEPT,     --마감부서
	   T.NM_KOR		AS NM_EMP,      --마감사원		-- 2011-04-13,  최승애  수정내용 : T.NM_DEPT  ===> T.NM_KOR로 변경함.
       (SELECT TOP 1 NO_PROJECT FROM SA_PJTCREDIT WHERE CD_COMPANY = @P_CD_COMPANY AND NO_PROJECT = T.CD_PJT) AS NO_PROJECT_PJTCREDIT,  --특별여신프로젝트(영우디지털용)
	   T.IV_GUBUN, --선입선출반제우선순위선정용
	   T.TP_AIS,
	   T.TP_IV,
	   T.CD_PJT,
	   (SELECT TOP 1 NM_PROJECT FROM SA_PROJECTH WHERE CD_COMPANY = @P_CD_COMPANY AND NO_PROJECT = T.CD_PJT) AS NM_PJT,
	   T.DC_REMARK
FROM (SELECT H.NO_IV,
		     H.DT_PROCESS, 
	         ISNULL(H.CD_EXCH, '000') CD_EXCH, --환종 
	         ISNULL(H.RT_EXCH, 1) RT_EXCH,     -- 환율(기표환율)
	         ISNULL(H.AM_EX,   0) AM_EX,       -- 마감금액(외화)
	         ISNULL(H.AM_K,    0) AM_K,        -- 공급가액(원화)
	         ISNULL(H.VAT_TAX, 0) AM_VAT,      -- 부가세
	         ISNULL(H.AM_BAN_EX,  0) AM_BAN_EX,    -- 반제액(외화)
	         0.0 AM_RCP_EX,                                                  -- 반제대상액(외화)                                                        
	         ROUND(ISNULL(H.AM_EX, 0) + (ISNULL(H.VAT_TAX, 0) / ISNULL(H.RT_EXCH, 1))  - ISNULL(H.AM_BAN_EX, 0), 2) AS AM_RCP_JAN_EX, -- 반제잔액(외화)
	         ISNULL(H.AM_BAN,  0) AM_BAN,        -- 반제액(원화)
	         0.0 AM_PL,        -- 환차손익(원화)
	         0.0 AM_RCP,                                                -- 반제대상액(원화)
	         (ISNULL(H.AM_K, 0) + ISNULL(H.VAT_TAX, 0) - ISNULL(H.AM_BAN, 0))  AM_RCP_JAN,  -- 반제잔액(원화)
		     (SELECT TOP 1 D.NM_DEPT FROM MA_DEPT D WHERE D.CD_COMPANY = H.CD_COMPANY AND D.CD_DEPT = H.CD_DEPT) AS NM_DEPT,
		     (SELECT TOP 1 E.NM_KOR  FROM MA_EMP E  WHERE E.CD_COMPANY = H.CD_COMPANY AND E.NO_EMP = H.NO_EMP) AS NM_KOR,
		     (SELECT TOP 1 L.CD_PJT FROM SA_IVL L WHERE L.CD_COMPANY = H.CD_COMPANY AND L.NO_IV = H.NO_IV) AS CD_PJT,
		     CASE SIGN(H.AM_EX) WHEN -1 THEN '0' ELSE '1' END AS IV_GUBUN,
		     H.TP_AIS,
			 L.TP_IV,
			 H.DC_REMARK
	    FROM SA_IVH H
		JOIN (SELECT L.CD_COMPANY, L.NO_IV, MAX(L.TP_IV) AS TP_IV 
			  FROM SA_IVL L
			  LEFT JOIN MM_QTIO QT ON QT.CD_COMPANY = QT.CD_COMPANY AND QT.NO_IO = L.NO_IO
			  AND (@P_CD_PJT IS NULL OR @P_CD_PJT = '' OR L.CD_PJT = @P_CD_PJT)
			  AND (QT.GI_PARTNER = @P_GI_PARTNER OR @P_GI_PARTNER = '' OR @P_GI_PARTNER IS NULL)
			  AND (QT.CD_BIZAREA = @P_CD_BIZAREA OR @P_CD_BIZAREA = '' OR @P_CD_BIZAREA IS NULL)
			  GROUP BY L.CD_COMPANY, L.NO_IV, L.TP_IV) L 
	    ON L.CD_COMPANY = H.CD_COMPANY AND L.NO_IV = H.NO_IV
	    WHERE H.CD_COMPANY = @P_CD_COMPANY
	    AND H.DT_PROCESS BETWEEN @P_DT_FROM AND @P_DT_TO
	    AND H.CD_PARTNER = @P_CD_PARTNER
	    AND H.FG_TRANS = @P_TP_BUSI
	    AND H.BILL_PARTNER = @P_BILL_PARTNER
	    AND (ISNULL(@P_CD_DEPT, '') = '' OR H.CD_DEPT = @P_CD_DEPT)
	    AND (ISNULL(@P_NO_EMP, '') = '' OR H.NO_EMP = @P_NO_EMP)) T
	LEFT JOIN (SELECT CD_COMPANY, YYMMDD, CURR_SOUR,
			   	      (CASE WHEN CURR_SOUR = '002' THEN RATE_BASE ELSE ROUND(RATE_BASE, 2) END) AS RATE_BASE 
			   FROM MA_EXCHANGE
			   WHERE NO_SEQ = '1'
			   AND CURR_DEST = '000') ME
	ON ME.CD_COMPANY = @P_CD_COMPANY AND YYMMDD = @P_DT_BILL AND CURR_SOUR = T.CD_EXCH 
    WHERE ABS(ISNULL(T.AM_K, 0) + ISNULL(T.AM_VAT, 0)) - ABS(ISNULL(T.AM_BAN, 0)) > 0 
    AND (T.NO_IV NOT IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_NO_TX)) OR @P_MULTI_NO_TX IS NULL OR @P_MULTI_NO_TX = '')
    ORDER BY T.DT_PROCESS ASC , T.NO_IV ASC 

GO