using System.Data;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_FI_ALLOCA_MNGPL_BIZ
    {
        internal DataSet Search(object[] args)
        {
            return DBHelper.GetDataSet("SP_CZ_FI_ALLOCA_MNGPL_S", args);
        }
    }
}