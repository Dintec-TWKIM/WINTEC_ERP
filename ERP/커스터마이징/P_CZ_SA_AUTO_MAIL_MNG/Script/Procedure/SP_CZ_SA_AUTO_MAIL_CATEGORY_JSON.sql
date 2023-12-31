USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_AUTO_MAIL_CATEGORY_JSON]    Script Date: 2017-01-05 ���� 5:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_AUTO_MAIL_CATEGORY_JSON]          
(
	@P_JSON			NVARCHAR(MAX),
	@P_ID_USER		NVARCHAR(10)	
)  
AS
   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE AC
FROM CZ_SA_AUTO_MAIL_CATEGORY AC
WHERE EXISTS (SELECT 1 
			  FROM OPENJSON(@P_JSON)
			  WITH
			  (
					CD_COMPANY      NVARCHAR(7),
					CD_PARTNER		NVARCHAR(20),
					CD_CATEGORY1	NVARCHAR(20),
					CD_CATEGORY2	NVARCHAR(20),
					JSON_FLAG		NVARCHAR(1)
			  ) JS
			  WHERE JS.JSON_FLAG = 'D'
			  AND JS.CD_COMPANY = AC.CD_COMPANY
			  AND JS.CD_PARTNER = AC.CD_PARTNER
			  AND JS.CD_CATEGORY1 = AC.CD_CATEGORY1
			  AND JS.CD_CATEGORY2 = AC.CD_CATEGORY2)

INSERT INTO CZ_SA_AUTO_MAIL_CATEGORY
(
	CD_COMPANY,
	CD_PARTNER,
	CD_CATEGORY1,
	CD_CATEGORY2,
	CD_DELIVERY,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   CD_PARTNER,
	   CD_CATEGORY1,
	   ISNULL(CD_CATEGORY2, '') AS CD_CATEGORY2,
	   ISNULL(CD_DELIVERY, '') AS CD_DELIVERY,
	   @P_ID_USER,
	   NEOE.SF_SYSDATE(GETDATE())
FROM OPENJSON(@P_JSON)
WITH
(
	CD_COMPANY		NVARCHAR(7),
	CD_PARTNER		NVARCHAR(20),
	CD_CATEGORY1	NVARCHAR(20),
	CD_CATEGORY2	NVARCHAR(20),
	CD_DELIVERY	    NVARCHAR(200),
	JSON_FLAG		NVARCHAR(1)
) JS
WHERE JS.JSON_FLAG = 'I'

GO