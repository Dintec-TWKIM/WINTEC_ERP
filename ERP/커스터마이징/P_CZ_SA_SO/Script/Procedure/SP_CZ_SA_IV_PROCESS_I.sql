USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_SA_IV_PROCESS_I]    Script Date: 2016-11-04 오후 4:09:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [NEOE].[SP_CZ_SA_IV_PROCESS_I]  
( 
  @P_SERVERKEY		NVARCHAR(50),			-- 업체서버키
  @P_UI				NVARCHAR(50),			-- 호출UI  (SA_SOL : 수주등록화면)
  @P_CD_COMPANY		NVARCHAR(7),			-- 회사코드
  @P_NO_SO			NVARCHAR(20),			-- 수주번호
  @P_SEQSO			NUMERIC(5,0),			-- 수주라인항번
  @P_ID_USER		NVARCHAR(15),
  @P_YN_ALL         NVARCHAR(1) = 'N'       -- Y : 수주번호 전체, N : 수주라인에 대해서  
)  
AS  

DECLARE  
       @V_ERROR       INT,  
       @V_ERRMSG      NVARCHAR(1000),  
       @V_NO_IV       NVARCHAR(20),    
       @V_YM          NVARCHAR(6),  
       @V_SYSDATE     NVARCHAR(14),  
       @V_TP_SO       NVARCHAR(4), 
       @V_IV_AUTO     NCHAR(1),    
       @V_SERVER_KEY  NVARCHAR(50),
       @V_DT_IO       NCHAR(8),
       @V_CNT		  INT,
       @P_CD_EXC      NVARCHAR(3),
       @V_STA_SO	  NVARCHAR(1)
  
