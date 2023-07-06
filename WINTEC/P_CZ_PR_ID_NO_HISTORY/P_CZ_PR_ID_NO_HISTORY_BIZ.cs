using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_PR_ID_NO_HISTORY_BIZ
	{
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ID_NO_HISTORYH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ID_NO_HISTORYL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ID_NO_HISTORYL1_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ID_NO_HISTORYL2_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
