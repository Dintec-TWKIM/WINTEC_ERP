using System.Data;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_SA_CLAIM_SUB_BIZ
    {
        #region Search
        public DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CLAIMH_SUB_S", obj);
            
            return dataTable;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CLAIML_SUB_S", obj);
            
            return dataTable;
        }
        #endregion
    }
}
