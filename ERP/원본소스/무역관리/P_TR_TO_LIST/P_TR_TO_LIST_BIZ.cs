using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;

namespace trade
{
    class P_TR_TO_LIST_BIZ
    {
        #region ♣ 멤버변수 / 생성자

        #region -> 멤버변수

        #endregion

        #region -> 생성자

        public P_TR_TO_LIST_BIZ()
        {
        }

        #endregion

        #endregion

        #region -> 조회

        public DataTable search(object[] args)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_TO_LIST_SELECT";
            si.SpParamsSelect = args;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;
            return dt;
        }

        #endregion
    }
}
