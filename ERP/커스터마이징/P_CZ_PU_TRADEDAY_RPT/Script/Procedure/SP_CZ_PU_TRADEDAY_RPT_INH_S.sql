USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_TRADEDAY_RPT_INH_S]    Script Date: 2015-12-17 오전 9:07:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PU_TRADEDAY_RPT_INH_S]        
(        
	 @P_CD_PLANT		NVARCHAR(7),        
	 @P_CD_COMPANY		NVARCHAR(7),
	 @P_LANGUAGE		NVARCHAR(5) = 'KR',        
	 @P_CD_ITEM_FROM	NVARCHAR(20),        
	 @P_CD_ITEM_TO		NVARCHAR(20),        
	 @P_MULTI_CD_SL		VARCHAR(8000), 
	 @P_DT_FROM			NVARCHAR(8),        
	 @P_DT_TO			NVARCHAR(8),        
	 @P_CLS_ITEM		NVARCHAR(4000) = '',
	 @P_ITEM_CHECK		NVARCHAR(1) = 'Y',
	 @P_SL_CHECK		NVARCHAR(1) = 'N', 
	 @P_YN_ZERO			NVARCHAR(1) = 'N',
	 @P_CD_PJT			NVARCHAR(20) = NULL,
	 @P_CD_ITEMGRP      NVARCHAR(20)
)
AS        

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
      
DECLARE @V_EXC_CLS_YN  NVARCHAR(3)

SELECT @V_EXC_CLS_YN = CD_EXC  
FROM MA_EXC 
WHERE CD_COMPANY = @P_CD_COMPANY 
AND EXC_TITLE = '재고현황_비수불계정포함여부';

SET @V_EXC_CLS_YN = (CASE WHEN ISNULL(@V_EXC_CLS_YN,'') = '' THEN '000' ELSE @V_EXC_CLS_YN END)

BEGIN
WITH TB_INV AS
(
	SELECT CD_COMPANY, CD_PLANT, CD_ITEM,
		   (SUM(QT_GOOD_OPEN + QT_REJECT_OPEN + QT_INSP_OPEN + QT_TRANS_OPEN) 
		   + SUM(QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR + QT_TRANS_GR) 
		   - SUM(QT_GOOD_GI + QT_REJECT_GI + QT_INSP_GI + QT_TRANS_GI)) AS QT_INV
	FROM MM_PINVN
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND CD_PLANT = @P_CD_PLANT
	AND P_YR = CONVERT(CHAR(4), GETDATE(), 112)
	GROUP BY CD_COMPANY, CD_PLANT, CD_ITEM
)

SELECT DISTINCT MI.CD_ITEM,
			    MI.NM_ITEM,
				MI.STND_ITEM,
				MI.UNIT_IM,
				MI.CLS_ITEM,
				ME.NM_KOR AS NM_MANAGER1,
				ME1.NM_KOR AS NM_MANAGER2,
				IG.NM_ITEMGRP,  --2011.05.16 품목군 추가(최규원) 
				(CASE WHEN MI.FG_SERNO = '001' THEN 'NO' 
											   ELSE (SELECT ISNULL((CASE @P_LANGUAGE WHEN 'KR' THEN NM_SYSDEF
																				     WHEN 'US' THEN NM_SYSDEF_E
																				     WHEN 'JP' THEN NM_SYSDEF_JP
																				     WHEN 'CH' THEN NM_SYSDEF_CH END), '')
													 FROM MA_CODEDTL
													 WHERE CD_COMPANY = MI.CD_COMPANY
													 AND CD_FIELD = 'MA_B000015'
													 AND CD_SYSDEF = MI.FG_SERNO) END) FG_SERNO, -- S/N,LOT관리 컬럼추가 2011.05.16 (최규원)   미관리를 영문화작업으로 인해 NO 바꿈
				MI.NO_DESIGN,
				MI.STND_DETAIL_ITEM,
				MI.MAT_ITEM,
				ISNULL(TI.QT_INV, 0) AS QT_INV,
				ISNULL(QL.DT_IO, '') AS DT_IO,
				MI.STAND_PRC
FROM MA_PITEM MI
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = MI.CD_COMPANY AND ME.NO_EMP = MI.NO_MANAGER1  
LEFT JOIN MA_EMP ME1 ON ME1.CD_COMPANY = MI.CD_COMPANY AND ME1.NO_EMP = MI.NO_MANAGER2    
LEFT JOIN MA_ITEMGRP IG ON  IG.CD_COMPANY = MI.CD_COMPANY AND IG.USE_YN = 'Y' AND  IG.CD_ITEMGRP = MI.GRP_ITEM             -- 2011.05.16 (최규원)
LEFT JOIN TB_INV TI ON TI.CD_COMPANY = MI.CD_COMPANY AND TI.CD_PLANT = MI.CD_PLANT AND TI.CD_ITEM = MI.CD_ITEM
JOIN (SELECT A.CD_COMPANY, A.CD_PLANT, A.CD_ITEM, MAX(A.DT_IO) AS DT_IO
      FROM (SELECT CD_COMPANY, CD_PLANT, CD_ITEM, DT_IO
			FROM CZ_MM_QTIO_OLD
			WHERE (ISNULL(@P_CD_PJT, '') = '' OR CD_PJT = @P_CD_PJT)
			UNION ALL
			SELECT IL.CD_COMPANY, IL.CD_PLANT, IL.CD_ITEM, IL.DT_IO
			FROM MM_QTIO IL
			WHERE IL.FG_PS = '1'
			AND (ISNULL(@P_CD_PJT, '') = '' OR IL.CD_PJT = @P_CD_PJT)) A
      GROUP BY A.CD_COMPANY, A.CD_PLANT, A.CD_ITEM) QL
ON QL.CD_COMPANY = MI.CD_COMPANY AND QL.CD_PLANT = MI.CD_PLANT AND QL.CD_ITEM = MI.CD_ITEM
WHERE MI.CD_COMPANY = @P_CD_COMPANY
AND MI.CD_PLANT = @P_CD_PLANT
AND (@P_YN_ZERO = 'N' OR (@P_YN_ZERO = 'Y' AND TI.QT_INV > 0))
AND (@P_CLS_ITEM = '' OR MI.CLS_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CLS_ITEM)))
AND (ISNULL(@P_CD_ITEM_FROM, '') = '' OR MI.CD_ITEM >= @P_CD_ITEM_FROM)        
AND (ISNULL(@P_CD_ITEM_TO, '') = '' OR MI.CD_ITEM <= @P_CD_ITEM_TO)     
AND ((@V_EXC_CLS_YN = '000' AND (MI.CLS_ITEM IN ('001', '002', '003', '004', '005', '009'))) OR(@V_EXC_CLS_YN = '100')) --20090506 수불계정만 해당하게   
AND ((@P_ITEM_CHECK = 'Y' AND MI.YN_USE = @P_ITEM_CHECK) OR (@P_ITEM_CHECK = 'N'))
AND (MI.GRP_ITEM = @P_CD_ITEMGRP OR @P_CD_ITEMGRP = '' OR @P_CD_ITEMGRP IS NULL)

END

GO

