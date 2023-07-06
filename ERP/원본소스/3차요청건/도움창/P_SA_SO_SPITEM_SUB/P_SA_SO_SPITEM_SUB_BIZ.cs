using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace sale
{
    class P_SA_SO_SPITEM_SUB_BIZ
    {
        #region 조회
        public DataSet Search(object[] obj)
        {
            ResultData rd = (ResultData)Global.MainFrame.FillDataSet("UP_SA_SO_SPITEM_SEARCH", obj);
            DataSet ds = (DataSet)rd.DataValue;

            return ds;
        }
        #endregion
    }
}
