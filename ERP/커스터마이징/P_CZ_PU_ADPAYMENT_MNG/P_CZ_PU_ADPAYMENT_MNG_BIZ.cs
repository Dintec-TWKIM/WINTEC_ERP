using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_PU_ADPAYMENT_MNG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_ADPAYMENTH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable DetailSearch(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_ADPAYMENTL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool Save(DataTable dtL, DataTable dtF)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();

            if (dtL != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataState = DataValueState.Added;
                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "SP_CZ_PU_ADPAYMENT_I";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_BIZAREA",
                                                       "NO_ADPAY",
                                                       "NO_ADPAYLINE",
                                                       "DT_ADPAY",
                                                       "NO_PO",
                                                       "NO_POLINE",
                                                       "CD_EXCH",
                                                       "RT_EXCH",
                                                       "AM_PRE",
                                                       "AM_EX_PRE",
                                                       "CD_DOCU",
                                                       "QT_PRE",
                                                       "CD_DEPT",
                                                       "YN_JEONJA",
                                                       "TP_AIS",
                                                       "DT_PAY_SCHEDULE",
                                                       "PO_CONDITION",
                                                       "DT_ACCT",
                                                       "ID_INSERT" };
                spInfoCollection.Add(spInfo);
            }

            if (dtF != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataState = DataValueState.Added;
                spInfo.DataValue = dtF;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "SP_CZ_PU_ADPAYMENT_TRANS_DOCU";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "LANGUAGE",
                                                       "NO_ADPAY",
                                                       "NO_PO",
                                                       "NO_MODULE",
                                                       "FG_EX_CC",
                                                       "ID_INSERT" };

                spInfo.SpParamsValues.Add(ActionState.Insert, "LANGUAGE", Global.MainFrame.LoginInfo.Language);
                spInfo.SpParamsValues.Add(ActionState.Insert, "FG_EX_CC", "000");

                spInfoCollection.Add(spInfo);
            }

            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spInfoCollection))
            {
                if (!resultData.Result)
                    return false;
            }

            foreach (DataRow dr in dtF.Rows)
            {
                DBHelper.ExecuteScalar(@"UPDATE PU_POH
                                         SET TXT_USERDEF1 = '" + dr["TXT_USERDEF1"].ToString() + "'" + Environment.NewLine +
                                        "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                        "AND NO_PO = '" + dr["NO_PO"].ToString() + "'");
            }

            return true;
        }

        public bool Delete(DataTable dtL)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            if (dtL != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataState = DataValueState.Added;
                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "SP_CZ_PU_ADPAYMENT_D";
                spInfo.SpParamsInsert = new string[] { "NO_ADPAY",
                                                       "CD_COMPANY" };
                spInfoCollection.Add(spInfo);
            }
            if (dtL != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataState = DataValueState.Added;
                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "SP_CZ_FI_DOCU_AUTODEL";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NO_MODULE",
                                                       "NO_ADPAY",
                                                       "ID_INSERT" };
                spInfoCollection.Add(spInfo);
            }
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spInfoCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }

        internal DataTable SearchCheckHeader(string 멀티번호, object[] obj)
        {
            DataTable dataTable1 = (DataTable)null;
            string[] pipes = D.StringConvert.GetPipes(멀티번호, 150);
            D.GetString((object)pipes.Length);
            foreach (string str in pipes)
            {
                obj[11] = (object)str;
                DataTable dataTable2 = DBHelper.GetDataTable("SP_CZ_PU_ADPAYMENTL_S", obj);
                if (dataTable1 == null)
                    dataTable1 = dataTable2;
                else
                    dataTable1.Merge(dataTable2);
            }
            return dataTable1;
        }
    }
}
