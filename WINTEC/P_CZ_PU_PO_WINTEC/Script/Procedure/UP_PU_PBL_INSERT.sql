USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[UP_PU_PBL_INSERT]    Script Date: 2022-03-24 ¿ÀÈÄ 6:07:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[UP_PU_PBL_INSERT]    
(   
    @P_CD_COMPANY     NVARCHAR(7), 
    @P_NO_PO          NVARCHAR(20),  
    @P_NO_POLINE	  NUMERIC(5,0),
    @P_NO_SEQ		  NUMERIC(5,0),  
    @P_FG_IV	      NVARCHAR(3),
    @P_DT_PUR_PLAN	  NVARCHAR(8),
    @P_RT_IV		  NUMERIC(5,0),
    @P_AM			  NUMERIC(17,4),
    @P_AM_K			  NUMERIC(17,4),
    @P_AM_VAT		  NUMERIC(17,4),
    @P_AM_SUM		  NUMERIC(17,4),
    @P_AM_PUL		  NUMERIC(17,4),
    @P_NM_TEXT		  NVARCHAR(100),
    @P_CD_PJT		  NVARCHAR(20),
    @P_SEQ_PROJECT    NUMERIC(5,0),
    @P_NM_TEXT2		  NVARCHAR(100),
    @P_ID_INSERT	  NVARCHAR(15)
)        
AS    
SET NOCOUNT ON  

DECLARE @V_SYSDATE  VARCHAR(14)  

SET @V_SYSDATE = NEOE.SF_SYSDATE(GETDATE())       
    
-----------------------------------------------------

BEGIN            
    INSERT INTO PU_PBL
    (   
		CD_COMPANY, NO_PO,		    NO_POLINE,  NO_SEQ, 
		FG_IV,		DT_PUR_PLAN,	RT_IV,		AM, 
		AM_K,		AM_VAT,			AM_SUM,		AM_PUL, 
		NM_TEXT,	NM_TEXT2,		CD_PJT,     SEQ_PROJECT,
		ID_INSERT,	DTS_INSERT     
    )        
    VALUES        
    ( 
        @P_CD_COMPANY,	@P_NO_PO,		@P_NO_POLINE,   @P_NO_SEQ, 
        @P_FG_IV,		@P_DT_PUR_PLAN, @P_RT_IV,		@P_AM, 
        @P_AM_K,		@P_AM_VAT,		@P_AM_SUM,		@P_AM_PUL, 
        @P_NM_TEXT,		@P_NM_TEXT2,	@P_CD_PJT,		@P_SEQ_PROJECT,
        @P_ID_INSERT,	@V_SYSDATE		
    )    
END
GO


