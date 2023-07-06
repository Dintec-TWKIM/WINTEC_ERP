using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Forms;
using Duzon.Common.Util;

namespace cz
{
	internal class P_CZ_SA_EXPENSE_MNG_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_EXPENSE_MNG_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public bool Save(DataTable dt, string tpIo)
		{
            if (dt == null || dt.Rows.Count == 0) return false;

            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_SA_EXPENSE_MNG_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "TP_IO",
                                               "NO_IV",
                                               "NO_LINE",
                                               "TXT_USERDEF1",
                                               "ID_UPDATE" };

            si.SpParamsValues.Add(ActionState.Update, "TP_IO", tpIo);

            return DBHelper.Save(si);
		}
	}
}
