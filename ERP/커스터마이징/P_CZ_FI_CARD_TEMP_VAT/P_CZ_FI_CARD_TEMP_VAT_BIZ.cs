using Duzon.BizOn.Erpu.Security;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;
using System.Text;

namespace cz
{
	internal class P_CZ_FI_CARD_TEMP_VAT_BIZ
	{
        private string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
        private string 로그인 = Global.MainFrame.LoginInfo.UserID;
        private string 사원번호 = Global.MainFrame.LoginInfo.EmployeeNo;
        private string 서버키 = Global.MainFrame.ServerKeyCommon.ToUpper();

        internal DataTable Search(object[] Params)
        {
            DataSet ds;

            if (MA.ServerKey(false, "DOOSANFEED", "KFC"))
                ds = DBHelper.GetDataSet("UP_FI_Z_CARD_TEMP_VAT_DOOSAN", Params);
            else
                ds = !MA.ServerKey(false, "SOLIDTECH", "SOLIDTECH1") ? DBHelper.GetDataSet("SP_CZ_FI_CARD_TEMP_VAT_S", Params) : DBHelper.GetDataSet("UP_FI_Z_CARD_TEMP_VAT_SOLIDTECH", Params);

            DataTable dataTable = ds.Tables[0].Clone();
            Duzon.ERPU.UEncryption.UEncryption.SearchDecryptionTable(ds.Tables[0], new object[] { "ACCT_NO", "NO_CARD_G", "NO_CARD" }, new DataType[] { DataType.Credit, DataType.Credit, DataType.Credit });
            Duzon.ERPU.UEncryption.UEncryption.SearchDecryptionTable(ds.Tables[1], new object[] { "NO_CARD" }, new DataType[] { DataType.Credit });
            Duzon.ERPU.UEncryption.UEncryption.SearchDecryptionTable(ds.Tables[2], new object[] { "NO_CARD" }, new DataType[] { DataType.Credit });
            T.SetDefaultValue(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (MA.ServerKey(true, "CBS", "119"))
                {
                    DataTable recardCbs = this.Get_RECARD_CBS(Params);
                    recardCbs.TableName = "dtCBS";
                    ds.Tables.Add(recardCbs);
                }

                CreateDataClass createDataClass = new CreateDataClass(ds);
                
                if (!createDataClass.binding())
                    return null;

                dataTable = createDataClass.CreateData();
                dataTable.AcceptChanges();
            }

            return dataTable;
        }

