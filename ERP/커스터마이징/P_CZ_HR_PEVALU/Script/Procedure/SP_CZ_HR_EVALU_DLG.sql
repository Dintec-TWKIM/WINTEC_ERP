USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_HR_EVALU_DLG]    Script Date: 2015-11-17 오전 10:07:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************  
**  System  : 인사관리  
**  Sub System : 고과관리  
**  Page  : 평가표  
**  Desc  : 관련정보 조회  
**    
**  Return Values  
**  
**  작    성    자  : 강연철  
**  작    성    일 : 2015.01.20  
**  수    정    자  :   
**  수  정  내  용  :   
*********************************************  
** Change History  
*********************************************  
*********************************************/  
  
ALTER PROCEDURE [NEOE].[SP_CZ_HR_EVALU_DLG]  
(  
	@P_CD_COMPANY	NVARCHAR(7),  
	@P_NO_EMP		NVARCHAR(10),
	@P_ST_DAY		NVARCHAR(6),
	@P_DT_DAY		NVARCHAR(6)
)  
AS  

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT *
FROM (SELECT A.NO_EMP,
			 B.NM_EDU, 
			 B.DC_EDU, 
			 B.NM_LOC,
			 B.DT_FROM,
			 B.DT_TO,
			 A.CD_EDU,		
			 A.CD_PASS, 
			 D.NM_SYSDEF AS NM_PASS, 
			 (ISNULL(A.PT_ASSIGN, 0) + ISNULL(A.PT_ATTEND, 0) + ISNULL(A.PT_ATTIT, 0)) AS PT_EDU								
	  FROM HR_HUEDUPER AS A
	  JOIN HR_HUEDUCODE AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.YY_EDU = B.YY_EDU AND A.CD_EDU = B.CD_EDU AND SUBSTRING(B.DT_FROM, 1, 6) >= @P_ST_DAY AND SUBSTRING(B.DT_TO, 1, 6) <= @P_DT_DAY
	  LEFT JOIN MA_CODEDTL AS D ON A.CD_COMPANY	= D.CD_COMPANY AND A.CD_PASS = D.CD_SYSDEF AND D.CD_FIELD = 'HR_H000045'
	  WHERE	A.CD_COMPANY = @P_CD_COMPANY
	  AND A.NO_EMP = @P_NO_EMP
	  UNION
	  SELECT NO_EMP,
			 NM_EDU,
			 DC_EDU,
			 NM_LOC,
			 DT_FROM,
			 DT_TO,
			 '' AS CD_EDU,
			 '' AS CD_PASS,
			 '' AS NM_PASS,
			 (ISNULL(PT_ASSIGN, 0) + ISNULL(PT_ATTEND, 0) + ISNULL(PT_ATTIT, 0)) AS PT_EDU			
	  FROM HR_HUEDU_HS
	  WHERE	CD_COMPANY = @P_CD_COMPANY
	  AND NO_EMP = @P_NO_EMP
	  AND SUBSTRING(DT_FROM, 1, 6) >= @P_ST_DAY
	  AND SUBSTRING(DT_TO, 1, 6) <= @P_DT_DAY) T
ORDER BY T.DT_FROM ASC, T.CD_EDU ASC

GO