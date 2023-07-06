using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU.FI.전표;
using Duzon.ERPU;
using Duzon.ERPU.UEncryption;
using System.Text;
using Duzon.Common.Util;
using System;
using Duzon.BizOn.Erpu.Security;

namespace cz
{
    internal class P_CZ_FI_EPDOCU_BIZ
    {
        private string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
        private string 사용자ID = Global.MainFrame.LoginInfo.UserID;
        private string 페이지ID = Global.MainFrame.CurrentPageID;
        private string _CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;
        private string _EmployeeNo = Global.MainFrame.LoginInfo.EmployeeNo;
        private bool _예산단위필수;
        private bool _사업계획필수;
        private bool _부서연결;
        private string _상대계정;

        internal bool Get예산필수여부
        {
            get
            {
                return this._예산단위필수;
            }
        }

        internal bool Get사업계획필수여부
        {
            get
            {
                return this._사업계획필수;
            }
        }

        public P_CZ_FI_EPDOCU_BIZ()
        {
            this._상대계정 = new EPDOCU().GetTpEpdocuAcct();
            foreach (DataRow dataRow in DBHelper.GetDataTable("UP_FI_ENVD_MULTI_SELECT", new object[] { this._CompanyCode,
                                                                                                        "TP_BUDGETMNG|TP_BUDGETUSE|TP_EPDOCUACCT|TP_EPDOCUJO|" }).Rows)
            {
                switch (D.GetString(dataRow["TP_ENV"]))
                {
                    case "TP_BUDGETUSE":
                        this._예산단위필수 = D.GetString(dataRow["CD_ENV"]) == "Y";
                        break;
                    case "TP_BUDGETMNG":
                        this._사업계획필수 = D.GetString(dataRow["CD_ENV"]) == "1";
                        break;
                    case "TP_EPDOCUJO":
                        this._부서연결 = D.GetString(dataRow["CD_ENV"]) == "Y";
                        break;
                }
            }
        }

