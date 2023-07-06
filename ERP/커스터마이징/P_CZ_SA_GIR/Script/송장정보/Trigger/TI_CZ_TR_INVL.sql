USE [NEOE]
GO

/****** Object:  Trigger [NEOE].[TI_CZ_TR_INVL]    Script Date: 2015-10-22 오후 5:32:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [NEOE].[TI_CZ_TR_INVL] ON [NEOE].[CZ_TR_INVL] AFTER INSERT
AS

DECLARE  @V_CD_COMPANY          NVARCHAR(7),
	     @V_NO_INV              NVARCHAR(20),
	     @V_NO_LINE             NUMERIC(5, 0),
	     @V_NO_PO               NVARCHAR(20),
	     @V_NO_LINE_PO          NUMERIC(5, 0),
	     @V_NO_QTIO             NVARCHAR(20),
	     @V_NO_LINE_QTIO        NUMERIC(5, 0),
	     @V_QT_SO               NUMERIC(17, 4), 
	     @V_AM_EX               NUMERIC(17, 4),
	     @V_STA_SO              NVARCHAR(3),
	     @V_AM_EXSO             NUMERIC(17, 4),
	     @V_QT_INV              NUMERIC(17, 4),
	     @ERRNO                 INT,
	     @ERRMSG                VARCHAR(255)

DECLARE CUR_TI_CZ_TR_INVL CURSOR FOR

SELECT CD_COMPANY, NO_INV, NO_LINE, NO_PO, NO_LINE_PO, NO_QTIO, NO_LINE_QTIO, QT_SO, AM_EXSO
FROM INSERTED

OPEN CUR_TI_CZ_TR_INVL

FETCH NEXT FROM CUR_TI_CZ_TR_INVL
INTO @V_CD_COMPANY, @V_NO_INV, @V_NO_LINE, @V_NO_PO, @V_NO_LINE_PO, @V_NO_QTIO, @V_NO_LINE_QTIO, @V_QT_SO, @V_AM_EXSO

WHILE (@@FETCH_STATUS <> -1)
BEGIN
	IF (@@FETCH_STATUS <> -2)
	BEGIN
		--수주상태를 체크한다.
		SELECT @V_STA_SO = STA_SO
		FROM SA_SOL
		WHERE CD_COMPANY = @V_CD_COMPANY
		AND NO_SO = @V_NO_PO
		AND SEQ_SO = @V_NO_LINE_PO
		AND ISNULL(CD_USERDEF1, '') <> '재고픽업'

		--수주상태가 진행이면 리턴
		IF (@V_STA_SO = 'O')
		BEGIN
			--수주가 진행 혹은 종결 이므로 저장 할 수 없습니다.
			SELECT @ERRNO = 100000,
				   @ERRMSG = 'SA_M000133'
		    GOTO ERROR 
		END

		--출고라인의 출고수량과 LC수량의 차를 변수에 저장한다. 
		SELECT @V_QT_INV = QT_UNIT_MM - QT_INV
		FROM MM_QTIO
		WHERE CD_COMPANY = @V_CD_COMPANY
		AND NO_IO = @V_NO_QTIO
		AND NO_IOLINE = @V_NO_LINE_QTIO

		--수주라인의 수주수량과 LC수량의 차가 새로운 LC수량보다 작으면
		IF (@V_QT_INV < @V_QT_SO) 
		BEGIN
			--송장수량이 출고수량을 초과할 수 없습니다.
		    SELECT @ERRNO =  100000,
				   @ERRMSG = 'TR_M000099'
		    GOTO ERROR
		END
		ELSE
		BEGIN
		--출고라인의 LC수량을 새로운 LC수량과 더해서 수정한다.
		UPDATE MM_QTIO
		SET QT_INV = QT_INV + @V_QT_SO
		WHERE CD_COMPANY = @V_CD_COMPANY
		AND NO_IO = @V_NO_QTIO
		AND NO_IOLINE = @V_NO_LINE_QTIO
		
		--LC등록헤더의 금액을 새로운 금액과 더해서 수정한다.
		UPDATE CZ_TR_INVH
		SET AM_EX = AM_EX + @V_AM_EXSO
		WHERE CD_COMPANY = @V_CD_COMPANY
		AND NO_INV = @V_NO_INV
		END
	END

FETCH NEXT FROM CUR_TI_CZ_TR_INVL
INTO @V_CD_COMPANY, @V_NO_INV, @V_NO_LINE, @V_NO_PO, @V_NO_LINE_PO, @V_NO_QTIO, @V_NO_LINE_QTIO, @V_QT_SO, @V_AM_EXSO
END

CLOSE CUR_TI_CZ_TR_INVL
DEALLOCATE CUR_TI_CZ_TR_INVL

RETURN 

ERROR:
		CLOSE CUR_TI_CZ_TR_INVL
		DEALLOCATE CUR_TI_CZ_TR_INVL
		RAISERROR (@ERRMSG, 18, 1)
		ROLLBACK TRANSACTION
		RETURN
GO

