using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace cz
{
    class P_CZ_SA_USER_SUB_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_USER_SUB_S", obj);
            T.SetDefaultValue(dt);

            dt.Columns["YN_EXPECT"].DefaultValue = "N";

            return dt;
        }

        public bool SaveData(DataTable dt, string 사용자ID)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_SA_USER_SUB_I";
            si.SpNameUpdate = "SP_CZ_SA_USER_SUB_U";
            si.SpParamsInsert = new string[] { "CD_COMPANY",
                                               "ID_USER",
                                               "TP_SALES",
                                               "TP_SALES_DEF",
                                               "TP_BIZ",
                                               "CD_SALEGRP",
                                               "CD_PARTNER_GRP",
                                               "ID_SALES",
                                               "ID_SALES_DEF",
                                               "ID_TYPIST",
                                               "ID_TYPIST_DEF",
                                               "ID_PUR",
                                               "ID_PUR_DEF",
                                               "ID_LOG",
                                               "ID_LOG_DEF",
                                               "ID_FIRST_APPROVAL",
                                               "ID_SECOND_APPROVAL",
                                               "NM_ENG",
	                                           "NO_EMAIL",
	                                           "NO_TEL",
	                                           "NO_TEL_EMER",
	                                           "DC_RMK",
	                                           "DC_RMK1",
                                               "YN_EXPECT",
                                               "ID_INSERT" };
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "ID_USER",
                                               "TP_SALES",
                                               "TP_SALES_DEF",
                                               "TP_BIZ",
                                               "CD_SALEGRP",
                                               "CD_PARTNER_GRP",
                                               "ID_SALES",
                                               "ID_SALES_DEF",
                                               "ID_TYPIST",
                                               "ID_TYPIST_DEF",
                                               "ID_PUR",
                                               "ID_PUR_DEF",
                                               "ID_LOG",
                                               "ID_LOG_DEF",
                                               "ID_FIRST_APPROVAL",
                                               "ID_SECOND_APPROVAL",
                                               "NM_ENG",
	                                           "NO_EMAIL",
	                                           "NO_TEL",
	                                           "NO_TEL_EMER",
	                                           "DC_RMK",
	                                           "DC_RMK1",
                                               "YN_EXPECT",
                                               "ID_UPDATE" };

            si.SpParamsValues.Add(ActionState.Insert, "ID_USER", 사용자ID);
            si.SpParamsValues.Add(ActionState.Update, "ID_USER", 사용자ID);

            return DBHelper.Save(si);
        }

        public bool DeleteData(string 사용자ID)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_SA_USER_SUB_D", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                  사용자ID });
        }
    }
}
