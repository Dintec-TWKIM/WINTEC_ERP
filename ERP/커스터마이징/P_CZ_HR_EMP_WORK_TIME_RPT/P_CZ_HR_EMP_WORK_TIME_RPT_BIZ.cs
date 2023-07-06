using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    internal class P_CZ_HR_EMP_WORK_TIME_RPT_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_EMP_WORK_TIME_RPT_S", obj);
        }

        internal DataTable SearchMonth(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_EMP_WORK_TIME_RPT_MONTH_S", obj);
        }

        internal DataTable SearchWeek(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_EMP_WORK_TIME_RPT_WEEK_S", obj);
        }

        internal DataTable SearchDay(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_EMP_WORK_TIME_RPT_DAY_S", obj);
        }
    }
}
