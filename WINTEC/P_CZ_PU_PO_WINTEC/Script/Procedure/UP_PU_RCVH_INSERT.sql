USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_RCVH_INSERT]    Script Date: 2022-03-24 오후 5:47:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER    PROC [NEOE].[UP_PU_RCVH_INSERT]  
(  
 @P_NO_RCV  NVARCHAR(20),                -- 의뢰번호  
 @P_CD_COMPANY   NVARCHAR(7),        -- 회사  
 @P_CD_PLANT  NVARCHAR(7),                -- 공장   
 @P_CD_PARTNER  NVARCHAR(20),        -- 거래처  
 @P_DT_REQ  NCHAR(8),                        -- 의뢰일  
 @P_NO_EMP  NVARCHAR(10),                -- 담당자  
 @P_FG_TRANS  NCHAR(3),                        -- 거래구분  
 @P_FG_PROCESS  NCHAR(3),                -- 프로세스구분  
 @P_CD_EXCH  NVARCHAR(3),                -- 환종  
 @P_CD_SL   NVARCHAR(7),                -- 창고   
 @P_YN_RETURN  NCHAR(1),                -- 반품유무  
 @P_YN_AM   NCHAR(1),                        -- 유무환구분  
 @P_DC_RMK  NVARCHAR(50),                -- 비고   
 @P_ID_INSERT  NVARCHAR(15),    -- 등록자  
 @P_FG_RCV  CHAR(3),
 @P_CD_DEPT   NVARCHAR(24),
 @P_FG_UM          NVARCHAR(6) = NULL,  /*구매반품의뢰등록 외에는 해당값 미입력하므로*/
 @P_DC_RMK_TEXT        TEXT = NULL
)  
AS  
BEGIN
DECLARE @P_DTS_INSERT VARCHAR(14)
SET @P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')

INSERT INTO PU_RCVH  
 (NO_RCV, CD_COMPANY, CD_PLANT, CD_PARTNER, DT_REQ,   
 NO_EMP, FG_TRANS, FG_PROCESS, CD_EXCH, CD_SL,   
 YN_RETURN, YN_AM, DC_RMK, DTS_INSERT, ID_INSERT,   
 FG_RCV, CD_DEPT, FG_UM, DC_RMK_TEXT)  
VALUES(@P_NO_RCV, @P_CD_COMPANY, @P_CD_PLANT, @P_CD_PARTNER, @P_DT_REQ,   
 @P_NO_EMP, @P_FG_TRANS, @P_FG_PROCESS, @P_CD_EXCH, @P_CD_SL,   
 @P_YN_RETURN, @P_YN_AM, @P_DC_RMK, @P_DTS_INSERT, @P_ID_INSERT,   
 @P_FG_RCV, @P_CD_DEPT, @P_FG_UM, @P_DC_RMK_TEXT)  
  
END
GO


