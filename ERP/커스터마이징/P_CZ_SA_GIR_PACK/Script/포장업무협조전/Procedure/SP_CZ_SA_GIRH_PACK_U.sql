USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIRH_PACK_U]    Script Date: 2015-06-04 오전 8:45:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************
**  System : 영업
**  Sub System : 출고의뢰관리        
**  Page  : 고객납품의뢰등록
**  Desc  : 고객납품의뢰 헤더 수정
**
**  Return Values
**
**  작    성    자  : 
**  작    성    일 : 
**  수    정    자     : 허성철
*********************************************
** Change History
*********************************************
*********************************************/
ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIRH_PACK_U]
(  
	@P_CD_COMPANY       NVARCHAR(7),                
	@P_NO_GIR			NVARCHAR(20),		--의뢰번호
	@P_DT_GIR           NVARCHAR(8),		--의뢰일자
	@P_NO_EMP           NVARCHAR(10),		--사번
	@P_PACK_CATEGORY	NVARCHAR(3),
	@P_SUB_CATEGORY		NVARCHAR(3),
	@P_YN_PACKING		NVARCHAR(1),
	@P_COLLECT_FROM		NVARCHAR(20),
	@P_COLLECT_PIC		NVARCHAR(5),
	@P_DT_START			NVARCHAR(8),
	@P_DT_COMPLETE		NVARCHAR(8),
	@P_CD_RMK           NVARCHAR(3),		--협조내용
	@P_DC_RMK           NVARCHAR(MAX),		--상세요청
	@P_DC_RMK1          NVARCHAR(MAX),		--취소사유
	@P_DC_RMK2          NVARCHAR(MAX),		--매출비고
	@P_DC_RMK3          NVARCHAR(MAX),		--기포장정보
	@P_DC_RMK4          NVARCHAR(MAX),		--빈값
	@P_DC_RMK5          NVARCHAR(MAX),		--PICKING 비고
	@P_NO_IMO			NVARCHAR(10),
	@P_ID_UPDATE        NVARCHAR(15)		--아이디
)   
AS  

UPDATE	CZ_SA_GIRH_PACK
SET     DT_GIR				= @P_DT_GIR,   
		NO_EMP				= @P_NO_EMP,
		ID_UPDATE			= @P_ID_UPDATE,
		DTS_UPDATE			= NEOE.SF_SYSDATE(GETDATE())
WHERE   CD_COMPANY			= @P_CD_COMPANY
AND     NO_GIR				= @P_NO_GIR

UPDATE	CZ_SA_GIRH_PACK_DETAIL
SET     CD_PACK_CATEGORY	= @P_PACK_CATEGORY,
		CD_SUB_CATEGORY		= @P_SUB_CATEGORY,
		YN_PACKING			= @P_YN_PACKING,
		CD_COLLECT_FROM		= @P_COLLECT_FROM,
		SEQ_COLLECT_PIC		= @P_COLLECT_PIC,
		DT_START			= @P_DT_START,
		DT_COMPLETE			= @P_DT_START,
		NO_IMO				= @P_NO_IMO,
		CD_RMK				= @P_CD_RMK,   
		DC_RMK				= @P_DC_RMK,   
		DC_RMK1				= @P_DC_RMK1,
		DC_RMK2				= @P_DC_RMK2,
		DC_RMK3				= @P_DC_RMK3,
		DC_RMK4				= @P_DC_RMK4,
		DC_RMK5				= @P_DC_RMK5,
		ID_UPDATE			= @P_ID_UPDATE,
		DTS_UPDATE			= NEOE.SF_SYSDATE(GETDATE())
WHERE   CD_COMPANY			= @P_CD_COMPANY
AND     NO_GIR				= @P_NO_GIR
GO

