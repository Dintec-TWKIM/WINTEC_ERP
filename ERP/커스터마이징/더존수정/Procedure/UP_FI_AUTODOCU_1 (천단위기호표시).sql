USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[UP_FI_AUTODOCU_1]    Script Date: 2018-01-10 오후 4:31:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [NEOE].[UP_FI_AUTODOCU_1]          
(              
	-- 전표 PKEY(4개)              
	@P_NO_DOCU  NVARCHAR(20),              
	@P_NO_DOLINE NUMERIC(5,0),              
	@P_CD_PC  NVARCHAR(7),              
	@P_CD_COMPANY   NVARCHAR(7),              
	          
	@P_CD_WDEPT   NVARCHAR(12),              
	@P_ID_WRITE   NVARCHAR(10),              
	@P_DT_ACCT    NCHAR(8),              
	@P_NO_ACCT   NUMERIC(5,0) = 0,              
	@P_TP_DOCU   NCHAR(1),              
	@P_CD_DOCU   NVARCHAR(3),    --LINE : 10              
	          
	@P_ST_DOCU   NCHAR(1),              
	@P_ID_ACCEPT   NVARCHAR(10),              
	@P_TP_DRCR    NCHAR(1),              
	@P_CD_ACCT    NVARCHAR(10),              
	@P_NM_NOTE   NVARCHAR(100),              
	@P_AM_DR    NUMERIC(19,4),              
	@P_AM_CR    NUMERIC(19,4),              
	@P_TP_ACAREA   NCHAR(1),    -- '4' 일 경우 반제원인전표이므로 FI_BANH에 INSERT된다              
	@P_CD_RELATION NCHAR(2),              
	@P_CD_BUDGET   NVARCHAR(20),    --LINE : 20              
	          
	@P_CD_FUND   NCHAR(4),              
	@P_NO_BDOCU   NVARCHAR(20),              
	@P_NO_BDOLINE   NUMERIC(5,0),              
	@P_TP_ETCACCT   NCHAR(1),              
	@P_CD_BIZAREA   NVARCHAR(7),              
	@P_NM_BIZAREA NVARCHAR(50),              
	@P_CD_CC    NVARCHAR(12),              
	@P_NM_CC  NVARCHAR(100),              
	@P_CD_PJT    NVARCHAR(20),              
	@P_NM_PJT    NVARCHAR(50),    --LINE : 30              
	          
	@P_CD_DEPT    NVARCHAR(12),              
	@P_NM_DEPT   NVARCHAR(50),              
	@P_CD_EMPLOY   NVARCHAR(10),              
	@P_NM_EMPLOY NVARCHAR(50),              
	@P_CD_PARTNER   NVARCHAR(20),              
	@P_NM_PARTNER NVARCHAR(50),              
	@P_CD_DEPOSIT   NVARCHAR(20),              
	@P_NM_DEPOSIT NVARCHAR(50),              
	@P_CD_CARD   NVARCHAR(20),              
	@P_NM_CARD   NVARCHAR(50),    --LINE : 40              
	          
	@P_CD_BANK    NVARCHAR(20),              
	@P_NM_BANK   NVARCHAR(50),              
	@P_NO_ITEM    NVARCHAR(20),              
	@P_NM_ITEM   NVARCHAR(50),              
	@P_TP_TAX    NVARCHAR(2),              
	@P_NM_TAX    NVARCHAR(50),              
	@P_CD_TRADE   NVARCHAR(2),              
	@P_NM_TRADE   NVARCHAR(50),              
	@P_CD_EXCH   NVARCHAR(3),              
	@P_NM_EXCH   NVARCHAR(50),    --LINE : 50              
	          
	@P_CD_UMNG1   NVARCHAR(20),              
	@P_CD_UMNG2   NVARCHAR(20),              
	@P_CD_UMNG3   NVARCHAR(20),              
	@P_CD_UMNG4   NVARCHAR(20),              
	@P_CD_UMNG5   NVARCHAR(20),              
	@P_NO_COMPANY NVARCHAR(20),              
	@P_AM_SUPPLY NUMERIC(19,4),   --과세표준              
	@P_CD_MNG    NVARCHAR(50),              
	@P_DT_START   NCHAR(8),              
	@P_DT_END    NCHAR(8),         --LINE : 60              
	          
	@P_RT_EXCH    NUMERIC(15,4),              
	@P_AM_EXDO   NUMERIC(19,4),     -- 외화금액              
	@P_NO_MODULE   NVARCHAR(3),              
	@P_NO_MDOCU   NVARCHAR(20),    -- 인사모듈에서 들어올때 'HR...'              
	@P_CD_EPNOTE   NVARCHAR(10),              
	@P_ID_INSERT   NVARCHAR(15),              
	@P_CD_BGACCT   NVARCHAR(10),              
	@P_TP_EPNOTE   CHAR(1),              
	@P_NM_PUMM   NVARCHAR(100),              
	@P_DT_WRITE   NCHAR(8),       --LINE : 70              
	          
	@P_AM_ACTSUM   NUMERIC(19,4) = 0,              
	@P_AM_JSUM    NUMERIC(19,4) = 0,              
	@P_YN_GWARE   NCHAR(1) = 'N',              
	@P_CD_BIZPLAN   NVARCHAR(20) = NULL,              
	@P_CD_ETC  NVARCHAR(6) = NULL,  -- 기타소득자코드              
	@P_ERRORCODE INT OUTPUT,   -- ERROR코드( 0 : 정상, 1: 오류)              
	@P_ERRORMSG NVARCHAR(300) OUTPUT, -- ERROR메세지 20090406              
	@P_NO_BL   NVARCHAR(20) = NULL,  -- BL번호 (전방추가)              
	@P_NO_LC   NVARCHAR(20) = NULL,     -- LC번호 (전방추가)              
	@P_FG_BILL NVARCHAR(3) = NULL,  -- 결재방법 (전방추가)    --LINE : 80      
	@P_NM_ARRIVE NVARCHAR(30) = NULL, -- 미착품구분 (전방추가)     
	@P_CD_PAYDEDUCT  NVARCHAR(3) = NULL,    --인사추가 (YTN)    
	@P_JUMIN_NO  NVARCHAR(40) = NULL,    --EC 추가 (본죽)    
	@P_DT_PAY   VARCHAR(8) = NULL,  --인사추가 (YTN)    
	@P_NO_INV  NVARCHAR(20) = NULL,    -- INVOICE NO    
	@P_NO_TO   NVARCHAR(20) = NULL,     -- 면장번호(NO_TO)    
	@P_VAT_TAX	NUMERIC(17,4) = 0,		--부가세 (전방추가)
	@P_PO_CONDITION     NVARCHAR(4) = NULL, -- 구매거래조건(삼보)
	@P_NM_CONDITION	NVARCHAR(60) = NULL,
	@P_NO_PO			NVARCHAR(20) = NULL, --LINE : 90
	@P_FG_JATA		NVARCHAR(3) = NULL,  -- 수금 자타구분 20131218 김현정
	@P_NM_FG_JATA		NVARCHAR(200) = NULL,  -- 수금 자타구분 20131218 김현정
	@P_NM_ISSUE		NVARCHAR(20) = NULL,   -- 수금 발행인 20131218 김현정
	@P_LN_PARTNER2  NVARCHAR(50) = NULL,
	@P_UM			NUMERIC(17,4) = 0, -- 유니포인트 전용 추가
	@P_QT			NUMERIC(17,4) = 0, -- 유니포인트 전용 추가
	@P_USER		    NVARCHAR(100) = NULL, -- 업체전용
	@P_NUM_USER		NUMERIC(17,4) = 0,  -- 업체전용
	@P_USER2		NVARCHAR(200) = NULL,
	@P_TP_EVIDENCE	NVARCHAR(4) = NULL,    -- 증빙(주홍열추가 2016.10.26)D20160929039  
	@P_YN_ISS       NVARCHAR(1) = NULL    -- 계산서발행형태
)              
AS              
/*******************************************              
**  SYSTEM :              
**  SUB SYSTEM :              
**  PAGE :              
**  DESC : JB (전방) 에서 사용하는 프로시저              
**              
**  RETURN VALUES              
**              
**  관    리    자 (정) : 전방 개발자    
**  관    리    자 (부) : 회계 개발자    
**              
**  수    정    자 : 장경민              
**  수 정 내 용 : CC --> 프로시저변경              
*********************************************              
** CHANGE HISTORY              
2012.07.03 : D20120625049 : YTN 전용 130 기타공제1 - 023.기타공제  
2012.07.03 : RAISERROR 수정  
2013.04.16 : D20130416001 : 삼보컴퓨터 추가(재반영함.)
2013.06.20 : D20130417060 
*********************************************/              
DECLARE @T_CD_MNG1   NCHAR(3)              
DECLARE @T_CD_MNGD1   NVARCHAR(20)              
DECLARE @T_NM_MNGD1   NVARCHAR(100)              
DECLARE @T_CD_MNG2   NCHAR(3)              
DECLARE @T_CD_MNGD2   NVARCHAR(20)              
DECLARE @T_NM_MNGD2   NVARCHAR(100)              
DECLARE @T_CD_MNG3   NCHAR(3)              
DECLARE @T_CD_MNGD3   NVARCHAR(20)              
DECLARE @T_NM_MNGD3   NVARCHAR(100)              
DECLARE @T_CD_MNG4   NCHAR(3)              
DECLARE @T_CD_MNGD4   NVARCHAR(20)              
DECLARE @T_NM_MNGD4   NVARCHAR(100)              
DECLARE @T_CD_MNG5   NCHAR(3)              
DECLARE @T_CD_MNGD5   NVARCHAR(20)              
DECLARE @T_NM_MNGD5   NVARCHAR(100)              
DECLARE @T_CD_MNG6   NCHAR(3)              
DECLARE @T_CD_MNGD6   NVARCHAR(20)              
DECLARE @T_NM_MNGD6   NVARCHAR(100)              
DECLARE @T_CD_MNG7   NCHAR(3)              
DECLARE @T_CD_MNGD7   NVARCHAR(20)              
DECLARE @T_NM_MNGD7   NVARCHAR(100)              
DECLARE @T_CD_MNG8   NCHAR(3)              
DECLARE @T_CD_MNGD8   NVARCHAR(20)              
DECLARE @T_NM_MNGD8   NVARCHAR(100)              
DECLARE @T_ST_MNG1  NCHAR(1)              
DECLARE @T_ST_MNG2  NCHAR(1)              
DECLARE @T_ST_MNG3  NCHAR(1)              
DECLARE @T_ST_MNG4  NCHAR(1)              
DECLARE @T_ST_MNG5  NCHAR(1)              
DECLARE @T_ST_MNG6  NCHAR(1)              
DECLARE @T_ST_MNG7  NCHAR(1)              
DECLARE @T_ST_MNG8  NCHAR(1)              
              
