using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_MM_NORMAL_ITEM_RPT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_MM_NORMAL_ITEM_RPT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_MM_NORMAL_ITEM_RPT_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataSet SearchDetail(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_MM_NORMAL_ITEM_RPTD_S", obj);
            T.SetDefaultValue(ds);
            return ds;
        }
    }
}
