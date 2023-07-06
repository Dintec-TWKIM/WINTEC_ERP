USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_CHECKCREDIT_SELECT]    Script Date: 2019-11-14 오후 3:13:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************  
*********************************************/  
ALTER PROCEDURE [NEOE].[UP_SA_CHECKCREDIT_SELECT]  
(  
	@P_CD_COMPANY		NVARCHAR(7),  
	@P_CD_PARTNER		NVARCHAR(20),  
	@P_AM_SUM			NUMERIC(17, 4),			--원화금액 + 부가세  
	@P_STA_CHK			NVARCHAR(3),			--001 : 수주확정, 002 : 출하의뢰, 003 : 출하등록, 100 : 수주,의뢰, 101 : 수주,의뢰,출하, 102 : 의뢰,출하동시
	@P_CD_EXC           NVARCHAR(3)     = NULL, --000 : 전체수금액, 001 : 수금액 중 만기일 이전 어음 제외
	@P_DT_BASIC         NVARCHAR(8)     = NULL, --기준일
	@P_TOT_CREDIT		NUMERIC(17, 4)	OUTPUT,                 
	@P_JAN				NUMERIC(17, 4)	OUTPUT,	--잔액  
	@P_LVL_CHK			NVARCHAR(3)		OUTPUT	--체크레벨 (001 : NOCHECK, 002 : WARNING, 003 : ERROR)  
)  
AS  
SET NOCOUNT OFF  
  
DECLARE	@V_GRP_CREDIT	NVARCHAR(3),	--여신그룹  
		@V_STA_CHK		NVARCHAR(3),	--체크시점 (001 : 수주확정, 002 : 출하의뢰, 003 : 출하등록)  
		@V_DATE			NCHAR(8),		--금일날짜  
		@V_YESTERDAY	NCHAR(8),		--금일하루전날  
		@V_YY			NCHAR(4),		--금일년도  
		@V_YY_1			NCHAR(8),		--금일년도 + '0000'  
		@V_YY_FIRST		NCHAR(8),		--금일년도01월01일  
		@V_ARD_JAN		NUMERIC(17, 4),	--채권잔액  
		@V_SERVER_KEY	NCHAR(1),
		@V_SECURITY_DATE_FR NVARCHAR(8),    --담보등록 만기일FR
		@V_SECURITY_DATE    NVARCHAR(8),    --담보등록 만기일
		@V_SECURITY_AM      NUMERIC(17, 4), --담보금액
		
		@V_PROMISSORY_NOTE  NUMERIC(17,4),  --수금액 중 만기일 이전 어음
        @V_SERVER_KEY2      VARCHAR(50)
		
SET		@V_GRP_CREDIT	= NULL  
SET		@V_STA_CHK		= NULL  
SET		@P_LVL_CHK		= NULL  
SET		@V_DATE			= CONVERT(NVARCHAR(30), GETDATE(),112)  
SET		@V_YESTERDAY	= CONVERT(NVARCHAR(30), DATEADD(DD, -1, @V_DATE), 112)  
SET		@V_YY			= SUBSTRING(@V_DATE, 1 ,4)  
SET		@V_YY_1			= @V_YY + '0000'  
SET		@V_YY_FIRST		= @V_YY + '0101'  
SET		@P_TOT_CREDIT	= NULL  
SET		@V_ARD_JAN		= 0  

SELECT @V_SERVER_KEY = NEOE.FN_SERVER_KEY('DZSQL')  

SELECT  TOP 1 @V_SERVER_KEY2 = SERVER_KEY
FROM    CM_SERVER_CONFIG
WHERE   YN_UPGRADE      = 'Y'

--에스피더블유코리아유한회사에서는 수주등록에서 별도의 여신체크를 쓰고 의뢰, 출하에서는 여신체크를 하지 않는다.
IF (@V_SERVER_KEY2 = 'SPK')
BEGIN
    RETURN
END

--해당 거래처(MA_PARTNER)의 여신여부(FG_CREDIT)가 Y이면 거래처여신정보(SA_PTRCREDIT)의 여신그룹(GRP_CREDIT)을 가져온다.  
SELECT	@V_GRP_CREDIT = A.GRP_CREDIT  
FROM	SA_PTRCREDIT A   
		INNER JOIN MA_PARTNER B ON B.CD_COMPANY = @P_CD_COMPANY AND B.CD_PARTNER = @P_CD_PARTNER
WHERE	A.CD_COMPANY    = @P_CD_COMPANY  
AND		A.CD_PARTNER    = @P_CD_PARTNER
AND     B.FG_CREDIT     = 'Y'
  
