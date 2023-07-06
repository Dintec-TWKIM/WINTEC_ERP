using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_SA_GIR_FORMAT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_FORMAT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
