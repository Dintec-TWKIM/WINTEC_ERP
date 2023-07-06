using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_PU_IV_REG_SUB_BIZ
    {
        #region 조회
        public DataSet Search(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_PU_IV_REG_SUB_S", obj);
            T.SetDefaultValue(dataSet);
            return dataSet;
        }
        #endregion

        public Decimal 환율(string 매입일자, string 통화명)
        {
            SpInfo spInfo = new SpInfo();
            spInfo.SpNameSelect = "SP_CZ_SA_IV_SUB_EXCH_S";
            spInfo.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                   매입일자,
                                                   통화명 };

            return Convert.ToDecimal(((ResultData)(Global.MainFrame.FillDataTable(spInfo))).OutParamsSelect[0, 0]);
        }
    }
}