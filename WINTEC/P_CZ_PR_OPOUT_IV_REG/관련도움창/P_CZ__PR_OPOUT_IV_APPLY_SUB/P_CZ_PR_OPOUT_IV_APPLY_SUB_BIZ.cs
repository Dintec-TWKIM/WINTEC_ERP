using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_OPOUT_IV_APPLY_SUB_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_CZ_PR_OPOUT_IV_APPLY_SUB_SEL", obj);
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            return dataTable;
        }

        public DataTable Chcoef_Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_PR_OPOUT_IV_APPLY_SUB_CH_S", obj);
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            return dataTable;
        }

        public decimal 환율(string 매출일자, string 환종) => Convert.ToDecimal(((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_SA_IV_SUB_SELECT_EXCH",
            SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                            매출일자,
                                            환종 }
        })).OutParamsSelect[0, 0]);

        public DataSet Search_Um(object[] obj) => DBHelper.GetDataSet("UP_SU_COMMON_UM_S", obj);
    }
}