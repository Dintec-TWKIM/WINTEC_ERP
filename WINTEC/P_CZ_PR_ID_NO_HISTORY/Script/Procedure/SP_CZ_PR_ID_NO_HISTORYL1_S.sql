USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PR_ID_NO_HISTORYL1_S]    Script Date: 2019-03-26 오전 11:26:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PR_ID_NO_HISTORYL1_S]
(
    @P_CD_COMPANY   NVARCHAR(7),
    @P_NO_WO        NVARCHAR(20),
	@P_SEQ_WO		NUMERIC(5, 0),
	@P_NO_LINE		NUMERIC(5, 0)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT WI.NO_WO,
	   WI.SEQ_WO,
	   WI.NO_LINE,
	   WI.NO_INSP,
	   WI.DT_INSP,
	   WI.NO_EMP,
	   ME.NM_KOR,
	   WI.NO_DATA1,
	   WI.NO_DATA2,
	   WI.NO_DATA3,
	   WI.NO_DATA4,
	   WI.NO_DATA5,
	   WI.NO_HEAT,
	   MC.NM_SYSDEF AS NM_REJECT,
	   MC1.NM_SYSDEF AS NM_RESOURCE,
	   WI.DTS_INSERT
FROM CZ_PR_WO_INSP WI
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = WI.CD_COMPANY AND ME.NO_EMP = WI.NO_EMP
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = WI.CD_COMPANY AND MC.CD_FIELD = 'QU_2000007' AND MC.CD_SYSDEF = WI.CD_REJECT
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = WI.CD_COMPANY AND MC1.CD_FIELD = 'QU_2000009' AND MC1.CD_SYSDEF = WI.CD_RESOURCE
WHERE WI.CD_COMPANY = @P_CD_COMPANY
AND WI.NO_WO = @P_NO_WO
AND WI.SEQ_WO = @P_SEQ_WO
AND WI.NO_LINE = @P_NO_LINE

GO