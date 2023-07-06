using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_PU_TRADEDAY_RPT_BIZ
    {
        public DataTable Search(object[] obj, bool 재고자산수불부)
        {
            SpInfo spInfo = new SpInfo();

            if (재고자산수불부 == true)
                return DBHelper.GetDataTable("SP_CZ_PU_TRADEDAY_RPTH_S", obj);
            else
                return DBHelper.GetDataTable("SP_CZ_PU_TRADEDAY_RPT_INH_S", obj);
        }

        public DataTable SearchDetail(object[] obj, bool 재고자산수불부)
        {
            SpInfo spInfo = new SpInfo();

            if (재고자산수불부 == true)
                return DBHelper.GetDataTable("SP_CZ_PU_TRADEDAY_RPTL_S", obj);
            else
                return DBHelper.GetDataTable("SP_CZ_PU_TRADEDAY_RPT_INL_S", obj);
        }
    }
}
