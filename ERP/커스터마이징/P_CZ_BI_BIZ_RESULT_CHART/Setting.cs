using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using account;
using Duzon.ERPU;
using Duzon.Common.Forms;
using System.Diagnostics;

namespace cz
{
    internal class Setting
    {
        private DataSet _dsData;
        private DataTable _dtAcct;
        private DataTable _dt;
        private Dictionary<int, string[]> _기간데이터;
        private string _기준년월F;

        internal DataTable SetDtAcct
        {
            set
            {
                this._dtAcct = value;
            }
        }

        internal DataSet SetDsData
        {
            set
            {
                this._dsData = value;
            }
        }

        internal DataTable GetDt
        {
            get
            {
                return this._dt;
            }
        }

        internal Setting(Dictionary<int, string[]> 기간데이터, bool Is누적, string 기준년월F)
        {
            this._기간데이터 = 기간데이터;
            this._기준년월F = 기준년월F;
            
            if (Is누적) this.Set누적(기준년월F);
            
            this._dt = this.CreateSchema();
        }

        private void Set누적(string 기준년월F)
        {
            foreach (int key in this._기간데이터.Keys)
                this._기간데이터[key][1] = 기준년월F;
        }

        private DataTable CreateSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CD_PACCT", typeof(string));
            dt.Columns.Add("NM_PACCT", typeof(string));
            dt.Columns.Add("L_AM_CUR", typeof(decimal));
            dt.Columns.Add("RT_CUR", typeof(decimal));

            foreach (int key in this._기간데이터.Keys)
            {
                dt.Columns.Add("AM_CUR" + D.GetInt(key), typeof(decimal));
                dt.Columns.Add("RT_CUR" + D.GetInt(key), typeof(decimal));
            }
            
            P_FI_PL_BASE.SetAddBaseCols(dt);
            dt.Columns.Add("STOCK_L_AM_CUR_PIPE", typeof(string));
            T.SetDefaultValue(dt);
            
            return dt;
        }

