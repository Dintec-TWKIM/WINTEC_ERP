using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_HR_PEVALU_RESULT_RPT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_HR_PEVALU_RESULT_RPT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
