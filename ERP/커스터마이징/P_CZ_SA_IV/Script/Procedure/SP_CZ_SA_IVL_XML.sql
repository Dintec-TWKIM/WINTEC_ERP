USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_IVL_I]    Script Date: 2015-12-22 오전 9:02:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************      
**  System : 영업      
**  Sub System : 매출관리      
**  Page  : 매출등록      
**  Desc  : 매출등록 라인 등록      
**      
**  Return Values      
**      
**  작    성    자  :       
**  작    성    일 :       
**  수    정    자     : 허성철      
*********************************************      
** Change History      
*********************************************      
*********************************************/      
ALTER PROCEDURE [NEOE].[SP_CZ_SA_IVL_XML]
(      
	@P_CD_COMPANY	NVARCHAR(7),
	@P_XML			XML, 
    @P_DT_TAX       NCHAR(8),      
	@P_ID_USER		NVARCHAR(14), 
	@DOC			INT = NULL
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML

SELECT * INTO #XML
FROM OPENXML (@DOC, '/XML/I', 2) 
WITH (NO_IV            NVARCHAR(20),      
	  NO_LINE          NUMERIC(5,0),      
	  CD_PLANT         NVARCHAR(7),      
	  NO_IO            NVARCHAR(20),      
	  NO_IOLINE        NUMERIC(5,0),      
	  CD_ITEM          NVARCHAR(20),      
	  CD_SALEGRP       NVARCHAR(7),      
	  CD_CC            NVARCHAR(12),      
	  QT_GI_CLS        NUMERIC(17,4),      
	  QT_CLS           NUMERIC(17,4),      
	  UM_ITEM_CLS      NUMERIC(15,4),      
	  AM_CLS           NUMERIC(17,4),      
	  VAT              NUMERIC(17,4),      
	  NO_EMP           NVARCHAR(10),      
	  CD_PJT           NVARCHAR(20),      
	  TP_IV            NCHAR(3),      
	  NO_SO            NVARCHAR(20),      
	  NO_SOLINE        NUMERIC(5,0),      
	  CD_EXCH          NVARCHAR(3),      
	  RT_EXCH          NUMERIC(11,4),      
	  YN_RETURN        NCHAR(1),      
	  UM_EX_CLS        NUMERIC(15,4),      
	  AM_EX_CLS        NUMERIC(17,4),      
	  FG_TRANS         NCHAR(3),
	  DC_RMK           NVARCHAR(100),
	  CD_PJTLINE	   NUMERIC(5, 0),
	  CD_MNGD1         NVARCHAR(50),
	  CD_MNGD2		   NVARCHAR(50),
	  CD_MNGD3		   NVARCHAR(50),
	  CD_MNGD4		   NVARCHAR(50),
	  DT_LOADING       NVARCHAR(8),
	  TP_UM_TAX        NVARCHAR(3),
	  UMVAT_IV         NUMERIC(15,4),
	  YN_AUTO_CHARGE   NVARCHAR(1))

EXEC SP_XML_REMOVEDOCUMENT @DOC

INSERT INTO SA_IVL
(
	CD_COMPANY,                      
	NO_IV,                      
	NO_LINE,                      
	CD_PLANT,                      
	NO_IO,                      
	NO_IOLINE,              
	CD_ITEM,                      
	CD_SALEGRP,                      
	CD_CC,                      
	DT_TAX,                      
	QT_GI_CLS,                      
	QT_CLS,              
	UM_ITEM_CLS,                      
	AM_CLS,                      
	VAT,                      
	NO_EMP,                      
	CD_PJT,                      
	TP_IV,              
	NO_SO,                      
	NO_SOLINE,                      
	CD_EXCH,                      
	RT_EXCH,                      
	YN_RETURN,                      
	UM_EX_CLS,              
	AM_EX_CLS,              
	FG_TRANS,
	DC_RMK,              
	CD_PJTLINE,
	UD_NM_01, 
	UD_NM_02,	
	UD_NM_03,	
	UD_NM_04,
	DT_BL,
	TP_UM_TAX,
	UMVAT_IV,
	CD_USERDEF1,
	NO_IV_ORG,
	ID_INSERT,
	DTS_INSERT
)
SELECT @P_CD_COMPANY,
	   IL.NO_IV,                      
	   IL.NO_LINE,                      
	   IL.CD_PLANT,                      
	   IL.NO_IO,                      
	   IL.NO_IOLINE,              
	   IL.CD_ITEM,                      
	   IL.CD_SALEGRP,                      
	   IL.CD_CC,                      
	   @P_DT_TAX,                      
	   IL.QT_GI_CLS,                      
	   IL.QT_CLS,              
	   IL.UM_ITEM_CLS,                      
	   IL.AM_CLS,                      
	   IL.VAT,                      
	   IL.NO_EMP,                      
	   IL.CD_PJT,                      
	   IL.TP_IV,              
	   IL.NO_SO,                      
	   IL.NO_SOLINE,                      
	   IL.CD_EXCH,                      
	   IL.RT_EXCH,                      
	   IL.YN_RETURN,                      
	   IL.UM_EX_CLS,              
	   IL.AM_EX_CLS,              
	   IL.FG_TRANS,
	   IL.DC_RMK,              
	   IL.CD_PJTLINE,
	   IL.CD_MNGD1, 
	   IL.CD_MNGD2,	
	   IL.CD_MNGD3,	
	   IL.CD_MNGD4,
	   IL.DT_LOADING,
	   IL.TP_UM_TAX,
	   IL.UMVAT_IV,
	   IL.YN_AUTO_CHARGE,
	   (CASE WHEN IL1.CNT_SO > 1 THEN IL.NO_IV + '-' + RIGHT('0' + CONVERT(NVARCHAR(3), DENSE_RANK() OVER (PARTITION BY IL.NO_IV ORDER BY IL.NO_SO)), 2)
								 ELSE IL.NO_IV END) AS NO_IV_ORG,
	   @P_ID_USER, 
	   NEOE.SF_SYSDATE(GETDATE())
FROM #XML IL
JOIN (SELECT NO_IV, 
			 COUNT(DISTINCT NO_SO) AS CNT_SO
	  FROM #XML 
	  GROUP BY NO_IV) IL1
ON IL1.NO_IV = IL.NO_IV

DROP TABLE #XML
GO