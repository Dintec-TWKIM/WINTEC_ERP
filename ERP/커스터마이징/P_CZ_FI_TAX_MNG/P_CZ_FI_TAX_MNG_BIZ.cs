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
	internal class P_CZ_FI_TAX_MNG_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_TAX_MNG_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		internal bool Save(DataTable dt)
		{
			if (dt == null || dt.Rows.Count == 0) return false;

			SpInfo si = new SpInfo();

			si.DataValue = dt;
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;

			si.SpNameUpdate = "SP_CZ_FI_TAX_MNG_U";
			si.SpParamsUpdate = new string[] { "CD_COMPANY",
											   "NO_IV",
											   "NO_IO",
											   "NO_SO",
											   "AM_EX",
											   "AM",
											   "DT_LOADING",
											   "DT_VAT",
											   "TP_EXPORT",
											   "DC_RMK",
											   "ID_UPDATE" };

			return DBHelper.Save(si);
		}
	}
}
