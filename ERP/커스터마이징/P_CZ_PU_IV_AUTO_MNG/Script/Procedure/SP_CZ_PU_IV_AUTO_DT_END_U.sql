USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_IV_AUTO_DT_END_U]    Script Date: 2016-03-30 오후 1:48:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
           
ALTER PROCEDURE [NEOE].[SP_CZ_PU_IV_AUTO_DT_END_U]                
(
	@P_CD_COMPANY           NVARCHAR(7)
)                
AS                

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

CREATE TABLE #PU_IVH
(
	CD_COMPANY	NVARCHAR(7),
	NO_IV		NVARCHAR(20),
	NO_DOCU		NVARCHAR(20),
	NO_DOLINE	NUMERIC(5, 0),
	DT_PAY		NVARCHAR(8)
)

CREATE NONCLUSTERED INDEX IDX_CZ_SA_IVH ON #PU_IVH (CD_COMPANY, NO_IV)
CREATE NONCLUSTERED INDEX IDX_FI_DOCU ON #PU_IVH (CD_COMPANY, NO_DOCU, NO_DOLINE);

WITH A AS
(
	SELECT IH.CD_COMPANY,
		   IH.NO_IV,
		   IH.DT_PROCESS,
		   IH.CD_PARTNER,
		   IH.DT_PAY_PREARRANGED AS DT_PAY_IV,
		   IH.DT_DUE,
		   MP.DT_PAY_PREARRANGED,
		   MP1.FG_PAYMENT,
		   FD.NO_DOCU,
		   FD.NO_DOLINE,
		   FD.DT_START,
		   FD.DT_END,
		   FD.CD_MNG4,
		   FD.NM_MNGD4,
		   FD.AM_CR,
		   BH.AM_BAN,
		   CONVERT(CHAR(8), EOMONTH(IH.DT_PROCESS), 112) AS DT_IV
	FROM PU_IVH IH
	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = IH.CD_COMPANY AND MP.CD_PARTNER = IH.CD_PARTNER
	LEFT JOIN CZ_MA_PARTNER MP1 ON MP1.CD_COMPANY = IH.CD_COMPANY AND MP1.CD_PARTNER = IH.CD_PARTNER
	JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU,
				 FD.NO_DOCU,
				 FD.NO_DOLINE,
				 FD.DT_START,
				 FD.DT_END,
				 FD.CD_MNG4,
				 FD.NM_MNGD4,
				 FD.AM_CR
		  FROM FI_DOCU FD
		  WHERE FD.CD_ACCT IN ('30110', '30310')
		  AND FD.ST_DOCU = 2) FD
	ON FD.CD_COMPANY = IH.CD_COMPANY AND FD.NO_MDOCU = IH.NO_IV
	LEFT JOIN FI_BANH BH ON BH.CD_COMPANY = FD.CD_COMPANY AND BH.NO_DOCU = FD.NO_DOCU AND BH.NO_DOLINE = FD.NO_DOLINE
	WHERE IH.CD_COMPANY = @P_CD_COMPANY
	AND IH.FG_TRANS = '001'
),
B AS
(
	SELECT A.CD_COMPANY, A.CD_PARTNER,
		   LEFT(A.DT_PROCESS, 6) AS DT_MONTH,
	       SUM(A.AM_CR) AS AM_DOCU
    FROM A
    GROUP BY A.CD_COMPANY, A.CD_PARTNER, LEFT(A.DT_PROCESS, 6)
),
C AS
(
	SELECT A.*, 
		   (CASE WHEN ISNULL(A.DT_PAY_PREARRANGED, 0) > 0 THEN (CASE WHEN A.DT_PAY_PREARRANGED = 930 THEN CONVERT(CHAR(8), EOMONTH(CONVERT(CHAR(6), DATEADD(MONTH, 1, A.DT_PROCESS), 112) + '01'), 112) -- 익월 말일 결제 (30일)
																	 WHEN A.DT_PAY_PREARRANGED = 960 THEN CONVERT(CHAR(8), EOMONTH(CONVERT(CHAR(6), DATEADD(MONTH, 2, A.DT_PROCESS), 112) + '01'), 112) -- 익익월 말일 결제 (60일)
																	 WHEN A.DT_PAY_PREARRANGED = 907 THEN CONVERT(CHAR(8), DATEADD(DAY, 7, A.DT_PROCESS), 112) -- 세금계산서 발행일 + 7일
																	 WHEN A.DT_PAY_PREARRANGED = 914 THEN CONVERT(CHAR(8), DATEADD(DAY, 14, A.DT_PROCESS), 112) -- 세금계산서 발행일 + 14일
																	 WHEN A.DT_PAY_PREARRANGED = 931 THEN CONVERT(CHAR(8), DATEADD(DAY, 30, A.DT_PROCESS), 112) -- 세금계산서 발행일 + 30일
																	 WHEN A.DT_PAY_PREARRANGED = 996 THEN CONVERT(CHAR(8), DATEADD(DAY, 59, A.DT_PROCESS), 112) -- 세금계산서 발행일 + 59일
																	 WHEN A.DT_PAY_PREARRANGED = 961 THEN CONVERT(CHAR(8), DATEADD(DAY, 60, A.DT_PROCESS), 112) -- 세금계산서 발행일 + 60일
																	 WHEN A.DT_PAY_PREARRANGED = 997 THEN CONVERT(CHAR(8), DATEADD(DAY, -1, EOMONTH(CONVERT(CHAR(6), DATEADD(MONTH, 1, A.DT_PROCESS), 112) + '01')), 112) -- 익월말 하루전 결제
																	 WHEN (A.DT_PAY_PREARRANGED = 998 OR A.DT_PAY_PREARRANGED = 999) THEN A.DT_PROCESS -- 선지급(998), 즉시결제(999)
																	 ELSE CONVERT(CHAR(8), DATEADD(DAY, (A.DT_PAY_PREARRANGED % 30), EOMONTH(DATEADD(MONTH, A.DT_PAY_PREARRANGED / 30, A.DT_PROCESS))), 112) -- 지급예정일이 정해져 있는 매입처 (익월 n일, 1달을 30일로 봄)
																	 END)
														  ELSE (CASE WHEN B.AM_DOCU <= 1000000 THEN CONVERT(CHAR(8), DATEADD(DAY, 6, CONVERT(CHAR(6), DATEADD(MONTH, 1, A.DT_IV), 112) + '01'), 112) -- 100만원 이하, 익월 7일
																	 WHEN B.AM_DOCU > 1000000 AND B.AM_DOCU <= 3000000 THEN CONVERT(CHAR(8), DATEADD(DAY, 14, CONVERT(CHAR(6), DATEADD(MONTH, 1, A.DT_IV), 112) + '01'), 112) -- 100만원 초과 300만원 이하, 익월 15일
																	 WHEN B.AM_DOCU > 3000000 AND B.AM_DOCU <= 5000000 THEN CONVERT(CHAR(8), EOMONTH(CONVERT(CHAR(6), DATEADD(MONTH, 1, A.DT_IV), 112) + '01'), 112) -- 300만원 초과 500만원 이하, 익월 말일
																	 WHEN B.AM_DOCU > 5000000 AND B.AM_DOCU <= 10000000 THEN CONVERT(CHAR(8), DATEADD(DAY, 14, CONVERT(CHAR(6), DATEADD(MONTH, 2, A.DT_IV), 112) + '01'), 112) -- 500만원 초과 1000만원 이하, 월 마감 후 45일 (익익월 15일)
																	 WHEN B.AM_DOCU > 10000000 AND B.AM_DOCU <= 30000000 THEN CONVERT(CHAR(8), EOMONTH(CONVERT(CHAR(6), DATEADD(MONTH, 2, A.DT_IV), 112) + '01'), 112) -- 1000만원 초과 3000만원 이하, 월 마감 후 60일 (익익월 말일)
																	 WHEN B.AM_DOCU > 30000000 THEN CONVERT(CHAR(8), DATEADD(DAY, 14, CONVERT(CHAR(6), DATEADD(MONTH, 3, A.DT_IV), 112) + '01'), 112) -- 3000만원 초과, 월 마감 후 75일 (익익익월 15일)
																	 END) END) AS DT_PAY
	FROM A
	JOIN B ON B.CD_COMPANY = A.CD_COMPANY AND B.CD_PARTNER = A.CD_PARTNER AND B.DT_MONTH = LEFT(A.DT_PROCESS, 6)
)
INSERT INTO #PU_IVH
(
	CD_COMPANY,
	NO_IV,
	NO_DOCU,
	NO_DOLINE,
	DT_PAY
)
SELECT C.CD_COMPANY,
	   C.NO_IV,
	   C.NO_DOCU,
	   C.NO_DOLINE,
	   C.DT_PAY
FROM C
WHERE C.AM_CR <> C.AM_BAN
AND C.DT_END <> C.DT_PAY

UPDATE IH
SET IH.DT_PAY_PREARRANGED = IH1.DT_PAY,
	IH.DT_DUE = IH1.DT_PAY
FROM PU_IVH IH
JOIN #PU_IVH IH1 ON IH1.CD_COMPANY = IH.CD_COMPANY AND IH1.NO_IV = IH.NO_IV

UPDATE FD
SET FD.DT_START = IH.DT_PAY,
	FD.DT_END = IH.DT_PAY,
	FD.NM_MNGD4 = (CASE WHEN FD.CD_MNG4 = 'B22' THEN IH.DT_PAY ELSE FD.NM_MNGD4 END)
FROM FI_DOCU FD
JOIN #PU_IVH IH ON IH.CD_COMPANY = FD.CD_COMPANY AND IH.NO_DOCU = FD.NO_DOCU AND IH.NO_DOLINE = FD.NO_DOLINE

DROP TABLE #PU_IVH

GO