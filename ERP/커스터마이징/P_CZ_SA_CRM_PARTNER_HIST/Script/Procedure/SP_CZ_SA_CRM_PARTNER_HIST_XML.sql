USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_CRM_PARTNER_HIST_XML]    Script Date: 2016-07-04 오후 4:11:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_SA_CRM_PARTNER_HIST_XML]
(
	@P_XML			XML, 
	@P_ID_USER		NVARCHAR(10), 
    @DOC			INT = NULL

) AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML 

-- ================================================== DELETE
DELETE A 
FROM CZ_SA_CRM_PARTNER_HIST A 
JOIN OPENXML (@DOC, '/XML/D', 2) 
        WITH (CD_COMPANY	NVARCHAR(7),
			  SEQ			INT) B 
ON A.CD_COMPANY = B.CD_COMPANY
AND A.SEQ = B.SEQ
-- ================================================== INSERT
INSERT INTO CZ_SA_CRM_PARTNER_HIST
(
	CD_COMPANY,
	SEQ,
	CD_PARTNER,
	DT_HIST,
	NO_EMP_SALES,
	NO_EMP_SALES1,
	NO_EMP_SALES2,
	NO_EMP_SALES3,
	NO_EMP_LOG,
	DC_ADDRESS1,
	DC_ADDRESS2,
	DC_ADDRESS3,
	DC_POTAL_ADDR,
	DC_POTAL_ID,
	DC_POTAL_PW,
	TP_ACK_SEND,
	YN_DUEDATE,
	DC_MARGIN_ENGINE,
	DC_MARGIN_SUPPLIER,
	DC_COMPETE,
	DC_INQ,
	DC_QTN,
	DC_QTN_SEND,
	DC_ACK,
	DC_DELIVERY,
	DC_INVOICE,
	DC_OUTSTANDING,
	DC_RMK,
	DC_GS,
	DC_PV,
	DC_NB,
	ID_INSERT,
	DTS_INSERT
)
SELECT CD_COMPANY,
	   SEQ,
	   CD_PARTNER,
	   DT_HIST,
	   NO_EMP_SALES,
	   NO_EMP_SALES1,
	   NO_EMP_SALES2,
	   NO_EMP_SALES3,
	   NO_EMP_LOG,
	   REPLACE(DC_ADDRESS1, CHAR(10), CHAR(13) + CHAR(10)) AS DC_ADDRESS1,
	   REPLACE(DC_ADDRESS2, CHAR(10), CHAR(13) + CHAR(10)) AS DC_ADDRESS2,
	   REPLACE(DC_ADDRESS3, CHAR(10), CHAR(13) + CHAR(10)) AS DC_ADDRESS3,
	   DC_POTAL_ADDR,
	   DC_POTAL_ID,
	   DC_POTAL_PW,
	   TP_ACK_SEND,
	   YN_DUEDATE,
	   REPLACE(DC_MARGIN_ENGINE, CHAR(10), CHAR(13) + CHAR(10)) AS DC_MARGIN_ENGINE,
	   REPLACE(DC_MARGIN_SUPPLIER, CHAR(10), CHAR(13) + CHAR(10)) AS DC_MARGIN_SUPPLIER,
	   REPLACE(DC_COMPETE, CHAR(10), CHAR(13) + CHAR(10)) AS DC_COMPETE,
	   REPLACE(DC_INQ, CHAR(10), CHAR(13) + CHAR(10)) AS DC_INQ,
	   REPLACE(DC_QTN, CHAR(10), CHAR(13) + CHAR(10)) AS DC_QTN,
	   REPLACE(DC_QTN_SEND, CHAR(10), CHAR(13) + CHAR(10)) AS DC_QTN_SEND,
	   REPLACE(DC_ACK, CHAR(10), CHAR(13) + CHAR(10)) AS DC_ACK,
	   REPLACE(DC_DELIVERY, CHAR(10), CHAR(13) + CHAR(10)) AS DC_DELIVERY,
	   REPLACE(DC_INVOICE, CHAR(10), CHAR(13) + CHAR(10)) AS DC_INVOICE,
	   REPLACE(DC_OUTSTANDING, CHAR(10), CHAR(13) + CHAR(10)) AS DC_OUTSTANDING,
	   REPLACE(DC_RMK, CHAR(10), CHAR(13) + CHAR(10)) AS DC_RMK,
	   REPLACE(DC_GS, CHAR(10), CHAR(13) + CHAR(10)) AS DC_GS,
	   REPLACE(DC_PV, CHAR(10), CHAR(13) + CHAR(10)) AS DC_PV,
	   REPLACE(DC_NB, CHAR(10), CHAR(13) + CHAR(10)) AS DC_NB,
	   @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (CD_COMPANY 				NVARCHAR(7),
				SEQ						INT,
				CD_PARTNER 				NVARCHAR(20),
				DT_HIST					NVARCHAR(8),
				NO_EMP_SALES 			NVARCHAR(20),
				NO_EMP_SALES1 			NVARCHAR(20),
				NO_EMP_SALES2 			NVARCHAR(20),
				NO_EMP_SALES3 			NVARCHAR(20),
				NO_EMP_LOG 				NVARCHAR(20),
				DC_ADDRESS1 			NVARCHAR(500),
				DC_ADDRESS2 			NVARCHAR(500),
				DC_ADDRESS3 			NVARCHAR(500),
				DC_POTAL_ADDR 			NVARCHAR(200),
				DC_POTAL_ID 			NVARCHAR(50),
				DC_POTAL_PW 			NVARCHAR(50),
				TP_ACK_SEND 			NVARCHAR(4),
				YN_DUEDATE 				NVARCHAR(1),
				DC_MARGIN_ENGINE 		NVARCHAR(300),
				DC_MARGIN_SUPPLIER 		NVARCHAR(300),
				DC_COMPETE 				NVARCHAR(300),
				DC_INQ 					NVARCHAR(500),
				DC_QTN 					NVARCHAR(500),
				DC_QTN_SEND 			NVARCHAR(500),
				DC_ACK 					NVARCHAR(500),
				DC_DELIVERY 			NVARCHAR(1000),
				DC_INVOICE 				NVARCHAR(500),
				DC_OUTSTANDING 			NVARCHAR(500),
				DC_RMK 					NVARCHAR(500),
				DC_GS 					NVARCHAR(500),
				DC_PV 					NVARCHAR(500),
				DC_NB 					NVARCHAR(500))