--여신그룹이 널이 아니면  
IF (@V_GRP_CREDIT IS NOT NULL)  
BEGIN  
	SELECT	@V_STA_CHK	= STA_CHK,  
			@P_LVL_CHK	= LVL_CHK  
	FROM	SA_GRPCREDIT  
	WHERE	CD_COMPANY	= @P_CD_COMPANY  
	AND     GRP_CREDIT	= @V_GRP_CREDIT  

		--여신그룹의 체크시점이 해당 호출 프로시져의 체크시점과 같으면  
		IF ((@P_STA_CHK = @V_STA_CHK) OR (@P_STA_CHK = '100' AND @V_STA_CHK IN ('001', '002'))
									  OR (@P_STA_CHK = '101' AND @V_STA_CHK IN ('001', '002', '003'))
									  OR (@P_STA_CHK = '102' AND @V_STA_CHK IN ('002', '003'))
									  OR (@P_STA_CHK = '001' AND @V_STA_CHK = '000'))  
		BEGIN  
			IF (@P_LVL_CHK = '002' OR @P_LVL_CHK = '003')  
			BEGIN  
				--채권잔액 구하는 쿼리  
				SELECT	@V_ARD_JAN = SUM(기초채권1) + SUM(기초채권2) + SUM(채권발생액) - SUM(수금액)  
				FROM	(  
					--SELECT	CD_PARTNER,   
					--		AM_GI 기초채권1,  
					--		0 기초채권2,  
					--		0 채권발생액,  
					--		0 수금액  
					--FROM	SA_ARD   
					--WHERE	CD_COMPANY = @P_CD_COMPANY  
					--AND		CD_PARTNER = @P_CD_PARTNER  
					--AND		DT_AR = @V_YY_1                   

					 SELECT CD_PARTNER,   
							(AM_GI + AM_GI_SUB) 기초채권1,  
							0 기초채권2,  
							0 채권발생액,  
							0 수금액  
					 FROM   SA_AR   
					 WHERE  CD_COMPANY = @P_CD_COMPANY  
					 AND    CD_PARTNER = @P_CD_PARTNER  
					 AND    YYMM = @V_YY + '00'

					UNION ALL

					SELECT	CD_PARTNER,   
							0 기초채권1,  
							SUM((AM_GI + AM_GI_SUB) - AM_RCP) 기초채권2,  
							0 채권발생액,  
							0 수금액  
					FROM	SA_ARD   
					WHERE	CD_COMPANY = @P_CD_COMPANY  
					AND		CD_PARTNER = @P_CD_PARTNER  
					AND		DT_AR BETWEEN @V_YY_FIRST AND @V_YESTERDAY
					GROUP BY CD_PARTNER  

					UNION ALL  

					SELECT	CD_PARTNER,   
							0 기초채권1,  
							0 기초채권2,  
							SUM((AM_GI + AM_GI_SUB)) 채권발생액,  
							SUM(AM_RCP) 수금액  
					FROM	SA_ARD   
					WHERE	CD_COMPANY = @P_CD_COMPANY  
					AND		CD_PARTNER = @P_CD_PARTNER  
					AND		DT_AR = @V_DATE  
					GROUP BY CD_PARTNER  
				) A  
				GROUP BY CD_PARTNER          
				
				IF (@V_SERVER_KEY = 'Y')
				BEGIN
					SELECT  @V_SECURITY_DATE_FR = SECURITY_DATE_FR,
							@V_SECURITY_DATE = SECURITY_DATE,
							@V_SECURITY_AM = SECURITY_AM
					FROM SA_SECURITY_PEACE
					WHERE CD_COMPANY = @P_CD_COMPANY
						AND CD_PARTNER = @P_CD_PARTNER
						AND	NO_LINE = (SELECT MAX(NO_LINE) FROM SA_SECURITY_PEACE
															WHERE CD_COMPANY = @P_CD_COMPANY
																AND CD_PARTNER = @P_CD_PARTNER )
					--IF (@V_SECURITY_DATE_FR > @V_DATE AND @V_SECURITY_DATE < @V_DATE)
					IF (@V_SECURITY_DATE < @V_DATE)
					BEGIN
						UPDATE SA_PTRCREDIT
						SET AM_REAL = 0, 
							TOT_CREDIT = AM_CREDIT + 0
						WHERE CD_COMPANY = @P_CD_COMPANY
							AND CD_PARTNER = @P_CD_PARTNER
					END
				END
				
				--거래처여신정보(SA_PTRCREDIT)의 여신총액(TOT_CREDIT)을 가져온다.  
				SELECT	@P_TOT_CREDIT = TOT_CREDIT  
				FROM	SA_PTRCREDIT   
				WHERE	CD_COMPANY = @P_CD_COMPANY  
				AND		CD_PARTNER = @P_CD_PARTNER  
				
				IF (@P_CD_EXC = '001')
				BEGIN
					--수금액 중 만기일 이전의 어음
					SELECT  @V_PROMISSORY_NOTE = ISNULL(SUM(L.AM_RCP + L.AM_RCP_A), 0)
					FROM    SA_RCPL L
							INNER JOIN SA_RCPH H ON L.CD_COMPANY = H.CD_COMPANY AND L.NO_RCP = H.NO_RCP
					WHERE   L.CD_COMPANY = @P_CD_COMPANY
					AND     H.CD_PARTNER = @P_CD_PARTNER
					AND     L.FG_RCP = '003'
					AND     DT_DUE > @P_DT_BASIC

					--여신총액 - 채권잔액 - 금번처리총액(원화금액 + 부가세) > 0이면 통과  
					IF (@P_TOT_CREDIT - (@V_ARD_JAN + @V_PROMISSORY_NOTE) - @P_AM_SUM < 0)   
					BEGIN  
						--SET	@P_JAN = @P_TOT_CREDIT - @V_ARD_JAN  
						SET	@P_TOT_CREDIT = @P_TOT_CREDIT - (@V_ARD_JAN + @V_PROMISSORY_NOTE)  
						SET	@P_JAN = @P_AM_SUM  
						RETURN  
					END  
					ELSE  
					BEGIN  
						SET	@P_TOT_CREDIT = NULL  
					END
				END
				ELSE
				BEGIN
					--여신총액 - 채권잔액 - 금번처리총액(원화금액 + 부가세) > 0이면 통과  
					IF (@P_TOT_CREDIT - @V_ARD_JAN - @P_AM_SUM < 0)   
					BEGIN  
						--SET	@P_JAN = @P_TOT_CREDIT - @V_ARD_JAN  
						SET	@P_TOT_CREDIT = @P_TOT_CREDIT - @V_ARD_JAN  
						SET	@P_JAN = @P_AM_SUM  
						RETURN  
					END  
					ELSE  
					BEGIN  
						SET	@P_TOT_CREDIT = NULL  
					END
				END
			END  
		END  
	END
  
  
SET NOCOUNT ON
GO