DECLARE @CNT INT              
DECLARE @CD  NVARCHAR(20)              
DECLARE @CDD NVARCHAR(20)              
DECLARE @NMD NVARCHAR(50)              
              
DECLARE @P_CD_ACTYPE NCHAR(2)              
DECLARE @P_CD_CRFUND NCHAR(4)              
DECLARE @P_CD_DRFUND NCHAR(4)              
DECLARE @P_AM_AMT   NUMERIC(19,4)              
DECLARE @P_CD_RELATION1 NCHAR(2)              
DECLARE @P_YN_FUNDPLAN NCHAR(1)              
              
DECLARE @P_MSG     NVARCHAR(300)              
DECLARE @P_MNG_CHECK  NCHAR(1)              
-- ▶ 전표마감 체크 추가 (2009-06-05)              
DECLARE @P_CD_ENV  NVARCHAR(3)              
-- SERVER KEY (업체전용)              
DECLARE @V_SERVER_KEY NVARCHAR(50)              
DECLARE @P_CD_EXC  NVARCHAR(3)              
  
SELECT @V_SERVER_KEY = MAX(SERVER_KEY) FROM CM_SERVER_CONFIG WHERE YN_UPGRADE = 'Y'             
              
-- 환경설정에 따라 마감체크가 다르다.              
SELECT @P_CD_ENV = CD_ENV FROM MA_ENVD WHERE CD_COMPANY = @P_CD_COMPANY AND TP_ENV = 'TP_DDCLOSE'              
      
--- 시스템통제설정  
SELECT @P_CD_EXC = CD_EXC FROM MA_EXC WHERE CD_COMPANY = @P_CD_COMPANY AND EXC_TITLE = '업체별프로세스'     
      
              
IF(ISNULL(@P_CD_ENV,'') = '')              
 SET @P_CD_ENV = '1'              
              
-- 회사단위 마감      
IF(@P_CD_ENV = '1')              
BEGIN              
 IF((SELECT COUNT(*) FROM FI_DDCLOSE WHERE DT_CLOSE = @P_DT_ACCT AND CD_COMPANY = @P_CD_COMPANY AND TP_CLOSE <> '0') > 0)              
 BEGIN              
  --RAISERROR 100000 'KR##전표가 마감되어 자동전표 처리를 할 수 없습니다.##US##The slip will be closed and there is not a possibility which slip will control automatically.##'              
  RAISERROR ('KR##전표가 마감되어 자동전표 처리를 할 수 없습니다.##US##The slip will be closed and there is not a possibility which slip will control automatically.##', 18, 1)  
  SET @P_ERRORCODE = 1              
  SET @P_ERRORMSG = @P_MSG              
  ROLLBACK  TRANSACTION              
  RETURN              
 END              
END              
-- 회계단위 마감      
ELSE              
BEGIN              
 IF((SELECT COUNT(*) FROM FI_DDCLOSE2 WHERE DT_CLOSE = @P_DT_ACCT AND CD_COMPANY = @P_CD_COMPANY AND CD_PC = @P_CD_PC AND TP_CLOSE <> '0') > 0)      
 BEGIN              
  --RAISERROR 100000 'KR##전표가 마감되어 자동전표 처리를 할 수 없습니다.(회계단위마감)##US##The slip will be closed and there is not a possibility which slip will control automatically.##'      
  RAISERROR ( 'KR##전표가 마감되어 자동전표 처리를 할 수 없습니다.(회계단위마감)##US##The slip will be closed and there is not a possibility which slip will control automatically.##', 18, 1)      
  SET @P_ERRORCODE = 1              
  SET @P_ERRORMSG = @P_MSG              
  ROLLBACK  TRANSACTION              
  RETURN              
 END          
END      
      
-- 전표유형별 마감      
IF((SELECT ISNULL(COUNT(*),0)      
 FROM FI_TYPECLOSE      
 WHERE CD_COMPANY = @P_CD_COMPANY      
 AND CD_DOCU = @P_CD_DOCU      
 AND DT_CLOSE = (CASE TP_DATE  WHEN '1' THEN @P_DT_ACCT      
          WHEN '2' THEN @P_DT_WRITE      
          ELSE 'FALSE'      
       END)                   
 AND TP_CLOSE <> '0') > 0)      
BEGIN              
 --RAISERROR 100000 'KR##전표가 마감되어 자동전표 처리를 할 수 없습니다.(유형마감)##US##The slip will be closed and there is not a possibility which slip will control automatically.##'      
 RAISERROR ('KR##전표가 마감되어 자동전표 처리를 할 수 없습니다.(유형마감)##US##The slip will be closed and there is not a possibility which slip will control automatically.##', 18, 1)  
 SET @P_ERRORCODE = 1              
 SET @P_ERRORMSG = @P_MSG              
 ROLLBACK  TRANSACTION              
 RETURN              
END              
      
-- 작성부서별 마감      
IF((SELECT ISNULL(COUNT(*),0)      
 FROM FI_WDEPTCLOSE      
 WHERE CD_COMPANY = @P_CD_COMPANY      
 AND CD_WDEPT = @P_CD_WDEPT      
 AND DT_CLOSE = (CASE TP_DATE WHEN '1' THEN @P_DT_ACCT      
         WHEN '2' THEN @P_DT_WRITE      
         ELSE 'FALSE'      
     END)                  
AND TP_CLOSE <> '0') > 0)      
BEGIN              
 --RAISERROR 100000 'KR##전표가 마감되어 자동전표 처리를 할 수 없습니다.(부서마감)##US##The slip will be closed and there is not a possibility which slip will control automatically.##'      
 RAISERROR ('KR##전표가 마감되어 자동전표 처리를 할 수 없습니다.(부서마감)##US##The slip will be closed and there is not a possibility which slip will control automatically.##', 18, 1)      
 SET @P_ERRORCODE = 1              
 SET @P_ERRORMSG = @P_MSG              
 ROLLBACK  TRANSACTION              
 RETURN              
END              
              
/* 관리항목  목록표              
A02 코스트센타              
A03 부서              
A04 사원              
A05 프로젝트              
A06 거래처              
A07 예적금계좌              
A08 신용카드              
A09 금융기관              
A10 품목              
A21 사업구분              
A21 사용자정의1              
A21 영업구분              
A22 사용자정의2              
A22 사원2              
A23 사용자정의3              
A24 사용자정의4              
A25 사용자정의5              
A45 테스트              
A45 SDF              
B01 자산관리번호              
B02 받을어음NO              
B03 지급어음NO              
B04 차입금관리번호              
B05 유가증권관리번호  
B06 L/C NO             
B07 미착품구분              
B11 자산처리구분              
B12 받을어음정리구분              
B13 지급어음정리구분              
B21 발생일자              
B22 자금예정일자              
B23 만기일자              
B24 환종              
B25 환율              
B26 외화금액              
B27 타계정대체              
C01 사업자등록번호              
C02 기간              
C03 관련계정              
C04 문서번호              
C04 수량              
C05 과세표준액              
C06 할인율              
C07 배서처              
C08 발행일              
C09 발행인              
C10 보관장소              
C11 배서인              
C12 수금일자              
C13 자타수구분          
C14 세무구분              
C15 거래처계좌번호              
C15 송금계좌정보              
C16 세액              
C17 예금주              
C18 불공제구분              
C19  기타소득자       
C22 결재조건            
D01 액면가액              
D02 표시이자율              
D03 단가              
D06 모델              
D07 규격              
D08 이율              
D09 국가              
D10 전화번호              
D11 INVOICE NO              
D12 B/L NO              
D13 증서번호              
D14 리스계약번호              
D15 가입일자              
D16 계약금액              
D17 만기금액              
D18 고정자산과표              
D19 작업지시번호              
D20 생산계획번호              
D21 작업장              
D22 성명              
D23 지급지              
D24 일수              
D25 면장번호              
D26 반출번호              
D27 대금지불조건              
D28 NEGO일자              
D29 면허일자      D30 본지점관리번호              
D31 매출지              
D31 사원              
D32 매출              
D33 김대중센터 관리단위              
D35 적요              
D50 XXX              
D51 KBSI관리단위              
*/              
              
SET @P_MNG_CHECK = 'Y'              
SET @P_AM_AMT = 0              
              
-- 자료 유효성 검사 추가 (2007-07-10)              
-- 거래처              
IF(ISNULL(@P_CD_PARTNER,'') <> '')              
BEGIN              
 IF ((SELECT COUNT(*) FROM MA_PARTNER WHERE CD_PARTNER = @P_CD_PARTNER AND CD_COMPANY = @P_CD_COMPANY) = 0)              
 BEGIN              
  SET @P_MSG = 'KR##거래처 마스터에 없는 거래처가 입력되었습니다.(거래처코드 : ' + @P_CD_PARTNER + ')##US##Customer Code does not exist.##'              
  RAISERROR (@P_MSG, 18, 1)  
  SET @P_ERRORCODE = 1              
  SET @P_ERRORMSG = @P_MSG              
  ROLLBACK  TRANSACTION              
  RETURN              
 END              
END              
-- 관리항목부서              
IF(ISNULL(@P_CD_DEPT,'') <> '')              
BEGIN              
 IF ((SELECT COUNT(*) FROM MA_DEPT WHERE CD_DEPT = @P_CD_DEPT AND CD_COMPANY = @P_CD_COMPANY) = 0)              
 BEGIN              
  SET @P_MSG = 'KR##부서 마스터에 없는 관리항목 부서가 입력되었습니다.(부서코드 : ' + @P_CD_DEPT + ')##US##Dept. Code does not exist.##'              
  RAISERROR (@P_MSG, 18, 1)  
  SET @P_ERRORCODE = 1              
  SET @P_ERRORMSG = @P_MSG              
  ROLLBACK  TRANSACTION              
  RETURN              
 END              
END              
-- 작성부서              
IF(ISNULL(@P_CD_WDEPT,'') <> '')              
BEGIN              
 IF ((SELECT COUNT(*) FROM MA_DEPT WHERE CD_DEPT = @P_CD_WDEPT AND CD_COMPANY = @P_CD_COMPANY) = 0)              
 BEGIN              
  SET @P_MSG = 'KR##부서 마스터에 없는 작성부서가 입력되었습니다.(부서코드 : ' + @P_CD_WDEPT + ')##US##Dept. Code does not exist.##'              
  RAISERROR (@P_MSG, 18, 1)  
  SET @P_ERRORCODE = 1              
  SET @P_ERRORMSG = @P_MSG              
  ROLLBACK  TRANSACTION              
  RETURN              
 END              
