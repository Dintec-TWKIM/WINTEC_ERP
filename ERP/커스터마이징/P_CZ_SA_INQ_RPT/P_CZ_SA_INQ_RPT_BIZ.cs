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
	class P_CZ_SA_INQ_RPT_BIZ
	{
		public DataTable Search(object[] obj, string name)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_INQ_RPTH_" + name + "_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_INQ_RPTL_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public bool SaveData(DataTable dtL)
		{
            SpInfo si = new SpInfo();
            si.DataValue = dtL;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;
            si.SpNameUpdate = "SP_CZ_SA_INQ_RPTL_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",   
											   "TP_STEP",      	
											   "NO_KEY",
											   "DC_RMK",
											   "ID_UPDATE" };

            return DBHelper.Save(si);
		}
	}
}
