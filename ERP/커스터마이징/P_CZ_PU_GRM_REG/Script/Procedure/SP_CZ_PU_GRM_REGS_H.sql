SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[SP_CZ_PU_GRM_REGS_H]   
(  
	@CD_COMPANY                 NVARCHAR(100),  
	@DT_REQ_FROM                NCHAR(8),  
	@DT_REQ_TO                  NCHAR(8),  
	@CD_PLANT					NVARCHAR(7),          
	@CD_SL					    TEXT,  
	@CD_PARTNER                 NVARCHAR(20),          
	@FG_TRANS					NCHAR(3),  
	@YN_RETURN					NCHAR(1),  
	@CD_QTIOTP					NVARCHAR(4000),  
	@NO_EMP						NVARCHAR(10),  
	@IS_CLOSED					NCHAR(3),
	@NO_IO						NVARCHAR(20),
	@CD_PJT						NVARCHAR(20) = NULL,
	@TXT_USERDEF4               NVARCHAR(20) = NULL,
    @P_FG_LANG					NVARCHAR(4) = NULL,	--언어
    @P_CD_TPPO_MULTI			NVARCHAR(4000) = NULL,
    @P_TXT_NO_IO				NVARCHAR(20) = NULL
)  
AS
-- MultiLang Call
EXEC UP_MA_LOCAL_LANGUAGE @P_FG_LANG

BEGIN  

IF( @IS_CLOSED = '001')  
BEGIN  
  
 SELECT  'N' AS S,
		@IS_CLOSED IS_CLOSED,
		'' AS NO,
		A.CD_COMPANY, 
		A.NO_IO,
		MAX(A.TXT_USERDEF1) AS TXT_USERDEF1,
		MAX(B.CD_PARTNER) AS CD_PARTNER,
		MAX(B.FG_IO) AS FG_IO,
		MAX(A.CD_DEPT) AS CD_DEPT,
		MAX(A.NO_EMP) AS NO_EMP,
		MAX(A.DC_RMK) AS DC_RMK,
		MAX(A.YN_RETURN) AS YN_RETURN,
		MAX(A.DT_IO) AS DT_IO,  
		MAX(B.YN_AM) AS YN_AM, 
		MAX(C.NM_KOR) AS NM_KOR, 
		MAX(D.NM_DEPT) AS NM_DEPT, 
		MAX(E.NM_PLANT) AS NM_PLANT,
		MAX(F.LN_PARTNER) AS LN_PARTNER,   
		--H.NM_QTIOTP,
		--Z1.NM_SYSDEF AS NM_FG_TRANS, 
		--Z2.NM_SYSDEF AS NM_CD_EXCH, 
		MAX(Z3.NM_SYSDEF) AS NM_YN_RETURN,
		MAX(A.NO_POP) AS NO_POP,
		MAX(A.FILE_PATH_MNG) AS FILE_PATH_MNG,
		COUNT(B.NO_IO) AS QT_ITEM,
		MAX(MC.NM_COMPANY) AS NM_COMPANY,
		MAX(POH.TXT_USERDEF4) AS TXT_USERDEF4
