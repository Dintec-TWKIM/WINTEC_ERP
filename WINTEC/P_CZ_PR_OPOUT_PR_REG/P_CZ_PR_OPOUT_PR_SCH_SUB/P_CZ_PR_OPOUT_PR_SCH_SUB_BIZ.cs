using Duzon.ERPU;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_OPOUT_PR_SCH_SUB_BIZ
    {
        public DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_OPOUT_PR_SCH_SUB_S", obj);
        }

        public DataTable SearchDetail(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_OPOUT_PR_SCH_SUB_S1", obj);
        }
    }
}