USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_GIRL_XML]    Script Date: 2015-10-23 오후 6:23:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [NEOE].[SP_CZ_SA_GIRL_PACK_XML] 
(
	@P_CD_COMPANY	NVARCHAR(7),
	@P_NO_GIR		NVARCHAR(20),
	@P_NO_INV		NVARCHAR(20),
    @P_XML			XML, 
	@P_ID_USER		NVARCHAR(10), 
    @DOC			INT = NULL
) 
AS 

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_CD_PACK_CATEGORY NVARCHAR(3)
 
EXEC SP_XML_PREPAREDOCUMENT @DOC OUTPUT, @P_XML 

-- ================================================== DELETE
DELETE A 
  FROM CZ_SA_GIRL_PACK A 
       JOIN OPENXML (@DOC, '/XML/D', 2) 
               WITH (SEQ_GIR NUMERIC(5, 0)) B 
         ON A.CD_COMPANY = @P_CD_COMPANY 
		 AND A.NO_GIR = @P_NO_GIR
		 AND A.SEQ_GIR = B.SEQ_GIR
DELETE A 
  FROM CZ_TR_INVL A 
       JOIN OPENXML (@DOC, '/XML/D', 2) 
               WITH (NO_LINE	NUMERIC(4, 0)) B 
         ON A.CD_COMPANY = @P_CD_COMPANY 
		 AND A.NO_INV = @P_NO_INV
		 AND A.NO_LINE = B.NO_LINE
-- ================================================== INSERT
SELECT @V_CD_PACK_CATEGORY = CD_PACK_CATEGORY
FROM CZ_SA_GIRH_PACK_DETAIL
WHERE CD_COMPANY = @P_CD_COMPANY
AND NO_GIR = @P_NO_GIR

INSERT INTO CZ_SA_GIRL_PACK 
(
	CD_COMPANY,
	NO_GIR,
	SEQ_GIR,     
	CD_ITEM,		
	TP_ITEM,
	DT_DUEDATE,  
	DT_REQGI,   
	YN_INSPECT,  
	CD_SL,		
	TP_GI,
	QT_GIR,
	QT_GIR_STOCK,    
	CD_EXCH,    
	UM,          
	AM_GIR,		
	AM_GIRAMT,
	AM_VAT,      
	UNIT,       
	QT_GIR_IM,   
	GI_PARTNER,	
	NO_PROJECT,
	RT_EXCH,     
	RT_VAT,     
	NO_SO,       
	SEQ_SO,		
	CD_SALEGRP,
	TP_VAT,      
	NO_EMP,     
	TP_IV,       
	FG_TAXP,		
	TP_BUSI,
	GIR,         
	IV,         
	RET,         
	SUBCONT,		
	NO_LC,
	SEQ_LC,      
	FG_LC_OPEN, 
	DC_RMK,      
	CD_WH,       
	NO_PMS,     
	SEQ_PROJECT, 
	YN_PICKING,	
	CD_USERDEF1,
	NO_INV,      
	TP_UM_TAX,  
	UMVAT_GIR,
	YN_ADD_STOCK,
	ID_INSERT,	
	DTS_INSERT
)
SELECT @P_CD_COMPANY, 
       @P_NO_GIR,
	   SEQ_GIR,     
	   CD_ITEM,		
	   TP_ITEM,
	   DT_DUEDATE,  
	   DT_REQGI,   
	   YN_INSPECT,  
	   CD_SL,		
	   TP_GI,
	   QT_GIR,
	   QT_GIR_STOCK,    
	   CD_EXCH,    
	   UM,          
	   AM_GIR,		
	   AM_GIRAMT,
	   AM_VAT,      
	   UNIT,       
	   QT_GIR_IM,   
	   GI_PARTNER,	
	   NO_PROJECT,
	   RT_EXCH,     
	   RT_VAT,     
	   NO_SO,       
	   SEQ_SO,		
	   CD_SALEGRP,
	   TP_VAT,      
	   NO_EMP,     
	   TP_IV,       
	   FG_TAXP,		
	   TP_BUSI,
	   GIR,         
	   IV,         
	   'N',         
	   'N',		
	   NO_LC,
	   SEQ_LC,      
	   FG_LC_OPEN, 
	   DC_RMK,      
	   CD_WH,       
	   NO_PMS,     
	   SEQ_PROJECT, 
	   YN_PICKING,	
	   CD_USERDEF1,
	   @P_NO_INV,      
	   TP_UM_TAX,  
	   UMVAT_GIR,
	   YN_ADD_STOCK,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()) 
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (SEQ_GIR			 NUMERIC(5,0),		
				CD_ITEM          NVARCHAR(20),       
				TP_ITEM          NVARCHAR(3),        
				DT_DUEDATE       NVARCHAR(8),        
				DT_REQGI         NVARCHAR(8),        
				YN_INSPECT       NCHAR(1),           
				CD_SL            NVARCHAR(7),        
				TP_GI            NVARCHAR(3),        
				QT_GIR           NUMERIC(17, 4),     
				QT_GIR_STOCK     NUMERIC(17, 4),        
				CD_EXCH          NVARCHAR(3),        
				UM               NUMERIC(15, 4),     
				AM_GIR           NUMERIC(17, 4),     
				AM_GIRAMT        NUMERIC(17, 4),     
				AM_VAT           NUMERIC(17, 4),     
				UNIT             NVARCHAR(3),        
				QT_GIR_IM        NUMERIC(17, 4),     
				GI_PARTNER       NVARCHAR(20),       
				NO_PROJECT       NVARCHAR(20),       
				RT_EXCH          NUMERIC(15, 4),     
				RT_VAT           NUMERIC(5, 2),      
				NO_SO            NVARCHAR(20),       
				SEQ_SO           NUMERIC(5,0),       
				CD_SALEGRP       NVARCHAR(7),        
				TP_VAT           NVARCHAR(3),        
				NO_EMP           NVARCHAR(20),       
				TP_IV            NVARCHAR(3),        
				FG_TAXP          NVARCHAR(3),        
				TP_BUSI          NVARCHAR(3),        
				NO_LC            NVARCHAR(20),       
				SEQ_LC           NUMERIC(3),         
				FG_LC_OPEN       NVARCHAR(3),        
				DC_RMK           NVARCHAR(100),
				GIR              NVARCHAR(1),
				IV               NVARCHAR(1),
				ID_INSERT        NVARCHAR(15),       
				CD_WH			 NVARCHAR(7), 
				NO_PMS           NVARCHAR(20),
				SEQ_PROJECT	     NUMERIC(5,0),   
				YN_PICKING	     NVARCHAR(1),
				CD_USERDEF1	     NVARCHAR(4),
				TP_UM_TAX        NVARCHAR(3),  
				UMVAT_GIR        NUMERIC(15, 4),
				YN_ADD_STOCK	 NVARCHAR(1))  

INSERT INTO CZ_TR_INVL 
(
	CD_COMPANY,
	NO_INV,
	NO_LINE,
	CD_PLANT,
	CD_ITEM,
	DT_DELIVERY,
	QT_INVENT,
	UM_INVENT,
	AM_INVENT,
	QT_SO,
	UM_SO,
	AM_EXSO,
	NO_PO,
	NO_LINE_PO,
	ID_INSERT,
	DTS_INSERT,
	YN_PRINT
) 
SELECT @P_CD_COMPANY, 
	   @P_NO_INV,
	   NO_LINE,
	   CD_PLANT,
	   CD_ITEM,
	   DT_DUEDATE,
	   QT_GIR_IM,
	   UM,
	   AM_GIR,
	   QT_GIR,
	   UM,
	   AM_GIR,
	   NO_SO,
	   SEQ_SO,
       @P_ID_USER, 
       NEOE.SF_SYSDATE(GETDATE()),
	   'Y'
  FROM OPENXML (@DOC, '/XML/I', 2) 
          WITH (NO_LINE			NUMERIC(5,0), 
			    CD_PLANT        NVARCHAR(7),  
			    CD_ITEM         NVARCHAR(20), 
			    DT_DUEDATE      NVARCHAR(8),     
			    QT_GIR_IM       NUMERIC(17, 4),
			    UM				NUMERIC(15,4),
			    AM_GIR			NUMERIC(17,4),
			    QT_GIR          NUMERIC(17,4),
			    NO_SO           NVARCHAR(20), 
			    SEQ_SO			NUMERIC(5,0))  
-- ================================================== UPDATE    
UPDATE A 
   SET A.CD_ITEM = B.CD_ITEM,
	   A.TP_ITEM = B.TP_ITEM,
	   A.DT_DUEDATE = B.DT_DUEDATE,
	   A.DT_REQGI = B.DT_REQGI,
	   A.YN_INSPECT = B.YN_INSPECT,
	   A.CD_SL = B.CD_SL,
	   A.TP_GI = B.TP_GI,
	   A.QT_GIR = B.QT_GIR,
	   A.QT_GIR_STOCK = B.QT_GIR_STOCK,
	   A.CD_EXCH = B.CD_EXCH,
	   A.UM = B.UM,
	   A.AM_GIR = B.AM_GIR,
	   A.AM_GIRAMT = B.AM_GIRAMT,
	   A.AM_VAT = B.AM_GIRAMT,
	   A.UNIT = B.UNIT,
	   A.QT_GIR_IM = B.QT_GIR_IM,
	   A.GI_PARTNER = B.GI_PARTNER,
	   A.NO_PROJECT = B.NO_PROJECT,
	   A.RT_EXCH = B.RT_EXCH,
	   A.RT_VAT = B.RT_VAT,
	   A.NO_SO = B.NO_SO,
	   A.SEQ_SO = B.SEQ_SO,
	   A.CD_SALEGRP = B.CD_SALEGRP,
	   A.TP_VAT = B.TP_VAT,
	   A.NO_EMP = B.NO_EMP,
	   A.TP_IV = B.TP_IV,
	   A.FG_TAXP = B.FG_TAXP,
	   A.TP_BUSI = B.TP_BUSI,
	   A.GIR = B.GIR,
	   A.IV = B.IV,
	   A.NO_LC = B.NO_LC,
	   A.SEQ_LC = B.SEQ_LC,
	   A.FG_LC_OPEN = B.FG_LC_OPEN,
	   A.DC_RMK = B.DC_RMK,
	   A.CD_WH = B.CD_WH,
	   A.NO_PMS = B.NO_PMS,
	   A.SEQ_PROJECT = B.SEQ_PROJECT,
	   A.YN_PICKING = B.YN_PICKING,
	   A.CD_USERDEF1 = B.CD_USERDEF1,
	   A.NO_INV = @P_NO_INV,
	   A.TP_UM_TAX = B.TP_UM_TAX,
	   A.UMVAT_GIR = B.UMVAT_GIR,
	   A.YN_ADD_STOCK = B.YN_ADD_STOCK,
	   A.ID_UPDATE = @P_ID_USER,
	   A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
  FROM CZ_SA_GIRL_PACK A 
       JOIN OPENXML (@DOC, '/XML/U', 2) 
               WITH (SEQ_GIR		  NUMERIC(5,0),		
					 CD_ITEM          NVARCHAR(20),       
					 TP_ITEM          NVARCHAR(3),        
					 DT_DUEDATE       NVARCHAR(8),        
					 DT_REQGI         NVARCHAR(8),        
					 YN_INSPECT       NCHAR(1),           
					 CD_SL            NVARCHAR(7),        
					 TP_GI            NVARCHAR(3),        
					 QT_GIR           NUMERIC(17, 4),     
					 QT_GIR_STOCK     NUMERIC(17, 4),        
					 CD_EXCH          NVARCHAR(3),        
					 UM               NUMERIC(15, 4),     
					 AM_GIR           NUMERIC(17, 4),     
					 AM_GIRAMT        NUMERIC(17, 4),     
					 AM_VAT           NUMERIC(17, 4),     
					 UNIT             NVARCHAR(3),        
					 QT_GIR_IM        NUMERIC(17, 4),     
					 GI_PARTNER       NVARCHAR(20),       
					 NO_PROJECT       NVARCHAR(20),       
					 RT_EXCH          NUMERIC(15, 4),     
					 RT_VAT           NUMERIC(5, 2),      
					 NO_SO            NVARCHAR(20),       
					 SEQ_SO           NUMERIC(5,0),       
					 CD_SALEGRP       NVARCHAR(7),        
					 TP_VAT           NVARCHAR(3),        
					 NO_EMP           NVARCHAR(20),       
					 TP_IV            NVARCHAR(3),        
					 FG_TAXP          NVARCHAR(3),        
					 TP_BUSI          NVARCHAR(3),        
					 NO_LC            NVARCHAR(20),       
					 SEQ_LC           NUMERIC(3),         
					 FG_LC_OPEN       NVARCHAR(3),        
					 DC_RMK           NVARCHAR(100),
					 GIR              NVARCHAR(1),
					 IV               NVARCHAR(1),
					 ID_INSERT        NVARCHAR(15),       
					 CD_WH			  NVARCHAR(7), 
					 NO_PMS           NVARCHAR(20),
					 SEQ_PROJECT	  NUMERIC(5,0),   
					 YN_PICKING		  NVARCHAR(1),
					 CD_USERDEF1	  NVARCHAR(4),
					 NO_INV		      NVARCHAR(20),
					 TP_UM_TAX        NVARCHAR(3),  
					 UMVAT_GIR        NUMERIC(15, 4),
					 YN_ADD_STOCK	  NVARCHAR(1)) B 
         ON A.CD_COMPANY = @P_CD_COMPANY 
	     AND A.NO_GIR = @P_NO_GIR 
	     AND A.SEQ_GIR = B.SEQ_GIR

