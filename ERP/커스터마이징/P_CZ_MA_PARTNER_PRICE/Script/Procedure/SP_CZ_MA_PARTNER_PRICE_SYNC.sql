USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_PARTNER_PRICE_SYNC]    Script Date: 2018-07-09 오후 1:47:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_MA_PARTNER_PRICE_SYNC]
(
	@P_CD_COMPANY			NVARCHAR(7),
	@P_CD_PLANT				NVARCHAR(20),
	@P_CD_PARTNER			NVARCHAR(20),
	@P_CD_TO_COMPANY		NVARCHAR(7),
	@P_CD_TO_PLANT			NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_ERRMSG		VARCHAR(255)   -- ERROR 메시지
DECLARE @V_CD_ITEM		NVARCHAR(20) = NULL

SELECT @V_CD_ITEM = IU.CD_ITEM
FROM MA_ITEM_UMPARTNER IU
WHERE IU.CD_COMPANY = @P_CD_COMPANY
AND IU.CD_PLANT = @P_CD_PLANT
AND IU.CD_PARTNER = @P_CD_PARTNER
AND NOT EXISTS (SELECT 1 
				FROM MA_PITEM MI
				WHERE MI.CD_COMPANY = @P_CD_TO_COMPANY
				AND MI.CD_PLANT = @P_CD_TO_PLANT
				AND MI.CD_ITEM = IU.CD_ITEM)

IF @V_CD_ITEM IS NOT NULL
BEGIN
	SET @V_ERRMSG = @V_CD_ITEM + ' 가 공장품목에 추가되어 있지 않습니다.'
	GOTO ERROR
END

BEGIN TRAN SP_CZ_MA_PARTNER_PRICE_SYNC
BEGIN TRY

DELETE T
FROM MA_ITEM_UMPARTNER T
WHERE T.CD_COMPANY = @P_CD_TO_COMPANY
AND T.CD_PLANT = @P_CD_TO_PLANT
AND T.CD_PARTNER = @P_CD_PARTNER
AND NOT EXISTS (SELECT 1 
			    FROM MA_ITEM_UMPARTNER F
				WHERE F.CD_COMPANY = @P_CD_COMPANY
			    AND F.CD_PLANT = @P_CD_PLANT
			    AND F.CD_PARTNER = @P_CD_PARTNER
			    AND F.TP_UMMODULE = T.TP_UMMODULE
			    AND (CASE WHEN @P_CD_COMPANY = 'K100' AND @P_CD_TO_COMPANY = 'K200' AND F.CD_ITEM LIKE 'DYES%' THEN F.CD_ITEM + '-DB' ELSE F.CD_ITEM END) = T.CD_ITEM
			    AND F.FG_UM = T.FG_UM
			    AND F.CD_EXCH = T.CD_EXCH 
			    AND F.SDT_UM = T.SDT_UM
			    AND F.NO_LINE = T.NO_LINE);

MERGE INTO MA_ITEM_UMPARTNER AS T
USING (SELECT @P_CD_TO_COMPANY AS CD_COMPANY,
	   	      @P_CD_TO_PLANT AS CD_PLANT,
	   	      CD_PARTNER,
	   	      TP_UMMODULE,
	   	      CD_ITEM,
	   	      FG_UM,
	   	      CD_EXCH,
	   	      SDT_UM,
	   	      NO_LINE,
			  UM_ITEM,
		      DC_PRICE_TERMS,
		      LT,
			  EDT_UM,
		      CD_EXCH_S,
		      UM_ITEM_S,
			  RT_PROFIT_A,
			  RT_PROFIT_B,
			  RT_PROFIT_C,
		      DC_RMK,
			  TXT_USERDEF1
	   FROM MA_ITEM_UMPARTNER
	   WHERE CD_COMPANY = @P_CD_COMPANY
	   AND CD_PLANT = @P_CD_PLANT
	   AND CD_PARTNER = @P_CD_PARTNER) AS F
ON T.CD_COMPANY = F.CD_COMPANY
AND T.CD_PLANT = F.CD_PLANT
AND T.CD_PARTNER = F.CD_PARTNER
AND T.TP_UMMODULE = F.TP_UMMODULE
AND T.CD_ITEM = (CASE WHEN @P_CD_COMPANY = 'K100' AND @P_CD_TO_COMPANY = 'K200' AND F.CD_ITEM LIKE 'DYES%' THEN F.CD_ITEM + '-DB' ELSE F.CD_ITEM END)
AND T.FG_UM = F.FG_UM
AND T.CD_EXCH = F.CD_EXCH 
AND T.SDT_UM = F.SDT_UM
AND T.NO_LINE = F.NO_LINE
WHEN NOT MATCHED THEN
INSERT
(
	CD_COMPANY,
	CD_PLANT,
	CD_PARTNER,
	TP_UMMODULE,
	NO_LINE,
	CD_ITEM,
	FG_UM,
	CD_EXCH,
	UM_ITEM,
	DC_PRICE_TERMS,
	LT,
	SDT_UM,
	EDT_UM,
	CD_EXCH_S,
	UM_ITEM_S,
	RT_PROFIT_A,
	RT_PROFIT_B,
	RT_PROFIT_C,
	DC_RMK,
	TXT_USERDEF1,
	ID_INSERT,
	DTS_INSERT
)
VALUES
(
	@P_CD_TO_COMPANY,
	@P_CD_TO_PLANT,
	F.CD_PARTNER,
	F.TP_UMMODULE,
	F.NO_LINE,
	(CASE WHEN @P_CD_COMPANY = 'K100' AND @P_CD_TO_COMPANY = 'K200' AND F.CD_ITEM LIKE 'DYES%' THEN F.CD_ITEM + '-DB' ELSE F.CD_ITEM END),
	F.FG_UM,
	F.CD_EXCH,
	F.UM_ITEM,
	F.DC_PRICE_TERMS,
	F.LT,
	F.SDT_UM,
	F.EDT_UM,
	F.CD_EXCH_S,
	F.UM_ITEM_S,
	F.RT_PROFIT_A,
	F.RT_PROFIT_B,
	F.RT_PROFIT_C,
	F.DC_RMK,
	F.TXT_USERDEF1,
	'SYNC',
	NEOE.SF_SYSDATE(GETDATE())
)
WHEN MATCHED THEN
UPDATE SET T.UM_ITEM = F.UM_ITEM,
		   T.DC_PRICE_TERMS = F.DC_PRICE_TERMS,
		   T.LT = F.LT,
		   T.EDT_UM = F.EDT_UM,
		   T.CD_EXCH_S = F.CD_EXCH_S,
		   T.UM_ITEM_S = F.UM_ITEM_S,
		   T.RT_PROFIT_A = F.RT_PROFIT_A,
		   T.RT_PROFIT_B = F.RT_PROFIT_B,
		   T.RT_PROFIT_C = F.RT_PROFIT_C,
		   T.DC_RMK = F.DC_RMK,
		   T.TXT_USERDEF1 = F.TXT_USERDEF1,
		   T.ID_UPDATE = 'SYNC', 
		   T.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE());

COMMIT TRAN SP_CZ_MA_PARTNER_PRICE_SYNC

END TRY
BEGIN CATCH
	ROLLBACK TRAN SP_CZ_MA_PARTNER_PRICE_SYNC
	SELECT @V_ERRMSG = ERROR_MESSAGE()
	GOTO ERROR
END CATCH

RETURN

ERROR: 
RAISERROR(@V_ERRMSG, 16, 1)

GO

