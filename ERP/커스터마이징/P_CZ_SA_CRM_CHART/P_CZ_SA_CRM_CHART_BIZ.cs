using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Dintec;

namespace cz
{
    internal class P_CZ_SA_CRM_CHART_BIZ
    {
        internal DataSet Search(object[] obj, string 조회유형)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_CRM_CHART_" + 조회유형 + "_S", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }

        internal DataSet Search1(object[] obj, string 조회유형)
        {
            DataSet dataSet = DBMgr.GetDataSet("SP_CZ_SA_CRM_CHART_" + 조회유형 + "_S", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }
    }
}
