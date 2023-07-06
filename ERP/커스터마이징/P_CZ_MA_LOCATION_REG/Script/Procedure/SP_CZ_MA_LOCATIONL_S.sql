USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_LOCATIONL_S]    Script Date: 2016-12-05 오전 10:28:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_MA_LOCATIONL_S]        
(
	@P_CD_COMPANY		NVARCHAR(7),
	@P_CD_PLANT			NVARCHAR(7),
	@P_CD_BIZAREA		NVARCHAR(7),
	@P_CD_SL			NVARCHAR(7),
	@P_CD_LOCATION		NVARCHAR(7) = NULL
)        
AS        

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT 'N' AS S,
	   ML.CD_SL,
	   ML.CD_PLANT,
	   ML.NO_SEQ,
	   ML.CD_LOCATION,
	   ML.NM_LOCATION,
	   ML.YN_USE,
	   ML.DC_RMK 
FROM MA_LOCATION AS ML      
JOIN MA_SL SL ON SL.CD_COMPANY = ML.CD_COMPANY AND SL.CD_PLANT = ML.CD_PLANT AND SL.CD_SL = ML.CD_SL 
WHERE SL.CD_COMPANY = @P_CD_COMPANY     
AND SL.CD_BIZAREA = @P_CD_BIZAREA     
AND SL.CD_PLANT = @P_CD_PLANT
AND ML.CD_SL = @P_CD_SL
AND ML.CD_LOCATION LIKE @P_CD_LOCATION + '%'
ORDER BY ML.NO_SEQ

GO

