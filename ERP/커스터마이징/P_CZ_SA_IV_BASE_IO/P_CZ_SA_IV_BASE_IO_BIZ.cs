using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	class P_CZ_SA_IV_BASE_IO_BIZ
	{
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IV_BASE_IOH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IV_BASE_IOL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
