using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Dintec;

namespace cz
{
    internal class P_CZ_SA_TR_PERFORM_PIVOT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_TR_PERFORM_PIVOT_S", obj);
        }

        public DataTable Search1(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_TR_PERFORM_PIVOT_S1", obj);
        }
    }
}
