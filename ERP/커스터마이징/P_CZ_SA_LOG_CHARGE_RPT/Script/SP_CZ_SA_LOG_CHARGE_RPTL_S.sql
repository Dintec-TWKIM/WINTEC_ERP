USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_LOG_CHARGE_RPTL_S]    Script Date: 2016-02-12 오후 1:40:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_SA_LOG_CHARGE_RPTL_S]  
(
    @P_CD_COMPANY		NVARCHAR(7),
	@P_NO_GIR			NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT GL.CD_COMPANY,
	   GL.NO_GIR,
	   GL.NO_SO,
	   SH.DT_SO,
	   ME.NM_KOR AS NM_EMP,
	   MC.NM_SYSDEF AS NM_PACKING,
	   GL.AM_SO,
	   GL.AM_PO,
	   GL.AM_STOCK,
	   GL.AM_PROFIT,
	   MC.CD_FLAG1 AS YN_BILL,
	   SH.DT_USERDEF1 AS DT_WARNING
FROM (SELECT GL.CD_COMPANY,
			 GL.NO_GIR,
			 GL.NO_SO,
			 SUM(SL.AM_SO) AS AM_SO,
			 SUM(SL.AM_PO) AS AM_PO,
			 SUM(SL.AM_STOCK) AS AM_STOCK,
			 SUM(ISNULL(SL.AM_SO, 0) - (ISNULL(SL.AM_PO, 0) + ISNULL(SL.AM_STOCK, 0))) AS AM_PROFIT
	  FROM SA_GIRL GL
	  JOIN (SELECT SL.CD_COMPANY, SL.NO_SO, SL.SEQ_SO,
	   			   SL.AM_WONAMT AS AM_SO,
	   			   PL.AM_PO,
	   			   (SB.UM_KR * SB.QT_STOCK) AS AM_STOCK
	        FROM SA_SOL SL
	   		LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
	   		LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
	   		 			      SUM(PL.AM) AS AM_PO
	   		 	       FROM PU_POL PL
	   		 	       WHERE PL.CD_ITEM NOT LIKE 'SD%'
	   		 	       GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE) PL
	   	    ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
	   	    WHERE SL.CD_ITEM NOT LIKE 'SD%') SL
	  ON SL.CD_COMPANY = GL.CD_COMPANY AND SL.NO_SO = GL.NO_SO AND SL.SEQ_SO = GL.SEQ_SO
	  WHERE GL.CD_COMPANY = @P_CD_COMPANY
	  AND GL.NO_GIR = @P_NO_GIR
	  GROUP BY GL.CD_COMPANY, GL.NO_GIR, GL.NO_SO) GL
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = GL.CD_COMPANY AND SH.NO_SO = GL.NO_SO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = SH.CD_COMPANY AND MC.CD_FIELD = 'TR_IM00011' AND MC.CD_SYSDEF = SH.TP_PACKING


GO