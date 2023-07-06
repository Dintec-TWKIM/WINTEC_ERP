USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIRH_PACK_I]    Script Date: 2015-06-04 오전 8:44:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System : 영업
**  Sub System : 출고의뢰관리
**  Page  : 고객납품의뢰등록
**  Desc  : 고객납품의뢰 헤더 저장
**
**  Return Values
**
**  작    성    자  : 
**  작    성    일         : 
**  수    정    자  : 허성철
*********************************************
** Change History
*********************************************
*********************************************/
ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIRH_PACK_I]        
(  
    @P_CD_COMPANY          NVARCHAR(7),		--회사
    @P_NO_GIR              NVARCHAR(20),	--의뢰번호
    @P_DT_GIR              NVARCHAR(8),		--의뢰일자
    @P_CD_PARTNER          NVARCHAR(20),    --매출처
	@P_CD_PLANT            NVARCHAR(7),		--공장
    @P_NO_EMP              NVARCHAR(20),	--사번
    @P_TP_BUSI             NVARCHAR(3),		--거래구분
	@P_CD_RMK              NVARCHAR(3),		--협조내용
    @P_DC_RMK              NVARCHAR(MAX),	--상세요청
	@P_DC_RMK1             NVARCHAR(MAX),	--취소사유
	@P_DC_RMK2             NVARCHAR(MAX),	--매출비고
	@P_DC_RMK3             NVARCHAR(MAX),	--기포장정보
	@P_DC_RMK4			   NVARCHAR(MAX),   --빈값
	@P_DC_RMK5			   NVARCHAR(MAX),   --PICKING비고
    @P_YN_RETURN           NVARCHAR(1),	    --반품여부 : 'N'정상, 'Y'반품
	@P_PACK_CATEGORY	   NVARCHAR(3),
	@P_SUB_CATEGORY		   NVARCHAR(3),
	@P_YN_PACKING		   NVARCHAR(1),
	@P_CD_COLLECT_FROM	   NVARCHAR(20),
	@P_SEQ_COLLECT_PIC	   NVARCHAR(5),
	@P_DT_START			   NVARCHAR(8),
	@P_DT_COMPLETE		   NVARCHAR(8),
	@P_NO_IMO			   NVARCHAR(10),
    @P_ID_INSERT           NVARCHAR(15)		--사용자
)   
AS

INSERT INTO CZ_SA_GIRH_PACK  
(
	CD_COMPANY,         
	NO_GIR,             
	DT_GIR,             
	CD_PARTNER,         
	CD_PLANT,
	NO_EMP,				
	TP_BUSI,            
	YN_RETURN,          
	ID_INSERT,          
	DTS_INSERT
)  
VALUES
(
	@P_CD_COMPANY,      
	@P_NO_GIR,          
	@P_DT_GIR,          
	@P_CD_PARTNER,      
	@P_CD_PLANT,
	@P_NO_EMP,			
	@P_TP_BUSI,         
	@P_YN_RETURN,       
	@P_ID_INSERT,       
	NEOE.SF_SYSDATE(GETDATE()) 
)

INSERT INTO CZ_SA_GIRH_PACK_DETAIL  
(
	CD_COMPANY,         
	NO_GIR,             
	CD_PACK_CATEGORY,   
	CD_SUB_CATEGORY,	
	YN_PACKING,
	CD_COLLECT_FROM,	
	SEQ_COLLECT_PIC,	
	DT_START,           
	DT_COMPLETE,		
	NO_IMO,
	CD_RMK,				
	DC_RMK,             
	DC_RMK1,			
	DC_RMK2,
	DC_RMK3,
	DC_RMK4,
	DC_RMK5,
	ID_INSERT,			
	DTS_INSERT
)  
VALUES
(
	@P_CD_COMPANY,      
	@P_NO_GIR,          
	@P_PACK_CATEGORY,   
	@P_SUB_CATEGORY,	
	@P_YN_PACKING,
	@P_CD_COLLECT_FROM,	
	@P_SEQ_COLLECT_PIC, 
	@P_DT_START,        
	@P_DT_START,		
	@P_NO_IMO,
	@P_CD_RMK,		    
	@P_DC_RMK,          
	@P_DC_RMK1,			
	@P_DC_RMK2,
	@P_DC_RMK3,
	@P_DC_RMK4,
	@P_DC_RMK5,
	@P_ID_INSERT,		
	NEOE.SF_SYSDATE(GETDATE()) 
)
GO