END              
-- 작성사원              
IF(ISNULL(@P_ID_WRITE,'') <> '')              
BEGIN              
 IF ((SELECT COUNT(*) FROM MA_EMP WHERE NO_EMP = @P_ID_WRITE AND CD_COMPANY = @P_CD_COMPANY) = 0)              
 BEGIN              
  SET @P_MSG = 'KR##사원 마스터에 없는 작성사원이 입력되었습니다.(사원코드 : ' + @P_ID_WRITE + ')##US##Employee does not exist.##'              
  RAISERROR (@P_MSG, 18, 1)  
  SET @P_ERRORCODE = 1              
  SET @P_ERRORMSG = @P_MSG       
  ROLLBACK  TRANSACTION              
  RETURN              
 END              
END              
-- 관리항목 사원              
IF(ISNULL(@P_CD_EMPLOY,'') <> '')              
BEGIN              
 IF ((SELECT COUNT(*) FROM MA_EMP WHERE NO_EMP = @P_CD_EMPLOY AND CD_COMPANY = @P_CD_COMPANY) = 0)              
 BEGIN              
  SET @P_MSG = 'KR##사원 마스터에 없는 관리항목 사원이 입력되었습니다.(사원코드 : ' + @P_CD_EMPLOY + ')##US##Employee does not exist.##'              
  RAISERROR (@P_MSG, 18, 1)  
  SET @P_ERRORCODE = 1              
  SET @P_ERRORMSG = @P_MSG              
  ROLLBACK  TRANSACTION              
  RETURN              
 END              
END              
-- 관리항목 C/C              
IF(ISNULL(@P_CD_CC,'') <> '')              
BEGIN              
 IF ((SELECT COUNT(*) FROM MA_CC WHERE CD_CC = @P_CD_CC AND CD_COMPANY = @P_CD_COMPANY) = 0)              
 BEGIN              
  SET @P_MSG = 'KR##C/C 마스터에 없는 C/C가 입력되었습니다.(C/C코드 : ' + @P_CD_CC + ')##US##C/C does not exsit.##'              
  RAISERROR (@P_MSG, 18, 1)  
  SET @P_ERRORCODE = 1              
  SET @P_ERRORMSG = @P_MSG              
  ROLLBACK  TRANSACTION              
  RETURN              
 END              
END              
-- 계정코드              
IF NOT EXISTS (SELECT CD_ACCT FROM FI_ACCTCODE WHERE CD_ACCT = @P_CD_ACCT AND CD_COMPANY = @P_CD_COMPANY)              
BEGIN              
 SET @P_MSG = 'KR##' + ISNULL(@P_CD_ACCT, 'NULL') + ' 은(는) 존재하지 않는 계정입니다.##US##Acct. Code does not exist.##'              
 RAISERROR (@P_MSG, 18, 1)  
 SET @P_ERRORCODE = 1              
 SET @P_ERRORMSG = @P_MSG              
 ROLLBACK  TRANSACTION              
 RETURN              
END              
              
SELECT @T_CD_MNG1 = CD_MNG1,              
    @T_CD_MNG2 = CD_MNG2,              
    @T_CD_MNG3 = CD_MNG3,              
    @T_CD_MNG4 = CD_MNG4,              
    @T_CD_MNG5 = CD_MNG5,              
    @T_CD_MNG6 = CD_MNG6,              
    @T_CD_MNG7 = CD_MNG7,              
    @T_CD_MNG8 = CD_MNG8,              
    @T_ST_MNG1 = ST_MNG1,              
    @T_ST_MNG2 = ST_MNG2,              
    @T_ST_MNG3 = ST_MNG3,              
    @T_ST_MNG4 = ST_MNG4,              
    @T_ST_MNG5 = ST_MNG5,              
    @T_ST_MNG6 = ST_MNG6,              
    @T_ST_MNG7 = ST_MNG7,              
    @T_ST_MNG8 = ST_MNG8,              
    @P_CD_ACTYPE = CD_ACTYPE, @P_CD_CRFUND = CD_CRFUND, @P_CD_DRFUND = CD_DRFUND,              
    @P_CD_RELATION1 = CD_RELATION, @P_YN_FUNDPLAN = YN_FUNDPLAN              
FROM      FI_ACCTCODE              
WHERE  CD_ACCT = @P_CD_ACCT              
AND   CD_COMPANY = @P_CD_COMPANY              
              
SET @CNT = 0              
              
WHILE(@CNT < 8)              
BEGIN              
 SET @CNT = @CNT + 1              
              
 IF @CNT = 1           
  SET @CD = @T_CD_MNG1              
 ELSE IF @CNT = 2              
  SET @CD = @T_CD_MNG2              
 ELSE IF @CNT = 3              
  SET @CD = @T_CD_MNG3              
 ELSE IF @CNT = 4              
  SET @CD = @T_CD_MNG4              
 ELSE IF @CNT = 5              
  SET @CD = @T_CD_MNG5              
 ELSE IF @CNT = 6              
  SET @CD = @T_CD_MNG6              
 ELSE IF @CNT = 7              
  SET @CD = @T_CD_MNG7              
 ELSE IF @CNT = 8              
  SET @CD = @T_CD_MNG8              
              
 IF @CD IS NULL OR @CD = ''              
  CONTINUE              
              
 SET @CDD = NULL              
 SET @NMD = NULL              
              
 IF @CD = 'A01'              
 BEGIN              
   SET @CDD = @P_CD_BIZAREA              
   SET @NMD = @P_NM_BIZAREA              
 END              
 ELSE IF @CD = 'A02'              
 BEGIN              
   SET @CDD = @P_CD_CC              
   SET @NMD = @P_NM_CC              
 END              
 ELSE IF @CD = 'A03'              
 BEGIN              
   SET @CDD = @P_CD_DEPT   
   SET @NMD = @P_NM_DEPT 
 END              
 ELSE IF @CD = 'A04'              
 BEGIN              
   SET @CDD = @P_CD_EMPLOY              
   SET @NMD = @P_NM_EMPLOY              
 END              
 ELSE IF @CD = 'A05'           
 BEGIN              
   SET @CDD = @P_CD_PJT              
   SET @NMD = @P_NM_PJT              
 END              
 ELSE IF @CD = 'A06'              
 BEGIN              
   SET @CDD = @P_CD_PARTNER              
   SET @NMD = @P_NM_PARTNER              
 END              
 ELSE IF @CD = 'A07'              
 BEGIN              
   SET @CDD = @P_CD_DEPOSIT              
   SET @NMD = @P_NM_DEPOSIT              
 END              
 ELSE IF @CD = 'A08'              
 BEGIN              
   SET @CDD = @P_CD_CARD              
   SET @NMD = @P_NM_CARD              
 END              
 ELSE IF @CD = 'A09'              
 BEGIN              
   SET @CDD = @P_CD_BANK              
   SET @NMD = @P_NM_BANK              
 END              
 ELSE IF @CD = 'A10'              
 BEGIN              
   SET @CDD = @P_NO_ITEM              
   SET @NMD = @P_NM_ITEM              
 END              
 ELSE IF @CD = 'B11' OR @CD = 'B12' OR @CD = 'B13' OR @CD = 'B51'              
 BEGIN              
   SET @CDD = @P_CD_TRADE              
   SET @NMD = @P_NM_TRADE              
 END              
 ELSE IF @CD = 'C14'              
 BEGIN              
   SET @CDD = @P_TP_TAX              
   SET @NMD = @P_NM_TAX              
 END 
 ELSE IF @CD = 'C09'   -- 발행인   20131218 김현정        
 BEGIN              
   SET @CDD = @P_NM_ISSUE            
   SET @NMD = @P_NM_ISSUE              
 END   
 ELSE IF @CD = 'C13'   -- 자타구분   20131218 김현정 
 BEGIN              
   SET @CDD = @P_FG_JATA
   SET @NMD = @P_NM_FG_JATA             
 END   
 ---  
 ELSE IF @CD = 'C15'              
  BEGIN              
                 
   SELECT @NMD = M.NM_SYSDEF + '/' + P.NO_DEPOSIT + '/' + ISNULL(P.NM_DEPOSIT,''),
		  @CDD = D.CD_DEPOSITNO 
    FROM MA_PARTNER P  
    INNER JOIN MA_CODEDTL M ON M.CD_SYSDEF = P.CD_BANK AND M.CD_COMPANY = P.CD_COMPANY AND M.CD_FIELD = 'MA_B000043' 
    LEFT JOIN MA_PARTNER_DEPOSIT D ON P.NO_DEPOSIT = D.NO_DEPOSIT AND P.CD_PARTNER = D.CD_PARTNER AND P.CD_COMPANY = D.CD_COMPANY  
    WHERE P.CD_COMPANY = @P_CD_COMPANY AND P.CD_PARTNER = @P_CD_PARTNER  
     
   -- 2013.02.26 : D20130118019 : 인사 - 전표처리시 관리항목 연동 (삼성리얼에스테이트)
   IF(@V_SERVER_KEY = 'SSRA')
   BEGIN
		IF EXISTS ( SELECT	* 
					FROM	HR_SLBASIS
					WHERE	CD_COMPANY = @P_CD_COMPANY
					AND		CD_ACCT = @P_CD_ACCT
					AND		TP_TR = '001'
					AND		CD_ACLIM = '004'
					AND		CD_MNG = 'C15' )
		BEGIN
			SELECT	@NMD = (SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'HR_P000006' AND CD_SYSDEF = EMP.CD_BANK1 AND CD_COMPANY = @P_CD_COMPANY) + ' / ' + EMP.NO_BANK1
			FROM	MA_EMP EMP
			WHERE	CD_COMPANY = @P_CD_COMPANY
			AND		NO_EMP = @P_CD_EMPLOY
		END
		
   END  
   
   IF(@V_SERVER_KEY LIKE 'DOMINO%') 
   BEGIN
	DECLARE @V_CD_DEPOSITNO	NVARCHAR(5)
	SELECT @V_CD_DEPOSITNO = D.CD_DEPOSITNO 
	  FROM MA_PARTNER P INNER JOIN
	       MA_PARTNER_DEPOSIT D ON D.CD_COMPANY = P.CD_COMPANY AND P.CD_PARTNER = D.CD_PARTNER AND P.NO_DEPOSIT = D.NO_DEPOSIT 
	 WHERE P.CD_COMPANY = @P_CD_COMPANY AND P.CD_PARTNER = @P_CD_PARTNER
	 
	 SET @CDD = @V_CD_DEPOSITNO
	 
   END    
     
  END  
 ---             
 ELSE IF @CD = 'B01' OR @CD = 'B02' OR @CD = 'B03' OR @CD = 'B04' OR @CD = 'B05' OR @CD = 'B06' OR @CD = 'B07' OR @CD = 'B50'              
 BEGIN              
    IF @CD = 'B06' --AND ISNULL(@P_CD_MNG ,'') = '' --전방수입미착정산전표에서 LC번호인자를 별도로 받음 20090507              
    BEGIN              
       SET @CDD = @P_NO_LC              
       SET @NMD = @P_NO_LC              
    END              
    ELSE IF @CD = 'B07' --AND ISNULL(@P_CD_MNG ,'') = ''     --미착구분 2010.02.02              
    BEGIN          
        
       SET @CDD = @P_NM_ARRIVE              
       SET @NMD = @P_NM_ARRIVE              
    END    
    ELSE IF @CD = 'B50'     --전자어음구분 2010.02.02              
    BEGIN              
       SET @CDD = SUBSTRING(@P_CD_MNG, 0, CHARINDEX('|', @P_CD_MNG))     --채번된 번호       
       SET @NMD = SUBSTRING(@P_CD_MNG,CHARINDEX('|', @P_CD_MNG) + 1,LEN(@P_CD_MNG))    --관리번호     
       SET @P_CD_MNG = @CDD /* 2013.01.25 김현정 전자어음일경우 FI_DOCU-CD_MNG 에는 전자어음의 채번된번호가 들어가야함. */
    END              
    ELSE              
    BEGIN              
       SET @CDD = @P_CD_MNG              
       SET @NMD = @P_CD_MNG              
    END              
 END              
 --ELSE IF @CD = 'B21' OR @CD = 'B22' OR @CD = 'C02'              
 --BEGIN              
 --    SET @NMD = @P_DT_START              
 --END              
 ELSE IF @CD = 'B21' OR @CD = 'C02'                
	BEGIN 
		IF @V_SERVER_KEY LIKE 'DWFISH%'
		BEGIN
			SET @NMD = @P_USER
		END
		ELSE                  
			SET @NMD = @P_DT_ACCT                
	END       
 ELSE IF @CD = 'B22'    
 BEGIN                
	IF @V_SERVER_KEY IN ('YTN','YTN1')      
	BEGIN      
		SET @NMD = @P_DT_PAY      
		SET @CD = @P_DT_PAY      
	END  
	ELSE      
		SET @NMD = @P_DT_START                
 END
 ELSE IF @CD = 'B22'  
 BEGIN              
  IF @V_SERVER_KEY IN ('KORAVL')    
  BEGIN    
	  SET @NMD = @P_DT_PAY    
	  SET @CD = @P_DT_PAY    
  END   
  ELSE    
	SET @NMD = @P_DT_START              
 END  
 ELSE IF @CD = 'B23'              
 BEGIN              
   SET @NMD = @P_DT_END              
 END              
 ELSE IF @CD = 'B24'              
   
 BEGIN              
   SET @CDD = @P_CD_EXCH              
   SET @NMD = @P_NM_EXCH              
 END              
 ELSE IF @CD = 'B25'              
 BEGIN              
   --SET @NMD = CONVERT(VARCHAR(20), ISNULL(@P_RT_EXCH,0))
   SET @NMD = CONVERT(VARCHAR, CONVERT(MONEY, ISNULL(@P_RT_EXCH, 0)), 1) -- 2017.01.06 수정 김기현 (금액에 천단위 기호가 안들어감)             
 END              
 ELSE IF @CD = 'B26' -- 외화금액              
 BEGIN              
	---- 다이킨의 경우 대변이면서 구매매입, 외주매입시 외화금액 관리항목에 합계금액을 넣어줌
	--IF (@V_SERVER_KEY IN ('DAIKIN') AND @P_TP_DRCR = '2' AND @P_NO_MODULE IN ('210', '400'))
	--	SET @NMD = CONVERT(VARCHAR(20), ISNULL(@P_AM_CR,0))    
	--ELSE 
		--SET @NMD = CONVERT(VARCHAR(20), ISNULL(@P_AM_EXDO,0))
	SET @NMD = CONVERT(VARCHAR, CONVERT(MONEY, ISNULL(@P_AM_EXDO, 0)), 1) -- 2017.01.06 수정 김기현 (금액에 천단위 기호가 안들어감)	          
 END              
 ELSE IF @CD = 'B27' -- 타계정대체                              
	BEGIN  
		IF @P_CD_DOCU = '12'
		BEGIN   
			SELECT @CDD = B.CD_SYSDEF, 
				   @NMD = B.NM_SYSDEF,
				   @P_TP_ETCACCT = B.CD_SYSDEF
			FROM FI_ACCTCODE A INNER JOIN MA_CODEDTL B ON B.CD_COMPANY = A.CD_COMPANY AND B.CD_FIELD = 'FI_J000005'
			WHERE A.CD_COMPANY = @P_CD_COMPANY
			AND   A.CD_ACCT    = @P_CD_ACCT
			AND ((A.TP_DRCR    = @P_TP_DRCR AND B.CD_SYSDEF = '2') OR
				 (A.TP_DRCR   != @P_TP_DRCR AND B.CD_SYSDEF = '1')) 
		END		            
	END  
 ELSE IF @CD = 'C01' --사업자등록번호              
 BEGIN              
   SET @CDD = @P_NO_COMPANY              
   SET @NMD = @P_NO_COMPANY              
 END              
 ELSE IF @CD = 'C05'              
 BEGIN              
   SET @NMD = REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,@P_AM_SUPPLY),1),'.00','')          
 END   
 ELSE IF @CD = 'C16'              
 BEGIN              
   --SET @NMD = CONVERT(VARCHAR(20), ISNULL(@P_VAT_TAX,0))
   SET @NMD = CONVERT(VARCHAR, CONVERT(MONEY, ISNULL(@P_VAT_TAX, 0)), 1) -- 2017.01.06 수정 김기현 (금액에 천단위 기호가 안들어감)     
 END             
 ELSE IF @CD = 'C19'              
 BEGIN              
  SELECT @NMD = NM_ETC              
  FROM HR_WTETC              
  WHERE CD_COMPANY = @P_CD_COMPANY AND CD_ETC = @P_CD_ETC              
 END              
  --대림전용              
 ELSE IF @CD = 'D35'          
 BEGIN              
   IF (@V_SERVER_KEY = 'DEM' OR @V_SERVER_KEY = 'DEM2')  
   BEGIN
    SET @CDD = '100'              
    SET @NMD = '영업수지'              
   END
   ELSE IF (@V_SERVER_KEY = 'DWFISH' )
   BEGIN
    --SET @CDD = @P_NUM_USER     
    SET @NMD = @P_NUM_USER       
   END  
	IF @V_SERVER_KEY IN ('HAATZ')            
	BEGIN
		SET @CDD = @P_NO_TO
	END
	
 END              
 
 --대림, SK 전용              
 -- 2010.04.07 SMR            
 -- 결재조건(C22), 대금지불조건(D27) 회계패키지에서             
 -- 사용되고있는 것을 사용한다.            
 -- 기존업체 대림과 SKD&D에서는 기존데로 사용            
 ELSE IF (@CD = 'C22') OR (@CD = 'D27') OR (@CD = 'A21' AND (@V_SERVER_KEY = 'DEM' OR @V_SERVER_KEY = 'DEM2' OR @V_SERVER_KEY = 'SKDND2') )            
 BEGIN              
   SET @CDD = @P_FG_BILL              
                 
   SELECT @NMD = NM_SYSDEF              
   FROM MA_CODEDTL              
   WHERE CD_COMPANY = @P_CD_COMPANY              
   AND  ((@CD = 'C22' AND CD_FIELD = 'SA_B000002') OR @CD = 'D27'AND CD_FIELD = 'PU_C000044')      
   AND  CD_SYSDEF = @CDD                         
 END              
   --아이리버 전용                        
 -- 2011.07.28 SMR                      
