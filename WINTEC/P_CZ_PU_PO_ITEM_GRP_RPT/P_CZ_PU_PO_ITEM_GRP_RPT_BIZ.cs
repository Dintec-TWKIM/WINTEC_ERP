using Duzon.ERPU;
using System.Data;

namespace cz
{
	internal class P_CZ_PU_PO_ITEM_GRP_RPT_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = null;
			dt = DBHelper.GetDataTable("SP_CZ_PU_PO_ITEM_GRP_RPT_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_PO_ITEM_GRP_RPT_L_S", obj);
			return dt;
		}
	}
}