IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXSEAL_SELECT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_EXSEAL_SELECT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXSEAL_SELECT
 * ����������: ���� >> �μ������ > �μ������ ��� ��ȸ
 * ��  ��  ��: 
 * EXEC UP_TR_EXSEAL_SELECT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXSEAL_SELECT 
	@CD_COMPANY		NVARCHAR(7),	--ȸ���ڵ�
	@NO_SEAL		NVARCHAR(20)	--�μ�����ȣ
AS  
BEGIN
	SELECT H.NO_SEAL,
	H.DT_BALLOT,
	H.CD_BIZAREA,
	H.CD_PARTNER,
	H.CD_EXCH,
	H.AM_SEAL,
	H.AM,
	H.CD_DEPT,
	H.NO_EMP,
	H.DT_TRANS,
	H.DT_SEAL,
	H.DT_VALIDITY,
	H.REMARK,
	H.DTS_INSERT,
	H.ID_INSERT,
	H.DTS_UPDATE,
	H.ID_UPDATE,
	(SELECT NM_BIZAREA FROM MA_BIZAREA WHERE CD_COMPANY = @CD_COMPANY AND CD_BIZAREA = H.CD_BIZAREA) NM_BIZAREA,	--������  
	(SELECT LN_PARTNER FROM MA_PARTNER WHERE CD_COMPANY = @CD_COMPANY AND CD_PARTNER = H.CD_PARTNER) LN_PARTNER,  --�ŷ�ó  
	(SELECT NM_KOR     FROM MA_EMP     WHERE CD_COMPANY = @CD_COMPANY AND NO_EMP = H.NO_EMP) NM_KOR					--����ڸ�
	FROM TR_SEALEXH H
	WHERE H.CD_COMPANY = @CD_COMPANY
	AND H.NO_SEAL = @NO_SEAL
	
	SELECT L.NO_SEAL,
	L.NO_LINE,
	L.NO_IV,
	L.NO_IVLINE,
	L.CD_PLANT,
	(SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = @CD_COMPANY AND CD_PLANT = L.CD_PLANT AND CD_BIZAREA = H.CD_BIZAREA) NM_PLANT,  --�����
	L.NO_LC,
	L.CD_ITEM,
	(SELECT NM_ITEM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = L.CD_ITEM) NM_ITEM,  --ǰ���
	(SELECT STND_ITEM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = L.CD_ITEM) STND_ITEM,  --�԰�
	(SELECT UNIT_IM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = L.CD_ITEM) UNIT_IM,  --����
	L.QT_GI_CLS,
	L.CD_EXCH,
	L.RT_EXCH,
	L.UM_EX_CLS,
	L.AM_EX_CLS,
	L.UM_ITEM_CLS,
	L.AM_CLS,
	L.QT_CLS,
	L.DTS_INSERT,
	L.ID_INSERT,
	L.DTS_UPDATE,
	L.ID_UPDATE
	FROM TR_SEALEXH H
	LEFT OUTER JOIN TR_SEALEXL L ON L.CD_COMPANY = H.CD_COMPANY AND L.NO_SEAL = H.NO_SEAL
	WHERE L.CD_COMPANY = @CD_COMPANY
	AND L.NO_SEAL = @NO_SEAL
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXSEAL_H_INSERT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_EXSEAL_H_INSERT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXSEAL_H_INSERT
 * ����������: ���� >> �μ������ > ���ι�ư����
 * ��  ��  ��: 
 * EXEC UP_TR_EXSEAL_H_INSERT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXSEAL_H_INSERT 
	@CD_COMPANY		NVARCHAR(7),	--ȸ���ڵ�
	@NO_SEAL		NVARCHAR(20),	--�μ����߱޹�ȣ
	@DT_BALLOT		NCHAR(8),		--��������
	@CD_BIZAREA		NVARCHAR(7),	--�����
	@CD_PARTNER		NVARCHAR(7),	--�ŷ�ó
	@CD_EXCH		NVARCHAR(3),	--��ȭ
	@AM_SEAL		NUMERIC(17,4),	--��ǰ�μ��ݾ�
	@AM				NUMERIC(17,4),	--�����ȭ�ݾ�
	@CD_DEPT		NVARCHAR(12),	--���μ�
	@NO_EMP			NVARCHAR(10),	--�����
	@DT_TRANS		NCHAR(8),		--�ε�����
	@DT_SEAL		NCHAR(8),		--��ǰ�ϼ���
	@DT_VALIDITY	NCHAR(8),		--��ȿ��
	@REMARK			NVARCHAR(100),	--���
	@DTS_INSERT		NVARCHAR(14),	--�����
	@ID_INSERT		NVARCHAR(15),	--�����
	@DTS_UPDATE		NVARCHAR(14),	--������	
	@ID_UPDATE		NVARCHAR(15)	--������
