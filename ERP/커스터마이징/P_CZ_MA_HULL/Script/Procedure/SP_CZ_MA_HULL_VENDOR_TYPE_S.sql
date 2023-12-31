USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_HULL_VENDOR_TYPE_S]    Script Date: 2016-05-31 오전 9:15:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_MA_HULL_VENDOR_TYPE_S]
(
	@P_CD_COMPANY      NVARCHAR(7),
	@P_NO_IMO          NVARCHAR(10),
	@P_CD_VENDOR	   NVARCHAR(20)
) 
AS
 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT ST.NO_IMO,
	   ST.CD_VENDOR,
	   ST.NO_TYPE,
	   ST.CLS_L,
	   MC.NM_SYSDEF AS NM_CLS_L,
	   ST.CLS_M,
	   MC1.NM_SYSDEF AS NM_CLS_M,
	   ST.CLS_S,
	   MC2.NM_SYSDEF AS NM_CLS_S,
	   MF.FILE_PATH_MNG,
	   ST.DC_RMK1,
	   ST.DC_RMK2,
	   ST.DC_RMK3,
	   ST.DC_RMK4,
	   ST.DC_RMK5,
	   ST.DC_RMK6,
	   ST.DC_RMK7,
	   ST.DC_RMK8,
	   ST.DC_RMK9,
	   ST.DC_RMK10,
	   ST.DC_RMK11,
	   ST.DC_RMK12,
	   ST.DC_RMK13,
	   ST.DC_RMK14,
	   ST.DC_RMK15,
	   ST.DC_RMK
FROM CZ_MA_HULL_VENDOR_TYPE ST
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = @P_CD_COMPANY AND MC.CD_FIELD = 'MA_B000030' AND MC.CD_SYSDEF = ST.CLS_L
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = @P_CD_COMPANY AND MC1.CD_FIELD = 'MA_B000031' AND MC1.CD_SYSDEF = ST.CLS_M
LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = @P_CD_COMPANY AND MC2.CD_FIELD = 'MA_B000032' AND MC2.CD_SYSDEF = ST.CLS_S
LEFT JOIN (SELECT CD_COMPANY, CD_MODULE, ID_MENU, CD_FILE,
				  CONVERT(VARCHAR, COUNT(1)) AS FILE_PATH_MNG
		   FROM MA_FILEINFO
		   GROUP BY CD_COMPANY, CD_MODULE, ID_MENU, CD_FILE) MF 
ON MF.CD_COMPANY = 'K100' AND MF.CD_MODULE = 'MA' AND MF.ID_MENU = 'P_CZ_MA_HULL' AND MF.CD_FILE = ST.NO_IMO + '_' + ST.CD_VENDOR + '_' + CONVERT(NVARCHAR, ST.NO_TYPE)
WHERE ST.NO_IMO = @P_NO_IMO
AND (ISNULL(@P_CD_VENDOR, '') = '' OR ST.CD_VENDOR = @P_CD_VENDOR)

GO

