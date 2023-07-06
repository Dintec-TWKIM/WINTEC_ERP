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
    internal class P_CZ_SA_READY_STATUS_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_READY_STATUSH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_READY_STATUSL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool SaveData(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_SA_READY_STATUSH_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_SO",
                                               "YN_ONTIME",
                                               "DC_RMK",
                                               "ID_UPDATE" };

            return DBHelper.Save(si);
        }
    }
}
