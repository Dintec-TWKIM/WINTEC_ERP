using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using DX;
using System.Data;

namespace cz
{
	internal class P_CZ_PR_ROUT_BATCH_GROUP_SUB_BIZ
	{
		internal DataTable Search(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_ROUT_BATCH_GROUP_SUB_S", obj);
		}

		internal bool Save(DataTable dt)
		{
			SpInfo si = new SpInfo();

			si.DataValue = dt;
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;
			si.SpNameInsert = "SP_CZ_PR_ROUT_BATCH_GROUP_SUB_I";
			si.SpNameUpdate = "SP_CZ_PR_ROUT_BATCH_GROUP_SUB_U";
			si.SpNameDelete = "SP_CZ_PR_ROUT_BATCH_GROUP_SUB_D";
			si.SpParamsInsert = new string[] { "CD_COMPANY",
											   "NM_GPE",
											   "QT_CAPAMIN",
											   "QT_CAPAMAX",
											   "DC_RMK",
											   "ID_INSERT" };
			si.SpParamsUpdate = new string[] { "CD_COMPANY",
											   "CD_GPE",
											   "NM_GPE",
											   "QT_CAPAMIN",
											   "QT_CAPAMAX",
											   "DC_RMK",
											   "ID_UPDATE" };
			si.SpParamsDelete = new string[] { "CD_COMPANY",
											   "CD_GPE" };
			si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT", Global.MainFrame.LoginInfo.EmployeeNo);
			si.SpParamsValues.Add(ActionState.Update, "ID_UPDATE", Global.MainFrame.LoginInfo.EmployeeNo);
			return DBHelper.Save(si);
		}
	}
}