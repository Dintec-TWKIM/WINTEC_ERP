using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_EALRY_WARNING_SYSTEM_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_EALRY_WARNING_SYSTEMH_S", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_EALRY_WARNING_SYSTEMH_S1", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataSet SearchDetail(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_EALRY_WARNING_SYSTEML_S", obj);
            return dataSet;
        }

        internal DataTable SearchDetail1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_EWS_LOG_S", obj);
            return dataTable;
        }
    }
}
