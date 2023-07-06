USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_TR_EXINV_S]    Script Date: 2015-04-27 오후 8:14:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************
**  System : 무역
**  Sub System : 수출
**  Page  : 송장등록
**  Desc  : 송장등록 초기화 및 조회
**  수정자 : 허성철
************************************************************************/
ALTER PROCEDURE [NEOE].[SP_CZ_TR_EXINV_S]
(
    @P_CD_COMPANY   NVARCHAR(7),
    @P_NO_INV       NVARCHAR(20),
	@P_NO_HST		NUMERIC(5, 0)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

IF ISNULL(@P_NO_HST, 0) = 0
BEGIN
	SELECT TH.NO_INV,           --송장번호
	       TH.DT_BALLOT,        --발행일자
	       TH.CD_BIZAREA,       --사업장
		   BA.NM_BIZAREA,
	       TH.CD_SALEGRP,       --영업그룹
		   SG.NM_SALEGRP,
	       TH.NO_EMP AS NO_EMP_INV, --담당자
		   ME.NM_KOR AS NM_KOR_INV,
	       TH.FG_LC,            --LC구분
	       TH.CD_PARTNER,       --매출처
	       TH.CD_EXCH,          --환종
	       TH.AM_EX,            --외화금액
	       TH.DT_LOADING,       --선적예정일
	       TH.CD_ORIGIN,        --원산지
		   MC1.NM_SYSDEF_E AS NM_ORIGIN,
	       TH.CD_AGENT,         --대행자
		   MP2.LN_PARTNER AS NM_AGENT,
	       TH.CD_EXPORT,        --수출자
	       TH.CD_PRODUCT,       --제조자
		   MP3.LN_PARTNER AS NM_PRODUCT,
	       TH.SHIP_CORP,        --선적회사
		   MP1.LN_PARTNER AS NM_SHIP_CORP,
	       TH.NM_VESSEL,        --VESSEL명
	       TH.COND_TRANS,       --인도조건
	       TH.TP_TRANSPORT,     --운송형태
	       TH.TP_TRANS,         --운송방법
	       TH.TP_PACKING,       --포장형태
	       TH.CD_WEIGHT,        --중량단위
	       TH.GROSS_WEIGHT,     --총중량
	       TH.NET_WEIGHT,       --순중량
	       TH.PORT_LOADING,     --선적지
	       TH.PORT_ARRIVER,     --도착지
	       TH.DESTINATION,      --목적지
	       TH.NO_SCT,           --시작C/T번호
	       TH.NO_ECT,           --종료C/T번호
	       TH.CD_NOTIFY, --착하통지서
	       TH.DT_TO,            --통관예정일
	       TH.NO_LC,            --관련LC번호
	       TH.NO_SO,            --관련수주번호
	       TH.REMARK1,
	       TH.REMARK2,
	       TH.REMARK3,
	       TH.REMARK4,
	       TH.REMARK5,
	       TH.DTS_INSERT,       --등록일
	       TH.ID_INSERT,        --등록자
	       TH.DTS_UPDATE,       --수정자
	       TH.ID_UPDATE,        --수정일
	     --TH.NO_TO,            --통관번호
	     --TH.NO_BL,            --선적번호
	       TH.NM_NOTIFY,        --착하통지처
	       TH.ADDR1_NOTIFY,     --통지처주소1
	       TH.ADDR2_NOTIFY,     --통지처주소2
	       TH.CD_CONSIGNEE,     --수하인
	       TH.NM_CONSIGNEE,     --수하인명
	       TH.ADDR1_CONSIGNEE,  --수하인주소1
	       TH.ADDR2_CONSIGNEE,  --수하인주소2
	       TH.REMARK,
	       TH.NM_PARTNER,
	       TH.ADDR1_PARTNER,
	       TH.ADDR2_PARTNER,
	       TH.NM_EXPORT,
	       TH.ADDR1_EXPORT,
	       TH.ADDR2_EXPORT,
	       TH.COND_PRICE,
	       TH.DESCRIPTION,  --20090427 추가
	       ISNULL(TH.GROSS_VOLUME, 0) AS GROSS_VOLUME,   --총부피(20100226추가 by SJH)
	       TH.FG_FREIGHT, AM_FREIGHT,
	       TH.YN_RETURN,
	       TH.DT_SAILING_ON,
	       TH.TXT_REMARK2,
	       TH.CD_BANK,
	       MP.LN_PARTNER AS NM_BANK,
	       TH.COND_PAY,
		   TH.ARRIVER_COUNTRY,
		   MC.NM_SYSDEF_E AS NM_ARRIVER_COUNTRY,
		   TH.YN_INSURANCE
	FROM CZ_TR_INVH TH
	LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = TH.CD_COMPANY AND ME.NO_EMP = TH.NO_EMP
	LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = TH.CD_COMPANY AND SG.CD_SALEGRP = TH.CD_SALEGRP
	LEFT JOIN MA_BIZAREA BA ON BA.CD_COMPANY = TH.CD_COMPANY AND BA.CD_BIZAREA = TH.CD_BIZAREA
	LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = TH.CD_COMPANY AND MP.CD_PARTNER = TH.CD_BANK
	LEFT JOIN MA_PARTNER MP1 ON MP1.CD_COMPANY = TH.CD_COMPANY AND MP1.CD_PARTNER = TH.SHIP_CORP
	LEFT JOIN MA_PARTNER MP2 ON MP2.CD_COMPANY = TH.CD_COMPANY AND MP2.CD_PARTNER = TH.CD_AGENT
	LEFT JOIN MA_PARTNER MP3 ON MP3.CD_COMPANY = TH.CD_COMPANY AND MP3.CD_PARTNER = TH.CD_PRODUCT
	LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = TH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000020' AND MC.CD_SYSDEF = TH.ARRIVER_COUNTRY
	LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = TH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000020' AND MC1.CD_SYSDEF = TH.CD_ORIGIN
	WHERE TH.CD_COMPANY = @P_CD_COMPANY
	AND TH.NO_INV = @P_NO_INV
END
ELSE
BEGIN
	SELECT TH.NO_INV,           --송장번호
	       TH.DT_BALLOT,        --발행일자
	       TH.CD_BIZAREA,       --사업장
		   BA.NM_BIZAREA,
	       TH.CD_SALEGRP,       --영업그룹
		   SG.NM_SALEGRP,
	       TH.NO_EMP AS NO_EMP_INV, --담당자
		   ME.NM_KOR AS NM_KOR_INV,
	       TH.FG_LC,            --LC구분
	       TH.CD_PARTNER,       --매출처
	       TH.CD_EXCH,          --환종
	       TH.AM_EX,            --외화금액
	       TH.DT_LOADING,       --선적예정일
	       TH.CD_ORIGIN,        --원산지
		   MC1.NM_SYSDEF_E AS NM_ORIGIN,
	       TH.CD_EXPORT,        --수출자
	       TH.CD_PRODUCT,       --제조자
		   MP3.LN_PARTNER AS NM_PRODUCT,
	       TH.TP_TRANSPORT,     --운송형태
	       TH.TP_TRANS,         --운송방법
	       TH.PORT_LOADING,     --선적지
	       TH.PORT_ARRIVER,     --도착지
	       TH.CD_NOTIFY, --착하통지서
	       TH.DT_TO,            --통관예정일
	       TH.REMARK1,
	       TH.REMARK2,
	       TH.REMARK3,
		   TH.REMARK4,
		   TH.REMARK5,
	       TH.DTS_INSERT,       --등록일
	       TH.ID_INSERT,        --등록자
	       TH.DTS_UPDATE,       --수정자
	       TH.ID_UPDATE,        --수정일
	     --TH.NO_TO,            --통관번호
	     --TH.NO_BL,            --선적번호
	       TH.NM_NOTIFY,        --착하통지처
	       TH.ADDR1_NOTIFY,     --통지처주소1
	       TH.ADDR2_NOTIFY,     --통지처주소2
	       TH.CD_CONSIGNEE,     --수하인
	       TH.NM_CONSIGNEE,     --수하인명
	       TH.ADDR1_CONSIGNEE,  --수하인주소1
	       TH.ADDR2_CONSIGNEE,  --수하인주소2
	       TH.REMARK,
	       TH.NM_PARTNER,
	       TH.ADDR1_PARTNER,
	       TH.ADDR2_PARTNER,
	       TH.NM_EXPORT,
	       TH.ADDR1_EXPORT,
	       TH.ADDR2_EXPORT,
	       TH.COND_PRICE,
	       TH.DESCRIPTION,  --20090427 추가
		   TH.DT_SAILING_ON,
	       TH.YN_RETURN,
	       TH.COND_PAY,
		   TH.ARRIVER_COUNTRY,
		   MC.NM_SYSDEF_E AS NM_ARRIVER_COUNTRY,
		   TH.YN_INSURANCE
	FROM CZ_TR_INVH_LOG TH
	LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = TH.CD_COMPANY AND ME.NO_EMP = TH.NO_EMP
	LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = TH.CD_COMPANY AND SG.CD_SALEGRP = TH.CD_SALEGRP
	LEFT JOIN MA_BIZAREA BA ON BA.CD_COMPANY = TH.CD_COMPANY AND BA.CD_BIZAREA = TH.CD_BIZAREA
	LEFT JOIN MA_PARTNER MP3 ON MP3.CD_COMPANY = TH.CD_COMPANY AND MP3.CD_PARTNER = TH.CD_PRODUCT
	LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = TH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000020' AND MC.CD_SYSDEF = TH.ARRIVER_COUNTRY
	LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = TH.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000020' AND MC1.CD_SYSDEF = TH.CD_ORIGIN
	WHERE TH.CD_COMPANY = @P_CD_COMPANY
	AND TH.NO_INV = @P_NO_INV
	AND TH.NO_HST = @P_NO_HST
END

   
GO