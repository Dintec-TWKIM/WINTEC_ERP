using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_SA_IV_BASE_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IV_BASEH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
