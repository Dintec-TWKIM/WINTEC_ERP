using Duzon.ERPU;
using System.Data;

namespace cz
{
	internal class P_CZ_PR_ASSEMBLING_COUNTING_SUB_BIZ
	{
		internal DataTable Search(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_ASSEMBLING_COUNTING_SUB_S", obj);
		}
	}
}