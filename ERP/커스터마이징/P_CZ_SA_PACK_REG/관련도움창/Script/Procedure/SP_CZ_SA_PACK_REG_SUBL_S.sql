USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_PACK_REG_SUBL_S]    Script Date: 2015-09-10 오전 10:49:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_PACK_REG_SUBL_S]
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_NO_GIR	NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT GL.NO_GIR,
	   GL.SEQ_GIR,
	   QL.NO_FILE,
	   QL.NO_LINE AS NO_QTLINE,
	   QL.NO_DSP,
	   GL.CD_ITEM,
	   MP.NM_ITEM,
	   QL.CD_ITEM_PARTNER,
	   QL.NM_ITEM_PARTNER,
	   (ISNULL(GL.QT_GIR, 0) - ISNULL(PL.QT_PACK, 0)) AS QT_GIR,
	   (ISNULL(GL.QT_GIR, 0) - ISNULL(PL.QT_PACK, 0)) AS QT_PACK
FROM (SELECT GL.CD_COMPANY,
	  		 GL.NO_GIR,
			 GL.SEQ_GIR,
	  		 GL.NO_SO,
			 GL.SEQ_SO,
	  		 GL.CD_ITEM,
	  		 ISNULL(GL.QT_GIR, 0) AS QT_GIR
	  FROM SA_GIRL GL
	  WHERE GL.CD_COMPANY = @P_CD_COMPANY
	  AND GL.NO_GIR = @P_NO_GIR
	  UNION ALL
	  SELECT PL.CD_COMPANY,
	  		 PL.NO_GIR,
			 PL.SEQ_GIR,
	  		 PL.NO_SO,
			 PL.SEQ_SO,
	  		 PL.CD_ITEM,
	  		 ISNULL(PL.QT_GIR, 0) AS QT_GIR
	  FROM CZ_SA_GIRL_PACK PL
	  WHERE PL.CD_COMPANY = @P_CD_COMPANY
	  AND PL.NO_GIR = @P_NO_GIR) GL
LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = GL.CD_COMPANY AND QL.NO_FILE = GL.NO_SO AND QL.NO_LINE = GL.SEQ_SO
LEFT JOIN MA_PITEM MP ON MP.CD_COMPANY = GL.CD_COMPANY AND MP.CD_ITEM = GL.CD_ITEM
LEFT JOIN (SELECT CD_COMPANY, NO_GIR, SEQ_GIR,
				  SUM(QT_PACK) AS QT_PACK
		   FROM CZ_SA_PACKL
		   GROUP BY CD_COMPANY, NO_GIR, SEQ_GIR) PL 
ON PL.CD_COMPANY = GL.CD_COMPANY AND PL.NO_GIR = GL.NO_GIR AND PL.SEQ_GIR = GL.SEQ_GIR
WHERE ISNULL(GL.QT_GIR, 0) <> ISNULL(PL.QT_PACK, 0)
AND GL.CD_ITEM NOT LIKE 'SD%'

GO