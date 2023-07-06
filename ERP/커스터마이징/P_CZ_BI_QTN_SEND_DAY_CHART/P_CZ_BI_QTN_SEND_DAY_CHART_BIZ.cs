using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_BI_QTN_SEND_DAY_CHART_BIZ
    {
        internal DataTable Search1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_SEND_DAY_CHART_S1", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search2(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_SEND_DAY_CHART_S2", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search3(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_SEND_DAY_CHART_S3", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search4(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_SEND_DAY_CHART_S4", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search5(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_SEND_DAY_CHART_S5", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search6(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_SEND_DAY_CHART_S6", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }
    }
}
