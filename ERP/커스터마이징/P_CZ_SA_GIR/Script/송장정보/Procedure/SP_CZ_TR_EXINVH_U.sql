USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_TR_EXINVH_U]    Script Date: 2015-04-27 오후 8:15:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************    
**  System : 무역  
**  Sub System : 수출  
**  Page  : 송장작성  
**  Desc  : 송장작성 수정  
**  수정자 : 허성철    
************************************************************************/
 
ALTER PROCEDURE [NEOE].[SP_CZ_TR_EXINVH_U]    
    @P_NO_INV             NVARCHAR(20),    
    @P_CD_COMPANY         NVARCHAR(7),    
    @P_DT_BALLOT          NCHAR(8),    
    @P_CD_BIZAREA         NVARCHAR(7),    
    @P_CD_SALEGRP         NVARCHAR(7),    
    @P_NO_EMP_INV         NVARCHAR(10),    

    @P_FG_LC              NCHAR(3),    
    @P_CD_PARTNER         NVARCHAR(20),    
    @P_CD_EXCH            NVARCHAR(3),    
    @P_AM_EX              NUMERIC(17,4),
    @P_DT_LOADING         NCHAR(8),    

    @P_CD_ORIGIN          NVARCHAR(3),    
    @P_CD_AGENT           NVARCHAR(7),    
    @P_CD_EXPORT          NVARCHAR(7),    
    @P_CD_PRODUCT         NVARCHAR(7),    
    @P_SHIP_CORP          NVARCHAR(7),    

    @P_NM_VESSEL          NVARCHAR(50),    
    @P_COND_TRANS         NVARCHAR(50),    
    @P_TP_TRANSPORT       NVARCHAR(3),    
    @P_TP_TRANS           NVARCHAR(3),    
    @P_TP_PACKING         NVARCHAR(3),    

    @P_CD_WEIGHT          NVARCHAR(3),    
    @P_GROSS_WEIGHT       NUMERIC(17,4),    
    @P_NET_WEIGHT         NUMERIC(17,4),    
    @P_PORT_LOADING       NVARCHAR(50),    
    @P_PORT_ARRIVER       NVARCHAR(50),    

    @P_DESTINATION        NVARCHAR(50),    
    @P_NO_SCT             NUMERIC(10,0),    
    @P_NO_ECT             NUMERIC(10,0),    
    @P_CD_NOTIFY          NVARCHAR(20),    
    @P_DT_TO              NCHAR(8),    

    @P_NO_LC              NVARCHAR(20),    
    @P_NO_SO              NVARCHAR(20),    
    @P_REMARK1            NVARCHAR(100),    
    @P_REMARK2            NVARCHAR(100),    
    @P_REMARK3            NVARCHAR(100),    

    @P_REMARK4            NVARCHAR(100),    
    @P_REMARK5            NVARCHAR(100),    
    @P_DTS_INSERT         NVARCHAR(14),   --등록일
    @P_ID_INSERT          NVARCHAR(15),   --등록자
    @P_DTS_UPDATE         NVARCHAR(14),   --수정자

    @P_ID_UPDATE          NVARCHAR(15),   --수정일
    --@P_NO_TO            NVARCHAR(15),   --통관번호
    --@P_NO_BL            NVARCHAR(15),   --선적번호
    @P_NM_NOTIFY          NVARCHAR(100),  --착하통지처
    @P_ADDR1_NOTIFY       NVARCHAR(400),  --통지처주소1

    @P_ADDR2_NOTIFY       NVARCHAR(400),   --통지처주소2
    @P_CD_CONSIGNEE       NVARCHAR(20),    --수하인
    @P_NM_CONSIGNEE       NVARCHAR(100),   --수하인명
    @P_ADDR1_CONSIGNEE    NVARCHAR(400),  --수하인주소1
    @P_ADDR2_CONSIGNEE    NVARCHAR(400),  --수하인주소2
    @P_REMARK             TEXT,

    @P_NM_PARTNER         NVARCHAR(100),
    @P_ADDR1_PARTNER      NVARCHAR(400),
    @P_ADDR2_PARTNER      NVARCHAR(400),
    @P_NM_EXPORT          NVARCHAR(100),
    @P_ADDR1_EXPORT       NVARCHAR(400),
    @P_ADDR2_EXPORT       NVARCHAR(400),
    @P_COND_PRICE         NVARCHAR(3),
    @P_DESCRIPTION        NVARCHAR(150),
    @P_GROSS_VOLUME       NUMERIC(17,4), 
    @P_FG_FREIGHT         NVARCHAR(3), 
    @P_AM_FREIGHT         NUMERIC(17,4),
    
    @P_YN_RETURN          NVARCHAR(3) = 'N',
    @P_DT_SAILING_ON      NVARCHAR(8) = NULL,
    @P_TXT_REMARK2        NVARCHAR(4000) = NULL,
    @P_CD_BANK			  NVARCHAR(20) = NULL,
    @P_COND_PAY 		  NVARCHAR(6) = NULL,
	@P_ARRIVER_COUNTRY	  NVARCHAR(3),
    @P_YN_INSURANCE		  NVARCHAR(1)
AS    
BEGIN
--DECLARE @P_DTS_UPDATE VARCHAR(14)    
--SET @P_DTS_UPDATE = REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), '-',''), ' ', ''),':','')    

UPDATE CZ_TR_INVH 
SET     
	DT_BALLOT           = @P_DT_BALLOT,  
	CD_BIZAREA          = @P_CD_BIZAREA,  
	CD_SALEGRP          = @P_CD_SALEGRP,     
	NO_EMP              = @P_NO_EMP_INV,  
	FG_LC               = @P_FG_LC,  
	CD_PARTNER          = @P_CD_PARTNER,     
	CD_EXCH             = @P_CD_EXCH,  
	--AM_EX               = @P_AM_EX,    --20100204 뺌
	DT_LOADING          = @P_DT_LOADING,     
	CD_ORIGIN           = @P_CD_ORIGIN,  
	CD_AGENT            = @P_CD_AGENT,  
	CD_EXPORT           = @P_CD_EXPORT,     
	CD_PRODUCT          = @P_CD_PRODUCT,  
	SHIP_CORP           = @P_SHIP_CORP,  
	NM_VESSEL           = @P_NM_VESSEL,     
	COND_TRANS          = @P_COND_TRANS,  
	TP_TRANSPORT        = @P_TP_TRANSPORT,  
	TP_TRANS        = @P_TP_TRANS,     
	TP_PACKING          = @P_TP_PACKING,  
	CD_WEIGHT           = @P_CD_WEIGHT,  
	GROSS_WEIGHT        = @P_GROSS_WEIGHT,     
	NET_WEIGHT          = @P_NET_WEIGHT,  
	PORT_LOADING        = @P_PORT_LOADING,  
	PORT_ARRIVER        = @P_PORT_ARRIVER,     
	DESTINATION         = @P_DESTINATION,  
	NO_SCT              = @P_NO_SCT,  
	NO_ECT              = @P_NO_ECT,     
	CD_NOTIFY           = @P_CD_NOTIFY,  
	DT_TO               = @P_DT_TO,  
	NO_LC               = @P_NO_LC,     
	NO_SO               = @P_NO_SO,  
	REMARK1             = @P_REMARK1,  
	REMARK2             = @P_REMARK2,     
	REMARK3             = @P_REMARK3,  
	REMARK4             = @P_REMARK4,  
	REMARK5             = @P_REMARK5,     
	DTS_UPDATE          = @P_DTS_UPDATE,  
	ID_UPDATE           = @P_ID_UPDATE,
	NM_NOTIFY           = @P_NM_NOTIFY,              --착하통지처
	ADDR1_NOTIFY        = @P_ADDR1_NOTIFY,           --통지처주소1
	ADDR2_NOTIFY        = @P_ADDR2_NOTIFY,           --통지처주소2
	CD_CONSIGNEE        = @P_CD_CONSIGNEE,           --수하인
	NM_CONSIGNEE        = @P_NM_CONSIGNEE,           --수하인명
	ADDR1_CONSIGNEE     = @P_ADDR1_CONSIGNEE,        --수하인주소1
	ADDR2_CONSIGNEE     = @P_ADDR2_CONSIGNEE,        --수하인주소2
	REMARK              = @P_REMARK,
	NM_PARTNER          = @P_NM_PARTNER,
	ADDR1_PARTNER       = @P_ADDR1_PARTNER,
	ADDR2_PARTNER       = @P_ADDR2_PARTNER,
	NM_EXPORT           = @P_NM_EXPORT,
	ADDR1_EXPORT        = @P_ADDR1_EXPORT,        
	ADDR2_EXPORT        = @P_ADDR2_EXPORT,
	COND_PRICE          = @P_COND_PRICE,        
    DESCRIPTION         = @P_DESCRIPTION,
    GROSS_VOLUME        = @P_GROSS_VOLUME, 
    FG_FREIGHT          = @P_FG_FREIGHT, 
    AM_FREIGHT          = @P_AM_FREIGHT,
    
    YN_RETURN           = @P_YN_RETURN,
    DT_SAILING_ON       = @P_DT_SAILING_ON,
    TXT_REMARK2         = @P_TXT_REMARK2,
    CD_BANK				= @P_CD_BANK,
    COND_PAY			= @P_COND_PAY,
	ARRIVER_COUNTRY     = @P_ARRIVER_COUNTRY,
    YN_INSURANCE        = @P_YN_INSURANCE
WHERE NO_INV   = @P_NO_INV    
AND CD_COMPANY = @P_CD_COMPANY        
END
GO

