using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Dintec;

namespace cz
{
    internal class P_CZ_MM_MONTHLY_STOCK_PIVOT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBMgr.GetDataTable("SP_CZ_MM_MONTHLY_STOCK_PIVOT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
