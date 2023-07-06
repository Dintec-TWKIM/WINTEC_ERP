using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_IV_RPT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IV_RPT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchBase(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IV_BASE_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchL(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IV_RPTL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchR(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IV_RPTR_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
