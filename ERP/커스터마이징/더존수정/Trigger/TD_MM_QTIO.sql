USE [NEOE]
GO
/****** Object:  Trigger [NEOE].[TD_MM_QTIO]    Script Date: 2017-03-15 오후 4:41:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  TRIGGER [NEOE].[TD_MM_QTIO] ON [NEOE].[MM_QTIO] FOR DELETE AS                        
BEGIN                        
    DECLARE @ERRNO              INT,                      
            @ERRMSG             NVARCHAR(255),                  
            @NUMROWS            INT,                  
            @NO_LOT             NVARCHAR(50),                   
            
            @NO_IO              NVARCHAR(20),   --수불번호                    
            @NO_IOLINE          NUMERIC(5),     --수불라인번호                    
            @CD_COMPANY         NVARCHAR(7),    --회사                    
            @YN_RETURN          NCHAR(1),       --반품유무                  
            @CD_SL              NVARCHAR(7),    --보관SL                  
            @FG_PS              NCHAR(1),       --입출고구분                  
            @YN_AM              NCHAR(1),       --유무환구분                  
            @DT_IO              NCHAR(8),       --수불일                  
            @CD_PLANT           NVARCHAR(7),    --공장                  
            @CD_ITEM            NVARCHAR(50),   --품목코드                  
            @FG_IO              NCHAR(3),       --수불구분                  
            @CD_QTIOTP          NCHAR(3),       --수불형태코드                  
            @FG_TRANS           NCHAR(3),       --거래구분                  
            @CD_PARTNER         NVARCHAR(20),   --거래처코드                  
            @CD_BIZAREA         NVARCHAR(7),    --사업장                  
            @QT_GOOD_INV        NUMERIC(17,4),  --양품재고                  
            @QT_REJECT_INV      NUMERIC(17,4),  --불량재고                  
            @QT_TRANS_INV       NUMERIC(17,4),  --이동중재고                  
            @QT_INSP_INV        NUMERIC(17,4),  --검사재고                  
            @QT_IO              NUMERIC(17,4),  --수불수량                  
            @QT_CLS             NUMERIC(17,4),  --마감수량                   
            @AM                 NUMERIC(17,4),  --원화금액                  
            @VAT                NUMERIC(17,4),  --부가세                  
            @CD_GROUP           NVARCHAR(7),    --그룹                  
            @YY                 NCHAR(4),                    
            @YM                 NCHAR(6),                  
            @CD_BIN_REF         NVARCHAR(7),    --화면적용구분                    
            @NO_ISURCV          NVARCHAR(20),   --의뢰번호                  
            @NO_ISURCVLINE      NUMERIC(5,0),   --의뢰항번                  
            @NO_PSO_MGMT        NVARCHAR(20),   --수주번호                  
            @NO_PSOLINE_MGMT    NUMERIC(5,0),   --수주항번                  
            @NO_IO_MGMT         NVARCHAR(20),                  
            @NO_IOLINE_MGMT     NUMERIC(5,0),                  
            @QT_UNIT_MM         NUMERIC(17,4),  --실출고수량,                  
            @AM_EX              NUMERIC(19,6),                  
            @NO_LC              NVARCHAR(20),   --LC번호                  
            @NO_LCLINE          NUMERIC(5),     --LC라인                  
            @QT_RETURN          NUMERIC(17,4),                   
            
            @NO_PO_RETURN       NVARCHAR(20),   --발주번호_반품                  
            @NO_POLINE_RETURN   NUMERIC(17,4),  --발주항번_반품                  
            
            @QT_PINVN           NUMERIC(17,4),  --현재고                   
            @P_CD_PJT           NVARCHAR(20),   --프로젝트코드                   
            @P_CLS_ITEM         NVARCHAR(3),    --계정구분                  
            @DTS_DELETE         NVARCHAR(14) ,                  
            @V_YN_INSPECT       NVARCHAR(1) ,                  
            @V_NO_POP           NVARCHAR(30),                  
            @V_NO_POP_LINE      NUMERIC(5,0),
            @V_NO_DLV           NVARCHAR(20),    
            @V_NO_DLVLINE		NUMERIC(5,0),                     
            @V_FG_PROCESS       NVARCHAR(3),                  
            
            @P_SEQ_PROJECT      NUMERIC(5,0),                  
            @V_YN_PJT           NVARCHAR(1) ,                  
            @V_YN_UNIT          NVARCHAR(1),                    
            @ERRMSG3    NVARCHAR(255),                     
            @V_CD_COMPNAY       NVARCHAR(7)      ,            
            @P_FG_SLQC   NVARCHAR(1), ---품목테이블 검사처리유무            
		    @V_SL_YN_QC      NVARCHAR(1), ---창고테이블 검사처리유무            
		    @P_YN_QC_FIX        NVARCHAR(1), -- 외주 검사처리 유무        
		    @V_FG_PS            NCHAR(1)  ,  
            @V_FG_BLOCK   NVARCHAR(1),  
            @V_DT_BLOCK   NVARCHAR(8),  
            @V_DAYS_BLOCK  NUMERIC(7),  
            @V_INSPECT_CHECK NVARCHAR(3),
            @V_NO_PR		NVARCHAR(20),
            @V_NO_PR_LINE	NUMERIC(5,0),
            @V_NO_WORK		NVARCHAR(20),
            @V_NO_IO_MGMT_PR		NVARCHAR(20),
            @V_NO_IOLINE_MGMT_PR	NUMERIC(5,0),
            @V_SERVER_KEY       VARCHAR(50),
            @V_YN_PMS	    	NVARCHAR(1),  
			@V_CD_CSTR			NVARCHAR(10),
			@V_DL_CSTR			NVARCHAR(5),
			@V_DT_PMS			NCHAR(8),
            @V_PMS_AM			NUMERIC(17,4),
            @V_PMS_QT			NUMERIC(17,4),
            @V_CD_EXC_SA_LINK	NVARCHAR(4),
            @V_CD_EXC_PU_LINK	NVARCHAR(4),
            @V_CD_EXC_EC_LINK	NVARCHAR(4),
            @V_CD_EXC_POP		NVARCHAR(4),
            @V_LINK_CNT			INT,
            @V_LINK_CNT_TEMP	INT,
            @V_NO_TRACK         NVARCHAR(40),
			@V_NO_TRACK_LINE    NUMERIC(5,0),
			@V_MA_EXC_LEGACY    NVARCHAR(3),
			@V_YN_SEND_LEGACY   NCHAR(1),
			@V_CD_LOCATION		NVARCHAR(20)
            
            
    SELECT  @NUMROWS = @@ROWCOUNT                  
                
    IF @NUMROWS = 0            
        RETURN                  
         
    SET @V_YN_INSPECT = NULL                  
            
    -->> 10. 헤더체크(MM_QTIOH)                  
    IF (SELECT COUNT(*) FROM MM_QTIOH H INNER JOIN DELETED D ON (H.NO_IO = D.NO_IO AND H.CD_COMPANY = D.CD_COMPANY) ) != @NUMROWS                    
    BEGIN                  
        SELECT  @ERRNO  = 100000,            
                @ERRMSG = 'PARENT DOES NOT EXIST IN "MM_QTIOH". CANNOT DELETE CHILD IN "MM_QTIO".'            
        GOTO ERROR2            
    END            
            
    IF EXISTS   (   SELECT  1            
                    FROM    DELETED            
                    WHERE   QT_IMSEAL > 0   )            
    BEGIN            
        SELECT  @ERRNO  = 100000,            
                @ERRMSG = 'TR_M000095' --인수증이 등록되었으므로 삭제불가합니다            
        GOTO ERROR2            
    END            
            
    --<< 10. 헤더체크(MM_QTIOH)            
    SELECT  @V_CD_COMPNAY  =  CD_COMPANY            
    FROM    DELETED            
            
    -->>  시스템환경설정 (PJT형,UNIT사용여부)                    
    --->> @V_YN_PJT: PJT형사용여부 (Y:사용,N미사용) ,  @V_YN_UNIT:UNIT사용여부 (Y:사용,N미사용)            
    EXEC UP_SF_GET_MA_ENV @V_CD_COMPNAY , '',  @V_YN_PJT OUTPUT , @V_YN_UNIT OUTPUT            
    --<<            
	SELECT  @V_YN_PMS = YN_PMS FROM MA_ENV AS A WHERE A.CD_COMPANY = @V_CD_COMPNAY 
	
    SELECT  @V_SERVER_KEY = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG WHERE YN_UPGRADE = 'Y'
		
	SELECT @V_MA_EXC_LEGACY = CD_EXC     
	FROM   MA_EXC   
	WHERE  CD_COMPANY = @V_CD_COMPNAY AND EXC_TITLE = '물류데이터전송연동-확정로직 사용유무'         
    
    SET @V_MA_EXC_LEGACY = CASE WHEN ISNULL(@V_MA_EXC_LEGACY,'') = '' THEN '000' ELSE @V_MA_EXC_LEGACY END
    
    DECLARE CUR_TD_MM_QTIO CURSOR FAST_FORWARD FOR            
    SELECT  H.YN_RETURN, D.NO_IO, D.NO_IOLINE, D.CD_COMPANY, D.CD_SL, D.FG_PS, D.YN_AM, D.DT_IO, D.CD_PLANT, D.CD_ITEM,            
            D.FG_IO, D.CD_QTIOTP, D.FG_TRANS, D.CD_PARTNER, D.CD_BIZAREA, -D.QT_GOOD_INV, -D.QT_REJECT_INV, -D.QT_TRANS_INV, -D.QT_INSP_INV, -D.QT_IO,                   
            -D.AM, -D.VAT, D.CD_GROUP, D.QT_CLS, D.CD_BIN_REF, D.NO_ISURCV, D.NO_ISURCVLINE, D.NO_PSO_MGMT, D.NO_PSOLINE_MGMT,            
            -D.QT_UNIT_MM, -D.AM_EX, D.NO_LC, D.NO_LCLINE, D.QT_RETURN, D.NO_IO_MGMT, D.NO_IOLINE_MGMT,            
            D.CD_PJT,            
            P.CLS_ITEM ,D.YN_INSPECT, D.NO_POP, D.NO_POP_LINE, D.SEQ_PROJECT, P.FG_SLQC, D.NO_DLV, D.NO_DLVLINE, D.NO_WORK, PC.CD_CSTR, PC.DL_CSTR,
            D.NO_TRACK, D.NO_TRACK_LINE, CASE WHEN ISNULL(D.YN_SEND_LEGACY,'') = '' THEN 'N' ELSE D.YN_SEND_LEGACY END, LC.CD_LOCATION
    FROM    MM_QTIOH H            
            INNER JOIN DELETED D ON H.NO_IO = D.NO_IO AND H.CD_COMPANY = D.CD_COMPANY
			LEFT OUTER JOIN MM_QTIO_LOCATION LC ON LC.CD_COMPANY = D.CD_COMPANY AND LC.NO_IO = D.NO_IO AND LC.NO_IOLINE = D.NO_IOLINE            
            LEFT OUTER JOIN MA_PITEM P ON D.CD_COMPANY = P.CD_COMPANY AND D.CD_PLANT = P.CD_PLANT AND D.CD_ITEM = P.CD_ITEM            
            LEFT OUTER JOIN PJ_CBS PC ON D.CD_COMPANY = PC.CD_COMPANY AND D.CD_PJT = PC.NO_PROJECT AND D.NO_CBS = PC.NO_CBS                  
    OPEN  CUR_TD_MM_QTIO            
    FETCH NEXT FROM CUR_TD_MM_QTIO            
    INTO    @YN_RETURN, @NO_IO, @NO_IOLINE, @CD_COMPANY, @CD_SL, @FG_PS, @YN_AM, @DT_IO, @CD_PLANT, @CD_ITEM,            
            @FG_IO, @CD_QTIOTP, @FG_TRANS, @CD_PARTNER, @CD_BIZAREA, @QT_GOOD_INV, @QT_REJECT_INV, @QT_TRANS_INV, @QT_INSP_INV, @QT_IO,            
            @AM, @VAT, @CD_GROUP, @QT_CLS, @CD_BIN_REF, @NO_ISURCV, @NO_ISURCVLINE, @NO_PSO_MGMT, @NO_PSOLINE_MGMT,            
            @QT_UNIT_MM, @AM_EX, @NO_LC, @NO_LCLINE, @QT_RETURN, @NO_IO_MGMT, @NO_IOLINE_MGMT,            
            @P_CD_PJT,            
            @P_CLS_ITEM, @V_YN_INSPECT, @V_NO_POP, @V_NO_POP_LINE, @P_SEQ_PROJECT, @P_FG_SLQC, @V_NO_DLV, @V_NO_DLVLINE, @V_NO_WORK, @V_CD_CSTR, @V_DL_CSTR,
            @V_NO_TRACK, @V_NO_TRACK_LINE, @V_YN_SEND_LEGACY, @V_CD_LOCATION
            
    WHILE (@@FETCH_STATUS <> -1)            
    BEGIN            
        IF (@@FETCH_STATUS <> -2)            
        BEGIN            
            IF ISNULL(@P_CD_PJT, '') = ''       SELECT @P_CD_PJT = ''            
            IF ISNULL(@P_SEQ_PROJECT, 0 ) = 0   SELECT @P_SEQ_PROJECT = 0    
            
            IF ISNULL(@V_NO_POP,'') <> ''
            BEGIN
				SET @V_LINK_CNT = 0
				SET @V_LINK_CNT_TEMP = 0
				--영업연동-ERP삭제가능유무 000 : 가능 , 100 : 불가능
				SELECT  @V_CD_EXC_SA_LINK = CD_EXC
				FROM    MA_EXC
				WHERE   CD_COMPANY  = @CD_COMPANY
				AND     EXC_TITLE   = '영업연동-ERP삭제가능유무'
				SET     @V_CD_EXC_SA_LINK   = ISNULL(@V_CD_EXC_SA_LINK, '000') 
	            
				SELECT  @V_CD_EXC_PU_LINK   = CD_EXC
				FROM    MA_EXC
				WHERE   CD_COMPANY  = @CD_COMPANY
				AND     EXC_TITLE   = '구매연동-ERP삭제가능유무'
				SET     @V_CD_EXC_PU_LINK   = ISNULL(@V_CD_EXC_PU_LINK, '000') 
	            
				SELECT  @V_CD_EXC_POP   = CD_EXC
				FROM    MA_EXC
				WHERE   CD_COMPANY  = @CD_COMPANY
				AND     EXC_TITLE   = 'POP연동-ERP삭제가능유무'
				SET     @V_CD_EXC_POP   = ISNULL(@V_CD_EXC_POP, '000') 
				
				SELECT  @V_CD_EXC_EC_LINK   = CD_EXC
				FROM    MA_EXC
				WHERE   CD_COMPANY  = @CD_COMPANY
				AND     EXC_TITLE   = 'EC연동-ERP삭제가능유무'
				SET     @V_CD_EXC_EC_LINK   = ISNULL(@V_CD_EXC_EC_LINK, '000') 
	            
				IF (@V_CD_EXC_SA_LINK = '100') 
				BEGIN 
					SELECT  @V_LINK_CNT_TEMP	= COUNT(1 )
					FROM SA_LINK 
					WHERE CD_COMPANY = @CD_COMPANY AND NO_LINK = @V_NO_POP AND NO_LINE_LINK = @V_NO_POP_LINE
					SET @V_LINK_CNT = @V_LINK_CNT + ISNULL(@V_LINK_CNT_TEMP,0)
				END
				
				IF (@V_CD_EXC_PU_LINK = '100') 
				BEGIN 
					SELECT  @V_LINK_CNT_TEMP	= COUNT(1 )
					FROM PU_LINK 
					WHERE CD_COMPANY = @CD_COMPANY AND NO_LINK = @V_NO_POP AND NO_LINE_LINK = @V_NO_POP_LINE
					SET @V_LINK_CNT = @V_LINK_CNT + ISNULL(@V_LINK_CNT_TEMP,0)
				END
				IF (@V_CD_EXC_POP = '100') 
				BEGIN 
					SELECT  @V_LINK_CNT_TEMP	= COUNT(1 )
					FROM MM_QTIO_POP
					WHERE CD_COMPANY = @CD_COMPANY AND NO_POP = @V_NO_POP AND NO_POP_LINE = @V_NO_POP_LINE
					SET @V_LINK_CNT = @V_LINK_CNT + ISNULL(@V_LINK_CNT_TEMP,0)
				END
				IF (@V_CD_EXC_EC_LINK = '100') 
				BEGIN 
					SELECT  @V_LINK_CNT_TEMP	= COUNT(1 )
					FROM EC_LINKL 
					WHERE CD_COMPANY = @CD_COMPANY AND NO_LINK = @V_NO_POP AND NO_LINE = @V_NO_POP_LINE
					SET @V_LINK_CNT = @V_LINK_CNT + ISNULL(@V_LINK_CNT_TEMP,0)
				END
	            
				SET @V_LINK_CNT = ISNULL(@V_LINK_CNT,0)
				
				IF (@V_LINK_CNT > 0 AND (@V_CD_EXC_SA_LINK = '100' OR @V_CD_EXC_PU_LINK = '100' OR @V_CD_EXC_POP = '100' ))
				BEGIN
					IF ISNULL(@V_NO_POP, '') <> ''
					BEGIN
						SELECT @ERRNO = 100000, @ERRMSG = '연동된 데이터 입니다. 삭제 할 수 없습니다.(MM_QTIO) ' GOTO ERROR
					END
				END         
            END
            
            	
			IF(@V_YN_SEND_LEGACY = 'Y' AND @V_MA_EXC_LEGACY NOT IN ('000'))
			BEGIN
				SELECT @ERRNO = 100000, @ERRMSG = 'LEGACY 연동 확정[Release]된 상태 입니다. 확인해주세요 ['+ @NO_IO + '  /  '+ STR(@NO_IOLINE)+']'  GOTO ERROR
			END	
  
    
-->> 20. 마감통제 검사            
            IF EXISTS( SELECT 1 FROM MM_CONTROL WHERE CD_COMPANY = @CD_COMPANY AND CD_PLANT = @CD_PLANT AND YM_CONTROL >= SUBSTRING(@DT_IO, 1, 6))                  
            BEGIN                  
                SELECT @ERRNO  = 100000, @ERRMSG = '마감통제가 걸려있습니다.' GOTO ERROR                  
            END                  
            --<< 20. 마감통제 검사            
   
            -->> 21. (-)재고통제 검사  2008.07.25            
   IF (@P_CLS_ITEM <= '005' OR  @P_CLS_ITEM = '009') /*계정구분이 001-005 대상만 저장품추가 20090504*/            
            BEGIN            
                IF EXISTS   (   SELECT  1            
                                FROM    MA_CONTROL_STOCK            
                                WHERE   CD_COMPANY = @CD_COMPANY            
                                AND     CD_PLANT   = @CD_PLANT            
                                AND     CD_SL      = @CD_SL            
                                AND     YN_STOCK   = 'Y'        ) --AND (@FG_PS + @YN_RETURN = '2N' OR @FG_PS + @YN_RETURN = '1Y')            
    BEGIN            
                  SELECT @QT_PINVN = 0            
            
                    SELECT  @QT_PINVN = SUM((QT_GOOD_OPEN + QT_REJECT_OPEN + QT_INSP_OPEN + QT_TRANS_OPEN)            
                            + (QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR + QT_TRANS_GR)            
                            - (QT_GOOD_GI + QT_REJECT_GI + QT_INSP_GI + QT_TRANS_GI))            
                    FROM    MM_PINVN                  
                    WHERE   P_YR  = LEFT(@DT_IO, 4)            
                    AND     CD_COMPANY = @CD_COMPANY            
                    AND     CD_PLANT   = @CD_PLANT            
                    AND     CD_SL      = @CD_SL            
                    AND     CD_ITEM    = @CD_ITEM            
                    AND     ((@V_YN_PJT = 'N') OR (@V_YN_PJT = 'Y' AND CD_PJT = @P_CD_PJT))            
                    AND     ((@V_YN_UNIT = 'N') OR (@V_YN_UNIT = 'Y' AND SEQ_PROJECT = @P_SEQ_PROJECT))            
            
                    IF (@QT_PINVN IS NULL )  SELECT @QT_PINVN = 0            
            
                    IF @QT_PINVN - ( CASE WHEN @FG_PS + @YN_RETURN = '2N' OR @FG_PS + @YN_RETURN = '1Y' THEN  @QT_IO ELSE 0 - @QT_IO END ) < 0                  
                    BEGIN            
                        SELECT  @ERRMSG3 = ''            
            
                        IF (@V_YN_PJT = 'Y')            
                        BEGIN            
                            SELECT  @ERRMSG3 = @P_CD_PJT            
            
                            IF (@V_YN_UNIT = 'Y')            
                            BEGIN            
                                SET @ERRMSG3 = @ERRMSG3 + ' ' + CONVERT(NVARCHAR, @P_SEQ_PROJECT)            
                            END            
                        END            
            
                        SELECT  @ERRNO  = 100000,            
                                @ERRMSG = @CD_PLANT + ' ' + @CD_SL + ' ' + @CD_ITEM + ' ' + @ERRMSG3 + '  : (-) 재고통제가 걸려있습니다. '            
                        GOTO ERROR            
                    END            
                END            
            END            
            --<< 21. (-)재고통제 검사            
            
            
            --<<일자별재고수불통제 검사  
               
          IF EXISTS ( SELECT 1 FROM MM_CONTROL_SLDAY     
                        WHERE   CD_COMPANY = @CD_COMPANY      
                        AND     CD_PLANT   = @CD_PLANT      
                        AND     CD_SL      = @CD_SL    )      
          BEGIN  
   
      SELECT  @V_FG_BLOCK = FG_BLOCK,  
        @V_DT_BLOCK = DT_BLOCK,  
        @V_DAYS_BLOCK = DAYS_BLOCK  
      FROM MM_CONTROL_SLDAY     
     WHERE   CD_COMPANY = @CD_COMPANY      
     AND     CD_PLANT   = @CD_PLANT      
     AND     CD_SL      = @CD_SL    
       
     IF(@V_FG_BLOCK = 'D') ---통제일자기준  
     BEGIN  
       
     IF(@DT_IO <= @V_DT_BLOCK)  
     BEGIN  
       SELECT @ERRNO  = 100000, @ERRMSG = '일자별재고수불통제가 걸려있습니다. ' GOTO ERROR      
     END  
     END  
     ELSE IF(@V_FG_BLOCK = 'T')---현재일기준으로 몇일전(@V_DAYS_BLOCK)부터 통제를 하는지 결정  
     BEGIN
		--
		-- 2015.08.03 D20150803081 김현수, 허상규
		-- 덕양산업의 경우 통제된 날짜로부터
		-- 8시간 이내에는 수불을 생성할 수 있다.
		--
		IF (@V_SERVER_KEY LIKE 'DUCKYANG%')
		BEGIN
			-- 현재일 기준 8시간 추가해줌
			DECLARE @DT_IO_CKD	NCHAR(8),
				@DT_IO_CKD_CAL	NCHAR(8)
			
			SELECT @DT_IO_CKD = CONVERT(NVARCHAR, DATEADD(DAY, @V_DAYS_BLOCK * -1, CONVERT(NVARCHAR, DATEADD(HOUR, -8, GETDATE()),112) ),112) 
			
			IF NOT EXISTS
			(
				SELECT 1 FROM PR_PLANT_CALENDAR WHERE CD_COMPANY = @CD_COMPANY AND CD_PLANT = @CD_PLANT AND DT_CAL = @DT_IO_CKD
			) 
			BEGIN
				-- 토요일 휴일로 설정함
				EXEC UP_PR_PLANT_CALENDAR_CH_I @CD_COMPANY, @CD_PLANT, @DT_IO_CKD, 'DUZON', 'H'
			END

			SELECT @DT_IO_CKD_CAL = MAX(DT_CAL)  
			FROM PR_PLANT_CALENDAR 
			WHERE CD_COMPANY = @CD_COMPANY AND CD_PLANT = @CD_PLANT
			AND DT_CAL <= @DT_IO_CKD
			AND FG_CAL <> 'H' 
			GROUP BY CD_COMPANY, CD_PLANT

			--IF(@DT_IO <= @DT_IO_CKD_CAL)
			IF(@DT_IO < @DT_IO_CKD_CAL)
			BEGIN
				 SELECT @ERRNO  = 100000, @ERRMSG = NEOE.FN_CONVERT_CM_MESSAGE_NEW('CM_CN00001', @CD_COMPANY)
				 GOTO ERROR    
			END
		END
		
		ELSE
		BEGIN
			IF(@DT_IO <=  CONVERT(NVARCHAR, DATEADD(DAY, @V_DAYS_BLOCK * -1, GETDATE()),112)  )  
			BEGIN  
				SELECT @ERRNO  = 100000, @ERRMSG = '일자별재고수불통제가 걸려있습니다. ' GOTO ERROR      
			END  
		END
     END  
            
              
              
          END   
            -->>일자별재고수불통제 검사  
            
            
            
            
            -->> 마감이 된것은 삭제 불가  2016.04.05(인산가)사용안함.  OR @FG_IO = '010' OR @FG_IO = '041'  
			IF ( @V_SERVER_KEY LIKE 'INSANGA%' )
            BEGIN            
				IF ((@FG_IO = '001' OR @FG_IO = '030' OR @FG_IO = '031' OR @FG_IO = '005' OR @FG_IO = '051') AND ISNULL(@QT_CLS, 0) <> 0 )
				BEGIN            
					SELECT @ERRNO  = 100000, @ERRMSG = 'PU_M000056' GOTO ERROR            
				END
			END	
            ELSE 
            BEGIN
				IF ((@FG_IO = '001' OR @FG_IO = '030' OR @FG_IO = '031' OR @FG_IO = '010' OR @FG_IO = '041' OR @FG_IO = '005' OR @FG_IO = '051') AND ISNULL(@QT_CLS, 0) <> 0)            
				BEGIN            
					SELECT @ERRNO  = 100000, @ERRMSG = 'PU_M000056' GOTO ERROR            
				END            
			END
			            
            IF ((SELECT COUNT(*) FROM TR_INVL WHERE CD_COMPANY = @CD_COMPANY AND NO_QTIO = @NO_IO) > 0)            
            BEGIN            
                SELECT @ERRNO  = 100000, @ERRMSG = '이미 송장등록 되었습니다.' GOTO ERROR            
            END            
            -->>     
            
             IF (@FG_IO = '022' OR @FG_IO = '010' OR @FG_IO = '041')
			 BEGIN
				  IF(  ISNULL(@V_NO_DLV ,'') <> '' AND ISNULL(@V_NO_DLVLINE,0) > 0)    
				  BEGIN    
					UPDATE SA_DLV_L SET QT_IO = ISNULL(QT_IO, 0) + @QT_IO, QT_IO_MM = ISNULL(QT_IO_MM,0) + @QT_UNIT_MM WHERE CD_COMPANY = @CD_COMPANY AND NO_DLV = @V_NO_DLV AND NO_LINE = @V_NO_DLVLINE
					AND (( @FG_IO = '022' AND @FG_PS = '2') OR (@FG_IO = '010' OR @FG_IO = '041') )
					IF (@@ERROR <> 0 ) BEGIN SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR END    
				  END
			 END                      
            
            --<< 30. 잔량관리            
			-- 2016.04.05 업체(인산가) 사용안함.         
            --IF (@FG_IO = '010' OR @FG_IO = '041' OR @FG_IO = '042')  --판매일경우동작(판매반품정리(042) 추가 2012.05.11 SJH)              
            IF ( (@FG_IO = '010' OR @FG_IO = '041' OR @FG_IO = '042') AND @V_SERVER_KEY NOT LIKE 'INSANGA%' ) --판매일경우동작(판매반품정리(042) 추가 2012.05.11 SJH)              
            BEGIN            
                IF EXISTS (SELECT 1 FROM SA_IVL WHERE CD_COMPANY = @CD_COMPANY AND NO_IO = @NO_IO AND NO_IOLINE = @NO_IOLINE)            
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = '이미 매출등록 되었습니다.' GOTO ERROR            
                END            
            
                /* 출고데이블로 수량 UPDATE*/            
                IF (ISNULL(@NO_ISURCV, '') <> '' AND ISNULL(@NO_ISURCVLINE, 0) > 0)            
                BEGIN            
                    EXEC SP_SA_GIRL_FROM_GI_UPDATE1            
                        @CD_COMPANY, @NO_ISURCV ,@NO_ISURCVLINE, @DT_IO,            
                        @NO_IO, @NO_IOLINE,            
                        0, @QT_UNIT_MM,     --출고수량(수주)(OLD,NEW)            
                        0, @QT_IO,          --출고수량(재고)(OLD,NEW)            
                        @AM_EX, @AM, 0, 0   --외화금액,원화금액            
            
                    IF (@@ERROR <> 0 ) BEGIN SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR END            
                END            
				
				
				
                /*수주테이블로 데이터 UPDATE*/            
                IF (ISNULL(@NO_PSO_MGMT, '') <> '' AND ISNULL(@NO_PSOLINE_MGMT, 0) > 0)            
                BEGIN            
                    --IF @YN_RETURN <> 'Y' /*반품여부*/            
                    IF (@YN_RETURN <> 'Y' OR ( @YN_RETURN = 'Y' AND ISNULL(@NO_IO_MGMT, '') = ''))            
                    /*출고처리 OR 반품출고(수주적용) 2010.06.11*/            
                    BEGIN            
                        EXEC SP_SA_SOL_FROM_GI_UPDATE1            
                            @CD_COMPANY, @NO_PSO_MGMT, @NO_PSOLINE_MGMT,            
                            @NO_ISURCV, @NO_ISURCVLINE, @NO_IO, @NO_IOLINE,            
                            0,      0,              --의뢰수량(수주)(OLD,NEW)            
                            0,      0,  --의뢰수량(재고)(OLD,NEW)            
                            0,      @QT_UNIT_MM,    --출고수량(수주)(OLD,NEW)            
                            0,      @QT_IO,         --출고수량(재고)(OLD,NEW)            
                            0,      0,              --반품수량(수주)(OLD,NEW)            
                            0,      0,              --반품수량(재고)(OLD,NEW)            
                            @NO_LC, @NO_LCLINE      --LC번호, LC항번            
                    END            
                    ELSE            
					BEGIN            
					EXEC SP_SA_SOL_FROM_GI_UPDATE1            
                            @CD_COMPANY, @NO_PSO_MGMT, @NO_PSOLINE_MGMT,            
                            @NO_ISURCV, @NO_ISURCVLINE, @NO_IO, @NO_IOLINE,            
                            0,      0,              --의뢰수량(수주)(OLD,NEW)            
                            0,      0,              --의뢰수량(재고)(OLD,NEW)            
                            0,      0,              --출고수량(수주)(OLD,NEW)            
                            0,      0,              --출고수량(재고)(OLD,NEW)            
                            0,      @QT_UNIT_MM,    --반품수량(수주)(OLD,NEW)            
                            0,      @QT_IO,         --반품수량(재고)(OLD,NEW)            
                            @NO_LC, @NO_LCLINE      --LC번호, LC항번            
                    END            
            
                    IF (@@ERROR <> 0 )            
                    BEGIN            
                        SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                    END            
                END            
            END            
            
            ELSE IF (@FG_IO = '001' OR @FG_IO = '030' OR @FG_IO = '031') -- 구매입고            
            BEGIN            
                IF EXISTS (SELECT 1 FROM PU_RCVL WHERE NO_IO_MGMT = @NO_IO AND NO_IOLINE_MGMT = @NO_IOLINE AND CD_COMPANY = @CD_COMPANY)            
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = 'PU_M000082' GOTO ERROR            
                END            
            
                IF EXISTS (SELECT 1 FROM SA_IVL WHERE CD_COMPANY = @CD_COMPANY AND NO_IO = @NO_IO AND NO_IOLINE = @NO_IOLINE)            
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = '이미 매입등록 되었습니다.' GOTO ERROR            
                END            
            END            
            
            IF (@FG_IO = '001' OR @FG_IO = '030' OR @FG_IO = '031' OR @FG_IO = '005' OR @FG_IO = '051') --005, 051 외주입고            
            BEGIN       
               
				IF (@FG_IO = '001' OR @FG_IO = '030' OR @FG_IO = '031')            
				BEGIN            
                    --발주 테이블 잔량관리            
                    IF (ISNULL(@NO_PSOLINE_MGMT, 0) > 0 AND ISNULL(@NO_PSO_MGMT, '') <> '')            
                    BEGIN            
                        EXEC UP_PU_POL_FROM_QTIO_UPDATE @CD_COMPANY, @NO_PSO_MGMT, @NO_PSOLINE_MGMT, 0, @QT_IO, 0, @QT_UNIT_MM, @FG_IO            
            
                        IF (@@ERROR <> 0)            
                        BEGIN            
         SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                        END            
                    END            
              
                    -- 의뢰 테이블 잔량관리            
                    IF (ISNULL(@NO_ISURCVLINE, 0) > 0 AND ISNULL(@NO_ISURCV, '') <> '')            
                    BEGIN            
                        EXEC UP_PU_RCVL_FROM_QTIO_UPDATE @CD_COMPANY, @NO_ISURCV, @NO_ISURCVLINE, @DT_IO, 0, @QT_IO, 0, @QT_UNIT_MM, 0, @VAT, 0, @AM_EX, 0, @AM            
            
                        IF (@@ERROR <> 0)            
                        BEGIN            
                            SELECT @ERRNO  = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
						END            
					END            
            
           
                    --UPDATE MM_QTIOH_POP SET NO_POP = NULL  WHERE CD_COMPANY = @CD_COMPANY AND NO_POP = @V_NO_POP                   
                    --IF (@@ERROR <> 0 ) BEGIN SELECT @ERRNO  =  100000, @ERRMSG = 'POP연동(MM_QTIO_POP) 데이터 동기화에 실패했습니다' GOTO ERROR END                  
            
                    IF @FG_IO = '001' -- 구매입고            
                    BEGIN 
						IF ISNULL(@V_NO_POP,'') <> '' AND EXISTS (SELECT 1 FROM MM_QTIO_POP 
																  WHERE NO_POP = @V_NO_POP AND NO_POP_LINE = @V_NO_POP_LINE AND CD_COMPANY = @CD_COMPANY )                
						BEGIN             
							-- 가입고 POP연동인 경우 입고의뢰데이터 자동생성됨 - 삭제시 입고의뢰데이터 자동삭제            
							/*UPDATE  PU_RCVL SET NO_POP = NULL ,NO_POP_LINE = 0            
							WHERE   CD_COMPANY = @CD_COMPANY AND NO_RCV = @NO_ISURCV AND NO_LINE = @NO_ISURCVLINE            
							AND     NO_POP = @V_NO_POP AND NO_POP_LINE = @V_NO_POP_LINE*/         
	      
							DELETE            
							FROM    PU_RCVL            
							WHERE   CD_COMPANY  = @CD_COMPANY            
							AND     NO_RCV      = @NO_ISURCV            
							AND     NO_LINE     = @NO_ISURCVLINE            
							AND     NO_POP      = @V_NO_POP            
							AND     NO_POP_LINE = @V_NO_POP_LINE            
							IF (@@ERROR <> 0)            
							BEGIN            
								SELECT @ERRNO = 100000, @ERRMSG = 'POP연동(PU_RCVL) 데이터 동기화에 실패했습니다' GOTO ERROR            
							END   
						END	
	          
						-- 검사품이면서 미확정인경우 품질 데이터 함께 삭제 2013.03.20 - D20130218002 - 김현정  
						SELECT @V_INSPECT_CHECK = CD_EXC   
						FROM MA_EXC A   
						WHERE CD_COMPANY = @CD_COMPANY  
						AND  CD_MODULE = 'PU'    
						AND  EXC_TITLE = '구매입고등록_검사'  

						IF ISNULL(@V_INSPECT_CHECK,'000') = '200' AND @V_YN_INSPECT = 'Y'  
						BEGIN   
							EXEC NEOE.UP_PU_QCL_DELETE @CD_COMPANY, @NO_IO, @NO_IOLINE   
							IF (@@ERROR <> 0)            
							BEGIN            
								SELECT @ERRNO = 100000, @ERRMSG = '품질데이터 삭제에 실패했습니다' GOTO ERROR            
							END   
						END   
					END 
					
				END            
	                            
					--외주입고, 외주입고반품 --> 여기에 외주입고, 외주입고반품일 경우 발주테이블 잔량관리 구문 넣어줘야함.            
					--상태관리에 대해서도 고려를 해야함.            
					IF (@FG_IO = '005' OR @FG_IO = '051')            
					BEGIN            
						-- 의뢰 테이블 잔량관리            
						IF (ISNULL(@NO_ISURCVLINE, 0) > 0 AND ISNULL(@NO_ISURCV, '') <> '')            
						BEGIN            
                        EXEC UP_SU_RCVL_FROM_QTIO_UPDATE            
                            @CD_COMPANY, @NO_ISURCV, @NO_ISURCVLINE, @DT_IO,            
                            0, @QT_IO, 0, @QT_UNIT_MM, 0, @VAT, 0, @AM_EX, 0, @AM            
            
                        IF (@@ERROR <> 0)            
                        BEGIN            
                            SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                        END            
                    END            
            
                    IF (ISNULL(@NO_PSOLINE_MGMT, 0) > 0 AND ISNULL(@NO_PSO_MGMT, '') <> '')            
                    BEGIN            
                       UPDATE  A            
                        SET     A.QT_RCV = A.QT_RCV + @QT_IO,
								A.QT_RCV_MM = A.QT_RCV_MM + @QT_UNIT_MM            
                        FROM    SU_POL A            
                                INNER JOIN SU_RCVL B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_PO = B.NO_PO AND A.NO_LINE = B.NO_POLINE            
                                                    AND B.NO_RCV = @NO_ISURCV AND B.NO_LINE = @NO_ISURCVLINE AND B.YN_RETURN = 'N'            
                        WHERE   A.CD_COMPANY = @CD_COMPANY            
                        AND     A.NO_PO      = @NO_PSO_MGMT            
                        AND     A.NO_LINE    = @NO_PSOLINE_MGMT            
            
                        --UPDATE SU_POL            
                        --SET QT_RCV = QT_RCV + @QT_IO            
                        --WHERE CD_COMPANY = @CD_COMPANY            
                        --        AND NO_PO = @NO_PSO_MGMT            
           --        AND NO_LINE = @NO_PSOLINE_MGMT            
            
                        DECLARE @SU_QT_PO   NUMERIC(17, 4),            
                                @SU_QT_RCV  NUMERIC(17, 4)            
            
                        SELECT  @SU_QT_PO = QT_PO, @SU_QT_RCV = QT_RCV            
                        FROM    SU_POL            
                        WHERE   CD_COMPANY  = @CD_COMPANY            
                        AND     NO_PO       = @NO_PSO_MGMT            
                        AND     NO_LINE     = @NO_PSOLINE_MGMT            
            
                        IF (@SU_QT_RCV = 0)            
                        BEGIN            
							UPDATE  SU_POL            
							SET     ST_PO       = 'R'            
							WHERE   CD_COMPANY  = @CD_COMPANY            
                            AND    NO_PO       = @NO_PSO_MGMT            
                            AND     NO_LINE     = @NO_PSOLINE_MGMT            
                        END            
                        ELSE IF (@SU_QT_RCV > 0 AND @SU_QT_RCV < @SU_QT_PO)            
                        BEGIN            
                            UPDATE  SU_POL            
                            SET     ST_PO   = 'S'            
                            WHERE   CD_COMPANY  = @CD_COMPANY            
                            AND     NO_PO       = @NO_PSO_MGMT            
                            AND     NO_LINE     = @NO_PSOLINE_MGMT            
                        END            
                        ELSE IF (@SU_QT_RCV = @SU_QT_PO AND @SU_QT_PO > 0)            
                        BEGIN            
                            UPDATE  SU_POL            
                            SET     ST_PO       = 'C'            
                            WHERE   CD_COMPANY  = @CD_COMPANY            
                            AND     NO_PO       = @NO_PSO_MGMT            
                            AND     NO_LINE     = @NO_PSOLINE_MGMT            
                        END            
                    END          

			  IF(@FG_IO = '005')        
			  BEGIN   

			  SELECT @V_INSPECT_CHECK = CD_EXC   
			  FROM MA_EXC A   
			  WHERE CD_COMPANY = @CD_COMPANY  
			  AND  CD_MODULE = 'PU'    
			  AND  EXC_TITLE = '외주입고등록_검사'  
		  
			  IF ISNULL(@V_INSPECT_CHECK,'000') = '200' --AND @V_YN_INSPECT = 'Y'  
			  BEGIN   
			   EXEC NEOE.UP_PU_QCL_DELETE @CD_COMPANY, @NO_IO, @NO_IOLINE   
		          
			   IF (@@ERROR <> 0)            
			   BEGIN            
				SELECT @ERRNO = 100000, @ERRMSG = '품질데이터 삭제에 실패했습니다' GOTO ERROR            
			   END   
			  END   
			       
     -- SELECT @P_YN_QC_FIX = YN_QC_FIX             
     --FROM MM_QCL             
     --  WHERE CD_COMPANY = @CD_COMPANY            
     -- AND CD_PLANT = @CD_PLANT             
     -- AND NO_REQ = @NO_IO            
     -- AND NO_LINE = @NO_IOLINE        

