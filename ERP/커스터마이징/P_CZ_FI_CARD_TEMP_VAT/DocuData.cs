using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace cz
{
    internal class DocuData
    {
        private string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
        private DataTable _dt카드 = (DataTable)null;

        internal DocuData(DataTable dt카드) => this._dt카드 = dt카드;

        internal void Set부가세처리시거래처셋팅(DataRow[] dr부가세처리)
        {
            if (dr부가세처리 == null || dr부가세처리.Length == 0)
                return;
            DataTable dataTable1 = dr부가세처리[0].Table.Clone();
            foreach (DataRow dataRow in dr부가세처리)
                Debug.WriteLine("전 : " + D.GetString(dataRow["TRADE_PLACE"]));
            foreach (DataRow row in dr부가세처리)
                dataTable1.ImportRow(row);
            foreach (DataRow dataRow in dr부가세처리)
                Debug.WriteLine("후 : " + D.GetString(dataRow["TRADE_PLACE"]));
            DBHelper.Save(new SpInfo()
            {
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                UserID = Global.MainFrame.LoginInfo.UserID,
                DataValue = (object)dataTable1,
                DataState = DataValueState.Added,
                SpNameInsert = "UP_FI_CARD_TEMP_VAT_I",
                SpParamsInsert = new string[7]
              {
          "CD_COMPANY",
          "S_IDNO",
          "TRADE_PLACE",
          "MERC_REPR",
          "MCC_CODE_NAME",
          "MERC_ADDR",
          "VAT_TYPE"
              },
                SpParamsValues = {
          {
            ActionState.Insert,
            "SERVERKEY",
            (object) Global.MainFrame.ServerKeyCommon.ToUpper()
          }
        }
            });
            DataTable table1 = (DataTable)null;
            foreach (string pipe in D.StringConvert.GetPipes(dr부가세처리, 300, "S_IDNO"))
            {
                DataTable dataTable2 = DBHelper.GetDataTable("UP_FI_CARD_TEMP_VAT_S2", new object[2]
                {
          (object) Global.MainFrame.LoginInfo.CompanyCode,
          (object) pipe
                });
                if (table1 == null)
                    table1 = dataTable2;
                else
                    table1.Merge(dataTable2);
            }
            if (table1 == null)
                return;
            DataTable table2 = new DataView(table1).ToTable(true, "NO_COMPANY", "CD_PARTNER", "LN_PARTNER");
            table2.PrimaryKey = new DataColumn[1]
            {
        table2.Columns["NO_COMPANY"]
            };
            for (int index = 0; index < dr부가세처리.Length; ++index)
            {
                DataRow dataRow = table2.Rows.Find(dr부가세처리[index]["S_IDNO"]);
                if (dataRow != null)
                {
                    dr부가세처리[index]["CD_PARTNER"] = dataRow["CD_PARTNER"];
                    dr부가세처리[index]["LN_PARTNER"] = dataRow["LN_PARTNER"];
                    dr부가세처리[index]["TRADE_PLACE"] = dataRow["LN_PARTNER"];
                }
            }
            foreach (DataRow dataRow in dr부가세처리)
                Debug.WriteLine("거래처 : " + D.GetString(dataRow["TRADE_PLACE"]));
        }

        internal Hashtable Get전표옵션(DataTable dt카드, Hashtable ht)
        {
            DataRow[] dataRowArray = dt카드.Select("YN_VAT = 'Y'");
            if (dataRowArray == null || dataRowArray.Length == 0)
                ht.Add((object)"부가세처리", (object)false);
            else
                ht.Add((object)"부가세처리", (object)true);
            ht.Add((object)"회계일자", (object)Global.MainFrame.GetStringToday);
            return ht;
        }

        internal void Set부가세미처리건거래처셋팅(DataRow[] dr거래처)
        {
            DataTable table1 = (DataTable)null;
            foreach (string pipe in D.StringConvert.GetPipes(dr거래처, 300, "S_IDNO"))
            {
                DataTable dataTable = DBHelper.GetDataTable("UP_FI_CARD_TEMP_VAT_S2", new object[2]
                {
          (object) Global.MainFrame.LoginInfo.CompanyCode,
          (object) pipe
                });
                if (table1 == null)
                    table1 = dataTable;
                else
                    table1.Merge(dataTable);
            }
            if (table1 == null)
                return;
            DataTable table2 = new DataView(table1).ToTable(true, "NO_COMPANY", "CD_PARTNER", "LN_PARTNER");
            table2.PrimaryKey = new DataColumn[1]
            {
        table2.Columns["NO_COMPANY"]
            };
            for (int index = 0; index < dr거래처.Length; ++index)
            {
                DataRow dataRow = table2.Rows.Find(dr거래처[index]["S_IDNO"]);
                if (dataRow != null && !(D.GetString(dataRow["CD_PARTNER"]) == string.Empty))
                {
                    dr거래처[index]["CD_PARTNER"] = dataRow["CD_PARTNER"];
                    dr거래처[index]["LN_PARTNER"] = dataRow["LN_PARTNER"];
                }
            }
        }
    }
}