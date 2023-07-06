using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
   internal class P_CZ_BI_WTMCALC_MONTH_RMK_BIZ
    {
        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameInsert = "PS_CZ_BI_WTMCALC_MONTH_RMK_I";
            spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "NO_EMP",
                                                   "RMK",
                                                   "DATE_MONTH"
            };

            spInfo.SpNameUpdate = "PS_CZ_BI_WTMCALC_MONTH_RMK_U";
            spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "NO_EMP",
                                                   "RMK",
                                                   "DATE_MONTH"
            };

            spInfo.SpNameDelete = "PS_CZ_BI_WTMCALC_MONTH_RMK_D";
            spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                   "NO_EMP",
                                                   "RMK",
                                                   "DATE_MONTH"};

            return DBHelper.Save(spInfo);
        }
    }
}