AS  
BEGIN
	IF NOT EXISTS(SELECT TOP 1 NO_SEAL FROM TR_SEALEXH WHERE CD_COMPANY = @CD_COMPANY AND NO_SEAL = @NO_SEAL)
	BEGIN
		INSERT INTO TR_SEALEXH( CD_COMPANY,		NO_SEAL,		DT_BALLOT,		CD_BIZAREA,		CD_PARTNER,		CD_EXCH,
							    AM_SEAL,		AM,				CD_DEPT,		NO_EMP,			DT_TRANS,
							    DT_SEAL,		DT_VALIDITY,	REMARK,			DTS_INSERT,		ID_INSERT)
		VALUES( @CD_COMPANY,	@NO_SEAL,		@DT_BALLOT,		@CD_BIZAREA,	@CD_PARTNER,	@CD_EXCH,
			    @AM_SEAL,		@AM,			@CD_DEPT,		@NO_EMP,		@DT_TRANS,
			    @DT_SEAL,		@DT_VALIDITY,	@REMARK,		@DTS_INSERT,	@ID_INSERT)
	END
	
	ELSE
	BEGIN
		UPDATE TR_SEALEXH
		SET
			DT_BALLOT		= @DT_BALLOT,
			CD_BIZAREA		= @CD_BIZAREA,
			CD_PARTNER		= @CD_PARTNER,
			CD_EXCH			= @CD_EXCH,
			AM_SEAL			= @AM_SEAL,	
			AM				= @AM,
			CD_DEPT			= @CD_DEPT,
			NO_EMP			= @NO_EMP,
			DT_TRANS		= @DT_TRANS,
			DT_SEAL			= @DT_SEAL,
			DT_VALIDITY		= @DT_VALIDITY,
			REMARK			= @REMARK,
			DTS_UPDATE		= @DTS_UPDATE,
			ID_UPDATE		= @ID_UPDATE
		WHERE CD_COMPANY = @CD_COMPANY
		AND NO_SEAL = @NO_SEAL
	END
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXSEAL_L_INSERT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_EXSEAL_L_INSERT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXSEAL_L_INSERT
 * ����������: ���� >> �μ������ > ���ι�ư����
 * ��  ��  ��: 
 * EXEC UP_TR_EXSEAL_L_INSERT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXSEAL_L_INSERT 
	@CD_COMPANY		NVARCHAR(7),	--ȸ���ڵ�
	@NO_SEAL		NVARCHAR(20),	--�μ����߱޹�ȣ
	@NO_LINE		NUMERIC(5),		--��������
	@NO_IV			NVARCHAR(20),	--������ȣ
	@NO_IVLINE		NUMERIC(5),		--��������
	@CD_PLANT		NVARCHAR(7),	--����
	@NO_LC			NVARCHAR(20),	--LC��ȣ
	@CD_ITEM		NVARCHAR(20),	--ǰ��
	@QT_GI_CLS		NUMERIC(17,4),	--����
	@CD_EXCH		NVARCHAR(3),	--ȯ��
	@RT_EXCH		NUMERIC(11,4),	--ȯ��
	@UM_EX_CLS		NUMERIC(17,4),	--��ȭ�ܰ�
	@AM_EX_CLS		NUMERIC(17,4),	--��ȭ�ݾ�
	@UM_ITEM_CLS	NUMERIC(17,4),	--��ȭ�ܰ�
	@AM_CLS			NUMERIC(17,4),	--��ȭ�ݾ�
	@QT_CLS			NUMERIC(17,4),	--��������
	@DTS_INSERT		NVARCHAR(14),	--�����
	@ID_INSERT		NVARCHAR(15),	--�����
	@DTS_UPDATE		NVARCHAR(14),	--������
	@ID_UPDATE		NVARCHAR(15)	--������
