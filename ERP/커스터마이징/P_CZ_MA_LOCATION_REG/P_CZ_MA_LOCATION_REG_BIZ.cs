using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_MA_LOCATION_REG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("UP_MA_LOCATION_SELECT", obj);
        }

        internal DataTable SearchLine(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_MA_LOCATIONL_S", obj);
        }

        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) 
                return true;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
            spInfo.SpNameInsert = "UP_MA_LOCATION_INSERT";
            spInfo.SpParamsInsert = new string[] { "CD_SL",
                                                   "CD_PLANT",
                                                   "CD_COMPANY",
                                                   "CD_LOCATION",
                                                   "NM_LOCATION",
                                                   "NO_SEQ",
                                                   "YN_USE",
                                                   "DC_RMK",
                                                   "ID_INSERT" };
            spInfo.SpNameUpdate = "UP_MA_LOCATION_UPDATE";
            spInfo.SpParamsUpdate = new string[] { "CD_SL",
                                                   "CD_PLANT",
                                                   "CD_COMPANY",
                                                   "CD_LOCATION",
                                                   "NM_LOCATION",
                                                   "NO_SEQ",
                                                   "YN_USE",
                                                   "DC_RMK",
                                                   "ID_INSERT" };
            spInfo.SpNameDelete = "UP_MA_LOCATION_DELETE";
            spInfo.SpParamsDelete = new string[] { "CD_SL",
                                                   "CD_PLANT",
                                                   "CD_COMPANY",
                                                   "CD_LOCATION" };
            return DBHelper.Save(spInfo);
        }
    }
}
