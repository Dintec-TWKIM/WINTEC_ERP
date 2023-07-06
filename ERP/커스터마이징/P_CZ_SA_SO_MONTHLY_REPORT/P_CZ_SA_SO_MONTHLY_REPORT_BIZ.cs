using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    internal class P_CZ_SA_SO_MONTHLY_REPORT_BIZ
	{
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MONTHLY_REPORT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
