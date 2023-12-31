USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[CZ_MAILBAGLIST_SH]    Script Date: 2023-05-03 오후 4:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[CZ_MAILBAGLIST_SH]
		@P_SEND_COMPANY	NVARCHAR(8),
		@P_DT_FROM		NVARCHAR(8),
	    @P_DT_TO		NVARCHAR(8)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

SELECT
	A.CD_COMPANY,
	A.NO_EMP,
	A.SEND_COMPANY,
	A.DAYS_RECORD,
	B.SEND_COMPANYNAME
FROM CZ_MAILBAGLIST_H AS A
LEFT JOIN CZ_MAILBAGLIST_COMPANY B ON A.SEND_COMPANY = B.SEND_COMPANY
WHERE A.SEND_COMPANY = @P_SEND_COMPANY
AND A.DAYS_RECORD BETWEEN @P_DT_FROM AND @P_DT_TO
GROUP BY A.CD_COMPANY,A.NO_EMP,A.DAYS_RECORD, A.SEND_COMPANY, B.SEND_COMPANYNAME
ORDER BY A.DAYS_RECORD DESC
--ASC 오름차순 DESC 내림차순