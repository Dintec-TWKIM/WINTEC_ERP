using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
	internal class P_CZ_PU_LOT_SUB_R_BIZ
	{
        private string 언어 = Global.SystemLanguage.MultiLanguageLpoint;

        internal DataTable Search_Detail(string NO_IO)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_LOT_R_SELECT_SUB", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                              NO_IO });
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable ExcelSearch(string 멀티품목코드, string 구분자, string CD_PLANT)
        {
            ArrayListExt arrayListExt = this.arr엑셀(멀티품목코드);
            DataTable dataTable = null;
            for (int index = 0; index < arrayListExt.Count; ++index)
            {
                DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
                {
                    SpNameSelect = "UP_SA_PTRPRICE_SELECT_L_EXCEL",
                    SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                    arrayListExt[index].ToString(),
                                                    구분자,
                                                    CD_PLANT,
                                                    this.언어 }
                })).DataValue;
                if (dataTable == null)
                {
                    dataTable = dataValue;
                }
                else
                {
                    foreach (DataRow row in dataValue.Rows)
                        dataTable.ImportRow(row);
                }
            }
            return dataTable;
        }

        internal DataTable 엑셀(DataTable dt_엑셀)
        {
            dt_엑셀.Columns.Add("NO_IO", typeof(string));
            dt_엑셀.Columns.Add("DT_IO", typeof(string));
            dt_엑셀.Columns.Add("FG_IO", typeof(string));
            dt_엑셀.Columns.Add("CD_QTIOTP", typeof(string));
            dt_엑셀.Columns.Add("CD_SL", typeof(string));
            dt_엑셀.Columns.Add("FG_PS", typeof(string));
            return dt_엑셀;
        }

        public DataTable TotalLine(string KEY, string TP_UMMODULE) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PU_UMPARTNER_SELECT_TOTAL_L",
            SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                            KEY,
                                            TP_UMMODULE }
        })).DataValue;

        private ArrayListExt arr엑셀(string 멀티품목코드)
        {
            int num1 = 50;
            int num2 = 1;
            string str = string.Empty;
            ArrayListExt arrayListExt = new ArrayListExt();
            string[] strArray = 멀티품목코드.Split('|');
            for (int index = 0; index < strArray.Length - 1; ++index)
            {
                str = str + strArray[index].ToString() + "|";
                if (num2 == num1)
                {
                    arrayListExt.Add(str);
                    str = string.Empty;
                    num2 = 0;
                }
                ++num2;
            }
            if (str != string.Empty)
                arrayListExt.Add(str);
            return arrayListExt;
        }

        internal DataTable dt_SER_MGMT(string NO_IO_MGMT) => DBHelper.GetDataTable("UP_PU_LOTSER_MGMT_S", new object[] { MA.Login.회사코드,
                                                                                                                         NO_IO_MGMT,
                                                                                                                         "LOT" });

        public string GetMaxLot(string CD_ITEM, string strServer, string cd_plant, string dt_io)
        {
            string sql;
            if (strServer == "MAKUS")
                sql = " SELECT MAX(NO_LOT) NO_LOT    FROM MM_QTIOLOT   WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'    AND (CD_ITEM    = '" + CD_ITEM + "' OR '" + CD_ITEM + "' = '') ";
            else if (strServer == "AMOS")
                sql = " SELECT MAX(NO_LOT) NO_LOT    FROM MM_QTIOLOT   WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'    AND CD_PLANT   = '" + cd_plant + "'    AND NO_LOT  LIKE '" + dt_io + "%'";
            else
                sql = " SELECT MAX(LOT.NO_LOT) NO_LOT    FROM MM_QTIOLOT LOT   INNER JOIN MA_PITEM P ON LOT.CD_ITEM = P.CD_ITEM AND P.CD_COMPANY = LOT.CD_COMPANY  WHERE LOT.CD_COMPANY = '" + MA.Login.회사코드 + "'    AND ((LOT.FG_IO = '001' AND P.CLS_ITEM IN ('001','002')) OR (LOT.FG_IO = '007' AND P.CLS_ITEM = '001'))";
            DataTable dataTable = DBHelper.GetDataTable(sql);
            return dataTable != null && dataTable.Rows.Count != 0 ? D.GetString(dataTable.Rows[0]["NO_LOT"]) : "";
        }

        public string Getdt(string CD_ITEM, string cd_plant)
        {
            DataTable dataTable = DBHelper.GetDataTable(" SELECT TXT_USERDEF44    FROM MA_Z_KB_PITEM_SUB  WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'    AND CD_PLANT = '" + cd_plant + "' AND CD_ITEM = '" + CD_ITEM + "'");
            return dataTable != null && dataTable.Rows.Count != 0 ? D.GetString(dataTable.Rows[0]["TXT_USERDEF44"]) : "";
        }

        internal DataTable getSOL_DAOU(object[] obj) => DBHelper.GetDataTable("UP_PU_Z_DAOU_SA_SOL_DATA_S", obj);

        internal DataTable getBARCODE_AXT(object[] obj) => DBHelper.GetDataTable("UP_PU_Z_AXT_GET_BARCODE_S", obj);

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
            SpNameSelect = "UP_PU_Z_AXT_GET_BARCODE_S",
            SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                            CD_PLANT,
                                            NO_KEY }
        })).DataValue;

        internal DataTable getMAX_LOT_DAIWA(string CD_ITEM, string CD_PARTNER, string DT_IO) => DBHelper.GetDataTable("UP_PU_Z_DAIWA_MAX_LOT_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                                                CD_ITEM,
                                                                                                                                                                CD_PARTNER,
                                                                                                                                                                DT_IO });

        internal DataSet getDefData_KYOTECH() => DBHelper.GetDataSet("UP_PU_Z_KYOTECH_GET_DATA_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode });
    }
}