USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_PARTNER_PRICE_EXCEL]    Script Date: 2016-08-09 오후 4:28:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_MA_PARTNER_PRICE_EXCEL] 
(
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10)
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_DOC INT
DECLARE @V_ERRMSG NVARCHAR(255)
DECLARE @V_CD_ITEM NVARCHAR(20) = NULL

EXEC SP_XML_PREPAREDOCUMENT @V_DOC OUTPUT, @P_XML

SELECT * INTO #XML
FROM OPENXML (@V_DOC, '/XML/ROW', 2) WITH 
(
	CD_COMPANY		    NVARCHAR(7),
    CD_PLANT			NVARCHAR(7),
	CD_PARTNER			NVARCHAR(20),
	CD_ITEM				NVARCHAR(20),
	CD_EXCH				NVARCHAR(3),
	FG_UM				NCHAR(3),
	TP_UMMODULE		    NCHAR(3),
	NO_LINE			    NUMERIC(5, 0),
	SDT_UM			    NCHAR(8),

	UM_ITEM				NUMERIC(15, 4),
	DC_PRICE_TERMS		NVARCHAR(20),
	LT					NUMERIC(5, 0),
	EDT_UM				NCHAR(8),
	CD_EXCH_S			NVARCHAR(3),
	UM_ITEM_S			NUMERIC(15, 4),
	RT_PROFIT_A			NUMERIC(11, 4),
	RT_PROFIT_B			NUMERIC(11, 4),
	RT_PROFIT_C			NUMERIC(11, 4),
	DC_RMK				NVARCHAR(100),
	TXT_USERDEF1		NVARCHAR(300),
    CD_PARTNER_STD		NVARCHAR(20),
	YN_DELETE			NVARCHAR(1)
)

EXEC SP_XML_REMOVEDOCUMENT @V_DOC

BEGIN TRAN SP_CZ_MA_PARTNER_PRICE_EXCEL
BEGIN TRY

;WITH A AS
(
	SELECT A.CD_COMPANY, 
		   A.CD_PLANT,
		   A.CD_PARTNER,
		   A.TP_UMMODULE,
		   A.CD_ITEM,
		   A.DC_PRICE_TERMS,
		   A.SDT_UM,
		   A.EDT_UM
	FROM MA_ITEM_UMPARTNER A
	WHERE EXISTS (SELECT 1 
				  FROM #XML B 
				  WHERE B.CD_COMPANY = A.CD_COMPANY 
				  AND B.CD_PLANT = A.CD_PLANT 
				  AND B.CD_PARTNER = A.CD_PARTNER
				  AND B.TP_UMMODULE = A.TP_UMMODULE
				  AND B.CD_ITEM = A.CD_ITEM
				  AND B.DC_PRICE_TERMS = A.DC_PRICE_TERMS
				  AND B.YN_DELETE <> 'Y')
	AND NOT EXISTS (SELECT 1 
					FROM #XML B
					WHERE B.CD_COMPANY = A.CD_COMPANY
					AND B.CD_PLANT = A.CD_PLANT
					AND B.CD_PARTNER = A.CD_PARTNER
					AND B.TP_UMMODULE = A.TP_UMMODULE
					AND B.CD_ITEM = A.CD_ITEM
					AND B.CD_EXCH = A.CD_EXCH
					AND B.SDT_UM = A.SDT_UM
					AND B.NO_LINE = A.NO_LINE
					AND B.YN_DELETE <> 'Y'
					AND (B.FG_UM = A.FG_UM OR (SELECT COUNT(1) 
											   FROM MA_ITEM_UMPARTNER C
											   WHERE C.CD_COMPANY = B.CD_COMPANY
											   AND C.CD_PLANT = B.CD_PLANT
											   AND C.CD_PARTNER = B.CD_PARTNER
											   AND C.TP_UMMODULE = B.TP_UMMODULE
											   AND C.CD_ITEM = B.CD_ITEM
											   AND C.CD_EXCH = B.CD_EXCH 
											   AND C.SDT_UM = B.SDT_UM
											   AND C.NO_LINE = B.NO_LINE) = 1))
	UNION ALL
	SELECT A.CD_COMPANY, 
		   A.CD_PLANT,
		   A.CD_PARTNER,
		   A.TP_UMMODULE,
		   A.CD_ITEM,
		   A.DC_PRICE_TERMS,
		   A.SDT_UM,
		   A.EDT_UM
	FROM #XML A
	WHERE A.YN_DELETE <> 'Y'
),
B AS
(
	SELECT *,
		   ROW_NUMBER() OVER (PARTITION BY A.CD_COMPANY, A.CD_PLANT, A.CD_PARTNER, A.TP_UMMODULE, A.CD_ITEM, A.DC_PRICE_TERMS ORDER BY A.SDT_UM) AS IDX
	FROM A
)
SELECT @V_CD_ITEM = MAX(B.CD_ITEM) 
FROM B
WHERE EXISTS (SELECT 1 
			  FROM B C
			  WHERE C.CD_COMPANY = B.CD_COMPANY 
			  AND C.CD_PLANT = B.CD_PLANT 
			  AND C.CD_PARTNER = B.CD_PARTNER
			  AND C.TP_UMMODULE = B.TP_UMMODULE
			  AND C.CD_ITEM = B.CD_ITEM
			  AND C.DC_PRICE_TERMS = B.DC_PRICE_TERMS
			  AND C.SDT_UM >= B.SDT_UM
			  AND C.EDT_UM <= B.EDT_UM
			  AND C.IDX <> B.IDX)

