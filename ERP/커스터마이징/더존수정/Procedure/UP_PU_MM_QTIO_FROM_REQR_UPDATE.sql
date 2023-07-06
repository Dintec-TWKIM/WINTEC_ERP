USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_MM_QTIO_FROM_REQR_UPDATE]    Script Date: 2016-05-17 오후 6:53:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*        의뢰반품에 의한 수불테이터 잔량 관리         */
ALTER PROC [NEOE].[UP_PU_MM_QTIO_FROM_REQR_UPDATE]
(
@NO_IO                                        NVARCHAR(20),        -- 수불번호
@NO_IOLINE                                NUMERIC(5, 0),                -- 수불항번
@CD_COMPANY                        NVARCHAR(7),                -- 회사
@QT_RETURN_OLD                        NUMERIC(17,4),        -- 반품수량        
@QT_RETURN_NEW                        NUMERIC(17,4),
@QT_RETURN_MM_OLD                NUMERIC(17,4),
@QT_RETURN_MM_NEW                NUMERIC(17,4),
@IS_DELETE                                NCHAR(1)                        -- 추가(N), 삭제(Y)
)
AS
DECLARE        
	@ERRNO                                INT,
	@ERRMSG                                VARCHAR(255),
	@QT_IO                                NUMERIC(17,4),         -- 수불수량
	@QT_RETURN_MM                NUMERIC(17,4)         -- 반품수량

IF EXISTS (SELECT 1 FROM MM_QTIO WHERE NO_IO = @NO_IO AND NO_IOLINE = @NO_IOLINE AND CD_COMPANY = @CD_COMPANY)
BEGIN
-- 추가 및 수정 이면
IF(@IS_DELETE = 'N')
	BEGIN
	        SELECT @QT_IO = QT_IO, @QT_RETURN_MM = QT_RETURN
	        FROM MM_QTIO
	        WHERE NO_IO = @NO_IO
	                AND NO_IOLINE = @NO_IOLINE
	                AND CD_COMPANY = @CD_COMPANY

	        IF( @QT_IO < ( ( @QT_RETURN_NEW - @QT_RETURN_OLD ) + @QT_RETURN_MM))
	        BEGIN 
	                SELECT @ERRNO  = 100000, @ERRMSG = 'PU_M000041' GOTO ERROR 
	        END
	END

-- QTIO테이블 반품 잔량처리                                
UPDATE MM_QTIO 
SET QT_RETURN = QT_RETURN + ( @QT_RETURN_NEW - @QT_RETURN_OLD ),
	QT_RETURN_MM = QT_RETURN_MM + ( @QT_RETURN_MM_NEW - @QT_RETURN_MM_OLD )
WHERE NO_IO =@NO_IO
	AND NO_IOLINE =@NO_IOLINE
	AND CD_COMPANY =@CD_COMPANY

IF (@@ERROR <> 0 ) BEGIN SELECT @ERRNO  = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR END
END
ELSE
BEGIN 
SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR 
END

RETURN
ERROR: RAISERROR(@ERRMSG, 16, 1) -- 2012/08/13:자동화 변경[RAISERROR], 변경전:RAISERROR @ERRNO @ERRMSG 




GO

