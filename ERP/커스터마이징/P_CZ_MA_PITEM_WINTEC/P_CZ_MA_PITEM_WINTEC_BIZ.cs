using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPIU.Data;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using System;
using System.Data;
using System.Text;
using System.Web;

namespace cz
{
    internal class P_CZ_MA_PITEM_WINTEC_BIZ
    {
        private string 회사코드 = MA.Login.회사코드;
        private DataTable dt계정구분 = (DataTable)null;
        private DataTable dt품목군 = (DataTable)null;
        private DataTable dt창고 = (DataTable)null;
        private DataTable _dtClsL = (DataTable)null;
        private DataTable _dtClsM = (DataTable)null;
        private DataTable _dtClsS = (DataTable)null;

        internal DataTable Search(object[] args, string clsitem, string chk_top)
        {
            string spName = "UP_MA_PITEM_S";
            int num;
            if (chk_top == "Y" && D.GetString(args[5]) != "")
                num = !MA.ServerKey(true, "PNT") ? 1 : 0;
            else
                num = 1;
            if (num == 0)
                spName = "UP_MA_Z_PNT_PITEM_S";
            DataTable dataTable = DBHelper.GetDataTable(spName, args);
            T.SetDefaultValue(dataTable);
            string str = "N";
            if (BASIC.GetMAEXC("공장품목등록-B/F필수여부") == "100")
                str = "Y";
            dataTable.Columns["TP_PROC"].DefaultValue = "P";
            dataTable.Columns["CLS_ITEM"].DefaultValue = clsitem;
            dataTable.Columns["TP_ITEM"].DefaultValue = "SIN";
            dataTable.Columns["TP_PART"].DefaultValue = "P";
            dataTable.Columns["DT_VALID"].DefaultValue = "29991231";
            dataTable.Columns["YN_USE"].DefaultValue = "Y";
            dataTable.Columns["FG_ABC"].DefaultValue = "C";
            dataTable.Columns["DT_IMMNG"].DefaultValue = "19900101";
            dataTable.Columns["FG_BF"].DefaultValue = str;
            dataTable.Columns["YN_PHANTOM"].DefaultValue = "N";
            dataTable.Columns["FG_LONG"].DefaultValue = "N";
            dataTable.Columns["FG_TRACKING"].DefaultValue = "N";
            dataTable.Columns["CLS_PO"].DefaultValue = "L4L";
            dataTable.Columns["FG_LOTNO"].DefaultValue = "N";
            dataTable.Columns["LOTSIZE"].DefaultValue = 1;
            dataTable.Columns["FG_SERNO"].DefaultValue = "1";
            dataTable.Columns["FG_PQC"].DefaultValue = "N";
            dataTable.Columns["TP_MANU"].DefaultValue = "MTS";
            dataTable.Columns["FG_GIR"].DefaultValue = "Y";
            dataTable.Columns["FG_MODEL"].DefaultValue = "N";
            dataTable.Columns["FG_IQC"].DefaultValue = "N";
            dataTable.Columns["FG_SQC"].DefaultValue = "N";
            dataTable.Columns["FG_PQC"].DefaultValue = "N";
            dataTable.Columns["FG_OQC"].DefaultValue = "N";
            dataTable.Columns["FG_OPQC"].DefaultValue = "N";
            dataTable.Columns["FG_SLQC"].DefaultValue = "N";
            dataTable.Columns["YN_FILE_ADD"].DefaultValue = "N";
            dataTable.Columns["TP_PO"].DefaultValue = "0";
            dataTable.Columns["YN_ATP"].DefaultValue = "N";
            dataTable.Columns["CD_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaCode;
            dataTable.Columns["PT_CD_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaCode;
            dataTable.Columns["FG_PQCB"].DefaultValue = "N";
            return dataTable;
        }

        internal DataTable Search버전관리(object[] args)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_MA_PITEM_VERSIONE_S", args);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal string UploadPath(string fileName)
        {
            fileName = HttpUtility.UrlEncode(fileName, Encoding.GetEncoding("EUC-KR"));
            return Global.MainFrame.HostURL + "/admin/FileUploader.aspx?File=" + fileName + "&Dir=/" + this.ServerDir + "&Action=etc&date=20090819";
        }

        internal string UploadPlantPath(string fileName, string str공장)
        {
            fileName = HttpUtility.UrlEncode(fileName, Encoding.GetEncoding("EUC-KR"));
            return Global.MainFrame.HostURL + "/admin/FileUploader.aspx?File=" + fileName + "&Dir=/" + this.ServerPlantDir(D.GetString(str공장)) + "&Action=etc&date=20090819";
        }

        internal bool Save(DataTable dt, DataTable dt버전관리)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            if (dt != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dt;
                dt.RemotingFormat = SerializationFormat.Binary;
                spInfo.CompanyID = this.회사코드;
                spInfo.UserID = MA.Login.사원번호;
                spInfo.SpNameInsert = "UP_MA_PITEM_INSERT";
                spInfo.SpNameUpdate = "UP_MA_PITEM_UPDATE";
                spInfo.SpNameDelete = "UP_MA_PITEM_DELETE";
                spInfo.SpParamsInsert = new string[179]
                {
          "CD_ITEM",
          "CD_PLANT",
          "CD_COMPANY",
          "NM_ITEM",
          "EN_ITEM",
          "CLS_ITEM",
          "TP_PROC",
          "TP_ITEM",
          "UNIT_IM",
          "UNIT_SO",
          "UNIT_PO",
          "UNIT_GI",
          "UNIT_MO",
          "GRP_ITEM",
          "MAT_ITEM",
          "STND_ITEM",
          "NO_HS",
          "UNIT_HS",
          "WEIGHT",
          "UNIT_WEIGHT",
          "URL_ITEM",
          "TP_PART",
          "BARCODE",
          "DT_VALID",
          "NO_DESIGN",
          "YN_USE",
          "CD_PURGRP",
          "CD_SL",
          "CD_GISL",
          "LT_GI",
          "FG_ABC",
          "QT_SSTOCK",
          "LT_SAFE",
          "DY_IMCLY",
          "DT_IMMNG",
          "QT_ROP",
          "FG_BF",
          "DY_VALID",
          "NO_MODEL",
          "FG_GIR",
          "CLS_L",
          "CLS_M",
          "CLS_S",
          "TP_MANU",
          "YN_PHANTOM",
          "TP_BOM",
          "FG_LONG",
          "QT_MIN",
          "QT_MAX",
          "RT_QM",
          "PARTNER",
          "FG_TRACKING",
          "RT_MINUS",
          "RT_PLUS",
          "LT_ITEM",
          "TP_PO",
          "CLS_PO",
          "QT_FOQ",
          "FG_FOQ",
          "DY_POQ",
          "PATN_ROUT",
          "FG_LOTNO",
          "LOTSIZE",
          "FG_SERNO",
          "STAND_PRC",
          "UPD",
          "UPH",
          "LT_QC",
          "NO_STND",
          "FG_PQC",
          "FG_SQC",
          "FG_OQC",
          "ID_INSERT",
          "PT_CD_BIZAREA",
          "UNIT_SO_FACT",
          "UNIT_PO_FACT",
          "UNIT_GI_FACT",
          "GRP_MFG",
          "CD_ZONE",
          "FG_TAX_PU",
          "FILE_PATH_MNG",
          "NO_MANAGER1",
          "NO_MANAGER2",
          "FG_MODEL",
          "FG_IQC",
          "FG_TAX_SA",
          "STND_DETAIL_ITEM",
          "YN_ATP",
          "CUR_ATP_DAY",
          "NM_MAKER",
          "NM_USERDEF1",
          "NM_USERDEF2",
          "NM_USERDEF3",
          "NM_USERDEF4",
          "NM_USERDEF5",
          "CD_USERDEF1",
          "CD_USERDEF2",
          "CD_USERDEF3",
          "CD_USERDEF4",
          "CD_USERDEF5",
          "CD_USERDEF6",
          "CD_USERDEF7",
          "CD_USERDEF8",
          "CD_USERDEF9",
          "CD_USERDEF10",
          "CD_USERDEF11",
          "CD_USERDEF12",
          "CD_USERDEF13",
          "CD_USERDEF14",
          "CD_USERDEF15",
          "CD_USERDEF16",
          "CD_USERDEF17",
          "CD_USERDEF18",
          "CD_USERDEF19",
          "CD_USERDEF20",
          "NUM_USERDEF1",
          "NUM_USERDEF2",
          "NUM_USERDEF3",
          "NUM_USERDEF4",
          "NUM_USERDEF5",
          "NUM_USERDEF6",
          "NUM_USERDEF7",
          "NUM_USERDEF8",
          "NUM_USERDEF9",
          "NUM_USERDEF10",
          "STND_ST",
          "DT_STAND_CO",
          "IMAGE1",
          "IMAGE2",
          "IMAGE3",
          "IMAGE4",
          "IMAGE5",
          "IMAGE6",
          "DC_IMAGE1",
          "DC_IMAGE2",
          "DC_IMAGE3",
          "DC_IMAGE4",
          "DC_IMAGE5",
          "DC_IMAGE6",
          "FG_OPQC",
          "QT_LENGTH",
          "QT_WIDTH",
          "CD_CORE",
          "CD_TP",
          "FG_SLQC",
          "DC_RMK",
          "DT_ST",
          "CD_CC",
          "FG_IQCL",
          "BOTH_SERLOT",
          "INSERT_ID",
          "UPDATE_ID",
          "DTS_INSERT",
          "DTS_UPDATE",
          "UM_ROYALTY",
          "CD_ITEM_RELATION",
          "CD_STND_ITEM_1",
          "CD_STND_ITEM_2",
          "CD_STND_ITEM_3",
          "CD_STND_ITEM_4",
          "CD_STND_ITEM_5",
          "NUM_STND_ITEM_1",
          "NUM_STND_ITEM_2",
          "NUM_STND_ITEM_3",
          "NUM_STND_ITEM_4",
          "NUM_STND_ITEM_5",
          "CD_USERDEF21",
          "CD_USERDEF22",
          "UNIT_SU",
          "UNIT_SU_FACT",
          "FG_PQCB",
          "NM_ITEM_L1",
          "NM_ITEM_L2",
          "NM_ITEM_L3",
          "NM_ITEM_L4",
          "NM_ITEM_L5",
          "CD_USERDEF23",
          "CD_USERDEF24",
          "UNIT_MO_FACT"
                };
                spInfo.SpParamsUpdate = new string[180]
                {
          "CD_ITEM",
          "CD_PLANT",
          "CD_COMPANY",
          "NM_ITEM",
          "EN_ITEM",
          "CLS_ITEM",
          "TP_PROC",
          "TP_ITEM",
          "UNIT_IM",
          "UNIT_SO",
          "UNIT_PO",
          "UNIT_GI",
          "UNIT_MO",
          "GRP_ITEM",
          "MAT_ITEM",
          "STND_ITEM",
          "NO_HS",
          "UNIT_HS",
          "WEIGHT",
          "UNIT_WEIGHT",
          "URL_ITEM",
          "TP_PART",
          "BARCODE",
          "DT_VALID",
          "NO_DESIGN",
          "YN_USE",
          "CD_PURGRP",
          "CD_SL",
          "CD_GISL",
          "LT_GI",
          "FG_ABC",
          "QT_SSTOCK",
          "LT_SAFE",
          "DY_IMCLY",
          "DT_IMMNG",
          "QT_ROP",
          "FG_BF",
          "DY_VALID",
          "NO_MODEL",
          "FG_GIR",
          "CLS_L",
          "CLS_M",
          "CLS_S",
          "TP_MANU",
          "YN_PHANTOM",
          "TP_BOM",
          "FG_LONG",
          "QT_MIN",
          "QT_MAX",
          "RT_QM",
          "PARTNER",
          "FG_TRACKING",
          "RT_MINUS",
          "RT_PLUS",
          "LT_ITEM",
          "TP_PO",
          "CLS_PO",
          "QT_FOQ",
          "FG_FOQ",
          "DY_POQ",
          "PATN_ROUT",
          "FG_LOTNO",
          "LOTSIZE",
          "FG_SERNO",
          "STAND_PRC",
          "UPD",
          "UPH",
          "LT_QC",
          "NO_STND",
          "FG_PQC",
          "FG_SQC",
          "FG_OQC",
          "ID_UPDATE",
          "PT_CD_BIZAREA",
          "UNIT_SO_FACT",
          "UNIT_PO_FACT",
          "UNIT_GI_FACT",
          "GRP_MFG",
          "CD_ZONE",
          "FG_TAX_PU",
          "FILE_PATH_MNG",
          "NO_MANAGER1",
          "NO_MANAGER2",
          "FG_MODEL",
          "FG_IQC",
          "FG_TAX_SA",
          "STND_DETAIL_ITEM",
          "YN_ATP",
          "CUR_ATP_DAY",
          "NM_MAKER",
          "NM_USERDEF1",
          "NM_USERDEF2",
          "NM_USERDEF3",
          "NM_USERDEF4",
          "NM_USERDEF5",
          "CD_USERDEF1",
          "CD_USERDEF2",
          "CD_USERDEF3",
          "CD_USERDEF4",
          "CD_USERDEF5",
          "CD_USERDEF6",
          "CD_USERDEF7",
          "CD_USERDEF8",
          "CD_USERDEF9",
          "CD_USERDEF10",
          "CD_USERDEF11",
          "CD_USERDEF12",
          "CD_USERDEF13",
          "CD_USERDEF14",
          "CD_USERDEF15",
          "CD_USERDEF16",
          "CD_USERDEF17",
          "CD_USERDEF18",
          "CD_USERDEF19",
          "CD_USERDEF20",
          "NUM_USERDEF1",
          "NUM_USERDEF2",
          "NUM_USERDEF3",
          "NUM_USERDEF4",
          "NUM_USERDEF5",
          "NUM_USERDEF6",
          "NUM_USERDEF7",
          "NUM_USERDEF8",
          "NUM_USERDEF9",
          "NUM_USERDEF10",
          "STND_ST",
          "DT_STAND_CO",
          "IMAGE1",
          "IMAGE2",
          "IMAGE3",
          "IMAGE4",
          "IMAGE5",
          "IMAGE6",
          "DC_IMAGE1",
          "DC_IMAGE2",
          "DC_IMAGE3",
          "DC_IMAGE4",
          "DC_IMAGE5",
          "DC_IMAGE6",
          "FG_OPQC",
          "QT_LENGTH",
          "QT_WIDTH",
          "CD_CORE",
          "CD_TP",
          "FG_SLQC",
          "DC_RMK",
          "DT_ST",
          "CD_CC",
          "FG_IQCL",
          "BOTH_SERLOT",
          "INSERT_ID",
          "UPDATE_ID",
          "DTS_INSERT",
          "DTS_UPDATE",
          "EXCEL_EDIT",
          "UM_ROYALTY",
          "CD_ITEM_RELATION",
          "CD_STND_ITEM_1",
          "CD_STND_ITEM_2",
          "CD_STND_ITEM_3",
          "CD_STND_ITEM_4",
          "CD_STND_ITEM_5",
          "NUM_STND_ITEM_1",
          "NUM_STND_ITEM_2",
          "NUM_STND_ITEM_3",
          "NUM_STND_ITEM_4",
          "NUM_STND_ITEM_5",
          "CD_USERDEF21",
          "CD_USERDEF22",
          "UNIT_SU",
          "UNIT_SU_FACT",
          "FG_PQCB",
          "NM_ITEM_L1",
          "NM_ITEM_L2",
          "NM_ITEM_L3",
          "NM_ITEM_L4",
          "NM_ITEM_L5",
          "CD_USERDEF23",
          "CD_USERDEF24",
          "UNIT_MO_FACT"
                };
                spInfo.SpParamsDelete = new string[3]
                {
          "CD_ITEM",
          "CD_PLANT",
          "CD_COMPANY"
                };
                spInfoCollection.Add(spInfo);
            }
            if (dt버전관리 != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dt버전관리;
                dt버전관리.RemotingFormat = SerializationFormat.Binary;
                spInfo.CompanyID = this.회사코드;
                spInfo.UserID = MA.Login.사원번호;
                spInfo.SpNameInsert = "UP_MA_PITEM_VERSIONE_INSERT";
                spInfo.SpNameUpdate = "UP_MA_PITEM_VERSIONE_UPDATE";
                spInfo.SpNameDelete = "UP_MA_PITEM_VERSIONE_DELETE";
                spInfo.SpParamsInsert = new string[18]
                {
          "CD_COMPANY",
          "CD_PLANT",
          "CD_ITEM",
          "SEQ",
          "NM_VERSION",
          "NM_LANGUAGE",
          "NM_ITEM_TYPE",
          "NM_OS",
          "ID_DEV_EMP",
          "ID_PMGM_EMP",
          "ID_SMGM_EMP",
          "FG_LVL",
          "YN_FINL_SOURCE",
          "YN_USE",
          "DTS_DSTB",
          "DC_ITEM_REMARK",
          "DC_REMARK",
          "ID_INSERT"
                };
                spInfo.SpParamsUpdate = new string[18]
                {
          "CD_COMPANY",
          "CD_PLANT",
          "CD_ITEM",
          "SEQ",
          "NM_VERSION",
          "NM_LANGUAGE",
          "NM_ITEM_TYPE",
          "NM_OS",
          "ID_DEV_EMP",
          "ID_PMGM_EMP",
          "ID_SMGM_EMP",
          "FG_LVL",
          "YN_FINL_SOURCE",
          "YN_USE",
          "DTS_DSTB",
          "DC_ITEM_REMARK",
          "DC_REMARK",
          "ID_UPDATE"
                };
                spInfo.SpParamsDelete = new string[4]
                {
          "CD_COMPANY",
          "CD_PLANT",
          "CD_ITEM",
          "SEQ"
                };
                spInfoCollection.Add(spInfo);
            }
            if (BASIC.GetMAEXC("공장품목등록-회사간품목전송") == "100")
            {
                SpInfo spInfo1 = new SpInfo();
                SpInfo spInfo2 = new SpInfo();
                spInfo2.DataValue = dt;
                dt.RemotingFormat = SerializationFormat.Binary;
                spInfo2.CompanyID = this.회사코드;
                spInfo2.UserID = MA.Login.사원번호;
                spInfo2.SpNameInsert = "UP_MA_PITEM_CT_I";
                spInfo2.SpNameUpdate = "UP_MA_PITEM_CT_U";
                spInfo2.SpNameDelete = "UP_MA_PITEM_CT_D";
                spInfo2.SpParamsInsert = new string[168]
                {
          "CD_ITEM",
          "CD_PLANT",
          "CD_COMPANY",
          "NM_ITEM",
          "EN_ITEM",
          "CLS_ITEM",
          "TP_PROC",
          "TP_ITEM",
          "UNIT_IM",
          "UNIT_SO",
          "UNIT_PO",
          "UNIT_GI",
          "UNIT_MO",
          "GRP_ITEM",
          "MAT_ITEM",
          "STND_ITEM",
          "NO_HS",
          "UNIT_HS",
          "WEIGHT",
          "UNIT_WEIGHT",
          "URL_ITEM",
          "TP_PART",
          "BARCODE",
          "DT_VALID",
          "NO_DESIGN",
          "YN_USE",
          "CD_PURGRP",
          "CD_SL",
          "CD_GISL",
          "LT_GI",
          "FG_ABC",
          "QT_SSTOCK",
          "LT_SAFE",
          "DY_IMCLY",
          "DT_IMMNG",
          "QT_ROP",
          "FG_BF",
          "DY_VALID",
          "NO_MODEL",
          "FG_GIR",
          "CLS_L",
          "CLS_M",
          "CLS_S",
          "TP_MANU",
          "YN_PHANTOM",
          "TP_BOM",
          "FG_LONG",
          "QT_MIN",
          "QT_MAX",
          "RT_QM",
          "PARTNER",
          "FG_TRACKING",
          "RT_MINUS",
          "RT_PLUS",
          "LT_ITEM",
          "TP_PO",
          "CLS_PO",
          "QT_FOQ",
          "FG_FOQ",
          "DY_POQ",
          "PATN_ROUT",
          "FG_LOTNO",
          "LOTSIZE",
          "FG_SERNO",
          "STAND_PRC",
          "UPD",
          "UPH",
          "LT_QC",
          "NO_STND",
          "FG_PQC",
          "FG_SQC",
          "FG_OQC",
          "ID_INSERT",
          "PT_CD_BIZAREA",
          "UNIT_SO_FACT",
          "UNIT_PO_FACT",
          "UNIT_GI_FACT",
          "GRP_MFG",
          "CD_ZONE",
          "FG_TAX_PU",
          "FILE_PATH_MNG",
          "NO_MANAGER1",
          "NO_MANAGER2",
          "FG_MODEL",
          "FG_IQC",
          "FG_TAX_SA",
          "STND_DETAIL_ITEM",
          "YN_ATP",
          "CUR_ATP_DAY",
          "NM_MAKER",
          "NM_USERDEF1",
          "NM_USERDEF2",
          "NM_USERDEF3",
          "NM_USERDEF4",
          "NM_USERDEF5",
          "CD_USERDEF1",
          "CD_USERDEF2",
          "CD_USERDEF3",
          "CD_USERDEF4",
          "CD_USERDEF5",
          "CD_USERDEF6",
          "CD_USERDEF7",
          "CD_USERDEF8",
          "CD_USERDEF9",
          "CD_USERDEF10",
          "CD_USERDEF11",
          "CD_USERDEF12",
          "CD_USERDEF13",
          "CD_USERDEF14",
          "CD_USERDEF15",
          "CD_USERDEF16",
          "CD_USERDEF17",
          "CD_USERDEF18",
          "CD_USERDEF19",
          "CD_USERDEF20",
          "NUM_USERDEF1",
          "NUM_USERDEF2",
          "NUM_USERDEF3",
          "NUM_USERDEF4",
          "NUM_USERDEF5",
          "NUM_USERDEF6",
          "NUM_USERDEF7",
          "NUM_USERDEF8",
          "NUM_USERDEF9",
          "NUM_USERDEF10",
          "STND_ST",
          "DT_STAND_CO",
          "IMAGE1",
          "IMAGE2",
          "IMAGE3",
          "IMAGE4",
          "IMAGE5",
          "IMAGE6",
          "DC_IMAGE1",
          "DC_IMAGE2",
          "DC_IMAGE3",
          "DC_IMAGE4",
          "DC_IMAGE5",
          "DC_IMAGE6",
          "FG_OPQC",
          "QT_LENGTH",
          "QT_WIDTH",
          "CD_CORE",
          "CD_TP",
          "FG_SLQC",
          "DC_RMK",
          "DT_ST",
          "CD_CC",
          "FG_IQCL",
          "BOTH_SERLOT",
          "INSERT_ID",
          "UPDATE_ID",
          "DTS_INSERT",
          "DTS_UPDATE",
          "UM_ROYALTY",
          "CD_ITEM_RELATION",
          "CD_STND_ITEM_1",
          "CD_STND_ITEM_2",
          "CD_STND_ITEM_3",
          "CD_STND_ITEM_4",
          "CD_STND_ITEM_5",
          "NUM_STND_ITEM_1",
          "NUM_STND_ITEM_2",
          "NUM_STND_ITEM_3",
          "NUM_STND_ITEM_4",
          "NUM_STND_ITEM_5",
          "CD_USERDEF21",
          "CD_USERDEF22"
                };
                spInfo2.SpParamsUpdate = new string[168]
                {
          "CD_ITEM",
          "CD_PLANT",
          "CD_COMPANY",
          "NM_ITEM",
          "EN_ITEM",
          "CLS_ITEM",
          "TP_PROC",
          "TP_ITEM",
          "UNIT_IM",
          "UNIT_SO",
          "UNIT_PO",
          "UNIT_GI",
          "UNIT_MO",
          "GRP_ITEM",
          "MAT_ITEM",
          "STND_ITEM",
          "NO_HS",
          "UNIT_HS",
          "WEIGHT",
          "UNIT_WEIGHT",
          "URL_ITEM",
          "TP_PART",
          "BARCODE",
          "DT_VALID",
          "NO_DESIGN",
          "YN_USE",
          "CD_PURGRP",
          "CD_SL",
          "CD_GISL",
          "LT_GI",
          "FG_ABC",
          "QT_SSTOCK",
          "LT_SAFE",
          "DY_IMCLY",
          "DT_IMMNG",
          "QT_ROP",
          "FG_BF",
          "DY_VALID",
          "NO_MODEL",
          "FG_GIR",
          "CLS_L",
          "CLS_M",
          "CLS_S",
          "TP_MANU",
          "YN_PHANTOM",
          "TP_BOM",
          "FG_LONG",
          "QT_MIN",
          "QT_MAX",
          "RT_QM",
          "PARTNER",
          "FG_TRACKING",
          "RT_MINUS",
          "RT_PLUS",
          "LT_ITEM",
          "TP_PO",
          "CLS_PO",
          "QT_FOQ",
          "FG_FOQ",
          "DY_POQ",
          "PATN_ROUT",
          "FG_LOTNO",
          "LOTSIZE",
          "FG_SERNO",
          "STAND_PRC",
          "UPD",
          "UPH",
          "LT_QC",
          "NO_STND",
          "FG_PQC",
          "FG_SQC",
          "FG_OQC",
          "ID_UPDATE",
          "PT_CD_BIZAREA",
          "UNIT_SO_FACT",
          "UNIT_PO_FACT",
          "UNIT_GI_FACT",
          "GRP_MFG",
          "CD_ZONE",
          "FG_TAX_PU",
          "FILE_PATH_MNG",
          "NO_MANAGER1",
          "NO_MANAGER2",
          "FG_MODEL",
          "FG_IQC",
          "FG_TAX_SA",
          "STND_DETAIL_ITEM",
          "YN_ATP",
          "CUR_ATP_DAY",
          "NM_MAKER",
          "NM_USERDEF1",
          "NM_USERDEF2",
          "NM_USERDEF3",
          "NM_USERDEF4",
          "NM_USERDEF5",
          "CD_USERDEF1",
          "CD_USERDEF2",
          "CD_USERDEF3",
          "CD_USERDEF4",
          "CD_USERDEF5",
          "CD_USERDEF6",
          "CD_USERDEF7",
          "CD_USERDEF8",
          "CD_USERDEF9",
          "CD_USERDEF10",
          "CD_USERDEF11",
          "CD_USERDEF12",
          "CD_USERDEF13",
          "CD_USERDEF14",
          "CD_USERDEF15",
          "CD_USERDEF16",
          "CD_USERDEF17",
          "CD_USERDEF18",
          "CD_USERDEF19",
          "CD_USERDEF20",
          "NUM_USERDEF1",
          "NUM_USERDEF2",
          "NUM_USERDEF3",
          "NUM_USERDEF4",
          "NUM_USERDEF5",
          "NUM_USERDEF6",
          "NUM_USERDEF7",
          "NUM_USERDEF8",
          "NUM_USERDEF9",
          "NUM_USERDEF10",
          "STND_ST",
          "DT_STAND_CO",
          "IMAGE1",
          "IMAGE2",
          "IMAGE3",
          "IMAGE4",
          "IMAGE5",
          "IMAGE6",
          "DC_IMAGE1",
          "DC_IMAGE2",
          "DC_IMAGE3",
          "DC_IMAGE4",
          "DC_IMAGE5",
          "DC_IMAGE6",
          "FG_OPQC",
          "QT_LENGTH",
          "QT_WIDTH",
          "CD_CORE",
          "CD_TP",
          "FG_SLQC",
          "DC_RMK",
          "DT_ST",
          "CD_CC",
          "FG_IQCL",
          "BOTH_SERLOT",
          "INSERT_ID",
          "UPDATE_ID",
          "DTS_INSERT",
          "DTS_UPDATE",
          "UM_ROYALTY",
          "CD_ITEM_RELATION",
          "CD_STND_ITEM_1",
          "CD_STND_ITEM_2",
          "CD_STND_ITEM_3",
          "CD_STND_ITEM_4",
          "CD_STND_ITEM_5",
          "NUM_STND_ITEM_1",
          "NUM_STND_ITEM_2",
          "NUM_STND_ITEM_3",
          "NUM_STND_ITEM_4",
          "NUM_STND_ITEM_5",
          "CD_USERDEF21",
          "CD_USERDEF22"
                };
                spInfo2.SpParamsDelete = new string[3]
                {
          "CD_ITEM",
          "CD_PLANT",
          "CD_COMPANY"
                };
                spInfoCollection.Add(spInfo2);
            }
            return DBHelper.Save(spInfoCollection);
        }

        internal void Save_Bulk(DataTable dtSave1, DataTable dtSave2, string 공장)
        {
            if (!dtSave1.Columns.Contains("CD_COMPANY"))
                dtSave1.Columns.Add("CD_COMPANY");
            foreach (DataRow row in (InternalDataCollectionBase)dtSave1.Rows)
                row["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
            DBHelper.save_bulk("MA_PITEM", dtSave1);
            if (dtSave2 == null || dtSave2.Rows.Count == 0)
                return;
            if (!dtSave2.Columns.Contains("CD_COMPANY"))
                dtSave2.Columns.Add("CD_COMPANY");
            foreach (DataRow row in (InternalDataCollectionBase)dtSave2.Rows)
                row["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
            DBHelper.save_bulk("MA_PITEM_VERSIONE", dtSave2);
        }

        internal bool ExistsITEM(DataTable dt엑셀, out string msg, out DataTable dtExists)
        {
            msg = string.Empty;
            dtExists = (DataTable)null;
            DataTable table = dt엑셀.DefaultView.ToTable(true, "CD_PLANT");
            DataTable dataTable1 = (DataTable)null;
            foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
            {
                string str1 = D.GetString(row["CD_PLANT"]);
                DataRow[] dataRowArray = dt엑셀.Select("CD_PLANT = '" + str1 + "'");
                string str2 = "";
                Decimal num = 1M;
                foreach (DataRow dataRow in dataRowArray)
                {
                    str2 = str2 + D.GetString(dataRow["CD_ITEM"]) + "|";
                    if (num % 200M == 0M)
                    {
                        DataTable dataTable2 = DBHelper.GetDataTable("UP_SA_MA_PITEM_S", new object[3]
                        {
               this.회사코드,
               str1,
               str2
                        }, "CD_ITEM");
                        if (dataTable1 == null)
                            dataTable1 = dataTable2;
                        else
                            dataTable1.Merge(dataTable2);
                        str2 = string.Empty;
                    }
                    ++num;
                }
                DataTable dataTable3 = DBHelper.GetDataTable("UP_SA_MA_PITEM_S", new object[3]
                {
           this.회사코드,
           str1,
           str2
                }, "CD_ITEM");
                if (dataTable1 == null)
                    dataTable1 = dataTable3;
                else
                    dataTable1.Merge(dataTable3);
            }
            if (dataTable1 == null || dataTable1.Rows.Count <= 0)
                return false;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
                stringBuilder.AppendLine(D.GetString(row["CD_ITEM"]));
            msg = stringBuilder.ToString();
            dtExists = dataTable1.Copy();
            return true;
        }

        internal void Set엑셀체크데이터()
        {
            if (this.dt계정구분 != null)
                return;
            this.dt계정구분 = MA.GetCode("MA_B000010");
            this.dt계정구분.PrimaryKey = new DataColumn[1]
            {
        this.dt계정구분.Columns["CODE"]
            };
            this.dt품목군 = DBHelper.GetDataTable("SELECT CD_ITEMGRP, NM_ITEMGRP FROM MA_ITEMGRP WHERE CD_COMPANY = '" + this.회사코드 + "'");
            this.dt품목군.PrimaryKey = new DataColumn[1]
            {
        this.dt품목군.Columns["CD_ITEMGRP"]
            };
            this.dt창고 = DBHelper.GetDataTable("SELECT CD_PLANT, CD_SL, NM_SL FROM MA_SL WHERE CD_COMPANY = '" + this.회사코드 + "'");
            this.dt창고.PrimaryKey = new DataColumn[2]
            {
        this.dt창고.Columns["CD_PLANT"],
        this.dt창고.Columns["CD_SL"]
            };
            this._dtClsL = DBHelper.GetDataTable("SELECT * FROM MA_CODEDTL WHERE CD_COMPANY = '" + this.회사코드 + "' AND CD_FIELD = 'MA_B000030'");
            this._dtClsM = DBHelper.GetDataTable("SELECT *  FROM MA_CODEDTL WHERE CD_COMPANY = '" + this.회사코드 + "' AND CD_FIELD = 'MA_B000031'");
            this._dtClsS = DBHelper.GetDataTable("SELECT *  FROM MA_CODEDTL WHERE CD_COMPANY = '" + this.회사코드 + "' AND CD_FIELD = 'MA_B000032'");
        }

        internal bool Chk계정구분(DataRow row품목, out string msg계정구분)
        {
            msg계정구분 = "";
            if (this.dt계정구분.Rows.Find(row품목["CLS_ITEM"]) != null)
                return true;
            msg계정구분 = D.GetString(row품목["CD_ITEM"]);
            return false;
        }

        internal bool Chk품목군(DataRow row품목, out string 품목군명, out string msg품목군)
        {
            msg품목군 = 품목군명 = "";
            if (D.GetString(row품목["GRP_ITEM"]) == string.Empty)
                return true;
            DataRow dataRow = this.dt품목군.Rows.Find(row품목["GRP_ITEM"]);
            if (dataRow == null)
            {
                msg품목군 = D.GetString(row품목["CD_ITEM"]);
                return false;
            }
            품목군명 = D.GetString(dataRow["NM_ITEMGRP"]);
            return true;
        }

        internal bool Chk창고(DataRow row품목, string ColName, out string NameCol, out string msg창고)
        {
            msg창고 = NameCol = "";
            if (D.GetString(row품목[ColName]) == string.Empty)
                return true;
            DataRow dataRow = this.dt창고.Rows.Find(new object[2]
            {
        row품목["CD_PLANT"],
        row품목[ColName]
            });
            if (dataRow == null)
            {
                msg창고 = D.GetString(row품목["CD_ITEM"]);
                return false;
            }
            NameCol = D.GetString(dataRow["NM_SL"]);
            return true;
        }

        internal bool ChkCls(DataRow row품목, out string msgCls)
        {
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string empty3 = string.Empty;
            DataRow[] dataRowArray1 = this._dtClsL.Select("CD_SYSDEF = '" + D.GetString(row품목["CLS_L"]) + "'");
            if (dataRowArray1 != null && dataRowArray1.Length > 0)
                empty1 = D.GetString(dataRowArray1[0]["CD_FLAG2"]);
            DataRow[] dataRowArray2 = this._dtClsM.Select("CD_SYSDEF = '" + D.GetString(row품목["CLS_M"]) + "'");
            if (dataRowArray2 != null && dataRowArray2.Length > 0)
                empty2 = D.GetString(dataRowArray2[0]["CD_FLAG2"]);
            DataRow[] dataRowArray3 = this._dtClsS.Select("CD_SYSDEF = '" + D.GetString(row품목["CLS_S"]) + "'");
            if (dataRowArray3 != null && dataRowArray3.Length > 0)
                empty3 = D.GetString(dataRowArray3[0]["CD_FLAG2"]);
            msgCls = "";
            string str = empty1 + empty2 + empty3;
            if (string.IsNullOrEmpty(str))
            {
                msgCls = D.GetString(row품목["CD_ITEM"]);
                return false;
            }
            int length = str.Length;
            if (!(str != D.GetString(row품목["CD_ITEM"]).Substring(0, length)))
                return true;
            msgCls = D.GetString(row품목["CD_ITEM"]);
            return false;
        }

        internal string ServerDir => "shared/MF_File_Mng/" + this.회사코드 + "/PITEM";

        internal string ServerPlantDir(string 공장코드) => "shared/MF_File_Mng/" + this.회사코드 + "/" + 공장코드 + "/PITEM";

        internal bool PDMApply(string 공장코드) => DBHelper.ExecuteNonQuery("UP_Z_WOORIERP_PDM_APPLY", new object[3]
        {
       MA.Login.회사코드,
       공장코드,
       Global.MainFrame.LoginInfo.UserID
        });

        internal string GetMaxCdItem(
          string 공장코드,
          string bigclass,
          string middleclass,
          string smallclass,
          int sublength,
          int seqlength)
        {
            return D.GetString(DBHelper.GetDataTable("SELECT MAX(SUBSTRING(CD_ITEM, " + (sublength + 1).ToString() + ", " + seqlength.ToString() + ")) NO_SEQ FROM MA_PITEM WHERE CD_COMPANY = '" + this.회사코드 + "' AND CD_PLANT = '" + 공장코드 + "' AND CLS_L = '" + bigclass + "' AND CLS_M = '" + middleclass + "' AND CLS_S = '" + smallclass + "'").Rows[0]["NO_SEQ"]);
        }

        internal string GetCdFlag2(string code, string type)
        {
            string str = "";
            switch (type)
            {
                case "GRP_ITEM":
                    str = "MA_B000066";
                    break;
                case "CLS_L":
                    str = "MA_B000030";
                    break;
                case "CLS_M":
                    str = "MA_B000031";
                    break;
                case "CLS_S":
                    str = "MA_B000032";
                    break;
                case "CLS_ITEM":
                    str = "MA_B000010";
                    break;
            }
            DataTable dataTable = DBHelper.GetDataTable("SELECT CD_FLAG2  FROM MA_CODEDTL  WHERE CD_COMPANY = '" + this.회사코드 + "' AND CD_FIELD = '" + str + "' AND CD_SYSDEF = '" + code + "'");
            return dataTable == null || dataTable.Rows.Count == 0 ? "" : D.GetString(dataTable.Rows[0]["CD_FLAG2"]);
        }

        internal string GetCLS(string cdclass) => D.GetString(DBHelper.GetDataTable("SELECT LEN_SEQ FROM MA_DOCUCTRL WHERE CD_COMPANY = '" + this.회사코드 + "'AND CD_MODULE = 'MA' AND CD_CLASS = '" + cdclass + "'").Rows[0]["LEN_SEQ"]);

        internal DataTable DBD_Search(object[] args)
        {
            DataTable dataTable = new DBDirectConnector().GetDataTable("UP_MA_PITEM_S", args);
            T.SetDefaultValue(dataTable);
            dataTable.Columns["TP_PROC"].DefaultValue = "P";
            dataTable.Columns["CLS_ITEM"].DefaultValue = "001";
            dataTable.Columns["TP_ITEM"].DefaultValue = "SIN";
            dataTable.Columns["TP_PART"].DefaultValue = "P";
            dataTable.Columns["DT_VALID"].DefaultValue = "29991231";
            dataTable.Columns["YN_USE"].DefaultValue = "Y";
            dataTable.Columns["FG_ABC"].DefaultValue = "C";
            dataTable.Columns["DT_IMMNG"].DefaultValue = "19900101";
            dataTable.Columns["FG_BF"].DefaultValue = "N";
            dataTable.Columns["YN_PHANTOM"].DefaultValue = "N";
            dataTable.Columns["FG_LONG"].DefaultValue = "N";
            dataTable.Columns["FG_TRACKING"].DefaultValue = "N";
            dataTable.Columns["CLS_PO"].DefaultValue = "L4L";
            dataTable.Columns["FG_LOTNO"].DefaultValue = "N";
            dataTable.Columns["LOTSIZE"].DefaultValue = 1;
            dataTable.Columns["FG_SERNO"].DefaultValue = "1";
            dataTable.Columns["FG_PQC"].DefaultValue = "N";
            dataTable.Columns["TP_MANU"].DefaultValue = "MTS";
            dataTable.Columns["FG_GIR"].DefaultValue = "Y";
            dataTable.Columns["FG_MODEL"].DefaultValue = "N";
            dataTable.Columns["FG_IQC"].DefaultValue = "N";
            dataTable.Columns["FG_SQC"].DefaultValue = "N";
            dataTable.Columns["FG_PQC"].DefaultValue = "N";
            dataTable.Columns["FG_OQC"].DefaultValue = "N";
            dataTable.Columns["FG_OPQC"].DefaultValue = "N";
            dataTable.Columns["FG_SLQC"].DefaultValue = "N";
            dataTable.Columns["YN_FILE_ADD"].DefaultValue = "N";
            dataTable.Columns["TP_PO"].DefaultValue = "0";
            dataTable.Columns["YN_ATP"].DefaultValue = "N";
            dataTable.Columns["CD_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaCode;
            dataTable.Columns["PT_CD_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaCode;
            dataTable.Columns["FG_PQCB"].DefaultValue = "N";
            return dataTable;
        }

        internal Exception DBD_Test() => new DBDirectConnector().Test();

        internal DataTable ChkStndItem(object[] args)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_MA_PITEM_STNDITEM_CHK", args);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal string GetCdBizarea(string cdplant)
        {
            this.dt창고 = DBHelper.GetDataTable("SELECT CD_BIZAREA FROM MA_PLANT WHERE CD_COMPANY = '" + this.회사코드 + "'AND CD_PLANT = '" + cdplant + "'");
            return D.GetString(this.dt창고.Rows[0]["CD_BIZAREA"]);
        }

        internal bool IsDbDirect() => D.GetString(DBHelper.ExecuteScalar("SELECT YN_DIRECT FROM CM_SERVER_CONFIG WHERE SERVER_KEY = '" + Global.MainFrame.ServerKey.ToUpper() + "'")) == "Y";

        internal int GetDigitPitem()
        {
            int num = D.GetInt(DBHelper.ExecuteScalar("SELECT DIGIT_PITEM FROM MA_ENV WHERE CD_COMPANY = '" + this.회사코드 + "'"));
            if (num == 0)
                num = 20;
            return num;
        }

        internal DataTable GetCode(string key) => DBHelper.GetDataTable("SELECT CD_SYSDEF AS CODE, NM_SYSDEF AS NAME,CD_FLAG1, CD_FLAG2  FROM DZSN_MA_CODEDTL  WHERE CD_COMPANY = '" + this.회사코드 + "' AND CD_FIELD = '" + key + "' ORDER BY CD_SYSDEF");

        internal DataTable GetCompanyTrans(string cdcompany_f, string cdplant_f) => DBHelper.GetDataTable("UP_MA_PITEM_COMPANY_TRANS_S", new object[2]
        {
       cdcompany_f,
       cdplant_f
        });

        internal bool SaveCompanyTrans(DataTable dt)
        {
            SpInfoCollection spc = new SpInfoCollection();
            if (dt != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dt;
                spInfo.DataState = DataValueState.Added;
                dt.RemotingFormat = SerializationFormat.Binary;
                spInfo.UserID = MA.Login.사원번호;
                spInfo.SpNameInsert = "UP_MA_PITEM_COMPANY_TRANS_I";
                spInfo.SpNameUpdate = "UP_MA_PITEM_COMPANY_TRANS_I";
                spInfo.SpNameDelete = "UP_MA_PITEM_COMPANY_TRANS_D";
                spInfo.SpParamsInsert = new string[6]
                {
          "CD_COMPANY_F",
          "CD_PLANT_F",
          "CD_COMPANY_T",
          "CD_PLANT_T",
          "FG_TRANS",
          "ID_INSERT"
                };
                spInfo.SpParamsUpdate = new string[6]
                {
          "CD_COMPANY_F",
          "CD_PLANT_F",
          "CD_COMPANY_T",
          "CD_PLANT_T",
          "FG_TRANS",
          "ID_INSERT"
                };
                spInfo.SpParamsDelete = new string[4]
                {
          "CD_COMPANY_F",
          "CD_PLANT_F",
          "CD_COMPANY_T",
          "CD_PLANT_T"
                };
                spc.Add(spInfo);
            }
            return DBHelper.Save(spc);
        }
    }
}