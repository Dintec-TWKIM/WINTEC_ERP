using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Dintec;
using Duzon.Common.Forms;

namespace cz
{
    class P_CZ_MA_HULL_RPT_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_RPTH_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_RPTL_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_MA_HULL_RPT_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_IMO",
                                               "YN_NEWSHIP",
                                               "DC_RMK_RPT",
                                               "ID_UPDATE" };

            si.SpParamsValues.Add(ActionState.Update, "ID_UPDATE", Global.MainFrame.LoginInfo.UserID);

            return DBHelper.Save(si);
        }
    }
}
