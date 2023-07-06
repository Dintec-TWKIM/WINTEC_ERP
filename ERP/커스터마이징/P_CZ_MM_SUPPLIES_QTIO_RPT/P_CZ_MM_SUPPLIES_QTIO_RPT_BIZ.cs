using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using DX;
using System.Data;

namespace cz
{
	internal class P_CZ_MM_SUPPLIES_QTIO_RPT_BIZ
	{
		public DataTable Search(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_MM_SUPPLIES_QTIO_RPT_S", obj);
		}

		public DataTable SearchDetail(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_MM_SUPPLIES_QTIO_RPT_S1", obj);
		}

		internal bool Save(DataTable dt)
		{
			SpInfo si = new SpInfo();

			si.DataValue = dt;
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;
			si.SpNameInsert = "SP_CZ_MM_SUPPLIES_QTIO_RPT_I";
			si.SpNameUpdate = "SP_CZ_MM_SUPPLIES_QTIO_RPT_U";
			si.SpNameDelete = "SP_CZ_MM_SUPPLIES_QTIO_RPT_D";
			si.SpParamsInsert = new string[] { "CD_COMPANY",
											   "NO_IO",
											   "CD_ITEM",
											   "DT_IO",
											   "CD_PARTNER",
											   "UM_GR",
											   "QT_GR",
											   "AM_GR",
											   "QT_GI",
											   "AM_GI",
											   "FG_PS",
											   "NO_EMP",
											   "DC_RMK",
											   "NO_DOCU",
											   "ID_INSERT" };
			si.SpParamsUpdate = new string[] { "CD_COMPANY",
											   "NO_IO",
											   "CD_ITEM",
											   "FG_PS",
											   "DT_IO",
											   "UM_GR",
											   "QT_GR",
											   "AM_GR",
											   "QT_GI",
											   "AM_GI",
											   "NO_EMP",
											   "DC_RMK",
											   "NO_DOCU",
											   "CD_PARTNER",
											   "ID_UPDATE" };
			si.SpParamsDelete = new string[] { "CD_COMPANY",
											   "NO_IO" };

			return DBHelper.Save(si);
		}
	}
}