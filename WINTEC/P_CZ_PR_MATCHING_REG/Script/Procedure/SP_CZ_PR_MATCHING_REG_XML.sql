USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_MATCHING_REG_XML]    Script Date: 2016-11-30 오후 5:22:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [NEOE].[SP_CZ_PR_MATCHING_REG_XML] 
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_CD_PLANT		NVARCHAR(20),
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10), 
    @DOC			INT = NULL
) 
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML 

-- ================================================== DELETE
DELETE A 
FROM CZ_PR_MATCHING_DATA A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (NO_ID_L		NVARCHAR(20),
			  NO_ID_C		NVARCHAR(20),
			  NO_ID_R		NVARCHAR(20)) B 
 ON A.CD_COMPANY = @P_CD_COMPANY
AND A.CD_PLANT = @P_CD_PLANT
AND A.NO_ID_L = B.NO_ID_L
AND A.NO_ID_C = B.NO_ID_C
AND A.NO_ID_R = B.NO_ID_R

-- ================================================== INSERT
INSERT INTO CZ_PR_MATCHING_DATA 
(
	CD_COMPANY,
    CD_PLANT,
    NO_ID_L,
    NO_ID_C,
    NO_ID_R,
	STA_MATCHING,
    CD_ITEM,
    NO_WO_L,
    CD_PITEM_L,
    NUM_P1_L,
    NUM_P2_L,
    NUM_P3_L,
    NUM_MIN_L,
    NUM_C1,
    NO_WO_C,
    CD_PITEM_C,
    NUM_P1_OUT_C,
    NUM_P2_OUT_C,
    NUM_P3_OUT_C,
    NUM_MAX_C,
    NUM_P1_IN_C,
    NUM_P2_IN_C,
    NUM_P3_IN_C,
    NUM_MIN_C,
    NUM_C2,
    NO_WO_R,
    CD_PITEM_R,
    NUM_P1_R,
    NUM_P2_R,
    NUM_P3_R,
    NUM_MAX_R,
	NUM_MATCHING_SIZE1,
	NUM_MATCHING_SIZE2,
	NO_SO,
	DC_RMK,
    ID_INSERT,
    DTS_INSERT
)
SELECT @P_CD_COMPANY,
	   @P_CD_PLANT,
	   NO_ID_L,
	   NO_ID_C,
	   NO_ID_R,
	   STA_MATCHING,
	   CD_ITEM,
	   NO_WO_L,
	   CD_PITEM_L,
	   NUM_P1_L,
	   NUM_P2_L,
	   NUM_P3_L,
	   NUM_MIN_L,
	   NUM_C1,
	   NO_WO_C,
	   CD_PITEM_C,
	   NUM_P1_OUT_C,
	   NUM_P2_OUT_C,
	   NUM_P3_OUT_C,
	   NUM_MAX_C,
	   NUM_P1_IN_C,
	   NUM_P2_IN_C,
	   NUM_P3_IN_C,
	   NUM_MIN_C,
	   NUM_C2,
	   NO_WO_R,
	   CD_PITEM_R,
	   NUM_P1_R,
	   NUM_P2_R,
	   NUM_P3_R,
	   NUM_MAX_R,
	   NUM_MATCHING_SIZE1,
	   NUM_MATCHING_SIZE2,
	   NO_SO,
	   DC_RMK,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
