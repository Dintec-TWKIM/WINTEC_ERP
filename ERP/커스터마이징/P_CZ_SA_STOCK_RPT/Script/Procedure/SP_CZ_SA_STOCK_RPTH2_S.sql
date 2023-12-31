USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_STOCK_RPTH2_S]    Script Date: 2016-07-28 오후 7:36:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_STOCK_RPTH2_S] 
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_TP_DATE			NVARCHAR(3),
	@P_DT_FROM			NVARCHAR(8),
	@P_DT_TO			NVARCHAR(8),
	@P_NO_SO			NVARCHAR(20),
	@P_NO_SO_PRE		NVARCHAR(4),
	@P_CD_PARTNER	    NVARCHAR(20),
	@P_NO_IMO			NVARCHAR(10),
	@P_CD_ITEM			NVARCHAR(20),
	@P_CD_ITEM_MULTI	NVARCHAR(1000),
	@P_CLS_ITEM			NVARCHAR(1000),
	@P_CLS_L			NVARCHAR(4),
	@P_CLS_M			NVARCHAR(4),
	@P_CLS_S			NVARCHAR(4)
)
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

WITH A AS
(
	SELECT SH.CD_COMPANY,
		   SL.CD_ITEM,
		   MI.NM_ITEM,
		   MI.STND_DETAIL_ITEM,
		   MI.STND_ITEM,
		   MI.MAT_ITEM,
		   MI.CLS_L,
		   MI.CLS_M,
		   MI.CLS_S,
		   MI.STAND_PRC,
		   ISNULL(SB.QT_STOCK, 0) AS QT_STOCK,
		   ISNULL(SB.QT_BOOK, 0) AS QT_BOOK,
		   ISNULL(SB.QT_HOLD, 0) AS QT_HOLD,
		   ISNULL(GL.QT_GIR_STOCK, 0) AS QT_GIR_STOCK,
		   ISNULL(GL.QT_IO, 0) AS QT_IO,
		   ISNULL(GL.QT_IV, 0) AS QT_IV,
		   (ISNULL(SB.QT_STOCK, 0) * ISNULL(SB.UM_KR, 0)) AS AM_STOCK,
		   (ISNULL(SB.QT_BOOK, 0) * ISNULL(SB.UM_KR, 0)) AS AM_BOOK,
		   (ISNULL(SB.QT_HOLD, 0) * ISNULL(SB.UM_KR, 0)) AS AM_HOLD,
		   (ISNULL(SB.QT_STOCK, 0) * ISNULL(SL.UM_KR_S, 0)) AS AM_SO,
		   ISNULL(GL.AM_GIR, 0) AS AM_GIR,
		   ISNULL(GL.AM_IO, 0) AS AM_IO,
		   ISNULL(GL.AM_IV, 0) AS AM_IV
	FROM SA_SOH SH
	JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
	JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
	LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_ITEM = SL.CD_ITEM
	LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO,
					  SUM(ISNULL(GL.QT_GIR_STOCK, 0)) AS QT_GIR_STOCK,
					  SUM(CASE WHEN ISNULL(OL.QT_IO, 0) > 0 THEN ISNULL(GL.QT_GIR_STOCK, 0) ELSE 0 END) AS QT_IO,
					  SUM(CASE WHEN ISNULL(IL.QT_CLS, 0) > 0 THEN ISNULL(GL.QT_GIR_STOCK, 0) ELSE 0 END) AS QT_IV,
					  SUM(ISNULL(GL.QT_GIR_STOCK, 0) * (ISNULL(GL.UM, 0) * ISNULL(GL.RT_EXCH, 1))) AS AM_GIR,
					  SUM((CASE WHEN ISNULL(OL.QT_IO, 0) > 0 THEN ISNULL(GL.QT_GIR_STOCK, 0) ELSE 0 END) * ISNULL(OL.UM, 0)) AS AM_IO,
					  SUM((CASE WHEN ISNULL(IL.QT_CLS, 0) > 0 THEN ISNULL(GL.QT_GIR_STOCK, 0) ELSE 0 END) * ISNULL(IL.UM_ITEM_CLS, 0)) AS AM_IV
			   FROM SA_GIRL GL
			   LEFT JOIN MM_QTIO OL ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
			   LEFT JOIN SA_IVL IL ON IL.CD_COMPANY = OL.CD_COMPANY AND IL.NO_IO = OL.NO_IO AND IL.NO_IOLINE = OL.NO_IOLINE AND IL.CD_ITEM NOT LIKE 'SD%'
			   WHERE GL.QT_GIR_STOCK IS NOT NULL
			   AND GL.QT_GIR_STOCK > 0
			   AND (@P_TP_DATE NOT IN ('003', '005') OR
				    (@P_TP_DATE = '003' AND EXISTS (SELECT 1 
												    FROM MM_QTIOH OH
												    WHERE OH.CD_COMPANY = OL.CD_COMPANY
												    AND OH.NO_IO = OL.NO_IO
												    AND OH.DT_IO BETWEEN @P_DT_FROM AND @P_DT_TO)) OR
				    (@P_TP_DATE = '005' AND EXISTS (SELECT 1 
												    FROM SA_IVH IH 
												    WHERE IH.CD_COMPANY = IL.CD_COMPANY
												    AND IH.NO_IV = IL.NO_IV
												    AND IH.DT_PROCESS BETWEEN @P_DT_FROM AND @P_DT_TO)))
			   GROUP BY GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO) GL
	ON GL.CD_COMPANY = SL.CD_COMPANY AND GL.NO_SO = SL.NO_SO AND GL.SEQ_SO = SL.SEQ_SO
	WHERE SH.CD_COMPANY = @P_CD_COMPANY
	AND (ISNULL(@P_NO_SO, '') = '' OR SH.NO_SO = @P_NO_SO)
	AND (ISNULL(@P_NO_SO_PRE, '') = '' OR SH.NO_SO LIKE @P_NO_SO_PRE + '%')
	AND (ISNULL(@P_CD_PARTNER, '') = '' OR SH.CD_PARTNER = @P_CD_PARTNER)
	AND (ISNULL(@P_NO_IMO, '') = '' OR SH.NO_IMO = @P_NO_IMO) 
	AND (ISNULL(@P_CD_ITEM, '') = '' OR SL.CD_ITEM LIKE @P_CD_ITEM + '%')
	AND (ISNULL(@P_CLS_L, '') = '' OR MI.CLS_L = @P_CLS_L)
	AND (ISNULL(@P_CLS_M, '') = '' OR MI.CLS_M = @P_CLS_M)
	AND (ISNULL(@P_CLS_S, '') = '' OR MI.CLS_S = @P_CLS_S)
	AND (ISNULL(@P_CD_ITEM_MULTI, '') = '' OR SL.CD_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_ITEM_MULTI)))
	AND (ISNULL(@P_CLS_ITEM, '') = '' OR MI.CLS_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CLS_ITEM)))
	AND ((@P_TP_DATE = '001' AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO) OR 
		 (@P_TP_DATE = '002' AND SH.DT_CONTRACT BETWEEN @P_DT_FROM AND @P_DT_TO) OR
		 (@P_TP_DATE = '004' AND LEFT(SB.DTS_INSERT, 8) BETWEEN @P_DT_FROM AND @P_DT_TO) OR
		 (@P_TP_DATE IN ('003', '005') AND GL.CD_COMPANY IS NOT NULL))
	AND EXISTS (SELECT 1 
			    FROM MA_CODEDTL MC
			    WHERE MC.CD_COMPANY = SH.CD_COMPANY
			    AND MC.CD_FIELD = 'CZ_SA00023'
			    AND MC.CD_SYSDEF NOT IN ('00', 'ZZ', 'T-')
				AND SH.NO_SO LIKE MC.CD_SYSDEF + '%')
),
B AS
(
	SELECT A.CD_COMPANY, 
		   A.CD_ITEM,
		   MAX(A.NM_ITEM) AS NM_ITEM,
		   MAX(A.STND_DETAIL_ITEM) AS STND_DETAIL_ITEM,
		   MAX(A.STND_ITEM) AS STND_ITEM,
		   MAX(A.MAT_ITEM) AS MAT_ITEM,
		   MAX(A.CLS_L) AS CLS_L,
		   MAX(A.CLS_M) AS CLS_M,
		   MAX(A.CLS_S) AS CLS_S,
		   MAX(A.STAND_PRC) AS STAND_PRC,
		   SUM(A.QT_STOCK) AS QT_STOCK,
		   SUM(A.QT_BOOK) AS QT_BOOK,
		   SUM(A.QT_HOLD) AS QT_HOLD,
		   SUM(A.QT_GIR_STOCK) AS QT_GIR_STOCK,
		   SUM(A.QT_IO) AS QT_IO,
		   SUM(A.QT_IV) AS QT_IV,
		   SUM(A.AM_STOCK) AS AM_STOCK,
		   SUM(A.AM_BOOK) AS AM_BOOK,
		   SUM(A.AM_HOLD) AS AM_HOLD,
		   SUM(A.AM_SO) AS AM_SO,
		   SUM(A.AM_GIR) AS AM_GIR,
		   SUM(A.AM_IO) AS AM_IO,
		   SUM(A.AM_IV) AS AM_IV
	FROM A
	GROUP BY A.CD_COMPANY, A.CD_ITEM
)
SELECT B.CD_ITEM,
	   B.QT_STOCK,
	   B.QT_BOOK,
	   B.QT_HOLD,
	   B.QT_GIR_STOCK,
	   B.QT_IO,
	   B.QT_IV,
	   B.AM_STOCK,
	   B.AM_BOOK,
	   B.AM_HOLD,
	   B.AM_SO,
	   B.AM_GIR,
	   B.AM_IO,
	   B.AM_IV,
	   B.NM_ITEM,
	   B.STND_DETAIL_ITEM,
	   B.STND_ITEM,
	   B.MAT_ITEM,
	   B.CLS_L,
	   B.CLS_M,
	   B.CLS_S,
	   B.STAND_PRC,
	   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = B.CD_COMPANY AND CD_FIELD = 'MA_B000030' AND CD_SYSDEF = B.CLS_L) AS NM_CLS_L,
	   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = B.CD_COMPANY AND CD_FIELD = 'MA_B000031' AND CD_SYSDEF = B.CLS_M) AS NM_CLS_M,
	   (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = B.CD_COMPANY AND CD_FIELD = 'MA_B000032' AND CD_SYSDEF = B.CLS_S) AS NM_CLS_S
FROM B

GO