USE [NeoBizboxS2]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CUST_REL_MNGH_S]    Script Date: 2017-05-23 오전 9:44:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [BX].[SP_CZ_SA_CRM_EMP_SCHEDULE_XML]
(
	@P_XML			XML, 
	@P_NO_EMP		NVARCHAR(10), 
    @DOC			INT = NULL
) 
AS
 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML

DECLARE @V_ID_USER INT
DECLARE @V_SCHM_ID INT
DECLARE @V_SCHD_ID INT
DECLARE @V_SCH_NM NVARCHAR(500),
	    @V_SCH_PLACE NVARCHAR(MAX),
	    @V_DT_START NVARCHAR(8),
	    @V_DT_HOUR_START NVARCHAR(2),
	    @V_DT_MINUTE_START NVARCHAR(2),
	    @V_DT_END NVARCHAR(8),
	    @V_DT_HOUR_END NVARCHAR(2),
	    @V_DT_MINUTE_END NVARCHAR(2),
	    @V_SCH_STS NVARCHAR(5),
	    @V_ALL_DAY_YN CHAR(1),
	    @V_CONTENTS NVARCHAR(MAX),
	    @V_WORK_CONTENTS NVARCHAR(MAX)

SELECT @V_ID_USER = USER_ID
FROM BX.TCMG_USER
WHERE LOGON_CD = @P_NO_EMP

-- ================================================== DELETE
UPDATE A
SET A.DEL_YN = '1',
	A.MODIFY_BY = @V_ID_USER,
	A.MODIFY_DT = GETDATE()
FROM BX.TSOG_SCH_D A 
JOIN OPENXML (@DOC, '/XML/ROW', 2) 
        WITH (SCHD_ID INT,
			  XML_FLAG NVARCHAR(1)) B 
ON A.SCHD_ID = B.SCHD_ID
AND B.XML_FLAG = 'D'

UPDATE A
SET A.DEL_YN = '1',
	A.MODIFY_BY = @V_ID_USER,
	A.MODIFY_DT = GETDATE() 
FROM BX.TSOG_SCH_M A 
JOIN OPENXML (@DOC, '/XML/ROW', 2) 
        WITH (SCHM_ID INT,
			  XML_FLAG NVARCHAR(1)) B 
ON A.SCHM_ID = B.SCHM_ID
AND B.XML_FLAG = 'D'
-- ================================================== INSERT
DECLARE CUR_CZ_SA_CRM_EMP_SCHEDULE CURSOR FOR
SELECT SCH_NM,
	   SCH_PLACE,
	   DT_START,
	   DT_HOUR_START,
	   DT_MINUTE_START,
	   DT_END,
	   DT_HOUR_END,
	   DT_MINUTE_END,
	   SCH_STS,
	   ALL_DAY_YN,
	   CONTENTS,
	   WORK_CONTENTS
FROM OPENXML (@DOC, '/XML/ROW', 2) 
        WITH (SCH_NM NVARCHAR(500),
			  SCH_PLACE NVARCHAR(MAX),
			  DT_START NVARCHAR(8),
			  DT_HOUR_START NVARCHAR(2),
			  DT_MINUTE_START NVARCHAR(2),
			  DT_END NVARCHAR(8),
			  DT_HOUR_END NVARCHAR(2),
			  DT_MINUTE_END NVARCHAR(2),
			  SCH_STS NVARCHAR(5),
			  ALL_DAY_YN CHAR(1),
			  CONTENTS NVARCHAR(MAX),
			  WORK_CONTENTS NVARCHAR(MAX),
			  XML_FLAG NVARCHAR(1))
WHERE XML_FLAG = 'I'

OPEN CUR_CZ_SA_CRM_EMP_SCHEDULE
FETCH NEXT FROM CUR_CZ_SA_CRM_EMP_SCHEDULE INTO @V_SCH_NM,
												@V_SCH_PLACE,
												@V_DT_START,
												@V_DT_HOUR_START,
												@V_DT_MINUTE_START,
												@V_DT_END,
												@V_DT_HOUR_END,
												@V_DT_MINUTE_END,
												@V_SCH_STS,
												@V_ALL_DAY_YN,
												@V_CONTENTS,
												@V_WORK_CONTENTS

WHILE @@FETCH_STATUS = 0                                                      
BEGIN

SELECT @V_SCHM_ID = (MAX(SCHM_ID) + 1)
FROM BX.TSOG_SCH_M

SELECT @V_SCHD_ID = (MAX(SCHD_ID) + 1)
FROM BX.TSOG_SCH_D

