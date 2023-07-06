using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.IO;
using Dintec;

namespace cz
{
    internal class P_CZ_HR_PHONE_PAY_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            //CZ_HR_PHONE_PAYH
            return DBHelper.GetDataTable("SP_CZ_HR_PHONE_PAY_S", obj);
        }

        internal DataTable UserSearch(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PHONE_PAY_USER_S", obj);
        }

        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameInsert = "SP_CZ_HR_PHONE_PAY_I";
            spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "YM",
                                                   "NO_EMP",
                                                   "NM_TITLE",
                                                   "AM_TOTAL",
                                                   "AM_BASIC",
                                                   "AM_ADD1",
                                                   "AM_ROAMING1",
                                                   "AM",
                                                   "DT_REG",
                                                   "YN_PAY",
                                                   "NO_DOCU",
                                                   "DC_RMK",
                                                   "YN_USE",
                                                   "ID_INSERT",
                                                   "YN_STAT"
            };

            spInfo.SpNameUpdate = "SP_CZ_HR_PHONE_PAY_U";
            spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "YM",
                                                   "NO_EMP",
                                                   "NM_TITLE",
                                                   "AM_TOTAL",
                                                   "AM_BASIC",
                                                   "AM_ADD1",
                                                   "AM_ROAMING1",
                                                   "AM",
                                                   "DT_REG",
                                                   "YN_PAY",
                                                   "NO_DOCU",
                                                   "DC_RMK",
                                                   "YN_USE",
                                                   "ID_UPDATE"
            };

            spInfo.SpNameDelete = "SP_CZ_HR_PHONE_PAY_D";
            spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                   "NO_EMP",
                                                   "YM" };

            return DBHelper.Save(spInfo);
        }


        public string SaveState(string CD_COMPANY, string YM, string NO_EMP)
        {
            string query, result = string.Empty;

            try
            {
                query = "UPDATE CZ_HR_PHONE_PAY " +
                        "SET YN_STAT = 'Y', NO_DOCU = ''" +
                        "WHERE CD_COMPANY = '" + CD_COMPANY + "' " +
                        "AND YM = '" + YM + "' " +
                        "AND NO_EMP = '" + NO_EMP + "' ";

                result = D.GetString(Global.MainFrame.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
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


    }
}
