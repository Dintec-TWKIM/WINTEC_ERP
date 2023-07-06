using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_FI_CARD_VERIFY_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_CARD_VERIFYH_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail1(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_CARD_VERIFYL_01_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail2(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_CARD_VERIFYL_02_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail3(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_CARD_VERIFYL_03_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail4(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_CARD_VERIFYL_04_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail5(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_CARD_VERIFYL_05_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail6(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_CARD_VERIFYL_06_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail7(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_CARD_VERIFYL_07_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		internal bool Save(DataTable dt)
		{
			SpInfo si = new SpInfo();

			si.DataValue = dt;
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;

			si.SpNameUpdate = "SP_CZ_FI_CARD_VERIFYH_U";
			si.SpParamsUpdate = new string[] { "CD_COMPANY",
											   "ACCT_NO",
											   "ADMIN_NO",
											   "YN_VERIFY",
											   "DC_RMK",
											   "ID_INSERT" };

			return DBHelper.Save(si);
		}
	}
}
