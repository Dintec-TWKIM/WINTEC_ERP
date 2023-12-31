USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SO_SUBH_S]    Script Date: 2016-12-13 오전 11:37:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System : 영업
**  Sub System : 수주관리
**  Page  : 수주등록
**  Desc  : 수주등록 도움창 조회 헤더
**
**  Return Values
**
**  작    성    자 :
**  작    성    일 :
**  수    정    자 : 허성철
*********************************************
** Change History
*********************************************
*********************************************/
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SO_SUBH_S]
(
    @P_CD_COMPANY       NVARCHAR(7),
    @P_DT_SO_FROM       NVARCHAR(8),
    @P_DT_SO_TO         NVARCHAR(8),
    @P_CD_SALEGRP       NVARCHAR(7),
    @P_NO_EMP           NVARCHAR(10),
    @P_CD_PARTNER       NVARCHAR(20),
    @P_TP_SO            NVARCHAR(4),
    @P_NO_PROJECT       NVARCHAR(20),
    @P_NO_SO			NVARCHAR(20),
	@P_NO_EMP_LOGIN     NVARCHAR(10)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
  
SELECT SH.NO_SO,
	   SH.DT_SO,
	   ST.NM_SO,
	   MP.LN_PARTNER,
	   SG.NM_SALEGRP,
	   ME.NM_KOR,
       SH.DC_RMK,
	   SH.NO_PROJECT,
	   SP.NM_PROJECT,
	   SH.STA_SO,
	   MO.TP_SALEPRICE
FROM SA_SOH SH
LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = SH.CD_COMPANY AND QH.NO_FILE = SH.NO_SO
LEFT JOIN MA_SALEGRP SG ON SH.CD_COMPANY = SG.CD_COMPANY AND SH.CD_SALEGRP = SG.CD_SALEGRP
LEFT JOIN SA_TPSO ST ON SH.CD_COMPANY = ST.CD_COMPANY AND SH.TP_SO = ST.TP_SO
LEFT JOIN MA_PARTNER MP ON SH.CD_COMPANY = MP.CD_COMPANY AND SH.CD_PARTNER = MP.CD_PARTNER
LEFT JOIN MA_EMP ME ON SH.CD_COMPANY = ME.CD_COMPANY AND SH.NO_EMP = ME.NO_EMP
LEFT JOIN SA_PROJECTH SP ON SH.CD_COMPANY = SP.CD_COMPANY AND SH.NO_PROJECT = SP.NO_PROJECT
LEFT JOIN MA_SALEORG MO ON SG.CD_COMPANY = MO.CD_COMPANY AND SG.CD_SALEORG = MO.CD_SALEORG
LEFT JOIN FI_GWDOCU GW ON SH.CD_COMPANY = GW.CD_COMPANY AND CASE WHEN ISNULL(SH.TXT_USERDEF1, '') = '' THEN SH.NO_SO ELSE SH.TXT_USERDEF1 END = GW.NO_DOCU
LEFT JOIN MA_CODEDTL MC ON GW.CD_COMPANY = MC.CD_COMPANY AND MC.CD_FIELD = 'SA_B000060' AND GW.ST_STAT = MC.CD_SYSDEF
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND ISNULL(QH.CD_COMPANY, '') = ''
AND ((ISNULL(@P_NO_PROJECT, '') <> '') OR 
	 (ISNULL(@P_NO_SO, '') <> '') OR 
	 (SH.DT_SO BETWEEN @P_DT_SO_FROM AND @P_DT_SO_TO))
AND (ISNULL(@P_CD_SALEGRP, '') = '' OR SH.CD_SALEGRP = @P_CD_SALEGRP)
AND (ISNULL(@P_NO_EMP, '') = '' OR SH.NO_EMP = @P_NO_EMP)
AND (ISNULL(@P_CD_PARTNER, '') = '' OR SH.CD_PARTNER = @P_CD_PARTNER)
AND (ISNULL(@P_TP_SO, '') = '' OR SH.TP_SO = @P_TP_SO)
AND (ISNULL(@P_NO_PROJECT, '') = '' OR SH.NO_PROJECT LIKE @P_NO_PROJECT + '%')
AND (ISNULL(@P_NO_SO, '') = '' OR SH.NO_SO LIKE @P_NO_SO + '%')

GO