--  IF ( @P_YN_QC_FIX = 'Y')                   
     --BEGIN                      
     -- SELECT @ERRNO  = 100000, @ERRMSG = '검사 확정 상태라 삭제하실수 없습니다.'      
     -- GOTO ERROR                  
     --END                        
     --ELSE             
     --BEGIN                      
     -- DELETE MM_QCL WHERE CD_COMPANY = @CD_COMPANY AND CD_PLANT = @CD_PLANT AND NO_REQ = @NO_IO AND NO_LINE = @NO_IOLINE            
     --END          
       END           
            
                    IF (@FG_IO = '051' AND @YN_RETURN = 'Y')        -- 수불구분 : 외주입고반품            
                    BEGIN            
                        -->  반품된건의 발주번호(SU_POL)를 찾아 반품수량(QT_RETURN)을 UPDATE 시켜주는 구문            
                        SELECT  @NO_PO_RETURN = NO_PO_MGMT, @NO_POLINE_RETURN = NO_POLINE_MGMT      
                        FROM    SU_RCVL            
                        WHERE   CD_COMPANY  = @CD_COMPANY            
                        AND     NO_RCV      = @NO_ISURCV            
                        AND     NO_LINE     = @NO_ISURCVLINE            
            
                        UPDATE  SU_POL            
                        SET     QT_RETURN   = QT_RETURN + @QT_IO,  
                                QT_RETURN_MM = QT_RETURN_MM + @QT_UNIT_MM  
                        WHERE   CD_COMPANY  = @CD_COMPANY            
                        AND     NO_PO       = @NO_PO_RETURN            
                        AND     NO_LINE     = @NO_POLINE_RETURN            
                    END            
                END            
            
                IF (@FG_IO = '001')            
                BEGIN            
                    IF (ISNULL(@CD_BIN_REF, 'N') = 'Y')            
                    BEGIN            
                        UPDATE  QU_QC_REQ_TEMP            
                        SET     NO_IO = '', NO_IOLINE = 0, QT_GOOD_IO = 0 , QT_BAD_IO = 0 , QT_GOOD_IO_O = 0 , QT_BAD_IO_O = 0            
                        WHERE   CD_COMPANY  = @CD_COMPANY            
                        AND   NO_REQ      = @NO_ISURCV            
                        AND     NO_LINE_REQ = @NO_ISURCVLINE            
                        AND     FG_INSP     = 'IQC'            
            
                        IF (@@ERROR <> 0)            
                        BEGIN            
							SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                        END            
                    END            
                END            
			END            
            
            ELSE IF (@FG_IO ='002' OR @FG_IO ='061' ) -- 생산입고            
            BEGIN            
                IF (ISNULL(@NO_ISURCVLINE, 0) > 0 AND  ISNULL(@NO_ISURCV, '') <> '')            
                BEGIN            
                    EXEC UP_PU_WGR_TO_PR_RCVL_UPDATE            
                        @CD_COMPANY, @NO_ISURCV, @NO_ISURCVLINE, 0, @QT_GOOD_INV, 0, @QT_REJECT_INV,            
						@NO_PSO_MGMT, 0, @QT_INSP_INV, @CD_QTIOTP            
            
                    IF (@@ERROR <> 0)            
                    BEGIN            
                        SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                    END            
            
                    --투입자재전표삭제            
                    EXEC UP_PU_WGI_TO_PR_QTIO_DELETE @CD_COMPANY, @CD_PLANT, @NO_IO, @NO_IOLINE, @NO_PSO_MGMT, '203'            
            
                    IF (@@ERROR <> 0 )            
                    BEGIN            
                        SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                    END            
                END   
                  
                 -- 검사품이면서 미확정인경우 품질 데이터 함께 삭제 2013.03.20 - D20130308026 - 김현정   
                IF @FG_IO = '002'   
                BEGIN   
     SELECT @V_INSPECT_CHECK = CD_EXC   
     FROM MA_EXC A   
     WHERE CD_COMPANY = @CD_COMPANY  
     AND  CD_MODULE = 'PU'    
     AND  EXC_TITLE = '생산입고등록_검사'
     
   
       
     IF ISNULL(@V_INSPECT_CHECK,'000') = '200' AND @V_YN_INSPECT = 'Y'  
     BEGIN  
      EXEC NEOE.UP_PU_QCL_DELETE @CD_COMPANY, @NO_IO, @NO_IOLINE   
      IF (@@ERROR <> 0)            
      BEGIN            
       SELECT @ERRNO = 100000, @ERRMSG = '품질데이터 삭제에 실패했습니다' GOTO ERROR            
      END   
     END  
                END  
                           
            END            
            
            ELSE IF (@FG_IO = '011' OR @FG_IO = '062') -- 생산출고012            
            BEGIN            
                IF (@CD_BIN_REF IN ('11', '33')) -- 생산의뢰적용에서 받아오는경우            
                BEGIN            
                    IF( ISNULL(@NO_ISURCVLINE,0) > 0 AND  ISNULL(@NO_ISURCV,'') <> '')     
                    BEGIN            
                        EXEC UP_PU_WGI_TO_PR_REQ  0, @QT_IO, @NO_ISURCV, @NO_ISURCVLINE, @NO_PSO_MGMT, @NO_PSOLINE_MGMT, @CD_COMPANY, @CD_QTIOTP            
            
                        IF (@@ERROR <> 0)            
                        BEGIN            
                            SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                        END            
            
						IF @CD_BIN_REF = '33' 
							BEGIN
								SET @V_NO_PR = @V_NO_WORK
								SET @V_NO_PR_LINE = 0
								SET @V_NO_IO_MGMT_PR = @NO_IO_MGMT
								SET @V_NO_IOLINE_MGMT_PR = @NO_IOLINE_MGMT
							END							
						ELSE
							BEGIN 
								SET @V_NO_PR = @NO_IO
								SET @V_NO_PR_LINE = @NO_IOLINE
								SET @V_NO_IO_MGMT_PR = ''
								SET @V_NO_IOLINE_MGMT_PR = 0
							END
							
                        --투입자재전표삭제            
                        EXEC UP_PU_WGI_TO_PR_QTIO_DELETE @CD_COMPANY, @CD_PLANT, @V_NO_PR, @V_NO_PR_LINE, @NO_PSO_MGMT, '101', @V_NO_IO_MGMT_PR, @V_NO_IOLINE_MGMT_PR              
            
                        IF (@@ERROR <> 0)            
                        BEGIN            
                            SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                        END            
            
                        IF (ISNULL(@V_YN_INSPECT,'N') = 'Y' AND @FG_IO = '062')  --불량검사여부                  
                        BEGIN            
                            EXEC UP_MM_QTIO_MM_QC_BAD            
                                @CD_COMPANY,  @NO_IO, @NO_IOLINE, @DT_IO, @CD_ITEM, @QT_GOOD_INV, 'D',                  
                                @CD_PLANT, @CD_PARTNER, '', @FG_IO, @CD_QTIOTP, @NO_ISURCV, @NO_ISURCVLINE                  
                        
                            IF (@@ERROR <> 0)            
                            BEGIN            
                                SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                            END            
						END            
					END                  
              END                  
            
				ELSE IF (@CD_BIN_REF = '22') -- 요청적용에서 받아오는 경우                  
                BEGIN                  
                    IF (ISNULL(@NO_ISURCVLINE, 0) > 0 AND ISNULL(@NO_ISURCV, '') <> '')                  
                    BEGIN                  
                        EXEC UP_PU_MM_GIREQL_FR_QTIO_UPDATE  @NO_ISURCV ,@NO_ISURCVLINE,@CD_COMPANY , 0, @QT_IO                  
            
                        IF (@@ERROR <> 0)            
                        BEGIN            
                            SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR       
                        END            
                    END            
                END            
            
                -- 다음의 구문 주석처리.... 생산모듈에서 해당 수불을 삭제하려고 하나... DELETE 트리거에서 막아버려서 삭제가 않됨...                  
                -- 해당 수불을 삭제방지하기 위한 구문이 필요할시는 해당 프로시져에서 처리 요망...                  
                --        ELSE IF ( @CD_BIN_REF ='33')  -- 생산 백 플러쉬에서 들어온건 삭제 불가...                         
                --        BEGIN                  
                --                SELECT @ERRNO = 100000, @ERRMSG = ' 백 플러쉬로  들어온 생산 출고건 삭제 불가' GOTO ERROR                  
                --        END                  
            END                        
            
            ELSE IF (@FG_IO = '012' OR @FG_IO = '023' OR @FG_IO ='007') -- 계정출고 혹은 공장간이동                      
            BEGIN                  
                IF (ISNULL(@NO_ISURCVLINE, 0) > 0 AND ISNULL(@NO_ISURCV, '') <> '')                        
                BEGIN                        
                    EXEC UP_PU_MM_GIREQL_FR_QTIO_UPDATE @NO_ISURCV, @NO_ISURCVLINE, @CD_COMPANY, 0, @QT_IO                  
            
                    IF (@@ERROR <> 0)            
                    BEGIN            
                        SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                    END            
                END  
                
                IF (@FG_IO = '007')                  
				BEGIN            
				   IF EXISTS   (   SELECT  1 FROM    MM_BALANCEL            
								   WHERE   CD_COMPANY  = @CD_COMPANY            
								   AND     NO_GIREQ    = @NO_IO        )            
					BEGIN            
						 UPDATE  MM_BALANCEL            
						 SET     NO_GIREQ    = ''            
						 FROM    MM_BALANCEL            
						 WHERE   CD_COMPANY  = @CD_COMPANY            
						 AND     NO_GIREQ    = @NO_IO            
					END                            
				END          
            END            
                        
            ELSE IF (@FG_IO = '013')  -- 공장간 이동 출고                    
            BEGIN            
                /*  CD_QTIOTP ='999' : 이동재고입고용 없앰     
            
                --        IF NOT EXISTS(SELECT 1 FROM MM_QTIO WHERE NO_IO =@NO_IO AND CD_COMPANY = @CD_COMPANY AND CD_QTIOTP ='999')                           
                --        BEGIN                          
                --  SELECT @ERRNO  = 100000,                          
                --               @ERRMSG = 'PU_M000030'                          
                --        GOTO ERROR                          
                --        END                    
                */                  
                IF (ISNULL(@NO_ISURCVLINE, 0) > 0 AND ISNULL(@NO_ISURCV, '') <> '')            
                BEGIN            
                    EXEC UP_PU_MM_GIREQL_FR_QTIO_UPDATE @NO_ISURCV, @NO_ISURCVLINE, @CD_COMPANY, 0, @QT_IO            
            
                    IF (@@ERROR <> 0)            
                    BEGIN            
                        SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                    END            
                END  
                
                IF( EXISTS(SELECT 1 FROM MM_QTIO WHERE CD_COMPANY = @CD_COMPANY AND FG_IO = '004' AND NO_IO_MGMT = @NO_IO))  
                BEGIN
					SELECT @ERRNO = 100000, @ERRMSG = '해당출고로 입고된 데이터가 존재하여 삭제 할 수 없습니다.' GOTO ERROR    
                END        
            END            
              
            ELSE IF (@FG_IO = '015' OR @FG_IO = '052' ) -- 외주출고                  
            BEGIN            
                         --SELECT @ERRNO = 100000, @ERRMSG = 'TEST' GOTO ERROR     
                IF (@CD_BIN_REF = '22') -- 의뢰적용에서 받아오는경우                  
                BEGIN            
                    --MS 추가                  
                    IF (ISNULL(@NO_ISURCVLINE, 0) > 0 AND ISNULL(@NO_ISURCV, '') <> '')            
					BEGIN            
                        EXEC UP_PU_SGI_TO_SU_UPDATE @CD_COMPANY, @NO_ISURCV, @NO_ISURCVLINE, @NO_PSO_MGMT, @NO_PSOLINE_MGMT, 0, @QT_IO                  
            
                        IF (@@ERROR <> 0)            
                        BEGIN     
                            SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                        END                             
                    END            
            
                    IF (ISNULL(@NO_ISURCVLINE, 0) > 0 AND ISNULL(@NO_ISURCV, '') <> '')                  
                    BEGIN                        
                        EXEC UP_PU_SGI_TO_SA_UPDATE @CD_COMPANY, @NO_ISURCV, @NO_ISURCVLINE, @NO_PSO_MGMT, @NO_PSOLINE_MGMT, 0, @QT_IO            
 
                        IF (@@ERROR <> 0)            
                        BEGIN            
                            SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                        END            
                    END            
                END      
                
              ELSE IF (@CD_BIN_REF = '11') -- 요청적용에서 받아오는 경우                        
				BEGIN                        
                    IF (ISNULL(@NO_ISURCVLINE, 0) > 0 AND  ISNULL(@NO_ISURCV, '') <> '')            
                    BEGIN            
                        EXEC UP_PU_MM_GIREQL_FR_QTIO_UPDATE @NO_ISURCV, @NO_ISURCVLINE, @CD_COMPANY, 0, @QT_IO                  
            
                        IF (@@ERROR <> 0)            
                        BEGIN            
                            SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                        END            
                    END            
                END   
              ELSE IF(@CD_BIN_REF ='99') -- 구매발주에서 외주발주유형으로 할경우 B/F  
              BEGIN      
                    IF(ISNULL(@NO_PSOLINE_MGMT, 0) > 0 AND ISNULL(@NO_PSO_MGMT, '') <> '')      
                    BEGIN      
                    --BEGIN SELECT @ERRNO = 100000, @ERRMSG = 'TTT' GOTO ERROR END  
                        EXEC UP_PU_SGI_TO_PU_POLL_UPDATE @CD_COMPANY, @NO_PSO_MGMT, @NO_PSOLINE_MGMT, 0, @QT_IO, @CD_ITEM, @V_NO_TRACK_LINE            
                        IF (@@ERROR <> 0) BEGIN SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR END      
                    END      
                END             
            END                     
            
            ELSE IF (@FG_IO = '022') --창고간 이동                  
            BEGIN   
                   
			 /* 창고이동라인삭제시 출고와 함께 입고데이터도 삭제        */                  
			 DELETE            
			 FROM    MM_QTIO            
			 WHERE   CD_COMPANY      = @CD_COMPANY            
			 AND     NO_IO           = @NO_IO            
			 AND     NO_IO_MGMT      = @NO_IO            
			 AND     NO_IOLINE_MGMT  = @NO_IOLINE                  
		            
			 /* 요청적용 버튼 이벤트에 의한 추가 작업*/                  
			 IF (ISNULL(@NO_ISURCVLINE, 0) > 0 AND  ISNULL(@NO_ISURCV, '') <> '' AND @FG_PS = '2')                        
			 BEGIN            
			 EXEC UP_PU_MM_GIREQL_FR_QTIO_UPDATE @NO_ISURCV, @NO_ISURCVLINE, @CD_COMPANY, 0, @QT_IO            
		            
			  IF (@@ERROR <> 0)            
			  BEGIN            
			   SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
			  END            
			 END            
		            
			 IF (ISNULL(@NO_PSOLINE_MGMT,0) > 0 AND ISNULL(@NO_PSO_MGMT,'') <> '')            
			 BEGIN            
			  EXEC UP_PU_SGI_TO_SU_UPDATE @CD_COMPANY, @NO_ISURCV, @NO_ISURCVLINE, @NO_PSO_MGMT, @NO_PSOLINE_MGMT, 0, @QT_IO            
		            
			  IF (@@ERROR <> 0)            
			  BEGIN            
			   SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
			  END            
			 END            
		                             
			 --IF(ISNULL(@NO_ISURCV,'') <> '' AND ISNULL(@NO_ISURCVLINE,0) <> 0 )                  
			 --BEGIN                  
			 --        EXEC UP_PU_STS_TO_QU_SA_FG_MOVE_REQL_UPDATE  @CD_COMPANY, @NO_ISURCV, @NO_ISURCVLINE, 0, @QT_GOOD_INV, @CD_BIN_REF                  
		                           
			 --        IF (@@ERROR <> 0 )                  
			 --        BEGIN                    
			 --              SELECT @ERRNO  = 100000,                    
			 --    @ERRMSG = 'CM_M100010'                    
			 --              GOTO ERROR                    
			 --        END                  
			 -- END                
			 --SELECT @ERRNO = 100000, @ERRMSG = @FG_PS GOTO ERROR            
						  ---***검사처리***---            
			 IF(ISNULL(@P_FG_SLQC,'N') = 'Y') ---품목검사처리여부            
			 BEGIN  
		                 
			   SELECT @V_SL_YN_QC = YN_QC  FROM MA_SL WHERE CD_COMPANY = @CD_COMPANY AND CD_SL= @CD_SL AND CD_PLANT = @CD_PLANT            
		                   
			   IF(@V_SL_YN_QC = 'Y')            
			   BEGIN            
				EXEC UP_PU_QCL_STS_DELETE @CD_COMPANY, @NO_IO, @NO_IOLINE            
		                    
				IF (@@ERROR <> 0)            
				BEGIN            
				SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
				END            
			   END            
		                   
		                   
			 END            
     ---**************---            
                                   
            END            
            
          /* 계정대체입고시 재고조정관련데이터 업데이트 -유지영                  
            ELSE IF (@FG_IO = '007')                  
			BEGIN            
			   IF EXISTS   (   SELECT  1            
				   FROM    MM_BALANCEL            
				   WHERE   CD_COMPANY  = @CD_COMPANY            
				   AND     NO_GIREQ    = @NO_IO        )            
				BEGIN            
					 UPDATE  MM_BALANCEL            
					 SET     NO_GIREQ    = ''            
					 FROM    MM_BALANCEL            
					 WHERE   CD_COMPANY  = @CD_COMPANY            
					 AND     NO_GIREQ    = @NO_IO            
				END                            
            END */           
            
            ----AS출고, AS무상출고
            ELSE IF( @FG_IO = '070' OR @FG_IO = '075' OR @FG_IO = '078' OR @FG_IO = '079' )  --AS일경우동작 20090514                  
            BEGIN            
                IF (@QT_CLS <> 0)
                BEGIN
                    SELECT @ERRNO  = 100000, @ERRMSG = '이미 매출등록 되었습니다.' GOTO ERROR                  
                END
                
                IF (ISNULL(@NO_ISURCV, '') <> '' AND ISNULL(@NO_ISURCVLINE, 0) > 0)
                BEGIN            
                    UPDATE  AS_SV_ASMANGL
                    SET     QT_IO       = QT_IO + @QT_IO
                    WHERE   CD_COMPANY  = @CD_COMPANY
                    AND     NO_AS       = @NO_ISURCV
                    AND     NO_LINE     = @NO_ISURCVLINE    
            
                    IF (@@ERROR <> 0) BEGIN SELECT @ERRMSG = 'CM_M100010' GOTO ERROR END
                END
            END                  
            -->> 30. 잔량관리            
            
            -->> 40.집계            
            -- MM_OHSLINVM( 월별 현재고 집계 (창고)                    
            SET @YY =  SUBSTRING(@DT_IO,1,4)                    
            SET @YM =  SUBSTRING(@DT_IO,1,6)                    
            
            EXEC UP_PU_MM_OHSLINVM_UPDATE            
                @CD_SL, @CD_PLANT, @CD_COMPANY, @CD_ITEM, @YY, @YM, @QT_IO,  --@QT_GOOD_INV                  
                @QT_REJECT_INV, @QT_TRANS_INV, @QT_INSP_INV, @FG_PS, @YN_RETURN, @CD_QTIOTP,            
                @P_CD_PJT, @P_SEQ_PROJECT                         
            
            IF (@@ERROR <> 0)            
            BEGIN            
                SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
            END                  
             
            EXEC UP_PU_MM_OHSLINVD_UPDATE            
                @CD_SL, @CD_PLANT, @CD_COMPANY, @CD_ITEM, @YY, @DT_IO, @QT_IO,  --@QT_GOOD_INV                  
                @QT_REJECT_INV, @QT_TRANS_INV, @QT_INSP_INV, @FG_PS, @YN_RETURN, @CD_QTIOTP, @FG_IO,            
                @P_CD_PJT, @P_SEQ_PROJECT                    
            
            IF (@@ERROR <> 0)            
            BEGIN            
                SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
            END            
              
            --PU_AP 프로시저                     
            IF ((@FG_IO = '001' OR @FG_IO ='030') AND @YN_AM = 'Y')            
            BEGIN            
                EXEC UP_PU_AP_UPDATE       
					@CD_PARTNER, @CD_GROUP, @CD_BIZAREA, @CD_COMPANY, @DT_IO, @AM,@VAT,                    
                    @YN_RETURN, 'Y', @CD_ITEM, @FG_TRANS, @CD_PLANT, @CD_QTIOTP, @QT_IO                    
            
                IF (@@ERROR <> 0)            
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                END                  
            END            
                        
            --SA_AR 프로시져 (판매/유환일경우만 동작 )                  
            IF ((@FG_IO = '010' OR @FG_IO ='041' OR ((@V_SERVER_KEY = 'NEOPH' OR @V_SERVER_KEY = 'DZSQL') AND (@FG_IO = '080' OR @FG_IO = '081'))) AND @YN_AM = 'Y')            
            BEGIN            
                EXEC SP_SA_AR_FROM_GI_UPDATE1                   
                    @CD_COMPANY,                  
                    @CD_PARTNER,                  
                    @DT_IO,         --발생일자            
                    0,  @AM,        --물대OLD, 물대NEW (-)            
                    0,  @VAT,       --부가세OLD, 부가세NEW (-)            
                    @YN_RETURN,     --반품유무(Y,N)            
                    @FG_TRANS,      --거래구분                  
                    @CD_GROUP,      --영업그룹            
                    @CD_BIZAREA,  --사업장코드            
                    @CD_QTIOTP      --수불형태코드                  
            
                IF (@@ERROR <> 0)            
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                END            
            END            
            
            --SA_PJTCREDIT 프로시져 (판매/유환일경우만 동작 ) 20081111                  
            IF ((@FG_IO = '010' OR @FG_IO ='041') AND @YN_AM = 'Y' AND @P_CD_PJT IS NOT NULL)                  
            BEGIN            
                EXEC SP_SA_PJTCREDIT_FROM_GI_UPDATE1                 
                @CD_COMPANY,                  
                    @P_CD_PJT,                  
                    @YN_RETURN,     --반품유무(Y,N)                  
                    0, @AM,         --물대OLD, 물대NEW (-)                  
                    0, @VAT         --부가세OLD, 부가세NEW (-)                  
            
                IF (@@ERROR <> 0)            
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
				END            
            END                  
            --<< 40.집계                  
            
            -->> 50.LOT            
            IF (@FG_PS = '1' OR @FG_PS = '2')            
            BEGIN            
            
         
                IF EXISTS   (   SELECT  1            
                                FROM    MM_QTIOLOT            
                                WHERE   CD_COMPANY  = @CD_COMPANY            
                                AND     NO_IO       = @NO_IO            
                                AND     NO_IOLINE   = @NO_IOLINE    )            
                BEGIN            
				IF (@YN_RETURN = 'Y' AND @FG_PS = '1') SET @V_FG_PS = '2'        
				IF (@YN_RETURN = 'Y' AND @FG_PS = '2') SET @V_FG_PS = '1'        
				IF (@YN_RETURN = 'N' ) SET @V_FG_PS = @FG_PS        
            
                    EXEC UP_MM_QTIOLOT_DELETE_FROM_MM_QTIO @CD_COMPANY, @NO_IO, @NO_IOLINE, @V_FG_PS      --UP_MM_QTIOLOT_DELETE_MM_QTIO            
            
                    IF (@@ERROR <> 0)            
                    BEGIN            
                        SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                    END                  
                END            
            END            
            --<< 50.LOT                  
                    
            	
            -->> 60.SERIAL                  
            BEGIN                  
                IF EXISTS   (   SELECT  1            
								FROM    MM_QTIODS            
								WHERE   CD_COMPANY  = @CD_COMPANY            
                                AND     NO_IO       = @NO_IO            
								AND     NO_IOLINE   = @NO_IOLINE    )            
				BEGIN         
					
				
					EXEC UP_MM_SERIAL_INV_UPDATE @CD_COMPANY, @NO_IO, @NO_IOLINE, @FG_PS, @CD_ITEM, @CD_PLANT, @YN_RETURN , @V_NO_POP, @V_NO_POP_LINE  
					IF (@@ERROR <> 0)            
                    BEGIN           
                        SELECT @ERRNO = 100000, @ERRMSG = 'UP_MM_SERIAL_INV_UPDATE' GOTO ERROR            
                    END   
                    
					DELETE            
					FROM  MM_QTIODS            
					WHERE   CD_COMPANY  = @CD_COMPANY            
                    AND     NO_IO       = @NO_IO            
                    AND     NO_IOLINE   = @NO_IOLINE            
					
         
                END            
            END            
            --<< 60.SERIAL            
            
            IF (@QT_RETURN > 0)                  
            BEGIN            
                SELECT @ERRNO  = 100000, @ERRMSG = '이미 반품의뢰되어 수정, 삭제가 불가능합니다.' GOTO ERROR                  
            END            
            
            -----------------LOCATION 등록된 수불번호 삭제시 자동으로 LOCATION정보삭제                  
            BEGIN                  
                IF EXISTS   (   SELECT  1            
                                FROM    MM_QTIO_LOCATION            
                                WHERE   CD_COMPANY  = @CD_COMPANY            
                 AND     NO_IO       = @NO_IO            
                                AND     NO_IOLINE   = @NO_IOLINE    )            
                BEGIN            
                    DELETE            
                    FROM    MM_QTIO_LOCATION            
                    WHERE   CD_COMPANY  = @CD_COMPANY            
                    AND     NO_IO       = @NO_IO            
                    AND     NO_IOLINE   = @NO_IOLINE            
            
                    IF (@@ERROR <> 0)            
                    BEGIN           
                        SELECT @ERRNO = 100000, @ERRMSG = 'CM_M100010' GOTO ERROR            
                    END            
                END            
            END                  
            
            IF (@QT_RETURN > 0)            
            BEGIN            
                SELECT @ERRNO = 100000, @ERRMSG = 'LOCATION정보를 삭제할수없습니다.' GOTO ERROR                  
            END            
            -----------------LOCATION 등록된 수불번호 삭제시 자동으로 LOCATION정보삭제                     
            
            -->> 2010.12.23  POP연동 데이터 삭제시 MM_QTIO_POP 에 수불번호 NULL로 업데이트 구문 추가!                 
            IF (ISNULL(@V_NO_POP, '') <> '' AND ISNULL(@V_NO_POP_LINE, 0) > 0)            
            BEGIN            
                UPDATE  MM_QTIO_POP            
                SET     NO_IO = '', NO_IOLINE = 0            
                WHERE   CD_COMPANY  = @CD_COMPANY            
				AND     NO_POP      = @V_NO_POP            
                AND  NO_POP_LINE = @V_NO_POP_LINE                  
                            
                IF (@@ERROR <> 0)            
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = 'POP연동(MM_QTIO_POP) 데이터 동기화에 실패했습니다' GOTO ERROR            
                END            
            
                UPDATE  SA_LINK            
                SET     YN_PROC     = 'N',                
                        NO_IO_GI    = CASE WHEN @FG_IO IN ('010', '041') THEN '' ELSE NO_IO_GI END, NO_IOLINE_GI = CASE WHEN @FG_IO IN ('010', '041') THEN 0 ELSE NO_IOLINE_GI END,            
                        NO_IO_PRO   = CASE WHEN @FG_IO = '011' THEN '' ELSE NO_IO_PRO END,                
                        NO_IO_PRI   = CASE WHEN @FG_IO = '002' THEN '' ELSE NO_IO_PRI END, NO_IOLINE_PRI = CASE WHEN @FG_IO = '002' THEN 0 ELSE NO_IOLINE_PRI END,
                        NO_GIREQ    = CASE WHEN @FG_IO = '022' THEN '' ELSE NO_GIREQ END, NO_LINE_GIREQ = CASE WHEN @FG_IO = '022' THEN 0 ELSE NO_LINE_GIREQ END            
                WHERE   CD_COMPANY  = @CD_COMPANY            
                AND     NO_LINK     = @V_NO_POP            
                AND     NO_LINE_LINK = @V_NO_POP_LINE                  
                IF (@@ERROR <> 0)            
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = '영업연동(SA_LINK) 데이터 동기화에 실패했습니다' GOTO ERROR            
                END    
                
                UPDATE  PU_LINK            
                SET     NO_IO = '', NO_IOLINE = 0, YN_PROC = 'N'                
                WHERE   CD_COMPANY  = @CD_COMPANY            
				AND     NO_LINK      = @V_NO_POP            
                AND		NO_LINE_LINK = @V_NO_POP_LINE                  
                IF (@@ERROR <> 0)            
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = '구매연동(PU_LINK) 데이터 동기화에 실패했습니다' GOTO ERROR            
                END           
                
                UPDATE  EC_LINKL            
                SET     NO_IO = '', NO_IOLINE = 0, YN_PROC = 'N'                
                WHERE   CD_COMPANY  = @CD_COMPANY            
				AND     NO_LINK      = @V_NO_POP            
                AND		NO_LINE = @V_NO_POP_LINE                  
                IF (@@ERROR <> 0)            
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = 'EC연동(EC_LINK) 데이터 동기화에 실패했습니다' GOTO ERROR            
                END                  
            END                  
            
            ---> 2011-07-04 LOCAL L/C 후L/C 일경우 L/C로 등록되었을경우 삭제 못하도록 수정 요청자 : 이형준대리                  
            IF (@FG_TRANS = '003' AND ISNULL(@NO_LC, '') <> '' AND ISNULL(@NO_LCLINE, 0) > 0)                  
            BEGIN            
                SELECT  @V_FG_PROCESS   = FG_PROCESS                   
                FROM    PU_RCVH A                  
                WHERE   A.CD_COMPANY    = @CD_COMPANY                   
				AND     A.NO_RCV        = @NO_ISURCV                   
            
			IF(@V_FG_PROCESS = '002')            
            BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = '후L/C로 등록된 입고번호입니다. 등록된 L/C번호가 존재합니다. 다시확인하여주시기 바랍니다.' GOTO ERROR            
                END            
            END            
          
			IF ISNULL(@V_YN_PMS,'N') = 'Y' 
			BEGIN
				SET @V_DT_PMS = LEFT(@DT_IO,6)
				SET @V_PMS_QT = @QT_GOOD_INV * -1
				SET @V_PMS_AM = @AM * -1
				EXEC UP_WC_C_COST_REAL_D @CD_COMPANY, @P_CD_PJT, @DT_IO, @V_CD_CSTR, @V_DL_CSTR, @V_PMS_QT, @V_PMS_AM , 'AI'  
			END
	
            --<< 라인전부 삭제시 헤더 자동삭세 -유지영(11.16)                  
            IF NOT EXISTS   (   SELECT  1            
                                FROM    MM_QTIO            
                              WHERE   CD_COMPANY  = @CD_COMPANY            
                                AND     NO_IO       = @NO_IO        )            
            BEGIN                  
                DELETE            
                FROM    MM_QTIOH            
                WHERE   CD_COMPANY  = @CD_COMPANY            
                AND     NO_IO       = @NO_IO            
            
                IF (@@ERROR <> 0)                   
                BEGIN            
                    SELECT @ERRNO = 100000, @ERRMSG = '[TD_MM_QTIO] MM_QTIOH 삭제 에러' GOTO ERROR            
                END            
            END                   
        END                  
             
        --SELECT * FROM MM_QTIO_LOG                  
        --<< MM_QTIO 삭제 LOG 생성(2010.03.19 SMR)                  
        SET @DTS_DELETE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')            
                    
        INSERT INTO MM_QTIO_LOG            
        (                    
                NO_IO, NO_IOLINE, CD_COMPANY, CD_PLANT, CD_BIZAREA, CD_SL, CD_SECTION, CD_BIN, DT_IO, NO_ISURCV,            
                NO_ISURCVLINE, NO_PSO_MGMT, NO_PSOLINE_MGMT, FG_PS, YN_PURSALE, FG_TPIO, FG_IO, CD_QTIOTP, FG_TRANS, FG_TAX,            
                CD_PARTNER, CD_ITEM, QT_IO, QT_RETURN, QT_TRANS_INV, QT_INSP_INV, QT_REJECT_INV, QT_GOOD_INV, CD_EXCH, RT_EXCH,            
                UM_EX, AM_EX, UM, AM, QT_CLS, AM_CLS, VAT, VAT_CLS, FG_TAXP, UM_STOCK,            
                UM_EVAL, YN_AM, CD_PJT, AM_DISTRIBU, RT_CUSTOMS, AM_CUSTOMS, NO_LC, NO_LCLINE, QT_IMSEAL, QT_EXLC,            
                GI_PARTNER, NO_EMP, CD_GROUP, NO_IO_MGMT, NO_IOLINE_MGMT, CD_BIZAREA_RCV, CD_PLANT_RCV, CD_SL_REF, CD_SECTION_REF,            
                CD_BIN_REF, BILL_PARTNER, CD_UNIT_MM, QT_UNIT_MM, UM_EX_PSO, QT_CLS_MM, QT_RETURN_MM, CD_WC, CD_OP, CD_WCOP,            
                AM_EVAL, FG_LC_OPEN, QT_INV, NO_WORK, CD_CC, DC_RMK, CD_WORKITEM, DTS_DELETE, SEQ_PROJECT, NO_TRACK, NO_TRACK_LINE, CD_LOCATION      
        )            
        SELECT  NO_IO, NO_IOLINE, CD_COMPANY, CD_PLANT, CD_BIZAREA, CD_SL, CD_SECTION, CD_BIN, DT_IO, NO_ISURCV,            
                NO_ISURCVLINE, NO_PSO_MGMT, NO_PSOLINE_MGMT, FG_PS, YN_PURSALE, FG_TPIO, FG_IO, CD_QTIOTP, FG_TRANS, FG_TAX,            
                CD_PARTNER, CD_ITEM, QT_IO, QT_RETURN, QT_TRANS_INV, QT_INSP_INV, QT_REJECT_INV, QT_GOOD_INV, CD_EXCH, RT_EXCH,            
                UM_EX, AM_EX, UM, AM, QT_CLS, AM_CLS, VAT, VAT_CLS, FG_TAXP, UM_STOCK,            
                UM_EVAL, YN_AM, CD_PJT, AM_DISTRIBU, RT_CUSTOMS, AM_CUSTOMS, NO_LC, NO_LCLINE, QT_IMSEAL, QT_EXLC,            
                GI_PARTNER, NO_EMP, CD_GROUP, NO_IO_MGMT, NO_IOLINE_MGMT, CD_BIZAREA_RCV, CD_PLANT_RCV, CD_SL_REF, CD_SECTION_REF,            
                CD_BIN_REF, BILL_PARTNER, CD_UNIT_MM, QT_UNIT_MM, UM_EX_PSO, QT_CLS_MM, QT_RETURN_MM, CD_WC, CD_OP, CD_WCOP,            
                AM_EVAL, FG_LC_OPEN, QT_INV, NO_WORK, CD_CC, DC_RMK, CD_WORKITEM, @DTS_DELETE, @P_SEQ_PROJECT, NO_TRACK, NO_TRACK_LINE, @V_CD_LOCATION    
        FROM    DELETED        
        WHERE   NO_IO       = @NO_IO            
        AND     CD_COMPANY  = @CD_COMPANY            
        AND     NO_IOLINE   = @NO_IOLINE            
            
        IF (@@ERROR <> 0)            
        BEGIN            
            SELECT @ERRNO = 100000, @ERRMSG = '[TD_MM_QTIO] MM_QTIO 삭제 에러 (MM_QTIO_LOG 생성 실패)' GOTO ERROR            
        END            
		
		IF @V_SERVER_KEY LIKE 'DZSQL%' OR @V_SERVER_KEY = 'SQL_108' OR @V_SERVER_KEY = 'TYPHC'		
		BEGIN
			DELETE FROM SA_Z_DYP_GI_VAT 
			WHERE 
			CD_COMPANY = @CD_COMPANY AND 
			CD_PLANT = @CD_PLANT AND 
			NO_IO = @NO_IO AND 
			NO_IOLINE = @NO_IOLINE 
		END
		            
        FETCH NEXT FROM CUR_TD_MM_QTIO            
        INTO    @YN_RETURN, @NO_IO, @NO_IOLINE, @CD_COMPANY, @CD_SL, @FG_PS, @YN_AM, @DT_IO, @CD_PLANT, @CD_ITEM,            
                @FG_IO, @CD_QTIOTP, @FG_TRANS,  @CD_PARTNER, @CD_BIZAREA, @QT_GOOD_INV, @QT_REJECT_INV, @QT_TRANS_INV, @QT_INSP_INV, @QT_IO,            
                @AM, @VAT, @CD_GROUP, @QT_CLS, @CD_BIN_REF, @NO_ISURCV, @NO_ISURCVLINE, @NO_PSO_MGMT, @NO_PSOLINE_MGMT,            
                @QT_UNIT_MM, @AM_EX, @NO_LC, @NO_LCLINE, @QT_RETURN, @NO_IO_MGMT, @NO_IOLINE_MGMT,            
                @P_CD_PJT,           
                @P_CLS_ITEM, @V_YN_INSPECT, @V_NO_POP, @V_NO_POP_LINE , @P_SEQ_PROJECT, @P_FG_SLQC, @V_NO_DLV, @V_NO_DLVLINE, @V_NO_WORK, @V_CD_CSTR, @V_DL_CSTR,
                @V_NO_TRACK, @V_NO_TRACK_LINE, @V_YN_SEND_LEGACY, @V_CD_LOCATION
    END            
            
    CLOSE CUR_TD_MM_QTIO            
    DEALLOCATE CUR_TD_MM_QTIO            
          
    RETURN            
            
    ERROR:            
        RAISERROR (@ERRMSG, 18, 1)            
        CLOSE CUR_TD_MM_QTIO            
        DEALLOCATE CUR_TD_MM_QTIO            
        ROLLBACK  TRANSACTION            
        RETURN            
    ERROR2:            
        RAISERROR (@ERRMSG, 18, 1)            
        ROLLBACK  TRANSACTION            
END