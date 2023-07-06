using Duzon.ERPU;
using System.Data;

namespace cz
{
	internal class P_CZ_SA_SO_PR_WO_RPT_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_SO_PR_WO_RPTH_S", obj);
			return dt;
		}

		public DataTable SearchDetail(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_SO_PR_WO_RPTD_S", obj);
			return dt;
		}
	}
}
