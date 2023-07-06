using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using account;

namespace cz
{
    internal class Setting
    {
        private DataSet _dsData;
        private DataTable _dtAcct;
        private DataTable _dt;
        private Dictionary<string, string> _배부단위;
        private Dictionary<string, string> _배부단위그룹;
        private bool _배부단위표시, _배부단위그룹표시, _증감율표시;

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

        internal Setting(Dictionary<string, string> 관리내역데이터, Dictionary<string, string> 관리내역데이터1, bool 배부단위표시, bool 배부단위그룹표시, bool 증감율표시)
        {
            this._배부단위 = 관리내역데이터;
            this._배부단위그룹 = 관리내역데이터1;
            this._배부단위표시 = 배부단위표시;
            this._배부단위그룹표시 = 배부단위그룹표시;
            this._증감율표시 = 증감율표시;

            this._dt = this.CreateSchema();
        }

        private DataTable CreateSchema()
        {
            DataTable dt = new DataTable();
            
            dt.Columns.Add("CD_PACCT", typeof(string));
            dt.Columns.Add("NM_PACCT", typeof(string));

            dt.Columns.Add("AM_SUM", typeof(decimal));
            dt.Columns.Add("AM_SUM_BEFORE", typeof(decimal));
            dt.Columns.Add("AM_SUM_DIFF", typeof(decimal));
            if (this._증감율표시)
                dt.Columns.Add("AM_SUM_RATE", typeof(decimal));

            if (this._배부단위그룹표시)
            {
                foreach (string key in this._배부단위그룹.Keys)
                {
                    dt.Columns.Add("AM_GRP_" + key, typeof(decimal));
                    dt.Columns.Add("AM_GRP_" + key + "_BEFORE", typeof(decimal));
                    dt.Columns.Add("AM_GRP_" + key + "_DIFF", typeof(decimal));
                    if (this._증감율표시)
                        dt.Columns.Add("AM_GRP_" + key + "_RATE", typeof(decimal));
                }
            }

            if (this._배부단위표시)
            {
                foreach (string key in this._배부단위.Keys)
                {
                    dt.Columns.Add("AM_CUR_" + key, typeof(decimal));
                    dt.Columns.Add("AM_CUR_" + key + "_BEFORE", typeof(decimal));
                    dt.Columns.Add("AM_CUR_" + key + "_DIFF", typeof(decimal));
                    if (this._증감율표시)
                        dt.Columns.Add("AM_CUR_" + key + "_RATE", typeof(decimal));
                }
            }
            
            dt.Columns.Add("AM_ETC", typeof(decimal));
            dt.Columns.Add("AM_ETC_BEFORE", typeof(decimal));
            dt.Columns.Add("AM_ETC_DIFF", typeof(decimal));
            if (this._증감율표시)
                dt.Columns.Add("AM_ETC_RATE", typeof(decimal));

            P_FI_PL_BASE.SetAddBaseCols(dt);
            T.SetDefaultValue(dt);
            
            return dt;
        }