FROM MM_QTIOH A 
	 INNER JOIN MM_QTIO B ON A.NO_IO = B.NO_IO AND A.CD_COMPANY = B.CD_COMPANY    
     LEFT OUTER JOIN DZSN_MA_EMP C ON A.CD_COMPANY = C.CD_COMPANY  AND A.NO_EMP = C.NO_EMP  
	 LEFT OUTER JOIN DZSN_MA_DEPT D ON A.CD_COMPANY = D.CD_COMPANY  AND A.CD_DEPT = D.CD_DEPT  
	 LEFT OUTER JOIN DZSN_MA_PLANT E ON   B.CD_COMPANY = E.CD_COMPANY  AND B.CD_PLANT = E.CD_PLANT    
	 LEFT OUTER JOIN DZSN_MA_PARTNER F ON   B.CD_COMPANY = F.CD_COMPANY  AND B.CD_PARTNER = F.CD_PARTNER  
	-- LEFT OUTER JOIN DZSN_MM_EJTP H ON  B.CD_COMPANY = H.CD_COMPANY AND B.CD_QTIOTP = H.CD_QTIOTP 
	-- LEFT OUTER JOIN DZSN_MA_CODEDTL Z1 ON B.CD_COMPANY  = Z1.CD_COMPANY AND B.FG_TRANS = Z1.CD_SYSDEF AND Z1.CD_FIELD = 'PU_C000016'  
    -- LEFT OUTER JOIN DZSN_MA_CODEDTL Z2 ON B.CD_COMPANY = Z2.CD_COMPANY AND B.CD_EXCH =  Z2.CD_SYSDEF AND Z2.CD_FIELD = 'MA_B000005'   
	 LEFT OUTER JOIN DZSN_MA_CODEDTL Z3 ON A.CD_COMPANY = Z3.CD_COMPANY  AND A.YN_RETURN =  Z3.CD_SYSDEF AND Z3.CD_FIELD = 'PU_C000027'
	 LEFT OUTER JOIN PU_POL POL ON POL.NO_PO = B.NO_PSO_MGMT AND POL.NO_LINE = B.NO_PSOLINE_MGMT AND POL.CD_COMPANY = B.CD_COMPANY
	 LEFT JOIN PU_POH POH ON POH.CD_COMPANY = POL.CD_COMPANY AND POH.NO_PO = POL.NO_PO
	 LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = A.CD_COMPANY
	 LEFT JOIN (SELECT CD_COMPANY, ID_MENU, CD_FILE, MAX(FILE_NAME) AS NM_FILE, CONVERT(NVARCHAR(5),COUNT(1)) AS FILE_COUNT
			      FROM MA_FILEINFO
				 WHERE CD_MODULE = 'PU'
				   AND (ISNULL(@CD_COMPANY, '') = '' OR CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_COMPANY)))  
			  GROUP BY CD_COMPANY, ID_MENU, CD_FILE
	)MF ON MF.CD_COMPANY = B.CD_COMPANY AND MF.ID_MENU = 'P_PU_GRM_REG' AND MF.CD_FILE = B.NO_IO 
    LEFT JOIN (SELECT CD_COMPANY, ID_MENU, CD_FILE, MAX(FILE_NAME) AS NM_FILE, CONVERT(NVARCHAR(5),COUNT(1)) AS FILE_COUNT
			      FROM MA_FILEINFO
				 WHERE CD_MODULE = 'PU'
				   AND (ISNULL(@CD_COMPANY, '') = '' OR CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_COMPANY)))
			  GROUP BY CD_COMPANY, ID_MENU, CD_FILE
	)MF2 ON MF2.CD_COMPANY = B.CD_COMPANY AND MF2.ID_MENU = 'P_PU_REQ_REG_1' AND MF2.CD_FILE = B.NO_ISURCV + '_' + A.CD_COMPANY
 WHERE  B.FG_PS = '1'  
 AND B.FG_IO IN ( '001', '030' , '031')  
 AND B.CD_COMPANY = A.CD_COMPANY
 AND B.DT_IO BETWEEN @DT_REQ_FROM AND @DT_REQ_TO   
 AND B.CD_PLANT = @CD_PLANT  
 AND ((  B.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@CD_SL))) OR (@CD_SL LIKE '' ) OR (@CD_SL IS NULL))  
 AND ((  B.CD_PARTNER  =@CD_PARTNER) OR (@CD_PARTNER = '' ) OR (@CD_PARTNER IS NULL))  
 AND ((  B.FG_TRANS  =@FG_TRANS) OR (@FG_TRANS = '' ) OR (@FG_TRANS IS NULL))  
 AND ((  A.YN_RETURN  =@YN_RETURN) OR (@YN_RETURN = '' ) OR (@YN_RETURN IS NULL))  
 AND ((  B.CD_QTIOTP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_QTIOTP))) OR (@CD_QTIOTP = '' ) OR (@CD_QTIOTP IS NULL))  
 AND ((  A.NO_EMP  =@NO_EMP) OR (@NO_EMP = '' ) OR (@NO_EMP IS NULL))  
 AND ( A.NO_IO = @NO_IO OR @NO_IO = '' OR @NO_IO IS NULL) 
 AND ( B.CD_PJT = @CD_PJT OR @CD_PJT = '' OR @CD_PJT IS NULL)
 AND ( POH.TXT_USERDEF4 LIKE '%'+@TXT_USERDEF4+'%' OR @TXT_USERDEF4 = '' OR @TXT_USERDEF4 IS NULL)
 AND B.QT_CLS = 0  
 AND (( POH.CD_TPPO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_TPPO_MULTI))) OR (@P_CD_TPPO_MULTI = '' ) OR (@P_CD_TPPO_MULTI IS NULL))
 AND ( A.NO_IO LIKE '%' + @P_TXT_NO_IO + '%' OR @P_TXT_NO_IO = '' OR @P_TXT_NO_IO IS NULL) 
 AND (ISNULL(@CD_COMPANY, '') = '' OR A.CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_COMPANY)))  
    
