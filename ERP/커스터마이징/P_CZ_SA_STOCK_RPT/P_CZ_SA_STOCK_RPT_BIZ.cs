using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_SA_STOCK_RPT_BIZ
    {
        public DataTable SearchHeader1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_STOCK_RPTH1_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchHeader2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_STOCK_RPTH2_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchHeader3(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_STOCK_RPTH3_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchHeader4(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_STOCK_RPTH4_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchLine1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_STOCK_RPTL1_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchLine2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_STOCK_RPTL2_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
