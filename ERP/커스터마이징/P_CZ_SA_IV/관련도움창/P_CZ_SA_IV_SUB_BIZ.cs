using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_IV_SUB_BIZ
    {
        private string 회사 = Global.MainFrame.LoginInfo.CompanyCode;

        public DataTable Search(object[] obj)
        {
            DataTable dataTable;

            dataTable = DBHelper.GetDataTable("SP_CZ_SA_IV_SUBH_S", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable;

            dataTable = DBHelper.GetDataTable("SP_CZ_SA_IV_SUBL_S", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        public Decimal 환율(string 매출일자, string 통화명)
        {
            SpInfo spInfo = new SpInfo();
            spInfo.SpNameSelect = "SP_CZ_SA_IV_SUB_EXCH_S";
            spInfo.SpParamsSelect = new object[] { this.회사,
                                                    매출일자,
                                                    통화명 };

            return Convert.ToDecimal(((ResultData)(Global.MainFrame.FillDataTable(spInfo))).OutParamsSelect[0, 0]);
        }
    }
}
