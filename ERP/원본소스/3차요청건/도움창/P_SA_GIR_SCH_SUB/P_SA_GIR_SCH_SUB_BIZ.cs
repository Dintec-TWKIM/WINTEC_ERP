using System.Data;
using Duzon.ERPU;

namespace sale
{
    class P_SA_GIR_SCH_SUB_BIZ
    {
        #region ♣ 조회
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIR_SUB_SELECT", obj, "NO_GIR");
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIR_SUB_SELECT1", obj, "SEQ_GIR");
            T.SetDefaultValue(dt);
            return dt;
        }
        #endregion 
    }
}