BEGIN 
     SET    @V_SYSDATE    = NEOE.SF_SYSDATE(GETDATE())    
     SELECT @V_SERVER_KEY = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG 
 
     SET    @P_CD_EXC = ''
     SELECT @P_CD_EXC = ISNULL(CD_EXC, '') 
     FROM   MA_EXC 
     WHERE  CD_COMPANY = @P_CD_COMPANY
     AND    EXC_TITLE  = '수주라인-과세변경유무' 
 
 
     -- 수주유형 조회  
     SELECT @V_IV_AUTO = ISNULL(IV_AUTO,'N') 
     FROM   SA_SOH SH 
            INNER JOIN SA_TPSO TP ON SH.CD_COMPANY = TP.CD_COMPANY AND SH.TP_SO = TP.TP_SO  
     WHERE  SH.CD_COMPANY = @P_CD_COMPANY 
     AND    SH.NO_SO      = @P_NO_SO   
     IF @@ERROR <> 0 RETURN 

     IF(@V_SERVER_KEY LIKE 'YWD%') -- 영우디지탈
     BEGIN
          SELECT @V_STA_SO = ISNULL(STA_SO, 'O')
	      FROM	 SA_SOL
	      WHERE	 CD_COMPANY	= @P_CD_COMPANY AND NO_SO = @P_NO_SO AND SEQ_SO = @P_SEQSO 
          
          IF(@V_IV_AUTO = 'N' OR @V_STA_SO = 'O' ) RETURN
     END
     ELSE
     BEGIN
          IF(@P_CD_EXC = 'Y' OR @V_IV_AUTO = 'N') RETURN
     END
     SET	@V_CNT = 0
     SELECT @V_CNT = COUNT(1)
     FROM   MM_QTIO MQ 
            INNER JOIN MA_PLANT P ON MQ.CD_PLANT = P.CD_PLANT AND MQ.CD_COMPANY = P.CD_COMPANY  
            INNER JOIN MA_BIZAREA BIZ ON P.CD_BIZAREA = BIZ.CD_BIZAREA AND P.CD_COMPANY = BIZ.CD_COMPANY  
     WHERE  MQ.CD_COMPANY   = @P_CD_COMPANY 
     AND    MQ.NO_PSO_MGMT  = @P_NO_SO
     AND	(BIZ.VAT_BIZAREA = '' OR BIZ.VAT_BIZAREA IS NULL)
  
     IF (@V_CNT > 0)
     BEGIN  
          RAISERROR ('부가세신고사업장이 등록되지 않았습니다.', 18, 1)
          RETURN  
     END  
 
     SET	@V_CNT = 0
     SELECT @V_CNT = COUNT(1)   
     FROM   MM_QTIO MQ  
            INNER JOIN MA_SALEGRP SG ON MQ.CD_COMPANY = SG.CD_COMPANY AND MQ.CD_GROUP = SG.CD_SALEGRP  
     WHERE  MQ.CD_COMPANY = @P_CD_COMPANY 
     AND    NO_PSO_MGMT   = @P_NO_SO
     AND   (SG.CD_CC = '' OR SG.CD_CC IS NULL)  
 
     IF (@V_CNT > 0)  
     BEGIN   
          RAISERROR (' C/C 정보가 등록되지 않았습니다.' , 18, 1)
          RETURN 
     END  

     SET	@V_CNT = 0
     SELECT @V_CNT = COUNT(1) 
     FROM   MM_QTIO 
     WHERE  CD_COMPANY  = @P_CD_COMPANY
     AND    NO_PSO_MGMT = @P_NO_SO
     AND    YN_AM       = 'N'
  
     IF (@V_CNT > 0)  
     BEGIN   
          RAISERROR ('무한이므로 매출데이터를 생성 할 수 없습니다.' , 18, 1)   
          RETURN
     END 
   
     SET	@V_CNT = 0
     SELECT @V_CNT = COUNT(1) 
     FROM   MM_QTIO 
     WHERE  CD_COMPANY  = @P_CD_COMPANY
     AND    NO_PSO_MGMT = @P_NO_SO
     AND    YN_PURSALE  = 'N'
  
     IF (@V_CNT > 0)  
     BEGIN    
          RAISERROR ('매출유무가 N이므로 매출데이터를 생성 할 수 없습니다.' , 18, 1)   
          RETURN
     END 

     SELECT @V_DT_IO = DT_PROCESS
     FROM   SA_SOH_SUB 
     WHERE  CD_COMPANY = @P_CD_COMPANY
     AND    NO_SO      = @P_NO_SO

     BEGIN      
         SET    @V_NO_IV = ''          
         SELECT @V_NO_IV = NO_IV          
         FROM   SA_IVL        
         WHERE  CD_COMPANY = @P_CD_COMPANY 
         AND    NO_SO      = @P_NO_SO  
         AND    NO_SOLINE  = @P_SEQSO
    
         IF ISNULL(@V_NO_IV,'') <> ''   
         BEGIN  
              RAISERROR ('이미 해당 수주번호로 처리된 내역이 존재합니다. 분할처리될 수 없습니다.' , 18, 1)  
              RETURN
         END  
  
         SELECT TOP 1 @V_NO_IV = H.NO_IV
         FROM   SA_IVH H
	            INNER JOIN SA_IVL L ON H.CD_COMPANY = L.CD_COMPANY AND H.NO_IV = L.NO_IV
         WHERE  H.CD_COMPANY = @P_CD_COMPANY
         AND	L.NO_SO		 = @P_NO_SO
    
         IF @V_NO_IV IS NULL OR @V_NO_IV = ''          
         BEGIN 
              SET  @V_YM = LEFT(@V_DT_IO, 6)          
              EXEC NEOE.CP_GETNO @P_CD_COMPANY,'SA', '05', @V_YM,  @V_NO_IV OUTPUT      
              IF @@ERROR <> 0 RETURN   
   
              INSERT INTO SA_IVH   
              (  
               NO_IV,        
               CD_COMPANY,      
               CD_BIZAREA,   
               NO_BIZAREA,  
               CD_PARTNER,   
               BILL_PARTNER,  
               FG_TRANS,  
               AM_K,         
               VAT_TAX,         
               DT_PROCESS,   
               FG_TAX,      
               TP_FD,        
               AM_BAN,        
               NO_BAN,  
               TP_RECEIPT,   
               TP_SUMTAX,       
               FG_TAXP,      
               TP_AIS,      
               CD_DEPT,      
               NO_EMP,        
               YN_EXPIV, 
               NO_LC,        
               DT_RCP_RSV,      
               ETAX_SYS,     
               CD_EXCH,     
               RT_EXCH,      
               AM_EX,         
               FG_MAP, 
               AM_BAN_EX,    
               CD_BIZAREA_TAX,  
               CD_DOCU,      
               ID_INSERT,   
               DTS_INSERT,   
               FG_AR_EXC,
               FG_BILL,
               NM_PTR,
               EX_EMIL,
               EX_HP,
               DC_REMARK 
               )  
               SELECT @V_NO_IV, 
                      QH.CD_COMPANY, 
                      QL.CD_BIZAREA, 
                      P.NO_COMPANY NO_BIZAREA, 
                      QH.CD_PARTNER, 
                      QH.CD_PARTNER BILL_PARTNER, 
                      QL.FG_TRANS,
                      (CASE WHEN QH.YN_RETURN = 'N' THEN SUB.AM_IV ELSE SUB.AM_IV * -1 END) AM_K,
                      (CASE WHEN QH.YN_RETURN = 'N' THEN SUB.AM_IV_VAT ELSE SUB.AM_IV_VAT * -1 END) VAT_TAX, 
                      SUB.DT_PROCESS,
                      QL.FG_TAX, 
                      'D' TP_FD, 
                      0 AM_BAN, 
                      NULL NO_BAN,  
                      '1' TP_RECEIPT, 
                      '001' TP_SUMTAX, 
                      '001' FG_TAXP, 
                      'N' TP_AIS,   
                      QH.CD_DEPT, 
                      QH.NO_EMP, 
                      'N' YN_EXPIV, 
                      NULL NO_LC,  
                      ISNULL(SUB.DT_RCP_RSV,'') DT_RCP_RSV, 
                      '00' ETAX_SYS, 
                      QL.CD_EXCH CD_EXCH, 
                      QL.RT_EXCH, 
                      (CASE WHEN QH.YN_RETURN = 'N' THEN SUB.AM_IV_EX ELSE SUB.AM_IV_EX * -1 END) AM_EX,    
                      '0' FG_MAP, 
                      0 AM_BAN_EX, 
                      BI.VAT_BIZAREA CD_BIZAREA_TAX,  
                      ISNULL(MAX(MA.CD_DOCU),'23') CD_DOCU, 
                      @P_ID_USER ID_INSERT, 
                      @V_SYSDATE DTS_INSERT,
                      SUB.FG_AR_EXC,
                      P.FG_BILL,
                      SUB.NM_PTR,
					  SUB.EX_EMIL,
					  SUB.EX_HP,
					  SUB.DC_RMK
               FROM   MM_QTIO QL    
                      INNER JOIN MM_QTIOH QH ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_IO = QH.NO_IO  
                      INNER JOIN MA_PARTNER P ON QH.CD_COMPANY = P.CD_COMPANY AND QH.CD_PARTNER = P.CD_PARTNER   
                      INNER JOIN MA_BIZAREA BI ON QL.CD_COMPANY = BI.CD_COMPANY AND QL.CD_BIZAREA = BI.CD_BIZAREA   
                      LEFT  JOIN SA_SOH SOH ON QL.CD_COMPANY = SOH.CD_COMPANY AND QL.NO_PSO_MGMT = SOH.NO_SO  
                      INNER JOIN SA_SOH_SUB SUB ON SOH.CD_COMPANY = SUB.CD_COMPANY AND SOH.NO_SO = SUB.NO_SO 
                      LEFT  JOIN SA_TPSO TPSO ON SOH.CD_COMPANY = TPSO.CD_COMPANY AND SOH.TP_SO = TPSO.TP_SO   
                      LEFT  JOIN SA_TPSO TP ON SOH.TP_SO = TP.TP_SO AND SOH.CD_COMPANY = TP.CD_COMPANY   
                      LEFT  JOIN MA_AISPOSTH MA ON QL.CD_COMPANY = MA.CD_COMPANY AND QL.FG_TPIO = MA.CD_TP AND MA.FG_TP = '100'
               WHERE  QL.CD_COMPANY = @P_CD_COMPANY 
               AND    QL.NO_PSO_MGMT = @P_NO_SO      
               AND    QL.FG_IO IN ('010', '041') 
               AND    QL.YN_PURSALE = 'Y' 
               AND    QL.YN_AM = 'Y'   
               GROUP  BY QH.CD_COMPANY, QL.CD_BIZAREA, P.NO_COMPANY, QH.CD_PARTNER, QL.FG_TRANS, QL.FG_TAX, QL.CD_EXCH, SUB.DT_RCP_RSV, 
                         QL.FG_TAX, SUB.DT_PROCESS,SUB.DT_RCP_RSV, QH.NO_EMP, SUB.FG_AR_EXC, QH.CD_DEPT, QL.RT_EXCH, BI.VAT_BIZAREA, P.FG_BILL, 
                         TP.TP_VAT, QH.YN_RETURN, SUB.AM_IV_EX, SUB.AM_IV, SUB.AM_IV_VAT, SUB.NM_PTR, SUB.EX_EMIL, SUB.EX_HP, SUB.DC_RMK   
               IF @@ERROR <> 0 RETURN    
          END   
     END  
    
     INSERT INTO SA_IVL  
     (  
      NO_IV,        
      NO_LINE,      
      CD_COMPANY, 
      CD_PLANT,     
      NO_IO,          
      NO_IOLINE,  
      CD_ITEM,      
      CD_SALEGRP,   
      CD_CC,      
      DT_TAX,       
      QT_GI_CLS,      
      QT_CLS,  
      UM_ITEM_CLS,  
      AM_CLS,      
      VAT,        
      NO_EMP,       
      CD_PJT,         
      TP_IV,  
      NO_SO,        
      NO_SOLINE,    
      CD_EXCH,    
      RT_EXCH,      
      YN_RETURN,  
      UM_EX_CLS,    
      AM_EX_CLS,    
      FG_TRANS,
      NO_IV_ORG,
      ID_INSERT,    
      DTS_INSERT
      )  

      SELECT @V_NO_IV, 
             QL.NO_IOLINE, 
             QL.CD_COMPANY, 
             QL.CD_PLANT, 
             QL.NO_IO, 
             QL.NO_IOLINE,  
             QL.CD_ITEM, 
             QL.CD_GROUP CD_SALEGRP, 
             SG.CD_CC CD_CC, 
             QL.DT_IO DT_TAX,  
             QL.QT_UNIT_MM  QT_GI_CLS, --잔량 = 수배수량 - 수배마감수량
             QL.QT_IO  QT_CLS,         --관리수량  
             QL.UM UM_K,  --단가   
             QL.AM AM_CLS,
             QL.VAT,                   --부가세  
             QL.NO_EMP, 
             QL.CD_PJT, 
             ISNULL(TPSO.TP_IV, QL.FG_TPIO) TP_IV,  
             QL.NO_PSO_MGMT NO_SO, 
             QL.NO_PSOLINE_MGMT NO_SOLINE, 
             QL.CD_EXCH, 
             QL.RT_EXCH, 
             QH.YN_RETURN,
             QL.UM_EX AS UM_EX_CLS,     --외화단가
             QL.AM_EX AS AM_EX_CLS,     --외화금액
             QL.FG_TRANS,
             @V_NO_IV,
             @P_ID_USER ID_INSERT, 
             @V_SYSDATE DTS_INSERT
      FROM   MM_QTIO QL       
             INNER JOIN MM_QTIOH QH ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_IO = QH.NO_IO   
             INNER JOIN MA_BIZAREA BI ON QL.CD_COMPANY = BI.CD_COMPANY AND QL.CD_BIZAREA = BI.CD_BIZAREA   
             INNER JOIN MA_SALEGRP SG ON QL.CD_COMPANY = SG.CD_COMPANY AND QL.CD_GROUP = SG.CD_SALEGRP  
             LEFT  JOIN SA_SOH SOH ON QL.CD_COMPANY = SOH.CD_COMPANY AND QL.NO_PSO_MGMT = SOH.NO_SO   
             LEFT  JOIN SA_TPSO TPSO ON SOH.CD_COMPANY = TPSO.CD_COMPANY AND SOH.TP_SO = TPSO.TP_SO   
             LEFT  JOIN MA_PITEM ITEM ON QL.CD_ITEM = ITEM.CD_ITEM AND QL.CD_PLANT = ITEM.CD_PLANT AND QL.CD_COMPANY = ITEM.CD_COMPANY  
      WHERE  QL.CD_COMPANY = @P_CD_COMPANY 
      AND    QL.NO_PSO_MGMT = @P_NO_SO 
      AND   (@P_YN_ALL = 'Y' OR QL.NO_PSOLINE_MGMT = @P_SEQSO)       
      AND    QL.FG_IO IN ('010', '041') 
      AND    QL.YN_PURSALE = 'Y' 
      AND    QL.YN_AM = 'Y'
      ORDER  BY QL.NO_IO, QL.NO_IOLINE
      IF @@ERROR <> 0 RETURN  

END
GO