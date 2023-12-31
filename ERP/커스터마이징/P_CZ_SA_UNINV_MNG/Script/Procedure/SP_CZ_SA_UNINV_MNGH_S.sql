USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_TRCREDITSCH_S]    Script Date: 2016-01-29 오전 11:42:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_SA_UNINV_MNGH_S]
( 
	@P_CD_COMPANY			NVARCHAR(7),
	@P_NO_SO				NVARCHAR(20),
	@P_DT_FROM				NVARCHAR(8),
	@P_DT_TO				NVARCHAR(8),
	@P_NO_PO_PARTNER		NVARCHAR(50),
	@P_CD_PARTNER			NVARCHAR(20),
	@P_NO_IMO				NVARCHAR(10),
	@P_YN_NOT_IV			NVARCHAR(1),
	@P_YN_NOT_INV		    NVARCHAR(1),
	@P_YN_FREE				NVARCHAR(1),
	@P_YN_ZERO				NVARCHAR(1)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT SH.NO_SO,
	   SH.NO_PO_PARTNER,
	   STUFF((SELECT DISTINCT ',' + CH.NO_CLAIM
	   	      FROM CZ_SA_CLAIMH CH
	   	      WHERE CH.CD_COMPANY = SH.CD_COMPANY
	   	      AND (CH.NO_SO = SH.NO_SO OR CH.NO_CLAIM = SH.NO_SO)
	   	      FOR XML PATH('')),1,1,'') AS NO_CLAIM,
	   SH.DT_SO,
	   ME.NM_KOR AS NM_EMP,
	   MP.LN_PARTNER,
	   MH.NM_VESSEL,
	   SH.TP_SO,
	   ISNULL(SL.AM_EX_SO, 0) AS AM_EX_SO,
	   ISNULL(SL.AM_SO, 0) AS AM_SO,
	   ISNULL(SL.AM_EX_OUT, 0) AS AM_EX_OUT,
	   ISNULL(SL.AM_OUT, 0) AS AM_OUT,
	   ISNULL(SL.AM_EX_FREE, 0) AS AM_EX_FREE,
	   ISNULL(SL.AM_FREE, 0) AS AM_FREE,
	   ISNULL(SL.AM_EX_RETURN, 0) AS AM_EX_RETURN,
	   ISNULL(SL.AM_RETURN, 0) AS AM_RETURN,
	   ISNULL(SL.AM_EX_RETURN_FREE, 0) AS AM_EX_RETURN_FREE,
	   ISNULL(SL.AM_RETURN_FREE, 0) AS AM_RETURN_FREE,
	   ISNULL(SL.AM_EX_IV, 0) AS AM_EX_IV,
	   ISNULL(SL.AM_IV, 0) AS AM_IV,
	   ISNULL(SL.AM_EX_IV_RETURN, 0) AS AM_EX_IV_RETURN,
	   ISNULL(SL.AM_IV_RETURN, 0) AS AM_IV_RETURN,
	   ISNULL(IL1.AM_EX_CLS, 0) AS AM_EX_AD,
	   ISNULL(IL1.AM_CLS, 0) AS AM_AD,
	   (ISNULL(SL.AM_EX_OUT, 0) - (ISNULL(SL.AM_EX_IV, 0) + ISNULL(SL.AM_EX_FREE, 0))) AS AM_EX_REMAIN,
	   (ISNULL(SL.AM_OUT, 0) - (ISNULL(SL.AM_IV, 0) + ISNULL(SL.AM_FREE, 0))) AS AM_REMAIN,
	   ISNULL(SL.QT_SO, 0) AS QT_SO,
	   ISNULL(SL.QT_OUT, 0) AS QT_OUT,
	   ISNULL(SL.QT_RETURN, 0) AS QT_RETURN,
	   ISNULL(SL.QT_IV, 0) AS QT_IV,
	   ISNULL(SL.QT_IV_RETURN, 0) AS QT_IV_RETURN,
	   SH.DC_RMK1,
	   SH.DC_RMK_CONTRACT AS DC_RMK
FROM SA_SOH SH
LEFT JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
				  SUM(SL.QT_SO) AS QT_SO,
		   		  SUM(ISNULL(SL.AM_KR_S, SL.AM_WONAMT)) AS AM_SO,
		   		  SUM(ISNULL(SL.AM_EX_S, SL.AM_SO)) AS AM_EX_SO,
				  SUM(OL.QT_OUT) AS QT_OUT,
				  SUM(OL.AM_EX_OUT) AS AM_EX_OUT,
				  SUM(OL.AM_OUT) AS AM_OUT,
				  SUM(OL.AM_EX_FREE) AS AM_EX_FREE,
				  SUM(OL.AM_FREE) AS AM_FREE,
				  SUM(OL.QT_RETURN) AS QT_RETURN,
				  SUM(OL.AM_EX_RETURN) AS AM_EX_RETURN,
				  SUM(OL.AM_RETURN) AS AM_RETURN,
				  SUM(OL.AM_EX_RETURN_FREE) AS AM_EX_RETURN_FREE,
				  SUM(OL.AM_RETURN_FREE) AS AM_RETURN_FREE,
				  SUM(OL.QT_IV) AS QT_IV,
				  SUM(OL.AM_EX_IV) AS AM_EX_IV,
				  SUM(OL.AM_IV) AS AM_IV,
				  SUM(OL.QT_IV_RETURN) AS QT_IV_RETURN,
				  SUM(OL.AM_EX_IV_RETURN) AS AM_EX_IV_RETURN,
				  SUM(OL.AM_IV_RETURN) AS AM_IV_RETURN
		   FROM SA_SOL SL
		   LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'N' THEN OL.QT_IO ELSE 0 END) AS QT_OUT,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'N' THEN OL.AM_EX ELSE 0 END) AS AM_EX_OUT,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'N' THEN OL.AM ELSE 0 END) AS AM_OUT,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'N' AND ISNULL(OL.YN_AM, 'N') = 'N' THEN OL.AM_EX ELSE 0 END) AS AM_EX_FREE,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'N' AND ISNULL(OL.YN_AM, 'N') = 'N' THEN OL.AM ELSE 0 END) AS AM_FREE,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'Y' THEN OL.QT_IO ELSE 0 END) AS QT_RETURN,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'Y' THEN OL.AM_EX ELSE 0 END) AS AM_EX_RETURN,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'Y' THEN OL.AM ELSE 0 END) AS AM_RETURN,
							 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'Y' AND ISNULL(OL.YN_AM, 'N') = 'N' THEN OL.AM_EX ELSE 0 END) AS AM_EX_RETURN_FREE,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'Y' AND ISNULL(OL.YN_AM, 'N') = 'N' THEN OL.AM ELSE 0 END) AS AM_RETURN_FREE,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'N' THEN IL.QT_CLS ELSE 0 END) AS QT_IV,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'N' THEN IL.AM_EX_CLS ELSE 0 END) AS AM_EX_IV,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'N' THEN IL.AM_CLS ELSE 0 END) AS AM_IV,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'Y' THEN IL.QT_CLS ELSE 0 END) AS QT_IV_RETURN,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'Y' THEN IL.AM_EX_CLS ELSE 0 END) AS AM_EX_IV_RETURN,
		   			  		 SUM(CASE WHEN ISNULL(OH.YN_RETURN, 'N') = 'Y' THEN IL.AM_CLS ELSE 0 END) AS AM_IV_RETURN
		   			  FROM MM_QTIO OL
		   			  LEFT JOIN MM_QTIOH OH ON OH.CD_COMPANY = OL.CD_COMPANY AND OH.NO_IO = OL.NO_IO
		   			  LEFT JOIN MM_EJTP TP ON TP.CD_COMPANY = OL.CD_COMPANY AND TP.CD_QTIOTP = OL.CD_QTIOTP
		   			  LEFT JOIN (SELECT CD_COMPANY, NO_IO, NO_IOLINE, CD_ITEM,
		   			  					SUM(QT_CLS) AS QT_CLS,
		   			  					SUM(AM_EX_CLS) AS AM_EX_CLS,
		   			  					SUM(AM_CLS) AS AM_CLS 
		   			  			 FROM SA_IVL
		   			  			 GROUP BY CD_COMPANY, NO_IO, NO_IOLINE, CD_ITEM) IL 
		   			  ON IL.CD_COMPANY = OL.CD_COMPANY AND IL.NO_IO = OL.NO_IO AND IL.NO_IOLINE = OL.NO_IOLINE AND IL.CD_ITEM = OL.CD_ITEM
		   			  WHERE FG_PS = '2'
					  GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT, OL.NO_PSOLINE_MGMT) OL 
		   ON OL.CD_COMPANY = SL.CD_COMPANY AND OL.NO_PSO_MGMT = SL.NO_SO AND OL.NO_PSOLINE_MGMT = SL.SEQ_SO
		   GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN (SELECT IL.CD_COMPANY, IL.CD_PJT,
		   		  SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.AM_CLS ELSE IL.AM_CLS END) AS AM_CLS,
				  SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -IL.AM_EX_CLS ELSE IL.AM_EX_CLS END) AS AM_EX_CLS
		   FROM SA_IVL IL
		   WHERE IL.CD_PJT <> IL.NO_SO
		   GROUP BY IL.CD_COMPANY, IL.CD_PJT) IL1
