USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_PTR_PLAN_XML]    Script Date: 2015-10-29 오전 11:13:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_SA_PTR_PLAN_XML] 
(
	@P_XML			XML, 
	@P_CD_COMPANY	NVARCHAR(7), 
	@P_ID_USER		NVARCHAR(10), 
    @DOC			INT = NULL
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML 

-- ================================================== DELETE
DELETE A 
  FROM CZ_SA_PTR_PLAN A 
       JOIN OPENXML (@DOC, '/XML/D', 2) 
               WITH (YY_PLAN	  NVARCHAR(4),
					 TP_PLAN	  NVARCHAR(1),
					 CD_KEY		  NVARCHAR(20)) B 
       ON A.CD_COMPANY = @P_CD_COMPANY
	   AND A.YY_PLAN = B.YY_PLAN
	   AND A.TP_PLAN = B.TP_PLAN
	   AND A.CD_KEY = B.CD_KEY
-- ================================================== INSERT
INSERT INTO CZ_SA_PTR_PLAN 
(
	CD_COMPANY,
	YY_PLAN,
	TP_PLAN,
	CD_KEY,
	AM_TOTWON,
	AM_TOT_JAN,
	AM_TOT_FEB,
	AM_TOT_MAR,
	AM_TOT_APR,
	AM_TOT_MAY,
	AM_TOT_JUN,
	AM_TOT_JUL,
	AM_TOT_AUG,
	AM_TOT_SEP,
	AM_TOT_OCT,
	AM_TOT_NOV,
	AM_TOT_DEC,
	ID_INSERT,
	DTS_INSERT
)
SELECT @P_CD_COMPANY,
	   YY_PLAN,
	   TP_PLAN,
	   CD_KEY,
	   AM_TOTWON,
	   AM_TOT_JAN,
	   AM_TOT_FEB,
	   AM_TOT_MAR,
	   AM_TOT_APR,
	   AM_TOT_MAY,
	   AM_TOT_JUN,
	   AM_TOT_JUL,
	   AM_TOT_AUG,
	   AM_TOT_SEP,
	   AM_TOT_OCT,
	   AM_TOT_NOV,
	   AM_TOT_DEC,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (YY_PLAN			NVARCHAR(4),
			    TP_PLAN			NVARCHAR(1),
			    CD_KEY			NVARCHAR(20),
			    AM_TOTWON		NUMERIC(17, 4),
			    AM_TOT_JAN		NUMERIC(17, 4),
			    AM_TOT_FEB		NUMERIC(17, 4),
			    AM_TOT_MAR		NUMERIC(17, 4),
			    AM_TOT_APR		NUMERIC(17, 4),
			    AM_TOT_MAY		NUMERIC(17, 4),
			    AM_TOT_JUN		NUMERIC(17, 4),
			    AM_TOT_JUL		NUMERIC(17, 4),
			    AM_TOT_AUG		NUMERIC(17, 4),
			    AM_TOT_SEP		NUMERIC(17, 4),
			    AM_TOT_OCT		NUMERIC(17, 4),
			    AM_TOT_NOV		NUMERIC(17, 4),
			    AM_TOT_DEC		NUMERIC(17, 4)) 
-- ================================================== UPDATE    
UPDATE A 
   SET A.AM_TOTWON = B.AM_TOTWON,
	   A.AM_TOT_JAN = B.AM_TOT_JAN,
	   A.AM_TOT_FEB = B.AM_TOT_FEB,
	   A.AM_TOT_MAR = B.AM_TOT_MAR,
	   A.AM_TOT_APR = B.AM_TOT_APR,
	   A.AM_TOT_MAY = B.AM_TOT_MAY,
	   A.AM_TOT_JUN = B.AM_TOT_JUN,
	   A.AM_TOT_JUL = B.AM_TOT_JUL,
	   A.AM_TOT_AUG = B.AM_TOT_AUG,
	   A.AM_TOT_SEP = B.AM_TOT_SEP,
	   A.AM_TOT_OCT = B.AM_TOT_OCT,
	   A.AM_TOT_NOV = B.AM_TOT_NOV,
	   A.AM_TOT_DEC = B.AM_TOT_DEC,
	   A.ID_UPDATE = @P_ID_USER, 
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_SA_PTR_PLAN A 
       JOIN OPENXML (@DOC, '/XML/U', 2) 
               WITH (YY_PLAN		NVARCHAR(4),
					 TP_PLAN		NVARCHAR(1),
					 CD_KEY			NVARCHAR(20),
					 AM_TOTWON		NUMERIC(17, 4),
					 AM_TOT_JAN		NUMERIC(17, 4),
					 AM_TOT_FEB		NUMERIC(17, 4),
					 AM_TOT_MAR		NUMERIC(17, 4),
					 AM_TOT_APR		NUMERIC(17, 4),
					 AM_TOT_MAY		NUMERIC(17, 4),
					 AM_TOT_JUN		NUMERIC(17, 4),
					 AM_TOT_JUL		NUMERIC(17, 4),
					 AM_TOT_AUG		NUMERIC(17, 4),
					 AM_TOT_SEP		NUMERIC(17, 4),
					 AM_TOT_OCT		NUMERIC(17, 4),
					 AM_TOT_NOV		NUMERIC(17, 4),
					 AM_TOT_DEC		NUMERIC(17, 4)) B 
       ON A.CD_COMPANY = @P_CD_COMPANY
	   AND A.YY_PLAN = B.YY_PLAN
	   AND A.TP_PLAN = B.TP_PLAN
	   AND A.CD_KEY = B.CD_KEY

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO