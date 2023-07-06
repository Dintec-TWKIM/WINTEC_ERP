USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_SA_SO_MNG_UPDATE]    Script Date: 2019-11-14 오후 3:12:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************    
*********************************************/    
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SO_MNG_WINTEC_U]    
(    
    @P_CD_COMPANY       NVARCHAR(7),         --회사    
    @P_NO_SO            NVARCHAR(20),        --수주번호    
    @P_SEQ_SO           NUMERIC(5,0),        --수주이력항번    
    @P_STA_SO           NVARCHAR(3),         --수주상태    
    @P_DC1              NVARCHAR(200),       --비고1    
    @P_DC2              NVARCHAR(200),       --비고2    
    @P_FG_USE           NVARCHAR(4),         --수주용도1    
    @P_FG_USE2          NVARCHAR(4),         --수주용도2
    @P_ID_UPDATE        NVARCHAR(15),        --UPDATE 한 사람  
    @P_TXT_USERDEF1		NVARCHAR(100) = NULL,
    @P_TXT_USERDEF2		NVARCHAR(100) = NULL,
    @P_TXT_USERDEF3		NVARCHAR(100) = NULL,
	@P_DT_EXPECT		NVARCHAR(8) = NULL,  --최초납기요구일
	@P_DT_DUEDATE		NVARCHAR(8) = NULL,	 --납기요구일
	@P_DT_REQGI			NVARCHAR(8) = NULL,	 --출하예정일
	@P_NUM_USERDEF3		NUMERIC(17, 4) = 0,	 --신규제작소요일
	@P_QT_SCORE			NUMERIC(17, 4) = 0,	 --납기준수율점수
    @P_NO_LC            NVARCHAR(100),       --LC번호
	@P_TXT_USERDEF5     NVARCHAR(100),       --납품장소
    @P_CD_USERDEF3      NVARCHAR(3),         --엔진타입
	@P_TXT_USERDEF7		NVARCHAR(100),		 --도면번호(수주)
	@P_DC_RMK_TEXT		text,				 --텍스트비고1
	@P_DC_RMK1			NVARCHAR(100)		 --비고1
)    
AS    

UPDATE SA_SOH
SET TXT_USERDEF2 = @P_NO_LC,
	DC_RMK_TEXT = @P_DC_RMK_TEXT,
	DC_RMK1 = @P_DC_RMK1
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_SO = @P_NO_SO

UPDATE SA_SOL    
SET STA_SO = @P_STA_SO,    
    DC1 = @P_DC1,    
    DC2 = @P_DC2,  
    FG_USE = @P_FG_USE,
    FG_USE2 = @P_FG_USE2, 
    DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE()),  
    ID_UPDATE = @P_ID_UPDATE,     
    TXT_USERDEF1 = @P_TXT_USERDEF1,
    TXT_USERDEF2 = @P_TXT_USERDEF2,
    TXT_USERDEF3 = @P_TXT_USERDEF3,
    TXT_USERDEF5 = @P_TXT_USERDEF5,
    CD_USERDEF3 = @P_CD_USERDEF3,
	DT_EXPECT = @P_DT_EXPECT,
	DT_DUEDATE = @P_DT_DUEDATE,
	DT_REQGI = @P_DT_REQGI,
	NUM_USERDEF3 = @P_NUM_USERDEF3,
	NUM_USERDEF4 = @P_QT_SCORE,
	TXT_USERDEF7 = @P_TXT_USERDEF7
WHERE CD_COMPANY  = @P_CD_COMPANY    
AND NO_SO = @P_NO_SO    
AND SEQ_SO = @P_SEQ_SO

GO