INSERT INTO BX.TSOG_SCH_M 
(
	SCHM_ID,
	MENU_ID,
	CO_ID,
	SCH_NM,
	SCH_PLACE,
	START_DT,
	START_TIME,
	END_DT,
	END_TIME,
	SCH_STS,
	ALL_DAY_YN,
	REPEAT_TP,
	CONTENTS,
	WORK_CONTENTS,
	SCH_GB,
	DEL_YN,
	CREATED_BY,
	CREATED_DT,
	SCHMEMO_DIV,
	DOC_ID
)
VALUES
(
     @V_SCHM_ID,
	 0, -- MENU_ID
	 0, -- CO_ID
	 @V_SCH_NM,
	 @V_SCH_PLACE,
	 CONVERT(DATETIME, @V_DT_START), -- START_DT
	 CONVERT(DATETIME, @V_DT_START + ' ' + @V_DT_HOUR_START + ':' + @V_DT_MINUTE_START), -- START_TIME
	 CONVERT(DATETIME, @V_DT_END), -- END_DT
	 CONVERT(DATETIME, @V_DT_END + ' ' + @V_DT_HOUR_END + ':' + @V_DT_MINUTE_END), -- END_TIME
	 @V_SCH_STS,
	 @V_ALL_DAY_YN,
	 '10', -- REPEAT_TP
	 REPLACE(@V_CONTENTS, CHAR(10), CHAR(13) + CHAR(10)), -- CONTENTS
	 REPLACE(@V_WORK_CONTENTS, CHAR(10), CHAR(13) + CHAR(10)), -- WORK_CONTENTS
	 '', -- SCH_GB
	 '0', -- DEL_YN
	 @V_ID_USER, -- CREATED_BY
	 GETDATE(), -- CREATED_DT
	 'S', -- SCHMEMO_DIV
	 0 -- DOC_ID
)

INSERT INTO BX.TSOG_SCH_D 
(
	SCHD_ID,
	MENU_ID,
	SCHM_ID,
	CO_ID,
	SCH_NM,
	SCH_PLACE,
	START_DT,
	START_TIME,
	END_DT,
	END_TIME,
	SCH_STS,
	ALL_DAY_YN,
	REPEAT_TP,
	EXT_SYNC_YN,
	CONTENTS,
	WORK_CONTENTS,
	SCH_GB,
	DEL_YN,
	REG_USER_ID,
	CREATED_BY,
	CREATED_DT,
	SCHMEMO_DIV,
	PJT_ID
)
VALUES
(
     @V_SCHD_ID,
	 0, -- MENU_ID
	 @V_SCHM_ID,
	 0, -- CO_ID
	 @V_SCH_NM,
	 @V_SCH_PLACE,
	 CONVERT(DATETIME, @V_DT_START), -- START_DT
	 CONVERT(DATETIME, @V_DT_START + ' ' + @V_DT_HOUR_START + ':' + @V_DT_MINUTE_START), -- START_TIME
	 CONVERT(DATETIME, @V_DT_END), -- END_DT
	 CONVERT(DATETIME, @V_DT_END + ' ' + @V_DT_HOUR_END + ':' + @V_DT_MINUTE_END), -- END_TIME
	 @V_SCH_STS,
	 @V_ALL_DAY_YN,
	 '10', -- REPEAT_TP
	 '0', -- EXT_SYNC_YN
	 REPLACE(@V_CONTENTS, CHAR(10), CHAR(13) + CHAR(10)), -- CONTENTS
	 REPLACE(@V_WORK_CONTENTS, CHAR(10), CHAR(13) + CHAR(10)), -- WORK_CONTENTS
	 '', -- SCH_GB
	 '0', -- DEL_YN
	 0, -- REG_USER_ID
	 @V_ID_USER, -- CREATED_BY
	 GETDATE(), -- CREATED_DT
	 'S', -- SCHMEMO_DIV
	 0 -- PJT_ID
)

INSERT INTO BX.TSOG_SCH_AUTH
(
	AUTH_DIV,
	SCHD_ID,
	ORG_DIV,
	CO_ID,
	ORG_ID,
	CREATED_DT
)
SELECT '10', @V_SCHD_ID, 'U', 1, @V_ID_USER, GETDATE()
UNION ALL
SELECT '30', @V_SCHD_ID, 'U', 1, @V_ID_USER, GETDATE()
	   	