AS  
BEGIN
	IF NOT EXISTS(SELECT TOP 1 NO_SEAL FROM TR_SEALEXL WHERE CD_COMPANY = @CD_COMPANY AND NO_SEAL = @NO_SEAL AND NO_LINE = @NO_LINE)
	BEGIN
		INSERT INTO TR_SEALEXL( CD_COMPANY,		NO_SEAL,		NO_LINE,		NO_IV,			NO_IVLINE,		CD_PLANT,
							    NO_LC,			CD_ITEM,		QT_GI_CLS,		CD_EXCH,		RT_EXCH,
							    UM_EX_CLS,		AM_EX_CLS,		UM_ITEM_CLS,	AM_CLS,			QT_CLS,
							    DTS_INSERT,		ID_INSERT)
		VALUES( @CD_COMPANY,	@NO_SEAL,		@NO_LINE,		@NO_IV,			@NO_IVLINE,		@CD_PLANT,
			    @NO_LC,			@CD_ITEM,		@QT_GI_CLS,		@CD_EXCH,		@RT_EXCH,
			    @UM_EX_CLS,		@AM_EX_CLS,		@UM_ITEM_CLS,	@AM_CLS,		@QT_CLS,
			    @DTS_INSERT,	@ID_INSERT)
	END
	
	ELSE
	BEGIN
		UPDATE TR_SEALEXL
		SET
			NO_IV		= @NO_IV,			
			NO_IVLINE	= @NO_IVLINE,	
			CD_PLANT	= @CD_PLANT,
		    NO_LC		= @NO_LC,			
		    CD_ITEM		= @CD_ITEM,		
		    QT_GI_CLS	= @QT_GI_CLS,		
		    CD_EXCH		= @CD_EXCH,		
		    RT_EXCH		= @RT_EXCH,
			UM_EX_CLS	= @UM_EX_CLS,		
			AM_EX_CLS	= @AM_EX_CLS,		
			UM_ITEM_CLS	= @UM_ITEM_CLS,	
			AM_CLS		= @AM_CLS,			
			QT_CLS		= @QT_CLS,
			DTS_UPDATE	= @DTS_UPDATE,		
			ID_UPDATE	= @ID_UPDATE
		WHERE CD_COMPANY = @CD_COMPANY
		AND NO_SEAL = @NO_SEAL
		AND NO_LINE = @NO_LINE
	END
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXSEAL_DELETE') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_EXSEAL_DELETE
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXSEAL_DELETE
 * ����������: ���� >> �μ������ > �μ������ ����
 * ��  ��  ��: 
 * EXEC UP_TR_EXSEAL_DELETE '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXSEAL_DELETE  
	@CD_COMPANY		NVARCHAR(7),	--ȸ���ڵ�
	@NO_SEAL		NVARCHAR(20)	--�μ�����ȣ
AS  
BEGIN
	DELETE FROM TR_SEALEXL
	WHERE CD_COMPANY = @CD_COMPANY
	AND NO_SEAL = @NO_SEAL
	
	DELETE FROM TR_SEALEXH
	WHERE CD_COMPANY = @CD_COMPANY
	AND NO_SEAL = @NO_SEAL
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'UP_TR_EXSEAL_JOIN_SELECT') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE UP_TR_EXSEAL_JOIN_SELECT
GO 
/**********************************************************************************************************  
 * ���ν�����: UP_TR_EXSEAL_JOIN_SELECT
 * ����������: ���� >> �μ������ > ��꼭����
 * ��  ��  ��: 
 * EXEC UP_TR_EXSEAL_JOIN_SELECT '',''
 *********************************************************************************************************/ 

CREATE PROCEDURE UP_TR_EXSEAL_JOIN_SELECT  
	@CD_COMPANY		NVARCHAR(7),	--ȸ���ڵ�
	@NO_IV			NVARCHAR(200)	--��꼭��ȣ
AS  
BEGIN
	SELECT '' NO_SEAL,				--�μ�����ȣ
		'' NO_LINE,					--��������
		SL.NO_IV,					--������ȣ
		SL.NO_LINE AS NO_IVLINE,	--��������
		SL.CD_PLANT,				--�����ڵ�
		(SELECT NM_PLANT FROM MA_PLANT WHERE CD_COMPANY = @CD_COMPANY AND CD_PLANT = SL.CD_PLANT AND CD_BIZAREA = SH.CD_BIZAREA) NM_PLANT,  --�����
		SH.NO_LC,					--L/C��ȣ
		SL.CD_ITEM,					--ǰ��
		(SELECT NM_ITEM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = SL.CD_ITEM) NM_ITEM,  --ǰ���
		(SELECT STND_ITEM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = SL.CD_ITEM) STND_ITEM,  --�԰�
		(SELECT UNIT_IM FROM MA_ITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = SL.CD_ITEM) UNIT_IM,  --����
		SL.QT_GI_CLS,				--��������(������)		
		SL.CD_EXCH,					--ȯ��
		SL.RT_EXCH,					--ȯ��	
		SL.UM_EX_CLS,				--�����ܰ�(��ȭ)
		SL.AM_EX_CLS,				--�����ݾ�(��ȭ)		
		SL.UM_ITEM_CLS,				--�����ܰ�(������)
		SL.AM_CLS,					--�����ݾ�(��ȭ)	
		SL.QT_CLS,					--��������(���ִ���)	
		'' DTS_INSERT,				--�����
		'' ID_INSERT,				--�����
		'' DTS_UPDATE,				--������
		'' ID_UPDATE				--������
	FROM SA_IVH SH
	LEFT OUTER JOIN SA_IVL SL ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_IV = SL.NO_IV
	INNER JOIN TR_LCEXH LC	ON SH.CD_COMPANY = LC.CD_COMPANY AND SH.NO_LC = LC.NO_LC AND LC.FG_LC = '003'
	WHERE SH.CD_COMPANY = @CD_COMPANY
	AND SH.NO_IV IN (SELECT * FROM TF_GETSPLIT(@NO_IV))
END
GO
