USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SU_PRH_I]    Script Date: 2021-03-12 오후 4:39:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SU_PRH_I]
(
    @P_CD_COMPANY   NVARCHAR(7),
    @P_CD_PLANT     NVARCHAR(7),
    @P_NO_PR        NVARCHAR(20),
    @P_DT_PR        NVARCHAR(8),
    @P_CD_DEPT      NVARCHAR(12), 
    @P_NO_EMP       NVARCHAR(10),
    @P_CD_PURGRP    NVARCHAR(7),
    @P_FG_PR        NVARCHAR(3),
    @P_CD_PJT       NVARCHAR(20), 
    @P_DC_RMK       NVARCHAR(50),
    @P_ID_INSERT    NVARCHAR(15)
)
AS

INSERT INTO SU_PRH
(
    CD_COMPANY,
    CD_PLANT,
    NO_PR,
    DT_PR,
    NO_EMP,
    CD_DEPT,
    CD_PURGRP,
    FG_PR,
    CD_PJT,
    DC_RMK,
    ID_INSERT,
    DTS_INSERT
)
VALUES
(
    @P_CD_COMPANY,
    @P_CD_PLANT,
    @P_NO_PR,
    @P_DT_PR,
    @P_NO_EMP,
    @P_CD_DEPT,
    @P_CD_PURGRP,
    @P_FG_PR,
    @P_CD_PJT,
    @P_DC_RMK,
    @P_ID_INSERT,
    NEOE.SF_SYSDATE(GETDATE())
)

GO