-- 지급조건(C28) 사용한다.                      
            
 ELSE IF (@CD = 'C28' )                     
 BEGIN                           
   SET @CDD = @P_FG_BILL                        
              
  IF (@V_SERVER_KEY = 'IRIVER2' OR   
      (SELECT ISNULL(CD_EXC,'N')    
              FROM MA_EXC     
             WHERE CD_COMPANY = @P_CD_COMPANY     
               AND EXC_TITLE  = '거래처정보등록-회계지급관리사용여부'     
               AND CD_MODULE  = 'MA') = 'N')    
   BEGIN    
  SELECT @NMD = NM_SYSDEF                        
  FROM MA_CODEDTL                        
  WHERE CD_COMPANY = @P_CD_COMPANY                        
  AND  (@CD = 'C28' AND CD_FIELD = 'PU_C000044')   
  AND  CD_SYSDEF = @CDD                        
   END    
  ELSE         
   BEGIN    
    SELECT @NMD = NM_PAYMENT        
    FROM FI_PAYMENT         
    WHERE CD_COMPANY = @P_CD_COMPANY        
    AND CD_PAYMENT = @CDD        
   END         
  END 
  
  --유니포인트, KPIC 
  ELSE IF (@CD = 'C04')
  BEGIN
	IF(@V_SERVER_KEY LIKE 'UNIPOINT%') OR (@V_SERVER_KEY LIKE 'KPCI%') OR (@V_SERVER_KEY LIKE 'DZSQL%') OR (@V_SERVER_KEY LIKE 'DWFISH%')
	BEGIN
	 SELECT @CDD = @P_QT
	 SELECT @NMD = @P_QT
	END
  END                     
  ELSE IF (@CD = 'C38' AND @P_NO_MODULE = '210')
  BEGIN
	IF(@P_YN_ISS = 'Y') --- 계산서발행형태 전자 : 2, 종이 : 0
		SET @P_YN_ISS = '2'
	ELSE IF(@P_YN_ISS = 'N')
		SET @P_YN_ISS = '0'
	ELSE IF(@P_YN_ISS = '3')
		SET @P_YN_ISS = '2'
		
	BEGIN
	 SELECT @CDD = @P_YN_ISS
	 SELECT @NMD = NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'FI_T000030' AND CD_SYSDEF = @CDD
	END
  END 
  ELSE IF (@CD = 'C39' AND @P_NO_MODULE = '210')
  BEGIN
	BEGIN
	 SELECT @CDD = '0'
	 SELECT @NMD = NM_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'FI_T000036' AND CD_SYSDEF = @CDD
	END
  END 
 -- A21 ∼ A25 까지는 사용자정의 집계성 관리항목  
 --2012.07.03 : D20120625049 : 130 기타공제1 - 023.기타공제  
 ELSE IF(@CD = 'A21' OR @CD = 'A22' OR @CD = 'A23' OR @CD = 'A24' OR @CD = 'A25')              
 BEGIN              
 IF(@CD = 'A21')     
  IF @V_SERVER_KEY IN ('YTN','YTN1')    
  BEGIN    
  SET @CDD = CASE WHEN ISNULL(@P_CD_PAYDEDUCT,'') = '100' THEN '016'     
          WHEN ISNULL(@P_CD_PAYDEDUCT,'') = '110' THEN '017'    
          WHEN ISNULL(@P_CD_PAYDEDUCT,'') = '120' THEN '018'    
          WHEN ISNULL(@P_CD_PAYDEDUCT,'') = 'STX' THEN '001'    
          WHEN ISNULL(@P_CD_PAYDEDUCT,'') = '001' THEN '001'              
          WHEN ISNULL(@P_CD_PAYDEDUCT,'') = 'JTX' THEN '002'    
          WHEN ISNULL(@P_CD_PAYDEDUCT,'') = '002' THEN '002'    
          WHEN ISNULL(@P_CD_PAYDEDUCT,'') = '130' THEN CASE WHEN @P_CD_COMPANY = 'YTN' THEN '023' ELSE '099' END  
  WHEN ( @P_CD_COMPANY = 'YTN' AND ISNULL(@P_CD_PAYDEDUCT,'') = '034' ) THEN '034'  
  WHEN ( @P_CD_COMPANY = 'YTN' AND ISNULL(@P_CD_PAYDEDUCT,'') = '037' ) THEN '037'  
  WHEN ( @P_CD_COMPANY = 'YTN' AND ISNULL(@P_CD_PAYDEDUCT,'') = '022' ) THEN '022'  
  WHEN ( @P_CD_COMPANY = 'YTN' AND ISNULL(@P_CD_PAYDEDUCT,'') = '033' ) THEN '033' END    
              
  SELECT @NMD = NM_MNGD              
    FROM FI_MNGD              
   WHERE CD_MNG = @CD AND CD_MNGD = @CDD AND CD_COMPANY = @P_CD_COMPANY     
  END   
  ELSE IF @V_SERVER_KEY IN ('KORAVL')   --한국avl20120828     
  BEGIN   
	SET @CDD = @P_CD_CC                  
	SET @NMD = @P_NM_CC     
	SET @P_CD_UMNG1 = @P_CD_CC               
  END
  ELSE IF @V_SERVER_KEY IN ('SEEGENE')   
  BEGIN
    SET @CDD = CASE @P_CD_UMNG1 WHEN '003' THEN '02'
						WHEN '005' THEN '01'
						ELSE '03' END               
    SET @NMD = CASE @CDD WHEN '01' THEN '상품'
						WHEN '02' THEN '제품'
						ELSE '기타' END  
  END 
  ELSE IF @P_CD_EXC IN ('002')    
  BEGIN  
 SET @CDD = @P_CD_UMNG1  --코드      
 SELECT @NMD = NM_SYSDEF --명칭              
    FROM MA_CODEDTL              
   WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'MA_B000030' AND CD_SYSDEF = @P_CD_UMNG1  
  END  
  ELSE IF @V_SERVER_KEY IN ('CHOSUNHOTELBA','CHOSUNHOTELBA4')  
  BEGIN  
    SET @CDD = @P_CD_UMNG1  --코드      
 SET @NMD = @P_CD_UMNG1        
  END  
  ELSE IF @V_SERVER_KEY IN ('TRIGEM')            
  BEGIN            
    SET @CDD = @P_NO_PO  --코드                
	SET @NMD = @P_NO_PO                  
  END 
  ELSE IF @V_SERVER_KEY IN ('DOMINO2')            
  BEGIN
	SET @CDD = '01'             
	SET @NMD = '01'
  END
  ELSE IF @V_SERVER_KEY IN ('ANJUN')            
  BEGIN
	SET @CDD = @P_NO_LC             
	SET @NMD = @P_NO_LC
  END
  ELSE IF @V_SERVER_KEY LIKE 'HANILTOYO%' OR  @V_SERVER_KEY LIKE 'KPCI%' 
  BEGIN
	SET @CDD = @P_USER             
	SET @NMD = @P_USER
  END
  ELSE IF @V_SERVER_KEY LIKE 'SNSTECH%'
  BEGIN
	SET @CDD = @P_USER  
	SET @NMD = @P_NM_ARRIVE  
  END
  ELSE IF @V_SERVER_KEY LIKE 'DINTEC%' 
  BEGIN
	SET @CDD = @P_CD_UMNG1  
	SET @NMD = @P_CD_UMNG1  
  END
  ELSE IF @V_SERVER_KEY LIKE 'SKTS%' OR @V_SERVER_KEY LIKE 'SKTSDEV%'  
  BEGIN
       IF(@P_NO_MODULE IN ('110', '130', '210')) --모듈구분 20170103 김동우 매입모듈 추가
       BEGIN
            SET @CDD = @P_NO_DOCU + CONVERT(NVARCHAR, @P_NO_DOLINE) 
	        SET @NMD = @P_NO_DOCU + CONVERT(NVARCHAR, @P_NO_DOLINE)
       END
  END
  ELSE IF @V_SERVER_KEY LIKE 'CSA%' 
  BEGIN

		SET @CDD = @P_USER
	    SET @NMD = @P_USER2
  END    
  ELSE IF @V_SERVER_KEY IN ('HAATZ')
  BEGIN
	SET @CDD = @P_CD_UMNG1  
	SELECT @NMD = NM_SYSDEF --명칭              
	FROM MA_CODEDTL              
    WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'MA_B000066' AND CD_SYSDEF = @P_CD_UMNG1  
  END
  ELSE     
  BEGIN           
  SET @CDD = @P_CD_UMNG1              
  END  
    
    
 ELSE IF(@CD = 'A22')     
 BEGIN  
  IF @V_SERVER_KEY IN ('KORAVL')   --한국avl20120828     
  BEGIN   
  SET @CDD = CASE ISNULL(@P_NO_MODULE,'') WHEN '210' THEN @P_NO_MDOCU WHEN '003' THEN @P_NO_INV ELSE @P_NO_MDOCU END             
  SET @NMD = CASE ISNULL(@P_NO_MODULE,'') WHEN '210' THEN @P_NO_MDOCU WHEN '003' THEN @P_NO_INV ELSE @P_NO_MDOCU END                 
  END  
  IF @V_SERVER_KEY LIKE 'KORFNT%'  
  BEGIN   
  SET @CDD = @P_CD_UMNG2            
  SET @NMD = @P_LN_PARTNER2               
  END
  ELSE IF @V_SERVER_KEY IN ('HAATZ')
  BEGIN
	SET @NMD = @P_CD_UMNG1 --명칭             
  END
  ELSE  
  BEGIN  
   IF @P_CD_EXC IN ('002')    
   BEGIN  
  SET @CDD = @P_CD_UMNG2  --코드      
  SELECT @NMD = NM_SYSDEF --명칭              
  FROM MA_CODEDTL              
    WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'MA_B000031' AND CD_SYSDEF = @P_CD_UMNG2  
   END  
   ELSE           
  SET @CDD = @P_CD_UMNG2              
  END    
  
  
 END  
 ELSE IF(@CD = 'A23')     
 BEGIN  
	IF @P_CD_EXC IN ('002')    
	BEGIN  
		SET @CDD = @P_CD_UMNG3  --코드      
		SELECT @NMD = NM_SYSDEF --명칭              
		FROM MA_CODEDTL              
		WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'MA_B000032' AND CD_SYSDEF = @P_CD_UMNG3  
	END    
	ELSE         
		SET @CDD = @P_CD_UMNG3 
	
	IF @V_SERVER_KEY IN ('DOMINO2')            
	BEGIN
		SET @CDD = '01'             
		SET @NMD = '01'
	END	
	ELSE IF @V_SERVER_KEY LIKE 'SNSTECH%'
	BEGIN
		SET @CDD = @P_NO_BL             
		SET @NMD = @P_NO_BL
	END 	     
 END          
 ELSE IF(@CD = 'A24')      
 BEGIN  
	IF @P_CD_EXC IN ('002')    
	BEGIN  
	 SET @CDD = @P_CD_UMNG4  --코드      
	 SELECT @NMD = NM_SYSDEF --명칭              
	   FROM MA_CODEDTL 
	   WHERE CD_COMPANY = @P_CD_COMPANY AND CD_FIELD = 'PU_C000007' AND CD_SYSDEF = @P_CD_UMNG4  
	END
	  IF(@V_SERVER_KEY LIKE 'HDS%')  
	  BEGIN
	   SET @CDD = @P_USER2 
	   SET @NMD = @P_USER2 
	  END
	ELSE          
	 SET @CDD = @P_CD_UMNG4              
 END   
 ELSE IF(@CD = 'A25')
               
	SET @CDD = @P_CD_UMNG5            
	       
	SELECT @NMD = NM_MNGD              
	FROM FI_MNGD              
	WHERE CD_MNG = @CD AND CD_MNGD = @CDD AND CD_COMPANY = @P_CD_COMPANY              	
 END              
 ELSE IF(@CD = 'D12') --BL번호 (2009-04-06 추가)              
 BEGIN              
   SET @CDD = @P_NO_BL              
   SET @NMD = @P_NO_BL              
 END              
 ELSE IF (@CD = 'D38' AND @V_SERVER_KEY IN ('YTN','YTN1')) -- YTN일 경우 D38의 급/상여구분    
 BEGIN    
   SET @CDD = CASE ISNULL(@P_CD_PAYDEDUCT,'') WHEN '380' THEN '2'     
          WHEN '390' THEN '5'    
          WHEN '400' THEN '2'    
          WHEN '200' THEN '6'    
          ELSE '1' END    
   SELECT @NMD = NM_MNGD              
     FROM FI_MNGD              
    WHERE CD_MNG = @CD AND CD_MNGD = @CDD AND CD_COMPANY = @P_CD_COMPANY     
 END     
 ELSE IF (@CD = 'D39' AND @V_SERVER_KEY IN ('YTN','YTN1') AND @P_CD_ACCT IN ('4220300','4420300','5510300','5510900','4220502','4420502','5511002'))    
 BEGIN    
   SET @NMD = @P_NM_PARTNER        
 END     
ELSE IF (@CD = 'D42') --주민번호 (2010-08 추가 EC 본죽)     
BEGIN       
IF (@V_SERVER_KEY IN ('GCI'))    
BEGIN    
	SET @CDD = @P_USER    
	SET @NMD = @P_USER2    
END    
ELSE
BEGIN       
 SET @CDD = @P_JUMIN_NO         
 SET @NMD = @P_JUMIN_NO              
END
END    
ELSE IF (@CD = 'D11')    
BEGIN    
   SET @CDD = @P_NO_INV    
   SET @NMD = @P_NO_INV    
END    

