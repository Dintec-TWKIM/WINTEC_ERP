USE [NEOE]
GO

/****** Object:  StoredProcedure [NEOE].[SP_CZ_SA_SOL_DTS_UPDATE_U]    Script Date: 2022-06-14 ���� 1:21:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*******************************************    
*********************************************/    
ALTER PROCEDURE [NEOE].[SP_CZ_SA_SOL_DTS_UPDATE_U]    
(    
    @P_CD_COMPANY       NVARCHAR(7),         --ȸ��    
    @P_NO_SO            NVARCHAR(20),        --���ֹ�ȣ    
    @P_SEQ_SO           NUMERIC(5,0)        --�����̷��׹�    
)    
AS      
  
DECLARE @P_DTS_UPDATE NVARCHAR(14)

SET  @P_DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())

UPDATE SA_SOL    
SET DTS_UPDATE = @P_DTS_UPDATE
WHERE CD_COMPANY  = @P_CD_COMPANY    
AND NO_SO = @P_NO_SO    
AND SEQ_SO = @P_SEQ_SO

GO