FETCH NEXT FROM CUR_CZ_SA_CRM_EMP_SCHEDULE INTO @V_SCH_NM,
												@V_SCH_PLACE,
												@V_DT_START,
												@V_DT_HOUR_START,
												@V_DT_MINUTE_START,
												@V_DT_END,
												@V_DT_HOUR_END,
												@V_DT_MINUTE_END,
												@V_SCH_STS,
												@V_ALL_DAY_YN,
												@V_CONTENTS,
												@V_WORK_CONTENTS

END

CLOSE CUR_CZ_SA_CRM_EMP_SCHEDULE                                                      
DEALLOCATE CUR_CZ_SA_CRM_EMP_SCHEDULE  

-- ================================================== UPDATE    
UPDATE A
SET A.SCH_NM = B.SCH_NM,
	A.SCH_PLACE = B.SCH_PLACE,
	A.START_DT = CONVERT(DATETIME, B.DT_START),
	A.START_TIME = CONVERT(DATETIME, B.DT_START + ' ' + B.DT_HOUR_START + ':' + B.DT_MINUTE_START),
	A.END_DT = CONVERT(DATETIME, B.DT_END),
	A.END_TIME = CONVERT(DATETIME, B.DT_END + ' ' + B.DT_HOUR_END + ':' + B.DT_MINUTE_END),
	A.SCH_STS = B.SCH_STS,
	A.ALL_DAY_YN = B.ALL_DAY_YN,
	A.CONTENTS = REPLACE(B.CONTENTS, CHAR(10), CHAR(13) + CHAR(10)),
	A.WORK_CONTENTS = REPLACE(B.WORK_CONTENTS, CHAR(10), CHAR(13) + CHAR(10)),
	A.MODIFY_BY = @V_ID_USER,
	A.MODIFY_DT = GETDATE()
FROM BX.TSOG_SCH_D A 
JOIN OPENXML (@DOC, '/XML/ROW', 2) 
        WITH (SCHD_ID INT,
			  SCH_NM NVARCHAR(500),
			  SCH_PLACE NVARCHAR(MAX),
			  DT_START NVARCHAR(8),
			  DT_HOUR_START NVARCHAR(2),
			  DT_MINUTE_START NVARCHAR(2),
			  DT_END NVARCHAR(8),
			  DT_HOUR_END NVARCHAR(2),
			  DT_MINUTE_END NVARCHAR(2),
			  SCH_STS NVARCHAR(5),
			  ALL_DAY_YN CHAR(1),
			  CONTENTS NVARCHAR(MAX),
			  WORK_CONTENTS NVARCHAR(MAX),
			  XML_FLAG NVARCHAR(1)) B 
ON A.SCHD_ID = B.SCHD_ID
AND B.XML_FLAG = 'U'

UPDATE A
SET A.SCH_NM = B.SCH_NM,
	A.SCH_PLACE = B.SCH_PLACE,
	A.START_DT = CONVERT(DATETIME, B.DT_START),
	A.START_TIME = CONVERT(DATETIME, B.DT_START + ' ' + B.DT_HOUR_START + ':' + B.DT_MINUTE_START),
	A.END_DT = CONVERT(DATETIME, B.DT_END),
	A.END_TIME = CONVERT(DATETIME, B.DT_END + ' ' + B.DT_HOUR_END + ':' + B.DT_MINUTE_END),
	A.SCH_STS = B.SCH_STS,
	A.ALL_DAY_YN = B.ALL_DAY_YN,
	A.CONTENTS = REPLACE(B.CONTENTS, CHAR(10), CHAR(13) + CHAR(10)),
	A.WORK_CONTENTS = REPLACE(B.WORK_CONTENTS, CHAR(10), CHAR(13) + CHAR(10)),
	A.MODIFY_BY = @V_ID_USER,
	A.MODIFY_DT = GETDATE() 
FROM BX.TSOG_SCH_M A 
JOIN OPENXML (@DOC, '/XML/ROW', 2) 
        WITH (SCHM_ID INT,
			  SCH_NM NVARCHAR(500),
			  SCH_PLACE NVARCHAR(MAX),
			  DT_START NVARCHAR(8),
			  DT_HOUR_START NVARCHAR(2),
			  DT_MINUTE_START NVARCHAR(2),
			  DT_END NVARCHAR(8),
			  DT_HOUR_END NVARCHAR(2),
			  DT_MINUTE_END NVARCHAR(2),
			  SCH_STS NVARCHAR(5),
			  ALL_DAY_YN CHAR(1),
			  CONTENTS NVARCHAR(MAX),
			  WORK_CONTENTS NVARCHAR(MAX),
			  XML_FLAG NVARCHAR(1)) B 
ON A.SCHM_ID = B.SCHM_ID
AND B.XML_FLAG = 'U'

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO

