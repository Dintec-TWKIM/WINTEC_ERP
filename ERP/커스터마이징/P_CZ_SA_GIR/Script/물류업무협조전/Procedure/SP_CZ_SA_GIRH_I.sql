USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIRH_I]    Script Date: 2015-04-28 오전 11:10:26 ******/
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
ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIRH_I]        
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
	@P_DC_RMK4			   NVARCHAR(MAX),	--포장비고
	@P_DC_RMK5			   NVARCHAR(MAX),	--PICKING비고
    @P_YN_RETURN           NVARCHAR(1),	    --반품여부 : 'N'정상, 'Y'반품
	@P_MAIN_CATEGORY	   NVARCHAR(3),
	@P_SUB_CATEGORY		   NVARCHAR(3),
	@P_YN_PACKING		   NVARCHAR(1),
	@P_YN_TAX_RETURN	   NVARCHAR(1),
	@P_CD_DELIVERY_TO	   NVARCHAR(20),
	@P_SEQ_DELIVERY_PIC	   NVARCHAR(5),
	@P_CD_FREIGHT		   NVARCHAR(3),
	@P_CD_FORWARDER		   NVARCHAR(20),
	@P_WEIGHT			   NUMERIC(17,4),
	@P_DT_START			   NVARCHAR(8),
	@P_DT_COMPLETE		   NVARCHAR(8),
	@P_YN_AUTO_SUBMIT	   NVARCHAR(1),
	@P_DT_AUTO_COMPLETE	   INT,
	@P_DT_BILL			   NVARCHAR(8),
	@P_FG_REASON		   NVARCHAR(1),
	@P_NM_FORWARDER_A	   NVARCHAR(50),
	@P_AM_FORWARDER_A	   NUMERIC(14,2),
	@P_NM_FORWARDER_B	   NVARCHAR(50),
	@P_AM_FORWARDER_B	   NUMERIC(14,2),
	@P_NM_FORWARDER_C	   NVARCHAR(50),
	@P_AM_FORWARDER_C	   NUMERIC(14,2),
	@P_NO_IMO			   NVARCHAR(10),
	@P_NO_IMO_BILL		   NVARCHAR(10),
	@P_TP_CHARGE		   NVARCHAR(4),
	@P_NM_DELIVERY		   NVARCHAR(100),
	@P_DC_DELIVERY_ADDR	   NVARCHAR(200),
	@P_DC_DELIVERY_TEL	   NVARCHAR(100),
	@P_DC_DESTINATION	   NVARCHAR(50),
	@P_YN_REVIEW		   NVARCHAR(1),
	@P_YN_SHIPPING		   NVARCHAR(1),
	@P_YN_CI			   NVARCHAR(1),
	@P_YN_RECEIPT		   NVARCHAR(1),
	@P_YN_REFUND		   NVARCHAR(1),
	@P_NO_GIR_SG		   NVARCHAR(20),
	@P_YN_LOADING		   NVARCHAR(1),
	@P_DC_RMK_ADD		   NVARCHAR(500),
	@P_YN_DOMESTIC		   NVARCHAR(1),
	@P_CD_SHIP_LOCATION    NVARCHAR(4),
	@P_YN_PRE_PHOTO		   NVARCHAR(1),
	@P_YN_POST_PHOTO	   NVARCHAR(1),
	@P_DC_PHOTO			   NVARCHAR(100),
	@P_YN_SPEC_CHECK	   NVARCHAR(1),
	@P_DC_SPEC			   NVARCHAR(100),
	@P_YN_SEPARATE_PACK	   NVARCHAR(1),
	@P_DC_SEPARATE	       NVARCHAR(100),
	@P_YN_CERT			   NVARCHAR(1),
	@P_YN_UNIDENTIFIABLE   NVARCHAR(1),
	@P_NO_FILE			   NVARCHAR(20),
	@P_TM_DELIVERY		   NVARCHAR(3),
	@P_DC_RMK_PACK		   NVARCHAR(500),
	@P_CD_PORT			   NVARCHAR(4),
	@P_DTS_ETB			   NVARCHAR(11),
	@P_DTS_ETD			   NVARCHAR(11),
	@P_DC_AGENCY		   NVARCHAR(50),
	@P_DC_RMK_SHIP		   NVARCHAR(200),
	@P_YN_UNPACK		   NVARCHAR(1),
	@P_DC_UNPACK		   NVARCHAR(100),
	@P_DTS_DEADLINE		   NVARCHAR(11),
    @P_YN_ETC			   NVARCHAR(1),
    @P_YN_EDIT			   NVARCHAR(1),
	@P_DC_VESSEL		   NVARCHAR(100),
	@P_DC_SHIP_CRANE	   NVARCHAR(100),
    @P_ID_INSERT           NVARCHAR(15)		--사용자
)   
AS

INSERT INTO SA_GIRH  
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

INSERT INTO CZ_SA_GIRH_WORK_DETAIL  
(
	CD_COMPANY,
	NO_GIR,
	CD_MAIN_CATEGORY,
	CD_SUB_CATEGORY,
	YN_PACKING,
	YN_TAX_RETURN,
	CD_DELIVERY_TO,
	SEQ_DELIVERY_PIC,
	CD_FREIGHT,
	CD_FORWARDER,
	WEIGHT,
	DT_START,
	DT_COMPLETE,
	YN_AUTO_SUBMIT,
	DT_AUTO_COMPLETE,
	DT_BILL,
	FG_REASON,
	NO_IMO,
	NO_IMO_BILL,
	CD_RMK,
	TP_CHARGE,
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
	@P_MAIN_CATEGORY,
	@P_SUB_CATEGORY,
	@P_YN_PACKING,
	@P_YN_TAX_RETURN,
	@P_CD_DELIVERY_TO,
	@P_SEQ_DELIVERY_PIC,
	@P_CD_FREIGHT,
    @P_CD_FORWARDER,
	@P_WEIGHT,		     
	@P_DT_START,
	@P_DT_COMPLETE,
	@P_YN_AUTO_SUBMIT,
	@P_DT_AUTO_COMPLETE,
	@P_DT_BILL,
	@P_FG_REASON,
	@P_NO_IMO,
	@P_NO_IMO_BILL,
	@P_CD_RMK,
	@P_TP_CHARGE,
	@P_DC_RMK,
	@P_DC_RMK1,
    @P_DC_RMK2,
	@P_DC_RMK3,
	@P_DC_RMK4,
	@P_DC_RMK5,
	@P_ID_INSERT,
    NEOE.SF_SYSDATE(GETDATE()) 
)

INSERT INTO CZ_SA_GIRH_REMARK
(
	CD_COMPANY,
	NO_GIR,
	NM_DELIVERY,
	DC_DELIVERY_ADDR,
	DC_DELIVERY_TEL,
	DC_DESTINATION,
	YN_REVIEW,
	YN_SHIPPING,
	YN_CI,
	YN_RECEIPT,
	YN_REFUND,
	NO_GIR_SG,
	YN_LOADING,
	DC_RMK_ADD,
	YN_DOMESTIC,
	CD_SHIP_LOCATION,
	YN_PRE_PHOTO,
	YN_POST_PHOTO,
	DC_PHOTO,
	YN_SPEC_CHECK,
	DC_SPEC,
	YN_SEPARATE_PACK,
	DC_SEPARATE,
	YN_CERT,
	YN_UNIDENTIFIABLE,
	NO_FILE,
	TM_DELIVERY,
	DC_RMK_PACK,
	CD_PORT,
	DTS_ETB,
	DTS_ETD,
	DC_AGENCY,
	DC_RMK_SHIP,
	YN_UNPACK,
	DC_UNPACK,
	DTS_DEADLINE,
    YN_ETC,
    YN_EDIT,
	DC_VESSEL,
	DC_SHIP_CRANE,
	ID_INSERT,
	DTS_INSERT
)
VALUES
(
	@P_CD_COMPANY,
	@P_NO_GIR,
	@P_NM_DELIVERY,
	@P_DC_DELIVERY_ADDR,
	@P_DC_DELIVERY_TEL,
	@P_DC_DESTINATION,
	@P_YN_REVIEW,
	@P_YN_SHIPPING,
	@P_YN_CI,
	@P_YN_RECEIPT,
	@P_YN_REFUND,
	@P_NO_GIR_SG,
	@P_YN_LOADING,
	@P_DC_RMK_ADD,
	@P_YN_DOMESTIC,
	@P_CD_SHIP_LOCATION,
	@P_YN_PRE_PHOTO,
	@P_YN_POST_PHOTO,
	@P_DC_PHOTO,
	@P_YN_SPEC_CHECK,
	@P_DC_SPEC,
	@P_YN_SEPARATE_PACK,
	@P_DC_SEPARATE,
	@P_YN_CERT,
	@P_YN_UNIDENTIFIABLE,
	@P_NO_FILE,
	@P_TM_DELIVERY,
	@P_DC_RMK_PACK,
	@P_CD_PORT,
	@P_DTS_ETB,
	@P_DTS_ETD,
	@P_DC_AGENCY,
	@P_DC_RMK_SHIP,
	@P_YN_UNPACK,
	@P_DC_UNPACK,
	@P_DTS_DEADLINE,
    @P_YN_ETC,
    @P_YN_EDIT,
	@P_DC_VESSEL,
	@P_DC_SHIP_CRANE,
	@P_ID_INSERT,
    NEOE.SF_SYSDATE(GETDATE()) 
)

IF (@P_MAIN_CATEGORY = '003' AND (@P_SUB_CATEGORY = '001' OR @P_SUB_CATEGORY = '002'))
BEGIN
	INSERT INTO CZ_SA_GIRH_FORWARDER 
	(
		CD_COMPANY,
		NO_GIR,
		CD_FORWARDER,
		NM_FORWARDER,
		AM_PRICE,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES 
	(
		@P_CD_COMPANY,
		@P_NO_GIR,
		'001',
		@P_NM_FORWARDER_A,
		@P_AM_FORWARDER_A,
		@P_ID_INSERT,
		NEOE.SF_SYSDATE(GETDATE())
	)

	INSERT INTO CZ_SA_GIRH_FORWARDER 
	(
		CD_COMPANY,
		NO_GIR,
		CD_FORWARDER,
		NM_FORWARDER,
		AM_PRICE,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES 
	(
		@P_CD_COMPANY,
		@P_NO_GIR,
		'002',
		@P_NM_FORWARDER_B,
		@P_AM_FORWARDER_B,
		@P_ID_INSERT,
		NEOE.SF_SYSDATE(GETDATE())
	)

	INSERT INTO CZ_SA_GIRH_FORWARDER 
	(
		CD_COMPANY,
		NO_GIR,
		CD_FORWARDER,
		NM_FORWARDER,
		AM_PRICE,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES 
	(
		@P_CD_COMPANY,
		@P_NO_GIR,
		'004',
		@P_NM_FORWARDER_C,
		@P_AM_FORWARDER_C,
		@P_ID_INSERT,
		NEOE.SF_SYSDATE(GETDATE())
	)
END
ELSE
BEGIN
	UPDATE CZ_SA_GIRH_WORK_DETAIL
	SET CD_FORWARDER = NULL,
		FG_REASON = NULL,
		WEIGHT = NULL
	WHERE CD_COMPANY = @P_CD_COMPANY
	AND	NO_GIR = @P_NO_GIR
END

GO

