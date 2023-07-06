using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    class P_CZ_SA_LOG_CHARGE_RPT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_CHARGE_RPT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchHeader(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_CHARGE_RPTH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchLine(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_CHARGE_RPTL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchLine1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_CHARGE_RPTL_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
