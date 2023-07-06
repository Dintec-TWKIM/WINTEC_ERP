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
	class P_CZ_SA_IV_PJT_RPT_BIZ
	{
		internal DataTable Search(object[] obj)
		{
			DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_IV_PJT_RPTH_S", obj);
			T.SetDefaultValue(dataTable);
			return dataTable;
		}

		public DataTable Search매입(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IV_PJT_RPTL_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable Search매출(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IV_PJT_RPTR_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

        public bool SaveData(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_SA_IV_PJT_RPTH_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_PROJECT",
                                               "DC_RMK_CONTRACT",
											   "DC_RMK_PJT",
											   "ID_UPDATE" };

            si.SpParamsValues.Add(ActionState.Update, "ID_UPDATE", Global.MainFrame.LoginInfo.UserID);

            return DBHelper.Save(si);
        }
	}
}