ELSE IF (@CD = 'D25')    
BEGIN    
	SET @CDD = @P_NO_TO    
	SET @NMD = @P_NO_TO    
END    
ELSE IF (@CD = 'D34') --한국AVL INVOICE 일자 -- 20120828
BEGIN 

 IF(@V_SERVER_KEY IN ('KORAVL'))
 BEGIN
   SET @NMD = @P_DT_ACCT
 END
 
 ELSE IF (@V_SERVER_KEY IN ('INTERBEX'))
 BEGIN    
   SET @CDD = CASE WHEN @P_CD_DOCU = '45' THEN '001' WHEN  @P_CD_DOCU = '46' THEN '002' END
   SET @NMD = CASE WHEN @P_CD_DOCU = '45' THEN '내수' WHEN  @P_CD_DOCU = '46' THEN '수입' END        
 END   
 
 ELSE IF (@V_SERVER_KEY IN ('SINJINSM','DZSQLv4') )
 BEGIN    
   SET @NMD = @P_USER
 END
 
 ELSE IF @V_SERVER_KEY LIKE 'SKTS%' OR @V_SERVER_KEY LIKE 'SKTSDEV%' 
 BEGIN
      IF(@P_NO_MODULE IN ('110', '130')) 
      BEGIN
            SET @CDD = '03'
            SET @NMD = '전자세금계산서(10%)'  
      END
      IF(@P_NO_MODULE = '210')
      BEGIN
		SELECT  @CDD = CD_MNGD,
				@NMD = NM_MNGD		 
		FROM	FI_MNGD
		WHERE	CD_COMPANY = @P_CD_COMPANY
		AND		CD_MNG = @CD
		AND     CD_MNGD = @P_USER	
      END
 END   
 
 ELSE IF (@V_SERVER_KEY LIKE 'TYPHC%') 
 BEGIN
      IF((@P_NO_MODULE = '110') AND @P_CD_ACCT BETWEEN '12470101' AND '12470105')
      BEGIN
           IF(@P_CD_BIZAREA = '1000')
           BEGIN
                SET @CDD = '1' 
                SET @NMD = '본사 출'  
           END
           ELSE IF(@P_CD_BIZAREA = '1100')
           BEGIN
                SET @CDD = '2' 
                SET @NMD = '서울 출'  
           END
           ELSE IF(@P_CD_BIZAREA = '1200')
           BEGIN
                SET @CDD = '3' 
                SET @NMD = '아산 출'  
           END
           ELSE IF(@P_CD_BIZAREA = '1300')
           BEGIN
                SET @CDD = '4' 
                SET @NMD = '익산 출'  
           END
           ELSE IF(@P_CD_BIZAREA = '1400')
           BEGIN
                SET @CDD = '5' 
                SET @NMD = '함안 출'  
           END
      END
    ELSE IF((@P_NO_MODULE = '130') AND @P_CD_ACCT BETWEEN '12470101' AND '12470105')
      BEGIN
           IF(@P_CD_PC = '1100')
           BEGIN
                SET @CDD = '2' 
                SET @NMD = '서울 출'  
           END
           ELSE IF(@P_CD_PC = '1200')
           BEGIN
                SET @CDD = '3' 
                SET @NMD = '아산 출'  
           END
           ELSE IF(@P_CD_PC = '1300')
           BEGIN
                SET @CDD = '4' 
                SET @NMD = '익산 출'  
           END
           ELSE IF(@P_CD_PC = '1400')
           BEGIN
                SET @CDD = '5' 
                SET @NMD = '함안 출'  
           END
      END
    END 
END  
         
ELSE IF (@CD = 'D40'AND @V_SERVER_KEY IN ('DZSQL','TRIGEM')) --삼보 구매거래건     
 BEGIN              
   SET @CDD = @P_PO_CONDITION  
   SET @NMD = @P_NM_CONDITION                 
 END   
                 
--유니포인트, 동원수산 
ELSE IF (@CD = 'D03')
BEGIN
  IF((@V_SERVER_KEY LIKE 'UNIPOINT%') OR (@V_SERVER_KEY LIKE 'DWFISH%'))
   BEGIN
     SELECT @CDD = @P_UM
     SELECT @NMD = @P_UM
   END
  
END

ELSE IF (@CD = 'C47')
BEGIN
  IF(@V_SERVER_KEY LIKE 'KEUNDAN%')
   BEGIN
     SET @CDD = @P_CD_DEPOSIT              
	 SET @NMD = @P_NM_DEPOSIT    
   END
END

ELSE IF (@CD = 'E01')
BEGIN
  IF(@V_SERVER_KEY LIKE 'HDS%')
   BEGIN
     SET @CDD = @P_USER              
	 SET @NMD = @P_USER    
   END
END

ELSE IF (@CD = 'C29')
BEGIN
  IF(@V_SERVER_KEY LIKE 'GIT%' AND @P_FG_BILL IN ('003', '03') AND @P_NO_MODULE = '210')
   BEGIN            
	 SET @NMD = @P_USER    
   END
END
          
 -- 예외처리 → 인사모듈에서 호출했을경우 관리항목 체크하지 않는다. (2008-06-09)              
IF(ISNULL(@P_NO_MDOCU,'') <> '' AND LEN(@P_NO_MDOCU) > 2 AND SUBSTRING(@P_NO_MDOCU,1,2) = 'HR')             
 SET @P_MNG_CHECK = 'N'              

  -- 전표생성 시 적요 :@NM_BIZAREA + '/' + CONVERT(VARCHAR, CONVERT(DATE, @P_DT_PROCESS), 111) + '/POS' / (주)신촌아이리버존/2015/02/04/POS
 -- 아이리버 전용(20150209 LKJ)
IF(@V_SERVER_KEY ='IRIVER')
BEGIN
	--@P_CD_ACCT ='111111'(현금계정) , D34:현금보관처
	--@CDD(01,02,03,04) @NMD(신촌, 대전, 대구, 본사)
	--CD_BIZAREA 1100,1200,1300(신촌,대전,대구)
	IF @CD ='D34'
	BEGIN 
	 SELECT  @CDD = CD_MNGD,
			 @NMD = NM_MNGD		 
		FROM FI_MNGD
		WHERE CD_COMPANY = @P_CD_COMPANY
		AND CD_MNG = @CD
		AND MNGD_ETC = @P_CD_BIZAREA
	END		         
	-- 보통예금 예적금계좌 - 금융기관 거래처로 강제 SETTING(20150302 LKJ)
	IF @P_CD_ACCT = '111151' AND @CD = 'A06'
	BEGIN
	SELECT @CDD = (CASE WHEN FD.CD_BANK IS NULL OR FD.CD_BANK ='' THEN @P_CD_PARTNER ELSE FD.CD_BANK END) ,
		   @NMD = (CASE WHEN MP.LN_PARTNER IS NULL OR MP.LN_PARTNER ='' THEN @P_NM_PARTNER ELSE MP.LN_PARTNER END)
	FROM FI_DEPOSIT	FD
	INNER JOIN MA_PARTNER MP ON (FD.CD_BANK = MP.CD_PARTNER AND FD.CD_COMPANY = MP.CD_COMPANY AND MP.FG_PARTNER ='002')
	WHERE FD.CD_COMPANY = @P_CD_COMPANY
	AND FD.CD_DEPOSIT = @P_CD_DEPOSIT
	END
END       
              
 -- 관리항목 필수 여부 체크 ('A' : 차대필수, 'C' : 대변필수, 'D' : 차변필수) 추가 (2007-05-28)              
 IF @CNT = 1              
 BEGIN              
  IF(@P_MNG_CHECK = 'Y')              
  BEGIN              
   IF(@T_ST_MNG1 = 'A')              
   BEGIN              
    IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
    BEGIN              
     print '1'              
     SET @P_MSG = 'KR##차대필수 관리항목이 누락되었습니다.##US##Debit/Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
     SET @P_ERRORCODE = 1              
     SET @P_ERRORMSG = @P_MSG              
     RETURN              
    END              
   END              
              
   ELSE IF(@T_ST_MNG1 = 'C')              
   BEGIN              
    IF(@P_TP_DRCR = '2')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##대변필수 관리항목이 누락되었습니다.##US##Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
  SET @P_ERRORCODE = 1              
  SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
              
   ELSE IF(@T_ST_MNG1 = 'D')              
   BEGIN              
    IF(@P_TP_DRCR = '1')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##차변필수 관리항목이 누락되었습니다.##US##Debit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN        
     END              
    END     
   END              
  END -- IF(@P_MNG_CHECK = 'Y')              
              
  SET @T_CD_MNGD1 = @CDD              
  SET @T_NM_MNGD1 = @NMD              
 END  -- IF @CNT = 1              
              
 ELSE IF @CNT = 2              
 BEGIN              
  IF(@P_MNG_CHECK = 'Y')              
  BEGIN              
   IF(@T_ST_MNG2 = 'A')              
   BEGIN              
    IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
    BEGIN              
         print '2'              
              
     SET @P_MSG = 'KR##차대필수 관리항목이 누락되었습니다.##US##Debit/Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
     SET @P_ERRORCODE = 1              
     SET @P_ERRORMSG = @P_MSG              
     RETURN              
    END              
   END              
   ELSE IF(@T_ST_MNG2 = 'C')              
   BEGIN              
    IF(@P_TP_DRCR = '2')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##대변필수 관리항목이 누락되었습니다.##US##Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
              
   ELSE IF(@T_ST_MNG2 = 'D')              
   BEGIN              
    IF(@P_TP_DRCR = '1')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##차변필수 관리항목이 누락되었습니다.##US##Debit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
  RETURN              
     END              
    END              
   END              
  END -- IF(@P_MNG_CHECK = 'Y')              
              
  SET @T_CD_MNGD2 = @CDD              
  SET @T_NM_MNGD2 = @NMD              
 END -- IF @CNT = 2              
              
 ELSE IF @CNT = 3              
 BEGIN              
  IF(@P_MNG_CHECK = 'Y')              
  BEGIN              
   IF(@T_ST_MNG3 = 'A')              
   BEGIN              
    IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
    BEGIN              
         print '3'              
              
     SET @P_MSG = 'KR##차대필수 관리항목이 누락되었습니다.##US##Debit/Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
     SET @P_ERRORCODE = 1              
     SET @P_ERRORMSG = @P_MSG              
     RETURN              
    END              
   END              
              
   ELSE IF(@T_ST_MNG3 = 'C')              
   BEGIN              
    IF(@P_TP_DRCR = '2')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##대변필수 관리항목이 누락되었습니다.##US##Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
              
   ELSE IF(@T_ST_MNG3 = 'D')              
   BEGIN              
    IF(@P_TP_DRCR = '1')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##차변필수 관리항목이 누락되었습니다.##US##Debit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