ON IL1.CD_COMPANY = SH.CD_COMPANY AND IL1.CD_PJT = SH.NO_SO
LEFT JOIN SA_TPSO ST ON ST.CD_COMPANY = SH.CD_COMPANY AND ST.TP_SO = SH.TP_SO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
WHERE SH.CD_COMPANY = @P_CD_COMPANY
AND SH.DT_SO BETWEEN @P_DT_FROM AND @P_DT_TO
AND (ISNULL(@P_NO_SO, '') = '' OR SH.NO_SO = @P_NO_SO)
AND (ISNULL(@P_NO_PO_PARTNER, '') = '' OR SH.NO_PO_PARTNER = @P_NO_PO_PARTNER)
AND (ISNULL(@P_CD_PARTNER, '') = '' OR SH.CD_PARTNER = @P_CD_PARTNER)
AND (ISNULL(@P_NO_IMO, '') = '' OR SH.NO_IMO = @P_NO_IMO)
AND (ISNULL(@P_YN_NOT_IV, 'N') = 'N' OR ISNULL(SL.QT_SO, 0) > ISNULL(SL.QT_IV, 0))
AND (ISNULL(@P_YN_NOT_INV, 'N') = 'N' OR ISNULL(SL.QT_OUT, 0) > ISNULL(SL.QT_IV, 0))
AND (ISNULL(@P_YN_FREE, 'N') = 'N' OR EXISTS (SELECT 1 
										      FROM MM_QTIO OL
											  LEFT JOIN MM_QTIOH OH ON OH.CD_COMPANY = OL.CD_COMPANY AND OH.NO_IO = OL.NO_IO
											  JOIN SA_SOL SL ON SL.CD_COMPANY = OL.CD_COMPANY AND SL.NO_SO = OL.NO_PSO_MGMT AND SL.SEQ_SO = OL.NO_PSOLINE_MGMT 
											  WHERE OL.CD_COMPANY = SH.CD_COMPANY
											  AND OL.NO_PSO_MGMT = SH.NO_SO
											  AND OL.FG_IO IN ('010', '075')
											  AND ISNULL(OL.YN_AM, 'N') = 'N'))
AND (ISNULL(@P_YN_ZERO, 'N') = 'N' 
OR ((ISNULL(SL.AM_EX_OUT, 0) - ISNULL(SL.AM_EX_IV, 0)) > 0 AND (ISNULL(SL.AM_OUT, 0) - (ISNULL(SL.AM_IV, 0) + ISNULL(SL.AM_FREE, 0))) > 0))

GO