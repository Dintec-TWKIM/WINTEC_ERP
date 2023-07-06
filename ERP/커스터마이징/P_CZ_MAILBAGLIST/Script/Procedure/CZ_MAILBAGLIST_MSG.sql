USE [NEOE]
GO
/****** Object:  StoredProcedure [NEOE].[CZ_MAILBAGLIST_MSG]    Script Date: 2023-05-03 오후 4:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [NEOE].[CZ_MAILBAGLIST_MSG]
		@P_NO_EMP		NVARCHAR(10),
		@P_CD_SEND		NVARCHAR(2)
AS

DECLARE	@V_CONTENTS		NVARCHAR(MAX)
DECLARE	@V_CONTENTS1	NVARCHAR(MAX)



iF @P_CD_SEND = '01'
BEGIN
 SET @V_CONTENTS1 = '(본사)행낭 물류 리스트'+char(10)+Convert(varchar(10),Getdate(),111)+' 본사에서 양산으로 발송'+char(10)+char(10)+
 (SELECT DISTINCT  
        STUFF((SELECT distinct concat(char(10),
		'발송자 : '+ISNULL(MP1.NM_KOR,'')+
		' / 수신자 : '+ISNULL(MP2.NM_KOR,'')+
		' // 내용 : '+ISNULL(A.NM_ITEM,'없음')+
		' / 수량 : '+ISNULL(CONVERT(NVARCHAR(3),A.QT),'없음')+	
		' / 비고 : '+ISNULL(A.DC_RMK,''))
	FROM CZ_MAILBAGLIST_L AS A
	LEFT JOIN CZ_MAILBAGLIST_H AS C ON A.CD_SEND = C.CD_SEND AND A.DT_ACCT = C.DT_ACCT
	LEFT JOIN MA_EMP MP1 ON A.NO_EMP_SEND = MP1.NO_EMP AND (MP1.CD_COMPANY ='k100' OR MP1.CD_COMPANY ='k200')
	LEFT JOIN MA_EMP MP2 ON A.NO_EMP_RECEIVE = MP2.NO_EMP AND (MP2.CD_COMPANY ='k100' OR MP2.CD_COMPANY ='k200')
	LEFT JOIN MA_EMP MP3 ON A.NO_EMP_INSPECT = MP3.NO_EMP AND (MP3.CD_COMPANY ='k100' OR MP3.CD_COMPANY ='k200')
	LEFT JOIN CZ_MA_CODEDTL B ON B.CD_FIELD = 'CZ_MM00004' AND A.CD_SEND = B.CD_SYSDEF
	WHERE A.DT_ACCT = FORMAT(CAST(GETDATE() AS DATE), 'yyyyMMdd')
	AND A.CD_SEND = '01'
	FOR XML PATH('')),1,1,'')  
	FROM CZ_MAILBAGLIST_L AS A)

	SET @V_CONTENTS = ISNULL(@V_CONTENTS1,'(본사)행낭 물류 리스트'+char(10)+Convert(varchar(10),Getdate(),111)+' 본사에서 양산으로 발송할 물건이 없습니다')
	EXEC PX_CZ_RPA_MSG_SEND @P_NO_EMP,@V_CONTENTS
	EXEC PX_CZ_RPA_MSG_SEND 'S-605',@V_CONTENTS

END
ELSE
BEGIN
 SET @V_CONTENTS1 = '(양산)행낭 물류 리스트'+char(10)+Convert(varchar(10),Getdate(),111)+' 양산에서 본사로 발송'+char(10)+char(10)+
 (SELECT DISTINCT  
        STUFF((SELECT distinct concat(char(10),
		'발송자 : '+ISNULL(MP1.NM_KOR,'')+
		' / 수신자 : '+ISNULL(MP2.NM_KOR,'')+
		' // 내용 : '+ISNULL(A.NM_ITEM,'없음')+
		' / 수량 : '+ISNULL(CONVERT(NVARCHAR(3),A.QT),'없음')+	
		' / 비고 : '+ISNULL(A.DC_RMK,''))
	FROM CZ_MAILBAGLIST_L AS A
	LEFT JOIN CZ_MAILBAGLIST_H AS C ON A.CD_SEND = C.CD_SEND AND A.DT_ACCT = C.DT_ACCT
	LEFT JOIN MA_EMP MP1 ON A.NO_EMP_SEND = MP1.NO_EMP AND (MP1.CD_COMPANY ='k100' OR MP1.CD_COMPANY ='k200')
	LEFT JOIN MA_EMP MP2 ON A.NO_EMP_RECEIVE = MP2.NO_EMP AND (MP2.CD_COMPANY ='k100' OR MP2.CD_COMPANY ='k200')
	LEFT JOIN MA_EMP MP3 ON A.NO_EMP_INSPECT = MP3.NO_EMP AND (MP3.CD_COMPANY ='k100' OR MP3.CD_COMPANY ='k200')
	LEFT JOIN CZ_MA_CODEDTL B ON B.CD_FIELD = 'CZ_MM00004' AND A.CD_SEND = B.CD_SYSDEF
	WHERE A.DT_ACCT = FORMAT(CAST(GETDATE() AS DATE), 'yyyyMMdd')
	AND A.CD_SEND = '02'
	FOR XML PATH('')),1,1,'')  
	FROM CZ_MAILBAGLIST_L AS A)

	SET @V_CONTENTS = ISNULL(@V_CONTENTS1,'(양산)행낭 물류 리스트'+char(10)+Convert(varchar(10),Getdate(),111)+' 양산에서 본사로 발송할 물건이 없습니다')
	
	EXEC PX_CZ_RPA_MSG_SEND @P_NO_EMP,@V_CONTENTS
	EXEC PX_CZ_RPA_MSG_SEND 'S-605',@V_CONTENTS
END