IF @V_CD_ITEM IS NOT NULL
BEGIN
	SELECT @V_ERRMSG = '중복되는 품목이 존재 합니다. [' + @V_CD_ITEM + ']'
	GOTO ERROR
END

-- ================================================== DELETE
DELETE UP
FROM MA_ITEM_UMPARTNER UP
WHERE EXISTS (SELECT 1 
			  FROM #XML XL
			  WHERE XL.CD_COMPANY = UP.CD_COMPANY
			  AND XL.CD_PLANT = UP.CD_PLANT
			  AND XL.CD_PARTNER = UP.CD_PARTNER
			  AND XL.TP_UMMODULE = UP.TP_UMMODULE
			  AND XL.CD_ITEM = UP.CD_ITEM
			  AND XL.FG_UM = UP.FG_UM
			  AND XL.CD_EXCH = UP.CD_EXCH
			  AND XL.SDT_UM = UP.SDT_UM
			  AND XL.NO_LINE = UP.NO_LINE
			  AND XL.YN_DELETE = 'Y')

UPDATE IU
SET IU.NO_LINE = IU.NO_LINE1
FROM (SELECT *, 
			 ROW_NUMBER() OVER (PARTITION BY CD_COMPANY, CD_PARTNER, CD_ITEM ORDER BY DTS_INSERT) AS NO_LINE1
	  FROM MA_ITEM_UMPARTNER IU
	  WHERE EXISTS (SELECT 1 
	  			    FROM #XML AS D
	  			    WHERE D.CD_COMPANY = IU.CD_COMPANY
	  			    AND D.CD_PARTNER = IU.CD_PARTNER
	  			    AND D.CD_ITEM = IU.CD_ITEM
					AND D.YN_DELETE = 'Y')) IU;

-- ================================================== INSERT
WITH A AS
(
	SELECT *, 
		   ROW_NUMBER() OVER (PARTITION BY CD_PARTNER, CD_ITEM ORDER BY DTS_INSERT) AS NO_LINE1 
	FROM (SELECT CD_COMPANY,
		         CD_PLANT,
		         CD_PARTNER,
		         TP_UMMODULE,
		         NO_LINE,
		         CD_ITEM,
		         FG_UM,
		         CD_EXCH,
		         UM_ITEM,
		         DC_PRICE_TERMS,
		         LT,
		         SDT_UM,
		         EDT_UM,
		         CD_EXCH_S,
		         UM_ITEM_S,
				 RT_PROFIT_A,
				 RT_PROFIT_B,
				 RT_PROFIT_C,
		         DC_RMK,
				 TXT_USERDEF1,
				 CD_PARTNER_STD,
		  	     ID_INSERT,
		  	     DTS_INSERT
		  FROM MA_ITEM_UMPARTNER IU
		  WHERE EXISTS (SELECT 1 
		  			    FROM #XML XL
		  			    WHERE XL.CD_COMPANY = IU.CD_COMPANY
		  			    AND XL.CD_PLANT = IU.CD_PLANT
		  			    AND XL.CD_PARTNER = IU.CD_PARTNER
		  			    AND XL.CD_ITEM = IU.CD_ITEM
						AND ISNULL(XL.YN_DELETE, 'N') = 'N'
						AND NOT EXISTS (SELECT 1 
						                FROM MA_ITEM_UMPARTNER UP
						                WHERE UP.CD_COMPANY = XL.CD_COMPANY
						                AND UP.CD_PLANT = XL.CD_PLANT
						                AND UP.CD_PARTNER = XL.CD_PARTNER
						                AND UP.TP_UMMODULE = XL.TP_UMMODULE
						                AND UP.CD_ITEM = XL.CD_ITEM
						                AND UP.FG_UM = XL.FG_UM
						                AND UP.CD_EXCH = XL.CD_EXCH
						                AND UP.SDT_UM = XL.SDT_UM
						                AND UP.NO_LINE = XL.NO_LINE))
		  UNION ALL
		  SELECT CD_COMPANY,
		         CD_PLANT,
		         CD_PARTNER,
		         TP_UMMODULE,
		         NO_LINE,
		         CD_ITEM,
		         FG_UM,
		         CD_EXCH,
		         UM_ITEM,
		         DC_PRICE_TERMS,
		         LT,
		         SDT_UM,
		         EDT_UM,
		         CD_EXCH_S,
		         UM_ITEM_S,
				 RT_PROFIT_A,
				 RT_PROFIT_B,
				 RT_PROFIT_C,
		         DC_RMK, 
				 TXT_USERDEF1,
				 CD_PARTNER_STD,
		  	     @P_ID_USER AS ID_INSERT, 
		  	     NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT 
		  FROM #XML XL
		  WHERE ISNULL(XL.YN_DELETE, 'N') = 'N'
		  AND NOT EXISTS (SELECT 1 
						  FROM MA_ITEM_UMPARTNER UP
						  WHERE UP.CD_COMPANY = XL.CD_COMPANY
						  AND UP.CD_PLANT = XL.CD_PLANT
						  AND UP.CD_PARTNER = XL.CD_PARTNER
						  AND UP.TP_UMMODULE = XL.TP_UMMODULE
						  AND UP.CD_ITEM = XL.CD_ITEM
						  AND UP.FG_UM = XL.FG_UM
						  AND UP.CD_EXCH = XL.CD_EXCH
						  AND UP.SDT_UM = XL.SDT_UM
						  AND UP.NO_LINE = XL.NO_LINE)) A
)
INSERT INTO MA_ITEM_UMPARTNER 
(
	CD_COMPANY,
	CD_PLANT,
	CD_PARTNER,
	TP_UMMODULE,
	NO_LINE,
	CD_ITEM,
	FG_UM,
	CD_EXCH,
	UM_ITEM,
	DC_PRICE_TERMS,
	LT,
	SDT_UM,
	EDT_UM,
	CD_EXCH_S,
	UM_ITEM_S,
	RT_PROFIT_A,
	RT_PROFIT_B,
	RT_PROFIT_C,
	DC_RMK,
	TXT_USERDEF1,
	CD_PARTNER_STD,
	ID_INSERT,
	DTS_INSERT
)
SELECT UP.CD_COMPANY,
	   UP.CD_PLANT,
	   UP.CD_PARTNER,
	   UP.TP_UMMODULE,
	   UP.NO_LINE1,
	   UP.CD_ITEM,
	   UP.FG_UM,
	   UP.CD_EXCH,
	   UP.UM_ITEM,
	   UP.DC_PRICE_TERMS,
	   UP.LT,
	   UP.SDT_UM,
	   UP.EDT_UM,
	   UP.CD_EXCH_S,
	   UP.UM_ITEM_S,
	   UP.RT_PROFIT_A,
	   UP.RT_PROFIT_B,
	   UP.RT_PROFIT_C,
	   UP.DC_RMK,
	   UP.TXT_USERDEF1,
	   UP.CD_PARTNER_STD,
       UP.ID_INSERT,
	   UP.DTS_INSERT
