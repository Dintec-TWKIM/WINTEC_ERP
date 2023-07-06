USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_PBL_DELETE]    Script Date: 2022-03-24 ���� 6:09:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_PU_PBL_DELETE]  
(  
	 @P_CD_COMPANY  NVARCHAR(7),  
	 @P_NO_PO       NVARCHAR(20),  
	 @P_NO_POLINE   NUMERIC(5,0),
	 @P_NO_SEQ      NUMERIC(5,0)
)   
AS  

  
DELETE	
FROM	PU_PBL
WHERE	CD_COMPANY  = @P_CD_COMPANY 
AND     NO_POLINE = @P_NO_POLINE
AND		NO_PO = @P_NO_PO 
AND		NO_SEQ = @P_NO_SEQ
GO


