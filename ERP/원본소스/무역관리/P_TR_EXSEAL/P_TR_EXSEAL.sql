IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXSEAL_SELECT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_EXSEAL_SELECT
GO 
/**********************************************************************************************************  
 * 프로시저명: UP_TR_EXSEAL_SELECT
 * 관련페이지: 수출 >> 인수증등록 > 인수증등록 헤더 조회
 * 작  성  자: 
 * EXEC UP_TR_EXSEAL_SELECT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXSEAL_SELECT 
	@CD_COMPANY		NVARCHAR(7),	--회사코드
	@NO_SEAL		NVARCHAR(20)	--인수증번호
AS  
BEGIN
	SELECT H.NO_SEAL,
	H.DT_BALLOT,
	H.CD_BIZAREA,
	H.CD_PARTNER,
	H.CD_EXCH,
	H.AM_SEAL,
	H.AM,
	H.CD_DEPT,
	H.NO_EMP,
	H.DT_TRANS,
	H.DT_SEAL,
	H.DT_VALIDITY,
	H.REMARK,
	H.DTS_INSERT,
	H.ID_INSERT,
	H.DTS_UPDATE,
	H.ID_UPDATE,
	(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = @CD_COMPANY AND CD_BIZAREA = H.CD_BIZAREA) NM_BIZAREA,	--사업장명  
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = @CD_COMPANY AND CD_PARTNER = H.CD_PARTNER) LN_PARTNER,  --거래처  
	(SELECT NM_KOR     FROM MA_EMP     WHERE CD_COMPANY = @CD_COMPANY AND NO_EMP = H.NO_EMP) NM_KOR					--담당자명
	FROM TR_SEALEXH H
	WHERE H.CD_COMPANY = @CD_COMPANY
	AND H.NO_SEAL = @NO_SEAL
	
	SELECT L.NO_SEAL,
	L.NO_LINE,
	L.NO_IV,
	L.NO_IVLINE,
	L.CD_PLANT,
	(SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = @CD_COMPANY AND CD_PLANT = L.CD_PLANT AND CD_BIZAREA = H.CD_BIZAREA) NM_PLANT,  --공장명
	L.NO_LC,
	L.CD_ITEM,
	(SELECT NM_ITEM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = L.CD_ITEM) NM_ITEM,  --품목명
	(SELECT STND_ITEM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = L.CD_ITEM) STND_ITEM,  --규격
	(SELECT UNIT_IM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = L.CD_ITEM) UNIT_IM,  --단위
	L.QT_GI_CLS,
	L.CD_EXCH,
	L.RT_EXCH,
	L.UM_EX_CLS,
	L.AM_EX_CLS,
	L.UM_ITEM_CLS,
	L.AM_CLS,
	L.QT_CLS,
	L.DTS_INSERT,
	L.ID_INSERT,
	L.DTS_UPDATE,
	L.ID_UPDATE
	FROM TR_SEALEXH H
	LEFT OUTER JOIN TR_SEALEXL L ON L.CD_COMPANY = H.CD_COMPANY AND L.NO_SEAL = H.NO_SEAL
	WHERE L.CD_COMPANY = @CD_COMPANY
	AND L.NO_SEAL = @NO_SEAL
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXSEAL_H_INSERT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_EXSEAL_H_INSERT
GO 
/**********************************************************************************************************  
 * 프로시저명: UP_TR_EXSEAL_H_INSERT
 * 관련페이지: 수출 >> 인수증등록 > 메인버튼저장
 * 작  성  자: 
 * EXEC UP_TR_EXSEAL_H_INSERT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXSEAL_H_INSERT 
	@CD_COMPANY		NVARCHAR(7),	--회사코드
	@NO_SEAL		NVARCHAR(20),	--인수증발급번호
	@DT_BALLOT		NCHAR(8),		--발행일자
	@CD_BIZAREA		NVARCHAR(7),	--사업장
	@CD_PARTNER		NVARCHAR(7),	--거래처
	@CD_EXCH		NVARCHAR(3),	--통화
	@AM_SEAL		NUMERIC(17,4),	--물품인수금액
	@AM				NUMERIC(17,4),	--발행원화금액
	@CD_DEPT		NVARCHAR(12),	--담당부서
	@NO_EMP			NVARCHAR(10),	--담당자
	@DT_TRANS		NCHAR(8),		--인도일자
	@DT_SEAL		NCHAR(8),		--물품일수일
	@DT_VALIDITY	NCHAR(8),		--유효일
	@REMARK			NVARCHAR(100),	--비고
	@DTS_INSERT		NVARCHAR(14),	--등록일
	@ID_INSERT		NVARCHAR(15),	--등록자
	@DTS_UPDATE		NVARCHAR(14),	--수정일	
	@ID_UPDATE		NVARCHAR(15)	--수정자
AS  
BEGIN
	IF NOT EXISTS(SELECT TOP 1 NO_SEAL FROM TR_SEALEXH WHERE CD_COMPANY = @CD_COMPANY AND NO_SEAL = @NO_SEAL)
	BEGIN
		INSERT INTO TR_SEALEXH( CD_COMPANY,		NO_SEAL,		DT_BALLOT,		CD_BIZAREA,		CD_PARTNER,		CD_EXCH,
							    AM_SEAL,		AM,				CD_DEPT,		NO_EMP,			DT_TRANS,
							    DT_SEAL,		DT_VALIDITY,	REMARK,			DTS_INSERT,		ID_INSERT)
		VALUES( @CD_COMPANY,	@NO_SEAL,		@DT_BALLOT,		@CD_BIZAREA,	@CD_PARTNER,	@CD_EXCH,
			    @AM_SEAL,		@AM,			@CD_DEPT,		@NO_EMP,		@DT_TRANS,
			    @DT_SEAL,		@DT_VALIDITY,	@REMARK,		@DTS_INSERT,	@ID_INSERT)
	END
	
	ELSE
	BEGIN
		UPDATE TR_SEALEXH
		SET
			DT_BALLOT		= @DT_BALLOT,
			CD_BIZAREA		= @CD_BIZAREA,
			CD_PARTNER		= @CD_PARTNER,
			CD_EXCH			= @CD_EXCH,
			AM_SEAL			= @AM_SEAL,	
			AM				= @AM,
			CD_DEPT			= @CD_DEPT,
			NO_EMP			= @NO_EMP,
			DT_TRANS		= @DT_TRANS,
			DT_SEAL			= @DT_SEAL,
			DT_VALIDITY		= @DT_VALIDITY,
			REMARK			= @REMARK,
			DTS_UPDATE		= @DTS_UPDATE,
			ID_UPDATE		= @ID_UPDATE
		WHERE CD_COMPANY = @CD_COMPANY
		AND NO_SEAL = @NO_SEAL
	END
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXSEAL_L_INSERT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_EXSEAL_L_INSERT
GO 
/**********************************************************************************************************  
 * 프로시저명: UP_TR_EXSEAL_L_INSERT
 * 관련페이지: 수출 >> 인수증등록 > 메인버튼저장
 * 작  성  자: 
 * EXEC UP_TR_EXSEAL_L_INSERT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXSEAL_L_INSERT 
	@CD_COMPANY		NVARCHAR(7),	--회사코드
	@NO_SEAL		NVARCHAR(20),	--인수증발급번호
	@NO_LINE		NUMERIC(5),		--마감라인
	@NO_IV			NVARCHAR(20),	--마감번호
	@NO_IVLINE		NUMERIC(5),		--마감라인
	@CD_PLANT		NVARCHAR(7),	--공장
	@NO_LC			NVARCHAR(20),	--LC번호
	@CD_ITEM		NVARCHAR(20),	--품목
	@QT_GI_CLS		NUMERIC(17,4),	--수량
	@CD_EXCH		NVARCHAR(3),	--환종
	@RT_EXCH		NUMERIC(11,4),	--환율
	@UM_EX_CLS		NUMERIC(17,4),	--외화단가
	@AM_EX_CLS		NUMERIC(17,4),	--외화금액
	@UM_ITEM_CLS	NUMERIC(17,4),	--원화단가
	@AM_CLS			NUMERIC(17,4),	--원화금액
	@QT_CLS			NUMERIC(17,4),	--관리수량
	@DTS_INSERT		NVARCHAR(14),	--등록자
	@ID_INSERT		NVARCHAR(15),	--등록일
	@DTS_UPDATE		NVARCHAR(14),	--수정자
	@ID_UPDATE		NVARCHAR(15)	--수정일
AS  
BEGIN
	IF NOT EXISTS(SELECT TOP 1 NO_SEAL FROM TR_SEALEXL WHERE CD_COMPANY = @CD_COMPANY AND NO_SEAL = @NO_SEAL AND NO_LINE = @NO_LINE)
	BEGIN
		INSERT INTO TR_SEALEXL( CD_COMPANY,		NO_SEAL,		NO_LINE,		NO_IV,			NO_IVLINE,		CD_PLANT,
							    NO_LC,			CD_ITEM,		QT_GI_CLS,		CD_EXCH,		RT_EXCH,
							    UM_EX_CLS,		AM_EX_CLS,		UM_ITEM_CLS,	AM_CLS,			QT_CLS,
							    DTS_INSERT,		ID_INSERT)
		VALUES( @CD_COMPANY,	@NO_SEAL,		@NO_LINE,		@NO_IV,			@NO_IVLINE,		@CD_PLANT,
			    @NO_LC,			@CD_ITEM,		@QT_GI_CLS,		@CD_EXCH,		@RT_EXCH,
			    @UM_EX_CLS,		@AM_EX_CLS,		@UM_ITEM_CLS,	@AM_CLS,		@QT_CLS,
			    @DTS_INSERT,	@ID_INSERT)
	END
	
	ELSE
	BEGIN
		UPDATE TR_SEALEXL
		SET
			NO_IV		= @NO_IV,			
			NO_IVLINE	= @NO_IVLINE,	
			CD_PLANT	= @CD_PLANT,
		    NO_LC		= @NO_LC,			
		    CD_ITEM		= @CD_ITEM,		
		    QT_GI_CLS	= @QT_GI_CLS,		
		    CD_EXCH		= @CD_EXCH,		
		    RT_EXCH		= @RT_EXCH,
			UM_EX_CLS	= @UM_EX_CLS,		
			AM_EX_CLS	= @AM_EX_CLS,		
			UM_ITEM_CLS	= @UM_ITEM_CLS,	
			AM_CLS		= @AM_CLS,			
			QT_CLS		= @QT_CLS,
			DTS_UPDATE	= @DTS_UPDATE,		
			ID_UPDATE	= @ID_UPDATE
		WHERE CD_COMPANY = @CD_COMPANY
		AND NO_SEAL = @NO_SEAL
		AND NO_LINE = @NO_LINE
	END
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXSEAL_DELETE') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_EXSEAL_DELETE
GO 
/**********************************************************************************************************  
 * 프로시저명: UP_TR_EXSEAL_DELETE
 * 관련페이지: 수출 >> 인수증등록 > 인수증등록 삭제
 * 작  성  자: 
 * EXEC UP_TR_EXSEAL_DELETE '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXSEAL_DELETE  
	@CD_COMPANY		NVARCHAR(7),	--회사코드
	@NO_SEAL		NVARCHAR(20)	--인수증번호
AS  
BEGIN
	DELETE FROM TR_SEALEXL
	WHERE CD_COMPANY = @CD_COMPANY
	AND NO_SEAL = @NO_SEAL
	
	DELETE FROM TR_SEALEXH
	WHERE CD_COMPANY = @CD_COMPANY
	AND NO_SEAL = @NO_SEAL
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXSEAL_JOIN_SELECT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_EXSEAL_JOIN_SELECT
GO 
/**********************************************************************************************************  
 * 프로시저명: UP_TR_EXSEAL_JOIN_SELECT
 * 관련페이지: 수출 >> 인수증등록 > 계산서적용
 * 작  성  자: 
 * EXEC UP_TR_EXSEAL_JOIN_SELECT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXSEAL_JOIN_SELECT  
	@CD_COMPANY		NVARCHAR(7),	--회사코드
	@NO_IV			NVARCHAR(200)	--계산서번호
AS  
BEGIN
	SELECT '' NO_SEAL,				--인수증번호
		'' NO_LINE,					--마감라인
		SL.NO_IV,					--마감번호
		SL.NO_LINE AS NO_IVLINE,	--마감라인
		SL.CD_PLANT,				--공장코드
		(SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = @CD_COMPANY AND CD_PLANT = SL.CD_PLANT AND CD_BIZAREA = SH.CD_BIZAREA) NM_PLANT,  --공장명
		SH.NO_LC,					--L/C번호
		SL.CD_ITEM,					--품번
		(SELECT NM_ITEM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = SL.CD_ITEM) NM_ITEM,  --품목명
		(SELECT STND_ITEM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = SL.CD_ITEM) STND_ITEM,  --규격
		(SELECT UNIT_IM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = SL.CD_ITEM) UNIT_IM,  --단위
		SL.QT_GI_CLS,				--마감수량(재고단위)		
		SL.CD_EXCH,					--환종
		SL.RT_EXCH,					--환율	
		SL.UM_EX_CLS,				--마감단가(외화)
		SL.AM_EX_CLS,				--마감금액(외화)		
		SL.UM_ITEM_CLS,				--마감단가(재고단위)
		SL.AM_CLS,					--마감금액(원화)	
		SL.QT_CLS,					--마감수량(수주단위)	
		'' DTS_INSERT,				--등록자
		'' ID_INSERT,				--등록일
		'' DTS_UPDATE,				--수정자
		'' ID_UPDATE				--수정일
	FROM SA_IVH SH
	LEFT OUTER JOIN SA_IVL SL ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_IV = SL.NO_IV
	INNER JOIN TR_LCEXH LC	ON SH.CD_COMPANY = LC.CD_COMPANY AND SH.NO_LC = LC.NO_LC AND LC.FG_LC = '003'
	WHERE SH.CD_COMPANY = @CD_COMPANY
	AND SH.NO_IV IN (SELECT * FROM TF_GETSPLIT(@NO_IV))
END
GO