GROUP BY A.CD_COMPANY, A.NO_IO
END  
ELSE IF( @IS_CLOSED = '002')   
BEGIN  
  
   
 SELECT  
       'N' AS S,
		@IS_CLOSED IS_CLOSED,
		'' AS NO,
		A.CD_COMPANY, 
		A.NO_IO,
		MAX(A.TXT_USERDEF1) AS TXT_USERDEF1,
		MAX(B.CD_PARTNER) AS CD_PARTNER,
		MAX(B.FG_IO) AS FG_IO,
		MAX(A.CD_DEPT) AS CD_DEPT,
		MAX(A.NO_EMP) AS NO_EMP,
		MAX(A.DC_RMK) AS DC_RMK,
		MAX(A.YN_RETURN) AS YN_RETURN,
		MAX(A.DT_IO) AS DT_IO,  
		MAX(B.YN_AM) AS YN_AM, 
		MAX(C.NM_KOR) AS NM_KOR, 
		MAX(D.NM_DEPT) AS NM_DEPT, 
		MAX(E.NM_PLANT) AS NM_PLANT,
		MAX(F.LN_PARTNER) AS LN_PARTNER,   
		--H.NM_QTIOTP,
		--Z1.NM_SYSDEF AS NM_FG_TRANS, 
		--Z2.NM_SYSDEF AS NM_CD_EXCH, 
		MAX(Z3.NM_SYSDEF) AS NM_YN_RETURN,
		MAX(A.NO_POP) AS NO_POP,
		MAX(A.FILE_PATH_MNG) AS FILE_PATH_MNG,
		COUNT(B.NO_IO) AS QT_ITEM,
		MAX(MC.NM_COMPANY) AS NM_COMPANY,
		MAX(POH.TXT_USERDEF4) AS TXT_USERDEF4
 FROM MM_QTIOH A
	 INNER JOIN MM_QTIO B ON A.NO_IO = B.NO_IO AND A.CD_COMPANY = B.CD_COMPANY    
     LEFT OUTER JOIN DZSN_MA_EMP C ON A.CD_COMPANY = C.CD_COMPANY  AND A.NO_EMP = C.NO_EMP  
	 LEFT OUTER JOIN DZSN_MA_DEPT D ON A.CD_COMPANY = D.CD_COMPANY  AND A.CD_DEPT = D.CD_DEPT  
	 LEFT OUTER JOIN DZSN_MA_PLANT E ON   B.CD_COMPANY = E.CD_COMPANY  AND B.CD_PLANT = E.CD_PLANT    
	 LEFT OUTER JOIN DZSN_MA_PARTNER F ON   B.CD_COMPANY = F.CD_COMPANY  AND B.CD_PARTNER = F.CD_PARTNER  
	-- LEFT OUTER JOIN DZSN_MM_EJTP H ON  B.CD_COMPANY = H.CD_COMPANY AND B.CD_QTIOTP = H.CD_QTIOTP 
	-- LEFT OUTER JOIN DZSN_MA_CODEDTL Z1 ON B.CD_COMPANY  = Z1.CD_COMPANY AND B.FG_TRANS = Z1.CD_SYSDEF AND Z1.CD_FIELD = 'PU_C000016'  
    -- LEFT OUTER JOIN DZSN_MA_CODEDTL Z2 ON  B.CD_COMPANY = Z2.CD_COMPANY AND B.CD_EXCH =  Z2.CD_SYSDEF AND Z2.CD_FIELD = 'MA_B000005'   
	 LEFT OUTER JOIN DZSN_MA_CODEDTL Z3 ON  A.CD_COMPANY = Z3.CD_COMPANY  AND A.YN_RETURN =  Z3.CD_SYSDEF AND Z3.CD_FIELD = 'PU_C000027'  
	 LEFT OUTER JOIN PU_POL POL ON POL.NO_PO = B.NO_PSO_MGMT AND POL.NO_LINE = B.NO_PSOLINE_MGMT AND POL.CD_COMPANY = B.CD_COMPANY
	 LEFT JOIN PU_POH POH ON POH.CD_COMPANY = POL.CD_COMPANY AND POH.NO_PO = POL.NO_PO    
	 LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = A.CD_COMPANY
	 LEFT JOIN (SELECT CD_COMPANY, ID_MENU, CD_FILE, MAX(FILE_NAME) AS NM_FILE, CONVERT(NVARCHAR(5),COUNT(1)) AS FILE_COUNT
			    FROM MA_FILEINFO
				WHERE CD_MODULE = 'PU'
				AND (ISNULL(@CD_COMPANY, '') = '' OR CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_COMPANY)))  
			  GROUP BY CD_COMPANY, ID_MENU, CD_FILE
	)MF ON MF.CD_COMPANY = B.CD_COMPANY AND MF.ID_MENU = 'P_PU_GRM_REG' AND MF.CD_FILE = B.NO_IO 
	LEFT JOIN (SELECT CD_COMPANY, ID_MENU, CD_FILE, MAX(FILE_NAME) AS NM_FILE, CONVERT(NVARCHAR(5),COUNT(1)) AS FILE_COUNT
			   FROM MA_FILEINFO
			   WHERE CD_MODULE = 'PU'
			   AND (ISNULL(@CD_COMPANY, '') = '' OR CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_COMPANY)))  
			  GROUP BY CD_COMPANY, ID_MENU, CD_FILE
	)MF2 ON MF2.CD_COMPANY = B.CD_COMPANY AND MF2.ID_MENU = 'P_PU_REQ_REG_1' AND MF2.CD_FILE = B.NO_ISURCV + '_' + A.CD_COMPANY
 WHERE  B.CD_COMPANY = A.CD_COMPANY 
 AND B.FG_PS = '1'  
 AND B.FG_IO IN ( '001', '030' , '031')  
 AND B.DT_IO BETWEEN @DT_REQ_FROM AND @DT_REQ_TO  
 AND B.CD_PLANT = @CD_PLANT  
 AND ((  B.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@CD_SL))) OR (@CD_SL LIKE '' ) OR (@CD_SL IS NULL))  
 AND ((  B.CD_PARTNER  =@CD_PARTNER) OR (@CD_PARTNER = '' ) OR (@CD_PARTNER IS NULL))  
 AND ((  B.FG_TRANS  =@FG_TRANS) OR (@FG_TRANS = '' ) OR (@FG_TRANS IS NULL))  
 AND ((  A.YN_RETURN  =@YN_RETURN) OR (@YN_RETURN = '' ) OR (@YN_RETURN IS NULL))  
 AND ((  B.CD_QTIOTP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_QTIOTP))) OR (@CD_QTIOTP = '' ) OR (@CD_QTIOTP IS NULL))  
 AND ((  A.NO_EMP  =@NO_EMP) OR (@NO_EMP = '' ) OR (@NO_EMP IS NULL)) 
 AND ( A.NO_IO = @NO_IO OR @NO_IO = '' OR @NO_IO IS NULL) 
 AND B.QT_CLS <> 0  
 AND ( B.CD_PJT = @CD_PJT OR @CD_PJT = '' OR @CD_PJT IS NULL) 
 AND ( POH.TXT_USERDEF4 LIKE '%'+@TXT_USERDEF4+'%' OR @TXT_USERDEF4 = '' OR @TXT_USERDEF4 IS NULL)
 AND (( POH.CD_TPPO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_TPPO_MULTI))) OR (@P_CD_TPPO_MULTI = '' ) OR (@P_CD_TPPO_MULTI IS NULL)) 
 AND ( A.NO_IO LIKE '%' + @P_TXT_NO_IO + '%' OR @P_TXT_NO_IO = '' OR @P_TXT_NO_IO IS NULL) 
 AND (ISNULL(@CD_COMPANY, '') = '' OR A.CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_COMPANY)))  

 GROUP BY A.CD_COMPANY, A.NO_IO
