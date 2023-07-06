using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.IO;
using Dintec;

namespace cz
{
    internal class P_CZ_HR_PHONE_PAY_LIST_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PHONE_PAY_LIST_S", obj);
        }

        internal DataTable UserSearch(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PHONE_PAY_USER_S", obj);
        }

        public bool 전표처리(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_HR_PHONE_PAY_DOCU", obj);
        }

        public bool 전표취소(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_FI_DOCU_AUTODEL", obj);
        }

        public string SearchFileInfo(string companyCode, string fileCode)
        {
            string query, result = string.Empty;

            try
            {
                query = "SELECT MAX(FILE_NAME) AS FILE_PATH_MNG " +
                        "FROM MA_FILEINFO WITH(NOLOCK) " +
                        "WHERE CD_COMPANY = '" + companyCode + "' " +
                        "AND CD_MODULE = 'CZ' " +
                        "AND ID_MENU = 'P_CZ_HR_PHONE_PAY' " +
                        "AND CD_FILE = '" + fileCode + "'";

                result = D.GetString(Global.MainFrame.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }

        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;

            spInfo.SpNameUpdate = "SP_CZ_HR_PHONE_PAY_LIST_U";
            spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "YM",
                                                   "NO_EMP",
                                                   "YN_PAY",
                                                   "ID_UPDATE"
            };
            return DBHelper.Save(spInfo);
        }

        internal bool SaveAuto(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;


            object[] data = new string[] { dt.Columns["CD_COMPANY"].ToString(), dt.Columns["YM"].ToString(), dt.Columns["NO_EMP"].ToString(), dt.Columns["ID_INSERT"].ToString()};
            return DBHelper.ExecuteNonQuery("SP_CZ_HR_PHONE_PAY_AUTO_I", data);

            //spInfo.SpNameInsert = "SP_CZ_HR_PHONE_PAY_AUTO_I";
            //spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
            //                                       "YM",
            //                                       "NO_EMP",
            //                                       "ID_INSERT"
            //};
            //return DBHelper.Save(spInfo);

        }
    }
}
