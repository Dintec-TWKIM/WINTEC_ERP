using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_SA_GIR_SUB_BIZ
    {
        #region 조회
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIRH_SUB_S", obj);
            T.SetDefaultValue(dt);            
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIRL_SUB_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable search_EnvMng()
        {
            return DBHelper.GetDataTable("UP_SA_ENV_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode });
        }
        #endregion
    }
}