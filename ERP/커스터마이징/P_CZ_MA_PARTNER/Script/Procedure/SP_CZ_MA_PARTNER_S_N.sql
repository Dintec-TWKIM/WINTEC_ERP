USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_MA_PARTNER_S_N]    Script Date: 2016-07-04 오후 4:21:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_MA_PARTNER_S_N]
(
	@P_CD_COMPANY  	NVARCHAR(7),
	@P_NO_RES		NVARCHAR(13),
	@P_REG			NVARCHAR(1) OUTPUT
) 
AS
-- =====================================================
-- SYSTEM			: 인사
-- SUB SYSTEM		: 시스템관리
-- PAGE				: 거래처정보관리
-- DESC				: 주민등록번호 중복 체크
--
-- RETURN VALUES
-- 
-- 작    성    자	: 이대성
-- 작    성    일   : 2009.05.12
-- 
-- 수    정    자   : 
-- 수  정  내  용   : 
-- =====================================================
-- CHANGE HISTORY
-- =====================================================
-- =====================================================
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET @P_REG = 'N'

SELECT	@P_REG = 'Y'
FROM	MA_PARTNER
WHERE	CD_COMPANY = @P_CD_COMPANY AND NO_RES = @P_NO_RES

IF @P_REG IS NULL
	SET @P_REG = 'N'
GO

