using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using DX;
using System.Data;

namespace cz
{
	internal class P_CZ_MM_SUPPLIES_QTIO_RPT_SUB_BIZ
	{
		internal DataTable Search(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_MM_SUPPLIES_QTIO_RPT_SUB_S", obj);
		}

		internal bool Save(DataTable dt)
		{
			SpInfo si = new SpInfo();

			si.DataValue = dt;
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;
			si.SpNameInsert = "SP_CZ_MM_SUPPLIES_QTIO_RPT_SUB_I";
			si.SpNameUpdate = "SP_CZ_MM_SUPPLIES_QTIO_RPT_SUB_U";
			si.SpNameDelete = "SP_CZ_MM_SUPPLIES_QTIO_RPT_SUB_D";
			si.SpParamsInsert = new string[] { "CD_COMPANY",
											   "NM_ITEM",
											   "MAKER",
											   "NO_MODEL",
											   "UNIT",
											   "CD_DEPT",
											   "ID_INSERT" };
			si.SpParamsUpdate = new string[] { "CD_COMPANY",
											   "CD_ITEM",
											   "NM_ITEM",
											   "MAKER",
											   "NO_MODEL",
											   "UNIT",
											   "CD_DEPT",
											   "ID_UPDATE" };
			si.SpParamsDelete = new string[] { "CD_COMPANY",
											   "CD_ITEM" };

			return DBHelper.Save(si);
		}
	}
}