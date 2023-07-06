USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_MM_QTIOH_INSERT]    Script Date: 2022-03-24 오후 5:50:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [NEOE].[UP_PU_MM_QTIOH_INSERT]  
(  
    @P_NO_IO               NVARCHAR(40),    -- 수불번호  
    @P_CD_COMPANY          NVARCHAR(14),    -- 회사  
    @P_CD_PLANT            NVARCHAR(14),    -- 공장  
    @P_CD_PARTNER          NVARCHAR(20),    -- 거래처  
    @P_FG_TRANS            NVARCHAR(3),     -- 거래구분  
    @P_YN_RETURN           NCHAR(1),        -- 반품유무  
    @P_DT_IO               NCHAR(8),        -- 수불일자  
    @P_GI_PARTNER          NVARCHAR(20),    --  
    @P_CD_DEPT             NVARCHAR(24),    -- 부서  
    @P_NO_EMP              NVARCHAR(10),    -- 담당자  
    @P_DC_RMK              NVARCHAR(100),    -- 비고  
    @P_ID_INSERT           NVARCHAR(15),    -- 등록자  
    @P_CD_QTIOTP           NCHAR(3) = '',    -- 수불형태  
    @P_FG_TRACK			   NVARCHAR(4) = '',
    @P_TXT_USERDEF1		   NVARCHAR(100) = '',
    @P_CD_DEPT_REQ		   NVARCHAR(12) = ''
)  
AS  
  
DECLARE @ERRNO             INT,             -- ERROR 번호  
        @ERRMSG            VARCHAR(255),    -- ERROR 메시지  
        @P_DTS_INSERT      NVARCHAR(28)  
  
 SET @P_DTS_INSERT = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(28), GETDATE(), 120), '-',''), ' ', ''),':','')  
 SET @ERRNO = 0  
  
IF( @P_YN_RETURN <> 'Y' AND @P_YN_RETURN <> 'N' AND @P_YN_RETURN <> 'C')  
BEGIN  
    SELECT @ERRNO          = 100000, @ERRMSG = 'CM_M100010'  
    GOTO ERROR  
END  
  
INSERT INTO MM_QTIOH (NO_IO, CD_COMPANY, CD_PLANT, CD_PARTNER, FG_TRANS, YN_RETURN, DT_IO, GI_PARTNER, CD_DEPT, NO_EMP, DC_RMK, DTS_INSERT, ID_INSERT, CD_QTIOTP, FG_TRACK, TXT_USERDEF1, CD_DEPT_REQ)  
VALUES (@P_NO_IO, @P_CD_COMPANY, @P_CD_PLANT, @P_CD_PARTNER, @P_FG_TRANS, @P_YN_RETURN, @P_DT_IO, @P_GI_PARTNER, @P_CD_DEPT, @P_NO_EMP, @P_DC_RMK, @P_DTS_INSERT, @P_ID_INSERT, @P_CD_QTIOTP, @P_FG_TRACK, @P_TXT_USERDEF1, @P_CD_DEPT_REQ)  
  
IF (@@ERROR <> 0 )  
BEGIN  
    SELECT @ERRNO          = 100000--, @P_ERRMSG = 'CM_M100010'  
    GOTO ERROR  
END  
  
RETURN  
ERROR:  
IF @ERRNO = 0 RETURN  
ELSE  
    RAISERROR (@ERRMSG, 18, 1)
GO


