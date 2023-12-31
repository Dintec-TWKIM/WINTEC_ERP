USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MATCHING_REG_S]    Script Date: 2017-01-05 오후 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--EXEC SP_CZ_PR_MATCHING_REG_S 'W100', 'F001', '2VC70002', '2VP70003', '2VP70003', '2VP70003'
ALTER PROCEDURE [NEOE].[SP_CZ_PR_MATCHING_REG_S]          
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
	@P_CD_ITEM			NVARCHAR(20),
	@P_CD_PITEM_L		NVARCHAR(20),
	@P_CD_PITEM_C		NVARCHAR(20),
	@P_CD_PITEM_R		NVARCHAR(20),
	@P_STA_MATCHING		NVARCHAR(4)
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_QUERY NVARCHAR(3000)

CREATE TABLE #MES
(
	CD_WO		VARCHAR(50),
	CD_ITEM		VARCHAR(30),
	CD_PITEM	VARCHAR(10),
	NO_ID	    VARCHAR(20),
	INSP_LC	    VARCHAR(20),
	NO_DATA	    NUMERIC(7, 4),
)
	
CREATE NONCLUSTERED INDEX MES ON #MES (CD_WO, CD_ITEM, CD_PITEM, NO_ID, INSP_LC)

SET @V_QUERY = 'SELECT CD_WO,
					   CD_ITEM,
					   CD_PITEM,
					   NO_ID,
					   INSP_LC,
					   NO_DATA 
				FROM OPENQUERY(MES, ''SELECT WP.CD_WO,
									  	     RQ.CD_MITEM AS CD_ITEM,
									  	     WP.CD_PITEM,
									  	     WP.ID_NO AS NO_ID,
									  	     ID.INSP_LC,
									  	     CONVERT(NUMERIC(7, 4), WP.NO_DATA) AS NO_DATA
									  FROM [WWiMES].[dbo].[TSWO_INSP] WP
									  JOIN [WWiMES].[dbo].[TSITEMPROC_INSP] ID ON ID.CD_PLANT = WP.CD_PLANT AND ID.CD_PITEM = WP.CD_PITEM AND ID.CD_IPROC = WP.CD_IPROC AND ID.SQ_INSP = WP.SQ_INSP
									  JOIN [WWiMES].[dbo].[TSWO_REQ] RQ ON RQ.CD_PLANT = WP.CD_PLANT AND RQ.CD_WO = WP.CD_WO AND RQ.WOOP_SQ = WP.WOOP_SQ
							          WHERE WP.CD_PLANT = ''''' + @P_CD_PLANT + '''''
							          AND RQ.CD_MITEM = ''''' + @P_CD_ITEM + '''''
							          AND WP.CD_PITEM = ''''' + @P_CD_PITEM_L + '''''
									  AND ISNULL(WP.CHK_ERROR, '''''''') = ''''''''
							          AND ID.INSP_LC IN (''''DI1-1'''', ''''DI1-2'''', ''''DI1-3'''')'')'

PRINT @V_QUERY

INSERT INTO #MES
(
	CD_WO, 
	CD_ITEM, 
	CD_PITEM, 
	NO_ID, 
	INSP_LC,
	NO_DATA
)
EXEC SP_EXECUTESQL @V_QUERY

SELECT C.CD_WO AS NO_WO,
	   C.CD_ITEM,
	   C.CD_PITEM,
	   C.NO_ID,
	   C.NUM_P1,
	   C.NUM_P2,
	   C.NUM_P3,
	   (CASE WHEN C.NUM_P1 <= C.NUM_P2 AND C.NUM_P1 <= C.NUM_P3 THEN C.NUM_P1
			 WHEN C.NUM_P2 <= C.NUM_P1 AND C.NUM_P2 <= C.NUM_P3 THEN C.NUM_P2
			 WHEN C.NUM_P3 <= C.NUM_P1 AND C.NUM_P3 <= C.NUM_P2 THEN C.NUM_P3 END) AS NUM_MIN,
	   'N' AS YN_USE 
FROM (SELECT B.CD_WO,
	  	     B.CD_ITEM,
	  	     B.CD_PITEM,
	  	     B.NO_ID,
	  	     B.[DI1-1] AS NUM_P1,
	         B.[DI1-2] AS NUM_P2,
	         B.[DI1-3] AS NUM_P3
	  FROM (SELECT MES.CD_WO,
	  		       MES.CD_ITEM,
	  		       MES.CD_PITEM,
	  		       MES.NO_ID,
	  		       MES.INSP_LC,
	  		       MES.NO_DATA  
	        FROM #MES MES
	  		WHERE NOT EXISTS (SELECT 1 
	  					      FROM CZ_PR_MATCHING_DATA MD 
	  					      WHERE MD.CD_COMPANY = @P_CD_COMPANY
	  					      AND MD.CD_PLANT = @P_CD_PLANT
	  					      AND MD.NO_WO_L = MES.CD_WO
	  					      AND MD.CD_ITEM = MES.CD_ITEM
	  					      AND MD.CD_PITEM_L = MES.CD_PITEM
	  					      AND MD.NO_ID_L = MES.NO_ID)
	  		AND NOT EXISTS (SELECT 1 
	  						FROM CZ_PR_MATCHING_DEACTIVATE DA
	  						WHERE DA.CD_COMPANY = @P_CD_COMPANY
	  						AND DA.CD_PLANT = @P_CD_PLANT
	  						AND DA.NO_ID = MES.NO_ID)) A
			PIVOT (MAX(A.NO_DATA) FOR A.INSP_LC IN ([DI1-1], [DI1-2], [DI1-3])) AS B
	  UNION ALL
	  SELECT DA.NO_WO,
	  	     DA.CD_ITEM,
	  	     DA.CD_PITEM,
	  	     DA.NO_ID,
	  	     DA.NUM_P1_OUT,
	  	     DA.NUM_P2_OUT,
	  	     DA.NUM_P3_OUT
	  FROM CZ_PR_MATCHING_DEACTIVATE DA
	  WHERE DA.CD_PLANT = @P_CD_PLANT
	  AND DA.CD_ITEM = @P_CD_ITEM
	  AND DA.CD_PITEM = @P_CD_PITEM_L
	  AND DA.STA_DEACTIVATE = '002'
	  AND NOT EXISTS (SELECT 1 
	  				  FROM CZ_PR_MATCHING_DATA MD 
	  				  WHERE MD.CD_COMPANY = @P_CD_COMPANY
	  				  AND MD.CD_PLANT = @P_CD_PLANT
	  				  AND MD.NO_WO_L = DA.NO_WO
	  				  AND MD.CD_ITEM = DA.CD_ITEM
	  				  AND MD.CD_PITEM_L = DA.CD_PITEM
	  				  AND MD.NO_ID_L = DA.NO_ID)) C

DELETE FROM #MES

SET @V_QUERY = 'SELECT CD_WO,
					   CD_ITEM,
					   CD_PITEM,
					   NO_ID,
					   INSP_LC,
					   NO_DATA  
				FROM OPENQUERY(MES, ''SELECT WP.CD_WO,
									  	     RQ.CD_MITEM AS CD_ITEM,
									  	     WP.CD_PITEM,
									  	     WP.ID_NO AS NO_ID,
									  	     ID.INSP_LC,
									  	     CONVERT(NUMERIC(7, 4), WP.NO_DATA) AS NO_DATA
									  FROM [WWiMES].[dbo].[TSWO_INSP] WP
									  JOIN [WWiMES].[dbo].[TSITEMPROC_INSP] ID ON ID.CD_PLANT = WP.CD_PLANT AND ID.CD_PITEM = WP.CD_PITEM AND ID.CD_IPROC = WP.CD_IPROC AND ID.SQ_INSP = WP.SQ_INSP
									  JOIN [WWiMES].[dbo].[TSWO_REQ] RQ ON RQ.CD_PLANT = WP.CD_PLANT AND RQ.CD_WO = WP.CD_WO AND RQ.WOOP_SQ = WP.WOOP_SQ
									  WHERE WP.CD_PLANT = ''''' + @P_CD_PLANT + '''''
									  AND RQ.CD_MITEM = ''''' + @P_CD_ITEM + '''''
									  AND WP.CD_PITEM = ''''' + @P_CD_PITEM_C + '''''
									  AND ISNULL(WP.CHK_ERROR, '''''''') = ''''''''
									  AND ID.INSP_LC IN (''''DI2-1'''', ''''DI2-2'''', ''''DI2-3'''', ''''DO1-1'''', ''''DO1-2'''', ''''DO1-3'''')'')'

PRINT @V_QUERY

INSERT INTO #MES
(
	CD_WO, 
	CD_ITEM, 
	CD_PITEM, 
	NO_ID, 
	INSP_LC,
	NO_DATA
)
EXEC SP_EXECUTESQL @V_QUERY

SELECT C.CD_WO AS NO_WO,
	   C.CD_ITEM,
	   C.CD_PITEM,
	   C.NO_ID,
	   C.NUM_P1_OUT,
	   C.NUM_P2_OUT,
	   C.NUM_P3_OUT,
	   (CASE WHEN C.NUM_P1_OUT >= C.NUM_P2_OUT AND C.NUM_P1_OUT >= C.NUM_P3_OUT THEN C.NUM_P1_OUT
			 WHEN C.NUM_P2_OUT >= C.NUM_P1_OUT AND C.NUM_P2_OUT >= C.NUM_P3_OUT THEN C.NUM_P2_OUT
			 WHEN C.NUM_P3_OUT >= C.NUM_P1_OUT AND C.NUM_P3_OUT >= C.NUM_P2_OUT THEN C.NUM_P3_OUT END) AS NUM_MAX,
	   C.NUM_P1_IN,
	   C.NUM_P2_IN,
	   C.NUM_P3_IN,
	   (CASE WHEN C.NUM_P1_IN <= C.NUM_P2_IN AND C.NUM_P1_IN <= C.NUM_P3_IN THEN C.NUM_P1_IN
			 WHEN C.NUM_P2_IN <= C.NUM_P1_IN AND C.NUM_P2_IN <= C.NUM_P3_IN THEN C.NUM_P2_IN
			 WHEN C.NUM_P3_IN <= C.NUM_P1_IN AND C.NUM_P3_IN <= C.NUM_P2_IN THEN C.NUM_P3_IN END) AS NUM_MIN,
	   'N' AS YN_USE
FROM (SELECT B.CD_WO,
			 B.CD_ITEM,
			 B.CD_PITEM,
			 B.NO_ID,
			 B.[DO1-1] AS NUM_P1_OUT,
	         B.[DO1-2] AS NUM_P2_OUT,
	         B.[DO1-3] AS NUM_P3_OUT,
	         B.[DI2-1] AS NUM_P1_IN,
	         B.[DI2-2] AS NUM_P2_IN,
	         B.[DI2-3] AS NUM_P3_IN
	  FROM (SELECT MES.CD_WO,
				   MES.CD_ITEM,
				   MES.CD_PITEM,
				   MES.NO_ID,
				   MES.INSP_LC,
				   MES.NO_DATA  
			FROM #MES MES
			WHERE NOT EXISTS (SELECT 1 
						      FROM CZ_PR_MATCHING_DATA MD 
						      WHERE MD.CD_COMPANY = @P_CD_COMPANY
						      AND MD.CD_PLANT = @P_CD_PLANT
						      AND MD.NO_WO_C = MES.CD_WO
						      AND MD.CD_ITEM = MES.CD_ITEM
						      AND MD.CD_PITEM_C = MES.CD_PITEM
						      AND MD.NO_ID_C = MES.NO_ID)
			AND NOT EXISTS (SELECT 1 
							FROM CZ_PR_MATCHING_DEACTIVATE DA
							WHERE DA.CD_COMPANY = @P_CD_COMPANY
							AND DA.CD_PLANT = @P_CD_PLANT
							AND DA.NO_ID = MES.NO_ID)) AS A
			PIVOT (MAX(A.NO_DATA) FOR A.INSP_LC IN ([DI2-1], [DI2-2], [DI2-3], [DO1-1], [DO1-2], [DO1-3])) AS B
	  UNION ALL
	  SELECT DA.NO_WO,
	  	     DA.CD_ITEM,
	  	     DA.CD_PITEM,
	  	     DA.NO_ID,
	  	     DA.NUM_P1_OUT,
	  	     DA.NUM_P2_OUT,
	  	     DA.NUM_P3_OUT,
			 DA.NUM_P1_IN,
	  	     DA.NUM_P2_IN,
	  	     DA.NUM_P3_IN
	  FROM CZ_PR_MATCHING_DEACTIVATE DA
	  WHERE DA.CD_PLANT = @P_CD_PLANT
	  AND DA.CD_ITEM = @P_CD_ITEM
	  AND DA.CD_PITEM = @P_CD_PITEM_C
	  AND DA.STA_DEACTIVATE = '002'
	  AND NOT EXISTS (SELECT 1 
					  FROM CZ_PR_MATCHING_DATA MD 
					  WHERE MD.CD_COMPANY = @P_CD_COMPANY
					  AND MD.CD_PLANT = @P_CD_PLANT
					  AND MD.NO_WO_C = DA.NO_WO
					  AND MD.CD_ITEM = DA.CD_ITEM
					  AND MD.CD_PITEM_C = DA.CD_PITEM
					  AND MD.NO_ID_C = DA.NO_ID)) C

DELETE FROM #MES

SET @V_QUERY = 'SELECT CD_WO,
					   CD_ITEM,
					   CD_PITEM,
					   NO_ID,
					   INSP_LC,
					   NO_DATA  
			    FROM OPENQUERY(MES, ''SELECT WP.CD_WO,
									  	     RQ.CD_MITEM AS CD_ITEM,
									  	     WP.CD_PITEM,
									  	     WP.ID_NO AS NO_ID,
									  	     ID.INSP_LC,
									  	     CONVERT(NUMERIC(7, 4), WP.NO_DATA) AS NO_DATA
									  FROM [WWiMES].[dbo].[TSWO_INSP] WP
									  JOIN [WWiMES].[dbo].[TSITEMPROC_INSP] ID ON ID.CD_PLANT = WP.CD_PLANT AND ID.CD_PITEM = WP.CD_PITEM AND ID.CD_IPROC = WP.CD_IPROC AND ID.SQ_INSP = WP.SQ_INSP
									  JOIN [WWiMES].[dbo].[TSWO_REQ] RQ ON RQ.CD_PLANT = WP.CD_PLANT AND RQ.CD_WO = WP.CD_WO AND RQ.WOOP_SQ = WP.WOOP_SQ
									  WHERE WP.CD_PLANT = ''''' + @P_CD_PLANT + '''''
									  AND RQ.CD_MITEM = ''''' + @P_CD_ITEM + '''''
									  AND WP.CD_PITEM = ''''' + @P_CD_PITEM_R + '''''
									  AND ISNULL(WP.CHK_ERROR, '''''''') = ''''''''
									  AND ID.INSP_LC IN (''''DO2-1'''', ''''DO2-2'''', ''''DO2-3'''')'')'

PRINT @V_QUERY

INSERT INTO #MES
(
	CD_WO, 
	CD_ITEM, 
	CD_PITEM, 
	NO_ID, 
	INSP_LC,
	NO_DATA
)
EXEC SP_EXECUTESQL @V_QUERY

SELECT C.CD_WO AS NO_WO,
	   C.CD_ITEM,
	   C.CD_PITEM,
	   C.NO_ID,
	   C.NUM_P1,
	   C.NUM_P2,
	   C.NUM_P3,
	   (CASE WHEN C.NUM_P1 >= C.NUM_P2 AND C.NUM_P1 >= C.NUM_P3 THEN C.NUM_P1
			 WHEN C.NUM_P2 >= C.NUM_P1 AND C.NUM_P2 >= C.NUM_P3 THEN C.NUM_P2
			 WHEN C.NUM_P3 >= C.NUM_P1 AND C.NUM_P3 >= C.NUM_P2 THEN C.NUM_P3 END) AS NUM_MAX,
	   'N' AS YN_USE
FROM (SELECT B.CD_WO,
			 B.CD_ITEM,
			 B.CD_PITEM,
			 B.NO_ID,
			 B.[DO2-1] AS NUM_P1,
	         B.[DO2-2] AS NUM_P2,
	         B.[DO2-3] AS NUM_P3
	  FROM (SELECT MES.CD_WO,
				   MES.CD_ITEM,
				   MES.CD_PITEM,
				   MES.NO_ID,
				   MES.INSP_LC,
				   MES.NO_DATA  
			FROM #MES MES
			WHERE NOT EXISTS (SELECT 1 
							  FROM CZ_PR_MATCHING_DATA MD 
							  WHERE MD.CD_COMPANY = @P_CD_COMPANY
							  AND MD.CD_PLANT = @P_CD_PLANT
							  AND MD.NO_WO_R = MES.CD_WO 
							  AND MD.CD_ITEM = MES.CD_ITEM 
							  AND MD.CD_PITEM_R = MES.CD_PITEM 
							  AND MD.NO_ID_R = MES.NO_ID)
			AND NOT EXISTS (SELECT 1 
							FROM CZ_PR_MATCHING_DEACTIVATE DA
							WHERE DA.CD_COMPANY = @P_CD_COMPANY
							AND DA.CD_PLANT = @P_CD_PLANT
							AND DA.NO_ID = MES.NO_ID)) AS A
			PIVOT (MAX(A.NO_DATA) FOR A.INSP_LC IN ([DO2-1], [DO2-2], [DO2-3])) AS B
	  UNION ALL
	  SELECT DA.NO_WO,
	  	     DA.CD_ITEM,
	  	     DA.CD_PITEM,
	  	     DA.NO_ID,
			 DA.NUM_P1_IN,
	  	     DA.NUM_P2_IN,
	  	     DA.NUM_P3_IN
	  FROM CZ_PR_MATCHING_DEACTIVATE DA
	  WHERE DA.CD_PLANT = @P_CD_PLANT
	  AND DA.CD_ITEM = @P_CD_ITEM
	  AND DA.CD_PITEM = @P_CD_PITEM_R
	  AND DA.STA_DEACTIVATE = '002'
	  AND NOT EXISTS (SELECT 1 
				      FROM CZ_PR_MATCHING_DATA MD 
				      WHERE MD.CD_COMPANY = @P_CD_COMPANY
				      AND MD.CD_PLANT = @P_CD_PLANT
				      AND MD.NO_WO_R = DA.NO_WO 
				      AND MD.CD_ITEM = DA.CD_ITEM 
				      AND MD.CD_PITEM_R = DA.CD_PITEM 
				      AND MD.NO_ID_R = DA.NO_ID)) C

DROP TABLE #MES

SELECT 'N' AS S,
       CD_COMPANY,
	   CD_PLANT,
	   NO_ID_L,
	   NO_ID_C,
	   NO_ID_R,
	   STA_MATCHING,
	   CD_ITEM,
	   NO_WO_L,
	   CD_PITEM_L,
	   NUM_P1_L,
	   NUM_P2_L,
	   NUM_P3_L,
	   NUM_MIN_L,
	   NUM_C1,
	   NO_WO_C,
	   CD_PITEM_C,
	   NUM_P1_OUT_C,
	   NUM_P2_OUT_C,
	   NUM_P3_OUT_C,
	   NUM_MAX_C,
	   NUM_P1_IN_C,
	   NUM_P2_IN_C,
	   NUM_P3_IN_C,
	   NUM_MIN_C,
	   NUM_C2,
	   NO_WO_R,
	   CD_PITEM_R,
	   NUM_P1_R,
	   NUM_P2_R,
	   NUM_P3_R,
	   NUM_MAX_R,
	   NUM_MATCHING_SIZE1,
	   NUM_MATCHING_SIZE2,
	   NO_SO,
	   DC_RMK
FROM CZ_PR_MATCHING_DATA MD
WHERE MD.CD_COMPANY = @P_CD_COMPANY
AND MD.CD_PLANT = @P_CD_PLANT
AND MD.CD_ITEM = @P_CD_ITEM
AND MD.CD_PITEM_L = @P_CD_PITEM_L
AND MD.CD_PITEM_C = @P_CD_PITEM_C
AND (ISNULL(@P_CD_PITEM_R, '') = '' OR MD.CD_PITEM_R = @P_CD_PITEM_R)
AND (ISNULL(@P_STA_MATCHING, '') = '' OR 
     (@P_STA_MATCHING <> '006' AND MD.STA_MATCHING = @P_STA_MATCHING) OR 
	 (@P_STA_MATCHING = '006' AND MD.STA_MATCHING IN ('001', '002')))
ORDER BY MD.NO_ID_L ASC

GO

