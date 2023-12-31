USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MM_MONTHLY_STOCK_CAL]    Script Date: 2017-01-09 오전 10:02:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_MM_MONTHLY_STOCK_CAL]
(
    @P_CD_COMPANY   NVARCHAR(7)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE CZ_MM_MONTHLY_STOCK
WHERE CD_COMPANY = @P_CD_COMPANY
AND YMM = SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6)

INSERT INTO CZ_MM_MONTHLY_STOCK
(
	CD_COMPANY,
	YMM,
	CD_ITEM,
	STAND_PRC,
	QT_INV,
	QT_AVL,
	QT_SO,
	QT_HOLD,
	QT_STOCK_MONTH,
	QT_BOOK_MONTH,
	QT_PO,
	QT_PO_MONTH,
	QT_GR,
	QT_GI,
	AM_INV,
	AM_AVL,
	AM_SO,
	AM_HOLD,
	AM_STOCK_MONTH,
	AM_BOOK_MONTH,
	AM_PO,
	AM_PO_MONTH,
	AM_GR,
	AM_GI,
	QT_BOOK_3MONTH,
	QT_BOOK_6MONTH,
	QT_BOOK_12MONTH,
	DTS_INSERT
)
SELECT MI.CD_COMPANY,
	   SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6) AS YMM,
	   MI.CD_ITEM,
	   MI.STAND_PRC,
	   MI.QT_INV,
	   MI.QT_AVL,
	   MI.QT_SO,
	   MI.QT_HOLD,
	   MI.QT_STOCK_MONTH,
	   MI.QT_BOOK_MONTH,
	   MI.QT_PO,
	   MI.QT_PO_MONTH,
	   MI.QT_GR,
	   MI.QT_GI,
	   (MI.QT_INV * MI.STAND_PRC) AS AM_INV,
	   (MI.QT_AVL * MI.STAND_PRC) AS AM_AVL,
	   (MI.QT_SO * MI.STAND_PRC) AS AM_SO,
	   MI.AM_HOLD,
	   MI.AM_STOCK_MONTH,
	   MI.AM_BOOK_MONTH,
	   MI.AM_PO,
	   MI.AM_PO_MONTH,
	   MI.AM_GR,
	   MI.AM_GI,
	   MI.QT_BOOK_3MONTH,
	   MI.QT_BOOK_6MONTH,
	   MI.QT_BOOK_12MONTH,
	   NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT
FROM (SELECT MI.CD_COMPANY,
	  	     MI.CD_ITEM,
			 MI.STAND_PRC,
	  	     (ISNULL(ML.QT_OPEN, 0) + ISNULL(IL.QT_IN, 0) - ISNULL(OL.QT_OUT, 0)) AS QT_INV,
	  	     ((ISNULL(ML.QT_OPEN, 0) + ISNULL(IL.QT_IN, 0) - ISNULL(OL.QT_OUT, 0)) - ISNULL(SB.QT_SO, 0)) AS QT_AVL,
	  	     ISNULL(SB.QT_SO, 0) AS QT_SO,
			 ISNULL(SB.QT_HOLD, 0) AS QT_HOLD,
			 ISNULL(SB.AM_HOLD, 0) AS AM_HOLD,
			 ISNULL(SB2.QT_STOCK, 0) AS QT_STOCK_MONTH,
			 ISNULL(SB2.AM_STOCK, 0) AS AM_STOCK_MONTH,
			 ISNULL(SB2.QT_BOOK, 0) AS QT_BOOK_MONTH,
			 ISNULL(SB2.AM_BOOK, 0) AS AM_BOOK_MONTH,
	  	     ISNULL(PL.QT_PO, 0) AS QT_PO,
			 ISNULL(PL1.QT_PO_MONTH, 0) AS QT_PO_MONTH,
			 ISNULL(PL.AM_PO, 0) AS AM_PO,
			 ISNULL(PL1.AM_PO_MONTH, 0) AS AM_PO_MONTH,
	  	     ISNULL(IL.QT_IN_MONTH, 0) AS QT_GR,
			 ISNULL(IL.AM_IN_MONTH, 0) AS AM_GR,
	  	     ISNULL(OL.QT_OUT_MONTH, 0) AS QT_GI,
			 ISNULL(OL.AM_OUT_MONTH, 0) AS AM_GI,
			 ISNULL(SB1.QT_BOOK_3MONTH, 0) AS QT_BOOK_3MONTH,
			 ISNULL(SB1.QT_BOOK_6MONTH, 0) AS QT_BOOK_6MONTH,
			 ISNULL(SB1.QT_BOOK_12MONTH, 0) AS QT_BOOK_12MONTH
	  FROM MA_PITEM MI
	  LEFT JOIN (SELECT SB.CD_COMPANY, SB.CD_ITEM,
						SUM(CASE WHEN LEFT(SB.DTS_INSERT, 8) >= CONVERT(NVARCHAR(8), DATEADD(MM, -3, GETDATE()), 112) THEN SB.QT_BOOK ELSE 0 END) AS QT_BOOK_3MONTH,
						SUM(CASE WHEN LEFT(SB.DTS_INSERT, 8) >= CONVERT(NVARCHAR(8), DATEADD(MM, -6, GETDATE()), 112) THEN SB.QT_BOOK ELSE 0 END) AS QT_BOOK_6MONTH,
						SUM(CASE WHEN LEFT(SB.DTS_INSERT, 8) >= CONVERT(NVARCHAR(8), DATEADD(MM, -12, GETDATE()), 112) THEN SB.QT_BOOK ELSE 0 END) AS QT_BOOK_12MONTH
				 FROM CZ_SA_STOCK_BOOK SB
				 WHERE LEFT(SB.DTS_INSERT, 8) >= CONVERT(NVARCHAR(8), DATEADD(MM, -12, GETDATE()), 112)
				 GROUP BY SB.CD_COMPANY, SB.CD_ITEM) SB1
	  ON SB1.CD_COMPANY = MI.CD_COMPANY AND SB1.CD_ITEM = MI.CD_ITEM
	  LEFT JOIN (SELECT OL.CD_COMPANY, OL.CD_ITEM,
	  				    SUM(ISNULL(OL.QT_GOOD_INV, 0) + ISNULL(OL.QT_REJECT_INV, 0) + ISNULL(OL.QT_INSP_INV, 0) + ISNULL(OL.QT_TRANS_INV, 0)) AS QT_OPEN
	  		     FROM MM_OPENQTL OL
	  		     WHERE OL.YM_STANDARD = SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 4) + '00'
				 AND OL.CD_SL = 'SL02'
	  		     GROUP BY OL.CD_COMPANY, OL.CD_ITEM) ML
	  ON ML.CD_COMPANY = MI.CD_COMPANY AND ML.CD_ITEM = MI.CD_ITEM
	  LEFT JOIN (SELECT IL.CD_COMPANY, IL.CD_ITEM,
	  			        SUM(IL.QT_IO) AS QT_IN,
	  				    SUM(CASE WHEN IL.DT_IO = SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6) THEN IL.QT_IO ELSE 0 END) AS QT_IN_MONTH,
						SUM(CASE WHEN IL.DT_IO = SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6) THEN (ISNULL(IL.UM, 0) * ISNULL(IL.QT_IO, 0)) ELSE 0 END) AS AM_IN_MONTH 
	  		     FROM (SELECT IH.CD_COMPANY, 
							  IL.CD_ITEM,
							  LEFT(IH.DT_IO, 6) AS DT_IO,
							  (CASE WHEN IH.YN_RETURN = 'Y' THEN -IL.QT_IO ELSE IL.QT_IO END) AS QT_IO,
							  IL.UM
					   FROM MM_QTIOH IH
					   JOIN MM_QTIO IL ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IO = IH.NO_IO
					   WHERE IL.FG_PS = '1'
					   AND IL.CD_SL = 'SL02'
					   AND LEFT(IH.DT_IO, 6) BETWEEN SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 4) + '01' AND SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6)) IL
	  		     GROUP BY IL.CD_COMPANY, IL.CD_ITEM) IL
	  ON IL.CD_COMPANY = MI.CD_COMPANY AND IL.CD_ITEM = MI.CD_ITEM
	  LEFT JOIN (SELECT OL.CD_COMPANY, OL.CD_ITEM,
			            SUM(OL.QT_IO) AS QT_OUT,
			     	    SUM(CASE WHEN OL.DT_IO = SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6) THEN OL.QT_IO ELSE 0 END) AS QT_OUT_MONTH,
			     	    SUM(CASE WHEN OL.DT_IO = SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6) THEN (ISNULL(OL.UM, 0) * ISNULL(OL.QT_IO, 0)) ELSE 0 END) AS AM_OUT_MONTH 
			     FROM (SELECT OH.CD_COMPANY, 
			     			  OL.CD_ITEM,
			     			  LEFT(OH.DT_IO, 6) AS DT_IO,
			     			  (CASE WHEN OH.YN_RETURN = 'Y' THEN -OL.QT_IO ELSE OL.QT_IO END) AS QT_IO,
			     			  OL.UM
			     	   FROM MM_QTIOH OH
			     	   JOIN MM_QTIO OL ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
			     	   WHERE OL.FG_PS = '2'
					   AND OL.CD_SL = 'SL02'
					   AND LEFT(OH.DT_IO, 6) BETWEEN SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 4) + '01' AND SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6)) OL
			     GROUP BY OL.CD_COMPANY, OL.CD_ITEM) OL
	  ON OL.CD_COMPANY = MI.CD_COMPANY AND OL.CD_ITEM = MI.CD_ITEM
	  LEFT JOIN (SELECT SB.CD_COMPANY, SB.CD_ITEM,
						SUM(SB.QT_HOLD) AS QT_HOLD,
						SUM(ISNULL(SB.QT_HOLD, 0) * ISNULL(UM_KR, 0)) AS AM_HOLD,
	  				    SUM(ISNULL(SB.QT_BOOK, 0) - (ISNULL(EL.QT_PACK, 0) + ISNULL(OL.QT_OUT, 0))) AS QT_SO
	  		     FROM CZ_SA_STOCK_BOOK SB
				 LEFT JOIN (SELECT EL.CD_COMPANY, EL.NO_SO, EL.SEQ_SO,
				 				   SUM(OL.QT_IO) AS QT_PACK 
				 		    FROM MM_GIREQL EL
				 		    JOIN (SELECT OL.CD_COMPANY, OL.NO_ISURCV, OL.NO_ISURCVLINE,
				 		    	  		 SUM(OL.QT_IO) AS QT_IO 
				 		    	  FROM MM_QTIO OL
				 		    	  WHERE OL.FG_PS = '2'
								  AND EXISTS (SELECT 1 
											  FROM MM_QTIOH OH
											  WHERE OH.CD_COMPANY = OL.CD_COMPANY
											  AND OH.NO_IO = OL.NO_IO
											  AND LEFT(OH.DT_IO, 6) <= SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6))
				 		    	  GROUP BY OL.CD_COMPANY, OL.NO_ISURCV, OL.NO_ISURCVLINE) OL
				 		    ON OL.CD_COMPANY = EL.CD_COMPANY AND OL.NO_ISURCV = EL.NO_GIREQ AND OL.NO_ISURCVLINE = EL.NO_LINE
							WHERE EL.CD_SL = 'SL02' 
							AND EL.CD_GRSL = 'SL03'
				 		    GROUP BY EL.CD_COMPANY, EL.NO_SO, EL.SEQ_SO) EL
				 ON EL.CD_COMPANY = SB.CD_COMPANY AND EL.NO_SO = SB.NO_FILE AND EL.SEQ_SO = SB.NO_LINE
				 LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO,
				 		    	   SUM(GL.QT_GIR_STOCK) AS QT_OUT
				 		    FROM SA_GIRL GL
				 		    JOIN (SELECT OL.CD_COMPANY, OL.NO_ISURCV, OL.NO_ISURCVLINE,
				 		    	  	     SUM(OL.QT_IO) AS QT_IO 
				 		    	  FROM MM_QTIO OL
				 		    	  LEFT JOIN MM_QTIOH OH ON OH.CD_COMPANY = OL.CD_COMPANY AND OH.NO_IO = OL.NO_IO
				 		    	  WHERE OL.FG_PS = '2'
								  AND LEFT(OH.DT_IO, 6) <= SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6)
				 		    	  GROUP BY OL.CD_COMPANY, OL.NO_ISURCV, OL.NO_ISURCVLINE) OL
				 		    ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
				 		    WHERE ISNULL(GL.QT_GIR_STOCK, 0) > 0
				 			AND GL.YN_GI_STOCK <> 'P'
				 			AND OL.QT_IO > 0
				 		    GROUP BY GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO) OL
				 ON OL.CD_COMPANY = SB.CD_COMPANY AND OL.NO_SO = SB.NO_FILE AND OL.SEQ_SO = SB.NO_LINE
				 WHERE LEFT(SB.DTS_INSERT, 6) <= SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6)
	  		     GROUP BY SB.CD_COMPANY, SB.CD_ITEM) SB
	  ON SB.CD_COMPANY = MI.CD_COMPANY AND SB.CD_ITEM = MI.CD_ITEM
	  LEFT JOIN (SELECT SB.CD_COMPANY, SB.CD_ITEM,
					    SUM(SB.QT_STOCK) AS QT_STOCK,
	  				    SUM(SB.QT_BOOK) AS QT_BOOK,
						SUM(ISNULL(SB.QT_STOCK, 0) * ISNULL(SB.UM_KR, 0)) AS AM_STOCK,
						SUM(SB.AM_KR) AS AM_BOOK
	  		     FROM CZ_SA_STOCK_BOOK SB
				 WHERE LEFT(SB.DTS_INSERT, 6) = SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6)
	  		     GROUP BY SB.CD_COMPANY, SB.CD_ITEM) SB2
	  ON SB2.CD_COMPANY = MI.CD_COMPANY AND SB2.CD_ITEM = MI.CD_ITEM
	  LEFT JOIN (SELECT PL.CD_COMPANY, PL.CD_ITEM,
	  		   		    SUM(ISNULL(PL.QT_PO, 0) - ISNULL(IL.QT_IO, 0)) AS QT_PO,
						SUM((ISNULL(PL.QT_PO, 0) - ISNULL(IL.QT_IO, 0)) * ISNULL(PL.UM, 0)) AS AM_PO
	  		     FROM PU_POH PH
	  		     LEFT JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PH.NO_PO = PL.NO_PO
				 LEFT JOIN (SELECT IL.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT,
								   SUM(IL.QT_IO) AS QT_IO 
							FROM MM_QTIOH IH
							LEFT JOIN MM_QTIO IL ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IO = IH.NO_IO
							WHERE (IH.YN_RETURN = 'N' OR IH.YN_RETURN IS NULL) 
							AND LEFT(IH.DT_IO, 6) <= SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6)
							AND IL.FG_PS = '1'
							GROUP BY IL.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT) IL
				 ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
	  		     WHERE (PH.CD_TPPO = '1300'
					 OR PH.CD_TPPO = '1400'
					 OR PH.CD_TPPO = '2300'
					 OR PH.CD_TPPO = '2400') -- 1300, 1400(무상) :재고매입(국내), 2300, 2400(무상) :재고매입(해외)
				 AND LEFT(PH.DT_PO, 6) <= SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6)
	  		     GROUP BY PL.CD_COMPANY, PL.CD_ITEM) PL
	  ON PL.CD_COMPANY = MI.CD_COMPANY AND PL.CD_ITEM = MI.CD_ITEM
	  LEFT JOIN (SELECT PL.CD_COMPANY, PL.CD_ITEM,
						SUM(PL.QT_PO) AS QT_PO_MONTH,
						SUM(PL.AM) AS AM_PO_MONTH
	  		     FROM PU_POH PH
	  		     LEFT JOIN PU_POL PL ON PL.CD_COMPANY = PH.CD_COMPANY AND PH.NO_PO = PL.NO_PO
	  		     WHERE (PH.CD_TPPO = '1300'
					 OR PH.CD_TPPO = '1400'
					 OR PH.CD_TPPO = '2300'
					 OR PH.CD_TPPO = '2400') -- 1300, 1400(무상) :재고매입(국내), 2300, 2400(무상) :재고매입(해외)
				 AND LEFT(PH.DT_PO, 6) = SUBSTRING(CONVERT(NVARCHAR(8), DATEADD(MM, -1, GETDATE()), 112), 1, 6)
	  		     GROUP BY PL.CD_COMPANY, PL.CD_ITEM) PL1
	  ON PL1.CD_COMPANY = MI.CD_COMPANY AND PL1.CD_ITEM = MI.CD_ITEM
	  WHERE MI.CD_COMPANY = @P_CD_COMPANY
	  AND (MI.CLS_ITEM = '009' OR MI.CLS_ITEM = '013')) MI

GO