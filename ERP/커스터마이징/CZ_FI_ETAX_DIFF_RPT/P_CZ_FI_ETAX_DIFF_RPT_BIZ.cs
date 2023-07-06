using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_FI_ETAX_DIFF_RPT_BIZ
	{
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_FI_ETAX_DIFF_RPT_S", obj);

            return dataTable;
        }

        internal DataTable SearchDetailL(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_FI_ETAX_DIFF_RPTL_S", obj);

            return dataTable;
        }

        internal DataTable SearchDetailR(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_FI_ETAX_DIFF_RPTR_S", obj);

            return dataTable;
        }
    }
}