        internal void SetTable(bool YN_CLOSEIN2007_IV)
        {
            DataTable table1 = this._dsData.Tables[0];
            DataTable table2 = this._dsData.Tables[1];
            DataTable table3 = this._dsData.Tables[2];
            DataTable table4 = this._dsData.Tables[3];
            DataTable table5 = this._dsData.Tables[4];
            DataTable dataTable = null;

            Stopwatch time = new Stopwatch();
            time.Start();

            if (this._dsData.Tables.Count >= 6)
                dataTable = this._dsData.Tables[5];
            
            foreach (DataRow row in this._dtAcct.Rows)
            {
                MsgControl.ShowMsg("손익계산서 생성중... (2/3)" + Environment.NewLine + 
                                   "계정명 :" + "(" + row["CD_PACCT"].ToString() + ") " + row["NM_PACCT"].ToString() + Environment.NewLine +
                                   "경과시간 : " + time.Elapsed.ToString());
                Decimal num1 = 0;
                string str1 = string.Empty;
                string str2 = string.Empty;
                DataRow dataRow = this._dt.NewRow();
                dataRow["CD_PACCT"] = row["CD_PACCT"];
                dataRow["NM_PACCT"] = row["NM_PACCT"];
                
                P_FI_PL_BASE.SetDataBaseCols(dataRow, row);

                if (D.GetInt(row["TP_GRPCAL"]) == FG_GRPCAL.계정.GetHashCode())
                {
                    if (D.GetInt(row["TP_PRINTTYPE"]) == 26)
                    {
                        dataRow["L_AM_CUR"] = D.GetDecimal(table5.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ")"));
                        foreach (int key in this._기간데이터.Keys)
                            dataRow["AM_CUR" + D.GetInt(key)] = D.GetDecimal(table5.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[key][1] + "' AND YM_ACCT <='" + this._기간데이터[key][2] + "'"));
                    }
                    else
                    {
                        if (D.GetInt(row["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가1.GetHashCode() || D.GetInt(row["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가2.GetHashCode() || (D.GetInt(row["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가3.GetHashCode() || D.GetInt(row["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가4.GetHashCode()))
                        {
                            if (D.GetInt(row["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가1.GetHashCode())
                                str1 = "1";
                            
                            if (D.GetInt(row["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가2.GetHashCode())
                                str1 = "2";
                            
                            if (D.GetInt(row["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가3.GetHashCode())
                                str1 = "3";
                            
                            if (D.GetInt(row["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가4.GetHashCode())
                                str1 = "4";
                            
                            string str3 = string.Empty;

                            if (YN_CLOSEIN2007_IV)
                                str3 = " AND TP_PRINTTYPE NOT IN ('63','65','67','69','64','66','68','70') ";
                            
                            if (dataTable != null && dataTable.Rows.Count != 0)
                                num1 = D.GetDecimal(dataTable.Compute("SUM(AM_SUM)", "PRINTTYPE = '" + str1 + "'" + str3));
                            
                            Decimal num4 = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_DR_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_DR_PIPE"])) + ")"));
                            Decimal num5 = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_CR_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_CR_PIPE"])) + ")"));
                            dataRow["L_AM_CUR"] = (D.GetString(row["TP_DRCR"]) == "1" ? num4 - num5 + num1 : num5 - num4 + num1);
                            
                            foreach (int key in this._기간데이터.Keys)
                            {
                                num1 = D.GetDecimal(dataTable.Compute("SUM(AM_SUM)", "PRINTTYPE = '" + str1 + "'" + str3 + " AND YM_ACCT >= '" + this._기간데이터[key][1] + "' AND YM_ACCT <='" + this._기간데이터[key][2] + "'"));
                                Decimal num6 = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_DR_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_DR_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[key][1] + "' AND YM_ACCT <='" + this._기간데이터[key][2] + "'"));
                                Decimal num7 = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_CR_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_CR_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[key][1] + "' AND YM_ACCT <='" + this._기간데이터[key][2] + "'"));
                                dataRow["AM_CUR" + D.GetInt(key)] = (D.GetString(row["TP_DRCR"]) == "1" ? num6 - num7 + num1 : num7 - num6 + num1);
                            }
                        }
                        else
                        {
                            Decimal num4 = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_DR_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_DR_PIPE"])) + ")"));
                            Decimal num5 = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_CR_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_CR_PIPE"])) + ")"));
                            dataRow["L_AM_CUR"] = (D.GetString(row["TP_DRCR"]) == "1" ? num4 - num5 : num5 - num4);
                            
                            foreach (int key in this._기간데이터.Keys)
                            {
                                Decimal num6 = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_DR_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_DR_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[key][1] + "' AND YM_ACCT <='" + this._기간데이터[key][2] + "'"));
                                Decimal num7 = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_CR_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_CR_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[key][1] + "' AND YM_ACCT <='" + this._기간데이터[key][2] + "'"));
                                dataRow["AM_CUR" + D.GetInt(key)] = (D.GetString(row["TP_DRCR"]) == "1" ? num6 - num7 : num7 - num6);
                            }
                        }
                    }
                }
                if (D.GetInt(row["TP_GRPCAL"]) == FG_GRPCAL.대차계정.GetHashCode())
                {
                    switch (D.GetInt(row["GOOD_CD_GOOD"]))
                    {
                        case 1:
                            dataRow["L_AM_CUR"] = D.GetDecimal(table2.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT <'" + this._기준년월F.Substring(0, 6) + "'"));
                            using (Dictionary<int, string[]>.KeyCollection.Enumerator enumerator = this._기간데이터.Keys.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    int current = enumerator.Current;
                                    dataRow["AM_CUR" + D.GetInt(current)] = D.GetDecimal(table2.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT <'" + this._기간데이터[current][1] + "'"));
                                    dataRow["AM_CUR" + D.GetInt(current)] = (D.GetDecimal(dataRow["AM_CUR" + D.GetInt(current)]) - D.GetDecimal(table2.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + row["CD_ACCT_CHAGAM_PIPE"] + ") AND YM_ACCT <'" + this._기간데이터[current][1] + "'")));
                                }
                                break;
                            }
                        case 2:
                            if (dataTable == null || dataTable.Rows.Count == 0)
                            {
                                dataRow["L_AM_CUR"] = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ")"));
                                using (Dictionary<int, string[]>.KeyCollection.Enumerator enumerator = this._기간데이터.Keys.GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        int current = enumerator.Current;
                                        dataRow["AM_CUR" + D.GetInt(current)] = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[current][1] + "' AND YM_ACCT <='" + this._기간데이터[current][2] + "'"));
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                DataRow[] dataRowArray = this._dtAcct.Select("CD_ACCT = '" + row["CD_ACCT"].ToString() + "'");
                                if (dataRowArray.Length != 0)
                                {
                                    if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가1.GetHashCode())
                                        str1 = "1";
                                    if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가2.GetHashCode())
                                        str1 = "2";
                                    if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가3.GetHashCode())
                                        str1 = "3";
                                    if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가4.GetHashCode())
                                        str1 = "4";
                                    string str3 = string.Empty;
                                    if (YN_CLOSEIN2007_IV)
                                        str3 = " AND TP_PRINTTYPE NOT IN ('63','65','67','69','64','66','68','70') ";
                                    Decimal num2 = D.GetDecimal(dataTable.Compute("SUM(AM_SUM)", "PRINTTYPE = '" + str1 + "'" + str3));
                                    dataRow["L_AM_CUR"] = !(num2 > new Decimal(0)) ? (D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ")")) - Math.Abs(num2)) : (D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ")")) + num2);
                                    using (Dictionary<int, string[]>.KeyCollection.Enumerator enumerator = this._기간데이터.Keys.GetEnumerator())
                                    {
                                        while (enumerator.MoveNext())
                                        {
                                            int current = enumerator.Current;
                                            Decimal num3 = D.GetDecimal(dataTable.Compute("SUM(AM_SUM)", "PRINTTYPE = '" + str1 + "'" + str3 + " AND YM_ACCT >= '" + this._기간데이터[current][1] + "' AND YM_ACCT <='" + this._기간데이터[current][2] + "'"));
                                            if (num3 > new Decimal(0))
                                                dataRow["AM_CUR" + D.GetInt(current)] = (D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[current][1] + "' AND YM_ACCT <='" + this._기간데이터[current][2] + "'")) + num3);
                                            else
                                                dataRow["AM_CUR" + D.GetInt(current)] = (D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[current][1] + "' AND YM_ACCT <='" + this._기간데이터[current][2] + "'")) - Math.Abs(num3));
                                        }
                                        break;
                                    }
                                }
                                else
                                    break;
                            }
                        case 3:
                            dataRow["L_AM_CUR"] = D.GetDecimal(table1.Compute("SUM(IN_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ")"));
                            using (Dictionary<int, string[]>.KeyCollection.Enumerator enumerator = this._기간데이터.Keys.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    int current = enumerator.Current;
                                    dataRow["AM_CUR" + D.GetInt(current)] = D.GetDecimal(table1.Compute("SUM(IN_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[current][1] + "' AND YM_ACCT <='" + this._기간데이터[current][2] + "'"));
                                }
                                break;
                            }
                        case 4:
                        case 9:
                            dataRow["L_AM_CUR"] = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ")"));
                            foreach (int key in this._기간데이터.Keys)
                                dataRow["AM_CUR" + D.GetInt(key)] = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[key][1] + "' AND YM_ACCT <='" + this._기간데이터[key][2] + "'"));
                            if (YN_CLOSEIN2007_IV && dataTable != null && dataTable.Rows.Count != 0)
                            {
                                DataRow[] dataRowArray = this._dtAcct.Select("CD_ACCT = '" + row["CD_ACCT"].ToString() + "'");
                                if (dataRowArray.Length != 0 && D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) >= FG_PTYPE.제품매출원가1.GetHashCode() && D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) <= FG_PTYPE.제품매출원가4.GetHashCode())
                                {
                                    if (D.GetInt(row["GOOD_CD_GOOD"]) == 4)
                                    {
                                        if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가1.GetHashCode())
                                            str2 = "63";
                                        else if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가2.GetHashCode())
                                            str2 = "65";
                                        else if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가3.GetHashCode())
                                            str2 = "67";
                                        else if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가4.GetHashCode())
                                            str2 = "69";
                                    }
                                    else if (D.GetInt(row["GOOD_CD_GOOD"]) == 9)
                                    {
                                        if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가1.GetHashCode())
                                            str2 = "64";
                                        else if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가2.GetHashCode())
                                            str2 = "66";
                                        else if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가3.GetHashCode())
                                            str2 = "68";
                                        else if (D.GetInt(dataRowArray[0]["TP_PRINTTYPE"]) == FG_PTYPE.제품매출원가4.GetHashCode())
                                            str2 = "70";
                                    }
                                    if (dataTable != null && dataTable.Rows.Count != 0)
                                    {
                                        dataTable.Select("TP_PRINTTYPE = '" + str2 + "'");
                                        num1 = D.GetDecimal(dataTable.Compute("SUM(AM_SUM)", "TP_PRINTTYPE = '" + str2 + "'"));
                                    }
                                    if (num1 != new Decimal(0))
                                    {
                                        dataRow["L_AM_CUR"] = (D.GetDecimal(dataRow["L_AM_CUR"]) + Math.Abs(num1));
                                        using (Dictionary<int, string[]>.KeyCollection.Enumerator enumerator = this._기간데이터.Keys.GetEnumerator())
                                        {
                                            while (enumerator.MoveNext())
                                            {
                                                int current = enumerator.Current;
                                                Decimal num2 = D.GetDecimal(dataTable.Compute("SUM(AM_SUM)", "TP_PRINTTYPE = '" + str2 + "' AND YM_ACCT >= '" + this._기간데이터[current][1] + "' AND YM_ACCT <='" + this._기간데이터[current][2] + "'"));
                                                dataRow["AM_CUR" + D.GetInt(current)] = (D.GetDecimal(dataRow["AM_CUR" + D.GetInt(current)]) + Math.Abs(num2));
                                            }
                                            break;
                                        }
                                    }
                                    else
                                        break;
                                }
                                else
                                    break;
                            }
                            else
                                break;
                        case 5:
                            dataRow["L_AM_CUR"] = D.GetDecimal(table1.Compute("SUM(OUT_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ")"));
                            using (Dictionary<int, string[]>.KeyCollection.Enumerator enumerator = this._기간데이터.Keys.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    int current = enumerator.Current;
                                    dataRow["AM_CUR" + D.GetInt(current)] = D.GetDecimal(table1.Compute("SUM(OUT_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[current][1] + "' AND YM_ACCT <='" + this._기간데이터[current][2] + "'"));
                                }
                                break;
                            }
                        case 6:
                        case 7:
                            dataRow["L_AM_CUR"] = D.GetDecimal(table1.Compute("SUM(AM_DIF)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ")"));
                            using (Dictionary<int, string[]>.KeyCollection.Enumerator enumerator = this._기간데이터.Keys.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    int current = enumerator.Current;
                                    dataRow["AM_CUR" + D.GetInt(current)] = D.GetDecimal(table1.Compute("SUM(AM_DIF)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[current][1] + "' AND YM_ACCT <='" + this._기간데이터[current][2] + "'"));
                                }
                                break;
                            }
                        case 8:
                            dataRow["L_AM_CUR"] = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ")"));
                            using (Dictionary<int, string[]>.KeyCollection.Enumerator enumerator = this._기간데이터.Keys.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    int current = enumerator.Current;
                                    dataRow["AM_CUR" + D.GetInt(current)] = D.GetDecimal(table1.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT >= '" + this._기간데이터[current][1] + "' AND YM_ACCT <='" + this._기간데이터[current][2] + "'"));
                                }
                                break;
                            }
                        case 10:
                            dataRow["L_AM_CUR"] = D.GetDecimal(table3.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ")"));
                            using (Dictionary<int, string[]>.KeyCollection.Enumerator enumerator = this._기간데이터.Keys.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    int current = enumerator.Current;
                                    dataRow["AM_CUR" + D.GetInt(current)] = D.GetDecimal(table3.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + (D.GetString(row["CD_ACCT_PIPE"]) == string.Empty ? "'X계정없음X'" : D.GetString(row["CD_ACCT_PIPE"])) + ") AND YM_ACCT <='" + this._기간데이터[current][2] + "'"));
                                    dataRow["AM_CUR" + D.GetInt(current)] = (D.GetDecimal(dataRow["AM_CUR" + D.GetInt(current)]) - D.GetDecimal(table2.Compute("SUM(AM_SUM)", "CD_ACCT IN (" + row["CD_ACCT_CHAGAM_PIPE"] + ") AND YM_ACCT <'" + this._기간데이터[current][2] + "'")));
                                }
                                break;
                            }
                    }
                }
                if (D.GetInt(row["TP_GRPCAL"]) == FG_GRPCAL.기타계산.GetHashCode() && table4.Rows.Count != 0)
                    dataRow["STOCK_L_AM_CUR_PIPE"] = (D.GetString(table4.Rows[0]["QT_BAS"]) + "!" + D.GetString(table4.Rows[0]["QT_DIL"]) + "!" + D.GetString(table4.Rows[0]["AM_BAS"]) + "!" + D.GetString(table4.Rows[0]["AM_DIL"]));
                this._dt.Rows.Add(dataRow);
            }

            time.Stop();
        }

        internal void SetModify(DataTable dt)
        {
            if (!(Global.MainFrame.ServerKeyCommon == "CINEMA") && !(Global.MainFrame.ServerKey == "DZSQL") && !(Global.MainFrame.ServerKey == "SQL_108"))
                return;

            Dictionary<string, Decimal> dictionary = new Dictionary<string, Decimal>();
            
            if (Global.MainFrame.ServerKeyCommon == "CINEMA" || Global.MainFrame.ServerKey == "DZSQL" || Global.MainFrame.ServerKey == "SQL_108")
            {
                dictionary["L_AM_CUR"] = D.GetDecimal(dt.Compute("SUM(L_AM_CUR)", "TP_GRPCAL = '" + FG_GRPCAL.그룹.GetHashCode() + "' AND TP_GRP = '" + TP_GRP.매출.GetHashCode() + "'"));
                
                foreach (int key in this._기간데이터.Keys)
                {
                    string index = "AM_CUR" + D.GetInt(key);
                    dictionary[index] = D.GetDecimal(dt.Compute("SUM(" + index + ")", "TP_GRPCAL = '" + FG_GRPCAL.그룹.GetHashCode() + "' AND TP_GRP = '" + TP_GRP.매출.GetHashCode() + "'"));
                }
            }
            
            foreach (DataRow row in dt.Rows)
            {
                if (Global.MainFrame.ServerKeyCommon == "CINEMA" || Global.MainFrame.ServerKey == "DZSQL" || Global.MainFrame.ServerKey == "SQL_108")
                {
                    Decimal val1 = UDecimal.Getdivision(D.GetDecimal(row["L_AM_CUR"]), dictionary["L_AM_CUR"]) * new Decimal(100);
                    row["RT_CUR"] = UDecimal.ConvertToCustom2(val1, 올림구분.반올림, 단위.소수점첫째자리, false);
                    
                    foreach (int key in this._기간데이터.Keys)
                    {
                        string index1 = "AM_CUR" + D.GetInt(key);
                        string index2 = "RT_CUR" + D.GetInt(key);
                        Decimal val2 = UDecimal.Getdivision(D.GetDecimal(row[index1]), dictionary[index1]) * new Decimal(100);
                        row[index2] = UDecimal.ConvertToCustom2(val2, 올림구분.반올림, 단위.소수점첫째자리, false);
                    }
                }
            }
        }
    }
}
