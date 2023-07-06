using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;

namespace pur
{
    class P_PU_INV_SCH2_BIZ
    {
        #region ♣ 멤버변수 / 생성자

        #region -> 멤버변수

        #endregion

        #region -> 생성자

        public P_PU_INV_SCH2_BIZ()
        {
        }

        #endregion

        #endregion

        #region -> 조회

        public DataTable search(object[] obj)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_INV_SCH2_SELECT";
            si.SpParamsSelect = obj;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            foreach (DataColumn col in dt.Columns)
            {
                if (col.DataType == typeof(decimal))
                    col.DefaultValue = 0;
                if (col.DataType == typeof(string))
                    col.DefaultValue = "";
            }

            return dt;
        }

        #endregion
    }
}
