USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_ERP_TO_APS_IF]    Script Date: 2023-07-03 오후 6:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
            
ALTER PROCEDURE [NEOE].[SP_CZ_PR_ERP_TO_APS_IF] 
(
       @P_CD_COMPANY NVARCHAR(7)       
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_CAL
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_PERI
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_GPE
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_SECT
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_ZONE
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_ILOT
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_MACH
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_OPE
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_CADE
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_GAMM
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_PHAS
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_PREC
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_TYPM
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_ART
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_VER_ART
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_NOME
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_SER
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_OF
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_COMMANDE_ENTETE
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_COMMANDE_LIGNE
DELETE FROM ORTEMS_MIDDLE_PS.dbo.B_UTIL

-- B_CAL (캘린더 헤더)
;WITH A AS
(
	SELECT 
		ST.NO_CAL AS NOCALHEBD -- 캘린더코드
	,	ST.NO_CAL AS NOMCAL -- 캘린더설명
	FROM CZ_PR_SHIFT ST
	WHERE CD_COMPANY = @P_CD_COMPANY
	GROUP BY ST.NO_CAL
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_CAL(NOCALHEBD, NOMCAL)
SELECT * FROM A

-- B_PERI (캘린더 정보)
;WITH A AS
(
	SELECT 
		ST.NO_CAL AS NOCALHEBD
	,	ST.TP_START AS NOJOUR_DEB
	,	LEFT(ST.TM_START, 2) + ':' + SUBSTRING(ST.TM_START, 3, 2) AS DEB_PERIO
	,	ST.TP_END AS NOJOUR_FIN
	,	LEFT(ST.TM_END, 2) + ':' + SUBSTRING(ST.TM_END, 3, 2) AS FIN_PERIO
	FROM CZ_PR_SHIFT ST
	WHERE CD_COMPANY = @P_CD_COMPANY
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_PERI
(
	NOCALHEBD
,	NOJOUR_DEB
,	DEB_PERIO
,	NOJOUR_FIN
,	FIN_PERIO
)
SELECT * FROM A

-- B_GPE
;WITH A AS
(
	SELECT
		CD_SYSDEF AS CODE_GPE -- 배치그룹코드
	,	NM_SYSDEF AS LIB_GPE -- 배치그룹설명
	,	CD_FLAG1 AS CAPAMINI -- 최소capacity(최소투입수량)
	,	CD_FLAG2 AS CAPAMAXI -- 최대capacity(최대투입수량)
	FROM MA_CODEDTL
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND CD_FIELD = 'CZ_WIN0016'
	AND USE_YN = 'Y'
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_GPE
(
	CODE_GPE
,	LIB_GPE
,	CAPAMINI
,	CAPAMAXI
)
SELECT * FROM A

-- B_SECT (Section 정보)
;WITH A AS
(
       SELECT DISTINCT
              ISNULL(EQ.CD_DEPT, '') AS CODESECTI,
              ISNULL(MD.NM_DEPT, '') AS DESIGSECT
       FROM PR_EQUIP EQ
       LEFT JOIN MA_DEPT MD ON MD.CD_COMPANY = EQ.CD_COMPANY AND MD.CD_DEPT = EQ.CD_DEPT
       WHERE EQ.CD_COMPANY = @P_CD_COMPANY
       AND ISNULL(EQ.CD_DEPT, '') <> ''
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_SECT
(
       CODESECTI,
       DESIGSECT
)
SELECT * FROM A

-- B_ZONE (공장정보)
;WITH A AS
(
       SELECT DISTINCT
              PT.CD_PLANT AS NOZONE,
              PT.NM_PLANT AS LIBZONE
       FROM MA_PLANT PT
       WHERE PT.CD_COMPANY = @P_CD_COMPANY
) 
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_ZONE
(
       NOZONE,
       LIBZONE
)
SELECT * FROM A

-- B_ILOT (설비그룹정보)
;WITH A AS
(
       SELECT DISTINCT 
              (CASE WHEN ISNULL(EQ.CD_EQIP_GRP, '') = '' THEN 'ETC' ELSE EQ.CD_EQIP_GRP END) AS ILOT,
              '2' AS TYPEILOT, -- 2:유한, 4:무한 capacity (외주는 무한으로)
              (CASE WHEN ISNULL(MC.NM_SYSDEF, '') = '' THEN 'ETC' ELSE ISNULL(MC.NM_SYSDEF, '') END) AS LIBILOT
       FROM PR_EQUIP EQ
       LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = EQ.CD_COMPANY AND MC.CD_FIELD = 'PR_E000003' AND MC.CD_SYSDEF = EQ.CD_EQIP_GRP
       WHERE EQ.CD_COMPANY = @P_CD_COMPANY
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_ILOT
(
       ILOT,
       TYPEILOT,
       LIBILOT
)
SELECT * FROM A

-- B_MACH (설비 마스터)
;WITH A AS
(
       SELECT DISTINCT 
              EQ.CD_EQUIP AS MACHINE, -- 외주설비 등록 필요
              ISNULL(EQ.CD_DEPT, '') AS CODESECTI,
              '' AS NOCALHEBD, -- 캘린더코드 (개발 필요)
              EQ.NM_EQUIP AS LIBMACH,
              (CASE WHEN EQ.CD_EQIP_GRP = 'HEA' THEN 'BA' ELSE 'NR' END) AS MACH_MODEMACH,
              (CASE WHEN ISNULL(EQ.CD_EQIP_GRP, '') = '' THEN 'ETC' ELSE EQ.CD_EQIP_GRP END) AS ILOT,
              EQ.CD_PLANT AS NOZONE
       FROM PR_EQUIP EQ
       WHERE EQ.CD_COMPANY = @P_CD_COMPANY
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_MACH
(
       MACHINE,
       CODESECTI,
       NOCALHEBD,
       LIBMACH,
       MACH_MODEMACH,
       ILOT,
       NOZONE
)
SELECT * FROM A

-- B_OPE (공정코드(OP) 정보)
;WITH A AS
(
       SELECT DISTINCT
              (RT.CD_ITEM + '_' + RL.CD_OP + '_' + RL.CD_WCOP) AS OPE,
              (RT.CD_ITEM + '_' + OP.NM_OP) AS LIBOP,
              (CASE WHEN ISNULL(EQ.CD_EQIP_GRP, '') = '' THEN 'ETC' ELSE CD_EQIP_GRP END) AS ILOT,
              ISNULL(RQ.QT_LABOR_PLAN, 2) AS CODEBASET, -- 기준 수량
              1 AS DURREAL,
              'H' AS UNITE, -- 1시간
              0 AS DURPREP,
              0 AS THM,
              (CASE WHEN EQ.CD_EQIP_GRP = 'HEA' THEN '0' ELSE '1' END) AS INTERUPT, -- 공정 중간에 멈출수 있는지 여부
			  (CASE WHEN EQ.CD_EQIP_GRP = 'HEA' THEN ISNULL(RL.CD_USERDEF1, '') ELSE '' END) AS CODE_GPE -- 배치 그룹 코드
       FROM PR_ROUT RT
       JOIN PR_ROUT_L RL ON RL.CD_COMPANY = RT.CD_COMPANY AND RL.CD_PLANT = RT.CD_PLANT AND RL.NO_OPPATH = RT.NO_OPPATH AND RL.CD_ITEM = RT.CD_ITEM
       LEFT JOIN PR_WCOP OP ON OP.CD_COMPANY = RL.CD_COMPANY AND OP.CD_PLANT = RL.CD_PLANT AND OP.CD_WC = RL.CD_WC AND OP.CD_WCOP = RL.CD_WCOP
	   LEFT JOIN CZ_PR_ROUT_EQUIP RQ ON RQ.CD_COMPANY = RL.CD_COMPANY AND RQ.CD_PLANT = RL.CD_PLANT AND RQ.CD_ITEM = RL.CD_ITEM AND RQ.CD_OP = RL.CD_OP
       LEFT JOIN PR_EQUIP EQ ON EQ.CD_COMPANY = RL.CD_COMPANY AND EQ.CD_PLANT = RL.CD_PLANT AND EQ.CD_EQUIP = RL.CD_EQUIP
       WHERE RT.CD_COMPANY = @P_CD_COMPANY
       AND ISNULL(RL.YN_USE, 'N') = 'Y'
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_OPE
(
       OPE,
       LIBOP,
       ILOT,
       CODEBASET,
       DURREAL,
       UNITE,
       DURPREP,
       THM,
       INTERUPT,
	   CODE_GPE
)
SELECT * FROM A

-- B_CADE (OP별 설비그룹)
;WITH A AS
(
       SELECT DISTINCT
              (RT.CD_ITEM + '_' + RL.CD_OP + '_' + RL.CD_WCOP) AS OPE,
              (CASE WHEN ISNULL(EQ.CD_EQIP_GRP, '') = '' THEN 'ETC' ELSE EQ.CD_EQIP_GRP END) AS ILOT,
              (CASE WHEN ISNULL(RL.CD_EQUIP, '') = '' OR RL.CD_EQUIP = 'GI304' THEN '0000' ELSE RL.CD_EQUIP END) AS MACHINE,
              1 AS CADE_DURREAL,
              2 AS CADE_CODEBASET,
              'H' AS CADE_UNITE
       FROM PR_ROUT RT
       JOIN PR_ROUT_L RL ON RL.CD_COMPANY = RT.CD_COMPANY AND RL.CD_PLANT = RT.CD_PLANT AND RL.NO_OPPATH = RT.NO_OPPATH AND RL.CD_ITEM = RT.CD_ITEM
       LEFT JOIN PR_EQUIP EQ ON EQ.CD_COMPANY = RL.CD_COMPANY AND EQ.CD_PLANT = RL.CD_PLANT AND EQ.CD_EQUIP = RL.CD_EQUIP
       WHERE RT.CD_COMPANY = @P_CD_COMPANY
       AND ISNULL(RL.YN_USE, 'N') = 'Y'
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_CADE
(
    OPE,
    ILOT,
    MACHINE,
    CADE_DURREAL,
    CADE_CODEBASET,
    CADE_UNITE
)
SELECT * FROM A

-- B_GAMM (라우팅 헤더)
;WITH A AS
(
       SELECT DISTINCT
              (PR.CD_ITEM + '_100') AS NOMG,
              (PR.CD_ITEM + '_' + PR.DC_OPPATH) AS LIBGAM
       FROM PR_ROUT PR
       WHERE PR.CD_COMPANY = @P_CD_COMPANY
       AND EXISTS (SELECT 1 
                   FROM PR_ROUT_L RL 
                   WHERE RL.CD_COMPANY = PR.CD_COMPANY 
                   AND RL.CD_ITEM = PR.CD_ITEM 
                   AND RL.CD_PLANT = PR.CD_PLANT 
                   AND RL.NO_OPPATH = PR.NO_OPPATH 
                   AND ISNULL(RL.YN_USE, 'N') = 'Y')
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_GAMM
(
       NOMG,
       LIBGAM
)
SELECT * FROM A

-- B_PHAS (라우팅 단계)
;WITH A AS
(
       SELECT DISTINCT         
              (PR.CD_ITEM + '_100') AS NOMG,
              RL.CD_OP AS NOPHASE,
              (PR.CD_ITEM + '_' + RL.CD_OP + '_' + RL.CD_WCOP) AS OPE,
              OP.NM_OP AS LIBPHASE
       FROM PR_ROUT PR
       JOIN PR_ROUT_L RL ON RL.CD_COMPANY = PR.CD_COMPANY AND RL.CD_ITEM = PR.CD_ITEM AND RL.CD_PLANT = PR.CD_PLANT AND RL.NO_OPPATH = PR.NO_OPPATH
       LEFT JOIN PR_WCOP OP ON OP.CD_COMPANY = RL.CD_COMPANY AND OP.CD_PLANT = RL.CD_PLANT AND OP.CD_WC = RL.CD_WC AND OP.CD_WCOP = RL.CD_WCOP
       WHERE PR.CD_COMPANY = @P_CD_COMPANY
       AND ISNULL(RL.YN_USE, 'N') = 'Y'
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_PHAS
(
       NOMG,
       NOPHASE,
       OPE,
       LIBPHASE
)
SELECT * FROM A

-- B_PREC (공정 순서)
;WITH A AS
(
    SELECT 
		(RL.CD_ITEM + '_' + RL.NO_OPPATH) AS NOMG
	,	ROW_NUMBER() OVER (PARTITION BY RL.CD_ITEM ORDER BY RL.CD_OP) AS IDX
    ,	RL.CD_OP 
    FROM PR_ROUT_L RL
    WHERE RL.CD_COMPANY = @P_CD_COMPANY
    AND ISNULL(RL.YN_USE, 'N') = 'Y'
)
, B AS
(
	SELECT
		A.NOMG -- 라우팅 코드
	,	A.CD_OP AS NOPHASE -- 시작 단계 코드
	,	A.NOMG AS B_P_NOMG
	,	ISNULL(A1.CD_OP, '') AS B_P_NOPHASE -- 종료 단계 코드
	,	'T' AS TYPEPREC -- Precedence 타입
	FROM A
	LEFT JOIN A A1 ON A1.NOMG = A.NOMG AND A1.IDX = A.IDX + 1
	WHERE ISNULL(A1.CD_OP, '') <> ''
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_PREC
(
	NOMG
,	NOPHASE
,	B_P_NOMG
,	B_P_NOPHASE
,	TYPEPREC
)
SELECT * FROM B

-- B_TYPM (파라미터(셋업타임))
;WITH A AS
(
	SELECT 
		(RT.CD_ITEM + '_' + RL.CD_OP + '_' + RL.CD_WCOP) AS TYPEMAT -- 파라미터 코드
	,	(MI.NM_ITEM + '_' + OP.NM_OP)  AS LIBTYPMAT -- 파라미터 설명
	,	CONVERT(INT, (((CAST(SUBSTRING(RL.TM_SETUP, 1, 2) AS INT) * 60) + CAST(SUBSTRING(RL.TM_SETUP, 3, 2) AS INT) + (CASE WHEN CAST(SUBSTRING(RL.TM_SETUP, 5, 2) AS INT) >= 30 THEN 1 ELSE 0 END)) / 60.0) * 100) AS TEMPSCST -- 전환 시간
	,	'C' AS TYPM_UNITE -- 시간 단위 C(defalut, 1/100 시간) or D (1/1000 시간)
	FROM PR_ROUT RT
	JOIN PR_ROUT_L RL ON RL.CD_COMPANY = RT.CD_COMPANY AND RL.CD_PLANT = RT.CD_PLANT AND RL.NO_OPPATH = RT.NO_OPPATH AND RL.CD_ITEM = RT.CD_ITEM
	LEFT JOIN PR_WCOP OP ON OP.CD_COMPANY = RL.CD_COMPANY AND OP.CD_PLANT = RL.CD_PLANT AND OP.CD_WC = RL.CD_WC AND OP.CD_WCOP = RL.CD_WCOP
	LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = RT.CD_COMPANY AND MI.CD_ITEM = RT.CD_ITEM
	WHERE RT.CD_COMPANY = @P_CD_COMPANY
	AND ISNULL(RL.YN_USE, 'N') = 'Y'
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_TYPM
(
	TYPEMAT
,	LIBTYPMAT
,	TEMPSCST
,	TYPM_UNITE
)
SELECT * FROM A

-- B_ART (아이템 마스터)
;WITH A AS
(
       SELECT DISTINCT
              MI.CD_ITEM,
              MI.NM_ITEM,
              (CASE MI.CLS_ITEM WHEN '001' THEN 'MP'
                                WHEN '002' THEN 'MP'
                                WHEN '003' THEN 'PF'
                                WHEN '004' THEN 'SF' END) AS TYPEMATI,
              CONVERT(INT, ISNULL(IV.QT_INV, 0)) AS QTE_STOCK,
              2 AS ART_REGLE_NETTING,
              'NM' AS ART_ALIM_LOT
       FROM MA_PITEM MI
       LEFT JOIN (SELECT MY.CD_COMPANY, MY.CD_PLANT, MY.CD_SL, MY.CD_ITEM, 
       	    	    (SUM(MY.QT_GOOD_OPEN + MY.QT_REJECT_OPEN + MY.QT_INSP_OPEN + MY.QT_TRANS_OPEN) 
       	           + SUM(MY.QT_GOOD_GR + MY.QT_REJECT_GR + MY.QT_INSP_GR + MY.QT_TRANS_GR) 
       	           - SUM(MY.QT_GOOD_GI + MY.QT_REJECT_GI + MY.QT_INSP_GI + MY.QT_TRANS_GI)) AS QT_INV
       	    FROM MM_PINVN MY
       	    WHERE MY.P_YR = SUBSTRING(CONVERT(NVARCHAR(8), GETDATE(), 112), 1, 4)
       	    GROUP BY MY.CD_COMPANY, MY.CD_PLANT, MY.CD_SL, MY.CD_ITEM) IV
       ON IV.CD_COMPANY = MI.CD_COMPANY AND IV.CD_PLANT = MI.CD_PLANT AND IV.CD_ITEM = MI.CD_ITEM AND IV.CD_SL = MI.CD_SL
       WHERE MI.CD_COMPANY = @P_CD_COMPANY
       AND ISNULL(MI.YN_USE, 'N') = 'Y'
       AND MI.CLS_ITEM IN ('001', '003', '004', '002') -- 원자재, 완제품, 반제품, 부자제
       AND (MI.CLS_ITEM NOT IN ('003', '004') OR EXISTS (SELECT 1 
                                                         FROM PR_ROUT PR
                                                         WHERE PR.CD_COMPANY = MI.CD_COMPANY
                                                         AND PR.CD_PLANT = MI.CD_PLANT
                                                         AND PR.CD_ITEM = MI.CD_ITEM))
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_ART
(
    CODEARTIC,
    LIBARTIC,
    TYPEMATI,
    QTE_STOCK,
    ART_REGLE_NETTING,
    ART_ALIM_LOT
)
SELECT * FROM A

-- B_VER_ART (아이템 버전 정보)
;WITH A AS
(
       SELECT DISTINCT
              MI.CD_ITEM AS CODEARTIC,
              (CASE WHEN MI.CLS_ITEM IN ('001', '002') THEN '' ELSE '00' END) AS VER_ART,
              (CASE WHEN MI.CLS_ITEM IN ('001', '002') THEN '' ELSE MI.CD_ITEM + '_' + '100' END) AS NOMG
       FROM MA_PITEM MI
       WHERE MI.CD_COMPANY = @P_CD_COMPANY
       AND ISNULL(MI.YN_USE, 'N') = 'Y'
       AND MI.CLS_ITEM IN ('001', '003', '004', '002') -- 원자재, 완제품, 반제품, 부자제
       AND (MI.CLS_ITEM NOT IN ('003', '004') OR EXISTS (SELECT 1 
                                                         FROM PR_ROUT PR
                                                         WHERE PR.CD_COMPANY = MI.CD_COMPANY
                                                         AND PR.CD_PLANT = MI.CD_PLANT
                                                         AND PR.CD_ITEM = MI.CD_ITEM))
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_VER_ART
(
       CODEARTIC,
       VER_ART,
       NOMG
)
SELECT * FROM A

-- B_NOME (BOM 정보)
;WITH A AS
(
       SELECT DISTINCT
              RA.CD_ITEM AS B_V_CODEARTIC,
              '00' AS VER_ART,
              RA.CD_MATL AS CODEARTIC,
              (RA.CD_ITEM + '_100') AS NOMG,
              RA.CD_OP AS NOPHASE,
              1 AS QTEREF,
              CONVERT(NUMERIC(17, 3), RA.QT_ITEM_NET) AS QTENEC -- 공정경로BOM -> 실소요량
       FROM PR_ROUT_ASN RA
       WHERE RA.CD_COMPANY = @P_CD_COMPANY
       AND CONVERT(CHAR(8), GETDATE(), 112) BETWEEN RA.DT_START AND RA.DT_END
       AND NOT EXISTS (SELECT 1 
                   FROM MA_PITEM MI
                   WHERE MI.CD_COMPANY = RA.CD_COMPANY
                   AND MI.CD_PLANT = RA.CD_PLANT 
                   AND MI.CD_ITEM = RA.CD_ITEM)
       AND EXISTS (SELECT 1 
                   FROM MA_PITEM MI
                   WHERE MI.CD_COMPANY = RA.CD_COMPANY
                   AND MI.CD_PLANT = RA.CD_PLANT 
                   AND MI.CD_ITEM = RA.CD_MATL)
       AND EXISTS (SELECT 1 
                   FROM PR_ROUT_L RL
                   WHERE RL.CD_COMPANY = RA.CD_COMPANY
                   AND RL.CD_PLANT = RA.CD_PLANT
                   AND RL.CD_ITEM = RA.CD_ITEM
                   AND RL.CD_OP = RA.CD_OP)
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_NOME
(
       B_V_CODEARTIC,
       VER_ART,
       CODEARTIC,
       NOMG,
       NOPHASE,
       QTEREF,
       QTENEC
)
SELECT * FROM A

-- B_SER (SERIES 정보)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_SER (SERIE) VALUES ('SERIE')

-- B_OF (작업지시 정보)
;WITH A AS
(
       SELECT DISTINCT
              WO.NO_WO AS NOF,
              'SERIE' AS SERIE,
              NULL AS CODECLIEN,
              CONVERT(CHAR(10), CONVERT(DATETIME, WO.DT_REL), 101) AS DPLUSTOT,
              CONVERT(CHAR(10), CONVERT(DATETIME, WM.DT_DUEDATE), 101) AS FPLUSTARD, -- 매핑된 수주건의 가장 빠른 납기일자
              WO.CD_ITEM AS CODEARTIC,
              (WO.CD_ITEM + '_' + '100') AS NOMG,
              '00' AS VER_ART,
              CONVERT(INT, WO.QT_ITEM) AS QTE,
              'F' AS CODEGEST,
              '1' AS PRIORITE, -- 긴급건만 APS에서 우선순위 조정 예정
              'O' AS ETAOF -- WO를 OPEN 상태로 넘겨줘야 함
       FROM PR_WO WO
       LEFT JOIN (SELECT WM.CD_COMPANY, WM.CD_PLANT, WM.NO_WO,
                         MIN(SL.DT_DUEDATE) AS DT_DUEDATE
                  FROM CZ_PR_SA_SOL_PR_WO_MAPPING WM
                  LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = WM.CD_COMPANY AND SL.NO_SO = WM.NO_SO AND SL.SEQ_SO = WM.NO_LINE
                  GROUP BY WM.CD_COMPANY, WM.CD_PLANT, WM.NO_WO) WM
       ON WM.CD_COMPANY = WO.CD_COMPANY AND WM.CD_PLANT = WO.CD_PLANT AND WM.NO_WO = WO.NO_WO
       WHERE WO.CD_COMPANY = @P_CD_COMPANY
       AND ISNULL(WO.ST_WO, '') IN ('R', 'S') -- 진행, 시작 건만 조회
       AND ISNULL(WM.DT_DUEDATE, '') <> '' -- 수주매칭 안되어 있는 데이터는 어떻게 처리할지 협의 필요
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_OF
(
       NOF,
       SERIE,
       CODECLIEN,
       DPLUSTOT,
       FPLUSTARD,
       CODEARTIC,
       NOMG,
       VER_ART,
       QTE,
       CODEGEST,
       PRIORITE,
       ETATOF
)
SELECT * FROM A

-- B_COMMANDE_ENTETE (수주 헤더 정보)
;WITH A AS
(
       SELECT DISTINCT
              SH.NO_SO AS COMMANDE_ID,
              SH.NO_SO AS COMMANDE_LIBELLE,
              MP.LN_PARTNER AS COMMANDE_CLIENT,
              CONVERT(CHAR(10), CONVERT(DATETIME, SH.DT_SO), 101) AS COMMANDE_DATE
       FROM SA_SOH SH
       LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
       WHERE SH.CD_COMPANY = @P_CD_COMPANY
       AND EXISTS (SELECT 1 
                   FROM SA_SOL SL
                   WHERE SL.CD_COMPANY = SH.CD_COMPANY
                   AND SL.NO_SO = SH.NO_SO
                   AND SL.QT_SO > SL.QT_GIR -- 출고처리 되지 않은 건만 조회
                   AND EXISTS (SELECT 1 
                               FROM MA_PITEM MI
                               WHERE MI.CD_COMPANY = SL.CD_COMPANY
                               AND MI.CD_PLANT = SL.CD_PLANT
                               AND MI.CD_ITEM = SL.CD_ITEM))
)

INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_COMMANDE_ENTETE
(
    COMMANDE_ID,
    COMMANDE_LIBELLE,
    COMMANDE_CLIENT,
    COMMANDE_DATE
)
SELECT * FROM A

-- B_COMMANDE_LIGNE (수주 라인 정보)
;WITH A AS
(
       SELECT DISTINCT
              SH.NO_SO AS COMMANDE_ID,
              SL.SEQ_SO AS NUMERO_LIGNE,
              SL.CD_ITEM AS CODEARTIC,
              '00' AS VERSION_ART,
              CONVERT(INT, SL.QT_SO) AS QUANTITE,
              CONVERT(CHAR(10), CONVERT(DATETIME, SL.DT_DUEDATE), 101) AS DELAI,
              'SET' AS UTIL
       FROM SA_SOH SH
       LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
       WHERE SH.CD_COMPANY = @P_CD_COMPANY
       AND SL.QT_SO > SL.QT_GIR -- 출고처리 되지 않은 건만 조회
       AND EXISTS (SELECT 1 
                   FROM MA_PITEM MI
                   WHERE MI.CD_COMPANY = SL.CD_COMPANY
                   AND MI.CD_PLANT = SL.CD_PLANT
                   AND MI.CD_ITEM = SL.CD_ITEM)
)
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_COMMANDE_LIGNE
(
       COMMANDE_ID,
       NUMERO_LIGNE,
       CODEARTIC,
       VERSION_ART,
       QUANTITE,
       DELAI,
       UTIL
)
SELECT * FROM A

-- B_UTIL
INSERT INTO ORTEMS_MIDDLE_PS.dbo.B_UTIL (UTIL) VALUES ('SET')