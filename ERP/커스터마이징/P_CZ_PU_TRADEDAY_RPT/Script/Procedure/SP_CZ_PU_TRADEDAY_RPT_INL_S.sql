USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_TRADEDAY_RPT_INL_S]    Script Date: 2015-12-17 오전 9:09:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_PU_TRADEDAY_RPT_INL_S]      
(      
	 @P_CD_PLANT	 NVARCHAR(7),      
	 @P_CD_COMPANY   NVARCHAR(7),
	 @P_LANGUAGE     NVARCHAR(5) = 'KR',      
	 @P_CD_ITEM		 NVARCHAR(20),      
	 @P_MULTI_CD_SL  VARCHAR(8000) = '',      --@CD_SL   NVARCHAR(7),      
	 @P_DT_FROM		 NVARCHAR(8),      
	 @P_DT_TO		 NVARCHAR(8),      
	 @P_SL_CHECK	 NVARCHAR(1),
	 @P_CD_PJT		 NVARCHAR(20) = '',
	 @P_YN_SL_SUM	 NVARCHAR(1) = NULL,
	 @P_YN_PJT_SUM	 NVARCHAR(1) = NULL,
	 @P_CD_ITEMGRP   NVARCHAR(20) = '' 
)      
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

BEGIN 
SELECT IL.*,
	   (SELECT (CASE @P_LANGUAGE WHEN 'KR' THEN NM_SYSDEF
								 WHEN 'US' THEN NM_SYSDEF_E
								 WHEN 'JP' THEN NM_SYSDEF_JP
								 WHEN 'CH' THEN NM_SYSDEF_CH END)
		FROM MA_CODEDTL
		WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_FIELD = 'PU_C000016'
		AND CD_SYSDEF = IL.FG_TRANS) AS NM_FGTRANS,
	   MP.LN_PARTNER,
	   PH.NM_PROJECT,
	   EJ.NM_QTIOTP
FROM (SELECT CD_COMPANY,
			 CD_PLANT,
	         CD_ITEM,
	         DT_IO,
	         FG_TRANS,
	         CD_QTIOTP,
	         'Y' AS YN_AM,
	         0 AS UM_STOCK,
	         QT_IO AS QT_IN,
	         '' AS NO_IO,
			 NO_LINE,
	         CD_PARTNER,
	         UM AS UM_IN,
	         CD_PJT
FROM CZ_MM_QTIO_OLD
WHERE CD_COMPANY = @P_CD_COMPANY
AND CD_PLANT = @P_CD_PLANT
AND FG_PS = '1'      
AND DT_IO BETWEEN @P_DT_FROM AND @P_DT_TO
AND CD_ITEM = @P_CD_ITEM            
AND (@P_CD_PJT = '' OR CD_PJT = @P_CD_PJT)
UNION ALL
SELECT IL.CD_COMPANY,
	   IL.CD_PLANT,
	   IL.CD_ITEM,
	   IL.DT_IO,
	   IL.FG_TRANS,
	   IL.CD_QTIOTP,
	   IL.YN_AM, 
	   ISNULL(ID.UM_STOCK, 0) AS UM_STOCK,
	   (CASE WHEN IH.YN_RETURN = 'Y' THEN (0 - IL.QT_IO) ELSE IL.QT_IO END) AS QT_IN,  
	   IL.NO_IO,
	   IL.NO_IOLINE,
	   IL.CD_PARTNER,
	   IL.UM AS UM_IN,
	   IL.CD_PJT
FROM MM_QTIO IL
JOIN MM_QTIOH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO
LEFT JOIN CZ_MM_QTIO_DETAIL ID ON ID.CD_COMPANY = IL.CD_COMPANY AND ID.NO_IO = IL.NO_IO AND ID.NO_IOLINE = IL.NO_IOLINE
WHERE IL.CD_ITEM = @P_CD_ITEM
AND (@P_MULTI_CD_SL = '' OR IL.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_MULTI_CD_SL)))            
AND (@P_CD_PJT = '' OR IL.CD_PJT = @P_CD_PJT)
AND IL.FG_PS = '1'
AND IL.CD_PLANT =@P_CD_PLANT      
AND IL.CD_COMPANY =@P_CD_COMPANY      
AND IL.DT_IO BETWEEN @P_DT_FROM AND @P_DT_TO 
AND ((@P_SL_CHECK = 'Y' AND IL.FG_IO <> '022') OR (@P_SL_CHECK = 'N'))) IL
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = IL.CD_COMPANY AND MI.CD_PLANT = IL.CD_PLANT AND MI.CD_ITEM = IL.CD_ITEM
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = IL.CD_COMPANY AND MP.CD_PARTNER = IL.CD_PARTNER       
LEFT JOIN SA_PROJECTH PH ON PH.CD_COMPANY = IL.CD_COMPANY AND PH.NO_PROJECT = IL.CD_PJT
LEFT JOIN MM_EJTP EJ ON EJ.CD_COMPANY = IL.CD_COMPANY AND EJ.CD_QTIOTP = IL.CD_QTIOTP
WHERE (@P_CD_ITEMGRP = '' OR MI.GRP_ITEM = @P_CD_ITEMGRP)
ORDER BY IL.DT_IO DESC

END
GO