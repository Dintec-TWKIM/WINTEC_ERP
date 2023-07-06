USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_RCVH_INSERT]    Script Date: 2022-03-24 ���� 5:47:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER    PROC [NEOE].[UP_PU_RCVH_INSERT]  
(  
 @P_NO_RCV  NVARCHAR(20),                -- �Ƿڹ�ȣ  
 @P_CD_COMPANY   NVARCHAR(7),        -- ȸ��  
 @P_CD_PLANT  NVARCHAR(7),                -- ����   
 @P_CD_PARTNER  NVARCHAR(20),        -- �ŷ�ó  
 @P_DT_REQ  NCHAR(8),                        -- �Ƿ���  
 @P_NO_EMP  NVARCHAR(10),                -- �����  
 @P_FG_TRANS  NCHAR(3),                        -- �ŷ�����  
 @P_FG_PROCESS  NCHAR(3),                -- ���μ�������  
 @P_CD_EXCH  NVARCHAR(3),                -- ȯ��  
 @P_CD_SL   NVARCHAR(7),                -- â��   
 @P_YN_RETURN  NCHAR(1),                -- ��ǰ����  
 @P_YN_AM   NCHAR(1),                        -- ����ȯ����  
 @P_DC_RMK  NVARCHAR(50),                -- ���   
 @P_ID_INSERT  NVARCHAR(15),    -- �����  
 @P_FG_RCV  CHAR(3),
 @P_CD_DEPT   NVARCHAR(24),
 @P_FG_UM          NVARCHAR(6) = NULL,  /*���Ź�ǰ�Ƿڵ�� �ܿ��� �ش簪 ���Է��ϹǷ�*/
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


