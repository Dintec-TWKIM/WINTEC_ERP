using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
	internal class P_CZ_PU_REQ_REG_1_WINTEC_BIZ
	{
        private string Lang = Global.SystemLanguage.MultiLanguageLpoint.ToString();

        public DataTable Search_LOT() => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PU_MNG_LOT_SELECT",
            CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
            SpParamsSelect = new string[] { Global.MainFrame.LoginInfo.CompanyCode }
        })).DataValue;

        public DataTable Search_SERIAL() => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PU_MNG_SER_SELECT",
            CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
            SpParamsSelect = new string[] { Global.MainFrame.LoginInfo.CompanyCode }
        })).DataValue;

        public DataSet Search(string NO_RCV, string YN_AUTORCV)
        {
            DataSet dataValue = (DataSet)((ResultData)Global.MainFrame.FillDataSet("UP_PU_REQ_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                      NO_RCV,
                                                                                                                      YN_AUTORCV,
                                                                                                                      this.Lang })).DataValue;
            DataTable table = dataValue.Tables[0];
            table.Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            table.Columns["DT_REQ"].DefaultValue = Global.MainFrame.GetStringToday;
            table.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            table.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            table.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            table.Columns["FG_TRANS"].DefaultValue = "001";
            table.Columns["FG_PROCESS"].DefaultValue = "001";
            table.Columns[nameof(YN_AUTORCV)].DefaultValue = "Y";
            dataValue.Tables[1].Columns["DATE_USERDEF1"].DefaultValue = Global.MainFrame.GetStringToday;
            return dataValue;
        }

        public DataTable Search_MATL(string NO_PO) => DBHelper.GetDataTable("UP_PU_REQ_MATL_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                               NO_PO,
                                                                                                               this.Lang });

        internal DataTable Search_MATL(string NO_PO_MULTI, object[] obj)
        {
            DataTable dataTable1 = null;
            try
            {
                string[] pipes = D.StringConvert.GetPipes(NO_PO_MULTI, 150);
                D.GetString(pipes.Length);
                foreach (string str in pipes)
                {
                    obj[1] = str;
                    DataTable dataTable2 = DBHelper.GetDataTable("UP_PU_REQ_MATL_MUTI_S", obj);
                    if (dataTable1 == null)
                        dataTable1 = dataTable2;
                    else
                        dataTable1.Merge(dataTable2);
                }
            }
            finally
            {
                MsgControl.CloseMsg();
            }
            return dataTable1;
        }

        public bool Save(
          DataTable dtH,
          DataTable dtL,
          DataTable dtLOT,
          string no_ioseq,
          DataTable dtSERL,
          string CD_PLANT,
          string NO_REQ,
          DataTable dtLOCATION,
          string YN_SPECIAL,
          DataTable dtHH,
          DataTable dtLL)
        {
            SpInfoCollection spc = new SpInfoCollection();
            if (dtH != null)
            {
                SpInfo spInfo = new SpInfo();
                dtH.RemotingFormat = SerializationFormat.Binary;
                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_PU_RCVH_INSERT";
                spInfo.SpNameUpdate = "UP_PU_RCVH_UPDATE";
                spInfo.SpParamsInsert = new string[] { "NO_RCV",
                                                     "CD_COMPANY",
                                                     nameof (CD_PLANT),
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
                                                     "FG_RCV",
                                                     "CD_DEPT",
                                                     "FG_UM",
                                                     "DC_RMK_TEXT" };
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "NO_RCV",
                                                       "DC_RMK",
                                                       "ID_INSERT",
                                                       "DC_RMK_TEXT",
                                                       "UM_WEIGHT",
                                                       "TOT_WEIGHT" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "FG_UM", "");
                spc.Add(spInfo);
            }
            if (dtL != null)
            {
                SpInfo spInfo = new SpInfo();
                dtL.RemotingFormat = SerializationFormat.Binary;
                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_PU_REQ_INSERT";
                spInfo.SpNameUpdate = "UP_PU_REQ_UPDATE";
                spInfo.SpNameDelete = "UP_PU_REQ_AUTO_DELETE";
                spInfo.SpParamsInsert = new string[] { "NO_RCV",
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
                                                       "AM_EXREQ",
                                                       "UM",
                                                       "AM_REQ",
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
                                                       nameof (CD_PLANT),
                                                       "CD_PARTNER",
                                                       "DT_REQ",
                                                       "DC_RMK",
                                                       "YN_AUTOSTOCK",
                                                       "NO_REV",
                                                       "NO_REVLINE",
                                                       "CD_WH",
                                                       "DC_RMK2",
                                                       "SEQ_PROJECT",
                                                       "NO_WBS",
                                                       "NO_CBS",
                                                       "TP_UM_TAX",
                                                       "FG_SPECIAL",
                                                       "DATE_USERDEF1",
                                                       "CDSL_USERDEF1",
                                                       "NUM_USERDEF1",
                                                       "NUM_USERDEF2",
                                                       "UM_WEIGHT",
                                                       "TOT_WEIGHT",
                                                       "CD_USERDEF1_RCV",
                                                       "CD_USERDEF2_RCV",
                                                       "NM_USERDEF1_RCV",
                                                       "NM_USERDEF2_RCV",
                                                       "DATE_USERDEF2",
                                                       "NO_LOT",
                                                       "DT_LIMIT_LOT" };
                spInfo.SpParamsUpdate = new string[] { "NO_RCV",
                                                       "NO_LINE",
                                                       "CD_COMPANY",
                                                       "DT_LIMIT",
                                                       "QT_REQ",
                                                       "QT_REQ_MM",
                                                       "UM_EX_PO",
                                                       "UM_EX",
                                                       "AM_EXREQ",
                                                       "UM",
                                                       "AM_REQ",
                                                       "VAT",
                                                       "CD_SL",
                                                       "YN_INSP",
                                                       "DC_RMK",
                                                       "DC_RMK2",
                                                       "DATE_USERDEF1",
                                                       "CDSL_USERDEF1",
                                                       "UM_WEIGHT",
                                                       "TOT_WEIGHT",
                                                       "CD_USERDEF1_RCV",
                                                       "CD_USERDEF2_RCV",
                                                       "NM_USERDEF1_RCV",
                                                       "NM_USERDEF2_RCV" };
                spInfo.SpParamsDelete = new string[] { "NO_RCV",
                                                       "NO_LINE",
                                                       "CD_COMPANY" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "YN_AUTOSTOCK", "Y");
                spInfo.SpParamsValues.Add(ActionState.Insert, "DATE_USERDEF2", "");
                spc.Add(spInfo);
            }
            if (dtH != null && dtL != null)
            {
                SpInfo spInfo1 = new SpInfo();
                dtH.RemotingFormat = SerializationFormat.Binary;
                spInfo1.DataValue = dtH;
                spInfo1.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo1.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo1.SpNameInsert = "UP_PU_MM_QTIOH_INSERT";
                spInfo1.SpParamsInsert = new string[] { "NO_IO1",
                                                        "CD_COMPANY",
                                                        nameof (CD_PLANT),
                                                        "CD_PARTNER",
                                                        "FG_TRANS",
                                                        "YN_RETURN",
                                                        "DT_IO1",
                                                        "GI_PARTNER",
                                                        "CD_DEPT",
                                                        "NO_EMP",
                                                        "DC_RMK",
                                                        "ID_INSERT",
                                                        "FG_RCV"
                };
                spInfo1.SpParamsValues.Add(ActionState.Insert, "NO_IO1", no_ioseq);
                spInfo1.SpParamsValues.Add(ActionState.Insert, "DT_IO1", dtH.Rows[0]["DT_REQ"]);
                spInfo1.SpParamsValues.Add(ActionState.Insert, "GI_PARTNER", "");
                spc.Add(spInfo1);
                SpInfo spInfo2 = new SpInfo();
                dtL.RemotingFormat = SerializationFormat.Binary;
                spInfo2.DataValue = dtL;
                spInfo2.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo2.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo2.SpNameInsert = "UP_PU_GR_INSERT";
                spInfo2.SpParamsInsert = new string[] { "YN_RETURN1",
                                                        "NO_IO",
                                                        "NO_IOLINE",
                                                        "CD_COMPANY",
                                                        nameof (CD_PLANT),
                                                        "CD_SL",
                                                        "DT_IO",
                                                        "NO_RCV",
                                                        "NO_LINE",
                                                        "NO_PO",
                                                        "NO_POLINE",
                                                        "FG_PS1",
                                                        "FG_TPPURCHASE",
                                                        "FG_IO",
                                                        "FG_RCV",
                                                        "FG_TRANS",
                                                        "FG_TAX",
                                                        "CD_PARTNER1",
                                                        "CD_ITEM",
                                                        "QT_REQ",
                                                        "QT_BAD1",
                                                        "CD_EXCH",
                                                        "RT_EXCH",
                                                        "UM_EX",
                                                        "UM",
                                                        "AM_EX",
                                                        "AM",
                                                        "VAT",
                                                        "FG_TAXP",
                                                        "YN_AM1",
                                                        "CD_PJT",
                                                        "NO_LC",
                                                        "NO_LCLINE",
                                                        "NO_EMP",
                                                        "CD_PURGRP",
                                                        "CD_UNIT_MM",
                                                        "QT_REQ_MM",
                                                        "QT_BAD_MM1",
                                                        "UM_EX_PO",
                                                        "YN_INSP",
                                                        "YN_PURCHASE",
                                                        "DC_RMK",
                                                        "CD_WH",
                                                        "SEQ_PROJECT",
                                                        "NO_WBS",
                                                        "NO_CBS",
                                                        "TP_UM_TAX",
                                                        "DC_RMK2",
                                                        "UM_WEIGHT",
                                                        "TOT_WEIGHT",
                                                        "CD_USERDEF1_RCV",
                                                        "CD_USERDEF2_RCV",
                                                        "DATE_USERDEF1",
                                                        "NM_USERDEF1_RCV",
                                                        "NM_USERDEF2_RCV",
                                                        "GI_PARTNER",
                                                        "CD_USERDEF3_IO",
                                                        "CD_USERDEF4_IO",
                                                        "CD_USERDEF5_IO",
                                                        "NM_USERDEF3_IO",
                                                        "NM_USERDEF4_IO",
                                                        "CD_USERDEF6_IO",
                                                        "TXT_USERDEF1_IO",
                                                        "DC_RMK2_IO",
                                                        "NUM_USERDEF1_IO",
                                                        "NUM_USERDEF2_IO",
                                                        "NUM_USERDEF3_IO" };
                spInfo2.SpParamsValues.Add(ActionState.Insert, "YN_RETURN1", "N");
                spInfo2.SpParamsValues.Add(ActionState.Insert, "CD_PARTNER1", dtH.Rows[0]["CD_PARTNER"].ToString());
                spInfo2.SpParamsValues.Add(ActionState.Insert, "YN_AM1", dtH.Rows[0]["YN_AM"].ToString());
                spInfo2.SpParamsValues.Add(ActionState.Insert, "FG_PS1", "1");
                spInfo2.SpParamsValues.Add(ActionState.Insert, "QT_BAD1", 0);
                spInfo2.SpParamsValues.Add(ActionState.Insert, "QT_BAD_MM1", 0);
                spc.Add(spInfo2);
            }
            if (YN_SPECIAL == "Y" && dtL != null)
                spc.Add(new SpInfo()
                {
                    DataValue = dtL,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameInsert = "UP_PU_REV_UPDATE_SPECIAL",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_REV",
                                                    "NO_REVLINE",
                                                    "JAN_QT_PASS",
                                                    "JAN_QT_SPECIAL" }
                });
            if (dtLOT != null)
            {
                SpInfo spInfo = new SpInfo();
                dtLOT.RemotingFormat = SerializationFormat.Binary;
                spInfo.DataValue = dtLOT;
                spInfo.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                spInfo.SpNameUpdate = "UP_MM_QTIOLOT_UPDATE";
                spInfo.SpNameDelete = "UP_MM_QTIOLOT_DELETE";
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
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
                                                       nameof (CD_PLANT),
                                                       "ROOT_NO_LOT",
                                                       "ID_INSERT",
                                                       "BEF_NO_LOT",
                                                       "FG_LOT_ADD",
                                                       "BARCODE",
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
                                                       "CD_MNG20" };
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "NO_LOT",
                                                       "QT_IO",
                                                       "QT_IO_OLD" };
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "NO_IOLINE2",
                                                       "NO_LOT" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", dtH.Rows[0]["YN_RETURN"].ToString());
                spInfo.SpParamsValues.Add(ActionState.Insert, "CD_PLANT_PR", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_IO_PR", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO_PR", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO2_PR", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "FG_SLIP_PR", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LOT_PR", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "P_NO_SO", "");
                if (!dtLOT.Columns.Contains(nameof(CD_PLANT)))
                    spInfo.SpParamsValues.Add(ActionState.Insert, nameof(CD_PLANT), CD_PLANT);
                if (!dtLOT.Columns.Contains("ROOT_NO_LOT"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "ROOT_NO_LOT", "");
                if (!dtLOT.Columns.Contains("ID_INSERT"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "ID_INSERT", "");
                if (!dtLOT.Columns.Contains("FG_LOT_ADD"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "FG_LOT_ADD", "");
                if (!dtLOT.Columns.Contains("BARCODE"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "BARCODE", "");
                if (!dtLOT.Columns.Contains("BEF_NO_LOT"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "BEF_NO_LOT", "N");
                spc.Add(spInfo);
            }
            if (dtSERL != null)
            {
                SpInfo spInfo = new SpInfo()
                {
                    DataValue = dtSERL,
                    UserID = Global.MainFrame.LoginInfo.UserID,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameInsert = "UP_MM_QTIODS_INSERT",
                    SpNameUpdate = "UP_MM_QTIODS_UPDATE",
                    SpNameDelete = "UP_MM_QTIODS_DELETE"
                };
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NO_SERIAL",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_ITEM",
                                                       "CD_QTIOTP",
                                                       "FG_IO",
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
                                                       "CD_MNG20",
                                                       nameof (CD_PLANT),
                                                       "ID_INSERT" };
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "NO_SERIAL",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_ITEM",
                                                       "CD_QTIOTP",
                                                       "FG_IO",
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
                                                       "CD_MNG20" };
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "NO_SERIAL" };
                spInfo.SpParamsValues.Add(ActionState.Insert, nameof(CD_PLANT), CD_PLANT);
                spc.Add(spInfo);
            }
            if (dtLOCATION != null && dtLOCATION.Rows.Count > 0)
                spc.Add(new SpInfo()
                {
                    DataValue = dtLOCATION,
                    DataState = DataValueState.Added,
                    SpNameInsert = "UP_MM_QTIO_LOCATION_I",
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_IO",
                                                    "NO_IOLINE",
                                                    "CD_LOCATION",
                                                    "CD_ITEM",
                                                    "DT_IO",
                                                    "FG_PS",
                                                    "FG_IO",
                                                    "CD_QTIOTP",
                                                    nameof (CD_PLANT),
                                                    "CD_SL",
                                                    "QT_IO_LOCATION",
                                                    "YN_RETURN" }
                });
            if (dtHH != null)
                spc.Add(new SpInfo()
                {
                    DataValue = dtHH,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.UserID,
                    SpNameInsert = "UP_PU_MM_QTIOH_INSERT",
                    SpParamsInsert = new string[] { "NO_IO",
                                                    "CD_COMPANY",
                                                    nameof (CD_PLANT),
                                                    "CD_PARTNER",
                                                    "FG_TRANS",
                                                    "YN_RETURN",
                                                    "DT_IO",
                                                    "GI_PARTNER",
                                                    "CD_DEPT",
                                                    "NO_EMP",
                                                    "DC_RMK",
                                                    "ID_INSERT",
                                                    "CD_QTIOTP" }
                });
            if (dtLL != null)
                spc.Add(new SpInfo()
                {
                    DataValue = dtLL,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.UserID,
                    SpNameInsert = "UP_PU_REQ_MATL_I",
                    SpNameDelete = "UP_PU_REQ_MATL_D",
                    SpParamsInsert = new string[] { "YN_RETURN",
                                                    "NO_IO",
                                                    "NO_IOLINE",
                                                    "CD_COMPANY",
                                                    nameof (CD_PLANT),
                                                    "CD_SL",
                                                    "DT_IO",
                                                    "NO_ISURCV",
                                                    "NO_ISURCVLINE",
                                                    "NO_PO",
                                                    "NO_POLINE",
                                                    "NO_PO_MAL_LINE",
                                                    "CD_PARTNER",
                                                    "CD_MATL",
                                                    "QT_NEED",
                                                    "CD_PJT",
                                                    "GI_PARTNER",
                                                    "NO_EMP",
                                                    "CD_GROUP",
                                                    "CD_BIN_REF",
                                                    "FG_IO",
                                                    "CD_QTIOTP",
                                                    "NO_IO_MGMT",
                                                    "NO_IOLINE_MGMT",
                                                    "YN_BF_ALL",
                                                    "DC_RMK",
                                                    "CD_ITEM",
                                                    "SEQ_PROJECT" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_IO",
                                                    "NO_IOLINE" },
                    SpParamsValues = { { ActionState.Insert, "CD_BIN_REF", "99" },
                                       { ActionState.Insert, "NO_ISURCV" ,  ""  },
                                       { ActionState.Insert, "NO_ISURCVLINE", 0 },
                                       { ActionState.Insert, "YN_BF_ALL" , ""   } }
                });
            return DBHelper.Save(spc);
        }

        public void Delete(object[] p_param1, object[] p_param2) => DBHelper.ExecuteNonQuery("UP_PU_RCVH_DELETE_1", p_param1);

        public string EnvSearch()
        {
            string str = "N";
            string query = "SELECT CD_TP   FROM PU_ENV  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'    AND FG_TP = '001' ";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["CD_TP"] != DBNull.Value && dataTable.Rows[0]["CD_TP"].ToString().Trim() != string.Empty)
                str = dataTable.Rows[0]["CD_TP"].ToString();
            return str;
        }

        internal DataTable getMail_Adress(DataTable no_po)
        {
            DataTable mailAdress = no_po.Clone();
            foreach (DataRow row in no_po.Rows)
            {
                DataTable dataTable = DBHelper.GetDataTable("SELECT   PA.EMP_WRITE,\r\n                                                ME.NO_EMP,\r\n                                                ME.NM_KOR,\r\n                                                ME.NO_EMAIL\r\n                                   FROM         PU_POL PP INNER JOIN PU_APPH PA ON PP.NO_APP = PA.NO_APP AND PP.CD_COMPANY = PA.CD_COMPANY \r\n                                                          INNER JOIN DZSN_MA_EMP ME ON PA.EMP_WRITE = ME.NO_EMP AND PA.CD_COMPANY = ME.CD_COMPANY\r\n                                   WHERE        PA.CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                   AND          PP.NO_PO ='" + D.GetString(row["NO_PO"]) + "'");
                if (dataTable != null && dataTable.Rows.Count != 0)
                    mailAdress.Merge(dataTable);
            }
            return mailAdress;
        }

        internal DataTable getMail_Adress_ICD(DataTable no_po)
        {
            DataTable mailAdressIcd = no_po.Clone();
            foreach (DataRow row in no_po.Rows)
            {
                DataTable dataTable = DBHelper.GetDataTable("SELECT \r\n                                                ME.NO_EMP,\r\n                                                ME.NM_KOR,\r\n                                                ME.NO_EMAIL\r\n                                   FROM         PU_POL PP INNER JOIN PU_PRL PA ON PP.NO_PR = PA.NO_PR AND PP.NO_PRLINE = PA.NO_PRLINE AND PP.CD_COMPANY = PA.CD_COMPANY \r\n                                                          INNER JOIN DZSN_MA_EMP ME ON PA.CD_USERDEF4 = ME.NO_EMP AND PA.CD_COMPANY = ME.CD_COMPANY\r\n                                   WHERE        PA.CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                   AND          PP.NO_PO ='" + D.GetString(row["NO_PO"]) + "'\r\n                                   AND          PP.NO_LINE ='" + D.GetString(row["NO_POLINE"]) + "'\r\n                                   GROUP BY     ME.NO_EMP, ME.NM_KOR, ME.NO_EMAIL");
                if (dataTable != null && dataTable.Rows.Count != 0)
                    mailAdressIcd.Merge(dataTable);
            }
            return mailAdressIcd;
        }

        public string getNO_IO(string NO_IO)
        {
            string empty = string.Empty;
            string query = "SELECT MAX(NO_IO) AS NO_IO   FROM MM_QTIO  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'    AND NO_IO_MGMT = '" + NO_IO + "'   AND CD_BIN_REF = '99'";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0][nameof(NO_IO)] != DBNull.Value && dataTable.Rows[0][nameof(NO_IO)].ToString().Trim() != string.Empty)
                empty = dataTable.Rows[0][nameof(NO_IO)].ToString();
            return empty;
        }

        public string getNO_IO_MGMT(string NO_ISURCV)
        {
            string empty = string.Empty;
            string query = "SELECT MAX(NO_IO) AS NO_IO   FROM MM_QTIO  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'    AND NO_ISURCV = '" + NO_ISURCV + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["NO_IO"] != DBNull.Value && dataTable.Rows[0]["NO_IO"].ToString().Trim() != string.Empty)
                empty = dataTable.Rows[0]["NO_IO"].ToString();
            return empty;
        }

        public string getCD_SL(string CD_PARTNER, string CD_PLANT)
        {
            string empty = string.Empty;
            string query = "SELECT MAX(CD_SL) AS CD_SL   FROM SU_PARTSL WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'    AND CD_PARTNER = '" + CD_PARTNER + "'   AND CD_PLANT = '" + CD_PLANT + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["CD_SL"] != DBNull.Value && dataTable.Rows[0]["CD_SL"].ToString().Trim() != string.Empty)
                empty = dataTable.Rows[0]["CD_SL"].ToString();
            return empty;
        }

        internal DataSet Initial_DataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable table1 = new DataTable();
            DataTable table2 = new DataTable();
            dataSet.Tables.Add(table1);
            dataSet.Tables.Add(table2);
            dataSet.Tables[0].Columns.Add("S", typeof(string));
            dataSet.Tables[0].Columns.Add("NO_RCV", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_PLANT", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_PARTNER", typeof(string));
            dataSet.Tables[0].Columns.Add("DT_REQ", typeof(string));
            dataSet.Tables[0].Columns.Add("NO_EMP", typeof(string));
            dataSet.Tables[0].Columns.Add("FG_TRANS", typeof(string));
            dataSet.Tables[0].Columns.Add("FG_PROCESS", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_EXCH", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_SL", typeof(string));
            dataSet.Tables[0].Columns.Add("YN_RETURN", typeof(string));
            dataSet.Tables[0].Columns.Add("YN_AM", typeof(string));
            dataSet.Tables[0].Columns.Add("DC_RMK", typeof(string));
            dataSet.Tables[0].Columns.Add("YN_SUBCON", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_DEPT", typeof(string));
            dataSet.Tables[0].Columns.Add("NM_PLANT", typeof(string));
            dataSet.Tables[0].Columns.Add("LN_PARTNER", typeof(string));
            dataSet.Tables[0].Columns.Add("NM_KOR", typeof(string));
            dataSet.Tables[0].Columns.Add("NM_SL", typeof(string));
            dataSet.Tables[0].Columns.Add("NM_EXCH", typeof(string));
            dataSet.Tables[0].Columns.Add("FG_RCV", typeof(string));
            dataSet.Tables[0].Columns.Add("NM_FG_RCV", typeof(string));
            dataSet.Tables[0].Columns.Add("YN_AUTORCV", typeof(string));
            dataSet.Tables[0].Columns.Add("DC_RMK_TEXT", typeof(string));
            dataSet.Tables[1].Columns.Add("S", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_RCV", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_LINE", typeof(decimal));
            dataSet.Tables[1].Columns.Add("CD_PURGRP", typeof(string));
            dataSet.Tables[1].Columns.Add("QT_REQ", typeof(decimal));
            dataSet.Tables[1].Columns.Add("QT_GOOD_INV", typeof(decimal));
            dataSet.Tables[1].Columns.Add("DT_LIMIT", typeof(string));
            dataSet.Tables[1].Columns.Add("DT_GRLAST", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("QT_PASS", typeof(decimal));
            dataSet.Tables[1].Columns.Add("YN_INSP", typeof(string));
            dataSet.Tables[1].Columns.Add("QT_REJECTION", typeof(decimal));
            dataSet.Tables[1].Columns.Add("QT_GR", typeof(decimal));
            dataSet.Tables[1].Columns.Add("CD_UNIT_MM", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_ZONE", typeof(string));
            dataSet.Tables[1].Columns.Add("QT_REQ_MM", typeof(decimal));
            dataSet.Tables[1].Columns.Add("QT_GR_MM", typeof(decimal));
            dataSet.Tables[1].Columns.Add("CD_EXCH", typeof(string));
            dataSet.Tables[1].Columns.Add("RT_EXCH", typeof(decimal));
            dataSet.Tables[1].Columns.Add("UM_EX_PO", typeof(decimal));
            dataSet.Tables[1].Columns.Add("UM_EX", typeof(decimal));
            dataSet.Tables[1].Columns.Add("UM", typeof(decimal));
            dataSet.Tables[1].Columns.Add("AM_EX", typeof(decimal));
            dataSet.Tables[1].Columns.Add("AM_EXREQ", typeof(decimal));
            dataSet.Tables[1].Columns.Add("AM", typeof(decimal));
            dataSet.Tables[1].Columns.Add("AM_REQ", typeof(decimal));
            dataSet.Tables[1].Columns.Add("VAT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("AM_EXRCV", typeof(decimal));
            dataSet.Tables[1].Columns.Add("AM_RCV", typeof(decimal));
            dataSet.Tables[1].Columns.Add("RT_CUSTOMS", typeof(decimal));
            dataSet.Tables[1].Columns.Add("CD_PJT", typeof(string));
            dataSet.Tables[1].Columns.Add("YN_PURCHASE", typeof(string));
            dataSet.Tables[1].Columns.Add("YN_RETURN", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_TPPURCHASE", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_RCV", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_TRANS", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_TAX", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_TAXP", typeof(string));
            dataSet.Tables[1].Columns.Add("YN_AUTORCV", typeof(string));
            dataSet.Tables[1].Columns.Add("YN_REQ", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_SL", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_LC", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_LCLINE", typeof(decimal));
            dataSet.Tables[1].Columns.Add("RT_SPEC", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NO_EMP", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_PO", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_POLINE", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NO_IO_MGMT", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_IOLINE_MGMT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NO_PO_MGMT", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_POLINE_MGMT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("YN_AM", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_PLANT", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_PARTNER", typeof(string));
            dataSet.Tables[1].Columns.Add("LN_PARTNER", typeof(string));
            dataSet.Tables[1].Columns.Add("DT_REQ", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_TO", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_TO_LINE", typeof(string));
            dataSet.Tables[1].Columns.Add("RATE_EXCHG", typeof(decimal));
            dataSet.Tables[1].Columns.Add("RT_VAT", typeof(string));
            dataSet.Tables[1].Columns.Add("VAT_CLS", typeof(decimal));
            dataSet.Tables[1].Columns.Add("DC_RMK", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("STND_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("UNIT_IM", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_LOT", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_SL", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_FG_RCV", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_PROJECT", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_KOR", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_SYSDEF", typeof(string));
            dataSet.Tables[1].Columns.Add("RATE_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_POST", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_FG_POST", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_NUM", typeof(string));
            dataSet.Tables[1].Columns.Add("QT_REAL", typeof(int));
            dataSet.Tables[1].Columns.Add("NO_IO1", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_IOLINE", typeof(int));
            dataSet.Tables[1].Columns.Add("DT_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_QTIOTP", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_QTIOTP", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_BL", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_BLLINE", typeof(int));
            dataSet.Tables[1].Columns.Add("NM_PURGRP", typeof(string));
            dataSet.Tables[1].Columns.Add("USE_YN", typeof(string));
            dataSet.Tables[1].Columns.Add("PO_PRICE", typeof(string));
            dataSet.Tables[1].Columns.Add("PO_UNIT", typeof(string));
            dataSet.Tables[1].Columns.Add("TP_PURPRICE", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_REV", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_REVLINE", typeof(decimal));
            dataSet.Tables[1].Columns.Add("QT_REQ_M", typeof(int));
            dataSet.Tables[1].Columns.Add("AM_TOTAL", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NO_SERL", typeof(string));
            dataSet.Tables[1].Columns.Add("QT_CLS", typeof(decimal));
            dataSet.Tables[1].Columns.Add("CD_WH", typeof(string));
            dataSet.Tables[1].Columns.Add("DC_RMK2", typeof(string));
            dataSet.Tables[1].Columns.Add("REV_QT_REQ_MM", typeof(decimal));
            dataSet.Tables[1].Columns.Add("REV_AM_EXREQ", typeof(decimal));
            dataSet.Tables[1].Columns.Add("REV_AM_REQ", typeof(decimal));
            dataSet.Tables[1].Columns.Add("CD_PJT_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_PJT_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_PJT_DESIGN", typeof(string));
            dataSet.Tables[1].Columns.Add("PJT_ITEM_STND", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_WBS", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_CBS", typeof(string));
            dataSet.Tables[1].Columns.Add("SEQ_PROJECT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("TP_UM_TAX", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_ITEM_ORIGIN", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_ITEM_ORIGIN", typeof(string));
            dataSet.Tables[1].Columns.Add("STND_ITEM_ORIGIN", typeof(string));
            dataSet.Tables[1].Columns.Add("REV_QT_PASS", typeof(decimal));
            dataSet.Tables[1].Columns.Add("REV_QT_REV_MM", typeof(decimal));
            dataSet.Tables[1].Columns.Add("GI_PARTNER", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_GI_PARTER", typeof(string));
            dataSet.Tables[1].Columns.Add("JAN_QT_SPECIAL", typeof(decimal));
            dataSet.Tables[1].Columns.Add("JAN_QT_PASS", typeof(decimal));
            dataSet.Tables[1].Columns.Add("FG_SPECIAL", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF1", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF2", typeof(string));
            dataSet.Tables[1].Columns.Add("DT_PLAN", typeof(string));
            dataSet.Tables[1].Columns.Add("CLS_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("DATE_USERDEF1", typeof(string));
            dataSet.Tables[1].Columns.Add("CDSL_USERDEF1", typeof(string));
            dataSet.Tables[1].Columns.Add("NMSL_USERDEF1", typeof(string));
            dataSet.Tables[1].Columns.Add("NUM_USERDEF1", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NUM_USERDEF2", typeof(decimal));
            dataSet.Tables[1].Columns.Add("PI_PARTNER", typeof(string));
            dataSet.Tables[1].Columns.Add("PI_LN_PARTNER", typeof(string));
            dataSet.Tables[1].Columns.Add("UM_WEIGHT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("TOT_WEIGHT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("WEIGHT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("MAT_ITEM", typeof(string));
            if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
            {
                dataSet.Tables[1].Columns.Add("CD_PARTNER_PJT", typeof(string));
                dataSet.Tables[1].Columns.Add("LN_PARTNER_PJT", typeof(string));
                dataSet.Tables[1].Columns.Add("NO_EMP_PJT", typeof(string));
                dataSet.Tables[1].Columns.Add("NM_KOR_PJT", typeof(string));
                dataSet.Tables[1].Columns.Add("END_USER", typeof(string));
            }
            foreach (DataTable table3 in dataSet.Tables)
            {
                foreach (DataColumn column in table3.Columns)
                {
                    if (column.DataType == Type.GetType("System.Decimal"))
                        column.DefaultValue = 0;
                    else if (column.DataType == Type.GetType("System.String"))
                        column.DefaultValue = "";
                }
            }
            dataSet.Tables[1].Columns.Add("CD_CSTR", typeof(string));
            dataSet.Tables[1].Columns.Add("DL_CSTR", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_CSTR", typeof(string));
            dataSet.Tables[1].Columns.Add("SIZE_CSTR", typeof(string));
            dataSet.Tables[1].Columns.Add("UNIT_CSTR", typeof(string));
            dataSet.Tables[1].Columns.Add("QTY_ACT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("UNT_ACT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("AMT_ACT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("CD_USERDEF1_RCV", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF2_RCV", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF1_RCV", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF2_RCV", typeof(string));
            dataSet.Tables[0].Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            dataSet.Tables[0].Columns["DT_REQ"].DefaultValue = Global.MainFrame.GetStringToday;
            dataSet.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dataSet.Tables[0].Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dataSet.Tables[0].Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            dataSet.Tables[0].Columns["FG_TRANS"].DefaultValue = "001";
            dataSet.Tables[0].Columns["FG_PROCESS"].DefaultValue = "001";
            dataSet.Tables[0].Columns["YN_AUTORCV"].DefaultValue = "Y";
            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_REQ_REG_1")
            {
                dataSet.Tables[1].Columns.Add("CLS_L", typeof(string));
                dataSet.Tables[1].Columns.Add("NM_CLS_L", typeof(string));
                dataSet.Tables[1].Columns.Add("NUM_STND_ITEM_1", typeof(decimal));
                dataSet.Tables[1].Columns.Add("NUM_STND_ITEM_2", typeof(decimal));
                dataSet.Tables[1].Columns.Add("NUM_STND_ITEM_3", typeof(decimal));
                dataSet.Tables[1].Columns.Add("NUM_STND_ITEM_4", typeof(decimal));
                dataSet.Tables[1].Columns.Add("NUM_STND_ITEM_5", typeof(decimal));
            }
            dataSet.Tables[1].Columns.Add("CD_USERDEF1_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF2_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF3_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF4_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF5_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF1_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF2_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF3_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF4_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("TXT_USERDEF1_PO", typeof(string));
            dataSet.Tables[1].Columns.Add("TXT_USERDEF2_PO", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF5_PO", typeof(string));
            dataSet.Tables[1].Columns.Add("DT_PR", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_SPECIAL", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF1_PRH", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_BUDGET", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_BUDGET", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_BGACCT", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_BGACCT", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_BIZPLAN", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_BIZPLAN", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_ACCT", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_ACCT", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF5_PR", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF6_PR", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF1_POH", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF2_POH", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF3_POH", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF4_POH", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_KOR_PR", typeof(string));
            dataSet.Tables[1].Columns.Add("STND_DETAIL_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_DESIGN", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF6_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("TXT_USERDEF1_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("DC_RMK2_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("NUM_USERDEF1_IO", typeof(decimal));
            dataSet.Tables[1].Columns.Add("YN_LOT", typeof(string));
            dataSet.Tables[1].Columns.Add("DT_LIMIT_LOT", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF2_PI", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_TPPO", typeof(string));
            dataSet.Tables[1].Columns.Add("QT_WEIGHT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("GRP_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_ITEMGRP", typeof(string));
            dataSet.Tables[1].Columns.Add("TP_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_TP_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("DC1_POL", typeof(string));
            dataSet.Tables[1].Columns.Add("NUM_USERDEF2_IO", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NUM_USERDEF3_IO", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NM_MAKER", typeof(string));
            if (MA.ServerKey(false, "THV"))
            {
                dataSet.Tables[1].Columns.Add("CD_THV1", typeof(string));
                dataSet.Tables[1].Columns.Add("CD_THV2", typeof(string));
                dataSet.Tables[1].Columns.Add("CD_THV3", typeof(string));
                dataSet.Tables[1].Columns.Add("CD_THV4", typeof(string));
            }
            dataSet.Tables[1].Columns.Add("EN_ITEM", typeof(string));
            return dataSet;
        }

        public DataTable search_barcode_rev(object[] obj)
        {
            DataTable dataTable = new DataTable();
            return DBHelper.GetDataTable("UP_PU_REQ_REG_1_BARCODE_REV_S", obj);
        }

        public DataTable search_barcode_iqc(object[] obj)
        {
            DataTable dataTable = new DataTable();
            return DBHelper.GetDataTable("UP_PU_REQ_REG_1_BARCODE_IQC_S", obj);
        }

        public string strTelcon(string NO_REV)
        {
            string str = "NOT";
            string query = "SELECT FG_QC   FROM PU_REV  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'    AND NO_REV = '" + NO_REV + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["FG_QC"] != DBNull.Value && dataTable.Rows[0]["FG_QC"].ToString().Trim() != string.Empty)
                str = D.GetString(dataTable.Rows[0]["FG_QC"]);
            return str;
        }

        public DataTable Search_Print(string NO_RCV)
        {
            string spName = "UP_PU_REQ_REG_1_PRINT";
            object[] parameters;
            if (Global.MainFrame.ServerKeyCommon == "CELLBIO")
            {
                spName = "UP_PU_Z_CELLBIO_REQ_REG_1_PRT";
                parameters = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                            NO_RCV };
            }
            else
                parameters = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                            NO_RCV,
                                            Global.MainFrame.LoginInfo.EmployeeNo,
                                            this.Lang };
            return DBHelper.GetDataTable(spName, parameters);
        }

        public object[] GetFI_GWDOCU(string p_no_po)
        {
            object[] fiGwdocu = new object[2]
            {
        999,
        ""
            };
            string query = "SELECT ST_STAT, CD_PC  FROM FI_GWDOCU WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  AND NO_DOCU = '" + p_no_po + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            if (dataTable.Rows.Count > 0 && dataTable.Rows.Count > 0)
            {
                fiGwdocu[0] = D.GetInt(dataTable.Rows[0]["ST_STAT"].ToString());
                fiGwdocu[1] = D.GetString(dataTable.Rows[0]["CD_PC"].ToString());
            }
            return fiGwdocu;
        }

        public bool GW_insert(
          DataRow drHeader,
          string Html_Code,
          string app_form_kind,
          string nm_pumm)
        {
            object[] parameters = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                 Global.MainFrame.LoginInfo.CdPc,
                                                 drHeader["NO_RCV"].ToString(),
                                                 drHeader["NO_EMP"].ToString(),
                                                 drHeader["DT_REQ"].ToString(),
                                                 app_form_kind,
                                                 Html_Code,
                                                 nm_pumm,
                                                 ""};
            return ((ResultData)Global.MainFrame.ExecSp("UP_PU_GWDOCU", parameters)).Result;
        }
    }
}