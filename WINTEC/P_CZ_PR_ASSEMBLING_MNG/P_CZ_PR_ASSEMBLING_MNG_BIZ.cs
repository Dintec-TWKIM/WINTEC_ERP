using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_PR_ASSEMBLING_MNG_BIZ
	{
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ASSEMBLING_MNG_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ASSEMBLING_MNG_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ASSEMBLING_MNG_S2", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchID(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ASSEMBLING_MNG_ID", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchIDMatch(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ASSEMBLING_MNG_ID_MATCH", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ASSEMBLING_MNGD_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}
