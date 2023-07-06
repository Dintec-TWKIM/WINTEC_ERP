USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PR_ROUT_H_DELETE]    Script Date: 2021-02-18 오후 1:26:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_PR_ROUT_H_DELETE]
(
@P_CD_COMPANY          NVARCHAR(7),
@P_CD_ITEM          NVARCHAR(20),
@P_CD_PLANT          NVARCHAR(7),
@P_NO_OPPATH          NVARCHAR(3),
@P_TP_OPPATH          NVARCHAR(3)
)
AS
DELETE FROM PR_ROUT
WHERE        CD_COMPANY  = @P_CD_COMPANY
AND                CD_ITEM           = @P_CD_ITEM
AND                CD_PLANT           = @P_CD_PLANT
AND                NO_OPPATH   = @P_NO_OPPATH
AND                TP_OPPATH   = @P_TP_OPPATH
GO

