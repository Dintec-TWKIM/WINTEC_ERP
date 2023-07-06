USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_FI_DOCU_AUTODEL]    Script Date: 2016-04-27 오전 9:43:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_FI_DOCU_AUTODEL]        
(
	@P_CD_COMPANY  NVARCHAR(7),                    
	@P_NO_MODULE   NVARCHAR(3),                    
	@P_NO_MDOCU    NVARCHAR(20),                    
	@P_ID_UPDATE   NVARCHAR(15)                    
)                    
AS          
BEGIN                    
 -- 전표처리후 CC_SA_RCP_001                    
 SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
                     
 DECLARE @P_DTS_UPDATE VARCHAR(14)                    
				  
 SET @P_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')                    
				  
 DECLARE                     
 @V_ERRNO   INT,   -- ERROR 번호                    
 @V_ERRMSG   VARCHAR(255),   -- ERROR 메시지                     
 @V_ST_DOCU   NCHAR(1),  -- 전표상태                    
 @V_NO_DOCU   NVARCHAR(20),   -- 회계전표번호                    
 @V_NO_DOLINE  NUMERIC(4,0),                    
 @V_TP_ACAREA  NCHAR(1),                    
 @V_CD_RELATION  NCHAR(2),                    
 @V_CD_FUND   NCHAR(4),                    
 @V_CD_MNG   NVARCHAR(50),                    
 @V_DT_START  NCHAR(8),                    
 @V_DT_END   NCHAR(8),                        
 @V_CD_RELATION1 NCHAR(2),                     
 @V_CD_PC   NVARCHAR(14),                     
 @V_SEQ_SO   NUMERIC(3,0),            
 @V_NO_MGMT   NVARCHAR(20),                  
 @SUGUM    NCHAR(5),  
 @V_SERVER_KEY NVARCHAR(50)
 --@V_ST_DOCUAPP	NVARCHAR(1) -- 사용자 권한                                          
		  
SELECT @V_SERVER_KEY = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG WHERE YN_UPGRADE = 'Y'                                         
 -------------------------------------------------------------------------------------------------                    
					 
 --PC를 구하는 구문(매출삭제할 때)                    
 --CD_BIZAREA부가세사업장으로수정 20081217                
 SELECT @V_CD_PC = MAX(B.CD_PC)                    
 FROM  SA_IVH A                    
 LEFT JOIN MA_BIZAREA B ON B.CD_COMPANY = @P_CD_COMPANY AND B.CD_BIZAREA = A.CD_BIZAREA_TAX                  
 WHERE  A.CD_COMPANY = @P_CD_COMPANY                    
 AND   A.NO_IV    = @P_NO_MDOCU                    
				
				
 --PC를 구하는 구문(EC 삭제할 때)                    
 IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(B.CD_PC)                    
  FROM  EC_IVH A                    
  LEFT JOIN MA_BIZAREA B ON B.CD_COMPANY = @P_CD_COMPANY AND B.CD_BIZAREA = A.CD_BIZAREA                    
  WHERE  A.CD_COMPANY = @P_CD_COMPANY                    
  AND   A.NO_IV    = @P_NO_MDOCU                    
 END                
				
 --PC를 구하는 구문(AS 매출삭제할 때)                    
 IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(B.CD_PC)                    
  FROM  AS_IVH A                    
  LEFT JOIN MA_BIZAREA B ON B.CD_COMPANY = @P_CD_COMPANY AND B.CD_BIZAREA = A.CD_BIZAREA_TAX                  
  WHERE  A.CD_COMPANY = @P_CD_COMPANY                    
  AND   A.NO_IV    = @P_NO_MDOCU                    
 END                
 --PC를 구하는 구문(수금삭제할 때)                
  IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
			   
  SELECT @V_CD_PC = MAX(G.CD_PC)                     
		FROM SA_RCPH A                  
		JOIN MA_BIZAREA G ON A.CD_COMPANY = G.CD_COMPANY AND A.CD_BIZAREA = G.CD_BIZAREA              
  WHERE  A.CD_COMPANY = @P_CD_COMPANY                    
  AND   A.NO_RCP   = @P_NO_MDOCU                
				
 END                    
				   
 IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
