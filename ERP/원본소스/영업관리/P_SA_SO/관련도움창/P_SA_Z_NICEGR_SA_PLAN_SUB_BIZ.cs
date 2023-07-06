using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace sale
{
    class P_SA_Z_NICEGR_SA_PLAN_SUB_BIZ
    {
        #region Search
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_Z_NICEGR_SA_PLAN_SUB_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        } 
        #endregion
    }
}