FROM OPENXML (@DOC, '/XML/I', 2) 
        WITH (NO_ID_L			 NVARCHAR(20),
			  NO_ID_C			 NVARCHAR(20),
			  NO_ID_R			 NVARCHAR(20),
			  STA_MATCHING		 NVARCHAR(4),
			  CD_ITEM			 NVARCHAR(20),
			  NO_WO_L			 NVARCHAR(20),
			  CD_PITEM_L		 NVARCHAR(20),
			  NUM_P1_L			 NUMERIC(7, 4),
			  NUM_P2_L			 NUMERIC(7, 4),
			  NUM_P3_L			 NUMERIC(7, 4),
			  NUM_MIN_L			 NUMERIC(7, 4),
			  NUM_C1			 NUMERIC(7, 4),
			  NO_WO_C			 NVARCHAR(20),
			  CD_PITEM_C		 NVARCHAR(20),
			  NUM_P1_OUT_C		 NUMERIC(7, 4),
			  NUM_P2_OUT_C		 NUMERIC(7, 4),
			  NUM_P3_OUT_C		 NUMERIC(7, 4),
			  NUM_MAX_C			 NUMERIC(7, 4),
			  NUM_P1_IN_C		 NUMERIC(7, 4),
			  NUM_P2_IN_C		 NUMERIC(7, 4),
			  NUM_P3_IN_C		 NUMERIC(7, 4),
			  NUM_MIN_C			 NUMERIC(7, 4),
			  NUM_C2			 NUMERIC(7, 4),
			  NO_WO_R			 NVARCHAR(20),
			  CD_PITEM_R		 NVARCHAR(20),
			  NUM_P1_R			 NUMERIC(7, 4),
			  NUM_P2_R			 NUMERIC(7, 4),
			  NUM_P3_R			 NUMERIC(7, 4),
			  NUM_MAX_R			 NUMERIC(7, 4),
			  NUM_MATCHING_SIZE1 NUMERIC(7, 4),
			  NUM_MATCHING_SIZE2 NUMERIC(7, 4),
			  NO_SO				 NVARCHAR(20),
			  DC_RMK			 NVARCHAR(500))

-- ================================================== UPDATE    
UPDATE A
SET A.STA_MATCHING = B.STA_MATCHING,
	A.NUM_MATCHING_SIZE1 = B.NUM_MATCHING_SIZE1,
	A.NUM_MATCHING_SIZE2 = B.NUM_MATCHING_SIZE2,
	A.NO_SO = B.NO_SO,
	A.DC_RMK = B.DC_RMK
FROM CZ_PR_MATCHING_DATA A 
JOIN OPENXML (@DOC, '/XML/U', 2) 
        WITH (NO_ID_L			 NVARCHAR(20),
			  NO_ID_C			 NVARCHAR(20),
			  NO_ID_R			 NVARCHAR(20),
			  STA_MATCHING		 NVARCHAR(4),
			  NUM_MATCHING_SIZE1 NUMERIC(7, 4),
			  NUM_MATCHING_SIZE2 NUMERIC(7, 4),
			  NO_SO				 NVARCHAR(20),
			  DC_RMK			 NVARCHAR(500)) B 
 ON A.CD_COMPANY = @P_CD_COMPANY
AND A.CD_PLANT = @P_CD_PLANT
AND A.NO_ID_L = B.NO_ID_L
AND A.NO_ID_C = B.NO_ID_C
AND A.NO_ID_R = B.NO_ID_R

INSERT INTO CZ_PR_MATCHING_DEACTIVATE
(
	CD_COMPANY,
	CD_PLANT,
	NO_ID,
	STA_DEACTIVATE,
	NO_WO,
	CD_ITEM,
	CD_PITEM,
	NUM_P1_OUT,
	NUM_P2_OUT,
	NUM_P3_OUT,
	NUM_P1_IN,
	NUM_P2_IN,
	NUM_P3_IN,
	ID_INSERT,
	DTS_INSERT
)
SELECT @P_CD_COMPANY,
	   @P_CD_PLANT,
	   MD.NO_ID_L AS NO_ID,
	   '001' AS STA_DEACTIVATE,
	   MD.NO_WO_L AS NO_WO,
	   MD.CD_ITEM,
	   MD.CD_PITEM_L AS CD_PITEM,
	   MD.NUM_P1_L AS NUM_P1_OUT,
	   MD.NUM_P2_L AS NUM_P2_OUT,
	   MD.NUM_P3_L AS NUM_P3_OUT,
	   NULL AS NUM_P1_IN,
	   NULL AS NUM_P2_IN,
	   NULL AS NUM_P3_IN,
	   @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