FROM A UP
WHERE ISNULL(UP.NO_LINE, 0) = 0

-- ================================================== UPDATE    
UPDATE UP 
   SET UP.FG_UM = XL.FG_UM,
	   UP.UM_ITEM = XL.UM_ITEM,
	   UP.DC_PRICE_TERMS = XL.DC_PRICE_TERMS,
	   UP.LT = XL.LT,
	   UP.EDT_UM = XL.EDT_UM,
	   UP.CD_EXCH_S = XL.CD_EXCH_S,
	   UP.UM_ITEM_S = XL.UM_ITEM_S,
	   UP.RT_PROFIT_A = XL.RT_PROFIT_A,
	   UP.RT_PROFIT_B = XL.RT_PROFIT_B,
	   UP.RT_PROFIT_C = XL.RT_PROFIT_C,
	   UP.DC_RMK = XL.DC_RMK,
	   UP.TXT_USERDEF1 = XL.TXT_USERDEF1,
	   UP.CD_PARTNER_STD = XL.CD_PARTNER_STD,
	   UP.ID_UPDATE = @P_ID_USER, 
	   UP.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM MA_ITEM_UMPARTNER UP
  JOIN #XML XL 
  ON UP.CD_COMPANY = XL.CD_COMPANY
  AND UP.CD_PLANT = XL.CD_PLANT
  AND UP.CD_PARTNER = XL.CD_PARTNER
  AND UP.TP_UMMODULE = XL.TP_UMMODULE
  AND UP.CD_ITEM = XL.CD_ITEM
  AND UP.CD_EXCH = XL.CD_EXCH 
  AND UP.SDT_UM = XL.SDT_UM
  AND UP.NO_LINE = XL.NO_LINE
  AND ISNULL(XL.YN_DELETE, 'N') = 'N'
  AND (UP.FG_UM = XL.FG_UM OR (SELECT COUNT(1) 
							   FROM MA_ITEM_UMPARTNER A
							   WHERE A.CD_COMPANY = XL.CD_COMPANY
							   AND A.CD_PLANT = XL.CD_PLANT
							   AND A.CD_PARTNER = XL.CD_PARTNER
							   AND A.TP_UMMODULE = XL.TP_UMMODULE
							   AND A.CD_ITEM = XL.CD_ITEM
							   AND A.CD_EXCH = XL.CD_EXCH 
							   AND A.SDT_UM = XL.SDT_UM
							   AND A.NO_LINE = XL.NO_LINE) = 1)

COMMIT TRAN SP_CZ_MA_PARTNER_PRICE_EXCEL

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_MA_PARTNER_PRICE_EXCEL
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN

ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)
ROLLBACK TRAN SP_CZ_MA_PARTNER_PRICE_EXCEL

GO