-- ================================================== UPDATE
UPDATE A
	SET A.CD_PARTNER = B.CD_PARTNER,
	    A.DT_HIST = B.DT_HIST,
	    A.NO_EMP_SALES = B.NO_EMP_SALES,
		A.NO_EMP_SALES1 = B.NO_EMP_SALES1,
		A.NO_EMP_SALES2 = B.NO_EMP_SALES2,
		A.NO_EMP_SALES3 = B.NO_EMP_SALES3,
	    A.NO_EMP_LOG = B.NO_EMP_LOG,
	    A.DC_ADDRESS1 = REPLACE(B.DC_ADDRESS1, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_ADDRESS2 = REPLACE(B.DC_ADDRESS2, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_ADDRESS3 = REPLACE(B.DC_ADDRESS3, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_POTAL_ADDR = B.DC_POTAL_ADDR,
	    A.DC_POTAL_ID = B.DC_POTAL_ID,
	    A.DC_POTAL_PW = B.DC_POTAL_PW,
	    A.TP_ACK_SEND = B.TP_ACK_SEND,
	    A.YN_DUEDATE = B.YN_DUEDATE,
	    A.DC_MARGIN_ENGINE = REPLACE(B.DC_MARGIN_ENGINE, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_MARGIN_SUPPLIER = REPLACE(B.DC_MARGIN_SUPPLIER, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_COMPETE = REPLACE(B.DC_COMPETE, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_INQ = REPLACE(B.DC_INQ, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_QTN = REPLACE(B.DC_QTN, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_QTN_SEND = REPLACE(B.DC_QTN_SEND, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_ACK = REPLACE(B.DC_ACK, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_DELIVERY = REPLACE(B.DC_DELIVERY, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_INVOICE = REPLACE(B.DC_INVOICE, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_OUTSTANDING = REPLACE(B.DC_OUTSTANDING, CHAR(10), CHAR(13) + CHAR(10)),
	    A.DC_RMK = REPLACE(B.DC_RMK, CHAR(10), CHAR(13) + CHAR(10)),
		A.DC_GS = REPLACE(B.DC_GS, CHAR(10), CHAR(13) + CHAR(10)),
		A.DC_PV = REPLACE(B.DC_PV, CHAR(10), CHAR(13) + CHAR(10)),
		A.DC_NB = REPLACE(B.DC_NB, CHAR(10), CHAR(13) + CHAR(10)),
		A.ID_UPDATE = @P_ID_USER, 
	    A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
	FROM CZ_SA_CRM_PARTNER_HIST A
	JOIN OPENXML (@DOC, '/XML/U', 2) 
			WITH (CD_COMPANY 				NVARCHAR(7),
				  SEQ						INT,
				  CD_PARTNER 				NVARCHAR(20),
				  DT_HIST					NVARCHAR(8),
				  NO_EMP_SALES 			    NVARCHAR(20),
				  NO_EMP_SALES1 			NVARCHAR(20),
				  NO_EMP_SALES2 			NVARCHAR(20),
				  NO_EMP_SALES3 			NVARCHAR(20),
				  NO_EMP_LOG 				NVARCHAR(20),
				  DC_ADDRESS1 				NVARCHAR(500),
				  DC_ADDRESS2 				NVARCHAR(500),
				  DC_ADDRESS3 				NVARCHAR(500),
				  DC_POTAL_ADDR 			NVARCHAR(200),
				  DC_POTAL_ID 				NVARCHAR(50),
				  DC_POTAL_PW 				NVARCHAR(50),
				  TP_ACK_SEND 				NVARCHAR(4),
				  YN_DUEDATE 				NVARCHAR(1),
				  DC_MARGIN_ENGINE 		    NVARCHAR(300),
				  DC_MARGIN_SUPPLIER 		NVARCHAR(300),
				  DC_COMPETE 				NVARCHAR(300),
				  DC_INQ 					NVARCHAR(500),
				  DC_QTN 					NVARCHAR(500),
				  DC_QTN_SEND 				NVARCHAR(500),
				  DC_ACK 					NVARCHAR(500),
				  DC_DELIVERY 				NVARCHAR(1000),
				  DC_INVOICE 				NVARCHAR(500),
				  DC_OUTSTANDING 			NVARCHAR(500),
				  DC_RMK 					NVARCHAR(500),
				  DC_GS 					NVARCHAR(500),
				  DC_PV 					NVARCHAR(500),
				  DC_NB 					NVARCHAR(500)) B
    ON A.CD_COMPANY = B.CD_COMPANY
	AND A.SEQ = B.SEQ

EXEC SP_XML_REMOVEDOCUMENT @DOC 

GO