FROM OPENXML (@DOC, '/XML/U', 2) 
        WITH (NO_ID_L			 NVARCHAR(20),
			  STA_MATCHING		 NVARCHAR(4),
			  NO_WO_L			 NVARCHAR(20),
			  CD_ITEM			 NVARCHAR(20),
			  CD_PITEM_L		 NVARCHAR(20),
			  NUM_P1_L			 NUMERIC(7, 4),
			  NUM_P2_L			 NUMERIC(7, 4),
			  NUM_P3_L			 NUMERIC(7, 4)) MD
WHERE MD.STA_MATCHING = '005'
UNION ALL
SELECT @P_CD_COMPANY,
	   @P_CD_PLANT,
	   MD.NO_ID_C AS NO_ID,
	   '001' AS STA_DEACTIVATE,
	   MD.NO_WO_C AS NO_WO,
	   MD.CD_ITEM,
	   MD.CD_PITEM_C AS CD_PITEM,
	   MD.NUM_P1_OUT_C AS NUM_P1_OUT,
	   MD.NUM_P2_OUT_C AS NUM_P2_OUT,
	   MD.NUM_P3_OUT_C AS NUM_P3_OUT,
	   MD.NUM_P1_IN_C AS NUM_P1_IN,
	   MD.NUM_P2_IN_C AS NUM_P2_IN,
	   MD.NUM_P3_IN_C AS NUM_P3_IN,
	   @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
FROM OPENXML (@DOC, '/XML/U', 2) 
        WITH (NO_ID_C			 NVARCHAR(20),
			  STA_MATCHING		 NVARCHAR(4),
			  NO_WO_C			 NVARCHAR(20),
			  CD_ITEM		     NVARCHAR(20),
			  CD_PITEM_C		 NVARCHAR(20),
			  NUM_P1_OUT_C		 NUMERIC(7, 4),
			  NUM_P2_OUT_C		 NUMERIC(7, 4),
			  NUM_P3_OUT_C		 NUMERIC(7, 4),
			  NUM_P1_IN_C		 NUMERIC(7, 4),
			  NUM_P2_IN_C		 NUMERIC(7, 4),
			  NUM_P3_IN_C		 NUMERIC(7, 4)) MD
WHERE MD.STA_MATCHING = '005'
UNION ALL
SELECT @P_CD_COMPANY,
	   @P_CD_PLANT,
	   MD.NO_ID_R AS NO_ID,
	   '001' AS STA_DEACTIVATE,
	   MD.NO_WO_R AS NO_WO,
	   MD.CD_ITEM,
	   MD.CD_PITEM_R AS CD_PITEM,
	   NULL AS NUM_P1_OUT,
	   NULL AS NUM_P2_OUT,
	   NULL AS NUM_P3_OUT,
	   MD.NUM_P1_R AS NUM_P1_IN,
	   MD.NUM_P2_R AS NUM_P2_IN,
	   MD.NUM_P3_R AS NUM_P3_IN,
	   @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
FROM OPENXML (@DOC, '/XML/U', 2) 
        WITH (NO_ID_R			 NVARCHAR(20),
			  STA_MATCHING		 NVARCHAR(4),
			  NO_WO_R			 NVARCHAR(20),
			  CD_ITEM			 NVARCHAR(20),
			  CD_PITEM_R		 NVARCHAR(20),
			  NUM_P1_R			 NUMERIC(7, 4),
			  NUM_P2_R			 NUMERIC(7, 4),
			  NUM_P3_R			 NUMERIC(7, 4)) MD
WHERE MD.STA_MATCHING = '005'

DELETE A 
FROM CZ_PR_MATCHING_DATA A 
JOIN OPENXML (@DOC, '/XML/U', 2) 
        WITH (NO_ID_L		NVARCHAR(20),
			  NO_ID_C		NVARCHAR(20),
			  NO_ID_R		NVARCHAR(20),
			  STA_MATCHING	NVARCHAR(4)) B 
 ON A.CD_COMPANY = @P_CD_COMPANY
AND A.CD_PLANT = @P_CD_PLANT
AND A.NO_ID_L = B.NO_ID_L
AND A.NO_ID_C = B.NO_ID_C
AND A.NO_ID_R = B.NO_ID_R
AND B.STA_MATCHING = '005'

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO
