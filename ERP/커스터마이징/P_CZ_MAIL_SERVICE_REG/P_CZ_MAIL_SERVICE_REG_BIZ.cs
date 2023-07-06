using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
    class P_CZ_MAIL_SERVICE_REG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_MAIL_SERVICE_S", obj);
        }

        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;

            dt.Rows[0]["ID_UPDATE"] = Global.MainFrame.LoginInfo.UserID;
            dt.Rows[0]["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserName;
            spInfo.SpNameInsert = "UP_CZ_MAIL_SERVICE_I";
            spInfo.SpParamsInsert = new string[] { "NO_MAIL",
                                                   "MAIL_TO",
                                                   "MAIL_CC",
                                                   "MAIL_BCC",
                                                   "FOLDER",
                                                   "CATEGORY_1",
                                                   "CATEGORY_2",
                                                   "CATEGORY_3",
                                                   "CATEGORY_4",
                                                   "KEYWORD",
                                                   "HOST_MAIL",
                                                   "MOVE_MAIL",
                                                   "USE_YN",
                                                   "DC_RMK",
                                                   "ID_INSERT"
            };
            spInfo.SpNameUpdate = "UP_CZ_MAIL_SERVICE_U";
            spInfo.SpParamsUpdate = new string[] {
                                                   "MAIL_TO",
                                                   "MAIL_CC",
                                                   "MAIL_BCC",
                                                   "FOLDER",
                                                   "CATEGORY_1",
                                                   "CATEGORY_2",
                                                   "CATEGORY_3",
                                                   "CATEGORY_4",
                                                   "KEYWORD",
                                                   "HOST_MAIL",
                                                   "MOVE_MAIL",
                                                   "USE_YN",
                                                   "DC_RMK",
                                                   "ID_UPDATE",
                                                   "NO_MAIL"
            };
            spInfo.SpNameDelete = "UP_CZ_MAIL_SERVICE_D";
            spInfo.SpParamsDelete = new string[] { "NO_MAIL" };
            return DBHelper.Save(spInfo);
        }
    }
}
