using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_OUTSTANDING_INV_RPT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_OUTSTANDING_INV_RPT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search매출리스트(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_OUTSTANDING_INV_RPT_IV_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search수금리스트(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_OUTSTANDING_INV_RPT_RCP_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search차트(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_OUTSTANDING_INV_RPT_CHART_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
