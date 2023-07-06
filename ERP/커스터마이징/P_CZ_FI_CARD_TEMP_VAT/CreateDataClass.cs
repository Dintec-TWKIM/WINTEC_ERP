using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    internal class CreateDataClass
    {
        private DataTable _dtData;
        private DataTable _dt집계;
        private DataTable _dt집계Sum;
        private DataTable _dt카드사용자;
        private DataTable _dtCBS;
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz = new P_CZ_FI_CARD_TEMP_VAT_BIZ();

        internal CreateDataClass(DataTable dtData) => this._dtData = dtData;

        internal CreateDataClass(DataSet ds)
        {
            if (MA.ServerKey(false, "NICEIT1", "NICEIT11"))
                this._dtData = this.ChangeCostAcct_Nice(ds.Tables[0]);
            else
                this._dtData = !MA.ServerKey(false, "DBCAS") ? ds.Tables[0] : this.CheckRed(ds.Tables[0]);
            if (MA.ServerKey(true, "CBS", "119"))
                this._dtCBS = ds.Tables[3];
            this._dt집계Sum = ds.Tables[1];
            this._dt집계 = ds.Tables[1];
            this._dt카드사용자 = ds.Tables[2];
        }

        internal bool binding()
        {
            this._dt집계.PrimaryKey = new DataColumn[1]
            {
        this._dt집계.Columns["NO_CARD"]
            };
            foreach (IEnumerable<object> source in this._dt카드사용자.Rows.Cast<DataRow>().ToList<DataRow>().GroupBy<DataRow, object, object>((Func<DataRow, object>)(row => row["NO_CARD"]), (Func<DataRow, object>)(row => row["NO_CARD"])))
            {
                if (source.Count<object>() > 1)
                {
                    int num = (int)Global.MainFrame.ShowMessage("불출정보등록에 오류가 있습니다.불출정보에 회수처리되지 않고 불출만 두번이상 등록된 카드가 있는지 확인하여주시기 바랍니다");
                    return false;
                }
            }
            this._dt카드사용자.PrimaryKey = new DataColumn[1]
            {
        this._dt카드사용자.Columns["NO_CARD"]
            };
            return true;
        }

        internal DataTable CreateData()
        {
            this._dtData.Columns.Add("TP_SUM", typeof(string));
            this._dtData.Merge(this.GetCreate소계Table());
            return this.SetSortingTable();
        }

        private DataTable GetCreate소계Table()
        {
            DataTable dataTable = this._dtData.Clone();
            foreach (DataRow row1 in (InternalDataCollectionBase)this._dtData.DefaultView.ToTable(true, "NO_CARD").Rows)
            {
                DataRow row2 = dataTable.NewRow();
                row2["TP_SORT"] = (object)"2";
                row2["NO_CARD"] = row1["NO_CARD"];
                row2["TP_SUM"] = (object)"승인합계";
                dataTable.Rows.Add(row2);
                DataRow row3 = dataTable.NewRow();
                row3["TP_SORT"] = (object)"2";
                row3["NO_CARD"] = row1["NO_CARD"];
                row3["TP_SUM"] = (object)"취소합계";
                dataTable.Rows.Add(row3);
                DataRow row4 = dataTable.NewRow();
                row4["TP_SORT"] = (object)"2";
                row4["NO_CARD"] = row1["NO_CARD"];
                row4["TP_SUM"] = (object)"계";
                dataTable.Rows.Add(row4);
            }
            DataRow row = dataTable.NewRow();
            row["TP_SORT"] = (object)"2";
            row["NO_CARD"] = row["TP_SUM"] = (object)"합계";
            dataTable.Rows.Add(row);
            dataTable.AcceptChanges();
            return dataTable;
        }

        private DataTable SetSortingTable()
        {
            DataTable table = new DataView(this._dtData, string.Empty, "NO_CARD, TP_SORT ASC", DataViewRowState.CurrentRows).ToTable();
            for (int index = 0; index < table.Rows.Count; ++index)
            {
                switch (table.Rows[index]["TP_SUM"].ToString())
                {
                    case "승인합계":
                        this.승인취소소계(index, table.Rows[index]["NO_CARD"].ToString(), "Y", "승인합계", ref table);
                        table.Rows[index]["NO_CARD"] = (object)Global.MainFrame.DD("승인합계");
                        break;
                    case "취소합계":
                        this.승인취소소계(index, table.Rows[index]["NO_CARD"].ToString(), "N", "취소합계", ref table);
                        table.Rows[index]["NO_CARD"] = (object)Global.MainFrame.DD("승인취소");
                        break;
                    case "계":
                        this.승인취소소계(index, table.Rows[index]["NO_CARD"].ToString(), "N", "합계", ref table);
                        table.Rows[index]["NO_CARD"] = (object)Global.MainFrame.DD("계");
                        break;
                    case "합계":
                        this.승인취소합계(index, ref table);
                        table.Rows[index]["NO_CARD"] = (object)Global.MainFrame.DD("합계");
                        break;
                }
                DataRow dataRow = this._dt카드사용자.Rows.Find((object)table.Rows[index]["NO_CARD"].ToString().Replace("-", ""));
                if (dataRow != null && (D.GetInt(table.Rows[index]["TRADE_DATE"]) > D.GetInt(dataRow["DT_RE"]) || D.GetInt(table.Rows[index]["TRADE_DATE"]) == D.GetInt(dataRow["DT_RE"]) && D.GetInt((object)table.Rows[index]["TRADE_TIME"].ToString().Substring(0, 2)) >= D.GetInt(dataRow["DT_RE2"])))
                    table.Rows[index]["NM_OWNER"] = dataRow["NM_KOR"];
                if (MA.ServerKey(true, "CBS", "119") && D.GetString(table.Rows[index]["TP_SORT"]) == "1" && D.GetString(table.Rows[index]["NM_OWNER"]) == string.Empty)
                {
                    DataRow[] dataRowArray = this._dtCBS.Select("NO_CARD = '" + table.Rows[index]["NO_CARD"] + "' AND ( (DT_RE + DT_RE2 < " + table.Rows[index]["TRADE_DATE"].ToString() + table.Rows[index]["TRADE_TIME"].ToString().Substring(0, 4) + " AND DT_CO + DT_CO2  > " + table.Rows[index]["TRADE_DATE"].ToString() + table.Rows[index]["TRADE_TIME"].ToString().Substring(0, 4) + ") OR (DT_RE + DT_RE2 <= " + table.Rows[index]["TRADE_DATE"].ToString() + table.Rows[index]["TRADE_TIME"].ToString().Substring(0, 4) + " AND DT_CO = '00000000') )");
                    if (dataRowArray == null || dataRowArray.Length == 0)
                        table.Rows[index]["NM_OWNER"] = table.Rows[index]["CARD_OWNER"];
                }
            }
            return table;
        }

        private void 승인취소합계(int idx, ref DataTable dt)
        {
            Decimal num1 = 0M;
            Decimal num2 = 0M;
            Decimal num3 = 0M;
            if (this._dt집계Sum.Rows.Count > 0 && this._dt집계Sum != null)
            {
                num1 = Math.Abs(D.GetDecimal(this._dt집계Sum.Compute("SUM(ADMIN_AMT_Y)", ""))) - Math.Abs(D.GetDecimal(this._dt집계Sum.Compute("SUM(ADMIN_AMT_N)", "")));
                num2 = Math.Abs(D.GetDecimal(this._dt집계Sum.Compute("SUM(SUPPLY_AMT_Y)", ""))) - Math.Abs(D.GetDecimal(this._dt집계Sum.Compute("SUM(SUPPLY_AMT_N)", "")));
                num3 = Math.Abs(D.GetDecimal(this._dt집계Sum.Compute("SUM(VAT_AMT_Y)", ""))) - Math.Abs(D.GetDecimal(this._dt집계Sum.Compute("SUM(VAT_AMT_N)", "")));
            }
            dt.Rows[idx]["ADMIN_AMT"] = (object)num1;
            dt.Rows[idx]["SUPPLY_AMT"] = (object)num2;
            dt.Rows[idx]["VAT_AMT"] = (object)num3;
        }

        private void 승인취소소계(int idx, string NO_CARD, string 승인구분, string 합계구분, ref DataTable dt)
        {
            DataRow dataRow = this._dt집계.Rows.Find((object)NO_CARD.Replace("-", ""));
            Decimal num1 = 0M;
            Decimal num2 = 0M;
            Decimal num3 = 0M;
            if (dataRow != null)
            {
                if (합계구분 == "합계")
                {
                    num1 = Math.Abs(D.GetDecimal(dataRow["ADMIN_AMT_Y"])) - Math.Abs(D.GetDecimal(dataRow["ADMIN_AMT_N"]));
                    num2 = Math.Abs(D.GetDecimal(dataRow["SUPPLY_AMT_Y"])) - Math.Abs(D.GetDecimal(dataRow["SUPPLY_AMT_N"]));
                    num3 = Math.Abs(D.GetDecimal(dataRow["VAT_AMT_Y"])) - Math.Abs(D.GetDecimal(dataRow["VAT_AMT_N"]));
                }
                else
                {
                    num1 = 승인구분 == "Y" ? D.GetDecimal(dataRow["ADMIN_AMT_Y"]) : D.GetDecimal(dataRow["ADMIN_AMT_N"]);
                    num2 = 승인구분 == "Y" ? D.GetDecimal(dataRow["SUPPLY_AMT_Y"]) : D.GetDecimal(dataRow["SUPPLY_AMT_N"]);
                    num3 = 승인구분 == "Y" ? D.GetDecimal(dataRow["VAT_AMT_Y"]) : D.GetDecimal(dataRow["VAT_AMT_N"]);
                }
            }
            dt.Rows[idx]["ADMIN_AMT"] = (object)num1;
            dt.Rows[idx]["SUPPLY_AMT"] = (object)num2;
            dt.Rows[idx]["VAT_AMT"] = (object)num3;
        }

        private DataTable ChangeCostAcct_Nice(DataTable dt)
        {
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                row["CD_COSTACCT"] = row["CD_USERDEF1"];
                row["NM_COSTACCT"] = (object)this.Get계정명(row["CD_USERDEF1"].ToString());
                row["NM_NOTE"] = row["CD_USERDEF2"];
            }
            return dt;
        }

        private DataTable CheckRed(DataTable dt)
        {
            DataTable holiday = this._biz.GetHoliday();
            DataTable codeData = this._biz.GetCodeData("FI_Z_HCA01");
            dt.Columns.Add("CHECK", typeof(string));
            string str1 = string.Empty;
            string str2 = string.Empty;
            DataRow[] dataRowArray;
            if (codeData != null && codeData.Rows.Count > 0)
            {
                for (int index = 0; index < codeData.Rows.Count; ++index)
                    str1 = index != codeData.Rows.Count - 1 ? str1 + "'" + codeData.Rows[index]["NM_SYSDEF"].ToString() + "', " : str1 + "'" + codeData.Rows[index]["NM_SYSDEF"].ToString() + "'";
                dataRowArray = (DataRow[])null;
                foreach (DataRow dataRow in dt.Select("MCC_CODE_NAME IN (" + str1 + ")", string.Empty, DataViewRowState.CurrentRows))
                    dataRow["CHECK"] = (object)"Y";
            }
            if (holiday != null && holiday.Rows.Count > 0)
            {
                for (int index = 0; index < holiday.Rows.Count; ++index)
                    str2 = index != holiday.Rows.Count - 1 ? str2 + "'" + holiday.Rows[index]["DT_DATE"].ToString() + "', " : str2 + "'" + holiday.Rows[index]["DT_DATE"].ToString() + "'";
                dataRowArray = (DataRow[])null;
                foreach (DataRow dataRow in dt.Select("TRADE_DATE IN (" + str2 + ")", string.Empty, DataViewRowState.CurrentRows))
                    dataRow["CHECK"] = (object)"Y";
            }
            dataRowArray = (DataRow[])null;
            foreach (DataRow dataRow in dt.Select("TRADE_TIME >= '000000' AND TRADE_TIME <='055900'", string.Empty, DataViewRowState.CurrentRows))
                dataRow["CHECK"] = (object)"Y";
            dt.AcceptChanges();
            return dt;
        }

        private DataTable updateTRADETIME(DataTable dt)
        {
            foreach (DataRow dataRow in dt.Select("TRADE_TIME LIKE '% '"))
            {
                string str = dataRow["TRADE_TIME"].ToString().Replace(" ", "");
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" UPDATE CARD_TEMP_BAK190515 ");
                stringBuilder.Append(" SET TRADE_TIME = '" + str + "'");
                stringBuilder.Append(" WHERE ACCT_NO = '" + D.GetString(dataRow["ACCT_NO"]) + "'");
                stringBuilder.Append(" AND BANK_CODE = '" + dataRow["BANK_CODE"] + "'");
                stringBuilder.Append(" AND TRADE_DATE = '" + dataRow["TRADE_DATE"] + "'");
                stringBuilder.Append(" AND TRADE_TIME '" + dataRow["TRADE_TIME"] + "'");
                stringBuilder.Append(" AND SEQ = '" + dataRow["SEQ"] + "'");
                DBHelper.ExecuteScalar(stringBuilder.ToString());
            }
            return new DataTable();
        }

        private string Get계정명(string 계정코드)
        {
            DataTable dataTable = DBHelper.GetDataTable("SELECT NM_ACCT FROM FI_ACCTCODE WHERE CD_ACCT ='" + 계정코드 + "' AND CD_COMPANY ='" + MA.Login.회사코드 + "'");
            string empty = string.Empty;
            if (dataTable != null && dataTable.Rows.Count == 1)
                empty = dataTable.Rows[0]["NM_ACCT"].ToString();
            return empty;
        }
    }
}