END              
    END              
    END              
  END -- IF(@P_MNG_CHECK = 'Y')              
              
  SET @T_CD_MNGD3 = @CDD              
  SET @T_NM_MNGD3 = @NMD              
 END -- IF @CNT = 3              
              
 ELSE IF @CNT = 4              
 BEGIN              
  IF(@P_MNG_CHECK = 'Y')              
  BEGIN              
   IF(@T_ST_MNG4 = 'A')              
   BEGIN              
    IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
    BEGIN              
         print '4'              
              
     SET @P_MSG = 'KR##차대필수 관리항목이 누락되었습니다.##US##Debit/Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
     SET @P_ERRORCODE = 1              
     SET @P_ERRORMSG = @P_MSG              
     RETURN              
    END              
   END              
              
   ELSE IF(@T_ST_MNG4 = 'C')              
   BEGIN              
    IF(@P_TP_DRCR = '2')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##대변필수 관리항목이 누락되었습니다.##US##Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
              
   ELSE IF(@T_ST_MNG4 = 'D')              
   BEGIN              
    IF(@P_TP_DRCR = '1')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##차변필수 관리항목이 누락되었습니다.##US##Debit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
  END -- IF(@P_MNG_CHECK = 'Y')              
              
  SET @T_CD_MNGD4 = @CDD              
  SET @T_NM_MNGD4 = @NMD              
 END -- IF @CNT = 4              
              
 ELSE IF @CNT = 5              
 BEGIN              
  IF(@P_MNG_CHECK = 'Y')              
  BEGIN              
   IF(@T_ST_MNG5 = 'A')              
   BEGIN              
    IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
    BEGIN                 
     SET @P_MSG = 'KR##차대필수 관리항목이 누락되었습니다.##US##Debit/Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
     SET @P_ERRORCODE = 1              
     SET @P_ERRORMSG = @P_MSG              
     RETURN              
    END              
   END              
              
   ELSE IF(@T_ST_MNG5 = 'C')              
   BEGIN              
    IF(@P_TP_DRCR = '2')             
     BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##대변필수 관리항목이 누락되었습니다.##US##Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1             
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
              
   ELSE IF(@T_ST_MNG5 = 'D')              
   BEGIN              
    IF(@P_TP_DRCR = '1')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##차변필수 관리항목이 누락되었습니다.##US##Debit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
  END -- IF(@P_MNG_CHECK = 'Y')              
              
  SET @T_CD_MNGD5 = @CDD              
  SET @T_NM_MNGD5 = @NMD              
 END -- IF @CNT = 5         
              
 ELSE IF @CNT = 6   
 BEGIN              
  IF(@P_MNG_CHECK = 'Y')              
  BEGIN              
   IF(@T_ST_MNG6 = 'A')              
   BEGIN              
    IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
    BEGIN              
         print '6'              
              
     SET @P_MSG = 'KR##차대필수 관리항목이 누락되었습니다.##US##Debit/Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
     SET @P_ERRORCODE = 1              
     SET @P_ERRORMSG = @P_MSG              
     RETURN              
    END              
   END              
              
   ELSE IF(@T_ST_MNG6 = 'C')              
   BEGIN              
    IF(@P_TP_DRCR = '2')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##대변필수 관리항목이 누락되었습니다.##US##Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
              
   ELSE IF(@T_ST_MNG6 = 'D')              
   BEGIN              
    IF(@P_TP_DRCR = '1')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##차변필수 관리항목이 누락되었습니다.##US##Debit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
  END -- IF(@P_MNG_CHECK = 'Y')              
              
  SET @T_CD_MNGD6 = @CDD              
  SET @T_NM_MNGD6 = @NMD              
 END -- IF @CNT = 6              
              
 ELSE IF @CNT = 7         
 BEGIN              
  IF(@P_MNG_CHECK = 'Y')              
  BEGIN              
   IF(@T_ST_MNG7 = 'A')              
   BEGIN              
    IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
    BEGIN              
         --print '7'              
              
     SET @P_MSG = 'KR##차대필수 관리항목이 누락되었습니다.##US##Debit/Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
     SET @P_ERRORCODE = 1              
     SET @P_ERRORMSG = @P_MSG              
     RETURN              
    END              
   END              
              
   ELSE IF(@T_ST_MNG7 = 'C')              
   BEGIN              
    IF(@P_TP_DRCR = '2')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##대변필수 관리항목이 누락되었습니다.##US##Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
              
   ELSE IF(@T_ST_MNG7 = 'D')              
   BEGIN              
    IF(@P_TP_DRCR = '1')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##차변필수 관리항목이 누락되었습니다.##US##Debit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
  END -- IF(@P_MNG_CHECK = 'Y')              
              
  SET @T_CD_MNGD7 = @CDD              
  SET @T_NM_MNGD7 = @NMD              
 END -- IF @CNT = 7              
          
 ELSE IF @CNT = 8              
 BEGIN              
  IF(@P_MNG_CHECK = 'Y')              
  BEGIN              
   IF(@T_ST_MNG8 = 'A')              
   BEGIN              
    IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
    BEGIN              
         --print '8'              
              
     SET @P_MSG = 'KR##차대필수 관리항목이 누락되었습니다.##US##Debit/Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
     SET @P_ERRORCODE = 1              
     SET @P_ERRORMSG = @P_MSG              
     RETURN              
    END              
   END              
              
   ELSE IF(@T_ST_MNG8 = 'C')              
   BEGIN              
    IF(@P_TP_DRCR = '2')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##대변필수 관리항목이 누락되었습니다.##US##Credit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1             
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
     END              
    END              
   END              
              
   ELSE IF(@T_ST_MNG8 = 'D')              
   BEGIN              
    IF(@P_TP_DRCR = '1')              
    BEGIN              
     IF(ISNULL(@CDD,'') = '' AND ISNULL(@NMD,'') = '')              
     BEGIN              
      SET @P_MSG = 'KR##차변필수 관리항목이 누락되었습니다.##US##Debit Managerial Item does not exist.##'              
   RAISERROR (@P_MSG, 18, 1)  
      SET @P_ERRORCODE = 1              
      SET @P_ERRORMSG = @P_MSG              
      RETURN              
    END              
    END              
   END              
  END -- IF(@P_MNG_CHECK = 'Y')              
              
  SET @T_CD_MNGD8 = @CDD              
  SET @T_NM_MNGD8 = @NMD              
 END -- IF @CNT = 8              
END              
              
-- 반제일경우 @P_AM_AMT 셋팅              
--IF(@P_TP_ACAREA = '4' OR @P_TP_ACAREA = '1' OR @P_YN_FUNDPLAN = 'Y')   -- 이거 필요없을 듯..              
 SET @P_AM_AMT = @P_AM_DR + @P_AM_CR              
              
IF(ISNULL(@P_CD_FUND, '') = '')              
BEGIN              
 IF @P_TP_DRCR = '2' -- 대변이면              
  SET @P_CD_FUND = @P_CD_CRFUND              
 ELSE              
  SET @P_CD_FUND = @P_CD_DRFUND              
END              
              
-- NULL --> 0 처리              
IF(ISNULL(@P_NO_ACCT,0) = 0) SET @P_NO_ACCT = 0              
IF(ISNULL(@P_AM_ACTSUM,0) = 0) SET @P_AM_ACTSUM = 0              
IF(ISNULL(@P_AM_JSUM,0) = 0) SET @P_AM_JSUM = 0              
IF(ISNULL(@P_NO_BDOLINE,0) = 0) SET @P_NO_BDOLINE = 0              
IF(ISNULL(@P_AM_EXDO,0) = 0) SET @P_AM_EXDO = 0              
IF(ISNULL(@P_RT_EXCH,0) = 0) SET @P_RT_EXCH = 0              
-- NULL --> '' 처리 (IF문을 위해)              
IF(ISNULL(@T_CD_MNG1,'') = '') SET @T_CD_MNG1 = ''              
IF(ISNULL(@T_CD_MNG2,'') = '') SET @T_CD_MNG2 = ''              
IF(ISNULL(@T_CD_MNG3,'') = '') SET @T_CD_MNG3 = ''              
IF(ISNULL(@T_CD_MNG4,'') = '') SET @T_CD_MNG4 = ''              
IF(ISNULL(@T_CD_MNG5,'') = '') SET @T_CD_MNG5 = ''              
IF(ISNULL(@T_CD_MNG6,'') = '') SET @T_CD_MNG6 = ''              
IF(ISNULL(@T_CD_MNG7,'') = '') SET @T_CD_MNG7 = ''              
IF(ISNULL(@T_CD_MNG8,'') = '') SET @T_CD_MNG8 = ''              
              
-- 관리항목에 걸려있는 A타입만 넣어준다. (2008-10-16)              
-- 사업장 체크              
IF(@T_CD_MNG1 <> 'A01' AND @T_CD_MNG2 <> 'A01' AND @T_CD_MNG3 <> 'A01' AND @T_CD_MNG4 <> 'A01' AND @T_CD_MNG5 <> 'A01' AND @T_CD_MNG6 <> 'A01' AND @T_CD_MNG7 <> 'A01' AND @T_CD_MNG8 <> 'A01')              
 SET @P_CD_BIZAREA = ''              
              
-- C/C 체크              
IF(@T_CD_MNG1 <> 'A02' AND @T_CD_MNG2 <> 'A02' AND @T_CD_MNG3 <> 'A02' AND @T_CD_MNG4 <> 'A02' AND @T_CD_MNG5 <> 'A02' AND @T_CD_MNG6 <> 'A02' AND @T_CD_MNG7 <> 'A02' AND @T_CD_MNG8 <> 'A02')              
 SET @P_CD_CC = ''              
  
-- 부서 체크              
IF(@T_CD_MNG1 <> 'A03' AND @T_CD_MNG2 <> 'A03' AND @T_CD_MNG3 <> 'A03' AND @T_CD_MNG4 <> 'A03' AND @T_CD_MNG5 <> 'A03' AND @T_CD_MNG6 <> 'A03' AND @T_CD_MNG7 <> 'A03' AND @T_CD_MNG8 <> 'A03')              
 SET @P_CD_DEPT = ''              
              
-- 사원 체크              
IF(@T_CD_MNG1 <> 'A04' AND @T_CD_MNG2 <> 'A04' AND @T_CD_MNG3 <> 'A04' AND @T_CD_MNG4 <> 'A04' AND @T_CD_MNG5 <> 'A04' AND @T_CD_MNG6 <> 'A04' AND @T_CD_MNG7 <> 'A04' AND @T_CD_MNG8 <> 'A04')              
 SET @P_CD_EMPLOY = ''              
              