--  SELECT @V_CD_PC = C.CD_PC                     
--  FROM SA_RCPH A                    
--  LEFT JOIN MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY AND B.NO_EMP = A.NO_EMP        
--  LEFT JOIN MA_CC C ON C.CD_COMPANY = @P_CD_COMPANY AND C.CD_CC = B.CD_CC                    
--  WHERE  A.CD_COMPANY = @P_CD_COMPANY            
--  AND   A.NO_RCP   = @P_NO_MDOCU                    
				
  SELECT @V_CD_PC = MAX(CC.CD_PC)                     
  FROM SA_RCPH A                    
		 JOIN MA_SALEGRP G ON                
						 A.CD_COMPANY = G.CD_COMPANY AND                
						 A.CD_SALEGRP = G.CD_SALEGRP                  
		 JOIN MA_CC CC ON                 
						 G.CD_COMPANY = CC.CD_COMPANY AND                
						 G.CD_CC      = CC.CD_CC                  
  WHERE  A.CD_COMPANY = @P_CD_COMPANY                    
  AND   A.NO_RCP   = @P_NO_MDOCU                
				
 END                    
					
 --PC를 구하는 구문(선수금정리삭제할 때)                    
 IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(C.CD_PC)                     
  FROM SA_BILLSH A                    
  LEFT JOIN MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY AND B.NO_EMP = A.NO_EMP                    
  LEFT JOIN MA_CC C ON C.CD_COMPANY = @P_CD_COMPANY AND C.CD_CC = B.CD_CC                    
  WHERE A.CD_COMPANY = @P_CD_COMPANY                    
  AND  A.NO_BILLS   = @P_NO_MDOCU                    
 END

 --PC를 구하는 구문(선지급정리삭제할 때)                    
 IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(C.CD_PC)                     
  FROM CZ_PU_ADPAYMENT_BILL_H A                    
  LEFT JOIN MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY AND B.NO_EMP = A.NO_EMP                    
  LEFT JOIN MA_CC C ON C.CD_COMPANY = @P_CD_COMPANY AND C.CD_CC = B.CD_CC                    
  WHERE A.CD_COMPANY = @P_CD_COMPANY                    
  AND  A.NO_BILLS   = @P_NO_MDOCU                    
 END                    
					 
 --PC를 구하는 구문(구매/자재관리>매입관리>매입관리)                 
 --2009.12.15 매입부가세사업장 CD_PC 구해서 삭제 -by SMR (KHS/크라제,KTIS)              
 IF (@P_NO_MODULE = '210' AND ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(B.CD_PC)                
FROM PU_IVH A                    
	LEFT JOIN MA_BIZAREA B ON B.CD_COMPANY = @P_CD_COMPANY AND B.CD_BIZAREA = A.CD_BIZAREA_TAX                  
  --LEFT JOIN MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY AND B.NO_EMP = A.NO_EMP                    
  --LEFT JOIN MA_CC C ON C.CD_COMPANY = @P_CD_COMPANY AND C.CD_CC = B.CD_CC                    
  WHERE A.CD_COMPANY = @P_CD_COMPANY                    
  AND  A.NO_IV    = @P_NO_MDOCU                    
 END                    
 --  PRINT(ISNULL(@V_CD_PC,'NULL이다'))                                
 --exec UP_FI_DOCU_AUTODEL @P_CD_COMPANY=N'KHS',@P_NO_MODULE=N'400',@P_NO_MDOCU=N'OTX20110200040',@P_ID_UPDATE=N'1'            
					
 --PC를 구하는 구문(외주매입 삭제할때)          
 -- 전표처리시  MA_BIZAREA.CD_PC로 처리했다...               
 IF (@P_NO_MODULE = '400' AND ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(B.CD_PC)                     
  FROM SU_IVH A           
   LEFT JOIN MA_BIZAREA B ON          
				 A.CD_BIZAREA_TAX = B.CD_BIZAREA AND                                                           
				 A.CD_COMPANY = B.CD_COMPANY                                    
  --LEFT JOIN MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY AND B.NO_EMP = A.NO_EMP                    
  --LEFT JOIN MA_CC C ON C.CD_COMPANY = @P_CD_COMPANY AND C.CD_CC = B.CD_CC                    
  WHERE A.CD_COMPANY = @P_CD_COMPANY                    
  AND  A.NO_IV    = @P_NO_MDOCU  
  
  
   IF (ISNULL(@V_CD_PC, '') = '')                        
 BEGIN                        
				   
  SELECT @V_CD_PC = MAX(G.CD_PC)                         
		FROM SU_TR_COSTEXH A                      
		JOIN MA_BIZAREA G ON A.CD_COMPANY = G.CD_COMPANY AND A.CD_BIZAREA = G.CD_BIZAREA                  
  WHERE  A.CD_COMPANY = @P_CD_COMPANY                        
  AND   A.NO_SALECOST   = @P_NO_MDOCU           
   END                       
 END                    
					 
 --PC를 구하는 구문(미착정산 삭제할때)                    
 IF (ISNULL(@V_CD_PC,'') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(MB.CD_PC)   -- 2011-08-30, 미착정산 전표표기 오류로 MAX처리됨.                 
  FROM TR_TO_IML AS TTL                    
  JOIN MA_PITEM AS MP ON TTL.CD_ITEM = MP.CD_ITEM AND TTL.CD_PLANT = MP.CD_PLANT AND TTL.CD_COMPANY = MP.CD_COMPANY                    
  JOIN MA_BIZAREA AS MB ON  MP.CD_BIZAREA = MB.CD_BIZAREA  AND MP.CD_COMPANY = MB.CD_COMPANY                      
  WHERE                     
  TTL.NO_TO = @P_NO_MDOCU AND                    
  TTL.CD_COMPANY = @P_CD_COMPANY                    
 END                    
					 
 --PC를 구하는 구문(부대비용전표 삭제할때)                    
 IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(MB.CD_PC)                    
  FROM TR_IMCOSTH AS TIH                    
  JOIN MA_BIZAREA AS MB ON  TIH.CD_BIZAREA = MB.CD_BIZAREA  AND TIH.CD_COMPANY = MB.CD_COMPANY                     
  WHERE                    
  TIH.NO_COST = @P_NO_MDOCU AND                    
  TIH.CD_COMPANY = @P_CD_COMPANY                    
 END                    
				 
 --선적등록 삭제시                
 IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(MB.CD_PC)                    
  FROM TR_EXBL AS EXBL                 
  JOIN MA_BIZAREA AS MB ON  EXBL.CD_BIZAREA = MB.CD_BIZAREA  AND EXBL.CD_COMPANY = MB.CD_COMPANY                     
  WHERE                    
  EXBL.NO_BL = @P_NO_MDOCU AND                    
  EXBL.CD_COMPANY = @P_CD_COMPANY                    
 END                 
				   
 --NEGO등록 삭제시                
 IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(MB.CD_PC)                    
  FROM TR_NEGOEXH AS NEGOEXH                   
  JOIN MA_BIZAREA AS MB ON  NEGOEXH.CD_BIZAREA = MB.CD_BIZAREA  AND NEGOEXH.CD_COMPANY = MB.CD_COMPANY                     
  WHERE                    
  NEGOEXH.NO_NEGO = @P_NO_MDOCU AND                    
  NEGOEXH.CD_COMPANY = @P_CD_COMPANY                    
 END                   
				
 --NEGO정리등록 삭제시                
 IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(MB.CD_PC)                    
  FROM TR_NEGOEXS AS NEGOEXS                   
  JOIN MA_BIZAREA AS MB ON  NEGOEXS.CD_BIZAREA = MB.CD_BIZAREA  AND NEGOEXS.CD_COMPANY = MB.CD_COMPANY                     
  WHERE                    
  NEGOEXS.NO_NEGOS = @P_NO_MDOCU AND                    
  NEGOEXS.CD_COMPANY = @P_CD_COMPANY                    
 END                 
		 
		 
 --일괄네고등록 삭제시(2011-08-30, 최승애)        
 IF (ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(MB.CD_PC)                    
  FROM TR_NEGOEXH AS NEGOEXH                   
  JOIN MA_BIZAREA AS MB ON  NEGOEXH.CD_BIZAREA = MB.CD_BIZAREA  AND NEGOEXH.CD_COMPANY = MB.CD_COMPANY                     
  WHERE                    
  NEGOEXH.NEGO_GRP   = @P_NO_MDOCU AND                    
  NEGOEXH.CD_COMPANY = @P_CD_COMPANY                    
 END                   
		 
		 
		  
				
 --PC를 구하는 구문(공정외주매입 삭제할때)                
	--20091112 코드 추가                
	--PR_OPOUT_IVH 테이블은 신규 추가                
 IF (@P_NO_MODULE = '800' AND ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(C.CD_PC)                     
  FROM PR_OPOUT_IVH A                    
  LEFT JOIN MA_EMP B ON B.CD_COMPANY = @P_CD_COMPANY AND B.NO_EMP = A.NO_EMP                    
  LEFT JOIN MA_CC C ON C.CD_COMPANY = @P_CD_COMPANY AND C.CD_CC = B.CD_CC                    
  WHERE A.CD_COMPANY = @P_CD_COMPANY                    
  AND  A.NO_IV    = @P_NO_MDOCU                    
 END                    
				 
			   
				   
 --PC를 구하는 구문(계정대체출고 마감 삭제할때)  20100202 코드 추가                
				  
 IF (@P_NO_MODULE = '700' AND ISNULL(@V_CD_PC, '') = '')                    
 BEGIN                    
  SELECT @V_CD_PC = MAX(E.CD_PC)              
  FROM  MM_AMINVH A                                 
   JOIN MA_PLANT P ON P.CD_COMPANY = A.CD_COMPANY              
		AND P.CD_PLANT = A.CD_PLANT              
   JOIN MA_BIZAREA E ON E.CD_COMPANY = A.CD_COMPANY              
						  AND E.CD_COMPANY = P.CD_COMPANY              
		AND E.CD_BIZAREA = P.CD_BIZAREA              
  WHERE A.CD_COMPANY  = @P_CD_COMPANY                    
  AND  A.YM_STANDARD  = LEFT(@P_NO_MDOCU, LEN(@P_NO_MDOCU) - ( LEN(@P_NO_MDOCU) - CHARINDEX('+' ,@P_NO_MDOCU) ) - 1)              
  AND  A.CD_PLANT     = RIGHT(@P_NO_MDOCU, LEN(@P_NO_MDOCU) - ( CHARINDEX('+' ,@P_NO_MDOCU) ) )              
				
 END              
					 
							
 -------------------------------------------------------------------------------------------------                    
				
 SELECT @V_ST_DOCU = ST_DOCU, @V_NO_DOCU = NO_DOCU                    
 FROM  FI_DOCU                     
 WHERE CD_COMPANY = @P_CD_COMPANY                    
 AND  NO_MODULE = @P_NO_MODULE                    
 AND  NO_MDOCU  = @P_NO_MDOCU                    
 AND  (@V_CD_PC IS NULL OR  CD_PC    = @V_CD_PC   ) -- AND  CD_PC    = @V_CD_PC                    
 GROUP BY ST_DOCU, NO_DOCU
 
 --SELECT @V_ST_DOCUAPP = ST_DOCUAPP 
 --FROM MA_USER 
 --WHERE CD_COMPANY = @P_CD_COMPANY 
 --AND ID_USER = @P_ID_UPDATE
					 
 --PRINT(@P_CD_COMPANY)                    
 --PRINT(@P_NO_MODULE)                    
 --PRINT(@P_NO_MDOCU)                    
		 
		  
		  
 --PRINT(@V_ST_DOCU)                    
 --PRINT(@V_NO_DOCU)                    
					 
 --만약, 전표상태가 승인상태라면 삭제가 불가능합니다.                    
				  
 --IF(@V_ST_DOCU = '2' AND (@V_ST_DOCUAPP = '1' OR @V_ST_DOCUAPP = '2'))
 IF @V_ST_DOCU = '2'                    
 BEGIN                    
  SELECT @V_ERRNO  = 100000, @V_ERRMSG = 'KR##승인된 전표이기 때문에 전표 삭제가 처리되지 못했습니다.##US##Cannot delete slip. because Approved slip##'                      
  GOTO ERROR                    
 END                    
					 
 -------------------------------------------------------------------------------------------------                    
 /* 커서 시작 */                    
 DECLARE CUR_FIDELETE CURSOR FOR                    
				
 SELECT A.NO_DOCU, A.NO_DOLINE, A.CD_PC, A.CD_COMPANY, A.TP_ACAREA,                     
 A.CD_RELATION, A.CD_FUND, A.CD_MNG, A.DT_START, A.DT_END, B.CD_RELATION AS CD_RELATION1                    
 FROM FI_DOCU A                    
 LEFT JOIN FI_ACCTCODE B ON B.CD_COMPANY = @P_CD_COMPANY AND A.CD_ACCT = B.CD_ACCT                    
 WHERE A.CD_COMPANY = @P_CD_COMPANY          
 AND A.NO_DOCU = @V_NO_DOCU                 
 AND ( @V_CD_PC IS NULL OR A.CD_PC = @V_CD_PC  ) -- AND A.CD_PC = @V_CD_PC                    
				 
--PRINT('open')          
					 
 OPEN CUR_FIDELETE                       
 FETCH NEXT FROM CUR_FIDELETE                       
 INTO  @V_NO_DOCU, @V_NO_DOLINE, @V_CD_PC, @P_CD_COMPANY, @V_TP_ACAREA, @V_CD_RELATION, @V_CD_FUND, @V_CD_MNG, @V_DT_START, @V_DT_END, @V_CD_RELATION1                    
				
 WHILE (@@FETCH_STATUS = 0)                       
 BEGIN           
		 
  IF (@V_CD_RELATION = '45' )                  
  BEGIN          
 SET @V_NO_MGMT = SUBSTRING(@V_CD_MNG, 0, CHARINDEX('|', @V_CD_MNG))           
 EXEC UP_FI_DOCU_DELETE @V_NO_DOCU, @V_NO_DOLINE, @V_CD_PC, @P_CD_COMPANY, @V_TP_ACAREA, @V_CD_RELATION, @V_CD_FUND, @V_NO_MGMT, @V_DT_START, @V_DT_END, @P_ID_UPDATE, @V_CD_RELATION1                         
  END          
  ELSE          
  BEGIN          
   EXEC UP_FI_DOCU_DELETE @V_NO_DOCU, @V_NO_DOLINE, @V_CD_PC, @P_CD_COMPANY, @V_TP_ACAREA, @V_CD_RELATION, @V_CD_FUND, @V_CD_MNG, @V_DT_START, @V_DT_END, @P_ID_UPDATE, @V_CD_RELATION1                         
		
  END           
  IF (@@ERROR <> 0 )                    
   BEGIN                      
   CLOSE CUR_FIDELETE     DEALLOCATE CUR_FIDELETE                     
   SELECT @V_ERRNO  = 100000, @V_ERRMSG = 'KR##라인 삭제 로직 타기 실패##US##Fail! Line Delete...##'                      
   GOTO ERROR                      
  END                    
			
  FETCH NEXT FROM CUR_FIDELETE                     
  INTO  @V_NO_DOCU, @V_NO_DOLINE, @V_CD_PC, @P_CD_COMPANY, @V_TP_ACAREA, @V_CD_RELATION, @V_CD_FUND, @V_CD_MNG, @V_DT_START, @V_DT_END, @V_CD_RELATION1                    
 END                       
		   
		   
					 
 CLOSE CUR_FIDELETE                         
 DEALLOCATE CUR_FIDELETE                    
		   
		   
					 
 /* 유차장님 작업 후 삭제 예정 --삭제안함! */                    
 UPDATE SA_IVH                    
 SET   TP_AIS = 'N', ID_UPDATE = @P_ID_UPDATE, DTS_UPDATE = @P_DTS_UPDATE                    
 WHERE  CD_COMPANY = @P_CD_COMPANY    
 AND   NO_IV    = @P_NO_MDOCU                    
			  
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
			  
 UPDATE SA_RCPH                    
 SET   TP_AIS = 'N', ID_UPDATE = @P_ID_UPDATE, DTS_UPDATE = @P_DTS_UPDATE                    
 WHERE  CD_COMPANY = @P_CD_COMPANY                    
 AND   NO_RCP   = @P_NO_MDOCU                    
			  
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
				
 UPDATE SA_BILLSH                    
 SET   ST_BILL = 'N', ID_UPDATE = @P_ID_UPDATE, DTS_UPDATE = @P_DTS_UPDATE                    
 WHERE  CD_COMPANY = @P_CD_COMPANY                    
 AND   NO_BILLS   = @P_NO_MDOCU                    
				
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택      

 UPDATE CZ_PU_ADPAYMENT_BILL_H                    
 SET   ST_BILL = 'N', ID_UPDATE = @P_ID_UPDATE, DTS_UPDATE = @P_DTS_UPDATE                    
 WHERE  CD_COMPANY = @P_CD_COMPANY                    
 AND   NO_BILLS   = @P_NO_MDOCU                    
				
IF @@ERROR <> 0 RETURN
				
 UPDATE PU_IVH                    
 SET   TP_AIS = 'N', ID_UPDATE = @P_ID_UPDATE, DTS_UPDATE = @P_DTS_UPDATE                    
 WHERE  CD_COMPANY = @P_CD_COMPANY                    
 AND   NO_IV    = @P_NO_MDOCU                    
			  
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
				
 UPDATE SU_IVH                    
SET   TP_AIS = 'N', ID_UPDATE = @P_ID_UPDATE, DTS_UPDATE = @P_DTS_UPDATE                    
 WHERE  CD_COMPANY = @P_CD_COMPANY                    
 AND   NO_IV    = @P_NO_MDOCU                    
			   
 IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택                    
			  
 UPDATE TR_DISTRIBU                    
 SET   YN_SLIP = 'N'                    
 WHERE  CD_COMPANY = @P_CD_COMPANY                    
 AND   NO_TO  = @P_NO_MDOCU                    
				
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
				  
 UPDATE TR_IMCOSTH                    
 SET   YN_SLIP = 'N'                    
 WHERE  CD_COMPANY = @P_CD_COMPANY                    
 AND   NO_COST = @P_NO_MDOCU                     
			  
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
				  
				  
 UPDATE EC_IVH                    
 SET   TP_AIS = 'N'                    
 WHERE  CD_COMPANY = @P_CD_COMPANY                    
 AND   NO_IV = @P_NO_MDOCU                   
			  
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
				 
 UPDATE TR_EXBL                
 SET YN_SLIP = 'N'              
 WHERE CD_COMPANY = @P_CD_COMPANY                    
 AND NO_BL = @P_NO_MDOCU                  
			  
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
				
 UPDATE TR_NEGOEXH                 
 SET YN_SLIP = 'N'              
 WHERE CD_COMPANY = @P_CD_COMPANY                    
 AND NO_NEGO = @P_NO_MDOCU                
				
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택                
   
		
-- 2011-07-08, 최승애, 일괄NEGO처리시 사용-------------        
UPDATE TR_NEGOEXH                 
 SET YN_SLIP = 'N'            
 WHERE CD_COMPANY = @P_CD_COMPANY                    
 AND NEGO_GRP = @P_NO_MDOCU                
				
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택                        
				
				
				
 UPDATE TR_NEGOEXS                 
 SET YN_SLIP = 'N'                     
 WHERE CD_COMPANY = @P_CD_COMPANY                    
 AND NO_NEGOS = @P_NO_MDOCU                   
			  
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
			   
 /* ---------------- 20090810추가 */                    
 UPDATE AS_IVH                    
 SET   TP_AIS = 'N'                    
 WHERE  CD_COMPANY = @P_CD_COMPANY                    
 AND   NO_IV = @P_NO_MDOCU                   
			  
IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
				
	--2009/11/12 추가                
	--PR_OPOUT_IVH 테이블은 신규 추가               
IF (@P_NO_MODULE = '800' )               
BEGIN              
 UPDATE PR_OPOUT_IVH                
 SET YN_DOCU = 'N',                 
		NO_DOCU = ''                
 WHERE CD_COMPANY = @P_CD_COMPANY                
  AND NO_IV = @P_NO_MDOCU               
				
  IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
				
END               
				
   -- 20100202추가               
 IF (@P_NO_MODULE = '700' )                    
 BEGIN                  
 UPDATE MM_AMINVH SET TP_AIS = 'N'               
 WHERE CD_COMPANY  = @P_CD_COMPANY              
   AND YM_STANDARD  = LEFT(@P_NO_MDOCU, LEN(@P_NO_MDOCU) - ( LEN(@P_NO_MDOCU) - CHARINDEX('+' ,@P_NO_MDOCU) ) - 1)              
   AND CD_PLANT     = RIGHT(@P_NO_MDOCU, LEN(@P_NO_MDOCU) - ( CHARINDEX('+' ,@P_NO_MDOCU) ) )                
				 
   IF @@ERROR <> 0 RETURN -- 추가된 부분 2010-05-19 광진윈택              
  END        
		  
		  
 UPDATE TR_TO_IMH     -- 전표번호        
 SET   NO_DOCU = ''                    
 WHERE  CD_COMPANY = @P_CD_COMPANY                    
 AND   NO_TO    = @P_NO_MDOCU            
		 
 IF @@ERROR <> 0 RETURN -- 추가된 부분 2011-04-13 최규원                
					
					
 UPDATE TR_IMCOSTH                    
 SET     NO_DOCU = ''                    
 WHERE   CD_COMPANY = @P_CD_COMPANY                    
 AND     NO_COST = @P_NO_MDOCU             
		   
 IF @@ERROR <> 0 RETURN             
		
UPDATE SU_TR_COSTEXH     SET NO_DOCU = NULL    WHERE   CD_COMPANY = @P_CD_COMPANY                  AND     NO_SALECOST = @P_NO_MDOCU     
 IF @@ERROR <> 0 RETURN       
	  
-- 조선호텔베이커리 전용처리      
IF ( 'CHOSUNHOTELBA' = @V_SERVER_KEY)      
BEGIN      
--1. 자재요청 테이블의 전표 처리 여부 FLAG 값 업데이트 추가      
--UPDATE PU_ITEM_REQ_L       
--   SET FG_FI='N'       
--  FROM PU_IVL IVL      
--       JOIN PU_POL POL ON POL.NO_PO = IVL.NO_PO AND POL.NO_LINE = IVL.NO_POLINE AND POL.CD_COMPANY = IVL.CD_COMPANY        
--       JOIN PU_PRL PRL ON PRL.NO_PR = POL.NO_PR AND PRL.NO_PRLINE = POL.NO_PRLINE AND PRL.CD_COMPANY = POL.CD_COMPANY      
--       JOIN PU_ITEM_REQ_L REQ ON REQ.NO_MREQ = PRL.CD_USERDEF1 AND REQ.NO_MREQLINE = PRL.CD_USERDEF2 AND REQ.CD_COMPANY = PRL.CD_COMPANY AND REQ.CD_PLANT= @CD_PLANT      
-- WHERE IVL.CD_COMPANY=@P_CD_COMPANY AND IVL.NO_IV=@P_NO_IV       
	   
  IF @@ERROR <> 0 RETURN        
	  
--2. 구매 견적 테이블의 전표 처리 여부 FLAG 값 업데이트 추가      
--UPDATE PU_EESTIMATEL       
--   SET FG_FI='N'       
--  FROM PU_IVL IVL      
--       JOIN PU_POL POL ON POL.NO_PO = IVL.NO_PO AND POL.NO_LINE = IVL.NO_POLINE AND POL.CD_COMPANY = IVL.CD_COMPANY        
--       JOIN PU_EESTIMATEL EES ON EES.NO_EEST = POL.NO_EEST AND EES.CD_ITEM = POL.CD_ITEM AND EES.CD_COMPANY = POL.CD_COMPANY AND EES.CD_PLANT= @CD_PLANT 
-- WHERE IVL.CD_COMPANY=@P_CD_COMPANY AND IVL.NO_IV=@P_NO_IV       
	  
 IF @@ERROR <> 0 RETURN                  
END        
  
  
  
   
--한국AVL 전용처리 (2012.11.22 SMR/전정식)  
IF ( 'KORAVL' = @V_SERVER_KEY AND @P_NO_MODULE IN ('110','210')) -- 우선 매입/매출만 관리내역에서 삭제 / 무역의 INVOICE는 중복된게 있을 수 있으므로 삭제할 수 없다(전정식사원이 체크하고 작업하기로...)  
BEGIN  
   DELETE FROM  FI_MNGD WHERE  CD_MNG = 'A22' AND CD_MNGD = @P_NO_MDOCU AND CD_COMPANY  = @P_CD_COMPANY    
END  
                
 RETURN                    
  ERROR:             
 ROLLBACK TRAN                       
 RAISERROR(@V_ERRMSG, 16, 1) -- 2015/03/05:자동화 변경[RAISERROR], 변경전:RAISERROR @V_ERRNO @V_ERRMSG 
			 
END
GO

