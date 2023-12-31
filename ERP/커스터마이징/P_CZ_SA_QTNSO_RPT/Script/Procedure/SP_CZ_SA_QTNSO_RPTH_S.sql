USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_QTNSO_RPTH_S]    Script Date: 2015-07-24 오전 9:24:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--EXEC SP_CZ_SA_QTNSO_RPTH_S '000', 0, '000', '20150628', '20150728', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1'
ALTER PROCEDURE [NEOE].[SP_CZ_SA_QTNSO_RPTH_S]
(
	@P_CD_COMPANY		NVARCHAR(3),
	@P_TYPE				INT,
	@P_DATE_TYPE		NVARCHAR(3),
	@P_FROM_DATE		NVARCHAR(8),
	@P_TO_DATE          NVARCHAR(8),
	@P_AMOUNT_TYPE		NVARCHAR(3),
	@P_FROM_AMOUNT		NVARCHAR(MAX),
	@P_TO_AMOUNT        NVARCHAR(MAX),
	@P_BUYER_CODE		NVARCHAR(5),
	@P_SUPPLIER_CODE	NVARCHAR(5),
	@P_HULL_NO			NVARCHAR(20),
	@P_USER_ID			NVARCHAR(8),
	@P_GRP_CODE			NVARCHAR(6),
	@P_COUNTRY_CODE		NVARCHAR(2),
	@P_FILE_NO			NVARCHAR(MAX),
	@P_SUBJECT			NVARCHAR(MAX),
	@P_ITEM_CODE		NVARCHAR(MAX),
	@P_ITEM_NAME		NVARCHAR(MAX),
	@P_INQ_NO			NVARCHAR(50),
	@P_ORDER_NO			NVARCHAR(50)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @QUERY NVARCHAR(MAX)
DECLARE @WHERE NVARCHAR(MAX)
DECLARE @TABLEPREFIX NVARCHAR(1)
DECLARE @USER_VALUE NVARCHAR(21)
DECLARE @RETURN_YN NVARCHAR(24)

IF @P_CD_COMPANY = '000'
BEGIN
	SET @TABLEPREFIX = 'T'
	SET @USER_VALUE = 'NVL(SH.USER_VALUE, 0)'
	SET @RETURN_YN = 'AND PH.RETURN_YN = ''N'''
END
ELSE IF @P_CD_COMPANY = '001'
BEGIN
	SET @TABLEPREFIX = 'T'
	SET @USER_VALUE = '0'
	SET @RETURN_YN = 'AND PH.RETURN_YN = ''N'''
END
ELSE
BEGIN
	SET @TABLEPREFIX = 'R'
	SET @USER_VALUE = '0'
	SET @RETURN_YN = ''
END

IF @P_TYPE = 0
BEGIN
SET @QUERY = 'SELECT QH.FILE_NO,
					 QH.BUYER_CODE,
					 CM.COMP_NAME AS BUYER_NAME,
					 QH.HULL_NO,
					 VS.VESSEL_NAME,
					 QH.REF_NO AS INQ_NO,
					 SH.REF_NO AS ORDER_NO,
					 TO_CHAR(QH.INQ_DATE, ''YYYYMMDD'') AS INQ_DATE,
					 TO_CHAR(QH.QTN_DATE, ''YYYYMMDD'') AS QTN_DATE,
					 SH.ISSUED_DATE AS SO_DATE,
					 QH.CURRENCY,
					 QH.EX_RATE,
					 QH.POR_AMOUNT,
					 ROUND(NVL((QH.QTN_AMOUNT / CASE NVL(QH.EX_RATE, 0) WHEN 0 THEN 1 ELSE QH.EX_RATE END), 0), 2) AS QTN_EX,
					 QH.QTN_AMOUNT,
					 ROUND(NVL((SH.SO_AMOUNT / CASE NVL(SH.EX_RATE, 0) WHEN 0 THEN 1 ELSE SH.EX_RATE END), 0), 2) AS SO_EX,
					 NVL(SH.SO_AMOUNT, 0) AS SO_AMOUNT,
					 QH.MAR_AMOUNT,
					 QH.MAR_RATE
			  FROM ' + @TABLEPREFIX + '_PS003 QH
			  LEFT JOIN (SELECT SH.FILE_NO,
								SH.REF_NO,
								SH.ISSUED_DATE,
								SH.EX_RATE,
								(ROUND(SH.TOT_AMOUNT, 2) - ROUND(SH.TOT_AMOUNT * (SH.DISCOUNT + ' + @USER_VALUE + ') / 100, 2)) AS SO_AMOUNT
						 FROM ' + @TABLEPREFIX + '_PS011 SH) SH 
			  ON SH.FILE_NO = QH.FILE_NO
			  LEFT JOIN ' + @TABLEPREFIX + '_BC002 CM ON CM.COMP_CODE = QH.BUYER_CODE
			  LEFT JOIN ' + @TABLEPREFIX + '_BP001 VS ON VS.HULL_NO = QH.HULL_NO
			  WHERE 1=1'
END
ELSE IF @P_TYPE = 1
BEGIN
SET @QUERY = 'SELECT QL.FILE_NO,
			         QL.SUBJECT_NAME,
			         QL.ITEM_CODE,
			         QL.DESCRIPTION,
			         QL.QTY,
			         QL.UNIT,
					 (CASE WHEN NVL(PL.DC_PRICE, 0) = 0 THEN QL.P_PRICE ELSE PL.DC_PRICE END) AS P_PRICE,
					 (CASE WHEN NVL(PL.DC_AMOUNT, 0) = 0 THEN QL.P_AMOUNT ELSE PL.DC_AMOUNT END) AS P_AMOUNT,
					 (CASE WHEN NVL((PL.DC_AMOUNT * PH.EX_RATE), 0) = 0 THEN QL.P_AMOUNT ELSE (PL.DC_AMOUNT * PH.EX_RATE) END) AS PW_AMOUNT,
					 QL.Q_PRICE,
			         QL.Q_AMOUNT,
					 NVL((QL.Q_AMOUNT * QH.EX_RATE), 0) AS QW_AMOUNT,
			         QL.LT,
					 QH.BUYER_CODE,
					 CM1.COMP_NAME AS BUYER_NAME,
			         NVL(PL.COMP_CODE, QL.STOCK_COMP) AS SUPPLIER_CODE,
			         CM.COMP_NAME AS SUPPLIER_NAME,
					 QH.REF_NO AS INQ_NO,
					 SH.REF_NO AS ORDER_NO,
					 QH.HULL_NO,
					 VS.VESSEL_NAME,
					 QL.S_CLASS,
					 QL.STOCK_CODE,
					 (SELECT MAX(INV_NO) AS INV_NO FROM ' + @TABLEPREFIX + '_CA002 WHERE INV_NO LIKE ''PS%'' AND ORD_NO = QL.FILE_NO AND SUBJECT_CODE = QL.SUBJECT_CODE AND ITEM_CODE = QL.ITEM_CODE) AS INV_NO
			  FROM ' + @TABLEPREFIX + '_PS004 QL
			  LEFT JOIN ' + @TABLEPREFIX + '_PS003 QH ON QH.FILE_NO = QL.FILE_NO
			  LEFT JOIN ' + @TABLEPREFIX + '_PS011 SH ON SH.FILE_NO = QH.FILE_NO
			  LEFT JOIN ' + @TABLEPREFIX + '_PS008 PL ON PL.FILE_NO = QL.FILE_NO AND PL.SUBJECT_CODE = QL.SUBJECT_CODE AND PL.ITEM_CODE = QL.ITEM_CODE AND (PL.CHOICE = ''Y'' OR PL.S_CLASS = ''Y'')
			  LEFT JOIN ' + @TABLEPREFIX + '_PS007 PH ON PH.FILE_NO = PL.FILE_NO AND PH.COMP_CODE = PL.COMP_CODE
			  LEFT JOIN ' + @TABLEPREFIX + '_BC002 CM ON CM.COMP_CODE = NVL(PL.COMP_CODE, QL.STOCK_COMP)
			  LEFT JOIN ' + @TABLEPREFIX + '_BC002 CM1 ON CM1.COMP_CODE = QH.BUYER_CODE
			  LEFT JOIN ' + @TABLEPREFIX + '_BP001 VS ON VS.HULL_NO = QH.HULL_NO
			  WHERE 1=1'
END
ELSE IF @P_TYPE = 2
BEGIN
SET @QUERY = 'SELECT SH.FILE_NO,
					 SH.BUYER_CODE,
					 CM.COMP_NAME AS BUYER_NAME,
					 SH.HULL_NO,
					 VS.VESSEL_NAME,
					 QH.REF_NO AS INQ_NO,
					 SH.REF_NO AS ORDER_NO,
					 TO_CHAR(QH.INQ_DATE, ''YYYYMMDD'') AS INQ_DATE,
					 TO_CHAR(QH.QTN_DATE, ''YYYYMMDD'') AS QTN_DATE,
					 SH.ISSUED_DATE AS SO_DATE,
					 SH.CURRENCY,
					 SH.EX_RATE,
					 NVL(SH.POR_AMOUNT, 0) AS POR_AMOUNT,
					 ROUND(NVL((QH.QTN_AMOUNT / CASE NVL(QH.EX_RATE, 0) WHEN 0 THEN 1 ELSE QH.EX_RATE END), 0), 2) AS QTN_EX,
					 QH.QTN_AMOUNT,
					 ROUND(NVL((SH.SO_AMOUNT / CASE NVL(SH.EX_RATE, 0) WHEN 0 THEN 1 ELSE SH.EX_RATE END), 0), 2) AS SO_EX,
					 NVL(SH.SO_AMOUNT, 0) AS SO_AMOUNT,
					 (SH.SO_AMOUNT - SH.POR_AMOUNT) AS MAR_AMOUNT,
					 CASE WHEN SH.SO_AMOUNT = 0 THEN 0 ELSE ROUND((((SH.SO_AMOUNT - SH.POR_AMOUNT) / SH.SO_AMOUNT) * 100), 2) END AS MAR_RATE
			  FROM (SELECT SH.FILE_NO,
						   SH.BUYER_CODE,
						   SH.HULL_NO,
						   SH.REF_NO,
						   SH.ISSUED_DATE,
						   SH.CURRENCY,
						   SH.EX_RATE,
						   SH.USER_ID,
						   (SELECT POR_AMOUNT FROM ' + @TABLEPREFIX + '_PROJ_NO_PORAMT_VIEW Z WHERE Z.FILE_NO = SH.FILE_NO) AS POR_AMOUNT,
						   (ROUND(SH.TOT_AMOUNT, 2) - ROUND(SH.TOT_AMOUNT * (SH.DISCOUNT + ' + @USER_VALUE + ') / 100, 2)) AS SO_AMOUNT
					FROM ' + @TABLEPREFIX + '_PS011 SH) SH
			  LEFT JOIN ' + @TABLEPREFIX + '_PS003 QH ON QH.FILE_NO = SH.FILE_NO
			  LEFT JOIN ' + @TABLEPREFIX + '_BC002 CM ON CM.COMP_CODE = SH.BUYER_CODE
			  LEFT JOIN ' + @TABLEPREFIX + '_BP001 VS ON VS.HULL_NO = SH.HULL_NO
			  WHERE 1=1'
END
ELSE IF @P_TYPE = 3
BEGIN
SET @QUERY = 'SELECT SL.FILE_NO,
			  		 SL.SUBJECT_NAME,
			  		 SL.ITEM_CODE,
			  		 SL.DESCRIPTION,
			  		 NVL(PL.QTY, 0) AS QTY,
			  		 SL.UNIT,
			  		 NVL(PL.P_PRICE, 0) AS P_PRICE,
					 NVL(PL.P_AMOUNT, 0) AS P_AMOUNT,
					 NVL(PL.PW_AMOUNT, 0) AS PW_AMOUNT,
					 NVL((CASE WHEN SL.S_CLASS = ''Y'' THEN SL.S_PRICE ELSE SL.Q_PRICE END), 0) Q_PRICE,
			  		 NVL((CASE WHEN SL.S_CLASS = ''Y'' THEN SL.S_AMOUNT ELSE SL.Q_AMOUNT END), 0) Q_AMOUNT,
					 NVL((CASE WHEN SL.S_CLASS = ''Y'' THEN (SL.S_AMOUNT * SH.EX_RATE) ELSE SL.W_AMOUNT END), 0)  AS QW_AMOUNT,
			  		 SL.LT,
					 SH.BUYER_CODE,
					 SH.BUYER_NAME,
			  		 (CASE WHEN SL.S_CLASS = ''Y'' THEN SL.STOCK_COMP ELSE PL.COMP_CODE END) AS SUPPLIER_CODE,
			  		 CM.COMP_NAME AS SUPPLIER_NAME,
					 QH.REF_NO AS INQ_NO,
					 SH.REF_NO AS ORDER_NO,
					 SH.HULL_NO,
					 VS.VESSEL_NAME,
					 SL.S_CLASS,
					 SL.STOCK_CODE,
					 (SELECT MAX(INV_NO) AS INV_NO FROM ' + @TABLEPREFIX + '_CA002 WHERE INV_NO LIKE ''PS%'' AND ORD_NO = SL.FILE_NO AND SUBJECT_CODE = SL.SUBJECT_CODE AND ITEM_CODE = SL.ITEM_CODE) AS INV_NO
			  FROM ' + @TABLEPREFIX + '_PS012 SL
			  LEFT JOIN (SELECT SH.FILE_NO,
								SH.ISSUED_DATE,
								SH.USER_ID,
								SH.HULL_NO,
								SH.BUYER_CODE,
								CM.COMP_NAME AS BUYER_NAME,
								SH.EX_RATE,
								SH.REF_NO,
								(SELECT POR_AMOUNT FROM ' + @TABLEPREFIX + '_PROJ_NO_PORAMT_VIEW Z WHERE Z.FILE_NO = SH.FILE_NO) AS POR_AMOUNT,
								(ROUND(SH.TOT_AMOUNT, 2) - ROUND(SH.TOT_AMOUNT * (SH.DISCOUNT + ' + @USER_VALUE + ') / 100, 2)) AS SO_AMOUNT 
						 FROM ' + @TABLEPREFIX + '_PS011 SH
						 LEFT JOIN ' + @TABLEPREFIX + '_BC002 CM ON CM.COMP_CODE = SH.BUYER_CODE) SH 
			  ON SH.FILE_NO = SL.FILE_NO
			  LEFT JOIN ' + @TABLEPREFIX + '_PS003 QH ON QH.FILE_NO = SL.FILE_NO
			  LEFT JOIN (SELECT FILE_NO, SUBJECT_CODE, ITEM_CODE, MAX(COMP_CODE) AS COMP_CODE, SUM(QTY) AS QTY, MAX(P_PRICE) AS P_PRICE, SUM(P_AMOUNT) AS P_AMOUNT, SUM(PW_AMOUNT) AS PW_AMOUNT 
						 FROM (SELECT ''STOCK'' AS TYPE, ST.FILE_NO, ST.SUBJECT_CODE, ST.ITEM_CODE, '''' AS COMP_CODE, ST.USED_QTY AS QTY, ST.PRICE AS P_PRICE, (ST.PRICE*ST.USED_QTY) AS P_AMOUNT, (ST.PRICE*ST.USED_QTY) AS PW_AMOUNT
						 FROM ' + @TABLEPREFIX + '_PL021_H ST
						 WHERE EXISTS (SELECT 1 FROM ' + @TABLEPREFIX + '_PS012 WHERE FILE_NO = ST.FILE_NO AND SUBJECT_CODE = ST.SUBJECT_CODE AND ITEM_CODE = ST.ITEM_CODE AND S_QTY > 0)
						 GROUP BY ST.FILE_NO, ST.SUBJECT_CODE, ST.ITEM_CODE, ST.USED_QTY, ST.PRICE
						 UNION ALL
						 SELECT ''PURCHASE'' AS TYPE, PL.FILE_NO, PL.SUBJECT_CODE, PL.ITEM_CODE, PH.COMP_CODE, PL.QTY, PL.DC_PRICE AS P_PRICE, PL.DC_AMOUNT AS P_AMOUNT, ROUND(PL.DC_AMOUNT*PH.EX_RATE) AS PW_AMOUNT
						 FROM ' + @TABLEPREFIX + '_PP003 PL
						 JOIN ' + @TABLEPREFIX + '_PP002 PH ON PH.POR_NO = PL.POR_NO ' + @RETURN_YN + '
						 GROUP BY PL.FILE_NO, PL.SUBJECT_CODE, PL.ITEM_CODE, PH.COMP_CODE, PL.QTY, PL.DC_PRICE, PL.DC_AMOUNT, PH.EX_RATE
						 UNION ALL
						 SELECT ''BOOKING'' AS TYPE, BK.FILE_NO, BK.SUBJECT_CODE, BK.ITEM_CODE, PH.COMP_CODE, BK.BOOKING_QTY AS QTY, PL.DC_PRICE AS P_PRICE, (PL.DC_PRICE*BK.BOOKING_QTY) AS P_AMOUNT, TRUNC(PL.DC_PRICE*BK.BOOKING_QTY*PH.EX_RATE) AS PW_AMOUNT
						 FROM ' + @TABLEPREFIX + '_PP007 BK
						 JOIN ' + @TABLEPREFIX + '_PP003 PL ON PL.FILE_NO = BK.ST_NO AND PL.STOCK_CODE = BK.STOCK_CODE
						 JOIN ' + @TABLEPREFIX + '_PP002 PH ON PH.POR_NO = PL.POR_NO
						 WHERE BK.BOOKING_QTY > 0
						 GROUP BY BK.FILE_NO, BK.SUBJECT_CODE, BK.ITEM_CODE, PH.COMP_CODE, PL.QTY, PL.DC_PRICE, BK.BOOKING_QTY, PH.EX_RATE)
						 GROUP BY FILE_NO, SUBJECT_CODE, ITEM_CODE) PL
			  ON PL.FILE_NO = SL.FILE_NO AND PL.SUBJECT_CODE = SL.SUBJECT_CODE AND PL.ITEM_CODE = SL.ITEM_CODE
			  LEFT JOIN ' + @TABLEPREFIX + '_BC002 CM ON CM.COMP_CODE = (CASE WHEN SL.S_CLASS = ''Y'' THEN SL.STOCK_COMP ELSE PL.COMP_CODE END)
			  LEFT JOIN ' + @TABLEPREFIX + '_BP001 VS ON VS.HULL_NO = SH.HULL_NO
			  WHERE 1=1'
END

SET @WHERE = ''

IF @P_DATE_TYPE = '000'
BEGIN
	SET @WHERE += CHAR(13) + 'AND TO_CHAR(QH.INQ_DATE, ''YYYYMMDD'') BETWEEN ''' + @P_FROM_DATE + ''' AND ''' + @P_TO_DATE + ''''
END
ELSE IF @P_DATE_TYPE = '001'
BEGIN
	SET @WHERE += CHAR(13) + 'AND TO_CHAR(QH.QTN_DATE, ''YYYYMMDD'') BETWEEN ''' + @P_FROM_DATE + ''' AND ''' + @P_TO_DATE + ''''
END
ELSE IF @P_DATE_TYPE = '002'
BEGIN
	SET @WHERE += CHAR(13) + 'AND SH.ISSUED_DATE BETWEEN ''' + @P_FROM_DATE + ''' AND ''' + @P_TO_DATE + ''''
END

IF @P_AMOUNT_TYPE = '000'
BEGIN
	IF @P_TYPE = 0 OR @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND QH.POR_AMOUNT BETWEEN ''' + @P_FROM_AMOUNT + ''' AND ''' + @P_TO_AMOUNT + ''''
	ELSE IF @P_TYPE = 2 OR @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND SH.POR_AMOUNT BETWEEN ''' + @P_FROM_AMOUNT + ''' AND ''' + @P_TO_AMOUNT + ''''
END
ELSE IF @P_AMOUNT_TYPE = '001'
BEGIN
	SET @WHERE += CHAR(13) + 'AND QH.QTN_AMOUNT BETWEEN ''' + @P_FROM_AMOUNT + ''' AND ''' + @P_TO_AMOUNT + ''''
END
ELSE IF @P_AMOUNT_TYPE = '002'
BEGIN
	IF @P_TYPE = 0 OR @P_TYPE = 2 OR @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND SH.SO_AMOUNT BETWEEN ''' + @P_FROM_AMOUNT + ''' AND ''' + @P_TO_AMOUNT + ''''
	ELSE IF @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND (ROUND(SH.TOT_AMOUNT, 2) - ROUND(SH.TOT_AMOUNT * (SH.DISCOUNT + ' + @USER_VALUE + ') / 100, 2)) BETWEEN ''' + @P_FROM_AMOUNT + ''' AND ''' + @P_TO_AMOUNT + ''''
END
ELSE IF @P_AMOUNT_TYPE = '003'
BEGIN
	IF @P_TYPE = 0 OR @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND QH.MAR_AMOUNT BETWEEN ''' + @P_FROM_AMOUNT + ''' AND ''' + @P_TO_AMOUNT + ''''
	ELSE IF @P_TYPE = 2 OR @P_TYPE = 3 
		SET @WHERE += CHAR(13) + 'AND (SH.SO_AMOUNT - SH.POR_AMOUNT) BETWEEN ''' + @P_FROM_AMOUNT + ''' AND ''' + @P_TO_AMOUNT + ''''
END

IF ISNULL(@P_BUYER_CODE, '') <> ''
BEGIN
	IF @P_TYPE = 0 OR @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND QH.BUYER_CODE = ''' + @P_BUYER_CODE + ''''
	ELSE IF @P_TYPE = 2 OR @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND SH.BUYER_CODE = ''' + @P_BUYER_CODE + ''''
END

IF ISNULL(@P_HULL_NO, '') <> ''
BEGIN
	IF @P_TYPE = 0 OR @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND QH.HULL_NO = ''' + @P_HULL_NO + ''''
	ELSE IF @P_TYPE = 2 OR @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND SH.HULL_NO = ''' + @P_HULL_NO + ''''
END

IF ISNULL(@P_USER_ID, '') <> ''
BEGIN
	IF @P_TYPE = 0 OR @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND QH.USER_ID = ''' + @P_USER_ID + ''''
	ELSE IF @P_TYPE = 2 OR @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND SH.USER_ID = ''' + @P_USER_ID + ''''
END

IF ISNULL(@P_GRP_CODE, '') <> ''
BEGIN
	SET @WHERE += CHAR(13) + 'AND QH.GRP_CD = ''' + @P_GRP_CODE + ''''
END

IF ISNULL(@P_COUNTRY_CODE, '') <> ''
BEGIN
	IF @P_TYPE = 0 OR @P_TYPE = 2
		SET @WHERE += CHAR(13) + 'AND CM.CUN_CODE = ''' + @P_COUNTRY_CODE + ''''
	ELSE IF @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND EXISTS (SELECT 1 FROM ' + @TABLEPREFIX + '_BC002 WHERE COMP_CODE = QH.BUYER_CODE AND CUN_CODE = ''' + @P_COUNTRY_CODE + ''')'
	ELSE IF @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND EXISTS (SELECT 1 FROM ' + @TABLEPREFIX + '_BC002 WHERE COMP_CODE = SH.BUYER_CODE AND CUN_CODE = ''' + @P_COUNTRY_CODE + ''')'
END

IF ISNULL(@P_FILE_NO, '') <> ''
BEGIN
	IF @P_TYPE = 0
		SET @WHERE += CHAR(13) + 'AND QH.FILE_NO LIKE ''' + '%' + @P_FILE_NO + '%' + ''''
	ELSE IF @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND QL.FILE_NO LIKE ''' + '%' + @P_FILE_NO + '%' + ''''
	ELSE IF @P_TYPE = 2
		SET @WHERE += CHAR(13) + 'AND SH.FILE_NO LIKE ''' + '%' + @P_FILE_NO + '%' + ''''
	ELSE IF @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND SL.FILE_NO LIKE ''' + '%' + @P_FILE_NO + '%' + ''''
END

IF ISNULL(@P_SUPPLIER_CODE, '') <> ''
BEGIN
	IF @P_TYPE = 0
		SET @WHERE += CHAR(13) + 'AND EXISTS (SELECT 1 
											  FROM ' + @TABLEPREFIX + '_PS004 QL, ' + @TABLEPREFIX + '_PP003 PL, ' + @TABLEPREFIX + '_PP002 PH
											  WHERE QL.FILE_NO = PL.FILE_NO
											  AND QL.SUBJECT_CODE = PL.SUBJECT_CODE
											  AND QL.ITEM_CODE = PL.ITEM_CODE
											  AND PL.POR_NO = PH.POR_NO
											  AND QL.FILE_NO = QH.FILE_NO
											  AND PH.COMP_CODE = ''' + @P_SUPPLIER_CODE + ''')'
	ELSE IF @P_TYPE = 2
		SET @WHERE += CHAR(13) + 'AND EXISTS (SELECT 1 
											  FROM ' + @TABLEPREFIX + '_PS012 SL, ' + @TABLEPREFIX + '_PP003 PL, ' + @TABLEPREFIX + '_PP002 PH
											  WHERE SL.FILE_NO = PL.FILE_NO
											  AND SL.SUBJECT_CODE = PL.SUBJECT_CODE
											  AND SL.ITEM_CODE = PL.ITEM_CODE
											  AND PL.POR_NO = PH.POR_NO
											  AND SL.FILE_NO = SH.FILE_NO
											  AND PH.COMP_CODE = ''' + @P_SUPPLIER_CODE + ''')' 
	ELSE IF @P_TYPE = 1 OR @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND CM.COMP_CODE = ''' + @P_SUPPLIER_CODE + ''''
END

IF ISNULL(@P_SUBJECT, '') <> ''
BEGIN
	IF @P_TYPE = 0
		SET @WHERE += CHAR(13) + 'AND EXISTS (SELECT 1 
											  FROM ' + @TABLEPREFIX + '_PS004 QL
											  WHERE QL.FILE_NO = QH.FILE_NO
											  AND QL.SUBJECT_NAME LIKE ''' + '%' + @P_SUBJECT + '%' + ''')'
	ELSE IF @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND QL.SUBJECT_NAME LIKE ''' + '%' + @P_SUBJECT + '%' + ''''
	ELSE IF @P_TYPE = 2
		SET @WHERE += CHAR(13) + 'AND EXISTS (SELECT 1 
											  FROM ' + @TABLEPREFIX + '_PS012 SL
											  WHERE SL.FILE_NO = SH.FILE_NO
											  AND SL.SUBJECT_NAME LIKE ''' + '%' + @P_SUBJECT + '%' + ''')'
	ELSE IF @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND SL.SUBJECT_NAME LIKE ''' + '%' + @P_SUBJECT + '%' + ''''
END

IF ISNULL(@P_ITEM_CODE, '') <> ''
BEGIN
	IF @P_TYPE = 0
		SET @WHERE += CHAR(13) + 'AND EXISTS (SELECT 1 
											  FROM ' + @TABLEPREFIX + '_PS004 QL
											  WHERE QL.FILE_NO = QH.FILE_NO
											  AND QL.ITEM_CODE LIKE ''' + '%' + @P_ITEM_CODE + '%' + ''')'
	ELSE IF @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND QL.ITEM_CODE LIKE ''' + '%' + @P_ITEM_CODE + '%' + ''''
	ELSE IF @P_TYPE = 2
		SET @WHERE += CHAR(13) + 'AND EXISTS (SELECT 1 
											  FROM ' + @TABLEPREFIX + '_PS012 SL
											  WHERE SL.FILE_NO = SH.FILE_NO
											  AND SL.ITEM_CODE LIKE ''' + '%' + @P_ITEM_CODE + '%' + ''')'
	ELSE IF @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND SL.ITEM_CODE LIKE ''' + '%' + @P_ITEM_CODE + '%' + ''''
END

IF ISNULL(@P_ITEM_NAME, '') <> ''
BEGIN
	IF @P_TYPE = 0
		SET @WHERE += CHAR(13) + 'AND EXISTS (SELECT 1 
											  FROM ' + @TABLEPREFIX + '_PS004 QL
											  WHERE QL.FILE_NO = QH.FILE_NO
											  AND QL.DESCRIPTION LIKE ''' + '%' + @P_ITEM_NAME + '%' + ''')'
	ELSE IF @P_TYPE = 1
		SET @WHERE += CHAR(13) + 'AND QL.DESCRIPTION LIKE ''' + '%' + @P_ITEM_NAME + '%' + ''''
	ELSE IF @P_TYPE = 2
		SET @WHERE += CHAR(13) + 'AND EXISTS (SELECT 1 
											  FROM ' + @TABLEPREFIX + '_PS012 SL
											  WHERE SL.FILE_NO = SH.FILE_NO
											  AND SL.DESCRIPTION LIKE ''' + '%' + @P_ITEM_NAME + '%' + ''')'
	ELSE IF @P_TYPE = 3
		SET @WHERE += CHAR(13) + 'AND SL.DESCRIPTION LIKE ''' + '%' + @P_ITEM_NAME + '%' + ''''
END

IF ISNULL(@P_INQ_NO, '') <> ''
BEGIN
	SET @WHERE += CHAR(13) + 'AND QH.REF_NO = ''' + @P_INQ_NO + ''''
END

IF ISNULL(@P_ORDER_NO, '') <> ''
BEGIN
	SET @WHERE += CHAR(13) + 'AND SH.REF_NO = ''' + @P_ORDER_NO + ''''
END

SET @QUERY = @QUERY + @WHERE + ' ORDER BY FILE_NO DESC'
--SET @QUERY = REPLACE(@QUERY, '''', '''''')
--SET @QUERY = 'SELECT * FROM OPENQUERY(DINTEC, ''' +  @QUERY + ''')'

--EXEC SP_EXECUTESQL @QUERY
--PRINT @QUERY
SELECT @QUERY

GO