        internal void SetTable()
        {
            DataTable dt1 = this._dsData.Tables[0];
            DataTable dt2 = this._dsData.Tables[1];

            foreach (DataRow row in this._dtAcct.Rows)
            {
                DataRow dataRow = this._dt.NewRow();
                
                dataRow["CD_PACCT"] = row["CD_PACCT"];
                dataRow["NM_PACCT"] = row["NM_PACCT"];

                DataTable dt3 = null;
                DataTable dt4 = null;
                DataTable dt5 = null;
                DataTable dt6 = null;
                DataTable dt7 = null;
                DataTable dt8 = null;

                P_FI_PL_BASE.SetDataBaseCols(dataRow, row);
                
                if (row["CD_ACCT_PIPE"].ToString().Trim() != string.Empty && row["CD_ACCT_PIPE"].ToString().Trim() != "계정없음")
                {
                    if (this._배부단위그룹표시)
                    {
                        dt5 = new DataView(dt2, "CD_ACCT IN (" + row["CD_ACCT_PIPE"] + ") AND YN_GROUP = 'Y'", string.Empty, DataViewRowState.CurrentRows).ToTable();
                        dt5.PrimaryKey = new DataColumn[] { dt5.Columns["CD_ACCT"], dt5.Columns["CD_CODE"] };
                        dt6 = dt5.DefaultView.ToTable(1 != 0, "CD_CODE");
                    }

                    if (this._배부단위표시)
                    {
                        dt3 = new DataView(dt2, "CD_ACCT IN (" + row["CD_ACCT_PIPE"] + ") AND YN_GROUP = 'N'", string.Empty, DataViewRowState.CurrentRows).ToTable();
                        dt3.PrimaryKey = new DataColumn[] { dt3.Columns["CD_ACCT"], dt3.Columns["CD_CODE"] };
                        dt4 = dt3.DefaultView.ToTable(1 != 0, "CD_CODE");
                    }
                }

                if (D.GetInt(row["TP_GRPCAL"]) == FG_GRPCAL.계정.GetHashCode())
                {
                    if (this._배부단위그룹표시)
                    {
                        dt8 = new DataView(dt2, string.Format("CD_ACCT IN ({0}) AND YN_GROUP = 'Y'", row["CD_ACCT_PIPE"]), string.Empty, DataViewRowState.CurrentRows).ToTable();

                        if (dt8.PrimaryKey.Length == 0)
                            dt8.PrimaryKey = new DataColumn[] { dt8.Columns["CD_ACCT"], dt8.Columns["CD_CODE"] };
                    }

                    if (this._배부단위표시)
                    {
                        dt7 = new DataView(dt2, string.Format("CD_ACCT IN ({0}) AND YN_GROUP = 'N'", row["CD_ACCT_PIPE"]), string.Empty, DataViewRowState.CurrentRows).ToTable();

                        if (dt7.PrimaryKey.Length == 0)
                            dt7.PrimaryKey = new DataColumn[] { dt7.Columns["CD_ACCT"], dt7.Columns["CD_CODE"] };
                    }
                    
                    dataRow["AM_SUM"] = D.GetDecimal(dt1.Compute("SUM(AM_AMT)", "CD_ACCT IN (" + row["CD_ACCT_PIPE"] + ")"));
                    dataRow["AM_SUM_BEFORE"] = D.GetDecimal(dt1.Compute("SUM(AM_AMT_BEFORE)", "CD_ACCT IN (" + row["CD_ACCT_PIPE"] + ")"));

                    decimal num = 0, num1 = 0;

                    if (this._배부단위그룹표시)
                    {
                        if (dt8 != null && dt8.Rows.Count > 0)
                        {
                            foreach (string key in this._배부단위그룹.Keys)
                            {
                                dataRow["AM_GRP_" + key] = D.GetDecimal(dt8.Compute("SUM(AM_AMT)", "CD_CODE IN ('" + key + "') AND YN_GROUP = 'Y'"));
                                dataRow["AM_GRP_" + key + "_BEFORE"] = D.GetDecimal(dt8.Compute("SUM(AM_AMT_BEFORE)", "CD_CODE IN ('" + key + "') AND YN_GROUP = 'Y'"));
                            }
                        }
                    }

                    if (this._배부단위표시)
                    {
                        if (dt7 != null && dt7.Rows.Count > 0)
                        {
                            foreach (string key in this._배부단위.Keys)
                            {
                                dataRow["AM_CUR_" + key] = D.GetDecimal(dt7.Compute("SUM(AM_AMT)", "CD_CODE IN ('" + key + "') AND YN_GROUP = 'N'"));
                                num += D.GetDecimal(dataRow["AM_CUR_" + key]);

                                dataRow["AM_CUR_" + key + "_BEFORE"] = D.GetDecimal(dt7.Compute("SUM(AM_AMT_BEFORE)", "CD_CODE IN ('" + key + "') AND YN_GROUP = 'N'"));
                                num1 += D.GetDecimal(dataRow["AM_CUR_" + key + "_BEFORE"]);
                            }
                        }
                    }
                    
                    dataRow["AM_ETC"] = (D.GetDecimal(dataRow["AM_SUM"]) - num);
                    dataRow["AM_ETC_BEFORE"] = (D.GetDecimal(dataRow["AM_SUM_BEFORE"]) - num1);
                }

                if (D.GetInt(row["TP_GRPCAL"]) == FG_GRPCAL.대차계정.GetHashCode())
                {
                    switch (D.GetInt(row["GOOD_CD_GOOD"]))
                    {
                        case 2:
                        case 4:
                        case 8:
                        case 9:
                        case 6:
                        case 7:
                            dataRow["AM_SUM"] = D.GetDecimal(dt1.Compute("SUM(AM_AMT)", "CD_ACCT IN (" + row["CD_ACCT_PIPE"] + ")"));
                            dataRow["AM_SUM_BEFORE"] = D.GetDecimal(dt1.Compute("SUM(AM_AMT_BEFORE)", "CD_ACCT IN (" + row["CD_ACCT_PIPE"] + ")"));

                            decimal num = 0, num1 = 0;

                            if (this._배부단위그룹표시)
                            {
                                if (dt5 != null)
                                {
                                    for (int index = 0; index < dt6.Rows.Count; ++index)
                                    {
                                        if (this._dt.Columns.Contains("AM_GRP_" + dt6.Rows[index]["CD_CODE"].ToString()))
                                            dataRow["AM_GRP_" + dt6.Rows[index]["CD_CODE"].ToString()] = D.GetDecimal(dt5.Compute("SUM(AM_AMT)", "CD_ACCT IN (" + row["CD_ACCT_PIPE"] + ") AND CD_CODE = '" + dt6.Rows[index]["CD_CODE"].ToString() + "' AND YN_GROUP = 'Y'"));

                                        if (this._dt.Columns.Contains("AM_GRP_" + dt6.Rows[index]["CD_CODE"].ToString() + "_BEFORE"))
                                            dataRow["AM_GRP_" + dt6.Rows[index]["CD_CODE"].ToString() + "_BEFORE"] = D.GetDecimal(dt5.Compute("SUM(AM_AMT_BEFORE)", "CD_ACCT IN (" + row["CD_ACCT_PIPE"] + ") AND CD_CODE = '" + dt6.Rows[index]["CD_CODE"].ToString() + "' AND YN_GROUP = 'Y'"));
                                    }
                                }
                            }

                            if (this._배부단위표시)
                            {
                                if (dt3 != null)
                                {
                                    for (int index = 0; index < dt4.Rows.Count; ++index)
                                    {
                                        if (this._dt.Columns.Contains("AM_CUR_" + dt4.Rows[index]["CD_CODE"].ToString()))
                                        {
                                            dataRow["AM_CUR_" + dt4.Rows[index]["CD_CODE"].ToString()] = D.GetDecimal(dt3.Compute("SUM(AM_AMT)", "CD_ACCT IN (" + row["CD_ACCT_PIPE"] + ") AND CD_CODE = '" + dt4.Rows[index]["CD_CODE"].ToString() + "' AND YN_GROUP = 'N'"));
                                            num += D.GetDecimal(dataRow["AM_CUR_" + dt4.Rows[index]["CD_CODE"].ToString()]);
                                        }

                                        if (this._dt.Columns.Contains("AM_CUR_" + dt4.Rows[index]["CD_CODE"].ToString() + "_BEFORE"))
                                        {
                                            dataRow["AM_CUR_" + dt4.Rows[index]["CD_CODE"].ToString() + "_BEFORE"] = D.GetDecimal(dt3.Compute("SUM(AM_AMT_BEFORE)", "CD_ACCT IN (" + row["CD_ACCT_PIPE"] + ") AND CD_CODE = '" + dt4.Rows[index]["CD_CODE"].ToString() + "' AND YN_GROUP = 'N'"));
                                            num1 += D.GetDecimal(dataRow["AM_CUR_" + dt4.Rows[index]["CD_CODE"].ToString() + "_BEFORE"]);
                                        }
                                    }
                                }
                            }
                            
                            dataRow["AM_ETC"] = (D.GetDecimal(dataRow["AM_SUM"]) - num);
                            dataRow["AM_ETC_BEFORE"] = (D.GetDecimal(dataRow["AM_SUM_BEFORE"]) - num1);
                            break;
                    }
                }

                this._dt.Rows.Add(dataRow);
            }
        }

