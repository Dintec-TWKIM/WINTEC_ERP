using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_GIR_HISTORY_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_GIR_HISTORYH_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_GIR_HISTORYL_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }
    }
}
