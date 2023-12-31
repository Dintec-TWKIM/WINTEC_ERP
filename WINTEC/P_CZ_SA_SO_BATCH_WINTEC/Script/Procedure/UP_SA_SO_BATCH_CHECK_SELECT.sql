USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SO_BATCH_CHECK_SELECT]    Script Date: 2019-11-11 오후 4:02:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_SA_SO_BATCH_CHECK_SELECT]
(
    @CD_COMPANY NVARCHAR(7),
    @P_FG_LANG     NVARCHAR(4) = NULL	--언어
)
AS
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG

declare
@V_SERVER_KEY		NVARCHAR(25)

SELECT  @V_SERVER_KEY = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG WHERE YN_UPGRADE = 'Y'  

SET NOCOUNT ON

--품목 마스터
SELECT  P.CD_PLANT, P.CD_ITEM, P.NM_ITEM, P.STND_ITEM, P.UNIT_SO, P.UNIT_IM, ISNULL(P.UNIT_SO_FACT,0) AS UNIT_SO_FACT,   
  P.LT_GI, ISNULL(P.CD_GISL, '') AS CD_GISL, P.TP_ITEM, RTRIM(P.FG_TAX_SA) AS FG_TAX_SA, VT.CD_FLAG1 AS RT_TAX_SA,
  P.CD_USERDEF1, P.CD_USERDEF2, P.CD_USERDEF3, P.CD_USERDEF4, P.CD_USERDEF5, P.CD_USERDEF6, P.CD_USERDEF7,
  P.CD_USERDEF8, P.CD_USERDEF9
  FROM  DZSN_MA_PITEM P
  LEFT OUTER JOIN DZSN_MA_CODEDTL VT ON P.CD_COMPANY = VT.CD_COMPANY AND VT.CD_FIELD = 'MA_B000040' AND P.FG_TAX_SA = VT.CD_SYSDEF
 WHERE  P.CD_COMPANY = @CD_COMPANY
 AND	(@V_SERVER_KEY NOT IN ('SINJINSM', 'DYAUTO' ))
 
 
--거래처 마스터
SELECT  CD_PARTNER, LN_PARTNER
  FROM DZSN_MA_PARTNER
 WHERE  CD_COMPANY = @CD_COMPANY

--창고 마스터
SELECT  CD_SL, NM_SL, CD_PLANT
  FROM DZSN_MA_SL
 WHERE  CD_COMPANY = @CD_COMPANY

--영업그룹 마스터
SELECT  CD_SALEGRP, NM_SALEGRP
  FROM  DZSN_MA_SALEGRP
 WHERE  CD_COMPANY = @CD_COMPANY

--사원 마스터
SELECT  NO_EMP, NM_KOR
  FROM  DZSN_MA_EMP
 WHERE  CD_COMPANY = @CD_COMPANY

--상품 마스터 --접수유형, 접수유형명, 상품코드, 상품코드명, 옵션코드, 옵션코드명, 기간시작, 기간끝, 공장, 품목, 품목명~
SELECT  ES.CD_SHOP,  ET.NM_SHOP, ES.CD_SPITEM, ES.NM_SPITEM, ES.CD_OPT,  ES.NM_OPT, ES.DT_FR, ES.DT_TO,
  ES.UM_SALE,  ES.QT,
  ES.CD_PLANT, ES.CD_ITEM, ES.NM_ITEM, MI.STND_ITEM, MI.UNIT_SO,   MI.UNIT_IM, ISNULL(MI.UNIT_SO_FACT,0) AS UNIT_SO_FACT,
  ET.CD_PARTNER, MP.LN_PARTNER
  FROM  DZSN_EC_SPITEM ES
  JOIN  DZSN_EC_SPTYPE ET ON ES.CD_COMPANY = ET.CD_COMPANY AND ES.CD_SHOP = ET.CD_SHOP
  LEFT  JOIN DZSN_MA_PITEM MI ON ES.CD_COMPANY = MI.CD_COMPANY AND ES.CD_PLANT = MI.CD_PLANT AND ES.CD_ITEM = MI.CD_ITEM
  LEFT  JOIN DZSN_MA_PARTNER MP ON ET.CD_COMPANY = MP.CD_COMPANY AND ET.CD_PARTNER = MP.CD_PARTNER
 WHERE  ES.CD_COMPANY = @CD_COMPANY

SET NOCOUNT OFF
GO