        public void CheckPartner(string PaterPipe, out DataTable dt, out bool Result)
        {
            dt = null;
            Result = false;
            DataSet dataSet = DBHelper.GetDataSet("UP_FI_ENVD_PARTNER_S", new object[] { this._CompanyCode,
                                                                                         PaterPipe });

            if (dataSet == null || dataSet.Tables.Count <= 0 || (dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count <= 0 || !(dataSet.Tables[0].Rows[0]["CD_ENV"].ToString() == "Y")) || (dataSet.Tables[1] == null || dataSet.Tables[2] == null))
                return;
            
            dt = dataSet.Tables[2].Clone();
            
            if (dataSet.Tables[3] != null && dataSet.Tables[3].Rows.Count > 0)
            {
                int result1 = 0;
                int result2 = 0;
                string str1 = string.Empty;
                int length1 = dataSet.Tables[3].Rows[0]["CD_PARTNER"].ToString().Length;
                int.TryParse(dataSet.Tables[3].Rows[0]["NUM_PARTNER"].ToString(), out result2);
                int length2 = dataSet.Tables[3].Rows[0]["CD_PARTNER"].ToString().Length - result2;
                string str2 = dataSet.Tables[3].Rows[0]["CD_PARTNER"].ToString().Substring(0, result2);
                int.TryParse(dataSet.Tables[3].Rows[0]["CD_PARTNER"].ToString().Substring(result2, length2), out result1);
                foreach (DataRow dataRow in (InternalDataCollectionBase)dataSet.Tables[1].Rows)
                {
                    if (dataSet.Tables[2].Select("NO_COMPANY = '" + dataRow["NO_BIZ"].ToString().Trim() + "'").Length == 0)
                    {
                        DataRow row = dt.NewRow();
                        row["CD_PARTNER"] = (str2 + result1.ToString("D" + length2.ToString()));
                        row["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                        row["LN_PARTNER"] = dataRow["NM_TR"].ToString();
                        row["SN_PARTNER"] = dataRow["NM_TR"].ToString();
                        row["NM_CEO"] = dataRow["NM_CEO"].ToString();
                        row["NO_COMPANY"] = dataRow["NO_BIZ"].ToString();
                        row["DC_ADS1_H"] = dataRow["ADDR1"].ToString();
                        row["DC_ADS1_D"] = dataRow["ADDR2"].ToString();
                        dt.Rows.Add(row);
                        Result = true;
                        ++result1;
                    }
                }
            }
        }

        public void 거래처_저장(DataTable dt)
        {
            ResultData resultData = (ResultData)Global.MainFrame.Save(new SpInfo()
            {
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                UserID = Global.MainFrame.LoginInfo.UserID,
                DataValue = dt,
                SpNameInsert = "UP_FI_EPDOCU_PARTNER_IN",
                SpParamsInsert = new string[] { "CD_PARTNER",
                                                "CD_COMPANY",
                                                "LN_PARTNER",
                                                "SN_PARTNER",
                                                "NM_CEO",
                                                "NO_COMPANY",
                                                "DC_ADS1_H",
                                                "DC_ADS1_D" }
            });
        }

        public bool CheckBntEnable()
        {
            bool flag = true;
            string query = "SELECT TP_EVIDENCE FROM MA_ENV WHERE CD_COMPANY='" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            if (dataTable != null && dataTable.Rows.Count > 0)
                flag = !(dataTable.Rows[0]["TP_EVIDENCE"].ToString() == "3");
            return flag;
        }

        internal DataTable Search(object[] obj)
        {
            D.GetString(obj[5]);
            string spName = "UP_FI_EPDOCU_S";
            if (this._상대계정 == "1")
                spName = "UP_FI_EPDOCU_S2";
            DataTable dataTable = new UEncryption().SearchDecryptionTable(DBHelper.GetDataTable(spName, obj), new string[] { "NO_DEPOSIT" }, new DataType[] { DataType.Account });
            dataTable.AcceptChanges();
            return dataTable;
        }

        internal DataTable SearchExcel(object[] obj)
        {
            string spName = "UP_FI_EPDOCU_EXCEL_S";
            if (this._상대계정 == "1")
                spName = "UP_FI_EPDOCU_EXCEL_S2";
            DataTable dataTable = DBHelper.GetDataTable(spName, obj);
            int num = 0;
            if (!dataTable.Columns.Contains("IMAGE_PATH_CHECK"))
                dataTable.Columns.Add("IMAGE_PATH_CHECK", typeof(string));
            if (!dataTable.Columns.Contains("IMAGE_PATH_D"))
                dataTable.Columns.Add("IMAGE_PATH_D", typeof(string));
            if (!dataTable.Columns.Contains("NO_GIAN"))
                dataTable.Columns.Add("NO_GIAN", typeof(string));
            if (!dataTable.Columns.Contains("TP_GIAN"))
                dataTable.Columns.Add("TP_GIAN", typeof(string));
            if (!dataTable.Columns.Contains("TP_BUNIT"))
                dataTable.Columns.Add("TP_BUNIT", typeof(string));
            if (!dataTable.Columns.Contains("TP_BMSG"))
                dataTable.Columns.Add("TP_BMSG", typeof(string));
            if (!dataTable.Columns.Contains("FG_EXPN"))
                dataTable.Columns.Add("FG_EXPN", typeof(string));
            foreach (DataRow dr in (InternalDataCollectionBase)dataTable.Rows)
            {
                ++num;
                dr["ROW_ID"] = num.ToString();
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_PARTNER"])))
                    this.Set관리항목(dr, "A06");
                if (!string.IsNullOrEmpty(D.GetString(dr["NO_COMPANY"])))
                    this.Set관리항목(dr, "C01");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_BIZAREA"])))
                    this.Set관리항목(dr, "A01");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_CC"])))
                    this.Set관리항목(dr, "A02");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_DEPT"])))
                    this.Set관리항목(dr, "A03");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_PJT"])))
                    this.Set관리항목(dr, "A05");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_CARD"])))
                    this.Set관리항목(dr, "A08");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_DEPOSIT"])))
                    this.Set관리항목(dr, "A07");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_BANK"])))
                    this.Set관리항목(dr, "A09");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_EMPLOY"])))
                    this.Set관리항목(dr, "A04");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_UMNG1"])))
                    this.Set관리항목(dr, "A21");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_UMNG2"])))
                    this.Set관리항목(dr, "A22");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_UMNG3"])))
                    this.Set관리항목(dr, "A23");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_UMNG4"])))
                    this.Set관리항목(dr, "A24");
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_UMNG5"])))
                    this.Set관리항목(dr, "A25");
                if (!string.IsNullOrEmpty(D.GetString(dr["DT_END"])))
                {
                    this.Set관리항목(dr, "B22");
                    this.Set관리항목(dr, "B23");
                }
                if (!string.IsNullOrEmpty(D.GetString(dr["CD_EXCH"])))
                    this.Set관리항목(dr, "B24");
                if (!string.IsNullOrEmpty(D.GetString(dr["RT_EXCH"])))
                    this.Set관리항목(dr, "B25");
                if (!string.IsNullOrEmpty(D.GetString(dr["AM_EX"])))
                    this.Set관리항목(dr, "B26");
                if (!string.IsNullOrEmpty(D.GetString(dr["DT_START"])))
                    this.Set관리항목(dr, "B21");
                for (int index = 1; index <= 10; ++index)
                {
                    string @string = D.GetString(dr["CD_MNG" + index.ToString()]);
                    switch (@string)
                    {
                        case "A06":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_PARTNER"])))
                            {
                                dr["CD_PARTNER"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["LN_PARTNER"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "C01":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["NO_COMPANY"])))
                            {
                                dr["NO_COMPANY"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A01":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_BIZAREA"])))
                            {
                                dr["CD_BIZAREA"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_BIZAREA"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A02":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_CC"])))
                            {
                                dr["CD_CC"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_CC"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A03":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_DEPT"])))
                            {
                                dr["CD_DEPT"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_DEPT"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A05":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_PJT"])))
                            {
                                dr["CD_PJT"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_PJT"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A08":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_CARD"])))
                            {
                                dr["CD_CARD"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_CARD"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A07":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_DEPOSIT"])))
                            {
                                dr["CD_DEPOSIT"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NO_DEPOSIT"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A09":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_BANK"])))
                            {
                                dr["CD_BANK"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_BANK"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A04":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_EMPLOY"])))
                            {
                                dr["CD_EMPLOY"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_EMP"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A21":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_UMNG1"])))
                            {
                                dr["CD_UMNG1"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_UMNG1"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A22":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_UMNG2"])))
                            {
                                dr["CD_UMNG2"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_UMNG2"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A23":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_UMNG3"])))
                            {
                                dr["CD_UMNG3"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_UMNG3"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A24":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_UMNG4"])))
                            {
                                dr["CD_UMNG4"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_UMNG4"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "A25":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_UMNG5"])))
                            {
                                dr["CD_UMNG5"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_UMNG5"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "B22":
                        case "B23":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["DT_END"])))
                            {
                                dr["DT_END"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "B24":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["CD_EXCH"])))
                            {
                                dr["CD_EXCH"] = D.GetString(dr["CD_MNGD" + index.ToString()]);
                                dr["NM_EXCH"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "B25":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["RT_EXCH"])))
                            {
                                dr["RT_EXCH"] = D.GetDecimal(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "B26":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["AM_EX"])))
                            {
                                dr["AM_EX"] = D.GetDecimal(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                        case "B21":
                            if (!string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(D.GetString(dr["DT_START"])))
                            {
                                dr["DT_START"] = D.GetString(dr["NM_MNGD" + index.ToString()]);
                                break;
                            }
                            break;
                    }
                }
            }
            return dataTable;
        }

        internal void Set관리항목(DataRow dr, string 관리항목코드)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            switch (관리항목코드)
            {
                case "A06":
                    str1 = D.GetString(dr["CD_PARTNER"]);
                    str2 = D.GetString(dr["LN_PARTNER"]);
                    break;
                case "C01":
                    str1 = D.GetString(dr["NO_COMPANY"]);
                    str2 = D.GetString(dr["NO_COMPANY"]);
                    break;
                case "A01":
                    str1 = D.GetString(dr["CD_BIZAREA"]);
                    str2 = D.GetString(dr["NM_BIZAREA"]);
                    break;
                case "A02":
                    str1 = D.GetString(dr["CD_CC"]);
                    str2 = D.GetString(dr["NM_CC"]);
                    break;
                case "A03":
                    str1 = D.GetString(dr["CD_DEPT"]);
                    str2 = D.GetString(dr["NM_DEPT"]);
                    break;
                case "A05":
                    str1 = D.GetString(dr["CD_PJT"]);
                    str2 = D.GetString(dr["NM_PJT"]);
                    break;
                case "A08":
                    str1 = D.GetString(dr["CD_CARD"]);
                    str2 = D.GetString(dr["NM_CARD"]);
                    break;
                case "A07":
                    str1 = D.GetString(dr["CD_DEPOSIT"]);
                    str2 = D.GetString(dr["NO_DEPOSIT"]);
                    break;
                case "A09":
                    str1 = D.GetString(dr["CD_BANK"]);
                    str2 = D.GetString(dr["NM_BANK"]);
                    break;
                case "A04":
                    str1 = D.GetString(dr["CD_EMPLOY"]);
                    str2 = D.GetString(dr["NM_EMP"]);
                    break;
                case "A21":
                    str1 = D.GetString(dr["CD_UMNG1"]);
                    str2 = D.GetString(dr["NM_UMNG1"]);
                    break;
                case "A22":
                    str1 = D.GetString(dr["CD_UMNG2"]);
                    str2 = D.GetString(dr["NM_UMNG2"]);
                    break;
                case "A23":
                    str1 = D.GetString(dr["CD_UMNG3"]);
                    str2 = D.GetString(dr["NM_UMNG3"]);
                    break;
                case "A24":
                    str1 = D.GetString(dr["CD_UMNG4"]);
                    str2 = D.GetString(dr["NM_UMNG4"]);
                    break;
                case "A25":
                    str1 = D.GetString(dr["CD_UMNG5"]);
                    str2 = D.GetString(dr["NM_UMNG5"]);
                    break;
                case "B22":
                case "B23":
                    str2 = D.GetString(dr["DT_END"]);
                    break;
                case "B24":
                    str1 = D.GetString(dr["CD_EXCH"]);
                    str2 = D.GetString(dr["NM_EXCH"]);
                    break;
                case "B25":
                    str2 = D.GetString(dr["RT_EXCH"]);
                    break;
                case "B26":
                    str2 = D.GetString(dr["AM_EX"]);
                    break;
                case "B21":
                    str2 = D.GetString(dr["DT_START"]);
                    break;
            }
            for (int index = 1; index <= 10; ++index)
            {
                if (D.GetString(dr["CD_MNG" + index.ToString()]) == 관리항목코드)
                {
                    dr["CD_MNGD" + index.ToString()] = str1;
                    dr["NM_MNGD" + index.ToString()] = str2;
                    break;
                }
            }
        }

        internal bool Save(DataTable dt, bool 엑셀데이타)
        {
            if (dt == null)
                return true;
            dt.RemotingFormat = SerializationFormat.Binary;
            SpInfo si = new SpInfo();
            if (엑셀데이타)
                si.DataState = DataValueState.Added;
            si.DataValue = dt;
            si.CompanyID = this._CompanyCode;
            si.UserID = this._EmployeeNo;
            si.SpNameInsert = "UP_FI_EPDOCU_I";
            si.SpParamsInsert = new string[] { "CD_COMPANY",
                                               "ROW_ID",
                                               "CD_PC",
                                               "CD_WDEPT",
                                               "ID_WRITE",
                                               "CD_EPCODE",
                                               "DT_ACCT",
                                               "CD_EVID",
                                               "AM_TAXSTD",
                                               "AM_ADDTAX",
                                               "NM_NOTE",
                                               "CD_PARTNER",
                                               "NO_COMPANY",
                                               "CD_BUDGET",
                                               "CD_BIZPLAN",
                                               "CD_BIZAREA",
                                               "CD_CC",
                                               "CD_DEPT",
                                               "CD_PJT",
                                               "CD_CARD",
                                               "CD_DEPOSIT",
                                               "CD_BANK",
                                               "CD_EMPLOY",
                                               "CD_UMNG1",
                                               "CD_UMNG2",
                                               "CD_UMNG3",
                                               "CD_UMNG4",
                                               "CD_UMNG5",
                                               "CD_MNG1",
                                               "CD_MNGD1",
                                               "NM_MNGD1",
                                               "CD_MNG2",
                                               "CD_MNGD2",
                                               "NM_MNGD2",
                                               "CD_MNG3",
                                               "CD_MNGD3",
                                               "NM_MNGD3",
                                               "CD_MNG4",
                                               "CD_MNGD4",
                                               "NM_MNGD4",
                                               "CD_MNG5",
                                               "CD_MNGD5",
                                               "NM_MNGD5",
                                               "CD_MNG6",
                                               "CD_MNGD6",
                                               "NM_MNGD6",
                                               "CD_MNG7",
                                               "CD_MNGD7",
                                               "NM_MNGD7",
                                               "CD_MNG8",
                                               "CD_MNGD8",
                                               "NM_MNGD8",
                                               "CD_MNG9",
                                               "CD_MNGD9",
                                               "NM_MNGD9",
                                               "CD_MNG10",
                                               "CD_MNGD10",
                                               "NM_MNGD10",
                                               "CD_FUND",
                                               "DT_END",
                                               "CD_EXCH",
                                               "RT_EXCH",
                                               "AM_EX",
                                               "DT_START",
                                               "ST_MUTUAL",
                                               "NO_CASH",
                                               "NO_TO",
                                               "DT_SHIPPING",
                                               "MD_TAX1",
                                               "NM_ITEM1",
                                               "NM_SIZE1",
                                               "QT_TAX1",
                                               "AM_PRC1",
                                               "AM_SUPPLY1",
                                               "AM_TAX1",
                                               "NM_NOTE1",
                                               "NO_GIAN",
                                               "FG_EXPN",
                                               "ID_INSERT",
                                               "TP_EVIDENCE" };
            si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT", Global.MainFrame.LoginInfo.UserID);
            si.SpNameUpdate = "UP_FI_EPDOCU_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "ROW_ID",
                                               "CD_PC",
                                               "CD_WDEPT",
                                               "ID_WRITE",
                                               "CD_EPCODE",
                                               "DT_ACCT",
                                               "CD_EVID",
                                               "AM_TAXSTD",
                                               "AM_ADDTAX",
                                               "NM_NOTE",
                                               "CD_PARTNER",
                                               "NO_COMPANY",
                                               "CD_BUDGET",
                                               "CD_BIZPLAN",
                                               "CD_BIZAREA",
                                               "CD_CC",
                                               "CD_DEPT",
                                               "CD_PJT",
                                               "CD_CARD",
                                               "CD_DEPOSIT",
                                               "CD_BANK",
                                               "CD_EMPLOY",
                                               "CD_UMNG1",
                                               "CD_UMNG2",
                                               "CD_UMNG3",
                                               "CD_UMNG4",
                                               "CD_UMNG5",
                                               "CD_MNGD1",
                                               "NM_MNGD1",
                                               "CD_MNGD2",
                                               "NM_MNGD2",
                                               "CD_MNGD3",
                                               "NM_MNGD3",
                                               "CD_MNGD4",
                                               "NM_MNGD4",
                                               "CD_MNGD5",
                                               "NM_MNGD5",
                                               "CD_MNGD6",
                                               "NM_MNGD6",
                                               "CD_MNGD7",
                                               "NM_MNGD7",
                                               "CD_MNGD8",
                                               "NM_MNGD8",
                                               "CD_MNGD9",
                                               "NM_MNGD9",
                                               "CD_MNGD10",
                                               "NM_MNGD10",
                                               "CD_FUND",
                                               "DT_END",
                                               "CD_EXCH",
                                               "RT_EXCH",
                                               "AM_EX",
                                               "DT_START",
                                               "ST_MUTUAL",
                                               "NO_CASH",
                                               "NO_TO",
                                               "DT_SHIPPING",
                                               "MD_TAX1",
                                               "NM_ITEM1",
                                               "NM_SIZE1",
                                               "QT_TAX1",
                                               "AM_PRC1",
                                               "AM_SUPPLY1",
                                               "AM_TAX1",
                                               "NM_NOTE1",
                                               "NO_GIAN",
                                               "FG_EXPN",
                                               "ID_UPDATE",
                                               "TP_EVIDENCE" };
            si.SpParamsValues.Add(ActionState.Update, "ID_UPDATE", Global.MainFrame.LoginInfo.UserID);
            si.SpNameDelete = "UP_FI_EPDOCU_D";
            si.SpParamsDelete = new string[] { "CD_COMPANY",
                                               "ROW_ID",
                                               "NO_GIAN" };
            return DBHelper.Save(si);
        }

        internal bool Save_Excel(DataTable dt)
        {
            if (dt == null)
                return true;
            dt.RemotingFormat = SerializationFormat.Binary;
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.DataState = DataValueState.Added;
            si.CompanyID = this._CompanyCode;
            si.UserID = this._EmployeeNo;
            si.SpNameInsert = "UP_FI_EPDOCU_EXCEL_I";
            if (!dt.Columns.Contains("CD_DEPOSIT"))
                dt.Columns.Add("CD_DEPOSIT");
            si.SpParamsInsert = new string[] { "CD_COMPANY",
                                               "CD_EPCODE",
                                               "DT_ACCT",
                                               "CD_EVID",
                                               "AM_TAXSTD",
                                               "AM_ADDTAX",
                                               "NM_NOTE",
                                               "CD_PARTNER",
                                               "NO_COMPANY",
                                               "CD_BUDGET",
                                               "CD_BIZPLAN",
                                               "CD_BIZAREA",
                                               "CD_CC",
                                               "CD_DEPT",
                                               "CD_PJT",
                                               "CD_CARD",
                                               "CD_DEPOSIT",
                                               "CD_BANK",
                                               "CD_EMPLOY",
                                               "CD_UMNG1",
                                               "CD_UMNG2",
                                               "CD_UMNG3",
                                               "CD_UMNG4",
                                               "CD_UMNG5",
                                               "CD_MNGD1",
                                               "NM_MNGD1",
                                               "CD_MNGD2",
                                               "NM_MNGD2",
                                               "CD_MNGD3",
                                               "NM_MNGD3",
                                               "CD_MNGD4",
                                               "NM_MNGD4",
                                               "CD_MNGD5",
                                               "NM_MNGD5",
                                               "CD_MNGD6",
                                               "NM_MNGD6",
                                               "CD_MNGD7",
                                               "NM_MNGD7",
                                               "CD_MNGD8",
                                               "NM_MNGD8",
                                               "CD_MNGD9",
                                               "NM_MNGD9",
                                               "CD_MNGD10",
                                               "NM_MNGD10",
                                               "CD_FUND",
                                               "DT_END",
                                               "CD_EXCH",
                                               "RT_EXCH",
                                               "AM_EX",
                                               "DT_START",
                                               "ST_MUTUAL",
                                               "NO_CASH",
                                               "NO_TO",
                                               "DT_SHIPPING",
                                               "MD_TAX1",
                                               "NM_ITEM1",
                                               "NM_SIZE1",
                                               "QT_TAX1",
                                               "AM_PRC1",
                                               "AM_SUPPLY1",
                                               "AM_TAX1",
                                               "NM_NOTE1",
                                               "TP_EVIDENCE" };
            if (DBHelper.ExecuteNonQuery("UP_FI_EPDOCU_EXCEL_D", new object[] { this._CompanyCode }))
                return DBHelper.Save(si);

            return false;
        }

        internal bool 전표처리(string key, bool 전표승인, string 회계일자, string 작성일자, out object[] result)
        {
            string str = "1";
            
            if (전표승인)
                str = "2";
            
            string spName = "UP_FI_EPDOCU_DOCU_I";
            
            if (this._상대계정 == "1")
                spName = "UP_FI_EPDOCU_DOCU_I2";
            
            DBHelper.GetDataTable(spName, new object[]{ this._CompanyCode,
                                                        key,
                                                        this._EmployeeNo,
                                                        str,
                                                        회계일자,
                                                        작성일자 }, out result);

            return result != null && !(result[0].ToString().Trim() == "");
        }

        internal bool 전표취소(string 회계단위, string 전표번호)
        {
            return DBHelper.ExecuteNonQuery("UP_FI_EPDOCU_DOCU_D", new object[] { this._CompanyCode,
                                                                                  회계단위,
                                                                                  전표번호,
                                                                                  this._EmployeeNo,
                                                                                  this.페이지ID });
        }

        public void ImageUpdate(string NO_DOCU, string CD_PC, string CD_ACCT, string NO_GIAN, string 구분)
        {
            bool flag;
            if (구분 == "D")
            {
                flag = DBHelper.ExecuteNonQuery("UP_FI_EPDOCU_IMAGE_UP", new object[] {this._CompanyCode,
                                                                                       NO_DOCU,
                                                                                       0,
                                                                                       CD_PC,
                                                                                       NO_GIAN,
                                                                                       구분 });
            }
            else
            {
                if (!(구분 == "Y")) return;

                flag = DBHelper.ExecuteNonQuery("UP_FI_EPDOCU_IMAGE_UP", new object[] { this._CompanyCode,
                                                                                        "",
                                                                                        0,
                                                                                        "",
                                                                                        NO_GIAN,
                                                                                        구분 });
            }
        }

        internal bool 예산조회(string 예산단위, string 사업계획, string 예산계정, string 년월, Decimal 공급가액, Decimal 부가세, string 예산통제, string 예산통제보기, string 공급가액계정, out string ErrCode, out string ErrMsg)
        {
            Decimal 집행신청 = 공급가액;
            ErrMsg = "";
            ErrCode = "";
            if (!this._예산단위필수)
                return true;
            if (!(D.GetDecimal(DBHelper.GetDataTable(" SELECT COUNT(*) FROM FI_ACCTCODE A, FI_BGACCTJO B WHERE A.CD_ACCT = B.CD_ACCT  AND A.CD_COMPANY = B.CD_COMPANY AND A.CD_COMPANY = '" + this._CompanyCode + "' AND A.CD_ACCT = '" + 공급가액계정 + "'").Rows[0][0]) > new Decimal(0)))
                return true;
            bool flag1 = true;
            bool flag2 = false;
            if (flag1 && !this.Get예산필수여부)
            {
                if (예산통제 == "4")
                    flag2 = true;
            }
            else if (flag1 && this.Get예산필수여부 && (예산통제 == "2" || 예산통제 == "3" || 예산통제 == "4"))
                flag2 = true;
            if (flag2)
            {
                if (예산단위 == null || 예산단위 == string.Empty || 예산단위 == "")
                {
                    ErrCode = "예산단위";
                    ErrMsg = "예산 단위가 누락 되었습니다.";
                    return false;
                }
                if (예산계정 == null || 예산계정 == string.Empty || 예산계정 == "")
                {
                    ErrCode = "예산계정";
                    ErrMsg = "예산계정이 누락 되었습니다.";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(예산통제) || string.IsNullOrEmpty(예산단위) || string.IsNullOrEmpty(예산계정))
                return true;
            DataTable dataTable = this.Get예산단위정보(예산단위);
            if (dataTable == null || dataTable.Rows.Count < 1 || D.GetString(dataTable.Rows[0]["TP_CONTROL"]) == "3")
                return true;
            object[] outParameters = (object[])null;
            Decimal 집행실적;
            Decimal 실행예산 = 집행실적 = new Decimal(0);
            DBHelper.GetDataTable("UP_FI_ABUDGET_CHECK", new object[] { this._CompanyCode,
                                                                        예산단위,
                                                                        사업계획,
                                                                        예산계정,
                                                                        년월,
                                                                        "N" }, out outParameters);
            if (outParameters != null && outParameters.Length > 0)
            {
                실행예산 = D.GetDecimal(outParameters[0]);
                집행실적 = D.GetDecimal(outParameters[1]);
            }
            if (!(예산통제 == "3") && !(예산통제 == "4") || 예산통제보기 == "2" && 실행예산 - 집행실적 - 집행신청 >= new Decimal(0) || !(예산통제보기 != "3"))
                return true;
            new P_CZ_FI_EPDOCU_BUDGET(실행예산, 집행실적, 집행신청).ShowDialog();
            return !(예산통제 == "4") || 실행예산 - 집행실적 - 집행신청 >= new Decimal(0);
        }

        public bool 연결계정체크(string 조회옵션, string 예산단위, string 사업계획, string 예산계정, string 회계계정)
        {
            string sql = string.Empty;
            switch (조회옵션)
            {
                case "0":
                    sql = "SELECT COUNT(*) FROM FI_BUDGETACCTJO A JOIN FI_BGACCTJO B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_BGACCT = B.CD_BGACCT WHERE A.CD_COMPANY = '" + this.회사코드 + "' AND A.CD_BUDGET = '" + 예산단위 + "' AND A.CD_BGACCT = '" + 예산계정 + "' AND B.CD_ACCT = '" + 회계계정 + "'";
                    break;
                case "1":
                    sql = "SELECT COUNT(*) FROM FI_BIZCODEJO2 A JOIN FI_BGACCTJO B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_BGACCT = B.CD_BGACCT WHERE A.CD_COMPANY = '" + this.회사코드 + "' AND A.CD_BIZPLAN = '" + 사업계획 + "' AND A.CD_BGACCT = '" + 예산계정 + "' AND B.CD_ACCT = '" + 회계계정 + "'";
                    break;
            }
            return D.GetDecimal(DBHelper.GetDataTable(sql).Rows[0][0]) > new Decimal(0);
        }

        public DataTable 연결예산계정(string 회계계정, string 예산단위, string 사업계획)
        {
            object[] parameters = new object[] { this.회사코드,
                                                 회계계정,
                                                 예산단위,
                                                 사업계획,
                                                 0 };
            object[] outParameters = (object[])null;
            return DBHelper.GetDataTable("UP_FI_DOCU_SELECT_BGACCT", parameters, out outParameters);
        }

        public DataTable Get예산단위정보(string 예산단위코드)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" SELECT  YN_USE, TP_CONTROL FROM FI_BGCODE");
            stringBuilder.Append(" WHERE   CD_COMPANY = '" + MA.Login.회사코드 + "' AND CD_BUDGET = '" + 예산단위코드 + "'");
            DataTable dataTable = DBHelper.GetDataTable(stringBuilder.ToString());
            if (dataTable.Rows.Count == 0)
                return (DataTable)null;
            return dataTable;
        }

        internal DataTable Edit_Search(string 결의코드, string 증빙유형)
        {
            string spName = "UP_FI_EPDOCU_EDIT";
            if (this._상대계정 == "1")
                spName = "UP_FI_EPDOCU_EDIT2";
            return DBHelper.GetDataTable(spName, new object[] { this._CompanyCode,
                                                                결의코드,
                                                                증빙유형 });
        }

        public DataTable 증빙조회(string 처리여부, string 권한, string form, string to, string 회계단위, string 사번, string 부서)
        {
            if (권한 == "T")
                return DBHelper.GetDataTable("UP_FI_EPDOCU_IMAGE_S", new object[] { this._CompanyCode,
                                                                                    처리여부,
                                                                                    form,
                                                                                    to,
                                                                                    "",
                                                                                    "",
                                                                                    "" });
            if (권한 == "P")
                return DBHelper.GetDataTable("UP_FI_EPDOCU_IMAGE_S", new object[] { this._CompanyCode,
                                                                                    처리여부,
                                                                                    form,
                                                                                    to,
                                                                                    "",
                                                                                    "",
                                                                                    "" });
            if (권한 == "D")
                return DBHelper.GetDataTable("UP_FI_EPDOCU_IMAGE_S", new object[] { this._CompanyCode,
                                                                                    처리여부,
                                                                                    form,
                                                                                    to,
                                                                                    "",
                                                                                    부서,
                                                                                    "" });
            if (권한 == "A")
                return DBHelper.GetDataTable("UP_FI_EPDOCU_IMAGE_S", new object[] { this._CompanyCode,
                                                                                    처리여부,
                                                                                    form,
                                                                                    to,
                                                                                    "",
                                                                                    "",
                                                                                    "" });
            return DBHelper.GetDataTable("UP_FI_EPDOCU_IMAGE_S", new object[] { this._CompanyCode,
                                                                                처리여부,
                                                                                form,
                                                                                to,
                                                                                "",
                                                                                "",
                                                                                사번 });
        }

        internal string 순번()
        {
            return (string)Global.MainFrame.GetSeq(this._CompanyCode, "FI", "36", Global.MainFrame.GetStringToday.Substring(0, 6));
        }

        public DataTable ImagesSetDataMap()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CODE");
            dataTable.Columns.Add("NAME");
            DataRow row1 = dataTable.NewRow();
            row1["CODE"] = "Y";
            row1["NAME"] = "[보기]";
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["CODE"] = "N";
            row2["NAME"] = "";
            dataTable.Rows.Add(row2);
            return dataTable;
        }

        internal string Get_환경설정_TP_EXPENSE()
        {
            string str = "0";
            string query = @"SELECT CD_ENV
                             FROM MA_ENVD WITH(NOLOCK)
                             WHERE CD_COMPANY='" + Global.MainFrame.LoginInfo.CompanyCode + "' AND TP_ENV = 'TP_EXPENSE' ";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            if (dataTable != null && dataTable.Rows.Count > 0)
                str = dataTable.Rows[0]["CD_ENV"].ToString();
            return str;
        }

        internal string Get_환경설정_TP_VATINPUT()
        {
            string str = "0";
            string query = @"SELECT CD_ENV 
                             FROM MA_ENVD WITH(NOLOCK)
                             WHERE CD_COMPANY='" + Global.MainFrame.LoginInfo.CompanyCode + "' AND TP_ENV = 'TP_VATINPUT' ";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);
            if (dataTable != null && dataTable.Rows.Count > 0)
                str = dataTable.Rows[0]["CD_ENV"].ToString();
            return str;
        }

        internal void Set_환경설정_TP_EXPENSE(string chk)
        {
            DBHelper.GetDataTable("UPDATE MA_ENVD SET CD_ENV = '" + chk + "' " + "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND TP_ENV = 'TP_EXPENSE' ");
        }

        public DataTable 중국버전_부가세율()
        {
            string str = string.Empty;
            return DBHelper.GetDataTable(@"SELECT CD_SYSDEF,
                                                  CD_FLAG1
                                           FROM MA_CODEDTL WITH(NOLOCK)
                                           WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND CD_FIELD = 'MA_B000046'");
        }

        public DataTable 결의사원_추가정보(string s결의사원)
        {
            string str = string.Empty;
            return DBHelper.GetDataTable(@"SELECT NM_SYSDEF
                                           FROM MA_CODEDTL WITH(NOLOCK)
                                           WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                         @"AND CD_FIELD = 'HR_H000002' 
                                           AND CD_SYSDEF IN " + @"(SELECT CD_DUTY_RANK 
                                                                   FROM MA_EMP WITH(NOLOCK)
                                                                   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND NO_EMP = '" + s결의사원 + "')");
        }
    }
}