        internal void SetModify(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                row["AM_SUM_DIFF"] = (D.GetDecimal(row["AM_SUM"]) - D.GetDecimal(row["AM_SUM_BEFORE"]));
                if (this._증감율표시)
                    row["AM_SUM_RATE"] = (D.GetDecimal(row["AM_SUM_BEFORE"]) == 0 ? 0 : (D.GetDecimal(row["AM_SUM"]) / D.GetDecimal(row["AM_SUM_BEFORE"])) * 100);

                if (this._배부단위그룹표시)
                {
                    foreach (string key in this._배부단위그룹.Keys)
                    {
                        row["AM_GRP_" + key + "_DIFF"] = (D.GetDecimal(row["AM_GRP_" + key]) - D.GetDecimal(row["AM_GRP_" + key + "_BEFORE"]));
                        if (this._증감율표시)
                            row["AM_GRP_" + key + "_RATE"] = (D.GetDecimal(row["AM_GRP_" + key + "_BEFORE"]) == 0 ? 0 : (D.GetDecimal(row["AM_GRP_" + key]) / D.GetDecimal(row["AM_GRP_" + key + "_BEFORE"])) * 100);
                    }
                }

                if (this._배부단위표시)
                {
                    foreach (string key in this._배부단위.Keys)
                    {
                        row["AM_CUR_" + key + "_DIFF"] = (D.GetDecimal(row["AM_CUR_" + key]) - D.GetDecimal(row["AM_CUR_" + key + "_BEFORE"]));
                        if (this._증감율표시)
                            row["AM_CUR_" + key + "_RATE"] = (D.GetDecimal(row["AM_CUR_" + key + "_BEFORE"]) == 0 ? 0 : (D.GetDecimal(row["AM_CUR_" + key]) / D.GetDecimal(row["AM_CUR_" + key + "_BEFORE"])) * 100);
                    }
                }
                
                row["AM_ETC_DIFF"] = (D.GetDecimal(row["AM_ETC"]) - D.GetDecimal(row["AM_ETC_BEFORE"]));
                if (this._증감율표시)
                    row["AM_ETC_RATE"] = (D.GetDecimal(row["AM_ETC_BEFORE"]) == 0 ? 0 : (D.GetDecimal(row["AM_ETC"]) / D.GetDecimal(row["AM_ETC_BEFORE"])) * 100);
            }
        }

        private string GetCodePipe(List<string> list)
        {
            return string.Join("','", list.ToArray());
        }
    }
}