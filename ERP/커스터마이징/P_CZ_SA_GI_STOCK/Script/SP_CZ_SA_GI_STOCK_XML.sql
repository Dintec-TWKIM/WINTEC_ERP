USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GI_STOCK_XML]    Script Date: 2016-01-25 오후 7:02:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_SA_GI_STOCK_XML] 
(
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10), 
    @DOC			INT = NULL
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML 

-- ================================================== UPDATE    
UPDATE A 
   SET A.YN_GI_STOCK = B.YN_GI_STOCK,
	   A.ID_GI_STOCK = @P_ID_USER,
	   A.DTS_GI_STOCK = NEOE.SF_SYSDATE(GETDATE()),
	   A.ID_UPDATE = @P_ID_USER,
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM SA_GIRL A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY		NVARCHAR(7),
				NO_GIR			NVARCHAR(20),
				SEQ_GIR			NUMERIC(5, 0),
				YN_GI_STOCK		NVARCHAR(1),
				TP_GIR			NVARCHAR(3)) B 
  ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_GIR = B.NO_GIR AND A.SEQ_GIR = B.SEQ_GIR AND B.TP_GIR = '001'

UPDATE A 
   SET A.YN_GI_STOCK = B.YN_GI_STOCK,
	   A.ID_GI_STOCK = @P_ID_USER,
	   A.DTS_GI_STOCK = NEOE.SF_SYSDATE(GETDATE()),
	   A.ID_UPDATE = @P_ID_USER,
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_SA_GIRL_PACK A 
  JOIN OPENXML (@DOC, '/XML/U', 2) 
          WITH (CD_COMPANY   NVARCHAR(7),
				NO_GIR	     NVARCHAR(20),
			    SEQ_GIR      NUMERIC(5, 0),
				YN_GI_STOCK  NVARCHAR(1),
				TP_GIR		 NVARCHAR(3)) B 
  ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_GIR = B.NO_GIR AND A.SEQ_GIR = B.SEQ_GIR AND B.TP_GIR = '002'

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO