USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_PU_ADPAYMENT_BILL_S]    Script Date: 2015-08-05 오후 6:21:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [NEOE].[SP_CZ_PU_ADPAYMENT_BILL_S]
(
    @P_CD_COMPANY       NVARCHAR(7),
	@P_LANGUAGE			NVARCHAR(5) = 'KR',
    @P_NO_BILLS         NVARCHAR(20)
)
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT A.NO_BILLS AS NO_BILLS,
	   A.DT_BILLS AS DT_BILLS,
	   A.CD_BILLTGRP AS CD_BILLTGRP,
	   E.NM_SALEGRP,
	   A.CD_PARTNER,
	   B.LN_PARTNER AS NM_PARTNER,
	   A.NO_EMP,
	   D.NM_KOR,
	   A.TP_BUSI,
	   A.AM_BILLS AS AM_BILLS,
	   A.AM_IV,
	   A.DC_RMK,
	   ISNULL(A.ST_BILL, 'N') AS ST_BILL,
	   ISNULL(A.CD_TP, '')    AS CD_TP,
       A.CD_DOCU,
	   ISNULL((SELECT TOP 1 FD.NO_DOCU   
			   FROM FI_DOCU FD WITH(READUNCOMMITTED)
			   WHERE FD.CD_COMPANY = A.CD_COMPANY                     
			   AND FD.NO_MDOCU = A.NO_BILLS), '') NO_DOCU,
	   CC.CD_PC AS CD_PC                
FROM CZ_PU_ADPAYMENT_BILL_H A 
LEFT JOIN MA_PARTNER B ON B.CD_COMPANY = A.CD_COMPANY AND B.CD_PARTNER = A.CD_PARTNER
LEFT JOIN MA_EMP D ON D.CD_COMPANY = A.CD_COMPANY AND D.NO_EMP = A.NO_EMP
LEFT JOIN MA_SALEGRP E ON E.CD_COMPANY = A.CD_COMPANY AND E.CD_SALEGRP = A.CD_BILLTGRP
LEFT JOIN MA_CC CC ON E.CD_COMPANY = CC.CD_COMPANY AND E.CD_CC = CC.CD_CC
WHERE A.CD_COMPANY = @P_CD_COMPANY
AND A.NO_BILLS = @P_NO_BILLS

SELECT BL.NO_BILLS,
	   BL.NO_LINE,
	   BL.NO_ADPAY,
	   PA.DT_ADPAY,
	   BL.AM_BILLS,
	   BL.AM_TARGET,
	   (BL.AM_TARGET - BL.AM_BILLS) AS AM_REMAIN,
	   BL.CD_EXCH,
	   MC.NM_SYSDEF AS NM_EXCH,
	   BL.RT_EXCH,
	   BL.AM_TARGET_EX,  
	   BL.AM_BILLS_EX,
	   BL.NO_MGMT,
	   BL.CD_PJT,
	   BL.NO_DOCU,
	   BL.NO_DOLINE
FROM CZ_PU_ADPAYMENT_BILL_L BL
LEFT JOIN (SELECT CD_COMPANY, NO_ADPAY, DT_ADPAY 
		   FROM PU_ADPAYMENT 
		   GROUP BY CD_COMPANY, NO_ADPAY, DT_ADPAY) PA 
ON PA.CD_COMPANY = BL.CD_COMPANY AND PA.NO_ADPAY = BL.NO_ADPAY
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = BL.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = BL.CD_EXCH
WHERE BL.CD_COMPANY = @P_CD_COMPANY
AND BL.NO_BILLS = @P_NO_BILLS

SELECT A.NO_BILLS,
       A.NO_IV,
	   A.NO_ADPAY,
       A.DT_IV,
       (ISNULL(B.AM_K, 0) + ISNULL(C.AM_K, 0)) AS AM_IV,
       (ISNULL(B.VAT_TAX, 0) + ISNULL(C.VAT, 0)) AS AM_VAT,
       A.AM_TARGET,
	   ( A.AM_TARGET - A.AM_RCPS ) AS AM_REMAIN,
       A.AM_RCPS AS AM_RCPS,
       A.GUBUN,
	   A.CD_EXCH_IV,
	   MC.NM_SYSDEF AS NM_EXCH_IV,
	   A.RT_EXCH_IV,
	   A.AM_TARGET_EX,
	   A.AM_RCPS_EX,
	   A.AM_PL,
	   CASE WHEN SIGN(A.AM_PL) = 1 THEN 0 - (A.AM_PL) ELSE 0 END AS AM_PL_LOSS,
	   CASE WHEN SIGN(A.AM_PL) = -1 THEN 0 - (A.AM_PL) ELSE 0 END AS AM_PL_PROFIT,
	   A.NO_DOCU_ADPAY,
	   A.NO_DOLINE_ADPAY,
	   A.NO_DOCU_IV,
	   A.NO_DOLINE_IV
FROM CZ_PU_ADPAYMENT_BILL_D A
LEFT JOIN SA_IVH B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_IV = B.NO_IV
LEFT JOIN EC_IVH C ON A.CD_COMPANY = C.CD_COMPANY AND A.NO_IV = C.NO_IV
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = A.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = A.CD_EXCH_IV
WHERE A.CD_COMPANY = @P_CD_COMPANY
AND A.NO_BILLS = @P_NO_BILLS
AND A.GUBUN = '0'

GO