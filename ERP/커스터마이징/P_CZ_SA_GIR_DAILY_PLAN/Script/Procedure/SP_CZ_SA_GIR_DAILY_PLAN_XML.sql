USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIR_DAILY_PLAN_XML]    Script Date: 12/2/2015 10:10:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIR_DAILY_PLAN_XML] 
(
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10), 
    @DOC			INT = NULL
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML 

-- ================================================== DELETE
DELETE A 
FROM CZ_SA_GIR_PLAN A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (TP_PLAN	NVARCHAR(4),
			  SEQ_PLAN	NUMERIC(18, 0)) B 
ON A.TP_PLAN = B.TP_PLAN
AND A.SEQ_PLAN = B.SEQ_PLAN
-- ================================================== INSERT
INSERT INTO CZ_SA_GIR_PLAN 
(
	TP_PLAN,
    SEQ_PLAN,
    DT_PLAN,
    NO_EMP,
    CD_COMPANY,
    NO_GIR,
    DC_RMK,
    ID_INSERT,
    DTS_INSERT
)
SELECT TP_PLAN,
       SEQ_PLAN,
       DT_PLAN,
       NO_EMP,
       CD_COMPANY,
       NO_GIR,
       DC_RMK,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (TP_PLAN				NVARCHAR(4), 
				SEQ_PLAN			NUMERIC(18, 0),
				DT_PLAN				NVARCHAR(8),
				NO_EMP				NVARCHAR(10), 
				CD_COMPANY		    NVARCHAR(7), 
				NO_GIR				NVARCHAR(20), 
				DC_RMK				NVARCHAR(500)); 
-- ================================================== UPDATE
MERGE INTO CZ_SA_GIR_PLAN AS T 
      USING (SELECT *
             FROM OPENXML (@DOC, '/XML/U', 2) 
                  WITH (TP_PLAN				NVARCHAR(4), 
				        SEQ_PLAN			NUMERIC(18, 0),
				        DT_PLAN				NVARCHAR(8),
				        NO_EMP				NVARCHAR(10), 
				        CD_COMPANY		    NVARCHAR(7), 
				        NO_GIR				NVARCHAR(20), 
				        DC_RMK				NVARCHAR(500))) S
	  ON T.TP_PLAN = S.TP_PLAN
      AND T.SEQ_PLAN = S.SEQ_PLAN
      WHEN MATCHED THEN
        UPDATE SET T.DT_PLAN = S.DT_PLAN,
                   T.NO_EMP = S.NO_EMP,
                   T.CD_COMPANY = S.CD_COMPANY,
                   T.NO_GIR = S.NO_GIR,
                   T.DC_RMK = S.DC_RMK,
	               T.ID_UPDATE = @P_ID_USER, 
	               T.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
      WHEN NOT MATCHED THEN 
        INSERT (
	                TP_PLAN,
                    SEQ_PLAN,
                    DT_PLAN,
                    NO_EMP,
                    CD_COMPANY,
                    NO_GIR,
                    DC_RMK,
                    ID_INSERT,
                    DTS_INSERT
                )
		VALUES (
                    S.TP_PLAN,
                    S.SEQ_PLAN,
                    S.DT_PLAN,
                    S.NO_EMP,
                    S.CD_COMPANY,
                    S.NO_GIR,
                    S.DC_RMK,
                    @P_ID_USER, 
                    NEOE.SF_SYSDATE(GETDATE()) 
               );

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO