using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_MM_STOCK_PO_RPT_BIZ
    {
        public DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_MM_STOCK_PO_RPT_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_MM_STOCK_PO_RPT_S2", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search3(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_MM_STOCK_PO_RPT_S3", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
