using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_GIR_SCH_SUB_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dataTable;

            dataTable = DBHelper.GetDataTable("SP_CZ_SA_GIRH_SCH_SUB_S", obj, "NO_GIR");

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable;

            dataTable = DBHelper.GetDataTable("SP_CZ_SA_GIRL_SCH_SUB_S", obj, "SEQ_GIR");

            T.SetDefaultValue(dataTable);
            return dataTable;
        }
    }
}
