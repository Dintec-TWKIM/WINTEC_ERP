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
    internal class P_CZ_HR_EMP_RPT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_HR_EMP_RPT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_HR_EMP_RPT_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_EMP",
                                               "DC_RMK",  
                                               "ID_UPDATE" };

            return DBHelper.Save(si);
        }
    }
}
