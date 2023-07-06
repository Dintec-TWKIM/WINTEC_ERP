using System.Data;
using Duzon.ERPU;

namespace pur
{
    class P_PU_PO_RPT_PIVOT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PU_PO_RPT_PIVOT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        } 
    }
}
