using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Dintec;

namespace cz
{
    internal class P_CZ_SA_CONTRACT_RPT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CONTRACT_RPTH_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CONTRACT_RPTL_S", obj);

            if (!Certify.IsLive())
                dataTable.Columns.Remove("YN_GULL");

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        public bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection sc = new SpInfoCollection();
            SpInfo spInfo;

            if (dtH != null)
            {
                dtH.RemotingFormat = SerializationFormat.Binary;

                spInfo = new SpInfo();

                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameUpdate = "SP_CZ_SA_CONTRACT_RPTH_U";
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "NO_SO",
                                                       "DC_RMK",
                                                       "ID_UPDATE" };
                sc.Add(spInfo);
            }

            if (dtL != null)
            {
                dtL.RemotingFormat = SerializationFormat.Binary;

                spInfo = new SpInfo();

                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameUpdate = "SP_CZ_SA_CONTRACT_RPTL_U";
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "NO_SO",
                                                       "SEQ_SO",
                                                       "DC_RMK_CONTRACT",
                                                       "ID_UPDATE" };
                sc.Add(spInfo);
            }

            return DBHelper.Save(sc);
        }

        public bool 확정상태저장(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();

            if (dt != null)
            {
                dt.RemotingFormat = SerializationFormat.Binary;

                spInfo.DataValue = dt;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameUpdate = "SP_CZ_SA_CONTRACT_RPT_STA";
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "NO_SO",
                                                       "STA_SO",
                                                       "NO_CONTRACT",
                                                       "DT_CONTRACT",
                                                       "YN_CHARGE",
                                                       "ID_UPDATE" };
            }

            return DBHelper.Save(spInfo);
        }

        public bool CheckCredit(string 거래처, Decimal 금액)
        {
            string str1 = "001";
            object[] outParameters = new object[3];
            List<object> list = new List<object>();
            list.Add(Global.MainFrame.LoginInfo.CompanyCode);
            list.Add(거래처);
            list.Add(금액);
            list.Add(str1);

            if (BASIC.GetMAEXC("수주관리-여신체크방법") == "001")
            {
                list.Add("001");
                list.Add(Global.MainFrame.GetStringToday);
            }

            DBHelper.GetDataTable("UP_SA_CHECKCREDIT_SELECT", list.ToArray(), out outParameters);

            if (outParameters[0] != DBNull.Value)
            {
                string str2 = D.GetDecimal(outParameters[0]).ToString("###,###,###,###,##0.####").PadLeft(15);
                string str3 = D.GetDecimal(outParameters[1]).ToString("###,###,###,###,##0.####").PadLeft(15);
                string str4 = "- 거 래 처  :   " + D.GetString(CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER, new object[]{ MA.Login.회사코드, 
                                                                                                                             거래처 })["LN_PARTNER"]) + " (" + 거래처 + ") \n- 여신잔액  : " + str2 + "\n- 수주금액  : " + str3 + "\n";
                if (D.GetString(outParameters[2]) == "002")
                {
                    string code = "여신금액을 초과하였습니다. 그래도 확정 하시겠습니까?\n\n" + str4;
                    return Global.MainFrame.ShowMessage(code, "QY2") == DialogResult.Yes;
                }

                if (D.GetString(outParameters[2]) == "003")
                {
                    string code = "여신금액을 초과하여 확정 할 수 없습니다.\n\n" + str4;
                    Global.MainFrame.ShowMessage(code);
                    return false;
                }
            }

            return true;
        }

        internal bool CheckCreditExec(string cdPartner, string cdExch, Decimal amEx)
        {
            object[] outParameters = new object[3];
            DBHelper.GetDataTable("UP_SA_CHECKCREDIT_EXCH_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                  cdPartner,
                                                                                  cdExch,
                                                                                  amEx,
                                                                                  "001" }, out outParameters);

            if (D.GetString(outParameters[0]) != string.Empty)
            {
                if (D.GetString(outParameters[2]) == "002")
                    return Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. [거래처 : @]\n(여신총액 : @, 잔액 : @)\n저장하시겠습니까 ?", new string[] { cdPartner, cdExch, D.GetString(outParameters[0]), D.GetString(outParameters[1]) }, "QY2") == DialogResult.Yes;
                if (D.GetString(outParameters[2]) == "003")
                {
                    Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액(@) : @ 잔액 : @)", new string[] { cdExch, D.GetString(outParameters[0]), D.GetString(outParameters[1]) });
                    return false;
                }
            }

            return true;
        }
    }
}