        internal bool Save(DataTable dt)
        {
            DataTable dataTable = Duzon.ERPU.UEncryption.UEncryption.SaveEncryptionTable(dt, new object[] { "ACCT_NO" }, new DataType[] { DataType.Account });
            SpInfo si = new SpInfo();
            si.DataValue = dataTable;
            si.CompanyID = MA.Login.회사코드;
            si.UserID = MA.Login.사용자아이디;

            if (MA.ServerKey(false, "NICEIT"))
            {
                si.SpNameUpdate = "UP_FI_Z_NICEIT1_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "NM_NOTE" };
            }
            else if (MA.ServerKey(false, "MNS"))
            {
                si.SpNameUpdate = "UP_FI_Z_MNS_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "CD_CC",
                                                   "NM_NOTE" };
            }
            else if (MA.ServerKey(false, "DKTE"))
            {
                si.SpNameUpdate = "UP_FI_Z_DKTE_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_USERDEF1",
                                                   "CD_USERDEF2",
                                                   "CD_COSTACCT" };
            }
            else if (MA.ServerKey(false, "KORAIL"))
            {
                si.SpNameUpdate = "UP_FI_Z_KORAIL_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "CD_USERDEF1",
                                                   "NM_NOTE" };
            }
            else if (MA.ServerKey(false, "MABIK"))
            {
                si.SpNameDelete = "UP_FI_Z_MABIK_CARDTEMP_D";
                si.SpParamsDelete = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ" };
            }
            else if (MA.ServerKey(false, "SSCARD"))
            {
                si.SpNameUpdate = "UP_FI_Z_SSCARD_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "NM_NOTE" };
            }
            else if (MA.ServerKey(false, "MTCN"))
            {
                si.SpNameUpdate = "UP_FI_Z_MTCN_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "CD_CC" };
            }
            else if (MA.ServerKey(false, "POINTI"))
            {
                si.SpNameUpdate = "UP_FI_Z_POINTI_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "CD_CC",
                                                   "NM_NOTE",
                                                   "CD_PJT" };
            }
            else if (MA.ServerKey(false, "ATECS"))
            {
                si.SpNameUpdate = "UP_FI_Z_ATEC_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "CD_TPACCT",
                                                   "CD_CC",
                                                   "NM_NOTE" };
            }
            else if (MA.ServerKey(false, "SEMICS"))
            {
                si.SpNameUpdate = "UP_FI_Z_SEMICS_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "CD_TPACCT",
                                                   "CD_CC",
                                                   "NM_NOTE" };
            }
            else if (MA.ServerKey(false, "CROEN"))
            {
                si.SpNameUpdate = "UP_FI_Z_CROEN_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "CD_TPACCT",
                                                   "CD_CC",
                                                   "CD_PJT",
                                                   "NM_NOTE" };
            }
            else if (MA.ServerKey(false, "SJMT"))
            {
                si.SpNameUpdate = "UP_FI_Z_SJMT_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "CD_TPACCT",
                                                   "CD_CC",
                                                   "CD_PJT",
                                                   "NM_NOTE" };
            }
            else if (MA.ServerKey(false, "NASM"))
            {
                si.SpNameUpdate = "UP_FI_Z_NASM_CARDTEMP_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "CD_TPACCT",
                                                   "CD_CC",
                                                   "CD_PJT",
                                                   "EXP_DATE" };
            }
            else
            {
                si.SpNameUpdate = "UP_FI_CARD_TEMP_COSTACCT_U";
                si.SpParamsUpdate = new string[] { "ACCT_NO",
                                                   "BANK_CODE",
                                                   "TRADE_DATE",
                                                   "TRADE_TIME",
                                                   "SEQ",
                                                   "CD_COSTACCT",
                                                   "CD_TPACCT",
                                                   "CD_CC",
                                                   "CD_PJT" };
            }

            return DBHelper.Save(si);
        }

        internal Decimal Get미등록거래처건수(DataRow[] dr부가세처리)
        {
            int num = 0;
            foreach (string pipe in D.StringConvert.GetPipes(dr부가세처리, 300, "S_IDNO"))
            {
                ResultData resultData = (ResultData)Global.MainFrame.FillDataTable(new SpInfo()
                {
                    SpNameSelect = "UP_FI_CARD_TEMP_VAT_S1",
                    SpParamsSelect = new object[] { this.회사코드, pipe }
                });
                num += D.GetInt(resultData.OutParamsSelect[0, 0]);
            }
            return num;
        }

        internal void 전표취소(string 회계단위, string 전표번호)
        {
            DBHelper.ExecuteNonQuery("UP_FI_CARD_TEMP_VAT_S5", new object[] { this.회사코드,
                                                                              회계단위,
                                                                              전표번호,
                                                                              MA.Login.사용자아이디,
                                                                              this.로그인,
                                                                              Global.MainFrame.CurrentPageID });
        }

        internal void 임의전표처리(string ACCT_NO, string BANK_CODE, string TRADE_DATE, string TRADE_TIME, string SEQ, string DOCU_STAT)
        {
            DBHelper.ExecuteNonQuery("UP_FI_CARD_TEMP_VAT_U3", new object[] { Duzon.ERPU.UEncryption.UEncryption.CreditEncryption(ACCT_NO),
                                                                              BANK_CODE,
                                                                              TRADE_DATE,
                                                                              TRADE_TIME,
                                                                              SEQ,
                                                                              DOCU_STAT,
                                                                              this.로그인 });
        }

        internal DataTable 연동과목(string 상대예산계정)
        {
            return DBHelper.GetDataTable("UP_FI_CARD_TEMP_VAT_S6", new object[] { this.회사코드, 상대예산계정, Global.MainFrame.GetStringYearMonth.Substring(0, 4) });
        }

        internal void 부가세처리미처리(object ACCT_NO, object BANK_CODE, object TRADE_DATE, object TRADE_TIME, object SEQ, string 부가세처리옵션, string 봉사료여부)
        {
            DBHelper.GetDataTable("UP_FI_CARD_TEMP_VAT_U", new object[] { this.회사코드,
                                                                          Duzon.ERPU.UEncryption.UEncryption.CreditEncryption(ACCT_NO.ToString()),
                                                                          BANK_CODE,
                                                                          TRADE_DATE,
                                                                          TRADE_TIME,
                                                                          SEQ,
                                                                          부가세처리옵션,
                                                                          봉사료여부 });
        }

        internal void GetDataSetting(string val1, string val2, string 세팅구분, out string 코드, out string 명)
        {
            object[] outParameters;
            DBHelper.GetDataTable("UP_FI_CARD_TEMP_VAT_SETTING", new object[] { this.회사코드,
                                                                                val1,
                                                                                val2,
                                                                                세팅구분 }, out outParameters);
            코드 = D.GetString(outParameters[0]);
            명 = D.GetString(outParameters[1]);
        }

        internal string GetSettingNmacct(string cd_acct)
        {
            string empty = string.Empty;
            DataTable dataTable = DBHelper.GetDataTable("SELECT NM_ACCT FROM   FI_ACCTCODE WHERE  CD_COMPANY = '" + this.회사코드 + "' AND CD_ACCT = '" + cd_acct + "'");

            if (dataTable != null && dataTable.Rows.Count != 0)
                empty = D.GetString(dataTable.Rows[0]["NM_ACCT"]);
            
            return empty;
        }

        internal DataSet GetButtonSet()
        {
            return DBHelper.GetDataSet("UP_FI_CARD_TEMP_BUTTON", new object[] { this.회사코드,
                                                                                this.로그인 });
        }

        internal Enum.상대계정전표처리설정 Get_CARDDOCU()
        {
            string query = " SELECT TP_CARDDOCU FROM BANK_SYS WHERE C_CODE = '" + MA.Login.회사코드 + "' AND USE_YN = '1' ";
            DataTable dataTable = Global.MainFrame.FillDataTable(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return Enum.상대계정전표처리설정.계좌번호연결사용;
            
            switch (D.GetString(dataTable.Rows[0]["TP_CARDDOCU"]))
            {
                case "1":
                    return Enum.상대계정전표처리설정.계좌번호연결사용;
                case "2":
                    return Enum.상대계정전표처리설정.상대계정처리유형사용;
                case "3":
                    return Enum.상대계정전표처리설정.비용계정사용;
                default:
                    return Enum.상대계정전표처리설정.계좌번호연결사용;
            }
        }

        internal DataTable AcctNoCodeCopy()
        {
            DataTable dataTable = DBHelper.GetDataTable("SELECT 'N' S, MAX(A.ACCT_NO) ACCT_NO FROM   CARD_TEMP A JOIN\tACCT_LINK B ON\t\tA.ACCT_NO = B.ACCT_NO WHERE\tA.C_CODE = '****'");
            Duzon.ERPU.UEncryption.UEncryption.SearchDecryptionTable(dataTable, new object[] { "ACCT_NO" }, new DataType[] { DataType.Credit });
            return dataTable;
        }

        internal bool AcctNOChageCompany(string AcctNO_Pipe)
        {
            return DBHelper.ExecuteNonQuery("UP_FI_CARD_TEMP_VAT_ACCTNO", new object[] { this.회사코드,
                                                                                         Duzon.ERPU.UEncryption.UEncryption.CreditEncryption(AcctNO_Pipe) });
        }

        internal DataTable GetCodeData(string CD_FIELD)
        {
            return DBHelper.GetDataTable("SELECT NM_SYSDEF, NM_SYSDEF_E FROM MA_CODEDTL WHERE CD_COMPANY = '" + this.회사코드 + "' AND CD_FIELD = '" + CD_FIELD + "' AND USE_YN = 'Y'");
        }

        internal bool DataDelete(DataTable dt)
        {
            return DBHelper.Save(new SpInfo()
            {
                DataValue = dt,
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                DataState = DataValueState.Deleted,
                SpNameDelete = "UP_FI_Z_CARD_TEMP_KBM_D",
                SpParamsDelete = new string[] { "ACCT_NO",
                                                "BANK_CODE",
                                                "TRADE_DATE",
                                                "TRADE_TIME",
                                                "SEQ" }
            });
        }

        internal string Get거래처(object 사업자등록번호)
        {
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT CD_PARTNER 
                                                          FROM MA_PARTNER WITH(NOLOCK)
                                                          WHERE CD_COMPANY = '" + this.회사코드 + "'AND NO_COMPANY = '" + 사업자등록번호 + "'");
            return dataTable.Rows.Count == 0 ? string.Empty : dataTable.Rows[0][0].ToString();
        }

        internal DataTable 결의서처리여부(object[] obj)
        {
            return DBHelper.GetDataTable(@"SELECT T.CD_USERDEF1, 
                                                  SL.SINGO_TYPE
                                           FROM CARD_TEMP T WITH(NOLOCK)
                                           LEFT JOIN FI_Z_SOLIDTECH_PARTTYPE SL WITH(NOLOCK) ON T.C_CODE = SL.CD_COMPANY AND T.S_IDNO = SL.NO_COMPANY
                                           WHERE T.ACCT_NO = '" + obj[0] + "' AND    T.BANK_CODE = '" + obj[1] + "' AND    T.TRADE_DATE = '" + obj[2] + "' AND    T.TRADE_TIME = '" + obj[3] + "' AND    T.SEQ = '" + obj[4] + "'");
        }

        internal DataTable GetHoliday()
        {
            return DBHelper.GetDataTable(@"SELECT DT_DATE, TP_DATE, TP_WEEK
                                           FROM FI_CALENDARDAY 
                                           WHERE CD_COMPANY = '" + this.회사코드 + "' AND TP_DATE = '2'");
        }

        internal bool GetMG신용정보삭제권한(string ST_DOCU)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" SELECT  ST_DOCUAPP FROM FI_Z_MGINFO_GRANT_DOCU");
            stringBuilder.Append(" WHERE   CD_COMPANY = '" + MA.Login.회사코드 + "' AND ID_USER = '" + MA.Login.사용자아이디 + "'");
            DataTable dataTable = DBHelper.GetDataTable(stringBuilder.ToString());
            
            if (dataTable.Rows.Count == 0)
                return false;
            
            string str = D.GetString(dataTable.Rows[0]["ST_DOCUAPP"]);
            
            return D.GetString(ST_DOCU) == "1" && str == "001" || str == "002";
        }

        internal object Search_VAT_ACCT_SUB(string[] param)
        {
            return DBHelper.GetDataTable("UP_FI_Z_MNS_CARD_VAT_ACCT_S", (object[])param);
        }

        internal bool Search_VAT_ACCT_SUB(string 계정코드)
        {
            return DBHelper.ExecuteScalar("SELECT * FROM FI_Z_MNS_VAT_ACCTCODE WHERE CD_COMPANY ='" + MA.Login.회사코드 + "' AND CD_ACCT ='" + 계정코드 + "'") != null;
        }

        internal bool Search_MCC_CODE(string 업종코드)
        {
            return DBHelper.ExecuteScalar("SELECT * FROM FI_Z_MNS_MCC_CODE WHERE MCC_CODE ='" + 업종코드 + "'") != null;
        }

        internal bool Save_VAT_ACCT_SUB(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return false;

            return DBHelper.Save(new SpInfo()
            {
                CompanyID = MA.Login.회사코드,
                UserID = MA.Login.사용자아이디,
                DataValue = dt,
                SpNameInsert = "UP_FI_Z_MNS_CARD_VAT_ACCT_I",
                SpNameDelete = "UP_FI_Z_MNS_CARD_VAT_ACCT_D",
                SpParamsInsert = new string[] { "CD_COMPANY",
                                                "SEQ",
                                                "CD_ACCT" },
                SpParamsDelete = new string[] { "CD_COMPANY", 
                                                "CD_ACCT" }
            });
        }

        internal DataTable Search_CARDCLOSE_SUB(object[] parameter)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_FI_Z_HAATZ_CARDCLOSE_S", parameter);
            string str = D.GetString(parameter[1]);

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                for (int index = 1; index < 13; ++index)
                {
                    DataRow row = dataTable.NewRow();
                    row["YM_CLOSE"] = (str + string.Format("{0:D2}", index));
                    row["TP_CLOSE"] = "0";
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        internal bool Save_CARDCLOSE_SUB(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return false;

            return DBHelper.Save(new SpInfo()
            {
                CompanyID = MA.Login.회사코드,
                UserID = MA.Login.사용자아이디,
                DataValue = dt,
                DataState = DataValueState.Modified,
                SpNameUpdate = "UP_FI_Z_HAATZ_CARDCLOSE_U",
                SpParamsUpdate = new string[] { "CD_COMPANY",
                                                "YM_CLOSE",
                                                "TP_CLOSE",
                                                "ID_UPDATE" }
            });
        }

        internal bool Check_CARDCLOSE(string 회계일자)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" SELECT TP_CLOSE ");
            stringBuilder.Append(" FROM FI_Z_HAATZ_CARDCLOSE ");
            stringBuilder.Append(" WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'");
            stringBuilder.Append(" AND YM_CLOSE = '" + 회계일자 + "' ");
            DataTable dataTable = DBHelper.GetDataTable(stringBuilder.ToString());
            
            return dataTable != null && dataTable.Rows.Count > 0 && dataTable.Rows[0]["TP_CLOSE"].ToString() == "1";
        }

        internal DataTable Search_MAPPING_SUB(object[] parameter)
        {
            return DBHelper.GetDataTable("UP_FI_Z_SSCARD_DOCU_MAPPING_S", parameter);
        }

        internal bool SAVE_MAPPING_DOCU(DataTable dt, object NO_DOCU, object NO_DOLINE, object CD_PC)
        {
            return DBHelper.Save(new SpInfo()
            {
                CompanyID = MA.Login.회사코드,
                DataValue = dt,
                DataState = DataValueState.Modified,
                UserID = MA.Login.사용자아이디,
                SpNameUpdate = "UP_FI_Z_SSCARD_DOCU_MAPPING_U",
                SpParamsUpdate = new string[] { "ACCT_NO",
                                                "BANK_CODE",
                                                "TRADE_DATE",
                                                "TRADE_TIME",
                                                "SEQ",
                                                "ID_INSERT",
                                                "MAP_CD_PC",
                                                "MAP_NO_DOCU",
                                                "MAP_NO_DOLINE",
                                                "CD_COMPANY" },
                SpParamsValues = { { ActionState.Update, "MAP_CD_PC", CD_PC },
                                   { ActionState.Update, "MAP_NO_DOCU", NO_DOCU },
                                   { ActionState.Update, "MAP_NO_DOLINE", NO_DOLINE } } 
            });
        }

        internal bool DELETE_MAPPING_DOCU(DataTable dt)
        {
            return DBHelper.Save(new SpInfo()
            {
                CompanyID = MA.Login.회사코드,
                DataValue = dt,
                DataState = DataValueState.Modified,
                UserID = MA.Login.사용자아이디,
                SpNameUpdate = "UP_FI_Z_SSCARD_DOCU_MAPPING_D",
                SpParamsUpdate = new string[] { "ACCT_NO",
                                                "BANK_CODE",
                                                "TRADE_DATE",
                                                "TRADE_TIME",
                                                "SEQ",
                                                "ID_INSERT",
                                                "NODE_CODE",
                                                "NO_DOCU",
                                                "LINE_NO",
                                                "CD_COMPANY" }
            });
        }

        internal DataRow GetUserCard(object[] parameter)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_H_FI_CARD_USER_SELECT", parameter);
            return dataTable == null || dataTable.Rows.Count == 0 ? null : dataTable.Rows[0];
        }

        internal DataTable Search_ISEC_SUB(object[] parameter)
        {
            return DBHelper.GetDataTable("UP_FI_Z_ISEC_CARDTEMP_GWSEND_S", parameter);
        }

        internal bool Save_ISEC_SUB(object[] parameter)
        {
            try
            {
                DBHelper.ExecuteNonQuery("UP_FI_Z_ISEC_CARDTEMP_GWSEND_U", parameter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal bool Save_ISEC_CANCEL(object[] parameter)
        {
            try
            {
                DBHelper.ExecuteNonQuery("UP_FI_Z_ISEC_CARDTEMP_GWSEND_D", parameter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal bool Save_KAIMRO_JUCARD(object[] parameter)
        {
            try
            {
                DBHelper.ExecuteNonQuery("UP_FI_Z_KAIMRO_CARDTEMP_JUCARD", parameter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal bool CreatePartner(object[] parameter)
        {
            DBHelper.ExecuteNonQuery("UP_FI_CARD_TEMP_PARTNER_REMAKE", parameter);
            return true;
        }

        internal DataTable Get_RECARD_CBS(object[] parameter)
        {
            return DBHelper.GetDataTable("UP_FI_Z_CBS_FI_RECARD", parameter);
        }

        public DataSet 전표출력(string 회계단위, string 전표번호)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_IVMNG_RPT2_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     회계단위,
                                                                                     전표번호 });

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    ds.Tables[0].Columns.Add("NO_SO");
                    ds.Tables[0].Columns.Add("NO_IO");
                }
            }

            return ds;
        }
    }
}
