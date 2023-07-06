using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_PU_REQR_REG_SUB_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_REQR_REG_SUBH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_REQR_REG_SUBL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
