using System;
using System.Data;
using Duzon.BizOn.Erpu.Security;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.UEncryption;

namespace cz
{
    internal class P_CZ_FI_BANK_SEND_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = this.Setting();

            string str = string.Empty;

            if (D.GetString(dataTable.Rows[0]["YN_LIMITE"]) == "N")
            {
                if (D.GetString(obj[5]) == "1")
                    str = "SP_CZ_FI_BANK_SEND_BAN_PARTNER_S";
                else if (D.GetString(obj[5]) == "2")
                    str = "SP_CZ_FI_BANK_SEND_BAN_EMP_S";
                else if (D.GetString(obj[5]) == "3")
                    str = "UP_FI_BANK_SEND_BAN_ETC_S";
                else if (D.GetString(obj[5]) == "4")
                    str = "UP_FI_BANK_SEND_BAN_CARD_S";
            }
            else
            {
                if (D.GetString(obj[5]) == "1")
                    str = "SP_CZ_FI_BANK_SEND_BAN_PARTNER_S2";
                else if (D.GetString(obj[5]) == "2")
                    str = "UP_FI_BANK_SEND_BAN_EMP_S2";
                else if (D.GetString(obj[5]) == "3")
                    str = "UP_FI_BANK_SEND_BAN_ETC_S2";
                else if (D.GetString(obj[5]) == "4")
                    str = "UP_FI_BANK_SEND_BAN_CARD_S2";
            }
            
            DataTable dt = DBHelper.GetDataTable(str, obj);
            UEncryption.SearchDecryptionTable(dt, new object[] { "TRANS_ACCT_NO" }, new DataType[] { DataType.Account });

            return dt;
        }

        internal DataTable SearchDetail(string 전표번호, int 라인번호)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_FI_BANK_SEND_BAND_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                    전표번호,
                                                                                                    라인번호 });
            UEncryption.SearchDecryptionTable(dataTable, new object[] { "ACCT_NO",
                                                                        "TRANS_ACCT_NO" }, new DataType[] { DataType.Account,
                                                                                                            DataType.Account });
            return dataTable;
        }

        internal DataSet SearchCdTacct(string 계좌번호)
        {
            return DBHelper.GetDataSet("UP_FI_BANK_SEND_BAN_ACCT_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                    계좌번호 });
        }

        internal DataTable Setting()
        {
            string str = @"SELECT RTRIM(TP_SET1) TP_SET1, 
                                  RTRIM(TP_SET2) TP_SET2, 
                                  RTRIM(TP_SET3) TP_SET3, 
                                  RTRIM(TP_TRANS) TP_TRANS, 
                                  YN_LIMITE 
                           FROM BANK_SYS WITH(NOLOCK)  
                           WHERE C_CODE = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                          "AND USE_YN = '1'";

            return Global.MainFrame.FillDataTable(str);
        }

        internal bool IsExist신전표()
        {
            string str = @"SELECT * 
                           FROM MA_N_BASEMENU WITH(NOLOCK)
                           WHERE ID_MENU = 'P_FI_DOCU'
                           AND ID_CLASS  = 'P_FI_DOCU_NEW'";
            return Global.MainFrame.FillDataTable(str).Rows.Count != 0;
        }
    }
}