UPDATE A 
     SET A.CD_PLANT = B.CD_PLANT,
		 A.CD_ITEM = B.CD_ITEM,
		 A.DT_DELIVERY = B.DT_DUEDATE,
		 A.QT_INVENT = B.QT_GIR_IM,
		 A.UM_INVENT = B.UM,
		 A.AM_INVENT = B.AM_GIR,
		 A.QT_SO = B.QT_GIR,
		 A.UM_SO = B.UM,
		 A.AM_EXSO = B.AM_GIR,
		 A.NO_PO = B.NO_SO,
		 A.NO_LINE_PO = B.SEQ_SO,
		 A.ID_UPDATE = @P_ID_USER,
		 A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
    FROM CZ_TR_INVL A 
         JOIN OPENXML (@DOC, '/XML/U', 2) 
                 WITH (NO_LINE		   NUMERIC(5,0), 
					   CD_PLANT        NVARCHAR(7),  
					   CD_ITEM         NVARCHAR(20), 
					   DT_DUEDATE      NVARCHAR(8),     
					   QT_GIR_IM       NUMERIC(17, 4),
					   UM			   NUMERIC(15,4),
					   AM_GIR		   NUMERIC(17,4),
					   QT_GIR          NUMERIC(17,4),
					   NO_SO           NVARCHAR(20), 
					   SEQ_SO		   NUMERIC(5,0)) B 
           ON A.CD_COMPANY = @P_CD_COMPANY 
		   AND A.NO_INV = @P_NO_INV 
		   AND A.NO_LINE = B.NO_LINE 

EXEC SP_XML_REMOVEDOCUMENT @DOC

GO