-- 프로젝트 체크              
IF(@T_CD_MNG1 <> 'A05' AND @T_CD_MNG2 <> 'A05' AND @T_CD_MNG3 <> 'A05' AND @T_CD_MNG4 <> 'A05' AND @T_CD_MNG5 <> 'A05' AND @T_CD_MNG6 <> 'A05' AND @T_CD_MNG7 <> 'A05' AND @T_CD_MNG8 <> 'A05')              
 SET @P_CD_PJT = ''              
              
-- 거래처 체크              
IF(@T_CD_MNG1 <> 'A06' AND @T_CD_MNG2 <> 'A06' AND @T_CD_MNG3 <> 'A06' AND @T_CD_MNG4 <> 'A06' AND @T_CD_MNG5 <> 'A06' AND @T_CD_MNG6 <> 'A06' AND @T_CD_MNG7 <> 'A06' AND @T_CD_MNG8 <> 'A06')              
 SET @P_CD_PARTNER = ''              
        
-- 예적금코드 체크              
IF(@T_CD_MNG1 <> 'A07' AND @T_CD_MNG2 <> 'A07' AND @T_CD_MNG3 <> 'A07' AND @T_CD_MNG4 <> 'A07' AND @T_CD_MNG5 <> 'A07' AND @T_CD_MNG6 <> 'A07' AND @T_CD_MNG7 <> 'A07' AND @T_CD_MNG8 <> 'A07')              
 SET @P_CD_DEPOSIT = ''                    
-- 신용카드 체크              
IF(@T_CD_MNG1 <> 'A08' AND @T_CD_MNG2 <> 'A08' AND @T_CD_MNG3 <> 'A08' AND @T_CD_MNG4 <> 'A08' AND @T_CD_MNG5 <> 'A08' AND @T_CD_MNG6 <> 'A08' AND @T_CD_MNG7 <> 'A08' AND @T_CD_MNG8 <> 'A08')              
 SET @P_CD_CARD = ''              
              
-- 금융기관 체크              
IF(@T_CD_MNG1 <> 'A09' AND @T_CD_MNG2 <> 'A09' AND @T_CD_MNG3 <> 'A09' AND @T_CD_MNG4 <> 'A09' AND @T_CD_MNG5 <> 'A09' AND @T_CD_MNG6 <> 'A09' AND @T_CD_MNG7 <> 'A09' AND @T_CD_MNG8 <> 'A09')              
 SET @P_CD_BANK = ''              
              
-- 품목 체크              
IF(@T_CD_MNG1 <> 'A10' AND @T_CD_MNG2 <> 'A10' AND @T_CD_MNG3 <> 'A10' AND @T_CD_MNG4 <> 'A10' AND @T_CD_MNG5 <> 'A10' AND @T_CD_MNG6 <> 'A10' AND @T_CD_MNG7 <> 'A10' AND @T_CD_MNG8 <> 'A10')              
 SET @P_NO_ITEM = ''              
              
-- 사용자정의관리항목 체크              
IF(@T_CD_MNG1 <> 'A21' AND @T_CD_MNG2 <> 'A21' AND @T_CD_MNG3 <> 'A21' AND @T_CD_MNG4 <> 'A21' AND @T_CD_MNG5 <> 'A21' AND @T_CD_MNG6 <> 'A21' AND @T_CD_MNG7 <> 'A21' AND @T_CD_MNG8 <> 'A21')              
 SET @P_CD_UMNG1 = ''              
IF(@T_CD_MNG1 <> 'A22' AND @T_CD_MNG2 <> 'A22' AND @T_CD_MNG3 <> 'A22' AND @T_CD_MNG4 <> 'A22' AND @T_CD_MNG5 <> 'A22' AND @T_CD_MNG6 <> 'A22' AND @T_CD_MNG7 <> 'A22' AND @T_CD_MNG8 <> 'A22')              
 SET @P_CD_UMNG2 = ''              
IF(@T_CD_MNG1 <> 'A23' AND @T_CD_MNG2 <> 'A23' AND @T_CD_MNG3 <> 'A23' AND @T_CD_MNG4 <> 'A23' AND @T_CD_MNG5 <> 'A23' AND @T_CD_MNG6 <> 'A23' AND @T_CD_MNG7 <> 'A23' AND @T_CD_MNG8 <> 'A23')              
 SET @P_CD_UMNG3 = ''              
IF(@T_CD_MNG1 <> 'A24' AND @T_CD_MNG2 <> 'A24' AND @T_CD_MNG3 <> 'A24' AND @T_CD_MNG4 <> 'A24' AND @T_CD_MNG5 <> 'A24' AND @T_CD_MNG6 <> 'A24' AND @T_CD_MNG7 <> 'A24' AND @T_CD_MNG8 <> 'A24')              
 SET @P_CD_UMNG4 = ''              
IF(@T_CD_MNG1 <> 'A25' AND @T_CD_MNG2 <> 'A25' AND @T_CD_MNG3 <> 'A25' AND @T_CD_MNG4 <> 'A25' AND @T_CD_MNG5 <> 'A25' AND @T_CD_MNG6 <> 'A25' AND @T_CD_MNG7 <> 'A25' AND @T_CD_MNG8 <> 'A25')              
 SET @P_CD_UMNG5 = ''              


IF(@V_SERVER_KEY = 'LUCOMS')
BEGIN              
	--해당 관리항목미지정시 관련인자 초기화 (놔둘경우 의도하지안게 외화채권에 반영됨) 20120608 김헌섭 - 대우루컴즈  
	IF(@T_CD_MNG1 <> 'B24' AND @T_CD_MNG2 <> 'B24' AND @T_CD_MNG3 <> 'B24' AND @T_CD_MNG4 <> 'B24' AND @T_CD_MNG5 <> 'B24' AND @T_CD_MNG6 <> 'B24' AND @T_CD_MNG7 <> 'B24' AND @T_CD_MNG8 <> 'B24')              
	BEGIN  
	   SET @P_CD_EXCH = ''              
	   SET @P_NM_EXCH = ''     
	END     
	IF(@T_CD_MNG1 <> 'B25' AND @T_CD_MNG2 <> 'B25' AND @T_CD_MNG3 <> 'B25' AND @T_CD_MNG4 <> 'B25' AND @T_CD_MNG5 <> 'B25' AND @T_CD_MNG6 <> 'B25' AND @T_CD_MNG7 <> 'B25' AND @T_CD_MNG8 <> 'B25')              
	 SET @P_RT_EXCH = 0             
	IF(@T_CD_MNG1 <> 'B26' AND @T_CD_MNG2 <> 'B26' AND @T_CD_MNG3 <> 'B26' AND @T_CD_MNG4 <> 'B26' AND @T_CD_MNG5 <> 'B26' AND @T_CD_MNG6 <> 'B26' AND @T_CD_MNG7 <> 'B26' AND @T_CD_MNG8 <> 'B26')              
	 SET @P_AM_EXDO = 0  
END  
               
EXEC UP_FI_DOCU_INSERT              
   @P_NO_DOCU,              
   @P_NO_DOLINE,              
   @P_CD_PC,              
   @P_CD_COMPANY,              
   @P_CD_WDEPT,              
   @P_ID_WRITE ,              
   @P_DT_ACCT  ,       
   @P_NO_ACCT  ,              
   @P_TP_DOCU  ,              
   @P_CD_DOCU  ,              
   @P_ST_DOCU  ,              
   @P_ID_ACCEPT ,              
   @P_TP_DRCR  ,              
   @P_CD_ACCT  ,              
   @P_NM_NOTE  ,              
   @P_AM_DR   ,              
   @P_AM_CR   ,              
   @P_TP_ACAREA ,              
   @P_CD_RELATION,              
   @P_CD_BUDGET  ,              
   @P_CD_FUND  ,              
   @P_TP_TAX   ,              
   @P_NO_BDOCU  ,              
   @P_NO_BDOLINE ,              
   @P_TP_ETCACCT  ,              
   @P_CD_BIZAREA  , -- 귀속사업장           
   @P_CD_CC   ,   -- 코스트센터              
   @P_CD_PJT  ,   -- 프로젝트              
   @P_CD_DEPT ,   -- 부서코드              
   @P_CD_EMPLOY , -- 사원              
   @P_CD_PARTNER , -- 거래처              
   @P_CD_DEPOSIT, -- 예적금코드              
   @P_CD_CARD ,   -- 신용카드              
   @P_CD_BANK ,   -- 금융기관              
   @P_NO_ITEM ,   -- 품목              
   @P_CD_UMNG1   ,              
   @P_CD_UMNG2   ,              
   @P_CD_UMNG3   ,              
   @P_CD_UMNG4   ,              
   @P_CD_UMNG5   ,              
   @P_CD_MNG    ,              
   @P_CD_TRADE   ,              
   @P_DT_START   ,              
   @P_DT_END    ,              
   @P_CD_EXCH   ,              
   @P_RT_EXCH   ,              
   @P_AM_EXDO   ,              
   @T_CD_MNG1   ,              
   @T_CD_MNGD1   ,              
   @T_NM_MNGD1   ,              
   @T_CD_MNG2   ,              
   @T_CD_MNGD2   ,              
   @T_NM_MNGD2   ,              
   @T_CD_MNG3   ,              
   @T_CD_MNGD3   ,              
   @T_NM_MNGD3   ,              
   @T_CD_MNG4   ,              
   @T_CD_MNGD4   ,              
   @T_NM_MNGD4   ,              
   @T_CD_MNG5   ,              
   @T_CD_MNGD5   ,              
   @T_NM_MNGD5   ,              
   @T_CD_MNG6   ,              
   @T_CD_MNGD6   ,              
   @T_NM_MNGD6   ,              
   @T_CD_MNG7   ,              
   @T_CD_MNGD7   ,              
   @T_NM_MNGD7   ,              
   @T_CD_MNG8   ,              
   @T_CD_MNGD8   ,              
   @T_NM_MNGD8   ,              
   @P_NO_MODULE   ,              
   @P_NO_MDOCU   ,              
   @P_CD_EPNOTE   ,              
   @P_ID_INSERT   ,              
   @P_AM_AMT  ,              
   @P_YN_FUNDPLAN,              
   @P_CD_RELATION1,              
   @P_CD_BGACCT  ,              
   @P_NM_PUMM  ,              
   @P_TP_EPNOTE ,              
   @P_DT_WRITE   ,              
   @P_AM_ACTSUM ,              
   @P_AM_JSUM  ,              
   @P_CD_BIZPLAN,              
   NULL,
   NULL,
   NULL,
   0,
   NULL,
   NULL,
   NULL,
   0,
   NULL,
   NULL,
   NULL,
   NULL,
   @P_TP_EVIDENCE
              
 SET @P_ERRORCODE = 0              
              
RETURN