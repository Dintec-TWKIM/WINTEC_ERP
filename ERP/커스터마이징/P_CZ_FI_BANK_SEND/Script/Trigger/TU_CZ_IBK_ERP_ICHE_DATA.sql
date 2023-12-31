USE [NEOE]
GO
/****** Object:  Trigger [NEOE].[TU_CZ_IBK_ERP_ICHE_DATA]    Script Date: 2018-01-08 오후 4:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [NEOE].[TU_CZ_IBK_ERP_ICHE_DATA] ON [NEOE].[IBK_ERP_ICHE_DATA]
FOR UPDATE
AS

SET NOCOUNT ON

UPDATE BH
SET BH.SETTLE_DATE = I.ICHE_DATE,
	BH.SETTLE_YN = (CASE WHEN ISNULL(I.ICHE_DATE, '') <> '' THEN 'Y' ELSE NULL END)
FROM BANK_SENDH BH
JOIN INSERTED I ON I.C_CODE = BH.C_CODE AND I.REG_DATE = BH.TRANS_DATE AND I.FL_SEQ = BH.TRANS_SEQ AND I.REG_SEQ = BH.SEQ
JOIN DELETED D ON D.SITE_NO = I.SITE_NO AND D.REG_DATE = I.REG_DATE AND D.REG_TIME = I.REG_TIME AND D.REG_SEQ = I.REG_SEQ AND D.ICHE_GB = I.ICHE_GB
WHERE ISNULL(D.ICHE_DATE, '') <> ISNULL(I.ICHE_DATE, '')

UPDATE BD
SET BD.TRANS_YN = (CASE WHEN ISNULL(I.ICHE_DATE, '') <> '' THEN 'Y' ELSE NULL END)
FROM BANK_SENDD BD
JOIN INSERTED I ON I.C_CODE = BD.C_CODE AND I.REG_DATE = BD.TRANS_DATE AND I.FL_SEQ = BD.TRANS_SEQ AND I.REG_SEQ = BD.SEQ
JOIN DELETED D ON D.SITE_NO = I.SITE_NO AND D.REG_DATE = I.REG_DATE AND D.REG_TIME = I.REG_TIME AND D.REG_SEQ = I.REG_SEQ AND D.ICHE_GB = I.ICHE_GB
WHERE ISNULL(D.ICHE_DATE, '') <> ISNULL(I.ICHE_DATE, '')

SET NOCOUNT OFF

GO