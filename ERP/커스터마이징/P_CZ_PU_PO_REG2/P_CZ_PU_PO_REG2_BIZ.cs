using System;
using System.Data;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;

namespace cz
{
    internal class P_CZ_PU_PO_REG2_BIZ
    {
        private DataTable _dt_fg_post = new DataTable();
        private bool b_first_clone = false;
        private string Lang = Global.SystemLanguage.MultiLanguageLpoint.ToString();

        public DataSet Search(string NO_PO)
        {
            ResultData resultData = (ResultData)Global.MainFrame.FillDataSet("UP_PU_PO_REG_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                   NO_PO,
                                                                                                                   this.Lang });
            DataSet dataValue = (DataSet)resultData.DataValue;

            foreach (DataTable table in dataValue.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType == Type.GetType("System.Decimal"))
                        column.DefaultValue = !(column.ColumnName == "RT_PO") ? 0 : 1;
                    if (column.ColumnName == "FG_POCON")
                        column.DefaultValue = "001";
                    if (column.ColumnName == "FG_POST")
                        column.DefaultValue = "O";
                }
            }

            DataTable table1 = dataValue.Tables[0];
            
            table1.Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            table1.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            table1.Columns["NM_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptName;
            table1.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            table1.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            table1.Columns["DT_PO"].DefaultValue = Global.MainFrame.GetStringToday;
            table1.Columns["FG_TAXP"].DefaultValue = "001";
            table1.Columns["TP_PROCESS"].DefaultValue = "2";
            table1.Columns["YN_BUDGET"].DefaultValue = BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000007") == "100" ? "Y" : "N";
            table1.Columns["BUDGET_PASS"].DefaultValue = "N";
            
            dataValue.Tables[1].Columns["DATE_USERDEF1"].DefaultValue = Global.MainFrame.GetStringToday;
            dataValue.Tables[1].Columns["DATE_USERDEF2"].DefaultValue = Global.MainFrame.GetStringToday;
            dataValue.Tables[1].Columns["YN_BUDGET"].DefaultValue = BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000007") == "100" ? "Y" : "N";
            dataValue.Tables[1].Columns["BUDGET_PASS"].DefaultValue = "N";
            
            return (DataSet)resultData.DataValue;
        }

        public bool Delete(string NO_PO, string _text_sub)
        {
            Global.MainFrame.ExecSp("UP_PU_POH_DELETE", new object[] { NO_PO,
                                                                       Global.MainFrame.LoginInfo.CompanyCode,
                                                                       Global.MainFrame.LoginInfo.UserID,
                                                                       _text_sub });
            return true;
        }

        public bool Save(DataTable dtH, DataTable dtL, bool lb_RcvSave, DataTable dt_RCVH, DataTable dt_RCVL, string 구분, DataTable dt_budget, string 구매발주번호, SpInfo si_subinfo, bool lb_RevSave, DataTable dtLOT, string strNOIO)
        {
            SpInfoCollection spCollection = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo spInfo = new SpInfo();

                if (구분 == "COPY")
                    spInfo.DataState = DataValueState.Added;
                
                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "UP_PU_POH_I";
                spInfo.SpNameUpdate = "UP_PU_POH_UPDATE";
                spInfo.SpParamsInsert = new string[] { "NO_PO",
                                                       "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "CD_PARTNER",
                                                       "DT_PO",
                                                       "CD_PURGRP",
                                                       "NO_EMP",
                                                       "CD_TPPO",
                                                       "FG_UM",
                                                       "FG_PAYMENT",
                                                       "FG_TAX",
                                                       "TP_UM_TAX",
                                                       "CD_PJT",
                                                       "CD_EXCH",
                                                       "RT_EXCH",
                                                       "AM_EX",
                                                       "AM",
                                                       "VAT",
                                                       "DC50_PO",
                                                       "TP_PROCESS",
                                                       "FG_TAXP",
                                                       "YN_AM",
                                                       "DTS_INSERT",
                                                       "ID_USER",
                                                       "FG_TRANS",
                                                       "FG_TRACK",
                                                       "DC_RMK2",
                                                       "TP_TRANSPORT",
                                                       "COND_PAY",
                                                       "COND_PAY_DLV",
                                                       "COND_PRICE",
                                                       "ARRIVER",
                                                       "LOADING",
                                                       "YN_BUDGET",
                                                       "BUDGET_PASS",
                                                       "COND_PRICE_DLV",
                                                       "CD_ARRIVER",
                                                       "CD_LOADING",
                                                       "DC_RMK_TEXT",
                                                       "CD_AGENCY",
                                                       "AM_NEGO",
                                                       "COND_SHIPMENT",
                                                       "FREIGHT_CHARGE",
                                                       "DC_RMK_TEXT2",
                                                       "STND_PAY",
                                                       "COND_DAYS",
                                                       "CD_ORGIN",
                                                       "DELIVERY_TERMS",
                                                       "DELIVERY_TIME",
                                                       "VALIDITY",
                                                       "TP_PACKING",
                                                       "DELIVERY_COST",
                                                       "INSPECTION",
                                                       "DOCUMENT_REQUIRED",
                                                       "SUPPLIER",
                                                       "MANUFACTURER",
                                                       "NO_ORDER",
                                                       "NM_PACKING",
                                                       "SHIP_DATE",
                                                       "DACU_NO",
                                                       "TP_GR",
                                                       "DT_PROCESS_IV",
                                                       "DT_PAY_PRE_IV",
                                                       "DT_DUE_IV",
                                                       "FG_PAYBILL_IV",
                                                       "CD_DOCU_IV",
                                                       "AM_K_IV",
                                                       "VAT_TAX_IV",
                                                       "AM_EX_IV",
                                                       "TXT_USERDEF4",
                                                       "DC_RMK_IV",
                                                       "CD_USERDEF1",
                                                       "CD_USERDEF2",
                                                       "CD_USERDEF3",
                                                       "CD_USERDEF4",
                                                       "CD_BIZAREA_TAX",
                                                       "TXT_USERDEF3" };

                spInfo.SpParamsUpdate = new string[] { "NO_PO",
                                                       "CD_COMPANY",
                                                       "DT_PO",
                                                       "CD_PURGRP",
                                                       "NO_EMP",
                                                       "CD_PJT",
                                                       "AM_EX",
                                                       "AM",
                                                       "VAT",
                                                       "DC50_PO",
                                                       "FG_TAXP",
                                                       "DTS_INSERT",
                                                       "ID_USER",
                                                       "DC_RMK2",
                                                       "TP_TRANSPORT",
                                                       "COND_PAY",
                                                       "COND_PAY_DLV",
                                                       "COND_PRICE",
                                                       "ARRIVER",
                                                       "LOADING",
                                                       "YN_BUDGET",
                                                       "BUDGET_PASS",
                                                       "COND_PRICE_DLV",
                                                       "CD_ARRIVER",
                                                       "CD_LOADING",
                                                       "DC_RMK_TEXT",
                                                       "FG_PAYMENT",
                                                       "COND_SHIPMENT",
                                                       "FREIGHT_CHARGE",
                                                       "DC_RMK_TEXT2",
                                                       "STND_PAY",
                                                       "COND_DAYS",
                                                       "CD_ORGIN",
                                                       "DELIVERY_TERMS",
                                                       "DELIVERY_TIME",
                                                       "VALIDITY",
                                                       "TP_PACKING",
                                                       "DELIVERY_COST",
                                                       "INSPECTION",
                                                       "DOCUMENT_REQUIRED",
                                                       "SUPPLIER",
                                                       "MANUFACTURER",
                                                       "NO_ORDER",
                                                       "NM_PACKING",
                                                       "SHIP_DATE",
                                                       "DACU_NO",
                                                       "CD_USERDEF1",
                                                       "CD_USERDEF2",
                                                       "CD_USERDEF3",
                                                       "CD_USERDEF4",
                                                       "TXT_USERDEF3" };
                spCollection.Add(spInfo);

                spInfo.SpParamsValues.Add(ActionState.Insert, "ID_USER", Global.MainFrame.LoginInfo.UserID);
                spInfo.SpParamsValues.Add(ActionState.Update, "ID_USER", Global.MainFrame.LoginInfo.UserID);
            }
            if (dtL != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtL,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.UserID,
                    SpNameInsert = "UP_PU_POL_INSERT",
                    SpNameUpdate = "UP_PU_POL_UPDATE",
                    SpNameDelete = "UP_PU_POL_DELETE",
                    SpParamsInsert = new string[] { "NO_PO",
                                                    "NO_LINE",
                                                    "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_CONTRACT",
                                                    "NO_CTLINE",
                                                    "NO_PR",
                                                    "NO_PRLINE",
                                                    "FG_TRANS",
                                                    "CD_ITEM",
                                                    "CD_UNIT_MM",
                                                    "FG_RCV",
                                                    "FG_PURCHASE",
                                                    "DT_LIMIT",
                                                    "QT_PO_MM",
                                                    "QT_PO",
                                                    "QT_REQ",
                                                    "QT_RCV",
                                                    "FG_TAX",
                                                    "UM_EX_PO",
                                                    "UM_EX",
                                                    "AM_EX",
                                                    "UM",
                                                    "AM",
                                                    "VAT",
                                                    "CD_SL",
                                                    "FG_POST",
                                                    "FG_POCON",
                                                    "YN_RCV",
                                                    "YN_AUTORCV",
                                                    "YN_RETURN",
                                                    "YN_ORDER",
                                                    "YN_SUBCON",
                                                    "YN_IMPORT",
                                                    "RT_PO",
                                                    "YN_REQ",
                                                    "CD_PJT",
                                                    "NO_APP",
                                                    "NO_APPLINE",
                                                    "DC1",
                                                    "DC2",
                                                    "SEQ_PROJECT",
                                                    "UMVAT_PO",
                                                    "AMVAT_PO",
                                                    "CD_CC",
                                                    "CD_BUDGET",
                                                    "CD_BGACCT",
                                                    "NO_SO",
                                                    "NO_SOLINE",
                                                    "GI_PARTNER",
                                                    "DC3",
                                                    "DUMMY_TEMP_P_NO_VMI",
                                                    "DUMMY_TEMP_P_SEQ_VMI",
                                                    "DUMMY_TEMP_P_NO_IO_MGMT",
                                                    "DUMMY_TEMP_P_NO_IOLINE_MGMT",
                                                    "DT_PLAN",
                                                    "DC4",
                                                    "UM_EX_AR",
                                                    "NO_WBS",
                                                    "NO_CBS",
                                                    "CD_USERDEF1",
                                                    "CD_USERDEF2",
                                                    "NM_USERDEF1",
                                                    "NM_USERDEF2",
                                                    "DUMMY_TEMP_NO_PREIV",
                                                    "DUMMY_TEMP_NO_PREIVLINE",
                                                    "TP_UM_TAX",
                                                    "DT_EXDATE",
                                                    "CD_ITEM_ORIGIN",
                                                    "AM_EX_TRANS",
                                                    "AM_TRANS",
                                                    "NO_LINE_PJTBOM",
                                                    "FG_PACKING",
                                                    "NUM_USERDEF1",
                                                    "AM_REBATE_EX",
                                                    "AM_REBATE",
                                                    "UM_REBATE",
                                                    "ID_INSERT",
                                                    "DATE_USERDEF1",
                                                    "DATE_USERDEF2",
                                                    "TXT_USERDEF1",
                                                    "TXT_USERDEF2",
                                                    "CDSL_USERDEF1",
                                                    "NUM_USERDEF2",
                                                    "CLS_L",
                                                    "CLS_M",
                                                    "CLS_S",
                                                    "GRP_ITEM",
                                                    "NUM_STND_ITEM_1",
                                                    "NUM_STND_ITEM_2",
                                                    "NUM_STND_ITEM_3",
                                                    "NUM_STND_ITEM_4",
                                                    "NUM_STND_ITEM_5",
                                                    "UM_WEIGHT",
                                                    "WEIGHT",
                                                    "TOT_WEIGHT",
                                                    "STND_ITEM",
                                                    "NO_RELATION",
                                                    "SEQ_RELATION",
                                                    "NUM_USERDEF3_PO",
                                                    "NUM_USERDEF4_PO",
                                                    "NUM_USERDEF5_PO",
                                                    "NO_QUO",
                                                    "NO_QUOLINE",
                                                    "CD_BIZPLAN",
                                                    "CD_USERDEF3_PO",
                                                    "CD_USERDEF4_PO",
                                                    "NM_USERDEF3_PO",
                                                    "NM_USERDEF4_PO",
                                                    "YN_BUDGET",
                                                    "BUDGET_PASS",
                                                    "NO_IO",
                                                    "UNIT_IM",
                                                    "NM_ITEM",
                                                    "PAGE_ID",
                                                    "CD_ACCT",
                                                    "NM_USERDEF5",
                                                    "STND_DETAIL_ITEM",
                                                    "MAT_ITEM",
                                                    "DC50_PO" },
                    SpParamsValues = { { ActionState.Insert, "DUMMY_TEMP_P_NO_VMI", string.Empty },
                                       { ActionState.Insert, "DUMMY_TEMP_P_SEQ_VMI", 0 },
                                       { ActionState.Insert, "DUMMY_TEMP_P_NO_IO_MGMT", string.Empty },
                                       { ActionState.Insert, "DUMMY_TEMP_P_NO_IOLINE_MGMT", 0 },
                                       { ActionState.Insert, "DUMMY_TEMP_NO_PREIV", string.Empty },
                                       { ActionState.Insert, "DUMMY_TEMP_NO_PREIVLINE", 0 },
                                       { ActionState.Insert, "NO_IO", strNOIO },
                                       { ActionState.Insert, "PAGE_ID", Global.MainFrame.CurrentPageID } },

                    SpParamsUpdate = new string[] { "NO_PO",
                                                    "NO_LINE",
                                                    "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "CD_UNIT_MM",
                                                    "DT_LIMIT",
                                                    "QT_PO_MM",
                                                    "QT_PO",
                                                    "UM_EX_PO",
                                                    "UM_EX",
                                                    "AM_EX",
                                                    "UM",
                                                    "AM",
                                                    "VAT",
                                                    "CD_SL",
                                                    "RT_PO",
                                                    "CD_PJT",
                                                    "DC1",
                                                    "DC2",
                                                    "UMVAT_PO",
                                                    "AMVAT_PO",
                                                    "CD_CC",
                                                    "CD_BUDGET",
                                                    "CD_BGACCT",
                                                    "GI_PARTNER",
                                                    "DC3",
                                                    "DT_PLAN",
                                                    "DC4",
                                                    "UM_EX_AR",
                                                    "NO_WBS",
                                                    "NO_CBS",
                                                    "CD_ITEM",
                                                    "CD_USERDEF1",
                                                    "CD_USERDEF2",
                                                    "NM_USERDEF1",
                                                    "NM_USERDEF2",
                                                    "TP_UM_TAX",
                                                    "DT_EXDATE",
                                                    "CD_ITEM_ORIGIN",
                                                    "AM_EX_TRANS",
                                                    "AM_TRANS",
                                                    "NO_LINE_PJTBOM",
                                                    "FG_PACKING",
                                                    "NUM_USERDEF1",
                                                    "AM_REBATE_EX",
                                                    "AM_REBATE",
                                                    "UM_REBATE",
                                                    "ID_INSERT",
                                                    "DATE_USERDEF1",
                                                    "DATE_USERDEF2",
                                                    "TXT_USERDEF1",
                                                    "TXT_USERDEF2",
                                                    "CDSL_USERDEF1",
                                                    "FG_TAX",
                                                    "NUM_USERDEF2",
                                                    "CLS_L",
                                                    "CLS_M",
                                                    "CLS_S",
                                                    "GRP_ITEM",
                                                    "NUM_STND_ITEM_1",
                                                    "NUM_STND_ITEM_2",
                                                    "NUM_STND_ITEM_3",
                                                    "NUM_STND_ITEM_4",
                                                    "NUM_STND_ITEM_5",
                                                    "UM_WEIGHT",
                                                    "WEIGHT",
                                                    "TOT_WEIGHT",
                                                    "STND_ITEM",
                                                    "NO_RELATION",
                                                    "SEQ_RELATION",
                                                    "NUM_USERDEF3_PO",
                                                    "NUM_USERDEF4_PO",
                                                    "NUM_USERDEF5_PO",
                                                    "CD_BIZPLAN",
                                                    "CD_USERDEF3_PO",
                                                    "CD_USERDEF4_PO",
                                                    "NM_USERDEF3_PO",
                                                    "NM_USERDEF4_PO",
                                                    "CD_ACCT",
                                                    "NM_USERDEF5",
                                                    "DC50_PO",
                                                    "SEQ_PROJECT" },
                    SpParamsDelete = new string[] { "NO_PO",
                                                    "NO_LINE",
                                                    "CD_COMPANY" }
                });
            if (lb_RevSave && (dtL != null && dtH != null))
                spCollection.Add(new SpInfo()
                {
                    DataState = DataValueState.Added,
                    DataValue = dtH,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PU_REV_INSERT_AUTO",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_PO",
                                                    "ID_INSERT",
                                                    "DT_PO",
                                                    "CD_PARTNER",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "NO_EMP" }
                });
            if (lb_RcvSave && (dt_RCVH != null && dt_RCVL != null && dt_RCVH.Rows.Count > 0 && dt_RCVL.Rows.Count > 0))
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dt_RCVH,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PU_RCVH_INSERT",
                    SpNameUpdate = "UP_PU_RCVH_UPDATE",
                    SpParamsInsert = new string[] { "NO_RCV",
                                                    "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "CD_PARTNER",
                                                    "DT_REQ",
                                                    "NO_EMP",
                                                    "FG_TRANS",
                                                    "FG_PROCESS",
                                                    "CD_EXCH",
                                                    "CD_SL",
                                                    "YN_RETURN",
                                                    "YN_AM",
                                                    "DC_RMK",
                                                    "ID_INSERT",
                                                    "FG_RCV" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_RCV",
                                                    "DC_RMK",
                                                    "ID_INSERT" }
                });
                spCollection.Add(new SpInfo()
                {
                    DataValue = dt_RCVL,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PU_REQ_INSERT",
                    SpParamsInsert = new string[] { "NO_RCV",
                                                    "NO_LINE",
                                                    "CD_COMPANY",
                                                    "NO_PO",
                                                    "NO_POLINE",
                                                    "CD_PURGRP",
                                                    "DT_LIMIT",
                                                    "CD_ITEM",
                                                    "QT_REQ",
                                                    "YN_INSP",
                                                    "QT_PASS",
                                                    "QT_REJECTION",
                                                    "CD_UNIT_MM",
                                                    "QT_REQ_MM",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "UM_EX_PO",
                                                    "UM_EX",
                                                    "AM_EX",
                                                    "UM",
                                                    "AM",
                                                    "VAT",
                                                    "RT_CUSTOMS",
                                                    "CD_PJT",
                                                    "YN_PURCHASE",
                                                    "YN_RETURN",
                                                    "FG_TPPURCHASE",
                                                    "FG_RCV",
                                                    "FG_TRANS",
                                                    "FG_TAX",
                                                    "FG_TAXP",
                                                    "YN_AUTORCV",
                                                    "YN_REQ",
                                                    "CD_SL",
                                                    "NO_LC",
                                                    "NO_LCLINE",
                                                    "RT_SPEC",
                                                    "NO_EMP",
                                                    "NO_TO",
                                                    "NO_TO_LINE",
                                                    "CD_PLANT",
                                                    "CD_PARTNER",
                                                    "DT_REQ",
                                                    "DC_RMK",
                                                    "TP_UM_TAX" },
                    SpParamsValues = { { ActionState.Insert, "DC_RMK", string.Empty } }
                });
                if (dt_RCVH.Rows[0]["YN_AUTORCV"].ToString() == "Y" && dt_RCVH.Rows[0].RowState.ToString() == "Added")
                {
                    string seq = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PU", "06", dtH.Rows[0]["DT_PO"].ToString().Substring(0, 6));
                    spCollection.Add(new SpInfo()
                    {
                        DataValue = dt_RCVH,
                        CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                        UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                        SpNameInsert = "UP_PU_MM_QTIOH_INSERT",
                        SpParamsInsert = new string[] { "NO_IO1",
                                                        "CD_COMPANY",
                                                        "CD_PLANT",
                                                        "CD_PARTNER",
                                                        "FG_TRANS",
                                                        "YN_RETURN",
                                                        "DT_IO",
                                                        "GI_PARTNER",
                                                        "CD_DEPT",
                                                        "NO_EMP",
                                                        "DC_RMK",
                                                        "ID_INSERT",
                                                        "FG_RCV" },
                        SpParamsValues = { { ActionState.Insert, "NO_IO1", seq } }
                    });
                    spCollection.Add(new SpInfo()
                    {
                        DataValue = dt_RCVL,
                        CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                        UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                        SpNameInsert = "UP_PU_GR_INSERT",
                        SpParamsInsert = new string[] { "YN_RETURN",
                                                        "NO_IO1",
                                                        "NO_LINE",
                                                        "CD_COMPANY",
                                                        "CD_PLANT",
                                                        "CD_SL",
                                                        "DT_IO",
                                                        "NO_RCV",
                                                        "NO_LINE",
                                                        "NO_PO",
                                                        "NO_POLINE",
                                                        "FG_PS1",
                                                        "FG_TPPURCHASE",
                                                        "FG_IO1",
                                                        "FG_RCV",
                                                        "FG_TRANS",
                                                        "FG_TAX",
                                                        "CD_PARTNER",
                                                        "CD_ITEM",
                                                        "QT_REQ",
                                                        "QT_BAD1",
                                                        "CD_EXCH",
                                                        "RT_EXCH",
                                                        "UM_EX",
                                                        "UM",
                                                        "VAT",
                                                        "FG_TAXP",
                                                        "YN_AM1",
                                                        "CD_PJT",
                                                        "NO_LC",
                                                        "NO_LCLINE",
                                                        "NO_EMP1",
                                                        "CD_PURGRP",
                                                        "CD_UNIT_MM",
                                                        "QT_REQ_MM",
                                                        "QT_BAD_MM1",
                                                        "UM_EX_PO",
                                                        "YN_INSP",
                                                        "TP_UM_TAX" },
                        SpParamsValues = { { ActionState.Insert, "YN_AM1", dtH.Rows[0]["YN_AM"].ToString() },
                                           { ActionState.Insert, "NO_IO1", seq },
                                           { ActionState.Insert, "FG_PS1", "1" },
                                           { ActionState.Insert, "FG_IO1", "001" },
                                           { ActionState.Insert, "NO_EMP1", dtH.Rows[0]["NO_EMP"].ToString() },
                                           { ActionState.Insert, "QT_BAD1", 0 },
                                           { ActionState.Insert, "QT_BAD_MM1", 0 } }
                    });
                }
            }

            Global.MainFrame.ExecSp("UP_PU_BUDGET_HST_DELETE", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                              구매발주번호,
                                                                              "PU_PO_REG" });

            if (dt_budget != null && dt_budget.Rows.Count > 0)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dt_budget,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PU_BUDGET_HST_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_PU",
                                                    "NENU_TYPE",
                                                    "CD_BUDGET",
                                                    "NM_BUDGET",
                                                    "CD_BGACCT",
                                                    "NM_BGACCT",
                                                    "AM_ACTSUM",
                                                    "AM_JSUM",
                                                    "RT_JSUM",
                                                    "AM",
                                                    "AM_JAN",
                                                    "TP_BUNIT",
                                                    "ERROR_MSG",
                                                    "ID_INSERT" }
                });
            if (si_subinfo.DataValue != null)
                spCollection.Add(new SpInfo()
                {
                    DataState = DataValueState.Added,
                    DataValue = si_subinfo.DataValue,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameInsert = si_subinfo.SpNameInsert,
                    SpParamsInsert = si_subinfo.SpParamsInsert,
                    SpParamsValues = { { ActionState.Insert, "NO_PK", 구매발주번호 } }
                });
            if (dtLOT != null && dtLOT.Rows.Count > 0)
            {
                if (D.GetString(dtH.Rows[0]["YN_RETURN"]) == "Y")
                    spCollection.Add(new SpInfo()
                    {
                        DataValue = dtLOT,
                        SpNameInsert = "UP_MM_QTIOLOT_INSERT",
                        CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                        SpParamsInsert = new string[] { "CD_COMPANY",
                                                        "출고번호",
                                                        "출고항번",
                                                        "NO_LOT",
                                                        "CD_ITEM",
                                                        "출고일",
                                                        "FG_PS",
                                                        "수불구분",
                                                        "수불형태",
                                                        "창고코드",
                                                        "QT_GOOD_MNG",
                                                        "NO_IO",
                                                        "NO_IOLINE",
                                                        "NO_IOLINE2",
                                                        "YN_RETURN_QTIO",
                                                        "CD_PLANT_PR",
                                                        "NO_IO_PR",
                                                        "NO_LINE_IO_PR",
                                                        "NO_LINE_IO2_PR",
                                                        "FG_SLIP_PR",
                                                        "NO_LOT_PR",
                                                        "P_NO_SO",
                                                        "DT_LIMIT",
                                                        "DC_LOTRMK",
                                                        "P_CD_PLANT",
                                                        "P_ROOT_NO_LOT",
                                                        "P_ID_INSERT",
                                                        "P_BEF_NO_LOT",
                                                        "P_FG_LOT_ADD",
                                                        "P_BARCODE",
                                                        "CD_MNG1",
                                                        "CD_MNG2",
                                                        "CD_MNG3",
                                                        "CD_MNG4",
                                                        "CD_MNG5",
                                                        "CD_MNG6",
                                                        "CD_MNG7",
                                                        "CD_MNG8",
                                                        "CD_MNG9",
                                                        "CD_MNG10",
                                                        "CD_MNG11",
                                                        "CD_MNG12",
                                                        "CD_MNG13",
                                                        "CD_MNG14",
                                                        "CD_MNG15",
                                                        "CD_MNG16",
                                                        "CD_MNG17",
                                                        "CD_MNG18",
                                                        "CD_MNG19",
                                                        "CD_MNG20" },
                        SpParamsDelete = new string[] { "CD_COMPANY",
                                                        "출고번호",
                                                        "출고항번",
                                                        "NO_IOLINE2",
                                                        "NO_LOT" },
                        SpParamsValues = { { ActionState.Insert, "YN_RETURN_QTIO", "Y" },
                                           { ActionState.Insert, "CD_PLANT_PR", string.Empty },
                                           { ActionState.Insert, "NO_IO_PR", string.Empty },
                                           { ActionState.Insert, "NO_LINE_IO_PR", 0 },
                                           { ActionState.Insert, "NO_LINE_IO2_PR", 0 },
                                           { ActionState.Insert, "FG_SLIP_PR", string.Empty },
                                           { ActionState.Insert, "NO_LOT_PR", string.Empty },
                                           { ActionState.Insert, "P_NO_SO", string.Empty },
                                           { ActionState.Insert, "P_CD_PLANT", D.GetString(dtH.Rows[0]["CD_PLANT"]) },
                                           { ActionState.Insert, "P_ROOT_NO_LOT", string.Empty },
                                           { ActionState.Insert, "P_ID_INSERT", Global.MainFrame.LoginInfo.UserID },
                                           { ActionState.Insert, "P_BEF_NO_LOT", string.Empty },
                                           { ActionState.Insert, "P_FG_LOT_ADD", "N" },
                                           { ActionState.Insert, "P_BARCODE", string.Empty } }
                    });
                else
                    spCollection.Add(new SpInfo()
                    {
                        DataValue = dtLOT,
                        SpNameInsert = "UP_MM_QTIOLOT_INSERT",
                        CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                        SpParamsInsert = new string[] { "CD_COMPANY",
                                                        "NO_IO",
                                                        "NO_IOLINE",
                                                        "NO_LOT",
                                                        "CD_ITEM",
                                                        "DT_IO",
                                                        "FG_PS",
                                                        "FG_IO",
                                                        "CD_QTIOTP",
                                                        "CD_SL",
                                                        "QT_IO",
                                                        "NO_IO",
                                                        "NO_IOLINE",
                                                        "NO_IOLINE2",
                                                        "YN_RETURN",
                                                        "CD_PLANT_PR",
                                                        "NO_IO_PR",
                                                        "NO_LINE_IO_PR",
                                                        "NO_LINE_IO2_PR",
                                                        "FG_SLIP_PR",
                                                        "NO_LOT_PR",
                                                        "P_NO_SO",
                                                        "DT_LIMIT",
                                                        "DC_LOTRMK",
                                                        "P_CD_PLANT",
                                                        "P_ROOT_NO_LOT",
                                                        "P_ID_INSERT",
                                                        "P_BEF_NO_LOT",
                                                        "P_FG_LOT_ADD",
                                                        "P_BARCODE",
                                                        "CD_MNG1",
                                                        "CD_MNG2",
                                                        "CD_MNG3",
                                                        "CD_MNG4",
                                                        "CD_MNG5",
                                                        "CD_MNG6",
                                                        "CD_MNG7",
                                                        "CD_MNG8",
                                                        "CD_MNG9",
                                                        "CD_MNG10",
                                                        "CD_MNG11",
                                                        "CD_MNG12",
                                                        "CD_MNG13",
                                                        "CD_MNG14",
                                                        "CD_MNG15",
                                                        "CD_MNG16",
                                                        "CD_MNG17",
                                                        "CD_MNG18",
                                                        "CD_MNG19",
                                                        "CD_MNG20" },
                        SpParamsValues = { { ActionState.Insert, "YN_RETURN", "N" },
                                           { ActionState.Insert, "CD_PLANT_PR", string.Empty },
                                           { ActionState.Insert, "NO_IO_PR", string.Empty },
                                           { ActionState.Insert, "NO_LINE_IO_PR", 0 },
                                           { ActionState.Insert, "NO_LINE_IO2_PR", 0 },
                                           { ActionState.Insert, "FG_SLIP_PR", string.Empty },
                                           { ActionState.Insert, "NO_LOT_PR", string.Empty },
                                           { ActionState.Insert, "P_NO_SO", string.Empty },
                                           { ActionState.Insert, "P_CD_PLANT", D.GetString(dtH.Rows[0]["CD_PLANT"]) },
                                           { ActionState.Insert, "P_ROOT_NO_LOT", string.Empty },
                                           { ActionState.Insert, "P_ID_INSERT", Global.MainFrame.LoginInfo.UserID },
                                           { ActionState.Insert, "P_BEF_NO_LOT", string.Empty },
                                           { ActionState.Insert, "P_FG_LOT_ADD", "N" },
                                           { ActionState.Insert, "P_BARCODE", string.Empty } }
                    });
            }

            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }

            return true;
        }

        public int GetFI_GWDOCU(string p_no_po)
        {
            int num = 999;
            string str = Global.MainFrame.LoginInfo.CdPc;
            
            string query = @"SELECT ST_STAT  
                             FROM FI_GWDOCU WITH(NOLOCK)
                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            "AND CD_PC = '" + str + "'" + Environment.NewLine + 
                            "AND NO_DOCU = '" + p_no_po + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            
            if (dataTable.Rows.Count > 0 && dataTable.Rows.Count > 0)
                num = Convert.ToInt32(dataTable.Rows[0]["ST_STAT"].ToString());
            
            return num;
        }

        public DataTable GetPartnerCodeSearch()
        {
            string query = @"SELECT CD_EXC    
                             FROM MA_EXC WITH(NOLOCK)   
                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            "AND EXC_TITLE = '여신한도'";
            
            return Global.MainFrame.FillDataTable(query);
        }

        public string EnvSearch()
        {
            string str = "N";
            string query = @"SELECT CD_TP
                             FROM PU_ENV WITH(NOLOCK)
                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            "AND FG_TP = '001'";

            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            
            if (dataTable.Rows.Count > 0 && (dataTable.Rows[0]["CD_TP"] != DBNull.Value && dataTable.Rows[0]["CD_TP"].ToString().Trim() != string.Empty))
                str = dataTable.Rows[0]["CD_TP"].ToString();
            
            return str;
        }

        public string GetCCCodeSearch(string arg_cd_cc)
        {
            string str = string.Empty;
            
            if (arg_cd_cc == string.Empty)
                return string.Empty;
            
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT NM_CC   
                                                          FROM DZSN_MA_CC WITH(NOLOCK)  
                                                          WHERE CD_CC = '" + arg_cd_cc + "'" + Environment.NewLine +
                                                         "AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
            
            if (dataTable.Rows.Count > 0 && (dataTable.Rows[0]["NM_CC"] != DBNull.Value && dataTable.Rows[0]["NM_CC"].ToString().Trim() != string.Empty))
                str = dataTable.Rows[0]["NM_CC"].ToString().Trim();
            
            return str;
        }

        public DataTable GetCD_CC_CodeSearch(string arg_cd_purgrp)
        {
            string empty = string.Empty;

            return DBHelper.GetDataTable(@"SELECT A.CD_CC, 
                                                  MC.NM_CC,
                                                  A.NO_TEL,
                                                  A.NO_FAX,
                                                  A.E_MAIL
                                           FROM DZSN_MA_PURGRP A WITH(NOLOCK) 
                                           LEFT JOIN DZSN_MA_CC MC WITH(NOLOCK) ON A.CD_CC = MC.CD_CC AND A.CD_COMPANY = MC.CD_COMPANY
                                           WHERE A.CD_PURGRP = '" + arg_cd_purgrp + "'" + Environment.NewLine +
                                          "AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
        }

        public DataTable GetCD_CC_CodeSearch_pjt(string arg_cd_pjt)
        {
            string empty = string.Empty;
            
            return DBHelper.GetDataTable(@"SELECT A.CD_CC,
                                                  MC.NM_CC
                                           FROM DZSN_SA_PROJECTH A WITH(NOLOCK)        
                                           LEFT JOIN DZSN_MA_CC MC WITH(NOLOCK) ON A.CD_CC = MC.CD_CC AND A.CD_COMPANY = MC.CD_COMPANY
                                           WHERE A.NO_PROJECT = '" + arg_cd_pjt + "'" + Environment.NewLine +
                                          "AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
        }

        public DataTable GetCD_CC(string CD_PLANT, string KEY, string FLAG)
        {
            return DBHelper.GetDataTable("UP_PU_CD_CC_SEARCH", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                              CD_PLANT,
                                                                              KEY,
                                                                              FLAG });
        }

        public DataTable GetCD_CC_CodeSearch_cd_item(string str_cd_item, string str_cd_plant)
        {
            string empty = string.Empty;

            return DBHelper.GetDataTable(@"SELECT A.CD_CC,
                                                  MC.NM_CC
                                           FROM DZSN_MA_PITEM A WITH(NOLOCK)
                                           LEFT JOIN DZSN_MA_CC MC WITH(NOLOCK) ON A.CD_CC = MC.CD_CC AND A.CD_COMPANY = MC.CD_COMPANY
                                           WHERE A.CD_ITEM = '" + str_cd_item + "'" + Environment.NewLine +
                                          "AND A.CD_PLANT = '" + str_cd_plant + "'" + Environment.NewLine +
                                          "AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
        }

        public DataSet Get_TPPO_PURGRP(object[] obj)
        {
            return DBHelper.GetDataSet("UP_PU_TPPO_PURGRP_INFO_SELECT", obj);
        }

        public string GetGubunCodeSearch(string p_cd_field, string p_cd_sysdef)
        {
            if (!this.b_first_clone)
            {
                this._dt_fg_post.Columns.Add("CD_FIELD", typeof(string));
                this._dt_fg_post.Columns.Add("CD_SYSDEF", typeof(string));
                this._dt_fg_post.Columns.Add("NM_SYSDEF", typeof(string));
                this.b_first_clone = true;
            }

            string empty = string.Empty;
            object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                               p_cd_field,
                                               p_cd_sysdef };

            DataRow[] dataRowArray = this._dt_fg_post.Select("CD_FIELD = '" + p_cd_field + "' AND CD_SYSDEF = '" + p_cd_sysdef + "'");
            
            if (dataRowArray.Length == 0)
            {
                foreach (DataRow row1 in ComFunc.GetTableSearch("MA_CODEDTL", objArray).Rows)
                {
                    DataRow row2 = this._dt_fg_post.NewRow();
                    row2["CD_FIELD"] = D.GetString(row1["CD_FIELD"]);
                    row2["CD_SYSDEF"] = D.GetString(row1["CD_SYSDEF"]);
                    row2["NM_SYSDEF"] = D.GetString(row1["NM_SYSDEF"]);
                    this._dt_fg_post.Rows.Add(row2);
                }

                dataRowArray = this._dt_fg_post.Select("CD_FIELD = '" + p_cd_field + "' AND CD_SYSDEF = '" + p_cd_sysdef + "'");
            }

            if (dataRowArray.Length > 0)
                empty = D.GetString(dataRowArray[0]["NM_SYSDEF"]);
            
            return empty;
        }

        public DataTable Get_PJTInfo(string p_cd_pjt_pipe)
        {
            return DBHelper.GetDataTable("UP_PU_COMMON_PJT_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                              p_cd_pjt_pipe,
                                                                              this.Lang });
        }

        public DataTable CheckBUDGET(string 예산단위, string 예산계정, string 체크일자, string CdBizplan, string chk_FI_BUDGET)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("AM_ACTSUM", typeof(decimal));
            dataTable.Columns.Add("AM_JSUM", typeof(decimal));
            dataTable.Columns.Add("TP_BUNIT", typeof(string));
            dataTable.Columns.Add("ERROR_MSG", typeof(string));

            ResultData resultData = new ResultData();
            string str = string.Empty;
            SpInfo spinfo = new SpInfo();
            
            if (chk_FI_BUDGET == "100")
            {
                spinfo.SpNameSelect = "UP_FI_DOCU_BUDGET_DATA";
                spinfo.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                       예산단위,
                                                       CdBizplan,
                                                       예산계정,
                                                       "1",
                                                       체크일자 };
            }
            else
            {
                spinfo.SpNameSelect = "UP_FI_BUDGET_ACTSUM_CHECK";
                spinfo.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                       예산단위,
                                                       예산계정,
                                                       체크일자 };
            }

            try
            {
                resultData = (ResultData)Global.MainFrame.FillDataTable(spinfo);
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }

            DataRow row = dataTable.NewRow();
            
            if (str != string.Empty)
            {
                row["ERROR_MSG"] = str;
            }
            else
            {
                row["AM_ACTSUM"] = resultData.OutParamsSelect[0, 0];
                row["AM_JSUM"] = resultData.OutParamsSelect[0, 1];
                row["TP_BUNIT"] = resultData.OutParamsSelect[0, 2].ToString();
            }
            
            dataTable.Rows.Add(row);
            return dataTable;
        }

        internal DataTable PU_BUDGET_HST()
        {
            return new DataTable()
            {
                Columns = { { "NO_PU", typeof (string) },
                            { "NENU_TYPE", typeof (string) },
                            { "NO_HST", typeof (string) },
                            { "CD_BUDGET", typeof (string) },
                            { "NM_BUDGET", typeof (string) },
                            { "CD_BGACCT", typeof (string) },
                            { "NM_BGACCT", typeof (string) },
                            { "CD_BIZPLAN", typeof (string) },
                            { "NM_BIZPLAN", typeof (string) },
                            { "AM_ACTSUM", typeof (decimal) },
                            { "AM_JSUM", typeof (decimal) },
                            { "RT_JSUM", typeof (decimal) },
                            { "AM", typeof (decimal) }, 
                            { "AM_JAN", typeof (decimal) },
                            { "TP_BUNIT", typeof (string) },
                            { "ERROR_MSG", typeof (string) },
                            { "DTS_INSERT", typeof (string) },
                            { "ID_INSERT", typeof (string) },
                            { "DTS_UPDATE", typeof (string) },
                            { "ID_UPDATE", typeof (string) } }
            };
        }

        public string GetNoPo(string id_memo)
        {
            string query = @"SELECT A.NO_PO
                             FROM PU_POL A WITH(NOLOCK)
                             WHERE A.ID_MEMO = '" + id_memo + "'" + Environment.NewLine +
                            "AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";

            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            
            return dataTable == null || dataTable.Rows.Count <= 0 ? string.Empty : D.GetString(dataTable.Rows[0]["NO_PO"]);
        }

        internal DataTable item_pinvn(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PU_PINVN_USE_QT", obj);
        }

        internal string pjt_item_josun(string cd_pjt)
        {
            string query = @"SELECT CD_USERDEF13
                             FROM SA_PROJECTH_SUB WITH(NOLOCK)
                             WHERE CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            "AND NO_PROJECT ='" + cd_pjt + "'";

            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            
            if (dataTable == null || dataTable.Rows.Count == 0)
                return string.Empty;
            
            return D.GetString(dataTable.Rows[0]["CD_USERDEF13"]);
        }

        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noIo, decimal no_ioline, string value)
        {
            string query = string.Empty;
            string str = string.Empty;

            switch (contentType)
            {
                case ContentType.Memo:
                    str = "MEMO_CD";
                    break;
                case ContentType.CheckPen:
                    str = "CHECK_PEN";
                    break;
            }
            
            switch (commandType)
            {
                case Dass.FlexGrid.CommandType.Add:
                    query = @"UPDATE PU_POL 
                              SET " + str + " = '" + value + "'" + Environment.NewLine +
                             "WHERE NO_PO  = '" + noIo + "'" + Environment.NewLine +
                             "AND CD_COMPANY = '" + MA.Login.회사코드 + "'" + Environment.NewLine +
                             "AND NO_LINE = " + no_ioline;
                    break;
                case Dass.FlexGrid.CommandType.Delete:
                    query = @"UPDATE PU_POL 
                              SET " + str + " = NULL" + Environment.NewLine +
                             "WHERE NO_PO  = '" + noIo + "'" + Environment.NewLine +
                             "AND CD_COMPANY = '" + MA.Login.회사코드 + "'" + Environment.NewLine + 
                             "AND NO_LINE = " + no_ioline;
                    break;
            }

            Global.MainFrame.ExecuteScalar(query);
        }

        public DataTable GetYN_SU(string sCD_TPPO)
        {
            string empty = string.Empty;
            string query = @"SELECT YN_SU,
                                    FG_TRANS
                             FROM PU_TPPO WITH(NOLOCK)
                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            "AND CD_TPPO = '" + sCD_TPPO + "'";
            
            return Global.MainFrame.FillDataTable(query);
        }

        public DataTable GET_SU_BOM(string strCD_PLANT, string strCD_PARTNER, string strCD_ITEM)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_PU_PO_REG_SU_BOM_SELECT",
                SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                strCD_PLANT,
                                                strCD_PARTNER,
                                                strCD_ITEM }
            })).DataValue;

            foreach (DataColumn column in dataValue.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }

            return dataValue;
        }

        internal DataTable dtLot_Schema()
        {
            return new DataTable()
            {
                Columns = { { "NO_IO", typeof (string) },
                            { "NO_IOLINE", typeof (decimal) },
                            { "CD_QTIOTP", typeof (string) },
                            { "NM_QTIOTP", typeof (string) },
                            { "DT_IO", typeof (string) },
                            { "CD_SL", typeof (string) },
                            { "NM_SL", typeof (string) },
                            { "FG_IO", typeof (string) },
                            { "FG_TRANS", typeof (string) },
                            { "CD_PLANT", typeof (string) },
                            { "CD_ITEM", typeof (string) },
                            { "NM_ITEM", typeof (string) },
                            { "UNIT_IM", typeof (string) },
                            { "STND_ITEM", typeof (string) },
                            { "CD_PJT", typeof (string) },
                            { "NM_PROJECT", typeof (string) },
                            { "UM_EX", typeof (decimal) },
                            { "AM_EX", typeof (decimal) },
                            { "AM", typeof (decimal) },
                            { "YN_RETURN", typeof (string) },
                            { "QT_GOOD_INV", typeof (decimal) } }
            };
        }

        public string getLOTIO(string NO_PO)
        {
            string query = @"SELECT MAX(NO_IO) AS NO_IO
                             FROM MM_QTIO WITH(NOLOCK)
                             WHERE NO_PSO_MGMT ='" + NO_PO + "'" + Environment.NewLine +
                            "AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            string str = string.Empty;
            
            if (dataTable != null && dataTable.Rows.Count > 0)
                str = D.GetString(dataTable.Rows[0]["NO_IO"]);
            
            return str;
        }

        public DataTable get_MA_PARTNER_SUB(string CD_PARTNER)
        {
            return DBHelper.GetDataTable(@"SELECT T.CD_TPPO,
                                                  T.NM_TPPO,
                                                  G.CD_PURGRP,
                                                  G.NM_PURGRP
                                           FROM MA_PARTNER_SUB P WITH(NOLOCK)
                                           LEFT JOIN DZSN_PU_TPPO T WITH(NOLOCK) ON T.CD_TPPO = P.CD_TPPO AND T.CD_COMPANY = P.CD_COMPANY
                                           LEFT JOIN DZSN_MA_PURGRP G WITH(NOLOCK) ON G.CD_PURGRP = P.CD_PURGRP AND G.CD_COMPANY = P.CD_COMPANY
                                           WHERE P.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                          "AND P.CD_PARTNER    = '" + CD_PARTNER + "'");
        }

        public DataTable GetCD_CC_Priority()
        {
            return DBHelper.GetDataTable(@"SELECT CD_SYSDEF,
                                                  NM_SYSDEF,
                                                  CD_FLAG1
                                           FROM DZSN_MA_CODEDTL WITH(NOLOCK)
                                           WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                          "AND CD_FIELD = 'PU_C000133'" + Environment.NewLine +
                                          "AND USE_YN = 'Y'" + Environment.NewLine +
                                          "ORDER BY CD_FLAG1");
        }

        public DataTable GetFG_PAYMENT(string CD_PARTNER)
        {
            return DBHelper.GetDataTable(@"SELECT MP.FG_PAYMENT
                                           FROM CZ_MA_PARTNER MP WITH(NOLOCK)
                                           WHERE MP.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                          "AND MP.CD_PARTNER    = '" + CD_PARTNER + "'");
        }
    }
}