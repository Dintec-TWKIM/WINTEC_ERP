using System;
using System.Data;
using Duzon.BizOn.Erpu.Security;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.UEncryption;

namespace cz
{
    class P_CZ_FI_BANK_SEND_SUB_BIZ
    {
        private Decimal _dSeq = 0;
        private string 프로시져명 = string.Empty;

        public bool Save(string 출금계좌번호, string 파일작성일자, string 파일순번, string 출금은행, DataTable dt, string 호출구분, string 유저ID, string 이체구분, string 이체명)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            bool flag = true;

            if (dt == null || dt.Rows.Count == 0)
                return false;

            DataTable dataTable = UEncryption.SaveEncryptionTable(dt, new object[] { "ACCT_NO", "TRANS_ACCT_NO" }, new DataType[] { DataType.Account, DataType.Account });

            SpInfo spInfo1 = new SpInfo();
            spInfo1.DataValue = dataTable;
            spInfo1.DataState = DataValueState.Added;
            spInfo1.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo1.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
            spInfo1.SpNameInsert = "SP_CZ_FI_BANK_SEND_SUB_I";
            spInfo1.SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NODE_CODE",
                                                    "TRANS_DATE",
                                                    "TRANS_SEQ",
                                                    "BANK_CODE",
                                                    "ACCT_NO",
                                                    "SEQ",
                                                    "TRANS_BANK_CODE",
                                                    "TRANS_ACCT_NO",
                                                    "TRANS_NAME",
                                                    "CD_EXCH",
                                                    "TRANS_AMT_EX",
                                                    "TRANS_AMT",
                                                    "CLIENT_NOTE",
                                                    "TRANS_NOTE",
                                                    "CUST_CODE",
                                                    "NO_DOCU",
                                                    "ACCT_DATE",
                                                    "DOCU_NO",
                                                    "LINE_NO",
                                                    "ACCT_CODE",
                                                    "AM_EX",
                                                    "AMT",
                                                    "CUST_NAME",
                                                    "TP_CHARGE",
                                                    "TP_SEND_BY",
                                                    "DC_RELATION",
                                                    "ID_INSERT",
                                                    "NO_COMPANY",
                                                    "DC_RMK" };
            spInfoCollection.Add(spInfo1);

