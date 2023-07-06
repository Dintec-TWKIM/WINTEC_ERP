SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE FROM CZ_MA_HULL_HGS_CUSTOMER
DELETE FROM CZ_MA_HULL_HGS_EQ_COUNT
DELETE FROM HGS_HULL_DOWN_ENGINE_ITEM

INSERT INTO CZ_MA_HULL_HGS_CUSTOMER
(
	NO_IMO,
	HULL_NO,
	SERVICEEQ,
	CD_CUSTOMER
)
SELECT ISNULL(IMO, '') AS NO_IMO,
	   HULLNO AS HULL_NO,
	   SERVICEEQ,
	   REPLACE(CD_CUSTOMER, '.xlsx', '') AS CD_CUSTOMER
FROM HGS_HULL_DOWN_ITEM_2
GROUP BY ISNULL(IMO, ''), 
		 HULLNO, 
		 SERVICEEQ, 
		 REPLACE(CD_CUSTOMER, '.xlsx', '')

INSERT INTO CZ_MA_HULL_HGS_EQ_COUNT
(
	NO_IMO,
	SERVICEEQ,
	SANCTION,
	HULLNO,
	SHIPNAME,
	CATEGORY,
	TYPE,
	POWERPRJ,
	PLANT,
	COUNT
)
SELECT A.NO_IMO,
	   A.SERVICEEQ,
	   A.SANCTION,
	   A.HULLNO,
	   A.SHIPNAME,
	   A.CATEGORY,
	   A.TYPE,
	   A.POWERPRJ,
	   A.PLANT,
	   A.COUNT
FROM
(SELECT ROW_NUMBER() OVER (PARTITION BY ISNULL(IMO, ''), SERVICEEQ, ISNULL(HULLNO, '') ORDER BY COUNT DESC) AS IDX,
	   ISNULL(IMO, '') AS NO_IMO,
	   SERVICEEQ,
	   SANCTION,
	   ISNULL(HULLNO, '') AS HULLNO,
	   SHIPNAME,
	   CATEGORY,
	   TYPE,
	   POWERPRJ,
	   PLANT,
	   COUNT
FROM HGS_HULL_DOWN_ITEM_2) A
WHERE A.IDX = 1

INSERT INTO HGS_HULL_DOWN_ENGINE_ITEM
(
	CD_CUSTOMER,
	NO_IMO,
	NO_ENGINE,
	SERIAL,
	NO_IMO_HGS,
	NO_HULL_HGS,
	CNT,
	COUNT,
	YN_USE,
	YN_URGENT
)
SELECT HC.CD_CUSTOMER,
	   HE.NO_IMO,
	   HE.NO_ENGINE,
	   HE.SERIAL,
	   HG.NO_IMO AS NO_IMO_HGS,
	   HG.HULLNO AS NO_HULL_HGS,
	   ISNULL(HE.QT_HGS, 0) AS QT_HGS,
	   ISNULL(HG.COUNT, 0) AS COUNT,
	   (CASE WHEN ISNULL(HE.QT_HGS, 0) <> ISNULL(HG.COUNT, 0) AND ISNULL(HG.SANCTION, 'N') <> 'Y' THEN 'N' ELSE 'Y' END) AS YN_USE,
	   HG.YN_URGENT
FROM CZ_MA_HULL_HGS_EQ_COUNT HG
JOIN (SELECT MH.NO_IMO,
	         MH.NO_HULL,
	  	     HE.NO_ENGINE,
	  	     HE.SERIAL,
	  	     HE.QT_HGS
	  FROM CZ_MA_HULL MH
	  JOIN CZ_MA_HULL_ENGINE HE ON HE.NO_IMO = MH.NO_IMO) HE
ON (HE.NO_IMO = HG.NO_IMO OR HE.NO_HULL = HG.HULLNO) AND HE.SERIAL = HG.SERVICEEQ
LEFT JOIN (SELECT NO_IMO,
				  HULL_NO,
				  SERVICEEQ,
				  MAX(CD_CUSTOMER) AS CD_CUSTOMER
	       FROM CZ_MA_HULL_HGS_CUSTOMER
		   GROUP BY NO_IMO, HULL_NO, SERVICEEQ) HC 
ON (HC.NO_IMO = HG.NO_IMO OR HC.HULL_NO = HG.HULLNO) AND HC.SERVICEEQ = HG.SERVICEEQ