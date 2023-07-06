using System.Data;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Common.Util;
using System;


namespace cz
{

    internal class P_CZ_HR_CONTRACT_REG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_HR_CONTRACT_REG_S", obj);
            return dataTable;
        }

        internal DataTable Search2(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_HR_CONTRACT_REG_S2", obj);
            return dataTable;
        }


        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameInsert = "SP_CZ_HR_CONTRACT_REG_I";
            spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "NO_EMP",
                                                   "NM_TITLE",
                                                   "NM_CONTRACT",
                                                   "DT_REG",
                                                   "NM_CONTRACT_COMPANY1",
                                                   "NM_CONTRACT_COMPANY2",
                                                   "DT_FROM",
                                                   "DT_TO",
                                                   "YN_AUTO",
                                                   "YN_APPROVE",
                                                   "NO_DOCU",
                                                   "CONTRACT_DEPT",
                                                   "CONTRACT_EMP",
                                                   "CONTRACT_DEPT2",
                                                   "DC_RMK",
                                                   "FILE_PATH_CODE",
                                                   "ID_INSERT"
            };

            spInfo.SpNameUpdate = "SP_CZ_HR_CONTRACT_REG_U";
            spInfo.SpParamsUpdate = new string[] { "SEQ",
                                                   "CD_COMPANY",
                                                   "NO_EMP",
                                                   "NM_TITLE",
                                                   "NM_CONTRACT",
                                                   "DT_REG",
                                                   "NM_CONTRACT_COMPANY1",
                                                   "NM_CONTRACT_COMPANY2",
                                                   "DT_FROM",
                                                   "DT_TO",
                                                   "YN_AUTO",
                                                   "YN_APPROVE",
                                                   "NO_DOCU",
                                                   "CONTRACT_DEPT",
                                                   "CONTRACT_EMP",
                                                   "CONTRACT_DEPT2",
                                                   "DC_RMK",
                                                   "FILE_PATH_CODE",
                                                   "ID_UPDATE"
            };

            spInfo.SpNameDelete = "SP_CZ_HR_CONTRACT_REG_D";
            spInfo.SpParamsDelete = new string[] { "SEQ" };

            return DBHelper.Save(spInfo);
        }



        public string SearchFileInfo(string companyCode, string fileCode)
        {
            string query, result = string.Empty;

            try
            {
                query = "SELECT MAX(FILE_NAME) AS FILE_PATH_MNG " +
                        "FROM MA_FILEINFO " +
                        "WHERE CD_COMPANY = '" + companyCode + "' " +
                        "AND CD_MODULE = 'CZ' " +
                        "AND ID_MENU = 'P_CZ_HR_CONTRACT_REG' " +
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