            //SpInfo spInfo2 = new SpInfo();
            //spInfo2.SpNameNonQuery = "SP_CZ_FI_BANK_SEND_EX_I";
            //spInfo2.SpParamsNonQuery = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
            //                                          파일작성일자,
            //                                          파일순번 };
            //spInfoCollection.Add(spInfo2);

            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spInfoCollection))
            {
                if (!resultData.Result)
                    flag = false;
            }

            return flag;
        }

        internal bool SaveHD(DataTable dtH, DataTable dtD, string 파일작성일자, string 파일순번, string 유저ID, string 이체구분)
        {
            if ((dtH == null || dtH.Rows.Count == 0) && (dtD == null || dtD.Rows.Count == 0))
                return false;

            SpInfoCollection spInfoCollection = new SpInfoCollection();

            if (dtH != null)
            {
                DataTable dataTable = UEncryption.SaveEncryptionTable(dtH, new object[] { "ACCT_NO", "TRANS_ACCT_NO" }, new DataType[] { DataType.Account, DataType.Account });
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dataTable;
                spInfo.DataState = DataValueState.Added;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "SP_CZ_FI_BANK_SEND_SUBH_I";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NODE_CODE",
                                                       "TRANS_DATE",
                                                       "TRANS_SEQ",
                                                       "BANK_CODE",
                                                       "ACCT_NO",
                                                       "SEQ",
                                                       "TRANS_BANK_CODE",
                                                       "TRANS_ACCT_NO",
                                                       "TRANS_NAME",
                                                       "CD_EXCH",
                                                       "TRANS_AMT_EX",
                                                       "TRANS_AMT",
                                                       "CLIENT_NOTE",
                                                       "TRANS_NOTE",
                                                       "CUST_CODE",
                                                       "CUST_NAME",
                                                       "ID_INSERT",
                                                       "NO_COMPANY",
                                                       "NO_DOCU",
                                                       "LINE_NO",
                                                       "DC_RMK",
                                                       "TP_CHARGE",
                                                       "TP_SEND_BY",
                                                       "DC_RELATION" };
                spInfoCollection.Add(spInfo);
            }

            if (dtD != null)
            {
                DataTable dataTable = UEncryption.SaveEncryptionTable(dtD, new object[] { "ACCT_NO", "TRANS_ACCT_NO" }, new DataType[] { DataType.Account, DataType.Account });
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dataTable;
                spInfo.DataState = DataValueState.Added;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "SP_CZ_FI_BANK_SEND_SUBD_I";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NODE_CODE",
                                                       "TRANS_DATE",
                                                       "TRANS_SEQ",
                                                       "BANK_CODE",
                                                       "ACCT_NO",
                                                       "SEQ",
                                                       "TRANS_BANK_CODE",
                                                       "TRANS_ACCT_NO",
                                                       "TRANS_NAME",
                                                       "CD_EXCH",
                                                       "TRANS_AMT_EX",
                                                       "TRANS_AMT",
                                                       "CLIENT_NOTE",
                                                       "CUST_CODE",
                                                       "CUST_NAME",
                                                       "NO_DOCU",
                                                       "ACCT_DATE",
                                                       "DOCU_NO",
                                                       "LINE_NO",
                                                       "ACCT_CODE",
                                                       "AM_EX",
                                                       "AMT",
                                                       "TP_CHARGE",
                                                       "TP_SEND_BY",
                                                       "DC_RELATION",
                                                       "ID_INSERT" };
                spInfoCollection.Add(spInfo);
            }

            //SpInfo spInfo1 = new SpInfo();
            //spInfo1.SpNameNonQuery = "SP_CZ_FI_BANK_SEND_EX_I";
            //spInfo1.SpParamsNonQuery = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
            //                                          파일작성일자,
            //                                          파일순번 };

            //spInfoCollection.Add(spInfo1);

            return DBHelper.Save(spInfoCollection);
        }

        internal bool SaveLimite(DataTable dt, Decimal am_limite, string 파일작성일자, string 파일순번, string 유저ID, string 이체구분)
        {
            if (dt == null || dt.Rows.Count == 0)
                return false;

            if (!dt.Columns.Contains("YN_LIMITE"))
                dt.Columns.Add("YN_LIMITE", typeof(string));

            if (!dt.Columns.Contains("NO_LIMITE"))
                dt.Columns.Add("NO_LIMITE", typeof(string));

            DataTable dt1 = dt.Clone();
            DataTable dt2 = new DataTable();

            dt2.Columns.Add("CD_PC", typeof(string));
            dt2.Columns.Add("NO_DOCU", typeof(string));
            dt2.Columns.Add("NO_DOLINE", typeof(string));
            dt2.Columns.Add("NO_LIMITE", typeof(string));
            dt2.Columns.Add("AM_PAY", typeof(decimal));
            dt2.Columns.Add("AM_TRPAY", typeof(decimal));

            string str = string.Empty;
            string @string = D.GetString(Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "FI", "38", 파일작성일자));

            foreach (DataRow dr in dt.Rows)
            {
                this.Bank_Limite(dr, @string, ref dt2);
                this.Bank_Sendh(dr, am_limite, @string, ref dt1);

                if (D.GetDecimal(dt2.Compute("SUM(AM_PAY)", "")) != D.GetDecimal(dt1.Compute("SUM(TRANS_AMT)", "")))
                {
                    Global.MainFrame.ShowMessage("이체파일 생성 중 오류가 발생했습니다\n자료 확인 후 이체 파일을 다시 작성해 주세요.");
                    return false;
                }
            }

            SpInfoCollection spInfoCollection = new SpInfoCollection();

            if (dt1 != null)
            {
                DataTable dataTable = UEncryption.SaveEncryptionTable(dt1, new object[] { "ACCT_NO", "TRANS_ACCT_NO" }, new DataType[] { DataType.Account, DataType.Account });
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dataTable;
                spInfo.DataState = DataValueState.Added;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "SP_CZ_FI_BANK_SEND_SUBH_LIMITE_I";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NODE_CODE",
                                                       "TRANS_DATE",
                                                       "TRANS_SEQ",
                                                       "BANK_CODE",
                                                       "ACCT_NO",
                                                       "SEQ",
                                                       "TRANS_BANK_CODE",
                                                       "TRANS_ACCT_NO",
                                                       "TRANS_NAME",
                                                       "CD_EXCH",
                                                       "TRANS_AMT_EX",
                                                       "TRANS_AMT",
                                                       "CLIENT_NOTE",
                                                       "TRANS_NOTE",
                                                       "CUST_CODE",
                                                       "CUST_NAME",
                                                       "ID_INSERT",
                                                       "NO_COMPANY",
                                                       "YN_LIMITE",
                                                       "NO_LIMITE",
                                                       "NO_DOCU",
                                                       "LINE_NO",
                                                       "TP_CHARGE",
                                                       "TP_SEND_BY",
                                                       "DC_RELATION" };
                spInfoCollection.Add(spInfo);
            }

            if (dt2 != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dt2;
                spInfo.DataState = DataValueState.Added;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "SP_CZ_FI_BANK_LIMITEH_INSERT";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "CD_PC",
                                                       "NO_DOCU",
                                                       "NO_DOLINE",
                                                       "NO_LIMITE",
                                                       "AM_PAY",
                                                       "AM_TRPAY",
                                                       "ID_INSERT" };
                spInfoCollection.Add(spInfo);
            }

            //SpInfo spInfo1 = new SpInfo();
            //spInfo1.SpNameNonQuery = "SP_CZ_FI_BANK_SEND_EX_I";
            //spInfo1.SpParamsNonQuery = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
            //                                          파일작성일자,
            //                                          파일순번 };

            //spInfoCollection.Add(spInfo1);

            return DBHelper.Save(spInfoCollection);
        }

        private void Bank_Sendh(DataRow dr, Decimal am_limite, string sNo_limite, ref DataTable dt)
        {
            Decimal @decimal = D.GetDecimal(dr["TRANS_AMT"]);

            if (D.GetDecimal(dr["TRANS_AMT"]) > D.GetDecimal(new 시스템환경설정().Get은행연동환경설정("AM_LIMITE")))
            {
                Decimal d = Decimal.Ceiling(D.GetDecimal(dr["TRANS_AMT"]) / am_limite);

                for (int index = 0; (Decimal)index < d; ++index)
                {
                    dr["YN_LIMITE"] = "Y";
                    dr["NO_LIMITE"] = sNo_limite;

                    int num2 = (index != --d) ? 1 : (!(Decimal.Remainder(@decimal, am_limite) != 0) ? 1 : 0);

                    dr["TRANS_AMT"] = num2 != 0 ? am_limite : Decimal.Remainder(@decimal, am_limite);
                    dr["SEQ"] = ++this._dSeq;
                    dt.ImportRow(dr);
                }
            }
            else
            {
                dr["YN_LIMITE"] = "N";
                dr["NO_LIMITE"] = sNo_limite;
                dr["SEQ"] = ++this._dSeq;
                dt.ImportRow(dr);
            }
        }

        private void Bank_Limite(DataRow dr, string sNo_limite, ref DataTable dt)
        {
            DataRow row = dt.NewRow();

            row["CD_PC"] = dr["NODE_CODE"];
            row["NO_DOCU"] = dr["NO_DOCU"];
            row["NO_DOLINE"] = dr["LINE_NO"];
            row["NO_LIMITE"] = sNo_limite;
            row["AM_PAY"] = dr["TRANS_AMT"];
            row["AM_TRPAY"] = dr["TRANS_AMT"];

            dt.Rows.Add(row);
        }

        private void Bank_SendhGS(DataRow dr, Decimal am_limite, string sNo_limite, ref DataTable dt)
        {
            Decimal @decimal = D.GetDecimal(dr["TRANS_AMT"]);
            if (D.GetString(dr["TRANS_BANK_CODE"]) != "025" && D.GetString(dr["TRANS_BANK_CODE"]) != "033" && D.GetString(dr["TRANS_BANK_CODE"]) != "081" && D.GetString(dr["TRANS_BANK_CODE"]) != "082")
            {
                if (D.GetDecimal(dr["TRANS_AMT"]) > D.GetDecimal(new 시스템환경설정().Get은행연동환경설정("AM_LIMITE")))
                {
                    Decimal d = Decimal.Ceiling(D.GetDecimal(dr["TRANS_AMT"]) / am_limite);
                    for (int index = 0; (Decimal)index < d; ++index)
                    {
                        dr["YN_LIMITE"] = "Y";
                        dr["NO_LIMITE"] = sNo_limite;
                        int num2 = !((Decimal)index == --d) ? 1 : (!(Decimal.Remainder(@decimal, am_limite) != 0) ? 1 : 0);
                        dr["TRANS_AMT"] = num2 != 0 ? am_limite : Decimal.Remainder(@decimal, am_limite);
                        dr["SEQ"] = ++this._dSeq;
                        dt.ImportRow(dr);
                    }
                }
                else
                {
                    dr["YN_LIMITE"] = "N";
                    dr["NO_LIMITE"] = sNo_limite;
                    dr["SEQ"] = ++this._dSeq;
                    dt.ImportRow(dr);
                }
            }
            else
            {
                dr["YN_LIMITE"] = "N";
                dr["NO_LIMITE"] = sNo_limite;
                dr["SEQ"] = ++this._dSeq;
                dt.ImportRow(dr);
            }
        }

        private void Bank_LimiteGS(DataRow dr, string sNo_limite, ref DataTable dt)
        {
            DataRow row = dt.NewRow();
            row["CD_PC"] = dr["NODE_CODE"];
            row["NO_DOCU"] = dr["NO_DOCU"];
            row["NO_DOLINE"] = dr["LINE_NO"];
            row["NO_LIMITE"] = sNo_limite;
            row["AM_PAY"] = dr["TRANS_AMT"];
            row["AM_TRPAY"] = dr["TRANS_AMT"];
            row["TRANS_BANK_CODE"] = dr["TRANS_BANK_CODE"];
            dt.Rows.Add(row);
        }

        public bool Check(string 파일작성일자, string 회계단위, string 파일순번)
        {
            SpInfo spInfo = new SpInfo();
            spInfo.SpNameSelect = "UP_FI_BANK_SEND_SUB_BEFORE_1";
            spInfo.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                   파일작성일자,
                                                   회계단위,
                                                   파일순번 };

            return !(((ResultData)Global.MainFrame.FillDataTable(spInfo)).OutParamsSelect[0, 0].ToString() != "0");
        }

        public string 출금은행명(string 출금은행코드)
        {
            string str = "SELECT NM_SYSDEF FROM MA_CODEDTL WHERE CD_FIELD = 'FI_T000013' AND CD_SYSDEF = '" + 출금은행코드 + "' AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            return Global.MainFrame.FillDataTable(str).Rows[0]["NM_SYSDEF"].ToString().Trim();
        }

        internal Decimal 자동순번(string TRANDS_DATE)
        {
            string str1 = string.Empty;
            string str2;
            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                str2 = "SELECT MAX(CONVERT(INT,TRANS_SEQ)) AS TRANS_SEQ FROM BANK_SENDH WHERE C_CODE = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND TRANS_DATE = '" + TRANDS_DATE + "'";
            else
                str2 = "SELECT MAX(TO_NUMBER(TRANS_SEQ)) AS TRANS_SEQ FROM BANK_SENDH WHERE C_CODE = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND TRANS_DATE = '" + TRANDS_DATE + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(str2);
            if (dataTable != null && dataTable.Rows.Count > 0)
                return D.GetDecimal(dataTable.Rows[0]["TRANS_SEQ"]);
            else
                return 0;
        }
    }
}