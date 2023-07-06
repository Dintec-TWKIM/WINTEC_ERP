using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using System;
using System.Data;

namespace cz
{
	internal class P_CZ_PU_CGI_REG_WINTEC_BIZ
	{
        public DataTable Search_LOT() => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PU_MNG_LOT_SELECT",
            CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
            SpParamsSelect = (object[])new string[] { Global.MainFrame.LoginInfo.CompanyCode }
        })).DataValue;

        public DataTable Search_SERIAL() => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PU_MNG_SER_SELECT",
            CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
            SpParamsSelect = (object[])new string[] { Global.MainFrame.LoginInfo.CompanyCode }
        })).DataValue;

        public DataSet Search(string no_io)
        {
            SpInfo spInfo = new SpInfo();
            ResultData resultData = (ResultData)Global.MainFrame.FillDataSet("UP_PU_CGI_SELECT", new object[] { no_io,
                                                                                                                Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                Global.SystemLanguage.MultiLanguageLpoint 
            });
            DataSet dataValue = (DataSet)resultData.DataValue;
            foreach (DataTable table in dataValue.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType == Type.GetType("System.Decimal"))
                        column.DefaultValue = 0;
                }
            }
            DataTable table1 = dataValue.Tables[0];
            table1.Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            table1.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            table1.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            table1.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            table1.Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
            table1.Columns["FG_TRANS"].DefaultValue = "001";
            table1.Columns["YN_RETURN"].DefaultValue = "N";
            table1.Columns["YN_AM"].DefaultValue = "Y";
            table1.Columns["FG_PS"].DefaultValue = "2";
            table1.Columns["CD_EXCH"].DefaultValue = "000";
            table1.Columns["RT_EXCH"].DefaultValue = 1M;
            return (DataSet)resultData.DataValue;
        }

        public DataSet SearchPrint(string no_io)
        {
            SpInfo spInfo = new SpInfo();
            ResultData resultData;
            if (Global.MainFrame.ServerKeyCommon == "YDGLS")
                resultData = (ResultData)Global.MainFrame.FillDataSet("UP_PU_Z_YDGLS_CGI_REG_P", new object[] { no_io,
                                                                                                                Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                Global.SystemLanguage.MultiLanguageLpoint });
            else
                resultData = (ResultData)Global.MainFrame.FillDataSet("UP_PU_CGI_REG_P", new object[] { no_io,
                                                                                                        Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                        Global.SystemLanguage.MultiLanguageLpoint });
            foreach (DataTable table in ((DataSet)resultData.DataValue).Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType == Type.GetType("System.Decimal"))
                        column.DefaultValue = 0;
                }
            }
            return (DataSet)resultData.DataValue;
        }

        public DataTable ItemInfo_Search(object[] m_obj) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PU_COMMON_SELECT_4",
            SpParamsSelect = m_obj
        })).DataValue;

        private ArrayListExt KEY배열(string 멀티KEY)
        {
            ArrayListExt arrayListExt = new ArrayListExt();
            string[] strArray = 멀티KEY.Split('|');
            int num1 = 200;
            int num2 = 1;
            string str = string.Empty;
            for (int index = 0; index < strArray.Length - 1; ++index)
            {
                str = str + strArray[index] + "|";
                ++num2;
                if (num2 == num1 || index == strArray.Length - 2)
                {
                    arrayListExt.Add(str);
                    str = string.Empty;
                    num2 = 1;
                }
            }
            if (str != string.Empty)
                arrayListExt.Add(str);
            return arrayListExt;
        }

        internal DataTable SearchItem(string CD_PLANT, string NO_KEY)
        {
            ArrayListExt arrayListExt = this.KEY배열(NO_KEY);
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = null;
            dataTable1.Columns.Add("멀티KEY", typeof(string));
            for (int index = 0; index < arrayListExt.Count; ++index)
            {
                DataRow row = dataTable1.NewRow();
                row["멀티KEY"] = arrayListExt[index].ToString();
                dataTable1.Rows.Add(row);
            }
            foreach (DataRow row1 in dataTable1.Rows)
            {
                DataTable dataTable4 = this.품목조회(CD_PLANT, row1["멀티KEY"].ToString());
                if (dataTable3 == null)
                    dataTable3 = dataTable4.Clone();
                foreach (DataRow row2 in dataTable4.Rows)
                    dataTable3.ImportRow(row2);
            }
            return dataTable3;
        }

        private DataTable 품목조회(string CD_PLANT, string NO_KEY) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_MM_PITEM_SELECT",
            SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                            CD_PLANT,
                                            NO_KEY }
        })).DataValue;

        internal DataTable SearchPJT(string NO_KEY)
        {
            ArrayListExt arrayListExt = this.KEY배열(NO_KEY);
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = null;
            dataTable1.Columns.Add("멀티KEY", typeof(string));
            for (int index = 0; index < arrayListExt.Count; ++index)
            {
                DataRow row = dataTable1.NewRow();
                row["멀티KEY"] = arrayListExt[index].ToString();
                dataTable1.Rows.Add(row);
            }
            foreach (DataRow row1 in dataTable1.Rows)
            {
                DataTable dataTable4 = this.PJT_SEARCH(row1["멀티KEY"].ToString());
                if (dataTable3 == null)
                    dataTable3 = dataTable4.Clone();
                foreach (DataRow row2 in dataTable4.Rows)
                    dataTable3.ImportRow(row2);
            }
            return dataTable3;
        }

        private DataTable PJT_SEARCH(string NO_KEY) => DBHelper.GetDataTable("UP_PU_PJT_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                NO_KEY,
                                                                                                                Global.SystemLanguage.MultiLanguageLpoint });

        internal DataTable SearchSL(string cd_plant, string multi_sl)
        {
            string[] pipes = D.StringConvert.GetPipes(multi_sl, 200);
            DataTable dataTable = null;
            for (int index = 0; index < pipes.Length; ++index)
            {
                DataTable table = this.SL_SEARCH(cd_plant, pipes[index]);
                if (table != null && table.Rows.Count > 0)
                {
                    if (dataTable == null)
                        dataTable = table.Clone();
                    dataTable.Merge(table);
                }
            }
            return dataTable;
        }

        private DataTable SL_SEARCH(string cd_plant, string multi_cd_sl) => DBHelper.GetDataTable("UP_PU_COMMON_SELECT_8", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                          cd_plant,
                                                                                                                                          multi_cd_sl,
                                                                                                                                          Global.SystemLanguage.MultiLanguageLpoint });

        internal DataTable SearchPARTNER(string NO_KEY)
        {
            ArrayListExt arrayListExt = this.KEY배열(NO_KEY);
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = null;
            dataTable1.Columns.Add("멀티KEY", typeof(string));
            for (int index = 0; index < arrayListExt.Count; ++index)
            {
                DataRow row = dataTable1.NewRow();
                row["멀티KEY"] = arrayListExt[index].ToString();
                dataTable1.Rows.Add(row);
            }
            foreach (DataRow row1 in dataTable1.Rows)
            {
                DataTable dataTable4 = this.PARTNER_SEARCH(row1["멀티KEY"].ToString());
                if (dataTable3 == null)
                    dataTable3 = dataTable4.Clone();
                foreach (DataRow row2 in dataTable4.Rows)
                    dataTable3.ImportRow(row2);
            }
            return dataTable3;
        }

        private DataTable PARTNER_SEARCH(string NO_KEY)
        {
            string empty = string.Empty;
            string sql;
            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                sql = "SELECT CD_PARTNER, LN_PARTNER   FROM DZSN_MA_PARTNER WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  AND CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2('" + NO_KEY + "'))";
            else
                sql = "SELECT CD_PARTNER, LN_PARTNER   FROM MA_PARTNER WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  AND CD_PARTNER IN (SELECT * FROM TABLE(CAST (TF_GETSPLIT('" + NO_KEY + "') AS TB_TF_GETSPLITTAB)))";
            return DBHelper.GetDataTable(sql);
        }

        internal DataTable SearchQTinv(string CD_PLANT, string NO_KEY)
        {
            ArrayListExt arrayListExt = this.KEY배열(NO_KEY);
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = null;
            dataTable1.Columns.Add("멀티KEY", typeof(string));
            for (int index = 0; index < arrayListExt.Count; ++index)
            {
                DataRow row = dataTable1.NewRow();
                row["멀티KEY"] = arrayListExt[index].ToString();
                dataTable1.Rows.Add(row);
            }
            foreach (DataRow row1 in dataTable1.Rows)
            {
                DataTable dataTable4 = this.현재고조회(CD_PLANT, row1["멀티KEY"].ToString());
                if (dataTable3 == null)
                    dataTable3 = dataTable4.Clone();
                foreach (DataRow row2 in dataTable4.Rows)
                    dataTable3.ImportRow(row2);
            }
            return dataTable3;
        }

        private DataTable 현재고조회(string CD_PLANT, string NO_KEY) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PU_PINVN_SELECT",
            SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                            CD_PLANT,
                                            NO_KEY }
        })).DataValue;

        public bool Save(
          DataTable dtH,
          DataTable dtL,
          DataTable dtLOT,
          DataTable dtSERL,
          DataTable dtLOCATION)
        {
            SpInfoCollection spc = new SpInfoCollection();
            SpInfo spInfo = new SpInfo();
            if (dtH != null)
            {
                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_PU_MM_QTIOH_I";
                spInfo.SpNameUpdate = "UP_PU_MM_QTIOH_UPDATE";
                spInfo.SpParamsInsert = new string[] { "NO_IO",
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
                                                       "CD_QTIOTP",
                                                       "CD_DEPT_REQ" };
                spInfo.SpParamsUpdate = new string[] { "NO_IO",
                                                       "CD_COMPANY",
                                                       "ID_INSERT",
                                                       "DC_RMK" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "GI_PARTNER", "");
                spc.Add(spInfo);
            }
            if (dtL != null)
                spc.Add(new SpInfo()
                {
                    DataValue = dtL,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PU_CGI_INSERT",
                    SpNameUpdate = "UP_PU_CGI_UPDATE",
                    SpNameDelete = "UP_PU_MM_QTIO_PAGE_LINE_DELETE",
                    SpParamsInsert = new string[] { "YN_RETURN",
                                                    "NO_IO",
                                                    "NO_IOLINE",
                                                    "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "CD_SL",
                                                    "DT_IO",
                                                    "NO_ISURCV",
                                                    "NO_ISURCVLINE",
                                                    "CD_QTIOTP",
                                                    "CD_PARTNER",
                                                    "CD_ITEM",
                                                    "QT_GOOD_INV",
                                                    "QT_REJECT_INV",
                                                    "YN_AM",
                                                    "CD_PJT",
                                                    "NO_EMP",
                                                    "FG_TPIO",
                                                    "UM_EX_PSO",
                                                    "UM_EX",
                                                    "UM",
                                                    "AM",
                                                    "AM_EX",
                                                    "UNIT_PO_FACT",
                                                    "NO_IO_MGMT",
                                                    "DC_RMK",
                                                    "NO_IOLINE_MGMT",
                                                    "CD_CC",
                                                    "SEQ_PROJECT",
                                                    "QT_UNIT_MM",
                                                    "QT_EMP",
                                                    "GI_PARTNER",
                                                    "DC_RMK1",
                                                    "CD_USERDEF1",
                                                    "CD_USERDEF2",
                                                    "CD_USERDEF3",
                                                    "CD_USERDEF4",
                                                    "CD_USERDEF5",
                                                    "NM_USERDEF1",
                                                    "NUM_USERDEF1",
                                                    "NO_WBS",
                                                    "NO_CBS",
                                                    "NO_LINE_PJTBOM",
                                                    "ID_INSERT",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "TXT_USERDEF1_QTIO",
                                                    "TXT_USERDEF2_QTIO",
                                                    "NUM_USERDEF2" },
                    SpParamsUpdate = new string[] { "NO_IO",
                                                    "NO_IOLINE",
                                                    "CD_COMPANY",
                                                    "QT_GOOD_OLD",
                                                    "QT_GOOD_INV",
                                                    "QT_REJECT_OLD",
                                                    "QT_REJECT_INV",
                                                    "UM_EX_PSO",
                                                    "UM_EX",
                                                    "UM",
                                                    "AM",
                                                    "AM_EX",
                                                    "UNIT_PO_FACT",
                                                    "CD_PJT",
                                                    "DC_RMK",
                                                    "CD_CC",
                                                    "SEQ_PROJECT",
                                                    "QT_UNIT_MM",
                                                    "QT_EMP",
                                                    "FG_TPIO",
                                                    "NUM_USERDEF1",
                                                    "ID_UPDATE" },
                    SpParamsDelete = new string[] { "NO_IO",
                                                    "NO_IOLINE",
                                                    "CD_COMPANY" },
                    SpParamsValues = { { ActionState.Insert, "DC_RMK1", "" },
                                       { ActionState.Insert, "CD_USERDEF3", "" },
                                       { ActionState.Insert, "CD_USERDEF4", "" },
                                       { ActionState.Insert, "CD_USERDEF5", "" } }
                });
            if (dtLOT != null)
                spc.Add(new SpInfo()
                {
                    DataValue = dtLOT,
                    DataState = DataValueState.Added,
                    SpNameInsert = "UP_MM_QTIOLOT_INSERT",
                    SpNameUpdate = "UP_MM_QTIOLOT_UPDATE",
                    SpNameDelete = "UP_MM_QTIOLOT_DELETE",
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "출고번호",
                                                    "출고항번",
                                                    "NO_LOT",
                                                    "CD_ITEM",
                                                    "출고일",
                                                    "FG_PS",
                                                    "FG_IO",
                                                    "수불형태",
                                                    "창고코드",
                                                    "QT_GOOD_MNG",
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
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "출고번호",
                                                    "출고항번",
                                                    "NO_LOT",
                                                    "QT_IO",
                                                    "QT_IO_OLD" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "출고번호",
                                                    "출고항번",
                                                    "NO_LOT" },
                    SpParamsValues = { { ActionState.Insert, "CD_PLANT_PR", "" },
                                       { ActionState.Insert, "NO_IO_PR", "" },
                                       { ActionState.Insert, "NO_LINE_IO_PR", 0 },
                                       { ActionState.Insert, "NO_LINE_IO2_PR", 0 },
                                       { ActionState.Insert, "FG_SLIP_PR", "" },
                                       { ActionState.Insert, "NO_LOT_PR", "" },
                                       { ActionState.Insert, "P_NO_SO", "" },
                                       { ActionState.Insert, "P_CD_PLANT", "" },
                                       { ActionState.Insert, "P_ROOT_NO_LOT", "" },
                                       { ActionState.Insert, "P_ID_INSERT", "" },
                                       { ActionState.Insert, "P_BEF_NO_LOT", "" },
                                       { ActionState.Insert, "P_FG_LOT_ADD", "N" },
                                       { ActionState.Insert, "P_BARCODE", "" } }
                });
            if (dtSERL != null)
                spc.Add(new SpInfo()
                {
                    DataValue = dtSERL,
                    SpNameInsert = "UP_MM_QTIODS_INSERT",
                    SpNameUpdate = "UP_MM_QTIODS_UPDATE",
                    SpNameDelete = "UP_MM_QTIODS_DELETE",
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpParamsInsert = new string[] { "CD_COMPANY",
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
                                                    "CD_MNG20" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
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
                                                    "CD_MNG20" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_IO",
                                                    "NO_IOLINE",
                                                    "NO_SERIAL" }
                });
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
                                                    "CD_PLANT",
                                                    "CD_SL",
                                                    "QT_IO_LOCATION",
                                                    "YN_RETURN" }
                });
            return DBHelper.Save(spc);
        }

        public void Delete(string NO_IO) => DBHelper.ExecuteNonQuery("UP_PU_GRM_DELETE", new object[] { NO_IO,
                                                                                                        Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                        Global.MainFrame.LoginInfo.UserID,
                                                                                                        Global.MainFrame.CurrentPageID });

        public DataSet SEARCH_PARTNER_DATA(object[] obj) => DBHelper.GetDataSet("UP_PU_CGI_PARTNER_SELECT", obj);

        internal DataSet Search_um_prioritize_item2(object[] obj) => DBHelper.GetDataSet("UP_PU_UM_TOTAL_SELECT2", obj);

        internal string Get설정(string 코드)
        {
            switch (코드)
            {
                case "수불형태코드":
                    return Settings.Default.수불형태코드;
                case "수불형태명":
                    return Settings.Default.수불형태명;
                case "회사코드":
                    return Settings.Default.회사코드;
                case "FG_IO":
                    return Settings.Default.FG_IO;
                case "YN_AM":
                    Settings.Default.YN_AM = D.GetString(CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드, Settings.Default.수불형태코드 })["YN_AM"]);
                    return Settings.Default.YN_AM;
                default:
                    return "";
            }
        }

        internal void Set설정(string 코드, string val)
        {
            switch (코드)
            {
                case "수불형태코드":
                    Settings.Default.수불형태코드 = val;
                    break;
                case "수불형태명":
                    Settings.Default.수불형태명 = val;
                    break;
                case "회사코드":
                    Settings.Default.회사코드 = val;
                    break;
                case "FG_IO":
                    Settings.Default.FG_IO = val;
                    break;
                case "YN_AM":
                    Settings.Default.YN_AM = val;
                    break;
            }
            Settings.Default.Save();
        }

        internal DataTable Get_FG_IO(string 수불형태코드) => DBHelper.GetDataTable("\r\n                                            SELECT FG_IO\r\n                                              FROM MM_EJTP\r\n                                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                               AND CD_QTIOTP = '" + 수불형태코드 + "'\r\n                                            ");

        internal DataTable getAmFromCode_KYOTECH() => DBHelper.GetDataTable("SELECT CD_FLAG1   FROM DZSN_MA_CODEDTL WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND CD_SYSDEF = '001'   AND CD_FIELD = 'CZ_KYO002'");
    }
}