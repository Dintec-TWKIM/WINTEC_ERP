using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using pur;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_PU_ITR_REG_WINTEC_BIZ
    {
        private string 공장코드 = string.Empty;
        private string Lang = Global.SystemLanguage.MultiLanguageLpoint.ToString();

        public string Search_SERIAL()
        {
            string str = "N";
            DataTable dataTable = DBHelper.GetDataTable("UP_PU_MNG_SER_SELECT", new string[] { Global.MainFrame.LoginInfo.CompanyCode });
            if (dataTable.Rows.Count > 0)
            {
                str = dataTable.Rows[0]["YN_SERIAL"].ToString();
                if (str == string.Empty)
                    str = "N";
            }
            return str;
        }

        public DataSet Search(string no_io, string FG_LOAD, string CD_PLANT)
        {
            SpInfo spInfo = new SpInfo();
            ResultData resultData = (ResultData)Global.MainFrame.FillDataSet("UP_PU_ITR_SELECT", new object[] { no_io,
                                                                                                                Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                FG_LOAD,
                                                                                                                CD_PLANT,
                                                                                                                this.Lang });
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
            table1.Columns[nameof(CD_PLANT)].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            table1.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            table1.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            table1.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            table1.Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
            table1.Columns["YN_RETURN"].DefaultValue = "N";
            return (DataSet)resultData.DataValue;
        }

        public DataTable Item_Search(string CD_PLANT, string CD_ITEM) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PU_ITEM_SELECT",
            CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
            SpParamsSelect = new string[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                      CD_PLANT,
                                                      CD_ITEM,
                                                      this.Lang }
        })).DataValue;

        public bool Save(
          DataTable dtH,
          DataTable dtL,
          DataTable dtLOT,
          string FG_LOAD,
          DataRow drHeader,
          DataTable dtSERL,
          DataTable dt_location)
        {
            SpInfoCollection spc = new SpInfoCollection();
            if (dtH != null)
                spc.Add(new SpInfo()
                {
                    DataValue = dtH,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PU_MM_QTIOH_INSERT",
                    SpNameUpdate = "UP_PU_MM_QTIOH_UPDATE",
                    SpParamsInsert = new string[] { "NO_IO",
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
                                                    "FG_TRACK",
                                                    "TXT_USERDEF1",
                                                    "CD_DEPT_REQ" },
                    SpParamsUpdate = new string[] { "NO_IO",
                                                    "CD_COMPANY",
                                                    "ID_INSERT",
                                                    "DC_RMK" },
                    SpParamsValues = { { ActionState.Insert, "GI_PARTNER", "" },
                                       { ActionState.Insert, "FG_TRACK", "" },
                                       { ActionState.Insert, "TXT_USERDEF1", "" }
          }
                });
            if (dtL != null)
            {
                this.공장코드 = D.GetString(drHeader["CD_PLANT"]);
                spc.Add(new SpInfo()
                {
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    DataValue = dtL,
                    SpNameInsert = "UP_PU_ITR_INSERT",
                    SpNameUpdate = "UP_PU_ITR_UPDATE",
                    SpNameDelete = "UP_PU_MM_QTIO_PAGE_LINE_DELETE",
                    SpParamsInsert = new string[] { "YN_RETURN",
                                                    "NO_IO",
                                                    "NO_IOLINE",
                                                    "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "CD_SL",
                                                    "DT_IO",
                                                    "FG_PS",
                                                    "CD_QTIOTP",
                                                    "FG_TRANS",
                                                    "CD_PARTNER",
                                                    "CD_ITEM",
                                                    "QT_GOOD_INV",
                                                    "QT_REJECT_INV",
                                                    "UM",
                                                    "AM",
                                                    "YN_AM",
                                                    "NO_EMP",
                                                    "CD_PJT",
                                                    FG_LOAD == "2" ? "NO_IO_MGMT" : "NO_IO_MGMT_APPLY",
                                                    "NO_IOLINE_MGMT_APPLY",
                                                    "DC_RMK",
                                                    "FG_TPIO",
                                                    "UNIT_PO_FACT",
                                                    "UM_EX",
                                                    "AM_EX",
                                                    "UM_EX_PSO",
                                                    "NO_ISURCV",
                                                    "NO_ISURCVLINE",
                                                    "SEQ_PROJECT",
                                                    "CD_CC",
                                                    "DC_RMK1",
                                                    "GI_PARTNER",
                                                    "QT_UNIT_MM",
                                                    "NUM_USERDEF1",
                                                    "NO_CBS",
                                                    "NO_WBS",
                                                    "NO_LINE_PJTBOM" },
                    SpParamsUpdate = new string[] { "NO_IO",
                                                    "NO_IOLINE",
                                                    "CD_COMPANY",
                                                    "QT_GOOD_OLD",
                                                    "QT_GOOD_INV",
                                                    "UM_OLD",
                                                    "UM",
                                                    "DC_RMK",
                                                    "CD_PJT",
                                                    "AM",
                                                    "UNIT_PO_FACT",
                                                    "UM_EX",
                                                    "AM_EX",
                                                    "UM_EX_PSO",
                                                    "CD_CC",
                                                    "DC_RMK1",
                                                    "QT_UNIT_MM",
                                                    "NUM_USERDEF1",
                                                    "NO_CBS" },
                    SpParamsDelete = new string[] { "NO_IO",
                                                    "NO_IOLINE",
                                                    "CD_COMPANY" }
                });
            }
            if (dtLOT != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtLOT;
                spInfo.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                spInfo.SpNameUpdate = "UP_MM_QTIOLOT_UPDATE";
                spInfo.SpNameDelete = "UP_MM_QTIOLOT_DELETE";
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                if (D.GetString(drHeader["YN_RETURN"]) == "N")
                {
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
                    spInfo.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", drHeader["YN_RETURN"].ToString());
                }
                else
                {
                    spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
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
                                                           "CD_MNG20" };
                    spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                           "출고번호",
                                                           "출고항번",
                                                           "NO_LOT",
                                                           "QT_IO",
                                                           "QT_IO_OLD" };
                    spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                           "출고번호",
                                                           "출고항번",
                                                           "NO_IOLINE2",
                                                           "NO_LOT" };
                    spInfo.SpParamsValues.Add(ActionState.Insert, "YN_RETURN_QTIO", drHeader["YN_RETURN"].ToString());
                }
                spInfo.SpParamsValues.Add(ActionState.Insert, "CD_PLANT_PR", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_IO_PR", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO_PR", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO2_PR", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "FG_SLIP_PR", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LOT_PR", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "P_NO_SO", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "P_CD_PLANT", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "P_ROOT_NO_LOT", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "P_ID_INSERT", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "P_BEF_NO_LOT", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "P_FG_LOT_ADD", "N");
                spInfo.SpParamsValues.Add(ActionState.Insert, "P_BARCODE", "");
                spc.Add(spInfo);
            }
            if (dtSERL != null)
                spc.Add(new SpInfo()
                {
                    DataValue = dtSERL,
                    SpNameInsert = "UP_MM_QTIODS_INSERT",
                    SpNameUpdate = "UP_MM_QTIODS_UPDATE",
                    SpNameDelete = "UP_MM_QTIODS_DELETE",
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.UserID,
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
                                                    "CD_MNG20",
                                                    "CD_PLANT",
                                                    "ID_INSERT" },
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
                                                    "NO_SERIAL" },
                    SpParamsValues = { { ActionState.Insert, "CD_PLANT", this.공장코드 },
                                       { ActionState.Insert, "ID_INSERT", Global.MainFrame.LoginInfo.UserID } }
                });
            if (dt_location != null && dt_location.Rows.Count > 0)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dt_location;
                spInfo.DataState = DataValueState.Added;
                spInfo.SpNameInsert = "UP_MM_QTIO_LOCATION_I";
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
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
                                                       "YN_RETURN" };
                if (!dt_location.Columns.Contains("YN_RETURN"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", dtH.Rows[0]["YN_RETURN"].ToString());
                spc.Add(spInfo);
            }
            return DBHelper.Save(spc);
        }

        public bool Delete(object[] m_obj) => DBHelper.ExecuteNonQuery("UP_PU_GRM_DELETE", m_obj);

        public DataTable Search_Print(object[] obj)
        {
            DataTable dataTable = null;
            if (MA.ServerKey(true, "DONG-AH"))
                dataTable = DBHelper.GetDataTable("UP_PU_Z_DONGAH_ITR_REG_PRINT", obj);
            return dataTable;
        }

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
                                            NO_KEY,
                                            this.Lang }
        })).DataValue;

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
                                                                                                                                          this.Lang });

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
                                                                                                                this.Lang });

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
                sql = "SELECT CD_PARTNER, LN_PARTNER   FROM DZSN_MA_PARTNER WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  AND CD_PARTNER IN (SELECT * FROM TABLE(CAST (TF_GETSPLIT('" + NO_KEY + "') AS TB_TF_GETSPLITTAB)))";
            return DBHelper.GetDataTable(sql);
        }

        public DataTable Search_um_item(object[] obj) => DBHelper.GetDataTable("UP_PU_ITR_REG_ITEM_UM_SELECT", obj);

        internal DataSet Search_um_prioritize_item(object[] obj) => DBHelper.GetDataSet("UP_PU_UM_TOTAL_SELECT", obj);

        internal DataSet Search_um_prioritize_item2(object[] obj) => DBHelper.GetDataSet("UP_PU_UM_TOTAL_SELECT2", obj);

        public string[] GetNoIo(string id_memo)
        {
            string[] noIo = new string[2] { "", "" };
            string query = "SELECT A.NO_IO, A.CD_PLANT   FROM   MM_QTIO A  WHERE A.ID_MEMO = '" + id_memo + "'   AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                noIo[0] = D.GetString(dataTable.Rows[0]["NO_IO"]);
                noIo[1] = D.GetString(dataTable.Rows[0]["CD_PLANT"]);
            }
            return noIo;
        }

        internal string Get설정(string 코드)
        {
            switch (코드)
            {
                case "수불형태코드":
                    return P_PU_ITR_REG_st.Default.수불형태코드;
                case "수불형태명":
                    return P_PU_ITR_REG_st.Default.수불형태명;
                case "회사코드":
                    return P_PU_ITR_REG_st.Default.회사코드;
                case "FG_IO":
                    return P_PU_ITR_REG_st.Default.FG_IO;
                case "YN_AM":
                    P_PU_ITR_REG_st.Default.YN_AM = D.GetString(CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드, P_PU_ITR_REG_st.Default.수불형태코드 })["YN_AM"]);
                    return P_PU_ITR_REG_st.Default.YN_AM;
                default:
                    return "";
            }
        }

        internal void Set설정(string 코드, string val)
        {
            switch (코드)
            {
                case "수불형태코드":
                    P_PU_ITR_REG_st.Default.수불형태코드 = val;
                    break;
                case "수불형태명":
                    P_PU_ITR_REG_st.Default.수불형태명 = val;
                    break;
                case "회사코드":
                    P_PU_ITR_REG_st.Default.회사코드 = val;
                    break;
                case "FG_IO":
                    P_PU_ITR_REG_st.Default.FG_IO = val;
                    break;
                case "YN_AM":
                    P_PU_ITR_REG_st.Default.YN_AM = val;
                    break;
            }
            P_PU_ITR_REG_st.Default.Save();
        }

        internal DataTable Get_FG_IO(string 수불형태코드) => DBHelper.GetDataTable("\r\n                                        SELECT FG_IO, TP_VARIATION\r\n                                        FROM MM_EJTP\r\n                                        WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                            AND CD_QTIOTP = '" + 수불형태코드 + "'\r\n                                        ");

        internal DataTable GET_AmWegint(string MULTI_GRPITEM) => DBHelper.GetDataTable("SELECT CD_NO_PK_2 AS GRP_ITEM, DC_1\r\n                                             FROM PU_CUST_PUBLIC\r\n                                            WHERE CD_NO_PK_1 = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                              AND CD_NO_PK_2 IN ( " + MULTI_GRPITEM + ")\r\n                                              AND NM_BUSINESS = 'P_PU_Z_SINJINSM_ITEMGRP_SG'\r\n                                         ");

        internal DataTable SaveCheck(string NO_IO, string NO_IOLINE) => DBHelper.GetDataTable("\t\r\n                SELECT  DS.NO_IO,\r\n\t\t\t\t\t    DS.NO_SERIAL,\r\n\t\t\t\t\t    E.NM_QTIOTP\r\n\t\t\t\tFROM\tMM_QTIODS DS INNER JOIN MM_QTIO L ON DS.NO_IO = L.NO_IO AND DS.NO_IOLINE = L.NO_IOLINE AND DS.CD_COMPANY = L.CD_COMPANY\r\n\t\t\t\tINNER JOIN MM_QTIOH H ON H.NO_IO = L.NO_IO AND H.CD_COMPANY = L.CD_COMPANY\r\n\t\t\t\tINNER JOIN (\r\n                    SELECT CD_COMPANY,CD_ITEM, NO_SERIAL \r\n\t\t\t\t\t  FROM MM_QTIODS \r\n\t\t\t\t\t WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' \r\n                       AND NO_IO = '" + NO_IO + "' \r\n                       AND NO_IOLINE = " + NO_IOLINE + "\r\n                ) VT ON DS.CD_ITEM = VT.CD_ITEM AND DS.NO_SERIAL = VT.NO_SERIAL AND DS.CD_COMPANY = VT.CD_COMPANY\r\n\t\t\t\tLEFT JOIN MM_EJTP E ON E.CD_QTIOTP = L.CD_QTIOTP AND E.CD_COMPANY = L.CD_COMPANY\r\n\t\t\t\tWHERE   DS.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                AND\t    ((L.FG_PS = '2' AND H.YN_RETURN = 'N') OR (L.FG_PS = '1' AND H.YN_RETURN = 'Y'))");
    }
}
