using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_SA_VSSL_ETRYND_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_VSSL_ETRYNDH1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_VSSL_ETRYNDH2", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_VSSL_ETRYNDL1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_VSSL_ETRYNDL2", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