END  

ELSE
BEGIN  
  
   
 SELECT  
       'N' AS S,
		@IS_CLOSED IS_CLOSED,
		'' AS NO,
		A.CD_COMPANY, 
		A.NO_IO,
		MAX(A.TXT_USERDEF1) AS TXT_USERDEF1,
		MAX(B.CD_PARTNER) AS CD_PARTNER,
		MAX(B.FG_IO) AS FG_IO,
		MAX(A.CD_DEPT) AS CD_DEPT,
		MAX(A.NO_EMP) AS NO_EMP,
		MAX(A.DC_RMK) AS DC_RMK,
		MAX(A.YN_RETURN) AS YN_RETURN,
		MAX(A.DT_IO) AS DT_IO,  
		MAX(B.YN_AM) AS YN_AM, 
		MAX(C.NM_KOR) AS NM_KOR, 
		MAX(D.NM_DEPT) AS NM_DEPT, 
		MAX(E.NM_PLANT) AS NM_PLANT,
		MAX(F.LN_PARTNER) AS LN_PARTNER,   
		--H.NM_QTIOTP,
		--Z1.NM_SYSDEF AS NM_FG_TRANS, 
		--Z2.NM_SYSDEF AS NM_CD_EXCH, 
		MAX(Z3.NM_SYSDEF) AS NM_YN_RETURN,
		MAX(A.NO_POP) AS NO_POP,
		MAX(A.FILE_PATH_MNG) AS FILE_PATH_MNG,
		COUNT(B.NO_IO) AS QT_ITEM,
		MAX(MC.NM_COMPANY) AS NM_COMPANY,
		MAX(POH.TXT_USERDEF4) AS TXT_USERDEF4
 FROM MM_QTIOH A
	 INNER JOIN MM_QTIO B ON A.NO_IO = B.NO_IO AND A.CD_COMPANY = B.CD_COMPANY    
     LEFT OUTER JOIN DZSN_MA_EMP C ON A.CD_COMPANY = C.CD_COMPANY  AND A.NO_EMP = C.NO_EMP  
	 LEFT OUTER JOIN DZSN_MA_DEPT D ON A.CD_COMPANY = D.CD_COMPANY  AND A.CD_DEPT = D.CD_DEPT  
	 LEFT OUTER JOIN DZSN_MA_PLANT E ON   B.CD_COMPANY = E.CD_COMPANY  AND B.CD_PLANT = E.CD_PLANT    
	 LEFT OUTER JOIN DZSN_MA_PARTNER F ON   B.CD_COMPANY = F.CD_COMPANY  AND B.CD_PARTNER = F.CD_PARTNER  
	-- LEFT OUTER JOIN DZSN_MM_EJTP H ON  B.CD_COMPANY = H.CD_COMPANY AND B.CD_QTIOTP = H.CD_QTIOTP 
	-- LEFT OUTER JOIN DZSN_MA_CODEDTL Z1 ON B.CD_COMPANY  = Z1.CD_COMPANY AND B.FG_TRANS = Z1.CD_SYSDEF AND Z1.CD_FIELD = 'PU_C000016'  
    -- LEFT OUTER JOIN DZSN_MA_CODEDTL Z2 ON  B.CD_COMPANY = Z2.CD_COMPANY AND B.CD_EXCH =  Z2.CD_SYSDEF AND Z2.CD_FIELD = 'MA_B000005'   
	 LEFT OUTER JOIN DZSN_MA_CODEDTL Z3 ON  A.CD_COMPANY = Z3.CD_COMPANY  AND A.YN_RETURN =  Z3.CD_SYSDEF AND Z3.CD_FIELD = 'PU_C000027'  
	 LEFT OUTER JOIN PU_POL POL ON POL.NO_PO = B.NO_PSO_MGMT AND POL.NO_LINE = B.NO_PSOLINE_MGMT AND POL.CD_COMPANY = B.CD_COMPANY
	 LEFT JOIN PU_POH POH ON POH.CD_COMPANY = POL.CD_COMPANY AND POH.NO_PO = POL.NO_PO    
	 LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY = A.CD_COMPANY
	 LEFT JOIN (SELECT CD_COMPANY, ID_MENU, CD_FILE, MAX(FILE_NAME) AS NM_FILE, CONVERT(NVARCHAR(5),COUNT(1)) AS FILE_COUNT
			    FROM MA_FILEINFO
				WHERE CD_MODULE = 'PU'
				AND (ISNULL(@CD_COMPANY, '') = '' OR CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_COMPANY)))  
			  GROUP BY CD_COMPANY, ID_MENU, CD_FILE
	)MF ON MF.CD_COMPANY = B.CD_COMPANY AND MF.ID_MENU = 'P_PU_GRM_REG' AND MF.CD_FILE = B.NO_IO 
	LEFT JOIN (SELECT CD_COMPANY, ID_MENU, CD_FILE, MAX(FILE_NAME) AS NM_FILE, CONVERT(NVARCHAR(5),COUNT(1)) AS FILE_COUNT
			   FROM MA_FILEINFO
			   WHERE CD_MODULE = 'PU'
			   AND (ISNULL(@CD_COMPANY, '') = '' OR CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_COMPANY)))  
			  GROUP BY CD_COMPANY, ID_MENU, CD_FILE
	)MF2 ON MF2.CD_COMPANY = B.CD_COMPANY AND MF2.ID_MENU = 'P_PU_REQ_REG_1' AND MF2.CD_FILE = B.NO_ISURCV + '_' + A.CD_COMPANY
 WHERE  B.CD_COMPANY = A.CD_COMPANY 
 AND B.FG_PS = '1'  
 AND B.FG_IO IN ( '001', '030' , '031')  
 AND B.DT_IO BETWEEN @DT_REQ_FROM AND @DT_REQ_TO  
 AND B.CD_PLANT = @CD_PLANT  
 AND ((  B.CD_SL IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2(@CD_SL))) OR (@CD_SL LIKE '' ) OR (@CD_SL IS NULL))  
 AND ((  B.CD_PARTNER  =@CD_PARTNER) OR (@CD_PARTNER = '' ) OR (@CD_PARTNER IS NULL))  
 AND ((  B.FG_TRANS  =@FG_TRANS) OR (@FG_TRANS = '' ) OR (@FG_TRANS IS NULL))  
 AND ((  A.YN_RETURN  =@YN_RETURN) OR (@YN_RETURN = '' ) OR (@YN_RETURN IS NULL))  
 AND ((  B.CD_QTIOTP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_QTIOTP))) OR (@CD_QTIOTP = '' ) OR (@CD_QTIOTP IS NULL))  
 AND ((  A.NO_EMP  =@NO_EMP) OR (@NO_EMP = '' ) OR (@NO_EMP IS NULL)) 
 AND ( A.NO_IO = @NO_IO OR @NO_IO = '' OR @NO_IO IS NULL) 
 AND ( B.CD_PJT = @CD_PJT OR @CD_PJT = '' OR @CD_PJT IS NULL) 
 AND ( POH.TXT_USERDEF4 LIKE '%'+@TXT_USERDEF4+'%' OR @TXT_USERDEF4 = '' OR @TXT_USERDEF4 IS NULL)
 AND (( POH.CD_TPPO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@P_CD_TPPO_MULTI))) OR (@P_CD_TPPO_MULTI = '' ) OR (@P_CD_TPPO_MULTI IS NULL)) 
 AND ( A.NO_IO LIKE '%' + @P_TXT_NO_IO + '%' OR @P_TXT_NO_IO = '' OR @P_TXT_NO_IO IS NULL) 
 AND (ISNULL(@CD_COMPANY, '') = '' OR A.CD_COMPANY IN (SELECT CD_STR FROM GETTABLEFROMSPLIT(@CD_COMPANY)))  

 GROUP BY A.CD_COMPANY, A.NO_IO
END  
END
GO