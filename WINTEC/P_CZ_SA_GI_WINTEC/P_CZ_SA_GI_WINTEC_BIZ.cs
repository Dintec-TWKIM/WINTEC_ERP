using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_SA_GI_WINTEC_BIZ
    {
        private string 출하등록_검사 = "000";

        public P_CZ_SA_GI_WINTEC_BIZ() => this.출하등록_검사 = BASIC.GetMAEXC(nameof(출하등록_검사));

        public DataTable Search(object[] obj, string str조회일자구분)
        {
            string str = "XXXXXXXXXX_XXXXXXXXXX";
            switch (str조회일자구분)
            {
                case "GI":
                    str = "UP_CZ_SA_GI_WINTEC_H_GI_S";
                    break;
                case "DU":
                    str = "UP_CZ_SA_GI_WINTEC_H_DU_S";
                    break;
                case "RQ":
                    str = "UP_CZ_SA_GI_WINTEC_H_RQ_S";
                    break;
            }
            if (MA.ServerKey(false, new string[1] { "THV" }))
            {
                switch (str조회일자구분)
                {
                    case "GI":
                        str = "UP_SA_Z_THV_GI_H_GI_S";
                        break;
                    case "DU":
                        str = "UP_SA_Z_THV_GI_H_DU_S";
                        break;
                    case "RQ":
                        str = "UP_SA_Z_THV_GI_H_RQ_S";
                        break;
                }
            }
            DataTable dataTable = DBHelper.GetDataTable(str, obj);
            T.SetDefaultValue(dataTable);
            dataTable.Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
            dataTable.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dataTable.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            return dataTable;
        }

        public DataTable SearchDetail(object[] obj, string str조회일자구분)
        {
            string str = "XXXXXXXXXX_XXXXXXXXXX";
            switch (str조회일자구분)
            {
                case "GI":
                    str = "UP_SA_GI_L_GI_S";
                    break;
                case "DU":
                    str = "UP_SA_GI_L_DU_S";
                    break;
                case "RQ":
                    str = "UP_SA_GI_L_RQ_S";
                    break;
            }
            if (MA.ServerKey(false, new string[] { "SFNB" }))
            {
                switch (str조회일자구분)
                {
                    case "GI":
                        str = "UP_Z_SA_SFNB_GI_L_GI_S";
                        break;
                    case "DU":
                        str = "UP_Z_SA_SFNB_GI_L_DU_S";
                        break;
                    case "RQ":
                        str = "UP_Z_SA_SFNB_GI_L_RQ_S";
                        break;
                }
            }
            else if (MA.ServerKey(false, new string[] { "THV" }))
            {
                switch (str조회일자구분)
                {
                    case "GI":
                        str = "UP_SA_Z_THV_GI_L_GI_S";
                        break;
                    case "DU":
                        str = "UP_SA_Z_THV_GI_L_DU_S";
                        break;
                    case "RQ":
                        str = "UP_SA_Z_THV_GI_L_RQ_S";
                        break;
                }
            }
            else if (MA.ServerKey(false, new string[] { "DAOU" }))
            {
                switch (str조회일자구분)
                {
                    case "GI":
                        str = "UP_SA_Z_DAOU_GI_L_GI_S";
                        break;
                    case "DU":
                        str = "UP_SA_Z_DAOU_GI_L_DU_S";
                        break;
                    case "RQ":
                        str = "UP_SA_Z_DAOU_GI_L_RQ_S";
                        break;
                }
            }
            DataTable dataTable = DBHelper.GetDataTable(str, obj);
            if (!dataTable.Columns.Contains("DC_RMK1"))
                dataTable.Columns.Add("DC_RMK1", typeof(string));
            if (!dataTable.Columns.Contains("DC_RMK2"))
                dataTable.Columns.Add("DC_RMK2", typeof(string));
            if (!dataTable.Columns.Contains("QTIO_CD_USERDEF3"))
                dataTable.Columns.Add("QTIO_CD_USERDEF3", typeof(string));
            if (!dataTable.Columns.Contains("QTIO_CD_USERDEF4"))
                dataTable.Columns.Add("QTIO_CD_USERDEF4", typeof(string));
            if (!dataTable.Columns.Contains("QTIO_CD_USERDEF5"))
                dataTable.Columns.Add("QTIO_CD_USERDEF5", typeof(string));
            if (!dataTable.Columns.Contains("GI_WEIGHT"))
                dataTable.Columns.Add("GI_WEIGHT", typeof(Decimal));
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchCheckHeader(string 멀티의뢰번호, object[] obj)
        {
            DataTable dataTable = null;
            try
            {
                MsgControl.ShowMsg("자료를 조회중 입니다.");
                foreach (string pipe in D.StringConvert.GetPipes(멀티의뢰번호, 150))
                {
                    obj[1] = pipe;
                    DataTable table = !MA.ServerKey(false, new string[] { "THV" }) ? DBHelper.GetDataTable("UP_SA_GI_S1", obj) : DBHelper.GetDataTable("UP_SA_Z_THV_GI_S1", obj);
                    if (dataTable == null)
                        dataTable = table;
                    else
                        dataTable.Merge(table);
                }
                for (int index = 0; index < dataTable.Rows.Count; ++index)
                    dataTable.Rows[index]["S"] = "Y";
            }
            finally
            {
                MsgControl.CloseMsg();
            }
            return dataTable;
        }

        public DataRow CheckCredit(
          string cd_Partner,
          string cd_Partner_name,
          Decimal am_sum,
          DataRow ex_Dr)
        {
            string str = "003";
            ResultData resultData = (ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_SA_CREDIT_POINT_SELECT",
                SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                cd_Partner,
                                                am_sum,
                                                str }
            });
            if (resultData.OutParamsSelect[0, 2] != DBNull.Value && Convert.ToDecimal(resultData.OutParamsSelect[0, 2]) < am_sum)
            {
                ex_Dr["S"] = "N";
                ex_Dr["CD_PARTNER"] = cd_Partner;
                ex_Dr["CD_PARTNER_NAME"] = cd_Partner_name;
                ex_Dr["CREDIT_TOT"] = Convert.ToDecimal(resultData.OutParamsSelect[0, 3]);
                ex_Dr["MISU_REMAIN"] = Convert.ToDecimal(resultData.OutParamsSelect[0, 1]);
                ex_Dr["CREDIT_RAMAIN"] = Convert.ToDecimal(resultData.OutParamsSelect[0, 2]);
                ex_Dr["AM_SUM"] = am_sum;
                if (resultData.OutParamsSelect[0, 0].ToString() == "002")
                    ex_Dr["EX_CONTENT"] = "WARNING";
                else if (resultData.OutParamsSelect[0, 0].ToString() == "003")
                    ex_Dr["EX_CONTENT"] = "ERROR";
            }
            return ex_Dr;
        }

        public bool Save(
          DataTable dt_H,
          DataTable dt_L,
          DataTable dtLocation,
          DataTable dt_LOT,
          DataTable dt_SERIAL,
          string 출하일자,
          DataTable dt_ASN)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            if (dt_H != null)
            {
                dt_H.RemotingFormat = SerializationFormat.Binary;
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dt_H,
                    DataState = (DataValueState)1,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_SA_GI_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_IO",
                                                    "CD_PLANT",
                                                    "CD_PARTNER",
                                                    "FG_TRANS",
                                                    "DT_IO",
                                                    "CD_DEPT",
                                                    "NO_EMP",
                                                    "DC_RMK",
                                                    "YN_RETURN",
                                                    "ID_INSERT" }
                });
            }
            if (dt_L != null)
            {
                dt_L.RemotingFormat = SerializationFormat.Binary;
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dt_L;
                spInfo.DataState = (DataValueState)1;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_SA_GI_INSERT1";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_PLANT",
                                                       "CD_BIZAREA",
                                                       "CD_SL",
                                                       "DT_IO",
                                                       "NO_ISURCV",
                                                       "NO_ISURCVLINE",
                                                       "NO_PSO_MGMT",
                                                       "NO_PSOLINE_MGMT",
                                                       "YN_PURSALE",
                                                       "FG_TPIO",
                                                       "CD_QTIOTP",
                                                       "FG_TRANS",
                                                       "FG_TAX",
                                                       "CD_PARTNER",
                                                       "CD_ITEM",
                                                       "QT_IO",
                                                       "QT_GOOD_INV",
                                                       "CD_EXCH",
                                                       "RT_EXCH",
                                                       "UM_EX",
                                                       "AM_EX",
                                                       "UM",
                                                       "AM",
                                                       "VAT",
                                                       "FG_TAXP",
                                                       "CD_PJT",
                                                       "NO_LC",
                                                       "NO_LCLINE",
                                                       "GI_PARTNER",
                                                       "NO_EMP",
                                                       "CD_GROUP",
                                                       "CD_UNIT_MM",
                                                       "QT_UNIT_MM",
                                                       "UM_EX_PSO",
                                                       "YN_AM",
                                                       "FG_IO",
                                                       "NO_IO_MGMT",
                                                       "NO_IOLINE_MGMT",
                                                       "FG_LC_OPEN",
                                                       "ID_INSERT",
                                                       "QT_GR_PASS_NEW",
                                                       "QT_GR_BAD_NEW",
                                                       "QT_BAD_INV_NEW",
                                                       "CD_WH",
                                                       "YN_INSPECT",
                                                       "DC_RMK",
                                                       "SEQ_PROJECT",
                                                       "QTIO_CD_USERDEF1",
                                                       "QTIO_CD_USERDEF2",
                                                       "TP_UM_TAX",
                                                       "UMVAT_GI",
                                                       "DC_RMK1",
                                                       "DC_RMK2",
                                                       "QTIO_CD_USERDEF3",
                                                       "QTIO_CD_USERDEF4",
                                                       "QTIO_CD_USERDEF5",
                                                       "NM_USERDEF4",
                                                       "NM_USERDEF1",
                                                       "NM_USERDEF2",
                                                       "NUM_USERDEF1",
                                                       "NUM_USERDEF2",
                                                       "GL_TXT_USERDEF1" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "QT_GR_PASS_NEW", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "QT_GR_BAD_NEW", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "QT_BAD_INV_NEW", 0);
                if (!dt_L.Columns.Contains("NM_USERDEF4"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NM_USERDEF4", string.Empty);
                if (!dt_L.Columns.Contains("NM_USERDEF1"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NM_USERDEF1", string.Empty);
                if (!dt_L.Columns.Contains("NM_USERDEF2"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NM_USERDEF2", string.Empty);
                if (!dt_L.Columns.Contains("NUM_USERDEF1"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NUM_USERDEF1", 0);
                if (!dt_L.Columns.Contains("NUM_USERDEF2"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NUM_USERDEF2", 0);
                if (!dt_L.Columns.Contains("GL_TXT_USERDEF1"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "GL_TXT_USERDEF1", string.Empty);
                spInfoCollection.Add(spInfo);
            }
            if (dt_LOT != null)
            {
                dt_LOT.RemotingFormat = SerializationFormat.Binary;
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dt_LOT;
                spInfo.DataState = DataValueState.Added;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
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
                                                       "YN_RETURN",
                                                       "DUMMY_TEMP_CD_PLANT_PR",
                                                       "DUMMY_TEMP_NO_IO_PR",
                                                       "DUMMY_TEMP_NO_LINE_IO_PR",
                                                       "DUMMY_TEMP_NO_LINE_IO2_PR",
                                                       "DUMMY_TEMP_FG_SLIP_PR",
                                                       "DUMMY_TEMP_NO_LOT_PR",
                                                       "DUMMY_TEMP_P_NO_SO",
                                                       "DT_LIMIT",
                                                       "DC_LOTRMK",
                                                       "DUMMY_TEMP_CD_PLANT",
                                                       "DUMMY_TEMP_ROOT_NO_LOT",
                                                       "ID_INSERT",
                                                       "DUMMY_BEF_NO_LOT",
                                                       "DUMMY_TEMP_FG_LOT_ADD",
                                                       "DUMMY_TEMP_BARCODE",
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
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_CD_PLANT_PR", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_IO_PR", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_LINE_IO_PR", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_LINE_IO2_PR", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_FG_SLIP_PR", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_LOT_PR", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_P_NO_SO", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_CD_PLANT", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_ROOT_NO_LOT", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_BEF_NO_LOT", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_FG_LOT_ADD", "N");
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_BARCODE", string.Empty);
                spInfoCollection.Add(spInfo);
            }
            if (dt_SERIAL != null)
            {
                dt_SERIAL.RemotingFormat = SerializationFormat.Binary;
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dt_SERIAL,
                    SpNameInsert = "UP_MM_QTIODS_INSERT",
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
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
                                                    "CD_MNG20",
                                                    "CD_PLANT",
                                                    "ID_INSERT",
                                                    "NO_REV",
                                                    "NO_REVLINE" }
                });
            }
            if (dtLocation != null && dtLocation.Rows.Count > 0)
            {
                dtLocation.RemotingFormat = SerializationFormat.Binary;
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtLocation;
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
                if (!dtLocation.Columns.Contains("DT_IO"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "DT_IO", 출하일자);
                if (!dtLocation.Columns.Contains("FG_IO"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "FG_IO", "010");
                if (!dtLocation.Columns.Contains("YN_RETURN"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", "N");
                spInfoCollection.Add(spInfo);
            }
            if (MA.ServerKey(false, new string[] { "ANJUN" }) && dt_ASN != null)
            {
                dt_ASN.RemotingFormat = SerializationFormat.Binary;
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dt_ASN,
                    SpNameInsert = "UP_SA_Z_ANJUN_ASNNO_REG_I",
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.UserID,
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_IO",
                                                    "NO_IOLINE",
                                                    "NO_ASN",
                                                    "CD_PLANT",
                                                    "CD_ITEM",
                                                    "DT_IO",
                                                    "CD_PLANT_DELV",
                                                    "QT",
                                                    "ID_INSERT" }
                });
            }
            if (spInfoCollection.List == null)
                return false;
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spInfoCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }

        public string search_SERIAL(object[] obj) => D.GetString(DBHelper.ExecuteScalar("UP_PU_MNG_SER_SELECT", obj));

        public DataTable search_EnvMng() => DBHelper.GetDataTable("UP_SA_ENV_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode });

        internal string Get출하등록_검사 => this.출하등록_검사;

        public DataTable SearchInv(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_SA_GI_INV_S", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable get품목_안전재고(string CD_PLANT, string CD_ITEM)
        {
            string getStringToday = Global.MainFrame.GetStringToday;
            return DBHelper.GetDataTable("  SELECT QT_SSTOCK, CD_ITEM, NM_ITEM   FROM\tMA_PITEM   WHERE CD_COMPANY= '" + Global.MainFrame.LoginInfo.CompanyCode + "'  AND CD_PLANT= '" + CD_PLANT + "'  AND CD_ITEM = '" + CD_ITEM + "'");
        }
    }
}
