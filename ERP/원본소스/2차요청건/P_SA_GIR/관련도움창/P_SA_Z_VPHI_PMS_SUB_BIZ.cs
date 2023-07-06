using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace sale
{
    class P_SA_Z_VPHI_PMS_SUB_BIZ
    {
        #region 조회
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_Z_VPHI_PMS_SUB_S", obj);
            T.SetDefaultValue(dt);            
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_Z_VPHI_PMS_SUB_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
        #endregion
    }
}