using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_MA_HULL_DOM_RPT_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_DOM_RPTH_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_DOM_RPTL_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_MA_HULL_DOM_RPT_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_IMO",
                                               "CD_AGENT",
                                               "CD_PUR",
                                               "DC_RMK_DOM",
                                               "ID_UPDATE" };

            si.SpParamsValues.Add(ActionState.Update, "ID_UPDATE", Global.MainFrame.LoginInfo.UserID);

            return DBHelper.Save(si);
        }
    }
}
